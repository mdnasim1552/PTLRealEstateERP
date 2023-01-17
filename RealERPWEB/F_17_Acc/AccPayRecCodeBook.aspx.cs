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
    public partial class AccPayRecCodeBook : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess da = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "PAY TO/RECEIVED BY CODE";
                this.Load_CodeBooList();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            }

            CommonButton();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
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
        protected void Load_CodeBooList()
        {
            try
            {
                string comcod = this.GetComCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "PRECCODE", "",
                                "", "", "", "", "", "", "", "");
                this.ddlPRecBook.DataTextField = "prdesc";
                this.ddlPRecBook.DataValueField = "prcode";
                this.ddlPRecBook.DataSource = dsone.Tables[0];
                this.ddlPRecBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void grvPayRec_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["tbPRecData"];
                this.grvPayRecCod.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvPayRecCod.DataSource = tbl1;
                this.grvPayRecCod.DataBind();
            }
            catch (Exception ex)
            {
            }

        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("tbPRecData");
                    this.lbtnOk.Text = "New";
                    this.LblBookName1.Text = "Code Book:";
                    this.ddlPRecBook.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl.Text = "Reporting Code Book: " + this.ddlPRecBook.SelectedItem.ToString().Trim();
                    this.ShowInformation();
                    this.grvPayRec_DataBind();
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
                this.LblBookName1.Text = "Code Book:";
                this.lbtnOk.Visible = true;
                this.LblBookName1.Visible = true;
                this.lbalterofddl.Visible = false;
                this.ddlPRecBook.Visible = true;
                this.grvPayRecCod.DataSource = null;
                this.grvPayRecCod.DataBind();
            }
        }

        private void ShowInformation()
        {
            string comcod = this.GetComCode();
            string tempddl1 = (this.ddlPRecBook.SelectedValue.ToString()).Substring(0, 2);
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "PRECCODEINFO", tempddl1,
                            "", "", "", "", "", "", "", "");
            Session["tbPRecData"] = ds1.Tables[0];

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvPayRec_DataBind();
        }


        protected void grvPayRecCod_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            string comcod = this.GetComCode();
            string gcode1 = ((Label)grvPayRecCod.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvPayRecCod.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
            string Desc = ((TextBox)grvPayRecCod.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();

            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvPayRecCod.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPPREINF", tgcod,
                           gdesc, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
            }
            this.grvPayRecCod.EditIndex = -1;
            this.ShowInformation();
            this.grvPayRec_DataBind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Reporting Code Book";
                string eventdesc = "Update CodeBook";
                string eventdesc2 = tgcod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void grvPayRecCod_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvPayRecCod.EditIndex = e.NewEditIndex;
            this.grvPayRec_DataBind();
        }
        protected void grvPayRecCod_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvPayRecCod.EditIndex = -1;
            this.grvPayRec_DataBind();
        }
        protected void grvPayRecCod_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvPayRecCod.PageIndex = e.NewPageIndex;
            this.grvPayRec_DataBind();
        }
    }
}
