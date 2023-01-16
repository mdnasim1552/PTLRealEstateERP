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
namespace RealERPWEB.F_21_MKT
{

    public partial class MktTeamCodeBook : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "MktTeam" ? "MARKETING TEAM CODE BOOK INFORMATION" : "LETTER CREATION INFORMATION");
                this.ViewSection();

            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            //

        }

        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "MktTeam":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.Load_CodeBooList();
                    break;

                case "MktLetter":
                    this.ShowLetterInfo();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "SalLetter":
                    this.ShowSalLetterInfo();
                    this.MultiView1.ActiveViewIndex = 2;
                    break;




            }


        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Load_CodeBooList()
        {
            try
            {

                string comcod = this.GetComeCode();
                DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "MKTTEAMCODE", "",
                                "", "", "", "", "", "", "", "");
                this.ddlCodeBook.DataTextField = "teamdesc";
                this.ddlCodeBook.DataValueField = "teamcode";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
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
            string rescode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim();
            string rescode2 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim();
            string rescode = rescode1 + rescode2.Substring(0, 2) + rescode2.Substring(3, 2) + rescode2.Substring(6, 2) + rescode2.Substring(9, 2) + rescode2.Substring(12);
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;

            string clientcode = ((DataTable)Session["storedata"]).Rows[rowindex]["clientcode"].ToString();
            string userid = ((DataTable)Session["storedata"]).Rows[rowindex]["userid"].ToString();

            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlClientName");
            DropDownList ddl3 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlUserId");

            if (ASTUtility.Right(rescode, 4) != "0000")
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETMKTCLIENT", "", "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "prosdesc";
                ddl2.DataValueField = "proscod";
                ddl2.DataSource = ds1;
                ddl2.DataBind();
                ddl2.SelectedValue = clientcode;

                /////////////
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETUSERNAME", "%%", "", "", "", "", "", "", "", "");
                ddl3.DataTextField = "usrsname";
                ddl3.DataValueField = "usrid";
                ddl3.DataSource = ds2;
                ddl3.DataBind();
                ddl3.SelectedValue = userid;

            }
            else
            {

                ddl2.Items.Clear();
                ddl3.Items.Clear();
            }

        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    return;
                }

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string proscod1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string proscod2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string Designation = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvdesignatin")).Text.Trim();
                string proscod = "";
                string clientcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlClientName")).Text.Trim();
                string userid = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlUserId")).Text.Trim();

                bool updateallow = true;
                bool c = proscod1.Contains(" ");
                proscod = proscod2.Substring(0, 2) + proscod1.Substring(0, 2) + proscod1.Substring(3, 2) + proscod1.Substring(6, 2) + proscod1.Substring(9, 2) + proscod1.Substring(12, 4);

                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string pactcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();


                if (updateallow)
                {



                    this.grvacc.EditIndex = -1;
                    bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INORUPDATEMKTTM", proscod2.Substring(0, 2), proscod, Desc, Designation, clientcode, userid, "", "", "", "", "", "", "", "", "");
                    string tempddl3 = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
                    tempddl3 = (tempddl3 == "00" ? "" : tempddl3);
                    string tempddl4 = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();

                    DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "MKTTEAMINFO", tempddl3,
                            tempddl4, "", "", "", "", "", "", "");
                    Session["storedata"] = ds1.Tables[0];
                    this.grvacc_DataBind();
                    if (result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        if (ConstantInfo.LogStatus == true)
                        {
                            string eventtype = "Marketing Team CodeBook";
                            string eventdesc = "Update CodeBook";
                            string eventdesc2 = tempddl3;
                            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                        }

                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }


        protected void grvacc_DataBind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)Session["storedata"];

            switch (Type)
            {

                case "MktTeam":
                    this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvacc.DataSource = tbl1;
                    this.grvacc.DataBind();

                    break;


                case "MktLetter":
                    this.grvletterinfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvletterinfo.DataSource = tbl1;
                    this.grvletterinfo.DataBind();
                    break;


                case "SalLetter":
                    this.grvSalletterinfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvSalletterinfo.DataSource = tbl1;
                    this.grvSalletterinfo.DataBind();
                    break;

            }

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.grvacc_DataBind();
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
                    //this.lnkok.Visible = false;
                    //this.lnkcancel.Visible = true;
                    this.LblBookName1.Text = "Code Book:";
                    this.ddlCodeBook.Visible = false;
                    this.ddlCodeBookSegment.Visible = false;
                    this.lbalterofddl0.Visible = true;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Text = "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";
                    this.lbalterofddl.Text = "      Code Book: " + this.ddlCodeBook.SelectedItem.ToString().Trim()
                                 + " " + "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";
                    string dd1value = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
                    string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                    dd1value = (dd1value == "00" ? "" : dd1value);
                    DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "MKTTEAMINFO", dd1value,
                                    dd2value, "", "", "", "", "", "", "");


                    Session["storedata"] = ds1.Tables[0];
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();


                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                }
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.LblBookName1.Text = "Select Code Book:";
                //this.lnkok.Visible = true;
                this.lbalterofddl0.Visible = false;
                this.LblBookName1.Visible = true;
                this.lbalterofddl.Visible = false;

                this.ddlCodeBook.Visible = true;
                this.ddlCodeBookSegment.Visible = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
            }
        }



        protected void lnknewentry_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string dd1value = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
            string proscod = dd1value + "010000000000";
            bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INORUPDATEMKTTM", dd1value, proscod, "", "", "", "", "", "", "", "",
                        "", "", "", "", "");
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "MKTTEAMINFO", dd1value, "14",
                            "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];
            grvacc.DataSource = (DataTable)Session["storedata"];
            grvacc.DataBind();
            ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;

        }



        private void ShowLetterInfo()
        {

            string comcod = this.GetComeCode();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "MKTLETTERINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvletterinfo.DataSource = null;
                this.grvletterinfo.DataBind();
                return;

            }

            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();


        }

        private void ShowSalLetterInfo()
        {

            string comcod = this.GetComeCode();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SALLETTERINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvletterinfo.DataSource = null;
                this.grvletterinfo.DataBind();
                return;

            }

            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();


        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        protected void grvletterinfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvletterinfo.EditIndex = -1;
            this.grvacc_DataBind();
        }
        protected void grvletterinfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvletterinfo.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
        }

        protected void grvletterinfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    return;
                }

                string comcod = this.GetComeCode();
                string letcod1 = ((Label)grvletterinfo.Rows[e.RowIndex].FindControl("lbgrletcode")).Text.Trim().Replace("-", "");
                string letcode3 = ((TextBox)grvletterinfo.Rows[e.RowIndex].FindControl("txtgrletcode3")).Text.Trim().Replace("-", "");
                string Desc = ((TextBox)grvletterinfo.Rows[e.RowIndex].FindControl("txtgvDescletter")).Text.Trim();
                string Code = letcod1 + letcode3;
                if (Code.Length != 7)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Code Length Must Be 7 Digit";
                    return;
                }
                bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INORUPDATELETTER", Code, Desc, "", "", "", "", "", "", "", "", "", "", "", "", "");
                this.grvletterinfo.EditIndex = -1;

                if (result)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    this.ShowLetterInfo();
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }


        }
        protected void grvSalletterinfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvSalletterinfo.EditIndex = -1;
            this.grvacc_DataBind();
        }
        protected void grvSalletterinfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvSalletterinfo.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
        }
        protected void grvSalletterinfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    return;
                }

                string comcod = this.GetComeCode();
                string letcod1 = ((Label)this.grvSalletterinfo.Rows[e.RowIndex].FindControl("lbgrletcodesal")).Text.Trim().Replace("-", "");
                string letcode3 = ((TextBox)this.grvSalletterinfo.Rows[e.RowIndex].FindControl("txtgrletcodesal")).Text.Trim().Replace("-", "");
                string Desc = ((TextBox)this.grvSalletterinfo.Rows[e.RowIndex].FindControl("txtgvDesclettersal")).Text.Trim();
                string Code = letcod1 + letcode3;
                if (Code.Length != 7)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Code Length Must Be 7 Digit";
                    return;
                }

                bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INORUPSALLETTER", Code, Desc, "", "", "", "", "", "", "", "", "", "", "", "", "");
                this.grvSalletterinfo.EditIndex = -1;

                if (result)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    this.ShowSalLetterInfo();
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }


        protected void grvSalletterinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvSalletterinfo.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
    }
}
