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
namespace RealERPWEB.F_39_MyPage
{
    public partial class ProGenCode : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Sales") ? "SALES CODE BOOK INFORMATION VIEW/EDIT" : "SUPPLIER INFORMATION FIELD VIEW/EDIT";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();

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
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                string comcod = this.GetCompCode();
                string Type = this.Request.QueryString["Type"].ToString().Trim();

                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTSALECODE", Type,
                                "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "gdesc";
                this.ddlOthersBook.DataValueField = "gcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }


        private void LoadGrid()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

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

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string Rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvrate")).Text.Trim()).ToString();

            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPSALINF", tgcod,
                           gdesc, Gtype, Rate, "", "", "", "", "", "", "", "", "", "", "");

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
            this.grvacc.EditIndex = -1;
            this.ShowInformation();
            this.grvacc_DataBind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales Code Book";
                string eventdesc = "Update CodeBook";
                string eventdesc2 = tgcod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];

                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
                double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                for (int i = 1; i <= TotalPage; i++)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                if (TotalPage > 1)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;



            }
            catch (Exception ex)
            {
            }

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {






        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    this.lnkok.Text = "New";
                    this.ddlOthersBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    string Code = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
                    this.grvacc.Columns[7].Visible = (Code == "88") ? true : false;
                    this.ShowInformation();
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();

                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
            }
            else
            {
                this.lnkok.Text = "Ok";
                this.ddlOthersBook.Enabled = true;
                this.ddlOthersBookSegment.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTSALECODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
        }







    }
}
