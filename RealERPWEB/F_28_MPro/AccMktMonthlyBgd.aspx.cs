﻿using System;
using System.Collections;
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

namespace RealERPWEB.F_28_MPro
{
    public partial class AccMktMonthlyBgd : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        string msg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                this.GetYearMonth();
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtbgddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

     
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
    
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();
            string yr = System.DateTime.Today.ToString("yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            string month = System.DateTime.Today.ToString("MM");
            this.ddlyearmon.SelectedValue = yr + month;

            this.ddltomonth.DataTextField = "yearmon";
            this.ddltomonth.DataValueField = "ymon";
            this.ddltomonth.DataSource = ds1.Tables[0];
            this.ddltomonth.DataBind();
            string month1 = System.DateTime.Today.AddMonths(1).ToString("MM");
            this.ddltomonth.SelectedValue = yr + month1;
            ds1.Dispose();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //
            //

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.MultiView1.ActiveViewIndex = 0;
                this.GetBudgetInfo();
                this.ddlyearmon.Enabled = false;
                return;
            }

            this.lbtnOk.Text = "Ok";
            this.txtCurDate.Enabled = true;
            this.MultiView1.ActiveViewIndex = -1;
            this.ddlyearmon.Enabled = true;

        }
        private void GetBudgetInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();  
            string YearMon = this.ddlyearmon.SelectedValue.ToString();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_MKT_MON_BUDGET_INFO", YearMon, "", "", "", "");
            if (ds2==null)
                return;

            Session["AccTbl02"] = ds2.Tables[0];
            this.dgv3_DataBind();
        }

        private void SessionUpdate2()
        {

            DataTable tblt02 = (DataTable)Session["AccTbl02"];
            int TblRowIndex2;

            for (int j = 0; j < this.dgv3.Rows.Count; j++)
            {
                double dgvTrnRate;
                double dgvTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtQty")).Text.Trim()));
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text.Trim()));
                if (dgvTrnDrAmt == 0 && dgvTrnCrAmt == 0)
                {
                    dgvTrnRate = 0;
                }
                else
                {
                    dgvTrnRate = (dgvTrnQty == 0 ? 0.00 : (dgvTrnDrAmt + dgvTrnCrAmt) / dgvTrnQty);
                }
                ((Label)this.dgv3.Rows[j].FindControl("gvlblRate")).Text = dgvTrnRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text = dgvTrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text = dgvTrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
                TblRowIndex2 = (dgv3.PageIndex) * dgv3.PageSize + j;
                tblt02.Rows[TblRowIndex2]["qty"] = dgvTrnQty;
                tblt02.Rows[TblRowIndex2]["rate"] = dgvTrnRate;
                tblt02.Rows[TblRowIndex2]["Dr"] = dgvTrnDrAmt;
                tblt02.Rows[TblRowIndex2]["Cr"] = dgvTrnCrAmt;

            }
            Session["AccTbl02"] = tblt02;

            this.dgv3_DataBind();
        }
       
        protected void gvlnkFTotal_Click(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
       
        private void UpdateTable02()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SessionUpdate2();
            string comcod = this.GetCompCode();
            string voudat = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string yearmon = this.ddlyearmon.SelectedValue.ToString();
            DataTable tblt03 = (DataTable)Session["AccTbl02"];

            for (int i = 0; i < tblt03.Rows.Count; i++)
            {
                string actcode = tblt03.Rows[i]["actcode"].ToString();
                string bgdqty = tblt03.Rows[i]["qty"].ToString();
                string Dramt = Convert.ToDouble(tblt03.Rows[i]["Dr"]).ToString();
                string Cramt = Convert.ToDouble(tblt03.Rows[i]["Cr"]).ToString();

                bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSERTORUPBGDINF", "00000000000000", actcode, voudat, bgdqty, Cramt.ToString(), Dramt.ToString(), yearmon, "", "", "", "", "", "", "");
                if (!resulta)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
            }

            this.txtCurDate.Enabled = false;
            msg = "Monthly Budget Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Monthly Budget";
                string eventdesc = "Update Details Budget";
                string eventdesc2 = yearmon;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void dgv3_DataBind()
        {
            DataTable tblt03 = (DataTable)Session["AccTbl02"];
            this.dgv3.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.dgv3.DataSource = tblt03;
            this.dgv3.DataBind();
            if (tblt03.Rows.Count == 0)
                return;
            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftDramt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(Dr)", "")) ?
            0.00 : tblt03.Compute("Sum(Dr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftCramt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(Cr)", "")) ?
            0.00 : tblt03.Compute("Sum(Cr)", ""))).ToString("#,##0.00;(#,##0.00);  ");

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        protected void dgv3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3.PageIndex = e.NewPageIndex;
            this.dgv3_DataBind();
        }

        protected void LbtnCopy_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mBGDDAT = Convert.ToDateTime(this.txtbgddate.Text).ToString("dd-MMM-yyyy");
            string tomonth = this.ddltomonth.SelectedValue.ToString();
            string frmonth = this.ddlyearmon.SelectedValue.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "COPY_MONTHLY_BUDGET", mBGDDAT, tomonth, frmonth, "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.ddlyearmon.SelectedValue = tomonth;
                msg = "Budget Copy Success";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                this.GetBudgetInfo();
            }
            else
            {
                msg = "Budget Copy Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            }
        }



        protected void CpyCHeck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CpyCHeck.Checked)
            {
                this.CopyTo.Visible = true;
                this.Copybtn.Visible = true;
                this.datediv.Visible = true;
            }
            else
            {
                this.CopyTo.Visible = false;
                this.Copybtn.Visible = false;
                this.datediv.Visible = false;
            }
        }

        protected void lnkbtnUpdateRes_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                msg = "You have no permission to Update!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            this.UpdateTable02();

        }


    }
}