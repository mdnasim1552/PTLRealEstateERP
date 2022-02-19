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
using System.Net;
using RealEntity;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_28_MPro
{
    public partial class MKTPurReqEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        UserManGenAccount objuserman = new UserManGenAccount();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                //this.txtReqText.Enabled = false;
                // this.ImgbtnReqse.Enabled = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];

                //this.CompBudgetexceed();
                this.chkdupMRF.Enabled = false;
                this.chkdupMRF.Checked = true;
                this.chkneBudget.Enabled = false;
                this.chkneBudget.Checked = true;
                //this.DupMRR();
                this.Load_Project_Combo();
                this.GetPRType();
                this.GetMarkType();

                this.VisibleGrid();
                this.lblmrfno.Text = ReadCookie();
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

                this.CalendarExtender_txtCurReqDate.EndDate = System.DateTime.Today;

                //only current date

                this.CurDate();
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS REQUISITION";

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
                    this.CalendarExtender_txtCurReqDate.StartDate = System.DateTime.Today;
                    this.txtCurReqDate.ReadOnly = true;

                    break;

            }


        }
        private string ReadCookie()
        {
            HttpCookie nameCookie = Request.Cookies["MRF"];
            string refno = nameCookie != null ? nameCookie.Value.Split('=')[1] : "Mrf No";
            return refno;
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            ReFindProject = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" : this.Request.QueryString["prjcode"].ToString() + "%";
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
        protected void GetPRType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit"
                        : (this.Request.QueryString["InputType"].ToString() == "HeadUsed") ? "HeadUsed" : "";


            string Aproval = (this.Request.QueryString["InputType"].ToString() == "Approval") ? "Aproval" : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "Aproval" : "";

            string CallType = (this.Request.QueryString["InputType"].ToString() == "Entry" || this.Request.QueryString["InputType"].ToString() == "LcEntry") ? "GET_MKT_DDL_LIST" : "PRJCODELIST";

            string userid = hst["usrid"].ToString();
            string type = this.Request.QueryString["InputType"];
            string ReFindProject;
            ReFindProject = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" : this.Request.QueryString["prjcode"].ToString() + "%";

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", CallType, ReFindProject, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            string gcod = "62%";
            DataView dv = ds2.Tables[0].Copy().DefaultView;
            dv.RowFilter = ("gcod  like '" + gcod + "'");

            this.ddlPRType.DataTextField = "gdesc";
            this.ddlPRType.DataValueField = "gcod";
            this.ddlPRType.DataSource = dv.ToTable();
            this.ddlPRType.DataBind();

            ViewState["tblddllist"] = ds2.Tables[0];
            this.ddlPRType_SelectedIndexChanged(null, null);

        }
        protected void GetMarkType()
        {
            DataTable dt = (DataTable)ViewState["tblddllist"];
            string gcod = "64%";
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("gcod  like '" + gcod + "'");

            this.ddlMarkType.DataTextField = "gdesc";
            this.ddlMarkType.DataValueField = "gcod";
            this.ddlMarkType.DataSource = dv.ToTable();
            this.ddlMarkType.DataBind();

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

                //this.txtSrchMrfNo.Visible = true;
                this.lblpreReq.Visible = true;
                this.ImgbtnFindReq.Visible = true;
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
                this.ddlProject.Visible = true;

                this.ddlFloor.Visible = false;
                this.lblddlFloor.Visible = false;
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.lblCurReqNo1.Text = "MREQ" + DateTime.Today.ToString("MM") + "-";
                this.txtCurReqDate.Enabled = true;
                this.txtMRFNo.Text = "";

                //this.txtResSearch.Text = "";
                this.ddlResSpcf.Items.Clear();
                this.ddlResList.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtExpDeliveryDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtReqNarr.Text = "";
                this.gvReqInfo.DataSource = null;
                this.gvReqInfo.DataBind();
                this.ddlProject.Enabled = true;

                this.pnlSpeDet.Visible = false;
                this.Panel2.Visible = false;
                //this.PnlDesc.Visible = false;
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
                    //this.txtReqText.Visible = false;
                    //this.ImgbtnReqse.Visible = false;
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
                // this.txtReqText.Visible = true;
                //this.ImgbtnReqse.Visible = true;

                //this.Panel1.Visible = false;

            }


            //this.txtSrchMrfNo.Visible = false;
            this.lblpreReq.Visible = false;
            this.ImgbtnFindReq.Visible = false;
            this.ddlPrevReqList.Visible = false; //
            this.ddlProject.Enabled = false;
            this.lblddlFloor.Text = this.ddlFloor.SelectedItem.Text.Trim();
            this.ddlFloor.Visible = false;
            this.lblddlFloor.Visible = false;
            // this.txtCurReqDate.ReadOnly = true;
            this.txtCurReqNo2.ReadOnly = true;
            this.pnlSpeDet.Visible = true;
            this.Panel2.Visible = true;
            //this.PnlDesc.Visible = false;
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
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_LAST_REQ_INFO", mREQDAT,
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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_MKT_PUR_REQ_INFO", mReqNo, CurDate1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblReq"] = this.HiddenSameData(ds1.Tables[0]);
            Session["tblUserReq"] = ds1.Tables[1];
            ViewState["tblreqdesc"] = ds1.Tables[2];

            if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "FxtAstApproval" || Request.QueryString["InputType"].ToString() == "HeadUsed")
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.GetApprQty();
                }
            }

            if (mReqNo == "NEWREQ")
            {
               DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_LAST_REQ_INFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);
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
            }
            else
            {
                this.uPrj.Visible = false; ;
            }

            this.ddlFloor.SelectedValue = ds1.Tables[1].Rows[0]["flrcod"].ToString();
            //this.lblddlProject.Text = (this.ddlProject.Items.Count == 0 ? "XXX" : this.ddlProject.SelectedItem.Text.Trim());
            //this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.ddlProject.Enabled=false;
            this.lblddlFloor.Text = (this.ddlFloor.Items.Count == 0 ? "YYY" : this.ddlFloor.SelectedItem.Text.Trim());
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["reqbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtExpDeliveryDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["eddat"]).ToString("dd.MM.yyyy");
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            //this.ddlptype.SelectedValue = ds1.Tables[1].Rows[0]["ptype"].ToString();
            this.gvResInfo_DataBind();
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
                dr1["rsirdesc1"] = ddldesc == "True" ? this.ddlResList.SelectedItem.Text.Trim() : this.ddlResList.SelectedItem.Text.Trim().Substring(14);
                dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["bbgdqty1"] = dr3[0]["bbgdqty1"];
                dr1["prtype"] = this.ddlPRType.SelectedValue.ToString();
                dr1["acttype"] = this.ddlActType.SelectedValue.ToString();
                dr1["mkttype"] = this.ddlMarkType.SelectedValue.ToString();
                dr1["prdesc"] = this.ddlPRType.SelectedItem.Text.Trim();
                dr1["actdesc"] = this.ddlActType.SelectedItem.Text.Trim();
                dr1["mktdesc"] = this.ddlMarkType.SelectedItem.Text.Trim(); 
                dr1["preqty"] = 0;
                dr1["areqty"] = 0;
                dr1["reqrat"] = 0;
                dr1["expusedt"] = "";
                dr1["reqnote"] = "";
                tbl1.Rows.Add(dr1);



            }

            ViewState["tblReq"] = this.HiddenSameData(tbl1);
            this.gvResInfo_DataBind();

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

        }

        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "M.R.F No. Should Not Be Empty" + "');", true);
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Found Duplicate M.R.F No" + "');", true);
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


            string EditByid = (this.Request.QueryString["InputType"] == "ReqEdit") ? userid : "";
            string Edittrmid = (this.Request.QueryString["InputType"] == "ReqEdit") ? Terminal : "";
            string EditSession = (this.Request.QueryString["InputType"] == "ReqEdit") ? Sessionid : "";
            string EditDat = (this.Request.QueryString["InputType"] == "ReqEdit") ? Date : "01-Jan-1900";


           // Budget quantity Cheecked after complete comment out.
            //if (this.Request.QueryString["InputType"] == "Entry" || this.Request.QueryString["InputType"] == "FxtAstEntry")
            //{
            //    // Emty Quantity
            //    DataRow[] drempty = tbl1.Select("preqty<=0");
            //    if (drempty.Length > 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Aprove Qty Must be Less Or Equal  Req. Qty" + "');", true);
            //        return;
            //    }

            //    int index;
            //    string Rsircode = "000000000000";
            //    double chkqty = 0.00;
            //    for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
            //    {

            //        index = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;

            //        string Resocde = tbl1.Rows[index]["rsircode"].ToString();
            //        double dgvBgdQty = Convert.ToDouble(tbl1.Rows[index]["bbgdqty1"]);
            //        double dgvReqQty = Convert.ToDouble(ASTUtility.ExprToValue("0" +((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));


            //        if (this.Request.QueryString["InputType"] == "Entry")
            //        {
            //            if (this.chkneBudget.Checked)
            //            {
            //                if (dgvBgdQty < dgvReqQty)
            //                {
            //                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
            //                    return;

            //                }
            //                else if (Rsircode == Resocde)
            //                {
            //                    chkqty = chkqty - dgvReqQty;
            //                    if (chkqty < 0)
            //                    {
            //                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
            //                        return;
            //                    }
            //                }
            //                else
            //                {
            //                    chkqty = dgvBgdQty - dgvReqQty;
            //                }
            //                Rsircode = tbl1.Rows[index]["rsircode"].ToString();
            //            }
            //        }


            //    }



            //}

            string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();       
            string mREQNAR = this.txtReqNarr.Text.Trim();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT", "UPDATE_MKT_REQ_INFO", "MKTREQB", mREQNO, mREQDAT, mPACTCODE, mMRFNO, PostedByid, Posttrmid, PostSession, PostedDat,
               EditByid, Edittrmid, EditSession, EditDat, mREQNAR);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string rowId = i.ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();

                double mPREQTY = Convert.ToDouble(tbl1.Rows[i]["preqty"]);
                double mAREQTY = Convert.ToDouble(tbl1.Rows[i]["areqty"]);
                string mREQRAT = tbl1.Rows[i]["reqrat"].ToString();
                string prType = tbl1.Rows[i]["prtype"].ToString();
                string mrkType = tbl1.Rows[i]["acttype"].ToString();
                string actType = tbl1.Rows[i]["mkttype"].ToString();
                string expectDate =tbl1.Rows[i]["expusedt"].ToString();
                string filePath = tbl1.Rows[i]["filepath"].ToString();

                if (mPREQTY >= mAREQTY)
                {
                    result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT", "UPDATE_MKT_REQ_INFO", "MKTREQA",
                                mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, prType, mrkType, actType,
                                expectDate, filePath, "", "", "", "", "", "");


                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Aprove Qty Must be Less Or Equal  Req. Qty" + "');", true);
                    return;

                }

            }

            this.txtCurReqDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Requisition Updated successfully" + "');", true);



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

        }

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



            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEREQCHECKED", mREQNO, checkusrid, checkTerminal, checkSessionid, checkDate, Approval, crmData, crmNarr,
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Updated successfully" + "');", true);



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

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Updated successfully" + "');", true);



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
            this.gvReqInfo.DataBind();
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

        }
        private void Session_tblReq_Update()
        {
            //DataTable tbl1 = (DataTable)ViewState["tblReq"];
            //int TblRowIndex2;

            //string Rsircode = "000000000000";
            //double chkqty = 0.00;
            //for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
            //{

            //    TblRowIndex2 = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;

            //    string Resocde = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

            //    double dgvBgdQty = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["bbgdqty1"]);
            //    double bbgdamt = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["bbgdamt1"]);
            //    double dgvReqQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));


            //    if (this.Request.QueryString["InputType"] == "Entry")
            //    {
            //        if (this.chkneBudget.Checked)
            //        {

            //            string comcod = this.GetCompCode();


            //            switch (comcod)
            //            {
            //                case "3336":
            //                case "3305":
            //                case "3306":
            //                case "3310":
            //                case "3311":
            //                case "2305":

            //                    if (dgvBgdQty < dgvReqQty)
            //                    {
            //                        // bbgdamt < 0 ||

            //                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
            //                        return;

            //                    }
            //                    if (Rsircode == Resocde)
            //                    {
            //                        chkqty = chkqty - dgvReqQty;
            //                        if (chkqty < 0)
            //                        {
            //                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
            //                            return;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        chkqty = dgvBgdQty - dgvReqQty;
            //                    }
            //                    Rsircode = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

            //                    break;


            //                default:

            //                    if (dgvBgdQty < dgvReqQty)
            //                    {
            //                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
            //                        return;

            //                    }
            //                    if (Rsircode == Resocde)
            //                    {
            //                        chkqty = chkqty - dgvReqQty;
            //                        if (chkqty < 0)
            //                        {
            //                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
            //                            return;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        chkqty = dgvBgdQty - dgvReqQty;
            //                    }
            //                    Rsircode = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

            //                    break;




            //            }

            //        }
            //    }



            //    double dgvApprQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text.Trim()));
            //    double dgvReqRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text.Trim()));
            //    double dgvReqsRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvReqsRat")).Text.Trim()));
            //    double dgvStokQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text.Trim()));
            //    string dgvUseDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvUseDat")).Text.Trim();
            //    string dgvSupDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvpursupDat")).Text.Trim();
            //    string dgvReqNote = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqNote")).Text.Trim();
            //    double dgvReqAmt = dgvReqQty * dgvReqRat;
            //    double dgvApprAmt = dgvApprQty * dgvReqRat;
            //    ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text = dgvReqQty.ToString("#,##0.000;(#,##0.000); ");
            //    ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text = dgvApprQty.ToString("#,##0.000;(#,##0.000); ");
            //    ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text = dgvReqRat.ToString("#,##0.0000;(#,##0.0000); ");
            //    ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text = dgvStokQty.ToString("#,##0.000;(#,##0.000); ");
            //    //((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTResAmt")).Text = dgvReqAmt.ToString("#,##0.000;(#,##0.000); ");
            //    ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTAprAmt")).Text = dgvApprAmt.ToString("#,##0.000;(#,##0.000); ");



            //    tbl1.Rows[TblRowIndex2]["chqty"] = chkqty;
            //    tbl1.Rows[TblRowIndex2]["preqty"] = dgvReqQty;
            //    tbl1.Rows[TblRowIndex2]["areqty"] = dgvApprQty;
            //    tbl1.Rows[TblRowIndex2]["reqrat"] = dgvReqRat;
            //    tbl1.Rows[TblRowIndex2]["reqsrat"] = dgvReqsRat < dgvReqRat ? dgvReqRat : dgvReqsRat;
            //    tbl1.Rows[TblRowIndex2]["preqamt"] = dgvReqAmt;
            //    tbl1.Rows[TblRowIndex2]["areqamt"] = dgvApprAmt;
            //    tbl1.Rows[TblRowIndex2]["pstkqty"] = dgvStokQty;
            //    tbl1.Rows[TblRowIndex2]["expusedt"] = dgvUseDat;
            //    tbl1.Rows[TblRowIndex2]["pursdate"] = dgvSupDat;
            //    tbl1.Rows[TblRowIndex2]["reqnote"] = dgvReqNote;

            //}
            //ViewState["tblReq"] = tbl1;
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

            string mProject = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_MKT_MATERAILS", mSrchTxt, mProject, approved, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblcat"] = ds1.Tables[0];
            ViewState["tblMat"] = ds1.Tables[1];
            ViewState["tblSpcf"] = ds1.Tables[2];


            // Catagory
            this.ddlCatagory.DataTextField = "catdesc";
            this.ddlCatagory.DataValueField = "catcode";
            this.ddlCatagory.DataSource = ds1.Tables[0];
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


            string mrfno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Deleted Failed" + "');", true);
                return;

            }


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Deleted Successfully" + "');", true);

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
            FileUpload fileUpload = gvReqInfo.Rows[e.RowIndex].FindControl("FileUpload1") as FileUpload;
            fileUpload.SaveAs(System.IO.Path.Combine(Server.MapPath("Images"), fileUpload.FileName));
            //SqlDataSource1.UpdateParameters["Image"].DefaultValue = "~/Images/" + fileUpload.FileName;

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
            tbl1.Rows[index]["filepath"] = "~/Images/" + fileUpload.FileName;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" +  purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Material Specification Update Successfully" + "');", true);
            }

        }

        protected void ddlPRType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblddllist"];
            string code = this.ddlPRType.SelectedValue;
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "code like ('" + code + "')";
            this.ddlActType.DataTextField = "gdesc";
            this.ddlActType.DataValueField = "gcod";
            this.ddlActType.DataSource = dv1.ToTable();
            this.ddlActType.DataBind();
        }

        protected void lbtngvReqDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblReq"];
            int gvRowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + gvRowIndex;
            string rescode = dt.Rows[rowindex]["rsircode"].ToString();
            string comcod = this.GetCompCode();
            string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQFORSPCRES",
                        mREQNO, rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" +  purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            else 
            {               
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rsircode<>''");
                ViewState["tblReq"] = dv.ToTable();
                this.gvResInfo_DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Requisition Deleted Successfully" + "');", true);
            }

        }
    }
}