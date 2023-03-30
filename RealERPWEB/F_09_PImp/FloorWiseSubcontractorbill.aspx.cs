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

namespace RealERPWEB.F_09_PImp
{
    public partial class FloorWiseSubcontractorbill : System.Web.UI.Page
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

              

                this.GetProjectName();
                this.GetCataGory();


            }

            }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProjectName()
        {
           
            string comcod = this.GetCompCode();

            
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETProjectsub", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "pactdesc";
            this.ddlprjlist.DataValueField = "pactcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            this.GetSubContractor();
        }

        private void GetSubContractor()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();

           
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURSUBNAME01", pactcode, "%", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlcontractorlist.DataTextField = "csirdesc";
            this.ddlcontractorlist.DataValueField = "csircode";
            this.ddlcontractorlist.DataSource = ds1.Tables[0];
            this.ddlcontractorlist.DataBind();
       


        }
       
        private void GetCataGory()
        {
            string comcod = this.GetCompCode();
           

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCATAGORY", "", "","" ,"", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlcatagory.DataValueField = "rsircode";
            this.ddlcatagory.DataTextField = "rsirdesc";
            this.ddlcatagory.DataSource = ds1.Tables[0];
            this.ddlcatagory.DataBind();
           
        }

        protected void ddlprjlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSubContractor();
        }

        protected void lbtnok_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string csircode=  this.ddlcontractorlist.SelectedValue.ToString() == "000000000000" ? "98%" : this.ddlcontractorlist.SelectedValue.ToString() + "%";
            string billtcode=  this.ddlcatagory.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlcatagory.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCONSTATEMENT", pactcode, csircode, billtcode, "", "", "", "", "", "");

            if (ds1 == null)
            {
           
                return;
            }
            Session["tblflrwisbill"] = ds1.Tables[0];
            this.Data_Bind();
       
        }

        protected void gvflrwisbill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvflrwisbill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblflrwisbill"];
            this.gvflrwisbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvflrwisbill.DataSource = dt;
            this.gvflrwisbill.DataBind();
            

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvflrwisbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
    }
}