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
    public partial class FinCodeBook : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sales payment Schedule Cood Book";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sales payment Schedule Cood Book";
                this.Load_CodeBooList();

            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "";
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


                string comcod = this.GetCompCode();

                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GETFINGENCODE",
                                "", "", "", "", "", "", "", "", "");
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

        private void LoadGrid()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlSalPayment.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "OACCOUNTINFO", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");
            //if (ds1.Tables[0].Rows.Count == 0)
            //{
            //    this.lnknewentry.Visible = true;

            //}
            Session["storedata"] = ds1.Tables[0];
            this.gvPaySch_DataBind();

        }


        protected void gvPaySch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {


            this.gvPaySch.EditIndex = -1;
            this.gvPaySch_DataBind();

        }
        protected void gvPaySch_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvPaySch.EditIndex = e.NewEditIndex;
            this.gvPaySch_DataBind();
        }
        protected void gvPaySch_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (((TextBox)gvPaySch.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Length == 6)
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();

                string comcod = this.GetCompCode();
                string gcode1 = ((Label)gvPaySch.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
                string gcode2 = ((TextBox)gvPaySch.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");

                string Desc = ((TextBox)gvPaySch.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string tgcod = gcode1.Substring(0, 2) + gcode2;
                string gdesc = ((TextBox)this.gvPaySch.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string gtype = ((TextBox)this.gvPaySch.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
                string Gtype = (gtype.ToString() == "") ? "T" : gtype;
                bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "INSERTUPFINGINF", tgcod,
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
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sales Payment Schedule Code Must be 7 Degits!";
            }
            this.gvPaySch.EditIndex = -1;
            this.ShowInformation();
            this.gvPaySch_DataBind();


        }

        protected void gvPaySch_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];

                this.gvPaySch.DataSource = tbl1;
                this.gvPaySch.DataBind();
                ((DropDownList)this.gvPaySch.FooterRow.FindControl("ddlPageNo")).Visible = false;
                double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvPaySch.PageSize);
                ((DropDownList)this.gvPaySch.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                for (int i = 1; i <= TotalPage; i++)
                    ((DropDownList)this.gvPaySch.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                if (TotalPage > 1)
                    ((DropDownList)this.gvPaySch.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.gvPaySch.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvPaySch.PageIndex;

            }
            catch (Exception ex)
            {
            }

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.gvPaySch.PageIndex = ((DropDownList)this.gvPaySch.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
                this.gvPaySch.EditIndex = -1;
                this.gvPaySch_DataBind();
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
            //DataSet ds1 = Rprss.DataCodeBooks("SP_ENTRY_CODEBOOK02", comcod, "RPTOTHERACCOUNTCODEBook", "", tempddl2);
            //ReportDocument rptAccCode = new RealERPRPT.R_17_Acc.rptOthersAccCode();
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
            //    string eventtype = "Sales Payment Code Book";
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
                    this.lbalterofddl.Text = "Code Book: " + this.ddlSalPayment.SelectedItem.ToString().Trim();
                    // + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    this.lbalterofddl0.Text = this.ddlOthersBookSegment.SelectedItem.ToString().Trim();

                    this.ShowInformation();
                    this.gvPaySch_DataBind();

                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                }
            }
            else
            {
                this.lnkok.Text = "OK";
                this.lnkok.Visible = true;
                //this.lnkcancel.Visible = false;
                //this.LblBookName1.Visible = true;
                this.lbalterofddl.Visible = false;
                this.lbalterofddl0.Visible = false;
                this.ddlSalPayment.Visible = true;
                this.ddlOthersBookSegment.Visible = true;

                this.gvPaySch.DataSource = null;
                this.gvPaySch.DataBind();
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlSalPayment.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SHOWGENCODE", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
        }
        //protected void lnkcancel_Click(object sender, EventArgs e)
        //{
        //    this.lnkok.Visible = true;
        //    this.lnkcancel.Visible = false;
        //    this.LblBookName1.Visible = true;
        //    this.lbalterofddl.Visible = false;
        //    this.ddlSalPayment.Visible = true;
        //    this.ddlOthersBookSegment.Visible = true;

        //    this.gvPaySch.DataSource = null;
        //    this.gvPaySch.DataBind();
        //}

    }
}
