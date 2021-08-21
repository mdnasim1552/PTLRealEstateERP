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
namespace RealERPWEB.F_34_Mgt
{

    public partial class OtherReqEntry02 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.GetProjectName();

                this.ViewComponent();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void ViewComponent()
        {
            if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "OreqAcc"))
            {

                this.lblCurReqNo1.Visible = false;
                this.txtCurReqNo2.Visible = false;
                this.lblmrfno.Visible = false;
                this.txtMRFNo.Visible = false;
            }


        }
        protected void ImgbtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string SearchProject = "%" + this.txtProSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "PROJECTNAME", SearchProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }

        private void GetMaterial()
        {
            string comcod = this.GetCompCode();
            string project = this.ddlProjectName.SelectedValue.ToString();
            string txtfindMat = this.txtProSearch.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETGRPMATERIAL", project, CurDate1, txtfindMat, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["getMat"] = ds1.Tables[0];
            this.ddlResList.DataTextField = "sirdesc";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.txtSrchMrfNo.Visible = true;
                this.lbtnPrevReqList.Visible = true;
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
                this.ddlProjectName.Enabled = true;
                this.lblCurReqNo1.Text = "REQ" + DateTime.Today.ToString("MM") + "-";
                this.txtCurReqDate.Enabled = true;
                this.txtMRFNo.Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.txtProSearch.Text = "";
                this.ddlResList.Items.Clear();
                this.txtReqNarr.Text = "";
                this.gvOtherReq.DataSource = null;
                this.gvOtherReq.DataBind();
                this.PnlDetails.Visible = false;
                this.PnlNarration.Visible = false;
                this.lbtnOk.Text = "Ok";
                if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "OreqAcc"))
                {

                    this.lblCurReqNo1.Visible = false;
                    this.txtCurReqNo2.Visible = false;
                    this.lblmrfno.Visible = false;
                    this.txtMRFNo.Visible = false;
                }

                return;
            }


            if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "OreqAcc"))
            {

                this.lblCurReqNo1.Visible = false;
                this.txtCurReqNo2.Visible = false;
                this.lblmrfno.Visible = false;
                this.txtMRFNo.Visible = false;
            }

            this.lblmrfno.Visible = true;
            this.txtMRFNo.Visible = true;
            this.lblCurReqNo1.Visible = true;
            this.txtCurReqNo2.Visible = true;
            this.txtSrchMrfNo.Visible = false;
            this.lbtnPrevReqList.Visible = false;
            this.ddlPrevReqList.Visible = false;
            this.txtCurReqNo2.ReadOnly = true;
            this.PnlDetails.Visible = true;
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Requisition_Info();
        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void GetReqNo()
        {
            string comcod = this.GetCompCode();
            string mREQNO = "NEWREQ";
            if (this.ddlPrevReqList.Items.Count > 0)
                mREQNO = this.ddlPrevReqList.SelectedValue.ToString();

            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            if (mREQNO == "NEWREQ")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTREQINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxreqno"].ToString();
                    this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);

                    this.ddlPrevReqList.DataTextField = "maxreqno1";
                    this.ddlPrevReqList.DataValueField = "maxreqno";
                    this.ddlPrevReqList.DataSource = ds2.Tables[0];
                    this.ddlPrevReqList.DataBind();
                }
            }

        }

        protected void lbtnPrevReqList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string prjcode = this.ddlProjectName.SelectedValue.ToString() + "%";
            string mrfno = this.txtSrchMrfNo.Text.Trim() + "%";
            string Module = (Request.QueryString["Type"].ToString() == "OreqAcc") ? "Acc" : "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPREVREQLIST", CurDate1,
                          prjcode, Module, mrfno, "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevReqList.Items.Clear();
            this.ddlPrevReqList.DataTextField = "reqno1";
            this.ddlPrevReqList.DataValueField = "reqno";
            this.ddlPrevReqList.DataSource = ds1.Tables[0];
            this.ddlPrevReqList.DataBind();

        }
        protected void Get_Requisition_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mReqNo = "NEWREQ";
            if (this.ddlPrevReqList.Items.Count > 0)
            {
                this.txtCurReqDate.Enabled = false;
                mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblReq"] = ds1.Tables[0];
            Session["tblUserReq"] = ds1.Tables[1];

            if (Request.QueryString["Type"].ToString() == "OreqApproved")
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.GetApprQty();
                }
            }
            if (mReqNo == "NEWREQ")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurReqNo1.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);
                }

                //string pactcode = this.ddlProjectName.SelectedValue.ToString();
                //DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETREQBGDBAL", pactcode, CurDate1, "", "", "", "", "","","");
                //Session["tblReq"] = ds2.Tables[0];
                //if (ds2 == null)
                //    return;
                //ds2.Dispose();
                //this.gvOtherReq_DataBind();
                return;
            }
            this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            this.lblCurReqNo1.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(0, 6);
            this.txtCurReqNo2.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(6, 5);
            this.txtCurReqDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString("dd.MM.yyyy");
            this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            this.gvOtherReq_DataBind();
        }

        private void GetApprQty()
        {
            DataTable dt = (DataTable)Session["tblReq"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double proamt = Convert.ToDouble(dt.Rows[i]["proamt"]);
                double appamt = Convert.ToDouble(dt.Rows[i]["appamt"]);
                if (appamt == 0)
                    dt.Rows[i]["appamt"] = proamt;

            }
            Session["tblReq"] = dt;


        }


        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)Session["tblReq"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["sirdesc"] = this.ddlResList.SelectedItem.Text.Trim();
                DataTable tbl2 = (DataTable)Session["getMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                dr1["bgdamt"] = dr3[0]["bgdamt"];
                dr1["trnamt"] = dr3[0]["trnamt"];
                dr1["balamt"] = dr3[0]["balamt"];
                dr1["proamt"] = 0;
                dr1["adjamt"] = 0;
                tbl1.Rows.Add(dr1);
            }
            Session["tblReq"] = tbl1;
            this.gvOtherReq_DataBind();
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            //string payto = "Pay To: " + this.txtPayto.Text.Trim().ToString();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string paytype = "Pay Type: " + this.rblpaytype.SelectedItem.Text.ToString();
            string date = "Date : " + this.txtCurReqDate.Text.ToString().Trim();
            string refno = "Ref No : " + this.txtMRFNo.Text.ToString().Trim();
            string reqno = "Requisition No : " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            string narration = "Narration:" + this.txtReqNarr.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                 "", "", "", "", "", "", "");

            DataTable dtsign = ds1.Tables[2];
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["tblReq"];

            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_34_Mgt.EClassOtherReq>();
            string requsinput = dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();
            // string confirmby = dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString()
            string confirmby = dtsign.Rows[0]["reqanam"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();
            string approved = dtsign.Rows[0]["faprovnam"].ToString() + "\n" + dtsign.Rows[0]["fapprvdat"].ToString();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.RptOtherReqStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Work Order"));
            Rpt1.SetParameters(new ReportParameter("paytype", ""));
            Rpt1.SetParameters(new ReportParameter("payto", ""));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("refno", refno));
            Rpt1.SetParameters(new ReportParameter("reqno", reqno));
            Rpt1.SetParameters(new ReportParameter("narration", narration));
            Rpt1.SetParameters(new ReportParameter("requsinput", requsinput));
            Rpt1.SetParameters(new ReportParameter("confirmby", confirmby));
            Rpt1.SetParameters(new ReportParameter("approved", approved));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Other Req Entry";
                string eventdesc = "Print Report";
                string eventdesc2 = "Requisition No: " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string usrid = hst["usrid"].ToString();
            //string sessionid = hst["session"].ToString();
            //string trmid = hst["compname"].ToString();
            this.Session_tblReq_Update();
            string comcod = this.GetCompCode();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevReqList.Items.Count == 0)
                this.GetReqNo();
            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            // Duplicate MRF


            if (mMRFNO.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "M.R.F No. Should Not Be Empty";
                return;
            }

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "CHECKEDDUPMRRNO", mMRFNO, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
                ;


            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("reqno <>'" + mREQNO + "'");
                DataTable dt = dv1.ToTable();
                if (dt.Rows.Count == 0)
                    ;
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate M.R.F No";
                    //this.ddlPrevReqList.Items.Clear();
                    return;
                }
            }






            //log Report
            DataTable dtuser = (DataTable)Session["tblUserReq"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblApprovByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string tblApprovDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["aprvdat"]).ToString("dd-MMM-yyyy");
            string tblApprovtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            string tblApprovSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Type"] == "OreqEntry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;

            string Posttrmid = (this.Request.QueryString["Type"] == "OreqEntry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;

            string PostSession = (this.Request.QueryString["Type"] == "OreqEntry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;

            string ApprovByid = (this.Request.QueryString["Type"] == "OreqEntry") ? "" : (tblApprovByid == "") ? userid : tblApprovByid;
            string approvdat = (this.Request.QueryString["Type"] == "OreqEntry") ? "01-Jan-1900" : (tblApprovDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblApprovDat;
            string Approvtrmid = (this.Request.QueryString["Type"] == "OreqEntry") ? "" : (tblApprovtrmid == "") ? Terminal : tblApprovtrmid;
            string ApprovSession = (this.Request.QueryString["Type"] == "OreqEntry") ? "" : (tblApprovSession == "") ? Sessionid : tblApprovSession;
            /////log end
            string mPACTCODE = this.ddlProjectName.SelectedValue.ToString().Trim();
            string nARRATION = txtReqNarr.Text;

            DataTable tbl1 = (DataTable)Session["tblReq"];
            bool result = true;
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                double mProAMT = Convert.ToDouble(tbl1.Rows[i]["proamt"]);
                string mAdjAMT = Convert.ToDouble(tbl1.Rows[i]["adjamt"]).ToString();


                if (mProAMT > 0)
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTOTHERREQ",
                             mREQNO, mPACTCODE, mRSIRCODE, mREQDAT, mMRFNO, mProAMT.ToString(), mAdjAMT, nARRATION,
                             PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }

            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Other Req Entry";
                string eventdesc = "Update Req";
                string eventdesc2 = mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvOtherReq_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblReq"];
            this.gvOtherReq.DataSource = tbl1;
            this.gvOtherReq.DataBind();
            this.FooterVallue();
        }


        protected void FooterVallue()
        {
            DataTable tbl1 = (DataTable)Session["tblReq"];
            if (tbl1.Rows.Count == 0)
                return;
            ((LinkButton)this.gvOtherReq.FooterRow.FindControl("lbtnUpdateResReq")).Visible = (Request.QueryString["Type"].ToString() == "OreqAcc") ? false : true;
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFBgdamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(bgdamt)", "")) ?
                0.00 : tbl1.Compute("Sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(trnamt)", "")) ?
                0.00 : tbl1.Compute("Sum(trnamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFBalamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(balamt)", "")) ?
                0.00 : tbl1.Compute("Sum(balamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFProposedamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(proamt)", "")) ?
                0.00 : tbl1.Compute("Sum(proamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFadjustamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(adjamt)", "")) ?
             0.00 : tbl1.Compute("Sum(adjamt)", ""))).ToString("#,##0;(#,##0); ");

        }


        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblReq"];
            // int TblRowIndex2;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            for (int i = 0; i < this.gvOtherReq.Rows.Count; i++)
            {
                //TblRowIndex2 = (this.gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + i;
                double Proamt = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvProposedamt")).Text.Trim());
                double adjamt = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvadjustamt")).Text.Trim());
                tbl1.Rows[i]["proamt"] = Proamt;
                tbl1.Rows[i]["adjamt"] = adjamt;
            }
            Session["tblReq"] = tbl1;
        }


        protected void ImgbtnFindRes_Click(object sender, ImageClickEventArgs e)
        {
            this.GetMaterial();

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            this.gvOtherReq_DataBind();

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Type = Request.QueryString["Type"].ToString();
            if (Type == "OreqEntry")
            {
                this.ddlPrevReqList.Items.Clear();
            }
            else
            {
                this.lbtnPrevReqList_Click(null, null);

            }
        }

        protected void gvOtherReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblReq"];
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string reqno = "REQ" + this.txtCurReqDate.Text.Substring(6, 4) + this.lblCurReqNo1.Text.Substring(3, 2) + this.txtCurReqNo2.Text.ToString();//((Label)this.gvOtherReq.Rows[e.RowIndex].FindControl("lblgvreqf")).Text.Trim();
            string rsircode = ((Label)this.gvOtherReq.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEREQRES", reqno, pactcode, rsircode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                //this.Get_Requisition_Info();
                int rowindex = (this.gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }
            DataView dv = dt.DefaultView;
            Session.Remove("tblReq");
            Session["tblReq"] = dv.ToTable();
            this.gvOtherReq_DataBind();
        }

    }
}
