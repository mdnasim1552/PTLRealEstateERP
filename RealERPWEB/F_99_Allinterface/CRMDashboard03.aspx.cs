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

                this.GetAllSubdata();
                this.GETEMPLOYEEUNDERSUPERVISED();
                //this.ModalDataBind();
                //this.GetComponentData();
                GetDashboardInformation();
                GetToDoListInformation();
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
            string filter = comcod == "3374" ? "namdesgsec" : "";
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", filter, "", "", "", "", "", "", "", "");
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
            ViewState["tblempsup"] = ds1.Tables[0];

            ds1.Dispose();

            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataTable dtemp = (DataTable)ViewState["tblempsup"];
            DataView dv;
            dv = dt1.Copy().DefaultView;
            string ddlempid = this.ddlEmpid.SelectedValue.ToString();
           
            string userrole = hst["userrole"].ToString();
            string lempid = hst["empid"].ToString();
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
                // if(dtE.Rows.Count>1)
                //dtE.Rows.Add("000000000000", "Choose Employee..", "");
            }

            this.ddlEmpid.DataTextField = "gdesc";
            this.ddlEmpid.DataValueField = "gcod";
            this.ddlEmpid.DataSource = dtE;
            this.ddlEmpid.DataBind();
            if (dtE.Rows.Count >= 2)
                this.ddlEmpid.SelectedValue = "000000000000";


            DataView dv5;
            dv5 = dt1.DefaultView;
            dv5.RowFilter = ("gcod like '31%' and code like '2901001%'");
            DataTable dt = dv5.ToTable();
            dt.Rows.Add("0000000", "--All--", "");
            DdlSubSource.DataTextField = "gdesc";
            DdlSubSource.DataValueField = "gcod";
            DdlSubSource.DataSource = dt;
            DdlSubSource.DataBind();
            DdlSubSource.SelectedValue = "0000000";

            DataView dv6;
            dv6 = dt1.DefaultView;
            dv6.RowFilter = ("gcod like '31%' and code like '2901002%'");
            DataTable dt2 = dv6.ToTable();
            dt2.Rows.Add("0000000", "--All--", "");
            DdlSalesSubsource.DataTextField = "gdesc";
            DdlSalesSubsource.DataValueField = "gcod";
            DdlSalesSubsource.DataSource = dt2;
            DdlSalesSubsource.DataBind();
            DdlSalesSubsource.SelectedValue = "0000000";
        }


        


private void GetDashboardInformation()
        {

            string ddlempid = this.ddlEmpid.SelectedValue.ToString();


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
           
            string mktsource = (this.DdlSubSource.SelectedValue.ToString() == "0000000" ? "%" : this.DdlSubSource.SelectedValue.ToString()) + "%";
            
            string salesource = (this.DdlSalesSubsource.SelectedValue.ToString() == "0000000" ? "%" : this.DdlSalesSubsource.SelectedValue.ToString()) + "%";
         
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_REPORT_CRM_DASHBOARD", "GET_CRM_DASHBAORD_INFO", "8301%", Empid, fromdate, todate, condate, ddlempid,mktsource, salesource);
         
            Session["tblNotification"] = ds1;
            BindWidgetData();

       
        }

        private void GetToDoListInformation()
        {

            string ddlempid = this.ddlEmpid.SelectedValue.ToString();


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

            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_REPORT_CRM_DASHBOARD", "GET_TODOLOIST_NUMBER", "8301%", Empid, ddlempid, fromdate, todate);
            Session["tbltodolist"] = ds2;
            BindToListData();
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

            string mktsourhtml = "";
            foreach (DataRow dr in ds3.Tables[2].Rows)
            {
                mktsourhtml += "<div class=\"row align-items-center\">"+
                                                            "<div class=\"col-4 py-0\">"+
                                                                "<div class=\"progress-label\">"+ dr["leadst"].ToString() + "</div>"+
                                                            "</div>"+
                                                            "<div class=\"col-8 py-0\">"+
                                                                "<div class=\"d-flex align-items-center py-2\">"+
                                                                    "<div class=\"flex-grow-1\">"+
                                                                        "<div class=\"progress "+ dr["cssclass"].ToString() + "\">"+
                                                                            "<div class=\"progress-bar\"  style=\"width: " + dr["percnt"].ToString() + "%\">" +
                                                                                "<p class=\"progress-percent\">"+ dr["percnt"].ToString() + "%</p>"+
                                                                            "</div>"+
                                                                        "</div>"+
                                                                    "</div>"+
                                                                    "<div class=\"progress-end\">"+ dr["total"].ToString() + "</div>"+
                                                                "</div>"+
                                                            "</div>"+
                                                        "</div>";
            }
         this.MktSourceGraph.InnerHtml = mktsourhtml;
        this.SearcingMktKey.InnerText = this.DdlSubSource.SelectedItem.ToString()+"("+ ds3.Tables[2].Rows[0]["ttlsts"].ToString() + ")";


            string salessourhtml = "";
            foreach (DataRow dr in ds3.Tables[3].Rows)
            {
                salessourhtml += "<div class=\"row align-items-center\">" +
                                                            "<div class=\"col-4 py-0\">" +
                                                                "<div class=\"progress-label\">" + dr["leadst"].ToString() + "</div>" +
                                                            "</div>" +
                                                            "<div class=\"col-8 py-0\">" +
                                                                "<div class=\"d-flex align-items-center py-2\">" +
                                                                    "<div class=\"flex-grow-1\">" +
                                                                        "<div class=\"progress " + dr["cssclass"].ToString() + "\">" +
                                                                            "<div class=\"progress-bar\"  style=\"width: " + dr["percnt"].ToString() + "%\">" +
                                                                                "<p class=\"progress-percent\">" + dr["percnt"].ToString() + "%</p>" +
                                                                            "</div>" +
                                                                        "</div>" +
                                                                    "</div>" +
                                                                    "<div class=\"progress-end\">" + dr["total"].ToString() + "</div>" +
                                                                "</div>" +
                                                            "</div>" +
                                                        "</div>";
            }
            this.SalesSourceGraph.InnerHtml = salessourhtml;
            this.SearcingSalKey.InnerText = this.DdlSalesSubsource.SelectedItem.ToString() + "(" + ds3.Tables[3].Rows[0]["ttlsts"].ToString() + ")";

            Bind_Project_Details();
        }
        private void Bind_Project_Details()
        {
            DataSet ds3 = (DataSet)Session["tblNotification"];
            this.GvPrjsum.DataSource = ds3.Tables[1];
            this.GvPrjsum.DataBind();
        }
         
        private void BindToListData()
        {
            DataSet ds3 = (DataSet)Session["tbltodolist"];
            if (ds3 == null)
            {
                return;
            }

            this.TodoScheduleWOrk.InnerText = ds3.Tables[0].Rows[0]["dws"].ToString();
            this.TodoTodayTask.InnerText = ds3.Tables[0].Rows[0]["tdt"].ToString();
            this.TodoDailyWorkReport.InnerText = ds3.Tables[0].Rows[0]["dwr"].ToString();
            this.TodoMissedCall.InnerText = ds3.Tables[0].Rows[0]["call"].ToString();
            this.TodoMissedVisit.InnerText = ds3.Tables[0].Rows[0]["visit"].ToString();
            this.TodoTodayVisit.InnerText = ds3.Tables[0].Rows[0]["todayvisit"].ToString();
            this.TodoTodayWorkLog.InnerText = ds3.Tables[0].Rows[0]["ttlactvty"].ToString();
            this.TodoTodayCall.InnerText = ds3.Tables[0].Rows[0]["todaycall"].ToString();
            this.TodoTodayMeeting.InnerText = ds3.Tables[0].Rows[0]["todaymeting"].ToString();
            //this.lblcsigned.InnerText = ds3.Tables[0].Rows[0]["signed"].ToString();

            this.TodoMissedMeetExt.InnerText = ds3.Tables[0].Rows[0]["pme"].ToString();
            this.TodoMissedMeetInt.InnerText = ds3.Tables[0].Rows[0]["pmi"].ToString();
            //this.lblDatablank.InnerText = ds3.Tables[0].Rows[0]["databank"].ToString();
            this.TodoTotalMissedFlowup.InnerText = ds3.Tables[0].Rows[0]["misdflowup"].ToString();

            
        }
        protected void LbtnOk_Click(object sender, EventArgs e)
        {
            this.GetDashboardInformation();
        }

        protected void GvPrjsum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GvPrjsum.PageIndex = e.NewPageIndex;
            this.Bind_Project_Details();
        }

        protected void LbtnSourceSum_Click(object sender, EventArgs e)
        {
            GetDashboardInformation();
        }
    }
}