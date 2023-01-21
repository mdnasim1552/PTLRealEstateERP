using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealEntity;
using RealEntity.C_21_Mkt;
using RealERPLIB;
using RealERPRDLC;
using System.Web.Services;
namespace RealERPWEB.F_47_Kpi
{

    public partial class RptEmpEvaluation : System.Web.UI.Page
    {
        ProcessAccess KpiData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Employee Evaluation(Individual)";
                this.GetTeamCode();
                this.GetYearMonth();
            }
        }

        private void GetTeamCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchoption = "%%";
            DataSet ds1 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EVALUATION", "GETEMPLYEENAME", srchoption, "", "", "", "", "", "", "", "");

            this.ddlTeam.DataTextField = "empname";
            this.ddlTeam.DataValueField = "empid";
            this.ddlTeam.DataSource = ds1.Tables[0];
            this.ddlTeam.DataBind();
            ds1.Dispose();



            ViewState["tblempname"] = ds1.Tables[0];
            ddlTeam_SelectedIndexChanged(null, null);
        }
        private void GetYearMonth()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            Session["tblyearmon"] = ds1.Tables[0];
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();
        }

        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblempname"];
            string ddlempname = this.ddlTeam.SelectedValue.Trim().ToString();
            DataRow[] dr = dt.Select("empid = '" + ddlempname + "'");

            this.lbbsupname.Text = dr[0]["teamdesc"].ToString();
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string selfcomm = this.txtSelefComment.Text.Trim().ToString();
            string supercom = this.txtSupcomments.Text.Trim().ToString();

            DataTable dt = (DataTable)ViewState["tbEmpData"];
            DataTable dtnote = (DataTable)ViewState["tbEmpNote"];
            DataTable dte = (DataTable)ViewState["tblempname"];
            string empid = this.ddlTeam.SelectedValue.ToString();
            string empname = "Name:" + " " + (dte.Select("empid='" + empid + "'"))[0]["empname1"].ToString();
            string desig = "Designation:" + " " + (dte.Select("empid='" + empid + "'"))[0]["desig"].ToString();
            string month = "Month:" + " " + this.ddlyearmon.SelectedItem.Text.Trim();
            string section = "Dept:" + " " + (dte.Select("empid='" + empid + "'"))[0]["section"].ToString();
            string supervisor = "Supervisor:" + " " + (dte.Select("empid='" + empid + "'"))[0]["teamdesc"].ToString();
            var lst = dt.DataTableToList<RealEntity.C_47_Kpi.EclassKeyResult>();
            var lst1 = dtnote.DataTableToList<RealEntity.C_47_Kpi.EclassKRANOte>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_47_kpi.RptKeyResultArea", lst, lst1, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Employee Evaluation(KPI, KRA)"));
            Rpt1.SetParameters(new ReportParameter("txtemployeeName", empname));
            Rpt1.SetParameters(new ReportParameter("txtdesignation", desig));
            Rpt1.SetParameters(new ReportParameter("txtDepartment", section));
            Rpt1.SetParameters(new ReportParameter("txtmonth", month));
            Rpt1.SetParameters(new ReportParameter("txtSupName", supervisor));


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("selfcomm", selfcomm));
            Rpt1.SetParameters(new ReportParameter("supercom", supercom));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void ShowEmpData()
        {


            this.pnlNarration.Visible = true;
            string comcod = this.Getcomcod();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string upload = "upload";//this.chkUpload.Checked ? "upload" : "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string teamid = this.ddlTeam.SelectedValue.ToString();

            DataSet ds = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EVALUATION", "RPTINDEMPEVALUATION", YmonID, teamid);
            ViewState["tbEmpData"] = this.HiddenSameData(ds.Tables[0]);
            ViewState["tbEmpNote"] = ds.Tables[2];

            DataTable dtc = ds.Tables[1];
            this.txtSelefComment.Text = dtc.Rows.Count == 0 ? "" : dtc.Rows[0]["selfcomments"].ToString();
            this.txtSupcomments.Text = dtc.Rows.Count == 0 ? "" : dtc.Rows[0]["supcomments"].ToString();

            this.Data_Bind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string mkpigrp = dt1.Rows[0]["mkpigrp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mkpigrp"].ToString() == mkpigrp)
                {
                    dt1.Rows[j]["mkpidesc"] = "";
                    dt1.Rows[j]["kraval"] = 0.00;
                    dt1.Rows[j]["slnum"] = 0;
                }
                mkpigrp = dt1.Rows[j]["mkpigrp"].ToString();
            }
            return dt1;

        }
        private void Data_Bind()
        {

            //DataTable dtHead = (DataTable)ViewState["tblkpigrp"];
            //int j = 4;
            //for (int i = 0; i < dtHead.Rows.Count; i++)
            //{

            //    this.gvStdKpi.Columns[j].HeaderText = dtHead.Rows[i]["kpigrpdesc"].ToString();
            //    j++;
            //    if (j == 11)
            //        break;


            //}
            //int k = 12;
            //for (int i = 0; i < dtHead.Rows.Count; i++)
            //{

            //    this.gvStdKpi.Columns[k].HeaderText = dtHead.Rows[i]["kpigrpdesc"].ToString();
            //    k++;
            //    if (k == 19)
            //        break;


            //}

            DataTable dt = (DataTable)ViewState["tbEmpData"];
            this.gvKRA.DataSource = dt;
            this.gvKRA.DataBind();
            this.FooterCal();
            //for (int i = 0; i < gvStdKpi.Rows.Count; i++)
            //{
            //    string Empid = ((Label)gvStdKpi.Rows[i].FindControl("lblgvEmpid")).Text.Trim();
            //    string Grp = ((Label)gvStdKpi.Rows[i].FindControl("lblgrp")).Text.Trim();
            //    LinkButton lbtn1 = (LinkButton)gvStdKpi.Rows[i].FindControl("btnGrp");
            //    if (lbtn1 != null)
            //    {
            //        if (lbtn1.Text.Trim().Length > 0)
            //            lbtn1.CommandArgument = Empid + Grp;
            //    }
            //}

        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["tbEmpData"];
            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvKRA.FooterRow.FindControl("lblgvFTotalWeight")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(stdkpival)", "")) ?
                                   0 : dt.Compute("sum(stdkpival)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvKRA.FooterRow.FindControl("lblgvFachive")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(achived)", "")) ?
                                   0 : dt.Compute("sum(achived)", ""))).ToString("#,##0.00;(#,##0.00); ");

            }
        }

        protected void gvKRA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvKRA.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.ShowEmpData();
        }
        protected void lnkappupdate_Click(object sender, EventArgs e)
        {



            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            try
            {

                string comcod = this.Getcomcod();
                string YmonID = this.ddlyearmon.SelectedValue.ToString();
                string empid = this.ddlTeam.SelectedValue.ToString();
                string selftcomments = this.txtSelefComment.Text.Trim();
                string supcomments = this.txtSupcomments.Text.Trim();


                bool result = false;
                result = KpiData.UpdateXmlTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "INSERTUPDATEKPICOMMENTS", null, null, null, YmonID, empid, selftcomments, supcomments, "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + KpiData.ErrorObject["Msg"];
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }







                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Update Info";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
    }
}