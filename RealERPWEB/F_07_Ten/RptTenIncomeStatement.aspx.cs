using System;
using System.Collections;
using System.Collections.Generic;
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
using RealERPRPT;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_07_Ten
{
    public partial class RptTenIncomeStatement : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //MASTER BUDGET INFORMATION
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgdAcWk") ? "Budgeted Income Statement -(1) "
                    : (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgd") ? "Budgeted Income Statement -(2)"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "PrjInfo") ? "PROJECT INFORMATION"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "WrkVsResource") ? "BUDGETED WORK VS RESOURCE INFORMATION"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "BudgetedCost") ? "Budgeted Total Cost"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "BudgetAlocation") ? "Budget Balance After Approval"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "BgdAlocBal") ? "Budget Balance Information"
                     : (this.Request.QueryString["Type"].ToString().Trim() == "LandPurReg") ? "Land Purchase Register" : "";

                this.GetProjectName();
                this.SectionView();

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MasterBgd":
                    this.MultiView1.ActiveViewIndex = 0;
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


            }



        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_TAS_INCOMESTATEMENT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }


        private void ShowFloorcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
            string comcod = hst["comcod"].ToString();
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

        protected void imgbtnFindProject_OnClick(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
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

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MasterBgd":
                    this.showMstrBgd();
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
            //// string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgd") ? "RPTMASTERBGD" : "RPTMASBGDACWORK";
            // string ProType = (ASTUtility.Left(comcod, 1) == "1") ? "SP_REPORT_BGDANALYSIS_01" : "SP_REPORT_BGDANALYSIS";
            // string CallType = (ASTUtility.Left(comcod, 1) == "1") ? "RPTMASBGDACRES_01" : "RPTMASBGDACRES";

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_TAS_INCOMESTATEMENT", "RPTINCOMESTRES", "", "", pactcode, "000", mRptGroup, "", "", "", "");
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

            }
            else
            {
                this.lblConArea.Text = txtconarea;


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
            //string ProType = (ASTUtility.Left(comcod, 1) == "1") ? "SP_REPORT_BGDANALYSIS_01" : "SP_REPORT_BGDANALYSIS";
            //string CallType = (ASTUtility.Left(comcod, 1) == "1") ? "RPTMASBGDACWORK_01" : "RPTMASBGDACWORK";

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_TAS_INCOMESTATEMENT", "RPTINCOMESTWORK", "", "", pactcode, "000", "", "", "", "", "");
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

            }
            else
            {
                this.lblConAreasp.Text = txtconarea;

            }

            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);
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

        private DataTable HiddenSameData(DataTable dt1)
        {




            if (dt1.Rows.Count == 0)
                return dt1;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            string actcode = "";
            switch (Type)
            {
                case "MasterBgd":
                case "MasterBgdAcWk":
                case "BudgetedCost":
                case "BudgetAlocation":

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
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);
            switch (Type)
            {
                case "MasterBgd":

                    this.gvBgd.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBgd.Columns[6].HeaderText = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";

                    this.gvBgd.DataSource = (DataTable)Session["tblbgd"];
                    this.gvBgd.DataBind();
                    break;
                case "MasterBgdAcWk":
                    this.gvBgdsp.PageSize = Convert.ToInt32(this.ddlpagesizesp.SelectedValue.ToString());
                    this.gvBgdsp.Columns[4].HeaderText = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
                    //this.gvBgdsp.Columns[5].HeaderText = (Comcode == "2") ? "Saleable Cost Per Khata" : "Saleable Cost Per SFT";
                    this.gvBgdsp.DataSource = (DataTable)Session["tblbgd"];
                    this.gvBgdsp.DataBind();
                    break;


                case "WrkVsResource":
                    this.gvWrkVsRes.PageSize = Convert.ToInt32(this.ddlpagesizewrkvres.SelectedValue.ToString());
                    this.gvWrkVsRes.DataSource = (DataTable)Session["tblbgd"];
                    this.gvWrkVsRes.DataBind();
                    this.FooterCalculation((DataTable)Session["tblbgd"]);
                    break;


                case "BudgetedCost":
                    this.gvBgdtc.PageSize = Convert.ToInt32(this.ddlpagesizetc.SelectedValue.ToString());
                    this.gvBgdtc.Columns[6].HeaderText = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
                    this.gvBgdtc.DataSource = (DataTable)Session["tblbgd"];
                    this.gvBgdtc.DataBind();
                    break;
                case "BudgetAlocation":
                    this.gvBgdAlc.PageSize = Convert.ToInt32(this.ddlPageAlc.SelectedValue.ToString());
                    this.gvBgdAlc.DataSource = (DataTable)Session["tblbgd"];
                    this.gvBgdAlc.DataBind();
                    break;
                case "BgdAlocBal":
                    this.gvBgdAlcBal.PageSize = Convert.ToInt32(this.ddlBgdPage.SelectedValue.ToString());
                    this.gvBgdAlcBal.DataSource = (DataTable)Session["tblbgd"];
                    this.gvBgdAlcBal.DataBind();
                    this.FooterCalculation((DataTable)Session["tblbgd"]);
                    break;
                case "LandPurReg":

                    this.gvLandPurreg.DataSource = (DataTable)Session["tblbgd"];
                    this.gvLandPurreg.DataBind();

                    break;

            }

        }


        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
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
            }


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MasterBgd":
                    this.PrinMasterBgd();
                    break;

                case "PrjInfo":
                    this.PrintPrjInfo();
                    break;
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


            }
        }

        private void PrinMasterBgd()
        {
            //Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
            Rpt1.SetParameters(new ReportParameter("conarea", "Construction Area: " + this.lblConArea.Text.Trim()));
            //  Rpt1.SetParameters(new ReportParameter("salarea", "Saleable Area: " + this.lblSalArea.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("Group", "Group: " + this.ddlRptGroup.SelectedItem.Text));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Nayan
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //string Comcode = ASTUtility.Left((this.GetComeCode()),1);
            //string concost = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";
            //string salcost = (Comcode == "2") ? "Saleable Cost Per Khata" : "Saleable Cost Per SFT";

            //ReportDocument rptstk = new ReportDocument();

            //if (ASTUtility.Left(comcod, 1) == "1")
            //{
            //    rptstk = new RealERPRPT.R_04_Bgd.RptProjBgdCon();
            //}
            //else
            //{

            //    this.PrintProBudgetResBasis();
            //    return;

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
            //    txtsalarea.Text = "";
            //}
            //TextObject txtgroup = rptstk.ReportDefinition.ReportObjects["txtgroup"] as TextObject;
            //txtgroup.Text ="Group: "+ this.ddlRptGroup.SelectedItem.Text;

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
            //    string eventtype = ((Label) this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";

        }

        private void PrintProBudgetResBasis()
        {

            //Nayan
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



            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    DataTable dt1 = (DataTable)Session["tblprjinf"];
            //    if (dt1.Rows.Count == 0)
            //        return;

            //    ReportDocument rptstk = new RealERPRPT.R_04_Bgd.RptPrjInfo();
            //    //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //    //txtCompany.Text = comnam;
            //    TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //    txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;

            //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = ((Label)this.Master.FindControl ("lblTitle")).Text;
            //        string eventdesc = "Print Report";
            //        string eventdesc2 = "";
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }
            //    rptstk.SetDataSource(dt1);


            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptstk.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptstk;
            //    ((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";     
        }


        private void PrinMasterBgdSp()
        {

            //Nayan
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





        }
        private void PrintWorkvsRes()
        {
            //Nayan
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


            //Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);
            //Session["tblproinfo"] = ds2.Tables[1];

            DataTable dt = (DataTable)Session["tblbgd"];
            DataTable dt1 = (DataTable)Session["tblproinfo"];

            string conarea = dt1.Rows[0]["conarea"].ToString();
            string salarea = dt1.Rows[0]["salarea"].ToString();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BudgTotalCost>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptPrjBudgetedCost", lst, null, null);

            //<asp:Label ID="lblConAreatc" CssClass="lblTxt lblName" runat="server"></asp:Label>
            //                            <asp:Label ID="lblSalAreatc" CssClass=" smLbl_to" runat="server"></asp:Label>

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("conarea", this.lblConAreatc.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("Landsize", "Land Size = " + dt1.Rows[0]["landsize"].ToString() + " " + dt1.Rows[0]["landunit"].ToString()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budgeted Total Cost"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();

            //DataTable dt1 = (DataTable)Session["tblbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //DataTable dt2=(DataTable )Session["tblproinfo"]; 
            //string Comcode = ASTUtility.Left((this.GetComeCode()), 1);
            //string concost = (Comcode == "2") ? "Development Cost Per Khata" : "Construction Cost Per SFT";

            //ReportDocument rptstk = new RealERPRPT.R_04_Bgd.RptPrjBudgetedCost();


            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;

            //TextObject txtstoried = rptstk.ReportDefinition.ReportObjects["txtstoried"] as TextObject;
            //txtstoried.Text = dt2.Rows[0]["basement"].ToString() + "+" + dt2.Rows[0]["stored"].ToString() + " Stored " + dt2.Rows[0]["protype"].ToString()+" Building";

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
            //    string eventtype = ((Label)this.Master.FindControl ("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";



        }
        private void PrintBgdAllc()
        {
            // ** ***Iqbal Nayan    
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
            //string comcod = hst["comcod"].ToString();
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
            //    string eventtype = ((Label)this.Master.FindControl ("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";

        }
        private void PrintWorkvsResVsAlloc()
        {
            // ** ***Iqbal Nayan    
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
            //string comcod = hst["comcod"].ToString();
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
            //txtprojectname.Text ="Project Name: "+ this.ddlProjectName.SelectedItem.Text;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl ("lblTitle")).Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        }

        private void PrintLandPurRegister()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }
        protected void gvBgd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgd.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvBgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgvActDesc");
                Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamt");
                Label lblConsCost = (Label)e.Row.FindControl("lgvconcost");


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
                Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lblConsCost = (Label)e.Row.FindControl("lgvconcostsp");
                //Label lblSalCost = (Label)e.Row.FindControl("lgvsalcostsp");

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
                    //lblSalCost.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");
                }

            }
        }
        protected void ddlpagesizesp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvWrkVsRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWrkVsRes.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesizewrkvres_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }



        protected void ddlpagesizetc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
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
        protected void ddlPageAlc_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }
        protected void ddlBgdPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }
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


    }
}