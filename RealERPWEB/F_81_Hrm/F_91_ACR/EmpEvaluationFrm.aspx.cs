using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_91_ACR
{
    [SerializableAttribute]
    public partial class EmpEvaluationFrm : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetLastPerNumber();
                this.GetEmployeeName();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetEmployeeName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETEMPNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            ds3.Dispose();

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetPerNumber()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mPerNo = "NEWPER";
            if (this.ddlPreList.Items.Count > 0)
                mPerNo = this.ddlPreList.SelectedValue.ToString();

            string mProDAT = this.txtCurDate.Text.Trim();
            if (mPerNo == "NEWPER")
            {
                DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETLASTEVANO", mProDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxevano1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxevano1"].ToString().Substring(6, 5);
                    this.ddlPreList.DataTextField = "maxevano1";
                    this.ddlPreList.DataValueField = "maxevano";
                    this.ddlPreList.DataSource = ds2.Tables[0];
                    this.ddlPreList.DataBind();
                }
            }
        }

        private void GetLastPerNumber()
        {

            string comcod = this.GetComeCode();
            string date = this.txtCurDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETLASTEVANO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxevano1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxevano1"].ToString().Substring(6);
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetEmployeeName();
        }
        protected void ibtnPreList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETPREEVANO", curdate, "", "", "", "", "", "", "", "");

            this.ddlPreList.DataTextField = "evano1";
            this.ddlPreList.DataValueField = "evano";
            this.ddlPreList.DataSource = ds2.Tables[0];
            this.ddlPreList.DataBind();
            ds2.Dispose();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.lblEmpname.Text = this.ddlEmpName.SelectedItem.Text.Trim().Substring(13);
                this.ddlEmpName.Enabled = false;
                //this.lblEmpname.Visible = true;
                this.ddlEmpName.Enabled = false;
                this.lblprelist.Enabled = false;
                this.txtrefno.Enabled = false;
                //this.txtPreViousList.Visible = false;
                //this.ibtnPreList.Visible = false;
                this.ddlPreList.Enabled = false;
                this.ShowPerformance();
                return;
            }
            this.lbtnOk.Text = "Ok";

            this.ddlPreList.Items.Clear();
            this.gvPerAppraisal.DataSource = null;
            this.gvPerAppraisal.DataBind();
            this.ddlEmpName.Visible = true;
            //this.lblEmpname.Visible = false;
            this.ddlEmpName.Enabled = true;
            this.lblprelist.Visible = true;
            //this.txtPreViousList.Visible = true;
            //this.ibtnPreList.Visible = true;
            this.ddlPreList.Visible = true;
            this.txtCurDate.Enabled = true;
        }

        private void ShowPerformance()
        {
            Session.Remove("tblper");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mPerNo = "NEWPER";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mPerNo = this.ddlPreList.SelectedValue.ToString();
            }


            // DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETPERINFO", mPerNo, CurDate1,
            //           "", "", "", "", "", "", "");



            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETEVAINFO", mPerNo, CurDate1,
                          "", "", "", "", "", "", "");



            if (ds1 == null)
                return;

            Session["tblper"] = this.hiddenSameData(ds1.Tables[0]);


            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["evano1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["evano1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["evadate"]).ToString("dd-MMM-yyyy");
                this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
                this.ddlEmpName.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
                //this.lblEmpname.Text = this.ddlEmpName.SelectedItem.Text.Trim();

            }

            this.Data_DataBind();


        }


        private DataTable hiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            int j;
            string mgdesc;

            string mgcod = dt1.Rows[0]["mgcod"].ToString();

            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mgcod"].ToString() == mgcod)
                {

                    dt1.Rows[j]["mgdesc"] = "";

                }
                mgcod = dt1.Rows[j]["mgcod"].ToString();


            }
            return dt1;
        }
        private void Data_DataBind()
        {


            try
            {
                this.gvPerAppraisal.DataSource = (DataTable)Session["tblper"];
                this.gvPerAppraisal.DataBind();

            }

            catch (Exception ex)

            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            }
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblper"];
            for (int i = 0; i < this.gvPerAppraisal.Rows.Count; i++)
            {
                string sgval1 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval1")).Checked) ? "True" : "False";
                string sgval2 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval2")).Checked) ? "True" : "False";
                string sgval3 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval3")).Checked) ? "True" : "False";
                string sgval4 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval4")).Checked) ? "True" : "False";
                string sgval5 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval5")).Checked) ? "True" : "False";
                dt.Rows[i]["sgval1"] = sgval1;
                dt.Rows[i]["sgval2"] = sgval2;
                dt.Rows[i]["sgval3"] = sgval3;
                dt.Rows[i]["sgval4"] = sgval4;
                dt.Rows[i]["sgval5"] = sgval5;

            }
            Session["tblper"] = dt;



        }


        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //   {
        //       //string comcod = this.GetComeCode();
        //       Hashtable hst = (Hashtable)Session["tblLogin"];
        //       string comcod = hst["comcod"].ToString();
        //       string comnam = hst["comnam"].ToString();
        //       string compname = hst["compname"].ToString();
        //       string comsnam = hst["comsnam"].ToString();
        //       string comadd = hst["comadd1"].ToString();
        //       string session = hst["session"].ToString();
        //       string username = hst["username"].ToString();
        //       string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //       string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
        //       string mPerNo = this.ddlPreList.SelectedValue.ToString();
        //        //DataSet ds1 = HRData.GetTransInfo (comcod, "dbo_hrm.SP_REPORT_EMPLOYEE_ACR", "RPTEMPLOYEEEVAINFO", mPerNo, "", "", "", "", "", "", "", "");
        //        //DataTable dt = ds1.Tables[0];
        //        //if (dt==null)
        //        //{
        //        //    return;
        //        //}

        //        //var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EMpEvaluationn>();



        //       LocalReport Rpt1 = new LocalReport();
        //       Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_91_ACR.RptEmpEvalu", null, null, null);

        //     ///  Rpt1.SetParameters(new ReportParameter("comadd", comadd));

        //       Session["Report1"] = Rpt1;

        //       //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
        //       //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //       ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
        //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string mPerNo = this.ddlPreList.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_EMPLOYEE_ACR", "RPTEMPLOYEEEVAINFO", mPerNo, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];
            DataTable dt1 = ds1.Tables[1];
            DataTable dt2 = ds1.Tables[2];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_91_ACR.EMpEvaluation>();

            var list1 = dt1.DataTableToList<RealEntity.C_81_Hrm.C_91_ACR.Rptnumber>();
            var list2 = dt2.DataTableToList<RealEntity.C_81_Hrm.C_91_ACR.RptEmpDetails>();

            //for (int i = 0; i <  ds1.Tables[1].Rows.Count; i++)
            //{
            //    string mcod = ds1.Tables[1].Rows[i]["mgcod"].ToString ();
            //    string mdesc = ds1.Tables[1].Rows[i]["mgdesc"].ToString ();
            //    double amt = Convert.ToDouble (ds1.Tables[1].Rows[i]["amt"]);
            //    list1.Add (new Rptnumber (mcod, mdesc, amt));
            //}



            string eid = ds1.Tables[2].Rows[0]["empid"].ToString();
            string ename = ds1.Tables[2].Rows[0]["empname"].ToString();
            string emjdate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["empjoindate"]).ToString("dd-MMM-yyyy");
            string emcdate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["confirmdate"]).ToString("dd-MMM-yyyy");

            string emdesig = ds1.Tables[2].Rows[0]["empdesig"].ToString();
            string emsection = ds1.Tables[2].Rows[0]["empsection"].ToString();
            string supname = ds1.Tables[2].Rows[0]["sname"].ToString();
            string supdesig = ds1.Tables[2].Rows[0]["sdesig"].ToString();

            //  list2.Add(new RealEntity.C_81_Hrm.C_91_ACR.RptEmpDetails(eid,ename,emjdate,emcdate,emdesig,emsection,supname,supdesig));

            string empjdate = (ds1.Tables[2].Rows[0]["empjoindate"]).ToString();
            string empcdate = (ds1.Tables[2].Rows[0]["confirmdate"]).ToString();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_91_ACR.RptEmpEvaluation", list, list1, list2);

            Rpt1.SetParameters(new ReportParameter("eid", eid));
            Rpt1.SetParameters(new ReportParameter("ename", ename));
            Rpt1.SetParameters(new ReportParameter("empjdate", empjdate));
            Rpt1.SetParameters(new ReportParameter("empcdate", empcdate));
            Rpt1.SetParameters(new ReportParameter("emdesig", emdesig));
            Rpt1.SetParameters(new ReportParameter("emsection", emsection));
            Rpt1.SetParameters(new ReportParameter("supname", supname));
            Rpt1.SetParameters(new ReportParameter("supdesig", supdesig));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        //RptEmpEvalu

        protected void lbtnUpPerAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                this.SaveValue();
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)Session["tblper"];
                if (this.ddlPreList.Items.Count == 0)
                    this.GetPerNumber();
                string empid = this.ddlEmpName.SelectedValue.ToString();
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string prono = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string refno = this.txtrefno.Text.Trim();
                bool result = false;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSORUPDATEEVAB", prono, empid, curdate, refno, "",
                  "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcod = dt.Rows[i]["gcod"].ToString();
                    string sgcod = "";
                    for (int j = 1; j <= 5; j++)
                    {
                        sgcod = Convert.ToString("0" + j);
                        bool chkgval = Convert.ToBoolean(dt.Rows[i]["sgval" + j.ToString()]);
                        if (chkgval)
                            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSORUPDATEEVAA", prono, gcod, sgcod, "",
                         "", "", "", "", "", "", "", "", "", "", "");
                        if (!result)
                            return;
                        if (chkgval)
                            break;
                    }


                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }
    }
}