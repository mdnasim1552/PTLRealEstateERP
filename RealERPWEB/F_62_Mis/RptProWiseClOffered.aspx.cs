using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using System.Net.Mail;
namespace RealERPWEB.F_62_Mis
{
    public partial class RptProWiseClOffered : System.Web.UI.Page
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "SalesDeamnd") ? "Sales Demand Analysis"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "SalesDeci") ? "Sales Decision" : "Client Capacity Analysis";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.SelectView();

            }
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalesDeamnd":
                    break;

                case "SalesDeci":
                    this.chkboxlist.Visible = true;
                    break;
                case "Capacity":
                    this.PanelCapacity.Visible = true;
                    this.txtAmountC1.Text = "10000000";
                    this.ddlSrchCash.SelectedIndex = 3;
                    break;




            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }




        private void GetProjectName()
        {

            string comcod = this.GetCompCode();

            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_CLIENT_INFORMATION", "GETPROJECTNAME", "", txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();



        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblcloffer");
            string comcod = this.GetCompCode();
            string ProjectCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string searchinfo = "";
            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(tuamt between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( tuamt " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }

            string Calltype = (this.Request.QueryString["Type"].ToString().Trim() == "SalesDeamnd") ? "RPRCLOFERED"
                : (this.Request.QueryString["Type"].ToString().Trim() == "SalesDeci") ? "RPRCLOFEREDHBOOFF" : "RPRCLOFEREDCAPACITY";
            string hboff = (this.chkboxlist.SelectedIndex == 0) ? "hoff" : (this.chkboxlist.SelectedIndex == 1) ? "" : "";
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_CLIENT_INFORMATION", Calltype, ProjectCode, searchinfo, hboff, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvClientOff.DataSource = null;
                this.gvClientOff.DataBind();
                return;

            }
            Session["tblcloffer"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalesDeamnd":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string usircode = dt1.Rows[0]["usircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode)
                        {
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["udesc"] = "";
                            dt1.Rows[j]["usize"] = 0;
                            dt1.Rows[j]["rate"] = 0;
                            dt1.Rows[j]["uamt"] = 0;
                            dt1.Rows[j]["paothamt"] = 0;
                            dt1.Rows[j]["tuamt"] = 0;
                            dt1.Rows[j]["facing"] = "";
                            dt1.Rows[j]["uview"] = "";

                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                                dt1.Rows[j]["pactdesc"] = "";




                            if (dt1.Rows[j]["usircode"].ToString() == usircode)
                            {
                                dt1.Rows[j]["udesc"] = "";
                                dt1.Rows[j]["usize"] = 0;
                                dt1.Rows[j]["rate"] = 0;
                                dt1.Rows[j]["uamt"] = 0;
                                dt1.Rows[j]["paothamt"] = 0;
                                dt1.Rows[j]["tuamt"] = 0;
                                dt1.Rows[j]["facing"] = "";
                                dt1.Rows[j]["uview"] = "";
                            }



                        }
                        pactcode = dt1.Rows[j]["pactcode"].ToString();
                        usircode = dt1.Rows[j]["usircode"].ToString();


                    }

                    break;

                case "SalesDeci":
                    this.chkboxlist.Visible = true;
                    break;
                case "Capacity":
                    this.PanelCapacity.Visible = true;
                    break;

            }



            return dt1;
        }




        private void Data_Bind()
        {
            this.gvClientOff.DataSource = (DataTable)Session["tblcloffer"];
            this.gvClientOff.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.gvClientOff.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblcloffer"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvClientOff.FooterRow.FindControl("lgvFusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usize)", "")) ? 0.00 : dt.Compute("sum(usize)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvClientOff.FooterRow.FindControl("lgvFuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uamt)", "")) ? 0.00 : dt.Compute("sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvClientOff.FooterRow.FindControl("lgvFcparaothamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paothamt)", "")) ? 0.00 : dt.Compute("sum(paothamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvClientOff.FooterRow.FindControl("lgvFtuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tuamt)", "")) ? 0.00 : dt.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");



        }

        protected void lnkprint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.RptOfferedPriceAllPro();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            CompName.Text = comnam;

            TextObject txtHeader = rptAppMonitor.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = (this.Request.QueryString["Type"].ToString().Trim() == "SalesDeamnd") ? "Sales Demand Analysis"
                : (this.Request.QueryString["Type"].ToString().Trim() == "SalesDeci") ? "Sales Decision - " + ((this.chkboxlist.SelectedIndex == 0) ? "Highest Offer" : "Highest Booking") : "Client Capacity Analysis";



            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "Date: As on " + this.txtDate.Text;
            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.SetDataSource((DataTable)Session["tblcloffer"]);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblToCash.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.txtAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
        }
        protected void gvClientOff_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (this.Request.QueryString["Type"].ToString().Trim() == "Capacity")
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label unitname = (Label)e.Row.FindControl("lgunitname");
                    Label lName = (Label)e.Row.FindControl("lgClName");


                    string selescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "salescode")).ToString();

                    if (selescode == "")
                    {
                        return;
                    }
                    if (selescode != "000000000000")
                    {


                        unitname.Style.Add("color", "red");
                        lName.Style.Add("color", "red");
                    }



                }
            }
        }
        protected void gvClientOff_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvClientOff.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}