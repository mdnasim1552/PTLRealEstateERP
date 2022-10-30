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
using System.Drawing;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_01_LPA
{
    public partial class RptAllProTopSheet : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Land Data Bank";
                this.Getcatagory();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            this.ShowReport();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //string comcod = this.GetComCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string category1 = this.ddlcatag.SelectedItem.Text.Trim().ToString();

            //DataTable dt = (DataTable)Session["tblfeaprjLand"];

            //LocalReport Rpt1 = new LocalReport();

            //var lst = dt.DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.Landdatabank01>();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_01_LPA.RptlandDataBank", lst, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("RptTital", "LAND DATA BANK"));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("category1", "Category Name: " + category1));
            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        //protected void lnkPrint_Click(object sender, EventArgs e)
        //{
        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = hst["comcod"].ToString();
        //    //string comname = hst["comnam"].ToString();
        //    //string comadd = hst["comadd1"].ToString();
        //    //string compname = hst["compname"].ToString();
        //    //string username = hst["username"].ToString();
        //    //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    //string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
        //    //ReportDocument rpcp = new RealERPRPT.R_01_LPA.RptFeaLandDevProSummary();
        //    //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    //CompName.Text = comname;
        //    //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
        //    //txtPrjName.Text = prjname;
        //    //TextObject txtDu = rpcp.ReportDefinition.ReportObjects["txtDu"] as TextObject;
        //    //txtDu.Text = this.lblDuration.Text;


        //    //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    //if (ConstantInfo.LogStatus == true)
        //    //{
        //    //    string eventtype = this.lblHeader.Text;
        //    //    string eventdesc = "Print Report";
        //    //    //string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
        //    //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc,"");
        //    //}

        //    //rpcp.SetDataSource((DataTable)Session["tblfeaprjLand"]);
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rpcp.SetParameterValue("ComLogo", ComLogo);
        //    //Session["Report1"] = rpcp;
        //    //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //    //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}

        private void Getcatagory()
        {
            string comcod = this.GetComCode();
            DataSet dscatg = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATEGORYALL", "", "", "", "", "", "", "", "", "");
            ddlcatag.DataTextField = "prgdesc";
            ddlcatag.DataValueField = "prgcod";
            ddlcatag.DataSource = dscatg.Tables[0];
            ddlcatag.DataBind();
        }

        private void ShowReport()
        {
            Session.Remove("tblfeaprjLand");
            string comcod = this.GetComCode();
            string frmdate = this.txtDate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            //string catcode = this.ddlcatag.SelectedValue.ToString() + "%";
            string catcode = (this.ddlcatag.SelectedValue == "00000" ? "99" : this.ddlcatag.SelectedValue) + "%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_LP_PROFEASIBILITY", "RPTALLPROTOPSHEET", frmdate, todate, catcode, "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaPrjLand.DataSource = null;
                this.gvFeaPrjLand.DataBind();
                return;
            }
            Session["tblfeaprjLand"] = ds2.Tables[0];
            this.Data_Bind();

        }






        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            this.gvFeaPrjLand.DataSource = dt;
            this.gvFeaPrjLand.DataBind();
            this.FooterCalCulation();

        }


        private void FooterCalCulation()
        {

            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvFeaPrjLand.FooterRow.FindControl("lgvFrevenue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salamt)", "")) ? 0.00 : dt.Compute("sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjLand.FooterRow.FindControl("lgvFcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tocost)", "")) ? 0.00 : dt.Compute("sum(tocost)", ""))).ToString("#,##0;(#,##0); ");


        }


        protected void gvFeaPrjLand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvFeaPrjLand_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnPreEntry");


                HyperLink npper = (HyperLink)e.Row.FindControl("hlnkgvnpper");
                HyperLink minfo = (HyperLink)e.Row.FindControl("hlnkgvinfo");
                Label lgvprost = (Label)e.Row.FindControl("lgvprost");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string prjdone = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prjdone")).ToString();
                if (code == "")
                {
                    return;
                }

                if (prjdone == "Done")
                {
                    lgvprost.Attributes["style"] = "color:green; font-weight:bold; ";

                }



                //Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatosaleamt")).ToString()).ToString("#,##0;(#,##0);");
                string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                npper.Style.Add("color", "blue");
                minfo.Style.Add("color", "blue");
                //npper.NavigateUrl = "~/F_02_Fea/RptProjectFeasibility.aspx?pactcode=" + code + "&pactdesc=" + pactdesc;
                npper.NavigateUrl = "~/F_02_Fea/RptProjectFeasibility.aspx?Type=Report&prjcode=" +code;
                minfo.NavigateUrl = "~/F_01_LPA/PriLandProposal.aspx?Type=Report&prjcode=" + code;
               
                hlink2.NavigateUrl = "~/F_02_Fea/RptProjectFeasibility.aspx?Type=Report&prjcode=";
                hlink3.NavigateUrl = "~/F_08_PPlan/PrjCompFlowchart.aspx";





            }


        }
    }
}