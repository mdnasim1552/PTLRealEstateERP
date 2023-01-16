using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;


namespace RealERPWEB.F_21_MKT
{
    public partial class MonthsWiseSale : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Sales Report ";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                DateTime nowDate = DateTime.Now;
                DateTime yearfday = new DateTime(nowDate.Year, 1, 1);
                DateTime ylDay = new DateTime(nowDate.Year, 12, 31);


                string fdate = yearfday.ToString("dd-MMM-yyyy");
                string edate = ylDay.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = fdate;
                this.txttodate.Text = edate;

                GetAllSubdata();
                this.lbtnOk_Click(null, null);
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string fromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt1 = (DataTable)ViewState["tblSales"];
            if (dt1 == null)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.EClassYearlySalesCRM>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_MKT.RptYearlySales", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
           
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + fromdate  + " To " + todate  + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Sales Report"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            string events = hst["events"].ToString();

            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Print Special Ledger ";
                string eventdesc = "Print Special Ledger ";
                string eventdesc2 = "Report Print Head  : " +"(From " + fromdate + " To " + todate + ")";

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }



        }

        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
            DataTable dt = ds2.Tables[0];
            DataView dv;
            dv = dt.DefaultView;

            dv.RowFilter = ("gcod like '95%'");
            this.ddlleadstatus.DataTextField = "gdesc";
            this.ddlleadstatus.DataValueField = "gcod";
            this.ddlleadstatus.DataSource = dv.ToTable();
            this.ddlleadstatus.DataBind();
            this.ddlleadstatus.SelectedValue = "9501001";
            this.ddlleadstatus.Enabled = false;

            ds2.Dispose();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string comcod = GetComeCode();
            string status = this.ddlleadstatus.SelectedValue;
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETCRMMONTHLYSALES", fdate, tdate, status, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvMonhtlySales.DataSource = null;
                this.gvMonhtlySales.DataBind();
                return;
            }

            this.gvMonhtlySales.DataSource = ds2.Tables[0];
            this.gvMonhtlySales.DataBind();
         

            ViewState["tblSales"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            this.gvMonhtlySales.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.gvMonhtlySales.DataSource = (DataTable)ViewState["tblSales"];
            this.gvMonhtlySales.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {
            DataTable ddt = (DataTable)ViewState["tblSales"];
            if (ddt.Rows.Count == 0)
                return;
           
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFtqty")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(tqty)", "")) ?
                             0 : ddt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty1")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty1)", "")) ?
                             0 : ddt.Compute("sum(qty1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty2")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty2)", "")) ?
                             0 : ddt.Compute("sum(qty2)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty3")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty3)", "")) ?
                             0 : ddt.Compute("sum(qty3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty4")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty4)", "")) ?
                             0 : ddt.Compute("sum(qty4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty5")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty5)", "")) ?
                             0 : ddt.Compute("sum(qty5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty6")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty6)", "")) ?
                             0 : ddt.Compute("sum(qty6)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty7")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty7)", "")) ?
                             0 : ddt.Compute("sum(qty7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty8")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty8)", "")) ?
                            0 : ddt.Compute("sum(qty8)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty9")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty9)", "")) ?
                            0 : ddt.Compute("sum(qty9)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty10")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty10)", "")) ?
                            0 : ddt.Compute("sum(qty10)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty11")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty11)", "")) ?
                            0 : ddt.Compute("sum(qty11)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFqty12")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(qty12)", "")) ?
                            0 : ddt.Compute("sum(qty12)", ""))).ToString("#,##0;(#,##0); ");


            Session["Report1"] = gvMonhtlySales;
            //((HyperLink)this.gvMonhtlySales.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ((HyperLink)this.gvMonhtlySales.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RDLCViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvMonhtlySales_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMonhtlySales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}