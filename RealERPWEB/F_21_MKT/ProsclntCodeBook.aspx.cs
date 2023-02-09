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
//
namespace RealERPWEB.F_21_MKT
{
    public partial class ProsclntCodeBook : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";   
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Code";
            }

            if (this.ddlCodeBook.Items.Count == 0)
                this.Load_CodeBooList();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

            //

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
                DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "PACCOUNTCODE", "", "", "", "", "", "", "", "", "");
                this.ddlCodeBook.DataTextField = "proscode";
                this.ddlCodeBook.DataValueField = "proscode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
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
            this.ConfirmMessage.Visible = true;
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
                string proscod1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
                string proscod2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
                string active = (((CheckBox)this.grvacc.Rows[e.RowIndex].FindControl("chkactive")).Checked) ? "True" : "False";
                string proscod = "";

                bool c = proscod1.Contains(" ");
                proscod = proscod2 + proscod1;
                if (proscod.Length != 14)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Code Lenght Must be 14 degit";
                    return;
                }

                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string Address = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvaddress")).Text.Trim();
                string Phone = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvphone")).Text.Trim();
                string Email = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvemail")).Text.Trim();

                DataTable tbl1 = (DataTable)Session["storedata"];
                int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                tbl1.Rows[Index]["proscod"] = proscod;
                tbl1.Rows[Index]["PROSDESC"] = Desc;
                tbl1.Rows[Index]["caddress"] = Address;
                tbl1.Rows[Index]["phone"] = Phone;
                tbl1.Rows[Index]["email"] = Email;
                tbl1.Rows[Index]["active"] = active;

                if (proscod.Substring(10) != "0000")
                {

                    if (Desc.Length == 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Name  Should Not Be Empty";
                        return;
                    }

                    // string Snameaddpandempid = Phone + Email;


                    DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "CHECKEDDUPUCLIENT", Phone, Email, "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                        ;


                    else
                    {

                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("proscod <>'" + proscod + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Name" + "<br />" + "Sales Team: " + dt.Rows[0]["teamdesc"].ToString();
                            //this.ddlPrevReqList.Items.Clear();
                            return;
                        }
                    }

                }

                bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "PACCOUNTUPDATE", proscod2.Substring(0, 2), proscod, Desc, Address, Phone, Email, active, "", "", "", "", "", "", "", "");
                this.grvacc.EditIndex = -1;
                this.ShowInformation();

                if (result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";


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


        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["storedata"];
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();

        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                string comcod = this.GetCompCode();
                this.lnkok.Text = "New";
                this.LblBookName1.Text = "Code Book:";
                this.ddlCodeBook.Visible = false;
                this.ddlCodeBookSegment.Visible = false;
                this.lbalterofddl.Visible = true;
                this.lbalterofddl0.Visible = true;
                this.ibtnSrch.Visible = true;
                this.PanelSearch.Visible = true;
                this.lbalterofddl.Text = "      Code Book: " + this.ddlCodeBook.SelectedItem.ToString().Trim();
                this.lbalterofddl0.Text = "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";
                string tempddl1 = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
                string tempddl2 = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                this.GetDistrict();
                this.ShowInformation();
            }

            else
            {

                this.lnkok.Text = "Ok";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.LblBookName1.Text = "Select Code Book:";
                this.PanelSearch.Visible = false;

                this.lbalterofddl.Visible = false;
                this.lbalterofddl0.Visible = false;
                this.ddlCodeBook.Visible = true;
                this.ddlCodeBookSegment.Visible = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();

            }


        }

        private void GetDistrict()
        {
            string comcod = this.GetCompCode();
            string countrycode = this.ddlCodeBook.SelectedValue.ToString().Substring(0, 2);
            DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETDISTRICT", countrycode, "", "", "", "", "", "", "", "");
            this.ddlDistName.DataTextField = "dstrictname";
            this.ddlDistName.DataValueField = "dstrictcode";
            this.ddlDistName.DataSource = dsone.Tables[0];
            this.ddlDistName.DataBind();
            dsone.Dispose();
            this.ddlDistName_SelectedIndexChanged(null, null);




        }


        private void ShowInformation()
        {
            Session.Remove("storedata");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string tempddl1 = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
            string districtcode = (this.ddlDistName.SelectedValue.ToString() == "0000") ? "%" : this.ddlDistName.SelectedValue.ToString() + "%";
            string catagory = (this.ddlCatagory.SelectedValue.ToString() == "000000") ? "%" : this.ddlCatagory.SelectedValue.ToString() + "%";
            string Calltype = (this.Request.QueryString["Type"] == "Mgt") ? "ALLPACCOUNTINFO" : "PACCOUNTINFO";




            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", Calltype, tempddl1,
                          tempddl2, districtcode, catagory, userid, "", "", "", "");

            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.grvacc_DataBind();
        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlDistName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string dstrictcode = this.ddlDistName.SelectedValue.ToString();
            DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETCATAGORY", dstrictcode, "", "", "", "", "", "", "", "");
            this.ddlCatagory.DataTextField = "catagryname";
            this.ddlCatagory.DataValueField = "catagrycode";
            this.ddlCatagory.DataSource = dsone.Tables[0];
            this.ddlCatagory.DataBind();
            dsone.Dispose();

        }

        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
    }
}
