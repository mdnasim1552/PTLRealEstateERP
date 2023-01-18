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
    public partial class HRCodeBook : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                if (this.ddlOthersBook.Items.Count == 0)
                    this.Load_CodeBooList();

            }

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

            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                string comcod = this.GetCompCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "OACCOUNTHRCODE", "", "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "hrgdesc";
                this.ddlOthersBook.DataValueField = "hrgcod";
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

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string msg = "";
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod3")).Text.Trim();
            if (gcode2.Length != 3)
                return;

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gdescbn = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDescBn")).Text.Trim();

            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string percent = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvtxtpercnt")).Text.Trim()).ToString();
            string unit = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvUnit")).Text.Trim().ToString();
            string slno = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvtslno")).Text.Trim();
            string rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvRate")).Text.Trim()).ToString();
            string sl = slno.Length == 0 ? "0" : slno;

            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INSERTUPHRINF", tgcod,
                           gdesc, Gtype, percent, unit, rate, sl, "", gdescbn, "", "", "", "", "", "");

            if (result == true)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                string Msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);
            }

            else
            {
                string Msg = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
            }
            this.grvacc.EditIndex = -1;
            this.ShowInformation();
            this.grvacc_DataBind();

        }

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                //this.grvacc.Columns[7].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "07") ? true : false;
                //this.grvacc.Columns[8].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "07") ? true : false;
               // this.grvacc.Columns[9].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "04") ? true : false;

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

        //        //this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
        //        //this.grvacc_DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Visible)
                this.lnkok_Click(null, null);

            string CodeDesc = this.ddlOthersBook.SelectedItem.ToString().Trim().Substring(3)
                        + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable Dtable = (DataTable)Session["storedata"];
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            ReportDocument rptAccCode = new RealERPRPT.R_81_Hrm.R_82_App.RptHRCodeBookInfo();
            TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            rpttxtNameR.Text = CodeDesc;
            TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAccCode.SetDataSource(Dtable);
            Session["Report1"] = rptAccCode;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                Session.Remove("storedata");
                this.lnkok.Visible = false;
                this.lnkcancel.Visible = true;

                this.ddlOthersBook.Enabled = false;
                this.ddlOthersBookSegment.Enabled = false;
               
                //this.lbalterofddl.Visible = true;
                //this.lbalterofddl.Text = "HR Code Book: " + this.ddlOthersBook.SelectedItem.ToString().Trim()
                //             + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

                this.ShowInformation();
                this.grvacc_DataBind();

            }
            catch (Exception ex)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                string Msg = "Information not found!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            tempddl1 = ((tempddl1 == "00" ? "" : tempddl1));
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "OACCOUNTHRCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
        }
        protected void lnkcancel_Click(object sender, EventArgs e)
        {
            this.lnkok.Visible = true;
            this.lnkcancel.Visible = false;

        
     

            this.ddlOthersBook.Enabled = true;
            this.ddlOthersBookSegment.Enabled = true;
            this.grvacc.DataBind();

        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int index = this.grvacc.PageSize * this.grvacc.PageIndex + RowIndex;
            string hrgcod = ((DataTable)Session["storedata"]).Rows[index]["hrgcod"].ToString();
            //this.lbgrcod.Text = gcod;
            this.hrgcodechk.Text = hrgcod;
            this.txthrgcode.Text = hrgcod.Substring(0, 2) + "-"  + ASTUtility.Right(hrgcod, 3);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
        }

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string hrgcod = hrgcodechk.Text;


            string tgrcode = this.txthrgcode.Text.Trim().Replace("-", "");
            string Desc = this.txtDesc.Text.Trim();
            string DescBN = this.txtDescBN.Text.Trim();
            string gtype = this.txttype.Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
;
            string mnumber = (hrgcod == tgrcode) ? "" : "manual";

            bool isResultValid = false;
            if (Desc.Length == 0)
            {
                msg = "Resource Head is not empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                isResultValid = false;
                return;
            }

            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INSERTHRGCODE", tgrcode,
                          Desc, DescBN, Gtype, mnumber, "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Created ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Create Failed";
            }
            ShowInformation();
            grvacc_DataBind();
        }
    }
}
