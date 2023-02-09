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
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccControlSchedule : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();

                //((Label)this.Master.FindControl("lblTitle")).Text = type == "Type01" ? " Account Control Schedule - 01" : type == "Type02" ? "Account Control Schedule - 02" : "ACCOUNT CONTROL SCHEDULE";
                //this.Master.Page.Title = "ACCOUNT CONTROL SCHEDULE";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.imgsearch_Click(null, null);
            }


            if (this.txtFromdat.Text.Trim().Length == 0)
            {
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtFromdat.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtTodat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtFromdat.Text = DateTime.Today.AddDays(-90).ToString("dd-MMM-yyyy ddd");
                //this.txtTodat.Text = DateTime.Today.ToString("dd-MMM-yyyy ddd");
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();

            string date1 = this.txtFromdat.Text.Substring(0, 11);
            string date2 = this.txtTodat.Text.Substring(0, 11);
            string level = this.ddlRptlbl.SelectedItem.Text.Substring(5);
            string TopHead = "dfdsf";//(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAccHeads.SelectedValue.ToString();
            string WzeroBal = (this.chkWiZeroBal.Checked) ? "WZero" : "";
            string WithOutOpn = (this.chkWithoutOpn.Checked) ? "WithOutOpn" : "";





            //string calltype1=this
            string CallType = ((this.Request.QueryString["Type"] == "Type01") ? "CSCH_REPORT_LEVEL01_0" : "CSCH_REPORT_LEVEL02_0") + level;
            //  string CallType =(this.ddlAccHeads.SelectedValue.ToString().Substring(0,2)=="26")?"PROJECTLIABILITIES": (((this.Request.QueryString["Type"] == "Type01") ? "CSCH_REPORT_LEVEL01_0" : "CSCH_REPORT_LEVEL02_0")+ level);
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_CSCH", CallType, date1, date2, TopHead, actcode, "", WzeroBal, "", WithOutOpn, "");


            return ds2;
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.txtFromdat.Text == "" && this.txtTodat.Text == "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please select from date and to date.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }// End If
            if (this.ddlAccHeads.SelectedValue.ToString() == "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please select Accounts Code.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }// End If
            try
            {

                DataSet ds2 = GetDataForReport();
                if (ds2 == null)
                {
                    this.dgv2.DataSource = null;
                    this.dgv2.DataBind();
                    return;
                }
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "There is no Transaction in this Accounts Code.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    this.lblmsg.ForeColor = System.Drawing.Color.Blue;
                    return;
                }



                this.dgv2.DataSource = ds2.Tables[0];
                this.dgv2.DataBind();
                ((Label)this.dgv2.FooterRow.FindControl("lblfopnDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(opndram)", "")) ?
                                0 : ds2.Tables[0].Compute("sum(opndram)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfopnCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(opncram)", "")) ?
                               0 : ds2.Tables[0].Compute("sum(opncram)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(dram)", "")) ?
                               0 : ds2.Tables[0].Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(cram)", "")) ?
                               0 : ds2.Tables[0].Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
                double closdramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(closdram)", "")) ? 0 : ds2.Tables[0].Compute("sum(closdram)", "")));
                double closcramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(closcram)", "")) ? 0 : ds2.Tables[0].Compute("sum(closcram)", "")));
                ((Label)this.dgv2.FooterRow.FindControl("lblfcloDramt")).Text = closdramt.ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfcloCramt")).Text = closcramt.ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfcloNetamt")).Text = (closdramt - closcramt).ToString("#,##0;(#,##0); ");
                Session["Report1"] = dgv2;
                ((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void imgsearch_Click(object sender, EventArgs e)
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();
                string filter = this.txtSearch.Text.Trim() + "%";
                // string calltype=asi
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_CSCH", "GETCONACCHEAD02", filter, "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlAccHeads.DataSource = dt1;
                this.ddlAccHeads.DataTextField = "actdesc1";
                this.ddlAccHeads.DataValueField = "actcode";
                this.ddlAccHeads.DataBind();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string hostname = hst["hostname"].ToString();
            string accdsce = this.ddlAccHeads.SelectedItem.Text.Substring(13).ToString().Trim();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataSet ds2 = GetDataForReport();
            if (ds2 == null)
                return;
            //RDLC convert By Parbaz
            var list = ds2.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.AccControlSchedule01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptAccConSchedule01", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compname", comnam));
            rpt.SetParameters(new ReportParameter("rptTitle", accdsce));
            rpt.SetParameters(new ReportParameter("rptdate", "(From " + Convert.ToDateTime(this.txtFromdat.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtTodat.Text.Trim()).ToString("dd-MMM-yyyy") + ")"));
            rpt.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt.SetParameters(new ReportParameter("txtuserinfo", "Print Source :" + username + " , " + session + " , " + printdate));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvCode")).Text;
            string mACTDesc = ((Label)e.Row.FindControl("lblgvDesc")).Text;
            string mTRNDAT1 = this.txtFromdat.Text;
            string mTRNDAT2 = this.txtTodat.Text;

            if (ASTUtility.Right(mACTCODE, 4) == "0000")
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&actdesc=" + mACTDesc +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            else
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&actdesc=" + mACTDesc +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        }

    }
}
