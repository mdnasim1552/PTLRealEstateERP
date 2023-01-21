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
namespace RealERPWEB.F_81_Hrm.F_99_MgtAct
{
    public partial class GenCodeBook : System.Web.UI.Page
    {

        ProcessAccess da = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.Load_CodeBooList();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }



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
                string type = this.Request.QueryString["Type"].ToString();
                string code = (type == "01") ? "01" : (type == "02") ? "02" : (type == "03") ? "03" : (type == "04") ? "04" : (type == "05") ? "05"
                        : (type == "06") ? "06" : (type == "07") ? "07" : (type == "08") ? "08" : (type == "09") ? "09" : (type == "10") ? "10" : (type == "11") ? "11"
                        : (type == "12") ? "12" : (type == "13") ? "13" : (type == "14") ? "14" : (type == "15") ? "18" : (type == "16") ? "25" : (type == "17") ? "41"
                        : (type == "18") ? "42" : (type == "19") ? "43" : (type == "20") ? "44" : "45";
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETMISGENCODE", code,
                                "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "gendesc";
                this.ddlOthersBook.DataValueField = "gencode";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void grvaccRP_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["tbGenCode"];
                this.gvmiscodeBook.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvmiscodeBook.DataSource = tbl1;
                this.gvmiscodeBook.DataBind();

                for (int i = 0; i < gvmiscodeBook.Rows.Count; i++)
                {
                    string usircode = ((Label)gvmiscodeBook.Rows[i].FindControl("lbgrcod1")).Text.Trim();
                    ImageButton lbtn1 = (ImageButton)gvmiscodeBook.Rows[i].FindControl("imgbtn");
                    if (lbtn1 != null)
                        //if (lbtn1.Length > 0)
                        lbtn1.CommandArgument = usircode;
                }

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

                    this.lbtnOk.Text = "New";
                    this.LblBookName1.Text = "Code Book:";
                    this.ddlOthersBook.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl.Text = "Code Book: " + this.ddlOthersBook.SelectedItem.ToString().Trim();
                    this.ShowInformation();
                    this.grvaccRP_DataBind();
                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
            }
            else
            {
                this.lbtnOk.Text = "Ok";

                this.LblBookName1.Text = "Select Code Book:";
                this.lbtnOk.Visible = true;
                this.LblBookName1.Visible = true;
                this.lbalterofddl.Visible = false;
                this.ddlOthersBook.Visible = true;
                this.gvmiscodeBook.DataSource = null;
                this.gvmiscodeBook.DataBind();
            }
        }

        private void ShowInformation()
        {
            string comcod = this.GetComCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "MISGENCODEINFO", tempddl1,
                            "", "", "", "", "", "", "", "");
            Session["tbGenCode"] = ds1.Tables[0];

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvaccRP_DataBind();
        }

        protected void gvmiscodeBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmiscodeBook.PageIndex = e.NewPageIndex;
            this.grvaccRP_DataBind();
        }
        protected void gvmiscodeBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvmiscodeBook.EditIndex = -1;
            this.grvaccRP_DataBind();
        }
        protected void gvmiscodeBook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvmiscodeBook.EditIndex = e.NewEditIndex;
            this.grvaccRP_DataBind();
        }
        protected void gvmiscodeBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string comcod = this.GetComCode();
            string gcode1 = ((Label)gvmiscodeBook.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)gvmiscodeBook.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();

            string Desc = ((TextBox)gvmiscodeBook.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2.Substring(0, 2) + gcode2.Substring(3, 2) + ASTUtility.Right(gcode2, 3);
            string gdesc = ((TextBox)this.gvmiscodeBook.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string unit = ((TextBox)this.gvmiscodeBook.Rows[e.RowIndex].FindControl("txtgvUnit")).Text.Trim();
            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPMISGINF", tgcod,
                           gdesc, unit, "", "", "", "", "", "", "", "", "", "", "", "");

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
            this.gvmiscodeBook.EditIndex = -1;
            this.ShowInformation();
            this.grvaccRP_DataBind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Reporting Code Book";
                string eventdesc = "Update CodeBook";
                string eventdesc2 = tgcod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string comcod = this.GetComCode();
            string gcode1 = lblGrpCode.Text;
            string gcode2 = txtCode2.Text;
            string tgcod = gcode1.Substring(0, 2) + gcode2.Substring(0, 2) + gcode2.Substring(3, 2) + ASTUtility.Right(gcode2, 3);

            string gdesc = txtDesc.Text;
            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPRPINF", tgcod,
                           gdesc, "", "", "", "", "", "", "", "", "", "", "", "", "");

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
            this.gvmiscodeBook.EditIndex = -1;
            this.ShowInformation();
            this.grvaccRP_DataBind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Reporting Code Book";
                string eventdesc = "Update CodeBook";
                string eventdesc2 = gcode2;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void imgbtn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            //lblID.Text = grvaccRp.DataKeys[gvrow.RowIndex].Value.ToString();
            //lblusername.Text = gvrow.Cells[1].Text;
            //txtfname.Text = gvrow.Cells[2].Text;
            string code = Convert.ToString(((ImageButton)sender).CommandArgument).Trim();

            DataTable dt = (DataTable)Session["tbRPCode"];
            DataRow[] dr = dt.Select("rpcode='" + code + "'");


            // int rpcode =Convert.ToInt32(dr[0]["rpcode"].ToString())+1;



            lblGrpCode.Text = dr[0]["rpcode2"].ToString();
            txtCode2.Text = dr[0]["rpcode3"].ToString();
            txtDesc.Text = gvrow.Cells[3].Text;
            this.ModalPopupExtender1.Show();
        }

    }
}
