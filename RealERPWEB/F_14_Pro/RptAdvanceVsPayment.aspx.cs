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
namespace RealERPWEB.F_14_Pro
{


    public partial class RptAdvanceVsPayment : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Advanced Vs Payment ";


                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetSupplierName();
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
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
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

        private void GetSupplierName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcSupplier.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPURSUPPLIERNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSupplierName.DataTextField = "sirdesc";
            this.ddlSupplierName.DataValueField = "sircode";
            this.ddlSupplierName.DataSource = ds1.Tables[0];
            this.ddlSupplierName.DataBind();

        }



        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            this.GetSupplierName();
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }


        private void ShowData()
        {
            Session.Remove("tbladvanpay");
            string comcod = this.GetCompCode();

            string PactCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string SupplierCode = (this.ddlSupplierName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSupplierName.SelectedValue.ToString() + "%";
            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string txtrefno = "%" + this.txtSrcRefno.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SHOWADVANCEDVSPAYMENT", PactCode, SupplierCode, frmdate, todate, txtrefno, "", "", "", "");
            if (ds1 == null)
                return;
            Session["tbladvanpay"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string orderno = dt1.Rows[0]["orderno"].ToString();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            //string orderdat =dt1.Rows[0]["orderdat"].ToString();
            //string ssircode = dt1.Rows[0]["ssircode"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["orderno"].ToString() == orderno && dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    dt1.Rows[j]["orderamt"] = 0.00;
                    dt1.Rows[j]["advamt"] = 0.00;
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["orderdat"] = "";

                    pactcode = dt1.Rows[j]["pactcode"].ToString();



                }


                else
                {

                    if (dt1.Rows[j]["orderno"].ToString() == orderno)
                    {
                        dt1.Rows[j]["orderamt"] = 0.00;
                        dt1.Rows[j]["advamt"] = 0.00;
                        dt1.Rows[j]["orderdat"] = "";

                    }

                    //if (dt1.Rows[j]["orderdat"].ToString() == orderdat)
                    //{
                    //    dt1.Rows[j]["orderdat"] = "";
                    //}

                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";
                    }

                    orderno = dt1.Rows[j]["orderno"].ToString();
                    pactcode = dt1.Rows[j]["pactcode"].ToString();

                }
            }

            return dt1;
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbladvanpay"];
            this.gvSubBill.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFOrderAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(orderamt)", "")) ? 0.00 :
                    dt.Compute("sum(orderamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFOAdvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(advamt)", "")) ? 0.00 :
                    dt.Compute("sum(advamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFVouAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(vouamt)", "")) ? 0.00 :
                    dt.Compute("sum(vouamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }
        protected void ibtnFindSubConName0_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindRefno_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void gvSubBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSubBill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tbladvanpay"];



            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.RptAdvVsPayment>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptAdvancedVsPayment", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Advanced Vs Payment"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "( From " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )"));
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

            //ReportDocument rptConSD = new RealERPRPT.R_14_Pro.RptAdvancedVsPayment();
            //TextObject rptCname = rptConSD.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptConSD.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "( From " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptConSD.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptConSD.SetDataSource((DataTable)Session["tbladvanpay"]);
            //Session["Report1"] = rptConSD;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            // ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}