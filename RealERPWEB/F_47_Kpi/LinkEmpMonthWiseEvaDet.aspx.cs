using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_47_Kpi
{

    public partial class LinkEmpMonthWiseEvaDet : System.Web.UI.Page
    {
        private UserManagerKPI objUser = new UserManagerKPI();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Evaluation";


                string date = this.Request.QueryString["date"].ToString();
                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.ShowView();

            }
        }

        private void ShowView()
        {

            string dept = this.Request.QueryString["dept"].ToString();

            switch (dept)
            {
                case "Sales":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Collection":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "Legal":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "Others":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;







            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }



        private void GetEmpList()
        {

            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            //string userid = (this.Request.QueryString["Type"] == "IndEmp") ? hst["usrid"].ToString() : "";
            string type = this.Request.QueryString["Type"];

            string userid = (type == "Sales" || type == "CR" || type == "General" || type == "IndEmp" || type == "Legal") ? hst["usrid"].ToString() : "";


            string deptcode = hst["deptcode"].ToString();
            string monid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM");


            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst3 = objUser.GetEmpCode2(srchEmp, userid, deptcode, monid);




            //List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            //lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid,deptcode);

            this.ddlEmpid.DataTextField = "empname";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();
            this.ddlEmpid.SelectedValue = this.Request.QueryString["empid"].ToString();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string dept = this.Request.QueryString["dept"].ToString();
            switch (dept)
            {
                case "Sales":
                    this.EmpDetailsSalse();
                    break;

                case "Collection":
                    this.EmpDetailsCollect();
                    break;
                case "Legal":
                    this.EmpDetailsLegal();
                    break;
                case "Others":
                    this.EmpDetailsOthers();
                    break;

                default:

                    break;




            }




        }

        private void EmpDetailsSalse()
        {

            Session.Remove("tblData");
            this.lblHeaderSales.Visible = true;
            this.lblHeaderColl.Visible = true;
            this.lblHeaderOffer.Visible = true;


            string comcod = this.Getcomcod();
            string empid = this.ddlEmpid.SelectedValue.ToString();
            string frdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "RPTMONWISEEVADETAILS", empid, frdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            Session["tblData"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private void EmpDetailsCollect()
        {
            Session.Remove("tblData");
            string comcod = this.Getcomcod();
            string empid = this.ddlEmpid.SelectedValue.ToString();
            string frdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "RPTMONWISEEVADETAILSOTH", empid, frdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            Session["tblData"] = ds1.Tables[0];
            this.Data_Bind();


        }
        private void EmpDetailsLegal()
        {

            Session.Remove("tblData");
            string comcod = this.Getcomcod();
            string empid = this.ddlEmpid.SelectedValue.ToString();
            string frdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "RPTMONWISEEVADETAILSLEG", empid, frdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            Session["tblData"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void EmpDetailsOthers()
        {

            Session.Remove("tblData");
            string comcod = this.Getcomcod();
            string empid = this.ddlEmpid.SelectedValue.ToString();
            string frdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "RPTMONWISEEVADETAILSOTH", empid, frdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            Session["tblData"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string dept = this.Request.QueryString["dept"].ToString();
            switch (dept)
            {
                case "Sales":

                    break;

                case "Collection":

                    break;

                case "Legal":
                    DateTime cdate = Convert.ToDateTime(dt1.Rows[0]["cdate"]);
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (Convert.ToDateTime(dt1.Rows[j]["cdate"]) == cdate)
                            dt1.Rows[j]["cdate1"] = "";
                        cdate = Convert.ToDateTime(dt1.Rows[j]["cdate"]);
                    }

                    break;


                case "Others":
                    DateTime exdate = Convert.ToDateTime(dt1.Rows[0]["exdate"]);
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (Convert.ToDateTime(dt1.Rows[j]["exdate"]) == exdate)
                            dt1.Rows[j]["exdate1"] = "";
                        exdate = Convert.ToDateTime(dt1.Rows[j]["exdate"]);
                    }


                    break;

                default:

                    break;






            }
            return dt1;






        }
        private void Data_Bind()
        {

            DataTable dt = ((DataTable)Session["tblData"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();

            string dept = this.Request.QueryString["dept"].ToString();
            switch (dept)
            {
                case "Sales":
                    //A. Sales
                    dvr = dt.DefaultView;
                    dvr.RowFilter = ("grp = '810100601001'");
                    dt1 = dvr.ToTable();
                    this.gvSales.DataSource = dt1;
                    this.gvSales.DataBind();
                    this.FooterCalculation(dt1, "gvSales");

                    //B. Collection Summary
                    dvr = dt.DefaultView;
                    dvr.RowFilter = ("grp = '810100601002'");
                    dt1 = dvr.ToTable();
                    this.gvCollection.DataSource = dt1;
                    this.gvCollection.DataBind();
                    this.FooterCalculation(dt1, "gvCollection");
                    //C. Cheque In Hand

                    dvr = dt.DefaultView;
                    dvr.RowFilter = ("grp not like  '81%'");
                    dt1 = dvr.ToTable();
                    this.gvOffer.DataSource = dt1;
                    this.gvOffer.DataBind();



                    //dvr = dt.DefaultView;
                    //dvr.RowFilter = ("grp = '9601008'");
                    //dt1 = dvr.ToTable();
                    //this.gvVisit.DataSource = dt1;
                    //this.gvVisit.DataBind();


                    //dvr = dt.DefaultView;
                    //dvr.RowFilter = ("grp = '9601001'");
                    //dt1 = dvr.ToTable();
                    //this.gvcall.DataSource = dt1;
                    //this.gvcall.DataBind();

                    //dvr = dt.DefaultView;
                    //dvr.RowFilter = ("grp = 'F'");
                    //dt1 = dvr.ToTable();
                    //this.gvothers.DataSource = dt1;
                    //this.gvothers.DataBind();
                    //this.FooterCalculation(dt1, "gvothers");   

                    break;

                case "Collection":

                    break;


                case "Legal":
                    this.gvdetailslg.DataSource = dt;
                    this.gvdetailslg.DataBind();
                    break;

                case "Others":
                    this.gvdetailsg.DataSource = dt;
                    this.gvdetailsg.DataBind();
                    break;


                default:

                    break;




            }










        }


        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;



            //DataTable dt = (DataTable)Session["tblData"];




            switch (GvName)
            {
                case "gvSales":
                    ((Label)this.gvSales.FooterRow.FindControl("lblgvFSaleamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "gvCollection":
                    ((Label)this.gvCollection.FooterRow.FindControl("lblgvFMramtcoll")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");


                    break;


                    //case "gvothers":
                    //    ((Label)this.gvothers.FooterRow.FindControl("lblgvFoth")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                    //            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");


                    break;










            }


        }

        //[System.Web.Services.WebMethod(EnableSession=true)]

        //public  static List<RealEntity.C_47_Kpi.EClassEmpCode> GetEmpList02(string srchteam) 
        //{

        //    List<RealEntity.C_47_Kpi.EClassEmpCode> lst = new List<RealEntity.C_47_Kpi.EClassEmpCode>();

        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = Getcomcod();
        //    //string userid = (Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
        //    string userid = "";

        //    lst = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchteam, userid);
        //    return lst;


        //}


        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetEmpList();
        }


        protected void gvResMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void lnkprint_Click(object sender, EventArgs e)
        {
            string dept = this.Request.QueryString["dept"].ToString();
            switch (dept)
            {
                case "Sales":
                    this.PrintEmpDetailsSalse();
                    break;

                case "Collection":
                    this.PrintEmpDetailsCollect();
                    break;
                case "Legal":
                    this.PrintEmpDetailsLegal();
                    break;

                case "Others":
                    this.PrintEmpDetailsOthers();
                    break;


                default:

                    break;




            }






        }

        private void PrintEmpDetailsSalse()
        {
            string empid = this.ddlEmpid.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblData"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptAppMonitor = new RealERPRPT.R_47_Kpi.RptMonEmpEvaDetails();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            CompName.Text = comnam;

            TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Salary:       , Benifits:     ";


            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";



            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintEmpDetailsCollect()
        {

        }

        private void PrintEmpDetailsLegal()
        {
            string empid = this.ddlEmpid.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblData"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");

            ReportDocument rptAppMonitor = new RealERPRPT.R_47_Kpi.RptMonEvaDetaisLeg();
            TextObject txtComName = rptAppMonitor.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtComName.Text = comnam;

            TextObject txtmonwise = rptAppMonitor.ReportDefinition.ReportObjects["txtmonwise"] as TextObject;
            txtmonwise.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg;


            TextObject txtDepartment = rptAppMonitor.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            txtDepartment.Text = "Department: " + hst["deptname"].ToString();


            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            TextObject txtDate = rptAppMonitor.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";



            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintEmpDetailsOthers()
        {
            string empid = this.ddlEmpid.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblData"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");

            ReportDocument rptAppMonitor = new RealERPRPT.R_47_Kpi.RptMonEvaDetaisOthers();
            TextObject txtComName = rptAppMonitor.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtComName.Text = comnam;

            TextObject txtmonwise = rptAppMonitor.ReportDefinition.ReportObjects["txtmonwise"] as TextObject;
            txtmonwise.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg;


            TextObject txtDepartment = rptAppMonitor.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            txtDepartment.Text = "Department: " + hst["deptname"].ToString();


            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            TextObject txtDate = rptAppMonitor.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";



            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}
