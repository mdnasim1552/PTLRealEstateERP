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
               string date= System.DateTime.Today.ToString("dd-MMM-yyyy");
                string finsdate = Convert.ToDateTime(date).AddMonths(1).ToString("dd-MMM-yyyy");
                this.txtBookingdate.Text = date;
                this.txtfirstinsdate.Text = finsdate;
                this.txtcoffBookingdate.Text = date;
                this.txtcoffinsdate.Text = finsdate;
                this.txtrevpBookingdate.Text = date;
                this.txtrevpinsdate.Text = finsdate;
                this.GetProjectName();
                this.GetProspective();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));  
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
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usircode = this.lblCode.Text;
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
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
            revpvpsft = Math.Round(revfvpsft / (Math.Pow(revpowbpart, noofemi)), 0);

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
            string coffemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffemi"]).ToString("#,##0;(#,##0);");
            string cofffvpersft = cofffvpsft.ToString("#,##0;(#,##0);");
            string coffpvpersft = coffpvpsft.ToString("#,##0;(#,##0);");


            string revparea = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
            string revprate = Convert.ToDouble(ds1.Tables[0].Rows[0]["revurate"]).ToString("#,##0;(#,##0);");
            string revpunitprice = Convert.ToDouble(ds1.Tables[0].Rows[0]["revuamt"]).ToString("#,##0;(#,##0);");
            string revpparking = Convert.ToDouble(ds1.Tables[0].Rows[0]["revpamt"]).ToString("#,##0;(#,##0);");
            string revputility = Convert.ToDouble(ds1.Tables[0].Rows[0]["revutility"]).ToString("#,##0;(#,##0);");
            string revpothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["revothers"]).ToString("#,##0;(#,##0);");
            string revpTotal = Convert.ToDouble(ds1.Tables[0].Rows[0]["revtunitamt"]).ToString("#,##0;(#,##0);");
            string revpbbookinmpercnt = Convert.ToDouble(ds1.Tables[0].Rows[0]["revbookingper"]).ToString("#,##0;(#,##0);");
            string revpbookingam = Convert.ToDouble(ds1.Tables[0].Rows[0]["revbookingam"]).ToString("#,##0;(#,##0);");
            string revpnooffemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["revnoofemi"]).ToString("#,##0;(#,##0);");
            string revpemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["revemi"]).ToString("#,##0;(#,##0);");
            string revpfvpersft = revfvpsft.ToString("#,##0;(#,##0);");
            string revppvpersft = revpvpsft.ToString("#,##0;(#,##0);");

            DataTable dt = (DataTable)ds1.Tables[1];
            DataTable dt1 = (DataTable)ds1.Tables[2];
            DataTable dt2 = (DataTable)ds1.Tables[3];

            LocalReport Rpt1 = new LocalReport();
            var lst1 = dt.DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>();
            var lst2 = dt1.DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>();
            var lst3 = dt2.DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet>();


            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptGrandNotesSheet", lst1, lst2, lst3);
            Rpt1.EnableExternalImages = true;
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
            //
            Rpt1.SetParameters(new ReportParameter("revparea", revparea));
            Rpt1.SetParameters(new ReportParameter("revprate", revprate));
            Rpt1.SetParameters(new ReportParameter("revpunitprice", revpunitprice));
            Rpt1.SetParameters(new ReportParameter("revpparking", revpparking));
            Rpt1.SetParameters(new ReportParameter("revputility", revputility));
            Rpt1.SetParameters(new ReportParameter("revpothers", revpothers));
            Rpt1.SetParameters(new ReportParameter("revpTotal", revpTotal));
            Rpt1.SetParameters(new ReportParameter("revpbbookinmpercnt", revpbbookinmpercnt));
            Rpt1.SetParameters(new ReportParameter("revpbookingam", revpbookingam));
            Rpt1.SetParameters(new ReportParameter("revpnooffemi", revpnooffemi));
            Rpt1.SetParameters(new ReportParameter("revpemi", revpemi));
            Rpt1.SetParameters(new ReportParameter("revpfvpersft", revpfvpersft));
            Rpt1.SetParameters(new ReportParameter("revppvpersft", revppvpersft));



            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Grand Note Sheet"));
            // Rpt1.SetParameters(new ReportParameter("projectName", projectName));

            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            revpvpsft = Math.Round(revfvpsft / (Math.Pow(revpowbpart, noofemi)), 0);






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
            this.lblvalfvpsft.InnerText = bfvpsft.ToString("#,##0;(#,##0);");
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



        }

        private void CalculationValue()
        {

            try
            {


              

                double intratio, usize, bnoofemi, coffurate, coffuamt, coffpamt, coffutility, coffothers, cofftunitamt, coffbookingper, coffbookingam, coffnoofemi, coffemi, cofffvpsft, coffpvpsft, coffpowbpart, cofffv, coffpv, revurate, revuamt, revpamt, revutility, revothers, revtunitamt, revbookingper, revbookingam, revfvpsft, revpvpsft, revpowbpart, revnoofemi, revemi, revfv, revpv;

                bnoofemi = Convert.ToDouble("0" + this.lblvalnoofemi.InnerText);
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
                revpvpsft = Math.Round(revfvpsft / (Math.Pow(revpowbpart, bnoofemi)), 0);

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
                double cofftunitamt, coffbookingam, coffemi, coffpowbpart, intratio, coffresamt, revtunitamt, revbookingam, revemi, revpowbpart, revresamt;
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

                DateTime strtdate, enddate, benddate;
                strtdate = System.DateTime.Today;
                enddate = strtdate.AddMonths(coffnoofemi*coffdur);
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
                            fv = Math.Round(coffbookingam * Math.Pow(coffpowbpart, mondiff),0)
                        };
                        lstcoff.Add(obj);
                      

                    }

                    else  if (initial == coffnoofemi)
                    {
                        
                        cofftunitamt = Convert.ToDouble("0" + this.lblcoffTotal.InnerText);
                        coffresamt = cofftunitamt-lstcoff.Sum(l => l.pv);

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = coffresamt,
                            fv = Math.Round(coffresamt * Math.Pow(coffpowbpart, mondiff),0)
                        };
                        lstcoff.Add(obj);

                    }


                    else
                    {

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = coffemi,
                            fv = Math.Round(coffemi * Math.Pow(coffpowbpart, mondiff),0)
                        };
                        lstcoff.Add(obj);
                    }
                    initial++;
                    strtdate = strtdate.AddMonths(coffdur);
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
                enddate = strtdate.AddMonths(revnoofemi*revdur);
                benddate= strtdate.AddMonths(bnoofemi);
                benddate = enddate > benddate ? enddate : benddate;
                initial = 0;

                while (strtdate <= enddate)
                {
                    monthid = strtdate.ToString("yyyyMM");
                    ymon = strtdate.ToString("MMM-yy");
                    grp = "03";
                    mondiff = ASTUtility.Datediff(benddate, strtdate);

                    if (initial == 0)
                    {
                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = revbookingam,
                            fv = Math.Round(revbookingam * Math.Pow(revpowbpart, mondiff),0)
                        };
                        lstrev.Add(obj);
                       

                    }


                   else if (initial == revnoofemi)
                    {

                        revtunitamt = Convert.ToDouble("0" + this.lblrevpTotal.InnerText);
                        revresamt = revtunitamt - lstrev.Sum(l => l.pv);

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = revresamt,
                            fv = Math.Round(revresamt * Math.Pow(revpowbpart, mondiff),0)
                        };
                        lstrev.Add(obj);


                    }


                    

                    else
                    {

                        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet
                        {
                            monthid = monthid,
                            ymon = ymon,
                            grp = grp,
                            pv = revemi,
                            fv = Math.Round(revemi * Math.Pow(revpowbpart, mondiff),0)
                        };
                        lstrev.Add(obj);
                    }

                    strtdate = strtdate.AddMonths(revdur);
                    initial++;
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


       
        protected void lbtnAddacsch_Click(object sender, EventArgs e)
        {
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            //int lrow = lst.Count;
            //DateTime insdate = lst[lrow - 1].schdate;
            //double schinsamt = lst[lrow - 1].schamt;
            //lst.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(insdate, schinsamt));
            //this.Data_Bind();
        }

        protected void lbtnDelacsch_Click(object sender, EventArgs e)
        {
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            //int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //lst.RemoveAt(rowindex);
            //Session["tblacamt"] = lst;
            //this.Data_Bind();


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

                string coffurate = Convert.ToDouble("0" + this.txtcoffrate.Text).ToString() ;
                string coffpamt = Convert.ToDouble("0" + this.txtcofffparking.Text).ToString();
                string coffutility = Convert.ToDouble("0" + this.txtcoffutility.Text).ToString();
                string coffothers = Convert.ToDouble("0" + this.txtcoffothers.Text).ToString();
                string coffbookingper = Convert.ToDouble("0" + this.txtcoffbookinmpercnt.Text).ToString();
                string coffbookingam = Convert.ToDouble("0" + this.lblvalcoffbookingam.InnerText).ToString();
                string coffnooffemi = Convert.ToDouble("0" + this.txtcoffnooffemi.Text).ToString();
                string coffemi = Convert.ToDouble("0" + this.lblvalcoffemi.InnerText).ToString();

                string revurate = Convert.ToDouble("0" + this.txtrevprate.Text).ToString();              
                string  revpamt = Convert.ToDouble("0" + this.txtrevpparking.Text).ToString();
                string  revutility = Convert.ToDouble("0" + this.txtrevputility.Text).ToString();
                string  revothers = Convert.ToDouble("0" + this.txtrevpothers.Text).ToString();

                string revbookingper = Convert.ToDouble("0" + this.txtrevpbbookinmpercnt.Text).ToString();
                string revbookingam = Convert.ToDouble("0" + this.lblvalrevpbookingam.InnerText).ToString();
                string revnooffemi = Convert.ToDouble("0" + this.txtrevpnooffemi.Text).ToString();
                string revemi = Convert.ToDouble("0" + this.lblvalrevpemi.InnerText).ToString();


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string usrid = hst["usrid"].ToString();
                string Postusrid = hst["usrid"].ToString();
                string trmnid = hst["compname"].ToString();
                string session = hst["session"].ToString();
                string PostedDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
                string proscode = this.ddlprospective.SelectedValue.ToString();
                bool resulta = false;
                 resulta = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "DELETENOTESHEET", noteshtid, "", "", "", "", "", "", "", "","", "", "", "", "");
                if (!resulta)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                    return;

                }

                //return;

                 resulta = MktData.UpdateTransInfo01(comcod, "SP_ENTRY_SALESNOTESHEET", "INSERTORUPDATESALESNOTESHEET", noteshtid, pactcode, usircode, noteshtdate, intrate, coffurate, coffpamt, coffutility, coffothers, revurate, revpamt, revutility, revothers, Postusrid, trmnid, session, PostedDate, proscode, coffbookingper, coffbookingam, coffnooffemi,coffemi,revbookingper,revbookingam,revnooffemi, revemi);
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

                    string grp = lstitem.grp;
                    string monthid = lstitem.monthid;
                    string pv = Convert.ToDouble(lstitem.pv).ToString();
                    string fv = Convert.ToDouble(lstitem.fv).ToString();



                    resulta = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "INSERTORUPDATESALESNOTESHEETDETAILS", noteshtid, monthid, grp, pv, fv, "", "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                        return;

                    }



                }


                foreach (RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet lstitem in lstcoff)
                {

                    string grp = lstitem.grp;
                    string monthid = lstitem.monthid;
                    string pv =Convert.ToDouble(lstitem.pv).ToString();
                    string fv = Convert.ToDouble(lstitem.fv).ToString();



                    resulta = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "INSERTORUPDATESALESNOTESHEETDETAILS", noteshtid, monthid, grp, pv, fv, "", "","","", "", "", "","","","");
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                        return;

                    }



                }



                foreach (RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassRevGrandNoteSheet lstitem in lstrev)
                {

                    string grp = lstitem.grp;
                    string monthid = lstitem.monthid;
                    string pv = Convert.ToDouble(lstitem.pv).ToString();
                    string fv = Convert.ToDouble(lstitem.fv).ToString();



                    resulta = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "INSERTORUPDATESALESNOTESHEETDETAILS", noteshtid, monthid, grp, pv, fv, "", "", "", "", "", "", "", "", "", "");
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
     
       
      
      

      
        protected void lbtnAddInstallment_Click(object sender, EventArgs e)
        {
            

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

