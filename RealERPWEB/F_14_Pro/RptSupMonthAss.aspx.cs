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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptSupMonthAss : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/F_81_Hrm/../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string date = System.DateTime.Today.AddMonths(-2).ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = "01" + date.Substring(2);
                this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(3).AddDays(-1).ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "SUPPLIER MONTHLY  ASSESSMENT REPORT";

                this.SupplierList();
            }
        }

        private void SupplierList()
        {
            string comcod = this.GetComeCode();
            string SrchSupplier = "%" + this.txtSupPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETASSSUPPLIER", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlSuplist.DataTextField = "resdesc";
            this.ddlSuplist.DataValueField = "rescode";
            this.ddlSuplist.DataSource = ds1.Tables[0];
            this.ddlSuplist.DataBind();
            ViewState["tblSup"] = ds1.Tables[0];
            ds1.Dispose();
          
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ShowSupPerformance()
        {
            Session.Remove("tblper");
            string comcod = this.GetComeCode();
            // string comcode = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string supplier = (this.ddlSuplist.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSuplist.SelectedValue.ToString() + "%";

            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy"); // this.txtfrmDate.Text.Trim();
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");//this.txttoDate.Text.Trim();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "SUPASSPOSITION", supplier, fromdate, todate, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvSupAssper.DataSource = null;
                this.gvSupAssper.DataBind();
                return;
            }
            Session["tblper"] = ds2.Tables[1];

            this.Data_Bind();
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblper"];
            this.gvSupAssper.DataSource = dt;
            this.gvSupAssper.DataBind();
        }

        protected void lnkbtnShow_OnClick(object sender, EventArgs e)
        {
            this.ShowSupPerformance();
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string Company = (this.ddlCompanyName.SelectedItem.Text.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedItem.Text.ToString().Substring(0, 2) + "%";
            //string deptname = this.ddlDepartment.SelectedItem.Text.ToString().Substring(13);  //== "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy"); // this.txtfrmDate.Text.Trim();
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy"); //this.txttoDate.Text.Trim();
            string FTdate = "( From " + fromdate + " To " + todate + " )";
            string title = "Supplier Monthly Assessment";


            DataTable dt = (DataTable)Session["tblper"];
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptSupMonthAss>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSupMonthAss", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("ftdate", FTdate));


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ibtnFindSupply_Click(object sender, EventArgs e)
        {
            this.SupplierList();
        }
    }
}