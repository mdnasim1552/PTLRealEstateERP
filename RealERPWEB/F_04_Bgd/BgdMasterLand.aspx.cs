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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_04_Bgd
{
    public partial class BgdMasterLand : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "Master Budget Information";

            if (this.gvAcc.PageCount == 0)
            {
                this.GetMainBudgetData();
                this.MultiView1.ActiveViewIndex = 0;
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void GetMainBudgetData()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = this.txtSearch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETMAINBGDINFO", filter, "", "", "", "", "", "", "", "");
            DataTable dt11 = ds1.Tables[0];
            if (dt11.Rows.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Give Correct Information";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                this.gvAcc.DataSource = null;
                this.gvAcc.DataBind();
                return;
            }
            Session["tblAcc"] = dt11;
            //Session["tblAcc"] = ds1.Tables[0];

            DateTime dt1 = Convert.ToDateTime(ds1.Tables[0].Rows[0]["bgddat"]);

            dt1 = (dt1.Year == 1900 ? DateTime.Today : dt1);
            this.txtdate.Text = dt1.ToString("dd.MM.yyyy");
            this.gvAcc_DataBind();
        }

        protected void gvAcc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblAcc"];
            this.gvAcc.DataSource = tbl1;
            this.gvAcc.DataBind();
            //if (tbl1.Rows.Count == 0)
            //    return;
            ((DropDownList)this.gvAcc.FooterRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvAcc.PageSize);
            ((DropDownList)this.gvAcc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvAcc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.gvAcc.FooterRow.FindControl("ddlPageNo")).Visible = true;
            ((DropDownList)this.gvAcc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvAcc.PageIndex;
            this.LnkfTotal_Click(null, null);
        }

        protected void gvAcc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string mAccDesc = ((Label)e.Row.FindControl("lblAccDesc")).Text.Trim();
            string mLevel = ((LinkButton)e.Row.FindControl("gvlnkLevel")).Text.Trim();
            ((TextBox)e.Row.FindControl("txtgvDrAmt")).ReadOnly = (mLevel == "2");
            ((TextBox)e.Row.FindControl("txtgvCrAmt")).ReadOnly = (mLevel == "2");
            ((TextBox)e.Row.FindControl("txtgvRmRk")).ReadOnly = (mLevel == "2");

            if (mLevel == "2")
                ((LinkButton)e.Row.FindControl("gvlnkLevel")).CommandArgument = mAccDesc;
        }

        protected void Session_tblAcc_Update()
        {

            DataTable tbl1 = (DataTable)Session["tblAcc"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvAcc.Rows.Count; j++)
            {
                double dgvDrAm = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAcc.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                double dgvCrAm = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAcc.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
                string dgvRmrk = ((TextBox)this.gvAcc.Rows[j].FindControl("txtgvRmRk")).Text.Trim();

                //double rgamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAcc.Rows[j].FindControl("gvtxtrgamt")).Text.Trim()));

                dgvCrAm = (dgvDrAm > 0 ? 0 : dgvCrAm);

                ((TextBox)this.gvAcc.Rows[j].FindControl("txtgvDrAmt")).Text = dgvDrAm.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvAcc.Rows[j].FindControl("txtgvCrAmt")).Text = dgvCrAm.ToString("#,##0.00;(#,##0.00); ");

                TblRowIndex2 = (this.gvAcc.PageIndex) * this.gvAcc.PageSize + j;
                tbl1.Rows[TblRowIndex2]["dram"] = dgvDrAm;
                tbl1.Rows[TblRowIndex2]["cram"] = dgvCrAm;
                tbl1.Rows[TblRowIndex2]["bgdrmrk"] = dgvRmrk;
                //tbl1.Rows[TblRowIndex2]["rgamt"] = rgamt;
            }
            Session["tblAcc"] = tbl1;
        }

        protected void gvlnkLevel_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 1;
            this.Panel2.Visible = true;
            string mAccDesc = ((LinkButton)sender).CommandArgument.ToString().Trim();
            this.lblAccHead.Text = mAccDesc;
            this.ShowData();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Master Budget";
                string eventdesc = "Click Master Budget Details Head";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string mCode = mAccDesc.Trim().Substring(0, 12);
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETDETAILSBGDINFO", "PB", mCode, "", "", "", "", "", "", "");
            //Session["tblRes"] = ds1.Tables[0];
            //this.gvRes.PageIndex = 0;
            //this.gvRes_DataBind();
        }
        protected void LnkfTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblAcc_Update();
            DataTable tbl1 = (DataTable)Session["tblAcc"];
            ((TextBox)this.gvAcc.FooterRow.FindControl("txtTgvDrAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(dram)", "")) ?
                    0.00 : tbl1.Compute("Sum(dram)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((TextBox)this.gvAcc.FooterRow.FindControl("txtTgvCrAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(cram)", "")) ?
                    0.00 : tbl1.Compute("Sum(cram)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr5 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr5[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.LnkfTotal_Click(null, null);
            string mBGDNUM = "PB000000000000";
            string mBGDDAT = this.GetStdDate(this.txtdate.Text);
            DataTable tbl1 = (DataTable)Session["tblAcc"];
            DataRow[] dr1 = tbl1.Select("actelev <>'2' and (dram-cram)<>0");
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "ACBGDDELETE",
                  mBGDNUM, "%", "000000000000", "", "", "", "", "", "", "", "", "", "", "", "");
            for (int i = 0; i < dr1.Length; i++)
            {
                string mACTCODE = dr1[i]["actcode"].ToString();
                string mRSIRCODE = "000000000000"; // dr1[i]["rsircode"].ToString();
                string mBGDQTY = "0"; // dr1[i]["bgdqty"].ToString();
                string mBGDAM = Convert.ToString(Convert.ToDouble(dr1[i]["dram"]) - Convert.ToDouble(dr1[i]["cram"]));
                string mBGDRMRK = tbl1.Rows[i]["bgdrmrk"].ToString();

                result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "ACBGDUPDATE",
                         mBGDNUM, mACTCODE, mRSIRCODE, mBGDDAT, mBGDQTY, mBGDAM, mBGDRMRK, "", "", "",
                         "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Master Budget";
                string eventdesc = "Master Budget Main Head Update";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr6 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr6[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.gvlnkFTotal_Click(null, null);
            string mBGDNUM = "PB000000000000";
            string mBGDDAT = this.GetStdDate(this.txtdate.Text);

            DataTable tbl1 = (DataTable)Session["tblRes"];
            DataRow[] dr1 = tbl1.Select("(dram-cram)<>0");
            string mACTCODE = this.lblAccHead.Text.Trim().Substring(0, 12);

            string Permission = (((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked) ? "1" : "0";
            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "ACBGDDELETE",
            //      mBGDNUM, mACTCODE, "%", "", "", "", "", "", "", "", "", "", "", "", "");
            for (int i = 0; i < dr1.Length; i++)
            {
                string mRSIRCODE = dr1[i]["rsircode"].ToString();
                string spcfcod = dr1[i]["spcfcod"].ToString();
                string mBGDQTY = dr1[i]["bgdqty"].ToString();
                string mBGDAM = Convert.ToString(Convert.ToDouble(dr1[i]["dram"]) - Convert.ToDouble(dr1[i]["cram"]));
                string mBGDRMRK = tbl1.Rows[i]["bgdrmrk"].ToString();
                // string mBGRGAMT =Convert.ToDouble( dr1[i]["rgamt"]).ToString();
                string mBGRGAMT = dr1[i]["rgamt"].ToString();
                string mOthAMT = dr1[i]["othamt"].ToString();
                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "ACBGDUPDATE",
                           mBGDNUM, mACTCODE, mRSIRCODE, spcfcod, mBGDDAT, mBGDQTY, mBGDAM, mBGDRMRK, Permission, mBGRGAMT, mOthAMT,
                           "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            //this.GetMainBudgetData();
            this.MultiView1.ActiveViewIndex = 1;
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Master Budget";
                string eventdesc = "Master Budget Details Head Update";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblAcc_Update();
            this.gvAcc.PageIndex = ((DropDownList)this.gvAcc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvAcc_DataBind();
        }

        protected void gvlnkFTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblRes_Update();
            DataTable tbl1 = (DataTable)Session["tblRes"];
            if (tbl1.Rows.Count == 0)
                return;
            ((TextBox)this.gvRes.FooterRow.FindControl("gvtxtftDramt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(dram)", "")) ?
                0.00 : tbl1.Compute("Sum(dram)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((TextBox)this.gvRes.FooterRow.FindControl("gvtxtftCramt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(cram)", "")) ?
                    0.00 : tbl1.Compute("Sum(cram)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((TextBox)this.gvRes.FooterRow.FindControl("gvtxtrgtotal")).Text =
             Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(rgamt)", "")) ?
                 0.00 : tbl1.Compute("Sum(rgamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvRes.FooterRow.FindControl("lblgvFother")).Text =
             Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(othamt)", "")) ?
                 0.00 : tbl1.Compute("Sum(othamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }

        protected void gvRes_DataBind()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable tbl1 = (DataTable)Session["tblRes"];
            this.gvRes.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRes.DataSource = tbl1;
            this.gvRes.DataBind();
            if (tbl1.Rows.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please search by right code!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            //DataTable tbl1 = (DataTable)Session["AccTbl01"];
            //this.dgv2.DataSource = tbl1;
            //this.dgv2.DataBind();
            //if (tbl1.Rows.Count == 0)
            //    return;
            //-------------------
            ((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked = Convert.ToBoolean(tbl1.Rows[0]["lock"].ToString());


            if (Request.QueryString["InputType"].ToString() == "BgdMain")
            {
                ((LinkButton)this.gvRes.FooterRow.FindControl("lnkSubmit")).Visible = (((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((LinkButton)this.gvRes.FooterRow.FindControl("gvlnkFTotal")).Enabled = (((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                //((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Visible = (((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Enabled = false;

            }

            //-------------------
            this.gvlnkFTotal_Click(null, null);
        }

        protected void Session_tblRes_Update() // new
        {

            DataTable tbl1 = (DataTable)Session["tblRes"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvRes.Rows.Count; j++)
            {
                double dgvQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtQty")).Text.Trim()));
                double dgvDrAm = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtDrAmt")).Text.Trim()));
                double dgvCrAm = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtCrAmt")).Text.Trim()));
                double dgvRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtRate")).Text.Trim()));
                double dgvrg = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtrgamt")).Text.Trim()));
                double dgvoth = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvRes.Rows[j].FindControl("txtgvothamt")).Text.Trim()));
                string dgvRmrk = ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtRmRk")).Text.Trim();
                dgvCrAm = (dgvDrAm > 0 ? 0 : dgvCrAm);

                dgvRate = (dgvQty == 0 ? 0.00 : (dgvDrAm + dgvCrAm) / dgvQty);

                ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtQty")).Text = dgvQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtDrAmt")).Text = dgvDrAm.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtCrAmt")).Text = dgvCrAm.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvRes.Rows[j].FindControl("gvtxtRate")).Text = dgvRate.ToString("#,##0.00;(#,##0.00); ");

                TblRowIndex2 = (this.gvRes.PageIndex) * this.gvRes.PageSize + j;
                tbl1.Rows[TblRowIndex2]["bgdqty"] = dgvQty;
                tbl1.Rows[TblRowIndex2]["bgdrate"] = dgvRate;
                tbl1.Rows[TblRowIndex2]["dram"] = dgvDrAm;
                tbl1.Rows[TblRowIndex2]["dram"] = dgvDrAm;
                tbl1.Rows[TblRowIndex2]["cram"] = dgvCrAm;
                tbl1.Rows[TblRowIndex2]["bgdrmrk"] = dgvRmrk;
                tbl1.Rows[TblRowIndex2]["rgamt"] = dgvrg;
                tbl1.Rows[TblRowIndex2]["othamt"] = dgvoth;

            }
            Session["tblRes"] = tbl1;
        }
        protected void ddlPageNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblRes_Update();
            this.gvRes.PageIndex = ((DropDownList)this.gvRes.FooterRow.FindControl("ddlPageNo2")).SelectedIndex;
            this.gvRes_DataBind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            if (this.ddlAccountDesc.Visible == false)
            {
                DataTable dt = (DataTable)Session["tblAcc"];
                if (dt == null)
                    return;

                LocalReport Rpt1 = new LocalReport();
                var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BugMasterDetails>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptMasterBudget", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Master Budget Report"));
                Rpt1.SetParameters(new ReportParameter("totalDrAmount", ((TextBox)this.gvAcc.FooterRow.FindControl("txtTgvDrAmt")).Text.ToString().Trim()));
                Rpt1.SetParameters(new ReportParameter("totalCrAmount", ((TextBox)this.gvAcc.FooterRow.FindControl("txtTgvCrAmt")).Text.ToString().Trim()));
                Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            else
            {
                string Code = this.ddlAccountDesc.SelectedItem.ToString().Trim();
                string mCode = Code.Trim().Substring(0, 12);
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETDETAILSBGDINFO", "PB", mCode, "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                if (dt1 == null)
                    return;

                LocalReport Rpt1 = new LocalReport();
                var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BugMasterDetails>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptDetailsBudget", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Details Budget Report"));
                Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Master Budget";
                string eventdesc = "Master Budget Update";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblbudgt.SelectedIndex == 0)
            {
                this.ddlAccountDesc.Visible = false;
            }
            else
            {
                DataTable dt1 = (DataTable)Session["tblAcc"];
                DataRow[] dr = dt1.Select("actelev= '2'");
                dt1 = dr.CopyToDataTable();
                this.ddlAccountDesc.DataTextField = "actdesc";
                this.ddlAccountDesc.DataValueField = "actcode";
                this.ddlAccountDesc.DataSource = dt1;
                this.ddlAccountDesc.DataBind();

                this.ddlAccountDesc.Visible = true;
            }
        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblRes_Update();
            this.gvRes_DataBind();
        }
        protected void ibtnDetailsCode_Click(object sender, EventArgs e)
        {
            //this.ShowResource();
            //string search = this.txtResSearch.Text.Trim().Substring(0,2);

            //if (search == "01" || search == "04")
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Please input right value!";
            //}
            //else
            //{

            this.ShowData();
            //}

        }
        private void ShowData()
        {
            Session.Remove("tblRes");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 1;
            string search = this.txtResSearch.Text.Trim() + "%";
            string mCode = this.lblAccHead.Text.Trim().Substring(0, 12);
            string Code = (comcod.Substring(0, 1) == "2") ? "(left(sircode,2) between '01' and '20')"
                : (mCode.Substring(0, 4) == "1102") ? "( sircode like '0[1-3]%' or sircode like '06%' or sircode like '2[12]%' )"
                : (mCode.Substring(0, 2) == "11") ? "sircode like '2[12]%'" : "(left(sircode,2) between '07' and '20')";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETDETAILSBGDINFOLAND", "PB", mCode, Code, search, "", "", "", "", "");
            Session["tblRes"] = this.HiddenSameData(ds1.Tables[0]);
            this.gvRes.PageIndex = 0;
            this.gvRes_DataBind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                    dt1.Rows[j]["rsirdesc"] = "";

                rsircode = dt1.Rows[j]["rsircode"].ToString();
            }
            return dt1;

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.Panel2.Visible = false;
            this.gvAcc.Visible = true;
            this.MultiView1.ActiveViewIndex = 0;
            this.txtResSearch.Text = "";
            this.GetMainBudgetData();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Master Budget";
                string eventdesc = "Back to Main Head";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void ShowResource()
        {



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string acccode01 = this.lblAccHead.Text.Trim().Substring(0, 12);
            //string filter2 = this.txtResSearch.Text.Trim() + "%";
            //string mBGDNUM = "PB000000000000";
            //string mBGDDAT = this.GetStdDate(this.txtdate.Text);
            //DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBGDRES", filter2, acccode01, bgdnum, "", "", "", "", "", "");
            //Session["AccTbl02"] = ds2.Tables[0];
            //this.dgv3_DataBind();
        }
        protected void gvRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_tblRes_Update();
            this.gvRes.PageIndex = e.NewPageIndex;
            this.gvRes_DataBind();

            //this.SessionUpdate2();
            //this.dgv3.PageIndex = e.NewPageIndex;
            //this.dgv3_DataBind();
        }
        protected void chklkrate_CheckedChanged(object sender, EventArgs e)
        {
            //if (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked)
            //    this.chktest.Text = "Check";
            //else
            //    this.chktest.Text = "Uncheck";
        }
        protected void ShowResourceList()
        {
            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            this.gvRes.DataSource = tbl1;
            this.gvRes.DataBind();

            ((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked = Convert.ToBoolean(tbl1.Rows[0]["lock"].ToString());

            if (Request.QueryString["InputType"].ToString() == "BgdMain")
            {
                ((LinkButton)this.gvRes.FooterRow.FindControl("lbtnUpdateResRate")).Visible = (((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((LinkButton)this.gvRes.FooterRow.FindControl("lbtnResTotal")).Visible = (((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Visible = (((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((CheckBox)this.gvRes.FooterRow.FindControl("chklkrate")).Enabled = false;

            }
            //if (tbl1.Rows.Count > 0)
            //    ((Label)this.gvRes.FooterRow.FindControl("lblgvTResAmtFooter")).Text =
            //        Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(tresamt)", "")) ? 0.00 : tbl1.Compute("sum(tresamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            // this.lbtnUpdateResRate_Click(null, null);
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            this.GetMainBudgetData();
            // this.GridLoad();
        }
        //private void GridLoad()
        //{
        //    Session.Remove("tblAcc");
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string filter = this.txtSearch.Text.Trim() + "%";
        //    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETBUDGETAMT", filter, "", "", "", "", "", "", "", "");
        //    Session["tblAcc"] = ds1.Tables[0];
        //    this.gvAcc_DataBind();

        //    //this.TotalCalculation1();

        //}

        protected void gvRes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblRes"];
            string actcode = this.lblAccHead.Text.Trim().Substring(0, 12);
            string rescode = ((Label)this.gvRes.Rows[e.RowIndex].FindControl("gvlblrescode")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEBGAMT", actcode,
                         rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvRes.PageSize) * (this.gvRes.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblRes");
            Session["tblRes"] = dv.ToTable();
            this.gvRes_DataBind();
        }
        protected void gvRes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            HyperLink mdesc = (HyperLink)e.Row.FindControl("HLgvDesc");


            string Pactcode = this.lblAccHead.Text.Trim().Substring(0, 12);
            string rsircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            //HyperLink hlink2 = (HyperLink)e.Row.FindControl("HLgvStatus");
            string mCOMCOD = comcod;


            if (ASTUtility.Left(rsircode, 2) == "14")
            {
                mdesc.Font.Bold = true;
                //hlink2.Font.Bold = true;
                mdesc.Style.Add("color", "blue");
                //hlink2.Style.Add("color", "blue");
                hlink1.NavigateUrl = "~/F_51_LBgd/LinkLandPurInfo.aspx?patcode=" + Pactcode + "&sircode=" + rsircode;
                // hlink2.NavigateUrl = "~/F_01_LPA/EntryLandRegProcess.aspx";
            }
            else
            {
                //mdesc.Style.Add("display", "none");
                //hlink2.Style.Add("display", "none");
            }
        }
    }
}
