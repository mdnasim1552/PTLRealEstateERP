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
    public partial class AddBudget : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "Entry") ? "Additional Budget" : "Addtional Budget Approval";
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtABDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetLabCode();
                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.imgbtnFindPreAb_Click(null, null);
                    this.lnkbtnOk_Click(null, null);
                }


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
            string Srchproject = "%" + this.txtSearchPrj.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "GETPROJECTNAME", Srchproject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            if (this.Request.QueryString["prjcode"].Length > 0)
            {
                this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"];
            }

            ds1.Dispose();
        }

        private void GetLabCode()
        {

            try
            {

                Session.Remove("tblwork");
                string comcod = this.GetCompCode();
                string Srchproject = "%%";
                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "GETWORKCODE", Srchproject, "", "", "", "", "", "", "", "");
                this.ddlItem.DataTextField = "sirdesc";
                this.ddlItem.DataValueField = "sircode";
                this.ddlItem.DataSource = ds1.Tables[0];
                this.DataBind();
                Session["tblwork"] = ds1.Tables[0];
                ds1.Dispose();


            }
            catch (Exception ex)
            {



            }

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
            //    string mABNo = this.ddlPrevABList.SelectedValue.ToString();
            //    ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "RPTADBGGDINFO", mABNo, "",
            //                   "", "", "", "", "", "", "");
            //    if (ds1 == null)
            //        return;

            //    ViewState["tblstatus"] = ds1.Tables[0];
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
                this.imgbtnFindPreAb.Visible = false;
                this.ddlPrevABList.Visible = false;
                this.PnlNarration.Visible = true;
                this.pnlItem.Visible = true;
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
                this.imgbtnFindPreAb.Visible = true;
                this.ddlPrevABList.Visible = true;
                this.ddlPrevABList.Items.Clear();
                this.PnlNarration.Visible = false;
                this.pnlItem.Visible = false;
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
                DataSet ds2 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "GETLASTABINFO", mABDAT,
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
            //this.lblPage.Visible = true;
            //this.ddlpagesize.Visible = true;
            ViewState.Remove("tblorderno");
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtABDate.Text).ToString("dd-MMM-yyyy");
            string mABNo = "NEWAB";
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1;
            if (this.ddlPrevABList.Items.Count > 0)
            {
                mABNo = this.ddlPrevABList.SelectedValue.ToString();
                this.txtABDate.Enabled = false;
                string type = this.Request.QueryString["Type"].ToString();
                ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "GETPURADDBGDINFO", mABNo, CurDate1,
                              type, "", "", "", "", "", "");
            }
            else
            {
                ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "GETADDBUDGET", CurDate1, pactcode,
                              "", "", "", "", "", "", "");
            }
            if (ds1 == null)
                return;

            ViewState["tblstatus"] = this.HiddenSameData(ds1.Tables[0]);

            if (mABNo == "NEWAB")
            {
                ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "GETLASTABINFO", CurDate1, "", "", "", "", "", "", "", "");
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
            this.txtNarration.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();


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

                ((LinkButton)this.dgvAddBgd.FooterRow.FindControl("lbtnApproved")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "Mgt");

                ((Label)this.dgvAddBgd.FooterRow.FindControl("lbgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?
                                        0 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");


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
                double proqty = ASTUtility.StrPosOrNagative(((TextBox)this.dgvAddBgd.Rows[i].FindControl("txtProQty")).Text.Trim());
                double bgdrate = Convert.ToDouble('0' + ((Label)this.dgvAddBgd.Rows[i].FindControl("lblBgdRat")).Text.Trim());
                // double proamt = Convert.ToDouble('0' + ((TextBox)this.dgvAddBgd.Rows[i].FindControl("txtAprAmt")).Text.Trim());

                double proamt = proqty * bgdrate;

                rowindex = (this.dgvAddBgd.PageSize * this.dgvAddBgd.PageIndex) + i;

                tblt02.Rows[rowindex]["proqty"] = proqty;
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
            //string pactcode = this.ddlProjectName.SelectedValue.ToString() + "%";
            string CurDate1 = Convert.ToDateTime(this.txtABDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string abSearch = "%%";
            if (this.Request.QueryString["prjcode"].Length > 0)
            {
                abSearch = this.Request.QueryString["prjcode"].ToString() + "%";
            }


            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "GETPREVABLIST", CurDate1,
                           abSearch, "", "", "", "", "", "", "");
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

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
            string rmrks = this.txtNarration.Text.Trim();

            bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "UPDATEADBGDINFO", "BGDADB", mABNO, mPACTCODE, mABDAT, rmrks, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = AccData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            DataTable tbl1 = (DataTable)ViewState["tblstatus"];
            foreach (DataRow dr in tbl1.Rows)
            {
                string mISIRCODE = dr["isircode"].ToString();
                string flrcod = dr["flrcod"].ToString();
                double mBGDQTY = Convert.ToDouble(dr["bgdqty"]);
                double mBGDAMT = Convert.ToDouble(dr["bgdamt"]);

                double mPROQty = Convert.ToDouble(dr["proqty"]);
                double mPROAMT = Convert.ToDouble(dr["proamt"]);

                //if (mPROAMT > 0)
                //{

                result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "UPDATEADBGDINFO", "BGDADA",
                                mABNO, mISIRCODE, flrcod, mBGDQTY.ToString(), mBGDAMT.ToString(), mPROQty.ToString(), mPROAMT.ToString(), "",
                                "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = AccData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                // }

            }

         //this.txtCurReqDate.Enabled = false;
         ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

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


        protected void lbtnApproved_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string approved = "Ok";


            string comcod = this.GetCompCode();
            string mABDAT = Convert.ToDateTime(this.txtABDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mABNO = this.lblCurABNo1.Text.Trim().Substring(0, 2) + mABDAT.Substring(7, 4) + this.lblCurABNo1.Text.Trim().Substring(2, 2) + this.txtCurABNo2.Text.Trim();
            string mPACTCODE = this.ddlProjectName.SelectedValue.ToString().Trim();

            bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "UPDATEAPPROVEDUSER", mABNO, usrid, Date, trmid, sessionid, approved, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = AccData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            DataTable tbl1 = (DataTable)ViewState["tblstatus"];
            foreach (DataRow dr in tbl1.Rows)
            {
                string mISIRCODE = dr["isircode"].ToString();
                string flrcod = dr["flrcod"].ToString();
                //double bgdqty = Convert.ToDouble(dr["bgdwqty"]);
                //double mPROQty = Convert.ToDouble(dr["proqty"]);
                double toqty = Convert.ToDouble(dr["bgdqty"]) + Convert.ToDouble(dr["proqty"]);

                //if (mPROAMT > 0)
                //{

                result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ADDBUDGET", "UPDATEBUDGETINFO",
                                mPACTCODE, mISIRCODE, flrcod, toqty.ToString(), "", "", "", "",
                                "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = AccData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                // }

            }

        //this.txtCurReqDate.Enabled = false;
        ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Update Reqisition";
                string eventdesc2 = "Req No- " + mABNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void CreateTable()
        {


            DataTable dt = (DataTable)Session["tblflrinf"];


            if (dt == null)
            {
                DataTable tblt01 = new DataTable();
                tblt01.Columns.Add("flrcod", Type.GetType("System.String"));
                tblt01.Columns.Add("flrdes", Type.GetType("System.String"));

            }




            Session["tblflrinf"] = dt;


        }

        protected void lbtnselect_Click(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)ViewState["tblstatus"]).Copy();
            this.CreateTable();

            DataTable dt1 = (DataTable)Session["tblflrinf"];
            dt1 = dt.DefaultView.ToTable(true, "flrcod", "flrdes");
            dt1.DefaultView.Sort = ("flrcod asc");
            //Session.Remove("tblwork");


            string isircode = this.ddlItem.SelectedValue.ToString();
            string isirdesc = this.ddlItem.SelectedItem.Text.Trim();
            string sirunit = ((DataTable)Session["tblwork"]).Select("sircode='" + isircode + "'")[0]["sirunit"].ToString();
            string bgdrat = Convert.ToDouble(((DataTable)Session["tblwork"]).Select("sircode='" + isircode + "'")[0]["bgdrat"]).ToString();
            string flrcod = "";
            foreach (DataRow dr1 in dt1.Rows)
            {
                flrcod = dr1["flrcod"].ToString();
                DataRow[] drs = dt.Select("isircode='" + isircode + "' and  flrcod='" + flrcod + "'");
                if (drs.Length == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["isircode"] = isircode;
                    dr["isirdesc"] = isirdesc;
                    dr["flrcod"] = flrcod;
                    dr["flrdes"] = dr1["flrdes"];
                    dr["bgdqty"] = 0.00;
                    dr["bgdrat"] = bgdrat;
                    dr["bgdamt"] = 0.00;
                    dr["proamt"] = 0.00;
                    dr["proqty"] = 0.00;
                    dt.Rows.Add(dr);
                }

            }


            ViewState["tblstatus"] = dt;
            this.Data_Bind();

            //var dr = ( from row in dt.AsEnumerable()
            //   select  flrcod=row.Field<string>("flrcod"), 
            //          flrdes=row.Field<string>("flrdes")).Distinct();







            //var dr =  dt.AsEnumerable()
            //            .Select(row => new {
            //                flrcod = row.Field<string>("flrcod"),
            //                flrdes = row.Field<string>("flrdes"),
            //                Address = row.Field<string>("Address")
            //            }).Distinct();





            //DataTable dt1=dr.ToString
            //EnumerableRowCollection<DataRow> item = (from r in dt.AsEnumerable()
            //                                         where r.Field<string>("actdesc1").ToUpper().Contains(srchfrmproject.ToUpper())
            //                                         select r);
            //dt = item.AsDataView().ToTable();









        }
    }
}