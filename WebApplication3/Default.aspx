<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication3._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <p>

            Number of Players&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="NumberOfPlayersTextBox" runat="server"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator2" runat="server" Display="Dynamic" ErrorMessage="Must be an integer greater than 1" ForeColor="#FF3300" MaximumValue="86400" MinimumValue="2" ControlToValidate="NumberOfPlayersTextBox" Type="Integer"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Required" ForeColor="#FF3300" ControlToValidate="NumberOfPlayersTextBox"></asp:RequiredFieldValidator>

        </p>
        <p>
            Time Played (In Minutes)
            <asp:TextBox ID="MinutesPlayedTextBox" runat="server"/>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="MinutesPlayedTextBox" Display="Dynamic" ErrorMessage="Must be an integer greater than 0" ForeColor="#FF3300" MaximumValue="86400" MinimumValue="1" Type="Integer"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="MinutesPlayedTextBox" Display="Dynamic" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:DropDownList ID="ModeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ModeDropDownList_SelectedIndexChanged">
                <asp:ListItem Selected="True">Ranked</asp:ListItem>
                <asp:ListItem>Co-op Game</asp:ListItem>
                <asp:ListItem>Unranked Winners</asp:ListItem>
                <asp:ListItem>Unranked Losers</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="TheOnlyFuckingButton" runat="server" OnClick="Button1_Click" Text="Get XP" Height="34px" Width="180px" />
        </p>
        <p>

            <asp:Label ID="HelperLabel" runat="server" Text="HelperBox" Visible="False"></asp:Label>
            <asp:TextBox ID="HelperInformationTextBox" runat="server" Visible="False"></asp:TextBox>

            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="HelperInformationTextBox" Display="Dynamic" ErrorMessage="Number must be an integer" ForeColor="#FF3300" MaximumValue="86400" MinimumValue="1" Type="Integer"></asp:RangeValidator>

        </p>
            <asp:TextBox ID="TextBox1" runat="server" Height="273px" TextMode="MultiLine" Width="419px" ReadOnly="True"></asp:TextBox>
    </div>

    </asp:Content>