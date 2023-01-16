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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptPrjWiseMrfHistory : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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


                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Wise MRF (Requisition) History";

                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetProjectName();
            }

        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETPROJECTNAME01", txtSProject, "", "", "", "", "", "", "", "");
            //this.ddlProjectName.DataTextField = "pactdesc";
            //this.ddlProjectName.DataValueField = "pactcode";
            //this.ddlProjectName.DataSource = ds1.Tables[0];
            //this.ddlProjectName.DataBind();

            this.chkProjectName.DataTextField = "pactdesc";
            this.chkProjectName.DataValueField = "pactcode";
            this.chkProjectName.DataSource = ds1.Tables[0];
            this.chkProjectName.DataBind();



        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string pactcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProjectName.SelectedValue.ToString()+"%";

            string pactcode = "";
            string gp = this.chkProjectName.SelectedValue.Trim();
            if (gp.Length > 0)
            {
                if (gp.Trim() == "000000000000" || gp.Trim() == "")
                    pactcode = "";
                else
                    foreach (ListItem s1 in chkProjectName.Items)
                    {
                        if (s1.Selected)
                        {
                            pactcode = pactcode + s1.Value.Substring(0, 12);
                        }
                    }

            }

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTMRFHISCOUNTING", pactcode, fromdate, todate, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvmrfstatus.DataSource = null;
                this.gvmrfstatus.DataBind();
                return;

            }

            Session["tblstatus"] = ds1.Tables[0];

            this.Data_Bind();


        }

        private void Data_Bind()
        {
            this.gvmrfstatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvmrfstatus.DataSource = (DataTable)Session["tblstatus"];
            this.gvmrfstatus.DataBind();

            this.FooterCalculation();


        }



        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvmrfstatus.FooterRow.FindControl("lgvFApprQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reqqty)", "")) ? 0.00 :
                 dt.Compute("sum(reqqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvmrfstatus.FooterRow.FindControl("lgvFprocess")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(procesqty)", "")) ? 0.00 :
                dt.Compute("sum(procesqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvmrfstatus.FooterRow.FindControl("lgvFporder")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(orderqty)", "")) ? 0.00 :
                dt.Compute("sum(orderqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvmrfstatus.FooterRow.FindControl("lgvFmrr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrqty)", "")) ? 0.00 :
                   dt.Compute("sum(mrqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvmrfstatus.FooterRow.FindControl("lgvFbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billqty)", "")) ? 0.00 :
                dt.Compute("sum(billqty)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvmrfstatus;
            ((HyperLink)this.gvmrfstatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        //private void workorderStatus()
        //{


        //    this.lblPage.Visible = true;
        //    this.ddlpagesize.Visible = true;
        //    //this.LoadData();

        //}

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string date1 = "From " + fromdate + " To " + todate;

            DataTable dt = (DataTable)Session["tblstatus"];

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPrjWiseMrfHistory>();
            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptPrjWiseMrfHistory", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("comName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Project Wise MRF(Requisition) History"));
            rpt.SetParameters(new ReportParameter("date1", date1));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


    }
}