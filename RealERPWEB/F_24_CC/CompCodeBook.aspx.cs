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

namespace RealERPWEB.F_24_CC
{
    public partial class CompCodeBook : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Complain Code Book Entry Screen ";

            }
            if (this.ddlComp.Items.Count == 0)
                this.Load_CodeBooList();
            //((Label)this.Master.FindControl("lblmsg")).Text = "";
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

            string comcod = this.GetCompCode();
            DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETCOMPCODEBOOK", "",
                            "", "", "", "", "", "", "", "");
            this.ddlComp.DataTextField = "gdesc";
            this.ddlComp.DataValueField = "gcod";
            this.ddlComp.DataSource = dsone.Tables[0];
            this.ddlComp.DataBind();


        }
        protected void Data_Bind()
        {


            DataTable tbl1 = (DataTable)Session["storedata"];
            //this.gvSalPlnCode.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvCompCode.DataSource = tbl1;
            this.gvCompCode.DataBind();



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
                    //this.lblPage.Visible = true;
                    //this.ddlpagesize.Visible = true;
                    this.ddlComp.Visible = false;
                    this.ddlOtherComp.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.lbalterofddl.Text = "Code Book: " + this.ddlComp.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = this.ddlOtherComp.SelectedItem.ToString().Trim();
                    // this.ibtnSrch.Visible = true;

                    this.gvCompCode.EditIndex = -1;
                    this.ShowInformation();


                }
                catch (Exception ex)
                {
                    // ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                }
            }
            else
            {
                this.lnkok.Text = "Ok";
                this.lnkok.Visible = true;
                this.lbalterofddl.Visible = false;
                this.lbalterofddl0.Visible = false;
                this.ddlComp.Visible = true;
                this.ddlOtherComp.Visible = true;
                //this.ibtnSrch.Visible = false;
                //this.lblPage.Visible = true;
                //this.ddlpagesize.Visible = true;
                this.gvCompCode.DataSource = null;
                this.gvCompCode.DataBind();
            }
        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();

        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();

            string tempddl1 = (this.ddlComp.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOtherComp.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETCOMPDETAILINFO", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
            this.Data_Bind();
        }
        protected void gvSalPlnCode_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvCompCode.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvSalPlnCode_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvCompCode.EditIndex = e.NewEditIndex;
            this.Data_Bind();


        }
        protected void gvSalPlnCode_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)gvCompCode.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)gvCompCode.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();

            string Desc = ((TextBox)gvCompCode.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.gvCompCode.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.gvCompCode.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPDATECOMP", tgcod,
                           gdesc, Gtype, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
            this.gvCompCode.EditIndex = -1;
            this.ShowInformation();
            this.Data_Bind();

        }


        protected void gvCompCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCompCode.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }



    }
}