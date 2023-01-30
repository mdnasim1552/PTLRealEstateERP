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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptSaleSoldunsoldUnit : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");





                string type = this.Request.QueryString["Type"].ToString().Trim();
                //this.HeaderText.Text = (type == "soldunsold" ? "Sold and Unsold Informaton " :(type == "parking" ? "Parking Information ":  "Day Wise Sales Information " ));



                //this.txtDate.Text =(this.Request.QueryString["Date1"].Length>0)?this.Request.QueryString["Date1"]: System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.lbltodate.Visible = false;
                this.txttodate.Visible = false;
                this.GetSalesTeam();
                this.gvVisibility();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "soldunsold") ? "SOLD & UNSOLD INFORMATION"
                //    : (this.Request.QueryString["Type"] == "uwiseCosting") ? "Unit Wise Costing"
                //    : (this.Request.QueryString["Type"] == "RptDayWSale") ? "Day Wise Sales"
                //    : (this.Request.QueryString["Type"] == "paking") ? "Parking Status" : "SOLD & UNSOLD INFORMATION";

                this.lbtnOk_Click(null, null);
            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();

            }

            if (this.Request.QueryString["prjcode"].Length > 0)
            {
                this.lbtnOk_Click(null, null);
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void gvVisibility()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "soldunsold":

                    
                    this.SoldType.Visible = true;
                    this.salesteamdv.Visible = false;
                    this.lblSalesTeam.Visible = false;
                    this.ddlSalesTeam.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "parking":
                    this.SoldType.Visible = false;

                    this.Label15.Visible = false;
                    this.txtDate.Visible = false;
                    this.lblSalesTeam.Visible = false;
                    this.lblGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblSalesTeam.Visible = false;
                    this.ddlSalesTeam.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "RptDayWSale":
                    this.SoldType.Visible = false;
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDate.Text = "01" + this.txtDate.Text.Trim().Substring(2);
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lbltodate.Visible = true;
                    this.txttodate.Visible = true;
                    //this.Label15.Text = "From: ";
                    //this.Label15.Visible = false;
                    //this.txtDate.Visible = false;
                    //this.lblGroup.Visible = false;
                    //this.ddlRptGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "uwiseCosting":
                    this.SoldType.Visible = false;
                    this.Label15.Visible = false;
                    this.txtDate.Visible = false;
                    this.lblSalesTeam.Visible = false;
                    this.lblGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblSalesTeam.Visible = false;
                    this.ddlSalesTeam.Visible = false;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

            }
        }

        private string GetComocd()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string qryprjcode = "";
            if (this.Request.QueryString["prjcode"].Length > 0)
            {
                string chkprjcod = this.Request.QueryString["prjcode"];
                if (chkprjcod.Substring(0, 2) == "16")
                {
                    qryprjcode = "18" + chkprjcod.Substring(2, 10);
                }
                else
                {
                    qryprjcode = chkprjcod;
                }
            };
            string txtSProject = "%";// qryprjcode == "" ? ("%" + this.txtSrcPro.Text + "%") : qryprjcode;
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            if (this.Request.QueryString["prjcode"].ToString().Trim().Length > 0)
            {
                this.ddlProjectName.SelectedValue = qryprjcode;
                ddlProjectName.Enabled = false;
            }
        }
        private void GetSalesTeam()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            //string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SALESTEAM", "", "", "", "", "", "", "", "", "");
            this.ddlSalesTeam.DataTextField = "steam";
            this.ddlSalesTeam.DataValueField = "gcod";
            this.ddlSalesTeam.DataSource = ds1.Tables[0];
            this.ddlSalesTeam.DataBind();


        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "soldunsold":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.LoadGrid();
                    break;
                case "parking":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.parkingStatus();
                    break;
                case "RptDayWSale":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.lblSalesTeam.Visible = true;
                    this.ddlSalesTeam.Visible = true;
                    this.salesStatus();
                    break;

                case "uwiseCosting":
                    this.ShowUnitWCosting();
                    break;

            }


        }


        private void LoadGrid()
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string saltype  = (this.rbtnSalType.SelectedIndex == 0) ? "Sold" : (this.rbtnSalType.SelectedIndex == 1) ? "Unsold": "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTSALSUMMERY", PactCode, date, mRptGroup, saltype, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSpayment.DataSource = null;
                this.gvSpayment.DataBind();
                return;
            }


            this.gvSpayment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSpayment.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            Session["tblData"] = HiddenSameData(ds1.Tables[0]);
            this.gvSpayment.DataSource = (DataTable)Session["tblData"];
            this.gvSpayment.DataBind();
            this.FooterCalculation();
        }

        private void parkingStatus()
        {
            Session.Remove("parking");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            //string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            //mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTPARKING", PactCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvparking.DataSource = null;
                this.gvparking.DataBind();
                return;
            }


            this.gvparking.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvparking.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            //Session["tblData"] = HiddenSameData(ds1.Tables[0]);
            //this.gvparking.DataSource = (DataTable)Session["tblData"];
            Session["parking"] = ds1.Tables[0];
            this.gvparking.DataSource = (DataTable)Session["parking"];
            this.gvparking.DataBind();
            this.FooterCalculation();
        }
        private void salesStatus()
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string PactCode = (((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : (this.ddlProjectName.SelectedValue.ToString() == "240000000000") ? "24" : this.ddlProjectName.SelectedValue.ToString())) + "%";
            string frdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            string steam = this.ddlSalesTeam.SelectedValue.Trim().ToString() + "%";
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDAYWISHSAL", PactCode, frdate, todate, mRptGroup, steam, "", "", "", "");
            if (ds1 == null)
            {
                this.gvDayWSale.DataSource = null;
                this.gvDayWSale.DataBind();
                return;
            }


            this.gvDayWSale.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDayWSale.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            Session["tblData"] = HiddenSameData(ds1.Tables[0]);
            this.gvDayWSale.DataSource = (DataTable)Session["tblData"];
            this.gvDayWSale.DataBind();
            this.FooterCalculation();
        }

        private void ShowUnitWCosting()
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTUNISWISECOSTING", PactCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvUnit.DataSource = null;
                this.gvUnit.DataBind();
                return;
            }



            Session["tblData"] = ds1.Tables[0];
            this.gvUnit.DataSource = (DataTable)Session["tblData"];
            this.gvUnit.DataBind();
            this.FooterCalculation();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }

            return dt1;
        }

        private void FooterCalculation()
        {
            string team = this.ddlSalesTeam.SelectedValue.Trim();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblData"];

            if (dt.Rows.Count == 0)
                return;

            switch (type)
            {
                case "soldunsold":

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFTsusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(susize)", "")) ?
                                       0 : dt.Compute("sum(susize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFTusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usize)", "")) ?
                               0 : dt.Compute("sum(usize)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFTqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tqty)", "")) ?
                                       0 : dt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tuamt)", "")) ?
                                    0 : dt.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFSqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sqty)", "")) ?
                                    0 : dt.Compute("sum(sqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(suamt)", "")) ?
                                    0 : dt.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFUsqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usqty)", "")) ?
                                    0 : dt.Compute("sum(usqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFUsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usuamt)", "")) ?
                                    0 : dt.Compute("sum(usuamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disamt)", "")) ?
                                    0 : dt.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFColAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(colamt)", "")) ?
                                   0 : dt.Compute("sum(colamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFRcvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvamt)", "")) ?
                                   0 : dt.Compute("sum(recvamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFParking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(parking)", "")) ?
                                  0 : dt.Compute("sum(parking)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFutility")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(utility)", "")) ?
                                   0 : dt.Compute("sum(utility)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFothers")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cooperative)", "")) ?
                                  0 : dt.Compute("sum(cooperative)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tacsalamt)", "")) ?
                                0 : dt.Compute("sum(tacsalamt)", ""))).ToString("#,##0;(#,##0); ");
                    

                    Session["Report1"] = gvSpayment;
                    ((HyperLink)this.gvSpayment.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;

                case "parking":


                    DataTable dt1 = (DataTable)Session["parking"];


                    ((Label)this.gvparking.FooterRow.FindControl("lgvftoqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tqty)", "")) ?
                                       0 : dt1.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvparking.FooterRow.FindControl("lgvPSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(sold)", "")) ?
                                       0 : dt1.Compute("sum(sold)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvparking.FooterRow.FindControl("lgvPUsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(unsold)", "")) ?
                                    0 : dt1.Compute("sum(unsold)", ""))).ToString("#,##0;(#,##0); ");

                    break;


                case "RptDayWSale":
                    DataView dv = dt.Copy().DefaultView;
                    if (team == "%")
                    {

                        dv.RowFilter = ("pactcode='AAAAAAAAAAAA'");
                    }


                    //dv.RowFilter = ("pactcode='AAAAAAAAAAAA'");
                    DataTable dt2 = dv.ToTable();
                    //DataTable dt = (DataTable)Session["tblData"];

                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tuamt)", "")) ?
                                    0 : dt2.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(suamt)", "")) ?
                                    0 : dt2.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(disamt)", "")) ?
                                    0 : dt2.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "uwiseCosting":

                    ((Label)this.gvUnit.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(uamt)", "")) ?
              0.00 : dt.Compute("Sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvUnit.FooterRow.FindControl("lFUsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usize)", "")) ?
                      0.00 : dt.Compute("Sum(usize)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvUnit.FooterRow.FindControl("lgvPAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pamt)", "")) ?
                      0.00 : dt.Compute("Sum(pamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvUnit.FooterRow.FindControl("lgvTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tamt)", "")) ?
                      0.00 : dt.Compute("Sum(tamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvUnit.FooterRow.FindControl("lgvPUtility")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(utility)", "")) ?
                  0.00 : dt.Compute("Sum(utility)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvUnit.FooterRow.FindControl("lgvPCooprative")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cooperative)", "")) ?
                  0.00 : dt.Compute("Sum(cooperative)", ""))).ToString("#,##0;(#,##0); ");
                    break;




            }


        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "soldunsold":
                    this.rptsold();
                    break;
                case "parking":
                    this.rptparking();
                    break;
                case "RptDayWSale":
                    this.rptDayWSale();
                    break;

                case "uwiseCosting":
                    this.PrintUnitWCosting();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                // string eventtype = this.HeaderText.Text;
                string eventdesc = "Print Report";
                string eventdesc2 = type;
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }







        }

        private void PrintUnitWCosting()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ProName = this.ddlProjectName.SelectedItem.ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblData"];


            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EclassGetUnitWiseCost>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptUnitWiseCost", lst, null, null);

            //if (comcod == "3101" || comcod == "3333")
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_90_PF.RptIndvPfAlli", pflist, null, null);
            //}
            //else
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIndvPf", pflist, null, null);
            //}


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("ProName", "Project Name:" + ProName));

            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Unit Wise Costing"));

            //Rpt1.SetParameters(new ReportParameter("empname", empinfo.Rows[0]["name"].ToString()));
            //Rpt1.SetParameters(new ReportParameter("empid", empinfo.Rows[0]["idcard"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void rptsold()
        {

            //Iqbal  Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblData"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.SoldUnsold>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSaleSoldUsold", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sold & Unslod Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("Level", "Level: " + this.ddlRptGroup.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            ////// Block Iqbal Nayan
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblData"];
            //ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptSaleSoldUsold();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptCode = rptsale.ReportDefinition.ReportObjects["CodeDesc"] as TextObject;
            //rptCode.Text ="Level: "+ this.ddlRptGroup.SelectedItem.Text;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "Date: "+Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void rptparking()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["parking"];
            ReportDocument rptsaleparking = new RealERPRPT.R_22_Sal.rptSaleParking();
            TextObject rptCname = rptsaleparking.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptProjectName = rptsaleparking.ReportDefinition.ReportObjects["projectName"] as TextObject;
            rptProjectName.Text = this.ddlProjectName.SelectedItem.Text.Trim();
            //TextObject rptCode = rptsale.ReportDefinition.ReportObjects["CodeDesc"] as TextObject;
            //rptCode.Text = "Level: " + this.ddlRptGroup.SelectedItem.Text;
            //TextObject rptDate = rptsaleparking.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptsaleparking.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsaleparking.SetDataSource(dt);
            Session["Report1"] = rptsaleparking;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void rptDayWSale()
        {

            //Iqbal  Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblData"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.DaywiseSale>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptDayWiseSales", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From : " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Day Wise Sales"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("Level", "Level: " + this.ddlRptGroup.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblData"];
            //ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptDayWiseSales();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptCode = rptsale.ReportDefinition.ReportObjects["CodeDesc"] as TextObject;
            //rptCode.Text = "Level: " + this.ddlRptGroup.SelectedItem.Text;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From : " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "soldunsold":
                    this.LoadGrid();
                    break;
                case "parking":
                    this.parkingStatus();
                    break;
                case "RptDayWSale":
                    this.salesStatus();
                    break;
            }


        }
        protected void gvSpayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSpayment.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void ddlRptGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvparking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvparking.PageIndex = e.NewPageIndex;
            this.parkingStatus();
        }

        protected void gvDayWSale_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDayWSale.PageIndex = e.NewPageIndex;
            this.salesStatus();
        }

        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HplgvSAmt = (HyperLink)e.Row.FindControl("HplgvSAmt");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComocd();
                string fromDtae = this.txttodate.Text;
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();



                Label actdesc = (Label)e.Row.FindControl("lblgvDPactdesc");
                Label bgdamt = (Label)e.Row.FindControl("lgvDTAmt");
                // HyperLink salamt = (HyperLink)e.Row.FindControl("HplgvAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    bgdamt.Font.Bold = true;
                    //HplgvSAmt.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");
                }

                HplgvSAmt.NavigateUrl = "~/F_22_Sal/LinkDuesColl.aspx?Type=ClientLedger&comcod=" + comcod + "&pactcode=" + pactcode + "&usircode=" + usircode + "&Date1=" + fromDtae;







            }
        }
        protected void gvSpayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HplgvAmt = (HyperLink)e.Row.FindControl("HplgvAmt");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComocd();
                string fromDtae = this.txtDate.Text;
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                HplgvAmt.NavigateUrl = "~/F_22_Sal/LinkDuesColl.aspx?Type=ClientLedger&comcod=" + comcod + "&pactcode=" + pactcode + "&usircode=" + usircode + "&Date1=" + fromDtae;


            }
        }
        protected void gvUnit_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 11;
                gvrow.Cells.Add(cell0);





                TableCell celll = new TableCell();
                celll.Text = "Cost Per SFT";
                celll.HorizontalAlign = HorizontalAlign.Center;
                celll.ColumnSpan = 3;
                gvrow.Cells.Add(celll);



                //this.gvHourlyProd.Columns[5 + i].HeaderText = dt2.Rows[i]["gdesc"].ToString();
                //  i++;


                gvUnit.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
    }



}
