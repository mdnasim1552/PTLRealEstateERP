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
namespace RealERPWEB.F_22_Sal
{
    public partial class EntryMonCharges : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetYearMonth();
                this.GetProjectName();
                this.GetCustomer();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Bill Conformation";







            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();


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
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            ds1.Dispose();
            this.GetCustomer();





        }
        private void GetCustomer()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtsrchCustomer = "%" + this.txtSrcCustomer.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETCUSTOMERNAME", pactcode, txtsrchCustomer, "", "", "", "", "", "", "");
            this.ddlCustomer.DataTextField = "sirdesc";
            this.ddlCustomer.DataValueField = "sircode";
            this.ddlCustomer.DataSource = ds1.Tables[0];
            this.ddlCustomer.DataBind();
            ds1.Dispose();

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();

        }
        protected void ibtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomer();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.lblCustomer.Text = (this.ddlCustomer.Items.Count == 0) ? "" : this.ddlCustomer.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.ddlCustomer.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.lblCustomer.Visible = true;
                this.ddlyearmon.Enabled = false;
                this.ShowData();
                return;
            }
            ViewState.Remove("tblcharge");
            this.lbtnOk.Text = "Ok";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtreference.Text = "";
            this.txtSubject.Text = "";
            this.ddlProjectName.Visible = true;
            this.ddlCustomer.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.lblCustomer.Visible = false;
            this.ddlyearmon.Enabled = true;

            this.gvcharges.DataSource = null;
            this.gvcharges.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";


        }






        private void ShowData()
        {

            ViewState.Remove("tblcharge");

            string comcod = this.GetCompCode();
            string Monthid = this.ddlyearmon.SelectedValue.ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustomer.SelectedValue.ToString();


            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETCHARGES", Monthid, PactCode, usircode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvcharges.DataSource = null;
                this.gvcharges.DataBind();
                return;
            }


            ViewState["tblcharge"] = ds1.Tables[0];

            DataRow[] dr = ds1.Tables[0].Select("amount>0");

            if (dr.Length > 0)
            {
                this.txtreference.Text = dr[0]["reference"].ToString();
                this.txtSubject.Text = dr[0]["subjct"].ToString();

            }
            else
            {
                this.txtreference.Text = "";
                this.txtSubject.Text = "";

            }


            this.Data_Bind();

        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblcharge"];
            for (int i = 0; i < gvcharges.Rows.Count; i++)
            {

                tbl1.Rows[i]["gdesc1"] = ((TextBox)this.gvcharges.Rows[i].FindControl("txtgvdescvalue")).Text.Trim();
                tbl1.Rows[i]["amount"] = Convert.ToDouble("0" + ((TextBox)this.gvcharges.Rows[i].FindControl("txtgvcharges")).Text.Trim()).ToString();

            }
            ViewState["tblcharge"] = tbl1;


        }





        protected void Data_Bind()
        {


            DataTable tbl1 = (DataTable)ViewState["tblcharge"];
            this.gvcharges.DataSource = tbl1;
            this.gvcharges.DataBind();

            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.gvcharges.FooterRow.FindControl("lblgvFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amount)", "")) ? 0.00 : tbl1.Compute("Sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); -");

            }

        }






        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();
                DataTable dt = (DataTable)ViewState["tblcharge"];
                string Monthid = this.ddlyearmon.SelectedValue.ToString();
                string reference = this.txtreference.Text.Trim();
                string subject = this.txtSubject.Text.Trim();

                string Pactcode = this.ddlProjectName.SelectedValue.ToString();
                string Usircode = this.ddlCustomer.SelectedValue.ToString();



                foreach (DataRow dr2 in dt.Rows)
                {


                    string gcod = dr2["gcod"].ToString();
                    string gdesc1 = dr2["gdesc1"].ToString();
                    double amount = Convert.ToDouble("0" + dr2["amount"].ToString());

                    if (amount != 0)
                    {
                        bool result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSOUPMONCHRINF", Monthid, Pactcode, Usircode, gcod, gdesc1, amount.ToString(), reference, subject, "", "", "", "", "", "", "");

                        if (result == false)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                            return;
                        }
                    }


                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";








            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetCustomer();
        }




        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.rbtntype.SelectedIndex == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


                string date = Convert.ToDateTime(this.ddlyearmon.SelectedValue.Substring(4, 2) + "-" + System.DateTime.Today.ToString("dd") + "-" + this.ddlyearmon.SelectedValue.Substring(0, 4)).ToString("dd-MMM-yyyy");
                string lpdate = Convert.ToDateTime(date).AddDays(5).ToString("dd-MMM-yyyy");

                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.ddlCustomer.SelectedValue.ToString();
                DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_RENTMGT", "RPTCUSTADDRESS", pactcode, usircode, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                ReportDocument rptletter = new RealERPRPT.R_22_Sal.RptCustSpMessenger();

                TextObject txtrefno = rptletter.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
                txtrefno.Text = "Ref: " + this.txtreference.Text.Trim();
                TextObject txtDate = rptletter.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtDate.Text = "Date: " + Convert.ToDateTime(date).ToString("MMMM dd, yyyy");
                //txtDate.Text =Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd,yyyy");



                string information = "";
                TextObject txtinformation = rptletter.ReportDefinition.ReportObjects["txtinformation"] as TextObject;

                information = (ds2.Tables[0].Rows[0]["cperson"].ToString().Trim() == "") ? "" : ds2.Tables[0].Rows[0]["cperson"].ToString().Trim();
                information = information + ((ds2.Tables[0].Rows[0]["custname"].ToString().Trim() == "") ? "" : "\n" + ds2.Tables[0].Rows[0]["custname"].ToString().Trim());
                information = information + ((ds2.Tables[0].Rows[0]["udesc"].ToString().Trim() == "") ? "" : "\nBusiness Suite # " + ds2.Tables[0].Rows[0]["udesc"].ToString().Trim());
                //  information = information + ((ds2.Tables[0].Rows[0]["pactdesc"].ToString().Trim() == "") ? "" : "\n" + ds2.Tables[0].Rows[0]["pactdesc"].ToString().Trim());
                information = information + ((ds2.Tables[0].Rows[0]["baddress"].ToString().Trim() == "") ? "" : "\n" + ds2.Tables[0].Rows[0]["baddress"].ToString().Trim());
                txtinformation.Text = information;
                //TextObject txtcustomer = rptletter.ReportDefinition.ReportObjects["txtcustomer"] as TextObject;
                //txtcustomer.Text = ds2.Tables[0].Rows[0]["custname"].ToString();
                //TextObject txtunit = rptletter.ReportDefinition.ReportObjects["txtunit"] as TextObject;
                //txtunit.Text = "Business Suite # " + ds2.Tables[0].Rows[0]["udesc"].ToString();
                //TextObject txtAddress = rptletter.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                //txtAddress.Text = ds2.Tables[0].Rows[0]["custadd"].ToString();

                TextObject txtsubject = rptletter.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
                txtsubject.Text = this.txtSubject.Text.Trim();


                TextObject rptPara1 = rptletter.ReportDefinition.ReportObjects["txtpare1"] as TextObject;
                rptPara1.Text = "Enclosed is the Invoice for the Payament of the mothtly lease Rent of Business suite & common service Charge and utility bill against " + "Business Suite # " + ds2.Tables[0].Rows[0]["udesc"].ToString() + " are Payable on or before " + Convert.ToDateTime(lpdate).ToString("MMMM dd, yyyy");



                rptletter.SetDataSource(ds2.Tables[0]);
                Session["Report1"] = rptletter;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }

            else if (this.rbtntype.SelectedIndex == 1)
            {
                this.PrintMonthlyRent();



            }

            else if (this.rbtntype.SelectedIndex == 2)
            {
                this.PrintMonthlyothCharge();



            }

        }
        private void PrintMonthlyRent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string date = Convert.ToDateTime(this.ddlyearmon.SelectedValue.Substring(4, 2) + "-" + System.DateTime.Today.ToString("dd") + "-" + this.ddlyearmon.SelectedValue.Substring(0, 4)).ToString("dd-MMM-yyyy");
            //string date = Convert.ToDateTime(this.ddlyearmon.SelectedValue.Substring(4, 2) + "-02-" + this.ddlyearmon.SelectedValue.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string lpdate = Convert.ToDateTime(date).AddDays(5).ToString("dd-MMM-yyyy");

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustomer.SelectedValue.ToString();
            string yearmon = this.ddlyearmon.SelectedValue.ToString();
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_RENTMGT", "RPTMONTYLYRENT", pactcode, usircode, yearmon, "", "", "", "", "", "");
            if (ds2 == null)
                return;

            DataRow[] dr = ds2.Tables[0].Select("code='006'");
            double Amount = Convert.ToDouble(dr[0]["toamt"]);

            ReportDocument rptmonrent = new RealERPRPT.R_22_Sal.RptCustMonthlyRent();

            TextObject txtrefno = rptmonrent.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
            txtrefno.Text = "Ref: " + this.txtreference.Text.Trim();
            TextObject txtDate = rptmonrent.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + Convert.ToDateTime(date).ToString("MMMM dd, yyyy");

            TextObject txtsubject = rptmonrent.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
            txtsubject.Text = this.txtSubject.Text.Trim() + "(A)";



            string information = "";
            TextObject txtinformation = rptmonrent.ReportDefinition.ReportObjects["txtinformation"] as TextObject;

            information = (ds2.Tables[1].Rows[0]["cperson"].ToString().Trim() == "") ? "" : ds2.Tables[1].Rows[0]["cperson"].ToString().Trim();
            information = information + ((ds2.Tables[1].Rows[0]["custname"].ToString().Trim() == "") ? "" : "\n" + ds2.Tables[1].Rows[0]["custname"].ToString().Trim());
            information = information + ((ds2.Tables[1].Rows[0]["udesc"].ToString().Trim() == "") ? "" : "\nBusiness Suite # " + ds2.Tables[1].Rows[0]["udesc"].ToString().Trim());
            information = information + ((ds2.Tables[1].Rows[0]["baddress"].ToString().Trim() == "") ? "" : "\n" + ds2.Tables[1].Rows[0]["baddress"].ToString().Trim());
            txtinformation.Text = information;

            //TextObject txtcustomer = rptmonrent.ReportDefinition.ReportObjects["txtcustomer"] as TextObject;
            //txtcustomer.Text = ds2.Tables[1].Rows[0]["custname"].ToString();
            //TextObject txtunit = rptmonrent.ReportDefinition.ReportObjects["txtunit"] as TextObject;
            //txtunit.Text = "Business Suite # " + ds2.Tables[1].Rows[0]["udesc"].ToString();
            //TextObject txtAddress = rptmonrent.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = ds2.Tables[1].Rows[0]["custadd"].ToString();

            TextObject txtcomname = rptmonrent.ReportDefinition.ReportObjects["txtcomname"] as TextObject;
            txtcomname.Text = comnam;
            TextObject txtsuite = rptmonrent.ReportDefinition.ReportObjects["txtsuite"] as TextObject;
            txtsuite.Text = ds2.Tables[1].Rows[0]["udesc"].ToString();

            TextObject txtfloor = rptmonrent.ReportDefinition.ReportObjects["txtfloor"] as TextObject;
            txtfloor.Text = ds2.Tables[1].Rows[0]["flrdesc"].ToString();
            TextObject txtflarea = rptmonrent.ReportDefinition.ReportObjects["txtflarea"] as TextObject;
            txtflarea.Text = ds2.Tables[1].Rows[0]["flarea"].ToString();
            TextObject txtleaseperiod = rptmonrent.ReportDefinition.ReportObjects["txtleaseperiod"] as TextObject;
            txtleaseperiod.Text = ds2.Tables[1].Rows[0]["lperiod"].ToString();
            TextObject txttinno = rptmonrent.ReportDefinition.ReportObjects["txttinno"] as TextObject;
            txttinno.Text = ds2.Tables[1].Rows[0]["tinno"].ToString();
            TextObject txtvatregno = rptmonrent.ReportDefinition.ReportObjects["txtvatregno"] as TextObject;
            txtvatregno.Text = ds2.Tables[1].Rows[0]["vatregno"].ToString();

            TextObject txtinword = rptmonrent.ReportDefinition.ReportObjects["txtinword"] as TextObject;
            txtinword.Text = ASTUtility.Trans(Math.Round(Amount), 2);

            TextObject txtdateofpayment = rptmonrent.ReportDefinition.ReportObjects["txtdateofpayment"] as TextObject;
            txtdateofpayment.Text = "On or before " + Convert.ToDateTime(lpdate).ToString("MMMM dd, yyyy");
            TextObject txtpayable = rptmonrent.ReportDefinition.ReportObjects["txtpayable"] as TextObject;
            txtpayable.Text = "*Payable only in favor of  " + ds2.Tables[1].Rows[0]["pactdesc"].ToString();

            rptmonrent.SetDataSource(ds2.Tables[0]);
            Session["Report1"] = rptmonrent;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintMonthlyothCharge()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string date = Convert.ToDateTime(this.ddlyearmon.SelectedValue.Substring(4, 2) + "-" + System.DateTime.Today.ToString("dd") + "-" + this.ddlyearmon.SelectedValue.Substring(0, 4)).ToString("dd-MMM-yyyy");
            //string date = Convert.ToDateTime(this.ddlyearmon.SelectedValue.Substring(4, 2) + "-02-" + this.ddlyearmon.SelectedValue.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string lpdate = Convert.ToDateTime(date).AddDays(5).ToString("dd-MMM-yyyy");

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustomer.SelectedValue.ToString();
            string yearmon = this.ddlyearmon.SelectedValue.ToString();
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_RENTMGT", "RPTMONTYLYOTHRENT", pactcode, usircode, yearmon, "", "", "", "", "", "");
            if (ds2 == null)
                return;

            DataRow[] dr = ds2.Tables[0].Select("code='AAA'");
            double Amount = Convert.ToDouble(dr[0]["toamt"]);

            ReportDocument rptmonrent = new RealERPRPT.R_22_Sal.RptCustMonthlyOthRent();

            TextObject txtrefno = rptmonrent.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
            txtrefno.Text = "Ref: " + this.txtreference.Text.Trim();
            TextObject txtDate = rptmonrent.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + Convert.ToDateTime(date).ToString("MMMM dd, yyyy");

            TextObject txtsubject = rptmonrent.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
            txtsubject.Text = this.txtSubject.Text.Trim() + "(B)";




            string information = "";
            TextObject txtinformation = rptmonrent.ReportDefinition.ReportObjects["txtinformation"] as TextObject;

            information = (ds2.Tables[1].Rows[0]["cperson"].ToString().Trim() == "") ? "" : ds2.Tables[1].Rows[0]["cperson"].ToString().Trim();
            information = information + ((ds2.Tables[1].Rows[0]["custname"].ToString().Trim() == "") ? "" : "\n" + ds2.Tables[1].Rows[0]["custname"].ToString().Trim());
            information = information + ((ds2.Tables[1].Rows[0]["udesc"].ToString().Trim() == "") ? "" : "\nBusiness Suite # " + ds2.Tables[1].Rows[0]["udesc"].ToString().Trim());
            information = information + ((ds2.Tables[1].Rows[0]["baddress"].ToString().Trim() == "") ? "" : "\n" + ds2.Tables[1].Rows[0]["baddress"].ToString().Trim());
            txtinformation.Text = information;


            //TextObject txtcustomer = rptmonrent.ReportDefinition.ReportObjects["txtcustomer"] as TextObject;
            //txtcustomer.Text = ds2.Tables[1].Rows[0]["custname"].ToString();
            //TextObject txtunit = rptmonrent.ReportDefinition.ReportObjects["txtunit"] as TextObject;
            //txtunit.Text = "Business Suite # " + ds2.Tables[1].Rows[0]["udesc"].ToString();
            //TextObject txtAddress = rptmonrent.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = ds2.Tables[1].Rows[0]["custadd"].ToString();

            TextObject txtcomname = rptmonrent.ReportDefinition.ReportObjects["txtcomname"] as TextObject;
            txtcomname.Text = comnam;
            TextObject txtfloor = rptmonrent.ReportDefinition.ReportObjects["txtfloor"] as TextObject;
            txtfloor.Text = ds2.Tables[1].Rows[0]["flrdesc"].ToString();

            TextObject txtsuite = rptmonrent.ReportDefinition.ReportObjects["txtsuite"] as TextObject;
            txtsuite.Text = ds2.Tables[1].Rows[0]["udesc"].ToString();
            TextObject txtflarea = rptmonrent.ReportDefinition.ReportObjects["txtflarea"] as TextObject;
            txtflarea.Text = ds2.Tables[1].Rows[0]["flarea"].ToString();
            TextObject txtleaseperiod = rptmonrent.ReportDefinition.ReportObjects["txtleaseperiod"] as TextObject;
            txtleaseperiod.Text = ds2.Tables[1].Rows[0]["lperiod"].ToString();
            TextObject txttinno = rptmonrent.ReportDefinition.ReportObjects["txttinno"] as TextObject;
            txttinno.Text = ds2.Tables[1].Rows[0]["tinno"].ToString();
            TextObject txtvatregno = rptmonrent.ReportDefinition.ReportObjects["txtvatregno"] as TextObject;
            txtvatregno.Text = ds2.Tables[1].Rows[0]["vatregno"].ToString();

            TextObject txtinword = rptmonrent.ReportDefinition.ReportObjects["txtinword"] as TextObject;
            txtinword.Text = ASTUtility.Trans(Math.Round(Amount), 2);

            TextObject txtdateofpayment = rptmonrent.ReportDefinition.ReportObjects["txtdateofpayment"] as TextObject;
            txtdateofpayment.Text = "On or before " + Convert.ToDateTime(lpdate).ToString("MMMM dd, yyyy");

            TextObject txtpayable = rptmonrent.ReportDefinition.ReportObjects["txtpayable"] as TextObject;
            txtpayable.Text = "*Payable only in favor of  " + ds2.Tables[1].Rows[0]["pactdesc"].ToString();
            rptmonrent.SetDataSource(ds2.Tables[0]);
            Session["Report1"] = rptmonrent;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}