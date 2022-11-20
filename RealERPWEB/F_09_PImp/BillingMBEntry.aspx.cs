﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mail;
using System.Web.Mail;
using RealERPLIB;
using RealERPRPT;
using System.Net;
using EASendMail;
using System.IO;
using System.Drawing;
using AjaxControlToolkit;
using RealEntity;

namespace RealERPWEB.F_09_PImp
{
    public partial class BillingMBEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        UserManPurchase objUserMan = new UserManPurchase();
        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.txtCurOrderDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurOrderDate_CalendarExtender.EndDate = System.DateTime.Today;


                this.GetProjectList();
                this.GetContractorList();
                string ordero = this.Request.QueryString["genno"] ?? "";
                if (ordero.Length > 0)
                {
                    if (ordero.Substring(0, 3) == "MBK")
                    {

                        this.lbtnPrevOrderList_Click(null, null);
                        this.lbtnOk_Click(null, null);

                    }

                    else
                    {
                        this.hdnorderno.Value = ordero;

                    }

                }
               

            }
        }



        private void GetProjectList()
        {

            string comcod = this.GetCompCode();
            string qprjcode = this.Request.QueryString["prjcode"] ?? "";
            string srchproject = (qprjcode.Length > 0 ? qprjcode : "") + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJODRLIST", srchproject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            ds1.Dispose();


        }

        private void GetContractorList()
        {
            string comcod = this.GetCompCode();
            string qsircode = this.Request.QueryString["sircode"] ?? "";
            string conlist = (qsircode.Length > 0 ? qsircode : "") + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUECONTLIST", conlist, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlContractor.DataTextField = "sircode1";
            this.ddlContractor.DataValueField = "sircode";
            this.ddlContractor.DataSource = ds1.Tables[0];
            this.ddlContractor.DataBind();

        }




        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //  ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click2);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkPrint_Click2(object sender, EventArgs e)
        {

            //string orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            //string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            //string currentptah = "PurchasePrint.aspx?Type=MktOrderPrint&orderno=" + orderno;
            //string totalpath = hostname + currentptah;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

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



        protected void lbtnPrevOrderList_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
                string qmbno = this.Request.QueryString["genno"] ?? "";
                string mbno = (qmbno.Length == 0 ? "" : this.Request.QueryString["genno"].ToString()) + "%";


                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVIOUSMBNO", CurDate1,
                              mbno, "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.ddlPrevOrderList.Items.Clear();
                this.ddlPrevOrderList.DataTextField = "orderno1";
                this.ddlPrevOrderList.DataValueField = "orderno";
                this.ddlPrevOrderList.DataSource = ds1.Tables[0];
                this.ddlPrevOrderList.DataBind();
                ds1.Dispose();
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }
            
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "New")
            {

                this.lblpreviousmb.Visible = true;
                this.lbtnPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Items.Clear();
              
                this.txtCurOrderDate.Enabled = true;
                this.txtRefNo.Text = "";
               
                this.lbtnOk.Text = "Ok";
                return;
            }
            this.lblpreviousmb.Visible = false;
            this.lbtnPrevOrderList.Visible = false;
            this.ddlPrevOrderList.Visible = false;
            this.txtmbno2.ReadOnly = true;
            this.lbtnOk.Text = "New";
            this.Get_MB_Info();
        }

        protected void GetMBNo()
        {

            string comcod = this.GetCompCode();
            string mOrderdate = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mMBNo = "NEWMB";
            if (this.ddlPrevOrderList.Items.Count > 0)
                mMBNo = this.ddlPrevOrderList.SelectedValue.ToString();

            if (mMBNo == "NEWMB")
            {



                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTMBNO", mOrderdate, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblmbno1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtmbno2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                    this.ddlPrevOrderList.DataTextField = "maxno1";
                    this.ddlPrevOrderList.DataValueField = "maxno";
                    this.ddlPrevOrderList.DataSource = ds1.Tables[0];
                    this.ddlPrevOrderList.DataBind();
                }



            }
        }









        protected void Get_MB_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mMBNo = "NEWMB";

            if (this.ddlPrevOrderList.Items.Count > 0)
            {
                // this.ddlSuplierList.Items.Clear();
                this.txtCurOrderDate.Enabled = false;

                mMBNo = this.ddlPrevOrderList.SelectedValue.ToString();
            }

            DataTable dt2 = (DataTable)ViewState["tblProject"];
            string pactcode = "";
            if (dt2 != null)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    pactcode += dt2.Rows[i]["pactcode"].ToString();
                }
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETMBINFO", mMBNo, CurDate1, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmb"] = ds1.Tables[0];
            ViewState["tblcorder"] = ds1.Tables[2];
        

            if (mMBNo == "NEWMB")
            {

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTMBNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblmbno1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtmbno2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                }

                this.GetCorderListInfo();


                return;

            }

            this.hdnorderno.Value= ds1.Tables[1].Rows[0]["orderno"].ToString().Substring(0, 6); 
            this.lblmbno1.Text = ds1.Tables[1].Rows[0]["mbno1"].ToString().Substring(0, 6);
            this.txtmbno2.Text = ds1.Tables[1].Rows[0]["mbno1"].ToString().Substring(6, 5);
            this.txtRefNo.Text = ds1.Tables[1].Rows[0]["mbrefno"].ToString();


            this.txtCurOrderDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["mbdat"]).ToString("dd.MM.yyyy");


            this.gvOrderInfo_DataBind();
        }

        private void GetCorderListInfo()
        {


            ViewState.Remove("tblcorder");
            string comcod = this.GetCompCode();
            string orderno = this.Request.QueryString["genno"] ?? "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONORDERLISTINFO", orderno, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvcorder.DataSource = null;
                this.gvcorder.DataBind();
            }
            ViewState["tblcorder"] = ds1.Tables[0];
            this.gvOrderInfo_DataBind();









        }
        protected void gvOrderInfo_DataBind()
        {
            try
            {
                string comcod = this.GetCompCode();
                DataTable tbl1 = (DataTable)ViewState["tblcorder"];
                this.gvcorder.DataSource = tbl1;
                this.gvcorder.DataBind();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;
            }

        }



        protected void SaveValue()
        {
            //DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            //for (int j = 0; j < this.gvOrderInfo.Rows.Count; j++)
            //{
            //    string acttypeCode = ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvActTypeCode")).Text.Trim();

            //    double dgvorderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderQty")).Text.Trim()));
            //    double dgvOrderRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvOrderRate")).Text.Trim()));
            //    double dgvAppAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderAmt")).Text.Trim()));
            //    string rsirDetDesc = ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvRsirdetDesc1")).Text.Trim();

            //    if(acttypeCode.Substring(0, 7) == "0199999")
            //    {
            //        tbl1.Rows[j]["ordrqty"] = dgvorderQty;
            //        tbl1.Rows[j]["ordramt"] = dgvAppAmt;
            //    }
            //    else
            //    {
            //        dgvAppAmt = dgvorderQty * dgvOrderRate;
            //        tbl1.Rows[j]["ordrqty"] = dgvorderQty;
            //        tbl1.Rows[j]["ordramt"] = dgvAppAmt;
            //        tbl1.Rows[j]["rsirdetdesc"] = rsirDetDesc;
            //    }           

            //}
            // ViewState["tblOrder"] = tbl1;

        }
     




        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
            //    return;
            //}

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //this.Session_tblOrder_Update();

            //string mORDERDAT = this.GetStdDate(this.txtCurOrderDate.Text.Trim());




            //string mSSIRCODE = this.ddlSuplierList.Items.Count > 0 ? this.ddlSuplierList.SelectedValue.ToString() : this.lssircode.Text.Trim();           
            //string mPORDREF = this.txtOrderRefNo.Text.Trim();
            //string mLETERDES = this.txtLETDES.Text.Trim();
            //string mPORDNAR = this.txtOrderNarr.Text.Trim();
            //string subject = this.txtSubject.Text.Trim();
            //double AdvAmt = Convert.ToDouble("0" + this.txtadvAmt.Text.Trim());
            ////log report

            //string userid = hst["usrid"].ToString();
            //string Terminal = hst["compname"].ToString();
            //string Sessionid = hst["session"].ToString();

            ////end log
            //bool result = false;

            ////Balance Approval
            //DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            //foreach (DataRow drf in tbl1.Rows)
            //{


            //    bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(drf["aprovdat"].ToString()), Convert.ToDateTime(mORDERDAT));
            //    if (!dcon)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Order Date is equal or greater Approved Date" + "');", true);
            //        return;
            //    }

            //}


            //if (this.ddlPrevOrderList.Items.Count == 0)
            //    this.GetOrderNo();

            //string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();



            //double netamt = Convert.ToDouble(((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text.ToString());  //(Label)gvOrderInfo.FindControl("lblgvFooterTOrderAmt");


            //if (AdvAmt <= netamt)
            //{ }

            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Advanced Amount must be equal /less in Total Amount" + "');", true);
            //    return;
            //}




            //string appxml = tbl1.Rows[0]["approval"].ToString();
            //string Approval = this.GetReqApproval(appxml);


            //bool forwarddesc = ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Checked ? true : false;
            //string type = this.Request.QueryString["InputType"];
            //switch (type)
            //{
            //    case "FirstApp":
            //        tbl1.Rows[0]["forward"] = forwarddesc;
            //        break;

            //    default:
            //        break;



            //}


            //string terms = "";
            //bool istxtTerms = true;
            //string advamt = ASTUtility.StrPosOrNagative(this.txtadvAmt.Text.Trim()).ToString();

            //string forward = (tbl1.Rows[0]["forward"].ToString().Trim().Length == 0) ? "False" : tbl1.Rows[0]["forward"].ToString();
            //result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERB",
            //                 mORDERNO, mORDERDAT, mSSIRCODE, mPORDREF, mLETERDES, mPORDNAR, subject, userid, Terminal, Sessionid, Approval, forward,
            //                 advamt, "", "");
            //if (!result)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
            //    return;
            //}


            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{

            //    bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["aprovdat"].ToString()), Convert.ToDateTime(mORDERDAT));
            //    if (!dcon)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Order Date is equal or greater Approved Date" + "');", true);
            //        return;
            //    }



            //    string mREQNO = tbl1.Rows[i]["reqno"].ToString();
            //    string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
            //    string prtype = tbl1.Rows[i]["prtype"].ToString();
            //    string acttype = tbl1.Rows[i]["acttype"].ToString();
            //    string mkttype = tbl1.Rows[i]["mkttype"].ToString();
            //    double mAprovqty = Convert.ToDouble(tbl1.Rows[i]["aprvqty"]);
            //    double mORDRQTY = Convert.ToDouble(tbl1.Rows[i]["ordrqty"]);

            //    if (mAprovqty < mORDRQTY)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Order Qty Must be Less Or Equal  Approve Qty" + "');", true);
            //        return;
            //    }

            //    //result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURPROPOSAL", mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mORDERNO, mORDRQTY.ToString(), "", "", "", "", "", "", "", "");

            //    if (mREQNO != "")
            //    {
            //        result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERA",
            //                                     mORDERNO, mREQNO, mORDRQTY.ToString(), prtype, acttype, mkttype, "", "", "", "", "", "", "", "", "", "", "","","","");
            //    }

            //    else
            //    {
            //        string mPactcode = tbl1.Rows[i]["pactcode"].ToString();
            //        string mOrderAmt = Convert.ToDouble(tbl1.Rows[i]["ordramt"]).ToString();
            //        result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERE", mORDERNO, mPactcode, acttype, "000000000000", mOrderAmt, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //    }

            //    if (!result)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
            //        return;
            //    }
            //}









        }









        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.gvOrderInfo_DataBind();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }










        protected void gvcorder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvcorder.PageIndex = e.NewPageIndex;
        }

        protected void lbtnDetails_Click(object sender, EventArgs e)
        {


            try
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                int index = this.gvcorder.PageSize * this.gvcorder.PageIndex + RowIndex;
                string rsircode = ((DataTable)ViewState["tblcorder"]).Rows[index]["rsircode"].ToString();
                string rsirdesc = ((DataTable)ViewState["tblcorder"]).Rows[index]["rsirdesc"].ToString();
                string rsirunit = ((DataTable)ViewState["tblcorder"]).Rows[index]["rsirunit"].ToString();
                string flrcod = ((DataTable)ViewState["tblcorder"]).Rows[index]["flrcod"].ToString();
                string flrdes = ((DataTable)ViewState["tblcorder"]).Rows[index]["flrdes"].ToString();
                DataTable dt = (DataTable)ViewState["tblmb"];
                this.hdnrsircode.Value = rsircode;
                this.hdnflrcod.Value = flrcod;

                DataRow[] dr1 = dt.Select("rsircode='" + rsircode + "' and flrcod='" + flrcod + "'");
                int sl = 1;
                if (dr1.Length == 0)
                {
                    //5 Row Added
                    for (int i = 0; i < 5; i++)
                    {

                        DataRow dradd = dt.NewRow();
                        dradd["rsircode"] = rsircode;
                        dradd["rsirdesc"] = rsirdesc;
                        dradd["rsirunit"] = rsirunit;
                        dradd["flrcod"] = flrcod;
                        dradd["flrdes"] = flrdes;
                        dradd["sl"] = sl;
                        dradd["nos"] = 0.00;
                        dradd["lnght"] = 0.00;
                        dradd["breadth"] = 0.00;
                        dradd["height"] = 0.00;
                        dradd["uweight"] = 0.00;
                        dradd["tweight"] = 0.00;
                        dradd["remarks"] = "";
                        dt.Rows.Add(dradd);
                        sl++;

                    }



                }


                ViewState["tblmb"] = dt;
                this.Data_Bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalDetails();", true);
            }


            catch (Exception ex)
            {

                string msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);



            }
        }
        private void Data_Bind()
        {
            try
            {

               
                string  rsircode = this.hdnrsircode.Value;
                string flrcod = this.hdnflrcod.Value;
                DataTable  dt = ((DataTable)ViewState["tblmb"]).Copy();
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = ("rsircode='" + rsircode + "' and flrcod='" + flrcod + "'");
                this.gvdetails.DataSource = dv1.ToTable() ;
                this.gvdetails.DataBind();

                
                 this.FooterCalCulation(dv1.ToTable());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }




        }


        private void FooterCalCulation(DataTable dt)
        {

            
            if (dt.Rows.Count > 0)
                ((Label)this.gvdetails.FooterRow.FindControl("lgvFtoWeight")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tweight)", "")) ?
                                          0 : dt.Compute("sum(tweight)", ""))).ToString("#,##0.0000;-#,##0.0000; ");

        }
    

        private void SaveValueDetails()
        {


            DataTable dt = (DataTable)ViewState["tblmb"];
            int rowindex, i = 0;
            double toweight = 0;
            string rsircode, flrcod;
            rsircode = this.hdnrsircode.Value;
            flrcod = this.hdnflrcod.Value;

            foreach (GridViewRow gv1 in gvdetails.Rows)
            {


                string sl =  ((Label)gv1.FindControl("lblgvserial")).Text.Trim(); 
                double nos = ASTUtility.StrPosOrNagative(((TextBox)gv1.FindControl("txtgvnos")).Text.Trim());
                double Length = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvlength")).Text.Trim());
                double breadth = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvbreadth")).Text.Trim());
                double height = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvheight")).Text.Trim());
                double uweight = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvuweight")).Text.Trim());
                toweight = ASTUtility.StrPosOrNagative(((TextBox)gv1.FindControl("txtgvtotalweight")).Text.Trim());
                string remarks = ((TextBox)gv1.FindControl("txtgvremarks")).Text.Trim();

               // toweight = nos * Length * breadth * height * uweight;
                //rowindex = (gvdetails.PageIndex) * gvdetails.PageSize + i;

                DataRow[] dr1 = dt.Select("rsircode='" + rsircode + "' and flrcod='" + flrcod + "' and sl='" + sl + "'");               
                dr1[0]["nos"] = nos;
                dr1[0]["lnght"] = Length;
                dr1[0]["breadth"] = breadth;
                dr1[0]["height"] = height;
                dr1[0]["uweight"] = uweight;
                dr1[0]["tweight"] = toweight;
                dr1[0]["remarks"] = remarks;
                i++;
            }


            ViewState["tblmb"] = dt;



        }

        protected void lnkbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValueDetails();
            this.Data_Bind();
          //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModalDetails();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModalDetailsBack();", true);

        }

        protected void lbtnDeldet_Click(object sender, EventArgs e)
        {

            // Log Data


            DataTable dt = (DataTable)ViewState["tblmb"];
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string rsircode = ((Label)this.gvdetails.Rows[RowIndex].FindControl("lblgvItmCode")).Text.Trim();
            string flrcod = ((Label)this.gvdetails.Rows[RowIndex].FindControl("lblgvflrcod")).Text.Trim();
            string sl = ((Label)this.gvdetails.Rows[RowIndex].FindControl("lblgvserial")).Text.Trim();
            DataRow dr1 = dt.Select("rsircode='" + rsircode + "' and flrcod='" + flrcod + "' and sl='" + sl + "'")[0];
            dt.Rows.Remove(dr1);
            dt.AcceptChanges();

            ViewState["tblmb"] = dt;
            this.Data_Bind();
           // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModalDetails();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModalDetailsBack();", true);
        }

        protected void lbtnAddRow_Click(object sender, EventArgs e)
        {


            DataTable dt = ((DataTable)ViewState["tblmb"]).Copy();
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string rsircode = ((Label)this.gvdetails.Rows[RowIndex].FindControl("lblgvItmCode")).Text.Trim();
            string flrcod = ((Label)this.gvdetails.Rows[RowIndex].FindControl("lblgvflrcod")).Text.Trim();
            string sl = ((Label)this.gvdetails.Rows[RowIndex].FindControl("lblgvserial")).Text.Trim();
            DataRow dr1 = dt.Select("rsircode='" + rsircode + "' and flrcod='" + flrcod + "' and sl='" + sl + "'")[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rsircode='" + rsircode + "' and flrcod='" + flrcod + "'");
            DataTable dt1 = dv.ToTable();
            
            int asl = Convert.ToInt32(dt1.Rows[dt1.Rows.Count - 1]["sl"]);
            asl++;
            
            //Add Row
            DataRow dradd = dt.NewRow();
            dradd["rsircode"] = dr1["rsircode"];
            dradd["rsirdesc"] = dr1["rsirdesc"];
            dradd["rsirunit"] = dr1["rsirunit"];
            dradd["flrcod"] = dr1["flrcod"];
            dradd["flrdes"] = dr1["flrdes"];
            dradd["sl"] = asl;
            dradd["nos"] = dr1["nos"];
            dradd["lnght"] = dr1["lnght"];
            dradd["breadth"] = dr1["breadth"];
            dradd["height"] = dr1["height"];
            dradd["uweight"] = dr1["uweight"];
            dradd["tweight"] = dr1["tweight"];
            dradd["remarks"] = dr1["remarks"];
            dt.Rows.Add(dradd);
            ViewState["tblmb"] = dt;
            this.Data_Bind();       
             ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModalDetailsBack();", true);

          //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalDetails();", true);





        }


        protected void lnkbtnCalculation_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblmb"];
            int i = 0;
            double toweight = 0;
            string rsircode, flrcod;
            rsircode = this.hdnrsircode.Value;
            flrcod = this.hdnflrcod.Value;
            foreach (GridViewRow gv1 in gvdetails.Rows)
            {


                string sl = ((Label)gv1.FindControl("lblgvserial")).Text.Trim();
                double nos = ASTUtility.StrPosOrNagative(((TextBox)gv1.FindControl("txtgvnos")).Text.Trim());
                double Length = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvlength")).Text.Trim());
                double breadth = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvbreadth")).Text.Trim());
                double height = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvheight")).Text.Trim());
                double uweight = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvuweight")).Text.Trim());
                // toweight = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvtotalweight")).Text.Trim());
                string remarks = ((TextBox)gv1.FindControl("txtgvremarks")).Text.Trim();

                toweight = Math.Round((nos * Length * breadth * height * uweight), 4);
                //rowindex = (gvdetails.PageIndex) * gvdetails.PageSize + i;

                DataRow[] dr1 = dt.Select("rsircode='" + rsircode + "' and flrcod='" + flrcod + "' and sl='" + sl + "'");
                dr1[0]["nos"] = nos;
                dr1[0]["lnght"] = Length;
                dr1[0]["breadth"] = breadth;
                dr1[0]["height"] = height;
                dr1[0]["uweight"] = uweight;
                dr1[0]["tweight"] = toweight;
                dr1[0]["remarks"] = remarks;
                i++;
            }


            ViewState["tblmb"] = dt;

            this.Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModalDetailsBack();", true);

        }

        protected void lbtnUpdatembinfo_Click(object sender, EventArgs e)
        {




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValueDetails();
            DataTable dt = (DataTable)ViewState["tblmb"];
            bool result = false;
         

              if (this.ddlPrevOrderList.Items.Count == 0)
                this.GetMBNo();
            string mbdat= this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mbno = this.lblmbno1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblmbno1.Text.Trim().Substring(3, 2) + this.txtmbno2.Text.Trim();
            string orderno = this.hdnorderno.Value;
            string mbrefno = this.txtRefNo.Text.Trim();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");




            result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_03", "INSERTORUPDATEPURCONMBAB", "PURCONMBB",
                             mbno, orderno,mbrefno, mbdat,userid, Terminal, Date, Sessionid, "", "",
                             "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
                }




                foreach (DataRow dr in dt.Rows)
           
                {
                string rsircode = dr["rsircode"].ToString();
                string flrcod = dr["flrcod"].ToString();
                string sl = dr["sl"].ToString();
                string nos = dr["nos"].ToString();
                string Length = dr["lnght"].ToString();
                string breadth = dr["breadth"].ToString();
                string height = dr["height"].ToString();
                
                string uweight = dr["uweight"].ToString();
                string toweight = dr["tweight"].ToString();
                string remarks= dr["remarks"].ToString();



                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "INSERTORUPDATEPURCONMBAB", "PURCONMBA", mbno,
                        rsircode, flrcod, sl, nos, Length, breadth, height, uweight, toweight, remarks, "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"]+"');", true);
                   
                  
                    return;
                }
                }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModalDetails();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);


        }

    }    
            
        
    
}