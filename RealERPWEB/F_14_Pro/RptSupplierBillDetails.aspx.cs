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
using Microsoft.Reporting.WinForms;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_14_Pro
{
    public partial class RptSupplierBillDetails : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Bill Details";
                DateTime curdate = System.DateTime.Today;
                string frmdate = "01-" + curdate.ToString("MMM-yyyy"); 
                string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.txtDate.Text = frmdate;
                this.txttoDate.Text = todate;
                this.GetSupplierName();
                this.GetProjectName();

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


        }

        private void GetSupplierName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETSUPPLIERNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "sirdesc";
            this.ddlSubName.DataValueField = "sircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();
            this.GetProjectName();



        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowBillDetails();


        }
        private void ShowBillDetails()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string PactCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string SupplierName = this.ddlSubName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            string asondate = this.chkasondate.Checked ? "asondate" : "";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTSUPPLIERBILLDETAILS", PactCode, SupplierName, frmdate, todate, asondate, "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblSubbill"] = HiddenSameData(ds1.Tables[0]);
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

            DataTable dt = (DataTable)Session["tblSubbill"];
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();
            this.FooterCalculation(dt);
        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


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
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFsPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(spayment)", "")) ? 0.00 :
                       dt.Compute("sum(spayment)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 :
                dt.Compute("sum(netpayable)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblSubbill"];
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


            // string txtRefno = (dt.Rows[0]["lisurefno"].ToString().Trim().Length > 0) ? "Bill Ref. No: " + dt.Rows[0]["lisurefno"].ToString().Trim() : "";
            //string Issueno = dt.Rows[0]["lisuno1"].ToString();
            //string date = Convert.ToDateTime(dt.Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            //string narration = dt.Rows[0]["rmrks"].ToString();

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.RptSubBillDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSubBillDetails", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));

            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + this.ddlProjectName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("SupplierName", this.ddlSubName.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date : " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Bill Detail -Supplier"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptSupp = new RealERPRPT.R_14_Pro.RptSubBillDetails();
            //TextObject rptCname = rptSupp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            ////TextObject rptpactdesc = rptConSD.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            ////rptpactdesc.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text.Substring(13);
            //TextObject rptSubdesc = rptSupp.ReportDefinition.ReportObjects["SupplierName"] as TextObject;
            //rptSubdesc.Text = this.ddlSubName.SelectedItem.Text.Substring(14); ;
            //TextObject rptDate = rptSupp.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "Date : " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptSupp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptSupp.SetDataSource((DataTable)Session["tblSubbill"]);


            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptConSD.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptSupp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetSupplierName();
        }

        protected void ddlSubName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
    }
}