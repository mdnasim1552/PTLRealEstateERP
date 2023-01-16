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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{


    public partial class RatioCodeBook : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Ratio Code Book";
                //this.Master.Page.Title = "Ratio Code Book";
                this.loadRatioCode();
                CommonButton();
                // this.GetCode();
            }
        }
        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void grvRatioCB_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["tbRatioData"];
                this.grvRatioCB.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvRatioCB.DataSource = tbl1;


                this.grvRatioCB.DataBind();
            }
            catch (Exception ex)
            {
            }

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("tbRatioData");
                    this.lbtnOk.Text = "New";
                    this.LblBookName1.Text = "Code Book:";
                    this.ddlPRatioBook.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl.Text = "Ratio Code Book: " + this.ddlPRatioBook.SelectedItem.ToString().Trim();
                    this.ShowInformation();
                    this.grvRatioCB_DataBind();
                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                }
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.LblBookName1.Text = "Ratio Code Book:";
                this.lbtnOk.Visible = true;
                this.LblBookName1.Visible = true;
                this.lbalterofddl.Visible = false;
                this.ddlPRatioBook.Visible = true;
                this.grvRatioCB.DataSource = null;
                this.grvRatioCB.DataBind();
            }
        }

        private void ShowInformation()
        {
            string comcod = this.GetComCode();
            string tempddl1 = (this.ddlPRatioBook.SelectedValue.ToString()).Substring(0, 2);
            tempddl1 = (tempddl1 == "00" ? "" : tempddl1) + "%";
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SHOWRATIOINFO", tempddl1,
                            "", "", "", "", "", "", "", "");
            Session["tbRatioData"] = ds1.Tables[0];
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.grvRatioCB_DataBind();
        }
        private void loadRatioCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GETRATIO", "", "", "", "", "", "", "", "", "");
            this.ddlPRatioBook.DataTextField = "RATIODESC";
            this.ddlPRatioBook.DataValueField = "RCODE";
            this.ddlPRatioBook.DataSource = ds.Tables[0];
            this.ddlPRatioBook.DataBind();

            // ViewState["ratiodata"] = ds;
        }
        protected void grvRatioCB_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            string comcod = this.GetComCode();
            string gcode1 = ((Label)grvRatioCB.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvRatioCB.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
            string Desc = ((TextBox)grvRatioCB.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gstdrate = Convert.ToDouble("0" + ((TextBox)grvRatioCB.Rows[e.RowIndex].FindControl("txtgvStdrate")).Text.Trim()).ToString();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvRatioCB.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gformula = ((TextBox)this.grvRatioCB.Rows[e.RowIndex].FindControl("txtgvformula")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "INSERTUPRATIOINF", tgcod,
                          gdesc, gformula, gstdrate, "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
            }
            this.grvRatioCB.EditIndex = -1;
            this.ShowInformation();
            this.grvRatioCB_DataBind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Reporting Code Book";
                string eventdesc = "Update CodeBook";
                string eventdesc2 = tgcod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void grvRatioCB_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvRatioCB.EditIndex = e.NewEditIndex;
            this.grvRatioCB_DataBind();
        }
        protected void grvRatioCB_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvRatioCB.EditIndex = -1;
            this.grvRatioCB_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintRatio();
        }

        private void PrintRatio()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            // string hostname = hst["hostname"].ToString();
            string currentDate = DateTime.Now.ToString("dd-MMM-yyyy");

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string shch = "%";
            DataSet ds = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GETRREPORTDATA", shch, "", "", "", "", "", "", "", "");

            //Session["tbRatioData"] = ds1.Tables[0];
            //DataTable dt = (DataTable)Session["tbRatioData"];
            // DataSet ds = (DataSet)ViewState["tbRatioData"];

            var lst = ds.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccRatioAnalysis>();
            LocalReport Rpt1 = null;
            Hashtable reportParm = new Hashtable();
            reportParm["CompanyName"] = comnam.ToUpper();
            reportParm["header"] = "Ratio Analysis";
            reportParm["CurrentDate"] = "As On :" + currentDate;
            reportParm["txtuserinfo"] = "Print Source :" + username + " , " + session + " , " + printdate;


            //reportParm["date"] = "From  " + this.txtEntryDate.Text.ToString() + "   To " + this.txtLDAte.Text.ToString();
            //reportParm["datetex"] = "Batch Description";

            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptRatioAnalysis", lst, reportParm, null);
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}



