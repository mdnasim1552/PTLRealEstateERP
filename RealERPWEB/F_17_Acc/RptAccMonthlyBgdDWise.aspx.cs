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
using Microsoft.Reporting.WinForms;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccMonthlyBgdDWise : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //  DateTime serverOffset =Convert.ToDateTime(TimeZoneInfo.Local.GetUtcOffset(DateTime.Now));
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Budget (Department Wise)";


                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetDepartment();
                this.GetBudgetedHead();
                this.rbtnList1_SelectedIndexChanged(null, null);

                CommonButton();


            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


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




        private void GetDepartment()
        {

            string comcod = this.GetCompCode();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETDEPARTMENTINF", "", "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            dt.Rows.Add("000000000000", "ALL");
            this.ddlDept.DataTextField = "deptdesc";
            this.ddlDept.DataValueField = "deptcode";
            this.ddlDept.DataSource = dt;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";


        }
        private void GetBudgetedHead()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODEHEAD", "%", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            dt.Rows.Add(comcod, "000000000000", "ALL");
            this.ddlActcode.DataTextField = "actdesc";
            this.ddlActcode.DataValueField = "actcode";
            this.ddlActcode.DataSource = dt;
            this.ddlActcode.DataBind();
            this.ddlActcode.SelectedValue = "000000000000";
            this.ddlActcode_SelectedIndexChanged(null, null);

        }

        private void GetResHead()
        {

            string comcod = this.GetCompCode();
            string actcode = this.ddlActcode.SelectedValue.ToString();
            string filter = "%";
            string srchoption = "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "RETRESCODESECTIONWISE", actcode, filter, srchoption, "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            this.ddlres.DataTextField = "resdesc1";
            this.ddlres.DataValueField = "rescode";
            this.ddlActcode.DataSource = dt;
            this.ddlres.DataBind();
            this.ddlres.SelectedValue = "000000000000";
            //this.ddlActcode_SelectedIndexChanged(null, null);

            //string comcod = this.GetCompCode();
            //List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
            //string actcode = this.ddlActcode.SelectedValue.ToString();


            ////lst = userSer.GetResHead(comcod, actcode, "%", "%");

            //lst = userSer.GetResHead(actcode, filter1, SearchInfo);

            //lst.Add(new RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead("000000000000", "All", "All", ""));
            //this.ddlres.DataSource = lst;
            //this.ddlres.DataTextField = "resdesc1";
            //this.ddlres.DataValueField = "rescode";
            //this.ddlres.DataBind();
            //this.ddlres.SelectedValue = "000000000000";
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {



            if (this.rbtnList1.SelectedIndex == 0)
            {
                this.PrintAccMonthlywrkBgd();
            }
            else
            {
                this.PrintMonthlywrkBgdAcc();
            }


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.GetBudgetInfo();

        }
        private void GetBudgetInfo()
        {
            ViewState.Remove("tblbudgvsact");
            string comcod = this.GetCompCode();
            string yearmon = Convert.ToDateTime(this.txtCurDate.Text).ToString("yyyyMM");
            string deptcode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString() + "%";
            string actcode = (this.ddlActcode.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlActcode.SelectedValue.ToString() + "%";
            string rescode = (this.ddlres.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlres.SelectedValue.ToString() + "%";
            string type = (this.rbtnList1.SelectedIndex == 0) ? "Dept" : "Acc";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_YEARLYBUDGET", "RPT_WORKING_BUDGET", yearmon,
                         actcode, "%", deptcode, type, "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                return;
            }


            ViewState["tblbudgvsact"] = ds1.Tables[0];
            this.dgv2_DataBind();
            this.TotalCalculation1();

        }

        private void PrintAccMonthlywrkBgd()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string date =Convert.ToDateTime( this.txtCurDate.Text).ToString("MMMM/yyyy");




            //DataTable dt1 = (DataTable)ViewState["tblbudgvsact"];

            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //var rptlist = dt1.DataTableToList<MFGOBJ.C_15_Acc.EClassAccounts.AccMonthlyWorkbgd>();
            //var rptlist1 = rptlist.FindAll(p=>p.grp=="B");


            //LocalReport rpt1 = new LocalReport();

            //rpt1 = MFGRPTRDLC.RptSetupClass1.GetLocalReport("RD_15_Acc.RptAccMonthlyWrkBgd", rptlist, rptlist1, null);

            //rpt1.EnableExternalImages = true;
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //rpt1.SetParameters(new ReportParameter("date", "Month : " + date));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("compname", username, printdate)));




            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintMonthlywrkBgdAcc()
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comnam = hst["comnam"].ToString();
            // string comadd = hst["comadd1"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // string comcod = hst["comcod"].ToString();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string date = Convert.ToDateTime(this.txtCurDate.Text).ToString("MMMM/yyyy");


            //// AAAA00000000

            // DataTable dt1 = (DataTable)ViewState["tblbudgvsact"];

            // string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            // var rptlist = dt1.DataTableToList<MFGOBJ.C_15_Acc.EClassAccounts.AccMonthlyWorkbgd>();
            // var rptlist1 = rptlist.FindAll(p => p.grp == "B");


            // LocalReport rpt1 = new LocalReport();

            // rpt1 = MFGRPTRDLC.RptSetupClass1.GetLocalReport("RD_15_Acc.RptMonthlyWrkBgdAcc", rptlist, rptlist1, null);


            // rpt1.EnableExternalImages = true;
            // rpt1.SetParameters(new ReportParameter("comnam", comnam));
            // rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            // rpt1.SetParameters(new ReportParameter("date", "Month : " + date));
            // rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("compname", username, printdate)));




            // Session["Report1"] = rpt1;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            // ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void TotalCalculation1()
        {
            DataTable budget = (DataTable)ViewState["tblbudgvsact"];
            DataView dv = budget.DefaultView;
            dv.RowFilter = "grp='A'";
            DataTable tblt06 = dv.ToTable();
            if (tblt06.Rows.Count == 0)
                return;

            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(bgdamt)", "")) ?
    0.00 : tblt06.Compute("Sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvActual")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(trnam)", "")) ?
           0.00 : tblt06.Compute("Sum(trnam)", ""))).ToString("#,##0.00;(#,##0.00);  ");




            ((TextBox)this.gvsummary.FooterRow.FindControl("StxtTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(bgdamt)", "")) ?
            0.00 : tblt06.Compute("Sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.gvsummary.FooterRow.FindControl("StxtTgvActual")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(trnam)", "")) ?
           0.00 : tblt06.Compute("Sum(trnam)", ""))).ToString("#,##0.00;(#,##0.00);  ");
        }


        protected void dgv2_DataBind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblbudgvsact"];
            DataView dv = tbl1.Copy().DefaultView;
            if (tbl1.Rows.Count == 0)
                return;
            if (this.rbtnList1.SelectedIndex == 0)
            {
                this.dgv2.Columns[2].Visible = true;
                this.dgv2.Columns[4].Visible = false;
                this.gvsummary.Columns[2].Visible = true;
                this.gvsummary.Columns[3].Visible = false;
            }
            else
            {
                this.dgv2.Columns[2].Visible = false;
                this.dgv2.Columns[4].Visible = true;
                this.gvsummary.Columns[2].Visible = false;
                this.gvsummary.Columns[3].Visible = true;
            }
            this.dgv2.DataSource = HiddenSameValue(tbl1);
            this.dgv2.DataBind();

            dv.RowFilter = "grp='B'";
            this.gvsummary.DataSource = dv.ToTable();
            this.gvsummary.DataBind();
        }







        protected void ddlActcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetResHead();
        }

        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rbtnList1.SelectedIndex == 0)
            {
                this.Deptpanel.Visible = true;
                this.Accpanel.Visible = false;
                this.lblachead.Visible = false;
                this.ddlActcode.Visible = false;
                this.lblDept.Visible = true;
                this.ddlDept.Visible = true;
            }
            else
            {
                this.Deptpanel.Visible = false;
                this.Accpanel.Visible = true;
                this.lblachead.Visible = true;
                this.ddlActcode.Visible = true;
                this.lblDept.Visible = false;
                this.ddlDept.Visible = false;
            }
        }

        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string resdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "resdesc")).ToString();
                Label lblAccdesc = (Label)e.Row.FindControl("lblAccdesc");
                if (resdesc == "Total")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    lblAccdesc.Attributes["style"] = "font-weight:bold;";

                }
                else if (resdesc == "Grand Total")
                {
                    e.Row.BackColor = System.Drawing.Color.LightSeaGreen;
                    lblAccdesc.Attributes["style"] = "font-weight:bold;";
                }

            }
        }
        private DataTable HiddenSameValue(DataTable tbl1)
        {
            //DataTable tbl1 = (DataTable)ViewState["tblbudgvsact"];
            string actcode = "";
            string deptcode = "";
            string gp = "";
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                if (gp == tbl1.Rows[i]["gp"].ToString())
                {
                    tbl1.Rows[i]["gpdesc"] = "";
                }
                else
                {
                    gp = tbl1.Rows[i]["gp"].ToString();
                }
                if (actcode == tbl1.Rows[i]["actcode"].ToString())
                {
                    tbl1.Rows[i]["actdesc"] = "";
                }
                else
                {
                    actcode = tbl1.Rows[i]["actcode"].ToString();
                }
                if (deptcode == tbl1.Rows[i]["deptcode"].ToString())
                {
                    tbl1.Rows[i]["deptdesc"] = "";
                }
                else
                {
                    deptcode = tbl1.Rows[i]["deptcode"].ToString();
                }

            }
            return tbl1;
        }
    }
}