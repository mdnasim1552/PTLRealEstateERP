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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAdvancedAgainstLoan : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Advance Againts Loan";

                var dtoday = System.DateTime.Today;
                this.txttodate.Text = dtoday.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = new System.DateTime(dtoday.Year, dtoday.Month, 1).ToString("dd-MMM-yyyy");
                this.DepartName();
            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
           // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }


        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void ibtnFindSupply_OnClick(object sender, EventArgs e)
        {
            this.DepartName();
        }


        private void DepartName()
        {
            string comcod = this.GetComeCode();
            string SrchSupplier = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETDEPARTEMTNAME", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlDeptName.DataTextField = "deptname";
            this.ddlDeptName.DataValueField = "deptcode";
            this.ddlDeptName.DataSource = ds1.Tables[0];
            this.ddlDeptName.DataBind();
            ViewState["tbldept"] = ds1.Tables[0];
        }



    protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            try
            {
                Session.Remove("tblsupinfo");
                string comcod = this.GetComeCode();
                string frmdate = txtfrmdate.Text.ToString();
                string todate = txttodate.Text.ToString();
                string deptcode = this.ddlDeptName.SelectedValue.ToString() == "000000000000" ? "94%" : this.ddlDeptName.SelectedValue.ToString() + "%";
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTADVANCEDLOAN", frmdate, todate, deptcode, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvadvLoan.DataSource = null;
                    this.gvadvLoan.DataBind();
                    return;
                }
               // Session["tblsupinfo"] = ds1.Tables[0];
                Session["tbladvloan"] = HiddenSameData(ds1.Tables[0]);
                this.DataBindGrid();
            }
            catch (Exception ex)
            {

            }

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();
            string deptcode = dt1.Rows[0]["spcfcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode && dt1.Rows[j]["spcfcode"].ToString() == deptcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    deptcode = dt1.Rows[j]["spcfcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["deptname"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["spcfcode"].ToString() == deptcode)
                    {
                        dt1.Rows[j]["deptname"] = "";
                    }
                    if (dt1.Rows[j]["spcfcode"].ToString() == deptcode)
                    {
                        dt1.Rows[j]["deptname"] = "";
                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    deptcode = dt1.Rows[j]["spcfcode"].ToString();
                }
            }
            return dt1;

        }

        private void DataBindGrid()
        {
            // this.MultiView1.ActiveViewIndex = 0;
            try
            {
                this.gvadvLoan.DataSource = (DataTable)Session["tbladvloan"];
                this.gvadvLoan.DataBind();
            }
            catch (Exception ex)
            {

            }
            //Session["Report1"] = gvsupstatus;
            //((HyperLink)this.gvsupstatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
    }




}