﻿using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RealERPEntity.C_70_Services.EClass_Quotation;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptServiceInterface : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                //    Response.Redirect("~/AcceessError");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Service Management Interface";//

                txtfrmdate.Text = System.DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy");
                txttoDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                ModuleName();
                getTotalCount();
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ModuleName()
        {

            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_INTERFACE_SERVICES]", "GETCOUNT", date1, date2, "", "", "", "", "", "", "", "", "");
            DataSet ds1 = _process.GetTransInfo(comcod, "[dbo_Services].[SP_INTERFACE_SERVICES]", "GETPROCESS", date1, date2, "", "", "", "", "", "", "");
            DataSet ds2 = _process.GetTransInfo(comcod, "[dbo_Services].[SP_INTERFACE_SERVICES]", "GETINVOICE", date1, date2, "", "", "", "", "", "", "");
            string item1, item2, item3, item4, item5, item6, item7;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                item1 = dt.Rows[0][1].ToString();
                item2 = dt.Rows[0][2].ToString();
                item3 = dt.Rows[0][3].ToString();
                item4 = dt.Rows[0][4].ToString();
                item5 = dt.Rows[0][5].ToString();
                item6 = dt.Rows[0][6].ToString();
                item7 = dt.Rows[0][7].ToString();
            }
            else
            {
                item1 = "0";
                item2 = "0";
                item3 = "0";
                item4 = "0";
                item5 = "0";
                item6 = "0";
                item7 = "0";
            }
            if (ds1.Tables[2].Rows.Count == 0)
            {
                item4 = "0";
                item6 = "0";
            }
            else
            {
                item4 = ds1.Tables[2].Rows[0]["countval"].ToString();
                item6 = ds1.Tables[2].Rows[1]["countval"].ToString();

            }
            if (ds2.Tables[1].Rows.Count == 0)
            {
               
                item5 = "0";
            }
            else
            {
                item5 = ds2.Tables[1].Rows[0]["countval"].ToString();  
            }
            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item1 + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + item2 + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Checked" + "</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item3 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Accept/Reject</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item4 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Process</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item5 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Invoice</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item6 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Complete</div></div></div>";
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item7 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Reject</div></div></div>";


        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    pnlTotalCount.Visible = true;
                    pnlChecked.Visible = false;
                    pnlApproval.Visible = false;
                    pnlProcess.Visible = false;
                    pnlInvoice.Visible = false;
                    pnlComplete.Visible = false;
                    getTotalCount();
                    break;
                case "1":
                    pnlTotalCount.Visible = false;
                    pnlChecked.Visible = true;
                    pnlApproval.Visible = false;
                    pnlProcess.Visible = false;
                    pnlInvoice.Visible = false;
                    pnlComplete.Visible = false;
                    getChecked();
                    break;
                case "2":
                    pnlTotalCount.Visible = false;
                    pnlChecked.Visible = false;
                    pnlApproval.Visible = true;
                    pnlProcess.Visible = false;
                    pnlInvoice.Visible = false;
                    pnlComplete.Visible = false;
                    getApproval();
                    break;
                case "3":
                    pnlTotalCount.Visible = false;
                    pnlChecked.Visible = false;
                    pnlApproval.Visible = false;
                    pnlProcess.Visible = true;
                    pnlInvoice.Visible = false;
                    pnlComplete.Visible = false;
                    getProcess();
                    break;
                case "4":
                    pnlTotalCount.Visible = false;
                    pnlChecked.Visible = false;
                    pnlApproval.Visible = false;
                    pnlProcess.Visible = false;
                    pnlInvoice.Visible = true;
                    pnlComplete.Visible = false;
                    getInvoice();
                    break;
                case "5":
                    pnlTotalCount.Visible = false;
                    pnlChecked.Visible = false;
                    pnlApproval.Visible = false;
                    pnlProcess.Visible = false;
                    pnlInvoice.Visible = false;
                    pnlComplete.Visible = true;
                    getProcess();
                    break;

            }
        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            ModuleName();
            pnlTotalCount.Visible = true;
            RadioButtonList1_SelectedIndexChanged(null, null);
        }

        private void getTotalCount()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "[dbo_Services].[SP_INTERFACE_SERVICES]", "GETTOTAL", date1, date2, "", "", "", "", "", "", "");
            gvTotal.DataSource = ds1.Tables[0];
            gvTotal.DataBind();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.gvTotal.FooterRow.FindControl("lblgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(qamt)", "")) ?
                0 : ds1.Tables[0].Compute("sum(qamt)", ""))).ToString("#,##0.00;(#,##0.00);");
            }
        }
        private void getChecked()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "[dbo_Services].[SP_INTERFACE_SERVICES]", "GETCHECK", date1, date2, "", "", "", "", "", "", "");
            gvChecked.DataSource = ds1.Tables[0];
            gvChecked.DataBind();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.gvChecked.FooterRow.FindControl("lblgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(qamt)", "")) ?
                0 : ds1.Tables[0].Compute("sum(qamt)", ""))).ToString("#,##0.00;(#,##0.00);");
            }
        }


        private void getApproval()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "[dbo_Services].[SP_INTERFACE_SERVICES]", "GETAPPROVE", date1, date2, "", "", "", "", "", "", "");
            gvApproval.DataSource = ds1.Tables[0];
            gvApproval.DataBind();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.gvApproval.FooterRow.FindControl("lblgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(qamt)", "")) ?
                0 : ds1.Tables[0].Compute("sum(qamt)", ""))).ToString("#,##0.00;(#,##0.00);");
            }
        }
        private void getProcess()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "[dbo_Services].[SP_INTERFACE_SERVICES]", "GETPROCESS", date1, date2, "", "", "", "", "", "", "");
            gvProcess.DataSource = ds1.Tables[0];
            gvProcess.DataBind();
            
            gvComplete.DataSource = ds1.Tables[1];
            gvComplete.DataBind();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.gvProcess.FooterRow.FindControl("lblgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(qamt)", "")) ?
                0 : ds1.Tables[0].Compute("sum(qamt)", ""))).ToString("#,##0.00;(#,##0.00);");
            }
            if (ds1.Tables[1].Rows.Count > 0)
            {
                ((Label)this.gvComplete.FooterRow.FindControl("lblgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("sum(qamt)", "")) ?
                0 : ds1.Tables[1].Compute("sum(qamt)", ""))).ToString("#,##0.00;(#,##0.00);");
            }
        }
        private void getInvoice()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "[dbo_Services].[SP_INTERFACE_SERVICES]", "GETINVOICE", date1, date2, "", "", "", "", "", "", "");
            gvInvoice.DataSource = ds1.Tables[0];
            gvInvoice.DataBind();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.gvInvoice.FooterRow.FindControl("lblgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(qamt)", "")) ?
                0 : ds1.Tables[0].Compute("sum(qamt)", ""))).ToString("#,##0.00;(#,##0.00);");
            }
        }


        protected void lnkUpdateReject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string userId = hst["usrid"].ToString();
            string seq = "11";
            string type = lblProcess.Text;
            string vehicleno = lblDgNoReject.Text;
            string rejectDesc = txtRejectDesc.Text;
            bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "UPSERTTRANSPORTINFHO", vehicleno, seq, rejectDesc, type, "", "", "", "", "", "", "", "",
                                     "", "", "", "", "", "", "", "", "", "", userId);
            if (resultflag)
            {
                ModuleName();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"TRP-{vehicleno} HOD Approval" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }
        }

        protected void gvChecked_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                HyperLink hlinkproceed = (HyperLink)e.Row.FindControl("lnkProceed");

                string quotid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "quotid")).ToString();
                string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "status"));
                hlink.NavigateUrl = "~/F_70_Services/QuotationEntry?Type=Edit&QId=" + quotid;
                hlinkproceed.NavigateUrl = "~/F_70_Services/QuotationEntry?Type=Check&QId=" + quotid;
                hlink.ToolTip = "Edit";
                hlink.Visible = true;
                hlinkproceed.Visible = true;


            }
        }

        protected void gvApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                HyperLink hlinkproceed = (HyperLink)e.Row.FindControl("lnkProceed");



                string quotid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "quotid")).ToString();
                string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "status"));
                hlink.NavigateUrl = "~/F_70_Services/QuotationEntry?Type=CheckEdit&QId=" + quotid;
                hlinkproceed.NavigateUrl = "~/F_70_Services/QuotationEntry?Type=Approval&QId=" + quotid;
                hlink.ToolTip = "Edit";
                hlink.Visible = true;
                hlinkproceed.Visible = true;


            }
        }

        protected void gvProcess_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                LinkButton lnkMatReq = (LinkButton)e.Row.FindControl("lnkbtnMatReq");
                LinkButton lnkSubCon = (LinkButton)e.Row.FindControl("lnkbtnSubCon");
                LinkButton lnkGenBill = (LinkButton)e.Row.FindControl("lnkbtnGenBill");

                string quotid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "quotid")).ToString();
                bool isMatReq = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "ismatreq"));
                bool isEdit = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isEdit"));
                bool isSubCon = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "islisuno"));
                bool islisuno = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "islisuno"));
                bool isGenBill = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isGenBill"));


                if (isEdit)
                {
                    hlink.NavigateUrl = "~/F_70_Services/QuotationEntry?Type=ApprovalEdit&QId=" + quotid;
                    hlink.ToolTip = "Edit";
                    hlink.Visible = true;
                }
                else
                {
                    hlink.Visible = false;
                }
                if (isMatReq)
                {
                    lnkMatReq.Visible = true;

                }
                else
                {
                    lnkMatReq.Visible = false;
                }
                if (isSubCon)
                {
                    lnkSubCon.Visible = true;
                }
                else
                {
                    lnkSubCon.Visible = false;
                }
                if (isGenBill)
                {
                    lnkGenBill.Visible = true;
                }
                else
                {
                    lnkGenBill.Visible = false;
                }


                // todo for link project
                HyperLink custdesc = (HyperLink)e.Row.FindControl("hlnkgvcustdesc");
                string mapactcode = "16"+ ASTUtility.Right(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mapactcode")).ToString(),10);
                custdesc.NavigateUrl = "~/F_04_Bgd/PrjInformation?Type=Report&prjcode=" + mapactcode; 

            }
        }



        private void GetQuotDataFunctionality(string QId)
        {
            string type = Request.QueryString["Type"] ?? "";
            string comcod = GetComCode();
            string quotid = QId;
            string status = "3";
            string isType = "ApprovalEdit";
            if (quotid != "" || quotid.Length > 0)
            {
                DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "EDITINFOQUOT", quotid, status, isType, "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                DataTable dt1 = ds.Tables[0];
                List<EQuotation> dt2 = ds.Tables[1].DataTableToList<EQuotation>();
                if (dt1.Rows.Count > 0)
                {
                    lblActcode.Text = dt1.Rows[0]["mapactcode"].ToString();
                    lblQuotation.Text = dt1.Rows[0]["quotid"].ToString();
                    string reqno = dt1.Rows[0]["reqno"].ToString();
                    if (reqno == "" || reqno == "00000000000000")
                    {
                        getReqNo();
                    }
                    else
                    {
                        lblReqno.Text = reqno;
                    }
                }
                ViewState["MaterialList"] = dt2;
            }

        }

        #region Material Requisition Section

        protected void lnkbtnMatReq_Click(object sender, EventArgs e)
        {
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string quotid = ((Label)this.gvProcess.Rows[RowIndex].FindControl("lblqid")).Text.Trim();
            GetQuotDataFunctionality(quotid);
            List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
            int isContain = obj.Where(x => x.resourcecode.StartsWith("01") && x.isprocess==true).ToList().Count;
            if (isContain == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"No Material for Material Requisition" + "');", true);
            }
            else
            {
                UpdateMaterialRequisition(quotid);
            }

        }


        private void getReqNo()
        {
            string comcod = GetComCode();
            string CurDate1 = System.DateTime.Today.ToString();
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblReqno.Text = ds1.Tables[0].Rows[0]["maxreqno"].ToString();
            }
            else
            {
                lblReqno.Text = "";
            }
        }
        protected void lnkMatReq_Click(object sender, EventArgs e)
        {
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string quotid = ((Label)this.gvProcess.Rows[RowIndex].FindControl("lblQId")).Text.Trim();
            GetQuotDataFunctionality(quotid);
            List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
            int isContain = obj.Where(x => x.resourcecode.StartsWith("01")).ToList().Count;
            if (isContain == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"No Material for Material Requisition" + "');", true);
            }
            else
            {
                UpdateMaterialRequisition(quotid);
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
        private void UpdateMaterialRequisition(string Qid)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = GetComCode();
            string reqno = lblReqno.Text;
            string reqdate = System.DateTime.Today.ToString();
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string PostedDat = Date;
            string pactcode = lblActcode.Text == "" ? "" : "16" + ASTUtility.Right(lblActcode.Text, 10);
            string flrcod = "000";
            string requsrid = "";
            string mrfno = Qid;
            string reqnar = "";
            string CRMPostedByid = userid;
            string CRMPosttrmid = Terminal;
            string CRMPostSession = Sessionid;
            string CRMPostedDat = Date;
            string checkbyid = userid;
            string quotid = Qid;
            List<EQuotation> obj = ((List<EQuotation>)ViewState["MaterialList"]).Where(x => x.resourcecode.StartsWith("01") && x.isprocess==true).ToList();

            DataSet ds1 = new DataSet("ds1");
            this.CreateDataTable();
            DataTable dt = (DataTable)ViewState["tblapproval"];
            DataRow dr1 = dt.NewRow();
            dr1["fappid"] = userid;
            dr1["fappdat"] = Date;
            dr1["fapptrmid"] = Posttrmid;
            dr1["fappseson"] = PostSession;
            dr1["sappid"] = userid;
            dr1["sappdat"] = Date;
            dr1["sapptrmid"] = Posttrmid;
            dr1["sappseson"] = PostSession;
            dt.Rows.Add(dr1);
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            string approval = ds1.GetXml();

            bool result = _process.UpdateTransInfo2(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATPURREQB", reqno, reqdate, pactcode, flrcod, requsrid, mrfno, reqnar,
                PostedByid, Posttrmid, PostSession, PostedDat, CRMPostedByid, CRMPosttrmid, CRMPostSession, CRMPostedDat, "", "", "", "", "", "");


            result = _process.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEREQCHECKED", reqno, checkbyid, Terminal,
                Sessionid, Date, approval, "", "",
                "", "AAAAAAAAAAAA", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                int i = 1;
                List<bool> resultCompA = new List<bool>();
                foreach (var item in obj)
                {
                    string rowId = i.ToString();
                    string mRSIRCODE = item.resourcecode.ToString();
                    string mSPCFCOD = "000000000000";

                    double mPREQTY = Convert.ToDouble(item.aprqty);
                    double mAREQTY = Convert.ToDouble("0.00");
                    double mBgdBalQty = Convert.ToDouble("0.00");
                    string mREQRAT = "0.00";      //item.rate.ToString();
                    string mREQSRAT = "0.00";     //item.rate.ToString();
                    string mPSTKQTY = "0.00";
                    string mEXPUSEDT = "";
                    string mREQNOTE = "";
                    string PursDate = "";
                    string Lpurrate = "0.00";
                    string storecode = "";
                    string ssircode = "";
                    string orderno = "";



                    bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATPURREQA", "",
                              reqno, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                              PursDate, Lpurrate, storecode, ssircode, orderno, mREQSRAT, rowId, "", "", "", "", "", "");
                    resultCompA.Add(resultA);
                    i++;
                }
                if (resultCompA.Contains(false))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
                else
                {
                    bool resultMatReq = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATEMATREQNO", quotid, reqno, "", "", "", "", "", "",
                                       "", "", "", "", "", "", "", "", "", "", "", "", "");

                    ModuleName();
                    getProcess();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Material Requisition Generated" + "');", true);

                }
            }
        }
        #endregion

        #region Sub Contractor Section
        protected void lnkbtnSubCon_Click(object sender, EventArgs e)
        {
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string quotid = ((Label)this.gvProcess.Rows[RowIndex].FindControl("lblqid")).Text.Trim();
            lblQuotation.Text = quotid;
            GetQuotDataFunctionality(quotid);
            List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
            int isContain = obj.Where(x => x.resourcecode.StartsWith("04") && x.resourcecode != "049700101001" && x.isprocess == true).ToList().Count;
            if (isContain == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"No Labour Work Found" + "');", true);
            }
            else
            {
                getSubContractor();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModalSubContractor();", true);
            }
        }

        private void getSubContractor()
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "GETSUBCONTRACTOR", "", "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                ddlSubContractor.DataSource = ds.Tables[0];
                ddlSubContractor.DataTextField = "sirdesc";
                ddlSubContractor.DataValueField = "sircode";
                ddlSubContractor.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        protected void lnkSubContractorSave_Click(object sender, EventArgs e)
        {

            List<EQuotation> obj = ((List<EQuotation>)ViewState["MaterialList"]).Where(x => x.resourcecode.StartsWith("04") && x.resourcecode != "049700101001" && x.isprocess == true).ToList();
            if (obj.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"No Labour Work Found" + "');", true);
            }
            else
            {
                string comcod = GetComCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string trmid = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");

                DataSet ds2 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTLABISSUENO", curdate,
                          "LIS", "", "", "", "", "", "", "");
                string csircode = ddlSubContractor.SelectedValue.ToString();
                string pactcode = lblActcode.Text == "" ? "" : "16" + ASTUtility.Right(lblActcode.Text, 10);


                string mISUNO = ds2.Tables[0].Rows[0]["maxmisuno"].ToString();
                string mISURNAR = "";
                string Remarks = "N/A";
                string trade = "";
                string rano = "";
                string percentage = Convert.ToDouble("0").ToString();
                string sdamt = Convert.ToDouble("0").ToString();
                string dedamt = Convert.ToDouble("0").ToString();
                string Penalty = Convert.ToDouble("0").ToString();
                string advamt = Convert.ToDouble("0").ToString();
                string Reward = Convert.ToDouble("0").ToString();
                string workorder = "";


                bool result = _process.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEB",
                                 mISUNO, curdate, pactcode, csircode, mISURNAR, Remarks, userid, Sessionid, trmid, trade, rano,
                                 percentage, sdamt, dedamt, Penalty, advamt, Reward, workorder, "", "");

                result = _process.UpdateTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATEPURLISSUEB", mISUNO, "S");

                // todo for auto approval 
                result = this.BillApprovalCompwise(mISUNO);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
                else
                {

                    List<bool> resultCompA = new List<bool>();
                    foreach (var item in obj)
                    {
                        string Flrcod = "000";
                        string Rsircode = item.resourcecode.ToString();
                        string prcent = Convert.ToDouble("0").ToString();
                        string Isuqty = item.aprqty.ToString();
                        string Isuamt = item.apramt.ToString();
                        string wrkqty = Convert.ToDouble("0").ToString();
                        string grp = "001";
                        string mbbook = "";
                        string above = Convert.ToDouble("0").ToString();
                        string dedqty = Convert.ToDouble("0").ToString();
                        string dedunit = "";
                        string idedamt = Convert.ToDouble("0").ToString();

                        bool resultA = _process.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                                Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");

                        resultCompA.Add(resultA);

                    }
                    if (resultCompA.Contains(false))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                    }
                    else
                    {
                        string quotid = lblQuotation.Text;
                        bool resultMatReq = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATESUBCONTRACTOR", quotid, mISUNO, "", "", "", "", "", "",
                                           "", "", "", "", "", "", "", "", "", "", "", "", "");
                        ModuleName();
                        getProcess();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Sub COntractor Bill Generated" + "');", true);
                    }

                }

            }


        }
        // todo for auto bill approval 
        private bool BillApprovalCompwise(string _issuno)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string session = hst["session"].ToString();
            string trmnid = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool isSuccess = false; 
            switch (comcod)
            {
                case "3368": // finlay
                case "3367": // epic
                case "3370": // cpdl
                case "3366": // lanco
                    break;
                default:
                    isSuccess = _process.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "ISSUEAPPROVEDAUTO", _issuno, usrid, Date, trmnid, session, "", "", "", "", "", "", "", "", "", "");
                    break;
            }
            return isSuccess;
        }
        #endregion

        #region General Bill

        protected void lnkbtnGenBill_Click(object sender, EventArgs e)
        {
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string quotid = ((Label)this.gvProcess.Rows[RowIndex].FindControl("lblqid")).Text.Trim();
            GetQuotDataFunctionality(quotid);
            List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
            int isContain = obj.Where(x => x.resourcecode.StartsWith("12") && x.isprocess == true).ToList().Count;
            if (isContain == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"No Over Head found" + "');", true);
            }
            else
            {
                updateGen(quotid);
                //UpdateMaterialRequisition(quotid);
            }
        }

        protected void GetReqNoGenBill()
        {
            string comcod = this.GetComCode();
            string mREQNO = "NEWREQ";

            string mREQDAT = System.DateTime.Today.ToString();
            if (mREQNO == "NEWREQ")
            {
                DataSet ds2 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTREQINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblGenReqno.Text = ds2.Tables[0].Rows[0]["maxreqno"].ToString();
                }
                else
                {
                    lblGenReqno.Text = "";
                }
            }

        }

        private void updateGen(string Qid)
        {
            List<EQuotation> obj = ((List<EQuotation>)ViewState["MaterialList"]).Where(x => x.resourcecode.StartsWith("12") && x.isprocess == true).ToList();

            Hashtable hst = (Hashtable)Session["tblLogin"];


            string comcod = this.GetComCode();
            string mMRFNO = "";

            this.GetReqNoGenBill();
            string mREQDAT = System.DateTime.Today.ToString();
            string mREQNO = lblGenReqno.Text;


            string type = "OreqApproved";

            //log Report


            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = userid;

            string Posttrmid = Terminal;

            string PostSession = Sessionid;
            string posteddat = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string ApprovByid = "";
            string approvdat = "01-Jan-1900";
            string Approvtrmid = "";
            string ApprovSession = "";


            /////log end

            string nARRATION = "";

            string paytype = "Cheque";
            string payto = "";
            string supcode = "000000000000";
            string termncon = "";
            string payofmod = "";
            supcode = (supcode.Trim() == "" ? "000000000000" : supcode);
            bool result = true;
            string adjcod = "000000000000";
            string bundleno = "";


            string bankcode = "000000000000";
            string refnum = ""; //Cheque No

            for (int i = 0; i < obj.Count; i++)
            {
                string mPACTCODE = lblActcode.Text == "" ? "" : "16" + ASTUtility.Right(lblActcode.Text, 10);
                string mRSIRCODE = obj[i].resourcecode;
                string spcfcod = "000000000000";
                string billno = "";
                double mProAMT = Convert.ToDouble(obj[i].apramt);
                double mAPPAMT = Convert.ToDouble("0");
                double qty = Convert.ToDouble(obj[i].aprqty);
                double rate = Convert.ToDouble(obj[i].aprrate);
                double ppdamt = Convert.ToDouble("0");

                string Approval = "";
                string advanced = "0";
                string attnper = "";


                if (mProAMT > 0)
                {
                    result = _process.UpdateTransInfo01(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTOTHERREQ",
                             mREQNO, mPACTCODE, mRSIRCODE, mREQDAT, mMRFNO, mProAMT.ToString(), mAPPAMT.ToString(), nARRATION,
                             PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, qty.ToString(), paytype, payto, ppdamt.ToString(), posteddat, supcode, spcfcod, adjcod, type, termncon, payofmod, bundleno, billno, bankcode, refnum, Approval, advanced, attnper);
                }
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured saving General bill" + "');", true);

                }

            }

            bool resultMatReq = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATEGENERALBILL", Qid, mREQNO, "", "", "", "", "", "",
                                      "", "", "", "", "", "", "", "", "", "", "", "", "");

            ModuleName();
            getProcess();


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"General Bill Generated" + "');", true);




            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Other Req Entry";
                string eventdesc = "Update Req";
                string eventdesc2 = mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }





        #endregion

        protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkbtnPrintQT = (HyperLink)e.Row.FindControl("lnkbtnPrintQT");
                HyperLink lnkbtnPrintIn = (HyperLink)e.Row.FindControl("lnkbtnPrintIn");
                HyperLink hlinkproceed = (HyperLink)e.Row.FindControl("lnkProceed");


                string quotid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "quotid")).ToString();
                string quotid1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "quotid1")).ToString();
                string qamt = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "qamt")).ToString();

                string pactcode = "18"+ ASTUtility.Right(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mapactcode")).ToString(), 10);
                hlinkproceed.NavigateUrl = "~/F_23_CR/CustOthMoneyReceipt?Type=Billing&pactcode=" + pactcode + "&quotid="+ quotid + "&qamt="+ qamt;               
                hlinkproceed.Visible = true;
                lnkbtnPrintQT.NavigateUrl = "~/F_70_Services/ServicePrint?Type=PrintQuote&QId=" + quotid;
                lnkbtnPrintIn.NavigateUrl = "~/F_70_Services/ServicePrint?Type=PrintInvoice&QId=" + quotid; 
            }
        }
    }
}
