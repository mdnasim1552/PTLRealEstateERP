using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;
using RealERPLIB;

namespace RealERPWEB
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (hst == null)
                    return;
                string usrid = hst["usrid"].ToString();
                string comcod = hst["comcod"].ToString();

                 

                    string eventtype = "0";
                    string eventdesc = "Login Out the system";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                 
                Session.Remove("tbllog1");
                Session.Remove("tblLogin");

                Session["ixComcod"] = comcod;

            }

            Response.Redirect("~/LogIn");

        }
    }
}