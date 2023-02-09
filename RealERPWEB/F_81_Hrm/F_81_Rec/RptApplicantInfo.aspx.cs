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
using System.IO;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class RptApplicantInfo : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetCompany();

                //this.GetInformation();
            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetCompany()
        {
            string comcod = this.GetComeCode();
            string txtSComp = this.txtSComp.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETCOMPANYNAME", txtSComp, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "degname";
            this.ddlCompany.DataValueField = "degid";
            this.ddlCompany.DataSource = ds3.Tables[0];
            this.ddlCompany.DataBind();
            this.GetOffice();
        }
        private void GetOffice()
        {
            string comcod = this.GetComeCode();
            string txtSOffice = this.txtSOffice.Text + "%";
            string txtcompany = this.ddlCompany.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETOFFICENAME", txtSOffice, txtcompany, "", "", "", "", "", "", "");
            this.ddlOffice.DataTextField = "degname";
            this.ddlOffice.DataValueField = "degid";
            this.ddlOffice.DataSource = ds3.Tables[0];
            this.ddlOffice.DataBind();
            this.GetDesignation();
        }

        private void GetDesignation()
        {
            string comcod = this.GetComeCode();
            string txtDSearch = this.txtDesSearch.Text + "%";
            string txtoffice = this.ddlOffice.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETEMPDESIGNATION", txtDSearch, txtoffice, "", "", "", "", "", "", "");
            this.ddlDesig.DataTextField = "degname";
            this.ddlDesig.DataValueField = "degid";
            this.ddlDesig.DataSource = ds3.Tables[0];
            this.ddlDesig.DataBind();
            //this.GetEmployeeName();
        }


        protected void ibtnEmpList_Click(object sender, ImageClickEventArgs e)
        {
            //this.GetEmployeeName();
        }
        protected void ibtnInformation_Click(object sender, ImageClickEventArgs e)
        {
            //this.GetInformation();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string company = this.ddlCompany.SelectedItem.Text.ToString();
            string office = this.ddlOffice.SelectedItem.Text.ToString();
            string post = this.ddlDesig.SelectedItem.Text.ToString();

            ReportDocument rptstk = new RealERPRPT.R_81_Hrm.R_81_Rec.rptApplicantInformation();//R_15_Acc.rptAccBudVsExpen();

            DataTable dt = (DataTable)Session["tblAppinfo"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtComOffinfo"] as TextObject;
            rpttxtcompanyname.Text = "Applicant for the company : " + company + ";    " + office;
            TextObject rpttxtpost = rptstk.ReportDefinition.ReportObjects["txtPost"] as TextObject;
            rpttxtpost.Text = "Post : " + post;
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource((DataTable)Session["tblAppinfo"]);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lblDesig.Visible = true;
                this.ddlDesig.Visible = false;
                this.ddlCompany.Visible = false;
                this.lblCompany.Visible = true;
                this.ddlOffice.Visible = false;
                this.lblOffice.Visible = true;
                this.lbtnOk.Text = "New";
                this.lblCompany.Text = this.ddlCompany.SelectedItem.Text;
                this.lblOffice.Text = this.ddlOffice.SelectedItem.Text;
                this.lblDesig.Text = this.ddlDesig.SelectedItem.Text;
                this.SelectView();
                return;
            }

            this.lblDesig.Visible = false;
            this.ddlDesig.Visible = true;

            this.ddlCompany.Visible = true;
            this.lblCompany.Visible = false;
            this.ddlOffice.Visible = true;
            this.lblOffice.Visible = false;

            this.lbtnOk.Text = "Ok";
            this.MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }

        private void SelectView()
        {

            this.MultiView1.ActiveViewIndex = 0;
            this.ShowInformation();
            this.Data_Bind();
        }
        private void ShowInformation()
        {
            Session.Remove("tblAppinfo");
            string comcod = this.GetComeCode();
            string txtDesig = this.ddlDesig.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "SHOWAPPINFO", txtDesig, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblAppinfo"] = ds2.Tables[0];
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblAppinfo"];
            if (dt.Rows.Count == 0)
            {
                this.gvappinfo.DataSource = null;
                this.gvappinfo.DataBind();
                return;

            }
            this.gvappinfo.DataSource = dt;
            this.gvappinfo.DataBind();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetOffice();
        }
        protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDesignation();
        }
        protected void ddlDesig_SelectedIndexChanged(object sender, EventArgs e)
        {

            // this.GetEmployeeName();
        }
    }
}