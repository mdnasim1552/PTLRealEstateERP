using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.IO;

using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_07_Ten
{
    public partial class TASActAnalysis : System.Web.UI.Page
    {
        ProcessAccess tasData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }
            if (this.ddlProject.Items.Count == 0)
                this.ImgbtnFindProject_Click(null, null);
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {


            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintAnaLysis_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk1_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk1.Text == "Other P/U")
            {
                this.rbtnList1.SelectedIndex = -1;
                this.MultiView1.ActiveViewIndex = -1;
                this.lbtnOk1.Text = "Select P/U";
                this.txtProjectSearch.Enabled = true;
                this.ImgbtnFindProject.Enabled = true;
                this.ddlProject.Visible = true;
                this.lblProjectDesc.Visible = false;
                this.ChkCopyProject.Checked = false;
                this.ChkCopyProject_CheckedChanged(null, null);
                this.ChkCopyProject.Visible = false;

                this.ChkCopyTender.Checked = false;
                this.ChkCopyTender_CheckedChanged(null, null);
                this.ChkCopyTender.Visible = false;
                //this.lblProjectDesc2.Text = "";
                this.rbtnList1.Visible = false;
                this.gvAnalysis.PageIndex = 0;
                this.gvAnalysis.EditIndex = -1;
                this.gvAnalysis.DataSource = null;
                this.gvAnalysis.DataBind();
                return;
            }

            this.lbtnOk1.Text = "Other P/U";
            this.txtProjectSearch.Enabled = false;
            this.ImgbtnFindProject.Enabled = false;
            this.ddlProject.Visible = false;
            this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.lblProjectDesc.Width = this.ddlProject.Width;
            this.lblProjectDesc.Visible = true;
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();

            DataRow[] dr1 = ((DataTable)Session["tblPrjCod"]).Select("prjcod='" + PrjCod + "'");
            //this.lblProjectDesc2.Text = dr1[0]["infdesc"].ToString();




            // this.rbtnList1.Visible = true;
            this.CallAnalysisData(PrjCod);
            string Type = this.Request.QueryString["Type"].Trim();
            this.rbtnList1.SelectedIndex = (Type == "Input") ? 0 : 4;
            this.rbtnList1.Visible = (this.Request.QueryString["Type"].Trim() == "Input");
            this.ddlReports.SelectedIndex = (Type == "ResBasis") ? 0 : (Type == "WrkBasis") ? 1 : (Type == "IResBasis") ? 2 : (Type == "IWrkBasis") ? 3 : (Type == "Analysis") ? 4 : 0;
            this.rbtnList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnPrintAnaLysis_Click(object sender, EventArgs e)
        {
            string IndexReport = this.ddlReports.SelectedIndex.ToString();
            switch (IndexReport)
            {
                case "0":
                case "1":
                    this.Print_Resource();
                    break;
                case "2":
                    Print_InResource();
                    break;

                case "3":
                    Print_IndWork();
                    break;

                case "4":
                    this.Print_Analysis();
                    break;



            }

        }


        protected void ChkCopyTender_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCopyTender.Visible = this.ChkCopyTender.Checked;
            this.ChkCopyProject.Checked = false;
            this.PnlCopyProject.Visible = false;
        }

        protected void ChkCopyProject_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCopyProject.Visible = this.ChkCopyProject.Checked;
            this.ChkCopyTender.Checked = false;
            this.PnlCopyTender.Visible = false;
        }


        protected void ibtnCopyFindProject_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string srchTxt = this.txtSrcCopyPro.Text.Trim() + "%";
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "GETPROJECTNAME", pactcode, srchTxt, "", "", "", "", "", "", "");
            this.ddlCopyProjectName.DataTextField = "prjdesc1";
            this.ddlCopyProjectName.DataValueField = "prjcod";
            this.ddlCopyProjectName.DataSource = ds1.Tables[0];
            this.ddlCopyProjectName.DataBind();
            ds1.Dispose();
        }

        protected void ibtnCopyFindTender_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string srchTxt = this.txtSrcCopyPro.Text.Trim() + "%";
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRITASPRJLIST", pactcode, srchTxt, "", "", "", "", "", "", "", "");
            this.ddlCopyTenderName.DataTextField = "prjdesc1";
            this.ddlCopyTenderName.DataValueField = "prjcod";
            this.ddlCopyTenderName.DataSource = ds1.Tables[0];
            this.ddlCopyTenderName.DataBind();
            ds1.Dispose();
        }

        private void Print_Resource()
        {
            //***** Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["tblResource"];
            // ViewState["tblconarea"] = ds1.Tables[0]; // nayan
            DataTable dt1 = (DataTable)ViewState["tblconarea"];
            double totalCost1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0.00 : dt.Compute("sum(rptamt)", "")));

            double totalCost = (totalCost1 / 2);


            double conarea = Convert.ToDouble(dt1.Rows[0]["conarea"]);

            double CostPsft = (totalCost / conarea);

            LocalReport Rpt1 = new LocalReport();

            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BugbasAns>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptResourceBasis", lst, null, null);


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProject.SelectedItem.ToString().Substring(14)));
            Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.ToString()));
            Rpt1.SetParameters(new ReportParameter("CostAre", (this.ddlReports.SelectedIndex == 1) ? "Construction Area" : ""));
            Rpt1.SetParameters(new ReportParameter("TotalCost", totalCost.ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("ConstArea", conarea.ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("CostPerSFT", CostPsft.ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("Present", "100.00 %"));


            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Cost - " + ((this.ddlReports.SelectedIndex == 0) ? "Resource Basis" : "Work Basis")));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";










            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)Session["tblResource"];
            //ReportDocument rptResource = new RealERPRPT.R_04_Bgd.rptResourceBasis();

            //TextObject txtTitle = rptResource.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Budgeted Cost - " + ((this.ddlReports.SelectedIndex == 0) ? "Resource Basis" : "Work Basis");

            //TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtProName.Text = this.ddlProject.SelectedItem.ToString().Substring(14);
            //TextObject rpttxtFloor = rptResource.ReportDefinition.ReportObjects["txtfloor"] as TextObject;
            //rpttxtFloor.Text = this.ddlFloorListRpt.SelectedItem.ToString();
            //TextObject rpttxtConarea = rptResource.ReportDefinition.ReportObjects["txtConarea"] as TextObject;
            //rpttxtConarea.Text = (this.ddlReports.SelectedIndex == 1) ? "Construction Area" : "";
            //TextObject rpttxtvalConArea = rptResource.ReportDefinition.ReportObjects["txtvalConArea"] as TextObject;
            //rpttxtvalConArea.Text = ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalConArea")).Text;

            //TextObject rpttxtCostPerSFT = rptResource.ReportDefinition.ReportObjects["txtCostPerSFT"] as TextObject;
            //rpttxtCostPerSFT.Text = (this.ddlReports.SelectedIndex == 1) ? "Cost Per SFT" : "";
            //TextObject rpttxtvalCostPersft = rptResource.ReportDefinition.ReportObjects["txtvalCostPersft"] as TextObject;
            //rpttxtvalCostPersft.Text = ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalCostPsft")).Text;
            //TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Constraction Budget";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlReports.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptResource.SetDataSource(dt);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptResource.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptResource;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";

        }

        private void Print_InResource()
        {
            //*** Nayan
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();
            string ResCode = this.ddlRptRes.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "INDIVIDUALRES", ResCode, "", "", "", "", "", "", "", "");
            DataTable dt1 = (DataTable)Session["tblResource"];
            //string conarea = dt1.Rows[0]["conarea"].ToString();
            //string salarea = dt1.Rows[0]["salarea"].ToString();
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.IndiMateDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptInResource", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProject.SelectedItem.ToString().Substring(14)));
            Rpt1.SetParameters(new ReportParameter("Resource", ds1.Tables[0].Rows[0]["sirdesc"].ToString() + "  " + ds1.Tables[0].Rows[0]["sirunit"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "TENDER ANALYSIS"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod =this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string ResCode = this.ddlRptRes.SelectedValue.ToString();
            //DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "INDIVIDUALRES", ResCode, "", "", "", "", "", "", "", "");
            //DataTable dt = (DataTable)Session["tblResource"];
            //ReportDocument rptResource = new RealERPRPT.R_04_Bgd.rptInResource();
            //TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rpttxtComName.Text = comnam;
            //TextObject rpttxtHeader = rptResource.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //rpttxtHeader.Text = "TENDER ANALYSIS";
            //TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtProName.Text = "Project Name: " + this.ddlProject.SelectedItem.ToString().Substring(14);

            //TextObject rpttxtFloor = rptResource.ReportDefinition.ReportObjects["txtfloor"] as TextObject;
            //rpttxtFloor.Text = this.ddlFloorListRpt.SelectedItem.ToString();
            //TextObject rpttxtResourceName = rptResource.ReportDefinition.ReportObjects["rptResource"] as TextObject;
            //string ResourceUnit = ds1.Tables[0].Rows[0]["sirdesc"].ToString() + "  " + ds1.Tables[0].Rows[0]["sirunit"].ToString();
            //rpttxtResourceName.Text = ResourceUnit;
            //TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptResource.SetDataSource(dt);
            //Session["Report1"] = rptResource;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //     ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";

        }

        private void Print_IndWork()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string ItemCode = this.ddlRptItem.SelectedValue.ToString();
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "INDIVIDUALITMANDQTY", pactcode, ItemCode, flrcod, "", "", "", "", "", "");

            LocalReport Rpt1 = new LocalReport();

            DataTable dt1 = (DataTable)Session["tblResource"];
            //string conarea = dt1.Rows[0]["conarea"].ToString();
            //string salarea = dt1.Rows[0]["salarea"].ToString();
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.IndiMateDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptInWork", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProject.SelectedItem.ToString().Substring(14)));
            Rpt1.SetParameters(new ReportParameter("Resource", ds1.Tables[0].Rows[0]["sirdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("sunit", "Considered Quantity: " + Convert.ToDouble(ds1.Tables[0].Rows[0]["bgdqty"].ToString()).ToString("#,##0;(#,##0);") + "  " + ds1.Tables[0].Rows[0]["sirunit"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "TENDER ALALYSIS"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod =this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = this.ddlProject.SelectedValue.ToString();
            //string ItemCode = this.ddlRptItem.SelectedValue.ToString();
            //string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            //DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "INDITMANDQTY", pactcode, ItemCode, flrcod, "", "", "", "", "", "");
            //DataTable dt = (DataTable)Session["tblResource"];
            //ReportDocument rptInwrk = new RealERPRPT.R_04_Bgd.RptInWork();

            //TextObject rpttxtComName = rptInwrk.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rpttxtComName.Text = comnam;
            //TextObject rpttxtHeader = rptInwrk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //rpttxtHeader.Text = "TENDER ALALYSIS";
            //TextObject rpttxtProName = rptInwrk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtProName.Text = "Project Name: " + this.ddlProject.SelectedItem.ToString().Substring(14);

            //TextObject rpttxtFloor = rptInwrk.ReportDefinition.ReportObjects["txtfloor"] as TextObject;
            //rpttxtFloor.Text = this.ddlFloorListRpt.SelectedItem.ToString();
            //TextObject rpttxtResourceName = rptInwrk.ReportDefinition.ReportObjects["rptResource"] as TextObject;
            //rpttxtResourceName.Text = ds1.Tables[0].Rows[0]["sirdesc"].ToString();
            //TextObject rpttxtconqty = rptInwrk.ReportDefinition.ReportObjects["txtconqty"] as TextObject;
            //rpttxtconqty.Text = "Considered Quantity: " + Convert.ToDouble(ds1.Tables[0].Rows[0]["bgdqty"].ToString()).ToString("#,##0;(#,##0);") + "  " + ds1.Tables[0].Rows[0]["sirunit"].ToString(); ;


            //TextObject txtuserinfo = rptInwrk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptInwrk.SetDataSource(dt);
            //Session["Report1"] = rptInwrk;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //     ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        }

        protected void ImgbtnFindItem2_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string ddlItemID = ((LinkButton)sender).ID.ToString();
            string srchTxt = "%" + (ddlItemID.Contains("ImgbtnFindItem2") ? this.txtItemSearch2.Text.Trim() : this.txtItemSearch.Text.Trim()) + "%";
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "ITMCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblItmCod"] = ds1.Tables[0];
            DropDownList ddlItm = (ddlItemID.Contains("ImgbtnFindItem2") ? this.ddlItem2 : this.ddlItem);
            ddlItm.DataTextField = "infdesc1";
            ddlItm.DataValueField = "infcod";
            ddlItm.DataSource = (DataTable)Session["tblItmCod"];
            ddlItm.DataBind();
        }


        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string srchTxt = this.txtItemSearch.Text.Trim() + "%";
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "PRJCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblPrjCod"] = ds1.Tables[0];
            Session["tblFlrCod"] = ds1.Tables[1];
            this.ddlProject.DataTextField = "prjdesc1";
            this.ddlProject.DataValueField = "prjcod";
            this.ddlProject.DataSource = (DataTable)Session["tblPrjCod"];
            this.ddlProject.DataBind();
        }


        protected void lbtnSelectItem_Click(object sender, EventArgs e)
        {
            if (this.ddlItem.Items.Count == 0)
                return;
            string ItmCode = this.ddlItem.SelectedValue.ToString();
            string ItmDesc = this.ddlItem.SelectedItem.Text.Trim();
            DataTable tbl1 = (DataTable)Session["tblActAna1"];
            DataRow[] dr1 = tbl1.Select("itmcod='" + ItmCode + "'");
            if (dr1.Length > 0)
                return;

            DataRow[] dr2 = ((DataTable)Session["tblItmCod"]).Select("infcod='" + ItmCode + "'");
            string ItmUnit = dr2[0]["unitfps"].ToString();
            DataRow dr3 = tbl1.NewRow();
            dr3["itmcod"] = ItmCode;
            dr3["itmdesc"] = ItmDesc;
            dr3["itmunit"] = ItmUnit;
            dr3["schqty"] = 0;
            dr3["schrate"] = 0;
            dr3["schamt"] = 0;
            tbl1.Rows.Add(dr3);
            Session["tblActAna1"] = tbl1;
            this.gvAnalysis.EditIndex = -1;
            this.ShowScheduledItemList();
        }

        protected void CallAnalysisData(string prjcod1)
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "ACTANAITEMS", prjcod1, "", "", "", "", "", "", "", "");
            Session["tblActAna1"] = ds1.Tables[0];
            this.cbListFloor.Items.Clear();
            this.chkFlrShowSelected.Checked = (ds1.Tables[1].Rows.Count > 0);
            DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                tbl1.Rows[i]["mark1"] = 0;
                string flrcod = tbl1.Rows[i]["flrcod"].ToString();
                DataRow[] dr1 = ds1.Tables[1].Select("flrcod='" + flrcod + "'");
                if (dr1.Length > 0)
                    tbl1.Rows[i]["mark1"] = 1;
            }
            Session["tblFlrCod"] = tbl1;
        }

        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.chkFlrShowSelected_CheckedChanged(null, null);
                    this.ChkCopyProject.Visible = true;
                    this.ChkCopyTender.Visible = true;
                    break;
                case 1:
                    this.gvAnalysis.PageIndex = 0;
                    this.gvAnalysis.EditIndex = -1;
                    this.ChkCopyProject.Visible = false;
                    this.ChkCopyTender.Visible = false;
                    this.ShowScheduledItemList();

                    break;
                case 2:
                    this.gvAnalysis2.PageIndex = 0;
                    this.gvAnalysis2.EditIndex = -1;
                    this.ChkCopyProject.Visible = false;
                    this.ChkCopyTender.Visible = false;
                    this.ShowFloorScheduledItemList();
                    break;
                case 3:
                    this.gvResInfo.PageIndex = 0;
                    this.gvResInfo.EditIndex = -1;
                    this.ChkCopyProject.Visible = false;
                    this.ChkCopyTender.Visible = false;
                    this.ShowFloorResourceList();
                    break;
                case 4:
                    this.ChkCopyProject.Visible = false;
                    this.ChkCopyTender.Visible = false;
                    this.ShowReportOptions();
                    if (this.Request.QueryString["Type"].ToString().Trim() != "Input")
                    {
                        this.ddlReports_SelectedIndexChanged(null, null);
                        this.ddlReports.Enabled = false;
                    }
                    break;
                case 5:
                    this.ChkCopyProject.Visible = false;
                    this.ChkCopyTender.Visible = false;
                    this.ShowReportOptions();
                    if (this.Request.QueryString["Type"].ToString().Trim() != "Input")
                    {
                        this.ddlReports_SelectedIndexChanged(null, null);
                        this.ddlReports.Enabled = false;
                    }
                    break;


                case 6:
                    this.ChkCopyProject.Visible = false;
                    this.ChkCopyTender.Visible = false;
                    this.gvSpRpt.DataSource = null;
                    this.gvSpRpt.DataBind();
                    this.GetDiffWork();
                    this.ShowSpReport();
                    break;
            }
            this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
        }


        protected void ShowFloorResourceList()
        {
            DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mark1=1";

            DataTable tbl2 = dv1.ToTable();
            DataRow dr2 = tbl2.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = "All Floors";
            tbl2.Rows.Add(dr2);
            DataView dv2 = tbl2.DefaultView;
            dv2.Sort = "flrcod";


            this.lbtnSelectFloorRes.Text = "Change";
            this.lbtnSelectFloorRes_Click(null, null);
        }

        protected void ShowScheduledItemList()
        {
            DataTable tbl1 = (DataTable)Session["tblActAna1"];
            this.gvAnalysis.DataSource = tbl1;
            this.gvAnalysis.DataBind();


            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.gvAnalysis.FooterRow.FindControl("lblgvAmountFooter")).Text =
                    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(schamt)", ""))
                        ? 0.00
                        : tbl1.Compute("sum(schamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((HyperLink)this.gvAnalysis.FooterRow.FindControl("hlbtnDetails")).NavigateUrl = "~/F_07_Ten/LinkTasAccDetAnalysis.aspx?pactcode=" + this.ddlProject.SelectedValue.ToString() + "&pactdesc=" + this.ddlProject.SelectedItem.Text;
            }
        }


        private void GetDiffWork()
        {

            //string comcod = this.GetCompCode();
            //string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            //DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "GETWORKNAME",
            //        PrjCod, "", "", "", "", "", "", "", "");
            //this.ddlRptWork.DataTextField = "sirtdes";
            //this.ddlRptWork.DataValueField = "grp";
            //this.ddlRptWork.DataSource = ds1.Tables[0];
            //this.ddlRptWork.DataBind();
            //ds1.Dispose();

        }

        private void ShowSpReport()
        {

            DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mark1=1";
            DataTable tbl2 = dv1.ToTable();
            DataRow dr2 = tbl2.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = "All Floors-Sum";
            DataRow dr3 = tbl2.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            tbl2.Rows.Add(dr2);
            tbl2.Rows.Add(dr3);
            DataView dv2 = tbl2.DefaultView;
            dv2.Sort = "flrcod";
            this.ddlFlrlstspr.Items.Clear();
            this.ddlFlrlstspr.DataTextField = "flrdes";
            this.ddlFlrlstspr.DataValueField = "flrcod";
            this.ddlFlrlstspr.DataSource = dv2;
            this.ddlFlrlstspr.DataBind();

        }


        protected void FloorDataBind(DataTable tbl1)
        {
            this.cbListFloor.Items.Clear();
            this.cbListFloor.DataTextField = "flrdes";
            this.cbListFloor.DataValueField = "flrcod";
            this.cbListFloor.DataSource = tbl1;
            this.cbListFloor.DataBind();

            for (int i = 0; i < this.cbListFloor.Items.Count; i++)
            {
                string flrcod = this.cbListFloor.Items[i].Value;
                DataRow[] dr1 = tbl1.Select("flrcod='" + flrcod + "'");
                this.cbListFloor.Items[i].Selected = (Convert.ToInt32(dr1[0]["mark1"]) == 1);
            }
        }
        protected void chkFlrShowSelected_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = ((DataTable)Session["tblFlrCod"]).Copy();

            // Session["tblPrjCod"] =
            //ACTANAITEMS

            string pactcode = this.ddlProject.SelectedValue.ToString();
            string cattype = (((DataTable)Session["tblPrjCod"]).Select("prjcod='" + pactcode + "'"))[0]["cattype"].ToString();

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("cattype='" + cattype + "'");
            dt = dv.ToTable();
            if (sender != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["mark1"] = 0;
                    dt.Rows[i]["schqty"] = 0;
                    dt.Rows[i]["schrate"] = 0;
                    dt.Rows[i]["schamt"] = 0;
                }
            }
            for (int i = 0; i < this.cbListFloor.Items.Count; i++)
            {
                if (this.cbListFloor.Items[i].Selected)
                {
                    string flrcod = this.cbListFloor.Items[i].Value;
                    DataRow[] dr1 = dt.Select("flrcod='" + flrcod + "'");
                    dr1[0]["mark1"] = 1;
                }
            }
            Session["tblFlrCod"] = dt;

            DataView dv1 = dt.DefaultView;
            if (this.chkFlrShowSelected.Checked)
                dv1.RowFilter = "mark1=1";
            else
                dv1.RowFilter = "";

            this.FloorDataBind(dv1.ToTable());
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget";
                string eventdesc = "Show Floor";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }



            //DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            //if (sender != null)
            //{
            //    for (int i = 0; i < tbl1.Rows.Count; i++)
            //    {
            //        tbl1.Rows[i]["mark1"] = 0;
            //        tbl1.Rows[i]["schqty"] = 0;
            //        tbl1.Rows[i]["schrate"] = 0;
            //        tbl1.Rows[i]["schamt"] = 0;
            //    }
            //}
            //for (int i = 0; i < this.cbListFloor.Items.Count; i++)
            //{
            //    if (this.cbListFloor.Items[i].Selected)
            //    {
            //        string flrcod = this.cbListFloor.Items[i].Value;
            //        DataRow[] dr1 = tbl1.Select("flrcod='" + flrcod + "'");
            //        dr1[0]["mark1"] = 1;
            //    }
            //}
            //Session["tblFlrCod"] = tbl1;

            //DataView dv1 = tbl1.DefaultView;
            //if (this.chkFlrShowSelected.Checked)
            //    dv1.RowFilter = "mark1=1";
            //else
            //    dv1.RowFilter = "";

            //this.FloorDataBind(dv1.ToTable());
        }
        protected void gvAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (gvAnalysis.EditIndex >= 0)
            {
                GridView gv1 = (GridView)e.Row.FindControl("gvgvFloorAna");
                if (gv1 != null)
                {

                    string comcod = this.GetCompCode();
                    string ItmCode = ((Label)e.Row.FindControl("lblgvItmCod")).Text.Trim();
                    string PrjCode = this.ddlProject.SelectedValue.ToString();

                    DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "ACTANAFLRITMQTY", PrjCode, ItmCode, "", "", "", "", "", "", "");

                    DataTable tbl1 = (DataTable)Session["tblFlrCod"];

                    for (int i = 0; i < tbl1.Rows.Count; i++)
                    {
                        tbl1.Rows[i]["schqty"] = 0;
                        tbl1.Rows[i]["schrate"] = 0;
                        tbl1.Rows[i]["schamt"] = 0;

                        tbl1.Rows[i]["itmslno"] = "";
                        tbl1.Rows[i]["itmschno"] = "";
                        string floorcod = tbl1.Rows[i]["flrcod"].ToString();
                        DataRow[] dr1 = ds1.Tables[0].Select("flrcod='" + floorcod + "'");
                        DataRow[] dr2 = ds1.Tables[1].Select("flrcod='" + floorcod + "'");
                        if (dr1.Length > 0)
                        {
                            tbl1.Rows[i]["schqty"] = dr1[0]["schqty"];
                            tbl1.Rows[i]["schrate"] = dr1[0]["schrate"];
                            tbl1.Rows[i]["schamt"] = dr1[0]["schamt"];
                            tbl1.Rows[i]["itmslno"] = dr1[0]["itmslno"];
                            tbl1.Rows[i]["itmschno"] = dr1[0]["itmschno"];
                        }
                        else if (dr2.Length > 0)

                        {


                            tbl1.Rows[i]["schrate"] = dr2[0]["schrate1"];
                        }
                    }
                    DataView dv1 = tbl1.DefaultView;
                    dv1.RowFilter = "mark1=1";
                    gv1.DataSource = dv1;
                    gv1.DataBind();
                }
            }
        }
        protected void gvAnalysis_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvAnalysis.EditIndex = e.NewEditIndex;
            this.ShowScheduledItemList();
            this.lbtngvgvRefresh_Click(null, null);
        }
        protected void gvAnalysis_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvAnalysis.EditIndex = -1;
            this.ShowScheduledItemList();
        }
        protected void gvAnalysis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAnalysis.PageIndex = e.NewPageIndex;
            this.ShowScheduledItemList();
        }
        protected void gvAnalysis_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string comcod = this.GetCompCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string ItmCode = ((Label)this.gvAnalysis.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();
            int RowIndex = this.gvAnalysis.PageIndex * this.gvAnalysis.PageSize + e.RowIndex;

            GridView gv1 = (GridView)this.gvAnalysis.Rows[e.RowIndex].FindControl("gvgvFloorAna");
            if (gv1 != null)
            {
                for (int i = 0; i < gv1.Rows.Count; i++)
                {
                    string FlrCode = ((Label)gv1.Rows[i].FindControl("lblgvgvFlrCod")).Text.Trim();
                    string Itmqtyf = "0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text.Trim().Replace(",", "");
                    string Qtdratf = "0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvRate")).Text.Trim().Replace(",", "");

                    string SchItmSl = ((TextBox)gv1.Rows[i].FindControl("txtgvgvItmSlNo")).Text.Trim();
                    string SchItmNo = ((TextBox)gv1.Rows[i].FindControl("txtgvgvItmSChNo")).Text.Trim();

                    bool result1 = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "UPDATEPRJFLOORQTY",
                                PrjCod, ItmCode, FlrCode, Itmqtyf, Qtdratf, SchItmSl, SchItmNo, "", "", "", "", "", "", "", "");
                }
            }

            this.gvAnalysis.EditIndex = -1;
            this.ShowScheduledItemList();
        }
        protected void lbtngvgvRefresh_Click(object sender, EventArgs e)
        {
            GridView gv1 = (GridView)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("gvgvFloorAna");
            if (gv1 != null)
            {
                if (gv1.Rows.Count == 0)
                    return;

                double SumQty = 0.00;
                double SumAmt = 0.00;

                for (int i = 0; i < gv1.Rows.Count; i++)
                {
                    double schqty = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text.Trim().Replace(",", ""));
                    double schrate = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvRate")).Text.Trim().Replace(",", ""));
                    double schamt = schqty * schrate;
                    SumQty += schqty;
                    SumAmt += schamt;

                    ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text = schqty.ToString("#,##0.0000;(#,##0.0000); ");
                    ((TextBox)gv1.Rows[i].FindControl("txtgvgvRate")).Text = schrate.ToString("#,##0.0000;(#,##0.0000); ");
                    ((Label)gv1.Rows[i].FindControl("lblgvgvAmt")).Text = schamt.ToString("#,##0.00;(#,##0.00); ");
                }
                ((Label)gv1.FooterRow.FindControl("lblgvgvQtyFooter")).Text = SumQty.ToString("#,##0.0000;(#,##0.0000); ");
                ((Label)gv1.FooterRow.FindControl("lblgvgvRatFooter")).Text = Convert.ToDouble(SumQty == 0 ? 0 : SumAmt / SumQty).ToString("#,##0.0000;(#,##0.0000); ");
                ((Label)gv1.FooterRow.FindControl("lblgvgvAmtFooter")).Text = SumAmt.ToString("#,##0.00;(#,##0.00); ");

                string ItmCode = ((Label)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("lblgvItmCod")).Text.Trim();
                DataTable tbl1 = (DataTable)Session["tblActAna1"];
                DataRow[] dr1 = tbl1.Select("itmcod='" + ItmCode + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["schqty"] = SumQty;
                    dr1[0]["schrate"] = Convert.ToDouble(SumQty == 0 ? 0 : SumAmt / SumQty);
                    dr1[0]["schamt"] = SumAmt;
                }
                Session["tblActAna1"] = tbl1;
            }
        }
        protected void ShowFloorScheduledItemList()
        {
            DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mark1=1";
            this.ddlFloorList.Items.Clear();
            this.ddlFloorList.DataTextField = "flrdes";
            this.ddlFloorList.DataValueField = "flrcod";
            this.ddlFloorList.DataSource = dv1;
            this.ddlFloorList.DataBind();

            this.ddlFloorListToCopy.Items.Clear();
            this.ddlFloorListToCopy.DataTextField = "flrdes";
            this.ddlFloorListToCopy.DataValueField = "flrcod";
            this.ddlFloorListToCopy.DataSource = dv1;
            this.ddlFloorListToCopy.DataBind();

            this.lbtnSelectFloor.Text = "Other Floor";
            this.lbtnSelectFloor_Click(null, null);

        }
        protected void lbtnSelectFloor_Click(object sender, EventArgs e)
        {
            this.ChkCopy.Checked = false;
            this.ddlFloorListToCopy.Visible = false;
            this.lbtnCopyData.Visible = false;

            if (this.lbtnSelectFloor.Text == "Select Floor")
            {
                this.lbtnSelectFloor.Text = "Other Floor";
                this.lblFloorName.Text = this.ddlFloorList.SelectedItem.Text.Trim();

                this.ddlFloorList.Visible = false;
                this.lblFloorName.Visible = true;

                this.ChkCopy.Visible = true;

                this.lblItem2.Visible = true;
                this.txtItemSearch2.Visible = true;
                this.ImgbtnFindItem2.Visible = true;
                this.ddlItem2.Visible = true;
                this.lbtnSelectItem2.Visible = true;
                this.gvAnalysis2.Visible = true;

                string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
                string FlrCod = this.ddlFloorList.SelectedValue.ToString().Trim();
                this.CallAnalysisData2(PrjCod, FlrCod);
                this.gvAnalysis2.PageIndex = 0;
                this.gvAnalysis2.EditIndex = -1;
                this.ShowScheduledItemList2();
            }
            else
            {
                this.lbtnSelectFloor.Text = "Select Floor";

                this.ChkCopy.Visible = false;

                this.ddlFloorList.Visible = true;
                this.lblFloorName.Visible = false;

                this.lblItem2.Visible = false;
                this.txtItemSearch2.Visible = false;
                this.ImgbtnFindItem2.Visible = false;
                this.ddlItem2.Visible = false;
                this.lbtnSelectItem2.Visible = false;
                this.gvAnalysis2.Visible = false;
            }
        }
        protected void CallAnalysisData2(string prjcod1, string floor1)
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "ACTANAITEMS2", prjcod1, floor1, "", "", "", "", "", "", "");
            Session["tblActAna2"] = ds1.Tables[0];
        }
        protected void lbtnSelectItem2_Click(object sender, EventArgs e)
        {
            if (this.ddlItem2.Items.Count == 0)
                return;
            string ItmCode = this.ddlItem2.SelectedValue.ToString();
            string ItmDesc = this.ddlItem2.SelectedItem.Text.Trim();
            DataTable tbl1 = (DataTable)Session["tblActAna2"];
            DataRow[] dr1 = tbl1.Select("itmcod='" + ItmCode + "'");
            if (dr1.Length > 0)
                return;

            DataRow[] dr2 = ((DataTable)Session["tblItmCod"]).Select("infcod='" + ItmCode + "'");
            string ItmUnit = dr2[0]["unitfps"].ToString();
            DataRow dr3 = tbl1.NewRow();
            dr3["itmcod"] = ItmCode;
            dr3["itmdesc"] = ItmDesc;
            dr3["itmunit"] = ItmUnit;
            dr3["itmslno"] = "";
            dr3["itmschno"] = "";
            dr3["schqty"] = 0;
            dr3["schrate"] = 0;
            dr3["schamt"] = 0;
            tbl1.Rows.Add(dr3);
            Session["tblActAna2"] = tbl1;
            this.gvAnalysis2.EditIndex = -1;
            this.ShowScheduledItemList2();
        }
        protected void ShowScheduledItemList2()
        {
            DataTable tbl1 = (DataTable)Session["tblActAna2"];
            this.gvAnalysis2.DataSource = tbl1;
            this.gvAnalysis2.DataBind();

            if (tbl1.Rows.Count > 0)
                ((Label)this.gvAnalysis2.FooterRow.FindControl("lblgvAmountFooter")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(schamt)", "")) ? 0.00 : tbl1.Compute("sum(schamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void gvAnalysis2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.UpdateSessionAnalysis2();
            this.gvAnalysis2.PageIndex = e.NewPageIndex;
            this.ShowScheduledItemList2();
        }

        protected void UpdateSessionAnalysis2()
        {

            DataTable tbl1 = (DataTable)Session["tblActAna2"];
            for (int i = 0; i < this.gvAnalysis2.Rows.Count; i++)
            {
                string SchItmSl = ((TextBox)this.gvAnalysis2.Rows[i].FindControl("txtgvItmSlNo")).Text.Trim();
                string SchItmNo = ((TextBox)this.gvAnalysis2.Rows[i].FindControl("txtgvItmSChNo")).Text.Trim();

                string Itmqtyf = "0" + ((TextBox)this.gvAnalysis2.Rows[i].FindControl("txtgvQty")).Text.Trim().Replace(",", "");
                string Qtdratf = "0" + ((TextBox)this.gvAnalysis2.Rows[i].FindControl("txtgvRate")).Text.Trim().Replace(",", "");

                int RowIndex = this.gvAnalysis2.PageIndex * this.gvAnalysis2.PageSize + i;

                tbl1.Rows[RowIndex]["itmslno"] = SchItmSl;
                tbl1.Rows[RowIndex]["itmschno"] = SchItmNo;
                tbl1.Rows[RowIndex]["schqty"] = Convert.ToDouble(Itmqtyf);
                tbl1.Rows[RowIndex]["schrate"] = Convert.ToDouble(Qtdratf);
                tbl1.Rows[RowIndex]["schamt"] = Convert.ToDouble(Itmqtyf) * Convert.ToDouble(Qtdratf);
            }
            Session["tblActAna2"] = tbl1;
        }
        protected void lbtnFinalUpdate2_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            this.UpdateSessionAnalysis2();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string FlrCode = this.ddlFloorList.SelectedValue.ToString().Trim();
            DataTable tbl1 = (DataTable)Session["tblActAna2"];
            string comcod = this.GetCompCode();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string ItmCode = tbl1.Rows[i]["itmcod"].ToString();
                string SchItmSl = tbl1.Rows[i]["itmslno"].ToString();
                string SchItmNo = tbl1.Rows[i]["itmschno"].ToString();
                string Itmqtyf = "0" + tbl1.Rows[i]["schqty"].ToString();
                string Qtdratf = "0" + tbl1.Rows[i]["schrate"].ToString();

                bool result1 = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "UPDATEPRJFLOORQTY",
                    PrjCod, ItmCode, FlrCode, Itmqtyf, Qtdratf, SchItmSl, SchItmNo, "", "", "", "", "", "", "", "");
            }
        }
        protected void lbtnTotal2_Click(object sender, EventArgs e)
        {
            this.UpdateSessionAnalysis2();
            this.ShowScheduledItemList2();
        }

        protected void CallResourceData()
        {

            string comcod = this.GetCompCode();
            string ProjCod = this.ddlProject.SelectedValue.ToString();
            string FlrCod = "000";
            string SearchItem = this.txtSearchItem.Text.Trim() + "%";

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "ACTRESQTYRATE", ProjCod, FlrCod, SearchItem, "", "", "", "", "", "");
            Session["tblActRes1"] = ds1.Tables[0];
        }

        protected void lbtnResTotal_Click(object sender, EventArgs e)
        {
            this.UpdateSessionResource();
            this.ShowResourceList();
        }

        protected void ShowResourceList()
        {
            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            this.gvResInfo.DataSource = tbl1;
            this.gvResInfo.DataBind();
            if (tbl1.Rows.Count > 0)
                ((Label)this.gvResInfo.FooterRow.FindControl("lblgvTResAmtFooter")).Text =
                    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(tresamt)", "")) ? 0.00 : tbl1.Compute("sum(tresamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void UpdateSessionResource()
        {






            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            string Rescode = "";
            double ResQty = 0;
            double ResRat = 0;
            int RowIndex = 0;
            for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            {

                if (i == 0)
                {
                    Rescode = ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim();
                    ResRat = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResRat")).Text.Trim());
                }

                ResQty = Convert.ToDouble("0" + ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResQty")).Text.Trim());
                if (Rescode == ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim())
                {
                    Rescode = ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim();
                    RowIndex = this.gvResInfo.PageIndex * this.gvResInfo.PageSize + i;
                    tbl1.Rows[RowIndex]["bgdrat"] = ResRat;
                    tbl1.Rows[RowIndex]["tresamt"] = ResQty * ResRat;
                }

                else
                {
                    Rescode = ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim();
                    ResRat = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResRat")).Text.Trim());
                    RowIndex = this.gvResInfo.PageIndex * this.gvResInfo.PageSize + i;
                    tbl1.Rows[RowIndex]["bgdrat"] = ResRat;
                    tbl1.Rows[RowIndex]["tresamt"] = ResQty * ResRat;
                }
            }
            Session["tblActRes1"] = tbl1;



        }

        protected void gvResInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.UpdateSessionResource();
            this.gvResInfo.PageIndex = e.NewPageIndex;
            this.ShowResourceList();
        }
        protected void gvResInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ResCode = ((Label)this.gvResInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            double ResQty = Convert.ToDouble("0" + ((Label)this.gvResInfo.Rows[e.RowIndex].FindControl("lblgvResQty")).Text.Trim());
            double ResRat = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[e.RowIndex].FindControl("txtgvResRat")).Text.Trim());
            int RowIndex = this.gvResInfo.PageIndex * this.gvResInfo.PageSize + e.RowIndex;
            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            if (tbl1.Rows[RowIndex]["ResCod"].ToString() == ResCode)
            {
                tbl1.Rows[RowIndex]["resrat"] = ResRat;
                tbl1.Rows[RowIndex]["tresamt"] = ResQty * ResRat;
            }
            Session["tblActRes1"] = tbl1;
            this.gvResInfo.EditIndex = -1;
            this.ShowResourceList();
        }
        protected void lbtnUpdateResRate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            this.UpdateSessionResource();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();

            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string flrcod = tbl1.Rows[i]["flrcod"].ToString();
                string ResCode = tbl1.Rows[i]["rsircode"].ToString().Trim();
                string ResRat = "0" + tbl1.Rows[i]["bgdrat"].ToString().Trim();
                bool result = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "UPDATEACTRESRATE",
                              PrjCod, flrcod, ResCode, ResRat, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
        }
        protected void lbtnSelectFloorRes_Click(object sender, EventArgs e)
        {
            if (this.lbtnSelectFloorRes.Text == "Show")
            {
                this.lbtnSelectFloorRes.Text = "Change";
                this.gvResInfo.Visible = true;
                this.CallResourceData();
                this.gvResInfo.PageIndex = 0;
                this.gvResInfo.EditIndex = -1;
                this.ShowResourceList();
            }
            else
            {
                this.lbtnSelectFloorRes.Text = "Show";
                this.gvResInfo.Visible = false;
            }
        }

        protected void ShowReportOptions()
        {
            DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mark1=1";

            DataTable tbl2 = dv1.ToTable();
            DataRow dr2 = tbl2.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = "All Catagory-Sum";
            DataRow dr3 = tbl2.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Catagory-Details";
            tbl2.Rows.Add(dr2);
            tbl2.Rows.Add(dr3);

            DataView dv2 = tbl2.DefaultView;
            dv2.Sort = "flrcod";

            this.ddlFloorListRpt.Items.Clear();
            this.ddlFloorListRpt.DataTextField = "flrdes";
            this.ddlFloorListRpt.DataValueField = "flrcod";
            this.ddlFloorListRpt.DataSource = dv2;
            this.ddlFloorListRpt.DataBind();

            this.ddlRptMainGroup.Items.Clear();
            string comcod = this.GetCompCode();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "RPTOPTIONS", "", "", "", "", "", "", "", "", "");
            this.ddlRptMainGroup.DataValueField = "maincod";
            this.ddlRptMainGroup.DataTextField = "maingroup";
            this.ddlRptMainGroup.DataSource = ds1.Tables[0];
            this.ddlRptMainGroup.DataBind();

            this.ddlRptResBreak.DataValueField = "maincod";
            this.ddlRptResBreak.DataTextField = "maingroup";
            this.ddlRptResBreak.DataSource = ds1.Tables[1];
            this.ddlRptResBreak.DataBind();

        }
        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ListVal = this.ddlReports.SelectedIndex;
            this.ChkAdditionalCost.Visible = (ListVal == 0);
            this.ChkOnSchiNo.Visible = (ListVal == 1);
            this.ChkIgnoreSchRate.Visible = (ListVal == 1);
            this.lblRptResBreak.Visible = (ListVal == 1);
            this.ddlRptResBreak.Visible = (ListVal == 1);
            this.ChkMKSUnit.Visible = (ListVal == 1 || ListVal >= 3);
            this.PnlRptResList.Visible = (ListVal == 2);
            this.PnlRptItmList.Visible = (ListVal >= 3);

        }
        protected void lbtnShowReport_Click(object sender, EventArgs e)
        {

            Session.Remove("tblResource");
            string comcod = this.GetCompCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            int mRptID = this.ddlReports.SelectedIndex;
            string mRptName = this.ddlReports.SelectedValue.ToString().Trim();
            string mRptCallType = (mRptID == 0 ? "RPTRESBASIS" : (mRptID == 1 ? "RPTWRKBASIS" :
                                  (mRptID == 2 ? "RPTINDRESBASIS" : (mRptID == 3 ? "RPTINDWRKBASIS" : "RPTACTANASHEET"))));

            string mRptFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptFlrDes = this.ddlFloorListRpt.SelectedItem.Text.Trim();
            string mRptChkPunch = (this.ChkPunchValues.Checked ? "1" : "0");
            string mRptChkIgnoreSchRate = (this.ChkIgnoreSchRate.Checked ? "1" : "0");
            string mRptChkMKSUnit = (this.ChkMKSUnit.Checked ? "1" : "0");
            string mRptChkAdditionalCost = (this.ChkAdditionalCost.Checked ? "1" : "0");
            string mRptChkOnSchiNo = (this.ChkOnSchiNo.Checked ? "1" : "0");

            string mRptChkOptions = mRptChkPunch + mRptChkIgnoreSchRate + mRptChkMKSUnit + mRptChkAdditionalCost + mRptChkOnSchiNo;

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string mRptGroupDes = this.ddlRptGroup.SelectedItem.Text.Trim();

            string mRptResBrkCod = this.ddlRptResBreak.SelectedValue.ToString();
            string mRptResBrkTxt = this.ddlRptResBreak.SelectedItem.Text.Trim();

            string mRptMainGroup = this.ddlRptMainGroup.SelectedValue.ToString();
            string mRptMainGroupTxt = this.ddlRptMainGroup.SelectedItem.Text.Trim();
            string mRescode = this.ddlRptRes.SelectedValue.ToString();
            string mItemCode = this.ddlRptItem.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", mRptCallType,
                    PrjCod, mRptFlrCod, mRptGroup, mRptResBrkCod, mRptMainGroup, mRptChkOptions, mRescode, mItemCode, "");
            Session["tblResource"] = ds1.Tables[0];
            this.Report_Resource_Basis(ds1.Tables[0]);




            //string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            //int mRptID = this.ddlReports.SelectedIndex;
            //string mRptName = this.ddlReports.SelectedValue.ToString().Trim();
            //string mRptCallType = (mRptID == 0 ? "RPTRESBASIS" : (mRptID == 1 ? "RPTWRKBASIS" : 
            //                      (mRptID == 2 ? "RPTINDRESBASIS" : (mRptID == 3 ? "RPTINDWRKBASIS" :
            //                      "RPTACTANASHEET"))));

            //string mRptFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            //string mRptFlrDes = this.ddlFloorListRpt.SelectedItem.Text.Trim();

            //string mRptChkPunch = (this.ChkPunchValues.Checked ? "1" : "0");
            //string mRptChkIgnoreSchRate = (this.ChkIgnoreSchRate.Checked ? "1" : "0");
            //string mRptChkMKSUnit = (this.ChkMKSUnit.Checked ? "1" : "0");
            //string mRptChkAdditionalCost = (this.ChkAdditionalCost.Checked ? "1" : "0");
            //string mRptChkOnSchiNo = (this.ChkOnSchiNo.Checked ? "1" : "0");

            //string mRptChkOptions = mRptChkPunch + mRptChkIgnoreSchRate + mRptChkMKSUnit + mRptChkAdditionalCost + mRptChkOnSchiNo;

            //string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex + 1);
            //string mRptGroupDes = this.ddlRptGroup.SelectedItem.Text.Trim();

            //string mRptResBrkCod = this.ddlRptResBreak.SelectedValue.ToString();
            //string mRptResBrkTxt = this.ddlRptResBreak.SelectedItem.Text.Trim();

            //string mRptMainGroup = this.ddlRptMainGroup.SelectedValue.ToString();
            //string mRptMainGroupTxt = this.ddlRptMainGroup.SelectedItem.Text.Trim();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //DataSet ds1 = new DataSet();
            //DataTable TptTable = new DataTable();
            //if(mRptID == 0 || mRptID == 1)
            //{
            //    ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", mRptCallType, 
            //        PrjCod, mRptFlrCod, mRptGroup, mRptResBrkCod, mRptMainGroup, mRptChkOptions, "", "", "");
            //    TptTable = ds1.Tables[0];
            //    double mSUMAM = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptamt)", "")) ?
            //        0.00 : TptTable.Compute("sum(rptamt)", "")));
            //    this.gvRptResBasis.Columns[6].FooterText = mSUMAM.ToString("#,##0.00;(#,##0.00);-");
            //    this.gvRptResBasis.DataSource = TptTable;
            //    this.gvRptResBasis.DataBind();
            //}
            //if (mRptID == 4)
            //{
            //    string mItemCod = this.ddlRptItem.SelectedValue.ToString();
            //    ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", mRptCallType,
            //        PrjCod, mRptFlrCod, mItemCod, "", "", "", "", "", "");
            //    TptTable = ds1.Tables[0];
            //}
        }

        protected void Report_Resource_Basis(DataTable TptTable)
        {
            // double mPer = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(peramt)", "")) ?
            //  0.00 : TptTable.Compute("sum(peramt)", "")));
            // this.gvRptResBasis.Columns[7].FooterText = mPer.ToString("#,##0.00;(#,##0.00);") + "%";
            // double mSUMAM = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptamt)", "")) ?
            // 0.00 : TptTable.Compute("sum(rptamt)", "")));
            // this.gvRptResBasis.Columns[6].FooterText = mSUMAM.ToString("#,##0;(#,##0);-");
            // double mSUMQTY = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptqty)", "")) ?
            //0.00 : TptTable.Compute("sum(rptqty)", "")));
            // this.gvRptResBasis.Columns[4].FooterText = mSUMQTY.ToString("#,##0.00;(#,##0.00);-");
            this.gvRptResBasis.DataSource = TptTable;
            this.gvRptResBasis.DataBind();




            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(peramt)", "")) ? 0.00 : TptTable.Compute("sum(peramt)", ""))).ToString("#,##0.00;(#,##0.00);") + "%";

            double mSUMAM = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptamt)", "")) ?
            0.00 : TptTable.Compute("sum(rptamt)", "")));
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFTotalCost")).Text = mSUMAM.ToString("#,##0.00;(#,##0.00);");
            //if (this.ddlReports.SelectedIndex == 1)
            //{
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "CONSAREA", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalConArea")).Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["conarea"]).ToString("#,##0.00;(#,##0.00); ") + " " + ds1.Tables[0].Rows[0]["gunit"].ToString();
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalCostPsft")).Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["conarea"]) < 0 ? "" : (mSUMAM / Convert.ToDouble(ds1.Tables[0].Rows[0]["conarea"])).ToString("#,##0.00;(#,##0.00); ");
            }
            else
            {
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalConArea")).Text = "";
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalCostPsft")).Text = "";

            }
            if (this.ddlReports.SelectedIndex == 2)
            {
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lbftTqty")).Text = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptqty)", "")) ? 0.00 : TptTable.Compute("sum(rptqty)", ""))).ToString("#,##0.00;(#,##0.00);");
            }
            ds1.Dispose();
            return;
            //}


            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblConArea")).Visible = false;
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblCostPsft")).Visible = false;
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalConArea")).Text = "";
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalCostPsft")).Text = "";
        }





        protected void ChkCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.ddlFloorListToCopy.Visible = this.ChkCopy.Checked;
            this.lbtnCopyData.Visible = this.ChkCopy.Checked;
        }

        protected void ImgbtnRptFindRes_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string mRptFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string srchTxt = "%" + this.txtRptResSearch.Text.Trim() + "%";

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "RESCODELISTRPT", srchTxt, PrjCod, mRptFlrCod, "", "", "", "", "", "");
            this.ddlRptRes.Items.Clear();
            this.ddlRptRes.DataTextField = "rinfdesc1";
            this.ddlRptRes.DataValueField = "rinfcod";
            this.ddlRptRes.DataSource = ds1.Tables[0];
            this.ddlRptRes.DataBind();
        }


        protected void ImgbtnRptFindItem_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string mRptFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string srchTxt = "%" + this.txtRptItemSearch.Text.Trim() + "%";

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "ITMCODELISTRPT", srchTxt, PrjCod, mRptFlrCod, "", "", "", "", "", "");
            this.ddlRptItem.DataTextField = "infdesc1";
            this.ddlRptItem.DataValueField = "infcod";
            this.ddlRptItem.DataSource = ds1.Tables[0];
            this.ddlRptItem.DataBind();
        }
        protected void Print_Analysis()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string mItmcod = this.ddlRptItem.SelectedValue.ToString();
            string mItmDes = this.ddlRptItem.SelectedItem.Text.Trim().Substring(12);
            string mFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mFlrDes = this.ddlFloorListRpt.SelectedItem.Text.Trim().ToLowerInvariant();
            string mBldCoe = this.ddlProject.SelectedValue.ToString();

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "RPTACTANASHEET",
                          mBldCoe, mFlrCod, mItmcod, "", "", "", "", "", "");

            ReportDocument rptAnaSheet = new RealERPRPT.R_07_Ten.rptTASAnaSheet();
            rptAnaSheet.SetDataSource(ds1.Tables[0]);

            TextObject TxtRptTitle1 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle1"] as TextObject;
            TxtRptTitle1.Text = hst["comnam"].ToString();

            TextObject TxtRptTitle2 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle2"] as TextObject;
            TxtRptTitle2.Text = "Project: " + this.ddlProject.SelectedItem.Text.Trim().Substring(14).Trim();

            TextObject TxtRptTitle3 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle3"] as TextObject;
            TxtRptTitle3.Text = "Sch.Item No: " + ds1.Tables[1].Rows[0]["SchItmNo1"].ToString(); // Sch. Item No

            TextObject TxtRptTitle4 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle4"] as TextObject;
            TxtRptTitle4.Text = ds1.Tables[1].Rows[0]["Itmdesc"].ToString();

            string mUnitFPS = ds1.Tables[1].Rows[0]["UnitFPS"].ToString();
            string mUnitMKS = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            double mStdQtyF = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyF"]);
            double mStdQtyM = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]);

            TextObject TxtRptTitle5 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle5"] as TextObject;
            TxtRptTitle5.Text = "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
                (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.00") + " " + mUnitMKS : "");

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptAnaSheet.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptAnaSheet;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void lbtnShowSpRpt_Click(object sender, EventArgs e)
        {

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvSpRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void lbtnSameValue_Click(object sender, EventArgs e)
        {
            this.UpdateSessionResource();
            this.ShowResourceList();
        }


        protected void lbtnRefresh_OnClick(object sender, EventArgs e)
        {
            CallAnalysisData(this.ddlProject.SelectedValue.ToString());
            this.ShowScheduledItemList();
        }

        protected void ImgbtnFindItem_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string ddlItemID = ((LinkButton)sender).ID.ToString();
            string srchTxt = "%" + (ddlItemID.Contains("ImgbtnFindItem2") ? this.txtItemSearch2.Text.Trim() : this.txtItemSearch.Text.Trim()) + "%";
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "ITMCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblItmCod"] = ds1.Tables[0];
            DropDownList ddlItm = (ddlItemID.Contains("ImgbtnFindItem2") ? this.ddlItem2 : this.ddlItem);
            ddlItm.DataTextField = "infdesc1";
            ddlItm.DataValueField = "infcod";
            ddlItm.DataSource = (DataTable)Session["tblItmCod"];
            ddlItm.DataBind();
        }

        protected void lbtnCopyData_Click(object sender, EventArgs e)
        {
            //this.ChkCopy.Checked = false;
            //this.ddlFloorListToCopy.Visible = false;
            //this.lbtnCopyData.Visible = false;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Copypactcode = this.ddlCopyProjectName.SelectedValue.ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString();

            bool result = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "INSORUPBGTPROCOPY", Copypactcode, pactcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            // Copy and Replace code gose here

        }
        protected void lbtnCopyTender_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Copypactcode = this.ddlCopyTenderName.SelectedValue.ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString();

            bool result = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "INSORUPTENPROCOPY", Copypactcode, pactcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
    }
}
