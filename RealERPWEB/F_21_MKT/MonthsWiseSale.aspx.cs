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
    public partial class MonthsWiseSale : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Sales Report ";

                DateTime nowDate = DateTime.Now;
                DateTime yearfday = new DateTime(nowDate.Year, 1, 1);
                DateTime ylDay = new DateTime(nowDate.Year, 12, 31);


                string fdate = yearfday.ToString("dd-MMM-yyyy");
                string edate = ylDay.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = fdate;
                this.txttodate.Text = edate;

                GetAllSubdata();
                this.lbtnOk_Click(null, null);
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
            DataTable dt = ds2.Tables[0];
            DataView dv;
            dv = dt.DefaultView;

            dv.RowFilter = ("gcod like '95%'");
            this.ddlleadstatus.DataTextField = "gdesc";
            this.ddlleadstatus.DataValueField = "gcod";
            this.ddlleadstatus.DataSource = dv.ToTable();
            this.ddlleadstatus.DataBind();
            this.ddlleadstatus.SelectedValue = "9501001";
            this.ddlleadstatus.Enabled = false;

            ds2.Dispose();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string comcod = GetComeCode();
            string status = this.ddlleadstatus.SelectedValue;
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETCRMMONTHLYSALES", fdate, tdate, status, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvMonhtlySales.DataSource = null;
                this.gvMonhtlySales.DataBind();
                return;
            }

            this.gvMonhtlySales.DataSource = ds2.Tables[0];
            this.gvMonhtlySales.DataBind();

            ViewState["tblSales"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            this.gvMonhtlySales.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.gvMonhtlySales.DataSource = (DataTable)ViewState["tblSales"];
            this.gvMonhtlySales.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {
            DataTable ddt = (DataTable)ViewState["tblSales"];
            if (ddt.Rows.Count == 0)
                return;
           
            ((Label)this.gvMonhtlySales.FooterRow.FindControl("lgvFtqty")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(tqty)", "")) ?
                             0 : ddt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvMonhtlySales;
            ((HyperLink)this.gvMonhtlySales.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvMonhtlySales_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMonhtlySales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}