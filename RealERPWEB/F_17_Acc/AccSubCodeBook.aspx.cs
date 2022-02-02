using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
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
namespace RealERPWEB.F_17_Acc
{
    public partial class AccSubCodeBook : System.Web.UI.Page
    {
        protected FullGridPager fullGridPager;
        protected int MaxVisible = 0;
        static string prevPage = String.Empty;
       // ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        static string[] CarArray = new string[3] { "Sub Code-1", "Sub Code-2", "Details Code" };
        //static string tempddl1 = "", tempddl2 = "";
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            //if (IsPostBack)
            //{


            //    fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
            //    fullGridPager.CreateCustomPager(grvacc.BottomPagerRow);
            //}


            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);//"Resource Code";

               // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));  //"Resource Code"
                //string title = (this.Request.QueryString["InputType"].ToString() == "res") ? "Resource Code"
                //    : (this.Request.QueryString["InputType"].ToString() == "Overhead") ? "Design & Consultancy"
                //    : (this.Request.QueryString["InputType"].ToString() == "Wrkschedule") ? "Work List"
                //    : (this.Request.QueryString["InputType"].ToString() == "Employee") ? "Employee Code"
                //    : (this.Request.QueryString["InputType"].ToString() == "DeptCode") ? "Department Code"
                //    : (this.Request.QueryString["InputType"].ToString() == "Supplier") ? "Supplier Code"
                //    : (this.Request.QueryString["InputType"].ToString() == "UnitCode") ? "New Unit Code" : "Sub -Contractor Code";


                //((Label)this.Master.FindControl("lblTitle")).Text = title;
                //this.Master.Page.Title = title;
                CommonButton();
              
            }
            //((Label)this.Master.FindControl("lblmsg")).Text= "";
        }
        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //Commented
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


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

        protected void Load_CodeBooList()
        {

            try
            {

                string Querytype = this.Request.QueryString["InputType"];

                string coderange = (Querytype == "res") ? "sircode like '[0-9]%'" : (Querytype == "Overhead") ? "sircode like '0[89]%'  or  sircode like '1[0-9]%' or sircode like '20%'"
                   : (Querytype == "Assets") ? "sircode like '2[1-9]%'" : (Querytype == "Liabilities") ? "sircode like '31%'" : (Querytype == "HOverhead") ? "sircode like '32%'"
                   : (Querytype == "Wrkschedule") ? "sircode like '4[1-5]%'" : (Querytype == "UnitCode") ? "sircode like '5[1-9]%'" : (Querytype == "customer") ? "sircode like '6[1-9]%'"
                   : (Querytype == "Subcontractor") ? "sircode like '98%'" : (Querytype == "ResCodePrint") ? "sircode like '99%'" : (Querytype == "Supplier") ? "sircode like '99%'" : (Querytype == "Mat") ? "sircode like '01'"
                   : (Querytype == "TaxVatAndSd") ? "sircode like '97%'" : (Querytype == "GenAdv") ? "sircode like '9[56]%'" : (Querytype == "Labour") ? "sircode like '04%'" : (Querytype == "Materials") ? "sircode like '01%'"
                   : (Querytype == "Employee") ? "sircode like '93%'" : (Querytype == "DeptCode") ? "sircode like '94%'" : "sircode like '%'";




                //string coderange = (Querytype == "Res") ? "sircode like '0[1-7]%'" : (Querytype == "Overhead") ? "sircode like '0[89]%'  or  sircode like '1[0-9]%' or sircode like '20%'"
                //    : (Querytype == "Assets") ? "sircode like '2[1-9]%'" : (Querytype == "Liabilities") ? "sircode like '31%'" : (Querytype == "HOverhead") ? "sircode like '32%'"
                //    : (Querytype == "Wrkschedule") ? "sircode like '41%'" : (Querytype == "UnitCode") ? "sircode like '5[1-9]%'" : (Querytype == "customer") ? "sircode like '6[1-9]%'"
                //    : (Querytype == "Subcontractor") ? "sircode like '98%'" : (Querytype == "ResCodePrint") ? "sircode like '%'"
                //    : (Querytype == "TaxVatAndSd") ? "sircode like '97%'" : (Querytype == "GenAdv") ? "sircode like '9[56]%'" : "sircode like '99%'";

                // string coderange = "sircode like  '99%'";
                //string coderange = "sircode like  '%'";
                string comcod = this.GetComeCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string AllRes = (Querytype == "ResCodePrint") ? "ALL" : "";
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTCODE", coderange, "", userid, "", "", "", "", "", "");
                if (dsone.Tables[0].Rows.Count == 0)
                {
                    dsone.Tables[0].Rows.Add(comcod, "----Have No Code Permission Please Contact Sys Admin----", "XXXXXXXXXXXX");

                }
                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();

                this.grvacc.Columns[10].Visible = (Querytype == "DeptCode") ? true : false;
            }
            catch (Exception ex)
            {
                msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
                //((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void GetResCodeleb2()
        {
            Session.Remove("tblresleb2");
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETRESCODELEVEL2", "", "", userid, "", "", "", "", "", "");
            Session["tblresleb2"] = ds1.Tables[0];
            ds1.Dispose();

        }
        private void SelectResCodeLeb2()
        {
            DataTable dt = ((DataTable)Session["tblresleb2"]).Copy();
            if (this.ddlOthersBook.SelectedValue.ToString().Length == 0)
                return;

            string mrescode = this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2);
            EnumerableRowCollection<DataRow> item = (from r in dt.AsEnumerable()
                                                     where (r.Field<string>("sircode").Substring(0, 2) == mrescode || r.Field<string>("sircode").Substring(0, 2) == "00")
                                                     select r);
            dt = item.AsDataView().ToTable();

            this.ddlcatagory.DataTextField = "sirdesc";
            this.ddlcatagory.DataValueField = "sircode";
            this.ddlcatagory.DataSource = dt;
            this.ddlcatagory.DataBind();
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
            string sircode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbllgrcode")).Text.Trim();
            string sircode2 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim();
            string sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            string actcode = ((DataTable)Session["storedata"]).Rows[rowindex]["actcode"].ToString();
            // string teamcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["catcode"].ToString();

            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlProName");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            Panel pnl02 = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("Panel2");
            if (sircode.Substring(0, 2) == "94" && (ASTUtility.Right(sircode, 3) != "000"))
            {
                ViewState["gindex"] = e.NewEditIndex;


                string SearchProject = "%"; //+ ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
                DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETHEADANDDEPT", SearchProject, "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "actdesc";
                ddl2.DataValueField = "actcode";
                ddl2.DataSource = ds1;
                ddl2.DataBind();
                ddl2.SelectedValue = actcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnl02.Visible = true;
            }
            else
            { 
                pnl02.Visible = false;
                ddl2.Items.Clear();

            }

            TextBox txtUnit = (TextBox)this.grvacc.Rows[e.NewEditIndex].FindControl("txtgvsirunit");
            DropDownList ddlUnit = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlUnit");
            if  (sircode.Substring(0, 2) == "41")
            {
                 
                DataSet ds1 = da.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GET_UNIT_NAME", "", "", "", "", "", "", "", "", "");
                ddlUnit.DataTextField = "gdesc";
                ddlUnit.DataValueField = "gcod";
                ddlUnit.DataSource = ds1;
                ddlUnit.DataBind();
                ddlUnit.SelectedItem.Text = txtUnit.Text; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                ddlUnit.Visible = true;
                txtUnit.Visible = false;
            }
            else
            {
                ddlUnit.Visible = false;
                txtUnit.Visible = true; 
            }


            //int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;


        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
               
            }
            try
            {

                string comcod = this.GetComeCode();
                string sircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbllgrcode")).Text.Trim();
                string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string sircode = "";
                bool updateallow = true;//01-001-01-001

                if (sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
                }
                else
                {
                    //((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    msg = "Invalid code!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    updateallow = false;
                    return;


                   
                }

                string actcode = "";


                //if((ASTUtility.Left(sircode,2)=="99") && (ASTUtility.Right(sircode, 3) != "000"))          
                actcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlProName")).SelectedValue.ToString();
                // string actdesc = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlProName")).Text;





                string Descbn = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDescbn")).Text.Trim();
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtsirtype = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridsirtype")).Text.Trim();
                string txtsirtdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
                string txtsirunit = ((ASTUtility.Left(sircode, 2) == "41") ? ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlUnit")).SelectedItem.Text.Trim() : ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirunit")).Text.Trim());
                string txtsirval = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim()).ToString();
                string psircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                string unitCode = ((ASTUtility.Left(sircode, 2) == "41") ? ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlUnit")).SelectedValue.Trim() : "");

                DataTable tbl1 = (DataTable)Session["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

                if (tempddl2 == "4" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) != psircode1.Substring(2, 3))
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";

                        msg = "Update Not Allowed";
                        updateallow = false;

                    }
                    else if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";

                        msg = "Update Not Allowed";

                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";

                        msg = "Update Not Allowed";

                        updateallow = false;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                }
                else if (tempddl2 == "7" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";

                        msg = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";

                        msg = "Update Not Allowed";
                        updateallow = false;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                }
                else if (tempddl2 == "9" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {

                    if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3) || sircode1.Substring(3, 3) == "000")
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        msg = "Update Not Allowed";
                        updateallow = false;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                }
                else if (tempddl2 == "12" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) == "000" && sircode1.Substring(7, 2) != "00" || sircode1.Substring(7, 2) == "00" && sircode1.Substring(10, 3) != "000")
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        msg = "Update Not Allowed";
                        updateallow = false;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                   
                }


                if (updateallow)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string userid = hst["usrid"].ToString();

                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;


                    //txtsirval = "0" + txtsirval;
                    //tbl1.Rows[Index]["SIRCODE"] = sircode;
                    //tbl1.Rows[Index]["SIRDESC"] = Desc;
                    //tbl1.Rows[Index]["SIRTYPE"] = txtsirtype;
                    //tbl1.Rows[Index]["SIRTDES"] = txtsirtdesc;
                    //tbl1.Rows[Index]["SIRUNIT"] = txtsirunit;
                    //tbl1.Rows[Index]["SIRVAL"] = Convert.ToDecimal(txtsirval);
                    //tbl1.Rows[Index]["actcode"] = actcode;
                    //tbl1.Rows[Index]["actdesc"] = actdesc;


                    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, actcode, Descbn,
                        unitCode, "", "", "", "");
                    this.ShowInformation();
                    if (result)
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                        msg = "Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                    }
                    else
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                        msg = "Parent Code Not Found!!!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    }
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();

                }
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Account Sub-CodeBook";
                    string eventdesc = "Update Sub-CodeBook";
                    string eventdesc2 = sircode;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            catch (Exception ex)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

                
                if(this.Request.QueryString["InputType"] == "DeptCode")
                {
                    this.grvacc.FooterRow.Visible = true;
                }
                else
                {
                    this.grvacc.FooterRow.Visible = false;
                }

                //((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Visible = false;
                //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                //((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Items.Clear();
                //for (int i = 1; i <= TotalPage; i++)
                //    ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                //if (TotalPage > 1)
                //    ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).Visible = true;
                //((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;                        
            }
            catch (Exception ex)
            {
            }
        }

        //protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.grvacc.PageIndex = ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex;
        //        this.grvacc.EditIndex = -1;                         
        //        this.grvacc_DataBind();
        //    }
        //    catch
        //    {
        //    }
        //}



        //protected void lnkPrint_Click(object sender, EventArgs e)
        //{
        //    if (this.lnkok.Visible)
        //        this.lnkok_Click(null, null);

        //    string CodeDesc = this.ddlOthersBook.SelectedItem.ToString().Trim().Substring(3)
        //                + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //    DataTable Dtable = (DataTable)Session["storedata"];
        //    ReportDocument rptAccCode = new RealERPRPT.R_17_Acc.rptOthersAccCode();
        //    TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
        //    txtCompany.Text = comnam;
        //    TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
        //    rpttxtNameR.Text = CodeDesc;
        //    TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Account Sub-CodeBook";
        //        string eventdesc = "Print Sub-CodeBook";
        //        string eventdesc2 = "";
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }

        //    rptAccCode.SetDataSource(Dtable);
        //    Session["Report1"] = rptAccCode;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";    
        //}


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {
                case "3338":
                    this.PrintResCodeAcme();
                    break;

                default:
                    this.PrintResCodeAll();
                    break;
            }

        }

        private void PrintResCodeAll()
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
            DataTable dt = (DataTable)Session["storedata"];

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.CodeBookInfo>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptOthersAccCode", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("codeDesc", CodeDesc));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintResCodeAcme()
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
            DataTable dt = (DataTable)Session["storedata"];

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.CodeBookInfo>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccCodeBookAcme", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("codeDesc", CodeDesc));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";

                    this.LblBookName1.Text = "Code Book:";
                    this.ddlOthersBook.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.lbalterofddl.Text = this.ddlOthersBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
                    string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
                    grvacc.Columns[7].HeaderText = (tempddl1 == "01") ? "Std.Rate" : (tempddl1 == "02") ? "Std.Rate"
                                : (tempddl1 == "03") ? "Std.Rate" : (tempddl1 == "04") ? "Std.Rate" : (tempddl1 == "41") ? "Qty. Considered" : "";

                    this.GetBaseUnit();
                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";

                    this.LblBookName1.Text = "Select Code Book:";
                    this.ibtnSrch.Visible = false;
                    this.lbalterofddl.Visible = false;
                    this.lbalterofddl0.Visible = false;
                    this.ddlOthersBook.Visible = true;
                    this.ddlOthersBookSegment.Visible = true;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }

            }
            catch (Exception ex)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                msg = "Information not found!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;

            }
        }


        private void GetBaseUnit()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();



        }
        private void ShowInformation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            Session.Remove("storedata");
            string srchoptionmain = this.txtsrch.Text.Trim();
            //string srchoption1 = "";
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

            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            tempddl1 = (tempddl1 == "00") ? "" : tempddl1;
            string Calltype = (srchoptionmain.Contains("-")) ? "OACCOUNTBTWNCINFO" : "OACCOUNTINFO";
            string catgrory = ((this.ddlcatagory.SelectedValue.ToString() == "0000") ? "" : this.ddlcatagory.SelectedValue.ToString()) + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", Calltype, tempddl1,
                                  tempddl2, srchoption, catgrory, userid, "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }


            //fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
            //fullGridPager.CreateCustomPager(grvacc.BottomPagerRow);
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

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


        protected void DDLPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = grvacc.BottomPagerRow;
            //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");

            //grvacc.PageIndex = DDLPage.SelectedIndex;
            //grvacc.DataBind();
            // this.grvacc_DataBind();
        }
        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                e.Row.Cells[2].ToolTip = "Edit Information";
                LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd");
                int index = e.Row.RowIndex;
                int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + index;
                DataTable dt = ((DataTable)Session["storedata"]);

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                int additem = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "additem"));

                if (Code == "")
                    return;

                if (ASTUtility.Left(Code, 2) == "04" || ASTUtility.Left(Code, 2) == "41" || ASTUtility.Left(Code, 2) == "41")

                {

                    LinkButton lbtnDetails = (LinkButton)e.Row.FindControl("lbtnDetails");

                    if (ASTUtility.Right(Code, 3) != "000")
                    {
                        lbtnDetails.Visible = true;
                    }
                    else
                    {
                        lbtnDetails.Visible = false;

                    }
                }


                

                if (ASTUtility.Right(Code, 8) == "00000000" && ASTUtility.Right(Code, 10) != "0000000000")
                {

                    //e.Row.ForeColor = System.Drawing.Color.White;
                    //e.Row.BackColor = System.Drawing.Color.Blue;
                    // lbtnACTDESC.ForeColor = System.Drawing.Color.White;

                    // e.Row.BackColor = System.Drawing.Color.Blue; 

                    e.Row.Attributes["style"] = "background-color:gray; font-weight:bold;";
                    ////hlnkgvdesc.Style.Add("color", "blue");
                    //string sirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();
                    //hlnkgvdesc.NavigateUrl = "~/F_17_Acc/LinkSpecificCodeBook.aspx?sircode=" + Code + "&sirdesc=" + sirdesc;



                }
                //
                else if (ASTUtility.Right(Code, 3) == "000" && ASTUtility.Right(Code, 5) != "00000")
                {

                    e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";

                }

                // For Add
                if (additem == 1)
                {

                    lbtnAdd.Visible = true;


                }


            }




            //GridViewRow row = this.grvacc.BottomPagerRow;

            //if (row == null)
            //{
            //    return;
            //}

            //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            //Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            //Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            ////if (lblPages != null)
            ////{
            //lblPages.Text = grvacc.PageCount.ToString();
            ////}

            ////if (lblCurrent != null)
            ////{
            //int currentPage = grvacc.PageIndex + 1;
            //lblCurrent.Text = currentPage.ToString();
            ////}

            //if (DDLPage != null)
            //{
            //    for (int i = 0; i < grvacc.PageCount; i++)
            //    {
            //        int pageNumber = i + 1;
            //        ListItem item = new ListItem(pageNumber.ToString());
            //        if (i == grvacc.PageIndex)
            //        {
            //            item.Selected = true;
            //        }
            //        DDLPage.Items.Add(item);
            //    }
            //}

            ////-- For First and Previous ImageButton
            //if (grvacc.PageIndex == 0)
            //{
            //    ((ImageButton)grvacc.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            //    ((ImageButton)grvacc.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            //    ((ImageButton)grvacc.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            //    ((ImageButton)grvacc.BottomPagerRow.FindControl("btnPrev")).Visible = false;

            //    //--- OR ---\\
            //    //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
            //    //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
            //    //btnFirst.Visible = false;
            //    //btnPrev.Visible = false;

            //}

            ////-- For Last and Next ImageButton
            //if (grvacc.PageIndex + 1 == grvacc.PageCount)
            //{
            //    ((ImageButton)grvacc.BottomPagerRow.FindControl("btnLast")).Enabled = false;
            //    ((ImageButton)grvacc.BottomPagerRow.FindControl("btnLast")).Visible = false;

            //    ((ImageButton)grvacc.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            //    ((ImageButton)grvacc.BottomPagerRow.FindControl("btnNext")).Visible = false;

            //    //--- OR ---\\
            //    //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
            //    //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
            //    //btnLast.Visible = false;
            //    //btnNext.Visible = false;
            //}



        }

        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[rowindex].FindControl("ddlProName");
            string SearchProject = "%";
            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_LP_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            //ddl2.SelectedValue = actcode;

        }
        protected void ibtnSrcurClick(object sender, ImageClickEventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //int rowindex = (int)ViewState["gindex"];
            ////string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            //DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[rowindex].FindControl("ddlProName");
            //string SearchProject = "%" + ((TextBox)gvCodeBook.Rows[rowindex].FindControl("txtSerachProject")).Text.Trim() + "%";
            //DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            //ddl2.DataTextField = "actdesc1";
            //ddl2.DataValueField = "actcode";
            //ddl2.DataSource = ds1;
            //ddl2.DataBind();
            //ddl2.SelectedValue = actcode;
        }

        protected void lbtnUpdateDetails_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string rsircode = this.txtrsircode.Text;
            string sdetails = this.txtDetails.Text.Trim();
            //  string catagory = this.ddlworkCatagory.SelectedValue.ToString();

            bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "UPDATESDETAILS", rsircode, sdetails, "", "", "", "", "", "");

            if (!result)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                msg = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;


            }

            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);


        }
        protected void lbtnDetails_Click(object sender, EventArgs e)

        {

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            string rsircode = ((DataTable)Session["storedata"]).Rows[RowIndex]["sircode"].ToString();
            this.txtrsircode.Text = rsircode;
            this.GetDetailsInfo(rsircode);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);

        }

        private void GetDetailsInfo(string rsircode)
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETRESOURCEDETEAILS", rsircode, "", "", "", "", "", "", "", "");
            this.txtDetails.Text = ds1.Tables[0].Rows[0]["sdetails"].ToString();
        }
        protected void ddlOthersBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectResCodeLeb2();
        }
        protected void ddlPageGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fullGridPager == null)
            {
                fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
            }
            fullGridPager.PageGroupChanged(grvacc.BottomPagerRow);
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

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {



            
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                
                return;
            }

            try
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                int index = this.grvacc.PageSize * this.grvacc.PageIndex + RowIndex;

                string sircode = ((DataTable)Session["storedata"]).Rows[index]["sircode"].ToString();
                string actcode = ((DataTable)Session["storedata"]).Rows[index]["actcode"].ToString();
                this.lblsircode.Text = sircode;
                this.txtresourcecode.Text = sircode.Substring(0, 2) + "-" + sircode.Substring(2, 2) + "-" + sircode.Substring(4, 3) + "-" + sircode.Substring(7, 2) + "-" + ASTUtility.Right(sircode, 3);

                this.Chboxchild.Checked = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");
                this.chkbod.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");
                this.lblchild.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");



                // Project Link

                if (sircode.Substring(0, 2) == "94" && (ASTUtility.Right(sircode, 3) != "000"))
                {

                  
                    string SearchProject = "%"; //+ ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
                    DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETHEADANDDEPT", SearchProject, "", "", "", "", "", "", "", "");
                    this.ddlProject.DataTextField = "actdesc";
                    this.ddlProject.DataValueField = "actcode";
                    this.ddlProject.DataSource = ds1;
                    this.ddlProject.DataBind();
                    this.ddlProject.SelectedValue = actcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();

                }
                else
                {
                    this.ddlProject.Items.Clear();
                    this.lblddlproject.Visible = false;
                    this.ddlProject.Visible = false;
                }

                if (sircode.Substring(0, 2) == "41")
                {
                    this.lblsdrate.InnerText = "Standard  Qty";
                    DataSet ds1 = da.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GET_UNIT_NAME", "", "", "", "", "", "", "", "", "");
                    ddlUnits.DataTextField = "gdesc";
                    ddlUnits.DataValueField = "gcod";
                    ddlUnits.DataSource = ds1;
                    ddlUnits.DataBind();
                    ddlUnits.SelectedValue = actcode; 
                    ddlUnits.Visible = true;
                    txtunit.Visible = false;
                }
                else
                {
                    this.lblsdrate.InnerText = "Standard  Rate";
                    ddlUnits.Visible = false ;
                    txtunit.Visible = true;
                }




                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
            }


            catch (Exception ex)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);



            }
        }

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {

         
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string isircode = this.lblsircode.Text.Trim();
                string tsircode = this.txtresourcecode.Text.Trim().Replace("-", "");
                string sircode = (this.Chboxchild.Checked) ? ((ASTUtility.Right(isircode, 8) == "00000000") ? (ASTUtility.Left(isircode, 4) + "001" + ASTUtility.Right(isircode, 5))
                    : ((ASTUtility.Right(isircode, 5) == "00000" && ASTUtility.Right(isircode, 8) != "00000000") ? (ASTUtility.Left(isircode, 7) + "01" + ASTUtility.Right(isircode, 3)) : ASTUtility.Left(isircode, 9) + "001"))
                    : ((isircode != tsircode) ? tsircode : isircode);
                string mnumber = (isircode == tsircode) ? "" : "manual";

                string Desc = this.txtresourcehead.Text.Trim();
                string DescBN = this.txtresourceheadBN.Text.Trim();

                string txtsirtype = "";
                string txtsirtdesc = "";
                string txtsirunit = (sircode.Substring(0, 2) == "41" ? this.ddlUnits.SelectedItem.ToString() : this.txtunit.Text.ToString()) ;
                string valusirunit = this.ddlUnits.SelectedValue.ToString();
                

                string txtsirval = Convert.ToDouble("0" + this.txtstdrate.Text.Trim()).ToString();
                string actcode = this.ddlProject.Items.Count == 0 ? "" : this.ddlProject.SelectedValue.ToString();
                string txtTDetails = this.txtTDetails.Text.Trim();
                // return;

                if (Desc.Length == 0)
                {

                    //((Label)this.Master.FindControl("lblmsg")).Text = "Resource Head is not empty";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    msg = "Resource Head is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {



                    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDRESOUCECODE",
                        sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, actcode, mnumber,
                      DescBN, valusirunit, txtTDetails, "", "");

                    if (!result)
                    {

                        //((Label)this.Master.FindControl("lblmsg")).Text = da.ErrorObject["Msg"].ToString();
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                        msg = da.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                        return;

                    }


                    //((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                    msg = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);



                    this.ShowInformation();
                    this.Chboxchild.Checked = false;

                }




            }
            catch (Exception ex)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);


            }

        }

        protected void lnkPageloadData_Click(object sender, EventArgs e)
        {
            if (this.ddlOthersBook.Items.Count == 0)
            {
                this.Load_CodeBooList();
                this.GetResCodeleb2();
                this.SelectResCodeLeb2();
            }
        }

      

        protected void lnkbtnDeptSeqUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            bool result = false;

            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                string sircode = ((Label)this.grvacc.Rows[i].FindControl("lbllgrcodefull")).Text.ToString();
                string seq = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvseq")).Text.Trim().ToString();


                result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "UPDATESEQ", sircode, seq, "", "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    msg = "Update Failed ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

            }

            msg = "Updated Successfully ";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
        }
    }
}
