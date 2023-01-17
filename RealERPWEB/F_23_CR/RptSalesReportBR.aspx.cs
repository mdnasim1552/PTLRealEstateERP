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
namespace RealERPWEB.F_23_CR
{
    public partial class RptSalesReportBR : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(txtfrmDate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
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
            ds1.Dispose();

        }





        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowCashFlow();



        }





        private void ShowCashFlow()
        {
            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string PactCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWISEPROINSDUESBR", PactCode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rpcashflow.DataSource = null;
                this.rpcashflow.DataBind();
                return;
            }

            Session["tblpayst"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["tblpayst"] =ds1.Tables[0];
            this.Data_Bind();


        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;





            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    dt1.Rows[j]["pactdesc"] = "";

                }



                pactcode = dt1.Rows[j]["pactcode"].ToString();




            }

            return dt1;

        }


        //

        // }








        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpayst"];
            this.rpcashflow.DataSource = dt;
            this.rpcashflow.DataBind();
        }


        private void FooterCalCulation()
        {

            DataTable dt = ((DataTable)Session["tblpayst"]).Copy();
            if (dt.Rows.Count == 0)
                return;


            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "rpcashflow":



                    //((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ?
                    //              0 : dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFduetopay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netdues)", "")) ?
                    //              0 : dt.Compute("sum(netdues)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFcrlimit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crlimit)", "")) ?
                    //              0 : dt.Compute("sum(crlimit)", ""))).ToString("#,##0;(#,##0); ");

                    // ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFnyetdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nyetdues)", "")) ?
                    //              0 : dt.Compute("sum(nyetdues)", ""))).ToString("#,##0;(#,##0); ");
                    break;






            }



        }








        protected void rpcashflow_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Header)
            {

                ((Label)e.Item.FindControl("lblrpscollam")).Text = "Sales Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
                ((Label)e.Item.FindControl("lblrpocollam")).Text = "Other Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
                ((Label)e.Item.FindControl("lblrptcollam")).Text = "Total Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
                ((Label)e.Item.FindControl("lblrpcurcollam")).Text = "Sales Collection during " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");
                ((Label)e.Item.FindControl("lblrpcurocollam")).Text = "Ohter Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");
                ((Label)e.Item.FindControl("lblrpcurtcollam")).Text = "Total Collectoon During  " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");


            }


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lgvProjectName = (Label)e.Item.FindControl("lblrpProjectName");
                Label lblrpamt01 = (Label)e.Item.FindControl("lblrpcustname");
                Label lblrpamt02 = (Label)e.Item.FindControl("lblrpunit");
                Label lblrpamt03 = (Label)e.Item.FindControl("lblrpaptst");
                Label lblrpamt04 = (Label)e.Item.FindControl("lblrpsalesam");
                Label lblrpamt05 = (Label)e.Item.FindControl("lblrpscollam");
                Label lblrpamt06 = (Label)e.Item.FindControl("lblrpsreceivable");
                Label lblrpamt07 = (Label)e.Item.FindControl("lblrpassociaam");
                Label lblrpamt08 = (Label)e.Item.FindControl("lblrpmodcharge");
                Label lblrpamt09 = (Label)e.Item.FindControl("lblrpocollam");
                Label lblrpamt10 = (Label)e.Item.FindControl("lblrpodues");
                Label lblrpamt11 = (Label)e.Item.FindControl("lblrptsaleeam");
                Label lblrpamt12 = (Label)e.Item.FindControl("lblrptcollam");
                Label lblrpamt13 = (Label)e.Item.FindControl("lblrptreceivable");
                Label lblrpamt14 = (Label)e.Item.FindControl("lblrpcurcollam");
                Label lblrpamt15 = (Label)e.Item.FindControl("lblrpcurocollam");
                Label lblrpamt16 = (Label)e.Item.FindControl("lblrptcurcollam");
                Label lblrpamt17 = (Label)e.Item.FindControl("lblrpuptocollam");
                Label lblrpamt18 = (Label)e.Item.FindControl("lblrptpcreceivable");
                Label lblrpamt19 = (Label)e.Item.FindControl("lblrplcollam");
                Label lblrpamt20 = (Label)e.Item.FindControl("lblrpinsdues");
                // Label lblrpamt21 = (Label)e.Item.FindControl("lblrpamt13");




                //string wastatus = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "wastatus")).ToString();
                //string mACTCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "actcode")).ToString();
                //string mMemoNo = "INV" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyyMM") + "00" + ASTUtility.Right(this.lblInvNo.Text, 3); //this.GetLastInVNo();
                //string mBATCHCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "batchcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "usircode")).ToString();
                if (usircode == "")
                    return;
                if (ASTUtility.Right(usircode, 4) == "AAAA")   //|| ASTUtility.Right(pactcode, 4) == "9999")
                {
                    lgvProjectName.Attributes["style"] = "font-weight:bold; text-align:right;";

                    lblrpamt01.Style.Add("font-weight", "bold");
                    lblrpamt02.Style.Add("font-weight", "bold");
                    lblrpamt03.Style.Add("font-weight", "bold");
                    lblrpamt04.Style.Add("font-weight", "bold");
                    lblrpamt05.Style.Add("font-weight", "bold");
                    lblrpamt06.Style.Add("font-weight", "bold");
                    lblrpamt07.Style.Add("font-weight", "bold");
                    lblrpamt08.Style.Add("font-weight", "bold");
                    lblrpamt09.Style.Add("font-weight", "bold");
                    lblrpamt10.Style.Add("font-weight", "bold");
                    lblrpamt11.Style.Add("font-weight", "bold");
                    lblrpamt12.Style.Add("font-weight", "bold");
                    lblrpamt13.Style.Add("font-weight", "bold");
                    lblrpamt14.Style.Add("font-weight", "bold");

                    lblrpamt15.Style.Add("font-weight", "bold");
                    lblrpamt16.Style.Add("font-weight", "bold");
                    lblrpamt17.Style.Add("font-weight", "bold");
                    lblrpamt18.Style.Add("font-weight", "bold");
                    lblrpamt19.Style.Add("font-weight", "bold");
                    lblrpamt20.Style.Add("font-weight", "bold");



                }
            }
            //if (e.Item.ItemType == ListItemType.Footer)
            //{
            //    DataTable dt = ((DataTable)Session["tblpayst"]).Copy();
            //    DataView dv = dt.DefaultView;


            //    DataTable dt = (DataTable)Session["tblpayst"];

            //    //List<SalesOpening> lst = (List<SalesOpening>)Session["tbl"];
            //    ((Label)e.Item.FindControl("lblrpFsalesam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salesam)", "")) ?
            //                      0 : dt.Compute("sum(salesam)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ?
            //                      0 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ?
            //                   0 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt04")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ?
            //                   0 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt05")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ?
            //                   0 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt06")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ?
            //                   0 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt07")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ?
            //                   0 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt08")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ?
            //                   0 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt09")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ?
            //                   0 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ?
            //                   0 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ?
            //                   0 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ?
            //                   0 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFamt13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt13)", "")) ?
            //                   0 : dt.Compute("sum(amt13)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)e.Item.FindControl("lblrpFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ?
            //                   0 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");



            //}


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            this.RptCashFlow();





        }

        private void RptCashFlow()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string rptdate = " (From " + this.txtfrmDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            string txtProject = "Project Name : " + this.ddlProjectName.SelectedItem.Text;
            string txtscollam = "Sales Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
            string txtocollam = "Other Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
            string txttcollam = "Total Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).AddDays(-1).ToString("MMMM-yy");
            string txtcurcollam = "Sales Collection during " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");

            string txtcurocollam = "Ohter Collection as on " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");
            string txttcurcollam = "Total Collectoon During  " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM-yy");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);


            DataTable dt = (DataTable)Session["tblpayst"];
            // ReportDocument rptcash = new RealERPRPT.R_32_Mis.RptCashFlowBridge();


            DataTable dt2 = new DataTable();

            dt2 = dt.Select("usircode not in ( '5AAAAAAAAAAA','5BBBBBBBAAAA')").CopyToDataTable();

            var lst = dt2.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassSalesSatusReport>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptInfAndActReceived", lst, null, null);


            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", rptdate));
            Rpt1.SetParameters(new ReportParameter("title", "Information Of Account Receivable & Unsold Flats Statement"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtProject", txtProject));
            Rpt1.SetParameters(new ReportParameter("txtscollam", txtscollam));
            Rpt1.SetParameters(new ReportParameter("txtocollam", txtocollam));
            Rpt1.SetParameters(new ReportParameter("txttcollam", txttcollam));
            Rpt1.SetParameters(new ReportParameter("txtcurcollam", txtcurcollam));
            Rpt1.SetParameters(new ReportParameter("txtcurocollam", txtcurocollam));
            Rpt1.SetParameters(new ReportParameter("txttcurcollam", txttcurcollam));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";







            //Crystal Report 
            // ReportDocument rptAccont = new RealERPRPT.R_23_CR.RptInfAndActRecived();
            // TextObject txtCompanyName = rptAccont.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            // txtCompanyName.Text = comnam;
            //// DataTable dt1 = (DataTable)Session["tblbank"];
            //// int j=1;
            //// for (int i = 0; i < dt1.Rows.Count; i++)
            //// {



            ////         TextObject rpttxth = rptcash.ReportDefinition.ReportObjects["txtb" + j.ToString()] as TextObject;
            ////         rpttxth.Text = dt1.Rows[i]["bankdesc"].ToString();
            ////         j++;
            ////         if (j == 12)
            ////             break;

            ////}


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
    }
}