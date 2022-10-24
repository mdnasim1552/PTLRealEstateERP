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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
using RealERPEntity;
using RealERPWEB.Service;
namespace RealERPWEB.F_22_Sal
{
    public partial class MktSampleNoteSheet : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        Common objcom = new Common();
        AutoCompleted AutoData = new AutoCompleted();

        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string TypeDesc = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sample Note Sheet";

                Session.Remove("Unit");
               string date= System.DateTime.Today.ToString("dd-MMM-yyyy");
                string finsdate = Convert.ToDateTime(date).AddMonths(1).ToString("dd-MMM-yyyy");
               
                this.txtcoffBookingdate.Text = date;
                this.txtcoffdownpaydate.Text = date;
                this.txtcoffinsdate.Text = finsdate;
              
                this.GetProjectName();
                this.GetProspective();
                this.getInstallment();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));  
                this.gvSpayment.Columns[0].Visible = false;

               



            }

        }


        private void getInstallment()
        {
            try
            {
                Session.Remove("tblinstallment");
                string comcod = this.GetCompCode();
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETINSTALLMENT", "", "", "", "", "", "", "", "", "");               
                Session["tblinstallment"] = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode>(); ;
              

                this.ddlInstallment.DataTextField = "gdesc";
                this.ddlInstallment.DataValueField = "gcod";
                this.ddlInstallment.DataSource = ds1.Tables[0];
                this.ddlInstallment.DataBind();
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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPROSPECTIVE", txtSProject, "", "", "", "", "", "", "", "");
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

           
            string PactCode =this.ddlPrevious.Items.Count>0 ? ((DataTable)ViewState["tblprenotesheet"]).Select("noteshtid='"+notesheetno+"'")[0]["pactcode"].ToString() : this.ddlProjectName.SelectedValue.ToString();


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
            string pactcode =  ((DataTable)ViewState["tblprenotesheet"]).Select("noteshtid='" + notesheetno + "'")[0]["pactcode"].ToString() ;

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
            Session["lstrevschedule"] = lstrev;

            this.ddlprospective.SelectedValue=ds1.Tables[0].Rows[0]["proscode"].ToString();
            this.ddlcoffduration.SelectedValue=ds1.Tables[0].Rows[0]["coffdur"].ToString();
         



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
            var lstcoff = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> ();
            var lstrev = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> ();
            Session["lstbaseschdule"] = lstb;
            Session["lstcoffschedule"] = lstcoff;
            Session["lstrevschedule"] = lstrev;
            this.CalCulationSummation(ds1);
            this.Data_Bind();
        }

        private void CalCulationSummation(DataSet ds1)
        {

            DateTime sysdate=System.DateTime.Today;
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>)Session["lstrevschedule"];


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
            cofffvpsft = ((uzize > 0) ? (cofffv==0?0.00:(cofffv - coffpamt - coffutility - coffothers) / uzize) : 0.00);
            coffpowbpart = (12 + intratio) / 12;
            coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, coffnoofemi)), 0);
            



            revfv = lstrev.Sum(l => l.fv);
            revpv = lstrev.Sum(l => l.pv);
            revpamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["revpamt"]);
            revutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["revutility"]);
            revothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["revothers"]);
            revnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["revnoofemi"]);
            revfvpsft = ((uzize > 0) ? (revfv == 0 ? 0.00 : (revfv - revpamt - revutility - revothers) / uzize) : 0.00);
            revpowbpart = (12 + intratio) / 12;
            revpvpsft = Math.Round(revfvpsft / (Math.Pow(revpowbpart, noofemi)), 0);

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
            this.txtcofffininsper.Text = 0.ToString("#,##0;(#,##0);");
            this.lblvalcofffininsam.InnerText = 0.ToString("#,##0;(#,##0);");
            this.txtcofffininsdate.Text = sysdate.AddMonths((int)coffnoofemi + 1).ToString("dd-MMM-yyyy");

            this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
            this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");


          



        }

        private void CalculationValue()
        {

            try
            {


                int badins = 0;

                double intratio, usize,  coffurate, coffuamt, coffpamt, coffutility, coffothers, cofftunitamt, coffbookingper, coffbookingam, coffdpaymentper, coffdpaymentam, coffnoofemi, coffemi, cofffvpsft, coffpvpsft, coffpowbpart, cofffv, coffpv,   finalinsper, finalinsam;

               
                usize = Convert.ToDouble(this.lblvalcoffarea.InnerText.ToString());
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

                coffdpaymentper = Convert.ToDouble("0" + this.txtcoffdownpayper.Text.ToString());
                coffdpaymentam = cofftunitamt * 0.01 * coffdpaymentper;

                coffnoofemi = Convert.ToDouble("0" + this.txtcoffnooffemi.Text.ToString());
                
                finalinsper = Convert.ToDouble("0" + this.txtcofffininsper.Text.ToString());
                finalinsam = cofftunitamt * 0.01 * finalinsper;

                //Booking and Downpayment and Final Installment
               // badins = coffbookingam > 0 ? ++badins : badins;
               // badins = coffdpaymentam > 0 ? ++badins : badins;
                badins = finalinsam > 0 ? ++badins : badins;


                coffemi = Math.Round((coffnoofemi > 0 ? (cofftunitamt - coffbookingam- coffdpaymentam - finalinsam) / (coffnoofemi- badins) : 0.00), 0);

                this.lblcoffunitprice.InnerText = coffuamt.ToString("#,##0;(#,##0);");
                this.txtcofffparking.Text = coffpamt.ToString("#,##0;(#,##0);");
                this.txtcoffutility.Text = coffutility.ToString("#,##0;(#,##0);");
                this.txtcoffothers.Text = coffothers.ToString("#,##0;(#,##0);");
                this.lblcoffTotal.InnerText = cofftunitamt.ToString("#,##0;(#,##0);");
                this.txtcoffbookinmpercnt.Text = coffbookingper.ToString("#,##0;(#,##0);");
                this.lblvalcoffbookingam.InnerText = coffbookingam.ToString("#,##0;(#,##0);");

                this.txtcoffdownpayper.Text = coffdpaymentper.ToString("#,##0;(#,##0);");
                this.lblvalcoffdownpayam.InnerText = coffdpaymentam.ToString("#,##0;(#,##0);");

                this.txtcoffnooffemi.Text = coffnoofemi.ToString("#,##0;(#,##0);");
                this.lblvalcoffemi.InnerText = coffemi.ToString("#,##0;(#,##0);");                
                this.txtcofffininsper.Text = finalinsper.ToString("#,##0;(#,##0);");
                this.lblvalcofffininsam.InnerText = finalinsam.ToString("#,##0;(#,##0);");
                //this.txtcofffininsdate=

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



               





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }
        }
        private void SaveValue()
        {

            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
             double pv;
            int i=0;
            foreach (GridViewRow gv1 in gvcoffsch.Rows)
            {
                DateTime schdate =Convert.ToDateTime(((TextBox)gv1.FindControl("txtgvScheduledate")).Text.Trim());
                string monthid = schdate.ToString("yyyyMM");
                string ymon = schdate.ToString("MMM-yy");

                pv =ASTUtility.StrNagative(((TextBox)gv1.FindControl("txtgvdumschamt")).Text.Trim());
                lstcoff[i].pv = pv;
                lstcoff[i].monthid = monthid;
                lstcoff[i].ymon = ymon;
                i++;



            }

           
            Session["lstcoff"] = lstcoff;
            this.Data_Bind();


        }

        private void CalCulationInstallment()
        {

            try

            {



                int coffnoofemi, initial, mondiff, finalins;
                finalins = 0;
                double cofftunitamt, coffbookingam, coffdpaymentam, coffemi, coffpowbpart, intratio, coffresamt, finalinsam;
                string monthid, gcod, gdesc, ymon, grp;
                double pv, fv;
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode> lstcode = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode>)Session["tblinstallment"];

                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = new List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>();
               
                intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;
                // Customer Offer
                coffbookingam = Convert.ToDouble("0" + this.lblvalcoffbookingam.InnerText);
                coffdpaymentam = Convert.ToDouble("0" + this.lblvalcoffdownpayam.InnerText);
                coffnoofemi = Convert.ToInt32("0" + this.txtcoffnooffemi.Text.ToString());
                coffemi = Convert.ToDouble("0" + this.lblvalcoffemi.InnerText);
                finalinsam= Convert.ToDouble("0" + this.lblvalcofffininsam.InnerText);
                int coffdur = Convert.ToInt32(this.ddlcoffduration.SelectedValue.ToString());
                // set @cintexpart = power((12 + @intratio) / 12, @mondiff)
                coffpowbpart = (12 + intratio) / 12;

                DateTime  bookingdate, dpaymentdate, firstinsdate, finalinsdate;
              //  strtdate = System.DateTime.Today;
                bookingdate = Convert.ToDateTime(this.txtcoffBookingdate.Text);
                dpaymentdate= Convert.ToDateTime(this.txtcoffdownpaydate.Text);
                firstinsdate = Convert.ToDateTime(this.txtcoffinsdate.Text);
                finalinsdate =Convert.ToDateTime( this.txtcofffininsdate.Text);
              
             
                initial = 0;


                //Booking Money
                if (coffbookingam > 0)
                {
                    var lstins = lstcode.FindAll(l => l.gcod.Substring(0, 5) == "81001");
                    monthid = bookingdate.ToString("yyyyMM");
                    ymon = bookingdate.ToString("MMM-yy");
                    gcod = lstins[0].gcod;
                    gdesc = lstins[0].gdesc;
                    grp = "02";
                    mondiff = ASTUtility.Datediff(finalinsdate, bookingdate);

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
                    mondiff = ASTUtility.Datediff(finalinsdate, dpaymentdate);

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
                    mondiff = ASTUtility.Datediff(finalinsdate, finalinsdate);


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


                var lstfoins = lstcode.FindAll(l => l.gcod.Substring(0, 5)!= "81001" && l.gcod.Substring(0, 5) != "81002" && l.gcod.Substring(0, 5) != "81985");
                int k = lstcoff.Count;
               
                int ins = 0;
                for (int j = k; j < coffnoofemi + k; j++)
                {


                   

                    monthid = finalinsdate.ToString("yyyyMM");
                    ymon = finalinsdate.ToString("MMM-yy");
                    gcod = lstfoins[ins].gcod;
                    gdesc = lstfoins[ins].gdesc;
                    grp = "02";
                    mondiff = ASTUtility.Datediff(finalinsdate, firstinsdate);




                    if (initial == coffnoofemi+k-1) // Last Installment
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
        }

        protected void lbtnDeldsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lst = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            lst.RemoveAt(rowindex);
            Session["lstcoffschedule"] = lst;
            this.Data_Bind();

        }


        protected void chkSegment_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlSlab.Visible = this.chkSegment.Checked;
        }


        protected void lbtnAddInstallment_Click(object sender, EventArgs e)
        {

            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lst = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            int lrow = lst.Count;

           
            string monthid = lst[lrow - 1].monthid;
            DateTime schdate = lst[lrow - 1].schdate;          
            string grp = "02";
            string ymon = lst[lrow - 1].ymon;
            double pv = lst[lrow - 1].pv;
            double fv = lst[lrow - 1].fv;
            string gcod = this.ddlInstallment.SelectedValue.ToString();
            string gdesc = this.ddlInstallment.SelectedItem.Text;


            RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
            {
                monthid = monthid,
                ymon = ymon,
              
                gcod = gcod,
                gdesc = gdesc,
                schdate = schdate,
                grp = grp,
                pv = pv,
                fv = fv
            };


            lst.Add(obj);
            var lst1 = lst.OrderBy(l => l.monthid).ToList();
            Session["lstcoffschedule"] = lst1;
            this.Data_Bind();


        }


        protected void lbtnSlab_Click(object sender, EventArgs e)
        {

           
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            int strins, endins; double insamt;
            strins = Convert.ToInt32("0" + this.txtfrmslab.Text.Trim());
            endins = Convert.ToInt32("0" + this.txttoslab.Text.Trim());
            insamt = Convert.ToDouble("0" + this.txtperslabamt.Text.Trim());
            int drowcount = lstcoff.Count;
            endins = endins > drowcount ? drowcount : endins;
            for (int i = strins - 1; i < endins; i++)
            {
                lstcoff[i].pv = insamt;

            }
            Session["lstcoff"] = lstcoff;
            this.Data_Bind();


        }



        private void Data_Bind()
        {
            try
            {



                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>)Session["lstrevschedule"];
                

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
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>)Session["lstrevschedule"];

              
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


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.ddlPrevious.Items.Count == 0)
                    this.LastGrandNoteSheet();

                string noteshtid = this.ddlPrevious.SelectedValue.ToString();
                string noteshtdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.lblCode.Text.Trim();
                string intrate = Convert.ToDouble("0" + this.txtinterestrate.Text.Replace("%", "")).ToString();


                string coffurate = Convert.ToDouble("0" + this.txtcoffrate.Text).ToString();
                string coffpamt = Convert.ToDouble("0" + this.txtcofffparking.Text).ToString();
                string coffutility = Convert.ToDouble("0" + this.txtcoffutility.Text).ToString();
                string coffothers = Convert.ToDouble("0" + this.txtcoffothers.Text).ToString();
                string coffbookingper = Convert.ToDouble("0" + this.txtcoffbookinmpercnt.Text).ToString();
                string coffbookingam = Convert.ToDouble("0" + this.lblvalcoffbookingam.InnerText).ToString();
                string coffbookdate=  this.txtcoffBookingdate.Text;
                string coffdpaymentper = Convert.ToDouble("0" + this.txtcoffdownpayper.Text).ToString();
                string coffdpaymentam = Convert.ToDouble("0" + this.lblvalcoffdownpayam.InnerText).ToString();
                string coffdpaymentdate = this.txtcoffdownpaydate.Text;

                string cofffinalinsper = Convert.ToDouble("0" + this.txtcofffininsper.Text).ToString();
                string cofffinalinsam = Convert.ToDouble("0" + this.lblvalcofffininsam.InnerText).ToString();
                string cofffinalinsdate = this.txtcofffininsdate.Text;
                string coffnooffemi = Convert.ToDouble("0" + this.txtcoffnooffemi.Text).ToString();               
                string coffduration = this.ddlcoffduration.SelectedValue.ToString();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string usrid = hst["usrid"].ToString();
                string Postusrid = hst["usrid"].ToString();
                string trmnid = hst["compname"].ToString();
                string session = hst["session"].ToString();
                string PostedDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
                string proscode = this.ddlprospective.SelectedValue.ToString();
                bool resulta = false;
                resulta = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "DELETENOTESHEET", noteshtid, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!resulta)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                    return;

                }

                //return;

                resulta = MktData.UpdateTransInfo01(comcod, "SP_ENTRY_SALESNOTESHEET", "INSERTORUPDATESALESNOTESHEET", noteshtid, pactcode, usircode, noteshtdate, intrate, coffurate, coffpamt, coffutility, coffothers, coffbookdate, coffdpaymentper, coffdpaymentam, coffdpaymentdate, cofffinalinsper, cofffinalinsam, cofffinalinsdate, coffnooffemi, coffduration, proscode, Postusrid, trmnid, session, PostedDate, coffbookingper, coffbookingam, "", "");
                if (!resulta)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                    return;

                }



                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstbcase = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet> lstrev = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>)Session["lstrevschedule"];

                foreach (RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet lstitem in lstbcase)
                {
                    string gcod = lstitem.gcod;
                    string grp = lstitem.grp;
                    string monthid = lstitem.monthid;
                    string pv = Convert.ToDouble(lstitem.pv).ToString();
                    string fv = Convert.ToDouble(lstitem.fv).ToString();
                    string schdate = lstitem.schdate.ToString("dd-MMM-yyyy");



                    resulta = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "INSERTORUPDATESALESNOTESHEETDETAILS", noteshtid, gcod, grp, pv, fv, schdate, "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                        return;

                    }



                }


                foreach (RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet lstitem in lstcoff)
                {
                    string gcod = lstitem.gcod;
                    string grp = lstitem.grp;                                  
                    string pv = Convert.ToDouble(lstitem.pv).ToString();
                    string fv = Convert.ToDouble(lstitem.fv).ToString();
                    string schdate = lstitem.schdate.ToString("dd-MMM-yyyy");



                    resulta = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "INSERTORUPDATESALESNOTESHEETDETAILS", noteshtid, gcod, grp, pv, fv, schdate, "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                        return;

                    }



                }







                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

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
     
       
      
      

      
      
        protected void lnkbtnFindProject_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnProspective_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnPrevious_Click(object sender, EventArgs e)
        {

            try
            {


                ViewState.Remove("tblprenotesheet");
                string comcod = this.GetCompCode();
                string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPREVIOUSNOTESHEETNO", "",
                       date, "", "", "", "", "", "", "");
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

     
        protected void chkAddIns_CheckedChanged(object sender, EventArgs e)
        {
            this.PanelAddIns.Visible = this.chkAddIns.Checked;
        }

    








    }
}

