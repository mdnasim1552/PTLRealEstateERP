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
namespace RealERPWEB.F_04_Bgd
{
    public partial class BgdAnalinkWithWork : System.Web.UI.Page
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
            //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Link With Materials (Work) INPUT/EDIT SCREEN";
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
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
            this.gvAnaLink.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvAnaLink.DataSource = tbl1;
            this.gvAnaLink.DataBind();


        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }



        private void ShowInformation()
        {
            Session.Remove("tblsup");
            string comcod = this.GetCompCode();
            string SrchWork = "%" + this.txtsrch.Text.Trim() + "%";
            DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SHOWANALINKWWORK", SrchWork, "", "", "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvAnaLink.DataSource = null;
                this.gvAnaLink.DataBind();
                return;
            }
            Session["tblsup"] = ds2.Tables[0];
            this.Data_Bind();

        }


        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }

        protected void ibtnSrchregis_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            DropDownList ddl2 = (DropDownList)this.gvAnaLink.Rows[rowindex].FindControl("ddlwrkdesc");
            string SearchProject = "%" + ((TextBox)gvAnaLink.Rows[rowindex].FindControl("txtSerachwrkdesc")).Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETANAWRORK", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "wrkdesc";
            ddl2.DataValueField = "wrkcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
        }
        protected void gvAnaLink_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAnaLink.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvAnaLink_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvAnaLink.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvAnaLink_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvAnaLink.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            string mwgcod = ((Label)this.gvAnaLink.Rows[e.NewEditIndex].FindControl("lgvwrkcode")).Text.Trim();
            int rowindex = (gvAnaLink.PageSize) * (this.gvAnaLink.PageIndex) + e.NewEditIndex;
            DropDownList ddl2 = (DropDownList)this.gvAnaLink.Rows[e.NewEditIndex].FindControl("ddlwrkdesc");
            ViewState["gindex"] = e.NewEditIndex;
            string comcod = this.GetCompCode();
            string SearchProject = "%" + ((TextBox)gvAnaLink.Rows[e.NewEditIndex].FindControl("txtSerachwrkdesc")).Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETANAWRORK", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "wrkdesc";
            ddl2.DataValueField = "wrkcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = mwgcod;
        }
        protected void gvAnaLink_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblsup"];
            int rowindex = (int)ViewState["gindex"];
            string sircode = ((DataTable)Session["tblsup"]).Rows[rowindex]["sircode"].ToString();

            string wrkcode = ((DropDownList)this.gvAnaLink.Rows[rowindex].FindControl("ddlwrkdesc")).SelectedValue.ToString();
            if (wrkcode == "000000000")
            {
                this.gvAnaLink.EditIndex = -1;
                this.Data_Bind();
                return;
            }

            DataRow[] dr2 = dt.Select("sircode = '" + sircode + "'");
            if (dr2.Length > 0)
            {
                dr2[0]["wrkcode"] = ((DropDownList)this.gvAnaLink.Rows[rowindex].FindControl("ddlwrkdesc")).SelectedValue.ToString();
                dr2[0]["wrkdesc"] = ((DropDownList)this.gvAnaLink.Rows[rowindex].FindControl("ddlwrkdesc")).SelectedItem.Text;
            }

            bool resulta = false;
            resulta = PurData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSOUPSUPLWMWORK", sircode, wrkcode, "", "", "",
                                    "", "", "", "", "", "", "", "", "", "");


            if (!resulta)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = PurData.ErrorObject["Msg"].ToString();
                return;
            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
            this.gvAnaLink.EditIndex = -1;
            Session["tblsup"] = dt;
            this.Data_Bind();

        }


    }
}
