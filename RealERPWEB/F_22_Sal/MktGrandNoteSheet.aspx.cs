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
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
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



            double uzize, bfv, bpv, pamt, utility, others, intratio, noofemi,
                cofffv, coffpv, coffpamt, coffutility, coffothers, cofffvpsft, coffpvpsft, coffpowbpart, coffnoofemi;


            //uzize =
            bfv = lstb.Sum(l => l.fv);
            bpv = lstb.Sum(l => l.pv);
            uzize = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]);
            pamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["pamt"]);
            utility = Convert.ToDouble(ds1.Tables[0].Rows[0]["utility"]);
            others = Convert.ToDouble(ds1.Tables[0].Rows[0]["others"]);
            intratio = Convert.ToDouble(ds1.Tables[0].Rows[0]["intratio"]);
            noofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]);

            //this.lblhiddenbpamt.Value = pamt.ToString("#,##0;(#,##0);");
            //this.lblhiddenbutility.Value = utility.ToString("#,##0;(#,##0);");
            //this.lblhiddenothers.Value = others.ToString("#,##0;(#,##0);");
            //this.lblhiddenbnoemi.Value = noofemi.ToString();
            //this.lblhiddenbstrtdate.Value = lstb[0].schdate.ToString("dd-MMM-yyyy");
            //this.lblhiddenbenddate.Value = lstb[lstb.Count - 1].schdate.ToString("dd-MMM-yyyy");
            cofffv = lstcoff.Sum(l => l.fv);
            coffpv = lstcoff.Sum(l => l.pv);
            coffpamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]);
            coffutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]);
            coffothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]);
            coffnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]);
            cofffvpsft = ((uzize > 0) ? (cofffv == 0 ? 0.00 : (cofffv - coffpamt - coffutility - coffothers) / uzize) : 0.00);
            coffpowbpart = (12 + intratio) / 12;
            coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, coffnoofemi)), 0);
            this.lblvalcoffarea.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            this.txtcoffrate.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffurate"]).ToString("#,##0;(#,##0);");
            this.lblcoffunitprice.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffuamt"]).ToString("#,##0;(#,##0);");
            this.txtcofffparking.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]).ToString("#,##0;(#,##0);");
            this.txtcoffutility.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]).ToString("#,##0;(#,##0);");
            this.txtcoffothers.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]).ToString("#,##0;(#,##0);");
            this.lblcoffTotal.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["cofftunitamt"]).ToString("#,##0;(#,##0);");
            this.txtcoffbookinmpercnt.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffbookingper"]).ToString("#,##0;(#,##0);");
            this.lblvalcoffbookingam.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffbookingam"]).ToString("#,##0;(#,##0);");
            this.txtcoffBookingdate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd-MMM-yyyy");
            this.txtcoffdownpayper.Text = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntper"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntper"]).ToString("#,##0;(#,##0);");
            this.lblvalcoffdownpayam.InnerText = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]).ToString("#,##0;(#,##0);");
            this.txtcoffdownpaydate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffdpaymntdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffdpaymntdat"]).ToString("dd-MMM-yyyy");


            this.txtcofffininsper.Text = (Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsper"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsper"]).ToString("#,##0;(#,##0);");
            this.lblvalcofffininsam.InnerText = (Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsam"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["cofffinsam"]).ToString("#,##0;(#,##0);");
            this.txtcofffininsdate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["cofffinsdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? sysdate.AddMonths((int)coffnoofemi).ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["cofffinsdat"]).ToString("dd-MMM-yyyy");



            this.txtcoffnooffemi.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]).ToString("#,##0;(#,##0);");
            this.lblvalcoffemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffemi"]).ToString("#,##0;(#,##0);");




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


                coffemi = Math.Round((coffnoofemi > 0 ? (cofftunitamt - coffbookingam - coffdpaymentam - finalinsam) / (coffnoofemi - badins) : 0.00), 0);

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


                int count = lstcoff.Count;
                coffbookingdate = Convert.ToDateTime(lstcoff[0].schdate);
                finalinsdate = Convert.ToDateTime(lstcoff[count - 1].schdate);
                benddate = Convert.ToDateTime(this.lblhiddenbenddate.Value);
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

                // List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode> lstcode = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode>)Session["tblinstallment"];

                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = new List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>();

                intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;
                // Customer Offer
                coffbookingam = Convert.ToDouble("0" + this.lblvalcoffbookingam.InnerText);
                coffdpaymentam = Convert.ToDouble("0" + this.lblvalcoffdownpayam.InnerText);
                coffnoofemi = Convert.ToInt32("0" + this.txtcoffnooffemi.Text.ToString());
                coffemi = Convert.ToDouble("0" + this.lblvalcoffemi.InnerText);
                finalinsam = Convert.ToDouble("0" + this.lblvalcofffininsam.InnerText);
                int coffdur = Convert.ToInt32(this.ddlcoffduration.SelectedValue.ToString());
                // set @cintexpart = power((12 + @intratio) / 12, @mondiff)
                coffpowbpart = (12 + intratio) / 12;

                DateTime bookingdate, dpaymentdate, firstinsdate, finalinsdate;
                //  strtdate = System.DateTime.Today;
                bookingdate = Convert.ToDateTime(this.txtcoffBookingdate.Text);
                dpaymentdate = Convert.ToDateTime(this.txtcoffdownpaydate.Text);
                firstinsdate = Convert.ToDateTime(this.txtcoffinsdate.Text);
                finalinsdate = Convert.ToDateTime(this.txtcofffininsdate.Text);
                bnoofemi = Convert.ToInt32(this.lblhiddenbnoemi.Value);

                //this.lblhiddenbnoemi.Value = noofemi.ToString();
                //this.lblhiddenbstrtdate.Value = lstb[0].schdate.ToString("dd-MMM-yyyy");
                //this.lblhiddenbenddate.Value = lstb[lstb.Count - 1].schdate.ToString("dd-MMM-yyyy");

                // strtdate =Convert.ToDateTime(this.lblhiddenbstrtdate.Value);
                benddate = Convert.ToDateTime(this.lblhiddenbenddate.Value);
                //benddate = strtdate.AddMonths(bnoofemi);
                benddate = finalinsdate > benddate ? finalinsdate : benddate;



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

      

      


      

      



        private void Data_Bind()
        {
            try
            {




                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];



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












    }
}

