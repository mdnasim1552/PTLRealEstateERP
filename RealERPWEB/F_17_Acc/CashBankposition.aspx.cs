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

    public partial class CashBankposition : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission (HttpContext.Current.Request.Url.AbsoluteUri.ToString (), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect ("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1 (HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Replace ("%20", " "), (DataSet)Session["tblusrlog"]);


                //((LinkButton)this.Master.FindControl ("lnkPrint")).Enabled = (Convert.ToBoolean (dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = ((type == "casbankpos")
                    ? "Cash & Bank Position (Group Wise)" : (type == "cascredpur") ? "Cash & Credit Purchase"
                    : "LABOUR DETAILS");

                this.Master.Page.Title = ((type == "casbankpos")
                    ? "Cash & Bank Position (Group Wise)" : (type == "cascredpur") ? "Cash & Credit Purchase"
                    : "LABOUR DETAILS");
                if (type == "labour")
                {

                    this.txtfrmdate.Text = this.Request.QueryString["frmdate"];
                    this.txttodate.Text = this.Request.QueryString["todate"];
                }
                else if (type == "cascredpur")
                {
                    this.txtfrmdate.Text = this.Request.QueryString["frmdate"];
                    this.txttodate.Text = this.Request.QueryString["todate"];
                }
                else
                {
                    this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                }

                this.ViewSelection();
                this.GetProjectName();
                this.FromQueryString();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "casbankpos")
            {

                int index = rbtntype.SelectedIndex;
                switch (index)
                {
                    case 0:
                        this.PrintCashBankGrp();
                        break;
                    case 1:
                        this.PrintCashBankGrpMonth();
                        break;
                    case 2:
                        this.PrintCashBankGrpMonthDts();
                        break;

                    default:
                        break;
                }

            }
            if (type == "labour")
            {
                this.PrintLabourDetails();
            }
            if (type == "cascredpur")
            {

                this.PrintCashCrePur();

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

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "PROJECTNAME", "", "", "", "", "", "", "", "", "");

            string type = this.Request.QueryString["Type"];


            if (type == "cascredpur")
            {
                string pactcode = this.Request.QueryString["actcode"].ToString();
                DataTable dt = ds1.Tables[0];
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = ("pactcode ='" + pactcode + "'");
                DataTable dt1 = dv1.ToTable();

                string actcode = dt1.Rows[0]["pactcode"].ToString();
                string actdesc = dt1.Rows[0]["pactdesc"].ToString();
                this.DropCheck1.SelectedValue = actcode + " , " + actdesc;
            }
            else if (type == "labour")
            {
                string pactcode = this.Request.QueryString["actcode"].ToString();
                DataTable dt = ds1.Tables[0];
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = ("pactcode ='" + pactcode + "'");
                DataTable dt1 = dv1.ToTable();

                string actcode = dt1.Rows[0]["pactcode"].ToString();
                string actdesc = dt1.Rows[0]["pactdesc"].ToString();
                this.DropCheck1.SelectedValue = actcode + " , " + actdesc;
            }
            else
            {
                this.DropCheck1.DataTextField = "pactdesc";
                this.DropCheck1.DataValueField = "pactcode";
                this.DropCheck1.DataSource = ds1;
                this.DropCheck1.DataBind();

            }






            ds1.Dispose();
        }

        protected void lnkOk_OnClick(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "casbankpos":

                    this.ShowInformation();
                    this.gvBakCashDt.DataSource = null;
                    this.gvBakCashDt.DataBind();
                    break;
                case "cascredpur":
                    this.ShowCashCredit();
                    break;
                case "labour":
                    this.ShowLabourDetails();
                    break;

            }

        }

        private void FromQueryString()
        {
            string type = this.Request.QueryString["Type"].ToString();


            if (type == "cascredpur")
            {
                this.txtfrmdate.Text = this.Request.QueryString["frmdate"].ToString();
                this.txttodate.Text = this.Request.QueryString["todate"].ToString();
                this.DropCheck1.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }



        }


        private void ViewSelection()
        {
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "casbankpos":
                    this.rbtntype.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "cascredpur":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "labour":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

            }

        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;

            string pactcode = "";
            // string[] gp = this.DropCheck1.Text.Trim().Split(',');

            string gp = this.DropCheck1.SelectedValue.Trim();
            if (gp.Length > 0)
            {
                if (gp.Trim() == "000000000000" || gp.Trim() == "")
                    pactcode = "";
                else
                    foreach (ListItem s1 in DropCheck1.Items)
                    {
                        if (s1.Selected)
                        {
                            pactcode = pactcode + s1.Value.Substring(0, 12);
                        }

                    }


            }
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "CASHBANKPOSITION", frmdate, todate, pactcode, "", "", "",
                "", "", "");
            ViewState["tbldetails"] = HiddenSameData(ds.Tables[0]);
            this.Data_Bind();
        }

        private void ShowCashCredit()
        {
            Session.Remove("tbldetails");
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            string pactcode = this.DropCheck1.SelectedValue.Trim().Substring(0, 12);
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "CASHCREDITPUR", frmdate, todate, pactcode, "", "", "",
                "", "", "");
            ViewState["tbldetails"] = ds.Tables[0];
            this.Data_Bind();
        }



        private void ShowLabourDetails()
        {
            Session.Remove("tbldetails");
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            string pactcode = this.DropCheck1.SelectedValue.Trim().Substring(0, 12);
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "LABOURDETAILS", frmdate, todate, pactcode, "", "", "",
                "", "", "");
            ViewState["tbldetails"] = ds.Tables[0];
            this.Data_Bind();
        }
        private void FooterCalculation()
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "cascredpur")
            {
                DataTable dt = (DataTable)ViewState["tbldetails"];
                DataTable dt1 = dt.Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = "rescode='000000000000'";
                dt1 = dv.ToTable();
                ((Label)this.gvCashCredPur.FooterRow.FindControl("lblamountF")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amount)", "")) ?
                0.00 : dt1.Compute("Sum(amount)", ""))).ToString("#,##0;(#,##0); ");
            }

            if (type == "labour")
            {
                DataTable dt = (DataTable)ViewState["tbldetails"];
                DataTable dt1 = dt.Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = "rescode like '%000'";
                dt1 = dv.ToTable();
                ((Label)this.gvlabour.FooterRow.FindControl("lblamountlabF")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amount)", "")) ?
                0.00 : dt1.Compute("Sum(amount)", ""))).ToString("#,##0;(#,##0); ");
            }


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {

                    dt1.Rows[j]["actdesc"] = "";
                }

                actcode = dt1.Rows[j]["actcode"].ToString();
            }



            return dt1;


        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tbldetails"];
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "casbankpos":

                    this.GvBankCash.DataSource = dt;
                    this.GvBankCash.DataBind();
                    break;
                case "cascredpur":
                    this.gvCashCredPur.DataSource = dt;
                    this.gvCashCredPur.DataBind();
                    this.FooterCalculation();
                    break;
                case "labour":
                    this.gvlabour.DataSource = dt;
                    this.gvlabour.DataBind();
                    this.FooterCalculation();
                    break;
            }

        }


        protected void GvBankCash_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton des = (LinkButton)e.Row.FindControl("lbtnactdesc");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmt");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmt");

                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (actcode == "")
                {
                    return;
                }
                if (actcode.Substring(4, 8) == "00000000")
                {

                    //OpAmt.Font.Bold = true;
                    DrAmt.Font.Bold = true;
                    CrAmt.Font.Bold = true;

                    des.Font.Bold = true;
                    des.Style.Add("color", "black");

                }
                else
                {
                    des.Style.Add("color", "blue");
                }
            }
        }

        protected void lbtnactdesc_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbldetails"];
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            string actcode = dt.Rows[index]["actcode"].ToString();
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "CASHBANKDETAILS", frmdate, todate, actcode, "", "", "",
               "", "", "");

            this.gvBakCashDt.DataSource = ds.Tables[0];
            this.gvBakCashDt.DataBind();
            Session["tblmonthdetails"] = ds.Tables[0];

        }

        protected void gvBakCashDt_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton des = (LinkButton)e.Row.FindControl("lbtnmonthname");
                Label clsam = (Label)e.Row.FindControl("lblgvcls");
                string monthid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "monthid")).ToString();

                if (monthid == "201800" || monthid == "2018AA")
                {
                    des.Font.Bold = true;
                    des.Style.Add("color", "Black");

                    clsam.Font.Bold = true;
                    clsam.Style.Add("color", "Black");

                }
                else
                {
                    des.Style.Add("color", "blue");

                }



            }
        }

        protected void lbtnmonthname_OnClick(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string comcod = this.GetCompCode();
            string monthid = ((Label)this.gvBakCashDt.Rows[index].FindControl("lblmonthid")).Text.Trim();
            string actcode = ((Label)this.gvBakCashDt.Rows[index].FindControl("lgvcactcode")).Text.Trim();



            DateTime date1 = Convert.ToDateTime(monthid.Substring(4, 2) + "/01/" + monthid.Substring(0, 4));

            DateTime date2 = date1.AddMonths(1).AddDays(-1);
            string Narration = "";
            DataSet ds1 = new DataSet();
            string withOutOpn = "withoutOpn";
            string ltype = "Without Cancel";

            ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUM", actcode, date1.ToString("dd-MMM-yyyy"), date2.ToString("dd-MMM-yyyy"), "", Narration, "", ltype, withOutOpn, "");



            DataTable dt = ds1.Tables[0];
            if (dt.Rows.Count == 0)
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                return;
            }
            Session["StoreTable"] = dt;
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);
            this.dgv2.DataSource = ds1.Tables[0];
            this.dgv2.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showmodal();", true);

        }

        private void HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return;
            string grp = dt1.Rows[0]["grp"].ToString();
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                if (dt1.Rows[j]["vounum"].ToString() == vounum)
                {

                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    //dt1.Rows[j]["refnum"] = "";
                }

                if (dt1.Rows[j]["vounum"].ToString().Trim() == "TOTAL")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";

                }
                if (dt1.Rows[j]["vounum"].ToString().Trim() == "BALANCE")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                }

                grp = dt1.Rows[j]["grp"].ToString();
                vounum = dt1.Rows[j]["vounum"].ToString();
            }

            this.dgv2.DataSource = dt1;
            this.dgv2.DataBind();


        }

        private void PrintCashBankGrp()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Actcode = this.ddlProjectInd.SelectedItem.Text.ToString().Substring(13);
            string daterange = "(From  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + " To " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + ")";

            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;



            DataTable dt = (DataTable)ViewState["tbldetails"];
            var rptlist = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.CashBankGrpReport>();

            LocalReport rptcb1 = new LocalReport();

            rptcb1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptCashBankPosGrp", rptlist, null, null);


            rptcb1.SetParameters(new ReportParameter("compname", comnam));
            rptcb1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters(new ReportParameter("ProjName", "Project Name:" + Actcode));
            rptcb1.SetParameters(new ReportParameter("FDate", daterange));



            Session["Report1"] = rptcb1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintCashBankGrpMonth()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Actcode = this.ddlProjectInd.SelectedItem.Text.ToString().Substring(13);
            string daterange = "(From  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + " To " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + ")";

            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;



            DataTable dt = (DataTable)Session["tblmonthdetails"];
            var rptlist = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.CashBankGrpMonthRpt>();

            LocalReport rptcb2 = new LocalReport();

            rptcb2 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptCsahBankGrpMonth", rptlist, null, null);


            rptcb2.SetParameters(new ReportParameter("comnam", comnam));
            rptcb2.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt2.SetParameters(new ReportParameter("ProjName", "Project Name:" + Actcode));
            rptcb2.SetParameters(new ReportParameter("FDate", daterange));



            Session["Report1"] = rptcb2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintCashBankGrpMonthDts()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Actcode = this.ddlProjectInd.SelectedItem.Text.ToString().Substring(13);
            string daterange = "(From  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + " To " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + ")";

            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;



            DataTable dt = (DataTable)Session["StoreTable"];
            var rptlist = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.CashBankGrpMonthDtsRpt>();


            LocalReport rptcb2 = new LocalReport();

            rptcb2 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptCashBankGrpMonthDts", rptlist, null, null);


            rptcb2.SetParameters(new ReportParameter("comnam", comnam));
            rptcb2.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt2.SetParameters(new ReportParameter("ProjName", "Project Name:" + Actcode));
            rptcb2.SetParameters(new ReportParameter("FDate", daterange));



            Session["Report1"] = rptcb2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintCashCrePur()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Actcode = this.ddlProjectInd.SelectedItem.Text.ToString().Substring(13);
            string daterange = "(From  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + " To " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + ")";

            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string total = ((Label)this.gvCashCredPur.FooterRow.FindControl("lblamountF")).Text.Trim();


            DataTable dt = (DataTable)ViewState["tbldetails"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var rptlist = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptCashCrPur>();

            LocalReport rptcb2 = new LocalReport();

            rptcb2 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptCashCrPur", rptlist, null, null);

            rptcb2.EnableExternalImages = true;
            rptcb2.SetParameters(new ReportParameter("comnam", comnam));
            rptcb2.SetParameters(new ReportParameter("printFooter", printFooter));
            rptcb2.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt2.SetParameters(new ReportParameter("ProjName", "Project Name:" + Actcode));
            rptcb2.SetParameters(new ReportParameter("FDate", daterange));
            rptcb2.SetParameters(new ReportParameter("total", total));



            Session["Report1"] = rptcb2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintLabourDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Actcode = this.ddlProjectInd.SelectedItem.Text.ToString().Substring(13);
            string daterange = "(From  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + " To " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyy") + ")";

            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string total = ((Label)this.gvlabour.FooterRow.FindControl("lblamountlabF")).Text.Trim();


            DataTable dt = (DataTable)ViewState["tbldetails"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var rptlist = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.Rptlabour>();

            LocalReport rptcb2 = new LocalReport();

            rptcb2 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptLabourDetails", rptlist, null, null);

            rptcb2.EnableExternalImages = true;
            rptcb2.SetParameters(new ReportParameter("comnam", comnam));
            rptcb2.SetParameters(new ReportParameter("printFooter", printFooter));
            rptcb2.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt2.SetParameters(new ReportParameter("ProjName", "Project Name:" + Actcode));
            rptcb2.SetParameters(new ReportParameter("FDate", daterange));
            rptcb2.SetParameters(new ReportParameter("total", total));



            Session["Report1"] = rptcb2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt, balamt = 0.000000; ;
            //string grp=
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {



                if ((dt.Rows[i]["vounum"]).ToString().Trim() == "TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "BALANCE")
                    continue;
                if ((dt.Rows[i]["grp"]).ToString().Trim() == "C")
                    break;

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

        protected void gvCashCredPur_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink des = (HyperLink)e.Row.FindControl("hydescription");
                Label amount = (Label)e.Row.FindControl("lblamount");
                //HyperLink resbreak = (HyperLink)e.Row.FindControl ("hydescription");
                string pactcode = this.DropCheck1.SelectedValue.Trim().Substring(0, 12);
                string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();
                if (rescode == "000000000000")
                {
                    des.Font.Bold = true;
                    des.Style.Add("color", "blue");
                    amount.Font.Bold = true;
                    amount.Style.Add("color", "blue");
                }
                else if (rescode.Substring(9, 3) == "000")
                {
                    des.Font.Bold = true;
                    des.Style.Add("color", "maroon");
                    amount.Font.Bold = true;
                    amount.Style.Add("color", "maroon");
                }

                if (rescode.Substring(9, 3) != "000")
                {
                    string frmdate = this.txtfrmdate.Text;
                    string todate = this.txttodate.Text;

                    des.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?rpttype=spledgerprj&frmdate=" + frmdate + "&todate=" +
                                          todate + "&pactcode=" + pactcode + "&rescode=" + rescode;
                }



            }
        }

        protected void gvlabour_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label des = (Label)e.Row.FindControl("lbldescriptionlab");
                Label amount = (Label)e.Row.FindControl("lblamountlab");

                string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();
                if (rescode == "000000000000")
                {
                    des.Font.Bold = true;
                    des.Style.Add("color", "blue");
                    amount.Font.Bold = true;
                    amount.Style.Add("color", "blue");
                }
                else if (rescode.Substring(9, 3) == "000")
                {
                    des.Font.Bold = true;
                    des.Style.Add("color", "maroon");
                    amount.Font.Bold = true;
                    amount.Style.Add("color", "maroon");
                }




            }
        }
    }
}