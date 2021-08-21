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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
//using RealERPRPT;

namespace RealERPWEB.F_17_Acc
{
    public partial class LinkAccLedger : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (Request.QueryString["Type"].ToString() == "SubLedger")
                {


                    this.Panel1.Visible = true;
                }
                this.rbtnList1.SelectedIndex = 0;


                this.txtDateFrom.Text = this.Request.QueryString["Date1"].ToString();
                this.txtDateto.Text = this.Request.QueryString["Date2"].ToString();
                this.IbtnSearchAcc_Click(null, null);
            }

        }
        private string GetCompcode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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

                    ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEADWITHRES", filter, "", "", "", "", "", "", "", "");
                }

                else
                {
                    string actcode = this.Request.QueryString["actcode"].ToString() + "%";

                    ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD02", actcode, "", "", "", "", "", "", "", "");

                }
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string comcod = hst["comcod"].ToString();
            string filter = this.txtSrchRes.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCRESHEAD", actcode, filter, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //DataTable dt1 = ds1.Tables[0];
            this.ddlConAccResHead.DataSource = ds1.Tables[0];
            this.ddlConAccResHead.DataTextField = "resdesc1";
            this.ddlConAccResHead.DataValueField = "rescode";
            this.ddlConAccResHead.DataBind();

        }


        protected void lnkShowLedger_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("StoreTable");
            DataSet ds2 = this.GetDataForReport();
            DataTable dt = ds2.Tables[0];
            if (dt.Rows.Count == 0)
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                return;
            }
            Session["StoreTable"] = dt;
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);
            //  (Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")).Trim().Length==14 ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") : "")
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Ledger";
                string eventdesc = "Show Ledger";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;
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

        private void HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return;
            string grp = dt1.Rows[0]["grp"].ToString();
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                {

                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    //dt1.Rows[j]["refnum"] = "";
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

                grp = dt1.Rows[j]["grp"].ToString();
                vounum = dt1.Rows[j]["vounum1"].ToString();
            }

            this.dgv2.DataSource = dt1;
            this.dgv2.DataBind();
            //this.dgv2.Columns[0].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") || (this.chkqty.Checked);
            this.dgv2.Columns[6].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") && (this.chkqty.Checked);
            this.dgv2.Columns[7].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") && (this.chkqty.Checked);

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
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", actcode, date1, date2, rescode, Narration, "", "", "", "");

            }

            else
            {
                //string calltype = (Request.QueryString["RType"].ToString() == "GLedger") ? "ACCOUNTSLEDGERWC" : "ACCOUNTSLEDGER";
                string ltype = (Request.QueryString["RType"].ToString() == "GLedger") ? "Without Cancel" : "";
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGER", actcode, date1, date2, "", Narration, "", ltype, "", "");
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

            string voucher = hlink1.Text;


            if (voucher.Trim().Length == 14)
            {
                hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
                hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            if (this.chkqty.Checked == true && Request.QueryString["Type"].ToString() == "SubLedger")
            {
                this.PrintLedgerWithQty();
            }
            else
            {
                this.PrintLedger();

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Ledger";
                string eventdesc = "Print Ledger";
                string eventdesc2 = "From: " + this.txtDateFrom.Text + " To: " + this.txtDateto.Text;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
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
            string daterange = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            string Resdesc = this.ddlConAccResHead.SelectedItem.Text.Substring(13);
            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            string comledger = this.ComLedger();
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }






        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ibtnFindRes_Click(null, null);
        }



        private string ComLedger()
        {

            string comcod = this.GetCompcode();
            string comledger = "";
            switch (comcod)
            {




                case "3337"://Suvastu
                case "3339"://Tropical
                case "3336"://Suvastu
                case "1103"://Tanvir Constructions Ltd.          
                    comledger = "LedgerSuTroaTanvir";
                    break;

                //case "3101":
                //case "3333":
                //    comledger = "LedgerAlli";
                //    break;


                case "3330":
                    comledger = "LedgerBridge";
                    break;

                case "3344":
                    //case "3336":
                    comledger = "LedgerTerranova";
                    break;


                case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    comledger = "LedgerRupayan";
                    break;

                default:
                    comledger = "LedgerGen";
                    break;




            }

            return comledger;

        }
        private void PrintLedger()
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

            string Headertitle = (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "19") ? "Cash/Bank Book"
               : (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "29") ? "Cash/Bank Book"
               : (Request.QueryString["Type"].ToString() == "SubLedger") ? "Subsidary Ledger" : "Ledger";
            string daterange = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            string Resdesc = "";
            if (Request.QueryString["Type"].ToString() == "SubLedger")
            {
                Resdesc = this.ddlConAccResHead.SelectedItem.Text.Substring(13);

            }

            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            string comledger = this.ComLedger();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            LocalReport Rpt1 = new LocalReport();

            if (comledger == "LedgerSuTroaTanvir")
            {

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptLedger", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }

            else if (comledger == "LedgerBridge")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerBridge", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }

            //else if (comledger == "LedgerAlli")
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptAccLedgerAlli();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            //}
            else if (comledger == "LedgerTerranova")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerTerra", lst, null, null);
                Rpt1.EnableExternalImages = true;

            }

            else if (comledger == "LedgerRupayan")
            {

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerRup", lst, null, null);
                Rpt1.EnableExternalImages = true;

            }


            else
            {
                string checkby = (comcod == "3340") ? "Checked By" : "Recommended By";
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedger", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtcheckedby", checkby));




            }
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("resdes", Resdesc));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccLedger() ;
            //string Resdesc = "";
            //if (Request.QueryString["Type"].ToString() == "SubLedger")
            //{
            //    Resdesc = this.ddlConAccResHead.SelectedItem.Text.Substring(13);

            //}
            //DataTable dt = (DataTable)Session["StoreTable"];
            //if (dt == null)
            //    return;
            //string Headertitle = (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "19") ? "Cash/Bank Book" 
            //    : (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "29") ? "Cash/Bank Book" 
            //    :(Request.QueryString["Type"].ToString() == "SubLedger")?"Subsidary Ledger" : "Ledger";


            //TextObject txtHeadertitle = rptstk.ReportDefinition.ReportObjects["txtHeadertitle"] as TextObject;
            //txtHeadertitle.Text = Headertitle;
            //TextObject txtcompanyname = rptstk.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //txtcompanyname.Text = comnam; 

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy")+" )";

            //TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            //rpttxtAccDesc.Text ="Accounts Head: "+ this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            //TextObject txtResDesc = rptstk.ReportDefinition.ReportObjects["txtResDesc"] as TextObject;
            //txtResDesc.Text = (Resdesc.Length==0)?"":"Details Head: "+Resdesc;




            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["StoreTable"]);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


    }
}
