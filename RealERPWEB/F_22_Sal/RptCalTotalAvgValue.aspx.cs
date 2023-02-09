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
namespace RealERPWEB.F_22_Sal
{
    public partial class RptCalTotalAvgValue : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sold Information";

                this.GetProjectName();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = "01" + this.txtDate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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
            return (hst["comcod"].ToString());
        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAME2", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }

        //protected void imgbtnFindProject_Click(object sender, EventArgs e)
        //{
        //    this.GetProjectName();
        //}
        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument rptcusdues = new RealERPRPT.R_22_Sal.RptCalTValAvgVal();
        //    TextObject rpttxtCompanyName = rptcusdues.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    rpttxtCompanyName.Text = comnam;

        //    TextObject txtuserinfo = rptcusdues.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    rptcusdues.SetDataSource((DataTable)Session["tbsoldinf"]);
        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Sold Info";
        //        string eventdesc = "Print Report Sold Inf";
        //        string eventdesc2 = "";
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rptcusdues.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptcusdues;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
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
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["tbsoldinf"];


            //string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");

            LocalReport Rpt1 = new LocalReport();


            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCalTValAvgVal>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCalTValAvgVal", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //Rpt1.SetParameters(new ReportParameter("txtDate", " (" + "From  " + frmdate + " To " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Calculation of Total Value & Average Value of Sold Units"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        private string Calltype()
        {
            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3101":
                case "3368":

                    Calltype = "CALTOTALAVGVAL02";
                    break;
                default:
                    Calltype = "CALTOTALAVGVAL";
                    break;

            }
            return Calltype;
        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string frmdate = this.txtDate.Text.ToString();
            string todate = this.txttodate.Text.ToString();
            string ProjectCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "000000000000" : this.ddlProjectName.SelectedValue.ToString();
            string Calltype = this.Calltype();
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", Calltype, ProjectCode, frmdate, todate, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.grvsoldinf.DataSource = null;
                this.grvsoldinf.DataBind();
                return;
            }

            Session["tbsoldinf"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();

                }

            }

            return dt1;
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbsoldinf"];
            this.grvsoldinf.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvsoldinf.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            this.grvsoldinf.DataSource = dt;
            this.grvsoldinf.DataBind();
        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }
        protected void grvsoldinf_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvsoldinf.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvsoldinf_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label custname = (Label)e.Row.FindControl("lgacuname");
                Label sftsize = (Label)e.Row.FindControl("lgvsftsize");
                Label aptval = (Label)e.Row.FindControl("lgvaptval");
                Label parkam = (Label)e.Row.FindControl("lgvparkam");
                Label utility = (Label)e.Row.FindControl("lgvutility");
                Label cooparative = (Label)e.Row.FindControl("lgvcooparative");
                Label otham = (Label)e.Row.FindControl("lgvotham");
                Label lgvpersft = (Label)e.Row.FindControl("lgvpersft");


                




                Label tval = (Label)e.Row.FindControl("lgvTVal");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 1) == "B" || ASTUtility.Right(code, 1) == "C")
                {

                    custname.Font.Bold = true;
                    sftsize.Font.Bold = true;
                    aptval.Font.Bold = true;
                    parkam.Font.Bold = true;
                    utility.Font.Bold = true;
                    cooparative.Font.Bold = true;
                    otham.Font.Bold = true;
                    tval.Font.Bold = true;
                    lgvpersft.Font.Bold = true;

                    custname.Style.Add("text-align", "right");


                }

            }
        }
    }
}