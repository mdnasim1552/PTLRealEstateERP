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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class RptYearlySalCollPur : System.Web.UI.Page
    {
        ProcessAccess GrpData = new ProcessAccess();
        public static double OpenBal, Clsbal;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompanyList();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Target & Acheivement";
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void GetCompanyList()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = this.GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", "", "", "", "", "", "", "", "", "");
            this.cblCompany.DataTextField = "comsnam";
            this.cblCompany.DataValueField = "comcod";
            this.cblCompany.DataSource = ds1.Tables[0];
            this.cblCompany.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("tblybgd");
                string comcod = "";
                for (int i = 0; i < this.cblCompany.Items.Count; i++)
                    comcod += (this.cblCompany.Items[i].Selected ? this.cblCompany.Items[i].Value.ToString() : "");
                string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_GRPACC_YEARLYBGD", "RPTYSALCOLPURINFO", date, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvybgd.DataSource = null;
                    this.gvybgd.DataBind();
                    return;
                }
                Session["tblybgd"] = this.HiddenSameData(ds1.Tables[0]);
                this.Data_Bind();

            }

            catch (Exception ex)
            {


            }



        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comcod = dt1.Rows[0]["comcod"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comcod"].ToString() == comcod)
                    dt1.Rows[j]["companyname"] = "";
                comcod = dt1.Rows[j]["comcod"].ToString();
            }


            return dt1;

        }



        private void Data_Bind()
        {

            this.gvybgd.DataSource = (DataTable)Session["tblybgd"];
            this.gvybgd.DataBind();

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblybgd"];
            ReportDocument rptsale = new RealERPRPT.R_45_GrAcc.rptYSCollAndPurBgd();
            TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtmonthyear = rptsale.ReportDefinition.ReportObjects["txtmonthyear"] as TextObject;
            txtmonthyear.Text = Convert.ToDateTime(this.txtdate.Text).ToString("MMMM-yyyy");
            TextObject txtdate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = Convert.ToDateTime(this.txtdate.Text).ToString("dddd, MMM dd,yyyy");

            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsale.SetDataSource(dt);
            Session["Report1"] = rptsale;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {

            if (chkall.Checked)
            {
                for (int i = 0; i < this.cblCompany.Items.Count; i++)
                {
                    cblCompany.Items[i].Selected = true;

                }


            }

            else
            {
                for (int i = 0; i < this.cblCompany.Items.Count; i++)
                {
                    cblCompany.Items[i].Selected = false;

                }

            }
        }

        protected void gvybgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CompanyDepartment = (Label)e.Row.FindControl("lblgvCompanyDepartment");
                Label lgvysalamt = (Label)e.Row.FindControl("lgvysalamt");
                Label lgvyasalamt = (Label)e.Row.FindControl("lgvyasalamt");
                Label lgvmsalamt = (Label)e.Row.FindControl("lgvmsalamt");
                Label lgvmasalamt = (Label)e.Row.FindControl("lgvmasalamt");
                Label lgvtsalamt = (Label)e.Row.FindControl("lgvtsalamt");

                Label lgvycollamt = (Label)e.Row.FindControl("lgvycollamt");
                Label lgvyacollamt = (Label)e.Row.FindControl("lgvyacollamt");
                Label lgvmcollamt = (Label)e.Row.FindControl("lgvmcollamt");
                Label lgvmacollamt = (Label)e.Row.FindControl("lgvmacollamt");
                Label lgvmdcollamt = (Label)e.Row.FindControl("lgvmdcollamt");
                Label lgvtcollamt = (Label)e.Row.FindControl("lgvtcollamt");

                Label lgvypuramt = (Label)e.Row.FindControl("lgvypuramt");
                Label lgvyapuramt = (Label)e.Row.FindControl("lgvyapuramt");
                Label lgvmpuramt = (Label)e.Row.FindControl("lgvmpuramt");
                Label lgvmapuramt = (Label)e.Row.FindControl("lgvmapuramt");
                Label lgvmdpuramt = (Label)e.Row.FindControl("lgvmdpuramt");
                Label lgvtpuramt = (Label)e.Row.FindControl("lgvtpuramt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {
                    CompanyDepartment.Font.Bold = true;
                    lgvysalamt.Font.Bold = true;
                    lgvyasalamt.Font.Bold = true;
                    lgvmsalamt.Font.Bold = true;
                    lgvmasalamt.Font.Bold = true;
                    lgvtsalamt.Font.Bold = true;
                    lgvycollamt.Font.Bold = true;
                    lgvyacollamt.Font.Bold = true;
                    lgvmcollamt.Font.Bold = true;
                    lgvmacollamt.Font.Bold = true;
                    lgvmdcollamt.Font.Bold = true;
                    lgvtcollamt.Font.Bold = true;
                    lgvypuramt.Font.Bold = true;
                    lgvyapuramt.Font.Bold = true;
                    lgvmpuramt.Font.Bold = true;
                    lgvmapuramt.Font.Bold = true;
                    lgvmdpuramt.Font.Bold = true;
                    lgvtpuramt.Font.Bold = true;
                    CompanyDepartment.Style.Add("text-align", "right");


                }

            }
        }
    }
}