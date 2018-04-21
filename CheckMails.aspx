<%@ Page Language="C#" MasterPageFile="~/mpPublic.master" AutoEventWireup="true" CodeFile="CheckMails.aspx.cs" Inherits="CheckMails" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server" />
           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                    <asp:Timer ID="tnCheckMails" runat="server" Interval="9000" ontick="tnCheckMails_Tick" 
                        >
                    </asp:Timer>
                    
                     <asp:Timer ID="tmCheckSMS" runat="server" Interval="9000" ontick="tmCheckSMS_Tick"  
                        >
                    </asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
</asp:Content>

