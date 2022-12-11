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
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

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
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string finsdate = Convert.ToDateTime(date).AddMonths(1).ToString("dd-MMM-yyyy");

                this.txtcoffBookingdate.Text = date;
                this.txtcoffdownpaydate.Text = date;
                this.txtcoffinsdate.Text = finsdate;




                this.GetProjectName();

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



        public string GetCompCode()
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
        public string GetEmpid()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["empid"].ToString());

        }



        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetProspective(string comcod, string empid, string type)
        {




            //string number = "";
            //number = Phone.Length > 0 ? Phone + "," : "";
            //number = number + (altphone1.Length > 0 ? altphone1 + "," : "");
            //number = number + (altphone2.Length > 0 ? altphone2 + "," : "");
            //number = number.Length > 0 ? number.Substring(0, number.Length - 1) : number;

            //Check Duplicate
            //DataSet ds2 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "CHECKEDDUPUCLIENT", number, "", "", "", "", "", "", "", "");


            //if (ds2 == null)
            //{
            //    return;
            //}

            ProcessAccess _processAccess = new ProcessAccess();
            string txtSProject = "%%";

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPROSPECTIVE", txtSProject, empid, type, "", "", "", "", "", "");


            var result = ds2.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassProspective>();

            //if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            //{
            //  //  var result = new { Message = "Success", result = true };
            //    var jsonSerialiser = new JavaScriptSerializer();
            //    var json = jsonSerialiser.Serialize(result);
            //    return json;

            //}


            //else
            //{

            //DataView dv1 = ds2.Tables[0].DefaultView;
            //dv1.RowFilter = ("sircode <>'" + sircode + "'");
            //DataTable dt1 = dv1.ToTable();






            //if (dt1.Rows.Count == 0)
            //{

            //    var result = new { Message = "Success", result = true };
            //    var jsonSerialiser = new JavaScriptSerializer();
            //    var json = jsonSerialiser.Serialize(result);
            //    return json;

            //}




            //else
            //{









            //var result = new { Message = "success", result = false };
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(result);

            return json;




            //  }




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
        private void PrintSampleNoteSheet()
            
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
            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lst1 = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
            string salesteam = ds1.Tables[0].Rows[0]["pestedempname"].ToString();
            string clustername = ds1.Tables[0].Rows[0]["clustername"].ToString();
            string recommendname = ds1.Tables[0].Rows[0]["recommendname"].ToString();
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
            string cofffvpersft = Convert.ToDouble(this.lblvalcofffvpersft.InnerText).ToString("#,##0;(#,##0);");
            string coffpvpersft = Convert.ToDouble(this.lblvalcofffvpersft.InnerText).ToString("#,##0;(#,##0);");
            string cbookingdate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("MMM-yyyy");
            string cbookingday = Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd");
            string cbookingmm = Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("MM");
            string cbookingyy = Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("yy");
            string clientname = this.txtprospective.Text.Trim();









            //string Projectunit = dt.Rows[0]["munit"].ToString();
            DataTable dtsummuary = (DataTable)ViewState["tblData"];
            DataRow[] dr = dtsummuary.Select("usircode='" + usircode + "'");
            string Projectname = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string Projectdesc = dr[0]["udesc"].ToString();
            string Projectunit = dr[0]["munit"].ToString();
            string unitsize = Convert.ToDouble(dr[0]["usize"]).ToString("#,##0;(#,##0);");



            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSampleNotesSheet", lst1, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));

            Rpt1.SetParameters(new ReportParameter("Projectunit", Projectunit));
            Rpt1.SetParameters(new ReportParameter("cbookingday", cbookingday));
            Rpt1.SetParameters(new ReportParameter("cbookingmm", cbookingmm));
            Rpt1.SetParameters(new ReportParameter("cbookingyy", cbookingyy));
            Rpt1.SetParameters(new ReportParameter("clientname", clientname));
            Rpt1.SetParameters(new ReportParameter("unitsize", unitsize));

            //customer
            Rpt1.SetParameters(new ReportParameter("valcoffarea", valcoffarea));
            Rpt1.SetParameters(new ReportParameter("coffrate", coffrate));
            Rpt1.SetParameters(new ReportParameter("coffunitprice", coffunitprice));
            Rpt1.SetParameters(new ReportParameter("cofffparking", cofffparking));
            Rpt1.SetParameters(new ReportParameter("valcoffutility", valcoffutility));
            Rpt1.SetParameters(new ReportParameter("valcoffothers", valcoffothers));
            Rpt1.SetParameters(new ReportParameter("cbookingdate", cbookingdate));
            Rpt1.SetParameters(new ReportParameter("coffTotal", coffTotal));
            Rpt1.SetParameters(new ReportParameter("coffbookinmpercnt", coffbookinmpercnt + "%"));
            Rpt1.SetParameters(new ReportParameter("coffbookingam", coffbookingam));

            Rpt1.SetParameters(new ReportParameter("coffnooffemi", coffnooffemi));
            Rpt1.SetParameters(new ReportParameter("coffemi", coffemi));
            Rpt1.SetParameters(new ReportParameter("cofffvpersft", cofffvpersft));
            Rpt1.SetParameters(new ReportParameter("coffpvpersft", coffpvpersft));




            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sample Note Sheet"));
            Rpt1.SetParameters(new ReportParameter("txtsalesteam", salesteam));
            Rpt1.SetParameters(new ReportParameter("txtClusterName", clustername));
            Rpt1.SetParameters(new ReportParameter("txtrecommendname", recommendname));


            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintGrandNotesheetsum()
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
            string downpercnt = Convert.ToDouble(ds1.Tables[0].Rows[0]["dpaymntper"]).ToString("#,##0;(#,##0);");
            string valdownpayamy = Convert.ToDouble(ds1.Tables[0].Rows[0]["dpaymntam"]).ToString("#,##0;(#,##0);");
            string valnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]).ToString("#,##0;(#,##0);");
            string emi = Convert.ToDouble(ds1.Tables[0].Rows[0]["emi"]).ToString("#,##0;(#,##0);");

            string downpaydate = Convert.ToDateTime(lstb[0].schdate).ToString("dd-MMM-yyyy");
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
            string coffemi = Convert.ToDouble("0" + this.lblvalcoffemi.InnerText).ToString("#,##0;(#,##0);");
            string cofffvpersft = Convert.ToDouble(this.lblvalcofffvpersft.InnerText).ToString("#,##0;(#,##0);");
            string coffpvpersft = Convert.ToDouble(this.lblvalcoffpvpersft.InnerText).ToString("#,##0;(#,##0);");

            string coffbookingdate = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd-MMM-yyyy");
            string coffdpaymentper = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntper"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntper"]).ToString("#,##0;(#,##0);");
            string coffdpaymentam = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]).ToString("#,##0;(#,##0);");
            string coffdpaymentdate = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffdpaymntdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffdpaymntdat"]).ToString("dd-MMM-yyyy");






            LocalReport Rpt1 = new LocalReport();
            var lst1 = lstb;
            var lst2 = lstcoff;


           


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
                Rpt1.SetParameters(new ReportParameter("downpercnt", downpercnt));
                Rpt1.SetParameters(new ReportParameter("downpaydate", downpaydate));
                Rpt1.SetParameters(new ReportParameter("valdownpayamy", valdownpayamy));
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

                Rpt1.SetParameters(new ReportParameter("coffbookingam", coffbookingam));
                Rpt1.SetParameters(new ReportParameter("coffdpaymentper", coffdpaymentper));
                Rpt1.SetParameters(new ReportParameter("coffbookingdate", coffbookingdate));
                Rpt1.SetParameters(new ReportParameter("coffdpaymentam", coffdpaymentam));
                Rpt1.SetParameters(new ReportParameter("coffdpaymentdate", coffdpaymentdate));

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
        private void PrintGrandNotesheetDet()
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
            string downpercnt = Convert.ToDouble(ds1.Tables[0].Rows[0]["dpaymntper"]).ToString("#,##0;(#,##0);");
            string valdownpayamy = Convert.ToDouble(ds1.Tables[0].Rows[0]["dpaymntam"]).ToString("#,##0;(#,##0);");
            string valnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]).ToString("#,##0;(#,##0);");
            string emi = Convert.ToDouble(ds1.Tables[0].Rows[0]["emi"]).ToString("#,##0;(#,##0);");

            string downpaydate = Convert.ToDateTime(lstb[0].schdate).ToString("dd-MMM-yyyy");
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
            string coffemi = Convert.ToDouble("0" + this.lblvalcoffemi.InnerText).ToString("#,##0;(#,##0);");
            string cofffvpersft = Convert.ToDouble(this.lblvalcofffvpersft.InnerText).ToString("#,##0;(#,##0);");
            string coffpvpersft = Convert.ToDouble(this.lblvalcoffpvpersft.InnerText).ToString("#,##0;(#,##0);");

            string coffbookingdate = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffbookingdat"]).ToString("dd-MMM-yyyy");
            string coffdpaymentper = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntper"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntper"]).ToString("#,##0;(#,##0);");
            string coffdpaymentam = (Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]) == 0) ? "" : Convert.ToDouble(ds1.Tables[0].Rows[0]["coffdpaymntam"]).ToString("#,##0;(#,##0);");
            string coffdpaymentdate = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffdpaymntdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["coffdpaymntdat"]).ToString("dd-MMM-yyyy");






            LocalReport Rpt1 = new LocalReport();
            var lst1 = lstb;
            var lst2 = lstcoff;


          
            
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
                Rpt1.SetParameters(new ReportParameter("downpercnt", downpercnt));
                Rpt1.SetParameters(new ReportParameter("downpaydate", downpaydate));
                Rpt1.SetParameters(new ReportParameter("valdownpayamy", valdownpayamy));

                Rpt1.SetParameters(new ReportParameter("downpaydate", downpaydate));
                Rpt1.SetParameters(new ReportParameter("bookingmoney", valdownpayamy));
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
                Rpt1.SetParameters(new ReportParameter("coffbookingam", coffbookingam));
                Rpt1.SetParameters(new ReportParameter("coffdpaymentper", coffdpaymentper));
                Rpt1.SetParameters(new ReportParameter("coffbookingdate", coffbookingdate));
                Rpt1.SetParameters(new ReportParameter("coffdpaymentam", coffdpaymentam));
                Rpt1.SetParameters(new ReportParameter("coffdpaymentdate", coffdpaymentdate));

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
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string notesheetprint = this.ddlPrintType.SelectedValue.ToString();

            switch (notesheetprint)
            {
                case "samnotesheet":
                this.PrintSampleNoteSheet();
                    break;
                case "grandnotesheet":
                    this.PrintGrandNotesheetsum();
                    break;
                default:
                    this.PrintGrandNotesheetDet();
                    break;

            
            }
           
            

            }
        private void GetPreviousNoteSheetDetails()
        {


            try
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


                Session["lstbaseschdule"] = lstb;
                Session["lstcoffschedule"] = lstcoff;


                this.txtprospective.Text = ds1.Tables[0].Rows[0]["custname"].ToString();
                this.ddlcoffduration.SelectedValue = ds1.Tables[0].Rows[0]["coffdur"].ToString();




                this.CalCulationSummation(ds1);
                this.Data_Bind();
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);



            }

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
          
            Session["lstbaseschdule"] = lstb;
            Session["lstcoffschedule"] = lstcoff;
           
            this.CalCulationSummation(ds1);
            this.Data_Bind();
        }

        private void CalCulationSummation(DataSet ds1)
        {

            try


            {
                DateTime sysdate = System.DateTime.Today;
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];



                DateTime coffbookingdate, benddate, finalinsdate;
                double usize, bfv, bpv, pamt, utility, others, intratio, noofemi, bfvpsft, bpowbpart, bpvpsft,
                    cofffv, coffpv, coffpamt, coffutility, coffothers, cofffvpsft, coffpvpsft, coffpowbpart, coffnoofemi;


                //uzize =
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
                cofffv = lstcoff.Sum(l => l.fv);
                coffpv = lstcoff.Sum(l => l.pv);
                coffpamt = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffpamt"]);
                coffutility = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffutility"]);
                coffothers = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffothers"]);
                coffnoofemi = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]);





                this.lblvalarea.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0);");
                this.lblvalrate.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["urate"]).ToString("#,##0;(#,##0);");
                this.lblvalunitprice.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["uamt"]).ToString("#,##0;(#,##0);");
                this.lblvalparking.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["pamt"]).ToString("#,##0;(#,##0);");
                this.lblvalutility.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["utility"]).ToString("#,##0;(#,##0);");
                this.lblvalother.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["others"]).ToString("#,##0;(#,##0);");
                this.lblvalTotal.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["tunitamt"]).ToString("#,##0;(#,##0);");
                this.lblvaldownpayper.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["dpaymntper"]).ToString("#,##0;(#,##0);");
                this.lblvaldownpayam.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["dpaymntam"]).ToString("#,##0;(#,##0);");
                this.txtdownpaydate.Text = Convert.ToDateTime(lstb[0].schdate).ToString("dd-MMM-yyyy");
                this.lblvalnoofemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["noofemi"]).ToString("#,##0;(#,##0);");
                this.lblvalemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["emi"]).ToString("#,##0;(#,##0);");
                this.lblvalhandovdate.InnerText = Convert.ToDateTime(ds1.Tables[0].Rows[0]["handovdate"]).ToString("dd-MMM-yyyy");
                this.lblfvalinstallmentper.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["handovper"]).ToString("#,##0;(#,##0);");

                this.lblvalfvpsft.InnerText = bfvpsft.ToString("#,##0;(#,##0);");
                this.lblvalpvpersft.InnerText = bpvpsft.ToString("#,##0;(#,##0);");






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


                cofffvpsft = ((usize > 0) ? (cofffv == 0 ? 0.00 : (cofffv - coffpamt - coffutility - coffothers) / usize) : 0.00);
                coffpowbpart = (12 + intratio) / 12;
                coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, noofemi)), 0);

                this.txtcoffnooffemi.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffnoofemi"]).ToString("#,##0;(#,##0);");
                this.lblvalcoffemi.InnerText = Convert.ToDouble(ds1.Tables[0].Rows[0]["coffemi"]).ToString("#,##0;(#,##0);");
                this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
                this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");

            }


            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }





        }

        private void CalculationValue()
        {

            try
            {

                DateTime coffbookingdate, finalinsdate, benddate;
                int noofemi, badins = 0;

                double intratio, usize,  coffurate, coffuamt, coffpamt, coffutility, coffothers, cofftunitamt, coffbookingam, coffdpaymentper, coffdpaymentwbookam, coffdpaymentam, coffnoofemi, coffemi, cofffvpsft, coffpvpsft, coffpowbpart, cofffv, coffpv,   finalinsper, finalinsam;

               
                usize = Convert.ToDouble(this.lblvalcoffarea.InnerText.ToString());
                intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;

                //Customer Offer
                coffurate = Convert.ToDouble("0" + this.txtcoffrate.Text.ToString());
                coffuamt = usize * coffurate;
                coffpamt = Convert.ToDouble("0" + this.txtcofffparking.Text.ToString());
                coffutility = Convert.ToDouble("0" + this.txtcoffutility.Text.ToString());
                coffothers = Convert.ToDouble("0" + this.txtcoffothers.Text.ToString());
                cofftunitamt = coffuamt + coffpamt + coffutility + coffothers;
              
                coffbookingam = Convert.ToDouble("0" + this.txtcoffbookingam.Text.ToString()); ;

                coffdpaymentper = Convert.ToDouble("0" + this.txtcoffdownpayper.Text.ToString());
                //coffdpaymentam = cofftunitamt * 0.01 * coffdpaymentper;

                coffdpaymentwbookam = cofftunitamt * 0.01 * coffdpaymentper;
                coffdpaymentam = coffdpaymentwbookam - coffbookingam;

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
            int mondiff ,i;
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

            i =0;
            foreach (GridViewRow gv1 in gvcoffsch.Rows)
            {
                DateTime schdate =Convert.ToDateTime(((TextBox)gv1.FindControl("txtgvScheduledate")).Text.Trim());
                string monthid = schdate.ToString("yyyyMM");
                string ymon = schdate.ToString("MMM-yy");
                pv =ASTUtility.StrNagative(((TextBox)gv1.FindControl("txtgvdumschamt")).Text.Trim());
                mondiff = ASTUtility.Datediff(benddate, schdate);

                
                lstcoff[i].pv = pv;
                lstcoff[i].fv = Math.Round(pv * Math.Pow(coffpowbpart, mondiff), 0); 
                lstcoff[i].monthid = monthid;
                lstcoff[i].ymon = ymon;
                lstcoff[i].schdate = schdate;
                i++;
            }
            
            
            DateTime coffbookingdate;
            double usize, coffurate, noofemi, coffuamt, coffpamt, coffutility, coffothers, cofftunitamt, coffbookingper, coffbookingam, cofffv, coffpv, cofffvpsft, coffpvpsft;

            int tolistitem = lstcoff.Count;
            double cofftotal = Convert.ToDouble(this.lblcoffTotal.InnerText);
           coffpv = lstcoff.Sum(l => l.pv);
            double adwfinalins = cofftotal - coffpv;
            lstcoff[tolistitem - 1].pv += adwfinalins;


            
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




        }

        private void CalCulationInstallment()
        {

            try

            {


                DateTime  benddate;
                int bnoofemi, coffnoofemi, initial, mondiff, finalins;
                finalins = 0;
                double cofftunitamt, coffbookingam, coffdpaymentam, coffemi, coffpowbpart, intratio, coffresamt, finalinsam;
                string monthid, gcod, gdesc, ymon, grp;
                double pv, fv;

                //List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstb = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode> lstcode = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassInsCode>)Session["tblinstallment"];
               
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = new List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>();
               
                intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;
                // Customer Offer
                coffbookingam = Convert.ToDouble("0" + this.txtcoffbookingam.Text);
                coffdpaymentam = Convert.ToDouble("0" + this.lblvalcoffdownpayam.InnerText);
                coffnoofemi = Convert.ToInt32("0" + this.txtcoffnooffemi.Text.ToString());
                coffemi = Convert.ToDouble("0" + this.lblvalcoffemi.InnerText);
                finalinsam= Convert.ToDouble("0" + this.lblvalcofffininsam.InnerText);
                int coffdur = Convert.ToInt32(this.ddlcoffduration.SelectedValue.ToString());               
                coffpowbpart = (12 + intratio) / 12;

                DateTime bcasebookingdate,  bookingdate, dpaymentdate, firstinsdate, finalinsdate;
                bcasebookingdate=Convert.ToDateTime(this.txtcoffBookingdate.Text);
                bookingdate = Convert.ToDateTime(this.txtcoffBookingdate.Text);
                dpaymentdate= Convert.ToDateTime(this.txtcoffdownpaydate.Text);
                firstinsdate = Convert.ToDateTime(this.txtcoffinsdate.Text);
                finalinsdate =Convert.ToDateTime( this.txtcofffininsdate.Text);
                bnoofemi =Convert.ToInt32(this.lblhiddenbnoemi.Value);
                benddate = bcasebookingdate.AddMonths(bnoofemi);
                
                //benddate = strtdate.AddMonths(bnoofemi);
                benddate = finalinsdate > benddate ? finalinsdate : benddate;
                benddate = finalinsdate > benddate ? finalinsdate : benddate;
                initial = 0;


                // Base Case 
                //foreach (RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet blstitem in lstb)
                //{

                   

                //    pv = blstitem.pv;
                //    mondiff = ASTUtility.Datediff(benddate, bcasebookingdate);
                //    monthid = bcasebookingdate.ToString("yyyyMM");
                //    ymon = bcasebookingdate.ToString("MMM-yy");
                //    blstitem.monthid = monthid;
                //    blstitem.ymon = ymon;
                //    blstitem.schdate = bcasebookingdate;
                //    blstitem.fv = Math.Round(pv * Math.Pow(coffpowbpart, mondiff), 0);
                //    bcasebookingdate = bcasebookingdate.AddMonths(1);
                //}

                //Session["lstbaseschdule"] = lstb;




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
                   // mondiff = ASTUtility.Datediff(benddate, firstinsdate);
                    mondiff = ASTUtility.Datediff(benddate, firstinsdate) ;




                    //if (initial == coffnoofemi+k-1) // Last Installment
                    //{

                    //    cofftunitamt = Convert.ToDouble("0" + this.lblcoffTotal.InnerText);
                    //    coffresamt = cofftunitamt - lstcoff.Sum(l => l.pv);

                    //    RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                    //    {
                    //        monthid = monthid,
                    //        ymon = ymon,
                    //        gcod = gcod,
                    //        gdesc = gdesc,
                    //        schdate = firstinsdate,
                    //        grp = grp,
                    //        pv = coffresamt,
                    //        fv = Math.Round(coffresamt * Math.Pow(coffpowbpart, mondiff), 0)
                    //    };
                    //    lstcoff.Add(obj);
                    //}



                    //if (initial == coffnoofemi + k) // Last Installment
                    //{
                    //    if (finalinsam > 0)
                    //    {

                    //        cofftunitamt = Convert.ToDouble("0" + this.lblcoffTotal.InnerText);
                    //        coffresamt = cofftunitamt - lstcoff.Sum(l => l.pv);

                    //        RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet obj = new RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet
                    //        {
                    //            monthid = monthid,
                    //            ymon = ymon,
                    //            gcod = gcod,
                    //            gdesc = gdesc,
                    //            schdate = firstinsdate,
                    //            grp = grp,
                    //            pv = coffresamt,
                    //            fv = Math.Round(coffresamt * Math.Pow(coffpowbpart, mondiff), 0)
                    //        };
                    //        lstcoff.Add(obj);
                    //    }
                    //}


                    //else
                    //{

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
                   // }
                    initial++;
                    ins++;
                    firstinsdate = firstinsdate.AddMonths(coffdur);


                }



                //Final Installment

                if (finalinsam > 0)
                {


                    cofftunitamt = Convert.ToDouble("0" + this.lblcoffTotal.InnerText);
                    finalinsam= cofftunitamt - lstcoff.Sum(l => l.pv);
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

            int tolistitem = lstcoff.Count;

            double cofftotal = Convert.ToDouble(this.lblcoffTotal.InnerText);
            double coffpv = lstcoff.Sum(l => l.pv);
            double adwfinalins = cofftotal - coffpv;
            lstcoff[tolistitem - 1].pv += adwfinalins;


            Session["lstcoff"] = lstcoff; 
            this.gvcoffsch.DataSource = lstcoff;
            this.gvcoffsch.DataBind();
           // this.FooterCalculation();

            this.SaveValue();


            //After Saving Future Value
            this.Data_Bind();


           


        }
        private void AdjustWithFinalInstallment()
        {

            List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];


        



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


               // double coffpv = lstcoff.Sum(l => l.pv);

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

          
            





            int bcasenofemi, noofemi;
            DateTime coffbookingdate, benddate, finalinsdate;
            double uzize, bfv, bpv, pamt, utility, others,bpowbpart, bfvpsft, bpvpsft, intratio, usize, coffpamt, coffutility, coffothers, cofffvpsft, coffpvpsft, coffpowbpart, cofffv, coffpv, upsftwiopuaoth;


            usize = Convert.ToDouble(this.lblvalcoffarea.InnerText.ToString());
            intratio = Convert.ToDouble("0" + txtinterestrate.Text.ToString().Replace("%", "")) * 0.01;

            int count = lstcoff.Count;
            coffbookingdate = Convert.ToDateTime(lstcoff[0].schdate);
            finalinsdate = Convert.ToDateTime(lstcoff[count - 1].schdate);
            benddate = Convert.ToDateTime(lstb[lstb.Count-1].schdate);           
            benddate = finalinsdate > benddate ? finalinsdate : benddate;

       
            coffpamt = Convert.ToDouble("0" + this.txtcofffparking.Text.ToString());
            coffutility = Convert.ToDouble("0" + this.txtcoffutility.Text.ToString());
            coffothers = Convert.ToDouble("0" + this.txtcoffothers.Text.ToString());            
            noofemi = ASTUtility.Datediff(benddate, coffbookingdate);

            //Base Case     
            bfv = lstb.Sum(l => l.fv);
            bpv = lstb.Sum(l => l.pv);
            pamt = Convert.ToDouble(this.lblhiddenbpamt.Value);
            bcasenofemi = Convert.ToInt32(this.lblvalnoofemi.InnerText);
            utility = Convert.ToDouble(this.lblhiddenbutility.Value);
            others = Convert.ToDouble(this.lblhiddenothers.Value); 
            bfvpsft =Math.Round( ((usize > 0) ? ((bfv - pamt - utility - others) / usize) : 0.00),0);
            bpowbpart = (12 + intratio) / 12;
            bpvpsft = Math.Round(bfvpsft / (Math.Pow(bpowbpart, bcasenofemi)), 0);


            // Customer Offer  Case
            cofffv = lstcoff.Sum(l => l.fv);
         //   coffpv = lstcoff.Sum(l => l.pv);
            cofffvpsft =Math.Round(((usize > 0) ? ((cofffv - coffpamt - coffutility - coffothers) / usize) : 0.00),0);
            coffpowbpart = (12 + intratio) / 12;
            coffpvpsft = Math.Round(cofffvpsft / (Math.Pow(coffpowbpart, noofemi)), 0);
          
            this.lblvalcofffvpersft.InnerText = cofffvpsft.ToString("#,##0;(#,##0);");
            this.lblvalcoffpvpersft.InnerText = coffpvpsft.ToString("#,##0;(#,##0);");

            this.lblhiddenfvpersft.Value = bfvpsft.ToString("#,##0;(#,##0);");
            this.lblhiddenpvpersft.Value = bpvpsft.ToString("#,##0;(#,##0);");


            if (cofffvpsft >= bfvpsft && coffpvpsft >= bpvpsft)
            {




                //cofffvpsft = bfvpsft;



                
                result = true;
            }




            double baseinterest, coffinterest, interestdiff, unitamt, newcofftotalam, newcoffunitamt, newcoffunitrate;
            benddate = Convert.ToDateTime(lstb[lstb.Count - 1].schdate);
            baseinterest = lstb.Sum(l => l.fv) - lstb.Sum(l => l.pv);
            coffinterest = lstcoff.Sum(l => l.fv) - lstcoff.Sum(l => l.pv);
            interestdiff = finalinsdate > benddate ?  ((baseinterest - coffinterest)*-1): (baseinterest - coffinterest);
            unitamt = Convert.ToDouble("0" + this.lblvalunitprice.InnerText);
            newcofftotalam = unitamt + coffpamt + coffutility + coffothers + interestdiff;
            newcoffunitamt = newcofftotalam - coffpamt - coffutility - coffothers;
            newcoffunitrate = Math.Round(((usize > 0) ? (newcoffunitamt / usize) : 0.00), 0); 
            this.lblhiddenncoffurate.Value = newcoffunitrate.ToString("#,##0;(#,##0); ");



            //cofffv = bfvpsft * usize + (coffpamt + coffutility + coffothers);
            //coffpv = cofffv / (Math.Pow(coffpowbpart, noofemi));
            //upsftwiopuaoth = ((usize > 0) ? ((coffpv - (coffpamt + coffutility + coffothers)) / usize) : 0.00);




            return result;
        
        

        
        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                this.SaveValue();
                bool resultfvapvpersft = this.GetResultpvapvpersft();

                if (!resultfvapvpersft)
                {
                    //"<span style='color:red'>Minimum FV Per SFT:" + this.lblhiddenfvpersft.Value + "Minimum PV Per SFT:" + this.lblhiddenpvpersft.Value + "</span>";

                    string mfvpsftapvpsft = "Minimum Rate Per SFT:" + this.lblhiddenncoffurate.Value;
                    
                   // string mfvpsftapvpsft = "Minimum FV Per SFT:" + this.lblhiddenfvpersft.Value + " Minimum PV Per SFT:" + this.lblhiddenpvpersft.Value;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ mfvpsftapvpsft + "');", true);
                    return;

                }

              //
              //return;

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
               
                string coffbookingam = Convert.ToDouble("0" + this.txtcoffbookingam.Text).ToString();
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
                string proscode =this.txtprospective.Text.Trim();
                bool resulta = false;
                resulta = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "DELETENOTESHEET", noteshtid, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!resulta)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                    return;

                }

                //return;

                resulta = MktData.UpdateTransInfo01(comcod, "SP_ENTRY_SALESNOTESHEET", "INSERTORUPDATESALESNOTESHEET", noteshtid, pactcode, usircode, noteshtdate, intrate, coffurate, coffpamt, coffutility, coffothers, coffbookdate, coffdpaymentper, coffdpaymentam, coffdpaymentdate, cofffinalinsper, cofffinalinsam, cofffinalinsdate, coffnooffemi, coffduration, proscode, Postusrid, trmnid, session, PostedDate, "", coffbookingam, "", "");
                if (!resulta)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"] + "');", true);
                    return;

                }



                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet> lstbcase = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)Session["lstbaseschdule"];
                List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet> lstcoff = (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)Session["lstcoffschedule"];
              

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
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + MktData.ErrorObject["Msg"].ToString() + "');", true);
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
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string usrid = hst["usrid"].ToString();               
                string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string qgeno = this.Request.QueryString["genno"] ?? "";
                string noteshtno = (qgeno.Length == 0 ? "" : qgeno) + "%";
              

                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPREVIOUSSAMNOTESHEETNO",
                       date, noteshtno, usrid, "", "", "", "", "");
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

        private void GetFinalInstallment()
        {

            int coffemi = Convert.ToInt32("0" + this.txtcoffnooffemi.Text.Trim());
            DateTime firstinsdate = Convert.ToDateTime(this.txtcoffinsdate.Text);
            DateTime fininsdate = firstinsdate.AddMonths(coffemi-1);
            this.txtcofffininsdate.Text = fininsdate.ToString("dd-MMM-yyyy");

           // TextBox txtevenname = (TextBox)send;

        }

        protected void txtcoffnooffemi_TextChanged(object sender, EventArgs e)
        {
            this.GetFinalInstallment();

        }

        protected void txtcoffinsdate_TextChanged(object sender, EventArgs e)
        {
            this.GetFinalInstallment();
        }

        protected void lnkgvbaseFcoffTotal_Click(object sender, EventArgs e)
        {

        }









    }
}

