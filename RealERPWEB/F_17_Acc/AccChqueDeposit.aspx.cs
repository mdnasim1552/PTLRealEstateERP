using System;
using System.Collections;
using System.Collections.Generic;
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
using Microsoft.Reporting.WinForms;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
using RealERPWEB.Service;

namespace RealERPWEB.F_17_Acc
{
    public partial class AccChqueDeposit : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        UserService userSer = new UserService();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);



                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Cheque Deposit";


                this.ShowView();


                string type = this.Request.QueryString["Type"].ToString().Trim();
                string title = (type == "ChquedepEntry") ? "Cheque Deposit" : (type == "RegChqCl") ? "Cheque Register History"
                    : "Cheque Deposit Report";
                //((Label)this.Master.FindControl("lblTitle")).Text = title;

                //this.Master.Page.Title = title;
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                CommonButton();
            }
        }


        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (type == "ChquedepEntry")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);

            }
            if (type == "MgtChqdepEntry")
            {

            }
            if (type == "RegChqCl")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnregUpdate_Click);
                ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lbtnDeleteAll_Click);

            }

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            switch (type)
            {

                case "ChquedepEntry":
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = "01" + date.Substring(2);
                    this.txttodate.Text = date;
                    this.txtDepositDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.GetConActCode();
                    this.GetProjectName();
                    // this.CompanyBalance();
                    this.MultiView1.ActiveViewIndex = 0;
                    ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
                    break;
                case "MgtChqdepEntry":
                    string date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfDate.Text = "01" + date1.Substring(2);
                    this.txttDate.Text = date1;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "RegChqCl":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.Panel1.Visible = false;
                    this.ShowRegChqHistory();
                    //this.CreateTable();
                    break;

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetConActCode()
        {
            Session.Remove("tblachead");
            string comcod = this.GetCompCode();
            string SearchBank = "%" + this.txtSerchBank.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETBANKCODE", SearchBank, "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1;
            this.ddlBankName.DataBind();
            Session["tblachead"] = ds1.Tables[0];
            this.ddlBankName.SelectedValue = (ds1.Tables[0].Select("actcode like '1902%'").Length > 0) ?
                    ds1.Tables[0].Select("actcode like '1902%'")[0]["actcode"].ToString() : ds1.Tables[0].Rows[0]["actcode"].ToString();

            ds1.Dispose();
            this.ddlBankName_SelectedIndexChanged(null, null);

        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            //string SearchProject = "%" + this.txtSerchProject.Text.Trim() + "%";
            string tempProject = Request.QueryString["prjcode"] ?? "";
            string SearchProject = (tempProject.ToString()).Length == 0 ? "%" + this.txtSerchProject.Text.Trim() + "%" : tempProject + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETPROJECTNAME", SearchProject, "", "", "", "", "", "", "", "");
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1;
            this.ddlProName.DataBind();
            this.ddlProName.SelectedValue = "000000000000";
            ds1.Dispose();
        }

        //private void CompanyBalance()
        //{
        //    string comcod = this.GetCompCode();
        //    //string SearchProject = "%" + this.txtSerchProject.Text.Trim() + "%";
        //    string projectcode = Request.QueryString["prjcode"] ?? "";
        //    string usircode = Request.QueryString["usircode"] ?? "";
        //    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETEXCESSDEPOSITE", projectcode, usircode, "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //    {
        //        return;
        //    }


        //    Session["tblbalance"] = ds1.Tables[0];

        //}




        private void ShowRegChqHistory()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "SHOWREGCHQHISTORY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvRegChqCl.DataSource = null;
                this.grvRegChqCl.DataBind();
                return;
            }
            Session["tblbdeposit"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();




        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChquedepEntry":
                    this.ShowDepositBank();
                    break;


            }




        }

        private void ShowDepositBank()
        {
            Session.Remove("tblbdeposit");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProName.SelectedValue.ToString() + "%";
            string chequeno = (this.Request.QueryString["chqno"].ToString()).Length == 0 ? "%" + this.txtSrchChequeno.Text.Trim() + "%" : this.Request.QueryString["chqno"].ToString() + "%";
            // string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETDEPOSITINFO", frmdate, todate, pactcode, chequeno, "", "", "", "", "");
            if (ds1 == null)
            {
                this.dgv1.DataSource = null;
                this.dgv1.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }



        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChquedepEntry":
                    this.dgv1.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
                    this.dgv1.DataSource = (DataTable)Session["tblbdeposit"];
                    this.dgv1.DataBind();
                    this.FooterCalculation();
                    break;
                case "MgtChqdepEntry":
                    this.grvMgtChqDep.PageSize = Convert.ToInt32(this.ddlPageSize1.SelectedValue);
                    this.grvMgtChqDep.DataSource = (DataTable)Session["tblbdeposit"];
                    this.grvMgtChqDep.DataBind();
                    break;
                case "RegChqCl":
                    this.grvRegChqCl.PageSize = Convert.ToInt32(this.ddlPageSize1.SelectedValue);
                    this.grvRegChqCl.DataSource = (DataTable)Session["tblbdeposit"];
                    this.grvRegChqCl.DataBind();
                    this.FooterCalculation();
                    break;

            }
        }
        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tblbdeposit"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (dt1.Rows.Count == 0)
                return;
            switch (type)
            {
                case "ChquedepEntry":
                    ((Label)this.dgv1.FooterRow.FindControl("lgvFdramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(paidamt)", "")) ?
                                0 : dt1.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "RegChqCl":
                    ((Label)this.grvRegChqCl.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(trnam)", "")) ?
                               0 : dt1.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0); ");

                    break;
            }


        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChquedepEntry":
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }

                        else
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                    }

                    break;
                case "MgtChqdepEntry":
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }

                        else
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                    }

                    break;
            }



            return dt1;

        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            this.SaveValue();
            DataTable tbl2 = (DataTable)Session["tblbdeposit"];
            string comcod = this.GetCompCode();
            string bankcode = this.ddlBankName.SelectedValue.ToString();
            string rescode = this.ddlresource.Items.Count == 0 ? "000000000000" : this.ddlresource.SelectedValue.ToString();
            string depositdat = this.txtDepositDate.Text.Trim();

            //DataTable dtuser = (DataTable)Session["UserLog"];
            //string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            //string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            //string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            //string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");


            // Company Balance

            switch (comcod)
            {
                case "3340"://Urban
                //case "3101"://Urban

                                     
                    string pactcode = this.Request.QueryString["prjcode"].ToString() ?? "" ;
                    string usircode = this.Request.QueryString["usircode"].ToString() ?? "" ;                   
                    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETEXCESSDEPOSITE", pactcode, usircode, "", "", "", "", "", "", "");

                    double balamt = 0;

                    balamt = ds1.Tables[0].Rows.Count == 0 ? 0.00 : Convert.ToDouble(ds1.Tables[0].Rows[0]["schamt"]);
                    double paidamt = 0.00;
                    double paidamt1 = 0.00;
                    for (int i = 0; i < tbl2.Rows.Count; i++)
                    {

                        if (tbl2.Rows[i]["chkdep"].ToString().Trim() == "True")
                        {
                            paidamt1 = Convert.ToDouble(tbl2.Rows[i]["paidamt"]);
                            paidamt = +paidamt1;

                        }



                    }
                 
                    if(ASTUtility.Left(pactcode,2)=="18")
                    {
                        if (paidamt > balamt)
                        {

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Please Updated Accounts Voucher !!!!!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;

                        }

                    }


                    ////string mrno = tbl2.Rows[0]["mrno"].ToString();
                    ////double  paidamt = Convert.ToDouble( tbl2.Rows[0]["paidamt"]);
                    //DataTable dt2 = (DataTable)Session["tblbalance"];
                    //double SAmount = 0;
                    //double PAmount = 0, BalAmt = 0;

                    break;
                default:
                    break;



            }




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string tblPostedByid = hst["usrid"].ToString();
            string tblPostedtrmid = hst["compname"].ToString();
            string tblPostedSession = hst["session"].ToString();
            string tblPosteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            //string tblPostedByid = hst["usrid"].ToString();
            //string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            //string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            //string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool result = false;
            for (int i = 0; i < tbl2.Rows.Count; i++)
            {

                string mrno = tbl2.Rows[i]["mrno"].ToString();
                string chqno = tbl2.Rows[i]["chqno"].ToString().Trim();
                string depositslip = tbl2.Rows[i]["depositslip"].ToString().Trim();
                string chkdeposit = tbl2.Rows[i]["chkdep"].ToString().Trim();
                if (chkdeposit == "True")
                {
                    result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "INSORUPCDEPINF", mrno, chqno, bankcode, depositdat, depositslip,
                        rescode, tblPostedSession, tblPostedByid, tblPosteddat, tblPostedtrmid, "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
            }


         ((Label)this.Master.FindControl("lblmsg")).Text = " Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            this.ShowDepositBank();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "RegChqCl":
                    this.RptRegChqHistory();
                    break;
                case "ChquedepEntry":
                    this.PrintDepositChq();
                    break;

            }

        }

        private void PrintDepositChq()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;


            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("chkdep=True");
            dt = dv.ToTable();


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptCkhDeposit>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptCkhDeposit", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text =
                @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() +
                "', target='_blank');</script>";
        }
        private void RptRegChqHistory()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            DataTable dt = (DataTable)Session["tblbdeposit"];

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RegChequeHistory>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptRegBillStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", "Cheque Register Status Report "));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptRegBillStatus();
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void dgv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.dgv1.EditIndex = -1;
            this.Data_Bind(); ;
        }



        protected void imgSearchCheque_Click(object sender, EventArgs e)
        {
            this.lnkOk_Click(null, null);
        }
        protected void ibtnSrchBank_Click(object sender, EventArgs e)
        {
            this.GetConActCode();

        }


        protected void dgv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.dgv1.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        private void SaveValue()

        {
            DataTable dt = (DataTable)Session["tblbdeposit"];
            int RowIndex;
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {

                string Depositslip = ((TextBox)dgv1.Rows[i].FindControl("txtdepositslip")).Text.Trim();
                string chkdeposit = (((CheckBox)this.dgv1.Rows[i].FindControl("chkdep")).Checked) ? "True" : "False";
                RowIndex = (dgv1.PageIndex) * dgv1.PageSize + i;
                dt.Rows[RowIndex]["depositslip"] = Depositslip;
                dt.Rows[RowIndex]["chkdep"] = chkdeposit;
            }
            Session["tblbdeposit"] = dt;
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void grvMgtChqDep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvMgtChqDep.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvMgtChqDep_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvMgtChqDep.EditIndex = -1;
            this.Data_Bind();
        }
        protected void grvMgtChqDep_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            bool result = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string bankcode = ((DropDownList)this.grvMgtChqDep.Rows[e.RowIndex].FindControl("ddlgvBankName")).SelectedValue.ToString();
            string depositslip = ((Label)this.grvMgtChqDep.Rows[e.RowIndex].FindControl("txtdepositslip")).Text;
            string mrno = ((Label)this.grvMgtChqDep.Rows[e.RowIndex].FindControl("lgvmrno")).Text;
            string chqno = ((Label)this.grvMgtChqDep.Rows[e.RowIndex].FindControl("lgvCheNo")).Text;
            string depositdat = Convert.ToDateTime(((TextBox)this.grvMgtChqDep.Rows[e.RowIndex].FindControl("txtgvDepDate")).Text).ToString("dd-MMM-yyyy");
            string depositdat1 = Convert.ToDateTime(((Label)this.grvMgtChqDep.Rows[e.RowIndex].FindControl("lgvdepositDate")).Text).ToString("dd-MMM-yyyy");
            result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "DELETECDEPINF", mrno, chqno, "", depositdat1, "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid date";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "INSORUPCDEPINF", mrno, chqno, bankcode, depositdat, depositslip, "", "", "", "", "", "", "", "", "", "");


            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid date";
                return;
            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Transaction Link";
                string eventdesc = "Update Transaction Link";
                string eventdesc2 = "MR: " + mrno + "Chq: " + chqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            this.grvMgtChqDep.EditIndex = -1;
            this.lnkBtnOk_Click(null, null);
        }

        protected void grvMgtChqDep_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvMgtChqDep.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            int rowindex = (grvMgtChqDep.PageSize) * (this.grvMgtChqDep.PageIndex) + e.NewEditIndex;

            string bankcode = ((DataTable)Session["tblbdeposit"]).Rows[rowindex]["bankcode"].ToString();

            DropDownList ddl2 = (DropDownList)this.grvMgtChqDep.Rows[e.NewEditIndex].FindControl("ddlgvBankName");
            ViewState["gindex"] = e.NewEditIndex;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SearchBank = "%" + ((TextBox)grvMgtChqDep.Rows[e.NewEditIndex].FindControl("txtSerachBank")).Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETBANKCODE2", SearchBank, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = bankcode;
            //ddl2.Visible = true;

            //ddl2.Visible = false;
            //ddl2.Items.Clear();
        }
        protected void lnkBtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblbdeposit");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttDate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtChqno.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETMGTDEPOSITINFO", frmdate, todate, chequeno, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvMgtChqDep.DataSource = null;
                this.grvMgtChqDep.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();
        }
        protected void imgBtnChq_Click(object sender, EventArgs e)
        {
            this.lnkBtnOk_Click(null, null);
        }
        protected void ibtnSrchBank1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.grvMgtChqDep.Rows[rowindex].FindControl("ddlgvBankName");
            string SearchBank = "%" + ((TextBox)grvMgtChqDep.Rows[rowindex].FindControl("txtSerachBank")).Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETBANKCODE2", SearchBank, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
        }

        protected void ddlPageSize1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void ibtnFindChequeno_Click(object sender, EventArgs e)
        {
            this.GetRegIsuChq();
        }
        private void GetRegIsuChq()
        {
            Session.Remove("tblisucheque");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtScheqno = "%" + this.txtIssSch.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETPAYPRONUM", txtScheqno, "", "", "", "", "", "", "", "");
            this.ddlChequeNo.DataTextField = "textfield";
            this.ddlChequeNo.DataValueField = "valuefield";
            this.ddlChequeNo.DataSource = ds1.Tables[0];
            this.ddlChequeNo.DataBind();
            Session["tblisucheque"] = ds1.Tables[0];
            ds1.Dispose();
            if (this.ddlChequeNo.Items.Count > 0)
                this.AddChequeNo();
            this.txtIssSch.Focus();
        }
        private void AddChequeNo()
        {
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.Sort = ("slno asc");
            dt = dv.ToTable();
            string isunum = this.ddlChequeNo.SelectedValue.ToString();



            int tocount = dt.Rows.Count;
            int slno = (tocount == 0) ? 1 : Convert.ToInt32(dt.Rows[tocount - 1]["slno"]) + 1;

            DataRow[] dr = dt.Select("isunum='" + isunum + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["slno"] = slno;
                dr1["isunum"] = isunum;
                dr1["recdat"] = ((DataTable)Session["tblisucheque"]).Select("isunum='" + isunum + "'")[0]["recdat"].ToString();
                dr1["billnat"] = ((DataTable)Session["tblisucheque"]).Select("isunum='" + isunum + "'")[0]["billnat"].ToString(); ;
                dr1["payto"] = ((DataTable)Session["tblisucheque"]).Select("isunum='" + isunum + "'")[0]["payto"].ToString();
                dr1["actdesc"] = ((DataTable)Session["tblisucheque"]).Select("isunum='" + isunum + "'")[0]["actdesc"].ToString();
                dr1["refno"] = ((DataTable)Session["tblisucheque"]).Select("isunum='" + isunum + "'")[0]["refno"].ToString();
                dr1["trnam"] = Convert.ToDouble(((DataTable)Session["tblisucheque"]).Select("isunum='" + isunum + "'")[0]["trnam"]).ToString();
                dr1["paydat"] = ((DataTable)Session["tblisucheque"]).Select("isunum='" + isunum + "'")[0]["paydat"].ToString();
                dt.Rows.Add(dr1);
            }

            // Session["tblbdeposit"] = dt;        
            dv = dt.DefaultView;
            dv.Sort = ("slno desc");
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();


        }
        protected void grvRegChqCl_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblbdeposit"];
            string Issueno = ((Label)this.grvRegChqCl.Rows[e.RowIndex].FindControl("lbgvslnum")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "DELREGCHQHISISSUNUM", Issueno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvRegChqCl.PageSize) * (this.grvRegChqCl.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblbdeposit");
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void grvRegChqCl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvRegChqCl.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnregUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            string comcod = this.GetCompCode();
            DataTable tbl1 = (DataTable)Session["tblbdeposit"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string IssueNo = tbl1.Rows[i]["isunum"].ToString();
                string recdat = Convert.ToDateTime(tbl1.Rows[i]["recdat"]).ToString("dd-MMM-yyyy");
                string billnat = tbl1.Rows[i]["billnat"].ToString();
                string Payto = tbl1.Rows[i]["payto"].ToString();
                string Actdesc = tbl1.Rows[i]["actdesc"].ToString();
                string refno = tbl1.Rows[i]["refno"].ToString();
                string paydat = Convert.ToDateTime(tbl1.Rows[i]["paydat"]).ToString("dd-MMM-yyyy");
                string Amount = Convert.ToDouble(tbl1.Rows[i]["trnam"]).ToString();


                bool result = accData.UpdateTransInfo2(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "INSORUPREGCHQHISTORY",
                             IssueNo, recdat, billnat, Payto, Actdesc, refno, Amount, paydat, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    return;
                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void lbtnDeleteAll_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            string comcod = this.GetCompCode();
            bool result = accData.UpdateTransInfo2(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "DELETEREGCHQHISTORY",
                             "", "", "", "", "", "", "", "",
                             "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            this.ShowRegChqHistory();
        }
        protected void chkorcheqnoasc_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv = new DataView();
            if (this.chkorcheqnoasc.Checked)
            {

                dv = dt.DefaultView;
                dv.Sort = ("slno asc");

            }
            else
            {
                dv = dt.DefaultView;
                dv.Sort = ("slno desc");


            }
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void lbtnSelectChequeNo_Click(object sender, EventArgs e)
        {
            this.AddChequeNo();
        }
        protected void lbtnreshead_Click(object sender, EventArgs e)
        {
            this.GetResCode();
        }
        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {

            string search1 = this.ddlBankName.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["tblachead"];
            DataView dv = new DataView();
            var results = (from srchrow in dt.AsEnumerable()
                           where srchrow.Field<string>("actcode").Equals(search1)
                           select srchrow);
            dv = results.AsDataView();

            //DataTable dt1 = dv.ToTable();










            //DataTable dt01 = (DataTable)Session["HeadAcc1"];
            //string search1 = this.ddlacccode.SelectedValue.ToString().Trim();fup
            //DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");


            if (dv.ToTable().Rows[0]["actelev"].ToString() == "2")
            {
                this.lblreshead.Visible = true;
                this.txtsrchres.Visible = true;
                this.lbtnreshead.Visible = true;
                this.ddlresource.Visible = true;
                this.GetResCode();
            }
            else
            {
                this.lblreshead.Visible = false;
                this.txtsrchres.Visible = false;
                this.lbtnreshead.Visible = false;
                this.ddlresource.Visible = false;
                this.ddlresource.Items.Clear();

            }
        }



        private void GetResCode()
        {


            try
            {

                DataTable dt = (DataTable)Session["tblachead"];
                string actcode = this.ddlBankName.SelectedValue.ToString();
                string filter1 = "%" + this.txtsrchres.Text.Trim() + "%";
                string SearchInfo = "";


                DataView dv = new DataView();
                var results = (from srchrow in dt.AsEnumerable()
                               where srchrow.Field<string>("actcode").Equals(actcode)
                               select srchrow);
                dv = results.AsDataView();


                string type = dv.ToTable().Rows[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {

                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {


                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHead(actcode, filter1, SearchInfo);


                this.ddlresource.DataSource = lst;
                this.ddlresource.DataTextField = "resdesc1";
                this.ddlresource.DataValueField = "rescode";
                this.ddlresource.DataBind();






            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
            }



            //try
            //{
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string actcode = this.ddlacccode.SelectedValue.ToString();
            //    string filter1 = "%" + this.txtserchReCode.Text.Trim() + "%";

            //    string oldRescode = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedValue.ToString();


            //    string SearchInfo = "";
            //    DataTable dt01 = (DataTable)Session["HeadAcc1"];
            //    string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            //    DataRow[] drac = dt01.Select("actcode='" + search1 + "'");
            //    string type = drac[0]["acttype"].ToString().Trim();
            //    if (type.Length > 0)
            //    {
            //        string[] ar = type.Split('/');
            //        foreach (string ar1 in ar)
            //        {

            //            if (ar1.Contains("-"))
            //                SearchInfo = SearchInfo + "left(sircode,2) between " + ar1.Trim().Replace("-", " and ") + " ";
            //            else
            //                SearchInfo = SearchInfo + "left(sircode,2)" + " = " + ar1 + " ";
            //            SearchInfo = SearchInfo + " or ";

            //        }
            //        if (SearchInfo.Length > 0)
            //            SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            //    }


            //    DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", actcode, filter1, SearchInfo, "", "", "", "", "", "");
            //    DataTable dt3 = ds3.Tables[0];
            //    Session["HeadRsc1"] = ds3.Tables[0];
            //    this.ddlresuorcecode.DataSource = dt3;
            //    this.ddlresuorcecode.DataTextField = "resdesc1";
            //    this.ddlresuorcecode.DataValueField = "rescode";
            //    this.ddlresuorcecode.DataBind();



            //    DataRow[] dt2 = dt3.Select("rescode='" + oldRescode + "'");
            //    if (dt2.Length > 0)
            //    {
            //        this.ddlresuorcecode.SelectedValue = oldRescode;


            //    }




            //    this.txtserchReCode.Text = "";

            //    string seaRes = this.ddlresuorcecode.SelectedValue.ToString().Trim();
            //    DataRow[] dr1 = dt3.Select("rescode='" + seaRes + "'");
            //    if (dr1.Length == 0)
            //        return;

            //    if (ASTUtility.Left(dr1[0]["rescode"].ToString(), 1) == "9")
            //    {
            //        this.lblbillno.Visible = true;
            //        this.txtserchBill.Visible = true;
            //        this.lnkBillNo.Visible = true;
            //        this.ddlBillList.Visible = true;


            //    }
            //    else
            //    {
            //        this.lblbillno.Visible = false;
            //        this.txtserchBill.Visible = false;
            //        this.lnkBillNo.Visible = false;
            //        this.ddlBillList.Visible = false;
            //        this.ddlBillList.Items.Clear();

            //    }
            //}


            //catch (Exception ex)
            //{

            // ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
            //}

        }
    }
}