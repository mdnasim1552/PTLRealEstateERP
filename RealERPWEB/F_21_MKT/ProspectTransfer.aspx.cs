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
    public partial class ProspectTransfer : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "CRM Prospect Transfer";           

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");




                GetAllSubdata();

                GETEMPLOYEEUNDERSUPERVISED();
                ModalDataBind();


            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];


            return (hst["comcod"].ToString());
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
            

        }

        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();

            string empId = this.ddlEmpid.SelectedValue.ToString();

            DataSet ds1 = instcrm.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "PROSPECT_LIST", null, null, null,empId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
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
    }
}