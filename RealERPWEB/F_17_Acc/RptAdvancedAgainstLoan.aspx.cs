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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAdvancedAgainstLoan : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Advance Againts Loan";

                var dtoday = System.DateTime.Today;
                this.txttodate.Text = dtoday.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = new System.DateTime(dtoday.Year, dtoday.Month, 1).ToString("dd-MMM-yyyy");
                this.DepartName();
            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }


        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void ibtnFindSupply_OnClick(object sender, EventArgs e)
        {
            this.DepartName();
        }


        private void DepartName()
        {
            string comcod = this.GetComeCode();
            string SrchSupplier = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETDEPARTEMTNAME", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlDeptName.DataTextField = "deptname";
            this.ddlDeptName.DataValueField = "deptcode";
            this.ddlDeptName.DataSource = ds1.Tables[0];
            this.ddlDeptName.DataBind();
            ViewState["tbldept"] = ds1.Tables[0];
        }



    protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            try
            {
                Session.Remove("tblsupinfo");
                string comcod = this.GetComeCode();
                string frmdate = txtfrmdate.Text.ToString();
                string todate = txttodate.Text.ToString();
                string deptcode = this.ddlDeptName.SelectedValue.ToString() == "000000000000" ? "94%" : this.ddlDeptName.SelectedValue.ToString() + "%";
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTADVANCEDLOAN", frmdate, todate, deptcode, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvadvLoan.DataSource = null;
                    this.gvadvLoan.DataBind();
                    return;
                }
               // Session["tblsupinfo"] = ds1.Tables[0];
                Session["tbladvloan"] = HiddenSameData(ds1.Tables[0]);
                this.DataBindGrid();
            }
            catch (Exception ex)
            {

            }

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();
            string deptcode = dt1.Rows[0]["spcfcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode )
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                    if ( dt1.Rows[j]["spcfcode"].ToString() == deptcode)
                    {
                        deptcode = dt1.Rows[j]["spcfcode"].ToString();
                        dt1.Rows[j]["deptname"] = "";
                    }
                    else
                    {
                        
                        deptcode = dt1.Rows[j]["spcfcode"].ToString();
                    }
                   
                }
                else
                {
                   
                    actcode = dt1.Rows[j]["actcode"].ToString();

                    if (dt1.Rows[j]["spcfcode"].ToString() == deptcode)
                    {
                        deptcode = dt1.Rows[j]["spcfcode"].ToString();
                        dt1.Rows[j]["deptname"] = "";
                    }
                    else
                    {

                        deptcode = dt1.Rows[j]["spcfcode"].ToString();
                    }

                }
            }
            return dt1;

        }

        private void DataBindGrid()
        {
            // this.MultiView1.ActiveViewIndex = 0;
            try
            {
                DataTable dt = (DataTable)Session["tbladvloan"];
                this.gvadvLoan.DataSource = dt;
                this.gvadvLoan.DataBind();
                if (dt.Rows.Count == 0)
                    return;
                Session["Report1"] = gvadvLoan;
                ((HyperLink)this.gvadvLoan.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception ex)
            {

            }
            //Session["Report1"] = gvsupstatus;
            //((HyperLink)this.gvsupstatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbladvloan"];
           
            LocalReport Rpt1 = new LocalReport();
          
                var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptAdvancedAgainstLoan>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAdvancedAgainstLoan", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Advance Againts Loan"));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            



        }

        protected void gvadvLoan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvempname = (Label)e.Row.FindControl("lblgvempname");
                Label lblgvopndr = (Label)e.Row.FindControl("lblgvopndr");
                Label lblgvopncr = (Label)e.Row.FindControl("lblgvopncr");
                Label lblgvcurrentdr = (Label)e.Row.FindControl("lblgvcurrentdr");
                Label lblgvcurrentcr = (Label)e.Row.FindControl("lblgvcurrentcr");
                Label lblgvclsdr = (Label)e.Row.FindControl("lblgvclsdr");
                Label lblgvclscr = (Label)e.Row.FindControl("lblgvclscr");
                Label lblgvnetamt = (Label)e.Row.FindControl("lblgvnetamt");
                Label lblgvdrcr = (Label)e.Row.FindControl("lblgvdrcr");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString().Trim();

                if (grp == "")
                {
                    return;
                }

                if (grp == "B")
                {


                    lblgvempname.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvopndr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvopncr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvcurrentdr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvcurrentcr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvclsdr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvclscr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvnetamt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvdrcr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";




                    lblgvempname.Style.Add("text-align", "right");

                }
                if (grp == "C")
                {


                    lblgvempname.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Green;";
                    lblgvopndr.Attributes["style"] = "font-weight:bold; font-size: 15px;  color:Green;";
                    lblgvopncr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Green;";
                    lblgvcurrentdr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Green;";
                    lblgvcurrentcr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Green;";
                    lblgvclsdr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Green;";
                    lblgvclscr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Green;";
                    lblgvnetamt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Green;";
                    lblgvdrcr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Green;";


                    lblgvempname.Style.Add("text-align", "right");

                }
                if (grp == "D")
                {


                    lblgvempname.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvopndr.Attributes["style"] = "font-weight:bold; font-size: 15px;  color:Orange;";
                    lblgvopncr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvcurrentdr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvcurrentcr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvclsdr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvclscr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvnetamt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvdrcr.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";


                    lblgvempname.Style.Add("text-align", "right");

                }

            }
        }
    }




}