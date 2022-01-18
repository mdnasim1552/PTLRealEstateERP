﻿using System;
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
using System.Net;
using RealEntity;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_12_Inv
{
    public partial class PurReqEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        UserManGenAccount objuserman = new UserManGenAccount();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtReqText.Enabled = false;
                this.ImgbtnReqse.Enabled = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];

                //this.CompBudgetexceed();
                this.chkdupMRF.Enabled = false;
                this.chkdupMRF.Checked = true;
                this.chkneBudget.Enabled = false;
                this.chkneBudget.Checked = true;
                //this.DupMRR();
                this.Load_Project_Combo();

                this.VisibleGrid();
                this.Comnamemrfno();
                this.lbtnOk.Text = "New";
                this.lbtnOk_Click(null, null);

                if ((this.Request.QueryString["genno"].ToString().Length > 0))
                {
                    this.ImgbtnFindReq_Click(null, null);
                    this.lbtnOk_Click(null, null);

                }

                string reqcheckorApproved = this.GetReqcheckorApproved();
                string title = (Request.QueryString["InputType"].ToString() == "Entry") ? "Materials Requisition"
                     : (Request.QueryString["InputType"].ToString() == "Approval") ? "Materials Requisition Approval Screen"
                     : (Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "Fixed Assets Requisition Information Input/Edit Screen"
                      : (Request.QueryString["InputType"].ToString() == "ReqEdit") ? "Materials Requisition Information Input/Edit Screen"
                       : (Request.QueryString["InputType"].ToString() == "ReqCheck") ? reqcheckorApproved
                       : (Request.QueryString["InputType"].ToString() == "ReqcRMCheck") ? "Req CRM Check"
                      : (Request.QueryString["InputType"].ToString() == "HeadUsed") ? "Material Requisition (H/O Used)"
                      : (Request.QueryString["InputType"].ToString() == "LcEntry") ? "Lc Requistion"
                      : (Request.QueryString["InputType"].ToString() == "LcApproval") ? "Lc Requistion Approval" : "Fixed Assets Requisition Approval Screen";

                this.txtCurReqDate_CalendarExtender.EndDate = System.DateTime.Today;

                //only current date

                this.CurDate();


                ((Label)this.Master.FindControl("lblTitle")).Text = title;
                this.Master.Page.Title = title;

                //this.ImgbtnFindReq_Click(null, null);


                //genno


                //PurReqEntry.asp


            }




        }


        private void CurDate()

        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3101":
                case "3336":
                case "3337":
                    this.txtCurReqDate_CalendarExtender.StartDate = System.DateTime.Today;
                    this.txtCurReqDate.ReadOnly = true;

                    break;

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void CompBudgetexceed()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3202":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3310":
                case "3311":
                case "3101":
                case "3315":
                case "3316":
                case "3325":
                case "3332":

                    this.chkneBudget.Enabled = false;
                    this.chkneBudget.Checked = true;
                    break;



            }


        }

        private void DupMRR()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "1101":
                case "1301":
                case "2101":
                case "3101":
                case "3301":
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3310":
                case "3311":
                case "3315":
                case "3316":
                case "3317":
                case "3325":
                    this.chkdupMRF.Enabled = false;
                    this.chkdupMRF.Checked = true;
                    break;
                default:
                    break;
            }




        }
        private void Comnamemrfno()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3310":
                case "3311":
                case "3315":
                case "3325":
                case "3364":// Jbs
                case "3353":        
                    this.lblmrfno.Text = "MPR No: ";

                    break;
                default:
                    break;







            }




        }
        private void VisibleGrid()
        {

            string Type = Request.QueryString["InputType"].ToString();

            switch (Type)
            {
                case "Approval":
                case "FxtAstApproval":
                case "ReqEdit":
                case "HeadUsed":
                    this.gvReqInfo.Columns[7].Visible = true;
                    this.gvReqInfo.Columns[10].Visible = true;
                    this.gvReqInfo.Columns[11].Visible = true;
                    this.gvReqInfo.Columns[12].Visible = true;
                    this.gvReqInfo.Columns[13].Visible = true;
                    //this.gvReqInfo.Columns[14].Visible = true;
                    this.gvReqInfo.Columns[15].Visible = true;
                    this.gvReqInfo.Columns[16].Visible = true;
                    this.gvReqInfo.Columns[17].Visible = true;
                    break;

            }





        }

        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Load_Project_Combo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            string comcod = this.GetCompCode();
            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit"
                        : (this.Request.QueryString["InputType"].ToString() == "HeadUsed") ? "HeadUsed" : "";


            string Aproval = (this.Request.QueryString["InputType"].ToString() == "Approval") ? "Aproval" : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "Aproval" : "";

            //string CallType = (this.Request.QueryString["InputType"].ToString() == "Entry" && comcod == "3301") ? "PRJCODELIST1" : "PRJCODELIST";
            string CallType = (this.Request.QueryString["InputType"].ToString() == "Entry" || this.Request.QueryString["InputType"].ToString() == "LcEntry") ? "PRJCODELIST1" : "PRJCODELIST";

            string userid = hst["usrid"].ToString();
            string type = this.Request.QueryString["InputType"];
            string ReFindProject;
            //if (type == "FxtAstEntry")
            //{
            //    ReFindProject = "%" + this.txtProjectSearch.Text.Trim() + "%";
            //}
            //else
            //{
            ReFindProject = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" + this.txtProjectSearch.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            //}


            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", CallType, ReFindProject, fxtast, Aproval, userid, "", "", "", "", "");
            if (ds2 == null)
                return;

            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlProject.DataTextField = TextField;
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();

            this.ddlFloor.DataTextField = "flrdes";
            this.ddlFloor.DataValueField = "flrcod";
            this.ddlFloor.DataSource = ds2.Tables[1];
            this.ddlFloor.DataBind();

            ViewState["tblprojlist"] = ds2.Tables[0];
            this.ddlProject_SelectedIndexChanged(null, null);

        }
        protected void Load_Project_To_Combo()
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblprojlist"];
            string actcode = "16%";
            DataView dv1 = dt.DefaultView;
            //dv1.RowFilter = "actcode not in ('" + actcode + "')";
            dv1.RowFilter = "actcode like ('" + actcode + "')";
            DataTable dt1 = dv1.ToTable();
            this.ddlPrjForUse.DataTextField = "actdesc1";
            this.ddlPrjForUse.DataValueField = "actcode";
            this.ddlPrjForUse.DataSource = dt1;
            this.ddlPrjForUse.DataBind();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "New")
            {

                this.txtSrchMrfNo.Visible = true;
                this.lblpreReq.Visible = true;
                this.ImgbtnFindReq.Visible = true;
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
                this.ddlProject.Visible = true;
                this.lblddlProject.Visible = false;
                this.ddlFloor.Visible = false;
                this.lblddlFloor.Visible = false;
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.lblCurReqNo1.Text = "REQ" + DateTime.Today.ToString("MM") + "-";
                this.txtCurReqDate.Enabled = true;
                this.txtMRFNo.Text = "";

                this.txtResSearch.Text = "";
                this.ddlResSpcf.Items.Clear();
                this.ddlResList.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtExpDeliveryDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtReqNarr.Text = "";
                this.gvReqInfo.DataSource = null;
                this.gvReqInfo.DataBind();
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.PnlDesc.Visible = false;
                this.lbtnOk.Text = "Ok";
                // this.ImgbtnSpecification_Click(null, null);

                if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "ReqEdit" || Request.QueryString["InputType"].ToString() == "HeadUsed")
                {

                    this.chkdupMRF.Visible = false;
                    this.chkneBudget.Visible = false;
                    this.ddlFloor.Visible = false;
                    this.lblddlFloor.Visible = false;
                    // this.lblResList.Visible = false;
                    //this.txtResSearch.Visible = false;
                    //this.ImgbtnFindRes.Visible = false;
                    //this.ddlResList.Visible = false;
                    // this.lblSpecification.Visible = false;
                    // this.txtSrchSpecification.Visible = false;
                    // this.ImgbtnSpecification.Visible = false;
                    //this.ddlResSpcf.Visible = false;
                    //this.lbtnSelectRes.Visible = false;
                    this.lblfloor.Visible = false;
                    this.lblddlFloor.Visible = false;
                    this.lblmrfno.Visible = false;
                    this.txtMRFNo.Visible = false;
                    this.lblCurNo.Visible = false;
                    this.lblCurReqNo1.Visible = false;
                    this.txtCurReqNo2.Visible = false;
                    this.txtReqText.Visible = false;
                    this.ImgbtnReqse.Visible = false;
                    this.lbtnSurVey.Visible = true;

                    // this.ImgbtnFindReq_Click(null, null);

                }


                return;
            }

            if (Request.QueryString["InputType"].ToString() == "FxtAstApproval" || Request.QueryString["InputType"].ToString() == "ReqEdit" || Request.QueryString["InputType"].ToString() == "HeadUsed")
            {
                this.lblfloor.Visible = false;
                this.lblddlFloor.Visible = false;
                this.lblmrfno.Visible = true;
                this.txtMRFNo.Visible = true;
                //this.lnkDeleteReqNo.Visible = true;

                this.lblCurNo.Visible = true;
                this.lblCurReqNo1.Visible = true;
                this.txtCurReqNo2.Visible = true;
                this.txtReqText.Visible = true;
                this.ImgbtnReqse.Visible = true;

                //this.Panel1.Visible = false;

            }


            this.txtSrchMrfNo.Visible = false;
            this.lblpreReq.Visible = false;
            this.ImgbtnFindReq.Visible = false;
            this.ddlPrevReqList.Visible = false; //

            this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.ddlProject.Visible = false;
            this.lblddlProject.Visible = true;
            this.lblddlFloor.Text = this.ddlFloor.SelectedItem.Text.Trim();
            this.ddlFloor.Visible = false;
            this.lblddlFloor.Visible = false;
            // this.txtCurReqDate.ReadOnly = true;
            this.txtCurReqNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.Panel2.Visible = true;
            this.PnlDesc.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Requisition_Info();
            this.LinkMarketSurvey();
            //this.ImgbtnFindReq_Click(null, null);
            this.ImgbtnFindRes_Click(null, null);




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
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);

                    this.ddlPrevReqList.DataTextField = "maxreqno1";
                    this.ddlPrevReqList.DataValueField = "maxreqno";
                    this.ddlPrevReqList.DataSource = ds2.Tables[0];
                    this.ddlPrevReqList.DataBind();
                }
            }

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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblReq"] = this.HiddenSameData(ds1.Tables[0]);
            Session["tblUserReq"] = ds1.Tables[1];
            ViewState["tblreqdesc"] = ds1.Tables[2];
            this.gvDescrip.DataSource = ds1.Tables[2];
            this.gvDescrip.DataBind();
            if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "FxtAstApproval" || Request.QueryString["InputType"].ToString() == "HeadUsed")
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.GetApprQty();
                }
            }

            if (mReqNo == "NEWREQ")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurReqNo1.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            this.lblCurReqNo1.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(0, 6);
            this.txtCurReqNo2.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(6, 5);
            this.txtCurReqDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString("dd.MM.yyyy");


            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            if (ASTUtility.Left(ds1.Tables[1].Rows[0]["pactcode"].ToString(), 4) == "1102")
            {
                this.uPrj.Visible = true; ;
                this.ddlPrjForUse.DataTextField = "upactdesc";
                this.ddlPrjForUse.DataValueField = "upactcode";
                this.ddlPrjForUse.DataSource = ds1.Tables[1];
                this.ddlPrjForUse.DataBind();

                //this.Load_Project_To_Combo();
                //this.ddlPrjForUse.SelectedValue = ds1.Tables[1].Rows[0]["upactcode"].ToString();
            }
            else
            {
                this.uPrj.Visible = false; ;
            }

            this.ddlFloor.SelectedValue = ds1.Tables[1].Rows[0]["flrcod"].ToString();
            //this.lblddlProject.Text = (this.ddlProject.Items.Count == 0 ? "XXX" : this.ddlProject.SelectedItem.Text.Trim());
            this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.lblddlFloor.Text = (this.ddlFloor.Items.Count == 0 ? "YYY" : this.ddlFloor.SelectedItem.Text.Trim());
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["reqbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtExpDeliveryDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["eddat"]).ToString("dd.MM.yyyy");
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            //this.ddlptype.SelectedValue = ds1.Tables[1].Rows[0]["ptype"].ToString();
            this.gvResInfo_DataBind();
            this.ShowMarketSurvey(ds1.Tables[1].Rows[0]["msrno"].ToString());
        }

        private void LinkMarketSurvey()
        {


            string reqno = this.ddlPrevReqList.SelectedValue.ToString();
            if (reqno == "")
                return;
            string QryStr = "reqno=" + reqno;
            string TString = "javascript:window.showModalDialog('../F_12_Inv/LinkMktSurvey.aspx?" + QryStr + "', 'Unit Description', 'dialogHeight:800px;dialogWidth:900px;status:no')";
            this.lbtnSurVey.Attributes.Add("OnClick", TString);




        }
        private void ShowMarketSurvey(string msrno)
        {

            if (msrno == "")
            {
                this.lblMurketSurvey.Visible = false;
                this.lblsurveyby.Text = "";
                this.gvMSRInfo.DataSource = null;
                this.gvMSRInfo.DataBind();
                return;
            }
            this.lblMurketSurvey.Visible = true;
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO", msrno, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt1 = ds1.Tables[0];
            this.gvMSRInfo.DataSource = dt1;
            this.gvMSRInfo.DataBind();
            this.lblsurveyby.Text = (ds1.Tables[1].Rows.Count == 0) ? "" : "Survey Completed By: " + ds1.Tables[1].Rows[0]["username"].ToString();

        }
        private void GetApprQty()
        {
            DataTable dt = (DataTable)ViewState["tblReq"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double aprqty = Convert.ToDouble(dt.Rows[i]["preqty"]);
                dt.Rows[i]["areqty"] = aprqty;

            }
            ViewState["tblReq"] = dt;


        }


        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            //this.Panel2.Visible = true;
            this.Session_tblReq_Update();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            string Specification = this.ddlResSpcf.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + Specification + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                dr1["rsirdesc2"] = ddldesc == "True" ? this.ddlResList.SelectedItem.Text.Trim() : this.ddlResList.SelectedItem.Text.Trim().Substring(14);
                dr1["rsirdesc1"] = ddldesc == "True" ? this.ddlResList.SelectedItem.Text.Trim() : this.ddlResList.SelectedItem.Text.Trim().Substring(14);
                dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["bgdqty"] = dr3[0]["bgdqty"];
                dr1["bgdrat"] = dr3[0]["bgdrat"];
                dr1["treceived"] = dr3[0]["treceived"];
                dr1["bbgdqty"] = dr3[0]["bbgdqty"];
                dr1["bbgdamt"] = dr3[0]["bbgdamt"];
                dr1["bbgdqty1"] = dr3[0]["bbgdqty1"];
                dr1["bbgdamt1"] = dr3[0]["bbgdamt1"];
                dr1["stkqty"] = dr3[0]["stkqty"];
                dr1["tbgdqty"] = dr3[0]["tbgdqty"];
                dr1["tbbgdqty"] = dr3[0]["tbbgdqty"];
                dr1["preqty"] = 0;
                dr1["areqty"] = 0;
                dr1["lpurrate"] = 0;
                string comcod = this.GetCompCode();
                switch (comcod)
                {
                    case "3332":

                        dr1["reqrat"] = dr3[0]["bgdrat"];
                        dr1["reqsrat"] = dr3[0]["bgdrat"];
                        break;
                    default:
                        dr1["reqrat"] = 0;
                        dr1["reqsrat"] = 0;
                        break;
                }
                dr1["preqamt"] = 0;
                dr1["areqamt"] = 0;


                dr1["pstkqty"] = 0;
                dr1["expusedt"] = "";// DateTime.Today;
                dr1["pursdate"] = "";// DateTime.Today;
                dr1["reqnote"] = "";
                dr1["storecode"] = "";
                dr1["ssircode"] = "";
                dr1["ssirdesc"] = "";
                dr1["orderno"] = "";
                tbl1.Rows.Add(dr1);



            }

            //DataView dv = tbl1.DefaultView;
            //dv.Sort = ("rsircode");
            //tbl1 = dv.ToTable();



            //else
            //{
            //    dr2[0]["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
            //    dr2[0]["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
            //}
            ViewState["tblReq"] = this.HiddenSameData(tbl1);
            this.gvResInfo_DataBind();

        }

        private string CompanyRequisition()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                //case "3101":
                case "3315":
                //case "3325":
                case "3330":
                case "3351":
                    //case "3332":
                    //case "3101":
                    PrintReq = "PrintReque02";

                    break;
                // case "3101":
                case "3325":
                case "2325":
                    PrintReq = "PrintReque03";
                    break;

                case "3332":
                    //case"3101":
                    PrintReq = "PrintReque04";
                    break;

                case "3339":

                    PrintReq = "PrintReque05";
                    break;


                case "1101": //All Constuction
                case "1102":
                case "1103":
                    PrintReq = "PrintReque06";
                    break;




                default:
                    PrintReq = "PrintReque02";
                    break;

            }

            return PrintReq;

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            //DataView dv = dt1.DefaultView;
            //dv.Sort = "rsircode";
            //dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                    dt1.Rows[j]["bbgdqty"] = 0.00;
                    dt1.Rows[j]["bbgdamt"] = 0.00;
                    dt1.Rows[j]["bgdqty"] = 0.00;
                    dt1.Rows[j]["treceived"] = 0.00;


                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();


            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            string currentpath = "PurchasePrint.aspx?Type=ReqPrint&reqno=" + mReqNo + "&reqdat=" + CurDate1;

            string totalpath = hostname + currentpath;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

            //string printcomreq = this.CompanyRequisition();

            //if (printcomreq == "PrintReque01")
            //{
            //    this.PrintRequisition01();

            //}

            //else if (printcomreq == "PrintReque03")
            //{
            //    this.PrintRequisition03();


            //}

            //else if (printcomreq == "PrintReque04")
            //{
            //    this.PrintRequisition04();


            //}


            //else if (printcomreq == "PrintReque05")
            //{
            //    this.PrintRequisition05();


            //}

            //else if (printcomreq == "PrintReque06")
            //{
            //    //All Construction
            //    this.PrintRequisition06();


            //}


            //else
            //{
            //    this.PrintRequisition02();



            //}




        }
        private void PrintRequisition03()
        {


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry03();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]

            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();

            //DataTable dtr = (DataTable)ViewState["tblReq"];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");


            ////TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            ////txttoamt.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();
            ////TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            ////txttoamt02.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();

            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();


            //if (comcod == "3330")
            //{
            //    TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //    txtSign1.Text = "Store In-charge";
            //    TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //    txtSign2.Text = "Project Incharge";
            //    TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //    txtSign3.Text = "DPM/PM (Operation)";
            //    TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //    txtSign4.Text = "Procurement";
            //    TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //    txtSign5.Text = "Cost & Budget";
            //    TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //    txtSign6.Text = "Head Of Construction";
            //    TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //    txtSign7.Text = "Approved By";
            //}
            //else
            //{
            //    TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //    txtSign1.Text = "S.K";
            //    TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //    txtSign2.Text = "Project Incharge";
            //    TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //    txtSign3.Text = "DPM/PM/AGM/DGM";
            //    TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //    txtSign4.Text = "Procurement";
            //    TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //    txtSign5.Text = "Cost & Budget";
            //    TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //    txtSign6.Text = "Head Of Construction";
            //    TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //    txtSign7.Text = "Managing Director";
            //}





            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["tblReq"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Rdlc

            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //string txtcrno = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();

            //string txtcrdate = this.txtCurReqDate.Text.ToString().Trim();

            //string txtmrfno = this.txtMRFNo.Text.ToString().Trim();

            //string txtprojectname = this.ddlProject.SelectedItem.Text.Trim().Substring(19);
            //string txtAddress = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();

            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            //string txtfloorno = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;

            //string txtpforused = dt.Rows[3]["termsdesc"].ToString();

            //DataTable dtr = (DataTable)ViewState["tblReq"];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            //string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            //string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //string rpttxtnaration = this.txtReqNarr.Text.Trim();
            //string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            //string txtSign1 = "";
            //string txtSign2 = "";
            //string txtSign3 = "";
            //string txtSign4 = "";
            //string txtSign5 = "";
            //string txtSign6 = "";
            //string txtSign7 = "";

            //if (comcod == "3330")
            //{

            //    txtSign1 = "Store In-charge";

            //    txtSign2 = "Project Incharge";

            //    txtSign3 = "DPM/PM (Operation)";

            //    txtSign4 = "Procurement";

            //    txtSign5 = "Cost & Budget";

            //    txtSign6 = "Head Of Construction";

            //    txtSign7 = "Approved By";
            //}


            //else
            //{

            //    txtSign1 = "S.K";

            //    txtSign2 = "Project Incharge";

            //    txtSign3 = "DPM/PM/AGM/DGM";

            //    txtSign4 = "Procurement";

            //    txtSign5 = "Cost & Budget";

            //    txtSign6 = "Head Of Construction";

            //    txtSign7 = "Managing Director";
            //}

            //var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry03", lst, null, null);
            //Rpt1.EnableExternalImages = true;


            //Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));

            //Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            //Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            //Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            //Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            //Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            //Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            //Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            //Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            //Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            //Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            //Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            //Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            //Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            //Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            //Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            //Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            //Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            //Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            //Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            //Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));




            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }
        private void PrintRequisition01()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject rpttxtexdeldate = rptstk.ReportDefinition.ReportObjects["eddate"] as TextObject;
            //rpttxtexdeldate.Text = this.txtExpDeliveryDate.Text.Trim();
            //TextObject rpttxtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpttxtadate.Text = this.txtApprovalDate.Text.Trim();
            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //TextObject txtfloornoText = rptstk.ReportDefinition.ReportObjects["floornotext"] as TextObject;
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["floorno"] as TextObject;
            //if (ddlFloor.SelectedValue.ToString().Trim() != "000")
            //{

            //    txtfloornoText.Text = "Floor No:";
            //    txtfloorno.Text = this.ddlFloor.SelectedValue.ToString().Trim();
            //}
            //else
            //{
            //    txtfloornoText.Text = "";
            //    txtfloorno.Text = " ";
            //}

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();
            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{

            //    string eventtype = "Material Requisition";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name " + this.ddlProject.SelectedItem.ToString() + "Req No- " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //DataTable dt1 = new DataTable();
            //dt1 = (DataTable)ViewState["tblReq"];

            //rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintRequisition05()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, CurDate1,
            //         "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //DataTable dt1 = ds1.Tables[1];
            //DataTable dt2 = ds1.Tables[3];


            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntryTropical();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();


            //TextObject rpttxtReq = rptstk.ReportDefinition.ReportObjects["txtReq"] as TextObject;
            //rpttxtReq.Text = dt2.Rows[0]["reqnam"].ToString() + "\n" + dt2.Rows[0]["reqdat"].ToString();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(19);
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            ////txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString() == "" ? "" : ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]
            //txtAddress.Text = dt1.Rows[0]["paddress"].ToString();
            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();


            //DataTable dtr = (DataTable)ViewState["tblReq"];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");



            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["tblReq"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintRequisition06()
        {


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, CurDate1,
            //         "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //DataTable dt1 = ds1.Tables[1];
            //DataTable dt2 = ds1.Tables[3];


            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntryCons();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text =  this.txtCurReqDate.Text.ToString().Trim();

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();


            //TextObject rpttxtReq = rptstk.ReportDefinition.ReportObjects["txtReq"] as TextObject;
            //rpttxtReq.Text = dt2.Rows[0]["reqnam"].ToString() + "\n" + dt2.Rows[0]["reqdat"].ToString();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(19);
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            ////txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString() == "" ? "" : ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]
            //txtAddress.Text = dt1.Rows[0]["paddress"].ToString();
            //DataTable dt = (DataTable)ViewState["tblreqdesc"];


            //string buildno = ((dt.Rows.Count == 0) ? "" : dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString()  : "");
            //string purpose = ((dt.Rows.Count == 0) ? "" : dt.Select("termsid='004'").Length > 0 ?(dt.Select("termsid='004'")[0]["termsdesc"]).ToString()  : "");
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = buildno;

            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = purpose;

            //DataTable dtr = (DataTable)ViewState["tblReq"];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");



            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["tblReq"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintRequisition02()
        {



            ////DataTable dt1 = (DataTable)Session["tblUserReq"]; 
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, CurDate1,
            //         "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            ////Rdlc
            //DataTable dt1 = ds1.Tables[1];
            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //string txtcrno = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();

            //string txtcrdate = this.txtCurReqDate.Text.ToString().Trim();

            //string txtmrfno = this.txtMRFNo.Text.ToString().Trim();

            //string txtprojectname = this.ddlProject.SelectedItem.Text.Trim().Substring(19);
            //string txtAddress = dt1.Rows[0]["paddress"].ToString();




            //string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString()  : ""));
            ////string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            //string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            //string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno +(dt.Select("termsid='003'").Length > 0 ?(", "+ (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            ////  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            //string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : "")); 

            //DataTable dtr = (DataTable)ViewState["tblReq"];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            //string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            //string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //string rpttxtnaration = this.txtReqNarr.Text.Trim();
            //string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            //string txtSign1 = "";
            //string txtSign2 = "";
            //string txtSign3 = "";
            //string txtSign4 = "";
            //string txtSign5 = "";
            //string txtSign6 = "";
            //string txtSign7 = "";

            //if (comcod == "3330")
            //{

            //    txtSign1 = "Store In-charge";

            //    txtSign2 = "Project Incharge";

            //    txtSign3 = "DPM/PM (Operation)";

            //    txtSign4 = "Procurement";

            //    txtSign5 = "Cost & Budget";

            //    txtSign6 = "Head Of Construction";

            //    txtSign7 = "Approved By";
            //}

            //else if (comcod == "3332" || comcod == "3101")
            //{


            //    txtSign1 = "S.K";

            //    txtSign2 = "Project Incharge";

            //    txtSign3 = "Procurement";

            //    txtSign4 = "Cost & Budget";

            //    txtSign5 = "Cheif Engineer";

            //    txtSign6 = "Director";

            //    txtSign7 = "Managing Director/Chairman";

            //}


            //else
            //{

            //    txtSign1 = "S.K";

            //    txtSign2 = "Project Incharge";

            //    txtSign3 = "DPM/PM/AGM/DGM";

            //    txtSign4 = "Procurement";

            //    txtSign5 = "Cost & Budget";

            //    txtSign6 = "Head Of Construction";

            //    txtSign7 = "Managing Director";
            //}

            //var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry02", lst, null, null);
            //Rpt1.EnableExternalImages = true;


            //Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));

            //Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            //Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            //Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            //Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            //Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            //Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            //Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            //Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            //Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            //Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            //Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            //Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            //Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));      
            //Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            //Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            //Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            //Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            //Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            //Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            //Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));




            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Crystal
            //DataTable dt1 = ds1.Tables[1];

            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry02();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(19);
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            ////txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString() == "" ? "" : ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]
            //txtAddress.Text = dt1.Rows[0]["paddress"].ToString();
            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();


            //DataTable dtr = (DataTable)ViewState["tblReq"];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");



            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();


            //if (comcod == "3330")
            //{
            //    TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //    txtSign1.Text = "Store In-charge";
            //    TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //    txtSign2.Text = "Project Incharge";
            //    TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //    txtSign3.Text = "DPM/PM (Operation)";
            //    TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //    txtSign4.Text = "Procurement";
            //    TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //    txtSign5.Text = "Cost & Budget";
            //    TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //    txtSign6.Text = "Head Of Construction";
            //    TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //    txtSign7.Text = "Approved By";
            //}

            //else if (comcod == "3332" || comcod == "3101")
            //{

            //    TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //    txtSign1.Text = "S.K";
            //    TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //    txtSign2.Text = "Project Incharge";
            //    TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //    txtSign3.Text = "Procurement";
            //    TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //    txtSign4.Text = "Cost & Budget";
            //    TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //    txtSign5.Text = "Cheif Engineer";
            //    TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //    txtSign6.Text = "Director";
            //    TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //    txtSign7.Text = "Managing Director/Chairman";

            //}


            //else
            //{
            //    TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //    txtSign1.Text = "S.K";
            //    TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //    txtSign2.Text = "Project Incharge";
            //    TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //    txtSign3.Text = "DPM/PM/AGM/DGM";
            //    TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //    txtSign4.Text = "Procurement";
            //    TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //    txtSign5.Text = "Cost & Budget";
            //    TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //    txtSign6.Text = "Head Of Construction";
            //    TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //    txtSign7.Text = "Managing Director";
            //}





            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["tblReq"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintRequisition04()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, CurDate1,
            //         "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //DataTable dt1 = ds1.Tables[1];

            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntryInns();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            ////TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            ////txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(19);
            ////TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            ////txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]

            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = dt1.Rows[0]["paddress"].ToString();

            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            ////TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            ////txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            ////TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            ////txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            ////TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            ////txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();


            //DataTable dtr = (DataTable)ViewState["tblReq"];

            ////double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            ////double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            ////double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            ////TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            ////txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            ////TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            ////txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");



            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();

            //TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //txtSign1.Text = "Site Engr";
            //TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //txtSign2.Text = "PM /P.Engr";
            //TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //txtSign3.Text = "Sr.PM";
            //TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //txtSign4.Text = "Chief Engineer";
            //TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //txtSign5.Text = "E.D";
            //TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //txtSign6.Text = "Director";
            //TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //txtSign7.Text = "Managing Director/Chairman";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(ds1.Tables[0]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }

        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {

            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            // int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            // DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            // if (!Convert.ToBoolean(dr1[0]["entry"]))
            // {
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //     ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            //     return;
            // }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.lbtnResFooterTotal_Click(null, null);
            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevReqList.Items.Count == 0)
                this.GetReqNo();
            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            DataTable dt2 = (DataTable)Session["tblUserReq"];


            if (this.chkdupMRF.Checked)
            {
                if (mMRFNO.Length == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "M.R.F No. Should Not Be Empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                DataSet ds2 = new DataSet();

                switch (comcod)
                {

                    case "3332":


                        string pactcode = this.ddlProject.SelectedValue.ToString();
                        ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKEDDUPMRRNO", mMRFNO, pactcode, "", "", "", "", "", "", "");
                        //if (bbgdamt < 0 || dgvBgdQty < dgvReqQty)


                        break;


                    default:
                        ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKEDDUPMRRNO", mMRFNO, "", "", "", "", "", "", "", "");

                        break;

                }





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
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //this.ddlPrevReqList.Items.Clear();
                        return;
                    }
                }
            }

            // Emty Quantity
            DataRow[] dr2 = tbl1.Select("areqty>0");
            //if(dr2.Length>0)
            //Log Entry

            DataTable dtuser = (DataTable)Session["tblUserReq"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPostedDat = (dtuser.Rows.Count == 0) ? "" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            string PostedByid = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedByid == "") ? userid : tblPostedByid) : ((tblPostedByid == "") ? userid : tblPostedByid);
            string Posttrmid = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid) : ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid);
            string PostSession = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedSession == "") ? Sessionid : tblPostedSession) : ((tblPostedSession == "") ? Sessionid : tblPostedSession);
            string PostedDat = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedDat == "") ? Date : tblPostedDat) : ((tblPostedDat == "") ? Date : tblPostedDat);




            //string PostedByid = (this.Request.QueryString["InputType"] == "Entry") ? userid : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? userid
            //        : (tblPostedByid == "") ? userid : tblPostedByid;
            //string Posttrmid = (this.Request.QueryString["InputType"] == "Entry") ? Terminal : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Terminal
            //    : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            //string PostSession = (this.Request.QueryString["InputType"] == "Entry") ? Sessionid : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Sessionid
            //    : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            //string PostedDat = (this.Request.QueryString["InputType"] == "Entry") ? Date : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Date
            //    : (tblPostedSession == "") ? Date : tblPostedDat;




            string EditByid = (this.Request.QueryString["InputType"] == "ReqEdit") ? userid : "";
            string Edittrmid = (this.Request.QueryString["InputType"] == "ReqEdit") ? Terminal : "";
            string EditSession = (this.Request.QueryString["InputType"] == "ReqEdit") ? Sessionid : "";
            string EditDat = (this.Request.QueryString["InputType"] == "ReqEdit") ? Date : "01-Jan-1900";






            //string ApprovByid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : userid;
            //string approvdat = (this.Request.QueryString["InputType"] == "Entry") ? "01-Jan-1900" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "01-Jan-1900"
            //    : System.DateTime.Today.ToString("dd-MMM-yyyy");
            //string Approvtrmid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : Terminal;
            //string ApprovSession = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : Sessionid;



            ////string approved = (this.Request.QueryString["InputType"] == "Approval") ? "Ok" :
            //    (this.Request.QueryString["InputType"] == "FxtAstApproval") ? "Ok" : (dtuser.Rows.Count == 0) ? "" : dr2.Length == 0 ? "" : dtuser.Rows[0]["approved"].ToString();

            //////


            // Balance quantity Cheecked
            if (this.Request.QueryString["InputType"] == "Entry" || this.Request.QueryString["InputType"] == "FxtAstEntry")
            {



                // Emty Quantity
                DataRow[] drempty = tbl1.Select("preqty<=0");
                if (drempty.Length > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Present Quantity Required";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                int index;
                string Rsircode = "000000000000";
                double chkqty = 0.00;
                for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
                {

                    index = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;

                    string Resocde = tbl1.Rows[index]["rsircode"].ToString();
                    double dgvBgdQty = Convert.ToDouble(tbl1.Rows[index]["bbgdqty1"]);
                    double bbgdamt = Convert.ToDouble(tbl1.Rows[index]["bbgdamt1"]);
                    double dgvReqQty =
                        Convert.ToDouble(
                            ASTUtility.ExprToValue("0" +
                                                   ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));


                    if (this.Request.QueryString["InputType"] == "Entry")
                    {
                        if (this.chkneBudget.Checked)
                        {
                            switch (comcod)
                            {
                                case "3202":
                                case "3305":
                                case "3306":
                                case "3307":
                                case "3308":
                                case "3310":
                                case "3311":
                                case "3101":
                                case "3315":
                                case "3316":
                                case "3325":
                                case "3332":
                                case "3330":

                                    //if (bbgdamt < 0 || dgvBgdQty < dgvReqQty)
                                    if (dgvBgdQty < dgvReqQty)
                                    {
                                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                        return;

                                    }
                                    if (Rsircode == Resocde)
                                    {
                                        chkqty = chkqty - dgvReqQty;
                                        if (chkqty < 0)
                                        {
                                            ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);",
                                                true);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        chkqty = dgvBgdQty - dgvReqQty;
                                    }
                                    Rsircode = tbl1.Rows[index]["rsircode"].ToString();

                                    break;


                                default:

                                    if (dgvBgdQty < dgvReqQty)
                                    {
                                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                        return;

                                    }
                                    if (Rsircode == Resocde)
                                    {
                                        chkqty = chkqty - dgvReqQty;
                                        if (chkqty < 0)
                                        {
                                            ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);",
                                                true);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        chkqty = dgvBgdQty - dgvReqQty;
                                    }
                                    Rsircode = tbl1.Rows[index]["rsircode"].ToString();

                                    break;

                            }





                        }
                    }




                }





            }








            string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
            string mFLRCOD = this.ddlFloor.SelectedValue.ToString().Trim();
            string mREQUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mEDDAT = this.GetStdDate(this.txtExpDeliveryDate.Text.Trim()); // DateTime.Today.ToString("dd-MMM-yyyy");
            string mREQBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string reqtype = "";
            string uFP = this.ddlPrjForUse.SelectedValue.ToString();
            if (this.Request.QueryString["InputType"] == "LcEntry")
            {
                reqtype = "LC";
            }


            string crmchekd = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["iscrchecked"].ToString();
            string tblcrmcheckbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["crmcheckbyid"].ToString();

            string tblcrnPosttrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["crmchecktrmid"].ToString();
            string tblcrmPostSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["crmcheckseson"].ToString();
            string tblcrmcPostedDat = (dtuser.Rows.Count == 0) ? "" : Convert.ToDateTime(dtuser.Rows[0]["crmcheckdat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            //crm part  iscrchecked

            string crmcheckbyid = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedByid == "") ? userid : tblPostedByid) : ((tblcrmcheckbyid == "") ? userid : tblcrmcheckbyid);
            string crnPosttrmid = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid) : ((tblcrnPosttrmid == "") ? Terminal : tblcrnPosttrmid);
            string crmPostSession = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedSession == "") ? Sessionid : tblPostedSession) : ((tblcrmPostSession == "") ? Sessionid : tblcrmPostSession);
            string crmcPostedDat = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedDat == "") ? Date : tblPostedDat) : ((tblcrmcPostedDat == "") ? Date : tblcrmcPostedDat);


            ////crm part
            //string crmchekd = "";
            //string crmcheckbyid = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedByid == "") ? userid : tblPostedByid) : ((tblPostedByid == "") ? userid : tblPostedByid);
            //string crnPosttrmid = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid) : ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid);
            //string crmPostSession = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedSession == "") ? Sessionid : tblPostedSession) : ((tblPostedSession == "") ? Sessionid : tblPostedSession);
            //string crmcPostedDat = ((this.Request.QueryString["InputType"] == "Entry") || (this.Request.QueryString["InputType"] == "FxtAstEntry")) ? ((tblPostedDat == "") ? Date : tblPostedDat) : ((tblPostedDat == "") ? Date : tblPostedDat);

            if (this.Request.QueryString["InputType"] == "Entry")
            {
                switch (comcod)
                {

                    // case "3101":
                    case "3316":
                    case "3315":
                    case "3317":
                    case "3101":
                        crmchekd = ((CheckBox)this.gvReqInfo.FooterRow.FindControl("crChkbox")).Checked == true ? "1" : "0";
                        if (crmchekd == "1")
                        {
                            crmcheckbyid = "";
                            crmcPostedDat = "01-01-1900";
                            crmPostSession = "";
                            crnPosttrmid = "";
                        }
                        else
                        {

                            crmchekd = "0";

                        }



                        break;

                    default:

                        crmchekd = "0";


                        break;



                }
            }

            //else if (this.Request.QueryString["InputType"] == "ReqCheck")
            //{
            //    string pactcode1 = this.ddlProject.SelectedValue.ToString();
            //    string mReqno = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();              
            //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKISCRMCHECKED", pactcode1, mReqno, "", "", "", "", "", "", "");
            //    if (ds1.Tables[0].Rows.Count == 0)
            //        return;

            //    crmchekd = ds1.Tables[0].Rows[0]["ISCRCHECKED"].ToString();

            //}




            string mREQNAR = this.txtReqNarr.Text.Trim();
            //string ptype = this.ddlptype.SelectedValue.ToString();
            bool result = purData.UpdateTransInfo01(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQB", mREQNO, mREQDAT, mPACTCODE, mFLRCOD, mREQUSRID,
                mAPPRUSRID, mAPPRDAT, mEDDAT, mREQBYDES, mAPPBYDES, mMRFNO, mREQNAR, PostedByid, Posttrmid, PostSession, EditByid, Edittrmid, EditSession,
                EditDat, PostedDat, reqtype, uFP, crmchekd, crmcheckbyid, crnPosttrmid, crmPostSession, crmcPostedDat);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string rowId = i.ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();

                double mPREQTY = Convert.ToDouble(tbl1.Rows[i]["preqty"]);
                double mAREQTY = Convert.ToDouble(tbl1.Rows[i]["areqty"]);
                double mBgdBalQty = Convert.ToDouble(tbl1.Rows[i]["bbgdqty"]);
                string mREQRAT = tbl1.Rows[i]["reqrat"].ToString();
                string mREQSRAT = tbl1.Rows[i]["reqsrat"].ToString();
                string mPSTKQTY = tbl1.Rows[i]["pstkqty"].ToString();
                string mEXPUSEDT = tbl1.Rows[i]["expusedt"].ToString();
                string mREQNOTE = tbl1.Rows[i]["reqnote"].ToString();
                string PursDate = tbl1.Rows[i]["pursdate"].ToString();
                string Lpurrate = tbl1.Rows[i]["lpurrate"].ToString();
                string storecode = tbl1.Rows[i]["storecode"].ToString();
                string ssircode = tbl1.Rows[i]["ssircode"].ToString();
                string orderno = tbl1.Rows[i]["orderno"].ToString();

                if (mPREQTY >= mAREQTY)
                {
                    //if (this.chkneBudget.Checked)
                    //{

                    //    if (mBgdBalQty < mPREQTY)
                    //    {
                    //       ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                    //        return;

                    //    }

                    //}
                    result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQA",
                                mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                                PursDate, Lpurrate, storecode, ssircode, orderno, mREQSRAT, rowId, "", "", "", "", "", "");


                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Aprove Qty Must be Less Or Equal  Req. Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

            }



            this.SaveReqDesc();
            DataTable dt1 = (DataTable)ViewState["tblreqdesc"];



            foreach (DataRow dr in dt1.Rows)
            {
                string mTERMSID = dr["termsid"].ToString().Trim();
                string mTERMSSUBJ = dr["termssubj"].ToString().Trim();
                string mTERMSDESC = dr["termsdesc"].ToString().Trim();
                string mTERMSRMRK = dr["termsrmrk"].ToString().Trim();


                switch (comcod)
                {

                    case "3336":
                    case "3337":
                    case "3101":



                        if (mTERMSID == "002" || mTERMSID == "004")
                        {
                            if (mTERMSDESC.Length == 0)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = "Floor No. / Purpose of Use Should Not Be Empty";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                        }

                        break;






                }

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQC",
                                mREQNO, mTERMSID, mTERMSSUBJ, mTERMSDESC, mTERMSRMRK, "", "", "", "",
                                "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }


            this.txtCurReqDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            if (hst["compsms"].ToString() == "True")
            {
                switch (comcod)
                {
                    case "3333":
                        break;

                    default:
                        if (this.Request.QueryString["InputType"] == "Entry" || this.Request.QueryString["InputType"] == "FxtAstEntry")
                        {

                            SendSmsProcess sms = new SendSmsProcess();
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string ddldesc = hst["ddldesc"].ToString();
                            string frmname = "PurReqEntry.aspx?InputType=ReqCheck";

                            string SMSHead = "Ready for Check, ";


                            string SMSText = comnam + ":\n" + SMSHead + "\n" + ddldesc == "True" ? ddlProject.SelectedItem.Text.Trim() : ddlProject.SelectedItem.Text.Trim().Substring(12) + "\n" + "MRF No: " + txtMRFNo.Text;
                            bool resultsms = sms.SendSmms(SMSText, userid, frmname);


                        }
                        break;


                }





            }

            //if (ConstantInfo.LogStatus == true)
            //{

            //    string eventtype = "Material Requisition";
            //    string eventdesc = "Update Reqisition";
            //    string eventdesc2 = "Req No- " + mREQNO;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


        }

        //private void sendSmsFromAPI(string SMSText, string userid, string frmname)
        //{



        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string Sesion = hst["session"].ToString();
        //        userid = hst["usrid"].ToString();
        //    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        //    string frmname1 = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp);
        //    frmname = frmname1.Substring(frmname.LastIndexOf('/') + 1) + "";
        //    SMSText = "Requisition created from Project:" + ddlProject.SelectedItem.Text + ",MRF No:" + txtMRFNo.Text; //    






        //    //DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFO", userid, frmname, "", "", "");
        //    //if (ds3.Tables[0].Rows.Count == 0)
        //    //    return;
        //    //string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
        //    //string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
        //    //string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
        //    //string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
        //    //string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender

        //     //   this.sendSmsFromAPI("Requisition created from Project:" + ddlProject.SelectedItem.Text + ",MRF No:" + txtMRFNo.Text);

        //   // SMSText = "Requisition created from Project:" + ddlProject.SelectedItem.Text + ",MRF No:" + txtMRFNo.Text; //        

        //    //string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
        //    //string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
        //    //for(int i=0; i<ds3.Tables[1].Rows.Count; i++)
        //    //{
        //    //    string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

        //    //    //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + mobile + "&sms[0][1]=" + System.Web.HttpUtility.UrlEncode(SMSText) + "&sms[0][2]=" + "1234567890" + "&sid=" + sender;
        //    //    //using (WebClient wc = new WebClient())
        //    //    //{
        //    //    //    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //    //    //    string HtmlResult = wc.UploadString(ApiUrl, myParameters); Console.Write(HtmlResult);
        //    //    //}
        //    //    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&sender=" + sender
        //    //       + "&SMSText=" + SMSText + "&GSM=" + mobile + "&type=longSMS");

        //    //    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //    //    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //    //    string responseString = respStreamReader.ReadToEnd();
        //    //    respStreamReader.Close();
        //    //    myResp.Close();
        //   // }


        //}


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("fappid", Type.GetType("System.String"));
            tblt01.Columns.Add("fappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("fapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("fappseson", Type.GetType("System.String"));
            tblt01.Columns.Add("sappid", Type.GetType("System.String"));
            tblt01.Columns.Add("sappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("sapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("sappseson", Type.GetType("System.String"));
            ViewState["tblapproval"] = tblt01;
        }

        private string GetReqApproval(string approval)
        {


            string type = this.Request.QueryString["InputType"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (type)
            {
                //n50
                case "ReqCheck":
                case "ReqcRMCheck":
                    switch (comcod)
                    {
                        // case "3101":
                        // case "3338": //ACME
                        case "3348": //Credence
                            break;

                        default:
                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();
                                dr1["fappid"] = usrid;
                                dr1["fappdat"] = Date;
                                dr1["fapptrmid"] = trmnid;
                                dr1["fappseson"] = session;
                                dr1["sappid"] = usrid;
                                dr1["sappdat"] = Date;
                                dr1["sapptrmid"] = trmnid;
                                dr1["sappseson"] = session;
                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();

                            }




                            break;

                    }

                    break;


                case "ReqFirstApproved":
                    //switch (comcod) 
                    //{ 
                    //case "3338": //ACME
                    //    if (approval == "")
                    //    {


                    //        this.CreateDataTable();
                    //        DataTable dt = (DataTable)ViewState["tblapproval"];
                    //        DataRow dr1 = dt.NewRow();

                    //        dr1["fappid"] = usrid;
                    //        dr1["fappdat"] = Date;
                    //        dr1["fapptrmid"] = trmnid;
                    //        dr1["fappseson"] = session;
                    //        dr1["sappid"] = usrid;
                    //        dr1["sappdat"] = Date;
                    //        dr1["sapptrmid"] = trmnid;
                    //        dr1["sappseson"] = session;
                    //        dt.Rows.Add(dr1);
                    //        ds1.Merge(dt);
                    //        ds1.Tables[0].TableName = "tbl1";
                    //        approval = ds1.GetXml();

                    //    }

                    //    else
                    //    {

                    //        xmlSR = new System.IO.StringReader(approval);
                    //        ds1.ReadXml(xmlSR);
                    //        ds1.Tables[0].TableName = "tbl1";
                    //        ds1.Tables[0].Rows[0]["fappid"] = usrid;
                    //        ds1.Tables[0].Rows[0]["fappdat"] = Date;
                    //        ds1.Tables[0].Rows[0]["fapptrmid"] = trmnid;
                    //        ds1.Tables[0].Rows[0]["fappseson"] = session;

                    //        ds1.Tables[0].Rows[0]["sappid"] = usrid;
                    //        ds1.Tables[0].Rows[0]["sappdat"] = Date;
                    //        ds1.Tables[0].Rows[0]["sapptrmid"] = trmnid;
                    //        ds1.Tables[0].Rows[0]["sappseson"] = session;
                    //        approval = ds1.GetXml();

                    //    }
                    //    break;


                    //default:
                    if (approval == "")
                    {


                        this.CreateDataTable();
                        DataTable dt = (DataTable)ViewState["tblapproval"];
                        DataRow dr1 = dt.NewRow();

                        dr1["fappid"] = usrid;
                        dr1["fappdat"] = Date;
                        dr1["fapptrmid"] = trmnid;
                        dr1["fappseson"] = session;
                        dr1["sappid"] = "";
                        dr1["sappdat"] = "";
                        dr1["sapptrmid"] = "";
                        dr1["sappseson"] = "";
                        dt.Rows.Add(dr1);
                        ds1.Merge(dt);
                        ds1.Tables[0].TableName = "tbl1";
                        approval = ds1.GetXml();

                    }

                    else
                    {

                        xmlSR = new System.IO.StringReader(approval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["fappid"] = usrid;
                        ds1.Tables[0].Rows[0]["fappdat"] = Date;
                        ds1.Tables[0].Rows[0]["fapptrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["fappseson"] = session;
                        ds1.Tables[0].Rows[0]["sappid"] = "";
                        ds1.Tables[0].Rows[0]["sappdat"] = "";
                        ds1.Tables[0].Rows[0]["sapptrmid"] = "";
                        ds1.Tables[0].Rows[0]["sappseson"] = "";
                        approval = ds1.GetXml();

                    }
                    break;




                // }




                //        break;




                case "ReqSecondApproved":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["sappid"] = usrid;
                    ds1.Tables[0].Rows[0]["sappdat"] = Date;
                    ds1.Tables[0].Rows[0]["sapptrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["sappseson"] = session;
                    approval = ds1.GetXml();

                    break;







            }


            return approval;

        }


        private string GetReqCheckAApproved()
        {

            string reqcheckaapproved = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                // case "3101":      //ASIT      
                case "3338":  //ACME
                              //case "1103":  //Tanvir
                case "3348":  //Credence

                    break;

                default:
                    reqcheckaapproved = "reqCheckedAApproved";
                    break;
            }

            return reqcheckaapproved;


        }

        protected void lbtnCheecked_Click(object sender, EventArgs e)
        {


            this.CheckedAndUpdate();

        }


        private void CheckedAndUpdate()
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string checkusrid = hst["usrid"].ToString();
            string checkTerminal = hst["compname"].ToString();
            string checkSessionid = hst["session"].ToString();
            string checkDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            //string reqcheckaapproved = this.GetReqCheckAApproved();
            //string 

            DataTable dt = ((DataTable)Session["tblUserReq"]);
            DataTable tbl1 = (DataTable)ViewState["tblReq"];

            int index = 0;
            string pactcode1 = this.Request.QueryString["prjcode"].ToString();
            string pactcode = ASTUtility.Left(pactcode1, 4);
            //  todo for check central inventory
            switch (pactcode)
            {
                case "1102":
                    break;

                default:
                    //txtgvReqQty
                    for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
                    {
                        index = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;

                        double dgvBgdQty = Convert.ToDouble(tbl1.Rows[index]["bbgdqty1"]);
                        double dgvReqQty =
                                Convert.ToDouble(
                                    ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));

                        if (this.Request.QueryString["InputType"] == "ReqCheck")
                        {
                            if (dgvBgdQty < dgvReqQty)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;

                            }
                        }

                    }
                    break;
            }

            string appxml = ((DataTable)Session["tblUserReq"]).Rows[0]["rapproval"].ToString();
            string Approval = this.GetReqApproval(appxml);
            string crmData = this.Request.QueryString["InputType"] == "ReqcRMCheck" ? "crm" : "";
            string crmNarr = "";
            if (crmData == "crm")
            {
                crmNarr = txtCCDNarr.Text.ToString();
            }

            else
            {
                crmNarr = txtCCDNarr.Text.ToString();

            }


            //string reqapproval = this.GetReqApproval();
            //bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEREQCHECKED", mREQNO, Approval, "", "", "", "", "", "",
            //    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEREQCHECKED", mREQNO, checkusrid, checkTerminal, checkSessionid, checkDate, Approval, crmData, crmNarr,
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            if (hst["compsms"].ToString() == "True")
            {

                switch (comcod)
                {
                    case "3333":
                        break;

                    default:
                        SendSmsProcess sms = new SendSmsProcess();
                        string comnam = hst["comnam"].ToString();
                        string ddldesc = hst["ddldesc"].ToString();
                        string compname = hst["compname"].ToString();
                        string frmname = "PurReqApproval.aspx?Type=RateInput";

                        string SMSHead = "Ready To Rate Proposal, ";
                        string SMSText = comnam + ":\n" + SMSHead + "\n" + ddldesc == "True" ? ddlProject.SelectedItem.Text.Trim() : ddlProject.SelectedItem.Text.Trim().Substring(12) + "\n" + "MRR No: " + txtMRFNo.Text + "\n" + "Thanks";
                        bool resultsms = sms.SendSmms(SMSText, checkusrid, frmname);
                        break;
                }
            }
            // comment nahid why its call ??
            if (this.Request.QueryString["InputType"] != "ReqcRMCheck")
            {
                lbtnUpdateResReq_Click(null, null);
            }



        }


        protected void lbtnFirstApproval_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string faprvusrid = hst["usrid"].ToString();
            string faprvTerminal = hst["compname"].ToString();
            string faprvSessionid = hst["session"].ToString();
            string faprvDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string appxml = ((DataTable)Session["tblUserReq"]).Rows[0]["rapproval"].ToString();
            string Approval = this.GetReqApproval(appxml);


            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEFIRSTAPPROVED", mREQNO, Approval, "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            //bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEFIRSTAPPROVED", mREQNO, faprvusrid, faprvTerminal, faprvSessionid, faprvDate, "", "", "",
            //    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            if (hst["compsms"].ToString() == "True")
            {

                switch (comcod)
                {
                    case "3333":
                        break;

                    default:
                        SendSmsProcess sms = new SendSmsProcess();
                        string comnam = hst["comnam"].ToString();
                        string compname = hst["compname"].ToString();
                        string ddldesc = hst["ddldesc"].ToString();
                        string frmname = "PurReqApproval.aspx?Type=RateInput";

                        string SMSHead = "Ready To Rate Proposal, ";
                        string SMSText = comnam + ":\n" + SMSHead + "\n" + ddldesc == "True" ? ddlProject.SelectedItem.Text.Trim() : ddlProject.SelectedItem.Text.Trim().Substring(12) + "\n" + "MRR No: " + txtMRFNo.Text + "\n" + "Thanks";
                        bool resultsms = sms.SendSmms(SMSText, faprvusrid, frmname);
                        break;
                }
            }

            lbtnUpdateResReq_Click(null, null);
        }

        private void SaveReqDesc()
        {

            DataTable dt = (DataTable)ViewState["tblreqdesc"];

            for (int i = 0; i < this.gvDescrip.Rows.Count; i++)
            {
                string trmdesc = ((TextBox)this.gvDescrip.Rows[i].FindControl("txtgvDesc")).Text.Trim();
                dt.Rows[i]["termsdesc"] = trmdesc;
            }


            ViewState["tblreqdesc"] = dt;

        }

        private string GetReqcheckorApproved()
        {
            string comcod = this.GetCompCode();
            string reqcheck = "";

            switch (comcod)
            {
                case "3336":
                case "3340":
                    reqcheck = "Requisition Approval";

                    break;

                default:

                    reqcheck = "Requisition Checked";

                    break;


            }

            return reqcheck;

        }

        protected void gvResInfo_DataBind()
        {
            string comcod = this.GetCompCode();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            this.gvReqInfo.DataSource = tbl1;
            this.gvReqInfo.Columns[21].Visible = true && (comcod == "3325" || comcod == "2325");


            this.gvReqInfo.Columns[22].Visible = true && (comcod == "3325" || comcod == "2325");

            this.gvReqInfo.DataBind();
            if (Request.QueryString["InputType"].ToString() == "Approval") // "Entry"
            {
                for (int i = 0; i < this.gvReqInfo.Rows.Count; i++)
                {
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvReqQty")).ReadOnly = true;
                    // ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvResRat")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvUseDat")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvStokQty")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvReqNote")).ReadOnly = true;

                }
            }


            ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnCheecked")).Visible = (this.Request.QueryString["InputType"] == "ReqCheck" || this.Request.QueryString["InputType"] == "ReqcRMCheck");
            ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnFirstApproval")).Visible = (this.Request.QueryString["InputType"] == "ReqFirstApproved" || this.Request.QueryString["InputType"] == "ReqSecondApproved");


            ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnUpdateResReq")).Visible = !(this.Request.QueryString["InputType"].ToString().Trim() == "ReqCheck" || this.Request.QueryString["InputType"].ToString().Trim() == "ReqcRMCheck" || this.Request.QueryString["InputType"].ToString().Trim() == "ReqFirstApproved" || this.Request.QueryString["InputType"] == "ReqSecondApproved");
            ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnResFooterTotal")).Visible = !(this.Request.QueryString["InputType"].ToString().Trim() == "ReqCheck" || this.Request.QueryString["InputType"].ToString().Trim() == "ReqcRMCheck" || this.Request.QueryString["InputType"].ToString().Trim() == "ReqFirstApproved" || this.Request.QueryString["InputType"] == "ReqSecondApproved");


            switch (comcod)
            {

                // case "3101":
                case "3316":
                case "3315":
                case "3317":
                case "3101":
                    if (this.Request.QueryString["InputType"] == "Entry")
                    {
                        //    this.lblReqNarr.Visible = false;
                        //    this.txtReqNarr.Visible = false;
                        ((CheckBox)this.gvReqInfo.FooterRow.FindControl("crChkbox")).Visible = true;

                    }

                    else if (this.Request.QueryString["InputType"] == "ReqcRMCheck")
                    {

                        //this. lblReqNarr.Visible=false;
                        //this.txtReqNarr.Visible = false;                       
                        this.lblCCDNarr.Visible = true;
                        this.txtCCDNarr.Visible = true;


                    }

                    else if (this.Request.QueryString["InputType"] == "ReqCheck")
                    {

                        //this.lblReqNarr.Visible = false;
                        //this.txtReqNarr.Visible = false; 
                        this.lblCCDNarr.Visible = true;
                        this.txtCCDNarr.Visible = true;
                        this.lblCCDNarr.Text = "Est-Narr :";

                    }

                    else
                    {
                        ((CheckBox)this.gvReqInfo.FooterRow.FindControl("crChkbox")).Visible = false;

                    }


                    break;

                default:

                    ((CheckBox)this.gvReqInfo.FooterRow.FindControl("crChkbox")).Visible = false;

                    break;
            }

            string reqcheckorapp = comcod == "3340" ? "Approval" : "Checked";
            ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnCheecked")).Text = reqcheckorapp;



            ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvReqInfo.PageSize);
            ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
            {
                ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvReqInfo.PageIndex;
            }
            this.lbtnResFooterTotal_Click(null, null);
        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            this.gvReqInfo.PageIndex = ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvResInfo_DataBind();
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            //((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTReqAmt")).Text =
            //    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(preqamt)", "")) ?
            //        0.00 : tbl1.Compute("Sum(preqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(areqamt)", "")) ?
                0.00 : tbl1.Compute("Sum(areqamt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            int TblRowIndex2;

            string Rsircode = "000000000000";
            double chkqty = 0.00;
            for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
            {

                TblRowIndex2 = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;

                string Resocde = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

                double dgvBgdQty = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["bbgdqty1"]);
                double bbgdamt = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["bbgdamt1"]);
                double dgvReqQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));


                if (this.Request.QueryString["InputType"] == "Entry")
                {
                    if (this.chkneBudget.Checked)
                    {

                        string comcod = this.GetCompCode();


                        switch (comcod)
                        {
                            case "3336":
                            case "3305":
                            case "3306":
                            case "3310":
                            case "3311":
                            case "2305":

                                if (dgvBgdQty < dgvReqQty)
                                {
                                    // bbgdamt < 0 ||

                                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                    return;

                                }
                                if (Rsircode == Resocde)
                                {
                                    chkqty = chkqty - dgvReqQty;
                                    if (chkqty < 0)
                                    {
                                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    chkqty = dgvBgdQty - dgvReqQty;
                                }
                                Rsircode = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

                                break;


                            default:

                                if (dgvBgdQty < dgvReqQty)
                                {
                                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                    return;

                                }
                                if (Rsircode == Resocde)
                                {
                                    chkqty = chkqty - dgvReqQty;
                                    if (chkqty < 0)
                                    {
                                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    chkqty = dgvBgdQty - dgvReqQty;
                                }
                                Rsircode = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

                                break;




                        }





                    }
                }



                double dgvApprQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text.Trim()));
                double dgvReqRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text.Trim()));
                double dgvReqsRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvReqsRat")).Text.Trim()));
                double dgvStokQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text.Trim()));
                string dgvUseDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvUseDat")).Text.Trim();
                string dgvSupDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvpursupDat")).Text.Trim();
                string dgvReqNote = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqNote")).Text.Trim();
                double dgvReqAmt = dgvReqQty * dgvReqRat;
                double dgvApprAmt = dgvApprQty * dgvReqRat;
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text = dgvReqQty.ToString("#,##0.000;(#,##0.000); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text = dgvApprQty.ToString("#,##0.000;(#,##0.000); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text = dgvReqRat.ToString("#,##0.0000;(#,##0.0000); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text = dgvStokQty.ToString("#,##0.000;(#,##0.000); ");
                //((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTResAmt")).Text = dgvReqAmt.ToString("#,##0.000;(#,##0.000); ");
                ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTAprAmt")).Text = dgvApprAmt.ToString("#,##0.000;(#,##0.000); ");



                tbl1.Rows[TblRowIndex2]["chqty"] = chkqty;
                tbl1.Rows[TblRowIndex2]["preqty"] = dgvReqQty;
                tbl1.Rows[TblRowIndex2]["areqty"] = dgvApprQty;
                tbl1.Rows[TblRowIndex2]["reqrat"] = dgvReqRat;
                tbl1.Rows[TblRowIndex2]["reqsrat"] = dgvReqsRat < dgvReqRat ? dgvReqRat : dgvReqsRat;
                tbl1.Rows[TblRowIndex2]["preqamt"] = dgvReqAmt;
                tbl1.Rows[TblRowIndex2]["areqamt"] = dgvApprAmt;
                tbl1.Rows[TblRowIndex2]["pstkqty"] = dgvStokQty;
                tbl1.Rows[TblRowIndex2]["expusedt"] = dgvUseDat;
                tbl1.Rows[TblRowIndex2]["pursdate"] = dgvSupDat;
                tbl1.Rows[TblRowIndex2]["reqnote"] = dgvReqNote;

            }
            ViewState["tblReq"] = tbl1;
        }

        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {
            string mResCode = this.ddlResList.SelectedValue.ToString().Substring(0, 9);
            string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();
            DataRow[] dr = dt.Select("spcfcod='" + spcfcod1 + "'");
            if (dr.Length > 0)
            {
                this.ddlResSpcf.SelectedValue = spcfcod1;
            }

        }



        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            string approved = "";
            switch (comcod)
            {
                case "3315"://Assure
                case "3316":
                case "3317":
                case "1205"://p2p
                case "3351":
                case "3352":

                    break;

                default:
                    approved = "approved";
                    break;
            }

            string mProject = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = "%" + this.txtResSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "MATCODELIST", mSrchTxt, mProject, approved, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblMat"] = ds1.Tables[0];
            ViewState["tblSpcf"] = ds1.Tables[1];
            ViewState["tblcat"] = ds1.Tables[2];


            // Catagory

            this.ddlCatagory.DataTextField = "catdesc";
            this.ddlCatagory.DataValueField = "catcode";
            this.ddlCatagory.DataSource = ds1.Tables[2];
            this.ddlCatagory.DataBind();
            this.ddlResourceBound();




        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblMat"];
            int tindex = dt.Rows.Count;
            if (tindex > 15)
            {
                string rsircode = this.ddlResList.SelectedValue.ToString();
                int sindex = Convert.ToInt16((dt.Select("rsircode='" + rsircode + "'"))[0]["rowid"].ToString());

                DataTable dt2 = dt.Clone();
                int rowid = 1;
                for (int i = sindex - 1; i < tindex; i++)
                {
                    DataRow dr1 = dt2.NewRow();

                    //            comcod, pactcode, rsircode,bgdqty, bgdrat, treceived, bbgdqty, bbgdqty1, bgdamt,rcvamt, bbgdamt, bbgdamt1, stkqty, rsirdesc1 , 
                    //rsirdesc2, rsirunit, stdrat,tbgdqty,tbbgdqty

                    dr1["rowid"] = rowid;
                    dr1["pactcode"] = dt.Rows[i]["pactcode"].ToString();
                    dr1["rsircode"] = dt.Rows[i]["rsircode"].ToString();
                    dr1["rsirdesc"] = dt.Rows[i]["rsirdesc"].ToString();
                    dr1["rsirdesc1"] = dt.Rows[i]["rsirdesc1"].ToString();
                    dr1["rsirdesc2"] = dt.Rows[i]["rsirdesc2"].ToString();
                    dr1["rsirunit"] = dt.Rows[i]["rsirunit"].ToString();
                    dr1["bgdqty"] = dt.Rows[i]["bgdqty"].ToString();
                    dr1["bgdrat"] = dt.Rows[i]["bgdrat"].ToString();
                    dr1["treceived"] = dt.Rows[i]["treceived"].ToString();
                    dr1["bbgdqty"] = dt.Rows[i]["bbgdqty"].ToString();
                    dr1["bbgdqty1"] = dt.Rows[i]["bbgdqty1"].ToString();
                    dr1["bgdamt"] = dt.Rows[i]["bgdamt"].ToString();
                    dr1["bbgdamt"] = dt.Rows[i]["bbgdamt"].ToString();
                    dr1["bbgdamt1"] = dt.Rows[i]["bbgdamt1"].ToString();
                    dr1["stkqty"] = dt.Rows[i]["stkqty"].ToString();
                    dr1["stdrat"] = dt.Rows[i]["stdrat"].ToString();
                    dr1["tbgdqty"] = dt.Rows[i]["tbgdqty"].ToString();
                    dr1["tbbgdqty"] = dt.Rows[i]["tbbgdqty"].ToString();
                    rowid++;
                    dt2.Rows.Add(dr1);

                }


                for (int i = 0; i < sindex - 1; i++)
                {
                    DataRow dr1 = dt2.NewRow();
                    dr1["rowid"] = rowid;
                    dr1["pactcode"] = dt.Rows[i]["pactcode"].ToString();
                    dr1["rsircode"] = dt.Rows[i]["rsircode"].ToString();
                    dr1["rsirdesc"] = dt.Rows[i]["rsirdesc"].ToString();
                    dr1["rsirdesc1"] = dt.Rows[i]["rsirdesc1"].ToString();
                    dr1["rsirdesc2"] = dt.Rows[i]["rsirdesc2"].ToString();
                    dr1["rsirunit"] = dt.Rows[i]["rsirunit"].ToString();
                    dr1["bgdqty"] = dt.Rows[i]["bgdqty"].ToString();
                    dr1["bgdrat"] = dt.Rows[i]["bgdrat"].ToString();
                    dr1["treceived"] = dt.Rows[i]["treceived"].ToString();
                    dr1["bbgdqty"] = dt.Rows[i]["bbgdqty"].ToString();
                    dr1["bbgdqty1"] = dt.Rows[i]["bbgdqty1"].ToString();
                    dr1["bgdamt"] = dt.Rows[i]["bgdamt"].ToString();
                    dr1["bbgdamt"] = dt.Rows[i]["bbgdamt"].ToString();
                    dr1["bbgdamt1"] = dt.Rows[i]["bbgdamt1"].ToString();
                    dr1["stkqty"] = dt.Rows[i]["stkqty"].ToString();
                    dr1["stdrat"] = dt.Rows[i]["stdrat"].ToString();
                    dr1["tbgdqty"] = dt.Rows[i]["tbgdqty"].ToString();
                    dr1["tbbgdqty"] = dt.Rows[i]["tbbgdqty"].ToString();
                    rowid++;
                    dt2.Rows.Add(dr1);

                }
                ViewState["tblMat"] = dt2;

                this.ddlResourceBound();



            }

            else
            {

                this.ImgbtnSpecification_Click(null, null);
            }

            //else
            //{
            //    this.ddlResourceBound();
            //}


        }


        private void ddlResourceBound()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            string TextField = (ddldesc == "True" ? "rsirdesc" : "rsirdesc1");
            DataTable dt = (DataTable)ViewState["tblMat"];
            this.ddlResList.Items.Clear();
            string catcode = this.ddlCatagory.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            if (catcode == "000000000000") ;
            else
            {
                catcode = catcode.Substring(0, 4) + "%";
                dv.RowFilter = ("rsircode  like '" + catcode + "'");
            }

            this.ddlResList.DataTextField = TextField;
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = dv.ToTable();
            this.ddlResList.DataBind();

            this.ImgbtnSpecification_Click(null, null);
        }




        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {


            string Type = Request.QueryString["InputType"].ToString();
            if (Type == "Entry" || Type == "FxtAstEntry")
            {
                this.ddlPrevReqList.Items.Clear();
            }
            else
            {
                this.ImgbtnFindReq_Click(null, null);
                //this.ImgbtnFindReq_Click(null, null);

            }
            //this.ImgbtnSpecification_Click(null, null); //w


            string actcode = this.ddlProject.SelectedValue.ToString().Trim();
            if (ASTUtility.Left(actcode, 4) == "1102")
            {
                this.uPrj.Visible = true;
                this.Load_Project_To_Combo();
            }
            else
            {
                this.uPrj.Visible = false;
                this.ddlPrjForUse.Items.Clear();
            }



        }
        protected void gvReqInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblReq"];
            string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string rescode = ((Label)this.gvReqInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQFORSPCRES",
                        mREQNO, rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rsircode<>''");
                ViewState["tblReq"] = dv.ToTable();
                this.gvResInfo_DataBind();
            }

        }
        //protected void lnkDeleteReqNo_Click(object sender, EventArgs e)
        //{

        //    string comcod =this.GetCompCode();
        //    DataTable dt = (DataTable)Session["tblReq"];
        //    string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

        //    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQNO",  mREQNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        //    if (!result) 
        //    {
        //       ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Failed";
        //        return;

        //    }

        //   ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";
        // }


        private string CompanyLength()
        {
            string comcod = this.GetCompCode();
            string length = "";
            switch (comcod)
            {
                case "3101":
                case "3340":
                    length = "length";
                    break;


                default:
                    length = "";
                    break;
            }

            return length;

        }


        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string length = this.CompanyLength();
            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit"
                : (Request.QueryString["InputType"].ToString() == "ReqCheck") ? "ReqCheck"
                : (Request.QueryString["InputType"].ToString() == "ReqcRMCheck") ? "ReqcRMCheck"
                : (Request.QueryString["InputType"].ToString() == "ReqFirstApproved") ? "ReqFirstApproved"
                : (Request.QueryString["InputType"].ToString() == "ReqSecondApproved") ? "ReqSecondApproved"

                : (Request.QueryString["InputType"].ToString() == "LcEntry") ? "FxtAst"
                : (Request.QueryString["InputType"].ToString() == "LcApproval") ? "LcApproval" : "";




            string prjcode = ((Request.QueryString["InputType"].ToString() == "Approval") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "ReqEdit") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "HeadUsed") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "ReqCheck") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "ReqcRMCheck") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "ReqFirstApproved") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "ReqSecondApproved") ? this.ddlProject.SelectedValue.ToString()

                : "") + "%";


            string mrfno = (this.Request.QueryString["genno"].ToString().Length == 0) ? this.txtSrchMrfNo.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREVREQLIST", CurDate1,
                          prjcode, fxtast, mrfno, length, usrid, "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevReqList.Items.Clear();
            this.ddlPrevReqList.DataTextField = "reqno1";
            this.ddlPrevReqList.DataValueField = "reqno";
            this.ddlPrevReqList.DataSource = ds1.Tables[0];
            this.ddlPrevReqList.DataBind();


        }
        protected void lbtnResFooterDelete_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblReq"];
            string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQNO", mREQNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Delete Requisition";
                string eventdesc2 = "Req No- " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ImgbtnFindProjectName_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.Load_Project_Combo();
            //this.ImgbtnSpecification_Click(null, null);
        }
        protected void ImgbtnReqse_Click(object sender, EventArgs e)
        {

        }

        protected void gvReqInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvReqInfo.EditIndex = -1;
            this.gvResInfo_DataBind();
        }
        protected void gvReqInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvReqInfo.EditIndex = e.NewEditIndex;
            this.gvResInfo_DataBind();



            string comcod = this.GetCompCode();
            int index = (this.gvReqInfo.PageIndex) * this.gvReqInfo.PageSize + e.NewEditIndex;
            string mSrchTxt = "%";
            string mResCode = ((Label)this.gvReqInfo.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            string mSupCode = ((Label)this.gvReqInfo.Rows[e.NewEditIndex].FindControl("lblgvsupliercode")).Text.Trim();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVSUPLIST", mSrchTxt, mResCode, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            if (ds2.Tables[0].Rows.Count == 0)
                return;

            DropDownList ddl2 = (DropDownList)this.gvReqInfo.Rows[e.NewEditIndex].FindControl("ddlSupname");
            ddl2.DataTextField = "ssirdesc1";
            ddl2.DataValueField = "ssircode";
            ddl2.DataSource = ds2.Tables[0];
            ddl2.DataBind();
            ddl2.SelectedValue = mSupCode;






        }
        protected void gvReqInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            string Storecode = ((DropDownList)this.gvReqInfo.Rows[e.RowIndex].FindControl("ddlStorename")).SelectedValue.ToString();
            string StoreDesc = ((DropDownList)this.gvReqInfo.Rows[e.RowIndex].FindControl("ddlStorename")).SelectedItem.Text.Trim();

            string ssircode = ((DropDownList)this.gvReqInfo.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedValue.ToString();
            string ssirdesc = ((DropDownList)this.gvReqInfo.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedItem.Text.Trim();
            double dgvReqRat = Convert.ToDouble("0" + ((TextBox)this.gvReqInfo.Rows[e.RowIndex].FindControl("txtgvResRat")).Text.Trim());

            int index = (this.gvReqInfo.PageIndex) * this.gvReqInfo.PageSize + e.RowIndex;
            tbl1.Rows[index]["storecode"] = Storecode;
            tbl1.Rows[index]["storedesc"] = StoreDesc;
            tbl1.Rows[index]["ssircode"] = ssircode;
            tbl1.Rows[index]["ssirdesc"] = ssirdesc;
            tbl1.Rows[index]["reqrat"] = dgvReqRat;
            ViewState["tblReq"] = tbl1;
            this.gvReqInfo.EditIndex = -1;
            this.gvResInfo_DataBind();
        }
        protected void lbtnSurVey_Click(object sender, EventArgs e)
        {

        }


        protected void ddlCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlResourceBound();
        }
        protected void lbtnAddspecifiation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }

        protected void lbtnUpdateSpeDetails_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string Desc = this.txtspcfdesc.Text.Trim();
            string sircode = this.ddlResList.SelectedValue.ToString().Substring(0, 9);
            List<RealEntity.C_17_Acc.EClassSpecification.EClassLastSpcfcodeofRes> lst = objuserman.GetLastSpeciCode(comcod, sircode);
            string spcfcod = lst[0].spcfcod;
            bool result = this.purData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTUPDATE", spcfcod.Substring(0, 2), spcfcod, Desc, "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                return;
            }




            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }



    }

}
