using AjaxControlToolkit;
using Microsoft.Reporting.WinForms;
using RealEntity.C_22_Sal;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_28_MPro
{
    public partial class MktMarketSurvey : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static int i, j;
        public static string Url = "";
        static string prevPage = String.Empty;
        Common Common = new Common();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                this.CommonButton();
                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");


                if (this.Request.QueryString["genno"].Length != 0)
                {
                    this.lnkReqList_Click(null, null);
                    this.lbtnMSROk_Click(null, null);
                }

            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(btnForward_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnResUpdate1_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(btnReqDelete_Click);
        }



        private void CommonButton()
        {


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
          
            if (this.Request.QueryString["Type"] == "Entry")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Next";
            }
            else if (this.Request.QueryString["Type"] == "Approval")
            {

                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approved";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Checked";
            }
          

            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Requisition Delete";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approved";

                this.gvBestSelect.Columns[8].Visible = true;
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
      
        protected void lbtnMSROk_Click(object sender, EventArgs e)
        {
            if (this.lbtnMSROk.Text == "New")
            {

                this.ImgbtnFindPreMR.Visible = true;
                this.ddlPrevMSRList.Visible = true;
                this.lblPreMrList.Visible = true;
                this.txtPreMSRSearch.Visible = true;
                this.ddlPrevMSRList.Items.Clear();
                this.lblCurMSRNo1.Text = "MSR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMSRDate.Enabled = true;
                this.lbltitel1.Visible = false;
                this.lbltitel2.Visible = false;
                this.ddlReqList.Items.Clear();
                this.ddlReqList.Enabled = true;
                this.gvResInfo.DataSource = null;
                this.gvResInfo.DataBind();
                this.gvBestSelect.DataSource = null;
                this.gvBestSelect.DataBind();
                this.lbtnMSROk.Text = "Ok";
                return;
            }
            this.ddlReqList.Enabled = false;

            this.lbltitel1.Visible = true;
            this.lbltitel2.Visible = true;
            this.ImgbtnFindPreMR.Visible = false;
            this.ddlPrevMSRList.Visible = false;
            this.lblPreMrList.Visible = false;
            this.txtPreMSRSearch.Visible = false;
            this.lbtnMSROk.Text = "New";
            this.Get_Survey_Info();


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.CSPrint();

        }

        private void CSPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString(); 
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string Reqno = this.ddlReqList.SelectedValue.ToString();
            string msrno = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETMATREQWISE", Reqno,
                        "%", CurDate1, "", "", "", "", "", "");


            DataTable dt = ds2.Tables[1];
            DataTable dt1 = ds2.Tables[0];


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.ComparativeStatementCreate>();
            var lst1 = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.ComparativeStatementCreate>();

            string preparedby = ds2.Tables[2].Rows[0]["postedname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["posteddat"];
            string checkedby = ds2.Tables[2].Rows[0]["fwdname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["fwddat"];
            string varifiedby = ds2.Tables[2].Rows[0]["auditname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["auditdat"];

            string SVJ = ds2.Tables[2].Rows[0]["msrnar"].ToString();
            string SVJ2 = ds2.Tables[2].Rows[0]["msrnar2"].ToString();
            string SVJ3 = ds2.Tables[2].Rows[0]["msrnar3"].ToString();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_28_MPro.RptMktPurMarketSurvey", lst, lst1, null);
            Rpt1.SetParameters(new ReportParameter("BestS", "Best Selection"));
            Rpt1.SetParameters(new ReportParameter("CS", "Comparative Statement"));
            Rpt1.SetParameters(new ReportParameter("SVJ", "Purchase Justification: " + SVJ));
            Rpt1.SetParameters(new ReportParameter("SVJ2", "Audit Justification: " + SVJ2));
            Rpt1.SetParameters(new ReportParameter("SVJ3", "MD Justification: " + SVJ3));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("msrno", "MSR No: " + msrno));
            Rpt1.SetParameters(new ReportParameter("Reqno", "Req No: " + Reqno));
            Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Marketing Market Survey Information"));
            Rpt1.SetParameters(new ReportParameter("preparedby", preparedby));
            Rpt1.SetParameters(new ReportParameter("checkedby", checkedby));
            Rpt1.SetParameters(new ReportParameter("varifiedby", varifiedby));
            Rpt1.SetParameters(new ReportParameter("paystatus", ""));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
        private void Get_Survey_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string txtsearch = "%" + this.txtReqSearch.Text.ToString() + "%";
            string reqno = this.ddlReqList.SelectedValue.ToString();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETMATREQWISE", reqno,
                          txtsearch, CurDate1, "", "", "", "", "", "");

            if (ds2 == null)
                return;

            if (ds2.Tables[2].Rows.Count > 0)
            {
                this.lblCurMSRNo1.Text = ds2.Tables[2].Rows[0]["msrno1"].ToString().Substring(0, 6);
                this.txtCurMSRNo2.Text = ds2.Tables[2].Rows[0]["msrno1"].ToString().Substring(6, 5);
                this.txtCurMSRDate.Text = Convert.ToDateTime(ds2.Tables[2].Rows[0]["msrdat"]).ToString("dd.MM.yyyy");
                this.txtCurMSRDate.Enabled = false;

                this.ddlPrevMSRList.DataTextField = "msrno1";
                this.ddlPrevMSRList.DataValueField = "msrno";
                this.ddlPrevMSRList.DataSource = ds2.Tables[2];
                this.ddlPrevMSRList.DataBind();


            }
            Session["tblBestSelect"] = ds2.Tables[1];
            Session["tblsup"] = ds2.Tables[0];
            this.gvResInfo_DataBind();

        }
        protected void gvResInfo_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["tblsup"];
                DataTable tbl2 = (DataTable)Session["tblBestSelect"];
                string comcod = this.GetCompCode();

                if (tbl1.Rows.Count == 0)
                    return;
                if (tbl2.Rows.Count == 0)
                    return;
                tbl1 = this.HiddenSameData(tbl1);



                this.gvResInfo.DataSource = tbl1;
                this.gvResInfo.DataBind();


                this.gvBestSelect.DataSource = HiddenSameData(tbl2);
                this.gvBestSelect.DataBind();

                //for (int i = 0; i < this.gvBestSelect.Rows.Count; i++)
                //{
                //    Label txtgvRate = (Label)gvBestSelect.Rows[i].FindControl("txtgvRateBSel");
                //    txtgvRate.Style.Add("color", "blue");


                //    string supcode1 = ((Label)this.gvBestSelect.Rows[i].FindControl("lblgvSuplBSel")).Text.Trim();
                //    if (supcode1 == "000000000000")
                //    {
                //        Label txtgvBsup = (Label)gvBestSelect.Rows[i].FindControl("lblgrmet1BSel");
                //        txtgvBsup.Style.Add("color", "red");
                //    }
                //}


            this.FooterAmount();

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void lnkReqList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string txtsearch = "%" + this.txtReqSearch.Text.ToString() + "%";
            string Type = (this.Request.QueryString["Type"] == "Check") ? "Check" : (this.Request.QueryString["Type"] == "Approved") ? "Approved"
                : (this.Request.QueryString["Type"] == "Audit") ? "Audit" : "Next";

            string Gennno = (this.Request.QueryString["genno"].Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETRPUREQNO", CurDate1,
                          txtsearch, Gennno, Type, "", "", "", "", "");

            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.ddlReqList.DataTextField = "REQNO1";
                this.ddlReqList.DataValueField = "REQNO";
                this.ddlReqList.DataSource = ds2.Tables[0];
                this.ddlReqList.DataBind();
            }
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string acttype;


            acttype = dt1.Rows[0]["acttype"].ToString();
           

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["acttype"].ToString() == acttype )
                {
                    dt1.Rows[j]["rsirdesc"] = "";
                    dt1.Rows[j]["propqty"] = 0.00;
                    dt1.Rows[j]["stkqty"] = 0.00;
                    dt1.Rows[j]["reqnote"] = "";
                }

                acttype = dt1.Rows[j]["acttype"].ToString();
                
            }

            return dt1;
        }

        private void FooterAmount()
        {
            DataTable tbl2 = (DataTable)Session["tblBestSelect"];

            if (tbl2.Rows.Count == 0)
                return;


            ((Label)this.gvBestSelect.FooterRow.FindControl("lblFAmountbs")).Text = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(amount)", "")) ?
                                0 : tbl2.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvBestSelect.FooterRow.FindControl("lblFbdtamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(bdtamt)", "")) ?
                               0 : tbl2.Compute("sum(bdtamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }     

        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.BS_SaveValue();
            this.gvResInfo_DataBind();
        }

        protected void GetMSRNo()
        {
            string comcod = this.GetCompCode();
            string mMSRNO = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
                mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();

            string mREQDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            if (mMSRNO == "NEWMSR")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETLASTMSRINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mMSRNO = ds2.Tables[0].Rows[0]["maxmsrno"].ToString();

                    this.lblCurMSRNo1.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);

                    this.ddlPrevMSRList.DataTextField = "maxmsrno";
                    this.ddlPrevMSRList.DataValueField = "maxmsrno1";
                    this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                    this.ddlPrevMSRList.DataBind();
                }
            }

        }
        protected void lbtnResUpdate1_Click(object sender, EventArgs e)
        {
            try
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);

                    return;
                }
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();

                this.lnkbtnRecalculate_Click(null, null);
                //this.SaveValue();
                //this.BS_SaveValue();
                DataTable tbl1 = (DataTable)Session["tblsup"];
                DataTable tbl2 = (DataTable)Session["tblBestSelect"];

                if (this.ddlPrevMSRList.Items.Count == 0)
                    this.GetMSRNo();
                string mMSRDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
                string reqno = this.ddlReqList.SelectedValue.ToString();
                string mMSRNO = this.lblCurMSRNo1.Text.Trim().Substring(0, 3) + this.txtCurMSRDate.Text.Trim().Substring(6, 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();


                int index;
                string Rsircode = "000000000000", spcfcod = "000000000000";
                double chkqty = 0.00, treqqty = 0.00;


                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    double mRESRATE = Convert.ToDouble(tbl1.Rows[i]["rate"].ToString());

                    if (mRESRATE == 0.00)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Check Supplier Rate" + "');", true);
                        return;
                    }

                }

                string PostedByid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Postedtrmid = hst["compname"].ToString();
                string PostedSession = hst["session"].ToString();
                string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


                string aprvbyid = (this.Request.QueryString["Type"]=="Approval") ? comcod + ASTUtility.Right(hst["usrid"].ToString(), 3) : "";
                string aprvtrmid = (this.Request.QueryString["Type"] == "Approval") ? hst["compname"].ToString() : "";
                string aprvSession = (this.Request.QueryString["Type"] == "Approval") ? hst["session"].ToString() : "";
                string aprvDat = (this.Request.QueryString["Type"] == "Approval") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : "01-Jan-1900";




                string mMSRNAR = "";
                string mMSRNAR2 = "";
                string mMSRNAR3 = "";

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATEPURMSRINFO", "PURMSRB",
                                 mMSRNO, mMSRDAT, PostedByid, Postedtrmid, PostedSession, PostedDat, mMSRNAR, reqno, mMSRNAR2, mMSRNAR3, aprvbyid, aprvtrmid, aprvSession, aprvDat);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                //DataTable tbl1 = (DataTable)Session["tblsup"];
                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    int index1 = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + i;
                    string acttype = tbl1.Rows[i]["acttype"].ToString();
                    string mSPCFCOD = "000000000000";
                    string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                    string mRESRATE = tbl1.Rows[i]["rate"].ToString();

                    string mMSRRMRK = tbl1.Rows[i]["msrrmrk"].ToString();
                    string mMSRRQty = tbl1.Rows[i]["csreqqty"].ToString();
                    string mMSRRBrand = "";
                    string mMSRRDelivery = "0.00";
                    string mMSRRPay = "0.00";
                    string mMaxrate = "0.00";
                    string mPaylimit = "0.00";

                    string mAppr = (((CheckBox)gvResInfo.Rows[index1].FindControl("chkboxgv")).Checked) ? "True" : "False";
                    string paytype = tbl1.Rows[i]["paytype"].ToString();
                    string advamt = tbl1.Rows[i]["advamt"].ToString();



                    result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATEPURMSRINFO", "PURMSRA",
                             mMSRNO, acttype, mSPCFCOD, mSSIRCODE, mRESRATE, mMSRRMRK, mMSRRQty, mMSRRBrand, mMSRRDelivery, mMSRRPay, mMaxrate, mPaylimit, "", "", mAppr, paytype, advamt);

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }


                }


                //Update Requisition Details Description form Market Survey      
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    string mREQNO = tbl2.Rows[i]["reqno"].ToString();
                    string prType = tbl2.Rows[i]["prtype"].ToString();
                    string actType = tbl2.Rows[i]["acttype"].ToString();
                    string mrkType = tbl2.Rows[i]["mkttype"].ToString();
                    string rSirDetDesc = tbl2.Rows[i]["rsirdetdesc"].ToString();
                    double mPREQTY = Convert.ToDouble(tbl2.Rows[i]["propqty"]);
                    double mAREQTY = Convert.ToDouble(tbl2.Rows[i]["propqty"]);
                    double mREQRAT = Convert.ToDouble(tbl2.Rows[i]["amount"]);
                    string reqType = "CS";

                    result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATEREQDETDESC", reqno, actType, rSirDetDesc, "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }

                    //Insert New Req. From CS
                    if (prType!="" && mrkType!="")
                    {
                        result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT", "UPDATE_MKT_REQ_INFO", "MKTREQA",
                                      mREQNO, "", "", mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT.ToString(), prType, actType, mrkType,
                                      "01-Jan-1900", "", "", "", reqType, rSirDetDesc, "", "");

                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }
                    }

                }


                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                    string acttype = tbl1.Rows[i]["acttype"].ToString();
                    string mSPCFCOD = "000000000000";
                    string rate = tbl1.Rows[i]["rate"].ToString();
                    string approved = tbl1.Rows[i]["approved"].ToString();

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATESUPLRESINFOAREQ", mSSIRCODE, acttype, mSPCFCOD, rate, reqno, approved, "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }

                string Type = this.Request.QueryString["Type"].ToString();
                string msg = "";
                switch (Type)
                {
                    case "Approval":
                        msg = "Market Survey Approved successfully";
                        break;

                    default:
                        msg = "Market Survey Updated successfully";
                        break;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = this.lblReqList.Text;
                    string eventdesc = "Update Market Suervey";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                this.Get_Survey_Info();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }           
        }


        private void SaveValue()
        {
            int index;
            DataTable dt = (DataTable)Session["tblsup"];
            for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            {
                index = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + i;

                string approved = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False"; //dt.Rows[index]["approved"].ToString();

                double Reqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvResInfo.Rows[i].FindControl("lblgvpropqty_01")).Text.Trim()));
                double Rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double csreqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvcsreqqty")).Text.Trim()));
                double advamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvadvamtC")).Text.Trim()));
                string paytype = ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvpaytypeC")).Text.Trim();
                string remakrs = ((TextBox)this.gvResInfo.Rows[i].FindControl("TextRemarks")).Text.Trim();
                dt.Rows[i]["approved"] = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False";

                dt.Rows[i]["advamt"] = advamt;
                dt.Rows[i]["paytype"] = paytype;
                dt.Rows[i]["msrrmrk"] = remakrs;
                dt.Rows[i]["rate"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                dt.Rows[i]["csreqqty"] = (approved == "False") ? 0.00 : (csreqqty == 0) ? Reqqty : Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvcsreqqty")).Text.Trim()));
                ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvcsreqqty")).Text = (approved == "False") ? "" : (csreqqty == 0) ? Reqqty.ToString() : csreqqty.ToString();


                dt.Rows[i]["amount"] = (approved == "False") ? 0.00 : (Rate * ((csreqqty == 0) ? Reqqty : csreqqty));
                dt.Rows[i]["bdtamt"] = (approved == "False") ? 0.00 : (Rate  * ((csreqqty == 0) ? Reqqty : csreqqty));


            }
            Session["tblsup"] = dt;

        }
        protected void gvResInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


        }




        protected void lbtTotal1_Click(object sender, EventArgs e)
        {
            this.FooterAmount();
            this.gvResInfo_DataBind();
        }
        protected void btnForward_Click(object sender, EventArgs e)
        {

            try
            {

                string comcod = this.GetCompCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string reqno = this.ddlReqList.SelectedValue.ToString();
                string surveynpo = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
                string Sessionid = hst["session"].ToString();
                string Terminal = hst["compname"].ToString();

                string Type = (this.Request.QueryString["Type"] == "Check") ? "Check" : (this.Request.QueryString["Type"] == "Approved") ? "Approved"
                    : (this.Request.QueryString["Type"] == "Audit") ? "Audit" : "Next";
                if (Type == "Approved")
                {
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHQREQAPP", reqno, "", "", "", "", "", "", "", "");

                    if (Convert.ToDouble(ds.Tables[0].Rows[0]["areqty"]) == 0)
                    {
                        this.AppReq();
                    }
                }



                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEMSERVAYNUM", reqno, surveynpo, userid, Date, Type, Sessionid,
                       Terminal, "", "", "", "", "", "", "", "");
                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Update Failed!" + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated successfully" + "');", true);

            }
            catch (Exception ex)
            {

            }
        }
        private void AppReq()
        {
            string comcod = this.GetCompCode();
            this.BS_SaveValue();
            DataTable tbl1 = (DataTable)Session["tblBestSelect"];
            // string mMSRNO = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
            string Reqno = this.ddlReqList.SelectedValue.ToString();
            string mMSRNO = this.lblCurMSRNo1.Text.Trim().Substring(0, 3) + this.txtCurMSRDate.Text.Trim().Substring(6, 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();

            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(tbl1);
            ds1.Tables[0].TableName = "tbl1";

            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATECSAPPREQ", ds1, null, null, Reqno, mMSRNO, "", "", "", "", "", "", "",
              "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

        }

        protected void btnReqDelete_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
                return;
            }

            string Reqno = this.Request.QueryString["genno"].ToString();
            string Type = "Audit";
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please select your item for Delete" + "');", true);
            }

            string comcod = this.GetCompCode();

            bool result = purData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "REVCSPART", Reqno, Type, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "CS Audit Delete Successfully" + "');", true);
            }
            Common.LogStatus("Purchase Interface", "CS Audit Delete", "Order No: ", Reqno);


        }
     
        protected void lblgrsirdescs1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string rsircode = ((Label)this.gvResInfo.Rows[index].FindControl("lblrsircode")).Text.ToString();
            Session["rsircode"] = rsircode;           
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETMSRSUPLIST", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlSupl2.DataTextField = "ssirdesc1";
            this.ddlSupl2.DataValueField = "ssircode";
            this.ddlSupl2.DataSource = ds1.Tables[0];
            this.ddlSupl2.DataBind();
          
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openServyModal();", true);


        }
        
        protected void UpdateData_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();            
            string SSIRCODE = this.ddlSupl2.SelectedValue.ToString();
            string rsircode = Session["rsircode"].ToString();
            string spcfcod = "000000000000";
            string mRMRKS = "";
            string mRate = this.TextRate.Text.ToString();
            string mDelsys = "0";
            string mPayss = "0";
            string mPaylimit = "0";

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATESUPLRES",
                          SSIRCODE, rsircode, spcfcod, mRMRKS, mDelsys, mPayss, mRate, mPaylimit, "", "", "", "", "", "", "");


            Response.Redirect(Request.RawUrl, true);

        }

        protected void lnkbtnNewReq_Click(object sender, EventArgs e)
        {
            this.pnlAddWorkDet.Visible = true;
            this.GetMaterial();
            this.GetPRType();
            this.GetMarkType();
            this.ddlPRType_SelectedIndexChanged(null, null);
        }
        private void GetMaterial()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblBestSelect"];
                string comcod = this.GetCompCode();
                string ReFindProject = dt.Rows[0]["pactcode"].ToString();
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GETMATLIST", ReFindProject, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                ViewState["tblmatdetails"] = ds2.Tables[1];

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;

            }


        }
        protected void GetPRType()
        {
            string comcod = this.GetCompCode();
            string ReFindProject = "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_MKT_DDL_LIST", ReFindProject, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            string gcod = "62%";
            DataView dv = ds2.Tables[0].Copy().DefaultView;
            dv.RowFilter = ("gcod like '" + gcod + "'");

            this.ddlPRType.DataTextField = "gdesc";
            this.ddlPRType.DataValueField = "gcod";
            this.ddlPRType.DataSource = dv.ToTable();
            this.ddlPRType.DataBind();

            ViewState["tblddllist"] = ds2.Tables[0];


        }
        protected void ddlPRType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblmatdetails"];
            string code = this.ddlPRType.SelectedValue;
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("mapcode like '" + code + "'");
            this.ddlActType.DataTextField = "rsirdesc";
            this.ddlActType.DataValueField = "rsircode";
            this.ddlActType.DataSource = dv1.ToTable();
            this.ddlActType.DataBind();

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
        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.BS_SaveValue();
            DataTable tbl1 = (DataTable)Session["tblBestSelect"];
            string acttype = this.ddlActType.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("acttype = '" + acttype + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["reqno"] = tbl1.Rows[0]["reqno"].ToString();
                dr1["prtype"] = this.ddlPRType.SelectedValue.ToString();
                dr1["acttype"] = this.ddlActType.SelectedValue.ToString();
                dr1["mkttype"] = this.ddlMarkType.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlActType.SelectedItem.Text.Trim();
                dr1["rsirunit"] = "";
                dr1["stkqty"] = 0;
                dr1["propqty"] = 0;
                dr1["areqty"] = 0;
                dr1["csreqqty"] = 0;
                dr1["rate"] = 0;
                dr1["amount"] = 0;
                dr1["bdtamt"] = 0;
                dr1["lstpurate"] = 0;
                dr1["lpurqty"] = 0;
                dr1["lpurdate"] = "01-Jan-1900";
                dr1["supdesc"] = "";
                dr1["paytype"] = "";
                dr1["advamt"] = 0;
                dr1["payment"] = "";
                dr1["cperson"] = "";
                dr1["mobile"] = "";
                dr1["leadtime"] = 0;
                tbl1.Rows.Add(dr1);

            }

            Session["tblBestSelect"] = tbl1;
            this.gvResInfo_DataBind();

        }
        private void BS_SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblBestSelect"];
            int rowindex;

            double areqqty = 0.00, rate = 0.00, amount =0.00;
            for (int j = 0; j < this.gvBestSelect.Rows.Count; j++)
            {
                rowindex = (this.gvBestSelect.PageSize) * (this.gvBestSelect.PageIndex) + j;
                areqqty = ASTUtility.StrPosOrNagative(((TextBox)this.gvBestSelect.Rows[j].FindControl("txtgvpropqtyBSel")).Text.Trim());
                rate = ASTUtility.StrPosOrNagative(((TextBox)this.gvBestSelect.Rows[j].FindControl("txtgvRateBSel")).Text.Trim());
                string rsirdetDesc = ((TextBox)this.gvBestSelect.Rows[j].FindControl("txtgvRSirDetDesc")).Text.Trim();

                amount = areqqty * rate;

                tbl1.Rows[rowindex]["propqty"] = areqqty;
                tbl1.Rows[rowindex]["rate"] = rate;
                tbl1.Rows[rowindex]["amount"] = amount;
                tbl1.Rows[rowindex]["rsirdetdesc"] = rsirdetDesc;

            }
            Session["tblBestSelect"] = tbl1;
        }

        protected void lbtnBSFTotal_Click(object sender, EventArgs e)
        {
            this.BS_SaveValue();            
            this.gvResInfo_DataBind();
            this.FooterAmount();
        }

    }
}