using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using Microsoft.Reporting.WinForms;
using RealERPRPT;

namespace RealERPWEB.F_23_CR
{
    public partial class RptReconcilationLetter : System.Web.UI.Page
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

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));



                this.GetProjectName();

            }
        }
        protected void gvflrwisbill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvflrwisbill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            this.gvflrwisbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvflrwisbill.DataSource = dt;
            this.gvflrwisbill.DataBind();


        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvflrwisbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();



            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETALLSALESPROJECTS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "pactdesc";
            this.ddlprjlist.DataValueField = "pactcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            //this.GetSubContractor();
        }
        protected void lbtnok_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string Desc2 = "asdas";
            //string csircode = "98%";//this.ddlcontractorlist.SelectedValue.ToString() == "000000000000" ? "98%" : this.ddlcontractorlist.SelectedValue.ToString() + "%";
            //string billtcode = "%";//this.ddlcatagory.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlcatagory.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCUSTOMERDETAILS", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }
            Session["RptReconcilationLetter"] = ds1.Tables[0];
            this.Data_Bind();

        }

        protected void ddlprjlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empId = dt.Rows[rowIndex]["Name"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenDedModal();", true);
        }

        private void RptReconsilationLetter()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["RptReconcilationLetter"];//Session["tblflrwisbill"];
            if (dt1 == null || dt1.Rows.Count == 0)
                return;
            //var lst = dt1.DataTableToList<RealEntity.C_09_PIMP.SubConBill.RptPrjFloorWiseBill>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptReconcilationLetter", null, null, null);
            Rpt1.EnableExternalImages = true;

            //string selectedProjectText = ddlprjlist.SelectedItem.Text;
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("selectedProjectText", selectedProjectText));
            //Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            string url = "../RDLCViewer.aspx?PrintOpt=PDF";
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OpenWindow", script, true);

        }

        protected void lbtnPrintMail_Click(object sender, EventArgs e)
        {
            this.RptReconsilationLetter();
        }
    }
}