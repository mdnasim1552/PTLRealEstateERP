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
namespace RealERPWEB.F_02_Fea
{
    public partial class RptProjectFeasibility03 : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.ProjectName();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString().Trim() == "PrjInfo") ? "Project Feasibility Project Information" : ((Request.QueryString["Type"].ToString().Trim() == "Cost") ? "Project Feasibility Cost Information" : "Project Feasibility Revenue Information");
                this.VeiwShow();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
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
        private void VColoumnAHeaderChange()
        {

            string pactcode = ASTUtility.Left(this.ddlProjectName.SelectedValue, 4);
            switch (pactcode)
            {
                case "0199":

                    this.gvFeaPrjFC.Columns[12].HeaderText = "Total Khata";
                    this.gvFeaPrjC.Columns[14].HeaderText = "Cost Per Khata";
                    break;
                default:
                    this.gvFeaPrjFC.Columns[12].HeaderText = "Total SFT";
                    this.gvFeaPrjC.Columns[14].HeaderText = "Cost Per SFT";
                    break;



            }


        }
        private void VeiwShow()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "PrjInfo":
                    this.ddlGroup.Visible = false;
                    break;
                case "Cost":

                    break;

                case "Revenue":

                    break;
            }

        }
        private void ProjectName()
        {
            string comcod = this.GetComCode();
            string Filter1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_02", "GET_PROJECTLISTGRP", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ProjectName();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {

            this.lbtnOk.Text = "OK";
            this.Load_Grid();
            return;


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            string pactcode = (ASTUtility.Right(this.ddlProjectName.SelectedValue.ToString(), 3) == "000" && ASTUtility.Right(this.ddlProjectName.SelectedValue.ToString(), 5) != "00000") ? ASTUtility.Left(this.ddlProjectName.SelectedValue.ToString(), 9) + "%"
                : (ASTUtility.Right(this.ddlProjectName.SelectedValue.ToString(), 5) == "00000") ? ASTUtility.Left(this.ddlProjectName.SelectedValue.ToString(), 7) + "%"
                : this.ddlProjectName.SelectedValue.ToString();
            string qtype = this.Request.QueryString["Type"];
            ReportDocument rpcp = new ReportDocument();
            DataSet dsrpt = new DataSet();


            if (qtype == "PrjInfo")
            {
                rpcp = new RealERPRPT.R_02_Fea.RptPrjFeasInfo();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                CompName.Text = comname;
                TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
                txtPrjName.Text = prjname;
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Print Report";
                    string eventdesc2 = "";// this.rbtnList1.SelectedItem.ToString();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                rpcp.SetDataSource((DataTable)ViewState["tblprjinfo"]);
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;
            }
            if (qtype == "Cost")
            {
                string Code = "(infcod like '0[3-5]%')";
                string CostOrSale = "81%";
                string mRptGroup = Convert.ToString(this.ddlGroup.SelectedIndex);
                mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
                string Calltype = (ASTUtility.Right(this.ddlProjectName.SelectedValue.ToString(), 3) != "000") ? "RPT_GETPROSALES" : "RPT_GETPROSALESGROUP";
                dsrpt = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_02", Calltype, pactcode, Code, CostOrSale, mRptGroup, "", "", "", "", "");
                if (dsrpt == null)
                {
                    return;
                }
                ViewState["tblfeaprj"] = this.HiddenSameData1(dsrpt.Tables[0]);
                DataTable dt = (DataTable)ViewState["tblfeaprj"];

                //if (ASTUtility.Left(comcod, 1) == "2" && mRptGroup == "4")
                //{
                //    rpcp = new RealERPRPT.R_02_Fea.RptProjectFeasibility02L();
                //}
                //if (ASTUtility.Left(comcod, 1) == "2" && mRptGroup != "4")
                //{
                //    rpcp = new RealERPRPT.R_02_Fea.RptProjectFeasibility02Cost2L();
                //}
                //else if (ASTUtility.Left(comcod, 1) != "2" && mRptGroup == "4")
                //{
                //    rpcp = new RealERPRPT.R_02_Fea.RptProjectFeasibility02();
                //}
                //else if (ASTUtility.Left(comcod, 1) != "2" && mRptGroup != "4")
                //{
                //    rpcp = new RealERPRPT.R_02_Fea.RptProjectFeasibility02Cost2();
                //}
                if (ASTUtility.Left(comcod, 1) == "2")
                {
                    rpcp = new RealERPRPT.R_02_Fea.RptProFeaLandDevCost();
                }
                else if (ASTUtility.Right(comcod, 2) == "10")
                {
                    rpcp = new RealERPRPT.R_02_Fea.RptProFeaCityDevCost();


                    TextObject txtcs1 = rpcp.ReportDefinition.ReportObjects["txtcs1"] as TextObject;
                    txtcs1.Text = (ASTUtility.Left(this.ddlProjectName.SelectedValue, 4) == "0199") ? "Cost/Khata" : "Cost/SFT";
                    TextObject txtcs2 = rpcp.ReportDefinition.ReportObjects["txtcs2"] as TextObject;
                    txtcs2.Text = (ASTUtility.Left(this.ddlProjectName.SelectedValue, 4) == "0199") ? "Cost/Khata" : "Cost/SFT";
                    TextObject txtbep1 = rpcp.ReportDefinition.ReportObjects["txtbep1"] as TextObject;
                    txtbep1.Text = (ASTUtility.Left(this.ddlProjectName.SelectedValue, 4) == "0199") ? "BEP/ Khata" : "BEP/ SFT";
                    TextObject txtbep2 = rpcp.ReportDefinition.ReportObjects["txtbep2"] as TextObject;
                    txtbep2.Text = (ASTUtility.Left(this.ddlProjectName.SelectedValue, 4) == "0199") ? "BEP/ Khata" : "BEP/ SFT";
                    TextObject txttsft = rpcp.ReportDefinition.ReportObjects["txttsft"] as TextObject;
                    txttsft.Text = (ASTUtility.Left(this.ddlProjectName.SelectedValue, 4) == "0199") ? "Total Khata" : "Total SFT";
                }

                else
                {
                    rpcp = new RealERPRPT.R_02_Fea.RptProFeaCost();


                }
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                CompName.Text = comname;
                TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
                txtPrjName.Text = prjname;
                TextObject txtPrjInfo = rpcp.ReportDefinition.ReportObjects["txtPrjInfo"] as TextObject;
                txtPrjInfo.Text = dsrpt.Tables[1].Rows.Count > 0 ? "Storied: " + Convert.ToDouble(dsrpt.Tables[1].Rows[0]["prjinfo"].ToString()).ToString("#,##0.00;(#,##0.00); ") : "";
                TextObject txtPrjAdd = rpcp.ReportDefinition.ReportObjects["txtPrjAdd"] as TextObject;
                txtPrjAdd.Text = dsrpt.Tables[1].Rows.Count > 0 ? "Address: " + dsrpt.Tables[1].Rows[0]["prjadd"].ToString() : "";
                TextObject txtPtype = rpcp.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
                txtPtype.Text = dsrpt.Tables[1].Rows.Count > 0 ? "Project Type: " + dsrpt.Tables[1].Rows[0]["prjtyp"].ToString() : "";

                TextObject txtBep1 = rpcp.ReportDefinition.ReportObjects["txtBep11"] as TextObject;
                txtBep1.Text = dsrpt.Tables[2].Rows.Count > 0 ? dsrpt.Tables[2].Rows[0]["bep1"].ToString() + " - " + Convert.ToDouble(dsrpt.Tables[2].Rows[0]["bepv1"]).ToString("#,##0;(#,##0); ") : "";
                TextObject txtBep2 = rpcp.ReportDefinition.ReportObjects["txtBep21"] as TextObject;
                txtBep2.Text = dsrpt.Tables[2].Rows.Count > 0 ? dsrpt.Tables[2].Rows[0]["bep2"].ToString() + " - " + Convert.ToDouble(dsrpt.Tables[2].Rows[0]["bepv2"]).ToString("#,##0;(#,##0); ") : "";
                TextObject txtBep3 = rpcp.ReportDefinition.ReportObjects["txtBep31"] as TextObject;
                txtBep3.Text = dsrpt.Tables[2].Rows.Count > 0 ? dsrpt.Tables[2].Rows[0]["bep3"].ToString() + " - " + Convert.ToDouble(dsrpt.Tables[2].Rows[0]["bepv3"]).ToString("#,##0;(#,##0); ") : "";



                //(ASTUtility.Left(this.ddlProjectName.SelectedValue, 4) == "0199") ? "Total Khata" : "Total SFT";  txtcs1, txtbep1, txttsft

                TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtDate.Text = "Date :" + System.DateTime.Now.ToString("dd-MMM-yyyy");


                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Print Report";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;
            }
            if (qtype == "Revenue")
            {
                string mRptGroup = Convert.ToString(this.ddlGroup.SelectedIndex);
                DataSet dsrevpt;

                string Code = "infcod like '83%'";
                string CostOrSale = "82%";
                dsrevpt = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_02", "RPT_GETPROREVENUE", pactcode, Code, CostOrSale, mRptGroup, "", "", "", "", "");

                //if (mRptGroup == "4")
                //{
                //    rpcp = new RealERPRPT.R_02_Fea.RptPrjFeasRevnue();
                //}
                //else
                //{
                //    rpcp = new RealERPRPT.R_02_Fea.RptPrjFeasRevnueDet();

                //}
                rpcp = new RealERPRPT.R_02_Fea.RptPrjFeasCityRevnue();

                if (dsrevpt == null)
                {
                    return;
                }
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                CompName.Text = comname;
                TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
                txtPrjName.Text = prjname;
                TextObject txtPrjInfo = rpcp.ReportDefinition.ReportObjects["txtPrjInfo"] as TextObject;
                txtPrjInfo.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjinfo"].ToString() : "";
                TextObject txtPrjAdd = rpcp.ReportDefinition.ReportObjects["txtPrjAdd"] as TextObject;
                txtPrjAdd.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjadd"].ToString() : "";
                TextObject txtPtype = rpcp.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
                txtPtype.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjtyp"].ToString() : "";
                if (ASTUtility.Left(comcod, 1) != "2")
                {
                    TextObject txtLand = rpcp.ReportDefinition.ReportObjects["txtLand"] as TextObject;
                    txtLand.Text = "Land Owner Share:  " + Convert.ToDouble(dsrevpt.Tables[2].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + " %";
                    TextObject txtComp = rpcp.ReportDefinition.ReportObjects["txtComp"] as TextObject;
                    txtComp.Text = "Company Share:  " + Convert.ToDouble(dsrevpt.Tables[2].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + " %";

                    TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                    txtDate.Text = "Date :" + System.DateTime.Now.ToString("dd-MMM-yyyy");
                }

                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Print Report";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                rpcp.SetDataSource((DataTable)dsrevpt.Tables[0]);
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;
            }

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        // protected void Load_Grid(object sender, EventArgs e)
        protected void Load_Grid()
        {
            string comcod = this.GetComCode();

            string qtype = this.Request.QueryString["Type"];
            //int rindex = this.rbtnList1.SelectedIndex;
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

                    this.ShowRevenue();
                    this.MultiView1.ActiveViewIndex = 2;

                    break;
                    //case 3:
                    //    this.ShowReport();
                    //    this.MultiView1.ActiveViewIndex = rindex;
                    //    break;
            }

        }

        private void GetProjectInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "PROJECTINFO", ProjectCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;
            }
            this.gvProjectInfo.DataSource = ds1.Tables[0];
            this.gvProjectInfo.DataBind();

            ViewState["tblprjinfo"] = ds1.Tables[0];

        }

        private void ShowCost()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "(infcod like '0[3-5]%')";
            string CostOrSale = "81%";
            string mRptGroup = Convert.ToString(this.ddlGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "GETPROSALES02", pactcode, Code, CostOrSale, mRptGroup, "", "", "", "", "");
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
            if (ASTUtility.Right(comcod, 2) != "10")
            {
                this.gvFeaPrjC.Columns[7].Visible = false;
                this.gvFeaPrjC.Columns[8].Visible = false;
            }
            if (ASTUtility.Left(comcod, 1) == "2")
            {
                this.gvFeaPrjFC.Columns[9].Visible = false;
            }
            if (ASTUtility.Left(comcod, 1) != "2")
            {
                this.gvFeaPrjFC.Columns[13].Visible = false;
            }
            ds2.Dispose();
            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFFar")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(far)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(far)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(perent)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(perent)", ""))).ToString("#,##0.00;(#,##0.00); ") + " %";
            this.Data_Bind();
        }

        private void ShowRevenue()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '83%'";
            string CostOrSale = "82%";
            string mRptGroup = Convert.ToString(this.ddlGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "GETPROSALESREVNUE02", pactcode, Code, CostOrSale, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrj.DataSource = null;
                this.gvFeaPrj.DataBind();
                this.gvFeaPrjFCS.DataSource = null;
                this.gvFeaPrjFCS.DataBind();
                return;
            }

            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblsaldis"] = ds2.Tables[2];
            this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            this.gvFeaPrjFCS.DataBind();
            this.gvFeaPrj.DataSource = ds2.Tables[2];
            this.gvFeaPrj.DataBind();
            if (ds2.Tables[1].Rows.Count != 0)
                ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lblgrFBAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
            //        0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lblgrFMAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(minamt)", "")) ?
            //0.00 : dt.Compute("Sum(minamt)", ""))).ToString("#,##0;(#,##0); ");


            this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.Data_Bind();
            ds2.Dispose();

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

        private DataTable HiddenSameData1(DataTable dt1)
        {
            string infcod1 = dt1.Rows[0]["infcod1"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["infcod1"].ToString() == infcod1)
                {
                    infcod1 = dt1.Rows[j]["infcod1"].ToString();
                    dt1.Rows[j]["infdesc1"] = "";
                }



                infcod1 = dt1.Rows[j]["infcod1"].ToString();
            }
            return dt1;
        }

        private void Data_Bind()
        {
            string comcod = this.GetComCode();
            string qtype = this.Request.QueryString["Type"];
            //int rindex = this.rbtnList1.SelectedIndex;
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            switch (qtype)
            {

                case "Cost":
                    this.gvFeaPrjC.DataSource = dt;
                    this.gvFeaPrjC.DataBind();
                    this.FooterCal();
                    break;
                case "Revenue":

                    this.gvFeaPrjsalrev.DataSource = dt;
                    this.gvFeaPrjsalrev.DataBind();
                    this.FooterCal();

                    break;
                    //case 3:
                    //    this.gvFeaPrjRep.DataSource = dt;
                    //    this.gvFeaPrjRep.DataBind();
                    //    break;                             
            }
        }
        private void FooterCal()
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            DataTable dt1 = (DataTable)ViewState["tblsaldis"];
            if (dt.Rows.Count == 0)
                return;
            string qtype = this.Request.QueryString["Type"];
            // int rindex = this.rbtnList1.SelectedIndex;
            switch (qtype)
            {

                case "Cost":
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFeAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(estam)", "")) ?
                     0.00 : dt.Compute("Sum(estam)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFphwam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(phwam)", "")) ?
                    0.00 : dt.Compute("Sum(phwam)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFprjwam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(prjwam)", "")) ?
                    0.00 : dt.Compute("Sum(prjwam)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFtcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(prjwam)", "")) ?
                    0.00 : dt.Compute("Sum(tcost)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFaaddAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aadam)", "")) ?
                        0.00 : dt.Compute("Sum(aadam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFexaddAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(eadam)", "")) ?
                     0.00 : dt.Compute("Sum(eadam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFsaveAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(savam)", "")) ?
                     0.00 : dt.Compute("Sum(savam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFtotalAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalam)", "")) ?
                     0.00 : dt.Compute("Sum(totalam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFtotalCpSft")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(costpsft)", "")) ?
                     0.00 : dt.Compute("Sum(costpsft)", ""))).ToString("#,##0;(#,##0); ");


                    break;
                case "Revenue":

                    if (dt.Rows.Count != 0)
                        ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lgvFamtsalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salamt)", "")) ?
                              0.00 : dt.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lblgrFBAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                         0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lblgrFMAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(minamt)", "")) ?
                         0.00 : dt.Compute("Sum(minamt)", ""))).ToString("#,##0;(#,##0); ");

                    DataView dv = dt1.Copy().DefaultView;
                    dv.RowFilter = ("infcod like '8301%'");
                    dt1 = dv.ToTable();
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFtotalsh")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(lsizes)", "")) ?
                       0.00 : dt1.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFlownersh")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(lowner)", "")) ?
                        0.00 : dt1.Compute("Sum(lowner)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFcompanysh")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(company)", "")) ?
                      0.00 : dt1.Compute("Sum(company)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFpfrmlownersh")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(pflowner)", "")) ?
                      0.00 : dt1.Compute("Sum(pflowner)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFadjmnt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(adjmnt)", "")) ?
                          0.00 : dt1.Compute("Sum(adjmnt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFtcompanysh")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(totalcom)", "")) ?
                      0.00 : dt1.Compute("Sum(totalcom)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFConArea")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(iconarea)", "")) ?
                         0.00 : dt1.Compute("Sum(iconarea)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }
        }

        private void UpdateProjectSaleAndCost()
        {

        }

        protected void gvFeaPrj_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //string comcod = this.GetComCode();
            //DataTable dt = (DataTable)ViewState["tblfeaprj"];
            //string PactCode = this.ddlProjectName.SelectedValue.ToString();
            //string Itemcode = ((Label)this.gvFeaPrj.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();
            //bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "DELETEITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "infcod not in('" + Itemcode + "')";
            //ViewState.Remove("tblfeaprj");
            //ViewState["tblfeaprj"] = dv.ToTable();
            //this.Data_Bind();
        }


        protected void gvFeaPrjC_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvFeaPrjRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lgvgroupdesc");
                Label ToSize = (Label)e.Row.FindControl("lgtsizecRep");
                Label RatepSft = (Label)e.Row.FindControl("lgsalraterep");
                Label amt = (Label)e.Row.FindControl("lgvAmtrep");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    groupdesc.Font.Bold = true;
                    ToSize.Font.Bold = true;
                    RatepSft.Font.Bold = true;
                    amt.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");

                }
            }
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.VColoumnAHeaderChange();
        }
    }
}