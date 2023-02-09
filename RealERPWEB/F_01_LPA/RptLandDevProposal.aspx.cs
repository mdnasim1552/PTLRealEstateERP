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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_01_LPA
{

    public partial class RptLandDevProposal : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Land Development Proposal";
                //  this.ViewSection();
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
            return (hst["comcod"].ToString());

        }
        private void ProjectName()
        {
            string comcod = this.GetComCode();
            string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            Session["tblpro"] = ds1.Tables[0];

        }

        private void ViewSection()
        {
            string qtype = this.Request.QueryString["Type"];
            switch (qtype)
            {
                case "PrjInfo":
                    this.GetProjectInfo();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "Cost":
                    this.ShowCost();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "Revenue":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowRevenue();
                    break;
                case "LandOwnerBenifit":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowRevenue();
                    break;

                case "PrjReport":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.ShowRevenue();
                    break;

            }



        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.ProjectName();
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            string qtype = this.Request.QueryString["Type"];
            switch (qtype)
            {
                case "PrjInfo":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetProjectInfo();
                    break;
                case "Revenue":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowRevenue();
                    break;
                case "Cost":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowCost();
                    break;
                case "LandOwnerBenifit":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowRevenue();
                    break;
                case "PrjReport":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.ShowReport();
                    break;

            }




            //this.lblMsg.Text = "";
            //int rindex = this.rbtnList1.SelectedIndex;
            //switch(rindex)
            //{
            //    case 0:              
            //        this.GetProjectInfo();
            //        this.chkAllRes.Visible = false;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;             
            //    case 1:
            //        this.ShowRevenue();
            //        this.chkAllRes.Visible = true;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;
            //    case 2:
            //        this.ShowCost();
            //        this.chkAllRes.Visible = true;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;
            //    case 3:
            //        this.ShowLandBenifit();
            //        this.chkAllRes.Visible = true;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;
            //    case 4:
            //        this.ShowReport();
            //        this.chkAllRes.Visible = false;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;
            //}



        }








        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string qtype = this.Request.QueryString["Type"];
            switch (qtype)
            {
                case "PrjInfo":
                    this.RptProjectInfo();
                    break;
                case "Revenue":
                    this.RptRevenue();
                    break;
                case "Cost":
                    this.RptCost();
                    break;
                case "LandOwnerBenifit":
                    break;
                case "PrjReport":
                    this.RptPrjReport();
                    break;

            }

        }

        private void RptProjectInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            DataTable dt = (DataTable)ViewState["tblfeaprj"];

            var lst = dt.DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.LandFesibility>();


            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_01_LPA.RptLandFeasibility", lst, null, null);

            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptname", "Project Information"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("prjname", prjname));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptRevenue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '01%'";
            string CostOrSale = "82%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_LP_PROFEASIBILITY", "RPT_GETLANDSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");


            ReportDocument rpcp = new RealERPRPT.R_01_LPA.RptFeaLandRevnue();//RptFeaProject();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;


            //TextObject txtPrjInfo = rpcp.ReportDefinition.ReportObjects["txtPrjInfo"] as TextObject;
            //txtPrjInfo.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjinfo"].ToString() : "";
            //TextObject txtPrjAdd = rpcp.ReportDefinition.ReportObjects["txtPrjAdd"] as TextObject;
            //txtPrjAdd.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjadd"].ToString() : "";
            //TextObject txtPtype = rpcp.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
            //txtPtype.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjtyp"].ToString() : "";
            if (ASTUtility.Left(comcod, 1) != "2")
            {
                TextObject txtLand = rpcp.ReportDefinition.ReportObjects["txtLand"] as TextObject;
                txtLand.Text = "Land Owner Share:  " + Convert.ToDouble(ds2.Tables[1].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + " %";
                TextObject txtComp = rpcp.ReportDefinition.ReportObjects["txtComp"] as TextObject;
                txtComp.Text = "Company Share:  " + Convert.ToDouble(ds2.Tables[1].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + " %";

                TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtDate.Text = "Date :" + System.DateTime.Now.ToString("dd-MMM-yyyy");
            }





            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rpcp.SetDataSource(ds2.Tables[0]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptCost()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "(infcod like '0[2-9]%'  or infcod like '1[0-9]%'  or infcod like '2[0-9]%' or infcod like '3[0-9]%'  or infcod like '4[0-9]%')";
            string CostOrSale = "81%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_LP_PROFEASIBILITY", "RPT_GETLANDSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");

            ReportDocument rpcp = new RealERPRPT.R_01_LPA.RptFeaLandCost();//RptFeaProject();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;

            //TextObject txtPrjInfo = rpcp.ReportDefinition.ReportObjects["txtPrjInfo"] as TextObject;
            //txtPrjInfo.Text = dsrpt.Tables[1].Rows.Count < 0 ? dsrpt.Tables[1].Rows[0]["prjinfo"].ToString() : "";
            //TextObject txtPrjAdd = rpcp.ReportDefinition.ReportObjects["txtPrjAdd"] as TextObject;
            //txtPrjAdd.Text = dsrpt.Tables[1].Rows.Count < 0 ? dsrpt.Tables[1].Rows[0]["prjadd"].ToString() : "";
            //TextObject txtPtype = rpcp.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
            //txtPtype.Text = dsrpt.Tables[1].Rows.Count < 0 ? dsrpt.Tables[1].Rows[0]["prjtyp"].ToString() : "";
            TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date :" + System.DateTime.Now.ToString("dd-MMM-yyyy");



            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rpcp.SetDataSource(ds2.Tables[0]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptPrjReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            ReportDocument rpcp = new RealERPRPT.R_01_LPA.RptFeaLandDevProposal();//RptFeaProject();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;


            DataTable dt = (DataTable)ViewState["tblfeaprj"];


            //DataView dv = new DataView(dt);

            //dv.RowFilter = "prgdesc1<>''"; // query example = "id = 10"

            //DataTable dt1 = dv.ToTable();






            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.lblMsg.Text = "";
            //int rindex = this.rbtnList1.SelectedIndex;
            //switch(rindex)
            //{
            //    case 0:              
            //        this.GetProjectInfo();
            //        this.chkAllRes.Visible = false;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;             
            //    case 1:
            //        this.ShowRevenue();
            //        this.chkAllRes.Visible = true;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;
            //    case 2:
            //        this.ShowCost();
            //        this.chkAllRes.Visible = true;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;
            //    case 3:
            //        this.ShowLandBenifit();
            //        this.chkAllRes.Visible = true;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;
            //    case 4:
            //        this.ShowReport();
            //        this.chkAllRes.Visible = false;
            //        this.MultiView1.ActiveViewIndex = rindex;
            //        break;
            //}

        }

        private void GetProjectInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            string fpactcode = this.ddlProjectName.SelectedValue.ToString();

            // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();



            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LDPROJECTINFO", pactcode, fpactcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds1.Tables[0];
            this.Data_Bind();
        }



        private void ShowRevenue()
        {


            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '01%'";
            string CostOrSale = "82%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPROSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrj.DataSource = null;
                this.gvFeaPrj.DataBind();
                this.gvFeaPrjFCS.DataSource = null;
                this.gvFeaPrjFCS.DataBind();
                return;
            }
            this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            ViewState["tblfeaprj"] = ds2.Tables[0];
            this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            this.gvFeaPrjFCS.DataBind();
            this.gvlpsaldis.DataSource = ds2.Tables[2];
            this.gvlpsaldis.DataBind();
            if (ds2.Tables[1].Rows.Count != 0)
                ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
            if (ds2.Tables[2].Rows.Count != 0)
            {
                DataView dv = ds2.Tables[2].DefaultView;
                dv.RowFilter = "infcod like('8301%')";
                DataTable dts = dv.ToTable();
                ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFcompanyshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(company)", "")) ?
                  0.00 : dts.Compute("Sum(company)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFtcompanyshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(totalcom)", "")) ?
                  0.00 : dts.Compute("Sum(totalcom)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFtotalshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(lsizes)", "")) ?
                                      0.00 : dts.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFlownershsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(lowner)", "")) ?
                    0.00 : dts.Compute("Sum(lowner)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFpfrmlownershsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(pflowner)", "")) ?
                  0.00 : dts.Compute("Sum(pflowner)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFadjmntsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(adjmnt)", "")) ?
                  0.00 : dts.Compute("Sum(adjmnt)", ""))).ToString("#,##0;(#,##0); ");
            }

            this.Data_Bind();
        }

        private void ShowCost()
        {

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "(infcod like '0[2-9]%'  or infcod like '1[0-9]%'  or infcod like '2[0-9]%' or infcod like '3[0-9]%'  or infcod like '4[0-9]%')";
            string CostOrSale = "81%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPROSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrjC.DataSource = null;
                this.gvFeaPrjC.DataBind();
                this.gvFeaPrjFC.DataSource = null;
                this.gvFeaPrjFC.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            this.gvFeaPrjFC.DataSource = ds2.Tables[1];
            this.gvFeaPrjFC.DataBind();
            if (ASTUtility.Left(comcod, 1) == "2")
            {
                this.gvFeaPrjFC.Columns[8].Visible = false;
            }
            if (ASTUtility.Left(comcod, 1) != "2")
            {
                this.gvFeaPrjFC.Columns[12].Visible = false;
            }
            ds2.Dispose();
            if (ds2.Tables[1].Rows.Count != 0)
            {
                ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(percntge)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(percntge)", ""))).ToString("#,##0.00;(#,##0.00); ") + " %";
            }
            this.Data_Bind();


        }

        private void ShowLandBenifit()
        {
            //sircode like '0[89]%'  or  sircode like '1[0-9]%'

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '5[1-2]%'";
            string CostOrSale = "";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPROSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaLOwner.DataSource = null;
                this.gvFeaLOwner.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ds2.Dispose();
            this.Data_Bind();
        }





        private void ShowReport()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //RPTPRJFEALDP
            // DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITY", pactcode, "", "", "", "", "", "", "", "");
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "RPTPRJFEALDP", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrjRep.DataSource = null;
                this.gvFeaPrjRep.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = this.HiddenSameData(ds2.Tables[0]);
            DataTable dt = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            string grp = dt1.Rows[0]["grp"].ToString();
            string subgrp = dt1.Rows[0]["subgrp"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["subgrp"].ToString() == subgrp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    subgrp = dt1.Rows[j]["subgrp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["subgrpdesc"] = "";

                }

                else
                {
                    if (dt1.Rows[j]["subgrp"].ToString() == subgrp)
                    {
                        dt1.Rows[j]["subgrpdesc"] = "";
                    }

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";
                    }

                    grp = dt1.Rows[j]["grp"].ToString();
                    subgrp = dt1.Rows[j]["subgrp"].ToString();

                }

            }
            return dt1;

        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            string qtype = this.Request.QueryString["Type"];
            switch (qtype)
            {
                case "PrjInfo":
                    this.gvProjectInfo.DataSource = dt;
                    this.gvProjectInfo.DataBind();
                    break;
                case "Cost":
                    this.gvFeaPrjC.DataSource = dt;
                    this.gvFeaPrjC.DataBind();
                    this.FooterCal();
                    break;
                case "Revenue":
                    this.gvFeaPrj.DataSource = dt;
                    this.gvFeaPrj.DataBind();
                    this.FooterCal();
                    break;
                case "PrjReport":
                    this.gvFeaPrjRep.DataSource = dt;
                    this.gvFeaPrjRep.DataBind();
                    break;

            }


            //int rindex = this.rbtnList1.SelectedIndex;
            //DataTable dt = (DataTable)ViewState["tblfeaprj"];
            //switch (rindex)
            //{

            //    case 1:

            //        this.gvFeaPrj.DataSource = dt;
            //        this.gvFeaPrj.DataBind();
            //        if (Request.QueryString["Type"].ToString() == "LandEntry")
            //        {
            //            ((LinkButton)this.gvFeaPrj.FooterRow.FindControl("lbtnFUpdateSales")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;

            //        }
            //        this.FooterCal();
            //        break;

            //    case 2:
            //        this.gvFeaPrjC.DataSource = dt;
            //        this.gvFeaPrjC.DataBind();
            //        if (Request.QueryString["Type"].ToString() == "LandEntry")
            //        {
            //            ((LinkButton)this.gvFeaPrjC.FooterRow.FindControl("lbtnfUpdateCost")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
            //        }
            //        this.FooterCal();
            //        break;

            //    case 3:
            //        this.gvFeaLOwner.DataSource = dt;
            //        this.gvFeaLOwner.DataBind();
            //        if (Request.QueryString["Type"].ToString() == "LandEntry")
            //        {
            //            if(dt.Rows.Count>0)
            //            ((LinkButton)this.gvFeaLOwner.FooterRow.FindControl("lbtnfUpdateLOwner")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
            //        }
            //        this.FooterCal();
            //        break;

            //    case 4:
            //        this.gvFeaPrjRep.DataSource = dt;
            //        this.gvFeaPrjRep.DataBind();
            //        break;



            //   }


        }
        private void FooterCal()
        {

            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            if (dt.Rows.Count == 0)
                return;
            double Stotalsize, stotalAmt;
            string qtype = this.Request.QueryString["Type"];
            switch (qtype)
            {
                case "PrjInfo":

                    break;
                case "Cost":
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFSFT")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(persft)", "")) ?
                        0.00 : dt.Compute("Sum(persft)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc01")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt01)", "")) ?
                        0.00 : dt.Compute("Sum(amt01)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt02)", "")) ?
                        0.00 : dt.Compute("Sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt03)", "")) ?
                        0.00 : dt.Compute("Sum(amt03)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "Revenue":
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "infcod like('0101%') or infcod like('0102%')";
                    DataTable dts = dv.ToTable();
                    Stotalsize = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(tsize)", "")) ?
                    0.00 : dts.Compute("Sum(tsize)", "")));
                    stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt01)", "")) ? 0.00 : dt.Compute("Sum(amt01)", "")));
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFTsize")).Text = Stotalsize.ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFSratepsft01")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt01")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt02)", "")) ?
                        0.00 : dt.Compute("Sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt03)", "")) ?
                        0.00 : dt.Compute("Sum(amt03)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "PrjReport":
                    break;

            }

            //DataTable dt = (DataTable)ViewState["tblfeaprj"];
            //if (dt.Rows.Count == 0)
            //    return;
            //double Stotalsize, stotalAmt;
            //int rindex = this.rbtnList1.SelectedIndex;
            //switch (rindex)
            //{

            //    case 1:
            //         DataView dv = dt.DefaultView;
            //         dv.RowFilter = "infcod like('0101%') or infcod like('0102%')";
            //         DataTable dts = dv.ToTable();
            //         Stotalsize = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(tsize)", "")) ?
            //         0.00 : dts.Compute("Sum(tsize)", "")));
            //         stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt01)", "")) ? 0.00 : dt.Compute("Sum(amt01)", "")));
            //        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFTsize")).Text = Stotalsize.ToString("#,##0.00;(#,##0.00); ");
            //        //((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFSratepsft01")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
            //        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt01")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
            //        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt02)", "")) ?
            //            0.00 : dt.Compute("Sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
            //        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt03)", "")) ?
            //            0.00 : dt.Compute("Sum(amt03)", ""))).ToString("#,##0;(#,##0); ");
            //        break;
            //    case 2:
            //          //((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFsalrate01")).Text =Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salrate01)", "")) ?
            //          //0.00 : dt.Compute("Sum(salrate01)", ""))).ToString("#,##0.00;(#,##0.00); ") ;

            //        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc01")).Text =Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt01)", "")) ?
            //            0.00 : dt.Compute("Sum(amt01)", ""))).ToString("#,##0;(#,##0); ");
            //        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc02")).Text =Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt02)", "")) ?
            //            0.00 : dt.Compute("Sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
            //        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt03)", "")) ?
            //            0.00 : dt.Compute("Sum(amt03)", ""))).ToString("#,##0;(#,##0); ");

            //        break;

            //    case 3:
            //         Stotalsize = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsize)", "")) ?
            //         0.00 : dt.Compute("Sum(tsize)", "")));
            //         stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt01)", "")) ?
            //         0.00 : dt.Compute("Sum(amt01)", "")));
            //         ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFTsizel")).Text = Stotalsize.ToString("#,##0;(#,##0); ");
            //        // ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFsalratel01")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
            //         ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFAmtl01")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
            //         ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFAmtl02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt02)", "")) ?
            //             0.00 : dt.Compute("Sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
            //        ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFAmtl03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt03)", "")) ?
            //            0.00 : dt.Compute("Sum(amt03)", ""))).ToString("#,##0;(#,##0); ");


            //         break;
            //}

        }


        protected void gvFeaPrjRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lgvgroupdesc");
                Label ToSize = (Label)e.Row.FindControl("lgtsizecRep");
                Label RatepSft = (Label)e.Row.FindControl("lgsalraterep");
                Label amt = (Label)e.Row.FindControl("lgvAmtrep");
                Label lgvAmt01 = (Label)e.Row.FindControl("lgvAmtrep01");
                Label lgvAmt02 = (Label)e.Row.FindControl("lgvAmtrep02");
                Label lgvAmt03 = (Label)e.Row.FindControl("lgvAmtrep03");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    ToSize.Font.Bold = true;
                    // RatepSft.Font.Bold = true;
                    //amt01.Font.Bold = true;
                    //amt02.Font.Bold = true;
                    lgvAmt01.Font.Bold = true;
                    lgvAmt02.Font.Bold = true;
                    lgvAmt03.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");


                }

            }
        }
    }
}