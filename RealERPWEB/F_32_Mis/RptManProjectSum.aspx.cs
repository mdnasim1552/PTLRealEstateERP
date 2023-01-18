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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_32_Mis
{
    public partial class RptManProjectSum : System.Web.UI.Page
    {
        ProcessAccess prjData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Summary";
                this.lbtnShow_Click(null, null);
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }



        protected void lbtnShow_Click(object sender, EventArgs e)
        {


            Session.Remove("tbPrjStatus");
            string comcod = this.GetComeCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_MIS04", "RPTPROJECTSUMMARY", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvprostatus.DataSource = null;
                this.gvprostatus.DataBind();
                return;

            }

            Session["tbPrjStatus"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string catmcode = dt1.Rows[0]["catmcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["catmcode"].ToString() == catmcode)
                {

                    dt1.Rows[j]["catmdesc"] = "";


                }



                catmcode = dt1.Rows[j]["catmcode"].ToString();

            }








            return dt1;


        }


        private void Data_Bind()
        {
            this.gvprostatus.DataSource = (DataTable)Session["tbPrjStatus"];
            this.gvprostatus.DataBind();
            this.FooteCalculation();
        }


        private void FooteCalculation()
        {
            DataTable dt1 = (DataTable)Session["tbPrjStatus"];
            if (dt1.Rows.Count == 0)
                return;


            double fbgdamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(bgdamt)", "")) ? 0.00 : dt1.Compute("sum(bgdamt)", "")));
            double adamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(adamt)", "")) ? 0.00 : dt1.Compute("sum(adamt)", "")));
            double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tbgdamt)", "")) ? 0.00 : dt1.Compute("sum(tbgdamt)", "")));

            double tacamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(toacamt)", "")) ? 0.00 : dt1.Compute("sum(toacamt)", "")));
            double examt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(examt)", "")) ? 0.00 : dt1.Compute("sum(examt)", "")));
            double mplanat = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(mplanat)", "")) ? 0.00 : dt1.Compute("sum(mplanat)", "")));
            double salbgd = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tosalval)", "")) ? 0.00 : dt1.Compute("sum(tosalval)", "")));
            double salamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(salamt)", "")) ? 0.00 : dt1.Compute("sum(salamt)", "")));

            double collbgd = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(collbgd)", "")) ? 0.00 : dt1.Compute("sum(collbgd)", "")));
            double collamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(collamt)", "")) ? 0.00 : dt1.Compute("sum(collamt)", "")));


            double perontw = bgdamt == 0 ? 0.00 : (mplanat / bgdamt);
            double perontac = bgdamt == 0 ? 0.00 : (examt / bgdamt);


            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFtfbgdamt")).Text = fbgdamt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFtadbgdamt")).Text = adamt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFtbgdamt")).Text = bgdamt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFtacamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(toacamt)", "")) ?
                            0.00 : dt1.Compute("sum(toacamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFperontw")).Text = perontw.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFperontac")).Text = perontac.ToString("#,##0.00;(#,##0.00); ");

            //lgvFperontw


            //    lgvFperontac

            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFcbgdamt")).Text = mplanat.ToString("#,##0;(#,##0); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFcactamt")).Text = examt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFtosalval")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tosalval)", "")) ?
                                    0.00 : dt1.Compute("sum(tosalval)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFsalamtl")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(salamt)", "")) ?
                                    0.00 : dt1.Compute("sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFcollbgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(collbgd)", "")) ?
                                    0.00 : dt1.Compute("sum(collbgd)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFcollamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(collamt)", "")) ?
                                    0.00 : dt1.Compute("sum(collamt)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFprincur")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(princur)", "")) ?
                                    0.00 : dt1.Compute("sum(princur)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFfuincur")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(fuincur)", "")) ?
                                    0.00 : dt1.Compute("sum(fuincur)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprostatus.FooterRow.FindControl("lgvFacInfamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(texwprincur)", "")) ?
                                   0.00 : dt1.Compute("sum(texwprincur)", ""))).ToString("#,##0;(#,##0); ");




            // Chart Image

            this.txttbgdamt.Text = Math.Round(bgdamt, 2).ToString();
            this.txttacamt.Text = Math.Round(tacamt, 2).ToString();

            this.txtperontw.Text = Math.Round(perontw, 2).ToString();
            this.txtperontac.Text = Math.Round(perontac, 2).ToString();

            this.txtbgdamt.Text = Math.Round(mplanat, 2).ToString();
            this.txtacamt.Text = Math.Round(examt, 2).ToString();

            this.txtsalbgd.Text = Math.Round(salbgd, 2).ToString();
            this.txtsalactual.Text = Math.Round(salamt, 2).ToString();


            this.txtcollbgd.Text = Math.Round(collbgd, 2).ToString();
            this.txtcollac.Text = Math.Round(collamt, 2).ToString();



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            // string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string DateFT = "As On Date: " + " " + date;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            DataTable dt = (DataTable)Session["tbPrjStatus"];
            var lst = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.Customer_ProjectSumm>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjectSummary", lst, null, null);

            //if (comcod == "3101" || comcod == "3333")
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_90_PF.RptIndvPfAlli", pflist, null, null);
            //}
            //else
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIndvPf", pflist, null, null);
            //}


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));

            //Rpt1.SetParameters(new ReportParameter("frmdate", "From: "+ frmdate));
            //Rpt1.SetParameters(new ReportParameter("todate", "To: " + todate));
            Rpt1.SetParameters(new ReportParameter("daterange", DateFT));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Project Status Summary"));

            //Rpt1.SetParameters(new ReportParameter("empname", empinfo.Rows[0]["name"].ToString()));
            //Rpt1.SetParameters(new ReportParameter("empid", empinfo.Rows[0]["idcard"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }



        private void RptPrjStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbPrjStatus"];
            string frmdate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectStatus>();

            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptProjectStatusLand", list, null, null);

            DataTable dt1 = (DataTable)ViewState["tblinterest"];
            double tchrgedpro, tpaidpro, nettotal;
            tchrgedpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interam)", "")) ? 0.00 : dt1.Compute("sum(interam)", "")));
            tpaidpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(inpaid)", "")) ? 0.00 : dt1.Compute("sum(inpaid)", "")));
            nettotal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netam)", "")) ? 0.00 : dt1.Compute("sum(netam)", "")));

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "Upto  " + Convert.ToDateTime(this.txtdate.Text).ToString("MMMM yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtthisonsal", "Month of " + Convert.ToDateTime(this.txtdate.Text).ToString("MMM yy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Project Status Report"));
            Rpt1.SetParameters(new ReportParameter("txtlandarea", (comcod == "3306") ? "Land Area(SFT/Katha)" : "Land Area(Katha)"));
            Rpt1.SetParameters(new ReportParameter("txtnofland", (comcod == "3306") ? "Nature of Product" : "Nature of Land"));
            Rpt1.SetParameters(new ReportParameter("txtCost", (comcod == "3306") ? "Appt. Price+Admin+Selling Exp." : "Const+Admin+Selling Exp."));
            Rpt1.SetParameters(new ReportParameter("txtonchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txttotalchrgetopro", tchrgedpro.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtonpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txttotalpaidtopro", tpaidpro.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtonnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtnettotal", nettotal.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            // ReportDocument rptsaldperiod = new ReportDocument();

            // rptsaldperiod=new RealERPRPT.R_32_Mis.RptProjectStatusLand();

            // TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            // rptComp.Text = comnam;
            // TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            // rptdate.Text = "Upto  " + Convert.ToDateTime(this.txtdate.Text).ToString("MMMM yyyy");
            // TextObject txtthisonsal = rptsaldperiod.ReportDefinition.ReportObjects["txtthisonsal"] as TextObject;
            // txtthisonsal.Text = "Month of " + Convert.ToDateTime(this.txtdate.Text).ToString("MMM yy");

            // DataTable dt1 = (DataTable)ViewState["tblinterest"];
            // double tchrgedpro, tpaidpro, nettotal;
            // tchrgedpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interam)", "")) ? 0.00 : dt1.Compute("sum(interam)", "")));
            // tpaidpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(inpaid)", "")) ? 0.00 : dt1.Compute("sum(inpaid)", "")));
            // nettotal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netam)", "")) ? 0.00 : dt1.Compute("sum(netam)", "")));
            //// ViewState["tblinterest"] = ds1.Tables[1];

            // //(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"]


            //     TextObject txtonchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtonchrgetopro"] as TextObject;
            //     txtonchrgetopro.Text =(dt1.Rows.Count==0)?"":dt1.Select("pactcode='410000000000'").Length>0?Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["interam"]).ToString("#,##0;(#,##0);"):"";

            //    TextObject txtfinchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtfinchrgetopro"] as TextObject;
            //    txtfinchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //     TextObject txthovrchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txthovrchrgetopro"] as TextObject;
            //     txthovrchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //     TextObject txtlandchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtlandchrgetopro"] as TextObject;
            //     txtlandchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //     TextObject txttotalchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txttotalchrgetopro"] as TextObject;
            //     txttotalchrgetopro.Text = tchrgedpro.ToString("#,##0;(#,##0); ");


            //     TextObject txtonpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtonpaidtopro"] as TextObject;
            //     txtonpaidtopro.Text = (dt1.Rows.Count == 0) ? "" :dt1.Select("pactcode='410000000000'").Length>0?Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);"):"";
            //     TextObject txtfinpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtfinpaidtopro"] as TextObject;
            //     txtfinpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txthovrpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txthovrpaidtopro"] as TextObject;
            //     txthovrpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txtlandpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtlandpaidtopro"] as TextObject;
            //     txtlandpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txttotalpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txttotalpaidtopro"] as TextObject;
            //     txttotalpaidtopro.Text = tpaidpro.ToString("#,##0;(#,##0); ");

            //     TextObject txtonnet = rptsaldperiod.ReportDefinition.ReportObjects["txtonnet"] as TextObject;
            //     txtonnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txtfinneto = rptsaldperiod.ReportDefinition.ReportObjects["txtfinnet"] as TextObject;
            //     txtfinneto.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txthovrnet = rptsaldperiod.ReportDefinition.ReportObjects["txthovrnet"] as TextObject;
            //     txthovrnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txtlandnet = rptsaldperiod.ReportDefinition.ReportObjects["txtlandnet"] as TextObject;
            //     txtlandnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txtnettotal = rptsaldperiod.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
            //     txtnettotal.Text = nettotal.ToString("#,##0;(#,##0); ");

            // //string calltype = this.PrjCallType();

            // //TextObject rptCost = rptsaldperiod.ReportDefinition.ReportObjects["txtCost"] as TextObject;
            // //rptCost.Text = (ASTUtility.Left(comcod, 1) == "4") ? "Const+Admin+Selling Expen." : "Apt. Purchase+Admin+Selling";

            // TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            // rptsaldperiod.SetDataSource(dt);

            // //string comcod = hst["comcod"].ToString();

            // if (ConstantInfo.LogStatus == true)
            // {
            //     string eventtype = "Sales During the Peroid";
            //     string eventdesc = "Print Report";
            //     string eventdesc2 = "";
            //     bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            // }
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            // rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            // Session["Report1"] = rptsaldperiod;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void gvprostatus_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 2;
                cell01.Attributes["style"] = "font-weight:bold; font-size:14px; width:30px; background-color:#5CB85C; color:white; ";


                //TableCell cell02 = new TableCell();
                //cell02.Text = "";
                //cell02.HorizontalAlign = HorizontalAlign.Center;
                //cell02.ColumnSpan = 1;



                //TableCell cell03 = new TableCell();
                //cell03.Text = "";
                //cell03.HorizontalAlign = HorizontalAlign.Center;
                //cell03.ColumnSpan = 1;
                //cell03.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white;";


                TableCell cell04 = new TableCell();
                cell04.Text = "Total Cost";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 4;
                cell04.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white;";



                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 1;
                cell05.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";



                TableCell cell06 = new TableCell();
                cell06.Text = "Construction Progress";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 3;
                cell06.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white; ";


                TableCell cell07 = new TableCell();
                cell07.Text = "";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 1;
                cell07.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";




                TableCell cell08 = new TableCell();
                cell08.Text = " Construction Cost";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 3;
                cell08.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white; ";





                TableCell cell09 = new TableCell();
                cell09.Text = "";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.ColumnSpan = 1;
                cell09.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";




                TableCell cell13 = new TableCell();
                cell13.Text = " Inflation";
                cell13.HorizontalAlign = HorizontalAlign.Center;
                cell13.ColumnSpan = 2;
                cell13.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white; ";


                TableCell cell14 = new TableCell();
                cell14.Text = "";
                cell14.HorizontalAlign = HorizontalAlign.Center;
                cell14.ColumnSpan = 1;
                cell14.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";





                TableCell cell10 = new TableCell();
                cell10.Text = "Sales";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.ColumnSpan = 3;
                cell10.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white; ";

                TableCell cell11 = new TableCell();
                cell11.Text = "";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.ColumnSpan = 1;
                cell11.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";




                TableCell cell12 = new TableCell();
                cell12.Text = "Collection";
                cell12.HorizontalAlign = HorizontalAlign.Center;
                cell12.ColumnSpan = 3;
                cell12.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white;";





                gvrow.Cells.Add(cell01);
                //  gvrow.Cells.Add(cell02);
                // gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);
                gvrow.Cells.Add(cell09);
                gvrow.Cells.Add(cell13);
                gvrow.Cells.Add(cell14);
                gvrow.Cells.Add(cell10);
                gvrow.Cells.Add(cell11);
                gvrow.Cells.Add(cell12);




                gvprostatus.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvprostatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            HyperLink hlnkBudgetAmt = (HyperLink)e.Row.FindControl("hlnkgvBudgetAmt");
            HyperLink hylbudgetp = (HyperLink)e.Row.FindControl("hylbudgetp");
            HyperLink hylbudgetCost = (HyperLink)e.Row.FindControl("hylbudgetCost");
            HyperLink hylbudgetSales = (HyperLink)e.Row.FindControl("hylbudgetSales");
            HyperLink hylActualSales = (HyperLink)e.Row.FindControl("hylActualSales");

            HyperLink hlnkprincur = (HyperLink)e.Row.FindControl("hlnkprincur");
            HyperLink hlnkfuincur = (HyperLink)e.Row.FindControl("hlnkfuincur");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();




            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string mPACTCODE = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

            string mTRNDAT1 = this.txtdate.Text;
            string frmdate = "01-" + mTRNDAT1.Substring(3);
            hlnkBudgetAmt.Style.Add("color", "blue");
            string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();



            hylbudgetp.NavigateUrl = "~/F_32_Mis/LinkConstruProgress.aspx?Type=Comcod&pactcode=" + mPACTCODE + "&pactdesc=" + pactdesc + "&Date=" + Convert.ToDateTime(txtdate.Text).ToString("dd-MMM-yyy");
            hylbudgetCost.NavigateUrl = "~/F_32_Mis/LinkConstruProgress.aspx?Type=Comcod&pactcode=" + mPACTCODE + "&pactdesc=" + pactdesc + "&Date=" + Convert.ToDateTime(txtdate.Text).ToString("dd-MMM-yyy");
            hylbudgetSales.NavigateUrl = "~/F_22_Sal/LinkMktEntryUnit.aspx?Type=Comcod&pactcode=" + "18" + ASTUtility.Right(mPACTCODE, 10) + "&pactdesc=" + pactdesc + "&Date=" + Convert.ToDateTime(txtdate.Text).ToString("dd-MMM-yyy");
            hylActualSales.NavigateUrl = "~/F_22_Sal/LinkRptSaleSoldunsoldUnit.aspx?Type=soldunsold" + "&pactcode=" + "18" + ASTUtility.Right(mPACTCODE, 10) + "&pactdesc=" + pactdesc + "&Date=" + Convert.ToDateTime(txtdate.Text).ToString("dd-MMM-yyy");
            hlnkprincur.NavigateUrl = "~/F_32_Mis/RptPrjCostPerSFT.aspx?Type=RemainingCost&prjcode=" + mPACTCODE;
            hlnkfuincur.NavigateUrl = "~/F_32_Mis/RptPrjCostPerSFT.aspx?Type=RemainingCost&prjcode=" + mPACTCODE;






            //"AccFinalReports.aspx?RepType=IS="
        }
    }
}