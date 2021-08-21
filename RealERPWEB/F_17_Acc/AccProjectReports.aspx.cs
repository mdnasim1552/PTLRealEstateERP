using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{


    public partial class AccProjectReports : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Final Accounts Reports View/Print Screen
            if (!IsPostBack)
            {
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Final Accounts Reports";
                this.Master.Page.Title = "Final Accounts Reports";
                this.GetProjectStatus();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            this.RptProjectReoprt();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report: " + "";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void GetProjectStatus()
        {
            Session.Remove("tblPS");
            DataSet ds2 = this.GetDataForProjectReport();
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                //this.lblmsg.Text = "There is no resource in this accounts.";
                //this.lblmsg.ForeColor = System.Drawing.Color.Blue;
                return;
            }
            Session["tblPS"] = ds2.Tables[0];
            DataTable dtr = (DataTable)Session["tblPS"];
            //DataView dvr = new DataView();
            //dvr = dtr.DefaultView;
            //dvr.RowFilter = ("grp1 = 'g01'");
            this.dgvPS.DataSource = dtr;
            this.dgvPS.DataBind();

            Session["Report1"] = dgvPS;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ((HyperLink)this.dgvPS.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }


            this.FooterCalculation(dtr, "dgvPS");
        }
        private void FooterCalculation(DataTable dt, string GvName)
        {
            switch (GvName)
            {
                case "dgvPS":
                    if (dt.Rows.Count == 0)
                        return;
                    string mRptGroup = Convert.ToString(this.ddlReportLevelDetails.SelectedIndex);
                    mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
                    if (mRptGroup == "12")
                    {

                        DataView dv = dt.DefaultView;
                        dv.RowFilter = ("subcode1  like '%000%'");
                        dt = dv.ToTable();

                    }

                    ((Label)this.dgvPS.FooterRow.FindControl("lblfopamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");

                    ((Label)this.dgvPS.FooterRow.FindControl("lblgvfdramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dramt)", "")) ?
                      0.00 : dt.Compute("Sum(dramt)", ""))).ToString("#,##0;(#,##0); - ");
                    ((Label)this.dgvPS.FooterRow.FindControl("lblgvfcramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cramt)", "")) ?
                      0.00 : dt.Compute("Sum(cramt)", ""))).ToString("#,##0;(#,##0); - ");

                    //  ((Label)this.dgvPS.FooterRow.FindControl("lblfcuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                    // 0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0); - ");
                    ((Label)this.dgvPS.FooterRow.FindControl("lblfclDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closdramt)", "")) ?
                     0.00 : dt.Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); - ");
                    ((Label)this.dgvPS.FooterRow.FindControl("lblfclCramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closcramt)", "")) ?
                      0.00 : dt.Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); - ");

                    string dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closdramt)", "")) ? 0.00 : dt.Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); 0");
                    string cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closcramt)", "")) ? 0.00 : dt.Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); 0");

                    ((Label)this.dgvPS.FooterRow.FindControl("lblfclBalAmt")).Text = (Convert.ToDouble(dramt) - Convert.ToDouble(cramt)).ToString("#,##0;(#,##0); 0");

                    break;

            }


        }
        protected void RptProjectReoprt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["BvsE"];

            DataSet ds2 = GetDataForProjectReport();
            if (ds2 == null)
                return;

            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccProjectReport1();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = "Individual Project Cost-Consolidated Report";

            DataTable dt = ds2.Tables[0];
            string mRptGroup = Convert.ToString(this.ddlReportLevelDetails.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            if (mRptGroup == "12")
            {

                DataView dv = ds2.Tables[0].DefaultView;
                dv.RowFilter = ("subcode1  like '%000%'");
                dt = dv.ToTable();

            }







            //string dramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closdramt)", "")) ? 0.00 : ds2.Tables[0].Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); - ");
            //string cramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closcramt)", "")) ? 0.00 : ds2.Tables[0].Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); - ");

            //TextObject txtNetAmt = rptstk.ReportDefinition.ReportObjects["txtNetAmt"] as TextObject;
            //txtNetAmt.Text = ((Convert.ToDouble(dramt) - Convert.ToDouble(cramt));// == "0") ? "" : (Convert.ToDouble(dramt) - Convert.ToDouble(cramt)).ToString();

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = this.lblDate.Text;
            TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            txtlevel.Text = this.ddlReportLevelDetails.SelectedValue.ToString().Trim();
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.lblActDesc.Text;


            TextObject txtopnamt = rptstk.ReportDefinition.ReportObjects["txtopnamt"] as TextObject;
            txtopnamt.Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ? 0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");
            TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            txtdramt.Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dramt)", "")) ? 0.00 : dt.Compute("Sum(dramt)", ""))).ToString("#,##0;(#,##0); - ");
            TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            txtcramt.Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cramt)", "")) ? 0.00 : dt.Compute("Sum(cramt)", ""))).ToString("#,##0;(#,##0); - ");
            TextObject txtclsdramt = rptstk.ReportDefinition.ReportObjects["txtclsdramt"] as TextObject;
            txtclsdramt.Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closdramt)", "")) ? 0.00 : dt.Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); - ");
            TextObject txtclscramt = rptstk.ReportDefinition.ReportObjects["txtclscramt"] as TextObject;
            txtclscramt.Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closcramt)", "")) ? 0.00 : dt.Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); - ");


            string dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closdramt)", "")) ? 0.00 : dt.Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); 0");
            string cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closcramt)", "")) ? 0.00 : dt.Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); 0");

            TextObject txtclsbalamt = rptstk.ReportDefinition.ReportObjects["txtclsbalamt"] as TextObject;
            txtclsbalamt.Text = (Convert.ToDouble(dramt) - Convert.ToDouble(cramt)).ToString("#,##0;(#,##0); 0");


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(ds2.Tables[0]);

            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private DataSet GetDataForProjectReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = Request.QueryString["Date1"].ToString().Trim();
            string date2 = Request.QueryString["Date2"].ToString().Trim();
            string TopHead = "dfdsf";
            string actcode = Request.QueryString["actcode"].ToString().Trim();
            string mRptGroup = this.ddlReportLevelDetails.SelectedValue.ToString();
            this.lblDate.Text = (date1 == date2) ? ("As On Date: " + date1) : ("From: " + date1 + " To:  " + date2);
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "PROJECT_REPORT_LEVEL", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            if (ds2 == null)
                return ds2;
            this.lblActDesc.Text = ds2.Tables[1].Rows[0]["actdesc"].ToString();
            return ds2;
        }




        protected void dgvPS_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;



            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            Label description = (Label)e.Row.FindControl("lblgvdescryption");
            Label opnam = (Label)e.Row.FindControl("lblgvOpnamt1");
            Label lblgvCuam = (Label)e.Row.FindControl("lblgvCuam");
            Label lblgvClrDrAmt = (Label)e.Row.FindControl("lblgvClrDrAmt");
            Label lblgvClCram = (Label)e.Row.FindControl("lblgvClCram");
            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode1")).ToString();
            string date1 = Request.QueryString["Date1"].ToString().Trim();
            string date2 = Request.QueryString["Date2"].ToString().Trim();

            string actcode = Request.QueryString["actcode"].ToString().Trim();

            if (code == "")
            {
                return;
            }



            if (ASTUtility.Right(code, 3) == "000")
            {
                //description.Font.Bold = true;

                opnam.Font.Bold = true;
                //lblgvCuam.Font.Bold = true;
                lblgvClrDrAmt.Font.Bold = true;
                lblgvClCram.Font.Bold = true;
                hlink1.Attributes["style"] = "color:black; font:bold;";




            }


            if (ASTUtility.Right(code, 3) != "000")
            {

                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledger&comcod=" + comcod + "&actcode=" + actcode + "&rescode=" + code +
              "&Date1=" + date1 + "&Date2=" + date2;




            }


            //hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE +
            //    "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&resdesc=" + mREESDESC;







        }


        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.GetProjectStatus();

        }
    }
}
