using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;

namespace RealERPWEB.F_08_PPlan

{
    public partial class RptProTarget : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "WorkBasis") ? "Cash Flow - Work Basis" : "Cash Flow - Resource Basis";


                this.GetProjectName();
                this.ShowView();
               
                if (this.Request.QueryString["prjcode"].Length > 0)
                {

                    this.lbtnOk_Click(null, null);
                }

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }

        private void ShowView()

        {

            string type = this.Request.QueryString["Type"];

            switch (type)
            {
                case "WorkBasis":
                case "ResBasis":
                    string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = "01" + Date.Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(6).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "RealFlow":
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.lblDate.Visible = false;
                    this.txtfrmdate.Visible = false;
                    this.lblrenddate.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblfloor.Visible = false;
                    this.ddlFloorListRpt.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


            }

        }
        protected void GetProjectName()
        {


            string comcod = this.GetCompCode();
            string serch1 = "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PROJECTTARGET", "GETPROJETNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"].Length > 0 ? this.Request.QueryString["prjcode"] : "";
            if (Request.QueryString["prjcode"].Length > 0)
            {
                ddlProjectName.Enabled = false;
            }
            this.GetFloor();

        }
        private void GetFloor()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string grp = (ASTUtility.Left(comcod, 1) == "2") ? "L" : "R";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PROJECTTARGET", "GETFLOOR", pactcode, grp, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable tbl2 = ds1.Tables[0];
            DataRow dr2 = tbl2.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = (ASTUtility.Left(this.GetCompCode(), 1) == "2") ? "All Phases-Sum" : "All Floors-Sum";
            DataRow dr3 = tbl2.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = (ASTUtility.Left(this.GetCompCode(), 1) == "2") ? "All Phases-Details" : "All Floors-Details";
            tbl2.Rows.Add(dr2);
            tbl2.Rows.Add(dr3);
            DataView dv2 = tbl2.DefaultView;
            // dv2.Sort = "flrcod";
            this.ddlFloorListRpt.DataTextField = "flrdes";
            this.ddlFloorListRpt.DataValueField = "flrcod";
            this.ddlFloorListRpt.DataSource = dv2;
            this.ddlFloorListRpt.DataBind();
            this.ddlFloorListRpt.SelectedValue = "000";
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetFloor();

        }


        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {

            this.GetProjectName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblprocomplan"];
            if (dt1.Rows.Count == 0)
                return;




            ReportDocument rptstk = new RealERPRPT.R_08_PPlan.rptProTarget();
            TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = (this.Request.QueryString["Type"].ToString() == "WorkBasis") ? "Cash Flow - Work Basis" : "Cash Flow - Resource Basis";


            TextObject rpttxtProject = rptstk.ReportDefinition.ReportObjects["txtprojectname"] as TextObject;
            rpttxtProject.Text = this.ddlProjectName.SelectedItem.Text.Trim();
            TextObject txtprocomdate = rptstk.ReportDefinition.ReportObjects["txtprocomdate"] as TextObject;
            txtprocomdate.Text = "Start Date: " + this.lblStartDate.Text.Trim() + "      End Date: " + this.lblEndDate.Text.Trim();
            TextObject txtProtovalue = rptstk.ReportDefinition.ReportObjects["txtProtovalue"] as TextObject;
            txtProtovalue.Text = "Total Value: " + this.lbltoValue.Text.Trim();



            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rpttxtdate.Text = "(From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 6; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtmonth" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Project Completion Plan";
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString() + "(From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"];
            switch (type)
            {
                case "WorkBasis":
                case "ResBasis":
                    this.ShowCashFlowRaWork();
                    break;

                case "RealFlow":
                    this.ShowCashFlow();
                    break;
            }

        }

        private void ShowCashFlowRaWork()
        {
            Session.Remove("tblprocomplan");
            Session.Remove("tblMon");
            this.PnlColoumn.Visible = true;
            string comcod = this.GetCompCode();
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfrmdate.Text.Trim()));
            if (mon > 6)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Month Less Than Equal Six');", true);
                return;
            }

            string txtdatefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string floor = this.ddlFloorListRpt.SelectedValue.ToString();

            string CallType = (this.Request.QueryString["Type"].ToString() == "WorkBasis") ? "RPTPROJECTTARGET" : "RPTPROJECTTARGETRESBASIS";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PROJECTTARGET", CallType, pactcode, txtdatefrm, txtdateto, floor, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProTarget.DataSource = null;
                this.gvProTarget.DataBind();
                return;
            }
            //DataTable dt = ds1.Tables[0];
            //if (this.Request.QueryString["Type"].ToString().Trim() == "SrAUtilitiesWOP")
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.RowFilter = ("toamtwp<>0");
            //    dt = dv.ToTable();

            //}


            Session["tblprocomplan"] = HiddenSameData(ds1.Tables[0]);
            this.Visibility();
            if (ds1.Tables[1].Rows.Count == 0)
                return;
            this.lblStartDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjstrtdat"]).ToString("dd-MMM-yyyy");
            this.lblEndDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjenddat"]).ToString("dd-MMM-yyyy");
            this.lblDuration.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["duration"]).ToString("#,#0;(#,#0); ") + " Month";
            this.lbltoValue.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["toamt"]).ToString("#,#0;(#,#0); ");
            this.Data_Bind();
            ds1.Dispose();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Project Completion Plan";
                string eventdesc = "Show Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString() + "From Date:  " + txtdatefrm + "To " + txtdateto;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        private void ShowCashFlow()
        {
            Session.Remove("tblprocomplan");
            Session.Remove("tblMon");
            this.PnlColoumn.Visible = true;
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string floor = this.ddlFloorListRpt.SelectedValue.ToString();
            int index = this.ddlRptGroup.SelectedIndex;
            string level = (index == 0) ? "4" : (index == 1) ? "7" : (index == 2) ? "9" : "12";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PROJECTTARGET", "RPTPROJECTCASHFLOW", pactcode, level, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProDetials.DataSource = null;
                this.gvProDetials.DataBind();
                return;
            }
            //DataTable dt = ds1.Tables[0];
            //if (this.Request.QueryString["Type"].ToString().Trim() == "SrAUtilitiesWOP")
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.RowFilter = ("toamtwp<>0");
            //    dt = dv.ToTable();

            //}


            Session["tblprocomplan"] = HiddenSameData(ds1.Tables[0]);
            if (ds1.Tables[1].Rows.Count == 0)
                return;

            this.lblStartDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjstrtdat"]).ToString("dd-MMM-yyyy");
            this.lblEndDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjenddat"]).ToString("dd-MMM-yyyy");
            this.lblDuration.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["duration"]).ToString("#,#0;(#,#0); ") + " Month";
            // this.lbltoValue.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["toamt"]).ToString("#,#0;(#,#0); ");
            this.Data_Bind();
            ds1.Dispose();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string type = this.Request.QueryString["Type"];
            int j;

            switch (type)
            {
                case "WorkBasis":
                case "ResBasis":
                    string isircode = dt1.Rows[0]["isircode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["isircode"].ToString() == isircode)
                        {
                            isircode = dt1.Rows[j]["isircode"].ToString();
                            dt1.Rows[j]["isirdesc"] = "";
                            dt1.Rows[j]["isirunit"] = "";
                        }

                        else
                        {
                            isircode = dt1.Rows[j]["isircode"].ToString();
                        }
                    }
                    break;

                case "RealFlow":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();
                    }
                    break;
            }

            return dt1;

        }

        private void Visibility()
        {
            int i;
            for (i = 6; i < this.gvProTarget.Columns.Count - 4; i++)
                this.gvProTarget.Columns[i].Visible = false;
            DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

            for (i = 6; i < 20; i = i + 2)
            {
                if (datefrm > dateto)
                    break;
                this.gvProTarget.Columns[i].Visible = true;
                this.gvProTarget.Columns[i + 1].Visible = true;
                this.gvProTarget.Columns[i].HeaderText = datefrm.ToString("MMM yy") + "<br />" + "Qty";
                this.gvProTarget.Columns[i + 1].HeaderText = datefrm.ToString("MMM yy") + "<br />" + "Amount";
                datefrm = datefrm.AddMonths(1);
            }

        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"];
            switch (type)
            {
                case "WorkBasis":
                case "ResBasis":
                    DataTable dt01 = (DataTable)Session["tblprocomplan"];
                    this.gvProTarget.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvProTarget.DataSource = dt01;
                    this.gvProTarget.DataBind();
                    this.FooterAmount(dt01);

                    if (dt01.Rows.Count == 0)
                        return;
                    Session["Report1"] = gvProTarget;
                    ((HyperLink)this.gvProTarget.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case "RealFlow":

                    int i, j;

                    for (i = 4; i < this.gvProDetials.Columns.Count; i++)
                        this.gvProDetials.Columns[i].Visible = false;




                    j = 4;
                    DateTime pstdate = Convert.ToDateTime(this.lblStartDate.Text);
                    DateTime penddate = Convert.ToDateTime(this.lblEndDate.Text);
                    while (pstdate <= penddate)
                    {

                        this.gvProDetials.Columns[j].Visible = true;
                        this.gvProDetials.Columns[j].HeaderText = pstdate.ToString("MMM yy");
                        j++;
                        pstdate = pstdate.AddMonths(1);
                    }






                    this.gvProDetials.DataSource = (DataTable)Session["tblprocomplan"];
                    this.gvProDetials.DataBind();

                    DataTable dt = (DataTable)Session["tblprocomplan"];

                    //for (i = 0; i < gvProDetials.Rows.Count; i++)
                    //{

                    //    int rowindex = (this.gvProDetials.PageSize * this.gvProDetials.PageIndex) + i;


                    //    string rsircode = dt.Rows[rowindex][rsircode].ToString();

                    //    LinkButton lbtn1 = (LinkButton)gvProDetials.Rows[i].FindControl("lnkgvResDescd");

                    //    if (lbtn1 != null)
                    //    {
                    //        if (lbtn1.Text.Trim().Length > 0)
                    //            lbtn1.CommandArgument = rsircode;
                    //    }

                    //}


                    break;


            }


        }

        private void FooterAmount(DataTable dt)
        {
            if (dt.Rows.Count == 0) return;

            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFpreqmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(preamt)", "")) ? 0.00
            : dt.Compute("Sum(preamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt1)", "")) ? 0.00
                : dt.Compute("Sum(amt1)", ""))).ToString("#,##0;(#,##0); ");



            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt2)", "")) ? 0.00
                : dt.Compute("Sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt3)", "")) ? 0.00
                : dt.Compute("Sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt4)", "")) ? 0.00
                : dt.Compute("Sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt5)", "")) ? 0.00
                : dt.Compute("Sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt6)", "")) ? 0.00
                : dt.Compute("Sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFnamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(namt)", "")) ? 0.00
           : dt.Compute("Sum(namt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(toamt)", "")) ? 0.00
           : dt.Compute("Sum(toamt)", ""))).ToString("#,##0;(#,##0); ");



        }

        protected void gvProTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProTarget.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        protected void gvProTarget_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvRptRes1 = (Label)e.Row.FindControl("lblgvItemDesc");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                //  Label lblgvRptAmt1 = (Label)e.Row.FindControl("lblgvRptAmt1");
                // Label lblgvPer = (Label)e.Row.FindControl("lblgvPer");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isircode")).ToString();

                if (code == "")
                {
                    return;
                }



                //if (code == "01AAAAAAAAAA" || code == "04AAAAAAAAAA")
                //{

                //    lblgvRptRes1.Font.Bold = true;
                //    lblgvRptAmt1.Font.Bold = true;
                //    lblgvPer.Font.Bold = true;
                //    lblgvRptRes1.Style.Add("text-align", "right");
                //    e.Row.Attributes["style"] = "background-color:pink;font-size:16px; font-weight:bold;";
                //}





                //else if (ASTUtility.Right(code, 4) == "AAAA")
                //{

                //    lblgvRptRes1.Font.Bold = true;
                //    lblgvRptAmt1.Font.Bold = true;
                //    lblgvPer.Font.Bold = true;
                //    lblgvRptRes1.Style.Add("text-align", "right");
                //}

                if (ASTUtility.Right(code, 5) == "00000")
                {
                    lblgvRptRes1.Attributes["style"] = "font-weight:bold; color:green;";
                    // lblgvRptAmt1.Font.Bold = true;
                    // lblgvPer.Font.Bold = true;


                }



            }
        }
        protected void gvProDetials_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton hlnkgvResDescd = (LinkButton)e.Row.FindControl("lnkgvResDescd");
                Label lgvbudgetam = (Label)e.Row.FindControl("lgvbudgetamcf");
                Label lgvtoamt = (Label)e.Row.FindControl("lgvtoamtcf");

                Label lgvamt1 = (Label)e.Row.FindControl("lgvamt1d");
                Label lgvamt2 = (Label)e.Row.FindControl("lgvamt2d");
                Label lgvamt3 = (Label)e.Row.FindControl("lgvamt3d");
                Label lgvamt4 = (Label)e.Row.FindControl("lgvamt4d");
                Label lgvamt5 = (Label)e.Row.FindControl("lgvamt5d");
                Label lgvamt6 = (Label)e.Row.FindControl("lgvamt6d");
                Label lgvamt7 = (Label)e.Row.FindControl("lgvamt7d");
                Label lgvamt8 = (Label)e.Row.FindControl("lgvamt8d");
                Label lgvamt9 = (Label)e.Row.FindControl("lgvamt9d");
                Label lgvamt10 = (Label)e.Row.FindControl("lgvamt10d");
                Label lgvamt11 = (Label)e.Row.FindControl("lgvamt11d");
                Label lgvamt12 = (Label)e.Row.FindControl("lgvamt12d");
                Label lgvamt13 = (Label)e.Row.FindControl("lgvamt13d");
                Label lgvamt14 = (Label)e.Row.FindControl("lgvamt14d");
                Label lgvamt15 = (Label)e.Row.FindControl("lgvamt15d");
                Label lgvamt16 = (Label)e.Row.FindControl("lgvamt16d");
                Label lgvamt17 = (Label)e.Row.FindControl("lgvamt17d");
                Label lgvamt18 = (Label)e.Row.FindControl("lgvamt18d");
                Label lgvamt19 = (Label)e.Row.FindControl("lgvamt19d");
                Label lgvamt20 = (Label)e.Row.FindControl("lgvamt20d");
                Label lgvamt21 = (Label)e.Row.FindControl("lgvamt21d");
                Label lgvamt22 = (Label)e.Row.FindControl("lgvamt22d");
                Label lgvamt23 = (Label)e.Row.FindControl("lgvamt23d");
                Label lgvamt24 = (Label)e.Row.FindControl("lgvamt24d");
                Label lgvamt25 = (Label)e.Row.FindControl("lgvamt25d");
                Label lgvamt26 = (Label)e.Row.FindControl("lgvamt26d");
                Label lgvamt27 = (Label)e.Row.FindControl("lgvamt27d");
                Label lgvamt28 = (Label)e.Row.FindControl("lgvamt28d");
                Label lgvamt29 = (Label)e.Row.FindControl("lgvamt29d");
                Label lgvamt30 = (Label)e.Row.FindControl("lgvamt30d");
                Label lgvamt31 = (Label)e.Row.FindControl("lgvamt31d");
                Label lgvamt32 = (Label)e.Row.FindControl("lgvamt32d");
                Label lgvamt33 = (Label)e.Row.FindControl("lgvamt33d");
                Label lgvamt34 = (Label)e.Row.FindControl("lgvamt34d");
                Label lgvamt35 = (Label)e.Row.FindControl("lgvamt35d");
                Label lgvamt36 = (Label)e.Row.FindControl("lgvamt36d");
                Label lgvamt37 = (Label)e.Row.FindControl("lgvamt37d");
                Label lgvamt38 = (Label)e.Row.FindControl("lgvamt38d");
                Label lgvamt39 = (Label)e.Row.FindControl("lgvamt39d");
                Label lgvamt40 = (Label)e.Row.FindControl("lgvamt40d");
                Label lgvamt41 = (Label)e.Row.FindControl("lgvamt41d");
                Label lgvamt42 = (Label)e.Row.FindControl("lgvamt42d");
                Label lgvamt43 = (Label)e.Row.FindControl("lgvamt43d");
                Label lgvamt44 = (Label)e.Row.FindControl("lgvamt44d");
                Label lgvamt45 = (Label)e.Row.FindControl("lgvamt45d");
                Label lgvamt46 = (Label)e.Row.FindControl("lgvamt46d");
                Label lgvamt47 = (Label)e.Row.FindControl("lgvamt47d");
                Label lgvamt48 = (Label)e.Row.FindControl("lgvamt48d");
                Label lgvamt49 = (Label)e.Row.FindControl("lgvamt49d");
                Label lgvamt50 = (Label)e.Row.FindControl("lgvamt50d");
                Label lgvamt51 = (Label)e.Row.FindControl("lgvamt51d");
                Label lgvamt52 = (Label)e.Row.FindControl("lgvamt52d");
                Label lgvamt53 = (Label)e.Row.FindControl("lgvamt53d");
                Label lgvamt54 = (Label)e.Row.FindControl("lgvamt54d");
                Label lgvamt55 = (Label)e.Row.FindControl("lgvamt55d");
                Label lgvamt56 = (Label)e.Row.FindControl("lgvamt56d");
                Label lgvamt57 = (Label)e.Row.FindControl("lgvamt57d");
                Label lgvamt58 = (Label)e.Row.FindControl("lgvamt58d");
                Label lgvamt59 = (Label)e.Row.FindControl("lgvamt59d");
                Label lgvamt60 = (Label)e.Row.FindControl("lgvamt60d");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();

                if (code == "")
                {
                    return;
                }



                if (code.Substring(2) == "00")
                {

                    //  lgvResDescd.Font.Bold = true;
                    lgvbudgetam.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvamt1.Font.Bold = true;
                    lgvamt2.Font.Bold = true;
                    lgvamt3.Font.Bold = true;
                    lgvamt4.Font.Bold = true;
                    lgvamt5.Font.Bold = true;
                    lgvamt6.Font.Bold = true;
                    lgvamt7.Font.Bold = true;
                    lgvamt8.Font.Bold = true;
                    lgvamt9.Font.Bold = true;
                    lgvamt10.Font.Bold = true;
                    lgvamt11.Font.Bold = true;
                    lgvamt13.Font.Bold = true;
                    lgvamt14.Font.Bold = true;
                    lgvamt15.Font.Bold = true;
                    lgvamt16.Font.Bold = true;
                    lgvamt17.Font.Bold = true;
                    lgvamt18.Font.Bold = true;
                    lgvamt19.Font.Bold = true;
                    lgvamt20.Font.Bold = true;
                    lgvamt21.Font.Bold = true;
                    lgvamt22.Font.Bold = true;
                    lgvamt23.Font.Bold = true;
                    lgvamt24.Font.Bold = true;
                    lgvamt25.Font.Bold = true;
                    lgvamt26.Font.Bold = true;
                    lgvamt27.Font.Bold = true;
                    lgvamt28.Font.Bold = true;
                    lgvamt29.Font.Bold = true;
                    lgvamt30.Font.Bold = true;
                    lgvamt31.Font.Bold = true;
                    lgvamt32.Font.Bold = true;
                    lgvamt33.Font.Bold = true;
                    lgvamt34.Font.Bold = true;
                    lgvamt35.Font.Bold = true;
                    lgvamt36.Font.Bold = true;
                    lgvamt37.Font.Bold = true;
                    lgvamt38.Font.Bold = true;
                    lgvamt39.Font.Bold = true;
                    lgvamt40.Font.Bold = true;
                    lgvamt41.Font.Bold = true;
                    lgvamt42.Font.Bold = true;
                    lgvamt43.Font.Bold = true;
                    lgvamt44.Font.Bold = true;
                    lgvamt45.Font.Bold = true;
                    lgvamt46.Font.Bold = true;
                    lgvamt47.Font.Bold = true;
                    lgvamt48.Font.Bold = true;
                    lgvamt49.Font.Bold = true;
                    lgvamt50.Font.Bold = true;
                    lgvamt51.Font.Bold = true;
                    lgvamt52.Font.Bold = true;
                    lgvamt53.Font.Bold = true;
                    lgvamt54.Font.Bold = true;
                    lgvamt55.Font.Bold = true;
                    lgvamt56.Font.Bold = true;
                    lgvamt57.Font.Bold = true;
                    lgvamt58.Font.Bold = true;
                    lgvamt59.Font.Bold = true;
                    lgvamt60.Font.Bold = true;

                    hlnkgvResDescd.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvbudgetam.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvtoamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt1.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt2.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt3.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt4.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt5.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt6.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt7.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt8.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt9.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt10.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt11.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt12.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt13.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt14.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt15.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt16.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt17.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt18.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt19.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt20.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt21.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt22.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt23.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt24.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt25.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt26.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt27.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt28.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt29.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt30.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt31.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt32.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt33.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt34.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt35.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt36.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt37.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt38.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt39.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt40.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt41.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt42.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt43.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt4.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt45.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt46.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt47.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt48.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt49.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt50.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt51.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt52.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt23.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt24.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt55.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt56.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt57.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt58.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt59.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt60.Attributes["style"] = "color:maroon; font-weight:bold;";

                }


                else if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    hlnkgvResDescd.Font.Bold = true;
                    lgvbudgetam.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvamt1.Font.Bold = true;
                    lgvamt2.Font.Bold = true;
                    lgvamt3.Font.Bold = true;
                    lgvamt4.Font.Bold = true;
                    lgvamt5.Font.Bold = true;
                    lgvamt6.Font.Bold = true;
                    lgvamt7.Font.Bold = true;
                    lgvamt8.Font.Bold = true;
                    lgvamt9.Font.Bold = true;
                    lgvamt10.Font.Bold = true;
                    lgvamt11.Font.Bold = true;
                    lgvamt13.Font.Bold = true;
                    lgvamt14.Font.Bold = true;
                    lgvamt15.Font.Bold = true;
                    lgvamt16.Font.Bold = true;
                    lgvamt17.Font.Bold = true;
                    lgvamt18.Font.Bold = true;
                    lgvamt19.Font.Bold = true;
                    lgvamt20.Font.Bold = true;
                    lgvamt21.Font.Bold = true;
                    lgvamt22.Font.Bold = true;
                    lgvamt23.Font.Bold = true;
                    lgvamt24.Font.Bold = true;
                    lgvamt25.Font.Bold = true;
                    lgvamt26.Font.Bold = true;
                    lgvamt27.Font.Bold = true;
                    lgvamt28.Font.Bold = true;
                    lgvamt29.Font.Bold = true;
                    lgvamt30.Font.Bold = true;
                    lgvamt31.Font.Bold = true;
                    lgvamt32.Font.Bold = true;
                    lgvamt33.Font.Bold = true;
                    lgvamt34.Font.Bold = true;
                    lgvamt35.Font.Bold = true;
                    lgvamt36.Font.Bold = true;
                    lgvamt37.Font.Bold = true;
                    lgvamt38.Font.Bold = true;
                    lgvamt39.Font.Bold = true;
                    lgvamt40.Font.Bold = true;
                    lgvamt41.Font.Bold = true;
                    lgvamt42.Font.Bold = true;
                    lgvamt43.Font.Bold = true;
                    lgvamt44.Font.Bold = true;
                    lgvamt45.Font.Bold = true;
                    lgvamt46.Font.Bold = true;
                    lgvamt47.Font.Bold = true;
                    lgvamt48.Font.Bold = true;
                    lgvamt49.Font.Bold = true;
                    lgvamt50.Font.Bold = true;
                    lgvamt51.Font.Bold = true;
                    lgvamt52.Font.Bold = true;
                    lgvamt53.Font.Bold = true;
                    lgvamt54.Font.Bold = true;
                    lgvamt55.Font.Bold = true;
                    lgvamt56.Font.Bold = true;
                    lgvamt57.Font.Bold = true;
                    lgvamt58.Font.Bold = true;
                    lgvamt59.Font.Bold = true;
                    lgvamt60.Font.Bold = true;


                    hlnkgvResDescd.Attributes["style"] = "color:blue; font-weight:bold; text-align:right;";
                    lgvbudgetam.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvtoamt.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt1.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt2.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt3.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt4.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt5.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt6.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt7.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt8.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt9.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt10.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt11.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt12.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt13.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt14.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt15.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt16.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt17.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt18.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt19.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt20.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt21.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt22.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt23.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt24.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt25.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt26.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt27.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt28.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt29.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt30.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt31.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt32.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt33.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt34.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt35.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt36.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt37.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt38.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt39.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt40.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt41.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt42.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt43.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt4.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt45.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt46.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt47.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt48.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt49.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt50.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt51.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt52.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt23.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt24.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt55.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt56.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt57.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt58.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt59.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt60.Attributes["style"] = "color:blue; font-weight:bold;";
                    hlnkgvResDescd.Style.Add("text-align", "right");
                }



            }
        }


        protected void gvresdet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvResDescd = (Label)e.Row.FindControl("lgvResDescd");
                Label lgvbudgetam = (Label)e.Row.FindControl("lgvbudgetamrdet");
                Label lgvtoamt = (Label)e.Row.FindControl("lgvtoamtrdet");

                Label lgvamt1 = (Label)e.Row.FindControl("lgvamt1drdet");
                Label lgvamt2 = (Label)e.Row.FindControl("lgvamt2drdet");
                Label lgvamt3 = (Label)e.Row.FindControl("lgvamt3drdet");
                Label lgvamt4 = (Label)e.Row.FindControl("lgvamt4drdet");
                Label lgvamt5 = (Label)e.Row.FindControl("lgvamt5drdet");
                Label lgvamt6 = (Label)e.Row.FindControl("lgvamt6drdet");
                Label lgvamt7 = (Label)e.Row.FindControl("lgvamt7drdet");
                Label lgvamt8 = (Label)e.Row.FindControl("lgvamt8drdet");
                Label lgvamt9 = (Label)e.Row.FindControl("lgvamt9drdet");
                Label lgvamt10 = (Label)e.Row.FindControl("lgvamt10drdet");
                Label lgvamt11 = (Label)e.Row.FindControl("lgvamt11drdet");
                Label lgvamt12 = (Label)e.Row.FindControl("lgvamt12drdet");
                Label lgvamt13 = (Label)e.Row.FindControl("lgvamt13drdet");
                Label lgvamt14 = (Label)e.Row.FindControl("lgvamt14drdet");
                Label lgvamt15 = (Label)e.Row.FindControl("lgvamt15drdet");
                Label lgvamt16 = (Label)e.Row.FindControl("lgvamt16drdet");
                Label lgvamt17 = (Label)e.Row.FindControl("lgvamt17drdet");
                Label lgvamt18 = (Label)e.Row.FindControl("lgvamt18drdet");
                Label lgvamt19 = (Label)e.Row.FindControl("lgvamt19drdet");
                Label lgvamt20 = (Label)e.Row.FindControl("lgvamt20drdet");
                Label lgvamt21 = (Label)e.Row.FindControl("lgvamt21drdet");
                Label lgvamt22 = (Label)e.Row.FindControl("lgvamt22drdet");
                Label lgvamt23 = (Label)e.Row.FindControl("lgvamt23drdet");
                Label lgvamt24 = (Label)e.Row.FindControl("lgvamt24drdet");
                Label lgvamt25 = (Label)e.Row.FindControl("lgvamt25drdet");
                Label lgvamt26 = (Label)e.Row.FindControl("lgvamt26drdet");
                Label lgvamt27 = (Label)e.Row.FindControl("lgvamt27drdet");
                Label lgvamt28 = (Label)e.Row.FindControl("lgvamt28drdet");
                Label lgvamt29 = (Label)e.Row.FindControl("lgvamt29drdet");
                Label lgvamt30 = (Label)e.Row.FindControl("lgvamt30drdet");
                Label lgvamt31 = (Label)e.Row.FindControl("lgvamt31drdet");
                Label lgvamt32 = (Label)e.Row.FindControl("lgvamt32drdet");
                Label lgvamt33 = (Label)e.Row.FindControl("lgvamt33drdet");
                Label lgvamt34 = (Label)e.Row.FindControl("lgvamt34drdet");
                Label lgvamt35 = (Label)e.Row.FindControl("lgvamt35drdet");
                Label lgvamt36 = (Label)e.Row.FindControl("lgvamt36drdet");
                Label lgvamt37 = (Label)e.Row.FindControl("lgvamt37drdet");
                Label lgvamt38 = (Label)e.Row.FindControl("lgvamt38drdet");
                Label lgvamt39 = (Label)e.Row.FindControl("lgvamt39drdet");
                Label lgvamt40 = (Label)e.Row.FindControl("lgvamt40drdet");
                Label lgvamt41 = (Label)e.Row.FindControl("lgvamt41drdet");
                Label lgvamt42 = (Label)e.Row.FindControl("lgvamt42drdet");
                Label lgvamt43 = (Label)e.Row.FindControl("lgvamt43drdet");
                Label lgvamt44 = (Label)e.Row.FindControl("lgvamt44drdet");
                Label lgvamt45 = (Label)e.Row.FindControl("lgvamt45drdet");
                Label lgvamt46 = (Label)e.Row.FindControl("lgvamt46drdet");
                Label lgvamt47 = (Label)e.Row.FindControl("lgvamt47drdet");
                Label lgvamt48 = (Label)e.Row.FindControl("lgvamt48drdet");
                Label lgvamt49 = (Label)e.Row.FindControl("lgvamt49drdet");
                Label lgvamt50 = (Label)e.Row.FindControl("lgvamt50drdet");
                Label lgvamt51 = (Label)e.Row.FindControl("lgvamt51drdet");
                Label lgvamt52 = (Label)e.Row.FindControl("lgvamt52drdet");
                Label lgvamt53 = (Label)e.Row.FindControl("lgvamt53drdet");
                Label lgvamt54 = (Label)e.Row.FindControl("lgvamt54drdet");
                Label lgvamt55 = (Label)e.Row.FindControl("lgvamt55drdet");
                Label lgvamt56 = (Label)e.Row.FindControl("lgvamt56drdet");
                Label lgvamt57 = (Label)e.Row.FindControl("lgvamt57drdet");
                Label lgvamt58 = (Label)e.Row.FindControl("lgvamt58drdet");
                Label lgvamt59 = (Label)e.Row.FindControl("lgvamt59drdet");
                Label lgvamt60 = (Label)e.Row.FindControl("lgvamt60drdet");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();

                if (code == "")
                {
                    return;
                }



                if (code.Substring(2) == "00")
                {

                    //  lgvResDescd.Font.Bold = true;
                    lgvbudgetam.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvamt1.Font.Bold = true;
                    lgvamt2.Font.Bold = true;
                    lgvamt3.Font.Bold = true;
                    lgvamt4.Font.Bold = true;
                    lgvamt5.Font.Bold = true;
                    lgvamt6.Font.Bold = true;
                    lgvamt7.Font.Bold = true;
                    lgvamt8.Font.Bold = true;
                    lgvamt9.Font.Bold = true;
                    lgvamt10.Font.Bold = true;
                    lgvamt11.Font.Bold = true;
                    lgvamt13.Font.Bold = true;
                    lgvamt14.Font.Bold = true;
                    lgvamt15.Font.Bold = true;
                    lgvamt16.Font.Bold = true;
                    lgvamt17.Font.Bold = true;
                    lgvamt18.Font.Bold = true;
                    lgvamt19.Font.Bold = true;
                    lgvamt20.Font.Bold = true;
                    lgvamt21.Font.Bold = true;
                    lgvamt22.Font.Bold = true;
                    lgvamt23.Font.Bold = true;
                    lgvamt24.Font.Bold = true;
                    lgvamt25.Font.Bold = true;
                    lgvamt26.Font.Bold = true;
                    lgvamt27.Font.Bold = true;
                    lgvamt28.Font.Bold = true;
                    lgvamt29.Font.Bold = true;
                    lgvamt30.Font.Bold = true;
                    lgvamt31.Font.Bold = true;
                    lgvamt32.Font.Bold = true;
                    lgvamt33.Font.Bold = true;
                    lgvamt34.Font.Bold = true;
                    lgvamt35.Font.Bold = true;
                    lgvamt36.Font.Bold = true;
                    lgvamt37.Font.Bold = true;
                    lgvamt38.Font.Bold = true;
                    lgvamt39.Font.Bold = true;
                    lgvamt40.Font.Bold = true;
                    lgvamt41.Font.Bold = true;
                    lgvamt42.Font.Bold = true;
                    lgvamt43.Font.Bold = true;
                    lgvamt44.Font.Bold = true;
                    lgvamt45.Font.Bold = true;
                    lgvamt46.Font.Bold = true;
                    lgvamt47.Font.Bold = true;
                    lgvamt48.Font.Bold = true;
                    lgvamt49.Font.Bold = true;
                    lgvamt50.Font.Bold = true;
                    lgvamt51.Font.Bold = true;
                    lgvamt52.Font.Bold = true;
                    lgvamt53.Font.Bold = true;
                    lgvamt54.Font.Bold = true;
                    lgvamt55.Font.Bold = true;
                    lgvamt56.Font.Bold = true;
                    lgvamt57.Font.Bold = true;
                    lgvamt58.Font.Bold = true;
                    lgvamt59.Font.Bold = true;
                    lgvamt60.Font.Bold = true;

                    lgvResDescd.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvbudgetam.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvtoamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt1.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt2.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt3.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt4.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt5.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt6.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt7.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt8.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt9.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt10.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt11.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt12.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt13.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt14.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt15.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt16.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt17.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt18.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt19.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt20.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt21.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt22.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt23.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt24.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt25.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt26.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt27.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt28.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt29.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt30.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt31.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt32.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt33.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt34.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt35.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt36.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt37.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt38.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt39.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt40.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt41.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt42.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt43.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt4.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt45.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt46.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt47.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt48.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt49.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt50.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt51.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt52.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt23.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt24.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt55.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt56.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt57.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt58.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt59.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lgvamt60.Attributes["style"] = "color:maroon; font-weight:bold;";

                }


                else if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvResDescd.Font.Bold = true;
                    lgvbudgetam.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvamt1.Font.Bold = true;
                    lgvamt2.Font.Bold = true;
                    lgvamt3.Font.Bold = true;
                    lgvamt4.Font.Bold = true;
                    lgvamt5.Font.Bold = true;
                    lgvamt6.Font.Bold = true;
                    lgvamt7.Font.Bold = true;
                    lgvamt8.Font.Bold = true;
                    lgvamt9.Font.Bold = true;
                    lgvamt10.Font.Bold = true;
                    lgvamt11.Font.Bold = true;
                    lgvamt13.Font.Bold = true;
                    lgvamt14.Font.Bold = true;
                    lgvamt15.Font.Bold = true;
                    lgvamt16.Font.Bold = true;
                    lgvamt17.Font.Bold = true;
                    lgvamt18.Font.Bold = true;
                    lgvamt19.Font.Bold = true;
                    lgvamt20.Font.Bold = true;
                    lgvamt21.Font.Bold = true;
                    lgvamt22.Font.Bold = true;
                    lgvamt23.Font.Bold = true;
                    lgvamt24.Font.Bold = true;
                    lgvamt25.Font.Bold = true;
                    lgvamt26.Font.Bold = true;
                    lgvamt27.Font.Bold = true;
                    lgvamt28.Font.Bold = true;
                    lgvamt29.Font.Bold = true;
                    lgvamt30.Font.Bold = true;
                    lgvamt31.Font.Bold = true;
                    lgvamt32.Font.Bold = true;
                    lgvamt33.Font.Bold = true;
                    lgvamt34.Font.Bold = true;
                    lgvamt35.Font.Bold = true;
                    lgvamt36.Font.Bold = true;
                    lgvamt37.Font.Bold = true;
                    lgvamt38.Font.Bold = true;
                    lgvamt39.Font.Bold = true;
                    lgvamt40.Font.Bold = true;
                    lgvamt41.Font.Bold = true;
                    lgvamt42.Font.Bold = true;
                    lgvamt43.Font.Bold = true;
                    lgvamt44.Font.Bold = true;
                    lgvamt45.Font.Bold = true;
                    lgvamt46.Font.Bold = true;
                    lgvamt47.Font.Bold = true;
                    lgvamt48.Font.Bold = true;
                    lgvamt49.Font.Bold = true;
                    lgvamt50.Font.Bold = true;
                    lgvamt51.Font.Bold = true;
                    lgvamt52.Font.Bold = true;
                    lgvamt53.Font.Bold = true;
                    lgvamt54.Font.Bold = true;
                    lgvamt55.Font.Bold = true;
                    lgvamt56.Font.Bold = true;
                    lgvamt57.Font.Bold = true;
                    lgvamt58.Font.Bold = true;
                    lgvamt59.Font.Bold = true;
                    lgvamt60.Font.Bold = true;


                    lgvResDescd.Attributes["style"] = "color:blue; font-weight:bold; text-align:right;";
                    lgvbudgetam.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvtoamt.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt1.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt2.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt3.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt4.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt5.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt6.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt7.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt8.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt9.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt10.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt11.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt12.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt13.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt14.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt15.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt16.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt17.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt18.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt19.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt20.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt21.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt22.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt23.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt24.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt25.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt26.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt27.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt28.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt29.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt30.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt31.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt32.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt33.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt34.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt35.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt36.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt37.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt38.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt39.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt40.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt41.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt42.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt43.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt4.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt45.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt46.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt47.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt48.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt49.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt50.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt51.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt52.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt23.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt24.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt55.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt56.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt57.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt58.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt59.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvamt60.Attributes["style"] = "color:blue; font-weight:bold;";
                    lgvResDescd.Style.Add("text-align", "right");
                }



            }
        }
        protected void lnkgvResDescd_Click(object sender, EventArgs e)
        {
            /*

            Session.Remove("tblresdetails");
            string comcod = this.GetCompCode();
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            DataTable dt = (DataTable)Session["tblprocomplan"];
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string rsircode = dt.Rows[rowindex]["rsircode"].ToString();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PROJECTTARGET", "RPTPROJECTCASHFLOWRESDET", pactcode, rsircode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvresdet.DataSource = null;
                this.gvresdet.DataBind();
                return;
            }


            int i, j;

            for (i = 4; i < this.gvresdet.Columns.Count; i++)
                this.gvresdet.Columns[i].Visible = false;


            j = 4;
            DateTime pstdate = Convert.ToDateTime(this.lblStartDate.Text);
            DateTime penddate = Convert.ToDateTime(this.lblEndDate.Text);
            while (pstdate <= penddate)
            {

                this.gvresdet.Columns[j].Visible = true;
                this.gvresdet.Columns[j].HeaderText = pstdate.ToString("MMM yy");
                j++;
                pstdate = pstdate.AddMonths(1);
            }


            this.gvresdet.DataSource = ds1.Tables[0];
            this.gvresdet.DataBind();


            //string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

            */
        }
    }
}