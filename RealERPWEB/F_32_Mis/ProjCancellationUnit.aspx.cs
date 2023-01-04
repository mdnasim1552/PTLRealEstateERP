using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_32_Mis
{
    public partial class ProjCancellationUnit : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Cancellation Summary";

                //this.txtfromDate.Text = System.DateTime.Today.ToString("01-"+"MMM-yyyy");
                //this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromDate.Text = "01" + date.Substring(2);
                //this.txttodate.Text = Convert.ToDateTime(this.txtfromDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.ImgbtnFindProjind_Click(null, null);
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.RtpPrjCancellationUnit();

        }

        protected void ImgbtnFindProjind_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 0;
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_CANCELLATION_UNIT", "GETPROJECTNAME", "", "%");
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectInd.DataSource = dt1;
            this.ddlProjectInd.DataTextField = "actdesc1";
            this.ddlProjectInd.DataValueField = "actcode";
            this.ddlProjectInd.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = this.txtfromDate.Text.ToString();
            string todate = this.txttoDate.Text.ToString();

            string actcode = this.ddlProjectInd.SelectedValue.ToString().Substring(0, 12) == "240000000000" ? "24%" : this.ddlProjectInd.SelectedValue.ToString();
            string asondate = this.chkasondate.Checked ? "asondate" : "";
            DataSet ds2 = accData.GetTransInfo(comcod, "dbo.SP_REPORT_CANCELLATION_UNIT", "CANCELLATIONUNIT", actcode, frmdate, todate, asondate);
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvPrjCancellation.DataSource = null;
                this.gvPrjCancellation.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjCancel"] = dt;
            this.gvPrjCancellation.DataSource = dt;
            this.gvPrjCancellation.DataBind();


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvPrjCancellation.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["Report1"] = gvPrjCancellation;
            ((HyperLink)this.gvPrjCancellation.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["prjname"] = "";
                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                }
            }
            return dt1;

        }


        private void RtpPrjCancellationUnit()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comnam"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();

            DataTable dt1 = (DataTable)Session["tblprjCancel"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjCancellationUnit>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjCancellationUnit", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("date1", "Date: " + Convert.ToDateTime(this.txtfromDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Cancellation Summary"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvPrjCancellation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("lgvcustomername");
            string mCOMCOD = comcod;
            string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();
            string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString().Trim();
            string fromdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "opdate")).ToString("dd-MMM-yyyy");
            string todate = this.txtfromDate.Text;
            //string mACTCODE = ((Label)e.Row.FindControl("lblgvcode")).Text;
            //string mACTDESC = ((Label)e.Row.FindControl("lblgvAcDesc")).Text;



            //string mTRNDAT1 = "";
            //string mTRNDAT2 = "";
            ////------------------------------//////
            //Label actcode = (Label)e.Row.FindControl("lblgvcode");
            //HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
            //Label lblgvopndramt = (Label)e.Row.FindControl("lblgvopndramt");
            //Label lblgvopncramt = (Label)e.Row.FindControl("lblgvopncramt");
            //Label lblgvDramt = (Label)e.Row.FindControl("lblgvDramt");
            //Label lblgvCramt = (Label)e.Row.FindControl("lblgvCramt");
            //Label lblgvclodramt = (Label)e.Row.FindControl("lblgvclodramt");
            //Label lblgvclocramt = (Label)e.Row.FindControl("lblgvclocramt");
            //Label lblgvnetamt = (Label)e.Row.FindControl("lblgvnetamt");


            //string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            //string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();

            hlink1.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + actcode + "&rescode=" + rescode + "&spclcode=%&Date1=" + fromdate + "&Date2=" + todate + "&opnoption=";

        }

        protected void chkasondate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkasondate.Checked)
            {
                txttoDate.Visible = false;
                lbltodate.Visible = false;
            }
            else 
            {
                txttoDate.Visible = true;
                lbltodate.Visible = true;
            }
        }
    }
}