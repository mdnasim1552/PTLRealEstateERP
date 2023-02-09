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
using System.IO;
namespace RealERPWEB.F_32_Mis
{
    public partial class RptAccountsReport : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Supplier & Group Wise Payable";
                this.ShowView();

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






        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "CashFlow":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "RptSupCredit02":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }
        }


















        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "CashFlow":
                    this.ShowCashFlow();
                    break;





            }



        }





        private void ShowCashFlow()
        {
            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string supplier = "99%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTCASHFLOWBRIDGE", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rpcashflow.DataSource = null;
                this.rpcashflow.DataBind();
                return;
            }


            Session["tblpayst"] = this.HiddenSameData(ds1.Tables[0]);
            Session["tblbank"] = ds1.Tables[1];
            this.Data_Bind();


        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;

            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "CashFlow":
                    string flow = dt1.Rows[0]["flow"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["flow"].ToString() == flow)
                        {
                            dt1.Rows[j]["flowdesc"] = "";

                        }



                        flow = dt1.Rows[j]["flow"].ToString();




                    }

                    break;



            }


            return dt1;

        }








        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpayst"];



            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "CashFlow":
                    int i, j;

                    //for (i = 2; i < this.rpcashflow.Items.Count; i++)
                    //    this.Items = false;
                    //j = 2;

                    //for (i = 0; i < this.cblProject.Items.Count; i++)
                    //{
                    //    if (this.cblProject.Items[i].Selected)
                    //    {
                    //        this.gvBgd.Columns[j].Visible = true;
                    //        this.gvBgd.Columns[j].HeaderText = this.cblProject.Items[i].Text.Trim();

                    //        if (j == 13)
                    //            break;
                    //        j++;
                    //    }

                    //}
                    this.rpcashflow.DataSource = dt;
                    this.rpcashflow.DataBind();
                    int c = this.rpcashflow.Items.Count;

                    //Session["Report1"] = rpcashflow;
                    // ((HyperLink)e.Item.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
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







        protected void rpsupplier_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {
                DataTable dt = (DataTable)Session["tblpayst"];

                //List<SalesOpening> lst = (List<SalesOpening>)Session["tbl"];
                ((Label)e.Item.FindControl("lblrpFtooutst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tooust)", "")) ?
                                  0 : dt.Compute("sum(tooust)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblrpF0to15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(step7)", "")) ?
                                  0 : dt.Compute("sum(step7)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblrpF16to30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(step6)", "")) ?
                                 0 : dt.Compute("sum(step6)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblrpF31to45")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(step5)", "")) ?
                                 0 : dt.Compute("sum(step5)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblrpF46to60")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(step4)", "")) ?
                                 0 : dt.Compute("sum(step4)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblrpF61to90")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(step3)", "")) ?
                                 0 : dt.Compute("sum(step3)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblrpF91to120")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(step2)", "")) ?
                                 0 : dt.Compute("sum(step2)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblrpFover120")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(step1)", "")) ?
                                 0 : dt.Compute("sum(step1)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblrpFpayrequired")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payreq)", "")) ?
                                 0 : dt.Compute("sum(payreq)", ""))).ToString("#,##0;(#,##0); ");



            }
        }
        protected void rpcashflow_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Header)
            {

                DataTable dt = (DataTable)Session["tblbank"];

                int j = 1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    ((Label)e.Item.FindControl("lblrphamt" + ASTUtility.Right(("00" + j.ToString()), 2))).Text = dt.Rows[i]["bankdesc"].ToString();
                    j++;

                    if (j == 18)
                        break;
                }


            }


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lgvProjectName = (Label)e.Item.FindControl("lgvProjectName");
                Label lblrpamt01 = (Label)e.Item.FindControl("lblrpamt01");
                Label lblrpamt02 = (Label)e.Item.FindControl("lblrpamt02");
                Label lblrpamt03 = (Label)e.Item.FindControl("lblrpamt03");
                Label lblrpamt04 = (Label)e.Item.FindControl("lblrpamt04");
                Label lblrpamt05 = (Label)e.Item.FindControl("lblrpamt05");
                Label lblrpamt06 = (Label)e.Item.FindControl("lblrpamt06");
                Label lblrpamt07 = (Label)e.Item.FindControl("lblrpamt07");
                Label lblrpamt08 = (Label)e.Item.FindControl("lblrpamt08");
                Label lblrpamt09 = (Label)e.Item.FindControl("lblrpamt09");
                Label lblrpamt10 = (Label)e.Item.FindControl("lblrpamt10");
                Label lblrpamt11 = (Label)e.Item.FindControl("lblrpamt11");
                Label lblrpamt12 = (Label)e.Item.FindControl("lblrpamt12");
                Label lblrpamt13 = (Label)e.Item.FindControl("lblrpamt13");




                //string wastatus = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "wastatus")).ToString();
                //string mACTCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "actcode")).ToString();
                //string mMemoNo = "INV" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyyMM") + "00" + ASTUtility.Right(this.lblInvNo.Text, 3); //this.GetLastInVNo();
                //string mBATCHCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "batchcode")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "pactcode")).ToString();
                if (pactcode == "")
                    return;
                if (ASTUtility.Right(pactcode, 4) == "AAAA" || ASTUtility.Right(pactcode, 4) == "9999")

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



                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //DataTable dt = (DataTable)Session["tblpayst"];

                ////List<SalesOpening> lst = (List<SalesOpening>)Session["tbl"];
                //((Label)e.Item.FindControl("lblrpFamt01")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ?
                //                  0 : dt.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ?
                //                  0 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ?
                //               0 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt04")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ?
                //               0 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt05")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ?
                //               0 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt06")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ?
                //               0 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt07")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ?
                //               0 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt08")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ?
                //               0 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt09")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ?
                //               0 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ?
                //               0 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ?
                //               0 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ?
                //               0 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt13)", "")) ?
                //               0 : dt.Compute("sum(amt13)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ?
                //               0 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");



            }


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "CashFlow":
                    this.RptCashFlow();
                    break;




            }





        }

        private void RptCashFlow()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblpayst"];
            DataTable dt1 = (DataTable)Session["tblbank"];
            if (dt == null)
                return;

            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.CashFlowBankWise>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_32_Mis.RptCashFlowBridge", list, null, null);

            int j = 1;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                rpt.SetParameters(new ReportParameter("txtbank" + j.ToString(), dt1.Rows[i]["bankdesc"].ToString()));
                j++;
                if (j == 11)
                    break;

            }

            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("rptTitle", "Cash Flow"));
            rpt.SetParameters(new ReportParameter("date", " (From " + this.txtfrmDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")"));
            rpt.SetParameters(new ReportParameter("txtuserinfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            // ReportDocument rptcash = new RealERPRPT.R_32_Mis.RptCashFlowBridge();
            // TextObject txtCompanyName = rptcash.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            // txtCompanyName.Text = comnam;
            // DataTable dt1 = (DataTable)Session["tblbank"];
            // int j=1;
            // for (int i = 0; i < dt1.Rows.Count; i++)
            // {



            //         TextObject rpttxth = rptcash.ReportDefinition.ReportObjects["txtb" + j.ToString()] as TextObject;
            //         rpttxth.Text = dt1.Rows[i]["bankdesc"].ToString();
            //         j++;
            //         if (j == 13)
            //             break;

            //}

            // TextObject txtDate = rptcash.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            // txtDate.Text = " (From " + this.txtfrmDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            // TextObject txtuserinfo = rptcash.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            // rptcash.SetDataSource(dt);
            // Session["Report1"] = rptcash;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnExpExcel_Click(object sender, EventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=RepeaterExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            rpcashflow.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}