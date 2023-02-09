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
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                this.txtCurOrderDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
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

            string orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            string currentptah = "PurchasePrint.aspx?Type=MktOrderPrint&orderno=" + orderno;
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

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
                this.pnlSupplier.Visible = false;
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
                this.txtOrderNarr.Text = "";
                this.lblissueno.Text = "";
                this.gvOrderInfo.DataSource = null;
                this.gvOrderInfo.DataBind();
                this.gvOrderTerms.DataSource = null;
                this.gvOrderTerms.DataBind();
                this.lbtnOk.Text = "Ok";
                this.ddlSuplierList.Items.Clear();
                return;
            }
            this.lbtnPrevOrderList.Visible = false;
            this.ddlPrevOrderList.Visible = false;
            this.txtCurOrderNo2.ReadOnly = true;
            this.lbtnOk.Text = "New";

            if (this.ddlPrevOrderList.Items.Count <= 0)
            {
                this.MultiView1.ActiveViewIndex = 0;
                this.pnlSupplier.Visible= true;
                this.ResourceForOrder();
                return;
            }

            
            this.MultiView1.ActiveViewIndex = 1;
            this.Get_Pur_Order_Info();
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

            this.ddlSuplierList.DataTextField = "ssirdesc1";
            this.ddlSuplierList.DataValueField = "ssircode";
            this.ddlSuplierList.DataSource = ds1.Tables[1];
            this.ddlSuplierList.DataBind();
            ViewState["tblResP"] = ds1.Tables[0];
            ViewState["tblProject"] = ds1.Tables[1];           
            this.ddlSuplierList_SelectedIndexChanged(null, null);
           
        }

       
        protected void imgSearchOrderno_Click(object sender, EventArgs e)
        {
            this.ResourceForOrder();
        }

        protected void ddlSuplierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblResP"];
            string supcode = this.ddlSuplierList.SelectedValue.ToString();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "ssircode in ('" + supcode + "')";
            this.gvAprovInfo.DataSource = dv1.ToTable();
            this.gvAprovInfo.DataBind();


        }


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
            this.txtadvAmt.Text= Convert.ToDouble(ds1.Tables[2].Rows[0]["advamt"]).ToString("#,##0;(#,##0); ");

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

                ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "FirstApp");

                if (tbl1.Rows.Count == 0)
                    return;

                double amt1 = 0.00;
                amt1 = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(ordramt)", "")) ? 0.00 : tbl1.Compute("Sum(ordramt)", "")));
                ((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text = (amt1).ToString("#,##0.00;(#,##0.00); ");

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

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string ssircode = dt1.Rows[0]["ssircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["ssircode"].ToString()==ssircode)
                {
                    dt1.Rows[j]["projdesc1"] = "";
                    dt1.Rows[j]["ssirdesc1"] = "";

                }

                else if (dt1.Rows[j]["pactcode"].ToString() == pactcode )
                {
                    dt1.Rows[j]["projdesc1"] = "";
                }
                else if (dt1.Rows[j]["ssircode"].ToString()==ssircode)
                {
                    dt1.Rows[j]["ssirdesc1"] = "";
                }

                pactcode = dt1.Rows[j]["pactcode"].ToString();
                ssircode = dt1.Rows[j]["ssircode"].ToString();

            }

            return dt1;
        }

        protected void Session_tblOrder_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            for (int j = 0; j < this.gvOrderInfo.Rows.Count; j++)
            {
                string acttypeCode = ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvActTypeCode")).Text.Trim();

                double dgvorderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderQty")).Text.Trim()));
                double dgvOrderRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvOrderRate")).Text.Trim()));
                double dgvAppAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderAmt")).Text.Trim()));
                string rsirDetDesc = ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvRsirdetDesc1")).Text.Trim();
              
                if(acttypeCode.Substring(0, 7) == "0199999")
                {
                    tbl1.Rows[j]["ordrqty"] = dgvorderQty;
                    tbl1.Rows[j]["ordramt"] = dgvAppAmt;
                }
                else
                {
                    dgvAppAmt = dgvorderQty * dgvOrderRate;
                    tbl1.Rows[j]["ordrqty"] = dgvorderQty;
                    tbl1.Rows[j]["ordramt"] = dgvAppAmt;
                    tbl1.Rows[j]["rsirdetdesc"] = rsirDetDesc;
                }           

            }
            ViewState["tblOrder"] = tbl1;

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

                        case "3354":  // Edison Real Estate   
                        case "3101":  //PTL 
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

                        case "3101":  //PTL                    
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

           

         
            string mSSIRCODE = this.ddlSuplierList.Items.Count > 0 ? this.ddlSuplierList.SelectedValue.ToString() : this.lssircode.Text.Trim();           
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
            string advamt = ASTUtility.StrPosOrNagative(this.txtadvAmt.Text.Trim()).ToString();

            string forward = (tbl1.Rows[0]["forward"].ToString().Trim().Length == 0) ? "False" : tbl1.Rows[0]["forward"].ToString();
            result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERB",
                             mORDERNO, mORDERDAT, mSSIRCODE, mPORDREF, mLETERDES, mPORDNAR, subject, userid, Terminal, Sessionid, Approval, forward,
                             advamt, "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["aprovdat"].ToString()), Convert.ToDateTime(mORDERDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Order Date is equal or greater Approved Date" + "');", true);
                    return;
                }



                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string prtype = tbl1.Rows[i]["prtype"].ToString();
                string acttype = tbl1.Rows[i]["acttype"].ToString();
                string mkttype = tbl1.Rows[i]["mkttype"].ToString();
                double mAprovqty = Convert.ToDouble(tbl1.Rows[i]["aprvqty"]);
                double mORDRQTY = Convert.ToDouble(tbl1.Rows[i]["ordrqty"]);

                if (mAprovqty < mORDRQTY)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Order Qty Must be Less Or Equal  Approve Qty" + "');", true);
                    return;
                }

                //result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURPROPOSAL", mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mORDERNO, mORDRQTY.ToString(), "", "", "", "", "", "", "", "");

                if (mREQNO != "")
                {
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERA",
                                                 mORDERNO, mREQNO, mORDRQTY.ToString(), prtype, acttype, mkttype, "", "", "", "", "", "", "", "", "", "", "","","","");
                }

                else
                {
                    string mPactcode = tbl1.Rows[i]["pactcode"].ToString();
                    string mOrderAmt = Convert.ToDouble(tbl1.Rows[i]["ordramt"]).ToString();
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_PUR_ORDER_INFO", "MKTORDERE", mORDERNO, mPactcode, acttype, "000000000000", mOrderAmt, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }

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
                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string acttpe= tbl1.Rows[i]["acttype"].ToString();
                string rSirDetDesc = tbl1.Rows[i]["rsirdetdesc"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "UPDATE_MKT_REQ", mREQNO, acttpe, mORDERNO, rSirDetDesc, "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
            }


            DataTable dsty = (DataTable)ViewState["tblOrder"];
            this.txtCurOrderDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Purchase Order Updated successfully" + "');", true);
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
            try
            {
                this.Get_Pur_Order_Info();

                this.txtSubject.Text = "Purchase Order For Materials";
                this.txtLETDES.Text = "Refer to your offer with specification dated on 15/02/2009 and subsequent discussion our management is pleased to issue work order for the following terms & conditions";

                DataTable dt1 = (DataTable)ViewState["tblOrder"];
                DataTable dtResP = (DataTable)ViewState["tblResP"];
                int i;
                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    bool chkitm = ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked;

                    string PactCode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvPrjCod11")).Text.Trim();
                    string prTypeCode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvPrTypeCode")).Text.Trim();
                    string actTypeCode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvActTypeCode")).Text.Trim();
                    string mktTypeCode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvMktTypeCode")).Text.Trim();
                    string Reqno = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvReqNo2")).Text.Trim();
                    string Ssircode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvSupCod")).Text.Trim();
                    if (chkitm == true)
                    {

                        DataRow[] dr2 = dtResP.Select("pactcode='" + PactCode + " 'and reqno = '" + Reqno + "' and ssircode = '" + Ssircode +
                                                      " 'and prtype = '" + prTypeCode + " 'and acttype = '" + actTypeCode + "' and mkttype = '" + mktTypeCode + "'");
                        if (dr2.Length > 0)
                        {
                            dr2[0]["chk"] = "1";
                        }


                    }

                    else
                    {

                        DataRow[] dr2 = dtResP.Select("pactcode='" + PactCode  + " 'and reqno = '" + Reqno + "' and ssircode = '" + Ssircode +
                                                      " 'and prtype = '" + prTypeCode + " 'and acttype = '" + actTypeCode + "' and mkttype = '" + mktTypeCode + "'");
                        if (dr2.Length > 0)
                        {
                            dr2[0]["chk"] = "0";
                        }
                    }

                }

                string Narration = "";
                for (i = 0; i < dtResP.Rows.Count; i++)
                {

                    string chkitem = dtResP.Rows[i]["chk"].ToString();
                    if (chkitem == "1")
                    {
                        DataRow dr1 = dt1.NewRow();
                        dr1["reqno"] = dtResP.Rows[i]["reqno"];
                        dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                        dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                        dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                        dr1["pactcode"] = dtResP.Rows[i]["pactcode"];
                        dr1["projdesc1"] = dtResP.Rows[i]["projdesc1"];
                        dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                        dr1["prtype"] = dtResP.Rows[i]["prtype"];
                        dr1["acttype"] = dtResP.Rows[i]["acttype"];
                        dr1["mkttype"] = dtResP.Rows[i]["mkttype"];
                        dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                        dr1["prtypedesc"] = dtResP.Rows[i]["prtypedesc"];
                        dr1["acttypedesc"] = dtResP.Rows[i]["acttypedesc"];
                        dr1["mkttypedesc"] = dtResP.Rows[i]["mkttypedesc"];
                        dr1["reqrat"] = dtResP.Rows[i]["reqrat"];
                        dr1["aprvqty"] = dtResP.Rows[i]["aprvqty"];
                        dr1["ordrqty"] = dtResP.Rows[i]["aprvqty"];
                        dr1["aprovdat"] = dtResP.Rows[i]["aprovdat"];
                        dr1["ordramt"] = dtResP.Rows[i]["orderamt"];
                        dr1["rsirdetdesc"] = dtResP.Rows[i]["rsirdetdesc"];
                        dt1.Rows.Add(dr1);
                        Narration = Narration + dtResP.Rows[i]["reqnar"] + ", ";

                    }
                }

                this.lblreqnaration.Text = "Req Naration : " + Narration.Substring(0, (Narration.Length) - 2);
                this.MultiView1.ActiveViewIndex = 1;
                ViewState["tblOrder"] = this.HiddenSameData(dt1);
                this.gvOrderInfo_DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;
            }           

        }

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvAprovInfo.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked = true;
                }

            }

            else
            {
                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked = false;

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
            string chargeCode = this.ddlCharge.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("pactcode ='" + mProjectCode + "' and prtype = '" + chargeCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                
                dr1["reqno"] = "";
                dr1["prtype"] = "";
                dr1["acttype"] = chargeCode;
                dr1["mkttype"] = "";
                dr1["ssircode"] = "";           
                dr1["reqno1"] = "";
                dr1["mrfno"] = "";
                dr1["pactcode"] = mProjectCode;
                dr1["projdesc1"] = this.ddlProjectName.SelectedItem.ToString();
                dr1["prtypedesc"] = this.ddlCharge.SelectedItem.ToString();
                dr1["acttypedesc"] = "";
                dr1["mkttypedesc"] = "";
                dr1["ssirdesc1"] = "";
                dr1["reqrat"] = 0.00;
                dr1["aprvqty"] = 0.00;
                dr1["ordrqty"] = 0.00;
                dr1["ordramt"] = 0.00;
                dr1["aprovdat"] = "01-Jan-1900";
                tbl1.Rows.Add(dr1);
            }

            ViewState["tblOrder"] = this.HiddenSameData(tbl1);
            this.gvOrderInfo_DataBind();
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
            int termsid = 0, count = 0;
            if (dt.Rows.Count==0)
            {
                termsid = 0;
            }
            else
            {
                count = dt.Rows.Count - 1;
                termsid = Convert.ToInt32(dt.Rows[count]["termsid"].ToString());
            }
           
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
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "DELETE_TERMS_CONDITIONS",
                  "", mOrderNo, termsid, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Terms & Condition Deleted Failed!" + "');", true);
                    return;
                }
                else
                {

                    ((DataTable)ViewState["purtermcon"]).Rows[index].Delete();
                    ((DataTable)ViewState["purtermcon"]).AcceptChanges();
                    //Terms & Conditin ID Serial
                    DataTable dt = this.GetTermsConIDSerial((DataTable)ViewState["purtermcon"]);
                    gvOrderTerms.DataSource = dt;
                    gvOrderTerms.DataBind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Terms & Condition Deleted Successfully" + "');", true);

                }
            }

            else
            {

                ((DataTable)ViewState["purtermcon"]).Rows[index].Delete();
                ((DataTable)ViewState["purtermcon"]).AcceptChanges();
                //Terms & Conditin ID Serial
                DataTable dt = this.GetTermsConIDSerial((DataTable)ViewState["purtermcon"]);
                gvOrderTerms.DataSource = dt;
                gvOrderTerms.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Terms & Condition Deleted Successfully" + "');", true);

            }

        }

        private DataTable GetTermsConIDSerial(DataTable dt)
        {
            double j = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                j++;
                dt.Rows[i]["termsid"] = ASTUtility.Right("000" + j, 3); 
                dt.AcceptChanges();
            }
            return dt;
        }

        protected void gvOrderInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            for (int j = 0; j < this.gvOrderInfo.Rows.Count; j++)
            {
                string acttypeCode = ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvActTypeCode")).Text.Trim();

                if (acttypeCode.Substring(0, 7) == "0199999")
                {
                    ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvRsirdetDesc1")).Visible=false;
                }
                else
                {
                    ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvRsirdetDesc1")).Visible=true;
                }

            }
        }
    }
}