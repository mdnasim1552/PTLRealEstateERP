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
namespace RealERPWEB.F_22_Sal
{
    public partial class MktGrandNoteSheet : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        Common objcom = new Common();

        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string TypeDesc = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Grand Note Sheet";

                Session.Remove("Unit");
               string date= System.DateTime.Today.ToString("dd-MMM-yyyy");
                string finsdate = Convert.ToDateTime(date).AddMonths(1).ToString("dd-MMM-yyyy");
                this.txtBookingdate.Text = date;
                this.txtfirstinsdate.Text = finsdate;
                this.txtcoffBookingdate.Text = date;
                this.txtcoffinsdate.Text = date;
                this.txtrevpBookingdate.Text = date;
                this.txtrevpinsdate.Text = date;
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALES WITH PAYMENT  INFORMATION ";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.gvSpayment.Columns[0].Visible = false;

               



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
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                MultiView1.ActiveViewIndex = -1;
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.lbtnBack.Visible = false;
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
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string srchunit = "%" + this.txtsrchunit.Text.Trim() + "%";
            // string musircode = "51";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "DETAILSIRINFINFORMATION", PactCode, srchunit, "", "", "", "", "", "", "");
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFORMATION", PactCode, srchunit, musircode, "", "", "", "", "", "");
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

            // this.Data_SumBind();
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
            var lstcoff = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> ();
            var lstrev = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> ();
         
            Session["lstbaseschdule"] = lstb;
            Session["lstcoffschedule"] = lstcoff;
            Session["lstrevschedule"] = lstrev;

            double uzize, bfv, bpv, uamt, pamt, utility, others, bfvpsft, bpvpsft, bpowbpart, intratio, noofemi, 
                cofffv, coffpv, coffpamt, coffutility, coffothers, cofffvpsft, coffpvpsft, coffpowbpart, coffnoofemi, revfv, revpv,
                revpamt, revutility, revothers, revfvpsft, revpvpsft, revpowbpart, revnoofemi;

            uzize=
            bfv = lstb.Sum(l => l.fv);
            bpv = lstb.Sum(l => l.pv);
            uzize= Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]);
            pamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["pamt"]);
            utility = Convert.ToDouble(ds1.Tables[0].Rows[0]["utility"]);
            others = Convert.ToDouble(ds1.Tables[0].Rows[0]["others"]);
            intratio= Convert.ToDouble(ds1.Tables[0].Rows[0]["intratio"]);
            noofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]);
            bfvpsft = ((uzize > 0) ? ((bfv - pamt - utility - others) / uzize) : 0.00);
            bpowbpart = (12 + intratio) / 12;
            bpvpsft =Math.Round(bfvpsft / (Math.Pow(bpowbpart, noofemi )),0);

            cofffv = lstcoff.Sum(l => l.fv);
            coffpv = lstcoff.Sum(l => l.pv);
            coffpamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]);
            coffutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]);
            coffothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]);
            coffnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]);
            cofffvpsft = ((uzize > 0) ? ((cofffv - coffpamt - coffutility - coffothers) / uzize) : 0.00);
            coffpowbpart = (12 + intratio) / 12;
            coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, coffnoofemi)), 0);




            revfv = lstrev.Sum(l => l.fv);
            revpv = lstrev.Sum(l => l.pv);
            revpamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["revpamt"]);
            revutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["revutility"]);
            revothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["revothers"]);
            revnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["revnoofemi"]);
            revfvpsft = ((uzize > 0) ? ((revfv - revpamt - revutility - revothers) / uzize) : 0.00);
            revpowbpart = (12 + intratio) / 12;
            revpvpsft = Math.Round(revfvpsft / (Math.Pow(revpowbpart, revnoofemi)), 0);






            this.lblvalarea.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            this.lblvalrate.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["urate"]).ToString("#,##0;(#,##0);");
            this.lblvalunitprice.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["uamt"]).ToString("#,##0;(#,##0);");
            this.lblvalparking.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["pamt"]).ToString("#,##0;(#,##0);");
            this.lblvalutility.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["utility"]).ToString("#,##0;(#,##0);");
            this.lblvalother.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["others"]).ToString("#,##0;(#,##0);");
            this.lblvalTotal.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["tunitamt"]).ToString("#,##0;(#,##0);");
            this.lblvalbookingpercnt.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["bookingper"]).ToString("#,##0;(#,##0);");
            this.lblvalbookingmoney.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["bookingam"]).ToString("#,##0;(#,##0);");
            this.lblvalnoofemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]).ToString("#,##0;(#,##0);");
            this.lblvalemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["emi"]).ToString("#,##0;(#,##0);");
            this.lblvalfvpsft.InnerText =bfvpsft.ToString("#,##0;(#,##0);");
            this.lblvalpvpersft.InnerText = bpvpsft.ToString("#,##0;(#,##0);");




            this.lblvalcoffarea.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            this.txtcoffrate.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffurate"]).ToString("#,##0;(#,##0);");
            this.lblcoffunitprice.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffuamt"]).ToString("#,##0;(#,##0);");
            this.txtcofffparking.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]).ToString("#,##0;(#,##0);");
            this.txtcoffutility.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]).ToString("#,##0;(#,##0);");
            this.txtcoffothers.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]).ToString("#,##0;(#,##0);");
            this.lblcoffTotal.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["cofftunitamt"]).ToString("#,##0;(#,##0);");
            this.txtcoffbookinmpercnt.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffbookingper"]).ToString("#,##0;(#,##0);");
            this.lblvalcoffbookingam.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffbookingam"]).ToString("#,##0;(#,##0);");
            this.txtcoffnooffemi.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]).ToString("#,##0;(#,##0);");
            this.lblvalcoffemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffemi"]).ToString("#,##0;(#,##0);");          
            this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
            this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");


            this.lblvalrevparea.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            this.txtrevprate.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["revurate"]).ToString("#,##0;(#,##0);");
            this.lblrevpunitprice.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["revuamt"]).ToString("#,##0;(#,##0);");
            this.txtrevpparking.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["revpamt"]).ToString("#,##0;(#,##0);");
            this.txtrevputility.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["revutility"]).ToString("#,##0;(#,##0);");
            this.txtrevpothers.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["revothers"]).ToString("#,##0;(#,##0);");
            this.lblrevpTotal.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["revtunitamt"]).ToString("#,##0;(#,##0);");
            this.txtrevpbbookinmpercnt.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["revbookingper"]).ToString("#,##0;(#,##0);");
            this.lblvalrevpbookingam.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["revbookingam"]).ToString("#,##0;(#,##0);");
            this.txtrevpnooffemi.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["revnoofemi"]).ToString("#,##0;(#,##0);");
            this.lblvalrevpemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["revemi"]).ToString("#,##0;(#,##0);");
            this.lblvalrevpfvpersft.InnerText = revfvpsft.ToString("#,##0;(#,##0);");
            this.lblvalrevppvpersft.InnerText = revpvpsft.ToString("#,##0;(#,##0);");



            this.Data_Bind();
        }

        private void CalculationValue()
        {

            try
            {


              

                double intratio, usize, coffurate, coffuamt, coffpamt, coffutility, coffothers, cofftunitamt, coffbookingper, coffbookingam, coffnoofemi, coffemi, cofffvpsft, coffpvpsft, coffpowbpart, cofffv, coffpv, revurate, revuamt, revpamt, revutility, revothers, revtunitamt, revbookingper, revbookingam, revfvpsft, revpvpsft, revpowbpart, revnoofemi, revemi, revfv, revpv;

                usize = Convert.ToDouble(this.lblvalarea.InnerText.ToString());
                intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;

                //Customer Offer

                coffurate = Convert.ToDouble("0" + this.txtcoffrate.Text.ToString());
                coffuamt = usize * coffurate;
                coffpamt = Convert.ToDouble("0" + this.txtcofffparking.Text.ToString());
                coffutility = Convert.ToDouble("0" + this.txtcoffutility.Text.ToString());
                coffothers = Convert.ToDouble("0" + this.txtcoffothers.Text.ToString());
                cofftunitamt = coffuamt + coffpamt + coffutility + coffothers;
                coffbookingper = Convert.ToDouble("0" + this.txtcoffbookinmpercnt.Text.ToString());
                coffbookingam = cofftunitamt * 0.01 * coffbookingper;
                coffnoofemi = Convert.ToDouble("0" + this.txtcoffnooffemi.Text.ToString());
                coffemi = Math.Round((coffnoofemi > 0 ? (cofftunitamt - coffbookingam) / coffnoofemi : 0.00), 0);

                this.lblcoffunitprice.InnerText = coffuamt.ToString("#,##0;(#,##0);");
                this.txtcofffparking.Text = coffpamt.ToString("#,##0;(#,##0);");
                this.txtcoffutility.Text = coffutility.ToString("#,##0;(#,##0);");
                this.txtcoffothers.Text = coffothers.ToString("#,##0;(#,##0);");
                this.lblcoffTotal.InnerText = cofftunitamt.ToString("#,##0;(#,##0);");
                this.txtcoffbookinmpercnt.Text = coffbookingper.ToString("#,##0;(#,##0);");
                this.lblvalcoffbookingam.InnerText = coffbookingam.ToString("#,##0;(#,##0);");
                this.txtcoffnooffemi.Text = coffnoofemi.ToString("#,##0;(#,##0);");
                this.lblvalcoffemi.InnerText = coffemi.ToString("#,##0;(#,##0);");


              


                // Revised offer

                revurate = Convert.ToDouble("0" + this.txtrevprate.Text.ToString());
                revuamt = usize * revurate;
                revpamt = Convert.ToDouble("0" + this.txtrevpparking.Text.ToString());
                revutility = Convert.ToDouble("0" + this.txtrevputility.Text.ToString());
                revothers = Convert.ToDouble("0" + this.txtrevpothers.Text.ToString());
                revtunitamt = revuamt + revpamt + revutility + revothers;
                revbookingper = Convert.ToDouble("0" + this.txtrevpbbookinmpercnt.Text.ToString());
                revbookingam = revtunitamt * 0.01 * revbookingper;
                revnoofemi = Convert.ToDouble("0" + this.txtrevpnooffemi.Text.ToString());
                revemi = Math.Round((revnoofemi > 0 ? (revtunitamt - revbookingam) / revnoofemi : 0.00), 0);
               
                this.lblrevpunitprice.InnerText = revuamt.ToString("#,##0;(#,##0);");
                this.txtrevpparking.Text = revpamt.ToString("#,##0;(#,##0);");
                this.txtrevputility.Text = revutility.ToString("#,##0;(#,##0);");
                this.txtrevpothers.Text = revothers.ToString("#,##0;(#,##0);");
                this.lblrevpTotal.InnerText = revtunitamt.ToString("#,##0;(#,##0);");
                this.txtrevpbbookinmpercnt.Text = revbookingper.ToString("#,##0;(#,##0);");
                this.lblvalrevpbookingam.InnerText = revbookingam.ToString("#,##0;(#,##0);");
                this.txtrevpnooffemi.Text = revnoofemi.ToString("#,##0;(#,##0);");
                this.lblvalrevpemi.InnerText = revemi.ToString("#,##0;(#,##0);");




                this.CalCulationInstallment();
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>)Session["lstrevschedule"];
               
                cofffv = lstcoff.Sum(l => l.fv);
                coffpv = lstcoff.Sum(l => l.pv);
                cofffvpsft = ((usize > 0) ? ((cofffv - coffpamt - coffutility - coffothers) / usize) : 0.00);
                coffpowbpart = (12 + intratio) / 12;
                coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, coffnoofemi)), 0);
                this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
                this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");



                revfv = lstrev.Sum(l => l.fv);
                revpv = lstrev.Sum(l => l.pv);
                revfvpsft = ((usize > 0) ? ((revfv - revpamt - revutility - revothers) / usize) : 0.00);
                revpowbpart = (12 + intratio) / 12;
                revpvpsft = Math.Round(revfvpsft / (Math.Pow(revpowbpart, revnoofemi)), 0);

                this.lblvalrevpfvpersft.InnerText = revfvpsft.ToString("#,##0;(#,##0);");
                this.lblvalrevppvpersft.InnerText = revpvpsft.ToString("#,##0;(#,##0);");





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }













           

        }

        private void CalCulationInstallment()
        {

            try

            {



                int bnoofemi, coffnoofemi, revnoofemi, initial, mondiff;
                double coffbookingam, coffemi, coffpowbpart, intratio, revbookingam, revemi, revpowbpart;
                string monthid, ymon, grp;
                double pv, fv;



                //List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
                //List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>)Session["lstrevschedule"];


                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = new List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>();
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = new List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>();
                intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;
                // Customer Offer
                coffbookingam = Convert.ToDouble("0" + this.lblvalcoffbookingam.InnerText);
                coffnoofemi = Convert.ToInt32("0" + this.txtcoffnooffemi.Text.ToString());
                coffemi = Convert.ToDouble("0" + this.lblvalcoffemi.InnerText);
                int coffdur = Convert.ToInt32(this.ddlcoffduration.SelectedValue.ToString());
                // set @cintexpart = power((12 + @intratio) / 12, @mondiff)
                coffpowbpart = (12 + intratio) / 12;

                DateTime strtdate, enddate, renddate;
                strtdate = System.DateTime.Today;
                enddate = strtdate.AddMonths(coffnoofemi);
                initial = 0;
                while (strtdate <= enddate)
                {
                    monthid = strtdate.ToString("yyyyMM");
                    ymon = strtdate.ToString("MMM-yy");
                    grp = "02";
                    mondiff = ASTUtility.Datediff(enddate, strtdate);

                    if (initial == 0)
                    {

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = coffbookingam,
                            fv = coffbookingam * Math.Pow(coffpowbpart, mondiff)
                        };
                        lstcoff.Add(obj);
                        initial++;

                    }

                    else
                    {

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = coffemi,
                            fv = coffemi * Math.Pow(coffpowbpart, mondiff)
                        };
                        lstcoff.Add(obj);
                    }

                    strtdate= strtdate.AddMonths(coffdur);
                }




                // Revised Offer
                revbookingam = Convert.ToDouble("0" + this.lblvalrevpbookingam.InnerText);
                bnoofemi = Convert.ToInt32("0" + this.lblvalnoofemi.InnerText);
                revnoofemi = Convert.ToInt32("0" + this.txtrevpnooffemi.Text.ToString());
                revemi = Convert.ToDouble("0" + this.lblvalrevpemi.InnerText);
                //int revdur = Convert.ToInt32(this.ddlduration.SelectedValue.ToString());
                int revdur = Convert.ToInt32(this.ddlrevpduration.SelectedValue.ToString());
                revpowbpart = (12 + intratio) / 12;


                strtdate = System.DateTime.Today;
                enddate = strtdate.AddMonths(revnoofemi);
                renddate= strtdate.AddMonths(bnoofemi); 
                initial = 0;

                while (strtdate <= enddate)
                {
                    monthid = strtdate.ToString("yyyyMM");
                    ymon = strtdate.ToString("MMM-yy");
                    grp = "03";
                    mondiff = ASTUtility.Datediff(renddate, strtdate);

                    if (initial == 0)
                    {
                          initial = 0;

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = revbookingam,
                            fv = revbookingam * Math.Pow(revpowbpart, mondiff)
                        };
                        lstrev.Add(obj);
                        initial++;

                    }

                    else
                    {

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = revemi,
                            fv = revemi * Math.Pow(revpowbpart, mondiff)
                        };
                        lstrev.Add(obj);
                    }

                    strtdate = strtdate.AddMonths(revdur);
                }


                Session["lstcoffschedule"] = lstcoff;
                Session["lstrevschedule"] = lstrev;
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




        private void Data_Bind()
        {
            try
            {



                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>)Session["lstrevschedule"];
                this.gvbcasesch.DataSource = lstb;
                this.gvbcasesch.DataBind();

                this.gvcoffsch.DataSource = lstcoff;
                this.gvcoffsch.DataBind();
                this.gvrevpsch.DataSource = lstrev;
                this.gvrevpsch.DataBind();
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
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>)Session["lstrevschedule"];

                ((Label)this.gvbcasesch.FooterRow.FindControl("lgvFpvschamt")).Text = lstb.Sum(l => l.pv).ToString("#,##0;(#,##0);");
                ((Label)this.gvbcasesch.FooterRow.FindControl("lgvFfvscham")).Text = lstb.Sum(l => l.fv).ToString("#,##0;(#,##0);");

                ((Label)this.gvcoffsch.FooterRow.FindControl("lgvFcoffpvschamt")).Text = lstcoff.Sum(l => l.pv).ToString("#,##0;(#,##0);");
                ((Label)this.gvcoffsch.FooterRow.FindControl("lgvFcofffvscham")).Text = lstcoff.Sum(l => l.fv).ToString("#,##0;(#,##0);");


                ((Label)this.gvrevpsch.FooterRow.FindControl("lgvFrevpvschamt")).Text = lstrev.Sum(l => l.pv).ToString("#,##0;(#,##0);");
                ((Label)this.gvrevpsch.FooterRow.FindControl("lgvFrevfvscham")).Text = lstrev.Sum(l => l.fv).ToString("#,##0;(#,##0);");


            }

            catch (Exception e)
            {



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

      
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {



            //DataTable dt = (DataTable)Session["tblPay"];
            //int i, k = 0;
            ////  List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = new List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>();
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            ////Booking and downpayment

            //double bandpamt = 0;
            //string gcod, gdesc; DateTime schDate; DateTime AccBookDate;
            //foreach (GridViewRow gvr1 in gvdumpay.Rows)
            //{
            //    gcod = ((Label)gvr1.FindControl("lblgvgcod")).Text.Trim();
            //    schDate = Convert.ToDateTime(((TextBox)gvr1.FindControl("txtgvScheduledate")).Text.Trim());
            //    double Amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)gvr1.FindControl("txtgvdumschamt")).Text.Trim()));
            //    //  Amount = (Amount>0)?Amount:0;
            //    bandpamt += Amount;

            //    if (ASTUtility.Left(gcod, 5) == "81985")
            //    {
            //        var lsts = lstd.FindAll(l => l.gcod == gcod);
            //        if (lsts.Count() > 0)
            //        {


            //            lsts[0].schdate = schDate;
            //            lsts[0].schamt = Amount;
            //        }
            //    }
            //    else
            //    {

            //        lstd[k].schdate = schDate;
            //        lstd[k].schamt = Amount;
            //        k++;

            //    }



            //}




            //double Tamt = Convert.ToDouble("0" + this.txttoamt.Text);
            //double ramt = Tamt - bandpamt;
            //int tin = Convert.ToInt16("0" + this.txtnofins.Text);
            //int dur = Convert.ToInt16(this.ddlMonth.SelectedValue.ToString());
            //double insamt = ramt / tin;
            //// string schDate1 = Convert.ToDateTime(this.txtfinsdate.Text).ToString("dd-MMM-yyyy");
            //DateTime insdate1 = Convert.ToDateTime(this.txtfinsdate.Text);
            //DateTime insdate2;
            //for (int j = k; j < tin + k; j++)
            //{
            //    insdate2 = (j == k) ? insdate1 : Convert.ToDateTime(insdate1).AddMonths(dur);
            //    double schamt = insamt;
            //    lstd[j].schdate = insdate2;
            //    lstd[j].schamt = schamt;
            //    insdate1 = insdate2;
            //}


            //double acbookamt = Convert.ToDouble("0" + this.txtacbooking.Text);
            //var lstbdate = lstd.FindAll(l => l.schamt > 0);
            //AccBookDate = lstbdate[0].schdate;
            //int atin = Convert.ToInt32("0" + this.txtacinstallment.Text);
            //DateTime ainsdate = Convert.ToDateTime(this.txtfinsdate.Text);
            ////lstd.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule(insdate, schinsamt));
            //lsta.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(AccBookDate, acbookamt));
            //ramt = Tamt - acbookamt;
            //double acinsamt = ramt / atin;

            //for (i = 0; i < atin; i++)
            //{

            //    lsta.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(ainsdate, acinsamt));
            //    ainsdate = ainsdate.AddMonths(dur);
            //}


            //Session["tbldschamt"] = lstd.FindAll(l => l.schamt > 0);
            //Session["tblacamt"] = lsta;
            //this.Data_Bind();
            //this.chkVisible.Checked = false;
            //this.chkVisible_CheckedChanged(null, null);
        }






        

    


        protected void lbtnTotaldumsch_Click(object sender, EventArgs e)
        {
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];

            //for (int i = 0; i < this.gvdumpay.Rows.Count; i++)
            //{
            //    DateTime schdate = Convert.ToDateTime(((TextBox)this.gvdumpay.Rows[i].FindControl("txtgvScheduledate")).Text.Trim());
            //    double schamt = Convert.ToDouble("0" + ((TextBox)this.gvdumpay.Rows[i].FindControl("txtgvdumschamt")).Text.Trim());

            //    lst[i].schdate = schdate;
            //    lst[i].schamt = schamt;
            //}
            //Session["tbldschamt"] = lst;
            //double Amount = lst.Sum(l => l.schamt);
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            //((Label)this.gvdumpay.FooterRow.FindControl("lgvFdumschamt")).Text = Amount.ToString("#,##0;(#,##0); ");




            //this.Data_Bind();

        }
        protected void lbtnTotalacsch_Click(object sender, EventArgs e)
        {
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

            //for (int i = 0; i < this.gvacpay.Rows.Count; i++)
            //{
            //    DateTime schdate = Convert.ToDateTime(((TextBox)this.gvacpay.Rows[i].FindControl("txtgvacScheduledate")).Text.Trim());
            //    double schamt = Convert.ToDouble("0" + ((TextBox)this.gvacpay.Rows[i].FindControl("txtgvacschamt")).Text.Trim());

            //    lst[i].schdate = schdate;
            //    lst[i].schamt = schamt;
            //}
            //Session["tblacamt"] = lst;
            //this.Data_Bind();


        }
        protected void lbtnAdddsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            int lrow = lst.Count;
            string gcod = "";
            string gdesc = "";

            DateTime insdate = lst[lrow - 1].schdate;
            double schinsamt = lst[lrow - 1].schamt;
            lst.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule(gcod, gdesc, insdate, schinsamt));
            this.Data_Bind();
        }

        protected void lbtnDeldsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            lst.RemoveAt(rowindex);
            Session["tbldschamt"] = lst;
            this.Data_Bind();

        }

        protected void lbtnAddacsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            int lrow = lst.Count;
            DateTime insdate = lst[lrow - 1].schdate;
            double schinsamt = lst[lrow - 1].schamt;
            lst.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(insdate, schinsamt));
            this.Data_Bind();
        }

        protected void lbtnDelacsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            lst.RemoveAt(rowindex);
            Session["tblacamt"] = lst;
            this.Data_Bind();


        }




        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //((Label)this.Master.FindControl("lblmsg")).Visible = true;
                //string pactcode = this.ddlProjectName.SelectedValue.ToString();

                //string usircode = this.lblCode.Text.Trim();
              

              
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = this.GetCompCode();
                //string usrid = hst["usrid"].ToString();
                //string Postusrid = hst["usrid"].ToString();
                //string trmnid = hst["compname"].ToString();
                //string session = hst["session"].ToString();
                //string PostedDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                //string entryben = Convert.ToDouble("0" + this.txtentryben.Text.Trim()).ToString();
                //string delaychrg = Convert.ToDouble("0" + this.txtdelaychrg.Text.Trim()).ToString();

                //string discount = Convert.ToDouble("0" + this.txtdiscount.Text).ToString() ;
                //string Parking = Convert.ToDouble("0" + this.txtParking.Text).ToString();


                //DataSet ds1 = new DataSet("ds1");
                ////Table Schdule
                //DataTable dt1 = new DataTable();
                //dt1.Columns.Add("gcod", typeof(string));
                //dt1.Columns.Add("gdesc", typeof(string));
                //dt1.Columns.Add("schamt", typeof(Double));
                //dt1.Columns.Add("schdate", typeof(DateTime));

                //// Table Actual
                //DataTable dt2 = new DataTable();
                //dt2.Columns.Add("schamt", typeof(Double));
                //dt2.Columns.Add("schdate", typeof(DateTime));



                //List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
                //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

                //dt1 = ASITUtility03.ListToDataTable(lstd);
                //dt2 = ASITUtility03.ListToDataTable(lsta);

               


                //ds1.Merge(dt1);
                //ds1.Merge(dt2);
                ////ds1.Merge(dt3);
                ////ds1.Merge(dt3);
                ////ds1.Merge(dt4);
                ////ds1.Merge(dt5);
                //ds1.Tables[0].TableName = "tbl1";
                //ds1.Tables[1].TableName = "tbl2";

                ////string xml = ds1.GetXml();
                ////return;
                //bool resulta = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "UPDATEDUMMYPAYMENTUSERWISE", ds1, null, null, pactcode, usircode,usrid, discount, Parking, entryben, delaychrg, Postusrid, trmnid, session, PostedDate);

                //if (!resulta)
                //{

                //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + MktData.ErrorObject["Mesage"].ToString();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;

                //}



                //((Label)this.Master.FindControl("lblmsg")).Text = "Updated SUccessfully";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                //this.ShowData();


            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
     
       
      
      

      
        protected void lbtnAddInstallment_Click(object sender, EventArgs e)
        {
            

        }

        protected void lnkbtnFindProject_Click(object sender, EventArgs e)
        {

        }

      

        protected void lbtnDelacshall_Click(object sender, EventArgs e)
        {



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string txtsrchisn = this.txtsrchInstallment.Text.Trim() + "%";
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETINSTALLMENT", txtsrchisn, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.ddlInstallment.Items.Clear();
            //    return;

            //}

            //List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            //int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //lst.RemoveAt(rowindex);
            //Session["tbldschamt"] = lst;
            //this.Data_Bind();
        }
    }
}

