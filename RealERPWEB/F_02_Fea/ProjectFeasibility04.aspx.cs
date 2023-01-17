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
    public partial class ProjectFeasibility04 : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        public static string Url = "";
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
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
                    this.chkCommercial.Visible = true;
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
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"];

            Session["tblpro"] = ds1.Tables[0];
            ds1.Dispose();

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
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.PanelSelName.Visible = true;
                this.ProjectCDate();
                //this.RadioVisibility();
                return;
            }
            this.lbtnOk.Text = "Ok";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.rbtnList1.SelectedIndex = -1;
            this.gvProjectInfo.DataSource = null;
            this.gvProjectInfo.DataBind();
            this.gvFeaPrj.DataSource = null;
            this.gvFeaPrj.DataBind();
            this.gvFeaPrjC.DataSource = null;
            this.gvFeaPrjC.DataBind();
            this.gvFeaLOwner.DataSource = null;
            this.gvFeaLOwner.DataBind();
            this.gvFeaPrjRep.DataSource = null;
            this.gvFeaPrjRep.DataBind();
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.PanelSelName.Visible = false;
            this.ListViewEmpAll.DataSource = null;
            this.ListViewEmpAll.DataBind();
            this.ImagePanel.Visible = false;

           // this.rbtnList1.Items.Add("Image");



        }

        private void RadioVisibility()
        {
            string type = this.Request.QueryString["Type"];
            if (type == "doc" || type == "docmore")
            {
                //for (int i = 0; i <=rbtnList1.Items.Count; i++)
                //{
                //    this.rbtnList1.Items.RemoveAt(i);
                //}
                this.rbtnList1.Items.Remove("Project Information");
                this.rbtnList1.Items.Remove("Sales Revenue");
                this.rbtnList1.Items.Remove("Cost");
                this.rbtnList1.Items.Remove("Land Owner's Benefit");
                this.rbtnList1.Items.Remove("Reports");
            }
            else
            {
                this.rbtnList1.Items.RemoveAt(5);
            }
        }
        private void ProjectCDate()
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "SHOWPCDATE", pactcode, "", "", "", "", "", "", "", "");
            this.txtDate.Text = (ds1.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["cdate"]).ToString("dd-MMM-yyyy");


        }


        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAllRes.Checked)
            {
                Session.Remove("tblfeaprj");
                string comcod = this.GetComCode();
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string Code = (this.rbtnList1.SelectedIndex == 1) ? "infcod like '51%'" : (this.rbtnList1.SelectedIndex == 2) ? "infcod like '5[2-5]%'" : "infcod like '5[67]%'";
                DataSet ds3 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "FEAPRANDPRJCT", pactcode, Code, "", "", "", "", "", "", "");
                Session["tblfeaprj"] = ds3.Tables[0];
                this.Data_Bind();
            }

            else
            {
                this.ShowRevenue();
            }
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

                case "3353":
                case "3101":
                    this.PrintFeasibilityManama();
                    break;

                default:
                    //this.PrintFeasibilitygen();
                    this.PrintFeasibilitySuvastu();
                    break;

            }



        }

        private void PrintFeasibilityManama()
        {
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compName = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text.ToString().Substring(13);
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITIESMANAMA", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds2.Tables[0];
            var list1 = dt.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProjectFeasibility>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_02_Fea.RptProjFeasibilityManama", list1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "PROJECT FEASIBILITY"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            int rindex = this.rbtnList1.SelectedIndex;

            switch (rindex)
            {


                case 0:
                    if (this.Request.QueryString["Type"] == "FeaEntry" || this.Request.QueryString["Type"] == "fea")
                    {
                        this.GetProjectInfo();
                        this.chkAllRes.Visible = false;
                        this.MultiView1.ActiveViewIndex = rindex;
                    }
                    else
                    {
                        if (this.Request.QueryString["Type"] == "docmore")
                        {
                            this.rmrks.Visible = true;
                            this.imgpanel.Visible = false;
                            this.btnupdate.Visible = true;
                        }
                        if (this.Request.QueryString["Type"] == "doc")
                        {
                            this.rmrks.Visible = true;
                            this.btnupdate.Visible = false;

                        }
                        this.ShowProjectFiles();
                        this.chkAllRes.Visible = false;
                        this.ImagePanel.Visible = true;
                        this.MultiView1.ActiveViewIndex = 5;
                    }

                    break;

                case 1:
                    this.ShowRevenue();
                    this.chkAllRes.Visible = true;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;
                case 2:
                    this.ShowRevenue();
                    this.chkAllRes.Visible = true;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;
                case 3:
                    this.ShowRevenue();
                    this.chkAllRes.Visible = true;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;

                case 4:
                    this.ShowReport();
                    this.chkAllRes.Visible = false;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;


                case 5:
                    this.ShowReport();
                    this.chkAllRes.Visible = false;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;

                case 6: // Bank Interest
                    this.ShowBankInterest();
                    this.chkAllRes.Visible = false;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;
                    //case 5:

                    //    break;


            }

        }

        private void ShowProjectFiles()
        {
            ViewState.Remove("tblimages");
            string comcod = this.GetComCode();
            string factcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GET_PROJECT_FILES", factcode, "", "", "", "", "", "", "");

            ViewState["tblimages"] = ds1.Tables[0];
            ListViewEmpAll.DataSource = ds1.Tables[0];
            ListViewEmpAll.DataBind();
            this.legammsg.Text = (ds1.Tables[1].Rows.Count == 0) ? "" : ds1.Tables[1].Rows[0]["rmrks"].ToString();
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
            string prjtype = this.chkCommercial.Checked ? "Commercial" : "";
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





            DataTable dt = ((DataTable)Session["tblprogeninfo"]).Copy();

            DataTable dt1 = new DataTable();
            DataView dv1 = new DataView();

            double totallarea = 0.00;

            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                string buildarea = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuildarea")).Text.Trim()).ToString("#,##0.0000;(#,##0.000);");
                string gtype = dt.Rows[i]["prgval"].ToString();
                double ratio = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtratio")).Text.Trim().Replace("%", ""));
                string percnt = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtratio")).Text.Trim().Contains("%") ? "%" : "";
                if (Gcode == "01003" || Gcode == "01004")
                {

                    Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }



                //Common
                dt.Rows[i]["percnt"] = percnt;

                switch (Gcode)
                {


                    case "02006": //Total Land Area (According to FAR)                   
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = ratio * totallarea;
                        break;


                    case "02007": //Residential Area (Every Floor)                 
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea);
                        break;

                    case "02009": //(Floor Number)                
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = ratio == 0.00 ? 0.00 : totallarea / ratio;
                        break;

                    case "02011": //(Construcion Area)  
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02012": //(Foundation)               
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;



                    case "02032": //(Basement-1)                
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02035": //(Basement-2)                
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;

                    case "02037": //(Semi Basement)                
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02038": //(Ground Floor)                
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;

                    case "02039": //(Belconi)                
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02040": //(Roof top)                
                        totallarea = ASTUtility.StrPosOrNagative(Gvalue);
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02090": //(Total Construction Area)                


                        DataView dv = dt.Copy().DefaultView;
                        dv.RowFilter = ("prgcod>='02011' and prgcod<='02089'");
                        DataTable dttcon = dv.ToTable();

                        double tconsarea = Convert.ToDouble((Convert.IsDBNull(dttcon.Compute("Sum(total)", "")) ?
                    0.00 : dttcon.Compute("Sum(total)", "")));
                        //totallarea = dt.Select("prgcod='02007'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02007'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = "";
                        dt.Rows[i]["ratio"] = 0;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = tconsarea;
                        break;



                    default:
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = gtype == "N" ? ratio * ASTUtility.StrPosOrNagative(Gvalue) : 0;
                        break;






                }


            }

            Session["tblprogeninfo"] = dt;
            this.gvProjectInfo.DataSource = dt;
            this.gvProjectInfo.DataBind();
            this.GridTextDDLVisible();

        }

        protected void llbtnCalculation_Click(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)Session["tblprogeninfo"]).Copy();
            string Joindate = "";
            DataTable dt1 = new DataTable();
            DataView dv1 = new DataView();
            double avgaptsize = 0.00, depshare = 0.00;
            string prgcod = "";
            double fabalcony = 0.00, fasbalcony = 0.00, fabafstair = 0.00, faotrace = 0.00, faothers = 0.00, gcov = 0.00, gcovsal = 0.00,
                lareainsft = 0.00, bheight = 0.00, tbuildacal = 0.00, tuasarea = 0.00, tcomarea = 0.00, buildpod = 0.00, commonpod = 0.00, tobuildarea = 0.00, constarea = 0.00, totallarea = 0.00;

            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                string buildarea = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtbuildarea")).Text.Trim()).ToString("#,##0.0000;(#,##0.000);");
                string gtype = dt.Rows[i]["prgval"].ToString();
                double ratio = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtratio")).Text.Trim().Replace("%", ""));
                string percnt = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtratio")).Text.Trim().Contains("%") ? "%" : "";
                if (Gcode == "01003" || Gcode == "01004")
                {

                    Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }



                //Common
                dt.Rows[i]["percnt"] = percnt;

                switch (Gcode)
                {


                    case "02006": //Total Land Area (According to FAR)                   
                        totallarea = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = ratio * totallarea;
                        break;


                    case "02007": //Residential Area (Every Floor)                 
                        totallarea = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea);
                        break;

                    case "02009": //(Floor Number)                
                        totallarea = dt.Select("prgcod='02006'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02006'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = ratio == 0.00 ? 0.00 : totallarea / ratio;
                        break;

                    case "02011": //(Construcion Area)                
                        totallarea = dt.Select("prgcod='02007'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02007'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02012": //(Foundation)                
                        totallarea = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;



                    case "02032": //(Basement-1)                
                        totallarea = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02035": //(Basement-2)                
                        totallarea = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;

                    case "02037": //(Semi Basement)                
                        totallarea = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02038": //(Ground Floor)                
                        totallarea = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;

                    case "02039": //(Belconi)                
                        totallarea = dt.Select("prgcod='02006'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02006'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02040": //(Roof top)                
                        totallarea = dt.Select("prgcod='02003'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02003'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = (percnt == "%") ? (ratio * totallarea * .01) : (ratio * totallarea); ;
                        break;


                    case "02090": //(Total Construction Area)                


                        DataView dv = dt.Copy().DefaultView;
                        dv.RowFilter = ("prgcod>='02011' and prgcod<='02089'");
                        DataTable dttcon = dv.ToTable();

                        double tconsarea = Convert.ToDouble((Convert.IsDBNull(dttcon.Compute("Sum(total)", "")) ?
                    0.00 : dttcon.Compute("Sum(total)", "")));
                        //totallarea = dt.Select("prgcod='02007'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02007'")[0]["total"]);
                        dt.Rows[i]["prgdesc1"] = "";
                        dt.Rows[i]["ratio"] = 0;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = tconsarea;
                        break;


                    case "14005": //Saleable Area(Common)                
                        double conarea2 = dt.Select("prgcod='02011'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='02011'")[0]["total"]);
                        double comarea = dt.Select("prgcod='14005'").Length == 0 ? 0.00 : Convert.ToDouble("0" + dt.Select("prgcod='14005'")[0]["prgdesc1"]);
                      //  dt.Rows[i]["prgdesc1"] = totallarea;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = 0;
                        dt.Rows[i]["total"] = Math.Round(((comarea * conarea2*0.01)+ conarea2),0) ;
                        break;



                    default:
                        dt.Rows[i]["prgdesc1"] = Gvalue;
                        dt.Rows[i]["ratio"] = ratio;
                        dt.Rows[i]["buildarea"] = buildarea;
                        dt.Rows[i]["total"] = gtype == "N" ? ratio * ASTUtility.StrPosOrNagative(Gvalue) : 0;
                        break;






                }


            }

            Session["tblprogeninfo"] = dt;
            this.gvProjectInfo.DataSource = dt;
            this.gvProjectInfo.DataBind();
            this.GridTextDDLVisible();



        }
        protected void lUpdatProInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

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


                string ratio = Convert.ToDouble("0" + ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtratio")).Text.Trim().Replace("%", "")).ToString();
                string percnt = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtratio")).Text.Trim().Contains("%") ? "%" : "";
                string total = Convert.ToDouble("0" + ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvtotal")).Text.Trim()).ToString();


                result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSORUPLDPRJINF", pactcode, Gcode, gtype, Gvalue, fpactcode, buildarea, ratio, percnt, total, "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = feaData.ErrorObject["Msg"].ToString();
                    return;
                }

            }


            string pcdate = this.txtDate.Text.Trim();
            result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSOUPPCDATE", fpactcode, pcdate, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private string GetComConArea()
        {
            string conarea = "";
            string comcod = this.GetComCode();
            switch (comcod)
            {

                case "3101":
                case "3353":// Manama
                    conarea = "02090";
                    break;

                default:
                    break;




            }

            return conarea;

        }

        private void ShowRevenue()
        {
            //sircode like '0[89]%'  or  sircode like '1[0-9]%'

            Session.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = (this.rbtnList1.SelectedIndex == 1) ? "infcod like '51%'" : (this.rbtnList1.SelectedIndex == 2) ? "infcod like '5[2-5]%'" : "infcod like '5[67]%'";
            string conarea = this.GetComConArea();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROSALES", pactcode, Code, conarea, "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaPrj.DataSource = null;
                this.gvFeaPrj.DataBind();
                return;
            }
            Session["tblfeaprj"] = ds2.Tables[0];
            this.Data_Bind();
        }

        private void ShowBankInterest()
        {
            //sircode like '0[89]%'  or  sircode like '1[0-9]%'

            Session.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
           // string Code =  "5[2-5]%'" ;
          
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "SHOWBANKINTEREST", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvBankIn.DataSource = null;
                this.gvBankIn.DataBind();
                return;
            }
            Session["tblfeaprj"] = ds2.Tables[0];
            this.Data_Bind();
        }

        
        private string GetCompProOnSaleorCost()
        {

            string comcod = this.GetComCode();
            string proonsalorcost = "";
            switch (comcod)
            {
                case "1205"://P2P Construction
                case "3351"://Wecon Properties
                case "3352"://P2P 360
                    proonsalorcost = "proonSales";
                    break;


                default:
                    break;



            }

            return proonsalorcost;
        }
        private string CallTypeFeasibility()
        {
            string comcod = this.GetComCode();
            string callType = "";
            switch(comcod)
            {
                case "3353":
                case "3101":
                    callType = "RPTPROJECTFEASIBILITIESMANAMA";
                    break;

                default:
                    callType = "RPTPROJECTFEASIBILITY";
                    break;
            }
            return callType;
        }
        private void ShowReport()
        {
            Session.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string proonsalorcost = this.GetCompProOnSaleorCost();
            string callType = this.CallTypeFeasibility();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", callType, pactcode, proonsalorcost, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaPrjRep.DataSource = null;
                this.gvFeaPrjRep.DataBind();
                return;
            }
            if(comcod=="3353" || comcod=="3101")
            {
                Session["tblfeaprj"] = ds2.Tables[0];                
            }
            else
            {
                Session["tblfeaprj"] = this.HiddenSameData(ds2.Tables[0]);
            }
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
            int rindex = this.rbtnList1.SelectedIndex;
            DataTable dt = (DataTable)Session["tblfeaprj"];
            string comcod = this.GetComCode();
            switch (rindex)
            {

                case 1:

                    this.gvFeaPrj.DataSource = dt;
                    this.gvFeaPrj.DataBind();
                    this.FooterCal();
                    break;

                case 2:
                    this.gvFeaPrjC.DataSource = dt;
                    this.gvFeaPrjC.DataBind();
                    this.FooterCal();
                    break;

                case 3:
                    this.gvFeaLOwner.DataSource = dt;
                    this.gvFeaLOwner.DataBind();
                    this.FooterCal();
                    break;

                case 4:
                    if(comcod=="3353" || comcod=="3101")
                    {
                        this.gvFeaPrjRepManama.Visible = true;
                        this.gvFeaPrjRepManama.DataSource = dt;
                        this.gvFeaPrjRepManama.DataBind();
                    }
                    else
                    {
                        this.gvFeaPrjRep.Visible = true;
                        this.gvFeaPrjRep.DataSource = dt;
                        this.gvFeaPrjRep.DataBind();
                    }                   
                    break;

                case 6:
                    this.gvBankIn.DataSource = dt;
                    this.gvBankIn.DataBind();


                    string infcod, ninfcod;
                    for (int k = 0; k < dt.Rows.Count - 1; k++)
                    {

                        infcod = dt.Rows[k]["infcod"].ToString();
                        ninfcod = dt.Rows[k + 1]["infcod"].ToString();

                        if (infcod == ninfcod)
                        {
                            ((LinkButton)this.gvBankIn.Rows[k].FindControl("lnkbtnAdd")).Style["display"] = "none";

                        }
                    }



                    this.FooterCal();
                    break;




            }


        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];
            if (dt.Rows.Count == 0)
                return;
            double Stotalsize, stotalAmt;
            int rindex = this.rbtnList1.SelectedIndex;
            switch (rindex)
            {

                case 1:
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "infcod like('51%')";
                    DataTable dts = dv.ToTable().Copy();
                    //Size
                    dv = dts.DefaultView;
                    dv.RowFilter = ("infcod like '510100101%'");
                    DataTable dtsize = dv.ToTable();


                    Stotalsize = Convert.ToDouble((Convert.IsDBNull(dtsize.Compute("Sum(tsize)", "")) ?
                    0.00 : dtsize.Compute("Sum(tsize)", "")));
                    stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                    0.00 : dt.Compute("Sum(amt)", "")));
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFTsize")).Text = Stotalsize.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFSratepsft")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
                    break;
                case 2:
                    //((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFsalrate")).Text =Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salrate)", "")) ?
                    //0.00 : dt.Compute("Sum(salrate)", ""))).ToString("#,##0.00;(#,##0.00); ") ;

                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                        0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case 3:
                    Stotalsize = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsize)", "")) ?
                    0.00 : dt.Compute("Sum(tsize)", "")));
                    stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                    0.00 : dt.Compute("Sum(amt)", "")));
                    ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFTsizel")).Text = Stotalsize.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFsalratel")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
                    ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFAmtl")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
                    break;


                case 6:

                    ((Label)this.gvBankIn.FooterRow.FindControl("lgvFAmtbi")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(intamt)", "")) ?
                    0.00 : dt.Compute("Sum(intamt)", ""))).ToString("#,##0; -#,##0; ");
                    break;
            }

        }


        protected void lbtnFUpdateSales_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.UpdateProjectSaleAndCost();
        }

        private void UpdateProjectSaleAndCost()
        {

            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblfeaprj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string ResCode = dt.Rows[i]["infcod"].ToString();
                string Size = Convert.ToDouble(dt.Rows[i]["rsize"]).ToString();
                string Number = Convert.ToDouble(dt.Rows[i]["number"]).ToString();
                string Quantity = Convert.ToDouble(dt.Rows[i]["tsize"]).ToString();
                double Amt = Convert.ToDouble(dt.Rows[i]["amt"]);
                string salrate = Convert.ToDouble(dt.Rows[i]["salrate"]).ToString();

                if (Amt > 0)
                {
                    //feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPDATEFEAPRJCT", PactCode, ResCode, Size, Number, Amt, salrate, "", "", "", "", "", "", "","","");
                    bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPDATELPFEAPRJCT", PactCode, ResCode, Size, Number, Amt.ToString(), salrate, Quantity, "", "", "", "", "", "", "", "");

                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = feaData.ErrorObject["Msg"].ToString();
                    }
                }


            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.ShowRevenue();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void gvFeaPrj_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)Session["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvFeaPrj.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "DELETEITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod not in('" + Itemcode + "')";
            Session.Remove("tblfeaprj");
            Session["tblfeaprj"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];
            for (int i = 0; i < this.gvFeaPrj.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrj.Rows[i].FindControl("lblgvItmCod")).Text.Trim();


                double number = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvnum")).Text.Trim());


                double tsize = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgtsize")).Text.Trim());
                double salrate = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvsalrate")).Text.Trim());

                double size = (number > 0) ? (tsize / number) : 0.00;
                double Amt = tsize * salrate;
                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");

                dr1[0]["rsize"] = size;
                dr1[0]["number"] = number;
                dr1[0]["tsize"] = tsize;
                dr1[0]["amt"] = Amt;
                dr1[0]["salrate"] = salrate;
            }
            Session["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];

            for (int i = 0; i < this.gvFeaPrjC.Rows.Count; i++)
            {

                string Code = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lblgvItmCodc")).Text.Trim();
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("lgtsizec")).Text.Trim());
                double salrate = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgsalratec")).Text.Trim());
                double Amt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvAmtc")).Text.Trim());

                salrate = Amt > 0 ? (qty > 0 ? Amt / qty : 0.00) : salrate;
                Amt = Amt > 0 ? Amt : qty * salrate;
                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");

                dr1[0]["amt"] = Amt;
                dr1[0]["salrate"] = salrate;
                dr1[0]["tsize"] = qty.ToString();


                //dr1[0]["salrate"] = salrate;
            }

            DataTable dto = dt.Copy();
            DataView dv = dto.DefaultView;



            string comcod = this.GetComCode();
            switch (comcod)
            {

                case "3101":
                case "3353":// Manama
                    dv.RowFilter = ("infcod >= '520100101001' and  infcod<='520100401030'");
                    DataTable dtm = dv.ToTable();
                    double toconsamt = Convert.ToDouble((Convert.IsDBNull(dtm.Compute("Sum(amt)", "")) ? 0.00 : dtm.Compute("Sum(amt)", "")));

                    double toovrhead = Convert.ToDouble(dt.Select("infcod='520100402025'")[0]["amt"]);
                    dt.Select("infcod='520100402025'")[0]["tsize"] = 1;
                    dt.Select("infcod='520100402025'")[0]["salrate"] = toovrhead > 0 ? toovrhead : toconsamt * 12 * .01;
                    dt.Select("infcod='520100402025'")[0]["amt"] = toovrhead > 0 ? toovrhead : toconsamt * 12 * .01;
                    break;

                default:
                    break;




            }


            Session["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void lbtnfUpdateCost_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.UpdateProjectSaleAndCost();
        }
        protected void gvFeaPrjC_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)Session["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvFeaPrjC.Rows[e.RowIndex].FindControl("lblgvItmCodc")).Text.Trim();

            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "DELETEITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod not in('" + Itemcode + "')";
            Session.Remove("tblfeaprj");
            Session["tblfeaprj"] = dv.ToTable();
            this.Data_Bind();








            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnTotalLOwner_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblfeaprj"];
            for (int i = 0; i < this.gvFeaLOwner.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaLOwner.Rows[i].FindControl("lblgvItmCodl")).Text.Trim();
                double number = Convert.ToDouble("0" + ((TextBox)this.gvFeaLOwner.Rows[i].FindControl("txtgvnuml")).Text.Trim());
                double tsize = Convert.ToDouble("0" + ((TextBox)this.gvFeaLOwner.Rows[i].FindControl("txtgtsizel")).Text.Trim());
                double salrate = Convert.ToDouble("0" + ((TextBox)this.gvFeaLOwner.Rows[i].FindControl("txtgsalratel")).Text.Trim());

                double size = (number > 0) ? (tsize / number) : 0.00;
                double Amt = tsize * salrate;
                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["rsize"] = size;
                dr1[0]["number"] = number;
                dr1[0]["tsize"] = tsize;
                dr1[0]["amt"] = Amt;
                dr1[0]["salrate"] = salrate;
            }

            Session["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void lbtnfUpdateLOwner_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.UpdateProjectSaleAndCost();
        }
        protected void gvFeaLOwner_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)Session["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvFeaLOwner.Rows[e.RowIndex].FindControl("lblgvItmCodl")).Text.Trim();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "DELETEITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod not in('" + Itemcode + "')";
            Session.Remove("tblfeaprj");
            Session["tblfeaprj"] = dv.ToTable();
            this.Data_Bind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        protected void gvFeaPrjRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lgvgroupdesc");
                Label ToSize = (Label)e.Row.FindControl("lgtsizecRep");
                Label RatepSft = (Label)e.Row.FindControl("lgsalraterep");
                Label amt = (Label)e.Row.FindControl("lgvAmtrep");
                Label per = (Label)e.Row.FindControl("lgvper");
                Label lgvgroupdesc = (Label)e.Row.FindControl("lgvgroupdesc");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 5) == "AAAAA")
                {
                    groupdesc.Font.Bold = true;

                    groupdesc.ForeColor = Color.Green;
                    ToSize.ForeColor = Color.Green;
                    RatepSft.ForeColor = Color.Green;
                    amt.ForeColor = Color.Green;
                    per.ForeColor = Color.Green;

                    ToSize.Font.Bold = true;
                    RatepSft.Font.Bold = true;
                    amt.Font.Bold = true;
                    per.Font.Bold = true;

                    groupdesc.Style.Add("text-align", "right");
                }


                if (ASTUtility.Right(code, 5) == "00000")
                {

                    lgvgroupdesc.Attributes["style"] = "color:blue; font-weight:bold;";
                    ToSize.ForeColor = Color.Blue;
                    RatepSft.ForeColor = Color.Blue;
                    amt.ForeColor = Color.Blue;
                    per.ForeColor = Color.Blue;

                    ToSize.Font.Bold = true;
                    RatepSft.Font.Bold = true;
                    amt.Font.Bold = true;
                    per.Font.Bold = true;

                    e.Row.Attributes["style"] = "background-color:#CCECC3; font-weight:bold;";

                }

            }

        }
        protected void lgtsizec_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];

            for (int i = 0; i < this.gvFeaPrjC.Rows.Count; i++)
            {

                string Code = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lblgvItmCodc")).Text.Trim();
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("lgtsizec")).Text.Trim());
                double salrate = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgsalratec")).Text.Trim());
                double Amt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvAmtc")).Text.Trim());


                Amt = qty * salrate;

                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");

                dr1[0]["amt"] = Amt;
                dr1[0]["salrate"] = salrate;
                dr1[0]["tsize"] = qty.ToString();


                //dr1[0]["salrate"] = salrate;
            }
            Session["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void txtgsalratec_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];

            for (int i = 0; i < this.gvFeaPrjC.Rows.Count; i++)
            {

                string Code = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lblgvItmCodc")).Text.Trim();
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("lgtsizec")).Text.Trim());
                double salrate = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgsalratec")).Text.Trim());
                double Amt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvAmtc")).Text.Trim());


                salrate = Amt > 0 ? Amt / qty : salrate;
                Amt = Amt > 0 ? Amt : qty * salrate;

                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");

                dr1[0]["amt"] = Amt;
                dr1[0]["salrate"] = salrate;
                dr1[0]["tsize"] = qty.ToString();


                //dr1[0]["salrate"] = salrate;
            }
            Session["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void txtgvAmtc_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprj"];

            for (int i = 0; i < this.gvFeaPrjC.Rows.Count; i++)
            {

                string Code = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lblgvItmCodc")).Text.Trim();
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("lgtsizec")).Text.Trim());
                double salrate = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgsalratec")).Text.Trim());
                double Amt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvAmtc")).Text.Trim());


                salrate = Amt > 0 ? Amt / qty : salrate;
                Amt = Amt > 0 ? Amt : qty * salrate;

                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");

                dr1[0]["amt"] = Amt;
                dr1[0]["salrate"] = salrate;
                dr1[0]["tsize"] = qty.ToString();


                //dr1[0]["salrate"] = salrate;
            }
            Session["tblfeaprj"] = dt;
            this.Data_Bind();




        }


        protected void gvProjectInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgcod")).ToString();
                string prgval = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgval")).ToString();
                TextBox txtgvVal = (TextBox)e.Row.FindControl("txtgvVal");


                if (prgval == "N")
                {
                    txtgvVal.Attributes["style"] = "text-align:right;";



                }
                if (code == "")
                    return;


                if (code.Substring(2) == "000")
                {
                    e.Row.Attributes["style"] = " background-color:green; color:white; font-weight:bold; font-size:12px;";
                }


                else if (code == "02017" || code == "02027" || code == "02029" || code == "02031" || code == "12003" || code == "13001" || code == "13002" || code == "13003" || code == "13101" || code == "13105" || code == "13106" || code == "13107" || code == "13108" || code == "13109" || code == "14002" || code == "25001" || code == "25003" || code == "25004" || code == "25005" || code == "25011" || code == "25012")
                {
                    e.Row.Attributes["style"] = " background-color:#80ffff; color:#000000; ";

                }

            }
        }

        protected void btnDelbint_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComCode();
            DataTable dt = (DataTable)Session["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();          
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string Itemcode = dt.Rows[RowIndex]["infcod"].ToString();// ((Label)this.gvBankIn.Rows[RowIndex].FindControl("lblgvItmCodbi")).Text.Trim();
            string seq = dt.Rows[RowIndex]["seq"].ToString();

            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "DELETEITEBANKINT", PactCode, Itemcode, seq, "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod not in('" + Itemcode + "')";
            Session.Remove("tblfeaprj");
            Session["tblfeaprj"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void lbtnTotalCostbi_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblfeaprj"];
            int i = 0;
            double twlvemonth = 12, intamt=0.00;
            foreach (GridViewRow gv1  in gvBankIn.Rows)
            {

               
                double prinamt = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvprincipalbi")).Text.Trim());
                double irate = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvintratebi")).Text.Trim());
                double imonth = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvimontbi")).Text.Trim());
                double gintamt = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvAmtbi")).Text.Trim());
                intamt = prinamt == 0 ? 0.00 :Math.Round(((irate * 0.01 * imonth * prinamt) / twlvemonth),0);
                intamt = gintamt > 0 ? gintamt : intamt;
                dt.Rows[i]["prinamt"] = prinamt;
                dt.Rows[i]["irate"] = irate;
                dt.Rows[i]["imonth"] = imonth;
                dt.Rows[i]["intamt"] = intamt;
                i++;


            }
            Session["tblfeaprj"] = dt;
            this.Data_Bind();

        }

        protected void lbtnfUpdateCostbi_Click(object sender, EventArgs e)
        {


            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataTable dt = ((DataTable)Session["tblfeaprj"]).Copy();
            dt.Columns.Remove("infdesc");
            dt.Columns.Remove("unit");
            DataView dv = dt.DefaultView;
            dv.RowFilter=("intamt>0");

            
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dv.ToTable());
            ds1.Tables[0].TableName = "tbl1";
            string xml = ds1.GetXml();          
            bool result = feaData.UpdateXmlTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPLNINTEREST", ds1, null, null, PactCode, "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = feaData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;


            }

             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);








            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {


            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            DataTable dt = (DataTable)Session["tblfeaprj"];
            string infcod = dt.Rows[Rowindex]["infcod"].ToString();
            int seq;
            DataRow dr1 = dt.Rows[Rowindex];//.Select("infcod='" + infcod + "'");
            //if (dr1.Length > 0)
            //{

                seq =Convert.ToInt32(dr1["seq"].ToString());
                seq++;
              //  dr1["seq"] = seq;
                dt.ImportRow(dr1);
            int rcount = dt.Rows.Count-1;
            dt.Rows[rcount]["seq"] = seq;
               // dt.ImportRow(dr1);

            //  }
            DataView dv = dt.DefaultView;
            dv.Sort = "infcod, seq";
            dt = dv.ToTable();

            Session["tblfeaprj"] = dt;
            this.Data_Bind();
        }

        protected void gvBankIn_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string infcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString().Trim();

                if (infcod.Length == 0)
                    return;

                if (infcod == "520100402001")
                {
                    e.Row.FindControl("lnkbtnAdd").Visible = true;

                }
                else
                {

                    e.Row.FindControl("lnkbtnAdd").Visible = false;

                }
            }
        }

        protected void gvFeaPrjRepManama_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string prgcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgcod")).ToString().Trim();

                if (prgcod.Length == 0)
                    return;

                if (ASTUtility.Right(prgcod, 3) == "AAA" || ASTUtility.Right(prgcod, 3) == "BAA")
                {
                    e.Row.Attributes["style"] = " background-color:#e62284; color:white; font-weight:bold; font-size:12px;";

                }
                else
                {

                }
            }
        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string prjcode = "";
            if (AsyncFileUpload1.HasFile)
            {
                prjcode = this.ddlProjectName.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/PROJECT/") + prjcode + random + extension);

                Url = "~/Upload/PROJECT/" + prjcode + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                dt.Rows.Add(comcod, prjcode, Url);
            }

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = feaData.UpdateXmlTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "UPDATE_PROJECT_FILES", ds1, null, null, prjcode, "", "", "", "", "");

            if (result == true)
            {
                this.lblMesg.Text = " Successfully Updated ";
                this.ShowProjectFiles();

            }
            else
            {
                string filePath = Server.MapPath("~/");
                System.IO.File.Delete(filePath + Url.Replace("~", ""));
            }


        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string actcode = ((Label)this.ListViewEmpAll.Items[j].FindControl("actcode")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    DataRow dr = dt.Rows[j];
                    dr.Delete();
                    DataSet ds1 = new DataSet("ds1");
                    ds1.Tables.Add(dt);
                    ds1.Tables[0].TableName = "tbl1";
                    bool result = feaData.UpdateXmlTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "UPDATE_PROJECT_FILES", ds1, null, null, actcode, "", "", "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + filesname.Replace("~", ""));
                        this.lblMesg.Text = " Files Removed ";
                        this.ShowProjectFiles();
                    }
                }

            }

        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                System.Web.UI.WebControls.Image imgname = (System.Web.UI.WebControls.Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;
                }
            }

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue;
            string rmrks = this.legammsg.Text.ToString().Trim();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "PROGRESS_RMRKS", rmrks, pactcode);

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
    }
}