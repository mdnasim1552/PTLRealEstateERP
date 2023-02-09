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
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{

    public partial class RptOPPayment : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = type == "HonourBasis" ? "Operational Payment- Summary" : "Operational Payment - Summary(Post Dated)";
                //this.Master.Page.Title = "Summary of Operational Payment";

                this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GridColumnChange();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GridColumnChange()
        {


            int grpinded = this.rbtnGroup.SelectedIndex;
            string comcod = this.GetCompCode();

            switch (grpinded)
            {
                case 0: // shahin vai say 
                    //this.gvtbOpPay.Columns[2].HeaderText = (comcod == "2305") ? "Up to 5000" : "Up to 10000";
                    //this.gvtbOpPay.Columns[3].HeaderText = (comcod == "2305") ? "5001-50000" : "10001-50000";

                    break;
                case 1:

                    this.ShowOPCollection();
                    break;


            }



        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.ShowData();

        }


        private void ShowData()
        {
            int grpinded = this.rbtnGroup.SelectedIndex;
            this.MultiView1.ActiveViewIndex = grpinded;
            switch (grpinded)
            {
                case 0:

                    this.ShowOPPayment();
                    break;
                case 1:

                    this.ShowOPCollection();
                    break;


            }


        }

        private void ShowOPPayment()
        {
            Session.Remove("tbOpPay");

            string comcod = this.GetCompCode();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string Type = ddlVaule.SelectedValue.ToString();
            //string Type = (this.Request.QueryString["Type"] == "HonourBasis") ? "HonourBasis" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTOPPAYMENT", fdate, tdate, Type, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtbOpPay.DataSource = null;
                this.gvtbOpPay.DataBind();
                return;
            }
            Session["tbOpPay"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "USER LOG DETAILS";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        private void ShowOPCollection()
        {
            Session.Remove("tbOpPay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTOPCOLLECTION", fdate, tdate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtbOpDep.DataSource = null;
                this.gvtbOpDep.DataBind();
                return;
            }
            Session["tbOpPay"] = ds1.Tables[0];
            this.Data_Bind();


        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbOpPay"];
            if (dt.Rows.Count < 0)
                return;
            int grpinded = this.rbtnGroup.SelectedIndex;

            switch (grpinded)
            {
                case 0:
                    //this.gvtbOpPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtbOpPay.DataSource = dt;
                    this.gvtbOpPay.DataBind();
                    Session["Report1"] = gvtbOpPay;
                    ((HyperLink)this.gvtbOpPay.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                case 1:
                    //this.gvtbOpDep.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtbOpDep.DataSource = dt;
                    this.gvtbOpDep.DataBind();
                    Session["Report1"] = gvtbOpDep;
                    ((HyperLink)this.gvtbOpDep.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
            }
        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tbOpPay"];


            DataTable dt1 = dt.Copy();
            if (dt1.Rows.Count == 0)
                return;
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("rpcode ='0000000'");
            DataTable dt2 = dv.ToTable();

            ((Label)this.gvtbOpPay.FooterRow.FindControl("lgvFUp")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(payam1)", "")) ?
                               0 : dt2.Compute("sum(payam1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbOpPay.FooterRow.FindControl("lgvFbtween")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(payam2)", "")) ?
                               0 : dt2.Compute("sum(payam2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbOpPay.FooterRow.FindControl("lgvFAv")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(payam3)", "")) ?
                            0 : dt2.Compute("sum(payam3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbOpPay.FooterRow.FindControl("lgvtFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tpay)", "")) ?
                            0 : dt2.Compute("sum(tpay)", ""))).ToString("#,##0;(#,##0); ");

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
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            int grpinded = this.rbtnGroup.SelectedIndex;

            switch (grpinded)
            {
                case 0:

                    this.PrintOPPayment();
                    break;
                case 1:
                    this.PrintOPCollection();
                    break;


            }

        }


        private void PrintOPPayment()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tbOpPay"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.OppPayment1>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptOpPayment", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtb5t1or10t1", (comcod == "2305") ? "5001-50000" : "10001-50000"));
            Rpt1.SetParameters(new ReportParameter("txtupto5or10T", (comcod == "2305") ? "Up to 5000" : "Up to 10000"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Summary of Operational Payment"));
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
            //string fdate = this.txtfromdate.Text.ToString();
            //string tdate = this.txttodate.Text.ToString();
            //DataTable dt = (DataTable)Session["tbOpPay"];
            //ReportDocument rptLog = new RealERPRPT.R_17_Acc.RptOpPayment();
            //TextObject txtupto5or10T = rptLog.ReportDefinition.ReportObjects["txtupto5or10T"] as TextObject;
            //txtupto5or10T.Text = (comcod == "2305") ? "Up to 5000" : "Up to 10000";
            //TextObject txtb5t1or10t1 = rptLog.ReportDefinition.ReportObjects["txtb5t1or10t1"] as TextObject;
            //txtb5t1or10t1.Text=(comcod == "2305") ? "5001-50000" : "10001-50000";



            //TextObject rptDate = rptLog.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From: " + fdate + " To: " + tdate;
            //TextObject rpCName = rptLog.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rpCName.Text = comnam;
            //TextObject txtuserinfo = rptLog.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptLog.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptLog.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptLog;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        private void PrintOPCollection()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            DataTable dt = (DataTable)Session["tbOpPay"];
            ReportDocument rptLog = new RealERPRPT.R_17_Acc.RptOpCollection();
            TextObject rptDate = rptLog.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From: " + fdate + " To: " + tdate;
            TextObject rpCName = rptLog.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rpCName.Text = comnam;
            TextObject txtuserinfo = rptLog.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptLog.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptLog.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptLog;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }


        protected void gvtbOpPay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvtbOpPay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvtbOpPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcType");
                Label Amount1 = (Label)e.Row.FindControl("lgvUp");
                Label Amount2 = (Label)e.Row.FindControl("lgvbtween");
                Label Amount3 = (Label)e.Row.FindControl("lgvAv");
                Label Amount4 = (Label)e.Row.FindControl("lgvAv4");

                Label Amount = (Label)e.Row.FindControl("lgvtAmt");

                //Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rpcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "0000000")
                {
                    Amount.Font.Bold = true;

                    Amount.Style.Add("text-align", "Left");
                }

                else if(code== "0000AAA" || code == "0000BBB")
                {
                    
                    actdesc.Attributes["style"] = "font-weight:bold; color:maroon;";
                }


                else if (code == "AAAAAAA" || code == "BBBBBBB")
                {
                    actdesc.Font.Bold = true;
                    Amount1.Font.Bold = true;
                    Amount2.Font.Bold = true;
                    Amount3.Font.Bold = true;
                    Amount4.Font.Bold = true;

                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");

                }
                else if (code == "BBBBAAA")
                {
                    actdesc.Font.Bold = true;
                    Amount1.Font.Bold = true;
                    Amount2.Font.Bold = true;
                    Amount3.Font.Bold = true;
                    Amount4.Font.Bold = true;

                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }


                else if (code == "CCCCAAA")
                {
                    actdesc.Font.Bold = true;
                    Amount1.Font.Bold = true;
                    Amount2.Font.Bold = true;
                    Amount3.Font.Bold = true;
                    Amount4.Font.Bold = true;

                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }



            }
        }
        protected void gvtbOpDep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvtbOpDep.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvtbOpDep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lgvdeptdesc = (Label)e.Row.FindControl("lgvdeptdesc");
            Label Amount1 = (Label)e.Row.FindControl("lgvUpcoll");
            Label Amount2 = (Label)e.Row.FindControl("lgvbtweencoll");
            Label Amount3 = (Label)e.Row.FindControl("lgvAvcoll");
            Label Amount = (Label)e.Row.FindControl("lgvtAmtcoll");

            //Label CAmount = (Label)e.Row.FindControl("lgvCre");

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

            if (code == "")
            {
                return;
            }



            if ((ASTUtility.Right(code, 2) == "AA") || (ASTUtility.Right(code, 2) == "59"))
            {
                lgvdeptdesc.Font.Bold = true;
                Amount1.Font.Bold = true;
                Amount2.Font.Bold = true;
                Amount3.Font.Bold = true;
                Amount.Font.Bold = true;
                lgvdeptdesc.Style.Add("text-align", "right");

            }

        }
    }
}

