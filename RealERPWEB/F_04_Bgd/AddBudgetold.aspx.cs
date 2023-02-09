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
namespace RealERPWEB.F_04_Bgd
{
    public partial class AddBudgetold : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Additional Budget Information";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.GetProjectName();
                this.txtABDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string SrchSupplier = "%" + this.txtSearchPrj.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "PROJECTNAME", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string date = Convert.ToDateTime(this.txtABDate.Text).ToString("dd-MMM-yyyy");
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //DataSet ds1;

            //if (this.ddlPrevABList.Items.Count > 0)
            //{
            //   string mABNo = this.ddlPrevABList.SelectedValue.ToString();
            //   ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "RPTADBGGDINFO", mABNo, "",
            //                  "", "", "", "", "", "", "");
            //   if (ds1 == null)
            //       return;

            //   ViewState["tblstatus"]=ds1.Tables[0];
            //}
            //else
            //{
            //    return;
            //}

            //DataTable dt = (DataTable)ViewState["tblstatus"];
            //ReportDocument rrs2 = new RealERPRPT.R_04_Bgd.rptAddBgd();
            //TextObject rptCname = rrs2.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptPrjName = rrs2.ReportDefinition.ReportObjects["txtprojectname"] as TextObject;
            //rptPrjName.Text = "Project: " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(14);

            //TextObject rptABno = rrs2.ReportDefinition.ReportObjects["txtBgdNo"] as TextObject;
            //rptABno.Text = "Budget No. " + this.lblCurABNo1.Text + this.txtCurABNo2.Text;

            //TextObject txtABDate1 = rrs2.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtABDate1.Text = "Date: " + date;

            //TextObject txtuserinfo = rrs2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs2.SetDataSource((DataTable)ViewState["tblstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs2.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rrs2;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Addtional Budget";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}            
        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk.Text = "New";
                this.lblProjectName.Text = this.ddlProjectName.SelectedItem.Text.Trim();
                this.ddlProjectName.Visible = false;
                this.lblProjectName.Visible = true;
                this.txtABDate.Enabled = false;
                this.lblPre.Visible = false;
                this.txtSearchPreAb.Visible = false;
                this.imgbtnFindPreAb.Visible = false;
                this.ddlPrevABList.Visible = false;
                this.Get_AddBgd_Info();
            }
            else
            {
                this.lnkbtnOk.Text = "Ok";
                this.ddlProjectName.Visible = true;
                this.lblProjectName.Visible = false;
                this.txtABDate.Enabled = true;
                this.txtABDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.dgvAddBgd.DataSource = null;
                this.dgvAddBgd.DataBind();
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.lblPre.Visible = true;
                this.txtSearchPreAb.Visible = true;
                this.imgbtnFindPreAb.Visible = true;
                this.ddlPrevABList.Visible = true;
                this.ddlPrevABList.Items.Clear();
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Addtional Budget";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void GetAbNo()
        {
            string comcod = this.GetCompCode();
            string mABNO = "NEWAB";
            if (this.ddlPrevABList.Items.Count > 0)
                mABNO = this.ddlPrevABList.SelectedValue.ToString();

            string mABDAT = Convert.ToDateTime(this.txtABDate.Text).ToString("dd-MMM-yyyy");
            if (mABNO == "NEWAB")
            {
                DataSet ds2 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTABINFO", mABDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurABNo1.Text = ds2.Tables[0].Rows[0]["maxabno1"].ToString().Substring(0, 5);
                    this.txtCurABNo2.Text = ds2.Tables[0].Rows[0]["maxabno1"].ToString().Substring(5);

                    this.ddlPrevABList.DataTextField = "maxabno1";
                    this.ddlPrevABList.DataValueField = "maxabno";
                    this.ddlPrevABList.DataSource = ds2.Tables[0];
                    this.ddlPrevABList.DataBind();
                }
            }

        }
        //protected void imgbtnFindRequiSition_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.LoadData();
        //}

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        protected void Get_AddBgd_Info()
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            ViewState.Remove("tblorderno");
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtABDate.Text).ToString("dd-MMM-yyyy");
            string mABNo = "NEWAB";
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1;
            if (this.ddlPrevABList.Items.Count > 0)
            {
                mABNo = this.ddlPrevABList.SelectedValue.ToString();
                ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURADDBGDINFO", mABNo, CurDate1,
                              "", "", "", "", "", "", "");
            }
            else
            {
                ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETADDBUDGET", CurDate1, pactcode,
                              "", "", "", "", "", "", "");
            }
            if (ds1 == null)
                return;

            ViewState["tblstatus"] = this.HiddenSameData(ds1.Tables[0]);

            if (mABNo == "NEWAB")
            {
                ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTABINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurABNo1.Text = ds1.Tables[0].Rows[0]["maxabno1"].ToString().Substring(0, 5);
                    this.txtCurABNo2.Text = ds1.Tables[0].Rows[0]["maxabno1"].ToString().Substring(5);
                }
                this.Data_Bind();
                return;
            }


            this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblCurABNo1.Text = ds1.Tables[1].Rows[0]["abno1"].ToString().Substring(0, 5);
            this.txtCurABNo2.Text = ds1.Tables[1].Rows[0]["abno1"].ToString().Substring(5);
            this.txtABDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["abdat"]).ToString("dd-MMM-yyyy");
            this.lblProjectName.Text = (this.ddlProjectName.Items.Count == 0 ? "XXX" : this.ddlProjectName.SelectedItem.Text.Trim());
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            //string rescode = dt1.Rows[0]["rescode"].ToString();
            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["rescode"].ToString() == rescode)
            //    {
            //        rescode = dt1.Rows[j]["rescode"].ToString();
            //        dt1.Rows[j]["resdesc"] = "";
            //    }

            //    else
            //        rescode = dt1.Rows[j]["rescode"].ToString();
            //}

            return dt1;
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblstatus"];
            this.dgvAddBgd.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.dgvAddBgd.DataSource = dt;
            this.dgvAddBgd.DataBind();

            if (dt.Rows.Count > 0)
            {
                ((Label)this.dgvAddBgd.FooterRow.FindControl("lbgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?
                                        0 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgvAddBgd.FooterRow.FindControl("lbgvFPreBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(preproamt)", "")) ?
                                        0 : dt.Compute("sum(preproamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgvAddBgd.FooterRow.FindControl("lbgvFActamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(actamt)", "")) ?
                                       0 : dt.Compute("sum(actamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgvAddBgd.FooterRow.FindControl("gvlbFAvaamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avaamt)", "")) ?
                                        0 : dt.Compute("sum(avaamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgvAddBgd.FooterRow.FindControl("lbgvFApramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proamt)", "")) ?
                                       0 : dt.Compute("sum(proamt)", ""))).ToString("#,##0;(#,##0); ");
            }
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private void SaveValue()
        {
            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tblstatus"];
            for (int i = 0; i < this.dgvAddBgd.Rows.Count; i++)
            {
                //string ssircode = ((Label)this.gvProBill.Rows[i].FindControl("lblCodeo1")).Text.Trim();
                double proqty = Convert.ToDouble('0' + ((TextBox)this.dgvAddBgd.Rows[i].FindControl("txtProQty")).Text.Trim());
                double prorat = Convert.ToDouble('0' + ((Label)this.dgvAddBgd.Rows[i].FindControl("lblProRat")).Text.Trim());
                double proamt = Convert.ToDouble('0' + ((TextBox)this.dgvAddBgd.Rows[i].FindControl("txtAprAmt")).Text.Trim());

                double rate = (proqty <= 0.00) ? 0 : proamt / proqty;

                rowindex = (this.dgvAddBgd.PageSize * this.dgvAddBgd.PageIndex) + i;

                tblt02.Rows[rowindex]["proqty"] = proqty;
                tblt02.Rows[rowindex]["prorat"] = rate;
                tblt02.Rows[rowindex]["proamt"] = proamt;
            }
            ViewState["tblstatus"] = tblt02;


        }

        protected void imgbtnFindPrj_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void dgvAddBgd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.dgvAddBgd.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void imgbtnFindPreAb_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString() + "%";
            string CurDate1 = Convert.ToDateTime(this.txtABDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string abSearch = "%" + this.txtSearchPreAb.Text + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPREVABLIST", CurDate1,
                          pactcode, abSearch, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevABList.Items.Clear();
            this.ddlPrevABList.DataTextField = "abno1";
            this.ddlPrevABList.DataValueField = "abno";
            this.ddlPrevABList.DataSource = ds1.Tables[0];
            this.ddlPrevABList.DataBind();
        }
        protected void lbtnUpdateAdBgd_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.SaveValue();
            if (this.ddlPrevABList.Items.Count == 0)
                this.GetAbNo();
            string mABDAT = Convert.ToDateTime(this.txtABDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mABNO = this.lblCurABNo1.Text.Trim().Substring(0, 2) + mABDAT.Substring(7, 4) + this.lblCurABNo1.Text.Trim().Substring(2, 2) + this.txtCurABNo2.Text.Trim();

            ////Log Entry

            //DataTable dtuser = (DataTable)Session["tblUserReq"];
            //string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            //string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            //string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            //string tblPostedDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string userid = hst["usrid"].ToString();
            //string Terminal = hst["compname"].ToString();
            //string Sessionid = hst["session"].ToString();
            //string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string PostedByid = (this.Request.QueryString["InputType"] == "Entry") ? userid : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? userid
            //    : (this.Request.QueryString["InputType"] == "ReqEdit") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            //string Posttrmid = (this.Request.QueryString["InputType"] == "Entry") ? Terminal : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Terminal
            //    : (this.Request.QueryString["InputType"] == "ReqEdit") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            //string PostSession = (this.Request.QueryString["InputType"] == "Entry") ? Sessionid : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Sessionid
            //    : (this.Request.QueryString["InputType"] == "ReqEdit") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            //string PostedDat = (this.Request.QueryString["InputType"] == "Entry") ? Date : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Date
            //    : (this.Request.QueryString["InputType"] == "ReqEdit") ? Date : (tblPostedSession == "") ? Date : tblPostedDat;
            //string ApprovByid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : userid;
            //string approvdat = (this.Request.QueryString["InputType"] == "Entry") ? "01-Jan-1900" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "01-Jan-1900"
            //    : System.DateTime.Today.ToString("dd-MMM-yyyy");
            //string Approvtrmid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : Terminal;
            //string ApprovSession = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : Sessionid;


            //////


            string mPACTCODE = this.ddlProjectName.SelectedValue.ToString().Trim();

            bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATEADBGDINFO", "BGDADB", mABNO, mPACTCODE, mABDAT, "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = AccData.ErrorObject["Msg"].ToString();
                return;
            }
            DataTable tbl1 = (DataTable)ViewState["tblstatus"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mISIRCODE = tbl1.Rows[i]["isircode"].ToString();
                double mBGDQTY = Convert.ToDouble(tbl1.Rows[i]["bgdqty"]);
                double mBGDAMT = Convert.ToDouble(tbl1.Rows[i]["bgdamt"]);
                double mAVAQty = Convert.ToDouble(tbl1.Rows[i]["avaqty"]);
                double mAVAAMT = Convert.ToDouble(tbl1.Rows[i]["avaamt"]);
                double mPROQty = Convert.ToDouble(tbl1.Rows[i]["proqty"]);
                double mPROAMT = Convert.ToDouble(tbl1.Rows[i]["proamt"]);

                //if (mPROAMT > 0)
                //{

                result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATEADBGDINFO", "BGDADA",
                                mABNO, mISIRCODE, mBGDQTY.ToString(), mBGDAMT.ToString(), mAVAQty.ToString(), mAVAAMT.ToString(), mPROQty.ToString(), mPROAMT.ToString(), "",
                                "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = AccData.ErrorObject["Msg"].ToString();
                    return;
                }
                // }

            }

         //this.txtCurReqDate.Enabled = false;
         ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Update Reqisition";
                string eventdesc2 = "Req No- " + mABNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
    }
}