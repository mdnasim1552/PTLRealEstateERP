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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using RealERPWEB.Service;

namespace RealERPWEB.F_22_Sal
{

    public partial class RptSaleOpening : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        UserService objUserService = new UserService();


        // WcfServices.WCFService objwcfservice = new WcfServices.WCFService();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.ShowData();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sales Opening -Summary";

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

        private void ShowData()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "SalesSummary":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowSaleSummary();
                    break;
            }
        }

        private void ShowSaleSummary()
        {

            List<EntityClassProject> lst1 = objUserService.ShowProject();

            List<EClassSalesOpening> lst = objUserService.ShowSalesOpening();
            // List<SalesOpening> lst = objwcfservice.ShowSalesOpening();
            Session["tbl"] = lst;


            this.Data_Bind(lst);
        }

        private void Data_Bind(List<EClassSalesOpening> lst)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "SalesSummary":
                    this.rpSaleOpensum.DataSource = lst;
                    this.rpSaleOpensum.DataBind();
                    //this.gvSaleOpensum.DataSource = lst;
                    //this.gvSaleOpensum.DataBind();
                    //this.FooterCalculation(lst);


                    break;
            }
        }



        protected void rpSaleOpensum_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {



            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label amt = ((Label)e.Item.FindControl("lblgvopnamt"));
                string code = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "pactcode")).ToString();

                if (code == "180100010058")
                {

                    amt.Font.Bold = true;

                }
                // DataRowView drv = (DataRowView)e.Item.DataItem;

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {


                List<EClassSalesOpening> lst = (List<EClassSalesOpening>)Session["tbl"];
                ((Label)e.Item.FindControl("lgvFToAmt")).Text = lst.Select(p => p.opnamt).Sum().ToString("#,##0;(#,##0); ");
            }



            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label actdesc = (Label)e.Row.FindControl("lblgvAcDescDet");
            //    Label bgdamt = (Label)e.Row.FindControl("lgvbgdamtDet");
            //    Label acamt = (Label)e.Row.FindControl("lgvacamtDet");
            //    Label diffamt = (Label)e.Row.FindControl("txtgvdiffamtDet");
            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if ((ASTUtility.Right(code, 4) == "AAAA") || (ASTUtility.Right(code, 4) == "BBBB"))
            //    {

            //        actdesc.Font.Bold = true;
            //        bgdamt.Font.Bold = true;
            //        acamt.Font.Bold = true;
            //        diffamt.Font.Bold = true;
            //        // actdesc.Style.Add("text-align", "right");


            //    }

            //}
        }

        private void FooterCalculation(List<EClassSalesOpening> lst)
        {
            double sum = 0.00;
            //foreach (SalesOpening  r in lst)
            //sum=sum+r.opnamt;

            sum = lst.Select(p => p.opnamt).Sum();


            //Label lgvFToAmt = this.rpSaleOpensum.FindControl("lgvFToAmt") as Label;
            //lgvFToAmt.Text= Convert.ToDouble(sum).ToString("#,##0;(#,##0); ");

            //  ((Label)this.rpSaleOpensum.Items.FindControl("lblDateTime")).Text= /* your value */;
            //((Label)this.rpSaleOpensum.Items.foo.FindControl("lgvFToAmt")).Text = Convert.ToDouble(sum).ToString("#,##0;(#,##0); ");
            //((Label)this.rpSaleOpensum.FooterTemplate("lgvFToAmt")).Text = Convert.ToDouble(sum).ToString("#,##0;(#,##0); ");

            //sum= lst.Select(p => p.opnamt).Sum();
            // ((Label)this.gvSaleOpensum.FooterRow.FindControl("lgvFToAmt")).Text = Convert.ToDouble(sum).ToString("#,##0;(#,##0); ");

        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "SalesSummary":
                    this.RptSalesSummary();
                    break;
            }



        }
        private void RptSalesSummary()
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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = (List<RealEntity.EClassSalesOpening>)Session["tbl"];

            //DataTable dt = (DataTable)Session["tbl"];

            ////List<SalesOpening> lst = objUserService.ShowSalesOpening();
            //var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptSalesOpening>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptOSalesSummary", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            // Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Sales Opening -Summary"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //List<SalesOpening> lst = objUserService.ShowSalesOpening();
            //ReportDocument ROSales = new RealERPRPT.R_22_Sal.RptOSalesSummary();
            //TextObject rptCname = ROSales.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtuserinfo = ROSales.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //ROSales.SetDataSource(lst);
            //Session["Report1"] = ROSales;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

    }
}