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
namespace RealERPWEB.F_12_Inv
{

    public partial class PurReqEntry02 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                // for Mettroo
                this.txtReqText.Enabled = false;
                this.ImgbtnReqse.Enabled = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];

                this.chkdupMRF.Enabled = false;
                this.chkdupMRF.Checked = true;
                this.chkneBudget.Enabled = false;
                this.chkneBudget.Checked = true;

                // this.DupMRR();
                this.Load_Project_Combo();
                this.VisibleGrid();
                this.lbtnOk.Text = "New";
                this.lbtnOk_Click(null, null);
                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["InputType"].ToString() == "Entry") ? "Materials Requisition Information Input/Edit Screen"
                //    : (Request.QueryString["InputType"].ToString() == "Approval") ? "Materials Requisition Approval Screen"
                //    : (Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "Fixed Assets Requisition Information Input/Edit Screen"
                //     : (Request.QueryString["InputType"].ToString() == "ReqEdit") ? "Materials Requisition Information Input/Edit Screen"
                //     : (Request.QueryString["InputType"].ToString() == "HeadUsed") ? "Material Requisition Input (Central Worehouse)-(H/O Used)" : "Fixed Assets Requisition Approval Screen";


            }




        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
                    this.gvReqInfo.Columns[12].Visible = true;
                    this.gvReqInfo.Columns[13].Visible = true;
                    this.gvReqInfo.Columns[14].Visible = true;
                    this.gvReqInfo.Columns[15].Visible = true;
                    this.gvReqInfo.Columns[16].Visible = true;

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
            string comcod = this.GetCompCode();
            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit"
                        : (this.Request.QueryString["InputType"].ToString() == "HeadUsed") ? "HeadUsed" : "";

            string Aproval = (this.Request.QueryString["InputType"].ToString() == "Approval") ? "Aproval" : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "Aproval" : "";

            string CallType = (this.Request.QueryString["InputType"].ToString() == "Entry" && comcod == "3301") ? "PRJCODELIST1" : "PRJCODELIST";

            string userid = hst["usrid"].ToString();
            string ReFindProject = "%" + this.txtProjectSearch.Text.Trim() + "%";

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", CallType, ReFindProject, fxtast, Aproval, userid, "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();

            this.ddlFloor.DataTextField = "flrdes";
            this.ddlFloor.DataValueField = "flrcod";
            this.ddlFloor.DataSource = ds2.Tables[1];
            this.ddlFloor.DataBind();

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
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
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
                if (Request.QueryString["InputType"].ToString() == "FxtAstApproval" || Request.QueryString["InputType"].ToString() == "ReqEdit" || Request.QueryString["InputType"].ToString() == "HeadUsed")
                {

                    this.chkdupMRF.Visible = false;
                    this.chkneBudget.Visible = false;
                    this.ddlFloor.Visible = false;
                    this.lblddlFloor.Visible = false;
                    //this.lblResList.Visible = false;
                    //this.txtResSearch.Visible = false;
                    //this.ImgbtnFindRes.Visible = false;
                    //this.ddlResList.Visible = false;
                    //this.lblSpecification.Visible = false;
                    //this.txtSrchSpecification.Visible = false;
                    //this.ImgbtnSpecification.Visible = false;
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

                    this.ImgbtnFindReq_Click(null, null);



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
            this.ddlPrevReqList.Visible = false;

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

        //protected void lbtnPrevReqList_Click(object sender, EventArgs e)
        //{

        //    string comcod =this.GetCompCode();
        //    string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst" : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst" : "";
        //    string prjcode = ((Request.QueryString["InputType"].ToString() == "Approval") ?
        //    this.ddlProject.SelectedValue.ToString() : (Request.QueryString["InputType"].ToString() == "FxtAstApproval")? this.ddlProject.SelectedValue.ToString():"") + "%";

        //    string mrfno = this.txtSrchMrfNo.Text.Trim() + "%";
        //    string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREVREQLIST", CurDate1,
        //                  prjcode, fxtast, mrfno, "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    this.ddlPrevReqList.Items.Clear();
        //    this.ddlPrevReqList.DataTextField = "reqno1";
        //    this.ddlPrevReqList.DataValueField = "reqno";
        //    this.ddlPrevReqList.DataSource = ds1.Tables[0];
        //    this.ddlPrevReqList.DataBind();

        //}
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


            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETPURREQINFO", mReqNo, CurDate1,
            //          "", "", "", "", "", "", "");
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
            this.ddlFloor.SelectedValue = ds1.Tables[1].Rows[0]["flrcod"].ToString();
            //this.lblddlProject.Text = (this.ddlProject.Items.Count == 0 ? "XXX" : this.ddlProject.SelectedItem.Text.Trim());
            this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.lblddlFloor.Text = (this.ddlFloor.Items.Count == 0 ? "YYY" : this.ddlFloor.SelectedItem.Text.Trim());
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["reqbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtExpDeliveryDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["eddat"]).ToString("dd.MM.yyyy");
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            this.gvResInfo_DataBind();
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
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            string Specification = this.ddlResSpcf.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + Specification + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                dr1["rsirdesc1"] = this.ddlResList.SelectedItem.Text.Trim().Substring(14);
                dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                DataTable tbl2 = (DataTable)ViewState["tblMat"];


                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "' and spcfcod='" + Specification + "'");
                //DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");

                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["bgdqty"] = dr3[0]["bgdqty"];
                dr1["treceived"] = dr3[0]["treceived"];
                dr1["bbgdqty"] = dr3[0]["bbgdqty"];
                dr1["stkqty"] = dr3[0]["stkqty"];


                dr1["preqty"] = 0;
                dr1["areqty"] = 0;
                dr1["lpurrate"] = 0;
                dr1["reqrat"] = 0;
                dr1["reqsrat"] = 0;
                dr1["preqamt"] = 0;
                dr1["areqamt"] = 0;


                dr1["pstkqty"] = 0;
                dr1["expusedt"] = "";// DateTime.Today;
                dr1["pursdate"] = "";// DateTime.Today;
                dr1["reqnote"] = "";
                dr1["storecode"] = "";
                tbl1.Rows.Add(dr1);
            }
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
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3101":
                    PrintReq = "PrintReque02";

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
                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string printcomreq = this.CompanyRequisition();

            if (printcomreq == "PrintReque01")
            {
                this.PrintRequisition01();

            }

            else
            {
                this.PrintRequisition02();



            }




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


        private void PrintRequisition02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            string txtcrno = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            string txtcrdate = this.txtCurReqDate.Text.ToString().Trim();
            string txtmrfno = this.txtMRFNo.Text.ToString().Trim();
            string txtprojectname = this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //string txtAddress = dt1.Rows[0]["paddress"].ToString();

            //DataTable dt = ds1.Tables[2];
            DataTable dt = (DataTable)ViewState["tblreqdesc"];

            string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string txtfloorno = dt.Rows[1]["termsdesc"].ToString();
            string txtpforused = dt.Rows[3]["termsdesc"].ToString();

            DataTable dtr = (DataTable)ViewState["tblReq"];

            string txttoamt = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();
            string txttoamt02 = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();
            string rpttxtnaration = this.txtReqNarr.Text.Trim();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "";
            string txtSign2 = "";
            string txtSign3 = "";
            string txtSign4 = "";
            string txtSign5 = "";
            string txtSign6 = "";
            string txtSign7 = "";

            if (comcod == "3330")
            {

                txtSign1 = "Store In-charge";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM (Operation)";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Approved By";
            }

            else if (comcod == "3332" || comcod == "3101")
            {


                txtSign1 = "S.K";

                txtSign2 = "Project Incharge";

                txtSign3 = "Procurement";

                txtSign4 = "Cost & Budget";

                txtSign5 = "Cheif Engineer";

                txtSign6 = "Director";

                txtSign7 = "Managing Director/Chairman";

            }


            else
            {

                txtSign1 = "S.K";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM/AGM/DGM";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Managing Director";
            }

            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry02", lst, null, null);
            Rpt1.EnableExternalImages = true;


            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));

            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", ""));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



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
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(14);

            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString();
            //TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            //txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text =((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();

            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["tblReq"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


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
            string comcod = this.GetCompCode();
            this.lbtnResFooterTotal_Click(null, null);
            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevReqList.Items.Count == 0)
                this.GetReqNo();
            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();


            if (this.chkdupMRF.Checked)
            {
                if (mMRFNO.Length == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "M.R.F No. Should Not Be Empty";
                    return;
                }

                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKEDDUPMRRNO", mMRFNO, "", "", "", "", "", "", "", "");
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
            }


            //Log Entry

            DataTable dtuser = (DataTable)Session["tblUserReq"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPostedDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string PostedByid = (this.Request.QueryString["InputType"] == "Entry") ? userid : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? userid
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["InputType"] == "Entry") ? Terminal : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Terminal
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["InputType"] == "Entry") ? Sessionid : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Sessionid
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string PostedDat = (this.Request.QueryString["InputType"] == "Entry") ? Date : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Date
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Date : (tblPostedSession == "") ? Date : tblPostedDat;
            string ApprovByid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : userid;
            string approvdat = (this.Request.QueryString["InputType"] == "Entry") ? "01-Jan-1900" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "01-Jan-1900"
                : System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Approvtrmid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : Terminal;
            string ApprovSession = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" : Sessionid;



            string approved = (this.Request.QueryString["InputType"] == "Approval") ? "Ok" : (this.Request.QueryString["InputType"] == "FxtAstApproval") ? "Ok" : (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["approved"].ToString();

            //////



            DataTable tbl1 = (DataTable)ViewState["tblReq"];

            // Balance quantity Cheecked
            if (this.Request.QueryString["InputType"] == "FxtAstEntry")
            {



                // Emty Quantity
                DataRow[] drempty = tbl1.Select("preqty<=0");
                if (drempty.Length > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Present Quantity Required";
                    return;
                }

                int index;
                string Rsircode = "000000000000";
                double chkqty = 0.00;
                for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
                {

                    index = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;

                    string Resocde = tbl1.Rows[index]["rsircode"].ToString();
                    double dgvBgdQty = Convert.ToDouble(tbl1.Rows[index]["bbgdqty"]);
                    // double bbgdamt = Convert.ToDouble(tbl1.Rows[index]["bbgdamt1"]);
                    double dgvReqQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));

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
                                    return;

                                }
                                if (Rsircode == Resocde)
                                {
                                    chkqty = chkqty - dgvReqQty;
                                    if (chkqty < 0)
                                    {
                                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
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
                                    return;

                                }
                                if (Rsircode == Resocde)
                                {
                                    chkqty = chkqty - dgvReqQty;
                                    if (chkqty < 0)
                                    {
                                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
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





            string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
            string mFLRCOD = this.ddlFloor.SelectedValue.ToString().Trim();
            string mREQUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mEDDAT = this.GetStdDate(this.txtExpDeliveryDate.Text.Trim()); // DateTime.Today.ToString("dd-MMM-yyyy");
            string mREQBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();

            string mREQNAR = this.txtReqNarr.Text.Trim();
            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQB", mREQNO, mREQDAT, mPACTCODE, mFLRCOD, mREQUSRID, mAPPRUSRID, mAPPRDAT, mEDDAT,

                             mREQBYDES, mAPPBYDES, mMRFNO, mREQNAR, PostedByid, Posttrmid, PostSession, ApprovByid, approvdat, Approvtrmid, ApprovSession, PostedDat, approved, "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
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
                    //    double mBgdBalQty = Convert.ToDouble(tbl1.Rows[i]["bbgdqty"]);
                    //    if (mBgdBalQty < mPREQTY)
                    //    {
                    //       ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                    //      return;UpdateTransInfo

                    //    }

                    //}

                    result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQA",
                               mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                               PursDate, Lpurrate, storecode, ssircode, orderno, mREQSRAT.ToString(), "", "", "", "", "", "", "");


                    //result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQA",
                    //            mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                    //            PursDate, Lpurrate, storecode, "", "", "", "", "", "", "", "", "", "");




                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        return;
                    }
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Aprove Qty Must be Less Or Equal  Req. Qty";
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
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQC",
                                mREQNO, mTERMSID, mTERMSSUBJ, mTERMSDESC, mTERMSRMRK, "", "", "", "",
                                "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }
            }


            this.txtCurReqDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Update Reqisition";
                string eventdesc2 = "Req No- " + mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
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


        protected void gvResInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            this.gvReqInfo.DataSource = tbl1;
            this.gvReqInfo.DataBind();
            if (Request.QueryString["InputType"].ToString() == "Approval") // "Entry"
            {
                for (int i = 0; i < this.gvReqInfo.Rows.Count; i++)
                {
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvReqQty")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvResRat")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvUseDat")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvStokQty")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvReqNote")).ReadOnly = true;
                }
            }

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
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
            {

                TblRowIndex2 = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;
                double dgvBgdQty = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["bbgdqty"]);
                double dgvReqQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));
                if (this.chkneBudget.Checked)
                {
                    if (dgvBgdQty < dgvReqQty)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                        break;

                    }

                }



                double dgvApprQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text.Trim()));
                double dgvReqRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text.Trim()));
                double dgvStokQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text.Trim()));
                string dgvUseDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvUseDat")).Text.Trim();
                string dgvSupDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvpursupDat")).Text.Trim();
                string dgvReqNote = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqNote")).Text.Trim();
                double dgvReqAmt = dgvReqQty * dgvReqRat;
                double dgvApprAmt = dgvApprQty * dgvReqRat;
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text = dgvReqQty.ToString("#,##0.000;(#,##0.000); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text = dgvApprQty.ToString("#,##0.000;(#,##0.000); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text = dgvReqRat.ToString("#,##0.000;(#,##0.00); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text = dgvStokQty.ToString("#,##0.000;(#,##0.000); ");
                //((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTResAmt")).Text = dgvReqAmt.ToString("#,##0.000;(#,##0.000); ");
                ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTAprAmt")).Text = dgvApprAmt.ToString("#,##0.000;(#,##0.000); ");



                tbl1.Rows[TblRowIndex2]["preqty"] = dgvReqQty;
                tbl1.Rows[TblRowIndex2]["areqty"] = dgvApprQty;
                tbl1.Rows[TblRowIndex2]["reqrat"] = dgvReqRat;
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
            // string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            // DataTable tbl1 = (DataTable)ViewState["tblMat"];
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];

            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
            //dv1.RowFilter = "rsircode = '" + mResCode+"'" ;
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();
            //DataRow[] dr = dt.Select("spcfcod='" + spcfcod1 + "'");
            //if (dr.Length > 0)
            //{
            //    this.ddlResSpcf.SelectedValue = spcfcod1;
            //}

        }



        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string mProject = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = this.txtResSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "MATCODELIST", mSrchTxt, mProject, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblMat"] = ds1.Tables[0];
            ViewState["tblSpcf"] = ds1.Tables[2];

            this.ddlResList.DataTextField = "rsirdesc1";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = ds1.Tables[1];
            this.ddlResList.DataBind();
            this.ImgbtnSpecification_Click(null, null);




        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit" : "";

            string prjcode = ((Request.QueryString["InputType"].ToString() == "Approval") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "ReqEdit") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "HeadUsed") ? this.ddlProject.SelectedValue.ToString() : "") + "%";

            string mrfno = this.txtSrchMrfNo.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETPREVREQLIST", CurDate1,
                          prjcode, fxtast, mrfno, "", "", "", "", "");
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
                return;

            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";

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
            this.Load_Project_Combo();
        }
        protected void ImgbtnReqse_Click(object sender, EventArgs e)
        {

        }



        protected void gvReqInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            string Storecode = ((DropDownList)this.gvReqInfo.Rows[e.RowIndex].FindControl("ddlStorename")).SelectedValue.ToString();
            string StoreDesc = ((DropDownList)this.gvReqInfo.Rows[e.RowIndex].FindControl("ddlStorename")).SelectedItem.Text.Trim();
            double dgvReqRat = Convert.ToDouble("0" + ((TextBox)this.gvReqInfo.Rows[e.RowIndex].FindControl("txtgvResRat")).Text.Trim());

            int index = (this.gvReqInfo.PageIndex) * this.gvReqInfo.PageSize + e.RowIndex;
            tbl1.Rows[index]["storecode"] = Storecode;
            tbl1.Rows[index]["storedesc"] = StoreDesc;
            tbl1.Rows[index]["reqrat"] = dgvReqRat;
            ViewState["tblReq"] = tbl1;
            this.gvReqInfo.EditIndex = -1;
            this.gvResInfo_DataBind();
        }
    }
}
