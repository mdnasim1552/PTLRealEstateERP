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
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.txtCurOrderDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurOrderDate_CalendarExtender.EndDate = System.DateTime.Today;


                this.GetProjectList();
                this.GetContractorList();
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



        private void GetProjectList()
        {

            string comcod = this.GetCompCode();
            string qprjcode = this.Request.QueryString["prjcode"] ?? "";
            string srchproject = (qprjcode.Length>0 ? qprjcode : "")+ "%" ;
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



        protected void lbtnPrevOrderList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();            
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string qorderno = this.Request.QueryString["genno"] ?? "";
            string orderno = (qorderno.Length == 0 ? "" : this.Request.QueryString["genno"].ToString()) + "%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_PREV_ORDER_LIST", CurDate1,
                          orderno, "", usrid, "", "", "", "", "");
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
                    this.lblCurOrderNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtCurOrderNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
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
            this.GetCorderListInfo();
            if (mMBNo == "NEWMB")
            {

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTMBNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurOrderNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtCurOrderNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                }

                
                return;

            }

            this.lblCurOrderNo1.Text = ds1.Tables[2].Rows[0]["orderno1"].ToString().Substring(0, 6);
            this.txtCurOrderNo2.Text = ds1.Tables[2].Rows[0]["orderno1"].ToString().Substring(6, 5);
            this.txtOrderRefNo.Text = ds1.Tables[2].Rows[0]["pordref"].ToString();
           

            this.txtCurOrderDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["orderdat"]).ToString("dd.MM.yyyy");
            

            this.gvOrderInfo_DataBind();
        }

        private void GetCorderListInfo()
        {


            ViewState.Remove("tblcorder");
            string comcod = this.GetCompCode();
            string orderno = this.Request.QueryString["genno"] ?? "";
           DataSet  ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONORDERINFO", orderno, "",
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
                DataTable tbl1 =(DataTable)ViewState["tblcorder"];
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
                string rsircode= ((DataTable)ViewState["tblcorder"]).Rows[index]["rsircode"].ToString();
                string rsirdesc= ((DataTable)ViewState["tblcorder"]).Rows[index]["rsirdesc"].ToString();
                string rsirunit= ((DataTable)ViewState["tblcorder"]).Rows[index]["rsirunit"].ToString();
                string flrcod= ((DataTable)ViewState["tblcorder"]).Rows[index]["flrcod"].ToString();
                string flrdes= ((DataTable)ViewState["tblcorder"]).Rows[index]["flrdes"].ToString();
                DataTable dt= (DataTable)ViewState["tblmb"];
                DataRow[] dr1 = dt.Select("rsircode='" + rsircode + "' and flrcod='" + flrcod + "'");

                if (dr1.Length == 0)
                {
                    DataRow dradd = dt.NewRow();
                    dradd["rsircode"] = rsircode;
                    dradd["rsirdesc"] = rsirdesc;
                    dradd["rsirunit"] = rsirunit;
                    dradd["flrcod"] = flrcod;
                    dradd["flrdes"] = flrdes;
                    dradd["sl"] = 1;
                    dradd["nos"] = 0.00;
                    dradd["lnght"] = 0.00;
                    dradd["breadth"] = 0.00;
                    dradd["height"] = 0.00;
                    dradd["uweight"] = 0.00;
                    dradd["tweight"] = 0.00;
                    dt.Rows.Add(dradd);


                }


                ViewState["tblmb"] = dt;
                this.Data_Bind();




                //string sircode = ((DataTable)Session["storedata"]).Rows[index]["sircode"].ToString();
                //string actcode = ((DataTable)Session["storedata"]).Rows[index]["actcode"].ToString();
                //string mapCode = ((DataTable)Session["storedata"]).Rows[index]["mapcode"].ToString();
                //this.lblsircode.Text = sircode;
                //this.txtresourcecode.Text = sircode.Substring(0, 2) + "-" + sircode.Substring(2, 2) + "-" + sircode.Substring(4, 3) + "-" + sircode.Substring(7, 2) + "-" + ASTUtility.Right(sircode, 3);

                //this.Chboxchild.Checked = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");
                //this.chkbod.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");
                //this.lblchild.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");




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
                this.gvdetails.DataSource = (DataTable)ViewState["tblmb"];
                this.gvdetails.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }




        }

        protected void lbtnUpdatembinfo_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnTotal_Click(object sender, EventArgs e)
        {

        }
    }
}