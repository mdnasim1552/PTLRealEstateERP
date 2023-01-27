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
namespace RealERPWEB.F_12_Inv
{
    public partial class RptMatStock : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS STOCK REPORT";
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetMaterialName();
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
        private string Complength()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
        protected void GetProjectName()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            string length = this.Complength();
            string userid = hst["usrid"].ToString();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPURPROJECTNAME", serch1, length, userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
        }

        private void GetMaterialName()
        {
            ViewState.Remove("tblmat");
            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcMat.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETMATERIALS", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMatName.DataTextField = "rsirdesc";
            this.ddlMatName.DataValueField = "rsircode";
            this.ddlMatName.DataSource = ds1.Tables[0];
            this.ddlMatName.DataBind();
            ViewState["tblmat"] = ds1.Tables[0];
            ds1.Dispose();



        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.MatStock();
        }

        private void MatStock()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProName.SelectedValue.ToString();
            string rsircode = this.ddlMatName.SelectedValue.ToString();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTINDMATSTOCK", pactcode, rsircode, fdate, tdate, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();
                return;
            }
            Session["tbMatStc"] = ds1.Tables[0];
            this.Data_Bind();



        }





        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatStock.DataSource = dt;
            this.gvMatStock.DataBind();
            this.FooterCalculation();

        }


        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tbMatStc"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFinqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(inqty)", "")) ? 0.00 : dt.Compute("Sum(inqty)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFoutqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(outqty)", "")) ? 0.00 : dt.Compute("Sum(outqty)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFclsqty")).Text = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["clqty"]).ToString("#,##0.0000;(#,##0.0000); ");


        }

        //Crystal Report

        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{

        //    DataTable dt = (DataTable)Session["tbMatStc"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string fdate=this.txtfromdate.Text.ToString();
        //    string tdate=this.txttodate.Text.ToString();
        //    string rsircode = this.ddlMatName.SelectedValue.ToString();

        //    ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptMaterialsStock();


        //    TextObject txtCompName = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        //    txtCompName.Text = comnam;
        //    TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
        //    rptProjectName.Text ="Project: "+ this.ddlProName.SelectedItem.Text;

        //    TextObject txtMaterials =rptstk.ReportDefinition.ReportObjects["txtMaterials"] as TextObject;
        //    txtMaterials.Text = "Materials: " + this.ddlMatName.SelectedItem.Text + "   Unit:" + (((DataTable)ViewState["tblmat"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"]; ;


        //    TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    rpttxtdate.Text = "From: " + fdate + " To: " + tdate;

        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptstk.SetDataSource(dt);

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
        //        string eventdesc = "Print Report:";
        //        string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }

        //    Session["Report1"] = rptstk;

        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tbMatStc"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            string rsircode = this.ddlMatName.SelectedValue.ToString();

            string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.EMaterialsStock>();

            DataTable dt2 = (DataTable)Session["tbMatStc"];
            if (dt2.Rows.Count == 0)
                return;
            string inwards = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(inqty)", "")) ? 0.00 : dt2.Compute("Sum(inqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            string outwards = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(outqty)", "")) ? 0.00 : dt2.Compute("Sum(outqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            string closing = Convert.ToDouble(dt2.Rows[(dt.Rows.Count) - 1]["clqty"]).ToString("#,##0.00;(#,##0.00); ");



            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMaterialsStock", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Stock Details "));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name : " + this.ddlProName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));
            Rpt1.SetParameters(new ReportParameter("materials", this.ddlMatName.SelectedItem.Text + "   Unit:" + (((DataTable)ViewState["tblmat"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"]));
            Rpt1.SetParameters(new ReportParameter("txtinwards", inwards));
            Rpt1.SetParameters(new ReportParameter("txtoutwards", outwards));
            Rpt1.SetParameters(new ReportParameter("txtclosing", closing));

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



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ibtnFindmat_Click(object sender, EventArgs e)
        {
            this.GetMaterialName();
        }
    }
}

