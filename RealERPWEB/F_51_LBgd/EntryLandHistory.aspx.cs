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
namespace RealERPWEB.F_51_LBgd
{
    public partial class EntryLandHistory : System.Web.UI.Page
    {
        ProcessAccess LandData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "LAND INFORMATION HISTORY";
                this.GettProjectName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GettProjectName()
        {

            string comcod = this.GetCompCode();
            string projdesc = "%" + this.txtSrchProjectName.Text + "%";
            DataSet ds1 = LandData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETPROJECTNAME", projdesc, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();


        }
        protected void imgSearchProject_Click(object sender, EventArgs e)
        {
            this.GettProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }

        }


        private void LoadGrid()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = LandData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETPROJECTINFO", PactCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvEnLandHist.DataSource = ds1.Tables[0];
            this.gvEnLandHist.DataBind();

            ViewState["tblData"] = ds1.Tables[0];



        }


        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvEnLandHist.DataSource = null;
            this.gvEnLandHist.DataBind();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


        }



        protected void gvEnLandHist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvsircode");
            string mCOMCOD = comcod;
            string mACTCODE = this.ddlProjectName.SelectedValue.ToString();
            ///string CustCode = ((Label)e.Row.FindControl("lblCode")).Text;


            hlink1.Font.Bold = true;
            hlink1.Style.Add("color", "blue");

            hlink1.NavigateUrl = "LinkLandPurInfo.aspx?comcod=" + mCOMCOD + "&patcode=" + mACTCODE + "&sircode=" + code;

        }
    }
}