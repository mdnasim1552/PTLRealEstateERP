using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_39_MyPage
{

    public partial class RptMIS02 : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        UserManagerMIS objuserMan = new UserManagerMIS();
        public UserManagerKPI objUser = new UserManagerKPI();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "EvaonProBasis") ? "EVALUATION ON PROGRAM"
                    : this.Request.QueryString["Type"].ToString().Trim() == "EmpEvaluation" ? "EMPLOYEE EVALUATION"
                    : this.Request.QueryString["Type"].ToString().Trim() == "EmpHistory" ? "INDIVIDUAL HISTORY" : "MO";

                this.ShowView();


            }
        }

        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }




        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            switch (type)
            {
                case "EvaonProBasis":
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "EmpEvaluation":
                    date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(6).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "EmpHistory":
                    date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(6).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "IndEmpHistory":
                    date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(6).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;
                    this.GetIniEmpList();
                    break;






            }





        }

        private void GetIniEmpList()
        {

            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string userid = (this.Request.QueryString["Type"] == "IndEmp") ? hst["usrid"].ToString() : "";
            string deptcode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid, deptcode);
            Session["tblemployee"] = lst3;
        }


        private void GetEmpList()
        {

            List<RealEntity.C_47_Kpi.EClassEmpCode> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode>)Session["tblemployee"];
            string srchEmp = this.txtSrchSalesTeam.Text.Trim();
            if (srchEmp.Length > 0)
            {

                IEnumerable<RealEntity.C_47_Kpi.EClassEmpCode> ProjectQuery = (from employee in lst
                                                                               where employee.empname1.ToUpper().Contains(srchEmp.ToUpper())
                                                                               orderby employee.empid ascending
                                                                               select employee);

                this.ddlEmpid.DataTextField = "empname";
                this.ddlEmpid.DataValueField = "empid";
                this.ddlEmpid.DataSource = ProjectQuery;
                this.ddlEmpid.DataBind();

            }

            else
            {

                this.ddlEmpid.DataTextField = "empname";
                this.ddlEmpid.DataValueField = "empid";
                this.ddlEmpid.DataSource = lst;
                this.ddlEmpid.DataBind();
            }

        }




        protected void lbntOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "EvaonProBasis":
                    this.ShowEvaProWise();
                    break;

                case "EmpEvaluation":
                    this.ShowEmpEvaluation();
                    break;

                case "EmpHistory":
                    this.ShowEmpHistory();
                    break;
                case "IndEmpHistory":
                    this.ShowIndEmpHistory();
                    break;



            }

        }

        private void ShowEvaProWise()
        {

            Session.Remove("tblmis");
            string date = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");



            List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram> lst = objuserMan.GetEvaonProgram(date);


            if (lst == null)
            {
                this.gvevaoproject.DataSource = null;
                this.gvevaoproject.DataBind();
                return;

            }
            Session["tblmis"] = lst;
            this.Data_Bind();



        }
        private void ShowEmpEvaluation()
        {

            Session.Remove("tblmis");
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = hst["deptcode"].ToString();



            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva> lst = objuserMan.GetEmployeeEva(frmdate, todate, deptcode);


            if (lst == null)
            {
                this.gvevaoproject.DataSource = null;
                this.gvevaoproject.DataBind();
                return;

            }
            Session["tblmis"] = lst;
            this.Data_Bind();

        }
        private void ShowEmpHistory()
        {

            Session.Remove("tblmis");
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userid = (this.Request.QueryString["History"] == "Individual") ? hst["usrid"].ToString() : "";

            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02> lst = objuserMan.GetEmployyHistory02(frmdate, todate, userid);


            if (lst == null)
            {
                this.gvevaoproject.DataSource = null;
                this.gvevaoproject.DataBind();
                return;

            }
            Session["tblmis"] = lst;
            this.HiddenSameData();
            this.Data_Bind();



        }

        private void ShowIndEmpHistory()
        {

            Session.Remove("tblmis");
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string empid = this.ddlEmpid.SelectedValue.ToString();
            //Hashtable hst = (Hashtable)Session["tblLogin"];

            //string userid = (this.Request.QueryString["History"] == "Individual") ? hst["usrid"].ToString() : "";

            List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar> lst = objuserMan.GetEmployyHistoryMar(frmdate, todate, empid);


            if (lst == null)
            {
                this.gvindemphis.DataSource = null;
                this.gvindemphis.DataBind();
                return;

            }
            Session["tblmis"] = lst;
            this.HiddenSameData();
            this.Data_Bind();

        }
        private void HiddenSameData()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            int i = 0;
            switch (type)
            {
                case "EvaonProBasis":

                    break;

                case "EmpEvaluation":

                    break;


                case "EmpHistory":
                    List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02> lst = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02>)Session["tblmis"];
                    if (lst.Count == 0)
                        return;


                    string empid = lst[0].empid;
                    string actcode = lst[0].actcode;
                    //  List<ACCMISEntity.C_22_Sal.EClassComProposal.EClassSalProInfo> lst2 = new List<ACCMISEntity.C_22_Sal.EClassComProposal.EClassSalProInfo>();
                    foreach (RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02 lst1 in lst)
                    {



                        if (i == 0)
                        {
                            i++;
                            continue;
                        }
                        else if (lst1.empid == empid && lst1.actcode == actcode)
                        {
                            lst[i].empname = "";
                            lst[i].actdesc = "";


                        }
                        else
                        {

                            if (lst1.empid == empid)
                                lst[i].empname = "";
                            if (lst1.actcode == actcode)
                                lst[i].actdesc = "";

                        }


                        empid = lst1.empid;
                        actcode = lst1.actcode;
                        i++;



                    }
                    Session["tblmis"] = lst;
                    break;

                case "IndEmpHistory":

                    List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar> lstiemp = (List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar>)Session["tblmis"];
                    if (lstiemp.Count == 0)
                        return;
                    actcode = lstiemp[0].actcode;
                    foreach (RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar lst1 in lstiemp)
                    {



                        if (i == 0)
                        {
                            i++;
                            continue;
                        }
                        else if (lst1.actcode == actcode)
                        {

                            lstiemp[i].actdesc = "";


                        }



                        actcode = lst1.actcode;
                        i++;



                    }
                    Session["tblmis"] = lstiemp;
                    break;



                case "NextApp":

                    break;

                case "OffPerformance":

                    break;

                case "SalePerformance":
                    // teamcode = dt1.Rows[0]["teamcode"].ToString();
                    //string proscod = dt1.Rows[0]["proscod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["teamcode"].ToString() == teamcode && dt1.Rows[j]["proscod"].ToString() == proscod)
                    //    {
                    //        teamcode = dt1.Rows[j]["teamcode"].ToString();
                    //        proscod = dt1.Rows[j]["proscod"].ToString();
                    //        dt1.Rows[j]["teamdesc"] = "";
                    //        dt1.Rows[j]["prosdesc"] = "";
                    //        dt1.Rows[j]["saldate"] = "";


                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["teamcode"].ToString() == teamcode)
                    //        {
                    //            dt1.Rows[j]["teamdesc"] = "";
                    //        }

                    //        if (dt1.Rows[j]["proscod"].ToString() == proscod)
                    //        {
                    //            dt1.Rows[j]["prosdesc"] = "";
                    //            dt1.Rows[j]["saldate"] = "";

                    //        }
                    //        teamcode = dt1.Rows[j]["teamcode"].ToString();
                    //        proscod = dt1.Rows[j]["proscod"].ToString();
                    //    }



                    //}

                    break;

                case "ClientLetter":
                case "SendOnlineLetter":

                    break;
                case "ProsClient":

                    break;

                case "AllOffPerformance":


                    break;


            }






        }


        private void Data_Bind()
        {

            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();


                switch (type)
                {
                    case "EvaonProBasis":
                        List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram> lst = (List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram>)Session["tblmis"];
                        this.gvevaoproject.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvevaoproject.DataSource = lst;
                        this.gvevaoproject.DataBind();
                        break;



                    case "EmpEvaluation":
                        List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva> lsteva = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva>)Session["tblmis"];
                        this.gvEmpEval.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpEval.DataSource = lsteva;
                        this.gvEmpEval.DataBind();
                        break;


                    case "EmpHistory":
                        List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02> lsthis = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02>)Session["tblmis"];
                        this.gvemphis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvemphis.DataSource = lsthis;
                        this.gvemphis.DataBind();
                        break;

                    case "IndEmpHistory":
                        List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar> lstinemphis = (List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar>)Session["tblmis"];

                        this.gvindemphis.DataSource = lstinemphis;
                        this.gvindemphis.DataBind();
                        break;


                }
            }
            catch (Exception ex) { }


        }

        private void FooterCalculation()
        {
            //DataTable dt = (DataTable)Session["tbltoapp"];
            //if (dt.Rows.Count == 0)
            //    return;
            //((Label)this.gvSal.FooterRow.FindControl("lgvFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uamt)", "")) ? 0.00 : dt.Compute("sum(uamt)", ""))).ToString("#,##0;(#,##0); ");



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EvaonProBasis":
                    this.PrintEvaonProBasis();
                    break;

                case "EmpEvaluation":
                    this.PrintEmpEvaluation();
                    break;

                case "EmpHistory":
                    this.PrintEmpHistory();
                    break;


                case "IndEmpHistory":
                    PrintMonthWiseEvalDet();
                    break;



            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }



        }



        private void PrintTodaysDisANextApp()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Salesteamcode=this.ddlSalesTeam.SelectedValue.ToString();
            //ReportDocument rptAppMonitor = new  RealERPRPT.R_21_Mkt.RptTodaysDisAndNextApp();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //string Date = (this.Request.QueryString["Type"].ToString().Trim() == "Todaysdis") ? "Date: "+this.txtfrmdate.Text.Trim() : " (From "+this.txtfrmdate.Text.Trim()+" To "+this.txttodate.Text.Trim()+")";
            //TextObject rpttxtHeaderTitle = rptAppMonitor.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = this.lblHeadertitle.Text;


            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = "Executive Name: " + this.ddlSalesTeam.SelectedItem.Text;

            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = Date;


            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource((DataTable)Session["tbltoapp"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintEvaonProBasis()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram> lst = (List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram>)Session["tblmis"];
            ReportDocument rptEvaonProBasis = new RealERPRPT.R_39_MyPage.rptEvaonProgram();
            TextObject txtComName = rptEvaonProBasis.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtComName.Text = comnam;
            TextObject date_range = rptEvaonProBasis.ReportDefinition.ReportObjects["date_range"] as TextObject;
            date_range.Text = "Date: " + txtfrmdate.Text.ToString();

            TextObject txtuserinfo = rptEvaonProBasis.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed From Computer Name:" + compname + ",User:" + username + ",Dated:" + printdate;
            rptEvaonProBasis.SetDataSource(lst);
            Session["Report1"] = rptEvaonProBasis;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintEmpEvaluation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptcode = hst["deptcode"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //Grade
            DataSet ds = MktData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTGRADE", "", "", "", "", "");
            if (ds == null) { return; }
            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva> lst = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva>)Session["tblmis"];
            ReportDocument rptAppMonitor = new RealERPRPT.R_39_MyPage.rptMonthWiseAllEmpEva02();
            TextObject txtCompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = comnam;
            TextObject date = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            date.Text = "(From " + this.txtfrmdate.Text.ToString() + " To " + this.txttodate.Text.ToString() + ")";
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(ds.Tables[0]);
            rptAppMonitor.SetDataSource(lst);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptCleintHis()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Salesteamcode = this.ddlSalesTeam.SelectedValue.ToString();
            //ReportDocument rptAppMonitor = new  RealERPRPT.R_21_Mkt.RptClientHistory();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = "Executive Name: " + this.ddlSalesTeam.SelectedItem.Text;

            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];
            //TextObject txtclientname = rptAppMonitor.ReportDefinition.ReportObjects["txtclientname"] as TextObject;
            //txtclientname.Text = "Client Name: " + this.ddlClientList.SelectedItem.Text;  
            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource( (DataTable)Session["tbltoapp"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintEmpHistory()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02> lst = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02>)Session["tblmis"];
            ReportDocument rptAppMonitor = new RealERPRPT.R_39_MyPage.rptAllEmpHistory02();
            TextObject txtCompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = comnam;
            TextObject date = rptAppMonitor.ReportDefinition.ReportObjects["date"] as TextObject;
            date.Text = "(From " + this.txtfrmdate.Text.ToString() + " To " + this.txttodate.Text.ToString() + ")";
            rptAppMonitor.SetDataSource(lst);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintMonthWiseEvalDet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpid.SelectedValue.ToString();

            List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar> lstinemphis = (List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar>)Session["tblmis"];
            List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp1 = lstemp.FindAll((p => p.empid == empid));
            ReportDocument rptIndEmpHistory = new RealERPRPT.R_39_MyPage.rptMonthWiseEvaDetMar();

            TextObject txtComName = rptIndEmpHistory.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtComName.Text = comnam;
            TextObject date_range = rptIndEmpHistory.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            date_range.Text = "(From " + txtfrmdate.Text.ToString() + " To " + this.txttodate.Text + ")";

            TextObject txtempname = rptIndEmpHistory.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desig + ", Salary:       , Benifits:     ";
            TextObject txtuserinfo = rptIndEmpHistory.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed From Computer Name:" + compname + ",User:" + username + ",Dated:" + printdate;
            rptIndEmpHistory.SetDataSource(lstinemphis);
            Session["Report1"] = rptIndEmpHistory;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }




        protected void gvevaoproject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvevaoproject.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvevaoproject_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;
                cell03.Font.Bold = true;


                TableCell cell04 = new TableCell();
                cell04.Text = "DATE";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;
                cell04.Font.Bold = true;

                TableCell cell05 = new TableCell();
                cell05.Text = "PERCENTAGE";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;
                cell05.Font.Bold = true;





                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);

                gvevaoproject.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void btnEmp_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string YmonID = this.ddlyearmon.SelectedValue.ToString();
            //string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();


            //DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "SHOWDAYWISEDATA", Empid, YmonID);

            //ViewState["tbModalDataNext"] = ds1.Tables[0];

            //this.lblName.Text = "Name: " + ds1.Tables[1].Rows[0]["gdatat"].ToString();
            //this.lblDesg.Text = "Designation: " + ds1.Tables[1].Rows[2]["gdatat"].ToString();
            //this.lblJoin.Text = "Date of Joining: " + ds1.Tables[1].Rows[1]["gdatad"].ToString();


            //this.Modal_2_Bind();
            //string radalertscript = "<script language='javascript'>function f(){loadModal4(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void btnGpa_Click(object sender, EventArgs e)
        {


            //string comcod = this.Getcomcod();
            //string YmonID = this.ddlyearmon.SelectedValue.ToString();
            //string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            //DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "SHOWINDKPISTATUS", Empid, YmonID);

            //ViewState["tbModalDataInd"] = HiddenSameData(ds1.Tables[0]);
            //this.Modal_Data_Bind();
            //string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void btnGraph_Click(object sender, EventArgs e)
        {

            //string comcod = this.Getcomcod();
            //string YmonID = this.ddlyearmon.SelectedValue.ToString();
            //string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            //DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "SHOWEMPPERGRAPH", Empid, YmonID);

            //ViewState["tbModalGraph"] = ds1.Tables[0];
            //this.showChart();
            //string radalertscript = "<script language='javascript'>function f(){loadModal5(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void gvEmpEval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpEval.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvEmpEval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkempname = (HyperLink)e.Row.FindControl("hlnkempname");
                Label Index = (Label)e.Row.FindControl("lbltpar");
                LinkButton Gpa = (LinkButton)e.Row.FindControl("btnGpa");
                double Value = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "tper"));
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string empname = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empname")).ToString();
                //string yearmon = this.ddlyearmon.SelectedValue.ToString();
                // DateTime date = Convert.ToDateTime(yearmon.Substring(4, 2) + "-01-" + yearmon.Substring(0, 4));
                string fromdate = this.txtfrmdate.Text;
                string todate = this.txttodate.Text;


                if (Value >= 100)
                {
                    Index.Style.Add("color", "Green");
                    Gpa.Style.Add("color", "Green");
                }
                else if (Value < 100 && Value >= 80)
                {
                    Index.Style.Add("color", "Navy");
                    Gpa.Style.Add("color", "Navy");
                }
                else if (Value < 80 && Value >= 60)
                {
                    Index.Style.Add("color", "Purple");
                    Gpa.Style.Add("color", "Purple");
                }
                else if (Value < 60 && Value >= 50)
                {
                    Index.Style.Add("color", "Silver");
                    Gpa.Style.Add("color", "Silver");
                }
                else if (Value < 50 && Value >= 40)
                {
                    Index.Style.Add("color", "Lime");
                    Gpa.Style.Add("color", "Lime");
                }
                else if (Value < 40 && Value >= 30)
                {
                    Index.Style.Add("color", "Olive");
                    Gpa.Style.Add("color", "Olive");
                }
                else
                {
                    Index.Style.Add("color", "Red");
                    Gpa.Style.Add("color", "Red");
                }

                hlnkempname.NavigateUrl = "~/F_05_MyPage/RptEmpMonthWiseEva03.aspx?Type=Mgt";
            }




        }
        protected void gvemphis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void gvevaoproject_DataBound(object sender, EventArgs e)
        {



        }
        protected void gvevaoproject_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvprojectname = (HyperLink)e.Row.FindControl("hlnkgvprojectname");
                string prono = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prono")).ToString();
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                hlnkgvprojectname.Style.Add("color", "blue");
                //graph.Style.Add("color", "blue");
                hlnkgvprojectname.NavigateUrl = "~/F_05_MyPage/LinkMonMarTarget.aspx?prono=" + prono + "&refno=" + refno;




            }




        }
        protected void gvindemphis_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;


                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;

                TableCell cell04 = new TableCell();
                cell04.Text = "TARGET";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;
                cell04.Font.Bold = true;




                TableCell cell05 = new TableCell();
                cell05.Text = "ACTUAL";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;
                cell05.Font.Bold = true;




                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;
                cell06.Font.Bold = true;


                TableCell cell07 = new TableCell();
                cell07.Text = "";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 1;
                cell07.Font.Bold = true;


                TableCell cell08 = new TableCell();
                cell08.Text = "";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 1;
                cell08.Font.Bold = true;





                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);


                gvindemphis.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetEmpList();
        }
        protected void gvindemphis_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label deloadv = (Label)e.Row.FindControl("lblgvdelayiemphis");

                string deloadvsign = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deloadvsign")).ToString();
                if (deloadvsign == "delay")
                {
                    deloadv.Style.Add("color", "red");
                }

            }
        }

    }
}
