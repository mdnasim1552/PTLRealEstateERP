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
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_15_DPayReg
{
    public partial class AccOnlinePaymentApp : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);


                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtpaymentdate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Payment Approval Information";
                this.Initial();
                this.lbtnOk_Click(null, null);
                this.Bankcode();
                this.ShowNote();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void Initial()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "1103":
                    this.lblbank.Visible = true;
                    this.ddlbanklist.Visible = true;


                    break;

                default:

                    break;
            }



        }
        private void ShowNote()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            // string refno = "%" + this.txtSearch.Text.Trim() + "%";
            string date = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();//  Convert.ToDateTime(this.txtpaymentdate.Text).ToString("dd-MMM-yyyy");//System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Chkdate = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString(); // Convert.ToDateTime(this.txtpaymentdate.Text).ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();
            string approved = (this.Request.QueryString["Type"].ToString().Trim() == "ChequeApproval") ? "approved" : "";
            string refno = (this.Request.QueryString["payid"].ToString()).Length == 0 ? this.txtSearch.Text.Trim() + "%" : this.Request.QueryString["payid"].ToString() + "%";
            //string refno = "%" + this.txtSearch.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCHEQUEREADY", refno, date, "12", Chkdate, userid, approved, "", "", "");
            if (ds1 == null)
            {
                return;

            }


            //  ViewState["tblpayment"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tblNote"] = ds1.Tables[1];
            this.Note();


        }

        private void Note()
        {
            DataTable dt = (DataTable)ViewState["tblNote"];
            if (dt.Rows.Count > 0)
            {
                this.Hyplnk.Text = Convert.ToDouble(dt.Rows[0]["closdram"]).ToString("#,##0;(#,##0) ;");

                //this.lblClAmt.Text =  Convert.ToDouble(dt.Rows[0]["closdram"]).ToString("#,##0;(#,##0) ;");
                //this.lbllssueAmt.Text = Convert.ToDouble(dt.Rows[0]["isuamt"]).ToString("#,##0;(#,##0) ;");
                //this.lblCollAmt.Text = Convert.ToDouble(dt.Rows[0]["collamt"]).ToString("#,##0;(#,##0) ;");
                //this.lblnetBal.Text = Convert.ToDouble(dt.Rows[0]["bankbal"]).ToString("#,##0;(#,##0) ;");

            }
        }
        private void Bankcode()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string UserId = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                string ttsrch = "%";
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCONACCHEAD01", ttsrch, UserId, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count == 0)
                    return;
                this.ddlbanklist.DataTextField = "cactdesc";
                this.ddlbanklist.DataValueField = "cactcode";
                this.ddlbanklist.DataSource = ds2.Tables[0];
                this.ddlbanklist.DataBind();



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void lbtnBankPos_Load(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Date = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();

            this.lbtnBankPos.NavigateUrl = "LinkPayAcc.aspx?Type=BankPosition02&comcod=" + comcod + "&Date1=" + Date;
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowChequeApp();
        }


        private void ShowChequeApp()
        {
            ViewState.Remove("tblpayment");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            //string refno = (this.Request.QueryString["payid"].ToString()).Length == 0 ? this.txtSearch.Text.Trim() + "%" : this.Request.QueryString["payid"].ToString() + "%"; 

            string refno = this.Request.QueryString["payid"].ToString();
            //string refno = "%" + this.txtSearch.Text.Trim() + "%";
            string date = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();
            string userid = hst["usrid"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCHEQUEPAYMENT", refno, date, userid, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                return;

            }
            DataTable dt2 = HiddenSameData(ds1.Tables[0]);
            ViewState["tblpayment"] = dt2;
            this.Data_Bind();

            string reqnar = "";
            foreach (DataRow row in dt2.Rows)
            {
                if (row["reqnar"].ToString().Length > 0)
                {
                    reqnar += row["reqnar"].ToString() + " , ";
                }

            }
            this.txtNarration.Text = reqnar;

            ds1.Dispose();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ChequePayment":
                    string useridapp = dt1.Rows[0]["useridapp"].ToString();
                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    string billno1 = dt1.Rows[0]["billno1"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["useridapp"].ToString() == useridapp && dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            useridapp = dt1.Rows[j]["useridapp"].ToString();
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["usrdesig"] = "";
                            dt1.Rows[j]["actdesc"] = "";
                        }
                        if (dt1.Rows[j]["useridapp"].ToString() == useridapp)
                        {
                            useridapp = dt1.Rows[j]["useridapp"].ToString();

                            dt1.Rows[j]["usrdesig"] = "";

                        }
                        if (dt1.Rows[j]["billno1"].ToString() == billno1)
                        {
                            dt1.Rows[j]["reqnar"] = "";
                        }
                        //if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        //{
                        //    actcode = dt1.Rows[j]["actcode"].ToString();
                        //    dt1.Rows[j]["actdesc"] = "";
                        //}

                        useridapp = dt1.Rows[j]["useridapp"].ToString();
                        actcode = dt1.Rows[j]["actcode"].ToString();
                    }

                    break;





            }

            return dt1;

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblpayment"];
            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();
            this.FooterCalculation();
        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblpayment"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvPayment.FooterRow.FindControl("lblFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ? 0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");
            ((Label)this.gvPayment.FooterRow.FindControl("lblFApamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(apamt)", "")) ? 0.00 : dt.Compute("Sum(apamt)", ""))).ToString("#,##0;(#,##0); -");
            ((Label)this.gvPayment.FooterRow.FindControl("lblFBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(balamt)", "")) ? 0.00 : dt.Compute("Sum(balamt)", ""))).ToString("#,##0;(#,##0); -");


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintReadyCheque();
        }

        private void PrintReadyCheque()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptAccOnlinePay();
            DataTable dt = (DataTable)ViewState["tblpayment"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtdate.Text = "Date: " + ASTUtility.DateInVal(this.txtpaymentdate.Text).ToString();
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void SaveValue()
        {


            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                tbl1.Rows[i]["apamt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAppramt")).Text.Trim()).ToString();
                tbl1.Rows[i]["nochq"] = Convert.ToDouble("0" + ((Label)this.gvPayment.Rows[i].FindControl("lblgvchq")).Text.Trim()).ToString();
                tbl1.Rows[i]["remarks"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvRemarks")).Text.Trim().ToString();
                tbl1.Rows[i]["chkapp"] = (((CheckBox)this.gvPayment.Rows[i].FindControl("chkapp")).Checked) ? "True" : "False";

            }
            ViewState["tblpayment"] = tbl1;

        }





        protected void lbtnTotal_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.Data_Bind();





        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "ChequePayment":
                    this.UpdateChequeReady();
                    break;


            }





        }

        private void UpdateChequeReady()
        {
            lmsg.Visible = true;

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();


                this.SaveValue();

                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;

                string bankcode = this.ddlbanklist.SelectedValue.ToString();



                foreach (DataRow dr in dt1.Rows)
                {
                    string slnum = dr["slnum"].ToString().Trim();
                    string actcode = dr["actcode"].ToString().Trim();
                    string rescode = dr["rescode"].ToString().Trim();
                    string spcfcod = dr["spcfcod"].ToString().Trim();
                    string billno = dr["billno1"].ToString().Trim();
                    double amt = Convert.ToDouble("0" + dr["apamt"].ToString());
                    double nochq = Convert.ToDouble("0" + dr["nochq"].ToString());
                    string apppaydate = ASTUtility.DateFormat(dr["apppaydate"].ToString());
                    string fiappdate = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();
                    string Approved = "Ok";
                    string remarks = dr["remarks"].ToString().Trim();
                    string chkapp = dr["chkapp"].ToString().Trim();
                    string narration = this.txtNarration.Text.Trim();

                    if (chkapp == "False")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select the Checkbox";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }
                    if (chkapp == "True")
                    {
                        result = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATECHEQUEPTPARTY", slnum, actcode, rescode, amt.ToString(), nochq.ToString(), apppaydate, Approved, remarks, fiappdate, userid, Terminal,
                            Sessionid, billno, spcfcod, bankcode, narration, "", "", "", "", "");
                    }

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel(0);", true);

                        return;
                    }
                    else
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel(1);", true);

                    }


                }




                //this.ShowChequeApp();


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }


        protected void txtpaymentdate_TextChanged(object sender, EventArgs e)
        {
            this.txtpaymentdate.Text = ASTUtility.DateInVal(this.txtpaymentdate.Text);
        }
        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblpayment"];
            string slnum = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lbgvslnum")).Text.Trim();
            //int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
            // dt.Rows[rowindex].Delete();



            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEPAYAPP", slnum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }





            DataView dv = dt.DefaultView;
            dv.RowFilter = ("slnum<>'" + slnum + "'");
            ViewState.Remove("tblpayment");
            ViewState["tblpayment"] = dv.ToTable();

            ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Row";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            this.Data_Bind();




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string slnum = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lbgvslnum")).Text.Trim();
            //string Date = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();
            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEPAYAPP", slnum, Date, "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            //this.ShowChequeApp();
        }

        protected void chkAllapp_CheckedChanged(object sender, EventArgs e)
        {

            int i, index;
            DataTable dt = (DataTable)ViewState["tblpayment"];

            if (((CheckBox)this.gvPayment.HeaderRow.FindControl("chkAllapp")).Checked)
            {

                for (i = 0; i < this.gvPayment.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPayment.Rows[i].FindControl("chkapp")).Checked = true;
                    //  ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = true;
                    index = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + i;
                    dt.Rows[index]["chkapp"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPayment.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPayment.Rows[i].FindControl("chkapp")).Checked = false;
                    // ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = true;
                    index = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + i;
                    dt.Rows[index]["chkapp"] = "False";


                }

            }

            Session["tblpayment"] = dt;
        }




    }
}