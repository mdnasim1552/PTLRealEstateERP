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
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_23_CR
{
    public partial class RptCustInvoice : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
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
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.ChangeCName();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = type == "Invoice01" ? "Customer Invoice - 01" : type == "Invoice02" ? "Customer Invoice - 02" : "CUSTOMER INVOICE INFORMATION";
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "GETSPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
            this.GetCustomer();
        }

        private void ChangeCName()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Invoice02":
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.gvCustInvoice.Columns[3].HeaderText = "Dues Amount";
                    this.gvCustInvoice.Columns[4].Visible = false;
                    this.lblfrmdate.Visible = false;
                    this.txtfrmdate.Visible = false;
                    break;
            }

        }

        private void GetCustomer()
        {


            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string SearchCustomer = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "GETCUSTOMERNAME", pactcode, SearchCustomer, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds1.Tables[0];
            this.ddlCustName.DataBind();
            ds1.Dispose();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ibtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomer();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Invoice01":
                    PrintCustomerInvoice01();
                    break;

                case "Invoice02":
                    PrintCustomerInvoice02();

                    break;
            }



        }

        private void PrintCustomerInvoice01()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds1 = (DataSet)Session["tblCustinvoice"];
            DataTable dt = ds1.Tables[0];

            DataTable dt1 = ds1.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("grp = 'A'");
            dt1 = dv.ToTable();

            DataTable dt2 = ds1.Tables[0].Copy();
            DataView dv2 = dt.DefaultView;
            dv2.RowFilter = ("grp = 'B'");
            dt2 = dv2.ToTable();

            double topayment = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(todue)", "")) ?
            0.00 : dt1.Compute("Sum(todue)", ""))), 0);

            double netTotal = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todue)", "")) ?
            0.00 : dt.Compute("Sum(todue)", ""))), 0);

            //string Takainword = "Amount in words: " + ASTUtility.Trans(topayment, 2);


            // rdlc start

            string txtTitle = "Invoice";
            string txtRefno = "Ref No";
            string date1 = "Date: " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string txtCname = ds1.Tables[1].Rows[0]["custnam"].ToString();
            string txtCaddress = ds1.Tables[1].Rows[0]["custadd"].ToString();
            string txtCproject = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string txtCunit = ds1.Tables[1].Rows[0]["unitdesc"].ToString();
            string compname2 = "For " + comnam;
            string txtInWord = "Amount in words: " + ASTUtility.Trans(topayment, 2);
            string txtPayNote = "Payment: By an Account Payee Cheque in favour of " + comnam;
            string txtDueDate = "Request is being made to clear the above payment on or before the due date:";
            string txtNote = "Note: Plase ignore this letter if your payment is already done.";
            string txtNtotal = netTotal.ToString("#,##0;(#,##0); ");
            string grp = "";
            if (dt2.Rows.Count > 0)
            {
                grp = dt2.Rows[0]["grpdesc"].ToString();
            }

            var list = dt1.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.Customer_Invoice01>();
            var list2 = dt2.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.Customer_Invoice01>();

            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass1.GetLocalReport("R_23_CR.RptCustomerInvoice02S", list, list2, null);
            rpt.SetParameters(new ReportParameter("CompName", comnam));
            rpt.SetParameters(new ReportParameter("txtAddress", comadd));
            rpt.SetParameters(new ReportParameter("txtTitle", txtTitle));
            rpt.SetParameters(new ReportParameter("txtRefno", txtRefno));
            rpt.SetParameters(new ReportParameter("date1", date1));
            rpt.SetParameters(new ReportParameter("txtCname", txtCname));
            rpt.SetParameters(new ReportParameter("txtCaddress", txtCaddress));
            rpt.SetParameters(new ReportParameter("txtCproject", txtCproject));
            rpt.SetParameters(new ReportParameter("txtCunit", txtCunit));
            rpt.SetParameters(new ReportParameter("compname2", compname2));
            rpt.SetParameters(new ReportParameter("txtInWord", txtInWord));
            rpt.SetParameters(new ReportParameter("txtPayNote", txtPayNote));
            rpt.SetParameters(new ReportParameter("txtDueDate", txtDueDate));
            rpt.SetParameters(new ReportParameter("txtNote", txtNote));

            rpt.SetParameters(new ReportParameter("txtGrp", grp));
            rpt.SetParameters(new ReportParameter("txtNtotal", txtNtotal));

            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            // rdlc end


            //ReportDocument rptstk = new RealERPRPT.R_23_CR.RptCustomerInvoice02S();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = comadd;


            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "Date: " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");

            //TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
            //txtcustname.Text = ds1.Tables[1].Rows[0]["custnam"].ToString();
            //TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
            //txtCustaddress.Text = ds1.Tables[1].Rows[0]["custadd"].ToString();


            //TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            //TextObject txtunitdesc = rptstk.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
            //txtunitdesc.Text = ds1.Tables[1].Rows[0]["unitdesc"].ToString();


            //TextObject txtbodyarea = rptstk.ReportDefinition.ReportObjects["txtbodyarea"] as TextObject;
            //txtbodyarea.Text = "According to agreed payment schdule following payments will be due.";

            ////TextObject txtamountinword = rptstk.ReportDefinition.ReportObjects["txtamountinword"] as TextObject;
            ////txtamountinword.Text = Takainword;

            //TextObject txtpaymenttype = rptstk.ReportDefinition.ReportObjects["txtpaymenttype"] as TextObject;
            //txtpaymenttype.Text = "Payment: By an Account Payee Cheque in favour of " + comnam;

            //TextObject txtforcompany = rptstk.ReportDefinition.ReportObjects["txtforcompany"] as TextObject;
            //txtforcompany.Text = "For " + comnam;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);


            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintCustomerInvoice02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataSet ds1 = (DataSet)Session["tblCustinvoice"];
            DataTable dt = ds1.Tables[0];

            DataTable dt1 = ds1.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("grp = 'A'");
            dt1 = dv.ToTable();

            DataTable dt2 = ds1.Tables[0].Copy();
            DataView dv2 = dt.DefaultView;
            dv2.RowFilter = ("grp = 'B'");
            dt2 = dv2.ToTable();

            double topayment = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(todue)", "")) ?
            0.00 : dt1.Compute("Sum(todue)", ""))), 0);

            double netTotal = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todue)", "")) ?
            0.00 : dt.Compute("Sum(todue)", ""))), 0);



            // rdlc start

            string txtTitle = "Invoice";
            string txtRefno = "Ref No";
            string date1 = "Date: " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string txtCname = ds1.Tables[1].Rows[0]["custnam"].ToString();
            string txtCaddress = ds1.Tables[1].Rows[0]["custadd"].ToString();
            string txtCproject = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string txtCunit = ds1.Tables[1].Rows[0]["unitdesc"].ToString();
            string compname2 = "For " + comnam;
            string txtInWord = "Amount in words: " + ASTUtility.Trans(topayment, 2);
            string txtPayNote = "Payment: By an Account Payee Cheque in favour of " + comnam;
            string txtDueDate = "Request is being made to clear the above payment on or before the due date:";
            string txtNote = "Note: Plase ignore this letter if your payment is already done.";
            string txtNtotal = netTotal.ToString("#,##0;(#,##0); ");
            string valueDate = "Value Date: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string grp = "";
            if (dt2.Rows.Count > 0)
            {
                grp = dt2.Rows[0]["grpdesc"].ToString();
            }

            var list = dt1.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.Customer_Invoice01>();
            var list2 = dt2.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.Customer_Invoice01>();

            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass1.GetLocalReport("R_23_CR.RptCustomerInvoice02", list, list2, null);
            rpt.SetParameters(new ReportParameter("CompName", comnam));
            rpt.SetParameters(new ReportParameter("txtAddress", comadd));
            rpt.SetParameters(new ReportParameter("txtTitle", txtTitle));
            rpt.SetParameters(new ReportParameter("txtRefno", txtRefno));
            rpt.SetParameters(new ReportParameter("date1", date1));
            rpt.SetParameters(new ReportParameter("txtCname", txtCname));
            rpt.SetParameters(new ReportParameter("txtCaddress", txtCaddress));
            rpt.SetParameters(new ReportParameter("txtCproject", txtCproject));
            rpt.SetParameters(new ReportParameter("txtCunit", txtCunit));
            rpt.SetParameters(new ReportParameter("compname2", compname2));
            rpt.SetParameters(new ReportParameter("txtInWord", txtInWord));
            rpt.SetParameters(new ReportParameter("txtPayNote", txtPayNote));
            rpt.SetParameters(new ReportParameter("txtDueDate", txtDueDate));
            rpt.SetParameters(new ReportParameter("txtNote", txtNote));
            rpt.SetParameters(new ReportParameter("txtBody", "According to agreed payment schdule following payments will be due."));
            rpt.SetParameters(new ReportParameter("valueDate", valueDate));
            rpt.SetParameters(new ReportParameter("txtGrp", grp));
            rpt.SetParameters(new ReportParameter("txtNtotal", txtNtotal));

            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            // rdlc end









            //ReportDocument rptstk = new RealERPRPT.R_23_CR.RptCustomerInvoice02();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = comadd;

            //TextObject txtissueDate = rptstk.ReportDefinition.ReportObjects["txtissueDate"] as TextObject;
            //txtissueDate.Text = "Issue Date: " +System.DateTime.Today.ToString("dd-MMM-yyyy");

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "Value Date: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            //TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
            //txtcustname.Text = ds1.Tables[1].Rows[0]["custnam"].ToString();
            //TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
            //txtCustaddress.Text = ds1.Tables[1].Rows[0]["custadd"].ToString();


            //TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            //TextObject txtunitdesc = rptstk.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
            //txtunitdesc.Text = ds1.Tables[1].Rows[0]["unitdesc"].ToString();


            //TextObject txtbodyarea = rptstk.ReportDefinition.ReportObjects["txtbodyarea"] as TextObject;
            //txtbodyarea.Text = "According to agreed payment schdule following payments will be due.";

            //TextObject txtamountinword = rptstk.ReportDefinition.ReportObjects["txtamountinword"] as TextObject;
            //txtamountinword.Text = Takainword;

            //TextObject txtpaymenttype = rptstk.ReportDefinition.ReportObjects["txtpaymenttype"] as TextObject;
            //txtpaymenttype.Text = "Payment: By an Account Payee Cheque in favour of " + comnam;

            //TextObject txtforcompany = rptstk.ReportDefinition.ReportObjects["txtforcompany"] as TextObject;
            //txtforcompany.Text = "For " + comnam;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);


            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomer();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string CustCode = this.ddlCustName.SelectedValue.ToString();
            string fromdate = (this.Request.QueryString["Type"].ToString() == "Invoice02") ? Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTINVOICELETTER", pactcode, CustCode, fromdate, todate, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvCustInvoice.DataSource = null;
                this.gvCustInvoice.DataBind();
                return;
            }

            Session["tblCustinvoice"] = ds2;

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt = (ds2.Tables[0]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("grp = 'A'");
            dt1 = dv.ToTable();
            this.gvCustInvoice.DataSource = dt1;
            this.gvCustInvoice.DataBind();
            this.FooterCalculation(dt1);




            dv = dt.DefaultView;
            dv.RowFilter = ("grp = 'B'");
            dt1 = dv.ToTable();
            this.gvChqnocl.DataSource = dt1;
            this.gvChqnocl.DataBind();
            this.lblchequenotyetcl.Visible = false;
            if (dt1.Rows.Count > 0)
            {
                this.lblchequenotyetcl.Visible = true;
                ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(predue)", "")) ?
                  0.00 : dt1.Compute("Sum(predue)", ""))).ToString("#,##0;(#,##0); ");

            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string grp;

            grp = dt1.Rows[0]["grp"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                    dt1.Rows[j]["grpdesc"] = "";
                grp = dt1.Rows[j]["grp"].ToString();
            }




            return dt1;

        }


        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvCustInvoice.FooterRow.FindControl("lgvFPreDue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(predue)", "")) ?
                0.00 : dt.Compute("Sum(predue)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustInvoice.FooterRow.FindControl("lgvFCurDue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(curdue)", "")) ?
              0.00 : dt.Compute("Sum(curdue)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustInvoice.FooterRow.FindControl("lgvFDelayCh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(delayamt)", "")) ?
              0.00 : dt.Compute("Sum(delayamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustInvoice.FooterRow.FindControl("lgvFtopayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todue)", "")) ?
              0.00 : dt.Compute("Sum(todue)", ""))).ToString("#,##0;(#,##0); ");


        }
    }
}