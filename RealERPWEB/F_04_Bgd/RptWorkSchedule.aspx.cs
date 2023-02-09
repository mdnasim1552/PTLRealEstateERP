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
namespace RealERPWEB.F_04_Bgd
{
    public partial class RptWorkSchedule : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "WORK LIST";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.ShowValue();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void ShowValue()
        {

            this.ShowWorkSchd();
        }



        private void ShowWorkSchd()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "RPTWORKSCHD", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvWorkSchd.DataSource = null;
                this.gvWorkSchd.DataBind();
                return;
            }

            Session["tblWoekSchd"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblWoekSchd"];
            this.gvWorkSchd.DataSource = dt;
            this.gvWorkSchd.DataBind();
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            PrintWorkSchd();

        }

        private void PrintWorkSchd()
        {
            // *****Iqbal Nayan    
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblWoekSchd"];
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.WorkList>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptWorkSchd", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProject.SelectedItem.ToString().Substring(14)));
            //Rpt1.SetParameters(new ReportParameter("Resource", this.ddlReports.SelectedItem.Text.Trim()));
            //Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Work List"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblWoekSchd"];

            //ReportDocument rptConPro = new RealERPRPT.R_04_Bgd.RptWorkSchd();
            //TextObject txtuserinfo = rptConPro.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Work Schedule";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptConPro.SetDataSource(dt);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptConPro.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptConPro;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void gvConPro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWorkSchd.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void gvWorkSchd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label sircode = (Label)e.Row.FindControl("lblgvItem");
                HyperLink sirdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label unit = (Label)e.Row.FindControl("lgvUnit");
                Label sval = (Label)e.Row.FindControl("lgvStdQty");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "000" && ASTUtility.Right(code, 5) != "00000")
                {

                    sircode.Font.Bold = true;
                    sirdesc.Font.Bold = true;
                    unit.Font.Bold = true;
                    sval.Font.Bold = true;
                    //actdesc.Style.Add("text-align", "right");


                }

            }


            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string ItemCode = ((Label)e.Row.FindControl("lblgvCode")).Text;
            string Isirdesc = ((HyperLink)e.Row.FindControl("HLgvDesc")).Text;
            string sirunit = ((Label)e.Row.FindControl("lgvUnit")).Text;
            string sirval = ((Label)e.Row.FindControl("lgvStdQty")).Text;

            if (ASTUtility.Right(ItemCode, 3) != "000")
                hlink1.NavigateUrl = "RptBgdStdAna.aspx?rpttype=RptAnaly&isircode=" + ItemCode + "&Isirdesc=" + Isirdesc + "&isirunit=" + sirunit + "&stdqty=" + sirval;
            //else
            //    hlink1.NavigateUrl = "BgdStdAna.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE;

        }
    }
}

