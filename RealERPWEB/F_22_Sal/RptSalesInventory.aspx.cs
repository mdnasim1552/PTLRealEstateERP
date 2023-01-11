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
using Microsoft.Reporting.WebForms;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptSalesInventory : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sales Inventory Report";
                this.Load_Project();
                this.Load_Grid();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }
        protected void Load_Project()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "GETPRJNAME", "", "", "", "", "", "", "", "", "");
            Session["projectlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlprojectname.DataTextField = "prjname";
            this.ddlprojectname.DataValueField = "actcode";
            this.ddlprojectname.DataSource = ds1.Tables[0];
            this.ddlprojectname.DataBind();
          

        }


        private void Load_Grid()
        {
            string comcod = this.GetCompCode();
            string prjname = this.ddlprojectname.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlprojectname.SelectedValue.ToString() + "%";
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "RPTSOLDUNSOLDINV", prjname, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblsalesinventory"] = HiddenSameData(ds2.Tables[0]);
            this.grvInvRpt.DataSource = ds2;
            this.grvInvRpt.DataBind();

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {








            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["StoreTable"];
            //ReportDocument rptstk = new ReportDocument();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string userinfo = ASTUtility.Concat(compname, username, printdate);
            //var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInventory>();
            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerBridge", lst, null, null);
            //Rpt1.EnableExternalImages = true;


             //Rpt1 = Server.MapPath("~/Report/RptSalesInv.rdlc");


            //Rpt1.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
            //Rpt1.SetParameters(new ReportParameter("RptTitle", "Sales Inventory Report(Summary)"));
            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));       
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptcusdues = new RealERPRPT.R_22_Sal.RptSalesInv();
            //TextObject rpttxtCompanyName = rptcusdues.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rpttxtCompanyName.Text = comnam;

            //TextObject txtuserinfo = rptcusdues.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptcusdues.SetDataSource((DataTable)Session["tbinvRpt"]);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Sold Info";
            //    string eventdesc = "Print Report Sold Inventory";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptcusdues.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcusdues;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        //private void ShowInfo()
        //{
        //    string comcod = this.GetCompCode();
        //    DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "RPTSOLDUNSOLDINV", "", "", "", "", "", "", "", "", "");
        //    if (ds2 == null)
        //    {
        //        this.grvInvRpt.DataSource = null;
        //        this.grvInvRpt.DataBind();
        //        return;
        //    }

        //    Session["tbinvRpt"] = HiddenSameData(ds2.Tables[0]);

        //    DataTable dt = (DataTable)Session["tbinvRpt"];
        //    this.ReportViewer1.Reset();
        //    this.ReportViewer1.LocalReport.EnableHyperlinks = true;
        //    this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //    this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/RptSalesInv.rdlc");
        //    this.ReportViewer1.LocalReport.Refresh();
        //    this.ReportViewer1.LocalReport.DataSources.Clear();
        //    var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesInventory>();



        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
        //    string userinfo = ASTUtility.Concat(compname, username, printdate);
        //    ReportViewer1.LocalReport.EnableExternalImages = true;
        //    ReportViewer1.LocalReport.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
        //    ReportViewer1.LocalReport.SetParameters(new ReportParameter("RptTitle", "Sales Inventory Report(Summary)"));
        //    ReportViewer1.LocalReport.SetParameters(new ReportParameter("txtuserinfo", userinfo));
        //    ReportViewer1.LocalReport.SetParameters(new ReportParameter("ComLogo", ComLogo));
        //    ReportDataSource rdc = new ReportDataSource("DataSet1", lst.ToList());
        //    ReportViewer1.LocalReport.DataSources.Add(rdc);
        //    ReportViewer1.DataBind();



        //    //ReportParameter p3 = new ReportParameter("pFromDate", pFromDate);
        //    //ReportParameter p4 = new ReportParameter("pFromYear", pFromYear);
        //    //ReportParameter p5 = new ReportParameter("pToDate", pToDate);
        //    //ReportParameter p6 = new ReportParameter("pToYear", pToYear);
        //    //ReportParameter p7 = new ReportParameter("pIsShowYear", model.IsAddYear.ToString());
        //    //ReportParameter p8 = new ReportParameter("pLevel", model.Level.ToString());
        //    //ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8 });


        //    //this.ReportViewer1.LocalReport.Refresh();
        //    //this.ReportViewer1.LocalReport.DataSources = (DataTable)Session["tbinvRpt"];
        //    //this.ReportViewer1.DataBind();


        //    // this.Data_Bind();
        //}
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    //dt1.Rows[j]["pactdesc"] = "";

                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();

                }
            }



            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbinvRpt"];
            this.grvInvRpt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

            this.grvInvRpt.DataSource = dt;
            this.grvInvRpt.DataBind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvInvRpt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Load_Grid();

        }

        protected void grvInvRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvInvRpt.PageIndex = e.NewPageIndex;
            this.Load_Grid();
        }
        protected void grvInvRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvLocation = (Label)e.Row.FindControl("lgvLoc");
                Label lgvTqty = (Label)e.Row.FindControl("lgvuqty");
                Label lgvsqty = (Label)e.Row.FindControl("lgvsold");
                Label lgvMgt = (Label)e.Row.FindControl("lgvMgtBokk");
                Label lgvusqty = (Label)e.Row.FindControl("lgvusldQty");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "loc")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "Total: " || code == "Grand Total: ")
                {

                    lgvLocation.Font.Bold = true;
                    lgvTqty.Font.Bold = true;
                    lgvsqty.Font.Bold = true;
                    lgvMgt.Font.Bold = true;
                    lgvusqty.Font.Bold = true;
                    lgvLocation.Style.Add("text-align", "right");
                }

            }
        }

        protected void prjSearch_Click(object sender, EventArgs e)
        {
            this.Load_Grid();
        }
    }
}