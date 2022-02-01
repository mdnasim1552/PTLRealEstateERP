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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Prospect Working Report";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetAllSubdata();
                this.BindDDLLead();
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
            string comcod = this.GetComeCode();
            string txtDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string empId = this.ddlEmpid.SelectedValue.ToString();
            DataSet ds1 = accessData.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "PROSPECT_WORKING_REPORT", null, null, null, txtDate, empId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (ds1==null)
                return;

            ViewState["tblproswork"] = ds1.Tables[0];
            this.Data_Bind();
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
            this.Data_Bind();
        }

        protected void gvProspectWorking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProspectWorking.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}