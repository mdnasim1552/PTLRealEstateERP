using System;
using System.Collections;
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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_81_Hrm.F_90_PF
{

    public partial class AccLedger : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common compUtility = new Common();
        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                if (Request.QueryString["Type"].ToString() == "SubLedger")
                {

                    //((Label)this.Master.FindControl("lblTitle")).Text = "accounts subsidiary Ledger";
                    this.Panel1.Visible = true;
                }
                this.rbtnList1.SelectedIndex = 0;
                this.GetDate();
            }
        }
        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtDateFrom.Text = startdate + date.Substring(2);
            this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string filter = this.txtAccSearch.Text.Trim() + "%";
                DataSet ds1 = new DataSet();
                if (Request.QueryString["Type"].ToString() == "SubLedger")
                {

                    ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEADWITHRES", filter, "", "", "", "", "", "", "", "");
                }

                else
                {
                    ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD01", filter, "", "", "", "", "", "", "", "");

                }
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
                //this.GetPriviousVoucher();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string comcod = hst["comcod"].ToString();
            string filter = this.txtSrchRes.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCRESHEAD", actcode, filter, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //DataTable dt1 = ds1.Tables[0];
            this.ddlConAccResHead.DataSource = ds1.Tables[0];
            this.ddlConAccResHead.DataTextField = "resdesc1";
            this.ddlConAccResHead.DataValueField = "rescode";
            this.ddlConAccResHead.DataBind();

            Session["StoreTable2"] = ds1.Tables[0];

        }


        protected void lnkShowLedger_Click(object sender, EventArgs e)
        {
            Session.Remove("StoreTable");
            DataSet ds2 = this.GetDataForReport();
            DataTable dt = ds2.Tables[0];
            Session["StoreTable"] = dt;
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);
            //  (Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")).Trim().Length==14 ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") : "")
        }

        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {
                if (((dt.Rows[i]["cactcode"]).ToString().Trim()).Length == 12)
                {
                    dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                    cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                    balamt = balamt + (dramt - cramt);
                    dt.Rows[i]["balamt"] = balamt;
                }
            }
            return dt;


        }

        private void HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return;
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                {
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    dt1.Rows[j]["refnum"] = "";
                }

                else
                {
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                }

                if (dt1.Rows[j]["vounum1"].ToString().Trim() == "TOTAL")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";

                }
                if (dt1.Rows[j]["vounum1"].ToString().Trim() == "BALANCE")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                }
            }

            this.dgv2.DataSource = dt1;
            this.dgv2.DataBind();
            this.dgv2.Columns[4].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") && (this.chkqty.Checked);
            this.dgv2.Columns[5].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") && (this.chkqty.Checked);

        }
        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string date1 = this.txtDateFrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string Narration = (this.rbtnList1.SelectedIndex == 0) ? "" : "WithoutNar";
            DataSet ds1 = new DataSet();

            if (Request.QueryString["Type"].ToString() == "SubLedger")
            {
                string rescode = this.ddlConAccResHead.SelectedValue.ToString();
                ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", actcode, date1, date2, rescode, Narration, "", "", "", "");

            }

            else
            {
                ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGER", actcode, date1, date2, "", Narration, "", "", "", "");
            }

            return ds1;
        }


        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14)
            {
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {




            if (this.chkqty.Checked == true && Request.QueryString["Type"].ToString() == "SubLedger")
            {
                this.PrintLedgerWithQty();
            }
            else
            {
                this.PrintLedger();

            }


        }
        private void PrintLedgerWithQty()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["StoreTable"];
            ReportDocument rptstk = new ReportDocument();
            string Headertitle = "Subsidary Ledger";
            string Resdesc = this.ddlConAccResHead.SelectedItem.Text.Substring(13);
            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccSLedger", lst, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("resdes", Resdesc));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Resdesc = "";
            if (Request.QueryString["Type"].ToString() == "SubLedger")
            {
                Resdesc = this.ddlConAccResHead.SelectedItem.Text;

            }
            DataTable dt = (DataTable)Session["StoreTable"];
            if (dt == null)
                return;
            DataTable dt2 = (DataTable)Session["StoreTable2"];
            if (dt == null)
                return;


            string Headertitle = (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "19") ? "Cash/Bank Book"
                : (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "29") ? "Cash/Bank Book"
                : (Request.QueryString["Type"].ToString() == "SubLedger") ? "Subsidary Ledger" : "Ledger";
            string daterange = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            if (Request.QueryString["Type"].ToString() == "SubLedger")
            {
                Resdesc = this.ddlConAccResHead.SelectedItem.Text.Substring(13);

            }

            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedger", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("resdes", (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc + "(" + dt2.Rows[0]["desig"].ToString() + ")"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

    }
}
