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

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                this.ProjectName();
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT FEASIBILITY";

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
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.PanelSelName.Visible = true;

                this.GetData();
                //this.ProjectCDate();
                // this.RadioVisibility();
                return;
            }
            this.lbtnOk.Text = "Ok";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            //this.rbtnList1.SelectedIndex = -1;
            this.gvProjectInfo.DataSource = null;
            this.gvProjectInfo.DataBind();
            // this.gvFeaPrj.DataSource = null;
            //this.gvFeaPrj.DataBind();
            // this.gvFeaPrjC.DataSource = null;
            //this.gvFeaPrjC.DataBind();
            //this.gvFeaLOwner.DataSource = null;
            // this.gvFeaLOwner.DataBind();
            // this.gvFeaPrjRep.DataSource = null;
            // this.gvFeaPrjRep.DataBind();
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.PanelSelName.Visible = false;

            //  this.ImagePanel.Visible = false;





        }

        private void GetData()
        {

            Session.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            // string Code = (this.rbtnList1.SelectedIndex == 1) ? "infcod like '51%'" : (this.rbtnList1.SelectedIndex == 2) ? "infcod like '5[2-5]%'" : "infcod like '5[67]%'";
            DataSet ds3 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", "FEAPRANDPRJCT05", pactcode, "", "", "", "", "", "", "", "");
            Session["tblfeaprj"] = ds3.Tables[0];
            this.Data_Bind();
        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            switch (comcod)
            {

                case "3336":
                case "3337":
                    this.PrintFeasibilitySuvastu();
                    break;

                default:
                    //this.PrintFeasibilitygen();
                    this.PrintFeasibilitySuvastu();
                    break;

            }



        }

        private void PrintFeasibilitygen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            ReportDocument rpcp = new RealERPRPT.R_02_Fea.RptFeaProject();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;

            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rpcp.SetDataSource((DataTable)Session["tblfeaprj"]);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private string GetCompanyCallType()
        {

            string comcod = this.GetComCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3336"://P2P Construction
                case "3337"://Wecon Properties

                    Calltype = "RPTPROJECTFEASIBILITYSUVASTU";
                    break;


                default:
                    Calltype = "RPTPROJECTFEASIBILITY";
                    break;



            }

            return Calltype;



        }
        private void PrintFeasibilitySuvastu()
        {
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string CallType = this.GetCompanyCallType();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITYSUVASTU", pactcode, "", "", "", "", "", "", "", "");

            DataTable dtr = ds2.Tables[0].Copy();
            DataTable dt = ds2.Tables[1];
            DataTable dt1 = ds2.Tables[0];

            LocalReport Rpt1 = new LocalReport();
            DataView dv1 = new DataView();
            //Sale Rate, Parking        
            //  dv1.RowFilter = ("grp='B'  and  infcod like '510100101%' and infcod not like '%000'");

            string salrate = (dtr.Select("grp='B'  and  infcod like '510100101%' and infcod not like '%000'")).Length == 0 ? ""
                : Convert.ToDouble(dtr.Select("grp='B'  and  infcod like '510100101%' and infcod not like '%000'")[0]["salrate"]).ToString("#,##0;(#,##0);") + "/Sft";

            string parking = (dtr.Select("grp='B'  and  infcod like '510100102001%' ")).Length == 0 ? ""
                : Convert.ToDouble(dtr.Select("grp='B'  and  infcod like '510100102001%' ")[0]["salrate"]).ToString("#,##0;(#,##0);");
            //Far
            dv1 = dt.Copy().DefaultView;
            double tuasarea = 0.00;
            dv1.RowFilter = ("prgcod like '25%'  and prgdesc1<>''");
            dt1 = dv1.ToTable();
            foreach (DataRow drb in dt1.Rows)
                tuasarea += Convert.ToDouble(drb["prgdesc1"]);
            string basement = dt.Select("prgcod='11101'").Length == 0 ? "" : dt.Select("prgcod='11101'")[0]["prgdesc1"].ToString();

            //Land Cost, Building Cost, Design Cost, Operation/Management Cost, Return of Investment
            double landaread = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble(dt.Select("prgcod='02003'")[0]["prgdesc1"].ToString());
            dv1 = dtr.DefaultView;
            dv1.RowFilter = ("infcod like '5201001%'");
            double landcost = Convert.ToDouble((Convert.IsDBNull(dv1.ToTable().Compute("Sum(amt)", "")) ? 0.00 : dv1.ToTable().Compute("Sum(amt)", "")));
            string landcostpk = landaread == 0.00 ? "" : Convert.ToDouble((landcost * 0.0000001) / landaread).ToString("#,##0.00;(#,##0.00); ") + "Cr." + " /Katha";

            dv1 = dtr.DefaultView;
            dv1.RowFilter = ("infcod like '5201004%'");
            double conscost = Convert.ToDouble((Convert.IsDBNull(dv1.ToTable().Compute("Sum(amt)", "")) ? 0.00 : dv1.ToTable().Compute("Sum(amt)", "")));
            string conscostpk = landaread == 0.00 ? "" : Convert.ToDouble((conscost * 0.0000001) / landaread).ToString("#,##0.00;(#,##0.00); ") + "Cr." + " /Katha";

            dv1 = dtr.DefaultView;
            dv1.RowFilter = ("infcod like '5201002%'");
            double designcost = Convert.ToDouble((Convert.IsDBNull(dv1.ToTable().Compute("Sum(amt)", "")) ? 0.00 : dv1.ToTable().Compute("Sum(amt)", "")));
            string designcostpk = landaread == 0.00 ? "" : Convert.ToDouble((designcost * 0.0000001) / landaread).ToString("#,##0.00;(#,##0.00); ") + "Cr." + " /Katha";

            dv1 = dtr.DefaultView;
            dv1.RowFilter = ("infcod like '5201005%'");
            double operecost = Convert.ToDouble((Convert.IsDBNull(dv1.ToTable().Compute("Sum(amt)", "")) ? 0.00 : dv1.ToTable().Compute("Sum(amt)", "")));
            string operecostpk = landaread == 0.00 ? "" : Convert.ToDouble((operecost * 0.0000001) / landaread).ToString("#,##0.00;(#,##0.00); ") + "Cr." + " /Katha";

            double signmoney = dtr.Select("infcod='520100101001'").Length == 0 ? 0.00 : Convert.ToDouble(dtr.Select("infcod='520100101001'")[0]["amt"].ToString());
            double toprocost = dtr.Select("infcod='5201005BAAA'").Length == 0 ? 0.00 : Convert.ToDouble(dtr.Select("infcod='5201005BAAA'")[0]["amt"].ToString());
            double profit = dtr.Select("infcod='5201006AAAAA'").Length == 0 ? 0.00 : Convert.ToDouble(dtr.Select("infcod='5201006AAAAA'")[0]["amt"].ToString());
            string reinvest = signmoney == 0.00 ? "" : Convert.ToDouble((profit * 100) / signmoney).ToString("#,##0.00;(#,##0.00); ") + " %";
            string proaprocost = toprocost == 0.00 ? "" : Convert.ToDouble((profit * 100) / toprocost).ToString("#,##0.00;(#,##0.00); ") + " %";

            string proname = dt.Select("prgcod='02002'").Length == 0 ? "" : dt.Select("prgcod='02002'")[0]["prgdesc1"].ToString();
            string landarea = dt.Select("prgcod='02003'").Length == 0 ? "" : dt.Select("prgcod='02003'")[0]["prgdesc1"].ToString() + " Katha";
            string buildarea = dt.Select("prgcod='01001'").Length == 0 ? "" : dt.Select("prgcod='01001'")[0]["prgdesc1"].ToString() + " Sft";
            string usablarea = dt.Select("prgcod='13002'").Length == 0 ? (tuasarea.ToString("#,##0.00;(#,##0.00); ") + " Sft")
                : dt.Select("prgcod='13002'")[0]["prgdesc1"].ToString() + " Sft";
            string selabelarea = dt.Select("prgcod='01002'").Length == 0 ? "" : dt.Select("prgcod='01002'")[0]["prgdesc1"].ToString() + " Sft";
            string buildheight = dt.Select("prgcod='11001'").Length == 0 ? "" : dt.Select("prgcod='11001'")[0]["prgdesc1"].ToString();
            basement = (dt.Select("prgcod='11002'").Length == 0 ? basement : dt.Select("prgcod='11002'")[0]["prgdesc1"].ToString()) + " Nos";
            string tcomfloor = dt.Select("prgcod='12003'").Length == 0 ? "" : dt.Select("prgcod='12003'")[0]["prgdesc1"].ToString();
            string tparking = dt.Select("prgcod='12005'").Length == 0 ? "" : dt.Select("prgcod='12005'")[0]["prgdesc1"].ToString();
            string comshare = dt.Select("prgcod='14001'").Length == 0 ? "" : dt.Select("prgcod='14001'")[0]["prgdesc1"].ToString() + " %";

            string groundfloor = dt.Select("prgcod='13105'").Length == 0 ? "" : dt.Select("prgcod='13105'")[0]["prgdesc1"].ToString() + " Sft";
            string firstfloor = dt.Select("prgcod='13106'").Length == 0 ? "" : dt.Select("prgcod='13106'")[0]["prgdesc1"].ToString() + " Sft";
            string Secondfloor = dt.Select("prgcod='13107'").Length == 0 ? "" : dt.Select("prgcod='13107'")[0]["prgdesc1"].ToString() + " Sft";
            string thirdfloor = dt.Select("prgcod='13108'").Length == 0 ? "" : dt.Select("prgcod='13108'")[0]["prgdesc1"].ToString() + " Sft";
            string tyfloor = dt.Select("prgcod='13109'").Length == 0 ? "" : dt.Select("prgcod='13109'")[0]["prgdesc1"].ToString() + " Sft";




            //string prol = dt.Select("prgcod='02003'").Length == 0 ? "" : dt.Select("prgcod='02003'")[0]["prgdesc1"].ToString();
            //string prol = dt.Select("prgcod='02002'").Length == 0 ? "" : dt.Select("prgcod='02002'")[0]["prgdesc1"].ToString();
            //string prol = dt.Select("prgcod='02002'").Length == 0 ? "" : dt.Select("prgcod='02002'")[0]["prgdesc1"].ToString();
            //string prol = dt.Select("prgcod='02002'").Length == 0 ? "" : dt.Select("prgcod='02002'")[0]["prgdesc1"].ToString();
            //string prol = dt.Select("prgcod='02002'").Length == 0 ? "" : dt.Select("prgcod='02002'")[0]["prgdesc1"].ToString();
            //string prol = dt.Select("prgcod='02002'").Length == 0 ? "" : dt.Select("prgcod='02002'")[0]["prgdesc1"].ToString();
            //string prol = dt.Select("prgcod='02002'").Length == 0 ? "" : dt.Select("prgcod='02002'")[0]["prgdesc1"].ToString();
            //string mAdNo = this.ddlPrevADNumber.SelectedValue.ToString();
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETADINFO", mAdNo, "",
            //              "", "", "", "", "", "", "");


            //Session["tbladwork"] = ds1.Tables[0];

            //DataTable dt = (DataTable)Session["tbladwork"];
            var lst = ds2.Tables[0].DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.EClassProFeasibility>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_02_Fea.rptProjectFeasibility", lst, null, null);
            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("proname", proname));
            Rpt1.SetParameters(new ReportParameter("landarea", landarea));
            Rpt1.SetParameters(new ReportParameter("buildarea", buildarea));
            Rpt1.SetParameters(new ReportParameter("usablarea", usablarea));
            Rpt1.SetParameters(new ReportParameter("selabelarea", selabelarea));
            Rpt1.SetParameters(new ReportParameter("buildheight", buildheight));
            Rpt1.SetParameters(new ReportParameter("basement", basement));
            Rpt1.SetParameters(new ReportParameter("tcomfloor", tcomfloor));
            Rpt1.SetParameters(new ReportParameter("tparking", tparking));
            Rpt1.SetParameters(new ReportParameter("comshare", comshare));
            Rpt1.SetParameters(new ReportParameter("grfloor", groundfloor));
            Rpt1.SetParameters(new ReportParameter("firstfloor", firstfloor));
            Rpt1.SetParameters(new ReportParameter("sndfloor", Secondfloor));
            Rpt1.SetParameters(new ReportParameter("thirdfloor", thirdfloor));
            Rpt1.SetParameters(new ReportParameter("typicalfloor", tyfloor));
            Rpt1.SetParameters(new ReportParameter("Averagefloor", salrate));
            Rpt1.SetParameters(new ReportParameter("parking", parking));
            Rpt1.SetParameters(new ReportParameter("landcpkhata", landcostpk));
            Rpt1.SetParameters(new ReportParameter("bcostpkhata", conscostpk));
            Rpt1.SetParameters(new ReportParameter("designcpkhata", designcostpk));
            Rpt1.SetParameters(new ReportParameter("mancpkhata", operecostpk));
            //Rpt1.SetParameters(new ReportParameter("pevenpoinwithp", parking));
            //Rpt1.SetParameters(new ReportParameter("evenpoinwithoutp", parking));

            Rpt1.SetParameters(new ReportParameter("retofinvestment", reinvest));
            Rpt1.SetParameters(new ReportParameter("perofproapcost", proaprocost));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        private void GetProjectInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("tblprogeninfo");
            //  string ProjectCode = this.ddlProjectName.SelectedValue.ToString();


            string fpactcode = this.ddlProjectName.SelectedValue.ToString();

            // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();
            string prjtype = "Commercial";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "PROJECTINFO", pactcode, fpactcode, prjtype, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;

            }
            DataTable dt = ds1.Tables[0];
            // DataView dv1;
            Session["tblprogeninfo"] = dt;
            this.gvProjectInfo.DataSource = ds1.Tables[0];
            this.gvProjectInfo.DataBind();
            this.llbtnCalculation_Click(null, null);
            this.GridTextDDLVisible();





        }

        private void GridTextDDLVisible()
        {
            string comcod = this.GetComCode();
            DataTable dt = ((DataTable)Session["tblprogeninfo"]).Copy();

            int count = gvProjectInfo.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["prgcod"].ToString();
                string val = dt.Rows[i]["prgdesc1"].ToString();
                switch (Gcode)
                {
                    case "01003": //Start Date                

                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
                        break;



                    case "01004": //Start Date                   
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;

                        //ddlcataloc.SelectedValue = val;
                        break;



                    case "02041": //Location                
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        DropDownList ddlcataloc = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));


                        DataSet dsloc = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLOCATION", "", "", "", "", "", "", "", "", "");
                        ddlcataloc.DataTextField = "prgdesc";
                        ddlcataloc.DataValueField = "prgcod";
                        ddlcataloc.DataSource = dsloc.Tables[0];
                        ddlcataloc.DataBind();
                        if (val.Length > 0)
                            ddlcataloc.SelectedValue = val;

                        break;


                    case "02045": //Category                  
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
                        DropDownList ddlcatag = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));


                        DataSet dscatg = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                        ddlcatag.DataTextField = "prgdesc";
                        ddlcatag.DataValueField = "prgcod";
                        ddlcatag.DataSource = dscatg.Tables[0];
                        ddlcatag.DataBind();
                        if (val.Length > 0)
                            ddlcatag.SelectedValue = val;

                        break;




                    default:
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
                        break;

                }
            }

        }
        protected void llbtnTotalproinfo_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];

            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {

                string lblgvItmCode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                double txtestcost = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim());
                double txtbuiactual = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuiactual")).Text.Trim());
                //double c2hour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Text.Trim());
                //double c3hour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Text.Trim());

               // double tohour = fixhour + hourly + c1hour + c2hour + c3hour;
               // rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEmpOverTime.PageIndex) + i;
                dt.Rows[i]["estgcod"] = lblgvItmCode;
                dt.Rows[i]["estcost"] = txtestcost;
                dt.Rows[i]["actual"] = txtbuiactual;
          

                Session["tblfeaprj"] = dt;

            }

            //for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            //{
            //    string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //    string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
            //    string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //    string buildarea = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuildarea")).Text.Trim()).ToString("#,##0.0000;(#,##0.0000);");


            //    if (Gcode == "01003" || Gcode == "01004")
            //    {

            //        Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
            //    }



            //    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
            //    dt.Rows[i]["prgdesc1"] = Gvalue;
            //    dt.Rows[i]["buildarea"] = buildarea;

            //}





            //string comcod = this.GetComCode();

            //switch (comcod)
            //{

            //    case "3335":
            //        double lsize = dt.Select("prgcod='02005'").Length > 0 ? Convert.ToDouble(dt.Select("prgcod='02005'")[0]["prgdesc1"]) : 0.00;
            //        double mgc = dt.Select("prgcod='02004'").Length > 0 ? Convert.ToDouble(dt.Select("prgcod='02004'")[0]["prgdesc1"]) * .01 : 0.00;
            //        double storeid = dt.Select("prgcod='02008'").Length > 0 ? Convert.ToDouble(dt.Select("prgcod='02008'")[0]["prgdesc1"]) : 0.00;

            //        storeid = storeid > 1 ? storeid - 1 : storeid;
            //        double flrconarea = lsize * 720 * mgc * storeid;
            //        double cbarconarea = lsize * 720 * mgc * .05 * storeid;
            //        double tconarea = ((flrconarea + cbarconarea) * .15) + (flrconarea + cbarconarea);

            //        DataRow[] dr = dt.Select("prgcod='01001'");
            //        dr[0]["prgdesc1"] = tconarea.ToString("#,##0;(#,##0);");
            //        break;

            //    default:

            //        break;
            //}







            //this.gvProjectInfo.DataSource = dt;
            //this.gvProjectInfo.DataBind();
            //this.GridTextDDLVisible();


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            //    string Gcode = dt.Rows[i]["prgcod"].ToString();

            //    switch (Gcode)
            //    {
            //        case "01003": //Start Date                

            //            ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //            break;



            //        case "01004": //Start Date                   
            //            ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            break;




            //        default:
            //            ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //            break;

            //    }
            //}


        }

        protected void llbtnCalculation_Click(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)Session["tblfeaprj"]).Copy();
            string Joindate = "";
            DataTable dt1 = new DataTable();
            DataView dv1 = new DataView();
            double avgaptsize = 0.00, depshare = 0.00;
            string prgcod = "";
            double fabalcony = 0.00, fasbalcony = 0.00, fabafstair = 0.00, faotrace = 0.00, faothers = 0.00, gcov = 0.00, gcovsal = 0.00,
                lareainsft = 0.00, bheight = 0.00, tbuildacal = 0.00, tuasarea = 0.00, tcomarea = 0.00, buildpod = 0.00, commonpod = 0.00, tobuildarea = 0.00, constarea = 0.00;

            double cost = 0.00;
            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                //string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                string Gvalue = "";

                //string txtestcost = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim();
                double txtestcost = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtestcost")).Text.Trim());

                string txtbuiactual = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuiactual")).Text.Trim()).ToString("#,##0.0000;(#,##0.000);");
                string gtype = dt.Rows[i]["prgval"].ToString();
                string buildarea = "";

                if (Gcode == "02001" || Gcode == "02002" || Gcode == "02003")
                {

                    //Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                    cost = cost+txtestcost;

                    dt.Rows[0]["estcost"] = cost;

                }








                //switch (Gcode)
                //{


                //    case "01002": //Salable


                //        depshare = dt.Select("prgcod='14001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='14001'")[0]["prgdesc1"]);
                //        constarea = dt.Select("prgcod='01001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='01001'")[0]["prgdesc1"]);

                //        Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() == "") ? "" : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                //        Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;

                //        Gvalue = (Gvalue == "0" ? (constarea * depshare * 0.01).ToString() : Gvalue);
                //        //dt.Rows[i]["prgdesc1"] = Gvalue;
                //        dt.Select("prgcod='01002'")[0]["prgdesc1"] = Gvalue;
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;



                //    case "01003": //Start Date
                //    case "01004": //End Date
                //        Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                //        Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                //        dt.Rows[i]["prgdesc1"] = Gvalue;
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;

                //    case "02013": //Total Land Area in SFT 
                //                  // Total Land Area in Khatha   02003
                //        double areainkatha = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["prgdesc1"]);
                //        dt.Rows[i]["prgdesc1"] = areainkatha * 720;
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;


                //    case "02017": //Total Far Excluding Area in SFT 
                //                  // Total Land Area in Khatha   02003
                //        fabalcony = dt.Select("prgcod='02019'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02019'")[0]["prgdesc1"]);
                //        fasbalcony = dt.Select("prgcod='02021'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02021'")[0]["prgdesc1"]);
                //        fabafstair = dt.Select("prgcod='02023'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02023'")[0]["prgdesc1"]);
                //        faotrace = dt.Select("prgcod='02025'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02025'")[0]["prgdesc1"]);
                //        faothers = dt.Select("prgcod='02027'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02027'")[0]["prgdesc1"]);
                //        dt.Rows[i]["prgdesc1"] = (fabalcony + fasbalcony + fabafstair + faotrace + faothers).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;

                //        break;


                //    case "02027": //Total Far Excluding others 
                //                  // Total Land Area in Khatha   02003
                //        fabalcony = dt.Select("prgcod='02019'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02019'")[0]["prgdesc1"]);
                //        fasbalcony = dt.Select("prgcod='02021'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02021'")[0]["prgdesc1"]);
                //        fabafstair = dt.Select("prgcod='02023'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02023'")[0]["prgdesc1"]);
                //        faotrace = dt.Select("prgcod='02025'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02025'")[0]["prgdesc1"]);
                //        faothers = dt.Select("prgcod='02027'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02027'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='02017'")[0]["prgdesc1"] = (fabalcony + fasbalcony + fabafstair + faotrace + faothers).ToString("#,##0.0000;(#,##0.0000);");


                //        Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                //        dt.Rows[i]["prgdesc1"] = Gvalue;
                //        dt.Rows[i]["buildarea"] = buildarea;

                //        break;




                //    case "02029": //Road width(m)
                //                  // Total Land Area in Khatha   02003
                //        double roadwf = dt.Select("prgcod='02015'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02015'")[0]["prgdesc1"]);
                //        dt.Rows[i]["prgdesc1"] = Math.Round(roadwf / 3.28, 0);
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;



                //    case "02031": //Total Far Area
                //                  // area in sft*far
                //        double areaisft = dt.Select("prgcod='02013'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02013'")[0]["prgdesc1"]);
                //        double far = dt.Select("prgcod='02005'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02005'")[0]["prgdesc1"]);
                //        dt.Rows[i]["prgdesc1"] = areaisft * far;
                //        dt.Rows[i]["buildarea"] = buildarea;

                //        break;



                //    case "12003": //Total nos of Appartment
                //                  // UTypical Floor*
                //        double flheight = dt.Select("prgcod='11005'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11005'")[0]["prgdesc1"]);
                //        double uperfloor = dt.Select("prgcod='12001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='12001'")[0]["prgdesc1"]);
                //        dt.Rows[i]["prgdesc1"] = flheight * uperfloor;
                //        dt.Rows[i]["buildarea"] = buildarea;


                //        break;


                //    case "13000": //Total Build AreaCal

                //        dv1 = dt.Copy().DefaultView;
                //        dv1.RowFilter = ("prgcod like '13%' and prgval='N' and (prgdesc1<>'' or buildarea>0) ");
                //        dt1 = dv1.ToTable();
                //        foreach (DataRow drb in dt1.Rows)
                //        {
                //            tbuildacal += Convert.ToDouble("0" + drb["prgdesc1"]);
                //            tobuildarea += Convert.ToDouble(drb["buildarea"]);
                //        }
                //        dt.Select("prgcod='13000'")[0]["prgdesc1"] = tbuildacal.ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Select("prgcod='13000'")[0]["buildarea"] = tobuildarea.ToString("#,##0.0000;(#,##0.0000);");
                //        break;

                //    case "13001": //Total Build Area
                //                  // area in sft*far
                //        double tfararea = dt.Select("prgcod='02031'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02031'")[0]["prgdesc1"]);
                //        double tfarexasft = dt.Select("prgcod='02017'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02017'")[0]["prgdesc1"]);
                //        dt.Rows[i]["prgdesc1"] = (tfararea + tfarexasft).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;

                //        dt.Select("prgcod='01001'")[0]["prgdesc1"] = (tfararea + tfarexasft).ToString("#,##0.0000;(#,##0.0000);");


                //        // dt.Rows[i]["buildarea"] = buildarea;
                //        break;



                //    case "13002": //Total Usable Area
                //                  // Unit Per Floor*
                //        double taptno = dt.Select("prgcod='12003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='12003'")[0]["prgdesc1"]);
                //        // double uperflr = dt.Select("prgcod='12001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='12001'")[0]["prgdesc1"]);
                //        avgaptsize = dt.Select("prgcod='12002'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='12002'")[0]["prgdesc1"]);
                //        dt.Rows[i]["prgdesc1"] = (taptno * avgaptsize).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;


                //    case "13003": //Total Salable Area
                //                  // Unit Per Floor*
                //                  //14001

                //        avgaptsize = dt.Select("prgcod='12002'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='12002'")[0]["prgdesc1"]);
                //        taptno = dt.Select("prgcod='12003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='12003'")[0]["prgdesc1"]);
                //        depshare = dt.Select("prgcod='14001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='14001'")[0]["prgdesc1"]);
                //        dt.Rows[i]["prgdesc1"] = (avgaptsize * taptno * depshare * 0.01).ToString("#,##0.0000;(#,##0.0000);");
                //        // dt.Select("prgcod='01002'")[0]["prgdesc1"] = (avgaptsize * taptno * depshare * 0.01).ToString("#,##0.0000;(#,##0.0000);");

                //        dt.Rows[i]["buildarea"] = buildarea;

                //        break;



                //    case "13101": //Basement Calculation

                //        lareainsft = dt.Select("prgcod='02013'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02013'")[0]["prgdesc1"]);
                //        bheight = dt.Select("prgcod='11101'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11101'")[0]["prgdesc1"]);
                //        gcov = dt.Select("prgcod='21001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21001'")[0]["prgdesc1"]);
                //        gcovsal = dt.Select("prgcod='21001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21001'")[0]["buildarea"]);
                //        dt.Select("prgcod='13101'")[0]["prgdesc1"] = (lareainsft * bheight * gcov * 0.01).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = (lareainsft * bheight * gcovsal * 0.01).ToString("#,##0.0000;(#,##0.0000);"); ;


                //        break;


                //    case "13105": //Ground floor Calculation

                //        lareainsft = dt.Select("prgcod='02013'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02013'")[0]["prgdesc1"]);
                //        bheight = dt.Select("prgcod='11105'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11105'")[0]["prgdesc1"]);
                //        gcov = dt.Select("prgcod='21002'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21002'")[0]["prgdesc1"]);
                //        gcovsal = dt.Select("prgcod='21002'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21002'")[0]["buildarea"]);
                //        dt.Select("prgcod='13105'")[0]["prgdesc1"] = (lareainsft * bheight * gcov * 0.01).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = (lareainsft * bheight * gcovsal * 0.01).ToString("#,##0.0000;(#,##0.0000);"); ;

                //        break;




                //    case "13106": // 1st floor 

                //        lareainsft = dt.Select("prgcod='02013'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02013'")[0]["prgdesc1"]);
                //        bheight = dt.Select("prgcod='11106'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11106'")[0]["prgdesc1"]);
                //        gcov = dt.Select("prgcod='21003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21003'")[0]["prgdesc1"]);
                //        gcovsal = dt.Select("prgcod='21003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21003'")[0]["buildarea"]);
                //        dt.Select("prgcod='13106'")[0]["prgdesc1"] = (lareainsft * bheight * gcov * 0.01).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = (lareainsft * bheight * gcovsal * 0.01).ToString("#,##0.0000;(#,##0.0000);");

                //        break;

                //    case "13107": //2nd floor 

                //        lareainsft = dt.Select("prgcod='02013'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02013'")[0]["prgdesc1"]);
                //        bheight = dt.Select("prgcod='11107'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11107'")[0]["prgdesc1"]);
                //        gcov = dt.Select("prgcod='21004'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21004'")[0]["prgdesc1"]);
                //        gcovsal = dt.Select("prgcod='21004'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21004'")[0]["buildarea"]);
                //        dt.Select("prgcod='13107'")[0]["prgdesc1"] = (lareainsft * bheight * gcov * 0.01).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = (lareainsft * bheight * gcovsal * 0.01).ToString("#,##0.0000;(#,##0.0000);");

                //        break;

                //    case "13108": //  3rd floor 

                //        lareainsft = dt.Select("prgcod='02013'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02013'")[0]["prgdesc1"]);
                //        bheight = dt.Select("prgcod='11108'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11108'")[0]["prgdesc1"]);
                //        gcov = dt.Select("prgcod='21005'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21005'")[0]["prgdesc1"]);
                //        gcovsal = dt.Select("prgcod='21005'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21005'")[0]["buildarea"]);
                //        dt.Select("prgcod='13108'")[0]["prgdesc1"] = (lareainsft * bheight * gcov * 0.01).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = (lareainsft * bheight * gcovsal * 0.01).ToString("#,##0.0000;(#,##0.0000);");

                //        break;


                //    case "13109": //  Typical 

                //        lareainsft = dt.Select("prgcod='02013'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02013'")[0]["prgdesc1"]);
                //        bheight = dt.Select("prgcod='11109'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11109'")[0]["prgdesc1"]);
                //        gcov = dt.Select("prgcod='21011'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21011'")[0]["prgdesc1"]);
                //        gcovsal = dt.Select("prgcod='21011'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21011'")[0]["buildarea"]);
                //        dt.Select("prgcod='13109'")[0]["prgdesc1"] = (lareainsft * bheight * gcov * 0.01).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = (lareainsft * bheight * gcovsal * 0.01).ToString("#,##0.0000;(#,##0.0000);");

                //        break;







                //    case "14002": //Land owner Share
                //        depshare = dt.Select("prgcod='14001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='14001'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='14002'")[0]["prgdesc1"] = 100 - depshare;
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;

                //    case "23000": //Common Area Calculation
                //        dv1 = dt.Copy().DefaultView;
                //        dv1.RowFilter = ("prgcod like '23%' and prgval='N'  and prgdesc1<>''");
                //        dt1 = dv1.ToTable();
                //        foreach (DataRow drb in dt1.Rows)
                //        {
                //            prgcod = drb["prgcod"].ToString();

                //            bheight = (prgcod == "23001") ?
                //                (dt.Select("prgcod='23001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11105'")[0]["prgdesc1"]))
                //                : (prgcod == "23003") ? (dt.Select("prgcod='11106'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11106'")[0]["prgdesc1"]))
                //                : (prgcod == "23004") ? (dt.Select("prgcod='11107'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11107'")[0]["prgdesc1"]))
                //                : (prgcod == "23005") ? (dt.Select("prgcod='11108'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11108'")[0]["prgdesc1"]))
                //                : 1;

                //            tcomarea += Convert.ToDouble(drb["prgdesc1"]) * bheight;
                //        }



                //        dt.Select("prgcod='23000'")[0]["prgdesc1"] = tcomarea.ToString("#,##0.0000;(#,##0.0000);");
                //        break;



                //    case "23001": //  Common Ground Floor                    

                //        lareainsft = dt.Select("prgcod='02013'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02013'")[0]["prgdesc1"]);
                //        bheight = dt.Select("prgcod='11109'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='11109'")[0]["prgdesc1"]);
                //        gcov = dt.Select("prgcod='21011'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='21011'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='13109'")[0]["prgdesc1"] = (lareainsft * bheight * gcov * 0.01).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;

                //        break;





                //    case "25000": //Total Usable /Saleble Area
                //        dv1 = dt.Copy().DefaultView;
                //        dv1.RowFilter = ("prgcod like '25%' and prgval='N'  and prgdesc1<>''");
                //        dt1 = dv1.ToTable();
                //        foreach (DataRow drb in dt1.Rows)
                //            tuasarea += Convert.ToDouble(drb["prgdesc1"]);
                //        dt.Select("prgcod='25000'")[0]["prgdesc1"] = tuasarea.ToString("#,##0.0000;(#,##0.0000);");
                //        break;




                //    case "25001": //Usable Ground Floor
                //        double buildgr = dt.Select("prgcod='13105'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='13105'")[0]["prgdesc1"]);
                //        double commongr = dt.Select("prgcod='23001'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='23001'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='25001'")[0]["prgdesc1"] = (buildgr + commongr).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;

                //    case "25003": //Usable 1st Floor
                //        buildpod = dt.Select("prgcod='13106'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='13106'")[0]["prgdesc1"]);
                //        commonpod = dt.Select("prgcod='23003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='23003'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='25003'")[0]["prgdesc1"] = (buildpod + commonpod).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;

                //    case "25004": //Usable 2nd Floor
                //        buildpod = dt.Select("prgcod='13107'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='13107'")[0]["prgdesc1"]);
                //        commonpod = dt.Select("prgcod='23004'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='23004'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='25004'")[0]["prgdesc1"] = (buildpod + commonpod).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;

                //    case "25005": //Usable 3rd Floor
                //        buildpod = dt.Select("prgcod='13108'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='13108'")[0]["prgdesc1"]);
                //        commonpod = dt.Select("prgcod='23005'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='23005'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='25005'")[0]["prgdesc1"] = (buildpod + commonpod).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;



                //    case "25011": //Usable Typical
                //        double buildtyp = dt.Select("prgcod='13109'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='13109'")[0]["prgdesc1"]);
                //        double commontyp = dt.Select("prgcod='23011'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='23011'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='25011'")[0]["prgdesc1"] = (buildtyp + commontyp).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;


                //    case "25012": //Usable Roof Top
                //        double buildrtop = dt.Select("prgcod='13110'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='13110'")[0]["prgdesc1"]);
                //        double commonrtop = dt.Select("prgcod='23012'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='23012'")[0]["prgdesc1"]);
                //        dt.Select("prgcod='25012'")[0]["prgdesc1"] = (buildrtop + commonrtop).ToString("#,##0.0000;(#,##0.0000);");
                //        dt.Rows[i]["buildarea"] = buildarea;
                //        break;










                //    default:
                //        //Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                //        //dt.Rows[i]["prgdesc1"] = Gvalue;
                //        //dt.Rows[i]["buildarea"] = buildarea;
                //        break;






                //}


            }

            Session["tblprogeninfo"] = dt;
            this.gvProjectInfo.DataSource = dt;
            this.gvProjectInfo.DataBind();
            //this.GridTextDDLVisible();



        }
        protected void lUpdatProInfo_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string fpactcode = this.ddlProjectName.SelectedValue.ToString();
            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();


            bool result = false;
            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                if (Gcode.Substring(2) == "000")
                    continue;

                string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue1 = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                string buildarea = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuildarea")).Text.Trim()).ToString();

                DropDownList ddlloc = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc")) as DropDownList;

                string Gvalue = "";

                if (Gcode == "02041" || Gcode == "02045")
                {
                    Gvalue = ddlloc.SelectedValue.ToString();
                }
                else
                {
                    Gvalue = Gvalue1;
                }

                if (Gcode == "01003" || Gcode == "01004")
                {

                    Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }



                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSORUPLDPRJINF", pactcode, Gcode, gtype, Gvalue, fpactcode, buildarea, "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = feaData.ErrorObject["Msg"].ToString();
                    return;
                }

            }


            //string pcdate = this.txtDate.Text.Trim();
            result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSOUPPCDATE", fpactcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();

            //for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            //{
            //    string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //    string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
            //    string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? (0 + Gvalue).ToString() : Gvalue;

            //        bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INSERTORUPFEAPRJINF", pactcode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "", "");
            //        if (!result)
            //        {
            //         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
            //            return;
            //        }

            //}
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                //string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                //bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        //private void ShowRevenue()
        //{
        //    //sircode like '0[89]%'  or  sircode like '1[0-9]%'

        //    Session.Remove("tblfeaprj");
        //    string comcod = this.GetComCode();
        //    string pactcode = this.ddlProjectName.SelectedValue.ToString();
        //    string Code = (this.rbtnList1.SelectedIndex == 1) ? "infcod like '51%'" : (this.rbtnList1.SelectedIndex == 2) ? "infcod like '5[2-5]%'" : "infcod like '5[67]%'";
        //    DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROSALES", pactcode, Code, "", "", "", "", "", "", "");
        //    if (ds2 == null)
        //    {

        //        this.gvFeaPrj.DataSource = null;
        //        this.gvFeaPrj.DataBind();
        //        return;
        //    }
        //    Session["tblfeaprj"] = ds2.Tables[0];
        //    this.Data_Bind();
        //}
        //private string GetCompProOnSaleorCost()
        //{

        //    string comcod = this.GetComCode();
        //    string proonsalorcost = "";
        //    switch (comcod)
        //    {
        //        case "1205"://P2P Construction
        //        case "3351"://Wecon Properties
        //        case "3352"://P2P 360
        //            proonsalorcost = "proonSales";
        //            break;


        //        default:
        //            break;



        //    }

        //    return proonsalorcost;



        //}

        //private void ShowReport()
        //{
        //    Session.Remove("tblfeaprj");
        //    string comcod = this.GetComCode();
        //    string pactcode = this.ddlProjectName.SelectedValue.ToString();
        //    string proonsalorcost = this.GetCompProOnSaleorCost();
        //    DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITY", pactcode, proonsalorcost, "", "", "", "", "", "", "");
        //    if (ds2 == null)
        //    {

        //        this.gvFeaPrjRep.DataSource = null;
        //        this.gvFeaPrjRep.DataBind();
        //        return;
        //    }
        //    Session["tblfeaprj"] = this.HiddenSameData(ds2.Tables[0]);
        //    DataTable dt = this.HiddenSameData(ds2.Tables[0]);
        //    this.Data_Bind();



        //}


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



        //private void Data_Bind()
        //{
        //    int rindex = this.rbtnList1.SelectedIndex;
        //    DataTable dt = (DataTable)Session["tblfeaprj"];
        //    switch (rindex)
        //    {

        //        case 1:

        //            this.gvFeaPrj.DataSource = dt;
        //            this.gvFeaPrj.DataBind();
        //            this.FooterCal();
        //            break;

        //        case 2:
        //            this.gvFeaPrjC.DataSource = dt;
        //            this.gvFeaPrjC.DataBind();
        //            this.FooterCal();
        //            break;

        //        case 3:
        //            this.gvFeaLOwner.DataSource = dt;
        //            this.gvFeaLOwner.DataBind();
        //            this.FooterCal();
        //            break;

        //        case 4:
        //            this.gvFeaPrjRep.DataSource = dt;
        //            this.gvFeaPrjRep.DataBind();
        //            break;



        //    }


        //}
        //private void FooterCal()
        //{
        //    DataTable dt = (DataTable)Session["tblfeaprj"];
        //    if (dt.Rows.Count == 0)
        //        return;
        //    double Stotalsize, stotalAmt;
        //    int rindex = this.rbtnList1.SelectedIndex;
        //    switch (rindex)
        //    {

        //        case 1:
        //            DataView dv = dt.DefaultView;
        //            dv.RowFilter = "infcod like('51%')";
        //            DataTable dts = dv.ToTable().Copy();
        //            //Size
        //            dv = dts.DefaultView;
        //            dv.RowFilter = ("infcod like '510100101%'");
        //            DataTable dtsize = dv.ToTable();


        //            Stotalsize = Convert.ToDouble((Convert.IsDBNull(dtsize.Compute("Sum(tsize)", "")) ?
        //            0.00 : dtsize.Compute("Sum(tsize)", "")));
        //            stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
        //            0.00 : dt.Compute("Sum(amt)", "")));
        //            ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFTsize")).Text = Stotalsize.ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFSratepsft")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
        //            ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
        //            break;
        //        case 2:
        //            //((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFsalrate")).Text =Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salrate)", "")) ?
        //            //0.00 : dt.Compute("Sum(salrate)", ""))).ToString("#,##0.00;(#,##0.00); ") ;

        //            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
        //                0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");
        //            break;

        //        case 3:
        //            Stotalsize = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsize)", "")) ?
        //            0.00 : dt.Compute("Sum(tsize)", "")));
        //            stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
        //            0.00 : dt.Compute("Sum(amt)", "")));
        //            ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFTsizel")).Text = Stotalsize.ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFsalratel")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
        //            ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFAmtl")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
        //            break;
        //    }

        //}


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
            //this.FooterCal();




        }





    }
}

            //string comcod = this.GetComCode();
            //DataView dv;
            //switch(comcod)
            //{ 
            //    case "3336":
            //        dv = dt.Copy().DefaultView;
            //        dv.RowFilter = ("infcod like '520100401%'");
            //        DataTable dt1 = dv.ToTable();
            //        double tocost = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt)", "")) ? 0.00 : dt1.Compute("Sum(amt)", "")));
            //        double infla = tocost * .01;
            //        double opeoverhead = tocost * .05;
            //        (dt.Select("infcod='520100402017'"))[0]["amt"]=infla;
            //        (dt.Select("infcod='520100403001'"))[0]["amt"] = opeoverhead;

            //        break;

            //    default:
            //        break;

            //}


            //Session["tblfeaprj"] = dt;
            //this.Data_Bind();
     
       







           
       
        



 
       


     
      
     
