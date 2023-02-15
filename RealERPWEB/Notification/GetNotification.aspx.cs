using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.Notification
{
    public partial class GetNotification : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01" + date.Substring(2);
                this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "NOTIFICATIONSver";
                this.GetTNotification();

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private void GetTNotification()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();


            string Id = this.Request.QueryString["Id"] ?? "";
            string RefId = this.Request.QueryString["RefId"] ?? "";
            string ntype = this.Request.QueryString["ntype"] ?? "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GETNOTIFICAITON", Id, userid, RefId, ntype, "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblnotify"] = ds1.Tables[0];
            this.gvNotificaitons.DataSource = ds1.Tables[0];
            this.gvNotificaitons.DataBind();
        }
    }
}