using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace WebApplication3
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //First item in the drop down is 2 players but is at index 0
            if (!int.TryParse(NumberOfPlayersTextBox.Text, out int NumberOfPlayers))
            {
                return;
            }
            //Get the number of minutes
            if (!int.TryParse(MinutesPlayedTextBox.Text, out int NumberOfTimeUnits))
            {
                return;
            }
            //Mode of play. I could have gotten the value and swiched on that but meh.
            int ModeOfPlay = ModeDropDownList.SelectedIndex;
            int[] XP = XPCalc(NumberOfPlayers, NumberOfTimeUnits);
            string NewContentForTextBox = "";
            int Winner;
            int Loser;
            int NumberOfWinners = 1;

            switch (ModeOfPlay)
            {
                //Mode 0: Ranked Play
                case 0:
                    for (int n = 0; n < XP.Length; ++n)
                    {
                        NewContentForTextBox += $"{AddOrdinal(n + 1)} Place: {XP[n]}{Environment.NewLine}";
                    }
                    break;
                //Mode 1: Co - Op
                case 1:
                    NewContentForTextBox = $"Everybody earns {XP.Sum() / XP.Length} XP";
                    break;
                //Mode 2: X Winners
                case 2:
                    if (!int.TryParse(HelperInformationTextBox.Text, out NumberOfWinners))
                    {
                        NumberOfWinners = 1;
                    }

                    goto case 4;
                //break;
                //Mode 3: X Losers
                case 3:
                    if (!int.TryParse(HelperInformationTextBox.Text, out NumberOfWinners))
                    {
                        NumberOfWinners = 1;
                    }

                    NumberOfWinners = NumberOfPlayers - NumberOfWinners;

                    goto case 4;
                //Mode 4: Teams
                case 4:
                    if (NumberOfWinners <= 0 || NumberOfWinners >= NumberOfPlayers)
                    {
                        goto case 1;
                    }

                    Winner = 0;
                    Loser = 0;
                    for (int n = 0; n < XP.Length; ++n)
                    {
                        if (n < NumberOfWinners)
                        {
                            Winner += XP[n];
                        }
                        else
                        {
                            Loser += XP[n];
                        }
                    }

                    Winner /= NumberOfWinners;
                    Loser /= NumberOfPlayers - NumberOfWinners;

                    NewContentForTextBox = $"Winners: {Winner}{Environment.NewLine}Losers: {Loser}";
                    break;
                //This should never be hit, but if it is hit at least the program shows something.
                default:
                    goto case 0;
            }
            //Write the content to the textbox
            TextBox1.Text = NewContentForTextBox;
            //Try to log the file (if it fails it's not a big deal)
            try
            {
                File.AppendAllText(@"C:\home\site\wwwroot\Logs.txt", $"{NumberOfPlayers},{NumberOfTimeUnits},{ModeOfPlay}{Environment.NewLine}");
            }
            catch (IOException)
            {
                //ignore
            }
        }

        protected string AddOrdinal(int num)
        {
            //Nope, not even bothering with negatives. Fuck that shit I'm out.
            if (num <= 0)
            {
                return num.ToString();
            }

            //11, 12, and 13 are weird, so we check for them first
            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            //Otherwise we just check the last digit
            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

        protected static readonly Dictionary<double, double> SigmoidComputations = new Dictionary<double, double>();

        protected double Sigmoid(double x)
        {
            if (!SigmoidComputations.ContainsKey(x))
            {
                SigmoidComputations.Add(x, 1 / (1 + Math.Exp(-x)));
            }
            return SigmoidComputations[x];
        }

        protected int BSOffSet(int n)
        {
            switch (n % 3)
            {
                case 0:
                    return 0;
                case 1:
                    return -2;
                case 2:
                    return 2;
                default:
                    goto case 0;
            }
        }

        protected int[] XPCalc(int Players, int Time)
        {
            double[] XPList = new double[Players];
            double ScaledTime = Time / 15.0;

            XPList[0] = 5 * Players;
            
            for (int n = 1; n < Players; ++n)
            {
                double XP1 = 5.0 * Players / Math.Pow(2, n);
                double XP2 = 5.0 * Players / (n + 1);
                XPList[n] = XP1 * Sigmoid(Players - 4) + XP2 * Sigmoid(4 - Players) + BSOffSet(n);
            }

            int[] XP = XPList
                .Select(z => (int)Math.Round(z * ScaledTime)) //Compute XP Value
                .Select(z => Math.Max(z, 1)).ToArray(); //XP Minimum Must Be At Least 1

            for (int n = XP.Length - 1; n > 0; --n)
            {
                if (!(XP[n] < XP[n - 1]))
                {
                    XP[n - 1] = XP[n] + 1;
                }
            }

            return XP;
        }

        protected void ModeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ModeDropDownList.SelectedIndex)
            {
                case 0:
                case 1:
                    HelperInformationTextBox.Visible = false;
                    HelperLabel.Visible = false;
                    HelperInformationTextBox.Text = "";
                    break;
                case 2:
                    HelperInformationTextBox.Visible = true;
                    HelperLabel.Visible = true;
                    HelperInformationTextBox.Text = "1";
                    HelperLabel.Text = "Number of Winners";
                    break;
                case 3:
                    HelperInformationTextBox.Visible = true;
                    HelperLabel.Visible = true;
                    HelperInformationTextBox.Text = "1";
                    HelperLabel.Text = "Number of Losers";
                    break;

            }
        }
    }
}