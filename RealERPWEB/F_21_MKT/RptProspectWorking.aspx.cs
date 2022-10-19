using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class RptProspectWorking : System.Web.UI.Page
    {
        ProcessAccess accessData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                string txtDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFrmDate.Text = "01-"+Convert.ToDateTime(txtDate).ToString("MMM-yyyy");
                this.txtToDate.Text = Convert.ToDateTime(this.txtFrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetAllSubdata();
                this.GETEMPLOYEEUNDERSUPERVISED();

                this.BindDDLLead();
                this.lnkbtnOk_Click(null, null);
            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GETEMPLOYEEUNDERSUPERVISED()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = accessData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
            ViewState["tblempsup"] = ds1.Tables[0];
            ds1.Dispose();


        }

        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = accessData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
            if (ds2==null)
                return;
            ViewState["tblsubddl"] = ds2.Tables[0];
            ViewState["tblstatus"] = ds2.Tables[1];
            ViewState["tblproject"] = ds2.Tables[2];
            ds2.Dispose();
        }
        private void BindDDLLead()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string lempid = hst["empid"].ToString();
            string comcod = this.GetComeCode();
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataTable dtemp = (DataTable)ViewState["tblempsup"];

            DataView dv;
            dv = dt1.Copy().DefaultView;
            string ddlempid = this.ddlEmpid.SelectedValue.ToString();
            //string empid = (userrole == "1" ? "93" : lempid) + "%";

            DataTable dtE = new DataTable();
            dv.RowFilter = ("gcod like '93%'");
            if (userrole == "1")
            {

                dtE = dv.ToTable();
                dtE.Rows.Add("000000000000", "Choose Employee..", "");

            }

            else
            {
                DataTable dts = dv.ToTable();
                var query = (from dtl1 in dts.AsEnumerable()
                             join dtl2 in dtemp.AsEnumerable() on dtl1.Field<string>("gcod") equals dtl2.Field<string>("empid")
                             select new
                             {
                                 gcod = dtl1.Field<string>("gcod"),
                                 gdesc = dtl1.Field<string>("gdesc"),
                                 code = dtl1.Field<string>("code")
                             }).ToList();
                dtE = ASITUtility03.ListToDataTable(query);
                if (dtE.Rows.Count >= 2)
                    dtE.Rows.Add("000000000000", "Choose Employee..", "");
            }

            this.ddlEmpid.DataTextField = "gdesc";
            this.ddlEmpid.DataValueField = "gcod";
            this.ddlEmpid.DataSource = dtE;
            this.ddlEmpid.DataBind();
            this.ddlEmpid.SelectedValue = "000000000000";

        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string queryType = this.Request.QueryString["Type"] ?? "";
            switch (queryType)
            {
                case "RptDayWise":
                    this.ShowRptProsDayWorking();
                    break;

                default:
                    this.ShowRptProsWorking();
                    break;
            }
        }
        private void ShowRptProsDayWorking()
        {
            string comcod = this.GetComeCode();
            string txtFrmDate = Convert.ToDateTime(this.txtFrmDate.Text).ToString("dd-MMM-yyyy");
            string txtToDate = Convert.ToDateTime(this.txtToDate.Text).ToString("dd-MMM-yyyy");
            string empId = this.ddlEmpid.SelectedValue.ToString();
            DataSet ds1 = accessData.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "PROSPECT_WORKING_REPORT_DAY_WISE", null, null, null, txtFrmDate, txtToDate, empId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);
                return;
            }

            ViewState["tblproswork"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void ShowRptProsWorking()
        {
            string comcod = this.GetComeCode();
            string txtFrmDate = Convert.ToDateTime(this.txtFrmDate.Text).ToString("dd-MMM-yyyy");
            string txtToDate = Convert.ToDateTime(this.txtToDate.Text).ToString("dd-MMM-yyyy");
            string empId = this.ddlEmpid.SelectedValue.ToString();
            DataSet ds1 = accessData.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "PROSPECT_WORKING_REPORT", null, null, null, txtFrmDate, txtToDate, empId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('No Data Found');", true);
                return;
            }

            ViewState["tblproswork"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    dt1.Rows[j]["grpdesc"] = "";
                    grp = dt1.Rows[j]["grp"].ToString();
                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                }

            }

            return dt1;
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblproswork"];
            this.gvProspectWorking.DataSource=dt;
            this.gvProspectWorking.DataBind();

            if (gvProspectWorking.Rows.Count > 0)
            {
                Session["Report1"] = gvProspectWorking;
                ((HyperLink)this.gvProspectWorking.HeaderRow.FindControl("hlnkbtnProsWorking")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvProspectWorking.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvProspectWorking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProspectWorking.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)ViewState["tblproswork"];
            string txtDate = "( From "+ Convert.ToDateTime(this.txtFrmDate.Text).ToString("dd-MMM-yyyy") + " To " +Convert.ToDateTime(this.txtToDate.Text).ToString("dd-MMM-yyyy") + " )";

            LocalReport Rpt1 = new LocalReport();            
            var list = dt.DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.RptProspectWorking>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_MKT.RptProspectWorking", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "WORKING REPORT"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate, session)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
    }
}