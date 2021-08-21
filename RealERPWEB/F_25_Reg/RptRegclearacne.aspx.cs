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
namespace RealERPWEB.F_25_Reg
{
    public partial class RptRegclearacne : System.Web.UI.Page
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "RECEIVED LIST INFORMATION";

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetregisCode();
                this.ViewSelection();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;



            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetregisCode()
        {

            string comcod = this.GetCompCode();

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALREGISTRATION", "GETREGCODE", "%", "", "", "", "", "", "", "", "");
            this.ddlregistd.DataTextField = "regdesc";
            this.ddlregistd.DataValueField = "regcode";
            this.ddlregistd.DataSource = ds1;
            this.ddlregistd.DataBind();
            ddlregistd.SelectedValue = "AAAAAAA";

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void ViewSelection()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Regiscl":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


            }

        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALREGISTRATION", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            //if (Request.QueryString["prjcode"].Length > 0)
            //{
            //    ddlProjectName.SelectedValue = Request.QueryString["prjcode"].ToString();
            //    ddlProjectName.Enabled = false;
            //}
            ds1.Dispose();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }





        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Regiscl":
                    this.ShowRegisCl();
                    break;


            }



        }
        private void ShowRegisCl()
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "1[38]" : this.ddlProjectName.SelectedValue.ToString()) + "%";

            string regcode = ((this.ddlregistd.SelectedValue.ToString() == "AAAAAAA") ? "" : this.ddlregistd.SelectedValue.ToString()) + "%";

            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALREGISTRATION", "RPTCUSTREGISTATION", ProjectCode, date, regcode, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvRegis.DataSource = null;
                this.gvRegis.DataBind();
                return;
            }
            Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();








        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string regcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "Regiscl":
                    regcode = dt1.Rows[0]["regcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["regcode"].ToString() == regcode)
                        {
                            regcode = dt1.Rows[j]["regcode"].ToString();
                            dt1.Rows[j]["regdesc"] = "";
                        }

                        else
                            regcode = dt1.Rows[j]["regcode"].ToString();
                    }

                    break;

            }
            return dt1;

        }

        private void Data_Bind()
        {
            //try
            //{
            DataTable dt = (DataTable)Session["tblAccRec"];
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {


                case "Regiscl":
                    this.gvRegis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvRegis.DataSource = dt;
                    this.gvRegis.DataBind();
                    this.FooterCalculation();
                    break;


            }





            //  }

            //catch (Exception e) 
            //{
            //}



        }

        private void FooterCalculation()
        {
            DataTable dt = ((DataTable)Session["tblAccRec"]).Copy();
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {


                case "Regiscl":
                    ((Label)this.gvRegis.FooterRow.FindControl("lgvFtocost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(suamt)", "")) ?
                        0.00 : dt.Compute("Sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvRegis.FooterRow.FindControl("lgFEncash")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reconamt)", "")) ?
                        0.00 : dt.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvRegis.FooterRow.FindControl("lgvFinproamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(inproamt)", "")) ?
                   0.00 : dt.Compute("Sum(inproamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvRegis.FooterRow.FindControl("lgvFtoreceivedt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recamt)", "")) ?
                        0.00 : dt.Compute("Sum(recamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvRegis.FooterRow.FindControl("lgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                        0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.gvRegis.FooterRow.FindControl("lgvFdelcharge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
                    0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvRegis.FooterRow.FindControl("lgvFdischarge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(discharge)", "")) ?
                    0.00 : dt.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvRegis.FooterRow.FindControl("lgvFtodelay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(delayadis)", "")) ?
                    0.00 : dt.Compute("Sum(delayadis)", ""))).ToString("#,##0;(#,##0); ");


                    break;
            }
        }




        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }



        protected void gvRegis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRegis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }






        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Regiscl":
                    this.PrintRegisStAllPro();
                    break;


            }


        }





        private void PrintRegisStAllPro()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt1 = (DataTable)Session["tblAccRec"];

            var lst = dt1.DataTableToList<RealEntity.C_24_CC.EClassRegistrationStatusAllPro>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptRegisrationStatusAllPro", lst, null, null);

            string title = "Registration Status - All Project";
            string date = "As On: " + this.txtdate.Text.Trim();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


    }
}











