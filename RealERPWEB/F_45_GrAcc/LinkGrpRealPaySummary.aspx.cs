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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkGrpRealPaySummary : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = this.Request.QueryString["Type"].ToString().Trim();


                this.lblfrmdate.Text = this.Request.QueryString["Date1"].ToString();
                this.lbltodate.Text = this.Request.QueryString["Date2"].ToString();
                this.SelectView();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "CollVsClearance") ? "Cheque Received Vs. Clearance" : (type == "DailyPayment") ? "Daily Payment Summary Report"
                    : (type == "DetRealColl") ? "Real Collection - Details "
                    : (type == "MonCollection") ? "Month Wise Collection(Received)"
                    : (type == "MonCollHonoured") ? "Month Wise Collection(Honoured)"
                    : (type == "MonPayment") ? "Month Wise Payment - All Project"
                    : (type == "MonSales") ? "Sales - All Project"
                    : (type == "MonReceipt") ? "Month Wise Receipt"
                    : (type == "MonPaymentDet") ? "Month Wise Payment(Cost)"
                    : "Month Wise Payment-Summary";





            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private string GetCompCode()
        {

            return (this.Request.QueryString["comcod"].ToString());


        }


        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "CollVsClearance":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "DailyPayment":
                    this.rbtPayment.SelectedIndex = 0;
                    break;
                case "DetRealColl":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "MonCollection":
                case "MonCollHonoured":
                case "MonSales":

                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentDet":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;


                case "MonPaymentSumm":
                    this.MultiView1.ActiveViewIndex = 7;
                    break;












            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "CollVsClearance":
                    this.ShowCollVsClearacne();
                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.MultiView1.ActiveViewIndex = 2;
                        this.ShowDailyPaymentDet();
                        ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Payment Details Report";
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.MultiView1.ActiveViewIndex = 4;
                        this.ShowPaymentDet();
                    }
                    else
                    {
                        this.MultiView1.ActiveViewIndex = 1;
                        this.ShowDailyPayment();
                        this.rbtPayment.Visible = true;
                    }
                    break;

                case "DetRealColl":
                    this.ShowCollDetails();
                    break;
                case "MonCollection":
                case "MonCollHonoured":
                    this.ShowMonCollection();
                    break;

                case "MonPayment":
                    this.ShowMonPayorReceipt();
                    break;

                case "MonSales":
                    this.ShowMonSales();
                    break;

                case "MonReceipt":
                    this.ShowMonPayorReceipt();
                    break;

                case "MonPaymentDet":
                    this.ShowMonPayDetCostWise();
                    break;


                case "MonPaymentSumm":

                    this.ShowMonPaySummary();
                    break;




            }
        }



        private void ShowCollVsClearacne()
        {
            //ViewState.Remove("tblcollvscl");
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTRECEIVEDVSCOLLEC", frmdate, todate, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvCollVsCleared.DataSource = null;
            //    this.gvCollVsCleared.DataBind();
            //    return;
            //}
            //ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();
            //ds1.Dispose();

        }
        private void ShowDailyPayment()
        {
            //ViewState.Remove("tblcollvscl");
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ////string CallType = (this.chbDetails.Checked) ? "RPTDAILYPAYMENTDETAILS" : "RPTDAILYPAYMENT";
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTDAILYPAYMENT", frmdate, todate, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvDPayment.DataSource = null;
            //    this.gvDPayment.DataBind();
            //    return;
            //}
            //ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();
            //ds1.Dispose();

        }
        private void ShowDailyPaymentDet()
        {
            //ViewState.Remove("tblcollvscl");
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTDAILYPAYMENTDETAILS", frmdate, todate, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvPayDetails.DataSource = null;
            //    this.gvPayDetails.DataBind();
            //    return;
            //}
            //ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();
            //ds1.Dispose();

        }
        private void ShowPaymentDet()
        {
            //ViewState.Remove("tblcollvscl");
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTPAYMENTDETAILS", frmdate, todate, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvPayDet.DataSource = null;
            //    this.gvPayDet.DataBind();
            //    return;
            //}
            //ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();
            //ds1.Dispose();

        }

        private void ShowCollDetails()
        {
            //ViewState.Remove("tblcollvscl");
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "RPTREALCOLLDETAILS", frmdate, todate, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvCollDet.DataSource = null;
            //    this.gvCollDet.DataBind();
            //    return;
            //}
            //ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();
            //ds1.Dispose();


        }


        private void ShowMonCollection()
        {
            //ViewState.Remove("tblcollvscl");
            //string comcod = this.GetCompCode();

            //int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()),Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            //if (mon>12)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
            //    return;


            //}



            //string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string realColl = (this.Request.QueryString["Type"] == "MonCollHonoured") ? "realization" : "";
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "RPTMONWISECOLLECT", txtdatefrm, txtdateto, realColl, "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvMonCollect.DataSource = null;
            //    this.gvMonCollect.DataBind();
            //    return;
            //}


            //ViewState["tblcollvscl"] = ds1.Tables[0];
            //this.Data_Bind();
        }

        private void ShowMonPayorReceipt()
        {

            //ViewState.Remove("tblcollvscl");
            //string comcod = this.GetCompCode();

            //int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            //if (mon > 12)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
            //    return;


            //}

            //string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            //string Receipt = (this.Request.QueryString["Type"].ToString() == "MonReceipt") ? "receipt" : "";
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "MONWPAYORRECEIPT", txtdatefrm, txtdateto, Receipt, "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvMonPayment.DataSource = null;
            //    this.gvMonPayment.DataBind();
            //    return;
            //}


            //ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();



        }

        private void ShowMonPayDetCostWise()
        {

            //ViewState.Remove("tblcollvscl");
            //string comcod = this.GetCompCode();

            //int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            //if (mon > 12)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
            //    return;


            //}

            //string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "MONWPAYDETAILS", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvMonPayment.DataSource = null;
            //    this.gvMonPayment.DataBind();
            //    return;
            //}


            //ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();


        }
        private void ShowMonPaySummary()
        {

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()), Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = this.Request.QueryString["Date1"].ToString();
            string txtdateto = this.Request.QueryString["Date2"].ToString();

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "MONWPAYSUMMARY", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMonPaymentSumm.DataSource = null;
                this.gvMonPaymentSumm.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }

        private void ShowMonSales()
        {
            //ViewState.Remove("tblcollvscl");
            //string comcod = this.GetCompCode();

            //int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            //if (mon > 12)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
            //    return;


            //}

            //string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "RPTMONWISESALES", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvMonCollect.DataSource = null;
            //    this.gvMonCollect.DataBind();
            //    return;
            //}


            //ViewState["tblcollvscl"] = ds1.Tables[0];
            //this.Data_Bind();



        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "CollVsClearance":
                    string rarcndate1 = dt1.Rows[0]["rarcndate1"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rarcndate1"].ToString() == rarcndate1)
                            dt1.Rows[j]["rarcndate"] = "";


                        rarcndate1 = dt1.Rows[j]["rarcndate1"].ToString();

                    }
                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        string actcode1 = dt1.Rows[0]["actcode1"].ToString();
                        for (int j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["actcode1"].ToString() == actcode1)
                            {
                                actcode1 = dt1.Rows[j]["actcode1"].ToString();
                                dt1.Rows[j]["actdesc1"] = "";
                            }
                            else
                            {
                                actcode1 = dt1.Rows[j]["actcode1"].ToString();
                            }
                        }
                    }
                    break;


                case "DetRealColl":
                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentSumm":
                case "MonPaymentDet":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        else
                            grp = dt1.Rows[j]["grp"].ToString();
                    }

                    break;





            }

            return dt1;

        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            DateTime datefrm, dateto;
            DataView dv; DataTable dt;
            switch (type)
            {


                case "CollVsClearance":
                    this.gvCollVsCleared.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCollVsCleared.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvCollVsCleared.DataBind();
                    this.FooterCalculation();
                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.gvPayDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPayDetails.DataSource = (DataTable)ViewState["tblcollvscl"];
                        this.gvPayDetails.DataBind();
                        this.FooterCalculation();
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.gvPayDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPayDet.DataSource = (DataTable)ViewState["tblcollvscl"];
                        this.gvPayDet.DataBind();
                        this.FooterCalculation();
                    }
                    else
                    {
                        this.gvDPayment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvDPayment.DataSource = (DataTable)ViewState["tblcollvscl"];
                        this.gvDPayment.DataBind();
                        this.FooterCalculation();
                    }
                    break;


                case "DetRealColl":
                    this.gvCollDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCollDet.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvCollDet.DataBind();
                    this.FooterCalculation();
                    break;

                case "MonCollection":
                case "MonCollHonoured":
                case "MonSales":

                    //dv=((DataTable)ViewState["tblcollvscl"]).Copy().DefaultView;
                    //dv.RowFilter = ("pactcode  like '%99BBBBAAAAAA%'");
                    //dt = dv.ToTable();
                    //amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    //amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    //amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    //amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    //amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    //amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    //amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    //amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    //amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    //amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    //amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    //amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    //this.gvMonCollect.Columns[3].Visible = (amt1 != 0);
                    //this.gvMonCollect.Columns[4].Visible = (amt2 != 0);
                    //this.gvMonCollect.Columns[5].Visible = (amt3 != 0);
                    //this.gvMonCollect.Columns[6].Visible = (amt4 != 0);
                    //this.gvMonCollect.Columns[7].Visible = (amt5 != 0);
                    //this.gvMonCollect.Columns[8].Visible = (amt6 != 0);
                    //this.gvMonCollect.Columns[9].Visible = (amt7 != 0);
                    //this.gvMonCollect.Columns[10].Visible = (amt8 != 0);
                    //this.gvMonCollect.Columns[11].Visible = (amt9 != 0);
                    //this.gvMonCollect.Columns[12].Visible = (amt10 != 0);
                    //this.gvMonCollect.Columns[13].Visible = (amt11 != 0);
                    //this.gvMonCollect.Columns[14].Visible = (amt12 != 0);

                    //datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    //dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    //for (int i = 3; i < 15; i++)
                    //{
                    //    if (datefrm > dateto)
                    //        break;

                    //    this.gvMonCollect.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                    //    datefrm = datefrm.AddMonths(1);

                    //}

                    //this.gvMonCollect.DataSource = (DataTable)ViewState["tblcollvscl"];
                    //this.gvMonCollect.DataBind();
                    //this.FooterCalculation();

                    break;



                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentDet":
                    //dv=((DataTable)ViewState["tblcollvscl"]).Copy().DefaultView;
                    //dv.RowFilter = ("pactcode  like '%99BBBBAAAAAA%'");
                    //dt = dv.ToTable();
                    //amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    //amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    //amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    //amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    //amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    //amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    //amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    //amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    //amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    //amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    //amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    //amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    //this.gvMonPayment.Columns[5].Visible = (amt1 != 0);
                    //this.gvMonPayment.Columns[6].Visible = (amt2 != 0);
                    //this.gvMonPayment.Columns[7].Visible = (amt3 != 0);
                    //this.gvMonPayment.Columns[8].Visible = (amt4 != 0);
                    //this.gvMonPayment.Columns[9].Visible = (amt5 != 0);
                    //this.gvMonPayment.Columns[10].Visible = (amt6 != 0);
                    //this.gvMonPayment.Columns[11].Visible = (amt7 != 0);
                    //this.gvMonPayment.Columns[12].Visible = (amt8 != 0);
                    //this.gvMonPayment.Columns[13].Visible = (amt9 != 0);
                    //this.gvMonPayment.Columns[14].Visible = (amt10 != 0);
                    //this.gvMonPayment.Columns[15].Visible = (amt11 != 0);
                    //this.gvMonPayment.Columns[16].Visible = (amt12 != 0);



                    // datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    // dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    //for (int i = 5; i < 17; i++)
                    //{
                    //    if (datefrm > dateto)
                    //        break;

                    //    this.gvMonPayment.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                    //    datefrm = datefrm.AddMonths(1);

                    //}

                    //this.gvMonPayment.DataSource = (DataTable)ViewState["tblcollvscl"];
                    //this.gvMonPayment.DataBind();
                    //this.FooterCalculation();
                    break;






                case "MonPaymentSumm":
                    dv = ((DataTable)ViewState["tblcollvscl"]).Copy().DefaultView;
                    dv.RowFilter = ("pactcode  like '%99BBBBAAAAAA%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    this.gvMonPaymentSumm.Columns[3].Visible = (amt1 != 0);
                    this.gvMonPaymentSumm.Columns[4].Visible = (amt2 != 0);
                    this.gvMonPaymentSumm.Columns[5].Visible = (amt3 != 0);
                    this.gvMonPaymentSumm.Columns[6].Visible = (amt4 != 0);
                    this.gvMonPaymentSumm.Columns[7].Visible = (amt5 != 0);
                    this.gvMonPaymentSumm.Columns[8].Visible = (amt6 != 0);
                    this.gvMonPaymentSumm.Columns[9].Visible = (amt7 != 0);
                    this.gvMonPaymentSumm.Columns[10].Visible = (amt8 != 0);
                    this.gvMonPaymentSumm.Columns[11].Visible = (amt9 != 0);
                    this.gvMonPaymentSumm.Columns[12].Visible = (amt10 != 0);
                    this.gvMonPaymentSumm.Columns[13].Visible = (amt11 != 0);
                    this.gvMonPaymentSumm.Columns[14].Visible = (amt12 != 0);



                    datefrm = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString());
                    dateto = Convert.ToDateTime(this.Request.QueryString["Date2"].ToString());
                    for (int i = 3; i < 15; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvMonPaymentSumm.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvMonPaymentSumm.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvMonPaymentSumm.DataBind();
                    this.FooterCalculation();
                    break;



            }

        }


        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt4;
            DataView dv1;
            switch (type)
            {


                case "CollVsClearance":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("chqno like 'Grand Total'");
                    dt4 = dv1.ToTable();
                    double curamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(clcuram)", "")) ? 0 : dt4.Compute("sum(clcuram)", "")));
                    double preamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(clpream)", "")) ? 0 : dt4.Compute("sum(clpream)", "")));

                    ((Label)this.gvCollVsCleared.FooterRow.FindControl("lgvFCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(recam)", "")) ?
                               0 : dt4.Compute("sum(recam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvCollVsCleared.FooterRow.FindControl("lgvFCuamt")).Text = curamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollVsCleared.FooterRow.FindControl("lgvFPreamt")).Text = preamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollVsCleared.FooterRow.FindControl("lgvNetTotal")).Text = (curamt + preamt).ToString("#,##0;(#,##0); ");
                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        DataTable ddtc = dt1.Copy();
                        DataView dvc = ddtc.DefaultView;
                        dvc.RowFilter = ("grp='B'");
                        ddtc = dvc.ToTable();
                        ((Label)this.gvPayDetails.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ddtc.Compute("sum(dram)", "")) ?
                               0 : ddtc.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        DataTable ddt = dt1.Copy();
                        DataView dv = ddt.DefaultView;
                        dv.RowFilter = ("grp='B'");
                        ddt = dv.ToTable();
                        ((Label)this.gvPayDet.FooterRow.FindControl("lgvFPayAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(payam)", "")) ?
                              0 : ddt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    else
                    {
                        ((Label)this.gvDPayment.FooterRow.FindControl("lgvFPayAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                                  0 : dt1.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    break;


                case "DetRealColl":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("grp='G' and usircode='CCCCAAAAAAAA' ");
                    dt4 = dv1.ToTable();
                    double cashamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(cashamt)", "")) ? 0 : dt4.Compute("sum(cashamt)", "")));
                    double chqamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(chqamt)", "")) ? 0 : dt4.Compute("sum(chqamt)", "")));


                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvFCashamt")).Text = cashamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvFChqamt")).Text = chqamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvCDNetTotal")).Text = (cashamt + chqamt).ToString("#,##0;(#,##0); ");
                    break;


                case "MonCollection":
                case "MonCollHonoured":
                case "MonSales":

                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("pactcode='99BBBBAAAAAA'");
                    dt4 = dv1.ToTable();

                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentDet":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("pactcode='99BBBBAAAAAA'");
                    dt4 = dv1.ToTable();
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFnetTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(netamt)", "")) ? 0.00 : dt4.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFopening")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(opnamt)", "")) ? 0.00 : dt4.Compute("sum(opnamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFtoamtmpay")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    break;



                case "MonPaymentSumm":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("pactcode='99BBBBAAAAAA'");
                    dt4 = dv1.ToTable();

                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFtoamtmpaysum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    break;


            }



        }



        protected void gvCollVsCleared_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCollVsCleared.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvCollVsCleared_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label date = (Label)e.Row.FindControl("lgvDate");
                Label collChqno = (Label)e.Row.FindControl("lgcollChqno");
                Label CollAmt = (Label)e.Row.FindControl("lgvCollAmt");
                Label ClChqno = (Label)e.Row.FindControl("lgvClChqno");
                Label cuamt = (Label)e.Row.FindControl("lgvclcuram");
                Label pramt = (Label)e.Row.FindControl("lgvClPramt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "clcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    //date.Font.Bold = true;
                    collChqno.Font.Bold = true;
                    CollAmt.Font.Bold = true;
                    ClChqno.Font.Bold = true;
                    cuamt.Font.Bold = true;
                    pramt.Font.Bold = true;
                    collChqno.Style.Add("text-align", "right");
                    ClChqno.Style.Add("text-align", "right");


                }

            }
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {


                case "CollVsClearance":
                    this.PrintCollVsClearacne();

                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.PrintDailyDetails();
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.PrintPaymentDetails();
                    }
                    else
                    {
                        this.PrintDailyPayment();
                    }
                    break;
                case "DetRealColl":
                    this.PrintRealCollDet();
                    break;

                case "MonCollection":
                case "MonCollHonoured":
                    this.PrintMonCollection();
                    break;

                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentSumm":
                case "MonPaymentDet":
                    this.PrintMonRecorPayment();
                    break;

                case "MonSales":
                    this.PrintMonSales();
                    break;

            }


        }


        private void PrintMonCollection()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptMonWiseCollection();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["Type"]=="MonCollHonoured") ? "Month Wise Collection (Honoured)" : "Month Wise Collection(Received)";

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() +" )";


            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintMonRecorPayment()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new ReportDocument();
            //if (this.Request.QueryString["Type"].ToString() == "MonPaymentDet")
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptMonWisePaymentDet();
            //}
            //else
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptMonWisePayment();
            //}
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["Type"].ToString() == "MonReceipt") ? "Month Wise Receipt" 
            //    : (this.Request.QueryString["Type"].ToString() == "MonPaymentSumm") ? "Month Wise Payment-Summary"
            //    : (this.Request.QueryString["Type"].ToString() == "MonPaymentDet") ? "Month Wise Payment(Cost Wise)" : "Month Wise Payment(Project Wise)";

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintMonSales()
        {

            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comnam = hst["comnam"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            //    if (dt1.Rows.Count == 0)
            //        return;
            //    ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptMonWiseCollection();
            //    TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //    txtCompany.Text = comnam;

            //    TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //    txtHeader.Text = "Month Wise Sales";

            //    TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //    txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            //    DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //    DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //    for (int i = 1; i <= 12; i++)
            //    {
            //        if (datefrm > dateto)
            //            break;
            //        TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //        rpttxth.Text = datefrm.ToString("MMM yy");
            //        datefrm = datefrm.AddMonths(1);

            //    }

            //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptstk.SetDataSource(dt1);
            //    Session["Report1"] = rptstk;
            //    this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintCollVsClearacne()
        {
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //    ReportDocument rptstate = new RealERPRPT.R_17_Acc.rptChqReceivedVsClr();
            //    TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //    rptCname.Text = comnam;


            //    TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //    rptftdate.Text = "(From " + fromdate + " To " + todate+")";
            //    TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //    rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);

            //    Session["Report1"] = rptstate;
            //    this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintDailyPayment()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptDailyPaymentSumm();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptftdate.Text = "(From " + fromdate + " To " + todate + ")";
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);

            //Session["Report1"] = rptstate;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintDailyDetails()
        {
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //    ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptDailyPaymentCostDet();
            //    TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //    rptCname.Text = comnam;


            //    TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //    rptftdate.Text = "(From " + fromdate + " To " + todate + ")";

            //    TextObject rpttotal = rptstate.ReportDefinition.ReportObjects["txttotal"] as TextObject;
            //    rpttotal.Text = ((Label)this.gvPayDetails.FooterRow.FindControl("lgvFTDrAmt")).Text;


            //    TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //    rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);

            //    Session["Report1"] = rptstate;
            //    this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintPaymentDetails()
        {
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //    ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptPaymentDetails();
            //    TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //    rptCname.Text = comnam;

            //    TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //    rptftdate.Text = "(From " + fromdate + " To " + todate + ")";
            //    TextObject rpAmt = rptstate.ReportDefinition.ReportObjects["txtPayAmt"] as TextObject;
            //    rpAmt.Text = ((Label)this.gvPayDet.FooterRow.FindControl("lgvFPayAmt")).Text;
            //    TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //    rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);

            //    Session["Report1"] = rptstate;
            //    this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        private void PrintRealCollDet()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptRealCollDetails();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);
            //Session["Report1"] = rptstate;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvDPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDPayment.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvPayDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Payamt = (Label)e.Row.FindControl("lgvAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "A")
                {
                    Payamt.Font.Bold = true;
                    Payamt.Style.Add("text-align", "left");


                }
            }
        }
        protected void gvCollDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCollDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void gvCollDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lgvudesc");
                Label cashamt = (Label)e.Row.FindControl("lgvcashamt");
                Label chqamt = (Label)e.Row.FindControl("lgvchqamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    udesc.Font.Bold = true;
                    cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                    udesc.Style.Add("text-align", "right");


                }

            }

        }
        protected void gvPayDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPayDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvPayDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Rescode = (Label)e.Row.FindControl("lgvResCod");
                Label Resdesc = (Label)e.Row.FindControl("lgvResName");
                Label Payamt = (Label)e.Row.FindControl("lgvPayAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "A")
                {

                    Rescode.Font.Bold = true;
                    Resdesc.Font.Bold = true;
                    Payamt.Font.Bold = true;
                    Payamt.Style.Add("text-align", "left");


                }
                if (code == "B")
                {
                    Rescode.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvMonCollect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    HyperLink HygvResDesc = (HyperLink)e.Row.FindControl("HygvResDesc");
            //    Label lgvtoamt = (Label)e.Row.FindControl("lgvtoamt");
            //    Label lgvamt1 = (Label)e.Row.FindControl("lgvamt1");
            //    Label lgvamt2 = (Label)e.Row.FindControl("lgvamt2");
            //    Label lgvamt3 = (Label)e.Row.FindControl("lgvamt3");
            //    Label lgvamt4 = (Label)e.Row.FindControl("lgvamt4");
            //    Label lgvamt5 = (Label)e.Row.FindControl("lgvamt5");
            //    Label lgvamt6 = (Label)e.Row.FindControl("lgvamt6");
            //    Label lgvamt7 = (Label)e.Row.FindControl("lgvamt7");
            //    Label lgvamt8 = (Label)e.Row.FindControl("lgvamt8");
            //    Label lgvamt9 = (Label)e.Row.FindControl("lgvamt9");
            //    Label lgvamt10 = (Label)e.Row.FindControl("lgvamt10");
            //    Label lgvamt11 = (Label)e.Row.FindControl("lgvamt11");
            //    Label lgvamt12 = (Label)e.Row.FindControl("lgvamt12");


            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (ASTUtility.Right(code, 4) == "AAAA") 
            //    {

            //        HygvResDesc.Font.Bold = true;
            //        lgvtoamt.Font.Bold = true;
            //        lgvamt1.Font.Bold = true;
            //        lgvamt2.Font.Bold = true;
            //        lgvamt3.Font.Bold = true;
            //        lgvamt4.Font.Bold = true;
            //        lgvamt5.Font.Bold = true;
            //        lgvamt6.Font.Bold = true;
            //        lgvamt7.Font.Bold = true;
            //        lgvamt8.Font.Bold = true;
            //        lgvamt9.Font.Bold = true;
            //        lgvamt10.Font.Bold = true;
            //        lgvamt11.Font.Bold = true;
            //        lgvamt12.Font.Bold = true;
            //        HygvResDesc.Style.Add("text-align", "right");
            //    }
            //    if (Request.QueryString["Type"] == "MonSales")
            //    {
            //        if (ASTUtility.Left(code, 2) == "18" || ASTUtility.Left(code, 2) == "24")
            //        {
            //            HygvResDesc.NavigateUrl = "LinkAccount.aspx?Type=SalesProj&pactcode=" + code + "&Date1=" + this.txtfromdate.Text.Trim() + "&Date2=" + this.txttodate.Text.Trim();
            //        }
            //    }
            //}
        }
        protected void gvMonPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvActdescmpay = (Label)e.Row.FindControl("lgvActdescmpay");
                Label lgvtoamtmpay = (Label)e.Row.FindControl("lgvtoamtmpay");
                Label lgvamtmpay1 = (Label)e.Row.FindControl("lgvamtmpay1");
                Label lgvamtmpay2 = (Label)e.Row.FindControl("lgvamtmpay2");
                Label lgvamtmpay3 = (Label)e.Row.FindControl("lgvamtmpay3");
                Label lgvamtmpay4 = (Label)e.Row.FindControl("lgvamtmpay4");
                Label lgvamtmpay5 = (Label)e.Row.FindControl("lgvamtmpay5");
                Label lgvamtmpay6 = (Label)e.Row.FindControl("lgvamtmpay6");
                Label lgvamtmpay7 = (Label)e.Row.FindControl("lgvamtmpay7");
                Label lgvamtmpay8 = (Label)e.Row.FindControl("lgvamtmpay8");
                Label lgvamtmpay9 = (Label)e.Row.FindControl("lgvamtmpay9");
                Label lgvamtmpay10 = (Label)e.Row.FindControl("lgvamtmpay10");
                Label lgvamtmpay11 = (Label)e.Row.FindControl("lgvamtmpay11");
                Label lgvamtmpay12 = (Label)e.Row.FindControl("lgvamtmpay12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvActdescmpay.Font.Bold = true;
                    lgvtoamtmpay.Font.Bold = true;
                    lgvamtmpay1.Font.Bold = true;
                    lgvamtmpay2.Font.Bold = true;
                    lgvamtmpay3.Font.Bold = true;
                    lgvamtmpay4.Font.Bold = true;
                    lgvamtmpay5.Font.Bold = true;
                    lgvamtmpay6.Font.Bold = true;
                    lgvamtmpay7.Font.Bold = true;
                    lgvamtmpay8.Font.Bold = true;
                    lgvamtmpay9.Font.Bold = true;
                    lgvamtmpay10.Font.Bold = true;
                    lgvamtmpay11.Font.Bold = true;
                    lgvamtmpay12.Font.Bold = true;
                    lgvActdescmpay.Style.Add("text-align", "right");
                }
                if (this.Request.QueryString["Type"] == "MonPaymentDet" || this.Request.QueryString["Type"] == "MonReceipt")
                {
                    if (ASTUtility.Right(code, 8) == "00000000")
                    {

                        lgvActdescmpay.Font.Bold = true;
                        lgvtoamtmpay.Font.Bold = true;
                        lgvamtmpay1.Font.Bold = true;
                        lgvamtmpay2.Font.Bold = true;
                        lgvamtmpay3.Font.Bold = true;
                        lgvamtmpay4.Font.Bold = true;
                        lgvamtmpay5.Font.Bold = true;
                        lgvamtmpay6.Font.Bold = true;
                        lgvamtmpay7.Font.Bold = true;
                        lgvamtmpay8.Font.Bold = true;
                        lgvamtmpay9.Font.Bold = true;
                        lgvamtmpay10.Font.Bold = true;
                        lgvamtmpay11.Font.Bold = true;
                        lgvamtmpay12.Font.Bold = true;
                        //lgvActdescmpay.Style.Add("text-align", "right");
                    }
                }

            }

        }
        protected void gvMonPaymentSumm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HLgvDescpaysum = (HyperLink)e.Row.FindControl("HLgvDescpaysum");
                Label lgvtoamtmpaysum = (Label)e.Row.FindControl("lgvtoamtmpaysum");
                Label lgvamtmpaysum1 = (Label)e.Row.FindControl("lgvamtmpaysum1");
                Label lgvamtmpaysum2 = (Label)e.Row.FindControl("lgvamtmpaysum2");
                Label lgvamtmpaysum3 = (Label)e.Row.FindControl("lgvamtmpaysum3");
                Label lgvamtmpaysum4 = (Label)e.Row.FindControl("lgvamtmpaysum4");
                Label lgvamtmpaysum5 = (Label)e.Row.FindControl("lgvamtmpaysum5");
                Label lgvamtmpaysum6 = (Label)e.Row.FindControl("lgvamtmpaysum6");
                Label lgvamtmpaysum7 = (Label)e.Row.FindControl("lgvamtmpaysum7");
                Label lgvamtmpaysum8 = (Label)e.Row.FindControl("lgvamtmpaysum8");
                Label lgvamtmpaysum9 = (Label)e.Row.FindControl("lgvamtmpaysum9");
                Label lgvamtmpaysum10 = (Label)e.Row.FindControl("lgvamtmpaysum10");
                Label lgvamtmpaysum11 = (Label)e.Row.FindControl("lgvamtmpaysum11");
                Label lgvamtmpaysum12 = (Label)e.Row.FindControl("lgvamtmpaysum12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDescpaysum.Font.Bold = true;
                    lgvtoamtmpaysum.Font.Bold = true;
                    lgvamtmpaysum1.Font.Bold = true;
                    lgvamtmpaysum2.Font.Bold = true;
                    lgvamtmpaysum3.Font.Bold = true;
                    lgvamtmpaysum4.Font.Bold = true;
                    lgvamtmpaysum5.Font.Bold = true;
                    lgvamtmpaysum6.Font.Bold = true;
                    lgvamtmpaysum7.Font.Bold = true;
                    lgvamtmpaysum8.Font.Bold = true;
                    lgvamtmpaysum9.Font.Bold = true;
                    lgvamtmpaysum10.Font.Bold = true;
                    lgvamtmpaysum11.Font.Bold = true;
                    lgvamtmpaysum12.Font.Bold = true;
                    HLgvDescpaysum.Style.Add("text-align", "right");
                }

                if (grp == "4" && ASTUtility.Right(code, 10) == "0000000000")
                {

                    string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();

                    //   HLgvDescpaysum.NavigateUrl = "~/F_32_Mis/LinkMis.aspx?Type=ResCostDet&rescode=" + code + "&resdesc=" + pactdesc + "&frmdate=" + this.txtfromdate.Text.Trim() + "&todate=" + this.txttodate.Text.Trim();

                }




            }
        }
    }
}
