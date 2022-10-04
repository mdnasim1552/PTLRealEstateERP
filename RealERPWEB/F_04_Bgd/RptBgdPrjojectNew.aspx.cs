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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_04_Bgd
{
    public partial class RptBgdPrjojectNew : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Contains ("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString ().IndexOf ('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Length;
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission (HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Substring (0, indexofamp),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean (hst["permission"]))
                //    Response.Redirect ("~/AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);




                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length==0?false:(Convert.ToBoolean(dr1[0]["printable"]));
                RadioButtonList1.SelectedIndex = 0;
                RadioButtonList1_SelectedIndexChanged(null, null);
                //((Label)this.Master.FindControl("lblTitle")).Text = "Budgeted Reports";
                //(this.Request.QueryString["Type"].ToString().Trim() == "MasterBgdAcWk") ? "Budgeted Income Statement -(Work)"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgd") ? "Budgeted Income Statement -(Resource)"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "PrjInfo") ? "PROJECT INFORMATION"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "WrkVsResource") ? "Budgeted Work Vs. Resource"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "BudgetedCost") ? "Budgeted Total Cost"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "BudgetAlocation") ? "Budget Balance After Approval"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "BgdAlocBal") ? "Budget Balance Information"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "LandPurReg") ? "Land Purchase Register"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "BudgetBal") ? "Budget Balance (Resource Basis)"

                //: (this.Request.QueryString["Type"].ToString().Trim() == "AddBudget") ? "Additional Budget"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "BgdWkVsActual") ? "Budgeted Income Statement -(Budget Vs Actual)"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgdGrWiseDet") ? "Budgeted Cost-Details"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgdFlrDet") ? "Budgeted Details(Catagory Wise)"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "MatRequired") ? "Material Requirements"
                // : (this.Request.QueryString["Type"].ToString().Trim() == "BgdCostResBasis02") ? "Material Group Wise Cost"
                //: "Budgeted Income Statement -Summary";


                this.GetProjectName();



            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionView();
            string value = this.RadioButtonList1.SelectedValue.ToString();

            switch (value)
            {

                case "MasterBgdGrWise":
                    this.lblfrmdate.Visible = false;
                    this.txtfromdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.rptgrp.Visible = true;
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Budgeted Income Statement-Summary";
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                //case "MasterBgd":
                //    ((Label)this.Master.FindControl("lblTitle")).Text = "Budgeted Income Statement -(Resource)";
                //    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                //    break;

                case "WrkVsResource":
                    this.lblfrmdate.Visible = false;
                    this.txtfromdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.rptgrp.Visible = false;
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Budgeted Work Vs. Resource";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;


                case "BudgetBal":
                    this.lblfrmdate.Visible = false;
                    this.txtfromdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.rptgrp.Visible = false;
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Budget Balance (Resource Basis)";
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "MasterBgdResGrWiseDet":

                    DateTime curdate = System.DateTime.Today;
                    DateTime frmdate = Convert.ToDateTime("01" + curdate.ToString("dd-MMM-yyyy").Substring(2));
                    DateTime todate = Convert.ToDateTime(frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy"));
                    this.txtfromdate.Text = frmdate.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = todate.ToString("dd-MMM-yyyy");
                    this.lblfrmdate.Visible = true;
                    this.txtfromdate.Visible = true;
                    this.lbltodate.Visible = true;
                    this.txttodate.Visible = true;
                    this.rptgrp.Visible = false;
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Budget Balance Information";
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;

                case "BgdCostResBasis02":
                    this.lblfrmdate.Visible = false;
                    this.txtfromdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.rptgrp.Visible = false;
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Material Group Wise Cost";
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;

                //case "MasterBgdGrWiseDet":
                //    ((Label)this.Master.FindControl("lblTitle")).Text = "Budgeted Cost-Details";
                //    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                //    break;

                case "MasterBgdFlrDet":
                    this.lblfrmdate.Visible = false;
                    this.txtfromdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.rptgrp.Visible = false;
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Budgeted Details(Catagory Wise)";
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;


                case "BgdAlocBal":
                    this.lblfrmdate.Visible = false;
                    this.txtfromdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.rptgrp.Visible = false;
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Budget Balance Information";
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;
            }
        }

        private void SectionView()
        {
            string Type = this.RadioButtonList1.SelectedValue.ToString();
            switch (Type)
            {
                case "MasterBgdGrWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    string rType = this.ddlReport.SelectedValue.ToString();
                    switch (rType)
                    {
                        case "MasterBgd":
                            this.lblrptgrp.Visible = true;
                            this.lblgrp.Visible = false;
                            this.ddlgrp.Visible = false;
                            this.chkSum.Visible = false;
                            break;
                        case "MasterBgdGrWise":
                            this.lblrptgrp.Visible = false;
                            this.lblgrp.Visible = false;
                            this.ddlgrp.Visible = false;
                            this.chkSum.Visible = false;
                            break;
                        case "MasterBgdGrWiseDet":
                            this.lblrptgrp.Visible = false;
                            this.lblgrp.Visible = true;
                            this.ddlgrp.Visible = true;
                            this.GetGroup();
                            this.chkSum.Visible = true;
                            break;
                    }

                    break;

                case "PrjInfo":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "MasterBgdAcWk":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "WrkVsResource":
                    this.ShowFloorcode();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "BudgetedCost":
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "BudgetAlocation":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case "BgdAlocBal":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.ShowBgdFloorcode();
                    break;

                case "LandPurReg":
                    this.MultiView1.ActiveViewIndex = 7;
                    break;
                case "BudgetBal":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 8;
                    break;

                //case "MasterBgdGrWise":
                //    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //    this.MultiView1.ActiveViewIndex = 9;
                //    break;


                //case "MasterBgdGrWiseDet":
                //    this.GetGroup();
                //    this.chkSum.Visible = true;
                //    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //    this.MultiView1.ActiveViewIndex = 10;
                //    break;

                case "AddBudget":

                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 11;
                    break;



                case "BgdWkVsActual":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 12;
                    break;


                case "BgdCostResBasis02":
                    this.MultiView1.ActiveViewIndex = 13;
                    break;
                case "MasterBgdFlrDet":
                    this.MultiView1.ActiveViewIndex = 14;
                    break;


                case "MatRequired":
                    this.txtDatemreq.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 15;
                    break;


                case "MasterBgdResGrWiseDet":
                    this.MultiView1.ActiveViewIndex = 16;
                    this.chkSum.Visible = true;
                    break;










            }



        }
        private void GetGroup()
        {

            //DropCheck1.Text = "";
            string comcod = this.GetComeCode();
            string SearchMat = "%" + this.txtSrcGroup.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETGROUP", SearchMat, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.DropCheck1.DataTextField = "gendesc";
            this.DropCheck1.DataValueField = "gencode";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            //string txtSProject = this.txtSrcProject.Text.Trim();

            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";//(this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" + this.txtSrcProject.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            //if (Request.QueryString["prjcode"].ToString() == "")
            //{
            //    this.ddlProjectName.DataSource = ds1.Tables[0];
            //}
            //else
            //{
            //    DataTable dt = ds1.Tables[0];
            //    DataView dv = dt.DefaultView;
            //    dv.RowFilter = "actcode like '" + Request.QueryString["prjcode"].ToString() + "'";
            //    DataTable dt1 = dv.ToTable();
            //    this.ddlProjectName.DataSource = dt1 ;
            //}
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataBind();
            if (Request.QueryString["prjcode"].ToString() == "")
            {
                this.ddlProjectName.Enabled = true;
            }
            else
            {
                this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"];
                this.ddlProjectName.Enabled = false;
            }

        }


        private void ShowFloorcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr3);
            DataView dv = dt.DefaultView;
            dv.Sort = "flrcod";
            dt = dv.ToTable();
            this.ddlFloorList.DataTextField = "flrdes";
            this.ddlFloorList.DataValueField = "flrcod";
            this.ddlFloorList.DataSource = dt;
            this.ddlFloorList.DataBind();
            this.ddlFloorList.SelectedValue = "AAA";
        }
        private void ShowBgdFloorcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr3);
            DataView dv = dt.DefaultView;
            dv.Sort = "flrcod";
            dt = dv.ToTable();
            this.ddlBgdFloor.DataTextField = "flrdes";
            this.ddlBgdFloor.DataValueField = "flrcod";
            this.ddlBgdFloor.DataSource = dt;
            this.ddlBgdFloor.DataBind();
            this.ddlBgdFloor.SelectedValue = "AAA";
        }
        protected void lbtnFloorList_Click(object sender, EventArgs e)
        {
            this.ShowFloorcode();
        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Type = this.RadioButtonList1.SelectedValue.ToString();
            if (Type == "WrkVsResource")
            {
                this.ShowFloorcode();

            }
            else if (Type == "BgdAlocBal")
            {
                this.ShowBgdFloorcode();
            }
        }



        protected void lbtOk_Click(object sender, EventArgs e)
        {
          //  RadioButtonList1_SelectedIndexChanged(null, null);
            string Type = this.RadioButtonList1.SelectedValue.ToString();
            switch (Type)
            {
                case "MasterBgdGrWise":
                    string rType = this.ddlReport.SelectedValue.ToString();
                    this.gvBgd.DataSource = null;
                    this.gvBgd.DataBind();
                    this.gvbgdgrwise.DataSource = null;
                    this.gvbgdgrwise.DataBind();
                    this.gvbgdgrwisedet.DataSource = null;
                    this.gvbgdgrwisedet.DataBind();
                    switch (rType)
                    {


                        case "MasterBgd":
                            this.showMstrBgd();
                            break;
                        case "MasterBgdGrWise":
                            this.showMstrBgdGrWise();
                            break;
                        case "MasterBgdGrWiseDet":
                            this.showMstrBgdGrWiseDet();
                            break;
                    }
                    break;

                case "PrjInfo":
                    this.ShowPrjInfo();
                    break;
                case "MasterBgdAcWk":
                    this.showMstrBgdSP();
                    break;
                case "WrkVsResource":
                    this.ShowWorkVsResource();
                    break;
                case "BudgetedCost":
                    this.ShowBudgetedCost();
                    break;
                case "BudgetAlocation":
                    this.ShowBgdAlc();
                    break;
                case "BgdAlocBal":
                    this.ShowWorkVsResVsAloc();
                    break;
                case "LandPurReg":
                    this.ShowLandPurRegister();
                    break;
                case "BudgetBal":
                    this.ShowBudgetBalance();
                    break;
                //case "MasterBgdGrWise":
                //    this.showMstrBgdGrWise();
                //    break;

                //case "MasterBgdGrWiseDet":
                //    this.showMstrBgdGrWiseDet();
                //    break;
                case "AddBudget":
                    this.showAddBudget();
                    break;
                case "BgdWkVsActual":
                    this.showBudgetVsActual();
                    break;
                case "BgdCostResBasis02":
                    this.showBgdCostResBasis02();
                    break;
                case "MasterBgdFlrDet":
                    this.showBgdCostFloorWise();
                    break;
                case "MatRequired":
                    this.showMatRequired();
                    break;



                case "MasterBgdResGrWiseDet":
                    this.showMstrBgResdGrWiseDet();
                    break;
            }
        }
        private void showMstrBgd()
        {
            Session.Remove("tblbgd");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            // string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgd") ? "RPTMASTERBGD" : "RPTMASBGDACWORK";
            string ProType = (ASTUtility.Left(comcod, 1) == "1") ? "SP_REPORT_BGDANALYSIS_01" : "SP_REPORT_BGDANALYSIS";
            string CallType = (ASTUtility.Left(comcod, 1) == "1") ? "RPTMASBGDACRES_01" : "RPTMASBGDACRES";

            DataSet ds2 = purData.GetTransInfo(comcod, ProType, CallType, "", "", pactcode, "000", mRptGroup, "", "", "", "");
            if (ds2 == null)
            {
                this.gvBgd.DataSource = null;
                this.gvBgd.DataBind();
                return;
            }

            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);

            string txtconarea = (Comcode == "2") ? "Development Area: " : "Construction Area: ";
            if (ds2.Tables[1].Rows.Count > 0)
            {
                this.lblConArea.Text = txtconarea + Convert.ToDouble(ds2.Tables[1].Rows[0]["conarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[1].Rows[0]["conunit"].ToString();
                this.lblSalArea.Text = "Saleable Area: " + Convert.ToDouble(ds2.Tables[1].Rows[0]["salarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[1].Rows[0]["salunit"].ToString();
            }
            else
            {
                this.lblConArea.Text = txtconarea;
                this.lblSalArea.Text = "Saleable Area:";

            }

            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);



            this.Data_Bind();


        }

        private void ShowPrjInfo()
        {
            Session.Remove("tblprjinf");

            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETPROJINFO", pactcode, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvprjInf.DataSource = null;
                this.gvprjInf.DataBind();
                return;
            }
            Session["tblprjinf"] = HiddenSameData(ds3.Tables[0]);
            this.gvprjInf.DataSource = (DataTable)Session["tblprjinf"];
            this.gvprjInf.DataBind();
        }

        private void showMstrBgdSP()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string mRptGroup = Convert.ToString(this.ddlRptGroupsp.SelectedIndex);
            //mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string ProType = (ASTUtility.Left(comcod, 1) == "1") ? "SP_REPORT_BGDANALYSIS_01" : "SP_REPORT_BGDANALYSIS";
            string CallType = (ASTUtility.Left(comcod, 1) == "1") ? "RPTMASBGDACWORK_01" : "RPTMASBGDACWORK";

            DataSet ds2 = purData.GetTransInfo(comcod, ProType, CallType, "", "", pactcode, "000", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBgdsp.DataSource = null;
                this.gvBgdsp.DataBind();
                return;
            }

            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);

            string txtconarea = (Comcode == "2") ? "Development Area: " : "Construction Area: ";
            if (ds2.Tables[1].Rows.Count > 0)
            {
                this.lblConAreasp.Text = txtconarea + Convert.ToDouble(ds2.Tables[1].Rows[0]["conarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[1].Rows[0]["conunit"].ToString();
                this.lblSalAreasp.Text = "Saleable Area: " + Convert.ToDouble(ds2.Tables[1].Rows[0]["salarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[1].Rows[0]["salunit"].ToString();
            }
            else
            {
                this.lblConAreasp.Text = txtconarea;
                this.lblSalAreasp.Text = "Saleable Area:";

            }

            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }
        private void showMstrBgdGrWise()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTMASBGDGRPWISE", "", "", pactcode, "000", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBgdsp.DataSource = null;
                this.gvBgdsp.DataBind();
                return;
            }

            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);

            string txtconarea = (Comcode == "2") ? "Development Area: " : "Construction Area: ";
            if (ds2.Tables[1].Rows.Count > 0)
            {
                this.lblConArea.Text = txtconarea + Convert.ToDouble(ds2.Tables[1].Rows[0]["conarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[1].Rows[0]["conunit"].ToString();
                this.lblSalArea.Text = "Saleable Area: " + Convert.ToDouble(ds2.Tables[1].Rows[0]["salarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[1].Rows[0]["salunit"].ToString();
            }
            else
            {
                this.lblConArea.Text = txtconarea;
                this.lblSalArea.Text = "Saleable Area:";
            }

            Session["tblbgd"] = this.HiddenSameData(ds2.Tables[0]);
            Session["tblbbgd"] = ds2.Tables[2];
            this.Data_Bind();
        }

        private void showMstrBgdGrWiseDet()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string summary = this.chkSum.Checked ? "Summary" : "";

            string grp = "";
            //string[] gp = this.DropCheck1.Text.Trim().Split(',');
            string gp = this.DropCheck1.SelectedValue.Trim();
            if (gp.Length > 0)
            {
                if (gp.Trim() == "00000000" || gp.Trim() == "")
                    grp = "";
                else
                    foreach (ListItem s1 in DropCheck1.Items)
                    {
                        if (s1.Selected)
                        {
                            grp = grp + s1.Value.Substring(0, 8);
                        }

                    }

            }

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTMASBGDGRPWISEDET", "", "", pactcode, "000", summary, grp, "", "", "");
            if (ds2 == null)
            {
                this.gvbgdgrwisedet.DataSource = null;
                this.gvbgdgrwisedet.DataBind();
                return;
            }

            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);

            string txtconarea = (Comcode == "2") ? "Development Area: " : "Construction Area: ";
            if (ds2.Tables[2].Rows.Count > 0)
            {
                this.lblConArea.Text = txtconarea + Convert.ToDouble(ds2.Tables[2].Rows[0]["conarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[2].Rows[0]["conunit"].ToString();
                this.lblSalArea.Text = "Saleable Area: " + Convert.ToDouble(ds2.Tables[2].Rows[0]["salarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[2].Rows[0]["salunit"].ToString();
            }
            else
            {
                this.lblConArea.Text = txtconarea;
                this.lblSalArea.Text = "Saleable Area:";

            }

            Session["tblbgd"] = this.HiddenSameData(ds2.Tables[1]);
            Session["tblbbgd"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }




        private void showAddBudget()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTADDITIONALBUDGET", pactcode, "000", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvadwrk.DataSource = null;
                this.gvadwrk.DataBind();
                return;
            }
            Session["tblbgd"] = ds2.Tables[0];
            this.Data_Bind();
        }

        private void showBudgetVsActual()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = this.txtDate.Text.Trim();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTBUDGETVSACTUAL", pactcode, date, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvbgdvac.DataSource = null;
                this.gvbgdvac.DataBind();
                return;
            }



            Session["tblbgd"] = this.HiddenSameData(ds2.Tables[0]);

            this.Data_Bind();


        }
        private void showBgdCostResBasis02()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = this.txtDate.Text.Trim();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTBUDGETEDCOSTRESBASI02", pactcode, date, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }
            Session["tblbgd"] = ds2.Tables[0];
            this.Data_Bind();


        }

        private void showBgdCostFloorWise()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTBGDFLOORWISE", pactcode, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                this.gvBgdFloor.DataSource = null;
                this.gvBgdFloor.DataBind();
                return;
            }
            Session["tblbgd"] = ds.Tables[0];
            Session["tblarea"] = ds.Tables[1];
            this.Data_Bind();

        }

        private void showMatRequired()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = this.txtDatemreq.Text.Trim();

            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTPROJECTREQUIREMENT", pactcode, date, "", "", "", "", "", "", "");
            if (ds == null)
            {
                this.gvmatreq.DataSource = null;
                this.gvmatreq.DataBind();
                return;
            }
            Session["tblbgd"] = ds.Tables[0];

            this.Data_Bind();

        }

        private void ShowWorkVsResource()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Floorcode = this.ddlFloorList.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTWORKVSRESOURCE", pactcode, Floorcode, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvWrkVsRes.DataSource = null;
                this.gvWrkVsRes.DataBind();
                return;
            }

            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);// HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void ShowBudgetedCost()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTBUDGETEDCOST", "", "", pactcode, "000", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBgdtc.DataSource = null;
                this.gvBgdtc.DataBind();
                return;
            }

            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);

            string txtconarea = (Comcode == "2") ? "Development Area: " : "Construction Area: ";
            if (ds2.Tables[1].Rows.Count > 0)
            {
                this.lblConAreatc.Text = txtconarea + Convert.ToDouble(ds2.Tables[1].Rows[0]["conarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[1].Rows[0]["conunit"].ToString();
                this.lblSalAreatc.Text = "Saleable Area: " + Convert.ToDouble(ds2.Tables[1].Rows[0]["salarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[1].Rows[0]["salunit"].ToString();
            }
            else
            {
                this.lblConAreatc.Text = txtconarea;
                this.lblSalAreatc.Text = "Saleable Area:";

            }

            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);
            Session["tblproinfo"] = ds2.Tables[1];
            this.Data_Bind();


        }
        private void ShowBgdAlc()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "RPTMASBGDALLCO", "", "", pactcode, "000", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBgdAlc.DataSource = null;
                this.gvBgdAlc.DataBind();
                return;
            }
            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }
        private void ShowWorkVsResVsAloc()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Floorcode = this.ddlBgdFloor.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "RPTWORKVSRESOURCEVSALLOC", pactcode, Floorcode, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBgdAlcBal.DataSource = null;
                this.gvBgdAlcBal.DataBind();
                return;
            }

            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);// HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void ShowLandPurRegister()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Floorcode = this.ddlBgdFloor.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BUDGETVSEX_PROJECT", "RPTLANDPURCHASE", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvLandPurreg.DataSource = null;
                this.gvLandPurreg.DataBind();
                return;
            }

            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);// HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void ShowBudgetBalance()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = this.txtDate.Text;
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTBUDGETBALANCE", pactcode, date, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBudgetBal.DataSource = null;
                this.gvBudgetBal.DataBind();
                return;
            }

            Session["tblbgd"] = ds2.Tables[0];// HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();



        }




        private void showMstrBgResdGrWiseDet()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string summary = this.chkSum.Checked ? "Summary" : "";

            string grp = "";
            //string[] gp = this.DropCheck1.Text.Trim().Split(',');
            string gp = this.DropCheck1.SelectedValue.Trim();
            if (gp.Length > 0)
            {
                if (gp.Trim() == "00000000" || gp.Trim() == "")
                    grp = "";
                else
                    foreach (ListItem s1 in DropCheck1.Items)
                    {
                        if (s1.Selected)
                        {
                            grp = grp + s1.Value.Substring(0, 8);
                        }

                    }

            }

            string frmdate = this.txtfromdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();


            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTPRJCOSTRESBASIS", "", "", pactcode, "000", summary, grp, frmdate, todate, "");
            if (ds2 == null)
            {
                this.gvbgdgrwisedet.DataSource = null;
                this.gvbgdgrwisedet.DataBind();
                return;
            }

            //string Comcode = ASTUtility.Left((this.GetComeCode()), 1);

            //string txtconarea = (Comcode == "2") ? "Development Area: " : "Construction Area: ";
            //if (ds2.Tables[2].Rows.Count > 0)
            //{
            //    this.lblConAreagrwisedet.Text = txtconarea + Convert.ToDouble(ds2.Tables[2].Rows[0]["conarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[2].Rows[0]["conunit"].ToString();
            //    this.lblSalAreagrwisedet.Text = "Saleable Area: " + Convert.ToDouble(ds2.Tables[2].Rows[0]["salarea"]).ToString("#,##0; (#,##0); ") + " " + ds2.Tables[2].Rows[0]["salunit"].ToString();
            //}
            //else
            //{
            //    this.lblConAreagrwisedet.Text = txtconarea;
            //    this.lblSalAreagrwisedet.Text = "Saleable Area:";

            //  }

            Session["tblbgd"] = this.HiddenSameData(ds2.Tables[1]);
            Session["tblbbgd"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Type = this.RadioButtonList1.SelectedValue.ToString();
            string actcode = "", acgcode = "";
            switch (Type)
            {

                case "MasterBgdGrWise":

                    string rType = this.ddlReport.SelectedValue.ToString();
                    switch (rType)
                    {
                        case "MasterBgd":
                        case "MasterBgdGrWise":
                            actcode = dt1.Rows[0]["actcode"].ToString();
                            for (int j = 1; j < dt1.Rows.Count; j++)
                            {
                                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                                {
                                    actcode = dt1.Rows[j]["actcode"].ToString();
                                    dt1.Rows[j]["actdesc"] = "";
                                }

                                else
                                {
                                    actcode = dt1.Rows[j]["actcode"].ToString();
                                }

                            }
                            break;
                        case "MasterBgdGrWiseDet":
                            acgcode = dt1.Rows[0]["acgcode"].ToString();
                            for (int j = 1; j < dt1.Rows.Count; j++)
                            {
                                if (dt1.Rows[j]["acgcode"].ToString() == acgcode)
                                {
                                    dt1.Rows[j]["acgdesc"] = "";
                                }
                                acgcode = dt1.Rows[j]["acgcode"].ToString();
                            }
                            break;
                    }

                    break;

                //case "MasterBgd":
                case "MasterBgdAcWk":
                case "BudgetedCost":
                case "BudgetAlocation":
                //case "MasterBgdGrWise":
                case "BgdWkVsActual":


                    actcode = dt1.Rows[0]["actcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                        }

                        else
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                        }

                    }
                    break;
                //case "MasterBgdGrWiseDet":
                case "MasterBgdResGrWiseDet":

                    acgcode = dt1.Rows[0]["acgcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["acgcode"].ToString() == acgcode)
                        {
                            dt1.Rows[j]["acgdesc"] = "";
                        }
                        acgcode = dt1.Rows[j]["acgcode"].ToString();
                    }
                    break;

                case "PrjInfo":
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
                    break;
                case "WrkVsResource":
                    string flrcod = dt1.Rows[0]["flrcod"].ToString();
                    string isircode = dt1.Rows[0]["isircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["flrcod"].ToString() == flrcod && dt1.Rows[j]["isircode"].ToString() == isircode)
                        {
                            flrcod = dt1.Rows[j]["flrcod"].ToString();
                            isircode = dt1.Rows[j]["isircode"].ToString();
                            dt1.Rows[j]["flrdes"] = "";
                            dt1.Rows[j]["isirdesc"] = "";
                            dt1.Rows[j]["isirunit"] = "";
                            dt1.Rows[j]["itemqty"] = 0.00;
                        }

                        else
                        {
                            if (dt1.Rows[j]["flrcod"].ToString() == flrcod)
                            {
                                dt1.Rows[j]["flrdes"] = "";
                            }
                            if (dt1.Rows[j]["isircode"].ToString() == isircode)
                            {
                                dt1.Rows[j]["isirdesc"] = "";
                                dt1.Rows[j]["isirunit"] = "";
                                dt1.Rows[j]["itemqty"] = 0.00;
                            }

                            flrcod = dt1.Rows[j]["flrcod"].ToString();
                            isircode = dt1.Rows[j]["isircode"].ToString();
                        }

                    }

                    break;
                case "BgdAlocBal":
                    string flrcoda = dt1.Rows[0]["flrcod"].ToString();
                    string isircodea = dt1.Rows[0]["isircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["flrcod"].ToString() == flrcoda && dt1.Rows[j]["isircode"].ToString() == isircodea)
                        {
                            flrcoda = dt1.Rows[j]["flrcod"].ToString();
                            isircodea = dt1.Rows[j]["isircode"].ToString();
                            dt1.Rows[j]["flrdes"] = "";
                            dt1.Rows[j]["isirdesc"] = "";
                            dt1.Rows[j]["isirunit"] = "";
                            dt1.Rows[j]["itemqty"] = 0.00;
                            dt1.Rows[j]["abgdqty"] = 0.00;
                            dt1.Rows[j]["difqty"] = 0.00;
                        }

                        else
                        {
                            if (dt1.Rows[j]["flrcod"].ToString() == flrcoda)
                            {
                                dt1.Rows[j]["flrdes"] = "";
                            }
                            if (dt1.Rows[j]["isircode"].ToString() == isircodea)
                            {
                                dt1.Rows[j]["isirdesc"] = "";
                                dt1.Rows[j]["isirunit"] = "";
                                dt1.Rows[j]["itemqty"] = 0.00;
                                dt1.Rows[j]["abgdqty"] = 0.00;
                                dt1.Rows[j]["difqty"] = 0.00;
                            }

                            flrcoda = dt1.Rows[j]["flrcod"].ToString();
                            isircodea = dt1.Rows[j]["isircode"].ToString();
                        }

                    }

                    break;


                case "LandPurReg":
                    actcode = dt1.Rows[0]["actcode"].ToString();
                    string dhagno = dt1.Rows[0]["dhagno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode"].ToString() == actcode && dt1.Rows[j]["dhagno"].ToString() == dhagno)
                        {

                            dt1.Rows[j]["actdesc"] = "";
                            dt1.Rows[j]["resunit"] = "";
                            dt1.Rows[j]["dhagno1"] = "";

                        }

                        else
                        {

                            if (dt1.Rows[j]["actcode"].ToString() == actcode)
                                dt1.Rows[j]["actdesc"] = "";


                            if (dt1.Rows[j]["dhagno"].ToString() == dhagno)
                            {
                                dt1.Rows[j]["dhagno1"] = "";
                                dt1.Rows[j]["resunit"] = "";
                            }



                        }

                        actcode = dt1.Rows[j]["actcode"].ToString();
                        dhagno = dt1.Rows[j]["dhagno"].ToString();
                    }

                    break;

            }





            return dt1;


        }


        private void Data_Bind()
        {
            try
            {
                string Type = this.RadioButtonList1.SelectedValue.ToString();
                string Comcode = ASTUtility.Left((this.GetComeCode()), 1);
                switch (Type)
                {
                    case "MasterBgdGrWise":

                        string rType = this.ddlReport.SelectedValue.ToString();

                        switch (rType)
                        {
                            case "MasterBgd":
                                this.gvBgd.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                                this.gvBgd.Columns[6].HeaderText = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
                                this.gvBgd.Columns[7].HeaderText = (Comcode == "2") ? "Saleable Cost Per Khata" : "Saleable Cost Per SFT";

                                this.gvBgd.Columns[9].HeaderText = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
                                this.gvBgd.Columns[10].HeaderText = (Comcode == "2") ? "Saleable Cost Per Khata" : "Saleable Cost Per SFT";
                                this.gvBgd.DataSource = (DataTable)Session["tblbgd"];
                                this.gvBgd.DataBind();
                                break;
                            case "MasterBgdGrWise":
                                this.gvbgdgrwise.DataSource = (DataTable)Session["tblbgd"];
                                this.gvbgdgrwise.DataBind();
                                break;
                            case "MasterBgdGrWiseDet":
                                this.gvbgdgrwisedet.DataSource = (DataTable)Session["tblbgd"];
                                this.gvbgdgrwisedet.DataBind();

                                break;
                        }


                        break;
                    case "MasterBgdAcWk":
                        this.gvBgdsp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBgdsp.Columns[4].HeaderText = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
                        this.gvBgdsp.Columns[5].HeaderText = (Comcode == "2") ? "Saleable Cost Per Khata" : "Saleable Cost Per SFT";
                        this.gvBgdsp.DataSource = (DataTable)Session["tblbgd"];
                        this.gvBgdsp.DataBind();
                        break;


                    case "WrkVsResource":
                        this.gvWrkVsRes.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvWrkVsRes.DataSource = (DataTable)Session["tblbgd"];
                        this.gvWrkVsRes.DataBind();
                        this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;


                    case "BudgetedCost":
                        this.gvBgdtc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBgdtc.Columns[6].HeaderText = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
                        this.gvBgdtc.DataSource = (DataTable)Session["tblbgd"];
                        this.gvBgdtc.DataBind();
                        break;
                    case "BudgetAlocation":
                        this.gvBgdAlc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBgdAlc.DataSource = (DataTable)Session["tblbgd"];
                        this.gvBgdAlc.DataBind();
                        break;
                    case "BgdAlocBal":
                        this.gvBgdAlcBal.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBgdAlcBal.DataSource = (DataTable)Session["tblbgd"];
                        this.gvBgdAlcBal.DataBind();
                        this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;
                    case "LandPurReg":

                        this.gvLandPurreg.DataSource = (DataTable)Session["tblbgd"];
                        this.gvLandPurreg.DataBind();

                        break;

                    case "BudgetBal":
                        this.gvBudgetBal.DataSource = (DataTable)Session["tblbgd"];
                        this.gvBudgetBal.DataBind();
                        this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;

                    //case "MasterBgdGrWise":
                    //    this.gvbgdgrwise.DataSource = (DataTable)Session["tblbgd"];
                    //    this.gvbgdgrwise.DataBind();
                    //    //  this.FooterCalculation((DataTable)Session["tblbgd"]);
                    //    break;


                    //case "MasterBgdGrWiseDet":
                    //    this.gvbgdgrwisedet.DataSource = (DataTable)Session["tblbgd"];
                    //    this.gvbgdgrwisedet.DataBind();
                    //    //  this.FooterCalculation((DataTable)Session["tblbgd"]);
                    //    break;


                    case "AddBudget":
                        this.gvadwrk.DataSource = (DataTable)Session["tblbgd"];
                        this.gvadwrk.DataBind();
                        this.FooterCalculation((DataTable)Session["tblbgd"]);
                        //  this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;

                    case "BgdWkVsActual":
                        this.gvbgdvac.DataSource = (DataTable)Session["tblbgd"];
                        this.gvbgdvac.DataBind();
                        Session["Report1"] = gvbgdvac;
                        ((HyperLink)this.gvbgdvac.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        //  this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;

                    case "BgdCostResBasis02":
                        this.gvRptResBasis.DataSource = (DataTable)Session["tblbgd"];
                        this.gvRptResBasis.DataBind();
                        this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;

                    case "MasterBgdFlrDet":
                        this.gvBgdFloor.DataSource = (DataTable)Session["tblbgd"];
                        this.gvBgdFloor.DataBind();
                        this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;

                    case "MatRequired":
                        this.gvmatreq.DataSource = (DataTable)Session["tblbgd"];
                        this.gvmatreq.DataBind();
                        //  this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;

                    case "MasterBgdResGrWiseDet":
                        this.gvbgrgrpdisedet.DataSource = (DataTable)Session["tblbgd"];
                        this.gvbgrgrpdisedet.DataBind();


                        Session["Report1"] = gvbgrgrpdisedet;

                        if (((DataTable)Session["tblbgd"]).Rows.Count > 0)
                        {
                            ((HyperLink)this.gvbgrgrpdisedet.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }


                        //  this.FooterCalculation((DataTable)Session["tblbgd"]);
                        break;


                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message+ "');", true);


            }

        }


        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            string Type = this.RadioButtonList1.SelectedValue.ToString();
            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);
            switch (Type)
            {
                case "WrkVsResource":

                    ((Label)this.gvWrkVsRes.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(resamt)", "")) ?
                                       0 : dt.Compute("sum(resamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "BgdAlocBal":

                    ((Label)this.gvBgdAlcBal.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(resamt)", "")) ?
                                       0 : dt.Compute("sum(resamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBgdAlcBal.FooterRow.FindControl("lgvFActAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdalamt)", "")) ?
                                       0 : dt.Compute("sum(bgdalamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBgdAlcBal.FooterRow.FindControl("lgvFDifAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(difamt)", "")) ?
                                       0 : dt.Compute("sum(difamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "BudgetBal":
                    ((Label)this.gvBudgetBal.FooterRow.FindControl("lgvFBgdAmtBal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?
                                       0 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBudgetBal.FooterRow.FindControl("lgvFAcAmtBal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rcvamt)", "")) ?
                                       0 : dt.Compute("sum(rcvamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBudgetBal.FooterRow.FindControl("lgvFBalAmtBal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
                                       0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;



                case "AddBudget":
                    ((Label)this.gvadwrk.FooterRow.FindControl("lgvFbgdamtad")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?
                                       0 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvadwrk.FooterRow.FindControl("lgvFadamt1adw")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adamt1)", "")) ?
                                       0 : dt.Compute("sum(adamt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvadwrk.FooterRow.FindControl("lgvFadamt2adw")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adamt2)", "")) ?
                                        0 : dt.Compute("sum(adamt2)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvadwrk.FooterRow.FindControl("lgvFadamt3adw")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adamt3)", "")) ?
                                      0 : dt.Compute("sum(adamt3)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvadwrk.FooterRow.FindControl("lgvFaddamtad")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toadamt)", "")) ?
                                    0 : dt.Compute("sum(toadamt)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.gvadwrk.FooterRow.FindControl("lgvFtobgdamtad")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tobgdamt)", "")) ?
                    0 : dt.Compute("sum(tobgdamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "MasterBgdFlrDet":
                    ((Label)this.gvBgdFloor.FooterRow.FindControl("lgvBgdamtflF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?
                                       0 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBgdFloor.FooterRow.FindControl("lgvconcostflF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(concost)", "")) ?
                                       0 : dt.Compute("sum(concost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBgdFloor.FooterRow.FindControl("lgvsalcostflF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salcost)", "")) ?
                                       0 : dt.Compute("sum(salcost)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "BgdCostResBasis02":
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("rptcod   like '%00' and rptcod   like '4%' ");
                    dt = dv.ToTable();
                    ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFTotalCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ?
                                       0 : dt.Compute("sum(rptamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.RadioButtonList1.SelectedValue.ToString();
            switch (Type)
            {
                case "MasterBgdGrWise":

                    string rType = this.ddlReport.SelectedValue.ToString();

                    switch (rType)
                    {
                        case "MasterBgd":
                            this.PrinMasterBgd();
                            break;
                        case "MasterBgdGrWise":
                            this.PrinMasterBgdSp();
                            break;
                        case "MasterBgdGrWiseDet":
                            this.PrinMasterBgdgrpDet();
                            break;
                    }
                    break;
                case "PrjInfo":
                    this.PrintPrjInfo();
                    break;
                //case "MasterBgdGrWise":
                case "MasterBgdAcWk":
                    this.PrinMasterBgdSp();
                    break;
                case "WrkVsResource":
                    this.PrintWorkvsRes();
                    break;
                case "BudgetedCost":
                    this.PrinBugetedCost();
                    break;
                case "BudgetAlocation":
                    this.PrintBgdAllc();
                    break;
                case "BgdAlocBal":
                    this.PrintWorkvsResVsAlloc();
                    break;
                case "LandPurReg":
                    this.PrintLandPurRegister();
                    break;
                case "BudgetBal":
                    this.PrintBudgetBalance();
                    break;
                //case "MasterBgdGrWiseDet":
                //    this.PrinMasterBgdgrpDet();
                //    break;
                case "AddBudget":
                    this.AddBudgetPrint();
                    break;
                case "BgdWkVsActual":
                    this.RptBugIncoStatement();
                    break;
                case "BgdCostResBasis02":
                    this.RptBugIncmStatementSuma();
                    break;
                case "MasterBgdFlrDet":
                    this.RptBgdFloorWise();
                    break;

                case "MatRequired":
                    this.RptmaterialsReq();
                    break;

                case "MasterBgdResGrWiseDet":
                    this.PrintMasterBgdResGrWiseDet();
                    break;
            }
        }



        private void RptmaterialsReq()
        {
            // IQBAL NAYAN
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
            string ProjectNam = this.ddlProjectName.SelectedItem.Text.Trim().ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblbgd"];

            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.MatReq>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptMaterialsReq", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "PROJECT NAME: " + ProjectNam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "MATERIALS REQUIREMENT"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptBugIncmStatementSuma()
        {
            // IQBAL NAYAN
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
            string ProjectNam = this.ddlProjectName.SelectedItem.Text.Trim().ToString();

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblbgd"];

            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BugIncmStatement>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptBugIncmStatement", lst, null, null);

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
        private void RptBgdFloorWise()
        {    // IQBAL NAYAN
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
            string ProjectNam = this.ddlProjectName.SelectedItem.Text.Trim().ToString();
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblbgd"];
            DataTable dt1 = (DataTable)Session["tblarea"];
            string conarea = dt1.Rows[0]["conarea"].ToString();
            string salarea = dt1.Rows[0]["salarea"].ToString();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BgdFlrWise>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptBgdFlrWise", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + ProjectNam));
            Rpt1.SetParameters(new ReportParameter("conarea", "Construction Area: " + conarea));
            Rpt1.SetParameters(new ReportParameter("salarea", "Saleable Area: " + salarea));

            //Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptBugIncoStatement()
        {
            // IQBAL NAYAN
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
            string ProjectNam = this.ddlProjectName.SelectedItem.Text.Trim().ToString();
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblbgd"];

            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BugIncoStatement>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptBgdWkVsActual", lst, null, null);

            // var emplist = empinfo.DataTableToList<RealEntity.C_81_Hrm.IndvPf.Empinfo>();

            //  if (comcod == "3101" || comcod == "3333")
            // {
            //       Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_90_PF.RptIndvPfAlli", pflist, null, null);
            //   }
            //    else
            //  {
            //      Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIndvPf", pflist, null, null);
            //  }


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + ProjectNam));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "BUDGETED INCOME STATEMENT -(BUDGET VS ACTUAL)"));

            //Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void AddBudgetPrint()
        {
            // IQBAL NAYAN 
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
            string ProjectNam = this.ddlProjectName.SelectedItem.Text.Trim().ToString();

            //Session["tblbgd"] = ds2.Tables[0];

            DataTable dt = (DataTable)Session["tblbgd"];

            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.Additionalbug>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptAdditionalBudget", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectName", ProjectNam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Additional Budget"));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrinMasterBgd()
        {



            //Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblbgd"];
            if (dt1.Rows.Count == 0)
                return;
            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);
            string concost = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
            string salcost = (Comcode == "2") ? "Saleable Cost Per Khata" : "Saleable Cost Per SFT";

            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.ProjBgdCon>();

            LocalReport Rpt1 = new LocalReport();
            if (ASTUtility.Left(comcod, 1) == "1")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptProjBgdCon", lst, null, null);
            }
            else
            {
                this.PrintProBudgetResBasis();
                return;
            }
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Income Statement - (Resource)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("conarea", this.lblConArea.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("salarea", "Saleable Area: " + this.lblSalArea.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("Group", "Group: " + this.ddlRptGroup.SelectedItem.Text));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptstk = new ReportDocument();

            //if (ASTUtility.Left(comcod, 1) == "1")
            //{


            //   rptstk = new RealERPRPT.R_04_Bgd.RptProjBgdCon();
            //}
            //else
            //{

            //    this.PrintProBudgetResBasis();
            //    return;

            //    //  rptstk = new RealERPRPT.R_04_Bgd.RptProjectBgd();
            //}

            //TextObject rpttxtHeaderTitle = rptstk.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = "Budgeted Income Statement -(Resource)";
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;
            //TextObject txtConArea = rptstk.ReportDefinition.ReportObjects["txtConArea"] as TextObject;
            //txtConArea.Text = this.lblConArea.Text.Trim();
            //if (ASTUtility.Left(comcod, 1) != "1")
            //{
            //    TextObject txtsalarea = rptstk.ReportDefinition.ReportObjects["txtsalarea"] as TextObject;
            //    txtsalarea.Text = this.lblSalArea.Text.Trim();
            //}
            //TextObject txtgroup = rptstk.ReportDefinition.ReportObjects["txtgroup"] as TextObject;
            //txtgroup.Text = "Group: " + this.ddlRptGroup.SelectedItem.Text;

            //TextObject txtconcost = rptstk.ReportDefinition.ReportObjects["txtconcost"] as TextObject;
            //txtconcost.Text = concost;
            //if (ASTUtility.Left(comcod, 1) != "1")
            //{
            //    TextObject txtsalcost = rptstk.ReportDefinition.ReportObjects["txtsalcost"] as TextObject;
            //    txtsalcost.Text = salcost;
            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }

        private void PrintProBudgetResBasis()
        {

            //Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //Session["tblbgd"] = this.HiddenSameData(ds2.Tables[0]);
            DataTable dt = (DataTable)Session["tblbgd"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BudgetInmStaSum>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptProjectBgd", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Income Statement - (Resource)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintPrjInfo()
        {
            //Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["tblprjinf"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BProjInfo>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptPrjInfo", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Project Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //** Nayan **
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblprjinf"];
            //if (dt1.Rows.Count == 0)
            //    return;

            //ReportDocument rptstk = new RealERPRPT.R_04_Bgd.RptPrjInfo();
            ////TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            ////txtCompany.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);


            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrinMasterBgdSp()
        {
            //Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //Session["tblbgd"] = this.HiddenSameData(ds2.Tables[0]);
            DataTable dt = (DataTable)Session["tblbgd"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BudgetInmStaSum>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptProjectBgd", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Income Statement - (Work)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //string Comcode = ASTUtility.Left((this.GetComeCode()), 1);
            //string concost = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
            //string salcost = (Comcode == "2") ? "Saleable Cost Per Khata" : "Saleable Cost Per SFT";
            //ReportDocument rptstk = new RealERPRPT.R_04_Bgd.RptProjectBgd();
            //string type = "Budgeted Income Statement - (Work)";
            //TextObject rpttxtHeaderTitle = rptstk.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = type;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;
            //TextObject txtConArea = rptstk.ReportDefinition.ReportObjects["txtConArea"] as TextObject;
            //txtConArea.Text = this.lblConAreasp.Text.Trim();
            //TextObject txtsalarea = rptstk.ReportDefinition.ReportObjects["txtsalarea"] as TextObject;
            //txtsalarea.Text = this.lblSalAreasp.Text.Trim();
            //TextObject txtgroup = rptstk.ReportDefinition.ReportObjects["txtgroup"] as TextObject;
            //txtgroup.Text = "";

            //TextObject txtconcost = rptstk.ReportDefinition.ReportObjects["txtconcost"] as TextObject;
            //txtconcost.Text = concost;
            //TextObject txtsalcost = rptstk.ReportDefinition.ReportObjects["txtsalcost"] as TextObject;
            //txtsalcost.Text = salcost;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private void PrintWorkvsRes()
        {


            //Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt1 = (DataTable)Session["tblbgd"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.WorkvsRes>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptWorkVsResource", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Work Vs Resource"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";













            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //string Comcode = ASTUtility.Left((this.GetComeCode()), 1);

            //ReportDocument rptstk = new RealERPRPT.R_04_Bgd.rptWorkVsResource();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrinBugetedCost()
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
            string ProjectNam = this.ddlProjectName.SelectedItem.Text.Trim().ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();

            DataTable dt = (DataTable)Session["tblbgd"];
            DataTable dt1 = (DataTable)Session["tblproinfo"];

            string conarea = dt1.Rows[0]["conarea"].ToString();
            string salarea = dt1.Rows[0]["salarea"].ToString();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BudgTotalCost>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptPrjBudgetedCost", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("conarea", this.lblConAreatc.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("Landsize", "Land Size = ")); //+ Convert.ToDouble("0" + dt1.Rows[0]["landsize"]).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Total Cost"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //** Block Nayan ***
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();

            //DataTable dt1 = (DataTable)Session["tblbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //DataTable dt2 = (DataTable)Session["tblproinfo"];
            //string Comcode = ASTUtility.Left((this.GetComeCode()), 1);
            //string concost = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";

            //ReportDocument rptstk = new RealERPRPT.R_04_Bgd.RptPrjBudgetedCost();


            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;

            //TextObject txtstoried = rptstk.ReportDefinition.ReportObjects["txtstoried"] as TextObject;
            //txtstoried.Text = dt2.Rows[0]["basement"].ToString() + "+" + dt2.Rows[0]["stored"].ToString() + " Stored " + dt2.Rows[0]["protype"].ToString() + " Building";

            //TextObject txtProAdd = rptstk.ReportDefinition.ReportObjects["txtProAdd"] as TextObject;
            //txtProAdd.Text = dt2.Rows[0]["proadd"].ToString();


            //TextObject txtConArea = rptstk.ReportDefinition.ReportObjects["txtConArea"] as TextObject;
            //txtConArea.Text = this.lblConAreatc.Text.Trim();
            //TextObject txtslandsize = rptstk.ReportDefinition.ReportObjects["txtslandsize"] as TextObject;
            //txtslandsize.Text = "Land Size = " + dt2.Rows[0]["landsize"].ToString() + " " + dt2.Rows[0]["landunit"].ToString();
            //TextObject txtgroup = rptstk.ReportDefinition.ReportObjects["txtgroup"] as TextObject;
            //txtgroup.Text = "";

            //TextObject txtconcost = rptstk.ReportDefinition.ReportObjects["txtconcost"] as TextObject;
            //txtconcost.Text = concost;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintBgdAllc()
        {
            // ** ***Iqbal Nayan    
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblbgd"];
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BudBalAfterAppro>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptProjectBgdVsAlloc", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text));
            //Rpt1.SetParameters(new ReportParameter("Resource", this.ddlReports.SelectedItem.Text.Trim()));
            //Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budget Balance After Approval"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;

            //ReportDocument rptstk = new RealERPRPT.R_04_Bgd.RptProjectBgdVsAlloc();
            //TextObject rpttxtCompName = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rpttxtCompName.Text = comnam;
            //TextObject rpttxtHeaderTitle = rptstk.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = "Budget Balance After Approval";
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintWorkvsResVsAlloc()
        {
            // ** ***Iqbal Nayan    
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblbgd"];
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BugPlanInfo>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptWorkVsResVsAllocDet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + this.ddlProjectName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Work Vs Resource"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;

            //ReportDocument rptstk = new RealERPRPT.R_04_Bgd.rptWorkVsResVsAllocDet();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintLandPurRegister()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblbgd"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.LandPurRegister>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptLandPurRegister", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Land Purchase Register"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Cash Flow";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintBudgetBalance()
        {

            // ** ***Iqbal Nayan    
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblbgd"];
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.bugdBalance>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptBudgetBalanceResource", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("date", "As On: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Balance (Resource)"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstate = new RealERPRPT.R_04_Bgd.rptBudgetBalanceResource();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtprojectname = rptstate.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;
            //TextObject date = rptstate.ReportDefinition.ReportObjects["date"] as TextObject;
            //date.Text = "As On: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");


            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbgd"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Cash Flow";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintMasterBgdResGrWiseDet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)Session["tblbgd"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BudMasterResGroupWiseDate>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptProjectBgdResGrWiseDet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name : " + this.ddlProjectName.SelectedItem.Text.ToString().Substring(4)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Project Cost - (Resource)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("Construction", this.lblConAreagrwisedet.Text.Trim().ToString()));
            //Rpt1.SetParameters(new ReportParameter("SaleableArea", this.lblSalAreagrwisedet.Text.Trim().ToString()));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrinMasterBgdgrpDet()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)Session["tblbgd"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BugCostDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptProjectBgdGrDet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Cost - Details"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("Construction", this.lblConAreagrwisedet.Text.Trim().ToString()));
            Rpt1.SetParameters(new ReportParameter("SaleableArea", this.lblSalAreagrwisedet.Text.Trim().ToString()));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            ////Iqbal  Nayan
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //DataTable dt = (DataTable)Session["tblbgd"];

            //LocalReport Rpt1 = new LocalReport();
            //var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BugCostDetails>();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptProjectBgdGrDet", lst, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text.ToString()));
            //Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Cost - Details"));
            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("Construction", this.lblConAreagrwisedet.Text.Trim().ToString()));
            //Rpt1.SetParameters(new ReportParameter("SaleableArea", this.lblSalAreagrwisedet.Text.Trim().ToString()));
            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }
        protected void gvBgd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgd.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        protected void gvBgd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {


                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);              

                TableCell cell01 = new TableCell();
                cell01.Text = "Sl.No.";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Description";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Unit";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);


                TableCell cell04 = new TableCell();
                cell04.Text = "Qty";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.RowSpan = 2;
                gvrow.Cells.Add(cell04);

                TableCell cell05 = new TableCell();
                cell05.Text = "Rate";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.RowSpan = 2;
                gvrow.Cells.Add(cell05);


                

                TableCell cell06 = new TableCell();
                cell06.Text = "Budgeted";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.Attributes["style"] = "font-weight:bold;";
                cell06.ColumnSpan = 3;
                gvrow.Cells.Add(cell06);



                TableCell cell07 = new TableCell();
                cell07.Text = "Actual Cost(with inflation)";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.Attributes["style"] = "font-weight:bold;";
                cell07.ColumnSpan = 3;
                gvrow.Cells.Add(cell07);
                gvBgd.Controls[0].Controls.AddAt(0, gvrow);



            }

        }
        protected void gvBgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
              
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgvActDesc");
                Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamt");
                Label lblConsCost = (Label)e.Row.FindControl("lgvconcost");
                Label lblSalCost = (Label)e.Row.FindControl("lgvsalcost");
                Label lgvacamt = (Label)e.Row.FindControl("lgvacamt");
                Label lgvconcostwinfla = (Label)e.Row.FindControl("lgvconcostwinfla");
                Label lgvsalcostwinfla = (Label)e.Row.FindControl("lgvsalcostwinfla");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    acresdesc.Font.Bold = true;
                    lblBgdamt.Font.Bold = true;
                    lblConsCost.Font.Bold = true;
                    lblSalCost.Font.Bold = true;
                    lblSalCost.Font.Bold = true;
                    lblSalCost.Font.Bold = true;
                    lgvacamt.Font.Bold = true;
                    lgvsalcostwinfla.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");
                }
            }
        }

        protected void gvBgdsp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdsp.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBgdsp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acresdesc = (Label)e.Row.FindControl("lgvActDescsp");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lblConsCost = (Label)e.Row.FindControl("lgvconcostsp");
                Label lblSalCost = (Label)e.Row.FindControl("lgvsalcostsp");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBgdamtsp");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    acresdesc.Font.Bold = true;
                    hlink1.Font.Bold = true;
                    lblConsCost.Font.Bold = true;
                    lblSalCost.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");
                }
                if (code == "05AAAAAAAAAA")
                {
                    string prjcode = this.ddlProjectName.SelectedValue.ToString();

                    hlink1.Style.Add("color", "blue");
                    hlink1.NavigateUrl = "~/F_04_Bgd/linkBgdPrjAna.aspx?InputType=BgdMainRpt&AnaType=1&&prjcode=" + prjcode;
                }

                //else if (code == "20BBAAAAAAAA")
                //{
                //    hlink1.Style.Add("color", "blue");
                //    hlink1.NavigateUrl = "~/F_02_Fea/RptProjectFeasibility03.aspx?Type=Cost";
                //}
            }
        }
        //protected void ddlpagesizesp_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Data_Bind();
        //}

        protected void gvWrkVsRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWrkVsRes.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        //protected void ddlpagesizewrkvres_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Data_Bind();
        //}



        //protected void ddlpagesizetc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Data_Bind();
        //}
        protected void gvBgdtc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdtc.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBgdtc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgvActDesctc");
                Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamttc");
                Label lblConsCost = (Label)e.Row.FindControl("lgvconcosttc");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    acresdesc.Font.Bold = true;
                    lblBgdamt.Font.Bold = true;
                    lblConsCost.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");
                }

            }

        }

        protected void gvBgdAlc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdAlc.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBgdAlc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgvActDesc");
                Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamt");
                Label Alcamt = (Label)e.Row.FindControl("lgvBGDAl");
                Label Balamt = (Label)e.Row.FindControl("lgvBal");
                Label Balp = (Label)e.Row.FindControl("lgvBalP");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    acresdesc.Font.Bold = true;
                    lblBgdamt.Font.Bold = true;
                    Alcamt.Font.Bold = true;
                    Balamt.Font.Bold = true;
                    Balp.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");
                }

            }

        }
        //protected void ddlPageAlc_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    this.Data_Bind();
        //}
        //protected void ddlBgdPage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Data_Bind();

        //}
        protected void lbtnBgdFlr_Click(object sender, EventArgs e)
        {
            this.ShowBgdFloorcode();
        }
        protected void gvBgdAlcBal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdAlcBal.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvLandPurreg_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label purtarget = (Label)e.Row.FindControl("lgvpurtarget");
                Label purbal = (Label)e.Row.FindControl("lgvpurbal");
                Label landowner = (Label)e.Row.FindControl("lgvlandowner");
                Label landsize = (Label)e.Row.FindControl("lgvlandsize");
                Label lpurchase = (Label)e.Row.FindControl("lgvlpurchase");
                Label lnbalance = (Label)e.Row.FindControl("lgvlnbalance");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    purtarget.Font.Bold = true;
                    purbal.Font.Bold = true;
                    landowner.Font.Bold = true;
                    landsize.Font.Bold = true;
                    lpurchase.Font.Bold = true;
                    lnbalance.Font.Bold = true;
                    landsize.Style.Add("text-align", "right");
                }

            }


        }
        protected void gvBudgetBal_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Width = 50;
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell02 = new TableCell();
                cell02.Width = 200;
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Width = 50;
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;

                TableCell cell04 = new TableCell();
                cell04.Width = 210;
                cell04.Text = "Budget";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;

                TableCell cell05 = new TableCell();
                cell05.Width = 210;
                cell05.Text = "Actual";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 3;


                TableCell cell06 = new TableCell();
                cell06.Width = 210;
                cell06.Text = "Balance";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 3;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvBudgetBal.Controls[0].Controls.AddAt(0, gvrow);
            }


        }
        protected void gvbgdgrwise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // Label acresdesc = (Label)e.Row.FindControl("lgvActDescgrwise");
                LinkButton acresdesc = (LinkButton)e.Row.FindControl("lnkgvActDescgrwise");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lblConsCost = (Label)e.Row.FindControl("lgvconcostgrwise");
                Label lblSalCost = (Label)e.Row.FindControl("lgvsalcostgrwise");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBgdamtgrwise");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }

                if (ASTUtility.Right(code, 3) == "000")
                {

                    acresdesc.Font.Bold = true;
                    hlink1.Font.Bold = true;
                    lblConsCost.Font.Bold = true;
                    lblSalCost.Font.Bold = true;


                    acresdesc.Attributes["style"] = "font-weight:bold; color:blue;";
                    hlink1.Attributes["style"] = "font-weight:bold; color:blue;";
                    lblConsCost.Attributes["style"] = "font-weight:bold; color:blue;";
                    lblSalCost.Attributes["style"] = "font-weight:bold; color:blue;";
                    acresdesc.Style.Add("text-align", "left");
                }

                else if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    acresdesc.Font.Bold = true;
                    hlink1.Font.Bold = true;
                    lblConsCost.Font.Bold = true;
                    lblSalCost.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");
                    acresdesc.Attributes["style"] = "font-weight:bold; color:red;";
                    hlink1.Attributes["style"] = "font-weight:bold; color:red;";
                    lblConsCost.Attributes["style"] = "font-weight:bold; color:red;";
                    lblSalCost.Attributes["style"] = "font-weight:bold; color:red;";
                }



                else if (code == "05AAAAAAAAAA")
                {
                    string prjcode = this.ddlProjectName.SelectedValue.ToString();

                    hlink1.Style.Add("color", "blue");
                    hlink1.NavigateUrl = "~/F_04_Bgd/linkBgdPrjAna.aspx?InputType=BgdMainRpt&AnaType=1&&prjcode=" + prjcode;
                }


                if (ASTUtility.Right(code, 4) == "AAAA")//|| code == "510100000000"
                {
                    acresdesc.Enabled = false;
                }

                else
                {
                    acresdesc.Enabled = true;
                }


            }
        }
        protected void gvbgdgrwisedet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton acresdesc = (LinkButton)e.Row.FindControl("lnkgvActDescgrwisedet");

                Label lblConsCost = (Label)e.Row.FindControl("lgvconcostgrwisedet");
                Label lblSalCost = (Label)e.Row.FindControl("lgvsalcostgrwisedet");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBgdamtgrwisedet");
                string sum = this.chkSum.Checked == true ? "Summary" : "";

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "000000000000")
                {

                    acresdesc.Font.Bold = true;
                    hlink1.Font.Bold = true;
                    lblConsCost.Font.Bold = true;
                    lblSalCost.Font.Bold = true;


                    acresdesc.Attributes["style"] = "font-weight:bold; color:maroon;";
                    hlink1.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblConsCost.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblSalCost.Attributes["style"] = "font-weight:bold; color:maroon;";
                    acresdesc.Style.Add("text-align", "left");
                }


                else if (code == "AAAAAAAAAAAA")
                {
                    //acresdesc.Font.Bold = true;
                    //hlink1.Font.Bold = true;
                    //lblConsCost.Font.Bold = true;
                    //lblSalCost.Font.Bold = true;
                    acresdesc.Attributes["style"] = "font-weight:bold; color:blue;";
                    hlink1.Attributes["style"] = "font-weight:bold; color:blue;";
                    lblConsCost.Attributes["style"] = "font-weight:bold; color:blue;";
                    lblSalCost.Attributes["style"] = "font-weight:bold; color:blue;";
                    acresdesc.Style.Add("text-align", "right");

                }

                if (code == "000000000000" && sum == "Summary")
                {
                    acresdesc.Enabled = true;
                }

                else
                {
                    acresdesc.Enabled = false;
                }






            }
        }


        protected void gvadwrk_RowCreated(object sender, GridViewRowEventArgs e)
        {

            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //    TableCell cell01 = new TableCell();
            //    cell01.Text = "";
            //    cell01.HorizontalAlign = HorizontalAlign.Center;
            //    cell01.ColumnSpan = 1;

            //    TableCell cell02 = new TableCell();
            //    cell02.Text = "";
            //    cell02.HorizontalAlign = HorizontalAlign.Center;
            //    cell02.ColumnSpan = 1;

            //    TableCell cell03 = new TableCell();
            //    cell03.Text = "";
            //    cell03.HorizontalAlign = HorizontalAlign.Center;
            //    cell03.ColumnSpan = 1;

            //    TableCell cell04 = new TableCell();
            //    cell04.Text = "";
            //    cell04.HorizontalAlign = HorizontalAlign.Center;
            //    cell04.ColumnSpan = 1;

            //    TableCell cell05 = new TableCell();
            //    cell05.Text = "";
            //    cell05.HorizontalAlign = HorizontalAlign.Center;
            //    cell05.ColumnSpan = 1;


            //    TableCell cell06 = new TableCell();
            //    cell06.Text = "";
            //    cell06.HorizontalAlign = HorizontalAlign.Center;
            //    cell06.ColumnSpan = 1;


            //    TableCell cell07 = new TableCell();
            //    cell07.Text = "Additional-1";
            //    cell07.HorizontalAlign = HorizontalAlign.Center;
            //    cell07.ColumnSpan = 2;



            //    TableCell cell09 = new TableCell();
            //    cell09.Text = "Additional-2";
            //    cell09.HorizontalAlign = HorizontalAlign.Center;
            //    cell09.ColumnSpan = 2;


            //    TableCell cell11 = new TableCell();
            //    cell11.Text = "Additional-3";
            //    cell11.HorizontalAlign = HorizontalAlign.Center;
            //    cell11.ColumnSpan = 2;


            //    TableCell cell13 = new TableCell();
            //    cell13.Text = "Total Addition";
            //    cell13.HorizontalAlign = HorizontalAlign.Center;
            //    cell13.ColumnSpan = 2;



            //    TableCell cell15 = new TableCell();
            //    cell15.Text = "";
            //    cell15.HorizontalAlign = HorizontalAlign.Center;
            //    cell15.ColumnSpan = 1;

            //    TableCell cell16 = new TableCell();
            //    cell16.Text = "";
            //    cell16.HorizontalAlign = HorizontalAlign.Center;
            //    cell16.ColumnSpan = 1;


            //    gvrow.Cells.Add(cell01);
            //    gvrow.Cells.Add(cell02);
            //    gvrow.Cells.Add(cell03);
            //    gvrow.Cells.Add(cell04);
            //    gvrow.Cells.Add(cell05);
            //    gvrow.Cells.Add(cell06);
            //    gvrow.Cells.Add(cell07);
            //    gvrow.Cells.Add(cell09);
            //    gvrow.Cells.Add(cell11);
            //    gvrow.Cells.Add(cell13);
            //    gvrow.Cells.Add(cell15);
            //    gvrow.Cells.Add(cell16);


            //    gvadwrk.Controls[0].Controls.AddAt(0, gvrow);
            //}

        }

        protected void ibtnFindgroup_Click(object sender, EventArgs e)
        {
            this.GetGroup();
        }










        protected void gvbgdvac_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgvActDescbgdvac");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBgdamtbgdvac");
                Label actualcost = (Label)e.Row.FindControl("lgvactualcost");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    acresdesc.Font.Bold = true;
                    hlink1.Font.Bold = true;
                    actualcost.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");
                }



                if (code == "05AAAAAAAAAA")
                {
                    string prjcode = this.ddlProjectName.SelectedValue.ToString();

                    hlink1.Style.Add("color", "blue");
                    hlink1.NavigateUrl = "~/F_04_Bgd/linkBgdPrjAna.aspx?InputType=BgdMainRpt&AnaType=1&&prjcode=" + prjcode;
                }
                //else if (code == "20BBAAAAAAAA")
                //{
                //    hlink1.Style.Add("color", "blue");
                //    hlink1.NavigateUrl = "~/F_02_Fea/RptProjectFeasibility03.aspx?Type=Cost";
                //}
            }
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
        protected void lnkgvActDescgrwisedet_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string acgcode = ((DataTable)Session["tblbgd"]).Rows[index]["acgcode"].ToString();
            string colst = ((DataTable)Session["tblbgd"]).Rows[index]["colst"].ToString();
            DataTable dt = ((DataTable)Session["tblbgd"]);
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = ("rescode= '000000000000' or rescode= 'AAAAAAAAAAAA'");
            dt = dv.ToTable();

            DataRow[] dr1 = dt.Select("acgcode='" + acgcode + "'");
            dr1[0]["colst"] = (colst == "0") ? "1" : "0";

            // For Status 0
            foreach (DataRow dr2 in dt.Rows)
            {
                if (dr2["acgcode"] != acgcode)
                {
                    dr2["colst"] = "0";

                }
            }

            colst = (dt.Select("acgcode='" + acgcode + "'"))[0]["colst"].ToString();
            if (colst == "1")
            {
                DataTable dtb = ((DataTable)Session["tblbbgd"]).Copy();
                dv = dtb.DefaultView;
                dv.RowFilter = ("acgcode='" + acgcode + "' and  rescode not like '%00000'");
                dtb = dv.ToTable();
                dt.Merge(dtb);

            }


            dv = dt.DefaultView;
            dv.Sort = ("acgcode, rescode");
            Session["tblbgd"] = dv.ToTable();
            this.Data_Bind();

        }

        protected void lnkgvActDescgrwise_OnClick(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string acgcode = ((DataTable)Session["tblbgd"]).Rows[index]["acgcode"].ToString();
            DataTable dt = ((DataTable)Session["tblbgd"]);
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = ("rescode  like '%000' or rescode   like '%AAA'");
            dt = dv.ToTable();

            DataTable dtb = ((DataTable)Session["tblbbgd"]).Copy();
            dv = dtb.DefaultView;
            dv.RowFilter = ("acgcode='" + acgcode + "'");
            dtb = dv.ToTable();
            dt.Merge(dtb);



            dv = dt.DefaultView;
            dv.Sort = ("grp, acgcode");
            DataTable dt1 = dv.ToTable();
            Session["tblbgd"] = this.HiddenSameData(dv.ToTable());
            this.Data_Bind();
        }
        protected void gvmatreq_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvrsirdescmreq = (HyperLink)e.Row.FindControl("hlnkgvrsirdescmreq");
                Label lgvrequiredqty = (Label)e.Row.FindControl("lgvrequiredqty");
                double reqqty = Convert.ToDouble((DataBinder.Eval(e.Row.DataItem, "requirqty")).ToString());
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string rsircode = Convert.ToString((DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString());

                if (ASTUtility.Right(rsircode, 3) == "000")
                {
                    hlnkgvrsirdescmreq.Attributes["style"] = "color:maroon; fontweight:bold;";
                }
                else
                {

                    hlnkgvrsirdescmreq.NavigateUrl = "~/F_04_Bgd/LinkMatRequirement.aspx?pactcode=" + pactcode + "&rsircode=" + rsircode + "&date=" + Convert.ToDateTime(this.txtDatemreq.Text).ToString("dd-MMM-yyyy");

                }

                if (reqqty > 0)
                {
                    lgvrequiredqty.Attributes["style"] = "color:red";

                }
                else if (reqqty < 0)
                {
                    lgvrequiredqty.Attributes["style"] = "color:green";
                }



            }
        }
        protected void gvbgrgrpdisedet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {




                LinkButton acresdesc = (LinkButton)e.Row.FindControl("lnkgvActDescgrwisedetres");


                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBgdamtgrwisedetres");
                HyperLink hlnkgvacamgrwisedetres = (HyperLink)e.Row.FindControl("hlnkgvacamgrwisedetres");
                HyperLink hlnkgvacamdrgrwisedetres = (HyperLink)e.Row.FindControl("hlnkgvacamdrgrwisedetres");

                
                string sum = this.chkSum.Checked == true ? "Summary" : "";

                string acgcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acgcode")).ToString();
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }


                else if (acgcode.Substring(4, 4) == "0000" && code == "000000000000")
                {


                    acresdesc.Attributes["style"] = "font-weight:bold; font-size:14px; color:maroon;";
                    hlink1.Attributes["style"] = "font-weight:bold; font-size:14px; color:maroon;";
                    hlnkgvacamgrwisedetres.Attributes["style"] = "font-weight:bold; font-size:14px; color:maroon;";
                    hlnkgvacamdrgrwisedetres.Attributes["style"] = "font-weight:bold; font-size:14px; color:maroon;";
                    acresdesc.Style.Add("text-align", "left");



                }

                else if (acgcode.Substring(4, 4) != "0000" && (code == "000000000000" || code == "AAAAAAAAAAAA"))
                {


                    acresdesc.Attributes["style"] = "font-weight:bold; font-size:12px; color:blue;";
                    hlink1.Attributes["style"] = "font-weight:bold; font-size:12px; color:blue;";
                    hlnkgvacamgrwisedetres.Attributes["style"] = "font-weight:bold; font-size:12px; color:blue;";
                    hlnkgvacamdrgrwisedetres.Attributes["style"] = "font-weight:bold; font-size:12px; color:blue;";


                }

                else
                {
                    string comcod = this.GetComeCode();
                    string sdae = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    string frmdate = Convert.ToDateTime(sdae).AddMonths(-2).ToString("dd-MMM-yyyy");
                    //  DateTime date = new DateTime(01,01,"") System.DateTime.Today.ToString("dd-MMM-yyyy");
                    //  string frmdate = "01" + date.Substring(2);
                    string todate = sdae;
                    acresdesc.Attributes["style"] = "font-weight:normal; color:black;";
                    hlink1.Attributes["style"] = "font-weight:normal; color:black;";
                    hlnkgvacamgrwisedetres.Attributes["style"] = "font-weight:normal; color:black;";
                    hlnkgvacamdrgrwisedetres.Attributes["style"] = "font-weight:normal; color:black;";
                    acresdesc.Enabled = false;



                    //  AccMultiReport.aspx? rpttype = spledger & comcod = 3101 & actcode = 160100010004 & rescode = 010200101000 & Date1 = 01 - Jan - 2018 & Date2 = 30 - Jun - 2020 & opnoption =


                    hlnkgvacamgrwisedetres.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?rpttype=spledger&comcod=" + comcod + "&actcode=" + this.ddlProjectName.SelectedValue.ToString() + "&rescode=" + code + "&Date1=" + frmdate + "&Date2=" + todate + "&opnoption=";





                }




                //if (code == "000000000000" && sum == "Summary")
                //{
                //    acresdesc.Enabled = true;
                //}

                //else
                //{
                //    acresdesc.Enabled = false;
                //}



                //if (code == "000000000000")
                //{

                //    acresdesc.Font.Bold = true;
                //    hlink1.Font.Bold = true;
                //    lblacam.Font.Bold = true;



                //    acresdesc.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    hlink1.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lblacam.Attributes["style"] = "font-weight:bold; color:maroon;";

                //    acresdesc.Style.Add("text-align", "left");
                //}


                //else if (code == "AAAAAAAAAAAA")
                //{
                //    //acresdesc.Font.Bold = true;
                //    //hlink1.Font.Bold = true;
                //    //lblConsCost.Font.Bold = true;
                //    //lblSalCost.Font.Bold = true;
                //    acresdesc.Attributes["style"] = "font-weight:bold; color:blue;";
                //    hlink1.Attributes["style"] = "font-weight:bold; color:blue;";
                //    lblacam.Attributes["style"] = "font-weight:bold; color:blue;";

                //    acresdesc.Style.Add("text-align", "right");

                //}

                //if (code == "000000000000" && sum == "Summary")
                //{
                //    acresdesc.Enabled = true;
                //}

                //else
                //{
                //    acresdesc.Enabled = false;
                //}






            }
        }
        protected void lnkgvActDescgrwisedetres_Click(object sender, EventArgs e)
        {



            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string acgcode = ((DataTable)Session["tblbgd"]).Rows[index]["acgcode"].ToString();
            string colst = ((DataTable)Session["tblbgd"]).Rows[index]["colst"].ToString();
            DataTable dt = ((DataTable)Session["tblbgd"]);
            DataView dv = new DataView();
            dv = dt.DefaultView;
            //Main Head
            string actcodei = ((acgcode.Substring(4, 4) == "0000") ? acgcode.Substring(0, 4) : acgcode) + "%";
            int leni = actcodei.Length;
            switch (leni)
            {

                case 9:
                    dv.RowFilter = ("rescode= '000000000000'");

                    break;


                case 5:
                    dv.RowFilter = ("acgcode like '%0000' and rescode= '000000000000'");
                    break;








            }


            dt = dv.ToTable();




            DataRow[] dr1 = dt.Select("acgcode='" + acgcode + "'");
            dr1[0]["colst"] = (colst == "0") ? "1" : "0";

            // For Status 0
            foreach (DataRow dr2 in dt.Rows)
            {
                if (dr2["acgcode"] != acgcode)
                {
                    dr2["colst"] = "0";

                }
            }

            colst = (dt.Select("acgcode='" + acgcode + "'"))[0]["colst"].ToString();
            if (colst == "1")
            {
                DataTable dtb = ((DataTable)Session["tblbbgd"]).Copy();
                acgcode = ((acgcode.Substring(4, 4) == "0000") ? acgcode.Substring(0, 4) : acgcode) + "%";
                int len = acgcode.Length;
                dv = dtb.DefaultView;
                switch (len)
                {

                    case 9:
                        dv.RowFilter = ("acgcode like '" + acgcode + "' and  rescode not like '%0000'");
                        break;


                    case 5:
                        dv.RowFilter = ("acgcode like '" + acgcode + "'  and acgcode not like '%0000' and  rescode like '%000000000000'");
                        break;









                }




                dtb = dv.ToTable();
                dt.Merge(dtb);

            }


            dv = dt.DefaultView;
            dv.Sort = ("rowid");
            Session["tblbgd"] = dv.ToTable();
            this.Data_Bind();




            //int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //string acgcode = ((DataTable)Session["tblbgd"]).Rows[index]["acgcode"].ToString();
            //string colst = ((DataTable)Session["tblbgd"]).Rows[index]["colst"].ToString();
            //DataTable dt = ((DataTable)Session["tblbgd"]);
            //DataView dv = new DataView();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("rescode= '000000000000' or rescode= 'AAAAAAAAAAAA'");
            //dt = dv.ToTable();

            //DataRow[] dr1 = dt.Select("acgcode='" + acgcode + "'");
            //dr1[0]["colst"] = (colst == "0") ? "1" : "0";

            //// For Status 0
            //foreach (DataRow dr2 in dt.Rows)
            //{
            //    if (dr2["acgcode"] != acgcode)
            //    {
            //        dr2["colst"] = "0";

            //    }
            //}

            //colst = (dt.Select("acgcode='" + acgcode + "'"))[0]["colst"].ToString();
            //if (colst == "1")
            //{
            //    DataTable dtb = ((DataTable)Session["tblbbgd"]).Copy();
            //    dv = dtb.DefaultView;
            //    dv.RowFilter = ("acgcode='" + acgcode + "' and  rescode not like '%00000'");
            //    dtb = dv.ToTable();
            //    dt.Merge(dtb);

            //}


            //dv = dt.DefaultView;
            //dv.Sort = ("acgcode, rescode");
            //Session["tblbgd"] = dv.ToTable();
            //this.Data_Bind();
        }
        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvBgd.DataSource = null;
            this.gvBgd.DataBind();
            this.gvbgdgrwise.DataSource = null;
            this.gvbgdgrwise.DataBind();
            this.gvbgdgrwisedet.DataSource = null;
            this.gvbgdgrwisedet.DataBind();
            this.SectionView();
        }

       
    }
}