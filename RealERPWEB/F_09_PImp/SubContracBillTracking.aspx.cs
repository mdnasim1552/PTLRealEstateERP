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

namespace RealERPWEB.F_09_PImp
{
    public partial class SubContracBillTracking : System.Web.UI.Page
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

                //((Label)this.Master.FindControl("lblTitle")).Text = "Sub-Contractor Bill Tracking";

            
                this.SubConbillNo();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void ibtnFindSupply_OnClick(object sender, EventArgs e)
        {
            this.SubConbillNo();
        }


        private void SubConbillNo()
        {
            string comcod = this.GetComeCode();
            string SrchSupplier = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "GETSUBCONBILLNO", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlbillno.DataTextField = "lisuno1";
            this.ddlbillno.DataValueField = "lisuno";
            this.ddlbillno.DataSource = ds1.Tables[0];
            this.ddlbillno.DataBind();
            ViewState["tblSup"] = ds1.Tables[0];
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }     
            string grp = dt1.Rows[0]["grp"].ToString();
            string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                    dt1.Rows[j]["grpdesc"] = "";

                grp = dt1.Rows[j]["grp"].ToString();

            }
            return dt1;

        }



        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            Session.Remove("tblcontrack");
            string comcod = this.GetComeCode();
            string billno = this.ddlbillno.SelectedValue.ToString();         
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "GETSUBCONBILLTRACKING", billno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvconbilltrack.DataSource = null;
                this.gvconbilltrack.DataBind();

                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            Session["tblcontrack"] = ds1.Tables[0];
            this.Date_Bind();       
        }

        private void Date_Bind()
        {
       
            this.gvconbilltrack.DataSource = (DataTable)Session["tblcontrack"];
            this.gvconbilltrack.DataBind();

            //Session["Report1"] = gvsupstatus;
            //((HyperLink)this.gvsupstatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            /*
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string date1 = "From "+fromdate + " To " +todate; 

            DataTable dt = (DataTable)Session["tblstatus"];

            DataView dv1 = dt.Copy().DefaultView;
            dv1.RowFilter = ("checkdat1<>'1/1/1900 12:00:00 AM'");
            DataTable dt01 = dv1.ToTable();

            if (dt.Rows.Count == 0)
                return;
            string totalnreqQty = Convert.ToDouble((Convert.IsDBNull(dt01.Compute("sum(nreqQty)", "")) ? 0.00 :
                 dt01.Compute("sum(nreqQty)", ""))).ToString("#,##0;(#,##0); ");


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.DateWiseReqCheckHistory>();
            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptDateWiseReqCheckHistory", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("comName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Date Wise Requisition Check History"));
            rpt.SetParameters(new ReportParameter("date1", date1));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));
            rpt.SetParameters(new ReportParameter("totalnreqQty", totalnreqQty));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


             */
        }

        protected void gvconbilltrack_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string grpdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpdesc")).ToString().Trim();            
                if (grpdesc == "")
                {
                    
                    return;

                }
                else
                {
                    e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";                
                }

            }
        }
    }

}