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
namespace RealERPWEB.F_81_Hrm.F_82_App

{
    public partial class AccCodeBook : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            //((Label)this.Master.FindControl("lblTitle")).Text = "Departement Code";
            if (this.ddlCodeBook.Items.Count == 0)
                this.Load_CodeBooList();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void Load_CodeBooList()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string CallType = (this.Request.QueryString["InputType"] == "Accounts") ? "ACCOUNTCODE" : "ACCOUNTCOMCODE";

                DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", CallType, "",
                                "", "", "", "", "", "", "", "");
                Session["LoadDataForDropDownList"] = dsone.Tables[0];
                DataTable dt1 = (DataTable)Session["LoadDataForDropDownList"];
                this.ddlCodeBook.DataSource = dt1;
                this.ddlCodeBook.DataTextField = "actcode";
                this.ddlCodeBook.DataValueField = "actcode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        //private void LoadGrid()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string dd1value = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
        //    string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
        //    tempddl1 = dd1value;
        //    tempddl2 = dd2value;

        //    DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", "dd1value",
        //                    "dd2value", "", "", "", "", "", "", "");
        //    if (ds1.Tables[0].Rows.Count == 0)
        //    {
        //        this.lnknewentry.Visible = true; 
        //    }
        //    Session["storedata"] = ds1.Tables[0];
        //    this.grvacc.EditIndex = -1; 
        //    this.grvacc_DataBind();
        //}

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
            //try
            {
                string actcode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string actcode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string actcode = "";
                bool updateallow = true;
                bool c = actcode1.Contains(" ");
                if (actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    actcode = actcode2.Substring(0, 2) + actcode1.Substring(0, 2) + actcode1.Substring(3, 4) + actcode1.Substring(8, 4);
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    updateallow = false;
                }
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtgvlevel = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridlevel")).Text.Trim();
                string typeCode = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvTypeCode")).Text.Trim();
                string TypeDesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvTypeDesc")).Text.Trim();
                string pactcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                DataTable tbl1 = (DataTable)Session["storedata"];
                string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                if (dd2value == "4" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) != pactcode1.Substring(2, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (dd2value == "8" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (dd2value == "12" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) == "0000" && actcode1.Substring(8, 4) != "0000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }

                if (updateallow)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    string comnam = hst["comnam"].ToString();
                    string comadd = hst["comadd1"].ToString();
                    string compname = hst["compname"].ToString();
                    string username = hst["username"].ToString();
                    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                    tbl1.Rows[Index]["ACTCODE"] = actcode;
                    tbl1.Rows[Index]["ACTDESC"] = Desc;
                    tbl1.Rows[Index]["ACTELEV"] = txtgvlevel;
                    tbl1.Rows[Index]["ACTTYPE"] = typeCode;
                    tbl1.Rows[Index]["ACTTDESC"] = TypeDesc;
                    this.grvacc.EditIndex = -1;
                    bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTUPDATE", actcode2.Substring(0, 2), actcode, Desc, txtgvlevel, typeCode, TypeDesc, "", "", "", "",
                        "", "", "", "", "");
                    string tempddl3 = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
                    tempddl3 = (tempddl3 == "00" ? "" : tempddl3);
                    string tempddl4 = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                    DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", tempddl3,
                            tempddl4, "", "", "", "", "", "", "");
                    Session["storedata"] = ds1.Tables[0];
                    this.grvacc_DataBind();
                    if (result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }
                }
            }
            //catch (Exception ex)
            //{
            //   this.lblprintstk.Text = "Error:" + ex.Message;
            //}
        }

        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["storedata"];
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
            if (tbl1.Rows.Count == 0)
                return;

            ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
            ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Visible = true;
            ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;
        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc.PageIndex = ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex;
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Visible == true)
            {
                this.lnkok_Click(null, null);
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CodeDesc = this.ddlCodeBook.SelectedItem.ToString().Trim().Substring(3)
                        + " " + "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";

            DataTable ddup = (DataTable)Session["storedata"];
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccountcode2();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtCodeBookDesc = rptstk.ReportDefinition.ReportObjects["CodeBookDesc"] as TextObject;
            txtCodeBookDesc.Text = CodeDesc;
            rptstk.SetDataSource(ddup);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.lnkok.Visible = false;
                this.lnkcancel.Visible = true;
                this.ddlCodeBook.Visible = false;
                this.ddlCodeBookSegment.Visible = false;
                //this.LblBookName1.Visible = false;
                this.lbalterofddl.Visible = true;
                this.lbalterofddl0.Visible = true;
                this.LblBookName1.Text = "Company Code Book:";
                this.lbalterofddl.Text = this.ddlCodeBook.SelectedItem.ToString().Trim();
                this.lbalterofddl0.Text = "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";
                //this.lbalterofddl.Text = "      Code Book: " + this.ddlCodeBook.SelectedItem.ToString().Trim()
                //             + " " + "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";
                string dd1value = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
                dd1value = (dd1value == "00" ? "" : dd1value);
                string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                DataSet ds1 = new DataSet();
                ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", dd1value,
                                    dd2value, "", "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                    return;

                Session["storedata"] = ds1.Tables[0];
                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void lnkcancel_Click(object sender, EventArgs e)
        {
            this.lnkok.Visible = true;
            this.lnkcancel.Visible = false;
            this.LblBookName1.Text = "Select Code Book:";
            this.lbalterofddl.Visible = false;
            this.lbalterofddl0.Visible = false;
            this.ddlCodeBook.Visible = true;
            this.ddlCodeBookSegment.Visible = true;
            this.grvacc.DataSource = null;
            this.grvacc.DataBind();
        }


    }
}
