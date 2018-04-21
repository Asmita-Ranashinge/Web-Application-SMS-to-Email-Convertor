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
using System.IO;

public partial class CheckMails : System.Web.UI.Page
{
    string varServer, varEmailId, varPassword, varPath, varUserInboxPath, varPOP, varContact;
    bool varSSL = false;
    string[,] arrMails;

    protected void Page_Load(object sender, EventArgs e)
    {
        sendSMS.funcCheckUnReadSMS();

      //  funcCheckMails();
    }

    private void funcCheckMails()
    {
        DBConnect.varQuery = "select s.ServerName, s.POP, s.SMTP, s.Port, e.EmailId, e.EmailPwd,e.UserId, u.ContactNo from EmailAccountDetails e, UserDetails u, ServerDetails s where e.UserId = u.UserId and e.ServerId =s.ServerId ";
        DBConnect.sqlReader = DBConnect.funcRetrieveData();

        while (DBConnect.sqlReader.Read())
        {
            varEmailId = DBConnect.sqlReader["EmailId"].ToString();
            varPassword = DBConnect.sqlReader["EmailPwd"].ToString();
            varPassword = process.funcDecrypt(varPassword);
            varServer = DBConnect.sqlReader["ServerName"].ToString();
            varPOP = DBConnect.sqlReader["POP"].ToString();
            varContact = DBConnect.sqlReader["ContactNo"].ToString();

            varUserInboxPath = Server.MapPath("\\Users");
            varUserInboxPath = varUserInboxPath + "\\" + DBConnect.sqlReader["UserId"].ToString() + "\\Inbox";

            if (!Directory.Exists(varUserInboxPath))
                Directory.CreateDirectory(varUserInboxPath);



            int n = Email.funcCheckEmail(varPOP, varEmailId, varPassword, varSSL, varUserInboxPath, out arrMails);


            if (n != 0 && n != -1)
            {
                if (n == 1 && arrMails != null)
                {

                    sendSMS.varMessage = "S.E.C.: You have " + n + " new mail in your inbox for email id " + varEmailId + "\n From : " + arrMails[0, 0] + "\n Subject : " + arrMails[0, 1].Substring(0, arrMails[0, 1].IndexOf("(Trial Version)"));
                    sendSMS.getPortDetails(ref sendSMS.port);
                    sendSMS.sendMsg(ref sendSMS.port, varContact, sendSMS.varMessage);
                }
                else
                {
                    sendSMS.varMessage = "S.E.C.: You have " + n + " new mails in your inbox for email id " + varEmailId;
                    sendSMS.getPortDetails(ref sendSMS.port);
                    sendSMS.sendMsg(ref sendSMS.port, varContact, sendSMS.varMessage);
                }
            }

        }
    }


    protected void tnCheckMails_Tick(object sender, EventArgs e)
    {

        //DBConnect.varQuery = "select s.ServerName, s.POP, s.SMTP, s.Port, e.EmailId, e.EmailPwd,e.UserId, u.ContactNo from EmailAccountDetails e, UserDetails u, ServerDetails s where e.UserId = u.UserId and e.ServerId =s.ServerId ";
        //DBConnect.sqlReader = DBConnect.funcRetrieveData();

        //while (DBConnect.sqlReader.Read())
        //{
        //    varEmailId = DBConnect.sqlReader["EmailId"].ToString();
        //    varPassword = DBConnect.sqlReader["EmailPwd"].ToString();
        //    varPassword = process.funcDecrypt(varPassword);
        //    varServer = DBConnect.sqlReader["ServerName"].ToString();
        //    varPOP = DBConnect.sqlReader["POP"].ToString();
        //    varContact = DBConnect.sqlReader["ContactNo"].ToString();

        //    varUserInboxPath = Server.MapPath("\\SMSToEmailConvertor\\Users");
        //    varUserInboxPath = varUserInboxPath + "\\" + DBConnect.sqlReader["UserId"].ToString() + "\\Inbox";

        //    if (!Directory.Exists(varUserInboxPath))
        //        Directory.CreateDirectory(varUserInboxPath);
            
        

        //    int n = Email.funcCheckEmail(varPOP, varEmailId, varPassword, varSSL, varUserInboxPath, out arrMails);


        //    if (n != 0 && n != -1)
        //    {
        //        if (n == 1 && arrMails!=null)
        //        {

        //            sendSMS.varMessage = "S.E.C.: You have " + n + " new mail in your inbox for email id " + varEmailId + "\n From : " + arrMails[0, 0] + "\n Subject : " + arrMails[0, 1].Substring(0, arrMails[0, 1].IndexOf("(Trial Version)"));
        //            sendSMS.getPortDetails(ref sendSMS.port);
        //            sendSMS.sendMsg(ref sendSMS.port, varContact, sendSMS.varMessage);
        //        }
        //        else
        //        {
        //            sendSMS.varMessage = "S.E.C.: You have " + n + " new mails in your inbox for email id " + varEmailId;
        //            sendSMS.getPortDetails(ref sendSMS.port);
        //            sendSMS.sendMsg(ref sendSMS.port, varContact, sendSMS.varMessage);
        //        }
        //    }

        //}

    }
    protected void tmCheckSMS_Tick(object sender, EventArgs e)
    {
        sendSMS.funcCheckUnReadSMS();
    }
}
