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
    public partial class MktPurReqEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        UserManGenAccount objuserman = new UserManGenAccount();
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

                this.chkdupMRF.Enabled = false;
                this.chkdupMRF.Checked = true;
                this.chkneBudget.Enabled = false;
                this.chkneBudget.Checked = true;                
                this.Load_Project_Combo();
                this.GetPRType();
               
                this.GetMarkType();

                this.VisibleGrid();
                this.lbtnOk.Text = "New";
                this.lbtnOk_Click(null, null);

                if ((this.Request.QueryString["genno"].ToString().Length > 0))
                {
                    this.ImgbtnFindReq_Click(null, null);
                    this.lbtnOk_Click(null, null);

                }

                this.CalendarExtender_txtCurReqDate.EndDate = System.DateTime.Today;
                this.txtCurReqDate.ReadOnly = true;

            }
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
                case "ReqApproval":                
                case "ReqEdit":
               
                    this.gvReqInfo.Columns[9].Visible = true;
                    this.gvReqInfo.Columns[11].Visible = false;
                    this.gvReqInfo.Columns[12].Visible = true;
                    
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
            string userid = hst["usrid"].ToString();
            string ReFindProject;        
            ReFindProject = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" : this.Request.QueryString["prjcode"].ToString() + "%";
           
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "PRJCODELIST", ReFindProject, fxtast, Aproval, userid, "", "", "", "", "");
            if (ds2 == null)
                return;

            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlProject.DataTextField = TextField;
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();
            ViewState["tblprojlist"] = ds2.Tables[0];

        }
        private void GetMaterial()
        {
            try
            {


                string comcod = this.GetCompCode();
                string ReFindProject = (this.Request.QueryString["prjcode"].ToString().Length == 0 ?this.ddlProject.SelectedValue.ToString() : this.Request.QueryString["prjcode"].ToString());
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GETMATLIST", ReFindProject, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                ViewState["tblmaterial"] = ds2.Tables[0];
                ViewState["tblmatdetails"] = ds2.Tables[1];

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" +ex.Message+ "');", true);
                return;

            }


        }
        protected void GetPRType()
        {
            
            string comcod = this.GetCompCode();           
            string ReFindProject = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_MKT_DDL_LIST", ReFindProject, "", "", "", "", "", "", "", "");
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
      

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "New")
            {
                this.ImgbtnFindReq.Visible = true;
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
              //  this.ddlProject.Visible = true;               
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.lblCurReqNo1.Text = "MRQ" + DateTime.Today.ToString("MM") + "-";
                this.txtCurReqDate.Enabled = true;
               // this.txtMRFNo.Text = "";    
                this.txtReqNarr.Text = "";
                this.gvReqInfo.DataSource = null;
                this.gvReqInfo.DataBind();
                //this.ddlProject.Enabled = true;
                this.pnlSpeDet.Visible = false;
                this.dNarr.Visible = false;              
                this.lbtnOk.Text = "Ok";              

                return;
            }


            this.ImgbtnFindReq.Visible = false;
            this.ddlPrevReqList.Visible = false;
            //this.ddlProject.Enabled = false;                     
            this.txtCurReqNo2.ReadOnly = true;
            this.pnlSpeDet.Visible = true;
            this.dNarr.Visible = true;         
            this.lbtnOk.Text = "New";
            this.Get_Requisition_Info();
            this.GetMaterial();
            this.ddlPRType_SelectedIndexChanged(null, null);

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

            ViewState["tblReq"] = ds1.Tables[0];
            Session["tblUserReq"] = ds1.Tables[1];
           

            if (Request.QueryString["InputType"].ToString() == "ReqApproval")
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

           // this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            this.lblCurReqNo1.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(0, 6);
            this.txtCurReqNo2.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(6, 5);
            this.txtCurReqDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString("dd.MM.yyyy");
            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();           
            //this.ddlProject.Enabled=false;                  
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            this.gvResInfo_DataBind();
        }

      
   
        private void GetApprQty()
        {
            DataTable dt = (DataTable)ViewState["tblReq"];
            double areqty, areqamt, reqrat;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                 areqty = Convert.ToDouble(dt.Rows[i]["preqty"]);
                reqrat = Convert.ToDouble(dt.Rows[i]["reqrat"]);
                areqamt = areqty * reqrat;
                dt.Rows[i]["areqty"] = areqty;
                dt.Rows[i]["areqamt"] = areqamt;

            }
            ViewState["tblReq"] = dt;


        }


        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            Hashtable hst = (Hashtable)Session["tblLogin"];
           
            DataTable tbl1 = (DataTable)ViewState["tblReq"];            
           string acttype= this.ddlActType.SelectedValue.ToString();           
           string prtype= this.ddlPRType.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("acttype = '" + acttype + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();                             
                DataTable tbl2 = (DataTable)ViewState["tblMat"];               
                dr1["prtype"] = this.ddlPRType.SelectedValue.ToString();
                dr1["acttype"] = this.ddlActType.SelectedValue.ToString();
                dr1["mkttype"] = this.ddlMarkType.SelectedValue.ToString();
                dr1["prdesc"] = this.ddlPRType.SelectedItem.Text.Trim();
                dr1["actdesc"] = this.ddlActType.SelectedItem.Text.Trim();
                dr1["mktdesc"] = this.ddlMarkType.SelectedItem.Text.Trim();
                dr1["bgdamt"] = ((DataTable)ViewState["tblmaterial"]).Select("rsircode='"+ prtype + "'")[0]["bgdamt"];
                dr1["bbgdamt"] = ((DataTable)ViewState["tblmaterial"]).Select("rsircode='" + prtype + "'")[0]["bbgdamt"];
                dr1["preqty"] = 0;
                dr1["areqty"] = 0;
                dr1["reqrat"] = 0;
                dr1["preqamt"] = 0;
                dr1["areqamt"] = 0;
                dr1["expusedt"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                dr1["reqnote"] = "";
                dr1["justific"] = "";
                tbl1.Rows.Add(dr1);

            }

            ViewState["tblReq"] = tbl1;
            this.gvResInfo_DataBind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                    dt1.Rows[j]["bbgdamt"] = 0.00;
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
            string currentpath = "PurchasePrint.aspx?Type=MktReqPrint&reqno=" + mReqNo + "&reqdat=" + CurDate1;

            string totalpath = hostname + currentpath;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

        }

        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                this.lbtnResFooterTotal_Click(null, null);
                //string mMRFNO = this.txtMRFNo.Text.Trim();
                string mMRFNO = "";
                if (this.ddlPrevReqList.Items.Count == 0)
                    this.GetReqNo();
                string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
                string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

                DataTable tbl1 = (DataTable)ViewState["tblReq"];
                DataTable dt2 = (DataTable)Session["tblUserReq"];

                // Emty Quantity
                DataRow[] dr2 = tbl1.Select("areqty>0");

                //Log Entry
                DataTable dtuser = (DataTable)Session["tblUserReq"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPostedDat = (dtuser.Rows.Count == 0) ? "" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                string tblaprvByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
                string tblaprvtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
                string tblaprvSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
                string tblaprvDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["aprvdat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");




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


                string AprvByid = (this.Request.QueryString["InputType"] == "ReqApproval") ? userid : ((tblaprvByid == "") ? "" : tblaprvByid);
                string Aprvtrmid = (this.Request.QueryString["InputType"] == "ReqApproval") ? Terminal : ((tblaprvtrmid == "") ? "" : tblaprvtrmid);
                string AprvSession = (this.Request.QueryString["InputType"] == "ReqApproval") ? Sessionid : ((tblaprvSession == "") ? "" : tblaprvSession);
                string AprvDat = (this.Request.QueryString["InputType"] == "ReqApproval") ? Date : ((Convert.ToDateTime(tblaprvDat).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "01-Jan-1900" : tblaprvDat);




                string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
                string mREQNAR = this.txtReqNarr.Text.Trim();

                bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT", "UPDATE_MKT_REQ_INFO", "MKTREQB", mREQNO, mREQDAT, mPACTCODE, mMRFNO, PostedByid, Posttrmid, PostSession, PostedDat,
                   EditByid, Edittrmid, EditSession, EditDat, mREQNAR, AprvByid, Aprvtrmid, AprvSession, AprvDat);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }


                foreach (DataRow dr1 in tbl1.Rows)
                {

                    double mPREQTY = Convert.ToDouble(dr1["preqty"]);
                    double mAREQTY = Convert.ToDouble(dr1["areqty"]);
                    double mREQRAT = Convert.ToDouble(dr1["reqrat"]);
                    string prType = dr1["prtype"].ToString();
                    string actType = dr1["acttype"].ToString();
                    string mrkType = dr1["mkttype"].ToString();
                    string expectDate = dr1["expusedt"].ToString();
                    string reqNote = dr1["reqnote"].ToString();
                    string filePath = dr1["filepath"].ToString();
                    string justific = dr1["justific"].ToString();
                    string reqType = "REQ";
                    //if(mREQRAT<=0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Insert Apprx. Unit Price " + "');", true);
                    //    return;
                    //}
                    if (mPREQTY >= mAREQTY)
                    {
                        result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT", "UPDATE_MKT_REQ_INFO", "MKTREQA",
                                    mREQNO, "", "", mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT.ToString(), prType, actType, mrkType,
                                    expectDate, filePath, reqNote, justific, reqType, "", "", "");


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
                string Type = this.Request.QueryString["InputType"].ToString();
                string msg = "";
                switch (Type)
                {
                    case "ReqCheck":
                        msg = "HOD Approval Updated successfully";
                        break;

                    case "ReqApproval":
                        msg = "DIV Approval Updated successfully";
                        break;

                    default:
                        msg = "Requisition Updated successfully";
                        break;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
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

        




        protected void lbtnCheecked_Click(object sender, EventArgs e)
        {

            this.lbtnUpdateResReq_Click(null, null);
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
            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT", "UPDATEREQCHECKED", mREQNO, checkusrid, checkTerminal, checkSessionid, checkDate, "", "", "",
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
                        string SMSText = comnam + ":\n" + SMSHead + "\n" + ddldesc == "True" ? ddlProject.SelectedItem.Text.Trim() : ddlProject.SelectedItem.Text.Trim().Substring(12) + "\n" + "Thanks";
                        bool resultsms = sms.SendSmms(SMSText, checkusrid, frmname);
                        break;
                }
            }          

        }


       


        protected void gvResInfo_DataBind()
        {
            try

            {

                DataTable tbl1 = (DataTable)ViewState["tblReq"];
                this.gvReqInfo.DataSource = tbl1;
                this.gvReqInfo.DataBind();


                ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnUpdateResReq")).Text = ((this.Request.QueryString["InputType"].ToString().Trim() == "ReqApproval") ? "Approved" : "Final Update");
                ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnCheecked")).Visible = (this.Request.QueryString["InputType"] == "ReqCheck");  
                ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnUpdateResReq")).Visible = !(this.Request.QueryString["InputType"].ToString().Trim() == "ReqCheck");
              //  ((LinkButton)this.gvReqInfo.FooterRow.FindControl("lbtnResFooterTotal")).Visible = !(this.Request.QueryString["InputType"].ToString().Trim() == "ReqCheck");
                this.FooterCalCulation();
            }
            
            
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;


            }
        }
        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)ViewState["tblReq"];
            if (dt.Rows.Count > 0)
            {

                

                ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFpreqamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(preqamt)", "")) ?
                    0.00 : dt.Compute("Sum(preqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


                ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFareqamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(areqamt)", "")) ?
                   0.00 : dt.Compute("Sum(areqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            }



        }

    
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            this.gvResInfo_DataBind();

        }
        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            int rowindex;

            double  reqqty=0.00, areqqty=0.00, price=0.00, amount, areqamt;
            for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
            {

                rowindex = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;

                // string Resocde = tbl1.Rows[rowindex]["rsircode"].ToString();

                //double dgvBgdQty = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["bbgdqty1"]);
                // double bbgdamt = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["bbgdamt1"]);



                //if (this.Request.QueryString["InputType"] == "Entry")
                //{
                //    if (this.chkneBudget.Checked)
                //    {

                //        string comcod = this.GetCompCode();


                //        switch (comcod)
                //        {
                //            case "3336":
                //            case "3305":
                //            case "3306":
                //            case "3310":
                //            case "3311":
                //            case "2305":

                //                if (dgvBgdQty < dgvReqQty)
                //                {


                //                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
                //                    return;

                //                }
                //                if (Rsircode == Resocde)
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
                //                Rsircode = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

                //                break;


                //            default:

                //                if (dgvBgdQty < dgvReqQty)
                //                {
                //                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
                //                    return;

                //                }
                //                if (Rsircode == Resocde)
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
                //                Rsircode = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

                //                break;




                //        }

                //    }
                //}
                string justific = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvJustification")).Text.Trim();

                reqqty = ASTUtility.StrPosOrNagative(((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim());
                areqqty = ASTUtility.StrPosOrNagative(((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqappQty")).Text.Trim());
                price = ASTUtility.StrPosOrNagative(((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvAppUnitPrice")).Text.Trim());
                string exdate = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvExpDate")).Text.Trim();
                string remarks = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqNote")).Text.Trim();
                amount = reqqty * price;
                areqamt = areqqty * price;
                tbl1.Rows[rowindex]["justific"] = justific;               
                tbl1.Rows[rowindex]["preqty"] = reqqty;
                tbl1.Rows[rowindex]["areqty"] = areqqty;
                tbl1.Rows[rowindex]["reqrat"] = price;                
                tbl1.Rows[rowindex]["preqamt"] = amount;
                tbl1.Rows[rowindex]["areqamt"] = areqamt;
                tbl1.Rows[rowindex]["expusedt"] = exdate;
                tbl1.Rows[rowindex]["reqnote"] = remarks;
               

            }
            ViewState["tblReq"] = tbl1;
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
          


         



        }

       

        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();           
            string fxtast = (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit"
                : (Request.QueryString["InputType"].ToString() == "ReqCheck") ? "ReqCheck"               
                : (Request.QueryString["InputType"].ToString() == "ReqApproval") ? "ReqApproval"
                 : "";

            string prjcode = ((Request.QueryString["InputType"].ToString() == "ReqApproval" || Request.QueryString["InputType"].ToString() == "ReqEdit" 
                || Request.QueryString["InputType"].ToString() == "ReqCheck" ) ? this.ddlProject.SelectedValue.ToString()               
               
                : "") + "%";


            string mrfno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GETPREVMKTREQLIST", CurDate1,
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


       
        protected void lbtnAddspecifiation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }

        protected void ddlPRType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblmatdetails"];
            string code = this.ddlPRType.SelectedValue;
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "mapcode like ('" + code + "')";
            this.ddlActType.DataTextField = "rsirdesc";
            this.ddlActType.DataValueField = "rsircode";
            this.ddlActType.DataSource = dv1.ToTable();
            this.ddlActType.DataBind();
            
        }

        protected void lbtngvReqDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblReq"];
            int gvRowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + gvRowIndex;
            string acttype = dt.Rows[rowindex]["acttype"].ToString();
            string comcod = this.GetCompCode();
            string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "DELETEREQFORSPCRES",
                        mREQNO, acttype, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" +  purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            else 
            {               
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("acttype<>''");
                ViewState["tblReq"] = dv.ToTable();
                this.gvResInfo_DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Requisition Deleted Successfully" + "');", true);
            }

        }
    }
}