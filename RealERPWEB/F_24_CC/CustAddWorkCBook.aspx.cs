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
using dpant;

namespace RealERPWEB.F_24_CC
{
    public partial class CustAddWorkCBook : System.Web.UI.Page
    {

        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        protected FullGridPager fullGridPager;
        protected int MaxVisible = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "ADDITIONAL BOOK INFORMATION VIEW/EDIT";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }
            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            if (IsPostBack)
            {


                fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
                fullGridPager.CreateCustomPager(grvacc.BottomPagerRow);
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "CUSTADWRKCODE", "",
                                "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "gdesc";
                this.ddlOthersBook.DataValueField = "gcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
            string comcod = this.GetComeCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string unit = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtunit")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPSALINF", tgcod,
                           gdesc, Gtype, "0", unit, "", "", "", "", "", "", "", "", "", "");
            this.grvacc.EditIndex = -1;
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
            this.ShowInformation();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Addtional CodeBook";
                string eventdesc = "Update CodeBook";
                string eventdesc2 = tgcod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        private void ShowInformation()
        {
            string comcod = this.GetComeCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "CUSTADDCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }


        protected void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
                //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                //for (int i = 1; i <= TotalPage; i++)
                //    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                //if (TotalPage > 1)
                //    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = true;
                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;



            }
            catch (Exception ex)
            {
            }

        }

        //protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
        //        this.grvacc.EditIndex = -1;
        //        this.grvacc_DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkok.Text == "Ok")
                {
                    this.lnkok.Text = "New";
                    string comcod = this.GetComeCode();
                    this.ddlOthersBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    //this.lbalterofddl.Visible = true;
                    //this.lblSegmentDetails.Visible = true;
                    //this.lbalterofddl.Text = "Code Book: " + this.ddlOthersBook.SelectedItem.ToString().Trim();
                    //this.lblSegmentDetails.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

                    this.ShowInformation();
                }
                else
                {
                    this.lnkok.Text = "Ok";
                    this.ddlOthersBook.Enabled = true;
                    this.ddlOthersBookSegment.Enabled = true;
                    //this.lbalterofddl.Visible = false;
                    //this.lblSegmentDetails.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();


                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }

        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        } 

        protected void grvacc_DataBound(object sender, EventArgs e)
        {
            if (fullGridPager == null)
            {
                fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
            }
            fullGridPager.CreateCustomPager(grvacc.BottomPagerRow);
            fullGridPager.PageGroups(grvacc.BottomPagerRow);
        }

        protected void ddlPageGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fullGridPager == null)
            {
                fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
            }
            fullGridPager.PageGroupChanged(grvacc.BottomPagerRow);
        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].ToolTip = "Edit Information";

                int index = e.Row.RowIndex;
                int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + index;
                DataTable dt = ((DataTable)Session["storedata"]);

                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 7) == "0000000" && ASTUtility.Right(Code, 5) != "000000")
                {
                    e.Row.Attributes["style"] = "background-color:#8DF4A9; font-weight:bold;";
                }
                else if (ASTUtility.Right(Code, 5) == "000000" && ASTUtility.Right(Code, 7) != "0000000")
                {
                    e.Row.Attributes["style"] = "background-color:#9EF5DC; font-weight:bold;";

                }
                else if (ASTUtility.Right(Code, 3) == "000" && ASTUtility.Right(Code, 5) != "00000")
                {
                    e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";
                }              

            }
        }
    }
}