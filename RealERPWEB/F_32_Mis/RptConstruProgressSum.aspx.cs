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
namespace RealERPWEB.F_32_Mis
{
    public partial class RptConstruProgressSum : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00, bgdpercent = 0.00, bgdexepercent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"])) ;

                this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
               // DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled =dr1.Length==0?false: (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Floor Wise Construction Progress";
                this.lbtnOk_Click(null, null);


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }




        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }







        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.ShowValue();
        }

        private void ShowValue()
        {
            this.lbljavascript.Text = "";
            this.ShowConProgress();
        }



        private void ShowConProgress()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string toDate = this.txtCurDate.Text;
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTCONPROGRAMSUM", toDate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvConPro.DataSource = null;
                this.gvConPro.DataBind();
                return;
            }


            Session["tblConPro"] = ds1.Tables[0];



            this.LoadGrid();


        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblConPro"];



            //string prjcode = this.Request.QueryString["prjcode"];
            //if (prjcode.Length > 0)
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.RowFilter = "pactcode=prjcode";
            //    Session["tblConPro"] = dv;
            //}






            this.gvConPro.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvConPro.DataSource = dt;
            this.gvConPro.DataBind();
            this.FooterCalcul(dt);
        }

        private void FooterCalcul(DataTable dt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            if (dt.Rows.Count == 0)
                return;


            double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
            double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));

            percent = (mplan == 0 ? 0.00 : ((examt * 100) / mplan));
            bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
            bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFBgdAmt")).Text = bgdamt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFMasPlan")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplan)", "")) ?
                                0 : dt.Compute("sum(mplan)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFexAmt")).Text = examt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFMPlanastoday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ?
                                0 : dt.Compute("sum(mplanat)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvConPro.FooterRow.FindControl("lgvFexAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ?
                                0 : dt.Compute("sum(eamt)", ""))).ToString("#,##0;(#,##0); ");

            ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFlessexAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(leamt)", "")) ?
                         0 : dt.Compute("sum(leamt)", ""))).ToString("#,##0;(#,##0); ");




            // //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //// string pactdesc = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13) ;
            // string frmdate = Convert.ToDateTime("01" + this.txtCurDate.Text.Substring(2)).ToString("dd-MMM-yyyy");
            // string todate = Convert.ToDateTime( this.txtCurDate.Text).ToString("dd-MMM-yyyy");


            // ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFlessexAmt")).NavigateUrl = "~/F_32_Mis/LinkMis.aspx?Type=ImpPlan02&comcod="+comcod+"&Pactcode=" + pactcode + "&Date1=" + frmdate + "&Date2=" + todate;


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblConPro"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            if (dt.Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.CatWiseConProgressAllPro>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptConProgramSum", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "As On " + this.txtCurDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            //Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Construction Progress -All Project"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBgsdVsExe()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetCompCode ( );
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblConPro"];
            //double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            //double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
            //double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));

            //percent = (mplan == 0 ? 0.00 : ((examt * 100) / mplan));
            //bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
            //bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));

            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //ReportDocument rptConPro = new RealERPRPT.R_32_Mis.RptConProgram();
            //TextObject rpttxtPrjName = rptConPro.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text = projectName;
            //TextObject rpttxtDate = rptConPro.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text ="As On "+this.txtCurDate.Text.Trim();
            //TextObject rpttxtpercent = rptConPro.ReportDefinition.ReportObjects["txtpercent"] as TextObject;
            //rpttxtpercent.Text = percent.ToString("#,##0.00;(#,##0.00); ") + " %";
            //TextObject rpttxBtpercent = rptConPro.ReportDefinition.ReportObjects["txtbpercent"] as TextObject;
            //rpttxBtpercent.Text = bgdpercent.ToString("#,##0.00;(#,##0.00); ") + " %";

            //TextObject txtbexepercent = rptConPro.ReportDefinition.ReportObjects["txtbexepercent"] as TextObject;
            //txtbexepercent.Text = bgdexepercent.ToString("#,##0.00;(#,##0.00); ") + " %";
            //TextObject txtuserinfo = rptConPro.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptConPro.SetDataSource(dt);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Floor Wise Construction Progress";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptConPro.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptConPro;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }


        protected void gvConPro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvConPro.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvConPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            Label lgvprcentonlp = (Label)e.Row.FindControl("lgvprcentonlp");


            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            HyperLink hlnkBudgetAmt = (HyperLink)e.Row.FindControl("hlnkgvBudgetAmt");
            HyperLink hlnkExAmt = (HyperLink)e.Row.FindControl("hlnklgvExAmt");

            string mPACTCODE = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
            string mpactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
            double pernolp = Convert.ToDouble(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "peronlp")));
            if (pernolp > 0)
            {
                lgvprcentonlp.Attributes["style"] = "color:red;";
            }



            //  string comcod = this.GetCompCode();
            string mTRNDAT1 = this.txtCurDate.Text;

            string frmdate = "01-" + mTRNDAT1.Substring(3);
            hlink1.Style.Add("color", "blue");
            hlnkBudgetAmt.Style.Add("color", "blue");
            hlnkExAmt.Style.Add("color", "blue");
            hlink1.NavigateUrl = "~/F_32_Mis/LinkConstruProgress.aspx?&pactcode=" + mPACTCODE + "&pactdesc=" + mpactdesc + "&date=" + mTRNDAT1;
            hlnkBudgetAmt.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdGrWiseDet&comcod=" + comcod + "&prjcode=" + mPACTCODE;
            hlnkExAmt.NavigateUrl = "~/F_32_Mis/LinkImpExeStatus.aspx?Type=DayWiseExecution&pactcode=" + mPACTCODE + "&Date1=" + frmdate + "&Date2=" + mTRNDAT1;

        }
    }
}
