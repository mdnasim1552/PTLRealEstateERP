using System;
using System.Collections;
using System.Collections.Generic;
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
namespace RealERPWEB.F_23_CR
{
    public partial class EarlyPayment : System.Web.UI.Page
    {

        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy"); ;
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Early Payment Benifit";


                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

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
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_SALEARLYBENIFIT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = dt;
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);



        }

        private void GetCustomerName()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_SALEARLYBENIFIT", "GETCUSTOMERNAME", pactcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();

        }



        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlProjectName.Enabled = false;
                this.ddlCustName.Enabled = false;
                this.PanelPayment.Visible = true;
                this.ShowEarlyPayment();
                this.ShowPaymentInfo();
                return;

            }

            this.ddlProjectName.Enabled = true;
            this.ddlCustName.Enabled = true;
            this.PanelPayment.Visible = false;
            this.gvPayment.DataSource = null;
            this.gvPayment.DataBind();
            this.lbtnOk.Text = "Ok";


            //ViewState.Remove("tblepayment");
            //string comcod = this.GetCompCode();
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string custid = this.ddlCustName.SelectedValue.ToString();
            //string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            //DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTINTEREST", pactcode, custid, date, "", "", "", "", "", "");
            //if (ds2 == null)
            //{
            //    this.gvPayment.DataSource = null;
            //    this.gvPayment.DataBind();

            //    return;
            //}

            //ViewState["tblepayment"] = ds2.Tables[0];


            //this.Data_Bind();
            // ds2.Dispose();

        }




        private void ShowEarlyPayment()
        {
            // ViewState.Remove("tblepayment");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_SALEARLYBENIFIT", "SHOWCUSBALANCE", pactcode, custid, date, "", "", "", "", "", "");





            if (ds2 == null)
            {
                this.lbltotalval.Text = "";
                this.lblPaid.Text = "";
                this.lblBalance.Text = "";

                return;
            }

            this.lbltotalval.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["schamt"]).ToString("#,##0;(#,##0);");
            this.lblPaid.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["paidamt"]).ToString("#,##0;(#,##0);"); ;
            this.lblBalance.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["balamt"]).ToString("#,##0;(#,##0);"); ;
            ds2.Dispose();



        }

        private void ShowPaymentInfo()
        {
            ViewState.Remove("tblepayment");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_SALEARLYBENIFIT", "RPTSALERARLYBENIFIT", pactcode, custid, date, "", "", "", "", "", "");





            if (ds2 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();

                return;
            }


            double prinamt = ASTUtility.StrPosOrNagative(this.lblBalance.Text.Trim());
            double inamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(inam)", "")) ?
                                   0 : ds2.Tables[0].Compute("sum(inam)", "")));


            this.lblPrincipalamt.Text = prinamt.ToString("#,##0;(#,##0); ");
            this.lblbenifit.Text = inamt.ToString("#,##0;(#,##0); ");
            this.lblnetpayment.Text = (prinamt - inamt).ToString("#,##0;(#,##0); ");


            ViewState["tblepayment"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
            ds2.Dispose();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string gcod = dt1.Rows[0]["gcod"].ToString();



            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {


                    dt1.Rows[j]["gdesc"] = "";
                    dt1.Rows[j]["schamt"] = 0;
                    // dt1.Rows[j]["schdate"] = "";
                }



                gcod = dt1.Rows[j]["gcod"].ToString();

            }




            return dt1;


        }

        private void Data_Bind()
        {

            this.gvPayment.DataSource = (DataTable)ViewState["tblepayment"];
            this.gvPayment.DataBind();
            this.FooterCal();
        }
        private void FooterCal()
        {

            DataTable dt = (DataTable)ViewState["tblepayment"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvPayment.FooterRow.FindControl("lFschAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ?
                                    0 : dt.Compute("sum(schamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPayment.FooterRow.FindControl("lgvFpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                                  0 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPayment.FooterRow.FindControl("lgvFinamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(inam)", "")) ?
                                  0 : dt.Compute("sum(inam)", ""))).ToString("#,##0;(#,##0); ");



        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblepayment"];


            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTPAYMENTSTATUS", pactcode, custid, "", "", "", "", "", "", "");
            if (ds5 == null)
                return;
            DataTable dtcust = ds5.Tables[0];
            string custname = dtcust.Rows[0]["custname"].ToString();
            string custadd = dtcust.Rows[0]["custadd"].ToString();
            string custmob = dtcust.Rows[0]["custmob"].ToString();
            string pactdesc = dtcust.Rows[0]["pactdesc"].ToString();
            string munit = dtcust.Rows[0]["munit"].ToString();
            string udesc = dtcust.Rows[0]["udesc"].ToString();
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassEarlyBenifit>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptEarlyPayBenifit", list, null, null);

            string usize = Convert.ToDouble(dtcust.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Client Information Details"));
            Rpt1.SetParameters(new ReportParameter("custname", custname));
            Rpt1.SetParameters(new ReportParameter("pactdesc", pactdesc));
            Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
            Rpt1.SetParameters(new ReportParameter("txttoval", this.lbltotalval.Text));
            Rpt1.SetParameters(new ReportParameter("txtpaidamt", this.lblPaid.Text));
            Rpt1.SetParameters(new ReportParameter("txtbalamt", this.lblBalance.Text));
            Rpt1.SetParameters(new ReportParameter("txtprinamt", this.lblPrincipalamt.Text));
            Rpt1.SetParameters(new ReportParameter("txtbenifit", this.lblbenifit.Text));
            Rpt1.SetParameters(new ReportParameter("txtnetpayment", this.lblnetpayment.Text));
            Rpt1.SetParameters(new ReportParameter("txtbenifitrate", Convert.ToDouble(dt.Rows[0]["inrate"]).ToString("#,##0;(#,##0); ") + "%"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";









            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string comcod =this.GetCompCode();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string custid = this.ddlCustName.SelectedValue.ToString();
            //DataTable dt = (DataTable)ViewState["tblepayment"];

            //ReportDocument rptStatus = new RealERPRPT.R_23_CR.RptEarlyPayBenifit();
            //DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTPAYMENTSTATUS", pactcode, custid, "", "", "", "", "", "", "");
            //if (ds5 == null)
            //    return;
            //DataTable dtcust = ds5.Tables[0];
            //string custname = dtcust.Rows[0]["custname"].ToString();
            //string custadd = dtcust.Rows[0]["custadd"].ToString();
            //string custmob = dtcust.Rows[0]["custmob"].ToString();
            //string pactdesc = dtcust.Rows[0]["pactdesc"].ToString();
            //string munit = dtcust.Rows[0]["munit"].ToString();
            //string udesc = dtcust.Rows[0]["udesc"].ToString();
            //string usize = Convert.ToDouble(dtcust.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");
            //TextObject rptcomname = rptStatus.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptcomname.Text = comnam;
            //TextObject rptcustname = rptStatus.ReportDefinition.ReportObjects["custname"] as TextObject;
            //rptcustname.Text = custname;
            //TextObject rptCustAdd = rptStatus.ReportDefinition.ReportObjects["CustAdd"] as TextObject;
            //rptCustAdd.Text = custadd + ", " + "Mobile: " + custmob;
            //TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["pactdesc"] as TextObject;
            //rptpactdesc.Text = pactdesc;
            //TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
            //rptUsize.Text = udesc + ", " + usize + " " + munit;
            //TextObject txttoval = rptStatus.ReportDefinition.ReportObjects["txttoval"] as TextObject;
            //txttoval.Text = this.lbltotalval.Text;
            //TextObject txtpaidamt = rptStatus.ReportDefinition.ReportObjects["txtpaidamt"] as TextObject;
            //txtpaidamt.Text = this.lblPaid.Text;

            //TextObject txtbalamt = rptStatus.ReportDefinition.ReportObjects["txtbalamt"] as TextObject;
            //txtbalamt.Text = this.lblBalance.Text;

            //TextObject txtprinamt = rptStatus.ReportDefinition.ReportObjects["txtprinamt"] as TextObject;
            //txtprinamt.Text = this.lblPrincipalamt.Text;

            //TextObject txtbenifit = rptStatus.ReportDefinition.ReportObjects["txtbenifit"] as TextObject;
            //txtbenifit.Text = this.lblbenifit.Text;
            //TextObject txtnetpayment = rptStatus.ReportDefinition.ReportObjects["txtnetpayment"] as TextObject;
            //txtnetpayment.Text = this.lblnetpayment.Text;

            //TextObject txtbenifitrate = rptStatus.ReportDefinition.ReportObjects["txtbenifitrate"] as TextObject;
            //txtbenifitrate.Text = Convert.ToDouble(dt.Rows[0]["inrate"]).ToString("#,##0;(#,##0); ") + "%";





            //TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptStatus.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptStatus.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptStatus;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlCustName.Text.Trim();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string payment = Convert.ToDouble("0" + this.txtPayment.Text).ToString();
            string inrate = Convert.ToDouble("0" + this.txtinrate.Text.Replace("%", "")).ToString();



            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_SALEARLYBENIFIT", "INORUSALPAYINF", PactCode, Usircode, date, payment, inrate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.ShowPaymentInfo();



        }
    }
}
