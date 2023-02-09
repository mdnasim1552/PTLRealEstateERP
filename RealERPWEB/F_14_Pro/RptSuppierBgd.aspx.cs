using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptSuppierBgd : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Budget";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSupplierName();
                this.GetProjectName();


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
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "GETPURPROJECTNAME_01", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }

        private void GetSupplierName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "GETSUPPLIERNAME01", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "sirdesc";
            this.ddlSubName.DataValueField = "sircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();
            this.GetProjectName();



        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowBillDetails();


        }
        private void ShowBillDetails()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string PactCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string SupplierName = (this.ddlSubName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSubName.SelectedValue.ToString() + "%";
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "RPTSUPPLIERBILLDETAILS02", PactCode, SupplierName, date, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblSupbill"] = ds1.Tables[0];
            this.Data_Bind();

        }

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            DataTable dt = ((DataTable)Session["tblSupbill"]).Copy();
            DataTable dt1; DataView dv;
            if (((CheckBox)this.gvSubBill.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvSubBill.Rows.Count; i++)
                {
                    ((CheckBox)this.gvSubBill.Rows[i].FindControl("chkitem")).Checked = true;
                    dt.Rows[i]["active"] = "True";
                }



                //dv = dt.DefaultView;
                //dv.RowFilter = ("active=1");
                //dt1 = dv.ToTable();
                //this.gvSubBill.DataSource = dt1;
                //this.gvSubBill.DataBind();
                //Session["Report1"] = gvSubBill;


                //this.gvSubBill.DataSource = dt;
                //this.gvSubBill.DataBind();
                //this.FooterCalculation(dt);





            }

            else
            {
                for (i = 0; i < this.gvSubBill.Rows.Count; i++)
                {
                    ((CheckBox)this.gvSubBill.Rows[i].FindControl("chkitem")).Checked = false;
                    dt.Rows[i]["active"] = "False";



                }

            }

            Session["tblSupbill"] = dt;
        }


        protected void lnkSelected_Click(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)Session["tblSupbill"]).Copy();

            int i;
            for (i = 0; i < this.gvSubBill.Rows.Count; i++)
            {
                string chk = ((CheckBox)this.gvSubBill.Rows[i].FindControl("chkitem")).Checked ? "True" : "False";
                //((CheckBox)this.gvSubBill.Rows[i].FindControl("chkitem")).Checked = true;
                dt.Rows[i]["active"] = chk;
            }


            DataTable dt1; DataView dv;


            dv = dt.DefaultView;
            dv.RowFilter = ("active=True");
            dt1 = dv.ToTable();

            this.gvSubBill.DataSource = dt1;
            this.gvSubBill.DataBind();
            this.gvSubBill.Columns[8].Visible = false;
            ((LinkButton)this.gvSubBill.HeaderRow.FindControl("lnkSelected")).Visible = false;  //(this.Request.QueryString["Type"].ToString().Trim() == "Edit" && this.lblBillno.Text.Trim() == "00000000000000");


            Session["Report1"] = gvSubBill;


            //this.gvSubBill.DataSource = dt;
            //this.gvSubBill.DataBind();

            Session["tblSupbill"] = dt1;
            if (dt1.Rows.Count == 0)
                return;
            ((HyperLink)this.gvSubBill.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            this.FooterCalculation(dt1);
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string ssircode = dt1.Rows[0]["ssircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();
                    dt1.Rows[j]["ssirdesc"] = "";
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

            DataTable dt = ((DataTable)Session["tblSupbill"]).Copy();
            this.gvSubBill.DataSource = HiddenSameData(dt);
            this.gvSubBill.DataBind();
            this.FooterCalculation(dt);

        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            DataTable dt2 = new DataTable();
            DataView dv = dt.Copy().DefaultView;

            dv.RowFilter = ("pactcode <> 'ZZZZZZZZZZZZ'");

            dt2 = dv.ToTable();
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(billamt)", "")) ? 0.00 :
                 dt2.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillpendAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(pendingamt)", "")) ? 0.00 :
                 dt2.Compute("sum(pendingamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFtotalbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(totalbill)", "")) ? 0.00 :
                 dt2.Compute("sum(totalbill)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(payment)", "")) ? 0.00 :
                dt2.Compute("sum(payment)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(netpayable)", "")) ? 0.00 :
                dt2.Compute("sum(netpayable)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetBal")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(ncpayable)", "")) ? 0.00 : dt2.Compute("sum(ncpayable)", ""))).ToString("#,##0;(#,##0); ");


            Session["Report1"] = gvSubBill;
            ((HyperLink)this.gvSubBill.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {



            DataTable dt = (DataTable)Session["tblSupbill"];

            DataTable dt2 = new DataTable();
            DataView dv = dt.Copy().DefaultView;

            dv.RowFilter = ("pactcode <> 'ZZZZZZZZZZZZ'");

            dt2 = dv.ToTable();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            var lst = dt2.DataTableToList<RealEntity.C_14_Pro.suppbgd>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RDLCAccountSetup.GetLocalReport("R_14_Pro.RptSuplierBgd", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("title", "Supplier Budget"));

            Rpt1.SetParameters(new ReportParameter("txtcomname", comnam));


            // Rpt1.SetParameters(new ReportParameter("footer", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetSupplierName();
        }

        protected void ddlSubName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
    }
}