using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_22_Sal
{

    public partial class RptSalInterest02 : System.Web.UI.Page
    {
        decimal cinsamount = 0;
        decimal payamount = 0;
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // lblHeadtitle.Text = (this.Request.QueryString["Type"].ToString().Trim() == "interest") ? "DELAY CHARGES CALCULATION INFORMATION"
                //    :(this.Request.QueryString["Type"].ToString().Trim() == "registration")?"REGISTRATION CLEARENCE"
                //    :(this.Request.QueryString["Type"].ToString().Trim() == "CustApp")?"CUSTOMER APPLICATION"
                //    :(this.Request.QueryString["Type"].ToString().Trim() == "DueCollAll")?"DUES COLLECTION STATEMENT(ALL)":"CUSTOMER PAYMENT SCHEDULE";
                //this.ShowView();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = "01" + date.Substring(2);
                this.txttoDate.Text = Convert.ToDateTime(this.txtDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "delay charges 02";


            }
        }
        private void ShowView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "interest":
                    this.ShowDelayRate();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "registration":
                    this.chkPayment.Visible = true;
                    this.lbltoDate.Visible = false;
                    this.txttoDate.Visible = false;
                    this.ShowDelayRate();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "CustApp":
                    this.lblinterest.Visible = false;
                    this.txtinpermonth.Visible = false;
                    this.lblDate.Visible = false;
                    this.txtDate.Visible = false;
                    this.lbltoDate.Visible = false;
                    this.txttoDate.Visible = false;
                    this.lbtnOk.Visible = false;
                    break;
                case "CustNoteSheet":
                    this.lblinterest.Visible = false;
                    this.txtinpermonth.Visible = false;
                    this.lblDate.Visible = false;
                    this.txtDate.Visible = false;
                    this.lbltoDate.Visible = false;
                    this.txttoDate.Visible = false;
                    this.lbtnOk.Visible = false;
                    break;

                case "PaymentSchedule":
                    this.lblinterest.Visible = false;
                    this.txtinpermonth.Visible = false;
                    this.lblDate.Visible = false;
                    this.txtDate.Visible = false;
                    this.lbltoDate.Visible = false;
                    this.txttoDate.Visible = false;
                    this.lbtnOk.Visible = false;
                    break;
                case "DueCollAll":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.chkInvoicePrint.Visible = true;
                    this.lblCustName.Visible = false;
                    this.txtSrcCustomer.Visible = false;
                    this.imgbtnFindCustomer.Visible = false;
                    this.ddlCustName.Visible = false;
                    this.lblinterest.Visible = false;
                    this.txtinpermonth.Visible = false;
                    break;
            }


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];
            if (this.Request.QueryString["Type"].ToString().Trim() == "DueCollAll")
            {
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = "pactcode not like '000000000000%'";
                dt = dv1.ToTable();

            }



            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = dt;
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
        }

        private void GetCustomerName()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();

        }

        private void ShowDelayRate()
        {

            string comcod = this.GetCompCode();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETDELAYRATEPERMON", "", "", "", "", "", "", "", "", "");
            this.txtinpermonth.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["inpermonth"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            ds2.Dispose();

        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            //this.lbljavascript.Text = "";
            switch (Type)
            {
                case "interest":
                    this.lblDelayCharge.Visible = true;
                    this.ShowInterest();

                    break;
                case "registration":
                    this.PanelNarration.Visible = true;
                    this.ShowRegistration();
                    break;
                case "DueCollAll":
                    //this.lbljavascript.Text = "";
                    this.ShowInterestAll();
                    break;



            }



        }

        private void ShowInterest()
        {

            ViewState.Remove("tblinterest");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //  string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            // string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTINTEREST02", pactcode, custid, frmdate, todate, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvInterest.DataSource = null;
                this.gvInterest.DataBind();
                this.gvCDHonour.DataSource = null;
                this.gvCDHonour.DataBind();
                this.gvChqnocl.DataSource = null;
                this.gvChqnocl.DataBind();
                return;
            }



            // DataTable dt = gvInterest_DataBind(ds2);
            ViewState["tblinterest"] = ds2.Tables[0];






            //Dishonour Cheque
            //Session["tblchqdishonour"] = ds2.Tables[3];
            //this.gvCDHonour.DataSource = ds2.Tables[3];
            //this.gvCDHonour.DataBind();
            //if(ds2.Tables[3].Rows.Count>0)
            //    ((Label)this.gvCDHonour.FooterRow.FindControl("lgvFdischarge")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[3].Compute("sum(discharge)", "")) ?
            //                 0 : ds2.Tables[3].Compute("sum(discharge)", ""))).ToString("#,##0;(#,##0); ");
            this.Data_Bind();
            ds2.Dispose();


        }
        private void ShowRegistration()
        {
            ViewState.Remove("tblinterest");

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTCLEARNESS", pactcode, custid, date, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvRegis.DataSource = null;
                this.gvRegis.DataBind();
                return;
            }

            this.txtregRemarks.Text = (ds2.Tables[1].Rows.Count == 0) ? "" : ds2.Tables[1].Rows[0]["rmrks"].ToString();
            //Interest Calculation

            //DataTable dt = gvInterest_DataBind(ds2);
            //double interest = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ?
            //                      0 : dt.Compute("sum(interest)", "")));
            //  DataTable dt1= ds2.Tables[3];
            //  int i;
            //  for (i = 0; i < dt1.Rows.Count; i++)
            //  {
            //      if (dt1.Rows[i]["rescode"].ToString().Trim() == "21000")
            //          break;

            //  }


            //  double Nettotal = Convert.ToDouble(dt1.Rows[i + 1]["amt"].ToString()) + interest;
            //  double paidamt = Convert.ToDouble(dt1.Rows[i + 2]["amt"].ToString());
            //  double Adjustment = Convert.ToDouble(dt1.Rows[i + 4]["amt"].ToString());
            //  dt1.Rows[i]["amt"] = interest;
            //  dt1.Rows[i + 1]["amt"] = Nettotal;
            //  dt1.Rows[i + 3]["amt"] = Nettotal - paidamt;
            //  dt1.Rows[i + 4]["amt"] = Adjustment;
            //  dt1.Rows[i + 5]["amt"] = Nettotal - paidamt -Adjustment;
            ViewState["tblinterest"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
            ds2.Dispose();
        }
        private void ShowInterestAll()
        {

            ViewState.Remove("tblinterest");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();


            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            // string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "RPTINTERESTALL", pactcode, "", frmdate, todate, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvDueCollAll.DataSource = null;
                this.gvDueCollAll.DataBind();
                return;
            }
            ViewState["tblinterest"] = ds2.Tables[0];
            this.Data_Bind();
            for (int i = 0; i < gvDueCollAll.Rows.Count; i++)
            {
                string usircode = ((Label)gvDueCollAll.Rows[i].FindControl("lgvrescode")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvDueCollAll.Rows[i].FindControl("lbok");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = usircode;
                }

            }
            ds2.Dispose();


        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblinterest"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "interest":
                    DataView dv1 = new DataView();
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = ("grp = 'A'");
                    this.gvInterest.DataSource = dv1.ToTable();
                    this.gvInterest.DataBind();
                    this.FooterCal(dv1.ToTable());
                    this.lblchqdishonour.Visible = false;
                    this.lblchqnotyetCleared.Visible = false;


                    //Cheque Not yet Cleared

                    dv1 = dt.DefaultView;
                    dv1.RowFilter = ("grp = 'B'");
                    this.gvChqnocl.DataSource = dv1.ToTable();
                    this.gvChqnocl.DataBind();
                    if (dv1.ToTable().Rows.Count > 0)
                    {
                        this.lblchqnotyetCleared.Visible = true;
                        ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamtbuncr")).Text = Convert.ToDouble((Convert.IsDBNull(dv1.ToTable().Compute("sum(pamount)", "")) ?
                                     0 : dv1.ToTable().Compute("sum(pamount)", ""))).ToString("#,##0;(#,##0); ");
                    }



                    //Dishonour
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = ("grp = 'C'");
                    this.gvCDHonour.DataSource = dv1.ToTable();
                    this.gvCDHonour.DataBind();
                    if (dv1.ToTable().Rows.Count > 0)
                    {
                        this.lblchqdishonour.Visible = true;
                        ((Label)this.gvCDHonour.FooterRow.FindControl("lgvFdischarge")).Text = Convert.ToDouble((Convert.IsDBNull(dv1.ToTable().Compute("sum(discharge)", "")) ?
                                     0 : dv1.ToTable().Compute("sum(discharge)", ""))).ToString("#,##0;(#,##0); ");

                    }

                    break;
                case "registration":
                    this.gvRegis.DataSource = (DataTable)ViewState["tblinterest"];
                    this.gvRegis.DataBind();
                    this.GridColoumnVisible();

                    break;
                case "DueCollAll":
                    this.gvDueCollAll.DataSource = (DataTable)ViewState["tblinterest"];
                    this.gvDueCollAll.DataBind();
                    this.FooterCal(dt);
                    break;



            }



        }
        private void FooterCal(DataTable dt)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "interest":
                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
                                 0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pamount)", "")) ?
                                          0 : dt.Compute("sum(pamount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFcumbalamt")).Text = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["cumbalance"]).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ?
                                          0 : dt.Compute("sum(interest)", ""))).ToString("#,##0;(#,##0); ");


                    double tointerest = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ? 0 : dt.Compute("sum(interest)", "")));
                    double linterest = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["interest"]);
                    double todue = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["dueamt"]);

                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text = (todue + tointerest - linterest).ToString("#,##0;(#,##0); ");



                    break;
                case "registration":
                    break;
                case "DueCollAll":


                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lgvFaptcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aptcost)", "")) ?
                                0 : dt.Compute("sum(aptcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lgvFcpcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cpcost)", "")) ?
                                0 : dt.Compute("sum(cpcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lgvFutilitycost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(utltycost)", "")) ?
                                0 : dt.Compute("sum(utltycost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lgvFothcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othcost)", "")) ?
                                0 : dt.Compute("sum(othcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFTVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalamt)", "")) ?
                                 0 : dt.Compute("sum(totalamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFClrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clramt)", "")) ?
                                          0 : dt.Compute("sum(clramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFUClrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ucamt)", "")) ?
                                          0 : dt.Compute("sum(ucamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFTRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecamt)", "")) ?
                                          0 : dt.Compute("sum(trecamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFInDues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cumbal)", "")) ?
                                 0 : dt.Compute("sum(cumbal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFDlChg")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cumintr)", "")) ?
                                          0 : dt.Compute("sum(cumintr)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFChqDis")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(discharge)", "")) ?
                                          0 : dt.Compute("sum(discharge)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFTDues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamount)", "")) ?
                                          0 : dt.Compute("sum(tamount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDueCollAll.FooterRow.FindControl("lblgvFGDues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gtamt)", "")) ?
                                          0 : dt.Compute("sum(gtamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }

        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlCustName.Text.Trim();
            DataRow[] dr = ((DataTable)ViewState["tblinterest"]).Select("rescode='25AAA'");
            string adjamt = Convert.ToDouble(dr[0]["amt"]).ToString();
            string Remarks = this.txtregRemarks.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT", "INORUPDATESALADJ", PactCode, Usircode, adjamt, Remarks, "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                return;

            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";


        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "interest":
                case "registration":
                    this.PrintInterestAndRegis();
                    break;

                case "CustApp":
                    this.PrintCustomerForm();
                    break;

                case "CustNoteSheet":
                    this.PrintCustomerNoteSheet();
                    break;

                case "PaymentSchedule":
                    PrintPaymentSchedule();
                    break;
                case "DueCollAll":
                    this.RptDuesCollAll();
                    break;
            }

            if (ConstantInfo.LogStatus == true)
            {
                //string eventtype = this.lblHeadtitle.Text;
                string eventdesc = "Print Report";
                string eventdesc2 = Type;
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private DataTable GetMarggeTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("grp", Type.GetType("System.String"));
            dt.Columns.Add("grpdesc", Type.GetType("System.String"));
            dt.Columns.Add("mrno", Type.GetType("System.String"));
            dt.Columns.Add("chqno", Type.GetType("System.String"));
            dt.Columns.Add("cinsdat", Type.GetType("System.DateTime"));
            dt.Columns.Add("cinsam", Type.GetType("System.Double"));
            dt.Columns.Add("trdat", Type.GetType("System.DateTime"));
            dt.Columns.Add("pamount", Type.GetType("System.Double"));
            dt.Columns.Add("insbal", Type.GetType("System.Double"));
            dt.Columns.Add("cumbalance", Type.GetType("System.Double"));
            dt.Columns.Add("interest", Type.GetType("System.Double"));
            dt.Columns.Add("day", Type.GetType("System.Double"));
            dt.Columns.Add("cuminterest", Type.GetType("System.Double"));
            dt.Columns.Add("dueamt", Type.GetType("System.Double"));
            dt.Columns.Add("discharge", Type.GetType("System.Double"));



            DataTable dt1 = (DataTable)ViewState["tblinterest"];
            DataTable dt2 = (DataTable)ViewState["tblchqdishonour"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                DataRow dr1 = dt.NewRow();
                dr1["grp"] = "A";
                dr1["grpdesc"] = "Delay Charge:";
                dr1["mrno"] = "";
                dr1["chqno"] = "";
                dr1["cinsdat"] = dt1.Rows[i]["cinsdat"];
                dr1["cinsam"] = dt1.Rows[i]["cinsam"];
                dr1["trdat"] = dt1.Rows[i]["trdat"];
                dr1["pamount"] = dt1.Rows[i]["pamount"];
                dr1["insbal"] = dt1.Rows[i]["insbal"];
                dr1["cumbalance"] = dt1.Rows[i]["cumbalance"];
                dr1["day"] = dt1.Rows[i]["day"];
                dr1["interest"] = dt1.Rows[i]["interest"];
                dr1["cuminterest"] = dt1.Rows[i]["cuminterest"];
                dr1["dueamt"] = dt1.Rows[i]["dueamt"];
                dr1["discharge"] = 0;
                dt.Rows.Add(dr1);
            }


            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                DataRow dr1 = dt.NewRow();
                dr1["grp"] = "B";
                dr1["grpdesc"] = "Cheque Dishonour:";
                dr1["mrno"] = dt2.Rows[i]["mrno"];
                dr1["chqno"] = dt2.Rows[i]["chqno"];
                dr1["cinsdat"] = dt2.Rows[i]["paydate"];
                dr1["cinsam"] = 0;
                dr1["trdat"] = dt2.Rows[i]["dhonrdate"];
                dr1["pamount"] = dt2.Rows[i]["paidamt"];
                dr1["insbal"] = 0;
                dr1["cumbalance"] = 0;
                dr1["day"] = 0;
                dr1["interest"] = 0;
                dr1["cuminterest"] = 0;
                dr1["dueamt"] = 0;
                dr1["discharge"] = dt2.Rows[i]["discharge"];
                dt.Rows.Add(dr1);
            }



            return dt;

        }


        private ReportDocument PrintCompany()
        {
            string comcod = this.GetCompCode();
            ReportDocument Print = new ReportDocument();
            switch (comcod)
            {
                case "2305":
                case "3305":
                case "3306":
                case "3310":
                case "3311":
                case "3101":
                    Print = new RealERPRPT.R_22_Sal.RptSalRegisClearence02();
                    break;
                default:
                    Print = new RealERPRPT.R_22_Sal.RptSalRegisClearence();
                    break;




            }
            return Print;




        }
        private void PrintInterestAndRegis()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblinterest"];


            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTADDUNIT", pactcode, custid, "", "", "", "", "", "", "");
            if (this.Request.QueryString["Type"].ToString().Trim() == "interest")
            {
                // DataTable dt2 = this.GetMarggeTable();

                string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                //string frmdate = "01-" + ASTUtility.Right(date, 8);
                string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
                ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptSalClntInterest();
                TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompany.Text = comnam;
                TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                txtAddress.Text = comadd;
                TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                txtProjectName.Text = this.ddlProjectName.SelectedItem.Text;

                // DataTable dt3 = (DataTable)Session["tblchqdishonour"]; ;
                double cdishonourcharge = 0.00;
                if (dt1.Rows.Count > 0)
                    cdishonourcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(discharge)", "")) ?
                                 0 : dt1.Compute("sum(discharge)", "")));

                double insamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text);
                double paidamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text);
                double dueamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text);

                double chqnotyetcl = (this.gvChqnocl.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamtbuncr")).Text);
                //insamt = insamt - chqnotyetcl;
                double todueamt = cdishonourcharge + dueamt;
                TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
                txtcustname.Text = this.ddlCustName.SelectedItem.Text;
                TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
                txtCustaddress.Text = ds2.Tables[0].Rows[0]["custadd"].ToString();
                TextObject txtunitdesc = rptstk.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
                txtunitdesc.Text = ds2.Tables[0].Rows[0]["udesc"].ToString();
                TextObject txtbodyarea = rptstk.ReportDefinition.ReportObjects["txtbodyarea"] as TextObject;
                txtbodyarea.Text = "With reference of the above , kindly be informed that you are payble to us an " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy") + " against your above unit total amount of Tk. " + todueamt.ToString("#,##0;(#,##0); ") + " which is as follows:";
                TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "As On " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy");
                TextObject txtdelaycharge = rptstk.ReportDefinition.ReportObjects["txtdelaycharge"] as TextObject;
                txtdelaycharge.Text = "Delay Charge " + this.txtinpermonth.Text.Trim() + " P. M";

                TextObject txtcubbalance = rptstk.ReportDefinition.ReportObjects["txtinsdueamt"] as TextObject;
                txtcubbalance.Text = (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ");
                TextObject txtchnnotcl = rptstk.ReportDefinition.ReportObjects["txtchnnotcl"] as TextObject;
                txtchnnotcl.Text = (chqnotyetcl).ToString("#,##0;(#,##0); ");

                //TextObject txtdueamt = rptstk.ReportDefinition.ReportObjects["txtdueamt"] as TextObject;
                //txtdueamt.Text = dueamt.ToString("#,##0;(#,##0); ");

                TextObject txttoDuesamt = rptstk.ReportDefinition.ReportObjects["txttoDuesamt"] as TextObject;
                txttoDuesamt.Text = todueamt.ToString("#,##0;(#,##0); ");



                TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rptstk.SetDataSource(dt1);
                //string comcod = this.GetComeCode();
                //string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptstk.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptstk;

            }
            else
            {

                double netBalamt = Math.Round(Convert.ToDouble(dt1.Rows[(dt1.Rows.Count) - 2]["amt"]));


                ReportDocument rptstk = this.PrintCompany();
                TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompany.Text = comnam;
                TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                txtAddress.Text = comadd;


                TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = (this.chkPayment.Checked) ? "Payment Clearence" : "Registration Clearence";

                TextObject txtcltype = rptstk.ReportDefinition.ReportObjects["txtcltype"] as TextObject;
                txtcltype.Text = (netBalamt == 0) ? "Orginal" : "Provisional";
                TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                txtProjectName.Text = this.ddlProjectName.SelectedItem.Text;

                TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
                txtcustname.Text = this.ddlCustName.SelectedItem.Text;
                TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
                txtCustaddress.Text = ds2.Tables[0].Rows[0]["custadd"].ToString();
                TextObject txtunitdesc = rptstk.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
                txtunitdesc.Text = ds2.Tables[0].Rows[0]["udesc"].ToString();

                TextObject txtparkingdesc = rptstk.ReportDefinition.ReportObjects["txtparkingdesc"] as TextObject;
                txtparkingdesc.Text = ds2.Tables[0].Rows[0]["parkno"].ToString();

                TextObject txtRemarks = rptstk.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
                txtRemarks.Text = this.txtregRemarks.Text.Trim();

                TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rptstk.SetDataSource(dt1);
                //string comcod = this.GetComeCode();
                //string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptstk.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptstk;

            }


            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private string AppFormType()
        {
            string comcod = this.GetCompCode();
            string formType = "";
            switch (comcod)
            {
                case "3305":
                case "3101":
                    formType = "formType02";
                    break;

                default:
                    formType = "formType01";
                    break;
            }

            return formType;


        }
        private void PrintCustomerForm()
        {

            string formtype = this.AppFormType();


            if (formtype == "formType02")
                this.CustAppForm02();

            else
                this.CustAppForm01();

        }

        private void CustAppForm01()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();

            DataTable dt1 = (DataTable)ViewState["tblinterest"];
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTINFORMATION", pactcode, custid, "", "", "", "", "", "", "");
            ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptCustomerAppform();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            txtAddress.Text = comadd;

            ds2.Tables[0].Rows[0]["tkinwrd"] = ASTUtility.Trans(Convert.ToDouble(ds2.Tables[0].Rows[0]["agrdprice"]), 2);
            //TextObject rpttxtcomweb = rptstk.ReportDefinition.ReportObjects["txtcomweb"] as TextObject;
            //rpttxtcomweb.Text = comweb;
            rptstk.SetDataSource(ds2.Tables[0]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void CustAppForm02()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTINFORMATION", pactcode, custid, "", "", "", "", "", "", "");
            ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptCustomerAppform02();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            txtAddress.Text = comadd;

            rptstk.SetDataSource(ds2.Tables[0]);

            Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintCustomerNoteSheet()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTNOTESHEET", pactcode, custid, "", "", "", "", "", "", "");
            ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptCustNoteSheet();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            txtAddress.Text = comadd;

            TextObject txtnotetitel = rptstk.ReportDefinition.ReportObjects["txtnotetitel"] as TextObject;
            txtnotetitel.Text = "NOTE SHEET FOR THE MONTH OF " + Convert.ToDateTime(System.DateTime.Today.ToString()).ToString("MMM yyyy").ToUpper();
            TextObject txtdateddpart = rptstk.ReportDefinition.ReportObjects["txtdateddpart"] as TextObject;
            txtdateddpart.Text = Convert.ToDateTime(System.DateTime.Today.ToString()).ToString("dd");

            TextObject txtdatemmpart = rptstk.ReportDefinition.ReportObjects["txtdatemmpart"] as TextObject;
            txtdatemmpart.Text = Convert.ToDateTime(System.DateTime.Today.ToString()).ToString("MM");
            TextObject txtdateyearpart = rptstk.ReportDefinition.ReportObjects["txtdateyearpart"] as TextObject;
            txtdateyearpart.Text = Convert.ToDateTime(System.DateTime.Today.ToString()).ToString("yyyy");




            rptstk.SetDataSource(ds2.Tables[0]);

            Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintPaymentSchedule()
        {
            //Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTPAYMENTSCHEDULE", pactcode, custid, "", "", "", "", "", "", "");
            DataTable dt = ds2.Tables[1];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("grp='A'");
            double directcost = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(amt)", "")) ?
                                 0 : dv.ToTable().Compute("sum(amt)", "")));

            LocalReport Rpt1 = new LocalReport();
            var lst = ds2.Tables[1].DataTableToList<RealEntity.C_22_Sal.Sales_BO.PaymentScheduleN>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCustPaySchedule", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("custnam", this.ddlCustName.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("Address", ds2.Tables[0].Rows[0]["paddress"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Telephone", ds2.Tables[0].Rows[0]["telephone"].ToString()));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", ds2.Tables[0].Rows[0]["projectname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("FloorType", ds2.Tables[0].Rows[0]["aptname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Mobile", ds2.Tables[0].Rows[0]["mobile"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Size", ds2.Tables[0].Rows[0]["aptsize"].ToString()));
            Rpt1.SetParameters(new ReportParameter("InWord", "Taka In Word: " + ASTUtility.Trans(directcost, 2)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Payment Schedule"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string comcod = this.GetCompCode();
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string custid = this.ddlCustName.SelectedValue.ToString();
            //DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTPAYMENTSCHEDULE", pactcode, custid, "", "", "", "", "", "", "");
            //ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptCustPaySchedule();
            //DataTable dt=ds2.Tables[1];
            //DataView dv=dt.DefaultView;
            //dv.RowFilter=("grp='A'");
            //double directcost= Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(amt)", "")) ?
            //                     0 : dv.ToTable().Compute("sum(amt)", "")));

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = comadd;
            //TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
            //txtcustname.Text = this.ddlCustName.SelectedItem.Text;
            //TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
            //txtCustaddress.Text = ds2.Tables[0].Rows[0]["paddress"].ToString();
            //TextObject txttelephone = rptstk.ReportDefinition.ReportObjects["txttelephone"] as TextObject;
            //txttelephone.Text = ds2.Tables[0].Rows[0]["telephone"].ToString();
            //TextObject txtmobile = rptstk.ReportDefinition.ReportObjects["txtmobile"] as TextObject;
            //txtmobile.Text = ds2.Tables[0].Rows[0]["mobile"].ToString();
            //TextObject txtProject = rptstk.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            //txtProject.Text = ds2.Tables[0].Rows[0]["projectname"].ToString();
            //TextObject txtusize = rptstk.ReportDefinition.ReportObjects["txtusize"] as TextObject;
            //txtusize.Text = ds2.Tables[0].Rows[0]["aptsize"].ToString();
            //TextObject txtaptdesc = rptstk.ReportDefinition.ReportObjects["txtaptdesc"] as TextObject;
            //txtaptdesc.Text = ds2.Tables[0].Rows[0]["aptname"].ToString();  
            //TextObject txttkInword = rptstk.ReportDefinition.ReportObjects["txttkInword"] as TextObject;
            //txttkInword.Text = "Taka In Word: " + ASTUtility.Trans(directcost, 2);

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(ds2.Tables[1]);
            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            ////this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ////                  this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private void RptDuesCollAll()
        {

            if (this.chkInvoicePrint.Checked)
            {
                this.PrintCustomerInvoice();

            }
            else
            {
                this.PrintRptDuesCollAll();

            }



        }

        private string CompanyInvoice()
        {

            string comcod = this.GetCompCode();
            string invoiceprint = "";
            switch (comcod)
            {
                case "3305":
                case "3306":
                case "3310":
                case "3311":
                case "3101":
                case "2305":
                    invoiceprint = "PInvoice04";
                    break;

                case "3301":
                case "2301":
                case "1301":
                    invoiceprint = "PInvoice03";
                    break;

                default:
                    invoiceprint = "PInvoice04";
                    break;
            }
            return invoiceprint;

        }
        private void PrintCustomerInvoice()
        {

            string invoiceprint = this.CompanyInvoice();

            switch (invoiceprint)
            {
                case "PInvoice03":
                    this.PrintInvoice03();
                    break;
                case "PInvoice04":
                    this.PrintInvoice04();
                    break;



            }




        }

        private void PrintInvoice03()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTINVOICECUSWISE", ProjectCode, frmdate, todate, "", "", "", "", "", "");
            if (ds2 == null)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = ds2.Tables[0].DataTableToList<RealEntity.C_23_CRR.EClassCutomer.InvoicePrint>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.rptCustomerInvoice03", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Invoice"));
            Rpt1.SetParameters(new ReportParameter("txtPayType", "Payment: By an Account Payee Cheque in favour of " + comnam));
            Rpt1.SetParameters(new ReportParameter("txtForCompany", "For " + comnam));
            Rpt1.SetParameters(new ReportParameter("txtBodyArea", "According to agreed payment schdule following payments will be due on " + Convert.ToDateTime(this.txtDate.Text).AddDays(7).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintInvoice04()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();

            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTINVOICECUSWISE", ProjectCode, frmdate, todate, "", "", "", "", "", "");
            if (ds2 == null)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = ds2.Tables[0].DataTableToList<RealEntity.C_23_CRR.EClassCutomer.InvoicePrint>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.rptCustomerInvoice04", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Invoice"));
            Rpt1.SetParameters(new ReportParameter("txtPreDues", "Dues Upto " + Convert.ToDateTime(frmdate).AddDays(-1).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtNote", "This is a computer generated statement and does not required any signature. Please advise the concern person for any discrepancies, if any, within 7 days from date of receipt of this statement. This Statement will otherwise be considered correct.Please ignore this Statement if your payment is already done."));
            Rpt1.SetParameters(new ReportParameter("txtBodyArea", "According to agreed payment schdule following payments will be due on " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtCompInfo", ASTUtility.Cominformation()));
            Session["Report1"] = Rpt1;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintRptDuesCollAll()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblinterest"];
            string comcod = this.GetCompCode();

            LocalReport Rpt1 = new LocalReport();
            var list = dt1.DataTableToList<RealEntity.C_22_Sal.Sales_BO.DuesCollAll>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptDuesCollAll", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Dues Collection Statement (All)"));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", "Project Name: " + this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("txtDate", "As on Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        public DataTable gvInterest_DataBind(DataSet ds)
        {
            //ds contains 3 table 
            //Table[0] contain one column name - [cinsdat] which contain all possible dates into payment schedule and actual payments table
            //Table[1] contain 7 columns name - [comcod], [inscod], [insdes], [pcode], [cinsdat], [cinsam], [cinsrmrk] which contain payment schedule
            //Table[2] contain 4 columns name - [comcod], [pcode], [trdat], [pamount] which contain actual payment details

            DataTable result = this.InterestResult(ds);

            for (int j = 0; j < result.Rows.Count; j++)
            {
                if (j > 0)
                {
                    string date = result.Rows[j]["trdat"].ToString();
                    string matchdate = result.Rows[j - 1]["trdat"].ToString();
                    if (date == matchdate)
                    {
                        result.Rows[j - 1].Delete();
                    }
                }
            }
            //  DataTable finaltable = this.InterestCalculation(result);
            return (this.InterestCalculation(result));

            //InterestGridView.DataSource = finaltable;
            //InterestGridView.DataBind();
            //if (finaltable.Rows.Count > 0)
            //{
            //    ((Label)this.InterestGridView.FooterRow.FindControl("lblgvStTotalIntAmount")).Text =
            //        Convert.ToDouble((Convert.IsDBNull(finaltable.Compute("sum(Interest)", "")) ? 0.00 :
            //       finaltable.Compute("sum(Interest)", ""))).ToString("#,##0.00");
            //}

        }

        private DataTable InterestResult(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            DataTable dt2 = ds.Tables[2];

            DataTable result = new DataTable();
            result.Columns.Add("cinsdat", Type.GetType("System.DateTime"));
            result.Columns.Add("cinsam", Type.GetType("System.Decimal"));
            result.Columns.Add("trdat", Type.GetType("System.DateTime"));
            result.Columns.Add("pamount", Type.GetType("System.Decimal"));
            result.Columns.Add("cumbalance", Type.GetType("System.Decimal"));

            decimal cbalance = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string d = dt.Rows[i]["cinsdat"].ToString();
                DataView cinsdv = new DataView(dt1);
                cinsdv.RowFilter = "cinsdat<='" + d + "'";
                DataTable cinstable = cinsdv.ToTable();

                if (cinstable.Rows.Count > 0)
                {
                    cinsamount = Convert.ToDecimal(cinstable.Compute("sum(cinsam)", "").ToString());
                }
                DataView paymentdv = new DataView(dt2);
                paymentdv.RowFilter = "trdat<='" + d + "'";
                DataTable paymentTable = paymentdv.ToTable();

                if (paymentTable.Rows.Count > 0)
                {
                    payamount = Convert.ToDecimal(paymentTable.Compute("sum(pamount)", "").ToString());
                }
                cbalance = cinsamount - payamount;
                if (cbalance > 0)
                {
                    DateTime paydat = Convert.ToDateTime("01-Jan-1900");
                    decimal payam = 0;
                    DataView cinsmaxdat = new DataView(cinstable);
                    cinsmaxdat.RowFilter = "cinsdat=max(cinsdat)";
                    DataTable cinsmaxdatatable = cinsmaxdat.ToTable();

                    DataView paymentmaxdat = new DataView(paymentTable);
                    paymentmaxdat.RowFilter = "trdat=max(trdat)";
                    DataTable paymentmaxdatatable = paymentmaxdat.ToTable();

                    DateTime schdat = Convert.ToDateTime(cinsmaxdatatable.Rows[0]["cinsdat"].ToString());
                    if (paymentmaxdatatable.Rows.Count > 0)
                    {
                        paydat = Convert.ToDateTime(paymentmaxdatatable.Rows[0]["trdat"].ToString());
                        payam = Convert.ToDecimal(paymentmaxdatatable.Rows[0]["pamount"].ToString());
                    }
                    decimal scham = Convert.ToDecimal(cinsmaxdatatable.Rows[0]["cinsam"].ToString());

                    if (paydat < schdat)
                        this.AddDataToIntTable(schdat, scham, schdat, 0, cbalance, result);
                    else
                        this.AddDataToIntTable(schdat, scham, paydat, payam, cbalance, result);
                }
                if (cbalance == 0)
                {

                    DataView cinsmaxdat = new DataView(cinstable);
                    cinsmaxdat.RowFilter = "cinsdat=max(cinsdat)";
                    DataTable cinsmaxdatatable = cinsmaxdat.ToTable();

                    DataView paymentmaxdat = new DataView(paymentTable);
                    paymentmaxdat.RowFilter = "trdat=max(trdat)";
                    DataTable paymentmaxdatatable = paymentmaxdat.ToTable();

                    DateTime schdat = Convert.ToDateTime(cinsmaxdatatable.Rows[0]["cinsdat"].ToString());
                    decimal scham = 0;
                    for (int row1 = 0; row1 < cinsmaxdatatable.Rows.Count; row1++)
                        scham += Convert.ToDecimal(cinsmaxdatatable.Rows[row1]["cinsam"].ToString());

                    DateTime paydat = Convert.ToDateTime(paymentmaxdatatable.Rows[0]["trdat"].ToString());
                    decimal payam = 0;
                    for (int row = 0; row < paymentmaxdatatable.Rows.Count; row++)
                        payam += Convert.ToDecimal(paymentmaxdatatable.Rows[row]["pamount"].ToString());

                    this.AddDataToIntTable(schdat, scham, paydat, payam, cbalance, result);

                }
                if (cbalance < 0)
                {
                    DateTime schdat = Convert.ToDateTime("01-Jan-1900");
                    decimal scham = 0;
                    DataView cinsmaxdat = new DataView(cinstable);
                    cinsmaxdat.RowFilter = "cinsdat=max(cinsdat)";
                    DataTable cinsmaxdatatable = cinsmaxdat.ToTable();

                    DataView paymentmaxdat = new DataView(paymentTable);
                    paymentmaxdat.RowFilter = "trdat=max(trdat)";
                    DataTable paymentmaxdatatable = paymentmaxdat.ToTable();
                    if (cinsmaxdatatable.Rows.Count > 0)
                    {
                        schdat = Convert.ToDateTime(cinsmaxdatatable.Rows[0]["cinsdat"].ToString());
                        scham = Convert.ToDecimal(cinsmaxdatatable.Rows[0]["cinsam"].ToString());
                    }

                    DateTime paydat = Convert.ToDateTime(paymentmaxdatatable.Rows[0]["trdat"].ToString());

                    decimal payam = Convert.ToDecimal(paymentmaxdatatable.Rows[0]["pamount"].ToString());

                    if (paydat > schdat)
                    {
                        this.AddDataToIntTable(schdat, scham, paydat, payam, cbalance, result);
                    }
                    else if (paydat < schdat)
                    {
                        this.AddDataToIntTable(schdat, scham, schdat, 0, cbalance, result);
                    }
                    else if (paydat == schdat)
                    {
                        this.AddDataToIntTable(schdat, scham, schdat, payam, cbalance, result);
                    }
                }

            }
            return result;
        }

        private void AddDataToIntTable(DateTime cinsdat, decimal scham, DateTime trdat, decimal actam, decimal balance, DataTable myTable)
        {
            DataRow row;
            int i = myTable.Rows.Count;
            row = myTable.NewRow();

            row["cinsdat"] = cinsdat;
            row["cinsam"] = scham;
            row["trdat"] = trdat;
            row["pamount"] = actam;
            row["cumbalance"] = balance;
            myTable.Rows.Add(row);

        }

        private DataTable InterestCalculation(DataTable dt)
        {
            DataTable interestTable = new DataTable();
            interestTable.Columns.Add("cinsdat", Type.GetType("System.DateTime"));
            interestTable.Columns.Add("cinsam", Type.GetType("System.Decimal"));
            interestTable.Columns.Add("trdat", Type.GetType("System.DateTime"));
            interestTable.Columns.Add("pamount", Type.GetType("System.Decimal"));
            interestTable.Columns.Add("insbal", Type.GetType("System.Decimal"));
            interestTable.Columns.Add("cumbalance", Type.GetType("System.Decimal"));
            interestTable.Columns.Add("day", Type.GetType("System.Int32"));
            interestTable.Columns.Add("Interest", Type.GetType("System.Decimal"));
            interestTable.Columns.Add("cuminterest", Type.GetType("System.Decimal"));
            interestTable.Columns.Add("dueamt", Type.GetType("System.Decimal"));
            decimal Perinterest = Convert.ToDecimal("0" + this.txtinpermonth.Text.Trim().Replace("%", ""));
            decimal cuminterest = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime cinsdat = Convert.ToDateTime(dt.Rows[i]["cinsdat"]);
                DateTime trdat = Convert.ToDateTime(dt.Rows[i]["trdat"]);
                decimal cinsam = Convert.ToDecimal(dt.Rows[i]["cinsam"].ToString());
                decimal pamount = Convert.ToDecimal(dt.Rows[i]["pamount"].ToString());
                decimal insbal = cinsam - pamount;
                decimal dueamt = 0;
                decimal cumbalance = Convert.ToDecimal(dt.Rows[i]["cumbalance"].ToString());
                if (i > 0)
                {
                    decimal balance = Convert.ToDecimal(dt.Rows[i - 1]["cumbalance"].ToString());
                    DateTime prevcisdat = Convert.ToDateTime(dt.Rows[i - 1]["cinsdat"]);
                    if (prevcisdat == cinsdat)
                    {
                        cinsdat = Convert.ToDateTime("01-Jan-1900");
                        cinsam = 0;
                    }


                    if (balance > 0)
                    {
                        DateTime prevdate = Convert.ToDateTime(dt.Rows[i - 1]["trdat"].ToString());
                        DateTime date = Convert.ToDateTime(dt.Rows[i]["trdat"].ToString());
                        int day = (int)(date - prevdate).TotalDays;
                        decimal interest = (balance * Perinterest * day) / (100 * 30);
                        cuminterest = interest + cuminterest;
                        dueamt = cumbalance + cuminterest;
                        this.AddDataToInterestTable(cinsdat, cinsam, trdat, pamount, insbal, cumbalance, day, interest, cuminterest, dueamt, interestTable);
                    }
                    else
                    {
                        dueamt = cumbalance + cuminterest;
                        this.AddDataToInterestTable(cinsdat, cinsam, trdat, pamount, insbal, cumbalance, 0, 0, cuminterest, dueamt, interestTable);
                    }
                }
                else
                {
                    this.AddDataToInterestTable(cinsdat, cinsam, trdat, pamount, insbal, cumbalance, 0, 0, 0, 0, interestTable);
                }
            }

            return interestTable;
        }

        private void AddDataToInterestTable(DateTime cinsdat, decimal scham, DateTime trdat, decimal actam, decimal insbal, decimal balance, int day, decimal interest, decimal cuminterest, decimal dueamt, DataTable dt)
        {
            DataRow row;
            int i = dt.Rows.Count;
            row = dt.NewRow();
            row["cinsdat"] = cinsdat;
            row["cinsam"] = scham;
            row["trdat"] = trdat;
            row["pamount"] = actam;
            row["insbal"] = insbal;
            row["cumbalance"] = balance;
            row["day"] = day;
            row["Interest"] = interest;
            row["cuminterest"] = cuminterest;
            row["dueamt"] = dueamt;
            dt.Rows.Add(row);
        }



        protected void gvRegis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvgdesc = (Label)e.Row.FindControl("lgvgdesc");
                TextBox txtamt = (TextBox)e.Row.FindControl("txtgvAmt");
                string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (rescode1 == "")
                {
                    return;
                }

                if (rescode1.Substring(2) == "AAA")
                {
                    lgvgdesc.Font.Bold = true;
                    txtamt.Font.Bold = true;
                }


            }
        }
        protected void lbtnCalculation_Click(object sender, EventArgs e)
        {

            DataTable dt1 = (DataTable)ViewState["tblinterest"];
            int count = dt1.Rows.Count;
            int i;
            for (i = 0; i < this.gvRegis.Rows.Count; i++)
            {
                string rescode = ((Label)this.gvRegis.Rows[i].FindControl("lgvrescode")).Text.Trim();
                if (rescode != "25AAA")
                    continue;
                dt1.Rows[i]["amt"] = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvRegis.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
            }

            double Balance = Convert.ToDouble(dt1.Rows[count - 4]["amt"].ToString());
            double Adjustment = Convert.ToDouble(dt1.Rows[count - 3]["amt"].ToString());
            dt1.Rows[count - 2]["amt"] = (Adjustment == 0) ? Balance : (Balance - Adjustment);
            ViewState["tblinterest"] = dt1;
            this.Data_Bind();

        }


        private void GridColoumnVisible()
        {
            for (int i = 0; i < this.gvRegis.Rows.Count; i++)
            {

                if (((Label)this.gvRegis.Rows[i].FindControl("lgvrescode")).Text.Trim() != "25AAA")
                    ((TextBox)this.gvRegis.Rows[i].FindControl("txtgvAmt")).ReadOnly = true;
            }

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                }


            }
            return dt1;


        }

        //protected void gvDueCollAll_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    if (e.Row.RowType != DataControlRowType.DataRow)
        //        return;

        //    HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
        //    string mCOMCOD = comcod;
        //    string uPACTCODE = this.ddlProjectName.SelectedValue.ToString();
        //    string uSIRCODE = ((Label)e.Row.FindControl("lgvrescode")).Text;
        //    string mTRNDAT1 = this.txtDate.Text;


        //    hlink1.NavigateUrl = "LinkDuesColl.aspx?Type=CustInvoice&pactcode=" + uPACTCODE + "&usircode=" + uSIRCODE + "&Date1=" + mTRNDAT1;
        //}
        protected void lbok_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)ViewState["tblinterest"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dt = dv1.ToTable();

            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtDate.Text;
            string mACTCODE = this.ddlProjectName.SelectedValue.ToString();
            string uSIRCODE = dt.Rows[0]["usircode"].ToString();
            if (uSIRCODE == "")
            {
                return;
            }

            ///---------------------------------//// 

            //lbljavascript.Text = @"<script>window.open('LinkDuesColl.aspx?Type=ClientLedger&pactcode=" + mACTCODE + "&usircode=" +
            //                uSIRCODE + "&Date1=" + mTRNDAT1  + "', target='_blank');</script>";

        }
    }
}
