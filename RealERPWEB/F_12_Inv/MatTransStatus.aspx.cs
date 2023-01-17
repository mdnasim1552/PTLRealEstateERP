using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_12_Inv
{
    public partial class MatTransStatus : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        DataTable dttemp = new DataTable();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Materials Transfer Screen";
                this.txtFDate.Text = DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetMaterial();
                this.GetToProject();
                this.GridHeaderName();
            }
        }

        private void GridHeaderName()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {






                case "3370": //CPDL
                    this.grvacc.Columns[3].HeaderText = "MRR No";
                    break;
                default:
                    
                    break;

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
            ds1.Dispose();
        }


        private void GetToProject()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlistto.DataTextField = "pactdesc";
            this.ddlprjlistto.DataValueField = "pactcode";
            this.ddlprjlistto.DataSource = ds1.Tables[0];
            this.ddlprjlistto.DataBind();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }

        private void GetMaterial()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProName.SelectedValue.ToString();
            string txtfindMat = this.txtsrchresource.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "rsirdesc";
            this.DropCheck1.DataValueField = "rsirdesc";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowTransStatus();
        }

        protected void imgbtnFindRefno_Click(object sender, EventArgs e)
        {
            this.ShowTransStatus();

        }



        protected void ShowTransStatus()
        {
            Session.Remove("tblMatTranStatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string dateFrom = this.txtFDate.Text.Trim();
            string dateTo = this.txtToDate.Text.Trim();
            string pactcode = ((this.ddlProName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProName.SelectedValue.ToString()) + "%";
            string SearchRefno = "%" + this.txtSrcRefNo.Text.Trim() + "%";
            string projecto = ((this.ddlprjlistto.SelectedValue.ToString() == "000000000000") ? "" : this.ddlprjlistto.SelectedValue.ToString()) + "%";

            string TrnsTo = (this.chkProjectTrnsTo.Checked) ? "trnsto" : "";
            string rsircode = "";
            string tempsec = this.DropCheck1.Text.Trim().ToString();
            if (tempsec.Length == 0)
            {
                return;
            }
            string[] sec = tempsec.Trim().Split(',');
            if (sec[0].Substring(0, 4) == "0000")
                rsircode = "";
            else
                foreach (string s1 in sec)
                    rsircode = rsircode + s1.Substring(0, 12);

            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_MAT_TRANS", "MATTRANSSTATUS", dateFrom, dateTo, pactcode, SearchRefno, TrnsTo, rsircode, projecto, "", "");
            if (ds.Tables[0].Rows.Count == 0)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            Session["tblMatTranStatus"] = ds.Tables[0];
            this.grvacc_DataBind();

        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.grvacc_DataBind();
        }
        protected void grvacc_DataBind()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataTable tbl1 = (DataTable)Session["tblMatTranStatus"];
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
            if (comcod == "3330")
            {
                grvacc.Columns[2].HeaderText = "TMRR No";
            }

            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.grvacc.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(tamount)", "")) ?
                                        0 : tbl1.Compute("sum(tamount)", ""))).ToString("#,##0;(#,##0); ");
            }

        }



        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string transinfo = this.CompMatTransInfo();

            if (transinfo == "mattransurban")
            {
                this.printMatTransStatusUrban();
            }
            else
            {
                this.printMatTransStatus();
            }

        }

        private string CompMatTransInfo()
        {
            string comcod = this.GetCompCode();
            string companyinfo = "";
            switch (comcod)
            {
                //case "3101":
                case "3336":
                case "3340":  // urban          
                    companyinfo = "mattransurban";
                    break;
                default:
                    companyinfo = "mattrnsinfoall";
                    break;
            }
            return companyinfo;
        }

        private void printMatTransStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = "Transfer From: " + this.txtFDate.Text + "  To: " + this.txtToDate.Text;
            DataTable dt1 = (DataTable)Session["tblMatTranStatus"];
            var list = dt1.DataTableToList<RealEntity.C_12_Inv.EClassMaterial.MatTransStatus>();
            LocalReport rpt = new LocalReport();
            
           
            
            
            string txtTmrrData = "";
           
            switch (comcod)
            {






                case "3370": //CPDL
                    txtTmrrData = "MRR No";
                    break;
                case "3330": //Bridge
                    txtTmrrData = "TMRR No";
                    break;
                default:
                    txtTmrrData="MTRF No";
                    break;

            }




            rpt = RptSetupClass1.GetLocalReport("R_12_Inv.RptMatTransStatus", list, null, null);
            rpt.SetParameters(new ReportParameter("compNam", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Materials Transfer Status"));
            rpt.SetParameters(new ReportParameter("txtDate", date));
            rpt.SetParameters(new ReportParameter("txtMtrf", txtTmrrData));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void printMatTransStatusUrban()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = "Transfer From: " + this.txtFDate.Text + "  To: " + this.txtToDate.Text;

            DataTable dt1 = (DataTable)Session["tblMatTranStatus"];
            var list = dt1.DataTableToList<RealEntity.C_12_Inv.EClassMaterial.MatTransStatus>();
            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass1.GetLocalReport("R_12_Inv.RptMatTransStatusUrban", list, null, null);
            rpt.SetParameters(new ReportParameter("compNam", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Materials Transfer Status"));
            rpt.SetParameters(new ReportParameter("txtDate", date));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();

            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblMatTranStatus"];

            //ReportDocument rptFassttran = new ReportDocument();

            //if (comcod == "3340" || comcod == "3336")
            //{
            //    rptFassttran = new RealERPRPT.R_12_Inv.rptMatTransStatusUrban();

            //}




            //TextObject rptCname = rptFassttran.ReportDefinition.ReportObjects["txtcompant"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptFassttran.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptDate.Text = "Transfer From: " + this.txtFDate.Text + "  To: " + this.txtToDate.Text;

            //TextObject txtuserinfo = rptFassttran.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //// todo for report column header 
            //string txtTmrrData = "MTRF No";

            //if (comcod == "3330")
            //{
            //    txtTmrrData = "TMRR No";
            //}

            //TextObject txtTmrr = rptFassttran.ReportDefinition.ReportObjects["txtTmrr"] as TextObject;
            //txtTmrr.Text = txtTmrrData;


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Transfer Status";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "From " + this.txtFDate.Text + "To " + this.txtToDate.Text;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptFassttran.SetDataSource(dt1);

            //Session["Report1"] = rptFassttran;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }
        protected void lbtnProName_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnresource_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
        protected void btntoproject_Click(object sender, EventArgs e)
        {
            GetToProject();
        }
    }
}
