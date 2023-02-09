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
namespace RealERPWEB.F_81_Hrm.F_85_Lon
{
    public partial class EmpLoanInfo : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE LOAN INFORMATION";
                // this.GetLoanNo();
                this.GetEmplist();
                this.GetLoanType();

                this.txtstrdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");




            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void GetLNNo()
        {
            string comcod = this.GetComeCode();
            string mLNNO = "NEWLN";
            if (this.ddlPrevLoanList.Items.Count > 0)
                mLNNO = this.ddlPrevLoanList.SelectedValue.ToString();

            string date = this.txtCurDate.Text; ;
            if (mLNNO == "NEWLN")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTLOANNO", date, "", "", "", "", "", "", "", "");

                if (ds3 == null)
                    return;
                if (ds3.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);

                    this.ddlPrevLoanList.DataTextField = "maxlnno1";
                    this.ddlPrevLoanList.DataValueField = "maxlnno";
                    this.ddlPrevLoanList.DataSource = ds3.Tables[0];
                    this.ddlPrevLoanList.DataBind();
                }
            }

        }
        private void GetEmplist()
        {

            string comcod = this.GetComeCode();
            string txtEmpname = this.txtsrchEmp.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, "", "", "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();

        }

        private void GetLoanType()
        {

            string comcod = this.GetComeCode();
           
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLOANTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlLoantype.DataTextField = "loantype";
            this.ddlLoantype.DataValueField = "gcod";
            this.ddlLoantype.DataSource = ds1.Tables[0];
            this.ddlLoantype.DataBind();

        }


        private void GetPreLnlist()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GetPrevLN", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevLoanList.DataTextField = "lnno1";
            this.ddlPrevLoanList.DataValueField = "lnno";
            this.ddlPrevLoanList.DataSource = ds1.Tables[0];
            this.ddlPrevLoanList.DataBind();
        }

        protected void lbtnPrevLoanList_Click(object sender, EventArgs e)
        {
            this.GetPreLnlist();
        }


        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmplist();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblEmpName.Text = this.ddlEmpList.SelectedItem.Text.Trim();
                this.lbtnPrevLoanList.Visible = false;
                this.ddlPrevLoanList.Visible = false;
                this.ddlEmpList.Visible = false;
                this.lblEmpName.Visible = true;
                this.chkAddIns.Checked = false;
                this.chkVisible.Checked = false;
                this.chkVisible.Visible = true;
                this.ShowLoanInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.lblEmpName.Text = "";

            this.ddlPrevLoanList.Items.Clear();
            this.lbtnPrevLoanList.Visible = true;
            this.ddlPrevLoanList.Visible = true;
            this.ddlEmpList.Visible = true;
            this.txtCurDate.Enabled = true;
            this.lblEmpName.Visible = false;
            this.chkAddIns.Visible = false;
            this.lbtnAddInstallment.Visible = false;
            this.chkVisible.Visible = false;
            this.pnlloan.Visible = false;
            this.ddlLoantype.Enabled = true;

            this.gvloan.DataSource = null;
            this.gvloan.DataBind();
        }

        private void ShowLoanInfo()
       {
            ViewState.Remove("tblln");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mLNNo = "NEWLN";
            if (this.ddlPrevLoanList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                this.chkAddIns.Visible = true;
                this.chkVisible.Visible = false;
                mLNNo = this.ddlPrevLoanList.SelectedValue.ToString();
                this.ddlLoantype.Enabled = false;
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNINFO", mLNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblln"] = ds1.Tables[0];
            if (mLNNo == "NEWLN")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTLOANNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);
                return;
            }
            ViewState["tblln1"] = ds1.Tables[1];
            this.ddlEmpList.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
            this.ddlLoantype.SelectedValue = ds1.Tables[1].Rows[0]["loantype"].ToString();
            this.lblEmpName.Text = this.ddlEmpList.SelectedItem.Text.Trim();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["lnno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["lnno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["lndate"]).ToString("dd-MMM-yyyy");
            this.Data_DataBind();
        }

        private void Data_DataBind()
        {

            this.gvloan.DataSource = (DataTable)ViewState["tblln"];
            this.gvloan.DataBind();
            this.FooterCalculation((DataTable)ViewState["tblln"]);
        }
        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvloan.FooterRow.FindControl("gvlFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))).ToString("#,##0;(#,##0); ");
            Session["Report1"] = gvloan;
            ((HyperLink)this.gvloan.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lnkupdate_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            this.pnlloan.Visible = false;
            this.chkVisible.Checked = false;
            DataTable dt = (DataTable)ViewState["tblln"];
            DataView dv = dt.DefaultView;
            DataTable dt1 = new DataTable();
            dt1 = dt.Clone();

            double toamt = Convert.ToDouble("0" + this.txtToamt.Text.Trim());
            double lnamt = Convert.ToDouble("0" + this.txtinsamt.Text.Trim());
            int dur = Convert.ToInt32(this.ddlMonth.SelectedValue.ToString());
            string date = this.txtstrdate.Text.Trim();
            string lndate;
            DataRow dr1;
            for (int i = 0; i < 500; i++)
            {
                if (toamt > 0)
                {
                    lnamt = (toamt > lnamt) ? lnamt : toamt;
                    if (i == 0)
                    {
                        dr1 = dt1.NewRow();
                        lndate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                        dr1["lndate"] = lndate;
                        dr1["lnamt"] = lnamt;
                        dt1.Rows.Add(dr1);
                        toamt = toamt - lnamt;
                        continue;
                    }
                    dr1 = dt1.NewRow();
                    lndate = Convert.ToDateTime(dt1.Rows[i - 1]["lndate"].ToString()).AddMonths(dur).ToString("dd-MMM-yyyy");
                    dr1["lndate"] = lndate;
                    dr1["lnamt"] = lnamt;
                    dt1.Rows.Add(dr1);
                    toamt = toamt - lnamt;
                }
                else
                {
                    break;

                }
            }
            ViewState["tblln"] = dt1;
            this.Data_DataBind();
        }
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlloan.Visible = this.chkVisible.Checked;

        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                string comcod = this.GetComeCode();
                if (this.ddlPrevLoanList.Items.Count == 0)
                    this.GetLNNo();

                DataTable dt = (DataTable)ViewState["tblln"];
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string lnno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string empid = this.ddlEmpList.SelectedValue.ToString();
                string toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))).ToString();
                string loantype = ddlLoantype.SelectedValue.ToString();
                bool result;
                //Delete Loaninfo
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETELNINFO", lnno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATELN", "LNINFB", lnno, curdate, toamt, "",loantype, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    return;

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string lndate = dt.Rows[i]["lndate"].ToString();
                    string lnamt = Convert.ToDouble(dt.Rows[i]["lnamt"]).ToString();
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATELN", "LNINFA", lnno, empid, lndate, lnamt, "",
                        "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
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
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblln"];
            for (int i = 0; i < this.gvloan.Rows.Count; i++)
            {

                string Insdate = Convert.ToDateTime(((TextBox)this.gvloan.Rows[i].FindControl("txtgvinstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string InsAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvloan.Rows[i].FindControl("gvtxtamt")).Text.Trim())).ToString();
                dt.Rows[i]["lndate"] = Insdate;
                dt.Rows[i]["lnamt"] = InsAmt;
            }
            ViewState["tblln"] = dt;
            this.Data_DataBind();
        }
        protected void lbtnAddInstallment_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblln1"];
            DataTable dt = (DataTable)ViewState["tblln"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string lnno = "";
            if (this.ddlPrevLoanList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                lnno = this.ddlPrevLoanList.SelectedValue.ToString();
            }
            DataRow[] dr = dt1.Select("lnno='" + lnno + "'");
            if (dr.Length != 0)
            {

                DataRow dr1 = dt.NewRow();
                //dr1["comcod"] = hst["comcod"].ToString();
                //dr1["gcod"] = this.ddlInstallment.SelectedValue.ToString();
                //dr1["gval"] = "T";
                //dr1["gdesc"] = this.ddlInstallment.SelectedItem.Text.Trim();
                //dr1["pactcode"] = this.ddlProjectName.SelectedValue.ToString();
                //dr1["usircode"] = this.lblCode.Text.Trim();
                dr1["lndate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                dr1["lnamt"] = 0;
                dt.Rows.Add(dr1);
            }

            Session["tblln"] = dt;
            this.gvloan.DataSource = dt;
            this.gvloan.DataBind();
            this.lbtnTotal_Click(null, null);
        }
        protected void chkAddIns_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddIns.Checked)
            {
                this.lbtnAddInstallment.Visible = true;
            }
            else
            {
                this.lbtnAddInstallment.Visible = false;
            }
        }
    }
}