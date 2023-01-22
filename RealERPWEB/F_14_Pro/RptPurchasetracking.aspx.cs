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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptPurchasetracking : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                //this.Master.Page.Title = dr1[0]["dscrption"].ToString();


                this.ShowView();
                this.ShowValue();


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            //if (string.IsNullOrEmpty(this.Request.QueryString["comcod"].ToString()))
            //{
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    return (hst["comcod"].ToString());
            //}


            //Added By Nime 
            if (this.Request.QueryString.AllKeys.Contains("comcod"))
            {

                return this.Request.QueryString["comcod"].ToString();
            }


            //Commented
            //if (this.Request.QueryString["comcod"].Length > 0)
            //{
            //    return this.Request.QueryString["comcod"].ToString();
            //}
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }


        }

        private void Getddldata()
        {
            string comcod = this.GetComeCode();
            string mtreqno = (txtSrcRequisition01.Text == "" ? "%" : txtSrcRequisition01.Text);


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "TREQNO", mtreqno, "", "", "", "", "", "", "", "");
            ddltreq.DataSource = ds1.Tables[0];
            ddltreq.DataTextField = "mtreqno1";
            ddltreq.DataValueField = "mtreqno";
            ddltreq.DataBind();


        }

        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {


                case "Purchasetrk":

                    ((Label)this.Master.FindControl("lblTitle")).Text = "Day Wise Purchase Report";
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;



                case "TransferReqtrk":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Transfer Tracking Report";
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


            }
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "Purchasetrk":
                    this.RptPurchaseTrack();
                    break;

                case "TransferReqtrk":
                    this.RptTransferReqTrack();
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

        private void RptTransferReqTrack()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string reqno = this.Request.QueryString["reqno"].ToString();

            DataTable dt = ((DataTable)Session["tbltranstrk"]);
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EClassMaterialTransferTacking>();

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rpttxtCompanyName = comnam;

            string txtTitle = "Material Transfer Tracking";
            string projectdesc = "From Project: " + ((DataTable)Session["tblpactdesctrk"]).Rows[0]["fpactdesc"].ToString()
                + " To " + ((DataTable)Session["tblpactdesctrk"]).Rows[0]["tpactdesc"].ToString(); ;
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string narration = this.txtReqNarr.Text.Trim();

            LocalReport RptMaterialTransferTracking = new LocalReport();


            RptMaterialTransferTracking = RptSetupClass1.GetLocalReport("R_14_Pro.RptMaterialTransferTracking", lst, null, null);
            RptMaterialTransferTracking.EnableExternalImages = true;
            RptMaterialTransferTracking.SetParameters(new ReportParameter("ComLogo", ComLogo));
            RptMaterialTransferTracking.SetParameters(new ReportParameter("rpttxtCompanyName", comnam));
            RptMaterialTransferTracking.SetParameters(new ReportParameter("txtTitle", txtTitle));
            RptMaterialTransferTracking.SetParameters(new ReportParameter("projectdesc", projectdesc));
            RptMaterialTransferTracking.SetParameters(new ReportParameter("narration", narration));
            RptMaterialTransferTracking.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Session["Report1"] = RptMaterialTransferTracking;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void RptPurchaseTrack()
        {

           // DataTable dt2 = (DataTable)Session["tblreq"];
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string reqno = this.Request.QueryString["reqno"].ToString();

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurTrack01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchaseTra", list, null, null);
            rpt.SetParameters(new ReportParameter("compName", comnam));

           
            rpt.SetParameters(new ReportParameter("projectName", (((DataTable)Session["tblpactdesc"]).Select("reqno='" + reqno + "'"))[0]["actdesc"].ToString()));
            rpt.SetParameters(new ReportParameter("rptTitle", "Purchase Tracking"));
            rpt.SetParameters(new ReportParameter("mrfNo", "MRF No: " + dt.Rows[0]["refno"].ToString()));
            rpt.SetParameters(new ReportParameter("narration", this.txtReqNarr.Text.Trim()));
            rpt.SetParameters(new ReportParameter("userInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void ShowValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();

            switch (rpt)
            {
                case "Purchasetrk":
                    //this.ShowPurChaseTrk();
                    this.pnlnarration.Visible = true;
                    this.ShowPurChaseTrk01();
                    break;
                case "TransferReqtrk":
                    string mtreqno = this.Request.QueryString["mtreqno"].ToString().Trim();
                    this.pnltranNar.Visible = true;
                    if (mtreqno == "")
                    {
                        pnlB.Visible = true;
                        Getddldata();
                    }
                    else
                    {
                        pnlA.Visible = true;
                    }
                    this.ShowTReqdata();
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



        private void ShowTReqdata()
        {

            string comcod = this.GetComeCode();
            string mtreqno1 = this.Request.QueryString["mtreqno"].ToString();
            string mtreqno = mtreqno1 == "" ? ddltreq.SelectedValue.ToString() : mtreqno1;

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "TRANSTRACTRACKING", mtreqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtreqtrk.DataSource = null;
                this.gvtreqtrk.DataBind();
                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            Session["tbltranstrk"] = ds1.Tables[0];
            Session["tblpactdesctrk"] = ds1.Tables[1];

            this.txtNartrans.Text = ds1.Tables[1].Rows.Count == 0 ? "" : ds1.Tables[1].Rows[0]["mtrnar"].ToString();
            this.lblheader.Text = ds1.Tables[1].Rows.Count == 0 ? "" : "From Project: " + ((DataTable)Session["tblpactdesctrk"]).Rows[0]["fpactdesc"].ToString()
                + " To " + ((DataTable)Session["tblpactdesctrk"]).Rows[0]["tpactdesc"].ToString();
            this.LoadGrid();
        }




        private void ShowPurChaseTrk01()
        {



            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string reqno = this.Request.QueryString["reqno"].ToString();
            string recom = this.getCompanyRecom();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", reqno, recom, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            Session["tblpactdesc"] = ds1.Tables[1];

            this.txtReqNarr.Text = ds1.Tables[1].Rows.Count == 0 ? "" : ds1.Tables[1].Rows[0]["reqnar"].ToString();
            this.lblproject.Text = ds1.Tables[1].Rows.Count == 0 ? "" : "Project Name : " + (((DataTable)Session["tblpactdesc"]).Select("reqno='" + reqno + "'"))[0]["actdesc"].ToString();

            //(((DataTable)Session["tblpactdesc"]).Select("reqno='" + reqno + "'"))[0]["actdesc"].ToString();


            ///this.lblshipsupdate.Text = ds1.Tables[0].Rows[0]["shipsupdat"].ToString();
            this.LoadGrid();


        }

        private string getCompanyRecom()
        {
            string comcod = this.GetComeCode();
            string comprecom = "";
            switch (comcod)
            {
                //case "3101":
                case "1103":
                    comprecom = "comprecom";
                    break;

                case "3348": //Credence
                case "3367": //Epic

                    comprecom = "firstasecapp";
                    break;

                case "3101": //ptl
                case "3315": //Assure
                case "3316": //Assure
                    comprecom = "iscrmchecked";
                    break;

                default:
                    comprecom = "";
                    break;

            }
            return comprecom;
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



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();


            switch (rpt)
            {

                case "Purchasetrk":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();

                    }

                    break;

                case "TransferReqtrk":

                    string grpA = dt1.Rows[0]["grp"].ToString();
                    string grpdescA = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpA)
                            dt1.Rows[j]["grpdesc"] = "";

                        grpA = dt1.Rows[j]["grp"].ToString();

                    }

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


                string rpt = this.Request.QueryString["Type"].ToString().Trim();
                switch (rpt)
                {


                    case "PenBill":



                        break;

                    case "Purchasetrk":
                        DataTable dt = ((DataTable)Session["tblpurchase"]).Copy();
                        if ((dt.Rows.Count == 0)) //Problem
                            return;
                        this.gvPurstk01.DataSource = dt;
                        this.gvPurstk01.DataBind();

                        break;

                    case "TransferReqtrk":

                        DataTable dtA = ((DataTable)Session["tbltranstrk"]).Copy();
                        if ((dtA.Rows.Count == 0)) //Problem
                            return;
                        this.gvtreqtrk.DataSource = dtA;
                        this.gvtreqtrk.DataBind();

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

        protected void gvPurSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.LoadGrid();
        }




        protected void imgbtnFindReqno01_Click(object sender, EventArgs e)
        {
            Getddldata();
        }

        protected void lbtnOk0_Click(object sender, EventArgs e)
        {
            this.ShowTReqdata();
        }
    }
}