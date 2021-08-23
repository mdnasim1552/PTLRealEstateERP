using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;

namespace RealERPWEB.F_46_GrMgtInter
{
    public partial class RptGrpDailyReportJq : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                this.SetCompCode();
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Management Interface";
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script> alert('<% Response.Write(this.txtDate.Text); %>');</script>";           
            }

        }


        public void SetCompCode()
        {
            Hashtable hst = (Hashtable)System.Web.HttpContext.Current.Session["tblLogin"];
            string str = (hst["comcod"].ToString());
            Session["comcod"] = (hst["comcod"].ToString());
        }
        //public string SetCompCode()
        //{
        //    Hashtable hst = (Hashtable)System.Web.HttpContext.Current.Session["tblLogin"];
        //    //string str = (hst["comcod"].ToString());
        //    //Session["comcod"]=(hst["comcod"].ToString());
        //    //Hashtable hst = (Hashtable)Session["tblLogin"];

        //   // string qcomcod = this.Request.QueryString["comcod"] ?? comcod;

        //    string comcod = hst["comcod"].ToString ();
        //    string qcomcod = this.Request.QueryString["comcod"] ?? comcod;
        //    comcod = qcomcod.Length > 0 ? qcomcod : comcod;
        //    //return comcod;

        //    //comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString () : comcod;
        //    return comcod;
        //}
    }
}
