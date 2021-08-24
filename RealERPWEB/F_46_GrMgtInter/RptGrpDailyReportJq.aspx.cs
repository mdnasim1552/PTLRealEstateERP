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
using System.Web.Services;
using System.Web.Script.Services;
using RealERPRPT;
using RealEntity.C_46_GrMgtInter;

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
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script> alert('<% Response.Write(this.txtDate.Text); %>');</script>";   .
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = true;
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<DailyGrpRpt> GetDailyGrpRpt(string frdate, string todate)
        {

            DailyGrpRptMan dailyGrpRptMan = new DailyGrpRptMan();
            List<DailyGrpRpt> lst = dailyGrpRptMan.GetRptGrpDailyReport(frdate, todate);
            return lst;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            List<RealEntity.C_46_GrMgtInter.DailyGrpRpt> list = (List<RealEntity.C_46_GrMgtInter.DailyGrpRpt>)Session["tblData"];


            ReportDocument rrs1 = new ReportDocument();

            string frdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            rrs1 = new RealERPRPT.R_46_GrMgtInter.RptGrpMisDailyAct();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "(From " + frdate.Trim() + " To " + todate.Trim() + ")";


            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(list);
            //HttpContext.Current.Session["Report1"] = rrs1;
            //rptstk.SetDataSource((DataTable)ViewState["tblAprov"]);
            Session["Report1"] = rrs1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}
