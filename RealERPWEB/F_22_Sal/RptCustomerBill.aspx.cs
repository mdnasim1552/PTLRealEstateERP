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
using System.IO;
using RealERPLIB;
using RealERPRPT;
using AjaxControlToolkit;
using RealEntity.C_22_Sal;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptCustomerBill : System.Web.UI.Page
    {

        ProcessAccess CustData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        SalesInvoice_BL GetCompinf = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Customer Bill Information";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //

                this.GetProjectName();
                //this.lbtnOk_Click(null, null);
                // this.GetPrevBill();

            }
        }



        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "GETPROJECTNAME", "%%", "%", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);



        }

        private void GetCustomerName()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "GETCUSTOMERNAME", pactcode, "%%", "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();


        }

        private void GetPrevBill()
        {
            string comcod = this.GetCompCode();
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string sircode = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "GETPREVBILL", date, "", "", "", "", "", "", "", "");
            this.ddlPrevBillNo.DataTextField = "billdesc";
            this.ddlPrevBillNo.DataValueField = "billno";
            this.ddlPrevBillNo.DataSource = ds2.Tables[0];
            this.ddlPrevBillNo.DataBind();


        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.GetCustomerBill();
                this.ltbnPrevBill.Visible = false;
                this.ddlPrevBillNo.Visible = false;
                this.ddlPrevBillNo.Items.Clear();

                this.ddlProjectName.Enabled = false;
                this.ddlCustName.Enabled = false;
                this.GenerateBillNo();
                return;

            }
            this.lbtnOk.Text = "Ok";
            this.ltbnPrevBill.Visible = true;
            this.ddlPrevBillNo.Visible = true;
            this.txtBillNo.Text = "";

            this.ddlProjectName.Enabled = true;
            this.ddlCustName.Enabled = true;

            this.ddlProjectName.Visible = true;
            this.ddlCustName.Visible = true;
            this.lblddlProjectName.Visible = false;
            this.lblddlCustName.Visible = false;

            this.txtSubject.ReadOnly = false;
            this.txtLetter.ReadOnly = false;



            this.GetProjectName();
            this.ddlCustName.DataSource = null;
            this.ddlCustName.DataBind();
            this.gvCustBill.DataSource = null;
            this.gvCustBill.DataBind();



        }
        private void GetCustomerBill()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string sircode = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "GETBILLINFO", pactcode, sircode, date, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count < 0)
                return;
            Session["tblcusbill01"] = ds1.Tables[0];
            Session["tblCutomerInfo"] = ds1.Tables[2];
            Session["tblPaidInfo"] = ds1.Tables[1];
            // Session["tblbillno"] = ds1.Tables[2];
            //  this.txtBillNo.Text = ds1.Tables[2].Rows[0]["billno"].ToString();
            this.bind_CustBildata();

        }

        //private void gvBind_data()
        //{
        //    DataTable dt1 = (DataTable)Session["tblcusbill01"];
        //    this.gvCustBill.DataSource = dt1;
        //    this.gvCustBill.DataBind();
        //    this.bind_CustBildata();


        //    //double schamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("schamt", "")) ? 0 : dt2.Compute("schamt", "")));


        //    //((Label)this.gvCustBill.FooterRow.FindControl("lgvFschamt")).Text = Convert.ToDouble(dt2.Rows[0]["schamt"]).ToString("#,##0;(#,##0); ");
        //    //((Label)this.gvCustBill.FooterRow.FindControl("lgvFrecvamt")).Text = Convert.ToDouble(dt2.Rows[0]["schamt"]).ToString("#,##0;(#,##0); ");


        //}




        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }
        protected void gvCustBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label date = (Label)e.Row.FindControl("lgvDate");
                Label itemdesc = (Label)e.Row.FindControl("lblItemdesc");
                Label gvSchamt = (Label)e.Row.FindControl("lblgvSchamt");
                Label lblgvSchamt = (Label)e.Row.FindControl("lblgvSchamt");
                Label lblslno = (Label)e.Row.FindControl("lblslno");

                LinkButton gvDel = (LinkButton)e.Row.FindControl("lnkDelete");
                double schamt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "schamt"));

                //if (schamt == 0)
                //{
                //    //gvSchamt.Font.Bold = true;
                //    //itemdesc.Font.Bold = true;
                //    //itemdesc.Style.Add("vis", "right");
                //    itemdesc.Visible = false;
                //    gvSchamt.Visible = false;
                //    gvDel.Visible = false;
                //    lblslno.Visible = false;
                //    lblgvSchamt.Visible = false;
                //}
            }
        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string userid = hst["usrid"].ToString();
                string pdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string sircode = this.ddlCustName.SelectedValue.ToString();
                string bill_date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                string subject = this.txtSubject.Text.ToString();
                string letter = this.txtLetter.Text.ToString();

                if (this.ddlPrevBillNo.Items.Count == 0)
                {
                    this.GenerateBillNo();
                }

                string billno = this.txtBillNo.Text.ToString() == "" ? "" : this.txtBillNo.Text.ToString();

                DataTable dt = (DataTable)Session["tblcusbill01"];
                //DataTable dt1 = (DataTable)Session["tblPaidInfo"];


                //string schamt = ((Label)this.gvCustBill.FooterRow.FindControl("lblgvScamt")).Text.ToString();
                //string recvamt = ((Label)this.gvCustBill.FooterRow.FindControl("lblvalRecv")).Text.ToString();
                //string payable = ((Label)this.gvCustBill.FooterRow.FindControl("lblvalNetPay")).Text.ToString();
                //double payable2 = Convert.ToDouble(dt.Rows[0]["payable"]);
                //string payword = ASTUtility.Trans(Math.Round(payable2), 2);
                //
                //double Schamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ? 0.00 : dt.Compute("Sum(schamt)", "")));
                //double rcvamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(paidamt)", "")) ? 0.00 : dt1.Compute("Sum(paidamt)", "")));
                //double balamt = Schamt - rcvamt;


                double Schamt = (double)ViewState["Schamt"];
                double rcvamt = (double)ViewState["rcvamt"];
                double payamt = (double)ViewState["balamt"];

                string schamt = Schamt.ToString();
                string recvamt = rcvamt.ToString();
                string payable = payamt.ToString();



                bool result = CustData.UpdateTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "SAVECUSTOMERBILL", pactcode, sircode, billno, bill_date, subject, letter, schamt, recvamt, payable, userid, pdate, "", "");
                if (result == true)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Saved Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                }
                foreach (DataRow dr in dt.Rows)
                {
                    string schamt2 = dr["schamt"].ToString().Trim();
                    string gcod = dr["gcod"].ToString().Trim();
                    string gdesc = dr["gdesc"].ToString().Trim();

                    bool result2 = CustData.UpdateTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "SAVEBILLDETAILS", billno, schamt2, gcod, gdesc);

                    if (result2 == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }

                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private void GenerateBillNo()
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "GENERATEBILLNO", date, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count < 0)
                return;
            Session["tblbillno"] = ds1.Tables[0];
            this.txtBillNo.Text = ds1.Tables[0].Rows[0]["billno"].ToString();
            this.txtBillNo1.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
            this.txtBillNo2.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);

        }

        protected void ltbnPrevBill_Click(object sender, EventArgs e)
        {
            this.GetPrevBill();
        }
        protected void ddlPrevBillNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.getPrevCustomerBill();
        }

        private void getPrevCustomerBill()
        {
            string prevbill = this.ddlPrevBillNo.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string sircode = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "GETPREVBILLINFO", prevbill, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count < 0)
                return;

            this.ddlProjectName.Visible = false;
            //this.ddlPrevBillNo.Items.Clear();
            this.lblddlProjectName.Visible = true;
            this.lblddlProjectName.Text = ds1.Tables[0].Rows[0]["actdesc"].ToString();

            this.ddlCustName.Visible = false;
            // this.ddlCustName.Items.Clear();
            this.lblddlCustName.Visible = true;
            this.lblddlCustName.Text = ds1.Tables[0].Rows[0]["sirdesc"].ToString();
            this.txtSubject.Text = ds1.Tables[0].Rows[0]["bill_subject"].ToString();
            this.txtSubject.ReadOnly = true;
            this.txtLetter.Text = ds1.Tables[0].Rows[0]["bill_letter"].ToString();
            this.txtLetter.ReadOnly = true;


            string actcode2 = ds1.Tables[0].Rows[0]["actcode"].ToString();
            string sircode2 = ds1.Tables[0].Rows[0]["sircode"].ToString();
            string billdate2 = ds1.Tables[0].Rows[0]["billdate"].ToString();


            //DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_CUSTOMER_BILL", "GETBILLINFO", actcode2, sircode2, billdate2, "", "", "", "", "", "");
            //if (ds2.Tables[0].Rows.Count < 0)
            //    return;
            //Session["tblcusbill01"] = ds2.Tables[0];
            //Session["tblCutomerInfo"] = ds2.Tables[2];
            Session["dsPrevBill"] = ds1;
            this.bind_CustBildata2();



            this.lbtnOk.Text = "New";

            //((LinkButton)this.gvCustBill.FooterRow.FindControl("lnkbtnSubmit")).Visible = false;


        }




        private void bind_CustBildata()
        {
            DataTable dt = (DataTable)Session["tblcusbill01"];

            //DataTable dt1 = dt.Copy();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("schamt > '0'");
            //dt1 = dv.ToTable();

            this.gvCustBill.DataSource = dt;
            this.gvCustBill.DataBind();
            this.footerCalculations();

        }

        private void footerCalculations()
        {
            DataTable dt1 = (DataTable)Session["tblcusbill01"];
            DataTable dt2 = (DataTable)Session["tblPaidInfo"];

            if (dt1.Rows.Count == 0)
                return;


            ((Label)this.gvCustBill.FooterRow.FindControl("lblgvScamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(schamt)", "")) ?
                    0.00 : dt1.Compute("sum(schamt)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvCustBill.FooterRow.FindControl("lblvalRecv")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(paidamt)", "")) ?
            0.00 : dt2.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0);-");


            double Schamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(schamt)", "")) ? 0.00 : dt1.Compute("Sum(schamt)", "")));
            double rcvamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(paidamt)", "")) ? 0.00 : dt2.Compute("Sum(paidamt)", "")));
            double balamt = Schamt - rcvamt;
            ViewState["Schamt"] = Schamt;
            ViewState["rcvamt"] = rcvamt;
            ViewState["balamt"] = balamt;

            ((Label)this.gvCustBill.FooterRow.FindControl("lblvalNetPay")).Text = balamt.ToString("#,##0;(#,##0); ");


            //((Label)this.gvCustBill.FooterRow.FindControl("lblvalNetPay")).Text ="";

        }

        private void bind_CustBildata2()
        {
            DataSet ds = (DataSet)Session["dsPrevBill"];
            this.gvCustBill.DataSource = ds.Tables[1];
            this.gvCustBill.DataBind();

            this.footerCalculations2();

        }

        private void footerCalculations2()
        {
            DataSet ds = (DataSet)Session["dsPrevBill"];
            double schamt = Convert.ToDouble(ds.Tables[0].Rows[0]["schamt"]);
            double received = Convert.ToDouble(ds.Tables[0].Rows[0]["received"]);
            double payable = Convert.ToDouble(ds.Tables[0].Rows[0]["payable"]);

            ((Label)this.gvCustBill.FooterRow.FindControl("lblgvScamt")).Text = schamt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustBill.FooterRow.FindControl("lblvalRecv")).Text = received.ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustBill.FooterRow.FindControl("lblvalNetPay")).Text = payable.ToString("#,##0;(#,##0); ");

            ((LinkButton)this.gvCustBill.FooterRow.FindControl("lnkbtnUpdate")).Visible = false;

            for (int i = 0; i < gvCustBill.Rows.Count; i++)
            {
                LinkButton lnkBtn = (LinkButton)gvCustBill.Rows[i].FindControl("lnkDelete");
                lnkBtn.Visible = false;
            }


        }

        protected void lbtnPrevOk_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.printCustomerBilP2p();

        }
        private void printCustomerBilP2p()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();

            // 
            string txtAddress = "";
            string txtAddress2 = "";

            //  string txtSubject = "";
            string txtSignatory = this.txtThank.Text.ToString() == "" ? "" : this.txtThank.Text.ToString();

            //string txtLetter = "";
            // string txtProject = "";
            // string txtDesig = "";

            //string date1 = "";

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date1 = System.DateTime.Now.ToString("MMMM dd, yyyy");
            DataTable dt = (DataTable)Session["tblcusbill01"];
            DataTable dt3 = (DataTable)Session["tblCutomerInfo"];

            string tempadd = dt3.Rows[0]["custadd"].ToString();
            if (!tempadd.Equals(""))
            {
                string[] add1 = tempadd.Split(',');
                txtAddress = add1[0] + " , " + add1[1];
                txtAddress2 = add1[2] + " , " + add1[3];
            }

            double Schamt = (double)ViewState["Schamt"];
            double rcvamt = (double)ViewState["rcvamt"];
            double payamt = (double)ViewState["balamt"];

            string txtRecv = rcvamt.ToString("#,##0;(#,##0); ");
            string txtNetPay = payamt.ToString("#,##0;(#,##0); ");

            string txtTkInword = "In Word : " + ASTUtility.Trans(Math.Round(payamt), 2);
            string txtRef = "Ref : " + comnam + "/" + "CRM" + "/" + dt3.Rows[0]["actdesc"].ToString();
            string txtName = dt3.Rows[0]["custname"].ToString();
            string txtProject = dt3.Rows[0]["actdesc"].ToString();
            string txtSubject = this.txtSubject.Text.ToString();
            string txtLetter = this.txtLetter.Text.ToString();

            string txtNB = "Note : The above bill prepared based on total work order value " + Schamt.ToString("#,##0;(#,##0); ") + "/-Tk.";

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerBillInfo>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCustomerBillInfo", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("date1", date1));
            Rpt1.SetParameters(new ReportParameter("txtTkInword", txtTkInword));
            Rpt1.SetParameters(new ReportParameter("txtref", txtRef));
            Rpt1.SetParameters(new ReportParameter("txtName", txtName));
            Rpt1.SetParameters(new ReportParameter("txtProject", txtProject));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtAddress2", txtAddress2));
            Rpt1.SetParameters(new ReportParameter("txtSubject", txtSubject));
            Rpt1.SetParameters(new ReportParameter("txtLetter", txtLetter));
            Rpt1.SetParameters(new ReportParameter("txtNB", txtNB));
            Rpt1.SetParameters(new ReportParameter("txtRecv", txtRecv));
            Rpt1.SetParameters(new ReportParameter("txtNetPay", txtNetPay));
            Rpt1.SetParameters(new ReportParameter("txtSignatory", txtSignatory));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblcusbill01"];
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            dt.Rows[RowIndex].Delete();
            DataView dv = dt.DefaultView;
            Session.Remove("tblcusbill01");
            Session["tblcusbill01"] = dv.ToTable();
            this.bind_CustBildata();

            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Deleted";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
    }
}