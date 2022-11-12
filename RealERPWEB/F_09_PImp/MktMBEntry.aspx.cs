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

namespace RealERPWEB.F_09_PImp
{
    public partial class MktMBEntry : System.Web.UI.Page
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
                this.txtCurOrderDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurOrderDate_CalendarExtender.EndDate = System.DateTime.Today;
               
                string ordero = this.Request.QueryString["genno"]??"";                
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
            string qorderno = this.Request.QueryString["genno"] ?? "";
            string orderno = (qorderno.Length == 0 ? "" : this.Request.QueryString["genno"].ToString()) + "%";


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

                this.lblpreviousmb.Visible = true ;
                this.lbtnPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Items.Clear();
                this.lblCurOrderNo1.Text = "POR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurOrderDate.Enabled = true;               
                this.txtOrderRefNo.Text = "";
                this.txtOrderRefNo.ReadOnly = false;
                this.lbtnOk.Text = "Ok";
                return;
            }
            this.lblpreviousmb.Visible = false;
            this.lbtnPrevOrderList.Visible = false;
            this.ddlPrevOrderList.Visible = false;
            this.txtCurOrderNo2.ReadOnly = true;
            this.lbtnOk.Text = "New";          
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



       
       




        protected void Get_Pur_Order_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mOrderNo = "NEWORDER";
           
            if (this.ddlPrevOrderList.Items.Count > 0)
            {
                // this.ddlSuplierList.Items.Clear();
                this.txtCurOrderDate.Enabled = false;              

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
           

            this.txtCurOrderDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["orderdat"]).ToString("dd.MM.yyyy");
            

            this.gvOrderInfo_DataBind();
        }
        protected void gvOrderInfo_DataBind()
        {
            try
            {
                string comcod = this.GetCompCode();
                DataTable tbl1 = this.HiddenSameData((DataTable)ViewState["tblOrder"]);
                //this.gvOrderInfo.DataSource = tbl1;
                //this.gvOrderInfo.DataBind();

             
                //if (tbl1.Rows.Count == 0)
                //    return;

                //double amt1 = 0.00;
                //amt1 = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(ordramt)", "")) ? 0.00 : tbl1.Compute("Sum(ordramt)", "")));
                //((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text = (amt1).ToString("#,##0.00;(#,##0.00); ");

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


    

       

        protected void lbtnUpdatePurOrder_Click(object sender, EventArgs e)
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

       
    }
}