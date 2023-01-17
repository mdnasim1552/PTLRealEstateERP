﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
namespace RealERPWEB.F_22_Sal
{
    public partial class ConAssessment : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Current") ? "Sub-Contractor Bill-Floor Wise" : "Contractor Assessment";

                this.GetProjectList();
                this.GetConList();
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;


            }
        }

        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void GetProjectList()
        {
            string comcod = this.GetComeCode();
            string pactcode = "%" + this.txtSrcPro.Text + "%";
            this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONASSPRJLIST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "actdesc1";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ViewState["tblProj"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void GetConList()
        {
            string comcod = this.GetComeCode();
            string conlist = "%" + this.txtSrcSub.Text + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUECONTLIST", conlist, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlcontractorlist.DataTextField = "sircode1";
            this.ddlcontractorlist.DataValueField = "sircode";
            this.ddlcontractorlist.DataSource = ds1.Tables[0];
            this.ddlcontractorlist.DataBind();
            ViewState["tblCon"] = ds1.Tables[0];
        }

        protected void ibtnFindProject_OnClick(object sender, EventArgs e)
        {
            this.GetProjectList();
        }

        protected void ibtnFindSubConName_OnClick(object sender, EventArgs e)
        {
            this.GetConList();
        }

        private void GetAssGenInfo()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETASSGENINFO", "", "",
                         "", "", "", "", "", "", "");


            ViewState["tblAss"] = ds1.Tables[0];
            this.Data_DataBind();
        }

        private void Data_DataBind()
        {

            this.gvAssessment.DataSource = (DataTable)ViewState["tblAss"];
            this.gvAssessment.DataBind();
        }
        private void ShowAssInfo()
        {
            ViewState.Remove("tblAss");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mAssNo = "NEWASS";
            if (this.ddlPrevAssNo.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mAssNo = this.ddlPrevAssNo.SelectedValue.ToString();
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETASSINFO", mAssNo, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ///////Here was
            ViewState["tblAss"] = ds1.Tables[0];



            if (mAssNo == "NEWASS")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "LASTASSINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxassno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxassno1"].ToString().Substring(6);
                this.GetAssGenInfo();
                return;

            }
            ViewState["tblAss1"] = ds1.Tables[1];

            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["assno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["assno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["assdate"]).ToString("dd-MMM-yyyy");
            this.ddlcontractorlist.SelectedValue = ds1.Tables[1].Rows[0]["csircod"].ToString();
            this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pojcod"].ToString();
            this.Data_DataBind();
        }
        protected void GetAssNO()
        {
            string comcod = this.GetComeCode();
            string mAssNO = "NEWASS";
            if (this.ddlPrevAssNo.Items.Count > 0)
                mAssNO = this.ddlPrevAssNo.SelectedValue.ToString();

            string date = this.txtCurDate.Text; ;
            if (mAssNO == "NEWASS")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "LASTASSINFO", date, "", "", "", "", "", "", "", "");

                if (ds3 == null)
                    return;
                if (ds3.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxassno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxassno1"].ToString().Substring(6);

                    this.ddlPrevAssNo.DataTextField = "maxassno1";
                    this.ddlPrevAssNo.DataValueField = "maxassno";
                    this.ddlPrevAssNo.DataSource = ds3.Tables[0];
                    this.ddlPrevAssNo.DataBind();
                }
            }

        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lbtnPrevAssNo.Visible = false;
                this.ddlPrevAssNo.Visible = false;
                this.ShowAssInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";


            this.ddlPrevAssNo.Items.Clear();
            this.lbtnPrevAssNo.Visible = true;
            this.ddlPrevAssNo.Visible = true;
            this.txtCurDate.Enabled = true;


            this.gvAssessment.DataSource = null;
            this.gvAssessment.DataBind();
        }

        private void GetPreAssNo()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPREVASSNO", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevAssNo.DataTextField = "assno1";
            this.ddlPrevAssNo.DataValueField = "assno";
            this.ddlPrevAssNo.DataSource = ds1.Tables[0];
            this.ddlPrevAssNo.DataBind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblAss"];
            int TblRowIndex;
            for (int i = 0; i < this.gvAssessment.Rows.Count; i++)
            {

                string txtgcod = ((TextBox)this.gvAssessment.Rows[i].FindControl("txtasscod")).Text.Trim();
                string txtdesc = ((TextBox)this.gvAssessment.Rows[i].FindControl("txtDescription")).Text.Trim();
                string exc = (((CheckBox)gvAssessment.Rows[i].FindControl("lblexec")).Checked) ? "True" : "False";
                string good = (((CheckBox)gvAssessment.Rows[i].FindControl("lblgood")).Checked) ? "True" : "False";
                string avg = (((CheckBox)gvAssessment.Rows[i].FindControl("lblavrg")).Checked) ? "True" : "False";
                string poor = (((CheckBox)gvAssessment.Rows[i].FindControl("lblpoor")).Checked) ? "True" : "False";
                string nill = (((CheckBox)gvAssessment.Rows[i].FindControl("lblnill")).Checked) ? "True" : "False";


                TblRowIndex = (gvAssessment.PageIndex) * gvAssessment.PageSize + i;
                dt.Rows[TblRowIndex]["asscode"] = txtgcod;
                dt.Rows[TblRowIndex]["assdesc"] = txtdesc;
                dt.Rows[TblRowIndex]["exc"] = exc;
                dt.Rows[TblRowIndex]["good"] = (exc == "True") ? "False" : good;
                dt.Rows[TblRowIndex]["avrg"] = (exc == "True" || good == "True") ? "False" : avg;
                dt.Rows[TblRowIndex]["poor"] = (exc == "True" || good == "True" || avg == "True") ? "False" : poor;
                dt.Rows[TblRowIndex]["nill"] = (exc == "True" || good == "True" || avg == "True" || poor == "True") ? "False" : nill;



            }
            ViewState["tblAss"] = dt;
        }

        protected void lbtnUpPerAppraisal_OnClick(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                if (this.ddlPrevAssNo.Items.Count == 0)
                    this.GetAssNO();

                this.SaveValue();
                DataTable dt = (DataTable)ViewState["tblAss"];
                string empid = this.ddlcontractorlist.SelectedValue.ToString();
                string pdojid = this.ddlprjlist.SelectedValue.ToString();
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string assno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string txtref = this.txtassRef.Text.Trim();


                bool result = false;
                result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEASS", "ASSINFB", assno, empid, pdojid, curdate, txtref, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    return;

                }


                foreach (DataRow dr1 in dt.Rows)
                {

                    string gcod = dr1["asscode"].ToString();
                    string desc = dr1["assdesc"].ToString();
                    string exc = dr1["exc"].ToString();
                    string good = dr1["good"].ToString();
                    string avg = dr1["avrg"].ToString();
                    string poor = dr1["poor"].ToString();
                    string nill = dr1["nill"].ToString();


                    result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEASS", "ASSINFA", assno, gcod, desc, exc, good,
                        avg, poor, nill, "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
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

        protected void lbtnPrevAssNo_OnClick(object sender, EventArgs e)
        {
            this.GetPreAssNo();
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            ////string hostname = hst["hostname"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string title = "ঠিকাদার এর মূল্যায়ন প্রতিবেদন";
            DataTable dt1 = (DataTable)ViewState["tblAss"];
            DataTable dt2 = (DataTable)ViewState["tblCon"];
            DataTable dt3 = (DataTable)ViewState["tblProj"];
            string empId = this.ddlcontractorlist.SelectedValue;
            string empname = dt2.Select("sircode='" + empId + "'")[0]["sirdesc"].ToString();
            string projId = this.ddlprjlist.SelectedValue;
            string proj = dt3.Select(("actcode='" + projId + "'"))[0]["actdesc"].ToString();
            string mark = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(mark)", "")) ?
                   0.00 : dt1.Compute("Sum(mark)", ""))).ToString("#,##0.00;(#,##0.00); ");
            string per = ((Convert.ToDouble(mark) * 100) / 50).ToString("#,##0.00;(#,##0.00); ");
            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.EmpAssesment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_22_Sal.RptConAssess", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtTitle", title));
            Rpt1.SetParameters(new ReportParameter("txtMark", mark));
            Rpt1.SetParameters(new ReportParameter("txtEmpname", empname));
            Rpt1.SetParameters(new ReportParameter("txtProj", proj));
            Rpt1.SetParameters(new ReportParameter("txtPer", per));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }

    }
}