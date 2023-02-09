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


    public partial class RptBillRegister : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Bill Register Report ";
                //this.Master.Page.Title = "Bill Register Report ";
                this.txtDatfrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatfrom.Text = "01" + this.txtDatfrom.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtDatfrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowGenBill();
        }

        private void ShowGenBill()
        {
            Session.Remove("tblbillno");
            string comcod = this.GetCompCode();
            string date1 = Convert.ToDateTime(this.txtDatfrom.Text).ToString("dd-MMM-yyyy");
            string date2 = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT", "GETREGISTERBILLNO", date1, date2, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblbillno"] = ds1.Tables[0];
            this.Data_Bind();

        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblbillno"];
            this.gvbillregis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvbillregis.DataSource = dt;
            this.gvbillregis.DataBind();

        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblbillno"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.OnlineBillReg>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBillRegister", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtDatfrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Bill Register Report"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptConSD = new RealERPRPT.R_17_Acc.RptBillRegister(); 
            //TextObject rptCname = rptConSD.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptdate = rptConSD.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text ="(From " + Convert.ToDateTime(this.txtDatfrom.Text).ToString("dd-MMM-yyyy") + " To "+  Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")+")";

            //TextObject txtuserinfo = rptConSD.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptConSD.SetDataSource((DataTable)Session["tblbillno"]);       
            //Session["Report1"] = rptConSD;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvbillregis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvbillregis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}