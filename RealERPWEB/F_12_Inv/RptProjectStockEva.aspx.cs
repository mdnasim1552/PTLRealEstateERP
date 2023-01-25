using System;
using System.Linq;
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
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_12_Inv
{
    public partial class RptProjectStockEva : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                // Session.Remove("Unit");
                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS STOCK REPORT EVALUATION";

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetMaterial();
                if (Request.QueryString.AllKeys.Contains("prjcode") && this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);

                }
            }
        }

        private string Complength()
        {
            string comcod = this.GetCompCode();
            string Complength = "";
            switch (comcod)
            {
                // case "3101":
                case "3348":
                    Complength = "Length";
                    break;

                default:
                    Complength = "";

                    break;
            }

            return Complength;


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            this.gvStocjEvaluation.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvStocjEvaluation.DataSource = dt;
            this.gvStocjEvaluation.DataBind();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.MatStockEva();
        }

        

        private void MatStockEva()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProName.SelectedValue.ToString() + "%";

            string resListMulti = "";
            string resourcelist = this.chkResourcelist.SelectedValue.ToString();



            if (resourcelist == "000000000000")
                resListMulti = "";
            else

                foreach (ListItem item in chkResourcelist.Items)
                {
                    if (item.Selected)
                    {
                        resListMulti += item.Value;
                    }
                }
           

            //foreach (ListItem item in chkResourcelist.Items)
            //{
            //    if (item.Selected)
            //    {
            //        resListMulti += item.Value;
            //    }
            //}

            string group = this.group.SelectedValue.ToString();


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_MAT_STOCK", "GETSTOCKVALUATIONMATWISE", fdate, tdate, pactcode, resListMulti, group, "", "", "", "", "", "");

            Session["tblRptMatStc"] = ds1.Tables[0];

            Session["tbMatStc"] = HiddenSameData(ds1.Tables[0]);
            //Session["tbMatStc"] = ds1.Tables[0];
            DataTable dt = ds1.Tables[0];

            this.gvStocjEvaluation.DataSource = dt;
            this.gvStocjEvaluation.DataBind();
            //this.FooterCalculation();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string isircod = dt1.Rows[0]["mrsircode"].ToString();
            for (int i = 1; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["mrsircode"].ToString() == isircod)
                {
                    dt1.Rows[i]["msirdesc"] = "";
                }
                isircod = dt1.Rows[i]["mrsircode"].ToString();
            }

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == isircod)
                {
                    //string strstkValuation = this.group.SelectedValue.ToString();
                    //if (strstkValuation != "1")
                    //{
                    //    dt1.Rows[0]["pactdesc"] = "";
                    //}
                    //else
                    //{
                        
                    //}
                    dt1.Rows[1]["pactdesc"] = "";
                    dt1.Rows[j]["pactdesc"] = "";
                }

                isircod = dt1.Rows[j]["pactcode"].ToString();
            }
            return dt1;
        }


        private void GetMaterial()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%%" : "%" + this.ddlProName.SelectedValue.ToString() + "%";
            string txtfindMat = this.txtsrchresource.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIALEVA", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.chkResourcelist.DataTextField = "rsirdesc";
            this.chkResourcelist.DataValueField = "rsircode";
            this.chkResourcelist.DataSource = ds1.Tables[0];
            this.chkResourcelist.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.chkResourcelist.Text = this.Request.QueryString["prjcode"].Length > 0 ? "000000000000" : "";
            }

            ds1.Dispose();
        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            string length = this.Complength();
            string userid = hst["usrid"].ToString();
            string ctype = this.CompCallType();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", ctype, serch1, length, userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.ddlProName.SelectedValue = this.Request.QueryString["prjcode"].Length > 0 ? this.Request.QueryString["prjcode"] : "";
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

            Hashtable hst = (Hashtable)Session["tblLogin"];

            this.RtpStockReportEva();
        }


        private void RtpStockReportEva()//09-May-2020
        {
            DataTable dt = (DataTable)Session["tblRptMatStc"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            

            
            string strstkValuation = this.group.SelectedValue.ToString();
            string strstock = "";
            if (strstkValuation == "1")
            {
                strstock = "( Group Wise )";
            }
            else 
            {
                strstock = "( Material Wise )";
            }

            
            //DataTable dt1 = (DataTable)Session["tblRptMatStc"];
            DataTable dt1 = (DataTable)Session["tbMatStc"];

            if (comcod == "3315" || comcod == "3316")
            {
                DataView dv = dt1.DefaultView; //only Assure
                dv.RowFilter = ("tqty<>0 or opqty<>0 or rcvqty<>0 or trninqty<>0 or trnoutqty<>0");
                dt1 = dv.ToTable();
            }

            if (dt1 == null)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.MatStockReportEvaluation>();
            LocalReport Rpt1 = new LocalReport();

            if (strstkValuation == "1")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptGroupStockEva", lst, null, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptMaterialStockEva", lst, null, null);
            }


            Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("StockValuation", "Stock Valuation : " + strstock));

            //Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name : " + this.ddlProName.SelectedItem.Text));

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        private string CompCallType()
        {
            string comcod = this.GetCompCode();
            string ctype = "";
            switch (comcod)
            {
                case "3101":
                case "2325":
                case "3325":
                case "3370":
                    ctype = "GETPURPROJECTNAMELEISURE";
                    break;
                default:
                    ctype = "GETPURPROJECTNAME";
                    break;
            }
            return ctype;
        }

        protected void lbtnresource_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }



        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tbMatStc"];

            if (dt.Rows.Count > 0)
            {
                //((Label)this.gvStocjEvaluation.FooterRow.FindControl("lgvttaccrecvbale")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actstock)", "")) ? 0.00 : dt.Compute("Sum(actstock)", ""))).ToString("#,##0.00;(#,##0.00); ");
                //((Label)this.gvStocjEvaluation.FooterRow.FindControl("lgvttlsolamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(percnt)", "")) ? 0.00 : dt.Compute("Sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ");


                //Session["Report1"] = gvMatStock;
                //((HyperLink)this.gvMatStock.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            }

            else
            {
                return;
            }
        }

        protected void gvStocjEvaluation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvStocjEvaluation.PageIndex = e.NewPageIndex;
            this.ddlpagesize_SelectedIndexChanged(null, null);
        }

        protected void gvStocjEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            //GridViewRow gvRow = e.Row;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            Label RecDesc = (Label)e.Row.FindControl("lblActualStock");
            string msirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msirdesc")).ToString();

            if (msirdesc == "")
            {
                return;
            }
            else
            {
                RecDesc.Font.Bold = true;
            }

        }
    }
}