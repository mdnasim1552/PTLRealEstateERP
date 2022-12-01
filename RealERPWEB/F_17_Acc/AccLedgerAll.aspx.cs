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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{

    public partial class AccLedgerAll : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                //((Label)this.Master.FindControl("lblTitle")).Text = (rbtnLedger.SelectedValue.ToString()=="SubLedger")?"Accounts Subsidiary Ledger":"Ledger";
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                ((Label)this.Master.FindControl("lblTitle")).Text = "Accounts Ledger- All";
                this.Master.Page.Title = "Ledger";
                this.rbtnLedger.SelectedIndex = 0;
                this.rbtnLedger_SelectedIndexChanged(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc2 = "";
                    string comcod = this.GetCompcode();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }

                //if (ConstantInfo.LogStatus)
                //{
                //    string comcod = this.GetCompcode();
                //    string eventdesc = "View " + ((Label)this.Master.FindControl("lblTitle")).Text;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, "");


                //}
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private string GetCompcode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void rbtnLedger_SelectedIndexChanged(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();
            string type = this.rbtnLedger.SelectedValue.ToString();
            switch (type)
            {

                case "Ledger":
                    if (this.txtDateFrom.Text.Trim().Length == 0)
                    {
                        double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                        this.txtDateFrom.Text = (comcod == "3336" || comcod == "3337") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                        this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                    }
                    this.rbtnLedger.SelectedIndex = 0;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.rbtnList1.SelectedIndex = 0;
                    this.Panel1.Visible = false;
                    this.IbtnSearchAcc_Click(null, null);
                    break;
                case "SubLedger":
                    if (this.txtDateFrom.Text.Trim().Length == 0)
                    {
                        double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                        this.txtDateFrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                        this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                    }
                    this.ibtnFindRes_Click(null, null);
                    this.Panel1.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.rbtnList1.SelectedIndex = 0;
                    this.ddlConAccResHead.Items.Clear();
                    this.dgv2.DataSource = null;
                    this.dgv2.DataBind();
                    break;

                case "DetailLedger":
                    this.txtDateFromSp.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txtDatetoSp.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.ibtnFindResSP_Click(null, null);
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ddlConAccResHead.Items.Clear();
                    this.gvSpledger.DataSource = null;
                    this.gvSpledger.DataBind();
                    break;


                case "DetailLedger02":
                    this.txtdatefrmsp02.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txtDatetosp02.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.ibtnFindResSP_Click(null, null);
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ddlConAccResHead.Items.Clear();
                    this.gvSpledger.DataSource = null;
                    this.gvSpledger.DataBind();
                    break;
            }

        }
        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompcode();
                string filter = "%";
                DataSet ds1 = new DataSet();
                if (rbtnLedger.SelectedValue.ToString() == "SubLedger")
                {

                    ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEADWITHRES", filter, "", "", "", "", "", "", "", "");
                }

                else
                {
                    ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD01", filter, "", "", "", "", "", "", "", "");
                }
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();

                this.ibtnFindRes_Click(null, null);

            }
            catch (Exception ex)
            {
                string msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ msg + "');", true);
                return;
            }
        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string comcod = GetCompcode();
            string filter = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCRESHEAD", actcode, filter, "", "", "", "", "", "", "");

            //string filter = (this.Request.QueryString["sircode"].ToString()).Length == 0 ? "%" + this.txtSrchRes.Text.Trim() + "%" : this.Request.QueryString["sircode"].ToString() + "%";
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCRESHEAD", actcode, filter, "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            //DataTable dt1 = ds1.Tables[0];
            this.ddlConAccResHead.DataSource = ds1.Tables[0];
            this.ddlConAccResHead.DataTextField = "resdesc1";
            this.ddlConAccResHead.DataValueField = "rescode";
            this.ddlConAccResHead.DataBind();
        }
        protected void ibtnFindResSP_Click(object sender, EventArgs e)
        {
            this.GetResList();
        }

        protected void lnkbtnRessp02_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();
            string filter = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTSPLGACCRESLIST", "%", filter, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlResoucesp02.DataTextField = "resdesc1";
            this.ddlResoucesp02.DataValueField = "rescode";
            this.ddlResoucesp02.DataSource = ds1.Tables[0];
            this.ddlResoucesp02.DataBind();

            ds1.Dispose();


        }
        private void GetResList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();
            string filter = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTSPLGACCRESLIST", "%", filter, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlRescode.DataTextField = "resdesc1";
            this.ddlRescode.DataValueField = "rescode";
            this.ddlRescode.DataSource = ds1.Tables[0];
            this.ddlRescode.DataBind();
        }

        protected void lnkShowLedger_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();
            Session.Remove("StoreTable");
            DataSet ds2 = this.GetDataForReport();
            DataTable dt = ds2.Tables[0];
            if (dt.Rows.Count == 0)
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found ..');", true);
                return;
            }
            Session["StoreTable"] = dt;
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Account Ledger";
            //    string eventdesc = "Show Ledger";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
        }

        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;
            //string grp=
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {

                if ((dt.Rows[i]["vounum"]).ToString().Trim() == "CURRENT DR/CR" || (dt.Rows[i]["vounum"]).ToString().Trim() == "TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "BALANCE")
                    continue;
                if ((dt.Rows[i]["grp"]).ToString().Trim() == "C")
                    break;

                if (((dt.Rows[i]["cactcode"]).ToString().Trim()).Length == 12)
                {
                    dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                    cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                    balamt = balamt + (dramt - cramt);
                    dt.Rows[i]["balamt"] = balamt;
                }
            }
            return dt;


        }

        private void HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return;
            string grp = dt1.Rows[0]["grp"].ToString();
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                if (dt1.Rows[j]["vounum"].ToString() == vounum)
                {

                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    //dt1.Rows[j]["refnum"] = "";
                }




                if (dt1.Rows[j]["vounum"].ToString().Trim() == "CURRENT DR/CR")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                }


                if (dt1.Rows[j]["vounum"].ToString().Trim() == "TOTAL")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";

                }
                if (dt1.Rows[j]["vounum"].ToString().Trim() == "BALANCE")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                }

                grp = dt1.Rows[j]["grp"].ToString();
                vounum = dt1.Rows[j]["vounum"].ToString();
            }


            this.dgv2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.dgv2.DataSource = dt1;
            this.dgv2.DataBind();
            Session["Report1"] = dgv2;

            if (dt1.Rows.Count > 0)
            {
                ((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            this.dgv2.Columns[0].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") || (this.chkqty.Checked);
            // this.dgv2.Columns[6].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") && (this.chkqty.Checked); // Emdad 9.20.2020
            this.dgv2.Columns[7].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") && (this.chkqty.Checked);


            //Session["Report1"] = dgv2;
            //if (dt1.Rows.Count > 0)
            //{
            //    ((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            //}
            //this.dgv2.Columns[0].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") || (this.chkqty.Checked);
            //this.dgv2.Columns[6].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") && (this.chkqty.Checked);
            //this.dgv2.Columns[7].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") && (this.chkqty.Checked);


            //if (dt1.Rows.Count == 0)
            //    return;
            //string grp = dt1.Rows[0]["grp"].ToString();
            //string Date1 = dt1.Rows[0]["voudat1"].ToString();
            //string vounum = dt1.Rows[0]["vounum1"].ToString();
            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["grp"].ToString() == grp)
            //    {
            //        grp = dt1.Rows[j]["grp"].ToString();
            //        dt1.Rows[j]["grpdesc"] = "";
            //    }

            //    if (dt1.Rows[j]["vounum1"].ToString() == vounum)
            //    {

            //        dt1.Rows[j]["vounum1"] = "";
            //        dt1.Rows[j]["voudat1"] = "";
            //        //dt1.Rows[j]["refnum"] = "";
            //    }

            //    if (dt1.Rows[j]["vounum1"].ToString().Trim() == "TOTAL")
            //    {
            //        dt1.Rows[j]["vounum1"] = "";
            //        dt1.Rows[j]["voudat1"] = "";

            //    }
            //    if (dt1.Rows[j]["vounum1"].ToString().Trim() == "BALANCE")
            //    {
            //        dt1.Rows[j]["vounum1"] = "";
            //        dt1.Rows[j]["voudat1"] = "";
            //    }

            //    grp = dt1.Rows[j]["grp"].ToString();
            //    vounum = dt1.Rows[j]["vounum1"].ToString();
            //}

            //this.dgv2.DataSource = dt1;
            //this.dgv2.DataBind();
            ////this.dgv2.Columns[0].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") || (this.chkqty.Checked);
            //this.dgv2.Columns[6].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") && (this.chkqty.Checked);
            //this.dgv2.Columns[7].Visible = (rbtnLedger.SelectedValue.ToString() == "SubLedger") && (this.chkqty.Checked);

        }
        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string date1 = this.txtDateFrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string Narration = (this.rbtnList1.SelectedIndex == 0) ? "" : "WithoutNar";
            DataSet ds1 = new DataSet();
            string withOutOpn = (this.chkwitoutopn.Checked) ? "withoutOpn" : "";
            string spclcode = "%";
            if (rbtnLedger.SelectedValue.ToString() == "SubLedger")
            {
                string rescode = this.ddlConAccResHead.SelectedValue.ToString();

                // ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", mACTCODE, mTRNDAT1, mTRNDAT2, mRESCODE, "", "", "", withOutOpn, spclcode);
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", actcode, date1, date2, rescode, Narration, "", "", withOutOpn, spclcode);


                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Show Data Sub-Ledger ";
                    string eventdesc2 = "Head " + this.ddlConAccHead.SelectedItem.Text.ToString() + " Resource Head : " + " " + this.ddlConAccResHead.SelectedItem.Text + " " + "(From " + date1 + "To " + date2 + " )";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }

            else
            {
                string calltype = (this.chksum.Checked) ? "ACCOUNTSLEDGERSUM" : "ACCOUNTSLEDGER";
                string ltype = "Without Cancel";
                string daywise = this.Checkdaywise.Checked ? "daywise" : "";
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", calltype, actcode, date1, date2, "", Narration, "", ltype, withOutOpn, daywise);

                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Show Data Accounts-Ledger ";
                    string eventdesc2 = "Account's Head " + " " + this.ddlConAccHead.SelectedItem.Text.ToString() + " " + "( From " + date1 + "To " + date2 + " )";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                ////string calltype = (Request.QueryString["RType"].ToString() == "GLedger") ? "ACCOUNTSLEDGERWC" : "ACCOUNTSLEDGER";
                //string ltype = ""; //(Request.QueryString["RType"].ToString() == "GLedger") ? "Without Cancel" : "";
                //ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGER", actcode, date1, date2, "", Narration, "", ltype, "", "");
            }
            return ds1;
        }
        protected void lnkShowSPLedger_Click(object sender, EventArgs e)
        {
            this.ShowDetailLedger();
        }
        private void ShowDetailLedger()
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();
            string frmdate = Convert.ToDateTime(this.txtDateFromSp.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDatetoSp.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlRescode.SelectedValue.ToString();
            string withOutOpn = (this.chkwithoutopen.Checked) ? "withoutOpn" : "";
            string withOutnarra = (this.rbtsplist.SelectedIndex == 0) ? "" : "WithoutNar";
            string acthead = "";
            string consolidate = this.Checkdaywise.Checked ? "Consolidate" : "";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, withOutOpn, acthead, withOutnarra, consolidate, "", "");

            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count==0 )
            {
                this.gvSpledger.DataSource = null;
                this.gvSpledger.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found ..');", true);
                return;
            }
            DataTable dt = HiddenSameDataSp(ds1.Tables[0]);
            DataTable dt1 = BalCalculationSp(dt);
            Session["tblspledger"] = dt1;
            this.gvSpledger.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSpledger.DataSource = dt1;
            this.gvSpledger.DataBind();
            Session["Report1"] = gvSpledger;
            if (dt1.Rows.Count > 0)
            {
                ((HyperLink)this.gvSpledger.HeaderRow.FindControl("hlbtnCBdataExelsp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Data Special Ledger ";
                string eventdesc2 = "Resource Head  " + this.ddlRescode.SelectedItem.Text.ToString() + " ( From " + frmdate + "To " + todate + " )";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }
            //this.FooterCal();
        }
        private DataTable HiddenSameDataSp(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string vounum = dt1.Rows[0]["vounum"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();

            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }
                if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum"].ToString() == vounum))
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["vounum"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {

                        dt1.Rows[j]["actdesc"] = "";
                    }

                    //if (dt1.Rows[j]["vounum"].ToString() == vounum)
                    //{

                    //    dt1.Rows[j]["vounum"] = "";

                    //}
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();
                }
            }
            return dt1;

        }

        private DataTable HiddenSameDataSp02(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string vounum = dt1.Rows[0]["vounum"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();

            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }
                if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum"].ToString() == vounum))
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    //dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["vounum"] = "";
                }
                else
                {                
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();
                }
            }
            return dt1;

        }

        private DataTable BalCalculationSp(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double opnam, dramt, cramt, bbalamt = 0.00;

            string type = this.rbtnLedger.SelectedValue.ToString();
            switch (type)
            {
                case "DetailLedger":
                    bool result = this.Checkdaywise.Checked;
                    switch (result)
                    {
                        case true:

                            foreach (DataRow dr1 in dt.Rows)
                            {
                                if ((dr1["vounum"]).ToString().Trim() == "CURRENT DR/CR" || (dr1["vounum"]).ToString().Trim() == "Total:" || (dr1["vounum"]).ToString().Trim() == "Balance:")
                                    continue;
                                opnam = Convert.ToDouble(dr1["opam"]);
                                dramt = Convert.ToDouble(dr1["dram"]);
                                cramt = Convert.ToDouble(dr1["cram"]);
                                bbalamt = bbalamt + (opnam + dramt - cramt);
                                dr1["clsam"] = bbalamt;
                            }


                            break;


                        default:
                            string actcode = dt.Rows[0]["actcode"].ToString();
                            //string grp=
                            for (int i = 0; i < dt.Rows.Count - 1; i++)
                            {
                                if ((dt.Rows[i]["actcode"]).ToString().Trim() != actcode)
                                {
                                    bbalamt = 0.00;
                                }
                                actcode = dt.Rows[i]["actcode"].ToString();

                                if ((dt.Rows[i]["vounum"]).ToString().Trim() == "CURRENT DR/CR" || (dt.Rows[i]["vounum"]).ToString().Trim() == "SUB TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "Balance:")
                                    continue;



                                //if (((dt.Rows[i]["actcode"]).ToString().Trim()).Length == 12)
                                //{
                                opnam = Convert.ToDouble(dt.Rows[i]["opam"]);
                                dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                                cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                                bbalamt = bbalamt + (opnam + dramt - cramt);
                                dt.Rows[i]["clsam"] = bbalamt;
                                //}


                            }

                            break;



                    }

                    break;


                case "DetailLedger02":
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if ((dr1["vounum"]).ToString().Trim() == "CURRENT DR/CR" || (dr1["vounum"]).ToString().Trim() == "Total:" || (dr1["vounum"]).ToString().Trim() == "Balance:")
                            continue;
                        opnam = Convert.ToDouble(dr1["opam"]);
                        dramt = Convert.ToDouble(dr1["dram"]);
                        cramt = Convert.ToDouble(dr1["cram"]);
                        bbalamt = bbalamt + (opnam + dramt - cramt);
                        dr1["clsam"] = bbalamt;
                    }
                    break;
            }
            return dt;
        }



        protected void gvSpledger_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounum");
                //Label OpAmt = (Label)e.Row.FindControl("lblgvOpAmount");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmount");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmount");
                Label ClAmt = (Label)e.Row.FindControl("lblgvClAmount");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "head1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "AB" || code.Trim() == "04CT")
                {
                    hlink.Font.Bold = true;
                    // OpAmt.Font.Bold = true;
                    DrAmt.Font.Bold = true;
                    CrAmt.Font.Bold = true;
                    ClAmt.Font.Bold = true;
                    hlink.Style.Add("text-align", "right");
                }
            }

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvvounum");
            string voucher = ((HyperLink)e.Row.FindControl("HLgvvounum")).Text.ToString();
            if (voucher.Trim().Length == 14)
            {
                if (ASTUtility.Left(voucher, 2) == "PV" || ASTUtility.Left(voucher, 2) == "DV")
                {
                    hlink1.NavigateUrl = "RptAccVouher02.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
                else
                {
                    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
            }

            //if (voucher.Substring(0,2)=="BC"|| voucher.Substring(0,2)=="BD"|| voucher.Substring(0,2)=="CC"|| voucher.Substring(0,2)=="CD"|| voucher.Substring(0,2)=="JV"|| voucher.Substring(0,2)=="CT")  
            //    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
        }
        
        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14)
            {
                //hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                //hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);


                hlink1.NavigateUrl = mVOUNUM.Substring(0, 2) == "PV" ? ("RptAccVouher02.aspx?vounum=" + mVOUNUM) : ("RptAccVouher.aspx?vounum=" + mVOUNUM);
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            }
        }

        protected void lnkShowsp02_Click(object sender, EventArgs e)
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();
            string frmdate = Convert.ToDateTime(this.txtdatefrmsp02.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDatetosp02.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlResoucesp02.SelectedValue.ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTSUPPLIERLEDGER", resource, frmdate, todate, "", "", "", "", "", "");

            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, "", "", "", "", "", "");
           
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                this.gvspleder02.DataSource = null;
                this.gvspleder02.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found ..');", true);
                return;
            }
            DataTable dt =  HiddenSameDataSp02(ds1.Tables[0]);
            DataTable dt1 = BalCalculationSp(dt);
            Session["tblspledger"] = dt1;
            this.gvspleder02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvspleder02.DataSource = dt1;
            this.gvspleder02.DataBind();
            Session["Report1"] = gvspleder02;
            if (dt1.Rows.Count > 0)
            {
                ((HyperLink)this.gvspleder02.HeaderRow.FindControl("hlbtnCBdataExelsp02")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Data Special Ledger ";
                string eventdesc2 = "Resource Head  " + this.ddlRescode.SelectedItem.Text.ToString() + " ( From " + frmdate + "To " + todate + " )";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompcode();

            if (rbtnLedger.SelectedValue.ToString() == "DetailLedger")
            {
                this.PrintDetailLedger();
            }

            else if (rbtnLedger.SelectedValue.ToString() == "DetailLedger02")
            {
                this.PrintDetailLedger02();
            }

            else
            {
                this.PrintLedger();
            }

            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Account Ledger";
                string eventdesc = rbtnLedger.SelectedValue.ToString() == "DetailLedger" ? "Print Special Ledger" : rbtnLedger.SelectedValue.ToString() == "Ledger" ? " Print Account's Ledger" : " Print Sub-Ledger";
                string eventdesc2 = "From: " + this.txtDateFrom.Text + " To: " + this.txtDateto.Text;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        private void PrintDetailLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompcode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string prjname = this.ddlConAccResHead.SelectedItem == null ? "" : this.ddlConAccResHead.SelectedItem.ToString().Substring(13);
            string prjname2 = this.ddlRescode.SelectedItem == null ? "" : this.ddlRescode.SelectedItem.ToString().Substring(13);

            string fdate = "";
            string tdate = "";
            if (rbtnLedger.SelectedValue == "DetailLedger")
            {
                fdate = Convert.ToDateTime(this.txtDateFromSp.Text).ToString("dd-MMM-yyyy");
                tdate = Convert.ToDateTime(this.txtDatetoSp.Text).ToString("dd-MMM-yyyy");
            }
            else
            {
                fdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
                tdate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            }

            DataTable dt1 = (DataTable)Session["tblspledger"];
            if (dt1 == null)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>();
            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "3101":
                case "3356":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedgerIntect", lst, null, null);
                    break;
                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedger", lst, null, null);
                    break;
            }
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("prjname", "Name : " + prjname2));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + fdate + " To " + tdate + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptaital", "SPECIAL LEDGER REPORT"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintDetailLedger02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompcode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string suppName = this.ddlResoucesp02.SelectedItem == null ? "" : this.ddlResoucesp02.SelectedItem.ToString().Substring(13);
            string fdate = Convert.ToDateTime(this.txtdatefrmsp02.Text).ToString("dd-MMM-yyyy");
            string tdate = Convert.ToDateTime(this.txtDatetosp02.Text).ToString("dd-MMM-yyyy");

            string sscode = ASTUtility.Left(this.ddlResoucesp02.SelectedValue.ToString(), 2); ;
            string txthead = "";
            switch (sscode)
            {
                case "99":
                    txthead = "- SUPPLIER";
                    break;
                case "98":
                    txthead = "- SUBCONTRACTOR";
                    break;
                default:
                    txthead = "";
                    break;
            }
            DataTable dt1 = (DataTable)Session["tblspledger"];
            // head1 ="AB" or "04CT" can be apply in rdlc
            var list = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>();
            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "3101":
                case "3356":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedger02Intech", list, null, null);
                    break;
                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedger02", list, null, null);
                    break;
            }
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + fdate + " To " + tdate + ")"));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "SPECIAL LEDGER " + txthead));
            Rpt1.SetParameters(new ReportParameter("suppName", "Resource Name: " + suppName));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", txtuserinfo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string ComLedger()
        {

            string comcod = this.GetCompcode();
            string comledger = "";
            switch (comcod)
            {

                //case "3101":
                case "3337"://Suvastu
                case "3339"://Tropical
                case "3336"://Suvastu                       
                    comledger = "LedgerSuvTropical";
                    break;

                case "1103"://Tanvir Constructions Ltd.          
                    comledger = "LedgerTanvir";
                    break;

                //case "3101":
                //case "3333":
                //    comledger = "LedgerAlli";
                //    break;


                case "3330":
                    comledger = "LedgerBridge";
                    break;

                case "3344":
                    //case "3336":
                    comledger = "LedgerTerranova";
                    break;

                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    comledger = "LedgerRupayan";
                    break;


                case "3357":
                    //case "3101":
                    comledger = "LedgerCube";
                    break;

                case "3356":
                //case "3101":
                    comledger = "LedgerIntech";
                    break;

                default:
                    comledger = "LedgerGen";
                    break;




            }

            return comledger;

        }

        private void PrintLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["StoreTable"];
            DataTable dt1 = dt.Copy();
            DataTable dt2 = HiddenSameDataRpt(dt1);
            DataTable dt3 = DeleteSomeData(dt2);



            ReportDocument rptstk = new ReportDocument();

            string Headertitle = (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "19") ? "Cash/Bank Book"
               : (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "29") ? "Cash/Bank Book"
               : (this.rbtnLedger.SelectedValue.ToString() == "SubLedger") ? "Subsidary Ledger" : "Ledger";
            string daterange = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            string Resdesc = "";
            if (this.rbtnLedger.SelectedValue.ToString() == "SubLedger")
            {
                //Resdesc = this.ddlConAccResHead.SelectedItem.Text.Substring(13);
                Resdesc = this.ddlConAccResHead.SelectedItem == null ? "" : this.ddlConAccResHead.SelectedItem.ToString().Substring(13);

            }

            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            string comledger = this.ComLedger();

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            var lst2 = dt3.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();


            LocalReport Rpt1 = new LocalReport();

            if (comledger == "LedgerSuvTropical")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptLedger", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }
            else if (comledger == "LedgerTanvir")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptLedgerTanvir", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }


            else if (comledger == "LedgerBridge")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerBridge", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }

            //else if (comledger == "LedgerAlli")
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptAccLedgerAlli();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            //}
            else if (comledger == "LedgerTerranova")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerTerra", lst, null, null);
                Rpt1.EnableExternalImages = true;

            }

            else if (comledger == "LedgerRupayan")
            {

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerRup", lst, null, null);
                Rpt1.EnableExternalImages = true;

            }

            else if (comledger == "LedgerCube")
            {
                string checkby = "Checked/Recommended By";
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerCube", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtcheckedby", checkby));
            }

            else if (comledger == "LedgerIntech")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerIntech", lst2, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtcheckedby", "Recommended By"));

            }
            else
            {
                if (this.chkqty.Checked)
                {
                    string checkby = (comcod == "3340") ? "Checked By" : "Recommended By";
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerWqty", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtcheckedby", checkby));
                }
                else
                {
                    string checkby = (comcod == "3340") ? "Checked By" : "Recommended By";
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedger", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtcheckedby", checkby));
                }
                

            }
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("resdes", Resdesc));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private DataTable HiddenSameDataRpt(DataTable dt1) 
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string cactdesc = "", venar1 = "", venar2 = "" , resdesc = ""; 
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["cactcode"].ToString() == "Narration:  ")
                {
                    cactdesc = dt1.Rows[j-1]["cactdesc"].ToString();
                    resdesc = dt1.Rows[j-1]["resdesc"].ToString();
                    venar1 = dt1.Rows[j]["venar1"].ToString();
                    venar2 = dt1.Rows[j]["venar2"].ToString();
                    dt1.Rows[j-1]["cactdesc"] = cactdesc +" , " + resdesc + "\n" + venar1 + " ," + venar2;
                }
                //else
                //{
                //    DataRow dr = dt1.Rows[j];
                //    if (dt1.Rows[j]["cactcode"].ToString() == "Narration:  ")
                //    {
                //        dt1.Rows.Remove(dr);
                //    }
                //}
            }
            return dt1;

        }

        private DataTable DeleteSomeData(DataTable dt1)
        {
            for (int i = dt1.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt1.Rows[i];
                if (dt1.Rows[i]["cactcode"].ToString() == "Narration:  ")
                {
                    dt1.Rows.Remove(dr);
                }
            }
            return dt1;
        }


        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ibtnFindRes_Click(null, null);
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            string type = this.rbtnLedger.SelectedValue.ToString();
            switch (type)
            {

                case "Ledger":
                    this.dgv2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    //this.dgv2.DataBind();
                    this.Data_BindLedger();
                    break;
                case "SubLedger":

                    this.dgv2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    //this.dgv2.DataBind();
                    this.Data_BindSubsidiaryLedger();

                    break;

                case "DetailLedger":
                    this.gvSpledger.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    //this.gvSpledger.DataBind();
                    this.Data_Bind();
                    break;


                case "DetailLedger02":
                    this.gvspleder02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    //this.gvSpledger.DataBind();
                    this.Data_Bind();
                    break;







            }
        }

        protected void gvSpledger_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSpledger.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblspledger"];
            string type = this.rbtnLedger.SelectedValue.ToString();

            switch (type)

            {
                case "DetailLedger":
                    if (dt.Rows.Count > 0)
                    {
                        this.gvSpledger.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvSpledger.DataSource = dt;
                        this.gvSpledger.DataBind();
                    }
                    else
                    {
                        this.gvSpledger.DataSource = null;
                        this.gvSpledger.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found..');", true);
                        return;
                    }
                    break;


                case "DetailLedger02":
                    if (dt.Rows.Count > 0)
                    {
                        this.gvspleder02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvspleder02.DataSource = dt;
                        this.gvspleder02.DataBind();
                    }
                    else
                    {
                        this.gvspleder02.DataSource = null;
                        this.gvspleder02.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found..');", true);
                        return; 
                    }
                    break;
            }
        }


        private void Data_BindLedger()
        {
            DataTable dt = (DataTable)Session["StoreTable"];

            if (dt.Rows.Count > 0)
            {
                this.dgv2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.dgv2.DataSource = dt;
                this.dgv2.DataBind();

            }
            else
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
        }


        private void Data_BindSubsidiaryLedger()
        {
            DataTable dt = (DataTable)Session["StoreTable"];

            if (dt.Rows.Count > 0)
            {
                this.dgv2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.dgv2.DataSource = dt;
                this.dgv2.DataBind();

            }
            else
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
        }




        protected void dgv2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string type = this.rbtnLedger.SelectedValue.ToString();
            switch (type)
            {

                case "Ledger":
                    this.dgv2.PageIndex = e.NewPageIndex;
                    //this.dgv2.DataBind();
                    this.Data_BindLedger();
                    break;


                case "SubLedger":

                    this.dgv2.PageIndex = e.NewPageIndex;
                    //this.dgv2.DataBind();
                    this.Data_BindSubsidiaryLedger();

                    break;

            }

            //this.dgv2.PageIndex = e.NewPageIndex;
            //this.Data_Bind();
        }

        protected void gvspleder02_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounumsp02");
                //Label OpAmt = (Label)e.Row.FindControl("lblgvOpAmount");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmountsp02");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmountsp02");
                Label ClAmt = (Label)e.Row.FindControl("lblgvClAmountsp02");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "head1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "AB" || code.Trim() == "04CT")
                {
                    hlink.Font.Bold = true;
                    // OpAmt.Font.Bold = true;
                    DrAmt.Font.Bold = true;
                    CrAmt.Font.Bold = true;
                    ClAmt.Font.Bold = true;
                    hlink.Style.Add("text-align", "right");
                }
            }

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvvounumsp02");
            string voucher = ((HyperLink)e.Row.FindControl("HLgvvounumsp02")).Text.ToString();
            if (voucher.Trim().Length == 14)
            {
                if (ASTUtility.Left(voucher, 2) == "PV" || ASTUtility.Left(voucher, 2) == "DV")
                {
                    hlink1.NavigateUrl = "RptAccVouher02.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
                else
                {
                    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
            }

        }

        protected void gvspleder02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvspleder02.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}
