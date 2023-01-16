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
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{

    public partial class RptMonthWiseSupplierBill : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
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

                //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Bill (Order Advanced)";

                this.txtDateFrom.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);



        }

        protected void lnkbtnok2_Click(object sender, EventArgs e)
        {

            this.ShowMonthWiseSuppInfo();
        }

        private void ShowMonthWiseSuppInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "MONTHWIWESUPBILL", frmdate, todate, "", "", "", "", "", "", "");
            Session["tblsupbill"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }


            string ssircode = dt1.Rows[0]["ssircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();
                    dt1.Rows[j]["sirdesc"] = "";
                }

                else
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();


                }

            }



            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsupbill"];
            this.grvMWiseSupBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.grvMWiseSupBill.DataSource = dt;
            this.grvMWiseSupBill.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblsupbill"];

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grvMWiseSupBill.FooterRow.FindControl("lblFordamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(orderamt)", "")) ?
                           0 : dt.Compute("sum(orderamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.grvMWiseSupBill.FooterRow.FindControl("lblFadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(advamt)", "")) ?
                           0 : dt.Compute("sum(advamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.grvMWiseSupBill.FooterRow.FindControl("lblFPpayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ppayment)", "")) ?
            //              0 : dt.Compute("sum(ppayment)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.grvMWiseSupBill.FooterRow.FindControl("lblFpay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ?
                           0 : dt.Compute("sum(trnam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.grvMWiseSupBill.FooterRow.FindControl("lblFtax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tax)", "")) ?
                       0 : dt.Compute("sum(tax)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.grvMWiseSupBill.FooterRow.FindControl("lblgvFbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balance)", "")) ?
                        0 : dt.Compute("sum(balance)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void lbtdeleteorderno_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsupbill"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rownum = (this.grvMWiseSupBill.PageSize) * (this.grvMWiseSupBill.PageIndex) + RowIndex;
      
           
            string ordreno = ((Label)this.grvMWiseSupBill.Rows[rownum].FindControl("lblorderno")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE", "MONTHSUPPLIERTAXDELETE",
                       ordreno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[rownum].Delete();
            }

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Fail !!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }




            DataView dv = dt.DefaultView;
            Session.Remove("tblsupbill");
            Session["tblsupbill"] = dv.ToTable();
            this.Data_Bind();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            //this.RptMonWiseSupplier();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblsupbill"];
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptMonWieSubBillStatus();

            TextObject rptCname = rptstk.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtFDate1 = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "(From " + txtDateFrom.Text + " To " + txtDateto.Text + ")";
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        protected void grvMWiseSupBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvMWiseSupBill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

       
    }
}