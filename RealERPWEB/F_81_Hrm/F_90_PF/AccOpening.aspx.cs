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
namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class AccOpening : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            if (this.dgv2.Rows.Count == 0)
            {
                Session.Remove("AccTbl01");
                this.GridLoad();
                this.DateSet();
                this.pnlsub.Visible = false;
            }
        }
        private void DateSet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string datepart;
            DataSet ds4 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            if (dt4.Rows.Count == 0)
            {
                datepart = "";
            }
            else
            {
                datepart = Convert.ToDateTime(dt4.Rows[0]["voudat"]).ToString("dd-MMM-yyyy ddd");
            }
            if (datepart == "")
            {
                this.txtdate.Text = datepart.ToString();
                this.txtdate.Enabled = true;
            }
            else
            {
                this.txtdate.Text = datepart;
                this.txtdate.Enabled = false;
            }
        }
        private void GridLoad()
        {
            Session.Remove("AccTbl01");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = "%" + this.txtFilter.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "PFGETOPENINGACC", filter, "", "", "", "", "", "", "", "");
            Session["AccTbl01"] = ds1.Tables[0];
            this.dgv2_DataBind();
            this.TotalCalculation1();
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            this.GridLoad();

        }
        private void SessionUpdate()
        {

            DataTable tblt01 = (DataTable)Session["AccTbl01"];
            int TblRowIndex;

            for (int i = 0; i < this.dgv2.Rows.Count; i++)
            {
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                //string dgvTrnRemarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                TblRowIndex = (dgv2.PageIndex) * dgv2.PageSize + i;

                tblt01.Rows[TblRowIndex]["Dr"] = dgvTrnDrAmt;
                tblt01.Rows[TblRowIndex]["Cr"] = dgvTrnCrAmt;
                //  tblt01.Rows[TblRowIndex]["Remarks"] = dgvTrnRemarks;
            }
            Session["AccTbl01"] = tblt01;
        }
        private void SessionUpdate2()
        {

            DataTable tblt02 = (DataTable)Session["AccTbl02"];
            int TblRowIndex2;

            for (int j = 0; j < this.dgv3.Rows.Count; j++)
            {
                // double dgvTrnRate;
                double dgvTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtQty")).Text.Trim()));
                // double dgvTrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtRate")).Text.Trim()));
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text.Trim()));
                double dgvTrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("txtgvRate")).Text.Trim()));
                double dgvcompamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxcompamt")).Text.Trim()));


                //if (dgvTrnDrAmt == 0 && dgvTrnCrAmt == 0)
                //{
                //    dgvTrnRate = 0;
                //}
                //else
                //{
                //    dgvTrnRate = (dgvTrnQty == 0 ? 0.00 : (dgvTrnDrAmt + dgvTrnCrAmt) / dgvTrnQty);
                //}

                dgvTrnDrAmt = dgvTrnRate > 0 ? dgvTrnQty * dgvTrnRate : dgvTrnDrAmt;
                // dgvTrnCrAmt = dgvTrnRate > 0 ? dgvTrnQty * dgvTrnRate : dgvTrnCrAmt;

                ((TextBox)this.dgv3.Rows[j].FindControl("txtgvRate")).Text = dgvTrnRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text = dgvTrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text = dgvTrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxcompamt")).Text = dgvcompamt.ToString("#,##0.00;(#,##0.00); ");
                TblRowIndex2 = (dgv3.PageIndex) * dgv3.PageSize + j;
                tblt02.Rows[TblRowIndex2]["qty"] = dgvTrnQty;
                tblt02.Rows[TblRowIndex2]["rate"] = dgvTrnRate;
                tblt02.Rows[TblRowIndex2]["Dr"] = dgvTrnDrAmt;
                tblt02.Rows[TblRowIndex2]["Cr"] = dgvTrnCrAmt;
                tblt02.Rows[TblRowIndex2]["compamt"] = dgvcompamt;

            }
            Session["AccTbl02"] = tblt02;

            this.dgv3_DataBind();
        }
        protected void dgv2_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "this.className='normalrow'");

                //e.Row.Attributes.Add("onmouseover", "this.className='highlightrow'");
                //e.Row.Attributes.Add("onmouseout", "this.className='normalrow'");
            }

        }
        protected void gvlnkLevel_Click(object sender, EventArgs e)
        {
            Session.Remove("AccTbl02");
            this.SessionUpdate();
        }
        private void ShowResource()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string acccode01 = this.txtActcode.Text.Trim().Substring(0, 12);
            string filter2 = "%" + this.txtResSearch.Text.Trim() + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGRES", filter2, acccode01, "", "", "", "", "", "", "");
            Session["AccTbl02"] = this.HiddenSameData(ds2.Tables[0]);
            this.dgv3_DataBind();
            this.dgv2.Visible = false;
            this.pnlsub.Visible = true;
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rescode = dt1.Rows[0]["rescode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rescode"].ToString() == rescode)
                    dt1.Rows[j]["resdesc"] = "";

                rescode = dt1.Rows[j]["rescode"].ToString();
            }
            return dt1;

        }

        protected void gvlnkFTotal_Click(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        private void TotalCalculation2()
        {
            this.SessionUpdate2();

        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            this.ShowResource();
        }
        protected void dgv2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session.Remove("RowIndex");
            if (e.CommandName == "")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                if (row.RowType.ToString() == "DataRow")
                {
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    int RowIndex = gvr.RowIndex;
                    Session["RowIndex"] = RowIndex;
                    this.lblacccode1.Visible = false;
                    this.txtFilter.Visible = false;
                    this.ImageButton1.Visible = false;
                    this.mainDDlPage.Visible = false;

                    
                    this.ShowActCode();
                    gvr.BackColor = System.Drawing.Color.Blue;

                }
            }
        }
        private void ShowActCode()
        {
            int rowin = (int)Session["RowIndex"];
            int rowin1 = (dgv2.PageIndex * dgv2.PageSize) + rowin;
            this.txtActcode.Text = ((Label)this.dgv2.Rows[rowin].FindControl("lblAccdesc")).Text;
            this.ShowResource();
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {

            this.GridLoad();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.pnlsub.Visible = false;
            this.dgv2.Visible = true;
            this.lblacccode1.Visible = true;
            this.txtFilter.Visible = true;
            this.ImageButton1.Visible = true;
            this.mainDDlPage.Visible = true;

            //int Rowindex1 = (int)Session["RowIndex"];
            //int Rowindex2 = (dgv2.PageSize * dgv2.PageIndex) + Rowindex1;
            //DataTable tblt03 = (DataTable)Session["AccTbl01"];
            //DataTable tblt03d = (DataTable)Session["AccTbl02"];
            //double Dramt = Convert.ToDouble((Convert.IsDBNull(tblt03d.Compute("Sum(dr)", "")) ? 0.00 : tblt03d.Compute("Sum(dr)", "")));
            //double Cramt = Convert.ToDouble((Convert.IsDBNull(tblt03d.Compute("Sum(cr)", "")) ? 0.00 : tblt03d.Compute("Sum(cr)", "")));
            // //    double Cramt= Convert.ToDouble("0" + ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftCramt")).Text);
            //double Amt=(Dramt - Cramt);
            //tblt03.Rows[Rowindex2]["Dr"] = (Amt > 0 ? Amt : 0);
            //tblt03.Rows[Rowindex2]["Cr"] = (Amt < 0 ? (Amt * (-1)) : 0);

            //Session["AccTbl01"] = tblt03;
            //this.dgv2_DataBind();
            //this.Panel2.Visible = false;


            //this.TotalCalculation1();

        }
        private void UpdateTable02()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SessionUpdate2();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UserId = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string EditDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string vounum = GetVou();
            string actcode = this.txtActcode.Text.Trim().Substring(0, 12);
            string cactcode = "000000000000";

            string vtcode = "00";
            string voudat = this.txtdate.Text.Substring(0, 11);
            DataTable tblt03 = (DataTable)Session["AccTbl02"];

            for (int i = 0; i < tblt03.Rows.Count; i++)
            {
                string rescode = tblt03.Rows[i]["rescode"].ToString();
                string spcfcod = tblt03.Rows[i]["spcfcod"].ToString();
                string trnqty = tblt03.Rows[i]["qty"].ToString();
                double Dramt = Convert.ToDouble(tblt03.Rows[i]["Dr"]);
                double Cramt = Convert.ToDouble(tblt03.Rows[i]["Cr"]);
                string trnamt = Convert.ToString(Dramt - Cramt);
                string trnremark = Convert.ToDouble(Dramt).ToString();
                double compamt = Convert.ToDouble(tblt03.Rows[i]["compamt"]);

                
                //if ((Dramt - Cramt) != 0)
                //{
                bool resulta = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "PFACVOPNUPDATEA", vounum, actcode,
                            rescode, cactcode, voudat, trnqty, trnremark, vtcode, trnamt, spcfcod, UserId, EditDate, Terminal, compamt.ToString(), "");
                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //}     
            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Opening";
                string eventdesc = "Update Resourc Balance";
                string eventdesc2 = vounum;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private string GetVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtdate.Text.Substring(0, 11);
            DataSet ds4 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "GETPFOPNVOUCHER", date1, "", "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            string ss = dt4.Rows[0]["couvounum"].ToString();
            //string dd = ss.Substring(12);
            if (ss.Substring(12) == "00")
            {
                string vounum = ss.Substring(0, 12) + "01";
                //-----------Update Transaction B Table-----------------//
                try
                {
                    bool resultb = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "PFACOPNUPDATE", vounum, date1,
                                "", "", "", "", "Accounts Opening", "00", "", "", "", "", "", "", "");
                    if (!resultb)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //return ;
                    }
                    ss = vounum;
                }
                catch (Exception e)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + e.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
                //-------------------------------------------------------//

            }
            return ss;
        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            //---------------Check Dr. and Cr------//
            this.TotalCalculation1();
            DataTable tblt07 = (DataTable)Session["AccTbl01"];
            for (int i = 0; i < tblt07.Rows.Count; i++)
            {
                double Dramt01 = Convert.ToDouble(tblt07.Rows[i]["Dr"]);
                double Cramt01 = Convert.ToDouble(tblt07.Rows[i]["Cr"]);
                if (Dramt01 > 0 && Cramt01 > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Choose Only Dr. Or Cr. Amount.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //else
                //{


                //}
            }
            //-------------------------//
            //this.TotalCalculation1();
            this.UpdateTable01();

        }
        private void UpdateTable01()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string comcod = hst["comcod"].ToString();
                string UserId = hst["usrid"].ToString();
                string EditDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string Terminal = hst["compname"].ToString();
                string vounum1 = GetVou();
                string cactcode = "000000000000";
                string spclcode = "000000000000";
                string rescode = "000000000000";
                string vtcode = "00";
                string trnqty = "0";

                string voudat = this.txtdate.Text.Substring(0, 11);
                DataTable tblt05 = (DataTable)Session["AccTbl01"];
                for (int i = 0; i < tblt05.Rows.Count; i++)
                {
                    string actcode = tblt05.Rows[i]["actcode"].ToString();
                    string actlev = tblt05.Rows[i]["actelev"].ToString();
                    double Dramt = Convert.ToDouble(tblt05.Rows[i]["Dr"]);
                    double Cramt = Convert.ToDouble(tblt05.Rows[i]["Cr"]);
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = Convert.ToDouble(Dramt).ToString();
                    //if ((Dramt - Cramt) != 0 && actlev != "2")
                    if (actlev != "2")
                    {
                        bool resulta = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER",
                                "PFACVOPNUPDATEA", vounum1, actcode, rescode, cactcode, voudat, trnqty,
                                trnremarks, vtcode, trnamt, spclcode, UserId, EditDate, Terminal, "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                    }
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Account Opening";
                    string eventdesc = "Update Balance";
                    string eventdesc2 = vounum1;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception e)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + e.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        protected void LnkfTotal_Click(object sender, EventArgs e)
        {
            this.TotalCalculation1();
        }
        private void TotalCalculation1()
        {
            this.SessionUpdate();
            DataTable tblt06 = (DataTable)Session["AccTbl01"];
            if (tblt06.Rows.Count == 0)
                return;
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(Dr)", "")) ?
            0.00 : tblt06.Compute("Sum(Dr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(Cr)", "")) ?
            0.00 : tblt06.Compute("Sum(Cr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }


        protected void dgv2ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate();
            this.dgv2.PageSize = Convert.ToInt32(this.dgv2ddlPageNo.SelectedValue.ToString());
            this.dgv2_DataBind();
            this.TotalCalculation1();


        }
        protected void dgv2_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["AccTbl01"];
            this.dgv2.PageSize = Convert.ToInt32(this.dgv2ddlPageNo.SelectedValue.ToString());

            this.dgv2.DataSource = tbl1;
            this.dgv2.DataBind();
            if (tbl1.Rows.Count == 0)
                return;

            //((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Visible = false;
            //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.dgv2.PageSize);
            //((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Items.Clear();
            //for (int i = 1; i <= TotalPage; i++)
            //    ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            //if (TotalPage > 1)
            //    ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Visible = true;
            //((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).SelectedIndex = this.dgv2.PageIndex;
        }

        // protected void dgv3ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.SessionUpdate2();

        //    this.dgv3.PageIndex = ((DropDownList)this.dgv3.FooterRow.FindControl("dgv3ddlPageNo")).SelectedIndex;
        //    //((DropDownList)this.dgv3.FooterRow.FindControl("dgv3ddlPageNo")).SelectedIndex
        //    this.dgv3_DataBind();
        //    this.TotalCalculation2();
        //}

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

            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftcompamt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(compamt)", "")) ?
           0.00 : tblt03.Compute("Sum(compamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");

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
        protected void lnkbtnUpdateRes_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.UpdateTable02();

        }
    }
}
