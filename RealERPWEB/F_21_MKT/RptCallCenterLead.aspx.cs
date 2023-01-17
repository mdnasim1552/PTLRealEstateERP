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
using RealERPRDLC;
namespace RealERPWEB.F_21_MKT
{
    public partial class RptCallCenterLead : System.Web.UI.Page
    {
        ProcessAccess prjData = new ProcessAccess();
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Date = "";
                Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "SourceWise") ? "Source Wise Leads" : "Sales Person Wise Leads";
                this.SelectView();

            }


        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "SourceWise":
                    this.CallCenteReport();
                    break;

                case "SalespWise":
                    this.PrintCallCenterLeadSalesWise();
                    break;

            }





        }


        protected void PrintCallCenterLeadSalesWise()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string reqno = this.Request.QueryString["reqno"].ToString();

            // DataTable dt = ((DataTable)Session["tbltranstrk"]);

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string Date = frmdate + " To " + toDate;

            DataTable dt = ((DataTable)Session["tblCallCenter"]);
            var lst = dt.DataTableToList<RealEntity.C_21_Mkt.EClassAdvertisement.EClassRptCallCenterLead>();

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rpttxtCompanyName = comnam;

            string txtTitle = "Lead Status Report";



            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);



            LocalReport Rpt1 = new LocalReport();




            Rpt1 = RptSetupClass1.GetLocalReport("R_21_Mkt.RptCallCenterLead", lst, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtTitle", txtTitle));
            Rpt1.SetParameters(new ReportParameter("Date", Date));

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        //protected void Page_PreInit(object sender, EventArgs e)

        //{
        //    // Create an event handler for the master page's contentCallEvent event
        //    ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        //    //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;




        private void CallCenteReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string date = " From " + frmdate + " To " + toDate;
            DataTable dt = (DataTable)Session["tblCallCenter"];
            DataTable dt1 = (DataTable)ViewState["tblCalldesc"];
            var list = dt.DataTableToList<RealEntity.C_21_Mkt.ESourceWiseLeadsclass.CallCenterLeads>();
            //var list2 = dt1.DataTableToList<RealEntity.C_21_Mkt.ESourceWiseLeadsclass.CallCenter>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_21_Mkt.RptSourceWiseLeads", list, null, null);

            //  string textbox=(dt1.Select("sourdesc=0").Length==0)?"":dt1.Rows[0]["sourdesc"].ToString();
            int i = 1;
            foreach (DataRow dr1 in dt1.Rows)
            {
                string textbox = "txtp" + i.ToString();
                Rpt1.SetParameters(new ReportParameter(textbox, dr1["sourdesc"].ToString()));
                i++;
                if (i == 8)
                    break;

            }
            Rpt1.SetParameters(new ReportParameter("comnam", comname));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Source Wise Call Center Report"));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }




        private void SelectView()
        {

            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "SourceWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "SalespWise":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }



        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "SourceWise":
                    this.ShowSourceWiseLead();
                    break;

                case "SalespWise":
                    this.ShowSalesPWiseLead();
                    break;

            }




        }
        private void ShowSourceWiseLead()
        {

            Session.Remove("tbPrjStatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = prjData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_MIS", "GETCALLCENTERLEADREPORT", frmdate, toDate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvCallCenter.DataSource = null;
                this.gvCallCenter.DataBind();
                return;

            }

            Session["tblCallCenter"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tblCalldesc"] = ds1.Tables[1];
            this.Data_Bind();


        }

        private void ShowSalesPWiseLead()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = prjData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_MIS", "RPTSALESPWISELEADSTATUS", frmdate, toDate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvsalpst.DataSource = null;
                this.gvsalpst.DataBind();
                return;

            }

            Session["tblCallCenter"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                //case "SourceWise":

                //    string gcod = dt1.Rows[0]["gcod"].ToString();
                //    for (int j = 1; j < dt1.Rows.Count; j++)
                //    {
                //        if (dt1.Rows[j]["gcod"].ToString() == gcod)

                //            dt1.Rows[j]["gdesc"] = "";
                //        gcod = dt1.Rows[j]["gcod"].ToString();
                //    }


                //    break;

                case "SalespWise":
                    string clustid = dt1.Rows[0]["clustid"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["clustid"].ToString() == clustid)
                            dt1.Rows[j]["clustname"] = "";


                        clustid = dt1.Rows[j]["clustid"].ToString();

                    }


                    break;

            }



            return dt1;


        }




        private void Data_Bind()
        {


            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "SourceWise":



                    DataTable dtpname = (DataTable)ViewState["tblCalldesc"];
                    int i,  j = 3;

                    for (i = 3; i < this.gvCallCenter.Columns.Count-1; i++)
                        this.gvCallCenter.Columns[i].Visible = false;
                   
                    
                    
                    
                    for (i = 0; i < dtpname.Rows.Count; i++)
                    {
                        this.gvCallCenter.Columns[j].Visible = true;
                        this.gvCallCenter.Columns[j].HeaderText = dtpname.Rows[i]["sourdesc"].ToString();
                        j++;
                      


                    }

                    this.gvCallCenter.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCallCenter.DataSource = (DataTable)Session["tblCallCenter"];
                    this.gvCallCenter.DataBind();
                    this.FooteCalculation();
                    break;

                case "SalespWise":
                    this.gvsalpst.DataSource = (DataTable)Session["tblCallCenter"];
                    this.gvsalpst.DataBind();
                    this.FooteCalculation();
                    break;

            }





        }

        private void FooteCalculation()
        {





            DataTable dt = (DataTable)Session["tblCallCenter"];
            if (dt.Rows.Count == 0)
                return;




            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "SourceWise":
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p1)", "")) ? 0.00 : dt.Compute("sum(p1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p2)", "")) ? 0.00 : dt.Compute("sum(p2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p3)", "")) ? 0.00 : dt.Compute("sum(p3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p4)", "")) ? 0.00 : dt.Compute("sum(p4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p5)", "")) ? 0.00 : dt.Compute("sum(p5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p6)", "")) ? 0.00 : dt.Compute("sum(p6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p7)", "")) ? 0.00 : dt.Compute("sum(p7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p8)", "")) ? 0.00 : dt.Compute("sum(p8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p9)", "")) ? 0.00 : dt.Compute("sum(p9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p10)", "")) ? 0.00 : dt.Compute("sum(p10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p11)", "")) ? 0.00 : dt.Compute("sum(p11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p12)", "")) ? 0.00 : dt.Compute("sum(p12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p13)", "")) ? 0.00 : dt.Compute("sum(p13)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p14)", "")) ? 0.00 : dt.Compute("sum(p14)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p15)", "")) ? 0.00 : dt.Compute("sum(p15)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p16)", "")) ? 0.00 : dt.Compute("sum(p16)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p17)", "")) ? 0.00 : dt.Compute("sum(p17)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p18)", "")) ? 0.00 : dt.Compute("sum(p18)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p19)", "")) ? 0.00 : dt.Compute("sum(p19)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFP20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p20)", "")) ? 0.00 : dt.Compute("sum(p20)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCallCenter.FooterRow.FindControl("lgvFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(total)", "")) ? 0.00 : dt.Compute("sum(total)", ""))).ToString("#,##0;(#,##0); ");

                    if (dt.Rows.Count > 0)
                    {
                        Session["Report1"] = gvCallCenter;
                        ((HyperLink)this.gvCallCenter.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    }
                    break;

                case "SalespWise":

                    ((Label)this.gvsalpst.FooterRow.FindControl("lgvFtotallead")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totlead)", "")) ? 0.00 : dt.Compute("sum(totlead)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalpst.FooterRow.FindControl("lgvFpvisitd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pvisitd)", "")) ? 0.00 : dt.Compute("sum(pvisitd)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalpst.FooterRow.FindControl("lgvFpvisits")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pvisits)", "")) ? 0.00 : dt.Compute("sum(pvisits)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalpst.FooterRow.FindControl("lgvFcmeetingd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cmeetingd)", "")) ? 0.00 : dt.Compute("sum(cmeetingd)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalpst.FooterRow.FindControl("lgvFcmeetings")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cmeetings)", "")) ? 0.00 : dt.Compute("sum(cmeetings)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalpst.FooterRow.FindControl("lgvFfollowup")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(followup)", "")) ? 0.00 : dt.Compute("sum(followup)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalpst.FooterRow.FindControl("lgvFjunk")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(junk)", "")) ? 0.00 : dt.Compute("sum(junk)", ""))).ToString("#,##0;(#,##0); ");

                    break;


            }



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvCallCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCallCenter.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}
