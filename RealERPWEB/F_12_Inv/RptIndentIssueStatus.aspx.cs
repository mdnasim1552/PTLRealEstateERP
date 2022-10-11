using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using RealERPLIB;
using RealERPLIB;
using System.Data.OleDb;
using System.Data;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_12_Inv
{
    public partial class RptIndentIssueStatus : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Indent Issue Status";
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetStoreName();
                this.GetDeparment();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
           
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

           

        }
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblindissuestatus"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.EClassMaterial.IndentStatus>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptIndentIssueStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Indent Issue Status"));
          
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetStoreName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "INDENTISSUESTORE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblStoreType"] = ds2.Tables[0];
            this.ddlStoreName.DataTextField = "actdesc1";
            this.ddlStoreName.DataValueField = "actcode";
            this.ddlStoreName.DataSource = ds2.Tables[0];
            this.ddlStoreName.DataBind();


        }
        protected void GetDeparment()
        {
            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "INDENTISSUEDEPARTMENT", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ds1.Tables[0].Rows.Add(comcod, "000000000000", "--- All Department--");
          


            this.ddldpt.DataTextField = "inddeptgdesc";
            this.ddldpt.DataValueField = "inddeptgcod";
            this.ddldpt.DataSource = ds1.Tables[0];
            this.ddldpt.DataBind();
            this.ddldpt.SelectedValue = "000000000000";
            
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
           
            string comcod = this.GetComeCode();
            string prjcode = this.ddlStoreName.SelectedValue.ToString() == "000000000000" ? "11%" : this.ddlStoreName.SelectedValue.ToString() + "%";
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string department = this.ddldpt.SelectedValue.ToString() == "000000000000" ? "94%" : this.ddldpt.SelectedValue.ToString() + "%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "RPTMATINDENTISSUESTATUS", prjcode, frmdate, todate, department, "", "", "", "");
            if (ds1 == null)
            {
                this.gvIssuest.DataSource = null;
                this.gvIssuest.DataBind();

                return;
            }
            Session["tblindissuestatus"] = this.HiddenSameData(ds1.Tables[0]);

            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string deptno = dt1.Rows[0]["deptno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptno"].ToString() == deptno)
                    dt1.Rows[j]["deptdesc"] = "";
                deptno = dt1.Rows[j]["deptno"].ToString();

            }

            return dt1;
        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblindissuestatus"];
            this.gvIssuest.DataSource = (DataTable)Session["tblindissuestatus"];
            this.gvIssuest.DataBind();
            this.FooterCalculation();


        }
       
        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblindissuestatus"];

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvIssuest.FooterRow.FindControl("lgvtotalissueqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(issueqty)", "")) ? 0.00 : dt.Compute("sum(issueqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            }

            else
            {
                return;
            }

        }

    }
}