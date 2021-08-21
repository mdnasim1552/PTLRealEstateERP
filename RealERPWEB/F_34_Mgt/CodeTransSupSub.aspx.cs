using System;
using System.Collections;
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
namespace RealERPWEB.F_34_Mgt
{
    public partial class CodeTransSupSub : System.Web.UI.Page
    {
        ProcessAccess mgtData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "SubCon") ? "SUB-CONTRACTOR CODE TRANSFER"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "Sup") ? "SUPPLIER CODE TRANSFER" : "GENERAL CODE TRANSFER";

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.SelectView();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void SelectView()
        {

            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {
                case "SubCon":
                case "Sup":
                    this.GetFromCode();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "General":
                    this.GetfrmGenCode();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetFromCode()
        {
            string comcod = this.GetComeCode();
            string filter = "%" + this.txtserceacc.Text + "%";
            string ctype = ((this.Request.QueryString["Type"].ToString().Trim() == "SubCon") ? "98" : "99") + "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_CODETRAN", "GETSUPSUBCON", filter, ctype, "", "", "", "", "", "", "");
            this.ddlHeadFrom.DataSource = ds1.Tables[0];
            this.ddlHeadFrom.DataTextField = "sirdesc1";
            this.ddlHeadFrom.DataValueField = "sircode";
            this.ddlHeadFrom.DataBind();
            this.ddlAccHead_SelectedIndexChanged(null, null);
        }

        private void GetSSCodeTo()
        {
            string comcod = this.GetComeCode();
            string filter1 = "%" + this.txtserDetailsCode.Text + "%";
            string ActCode = this.ddlHeadFrom.SelectedValue.ToString();
            DataSet ds3 = mgtData.GetTransInfo(comcod, "SP_ENTRY_CODETRAN", "GETSSCODETO", filter1, ActCode, "", "", "", "", "", "", "");
            this.ddlHeadTo.DataSource = ds3.Tables[0];
            this.ddlHeadTo.DataTextField = "sirdesc1";
            this.ddlHeadTo.DataValueField = "sircode";
            this.ddlHeadTo.DataBind();
        }

        protected void imgbtnFindAccount_Click(object sender, EventArgs e)
        {
            this.GetFromCode();
        }

        protected void ddlAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSSCodeTo();
        }
        protected void imgbtnFindDetailsCode_Click(object sender, EventArgs e)
        {
            this.GetSSCodeTo();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string comcod = this.GetComeCode();
            string CodeFrom = this.ddlHeadFrom.SelectedValue.ToString();
            string CodeTo = this.ddlHeadTo.SelectedValue.ToString();
            bool result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_CODETRAN", "UPDATECODETRAN", CodeFrom, CodeTo, "", "", "",
                                    "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Failed');", true);

                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);


        }
        private void GetfrmGenCode()
        {
            string comcod = this.GetComeCode();
            string filter = "%" + this.txtfrmcodegen.Text + "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_CODETRAN", "GETGENCODE", filter, "", "", "", "", "", "", "", "");
            this.ddlHeadFromgen.DataSource = ds1.Tables[0];
            this.ddlHeadFromgen.DataTextField = "sirdesc1";
            this.ddlHeadFromgen.DataValueField = "sircode";
            this.ddlHeadFromgen.DataBind();
            this.GetToGenCode();


        }


        private void GetToGenCode()
        {
            string comcod = this.GetComeCode();
            string filter1 = "%" + this.txttocodegen.Text + "%";
            string frmcodegen = this.ddlHeadFromgen.SelectedValue.ToString();
            DataSet ds3 = mgtData.GetTransInfo(comcod, "SP_ENTRY_CODETRAN", "GETGENCODETO", filter1, frmcodegen, "", "", "", "", "", "", "");
            this.ddlHeadTogen.DataSource = ds3.Tables[0];
            this.ddlHeadTogen.DataTextField = "sirdesc1";
            this.ddlHeadTogen.DataValueField = "sircode";
            this.ddlHeadTogen.DataBind();


        }

        protected void imgbtnfrmcodegen_Click(object sender, EventArgs e)
        {
            this.GetfrmGenCode();
        }
        protected void imgbtntocodegen_Click(object sender, EventArgs e)
        {
            this.GetToGenCode();
        }
        protected void ddlHeadFromgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetToGenCode();
        }

        protected void lnkFinalUpdateGen_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string comcod = this.GetComeCode();
            string CodeFrom = this.ddlHeadFromgen.SelectedValue.ToString();
            string CodeTo = this.ddlHeadTogen.SelectedValue.ToString();
            bool result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_CODETRAN", "UPDATECODETRAN", CodeFrom, CodeTo, "", "", "",
                                    "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Failed');", true);

                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);
        }


    }
}