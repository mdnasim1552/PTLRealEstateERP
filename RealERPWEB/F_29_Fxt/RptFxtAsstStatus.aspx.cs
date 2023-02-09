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
namespace RealERPWEB.F_29_Fxt
{
    public partial class RptFxtAsstStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "";

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Fix") ? "Fixed Assets Status" : "Fixed Assets Schedule";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.rbtnList1.SelectedIndex = 0;
                this.gvVisibility();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();


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
                case "Fix":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "DepCost":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.txtFromdate.Text = Convert.ToDateTime(DateTime.Now.AddMonths(-1)).ToString("dd-MMM-yyyy");
                    this.txtTodate.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
                    break;

            }
        }


        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;


        }

        private void GetProjectName()
        {
            string comcod = this.GetComcod();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GetProjectName", txtSProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.TransferInfo();
        }

        private void TransferInfo()
        {
            Session.Remove("tbltransfer");
            string comcod = this.GetComcod();
            string ProjectName = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string hzero = (this.ChkBalance.Checked == true) ? "hzero" : "";
            string calltype = (this.rbtnList1.SelectedIndex == 0) ? "RPTBALRESOURCE" : (this.rbtnList1.SelectedIndex == 1) ? "RPTBALRESOURCEWDETAILS" : "RPTFXTASTVALUE";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", calltype, ProjectName, date, hzero, mRptGroup, "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            this.grvacc.Columns[1].Visible = (this.ddlProjectName.SelectedValue == "000000000000") ? true : false;
            this.grvacc.Columns[4].Visible = (calltype == "RPTBALRESOURCE");
            this.grvacc.Columns[6].Visible = (calltype == "RPTBALRESOURCE");
            this.grvacc.Columns[9].Visible = (calltype == "RPTFXTASTVALUE");
            this.grvacc.Columns[10].Visible = (calltype == "RPTFXTASTVALUE");
            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tbltransfer"] = dt;
            this.grvacc.DataSource = dt; ;
            this.grvacc.DataBind();
            this.FooterCalculation(dt);
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                }

                else
                {



                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";
                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                    {
                        dt1.Rows[j]["rsirdesc"] = "";

                    }
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();

                }

            }
            return dt1;
            //string rsircode = dt1.Rows[0]["rsircode"].ToString();

            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
            //    {
            //        rsircode = dt1.Rows[j]["rsircode"].ToString();
            //        dt1.Rows[j]["rsirdesc"] = "";

            //    }

            //    else
            //    {
            //        rsircode = dt1.Rows[j]["rsircode"].ToString();
            //    }


            //}

            //return dt1;

        }

        private void FooterCalculation(DataTable dt)
        {
            if (this.rbtnList1.SelectedIndex == 2)
            {

                ((Label)this.grvacc.FooterRow.FindControl("lblFoterAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Fix":
                    if (this.rbtnList1.SelectedIndex == 0)
                    {
                        this.rptFxtAsstStatusDetails();
                        break;
                    }
                    if (this.rbtnList1.SelectedIndex == 1)
                    {
                        this.rptFxtAsstStatusWiD();
                        break;
                    }
                    else
                    {
                        this.PrintFxtAstValue();
                    }
                    break;

                case "DepCost":
                    this.printDepreciation();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = type;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        private void rptFxtAsstStatusDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbltransfer"];

            var list = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.FixedAssetsStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.rptFxtAsstStatusDetails", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Assets Status"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void rptFxtAsstStatusWiD()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbltransfer"];

            var list = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.FixedAssetsStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.rptFxtAsstStatusWD", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Assets Status"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintFxtAstValue()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbltransfer"];

            var list = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.FixedAssetsStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.rptFxtAsstValue", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Assets Status"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void printDepreciation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblDepcost"];


            string date = "Period: " + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "  To  " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            int dateDife = ASTUtility.Datediffday(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text));
            int dateDife1 = dateDife + 1;
            string rpttxtDays = "Days : " + dateDife1.ToString();
            string txtBalance = "Balance as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            string txtTotal = "Balane as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            string txtDepr = "Depreciatoin as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            string txtWD = "W.D Values as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);


            var lst = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EClassDepricationCost>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptDepricationCharge", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Assets Schedule"));
            Rpt1.SetParameters(new ReportParameter("rpttxtDays", rpttxtDays));
            Rpt1.SetParameters(new ReportParameter("txtBalance", txtBalance));
            Rpt1.SetParameters(new ReportParameter("txtTotal", txtTotal));
            Rpt1.SetParameters(new ReportParameter("txtDepr", txtDepr));
            Rpt1.SetParameters(new ReportParameter("txtWD", txtWD));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void grDep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grDep.PageIndex = e.NewPageIndex;
            this.grDep_DataBind();
        }

        private void grDep_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblDepcost"];
            this.grDep.Columns[2].HeaderText = "Balance as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            this.grDep.Columns[6].HeaderText = "Balance as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            this.grDep.Columns[8].HeaderText = "Depreciation as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            this.grDep.Columns[12].HeaderText = "W.D Values as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            this.grDep.DataSource = tbl1;
            this.grDep.DataBind();
            Session["Report1"] = grDep;



            this.FooterRowCal();

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblDepcost");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frdate = Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTDEPRSCHEDULE", frdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grDep.DataSource = null;
                this.grDep.DataBind();
                return;
            }


            //int dateDife = TimeSpan(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text)); ASTUtility.Datediffday(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text));


            this.txtDays.Text = "Days: " + Convert.ToDouble(ds1.Tables[1].Rows[0]["cday"]).ToString("#,##0;(#,##0);");
            Session["tblDepcost"] = (DataTable)ds1.Tables[0];
            this.grDep_DataBind();

        }
        private void FooterRowCal()
        {
            DataTable dt = (DataTable)Session["tblDepcost"];
            if (dt.Rows.Count == 0)
                return;


            ((HyperLink)this.grDep.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            ((Label)this.grDep.FooterRow.FindControl("lgvFTOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                  0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTAddition")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curam)", "")) ?
                                   0 : dt.Compute("sum(curam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFsalesdec")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saleam)", "")) ?
                                 0 : dt.Compute("sum(saleam)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.grDep.FooterRow.FindControl("lgvFTDisposal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disam)", "")) ?
                                  0 : dt.Compute("sum(disam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFTTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ?
                                   0 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepOpen")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opndep)", "")) ?
                                   0 : dt.Compute("sum(opndep)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFadjment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adjam)", "")) ?
                                   0 : dt.Compute("sum(adjam)", ""))).ToString("#,##0;(#,##0); ");



            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepCur")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curdep)", "")) ?
                                   0 : dt.Compute("sum(curdep)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(todep)", "")) ?
                                   0 : dt.Compute("sum(todep)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTCBal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                                   0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");










        }

    }
}











