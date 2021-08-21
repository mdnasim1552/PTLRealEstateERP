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
namespace RealERPWEB.F_62_Mis
{

    public partial class RptDeptWiseEmpAcitviteis : System.Web.UI.Page
    {

        private UserManagerKPI objUser = new UserManagerKPI();
        UserManagerMIS objuserMan = new UserManagerMIS();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Evaluation";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetDepartment();

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }


        protected void lnkok_Click(object sender, EventArgs e)
        {

            Session.Remove("tblData");
            string comcod = this.Getcomcod();
            string deptcode = this.ddldepartment.SelectedValue.ToString();
            string frdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI02", "RPTDEPTWISEEVADETAILSOTH", deptcode, frdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            Session["tblData"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }


        private void GetDepartment()
        {

            List<RealEntity.C_39_MyPage.EClassMIS.EClassDepartment> lst = objuserMan.GetDepartment();

            if (lst.Count == 0)
            {
                this.ddldepartment.DataSource = null;
                this.ddldepartment.DataBind();
                return;
            }

            this.ddldepartment.DataTextField = "deptname";
            this.ddldepartment.DataValueField = "deptcode";
            this.ddldepartment.DataSource = lst;
            this.ddldepartment.DataBind();

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string empid = dt1.Rows[0]["empid"].ToString();
            //DateTime exdate = Convert.ToDateTime(dt1.Rows[0]["exdate"]);
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["empid"].ToString() == empid)
                {
                    dt1.Rows[j]["empname"] = "";

                }

                //else {

                //    if (dt1.Rows[j]["empid"].ToString() == empid)
                //        dt1.Rows[j]["empname"] = "";

                //    if (Convert.ToDateTime(dt1.Rows[j]["exdate"]) == exdate)
                //        dt1.Rows[j]["exdate1"] = "";




                //}
                empid = dt1.Rows[j]["empid"].ToString();
                // exdate = Convert.ToDateTime(dt1.Rows[j]["exdate"]);
            }

            return dt1;



        }


        private void Data_Bind()
        {

            DataTable dt = ((DataTable)Session["tblData"]).Copy();
            this.gvdetails.DataSource = dt;
            this.gvdetails.DataBind();
        }






        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }





        protected void lnkprint_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)Session["tblData"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");

            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.RptDeptWiseEvaDetails();
            TextObject txtComName = rptAppMonitor.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtComName.Text = comnam;

            TextObject txtDepartment = rptAppMonitor.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            txtDepartment.Text = this.ddldepartment.SelectedItem.Text.Trim(); ;


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
            //string empid = this.ddlEmpid.SelectedValue.ToString();
            //DataTable dt = (DataTable)Session["tblData"];
            //List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            //List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");

            //ReportDocument rptAppMonitor = new  RealERPRPT.R_21_Kpi.RptMonEvaDetaisOthers();
            //TextObject txtComName = rptAppMonitor.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //txtComName.Text = comnam;

            //TextObject txtmonwise = rptAppMonitor.ReportDefinition.ReportObjects["txtmonwise"] as TextObject;
            //txtmonwise.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Salary:       , Benifits:     ";


            ////TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            ////txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            //TextObject txtDate = rptAppMonitor.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";



            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void imgsrchdepartment_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
    }
}
