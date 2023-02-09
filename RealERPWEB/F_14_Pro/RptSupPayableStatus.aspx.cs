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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptSupPayableStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();


                //((Label)this.Master.FindControl("lblTitle")).Text = type == "RptgrpPayable" ? "Monthly Payable( Group Wise)" : "Monthly Supplier & Group Wise Payable";

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = Convert.ToDateTime("01" + (this.txttodate.Text).Substring(2)).ToString("dd-MMM-yyyy");

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
            return (hst["comcod"].ToString());

        }






        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSuppPayable":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "RptgrpPayable":
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.lblGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "RptmgrpPayable":
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.lblGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

            }
        }













        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSuppPayable":
                    this.RptSupPayableStatus02();
                    break;


                case "RptgrpPayable":
                    this.RptGrpPayableStatus();

                    break;

                case "RptmgrpPayable":
                    this.RptMatGroupPayable();
                    break;
            }





        }

        private void RptSupPayableStatus02()
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

            DataTable dt1 = (DataTable)Session["tblpayst"];

            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_14_Pro.RptSuppPayable>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSupplierPayable", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));

            Rpt1.SetParameters(new ReportParameter("txtDate", " (" + "From  " + frmdate + " To " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Supplier & Group Wise Payable"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            //ReportDocument rptImp = new RealERPRPT.R_14_Pro.RptSupplierPayable();
            //TextObject txtCompanyName = rptImp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            //txtdat.Text = " (" + "From  " + frmdate + " To " + todate + ")";
            //DataTable dt = (DataTable)Session["tblpayst"];
            //TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptImp.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptImp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptImp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        //private void RptGrpPayableStatus()
        //{

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = this.GetCompCode();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
        //    ReportDocument rptImp = new RealERPRPT.R_14_Pro.RptGrpwisePayable();
        //    TextObject txtCompanyName = rptImp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    txtCompanyName.Text = comnam;
        //    TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
        //    txtdat.Text = " (" + "From  " + frmdate + " To " + todate + ")";
        //    DataTable dt = (DataTable)Session["tblpayst"];
        //    TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
        //    rptImp.SetDataSource(dt);
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rptImp.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptImp;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}

        private void RptGrpPayableStatus()
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

            DataTable dt = (DataTable)Session["tblpayst"];

            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            LocalReport Rpt1 = new LocalReport();


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptMatGrpwisePayable>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptGrpwisePayable", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", " (" + "From  " + frmdate + " To " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Payable (Group Wise )"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }






        //private void RptMatGroupPayable() 
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = this.GetCompCode();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
        //    ReportDocument rptImp = new RealERPRPT.R_14_Pro.RptMatGrpwisePayable();
        //    TextObject txtCompanyName = rptImp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    txtCompanyName.Text = comnam;
        //    TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
        //    txtdat.Text = " (" + "From  " + frmdate + " To " + todate + ")";
        //    DataTable dt = (DataTable)Session["tblpayst"];
        //    TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
        //    rptImp.SetDataSource(dt);
        //    Session["Report1"] = rptImp;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}

        private void RptMatGroupPayable()
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

            DataTable dt = (DataTable)Session["tblpayst"];

            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            LocalReport Rpt1 = new LocalReport();


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptMatGrpwisePayable>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptMatGrpwisePayable", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", " (" + "From  " + frmdate + " To " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Payable (Group Wise )"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSuppPayable":
                    this.ShowSupplierPayable();
                    break;

                case "RptgrpPayable":
                    this.ShowGroupPayable();
                    break;

                case "RptmgrpPayable":
                    this.ShowMatGroupPayable();
                    break;


            }



        }





        private void ShowSupplierPayable()
        {
            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = "%";
            string supplier = "99%";
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_SUPPLER", "RPTSUPPAYABLE", fromdate, todate, pactcode, supplier, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayableStatus.DataSource = null;
                this.gvPayableStatus.DataBind();
                return;
            }


            Session["tblpayst"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }
        private void ShowGroupPayable()
        {
            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = "%";
            string supplier = "99%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_SUPPLER", "RPTGROUPPAYABLE", fromdate, todate, pactcode, supplier, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayableSum.DataSource = null;
                this.gvPayableSum.DataBind();
                return;
            }


            Session["tblpayst"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void ShowMatGroupPayable()
        {

            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = "%";
            string supplier = "99%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_SUPPLER", "RPTMWISEGRPPAYABLE", fromdate, todate, pactcode, supplier, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvmwisePayable.DataSource = null;
                this.gvmwisePayable.DataBind();
                return;
            }


            Session["tblpayst"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSuppPayable":

                    string mwgcod = dt1.Rows[0]["mwgcod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["mwgcod"].ToString() == mwgcod)
                        {
                            mwgcod = dt1.Rows[j]["mwgcod"].ToString();
                            dt1.Rows[j]["mwgdesc"] = "";
                        }


                        mwgcod = dt1.Rows[j]["mwgcod"].ToString();
                    }

                    break;



                case "RptmgrpPayable":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";



                        grp = dt1.Rows[j]["grp"].ToString();
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
                case "RptSuppPayable":

                    this.gvPayableStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPayableStatus.DataSource = dt;
                    this.gvPayableStatus.DataBind();
                    this.FooterCalCulation();

                    break;

                case "RptgrpPayable":
                    this.gvPayableSum.DataSource = dt;
                    this.gvPayableSum.DataBind();
                    this.FooterCalCulation();
                    break;


                case "RptmgrpPayable":
                    this.gvmwisePayable.DataSource = dt;
                    this.gvmwisePayable.DataBind();
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
                case "RptSuppPayable":

                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("rescode='AAAAAAAAAAAA'");
                    DataTable dt1 = dv.ToTable();

                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(opnam)", "")) ?
                                  0 : dt1.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFperiodAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(periodam)", "")) ?
                                  0 : dt1.Compute("sum(periodam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFnetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netbal)", "")) ?
                                  0 : dt1.Compute("sum(netbal)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "RptgrpPayable":

                    ((Label)this.gvPayableSum.FooterRow.FindControl("lgvFOpeninggp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                  0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableSum.FooterRow.FindControl("lgvFperiodAmtgp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(periodam)", "")) ?
                                  0 : dt.Compute("sum(periodam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableSum.FooterRow.FindControl("lgvFnetpayableAmtgp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netbal)", "")) ?
                                  0 : dt.Compute("sum(netbal)", ""))).ToString("#,##0;(#,##0); ");
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
        protected void gvPayableStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label desc = (Label)e.Row.FindControl("lgvdesc");
                Label Opening = (Label)e.Row.FindControl("lgvOpening");
                Label periodAmt = (Label)e.Row.FindControl("lgvperiodAmt");
                Label netpayableAmt = (Label)e.Row.FindControl("lgvnetpayableAmt");



                string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (rescode == "")
                {
                    return;
                }
                if (ASTUtility.Right(rescode, 4) == "AAAA")
                {

                    desc.Font.Bold = true;
                    Opening.Font.Bold = true;
                    periodAmt.Font.Bold = true;
                    netpayableAmt.Font.Bold = true;
                    desc.Style.Add("text-align", "right");


                }

            }
        }

        protected void gvPayableSum_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink group = (HyperLink)e.Row.FindControl("hlnkgvgroup");
                string mwgcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mwgcod")).ToString();
                string groupdesc = ((HyperLink)e.Row.FindControl("hlnkgvgroup")).Text.Trim();
                group.NavigateUrl = "~/F_14_Pro/LinkSupPayableStatus.aspx?Type=RptSuppPayable&Grp=" + mwgcod + "&Grpdesc=" + groupdesc + "&Date1=" + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            }
        }




        protected void gvmwisePayable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink group = (HyperLink)e.Row.FindControl("hlnkgvgroupmw");
                Label Openinggpmw = (Label)e.Row.FindControl("lgvOpeninggpmw");
                Label periodAmtgpmw = (Label)e.Row.FindControl("lgvperiodAmtgpmw");
                Label payableAmtgpmw = (Label)e.Row.FindControl("lgvnetpayableAmtgpmw");

                string mwgcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mwgcod")).ToString();
                if (mwgcod == "")
                    return;
                if (ASTUtility.Right(mwgcod, 2) == "AA")
                {
                    group.Font.Bold = true;
                    Openinggpmw.Font.Bold = true;
                    periodAmtgpmw.Font.Bold = true;
                    payableAmtgpmw.Font.Bold = true;

                }
                else
                {
                    string groupdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mwgdesc")).ToString();
                    group.NavigateUrl = "~/F_14_Pro/LinkSupPayableStatus.aspx?Type=RptSuppPayable&Grp=" + mwgcod + "&Grpdesc=" + groupdesc + "&Date1=" + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }

            }
        }
    }
}