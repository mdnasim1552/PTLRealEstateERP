using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_14_Pro
{
    public partial class PurSuplinkWithMat : System.Web.UI.Page
    {


        ProcessAccess PurData = new ProcessAccess();

        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Link With Materials (Work) ";
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





        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)Session["tblsup"];
            this.gvSupLink.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSupLink.DataSource = tbl1;
            this.gvSupLink.DataBind();


        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }



        private void ShowInformation()
        {
            Session.Remove("tblsup");
            string comcod = this.GetCompCode();
            string SrchSupDesc = "%" + this.txtsrch.Text.Trim() + "%";
            DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SHOWSUPLINKWMATERIALS", SrchSupDesc, "", "", "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvSupLink.DataSource = null;
                this.gvSupLink.DataBind();
                return;
            }
            Session["tblsup"] = ds2.Tables[0];
            this.Data_Bind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }

        protected void ibtnSrchregis_Click(object sender, ImageClickEventArgs e)
        {


            string comcod = this.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            DropDownList ddl2 = (DropDownList)this.gvSupLink.Rows[rowindex].FindControl("ddlmwgdesc");
            string mwgcod01 = "01";
            string SearchProject = "%" + ((TextBox)gvSupLink.Rows[rowindex].FindControl("txtSerachmwgdesc")).Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETMATFORWRORK", mwgcod01, SearchProject, "", "", "", "", "", "", "");
            ddl2.DataTextField = "mwgdesc";
            ddl2.DataValueField = "mwgcod";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
        }

        protected void ibtnSrchmwgm_Click(object sender, ImageClickEventArgs e)
        {

            string comcod = this.GetCompCode();

            int rowindex = (int)ViewState["gindex"];
            DropDownList ddlmat = (DropDownList)this.gvSupLink.Rows[rowindex].FindControl("ddlmwgmdesc");
            string mwgcod02 = "02";
            string Searmdesc = "%" + ((TextBox)gvSupLink.Rows[rowindex].FindControl("txtSerachmwgmdesc")).Text.Trim() + "%";
            DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETMATFORWRORK", mwgcod02, Searmdesc, "", "", "", "", "", "", "");
            ddlmat.DataTextField = "mwgdesc";
            ddlmat.DataValueField = "mwgcod";
            ddlmat.DataSource = ds2.Tables[0];
            ddlmat.DataBind();
            ds2.Dispose();
        }


        protected void gvSupLink_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSupLink.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvSupLink_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvSupLink.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvSupLink_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvSupLink.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            string mwgcod = ((Label)this.gvSupLink.Rows[e.NewEditIndex].FindControl("lgvmwgcode")).Text.Trim();
            string mgcod = ((Label)this.gvSupLink.Rows[e.NewEditIndex].FindControl("lgvmwgmcode")).Text.Trim();
            int rowindex = (gvSupLink.PageSize) * (this.gvSupLink.PageIndex) + e.NewEditIndex;
            ViewState["gindex"] = e.NewEditIndex;

            //Work
            DropDownList ddl2 = (DropDownList)this.gvSupLink.Rows[e.NewEditIndex].FindControl("ddlmwgdesc");
            string comcod = this.GetCompCode();
            string mwgcod01 = "01";
            string SearchProject = "%" + ((TextBox)gvSupLink.Rows[e.NewEditIndex].FindControl("txtSerachmwgdesc")).Text.Trim() + "%";

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETMATFORWRORK", mwgcod01, SearchProject, "", "", "", "", "", "", "");
            ddl2.DataTextField = "mwgdesc";
            ddl2.DataValueField = "mwgcod";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = mwgcod;

            // Material

            DropDownList ddlmat = (DropDownList)this.gvSupLink.Rows[e.NewEditIndex].FindControl("ddlmwgmdesc");
            string mwgcod02 = "02";
            string Searmdesc = "%" + ((TextBox)gvSupLink.Rows[e.NewEditIndex].FindControl("txtSerachmwgmdesc")).Text.Trim() + "%";
            DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETMATFORWRORK", mwgcod02, Searmdesc, "", "", "", "", "", "", "");
            ddlmat.DataTextField = "mwgdesc";
            ddlmat.DataValueField = "mwgcod";
            ddlmat.DataSource = ds2.Tables[0];
            ddlmat.DataBind();
            ddlmat.SelectedValue = mgcod;
            ds1.Dispose();
            ds2.Dispose();





        }
        protected void gvSupLink_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblsup"];
            int rowindex = (int)ViewState["gindex"];
            string Ssircode = ((DataTable)Session["tblsup"]).Rows[rowindex]["ssircode"].ToString();

            string mwgcode = ((DropDownList)this.gvSupLink.Rows[rowindex].FindControl("ddlmwgdesc")).SelectedValue.ToString();
            string mgcode = ((DropDownList)this.gvSupLink.Rows[rowindex].FindControl("ddlmwgmdesc")).SelectedValue.ToString();


            DataRow[] dr2 = dt.Select("ssircode = '" + Ssircode + "'");
            if (dr2.Length > 0)
            {
                dr2[0]["mwgcod"] = ((DropDownList)this.gvSupLink.Rows[rowindex].FindControl("ddlmwgdesc")).SelectedValue.ToString();
                dr2[0]["mwgdesc"] = ((DropDownList)this.gvSupLink.Rows[rowindex].FindControl("ddlmwgdesc")).SelectedItem.Text;

                dr2[0]["mgcod"] = ((DropDownList)this.gvSupLink.Rows[rowindex].FindControl("ddlmwgmdesc")).SelectedValue.ToString();
                dr2[0]["mgdesc"] = ((DropDownList)this.gvSupLink.Rows[rowindex].FindControl("ddlmwgmdesc")).SelectedItem.Text;
            }

            bool resulta = false;
            mwgcode = (mwgcode == "00000") ? "" : mwgcode;
            mgcode = (mgcode == "00000") ? "" : mgcode;
            resulta = PurData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSOUPSUPLWMWORK", Ssircode, mwgcode, mgcode, "", "",
                                    "", "", "", "", "", "", "", "", "", "");


            if (!resulta)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = PurData.ErrorObject["Msg"].ToString();
                return;
            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
            this.gvSupLink.EditIndex = -1;
            Session["tblsup"] = dt;
            this.Data_Bind();

        }



    }
}
