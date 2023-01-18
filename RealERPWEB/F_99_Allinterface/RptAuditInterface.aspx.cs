using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptAuditInterface : System.Web.UI.Page
    {
        // public static string recvno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus = "";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Audit Interface";//

                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtdate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.txFdate.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                this.gridVisibility();
                this.lbtnOk_Click(null, null);
                //this.txtIme_TextChanged(null, null);
                //this.RadioButtonList1_SelectedIndexChanged(null, null);

            }
        }

        private void gridVisibility()
        {

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "3338":
                //case "3336":

                // break;

                default:
                    //this.gvReqInfo.Columns[5].Visible = false;
                    //this.gvReqInfo.Columns[8].Visible = false;
                    //this.gvPenApproval.Columns[4].Visible = false;
                    break;


            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3333":
                    this.Timer1.Interval = 10000; // 10 Secnod
                    break;
                default:
                    this.Timer1.Interval = 3600000; //30 Second
                    break;
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "5":
                    this.PrintToDaysApproval();
                    break;
                default:
                    break;
            }
        }

        private void PrintToDaysApproval()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptToDaysApproval();
            TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompanyName.Text = comnam;

            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = todate;

            DataTable dt = (DataTable)Session["tbldate1"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "faprvdat = '" + todate + "'";
            dt = dv.ToTable();



            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            lbtnOk_Click(null, null);
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.reqStatus();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    // status

                    this.pnlReqInfo.Visible = true;
                    this.pnlPendapp.Visible = false;
                    this.pnlfrec.Visible = false;
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #D0DECA; display:block;";
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1":
                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = true;
                    this.pnlfrec.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;

                case "2":// First Approval/ Checked

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.pnlfrec.Visible = true;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[2].Attributes["style"] = "background: #D0DECA; display:block;";
                    // this.RadioButtonList1.Items[2].Attributes.Add("class", "lblactive");
                    break;
            }
        }
        // Iqbal Nayan
        private void DataCountShow()
        {
            string comcod = this.GetCompCode();
            DataTable dt4 = (DataTable)ViewState["tblcount"];


            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ((dt4.Rows.Count == 0) ? 0 : Convert.ToDouble(dt4.Rows[0]["tstatus"])).ToString("#,##0;(#,##0);") + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ((dt4.Rows.Count == 0) ? 0 : Convert.ToDouble(dt4.Rows[0]["treadyto"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Ready to Audit</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ((dt4.Rows.Count == 0) ? 0 : Convert.ToDouble(dt4.Rows[0]["tauditc"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> Audit</div></div></div>";

        }
        // Iqbal Nayan
        private void reqStatus()
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "RPTACCOUNTAUDIT", frmdate, todate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                //this.gvAccVoucher.DataSource = null;
                //this.gvAccVoucher.DataBind();
                return;
            }

            Session["tbldate1"] = ds1.Tables[0];
            Session["tbldate2"] = ds1.Tables[1];
            Session["tbldate3"] = ds1.Tables[2];

            this.Data_Bind();

            ViewState["tblcount"] = ds1.Tables[3];
            this.DataCountShow();
        }




        // Iqbal Nayan
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbldate1"];
            this.gvAccVoucher.DataSource = dt;
            this.gvAccVoucher.DataBind();

            DataTable dt2 = (DataTable)Session["tbldate2"];
            this.gvReadytoAudit.DataSource = dt2;
            this.gvReadytoAudit.DataBind();

            DataTable dt3 = (DataTable)Session["tbldate3"];
            this.gvAudit.DataSource = dt3;
            this.gvAudit.DataBind();
        }

        // Iqbal Nayan
        protected void lnkbtnAudit_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string chkeaudit = ((CheckBox)gvr.FindControl("chkAuditid")).Checked ? "True" : "False";
            if (chkeaudit == "True")
            {
                string vounum = ((Label)this.gvReadytoAudit.Rows[rowindex].FindControl("lblvou")).Text.Trim();
                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "AUDITUPDATE", vounum, userid, todate, Sessionid, "", "", "", "", "", "", "", "", "", "", "");

                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Audit Sucess');", true);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Audit Fail');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Checked Box');", true);
            }
        }

        protected void gvReadytoAudit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgvType");
                string gennum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                if (gennum == "")
                {
                    return;
                }

                if (gennum.Substring(0, 2) == "BC" || gennum.Substring(0, 2) == "BD" || gennum.Substring(0, 2) == "CC"
                    || gennum.Substring(0, 2) == "CD" || gennum.Substring(0, 2) == "CT" || gennum.Substring(0, 2) == "JV")
                {

                    hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher?vounum=" + gennum;//?type=ConstRpt&prjcode="+code;
                                                                                         // hlink1.Style.Add("color", "blue");

                }

            }
        }
        protected void gvAudit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgvAudit");
                string gennum1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                if (gennum1 == "")
                {
                    return;
                }

                if (gennum1.Substring(0, 2) == "BC" || gennum1.Substring(0, 2) == "BD" || gennum1.Substring(0, 2) == "CC"
                    || gennum1.Substring(0, 2) == "CD" || gennum1.Substring(0, 2) == "CT" || gennum1.Substring(0, 2) == "JV")
                {

                    hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher?vounum=" + gennum1;//?type=ConstRpt&prjcode="+code;
                                                                                          // hlink1.Style.Add("color", "blue");

                }

            }
        }
    }
}