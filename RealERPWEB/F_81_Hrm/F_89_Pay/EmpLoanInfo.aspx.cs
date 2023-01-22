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
namespace RealERPWEB.F_81_Hrm.F_89_Pay
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

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetLoanNo();
                this.GetEmplist();
                this.txtstrdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");




            }


        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetLoanNo()
        {

            string comcod = this.GetComeCode();
            string date = this.txtCurDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTLOANNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);
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


        protected void ibtnEmpList_Click(object sender, ImageClickEventArgs e)
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
            this.lblEmpName.Visible = false;
            this.gvloan.DataSource = null;
            this.gvloan.DataBind();
        }

        private void ShowLoanInfo()
        {
            ViewState.Remove("tblln");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mLnNo = "NEWLN";
            if (this.ddlPrevLoanList.Items.Count > 0)
                mLnNo = this.ddlPrevLoanList.SelectedValue.ToString();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNINFO", mLnNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblln"] = ds1.Tables[0];


            if (mLnNo == "NEWLN")
            {
                this.GetLoanNo();
                return;
            }

            this.ddlEmpList.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
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
            for (int i = 0; i < 200; i++)
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
                DataTable dt = (DataTable)ViewState["tblln"];


                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string lnno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string empid = this.ddlEmpList.SelectedValue.ToString();
                string toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))).ToString();
                bool result;

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATELN", "LNINFB", lnno, curdate, toamt, "",
                       "", "", "", "", "", "", "", "", "", "");
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
    }
}