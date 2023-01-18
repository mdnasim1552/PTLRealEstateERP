using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{

    public partial class RptCustCastHRaSelection : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();

                //this.lblHeadtitle.Text = (this.Request.QueryString["Type"].ToString() == "ClLedger") ? "Client Ledger Report" : "CUSTOMER PAYMENT STATUS";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "CUSTOMER PAYMENT STATUS";


            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
        }

        private void GetCustomerName()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string letcode = "";

            for (int i = 0; i < this.gvclientchoice.Rows.Count; i++)
            {
                if (((CheckBox)this.gvclientchoice.Rows[i].FindControl("chklet")).Checked)
                {

                    letcode = ((Label)this.gvclientchoice.Rows[i].FindControl("lblgvletcode")).Text.Trim();
                    break;
                }

            }

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();

            DataSet ds1 = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "GETLETTERDETAIL", letcode.Substring(0, 4), "", "", "", "", "", "", "", "");

            DataSet ds2 = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "GETCUSTOMERNAMEANDADDRESS", pactcode, usircode, "", "", "", "", "", "", "");

            LocalReport Rpt1 = new LocalReport();
            var list = ds1.Tables[0].DataTableToList<RealEntity.C_24_CC.EClassAddwork.HandOverLetter>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.rptCastingLeltter", null, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtDate", Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd,yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtCustomer", ds2.Tables[0].Rows[0]["custname"].ToString() + "\n" + ds2.Tables[0].Rows[0]["proaddress"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtSubject", ds1.Tables[0].Rows[1]["letdesc"].ToString() + "at the apartment " + ds2.Tables[0].Rows[0]["udesc"] + "," + ds2.Tables[0].Rows[0]["pactdesc"].ToString() + "," + ds2.Tables[0].Rows[0]["proaddress"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtBody", ds1.Tables[0].Rows[2]["letdesc"].ToString()));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.lblCustomer.Text = (this.ddlCustName.Items.Count == 0) ? "" : this.ddlCustName.SelectedItem.Text;
                this.ddlProjectName.Visible = false;
                this.ddlCustName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.lblCustomer.Visible = true;
                this.ShowLetter();


                return;
            }

            this.lbtnOk.Text = "Ok";
            //((Label)this.Master.FindControl("lblmsg")).Text = "";

            this.ddlProjectName.Visible = true;
            this.ddlCustName.Visible = true;
            this.ddlCustName.Visible = false;
            this.ddlCustName.Visible = false;
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.gvclientchoice.DataSource = null;
            this.gvclientchoice.DataBind();




        }

        private void ShowLetter()
        {
            //Session.Remove("tblLetter");

            string comcod = this.GetComeCode();

            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "GETLETTER", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvclientchoice.DataSource = null;
                this.gvclientchoice.DataBind();
                return;
            }


            Session["tblLetter"] = ds1.Tables[0];           //ds1.Tables[0];

            this.Data_Bind();
        }

        protected void ddlCustName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvclientchoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Data_Bind()
        {

            try
            {
                DataTable tbl1 = (DataTable)Session["tblLetter"];
                this.gvclientchoice.DataSource = tbl1;
                this.gvclientchoice.DataBind();
            }

            catch (Exception ex)
            {



            }
        }
    }
}
