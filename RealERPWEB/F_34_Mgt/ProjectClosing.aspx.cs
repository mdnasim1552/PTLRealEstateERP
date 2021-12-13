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
namespace RealERPWEB.F_34_Mgt
{
    public partial class ProjectClosing : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project closing";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.txtCloseDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetClosingProjectInfo();

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


        protected void lnkPrint_Click(object sender, EventArgs e)
        {                 
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "SHOWPROJECTINFO", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }
       private void GetClosingProjectInfo()
        {
            ViewState.Remove("tblProjClosing");
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "SHOWCLOSINGPROJECTINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblProjClosing"] = ds1.Tables[0];
            this.gvProLinkInfo_DataBind();
            ds1.Dispose();
      
        }
        protected void gvProLinkInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblProjClosing"];
            this.gvProLinkInfo.DataSource = tbl1;
            this.gvProLinkInfo.DataBind();

        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }


            string comcod = this.GetCompCode();
            this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblProjClosing"];
            
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string actcode = tbl1.Rows[i]["actcode"].ToString();
                string clsDate = Convert.ToDateTime(tbl1.Rows[i]["clsdate"]).ToString("dd-MMM-yyyy");
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTORUPDATECLOSEPROJ", actcode, clsDate, "", "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Project user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            GetProjectName();
        }

        protected void lbtnSelectPrj_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblProjClosing"];
            string actCode = this.ddlProjectName.SelectedValue.ToString();
            string actdesc = this.ddlProjectName.SelectedItem.Text.Trim();
            string clsdate = this.txtCloseDate.Text;
            DataRow[] dr1 = dt.Select("actcode='" + actCode + "'");
            if (dr1.Length == 0)
            {
                DataRow dr = dt.NewRow();
                dr["actcode"] = actCode;
                dr["actdesc"] = actdesc;
                dr["clsdate"] = clsdate;
                dt.Rows.Add(dr);

            }

            else
            {

                dr1[0]["clsdate"] = clsdate;


            }
            
            ViewState["tblProjClosing"] = dt;
            this.gvProLinkInfo_DataBind();
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblProjClosing"];
            int row = 0;
            foreach(GridViewRow gvr in gvProLinkInfo.Rows)
            {

                string clsDate = ((TextBox)gvr.FindControl("txtgvCloseDate")).Text.ToString();
                dt.Rows[row]["clsdate"] = clsDate ;
                row++;

            }

            ViewState["tblProjClosing"] = dt;

        }

        //protected void lbtngvSaveValue_Click(object sender, EventArgs e)
        //{
        //    this.SaveValue();
        //    this.gvProLinkInfo_DataBind();
        //}

        protected void lbtngvDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblProjClosing"];
            int gvRowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + gvRowIndex;
            string actcode = dt.Rows[rowindex]["actcode"].ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELETEPROJECTCLOSING", actcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblProjClosing");
            ViewState["tblProjClosing"] = dv.ToTable();
            this.gvProLinkInfo_DataBind();
        }
    }
}

