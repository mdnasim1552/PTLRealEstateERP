using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using System.Data;
using RealERPLIB;
using RealERPRPT;
using System.IO;
namespace RealERPWEB.F_04_Bgd
{
    public partial class BgdPrjAna02 : System.Web.UI.Page
    {
        ProcessAccess bgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrintReport.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Construction Budget 02";

                this.ImgbtnFindProject_Click(null, null);
                this.ChangeName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ChangeName()
        {
            string comcod = this.GetComeCode();
            if ((ASTUtility.Left(comcod, 1) == "2"))
            {
                this.rbtnList1.Items[0].Text = "Phase Selection";
                this.rbtnList1.Items[1].Text = "Item Selection(All Phase)";
                this.rbtnList1.Items[2].Text = "Item Selction(Ind.Phase)";
                this.lblFPhaseTitle.Text = "Phase Selecttion";
                this.chkFlrShowSelected.Text = "Show selected Phase only";
                this.lblTitle1.Text = "Item Selection (All Phase)";
                this.lblFloor.Text = "Phase";
            }



        }

        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["InputType"].ToString() == "BgdMain")
        //        Page.MasterPageFile = "~/BudgetMgt.master";
        //    else if (Request.QueryString["InputType"].ToString() == "BgdSub")
        //        Page.MasterPageFile = "~/ApprovalMgt.master";

        //}

        protected void lbtnOk1_Click(object sender, EventArgs e)
        {
            this.rbtnList1.SelectedIndex = -1;
            this.MultiView1.ActiveViewIndex = -1;
            if (this.lbtnOk1.Text == "Other P/U")
            {
                this.lbtnOk1.Text = "Select P/U";
                this.txtProjectSearch.Enabled = true;
                this.ImgbtnFindProject.Enabled = true;
                this.ddlProject.Visible = true;
                this.lblProjectDesc.Visible = false;
                this.lblProjectDesc2.Text = "";
                this.rbtnList1.Visible = false;
                this.ChkCopyProject.Checked = false;
                this.ChkCopyProject_CheckedChanged(null, null);
                this.ChkCopyProject.Visible = false;
                this.gvAnalysis.PageIndex = 0;
                this.gvAnalysis.EditIndex = -1;
                this.gvAnalysis.DataSource = null;
                this.gvAnalysis.DataBind();
                this.gvResInfo.DataSource = null;
                this.gvResInfo.DataBind();
                return;
            }

            if (Request.QueryString["InputType"].ToString() == "BgdSub")
            {
                this.rbtnList1.Items[0].Enabled = false;
                this.rbtnList1.Items[1].Enabled = false;
                this.rbtnList1.Items[2].Enabled = false;
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
            this.lblProjectDesc2.Text = dr1[0]["prjdesc2"].ToString();
            this.rbtnList1.Visible = true;
            this.ChkCopyProject.Visible = true;
            this.CallAnalysisData(PrjCod);
            this.ViewSection();

            if (Request.QueryString["InputType"].ToString() == "BgdMainRpt")
            {
                this.rbtnList1.SelectedIndex = 4;
                rbtnList1_SelectedIndexChanged(null, null);
            }
        }

        private void ViewSection()
        {

            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "PROJECTLOCK", pactcode, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.rbtnList1.Items[0].Enabled = true;
                this.rbtnList1.Items[1].Enabled = true;
                this.rbtnList1.Items[2].Enabled = true;
                this.lblProjectLock.Text = "False";
                return;

            }

            this.rbtnList1.Items[0].Enabled = !(Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"].ToString()));
            this.rbtnList1.Items[1].Enabled = !(Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"].ToString()));
            this.rbtnList1.Items[2].Enabled = !(Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"].ToString()));
            this.lblProjectLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();





        }
        protected void lbtnPrintReport_Click(object sender, EventArgs e)
        {
            if (this.rbtnList1.SelectedIndex != 5)
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
                return;
            }
            this.PrintBgtDifCost();


        }

        private void PrintBgtDifCost()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblResource"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rptcod<>'AAAAAAAAAAAA'");
            dt = dv.ToTable();

            var list = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BgdProjectAnalysis>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptBgdCostDifWork", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Budgetary Plan"));
            Rpt1.SetParameters(new ReportParameter("projectName", "Project Name: " + this.ddlProject.SelectedItem.ToString().Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtFloor", this.ddlFlrlstspr.SelectedItem.ToString()));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlReports.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void Print_InResource()
        {  // ***** Nayan
            string comcod = this.GetComeCode();
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

            DataTable dt1 = (DataTable)Session["tblResource"];
            //string conarea = dt1.Rows[0]["conarea"].ToString();
            //string salarea = dt1.Rows[0]["salarea"].ToString();
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.IndiMateDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptInResource", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProject.SelectedItem.ToString().Substring(14)));
            Rpt1.SetParameters(new ReportParameter("Resource", this.ddlReports.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Individual Material Details"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = hst["comcod"].ToString();
            // string comnam = hst["comnam"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            // string ResCode = this.ddlRptRes.SelectedValue.ToString();
            // DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "INDIVIDUALRES", ResCode, "", "", "", "", "", "", "", "");
            // DataTable dt = (DataTable)Session["tblResource"];
            // ReportDocument rptResource = new  RealERPRPT.R_04_Bgd.rptInResource();
            // TextObject rpttxtHeader = rptResource.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            // rpttxtHeader.Text = "Individual Material Details";
            // TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            // rpttxtProName.Text =this.ddlProject.SelectedItem.ToString().Substring(14);

            // TextObject rpttxtFloor = rptResource.ReportDefinition.ReportObjects["txtfloor"] as TextObject;
            // rpttxtFloor.Text = this.ddlFloorListRpt.SelectedItem.ToString();
            // TextObject rpttxtResourceName = rptResource.ReportDefinition.ReportObjects["rptResource"] as TextObject;
            // string ResourceUnit =ds1.Tables[0].Rows[0]["sirdesc"].ToString()+"  "+ds1.Tables[0].Rows[0]["sirunit"].ToString() ;
            //rpttxtResourceName.Text = ResourceUnit;
            //TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Constraction Budget";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlReports.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            // rptResource.SetDataSource(dt);
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            // rptResource.SetParameterValue("ComLogo", ComLogo);
            // Session["Report1"] = rptResource;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void Print_IndWork()
        {
            // ***** Nayan
            string comcod = this.GetComeCode();
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
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "INDIVIDUALITMANDQTY", pactcode, ItemCode, flrcod, "", "", "", "", "", "");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblResource"];
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.IndiMateDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptInWork", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProject.SelectedItem.ToString().Substring(14)));
            Rpt1.SetParameters(new ReportParameter("Resource", ds1.Tables[0].Rows[0]["sirdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("sunit", "Considered Quantity: " + Convert.ToDouble(ds1.Tables[0].Rows[0]["bgdqty"].ToString()).ToString("#,##0;(#,##0);") + "  " + ds1.Tables[0].Rows[0]["sirunit"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Individual Work Details"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = this.ddlProject.SelectedValue.ToString();
            //string ItemCode = this.ddlRptItem.SelectedValue.ToString();
            //string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            //DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "INDIVIDUALITMANDQTY", pactcode, ItemCode, flrcod, "", "", "", "","","");
            //DataTable dt = (DataTable)Session["tblResource"];
            //ReportDocument rptInwrk = new RealERPRPT.R_04_Bgd.RptInWork();

            //TextObject rpttxtHeader = rptInwrk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //rpttxtHeader.Text = "Individual Work Details";
            //TextObject rpttxtProName = rptInwrk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtProName.Text =this.ddlProject.SelectedItem.ToString().Substring(14);

            //TextObject rpttxtFloor = rptInwrk.ReportDefinition.ReportObjects["txtfloor"] as TextObject;
            //rpttxtFloor.Text = this.ddlFloorListRpt.SelectedItem.ToString();
            //TextObject rpttxtResourceName = rptInwrk.ReportDefinition.ReportObjects["rptResource"] as TextObject;
            //rpttxtResourceName.Text =ds1.Tables[0].Rows[0]["sirdesc"].ToString();
            //TextObject rpttxtconqty = rptInwrk.ReportDefinition.ReportObjects["txtconqty"] as TextObject;
            //rpttxtconqty.Text = "Considered Quantity: "+Convert.ToDouble(ds1.Tables[0].Rows[0]["bgdqty"].ToString()).ToString("#,##0;(#,##0);") + "  " + ds1.Tables[0].Rows[0]["sirunit"].ToString(); ;


            //TextObject txtuserinfo = rptInwrk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Constraction Budget";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlReports.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptInwrk.SetDataSource(dt);
            //rptInwrk.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptInwrk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptInwrk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void Print_Resource()
        {
            // ***** Nayan
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






        }

        protected void ImgbtnFindItem_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string ddlItemID = ((ImageButton)sender).ID.ToString();
            string srchTxt = "%" + (ddlItemID.Contains("ImgbtnFindItem2") ? this.txtItemSearch2.Text.Trim() : this.txtItemSearch.Text.Trim()) + "%";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ITMCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblItmCod"] = ds1.Tables[0];
            DropDownList ddlItm = (ddlItemID.Contains("ImgbtnFindItem2") ? this.ddlItem2 : this.ddlItem);
            ddlItm.DataTextField = "isirdesc1";
            ddlItm.DataValueField = "isircode";
            ddlItm.DataSource = (DataTable)Session["tblItmCod"];
            ddlItm.DataBind();
        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string srchTxt = this.txtProjectSearch.Text.Trim() + "%";
            string grp = (ASTUtility.Left(comcod, 1) == "2") ? "L" : "R";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRJCODELIST", srchTxt, grp, "", "", "", "", "", "", "");
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
            DataRow[] dr1 = tbl1.Select("isircode='" + ItmCode + "'");
            if (dr1.Length > 0)
                return;

            DataRow[] dr2 = ((DataTable)Session["tblItmCod"]).Select("isircode='" + ItmCode + "'");
            string ItmUnit = dr2[0]["isirunit"].ToString();
            string wrkcode = dr2[0]["wrkcode"].ToString();
            DataRow dr3 = tbl1.NewRow();
            dr3["isircode"] = ItmCode;

            dr3["wrkcode"] = wrkcode;
            dr3["isirdesc1"] = ItmDesc;
            dr3["isirunit"] = ItmUnit;
            dr3["bgdwqty"] = 0;
            tbl1.Rows.Add(dr3);
            Session["tblActAna1"] = tbl1;
            this.gvAnalysis.EditIndex = -1;
            this.ShowScheduledItemList();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget";
                string eventdesc = "Floor Selection Item";
                string eventdesc2 = this.ddlItem.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void CallAnalysisData(string prjcod1)
        {

            string comcod = this.GetComeCode();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ACTANAITEMS", prjcod1, "", "", "", "", "", "", "", "");
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
            //if (Request.QueryString["InputType"].ToString() == "BgdMainRpt")
            //    rbtnList1.SelectedIndex = 4;

            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.chkFlrShowSelected_CheckedChanged(null, null);
                    break;
                case 1:
                    //this.gvAnalysis.PageIndex = 0;
                    //this.gvAnalysis.EditIndex = -1;
                    this.ShowScheduledItemList();
                    break;
                case 2:
                    //this.gvAnalysis2.PageIndex = 0;
                    //this.gvAnalysis2.EditIndex = -1;
                    this.ShowFloorScheduledItemList();
                    break;
                case 3:
                    break;
                case 4:
                    this.ShowReportOptions();
                    break;
                case 5:
                    this.gvSpRpt.DataSource = null;
                    this.gvSpRpt.DataBind();
                    this.GetDiffWork();
                    this.ShowSpReport();
                    break;
            }
            this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget";
                string eventdesc = "Select Selection";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }




        protected void ShowScheduledItemList()
        {
            DataTable tbl1 = (DataTable)Session["tblActAna1"];
            this.gvAnalysis.DataSource = tbl1;
            this.gvAnalysis.DataBind();
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
            DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            if (sender != null)
            {
                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    tbl1.Rows[i]["mark1"] = 0;
                    tbl1.Rows[i]["bgdwqty"] = 0;
                    tbl1.Rows[i]["itmrefno"] = "";
                }
            }
            for (int i = 0; i < this.cbListFloor.Items.Count; i++)
            {
                if (this.cbListFloor.Items[i].Selected)
                {
                    string flrcod = this.cbListFloor.Items[i].Value;
                    DataRow[] dr1 = tbl1.Select("flrcod='" + flrcod + "'");
                    dr1[0]["mark1"] = 1;
                }
            }
            Session["tblFlrCod"] = tbl1;

            DataView dv1 = tbl1.DefaultView;
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
        }
        protected void gvAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string comcod = this.GetComeCode();

            if (gvAnalysis.EditIndex >= 0)
            {
                GridView gv1 = (GridView)e.Row.FindControl("gvgvFloorAna");
                if (gv1 != null)
                {
                    string ItmCode = ((Label)e.Row.FindControl("lblgvItmCod")).Text.Trim();
                    string Itemdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isirdesc1")).ToString();
                    string WrkCode = ((Label)e.Row.FindControl("lblgvwrkcode")).Text.Trim();
                    string PrjCode = this.ddlProject.SelectedValue.ToString();
                    ViewState["isirdesc"] = Itemdesc;
                    ViewState["wrkcode"] = WrkCode;
                    DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ACTANAFLRITMQTY", PrjCode, ItmCode, "", "", "", "", "", "", "");

                    DataTable tbl1 = (DataTable)Session["tblFlrCod"];

                    for (int i = 0; i < tbl1.Rows.Count; i++)
                    {
                        tbl1.Rows[i]["bgdwqty"] = 0;

                        string floorcod = tbl1.Rows[i]["flrcod"].ToString();
                        DataRow[] dr1 = ds1.Tables[0].Select("flrcod='" + floorcod + "'");
                        if (dr1.Length > 0)
                        {
                            tbl1.Rows[i]["bgdwqty"] = dr1[0]["bgdwqty"];
                            tbl1.Rows[i]["itmrefno"] = dr1[0]["itmrefno"];
                        }
                    }
                    DataView dv1 = tbl1.DefaultView;
                    dv1.RowFilter = "mark1=1";

                    gv1.Columns[3].Visible = (WrkCode.Length) == 0;
                    gv1.Columns[4].Visible = (WrkCode.Length) > 0;
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

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget(Analysis)";
                string eventdesc = "Edit Floor";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvAnalysis_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvAnalysis.EditIndex = -1;
            this.ShowScheduledItemList();
        }
        protected void gvAnalysis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAnalysis.EditIndex = -1;
            this.gvAnalysis.PageIndex = e.NewPageIndex;
            this.ShowScheduledItemList();
        }
        protected void gvAnalysis_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string ItmCode = ((Label)this.gvAnalysis.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();
            string WrkCode = ((Label)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("lblgvwrkcode")).Text;
            int RowIndex = this.gvAnalysis.PageIndex * this.gvAnalysis.PageSize + e.RowIndex;

            if (WrkCode.Length > 0)
            {
                this.lbtngvgvRefresh02_Click(null, null);
            }
            GridView gv1 = (GridView)this.gvAnalysis.Rows[e.RowIndex].FindControl("gvgvFloorAna");
            if (gv1 != null)
            {
                for (int i = 0; i < gv1.Rows.Count; i++)
                {
                    string FlrCode = ((Label)gv1.Rows[i].FindControl("lblgvgvFlrCod")).Text.Trim();
                    string ItmRef = ((TextBox)gv1.Rows[i].FindControl("txtgvgvItmRef")).Text.Trim();
                    string Itmqtyf = (WrkCode.Length == 0) ? ("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text.Trim().Replace(",", "")) : ("0" + ((HyperLink)gv1.Rows[i].FindControl("hlnkgvgvqty02")).Text.Trim().Replace(",", ""));

                    bool result1 = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATEPRJFLOORQTY",
                                PrjCod, ItmCode, FlrCode, Itmqtyf, ItmRef, "", "", "", "", "", "", "", "", "", "");
                }
            }

            this.gvAnalysis.EditIndex = -1;
            this.ShowScheduledItemList();


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget(Analysis)";
                string eventdesc = "Delete Floor";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtngvgvRefresh_Click(object sender, EventArgs e)
        {
            GridView gv1 = (GridView)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("gvgvFloorAna");

            if (gv1 != null)
            {
                if (gv1.Rows.Count == 0)
                    return;

                double SumQty = 0.00;

                for (int i = 0; i < gv1.Rows.Count; i++)
                {
                    double schqty = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text.Trim().Replace(",", ""));
                    SumQty += schqty;

                    ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text = schqty.ToString("#,##0.0000;(#,##0.0000); ");
                }
                ((Label)gv1.FooterRow.FindControl("lblgvgvQtyFooter")).Text = SumQty.ToString("#,##0.0000;(#,##0.0000); ");

                string ItmCode = ((Label)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("lblgvItmCod")).Text.Trim();
                DataTable tbl1 = (DataTable)Session["tblActAna1"];
                DataRow[] dr1 = tbl1.Select("isircode='" + ItmCode + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["bgdwqty"] = SumQty;
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

            string comcod = this.GetComeCode();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ACTANAITEMS2", prjcod1, floor1, "", "", "", "", "", "", "");
            Session["tblActAna2"] = ds1.Tables[0];
        }
        protected void lbtnSelectItem2_Click(object sender, EventArgs e)
        {
            if (this.ddlItem2.Items.Count == 0)
                return;
            string ItmCode = this.ddlItem2.SelectedValue.ToString();
            string ItmDesc = this.ddlItem2.SelectedItem.Text.Trim();
            DataTable tbl1 = (DataTable)Session["tblActAna2"];
            DataRow[] dr1 = tbl1.Select("isircode='" + ItmCode + "'");
            if (dr1.Length > 0)
                return;

            DataRow[] dr2 = ((DataTable)Session["tblItmCod"]).Select("isircode='" + ItmCode + "'");
            string ItmUnit = dr2[0]["isirunit"].ToString();
            DataRow dr3 = tbl1.NewRow();
            dr3["isircode"] = ItmCode;
            dr3["isirdesc1"] = ItmDesc;
            dr3["isirunit"] = ItmUnit;
            dr3["bgdwqty"] = 0;
            dr3["itmrefno"] = 0;
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

                string Itmqtyf = "0" + ((TextBox)this.gvAnalysis2.Rows[i].FindControl("txtgvQty")).Text.Trim().Replace(",", "");
                string ItmRef = ((TextBox)this.gvAnalysis2.Rows[i].FindControl("txtgvItmRef")).Text.Trim();

                int RowIndex = this.gvAnalysis2.PageIndex * this.gvAnalysis2.PageSize + i;

                tbl1.Rows[RowIndex]["bgdwqty"] = Convert.ToDouble(Itmqtyf);
                tbl1.Rows[RowIndex]["itmrefno"] = ItmRef;
            }
            Session["tblActAna2"] = tbl1;
        }
        protected void lbtnFinalUpdate2_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string comcod = this.GetComeCode();
            this.UpdateSessionAnalysis2();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string FlrCode = this.ddlFloorList.SelectedValue.ToString().Trim();
            DataTable tbl1 = (DataTable)Session["tblActAna2"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string ItmCode = tbl1.Rows[i]["isircode"].ToString();
                string Itmqtyf = "0" + tbl1.Rows[i]["bgdwqty"].ToString();
                string ItmRef = tbl1.Rows[i]["itmrefno"].ToString();


                bool result1 = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATEPRJFLOORQTY",
                    PrjCod, ItmCode, FlrCode, Itmqtyf, ItmRef, "", "", "", "", "", "", "", "", "", "");
            }
            this.ShowScheduledItemList2();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget(Individualy)";
                string eventdesc = "Copy Analysis Update";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void CallResourceRata()
        {

            string comcod = this.GetComeCode();
            string ProjCod = this.ddlProject.SelectedValue.ToString();
            string FlrCod = "000";
            string SearchItem = this.txtSearchItem.Text.Trim() + "%";
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ACTRESQTYRATE", ProjCod, FlrCod, SearchItem, "", "", "", "", "", "");
            Session["tblActRes1"] = ds1.Tables[0];

        }

        protected void lbtnResTotal_Click(object sender, EventArgs e)
        {
            this.UpdateSessionResource01();
            this.ShowResourceList();
        }

        protected void lbtnSameValue_Click(object sender, EventArgs e)
        {
            this.UpdateSessionResource();
            this.ShowResourceList();
        }


        protected void ShowResourceList()
        {
            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            this.gvResInfo.DataSource = tbl1;
            this.gvResInfo.DataBind();
            ((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Checked = (this.lblProjectLock.Text == "True") ? true : false;
            ((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked = Convert.ToBoolean(tbl1.Rows[0]["lock"].ToString());



            if (Request.QueryString["InputType"].ToString() == "BgdMain")
            {
                ((LinkButton)this.gvResInfo.FooterRow.FindControl("lbtnUpdateResRate")).Visible = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((LinkButton)this.gvResInfo.FooterRow.FindControl("lbtnResTotal")).Visible = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Visible = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Enabled = false;
                ((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Visible = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Checked) ? false : true;
                ((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Enabled = false;

            }
            if (Request.QueryString["InputType"].ToString() == "BgdMainRpt")
            {
                ((LinkButton)this.gvResInfo.FooterRow.FindControl("lbtnUpdateResRate")).Visible = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((LinkButton)this.gvResInfo.FooterRow.FindControl("lbtnResTotal")).Visible = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Visible = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked) ? false : true;
                ((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Enabled = false;
                ((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Visible = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Checked) ? false : true;
                ((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Enabled = false;

            }




            if (tbl1.Rows.Count > 0)
                ((Label)this.gvResInfo.FooterRow.FindControl("lblgvTResAmtFooter")).Text =
                    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(tresamt)", "")) ? 0.00 : tbl1.Compute("sum(tresamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            // this.lbtnUpdateResRate_Click(null, null);
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


            //DataTable tbl1 = (DataTable)Session["tblActRes1"];
            //for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            //{

            // //   string Rescode = ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim();
            //    double ResQty = Convert.ToDouble("0" + ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResQty")).Text.Trim());
            //    double ResRat = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResRat")).Text.Trim());
            //    //if (i > 0)
            //    //{
            //    //    string Rescodei = ((Label)this.gvResInfo.Rows[i - 1].FindControl("lblgvResCod")).Text.Trim();
            //    //    ResRat = (Rescode == Rescodei) ? Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i - 1].FindControl("txtgvResRat")).Text.Trim()) : ResRat;

            //    //}
            //    // ResRat=(if>0)?Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i-1].FindControl("txtgvResRat")).Text.Trim())
            //    int RowIndex = this.gvResInfo.PageIndex * this.gvResInfo.PageSize + i;
            //    tbl1.Rows[RowIndex]["bgdrat"] = ResRat;
            //    tbl1.Rows[RowIndex]["tresamt"] = ResQty * ResRat;
            //}
            //Session["tblActRes1"] = tbl1;

        }

        private void UpdateSessionResource01()
        {

            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            {
                double ResQty = Convert.ToDouble("0" + ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResQty")).Text.Trim());
                double ResRat = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResRat")).Text.Trim());
                int RowIndex = this.gvResInfo.PageIndex * this.gvResInfo.PageSize + i;
                tbl1.Rows[RowIndex]["bgdrat"] = ResRat;
                tbl1.Rows[RowIndex]["tresamt"] = ResQty * ResRat;
            }
            Session["tblActRes1"] = tbl1;


        }


        protected void gvResInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.UpdateSessionResource01();
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
            if (tbl1.Rows[RowIndex]["rsircode"].ToString() == ResCode)
            {
                tbl1.Rows[RowIndex]["bgdrat"] = ResRat;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.UpdateSessionResource01();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            string Permission = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked) ? "1" : "0";

            //bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "DELETERATE",
            //                 PrjCod, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            bool result = false;

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string flrcod = tbl1.Rows[i]["flrcod"].ToString();
                string ResCode = tbl1.Rows[i]["rsircode"].ToString().Trim();
                string ResRat = "0" + tbl1.Rows[i]["bgdrat"].ToString().Trim();
                result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATEACTRESRATE", PrjCod, flrcod, ResCode, ResRat, Permission, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }

            string Projectlock = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Checked) ? "1" : "0";
            result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "INSERTORUPPLOCK", PrjCod, Projectlock, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget";
                string eventdesc = "Resource rate Input & Report";
                string eventdesc2 = "Update rate";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnSelectFloorRes_Click(object sender, EventArgs e)
        {

            this.CallResourceRata();
            this.ShowResourceList();
        }

        protected void ShowReportOptions()
        {


            if (Request.QueryString["InputType"].ToString() == "BgdMainRpt")
            {
                this.ddlReports.SelectedIndex = Int32.Parse(Request.QueryString["AnaType"]);
                this.ddlReports_SelectedIndexChanged(null, null);
                this.ddlReports.Enabled = false;
                this.rbtnList1.Visible = false;
            }

            string comcod = this.GetComeCode();
            DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mark1=1";

            DataTable tbl2 = dv1.ToTable();
            DataRow dr2 = tbl2.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = (ASTUtility.Left(this.GetComeCode(), 1) == "2") ? "All Phases-Sum" : "All Floors-Sum";
            DataRow dr3 = tbl2.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = (ASTUtility.Left(this.GetComeCode(), 1) == "2") ? "All Phases-Details" : "All Floors-Details";
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

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTOPTIONS", "", "", "", "", "", "", "", "", "");
            this.ddlRptMainGroup.DataValueField = "maincod";
            this.ddlRptMainGroup.DataTextField = "maingroup";
            this.ddlRptMainGroup.DataSource = ds1.Tables[0];
            this.ddlRptMainGroup.DataBind();

            this.ddlRptResBreak.DataValueField = "maincod";
            this.ddlRptResBreak.DataTextField = "maingroup";
            this.ddlRptResBreak.DataSource = ds1.Tables[1];
            this.ddlRptResBreak.DataBind();
            this.gvRptResBasis.DataSource = null;
            this.gvRptResBasis.DataBind();



        }
        private void GetDiffWork()
        {


            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSISDIFWRK", "GETWORKNAME",
                    PrjCod, "", "", "", "", "", "", "", "");
            this.ddlRptWork.DataTextField = "sirtdes";
            this.ddlRptWork.DataValueField = "grp";
            this.ddlRptWork.DataSource = ds1.Tables[0];
            this.ddlRptWork.DataBind();
            ds1.Dispose();

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
            this.gvRptResBasis.DataSource = null;
            this.gvRptResBasis.DataBind();
        }
        protected void lbtnShowReport_Click(object sender, EventArgs e)
        {
            Session.Remove("tblResource");

            string comcod = this.GetComeCode();
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
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", mRptCallType,
                    PrjCod, mRptFlrCod, mRptGroup, mRptResBrkCod, mRptMainGroup, mRptChkOptions, mRescode, mItemCode, "");
            Session["tblResource"] = ds1.Tables[0];
            this.Report_Resource_Basis(ds1.Tables[0]);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget";
                string eventdesc = "Show Analysis Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void lbtnCopyData_Click(object sender, EventArgs e)
        {
            this.ChkCopy.Checked = false;
            this.ddlFloorListToCopy.Visible = false;
            this.lbtnCopyData.Visible = false;
            // Copy and Replace code gose here

        }
        protected void ChkCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.ddlFloorListToCopy.Visible = this.ChkCopy.Checked;
            this.lbtnCopyData.Visible = this.ChkCopy.Checked;
        }
        protected void Report_Resource_Basis(DataTable TptTable)
        {

            this.gvRptResBasis.DataSource = TptTable;
            this.gvRptResBasis.DataBind();
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(peramt)", "")) ? 0.00 : TptTable.Compute("sum(peramt)", ""))).ToString("#,##0.00;(#,##0.00);") + "%";

            double mSUMAM = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptamt)", "")) ?
            0.00 : TptTable.Compute("sum(rptamt)", "")));
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFTotalCost")).Text = mSUMAM.ToString("#,##0.00;(#,##0.00);");
            //if (this.ddlReports.SelectedIndex == 1)
            //{
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "CONSAREA", pactcode, "", "", "", "", "", "", "", "");
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
        protected void ImgbtnRptFindRes_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string mRptFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string srchTxt = "%" + this.txtRptResSearch.Text.Trim() + "%";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RESCODELISTRPT", srchTxt, PrjCod, mRptFlrCod, "", "", "", "", "", "");
            this.ddlRptRes.Items.Clear();
            this.ddlRptRes.DataTextField = "rsirdesc1";
            this.ddlRptRes.DataValueField = "rsircode";
            this.ddlRptRes.DataSource = ds1.Tables[0];
            this.ddlRptRes.DataBind();
        }
        protected void ImgbtnRptFindItem_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string mRptFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string srchTxt = "%" + this.txtRptItemSearch.Text.Trim() + "%";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "ITMCODELISTRPT", srchTxt, PrjCod, mRptFlrCod, "", "", "", "", "", "");
            this.ddlRptItem.DataTextField = "isirdesc1";
            this.ddlRptItem.DataValueField = "isircode";
            this.ddlRptItem.DataSource = ds1.Tables[0];
            this.ddlRptItem.DataBind();
        }

        protected void Print_Analysis()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mItmcod = this.ddlRptItem.SelectedValue.ToString();
            string mItmDes = this.ddlRptItem.SelectedItem.Text.Trim().Substring(12);
            string mFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mFlrDes = this.ddlFloorListRpt.SelectedItem.Text.Trim().ToLowerInvariant();
            string mBldCoe = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTACTANASHEET",
                          mBldCoe, mFlrCod, mItmcod, "", "", "", "", "", "");

            LocalReport Rpt1 = new LocalReport();
            //  DataTable dt1 = (DataTable)Session["tblbgd"];
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BugdAna>();
            string mUnitFPS = ds1.Tables[1].Rows[0]["UnitFPS"].ToString();
            string mUnitMKS = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            double mStdQtyF = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyF"]);
            double mStdQtyM = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]);
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptStdAnaSheet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Item Name: " + ds1.Tables[1].Rows[0]["Itmdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Quantity", "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
                (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.00") + " " + mUnitMKS : "")));
            //Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Standard Analysis"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptStdAnaSheet = new RealERPRPT.R_04_Bgd.rptStdAnaSheet() ;
            //TextObject TxtRptTitle2 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle2"] as TextObject;
            //TxtRptTitle2.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(15).Trim();

            ////TextObject TxtRptTitle3 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle3"] as TextObject;
            ////TxtRptTitle3.Text = "Item Name:"; // "Sch.Item No: " + ds1.Tables[1].Rows[0]["SchItmNo1"].ToString(); // Sch. Item No

            //TextObject TxtRptTitle4 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle4"] as TextObject;
            //TxtRptTitle4.Text ="Item Name: "+ ds1.Tables[1].Rows[0]["Itmdesc"].ToString();

            //string mUnitFPS = ds1.Tables[1].Rows[0]["UnitFPS"].ToString();
            //string mUnitMKS = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            //double mStdQtyF = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyF"]);
            //double mStdQtyM = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]);

            //TextObject TxtRptTitle5 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle5"] as TextObject;
            //TxtRptTitle5.Text = "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
            //    (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.000") + " " + mUnitMKS : "");
            //TextObject txtuserinfo = rptStdAnaSheet.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptStdAnaSheet.SetDataSource(ds1.Tables[0]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptStdAnaSheet.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptStdAnaSheet;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void chklkrate_CheckedChanged(object sender, EventArgs e)
        {
            //if (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked)
            //    this.chktest.Text = "Check";
            //else
            //    this.chktest.Text = "Uncheck";
        }

        protected void gvAnalysis_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblActAna1"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Prjcode = this.ddlProject.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvAnalysis.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();
            bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "DELETEITEME", Prjcode, Itemcode,
                            "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (result == true)
            {
                int rowindex = (this.gvAnalysis.PageSize) * (this.gvAnalysis.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            this.gvAnalysis.DataSource = dv.ToTable();
            this.gvAnalysis.DataBind();
            Session.Remove("tblActAna1");
            Session["tblActAna1"] = dv.ToTable();
            this.ShowScheduledItemList();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget(Indevidual Floor)";
                string eventdesc = "Floor Delete";
                string eventdesc2 = Itemcode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void lbtnShowSpRpt_Click(object sender, EventArgs e)
        {
            Session.Remove("tblResource");
            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string mRptFlrCod = this.ddlFlrlstspr.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroupspr.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            // string wokkItem = (this.ddlRptWork.SelectedItem.Text.Trim() =="All Work") ? "all" : this.ddlRptWork.SelectedItem.Text.Trim();
            string wokkItem = this.ddlRptWork.SelectedValue.ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSISDIFWRK", "RPTWRKBASIS",
                    PrjCod, mRptFlrCod, mRptGroup, wokkItem, "", "", "", "", "");
            Session["tblResource"] = ds1.Tables[0];
            this.LoadGrid();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget";
                string eventdesc = "Show Special Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblResource"];
            DataTable dt1 = dt.Copy();
            this.gvSpRpt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSpRpt.DataSource = dt1;
            this.gvSpRpt.DataBind();
            this.FooterCalCulation(dt1);

        }

        private void FooterCalCulation(DataTable dt)
        {
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = ("rptcod='000000000000'");
            dt = dv.ToTable();
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvSpRpt.FooterRow.FindControl("lgvFPer")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(peramt)", "")) ?
                0.00 : dt.Compute("sum(peramt)", ""))).ToString("#,##0.00;(#,##0.00);") + "%";
            ((Label)this.gvSpRpt.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ?
            0.00 : dt.Compute("sum(rptamt)", ""))).ToString("#,##0.00;(#,##0.00);-");


        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvSpRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSpRpt.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }




        protected void ibtnCopyFindProject_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string srchTxt = this.txtSrcCopyPro.Text.Trim() + "%";
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "GETPROJECTNAME", pactcode, srchTxt, "", "", "", "", "", "", "");
            this.ddlCopyProjectName.DataTextField = "prjdesc1";
            this.ddlCopyProjectName.DataValueField = "prjcod";
            this.ddlCopyProjectName.DataSource = ds1.Tables[0];
            this.ddlCopyProjectName.DataBind();
            ds1.Dispose();
        }
        protected void lbtnCopyProject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Copypactcode = this.ddlCopyProjectName.SelectedValue.ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "INSORUPBGTPROCOPY", Copypactcode, pactcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void ChkCopyProject_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCopyProject.Visible = this.ChkCopyProject.Checked;
        }

        protected void gvgvFloorAna_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string pactcode = this.ddlProject.SelectedValue.ToString();
                string pactdesc = this.ddlProject.SelectedItem.Text;
                string itemcode = (string)ViewState["isirdesc"].ToString().Substring(0, 12);
                string itemdesc = (string)ViewState["isirdesc"].ToString().Substring(14);
                HyperLink hlnkgvgvqty02 = (HyperLink)e.Row.FindControl("hlnkgvgvqty02");
                string flrcod = ((Label)e.Row.FindControl("lblgvgvFlrCod")).Text;
                string flrdes = ((Label)e.Row.FindControl("lblgvgvFlrDesc")).Text;
                hlnkgvgvqty02.NavigateUrl = "~/F_04_Bgd/LinkBgdEstStdAna.aspx?pactcode=" + pactcode + "&pactdesc=" + pactdesc + "&isircode=" + itemcode + "&isirdesc=" + itemdesc + "&flrcod=" + flrcod + "&flrdes=" + flrdes;
            }



            //}
        }
        protected void lbtngvgvRefresh02_Click(object sender, EventArgs e)
        {
            //ViewState["wrkcode"] = WrkCode;
            GridView gv1 = (GridView)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("gvgvFloorAna");
            string WrkCode = ((Label)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("lblgvwrkcode")).Text;


            if (gv1 != null)
            {

                string comcod = this.GetComeCode();
                string PrjCode = this.ddlProject.SelectedValue.ToString();
                string itemcode = (string)ViewState["isirdesc"].ToString().Substring(0, 12);

                DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ACTEANAFLRITMQTY", PrjCode, itemcode, "", "", "", "", "", "", "");

                DataTable tbl1 = (DataTable)Session["tblFlrCod"];

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    tbl1.Rows[i]["bgdwqty"] = 0;

                    string floorcod = tbl1.Rows[i]["flrcod"].ToString();
                    DataRow[] dr1 = ds1.Tables[0].Select("flrcod='" + floorcod + "'");
                    if (dr1.Length > 0)
                    {
                        tbl1.Rows[i]["bgdwqty"] = dr1[0]["bgdwqty"];
                        tbl1.Rows[i]["itmrefno"] = dr1[0]["itmrefno"];
                    }
                }
                DataView dv1 = tbl1.DefaultView;
                dv1.RowFilter = "mark1=1";

                gv1.Columns[3].Visible = (WrkCode.Length) == 0;
                gv1.Columns[4].Visible = (WrkCode.Length) > 0;
                gv1.DataSource = dv1;
                gv1.DataBind();


                double SumQty = 0.00;

                for (int i = 0; i < gv1.Rows.Count; i++)
                {
                    double schqty = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text.Trim().Replace(",", ""));
                    SumQty += schqty;

                    ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text = schqty.ToString("#,##0.0000;(#,##0.0000); ");
                }
                  ((Label)gv1.FooterRow.FindControl("lblgvgvQtyFooter")).Text = SumQty.ToString("#,##0.0000;(#,##0.0000); ");

                string ItmCode = ((Label)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("lblgvItmCod")).Text.Trim();
                DataTable tbl02 = (DataTable)Session["tblActAna1"];
                DataRow[] dr02 = tbl02.Select("isircode='" + ItmCode + "'");
                if (dr02.Length > 0)
                {
                    dr02[0]["bgdwqty"] = SumQty;
                }
                Session["tblActAna1"] = tbl02;


            }


        }
    }
}
