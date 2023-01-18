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
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_81_Hrm.F_90_PF
{

    public partial class AccDetailsSchedule : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = " ACCOUNT DETAILS SCHEDULE";
            this.GetDate();
        }
        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtFromdat.Text = startdate + date.Substring(2);
            this.txtTodat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private DataSet GetDataForReport()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtFromdat.Text.Substring(0, 11);
            string date2 = this.txtTodat.Text.Substring(0, 11);
            //string level = this.ddlRptlbl.SelectedItem.Text.Substring(5);
            string TopHead = "dfdsf";//(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAccHeads.SelectedValue.ToString();
            string rescode = this.ddlResHead.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptlbl.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string WzeroBal = (this.chkWiZeroBal.Checked) ? "WZero" : "";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RESSCH02", "RESSCH_REPORT_LEVEL", date1, date2, TopHead, actcode, rescode, mRptGroup, "", "", WzeroBal);

            return ds2;
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.txtFromdat.Text == "" && this.txtTodat.Text == "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please select from date and to date.";
                return;
            }// End If
            if (this.ddlAccHeads.SelectedValue.ToString() == "" && this.ddlResHead.SelectedValue.ToString() == "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please select Accounts Code Or Resource code.";
                return;
            }// End If
            try
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                DataSet ds2 = GetDataForReport();
                Session["tblDetails"] = ds2.Tables[0];
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "There is no resource in this accounts.";
                    this.lblmsg.ForeColor = System.Drawing.Color.Blue;
                    return;
                }
                this.dgv2.DataSource = ds2.Tables[0];
                this.dgv2.DataBind();
                ((Label)this.dgv2.FooterRow.FindControl("lblfopnamt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds2.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ") + "<br>" + "-";
                ((Label)this.dgv2.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ") + "<br>" + "-" + "<br>" + "-";
                ((Label)this.dgv2.FooterRow.FindControl("lblfCramt")).Text = "-" + "<br>" + Convert.ToDouble(ds2.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ") + "<br>" + "-";

                double balamt = (Convert.ToDouble(ds2.Tables[1].Rows[0]["closdram"])) - (Convert.ToDouble(ds2.Tables[1].Rows[0]["closcram"]));

                ((Label)this.dgv2.FooterRow.FindControl("lblfcloamt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds2.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ") + "<br>" + balamt.ToString("#,##0;(#,##0); ");


                Session["Report1"] = dgv2;
                ((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        protected void imgsearch_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string filter = this.txtSearch.Text.Trim() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD02", filter, "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlAccHeads.DataSource = dt1;
                this.ddlAccHeads.DataTextField = "actdesc1";
                this.ddlAccHeads.DataValueField = "actcode";
                this.ddlAccHeads.DataBind();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void imgsrcres_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string filter1 = this.txtSrcRes.Text.Trim() + "%";
                string actcode = this.ddlAccHeads.SelectedValue.ToString();

                DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONRESHEAD01", filter1, actcode, "", "", "", "", "", "", "");
                DataTable dt3 = ds3.Tables[0];
                this.ddlResHead.DataSource = dt3;
                this.ddlResHead.DataTextField = "resdesc1";
                this.ddlResHead.DataValueField = "rescode";
                this.ddlResHead.DataBind();
                //this.GetPriviousVoucher();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dtds = (DataTable)Session["tblDetails"];

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblDetails"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccDetailsSchedul>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptDetailShedule", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtFromdat.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtTodat.Text.Trim()).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Account Details Schedule Report - " + this.ddlRptlbl.SelectedValue.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("Accdesc", "Account Description: " + this.ddlAccHeads.SelectedItem.ToString().Substring(13)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}
