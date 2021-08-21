using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using RealEntity.C_14_Pro;
//using RealEntity.C_17_Acc;
//using RealEntity.C_13_ProdMon;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;

//using RealEntity.C_08_PPlan;
namespace RealERPWEB.F_15_DPayReg
{
    public partial class RptPaymentGraph : System.Web.UI.Page
    {
        ProcessAccess PayData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                this.txtdate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Visible = false;
                //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
                //((Label)this.Master.FindControl("lblprintstk")).Visible = false;
                //((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((Label)this.Master.FindControl("lblTitle")).Text = "Bills (Graph)";
                string comcod = this.GetCompCode();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                lbtnOk_Click(null, null);


            }
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            Session.Remove("dsbill");
            string comcod = this.GetCompCode();
            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = PayData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENTGRAPH", "GETBILLNO", Date, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                //this.gvdaywise.DataSource = null;
                //this.gvdaywise.DataBind();
                //this.gvweek.DataSource = null;
                //this.gvweek.DataBind();
                //this.gvmonthly.DataSource = null;
                //  this.gvmonthly.DataBind();            
                return;

            }
            Session["dsbill"] = ds1;
            this.ShowGraph();
            this.BankPosition();



        }

        private void BankPosition()
        {
            string comcod = this.GetCompCode();
            string date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string date2 = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string levelbank = "12";
            DataSet ds1 = PayData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION", date1, date2, levelbank, "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='AAAABBBBAAAA'");
            dt = dv.ToTable();
            this.Hyplnkbal.Text = Convert.ToDouble(dt.Rows[0]["closdram"]).ToString("#,##0;(#,##0) ;");


        }

        private void ShowGraph()
        {

            DataSet ds1 = (DataSet)Session["dsbill"];
            var lstdaily = ds1.Tables[0].DataTableToList<RealEntity.C_14_Pro.EClassPayment.EClassDaily>();
            var lstweekly = ds1.Tables[1].DataTableToList<RealEntity.C_14_Pro.EClassPayment.EclassWeekly>();
            var lstmonthly = ds1.Tables[2].DataTableToList<RealEntity.C_14_Pro.EClassPayment.EclassMonthly>();
            var lstcatwise = ds1.Tables[3].DataTableToList<RealEntity.C_14_Pro.EClassPayment.EclassCatWise>();
            var lstacthead = ds1.Tables[4].DataTableToList<RealEntity.C_14_Pro.EClassPayment.EclassAccHead>();
            var lstpaywpro = ds1.Tables[5].DataTableToList<RealEntity.C_14_Pro.EClassPayment.EclassPaywithPro>();
            //    double tosalval = list.Select(s => s.tosalval).Sum();
            //    double salamt = list.Select(s => s.salamt).Sum();
            //    double tsaldue = list.Select(s => s.tsaldue).Sum();
            //    double collamt = list.Select(s => s.collamt).Sum();
            //    double tcoldue = list.Select(s => s.tcoldue).Sum();
            //    double tsalcoldue = list.Select(s => s.tsalcoldue).Sum();
            //    double core = 10000000;


            //    List<RealEntity.C_32_Mis.EClassAcc_03.PrjAnalysis1> list1 = new List<RealEntity.C_32_Mis.EClassAcc_03.PrjAnalysis1>();
            //    list1.Add(new EClassAcc_03.PrjAnalysis1(tosalval / core, salamt / core, tsaldue / core, collamt / core, tcoldue / core, tsalcoldue / core));
            var jsonSerializer = new JavaScriptSerializer();
            var json1 = jsonSerializer.Serialize(lstdaily);
            var json2 = jsonSerializer.Serialize(lstweekly);
            var json3 = jsonSerializer.Serialize(lstmonthly);
            var json4 = jsonSerializer.Serialize(lstcatwise);
            var json5 = jsonSerializer.Serialize(lstacthead);
            var json6 = jsonSerializer.Serialize(lstpaywpro);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "funcFundRequirement('" + json1 + "','" + json2 + "','" + json3 + "', '" + json4 + "', '" + json5 + "','" + json6 + "')", true);

        }
    }
}

