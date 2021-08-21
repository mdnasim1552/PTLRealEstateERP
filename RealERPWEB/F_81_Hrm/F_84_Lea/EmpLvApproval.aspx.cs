using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Collections.Generic;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using EASendMail;

namespace RealERPWEB.F_81_Hrm.F_84_Lea
{

    public partial class EmpLvApproval : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess HRData = new ProcessAccess();
        public static int PageNumber = 0;
        // SmsSend SmsApps = new SmsSend();
        List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval> lstsalorder = new List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval>();

        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Leave Approval";
                string Type = this.Request.QueryString["Type"].ToString();
                string Date = (Type == "Ind") ? this.Request.QueryString["Date"].ToString() : System.DateTime.Today.ToString("dd-MMM-yyyy"); ;
                this.txtdate.Text = Date;
                // this.CommonButton();
                this.GetProjectName();
                this.GetOrderName();
                //this.PanelHead.Visible = true;
                this.PnlNarration.Visible = true;
                this.ShowData();

            }

        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approved";
            ((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;




            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler();
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnUpdate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).Checked += new EventHandler(chkBoxN_Click);

        }
        private void UserPermission()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Userid = hst["usrid"].ToString();
            string Store = this.ddlCenter.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "GETUSERINF", Userid, Store, "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
            {

                Response.Redirect("../AcceessError.aspx");
                return;
            }
        }

        private void Refrsh()
        {

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void ImgbtnFindProjectName_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Type = (this.Request.QueryString["Type"]).ToString();
            string srchproject = (Type == "Ind") ? this.Request.QueryString["refno"].ToString() : ("%" + this.txtProjectSearch.Text.Trim() + "%");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "GETDEPTNAME", srchproject, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlCenter.DataTextField = "deptanme";
            this.ddlCenter.DataValueField = "deptid";
            this.ddlCenter.DataSource = ds2.Tables[0];
            this.ddlCenter.DataBind();
            if (Request.QueryString.AllKeys.Contains("refno"))
            {
                this.ddlCenter.SelectedValue = this.Request.QueryString["refno"].ToString();
            }

            if (Type != "Ind")
                this.GetOrderName();

        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {

            this.ShowData();

        }

        private void GetOrderName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Type = (this.Request.QueryString["Type"]).ToString();
            string comcod = this.GetCompCode();
            string Date = (Type == "Ind") ? this.Request.QueryString["Date"].ToString() : Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string srchproject = (Type == "Ind") ? this.Request.QueryString["ltrnid"].ToString() : "%%"; //+ this.txtserchmrf.Text.Trim() 
            string pactcode = this.ddlCenter.SelectedValue.ToString();
            string Usrid = hst["usrid"].ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "GETLVAPPAUT", Date, srchproject, pactcode, Usrid, "", "", "", "", "");
            if (ds2 == null)
                return;

            this.lstOrderNo.DataTextField = "ltrnid2";
            this.lstOrderNo.DataValueField = "ltrnid";
            this.lstOrderNo.DataSource = ds2.Tables[0];
            this.lstOrderNo.DataBind();
            this.lstOrderNo.Focus();
            if (this.lstOrderNo.Items.Count > 0)
                this.lstOrderNo.SelectedIndex = 0;

            this.chkbod.Visible = ds2.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(ds2.Tables[0].Rows[0]["tcount"]);
            this.lblforward.Visible = ds2.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(ds2.Tables[0].Rows[0]["tcount"]);


        }


        private void ShowData()
        {

            //this.lblmsg.Text = " "; 100000000088
            try
            {
                Session.Remove("tblOrder");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string SrchChequeno = "%" + this.txtserchmrf.Text.Trim() + "%";


                string DeptCode = ((this.ddlCenter.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCenter.SelectedValue.ToString()) + "%";
                //string Approval = this.RateorApproved();
                string Userid = hst["usrid"].ToString();
                string Lvidno = this.lstOrderNo.SelectedValue.ToString();
                DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "SHOWLVSTATUS", Date, SrchChequeno, "", "", DeptCode, Userid, Lvidno, "", "");
                if (ds == null)
                {
                    this.gvLvReq.DataSource = null;
                    this.gvLvReq.DataBind();
                    return;
                }

                var lst = ds.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval>();
                ViewState["tblt01"] = lst;
                this.Data_Bind();

                this.ShowEmppLeave(ds.Tables[0].Rows[0]["empid"].ToString());

                this.lblvalNarration.Text = ds.Tables[0].Rows[0]["LREASON"].ToString();
                this.lblRemarks.Text = ds.Tables[0].Rows[0]["LRMARKS"].ToString();
                this.Chboxforward.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["forward"]);

                if (ds.Tables[1].Rows.Count == 0)
                    return;

                this.lblRemarks.Text = ds.Tables[1].Rows[0]["usrname"].ToString();




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        private string ShowEmppLeave(string Empid)
        {
            this.lblleaveStatus.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string aplydat = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", Empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return "";
            }


            this.gvLeaveStatus.DataSource = ds1.Tables[0];
            this.gvLeaveStatus.DataBind();

            return "";
        }
        protected void lstVouname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowData();


        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = ((this.ddlCenter.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCenter.SelectedValue.ToString());

            string Orderno = this.lstOrderNo.SelectedValue.ToString();
            if (Orderno.Length == 0)
            {
                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Please select your item for Delete";


                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }






            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERAPPDELETE", pactcode, Orderno, "", "", "");

            if (result == true)
            {

                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Successfully Deleted";
                ((Label)this.Master.FindControl("lblANMgsBox")).Attributes["style"] = "background:Green;";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                this.ShowData();
            }
            //this.Data_Bind();
        }
        private void Data_Bind()
        {

            var lst = (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval>)ViewState["tblt01"];

            this.gvLvReq.DataSource = lst;
            this.gvLvReq.DataBind();


            //for (int i = 0; i < this.RepInv.Items.Count; i++)
            //{
            //    string SubCode = ((Label)this.RepInv.Items[i].FindControl("lblSubCode")).Text.Trim();


            //    LinkButton lbtn1 = (LinkButton)RepInv.Items[i].FindControl("btnDel");
            //    if (lbtn1 != null)
            //        if (lbtn1.Text.Trim().Length > 0)
            //            lbtn1.CommandArgument = SubCode;

            //}




        }



        //private DataTable HiddenSameDate(DataTable dt1)
        //{

        //    if (dt1.Rows.Count == 0)
        //        return dt1;

        //    if (dt1.Rows.Count == 0)
        //        return dt1;

        //    string centrid = dt1.Rows[0]["centrid"].ToString();
        //    string orderno = dt1.Rows[0]["orderno"].ToString();
        //    string custcode = dt1.Rows[0]["custcode"].ToString();

        //    int j;



        //    for (j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[0]["centrid"].ToString() == centrid && dt1.Rows[j]["orderno"].ToString() == orderno && dt1.Rows[j]["custcode"].ToString() == custcode)
        //        {

        //            dt1.Rows[j]["orderno1"] = "";
        //            dt1.Rows[j]["refno"] = "";
        //            dt1.Rows[j]["orderdat"] = "";
        //            dt1.Rows[j]["teamdesc"] = "";

        //            dt1.Rows[j]["centrdesc"] = "";
        //            dt1.Rows[j]["custdesc"] = "";
        //            dt1.Rows[j]["limit"] = 0.00;
        //            dt1.Rows[j]["dues"] = 0.00;
        //            //dt1.Rows[j]["rsirunit"] = "";
        //        }
        //        else
        //        {
        //            if (dt1.Rows[j]["orderno"].ToString() == orderno)
        //            {
        //                dt1.Rows[j]["orderno1"] = "";
        //                dt1.Rows[j]["refno"] = "";
        //                dt1.Rows[j]["orderdat"] = "";


        //            }

        //            if (dt1.Rows[j]["centrid"].ToString() == centrid)
        //            {
        //                dt1.Rows[j]["centrdesc"] = "";



        //            }

        //            if (dt1.Rows[j]["custcode"].ToString() == custcode)
        //            {
        //                dt1.Rows[j]["custdesc"] = "";
        //                dt1.Rows[j]["teamdesc"] = "";
        //                //dt1.Rows[j]["rsirunit"] = "";


        //            }



        //        }

        //        centrid = dt1.Rows[j]["centrid"].ToString();
        //        orderno = dt1.Rows[j]["orderno"].ToString();
        //        custcode = dt1.Rows[j]["custcode"].ToString();



        //    }





        //    return dt1;

        //}




        private void SmsData()
        {
            //try
            //{
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string Usrid = hst["usrid"].ToString().Trim();
            //    string comnam = hst["comnam"].ToString().Trim();
            //    string trmid = hst["compname"].ToString().Trim();
            //    string usrSession = hst["session"].ToString().Trim();
            //    string comcod = this.GetCompCode();
            //    string orderno = this.lstOrderNo.SelectedValue.ToString().Trim();
            //    string Centrid = this.ddlCenter.SelectedValue.ToString().Trim();
            //    DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER", "GETSMSDATA", orderno, Centrid, "", "", "", "", "", "", "");

            //    string teamdesc = ds1.Tables[0].Rows[0]["teamdesc"].ToString().Trim();
            //    string Phone = ds1.Tables[0].Rows[0]["phone"].ToString().Trim();
            //    string Amount = "Dear Customer, Your Sales Order No: " + orderno + ". Tk." + Convert.ToDouble(ds1.Tables[0].Rows[0]["amount"]).ToString("#,##0;(#,##0); ") + " Approved";

            //    SmsApps.SendSms(comcod, Amount, comnam + " , " + teamdesc, Usrid + ", " + trmid + ", " + usrSession, Phone);
            //}
            //catch (Exception Ex)
            //{

            //}

        }



        protected void imgbtnSearchCheqNO_Click(object sender, ImageClickEventArgs e)
        {

            this.ShowData();
        }






        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string comcod = this.GetCompCode();
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string orderno = this.lstOrderNo.SelectedValue.ToString();
            //    string centrid = this.ddlCenter.SelectedValue.ToString();

            //    DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "RPTCUSTINFORMATION", orderno, centrid, "", "", "", "", "", "", "");
            //    if (ds2 == null)
            //        return;

            //    ReportDocument rptSOrder = new MFGRPT.R_23_SaM.RptSalOrdrZelta();

            //    TextObject txtrptcomp = rptSOrder.ReportDefinition.ReportObjects["Company"] as TextObject;
            //    txtrptcomp.Text = comnam;

            //    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //    txtHeader.Text = "SALES ORDER";

            //    TextObject txtCompAdd = rptSOrder.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //    txtCompAdd.Text = comadd;

            //    TextObject txtsaledate = rptSOrder.ReportDefinition.ReportObjects["txtsaledate"] as TextObject;
            //    txtsaledate.Text = this.txtdate.Text;

            //    TextObject txtCust = rptSOrder.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //    txtCust.Text = ds2.Tables[2].Rows[0]["name"].ToString().Trim();

            //    TextObject txtAdd = rptSOrder.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //    txtAdd.Text = ds2.Tables[2].Rows[0]["addr"].ToString().Trim();

            //    TextObject txtPhone = rptSOrder.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //    txtPhone.Text = ds2.Tables[2].Rows[0]["phone"].ToString().Trim();

            //    TextObject txtTrans = rptSOrder.ReportDefinition.ReportObjects["txtTrans"] as TextObject;
            //    txtTrans.Text = ds2.Tables[0].Rows[0]["courie"].ToString().Trim();

            //    TextObject txtStore = rptSOrder.ReportDefinition.ReportObjects["txtStore"] as TextObject;
            //    txtStore.Text = ds2.Tables[2].Rows[0]["storename"].ToString().Trim();


            //    TextObject txtCode = rptSOrder.ReportDefinition.ReportObjects["txtCode"] as TextObject;
            //    txtCode.Text = ds2.Tables[2].Rows[0]["sirtdes"].ToString().Trim();

            //    TextObject txtOrdTime = rptSOrder.ReportDefinition.ReportObjects["txtOrdTime"] as TextObject;
            //    txtOrdTime.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["posteddat"]).ToString("hh:mm:ss tt").Trim();


            //    TextObject txtsaleNo = rptSOrder.ReportDefinition.ReportObjects["txtsaleNo"] as TextObject;
            //    txtsaleNo.Text = orderno;
            //    //BALANCE 

            //    DataTable dt = ds2.Tables[0];
            //    DataTable dt2 = ds2.Tables[1];
            //    DataTable dt3 = ds2.Tables[2];

            //    double oStdAmt, Dipsamt, ordAmt, balAmt;
            //    oStdAmt = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("sum(dues)", "")) ? 0.00 : dt3.Compute("sum(dues)", "")));
            //    ordAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamount)", "")) ? 0.00 : dt.Compute("sum(tamount)", "")));
            //    Dipsamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(paidamt)", "")) ? 0.00 : dt2.Compute("sum(paidamt)", "")));

            //    balAmt = (oStdAmt + ordAmt) - Dipsamt;

            //    TextObject txtOutStdBal = rptSOrder.ReportDefinition.ReportObjects["txtOutStdBal"] as TextObject;
            //    txtOutStdBal.Text = oStdAmt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtDipositeAmt = rptSOrder.ReportDefinition.ReportObjects["txtDipositeAmt"] as TextObject;
            //    txtDipositeAmt.Text = Dipsamt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtOrderAmt = rptSOrder.ReportDefinition.ReportObjects["txtOrderAmt"] as TextObject;
            //    txtOrderAmt.Text = ordAmt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtBalanceAmt = rptSOrder.ReportDefinition.ReportObjects["txtBalanceAmt"] as TextObject;
            //    txtBalanceAmt.Text = balAmt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtAppby = rptSOrder.ReportDefinition.ReportObjects["txtAppby"] as TextObject;
            //    txtAppby.Text = ds2.Tables[2].Rows[0]["appby"].ToString().Trim();

            //    TextObject txtPreBy = rptSOrder.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            //    txtPreBy.Text = ds2.Tables[0].Rows[0]["usrname"].ToString().Trim();

            //    TextObject txtuserinfo = rptSOrder.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptSOrder.SetDataSource(ds2.Tables[0]);

            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "SALES ORDER";
            //        string eventdesc = "Print Report";
            //        string eventdesc2 = "ORDER: " + this.lstOrderNo.SelectedItem.Text;
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }

            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptSOrder.SetParameterValue("ComLogo", ComLogo);

            //    Session["Report1"] = rptSOrder;

            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //}
            //catch (Exception ex)
            //{

            //}

        }


        protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetOrderName();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            string centrid = this.ddlCenter.SelectedValue.ToString();
            string orderno = this.lstOrderNo.SelectedValue.ToString();
            //if (billstatus == "True")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('This Bill Already Adjusted');", true);
            //}
            //else
            //{
            if (orderno.Length != 0)
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_23_SaM/SalesOrder.aspx?Type=Edit" + "&orderno=" + orderno + "&centrid=" + centrid + "', target='_self');</script>";
            // }
        }

        private void SaveLeave()
        {

            for (int i = 0; i < this.gvLvReq.Rows.Count; i++)
            {
                //TimeSpan ts = (this.CalExt3.SelectedDate.Value - this.CalExt2.SelectedDate.Value);
                int leaveday = Convert.ToInt32("0" + ((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlapplied")).Text.Trim());

                if (leaveday > 0)
                {
                    string stdat = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string endat = Convert.ToDateTime(stdat).AddDays(leaveday - 1).ToString("dd-MMM-yyyy");
                    ((Label)this.gvLvReq.Rows[i].FindControl("lblgvenddat")).Text = endat;
                }
            }


        }

        private void LeaveUpdate()

        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string trnid = this.lstOrderNo.SelectedValue.ToString();
            string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text.Trim();
            bool result = false;
            string applydat = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            for (int i = 0; i < gvLvReq.Rows.Count; i++)
            {
                double lapplied = Convert.ToInt32("0" + ((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlapplied")).Text.Trim());
                if (lapplied > 0)
                {
                    string gcod = ((Label)this.gvLvReq.Rows[i].FindControl("lblgvgcod")).Text.Trim();
                    string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[i].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string forword = Convert.ToBoolean(this.Chboxforward.Checked).ToString();
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP02", trnid, empid, gcod, frmdate, todate, applydat, forword, "", "", "", "", "", "", "", "");

                    if (!result)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    }
                }

            }
        }
        protected void ApprovedBtn_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {


                this.SaveLeave();
                this.LeaveUpdate();
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                //this.CheckValue();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string ApprovByid = hst["usrid"].ToString();
                string Approvtrmid = hst["compname"].ToString();
                string ApprovSession = hst["session"].ToString();
                //string approvdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string approvdat = System.DateTime.Now.ToString("dd-MMM-yyyy");

                string Centrid = this.ddlCenter.SelectedValue.ToString();
                //string Type = (this.Request.QueryString["Type"]).ToString();
                //string Lvidno = (Type == "Ind") ? this.Request.QueryString["ltrnid"].ToString() : this.lstOrderNo.SelectedValue.ToString();
                string Orderno = this.lstOrderNo.SelectedValue.ToString();
                string approved = "Ok";
                string apDate = this.txtdate.Text.ToString();


                //for (int i = 0; i < dt1.Rows.Count; i++)
                //{
                //    string Chk = dt1.Rows[i]["chkmv"].ToString();
                //    if (Chk == "False")
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check CheckBox');", true);
                //        return;
                //    }
                //}
                //--------------------------Check this Order Approved Or Not--------------//

                DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "LEAVEAPPROVED", Orderno, ApprovByid, Centrid, "", "", "", "", "", "");
                if (ds4.Tables[0].Rows[0]["approved"].ToString() != "")
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Order Number Already Approved";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                //--------------------------Check More then 1 Approval--------------//

                if (ds4.Tables[1].Rows[0]["chk"].ToString() == "OK")
                {
                    if (ds4.Tables[2].Rows.Count == 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Need More then 1 Approval";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                        return;
                    }

                }


                else
                {
                    if (hst["compsms"].ToString() == "True")
                    {
                        this.sendsms();
                    }

                    else if (hst["compmail"].ToString() == "True")
                    {
                        this.sendMail();
                    }


                }


                bool result = false;
                string Orderno1 = "XXXXXXXXXXXXXX";


                //------------------C Table----------------------//

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "UPDATELVAPP", Orderno, ApprovByid, Approvtrmid, ApprovSession, approvdat, Centrid, "", "", "", "", "", "", "", "", "");
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Order Not Approved";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                if (!this.Chboxforward.Checked)
                {


                    DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "CHKFINALAPP", ApprovByid, Centrid, "", "", "", "", "", "", "");

                    //------------------B Table----------------------//
                    if (ds6.Tables[0].Rows.Count != 0)
                    {
                        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "UPDATELVAPPSTATUS", Orderno, apDate, "", "", "", "", "", "", "", "", "", "", "", "", "");

                        if (result == false)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Order Not Approved";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                        this.SMSORMAIL();


                    }


                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                        return;
                    }

                }

                else
                {


                    if (this.Request["Type"] == "App")
                    {

                        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "UPDATELVAPPSTATUS", Orderno, apDate, "", "", "", "", "", "", "", "", "", "", "", "", "");

                        if (result == false)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Order Not Approved";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                        this.lvconfirmSMS();


                    }



                }
             //  this.GetOrderName();
             ((Label)this.Master.FindControl("lblmsg")).Text = "Approved";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                //Session["tblOrder"] = dt;
                //this.Data_Bind();
                //this.CheckValue();


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }

        private void SMSORMAIL()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst["compsms"].ToString() == "True")
            {
                this.lvconfirmSMS();
            }

            else if (hst["compmail"].ToString() == "True")
            {
                lvconfirmMail();
            }


        }


        protected void lvconfirmMail()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetCompCode();

            string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
            string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;

            //string empid = this.ddlEmpName.SelectedValue.ToString();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPMAIL", empid, "", "", "", "", "", "", "", "");
            var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTHEADDETAILS", usrid, "", "", "", "", "", "", "", "");

            string empname = (string)ds1.Tables[0].Rows[0]["name"];
            string empdesig = (string)ds1.Tables[0].Rows[0]["desig"];
            string deptName = (string)ds1.Tables[0].Rows[0]["deptname"];


            if (ds == null)
                return;

            string mail = (string)ds.Tables[0].Rows[0]["mail"];

            string maildesc = "Leave Approved From : " + frmdate + " To " + todate + "\n" + "Approved By : " + empname + "\n" + "Designation : " + empdesig + "\n" +
            "Department Name : " + deptName;

            this.SendMailPass(maildesc, mail);

        }

        private void sendMail()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = this.ddlCenter.SelectedValue.ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
            string empname = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempname")).Text;
            string empdesig = ((Label)this.gvLvReq.Rows[0].FindControl("lgdesig")).Text;
            string deptName = ((Label)this.gvLvReq.Rows[0].FindControl("lgdeptanme")).Text;
            string leavetype = ((Label)this.gvLvReq.Rows[0].FindControl("lglvtype")).Text;
            string lapplied = ((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlapplied")).Text;
            string idcard = ((Label)this.gvLvReq.Rows[0].FindControl("lgidcard")).Text;
            string lvreason = lblvalNarration.Text;





            //string empid = this.ddlEmpName.SelectedValue.ToString();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAPPRVPMAIL", deptcode, "", "", "", "", "", "", "", "");

            if (ds == null)
                return;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string mail = (string)ds.Tables[0].Rows[i]["mail"];
                string maildesc = "Employee ID : " + idcard + " \n" + "Employee Name : " + empname + "\n" + "Designation : " + empdesig + "\n" +
                "Department Name : " + deptName + "\n" + "Leave Period : " + frmdate + " To " + todate + "\n" + "Leave Duration : " + lapplied +
                "\n" + "Leave Type : " + leavetype + "\n" + "Leave Reason : " + lvreason;
                this.SendMailPass(maildesc, mail);
                // }
            }

        }


        public void SendMailPass(string maildesc, string mail)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");

            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            string mailtousr = mail;
            //string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf";

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
            // oMail.Subject = subject;


            oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify; font-weight: bold;font-size: 14px'>" + "<br/>" + maildesc + "</pre></body></html>";
            // oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + maildesc + "</pre></body></html>";






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
        protected void sendsms()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = this.ddlCenter.SelectedValue.ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetCompCode();

            string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
            string empname = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempname")).Text;
            string empdesig = ((Label)this.gvLvReq.Rows[0].FindControl("lgdesig")).Text;

            //string empid = this.ddlEmpName.SelectedValue.ToString();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAPPRVPPHONE", deptcode, "", "", "", "", "", "", "", "");

            if (ds == null)
                return;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string phone = (string)ds.Tables[0].Rows[i]["phone"];
                if (hst["compsms"].ToString() == "True")
                {
                    SendSmsProcess sms = new SendSmsProcess();
                    string comnam = hst["comnam"].ToString();
                    string compname = hst["compname"].ToString();
                    // string frmname = "PurReqApproval.aspx?Type=RateInput";

                    string SMSText = "Leave applied from : " + frmdate + " To " + todate + "\n" + "Name: " + empname + " Designation : " + empdesig + "\n" + "First approved by " + username;
                    bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
                }
            }
        }

        protected void lvconfirmSMS()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = hst["deptcode"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetCompCode();

            string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
            string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;

            //string empid = this.ddlEmpName.SelectedValue.ToString();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPPHONE", empid, "", "", "", "", "", "", "", "");



            if (ds == null)
                return;

            string phone = (string)ds.Tables[0].Rows[0]["phone"];
            if (hst["compsms"].ToString() == "True")
            {
                SendSmsProcess sms = new SendSmsProcess();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                // string frmname = "PurReqApproval.aspx?Type=RateInput";

                string SMSText = "Leave approved from : " + frmdate + " To " + todate;// 
                bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
            }
        }



        protected void lbtnTotal_Click(object sender, EventArgs e)
        {


            this.SaveLeave();



        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string trnid = this.lstOrderNo.SelectedValue.ToString();
            string remarks = this.txtremarks.Text;

            if (remarks.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill remarks');", true);
                return;
            }

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted failed');", true);
                return;
            }


            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' Not Apporved');", true);

            if (hst["compsms"].ToString() == "True")
            {
                string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;
                string canname = hst["username"].ToString(); ;
                //string empid = this.ddlEmpName.SelectedValue.ToString();
                var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPPHONE", empid, "", "", "", "", "", "", "", "");

                if (ds == null)
                    return;
                string phone = (string)ds.Tables[0].Rows[0]["phone"];
                SendSmsProcess sms = new SendSmsProcess();
                string SMSText = "Leave Canceled by : " + canname; // 
                bool resultsms = sms.SendSmmsPwd(SMSText, SMSText, phone);

            }

            else if (hst["compmail"].ToString() == "True")

            {

                string usrid = hst["usrid"].ToString();
                string deptcode = hst["deptcode"].ToString();
                string username = hst["username"].ToString();
                string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
                string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;

                //string empid = this.ddlEmpName.SelectedValue.ToString();
                var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPMAIL", empid, "", "", "", "", "", "", "", "");
                var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTHEADDETAILS", usrid, "", "", "", "", "", "", "", "");

                string empname = (string)ds1.Tables[0].Rows[0]["name"];
                string empdesig = (string)ds1.Tables[0].Rows[0]["desig"];
                string deptName = (string)ds1.Tables[0].Rows[0]["deptname"];
                // string t = (string)ds1.Tables[0].Rows[0]["deptname"];

                if (ds == null)
                    return;

                string mail = (string)ds.Tables[0].Rows[0]["mail"];

                string maildesc = "Reason : " + remarks + "\n" + "Leave Canceled By : " + empname;

                this.SendMailPass(maildesc, mail);

            }



        }



        protected void lbtnnotapproved_Click(object sender, EventArgs e)
        {

        }

    }
}

