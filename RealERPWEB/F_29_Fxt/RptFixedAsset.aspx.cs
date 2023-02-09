using System;
using System.Collections;
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

namespace RealERPWEB.F_29_Fxt
{
    public partial class RptFixedAsset : System.Web.UI.Page
    {
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

                this.txtDateFrom.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtcal.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetDepartment();
                this.GetMatlist();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";

            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "fxtgdesc";
            this.ddlProjectName.DataValueField = "fxtgcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
        private void GetMatlist()
        {

            string comcod = this.GetComeCode();
            string filter = this.txtSrchmat.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXASSTLIST", filter, "", "", "", "", "", "", "", "");
            this.ddllMatList.DataSource = ds1.Tables[0];
            this.ddllMatList.DataTextField = "sirdesc1";
            this.ddllMatList.DataValueField = "sircode";
            this.ddllMatList.DataBind();
        }


        protected void lnkdetail_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        private void ShowData()
        {

            Session.Remove("tblFxtasst");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string dept = ASTUtility.Right(this.ddlProjectName.SelectedValue.ToString(),4);
            //string dept = ASTUtility.Right(this.ddlProjectName.SelectedValue.ToString(),4) + "%";
            string Rescode = (this.ddllMatList.SelectedValue.Substring(0, 2) == "00" ? "2[1-2]" : this.ddllMatList.SelectedValue.ToString()).Substring(0, 4) + "%";
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string caldate = Convert.ToDateTime(this.txtcal.Text).ToString("dd-MMM-yyyy");
            string dept = this.ddlProjectName.SelectedValue.ToString();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "DATEWISFIXASST", Rescode, frmdate, todate, caldate, dept, "", "", "", "");
            if (ds2 == null)
            {

                this.gvFixAsst.DataSource = null;
                this.gvFixAsst.DataBind();
                return;
            }

            Session["tblFxtasst"] = this.HiddenSameData(ds2.Tables[0]);

            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["sirdesc"] = "";

                }

                rsircode = dt1.Rows[j]["rsircode"].ToString();

            }
            return dt1;

        }
        private void Data_Bind()
        {
            this.gvFixAsst.DataSource = Session["tblFxtasst"];
            this.gvFixAsst.DataBind();
        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblFxtasst"];
            var list = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.FixedAssetRegister>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptFixedAsstDate", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Asset Register"));
            Rpt1.SetParameters(new ReportParameter("txtAsset", this.ddllMatList.SelectedItem.Text.Trim().Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtDate", " (From " + this.txtDateFrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + ")"));
            Rpt1.SetParameters(new ReportParameter("txtProject", this.ddlProjectName.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ibtnFindlist_Click(object sender, EventArgs e)
        {
            this.GetMatlist();
        }
    }
}