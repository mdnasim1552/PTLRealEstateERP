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
using System.Collections.Generic;
using RealERPWEB.Service;

namespace RealERPWEB.F_17_Acc
{

    public partial class AccTopPageUpdate : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        UserService userSer = new UserService();
        public static string lblTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtVouDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtEntryDate.Text = "01" + date.Substring(2);
                this.txtVouDate_CalendarExtender.EndDate = System.DateTime.Today;
                //   string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Petty cash Bill Update";
                //this.Master.Page.Title = "Petty cash Bill Update";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "pttycash") ? "Petty cash Bill Approval Sheet" : "";
                this.GetBankCOde();
            }



        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);


            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void lnkPrint_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lnkOk_Click(object sender, EventArgs e)
        {
            //string type = this.Request.QueryString["Type"].ToString();
            //switch (type)
            //{
            //    case "pttycash":
            //        Get_pettyCashBillInfo();
            //        break;
            //}

            this.Get_pettyCashBillInfo();

        }
        protected void Get_pettyCashBillInfo()
        {
            //string SrchProject = "%" + this.txtSrchProjectName.Text.Trim() + "%";
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPETTYCASHFORACCUP", fromdate, todate);
            if (ds == null)
                return;
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = ds.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>();
            ViewState["Pettycash"] = lst;
            this.Data_Bind();




        }

        private void Data_Bind()
        {
            //string type = this.Request.QueryString["Type"].ToString();
            //switch (type)
            //{
            //    case "pttycash":

            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];
            if (lst.Count == 0)
            {
                this.gvpetty.DataSource = null;
                this.gvpetty.DataBind();
                return;
            }

            this.gvpetty.DataSource = lst;
            this.gvpetty.DataBind();

            //        break;
            //}
        }

        protected void gvpetty_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgvVounum1");
            HyperLink applink = (HyperLink)e.Row.FindControl("HypApprv");
            string mVOUNUM = hlink1.Text;


            string pcblno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pcblno")).ToString();
            string billdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "billdate")).ToString("dd-MMM-yyyy");
            applink.NavigateUrl = "~/F_17_Acc/AccPettyCashApp.aspx?Type=Entry&genno=" + pcblno + "&date=" + billdate;



            if (mVOUNUM.Trim().Length == 14)
            {

                hlink1.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + mVOUNUM;
            }


        }



        private void CheckValue()
        {

            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];
            DataTable dt = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.gvpetty.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.gvpetty.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";

                lst[i].chkmv = Convert.ToBoolean(chkmr);
                ((CheckBox)this.gvpetty.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.gvpetty.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.gvpetty.Rows[i].FindControl("lbtnupdate")).Enabled = (((CheckBox)this.gvpetty.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["Pettycash"] = lst;
        }
        protected void PTCUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                // DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);



                this.CheckValue();
                List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string Terminal = hst["trmid"].ToString();
                string Sessionid = hst["session"].ToString();
                string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                string pcblno = lst[rowindex].pcblno;

                var lst1 = lst.FindAll(l => l.pcblno == pcblno);


                foreach (RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash lsts in lst1)
                {
                    bool Chk = lsts.chkmv;
                    if (Chk == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check CheckBox');", true);
                        return;
                    }
                }







                //DateTime Paydate = Convert.ToDateTime(dr[0]["paydate"]);
                // string voudat = this.txtEntryDate.Text.Substring(0, 11);
                string voudat = this.txtVouDate.Text.Substring(0, 11);




                DateTime Bdate;
                bool dcon;
                Bdate = this.GetBackDate();

                dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                    return;
                }







                /////////////////--------------------------------------------------
                //Existing MR

                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPETTYCASH", pcblno, "", "", "", "", "", "", "", "");
                if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this Petty Cash No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                ////return;
                ////////////////-----------------------------
                string Cactcode = this.ddlBankName.SelectedValue.ToString();
                string txtnar = this.txtBillNarr.Text.ToString();

                string VouMode = "Payment";
                this.vounum(Cactcode, VouMode);
                DataTable dt12 = (DataTable)Session["NEWVOUNUM"];
                string vounum = dt12.Rows[0]["couvounum"].ToString();
                string vouno = vounum.Substring(0, 2);
                string voutype = (vouno == "JV" ? "Journal Voucher" :
                                   (vouno == "CD" ? "Cash Payment Voucher" :
                                   (vouno == "BD" ? "Bank Payment Voucher" :
                                   (vouno == "CC" ? "Cash Deposit Voucher" :
                                   (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));

                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACCUPDATEAB", vounum, voudat, pcblno, voutype, userid, Terminal, Sessionid, Postdat, Cactcode, txtnar,
                               "", "", "", "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }




                lst[rowindex].newvocnum = vounum;
                ViewState["Pettycash"] = lst;
                this.Data_Bind();
                this.CheckValue();





            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }


        private void GetBankCOde()
        {
            Session.Remove("tblachead");
            string comcod = this.GetCompCode();
            string SearchBank = "%" + this.txtSerchBank.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD", SearchBank, "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1;
            this.ddlBankName.DataBind();
            ds1.Dispose();

        }
        private void vounum(string Cactcode, string VouMode)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate >= Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }
                double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
                string ConAccHead = Cactcode;
                string VNo1 = (ConAccHead.Substring(0, 4) == "1901" ? "C"
                    : ((ConAccHead.Substring(0, 2) == "22" || ConAccHead.Substring(0, 2) == "23" || ConAccHead.Substring(0, 2) == "24" || ConAccHead.Substring(0, 2) == "16") ? "J" : "B"));




                string VNo2 = (VNo1 == "J" ? "V" : ((VouMode == "Payment") ? "D" : "C"));
                string VNo3 = Convert.ToString(VNo1 + VNo2);
                // string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
                string entrydate = this.txtVouDate.Text.Substring(0, 11).Trim();

                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                Session["NEWVOUNUM"] = dt4;

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        protected void ibtnSrchBank_Click(object sender, EventArgs e)
        {
            this.GetBankCOde();

        }
        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string entrydate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", userid, entrydate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));






        }
    }
}


