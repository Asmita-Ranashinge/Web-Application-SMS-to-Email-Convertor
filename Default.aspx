<%@ Page Language="C#" MasterPageFile="~/mpPublic.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="border: medium ridge #C0C0C0; width: 80%;" >
<tr><td align="center">
<table style="width: 100%;">
    <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align: left">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            &nbsp;
        </td>
        
    </tr>
    <tr>
        <td style="text-align: right">
            <b>&nbsp; Contact No </b></td>
        <td>
            &nbsp;
            <asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox>
        </td>
       
    </tr>
    <tr>
        <td style="text-align: right">
            <b>&nbsp; Password</b></td>
        <td>
            &nbsp;
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
        
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnLogin" runat="server" Text="Login" 
                onclick="btnLogin_Click" />
        </td>
        
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td style="text-align: right">
            <asp:HyperLink ID="hlSignUp" runat="server" Font-Bold="True" 
                NavigateUrl="~/SignUp.aspx">SignUp</asp:HyperLink>
&nbsp;
            <asp:HyperLink ID="hlForgotPassword" runat="server" Font-Bold="True" 
                NavigateUrl="~/ForgotPassword.aspx">Forgot Password</asp:HyperLink>
        </td>
        
    </tr>
</table>
</td></tr>
</table>
</asp:Content>


