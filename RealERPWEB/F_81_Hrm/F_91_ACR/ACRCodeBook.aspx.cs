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
namespace RealERPWEB.F_81_Hrm.F_91_ACR
{
    public partial class ACRCodeBook : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.Load_CodeBooList();
            }

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
                DataSet dsone = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "OACCOUNTACRCODE", "", "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "acrgdesc";
                this.ddlOthersBook.DataValueField = "acrgcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {


                string Msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                
            }

        }


        protected void gvACRcBook_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];

                this.gvAcrCBook.DataSource = tbl1;
                this.gvAcrCBook.DataBind();
                //this.gvAcrCBook.Columns[6].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "07") ? true : false;
                //this.gvAcrCBook.Columns[7].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "07") ? true : false;
                //this.gvAcrCBook.Columns[9].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "08") ? true : false;

                //((DropDownList)this.gvAcrCBook.FooterRow.FindControl("ddlPageNo")).Visible = false;
                //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvAcrCBook.PageSize);
                //((DropDownList)this.gvAcrCBook.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                //for (int i = 1; i <= TotalPage; i++)
                //    ((DropDownList)this.gvAcrCBook.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                //if (TotalPage > 1)
                //    ((DropDownList)this.gvAcrCBook.FooterRow.FindControl("ddlPageNo")).Visible = true;
                //((DropDownList)this.gvAcrCBook.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvAcrCBook.PageIndex;



            }
            catch (Exception ex)
            {
            }

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.gvAcrCBook.PageIndex = ((DropDownList)this.gvAcrCBook.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
                this.gvACRcBook_DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //if (this.lnkok.Visible)
            //    this.lbtnOk_Click(null, null);

            //string CodeDesc = this.ddlOthersBook.SelectedItem.ToString().Trim().Substring(3)
            //            + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable Dtable = (DataTable)Session["storedata"];
            //string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //ReportDocument rptAccCode = new RealERPRPT.R_81_Hrm.R_82_App.RptHRCodeBookInfo();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            //this.lbljavascript.Text = "<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            tempddl1 = ((tempddl1 == "00" ? "" : tempddl1));
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "OACCOUNTACRCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
        }
        protected void lnkcancel_Click(object sender, EventArgs e)
        {
            this.lnkok.Visible = true;

            this.LblBookName1.Visible = true;
            this.lbalterofddl.Visible = false;
            this.ddlOthersBook.Visible = true;
            this.ddlOthersBookSegment.Visible = true;
            this.gvAcrCBook.DataSource = null;
            this.gvAcrCBook.DataBind();

        }


        protected void gvAcrCBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvAcrCBook.EditIndex = -1;
            this.gvACRcBook_DataBind();
        }
        protected void gvAcrCBook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvAcrCBook.EditIndex = e.NewEditIndex;
            this.gvACRcBook_DataBind();
        }
        protected void gvAcrCBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

           
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)gvAcrCBook.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)gvAcrCBook.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
            if (gcode2.Length != 3)
                return;

            string Desc = ((TextBox)gvAcrCBook.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.gvAcrCBook.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gddesc = ((TextBox)this.gvAcrCBook.Rows[e.RowIndex].FindControl("txtgvddesc")).Text.Trim();
            //string gtype = ((TextBox)this.gvAcrCBook.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            //string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            //string percent = Convert.ToDouble("0" + ((TextBox)this.gvAcrCBook.Rows[e.RowIndex].FindControl("txtgvtxtpercnt")).Text.Trim()).ToString();
            //string unit = ((TextBox)this.gvAcrCBook.Rows[e.RowIndex].FindControl("txtgvUnit")).Text.Trim().ToString();
            //string rate = Convert.ToDouble("0" + ((TextBox)this.gvAcrCBook.Rows[e.RowIndex].FindControl("txtgvRate")).Text.Trim()).ToString();
            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INSERTUPACRCBOOKINF", tgcod,
                           gdesc, gddesc, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                string Msg = " Successfully Updated ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);

            }

            else
            {
                string Msg = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);               
            }
            this.gvAcrCBook.EditIndex = -1;
            this.ShowInformation();
            this.gvACRcBook_DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                
                if(this.lnkok.Text.ToString() == "NEW")
                {
                    this.gvAcrCBook.DataSource = null;
                    this.gvAcrCBook.DataBind();
                    this.lnkok.Text = "OK";
                }
                else
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    this.lnkok.Text = "NEW";

                    this.ShowInformation();
                    this.gvACRcBook_DataBind();
                }

                //this.ddlOthersBook.Visible = false;
                //this.ddlOthersBookSegment.Visible = false;
                //this.LblBookName1.Visible = false;
                //this.lbalterofddl.Visible = true;
                //this.lbalterofddl.Text = "ACR Code Book: " + this.ddlOthersBook.SelectedItem.ToString().Trim()
                //    + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

 

            }
            catch (Exception ex)
            {
                string Message = "Information not found!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
 
            }
        }
    }
}
