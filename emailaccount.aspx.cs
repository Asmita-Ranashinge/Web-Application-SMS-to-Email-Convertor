using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class emailaccount : System.Web.UI.Page
{
    string  varEmailAccount, varEmailId, varPassword, varPort, varPOP, varSMTP, varServerName, varServerID,  varUserId;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            varUserId = Session["userid"].ToString();
        }
        catch
        {
            //varUserId = "3";
            Response.Redirect("default.aspx");
        }


    }
    protected void txtSubmit_Click(object sender, EventArgs e)
    {
        
            varPOP = txtPOPServer.Text;
            varPort = txtPort.Text;
            varSMTP = txtSMTPServer.Text;
            lblMsg.Text = "";


            varServerID = hfServerId.Value;

            varEmailAccount = ddlEmailAccount.Text;
            varEmailId = txtEmailId.Text;
            varPassword = txtPassword.Text;
           
            if (btnSubmit.Text.Equals("Update"))
            {
               
              
                    if (funcValidateEmail())
                    {
                        varPassword = process.funcEncrypt(varPassword);
                        DBConnect.varQuery = "Update EmailAccountDetails set EmailPwd = '" + varPassword + "' where EmailId = '" + varEmailId + "' and UserId =" + Session["userid"];
                        DBConnect.funcInsertData();
                        lblMsg.Text = " Password updated successfully";
                        btnSubmit.Text = "Submit";
                        txtEmailId.Text = "";
                        txtEmailId.Enabled = true;
                        txtPOPServer.Text = "";
                        txtPort.Text = "";
                        txtSMTPServer.Text = "";
                        ddlEmailAccount.SelectedIndex = 0;
                    }
                    else
                    {
                        lblMsg.Text = "please check the Email Details";
                    }

                
                
              
            }
            else if (btnSubmit.Text.Equals("Delete"))
            {
                DBConnect.varQuery = "Delete from EmailAccountDetails  where EmailId = '" + varEmailId + "' and UserId =" + Session["userid"];
                DBConnect.funcInsertData();
                lblMsg.Text = " Email Id deleted successfully";
                btnSubmit.Text = "Submit";
                txtEmailId.Text = "";
                txtEmailId.Enabled = true;
                txtPOPServer.Text = "";
                txtPort.Text = "";
                txtSMTPServer.Text = "";
                ddlEmailAccount.SelectedIndex = 0;
                Response.Redirect("emailaccount.aspx");

            }
            else
            {
                if (!funcExistEmailId())
                {
                    if (funcValidateEmail())
                    {
                        varPassword = process.funcEncrypt(varPassword);


                        DBConnect.varQuery = "Insert into EmailAccountDetails values('" + varUserId + "','" + varServerID + "','" + varEmailId + "','" + varPassword + "')";
                        DBConnect.funcInsertData();

                        txtEmailId.Text = "";
                        txtPassword.Text = "";

                        lblMsg.Text = " Account added successfully";
                    }
                    else
                    {
                        lblMsg.Text = "please check the Email Details";

                    }
                }
                else
                {
                    lblMsg.Text = " Email Id already exist";
                }
            }

       
    }

    private bool funcExistEmailId()
    {
        DBConnect.varQuery = "select * from EmailAccountDetails where EmailId = '"+varEmailId+"' and userid = "+Session["userid"];
        DBConnect.sqlReader =  DBConnect.funcRetrieveData();
        return DBConnect.sqlReader.Read();
    }

    private bool funcValidateEmail()
    {
        string varMessage = "Hello "+Session["user"].ToString()+", <br><br> Thanks for registering your email id " + varEmailId + " with SMS to Email Convertor. <br><br> Thanks, <br> S.E.C Team";
        return Email.sendMail( varEmailId, varPassword, varEmailId, "Email Id Registration from S.E.C.", varMessage, varSMTP, varPort);

    }

    private void funcFetchServerDetails()
    {
        varEmailAccount = ddlEmailAccount.Text;
        DBConnect.varQuery = "select * from ServerDetails where ServerName = '" + varEmailAccount + "' ";
        DBConnect.sqlReader = DBConnect.funcRetrieveData();
        while (DBConnect.sqlReader.Read())
        {
            hfServerId.Value = DBConnect.sqlReader["ServerId"].ToString();
            txtPOPServer.Text =  DBConnect.sqlReader["POP"].ToString();
            txtPort.Text =  DBConnect.sqlReader["Port"].ToString();
            txtSMTPServer.Text = DBConnect.sqlReader["SMTP"].ToString();
           // txtServerName.Text =  DBConnect.sqlReader["ServerName"].ToString();

        }
    }


    protected void ddlEmailAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        funcFetchServerDetails();
    }
    protected void dgvEmailAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvrRow = dgvEmailAccounts.SelectedRow;
        int varColIndex = gvrRow.DataItemIndex;
        RadioButton rdbEdit = (RadioButton) dgvEmailAccounts.SelectedRow.FindControl("rdbEdit");
        RadioButton rdbDelete = (RadioButton)dgvEmailAccounts.SelectedRow.FindControl("rdbDelete"); 


        varEmailId =  gvrRow.Cells[3].Text.ToString();
        ddlEmailAccount.Text = gvrRow.Cells[2].Text.ToString();
        funcFetchServerDetails();
        txtEmailId.Text = gvrRow.Cells[3].Text.ToString();
        DBConnect.varQuery = "select * from EmailAccountDetails where EmailId = '" + varEmailId + "' and userid = " + Session["userid"];
        DBConnect.sqlReader = DBConnect.funcRetrieveData();

        if (DBConnect.sqlReader.Read())
        {
            txtEmailId.Enabled = false;
            txtPassword.Text = process.funcDecrypt(DBConnect.sqlReader["EmailPwd"].ToString());
        }

        if (rdbEdit.Checked)
        {
            btnSubmit.Text = "Update";
        }
        else if (rdbDelete.Checked)
        {
            btnSubmit.Text = "Delete";
            rfvPassword.Enabled = false;
        }
    }
}
