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
namespace RealERPWEB.F_22_Sal
{
    public partial class MktDummySalsPayment : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = (TypeDesc == "Sales" ? "Dummy Payment Schedule " : (TypeDesc == "Cust" ? "SALES WITH PAYMENT " : (TypeDesc == "Loan" ? "CUSTOMER LOAN " : (TypeDesc == "Registration" ? "CUSTOMER REGISTRATION  " : "")))) + " INFORMATIOIN VIEW/EDIT";

                Session.Remove("Unit");



                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES WITH PAYMENT  INFORMATION ";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.gvSpayment.Columns[0].Visible = false;
                this.txtbookdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfinsdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");



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
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
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
                //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                //this.lblProjectdesc.Visible = true;

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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "DETAILSIRINFINFORMATION", PactCode, srchunit, "", "", "", "", "", "", "");
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
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string project = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13).ToString();


            LocalReport Rpt1 = new LocalReport();

            DataTable dt2 = (DataTable)Session["tblshusel"];
            DataTable dt3 = ((DataTable)Session["tblactive"]).Copy();
            DataTable dt4 = (DataTable)Session["tblact01"];
            DataTable dt5 = (DataTable)Session["tblintcol"];
            DataTable dt6 = (DataTable)Session["tblintcol01"];



            //   Session["tblintcol01"] = ds1.Tables[6];
            DataTable dtOrder = (DataTable)ViewState["tblData"];

            //this.txtdiscount.Text = (dt4.Rows.Count == 0) ? "" : Convert.ToDouble(dt4.Rows[0]["disrate"]).ToString("#,##0.00;(#,##0.00); ");
            //this.txtentryben.Text = (dt5.Rows.Count == 0) ? "" : Convert.ToDouble(dt5.Select("code='001'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            //this.txtdelaychrg.Text = (dt5.Rows.Count < 1) ? "" : Convert.ToDouble(dt5.Select("code='002'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");


            string discount = this.txtdiscount.Text.Trim();
            string entryben = this.txtentryben.Text;
            string delay = this.txtdelaychrg.Text;



            var lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            var lst1 = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            var alst1 = lst1.FindAll(l => l.schamt > 0);

            var lst2 = dt2.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.dammySchedule>();
            var lst3 = dt3.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClasActual>();
            var lst4 = dt4.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClasActual01>();
            var lst5 = dt6.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClasInterestCalculation>();

            var lstdummyschedule = new RealEntity.C_22_Sal.EClassSales_02.Eclassdummyschedule();
            lstdummyschedule.Lst01EClassDumPaSchdule = lst;
            lstdummyschedule.Lst02EClassAcPaSchdule = alst1;
            lstdummyschedule.Lst03EClassSehedual = lst2;
            lstdummyschedule.Lst04EClasActual = lst3;
            lstdummyschedule.Lst05EClasActual01 = lst4;
            lstdummyschedule.Lst06EClasInterestCalculation = lst5;


            if (discount == "")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptDummyPaywithoutDiscount", lstdummyschedule, null, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptDummyPaySchidule", lstdummyschedule, null, null);
            }

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptHead", "Dummy Payment Schedule"));
            Rpt1.SetParameters(new ReportParameter("ProjName", "Project Name: " + project));
            Rpt1.SetParameters(new ReportParameter("Unit", "Unit: " + dtOrder.Rows[0]["udesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Discount", discount));  // this.txtdiscount.Text = (dt4.Rows.Count == 0) ? "" : Convert.ToDouble(dt4.Rows[0]["disrate"]).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("EarlyBeni", entryben));  //this.txtentryben.Text = (dt5.Rows.Count == 0) ? "" : Convert.ToDouble(dt5.Select("code='001'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("Delacharge", delay)); // this.txtdelaychrg.Text = (dt5.Rows.Count < 1) ? "" : Convert.ToDouble(dt5.Select("code='002'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ")));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
            // this.lblAcAmt.Text = Convert.ToDouble(dtOrder.Rows[0]["tamt"]).ToString("#,##0;(#,##0); ");
            this.gvSpayment.Columns[5].Visible = true;
            this.gvSpayment.Columns[6].Visible = true;
            this.ShowData();

            // this.Data_SumBind();
        }

        private void ShowData()
        {
            string comcod = this.GetCompCode();
            string usircode = this.lblCode.Text;
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "SHOWDUMMYSCHDULE", pactcode, usircode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            Session["tbldschamt"] = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>();
            Session["tblacamt"] = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>();

            Session["tblshusel"] = ds1.Tables[2];
            Session["tblactive"] = ds1.Tables[3];
            Session["tblact01"] = ds1.Tables[4];
            Session["tblintcol"] = ds1.Tables[5];
            Session["tblintcol01"] = ds1.Tables[6];

            // Session["UsirBasicInformation"] = dtOrder;

            this.Data_Bind();

            this.gvsumdumsch.DataSource = ds1.Tables[2];
            this.gvsumdumsch.DataBind();
            this.gvsumacsch.DataSource = ds1.Tables[3];
            this.gvsumacsch.DataBind();
            this.txtdiscount.Text = (ds1.Tables[4].Rows.Count == 0) ? "" : Convert.ToDouble(ds1.Tables[4].Rows[0]["disrate"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtParking.Text = (ds1.Tables[4].Rows.Count == 0) ? "" : Convert.ToDouble(ds1.Tables[4].Rows[0]["parking"]).ToString("#,##0;(#,##0); ");

            DataTable dt = ds1.Tables[5];
            this.txtentryben.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Select("code='001'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtdelaychrg.Text = (dt.Rows.Count < 1) ? "" : Convert.ToDouble(dt.Select("code='002'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            this.gvInterest.DataSource = ds1.Tables[6];
            this.gvInterest.DataBind();
            this.FooterCal(ds1.Tables[6]);
        }



        private void FooterCal(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
                         0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;-#,##0;");
            ((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pamount)", "")) ?
                                  0 : dt.Compute("sum(pamount)", ""))).ToString("#,##0;-#,##0;");
            ((Label)this.gvInterest.FooterRow.FindControl("lgvFcumbalamt")).Text = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["cumbalance"]).ToString("#,##0;-#,##0;");

            ((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ?
                                  0 : dt.Compute("sum(interest)", ""))).ToString("#,##0;-#,##0;");
            //double tointerest = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ? 0 : dt.Compute("sum(interest)", "")));
            //double linterest = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["interest"]);
            //double todue = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["dueamt"]);


        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

            this.LoadGrid();



        }

        private void CustInf()
        {
            Session.Remove("tblcost");
            Session.Remove("tblPay");
            Session.Remove("tpripay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string empid = hst["empid"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            Session["UserLog"] = ds1.Tables[6];
            Session["tpripay"] = ds1.Tables[4];
            Session["tblcost"] = ds1.Tables[1];
        }
        private void Calculation()
        {

            DataTable dtcost = (DataTable)Session["tblcost"];
            DataTable dtpay = (DataTable)Session["tblPay"];
        }

        protected void lbtnsrchunit_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkVisible.Checked == true)
            {

                this.pnlgenMrr.Visible = true;

            }
            else
            {
                this.pnlgenMrr.Visible = false;
            }

        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            int i, k = 0;


            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = new List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>();
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = new List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>();
            string gcod = "";
            string gdesc = "";
            DateTime bookdate = Convert.ToDateTime(this.txtbookdate.Text);
            DateTime insdate = Convert.ToDateTime(this.txtfinsdate.Text);
            double toamt = Convert.ToDouble("0" + this.txttoamt.Text);
            double bookamt = Convert.ToDouble("0" + this.txtbooking.Text);
            double schinsamt = Convert.ToDouble("0" + this.txtinsamt.Text);
            double acbookamt = Convert.ToDouble("0" + this.txtacbooking.Text);
            double acinsamt = Convert.ToDouble("0" + this.txtacinstallment.Text);


            // Booking and first Installent
            lstd.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule(gcod, gdesc, bookdate, bookamt));
            //lstd.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule(insdate, schinsamt));
            lsta.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(bookdate, acbookamt));

            int dur = Convert.ToInt16(this.ddlMonth.SelectedValue.ToString());
            int insno = Convert.ToInt16("0" + this.txtnofins.Text);
            // double  
            for (i = 0; i < insno; i++)
            {
                lstd.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule(gcod, gdesc, insdate, schinsamt));
                lsta.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(insdate, acinsamt));

                insdate = insdate.AddMonths(dur);

            }
            Session["tbldschamt"] = lstd;
            Session["tblacamt"] = lsta;
            this.Data_Bind();




            // DataView dv1 = dt.DefaultView;
            // dv1.RowFilter = ("schamt>0");
            // dv1.Sort = "gcod";
            // Session["tblPay"] = dv1.ToTable();
            //;
            this.chkVisible.Checked = false;
            this.chkVisible_CheckedChanged(null, null);
        }

        private void Data_Bind()
        {

            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

            this.gvdumpay.DataSource = lstd;
            this.gvdumpay.DataBind();

            this.gvacpay.DataSource = lsta;
            this.gvacpay.DataBind();

            this.FooterCalculation();


        }

        private void Data_SumBind()
        {

            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

            this.gvdumpay.DataSource = lstd;
            this.gvdumpay.DataBind();

            this.gvacpay.DataSource = lsta;
            this.gvacpay.DataBind();

            this.FooterCalculationsum();


        }



        private void FooterCalculation()
        {
            try
            {
                List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
                List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

                if (lstd.Count > 0)
                    ((Label)this.gvdumpay.FooterRow.FindControl("lgvFdumschamt")).Text = lstd.Sum(l => l.schamt).ToString("#,##0;(#,##0); ");
                if (lsta.Count > 0)
                    ((Label)this.gvacpay.FooterRow.FindControl("lgvFacschamt")).Text = lsta.Sum(l => l.schamt).ToString("#,##0;(#,##0); ");
            }

            catch (Exception e)
            {



            }



        }

        private void FooterCalculationsum()
        {
            try
            {
                List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
                List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

                if (lstd.Count > 0)
                    ((Label)this.gvdumpay.FooterRow.FindControl("lgvFdumschamt")).Text = lstd.Sum(l => l.schamt).ToString("#,##0;(#,##0); ");
                if (lsta.Count > 0)
                    ((Label)this.gvacpay.FooterRow.FindControl("lgvFacschamt")).Text = lsta.Sum(l => l.schamt).ToString("#,##0;(#,##0); ");
            }

            catch (Exception e)
            {



            }



        }


        protected void lbtnTotaldumsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];

            for (int i = 0; i < this.gvdumpay.Rows.Count; i++)
            {
                DateTime schdate = Convert.ToDateTime(((TextBox)this.gvdumpay.Rows[i].FindControl("txtgvScheduledate")).Text.Trim());
                double schamt = Convert.ToDouble("0" + ((TextBox)this.gvdumpay.Rows[i].FindControl("txtgvdumschamt")).Text.Trim());

                lst[i].schdate = schdate;
                lst[i].schamt = schamt;
            }
            Session["tbldschamt"] = lst;
            this.Data_Bind();

        }
        protected void lbtnTotalacsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

            for (int i = 0; i < this.gvacpay.Rows.Count; i++)
            {
                DateTime schdate = Convert.ToDateTime(((TextBox)this.gvacpay.Rows[i].FindControl("txtgvacScheduledate")).Text.Trim());
                double schamt = Convert.ToDouble("0" + ((TextBox)this.gvacpay.Rows[i].FindControl("txtgvacschamt")).Text.Trim());

                lst[i].schdate = schdate;
                lst[i].schamt = schamt;
            }
            Session["tblacamt"] = lst;
            this.Data_Bind();


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
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.lblCode.Text.Trim();

                //Log Data
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string usrid = hst["usrid"].ToString();
                string trmnid = hst["compname"].ToString();
                string session = hst["session"].ToString();
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string entryben = Convert.ToDouble("0" + this.txtentryben.Text.Trim()).ToString();
                string delaychrg = Convert.ToDouble("0" + this.txtdelaychrg.Text.Trim()).ToString();


                DataSet ds1 = new DataSet("ds1");
                //Table Schdule
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("schamt", typeof(Double));
                dt1.Columns.Add("schdate", typeof(DateTime));

                // Table Actual
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("schamt", typeof(Double));
                dt2.Columns.Add("schdate", typeof(DateTime));


                DataTable dt3 = new DataTable();
                dt3.Columns.Add("postedbyid", typeof(string));
                dt3.Columns.Add("postseson", typeof(string));
                dt3.Columns.Add("posttrmnid", typeof(string));
                dt3.Columns.Add("posteddate", typeof(DateTime));


                DataTable dt4 = new DataTable();
                dt4.Columns.Add("disrate", typeof(double));
                dt4.Columns.Add("parking", typeof(double));


                DataTable dt5 = new DataTable();
                dt5.Columns.Add("code", typeof(string));
                dt5.Columns.Add("charge", typeof(double));








                List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
                List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

                dt1 = ASITUtility03.ListToDataTable(lstd);
                dt2 = ASITUtility03.ListToDataTable(lsta);

                DataRow dr1 = dt3.NewRow();
                dr1["postedbyid"] = usrid;
                dr1["postseson"] = session;
                dr1["posttrmnid"] = trmnid;
                dr1["posteddate"] = Date;
                dt3.Rows.Add(dr1);
                dt3.TableName = "tbl3";

                DataRow dr2 = dt4.NewRow();
                dr2["disrate"] = Convert.ToDouble("0" + this.txtdiscount.Text);
                dr2["parking"] = Convert.ToDouble("0" + this.txtParking.Text);
                dt4.Rows.Add(dr2);
                dt4.TableName = "tbl4";



                DataRow drd;
                drd = dt5.NewRow();
                drd["code"] = "001";
                drd["charge"] = entryben;
                dt5.Rows.Add(drd);
                drd = dt5.NewRow();
                drd["code"] = "002";
                drd["charge"] = delaychrg;
                dt5.Rows.Add(drd);




                ds1.Merge(dt1);
                ds1.Merge(dt2);
                ds1.Merge(dt3);
                ds1.Merge(dt3);
                ds1.Merge(dt4);
                ds1.Merge(dt5);
                ds1.Tables[0].TableName = "tbl1";
                ds1.Tables[1].TableName = "tbl2";
                ds1.Tables[2].TableName = "tbl3";
                ds1.Tables[3].TableName = "tbl4";
                ds1.Tables[4].TableName = "tbl5";


                bool resulta = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "UPDATEDUMMYPAYMENT", ds1, null, null, pactcode, usircode);

                if (!resulta)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + MktData.ErrorObject["Mesage"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }



                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated SUccessfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                this.ShowData();


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
    }
}

