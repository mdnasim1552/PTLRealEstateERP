using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using System.Data;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using System.IO;
namespace RealERPWEB.F_23_CR
{

    public partial class LinkRptSaleDues : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        UserManSales objUserService = new UserManSales();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                this.ViewSelection();
                this.NameChange();
                this.CompanyColumnVisible();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "DuesCollect") ? "Dues Collection Statment Report"
                    : (this.Request.QueryString["Type"].ToString() == "WeeklyColl") ? "Weekly Collection" : "Dues Collection -Summary";
                //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            }

        }

        private void CompanyColumnVisible()
        {


            try
            {
                string comcod = this.GetCompCode();
                switch (comcod)
                {
                    case "3339":   //Tropical
                                   //case "3101":
                        this.dgvAccRec02.Columns[6].Visible = false;
                        this.dgvAccRec02.Columns[7].Visible = false;
                        this.dgvAccRec02.Columns[8].Visible = false;
                        this.dgvAccRec02.Columns[9].Visible = false;
                        this.dgvAccRec02.Columns[10].Visible = false;
                        this.dgvAccRec02.Columns[11].Visible = false;
                        this.dgvAccRec02.Columns[13].Visible = false;
                        this.dgvAccRec02.Columns[14].Visible = false;
                        this.dgvAccRec02.Columns[15].Visible = false;
                        this.dgvAccRec02.Columns[16].Visible = false;
                        this.dgvAccRec02.Columns[18].Visible = false;
                        this.dgvAccRec02.Columns[19].Visible = false;
                        this.dgvAccRec02.Columns[20].Visible = false;
                        this.dgvAccRec02.Columns[21].Visible = false;
                        this.dgvAccRec02.Columns[22].Visible = false;
                        this.dgvAccRec02.Columns[24].Visible = false;
                        this.dgvAccRec02.Columns[25].Visible = false;
                        this.dgvAccRec02.Columns[28].Visible = false;
                        this.dgvAccRec02.Columns[29].Visible = false;
                        this.dgvAccRec02.Columns[30].Visible = false;
                        this.dgvAccRec02.Columns[31].Visible = true;

                        break;

                    default:
                        break;

                }

            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

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


                case "DuesCollect":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblProjectName.Text = this.Request.QueryString["pactdesc"].ToString();
                    this.lbldaterange.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowDuesCollection();
                    break;

                case "WeeklyColl":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.lblProjectName.Text = this.Request.QueryString["pactdesc"].ToString();
                    this.lbldaterange.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.showWeeklyCollection();
                    break;




            }

        }
        private void NameChange()
        {

            string type = this.Request.QueryString["Type"].ToString();
            string comcod = this.GetCompCode();
            switch (type)
            {


                case "DuesCollect":

                    this.dgvAccRec02.Columns[4].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
                    this.dgvAccRec02.Columns[6].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Land Cost" : "Apartment Cost";
                    this.dgvAccRec02.Columns[7].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
                    break;

            }



        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {


                case "DuesCollect":
                    string comcod = this.GetCompCode();

                    switch (comcod)
                    {
                        case "3339":
                        case "3101":
                            this.printDuesCollectionTropical();
                            break;

                        default:
                            this.printDuesCollection();
                            break;
                    }



                    break;
                case "WeeklyColl":
                    this.printWeeklyColl();
                    break;

            }


        }



        private void printWeeklyColl()
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
            string Ftdate = this.lbldaterange.Text.ToString(); //= "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
            string ProjectNam = this.lblProjectName.Text.ToString();

            LocalReport Rpt1 = new LocalReport();
            //DataTable dt = (DataTable)Session["tblbgd"];
            //DataTable dt1 = (DataTable)Session["tblbgd"];

            var lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly>)ViewState["listweekcol"];
            if (lst == null)
                return;



            //DataView dv = dt1.DefaultView;
            //dv.RowFilter = ("rptcod   like '%00' and rptcod   like '4%' ");
            //dt1 = dv.ToTable();
            //double totalamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(rptamt)", "")) ?
            //                    0 : dt1.Compute("sum(rptamt)", "")));





            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptWeeklyCollection", lst, null, null);

            // Rpt1.SetParameters(new ReportParameter("totalamt", totalamt.ToString("#,##0.00;(#,##0.00); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Ftdate", Ftdate));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + ProjectNam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "WEEKLY COLLECTION"));

            //Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void printDuesCollectionTropical()
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
            string Ftdate = this.lbldaterange.Text;



            DataTable dt = (DataTable)Session["tblAccRec"];
            string pactdesc = dt.Rows[0]["pactdesc"].ToString();



            //var lst = dt.DataTableToList< C_23_CRR.EClassSales_03.DueCollStatmentRe>();
            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassDuesAOverDuesIndPro>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptDuesAOverDuesInd", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtComName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtfrmatodate", Ftdate));
            Rpt1.SetParameters(new ReportParameter("txtProject", pactdesc));


            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private void printDuesCollection()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptDuesCollection();
            DataTable dt1 = (DataTable)ViewState["tbltosusold"];
            TextObject rpttxtCompName = rptRcvList.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtCompName.Text = comnam;



            TextObject txtsize = rptRcvList.ReportDefinition.ReportObjects["txtsize"] as TextObject;
            txtsize.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
            TextObject txtaptcost = rptRcvList.ReportDefinition.ReportObjects["txtaptcost"] as TextObject;
            txtaptcost.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price";
            TextObject txtparking = rptRcvList.ReportDefinition.ReportObjects["txtparking"] as TextObject;
            txtparking.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
            TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Monthly Installment Due -  " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("MMMM-yyyy");
            TextObject rpttxttoduesupto = rptRcvList.ReportDefinition.ReportObjects["txttoduesupto"] as TextObject;
            rpttxttoduesupto.Text = "Dues Up to " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("MMM-yyyy");
            TextObject rpttxtpredues = rptRcvList.ReportDefinition.ReportObjects["txtpredues"] as TextObject;
            rpttxtpredues.Text = "Previous Due up to " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("MMM-yyyy");
            TextObject rpttxtcurrentdues = rptRcvList.ReportDefinition.ReportObjects["txtcurrentdues"] as TextObject;
            rpttxtcurrentdues.Text = "Current  Due " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("MMMM-yyyy");



            TextObject txtsoldqty = rptRcvList.ReportDefinition.ReportObjects["txtsoldqty"] as TextObject;
            txtsoldqty.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtunsoldqty = rptRcvList.ReportDefinition.ReportObjects["txtunsoldqty"] as TextObject;
            txtunsoldqty.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";

            TextObject txttoqty = rptRcvList.ReportDefinition.ReportObjects["txttoqty"] as TextObject;
            txttoqty.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";


            TextObject txtsoldsize = rptRcvList.ReportDefinition.ReportObjects["txtsoldsize"] as TextObject;
            txtsoldsize.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtunsoldsize = rptRcvList.ReportDefinition.ReportObjects["txtunsoldsize"] as TextObject;
            txtunsoldsize.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";

            TextObject txttosize = rptRcvList.ReportDefinition.ReportObjects["txttosize"] as TextObject;
            txttosize.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtsoldrate = rptRcvList.ReportDefinition.ReportObjects["txtsoldrate"] as TextObject;
            txtsoldrate.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtunsoldrate = rptRcvList.ReportDefinition.ReportObjects["txtunsoldrate"] as TextObject;
            txtunsoldrate.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";

            TextObject txttorate = rptRcvList.ReportDefinition.ReportObjects["txttorate"] as TextObject;
            txttorate.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtsoldamt = rptRcvList.ReportDefinition.ReportObjects["txtsoldamt"] as TextObject;
            txtsoldamt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtunsoldamt = rptRcvList.ReportDefinition.ReportObjects["txtunsoldamt"] as TextObject;
            txtunsoldamt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            TextObject txttoamt = rptRcvList.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            txttoamt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtsoldpercnt = rptRcvList.ReportDefinition.ReportObjects["txtsoldpercnt"] as TextObject;
            txtsoldpercnt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";

            TextObject txtunsoldpercnt = rptRcvList.ReportDefinition.ReportObjects["txtunsoldpercnt"] as TextObject;
            txtunsoldpercnt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";

            TextObject txttopercnt = rptRcvList.ReportDefinition.ReportObjects["txttopercnt"] as TextObject;
            txttopercnt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";





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





        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "AllProDuesCollect":
                    string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpdesc"].ToString() == grpdesc)
                        {
                            grpdesc = dt1.Rows[j]["grpdesc"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        else
                        {
                            grpdesc = dt1.Rows[j]["grpdesc"].ToString();
                        }

                    }

                    break;
                default:
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }

                        else
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                        }

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


                    case "DuesCollect":
                        this.dgvAccRec02.Columns[18].HeaderText = "Receivable Up to " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("MMM- yyyy");
                        this.dgvAccRec02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec02.DataSource = dt;
                        this.dgvAccRec02.DataBind();


                        if (dt.Rows.Count > 0)
                        {

                            Session["Report1"] = dgvAccRec02;
                            if (dt.Rows.Count > 0)

                                ((HyperLink)this.dgvAccRec02.HeaderRow.FindControl("hlbtntbCdataExel1")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
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
            DataTable dt = (DataTable)Session["tblAccRec"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {


                case "DuesCollect":
                    double usize = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usize)", "")) ? 0.00 : dt.Compute("Sum(usize)", "")));
                    double aptccost = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aptcost)", "")) ? 0.00 : dt.Compute("Sum(aptcost)", "")));
                    double avgrate = (usize == 0) ? 0.00 : (aptccost / usize);
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFunitsize")).Text = usize.ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFavgrate")).Text = avgrate.ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFaptcost")).Text = aptccost.ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFcpcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cpcost)", "")) ?
                        0.00 : dt.Compute("Sum(cpcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFutilitycost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(utltycost)", "")) ?
                        0.00 : dt.Compute("Sum(utltycost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFothcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(othcost)", "")) ?
                    0.00 : dt.Compute("Sum(othcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtocost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                        0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgFEncash")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reconamt)", "")) ?
                        0.00 : dt.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtretamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(retcheque)", "")) ?
                        0.00 : dt.Compute("Sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtframt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(fcheque)", "")) ?
                        0.00 : dt.Compute("Sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtpdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pcheque)", "")) ?
                        0.00 : dt.Compute("Sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((HyperLink)this.dgvAccRec02.FooterRow.FindControl("hlnkgvFtoreceived")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                        0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFatodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(atodues)", "")) ?
                       0.00 : dt.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtotaldues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todues)", "")) ?
                        0.00 : dt.Compute("Sum(todues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                        0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFpbooking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pbookam)", "")) ?
                        0.00 : dt.Compute("Sum(pbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFpinstallment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pinsam)", "")) ?
                    0.00 : dt.Compute("Sum(pinsam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFpretodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ptodues)", "")) ?
                        0.00 : dt.Compute("Sum(ptodues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFCbooking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbookam)", "")) ?
                        0.00 : dt.Compute("Sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFCinstallment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cinsam)", "")) ?
                        0.00 : dt.Compute("Sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtoCInstalment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ctodues)", "")) ?
                    0.00 : dt.Compute("Sum(ctodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFvtodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(vtodues)", "")) ?
                    0.00 : dt.Compute("Sum(vtodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFdelcharge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
                    0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFdischarge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(discharge)", "")) ?
                    0.00 : dt.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFnettodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ntodues)", "")) ?
                   0.00 : dt.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");

                    break;





            }







        }






        private void ShowDuesCollection()
        {
            string comcod = this.GetCompCode();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string ProjectCode = this.Request.QueryString["pactcode"].ToString() + "%";
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWISEPROINSDUES", ProjectCode, frmdate, todate, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.dgvAccRec02.DataSource = null;
                this.dgvAccRec02.DataBind();
                return;
            }

            Session["tblpactusir"] = ds2.Tables[0];
            Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
            ViewState["tbltosusold"] = ds2.Tables[1];
            this.gvinpro.DataSource = ds2.Tables[1];
            this.gvinpro.DataBind();
            this.Data_Bind();

            for (int i = 0; i < dgvAccRec02.Rows.Count; i++)
            {

                string usircode = ((Label)dgvAccRec02.Rows[i].FindControl("lgusircode")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)dgvAccRec02.Rows[i].FindControl("lbtngacuname");
                LinkButton lbtn2 = (LinkButton)dgvAccRec02.Rows[i].FindControl("lbtngvnettodues");

                if (lbtn1.Text.Trim().Length > 0)
                    lbtn1.CommandArgument = usircode;
                if (lbtn2.Text.Trim().Length > 0)
                    lbtn2.CommandArgument = usircode;


            }



        }

        private void showWeeklyCollection()
        {


            try
            {
                string comcod = this.GetCompCode();
                string pactcode = this.Request.QueryString["pactcode"].ToString();
                string CurDate1 = this.Request.QueryString["Date1"].ToString();
                List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly> lst1 = objUserService.ShowWeeklySalesACollection(comcod, CurDate1, pactcode);
                if (lst1 == null)
                    return;

                ViewState["listweekcol"] = lst1;
                this.grvWeekSales.DataSource = lst1;
                this.grvWeekSales.DataBind();

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1")).Text = (lst1.Select(p => p.wsamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt2")).Text = (lst1.Select(p => p.wsamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt3")).Text = (lst1.Select(p => p.wsamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt4")).Text = (lst1.Select(p => p.wsamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt4).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt1")).Text = (lst1.Select(p => p.wcamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt2")).Text = (lst1.Select(p => p.wcamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt3")).Text = (lst1.Select(p => p.wcamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt4")).Text = (lst1.Select(p => p.wcamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt4).Sum().ToString("#,##0;(#,##0); ");


                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt2T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt3T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2 + p.wsamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt4T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2 + p.wsamt3 + p.wsamt4)).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt1T")).Text = lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt2T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt3T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2 + p.wcamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt4T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2 + p.wcamt3 + p.wcamt4)).Sum().ToString("#,##0;(#,##0); ");


            }
            catch (Exception ex)
            {

            }



        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }


        protected void dgvAccRec02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvAccRec02.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void dgvAccRec02_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //if (e.Row.RowType != DataControlRowType.DataRow)
                //    return;

                LinkButton hlink1 = (LinkButton)e.Row.FindControl("lbtngacuname");
                LinkButton hlink2 = (LinkButton)e.Row.FindControl("lbtngvnettodues");


                hlink1.Style.Add("color", "blue");
                hlink2.Style.Add("color", "blue");

            }
        }

        protected void lbtngacuname_Click(object sender, EventArgs e)
        {
            //string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;      
            string usircode = ((Label)this.dgvAccRec02.Rows[rownum].FindControl("lgusircode")).Text.Trim();           
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblAccRec"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dt = dv1.ToTable();

            string mTRNDAT1 = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("dd-MMM-yyyy");
            string mACTCODE = this.Request.QueryString["pactcode"].ToString();
            string uSIRCODE = dt.Rows[0]["usircode"].ToString();
            if (uSIRCODE == "")
            {
                return;
            }

            ///---------------------------------//// 

            lbljavascript.Text = @"<script>window.open('../F_22_Sal/LinkDuesColl.aspx?Type=ClientLedger&comcod=" + comcod + "&pactcode=" + mACTCODE + "&usircode=" +
                            uSIRCODE + "&Date1=" + mTRNDAT1 + "', target='_blank');</script>";
        }
        protected void lbtngvnettodues_Click(object sender, EventArgs e)
        {
            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblAccRec"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dt = dv1.ToTable();
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
            string mACTCODE = this.Request.QueryString["pactcode"].ToString();
            string uSIRCODE = dt.Rows[0]["usircode"].ToString();
            if (uSIRCODE == "")
            {
                return;
            }

            ///---------------------------------//// 

            lbljavascript.Text = @"<script>window.open('../F_22_Sal/LinkDuesColl.aspx?Type=CustInvoice&comcod=" + comcod + "&pactcode=" + mACTCODE + "&usircode=" +
                            uSIRCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "', target='_blank');</script>";
        }



        protected void lbtnsalesImg_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
            DataTable dt = (DataTable)Session["tblpactusir"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = dt.Rows[Rowindex]["pactcode"].ToString();
            string usircode = dt.Rows[Rowindex]["usircode"].ToString();



            // ViewState.Remove ("tblimages");
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SHOWIMG", pactcode, usircode, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = ("holder ='Customer'");

            ListCustomer.DataSource = dv1.ToTable();
            ListCustomer.DataBind();

            DataView dv2 = dt1.DefaultView;
            dv2.RowFilter = ("holder = 'Nominee'");
            ListNominee.DataSource = dv2.ToTable();
            ListNominee.DataBind();
            ds1.Dispose();

        }

        //protected void ListViewEmpAll_ItemDataBound ( object sender, ListViewItemEventArgs e )
        //{

        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        Image imgname = (Image)e.Item.FindControl ("GetImg");
        //        Label imglink = (Label)e.Item.FindControl ("ImgLink");
        //        string extension = Path.GetExtension (imglink.Text.ToString ());
        //        switch (extension)
        //        {
        //            case ".PNG":
        //            case ".png":
        //            case ".JPEG":
        //            case ".JPG":
        //            case ".jpg":
        //            case ".jpeg":
        //            case ".GIF":
        //            case ".gif":
        //                imgname.ImageUrl = imglink.Text.ToString ();
        //                break;


        //        }

        //    }

        //}

        protected void ListCustomer_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;


                }

            }
        }

        protected void ListNominee_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;


                }

            }
        }
        protected void grvWeekSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "WEEK-1";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 4;


                TableCell cell02 = new TableCell();
                cell02.Text = "WEEK-2";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 3;


                TableCell cell03 = new TableCell();
                cell03.Text = "WEEK-3";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 3;


                TableCell cell04 = new TableCell();
                cell04.Text = "WEEK-4";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                grvWeekSales.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}











