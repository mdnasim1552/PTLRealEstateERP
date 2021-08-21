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
//using  RealERPRPT;
namespace RealERPWEB.F_64_Mgt
{
    public partial class AccSubCodeBook : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        static string[] CarArray = new string[3] { "Sub Code-1", "Sub Code-2", "Details Code" };
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            ((Label)this.Master.FindControl("lblTitle")).Text = "Resource Code";

            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            //this.ConfirmMessage.Text = "";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Load_CodeBooList()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            try
            {

                string Querytype = this.Request.QueryString["InputType"];
                //string coderange = (Querytype == "Res") ? "sircode like '0[1-7]%'" : (Querytype == "Overhead") ? "sircode like '0[89]%'  or  sircode like '1[0-9]%' or sircode like '20%'"
                //    : (Querytype == "Assets") ? "sircode like '2[1-9]%'" : (Querytype == "Liabilities") ? "sircode like '31%'" : (Querytype == "HOverhead") ? "sircode like '32%'"
                //    : (Querytype == "Wrkschedule") ? "sircode like '41%'" : (Querytype == "UnitCode") ? "sircode like '5[1-9]%'" : (Querytype == "customer") ? "sircode like '6[1-9]%'"
                //    : (Querytype == "Subcontractor") ? "sircode like '98%'": (Querytype == "ResCodePrint") ? "sircode like '%'"
                //    : (Querytype == "TaxVatAndSd") ? "sircode like '97%'" : (Querytype == "GenAdv") ? "sircode like '9[56]%'" : "sircode like '99%'";

                string coderange = "sircode like  '%'";
                string comcod = this.GetComeCode();
                string AllRes = (Querytype == "ResCodePrint") ? "ALL" : "";
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTCODE", coderange, AllRes, "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();

                this.grvacc.Columns[9].Visible = (Querytype == "Supplier") ? true : false;
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
            this.ConfirmMessage.Visible = false;
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();


            string sircode1 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim();
            string sircode2 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim();
            string sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
            int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;

            string actcode = ((DataTable)Session["storedata"]).Rows[rowindex]["curcode"].ToString();


            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlCur");
            Panel pnl02 = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("Panel2");
            if ((ASTUtility.Left(sircode, 2) == "99") && (ASTUtility.Right(sircode, 3) != "000"))
            {
                ViewState["gindex"] = e.NewEditIndex;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string SearchCur = "%" + ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtSerachcur")).Text.Trim() + "%";
                DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETCURRENCY", SearchCur, "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "codedesc";
                ddl2.DataValueField = "code";
                ddl2.DataSource = ds1;
                ddl2.DataBind();
                ddl2.SelectedValue = actcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnl02.Visible = true;
                //ddl2.Visible = true;
            }
            else
            {
                pnl02.Visible = false;
                ddl2.Items.Clear();
            }
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.ConfirmMessage.Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetComeCode();
                string sircode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string sircode = "";
                bool updateallow = true;//01-001-01-001

                if (sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
                    updateallow = false;
                }

                string curcode = "";


                if ((ASTUtility.Left(sircode, 2) == "99") && (ASTUtility.Right(sircode, 3) != "000"))
                    curcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlCur")).SelectedValue.ToString();





                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtsirtype = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridsirtype")).Text.Trim();
                string txtsirtdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
                string txtsirunit = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirunit")).Text.Trim();
                string txtsirval = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim()).ToString();
                string psircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();

                DataTable tbl1 = (DataTable)Session["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

                if (tempddl2 == "4" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) != psircode1.Substring(2, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "7" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "9" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {

                    if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3) || sircode1.Substring(3, 3) == "000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }

                }
                else if (tempddl2 == "12" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) == "000" && sircode1.Substring(7, 2) != "00" || sircode1.Substring(7, 2) == "00" && sircode1.Substring(10, 3) != "000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }


                if (updateallow)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string userid = hst["usrid"].ToString();

                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;


                    txtsirval = "0" + txtsirval;
                    tbl1.Rows[Index]["SIRCODE"] = sircode;
                    tbl1.Rows[Index]["SIRDESC"] = Desc;
                    tbl1.Rows[Index]["SIRTYPE"] = txtsirtype;
                    tbl1.Rows[Index]["SIRTDES"] = txtsirtdesc;
                    tbl1.Rows[Index]["SIRUNIT"] = txtsirunit;
                    tbl1.Rows[Index]["SIRVAL"] = Convert.ToDecimal(txtsirval);


                    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, curcode, "",
                        "", "", "", "", "");
                    this.ShowInformation();
                    if (result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
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
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
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



        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //if (this.lnkok.Visible)
            //    this.lnkok_Click(null, null);

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
            //ReportDocument rptAccCode = new  RealERPRPT.R_17_Acc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Account Sub-CodeBook";
            //    string eventdesc = "Print Sub-CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";    
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    this.ConfirmMessage.Visible = false;
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

                    //if (tempddl1 == "01")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Description of Code";
                    //    grvacc.Columns[5].Visible = true;
                    //    grvacc.Columns[6].Visible = true;
                    //    grvacc.Columns[5].HeaderText = "Unit";
                    //    grvacc.Columns[6].HeaderText = "Std.Rate";
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    //else if (tempddl1 == "02")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Description of Code";
                    //    grvacc.Columns[5].Visible = true;
                    //    grvacc.Columns[6].Visible = true;
                    //    grvacc.Columns[5].HeaderText = "Unit";
                    //    grvacc.Columns[6].HeaderText = "Std.Rate";
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    //else if (tempddl1 == "03")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Description of Code";
                    //    grvacc.Columns[5].Visible = true;
                    //    grvacc.Columns[6].Visible = true;
                    //    grvacc.Columns[5].HeaderText = "Unit";
                    //    grvacc.Columns[6].HeaderText = "Std.Rate";
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    //else if (tempddl1 == "04")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Description of Code";
                    //    grvacc.Columns[5].Visible = true;
                    //    grvacc.Columns[6].Visible = true;
                    //    grvacc.Columns[5].HeaderText = "Unit";
                    //    grvacc.Columns[6].HeaderText = "Std.Rate";
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    //else if (tempddl1 == "41")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Item Code";
                    //    grvacc.Columns[4].HeaderText = "Description of Code";
                    //    grvacc.Columns[5].Visible = true;
                    //    grvacc.Columns[6].Visible = true;
                    //    grvacc.Columns[5].HeaderText = "Unit";
                    //    grvacc.Columns[6].HeaderText = "Std.Qty";
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    //else if (tempddl1 == "81")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Contractor Name";
                    //    grvacc.Columns[5].Visible = false;
                    //    grvacc.Columns[6].Visible = false;
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    //else if (tempddl1 == "83")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Supplier/Purchaser Name";
                    //    grvacc.Columns[5].Visible = false;
                    //    grvacc.Columns[6].Visible = false;
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";
                    this.ConfirmMessage.Visible = false;
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
                ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
            }
        }

        private void ShowInformation()
        {
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
        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HyperLink hlnkgvdesc = (HyperLink)e.Row.FindControl("hlnkgvdesc");



            //    string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();

            //    if (Code == "")
            //        return;

            //    if (ASTUtility.Right(Code, 3)!= "000")
            //    {
            //        //hlnkgvdesc.Style.Add("color", "blue");
            //        string sirdesc= Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();
            //        hlnkgvdesc.NavigateUrl = "~/F_17_Acc/LinkSpecificCodeBook.aspx?sircode=" + Code + "&sirdesc=" + sirdesc;



            //    }




            //}
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
    }
}
