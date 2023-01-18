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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccPaySlip : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "PAY SLIP INFORMATION ";
                //this.Master.Page.Title = "PAY SLIP INFORMATION ";
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDateFrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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
            string date1 = this.txtDateFrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string rescode = ((this.ddlResource.Items.Count == 0) ? "" : this.ddlResource.SelectedValue.ToString()) + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "RPTPAYSLIP", actcode, date1, date2, rescode, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvpayslip.DataSource = null;
                this.gvpayslip.DataBind();
                return;
            }
            Session["tblpayslip"] = HiddenSameData(ds1.Tables[0]);
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
            this.gvpayslip.DataSource = (DataTable)Session["tblpayslip"];
            this.gvpayslip.DataBind();

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


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptPaySlip();
            DataTable dt = (DataTable)Session["tblpayslip"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtcomAddress = rptstk.ReportDefinition.ReportObjects["txtcomAddress"] as TextObject;
            txtcomAddress.Text = comadd;

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            rpttxtAccDesc.Text = this.ddlConAccHead.SelectedItem.ToString().Substring(13);

            TextObject txtDetsailhead = rptstk.ReportDefinition.ReportObjects["txtDetsailhead"] as TextObject;
            txtDetsailhead.Text = (this.ddlResource.Items.Count == 0) ? "" : this.ddlResource.SelectedItem.Text.Substring(13);



            TextObject rpttxtcurdate = rptstk.ReportDefinition.ReportObjects["txtcurdate"] as TextObject;
            rpttxtcurdate.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
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
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
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