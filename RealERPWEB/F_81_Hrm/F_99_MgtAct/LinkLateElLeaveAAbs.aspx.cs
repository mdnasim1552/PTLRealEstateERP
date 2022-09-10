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

namespace RealERPWEB.F_81_Hrm.F_99_MgtAct
{
    public partial class LinkLateElLeaveAAbs : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Late, Early Leave, On Leave and Absent Information";
                this.ShowView();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "LELLAndAbsent":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowLELLAndAbsent();
                    break;

                case "RptSupCredit02":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "LELLAndAbsent":
                    this.ShowLELLAndAbsent();
                    break;

            }
        }

        private void ShowLELLAndAbsent()
        {
            Session.Remove("tblemplellandabs");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string Date = Convert.ToDateTime(this.Request.QueryString["date"].ToString()).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE02", "RPTLATEEONANDABSENTDET", Date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rplellaabsemp.DataSource = null;
                this.rplellaabsemp.DataBind();
                return;
            }
            Session["tblemplellandabs"] = ds1.Tables[0];
            Session["tblsummary"] = ds1.Tables[1];
            this.Data_Bind();

            this.lblvaldate.Text = Date;

            this.lbltointime.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["present"]).ToString("#,##0;(#,##0); ");
            this.lblvaltoStaff.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["ttlstaff"]).ToString("#,##0;(#,##0); ");
            this.lbltolate.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["late"]).ToString("#,##0;(#,##0); ");
            this.lbltoeleave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["earlyLev"]).ToString("#,##0;(#,##0); ");
            this.lbltooleave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["onlev"]).ToString("#,##0;(#,##0); ");
            this.lbltoabsent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["absnt"]).ToString("#,##0;(#,##0); ");



        }
      
        









        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblemplellandabs"];



            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "LELLAndAbsent":
                    this.rplellaabsemp.DataSource = dt;
                    this.rplellaabsemp.DataBind();
                    break;





            }






        }












        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "CashFlow":
                    this.RptCashFlow();
                    break;




            }





        }

        private void RptCashFlow()
        {

            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = this.GetCompCode();
            // string comnam = hst["comnam"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // DataTable dt = (DataTable)Session["tblpayst"];
            // ReportDocument rptcash = new RealERPRPT.R_32_Mis.RptCashFlowBridge();
            // TextObject txtCompanyName = rptcash.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            // txtCompanyName.Text = comnam;
            // DataTable dt1 = (DataTable)Session["tblbank"];
            // int j=1;
            // for (int i = 0; i < dt1.Rows.Count; i++)
            // {



            //         TextObject rpttxth = rptcash.ReportDefinition.ReportObjects["txtb" + j.ToString()] as TextObject;
            //         rpttxth.Text = dt1.Rows[i]["bankdesc"].ToString();
            //         j++;
            //         if (j == 12)
            //             break;

            //}




            // TextObject txtDate = rptcash.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            // txtDate.Text = " (From " + this.txtfrmDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            // TextObject txtuserinfo = rptcash.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            // rptcash.SetDataSource(dt);
            // Session["Report1"] = rptcash;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}