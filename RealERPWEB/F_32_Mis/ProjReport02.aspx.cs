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
namespace RealERPWEB.F_32_Mis
{
    public partial class ProjReport02 : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //double day = System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.ImgbtnFindProjind_Click(null, null);
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Report 02";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void ImgbtnFindProjind_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = this.txtSearchpIndp.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", "", filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectInd.DataSource = dt1;
            this.ddlProjectInd.DataTextField = "actdesc1";
            this.ddlProjectInd.DataValueField = "actcode";
            this.ddlProjectInd.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblprjtbl");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string actcode = this.ddlProjectInd.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string Type = this.Request.QueryString["RepType"].ToString();

            //string Calltype = ((Type == "IPRJ") ? ((this.rbtnList1.SelectedIndex == 0) ? "RPTINCOMESTATMENTINPRJ" : "RPTINCOMESTATMENTINPRJSUM") : ((this.rbtnList1.SelectedIndex == 0) ? "RPTINCOMESTATMENTACURAL" : "RPTINCOMESTATMENTACURALSUM"));
            string Calltype = "RPT_PROJRPT02";//"RPTINCOMESTATMENTINPRJ";SP_REPORT_ACCOUNTS_IS_BS_R2

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", Calltype, "", date1, actcode, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvPrjRpt.DataSource = null;
                this.gvPrjRpt.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rescode  not like '31%'");
            this.gvPrjRpt.DataSource = dv.ToTable();
            this.gvPrjRpt.DataBind();

            Session["tblFooter"] = ds2.Tables[1];
            Session["tblPrjname"] = ds2.Tables[2];

            double dbtamt = Convert.ToDouble(ds2.Tables[1].Rows[0]["dramt"]);
            double crtamt = Convert.ToDouble(ds2.Tables[1].Rows[0]["cramt"]);
            double netbal = dbtamt - crtamt;

            ((Label)this.gvPrjRpt.FooterRow.FindControl("lgvNbal")).Text = Convert.ToDouble((Convert.IsDBNull(netbal) ?
                      0.00 : netbal)).ToString("#,##0;(#,##0); - ");


            ((Label)this.gvPrjRpt.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(dramt)", "")) ?
                      0.00 : ds2.Tables[1].Compute("Sum(dramt)", ""))).ToString("#,##0;(#,##0); - ");

            ((Label)this.gvPrjRpt.FooterRow.FindControl("lgvFTCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(cramt)", "")) ?
                     0.00 : ds2.Tables[1].Compute("Sum(cramt)", ""))).ToString("#,##0;(#,##0); - ");

            //Session["Report1"] = gvPrjRpt;
            //((HyperLink)this.gvPrjRpt.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            //string RptType = Request.QueryString["RepType"].ToString();
            int j;
            string grpcode;

            grpcode = dt1.Rows[0]["grp"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grpcode)
                {
                    grpcode = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }
                else
                {
                    grpcode = dt1.Rows[j]["grp"].ToString();
                }

            }
            return dt1;
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
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];
            DataTable dt3 = (DataTable)Session["tblPrjname"];

            double dbtamt = Convert.ToDouble(dt2.Rows[0]["dramt"]);
            double crtamt = Convert.ToDouble(dt2.Rows[0]["cramt"]);
            double netbal = dbtamt - crtamt;

            string dram = dbtamt.ToString("#, #,#0; (#, #,#0); ");
            string cram = crtamt.ToString("#, #,#0; (#, #,#0); ");
            string net = netbal.ToString("#, #,#0; (#, #,#0); ");

            if (dt1.Rows.Count == 0)
                return;


            string date = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string rpttitle = "Project Report";
            string txtprojectname = (dt3.Rows[0]["prjsdesc"]).ToString();


            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.RptProjectReport02>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjectReport02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));
            Rpt1.SetParameters(new ReportParameter("txtprojectname", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("dram", dram));
            Rpt1.SetParameters(new ReportParameter("cram", cram));
            Rpt1.SetParameters(new ReportParameter("net", net));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptPrjReport02();//RptProjTrialBalance();
            ////TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
            ////txtCompany.Text = comnam;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtfdate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") ;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            //txtprojectname.Text = (dt3.Rows[0]["prjsdesc"]).ToString(); // prjsdesc   this.ddlProjectInd.SelectedItem.ToString().Trim().Substring(13);

            //TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            //txtdramt.Text = Convert.ToDouble(dt2.Rows[0]["dramt"]).ToString("#, #,#0; (#, #,#0); "); 
            //TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            //txtcramt.Text = Convert.ToDouble(dt2.Rows[0]["cramt"]).ToString("#, #,#0; (#, #,#0); ");

            //TextObject txtnbal = rptstk.ReportDefinition.ReportObjects["txtNBalance"] as TextObject;
            //txtnbal.Text = Convert.ToDouble(netbal).ToString("#, #,#0; (#, #,#0); ");

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvPrjtrbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 4) == "0000")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;

                    DAmount.Style.Add("text-align", "Left");
                    CAmount.Style.Add("text-align", "Left");
                }
                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }

            }
        }
    }
}