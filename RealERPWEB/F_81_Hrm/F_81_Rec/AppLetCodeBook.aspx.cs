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
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class AppLetCodeBook : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "APPIONTMENT LETTER CODE BOOK INFORMATION";

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //this.lblTitle.Text = (this.Request.QueryString["Type"] == "MktTeam" ? "MARKETING TEAM CODE BOOK INFORMATION" : "LETTER CREATION INFORMATION");
                this.ViewSection();

            }



        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "AppLetter":
                    this.ShowSalLetterInfo();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }


        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)Session["LetterInfo"];

            switch (Type)
            {

                case "AppLetter":
                    this.grvAppLetterinfo.DataSource = tbl1;
                    this.grvAppLetterinfo.DataBind();
                    break;

            }

        }
        private void ShowSalLetterInfo()
        {

            string comcod = this.GetComeCode();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "APPLETTERINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvAppLetterinfo.DataSource = null;
                this.grvAppLetterinfo.DataBind();
                return;

            }

            Session["LetterInfo"] = ds1.Tables[0];
            this.Data_Bind();


        }


        protected void grvAppLetterinfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {


                string comcod = this.GetComeCode();
                string letcod1 = ((Label)this.grvAppLetterinfo.Rows[e.RowIndex].FindControl("lbgrletcodesal")).Text.Trim().Replace("-", "");
                string letcode3 = ((TextBox)this.grvAppLetterinfo.Rows[e.RowIndex].FindControl("txtgrletcodesal")).Text.Trim().Replace("-", "");
                string Desc = ((TextBox)this.grvAppLetterinfo.Rows[e.RowIndex].FindControl("txtgvDesclettersal")).Text.Trim();
                string Code = letcod1 + letcode3;
                if (Code.Length != 7)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Code Length Must Be 7 Digit";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    return;
                }

                bool result = this.accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTER", Code, Desc, "", "", "", "", "", "", "", "", "", "", "", "", "");
                this.grvAppLetterinfo.EditIndex = -1;

                if (result)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    this.ShowSalLetterInfo();
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void grvAppLetterinfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvAppLetterinfo.EditIndex = -1;
            this.Data_Bind();
        }
        protected void grvAppLetterinfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvAppLetterinfo.EditIndex = e.NewEditIndex;
            this.Data_Bind();
        }
        protected void grvAppLetterinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvAppLetterinfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}
