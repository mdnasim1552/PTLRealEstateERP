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
    public partial class PurchaseOrderRecived : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Order Received";
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
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "MONTHWIWESUPBILLRECEIVED", frmdate, todate, "", "", "", "", "", "", "");
            Session["tblsupbill"] = ds1.Tables[0];
            this.Data_Bind();
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
            ((Label)this.grvMWiseSupBill.FooterRow.FindControl("lblgvFbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balance)", "")) ?
                        0 : dt.Compute("sum(balance)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblsupbill"];
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptMonWiseReceived();
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

        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblsupbill"];
            for (int i = 0; i < this.grvMWiseSupBill.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.grvMWiseSupBill.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                //string Sirialno = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvsirialno")).Text.Trim();
                //string Recdate = (((TextBox)this.dgv1.Rows[i].FindControl("txtgvReconDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.dgv1.Rows[i].FindControl("txtgvReconDat")).Text.Trim();
                //dt.Rows[i]["sirialno"] = Sirialno;
                //dt.Rows[i]["recondat"] = Recdate;
                dt.Rows[i]["chkmv"] = chkmr;
                ((CheckBox)this.grvMWiseSupBill.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.grvMWiseSupBill.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.grvMWiseSupBill.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.grvMWiseSupBill.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblsupbill"] = dt;
        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblsupbill"];
            for (int i = 0; i < grvMWiseSupBill.Rows.Count; i++)
            {


                //string  qty = Convert.ToDouble("0" + ((TextBox)this.gvclientchoice.Rows[i].FindControl("txtgvcrate")).Text.Trim()).ToString();


                dt.Rows[i]["taxam"] = Convert.ToDouble("0" + ((TextBox)this.grvMWiseSupBill.Rows[i].FindControl("txttax")).Text.Trim()).ToString();
                dt.Rows[i]["ppayment"] = Convert.ToDouble("0" + ((TextBox)this.grvMWiseSupBill.Rows[i].FindControl("txtppayment")).Text.Trim()).ToString();

                // tbl1.Rows[i]["chk"] = (((CheckBox)gvclientchoice.Rows[i].FindControl("chkgvcc")).Checked) ? "True" : "False"; 


            }



            Session["tblsupbill"] = dt;
        }
        protected void lbok_Click(object sender, EventArgs e)
        {


            //int rowindex=()((LinkButton)sender).CommandArgument)
            this.SaveValue();
            this.CheckValue();
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblsupbill"];
            string orderno = dt.Rows[RowIndex]["orderno"].ToString();
            double ppayment = Convert.ToDouble(dt.Rows[RowIndex]["ppayment"].ToString());
            double taxamt = Convert.ToDouble(dt.Rows[RowIndex]["taxam"].ToString());
            //string  rsircode="970100101001";

            bool resultb = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE", "UPDATERECIVED", orderno, userid, Postdat, ppayment.ToString(), "",
                            "", "", "", "", "", "", "", "", "", "");



            if (!resultb)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }



            if (taxamt != 0.00)


            {
                string rsircode = "970100101001";
                bool resultt = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INSERTORUPSUPTAX", orderno, rsircode, taxamt.ToString(), "", "",
                             "", "", "", "", "", "", "", "", "", "");
                if (!resultt)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


            }

            //this.Data_Bind();
            //this.CheckValue();









        }
    }
}