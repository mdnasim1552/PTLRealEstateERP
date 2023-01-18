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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccFincStatmnt : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = Convert.ToDateTime("01-Jan-" + curdate.Substring(7)).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtOpeningDate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddYears(-1).ToString("dd-MMM-yyyy");

            }
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string reptype = this.txtflag.Text.Trim();

            switch (reptype)
            {
                case "IS":
                    this.GetIncomeStatementForPrint();
                    break;
                case "BS":
                    this.GetBalanceSheetForPrint();
                    break;
                case "SHEQUITY":
                    this.PrintShareQty();
                    break;
                case "CSHFLW":
                    this.RptCashFlow02();
                    break;
                case "CSHFLW2":
                    this.RptCashFlow02();
                    break;

                case "Prjwiseres":
                    this.RptProWisResource();
                    break;





            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();

            string reptype = this.txtflag.Text.Trim();

            switch (reptype)
            {
                case "IS":
                    this.GetIncomeStatement();
                    break;
                case "BS":
                    this.GetBalanceSheet();
                    break;
                case "SHEQUITY":
                    this.SHOWSHAREEQUIT();
                    break;
                case "CSHFLW":
                    this.ShowCashFlow();
                    break;
                case "CSHFLW2":
                    this.ShowCashFlow02();
                    break;
                case "Prjwiseres":
                    this.ShowProWiseResource();

                    break;



            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "activetab();", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report: ";// + mRepID;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void GetBalanceSheet()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetComeCode();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string CallType = this.Company();
            string headallocation = this.GetCompanyHeadAllocation();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", CallType +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, headallocation, "", "", "", "", "");


            this.dgvBS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.DataSource = ds1.Tables[0];
            this.dgvBS.DataBind();
            ViewState["tblAcc"] = ds1.Tables[0];

            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvBS.HeaderRow.FindControl("hlbtnDetailsbs")).NavigateUrl = "LinkAccount.aspx?Type=Details&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

        }
        protected void dgvBS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            this.txtOpeningDate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddYears(-1).ToString("dd-MMM-yyyy");

            string opendat = this.txtOpeningDate.Text;
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDesc");
            Label lblcode = (Label)e.Row.FindControl("lblgvcode");
            Label clobal = (Label)e.Row.FindControl("lblgvclobal");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label cuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetComeCode();


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();

            string level = this.DDListLevels.SelectedValue.ToString();
            if (code == "")
            {
                return;
            }
            if (code == "01DAAAAA" || code == "02IAAAAA")
            {
                hlink1.Style.Add("color", "green");
                lblcode.Style.Add("color", "green");
                clobal.Style.Add("color", "green");
                opnamt.Style.Add("color", "green");
                cuamt.Style.Add("color", "green");
                hlink1.Style.Add("font-weight", "bolder");
                lblcode.Style.Add("font-weight", "bolder");
                clobal.Style.Add("font-weight", "bolder");
                opnamt.Style.Add("font-weight", "bolder");
                cuamt.Style.Add("font-weight", "bolder");
            }
            else if (code == "01010000" || code == "01020000" || code == "02010000" || code == "02020000" || code == "02030000")
            {
                hlink1.Style.Add("color", "blue");
                lblcode.Style.Add("color", "blue");
                clobal.Style.Add("color", "blue");
                opnamt.Style.Add("color", "blue");
                cuamt.Style.Add("color", "blue");


            }
            else if (level == "4" && code.Length == 8 && (ASTUtility.Right(code, 2) != "00" && ASTUtility.Right(code, 2) != "AA"))
            {
                hlink1.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblcode.Attributes["style"] = "color:maroon; font-weight:bold;";
                clobal.Attributes["style"] = "color:maroon; font-weight:bold;";
                opnamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                cuamt.Attributes["style"] = "color:maroon; font-weight:bold;";

            }
            else if (level == "2")
            {
                if (code == "02010700" || code == "02010300")   //F_17_Acc/AccFinalReports.aspx?RepType=IS // code == "02010200" 
                {
                    hlink1.NavigateUrl = "LinkAccFinalReports.aspx?RepType=" + "IS" + "&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");

                }

                //else
                //{
                //    hlink1.NavigateUrl = "LinkAccount.aspx?Type=BalanceDet&acgcode=" + code.Substring(0, 6) + "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim() + "&mdesc=" + txtmACTDESC();

                //}



            }




        }
        private string Company()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string Calltype = "";
            switch (comcod)
            {
                case "2305":
                case "3306":
                    Calltype = "BSR_WIP_COMPANY_0";
                    break;
                default:
                    Calltype = "BSR_COMPANY_0";
                    break;
            }
            return Calltype;
        }

        private string GetCompanyHeadAllocation()
        {
            string comcod = this.GetComeCode();
            string headallocation = "";
            switch (comcod)
            {
                case "3348":// Credence
                case "3101"://Asit
                    headallocation = "Headallocation";
                    break;


                default:

                    break;


            }

            return headallocation;


        }
        protected void GetIncomeStatement()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetComeCode();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string Opndate = this.txtOpeningDate.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");

            string headallocation = this.GetCompanyHeadAllocation();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, Opndate, headallocation, "", "", "", "");

            this.dgvIS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            this.dgvIS.Columns[3].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.dgvIS.DataSource = ds1.Tables[0];
            this.dgvIS.DataBind();
            ds1.Dispose();
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvIS.HeaderRow.FindControl("hlbtnDetails")).NavigateUrl = "LinkAccount.aspx?Type=INDetails&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy"); ;


        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            int j;
            string grpcode;
            string RptType = this.txtflag.Text.Trim();
            switch (RptType)
            {

                case "IS":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            dt1.Rows[j]["grpcode"] = "";



                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                        }

                    }
                    break;



                case "SPBS":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                        }

                    }
                    break;
                case "CSHFLW":
                case "CSHFLW2":

                    grpcode = dt1.Rows[0]["grp"].ToString();

                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpcode)
                            dt1.Rows[j]["grpdesc"] = "";
                        grpcode = dt1.Rows[j]["grp"].ToString();




                    }



                    break;


                case "Prjwiseres":

                    string catmcode = dt1.Rows[0]["catmcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["catmcode"].ToString() == catmcode)
                            dt1.Rows[j]["catmdesc"] = "";
                        catmcode = dt1.Rows[j]["catmcode"].ToString();
                    }
                    break;


                default:
                    grpcode = dt1.Rows[0]["grp"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                        }

                    }

                    break;

            }


            return dt1;


        }

        protected void dgvIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvISDesc");
            Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Label lblgvopnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobal");


            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (code == "3AAAAAAAAAAA")
            {
                hlink1.Style.Add("color", "blue");
                hlink1.NavigateUrl = "LinkAccount.aspx?Type=SalDetails&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }




            if (code == "030102AA" || code == "030201AA" || code == "030201CA" || code == "030202AA" || code == "030301AA" || code == "030302AA" || code == "030501AA" || code == "030502AA" || code == "03BAAAAA" || code == "03CAAAAA")
            {

                hlink1.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvcuamt.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvopnamt.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvclobal.Attributes["style"] = "color:blue; font-weight:bold;";

            }

            else if (ASTUtility.Right(code, 2) == "00")
            {
                hlink1.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvcuamt.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvopnamt.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvclobal.Attributes["style"] = "color:green; font-weight:bold;";

            }

            else
            {

                hlink1.Attributes["style"] = "color:black;";
                lblgvcuamt.Attributes["style"] = "color:black;";
                lblgvopnamt.Attributes["style"] = "color:black; ";
                lblgvclobal.Attributes["style"] = "color:black;";
            }


        }


        private void SHOWSHAREEQUIT()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            Session.Remove("tblfinst");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string Level = this.DDListLevels.SelectedValue.ToString();


            string levelval = (Level == "1") ? "2" : (Level == "2") ? "4" : (Level == "3") ? "8" : "12";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTSHAREHOLDEREQUITY", date1, date2, levelval, "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvsequ.DataSource = null;
                this.gvsequ.DataBind();

            }

            DataTable dt = ds2.Tables[0];
            Session["tblfinst"] = dt;

            this.gvsequ.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd MMMM yyyy");
            this.gvsequ.Columns[5].HeaderText = "Balance as at " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd MMMM yyyy");

            this.gvsequ.DataSource = dt;
            this.gvsequ.DataBind();

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFopnamse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFcramtse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cram)", "")) ?
                        0.00 : dt.Compute("Sum(cram)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFdramtse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dram)", "")) ?
                        0.00 : dt.Compute("Sum(dram)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFclosamse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closam)", "")) ?
                        0.00 : dt.Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); - ");



        }

        private void ShowCashFlow()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string Opendate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string group = this.DDListLevels.SelectedValue.ToString();
            switch (group)
            {
                case "1":
                    group = "2";
                    break;
                case "2":
                    group = "4";
                    break;
                case "3":
                    group = "8";
                    break;
                case "4":
                    group = "12";
                    break;
            }

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTCASHFLOW", frmdate, todate, Opendate, group, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvCashFlow.DataSource = null;
                this.grvCashFlow.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            // ViewState["recandpayNote"] = ds1.Tables[1];
            // this.grvCashFlow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvCashFlow.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            this.grvCashFlow.Columns[4].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            this.grvCashFlow.DataSource = this.HiddenSameData(ds1.Tables[0]);
            this.grvCashFlow.DataBind();
            ds1.Dispose();

            for (int i = 0; i < grvCashFlow.Rows.Count; i++)
            {
                string actcode = ((Label)grvCashFlow.Rows[i].FindControl("lgvactcode")).Text.Trim();
                LinkButton lactDesc = (LinkButton)grvCashFlow.Rows[i].FindControl("lbtnactDesc");
                if (ASTUtility.Right(actcode, 4) != "AAAA")
                    lactDesc.CommandArgument = actcode;

            }



        }
        protected void lbtnactDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string actcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode like('" + actcode + "')";
            dt = dv1.ToTable();
            if (dt.Rows.Count == 0)
                return;

            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;
            string mACTCODE = dt.Rows[0]["actcode"].ToString();
            string mACTDESC = dt.Rows[0]["actdesc"].ToString();
            string lebel2 = dt.Rows[0]["rleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 
            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
                                mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }

        }




        private void ShowProWiseResource()
        {

            Session.Remove("tbPrjStatus");
            string comcod = this.GetComeCode(); ;
            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", "RPTPROJECTWISERES", "", frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvprowres.DataSource = null;
                this.gvprowres.DataBind();
                return;

            }
            // 
            Session["tbPrjStatus"] = HiddenSameData(ds1.Tables[0]);
            this.gvprowres.DataSource = (DataTable)Session["tbPrjStatus"];
            this.gvprowres.DataBind();
            //this.FooteCalculation();

            DataTable dt = (DataTable)Session["tbPrjStatus"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvprowres.FooterRow.FindControl("lgvFiopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iopnam)", "")) ? 0.00 : dt.Compute("sum(iopnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFiaddam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iaddam)", "")) ? 0.00 : dt.Compute("sum(iaddam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFiadjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iadjam)", "")) ? 0.00 : dt.Compute("sum(iadjam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFiclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iclsam)", "")) ? 0.00 : dt.Compute("sum(iclsam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprowres.FooterRow.FindControl("lgvFlopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lopnam)", "")) ? 0.00 : dt.Compute("sum(lopnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFladdam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(laddam)", "")) ? 0.00 : dt.Compute("sum(laddam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFladjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ladjam)", "")) ? 0.00 : dt.Compute("sum(ladjam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFlclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lclsam)", "")) ? 0.00 : dt.Compute("sum(lclsam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprowres.FooterRow.FindControl("lgvFmopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mopnam)", "")) ? 0.00 : dt.Compute("sum(mopnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFmaddam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(maddam)", "")) ? 0.00 : dt.Compute("sum(maddam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFmadjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(madjam)", "")) ? 0.00 : dt.Compute("sum(madjam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFmclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mclsam)", "")) ? 0.00 : dt.Compute("sum(mclsam)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvprowres.FooterRow.FindControl("lgvFsopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sopnam)", "")) ? 0.00 : dt.Compute("sum(sopnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFsaddam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saddam)", "")) ? 0.00 : dt.Compute("sum(saddam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFsadjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sadjam)", "")) ? 0.00 : dt.Compute("sum(sadjam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFsclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sclsam)", "")) ? 0.00 : dt.Compute("sum(sclsam)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvprowres.FooterRow.FindControl("lgvFtopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(topnam)", "")) ? 0.00 : dt.Compute("sum(topnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFtaddam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(taddam)", "")) ? 0.00 : dt.Compute("sum(taddam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFtadjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tadjam)", "")) ? 0.00 : dt.Compute("sum(tadjam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprowres.FooterRow.FindControl("lgvFtclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tclsam)", "")) ? 0.00 : dt.Compute("sum(tclsam)", ""))).ToString("#,##0;(#,##0); ");





        }
        private void ShowCashFlow02()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string Opendate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string Procedure = "SP_REPORT_ACCOUNTS_RP"; //(this.Request.QueryString["Type"] == "CashFlow02") ? "SP_REPORT_ACCOUNTS_RP" : "SP_REPORT_ACCOUNTS_RP_02";
            DataSet ds1 = accData.GetTransInfo(comcod, Procedure, "RPTCASHFLOW02", frmdate, todate, Opendate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvCashFlow02.DataSource = null;
                this.grvCashFlow02.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.grvCashFlow02.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "<br />" + " To " + "<br / >" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            this.grvCashFlow02.Columns[4].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + " To " + "<br / >" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            //    this.grvCashFlow02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvCashFlow02.DataSource = this.HiddenSameData(ds1.Tables[0]);
            this.grvCashFlow02.DataBind();
            ds1.Dispose();
            // this.RPNote02();
            //for (int i = 0; i < grvCashFlow.Rows.Count; i++)
            //{
            //    string actcode = ((Label)grvCashFlow.Rows[i].FindControl("lgvactcode")).Text.Trim();
            //    LinkButton lactDesc = (LinkButton)grvCashFlow.Rows[i].FindControl("lbtnactDesc");
            //    if (ASTUtility.Right(actcode, 4) != "AAAA")
            //        lactDesc.CommandArgument = actcode;

            //}



        }


        protected void gvprowres_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 3;
                cell01.Attributes["style"] = "font-weight:bold; font-size:14px; width:30px; background-color:#5CB85C; color:white; ";



                TableCell cell04 = new TableCell();
                cell04.Text = "Initial & Professional Cost";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 4;
                cell04.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white;";



                TableCell cell05 = new TableCell();
                cell05.Text = "Labour Cost";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 4;
                cell05.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";



                TableCell cell06 = new TableCell();
                cell06.Text = "Material Cost";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 4;
                cell06.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white; ";


                TableCell cell07 = new TableCell();
                cell07.Text = "Site Overhead Cost";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 4;
                cell07.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";




                TableCell cell08 = new TableCell();
                cell08.Text = " Total Cost";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 4;
                cell08.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white; ";








                gvrow.Cells.Add(cell01);

                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);
                gvprowres.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvprowres_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescpwres");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                if (code == "")
                {
                    return;
                }

                // hlink1.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal&prjcode=" + code;

            }

        }
        protected void grvCashFlow02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label Desc = (Label)e.Row.FindControl("lblgvDesccf02");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcf02");
                Label opnam = (Label)e.Row.FindControl("lblgvopnamcf02");
                HyperLink cuamt = (HyperLink)e.Row.FindControl("hlnkgvcuamtcf02");
                // Label ffamt = (Label)e.Row.FindControl("lgvffamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    Desc.Font.Bold = true;
                    closam.Font.Bold = true;
                    opnam.Font.Bold = true;
                    cuamt.Font.Bold = true;

                    // ffamt.Font.Bold = true;
                    Desc.Style.Add("text-align", "right");


                }
                if (code == "28BBBBAAAAAA")
                {

                    cuamt.Style.Add("color", "blue");
                    cuamt.NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtDatefrom.Text + "&Date2=" + this.txtDateto.Text;

                }




            }
        }

        private string compIncomest()
        {

            string compIncomest;
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3333":
                case "3101":
                    compIncomest = "IncomestAlli";
                    break;

                default:
                    compIncomest = "IncomeGen";
                    break;
            }

            return compIncomest;
        }
        protected void GetIncomeStatementForPrint()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string txtCuramt = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + System.Environment.NewLine + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");



            string txtPreamt = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + System.Environment.NewLine + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();


            DataTable dt = (DataTable)ViewState["tblAcc"];

            string lvel = this.DDListLevels.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            // string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3333"://Alliance
                    dv.RowFilter = ("actcode4 not like'%00'");
                    dt = dv.ToTable();

                    break;

                default:

                    break;


            }


            string compIncomest = this.compIncomest();
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.NoteIncoStatement>();
            if (compIncomest == "IncomestAlli")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIncomeSt", list, null, null);
            }

            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIncomeSt", list, null, null);

            }

            Rpt1.EnableExternalImages = true;
            Hashtable reportParm = new Hashtable();
            Rpt1.SetParameters(new ReportParameter("TxtCompName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtCuramt", txtCuramt));
            Rpt1.SetParameters(new ReportParameter("txtPreamt", txtPreamt));
            Rpt1.SetParameters(new ReportParameter("TxtRptPeriod", "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Substring(0, 11)).ToString("dd MMMM yyyy")));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtdate", System.DateTime.Today.ToString("MMMM dd, yyyy")));
            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";








        }
        protected void GetBalanceSheetForPrint()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string mTRNDAT1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            DataTable dt = (DataTable)ViewState["tblAcc"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.Rptspbalancesheet>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetAlli", list, null, null);

            Rpt1.EnableExternalImages = true;
            Hashtable reportParm = new Hashtable();
            Rpt1.SetParameters(new ReportParameter("TxtCompName", comnam));
            Rpt1.SetParameters(new ReportParameter("TxtOpening", Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtClosing", Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtRptPeriod", "As at " + Convert.ToDateTime(mTRNDAT2).ToString("dd MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("txtdate", System.DateTime.Today.ToString("MMMM dd, yyyy")));
            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintShareQty()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblfinst"];
            if (dt1.Rows.Count == 0)
                return;

            var list = dt1.DataTableToList<RealEntity.C_17_Acc.EClassFinanStatement.IncomeStatementSHE>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptShareQty", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("rptTitle", "Statement Of  Changes In Equity"));
            rpt.SetParameters(new ReportParameter("date", "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Substring(0, 11)).ToString("dd MMMM yyyy")));
            rpt.SetParameters(new ReportParameter("openingDate", Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd MMMM yyyy")));
            rpt.SetParameters(new ReportParameter("closingDate", "Balance as at " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd MMMM yyyy")));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblfinst"];
            //if (dt1.Rows.Count == 0)
            //    return;

            //ReportDocument RptShareQty = new RealERPRPT.R_17_Acc.RptShareQty();

            //TextObject TxtCompName = RptShareQty.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //TxtCompName.Text = comnam;

            //TextObject txtdate = RptShareQty.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Substring(0, 11)).ToString("dd MMMM yyyy");
            //TextObject rpttxtopening = RptShareQty.ReportDefinition.ReportObjects["rpttxtopening"] as TextObject;
            //rpttxtopening.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd MMMM yyyy");

            //TextObject rpttxtclosing = RptShareQty.ReportDefinition.ReportObjects["rpttxtclosing"] as TextObject;
            //rpttxtclosing.Text = "Balance as at " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd MMMM yyyy");

            //// txtdate.Text = "(Form " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            //RptShareQty.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //RptShareQty.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = RptShareQty;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptProWisResource()
        {
            string comcod = this.GetComeCode(); ;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            string FTDate = "( From " + frmdate + " To " + todate + " )";



            LocalReport Rpt1 = new LocalReport();
            DataTable Pfinfo = (DataTable)Session["tbPrjStatus"];

            var lst = Pfinfo.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectWisRes>();



            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjectWisResource", lst, null, null);




            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("FTDate", FTDate));
            Rpt1.SetParameters(new ReportParameter("RptTitale", "Project Wise Resource"));
            Rpt1.SetParameters(new ReportParameter("PFooter", printFooter));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptCashFlow02()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)ViewState["recandpayNote"];
            DataTable dt1 = (DataTable)Session["tblbdeposit"];


            var list = dt1.DataTableToList<RealEntity.C_17_Acc.EClassFinanStatement.CashFlowIndirect>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptCashFlow02", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt.SetParameters(new ReportParameter("rptTitle", (this.Request.QueryString["Type"].ToString() == "CashFlow") ? "Statement of Cash Flow" : "Statement of Cash Flow -Indirect"));
            rpt.SetParameters(new ReportParameter("date", "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd MMMM yyyy")));
            rpt.SetParameters(new ReportParameter("closingDate", Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To \n" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy")));
            rpt.SetParameters(new ReportParameter("openingDate", Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To \n" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy")));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            //DataTable dt1 = (DataTable)ViewState["recandpayNote"];





            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptCashFlow02();

            //// rptstate.Subreports["RptBankBalance02.rpt"].SetDataSource(dt1);

            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;
            //string reptype = this.txtflag.Text.Trim().ToString();
            //TextObject TxtHeader = rptstate.ReportDefinition.ReportObjects["TxtHeader"] as TextObject;
            //TxtHeader.Text = (reptype == "CSHFLW") ? "Statement of Cash Flow" : "Statement of Cash Flow -Indirect";

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd MMMM yyyy");
            ////rptftdate.Text = "Date: " + fromdate + " To " + todate;




            //TextObject txtCuramt = rptstate.ReportDefinition.ReportObjects["txtCuramt"] as TextObject;
            //txtCuramt.Text = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            //TextObject txtPreamt = rptstate.ReportDefinition.ReportObjects["txtPreamt"] as TextObject;
            //txtPreamt.Text = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");





            ////TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            ////txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;


            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Cash Flow";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void grvCashFlow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton description = (LinkButton)e.Row.FindControl("lbtnactDesc");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcf");
                Label opnam = (Label)e.Row.FindControl("lblgvopnamcf");
                HyperLink cuamt = (HyperLink)e.Row.FindControl("hlnkgvcuamtcf");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    closam.Font.Bold = true;

                    opnam.Font.Bold = true;
                    cuamt.Font.Bold = true;
                    //  description.Style.Add("text-align", "right");


                }





                if (code == "FFFFAAAAAAAA")
                {

                    cuamt.Style.Add("color", "blue");
                    cuamt.NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtDatefrom.Text + "&Date2=" + this.txtDateto.Text;

                }
                else
                {

                    cuamt.Style.Add("color", "black");


                }



            }
        }

    }
}