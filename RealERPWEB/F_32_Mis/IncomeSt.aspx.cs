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
    public partial class IncomeSt : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "CB") ? "Income Statement (Cash Basis)" : "Income Statement (Project)";
                //double day = System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.ImgbtnFindProjind_Click(null, null);


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

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", "4[1-7]%", filter, "", "", "", "", "", "", "");
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
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string actcode = this.ddlProjectInd.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
            string Calltype = (this.Request.QueryString["Type"] == "CB" && ASTUtility.Left(actcode, 2) == "41") ? "RPTINSTCASHBASIC" :
                (this.Request.QueryString["Type"] == "CB" && ASTUtility.Left(actcode, 2) == "47") ? "RPTINSTCASHBASIC2" : "RPTINSTACTURALBASIC";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", Calltype, "", date1, actcode, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvIncomeSt.DataSource = null;
                this.gvIncomeSt.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("rescode  not like '31%'");
            this.gvIncomeSt.DataSource = dt;
            this.gvIncomeSt.DataBind();

            //Session["tblFooter"] = ds2.Tables[1];
            Session["tblPrjname"] = ds2.Tables[1];



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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label parcent = (Label)e.Row.FindControl("lgvParcent");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    parcent.Font.Bold = true;
                    DAmount.Style.Add("text-align", "Left");
                }
                if (ASTUtility.Right((code), 5) == "99998" || ASTUtility.Right((code), 5) == "99999" || ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    parcent.Font.Bold = true;
                    actdesc.Style.Add("text-align", "Right");
                }

            }
        }
    }
}