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
namespace RealERPWEB.F_12_Inv
{
    public partial class RptMatIssueStatus : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                {
                    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                        Response.Redirect("../AcceessError.aspx");
                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                    this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                    ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                    //((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS ISSUE STATUS";

                }
                // Session.Remove("Unit");
                //string type=this.Request.QueryString["Type"].ToString();
                //this.HeaderText.Text = (type == "acc" ? "MATERIALS STOCK REPORT " : "MATERIALS STOCK REPORT INVENTORY");
                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                this.GetProjectName();
                this.GridViewHeaderName();
                this.GetMaterialCode();
            }
        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GridViewHeaderName()
        {

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3370"://CPDL
                case "3101"://PTL
                    this.gvMatIssueStatus.Columns[3].HeaderText = "SIS No";
                    this.gvMatIssueStatus.Columns[4].HeaderText = "SIR No";
                    break;

                default:
                    break;
            
            
            
            }
            
        
        
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
       
        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
        }
        private void GetMaterialCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            DataSet ds3 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETRESOURCE", "%%", "", "", "", "", "", "", "", "");
            this.ddlmatlist.DataTextField = "sirdesc";
            this.ddlmatlist.DataValueField = "sircode";
            this.ddlmatlist.DataSource = ds3.Tables[0];
            this.ddlmatlist.DataBind();
          
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.MatIsuueStatus();
        }

        private void MatIsuueStatus()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = ((this.ddlProName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProName.SelectedValue.ToString()) + "%";
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string refno = "%" + this.txtSrcRefNo.Text.Trim() + "%";
            string rescode = ((this.ddlmatlist.SelectedValue.ToString() == "000000000000") ? ""
              : ((this.ddlmatlist.SelectedValue.ToString().Substring(9) == "000") ? this.ddlmatlist.SelectedValue.ToString().Substring(0,9):  this.ddlmatlist.SelectedValue.ToString())) + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATISSUESTATUS", pactcode, fdate, tdate, refno, rescode, "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatIssueStatus.DataSource = null;
                this.gvMatIssueStatus.DataBind();
                return;
            }

            if(this.Request.QueryString["Type"] == "AmountBasis")
            {
                this.gvMatIssueStatus.Columns[11].Visible = true;
                this.gvMatIssueStatus.Columns[12].Visible = true;
            }
            this.gvMatIssueStatus.Columns[1].Visible = (this.ddlProName.SelectedValue.ToString() == "000000000000") ? true : false;
            Session["tbMatIsuStatus"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
           

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Issue Status";
                string eventdesc = "Show Report:";
                string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string isuno = dt1.Rows[0]["isuno"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["isuno"].ToString() == isuno)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    isuno = dt1.Rows[j]["isuno"].ToString();

                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["isuno1"] = "";
                    dt1.Rows[j]["isurefno"] = "";
                    dt1.Rows[j]["isudat1"] = "";
                    dt1.Rows[j]["dmirfno"] = "";



                }

                else
                {

                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["actdesc"] = "";

                    if (dt1.Rows[j]["isuno"].ToString() == isuno)
                    {
                        dt1.Rows[j]["isuno1"] = "";
                        dt1.Rows[j]["isudat1"] = "";
                    }


                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    isuno = dt1.Rows[j]["isuno"].ToString();


                }

            }

            return dt1;

        }
       
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbMatIsuStatus"];
            this.gvMatIssueStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatIssueStatus.DataSource = dt;
            this.gvMatIssueStatus.DataBind();
            this.FooterCal();

        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tbMatIsuStatus"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvMatIssueStatus.FooterRow.FindControl("lblIssueAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ? 0.00 :
                dt.Compute("sum(isuamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tbMatIsuStatus"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_12_Inv.RptMatIssStatus>();
            LocalReport Rpt1 = new LocalReport();

            if(this.Request.QueryString["Type"] == "AmountBasis")
            {
                switch (comcod)
                {
                    case "3370":
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMatIssueStatusCPDL", lst, null, null);

                        break;
                    default:
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMatIssueStatus", lst, null, null);
                        break;
                }
               
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMatIssueStatusQtyBasis", lst, null, null);
            }
          
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name : " + this.ddlProName.SelectedItem.Text)); //
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "( From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptMatIssueStatus();
            //TextObject rpttxtCompanyName = rptstk.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rpttxtCompanyName.Text = comnam;
            //TextObject rpttxtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text = "( From "+Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy")+" To "+Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") +" )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Stock Information";
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatIssueStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }


        protected void gvMatIssueStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatIssueStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void imgbtnFindRefno_Click(object sender, EventArgs e)
        {
            this.MatIsuueStatus();
        }
    }
}

