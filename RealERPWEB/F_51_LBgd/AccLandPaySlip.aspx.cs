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
namespace RealERPWEB.F_51_LBgd
{
    public partial class AccLandPaySlip : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PAY SLIP INFORMATION ";
                this.GetAccCode();

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetAccCode()
        {


            string comcod = this.GetComeCode();
            string filter = this.txtAccSearch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD01", filter, "", "", "", "", "", "", "", "");
            this.ddlConAccHead.DataSource = ds1.Tables[0];
            this.ddlConAccHead.DataTextField = "actdesc1";
            this.ddlConAccHead.DataValueField = "actcode";
            if (this.Request.QueryString["prjcode"].ToString().Length > 0)
            {
                this.ddlConAccHead.SelectedValue = this.Request.QueryString["prjcode"].ToString();
            }

            this.ddlConAccHead.DataBind();
            ViewState["HeadAcc1"] = ds1.Tables[0];

            DataTable dt01 = ds1.Tables[0];
            string search1 = this.ddlConAccHead.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;
            if (dr1[0]["actelev"].ToString() == "2")
            {

                this.GetResCode();
            }
            else
            {

                this.ddlResource.Items.Clear();
            }
            ds1.Dispose();
        }


        private void GetResCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string filter1 = "%" + this.txtSearchRes.Text.Trim() + "%";
            DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", actcode, filter1, "", "", "", "", "", "", "");
            DataTable dt3 = ds3.Tables[0];
            Session["HeadRsc1"] = ds3.Tables[0];

            this.ddlResource.DataTextField = "resdesc1";
            this.ddlResource.DataValueField = "rescode";
            this.ddlResource.DataSource = dt3;
            if (this.Request.QueryString["sircode"].ToString().Length > 0)
            {
                this.ddlResource.SelectedValue = this.Request.QueryString["sircode"].ToString();
            }
            this.ddlResource.DataBind();
        }
        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {


            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = this.ddlConAccHead.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;
            if (dr1[0]["actelev"].ToString() == "2")
            {

                this.GetResCode();
            }
            else
            {

                this.ddlResource.Items.Clear();
            }

        }

        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            this.GetAccCode();
        }
        protected void IbtnResource_Click(object sender, EventArgs e)
        {
            this.GetResCode();

        }
        protected void lnkShowLedger_Click(object sender, EventArgs e)
        {
            Session.Remove("tblpayslip");
            string comcod = this.GetComeCode();
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string rescode = this.ddlResource.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "RPTPAYSLIPLAND", actcode, rescode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rppayslip.DataSource = null;
                this.rppayslip.DataBind();
                return;
            }
            Session["tblpayslip"] = ds1.Tables[0];
            this.Data_Bind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account PaySlip";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void Data_Bind()
        {
            this.rppayslip.DataSource = (DataTable)Session["tblpayslip"];
            this.rppayslip.DataBind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                {

                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    //dt1.Rows[j]["refnum"] = "";
                }

                if (dt1.Rows[j]["vounum1"].ToString().Trim() == "TOTAL")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";

                }
                if (dt1.Rows[j]["vounum1"].ToString().Trim() == "BALANCE")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                }

                grp = dt1.Rows[j]["grp"].ToString();
                vounum = dt1.Rows[j]["vounum1"].ToString();
            }

            return dt1;


        }



        private string companytype()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string payslip = "";
            switch (comcod)
            {

                case "3325":
                case "2325":
                    payslip = "paysliplei";
                    break;

                default:
                    payslip = "payslipgen";
                    break;
            }
            return payslip;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblpayslip"];
            string type = this.companytype();
            ReportDocument rptstk = new ReportDocument();

            if (type == "paysliplei")
            {
                rptstk = new RealERPRPT.R_51_LBgd.RptAccPaySlipLei();
            }

            else
            {
                rptstk = new RealERPRPT.R_51_LBgd.RptAccPaySlip();
            }

            //ReportDocument rptstk = new RealERPRPT.R_51_LBgd.RptAccPaySlip();



            double tocost = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tocostam)", "")) ? 0.00 : dt.Compute("sum(tocostam)", "")));

            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtrptproject = rptstk.ReportDefinition.ReportObjects["txtrptproject"] as TextObject;
            txtrptproject.Text = this.ddlConAccHead.SelectedItem.Text.Substring(18);
            TextObject txtresource = rptstk.ReportDefinition.ReportObjects["txtresource"] as TextObject;
            txtresource.Text = this.ddlResource.SelectedItem.Text.Substring(13);
            TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");

            TextObject txttakainword = rptstk.ReportDefinition.ReportObjects["txttakainword"] as TextObject;
            txttakainword.Text = "In Word: " + ASTUtility.Trans(Math.Round(tocost), 2);

            //txttakainword
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource((DataTable)Session["tblpayslip"]);
            string comcod = this.GetComeCode();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account PaySlip";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvpayslip_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lbldescription");
                Label lbldram = (Label)e.Row.FindControl("lblgvDrAmount");
                Label lblcramt = (Label)e.Row.FindControl("lblgvCrAmount");

                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString().Trim();

                if (vounum == "")
                {
                    return;
                }
                if (vounum == "TOTAL" || vounum == "BALANCE")
                {


                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;

                }

            }
        }


    }
}