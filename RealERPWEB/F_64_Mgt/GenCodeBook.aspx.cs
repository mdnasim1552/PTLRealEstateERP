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
    public partial class GenCodeBook : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        static string[] CarArray = new string[3] { "Sub Code-1", "Sub Code-2", "Details Code" };
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Discussion Field Code";


                this.Load_CodeBooList();

                this.GetTeamCode();
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "";

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

        private void GetTeamCode()
        {
            string comcod = this.GetComeCode();
            string srchoption = "%%";
            DataSet dsone = this.da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "GETTEAMNAME", srchoption, "", "", "", "", "", "", "", "");
            ViewState["tblteam"] = dsone.Tables[0];
            dsone.Dispose();

        }

        private void GetEmpllist()
        {
            string comcod = this.GetComeCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();


            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "GRPACCOUNTINFO", tempddl1,
                                 "7", "%%", "", "", "", "", "", "");
            var rows = new DataTable();
            //if (tempddl2=="7")
            //{
            rows = ds1.Tables[0].AsEnumerable().Where
          (row => row.Field<string>("sircode").Substring(7, 5) == "00000" && row.Field<string>("sircode").Substring(4, 8) != "00000000").CopyToDataTable();

            // }
            //else
            //{
            //    rows = ds1.Tables[0].AsEnumerable().Where
            //  (row => row.Field<string>("sircode").Substring(9, 3) == "000" && row.Field<string>("sircode").Substring(4, 8) != "00000000").CopyToDataTable();

            //}




            this.ddlemplist.DataTextField = "sirdesc";
            this.ddlemplist.DataValueField = "sircode";
            this.ddlemplist.DataSource = rows.AsDataView();
            this.ddlemplist.DataBind();

        }

        protected void Load_CodeBooList()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            try
            {
                string comcod = this.GetComeCode();
                string qtype = this.Request.QueryString["Type"].ToString();
                DataSet dsone = this.da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "GRPACCOUNTCODE", qtype, "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
                this.GetEmpllist();

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

            string comcod = this.GetComeCode();
            string mgcode = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
            string gcode = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;

            string empcode = ((DataTable)Session["storedata"]).Rows[rowindex]["empid"].ToString();
            string teamcode = ((DataTable)Session["storedata"]).Rows[rowindex]["teamcode"].ToString();


            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlUserName");
            DropDownList ddlteam = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlteam");
            Panel pnl02 = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("Panel2");
            Panel pnlteam = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("pnlTeam");
            if (mgcode == "83")
            {
                if ((ASTUtility.Right(gcode, 5) == "00000") && (ASTUtility.Right(gcode, 7) != "0000000"))
                {
                    ViewState["gindex"] = e.NewEditIndex;
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string deptcode = "9402%";
                    string SearchProject = "%" + ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtSearchUserName")).Text.Trim() + "%";
                    DataSet ds1 = da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "GETEMPTIDNAME", SearchProject, deptcode, "", "", "", "", "", "", "");
                    ddl2.DataTextField = "empname";
                    ddl2.DataValueField = "empid";
                    ddl2.DataSource = ds1;
                    ddl2.DataBind();
                    ddl2.SelectedValue = empcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                    pnl02.Visible = true;


                    // Team

                    //  string SearchProject = "%" + ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtSearchUserName")).Text.Trim() + "%";
                    // DataSet ds1 = da.GetTransInfo("", "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
                    ddlteam.DataTextField = "teamdesc";
                    ddlteam.DataValueField = "teamcode";
                    ddlteam.DataSource = (DataTable)ViewState["tblteam"];
                    ddlteam.DataBind();
                    ddlteam.SelectedValue = teamcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                    ddlteam.Visible = true;







                }
                else
                {
                    pnl02.Visible = false;
                    pnlteam.Visible = false;
                    ddl2.Items.Clear();
                    ddlteam.Items.Clear();

                }
            }

            else
            {
                pnl02.Visible = false;
                pnlteam.Visible = false;
                ddl2.Items.Clear();
                ddlteam.Items.Clear();
            }

        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

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
                    string empcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlUserName")).Text.Trim();
                    string teamcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlteam")).Text.Trim();

                    bool result = this.da.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "GRPACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, curcode, empcode,
                       teamcode, "", "", "", "");
                    this.ShowInformation();
                    if (result)
                    {
                        // ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";

                        string msg = "Update Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
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
            }
            catch (Exception ex)
            {
            }
        }
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

                    this.LblBookName1.Text = "Code Book:";
                    this.ddlOthersBook.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    //this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    //this.ibtnSrch.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    //this.lbalterofddl.Text = this.ddlOthersBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.CssClass = "mt-2";
                    this.lbalterofddl0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    //string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
                    //string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";

                    this.LblBookName1.Text = "Select Code Book:";
                    this.ibtnSrch.Visible = false;

                    this.lbalterofddl0.Visible = false;


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
            // string srchoptionmain = "%"+this.txtsrch.Text.Trim()+"%";

            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            string ddd = this.ddlemplist.SelectedValue.ToString();
            string srchitem = ddd.Substring(0, 7) + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "GRPACCOUNTINFO", tempddl1,
                                  tempddl2, srchitem, "", "", "", "", "", "");
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd");

                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                string mgcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                int additem = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "additem"));

                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 5) == "00000" && ASTUtility.Right(Code, 8) != "00000000")
                {
                    e.Row.Attributes["style"] = "background:#b9b9b9;";
                }

                if (additem == 1)
                {
                    lbtnAdd.Visible = true;
                }

            }
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
        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[rowindex].FindControl("ddlUserName");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = "9402%";
            string SearchProject = "%" + ((TextBox)grvacc.Rows[rowindex].FindControl("txtSearchUserName")).Text.Trim() + "%";
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "GETEMPTIDNAME", SearchProject, deptcode, "", "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
        }
        protected void ibtnSrchteam_Click(object sender, EventArgs e)
        {

        }

        protected void ddlOthersBookSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetEmpllist();

        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["storedata"];
                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;

                int index = this.grvacc.PageSize * this.grvacc.PageIndex + RowIndex;
                
                Hashtable hst = (Hashtable)Session["tblLogin"];
                
                string comcod = hst["comcod"].ToString();

                string sircode = dt.Rows[index]["sircode"].ToString();

                this.lblsircode.Text = sircode;

                this.txtCode.Text = sircode.Substring(0, 2) + "-" + sircode.Substring(2, 2) + "-" + sircode.Substring(4, 3) + "-" + sircode.Substring(7, 2) + "-" + ASTUtility.Right(sircode, 3);

                this.Chboxchild.Checked = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");

                this.chkbod.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");

                this.lblchild.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string comcod = hst["comcod"].ToString();

                string txtCode = this.txtCode.Text.Trim();

                string txtDescCode = this.txtDescCode.Text.Trim();

                string txtShrtDesc = this.txtShrtDesc.Text.Trim();

                string textDataTyp = this.textDataTyp.Text.Trim();

                string isircode = this.lblsircode.Text.Trim();

                string tsircode = this.txtCode.Text.Trim().Replace("-", ""); 

                string sircode = (this.Chboxchild.Checked) ? ((ASTUtility.Right(isircode, 8) == "00000000") ? (ASTUtility.Left(isircode, 4) + "001" + ASTUtility.Right(isircode, 5))
                    : ((ASTUtility.Right(isircode, 5) == "00000" && ASTUtility.Right(isircode, 8) != "00000000") ? (ASTUtility.Left(isircode, 7) + "01" + ASTUtility.Right(isircode, 3)) : ASTUtility.Left(isircode, 9) + "001"))
                    : ((isircode != tsircode) ? tsircode : isircode);

                bool addDone = this.da.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_CODEBOOK", "INSERTTOTEAMINF", sircode, txtDescCode, textDataTyp, txtShrtDesc);

                if (addDone)
                {
                    string msg = "Code Book Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                    this.clearDataField();
                    this.ShowInformation();
                    this.Chboxchild.Checked = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void clearDataField()
        {
            this.txtCode.Text="";

            this.txtDescCode.Text = "";

            this.txtShrtDesc.Text = "";

            this.textDataTyp.Text = "";

            this.lblsircode.Text = "";
        }
    }
}
