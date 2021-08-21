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
namespace RealERPWEB.F_17_Acc
{
    public partial class AccSpecificCodeBook : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Specification Code";
                this.Master.Page.Title = "Specification Code";
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetspecificationCode();
                this.GetGroup();
                CommonButton();
            }

        }
        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        protected void GetspecificationCode()
        {

            try
            {

                string comcod = this.GetComeCode();
                DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTCODE", "", "", "", "", "", "", "", "", "");
                this.ddlMainBook.DataTextField = "spcfdesc";
                this.ddlMainBook.DataValueField = "spcfcod";
                this.ddlMainBook.DataSource = ds1.Tables[0];
                this.ddlMainBook.DataBind();
                ds1.Dispose();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }


        private void GetGroup()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPCFGROUP", "", "", "", "", "", "", "", "", "");
            this.ddlGroup.DataTextField = "spcfdesc";
            this.ddlGroup.DataValueField = "spcfcod";
            this.ddlGroup.DataSource = ds1.Tables[0];
            this.ddlGroup.DataBind();
            ds1.Dispose();


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
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            try
            {
                string comcod = this.GetComeCode();
                string sircode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
                string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
                string sircode = "";
                if (sircode1.Length == 5)
                {
                    sircode = sircode2 + sircode1;
                }

                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                this.grvacc.EditIndex = -1;
                bool result = false;
                result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, "", "", "", "", "", "", "", "", "", "", "", "");
                if (result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                    if (ConstantInfo.LogStatus == true)
                    {
                        string eventtype = "Specification CodeBook";
                        string eventdesc = "Update CodeBook";
                        string eventdesc2 = sircode;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                    }
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
                this.ShowInformation();
            }

            catch (Exception ex)
            {


            }
        }

        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["storedata"];
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();

        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.grvacc_DataBind();
        }

        protected void lnkcancel_Click(object sender, EventArgs e)
        {



        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";

                    this.ddlMainBook.Enabled = false;
                    this.ddlCodeSegment.Enabled = false;


                    this.ibtnSrch.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ibtnGroup.Visible = true;
                    this.ShowInformation();
                }


                else
                {

                    this.lnkok.Text = "Ok";
                    this.ddlMainBook.Enabled = true;
                    this.ddlCodeSegment.Enabled = true;

                    this.ibtnSrch.Visible = false;
                    this.ibtnGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
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

        private void ShowInformation()
        {






            string comcod = this.GetComeCode();
            Session.Remove("storedata");
            string srchoptionmain = (this.ddlGroup.SelectedValue == "000000000" ? "" : this.ddlGroup.SelectedValue.ToString()) + "%";

            string srchoption = (srchoptionmain.Contains("-")) ? srchoptionmain : srchoptionmain + "%";
            if (srchoption.Contains("-"))
            {
                //int index = srchoption.IndexOf("-");
                //srchoption = srchoptionmain.Substring(0, index);
                //srchoption1 = srchoptionmain.Substring(index+1);
                int index, index01;
                if (srchoption.Contains(","))
                {
                    index = srchoption.IndexOf(",");
                    index01 = srchoption.IndexOf("-");
                    srchoption = "sircode like '" + srchoptionmain.Substring(0, 1) + "[" + srchoptionmain.Substring(1, 1) + "-" + srchoptionmain.Substring(index01 + 2, 1) + "]%'";
                    srchoption = srchoption + " or " + "sircode like '" + srchoptionmain.Substring(index + 1) + "%'";
                }
                else
                {
                    index01 = srchoption.IndexOf("-");
                    srchoption = "sircode like '" + srchoptionmain.Substring(0, 1) + "[" + srchoptionmain.Substring(1, 1) + "-" + srchoptionmain.Substring(index01 + 2, 1) + "]%'";

                }

            }

            string tempddl1 = (this.ddlMainBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlCodeSegment.SelectedValue.ToString().Trim();

            tempddl1 = (tempddl1 == "00") ? "" : tempddl1;
            string Calltype = (srchoptionmain.Contains("-")) ? "OACCOUNTBTWNCINFO" : "SPACCOUNTINFO";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", Calltype, tempddl1,
                                  tempddl2, srchoption, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();



            //string dd1value = "01";
            //string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
            //string txtSearch = this.txtsrch.Text.Trim() + "%";
            //DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTINFO", dd1value,
            //                dd2value, txtSearch, "", "", "", "", "", "");
            //Session["storedata"] = ds1.Tables[0];
            //this.grvacc_DataBind();


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds1 = Rprss.DataCodeBooks("SP_REPORT_CODEBOOK", comcod, "RPTSPECIFICATIONCODE", "", "");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptSpecification();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            //TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text =comadd;
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Specification CodeBook";
                string eventdesc = "Print CodeBook";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptstk.SetDataSource(ds1.Tables[0]);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
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
        protected void ibtnGroup_Click(object sender, EventArgs e)
        {
            this.GetGroup();
        }
        protected void imgSearchSrchResouce_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnok_Click(object sender, EventArgs e)
        {

        }
        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {




                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod")).ToString();

                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 3) == "000" && ASTUtility.Right(Code, 5) != "00000")
                {

                    //e.Row.ForeColor = System.Drawing.Color.White;
                    //e.Row.BackColor = System.Drawing.Color.Blue;
                    // lbtnACTDESC.ForeColor = System.Drawing.Color.White;

                    // e.Row.BackColor = System.Drawing.Color.Blue; 

                    e.Row.Attributes["style"] = "background-color:#3399FF; font-weight:bold;";
                    ////hlnkgvdesc.Style.Add("color", "blue");
                    //string sirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();
                    //hlnkgvdesc.NavigateUrl = "~/F_17_Acc/LinkSpecificCodeBook.aspx?sircode=" + Code + "&sirdesc=" + sirdesc;



                }






            }


        }
    }
}
