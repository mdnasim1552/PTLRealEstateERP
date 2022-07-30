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
using RealERPRDLC;
using AjaxControlToolkit;
using System.IO;

namespace RealERPWEB.F_02_Fea
{
    public partial class RptEstProfitloss : System.Web.UI.Page

    {
        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.ProjectName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Product Costing ";
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CompanyInitialize();

            }

        }
        private void CompanyInitialize()
        {

            string comcod = this.GetComCode();

            switch (comcod)
            {
                case "3336":
                case "3337":
                    //this.chkCommercial.Visible = true;
                    break;

                default:
                    break;


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
            Session.Remove("tblpro");
            string comcod = this.GetComCode();
            string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", "GETPROJECTNAME02", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"];

            Session["tblpro"] = ds1.Tables[0];
            ds1.Dispose();




            //string comcod = this.GetComCode();
            //string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            //DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            //this.ddlProjectName.DataTextField = "infdesc";
            //this.ddlProjectName.DataValueField = "infcod";
            //this.ddlProjectName.DataSource = ds1.Tables[0];
            //this.ddlProjectName.DataBind();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                // this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
                // this.ddlProjectName.Visible = false;
                //this.lblProjectdesc.Visible = true;
                this.PanelSelName.Visible = true;
                this.Intialinfo();
                this.GetAgeing();

                this.GetData();
                this.lblsaleprice.Visible = true;
                this.lblsalecore.Visible = true;
                //this.ProjectCDate();
                // this.RadioVisibilitytrue
                return;
            }
            this.lbtnOk.Text = "Ok";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            //this.rbtnList1.SelectedIndex = -1;
            this.gvProjectInfo.DataSource = null;
            this.gvProjectInfo.DataBind();
            this.gvAgeing.DataSource = null;
            this.gvAgeing.DataBind();
            // this.gvFeaPrjC.DataSource = null;
            //this.gvFeaPrjC.DataBind();
            //this.gvFeaLOwner.DataSource = null;
            // this.gvFeaLOwner.DataBind();
            // this.gvFeaPrjRep.DataSource = null;
            // this.gvFeaPrjRep.DataBind();
            this.ddlProjectName.Visible = true;
            this.PanelSelName.Visible = false;

            this.lblsaleprice.Visible = false;
            this.lblsalecore.Visible = false;






        }

        private void GetAgeing()
        {
            Session.Remove("tblagin");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string fdate = this.txtCurDate.Text;
            // string Code = (this.rbtnList1.SelectedIndex == 1) ? "infcod like '51%'" : (this.rbtnList1.SelectedIndex == 2) ? "infcod like '5[2-5]%'" : "infcod like '5[67]%'";
            DataSet ds3 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", "GETAgeingDays", pactcode, fdate, "", "", "", "", "", "", "");
            Session["tblagin"] = ds3.Tables[0];
            this.gvAgeing.DataSource = ds3.Tables[0];
            this.gvAgeing.DataBind();


        }

        private void Intialinfo()
        {

            string comcod = this.GetComCode();
            string prjName = ddlProjectName.SelectedValue.ToString();

            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", "GetIntialdate", prjName,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblintail"] = ds1.Tables[0];



            this.lblUnitName.Text = ds1.Tables[0].Rows[0]["udesc"].ToString();
            this.lblunitsizeval.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0); ");
            this.lblrate.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["rate"]).ToString("#,##0;(#,##0); ");
            this.lblpurdate1.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["purdate"]).ToString("dd-MMM-yyyy");
            this.lblPurValuse1.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["purvalue"]).ToString("#,##0;(#,##0); ");
            this.lblcommitedval.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["commitedval"]).ToString("#,##0;(#,##0); ");
            this.lblactualsal1.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["acsalvalue"]).ToString("#,##0;(#,##0); ");




            //this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            //if (ASTUtility.Left(ds1.Tables[1].Rows[0]["pactcode"].ToString(), 4) == "1102")
            //{
            //    this.uPrj.Visible = true; ;
            //    this.ddlPrjForUse.DataTextField = "upactdesc";
            //    this.ddlPrjForUse.DataValueField = "upactcode";
            //    this.ddlPrjForUse.DataSource = ds1.Tables[1];
            //    this.ddlPrjForUse.DataBind();

            //    //this.Load_Project_To_Combo();
            //    //this.ddlPrjForUse.SelectedValue = ds1.Tables[1].Rows[0]["upactcode"].ToString();
            //}
            //else if (ASTUtility.Left(ds1.Tables[1].Rows[0]["pactcode"].ToString(), 8) == "11020099")
            //{
            //    this.ddlDeptCode.SelectedValue = ds1.Tables[1].Rows[0]["deptcode"].ToString();
            //}
            //else
            //{
            //    this.uPrj.Visible = false; ;
            //}

            //this.ddlFloor.SelectedValue = ds1.Tables[1].Rows[0]["flrcod"].ToString();
            ////this.lblddlProject.Text = (this.ddlProject.Items.Count == 0 ? "XXX" : this.ddlProject.SelectedItem.Text.Trim());
            //this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            //this.lblddlFloor.Text = (this.ddlFloor.Items.Count == 0 ? "YYY" : this.ddlFloor.SelectedItem.Text.Trim());
            //this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["reqbydes"].ToString();
            //this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            //this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            //this.txtExpDeliveryDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["eddat"]).ToString("dd.MM.yyyy");
            //this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
        }

        private void GetData()
        {

            Session.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string fdate = this.txtCurDate.Text;
            // string Code = (this.rbtnList1.SelectedIndex == 1) ? "infcod like '51%'" : (this.rbtnList1.SelectedIndex == 2) ? "infcod like '5[2-5]%'" : "infcod like '5[67]%'";
            DataSet ds3 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", "FEAPRANDPRJCT05", pactcode, fdate, "", "", "", "", "", "", "");
            Session["tblfeaprj"] = ds3.Tables[0];
            this.Data_Bind();
        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {

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

            DataTable dt = (DataTable)Session["tblfeaprj"];
            DataTable dt2 = (DataTable)Session["tblagin"];

            var list = dt.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProfitAndLoss>();
            var list2 = dt2.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.AgeingDays>();

            string unit = lblUnitName.Text.ToString();
            string udesc = lblunitsizeval.Text.ToString();
            string rate = lblrate.Text.ToString();
            string size = lblunitsizeval.Text.ToString();

            string purdate = lblpurdate1.Text.ToString();
            string purvalue = lblPurValuse1.Text.ToString();
            string commitedval = lblcommitedval.Text.ToString();

            string projectName = this.ddlProjectName.SelectedItem.Text.ToString() ?? "";
            string actualsal = this.lblactualsal1.Text.ToString();
            string salprice =this.lblsalecore.Text.ToString();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_02_Fea.rptEstmtProfitLoss", list, list2, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("unit", unit));
            Rpt1.SetParameters(new ReportParameter("udesc", udesc));
            Rpt1.SetParameters(new ReportParameter("rate", rate));
            Rpt1.SetParameters(new ReportParameter("size", size));
            Rpt1.SetParameters(new ReportParameter("purdate", purdate));
            Rpt1.SetParameters(new ReportParameter("purvalue", purvalue));
            Rpt1.SetParameters(new ReportParameter("commitedval", commitedval));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("actualsal", actualsal));


            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Estimated Profit & Loss A/c"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("salprice", salprice));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];

            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {

                //string lblgvItmCode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                //double txtestcost = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim());
                //double txtbuiactual = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuiactual")).Text.Trim());
                //double percnt = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtpercnt")).Text.Trim());
                //double fundamt = Convert.ToDouble("0" + ((Label)this.gvProjectInfo.Rows[i].FindControl("lblsaving")).Text.Trim());
                //double balamt = txtestcost - txtbuiactual;


                string lblgvItmCode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                double txtestcost = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim());
                double txtbuiactual = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuiactual")).Text.Trim());
                double percnt = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtpercnt")).Text.Trim());
                double fundamt = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtcostoffund")).Text.Trim());
                string paymentdate = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvDate")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvDate")).Text.Trim();
                double balamt = txtestcost - txtbuiactual;

                //if (lblgvItmCode == "07001")
                //{
                //    string dd = paymentdate;
                //}
                // double tohour = fixhour + hourly + c1hour + c2hour + c3hour;
                // rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEmpOverTime.PageIndex) + i;
                dt.Rows[i]["estgcod"] = lblgvItmCode;
                dt.Rows[i]["estcost"] = txtestcost;
                dt.Rows[i]["actual"] = txtbuiactual;
                dt.Rows[i]["percnt"] = percnt;
                dt.Rows[i]["fundamt"] = fundamt;
                dt.Rows[i]["balamt"] = balamt;
                dt.Rows[i]["paymentdate"] = paymentdate;


                Session["tblfeaprj"] = dt;

            }
        }


        protected void llbtnTotalproinfo_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];

            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {

                //string lblgvItmCode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                //double txtestcost = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim());
                //double txtbuiactual = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuiactual")).Text.Trim());
                //double percnt = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtpercnt")).Text.Trim());
                //double fundamt = Convert.ToDouble("0" + ((Label)this.gvProjectInfo.Rows[i].FindControl("lblsaving")).Text.Trim());
                //double balamt = txtestcost - txtbuiactual;


                string lblgvItmCode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                double txtestcost = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim());
                double txtbuiactual = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuiactual")).Text.Trim());
                double percnt = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtpercnt")).Text.Trim());
                double fundamt = ASTUtility.StrPosOrNagative(((Label)this.gvProjectInfo.Rows[i].FindControl("lblsaving")).Text.Trim());
                string paymentdate = Convert.ToDateTime(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");

                double balamt = txtestcost - txtbuiactual;

                // double tohour = fixhour + hourly + c1hour + c2hour + c3hour;
                // rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEmpOverTime.PageIndex) + i;
                dt.Rows[i]["estgcod"] = lblgvItmCode;
                dt.Rows[i]["estcost"] = txtestcost;
                dt.Rows[i]["actual"] = txtbuiactual;
                dt.Rows[i]["percnt"] = percnt;
                dt.Rows[i]["fundamt"] = fundamt;
                dt.Rows[i]["balamt"] = balamt;
                dt.Rows[i]["paymentdate"] = paymentdate;
                Session["tblfeaprj"] = dt;

            }          


        }




        protected void llbtnCalculation_Click(object sender, EventArgs e)
        {

            this.SaveValue();

            DataTable dt = ((DataTable)Session["tblfeaprj"]).Copy();
            DataTable dt2 = ((DataTable)Session["tblagin"]).Copy();

            double vality = dt2.Select("grp='A'").Length > 0 ? Convert.ToDouble(dt2.Select("grp='A'")[0]["aginday"]) : 0.00;
            double aging = dt2.Select("grp='B'").Length > 0 ? Convert.ToDouble(dt2.Select("grp='B'")[0]["aginday"]) : 0.00;

            //        double mgc = dt.Select("prgcod='02004'").Length > 0 ? Convert.ToDouble(dt.Select("prgcod='02004'")[0]["prgdesc1"]) * .01 : 0.00;

            //string agingday = dt2.Rows.Count == 0 ? "" : dt2.Rows[0]["aginday"].ToString();



            DataTable dt1 = new DataTable();
            DataView dv1 = new DataView();
            double avgaptsize = 0.00, depshare = 0.00;
            string prgcod = "";
            double fabalcony = 0.00, actual = 0.00, cost1 = 0.00, actual1 = 0.00,
                costofpurest = 0.00, costofpuractual = 0.00, purincest = 0.00,
                purincactual = 0.00, saleincentest = 0.00, saleincentactula = 0.00, adminohest = 0.00,
                adminohactual = 0.00, texpendamtest = 0.00, texpendamtactual = 0.00,
                mktexpest = 0.00, mktexpactual = 0.00, otherest = 0.00, otheractual = 0.00,
                 costpest = 0.00, costpactual = 0.00,
                  riskest = 0.00, riskactual = 0.00, totherest = 0.00, totheractual = 0.00,
                  totalcostest = 0.00, totalcostactual = 0.00, mktexpenforest = 0.00, mktexpenforactaul = 0.00,
                  mktexpenmonest = 0.00, mktexpenmonactaul = 0.00, tmktexpenmonest = 0.00, tmktexpenmonactaul = 0.00
                  , costoffundest = 0.00, costoffundactual = 0.00, purvalue = 0.00, comitedval = 0.00, taginday = 0.00,
                  lossest = 0.00, lossactual = 0.00, comitedval2 = 0.00, lossest2 = 0.00,
            costest2 = 0.00, costoffundest5 = 0.00, costoffundest2 = 0.00,
            costacutal2 = 0.00, costoffundactual5 = 0.00, costoffundactual2 = 0.00, 
            
            costoffundest6 = 0.00, costoffundactual6 = 0.00, actualsalvalue=0.00,
              purincenspcialest = 0.00 , purincenspcialactual = 0.00 , totalpurinspecialest =0.00, totalpurinspecialactual = 0.00,
               grpcotributionest = 0.00, grpcotributionactual = 0.00, spsalincest = 0.00, spsalincacutal = 0.00, 
               totalovcostest = 0.00, totalovcostactual = 0.00, fixtsalinvest=0.00, fixtsalinvactual = 0.00;
        

            TimeSpan tday;
            double NrOfDays = 0.00;
            double cost = 0.00;
            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                double txtestcost = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim());
                double txtactual = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuiactual")).Text.Trim());
                double percnt = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtpercnt")).Text.Trim());
                double costoffund = ASTUtility.StrPosOrNagative(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtcostoffund")).Text.Trim());
                //string paymentdate = Convert.ToDateTime(((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string paymentdate = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvDate")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvDate")).Text.Trim();


                //double txtestcost = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim());

                //double txtactual = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuiactual")).Text.Trim());
                //double percnt = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtpercnt")).Text.Trim());
                //double costoffund = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtcostoffund")).Text.Trim());

                //string gtype = dt.Rows[i]["prgval"].ToString();
                string buildarea = "";

                switch (Gcode)
                {
                    case "02001":
                    case "02002":
                    case "02003":
                        cost = cost + txtestcost;
                        actual = actual + txtactual;

                        cost1 = cost + txtestcost;
                        actual1 = actual + txtactual;



                        dt.Select("estgcod='02000'")[0]["estcost"] = cost;//02005 change
                        dt.Select("estgcod='02000'")[0]["actual"] = actual;
                        dt.Select("estgcod='02000'")[0]["balamt"] = cost - actual;

                        break;

                    //case "02006":
                    //    purincest = cost * percnt * .01;
                    //    purincactual = actual * percnt * .01;

                    //    //purincest = cost + txtestcost;
                    //    //purincactual = actual + txtactual;

                    //    dt.Select("estgcod='02006'")[0]["estcost"] = purincest;
                    //    dt.Select("estgcod='02006'")[0]["actual"] = purincactual;
                    //    dt.Select("estgcod='02006'")[0]["balamt"] = purincest - purincactual;

                    //    break;

                    case "02006":
                    case "02008":

                        //double purincest1 = 0.00, purincactual1 = 0.00;

                         if(Gcode == "02006")
                        {
                            purincest = cost * percnt * .01;
                            purincactual =  actual * percnt * .01;
                            

                            //purincest = cost + txtestcost;
                            //purincactual = actual + txtactual;

                            dt.Select("estgcod='02006'")[0]["estcost"] = purincest;
                            dt.Select("estgcod='02006'")[0]["actual"] = purincactual;
                            dt.Select("estgcod='02006'")[0]["balamt"] = purincest - purincactual;

                        }

                         
                      if(Gcode == "02008")
                        {
                            purincenspcialest = txtestcost;
                            purincenspcialactual = txtactual;
                            dt.Select("estgcod='02008'")[0]["estcost"] = purincenspcialest;
                            dt.Select("estgcod='02008'")[0]["actual"] = purincenspcialactual;
                            dt.Select("estgcod='02008'")[0]["balamt"] = purincenspcialest - purincenspcialest;

                        }


                    


                        totalpurinspecialest = purincest + purincenspcialest;
                        totalpurinspecialactual = purincactual + purincenspcialactual;

                        dt.Select("estgcod='02005'")[0]["estcost"] = totalpurinspecialest;
                        dt.Select("estgcod='02005'")[0]["actual"] = totalpurinspecialactual;
                        dt.Select("estgcod='02005'")[0]["balamt"] = totalpurinspecialest - totalpurinspecialactual;

                        break;

                    //case "02005": // Total Part incentiva+ special incentive

                    //    totalpurinspecialest = purincest + purincenspcialest;
                    //    totalpurinspecialactual = purincactual + purincenspcialactual;

                    //    dt.Select("estgcod='02005'")[0]["estcost"] = totalpurinspecialest;
                    //    dt.Select("estgcod='02005'")[0]["actual"] = totalpurinspecialactual;
                    //    dt.Select("estgcod='02005'")[0]["balamt"] = totalpurinspecialest - totalpurinspecialactual;


                    //    break;


                    case "03000":
                        costofpurest = cost + purincest + purincenspcialest;
                        costofpuractual = actual + purincactual + purincenspcialactual;
                        //costofpurest =purincest + totalpurinspecialest;
                        //costofpuractual =purincactual + totalpurinspecialactual;


                        dt.Select("estgcod='03000'")[0]["estcost"] = costofpurest;
                        dt.Select("estgcod='03000'")[0]["actual"] = costofpuractual;
                        dt.Select("estgcod='03000'")[0]["balamt"] = costofpurest - costofpuractual;

                        break;

                    //  case marge 04001,04002,04003,04004


                    case "04001":
                    case "04002":
                    case "04003":
                    case "04004":



                        if (Gcode == "04001")
                        {
                            //saleincentest = 0.00, saleincentactula = 0.00
                            comitedval = Convert.ToDouble("0" + (lblcommitedval.Text));
                            actualsalvalue = Convert.ToDouble("0" + (lblactualsal1.Text));
                            saleincentest = comitedval * percnt * .01;
                            saleincentactula = actualsalvalue * percnt * .01;
                            //costofpuractual = actual + purincactual;


                            dt.Select("estgcod='04001'")[0]["estcost"] = saleincentest;
                            dt.Select("estgcod='04001'")[0]["actual"] = saleincentactula;
                            dt.Select("estgcod='04001'")[0]["balamt"] = saleincentest - saleincentactula;

                        }

                        else if (Gcode == "04002")
                        {

                            spsalincest = txtestcost;
                            spsalincacutal = txtactual;
                            dt.Select("estgcod='04002'")[0]["estcost"] = spsalincest;
                            dt.Select("estgcod='04002'")[0]["actual"] = spsalincacutal;
                            dt.Select("estgcod='04002'")[0]["balamt"] = spsalincest - spsalincacutal;


                        }

                        else if (Gcode == "04003")
                        {
                            comitedval = Convert.ToDouble("0" + (lblcommitedval.Text));
                            actualsalvalue = Convert.ToDouble("0" + (lblactualsal1.Text));

                            //adminohest = 500000;
                            adminohest = comitedval * percnt * .01;
                            adminohactual = actualsalvalue * percnt * .01;
                            //costofpuractual = actual + purincactual;


                            dt.Select("estgcod='04003'")[0]["estcost"] = adminohest;
                            dt.Select("estgcod='04003'")[0]["actual"] = adminohactual;
                            dt.Select("estgcod='04003'")[0]["balamt"] = adminohest - adminohactual;


                        


                        }


                        else if(Gcode == "04004")
                        {
                            grpcotributionest = txtestcost;
                            grpcotributionactual = txtactual;
                            dt.Select("estgcod='04002'")[0]["estcost"] = grpcotributionest;
                            dt.Select("estgcod='04002'")[0]["actual"] = grpcotributionactual;
                            dt.Select("estgcod='04002'")[0]["balamt"] = grpcotributionest - grpcotributionactual;

                        }


                        totalovcostest = saleincentest + spsalincest + adminohest + grpcotributionest;
                        totalovcostactual = saleincentactula + spsalincacutal + adminohactual + grpcotributionest;


                        dt.Select("estgcod='04000'")[0]["estcost"] = totalovcostest;
                        dt.Select("estgcod='04000'")[0]["actual"] = totalovcostactual;
                        dt.Select("estgcod='04000'")[0]["balamt"] = totalovcostest - totalovcostactual;

                        break;


                    //case "04002":

                    //    //04002
                    //    spsalincest = txtestcost; 
                    //    spsalincacutal = txtactual;
                    //    dt.Select("estgcod='04002'")[0]["estcost"] = spsalincest;
                    //    dt.Select("estgcod='04002'")[0]["actual"] = spsalincacutal;
                    //    dt.Select("estgcod='04002'")[0]["balamt"] = spsalincest - spsalincacutal;

                    //    break;



                    //case "04003": //change 04002
                    //    //04003
                    //    comitedval = Convert.ToDouble("0" + (lblcommitedval.Text));
                    //    actualsalvalue = Convert.ToDouble("0" + (lblactualsal1.Text));

                    //    //adminohest = 500000;
                    //    adminohest = comitedval * percnt * .01;
                    //    adminohactual = actualsalvalue * percnt * .01;
                    //    //costofpuractual = actual + purincactual;


                    //    dt.Select("estgcod='04003'")[0]["estcost"] = adminohest;
                    //    dt.Select("estgcod='04003'")[0]["actual"] = adminohactual;
                    //    dt.Select("estgcod='04003'")[0]["balamt"] = adminohest - adminohactual;

                    //    break;


                    //case "04004":

                    //    //04004
                    //    grpcotributionest = txtestcost;
                    //    grpcotributionactual = txtactual;
                    //    dt.Select("estgcod='04002'")[0]["estcost"] = grpcotributionest;
                    //    dt.Select("estgcod='04002'")[0]["actual"] = grpcotributionactual;
                    //    dt.Select("estgcod='04002'")[0]["balamt"] = grpcotributionest - grpcotributionactual;

                    //    break;


                    //case "04000":

                       
                    //    totalovcostest = saleincentest+ spsalincest + adminohest+ grpcotributionest;
                    //    totalovcostactual = saleincentactula+ spsalincacutal+adminohactual + grpcotributionest;
                       
                        
                    //    dt.Select("estgcod='04000'")[0]["estcost"] = totalovcostest;
                    //    dt.Select("estgcod='04000'")[0]["actual"] = totalovcostactual;
                    //    dt.Select("estgcod='04000'")[0]["balamt"] = totalovcostest - totalovcostactual;

                    //    break;




                    case "04005":

                        texpendamtest = costofpurest + totalovcostest;
                        texpendamtactual = costofpuractual + totalovcostactual;

                        //texpendamtest = costofpurest + saleincentest + adminohest;
                        //texpendamtactual = costofpuractual + saleincentactula + adminohactual;
                        //costofpuractual = actual + purincactual;
                        dt.Select("estgcod='04005'")[0]["estcost"] = texpendamtest;
                        dt.Select("estgcod='04005'")[0]["actual"] = texpendamtactual;
                        dt.Select("estgcod='04005'")[0]["balamt"] = texpendamtest - texpendamtactual;

                        break;

                    case "05001":
                    case "05002":
                    case "05003":
                    case "05004":


                        if (Gcode == "05001")
                        {
                            mktexpest = txtestcost;
                            mktexpactual = txtactual;
                            dt.Select("estgcod='05001'")[0]["estcost"] = mktexpest;
                            dt.Select("estgcod='05001'")[0]["actual"] = mktexpactual;
                            dt.Select("estgcod='05001'")[0]["balamt"] = mktexpest - mktexpactual;


                        }

                        else if (Gcode == "05002")
                        {
                            comitedval = Convert.ToDouble("0" + (lblcommitedval.Text));
                            actualsalvalue = Convert.ToDouble("0" + (lblactualsal1.Text));

                            //otherest = 500000;
                            otherest = comitedval * percnt * .01;
                            otheractual = actualsalvalue * percnt * .01;

                            dt.Select("estgcod='05002'")[0]["estcost"] = otherest;
                            dt.Select("estgcod='05002'")[0]["actual"] = otheractual;
                            dt.Select("estgcod='05002'")[0]["balamt"] = otherest - otheractual;

                        }

                        else if (Gcode == "05003")
                        {
                            costpest = txtestcost;
                            costpactual = txtactual;
                            dt.Select("estgcod='05003'")[0]["estcost"] = costpest;
                            dt.Select("estgcod='05003'")[0]["actual"] = costpactual;
                            dt.Select("estgcod='05003'")[0]["balamt"] = costpest - costpactual;

                        }

                        else if (Gcode == "05004")
                        {
                            comitedval = Convert.ToDouble("0" + (lblcommitedval.Text));
                            actualsalvalue = Convert.ToDouble("0" + (lblactualsal1.Text));

                            riskest = comitedval * percnt * .01;
                            riskactual = actualsalvalue * percnt * .01;

                            dt.Select("estgcod='05004'")[0]["estcost"] = riskest;
                            dt.Select("estgcod='05004'")[0]["actual"] = riskactual;
                            dt.Select("estgcod='05004'")[0]["balamt"] = riskest - riskactual;

                        }

                        totherest = mktexpest + otherest + costpest + riskest;
                        totheractual = mktexpactual + otheractual + costpactual + riskactual;
                        dt.Select("estgcod='05000'")[0]["estcost"] = totherest;
                        dt.Select("estgcod='05000'")[0]["actual"] = totheractual;
                        dt.Select("estgcod='05000'")[0]["balamt"] = totherest - totheractual;
                       
                        break;


                   

                    //case "05002":

                    //    comitedval = Convert.ToDouble("0" + (lblcommitedval.Text));
                    //    actualsalvalue = Convert.ToDouble("0" + (lblactualsal1.Text));

                    //    //otherest = 500000;
                    //    otherest = comitedval * percnt * .01;
                    //    otheractual = actualsalvalue * percnt * .01;

                    //    dt.Select("estgcod='05002'")[0]["estcost"] = otherest;
                    //    dt.Select("estgcod='05002'")[0]["actual"] = otheractual;
                    //    dt.Select("estgcod='05002'")[0]["balamt"] = otherest - otheractual;

                    //    break;
                    //case "05003":
                    //    costpest = txtestcost;
                    //    costpactual = txtactual;


                    //    dt.Select("estgcod='05003'")[0]["estcost"] = costpest;
                    //    dt.Select("estgcod='05003'")[0]["actual"] = costpactual;
                    //    dt.Select("estgcod='05003'")[0]["balamt"] = costpest - costpactual;

                    //    break;

                    //case "05004":

                    //    comitedval = Convert.ToDouble("0" + (lblcommitedval.Text));
                    //    actualsalvalue = Convert.ToDouble("0" + (lblactualsal1.Text));
                  
                    //    riskest = comitedval * percnt * .01;
                    //    riskactual = actualsalvalue * percnt * .01;

                    //    dt.Select("estgcod='05004'")[0]["estcost"] = riskest;
                    //    dt.Select("estgcod='05004'")[0]["actual"] = riskactual;
                    //    dt.Select("estgcod='05004'")[0]["balamt"] = riskest - riskactual;

                    //    break;

                    //case "05006":                     
                    //    totherest = mktexpest + otherest + costpest + riskest;
                    //    totheractual = mktexpactual + otheractual + costpactual + riskactual;
                    //    dt.Select("estgcod='05006'")[0]["estcost"] = totherest;
                    //    dt.Select("estgcod='05006'")[0]["actual"] = totheractual;
                    //    dt.Select("estgcod='05006'")[0]["balamt"] = totherest - totheractual;

                    //    break;

                    //case "05000": // change 05006
                    //    totherest = mktexpest + otherest + costpest + riskest;
                    //    totheractual = mktexpactual + otheractual + costpactual + riskactual;
                    //    dt.Select("estgcod='05000'")[0]["estcost"] = totherest;
                    //    dt.Select("estgcod='05000'")[0]["actual"] = totheractual;
                    //    dt.Select("estgcod='05000'")[0]["balamt"] = totherest - totheractual;
                    //    break;

                    case "05008":

                        totalcostest = totherest + texpendamtest;
                        totalcostactual = totheractual + texpendamtactual;
                        dt.Select("estgcod='05008'")[0]["estcost"] = totalcostest;
                        dt.Select("estgcod='05008'")[0]["actual"] = totalcostactual;
                        dt.Select("estgcod='05008'")[0]["balamt"] = totalcostest - totalcostactual;

                        break;

                    
                    case "06001":
                    case "06002":


                        if (Gcode == "06001")
                        {

                            mktexpenforest = 500000 * percnt * .01;
                            mktexpenforest = 0.00;


                            dt.Select("estgcod='06001'")[0]["estcost"] = mktexpenforest;
                            dt.Select("estgcod='06001'")[0]["actual"] = mktexpenforest;
                            dt.Select("estgcod='06001'")[0]["balamt"] = mktexpenforest - mktexpenforest;

                        }

                        else if(Gcode == "06002")
                        {

                            purvalue = Convert.ToDouble("0" + (lblPurValuse1.Text));

                            // taginday= Convert.ToDouble("0" + (agingday));
                            mktexpenmonest = (purvalue * percnt * .01 * vality) / (30);
                            mktexpenmonactaul = (purvalue * percnt * .01 * aging) / (30);

                            dt.Select("estgcod='06002'")[0]["estcost"] = mktexpenmonest;
                            dt.Select("estgcod='06002'")[0]["actual"] = mktexpenmonactaul;
                            dt.Select("estgcod='06002'")[0]["balamt"] = mktexpenmonest - mktexpenmonactaul;


                        }

                        tmktexpenmonest = mktexpenforest + mktexpenmonest;
                        tmktexpenmonactaul = mktexpenforest + mktexpenmonactaul;

                        dt.Select("estgcod='06000'")[0]["estcost"] = tmktexpenmonest;
                        dt.Select("estgcod='06000'")[0]["actual"] = tmktexpenmonactaul;
                        dt.Select("estgcod='06000'")[0]["balamt"] = tmktexpenmonest - tmktexpenmonactaul;




                        break;

                    //case "06002":

                        
                    //    purvalue = Convert.ToDouble("0" + (lblPurValuse1.Text));

                    //    // taginday= Convert.ToDouble("0" + (agingday));
                    //    mktexpenmonest = (purvalue * percnt * .01 * vality) / (30);
                    //    mktexpenmonactaul = (purvalue * percnt * .01 * aging) / (30);

                    //    dt.Select("estgcod='06002'")[0]["estcost"] = mktexpenmonest;
                    //    dt.Select("estgcod='06002'")[0]["actual"] = mktexpenmonactaul;
                    //    dt.Select("estgcod='06002'")[0]["balamt"] = mktexpenmonest - mktexpenmonactaul;

                    //    break;

                    //case "06004":
                    //    tmktexpenmonest = mktexpenforest + mktexpenmonest;
                    //    tmktexpenmonactaul = mktexpenforest + mktexpenmonactaul;

                    //    dt.Select("estgcod='06004'")[0]["estcost"] = tmktexpenmonest;
                    //    dt.Select("estgcod='06004'")[0]["actual"] = tmktexpenmonactaul;
                    //    dt.Select("estgcod='06004'")[0]["balamt"] = tmktexpenmonest - tmktexpenmonactaul;

                    //    break;


                    //case "06000": //change 06004
                    //    tmktexpenmonest = mktexpenforest + mktexpenmonest;
                    //    tmktexpenmonactaul = mktexpenforest + mktexpenmonactaul;

                    //    dt.Select("estgcod='06000'")[0]["estcost"] = tmktexpenmonest;
                    //    dt.Select("estgcod='06000'")[0]["actual"] = tmktexpenmonactaul;
                    //    dt.Select("estgcod='06000'")[0]["balamt"] = tmktexpenmonest - tmktexpenmonactaul;

                    //    break;

                        //auto calculate


                    case "07001":
                    case "07002":
                    case "07003":


                        if (Gcode == "07001")
                        {
                            DateTime datefrm = Convert.ToDateTime(this.txtCurDate.Text.Trim());
                            DateTime dateto = Convert.ToDateTime(paymentdate.Trim());
                            //DateTime p1;

                            //DateTime dateTime10 = Convert.ToDateTime(txtCurDate);
                            //DateTime dateTime12 = Convert.ToDateTime(paymentdate);

                            //d1 = Convert.ToDateTime (txtCurDate.Text);
                            //p1=Convert.dat
                            //d2 Convert.ToDateTime(paymentdate) ;

                            tday = datefrm - dateto;
                            NrOfDays = tday.TotalDays;


                            costoffundest = (costoffund * percnt * .01 * vality) / (360);
                            costoffundactual = (costoffund * percnt * .01 * NrOfDays) / (360); //current date

                            dt.Select("estgcod='07001'")[0]["fundamt"] = costoffund;
                            dt.Select("estgcod='07001'")[0]["estcost"] = costoffundest;
                            dt.Select("estgcod='07001'")[0]["actual"] = costoffundactual;
                            dt.Select("estgcod='07001'")[0]["balamt"] = costoffundest - costoffundactual;

                        }

                          else if(Gcode == "07002")
                        {
                            DateTime datefrm1 = Convert.ToDateTime(this.txtCurDate.Text.Trim());
                            DateTime dateto2 = Convert.ToDateTime(paymentdate.Trim());

                            tday = datefrm1 - dateto2;
                            NrOfDays = tday.TotalDays;


                            costoffundest5 = (costoffund * percnt * .01 * vality) / (360);
                            costoffundactual5 = (costoffund * percnt * .01 * NrOfDays) / (360); //current date

                            dt.Select("estgcod='07002'")[0]["fundamt"] = costoffund;
                            dt.Select("estgcod='07002'")[0]["estcost"] = costoffundest5;
                            dt.Select("estgcod='07002'")[0]["actual"] = costoffundactual5;
                            dt.Select("estgcod='07002'")[0]["balamt"] = costoffundest5 - costoffundactual5;


                        }

                        else if (Gcode == "07003")
                        {

                            DateTime datefrm3 = Convert.ToDateTime(this.txtCurDate.Text.Trim());
                            DateTime dateto4 = Convert.ToDateTime(paymentdate.Trim());

                            tday = datefrm3 - dateto4;
                            NrOfDays = tday.TotalDays;


                            costoffundest6 = (costoffund * percnt * .01 * vality) / (360);
                            costoffundactual6 = (costoffund * percnt * .01 * NrOfDays) / (360); //current date

                            dt.Select("estgcod='07003'")[0]["fundamt"] = costoffund;
                            dt.Select("estgcod='07003'")[0]["estcost"] = costoffundest6;
                            dt.Select("estgcod='07003'")[0]["actual"] = costoffundactual6;
                            dt.Select("estgcod='07003'")[0]["balamt"] = costoffundest6 - costoffundactual6;

                        }

                        costest2 = costoffundest + costoffundest5 + costoffundest6;
                        costacutal2 = costoffundactual + costoffundactual5 + costoffundactual6;


                        dt.Select("estgcod='07000'")[0]["fundamt"] = 0.00;
                        dt.Select("estgcod='07000'")[0]["estcost"] = costest2;
                        dt.Select("estgcod='07000'")[0]["actual"] = costacutal2;
                        dt.Select("estgcod='07000'")[0]["balamt"] = costest2 - costacutal2;

                        break;


                    //case "07002":

                    //    DateTime datefrm1 = Convert.ToDateTime(this.txtCurDate.Text.Trim());
                    //    DateTime dateto2 = Convert.ToDateTime(paymentdate.Trim());

                    //    tday = datefrm1 - dateto2;
                    //    NrOfDays = tday.TotalDays;


                    //    costoffundest5 = (costoffund * percnt * .01 * vality) / (360);
                    //    costoffundactual5 = (costoffund * percnt * .01 * NrOfDays) / (360); //current date

                    //    dt.Select("estgcod='07002'")[0]["fundamt"] = costoffund;
                    //    dt.Select("estgcod='07002'")[0]["estcost"] = costoffundest5;
                    //    dt.Select("estgcod='07002'")[0]["actual"] = costoffundactual5;
                    //    dt.Select("estgcod='07002'")[0]["balamt"] = costoffundest5 - costoffundactual5;

                    //    break;
                    //case "07003":


                    //    DateTime datefrm3 = Convert.ToDateTime(this.txtCurDate.Text.Trim());
                    //    DateTime dateto4 = Convert.ToDateTime(paymentdate.Trim());

                    //    tday = datefrm3 - dateto4;
                    //    NrOfDays = tday.TotalDays;


                    //    costoffundest6 = (costoffund * percnt * .01 * vality) / (360);
                    //    costoffundactual6 = (costoffund * percnt * .01 * NrOfDays) / (360); //current date

                    //    dt.Select("estgcod='07003'")[0]["fundamt"] = costoffund;
                    //    dt.Select("estgcod='07003'")[0]["estcost"] = costoffundest6;
                    //    dt.Select("estgcod='07003'")[0]["actual"] = costoffundactual6;
                    //    dt.Select("estgcod='07003'")[0]["balamt"] = costoffundest6 - costoffundactual6;

                    //    break;


                    //case "07004":

                    //    costest2 = costoffundest + costoffundest5 + costoffundest6;
                    //    costacutal2 = costoffundactual + costoffundactual5 + costoffundactual6;


                    //    dt.Select("estgcod='07004'")[0]["fundamt"] = 0.00;
                    //    dt.Select("estgcod='07004'")[0]["estcost"] = costest2;
                    //    dt.Select("estgcod='07004'")[0]["actual"] = costacutal2;
                    //    dt.Select("estgcod='07004'")[0]["balamt"] = costest2 - costacutal2;

                    //    break;

                    case "08000":

                        comitedval2 = Convert.ToDouble("0" + (lblcommitedval.Text));
                        double comitedvalactual = Convert.ToDouble("0" + (lblactualsal1.Text));



                        //costoffundest = (costoffund * percnt * .01 * vality) / (360);
                        //costoffundactual = (costoffund * percnt * .01 * NrOfDays) / (360); //current date

                        // dt.Select("estgcod='08000'")[0]["fundamt"] = comitedval;
                        dt.Select("estgcod='08000'")[0]["estcost"] = comitedval2;
                        dt.Select("estgcod='08000'")[0]["actual"] = comitedvalactual;
                        dt.Select("estgcod='08000'")[0]["balamt"] = comitedval2 - comitedvalactual;

                        break;

                    case "08001":

                        dt.Select("estgcod='08001'")[0]["estcost"] = totalcostest;
                        dt.Select("estgcod='08001'")[0]["actual"] = totalcostactual;
                        dt.Select("estgcod='08001'")[0]["balamt"] = totalcostest- totalcostactual;

                        break;

                    //case "09000":
                    //    lossest2 = Convert.ToDouble("0" + (lblcommitedval.Text));
                    //    double lossest6 = Convert.ToDouble("0" + (lblactualsal1.Text));
                    //    // lossactual = Convert.ToDouble("0" + (lblcommitedval.Text));
                    //    lossest = lossest2 - totalcostest;
                    //    lossactual = lossest6 - totalcostactual;
                    //    dt.Select("estgcod='09000'")[0]["estcost"] = lossest;
                    //    dt.Select("estgcod='09000'")[0]["actual"] = lossactual;
                    //    dt.Select("estgcod='09000'")[0]["balamt"] = lossactual == 0 ? 0 : lossactual * 100 / lossest6;

                    //    dt.Select("estgcod='09000'")[0]["percnt"] = lossest == 0 ? 0 : lossest * 100 / lossest2;

                    case "09000":
                        lossest2 = Convert.ToDouble("0" + (lblcommitedval.Text));
                        double lossest6 = Convert.ToDouble("0" + (lblactualsal1.Text));
                        // lossactual = Convert.ToDouble("0" + (lblcommitedval.Text));
                        lossest = lossest2 - totalcostest;
                        lossactual = lossest6 - totalcostactual;
                        dt.Select("estgcod='09000'")[0]["estcost"] = lossest;
                        dt.Select("estgcod='09000'")[0]["actual"] = lossactual;
                        dt.Select("estgcod='09000'")[0]["balamt"] = lossest- lossactual;

                      //  dt.Select("estgcod='09000'")[0]["percnt"] = lossest == 0 ? 0 : lossest * 100 / lossest2;

                        // dt.Select("estgcod='09000'")[0]["balamt"] = 0.00;


                        break;





                    default:

                        break;
                }

            }

            //Session["tblprogeninfo"] = dt;
            Session["tblfeaprj"] = dt;

            this.Data_Bind();

            //this.gvProjectInfo.DataSource = dt;
            //this.gvProjectInfo.DataBind();




        }
        protected void lUpdatProInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tblfeaprj"];
            string comcod = this.GetComCode();
            string prjcode = this.ddlProjectName.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string gcod = dt.Rows[i]["estgcod"].ToString();
                string estcost = dt.Rows[i]["estcost"].ToString();
                string actual = dt.Rows[i]["actual"].ToString();
                string fundamt = dt.Rows[i]["fundamt"].ToString();
                string percnt = dt.Rows[i]["percnt"].ToString();
                string paymentdate = dt.Rows[i]["paymentdate"].ToString();

                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", "INSORUPDATEPRODUCTCOSTING",
                    prjcode, gcod, estcost, actual, percnt, fundamt, paymentdate, "", "", "", "", "", "", "", "");


                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    return;
                }
            }


            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

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



        protected void lbtnFUpdateSales_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            // this.UpdateProjectSaleAndCost();
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblfeaprj"];


            this.gvProjectInfo.DataSource = dt;
            this.gvProjectInfo.DataBind();

            double salpriceest = dt.Select("estgcod='05008'").Length > 0 ? Convert.ToDouble(dt.Select("estgcod='05008'")[0]["estcost"]) : 0.00;
            double salpriceactual = dt.Select("estgcod='05008'").Length > 0 ? Convert.ToDouble(dt.Select("estgcod='05008'")[0]["actual"]) : 0.00;

            this.lblsalecore.Text = Convert.ToDouble(salpriceest / 10000000).ToString("#,##0.00;(#,##0.00); ") + " Crore";
            this.lblsalecoreactual.Text = Convert.ToDouble(salpriceactual / 10000000).ToString("#,##0.00;(#,##0.00); ") + " Crore";



            this.FooterCal();



            //this.FooterCal();




        }

        private void FooterCal()
        {
            Session["Report1"] = gvProjectInfo;
            //((HyperLink)this.gvProjectInfo.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCELNEW";
            ((HyperLink)this.gvProjectInfo.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RDLCViewer.aspx?PrintOpt=GRIDTOEXCELNEW";




        }

        protected void gvProjectInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvItmCode = (Label)e.Row.FindControl("lblgvItmCode");
                Label lgcResDesc1 = (Label)e.Row.FindControl("lgcResDesc1");
                TextBox txtpercnt = (TextBox)e.Row.FindControl("txtpercnt");
                TextBox txtcostoffund = (TextBox)e.Row.FindControl("txtcostoffund");

                TextBox txtestcost = (TextBox)e.Row.FindControl("txtestcost");
                TextBox txtbuiactual = (TextBox)e.Row.FindControl("txtbuiactual");
                Label lblsaving = (Label)e.Row.FindControl("lblsaving");
                TextBox txtgvDate = (TextBox)e.Row.FindControl("txtgvDate");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "estgcod")).ToString();

                if (code == "")
                {
                    return;
                }


                if (code == "02000" || code == "02005" || code == "03000" || code == "04000" || code == "04005" || code == "05000" ||
                    code == "05006" || code == "05008" || code == "06000" || code == "06004" || code == "07000" || code == "08000"  || code == "07004"||code=="09000")
                {

                    txtpercnt.Enabled = false;
                    txtcostoffund.Enabled = false;
                    txtestcost.Enabled = false;
                    txtbuiactual.Enabled = false;
                    txtpercnt.Enabled = false;

                    txtpercnt.Font.Bold = true;
                    txtcostoffund.Font.Bold = true;
                    txtestcost.Font.Bold = true;
                    txtbuiactual.Font.Bold = true;
                    lgcResDesc1.Font.Bold = true;
                    lblsaving.Font.Bold = true;

                    txtpercnt.Style["font-size"] = "12px";
                    txtcostoffund.Style["font-size"] = "12px";
                    txtestcost.Style["font-size"] = "12px";
                    txtbuiactual.Style["font-size"] = "12px";
                    lgcResDesc1.Style["font-size"] = "12px";
                    lblsaving.Style["font-size"] = "12px";



                    //  e.Row.BackColor = System.Drawing.Color.Orange;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgcResDesc1.Attributes["style"] = "font-weight:bold; color:maroon;";
                    txtpercnt.Attributes["style"] = "font-weight:bold; color:maroon;";
                    txtcostoffund.Attributes["style"] = "font-weight:bold; color:maroon;";
                    txtestcost.Attributes["style"] = "font-weight:bold; color:maroon;";
                    txtbuiactual.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblsaving.Attributes["style"] = "font-weight:bold; color:maroon; ";

                    //lgvNagad.Style.Add("text-align", "left");
                    //lgvNetPayment.Style.Add("text-align", "right");





                    //e.Row.BackColor = System.Drawing.Color.Orange;
                    ////e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    //lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                    //lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                    //lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    //lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                    //lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon; ";

                    //lgvNagad.Style.Add("text-align", "left");
                    //lgvNetPayment.Style.Add("text-align", "right");

                }

               

                else if (code == "02001" || code == "02002" || code == "02003" || code == "05001" || code == "05003" || code == "08001")
                {
                    txtpercnt.Enabled = false;
                    //txtcostoffund.Enabled = false;
                    //txtestcost.Enabled = false;
                    //txtbuiactual.Enabled = false;

                    //e.Row.BackColor = System.Drawing.Color.Orange;
                    ////e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    //lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                    //lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                    //lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    //lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                    //lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                    //lgvNagad.Style.Add("text-align", "left");
                    //lgvNetPayment.Style.Add("text-align", "right");


                }

                else if (code == "07001" || code == "07002" || code == "07003")
                {
                    txtgvDate.Enabled = true;

                }




            }


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}























