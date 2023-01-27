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
using RealERPRDLC;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class BankInterest : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (
                    !ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                        (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Bank Interest";
                this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ExpenseType();
                this.BankName();
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void ExpenseType()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "FINANCIALEXPENSES", "", "", "", "", "",
                "", "", "", "");
            this.ddlexpenseType.DataTextField = "actdesc";
            this.ddlexpenseType.DataValueField = "actcode";
            this.ddlexpenseType.DataSource = ds1.Tables[0];
            this.ddlexpenseType.DataBind();
        }

        private void BankName()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "BANKLIST", "", "", "", "", "", "", "",
                "", "");
            this.ddlbanklist.DataTextField = "actdesc";
            this.ddlbanklist.DataValueField = "actcode";
            this.ddlbanklist.DataSource = ds1.Tables[0];
            this.ddlbanklist.DataBind();
        }

        protected void lnkok_OnClick(object sender, EventArgs e)
        {

            this.BankInterest02();

        }

        private void BankInterest02()
        {
            string comcod = this.GetComeCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            string actcode = this.ddlexpenseType.SelectedValue;
            string cactcode = this.ddlbanklist.Text;
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "ACCOUNTSLEDGER", frmdate, todate, actcode,
                cactcode, "", "", "", "", "");
            Session["tblbankinteret"] = ds.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblbankinteret"];
            this.gvBankInterest.DataSource = dt;
            this.gvBankInterest.DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string title = "Bank Interest";
            string date = "(From " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
            string accdesc = "Bank Name : " + this.ddlbanklist.SelectedItem.ToString();
            string cactdesc = "Accounts Head : " + this.ddlexpenseType.SelectedItem.ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            DataTable dt = (DataTable)Session["tblbankinteret"];

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.BankInterest>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptBankInterest2", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("accountshead", cactdesc));
            Rpt1.SetParameters(new ReportParameter("bankname", accdesc));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rptstk = new ReportDocument ();
            //    rptstk = new RealERPRPT.R_17_Acc.RptBankInterest ();

            //    TextObject rpttxtCactdesc = rptstk.ReportDefinition.ReportObjects["txtcactdesc"] as TextObject;
            //    rpttxtCactdesc.Text = "Accounts Head : " + this.ddlexpenseType.SelectedItem.ToString ();

            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = "Bank Name : " + this.ddlbanklist.SelectedItem.ToString ();

            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = "(From " + Convert.ToDateTime (this.txtfrmdate.Text).ToString ("dd-MMM-yyyy") + " To " + Convert.ToDateTime (this.txttodate.Text).ToString ("dd-MMM-yyyy") + " )";

            //    DataTable dt = (DataTable)Session["tblbankinteret"];
            //if (dt == null)
            //    return;
            //string Headertitle = "Bank Interest";



            //TextObject txtHeadertitle = rptstk.ReportDefinition.ReportObjects["txtHeadertitle"] as TextObject;
            //txtHeadertitle.Text = Headertitle;



            //TextObject txtcompanyname = rptstk.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //txtcompanyname.Text = comnam;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat (compname, username, printdate);


            //rptstk.SetDataSource ((DataTable)Session["tblbankinteret"]);
            ////tring comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath (@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue ("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                      ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";







            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString ();
            //string comcod = hst["comcod"].ToString ();
            //string comadd = hst["comadd1"].ToString ();
            //string compname = hst["compname"].ToString ();
            //string session = hst["session"].ToString ();
            //string username = hst["username"].ToString ();
            ////string hostname = hst["hostname"].ToString();
            //string printdate = System.DateTime.Now.ToString ("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string frmdate = this.txtfrmdate.Text;
            //string todate = this.txttodate.Text;
            //string type = this.ddlexpenseType.SelectedItem.Text;
            //string bank = this.ddlbanklist.SelectedItem.Text;
            //DataTable dt = (DataTable)Session["tblbankinteret"];


            //var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassBankInterest> ();

            //LocalReport Rpt1 = new LocalReport ();

            //Rpt1 = RDLCAccountSetup.GetLocalReport ("R_17_Acc.RptBankInterest", list, null, null);
            //Rpt1.SetParameters (new ReportParameter ("comname", comnam));
            //Rpt1.SetParameters (new ReportParameter ("comadd", comadd));
            //Rpt1.SetParameters (new ReportParameter ("printFooter", printFooter));
            //Rpt1.SetParameters (new ReportParameter ("frmdate", frmdate));
            //Rpt1.SetParameters (new ReportParameter ("todate", todate));
            //Rpt1.SetParameters (new ReportParameter ("type", type));
            //Rpt1.SetParameters (new ReportParameter ("bank", bank));
            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";

        }
    }
}