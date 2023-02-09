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
namespace RealERPWEB.F_23_CR
{
    public partial class RptSalesReportGen : System.Web.UI.Page
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

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = date;
                //   this.txttodate.Text = Convert.ToDateTime(txtfrmDate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Account Receivable & Unsold Flates Statement";
                this.GetProjectName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

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
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            if (Request.QueryString["prjcode"].Length > 0)
            {
                ddlProjectName.SelectedValue = Request.QueryString["prjcode"].ToString();
                ddlProjectName.Enabled = false;
            }


        }





        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowSalesStatus();



        }





        private void ShowSalesStatus()
        {
            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string PactCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWISEPROINSDUESURBAN", PactCode, frmdate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSales.DataSource = null;
                this.gvSales.DataBind();
                return;
            }

            List<RealEntity.C_23_CRR.EClassSalesStatus.SalesStatus> lst = ds1.Tables[0].DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.SalesStatus>();


            Session["tblpayst"] = this.HiddenSameData(lst);
            //Session["tblpayst"] =ds1.Tables[0];
            this.Data_Bind();


        }


        private List<RealEntity.C_23_CRR.EClassSalesStatus.SalesStatus> HiddenSameData(List<RealEntity.C_23_CRR.EClassSalesStatus.SalesStatus> lst)
        {
            if (lst.Count == 0)
                return lst;
            int j = 0;
            string pactcode = lst[0].pactcode;
            foreach (var lst1 in lst)
            {
                if (j == 0)
                {
                    j++;

                }

                else if (lst1.pactcode.ToString() == pactcode)
                {
                    lst1.pactdesc = "";

                }



                pactcode = lst1.pactcode;




            }

            return lst;

        }


        //

        // }








        private void Data_Bind()
        {
            var lst = (List<RealEntity.C_23_CRR.EClassSalesStatus.SalesStatus>)Session["tblpayst"];
            this.gvSales.DataSource = lst;
            this.gvSales.DataBind();
            this.FooterCalCulation();
        }


        private void FooterCalCulation()
        {




            var lst = (List<RealEntity.C_23_CRR.EClassSalesStatus.SalesStatus>)Session["tblpayst"];
            if (lst.Count == 0)
                return;



            Session["Report1"] = gvSales;
            ((HyperLink)this.gvSales.FooterRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            //((Label)this.gvSales.FooterRow.FindControl("lblgvFsalesam")).Text = lst.Sum(l => l.salesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFscollam")).Text = lst.Sum(l => l.scollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFsalesbalam")).Text = lst.Sum(l => l.salesbalam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFutsalesam")).Text = lst.Sum(l => l.utsalesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFutcollam")).Text = lst.Sum(l => l.utcollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFutbalam")).Text = lst.Sum(l => l.utbalam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFmodsalesam")).Text = lst.Sum(l => l.modsalesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFmodcollam")).Text = lst.Sum(l => l.modcollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFmodbalam")).Text = lst.Sum(l => l.modbalam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFopsalesam")).Text = lst.Sum(l => l.opsalesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFopcollam")).Text = lst.Sum(l => l.opcollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFopbalam")).Text = lst.Sum(l => l.opbalam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFtrnsalesam")).Text = lst.Sum(l => l.trnsalesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFtrncollam")).Text = lst.Sum(l => l.trncollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFtrnbalam")).Text = lst.Sum(l => l.trnbalam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFregsalesam")).Text = lst.Sum(l => l.regsalesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFregcollam")).Text = lst.Sum(l => l.regcollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFregbalam")).Text = lst.Sum(l => l.regbalam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFasosalesam")).Text = lst.Sum(l => l.asosalesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFasocollam")).Text = lst.Sum(l => l.asocollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFasobalam")).Text = lst.Sum(l => l.asobalam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFmutsalesam")).Text = lst.Sum(l => l.mutsalesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFmutcollam")).Text = lst.Sum(l => l.mutcollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFmutbalam")).Text = lst.Sum(l => l.mutbalam).ToString("#,##0;(#,##0); ");


            //((Label)this.gvSales.FooterRow.FindControl("lblgvFupsalesam")).Text = lst.Sum(l => l.upsalesam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFupcollam")).Text = lst.Sum(l => l.upcollam).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSales.FooterRow.FindControl("lblgvFupbalam")).Text = lst.Sum(l => l.upbalam).ToString("#,##0;(#,##0); ");
        }








        //protected void rpsales_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{

        //    if (e.Item.ItemType == ListItemType.Header)
        //    {

        //        ((Label)e.Item.FindControl("lblrpscollam")).Text = "Sales Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
        //        ((Label)e.Item.FindControl("lblrpocollam")).Text = "Other Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
        //        ((Label)e.Item.FindControl("lblrptcollam")).Text = "Total Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
        //        ((Label)e.Item.FindControl("lblrpcurcollam")).Text = "Sales Collection during " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");
        //        ((Label)e.Item.FindControl("lblrpcurocollam")).Text = "Ohter Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");
        //        ((Label)e.Item.FindControl("lblrpcurtcollam")).Text = "Total Collectoon During  " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");


        //    }


        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {

        //        Label lgvProjectName = (Label)e.Item.FindControl("lblrpProjectName");
        //        Label lblrpamt01 = (Label)e.Item.FindControl("lblrpcustname");
        //        Label lblrpamt02 = (Label)e.Item.FindControl("lblrpunit");
        //        Label lblrpamt03 = (Label)e.Item.FindControl("lblrpaptst");
        //        Label lblrpamt04 = (Label)e.Item.FindControl("lblrpsalesam");
        //        Label lblrpamt05 = (Label)e.Item.FindControl("lblrpscollam");
        //        Label lblrpamt06 = (Label)e.Item.FindControl("lblrpsreceivable");
        //        Label lblrpamt07 = (Label)e.Item.FindControl("lblrpassociaam");
        //        Label lblrpamt08 = (Label)e.Item.FindControl("lblrpmodcharge");
        //        Label lblrpamt09 = (Label)e.Item.FindControl("lblrpocollam");
        //        Label lblrpamt10 = (Label)e.Item.FindControl("lblrpodues");
        //        Label lblrpamt11 = (Label)e.Item.FindControl("lblrptsaleeam");
        //        Label lblrpamt12 = (Label)e.Item.FindControl("lblrptcollam");
        //        Label lblrpamt13 = (Label)e.Item.FindControl("lblrptreceivable");
        //        Label lblrpamt14 = (Label)e.Item.FindControl("lblrpcurcollam");
        //        Label lblrpamt15 = (Label)e.Item.FindControl("lblrpcurocollam");
        //        Label lblrpamt16 = (Label)e.Item.FindControl("lblrptcurcollam");
        //        Label lblrpamt17 = (Label)e.Item.FindControl("lblrpuptocollam");
        //        Label lblrpamt18 = (Label)e.Item.FindControl("lblrptpcreceivable");
        //        Label lblrpamt19 = (Label)e.Item.FindControl("lblrplcollam");
        //        Label lblrpamt20 = (Label)e.Item.FindControl("lblrpinsdues");
        //        // Label lblrpamt21 = (Label)e.Item.FindControl("lblrpamt13");




        //        //string wastatus = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "wastatus")).ToString();
        //        //string mACTCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "actcode")).ToString();
        //        //string mMemoNo = "INV" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyyMM") + "00" + ASTUtility.Right(this.lblInvNo.Text, 3); //this.GetLastInVNo();
        //        //string mBATCHCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "batchcode")).ToString();
        //        string usircode = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "usircode")).ToString();
        //        if (usircode == "")
        //            return;
        //        if (ASTUtility.Right(usircode, 4) == "AAAA")   //|| ASTUtility.Right(pactcode, 4) == "9999")
        //        {
        //            lgvProjectName.Attributes["style"] = "font-weight:bold; text-align:right;";

        //            lblrpamt01.Style.Add("font-weight", "bold");
        //            lblrpamt02.Style.Add("font-weight", "bold");
        //            lblrpamt03.Style.Add("font-weight", "bold");
        //            lblrpamt04.Style.Add("font-weight", "bold");
        //            lblrpamt05.Style.Add("font-weight", "bold");
        //            lblrpamt06.Style.Add("font-weight", "bold");
        //            lblrpamt07.Style.Add("font-weight", "bold");
        //            lblrpamt08.Style.Add("font-weight", "bold");
        //            lblrpamt09.Style.Add("font-weight", "bold");
        //            lblrpamt10.Style.Add("font-weight", "bold");
        //            lblrpamt11.Style.Add("font-weight", "bold");
        //            lblrpamt12.Style.Add("font-weight", "bold");
        //            lblrpamt13.Style.Add("font-weight", "bold");
        //            lblrpamt14.Style.Add("font-weight", "bold");

        //            lblrpamt15.Style.Add("font-weight", "bold");
        //            lblrpamt16.Style.Add("font-weight", "bold");
        //            lblrpamt17.Style.Add("font-weight", "bold");
        //            lblrpamt18.Style.Add("font-weight", "bold");
        //            lblrpamt19.Style.Add("font-weight", "bold");
        //            lblrpamt20.Style.Add("font-weight", "bold");



        //        }
        //    }
        //    //if (e.Item.ItemType == ListItemType.Footer)
        //    //{
        //    //    DataTable dt = ((DataTable)Session["tblpayst"]).Copy();
        //    //    DataView dv = dt.DefaultView;


        //    //    DataTable dt = (DataTable)Session["tblpayst"];

        //    //    //List<SalesOpening> lst = (List<SalesOpening>)Session["tbl"];
        //    //    ((Label)e.Item.FindControl("lblrpFsalesam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salesam)", "")) ?
        //    //                      0 : dt.Compute("sum(salesam)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ?
        //    //                      0 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ?
        //    //                   0 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt04")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ?
        //    //                   0 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt05")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ?
        //    //                   0 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt06")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ?
        //    //                   0 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt07")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ?
        //    //                   0 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt08")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ?
        //    //                   0 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt09")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ?
        //    //                   0 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ?
        //    //                   0 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ?
        //    //                   0 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ?
        //    //                   0 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFamt13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt13)", "")) ?
        //    //                   0 : dt.Compute("sum(amt13)", ""))).ToString("#,##0;(#,##0); ");
        //    //    ((Label)e.Item.FindControl("lblrpFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ?
        //    //                   0 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");



        //    //}


        //}

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            // IQBAL NAYAN
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = ASTUtility.Concat(compname, username, printdate);
            var lst = (List<RealEntity.C_23_CRR.EClassSalesStatus.SalesStatus>)Session["tblpayst"];
            string date = "As On:" + this.txtfrmDate.Text;

            // ViewState["tblconarea"] = ds1.Tables[0]; // nayan

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptSalesStatusGen", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("txtdate", date));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptCashFlow()
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = this.GetCompCode();
            // string comnam = hst["comnam"].ToString();
            // string compname = hst["compname"].ToString();
            // string comadd = hst["comadd1"].ToString();
            // string username = hst["username"].ToString();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // DataTable dt = (DataTable)Session["tblpayst"];
            //// ReportDocument rptcash = new RealERPRPT.R_32_Mis.RptCashFlowBridge();

            // ReportDocument rptAccont = new RealERPRPT.R_23_CR.RptInfAndActRecived();
            // TextObject txtCompanyName = rptAccont.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            // txtCompanyName.Text = comnam;



            // TextObject txtAdd = rptAccont.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            // txtAdd.Text = comadd;

            // TextObject txtProject = rptAccont.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            // txtProject.Text = "Project Name : "+ this.ddlProjectName.SelectedItem.Text; 
            // TextObject txtDate = rptAccont.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            // txtDate.Text = " (From " + this.txtfrmDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            // TextObject txtscollam = rptAccont.ReportDefinition.ReportObjects["txtscollam"] as TextObject;
            // txtscollam.Text = "Sales Collection as on "+Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");

            // TextObject txtocollam = rptAccont.ReportDefinition.ReportObjects["txtocollam"] as TextObject;
            // txtocollam.Text = "Other Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
            // TextObject txttcollam = rptAccont.ReportDefinition.ReportObjects["txttcollam"] as TextObject;
            // txttcollam.Text = "Total Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
            // TextObject txtcurcollam = rptAccont.ReportDefinition.ReportObjects["txtcurcollam"] as TextObject;
            // txtcurcollam.Text = "Sales Collection during " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");
            // TextObject txtcurocollam = rptAccont.ReportDefinition.ReportObjects["txtcurocollam"] as TextObject;
            // txtcurocollam.Text = "Ohter Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");

            // TextObject txttcurcollam = rptAccont.ReportDefinition.ReportObjects["txttcurcollam"] as TextObject;
            // txttcurcollam.Text = "Total Collectoon During  " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy"); 

            // TextObject txtuserinfo = rptAccont.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            // rptAccont.SetDataSource(dt);
            // Session["Report1"] = rptAccont;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void gvSales_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //  gvrow.Cells.Remove(TableCell [0]);

                TableCell cell01 = new TableCell();
                cell01.Text = "Sl.No.";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Project Name";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Client's Name";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);


                TableCell cell04 = new TableCell();
                cell04.Text = "Flat No";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.RowSpan = 2;
                gvrow.Cells.Add(cell04);


                TableCell cell05 = new TableCell();
                cell05.Text = "Installment";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.Attributes["style"] = "font-weight:bold;";
                cell05.ColumnSpan = 3;
                gvrow.Cells.Add(cell05);

                TableCell cell06 = new TableCell();
                cell06.Text = "Utility";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.Attributes["style"] = "font-weight:bold;";
                cell06.ColumnSpan = 3;
                gvrow.Cells.Add(cell06);



                //TableCell cell06B = new TableCell();
                //cell06B.Text = "Upgradation";
                //cell06B.HorizontalAlign = HorizontalAlign.Center;
                //cell06B.Attributes["style"] = "font-weight:bold;";
                //cell06B.ColumnSpan = 3;
                //gvrow.Cells.Add(cell06B);


                TableCell cell07 = new TableCell();
                cell07.Text = "Modification";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.Attributes["style"] = "font-weight:bold;";
                cell07.ColumnSpan = 3;
                gvrow.Cells.Add(cell07);

                TableCell cell08 = new TableCell();
                cell08.Text = "Optional";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.Attributes["style"] = "font-weight:bold;";
                cell08.ColumnSpan = 3;
                gvrow.Cells.Add(cell08);

                TableCell cell09 = new TableCell();
                cell09.Text = "Transfer Fee";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.Attributes["style"] = "font-weight:bold;";
                cell09.ColumnSpan = 3;
                gvrow.Cells.Add(cell09);


                TableCell cell10 = new TableCell();
                cell10.Text = "Registration";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.Attributes["style"] = "font-weight:bold;";
                cell10.ColumnSpan = 3;
                gvrow.Cells.Add(cell10);


                TableCell cell11 = new TableCell();
                cell11.Text = "Association";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.Attributes["style"] = "font-weight:bold;";
                cell11.ColumnSpan = 3;
                gvrow.Cells.Add(cell11);

                TableCell cell12 = new TableCell();
                cell12.Text = "Mutation";
                cell12.HorizontalAlign = HorizontalAlign.Center;
                cell12.Attributes["style"] = "font-weight:bold;";
                cell12.ColumnSpan = 3;
                gvrow.Cells.Add(cell12);
                gvSales.Controls[0].Controls.AddAt(0, gvrow);



                gvSales.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;


            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlnkgvscollam = (HyperLink)e.Row.FindControl("hlnkgvscollam");
                HyperLink hlnkgvutcollam = (HyperLink)e.Row.FindControl("hlnkgvutcollam");
                HyperLink hlnkgvmodcollam = (HyperLink)e.Row.FindControl("hlnkgvmodcollam");
                HyperLink hlnkgvopcollam = (HyperLink)e.Row.FindControl("hlnkgvopcollam");
                HyperLink hlnkgvtrncollam = (HyperLink)e.Row.FindControl("hlnkgvtrncollam");
                HyperLink hlnkgvregcollam = (HyperLink)e.Row.FindControl("hlnkgvregcollam");
                HyperLink hlnkgvasocollam = (HyperLink)e.Row.FindControl("hlnkgvasocollam");
                HyperLink hlnkgvmutcollam = (HyperLink)e.Row.FindControl("hlnkgvmutcollam");
                HyperLink hlnkgvupcollam = (HyperLink)e.Row.FindControl("hlnkgvupcollam");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }


                else
                {
                    string insgencod = "02001";
                    string modgencod = "02090";
                    string opgencod = "02097";
                    string reggencod = "02901";
                    string trngencod = "02903";
                    string utilitygencod = "02904";
                    string assogencod = "02905";
                    string mutgencod = "02907";
                    //string upgencod = "02086";
                    string date = this.txtfrmDate.Text;

                    hlnkgvscollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + insgencod + "&Date1=" + date;
                    hlnkgvutcollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + utilitygencod + "&Date1=" + date;
                    hlnkgvmodcollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + modgencod + "&Date1=" + date;
                    hlnkgvopcollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + opgencod + "&Date1=" + date;
                    hlnkgvtrncollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + trngencod + "&Date1=" + date;
                    hlnkgvregcollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + reggencod + "&Date1=" + date;
                    hlnkgvasocollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + assogencod + "&Date1=" + date;
                    hlnkgvmutcollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + mutgencod + "&Date1=" + date;
                    // hlnkgvupcollam.NavigateUrl = "~/F_23_CR/LinkMktMoneyReceipt.aspx?Type=Report&prjcode=" + code + "&usircode=" + usircode + "&genno=" + upgencod + "&Date1=" + date;



                }




            }
        }
    }
}