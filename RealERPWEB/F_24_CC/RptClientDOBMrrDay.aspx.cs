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
namespace RealERPWEB.F_24_CC
{
    public partial class RptClientDOBMrrDay : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            //    ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "prosClient") ? "Prospective Client List " :
            //(this.Request.QueryString["Type"].ToString().Trim() == "ClientBrthDay") ? "Client's Birthday" :
            //(this.Request.QueryString["Type"].ToString().Trim() == "ClientMrgDay") ? "Client's Marriage Anniversary" : "";


                this.SectionView();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "prosClient":
                    this.MultiView1.ActiveViewIndex = 0;
                    //this.GetProsClientList();
                    break;

                case "ClientBrthDay":
                    this.MultiView1.ActiveViewIndex = 1;
                    txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lbtnShowBrthDay_Click(null, null);
                    break;

                case "ClientMrgDay":
                    this.MultiView1.ActiveViewIndex = 2;
                    TxtmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lbtnShowMrgDay_Click(null, null);
                    break;

            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void lbtnShowBrthDay_Click(object sender, EventArgs e)
        {
            Session.Remove("tblClientBrthDay");
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            string comcod = this.GetComeCode();
            string date = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM");
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_CUSTCHARE", "GETCUSTBIRTHDAY", date, "", "", "", "", "", "", "", "");
            DataTable dt2 = ds2.Tables[0];
            if (dt2.Rows.Count == 0)
            { return; }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Today(" + Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyy") + ") is your " + dt2.Rows.Count + " Clients Birthday";

            }
            if (ds2 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Match Found";
                this.gvClientBrthDay.DataSource = null;
                this.gvClientBrthDay.DataBind();
                return;
            }

            Session["tblClientBrthDay"] = ds2.Tables[1];
            gvClientBrthDay.DataSource = ds2.Tables[1];
            gvClientBrthDay.DataBind();

        }

        protected void lbtnShowMrgDay_Click(object sender, EventArgs e)
        {
            Session.Remove("tblClientMrgDay");
            this.LblmMsg.Text = "";
            string comcod = this.GetComeCode();
            string date = Convert.ToDateTime(this.TxtmDate.Text.Trim()).ToString("dd-MMM");
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_REPORT_CUSTCHARE", "GETCUSTMARGDAY", date, "", "", "", "", "", "", "", "");
            //DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_CLIENT_INFORMATION", "GETCLIENTMARGDAY", date, "", "", "", "", "", "", "", "");

            DataTable dt3 = ds3.Tables[0];
            if (dt3.Rows.Count == 0)
            { return; }
            else
            {
                this.LblmMsg.Text = "Today (" + Convert.ToDateTime(this.TxtmDate.Text.Trim()).ToString("dd-MMM-yyy") + ") is your " + dt3.Rows.Count + " Clients Marriagday";
                //this.lblMd.Visible = true;
                //this.lblMd.Text = "The day (" + Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyy") + ") is the marriagday of the listed customer";
            }
            if (ds3 == null)
            {

                this.gvClientMrgDay.DataSource = null;
                this.gvClientMrgDay.DataBind();
                return;
            }
            if (ds3.Tables[0].Rows.Count == 0)
                this.LblmMsg.Text = "No Match Found";

            Session["tblClientMrgDay"] = ds3.Tables[1];
            gvClientMrgDay.DataSource = ds3.Tables[1];
            gvClientMrgDay.DataBind();


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)


            {
                case "prosClient":

                    this.printProsClntList();
                    return;

                case "ClientBrthDay":
                    this.PrintClientBrthDay();
                    return;

                case "ClientMrgDay":
                    this.PrintClientMrgDay();
                    return;

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = type;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void printProsClntList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblprosClient"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_21_Mkt.RptProsClient();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = "Prospective Client List";
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);

            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintClientBrthDay()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblClientBrthDay"];
            if (dt1.Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = dt1.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.ClientDOBMrrDay>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientDOBMrrDay", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Client Birthday List"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintClientMrgDay()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblClientMrgDay"];
            if (dt1.Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = dt1.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.ClientDOBMrrDay>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientDOBMrrDay", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Client Marriage Day List"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        //protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Data_Bind();
        //}
        //protected void gvProsClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvProsClient.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}
    }
}




