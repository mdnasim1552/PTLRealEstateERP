using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
namespace RealERPWEB.F_09_PImp
{
    public partial class SubConBillAllPrj : System.Web.UI.Page
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

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sub-Contractor's Project Wise Bill Report ";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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
            this.ShowBillDetails();


        }
        private void ShowBillDetails()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();

            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SUBCONTRACTOR", "SUBCONBILLPRJWISE", date, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblConbill"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblConbill"];
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();
            this.FooterCalculation(dt);

        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                 dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
                dt.Compute("sum(payment)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 :
                dt.Compute("sum(netpayable)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;



            DataTable dt = (DataTable)Session["tblConbill"];

            DataTable newdt = dt.Copy();
            newdt.Rows.Clear();
            for (int i = 0; i < this.gvSubBill.Rows.Count; i++)
            {

                bool chekvalue = ((CheckBox)this.gvSubBill.Rows[i].FindControl("chkprint")).Checked;
                if (chekvalue == true)
                {
                    DataRow dr = dt.Rows[i];
                    newdt.Rows.Add(dr.ItemArray);
                }
            }



            string date = this.txtDate.Text.ToString();
            var list = newdt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.SubconbillPrjwise>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_09_PIMP.RptsubConBillPrj", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("title", "Project Wise Sub-Contractor Bill Report"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}
