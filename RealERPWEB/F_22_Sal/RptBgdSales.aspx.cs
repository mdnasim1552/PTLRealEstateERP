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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptBgdSales : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // Session.Remove("Unit");

                //this.HeaderText.Text = (type == "soldunsold" ? "Sold and Unsold Informaton " :(type == "parking" ? "Parking Information ":  "Day Wise Sales Information " ));
                //this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "BUDGETED SALES";

                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);

                }

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

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
            if (this.Request.QueryString["prjcode"].Length > 0)
            {
                string pactcode = this.Request.QueryString["prjcode"].Substring(2, 10);
                this.ddlProName.SelectedValue = "18" + pactcode;
            }

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.BGDSales();
        }

        private void BGDSales()
        {
            Session.Remove("tbBgdSal");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProName.SelectedValue.ToString();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "BUDGETSALES", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgdSales.DataSource = null;
                this.gvBgdSales.DataBind();
                return;
            }
            Session["tbBgdSal"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Budgeted Sales Report";
                string eventdesc = "Show Report";
                string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbBgdSal"];
            this.gvBgdSales.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvBgdSales.DataSource = dt;
            this.gvBgdSales.DataBind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string grpcod = dt1.Rows[0]["grpcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grpcod"].ToString() == grpcod)
                {
                    grpcod = dt1.Rows[j]["grpcod"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grpcod = dt1.Rows[j]["grpcod"].ToString();
                }

            }

            return dt1;
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["tbBgdSal"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BgdSales>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptBgdSales", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + this.ddlProName.SelectedItem.ToString()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Sales"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            // ** Iqbal Nayan **
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new RealERPRPT.R_22_Sal.rptBgdSales();
            //DataTable dt1 = (DataTable)Session["tbBgdSal"];
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //txtprojectname.Text = this.ddlProName.SelectedItem.Text;

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Budgeted Sales Report";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvBgdSales.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        protected void gvBgdSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdSales.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBgdSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label flrdesc = (Label)e.Row.FindControl("lgcFlDesc");
            Label USize = (Label)e.Row.FindControl("lgvUSize");
            Label UAmt = (Label)e.Row.FindControl("lgvUAmt");
            Label UQty = (Label)e.Row.FindControl("lgvUQty");
            Label PQty = (Label)e.Row.FindControl("lgvPQty");
            Label OChr = (Label)e.Row.FindControl("lgvOChr");
            Label TAmt = (Label)e.Row.FindControl("lgvTAmt");
            string fcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
            //if (code == "")
            //{
            //    return;
            //}
            //if (ASTUtility.Right(code, 5) == "00000") 
            //{
            //    flrdesc.Font.Bold = true;
            //}
            if (fcode == "")
            {
                return;
            }
            if (ASTUtility.Right(fcode, 5) == "AAAAA")
            {
                flrdesc.Font.Bold = true;
                USize.Font.Bold = true;
                UAmt.Font.Bold = true;
                UQty.Font.Bold = true;
                PQty.Font.Bold = true;
                OChr.Font.Bold = true;
                TAmt.Font.Bold = true;

                flrdesc.Style.Add("text-align", "right");
            }
        }
    }
}

