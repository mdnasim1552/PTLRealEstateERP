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
namespace RealERPWEB.F_15_DPayReg
{
    public partial class RptPurBillTracking : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //  RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurBilltk

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "PurBilltk") ? "Bill Tracking" : "";
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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {




                case "PurBilltk":
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

            }



        }












        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {


                case "PurBilltk":
                    // this.RptPurchaseTrack();
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


                case "PurBilltk":
                    this.ShowPurchaseBill();
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














        private void ShowPurchaseBill()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string billno = this.Request.QueryString["genno"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURBILLTRACK", billno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();

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


                case "PurBilltk":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();

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

                    case "PurBilltk":
                        this.gvPurstk01.DataSource = dt;
                        this.gvPurstk01.DataBind();

                        DataView dv = dt.DefaultView;
                        dv.RowFilter = ("grp='F'");
                        dt = dv.ToTable();

                        ((Label)this.gvPurstk01.FooterRow.FindControl("lblgvFbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        break;


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


    }
}