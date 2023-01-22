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
    public partial class EmpStdKpiCR : System.Web.UI.Page
    {
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Employee Standerd KPI Setup";
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                this.GetYearMonth();

            }
        }

        private void GetYearMonth()
        {
            Session.Remove("tblyearmon");
            string comcod = this.Getcomcod();

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

        private void GetCopyYearMonth()
        {
            DataTable dt = (DataTable)Session["tblyearmon"];
            this.ddlperyearmon.DataTextField = "yearmon";
            this.ddlperyearmon.DataValueField = "ymon";
            this.ddlperyearmon.DataSource = dt;
            this.ddlperyearmon.DataBind();
        }



        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }




        protected void lnkok_Click(object sender, EventArgs e)
        {

            if (this.lnkok.Text == "Ok")
            {

                ViewState.Remove("tblappmnt");
                string comcod = this.Getcomcod();

                this.ddlyearmon.Enabled = false;
                this.chkcopy.Visible = true;
                this.lnkok.Text = "New";
                this.ShowEmpData();
            }
            else
            {

                this.lnkok.Text = "Ok";
                //this.chkUpload.Checked = false;
                this.ddlyearmon.Enabled = true;
                this.chkcopy.Visible = false;
                this.gvStdKpi.DataSource = null;
                this.gvStdKpi.DataBind();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            }
        }

        private void ShowEmpData()
        {
            string comcod = this.Getcomcod();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string upload = this.chkUpload.Checked ? "upload" : "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = "9403%";
            DataSet ds = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "SHOWSTDKPICR", "", YmonID, upload, deptcode);
            ViewState["tbEmpData"] = this.HiddenSameData(ds.Tables[0]);
            ViewState["tblkpigrp"] = ds.Tables[1];
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string mteamcode = dt1.Rows[0]["mteamcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mteamcode"].ToString() == mteamcode)
                    dt1.Rows[j]["mteamdesc"] = "";
                mteamcode = dt1.Rows[j]["mteamcode"].ToString();
            }
            return dt1;

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tbEmpData"];
            this.gvStdKpi.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvStdKpi.DataSource = dt;
            this.gvStdKpi.DataBind();
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
                ((Label)this.gvStdKpi.FooterRow.FindControl("lblFtarcurdue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(star1)", "")) ?
                                   0 : dt.Compute("sum(star1)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvStdKpi.FooterRow.FindControl("lblFtaroverdue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(star2)", "")) ?
                                  0 : dt.Compute("sum(star2)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvStdKpi.FooterRow.FindControl("lblFtarreturn")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(star3)", "")) ?
                                  0 : dt.Compute("sum(star3)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvStdKpi.FooterRow.FindControl("lblFtaradvcoll")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(star4)", "")) ?
                                  0 : dt.Compute("sum(star4)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvStdKpi.FooterRow.FindControl("lblFtarothers")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(star6)", "")) ?
                                  0 : dt.Compute("sum(star6)", ""))).ToString("#,##0;(#,##0); ");
            }
        }


        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tbEmpData"];
            int rowindex;
            for (int i = 0; i < gvStdKpi.Rows.Count; i++)
            {
                double star1 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvtarcurdue")).Text.Trim());
                double star2 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvtaroverdue")).Text.Trim());
                double star3 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvtarreturn")).Text.Trim());
                double star4 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvtaradvcoll")).Text.Trim());
                double star5 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvtarothers")).Text.Trim());
                double sval1 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvindcurdue")).Text.Trim());
                double sval2 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvindoverdue")).Text.Trim());
                double sval3 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvindreturn")).Text.Trim());
                double sval4 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvindadvcoll")).Text.Trim());
                double sval5 = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvindothers")).Text.Trim());
                double acothers = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvacothers")).Text.Trim());



                rowindex = (this.gvStdKpi.PageIndex * this.gvStdKpi.PageSize) + i;

                dt.Rows[rowindex]["star1"] = star1;
                dt.Rows[rowindex]["star2"] = star2;
                dt.Rows[rowindex]["star3"] = star3;
                dt.Rows[rowindex]["star4"] = star4;
                dt.Rows[rowindex]["star5"] = star5;

                //dt.Rows[i]["stdrmrk"] = Activ;
                dt.Rows[rowindex]["sval1"] = sval1;
                dt.Rows[rowindex]["sval2"] = sval2;
                dt.Rows[rowindex]["sval3"] = sval3;
                dt.Rows[rowindex]["sval4"] = sval4;
                dt.Rows[rowindex]["sval5"] = sval5;

                dt.Rows[rowindex]["acothers"] = acothers;


            }
            ViewState["tbEmpData"] = dt;
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {

            this.Save_Value();
            this.Data_Bind();
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
                string YmonID = this.ddlyearmon.SelectedValue.ToString();
                string comcod = this.Getcomcod();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string deptcode = hst["deptcode"].ToString();

                this.Save_Value();
                DataTable tbl1 = (DataTable)ViewState["tbEmpData"];
                DataTable dt = (DataTable)ViewState["tblkpigrp"];
                bool result = false;
                result = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "DELMONTHLYTARGET", YmonID, deptcode, "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + KpiData.ErrorObject["Msg"];
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }





                foreach (DataRow dr2 in tbl1.Rows)
                {
                    string empid = dr2["empid"].ToString();
                    int i = 1;
                    foreach (DataRow drg in dt.Rows)
                    {

                        string kpigrp = drg["kpigrp"].ToString();
                        string stdtarget = dr2["star" + i].ToString();
                        string stdkpival = dr2["sval" + i].ToString();

                        i++;

                        result = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "INSERTUPDATESTD", YmonID, empid, kpigrp, stdkpival, "", stdtarget, deptcode);
                        if (result == false)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + KpiData.ErrorObject["Msg"];
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                    }
                }

                DataView dv = dt.DefaultView;
                dv.RowFilter = (" kpigrp like '810100602901%' ");
                dt = dv.ToTable();
                foreach (DataRow dr2 in tbl1.Rows)
                {
                    string empid = dr2["empid"].ToString();
                    foreach (DataRow drg in dt.Rows)
                    {

                        string kpigrp = drg["kpigrp"].ToString();
                        string acothers = dr2["acothers"].ToString();



                        result = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "INSORUPACOTHERS", YmonID, empid, kpigrp, acothers, "", "", deptcode);
                        if (result == false)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + KpiData.ErrorObject["Msg"];
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                    }
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


        protected void lnkprint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string comcod = this.Getcomcod();
            //string clientcod = this.ddlClientList.SelectedValue.ToString();
            //DataSet dset1 = this.KpiData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTCLIENTCOMUCATION", clientcod, "", "", "", "", "", "", "", "");
            //DataTable dtab1 = dset1.Tables[0];
            //ReportDocument rptAppMonitor = new  RealERPRPT.R_21_Mkt.RptTodaysDisAndNextApp();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = this.ddlSalesTeam.SelectedItem.Text;
            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //TextObject txtclientname = rptAppMonitor.ReportDefinition.ReportObjects["txtclientname"] as TextObject;
            //txtclientname.Text = this.ddlClientList.SelectedItem.Text;
            //rptAppMonitor.SetDataSource(dtab1);
            //Session["Report1"] = rptAppMonitor;
            //this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }





        protected void gvStdKpi_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //DataTable dt = (DataTable)ViewState["tbEmpData"];
            //string comcod = this.GetHRcomcod();
            //string Empid = this.ddlEmpid.SelectedValue.ToString();

            //string Grp = ((Label)this.gvStdKpi.Rows[e.RowIndex].FindControl("lblgrp")).Text.Trim();

            //bool result = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "DELETESTDKPI", Empid, Grp);

            //if (result == true)
            //{
            //    int rowindex = (this.gvStdKpi.PageSize) * (this.gvStdKpi.PageIndex) + e.RowIndex;
            //    dt.Rows[rowindex].Delete();
            //}

            //DataView dv = dt.DefaultView;
            //ViewState.Remove("tbEmpData");
            //ViewState["tbEmpData"] = dv.ToTable();
            //this.Data_Bind();
        }





        private void GetSircode()
        {

            //-----------Get Person List ---------------//
            //UserManagerKPI objUser = new UserManagerKPI();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetHRcomcod();
            //string srchEmp = "%" + this.txtSircode.Text;
            //List<RealEntity.C_47_Kpi.EClassSirCode> lst3 = new List<RealEntity.C_47_Kpi.EClassSirCode>();
            //lst3 = objUser.GetSirCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETSIRCODE", srchEmp);

            //this.ddlSircode.DataTextField = "sirdesc";
            //this.ddlSircode.DataValueField = "sircode";
            //this.ddlSircode.DataSource = lst3;
            //this.ddlSircode.DataBind();
        }



        protected void btnSircode_Click(object sender, EventArgs e)
        {
            this.GetSircode();
        }

        protected void btnbtnGrp_Click(object sender, EventArgs e)
        {
            //this.lblmsg01.Visible = false;
            //this.GetCompCode();
            //this.GetDptCode();
            //this.GetSircode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["hcomcod"].ToString();
            //string YmonID = this.ddlyearmon.SelectedValue.ToString();
            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string Empid = code.Substring(0, 12).ToString();
            //string grpcode = code.Substring(12, 12).ToString();
            //this.lblgrp.Text = grpcode;

            //DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "SHOWGRPDATA", Empid, grpcode, YmonID);

            //ViewState["tbModalData"] = HiddenSameData1(ds1.Tables[0]);
            //this.Modal_Data_Bind();
            //string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        private void Modal_Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tbModalData"];
            this.gvKpiGrp.DataSource = dt;
            this.gvKpiGrp.DataBind();
            string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            //DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            //string YmonID = this.ddlyearmon.SelectedValue.ToString();
            //string Empid = this.ddlEmpid.SelectedValue.ToString();
            //string grpcode = this.lblgrp.Text;
            //string Compcode = this.ddlCompName.SelectedValue.ToString();
            //string DptCode = this.ddlDptcode.SelectedValue.ToString();
            ////string Sircode = this.ddlSircode.SelectedValue.ToString();

            //DataRow[] dr2 = tbl1.Select("empid ='" + Empid + "' and ymonid = '" + YmonID + "' and kpigrp = '" + grpcode + "' and wrkcom = '" + Compcode + "' and wrkdpt = '" + DptCode +  "'");
            //if (dr2.Length == 0)
            //{

            //    DataRow dr1 = tbl1.NewRow();
            //    dr1["ymonid"] = this.ddlyearmon.SelectedValue.ToString();
            //    dr1["empid"] = this.ddlEmpid.SelectedValue.ToString();
            //    dr1["empname"] = this.ddlEmpid.SelectedItem.ToString();
            //    dr1["wrkcom"] = this.ddlCompName.SelectedValue.ToString();
            //    dr1["comnam"] = this.ddlCompName.SelectedItem.ToString();
            //    dr1["wrkdpt"] = this.ddlDptcode.SelectedValue.ToString();
            //    dr1["wrkdptdesc"] = this.ddlDptcode.SelectedItem.ToString();
            //    //dr1["sircode"] = this.ddlSircode.SelectedValue.ToString();
            //    //dr1["sirdesc"] = this.ddlSircode.SelectedItem.ToString();
            //    dr1["kpigrp"] = grpcode;

            //    tbl1.Rows.Add(dr1);
            //}
            //ViewState["tbModalData"] = HiddenSameData1(tbl1);
            //this.Modal_Data_Bind();
            //string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        private DataTable HiddenSameData1(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {

            }
            else
            {
                string empid = dt1.Rows[0]["empid"].ToString();
                string wrkcom = dt1.Rows[0]["wrkcom"].ToString();
                string wrkdpt = dt1.Rows[0]["wrkdpt"].ToString();
                for (int j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["empid"].ToString() == empid)
                    {
                        empid = dt1.Rows[j]["empid"].ToString();
                        dt1.Rows[j]["empname"] = "";
                    }
                    if (dt1.Rows[j]["wrkcom"].ToString() == wrkcom)
                    {
                        wrkcom = dt1.Rows[j]["wrkcom"].ToString();
                        dt1.Rows[j]["comnam"] = "";
                    }
                    if (dt1.Rows[j]["wrkdpt"].ToString() == wrkdpt)
                    {
                        wrkdpt = dt1.Rows[j]["wrkdpt"].ToString();
                        dt1.Rows[j]["wrkdptdesc"] = "";
                    }


                    empid = dt1.Rows[j]["empid"].ToString();
                    wrkcom = dt1.Rows[j]["wrkcom"].ToString();
                    wrkdpt = dt1.Rows[j]["wrkdpt"].ToString();
                }
            }

            return dt1;

        }
        protected void lUpdatModInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                string YmonID = this.ddlyearmon.SelectedValue.ToString();
                string comcod = this.Getcomcod();
                DataTable tbl1 = (DataTable)ViewState["tbModalData"];

                foreach (DataRow dr2 in tbl1.Rows)
                {
                    string empid = dr2["empid"].ToString();
                    string kpigrp = dr2["kpigrp"].ToString();
                    string wrkcom = dr2["wrkcom"].ToString();
                    string wrkdpt = dr2["wrkdpt"].ToString();

                    bool m = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "INSERTUPDATEGRP", empid, kpigrp, "", wrkdpt, "", YmonID);
                    if (m == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + KpiData.ErrorObject["Msg"];
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
               ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
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
            }
            string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void gvKpiGrp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbModalData"];
            string comcod = this.Getcomcod();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = ((Label)this.gvKpiGrp.Rows[e.RowIndex].FindControl("lblgvEmpid")).Text.Trim();
            string Grp = ((Label)this.gvKpiGrp.Rows[e.RowIndex].FindControl("lblgvGrp")).Text.Trim();

            string wrkdpt = ((Label)this.gvKpiGrp.Rows[e.RowIndex].FindControl("lblgvwrkdpt")).Text.Trim();


            bool result = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "DELETEKPIGRP", Empid, Grp, "", wrkdpt, "", YmonID);

            if (result == true)
            {
                int rowindex = (this.gvKpiGrp.PageSize) * (this.gvKpiGrp.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tbModalData");
            ViewState["tbModalData"] = dv.ToTable();
            this.Modal_Data_Bind();
            string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        //private void M_Save_Value()
        //{
        //    DataTable dt = (DataTable)ViewState["tbModalData"];
        //    for (int i = 0; i < gvStdKpi.Rows.Count; i++)
        //    {

        //        double Kpival = Convert.ToDouble("0" + ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvKpiVal")).Text.Trim());
        //        string remarks = ((TextBox)this.gvStdKpi.Rows[i].FindControl("txtgvStdMrk")).Text.Trim();

        //        dt.Rows[i]["stdkpival"] = Kpival;
        //        dt.Rows[i]["stdrmrk"] = remarks;

        //    }
        //    ViewState["tbModalData"] = dt;
        //}

        protected void gvStdKpi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Save_Value();
            this.gvStdKpi.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }
        protected void btnComp_Click(object sender, EventArgs e)
        {

        }

        protected void btnDpt_Click(object sender, EventArgs e)
        {

        }


        protected void chkcopy_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkcopy.Checked)
            {
                this.GetCopyYearMonth();
            }
            this.pnlCopy.Visible = (this.chkcopy.Checked);
        }
        protected void lbtnCopy_Click(object sender, EventArgs e)
        {
            string comcod = this.Getcomcod();
            string YmonID = this.ddlperyearmon.SelectedValue.ToString();
            string upload = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = hst["deptcode"].ToString();

            DataSet ds = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "SHOWSTDKPICR", "", YmonID, upload, deptcode);
            ViewState["tbEmpData"] = this.HiddenSameData(ds.Tables[0]);
            ViewState["tblkpigrp"] = ds.Tables[1];
            this.Data_Bind();
            this.chkcopy.Checked = false;
            this.chkcopy_CheckedChanged(null, null);


        }
    }
}


