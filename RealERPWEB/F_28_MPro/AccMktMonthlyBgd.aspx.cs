using System;
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
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetYearMonth();
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtbgddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();
            }
        }

        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkUpdate_Click);
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

            this.ddlfrmmonth.DataTextField = "yearmon";
            this.ddlfrmmonth.DataValueField = "ymon";
            this.ddlfrmmonth.DataSource = ds1.Tables[0];
            this.ddlfrmmonth.DataBind();
            string month1 = System.DateTime.Today.AddMonths(1).ToString("MM");
            this.ddlfrmmonth.SelectedValue = yr + month1;
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
            this.CpyCHeck.Checked = false;
            this.pnlCopy.Visible = false;

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
            Session["tblMktType"] = ds2.Tables[1];
            this.dgv3_DataBind();
        }

        private void SessionUpdate2()
        {

            DataTable tblt02 = (DataTable)Session["AccTbl02"];
            int TblRowIndex2;

            for (int j = 0; j < this.dgv3.Rows.Count; j++)
            {
                double dgvTxtAmt1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvTxtAmt1")).Text.Trim()));
                double dgvTxtAmt2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvTxtAmt2")).Text.Trim()));
                double dgvTxtAmt3 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvTxtAmt3")).Text.Trim()));

                TblRowIndex2 = (dgv3.PageIndex) * dgv3.PageSize + j;
                tblt02.Rows[TblRowIndex2]["amt1"] = dgvTxtAmt1;
                tblt02.Rows[TblRowIndex2]["amt2"] = dgvTxtAmt2;
                tblt02.Rows[TblRowIndex2]["amt3"] = dgvTxtAmt3;

            }

            Session["AccTbl02"] = tblt02;
            this.dgv3_DataBind();
        }
       
        protected void gvlnkFTotal_Click(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        private void lnkUpdate_Click(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                msg = "You have no permission to Update!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            this.SessionUpdate2();
            string comcod = this.GetCompCode();
            string yearmon = this.ddlyearmon.SelectedValue.ToString();
            DataTable tblMkt = (DataTable)Session["tblMktType"];

            int k;
            for (int i = 0; i < this.dgv3.Rows.Count; i++)
            {
                k=1;
                string pactcode = ((Label)this.dgv3.Rows[i].FindControl("gvlblActCode")).Text.Trim();
                for (int j = 0; j <tblMkt.Rows.Count; j++)
                {
                    string mktCode = tblMkt.Rows[j]["mktcode"].ToString();
                    double amt1 = Convert.ToDouble("0"+((TextBox)this.dgv3.Rows[i].FindControl("gvTxtAmt"+k.ToString())).Text.Trim());
                    k++;
                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "UPDATE_MON_BUDGET_INFO", pactcode, mktCode, yearmon, amt1.ToString(), "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
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
            this.FooterCalCulation();
        }

        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["AccTbl02"];
            if (dt.Rows.Count > 0)
            {
                ((Label)this.dgv3.FooterRow.FindControl("lblgvFATL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt1)", "")) ?
                    0.00 : dt.Compute("Sum(amt1)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.dgv3.FooterRow.FindControl("lblgvFBTL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt2)", "")) ?
                   0.00 : dt.Compute("Sum(amt2)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.dgv3.FooterRow.FindControl("lblgvFTTL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt3)", "")) ?
                    0.00 : dt.Compute("Sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
            }

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
            string YearMon = this.ddlfrmmonth.SelectedValue.ToString();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_MKT_MON_BUDGET_INFO", YearMon, "", "", "", "");
            if (ds2==null)
                return;

            Session["AccTbl02"] = ds2.Tables[0];
            Session["tblMktType"] = ds2.Tables[1];
            this.dgv3_DataBind();

        }



        protected void CpyCHeck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CpyCHeck.Checked)
            {
                this.pnlCopy.Visible = true;
            }
            else
            {
                this.pnlCopy.Visible = false;
            }
        }

    }
}