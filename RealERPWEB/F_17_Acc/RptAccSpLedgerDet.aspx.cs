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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccSpLedgerDet : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double dramt, cramt, opnamt, clsamt;
        public double balamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowDetailLedger();

            this.LblSchReportPeriod.Text = "(From " + Convert.ToDateTime(Request.QueryString["Date1"].ToString().Trim()).ToString("dd-MMM-yyyy") +
                " to " + Convert.ToDateTime(Request.QueryString["Date2"].ToString().Trim()).ToString("dd-MMM-yyyy") + ")";
            this.LblAcchead.Text = Request.QueryString["head"].ToString().Trim();
            ((Label)this.Master.FindControl("lblTitle")).Text = "";
            this.Master.Page.Title = "";
            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            this.PrintDetailLedger();

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void PrintDetailLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblspledger"];
            if (dt == null)
                return;
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>();
            LocalReport Rpt1 = new LocalReport();

            switch (comcod)
            {
                case "3305":// RHEL
                case "3311":// RHEL(ctg)
                case "3306":// Ratul
                case "3309":// HOlding
                case "2305"://RLDL
                            //rptsl = new RealERPRPT.R_17_Acc.RPTSpecialLedgerRup();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedgerRup", lst, null, null);
                    break;

                default:

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedger", lst, null, null);


                    break;


            }



            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("prjname", "Acc.Desc : " + dt.Rows[0]["resdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(Request.QueryString["Date1"].ToString().Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(Request.QueryString["Date2"].ToString().Trim()).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptaital", "SPECIAL LEDGER REPORT"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptsl = new ReportDocument(); 

            //switch (comcod)
            //{
            //    case "3305":// RHEL
            //    case "3311":// RHEL(ctg)
            //    case "3306":// Ratul
            //    case "3309":// HOlding
            //    case "2305"://RLDL
            //    rptsl = new RealERPRPT.R_17_Acc.RPTSpecialLedgerRup() ;
            //   break;

            //    default:

            //    rptsl = new RealERPRPT.R_17_Acc.RPTSpecialLedger() ;


            //    break;


            //}




            //DataTable dt = (DataTable)Session["tblspledger"];


            //TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtcomadd = rptsl.ReportDefinition.ReportObjects["txtcomadd"] as TextObject;
            //txtcomadd.Text = comadd;
            //TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "(From " + Convert.ToDateTime(Request.QueryString["Date1"].ToString().Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(Request.QueryString["Date2"].ToString().Trim()).ToString("dd-MMM-yyyy") + ")";
            //TextObject rpttxtAccDesc = rptsl.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            //rpttxtAccDesc.Text = "Account Description: " + dt.Rows[0]["resdesc"].ToString();

            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource((DataTable)Session["tblspledger"]);
            //Session["Report1"] = rptsl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void ShowDetailLedger()
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Request.QueryString["comcod"].ToString().Trim();
            string frmdate = Request.QueryString["Date1"].ToString().Trim();
            string todate = Request.QueryString["Date2"].ToString().Trim();
            string resource = Request.QueryString["actcode"].ToString().Trim();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvSpledger.DataSource = null;
                this.gvSpledger.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblspledger"] = dt;
            this.gvSpledger.DataSource = dt;
            this.gvSpledger.DataBind();
            //this.FooterCal();


        }




        //private void SaveValue()
        //{
        //    DataTable dt = (DataTable)Session["tblspledger"];

        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFOpnbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnbill)", "")) ?
        //                  0 : dt.Compute("sum(opnbill)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFOpnAdv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnadv)", "")) ?
        //          0 : dt.Compute("sum(opnadv)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFCrAmtas")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
        //           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFDrAmtas")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
        //          0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");

        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFclsbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsbill)", "")) ?
        //           0 : dt.Compute("sum(clsbill)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFclsadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsadv)", "")) ?
        //          0 : dt.Compute("sum(clsadv)", ""))).ToString("#,##0;(#,##0); ");



        //}


        private DataTable HiddenSameData(DataTable dt1)
        {

            string vounum = dt1.Rows[0]["vounum1"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }
                if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum1"].ToString() == vounum))
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["vounum1"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {

                        dt1.Rows[j]["actdesc"] = "";
                    }

                    if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                    {

                        dt1.Rows[j]["vounum1"] = "";

                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();
                }

            }
            return dt1;

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblspledger"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "head1='03CT'";
            dt = dv.ToTable();
            //string type = Request.QueryString["Type"].ToString().Trim();
            //switch (type)
            //{

            // case "DetailLedger":
            ((Label)this.gvSpledger.FooterRow.FindControl("lgvFOpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
                    0 : dt.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSpledger.FooterRow.FindControl("lgvFDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                  0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSpledger.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                   0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSpledger.FooterRow.FindControl("lgvFClsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                  0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");
            //    break;

            //case "SPayment":
            //    dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?0 : dt.Compute("sum(dram)", "")));
            //    ((Label)this.gvSPayment.FooterRow.FindControl("lgvFDrAmts")).Text = dramt.ToString("#,##0;(#,##0); ");
            //   cramt=Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?0 : dt.Compute("sum(cram)", "")))  ;
            //   ((Label)this.gvSPayment.FooterRow.FindControl("lgvFCrAmts")).Text = cramt.ToString("#,##0;(#,##0); ");
            //   balamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ? 0 : dt.Compute("sum(balamt)", "")));
            //    ((Label)this.gvSPayment.FooterRow.FindControl("lgvFBalAmts")).Text = balamt.ToString("#,##0;(#,##0); ");

            //   break;

            //}

        }
        protected void gvSpledger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvVounum1");
                Label OpAmt = (Label)e.Row.FindControl("lblgvOpAmount");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmount");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmount");
                Label ClAmt = (Label)e.Row.FindControl("lblgvClAmount");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "head1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "AB")
                {
                    hlink.Font.Bold = true;
                    OpAmt.Font.Bold = true;
                    DrAmt.Font.Bold = true;
                    CrAmt.Font.Bold = true;
                    ClAmt.Font.Bold = true;
                    hlink.Style.Add("text-align", "right");
                }
            }

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
                if (mVOUNUM.Substring(0, 2) == "PV" || mVOUNUM.Substring(0, 2) == "PV")
                    hlink1.NavigateUrl = "RptAccVouher02.aspx?vounum=" + mVOUNUM;
                else
                    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + mVOUNUM;

                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            }
        }


        //protected void gvAllSupPay_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    if (e.Row.RowType != DataControlRowType.DataRow)
        //        return;

        //    HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvSupDesc");
        //    string mCOMCOD = comcod;
        //    string sircod = ((Label)e.Row.FindControl("lblSupCode")).Text;
        //    string mTRNDAT1 = this.txtDateFrom.Text;
        //    string mTRNDAT2 = this.txtDateto.Text;

        //    //if (ASTUtility.Right(mACTCODE, 4) == "0000")
        //    //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
        //    //         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        //    //else
        //    hlink1.NavigateUrl = "RptAccSpLedgerDet.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + sircod +
        //             "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        //}

    }
}
