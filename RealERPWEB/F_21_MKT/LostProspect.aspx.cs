using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class LostProspect : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "CRM Lost Prospect";

                GetAllSubdata();

                DateTime nowDate = DateTime.Now;
                DateTime yearfday = new DateTime(nowDate.Year, 1, 1);
                DateTime ylDay = new DateTime(nowDate.Year, 12, 31);


                string fdate = yearfday.ToString("dd-MMM-yyyy");
                string edate = ylDay.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = fdate;
                this.txttodate.Text = edate;

                GETEMPLOYEEUNDERSUPERVISED();
                ModalDataBind();
                this.ddlEmpid_SelectedIndexChanged(null, null);

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


        }


        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            ViewState["tblsubddl"] = ds2.Tables[0];
            ViewState["tblstatus"] = ds2.Tables[1];
            ViewState["tblproject"] = ds2.Tables[2];
            ViewState["tblcompany"] = ds2.Tables[3];
            ds2.Dispose();
        }
        private void GETEMPLOYEEUNDERSUPERVISED()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblempsup"] = ds1.Tables[0];
            ds1.Dispose();


        }
        private void ModalDataBind()
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataTable dtemp = (DataTable)ViewState["tblempsup"];
            DataView dv;
            dv = dt1.Copy().DefaultView;
            string ddlempid = this.ddlEmpid.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userrole = hst["userrole"].ToString();
            string lempid = hst["empid"].ToString();
            string comcod = this.GetComeCode();
            DataTable dtE = new DataTable();
            dv.RowFilter = ("gcod like '93%'");
            if (userrole == "1")
            {
                dtE = dv.ToTable();
                dtE.Rows.Add("000000000000", "All Employee", "");
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
                    dtE.Rows.Add("000000000000", "All Employee", "");
            }

            this.ddlEmpid.DataTextField = "gdesc";
            this.ddlEmpid.DataValueField = "gcod";
            this.ddlEmpid.DataSource = dtE;
            this.ddlEmpid.DataBind();
            this.ddlEmpid.SelectedValue = lempid;

            this.ddlEmpNameTo.DataTextField = "gdesc";
            this.ddlEmpNameTo.DataValueField = "gcod";
            this.ddlEmpNameTo.DataSource = dtE;
            this.ddlEmpNameTo.DataBind();
            this.ddlEmpNameTo.SelectedValue= "000000000000";


        }

        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            ViewState.Remove("tblproswork");

            string comcod = this.GetComeCode();
            string empId = this.ddlEmpid.SelectedValue.ToString();
            DataSet ds1 = instcrm.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "LOST_PROSPECT", null, null, null, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblproswork"] = (ds1.Tables[0]);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblproswork"];
            this.gvProspectWorking.DataSource = dt;
            this.gvProspectWorking.DataBind();

            if (gvProspectWorking.Rows.Count > 0)
            {
                Session["Report1"] = gvProspectWorking;
                ((HyperLink)this.gvProspectWorking.HeaderRow.FindControl("hlnkbtnProsWorking")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }


        protected void gvProspectWorking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProspectWorking.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvProspectWorking.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)ViewState["tblproswork"];

            string assocname = this.ddlEmpNameTo.SelectedValue == "000000000000"?" All" : dt.Rows[0]["assocname"].ToString();

            var lst = dt.DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.RptProspectTransfer>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_MKT.RptProspectTransfer", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Lost Prospect of " + assocname));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string toemp = this.ddlEmpNameTo.SelectedValue=="000000000000"?"%" : this.ddlEmpNameTo.SelectedValue.ToString();

            DataSet ds1 = instcrm.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "LOST_PROSPECT_UNDER_ASSOCIATE", null, null, null, toemp, frmdate, todate, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblproswork"] = (ds1.Tables[0]);
            this.Data_Bind();
        }

        protected void SrchBtn_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string toemp = this.ddlEmpNameTo.SelectedValue == "000000000000" ? "%" : this.ddlEmpNameTo.SelectedValue.ToString();
            string srcval ="%" + txtVal.Text + "%";
            DataSet ds1 = instcrm.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "LOST_PROSPECT_SEARCH", null, null, null, toemp, srcval, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblproswork"] = (ds1.Tables[0]);
            this.Data_Bind();
        }
    }
}