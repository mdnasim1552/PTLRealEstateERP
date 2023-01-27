using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class EmpAssesmentCode : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE ASSESSMENT CODE BOOK INFORMATION";
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
                case "AssessmntCode":
                    this.ShowAssmntInfo();
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
            DataTable tbl1 = (DataTable)Session["AssmntInfo"];

            switch (Type)
            {

                case "AssessmntCode":
                    this.grvAssessmntCodeinfo.DataSource = tbl1;
                    this.grvAssessmntCodeinfo.DataBind();
                    break;

            }

        }
        private void ShowAssmntInfo()
        {

            string comcod = this.GetComeCode();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "ASSESSCODEINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvAssessmntCodeinfo.DataSource = null;
                this.grvAssessmntCodeinfo.DataBind();
                return;

            }

            Session["AssmntInfo"] = ds1.Tables[0];
            this.Data_Bind();


        }
        protected void grvAssessmntCodeinfo_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvAssessmntCodeinfo.EditIndex = -1;
            this.Data_Bind();
        }

        protected void grvAssessmntCodeinfo_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvAssessmntCodeinfo.EditIndex = e.NewEditIndex;
            this.Data_Bind();
        }

        protected void grvAssessmntCodeinfo_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {


                string comcod = this.GetComeCode();
                string asscod1 = ((Label)this.grvAssessmntCodeinfo.Rows[e.RowIndex].FindControl("lblasscode")).Text.Trim().Replace("-", "");
                string asscod3 = ((TextBox)this.grvAssessmntCodeinfo.Rows[e.RowIndex].FindControl("txtasscode")).Text.Trim().Replace("-", "");
                string Desc = ((TextBox)this.grvAssessmntCodeinfo.Rows[e.RowIndex].FindControl("txtassDesc")).Text.Trim();
                string Code = asscod1 + asscod3;
                if (Code.Length != 7)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Code Length Must Be 7 Digit";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    return;
                }

                bool result = this.accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPASSMNTCODE", Code, Desc, "", "", "", "", "", "", "", "", "", "", "", "", "");
                this.grvAssessmntCodeinfo.EditIndex = -1;

                if (result)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    this.ShowAssmntInfo();
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

        protected void grvAssessmntCodeinfo_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvAssessmntCodeinfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}