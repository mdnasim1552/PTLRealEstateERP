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
using System.Drawing;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptSalInterest : System.Web.UI.Page
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
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "interest") ? "DELAY CHARGES "
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "registration") ? "REGISTRATION CLEARENCE"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "CustApp") ? "CUSTOMER APPLICATION"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "DueCollAll") ? "Invoice Print" : "CUSTOMER PAYMENT SCHEDULE";


                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = "01" + date.Substring(2);
                this.txttoDate.Text = Convert.ToDateTime(this.txtDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.ShowView();
                this.GetProjectName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private void ShowView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "interest":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.ShowDelayRate();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "registration":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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
                case "LO":
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


                case "EarlybenADelay":
                    this.ShowDelayRate();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "EarlybenADelay02":
                    this.ShowDelayRate();
                    this.MultiView1.ActiveViewIndex = 4;
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
            string custotype = this.Request.QueryString["Type"].ToString();
            //string calltype = custotype=="LO"? "GETCUSTOMERNAMELANDOWNER" : "GETCUSTOMERNAME";          
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            string islandowner = this.Request.QueryString["Type"] == "LO" ? "1" : "0";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, islandowner, "", "", "", "", "", "");
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
            this.CompanyDaleyRate();

        }
        private void CompanyDaleyRate()
        {

            //Interest Per Month(%):
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "3338":
                case "3101":
                    //case "3366":
                    this.lblinterest.Text = "Interest Per Year(%):";
                    break;


                default:
                    this.lblinterest.Text = "Interest Per Month(%):";
                    break;



            }


        }


        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
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
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    this.ShowInterestAll();
                    break;

                case "EarlybenADelay":
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    this.ShowEarbenADelay();
                    break;

                case "EarlybenADelay02":
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    this.ShowEarbenADelay02();
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
            string permonth = this.txtinpermonth.Text.Trim().Replace("%", "");

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTINTEREST", pactcode, custid, frmdate, todate, permonth, "", "", "", "");
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
            ViewState["tblmarcrdelay"] = ds2.Tables[1];







            //Dishonour Cheque
            //Session["tblchqdishonour"] = ds2.Tables[3];
            //this.gvCDHonour.DataSource = ds2.Tables[3];
            //this.gvCDHonour.DataBind();
            //if(ds2.Tables[3].Rows.Count>0)
            //    ((Label)this.gvCDHonour.FooterRow.FindControl("lgvFdischarge")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[3].Compute("sum(discharge)", "")) ?
            //                 0 : ds2.Tables[3].Compute("sum(discharge)", ""))).ToString("#,##0;(#,##0); ");

            DataTable dt = ds2.Tables[2];

            this.txtentryben.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Select("code='001'")[0]["charge"]).ToString("#,##0.0000;(#,##0.0000); ");
            this.txtdelaychrg.Text = (dt.Rows.Count < 1) ? "" : Convert.ToDouble(dt.Select("code='002'")[0]["charge"]).ToString("#,##0.0000;(#,##0.0000); ");
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

        private void ShowEarbenADelay()
        {

            ViewState.Remove("tblinterest");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //  string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            // string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SHOWEARBENADELAY", pactcode, custid, frmdate, todate, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvearbenadelay.DataSource = null;
                this.gvearbenadelay.DataBind();
                return;
            }




            ViewState["tblinterest"] = this.HiddenSameData(ds2.Tables[0]);








            DataTable dt = ds2.Tables[1];

            this.txtentryben.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Select("code='001'")[0]["charge"]).ToString("#,##0.0000;(#,##0.0000); ");
            this.txtdelaychrg.Text = (dt.Rows.Count < 1) ? "" : Convert.ToDouble(dt.Select("code='002'")[0]["charge"]).ToString("#,##0.0000;(#,##0.0000); ");
            this.Data_Bind();
            ds2.Dispose();


        }

        private void ShowEarbenADelay02()
        {

            ViewState.Remove("tblinterest");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //  string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            // string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SHOWEARBENADELAY02", pactcode, custid, frmdate, todate, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvearbenadelay02.DataSource = null;
                this.gvearbenadelay02.DataBind();
                return;
            }
            ViewState["tblinterest"] = this.HiddenSameData(ds2.Tables[0]);
            ViewState["tblclientsum"] = ds2.Tables[2];
            
            DataTable dt = ds2.Tables[1];
            this.txtentryben.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Select("code='001'")[0]["charge"]).ToString("#,##0.0000;(#,##0.0000); ");
            this.txtdelaychrg.Text = (dt.Rows.Count < 1) ? "" : Convert.ToDouble(dt.Select("code='002'")[0]["charge"]).ToString("#,##0.0000;(#,##0.0000); ");
            this.Data_Bind();

            this.gvinssum.DataSource = ds2.Tables[2];
            this.gvinssum.DataBind();
            ds2.Dispose();


        }
        
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "interest":

                    break;
                case "registration":
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

                    break;
                case "DueCollAll":

                    break;

                case "EarlybenADelay":
                case "EarlybenADelay02":
                    int i = 0;
                    string gcod = dt1.Rows[0]["gcod"].ToString();

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {


                            gcod = dr1["gcod"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["gcod"].ToString() == gcod)
                        {

                            dr1["gdesc"] = "";
                            dr1["cinsam"] = 0.00;

                        }


                        gcod = dr1["gcod"].ToString();
                    }

                    break;




            }




            return dt1;

        }

        private void Data_Bind()
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblinterest"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataView dv1 = new DataView();
            switch (Type)
            {
                case "interest":

                    dv1 = dt.DefaultView;
                    dv1.RowFilter = ("grp = 'A'");
                    this.gvInterest.DataSource = dv1.ToTable();
                    this.gvInterest.DataBind();
                    this.FooterCal(dv1.ToTable());
                    this.lblchqdishonour.Visible = false;
                    this.lblchqnotyetCleared.Visible = false;
                    if (comcod == "3354")
                    {
                        this.gvInterest.Columns[7].Visible = false;
                        this.gvInterest.Columns[8].Visible = false;
                        this.gvInterest.Columns[9].Visible = false;
                        this.gvInterest.Columns[12].Visible = false;
                    }

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


                case "EarlybenADelay":

                    this.gvearbenadelay.DataSource = dt;
                    this.gvearbenadelay.DataBind();
                    this.FooterCal(dt);
                    break;


                case "EarlybenADelay02":

                    this.gvearbenadelay02.DataSource = dt;
                    this.gvearbenadelay02.DataBind();
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
                                 0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;-#,##0;");
                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pamount)", "")) ?
                                          0 : dt.Compute("sum(pamount)", ""))).ToString("#,##0;-#,##0;");
                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFcumbalamt")).Text = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["cumbalance"]).ToString("#,##0;-#,##0;");

                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ?
                                          0 : dt.Compute("sum(interest)", ""))).ToString("#,##0;-#,##0;");


                    double tointerest = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ? 0 : dt.Compute("sum(interest)", "")));
                    double linterest = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["interest"]);
                    double todue = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["dueamt"]);

                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text = (todue + tointerest - linterest).ToString("#,##0;-#,##0;");

                    Session["Report1"] = gvInterest;
                    ((HyperLink)this.gvInterest.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

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


                case "EarlybenADelay":



                    if (dt.Rows.Count > 0)
                    {
                        Session["Report1"] = gvearbenadelay;
                        ((HyperLink)this.gvearbenadelay.FooterRow.FindControl("hlbtntbCdataExeleben")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        ((Label)this.gvearbenadelay.FooterRow.FindControl("lgvFinsamteben")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
                                 0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;-#,##0;");
                        ((Label)this.gvearbenadelay.FooterRow.FindControl("lgvFpayamteben")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pamount)", "")) ?
                                              0 : dt.Compute("sum(pamount)", ""))).ToString("#,##0;-#,##0;");
                        ((Label)this.gvearbenadelay.FooterRow.FindControl("lgvFdelordiseben")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(delodis)", "")) ?
                                                  0 : dt.Compute("sum(delodis)", ""))).ToString("#,##0;-#,##0;");
                    }



                    break;

                case "EarlybenADelay02":



                    if (dt.Rows.Count > 0)
                    {
                        Session["Report1"] = gvearbenadelay02;
                        ((HyperLink)this.gvearbenadelay02.FooterRow.FindControl("hlbtntbCdataExeleben02")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        ((Label)this.gvearbenadelay02.FooterRow.FindControl("lgvFinsamteben02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
                                 0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;-#,##0;");
                        ((Label)this.gvearbenadelay02.FooterRow.FindControl("lgvFpayamteben02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pamount)", "")) ?
                                              0 : dt.Compute("sum(pamount)", ""))).ToString("#,##0;-#,##0;");

                        ((Label)this.gvearbenadelay02.FooterRow.FindControl("lgvFdelamteben02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(delamt)", "")) ?
                                                  0 : dt.Compute("sum(delamt)", ""))).ToString("#,##0;-#,##0;");

                        ((Label)this.gvearbenadelay02.FooterRow.FindControl("lgvFdisamteben02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disamt)", "")) ?
                                                  0 : dt.Compute("sum(disamt)", ""))).ToString("#,##0;-#,##0;");
                   
                    
                    
                    }



                    break;


                    
            }

        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
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
                case "LO":
                    PrintPaymentSchedule();
                    break;
                case "DueCollAll":
                    this.RptDuesCollAll();
                    break;

                case "EarlybenADelay":

                    if (comcod == "3370")
                    {
                        this.RptEarlyBenADelayCPDL();
                    }
                    else
                    {
                        this.RptEarlyBenADelay();
                    }
                    break;

                  
                case "EarlybenADelay02":
                    if (comcod == "3370")
                    {
                        this.RptEarlyBenADelayCPDL();
                    }
                    else
                    {
                        this.RptEarlyBenADelay();
                    }
                    break;


            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = Type;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
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
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt1 = (DataTable)ViewState["tblinterest"];

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();

            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            string date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTADDUNIT", pactcode, custid, todate, "", "", "", "", "", "");
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, date1, "", "", "", "", "", "");
            if (this.Request.QueryString["Type"].ToString().Trim() == "interest")
            {
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                if (comcod == "3330")
                {
                    double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
                    double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
                    double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
                    double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
                    double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
                    double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
                    double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
                    double associafeerec = Convert.ToDouble(ds5.Tables[1].Rows[0]["associarec"]);
                    double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
                    double others = Convert.ToDouble(ds5.Tables[1].Rows[0]["transother"]);

                    //Sales Part  
                    double totalsales = aprment + carparking + utility;
                    double totalreceevable = totalsales + delcharge + modicharge + regisfee + transfee + others;
                    double totalrecived = Convert.ToDouble((Convert.IsDBNull(ds5.Tables[0].Compute("Sum(paidamt)", "")) ? 0.00 : ds5.Tables[0].Compute("Sum(paidamt)", "")));
                    double balance = totalreceevable - totalrecived;

                    // Association part
                    double associabal = assciationfee - associafeerec;
                    double netbal = balance + associabal;
                    double tDelaycharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(interest)", "")) ? 0.00 : dt1.Compute("Sum(interest)", "")));


                    // rdlc start
                    string txtadwrk = Convert.ToDecimal(ds5.Tables[1].Rows[0]["adwrk"]).ToString("#,##0;(#,##0); ");
                    string txttransfeeothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["transother"]).ToString("#,##0;(#,##0); ");
                    string txttotalsales = totalsales.ToString("#,##0;(#,##0); ");
                    string txttotalreceevable = totalreceevable.ToString("#,##0;(#,##0); ");
                    string txttotalrecived = totalrecived.ToString("#,##0;(#,##0); ");
                    string txtbalance = balance.ToString("#,##0;(#,##0); ");

                    string txtcustname = ds5.Tables[1].Rows[0]["name"].ToString();
                    string CustAdd = ds5.Tables[1].Rows[0]["paddress"].ToString();
                    string txtPhone = ds5.Tables[1].Rows[0]["mobile"].ToString();
                    string txtProjectName = ds5.Tables[1].Rows[0]["projectname"].ToString();
                    string txtunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
                    string usize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
                    string txtproaddress = ds5.Tables[1].Rows[0]["prjadd"].ToString();
                    string txtSalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
                    string txtsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
                    string txtagdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");

                    string txtaptprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
                    string txtcarparking = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
                    string txtutility = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");
                    string txtDelaycharge = tDelaycharge.ToString("#,##0;(#,##0);");
                    string txtDuesamt = (tDelaycharge + balance).ToString("#,##0;(#,##0);");
                    string txtbalance1 = balance.ToString("#,##0;(#,##0); ");
                    string txtChargeper = "Chargeable " + this.txtinpermonth.Text + " per month";




                    var list = dt1.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    LocalReport rpt = new LocalReport();
                    rpt = RptSetupClass1.GetLocalReport("R_22_Sal.RptSalClntInterestBr", list, null, null);
                    rpt.EnableExternalImages = true;
                    rpt.SetParameters(new ReportParameter("txtComName", comnam));
                    rpt.SetParameters(new ReportParameter("txtProject", comadd));
                    rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                    rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

                    rpt.SetParameters(new ReportParameter("txtadwrk", txtadwrk));
                    rpt.SetParameters(new ReportParameter("txttransfeeothers", txttransfeeothers));
                    rpt.SetParameters(new ReportParameter("txttotalsales", txttotalsales));
                    rpt.SetParameters(new ReportParameter("txttotalreceevable", txttotalreceevable));
                    rpt.SetParameters(new ReportParameter("txttotalrecived", txttotalrecived));
                    rpt.SetParameters(new ReportParameter("txtbalance", txtbalance));

                    rpt.SetParameters(new ReportParameter("txtcustname", txtcustname));
                    rpt.SetParameters(new ReportParameter("CustAdd", CustAdd));
                    rpt.SetParameters(new ReportParameter("txtPhone", txtPhone));
                    rpt.SetParameters(new ReportParameter("txtProjectName", txtProjectName));
                    rpt.SetParameters(new ReportParameter("txtunitdesc", txtunitdesc));
                    rpt.SetParameters(new ReportParameter("usize", usize));
                    rpt.SetParameters(new ReportParameter("txtproaddress", txtproaddress));
                    rpt.SetParameters(new ReportParameter("txtSalesteam", txtSalesteam));
                    rpt.SetParameters(new ReportParameter("txtsalesdate", txtsalesdate));
                    rpt.SetParameters(new ReportParameter("txtagdate", txtagdate));

                    rpt.SetParameters(new ReportParameter("txtaptprice", txtaptprice));
                    rpt.SetParameters(new ReportParameter("txtcarparking", txtcarparking));
                    rpt.SetParameters(new ReportParameter("txtutility", txtutility));
                    rpt.SetParameters(new ReportParameter("txtDelaycharge", txtDelaycharge));
                    rpt.SetParameters(new ReportParameter("txtDuesamt", txtDuesamt));
                    rpt.SetParameters(new ReportParameter("txtbalance1", txtbalance1));
                    rpt.SetParameters(new ReportParameter("txtChargeper", txtChargeper));


                    Session["Report1"] = rpt;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


                    /*


                    ReportDocument rptStatus = new ReportDocument();
                    rptStatus = new RealERPRPT.R_22_Sal.RptSalClntInterestBr();

                    TextObject rptadwrk = rptStatus.ReportDefinition.ReportObjects["rptadwrk"] as TextObject;
                    rptadwrk.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["adwrk"]).ToString("#,##0;(#,##0); ");

                    //TextObject rptdelchg = rptStatus.ReportDefinition.ReportObjects["rptdelchg"] as TextObject;
                    //rptdelchg.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["delchg"]).ToString("#,##0;(#,##0); ");

                    //TextObject rpttransfee = rptStatus.ReportDefinition.ReportObjects["rpttransfee"] as TextObject;
                    //rpttransfee.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["transfee"]).ToString("#,##0;(#,##0); ");

                    TextObject rpttransfeeothers = rptStatus.ReportDefinition.ReportObjects["rpttransfeeothers"] as TextObject;
                    rpttransfeeothers.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["transother"]).ToString("#,##0;(#,##0); ");

                    TextObject rpttotalsales = rptStatus.ReportDefinition.ReportObjects["rpttotalsales"] as TextObject;
                    rpttotalsales.Text = totalsales.ToString("#,##0;(#,##0); ");

                    TextObject rpttotalreceevable = rptStatus.ReportDefinition.ReportObjects["rpttotalreceevable"] as TextObject;
                    rpttotalreceevable.Text = totalreceevable.ToString("#,##0;(#,##0); ");

                    TextObject rpttotalrecieved = rptStatus.ReportDefinition.ReportObjects["rpttotalrecieved"] as TextObject;
                    rpttotalrecieved.Text = totalrecived.ToString("#,##0;(#,##0); ");

                    TextObject rpttBalance = rptStatus.ReportDefinition.ReportObjects["rpttBalance"] as TextObject;
                    rpttBalance.Text = balance.ToString("#,##0;(#,##0); ");
                    TextObject rpttxtCompanyName = rptStatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                    rpttxtCompanyName.Text = comnam;
                    TextObject rptcompadd = rptStatus.ReportDefinition.ReportObjects["compadd"] as TextObject;
                    rptcompadd.Text = comadd;

                    //string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                    //string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
                    //TextObject txtdate = rptStatus.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                    //txtdate.Text = "Delay Charge As On : " + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy");
                    //TextObject txtDate = rptStatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                    //txtDate.Text = "Print Date: " + this.txtDate.Text.Trim();
                    TextObject rptcustname = rptStatus.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
                    rptcustname.Text = ds5.Tables[1].Rows[0]["name"].ToString();
                    TextObject rptCustAdd = rptStatus.ReportDefinition.ReportObjects["CustAdd"] as TextObject;
                    rptCustAdd.Text = ds5.Tables[1].Rows[0]["paddress"].ToString();

                    TextObject rptCustPhone = rptStatus.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
                    rptCustPhone.Text = ds5.Tables[1].Rows[0]["mobile"].ToString();

                    TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                    rptpactdesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
                    TextObject rptUnitDesc = rptStatus.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
                    rptUnitDesc.Text = ds5.Tables[1].Rows[0]["aptname"].ToString();

                    TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
                    rptUsize.Text = ds5.Tables[1].Rows[0]["aptsize"].ToString();
                    TextObject txtproaddress = rptStatus.ReportDefinition.ReportObjects["txtproaddress"] as TextObject;
                    txtproaddress.Text = ds5.Tables[1].Rows[0]["prjadd"].ToString();


                    TextObject rptSalesteam = rptStatus.ReportDefinition.ReportObjects["Salesteam"] as TextObject;
                    rptSalesteam.Text = ds5.Tables[1].Rows[0]["salesteam"].ToString();

                    TextObject rptsalesdate = rptStatus.ReportDefinition.ReportObjects["salesdate"] as TextObject;
                    rptsalesdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");

                    TextObject rptagreementdate = rptStatus.ReportDefinition.ReportObjects["agreementdate"] as TextObject;
                    rptagreementdate.Text = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
                    //TextObject rptHandoverdate = rptStatus.ReportDefinition.ReportObjects["Handoverdate"] as TextObject;
                    //rptHandoverdate.Text = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

                    TextObject rptapartmentprice = rptStatus.ReportDefinition.ReportObjects["apartmentprice"] as TextObject;
                    rptapartmentprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
                    TextObject rptcarparking = rptStatus.ReportDefinition.ReportObjects["carparking"] as TextObject;
                    rptcarparking.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
                    TextObject rptUtility = rptStatus.ReportDefinition.ReportObjects["Utility"] as TextObject;
                    rptUtility.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");

                    TextObject txtDelaycharge = rptStatus.ReportDefinition.ReportObjects["txtDelaycharge"] as TextObject;
                    txtDelaycharge.Text = tDelaycharge.ToString("#,##0;(#,##0);");

                    TextObject txtDuesamt = rptStatus.ReportDefinition.ReportObjects["txtDuesamt"] as TextObject;
                    txtDuesamt.Text = (tDelaycharge + balance).ToString("#,##0;(#,##0);");

                    TextObject rpttBalance1 = rptStatus.ReportDefinition.ReportObjects["rpttBalance1"] as TextObject;
                    rpttBalance1.Text = balance.ToString("#,##0;(#,##0); ");

                    TextObject txtChargeper = rptStatus.ReportDefinition.ReportObjects["txtChargeper"] as TextObject;
                    txtChargeper.Text = "Chargeable " + this.txtinpermonth.Text + " per month";



                    //TextObject rptdevelopmentcost = rptStatus.ReportDefinition.ReportObjects["developmentcost"] as TextObject;
                    //rptdevelopmentcost.Text = assciationfee.ToString("#,##0;(#,##0); ");




                    //TextObject txtassociafeereceipt = rptStatus.ReportDefinition.ReportObjects["txtassociafeereceipt"] as TextObject;
                    //txtassociafeereceipt.Text = associafeerec.ToString("#,##0;(#,##0); ");

                    //TextObject txtassociabal = rptStatus.ReportDefinition.ReportObjects["txtassociabal"] as TextObject;
                    //txtassociabal.Text = associabal.ToString("#,##0;(#,##0); ");
                    //TextObject txtnetbalance = rptStatus.ReportDefinition.ReportObjects["txtnetbalance"] as TextObject;
                    //txtnetbalance.Text = netbal.ToString("#,##0;(#,##0); ");

                    TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                    rptStatus.SetDataSource(dt1);
                    //string comcod = this.GetComeCode();
                    //string comcod = hst["comcod"].ToString();
                    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                    rptStatus.SetParameterValue("ComLogo", ComLogo);
                    Session["Report1"] = rptStatus;

                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
                 */
                }

                else if (comcod == "3305" || comcod == "2305" || comcod == "3306" || comcod == "3311" || comcod == "3310"  )
                {
                    string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                   
                    string txtProject = "Project Name : " + this.ddlProjectName.SelectedItem.Text;

                    // DataTable dt3 = (DataTable)Session["tblchqdishonour"]; ;
                    double cdishonourcharge = 0.00;
                    if (dt1.Rows.Count > 0)
                        cdishonourcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(discharge)", "")) ?
                                     0 : dt1.Compute("sum(discharge)", "")));

                    DataTable dtmacrdelay = (DataTable)ViewState["tblmarcrdelay"];

                    double insamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text));
                    double paidamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text));
                    double dueamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text));

                    double mardelay = Convert.ToDouble(dtmacrdelay.Rows[0]["mardelay"]);
                    double crdelay = Convert.ToDouble(dtmacrdelay.Rows[0]["crdelay"]);
                    double apramt = Convert.ToDouble(dtmacrdelay.Rows[0]["apramt"]);



                    //Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text));

                    double delcharge = mardelay + crdelay;
                    double chqnotyetcl = (this.gvChqnocl.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamtbuncr")).Text);
                    paidamt = (paidamt > 0) ? paidamt : 0.00;
                    dueamt = (dueamt > 0) ? dueamt : delcharge;
                    cdishonourcharge = (cdishonourcharge > 0) ? cdishonourcharge : 0.00;
                    //insamt = insamt - chqnotyetcl;
                    double todueamt = cdishonourcharge + dueamt - apramt;

                    string txtcustname = this.ddlCustName.SelectedItem.Text;
                    string txtCustaddress = ds2.Tables[0].Rows[0]["custadd"].ToString();
                    string txtunitdesc = "Unit No : " + ds2.Tables[0].Rows[0]["udesc"].ToString();
                    string txtSubject = "Subject: Dues along with cheque dishonour & delay charges";
                    string txtbodyarea = "With reference of the above , kindly be informed that you are payble to us an " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy") + " against your above unit total amount of Tk. " + todueamt.ToString("#,##0;(#,##0); ") + " which is as follows:";
                    string txtdate = "As On " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy");
                    string txtdelaycharge = "Delay Charge " + this.txtinpermonth.Text.Trim() + " P. M";
                    string txtinsdueamt = (insamt - paidamt - chqnotyetcl) > 0 ? (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ") : "";
                    string txtchnnotcl = (chqnotyetcl).ToString("#,##0;(#,##0); ");
                    string txtmardelay = mardelay.ToString("#,##0;(#,##0); ");
                    string txttoDuesamt = todueamt.ToString("#,##0;(#,##0); ");
                    string txtcrdelay = crdelay.ToString("#,##0;(#,##0); ");
                    string totalcharge = ((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text.ToString();
                    string txtdischarge = cdishonourcharge.ToString("#,##0;(#,##0); ");

                    // net dues 

                    double totalcharge1 = Convert.ToDouble(totalcharge);

                    double netdues = totalcharge1 - apramt;
                    


                    DataView dv1 = dt1.Copy().DefaultView;
                    dv1.RowFilter = ("grp='A'");
                    DataTable dt01 = dv1.ToTable();

                    DataView dv2 = dt1.Copy().DefaultView;
                    dv2.RowFilter = ("grp='B'");
                    DataTable dt02 = dv2.ToTable();

                    DataView dv3 = dt1.Copy().DefaultView;
                    dv3.RowFilter = ("grp='C'");
                    DataTable dt03 = dv3.ToTable();


                    var list01 = dt01.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    var list02 = dt02.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    var list03 = dt03.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();


                    LocalReport rpt = new LocalReport();
                    rpt = RptSetupClass1.GetLocalReport("R_22_Sal.RptSalClntInterestRup", list01, list02, list03);
                    rpt.EnableExternalImages = true;
                    rpt.SetParameters(new ReportParameter("comName", comnam));
                    rpt.SetParameters(new ReportParameter("comAddress", comadd));

                    rpt.SetParameters(new ReportParameter("frmdate", frmdate));
                    rpt.SetParameters(new ReportParameter("todate", todate));
                    rpt.SetParameters(new ReportParameter("txtProject", txtProject));
                    rpt.SetParameters(new ReportParameter("txtcustname", txtcustname));
                    rpt.SetParameters(new ReportParameter("txtCustaddress", txtCustaddress));
                    rpt.SetParameters(new ReportParameter("txtunitdesc", txtunitdesc));
                    rpt.SetParameters(new ReportParameter("txtbodyarea", txtbodyarea));
                    rpt.SetParameters(new ReportParameter("txtdate", txtdate));
                    rpt.SetParameters(new ReportParameter("txtdelaycharge", txtdelaycharge));
                    rpt.SetParameters(new ReportParameter("txtinsdueamt", txtinsdueamt));
                    rpt.SetParameters(new ReportParameter("txtchnnotcl", txtchnnotcl));
                    rpt.SetParameters(new ReportParameter("txtmardelay", txtmardelay));
                    rpt.SetParameters(new ReportParameter("txttoDuesamt", txttoDuesamt));
                    rpt.SetParameters(new ReportParameter("txtcrdelay", txtcrdelay));
                    rpt.SetParameters(new ReportParameter("totalcharge", totalcharge));
                    rpt.SetParameters(new ReportParameter("txtSubject", txtSubject));
                    rpt.SetParameters(new ReportParameter("txtdischarge", txtdischarge));
                    rpt.SetParameters(new ReportParameter("txtapramt", apramt.ToString("#,##0;(#,##0); ")));
                    rpt.SetParameters(new ReportParameter("txtnetdues", netdues.ToString("#,##0;(#,##0); ")));



                    rpt.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                    rpt.SetParameters(new ReportParameter("compLogo", ComLogo));

                    Session["Report1"] = rpt;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


                    /*
                    ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptSalClntInterestRup();
                    TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
                    txtCompany.Text = comnam;
                    TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                    txtAddress.Text = comadd;
                    TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                    txtProjectName.Text = this.ddlProjectName.SelectedItem.Text;

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
                    txtcubbalance.Text = (insamt - paidamt - chqnotyetcl) > 0 ? (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ") : "";

                    // txtcubbalance.Text = (Installdue < 0) ? 0 : (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ");   //(insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ") ;


                    TextObject txtchnnotcl = rptstk.ReportDefinition.ReportObjects["txtchnnotcl"] as TextObject;
                    txtchnnotcl.Text = (chqnotyetcl).ToString("#,##0;(#,##0); ");

                    //TextObject txtdueamt = rptstk.ReportDefinition.ReportObjects["txtdueamt"] as TextObject;
                    //txtdueamt.Text = dueamt.ToString("#,##0;(#,##0); ");

                    TextObject txtmardelay = rptstk.ReportDefinition.ReportObjects["txtmardelay"] as TextObject;
                    txtmardelay.Text = mardelay.ToString("#,##0;(#,##0); ");

                    TextObject txttoDuesamt = rptstk.ReportDefinition.ReportObjects["txttoDuesamt"] as TextObject;
                    txttoDuesamt.Text = todueamt.ToString("#,##0;(#,##0); ");

                    TextObject txtcrdelay = rptstk.ReportDefinition.ReportObjects["txtcrdelay"] as TextObject;
                    txtcrdelay.Text = crdelay.ToString("#,##0;(#,##0); ");


                    TextObject totalcharge = rptstk.ReportDefinition.ReportObjects["totalcharge"] as TextObject;
                    totalcharge.Text = ((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text;

                    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                    rptstk.SetDataSource(dt1);



                    //string comcod = this.GetComeCode();
                    //string comcod = hst["comcod"].ToString();
                    string ComLogo2 = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                    rptstk.SetParameterValue("ComLogo", ComLogo2);
                    Session["Report1"] = rptstk;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

                    */

                }

                else if (comcod =="3354" )
                {

                    string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                    //string frmdate = "01-" + ASTUtility.Right(date, 8);
                    

                    double cdishonourcharge = 0.00;
                    double delaycharge = 0.00;
                    if (dt1.Rows.Count > 0)
                    {
                        cdishonourcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(discharge)", "")) ? 0 : dt1.Compute("sum(discharge)", "")));
                        delaycharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interest)", "")) ? 0 : dt1.Compute("sum(interest)", "")));
                    }
                    double insamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text));
                    double paidamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text));
                    double dueamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text));
                    double delcharge = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text));
                    double chqnotyetcl = (this.gvChqnocl.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamtbuncr")).Text);
                    paidamt = (paidamt > 0) ? paidamt : 0.00;
                    dueamt = (dueamt > 0) ? dueamt : delcharge;
                    cdishonourcharge = (cdishonourcharge > 0) ? cdishonourcharge : 0.00;
                    double todueamt = cdishonourcharge + dueamt;

                    string compName = comnam;
                    string txtAddress = comadd;
                    string cusName = this.ddlCustName.SelectedItem.Text;
                    string cusAddress = ds2.Tables[0].Rows[0]["custadd"].ToString();
                    string cusProj = this.ddlProjectName.SelectedItem.Text;
                    string cusUnit = ds2.Tables[0].Rows[0]["udesc"].ToString();
                    string cusSubject = "Subject: Dues along with cheque dishonour & delay charges";
                    string data1 = "With reference of the above , kindly be informed that you are payble to us an " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy") + " against your above unit total amount of Tk. " + todueamt.ToString("#,##0;(#,##0); ") + " which is as follows:";
                    string insdue = (insamt - paidamt - chqnotyetcl) > 0 ? (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ") : "";
                    string chqnotclear = (chqnotyetcl).ToString("#,##0;(#,##0); ");
                    string delcharges = delaycharge.ToString("#,##0;(#,##0); ");
                    string chqdishonor = cdishonourcharge.ToString("#,##0;(#,##0); ");
                    string totaldue = todueamt.ToString("#,##0;(#,##0); ");
                    string date2 = "As On " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy");

                    DataView dv1 = dt1.Copy().DefaultView;
                    dv1.RowFilter = ("grp='A'");
                    DataTable dt01 = dv1.ToTable();

                    DataView dv2 = dt1.Copy().DefaultView;
                    dv2.RowFilter = ("grp='B'");
                    DataTable dt02 = dv2.ToTable();

                    DataView dv3 = dt1.Copy().DefaultView;
                    dv3.RowFilter = ("grp='C'");
                    DataTable dt03 = dv3.ToTable();


                    var list01 = dt01.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    var list02 = dt02.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    var list03 = dt03.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();


                    LocalReport rpt = new LocalReport();
                    rpt = RptSetupClass1.GetLocalReport("R_22_Sal.RptSalClntInterestEdison", list01, list02, list03);
                    rpt.EnableExternalImages = true;
                    rpt.SetParameters(new ReportParameter("compName", compName));
                    rpt.SetParameters(new ReportParameter("txtAddress", txtAddress));
                    rpt.SetParameters(new ReportParameter("cusName", cusName));
                    rpt.SetParameters(new ReportParameter("cusAddress", cusAddress));
                    rpt.SetParameters(new ReportParameter("cusProj", cusProj));
                    rpt.SetParameters(new ReportParameter("cusUnit", cusUnit));
                    rpt.SetParameters(new ReportParameter("cusSubject", cusSubject));
                    rpt.SetParameters(new ReportParameter("data1", data1));
                    rpt.SetParameters(new ReportParameter("insdue", insdue));
                    rpt.SetParameters(new ReportParameter("chqnotclear", chqnotclear));
                    rpt.SetParameters(new ReportParameter("delcharge", delcharges));
                    rpt.SetParameters(new ReportParameter("chqdishonor", chqdishonor));
                    rpt.SetParameters(new ReportParameter("totaldue", totaldue));
                    rpt.SetParameters(new ReportParameter("date1", date2));
                    rpt.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                    rpt.SetParameters(new ReportParameter("compLogo", ComLogo));

                    Session["Report1"] = rpt;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


                }

                else if(comcod == "3368" || comcod == "3101")
                {

                    string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");


                    double cdishonourcharge = 0.00;
                    double delaycharge = 0.00;
                    if (dt1.Rows.Count > 0)
                    {
                        cdishonourcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(discharge)", "")) ? 0 : dt1.Compute("sum(discharge)", "")));
                        delaycharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interest)", "")) ? 0 : dt1.Compute("sum(interest)", "")));
                    }


                    double insamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text));
                    double paidamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text));
                    double dueamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text));
                    double delcharge = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text));
                    double chqnotyetcl = (this.gvChqnocl.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamtbuncr")).Text);
                    paidamt = (paidamt > 0) ? paidamt : 0.00;
                    dueamt = (dueamt > 0) ? dueamt : delcharge;
                    cdishonourcharge = (cdishonourcharge > 0) ? cdishonourcharge : 0.00;

                    double todueamt = cdishonourcharge + dueamt;

                    string compName = comnam;
                    string txtAddress = comadd;
                    string cusName = this.ddlCustName.SelectedItem.Text.Substring(0,9);
                    string projectName = this.ddlProjectName.SelectedItem.Text;
                   
                    string insperyr = this.txtinpermonth.Text.Trim();
                    string cusAddress = ds2.Tables[0].Rows[0]["custadd"].ToString(); 
                    string cuspreAddress = ds2.Tables[0].Rows[0]["custprsntadd"].ToString();
                    string cusProj = this.ddlProjectName.SelectedItem.Text;
                    string cusUnit = ds2.Tables[0].Rows[0]["udesc"].ToString();
                    string utilityamt = Convert.ToDouble(ds2.Tables[1].Rows[0]["utilityamt"]).ToString("#,##0;(#,##0); ");
                    string cooperative = Convert.ToDouble(ds2.Tables[1].Rows[0]["cooperative"]).ToString("#,##0;(#,##0); ");
                    string outstanbal = Convert.ToDouble(ds2.Tables[1].Rows[0]["outstanbal"]).ToString("#,##0;(#,##0); ");
                    string paraamt = Convert.ToDouble(ds2.Tables[1].Rows[0]["paramt"]).ToString("#,##0;(#,##0); ");
                    string costamt = Convert.ToDouble(ds2.Tables[1].Rows[0]["costamt"]).ToString("#,##0;(#,##0); ");
                    string ramt = Convert.ToDouble(ds2.Tables[1].Rows[0]["ramt"]).ToString("#,##0;(#,##0); ");
                    string balance =Convert.ToDouble(ds2.Tables[1].Rows[0]["balance"]).ToString("#,##0;(#,##0); ");
                    string chqinamt = Convert.ToDouble(ds2.Tables[1].Rows[0]["chqinamt"]).ToString("#,##0;(#,##0); ");
                    string uamt = Convert.ToDouble(ds2.Tables[1].Rows[0]["uamt"]).ToString("#,##0;(#,##0); ");
                    string parkno = ds2.Tables[0].Rows[0]["parkno"].ToString();
                    string actdesc = ds2.Tables[0].Rows[0]["actdesc"].ToString();
                    string mobno = ds2.Tables[0].Rows[0]["mobno"].ToString();
                    string custname = ds2.Tables[0].Rows[0]["custname"].ToString();
                    string usize = Convert.ToDouble(ds2.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0); ");



                    string cusSubject = "Subject: Your Payment history";
                   
                    string data1 = "With reference of the above , kindly be informed that you are payble to us an " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy") + " against your above unit total amount of Tk. " + todueamt.ToString("#,##0;(#,##0); ") + " which is as follows:";
                    string insdue = (insamt - paidamt - chqnotyetcl) > 0 ? (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ") : "";
                    string chqnotclear = (chqnotyetcl).ToString("#,##0;(#,##0); ");
                    string delcharges = delaycharge.ToString("#,##0;(#,##0); ");
                    string chqdishonor = cdishonourcharge.ToString("#,##0;(#,##0); ");
                    string totaldue = todueamt.ToString("#,##0;(#,##0); ");
                    string date2 = "As On " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy");

                    DataView dv1 = dt1.Copy().DefaultView;
                    dv1.RowFilter = ("grp='A'");
                    DataTable dt01 = dv1.ToTable();

                    DataView dv2 = dt1.Copy().DefaultView;
                    dv2.RowFilter = ("grp='B'");
                    DataTable dt02 = dv2.ToTable();

                    DataView dv3 = dt1.Copy().DefaultView;
                    dv3.RowFilter = ("grp='C'");
                    DataTable dt03 = dv3.ToTable();

                    string pamount = Convert.ToDouble((Convert.IsDBNull(dt01.Compute("sum(pamount)", "")) ? 0 : dt01.Compute("sum(pamount)", ""))).ToString("#,##0;(#,##0); ");
                    var list01 = dt01.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    var list02 = dt02.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    var list03 = dt03.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();


                    LocalReport rpt = new LocalReport();
                    rpt = RptSetupClass1.GetLocalReport("R_22_Sal.RptSalClntInterestFinlay", list01, list02, list03);
                    rpt.EnableExternalImages = true;
                    
                    rpt.SetParameters(new ReportParameter("compName", compName));
                    rpt.SetParameters(new ReportParameter("txtAddress", txtAddress));
                    rpt.SetParameters(new ReportParameter("cusName", cusName));
                    rpt.SetParameters(new ReportParameter("insperyr", insperyr));
                    rpt.SetParameters(new ReportParameter("RptTitle", "Payment Status with delay charge"));
                    rpt.SetParameters(new ReportParameter("NutCal", "Monthly"));
                    rpt.SetParameters(new ReportParameter("TotalRUtilityDel", ""));
                    rpt.SetParameters(new ReportParameter("projectName", projectName));
                    rpt.SetParameters(new ReportParameter("actdesc", actdesc));
                    rpt.SetParameters(new ReportParameter("mobno", mobno));
                    rpt.SetParameters(new ReportParameter("pamount", mobno));
                    rpt.SetParameters(new ReportParameter("usize", usize));
                    rpt.SetParameters(new ReportParameter("custname", custname));
                    //
                    rpt.SetParameters(new ReportParameter("utilityamt", utilityamt));
                    rpt.SetParameters(new ReportParameter("pamount", pamount));
                    rpt.SetParameters(new ReportParameter("balance", balance));
                    rpt.SetParameters(new ReportParameter("cooperative", cooperative));
                    rpt.SetParameters(new ReportParameter("outstanbal", outstanbal));
                    rpt.SetParameters(new ReportParameter("costamt", costamt));
                    rpt.SetParameters(new ReportParameter("paraamt", paraamt));
                    rpt.SetParameters(new ReportParameter("ramt", ramt));
                    rpt.SetParameters(new ReportParameter("chqinamt", chqinamt));
                    rpt.SetParameters(new ReportParameter("uamt", uamt));
                    rpt.SetParameters(new ReportParameter("parkno", parkno));
                   //
                    rpt.SetParameters(new ReportParameter("cusAddress", cuspreAddress));
                    rpt.SetParameters(new ReportParameter("cusProj", cusProj));
                    rpt.SetParameters(new ReportParameter("cusUnit", cusUnit));
                    rpt.SetParameters(new ReportParameter("cusSubject", cusSubject));
                    rpt.SetParameters(new ReportParameter("data1", data1));
                    rpt.SetParameters(new ReportParameter("insdue", insdue));
                    rpt.SetParameters(new ReportParameter("chqnotclear", chqnotclear));
                    rpt.SetParameters(new ReportParameter("delcharge", delcharges));
                    rpt.SetParameters(new ReportParameter("chqdishonor", chqdishonor));
                    rpt.SetParameters(new ReportParameter("totaldue", totaldue));
                    rpt.SetParameters(new ReportParameter("date1", date2));


                    rpt.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                    rpt.SetParameters(new ReportParameter("compLogo", ComLogo));

                    Session["Report1"] = rpt;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
                }
                else
                {

                    string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                   

                    double cdishonourcharge = 0.00;
                    double delaycharge = 0.00;
                    if (dt1.Rows.Count > 0)
                    {
                        cdishonourcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(discharge)", "")) ? 0 : dt1.Compute("sum(discharge)", "")));
                        delaycharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interest)", "")) ? 0 : dt1.Compute("sum(interest)", "")));
                    }


                    double insamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text));
                    double paidamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text));
                    double dueamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text));
                    double delcharge = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text));
                    double chqnotyetcl = (this.gvChqnocl.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamtbuncr")).Text);
                    paidamt = (paidamt > 0) ? paidamt : 0.00;
                    dueamt = (dueamt > 0) ? dueamt : delcharge;
                    cdishonourcharge = (cdishonourcharge > 0) ? cdishonourcharge : 0.00;

                    double todueamt = cdishonourcharge + dueamt;

                    string compName = comnam;
                    string txtAddress = comadd;
                    string cusName = this.ddlCustName.SelectedItem.Text;
                   
                    string cusAddress = ds2.Tables[0].Rows[0]["custadd"].ToString();
                    string cusProj = this.ddlProjectName.SelectedItem.Text;
                    string cusUnit = ds2.Tables[0].Rows[0]["udesc"].ToString();
                    string cusSubject = "Subject: Dues along with cheque dishonour & delay charges";
                    string data1 = "With reference of the above , kindly be informed that you are payble to us an " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy") + " against your above unit total amount of Tk. " + todueamt.ToString("#,##0;(#,##0); ") + " which is as follows:";
                    string insdue = (insamt - paidamt - chqnotyetcl) > 0 ? (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ") : "";
                    string chqnotclear = (chqnotyetcl).ToString("#,##0;(#,##0); ");
                    string delcharges = delaycharge.ToString("#,##0;(#,##0); ");
                    string chqdishonor = cdishonourcharge.ToString("#,##0;(#,##0); ");
                    string totaldue = todueamt.ToString("#,##0;(#,##0); ");
                    string date2 = "As On " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy");

                    DataView dv1 = dt1.Copy().DefaultView;
                    dv1.RowFilter = ("grp='A'");
                    DataTable dt01 = dv1.ToTable();

                    DataView dv2 = dt1.Copy().DefaultView;
                    dv2.RowFilter = ("grp='B'");
                    DataTable dt02 = dv2.ToTable();

                    DataView dv3 = dt1.Copy().DefaultView;
                    dv3.RowFilter = ("grp='C'");
                    DataTable dt03 = dv3.ToTable();


                    var list01 = dt01.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    var list02 = dt02.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();
                    var list03 = dt03.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInterest>();


                    LocalReport rpt = new LocalReport();
                    rpt = RptSetupClass1.GetLocalReport("R_22_Sal.RptSalClntInterest", list01, list02, list03);
                    rpt.EnableExternalImages = true;
                    rpt.SetParameters(new ReportParameter("compName", compName));
                    rpt.SetParameters(new ReportParameter("txtAddress", txtAddress));
                    rpt.SetParameters(new ReportParameter("cusName", cusName));
                    rpt.SetParameters(new ReportParameter("cusAddress", cusAddress));
                    rpt.SetParameters(new ReportParameter("cusProj", cusProj));
                    rpt.SetParameters(new ReportParameter("cusUnit", cusUnit));
                    rpt.SetParameters(new ReportParameter("cusSubject", cusSubject));
                    rpt.SetParameters(new ReportParameter("data1", data1));
                    rpt.SetParameters(new ReportParameter("insdue", insdue));
                    rpt.SetParameters(new ReportParameter("chqnotclear", chqnotclear));
                    rpt.SetParameters(new ReportParameter("delcharge", delcharges));
                    rpt.SetParameters(new ReportParameter("chqdishonor", chqdishonor));
                    rpt.SetParameters(new ReportParameter("totaldue", totaldue));
                    rpt.SetParameters(new ReportParameter("date1", date2));


                    rpt.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                    rpt.SetParameters(new ReportParameter("compLogo", ComLogo));

                    Session["Report1"] = rpt;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



                    ////ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptSalClntInterest();

                    ////TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
                    ////txtCompany.Text = comnam;
                    ////TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                    ////txtAddress.Text = comadd;
                    ////TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                    ////txtProjectName.Text = this.ddlProjectName.SelectedItem.Text;

                    //// DataTable dt3 = (DataTable)Session["tblchqdishonour"]; ;


                    ////TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
                    ////txtcustname.Text = this.ddlCustName.SelectedItem.Text;
                    ////TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
                    ////txtCustaddress.Text = ds2.Tables[0].Rows[0]["custadd"].ToString();
                    ////TextObject txtunitdesc = rptstk.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
                    ////txtunitdesc.Text = ds2.Tables[0].Rows[0]["udesc"].ToString();
                    ////TextObject txtbodyarea = rptstk.ReportDefinition.ReportObjects["txtbodyarea"] as TextObject;
                    ////txtbodyarea.Text = "With reference of the above , kindly be informed that you are payble to us an " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy") + " against your above unit total amount of Tk. " + todueamt.ToString("#,##0;(#,##0); ") + " which is as follows:";               
                    ////TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                    ////txtdate.Text = "As On " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy");

                    //TextObject txtdelaycharge = rptstk.ReportDefinition.ReportObjects["txtdelaycharge"] as TextObject;
                    //txtdelaycharge.Text = "Delay Charge " + this.txtinpermonth.Text.Trim() + ((comcod == "3338") ? "P. Y" : "P. M");

                    ////TextObject txtcubbalance = rptstk.ReportDefinition.ReportObjects["txtinsdueamt"] as TextObject;
                    ////txtcubbalance.Text = (insamt - paidamt - chqnotyetcl) > 0 ? (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ") : "";               
                    ////TextObject txtchnnotcl = rptstk.ReportDefinition.ReportObjects["txtchnnotcl"] as TextObject;
                    ////txtchnnotcl.Text = (chqnotyetcl).ToString("#,##0;(#,##0); ");.

                    ////TextObject txtdueamt = rptstk.ReportDefinition.ReportObjects["txtdueamt"] as TextObject;
                    ////txtdueamt.Text = dueamt.ToString("#,##0;(#,##0); ");

                    //TextObject txttoDuesamt = rptstk.ReportDefinition.ReportObjects["txttoDuesamt"] as TextObject;
                    //txttoDuesamt.Text = todueamt.ToString("#,##0;(#,##0); ");

                    //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                    //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                    //rptstk.SetDataSource(dt1);
                    ////string comcod = this.GetComeCode();
                    ////string comcod = hst["comcod"].ToString();
                    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                    //rptstk.SetParameterValue("ComLogo", ComLogo);
                    //Session["Report1"] = rptstk;

                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                    //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

                }

                // DataTable dt2 = this.GetMarggeTable();
            }
            else
            {

                double netBalamt = Math.Round(Convert.ToDouble(dt1.Rows[(dt1.Rows.Count) - 2]["amt"]));

                //Rdlc
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string rpttxtCompanyName = comnam;
                string rptcompadd = comadd;
                string txtTitle = (this.chkPayment.Checked) ? "Payment Clearence" : "Registration Clearence";
                string txtcltype = (netBalamt == 0) ? "Orginal" : "Provisional";
                string txtProjectName = this.ddlProjectName.SelectedItem.Text;
                string txtcustname = this.ddlCustName.SelectedItem.Text;
                string txtCustaddress = ds2.Tables[0].Rows[0]["custadd"].ToString();
                string txtunitdesc = ds2.Tables[0].Rows[0]["udesc"].ToString();
                string txtparkingdesc = ds2.Tables[0].Rows[0]["parkno"].ToString();
                string txtRemarks = this.txtregRemarks.Text.Trim();
                string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

                var lst = dt1.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassSaleRegisClearance>();

                LocalReport RptSalRegisClearence = new LocalReport();

                switch (comcod)
                {
                    case "2305":
                    case "3305":
                    case "3306":
                    case "3310":
                    case "3311":
                    case "3101":
                        RptSalRegisClearence = RptSetupClass1.GetLocalReport("R_22_Sal.RptSalRegisClearence02", lst, null, null);
                        break;
                    default:
                        RptSalRegisClearence = RptSetupClass1.GetLocalReport("R_22_Sal.RptSalRegisClearence", lst, null, null);
                        break;
                }


                RptSalRegisClearence.EnableExternalImages = true;
                RptSalRegisClearence.SetParameters(new ReportParameter("ComLogo", ComLogo));
                RptSalRegisClearence.SetParameters(new ReportParameter("rpttxtCompanyName", comnam));
                RptSalRegisClearence.SetParameters(new ReportParameter("rptcompadd", rptcompadd));
                RptSalRegisClearence.SetParameters(new ReportParameter("title", "Payment/Registration Clearance"));
                RptSalRegisClearence.SetParameters(new ReportParameter("txtcltype", txtcltype));
                RptSalRegisClearence.SetParameters(new ReportParameter("txtProjectName", txtProjectName));
                RptSalRegisClearence.SetParameters(new ReportParameter("txtcustname", txtcustname));
                RptSalRegisClearence.SetParameters(new ReportParameter("txtCustaddress", txtCustaddress));
                RptSalRegisClearence.SetParameters(new ReportParameter("txtunitdesc", txtunitdesc));
                RptSalRegisClearence.SetParameters(new ReportParameter("txtparkingdesc", txtparkingdesc));
                RptSalRegisClearence.SetParameters(new ReportParameter("txtRemarks", txtRemarks));
                RptSalRegisClearence.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
                Session["Report1"] = RptSalRegisClearence;

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
        }     

        private string AppFormType()
        {
            string comcod = this.GetCompCode();
            string formType = "";
            switch (comcod)
            {
                case "3336":
                case "3637":
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void CustAppForm02()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTINFORMATION", pactcode, custid, "", "", "", "", "", "", "");

            DataTable dt = ds2.Tables[0];

            var list = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCustApp>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_22_Sal.RptCustApp", list, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comlogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            //Rpt1.SetParameters (new ReportParameter ("txtTitle", title));
            //Rpt1.SetParameters (new ReportParameter ("txtMark", mark));
            //Rpt1.SetParameters (new ReportParameter ("txtEmpname", empname));
            //Rpt1.SetParameters (new ReportParameter ("txtProj", proj));
            //Rpt1.SetParameters (new ReportParameter ("txtPer", per));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintPaymentSchedule()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comfadd = hst["comadd"].ToString().Replace("<br />", "\n");
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTPAYMENTSCHEDULE", pactcode, custid, "", "", "", "", "", "", "");
            if (ds2 == null || ds2.Tables[0].Rows.Count == 0 || ds2.Tables[1].Rows.Count == 0)
                return;
            DataTable dt = ds2.Tables[1];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("grp='A'");
            double directcost = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(amt)", "")) ?
                                 0 : dv.ToTable().Compute("sum(amt)", "")));

            LocalReport Rpt1 = new LocalReport();
            var lst = ds2.Tables[1].DataTableToList<RealEntity.C_22_Sal.Sales_BO.PaymentScheduleN>();

            string address = "";
            string sign1 = "", sign2 = "", sign3 = "", sign4 = "";
            //Land Owner
            string projectname = this.Request.QueryString["Type"].ToString() == "LO" ? ds2.Tables[0].Rows[0]["projectname"].ToString().Substring(3) + " (L/O PART)" : ds2.Tables[0].Rows[0]["projectname"].ToString().Substring(3);
            string bookno = Convert.ToDateTime(ds2.Tables[0].Rows[0]["bookdate"].ToString()).ToString("dd-MMM-yyyy");
            string enrolno = (bookno == "01-Jan-1900") ? " " : bookno;
            string leader = ds2.Tables[0].Rows[0]["temleadname"].ToString();


            switch (comcod)
            {

                // finlay 
                case "3368":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCustPaySchedule", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    address = ds2.Tables[0].Rows[0]["presentadd"].ToString();
                    Rpt1.SetParameters(new ReportParameter("notice", "N:B.In case of default of any payment within due date, delay charge will be applicable as per company policy"));
                    break;

                //case "3101": // epic 

                case "3367":
                    sign1 = ds2.Tables[0].Rows[0]["name"].ToString() + "\n" + "Customer";
                    sign2 = ds2.Tables[0].Rows[0]["usrname"].ToString() + "\n" + ds2.Tables[0].Rows[0]["usrdesig"].ToString();
                    sign3 = "Kazi Abdul Hamid" + "\n" + "AGM Sales & Marketing";
                    sign4 = "Approved By" + "\n" + "Director / Managing Director";

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCustPayScheduleEpic", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    address = ds2.Tables[0].Rows[0]["paddress"].ToString();
                    Rpt1.SetParameters(new ReportParameter("sign1", sign1));
                    Rpt1.SetParameters(new ReportParameter("sign2", sign2));
                    Rpt1.SetParameters(new ReportParameter("sign3", sign3));
                    Rpt1.SetParameters(new ReportParameter("sign4", sign4));
                    break;

                case "3370":

                    Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptCustPayScheduleCPDL", lst, null, null);
                    Rpt1.EnableExternalImages = true;                

                    Rpt1.SetParameters(new ReportParameter("apttype", ds2.Tables[0].Rows[0]["apttype"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("comfadd", comfadd));
                    Rpt1.SetParameters(new ReportParameter("leader", leader));

                    Rpt1.SetParameters(new ReportParameter("flrdesc", ds2.Tables[0].Rows[0]["flrdesc"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("bookdate", enrolno));
                    Rpt1.SetParameters(new ReportParameter("bookno", ds2.Tables[0].Rows[0]["bookno"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("prjadd", ds2.Tables[0].Rows[0]["prjadd"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("padrss", ds2.Tables[0].Rows[0]["presentadd"].ToString()));

                    Rpt1.SetParameters(new ReportParameter("custid", ds2.Tables[0].Rows[0]["customerno"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("txtremarks", ds2.Tables[0].Rows[0]["remarks"].ToString()));
                    //if (ds2.Tables[0].Rows[0]["customerno"].ToString() == "") {
                    //    Rpt1.SetParameters(new ReportParameter("custid", ds2.Tables[0].Rows[0]["usircode"].ToString()));
                    //}



                    break;
                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCustPaySchedule", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    address = ds2.Tables[0].Rows[0]["paddress"].ToString();
                    break;
            }



         

                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("custnam", ds2.Tables[0].Rows[0]["name"].ToString()));
                Rpt1.SetParameters(new ReportParameter("Address", address));
                Rpt1.SetParameters(new ReportParameter("Telephone", ds2.Tables[0].Rows[0]["telephone"].ToString()));
                Rpt1.SetParameters(new ReportParameter("ProjectNam", projectname));
                Rpt1.SetParameters(new ReportParameter("FloorType", ds2.Tables[0].Rows[0]["aptname"].ToString()));
                Rpt1.SetParameters(new ReportParameter("Mobile", ds2.Tables[0].Rows[0]["mobile"].ToString()));
                Rpt1.SetParameters(new ReportParameter("Size", ds2.Tables[0].Rows[0]["aptsize"].ToString()));
                Rpt1.SetParameters(new ReportParameter("InWord", "Taka In Word: " + ASTUtility.Trans(directcost, 2)));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "PAYMENT SCHEDULE"));
                Rpt1.SetParameters(new ReportParameter("AppNo", ds2.Tables[0].Rows[0]["aptname"].ToString()));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
               

         
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


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
        private void RptEarlyBenADelayCPDL()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string project = this.ddlProjectName.SelectedItem.Text.Trim();
            string customer = this.ddlCustName.SelectedItem.Text.Trim();
            string unit =  ASTUtility.Right(this.ddlCustName.SelectedItem.Text.Trim(), 8);
            double delcrg = Convert.ToDouble(this.txtdelaychrg.Text.Trim())/100 ;
            double entben = Convert.ToDouble(this.txtentryben.Text.Trim())/100;
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)ViewState["tblinterest"];
            DataTable dt1 = (DataTable)ViewState["tblclientsum"];

            List<RealEntity.C_22_Sal.EClassSales_02.EClassInterestDummyPay02> lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassInterestDummyPay02>();
            List<RealEntity.C_22_Sal.EClassSales_02.EClassClientSum> lst1 = dt1.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassClientSum>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEarlybenefitADelayCPDL", lst, lst1, null);
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptHead", "INDIVITUAL CLIENTS STATEMENT"));
            Rpt1.SetParameters(new ReportParameter("ProjName", "Name of Project  : " + project));
            Rpt1.SetParameters(new ReportParameter("customer","Client Name : " +customer));
            Rpt1.SetParameters(new ReportParameter("Unit", "Apartment No : "+ unit));
            Rpt1.SetParameters(new ReportParameter("delcrg", "Delay Charge : " + delcrg +" & Advance Discount " + entben));
           
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptEarlyBenADelay()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string project = this.ddlProjectName.SelectedItem.Text.Trim();
            string uacustomer = this.ddlCustName.SelectedItem.Text.Trim();
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)ViewState["tblinterest"];
            List<RealEntity.C_22_Sal.EClassSales_02.EClassInterestDummyPay02> lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassInterestDummyPay02>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEarlybenefitADelay", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptHead", "Delay Charge /Discount Calculation Statement"));
            Rpt1.SetParameters(new ReportParameter("ProjName", "Project Name: " + project));
            Rpt1.SetParameters(new ReportParameter("Unit", uacustomer));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
                    //case"3101":
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
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTINVOICECUSWISE", "180100010011", frmdate, todate, "", "", "", "", "", "");
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
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTINVOICECUSWISE", "180100010011", frmdate, todate, "", "", "", "", "", "");
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

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('LinkDuesColl.aspx?Type=ClientLedger&pactcode=" + mACTCODE + "&usircode=" +
                            uSIRCODE + "&Date1=" + mTRNDAT1 + "', target='_blank');</script>";

        }
        protected void gvRegis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRegis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnupdateb_OnClick(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                string entryben = Convert.ToDouble("0" + this.txtentryben.Text.Trim()).ToString();
                string delaychrg = Convert.ToDouble("0" + this.txtdelaychrg.Text.Trim()).ToString();
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string custid = this.ddlCustName.SelectedValue.ToString();

                DataSet ds1 = new DataSet("ds1");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("code", Type.GetType("System.String"));
                dt1.Columns.Add("charge", Type.GetType("System.String"));

                DataRow dr1;

                dr1 = dt1.NewRow();
                dr1["code"] = "001";
                dr1["charge"] = entryben;
                dt1.Rows.Add(dr1);

                dr1 = dt1.NewRow();
                dr1["code"] = "002";
                dr1["charge"] = delaychrg;
                dt1.Rows.Add(dr1);
                ds1.Tables.Add(dt1);

                ds1.Tables[0].TableName = "tbl1";


                bool result = purData.UpdateXmlTransInfo(comcod, "SP_REPORT_SALSMGT01", "UPDATEBEN", ds1, null, null, pactcode, custid, "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "", "", "", "");

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }


    }
}
