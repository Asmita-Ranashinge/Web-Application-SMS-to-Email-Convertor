<%@ Page Language="C#" MasterPageFile="~/mpUser.master" AutoEventWireup="true" CodeFile="emailaccount.aspx.cs" Inherits="emailaccount" Title="Untitled Page" %>

<script runat="server">

   
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
      <asp:UpdateProgress ID="updProgress" runat="server">
         <ProgressTemplate>
                        <asp:Label ID="Label7" runat="server" 
                            Text="Validating the Email Id and Password" ForeColor="Red"></asp:Label>
                    </ProgressTemplate>
      </asp:UpdateProgress>
 <table class="style2">
                 <tr>
                     <td class="style3">
                         &nbsp;</td>
                     <td>
                         <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                 </tr>
                 <tr>
                     <td class="style3">
                         Email Account</td>
                     <td>
                         <asp:DropDownList ID="ddlEmailAccount" runat="server" 
                             onselectedindexchanged="ddlEmailAccount_SelectedIndexChanged" 
                             AutoPostBack="True" >
                             <asp:ListItem>Select</asp:ListItem>
                             <asp:ListItem>Google</asp:ListItem>
                             <asp:ListItem>Yahoo</asp:ListItem>
                             <asp:ListItem>Hotmail</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                 </tr>
                
                 </tr>
                 <tr>
                     <td class="style3">
                         POP Server</td>
                     <td>
                         <asp:TextBox ID="txtPOPServer" runat="server" Enabled="False"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td class="style3">
                         SMTP Server</td>
                     <td>
                         <asp:TextBox ID="txtSMTPServer" runat="server" Enabled="False" 
                             ></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td class="style3">
                         Port</td>
                     <td>
                         <asp:TextBox ID="txtPort" runat="server" Enabled="False"></asp:TextBox>
                     </td>
                 </tr>
                  <tr>
                     <td class="style3">
                         Email ID</td>
                     <td>
                         <asp:TextBox ID="txtEmailId" runat="server" 
                            ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" 
                             ControlToValidate="txtEmailId">Enter Email Id</asp:RequiredFieldValidator>
                     </td>
                 </tr>
                 <tr>
                     <td class="style3">
                         Password</td>
                     <td>
                         <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                             ControlToValidate="txtPassword" ErrorMessage="Enter Password"></asp:RequiredFieldValidator>
                     </td>
                 </tr>
                 <tr>
                     <td class="style3">
                         &nbsp;</td>
                     <td>
                         <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                             onclick="txtSubmit_Click" />
                     </td>
                 </tr>
            </table>
      <asp:HiddenField ID="hfServerId" runat="server" />
      <asp:GridView ID="dgvEmailAccounts" runat="server" AllowPaging="True" 
          AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsEmailAccounts" 
          ForeColor="#333333" GridLines="None" 
          onselectedindexchanged="dgvEmailAccounts_SelectedIndexChanged">
          <RowStyle BackColor="#E3EAEB" />
          <Columns>
              <asp:TemplateField>
                  <ItemTemplate>
                      <asp:RadioButton ID="rdbEdit" runat="server" GroupName="Email" Text="Edit" />
                      <asp:RadioButton ID="rdbDelete" runat="server" GroupName="Email" 
                          Text="Delete" />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:CommandField SelectText="Submit" ShowSelectButton="True" 
                  DeleteText="Submit" EditText="Submit" />
              <asp:BoundField DataField="ServerName" HeaderText="ServerName" 
                  SortExpression="ServerName" />
              <asp:BoundField DataField="EmailId" HeaderText="EmailId" 
                  SortExpression="EmailId" />
          </Columns>
          <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#7C6F57" />
          <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
      
      
            <asp:AccessDataSource ID="dsEmailAccounts" runat="server" 
          DataFile="~/App_Data/SEC.mdb" 
          SelectCommand="SELECT ServerDetails.ServerName, EmailAccountDetails.EmailId FROM (EmailAccountDetails INNER JOIN ServerDetails ON EmailAccountDetails.ServerId = ServerDetails.ServerId)">
      </asp:AccessDataSource>
      
      
            </ContentTemplate>
              </asp:UpdatePanel>
</asp:Content>

