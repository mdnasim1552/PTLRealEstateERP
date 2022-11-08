﻿using System;
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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
using RealERPEntity;
using RealERPWEB.Service;
namespace RealERPWEB.F_22_Sal
{
   
   


    public partial class MktGrandNoteSheet : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        Common objcom = new Common();
        AutoCompleted AutoData = new AutoCompleted();

        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
        

                 

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
      
                string TypeDesc = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Grand Note Sheet";

                Session.Remove("Unit");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string finsdate = Convert.ToDateTime(date).AddMonths(1).ToString("dd-MMM-yyyy");

                this.txtcoffBookingdate.Text = date;
                this.txtcoffdownpaydate.Text = date;
                this.txtcoffinsdate.Text = finsdate;

                this.GetProjectName();
                this.GetProspective();
                this.getInstallment();              
                this.gvSpayment.Columns[0].Visible = false;


                string qgeno = this.Request.QueryString["genno"] ?? "";
                if (qgeno.Length > 0)
                {
                    this.lnkbtnPrevious_Click(null, null);
                    this.lbtnOk_Click(null, null);

                }



            }

        }



        private void getInstallment()
        {
            try
            {
                Session.Remove("tblinstallment");
                string comcod = this.GetCompCode();
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETINSTALLMENT", "", "", "", "", "", "", "", "", "");
                Session["tblinstallment"] = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode>(); 
                ds1.Dispose();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }

        }




        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
        private void GetProspective()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            string empid = hst["empid"].ToString();
            string Type = "Management";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPROSPECTIVE", txtSProject, empid, Type, "", "", "", "", "", "");
            this.ddlprospective.DataTextField = "prosdesc";
            this.ddlprospective.DataValueField = "proscode";
            this.ddlprospective.DataSource = ds1.Tables[0];
            this.ddlprospective.DataBind();
            ds1.Dispose();

            //Session.Remove("tblprospective");
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //AutoData.GetRecAndPayto(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPROSPECTIVE", "", "", "", "", "", "", "", "", "");

        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                this.lblprevious.Visible = false;
                this.lnkbtnPrevious.Visible = false;
                this.ddlPrevious.Visible = false;
                this.ddlprospective.Enabled = false;

                this.LoadGrid();

                if (this.ddlPrevious.Items.Count > 0)
                    GetPreviousNoteSheetDetails();



            }
            else
            {
                this.lbtnOk.Text = "Ok";
                MultiView1.ActiveViewIndex = -1;
                this.ddlPrevious.Items.Clear();
                this.lbtnBack.Visible = false;
                this.lblprevious.Visible = true;
                this.lnkbtnPrevious.Visible = true;
                this.ddlPrevious.Visible = true;
                this.ddlprospective.Enabled = true;
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            //this.lblProjectdesc.Text = "";
            //this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }

        private void LoadGrid()
        {
            ViewState.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string notesheetno = "";
            if (this.ddlPrevious.Items.Count > 0)
                notesheetno = this.ddlPrevious.SelectedValue.ToString();


            string PactCode = this.ddlPrevious.Items.Count > 0 ? ((DataTable)ViewState["tblprenotesheet"]).Select("noteshtid='" + notesheetno + "'")[0]["pactcode"].ToString() : this.ddlProjectName.SelectedValue.ToString();


            string srchunit = "%" + this.txtsrchunit.Text.Trim() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "DETAILSIRINFINFORMATION", PactCode, srchunit, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvSpayment.DataSource = ds1.Tables[0];
            this.gvSpayment.DataBind();
            ViewState["tblData"] = ds1.Tables[0];

            for (int i = 0; i < gvSpayment.Rows.Count; i++)
            {
                string usircode = ((Label)gvSpayment.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSpayment.Rows[i].FindControl("lbtnusize");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = usircode;
            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string notesheetno = this.ddlPrevious.SelectedValue.ToString();
            string usircode = ((DataTable)ViewState["tblprenotesheet"]).Select("noteshtid='" + notesheetno + "'")[0]["usircode"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //  string usrid = hst["usrid"].ToString();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string pactcode = ((DataTable)ViewState["tblprenotesheet"]).Select("noteshtid='" + notesheetno + "'")[0]["pactcode"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPRENOTESHEETDETINFO", pactcode, usircode, notesheetno, "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            


            double uzize, bfv, bpv, uamt, pamt, utility, others, bfvpsft, bpvpsft, bpowbpart, intratio, noofemi,
                cofffv, coffpv, coffpamt, coffutility, coffothers, cofffvpsft, coffpvpsft, coffpowbpart, coffnoofemi, revfv, revpv,
                revpamt, revutility, revothers, revfvpsft, revpvpsft, revpowbpart, revnoofemi;

            uzize =
            bfv = lstb.Sum(l => l.fv);
            bpv = lstb.Sum(l => l.pv);
            uzize = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]);
            pamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["pamt"]);
            utility = Convert.ToDouble(ds1.Tables[0].Rows[0]["utility"]);
            others = Convert.ToDouble(ds1.Tables[0].Rows[0]["others"]);
            intratio = Convert.ToDouble(ds1.Tables[0].Rows[0]["intratio"]);
            noofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]);
            bfvpsft = ((uzize > 0) ? ((bfv - pamt - utility - others) / uzize) : 0.00);
            bpowbpart = (12 + intratio) / 12;
            bpvpsft = Math.Round(bfvpsft / (Math.Pow(bpowbpart, noofemi)), 0);
            cofffv = lstcoff.Sum(l => l.fv);
            coffpv = lstcoff.Sum(l => l.pv);
            coffpamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]);
            coffutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]);
            coffothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]);
            coffnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]);
            //cofffvpsft = ((uzize > 0) ? ((cofffv - coffpamt - coffutility - coffothers) / uzize) : 0.00);
            //coffpowbpart = (12 + intratio) / 12;
            //coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, coffnoofemi)), 0);




          

            string area = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            string rate = Convert.ToDouble(ds1.Tables[0].Rows[0]["urate"]).ToString("#,##0;(#,##0);");
            string unitprice = Convert.ToDouble(ds1.Tables[0].Rows[0]["uamt"]).ToString("#,##0;(#,##0);");
            string parking = Convert.ToDouble(ds1.Tables[0].Rows[0]["pamt"]).ToString("#,##0;(#,##0);");
            string valutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["utility"]).ToString("#,##0;(#,##0);");
            string other = Convert.ToDouble(ds1.Tables[0].Rows[0]["others"]).ToString("#,##0;(#,##0);");
            string Total = Convert.ToDouble(ds1.Tables[0].Rows[0]["tunitamt"]).ToString("#,##0;(#,##0);");
            string bookingpercnt = Convert.ToDouble(ds1.Tables[0].Rows[0]["bookingper"]).ToString("#,##0;(#,##0);");
            string bookingmoney = Convert.ToDouble(ds1.Tables[0].Rows[0]["bookingam"]).ToString("#,##0;(#,##0);");
            string valnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]).ToString("#,##0;(#,##0);");
            string emi = Convert.ToDouble(ds1.Tables[0].Rows[0]["emi"]).ToString("#,##0;(#,##0);");
            string fvpsft = bfvpsft.ToString("#,##0;(#,##0);");
            string pvpersft = bpvpsft.ToString("#,##0;(#,##0);");




            string valcoffarea = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            string coffrate = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffurate"]).ToString("#,##0;(#,##0);");
            string coffunitprice = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffuamt"]).ToString("#,##0;(#,##0);");
            string cofffparking = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]).ToString("#,##0;(#,##0);");
            string valcoffutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]).ToString("#,##0;(#,##0);");
            string valcoffothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]).ToString("#,##0;(#,##0);");
            string coffTotal = Convert.ToDouble(ds1.Tables[0].Rows[0]["cofftunitamt"]).ToString("#,##0;(#,##0);");
            string coffbookinmpercnt = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffbookingper"]).ToString("#,##0;(#,##0);");
            string coffbookingam = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffbookingam"]).ToString("#,##0;(#,##0);");
            string coffnooffemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]).ToString("#,##0;(#,##0);");
            string coffemi = Convert.ToDouble("0"+this.lblvalcoffemi.InnerText).ToString("#,##0;(#,##0);");
            string cofffvpersft =Convert.ToDouble(this.lblvalcofffvpersft.InnerText).ToString("#,##0;(#,##0);");
            string coffpvpersft = Convert.ToDouble(this.lblvalcoffpvpersft.InnerText).ToString("#,##0;(#,##0);");


           

            

            LocalReport Rpt1 = new LocalReport();
            var lst1 = lstb;
            var lst2 = lstcoff;
           

            if (this.rbtnnoteType.SelectedIndex == 0)
            {


                //DataTable dtsummuary = (DataTable)ViewState["tblData"];
                //DataView dv1 = dtsummuary.DefaultView;
                //dv1.RowFilter = ("usircode ='" + usircode + "'");
                //dt = dv1.ToTable();
                //string Projectname =  this.ddlProjectName.SelectedItem.Text.Substring(13);

                //string Projectdesc = dt.Rows[0]["udesc"].ToString();
                //string Projectunit = dt.Rows[0]["munit"].ToString();
                DataTable dtsummuary = (DataTable)ViewState["tblData"];
                DataRow[] dr = dtsummuary.Select("usircode='" + usircode + "'");
                string Projectname = this.ddlProjectName.SelectedItem.Text.Substring(13);
                string Projectdesc = dr[0]["udesc"].ToString();
                string Projectunit = dr[0]["munit"].ToString();




                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptGrandNotesSheetSummary", lst1, null, null);
                Rpt1.EnableExternalImages = true;

                Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
                Rpt1.SetParameters(new ReportParameter("Projectdesc", Projectdesc));
                Rpt1.SetParameters(new ReportParameter("Projectunit", Projectunit));
                //Basic
                Rpt1.SetParameters(new ReportParameter("area", area));
                Rpt1.SetParameters(new ReportParameter("rate", rate));
                Rpt1.SetParameters(new ReportParameter("unitprice", unitprice));
                Rpt1.SetParameters(new ReportParameter("parking", parking));
                Rpt1.SetParameters(new ReportParameter("valutility", valutility));
                Rpt1.SetParameters(new ReportParameter("other", other));
                Rpt1.SetParameters(new ReportParameter("Total", Total));
                Rpt1.SetParameters(new ReportParameter("bookingpercnt", bookingpercnt));
                Rpt1.SetParameters(new ReportParameter("bookingmoney", bookingmoney));
                Rpt1.SetParameters(new ReportParameter("valnoofemi", valnoofemi));
                Rpt1.SetParameters(new ReportParameter("emi", emi));
                Rpt1.SetParameters(new ReportParameter("fvpsft", fvpsft));
                Rpt1.SetParameters(new ReportParameter("pvpersft", pvpersft));
                //customer
                Rpt1.SetParameters(new ReportParameter("valcoffarea", valcoffarea));
                Rpt1.SetParameters(new ReportParameter("coffrate", coffrate));
                Rpt1.SetParameters(new ReportParameter("coffunitprice", coffunitprice));
                Rpt1.SetParameters(new ReportParameter("cofffparking", cofffparking));
                Rpt1.SetParameters(new ReportParameter("valcoffutility", valcoffutility));
                Rpt1.SetParameters(new ReportParameter("valcoffothers", valcoffothers));
                Rpt1.SetParameters(new ReportParameter("coffTotal", coffTotal));
                Rpt1.SetParameters(new ReportParameter("coffbookinmpercnt", coffbookinmpercnt));
                Rpt1.SetParameters(new ReportParameter("coffbookingam", coffbookingam));

                Rpt1.SetParameters(new ReportParameter("coffnooffemi", coffnooffemi));
                Rpt1.SetParameters(new ReportParameter("coffemi", coffemi));
                Rpt1.SetParameters(new ReportParameter("cofffvpersft", cofffvpersft));
                Rpt1.SetParameters(new ReportParameter("coffpvpersft", coffpvpersft));
              



                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Grand Note Sheet Summary"));
                // Rpt1.SetParameters(new ReportParameter("projectName", projectName));

                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            else
            {
                DataTable dtsummuary = (DataTable)ViewState["tblData"];
                DataRow[] dr = dtsummuary.Select("usircode='" + usircode + "'");
                string Projectname = this.ddlProjectName.SelectedItem.Text.Substring(13);
                string Projectdesc = dr[0]["udesc"].ToString();
                string Projectunit = dr[0]["munit"].ToString();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptGrandNotesSheet", lst1, lst2, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
                Rpt1.SetParameters(new ReportParameter("Projectdesc", Projectdesc));
                Rpt1.SetParameters(new ReportParameter("Projectunit", Projectunit));
                Rpt1.SetParameters(new ReportParameter("area", area));
                Rpt1.SetParameters(new ReportParameter("rate", rate));
                Rpt1.SetParameters(new ReportParameter("unitprice", unitprice));
                Rpt1.SetParameters(new ReportParameter("parking", parking));
                Rpt1.SetParameters(new ReportParameter("valutility", valutility));
                Rpt1.SetParameters(new ReportParameter("other", other));
                Rpt1.SetParameters(new ReportParameter("Total", Total));
                Rpt1.SetParameters(new ReportParameter("bookingpercnt", bookingpercnt));
                Rpt1.SetParameters(new ReportParameter("bookingmoney", bookingmoney));
                Rpt1.SetParameters(new ReportParameter("valnoofemi", valnoofemi));
                Rpt1.SetParameters(new ReportParameter("emi", emi));
                Rpt1.SetParameters(new ReportParameter("fvpsft", fvpsft));
                Rpt1.SetParameters(new ReportParameter("pvpersft", pvpersft));
                //customer
                Rpt1.SetParameters(new ReportParameter("valcoffarea", valcoffarea));
                Rpt1.SetParameters(new ReportParameter("coffrate", coffrate));
                Rpt1.SetParameters(new ReportParameter("coffunitprice", coffunitprice));
                Rpt1.SetParameters(new ReportParameter("cofffparking", cofffparking));
                Rpt1.SetParameters(new ReportParameter("valcoffutility", valcoffutility));
                Rpt1.SetParameters(new ReportParameter("valcoffothers", valcoffothers));
                Rpt1.SetParameters(new ReportParameter("coffTotal", coffTotal));
                Rpt1.SetParameters(new ReportParameter("coffbookinmpercnt", coffbookinmpercnt));
                Rpt1.SetParameters(new ReportParameter("coffbookingam", coffbookingam));

                Rpt1.SetParameters(new ReportParameter("coffnooffemi", coffnooffemi));
                Rpt1.SetParameters(new ReportParameter("coffemi", coffemi));
                Rpt1.SetParameters(new ReportParameter("cofffvpersft", cofffvpersft));
                Rpt1.SetParameters(new ReportParameter("coffpvpersft", coffpvpersft));
             


                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Grand Note Sheet Details"));
                // Rpt1.SetParameters(new ReportParameter("projectName", projectName));

                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }



        }
        private void GetPreviousNoteSheetDetails()
        {


            this.lbtnBack.Visible = true;
            string notesheetno = this.ddlPrevious.SelectedValue.ToString();
            string usircode = ((DataTable)ViewState["tblprenotesheet"]).Select("noteshtid='" + notesheetno + "'")[0]["usircode"].ToString();
            Session.Remove("UsirBasicInformation");
            DataTable dtOrder = (DataTable)ViewState["tblData"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dtOrder = dv1.ToTable();


            this.MultiView1.ActiveViewIndex = 0;
            Session["UsirBasicInformation"] = dtOrder;
            this.gvSpayment.DataSource = dtOrder;
            this.gvSpayment.DataBind();
            this.lblCode.Text = usircode;
            this.gvSpayment.Columns[5].Visible = true;
            this.gvSpayment.Columns[6].Visible = true;



            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string pactcode = ((DataTable)ViewState["tblprenotesheet"]).Select("noteshtid='" + notesheetno + "'")[0]["pactcode"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPRENOTESHEETDETINFO", pactcode, usircode, notesheetno, "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            var lstb = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>();
            var lstcoff = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>();
            var lstrev = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>();

            Session["lstbaseschdule"] = lstb;
            Session["lstcoffschedule"] = lstcoff;


            this.ddlprospective.SelectedValue = ds1.Tables[0].Rows[0]["proscode"].ToString();
            this.ddlcoffduration.SelectedValue = ds1.Tables[0].Rows[0]["coffdur"].ToString();




            this.CalCulationSummation(ds1);
            this.Data_Bind();

        }



        protected void lbtnusize_Click(object sender, EventArgs e)
        {

            this.lbtnBack.Visible = true;

            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            Session.Remove("UsirBasicInformation");

            DataTable dtOrder = (DataTable)ViewState["tblData"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dtOrder = dv1.ToTable();


            this.MultiView1.ActiveViewIndex = 0;
            Session["UsirBasicInformation"] = dtOrder;
            this.gvSpayment.DataSource = dtOrder;
            this.gvSpayment.DataBind();
            this.lblCode.Text = usircode;
            this.gvSpayment.Columns[5].Visible = true;
            this.gvSpayment.Columns[6].Visible = true;
            this.ShowData();





        }

        private void ShowData()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usircode = this.lblCode.Text;
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //  string usrid = hst["usrid"].ToString();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "SHOWGRANDNOTESHEET", pactcode, usircode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            var lstb = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>();
            var lstcoff = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>();
            var lstrev = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>();
            Session["lstbaseschdule"] = lstb;
            Session["lstcoffschedule"] = lstcoff;

            this.CalCulationSummation(ds1);
            this.Data_Bind();
        }

        private void CalCulationSummation(DataSet ds1)
        {

            DateTime sysdate = System.DateTime.Today;
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];


            int badins = 0;
            DateTime coffbookingdate, benddate, finalinsdate;
            double usize, bfv, bpv, pamt, utility, others, intratio, noofemi, bfvpsft, bpowbpart, bpvpsft, coffurate, coffuamt, cofftunitamt,
                cofffv, coffpv, coffpamt, coffutility, coffothers, cofffvpsft, coffpvpsft, coffpowbpart, coffnoofemi, finalinsam, coffbookingam, coffdpaymentam, coffemi;






            //// Base Case
            bfv = lstb.Sum(l => l.fv);
            bpv = lstb.Sum(l => l.pv);
            usize = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]);
            pamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["pamt"]);
            utility = Convert.ToDouble(ds1.Tables[0].Rows[0]["utility"]);
            others = Convert.ToDouble(ds1.Tables[0].Rows[0]["others"]);
            intratio = Convert.ToDouble(ds1.Tables[0].Rows[0]["intratio"]);
            noofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]);
            bfvpsft = ((usize > 0) ? ((bfv - pamt - utility - others) / usize) : 0.00);
            bpowbpart = (12 + intratio) / 12;
            bpvpsft = Math.Round(bfvpsft / (Math.Pow(bpowbpart, noofemi)), 0);


            this.lblhiddenbpamt.Value = pamt.ToString("#,##0;(#,##0);");
            this.lblhiddenbutility.Value = utility.ToString("#,##0;(#,##0);");
            this.lblhiddenothers.Value = others.ToString("#,##0;(#,##0);");
            this.lblhiddenbnoemi.Value = noofemi.ToString();


            this.lblvalarea.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            this.lblvalrate.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["urate"]).ToString("#,##0;(#,##0);");
            this.lblvalunitprice.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["uamt"]).ToString("#,##0;(#,##0);");
            this.lblvalparking.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["pamt"]).ToString("#,##0;(#,##0);");
            this.lblvalutility.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["utility"]).ToString("#,##0;(#,##0);");
            this.lblvalother.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["others"]).ToString("#,##0;(#,##0);");
            this.lblvalTotal.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["tunitamt"]).ToString("#,##0;(#,##0);");           
            this.txtdownpayper.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["bookingper"]).ToString("#,##0;(#,##0);");
            this.lblvaldownpayam.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["bookingam"]).ToString("#,##0;(#,##0);");
            this.txtdownpaydate.Text = Convert.ToDateTime(lstb[0].schdate).ToString("dd-MMM-yyyy");


            this.lblvalnoofemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]).ToString("#,##0;(#,##0);");
            this.lblvalemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["emi"]).ToString("#,##0;(#,##0);");
            this.lblvalfvpsft.InnerText = bfvpsft.ToString("#,##0;(#,##0);");
            this.lblvalpvpersft.InnerText = bpvpsft.ToString("#,##0;(#,##0);");




            cofffv = lstcoff.Sum(l => l.fv);
            coffpv = lstcoff.Sum(l => l.pv);
            coffurate = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffurate"]);
            coffuamt = usize * coffurate;
            coffpamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]);
            coffutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]);
            coffothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]);
            
            coffnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]);
            coffbookingam = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffbookingam"]);
            coffdpaymentam = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]) == 0) ? 0 : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]);
            finalinsam = (Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsam"]) == 0) ? 0 : Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsam"]);            
            cofftunitamt = coffuamt + coffpamt + coffutility + coffothers;


            //Booking and Downpayment and Final Installment
            // badins = coffbookingam > 0 ? ++badins : badins;
            // badins = coffdpaymentam > 0 ? ++badins : badins;



            this.lblvalcoffarea.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            this.txtcoffrate.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffurate"]).ToString("#,##0;(#,##0);");
            this.lblcoffunitprice.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffuamt"]).ToString("#,##0;(#,##0);");
            this.txtcofffparking.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]).ToString("#,##0;(#,##0);");
            this.txtcoffutility.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]).ToString("#,##0;(#,##0);");
            this.txtcoffothers.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]).ToString("#,##0;(#,##0);");
            this.lblcoffTotal.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["cofftunitamt"]).ToString("#,##0;(#,##0);");
          
            this.txtcoffbookingam.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffbookingam"]).ToString("#,##0;(#,##0);");
            this.txtcoffBookingdate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd-MMM-yyyy");
            this.txtcoffdownpayper.Text = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntper"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntper"]).ToString("#,##0;(#,##0);");
            this.lblvalcoffdownpayam.InnerText = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]).ToString("#,##0;(#,##0);");
            this.txtcoffdownpaydate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffdpaymntdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffdpaymntdat"]).ToString("dd-MMM-yyyy");


            this.txtcofffininsper.Text = (Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsper"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsper"]).ToString("#,##0;(#,##0);");
            this.lblvalcofffininsam.InnerText = (Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsam"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsam"]).ToString("#,##0;(#,##0);");
            this.txtcofffininsdate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["cofffinsdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? sysdate.AddMonths((int)coffnoofemi).ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["cofffinsdat"]).ToString("dd-MMM-yyyy");





            coffbookingdate = Convert.ToDateTime(this.txtcoffBookingdate.Text);
            benddate = Convert.ToDateTime(lstb[lstb.Count - 1].schdate);
            finalinsdate = Convert.ToDateTime(this.txtcofffininsdate.Text); ;
            benddate = finalinsdate > benddate ? finalinsdate : benddate;
            noofemi = ASTUtility.Datediff(benddate, coffbookingdate);
            badins = finalinsam > 0 ? ++badins : badins;

            coffemi = Math.Round((coffnoofemi > 0 ? (cofftunitamt - coffbookingam - coffdpaymentam - finalinsam) / (coffnoofemi - badins) : 0.00), 0);



            cofffvpsft = ((usize > 0) ? (cofffv == 0 ? 0.00 : (cofffv - coffpamt - coffutility - coffothers) / usize) : 0.00);
            coffpowbpart = (12 + intratio) / 12;
            coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, noofemi)), 0);

            this.txtcoffnooffemi.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]).ToString("#,##0;(#,##0);");
            this.lblvalcoffemi.InnerText = coffemi.ToString("#,##0;(#,##0);");
            this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
            this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");



        }

        private void CalculationValue()
        {

            try
            {

                DateTime coffbookingdate, finalinsdate, benddate;
                int noofemi, badins = 0;

                double intratio, usize, coffurate, coffuamt, coffpamt, coffutility, coffothers, cofftunitamt, coffbookingper, coffbookingam, coffdpaymentper, coffdpaymentam, coffnoofemi, coffemi, cofffvpsft, coffpvpsft, coffpowbpart, cofffv, coffpv, finalinsper, finalinsam;


                usize = Convert.ToDouble(this.lblvalcoffarea.InnerText.ToString());
                intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;

                //Customer Offer
                coffurate = Convert.ToDouble("0" + this.txtcoffrate.Text.ToString());
                coffuamt = usize * coffurate;
                coffpamt = Convert.ToDouble("0" + this.txtcofffparking.Text.ToString());
                coffutility = Convert.ToDouble("0" + this.txtcoffutility.Text.ToString());
                coffothers = Convert.ToDouble("0" + this.txtcoffothers.Text.ToString());
                cofftunitamt = coffuamt + coffpamt + coffutility + coffothers;
                coffbookingam = Convert.ToDouble("0" + this.txtcoffbookingam.Text.ToString());
                coffdpaymentper = Convert.ToDouble("0" + this.txtcoffdownpayper.Text.ToString());
                coffdpaymentam = cofftunitamt * 0.01 * coffdpaymentper;
                coffnoofemi = Convert.ToDouble("0" + this.txtcoffnooffemi.Text.ToString());
                finalinsper = Convert.ToDouble("0" + this.txtcofffininsper.Text.ToString());
                finalinsam = cofftunitamt * 0.01 * finalinsper;

                
                badins = finalinsam > 0 ? ++badins : badins;


                coffemi = Math.Round((coffnoofemi > 0 ? (cofftunitamt - coffbookingam - coffdpaymentam - finalinsam) / (coffnoofemi - badins) : 0.00), 0);

                this.lblcoffunitprice.InnerText = coffuamt.ToString("#,##0;(#,##0);");
                this.txtcofffparking.Text = coffpamt.ToString("#,##0;(#,##0);");
                this.txtcoffutility.Text = coffutility.ToString("#,##0;(#,##0);");
                this.txtcoffothers.Text = coffothers.ToString("#,##0;(#,##0);");
                this.lblcoffTotal.InnerText = cofftunitamt.ToString("#,##0;(#,##0);");
               
                this.txtcoffbookingam.Text = coffbookingam.ToString("#,##0;(#,##0);");

                this.txtcoffdownpayper.Text = coffdpaymentper.ToString("#,##0;(#,##0);");
                this.lblvalcoffdownpayam.InnerText = coffdpaymentam.ToString("#,##0;(#,##0);");

                this.txtcoffnooffemi.Text = coffnoofemi.ToString("#,##0;(#,##0);");
                this.lblvalcoffemi.InnerText = coffemi.ToString("#,##0;(#,##0);");
                this.txtcofffininsper.Text = finalinsper.ToString("#,##0;(#,##0);");
                this.lblvalcofffininsam.InnerText = finalinsam.ToString("#,##0;(#,##0);");
                //this.txtcofffininsdate=

                this.CalCulationInstallment();
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];


                int count = lstcoff.Count;
                coffbookingdate = Convert.ToDateTime(lstcoff[0].schdate);
                finalinsdate = Convert.ToDateTime(lstcoff[count - 1].schdate);
                benddate = Convert.ToDateTime(lstb[lstb.Count - 1].schdate);
                benddate = finalinsdate > benddate ? finalinsdate : benddate;
                noofemi = ASTUtility.Datediff(benddate, coffbookingdate);



                cofffv = lstcoff.Sum(l => l.fv);
                coffpv = lstcoff.Sum(l => l.pv);
                cofffvpsft = ((usize > 0) ? ((cofffv - coffpamt - coffutility - coffothers) / usize) : 0.00);
                coffpowbpart = (12 + intratio) / 12;
                coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, noofemi)), 0);
                this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
                this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");









            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }
        }

        private void SaveValue()
        {
            int mondiff, i;
            double coffpowbpart, intratio, pv;
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];

            intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;
            int coffdur = Convert.ToInt32(this.ddlcoffduration.SelectedValue.ToString());

            coffpowbpart = (12 + intratio) / 12;
            DateTime strtdate, benddate, finalinsdate;
            int count = lstcoff.Count;
            finalinsdate = Convert.ToDateTime(lstcoff[count - 1].schdate);
            strtdate = Convert.ToDateTime(lstb[0].schdate);
            benddate = Convert.ToDateTime(lstb[lstb.Count - 1].schdate);
            //benddate = strtdate.AddMonths(bnoofemi);
            benddate = finalinsdate > benddate ? finalinsdate : benddate;

            i = 0;
            foreach (GridViewRow gv1 in gvcoffsch.Rows)
            {
                DateTime schdate = Convert.ToDateTime(((TextBox)gv1.FindControl("txtgvScheduledate")).Text.Trim());
                string monthid = schdate.ToString("yyyyMM");
                string ymon = schdate.ToString("MMM-yy");
                pv = ASTUtility.StrNagative(((TextBox)gv1.FindControl("txtgvdumschamt")).Text.Trim());
                mondiff = ASTUtility.Datediff(benddate, schdate);


                lstcoff[i].pv = pv;
                lstcoff[i].fv = Math.Round(pv * Math.Pow(coffpowbpart, mondiff), 0);
                lstcoff[i].monthid = monthid;
                lstcoff[i].ymon = ymon;
                i++;
            }


            DateTime coffbookingdate;
            double usize, coffurate, noofemi, coffuamt, coffpamt, coffutility, coffothers, cofftunitamt, coffbookingper, coffbookingam, cofffv, coffpv, cofffvpsft, coffpvpsft;

            usize = Convert.ToDouble(this.lblvalcoffarea.InnerText.ToString());
            coffurate = Convert.ToDouble("0" + this.txtcoffrate.Text.ToString());
            coffuamt = usize * coffurate;
            coffpamt = Convert.ToDouble("0" + this.txtcofffparking.Text.ToString());
            coffutility = Convert.ToDouble("0" + this.txtcoffutility.Text.ToString());
            coffothers = Convert.ToDouble("0" + this.txtcoffothers.Text.ToString());
            cofftunitamt = coffuamt + coffpamt + coffutility + coffothers;
            coffbookingam = Convert.ToDouble("0" + this.txtcoffbookingam.Text.ToString());
            
            coffbookingdate = Convert.ToDateTime(lstcoff[0].schdate);
            noofemi = ASTUtility.Datediff(benddate, coffbookingdate);

            cofffv = lstcoff.Sum(l => l.fv);
            coffpv = lstcoff.Sum(l => l.pv);
            cofffvpsft = ((usize > 0) ? ((cofffv - coffpamt - coffutility - coffothers) / usize) : 0.00);
            coffpowbpart = (12 + intratio) / 12;
            coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, noofemi)), 0);
            this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
            this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");

            Session["lstcoff"] = lstcoff;

            Session["lstcoff"] = lstcoff;
        }

        private void CalCulationInstallment()
        {

            try

            {


                DateTime benddate;
                int bnoofemi, coffnoofemi, initial, mondiff, finalins;
                finalins = 0;
                double cofftunitamt, coffbookingam, coffdpaymentam, coffemi, coffpowbpart, intratio, coffresamt, finalinsam;
                string monthid, gcod, gdesc, ymon, grp;
                double pv, fv;

                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode> lstcode = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode>)Session["tblinstallment"];

                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = new List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>();

                intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;
                // Customer Offer
                coffbookingam = Convert.ToDouble("0" + this.txtcoffbookingam.Text);
                coffdpaymentam = Convert.ToDouble("0" + this.lblvalcoffdownpayam.InnerText);
                coffnoofemi = Convert.ToInt32("0" + this.txtcoffnooffemi.Text.ToString());
                coffemi = Convert.ToDouble("0" + this.lblvalcoffemi.InnerText);
                finalinsam = Convert.ToDouble("0" + this.lblvalcofffininsam.InnerText);
                int coffdur = Convert.ToInt32(this.ddlcoffduration.SelectedValue.ToString());
                coffpowbpart = (12 + intratio) / 12;

                DateTime bcasebookingdate, bookingdate, dpaymentdate, firstinsdate, finalinsdate;
                bcasebookingdate = Convert.ToDateTime(this.txtcoffBookingdate.Text);
                bookingdate = Convert.ToDateTime(this.txtcoffBookingdate.Text);
                dpaymentdate = Convert.ToDateTime(this.txtcoffdownpaydate.Text);
                firstinsdate = Convert.ToDateTime(this.txtcoffinsdate.Text);
                finalinsdate = Convert.ToDateTime(this.txtcofffininsdate.Text);
                bnoofemi = Convert.ToInt32(this.lblhiddenbnoemi.Value);
                benddate = bcasebookingdate.AddMonths(bnoofemi);

                //benddate = strtdate.AddMonths(bnoofemi);
                benddate = finalinsdate > benddate ? finalinsdate : benddate;
                benddate = finalinsdate > benddate ? finalinsdate : benddate;
                initial = 0;


                // Base Case 
                foreach (RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet blstitem in lstb)
                {



                    pv = blstitem.pv;
                    mondiff = ASTUtility.Datediff(benddate, bcasebookingdate);
                    monthid = bcasebookingdate.ToString("yyyyMM");
                    ymon = bcasebookingdate.ToString("MMM-yy");
                    blstitem.monthid = monthid;
                    blstitem.ymon = ymon;
                    blstitem.schdate = bcasebookingdate;
                    blstitem.fv = Math.Round(pv * Math.Pow(coffpowbpart, mondiff), 0);
                    bcasebookingdate = bcasebookingdate.AddMonths(1);
                }

                Session["lstbaseschdule"] = lstb;
              




                //Booking Money
                if (coffbookingam > 0)
                {
                    var lstins = lstcode.FindAll(l => l.gcod.Substring(0, 5) == "81001");
                    monthid = bookingdate.ToString("yyyyMM");
                    ymon = bookingdate.ToString("MMM-yy");
                    gcod = lstins[0].gcod;
                    gdesc = lstins[0].gdesc;
                    grp = "02";
                    mondiff = ASTUtility.Datediff(benddate, bookingdate);
                    RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                    {
                        monthid = monthid,
                        ymon = ymon,
                        gcod = gcod,
                        gdesc = gdesc,
                        schdate = bookingdate,
                        grp = grp,
                        pv = coffbookingam,
                        fv = Math.Round(coffbookingam * Math.Pow(coffpowbpart, mondiff), 0)
                    };
                    lstcoff.Add(obj);


                }
                //DownPayment

                if (coffdpaymentam > 0)
                {
                    var lstins = lstcode.FindAll(l => l.gcod.Substring(0, 5) == "81002");
                    monthid = dpaymentdate.ToString("yyyyMM");
                    ymon = dpaymentdate.ToString("MMM-yy");
                    gcod = lstins[0].gcod;
                    gdesc = lstins[0].gdesc;
                    grp = "02";
                    mondiff = ASTUtility.Datediff(benddate, dpaymentdate);


                    RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                    {
                        monthid = monthid,
                        ymon = ymon,
                        gcod = gcod,
                        gdesc = gdesc,
                        schdate = dpaymentdate,
                        grp = grp,
                        pv = coffdpaymentam,
                        fv = Math.Round(coffdpaymentam * Math.Pow(coffpowbpart, mondiff), 0)
                    };
                    lstcoff.Add(obj);


                }


                //Final Installment

                if (finalinsam > 0)
                {
                    var lstfin = lstcode.FindAll(l => l.gcod.Substring(0, 5) == "81985");

                    monthid = finalinsdate.ToString("yyyyMM");
                    ymon = finalinsdate.ToString("MMM-yy");
                    gcod = lstfin[0].gcod;
                    gdesc = lstfin[0].gdesc;
                    grp = "02";
                    //  mondiff = ASTUtility.Datediff(benddate, finalinsdate)>0? (ASTUtility.Datediff(benddate, finalinsdate)-1):0;
                    mondiff = ASTUtility.Datediff(benddate, finalinsdate);

                    RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                    {
                        monthid = monthid,
                        ymon = ymon,
                        gcod = gcod,
                        gdesc = gdesc,
                        schdate = finalinsdate,
                        grp = grp,
                        pv = finalinsam,
                        fv = Math.Round(finalinsam * Math.Pow(coffpowbpart, mondiff), 0)
                    };
                    lstcoff.Add(obj);
                }


                //Booking, Downpayment, Final Installment
                //Booking and Downpayment

                initial = coffbookingam > 0 ? ++initial : initial;
                initial = coffdpaymentam > 0 ? ++initial : initial;
                initial = finalinsam > 0 ? ++initial : initial;
                // for  Final Installment
                finalins = finalinsam > 0 ? ++finalins : finalins;
                coffnoofemi = coffnoofemi - finalins;


                var lstfoins = lstcode.FindAll(l => l.gcod.Substring(0, 5) != "81001" && l.gcod.Substring(0, 5) != "81002" && l.gcod.Substring(0, 5) != "81985");
                int k = lstcoff.Count;

                int ins = 0;
                for (int j = k; j < coffnoofemi + k; j++)
                {




                    monthid = finalinsdate.ToString("yyyyMM");
                    ymon = finalinsdate.ToString("MMM-yy");
                    gcod = lstfoins[ins].gcod;
                    gdesc = lstfoins[ins].gdesc;
                    grp = "02";
                    // mondiff = ASTUtility.Datediff(benddate, firstinsdate);
                    mondiff = ASTUtility.Datediff(benddate, firstinsdate);




                    if (initial == coffnoofemi + k - 1) // Last Installment
                    {

                        cofftunitamt = Convert.ToDouble("0" + this.lblcoffTotal.InnerText);
                        coffresamt = cofftunitamt - lstcoff.Sum(l => l.pv);

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            gcod = gcod,
                            gdesc = gdesc,
                            schdate = firstinsdate,
                            grp = grp,
                            pv = coffresamt,
                            fv = Math.Round(coffresamt * Math.Pow(coffpowbpart, mondiff), 0)
                        };
                        lstcoff.Add(obj);
                    }



                    else
                    {

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            gcod = gcod,
                            gdesc = gdesc,
                            schdate = firstinsdate,
                            grp = grp,
                            pv = coffemi,
                            fv = Math.Round(coffemi * Math.Pow(coffpowbpart, mondiff), 0)
                        };
                        lstcoff.Add(obj);
                    }
                    initial++;
                    ins++;
                    firstinsdate = firstinsdate.AddMonths(coffdur);


                }
                var lst1 = lstcoff.OrderBy(l => l.gcod).ToList();
                Session["lstcoffschedule"] = lst1;
                this.Data_Bind();
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }

        }
        protected void lbtnCalCulation_Click(object sender, EventArgs e)
        {

            this.CalculationValue();
        }

        protected void lbtnAdddsch_Click(object sender, EventArgs e)
        {
            //List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lst = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            //int lrow = lst.Count;


            //string monthid = lst[lrow - 1].monthid;
            //string grp = "02";
            //string ymon = lst[lrow - 1].ymon;
            //double pv = lst[lrow - 1].pv;
            //double fv= lst[lrow - 1].fv;


            //RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
            //{
            //    monthid = monthid,
            //    ymon = ymon,
            //    grp = grp,
            //    pv = pv,
            //    fv = fv
            //};


            //lst.Add(obj);
            //var lst1 = lst.OrderBy(l => l.monthid).ToList();
            //Session["lstcoffschedule"] = lst1;
            //this.Data_Bind();
        }

        protected void lnkgvFcoffTotal_Click(object sender, EventArgs e)
        {

            this.SaveValue();

            this.Data_Bind();
        }

        protected void lbtnDeldsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lst = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            lst.RemoveAt(rowindex);
            Session["lstcoffschedule"] = lst;
            this.Data_Bind();

        }


       





     



        private void Data_Bind()
        {
            try
            {


                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];

                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];


                this.gvbcasesch.DataSource = lstb;
                this.gvbcasesch.DataBind();


                this.gvcoffsch.DataSource = lstcoff;
                this.gvcoffsch.DataBind();
                this.FooterCalculation();


            }

            catch (Exception ex)
            {


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }

        }

        private void FooterCalculation()
        {
            try
            {



                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            


                ((Label)this.gvbcasesch.FooterRow.FindControl("lgvFpvschamt")).Text = lstb.Sum(l => l.pv).ToString("#,##0;(#,##0);");
                ((Label)this.gvbcasesch.FooterRow.FindControl("lgvFfvscham")).Text = lstb.Sum(l => l.fv).ToString("#,##0;(#,##0);");


                if (lstcoff.Count > 0)
                {
                    ((Label)this.gvcoffsch.FooterRow.FindControl("lgvFcoffpvschamt")).Text = lstcoff.Sum(l => l.pv).ToString("#,##0;(#,##0);");
                    ((Label)this.gvcoffsch.FooterRow.FindControl("lgvFcofffvscham")).Text = lstcoff.Sum(l => l.fv).ToString("#,##0;(#,##0);");
                }




            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }



        }




        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.LoadGrid();



        }




        protected void lbtnsrchunit_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }







        private void LastGrandNoteSheet()
        {
            string comcod = this.GetCompCode();
            string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETLASTNOTESHEETNO", "",
                   date, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {


                this.ddlPrevious.DataTextField = "maxno1";
                this.ddlPrevious.DataValueField = "maxno";
                this.ddlPrevious.DataSource = ds2.Tables[0];
                this.ddlPrevious.DataBind();


            }



        }

        private bool GetResultpvapvpersft()
        {

            bool result = false;

            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];


            int noofemi;
            DateTime coffbookingdate, benddate, finalinsdate;
            double uzize, bfv, bpv, pamt, utility, others, bpowbpart, bfvpsft, bpvpsft, intratio, usize, coffpamt, coffutility, coffothers, cofffvpsft, coffpvpsft, coffpowbpart, cofffv, coffpv;


            usize = Convert.ToDouble(this.lblvalcoffarea.InnerText.ToString());
            intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;

            int count = lstcoff.Count;
            coffbookingdate = Convert.ToDateTime(lstcoff[0].schdate);
            finalinsdate = Convert.ToDateTime(lstcoff[count - 1].schdate);
            benddate = Convert.ToDateTime(lstb[lstb.Count - 1].schdate);
            benddate = finalinsdate > benddate ? finalinsdate : benddate;

            uzize = Convert.ToDouble("0" + this.lblvalcoffarea.InnerText.ToString());
            coffpamt = Convert.ToDouble("0" + this.txtcofffparking.Text.ToString());
            coffutility = Convert.ToDouble("0" + this.txtcoffutility.Text.ToString());
            coffothers = Convert.ToDouble("0" + this.txtcoffothers.Text.ToString());
            noofemi = ASTUtility.Datediff(benddate, coffbookingdate);

            //Base Case     
            bfv = lstb.Sum(l => l.fv);
            bpv = lstb.Sum(l => l.pv);
            pamt = Convert.ToDouble(this.lblhiddenbpamt.Value);
            utility = Convert.ToDouble(this.lblhiddenbutility.Value);
            others = Convert.ToDouble(this.lblhiddenothers.Value);
            bfvpsft = ((uzize > 0) ? ((bfv - pamt - utility - others) / uzize) : 0.00);
            bpowbpart = (12 + intratio) / 12;
            bpvpsft = Math.Round(bfvpsft / (Math.Pow(bpowbpart, noofemi)), 0);


            // Customer Offer  Case
            cofffv = lstcoff.Sum(l => l.fv);
            coffpv = lstcoff.Sum(l => l.pv);
            cofffvpsft = ((usize > 0) ? ((cofffv - coffpamt - coffutility - coffothers) / usize) : 0.00);
            coffpowbpart = (12 + intratio) / 12;
            coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, noofemi)), 0);
            //this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
            //this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");

            if (cofffvpsft >= bfvpsft && coffpvpsft >= bpvpsft)
            {

                result = true;
            }



            return result;




        }


       
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }







        protected void lnkbtnFindProject_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnProspective_Click(object sender, EventArgs e)
        {

        }

        protected void lnkgvbaseFcoffTotal_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnPrevious_Click(object sender, EventArgs e)
        {

            try
            {


                ViewState.Remove("tblprenotesheet");
                string comcod = this.GetCompCode();
                string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string qgeno = this.Request.QueryString["genno"] ?? "";
                string noteshtno = (qgeno.Length == 0 ? "" : qgeno) + "%";
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPREVIOUSNOTESHEETNO",
                       date, noteshtno, "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.ddlPrevious.DataTextField = "noteshiddet";
                this.ddlPrevious.DataValueField = "noteshtid";
                this.ddlPrevious.DataSource = ds1.Tables[0];
                this.ddlPrevious.DataBind();
                ViewState["tblprenotesheet"] = ds1.Tables[0];
                ds1.Dispose();




            }
            catch (Exception ex)
            {


            }
        }


      










    }
}

