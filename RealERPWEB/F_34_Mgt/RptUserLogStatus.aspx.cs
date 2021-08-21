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
    public partial class RptUserLogStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyy"); //System.DateTime.Today.AddDays(-1).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                this.GetUserName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "USER LOG DETAILS";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));



            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetUserName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETUSERNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlUserName.DataTextField = "usrsname";
            this.ddlUserName.DataValueField = "usrid";
            this.ddlUserName.DataSource = ds1.Tables[0];
            this.ddlUserName.DataBind();

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetUserName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.UserLogStatus();
        }

        private void UserLogStatus()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = hst["comcod"].ToString();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string usercode = (this.ddlUserName.SelectedValue == "0000000") ? "%" : this.ddlUserName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETUSERLOGINF", usercode, fdate, tdate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvUserLog.DataSource = null;
                this.gvUserLog.DataBind();
                return;
            }
            Session["UserLog"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "USER LOG DETAILS";
                string eventdesc = "Show Report";
                string eventdesc2 = this.ddlUserName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string userid = dt1.Rows[0]["userid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["userid"].ToString() == userid)
                {
                    userid = dt1.Rows[j]["userid"].ToString();
                    dt1.Rows[j]["empname"] = "";


                }

                else
                {
                    userid = dt1.Rows[j]["userid"].ToString();
                }
            }



            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["UserLog"];
            this.gvUserLog.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvUserLog.DataSource = dt;
            this.gvUserLog.DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintLogAll();
        }


        private void PrintLogAll()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            DataTable dt = (DataTable)Session["UserLog"];
            ReportDocument rptLog = new RealERPRPT.R_34_Mgt.rptEntryLogAll();
            TextObject rptDate = rptLog.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From: " + fdate + " To: " + tdate;
            TextObject rptName = rptLog.ReportDefinition.ReportObjects["name"] as TextObject;
            rptName.Text = "Entry User: " + this.ddlUserName.SelectedItem.Text;
            //TextObject rptType = rptLog.ReportDefinition.ReportObjects["type"] as TextObject;
            //rptType.Text = "Type: " + this.ddlType.SelectedItem.Text;
            TextObject txtuserinfo = rptLog.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptLog.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptLog.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptLog;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvUserLog.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvLogType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvUserLog.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}

