using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class CRMDashboard03 : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //DateTime curdate = System.DateTime.Today;
                //this.txtfrmdate.Text = Convert.ToDateTime("01-Jan-" + curdate.ToString("yyyy")).ToString("dd-MMM-yyyy");
                //this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");

                //this.GetAllSubdata();
                //this.GETEMPLOYEEUNDERSUPERVISED();
                //this.ModalDataBind();
                //this.GetComponentData();
                GetDashboardInformation();
            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];


            return (hst["comcod"].ToString());
        }
        private void GetDashboardInformation()
        {

            string ddlempid = "000000000000";// this.ddlEmpid.SelectedValue.ToString();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            string datetype = DdlDateType.SelectedValue.ToString();
            string fromdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            switch (datetype)
            {
                case "1"://yesterday
                     fromdate = System.DateTime.Today.AddDays(-1).ToString("dd-MMM-yyyy");
                     todate = System.DateTime.Today.AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
                case "2":// Last Seven day
                    fromdate = System.DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
                    todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;
                case "3": // this Month
                    fromdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    fromdate = "01" + fromdate.Substring(2);
                    todate = Convert.ToDateTime(fromdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    break;
                case "4": // Last month
                    fromdate = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    fromdate = "01" + fromdate.Substring(2);
                    todate = Convert.ToDateTime(fromdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    break;
                case "5": // This Year
                    int year = DateTime.Now.Year;
                    DateTime firstDay = new DateTime(year, 1, 1);
                    fromdate = firstDay.ToString("dd-MMM-yyyy");
                 
                    break;
                case "6": // Last Year
                    int year1 = DateTime.Now.AddYears(-1).Year;
                    DateTime firstDaylastyear = new DateTime(year1, 1, 1);
                    DateTime lastDaylastyear = new DateTime(year1, 12, 31);
                    fromdate = firstDaylastyear.ToString("dd-MMM-yyyy");
                    todate = lastDaylastyear.ToString("dd-MMM-yyyy");
                    break;
                case "7": // Custom                  
                    fromdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                    todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    break;
            }
            string condate = todate;
            string Empid = "";
            if (userrole != "1")
            {
                Empid = hst["empid"].ToString();
            }
            //Empid =((ddlempid == "000000000000") ? "" : ddlempid)+"%";
            ddlempid = (ddlempid == "000000000000" ? "93" : ddlempid) + "%";

            DataSet ds3 = instcrm.GetTransInfo(comcod, "SP_REPORT_CRM_DASHBOARD", "GET_CRM_DASHBAORD_INFO", "8301%", Empid, fromdate, todate, condate, ddlempid);
         
            Session["tblNotification"] = ds3;
            BindWidgetData();
           
        }

        private void BindWidgetData()
        {
            DataSet ds3 = (DataSet)Session["tblNotification"];
            if (ds3 == null)
            {
                return;
            }

            this.Widget_Query.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["query"]).ToString("#,##0;(#,##0);");
            this.Widget_Lead.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["lead"]).ToString("#,##0;(#,##0);");
            this.Widget_QLead.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["qualiflead"]).ToString("#,##0;(#,##0);");
            this.Widget_Nego.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["nego"]).ToString("#,##0;(#,##0);");
            this.Widget_Sold.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["win"]).ToString("#,##0;(#,##0);");
            this.Widget_close.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["lclose"]).ToString("#,##0;(#,##0);");
            this.Widget_Hold.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["hold"]).ToString("#,##0;(#,##0);");
            this.Widget_lost.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["lost"]).ToString("#,##0;(#,##0);");

            //string empId = this.ddlEmpid.SelectedValue.ToString();
            //string curDate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            //// this.hyplnkOccasion.NavigateUrl="~/Notification/Occasion?EmpId=" + empId +"&curDate="+curDate;
            //hlink2.NavigateUrl = "~/F_12_Inv/PurMRREntry?Type=Entry&prjcode=" + pactcode + "&genno=" + orderno + "&sircode=" + sircode;

        }

        protected void LbtnOk_Click(object sender, EventArgs e)
        {
            this.GetDashboardInformation();
        }
    }
}