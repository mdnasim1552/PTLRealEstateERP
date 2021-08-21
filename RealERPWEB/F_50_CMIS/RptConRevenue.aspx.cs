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
namespace RealERPWEB.F_50_CMIS
{
    public partial class RptConRevenue : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "REVENUE STATUS";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.ViewSelection();
                this.NameChange();
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


        private void ViewSelection()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {

                case "AllProDuesCollect":

                    this.MultiView1.ActiveViewIndex = 0;
                    break;


            }

        }
        private void NameChange()
        {

            string type = this.Request.QueryString["Type"].ToString();
            string comcod = this.GetCompCode();
            switch (type)
            {



                case "AllProDuesCollect":
                    this.dgvAccRec03.Columns[6].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
                    this.dgvAccRec03.Columns[8].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Land Cost" : "Apartment Cost ";
                    this.dgvAccRec03.Columns[9].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking & Others";
                    break;
            }



        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {


                case "AllProDuesCollect":
                    this.PrintAllProDuesCollection();
                    break;

            }


        }










        private void PrintAllProDuesCollection()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tbltosusold"];
            ReportDocument rptRcvList = new RealERPRPT.R_50_CMIS.RptMisAllProDuesColl02();
            TextObject rpttxtCompName = rptRcvList.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtCompName.Text = comnam;

            TextObject txtdeptpredues = rptRcvList.ReportDefinition.ReportObjects["txtdeptpredues"] as TextObject;
            txtdeptpredues.Text = "Dues up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            TextObject txtdeptcdues = rptRcvList.ReportDefinition.ReportObjects["txtdeptcdues"] as TextObject;
            txtdeptcdues.Text = "Current  Dues " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            TextObject txtsize = rptRcvList.ReportDefinition.ReportObjects["txtsize"] as TextObject;
            txtsize.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
            TextObject txtaptcost = rptRcvList.ReportDefinition.ReportObjects["txtaptcost"] as TextObject;
            txtaptcost.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price";
            TextObject txtparking = rptRcvList.ReportDefinition.ReportObjects["txtparking"] as TextObject;
            txtparking.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking & Others";
            TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Stock, Sales, Received & Dues Statement -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            TextObject rpttxttoduesupto = rptRcvList.ReportDefinition.ReportObjects["txttoduesupto"] as TextObject;
            rpttxttoduesupto.Text = "Dues Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            TextObject rpttxtpredues = rptRcvList.ReportDefinition.ReportObjects["txtpredues"] as TextObject;
            rpttxtpredues.Text = "Previous Dues up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            TextObject rpttxtcurrentdues = rptRcvList.ReportDefinition.ReportObjects["txtcurrentdues"] as TextObject;
            rpttxtcurrentdues.Text = "Current  Dues " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");




            TextObject txtpmdues = rptRcvList.ReportDefinition.ReportObjects["txtpmdues"] as TextObject;
            txtpmdues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53001'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53001'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtpcrdues = rptRcvList.ReportDefinition.ReportObjects["txtpcrdues"] as TextObject;
            txtpcrdues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53002'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53002'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtdealy = rptRcvList.ReportDefinition.ReportObjects["txtdealy"] as TextObject;
            txtdealy.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53003'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53003'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtdealyins = rptRcvList.ReportDefinition.ReportObjects["txtdealyins"] as TextObject;
            txtdealyins.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53004'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53004'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";


            TextObject txtdishonour = rptRcvList.ReportDefinition.ReportObjects["txtdishonour"] as TextObject;
            txtdishonour.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53005'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53005'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";


            TextObject txtptodues = rptRcvList.ReportDefinition.ReportObjects["txtptodues"] as TextObject;
            txtptodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='AAAAA'").Length > 0 ? Convert.ToDouble(dt1.Select("code='AAAAA'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";


            TextObject txtcmdues = rptRcvList.ReportDefinition.ReportObjects["txtcmdues"] as TextObject;
            txtcmdues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53001'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53001'")[0]["cdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtccrdues = rptRcvList.ReportDefinition.ReportObjects["txtccrdues"] as TextObject;
            txtccrdues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53002'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53002'")[0]["cdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtctodues = rptRcvList.ReportDefinition.ReportObjects["txtctodues"] as TextObject;
            txtctodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='AAAAA'").Length > 0 ? Convert.ToDouble(dt1.Select("code='AAAAA'")[0]["cdues"]).ToString("#,##0;(#,##0);") : "";



            TextObject txtmtodues = rptRcvList.ReportDefinition.ReportObjects["txtmtodues"] as TextObject;
            txtmtodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53001'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53001'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";

            //Edit

            //double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double pdues = Convert.ToDouble(dt1.Select("code='53001'")[0]["pdues"]);
            double pdues1 = Convert.ToDouble(dt1.Select("code='53002'")[0]["pdues"]);

            // double medical = Convert.ToDouble(dtsaladd.Select("gcod='04004'")[0]["gval"]);

            double cdues = Convert.ToDouble(dt1.Select("code='53001'")[0]["cdues"]);
            double cdues1 = Convert.ToDouble(dt1.Select("code='53002'")[0]["cdues"]);

            double todue = Convert.ToDouble(dt1.Select("code='53001'")[0]["todues"]);
            double todue1 = Convert.ToDouble(dt1.Select("code='53002'")[0]["todues"]);

            double pduesto = (pdues + pdues1);
            double crduesto = (cdues + cdues1);
            double totaldues = (todue + todue1);


            double pdues3 = Convert.ToDouble(dt1.Select("code='53003'")[0]["pdues"]);
            double pdues4 = Convert.ToDouble(dt1.Select("code='53004'")[0]["pdues"]);
            double pdues5 = Convert.ToDouble(dt1.Select("code='53005'")[0]["pdues"]);

            double cdues3 = Convert.ToDouble(dt1.Select("code='53003'")[0]["cdues"]);
            double cdues4 = Convert.ToDouble(dt1.Select("code='53004'")[0]["cdues"]);
            double cdues5 = Convert.ToDouble(dt1.Select("code='53005'")[0]["cdues"]);

            double todue3 = Convert.ToDouble(dt1.Select("code='53003'")[0]["todues"]);
            double todue4 = Convert.ToDouble(dt1.Select("code='53004'")[0]["todues"]);
            double todue5 = Convert.ToDouble(dt1.Select("code='53005'")[0]["todues"]);


            double pduesto2 = (pdues3 + pdues4 + pdues5);
            double crduesto2 = (cdues3 + cdues4 + cdues5);
            double totaldues2 = (todue3 + todue4 + todue5);


            TextObject subpdusto = rptRcvList.ReportDefinition.ReportObjects["subpdusto"] as TextObject;
            subpdusto.Text = pduesto.ToString("#,##0;(#,##0)");

            TextObject subcdusto = rptRcvList.ReportDefinition.ReportObjects["subcdusto"] as TextObject;
            subcdusto.Text = crduesto.ToString("#,##0;(#,##0)");

            TextObject txttotaldues = rptRcvList.ReportDefinition.ReportObjects["txttotaldues"] as TextObject;
            txttotaldues.Text = totaldues.ToString("#,##0;(#,##0)");


            TextObject subpdusto2 = rptRcvList.ReportDefinition.ReportObjects["subpdusto2"] as TextObject;
            subpdusto2.Text = pduesto2.ToString("#,##0;(#,##0)");

            TextObject subcdusto2 = rptRcvList.ReportDefinition.ReportObjects["subcdusto2"] as TextObject;
            subcdusto2.Text = crduesto2.ToString("#,##0;(#,##0)");

            TextObject txttotaldues2 = rptRcvList.ReportDefinition.ReportObjects["txttotaldues2"] as TextObject;
            txttotaldues2.Text = totaldues2.ToString("#,##0;(#,##0)");




            //

            TextObject txtcrtodues = rptRcvList.ReportDefinition.ReportObjects["txtcrtodues"] as TextObject;
            txtcrtodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53002'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53002'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtdtodues = rptRcvList.ReportDefinition.ReportObjects["txtdtodues"] as TextObject;
            txtdtodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53003'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53003'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtdtoduesins = rptRcvList.ReportDefinition.ReportObjects["txtdtoduesins"] as TextObject;
            txtdtoduesins.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53004'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53004'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtdtodish = rptRcvList.ReportDefinition.ReportObjects["txtdtodish"] as TextObject;
            txtdtodish.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53005'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53005'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";



            //



            TextObject txtnettodues = rptRcvList.ReportDefinition.ReportObjects["txtnettodues"] as TextObject;
            txtnettodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='AAAAA'").Length > 0 ? Convert.ToDouble(dt1.Select("code='AAAAA'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";




            TextObject txtuserinfo = rptRcvList.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptRcvList.SetDataSource(this.HiddenSameData((DataTable)Session["tblAccRec"]));
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Received List Info";
                string eventdesc = "Print Report MR";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptRcvList.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptRcvList;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {

                case "AllProDuesCollect":
                    this.AllProDuesCollection();
                    break;

            }



        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "AllProDuesCollect":
                    string comcod = dt1.Rows[0]["comcod"].ToString();
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["comcod"].ToString() == comcod && dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            dt1.Rows[j]["comnam"] = "";
                            dt1.Rows[j]["grpdesc"] = "";

                        }


                        else
                        {
                            if (dt1.Rows[j]["comcod"].ToString() == comcod)
                                dt1.Rows[j]["comnam"] = "";
                            if (dt1.Rows[j]["grp"].ToString() == grp)
                                dt1.Rows[j]["grpdesc"] = "";

                        }

                        comcod = dt1.Rows[j]["comcod"].ToString();
                        grp = dt1.Rows[j]["grp"].ToString();



                    }

                    break;

            }


            return dt1;
        }
        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAccRec"];
                string type = this.Request.QueryString["Type"].ToString();
                switch (type)
                {
                    case "AllProDuesCollect":
                        this.dgvAccRec03.Columns[17].HeaderText = "Dues Up to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM- yyyy");
                        this.dgvAccRec03.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec03.DataSource = dt;
                        this.dgvAccRec03.DataBind();
                        Session["Report1"] = dgvAccRec03;
                        if (dt.Rows.Count > 0)
                            ((HyperLink)this.dgvAccRec03.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        this.FooterCalculation();
                        break;


                }





            }

            catch (Exception e)
            {
            }



        }

        private void FooterCalculation()
        {
            DataTable dt = ((DataTable)Session["tblAccRec"]).Copy();
            if (dt.Rows.Count == 0)
                return;
            string pactcode = "";
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {




                case "AllProDuesCollect":

                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("pactcode  like 'HHHHAAAAAAAA%'");
                    dt = dv.ToTable();

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtstkamal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tstkam)", "")) ?
                     0.00 : dt.Compute("Sum(tstkam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFususizeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ususize)", "")) ?
                       0.00 : dt.Compute("Sum(ususize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFusuamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usamt)", "")) ?
                       0.00 : dt.Compute("Sum(usamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFusizeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usize)", "")) ?
                          0.00 : dt.Compute("Sum(usize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFaptcostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aptcost)", "")) ?
                         0.00 : dt.Compute("Sum(aptcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFcpaocostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cpaocost)", "")) ?
                        0.00 : dt.Compute("Sum(cpaocost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtocostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                   0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFatoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(atodues)", "")) ?
                    0.00 : dt.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtotalduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todues)", "")) ?
                   0.00 : dt.Compute("Sum(todues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgFEncashal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reconamt)", "")) ?
                   0.00 : dt.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtretamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(retcheque)", "")) ?
                   0.00 : dt.Compute("Sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtframtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(fcheque)", "")) ?
                   0.00 : dt.Compute("Sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtpdamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pcheque)", "")) ?
                   0.00 : dt.Compute("Sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoreceivedal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                    0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                   0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpbookingal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pbookam)", "")) ?
                   0.00 : dt.Compute("Sum(pbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpinstallmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pinsam)", "")) ?
               0.00 : dt.Compute("Sum(pinsam)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFCbookingal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbookam)", "")) ?
                   0.00 : dt.Compute("Sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFCinstallmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cinsam)", "")) ?
                   0.00 : dt.Compute("Sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoCInstalmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ctodues)", "")) ?
               0.00 : dt.Compute("Sum(ctodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFvbaamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(vbamt)", "")) ?
                           0.00 : dt.Compute("Sum(vbamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdelchargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
                     0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdischargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(discharge)", "")) ?
                    0.00 : dt.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFnettoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ntodues)", "")) ?
                   0.00 : dt.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");
                    break;



            }







        }






        private void AllProDuesCollection()
        {
            this.pnlIndPro.Visible = true;
            string comcod = this.GetCompCode();

            //string comcod = "7101";

            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string searchinfo = "";
            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(vtodues between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( vtodues " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }



            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_MISCON_SALMGT", "RPTDATEWALLPROINSDUES", "", frmdate, todate, searchinfo, "", "", "", "", "");

            if (ds2 == null)
            {
                this.dgvAccRec03.DataSource = null;
                this.dgvAccRec03.DataBind();
                return;
            }
            Session["tblAccRec"] = HiddenSameData(ds2.Tables[0]);


            ViewState["tbltosusold"] = ds2.Tables[1];



            this.Data_Bind();
            this.ShowSummary();


            //



        }
        private void ShowSummary()
        {
            DataTable dt = (DataTable)ViewState["tbltosusold"];
            this.gvinpro.Columns[2].HeaderText = "Dues Up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMMM-yyyy");
            this.gvinpro.Columns[3].HeaderText = "Current Dues " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            this.gvinpro.DataSource = dt;
            this.gvinpro.DataBind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }


        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblToCash.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.txtAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
        }


        protected void dgvAccRec03_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvAccRec03.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void dgvAccRec03_RowDataBound(object sender, GridViewRowEventArgs e)
        {







            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HLgvDesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label lgvtstkamal = (Label)e.Row.FindControl("lgvtstkamal");
                Label lgvusunitsizeal = (Label)e.Row.FindControl("lgvusunitsizeal");
                Label lgvusuamtal = (Label)e.Row.FindControl("lgvusuamtal");
                Label lgvunitsizeal = (Label)e.Row.FindControl("lgvunitsizeal");
                Label lgvaptcostal = (Label)e.Row.FindControl("lgvaptcostal");
                Label lgvcpaocostal = (Label)e.Row.FindControl("lgvcpaocostal");
                Label lgvtocsotal = (Label)e.Row.FindControl("lgvtocsotal");
                Label lgvEncashal = (Label)e.Row.FindControl("lgvEncashal");
                Label lgvtretamtal = (Label)e.Row.FindControl("lgvtretamtal");
                Label lgvtframtal = (Label)e.Row.FindControl("lgvtframtal");
                Label lgvtpdamtal = (Label)e.Row.FindControl("lgvtpdamtal");
                Label lgvtotreceivedal = (Label)e.Row.FindControl("lgvtotreceivedal");
                Label lgvtatoduesall = (Label)e.Row.FindControl("lgvtatoduesall");
                Label lgvtotalduesal = (Label)e.Row.FindControl("lgvtotalduesal");
                Label lgvtoduesal = (Label)e.Row.FindControl("lgvtoduesal");
                Label lgvpbduesal = (Label)e.Row.FindControl("lgvpbduesal");
                Label lgvpinsduesall = (Label)e.Row.FindControl("lgvpinsduesall");
                Label lgvCbookingal = (Label)e.Row.FindControl("lgvCbookingal");
                Label lgvCinstallmental = (Label)e.Row.FindControl("lgvCinstallmental");
                Label lgvCoCInstalmental = (Label)e.Row.FindControl("lgvCoCInstalmental");
                Label lgvvbaamtal = (Label)e.Row.FindControl("lgvvbaamtal");
                Label lgvdelchargeal = (Label)e.Row.FindControl("lgvdelchargeal");
                Label lgvdischargeal = (Label)e.Row.FindControl("lgvdischargeal");
                Label lgvnettoduesal = (Label)e.Row.FindControl("lgvnettoduesal");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDesc.Font.Bold = true;
                    lgvtstkamal.Font.Bold = true;
                    lgvusunitsizeal.Font.Bold = true;
                    lgvusuamtal.Font.Bold = true;
                    lgvunitsizeal.Font.Bold = true;
                    lgvaptcostal.Font.Bold = true;
                    lgvcpaocostal.Font.Bold = true;
                    lgvtocsotal.Font.Bold = true;
                    lgvEncashal.Font.Bold = true;
                    lgvtretamtal.Font.Bold = true;
                    lgvtframtal.Font.Bold = true;
                    lgvtpdamtal.Font.Bold = true;
                    lgvtotreceivedal.Font.Bold = true;
                    lgvtatoduesall.Font.Bold = true;
                    lgvtotalduesal.Font.Bold = true;
                    lgvtoduesal.Font.Bold = true;
                    lgvpbduesal.Font.Bold = true;
                    lgvpinsduesall.Font.Bold = true;
                    lgvCbookingal.Font.Bold = true;
                    lgvCinstallmental.Font.Bold = true;
                    lgvCoCInstalmental.Font.Bold = true;
                    lgvCbookingal.Font.Bold = true;
                    lgvvbaamtal.Font.Bold = true;
                    lgvdelchargeal.Font.Bold = true;
                    lgvdischargeal.Font.Bold = true;
                    lgvnettoduesal.Font.Bold = true;
                    // actdesc.Style.Add("text-align", "right");


                }

                else
                {
                    string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                    string frmdate = this.txtfrmdate.Text;
                    string todate = this.txttodate.Text;
                    HLgvDesc.NavigateUrl = "~/F_23_CR/LinkRptSaleDues.aspx?Type=DuesCollect&pactcode=" + code + "&pactdesc=" + pactdesc + "&Date1=" + frmdate + "&Date2=" + todate;


                }

            }


        }

    }
}











