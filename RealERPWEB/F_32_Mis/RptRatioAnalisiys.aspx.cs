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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_32_Mis
{

    public partial class RptRatioAnalisiys : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                // string type = this.Request.QueryString["Type"].ToString().Trim();

                //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //  string date1 = "01-" + ASTUtility.Right(date, 8);
                // // this.txtfrmdate.Text = date;
                ((Label)this.Master.FindControl("lblTitle")).Text = "Ratio Analysis";

                //  this.txttodate.Text = Convert.ToDateTime(date1).AddMonths(12).ToString("dd-MMM-yyyy");


                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy"); ; // "01-Jan-" + date.Substring(7);


                GetCompCode();

                this.lbtnOk_Click(null, null);


            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);


            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GetFinancialAnalysis();
        }
        private void GetFinancialAnalysis()
        {
            string comcod = this.GetCompCode();

            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string date1 = "01-Jan-" + date.Substring(7);

            string txtdatefrm = date1; //Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(date1).AddMonths(12).ToString("dd-MMM-yyyy");  //Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_MIS01", "GETRATIOANALISIYS", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvIncomeMon.DataSource = null;
                this.gvIncomeMon.DataBind();
                return;
            }
            ViewState["tblFinanAnalysis"] = HiddenSameData(ds1.Tables[0]); ;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblFinanAnalysis"];

            this.gvIncomeMon.DataSource = dt;
            this.gvIncomeMon.DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintRatio();
        }

        private void PrintRatio()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            // string hostname = hst["hostname"].ToString();
            string currentDate = DateTime.Now.ToString("dd-MMM-yyyy");

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblFinanAnalysis"];



            //Session["tbRatioData"] = ds1.Tables[0];
            //DataTable dt = (DataTable)Session["tbRatioData"];
            // DataSet ds = (DataSet)ViewState["tbRatioData"];

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccRatioAnalysis>();
            LocalReport Rpt1 = null;
            Hashtable reportParm = new Hashtable();
            reportParm["CompanyName"] = comnam.ToUpper();
            reportParm["header"] = "Ratio Analysis";
            reportParm["CurrentDate"] = "As On :" + currentDate;
            reportParm["txtuserinfo"] = "Print Source :" + username + " , " + session + " , " + printdate;


            //reportParm["date"] = "From  " + this.txtEntryDate.Text.ToString() + "   To " + this.txtLDAte.Text.ToString();
            //reportParm["datetex"] = "Batch Description";

            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptRatioAnalysis", lst, reportParm, null);
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["grp"].ToString();
                }

            }

            return dt1;
        }
    }
}