using System;
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

namespace RealERPWEB.F_28_MPro
{
    public partial class MktWorkOrderEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        UserManPurchase objUserMan = new UserManPurchase();
        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Marketing Purchase Order";
                this.txtCurOrderDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtLETDES.Text = comnam + this.CompanySubject();
                this.txtCurOrderDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.SendMail();

                //only current date

                // this.CurDate();

                if (Session["tblordrange"] == null)
                {
                    this.GetOrderRange();
                    this.btnSendmail.Visible = false;
                }
                string ordero = this.Request.QueryString["genno"].ToString().Trim();
                if (ordero.Length > 0)
                {
                    if (ordero.Substring(0, 3) == "POR")
                    {

                        this.lbtnPrevOrderList_Click(null, null);
                        this.lbtnOk_Click(null, null);

                    }

                }

            }
        }

      
        private void SendMail()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3335": //Edison
                             //case "3101":

                    this.btnSendmail.Visible = false;
                    break;

                default:
                    break;


            }

        }

        private void GetOrderRange()
        {

            Session.Remove("tblordrange");
            string comcod = this.GetCompCode();
            List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> lst = objUserMan.GetOrderRange(comcod);
            Session["tblordrange"] = lst;



        }
        private string CompanySubject()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comsubject = "";
            switch (comcod)
            {
                case "3330":
                    //case "3101":
                    comsubject = " requests you to arrange supply of following materials from your organization.";
                    break;
                default:
                    comsubject = " requests you to  supply the following materials from your organization.";
                    break;
            }
            return comsubject;
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



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();


            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            //string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "F_99_Allinterface/";
            string currentptah = "PurchasePrint.aspx?Type=OrderPrint&orderno=" + orderno;
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

            // lbtnPrint.PostBackUrl = "~/F_99_Allinterface/PurchasePrint.aspx?Type=OrderPrint&orderno=" + orderno;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private string CompanyPrintWorkOrder()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PrintWorkOrder = "";
            switch (comcod)
            {
                case "1301":
                case "3301":
                    PrintWorkOrder = "PrintWorkOrder02";
                    break;
                default:
                    PrintWorkOrder = "PrintWorkOrder";
                    break;
            }
            return PrintWorkOrder;
        }
      
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
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
        protected void lbtnPrevOrderList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string length = this.CompanyLength();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string orderno = (this.Request.QueryString["genno"].ToString().Trim().Length == 0 ? "" : this.Request.QueryString["genno"].ToString()) + "%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_PREV_ORDER_LIST", CurDate1,
                          orderno, length, usrid, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevOrderList.Items.Clear();
            this.ddlPrevOrderList.DataTextField = "orderno1";
            this.ddlPrevOrderList.DataValueField = "orderno";
            this.ddlPrevOrderList.DataSource = ds1.Tables[0];
            this.ddlPrevOrderList.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "New")
            {
                this.MultiView1.ActiveViewIndex = -1;

                //this.lblPrevious.Visible = true;
                //this.txtsearchpre.Visible = true;
                this.lbtnPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Items.Clear();
                this.lblCurOrderNo1.Text = "POR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurOrderDate.Enabled = true;
                this.lblissueno.Enabled = true;
                this.txtOrderRefNo.Text = "";
                this.txtOrderRefNo.ReadOnly = false;
                this.lssircode.Text = "";

                //For Charging
                ViewState.Remove("tblproject");
                this.ddlProjectName.Items.Clear();



                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtOrderNarr.Text = "";
                this.lblissueno.Text = "";
                this.gvOrderInfo.DataSource = null;
                this.gvOrderInfo.DataBind();
                this.gvOrderTerms.DataSource = null;
                this.gvOrderTerms.DataBind();
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                this.lbtnOk.Text = "Ok";
                this.ddlSuplierList.Items.Clear();
                return;
            }


            this.lbtnPrevOrderList.Visible = false;
            this.ddlPrevOrderList.Visible = false;
            this.txtCurOrderNo2.ReadOnly = true;
            // this.lblissueno.Enabled = true;
            this.lbtnOk.Text = "New";

            if (this.ddlPrevOrderList.Items.Count <= 0)
            {
                this.MultiView1.ActiveViewIndex = 0;
                this.ResourceForOrder();
                return;

            }
            this.MultiView1.ActiveViewIndex = 1;
            this.Get_Pur_Order_Info();
            //this.lbtnPrevOrderList_Click(null, null);
            this.ShowProjectFiles();
            this.hideTermsConditions();
        }

        private void GetProConPerson(string pactcode)
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_ORDER_TERMS", pactcode, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.gvOrderTerms.DataSource = ds2.Tables[0];
                this.gvOrderTerms.DataBind();
            }
        }


        private void GetPreNarration()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_PREV_NARRATION:", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.txtOrderNarr.Text = ds2.Tables[0].Rows[0]["pordnar"].ToString().Trim();
            }

        }
        private void GetOrRefno(string pactcode)
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_ORDER_REF_NO", pactcode, "", "", "", "", "", "", "", "");
            this.txtOrderRefNo.Text = ds2.Tables[0].Rows[0]["pordref"].ToString();

        }
        protected void GetOrderNo()
        {

            string comcod = this.GetCompCode();
            string mOrderdate = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mOrderNo = "NEWORDER";
            if (this.ddlPrevOrderList.Items.Count > 0)
                mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();

            if (mOrderNo == "NEWORDER")
            {



                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_LAST_ORDER_INFO", mOrderdate, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurOrderNo1.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(0, 6);
                    this.txtCurOrderNo2.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(6, 5);
                    this.ddlPrevOrderList.DataTextField = "maxorderno1";
                    this.ddlPrevOrderList.DataValueField = "maxorderno";
                    this.ddlPrevOrderList.DataSource = ds1.Tables[0];
                    this.ddlPrevOrderList.DataBind();
                }



            }
        }

        private void ResourceForOrder()
        {
            ViewState.Remove("tblResP");
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string findSupplier = this.txtsrchSupplier.Text.Trim() + "%";
            string[] approvno;

            string qgenno = this.Request.QueryString["genno"] ?? "";
            string findReq = qgenno.Length == 0 ? "%%" : this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "ORDER_RESOURCE_INFO", CurDate1,
                         findSupplier, findReq, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            //this.ddlSuplierList.DataTextField = "ssirdesc1";
            //this.ddlSuplierList.DataValueField = "ssircode";
            //this.ddlSuplierList.DataSource = ds1.Tables[1];
            //this.ddlSuplierList.DataBind();
            ViewState["tblResP"] = ds1.Tables[0];
            ViewState["tblProject"] = ds1.Tables[1];
            this.Data_Bind();
            //this.ddlSuplierList_SelectedIndexChanged(null, null);
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblResP"];
            this.gvAprovInfo.DataSource = dt;
            this.gvAprovInfo.DataBind();
        }

        protected void imgSearchOrderno_Click(object sender, EventArgs e)
        {
            this.ResourceForOrder();
        }

        //protected void ddlSuplierList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = (DataTable)ViewState["tblResP"];
        //    string supcode = this.ddlSuplierList.SelectedValue.ToString();
        //    DataView dv1 = dt.DefaultView;
        //    dv1.RowFilter = "ssircode in ('" + supcode + "')";
        //    this.gvAprovInfo.DataSource = dv1.ToTable();
        //    this.gvAprovInfo.DataBind();

        //    //For Visible Item Serial Manama
        //    string comcod = GetCompCode();
        //    if (comcod == "3353" || comcod == "3101")
        //    {
        //        this.gvAprovInfo.Columns[1].Visible = true;
        //    }

        //}


        protected void Get_Pur_Order_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mOrderNo = "NEWORDER";
           
            if (this.ddlPrevOrderList.Items.Count > 0)
            {
                // this.ddlSuplierList.Items.Clear();
                this.txtCurOrderDate.Enabled = false;
                this.lblissueno.Enabled = false;
                mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();
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

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_PUR_ORDER_INFO", mOrderNo, CurDate1, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["dsOrder"] = ds1;
            ViewState["tblOrder"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["purtermcon"] = ds1.Tables[1];

            this.gvOrderTerms.DataSource = ds1.Tables[1];
            this.gvOrderTerms.DataBind();
           

            //ViewState["tblpaysch"] = ds1.Tables[2];
            //this.SchData_Bind();


            if (mOrderNo == "NEWORDER")
            {

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_LAST_ORDER_INFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurOrderNo1.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(0, 6);
                    this.txtCurOrderNo2.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(6, 5);
                }
                return;
            }

            this.lblCurOrderNo1.Text = ds1.Tables[2].Rows[0]["orderno1"].ToString().Substring(0, 6);
            this.txtCurOrderNo2.Text = ds1.Tables[2].Rows[0]["orderno1"].ToString().Substring(6, 5);
            this.txtOrderRefNo.Text = ds1.Tables[2].Rows[0]["pordref"].ToString();
            this.txtLETDES.Text = ds1.Tables[2].Rows[0]["leterdes"].ToString();
            this.txtSubject.Text = ds1.Tables[2].Rows[0]["subject"].ToString();

            this.txtCurOrderDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["orderdat"]).ToString("dd.MM.yyyy");
            this.lssircode.Text = ds1.Tables[2].Rows[0]["ssircode"].ToString();
            this.txtOrderNarr.Text = ds1.Tables[2].Rows[0]["pordnar"].ToString();            

            this.gvOrderInfo_DataBind();
        }
        protected void gvOrderInfo_DataBind()
        {
            try
            {
                string comcod = this.GetCompCode();
                DataTable tbl1 = this.HiddenSameData((DataTable)ViewState["tblOrder"]);
                this.gvOrderInfo.DataSource = tbl1;
                this.gvOrderInfo.DataBind();

                ((LinkButton)this.gvOrderInfo.FooterRow.FindControl("lbtnDelete")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry" || this.Request.QueryString["InputType"].ToString().Trim() == "OrderEdit" || this.Request.QueryString["InputType"].ToString().Trim() == "FirstApp" || this.Request.QueryString["InputType"].ToString().Trim() == "SecondApp");
                ((LinkButton)this.gvOrderInfo.FooterRow.FindControl("lbtnUpdatePurOrder")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry" || this.Request.QueryString["InputType"].ToString().Trim() == "OrderEdit" || this.Request.QueryString["InputType"].ToString().Trim() == "FirstApp" || this.Request.QueryString["InputType"].ToString().Trim() == "SecondApp");

                if (tbl1.Rows.Count == 0)
                    return;


                double amt1 = 0.00, amt2 = 0.00;
                DataTable td1 = tbl1.Copy();
                DataTable td2 = tbl1.Copy();
                DataView dv1;
                //Deduction
                dv1 = td2.DefaultView;
                dv1.RowFilter = ("rsircode like '019999902%'");
                td2 = dv1.ToTable();
                // Others
                dv1 = td1.DefaultView;
                dv1.RowFilter = ("rsircode not like '019999902%'");
                td1 = dv1.ToTable();
                amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(ordramt)", "")) ? 0.00 : td2.Compute("Sum(ordramt)", "")));
                amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(ordramt)", "")) ? 0.00 : td1.Compute("Sum(ordramt)", "")));
                ((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text = (amt1 - amt2).ToString("#,##0.00;(#,##0.00); ");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;
            }
          
        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "rsircode";
            dt1 = dv.ToTable();

            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                }

            }

            return dt1;
        }

        protected void Session_tblOrder_Update()
        {

            DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvOrderInfo.Rows.Count; j++)
            {


                string rsircode = ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvResCod")).Text.Trim();
                double dgvorderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderQty")).Text.Trim()));
                double dgvAppAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderAmt")).Text.Trim()));
                TblRowIndex2 = (this.gvOrderInfo.PageIndex) * this.gvOrderInfo.PageSize + j;

                //if (aprovsrate < aprovrate)
                //{
                //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                //    ((Label)this.Master.FindControl("lblmsg")).Text = "Supplier rate must be greater then Actual Rate";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;

                //}

                if (rsircode.Substring(0, 7) == "0199999")
                {


                    double dgvMRRRate = (dgvorderQty > 0) ? dgvAppAmt / dgvorderQty : 00;

                    tbl1.Rows[TblRowIndex2]["ordrqty"] = dgvorderQty;
                    tbl1.Rows[TblRowIndex2]["aprovrate"] = dgvMRRRate;
                    tbl1.Rows[TblRowIndex2]["ordramt"] = dgvAppAmt;
                }

                else
                {
                    //dispercnt = (aprovrate > 0) ? ((aprovsrate - aprovrate) * 100) / aprovsrate : dispercnt;
                    //aprovrate = (aprovrate > 0) ? aprovrate : (aprovsrate - aprovsrate * .01 * dispercnt);
                    dgvAppAmt = dgvorderQty * dgvAppAmt;
                    tbl1.Rows[TblRowIndex2]["ordrqty"] = dgvorderQty;
                    //tbl1.Rows[TblRowIndex2]["aprovsrate"] = aprovsrate;
                    //tbl1.Rows[TblRowIndex2]["dispercnt"] = dispercnt;
                    //tbl1.Rows[TblRowIndex2]["aprovrate"] = aprovrate;
                    tbl1.Rows[TblRowIndex2]["ordramt"] = dgvAppAmt;
                }

            }
            ViewState["tblOrder"] = tbl1;

        }



        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblOrder_Update();
            this.gvOrderInfo.PageIndex = ((DropDownList)this.gvOrderInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvOrderInfo_DataBind();
        }


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("fappid", Type.GetType("System.String"));
            tblt01.Columns.Add("fappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("fapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("fappseson", Type.GetType("System.String"));
            tblt01.Columns.Add("secappid", Type.GetType("System.String"));
            tblt01.Columns.Add("secappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("secapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("secappseson", Type.GetType("System.String"));
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
                case "OrderEntry":
                    switch (comcod)
                    {

                        case "3335": // Edison
                        case "3355": // Green Wood
                        case "3354":  // Edison Real Estate
                        case "1205":  //P2P Construction
                        case "3351":  //wecon Properties
                        case "3352":  //p2p360
                                      //case "3101": // ASIT

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
                                dr1["secappid"] = usrid;
                                dr1["secappdat"] = Date;
                                dr1["secapptrmid"] = trmnid;
                                dr1["secappseson"] = session;

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
                                ds1.Tables[0].Rows[0]["secappid"] = usrid;
                                ds1.Tables[0].Rows[0]["secappdat"] = Date;
                                ds1.Tables[0].Rows[0]["secapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["secappseson"] = session;

                                approval = ds1.GetXml();

                            }



                            break;

                    }

                    break;


                case "FirstApp":

                    switch (comcod)
                    {
                        //case "3101": // ptl
                        case "3355": // grenwood
                            string sappusridg = "";
                            string sapptrmnidg = "";
                            string sappsessiong = "";
                            string sappDateg = "";

                            List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> lst2 = (List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange>)Session["tblordrange"];

                            bool forardg = ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Checked ? true : false;
                            double toamtg = Convert.ToDouble(((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text.ToString());
                            string sslnumg = "";
                            foreach (RealEntity.C_14_Pro.EClassPur.EClassOrderRange lst1 in lst2)
                            {

                                string slnumg = lst1.slnum;
                                double minamtg = lst1.minamt;
                                double maxamtg = lst1.maxamt;
                                if (toamtg > minamtg && toamtg <= maxamtg)
                                {
                                    sslnumg = slnumg;
                                }

                            }
                            string fslnumg = lst2[0].slnum.ToString();
                            // First Approval
                            if (sslnumg == fslnumg)
                            {

                                if (forardg == true)
                                    ;
                                else
                                {

                                    sappusridg = hst["usrid"].ToString();
                                    sapptrmnidg = hst["compname"].ToString();
                                    sappsessiong = hst["session"].ToString();
                                    sappDateg = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                                }
                            }

                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["fappid"] = usrid;
                                dr1["fappdat"] = Date;
                                dr1["fapptrmid"] = trmnid;
                                dr1["fappseson"] = session;
                                dr1["secappid"] = "";
                                dr1["secappdat"] = "";
                                dr1["secapptrmid"] = "";
                                dr1["secappseson"] = "";

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
                                ds1.Tables[0].Rows[0]["secappid"] = "";
                                ds1.Tables[0].Rows[0]["secappdat"] = "";
                                ds1.Tables[0].Rows[0]["secapptrmid"] = "";
                                ds1.Tables[0].Rows[0]["secappseson"] = "";
                                approval = ds1.GetXml();

                            }
                            break;


                        case "3335":
                        case "3354":// Edison Real Estate
                            string sappusrid = "";
                            string sapptrmnid = "";
                            string sappsession = "";
                            string sappDate = "";
                            List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> lst = (List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange>)Session["tblordrange"];

                            bool forard = ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Checked ? true : false;

                            double toamt = Convert.ToDouble(((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text.ToString());
                            string sslnum = "";
                            foreach (RealEntity.C_14_Pro.EClassPur.EClassOrderRange lst1 in lst)
                            {

                                string slnum = lst1.slnum;
                                double minamt = lst1.minamt;
                                double maxamt = lst1.maxamt;

                                if (toamt > minamt && toamt <= maxamt)
                                {

                                    sslnum = slnum;

                                }

                            }


                            string fslnum = lst[0].slnum.ToString();

                            // First Approval
                            if (sslnum == fslnum)
                            {

                                if (forard == true)
                                    ;
                                else
                                {

                                    sappusrid = hst["usrid"].ToString();
                                    sapptrmnid = hst["compname"].ToString();
                                    sappsession = hst["session"].ToString();
                                    sappDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                                }
                            }





                            if (approval == "")
                            {




                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["fappid"] = usrid;
                                dr1["fappdat"] = Date;
                                dr1["fapptrmid"] = trmnid;
                                dr1["fappseson"] = session;
                                dr1["secappid"] = sappusrid;
                                dr1["secappdat"] = sappDate;
                                dr1["secapptrmid"] = sapptrmnid;
                                dr1["secappseson"] = sappsession;

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
                                ds1.Tables[0].Rows[0]["secappid"] = sappusrid;
                                ds1.Tables[0].Rows[0]["secappdat"] = sappDate;
                                ds1.Tables[0].Rows[0]["secapptrmid"] = sapptrmnid;
                                ds1.Tables[0].Rows[0]["secappseson"] = sappsession;
                                approval = ds1.GetXml();






                            }


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
                                dr1["secappid"] = usrid;
                                dr1["secappdat"] = Date;
                                dr1["secapptrmid"] = trmnid;
                                dr1["secappseson"] = session;

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
                                ds1.Tables[0].Rows[0]["secappid"] = usrid;
                                ds1.Tables[0].Rows[0]["secappdat"] = Date;
                                ds1.Tables[0].Rows[0]["secapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["secappseson"] = session;
                                approval = ds1.GetXml();
                            }


                            break;

                    }



                    break;




                case "SecondApp":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["secappid"] = usrid;
                    ds1.Tables[0].Rows[0]["secappdat"] = Date;
                    ds1.Tables[0].Rows[0]["secapptrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["secappseson"] = session;
                    approval = ds1.GetXml();

                    break;





            }


            return approval;

        }

        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string entrydate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETBDATEORDER", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }


        protected void lbtnUpdatePurOrder_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.Session_tblOrder_Update();

            string mORDERDAT = this.GetStdDate(this.txtCurOrderDate.Text.Trim());

            // Back date Entry  only Tropical
            if (comcod == "3339")
            {
                DateTime Bdate;
                Bdate = this.GetBackDate();
                bool dconi = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(mORDERDAT));
                string type1 = this.Request.QueryString["InputType"].ToString().Trim();

                if (type1 == "OrderEntry")
                {
                    if (!dconi)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Purchase Order Entry Only Current Date" + "');", true);
                        return;
                    }
                }
            }

            string mPORDUSRID = "";
            string mAPPRUSRID = "";
            string mSSIRCODE = this.ddlSuplierList.Items.Count > 0 ? this.ddlSuplierList.SelectedValue.ToString() : this.lssircode.Text.Trim();
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());
            string mPORDBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mPORDREF = this.txtOrderRefNo.Text.Trim();
            string mLETERDES = this.txtLETDES.Text.Trim();
            string mPORDNAR = this.txtOrderNarr.Text.Trim();
            string subject = this.txtSubject.Text.Trim();
            double AdvAmt = Convert.ToDouble("0" + this.txtadvAmt.Text.Trim());
            //log report

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();

            //end log
            bool result = false;
            //forward Programm
            //Balance Approval
            DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            foreach (DataRow drf in tbl1.Rows)
            {


                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(drf["aprovdat"].ToString()), Convert.ToDateTime(mORDERDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Order Date is equal or greater Approved Date" + "');", true);
                    return;
                }

            }


            if (this.ddlPrevOrderList.Items.Count == 0)
                this.GetOrderNo();

            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
           
            //Commented by Parbaz 14 March 2022

            //if ((this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry"))
            //{

            //    for (int i = 0; i < tbl1.Rows.Count; i++)
            //    {
            //        string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
            //        string mREQNO = tbl1.Rows[i]["reqno"].ToString();
            //        string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
            //        string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
            //        DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "EMPTYORDERNO", mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, "", "", "", "", "");
            //        if (ds1 == null)
            //            return;
            //        if (ds1.Tables[0].Rows.Count == 0)
            //            continue;
            //        if (ds1.Tables[0].Rows[0]["orderno"].ToString().Trim() != "")
            //        {

            //            DataView dv1 = ds1.Tables[0].DefaultView;
            //            dv1.RowFilter = ("orderno <>'" + mORDERNO + "'");
            //            DataTable dt = dv1.ToTable();
            //            if (dt.Rows.Count == 0)
            //                ;
            //            else
            //            {
            //                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Materials  already Orderred another order" + "');", true);
            //                return;
            //            }
            //        }
            //    }

            //}

            double netamt = Convert.ToDouble(((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text.ToString());  //(Label)gvOrderInfo.FindControl("lblgvFooterTOrderAmt");


            if (AdvAmt <= netamt)
            { }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Advanced Amount must be equal /less in Total Amount" + "');", true);
                return;
            }



            string issueno = this.lblissueno.Text.Trim();
            string appxml = tbl1.Rows[0]["approval"].ToString();
            string Approval = this.GetReqApproval(appxml);


            bool forwarddesc = ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Checked ? true : false;
            string type = this.Request.QueryString["InputType"];
            switch (type)
            {
                case "FirstApp":
                    tbl1.Rows[0]["forward"] = forwarddesc;
                    break;

                default:
                    break;



            }


            string terms = "";
            bool istxtTerms = true;

            string forward = (tbl1.Rows[0]["forward"].ToString().Trim().Length == 0) ? "False" : tbl1.Rows[0]["forward"].ToString();
            result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERB",
                             mORDERNO, mORDERDAT, mSSIRCODE, mPORDREF, mLETERDES, mPORDNAR, subject, userid, Terminal, Sessionid, Approval, forward,
                             "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();

                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["aprovdat"].ToString()), Convert.ToDateTime(mORDERDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Order Date is equal or greater Approved Date" + "');", true);
                    return;
                }



                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                double mAprovqty = Convert.ToDouble(tbl1.Rows[i]["aprovqty"]);
                double mORDRQTY = Convert.ToDouble(tbl1.Rows[i]["ordrqty"]);
                string dispercnt = Convert.ToDouble(tbl1.Rows[i]["dispercnt"]).ToString();


                // string mORDRQTY = tbl1.Rows[i]["ordrqty"].ToString();
                if (mAprovqty < mORDRQTY)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Order Qty Must be Less Or Equal  Approve Qty" + "');", true);
                    return;
                }

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURPROPOSAL", mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mORDERNO, mORDRQTY.ToString(), "", "", "", "", "", "", "", "");

                if (mREQNO != "")
                {
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERA",
                                                 mORDERNO, mREQNO, mRSIRCODE, mSPCFCOD, mORDRQTY.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "", "","");
                }                    

                //else
                //{
                //    string mPactcode = tbl1.Rows[i]["pactcode"].ToString();
                //    string mOrderAmt = Convert.ToDouble(tbl1.Rows[i]["ordramt"]).ToString();

                //    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERE", mORDERNO, mPactcode, mRSIRCODE, "000000000000", mOrderAmt, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                //}



                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
            }

            // todo for p2p terms and conditions in text box
            if (istxtTerms)
            {
                for (int j = 0; j < this.gvOrderTerms.Rows.Count; j++)
                {
                    string mTERMSID = ((Label)this.gvOrderTerms.Rows[j].FindControl("lblgvTermsID")).Text.Trim();
                    string mTERMSSUBJ = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvSubject")).Text.Trim();
                    string mTERMSDESC = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvDesc")).Text.Trim();
                    string mTERMSRMRK = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvRemarks")).Text.Trim();
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERC",
                            mORDERNO, mTERMSID, mTERMSSUBJ, mTERMSDESC, mTERMSRMRK, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }

                }
            }



            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mORDRQTY = tbl1.Rows[i]["ordrqty"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURAPPROVA", mAPROVNO, mREQNO, mRSIRCODE, SSIRCODE, mORDERNO, mSPCFCOD, "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
            }


            //for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            //{

            //    string inscode = ((Label)this.gvPayment.Rows[i].FindControl("lblgvschcode")).Text.Trim();
            //    string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
            //    string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
            //    string Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvPayment.Rows[i].FindControl("lblnetamt")).Text.Trim())).ToString();
            //    string ait = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvait")).Text.Trim())).ToString();
            //    string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
            //    string Remarks02 = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks02")).Text.Trim();




            //    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERD",
            //            mORDERNO, inscode, desc, Date, Amt, Remarks, Remarks02, ait, "", "", "", "", "", "", "", "", "", "", "", "");
            //    if (!result)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
            //        return;
            //    }

            //}

            DataTable dsty = (DataTable)ViewState["tblOrder"];
            this.txtCurOrderDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Purchase Order Updated successfully" + "');", true);
            ViewState["purtermcon"] = null;

            if (hst["compsms"].ToString() == "True")
            {
                switch (comcod)
                {
                    case "3333":
                        break;

                    default:


                        if ((this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry"))
                        {
                            SendSmsProcess sms = new SendSmsProcess();
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string frmname = "PurMRREntry.aspx?Type=Entry";
                            string SMSHead = "Ready To Recived, ";
                            string SMSText = comnam + ":\n" + SMSHead + "\n" + dsty.Rows[0]["projdesc1"].ToString() + "\n" + "MRF No:" + dsty.Rows[0]["mrfno"].ToString() + "\n" + "to Supplier: " +
                             dsty.Rows[0]["ssirdesc1"].ToString();
                            bool resultsms = sms.SendSmms(SMSText, userid, frmname);

                        }
                        break;
                }

            }




        }

        protected void lbtnSelectedOrdr_Click(object sender, EventArgs e)
        {

            this.Get_Pur_Order_Info();
            string comcod = this.GetCompCode();
            if (comcod == "3335")
            {
                this.ddltypecod.Visible = true;
                this.lnkselect.Visible = true;

            }
            switch (comcod)
            {
                case "3335":
                    this.ddltypecod.Visible = true;
                    this.lnkselect.Visible = true;
                    this.txtSubject.Text = "Purchase Order For Materials";
                    this.txtLETDES.Text = "Refer to your offer with specification dated on 15/02/2009 and subsequent discussion our management is pleased to issue work order for the following terms &amp; conditions";
                    break;

                case "3364":
                    this.txtSubject.Text = "Purchase Order For ";
                    this.txtLETDES.Text = "This is an reference to your discussion had with us today, we are pleased to place an order for supplying Rmc at our project under the following terms & conditions.";
                    break;

                case "3101":
                case "3357":
                    this.txtSubject.Text = "Purchase Order For ";
                    this.txtLETDES.Text = "Thank you very much for cooperating with Cube Holdings Ltd. Against your offer and further discussion we are offering you for the supply of ... under the following terms & condition and rate.";
                    break;

                default:
                    this.txtSubject.Text = "Purchase Order For Materials";
                    this.txtLETDES.Text = "Refer to your offer with specification dated on 15/02/2009 and subsequent discussion our management is pleased to issue work order for the following terms &amp; conditions";

                    break;

            }



            DataTable dt1 = (DataTable)ViewState["tblOrder"];
            DataTable dtResP = (DataTable)ViewState["tblResP"];
            int i;
            for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
            {
                bool chkitm = ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked;

                string PactCode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvPrjCod11")).Text.Trim();
                //string Appno = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvPAPNo")).Text.Trim();
                string Reqno = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvReqNo2")).Text.Trim();
                string Rsircode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvResCod2")).Text.Trim();
                string Spcfcod = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvspfcod02")).Text.Trim();
                string Ssircode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvSupCod")).Text.Trim();
                if (chkitm == true)
                {


                    DataRow[] dr2 = dtResP.Select("pactcode='" + PactCode + " 'and reqno = '" + Reqno + "' and rsircode = '" + Rsircode +
                                    "' and spcfcod='" + Spcfcod + "' and ssircode = '" + Ssircode + "'");
                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = "1";
                    }


                }

                else
                {

                    DataRow[] dr2 = dtResP.Select("pactcode='" + PactCode  + " 'and reqno = '" + Reqno + "' and rsircode = '" + Rsircode +
                                      "' and spcfcod='" + Spcfcod + "' and ssircode = '" + Ssircode + "'");
                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = "0";
                    }
                }

            }

            string Narration = "";
            string aprovno1 = "00000000000000";
            //string comcod = this.GetCompCode();
            for (i = 0; i < dtResP.Rows.Count; i++)
            {
                
                string chkitem = dtResP.Rows[i]["chk"].ToString();
                if (chkitem == "1")
                {
                    DataRow dr1 = dt1.NewRow();
                    dr1["reqno"] = dtResP.Rows[i]["reqno"];
                    dr1["rsircode"] = dtResP.Rows[i]["rsircode"];
                    dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                    dr1["spcfcod"] = dtResP.Rows[i]["spcfcod"];
                    dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                    dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                    dr1["pactcode"] = dtResP.Rows[i]["pactcode"];
                    dr1["projdesc1"] = dtResP.Rows[i]["projdesc1"];
                    dr1["rsirdesc1"] = dtResP.Rows[i]["rsirdesc1"];
                    dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                    dr1["spcfdesc"] = dtResP.Rows[i]["spcfdesc"];
                    dr1["rsirunit"] = dtResP.Rows[i]["rsirunit"];
                    dt1.Rows.Add(dr1);
                    Narration = Narration + dtResP.Rows[i]["reqnar"] + ", ";

                }
            }

            this.lblreqnaration.Text = "Req Naration : " + Narration.Substring(0, (Narration.Length) - 2);

            this.MultiView1.ActiveViewIndex = 1;
            this.hideTermsConditions();

            ViewState["tblOrder"] = this.HiddenSameData(dt1);
            this.gvOrderInfo_DataBind();

            this.ShowProjectFiles();

        }

        private void hideTermsConditions()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "1205":
                case "3351":
                case "3352":
                    this.divtermsp2p.Visible = true;
                    this.divterms.Visible = false;
                    //this.ImagePanel.Visible = false;
                    break;

                case "3101":
                case "3357":
                case "3366":
                    this.divtermsp2p.Visible = true;
                    this.divterms.Visible = false;
                    this.txtOrderNarrP.Text = this.bindDataText();
                    break;

                default:
                    this.divtermsp2p.Visible = false;
                    this.divterms.Visible = true;
                    //this.ImagePanel.Visible = true;
                    break;
            }
        }

        private string bindDataText()
        {
            string comcod = this.GetCompCode();
            string msg = "";
            switch (comcod)
            {
                case "3101":
                case "3357":
                    msg = "1. Product quality must be ensured on the basis of requirement and as per site count. " +
                        "\n2. Product should be newly produced, fresh and free from cracks and broken edges." +
                        "\n3. Product delivery time must be on time." +
                        "\n4. Payment shall be made by cash/A/C cheque after ………. Days of receipt of all materials in good conditions." +
                        "\n5. Delivery place: at project site " +
                        "\n6. Delivery date: ……………………" +
                        "\n7. Cube Holdings Ltd. has the right to cancel the work order in any time." +
                        "\n8. TDS will be applicable as per TAX ordinance compliance by 3%" +
                        "\n9. Please send all bill in duplicate.";
                    break;

                case "3366":
                    msg = "1. Delivery Place : " +
                        "\n2. Delivery Date : " +
                        "\n3. Contact Person : " +
                        "\n4. Cell Number : " +
                        "\n5. Bill of any supply order against purchase order shall be enclosed with the copy of purchase order and challan detected description of goods. Any discrepancy shall not be accepted." +
                        "\n6. Copy of delivery challan must be signed by proprietor of supplying designation with seal containing name of his organization. " +
                        "\n7. Supply must be completed within 24 hours of any purchase order otherwise the purchase order will be cancelled unless otherwise instructed." +
                        "\n8. Any payment to the supplies more than Tk. 10,000.00 (Taka Ten thousand) will be made through A/c payee cheque." +
                        "\n9. Payment shall have to be received from this office through money receipt of the company." +
                        "\n10. The supplier will be obliged to change the quantity if it is damaged, unspecified and if there is a mismatch in the model according to the purchase order inside the supplied product packet. If not in stock, will be obliged to return the money";
                    break;

                default:
                    msg = "";
                    break;
            }

            return msg;
        }

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvAprovInfo.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked = true;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked = false;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "False";

                }

            }
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable tbl1 = (DataTable)ViewState["tblOrder"];

            string comcod = this.GetCompCode();
            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            bool result;


            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERNO",
                           mORDERNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Deleted Failed!" + "');", true);
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UNLINKAPPROVENO",
                         mAPROVNO, mREQNO, mRSIRCODE, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Successfully Deleted" + "');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Order Info";
                string eventdesc = "Update Order";
                string eventdesc2 = mORDERNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            this.pnlschgenerate.Visible = false;
            DataTable dt = (DataTable)ViewState["tblpaysch"];
            int toins = Convert.ToInt32("0" + this.txtTInstall.Text.Trim());
            int incode = 0;
            for (int i = 0; i < toins; i++)
            {
                incode = incode + 1;
                string inscode = incode.ToString().PadLeft(3, '0');
                incode = Convert.ToInt32(inscode);
                DataRow dr = dt.NewRow();

                dr["inscode"] = inscode;
                dr["insdesc"] = "";
                dr["insdate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                dr["insamt"] = 0.00;
                dr["amt"] = 0.00;
                dr["ait"] = 0.00;
                dr["aitper"] = 0.00;
                dr["rmrks"] = "";
                dr["rmrks02"] = "";
                dt.Rows.Add(dr);
            }
            ViewState["tblpaysch"] = dt;

            this.chkVisible.Checked = false;
            //this.SchData_Bind();

        }


        //private void SchData_Bind()
        //{
        //    DataTable dt = (DataTable)ViewState["tblpaysch"];

        //    this.gvPayment.DataSource = dt;
        //    this.gvPayment.DataBind();

        //    if (dt.Rows.Count > 0)
        //    {

        //        ((Label)this.gvPayment.FooterRow.FindControl("lblgvfschAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(insamt)", "")) ? 0.00 : dt.Compute("sum(insamt)", ""))).ToString("#,##0;(#,##0); ");
        //        ((Label)this.gvPayment.FooterRow.FindControl("lblgvfait")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ait)", "")) ? 0.00 : dt.Compute("sum(ait)", ""))).ToString("#,##0;(#,##0); ");
        //        ((Label)this.gvPayment.FooterRow.FindControl("lblgvfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
        //    }


        //}
        private void SavePaymentSchdule()
        {

            DataTable dt = (DataTable)ViewState["tblpaysch"];
            //dt.Columns.Add ("aitper", typeof (double));
            //DataRow dr = dt.NewRow ();
            //dr["aitper"] = 0;
            //dt.Rows.Add (dr);


            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
                string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double ait = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvait")).Text.Trim()));
                double aitper = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvaitper")).Text.Trim()));

                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                double insamt = Amt - ait;
                ait = ait > 0 ? ait : (Amt * .01 * aitper);
                aitper = ait > 0 ? (ait * 100) / Amt : aitper;

                string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
                string Remarks02 = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks02")).Text.Trim();

                dt.Rows[i]["insdesc"] = desc;
                dt.Rows[i]["insdate"] = Date;
                dt.Rows[i]["insamt"] = insamt;
                dt.Rows[i]["amt"] = Amt;
                dt.Rows[i]["ait"] = ait;
                dt.Rows[i]["rmrks"] = Remarks;
                dt.Rows[i]["rmrks02"] = Remarks02;
                dt.Rows[i]["aitper"] = aitper;
                //dt.Rows[i]["aitpercen"] = aitper;
            }

            ViewState["tblpaysch"] = dt;

        }

        protected void lUpdatpayment_Click(object sender, EventArgs e)
        {
            this.SavePaymentSchdule();
            DataTable dt = (DataTable)ViewState["tblpaysch"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
                string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
                dt.Rows[i]["insdesc"] = desc;
                dt.Rows[i]["insdate"] = Date;
                dt.Rows[i]["insamt"] = Amt;
                dt.Rows[i]["rmrks"] = Remarks;
            }

            ViewState["tblpaysch"] = dt;
        }
        protected void lTotalPayment_Click(object sender, EventArgs e)
        {
            this.SavePaymentSchdule();
            //this.SchData_Bind();
        }
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlschgenerate.Visible = this.chkVisible.Checked;
        }
        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void chkCharging_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCharging.Visible = (chkCharging.Checked);
            this.imgSearchProject_Click(null, null);
            this.imgSearchCharge_Click(null, null);
        }
        protected void imgSearchCharge_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABOURCHARGE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlCharge.DataTextField = "sirdesc";
            this.ddlCharge.DataValueField = "sircode";
            this.ddlCharge.DataSource = ds1.Tables[0];
            this.ddlCharge.DataBind();
            ds1.Dispose();
        }
        private void TblProject()
        {
            if (ViewState["tblproject"] == null)
            {
                DataTable tblproject = new DataTable();
                tblproject.Columns.Add("pactcode", Type.GetType("System.String"));
                tblproject.Columns.Add("pactdesc", Type.GetType("System.String"));
                ViewState["tblproject"] = tblproject;
            }
        }
        protected void imgSearchProject_Click(object sender, EventArgs e)
        {


            this.TblProject();
            DataTable dt = (DataTable)(DataTable)ViewState["tblOrder"];
            DataTable dt1 = (DataTable)ViewState["tblproject"];
            if (dt.Rows.Count == 0)
            {
                this.ddlProjectName.Items.Clear();
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Pactcode = dt.Rows[i]["pactcode"].ToString();
                DataRow[] dr1 = dt1.Select("pactcode='" + Pactcode + "'");
                if (dr1.Length == 0)
                {
                    DataRow dr2 = dt1.NewRow();
                    dr2["pactcode"] = Pactcode;
                    dr2["pactdesc"] = dt.Rows[i]["projdesc1"].ToString();
                    dt1.Rows.Add(dr2);

                }

            }

            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = dt1;
            this.ddlProjectName.DataBind();
        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            DataTable tbl1 = (DataTable)ViewState["tblOrder"];

            string mProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string mResCode = this.ddlCharge.SelectedValue.ToString();
            string mSpcfCode = "000000000000";
            DataRow[] dr2 = tbl1.Select("pactcode ='" + mProjectCode + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();


                
                dr1["reqno"] = "";
                dr1["rsircode"] = mResCode;
                dr1["ssircode"] = "";
                dr1["spcfcod"] = mSpcfCode;              
                dr1["reqno1"] = "";
                dr1["mrfno"] = "";
                dr1["pactcode"] = mProjectCode;
                dr1["projdesc1"] = this.ddlProjectName.SelectedItem.ToString();
                dr1["rsirdesc1"] = this.ddlCharge.SelectedItem.ToString(); ;
                dr1["ssirdesc1"] = "";
                dr1["spcfdesc"] = "";
                dr1["rsirunit"] = "";
                dr1["ordrqty"] = 0.00;
                dr1["ordramt"] = 0.00;
                tbl1.Rows.Add(dr1);
            }

            ViewState["tblOrder"] = this.HiddenSameData(tbl1);
            this.gvOrderInfo_DataBind();
            // this.gvBillInfo_DataBind();
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblOrder_Update();
            this.gvOrderInfo_DataBind();

        }
      
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
          
        }
      
        protected void btnSendmail_Click(object sender, EventArgs e)
        {
            bool ssl = Convert.ToBoolean(((Hashtable)Session["tblLogin"])["ssl"].ToString());


            switch (ssl)
            {
                case true:
                    this.SendSSLMail();

                    break;

                case false:
                    this.SendNormalMail();
                    break;

            }



        }
        private void SendNormalMail()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Work Order";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());





            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(hostname, portnumber);
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            client.EnableSsl = false;
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            ///////////////////////

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(frmemail);



            msg.To.Add(new System.Net.Mail.MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));
            msg.Subject = subject;
            msg.IsBodyHtml = true;

            System.Net.Mail.Attachment attachment;

            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;

            attachment = new System.Net.Mail.Attachment(apppath);
            msg.Attachments.Add(attachment);



            msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>");
            try
            {
                client.Send(msg);

                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                //string savelocation = Server.MapPath("~") + "\\SupWorkOreder";
                //string[] filePaths = Directory.GetFiles(savelocation);
                //foreach (string filePath in filePaths)
                //    File.Delete(filePath);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void SendSSLMail()
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Work Order";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            string mailtousr = ds1.Tables[0].Rows[0]["mailid"].ToString();
            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf";


            EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");

            //Connection Details 
            SmtpServer oServer = new SmtpServer(hostname);
            oServer.User = frmemail;
            oServer.Password = psssword;
            oServer.Port = portnumber;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oMail.From = frmemail;
            oMail.To = mailtousr;
            oMail.Cc = frmemail;
            oMail.Subject = subject;


            oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>";
            oMail.AddAttachment(apppath);


            //System.Net.Mail.Attachment attachment;

            //attachment = new System.Net.Mail.Attachment(apppath);
            //oMail.AddAttachment(attachment);





            try
            {

                oSmtp.SendMail(oServer, oMail);
                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            }

        }
        protected void gvOrderInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.Session_tblOrder_Update();
            this.gvOrderInfo.PageIndex = e.NewPageIndex;
            this.gvOrderInfo_DataBind();

        }

        private void ShowProjectFiles()
        {
            ViewState.Remove("tblimages");
            string comcod = this.GetCompCode();
            string orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERFILES", orderno, "", "", "", "", "", "", "");

            ViewState["tblimages"] = ds1.Tables[0];
            ListViewEmpAll.DataSource = ds1.Tables[0];
            ListViewEmpAll.DataBind();

        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string orderno = ((Label)this.ListViewEmpAll.Items[j].FindControl("orderno")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    DataRow dr = dt.Rows[j];
                    dr.Delete();
                    DataSet ds1 = new DataSet("ds1");
                    ds1.Tables.Add(dt);
                    ds1.Tables[0].TableName = "tbl1";
                    bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERIMG", ds1, null, null, orderno, "", "", "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + filesname.Replace("~", ""));
                        this.lblMesg.Text = " Files Removed ";
                        this.ShowProjectFiles();
                    }
                }

            }

        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                System.Web.UI.WebControls.Image imgname = (System.Web.UI.WebControls.Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;
                }
            }
           

        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string orderno = "";
            if (AsyncFileUpload1.HasFile)
            {
                orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/purorder/") + orderno + random + extension);

                // Url = Server.MapPath("~/Upload/purorder/") + orderno + random + extension;
                Url = "~/Upload/purorder/" + orderno + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                dt.Rows.Add(comcod, orderno, Url);
            }

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERIMG", ds1, null, null, orderno, "", "", "", "", "");

            if (result == true)
            {
                this.lblMesg.Text = " Successfully Updated ";
                this.ShowProjectFiles();

            }
            else
            {
                string filePath = Server.MapPath("~/");
                System.IO.File.Delete(filePath + Url.Replace("~", ""));
            }


        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {

            DataTable dt = ((DataTable)ViewState["purtermcon"]).Copy();
            string typecod = this.ddltypecod.SelectedValue;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "typecod='" + typecod + "'";

            this.gvOrderTerms.DataSource = dv.ToTable();
            this.gvOrderTerms.DataBind();
        }
        protected void lnkAddTerms_Click(object sender, EventArgs e)
        {
            this.bindTermsintoGrid();

            DataTable dt = ((DataTable)ViewState["purtermcon"]).Copy();
           
            int count = dt.Rows.Count - 1;
            int termsid = Convert.ToInt32(dt.Rows[count]["termsid"].ToString());
            termsid++;
            string stermsid = ASTUtility.Right("000" + termsid.ToString(), 3);

            //termsid, termssubj,   termsdesc, termsrmrk,termsdesc1


            DataRow dr1 = dt.NewRow();
            dr1["termsid"] = stermsid;
            dr1["termssubj"] = "";
            dr1["termsdesc"] = "";
            dr1["termsrmrk"] = "";
            dr1["termsdesc"] = "";
            dt.Rows.Add(dr1);
            ViewState["purtermcon"] = dt;
            this.gvOrderTerms.DataSource = (DataTable)ViewState["purtermcon"];
            this.gvOrderTerms.DataBind();

        }


        private void bindTermsintoGrid()
        {
            DataTable dt = (DataTable)ViewState["purtermcon"];

            for (int j = 0; j < this.gvOrderTerms.Rows.Count; j++)
            {
                string mTERMSID = ((Label)this.gvOrderTerms.Rows[j].FindControl("lblgvTermsID")).Text.Trim();
                string mTERMSSUBJ = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvSubject")).Text.Trim();
                string mTERMSDESC = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvDesc")).Text.Trim();
                string mTERMSRMRK = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvRemarks")).Text.Trim();

                dt.Rows[j]["termsid"] = mTERMSID;
                dt.Rows[j]["termssubj"] = mTERMSSUBJ;
                dt.Rows[j]["termsdesc"] = mTERMSDESC;
                dt.Rows[j]["termsrmrk"] = mTERMSRMRK;
                dt.AcceptChanges();
            }
            ViewState["purtermcon"] = dt;
        }


        protected void lbtndelterm_Click(object sender, EventArgs e)
        {

            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string termsid = ((DataTable)ViewState["purtermcon"]).Rows[index]["termsid"].ToString();
            string comcod = this.GetCompCode();
            string mOrderNo = "NEWORDER";
            if (this.ddlPrevOrderList.Items.Count > 0)
            {

                mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();
            }

            if (mOrderNo != "NEWORDER")
            {
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETETERMSANDCONDITIONS",
                  "", mOrderNo, termsid, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Deleted Failed" + "');", true);
                    return;
                }
                else
                {

                    ((DataTable)ViewState["purtermcon"]).Rows[index].Delete();
                    ((DataTable)ViewState["purtermcon"]).AcceptChanges();
                    gvOrderTerms.DataSource = (DataTable)ViewState["purtermcon"];
                    gvOrderTerms.DataBind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Deleted Successfully" + "');", true);

                }
            }

            else
            {

                ((DataTable)ViewState["purtermcon"]).Rows[index].Delete();
                ((DataTable)ViewState["purtermcon"]).AcceptChanges();
                gvOrderTerms.DataSource = (DataTable)ViewState["purtermcon"];
                gvOrderTerms.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Deleted Successfully" + "');", true);


            }

        }


    }
}