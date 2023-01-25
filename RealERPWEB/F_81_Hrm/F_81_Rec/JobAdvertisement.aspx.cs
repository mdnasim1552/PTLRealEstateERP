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
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{

    public partial class JobAdvertisement : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // for Mettroo
                this.txtADVText.Enabled = false;
                this.ImgbtnReqse.Enabled = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];

                //((Label)this.Master.FindControl("lblTitle")).Text = "Job Advertisement Information";
                this.GetCompany();
                this.lbtnOk.Text = "New";
                this.lbtnOk_Click(null, null);
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetCompany()
        {




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%" + this.txtCompSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            //this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();


            //if (this.lbtnOk.Text == "New")
            //    return;
            //string comcod = this.GetCompCode();
            //string txtCompany = "%" + this.txtCompSearch.Text.Trim() + "%";
            //DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            //this.ddlCompany.DataTextField = "actdesc";
            //this.ddlCompany.DataValueField = "actcode";
            //this.ddlCompany.DataSource = ds1.Tables[0];
            //this.ddlCompany.DataBind();


            this.ddlSource.DataTextField = "soudesc";
            this.ddlSource.DataValueField = "soucod";
            this.ddlSource.DataSource = ds1.Tables[1];
            this.ddlSource.DataBind();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.txtSrchPre.Visible = true;
                this.lblpreReq.Visible = true;
                this.ImgbtnFindReq.Visible = true;
                this.ddlPrevAdvList.Visible = true;
                this.ddlPrevAdvList.Items.Clear();
                this.ddlCompany.Visible = true;
                this.lblddlCompany.Visible = false;
                this.ddlSource.Visible = true;
                this.lblJobSource.Visible = false;

                this.txtCurAdvDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.lblCurAdvNo1.Text = "ADV" + DateTime.Today.ToString("MM") + "-";
                this.txtCurAdvDate.Enabled = true;
                this.txtMRFNo.Text = "";

                this.txtPostSearch.Text = "";
                this.ddlDept.Items.Clear();
                this.ddlPOSTList.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtExpDeliveryDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtAdvNarr.Text = "";
                this.gvAdvInfo.DataSource = null;
                this.gvAdvInfo.DataBind();
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.txtCurAdvDate.Enabled = true;
                //if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "ReqEdit" || Request.QueryString["InputType"].ToString() == "HeadUsed")
                //{


                //this.lblAdvno.Visible = false;
                //this.txtMRFNo.Visible = false;
                //this.lblCurNo.Visible = false;
                //this.lblCurAdvNo1.Visible = false;
                //this.txtCurAdvNo2.Visible = false;
                //this.txtADVText.Visible = false;
                //this.ImgbtnReqse.Visible = false;

                //this.ImgbtnFindReq_Click(null, null);

                //}

                return;
            }

            this.txtSrchPre.Visible = false;
            this.lblpreReq.Visible = false;
            this.ImgbtnFindReq.Visible = false;
            this.ddlPrevAdvList.Visible = false;

            this.lblddlCompany.Text = this.ddlCompany.SelectedItem.Text.Trim();
            this.ddlCompany.Visible = false;
            this.lblddlCompany.Visible = true;
            this.ddlSource.Visible = false;
            this.lblJobSource.Visible = true;
            this.lblJobSource.Text = this.ddlSource.SelectedItem.Text.Trim();
            this.txtCurAdvDate.Enabled = false;

            this.txtCurAdvNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Advertisement_Info();



        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void GetAdvNo()
        {
            string comcod = this.GetCompCode();
            string mADVNO = "NEWADV";
            if (this.ddlPrevAdvList.Items.Count > 0)
                mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();

            string mADVDAT = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            if (mADVNO == "NEWADV")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETREFNO", mADVDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurAdvNo1.Text = ds2.Tables[0].Rows[0]["advno1"].ToString().Substring(0, 5);
                    this.txtCurAdvNo2.Text = ds2.Tables[0].Rows[0]["advno1"].ToString().Substring(6, 3);

                    this.ddlPrevAdvList.DataTextField = "advno1";
                    this.ddlPrevAdvList.DataValueField = "advno";
                    this.ddlPrevAdvList.DataSource = ds2.Tables[0];
                    this.ddlPrevAdvList.DataBind();
                }
            }

        }


        protected void Get_Advertisement_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            string mADVNO = "NEWADV";
            if (this.ddlPrevAdvList.Items.Count > 0)
            {
                this.txtCurAdvDate.Enabled = false;
                mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();


            }
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETPREADVDATA", mADVNO, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblAdvPost"] = this.HiddenSameData(ds1.Tables[0]);


            if (mADVNO == "NEWADV")
            {
                ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETREFNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurAdvNo1.Text = ds1.Tables[0].Rows[0]["advno1"].ToString().Substring(0, 5);
                    this.txtCurAdvNo2.Text = ds1.Tables[0].Rows[0]["advno1"].ToString().Substring(6, 3);
                }
                return;
            }
            this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurAdvNo1.Text = ds1.Tables[1].Rows[0]["advno1"].ToString().Substring(0, 5);
            this.txtCurAdvNo2.Text = ds1.Tables[1].Rows[0]["advno1"].ToString().Substring(6, 3);
            this.txtCurAdvDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["advdat"]).ToString("dd.MM.yyyy");


            this.ddlCompany.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlSource.SelectedValue = ds1.Tables[1].Rows[0]["soucode"].ToString();

            this.lblddlCompany.Text = this.ddlCompany.SelectedItem.Text.Trim();
            this.lblJobSource.Text = this.ddlSource.SelectedItem.Text.Trim();
            //this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["advbydes"].ToString();
            //this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            //this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            //this.txtExpDeliveryDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["eddat"]).ToString("dd.MM.yyyy");
            this.txtAdvNarr.Text = ds1.Tables[1].Rows[0]["remarks"].ToString();
            this.Data_Bind();

        }




        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {

            this.Save_Value();
            DataTable tbl1 = (DataTable)ViewState["tblAdvPost"];
            string mPostCode = this.ddlPOSTList.SelectedValue.ToString();
            DataTable tbl2 = (DataTable)ViewState["tblJobPost"];
            DataTable tbl3 = (DataTable)ViewState["tblSpcf"];

            DataRow[] dr3 = tbl1.Select("postcode = '" + mPostCode + "'");
            if (dr3.Length == 0)
            {
                for (int i = 0; i < tbl3.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    //dr1["advno"] = this.ddlDept.SelectedValue.ToString();
                    dr1["deptcode"] = this.ddlDept.SelectedValue.ToString();
                    dr1["deptdesc"] = this.ddlDept.SelectedItem.Text.Trim();
                    dr1["postcode"] = this.ddlPOSTList.SelectedValue.ToString();
                    dr1["postdesc"] = this.ddlPOSTList.SelectedItem.Text.Trim();



                    dr1["gcod"] = tbl3.Rows[i]["gcod"];
                    dr1["gdesc"] = tbl3.Rows[i]["gdesc"];
                    dr1["requir"] = "";

                    tbl1.Rows.Add(dr1);
                }
            }


            ViewState["tblAdvPost"] = this.HiddenSameData(tbl1);
            this.Data_Bind();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string postcode = dt1.Rows[0]["postcode"].ToString();
            string deptcode = dt1.Rows[0]["deptcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    dt1.Rows[j]["deptdesc"] = "";
                }
                if (dt1.Rows[j]["postcode"].ToString() == postcode)
                {
                    postcode = dt1.Rows[j]["postcode"].ToString();
                    dt1.Rows[j]["postdesc"] = "";

                }


                else
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                postcode = dt1.Rows[j]["postcode"].ToString();

            }



            return dt1;
        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }


        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //   ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            this.Save_Value();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevAdvList.Items.Count == 0)
                this.GetAdvNo();
            string mADVDAT = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            string mADVNO = this.lblCurAdvNo1.Text.Trim().Substring(0, 3) + this.txtCurAdvDate.Text.Trim().Substring(6, 4) + this.lblCurAdvNo1.Text.Trim().Substring(3, 2) + this.txtCurAdvNo2.Text.Trim();


            //if (this.chkdupMRF.Checked)
            //{
            //    if (mMRFNO.Length == 0)
            //    {
            //       ((Label)this.Master.FindControl("lblmsg")).Text = "M.R.F No. Should Not Be Empty";
            //        return;
            //    }

            //    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKEDDUPMRRNO", mMRFNO, "", "", "", "", "", "", "", "");
            //    if (ds2.Tables[0].Rows.Count == 0)
            //        ;


            //    else
            //    {

            //        DataView dv1 = ds2.Tables[0].DefaultView;
            //        dv1.RowFilter = ("reqno <>'" + mADVNO + "'");
            //        DataTable dt = dv1.ToTable();
            //        if (dt.Rows.Count == 0)
            //            ;
            //        else
            //        {
            //           ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate M.R.F No";
            //            //this.ddlPrevReqList.Items.Clear();
            //            return;
            //        }
            //    }
            //}
            DataTable tbl1 = (DataTable)ViewState["tblAdvPost"];


            string mPACTCODE = this.ddlCompany.SelectedValue.ToString().Trim();
            string mJOBSOURCE = this.ddlSource.SelectedValue.ToString().Trim();
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());
            string mEDDAT = this.GetStdDate(this.txtExpDeliveryDate.Text.Trim());
            string mREQBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();

            string mREQNAR = this.txtAdvNarr.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "INSERTUPDATEININFO", "INADVINFOB", mADVNO, mPACTCODE, mADVDAT, mMRFNO, mJOBSOURCE, mREQNAR, "", "", "",

                             "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mpostcode = tbl1.Rows[i]["postcode"].ToString();
                string mGCOD = tbl1.Rows[i]["gcod"].ToString();
                string mREQUIR = tbl1.Rows[i]["requir"].ToString();

                result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "INSERTUPDATEININFO", "INADVINFOA",
                            mADVNO, mpostcode, mGCOD, mREQUIR, "", "", "", "", "",
                            "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


            }



            string type = this.Request.QueryString["Type"].ToString();
            if (type == "App")
            {
                result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "APPROVEADD", mADVNO, userid, "", "", "", "", "", "", "", "", "",
                               "", "", "");



                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }


            this.txtCurAdvDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Advertisement Entry";
                string eventdesc = "Update Advertisement";
                string eventdesc2 = "Adv No- " + mADVNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }





        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblAdvPost"];
            this.gvAdvInfo.DataSource = tbl1;
            this.gvAdvInfo.DataBind();

        }


        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["tblAdvPost"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvAdvInfo.Rows.Count; j++)
            {
                string dgvMSRBrand = ((TextBox)this.gvAdvInfo.Rows[j].FindControl("txtgvRequ")).Text.Trim();

                TblRowIndex2 = (this.gvAdvInfo.PageSize) * (this.gvAdvInfo.PageIndex) + j;

                tbl1.Rows[TblRowIndex2]["requir"] = dgvMSRBrand;

            }
            ViewState["tblAdvPost"] = tbl1;
        }



        protected void ImgbtnFindPost_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string mProject = this.ddlDept.SelectedValue.ToString().Substring(0, 2) + "%";
            string mSrchTxt = this.txtPostSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETNEWPOST", mProject, mSrchTxt, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblJobPost"] = ds1.Tables[0];
            ViewState["tblSpcf"] = ds1.Tables[1];

            this.ddlPOSTList.DataTextField = "postdesc";
            this.ddlPOSTList.DataValueField = "postcode";
            this.ddlPOSTList.DataSource = ds1.Tables[0];
            this.ddlPOSTList.DataBind();



        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvAdvInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblAdvPost"];
            string mADVNO = ASTUtility.Left(this.lblCurAdvNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurAdvDate.Text.Trim(), 4) + this.lblCurAdvNo1.Text.Trim().Substring(3, 2) + this.txtCurAdvNo2.Text.Trim();
            string postcode = ((Label)this.gvAdvInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "DELETEADVINF",
                        mADVNO, postcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvAdvInfo.PageSize) * (this.gvAdvInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("postcode<>''");
                ViewState["tblAdvPost"] = dv.ToTable();
                this.Data_Bind();
            }

        }

        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();


            string mrfno = "%" + this.txtSrchPre.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETPREREF", CurDate1,
                          mrfno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevAdvList.Items.Clear();
            this.ddlPrevAdvList.DataTextField = "advno1";
            this.ddlPrevAdvList.DataValueField = "advno";
            this.ddlPrevAdvList.DataSource = ds1.Tables[0];
            this.ddlPrevAdvList.DataBind();


        }
        //protected void lbtnResFooterDelete_Click(object sender, EventArgs e)
        //{

        //    string comcod = this.GetCompCode();
        //    DataTable dt = (DataTable)ViewState["tblAdvPost"];
        //    string mADVNO = ASTUtility.Left(this.lblCurAdvNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurAdvDate.Text.Trim(), 4) + this.lblCurAdvNo1.Text.Trim().Substring(3, 2) + this.txtCurAdvNo2.Text.Trim();

        //    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQNO", mADVNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        //    if (!result)
        //    {
        //       ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Failed";
        //        return;

        //    }

        //   ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";

        //    if (ConstantInfo.LogStatus == true)
        //    {

        //        string eventtype = "Material Requisition";
        //        string eventdesc = "Delete Requisition";
        //        string eventdesc2 = "Req No- " + this.lblCurAdvNo1.Text + this.txtCurAdvNo2.Text.ToString().Trim();
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }
        //}

        protected void ImgbtnFindComp_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void ImgbtnReqse_Click(object sender, EventArgs e)
        {

        }


        protected void imgBtnDept_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string mSrchTxt = this.txtDepList.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETDEPTARTMENT", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            //ViewState["tblJobPost"] = ds1.Tables[0];
            //ViewState["tblSpcf"] = ds1.Tables[1];

            this.ddlDept.DataTextField = "deptdesc";
            this.ddlDept.DataValueField = "deptcode";
            this.ddlDept.DataSource = ds1.Tables[0];
            this.ddlDept.DataBind();
            this.ImgbtnFindPost_Click(null, null);
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnFindPost_Click(null, null);
        }
        protected void gvAdvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Save_Value();
            this.gvAdvInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}
