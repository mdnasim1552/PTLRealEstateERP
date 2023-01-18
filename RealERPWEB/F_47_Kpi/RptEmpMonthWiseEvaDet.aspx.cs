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
//using  RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_47_Kpi
{

    public partial class RptEmpMonthWiseEvaDet : System.Web.UI.Page
    {
        private UserManagerKPI objUser = new UserManagerKPI();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Evaluation";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdate.Text = Convert.ToDateTime("01-Jan-" + date.Substring(7)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lbluseid.Text = (Request.QueryString["Type"] == "IndEmp") ? hst["usrid"].ToString() : "";
                this.GetIniEmpList();
                this.GetEmpList();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }
        private void GetIniEmpList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string type = this.Request.QueryString["Type"];
            string userid = (type == "Sales" || type == "CR" || type == "General") ? hst["usrid"].ToString() : "";

            string deptcode = "9402%";

            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string Date = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM");
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst3 = objUser.GetEmpCode2(srchEmp, userid, deptcode, Date);
            Session["tblemployee"] = lst3;
            this.GetEmpList();


        }
        private void GetEmpList()
        {


            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            string srchEmp = this.txtSrchSalesTeam.Text.Trim();
            if (srchEmp.Length > 0)
            {

                IEnumerable<RealEntity.C_47_Kpi.EClassEmpCode2> ProjectQuery = (from employee in lst
                                                                                where employee.empname1.ToUpper().Contains(srchEmp.ToUpper())
                                                                                orderby employee.empid ascending
                                                                                select employee);
                this.ddlEmpid.DataTextField = "empname";
                this.ddlEmpid.DataValueField = "empid";
                this.ddlEmpid.DataSource = ProjectQuery.ToList();
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

        protected void lbtnOk_Click(object sender, EventArgs e)
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
        private void Data_Bind()
        {

            DataTable dt = ((DataTable)Session["tblData"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();





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
            //dvr.RowFilter = ("grp = 'F'");
            //dt1 = dvr.ToTable();
            //this.gvothers.DataSource = dt1;
            //this.gvothers.DataBind();
            //this.FooterCalculation(dt1, "gvothers");   







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
            string empid = this.ddlEmpid.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblData"];
            List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp1 = lstemp.FindAll((p => p.empid == empid));

            //lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid);

            //this.ddlEmpid.DataTextField = "empname";
            //this.ddlEmpid.DataValueField = "empid";
            //this.ddlEmpid.DataSource = lst3;
            //this.ddlEmpid.DataBind();
            //Session["tblemployee"] = lst3;




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
            txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desig + ", Salary:       , Benifits:     ";


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


    }
}