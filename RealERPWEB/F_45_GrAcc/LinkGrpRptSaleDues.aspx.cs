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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkGrpRptSaleDues : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                this.ViewSelection();
                this.NameChange();
                this.lblHeader.Text = (this.Request.QueryString["Type"].ToString() == "DuesCollect") ? "Dues Collection Statment Report" : "Dues Collection -Summary";


            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (this.Request.QueryString["comcod"].ToString());
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

            }

        }
        private void NameChange()
        {

            string type = this.Request.QueryString["Type"].ToString();
            string comcod = this.Request.QueryString["comcod"].ToString();
            switch (type)
            {


                case "DuesCollect":

                    this.dgvAccRec02.Columns[3].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
                    this.dgvAccRec02.Columns[5].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Land Cost" : "Apartment Cost";
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
                    this.printDuesCollection();
                    break;


            }


        }







        private void printDuesCollection()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
            lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





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
                        this.dgvAccRec02.Columns[17].HeaderText = "Receivable Up to " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("MMM- yyyy");
                        this.dgvAccRec02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec02.DataSource = dt;
                        this.dgvAccRec02.DataBind();
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

            string comcod = this.Request.QueryString["comcod"].ToString();
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



            //if (e.Row.RowType != DataControlRowType.DataRow)
            //    return;

            //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");

            //string uPACTCODE = this.Request.QueryString["pactcode"].ToString();
            //string uSIRCODE =Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
            //string mTRNDAT1 = this.txtDate.Text;


            //hlink1.NavigateUrl = "LinkDuesColl.aspx?Type=CustInvoice&pactcode=" + uPACTCODE + "&usircode=" + uSIRCODE + "&Date1=" + mTRNDAT1;
        }

        protected void lbtngacuname_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblAccRec"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dt = dv1.ToTable();

            string mTRNDAT1 = Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
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
            string comcod = this.GetCompCode();
            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
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
    }
}











