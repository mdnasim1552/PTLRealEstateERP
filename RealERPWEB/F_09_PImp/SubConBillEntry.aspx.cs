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
namespace RealERPWEB.F_09_PImp
{
    public partial class SubConBillEntry : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SUB CONTRACTOR BILL INFORMATION";

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();

            }
            if (this.ddlSubName.Items.Count == 0)
            {
                this.GetConTractorName();

            }



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        private void GetConTractorName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURSUBNAME", pactcode, serch1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "csirdesc";
            this.ddlSubName.DataValueField = "csircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetConTractorName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetConTractorName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.lblSubDesc.Text = this.ddlSubName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.ddlSubName.Visible = false;
                this.lblSubDesc.Visible = true;
                this.PnlPay.Visible = true;
                this.PnlDed.Visible = true;
                this.PnlNet.Visible = true;
                this.lnkbtnTotalCal.Visible = true;
                this.LoadValue();


            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.PnlPay.Visible = false;
                this.PnlDed.Visible = false;
                this.PnlNet.Visible = false;
                this.lnkbtnTotalCal.Visible = false;
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.lblSubDesc.Text = "";
            this.ddlSubName.Visible = true;
            this.lblSubDesc.Visible = false;
            this.txtSubConAmt.Text = "";
            this.txtAbvAmt.Text = "";
            this.txtCstbamt.Text = "";
            this.txtCstbbat.Text = "";
            this.txtPayOtAmt.Text = "";
            this.lbltPayAmt.Text = "";
            this.txtSecAmt.Text = "";
            this.txtPanalAmt.Text = "";
            this.txtMatamt.Text = "";
            this.txtDedOtAmt.Text = "";
            this.lbltDedAmt.Text = "";
            this.lbltPayAfDed.Text = "";
            this.lblAccAmt.Text = "";
            this.lbltNetPayAmt.Text = "";


        }




        private void LoadValue()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string SubconName = this.ddlSubName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "SUBCONBILLENTRY", PactCode, SubconName, date, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            this.txtSubConAmt.Text = Convert.ToDouble(dt.Rows[0]["tsubconbill"]).ToString("#,##0.00;(#,##0.00); ");
            if (Convert.ToDouble(dt.Rows[0]["tsubconbill"]) > 0 & Convert.ToDouble(dt.Rows[0]["abvamt"]) > 0)
                this.txtperAmt.Text = ((Convert.ToDouble(dt.Rows[0]["abvamt"]) * 100) / Convert.ToDouble(dt.Rows[0]["tsubconbill"])).ToString("#,##0;(#,##0); ");
            this.txtAbvAmt.Text = Convert.ToDouble(dt.Rows[0]["abvamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtCstbamt.Text = Convert.ToDouble(dt.Rows[0]["cstbamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtCstbbat.Text = Convert.ToDouble(dt.Rows[0]["cstbbamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtPayOtAmt.Text = Convert.ToDouble(dt.Rows[0]["othrpamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.lbltPayAmt.Text = Convert.ToDouble(dt.Rows[0]["topayamt"]).ToString("#,##0.00;(#,##0.00); ");


            this.txtSecAmt.Text = Convert.ToDouble(dt.Rows[0]["secamt"]).ToString("#,##0.00;(#,##0.00); ");
            if (Convert.ToDouble(dt.Rows[0]["tsubconbill"]) > 0 & Convert.ToDouble(dt.Rows[0]["secamt"]) > 0)
                this.txtSperAmt.Text = ((Convert.ToDouble(dt.Rows[0]["secamt"]) * 100) / Convert.ToDouble(dt.Rows[0]["tsubconbill"])).ToString("#,##0;(#,##0); ");
            this.txtPanalAmt.Text = Convert.ToDouble(dt.Rows[0]["panamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtMatamt.Text = Convert.ToDouble(dt.Rows[0]["matamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtDedOtAmt.Text = Convert.ToDouble(dt.Rows[0]["othrdamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.lbltDedAmt.Text = Convert.ToDouble(dt.Rows[0]["todedamt"]).ToString("#,##0.00;(#,##0.00); ");

            this.lbltPayAfDed.Text = Convert.ToDouble(dt.Rows[0]["payafdedamt"]).ToString("#,##0.00;-#,##0.00; ");
            this.lblAccAmt.Text = Convert.ToDouble(dt.Rows[0]["accpayamt"]).ToString("#,##0.00;-#,##0.00; ");
            this.lbltNetPayAmt.Text = Convert.ToDouble(dt.Rows[0]["netpayamt"]).ToString("#,##0.00;(#,##0.00); ");


        }





        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string SubconName = this.ddlSubName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "PRINTSUBCONBILLENTRY", PactCode, SubconName, date, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];
            ReportDocument rptsale = new RealERPRPT.R_09_PImp.rptPrintSubConBill();
            TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptpactdesc = rptsale.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            rptpactdesc.Text = "Project Name: " + this.lblProjectdesc.Text;
            TextObject rptSubdesc = rptsale.ReportDefinition.ReportObjects["SubConName"] as TextObject;
            rptSubdesc.Text = "Sub-Contractor Name: " + this.lblSubDesc.Text;
            TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsale.SetDataSource(dt);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sub - Cont.Payment";
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString() + " Con Name: " + this.ddlSubName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptsale.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptsale;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void lbtnAmt_Click(object sender, EventArgs e)
        {
            double tsubconbillamt = Convert.ToDouble(this.txtSubConAmt.Text);
            double peramt = Convert.ToDouble("0" + this.txtperAmt.Text);
            this.txtAbvAmt.Text = Convert.ToDouble(tsubconbillamt * peramt * 0.01).ToString("#,##0.00;(#,##0.00); ");



        }
        protected void lbtnSAmt_Click(object sender, EventArgs e)
        {
            double tsubconbillamt = Convert.ToDouble(this.txtSubConAmt.Text);
            double peramt = Convert.ToDouble("0" + this.txtSperAmt.Text);
            this.txtSecAmt.Text = Convert.ToDouble(tsubconbillamt * peramt * 0.01).ToString("#,##0.00;(#,##0.00); ");

        }
        protected void lnkbtnTotalCal_Click(object sender, EventArgs e)
        {
            double tsubconbill = Convert.ToDouble(this.txtSubConAmt.Text);
            double abvamt = Convert.ToDouble("0" + this.txtAbvAmt.Text);
            double cstbamt = Convert.ToDouble("0" + this.txtCstbamt.Text);
            double cstbbamt = Convert.ToDouble("0" + this.txtCstbbat.Text);
            double othrpayamt = Convert.ToDouble("0" + this.txtPayOtAmt.Text);
            double topayamt = tsubconbill + abvamt + cstbamt + cstbbamt + othrpayamt;
            this.lbltPayAmt.Text = topayamt.ToString("#,##0.00;(#,##0.00); ");
            this.txtAbvAmt.Text = abvamt.ToString("#,##0.00;(#,##0.00); ");
            this.txtCstbamt.Text = cstbamt.ToString("#,##0.00;(#,##0.00); "); ;
            this.txtCstbbat.Text = cstbbamt.ToString("#,##0.00;(#,##0.00); "); ;
            this.txtPayOtAmt.Text = othrpayamt.ToString("#,##0.00;(#,##0.00); ");

            double secamt = Convert.ToDouble("0" + this.txtSecAmt.Text);
            double panamt = Convert.ToDouble("0" + this.txtPanalAmt.Text);
            double matamt = Convert.ToDouble("0" + this.txtMatamt.Text);
            double othrdamt = Convert.ToDouble("0" + this.txtDedOtAmt.Text);
            double todedamt = secamt + panamt + matamt + othrdamt;
            this.lbltDedAmt.Text = todedamt.ToString("#,##0.00;(#,##0.00); ");
            this.txtSecAmt.Text = secamt.ToString("#,##0.00;(#,##0.00); "); ;
            this.txtPanalAmt.Text = panamt.ToString("#,##0.00;(#,##0.00); "); ;
            this.txtMatamt.Text = matamt.ToString("#,##0.00;(#,##0.00); "); ;
            this.txtDedOtAmt.Text = othrdamt.ToString("#,##0.00;(#,##0.00); ");

            double payafdedamt = topayamt - todedamt;
            this.lbltPayAfDed.Text = payafdedamt.ToString("#,##0.00;(#,##0.00); ");
            double accamt = Convert.ToDouble("0" + this.lblAccAmt.Text);
            this.lbltNetPayAmt.Text = (payafdedamt - accamt).ToString("#,##0.00;(#,##0.00); ");


        }
        protected void lnlbtnfUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string SubconName = this.ddlSubName.SelectedValue.ToString();
            string abvamt = Convert.ToDouble("0" + this.txtAbvAmt.Text).ToString();
            string cstbamt = Convert.ToDouble("0" + this.txtCstbamt.Text).ToString();
            string cstbbamt = Convert.ToDouble("0" + this.txtCstbbat.Text).ToString();
            string othrpayamt = Convert.ToDouble("0" + this.txtPayOtAmt.Text).ToString();
            string secamt = Convert.ToDouble("0" + this.txtSecAmt.Text).ToString();
            string panamt = Convert.ToDouble("0" + this.txtPanalAmt.Text).ToString();
            string matamt = Convert.ToDouble("0" + this.txtMatamt.Text).ToString();
            string othrdamt = Convert.ToDouble("0" + this.txtDedOtAmt.Text).ToString();

            bool result = BgdData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "INSERTUPSUBCONBILL", PactCode,
                                 SubconName, abvamt, cstbamt, cstbbamt, othrpayamt, secamt, panamt, matamt, othrdamt, "", "", "", "", "");

            if (result == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sub - Cont.Payment";
                string eventdesc = "Update Bill";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString() + " Con Name: " + this.ddlSubName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
    }
}

