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
using RealEntity.C_17_Acc;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_23_CR
{
    public partial class RptInvoice02 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)


        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                this.GetProjectName();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Invoice";


                //this.txtSubject.Visible = false;
                //this.Label1.Visible = false;

                //this.txthead.Visible = false;
                //this.Label12.Visible =false;

                //this.Label3.Visible = false;
            }




        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string projName = this.ddlProjectName.SelectedValue.ToString();

            string custName = this.ddlCustName.SelectedValue.ToString();
            string subject = this.txtSubject.Text.ToString();
            string lbl1 = this.Label1.Text.ToString();
            string head = this.txthead.Text.ToString();

            DataTable dt = (DataTable)Session["tblCustPayment"];

            DataTable dt2 = (DataTable)Session["tblCustinfo"];
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();

            DataTable dt5 = (DataTable)Session["tbluamt"];
            string custname = dt2.Rows[0]["custname"].ToString();
            string projectname = dt2.Rows[0]["pname"].ToString();
            string unit = dt2.Rows[0]["sirdesc"].ToString();
            string location = dt2.Rows[0]["location"].ToString();

            double uamt = Convert.ToDouble(dt5.Rows[0]["uamt"].ToString());

            DataView dv = dt.DefaultView;
            dv.RowFilter = "gcod <> 'AAAAAAA' and  gcod <> 'BBBBBBB' and  gcod <> 'CCCCCCC'";
            dt3 = dv.ToTable();

            DataRow[] dr = dt.Select("gcod ='AAAAAAA'");

            double total = Convert.ToDouble(dr[0]["amount"].ToString());


            DataRow[] dr2 = dt.Select("gcod ='BBBBBBB'");

            double received = Convert.ToDouble(dr2[0]["amount"].ToString());

            DataRow[] dr3 = dt.Select("gcod ='CCCCCCC'");

            double payable = Convert.ToDouble(dr3[0]["amount"].ToString());



            string amtwrd = "In Word: " + ASTUtility.Trans(Math.Round(payable), 2);


            var lst = dt3.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.InvoiceP2P360>();

            LocalReport Rpt2 = new LocalReport();

            Rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptInvoicep2p360", lst, null, null);
            Rpt2.EnableExternalImages = true;
            Rpt2.SetParameters(new ReportParameter("comnam", comnam));
            Rpt2.SetParameters(new ReportParameter("comadd", comadd));
            Rpt2.SetParameters(new ReportParameter("date", Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt2.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Rpt2.SetParameters(new ReportParameter("proj", projectname));
            Rpt2.SetParameters(new ReportParameter("cust", custname));
            Rpt2.SetParameters(new ReportParameter("unit", unit));
            Rpt2.SetParameters(new ReportParameter("location", location));
            Rpt2.SetParameters(new ReportParameter("lbl1", lbl1));
            Rpt2.SetParameters(new ReportParameter("head", head));

            Rpt2.SetParameters(new ReportParameter("subject", "Subject: " + subject));
            Rpt2.SetParameters(new ReportParameter("refnum", "Ref : "));
            Rpt2.SetParameters(new ReportParameter("RptTitle", "Invoice"));
            Rpt2.SetParameters(new ReportParameter("InWord", amtwrd));
            Rpt2.SetParameters(new ReportParameter("total", total.ToString("#,##0.00;(#,##0.00); ")));
            Rpt2.SetParameters(new ReportParameter("received", received.ToString("#,##0.00;(#,##0.00); ")));
            Rpt2.SetParameters(new ReportParameter("payable", payable.ToString("#,##0.00;(#,##0.00); ")));
            Rpt2.SetParameters(new ReportParameter("uamt", "Note: The above bill prepared based on the Total Work Order Value " + uamt.ToString("#,##0.00;(#,##0.00); ") + "/-Tk."));




            Rpt2.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();

        }

        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }

        private void GetCustomerName()
        {
            ViewState.Remove("tblcustomer");

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();
            ViewState["tblcustomer"] = ds2.Tables[0];
            ds2.Dispose();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            //DataTable dt = new DataTable();
            //Session["CustSubDesc"] = dt;

            this.ShowCustPayment();

        }


        private void ShowCustPayment()

        {


            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");


            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETINVOICE", pactcode, custid, "", "", "", "", "", "", "");
            //DataSet ds2= purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, Date, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvCustInvoice.DataSource = null;
                this.gvCustInvoice.DataBind();
                return;
            }

            Session["tblCustPayment"] = ds2.Tables[0];     //this.HiddenSameDate2(ds2.Tables[0]);     

            Session["tblCustinfo"] = ds2.Tables[1];
            Session["tbluamt"] = ds2.Tables[2];
            Session["CustSubDesc"] = ds2.Tables[3];


            this.txtSubject.Visible = true;
            this.Label1.Visible = true;



            this.txthead.Visible = true;
            this.Label12.Visible = true;
            this.Label3.Visible = true;

            DataTable dt = (DataTable)Session["CustSubDesc"];
            if (dt.Rows.Count > 0)
            {

                this.txtSubject.Text = dt.Rows[0]["subdesc"].ToString();
                this.txthead.Text = dt.Rows[0]["descrip"].ToString();

            }
            else
            {
                this.txtSubject.Text = "Running Bill for Interior decoration Works.";
                this.txthead.Text = "It is to Inform you that, as per our offer/agreement we have provided the followinf seviecs for interior decoration work at Shakil Masud.";
            }




            this.Data_Bind();



        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblCustPayment"];

            this.gvCustInvoice.DataSource = dt;
            this.gvCustInvoice.DataBind();

        }


        protected void gvCustInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgcResDesc2 = (Label)e.Row.FindControl("lgcResDesc2");
                Label lgvschamt = (Label)e.Row.FindControl("lgvschamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA" || ASTUtility.Right(code, 4) == "BBBB" || ASTUtility.Right(code, 4) == "CCCC")
                {

                    lgcResDesc2.Font.Bold = true;
                    lgvschamt.Font.Bold = true;

                    lgcResDesc2.Style.Add("text-align", "right");
                }

            }
        }
        protected void lbtnUpdateInvoice_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string prjcode = ddlProjectName.SelectedValue.ToString();
            string custcode = ddlCustName.SelectedValue.ToString();
            string subject = this.txtSubject.Text.Trim();
            string txthead = this.txthead.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT03", "INSERTORUPDATEINVOICE", prjcode, custcode, subject, txthead, "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);



        }
    }
}