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
namespace RealERPWEB.F_22_Sal
{
    public partial class SalesPlaningCodeBook : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sales Planing Cood Book";

            }
            if (this.ddlSalPayment.Items.Count == 0)
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
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTSALEPLNCODE", "",
                                "", "", "", "", "", "", "", "");
                this.ddlSalPayment.DataTextField = "gdesc";
                this.ddlSalPayment.DataValueField = "gcod";
                this.ddlSalPayment.DataSource = dsone.Tables[0];
                this.ddlSalPayment.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        protected void Data_Bind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                this.gvSalPlnCode.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvSalPlnCode.DataSource = tbl1;
                this.gvSalPlnCode.DataBind();

            }
            catch (Exception ex)
            {
            }

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Data_Bind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //DataSet ds1 = Rprss.DataCodeBooks("SP_REPORT_CODEBOOK", comcod, "RPTOTHERACCOUNTCODEBook", "", tempddl2);
            //ReportDocument rptAccCode = new RealERPRPT.R_17_Acc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtadress = rptAccCode.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = "OTHER ACCOUNTS  CODE BOOK  REPORT";
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptAccCode.SetDataSource(ds1.Tables[0]);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Sales Payment Code Book";
            //    string eventdesc = "Print CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //Session["Report1"] = rptAccCode;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                this.lnkok.Text = "New";
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ddlSalPayment.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.lbalterofddl.Text = "Code Book: " + this.ddlSalPayment.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = this.ddlOthersBookSegment.SelectedItem.ToString().Trim();
                    this.ibtnSrch.Visible = true;

                    this.gvSalPlnCode.EditIndex = -1;
                    this.ShowInformation();


                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                }
            }
            else
            {
                this.lnkok.Text = "Ok";
                this.lnkok.Visible = true;
                this.lbalterofddl.Visible = false;
                this.lbalterofddl0.Visible = false;
                this.ddlSalPayment.Visible = true;
                this.ddlOthersBookSegment.Visible = true;
                this.ibtnSrch.Visible = false;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.gvSalPlnCode.DataSource = null;
                this.gvSalPlnCode.DataBind();
            }
        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();

        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlSalPayment.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            string SchEmp = "%" + this.txtsrch.Text + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETPLNINGUNFO", tempddl1,
                            tempddl2, SchEmp, "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
            this.Data_Bind();
        }
        protected void gvSalPlnCode_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvSalPlnCode.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvSalPlnCode_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string comcod = this.GetCompCode();
            this.gvSalPlnCode.EditIndex = e.NewEditIndex;
            this.Data_Bind();

            string gcode = ((TextBox)gvSalPlnCode.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            int rowindex = (this.gvSalPlnCode.PageSize) * (this.gvSalPlnCode.PageIndex) + e.NewEditIndex;

            string empcode = ((DataTable)Session["storedata"]).Rows[rowindex]["empid"].ToString();


            DropDownList ddl2 = (DropDownList)this.gvSalPlnCode.Rows[e.NewEditIndex].FindControl("ddlUserName");
            Panel pnl02 = (Panel)this.gvSalPlnCode.Rows[e.NewEditIndex].FindControl("Panel2");

            if ((ASTUtility.Right(gcode, 3) != "000"))
            {
                ViewState["gindex"] = e.NewEditIndex;
                string SearchProject = "%" + ((TextBox)gvSalPlnCode.Rows[e.NewEditIndex].FindControl("txtSearchUserName")).Text.Trim() + "%";
                DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "empname";
                ddl2.DataValueField = "empid";
                ddl2.DataSource = ds1;
                ddl2.DataBind();
                ddl2.SelectedValue = empcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnl02.Visible = true;

            }
            else
            {
                pnl02.Visible = true;
                ddl2.Items.Clear();
            }
        }
        protected void gvSalPlnCode_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.ConfirmMessage.Visible = true;
            if (((TextBox)gvSalPlnCode.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Length == 11)
            {

                string comcod = this.GetCompCode();
                string gcode1 = ((Label)gvSalPlnCode.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
                string gcode2 = ((TextBox)gvSalPlnCode.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");

                string Desc = ((TextBox)gvSalPlnCode.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string tgcod = gcode1.Substring(0, 2) + gcode2;
                string gdesc = ((TextBox)this.gvSalPlnCode.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string gtype = ((TextBox)this.gvSalPlnCode.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
                string Gtype = (gtype.ToString() == "") ? "T" : gtype;
                string empcode = ((DropDownList)this.gvSalPlnCode.Rows[e.RowIndex].FindControl("ddlUserName")).Text.Trim();
                bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPSALPLNINF", tgcod,
                               gdesc, Gtype, empcode, "", "", "", "", "", "", "", "", "", "", "");

                if (result == true)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                }

                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                }

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales Payment Code Book";
                    string eventdesc = "Update CodeBook";
                    string eventdesc2 = tgcod;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sales Payment Schedule Code Must be 11 Degits!";
            }
            this.gvSalPlnCode.EditIndex = -1;
            this.ShowInformation();


        }
        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvSalPlnCode.Rows[rowindex].FindControl("ddlUserName");
            string SearchProject = "%" + ((TextBox)gvSalPlnCode.Rows[rowindex].FindControl("txtSearchUserName")).Text.Trim() + "%";
            DataSet ds1 = da.GetTransInfo("", "SP_ENTRY_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
        }
        protected void gvSalPlnCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalPlnCode.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}
