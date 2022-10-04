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
namespace RealERPWEB.F_03_Fin
{

    public partial class FinCodeBook02 : System.Web.UI.Page
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
                this.lblHeader.Text = "YEARLY CODE VIEW/EDIT";
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALES CODE BOOK INFORMATION VIEW/EDIT";

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
            if (this.ddlFinBook.Items.Count == 0)
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
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GETCODEBOOK", "",
                                "", "", "", "", "", "", "", "");
                this.ddlFinBook.DataTextField = "gdesc";
                this.ddlFinBook.DataValueField = "gcod";
                this.ddlFinBook.DataSource = dsone.Tables[0];
                this.ddlFinBook.DataBind();
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


            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + ASTUtility.Left(gcode2, 2) + gcode2.Substring(3, 2) + ASTUtility.Right(gcode2, 2);
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();


            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "INSERTUPFINRESINF", tgcod,
                           gdesc, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
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
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //DataSet ds1 = Rprss.DataCodeBooks("SP_REPORT_CODEBOOK", comcod, "RPTOTHERACCOUNTCODEBook", "", tempddl2);
            //ReportDocument rptAccCode = new SHIPERPRPT.R_21_Acc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtadress = rptAccCode.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = "OTHER ACCOUNTS  CODE BOOK  REPORT";
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptAccCode.SetDataSource(ds1.Tables[0]);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Sales Code Book";
            //    string eventdesc = "Print CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //Session["Report1"] = rptAccCode;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "OK")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    this.lnkok.Text = "New";
                    this.ddlFinBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    string Code = (this.ddlFinBook.SelectedValue.ToString()).Substring(0, 2);
                    //this.grvacc.Columns[7].Visible = (Code == "88") ? true : false;
                    this.ShowInformation();
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
                this.lnkok.Text = "OK";
                this.ddlFinBook.Enabled = true;
                this.ddlOthersBookSegment.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlFinBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SHOWFINDATA", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                //this.lnknewentry.Visible = true;

            }
            Session["storedata"] = ds1.Tables[0];
        }



        //protected void lnknewentry_Click(object sender, EventArgs e)
        //{
        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = hst["comcod"].ToString();
        //    string comcod = this.GetCompCode();
        //    string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
        //    string sircode = tempddl1 + "0100000000";
        //    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", tempddl1, sircode, "", "", "", "", "0.000000", "", "", "",
        //                "", "", "", "", "");
        //    DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1, "12",
        //                    "", "", "", "", "", "", "");
        //    Session["storedata"] = ds1.Tables[0];


        //    this.grvacc.DataSource = (DataTable)Session["storedata"];
        //    this.grvacc.DataBind();
        //    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;


        //    this.lnknewentry.Visible = false;


        //}



    }
}
