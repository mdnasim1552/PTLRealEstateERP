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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptSupCreditLimit : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();

                //((Label)this.Master.FindControl("lblTitle")).Text = type == "RptSupCredit" ? "Supplier Overall Position-2" : "Monthly Supplier & Group Wise Payable";
                this.ShowView();

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






        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSupCredit":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }
        }













        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSupCredit":
                    this.RptSupCredit();
                    break;



            }





        }

        //private void RptSupCredit() 
        //{



        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = this.GetCompCode();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");       
        //    ReportDocument rptImp = new RealERPRPT.R_14_Pro.RptSupCreditLimit();
        //    TextObject txtCompanyName = rptImp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    txtCompanyName.Text = comnam;
        //    TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
        //    txtdat.Text = " Date: "+  "As On   " + Date ;
        //    DataTable dt = (DataTable)Session["tblpayst"];
        //    TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
        //    rptImp.SetDataSource(dt);
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rptImp.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptImp;

        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}


        private void RptSupCredit()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd MMMM, yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblpayst"];


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptSupCreditLimit>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSupCreditLimit", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("todate", "As On: " + date));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Supplier Overall Position "));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }








        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSupCredit":
                    this.ShowSupCredit();
                    break;



            }



        }





        private void ShowSupCredit()
        {
            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string supplier = "99%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_SUPPLER", "RPTSUPCREDITLIMIT", date, supplier, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayableStatus.DataSource = null;
                this.gvPayableStatus.DataBind();
                return;
            }


            Session["tblpayst"] = ds1.Tables[0];
            this.Data_Bind();


        }










        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpayst"];



            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSupCredit":

                    this.gvPayableStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPayableStatus.DataSource = dt;
                    this.gvPayableStatus.DataBind();
                    this.FooterCalCulation();

                    break;




            }






        }


        private void FooterCalCulation()
        {

            DataTable dt = ((DataTable)Session["tblpayst"]).Copy();
            if (dt.Rows.Count == 0)
                return;


            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSupCredit":



                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ?
                                  0 : dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFduetopay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netdues)", "")) ?
                                  0 : dt.Compute("sum(netdues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFcrlimit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crlimit)", "")) ?
                                  0 : dt.Compute("sum(crlimit)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFnyetdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nyetdues)", "")) ?
                                 0 : dt.Compute("sum(nyetdues)", ""))).ToString("#,##0;(#,##0); ");
                    break;






            }



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }



        protected void gvPayableStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPayableStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }




    }
}