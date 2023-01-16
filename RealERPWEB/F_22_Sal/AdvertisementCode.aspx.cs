using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_22_Sal
{
    public partial class AdvertisementCode : System.Web.UI.Page
    {
        ProcessRAccess Rprss = new ProcessRAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Advertisement Cood Book";

            }
            if (this.ddlSalPayment.Items.Count == 0)
                this.Load_CodeBooList();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

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
                string Code = (this.Request.QueryString["Type"].ToString() == "MktCode") ? "09" : "";
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDVERTISECODE",
                                Code, "", "", "", "", "", "", "", "");
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
                    // this.lnkok.Visible = false;
                    // this.lnkcancel.Visible = true;
                    this.ddlSalPayment.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    //this.LblBookName1.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.lbalterofddl.Text = "Advertisement Code Book: " + this.ddlSalPayment.SelectedItem.ToString().Trim();
                    // + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    this.lbalterofddl0.Text = this.ddlOthersBookSegment.SelectedItem.ToString().Trim();

                    this.ShowInformation();
                    this.gvAddCode.EditIndex = -1;
                    this.gvAddCode_DataBind();

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
                //this.lnkcancel.Visible = false;
                //this.LblBookName1.Visible = true;
                this.lbalterofddl.Visible = false;
                this.lbalterofddl0.Visible = false;
                this.ddlSalPayment.Visible = true;
                this.ddlOthersBookSegment.Visible = true;

                this.gvAddCode.DataSource = null;
                this.gvAddCode.DataBind();
            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // this.gvPaySch_DataBind ();
            }
            catch (Exception ex)
            {
            }
        }

        protected void gvAddCode_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (((TextBox)gvAddCode.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Length == 6)
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();

                string comcod = this.GetCompCode();
                string gcode1 = ((Label)gvAddCode.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
                string gcode2 = ((TextBox)gvAddCode.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");

                string Desc = ((TextBox)gvAddCode.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string tgcod = gcode1.Substring(0, 2) + gcode2;
                string gdesc = ((TextBox)this.gvAddCode.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string gtype = ((TextBox)this.gvAddCode.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
                string Gtype = (gtype.ToString() == "") ? "T" : gtype;
                bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPADDCODE", tgcod,
                               gdesc, Gtype, "", "", "", "", "", "", "", "", "", "", "", "");

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
                ((Label)this.Master.FindControl("lblmsg")).Text = "Advertisement Code Must be 7 Degits!";
            }
            this.gvAddCode.EditIndex = -1;
            this.ShowInformation();
            this.gvAddCode_DataBind();
        }

        protected void gvAddCode_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvAddCode.EditIndex = e.NewEditIndex;
            this.gvAddCode_DataBind();
        }

        protected void gvAddCode_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvAddCode.EditIndex = -1;
            this.gvAddCode_DataBind();
        }

        protected void gvAddCode_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAddCode.PageIndex = e.NewPageIndex;
            this.gvAddCode_DataBind();
        }

        protected void gvAddCode_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                this.gvAddCode.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvAddCode.DataSource = tbl1;
                this.gvAddCode.DataBind();


            }
            catch (Exception ex)
            {
            }

        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlSalPayment.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "LAODADDCODE", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
        }
    }
}