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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_09_PImp
{
    public partial class RptSubContractorSd : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = type == "BillDetails" ? "Sub-Contractor Status (R/A Bill All)" : "Sub-Contractor Bill - R/A Wise";
                this.GetConTractorName();
                this.GetProjectName();
                this.SelectView();
                CommonButton();
            }

        }

        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "BillDetails":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


                case "BillRAWise":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }


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
            this.GetRANumber();

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

        private void GetRANumber()
        {

            string comcod = this.GetCompCode();
            string ConCode = this.ddlSubName.SelectedValue.ToString();
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string Ranumber = this.txtSrcRaNumber.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETRANUMBER", ConCode, pactcode, Ranumber, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlRANumber.DataTextField = "lisuno1";
            this.ddlRANumber.DataValueField = "lisuno";
            this.ddlRANumber.DataSource = ds1.Tables[0];
            this.ddlRANumber.DataBind();
            ds1.Dispose();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetConTractorName();

        }

        protected void ibtnFindRANumber_Click(object sender, EventArgs e)
        {
            this.GetRANumber();
        }

        protected void ddlSubName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString().Trim() == "BillRAWise")
                this.GetRANumber();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "BillDetails":
                    this.ShowBillDetails();
                    break;


                case "BillRAWise":
                    this.ShowBillRAWise();
                    break;

            }




        }

        private void ShowBillDetails()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string PactCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string SubconName = this.ddlSubName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "RPTSUBCONSDDETAILS", PactCode, SubconName, date, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblconsddetails"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private void ShowBillRAWise()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string Isueno = this.ddlRANumber.SelectedValue.ToString();
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "RPTBILLRAWISE", Isueno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvRABill.DataSource = null;
                this.gvRABill.DataBind();
                return;
            }
            Session["tblconsddetails"] = ds1.Tables[0];
            this.Data_Bind();



        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }
            }
            return dt1;

        }

        private void Data_Bind()
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblconsddetails"];
            switch (Type)
            {
                case "BillDetails":
                    this.gvSubBill.DataSource = dt;
                    this.gvSubBill.DataBind();
                    this.FooterCalculation(dt);
                    break;


                case "BillRAWise":
                    this.gvRABill.DataSource = dt;
                    this.gvRABill.DataBind();
                    this.FooterCalculation(dt);
                    break;

            }







        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "BillDetails":
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                         dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFSecurityAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sdamt)", "")) ? 0.00 :
                        dt.Compute("sum(sdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFdedAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dedamt)", "")) ? 0.00 :
                           dt.Compute("sum(dedamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPenaltyAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(penamt)", "")) ? 0.00 :
                           dt.Compute("sum(penamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFTotalAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayamt)", "")) ? 0.00 :
                                          dt.Compute("sum(netpayamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
                         dt.Compute("sum(payment)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 :
                          dt.Compute("sum(netpayable)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFsPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(spayment)", "")) ? 0.00 :
                          dt.Compute("sum(spayment)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFdedPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dedpayment)", "")) ? 0.00 :
                          dt.Compute("sum(dedpayment)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    break;


                case "BillRAWise":
                    ((Label)this.gvRABill.FooterRow.FindControl("lgvFBillAmtr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ? 0.00 :
                        dt.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }



        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "BillDetails":
                    this.PrintBillDetails();
                    break;


                case "BillRAWise":
                    this.PrintBillRAWise();
                    break;

            }



            string comcod = this.GetCompCode();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sub - Cont.Payment";
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString() + " Con Name: " + this.ddlSubName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }


        private void PrintBillDetails()
        {



            DataTable dt = (DataTable)Session["tblconsddetails"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string txtRefno = (dt.Rows[0]["lisurefno"].ToString().Trim().Length > 0) ? "Bill Ref. No: " + dt.Rows[0]["lisurefno"].ToString().Trim() : "";
            //string Issueno = dt.Rows[0]["lisuno1"].ToString();
            //string date = Convert.ToDateTime(dt.Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            //string narration = dt.Rows[0]["rmrks"].ToString();

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.ContractorBillDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubConSD", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));

            Rpt1.SetParameters(new ReportParameter("txtSubConName", "Sub Contractor Name: " + this.ddlSubName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));




            Rpt1.SetParameters(new ReportParameter("RptTitle", "Bill Details  -  Sub-Contractor"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBillRAWise()
        {
            DataTable dt = (DataTable)Session["tblconsddetails"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string txtRefno = (dt.Rows[0]["lisurefno"].ToString().Trim().Length > 0) ? "Bill Ref. No: " + dt.Rows[0]["lisurefno"].ToString().Trim() : "";
            string Issueno = dt.Rows[0]["lisuno1"].ToString();
            string date = Convert.ToDateTime(dt.Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            string narration = dt.Rows[0]["rmrks"].ToString();

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.ConRaBill>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptLabIssue", lst, null, null);
            // Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + dt.Rows[0]["pactdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtSubConNam", "Sub Contractor Name: " + this.ddlSubName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("Issueno", "Issue No: " + Issueno));
            Rpt1.SetParameters(new ReportParameter("txtRefno", txtRefno));
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            Rpt1.SetParameters(new ReportParameter("narrationname", "Narration : " + narration));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sub Contractor Bill (R/A Wise)"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", printFooter));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //DataTable dt = (DataTable)Session["tblconsddetails"];

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_09_PImp.rptLabIssue();//.rptPurIssueEntry();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            ////txtSubConNam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text = "Project Name: " + dt.Rows[0]["pactdesc"].ToString();
            //TextObject rptSunConName = rptstk.ReportDefinition.ReportObjects["txtSubConNam"] as TextObject;
            //rptSunConName.Text = "Sub Contractor Name: " + this.ddlSubName.SelectedItem.Text.Substring(13);
            //TextObject rpttxtissueno = rptstk.ReportDefinition.ReportObjects["Issueno"] as TextObject;
            //rpttxtissueno.Text = "Issue No: " + dt.Rows[0]["lisuno1"].ToString();
            //TextObject rpttxtrefno = rptstk.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            //rpttxtrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Trim().Length > 0) ? "Bill Ref. No: " + dt.Rows[0]["lisurefno"].ToString().Trim() : "";

            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + Convert.ToDateTime(dt.Rows[0]["isudat"]).ToString("dd-MMM-yyyy");

            //TextObject narrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //narrationname.Text = dt.Rows[0]["rmrks"].ToString(); ;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }




    }
}