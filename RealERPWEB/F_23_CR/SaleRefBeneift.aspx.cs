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
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_23_CR
{
    public partial class SaleRefBeneift : System.Web.UI.Page
    {

        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy"); ;
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "INTEREST CALCULATION INFORMATION";

                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


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

            this.ShowRefBenInfo();
        }





        private void ShowRefBenInfo()
        {
            ViewState.Remove("tblepayment");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string inrate = Convert.ToDouble("0" + this.txtinrate.Text.Replace("%", "")).ToString();


            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_SALEARLYBENIFIT", "RPTSALEREFUNBENIFIT", pactcode, custid, date, inrate, "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();

                return;
            }


            ViewState["tblepayment"] = ds2.Tables[0];
            ViewState["tblgeninfo"] = ds2.Tables[1];
            this.Data_Bind();
            ds2.Dispose();


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


            ((Label)this.gvPayment.FooterRow.FindControl("lFrcvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
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
            DataTable dt = (DataTable)ViewState["tblepayment"];
            DataTable dt1 = (DataTable)ViewState["tblgeninfo"];

            ReportDocument rptStatus = new RealERPRPT.R_23_CR.RptRefundableben();
            TextObject rpttxtCompanyName = rptStatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rpttxtCompanyName.Text = comnam;

            TextObject txtDate = rptStatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + this.txtDate.Text.Trim();
            TextObject rptcustname = rptStatus.ReportDefinition.ReportObjects["custname"] as TextObject;
            rptcustname.Text = dt1.Rows[0]["name"].ToString();
            TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["pactdesc"] as TextObject;
            rptpactdesc.Text = dt1.Rows[0]["projectname"].ToString();
            TextObject rptUnitDesc = rptStatus.ReportDefinition.ReportObjects["UnitDesc"] as TextObject;
            rptUnitDesc.Text = dt1.Rows[0]["aptname"].ToString();
            TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
            rptUsize.Text = dt1.Rows[0]["aptsize"].ToString();

            TextObject rptHandoverdate = rptStatus.ReportDefinition.ReportObjects["Handoverdate"] as TextObject;
            rptHandoverdate.Text = Convert.ToDateTime(dt1.Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            TextObject rptapartmentprice = rptStatus.ReportDefinition.ReportObjects["apartmentprice"] as TextObject;
            rptapartmentprice.Text = Convert.ToDecimal(dt1.Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
            TextObject rptcarparking = rptStatus.ReportDefinition.ReportObjects["carparking"] as TextObject;
            rptcarparking.Text = Convert.ToDecimal(dt1.Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
            TextObject rptUtility = rptStatus.ReportDefinition.ReportObjects["Utility"] as TextObject;
            rptUtility.Text = Convert.ToDecimal(dt1.Rows[0]["utility"]).ToString("#,##0;(#,##0); ");

            TextObject txttoprice = rptStatus.ReportDefinition.ReportObjects["txttoprice"] as TextObject;
            txttoprice.Text = Convert.ToDecimal(dt1.Rows[0]["toprice"]).ToString("#,##0;(#,##0);");
            TextObject txttorecieved = rptStatus.ReportDefinition.ReportObjects["txttorecieved"] as TextObject;
            txttorecieved.Text = Convert.ToDecimal(dt1.Rows[0]["toreceived"]).ToString("#,##0;(#,##0); ");

            TextObject txtrefundableamt = rptStatus.ReportDefinition.ReportObjects["txtrefundableamt"] as TextObject;
            txtrefundableamt.Text = Convert.ToDecimal(dt1.Rows[0]["toreceived"]).ToString("#,##0;(#,##0); ");


            TextObject txtinamt = rptStatus.ReportDefinition.ReportObjects["txtinamt"] as TextObject;
            txtinamt.Text = Convert.ToDecimal(dt1.Rows[0]["inam"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetrefundamt = rptStatus.ReportDefinition.ReportObjects["txtnetrefundamt"] as TextObject;
            txtnetrefundamt.Text = Convert.ToDecimal(dt1.Rows[0]["netrefunam"]).ToString("#,##0;(#,##0); ");










            TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptStatus.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptStatus.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptStatus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}

            //string comcod = this.GetCompCode();
            //string PactCode = this.ddlProjectName.SelectedValue.ToString();
            //string Usircode = this.ddlCustName.Text.Trim();
            //string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //string payment = Convert.ToDouble("0" + this.txtPayment.Text).ToString();
            //string inrate = Convert.ToDouble("0" + this.txtinrate.Text.Replace("%", "")).ToString();



            //bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_SALEARLYBENIFIT", "INORUSALPAYINF", PactCode, Usircode, date, payment, inrate, "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
            //    return;

            //}

            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //this.ShowPaymentInfo();



        }
    }
}
