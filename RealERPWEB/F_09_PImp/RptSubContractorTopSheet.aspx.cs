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

namespace RealERPWEB.F_09_PImp
{
    public partial class RptSubContractorTopSheet : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = " SUB CONTRACTOR Top Sheet ";
                this.GetConTractorName();
                this.GetProjectName();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.SelectView();

            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);



        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            this.printSubConTopSheet();
            //this.printPrev();



        }

        private void printPrev()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            //DataTable dt = (DataTable)Session["tblconsddetails"];
            //DataTable dt1 = (DataTable)Session["topsheet"];

            //DataTable dt2 = dt.Copy();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "rsircode='RRRRAAAAAAAA'";
            //dt2 = dv.ToTable();

            //ReportDocument rptbill = new RealERPRPT.R_09_PImp.rptSubConBillTopSheet();

            //TextObject txtcompname = rptbill.ReportDefinition.ReportObjects["txtcompname"] as TextObject;
            //txtcompname.Text = comnam;


            //TextObject txtProjectName = rptbill.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(17);


            //TextObject txtSubConName = rptbill.ReportDefinition.ReportObjects["txtSubcon"] as TextObject;
            //txtSubConName.Text = "Sub-Contractor Name : " + this.ddlSubName.SelectedItem.Text.Trim().Substring(13);

            //TextObject txttopSheet = rptbill.ReportDefinition.ReportObjects["txttopsheet"] as TextObject;
            //txttopSheet.Text = "Top Sheet Of :" + ((dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["cbillref"].ToString());

            //TextObject txtbillno = rptbill.ReportDefinition.ReportObjects["txtbillno"] as TextObject;
            //txtbillno.Text = "Bill No : " + ((dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["billno"].ToString());

            //TextObject txtRefno = rptbill.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            //txtRefno.Text = "Nature Of Work : " + ((dt.Rows.Count == 0) ? "" : dt.Rows[0]["rsirdesc"].ToString());


            //TextObject txtDate = rptbill.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtDate.Text = "Date :" + this.txtDate.Text;


            //TextObject txtRa = rptbill.ReportDefinition.ReportObjects["txtRa"] as TextObject;
            //txtRa.Text = (dt1.Rows.Count == 0) ? "Total Bill Up To " : "Total Bill Up To " + dt1.Rows[0]["cbillref"].ToString();

            //double amt = (Math.Round(Convert.ToDouble(dt2.Rows[0]["totalbill"]), 0));
            //TextObject rpttxtTaka = rptbill.ReportDefinition.ReportObjects["takainword"] as TextObject;
            //rpttxtTaka.Text = (dt2.Rows.Count == 0) ? "" : "In Word : " + ASTUtility.Trans(amt, 2);

            //TextObject txtuserinfo = rptbill.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptbill.SetDataSource(dt);
            //Session["Report1"] = rptbill;


            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void printSubConTopSheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataTable dt = (DataTable)Session["tblconsddetails"];
            DataTable dt1 = (DataTable)Session["topsheet"];

            //DataTable dt2 = dt.Copy();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "rsircode='RRRRAAAAAAAA'";
            //dt2 = dv.ToTable();

            string project = "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(17);
            string subconname = "Sub-Contractor Name : " + this.ddlSubName.SelectedItem.Text.Trim().Substring(13);
            string topsheet = "Top Sheet Of :" + ((dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["cbillref"].ToString());
            string nature = "Nature Of Work : " + ((dt.Rows.Count == 0) ? "" : dt.Rows[0]["rsirdesc"].ToString());
            string billno = "Bill No : " + ((dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["billno"].ToString());

            string date = "Date :" + this.txtDate.Text;

            double amt = (Math.Round(Convert.ToDouble(dt.Rows[0]["totalbill"]), 0));
            string inWord = (dt.Rows.Count == 0) ? "" : "In Word : " + ASTUtility.Trans(amt, 2);


            var list = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.SubConBillTopSheet>();
            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubConBillTopSheet", list, null, null);
            rpt.SetParameters(new ReportParameter("txtComNam", comnam));
            rpt.SetParameters(new ReportParameter("txtProject", project));
            rpt.SetParameters(new ReportParameter("txtSubConName", subconname));
            rpt.SetParameters(new ReportParameter("txtSheetNo", topsheet));
            rpt.SetParameters(new ReportParameter("txtNature", nature));
            rpt.SetParameters(new ReportParameter("txtBillNo", billno));
            rpt.SetParameters(new ReportParameter("txtDate", date));
            rpt.SetParameters(new ReportParameter("txtInWord", inWord));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME_01", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            // this.GetRANumber();

        }

        private void GetConTractorName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETBILLSDSUBNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "csirdesc";
            this.ddlSubName.DataValueField = "csircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();
            this.GetProjectName();



        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetConTractorName();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowBillSummary();


        }


        private string ComAccPayment()
        {
            string comcod = this.GetCompCode();
            string allpayment = "";
            switch (comcod)
            {
                case "1205":  //p2p 
                case "3351"://p2p
                case "3352"://p2p
                case "3340"://Urban
                    allpayment = "AllAccountPayment";
                    break;

                default:
                    break;
            }
            return allpayment;
        }
        private string IsADVPayment()
        {
            string comcod = this.GetCompCode();
            string advpayment = ""; 
            switch (comcod)
            {
                case "1205":  //p2p 
                case "3351"://p2p
                case "3352"://p2p
                    advpayment = "AdvPayment";
                    break;

                default:
                    break;
            }
            return advpayment;
        }


        private void ShowBillSummary()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();

            string SubconName = this.ddlSubName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string allpayment = ComAccPayment();
            string advpayment = IsADVPayment();
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SUBCONTRACTOR", "RPTSUBCONTRACTORBILL", PactCode, SubconName, date, allpayment, advpayment, "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblconsddetails"] = ds1.Tables[0];
            Session["topsheet"] = ds1.Tables[1];
            this.gvSubBill.Columns[4].HeaderText = (ds1.Tables[1].Rows.Count == 0) ? "Total Bill Up To " : "Total Bill Up To " + ds1.Tables[1].Rows[0]["cbillref"].ToString();

            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt1 = (DataTable)Session["topsheet"];
            DataTable dt = (DataTable)Session["tblconsddetails"];
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();





        }


        protected void ddlSubName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

    }
}



