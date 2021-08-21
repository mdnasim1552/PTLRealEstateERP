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
namespace RealERPWEB.F_35_MgtAct
{
    public partial class RptMisDailyActiviteis : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblData");
            this.lblSales.Visible = true;
            this.lblCollectionStatus.Visible = true;
            this.lblCollectionStatus.Visible = true;
            this.lblChequeInHand.Visible = true;
            this.lblRecAPayEncash.Visible = true;
            this.lblRecAPayIssue.Visible = true;
            this.lblPDCCheque.Visible = true;
            this.lblProcurement.Visible = true;
            //this.lblstkst1.Visible = true;
            this.lblStock.Visible = true;
            this.lblBankPosition.Visible = true;
            this.lblFeasibility.Visible = true;


            this.lblConstruction.Visible = true;
            //this.lblGPNPStatus.Visible = true;
            this.lblProjectStatus.Visible = true;
            //this.lblProStatus02.Visible = true;
            this.lblMonProStatus.Visible = true;
            this.lblInventorystock.Visible = true;

            this.lblMarketing.Visible = true;
            this.lblFixedAssets.Visible = true;
            this.lblFinancialst.Visible = true;
            this.lblHrMgt.Visible = true;
            string comp1 = this.GetCompCode();
            string frdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = MktData.GetTransInfo(comp1, "SP_REPORT_GROUP_MIS02", "RPTMGTACTIVITEIS", frdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDayWSale.DataSource = null;
                this.gvDayWSale.DataBind();
                return;
            }

            ViewState["tblData"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }

        private void Data_Bind()
        {

            DataTable dt = ((DataTable)ViewState["tblData"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();





            //A. Sales
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'A'");
            dt1 = dvr.ToTable();
            this.gvDayWSale.DataSource = dt1;
            this.gvDayWSale.DataBind();
            // this.FooterCalculation(dt1, "gvDayWSale");   

            //B. Collection Summary
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'B'");
            dt1 = dvr.ToTable();
            this.gvrcoll.DataSource = dt1;
            this.gvrcoll.DataBind();
            //C. Cheque In Hand

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'C'");
            dt1 = dvr.ToTable();
            this.gvchequeinhand.DataSource = dt1;
            this.gvchequeinhand.DataBind();



            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'D'");
            dt1 = dvr.ToTable();
            this.gvarecandpay.DataSource = dt1;
            this.gvarecandpay.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'E'");
            dt1 = dvr.ToTable();
            this.gvBankPosition.DataSource = dt1;
            this.gvBankPosition.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'F'");
            dt1 = dvr.ToTable();
            this.gvarecandpayis.DataSource = dt1;
            this.gvarecandpayis.DataBind();




            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'G'");
            dt1 = dvr.ToTable();
            this.gvpdc.DataSource = dt1;
            this.gvpdc.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'H'");
            dt1 = dvr.ToTable();
            this.gvprocure.DataSource = dt1;
            this.gvprocure.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'I'");
            dt1 = dvr.ToTable();
            this.gvMMPlanVsAch.DataSource = dt1;
            this.gvMMPlanVsAch.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'J'");
            dt1 = dvr.ToTable();
            this.gvFeasibility.DataSource = dt1;
            this.gvFeasibility.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'K'");
            dt1 = dvr.ToTable();
            //this.gvGPNP.DataSource = dt1;
            //this.gvGPNP.DataBind();




            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'L'");
            dt1 = dvr.ToTable();
            this.gvpstk01.DataSource = dt1;
            this.gvpstk01.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'M'");
            dt1 = dvr.ToTable();
            this.gvpstk01.Columns[5].HeaderText = "Dues Up to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM- yyyy");
            this.gvpstk01.DataSource = dt1;
            this.gvpstk01.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'N'");
            dt1 = dvr.ToTable();
            //DataTable  dt2 = dt1.Copy();
            //dvr = dt2.DefaultView;
            //dvr.RowFilter = ("deptcode = '51001'");
            //dt2 = dvr.ToTable();
            this.gvProjectStatus01.DataSource = dt1;
            this.gvProjectStatus01.DataBind();


            //dt2 = dt1.Copy();
            //dvr = dt2.DefaultView;
            //dvr.RowFilter = ("deptcode = '51002'");
            //dt2 = dvr.ToTable();
            //this.gvProjectStatus02.DataSource = dt2;
            //this.gvProjectStatus02.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'O'");
            dt1 = dvr.ToTable();
            //DataTable  dt2 = dt1.Copy();
            //dvr = dt2.DefaultView;
            //dvr.RowFilter = ("deptcode = '51001'");
            //dt2 = dvr.ToTable();
            //this.gvProjectStatus02.DataSource = dt1;
            //this.gvProjectStatus02.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'P'");
            dt1 = dvr.ToTable();
            this.gvmonprost.DataSource = dt1;
            this.gvmonprost.DataBind();



            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'Q'");
            dt1 = dvr.ToTable();
            this.gvInventory.DataSource = dt1;
            this.gvInventory.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'R'");
            dt1 = dvr.ToTable();
            this.gvcomwclients.DataSource = dt1;
            this.gvcomwclients.DataBind();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'S'");
            dt1 = dvr.ToTable();
            this.gvFxtAssets.DataSource = dt1;
            this.gvFxtAssets.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'T'");
            dt1 = dvr.ToTable();
            this.gvFinalcialst.DataSource = dt1;
            this.gvFinalcialst.DataBind();


            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'U'");
            dt1 = dvr.ToTable();
            this.gvHremp.DataSource = dt1;
            this.gvHremp.DataBind();
        }



        private DataTable HiddenSameData01(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                    dt1.Rows[j]["deptname"] = "";


                deptcode = dt1.Rows[j]["deptcode"].ToString();
            }


            return dt1;

        }

        private void LinkBound()
        {

            //for (int i = 0; i < gvprocure.Rows.Count; i++)
            //{
            //    string comcod = ((Label)gvprocure.Rows[i].FindControl("lblgvcomcodepro")).Text.Trim();

            //    LinkButton lbtn1 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvreqpro");
            //    LinkButton lbtn2 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvreqapppro");
            //    LinkButton lbtn3 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvorderpro");
            //    LinkButton lbtn4 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvourchasepro");
            //    LinkButton lbtn5 = (LinkButton)gvprocure.Rows[i].FindControl("hlnkgvbillpro");
            //    if (lbtn1 != null)
            //    {
            //        if (lbtn1.Text.Trim().Length > 0)
            //            lbtn1.CommandArgument = comcod;
            //    }
            //    if (lbtn2 != null)
            //    {
            //        if (lbtn2.Text.Trim().Length > 0)
            //            lbtn2.CommandArgument = comcod;
            //    }

            //    if (lbtn3 != null)
            //    {
            //        if (lbtn3.Text.Trim().Length > 0)
            //            lbtn3.CommandArgument = comcod;
            //    }
            //    if (lbtn4 != null)
            //    {
            //        if (lbtn4.Text.Trim().Length > 0)
            //            lbtn4.CommandArgument = comcod;
            //    }

            //    if (lbtn5 != null)
            //    {
            //        if (lbtn5.Text.Trim().Length > 0)
            //            lbtn5.CommandArgument = comcod;
            //    }


            //}

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comcod = dt1.Rows[0]["comcod"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();
            string pgrp = dt1.Rows[0]["pgrp"].ToString();
            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["comcod"].ToString() == comcod) && (dt1.Rows[j]["grp"].ToString() == grp))
                    dt1.Rows[j]["compname"] = "";
                if (dt1.Rows[j]["pgrp"].ToString() == pgrp)
                    dt1.Rows[j]["pgrpdesc"] = "";

                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                    dt1.Rows[j]["deptname"] = "";

                comcod = dt1.Rows[j]["comcod"].ToString();
                grp = dt1.Rows[j]["grp"].ToString();
                pgrp = dt1.Rows[j]["pgrp"].ToString();
                deptcode = dt1.Rows[j]["deptcode"].ToString();
            }


            return dt1;

        }

        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();
            DataTable dt2 = new DataTable();



            //DataTable dt = (DataTable)Session["tblData"];




            switch (GvName)
            {
                case "gvDayWSale":
                    dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("pactcode='AAAAAAAAAAAA'");
                    dt2 = dv.ToTable();
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tuamt)", "")) ?
                            0 : dt2.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(suamt)", "")) ?
                                    0 : dt2.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;

            }


        }


        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label comname = (Label)e.Row.FindControl("lkgvcomnamesale");
                Label capacity = (Label)e.Row.FindControl("lgvcapacityotvachsal");
                Label masbgd = (Label)e.Row.FindControl("lgvmasbgdotvachsal");
                Label bep = (Label)e.Row.FindControl("lgvbepotvachsal");

                HyperLink tsaleamt = (HyperLink)e.Row.FindControl("hlnkgvtsaleamt");
                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraph");
                HyperLink salamt = (HyperLink)e.Row.FindControl("hlnkgvDSAmt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                double ttargetamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvtsaleamt")).Text);
                string ttargetamt1 = ((ttargetamt == 0) ? 0 : ttargetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                double targetamt = Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatosaleamt")).Text);
                string targetamt1 = ((targetamt == 0) ? 0 : targetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                double acsalamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvDSAmt")).Text);

                string acsalamt1 = ((acsalamt == 0) ? 0 : acsalamt / 1000000).ToString("#,##0.00;(#,##0.00);");

                tsaleamt.Style.Add("color", "blue");
                graph.Style.Add("color", "blue");
                tsaleamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RptDayWSale&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                graph.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + code + "&ttargetamt=" + ttargetamt1 + "&targetamt=" + targetamt1 + "&acsalamt=" + acsalamt1;


            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblData"];
            ReportDocument rrs1 = new RealERPRPT.R_35_MgtAct.RptMisMgtActivities();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "(From " + this.txtDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            //TextObject txtduesupto = rrs1.ReportDefinition.ReportObjects["txtduesupto"] as TextObject;
            //txtduesupto.Text = "Dues Up to " + Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yyyy");
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rrs1.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rrs1;
            lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        protected void gvrcoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label comname = (Label)e.Row.FindControl("lkgvCompanyrc");
                Label capacity = (Label)e.Row.FindControl("lgvcapacityotvachcl");
                Label masbgd = (Label)e.Row.FindControl("lgvmasbgdotvachcl");
                Label bep = (Label)e.Row.FindControl("lgvbepotvachcl");

                HyperLink tcollamt = (HyperLink)e.Row.FindControl("hlnkgvtcollamt");
                Label tatocollamt = (Label)e.Row.FindControl("lgvtatocollamt");
                HyperLink achamt = (HyperLink)e.Row.FindControl("hlnkgvaccollAmt");

                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraphcoll");
                Label perotcoll = (Label)e.Row.FindControl("lgvperotcoll");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {



                    tcollamt.Font.Bold = true;
                    tatocollamt.Font.Bold = true;
                    achamt.Font.Bold = true;
                    perotcoll.Font.Bold = true;

                }
                else
                {

                    double ttargetamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvtcollamt")).Text);
                    string ttargetamt1 = ((ttargetamt == 0) ? 0 : ttargetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                    double targetamt = Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatocollamt")).Text);
                    string targetamt1 = ((targetamt == 0) ? 0 : targetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                    double accollamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvaccollAmt")).Text);
                    string accollamt1 = ((accollamt == 0) ? 0 : accollamt / 1000000).ToString("#,##0.00;(#,##0.00);");


                    tcollamt.Style.Add("color", "blue");
                    achamt.Style.Add("color", "blue");
                    graph.Style.Add("color", "blue");
                    tcollamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Collection&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    achamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=CollSummary&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    graph.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + code + "&ttargetamt=" + ttargetamt1 + "&targetamt=" + targetamt1 + "&acsalamt=" + accollamt1;



                }

            }
        }
        protected void gvchequeinhand_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Company = (Label)e.Row.FindControl("lblgvCompanycih");
                HyperLink amount = (HyperLink)e.Row.FindControl("hlnkgvamtcin");
                Label inhrchqcih = (Label)e.Row.FindControl("lgvinhrchqcih");
                Label inhfchqcih = (Label)e.Row.FindControl("lgvinhfchqcih");
                Label inhpchqcih = (Label)e.Row.FindControl("lgvinhpchqcih");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Company.Font.Bold = true;
                    amount.Font.Bold = true;
                    inhrchqcih.Font.Bold = true;
                    inhfchqcih.Font.Bold = true;
                    inhpchqcih.Font.Bold = true;
                    Company.Style.Add("text-align", "right");
                }

                else
                {

                    DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                        return;

                    }
                    amount.Style.Add("color", "blue");
                    DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);
                    amount.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=ChequeInHand&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(txtopndate).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }

            }
        }
        protected void gvarecandpay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label recam = (Label)e.Row.FindControl("lgvrecam");
                Label PayAmt = (Label)e.Row.FindControl("lgvpayam");
                HyperLink Balamt = (HyperLink)e.Row.FindControl("hlnkgvbalpam");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                {
                    return;
                }

                if (comcod == "AAAA")
                {


                    recam.Font.Bold = true;
                    PayAmt.Font.Bold = true;
                    Balamt.Font.Bold = true;

                }
                else
                {
                    Balamt.Style.Add("color", "blue");
                    Balamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RecPay&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }



            }

        }
        protected void gvarecandpayis_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label recam = (Label)e.Row.FindControl("lgvrecamis");
                Label PayAmt = (Label)e.Row.FindControl("lgvpayamis");
                HyperLink Balamt = (HyperLink)e.Row.FindControl("hlnkgvbalamis");


                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                {
                    return;
                }

                if (comcod == "AAAA")
                {


                    recam.Font.Bold = true;
                    PayAmt.Font.Bold = true;
                    Balamt.Font.Bold = true;

                }
                else
                {
                    Balamt.Style.Add("color", "blue");
                    Balamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=IssuedVsCollect&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }



            }

        }
        protected void gvpdc_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HLgvDescpaysum = (HyperLink)e.Row.FindControl("HLgvDescpaysum");
                Label toamtpdc = (Label)e.Row.FindControl("lgvtoamtpdc");
                Label dueam = (Label)e.Row.FindControl("lgvdueam");
                Label pdcam = (Label)e.Row.FindControl("lgvpdc");



                //  string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pgrp")).ToString();

                if (grp == "")
                {
                    return;
                }
                if (grp == "9")
                {

                    HLgvDescpaysum.Font.Bold = true;
                    toamtpdc.Font.Bold = true;
                    dueam.Font.Bold = true;
                    pdcam.Font.Bold = true;
                    HLgvDescpaysum.Style.Add("text-align", "right");
                    HLgvDescpaysum.Style.Add("color", "blue");
                    string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                    HLgvDescpaysum.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PDCSummary&comcod=" + comcod + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy"); ;

                }

                if (grp == "A")
                {

                    HLgvDescpaysum.Font.Bold = true;
                    toamtpdc.Font.Bold = true;
                    dueam.Font.Bold = true;
                    pdcam.Font.Bold = true;
                    HLgvDescpaysum.Style.Add("text-align", "right");
                }




            }
        }

        protected void gvHremp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("lnkgvcomname");
                Label toemp = (Label)e.Row.FindControl("lgvtoemp");
                Label netsalary = (Label)e.Row.FindControl("lgvnetsalary");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hrcomcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    toemp.Font.Bold = true;
                    netsalary.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                    comname.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkRptMgtInterface.aspx?comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }





            }


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //    Label comname = (Label)e.Row.FindControl("lblgvcomname");
            //    Label toemp = (Label)e.Row.FindControl("lgvtoemp");
            //    Label netsalary = (Label)e.Row.FindControl("lgvnetsalary");



            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "compcod")).ToString();


            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (ASTUtility.Right(code, 4) == "AAAA")
            //    {


            //        comname.Font.Bold = true;
            //        toemp.Font.Bold = true;
            //        netsalary.Font.Bold = true;
            //        comname.Style.Add("text-align", "right");
            //    }





            //}

        }
        protected void gvBankPosition_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink bankposition = (HyperLink)e.Row.FindControl("hlnkgvbankposition");
                Label BankBal = (Label)e.Row.FindControl("lgvbankbalbp");
                Label banklia = (Label)e.Row.FindControl("lblgvbankliabp");
                Label netcbolia = (Label)e.Row.FindControl("lblgvnetcbolia");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                    return;

                if (comcod == "AAAA")
                {
                    bankposition.Font.Bold = true;
                    BankBal.Font.Bold = true;
                    banklia.Font.Bold = true;
                    netcbolia.Font.Bold = true;
                    bankposition.Style.Add("text-align", "right");
                }

                else
                {
                    bankposition.Style.Add("color", "blue");
                    bankposition.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=BankPosition&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }



            }





        }

        protected void gvpstk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomname");
                Label stkamt = (Label)e.Row.FindControl("lgvtstkamt");
                Label unsoldamt = (Label)e.Row.FindControl("lgvunsoldamt");

                Label soldamt = (Label)e.Row.FindControl("lgvsoldamt");

                Label toramt = (Label)e.Row.FindControl("lgvtoramt");
                Label atodues = (Label)e.Row.FindControl("lgvatodues");
                // Label totalduesal = (Label)e.Row.FindControl("lgvtotalduesal");
                Label toduesal = (Label)e.Row.FindControl("lgvtoduesal");
                Label ptoduesal = (Label)e.Row.FindControl("lgvptoduesal");
                Label curduesal = (Label)e.Row.FindControl("lgvcurduesal");
                Label delchargeal = (Label)e.Row.FindControl("lgvdelchargeal");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    stkamt.Font.Bold = true;
                    unsoldamt.Font.Bold = true;
                    soldamt.Font.Bold = true;
                    toramt.Font.Bold = true;
                    atodues.Font.Bold = true;
                    //totalduesal.Font.Bold = true;
                    toduesal.Font.Bold = true;
                    ptoduesal.Font.Bold = true;
                    curduesal.Font.Bold = true;
                    delchargeal.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
                else
                {
                    comname.Style.Add("color", "blue");
                    string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=SoldUnsold&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }





            }


        }

        protected void gvProjectStatus01_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Compname = (HyperLink)e.Row.FindControl("hlnkgvcomnameps1");
                Label Sales = (Label)e.Row.FindControl("lgvSales");

                Label toamt = (Label)e.Row.FindControl("lgvtoamtps1");
                Label liaamt = (Label)e.Row.FindControl("lgvliaamt");
                Label netexpenses = (Label)e.Row.FindControl("lgvnetexpenses01");
                Label netloantamt = (Label)e.Row.FindControl("lgvnetloantamt");
                HyperLink prorptwqty = (HyperLink)e.Row.FindControl("hlnkgvprorptwqty");
                HyperLink probgdvsexp = (HyperLink)e.Row.FindControl("hlnkgvprobgdvsexp");
                HyperLink protrdaywise = (HyperLink)e.Row.FindControl("hlnkgvprotrdaywise");




                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {

                    Compname.Font.Bold = true;
                    Sales.Font.Bold = true;

                    toamt.Font.Bold = true;
                    liaamt.Font.Bold = true;
                    netexpenses.Font.Bold = true;
                    netloantamt.Font.Bold = true;
                    Compname.Style.Add("text-align", "right");
                }

                else
                {

                    Compname.Style.Add("color", "blue");
                    prorptwqty.Style.Add("color", "blue");
                    probgdvsexp.Style.Add("color", "blue");
                    protrdaywise.Style.Add("color", "blue");
                    Compname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PrjStatus&comcod=" + code + "&date=" + this.txttodate.Text.Trim();
                    prorptwqty.NavigateUrl = "~/F_45_GrAcc/LinkGrpProjReport02.aspx?comcod=" + code + "&date=" + this.txttodate.Text.Trim();
                    probgdvsexp.NavigateUrl = "~/F_45_GrAcc/LinkFinncialReports.aspx?Type=BE&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    protrdaywise.NavigateUrl = "~/F_45_GrAcc/RptGrpAccDailyTransaction.aspx?Type=GrpProTrans&comcod=" + code;


                    //prorptataglance.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PrjStatus&comcod=" + code + "&date=" + this.txttodate.Text.Trim();
                    //proreport.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PrjStatus&comcod=" + code + "&date=" + this.txttodate.Text.Trim();



                }

            }
        }

        protected void gvProjectStatus02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label ExpAmt = (Label)e.Row.FindControl("lgvExpAmt");
                Label PAdvAmt = (Label)e.Row.FindControl("lgvPAdvAmt");
                Label LCNFAmt = (Label)e.Row.FindControl("lgvLCNFAmt");
                Label Ovmt = (Label)e.Row.FindControl("lgvOvmt");
                Label IAmt = (Label)e.Row.FindControl("lgvIAmt");
                Label texpamt = (Label)e.Row.FindControl("lgvtexamt");
                Label liaamt = (Label)e.Row.FindControl("lgvliaamt");
                Label netexpenses = (Label)e.Row.FindControl("lgvnetexpenses02");





                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    ExpAmt.Font.Bold = true;
                    PAdvAmt.Font.Bold = true;
                    LCNFAmt.Font.Bold = true;
                    Ovmt.Font.Bold = true;
                    IAmt.Font.Bold = true;
                    texpamt.Font.Bold = true;
                    liaamt.Font.Bold = true;
                    netexpenses.Font.Bold = true;

                }



            }

        }



        protected void gvSalevsColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomnamesvscoll");
                Label tsales = (Label)e.Row.FindControl("lgvtsales");
                Label tosales = (Label)e.Row.FindControl("lgvtosales");
                Label acsales = (Label)e.Row.FindControl("lgvacsales");
                Label peronsal = (Label)e.Row.FindControl("lgvperonsal");
                Label tcoll = (Label)e.Row.FindControl("lgvtcoll");
                Label tocoll = (Label)e.Row.FindControl("lgvtocoll");
                Label accoll = (Label)e.Row.FindControl("lgvaccoll");
                Label peroncoll = (Label)e.Row.FindControl("lgvperoncoll");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    tsales.Font.Bold = true;
                    tosales.Font.Bold = true;
                    acsales.Font.Bold = true;

                    peronsal.Font.Bold = true;
                    tcoll.Font.Bold = true;
                    tocoll.Font.Bold = true;
                    accoll.Font.Bold = true;
                    peroncoll.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
                else
                {

                    comname.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }





            }

        }

        protected void gvFeasibility_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink Feadesc = (HyperLink)e.Row.FindControl("hlnkgvFeadesc");
                //Label revnue = (Label)e.Row.FindControl("lgvfearevnue");
                //Label cost = (Label)e.Row.FindControl("lblgvfeacost");
                //Label margin = (Label)e.Row.FindControl("lblgvfeamargin");
                //Label percnt = (Label)e.Row.FindControl("lblgvfeapercnt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                    return;

                if (code == "51003" && comcod != "AAAA")
                {
                    Feadesc.Style.Add("color", "blue");

                    Feadesc.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=FeaAllProject&comcod=" + comcod + "&date=" + this.txttodate.Text.Trim();

                }



            }

        }


        //protected void gvGPNP_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {



        //        HyperLink Sales = (HyperLink)e.Row.FindControl("hlnkgvSalesgn");
        //        Label procost = (Label)e.Row.FindControl("lgvprocostgn");
        //        Label gp = (Label)e.Row.FindControl("lgvgp");
        //        Label opcost = (Label)e.Row.FindControl("lgvopcostgn");
        //        Label np = (Label)e.Row.FindControl("lgvnp");
        //        Label pergponcost = (Label)e.Row.FindControl("lgvpergponcost");
        //        Label pernponcost = (Label)e.Row.FindControl("lgvpernponcost");




        //        string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


        //        if (code == "")
        //        {
        //            return;
        //        }
        //        if (code == "AAAA")
        //        {


        //            Sales.Font.Bold = true;
        //            procost.Font.Bold = true;
        //            gp.Font.Bold = true;
        //            opcost.Font.Bold = true;
        //            np.Font.Bold = true;
        //            pergponcost.Font.Bold = true;
        //            pernponcost.Font.Bold = true;
        //           // Compname.Style.Add("text-align", "right");
        //        }

        //        else
        //        {

        //            Sales.Style.Add("color", "blue");
        //            Sales.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=GPNPALLPRO&comcod=" + code + "&date=" + this.txttodate.Text.Trim();



        //        }

        //    }

        //}
        protected void gvMMPlanVsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomnamecons");
                Label masterplan = (Label)e.Row.FindControl("lgvmasterplan");
                Label monplan = (Label)e.Row.FindControl("lgvmonplan");
                Label ExecutionpAC = (Label)e.Row.FindControl("lgvExecutionpAC");
                Label PerMasPlan = (Label)e.Row.FindControl("lgvPerMasPlan");
                Label PerMonthPlan = (Label)e.Row.FindControl("lgvPerMonthPlan");
                HyperLink flrwiseprogress = (HyperLink)e.Row.FindControl("hlnkgvflrwiseprogress");
                HyperLink sysgentarget = (HyperLink)e.Row.FindControl("hlnkgvsysgentarget");
                HyperLink ineffect = (HyperLink)e.Row.FindControl("hlnkgvineffect");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    masterplan.Font.Bold = true;
                    monplan.Font.Bold = true;
                    ExecutionpAC.Font.Bold = true;
                    PerMasPlan.Font.Bold = true;
                    PerMonthPlan.Font.Bold = true;


                    comname.Style.Add("text-align", "right");
                }
                else
                {

                    comname.Style.Add("color", "blue");
                    flrwiseprogress.Style.Add("color", "blue");
                    sysgentarget.Style.Add("color", "blue");
                    ineffect.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MasPVsMonPVsExAllPro&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    flrwiseprogress.NavigateUrl = "~/F_45_GrAcc/LinkConstruProgress.aspx?comcod=" + code + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    sysgentarget.NavigateUrl = "~/F_45_GrAcc/LinkGrpSysGenTarget.aspx?Type=SymGenTar&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                    ineffect.NavigateUrl = "~/F_45_GrAcc/LinkGrpInflaEffect.aspx?Type=RemainingCost&comcod=" + code + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }





            }

        }
        protected void gvcomwclients_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label Description = (Label)e.Row.FindControl("lblgvDescription");
                Label clnumber = (Label)e.Row.FindControl("lgclnumber");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "teamcode")).ToString();


                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    Description.Font.Bold = true;
                    clnumber.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }





            }



        }
        protected void gvprocure_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlnkgvreqpro = (HyperLink)e.Row.FindControl("hlnkgvreqpro");

                HyperLink hlnkgvourchasepro = (HyperLink)e.Row.FindControl("hlnkgvourchasepro");
                HyperLink hlnkgvbillpro = (HyperLink)e.Row.FindControl("hlnkgvbillpro");
                HyperLink purhmwisepro = (HyperLink)e.Row.FindControl("hlnkgvpurhmwisepro");
                HyperLink purhswisepro = (HyperLink)e.Row.FindControl("hlnkgvpurhswisepro");
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();




                if (comcod != "AAAA")
                {
                    hlnkgvreqpro.Style.Add("color", "blue");
                    hlnkgvourchasepro.Style.Add("color", "blue");
                    hlnkgvbillpro.Style.Add("color", "blue");
                    purhmwisepro.Style.Add("color", "blue");
                    purhswisepro.Style.Add("color", "blue");




                    hlnkgvreqpro.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=PendingStatus&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    //hlnkgvreqapppro.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=ReqAppStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    //hlnkgvorderpro.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=WorkOrder&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    hlnkgvourchasepro.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=Purchase&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                    hlnkgvbillpro.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=ConBill&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    purhmwisepro.NavigateUrl = "~/F_45_GrAcc/LinkGrpMatPurHistory.aspx?Type=PurHisMatWise&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    purhswisepro.NavigateUrl = "~/F_45_GrAcc/LinkGrpMatPurHistory.aspx?Type=PurHisSupWise&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }








            }
        }
        protected void gvarecandpay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lnkgvreqpro_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string recpcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //DataTable dt = (DataTable)Session["recandpay"];
            //DataView dv1 = dt.DefaultView;
            //dv1.RowFilter = "recpcode like('" + recpcode + "')";
            //dt = dv1.ToTable();

            //string mCOMCOD = comcod;
            //string mTRNDAT1 = this.txtfromdate.Text;
            //string mTRNDAT2 = this.txttodate.Text;
            //string mACTCODE = dt.Rows[0]["recpcode"].ToString();
            //string mACTDESC = dt.Rows[0]["recpdesc"].ToString();
            //string lebel2 = dt.Rows[0]["rleb2"].ToString();
            //if (mACTCODE == "")
            //{
            //    return;
            //}

            /////---------------------------------//// 
            //if (ASTUtility.Left(mACTCODE, 1) == "4")
            //{
            //    lbljavascript.Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
            //                    mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            //}
            //else if (lebel2 == "")
            //{
            //    lbljavascript.Text = @"<script>window.open('AccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
            //                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            //}
            //else
            //{
            //    lbljavascript.Text = @"<script>window.open('AccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
            //                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            //}

        }
        protected void lnkgvreqapppro_Click(object sender, EventArgs e)
        {
            string comcod = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            lbljavascript.Text = @"<script>window.open('~/F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=ReqAppStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + "', target='_blank');</script>";


        }
        protected void lnkgvorderpro_Click(object sender, EventArgs e)
        {

        }
        protected void lnkgvourchasepro_Click(object sender, EventArgs e)
        {

        }
        protected void hlnkgvbillpro_Click(object sender, EventArgs e)
        {

        }






        protected void gvmonprost_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Compname = (HyperLink)e.Row.FindControl("hlnkgvcomnamemps");
                Label Cost = (Label)e.Row.FindControl("lgvcostmps");
                Label collamt = (Label)e.Row.FindControl("lgvcollamtmps");
                Label netposition = (Label)e.Row.FindControl("lgvnetpositionmps");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Compname.Font.Bold = true;
                    Cost.Font.Bold = true;
                    collamt.Font.Bold = true;
                    netposition.Font.Bold = true;

                    Compname.Style.Add("text-align", "right");
                }

                else
                {

                    Compname.Style.Add("color", "blue");
                    Compname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MProStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");



                }

            }
        }


        protected void gvFxtAssets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Company = (HyperLink)e.Row.FindControl("hlnkgvCompanyfxt");
                Label closamt = (Label)e.Row.FindControl("lgvclosamtfxt");
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Company.Font.Bold = true;
                    closamt.Font.Bold = true;
                    Company.Style.Add("text-align", "right");
                }

                else
                {

                    Company.Style.Add("color", "blue");
                    Company.NavigateUrl = "~/F_45_GrAcc/LinkAccMultiReport.aspx?rpttype=FxtAsset&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");



                }



            }
        }

        protected void gvInventory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlnkgvissuebasisinv = (HyperLink)e.Row.FindControl("hlnkgvissuebasisinv");
                HyperLink hlnkgvprobasisinv = (HyperLink)e.Row.FindControl("hlnkgvprobasisinv");
                HyperLink hlnkgvmatconinv = (HyperLink)e.Row.FindControl("hlnkgvmatconinv");
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                hlnkgvissuebasisinv.Style.Add("color", "blue");
                hlnkgvprobasisinv.Style.Add("color", "blue");
                hlnkgvmatconinv.Style.Add("color", "blue");
                hlnkgvissuebasisinv.NavigateUrl = "~/F_45_GrAcc/LinkGrpPrurVarAna.aspx?Type=IssueBasis&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                hlnkgvprobasisinv.NavigateUrl = "~/F_45_GrAcc/LinkGrpPrurVarAna.aspx?Type=StkBasis&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                hlnkgvmatconinv.NavigateUrl = "~/F_45_GrAcc/LinkGrpIndResourceConsum.aspx?comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            }
        }

        protected void gvFinalcialst_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink incomest = (HyperLink)e.Row.FindControl("hlnkgvincomest");
                HyperLink balancesheet = (HyperLink)e.Row.FindControl("hlnkgvbalancesheet");
                HyperLink inaoutflow = (HyperLink)e.Row.FindControl("hlnkgvinaoutflow");
                HyperLink realpsum = (HyperLink)e.Row.FindControl("hlnkgvrealpsum");
                HyperLink inplanfin = (HyperLink)e.Row.FindControl("hlnkgvinplanfin");
                HyperLink pstaaglance = (HyperLink)e.Row.FindControl("hlnkgvpstaaglance");
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (comcod == "")
                    return;
                incomest.Style.Add("color", "blue");
                balancesheet.Style.Add("color", "blue");
                inaoutflow.Style.Add("color", "blue");
                realpsum.Style.Add("color", "blue");
                inplanfin.Style.Add("color", "blue");
                pstaaglance.Style.Add("color", "blue");
                incomest.NavigateUrl = "~/F_45_GrAcc/LinkFinncialReports.aspx?Type=IS&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                balancesheet.NavigateUrl = "~/F_45_GrAcc/LinkFinncialReports.aspx?Type=BS&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                inaoutflow.NavigateUrl = "~/F_45_GrAcc/LinkGrpRealInOutFlow.aspx?Type=RealFlow&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).AddMonths(-11).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                realpsum.NavigateUrl = "~/F_45_GrAcc/LinkGrpRealPaySummary.aspx?Type=MonPaymentSumm&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).AddMonths(-11).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                inplanfin.NavigateUrl = "~/F_45_GrAcc/LinkGrpInvestmentPlan.aspx?Type=ColVsExpenses&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                pstaaglance.NavigateUrl = "~/F_45_GrAcc/LinkGrpProjectSummary.aspx?comcod=" + comcod;



            }



        }
    }
}
