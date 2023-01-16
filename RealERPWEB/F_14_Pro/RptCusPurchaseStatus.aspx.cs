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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_14_Pro
{
    public partial class RptCusPurchaseStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;


                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("../AcceessError.aspx");




                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));



                //((Label)this.Master.FindControl("lblTitle")).Text = "Day Wise Purchase(Customization)";


                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string qdate1 = this.Request.QueryString["Date1"];
                string qdate2 = this.Request.QueryString["Date2"];

                this.txttodate.Text = qdate2.Length > 0 ? qdate2 : date;
                this.txtFDate.Text = qdate1.Length > 0 ? qdate1 : "01" + date.Substring(2);
                this.GetProjectName();
                this.GetInformationField();

                this.ShowView();
                //this.GetReqno01 ();
                //this.GetBillNo ();
                //this.LoadSertial();
                this.imgbtnFindMatCom_Click(null, null);
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void LoadSertial()
        {
            string comcod = this.GetComeCode();
            //string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "LOADSERIAL", "", "", "", "", "", "", "", "", "");
            this.ddlSerialno.DataTextField = "slnum1";
            this.ddlSerialno.DataValueField = "slnum";
            this.ddlSerialno.DataSource = ds1.Tables[0];
            this.ddlSerialno.DataBind();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }
        private void GetInformationField()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTDYNAMICFIELD", "", "", "", "", "", "", "", "", "");
            this.ddlInformation.DataTextField = "descrip";
            this.ddlInformation.DataValueField = "code";
            this.ddlInformation.DataSource = ds1.Tables[0];
            this.ddlInformation.DataBind();
            ViewState["tbldyfield"] = ds1.Tables[0];
            ds1.Dispose();

        }
        private void GetSupplier()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSrchSupplier = this.txtSrcSupplier.Text.Trim() + "%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETSUPPLIER", pactcode, txtSrchSupplier, "", "", "", "", "", "", "");
            this.ddlSupplier.DataTextField = "ssirdesc";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataSource = ds2.Tables[0];
            this.ddlSupplier.DataBind();

        }

        private void GetMaterialCode()
        {
            string comcod = this.GetComeCode();

            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            // string txtSrchSupplier = this.txtSrcSupplier.Text.Trim() + "%";
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETRESOURCE", "%%", "", "", "", "", "", "", "", "");
            this.ddlMatCode.DataTextField = "sirdesc";
            this.ddlMatCode.DataValueField = "sircode";
            this.ddlMatCode.DataSource = ds3.Tables[0];
            this.ddlMatCode.DataBind();
            this.colorBind();
            //foreach (ListItem lteam in ddlMatCode.Items)
            //{
            //    string matcode = lteam.Value.Substring(9, 3).ToString();
            //    if (matcode == "000")
            //    {
            //        lteam.Attributes.Add("style", "background-color:#a3ffa3");
            //    }
            //}
        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            this.GetSupplier();
        }
        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "DayWisePurchase":
                    this.GetMaterialCode();
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;
                    this.lblMcod.Visible = true;
                    this.ddlMatCode.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.chkDirect.Visible = true;

                    break;



            }



        }


        protected void imgbtnFindReqno01_Click(object sender, EventArgs e)
        {
            this.GetReqno01();
        }


        private void GetReqno01()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = "%" + this.txtSrcRequisition01.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
            this.ddlReqNo01.DataTextField = "reqno1";
            this.ddlReqNo01.DataValueField = "reqno";
            this.ddlReqNo01.DataSource = ds1.Tables[0];
            this.ddlReqNo01.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }

        private void GetReqno02()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = this.txtSrcRequisition02.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
            this.ddlReqNo02.DataTextField = "reqno1";
            this.ddlReqNo02.DataValueField = "reqno";
            this.ddlReqNo02.DataSource = ds1.Tables[0];
            this.ddlReqNo02.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }

        private void GetBillNo()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string billno = this.txtBillSearch.Text.Trim();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETBILLNO", billno, "", "", "", "", "", "", "", "");
            this.ddlBillno.DataTextField = "billno1";
            this.ddlBillno.DataValueField = "billno";
            this.ddlBillno.DataSource = ds1.Tables[1];
            this.ddlBillno.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();


        }

        private void GetMaterial()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtfindMat = "%" + this.txtSrcMat.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.ddlMaterial.DataTextField = "rsirdesc";
            this.ddlMaterial.DataValueField = "rsircode";
            this.ddlMaterial.DataSource = ds1.Tables[0];
            this.ddlMaterial.DataBind();

        }

        protected void imgbtnFindMat_Click(object sender, EventArgs e)
        {

            this.GetMaterial();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Rpt"] ?? "DaywPur";
            switch (rpt)
            {

                case "DaywPur":
                    this.RptDayPurchase();
                    break;

                case "PurSum":
                    this.RptPurchaseSum();
                    break;

                case "PenBill":
                    break;

                case "IndSup":
                    this.RptIndSup();
                    break;
                case "Purchasetrk":
                    this.RptPurchaseTrack();
                    break;

                case "BgdBal":
                    this.RptBgdBal();
                    break;

                case "PurBilltk":
                    this.PrintBurBillTrack();
                    break;

                case "MatRateVar":
                    PrintMatRateVariance();
                    break;
                case "BillRegTrack":
                    this.PrintBillRegTrack();
                    break;


            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        private void RptDayPurchase()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            switch (comcod)
            {

                case "3330":
                case "3101":
                    this.RptDayPurchaseBridge();
                    break;

                default:
                    this.RptDayPurchaseGen();
                    break;


            }





        }

        private void RptDayPurchaseBridge()
        {

            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.RptDayWisePurchase>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptDayWisePurchase", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Day Wise Purchase Report"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            // Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void RptDayPurchaseGen()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.RptDayWisePurchase>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptPurchaseStatus1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));

            Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));

            Rpt1.SetParameters(new ReportParameter("rptTitle", "Day Wise Purchase Report"));


            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            // Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void RptPurchaseSum()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            string newsum = ((Label)this.gvPurSum.FooterRow.FindControl("lgvFAmtS")).Text.ToString();
            DataTable dt = (DataTable)Session["tblpurchase"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurchaseSummary02>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchaseSummary", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtGroup", "Group: " + this.ddlRptGroup.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Purchase Summary"));
            Rpt1.SetParameters(new ReportParameter("txtGrandTotal", newsum));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptIndSup()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

            DataTable dt = (DataTable)Session["tblpurchase"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptIndSupPurchase>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptIndSupPurchae", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtSupplier", this.ddlSupplier.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Purchase History- Supplier Wise"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptPurchaseTrack()
        {
            DataTable dt2 = (DataTable)Session["tblreq"];
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string reqno = this.ddlReqNo01.SelectedValue.ToString();

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurTrack01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchaseTra", list, null, null);
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("projectName", (((DataTable)Session["tblreq"]).Select("reqno='" + reqno + "'"))[0]["actdesc"].ToString()));
            rpt.SetParameters(new ReportParameter("rptTitle", "Purchase Tracking"));
            rpt.SetParameters(new ReportParameter("mrfNo", "MRF No: " + dt.Rows[0]["refno"].ToString()));
            rpt.SetParameters(new ReportParameter("narration", this.txtReqNarr.Text.Trim()));
            rpt.SetParameters(new ReportParameter("userInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptBgdBal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = System.DateTime.Now.ToString("dd.MM.yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = ((DataTable)Session["tblpurchase"]);
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EclassBudgetTracking>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptBudgetTracking", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Budget Tracking"));
            Rpt1.SetParameters(new ReportParameter("comlogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("project", this.ddlProjectName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("material", this.ddlMaterial.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("opening", this.lblvalOpenig.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("bgdqty", this.lblvalBudget.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("transfer", this.lblvaltrans.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("ttlsuply", this.lblvalTotalSupp.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("balqty", this.lblvalBalance.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }


        private void PrintBillRegTrack()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string paymentid = this.ddlSerialno.SelectedValue;
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillRegTrack>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_14_Pro.RptBillRegTrack", list, null, null);

            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("paymentid", paymentid));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintBurBillTrack()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string billno = this.ddlBillno.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblpurchase"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillTracking>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.rptPurchaseBillTk", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("projectName", (((DataTable)Session["tblreq"]).Select("billno='" + billno + "'"))[0]["actdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("supplierName", (((DataTable)Session["tblreq"]).Select("billno='" + billno + "'"))[0]["ssirdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Purchase Bill Tracking"));
            Rpt1.SetParameters(new ReportParameter("refNo", "Ref No: " + (((DataTable)Session["tblreq"]).Select("billno='" + billno + "'"))[0]["billref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("reqDate", "Bill Date: " + this.ddlBillno.SelectedItem.Text.Substring(13, 11)));
            Rpt1.SetParameters(new ReportParameter("billNo", "Bill  No: " + ASTUtility.Left(this.ddlBillno.SelectedItem.Text.Trim(), 11)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintMatRateVariance()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string billno = this.ddlBillno.SelectedValue.ToString();
            string frmdate = " Variance Date Range:-  Past Date: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy") + " Present Date: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            var matratevar = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EClassMatRateVar>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptMatRateVar", matratevar, null, null);


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            var datefrm = Convert.ToDateTime(this.txtFDate.Text.Trim());
            var dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            string monthcount = "";
            for (int i = 1; i < 13; i++)
            {
                //if (datefrm > dateto)
                //    break;
                monthcount = "month" + i.ToString();
                Rpt1.SetParameters(new ReportParameter(monthcount, datefrm.ToString("MMM yy")));
                datefrm = datefrm.AddMonths(1);

            }

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowValue();


        }
        private void ShowValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "DayWisePurchase":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.lblMcod.Visible = true;
                    this.ddlMatCode.Visible = true;
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;



                    this.ShowDayPur();
                    break;

                case "PurSum":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowPurSum();
                    break;

                case "PenBill":
                    break;
                case "IndSup":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;
                    this.ShowIndSupplier();
                    break;

                case "Purchasetrk":
                    //this.ShowPurChaseTrk();
                    this.pnlnarration.Visible = true;
                    this.ShowPurChaseTrk01();
                    break;

                case "Purchasetrk02":

                    this.ShowPurChaseTrk02();
                    break;

                case "BgdBal":
                    this.Panelbgdbal.Visible = true;
                    this.ShowBgdBal();
                    break;

                case "PurBilltk":
                    this.ShowPurchaseBill();
                    break;

                case "MatRateVar":
                    this.ShowMatRVariacne();
                    break;
                case "BillRegTrack":
                    this.BillRegTrack();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Show Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void imgbtnFindRequiSition_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.ShowDayPur();
                    break;



                case "IndSup":
                    this.ShowIndSupplier();
                    break;

            }

        }


        private void ShowDayPur()
        {
            this.colorBind();
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string mrfno = "%" + this.txtSrcMrfNo.Text.Trim() + "%";
            string rescode = ((this.ddlMatCode.SelectedValue.ToString() == "000000000000") ? "" : (this.ddlMatCode.SelectedValue.Substring(9, 3).ToString() == "000") ? (this.ddlMatCode.SelectedValue.ToString().Substring(0, 9)).ToString() : this.ddlMatCode.SelectedValue.ToString()) + "%";
            string dirorin = (this.chkDirect.Checked) ? "direct" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQSATIONMRRSTATUS", fromdate, todate, pactcode, mrfno, rescode, dirorin, "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = dt;


            this.LoadGrid();
        }

        public void colorBind()
        {
            foreach (ListItem lteam in ddlMatCode.Items)
            {
                string matcode = lteam.Value.Substring(9, 3).ToString();
                if (matcode == "000")
                {
                    lteam.Attributes.Add("style", "background-color:#a3ffa3");
                }
            }
        }
        private void ShowPurSum()
        {
            Session.Remove("tblpurchase");

            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURSUMMARY", fromdate, todate, pactcode, mRptGroup, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurSum.DataSource = null;
                this.gvPurSum.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();

        }
        private void ShowIndSupplier()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string supplier = this.ddlSupplier.SelectedValue.ToString();
            string mrfno = this.txtSrcMrfNo.Text.Trim() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTINDSUPINFO", fromdate, todate, pactcode, supplier, mrfno, "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = dt;
            this.LoadGrid();


        }


        private void ShowPurChaseTrk01()
        {



            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string reqno = this.ddlReqNo01.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            this.txtReqNarr.Text = ds1.Tables[1].Rows.Count == 0 ? "" : ds1.Tables[1].Rows[0]["reqnar"].ToString();
            ///this.lblshipsupdate.Text = ds1.Tables[0].Rows[0]["shipsupdat"].ToString();
            this.LoadGrid();


        }

        private void BillRegTrack()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string serial = this.ddlSerialno.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "BILLREGTRACKING", serial, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            // DataTable dt = this.HiddenSameData (ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];

            this.LoadGrid();
        }

        private void ShowPurChaseTrk02()
        {


            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string reqno = this.ddlReqNo02.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK02", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk.DataSource = null;
                this.gvPurstk.DataBind();
                this.gvPurstk2.DataSource = null;
                this.gvPurstk2.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            Session["tblpurchase1"] = ds1.Tables[1];
            this.LoadGrid();
        }


        private void ShowPurchaseBill()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string billno = this.ddlBillno.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURBILLTRACK", billno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurBilltk.DataSource = null;
                this.gvPurBilltk.DataBind();

                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private string CompBudgetBalance()
        {
            string comcod = this.GetComeCode();
            string reqorapproved = "";
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                    reqorapproved = "req";
                    break;

                default:
                    break;



            }
            return reqorapproved;

        }
        private void ShowBgdBal()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string rescode = this.ddlMaterial.SelectedValue.ToString();






            string reqorapproved = this.CompBudgetBalance();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTBUDGETBAL", pactcode, rescode, reqorapproved, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgdBal.DataSource = null;
                this.gvBgdBal.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];

            this.lblvalBudget.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdqty"]).ToString("#,##0;(#,##0); ");

            this.lblvalOpenig.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opqty"]).ToString("#,##0;(#,##0); ");
            this.lbltxtvaldqty.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dqty"]).ToString("#,##0;(#,##0); ");

            this.lblvalRequisition.Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(areqty)", "")) ?
                                         0 : ds1.Tables[0].Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
            this.lblvaltrans.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["trnqty"]).ToString("#,##0;(#,##0); ");

            this.lblvalTotalSupp.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tosupqty"]).ToString("#,##0;(#,##0); ");
            this.lblvalBalance.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdbal"]).ToString("#,##0;(#,##0); ");
            this.LoadGrid();

        }

        private void ShowMatRVariacne()
        {


            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string ResCode = ((this.ddlMaterialscom.SelectedValue == "000000000000") ? "" : this.ddlMaterialscom.SelectedValue.ToString()) + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATRATEVARIANCE", frmdate, todate, ResCode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurMatRVar.DataSource = null;
                this.gvPurMatRVar.DataBind();

                return;
            }

            Session["tblpurchase"] = this.HiddenSameData(ds1.Tables[0]);
            this.gvPurMatRVar.Columns[4].HeaderText = "Price On <br />" + Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd.MM.yyyy");
            this.gvPurMatRVar.Columns[5].HeaderText = "Price On <br />" + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd.MM.yyyy");
            this.LoadGrid();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();

            string reqno = "", matcode = "", spcfcod = "";
            switch (rpt)
            {
                case "DayWisePurchase":
                case "IndSup":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string mrrno = dt1.Rows[0]["mrrno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["mrrno"].ToString() == mrrno)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            mrrno = dt1.Rows[j]["mrrno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["mrrno1"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";
                            }

                            if (dt1.Rows[j]["mrrno"].ToString() == mrrno)
                            {
                                dt1.Rows[j]["mrrno1"] = "";
                            }
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            mrrno = dt1.Rows[j]["mrrno"].ToString();

                        }

                    }

                    break;

                case "PurSum":
                    break;

                case "PenBill":
                    break;



                case "Purchasetrk":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();

                    }



                    //reqno = dt1.Rows[0]["reqno"].ToString();
                    //matcode = dt1.Rows[0]["rsircode"].ToString();
                    // spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //    {

                    //        dt1.Rows[j]["reqno1"] = "";
                    //        dt1.Rows[j]["mrfno"] = "";
                    //        dt1.Rows[j]["reqdat"] = "";
                    //        dt1.Rows[j]["shipsupdat"] = "";
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";

                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    //        {
                    //            dt1.Rows[j]["reqno1"] = "";
                    //            dt1.Rows[j]["mrfno"] = "";
                    //            dt1.Rows[j]["reqdat"] = "";
                    //            dt1.Rows[j]["shipsupdat"] = "";
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //        }
                    //         if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //             dt1.Rows[j]["rsirdesc"] = "";
                    //         if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //             dt1.Rows[j]["spcfdesc"] = "";





                    //    }


                    //    reqno = dt1.Rows[j]["reqno"].ToString();
                    //    matcode = dt1.Rows[j]["rsircode"].ToString();
                    //    spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //}

                    break;




                case "PurBilltk":
                    //reqno = dt1.Rows[0]["reqno"].ToString();
                    //matcode = dt1.Rows[0]["rsircode"].ToString();
                    //spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //    {

                    //        dt1.Rows[j]["reqno1"] = "";
                    //        dt1.Rows[j]["mrfno"] = "";
                    //        dt1.Rows[j]["reqdat"] = "";
                    //        dt1.Rows[j]["shipsupdat"] = "";
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";

                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    //        {
                    //            dt1.Rows[j]["reqno1"] = "";
                    //            dt1.Rows[j]["mrfno"] = "";
                    //            dt1.Rows[j]["reqdat"] = "";
                    //            dt1.Rows[j]["shipsupdat"] = "";
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //        }
                    //        if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //            dt1.Rows[j]["rsirdesc"] = "";
                    //        if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //            dt1.Rows[j]["spcfdesc"] = "";





                    //    }


                    //    reqno = dt1.Rows[j]["reqno"].ToString();
                    //    matcode = dt1.Rows[j]["rsircode"].ToString();
                    //    spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //}

                    break;


                case "Purchasetrk02":
                    //string ppactcode = dt1.Rows[0]["pactcode"].ToString();
                    //string matcode = dt1.Rows[0]["rsircode"].ToString();
                    //string spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["pactcode"].ToString() == ppactcode && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() ==spcfcod)
                    //    {
                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";
                    //        dt1.Rows[j]["areqty"] = 0.0000000;
                    //    }

                    //    else
                    //    {
                    //         if (dt1.Rows[j]["pactcode"].ToString() == ppactcode)
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //         if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //             dt1.Rows[j]["rsirdesc"] = "";
                    //         if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //             dt1.Rows[j]["spcfdesc"] = "";




                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //    }
                    //}

                    break;



                case "MatRateVar":

                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                        }

                        rsircode = dt1.Rows[j]["rsircode"].ToString();

                    }

                    break;


            }


            return dt1;

        }


        private void LoadGrid()
        {

            try
            {
                DataTable dt = ((DataTable)Session["tblpurchase"]).Copy();

                if ((dt.Rows.Count == 0)) //Problem
                    return;

                string rpt = this.Request.QueryString["Type"].ToString().Trim();
                switch (rpt)
                {
                    case "DayWisePurchase":
                    case "IndSup":

                        int i;
                        for (i = 1; i < this.gvPurStatus.Columns.Count; i++)
                            this.gvPurStatus.Columns[i].Visible = false;
                        int j = 1;
                        foreach (ListItem ltem in ddlInformation.Items)
                        {
                            if (ltem.Selected)
                            {
                                this.gvPurStatus.Columns[j].Visible = true;
                                this.gvPurStatus.Columns[j].HeaderText = ltem.Text.Trim();
                            }

                            j++;



                        }

                        //for (i = 0; i < this.ddlInformation.Items.Count; i++)
                        //{


                        //    if (this.ddlInformation.Items[i].Selected)
                        //    {
                        //        this.gvPurStatus.Columns[j].Visible = true;
                        //        this.gvPurStatus.Columns[j].HeaderText = this.ddlInformation.Items[i].Text.Trim();


                        //    }

                        //    j++;

                        //}

                        this.gvPurStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPurStatus.DataSource = dt;
                        this.gvPurStatus.DataBind();
                        ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                             0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        if (ddlProjectName.SelectedValue.ToString() != "000000000000")
                        {

                            if (ddlMatCode.SelectedValue.ToString() != "000000000000")
                            {
                                ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ?
                                                0 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                            }

                        }
                        break;

                    case "PurSum":
                        this.gvPurSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPurSum.DataSource = dt;
                        this.gvPurSum.DataBind();
                        ((Label)this.gvPurSum.FooterRow.FindControl("lgvFAmtS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                             0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        break;

                    case "PenBill":



                        break;

                    case "Purchasetrk":
                        this.gvPurstk01.DataSource = dt;
                        this.gvPurstk01.DataBind();

                        break;
                    case "BillRegTrack":
                        this.gvBillRegTrack.DataSource = dt;
                        this.gvBillRegTrack.DataBind();
                        break;
                    case "Purchasetrk02":
                        DataTable dt1 = (DataTable)Session["tblpurchase1"];
                        this.gvPurstk.DataSource = dt;
                        this.gvPurstk.DataBind();

                        this.gvPurstk2.DataSource = dt1;
                        this.gvPurstk2.DataBind();

                        break;


                    case "BgdBal":
                        this.gvBgdBal.DataSource = dt;
                        this.gvBgdBal.DataBind();


                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFareqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(areqty)", "")) ?
                                                0 : dt.Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFprogqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(progqty)", "")) ?
                                            0 : dt.Compute("sum(progqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFordrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                                            0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFmrrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                            0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0;(#,##0); ");
                        break;


                    case "PurBilltk":
                        this.gvPurBilltk.DataSource = dt;
                        this.gvPurBilltk.DataBind();
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = ("grp='F'");
                        dt = dv.ToTable();

                        ((Label)this.gvPurBilltk.FooterRow.FindControl("lblgvFbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        break;

                    case "MatRateVar":
                        this.gvPurMatRVar.DataSource = dt;
                        this.gvPurMatRVar.DataBind();
                        break;

                }

            }
            catch (Exception ex)
            {


            }




        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvPurStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvPurSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.LoadGrid();
        }





        protected void imgbtnFindReqno02_Click(object sender, EventArgs e)
        {
            this.GetReqno02();
        }
        protected void imgbtnFindBill_Click(object sender, EventArgs e)
        {
            this.GetBillNo();
        }

        protected void imgbtnFindMatCom_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtfindMat = "%" + this.txtMatcomSearch.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETMATERIALCOM", txtfindMat, "", "", "", "", "", "", "", "");
            this.ddlMaterialscom.DataTextField = "sirdesc";
            this.ddlMaterialscom.DataValueField = "sircode";
            this.ddlMaterialscom.DataSource = ds1.Tables[0];
            this.ddlMaterialscom.DataBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {


            string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            int index1 = (url.Contains("&")) ? url.IndexOf('&') : url.Length;
            int index2 = (url.Contains("&")) ? url.Substring(index1 + 1).IndexOf('&') : 0;

            int indexofamp = index1 + index2;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!(Convert.ToBoolean(dr1[0]["entry"])))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You Have No Permission');", true);
                return;

            }



            string comcod = this.GetComeCode();
            string Billno = this.ddlBillno.SelectedValue.ToString();
            bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_REQ_STATUS", "DELETEPURCHASE", Billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Fail');", true);
                return;

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);




        }


        protected void gvPurstk01_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {




                string grpdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpdesc")).ToString().Trim();

                if (grpdesc == "")
                    return;


                if (grpdesc == "1. Requisition" || grpdesc == "2. Requisition Checked" || grpdesc == "3. Requisition Approved" || grpdesc == "4. Order Process"
                        || grpdesc == "5. Purchase Order" || grpdesc == "6. Materials Received" || grpdesc == "7. Bill Confirmation" || grpdesc == "11. Cheque Preparation" || grpdesc == "12. Reconcilation")
                {


                    e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";





                }



            }


        }

        protected void ddlProjectName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnFindMat_Click(null, null);
            this.colorBind();


        }
    }
}