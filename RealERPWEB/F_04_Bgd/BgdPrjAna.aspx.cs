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
    public partial class BgdPrjAna : System.Web.UI.Page
    {
        ProcessAccess bgdData = new ProcessAccess();

        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string type = this.Request.QueryString["InputType"].ToString();
                //string antype = this.Request.QueryString["AnaType"].ToString();
                if (type == "BgdMainRpt")
                {
                    string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
                    int index1 = (url.Contains("&")) ? url.IndexOf('&') : url.Length;
                    int index2 = (url.Contains("&")) ? url.Substring(index1 + 1).IndexOf('&') : 0;

                    int indexofamp = index1 + (index2 > 0 ? index2 + 1 : index2);

                    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                        Response.Redirect("~/AcceessError.aspx");
                }



                else if (type == "BgdMainRptALL" || type == "BgdMain" || type == "BgdSub")
                {

                    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                        Response.Redirect("~/AcceessError.aspx");

                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                    ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                    this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                }


                else
                {

                    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                        Response.Redirect("~/AcceessError.aspx");

                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                    this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                    ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                }
                this.chkShorting.Visible = false;



                //((Label)this.Master.FindControl("lblTitle")).Text = this.Request.QueryString["AnaType"].ToString() == "2" ? "Individual material details"
                //  :(Request.QueryString["InputType"].ToString() == "BgdMain") ? "CONSTRUCTION BUDGET"
                //  : Request.QueryString["AnaType"].ToString() == "3" ? "Individual work details" : "CONSTRUCTION BUDGET";
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["InputType"].ToString() == "BgdMain") ? "Budget-Engineering"
                //              : (this.Request.QueryString["InputType"].ToString() == "BgdMainRptALL") ? "Budget-Engineering Reports" : "";

                //: ((this.Request.QueryString["InputType"].ToString() == "BgdMain") && (this.Request.QueryString["AnaType"].ToString() == "1")) ? "" : "";

                // rbtnList1.Items.Remove("Special Report");
                this.ImgbtnFindProject_Click(null, null);
                this.ChangeName();
                //Master.Page.Title = "Budget-Engineering";
                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk1_Click(null, null);
                }

                if (this.Request.QueryString["prjcode"].Length > 0 && this.Request.QueryString["sircode"].Length > 0)
                {
                    //this.ImgbtnRptFindRes_Click(null, null);
                    //this.ImgbtnRptFindItem_Click(null, null);
                    this.lbtnShowReport_Click(null, null);
                }



            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintReport_Click);

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
            if (this.lbtnOk1.Text == "New")
            {
                this.lbtnOk1.Text = "Ok";
                this.txtProjectSearch.Enabled = true;
                this.ImgbtnFindProject.Enabled = true;
                this.ddlProject.Visible = true;
                this.lblProjectDesc.Visible = false;
                // this.lblProjectDesc2.Text = "";
                this.rbtnList1.Visible = false;
                this.lblcreationdate.Visible = false;
                this.txtDate.Visible = false;
                this.lbtnUpdatePCDate.Visible = false;

                this.ChkCopyProject.Checked = false;
                this.ChkCopyProject_CheckedChanged(null, null);
                this.ChkCopyProject.Visible = false;

                this.ChkCopyTender.Checked = false;
                this.ChkCopyTender_CheckedChanged(null, null);
                this.ChkCopyTender.Visible = false;

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


            this.lbtnOk1.Text = "New";
            this.txtProjectSearch.Enabled = false;
            this.ImgbtnFindProject.Enabled = false;
            this.ddlProject.Visible = false;
            this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.lblProjectDesc.Width = this.ddlProject.Width;
            this.lblProjectDesc.Visible = true;
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();

            DataRow[] dr1 = ((DataTable)Session["tblPrjCod"]).Select("prjcod='" + PrjCod + "'");
            //this.lblProjectDesc2.Text = dr1[0]["prjdesc2"].ToString();
            this.rbtnList1.Visible = true;
            //this.ChkCopyProject.Visible = true;
            this.lblcreationdate.Visible = true;
            this.txtDate.Visible = true;
            this.lbtnUpdatePCDate.Visible = true;
            this.CallAnalysisData(PrjCod);
            this.ViewSection();
            this.ProjectCDate();

            if (Request.QueryString["InputType"].ToString() == "BgdMainRpt" || Request.QueryString["InputType"].ToString() == "BgdMainRptALL")
            {
                this.rbtnList1.SelectedIndex = 4;
                rbtnList1_SelectedIndexChanged(null, null);
            }
            this.lbtnShowSelectedFloor.Visible = (Request.QueryString["InputType"].ToString() == "BgdSub");
        }


        private void ProjectCDate()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "SHOWPCDATE", pactcode, "", "", "", "", "", "", "", "");
            this.txtDate.Text = (ds1.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["cdate"]).ToString("dd-MMM-yyyy");


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
                this.lblprocopy.Text = "False";

                return;

            }

            this.rbtnList1.Items[0].Enabled = !(Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"].ToString()));
            this.rbtnList1.Items[1].Enabled = !(Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"].ToString()));
            this.rbtnList1.Items[2].Enabled = !(Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"].ToString()));
            this.lblProjectLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();
            this.lblprocopy.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["copy"]).ToString(); ;





        }
        protected void lbtnPrintReport_Click(object sender, EventArgs e)
        {


            if (this.rbtnList1.SelectedIndex == 5)
                this.PrintBgtDifCost();

            else if (this.rbtnList1.SelectedIndex == 6)

                this.RptBugIncmStatementSuma();

            else
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
                    case "6":
                        this.RptBugIncmStatementSuma();
                        break;
                }
                return;
            }
        }


        private void RptBugIncmStatementSuma()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ProjectNam = this.ddlProject.SelectedItem.Text.Trim().ToString();
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblbgd"];
            DataTable dt1 = (DataTable)Session["tblbgd"];


            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("rptcod   like '%00' and rptcod   like '4%' ");
            dt1 = dv.ToTable();
            double totalamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(rptamt)", "")) ?
                                0 : dt1.Compute("sum(rptamt)", "")));


            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BugIncmStatement>();


            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptBugIncmStatement", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("totalamt", totalamt.ToString("#,##0.00;(#,##0.00); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + ProjectNam));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "BUDGETED INCOME STATEMENT -SUMMARY"));

            //Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
        {
            //*** Nayan
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


        }

        private void Print_IndWork()
        {
            //*** Nayan
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
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Individual Work Details"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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

            //this. Report_Resource_Basis(DataTable dt);


            //string comcod = this.GetComeCode();
           

            //ViewState["tblconarea"] = ds1.Tables[0];

            //ViewState["tblconarea"] = ds1.Tables[0]; // nayan
            DataTable dt1 = (DataTable)ViewState["tblconarea"];



            DataView dv = dt.Copy().DefaultView;

            dv.RowFilter = ("rptcod   not like '%00000000' and  rptcod   not like '4111AAAAAAAA%' and rptcod not like  '01AAAAAAAAAA%'  and  rptcod not like  '04AAAAAAAAAA%'   and  rptcod not like  '21AAAAAAAAAA%'");

            DataTable dt2 = dv.ToTable();


            // double totalCost1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0.00 : dt.Compute("sum(rptamt)", "")));

            double totalCost1 = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(rptamt)", "")) ? 0.00 : dt2.Compute("sum(rptamt)", "")));

            // double totalCost = (totalCost1 / 2);

            double totalCost = totalCost1;

          

           

            double conarea =  dt1.Rows.Count == 0 ? 0 : Convert.ToDouble(dt1.Rows[0]["conarea"]);

            double CostPsft = conarea == 0 ? 0 : (totalCost / conarea);

            LocalReport Rpt1 = new LocalReport();

            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BugbasAns>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptResourceBasis", lst, null, null);


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProject.SelectedItem.ToString().Substring(14)));
            Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.ToString()));
            Rpt1.SetParameters(new ReportParameter("CostAre", (this.ddlReports.SelectedIndex == 1) ? "Construction Area" : ""));
            Rpt1.SetParameters(new ReportParameter("TotalCost", totalCost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("ConstArea", conarea.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("CostPerSFT", CostPsft.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("Present", "100.00 %"));


            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Cost - " + ((this.ddlReports.SelectedIndex == 0) ? "Resource Basis" : "Work Basis")));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void GetMetarilsList()
        {
            ViewState.Remove("tblItmCod");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string srchTxt = "%";
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ITMCODELIST", srchTxt, userid, "", "", "", "", "", "", "");

            ViewState["tblItmCod"] = ds1.Tables[0];

            this.ddlgroupwrk.DataTextField = "isirdesc1";
            this.ddlgroupwrk.DataValueField = "isircode";
            this.ddlgroupwrk.DataSource = ds1.Tables[1];
            this.ddlgroupwrk.DataBind();







            this.ImgbtnFindItem_Click(null, null);

        }


        protected void ImgbtnFindItem_Click(object sender, EventArgs e)
        {

            DataTable dt = ((DataTable)ViewState["tblItmCod"]).Copy();
            string comcod = this.GetComeCode();
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(comcod, 1, "XXXXXXXXXXXX", "XXXXXXXXXXXX", "", "XXXXXXXXXXXX", "----Have No Code Permission Please Contact Sys Admin----", "", 0, "", 1);
            }
            string groupwrk = this.ddlgroupwrk.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            if (groupwrk == "000000000000")
            {

                dt = dv.ToTable();

            }
            else
            {
                dv.RowFilter = ("misircode='" + groupwrk + "'");
                dt = dv.ToTable();

            }



            string ddlItemID = "%";

            DropDownList ddlItm = (ddlItemID.Contains("ImgbtnFindItem2") ? this.ddlItem2 : this.ddlItem);
            ddlItm.DataTextField = "isirdesc1";
            ddlItm.DataValueField = "isircode";
            ddlItm.DataSource = dt;
            ddlItm.DataBind();
            //this.ddlItemColor();       
        }
        private void ddlItemColor()
        {

            // DropDownList ddlItm =this.ddlItem;
            foreach (ListItem lteam in this.ddlItem.Items)
            {
                string item = lteam.Value;
                if (item == "XXXXXXXXXXXX")
                    return;

                bool Link = Convert.ToBoolean((((DataTable)ViewState["tblItmCod"]).Select("isircode='" + item + "'"))[0]["link"].ToString());
                if (!Link)
                {
                    lteam.Attributes["style"] = "color:red;";
                }
            }

        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            if (comcod == "3339")
            {
                chkShorting.Visible = true;
            }


            //string srchTxt = (this.Request.QueryString["prjcode"].ToString ().Trim () == "") ? ("%" + this.txtProjectSearch.Text.Trim () + "%") : (this.Request.QueryString["prjcode"].ToString () + "%");

            string srchTxt = "%" + this.txtProjectSearch.Text.Trim() + "%";
            string grp = (ASTUtility.Left(comcod, 1) == "2") ? "L" : "R";

            string type = this.Request.QueryString["InputType"].ToString();
            if (type == "BgdMainRptALL")
            {
                srchTxt = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" + this.txtProjectSearch.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRJCODELIST", srchTxt, grp, userid, "", "", "", "", "", "");
            Session["tblPrjCod"] = ds1.Tables[0];
            Session["tblFlrCod"] = ds1.Tables[1];
            this.ddlProject.DataTextField = "prjdesc1";
            this.ddlProject.DataValueField = "prjcod";
            this.ddlProject.DataSource = (DataTable)Session["tblPrjCod"];
            this.ddlProject.DataBind();
            this.ddlProject.SelectedValue = this.Request.QueryString["prjcode"];
        }
        protected void lbtnSelectItem_Click(object sender, EventArgs e)
        {
            if (this.ddlItem.Items.Count == 0)
                return;
            // string misircode = this.ddlItem.SelectedValue.ToString().Substring(0, 4) + ASTUtility.Replicate("0", 8);
            // string misirdesc = "";
            string ItmCode = this.ddlItem.SelectedValue.ToString();
            string ItmDesc = this.ddlItem.SelectedItem.Text.Trim();
            DataTable tbl1 = (DataTable)Session["tblActAna1"];
            DataRow[] dr1 = tbl1.Select("isircode='" + ItmCode + "'");
            if (dr1.Length > 0)
                return;


            DataTable dt01 = (DataTable)ViewState["tblItmCod"];
            DataRow[] dr2 = dt01.Select("isircode='" + ItmCode + "'");
            string ItmUnit = dr2[0]["isirunit"].ToString();
            DataRow dr3 = tbl1.NewRow();
            dr3["misircode"] = dt01.Select("isircode='" + ItmCode + "'")[0]["misircode7"].ToString();
            dr3["misirdesc"] = dt01.Select("isircode='" + ItmCode + "'")[0]["misirdesc7"].ToString();
            dr3["isircode"] = ItmCode;
            dr3["isirdesc1"] = ItmDesc;
            dr3["isirunit"] = ItmUnit;
            dr3["bgdwqty"] = 0;
            dr3["edited"] = "Eidted";
            dr3["link"] = Convert.ToBoolean(dt01.Select("isircode='" + ItmCode + "'")[0]["link"].ToString());
            tbl1.Rows.Add(dr3);
            Session["tblActAna1"] = this.HiddenSameDataEn(tbl1);
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
        protected void ddlpagesizeen_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowScheduledItemList();
        }
        protected void CallAnalysisData(string prjcod1)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ACTANAITEMS", prjcod1, userid, "", "", "", "", "", "", "");
            Session["tblActAna1"] = this.HiddenSameDataEn(ds1.Tables[0]);
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





        private DataTable HiddenSameDataEn(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string misircode = dt1.Rows[0]["misircode"].ToString();
            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {
                    misircode = dr1["misircode"].ToString();
                    i++;
                    continue;
                }

                if (dr1["misircode"].ToString() == misircode)
                {
                    dr1["misirdesc"] = "";
                }
                misircode = dr1["misircode"].ToString();
            }
            return dt1;
        }

        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Request.QueryString["InputType"].ToString() == "BgdMainRpt")
            //    rbtnList1.SelectedIndex = 4;

            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.crDate.Visible = true;
                    this.ChkCopyProject.Visible = true;
                    //this.ChkCopyTender.Visible = true;
                    this.ChkCopyTenderVisiable();
                    this.chkFlrShowSelected_CheckedChanged(null, null);
                    break;
                case 1:
                    this.crDate.Visible = false;
                    this.ChkCopyProject.Visible = true;
                    //this.ChkCopyTender.Visible = true;
                    this.ChkCopyTenderVisiable();

                    //this.gvAnalysis.PageIndex = 0;
                    //this.gvAnalysis.EditIndex = -1;
                    this.GetMetarilsList();
                    this.ShowScheduledItemList();
                    //ImgbtnFindItem

                    break;
                case 2:
                    this.crDate.Visible = false;
                    this.ChkCopyProject.Visible = true;
                    //this.ChkCopyTender.Visible = true;
                    this.ChkCopyTenderVisiable();

                    //this.gvAnalysis2.PageIndex = 0;
                    //this.gvAnalysis2.EditIndex = -1;
                    this.ShowFloorScheduledItemList();

                    break;
                case 3:
                    this.crDate.Visible = false;
                    this.ChkCopyProject.Visible = true;
                    //this.ChkCopyTender.Visible = true;
                    this.ChkCopyTenderVisiable();

                    break;
                case 4:
                    this.crDate.Visible = false;
                    this.ChkCopyProject.Visible = true;
                    //this.ChkCopyTender.Visible = true;
                    this.ChkCopyTenderVisiable();

                    this.ShowReportOptions();
                    break;
                case 5:
                    this.crDate.Visible = false;
                    this.ChkCopyProject.Visible = true;
                    //this.ChkCopyTender.Visible = true;
                    this.ChkCopyTenderVisiable();

                    //this.lbtnShowReport.Visible = false;
                    this.gvSpRpt.DataSource = null;
                    this.gvSpRpt.DataBind();
                    this.GetDiffWork();
                    this.ShowSpReport();
                    break;


                case 6:
                    this.details.Visible = true;
                    this.crDate.Visible = false;
                    this.ChkCopyProject.Visible = true;
                    //this.ChkCopyTender.Visible = true;
                    this.ChkCopyTenderVisiable();
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

        private void ChkCopyTenderVisiable()
        {
            string comcod = ASTUtility.Left(this.GetComeCode(),2);
            switch (comcod)
            {
                case "11":
                    this.ChkCopyTender.Visible = true;

                    break;
                default:
                    this.ChkCopyTender.Visible = false;
                    break;
            }

        }


        private void showBgdCostResBasis02()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            // string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string val = this.ddldetails.SelectedValue;
            DataSet ds2 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTBUDGETEDCOSTRESBASI02", pactcode, val, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvRptResBasis02.DataSource = null;
                this.gvRptResBasis02.DataBind();
                return;
            }
            Session["tblbgd"] = ds2.Tables[0];
            this.gvRptResBasis02.DataSource = (DataTable)Session["tblbgd"];
            this.gvRptResBasis02.DataBind();
            Session["Report1"] = gvRptResBasis02;
            DataTable dt = ds2.Tables[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rptcod   like '%00' and rptcod   like '4%' ");
            dt = dv.ToTable();
            ((Label)this.gvRptResBasis02.FooterRow.FindControl("lblgvFTotalCost02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ?
                                0 : dt.Compute("sum(rptamt)", ""))).ToString("#,##0;(#,##0); ");


            ((HyperLink)this.gvRptResBasis02.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";








        }

        protected void ShowScheduledItemList()
        {
            DataTable dt = (DataTable)Session["tblActAna1"];

            if (chkShorting.Checked == true)
            {
                dt.DefaultView.Sort = "isirdesc Asc";
                dt = dt.DefaultView.ToTable();
            }
            else
            {
                dt.DefaultView.Sort = "isircode Asc";
                dt = dt.DefaultView.ToTable();
            }
            Session["tblActAna1"] = dt;

            this.gvAnalysis.PageSize = Convert.ToInt32(this.ddlpagesizeen.SelectedValue.ToString());
            this.gvAnalysis.DataSource = dt;
            this.gvAnalysis.DataBind();
            this.gvAnalysis.Columns[8].Visible = (this.lblprocopy.Text == "True");

            //  ((HyperLink)this.gvAnalysis.HeaderRow.FindControl ("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean (dr1[0]["printable"]));
            Session["Report1"] = gvAnalysis;
            this.ddlItemColor();
            if (dt.Rows.Count > 0)
                ((HyperLink)this.gvAnalysis.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

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
            dv.RowFilter = ("cattype='" + cattype + "' or cattype='CCC'"); // for common work
            dv.Sort = ("cattype,flrslno");
            dt = dv.ToTable();            
            //dv.RowFilter = ("cattype='" + cattype + "' or cattype='CCC'"); // for common work
            //dv.Sort = ("cattype,flrcod");
            //dt = dv.ToTable();


            if (sender != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["mark1"] = 0;
                    dt.Rows[i]["bgdwqty"] = 0;
                    dt.Rows[i]["itmrefno"] = "";
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

            DataView dv1 = dt.Copy().DefaultView;
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
                    string PrjCode = this.ddlProject.SelectedValue.ToString();

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
                    gv1.DataSource = dv1;
                    gv1.DataBind();
                }


            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvEdited = (Label)e.Row.FindControl("lblgvEdited");
                Label lblgvItmDesc = (Label)e.Row.FindControl("lblgvItmDesc");

                Label lblgvItmDesc_e = (Label)e.Row.FindControl("lblgvItmDesc_e");
                Label Description;
                Description = lblgvItmDesc ?? lblgvItmDesc_e;
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "edited")).ToString().Trim();
                bool link = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "link"));

                if (code == "Edited")
                {
                    lblgvEdited.Attributes["style"] = "color:green;";

                }
                else
                {

                    lblgvEdited.Attributes["style"] = "color:red;";

                }

                if (!link)
                {
                    Description.Attributes["style"] = "color:red;";

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
            //gvAnalysis.PageIndex = e.NewPageIndex;
            //if (Session["SortedView"] != null)
            //{
            //    gvAnalysis.DataSource = Session["SortedView"];
            //    gvAnalysis.DataBind();
            //}
            //else
            //{
            //    this.ShowScheduledItemList();
            //}



            this.gvAnalysis.EditIndex = -1;
            this.gvAnalysis.PageIndex = e.NewPageIndex;
            this.ShowScheduledItemList();
        }


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("postid", Type.GetType("System.String"));
            tblt01.Columns.Add("posttrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("postsession", Type.GetType("System.String"));
            tblt01.Columns.Add("postdat", Type.GetType("System.String"));

            ViewState["tblapproval"] = tblt01;
        }


        private string GetReqApproval()
        {

            string details = "";

            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataSet ds1 = new DataSet("ds1");

            this.CreateDataTable();
            DataTable dt = (DataTable)ViewState["tblapproval"];
            DataRow dr1 = dt.NewRow();

            dr1["postid"] = usrid;
            dr1["postdat"] = Date;
            dr1["posttrmid"] = trmnid;
            dr1["postsession"] = session;

            dt.Rows.Add(dr1);
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            details = ds1.GetXml();
            return details;

        }


        protected void gvAnalysis_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //postedbyid,posteddat,postrmid,postseson,

            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string ItmCode = ((Label)this.gvAnalysis.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();
            int RowIndex = this.gvAnalysis.PageIndex * this.gvAnalysis.PageSize + e.RowIndex;

            GridView gv1 = (GridView)this.gvAnalysis.Rows[e.RowIndex].FindControl("gvgvFloorAna");
            if (gv1 != null)
            {
                //string appxml = dt.Rows[0]["approval"].ToString();

                string approval = GetReqApproval();
                for (int i = 0; i < gv1.Rows.Count; i++)
                {
                    string FlrCode = ((Label)gv1.Rows[i].FindControl("lblgvgvFlrCod")).Text.Trim();
                    string ItmRef = ((TextBox)gv1.Rows[i].FindControl("txtgvgvItmRef")).Text.Trim();
                    string Itmqtyf = "0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text.Trim().Replace(",", "");

                    bool result1 = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATEPRJFLOORQTY",
                                PrjCod, ItmCode, FlrCode, Itmqtyf, ItmRef, approval, userid, Date, Terminal, Sessionid, "", "", "", "", "");



                    //temporary

                    string conlevel = Convert.ToBoolean("True").ToString();
                    double bgdwqty = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvgvQty")).Text.Trim().Replace(",", ""));
                    if (bgdwqty > 0)
                    {
                        bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATESUPRAT", PrjCod, ItmCode,
                              FlrCode, conlevel, "", "", "", "", "", "", "", "", "", "", "");
                    }

                }
            }

            this.gvAnalysis.EditIndex = -1;
            this.ShowScheduledItemList();
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


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
                    dr1[0]["edited"] = "Edited";
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

            DataRow[] dr2 = ((DataTable)ViewState["tblItmCod"]).Select("isircode='" + ItmCode + "'");
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




            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
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
            string approval = this.GetReqApproval();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string ItmCode = tbl1.Rows[i]["isircode"].ToString();
                string Itmqtyf = "0" + tbl1.Rows[i]["bgdwqty"].ToString();
                string ItmRef = tbl1.Rows[i]["itmrefno"].ToString();


                bool result1 = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATEPRJFLOORQTY",
                    PrjCod, ItmCode, FlrCode, Itmqtyf, ItmRef, approval, "", "", "", "", "", "", "", "", "");
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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string ProjCod = this.ddlProject.SelectedValue.ToString();
            string FlrCod = "000";
            string SearchItem = "%" + this.txtSearchItem.Text.Trim() + "%";
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "ACTRESQTYRATE", ProjCod, FlrCod, SearchItem, userid, "", "", "", "", "");
            Session["tblActRes1"] = HiddenSameDataR(ds1.Tables[0]);

        }

        private DataTable HiddenSameDataR(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string flrcod = "", sircode1 = "";

            //flrcod = dt1.Rows[0]["flrcod"].ToString();
            sircode1 = dt1.Rows[0]["sircode1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["sircode1"].ToString() == sircode1)
                {
                    //flrcod = dt1.Rows[j]["flrcod"].ToString();
                    sircode1 = dt1.Rows[j]["sircode1"].ToString();
                    dt1.Rows[j]["sirdesc1"] = "";
                }

                else
                {
                    //flrcod = dt1.Rows[j]["flrcod"].ToString();
                    sircode1 = dt1.Rows[j]["sircode1"].ToString();
                }

            }
            return dt1;
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
            if (chkShorting.Checked == true)
            {
                tbl1.DefaultView.Sort = "rsirdesc Asc";
                tbl1 = tbl1.DefaultView.ToTable();
            }
            else
            {
                tbl1.DefaultView.Sort = "rsircode Asc";
                tbl1 = tbl1.DefaultView.ToTable();
            }



            this.gvResInfo.DataSource = tbl1;
            this.gvResInfo.DataBind();
            // this.gvAnalysis.Attributes["style"] = "readonly:true;";


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




            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.UpdateSessionResource01();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string postedbyid = hst["usrid"].ToString();
            string postrmid = hst["compname"].ToString();
            string postseson = hst["session"].ToString();

            //postedbyid,posteddat,postrmid,postseson,
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            DataTable tbl1 = (DataTable)Session["tblActRes1"];
            string Permission = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chklkrate")).Checked) ? "1" : "0";

            //bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "DELETERATE",
            //                 PrjCod, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //bool result = false;
            string Projectlock = (((CheckBox)this.gvResInfo.FooterRow.FindControl("chkProjectLock")).Checked) ? "1" : "0";
            DataSet ds1 = new DataSet();
            DataTable dt1 = ((DataTable)Session["tblActRes1"]).Copy();

            // New Table

            //DataTable dtc=new DataTable();
            //dtc.Columns.Add("pactcode", Type.GetType("System.String"));
            //dtc.Columns.Add("flrcod", Type.GetType("System.String"));
            //dtc.Columns.Add("rsircode", Type.GetType("System.String"));
            //dtc.Columns.Add("bgdrat", Type.GetType("System.Double"));
            //dtc.Columns.Add("lock", Type.GetType("System.Boolean"));
            //dtc = dt1.;
            // Column Remove
            dt1.Columns.Remove("flrdes");
            dt1.Columns.Remove("rsirdesc");
            dt1.Columns.Remove("rsirdesc1");
            dt1.Columns.Remove("rsirunit");

            dt1.Columns.Add("postedbyid", typeof(String), postedbyid.ToString());
            dt1.Columns.Add("postseson", typeof(String), postseson.ToString());

            ds1.DataSetName = "ds1";
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";
            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{
            //    string flrcod = tbl1.Rows[i]["flrcod"].ToString();
            //    string ResCode = tbl1.Rows[i]["rsircode"].ToString().Trim();
            //    string ResRat = "0" + tbl1.Rows[i]["bgdrat"].ToString().Trim();
            //result = bgdData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATEACTRESRATE", PrjCod, flrcod, ResCode, ResRat, Permission, "", "", "", "", "", "", "", "", "", "");
            bool result = bgdData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATEACTRESRATE", ds1, null, null, PrjCod, Projectlock, Permission, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Sussesfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

            //}






            //result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "INSERTORUPPLOCK", PrjCod, Projectlock, "", "", "", "", "", "", "", "", "", "", "", "", "");

            //if (!result)
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
            //    return;
            //} 
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


            if (Request.QueryString["InputType"].ToString() == "BgdMainRptALL")
            {

                this.ddlReports_SelectedIndexChanged(null, null);
                this.rbtnList1.Visible = false;
                this.ChkCopyProject.Visible = false;
                this.ChkCopyTender.Visible = false;
            }
            string comcod = this.GetComeCode();
            DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mark1=1";

            DataTable tbl2 = dv1.ToTable();
            DataRow dr2 = tbl2.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = "All Catatory Sum";
            DataRow dr3 = tbl2.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Catatory-Details";
            tbl2.Rows.Add(dr2);
            tbl2.Rows.Add(dr3);
            DataView dv2 = tbl2.DefaultView;
            dv2.Sort = "flrcod";

            //(ASTUtility.Left(this.GetComeCode(), 1) == "2") ? "All Phases-Sum" : "All Floors-Sum";
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
            //this.ChkAdditionalCost.Visible = (ListVal == 0);
            //this.ChkOnSchiNo.Visible = (ListVal == 1);
            //this.ChkIgnoreSchRate.Visible = (ListVal == 1);
            this.lblRptResBreak.Visible = (ListVal == 1);
            this.ddlRptResBreak.Visible = (ListVal == 1);
            //this.ChkMKSUnit.Visible = (ListVal == 1 || ListVal >= 3);
            this.PnlRptResList.Visible = (ListVal == 2);
            this.PnlRptItmList.Visible = (ListVal >= 3);


            this.gvRptResBasis.DataSource = null;
            this.gvRptResBasis.DataBind();
            if (this.ddlReports.SelectedIndex == 2)
            {
                this.ImgbtnRptFindRes_Click(null, null);
            }
            if (this.ddlReports.SelectedIndex == 3)
            {
                this.ImgbtnRptFindItem_Click(null, null);
            }
            if (this.ddlReports.SelectedIndex == 5)
            {
                this.PnlRptItmList.Visible = false;

            }


        }
        protected void lbtnShowReport_Click(object sender, EventArgs e)
        {
            if (this.ddlReports.SelectedIndex == 5)
            {

                this.pnlPrjInfo.Visible = true;
                this.pnlAllRpt.Visible = false;
                this.Show_Prj_Info();
            }
            else
            {
                this.pnlPrjInfo.Visible = false;
                this.pnlAllRpt.Visible = true;

                this.Show_All_Reports();
            }

        }
        private void Show_Prj_Info()
        {
            Session.Remove("tblprjinf");

            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds3 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETPROJINFO", pactcode, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvprjInf.DataSource = null;
                this.gvprjInf.DataBind();
                return;
            }
            //Session["tblprjinf"] =
            this.gvprjInf.DataSource = HiddenSameData(ds3.Tables[0]);
            this.gvprjInf.DataBind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string actcode = "";


            actcode = dt1.Rows[0]["grp"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["grp"].ToString();

                }

            }



            return dt1;


        }
        private void Show_All_Reports()
        {
            Session.Remove("tblResource");



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            int mRptID = this.ddlReports.SelectedIndex;
            string mRptName = this.ddlReports.SelectedValue.ToString().Trim();
            string mRptCallType = (mRptID == 0 ? "RPTRESBASIS" : (mRptID == 1 ? "RPTWRKBASIS" :
                                  (mRptID == 2 ? "RPTINDRESBASIS" : (mRptID == 3 ? "RPTINDWRKBASIS" : "RPTACTANASHEET"))));
            if (mRptID == 0)
            {
                this.gvRptResBasis.Columns[2].Visible = true;
            }
            else
            {
                this.gvRptResBasis.Columns[2].Visible = false;
            }

            if (mRptID == 1)
            {
                this.gvRptResBasis.Columns[9].Visible = true;
                this.gvRptResBasis.Columns[10].Visible = true;

            }


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
                    PrjCod, mRptFlrCod, mRptGroup, mRptResBrkCod, mRptMainGroup, mRptChkOptions, mRescode, mItemCode, userid);
            if (ds1 == null)
            {
                return;
            }

            Session["tblResource"] = HiddenSameDataAR(ds1.Tables[0]);
            this.Report_Resource_Basis(ds1.Tables[0]);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget";
                string eventdesc = "Show Analysis Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private DataTable HiddenSameDataAR(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string flrcod = "", sircode1 = "";

            //flrcod = dt1.Rows[0]["flrcod"].ToString();
            sircode1 = dt1.Rows[0]["sircode1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["sircode1"].ToString() == sircode1)
                {
                    //flrcod = dt1.Rows[j]["flrcod"].ToString();
                    sircode1 = dt1.Rows[j]["sircode1"].ToString();
                    dt1.Rows[j]["sirdesc1"] = "";
                }

                else
                {
                    //flrcod = dt1.Rows[j]["flrcod"].ToString();
                    sircode1 = dt1.Rows[j]["sircode1"].ToString();
                }

            }
            return dt1;
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


            DataTable dt = TptTable.Copy();
            this.gvRptResBasis.DataSource = dt;
            this.gvRptResBasis.DataBind();
            //New Work
            //  DataView dv = dt.DefaultView();

            string grp = this.ddlRptGroup.SelectedIndex.ToString();
            DataView dv = dt.DefaultView;
            switch (grp)
            {
                case "0":
                case "1":
                    dv.RowFilter = ("rptcod   not like '4111AAAAAAAA%' and rptcod not like  '01AAAAAAAAAA%'    and  rptcod not like  '04AAAAAAAAAA%'  and  rptcod not like  '21AAAAAAAAAA%'");
                    break;
                default:
                    dv.RowFilter = ("rptcod   not like '%00000000' and  rptcod   not like '4111AAAAAAAA%' and rptcod not like  '01AAAAAAAAAA%'  and  rptcod not like  '04AAAAAAAAAA%'   and  rptcod not like  '21AAAAAAAAAA%'");
                    break;
            }

            dt = dv.ToTable();

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(peramt)", "")) ? 0.00 : dt.Compute("sum(peramt)", ""))).ToString("#,##0.00;(#,##0.00);") + "%";

            }
            else
            {
                if (dt.Rows.Count == 0)
                    return;
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFPercent")).Text = "0.00";
            }

            double mSUMAM = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0.00 : dt.Compute("sum(rptamt)", "")));
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFTotalCost")).Text = mSUMAM.ToString("#,##0.00;(#,##0.00);");

            //if (this.ddlReports.SelectedIndex == 1)
            //{
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "CONSAREA", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblconarea"] = ds1.Tables[0];

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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string mRptFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string srchTxt = "%" + this.txtRptResSearch.Text.Trim() + "%";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RESCODELISTRPT", srchTxt, PrjCod, mRptFlrCod, userid, "", "", "", "", "");
            this.ddlRptRes.Items.Clear();
            this.ddlRptRes.DataTextField = "rsirdesc1";
            this.ddlRptRes.DataValueField = "rsircode";
            this.ddlRptRes.DataSource = ds1.Tables[0];
            this.ddlRptRes.DataBind();
            if (this.Request.QueryString["sircode"].Length > 0)
            {

                this.ddlRptRes.SelectedValue = this.Request.QueryString["sircode"];

            }
        }
        protected void ImgbtnRptFindItem_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            string mRptFlrCod = this.ddlFloorListRpt.SelectedValue.ToString();
            string srchTxt = "%" + this.txtRptItemSearch.Text.Trim() + "%";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "ITMCODELISTRPT", srchTxt, PrjCod, mRptFlrCod, userid, "", "", "", "", "");
            this.ddlRptItem.DataTextField = "isirdesc1";
            this.ddlRptItem.DataValueField = "isircode";
            this.ddlRptItem.DataSource = ds1.Tables[0];
            this.ddlRptItem.DataBind();

            if (this.Request.QueryString["sircode"].Length > 0)
            {
                this.ddlRptItem.SelectedValue = this.Request.QueryString["sircode"];
            }

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


            //ReportDocument rptStdAnaSheet = new RealERPRPT.R_04_Bgd.rptStdAnaSheet();
            //TextObject TxtRptTitle2 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle2"] as TextObject;
            //TxtRptTitle2.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(15).Trim();
            ////TextObject TxtRptTitle3 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle3"] as TextObject;
            ////TxtRptTitle3.Text = "Item Name:"; // "Sch.Item No: " + ds1.Tables[1].Rows[0]["SchItmNo1"].ToString(); // Sch. Item No
            //TextObject TxtRptTitle4 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle4"] as TextObject;
            //TxtRptTitle4.Text = "Item Name: " + ds1.Tables[1].Rows[0]["Itmdesc"].ToString();
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
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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

            dv.RowFilter = ("rptcod='AAAAAAAAAAAA'");

            // dv.RowFilter = ("rptcod='000000000000'");


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
        protected void ibtnCopyFindTender_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string srchTxt = this.txtSrcCopyPro.Text.Trim() + "%";
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRITASPRJLIST", pactcode, srchTxt, "", "", "", "", "", "", "", "");
            this.ddlCopyTenderName.DataTextField = "prjdesc1";
            this.ddlCopyTenderName.DataValueField = "prjcod";
            this.ddlCopyTenderName.DataSource = ds1.Tables[0];
            this.ddlCopyTenderName.DataBind();
            ds1.Dispose();
        }

        protected void lbtnCopyProject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Copypactcode = this.ddlCopyProjectName.SelectedValue.ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string withoutqty = this.chkwithoutqty.Checked ? "withoutqty" : "";
            string approval = GetReqApproval();
            bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "INSORUPBGTPROCOPY", Copypactcode, pactcode, withoutqty, approval, "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = bgdData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
                //((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        protected void lbtnCopyTender_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Copypactcode = this.ddlCopyTenderName.SelectedValue.ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string withoutqty = this.Chknoqty.Checked ? "withoutqty" : "";
            string approval = GetReqApproval();
            bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "INSORUPTENPROCOPY", Copypactcode, pactcode, withoutqty, approval, "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
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
        protected void lbtnUpdatePCDate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string pcdate = this.txtDate.Text.Trim();
            bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "INSOUPPCDATE", pactcode, pcdate, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void ImgbtngrdFindItem_Click(object sender, EventArgs e)
        {
            string srchname = ((TextBox)this.gvAnalysis.HeaderRow.FindControl("txtIgrdtemSearch")).Text.Trim();
            DataTable dt = (DataTable)Session["tblActAna1"];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["isirdesc1"].ToString().ToUpper().Contains(srchname.ToUpper()))
                    break;

                i++;
            }


            int pagesize = (int)this.gvAnalysis.PageSize;
            int pageindex = (int)(i / pagesize);
            //  int fpageindex = (int)Math.Floor(pageindex);
            this.gvAnalysis.EditIndex = -1;
            this.gvAnalysis.PageIndex = pageindex;
            this.ShowScheduledItemList();




        }
        protected void lbtnShowSelectedFloor_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string concatenate = "";
            string flrcod = "";
            concatenate = "comcod ='" + comcod + "'" + " and ";
            concatenate = concatenate + "pactcode ='" + pactcode + "'";

            for (int i = 0; i < this.cbListFloor.Items.Count; i++)
            {
                if (this.cbListFloor.Items[i].Selected)
                    flrcod = flrcod + "'" + this.cbListFloor.Items[i].Value.ToString() + "', ";


            }

            flrcod = flrcod.Substring(0, flrcod.Length - 2);
            concatenate = concatenate + " and  flrcod  not in (" + flrcod + ")";

            bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "DELETEFLOOR", concatenate, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void gvRptResBasis_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvRptRes1 = (Label)e.Row.FindControl("lblgvRptRes1");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lblgvRptAmt1 = (Label)e.Row.FindControl("lblgvRptAmt1");
                Label lblgvPer = (Label)e.Row.FindControl("lblgvPer");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcod")).ToString();

                if (code == "")
                {
                    return;
                }



                if (code == "01AAAAAAAAAA" || code == "04AAAAAAAAAA")
                {

                    lblgvRptRes1.Font.Bold = true;
                    lblgvRptAmt1.Font.Bold = true;
                    lblgvPer.Font.Bold = true;
                    lblgvRptRes1.Style.Add("text-align", "right");
                    e.Row.Attributes["style"] = "background-color:pink;font-size:16px; font-weight:bold;";
                }





                else if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lblgvRptRes1.Font.Bold = true;
                    lblgvRptAmt1.Font.Bold = true;
                    lblgvPer.Font.Bold = true;
                    lblgvRptRes1.Style.Add("text-align", "right");
                }

                else if (ASTUtility.Right(code, 8) == "00000000")
                {
                    lblgvRptRes1.Attributes["style"] = "font-weight:bold; color:green;";
                    lblgvRptAmt1.Font.Bold = true;
                    lblgvPer.Font.Bold = true;


                }



            }
        }

        protected void dddlgroupwrk_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnFindItem_Click(null, null);
        }
        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblItmCod"];
            int tindex = dt.Rows.Count;
            if (tindex > 15)
            {
                string isircode = this.ddlItem.SelectedValue.ToString();
                int sindex = Convert.ToInt16((dt.Select("isircode='" + isircode + "'"))[0]["rowid"].ToString());

                DataTable dt2 = dt.Clone();
                int rowid = 1;
                for (int i = sindex - 1; i < tindex; i++)
                {
                    DataRow dr1 = dt2.NewRow();
                    dr1["rowid"] = rowid;
                    dr1["isircode"] = dt.Rows[i]["isircode"].ToString();
                    dr1["isirdesc1"] = dt.Rows[i]["isirdesc1"].ToString();
                    dr1["isirunit"] = dt.Rows[i]["isirunit"].ToString();
                    dr1["stdqty"] = dt.Rows[i]["stdqty"].ToString();
                    rowid++;
                    dt2.Rows.Add(dr1);

                }


                for (int i = 0; i < sindex - 1; i++)
                {
                    DataRow dr1 = dt2.NewRow();
                    dr1["rowid"] = rowid;
                    dr1["isircode"] = dt.Rows[i]["isircode"].ToString();
                    dr1["isirdesc1"] = dt.Rows[i]["isirdesc1"].ToString();
                    dr1["isirunit"] = dt.Rows[i]["isirunit"].ToString();
                    dr1["stdqty"] = dt.Rows[i]["stdqty"].ToString();
                    rowid++;
                    dt2.Rows.Add(dr1);

                }
                ViewState["tblItmCod"] = dt2;

                this.ddlBound();



            }



        }

        private void ddlBound()
        {
            this.ddlItem.Items.Clear();
            DataTable dt = (DataTable)ViewState["tblItmCod"];





            this.ddlItem.DataTextField = "isirdesc1";

            this.ddlItem.DataValueField = "isircode";
            this.ddlItem.DataSource = dt;
            this.ddlItem.DataBind();
        }

        protected void gvResInfo_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataTable dt = (DataTable)Session["tblActRes1"];

            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvResInfo.DataSource = sortedView;
            gvResInfo.DataBind();
        }

        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }



        protected void gvRptResBasis_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataTable dt = (DataTable)Session["tblResource"];

            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvRptResBasis.DataSource = sortedView;
            gvRptResBasis.DataBind();
        }
        protected void gvAnalysis_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }

            DataTable dt = (DataTable)Session["tblActAna1"];

            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvAnalysis.DataSource = sortedView;
            gvAnalysis.DataBind();


        }



        protected void chkShorting_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnList1.SelectedIndex == 1)
            {
                ShowScheduledItemList();

            }
            else if (rbtnList1.SelectedIndex == 3)
            {
                ShowResourceList();

            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string UserId = hst["usrid"].ToString();
            //string Terminal = hst["compname"].ToString();
            //string EditDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //int rowindex = (this.dgv2.PageSize) * (this.dgv2.PageIndex) + rownum;
            //string comcod = this.GetCompCode();



            //string vounum1 = GetVou();
            //string cactcode = "000000000000";
            //string spcfcod = "000000000000";
            //string rescode = "000000000000";
            //string vtcode = "00";
            //string trnqty = "0";
            //string voudat = this.txtdate.Text.Substring(0, 11);
            //DataTable tblt05 = (DataTable)Session["AccTbl01"];
            //string actcode = tblt05.Rows[rowindex]["actcode"].ToString();
            //string actlev = tblt05.Rows[rowindex]["actelev"].ToString();
            //if (actlev != "2")
            //{
            //    bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELATEOPENACTRNA", vounum1, actcode, rescode, spcfcod, cactcode, UserId, Terminal, EditDate, "", "", "", "", "", "");

            //    if (result == true)
            //    {

            //        tblt05.Rows[rowindex].Delete();
            //    }
            //}
            //else
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Main Head";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            //}

            //DataView dv = tblt05.DefaultView;
            //Session.Remove("AccTbl01");
            //Session["AccTbl01"] = dv.ToTable();
            //this.dgv2_DataBind();



            // GridView gv1 = (GridView)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("gvgvFloorAna");
            //GridView gv1 = (GridView)this.gvAnalysis.Rows[this.gvAnalysis.EditIndex].FindControl("gvgvFloorAna");

            //GridView gv1 = (GridView)e.Row.FindControl("gvgvFloorAna");

            string comcod = this.GetComeCode();
            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (this.gvAnalysis.PageSize) * (this.gvAnalysis.PageIndex) + rownum;
            DataTable dt = (DataTable)Session["tblActAna1"];
            string ItmCode = dt.Rows[rowindex]["isircode"].ToString();

            bool result1 = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATEZEROQTY",
                        PrjCod, ItmCode, "", "", "", "", "", "", "", "", "", "", "");

            if (result1 == true)
            {

                // dt.Rows[rowindex].Delete();
                dt.Rows[rowindex]["bgdwqty"] = 0.00;

                //DataView dv = dt.DefaultView;
                //  Session.Remove("tblActAna1");
                Session["tblActAna1"] = dt;
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


            gvAnalysis.DataSource = (DataTable)Session["tblActAna1"];
            gvAnalysis.DataBind();




        }

        protected void gvRptResBasis02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvRptRes1 = (Label)e.Row.FindControl("lblgvRptRes02");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lblgvRptAmt1 = (Label)e.Row.FindControl("lblgvRptAmt02");
                Label lblgvPer = (Label)e.Row.FindControl("lblgvPer02");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcod")).ToString();

                if (code == "")
                {
                    return;
                }



                if (code.Substring(0, 2) == "42" && ASTUtility.Right(code, 2) == "00")
                {

                    lblgvRptRes1.Font.Bold = true;
                    lblgvRptAmt1.Font.Bold = true;
                    lblgvPer.Font.Bold = true;

                    e.Row.Attributes["style"] = " background-color:green; color:white;font-size:16px; font-weight:bold;";
                    //lblgvRptAmt1.Attributes["style"] = "background-color:green;font-size:16px; font-weight:bold;";
                    //lblgvPer.Attributes["style"] = "background-color:green;font-size:16px; font-weight:bold;";
                }


                else if (code.Substring(0, 2) == "42" && ASTUtility.Right(code, 2) != "00")
                {

                    lblgvRptRes1.Font.Bold = true;
                    lblgvRptAmt1.Font.Bold = true;
                    lblgvPer.Font.Bold = true;
                    e.Row.Attributes["style"] = "color:maroon;font-size:16px; font-weight:bold;";
                    //lblgvRptAmt1.Attributes["style"] = "background-color:maroon;font-size:16px; font-weight:bold;";
                    //lblgvPer.Attributes["style"] = "background-color:maroon;font-size:16px; font-weight:bold;";

                }
            }
        }

        protected void lbtnshow_OnClick(object sender, EventArgs e)
        {

            this.gvRptResBasis02.DataSource = null;
            this.gvRptResBasis02.DataBind();
            this.showBgdCostResBasis02();

        }



        protected void gvResInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool rlock = Convert.ToBoolean(((DataTable)Session["tblActRes1"]).Rows[0]["lock"]);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (rlock == true)
                {
                    ((TextBox)e.Row.FindControl("txtgvResRat")).ReadOnly = true;

                }
            }

        }

        protected void ddlItem_DataBound(object sender, EventArgs e)
        {

        }

        protected void lbtnAddNotes_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblActAna1"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string rsircode = dt.Rows[rowIndex]["isircode"].ToString();
            this.txtisircode.Text = rsircode;
            string pactcode = this.ddlProject.SelectedValue;
            this.txtpactcode.Text = pactcode;
            this.GetDetailsInfo(pactcode, rsircode);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openNotesModal();", true);
        }

        private void GetDetailsInfo(string pactcode,string rsircode)
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "GETNOTEDETEAILS", pactcode, rsircode, "", "", "", "", "", "", "");
            this.txtNoteDetails.Text = ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["notes"].ToString();
        }

        protected void lbtnUpdateNotes_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblActAna1"];
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();

                string actcode = this.txtpactcode.Text.Trim();
                string isircode = this.txtisircode.Text.Trim();
                string txtNotes = this.txtNoteDetails.Text.Trim();

                bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "INSERTUPDATENOTEDETAILS", actcode, isircode, txtNotes, "", "", "", "");

                if (!result)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = bgdData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void lbtnDelWork_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblActAna1"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = hst["comcod"].ToString();
            string Prjcode = this.ddlProject.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvAnalysis.Rows[rowIndex].FindControl("lblgvItmCod")).Text.Trim();
            bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "DELETEITEME", Prjcode, Itemcode,
                            "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                int rowindex = (this.gvAnalysis.PageSize) * (this.gvAnalysis.PageIndex) + rowIndex;
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
    }

}

