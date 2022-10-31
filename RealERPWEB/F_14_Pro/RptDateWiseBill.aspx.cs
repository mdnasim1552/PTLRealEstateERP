using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using RealERPLIB;
using RealERPLIB;
using System.Data.OleDb;
using System.Data;
using Microsoft.Reporting.WinForms;


using System.Collections;

namespace RealERPWEB.F_14_Pro
{
    public partial class RptDateWiseBill : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Date Wise Bill";
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            }
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
           
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
           


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "GETDATWISESUPPLIERBILL", frmdate, todate, "", "", "", "");
            if (ds1 == null)
            {
                this.gvDWBill.DataSource = null;
                this.gvDWBill.DataBind();

                return;
            }
            Session["tblDateWiseBill"] = ds1.Tables[0];

            this.Data_Bind();
        }
       
       
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblDateWiseBill"];
            this.gvDWBill.DataSource = (DataTable)Session["tblDateWiseBill"];
            this.gvDWBill.DataBind();
            FooterCalculation();

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }


        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string fromdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblDateWiseBill"];
            LocalReport Rpt1 = new LocalReport();


            int index;
            for (int i = 0; i < this.gvDWBill.Rows.Count; i++)
            {
                string isPrint = (((CheckBox)gvDWBill.Rows[i].FindControl("isPrint")).Checked) ? "True" : "False";
                index = (this.gvDWBill.PageSize) * (this.gvDWBill.PageIndex) + i;
                dt.Rows[index]["isPrint"] = isPrint;
            }

            Session["tblDateWiseBill"] = dt;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "isPrint = 'True'";
            DataTable dt1 = dv.ToTable();




            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPayment.EclassRptDateWiseBill>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptDateWiseBill", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printtodate","Date: "+printdate));
            Rpt1.SetParameters(new ReportParameter("printdate", "( " + fromdate + " To " + todate + " )"));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Date Wise Bill"));

            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            int i, index;
            if (((CheckBox)this.gvDWBill.HeaderRow.FindControl("chkAllfrm")).Checked)
            {
                for (i = 0; i < this.gvDWBill.Rows.Count; i++)
                {
                    ((CheckBox)this.gvDWBill.Rows[i].FindControl("isPrint")).Checked = true;

                    index = (this.gvDWBill.PageSize) * (this.gvDWBill.PageIndex) + i;
                    dt.Rows[index]["isPrint"] = "True";
                }
            }

            else
            {
                for (i = 0; i < this.gvDWBill.Rows.Count; i++)
                {
                    ((CheckBox)this.gvDWBill.Rows[i].FindControl("isPrint")).Checked = false;

                    index = (this.gvDWBill.PageSize) * (this.gvDWBill.PageIndex) + i;
                    dt.Rows[index]["isPrint"] = "False";
                }
            }

            Session["tblEmpstatus"] = dt;
            // this.ShowPer();

        }



        protected void gvDWBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDWBill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            this.Data_Bind();
        }
        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblDateWiseBill"];

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvDWBill.FooterRow.FindControl("lgvtotalappamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 : dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvDWBill.FooterRow.FindControl("lgvtotaladvamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(advamt)", "")) ? 0.00 : dt.Compute("sum(advamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvDWBill.FooterRow.FindControl("lgvtotalnetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 : dt.Compute("sum(netpayable)", ""))).ToString("#,##0.00;(#,##0.00); ");

            }

            else
            {
                return;
            }

        }
    }
}