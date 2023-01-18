using System;
using System.Collections;
using System.Collections.Generic;
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
namespace RealERPWEB.F_32_Mis
{

    public partial class RptBalSheetSummary : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Balance Sheet Summary";
                //double day = System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.lbtnOk_Click(null, null);


            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            //  string date2 = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_MIS_GRAPH", "GET_MIS_GRAPH_DATA", date1, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvIncomeSt.DataSource = null;
                this.gvIncomeSt.DataBind();
                return;
            }

            DataTable dt = ds2.Tables[0];
            ViewState["tblprjtbl"] = dt;

            this.gvIncomeSt.DataSource = dt;
            this.gvIncomeSt.DataBind();
            this.ShowBarChart();
            this.ShowPieChart();





            DataTable drt = (DataTable)ViewState["tblprjtbl"];
            DataView dv = drt.DefaultView;
            dv.RowFilter = ("grp='2'");
            drt = dv.ToTable();


            double noncuram = Convert.ToDouble((Convert.IsDBNull(drt.Compute("sum(noncuram)", "")) ? 0.00 : drt.Compute("sum(noncuram)", "")));
            double curam = Convert.ToDouble((Convert.IsDBNull(drt.Compute("sum(curam)", "")) ? 0.00 : drt.Compute("sum(curam)", "")));
            double equityam = Convert.ToDouble((Convert.IsDBNull(drt.Compute("sum(equityam)", "")) ? 0.00 : drt.Compute("sum(equityam)", "")));

            double noncurlia = Convert.ToDouble((Convert.IsDBNull(drt.Compute("sum(noncurlia)", "")) ? 0.00 : drt.Compute("sum(noncurlia)", "")));
            double curlia = Convert.ToDouble((Convert.IsDBNull(drt.Compute("sum(curlia)", "")) ? 0.00 : drt.Compute("sum(curlia)", "")));






            // Chart Image

            this.txtnoncuram.Text = Math.Round(noncuram, 2).ToString();
            this.txtcuram.Text = Math.Round(curam, 2).ToString();
            this.txtequityam.Text = Math.Round(equityam, 2).ToString();
            this.txtnoncurlia.Text = Math.Round(noncurlia, 2).ToString();
            this.txtcurlia.Text = Math.Round(curlia, 2).ToString();




        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt3 = (DataTable)Session["tblPrjname"];
            if (dt1.Rows.Count == 0)
                return;

            var list = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectIncomeSt>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptPrjIncomeSt", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("projectName", "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString()));
            Rpt1.SetParameters(new ReportParameter("date", "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", ((Label)this.Master.FindControl("lblTitle")).Text));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptPrjIncomeSt();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = ((Label)this.Master.FindControl("lblTitle")).Text;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtfdate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") ;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            //txtprojectname.Text = "Project Name: "+(dt3.Rows[0]["prjsdesc"]).ToString(); 

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void gvIncomeSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
            //    Label DAmount = (Label)e.Row.FindControl("lgvAmt");
            //    Label parcent = (Label)e.Row.FindControl("lgvParcent");

            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (ASTUtility.Right((code), 10) == "0000000000")
            //    {
            //        actdesc.Font.Bold = true;
            //        DAmount.Font.Bold = true;
            //        parcent.Font.Bold = true;
            //        DAmount.Style.Add("text-align", "Left");
            //    }
            //    if (ASTUtility.Right((code), 5) == "99998" || ASTUtility.Right((code), 5) == "99999" || ASTUtility.Right(code, 4) == "AAAA")
            //    {
            //        actdesc.Font.Bold = true;
            //        DAmount.Font.Bold = true;
            //        parcent.Font.Bold = true;
            //        actdesc.Style.Add("text-align", "Right");
            //    }

            //}
        }

        protected void gvIncomeSt_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);



                //Adding Head Office Debit Column
                TableCell cell22 = new TableCell();
                cell22.Text = "ASSETS";
                cell22.ForeColor = System.Drawing.Color.White;
                cell22.BackColor = System.Drawing.Color.ForestGreen;
                cell22.Attributes["style"] = "font-weight:bold; font-size:12px; color:white; ";
                cell22.HorizontalAlign = HorizontalAlign.Center;
                cell22.ColumnSpan = 3;
                gvrow.Cells.Add(cell22);

                TableCell cell23 = new TableCell();
                cell23.Text = "LIABILITIES";
                cell23.ForeColor = System.Drawing.Color.White;
                cell23.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:#993366; color:white; ";
                cell23.HorizontalAlign = HorizontalAlign.Center;
                cell23.ColumnSpan = 3;
                gvrow.Cells.Add(cell23);
                gvIncomeSt.Controls[0].Controls.AddAt(0, gvrow);



            }
        }

        private void ShowBarChart()
        {
            //a.comcod, a.pactcode, a.bgdamt, a.mplan , a.mplanat, a.eamt, a.leamt, a.perontw, a.peronac,  peronlp= a.perontw-a.peronac, pactdesc=isnull(b.acttdesc,'')  
            DataTable drt = (DataTable)ViewState["tblprjtbl"];

            // this.gv01.DataSource = drt;
            // this.gv01.DataBind();



            DataView dv = drt.DefaultView;
            dv.RowFilter = ("grp='2'");
            drt = dv.ToTable();
            Chart1.Series["Series1"].YValueMembers = "noncuram";
            Chart1.Series["Series2"].YValueMembers = "curam";
            Chart1.Series["Series3"].YValueMembers = "equityam";
            Chart1.Series["Series4"].YValueMembers = "noncurlia";
            Chart1.Series["Series5"].YValueMembers = "curlia";







            Chart1.DataSource = drt;
            Chart1.DataBind();
            //   Chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = this.Chart1.dataCharting.ChartDashStyle.Dash;
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series2"].IsValueShownAsLabel = true;
            Chart1.Series["Series3"].IsValueShownAsLabel = true;
            Chart1.Series["Series4"].IsValueShownAsLabel = true;
            Chart1.Series["Series5"].IsValueShownAsLabel = true;





            Chart1.Series["Series1"].LabelAngle = 90;
            Chart1.Series["Series1"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series1"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series1"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series1"]["PointWidth"] = "2";
            Chart1.Series["Series1"]["BorderWidth "] = "3";
            // Chart1.Series["Series1"]["groupPadding"] = "2";

            Chart1.Series["Series2"].LabelAngle = 90;
            Chart1.Series["Series2"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series2"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series2"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series2"]["PointWidth"] = "2";
            Chart1.Series["Series2"]["BorderWidth "] = "3";
            //  Chart1.Series["Series1"]["groupPadding"] = "2";

            Chart1.Series["Series3"].LabelAngle = 90;
            Chart1.Series["Series3"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series3"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series3"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series3"]["PointWidth"] = "2";
            Chart1.Series["Series3"]["BorderWidth "] = "3";


            Chart1.Series["Series4"].LabelAngle = 90;
            Chart1.Series["Series4"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series4"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series4"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series4"]["PointWidth"] = "2";
            Chart1.Series["Series4"]["BorderWidth "] = "3";


            Chart1.Series["Series5"].LabelAngle = 90;
            Chart1.Series["Series5"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series5"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series5"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series5"]["PointWidth"] = "2";
            Chart1.Series["Series5"]["BorderWidth "] = "3";





            // Chart1.Series["Series15"].Color = System.Drawing.Color.FromArgb(0, 128, 0);


            // System.Drawing.Color.FromArgb(255, 0, 0);

        }


        private void ShowPieChart()
        {
            //DataTable drt = (DataTable)ViewState["tblprjtbl"];

            //// this.gv01.DataSource = drt;
            //// this.gv01.DataBind();



            //DataView dv = drt.DefaultView;
            //dv.RowFilter = ("grp='2'");
            //drt = dv.ToTable();

            //chrtpie.Series["Series1"].YValueMembers = "noncuram";
            //chrtpie.Series["Series1"].YValueMembers = "curam";
            //chrtpie.DataSource = drt;
            //chrtpie.DataBind();

        }
    }
}