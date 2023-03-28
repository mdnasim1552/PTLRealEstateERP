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
    public partial class RptSalesEvaulation : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                Response.Redirect("~/AcceessError.aspx");

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            DateTime nowDate = DateTime.Now;
            DateTime yearfday = new DateTime(nowDate.Year, nowDate.Month, 1);
            int days = DateTime.DaysInMonth(nowDate.Year, nowDate.Month);
            DateTime ylDay = new DateTime(nowDate.Year, nowDate.Month, days);

            string fdate = yearfday.ToString("dd-MMM-yyyy");
            string edate = ylDay.ToString("dd-MMM-yyyy");
            this.txtfromdate.Text = fdate;
            this.txttodate.Text = edate;
            this.GetEmployeeList();
        }

        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        public void GetEmployeeList()
        {
            string comcod = GetComeCode();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "EMPLOYEELIST", "", "", "", "", "", "");

            ddlEmp.DataTextField = "sirdesc";
            ddlEmp.DataValueField = "sircode";
            this.ddlEmp.DataSource = ds1.Tables[0];
            this.ddlEmp.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string empid = this.ddlEmp.SelectedValue.ToString();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string monthid = this.txtfromdate.Text;
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {

        }
    
    }
}