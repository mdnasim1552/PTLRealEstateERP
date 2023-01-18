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
    public partial class RptMaterialStock : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                // Session.Remove("Unit");
                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "acc" ? "MATERIALS STOCK REPORT " : "Materials Stock Information(Project Wise)");

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetMaterial();
                this.GetProjectName();
                if (this.Request.QueryString["sircode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);
                }
                CommonButton();
            }
        }
        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }



        private void GetMaterial()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string txtfindMat = this.txtSrcMat.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETMATERIALS", txtfindMat, "", "", "", "", "", "", "", "");
            this.ddlMaterial.DataTextField = "rsirdesc";
            this.ddlMaterial.DataValueField = "rsircode";
            this.ddlMaterial.DataSource = ds1.Tables[0];
            this.ddlMaterial.DataBind();
            this.ddlMaterial.SelectedValue = this.Request.QueryString["sircode"].Length > 0 ? this.Request.QueryString["sircode"] : "";
            ds1.Dispose();


        }

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtsrchPro.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            DataTable dt = ds1.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["pactcode"] = "000000000000";
            dr1["pactdesc"] = "All Project";
            dr1["pactdesc1"] = "000000000000-All Project";
            dt.Rows.Add(dr1);

            this.DropCheck1.DataTextField = "pactdesc1";
            this.DropCheck1.DataValueField = "pactdesc1";
            this.DropCheck1.DataSource = dt;
            this.DropCheck1.Text = "000000000000-All Project";
            this.DropCheck1.DataBind();
        }





        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.MatStock();
        }

        private void MatStock()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rsircode = this.ddlMaterial.SelectedValue.ToString();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            //string calltype = (this.Request.QueryString["Type"].ToString() == "acc") ? "RPTPROJECTSTOCK" : "RPTPROSTOCKINV";
            string pactcode = "";
            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 4) == "0000")
                pactcode = "";
            else
                foreach (string s1 in sec)
                    pactcode = pactcode + s1.Substring(0, 12);
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTINDMATSTOCKPROWISE", rsircode, fdate, tdate, pactcode, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();
                return;
            }
            Session["tbMatStc"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatStock.DataSource = dt;
            this.gvMatStock.DataBind();

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();


            DataTable dt = (DataTable)Session["tbMatStc"];

            string txtMaterial = this.ddlMaterial.SelectedItem.Text;
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            var lst = dt.DataTableToList<RealEntity.C_12_Inv.RptMaterialStock>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptMatProjWise", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));

            Rpt1.SetParameters(new ReportParameter("date", "( From " + fdate + " To " + tdate + " )"));
            Rpt1.SetParameters(new ReportParameter("txtMaterial", "Material : " + txtMaterial));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Individual Material Stock(All Project)"));


            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            // Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.rptMatProjWise();

            ////if (this.Request.QueryString["Type"].ToString() == "inv")
            ////{
            ////    TextObject rptHeader = rptstk.ReportDefinition.ReportObjects["header"] as TextObject;
            ////    rptHeader.Text = "Materials Stock Information(Inventory)";  txtcompany
            ////}
            //TextObject txtcompany = rptstk.ReportDefinition.ReportObjects["txtcompany"] as TextObject;
            //txtcompany.Text = comnam;

            //TextObject txtMaterial = rptstk.ReportDefinition.ReportObjects["txtMaterial"] as TextObject;
            //txtMaterial.Text = this.ddlMaterial.SelectedItem.Text;
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtdate.Text = "( From " + fdate + " To " + tdate + " )";

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report:";
            //    string eventdesc2 = "Project Name: " + this.DropCheck1.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}



            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvMatStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{




            //    HyperLink hlnkgcResDesc = (HyperLink)e.Row.FindControl("hlnkgcResDesc");




            //    string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bldcod")).ToString();
            //    string rsircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcod")).ToString();
            //    string rsirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptdesc1")).ToString();
            //    if (pactcode == "")
            //    {
            //        return;
            //    }

            //    else
            //    {



            //        hlnkgcResDesc.Style.Add("color", "blue");
            //        hlnkgcResDesc.NavigateUrl = "~/F_12_Inv/LinkMatSpeciStock.aspx?pactcode=" + pactcode + "&rsircode=" + rsircode + "&frmdate=" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + "&pactdesc=" + this.ddlProName.SelectedItem.Text + "&rsirdesc=" + rsirdesc;



            //    }

            //}

        }

        protected void lbtnFindMat_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnProName_Click(object sender, EventArgs e)
        {

        }
    }
}

