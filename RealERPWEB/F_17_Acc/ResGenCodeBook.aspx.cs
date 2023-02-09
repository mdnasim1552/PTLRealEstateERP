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
namespace RealERPWEB.F_17_Acc
{
    public partial class ResGenCodeBook : System.Web.UI.Page
    {

        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "GENERAL MATERIAL CODE";
            //this.Master.Page.Title = "GENERAL MATERIAL CODE";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            // this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            if (this.ddlGenCodeBook.Items.Count == 0)
                this.Load_CodeBooList();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
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
        protected void Load_CodeBooList()
        {
            try
            {
                string comcod = this.GetCompCode();

                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTSIRGENCODE", "", "", "", "", "", "", "", "", "");
                this.ddlGenCodeBook.DataTextField = "gencode";
                this.ddlGenCodeBook.DataValueField = "gencode1";
                this.ddlGenCodeBook.DataSource = dsone.Tables[0];
                this.ddlGenCodeBook.DataBind();
                dsone.Dispose();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                string gencode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
                string gencode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");

                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtsirtdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
                string gencode = gencode1 + gencode2;

                if (gencode.Length != 7)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Code Length Must Be 7 Digit";
                    return;
                }
                bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSORUPACCSIRGENINFO", gencode, Desc, txtsirtdesc, "", "", "",
                         "", "", "", "", "", "", "", "", "");
                this.ShowInformation();
                if (result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
                }
                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "General CodeBook";
                    string eventdesc = "Update GenCodeBook";
                    string eventdesc2 = gencode;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
                ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Visible = false;
                double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Items.Clear();
                for (int i = 1; i <= TotalPage; i++)
                    ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                if (TotalPage > 1)
                    ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.grvacc.PageIndex = ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex;
                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();
            }
            catch
            {
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Visible)
                this.lnkok_Click(null, null);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable Dtable = (DataTable)Session["storedata"];
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            ReportDocument rptAccCode = new RealERPRPT.R_17_Acc.RptGenAccCode();
            TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "General CodeBook";
                string eventdesc = "Print GenCodeBook";
                string eventdesc2 = this.ddlGenCodeBook.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            rptAccCode.SetDataSource(Dtable);
            Session["Report1"] = rptAccCode;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    ((Label)this.Master.FindControl("lblmsg")).Text = "";
                    this.LblBookName1.Text = "Code Book:";
                    this.ddlGenCodeBook.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    this.lblGenCode.Visible = true;
                    this.lblGenCode0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lblGenCode.Text = this.ddlGenCodeBook.SelectedItem.ToString().Trim();
                    this.lblGenCode0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    string tempddl1 = (this.ddlGenCodeBook.SelectedValue.ToString()).Substring(0, 2);
                    string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
                    this.ShowInformation();
                }
                else
                {
                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";
                    ((Label)this.Master.FindControl("lblmsg")).Text = "";
                    this.LblBookName1.Text = "Code Book:";
                    this.ibtnSrch.Visible = false;
                    this.lblGenCode.Visible = false;
                    this.lblGenCode0.Visible = false;
                    this.ddlGenCodeBook.Visible = true;
                    this.ddlOthersBookSegment.Visible = true;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();
                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
            }
        }

        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            Session.Remove("storedata");
            string tempddl1 = (this.ddlGenCodeBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            string srchoption = this.txtsrch.Text.Trim() + "%";
            tempddl1 = (tempddl1 == "00") ? "" : tempddl1;
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SIRGENCODEINFO", tempddl1,
                                  tempddl2, srchoption, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }


        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
    }
}