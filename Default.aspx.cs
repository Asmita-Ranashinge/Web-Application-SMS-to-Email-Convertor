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

public partial class _Default : System.Web.UI.Page
{
    string varMsg, varContactNo, varPassword;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            varMsg = Request.QueryString["msg"].ToString();
            lblMsg.Text = varMsg;
        }
        catch
        {
            lblMsg.Text = "";
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        varContactNo = txtContactNo.Text;
        varPassword = txtPassword.Text;
        varPassword = process.funcEncrypt(varPassword);
        DBConnect.varQuery = "select * from userdetails where contactno  = '"+varContactNo+"' and loginpwd = '"+varPassword+"'";
        DBConnect.sqlReader = DBConnect.funcRetrieveData();

        if (DBConnect.sqlReader.Read())
        {
            Session["userid"] = DBConnect.sqlReader["userid"]. ToString();
            Session["contactno"] = DBConnect.sqlReader["contactno"].ToString();
            Session["user"] = DBConnect.sqlReader["FirstName"].ToString();
            Session.Timeout = 20;
            
            lblMsg.Text = "";

            sendSMS.varMessage = "S.E.C.: Hi " + DBConnect.sqlReader["FirstName"].ToString() + ", You have been looged in to youe SMS to Email Converter account ";
            sendSMS.getPortDetails(ref sendSMS.port);
           // sendSMS.sendMsg(ref sendSMS.port, Session["contactno"].ToString(), sendSMS.varMessage);
            DBConnect.sqlReader.Close();
            Response.Redirect("home.aspx");
        }
        else
        {
            lblMsg.Text = "Invalid Login Details. Please check your username and password";
        }
    }
}
