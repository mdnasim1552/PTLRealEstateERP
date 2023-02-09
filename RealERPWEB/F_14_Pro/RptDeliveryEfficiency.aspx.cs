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

namespace RealERPWEB.F_14_Pro
{
    public partial class RptDeliveryEfficiency : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Materials Delivery Efficiency Report";

            }

        }

        public string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadGrid()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTDELIVERYEFF", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvRptDelEff.DataSource = null;
                this.gvRptDelEff.DataBind();
                return;
            }

            Session["tbDeEffi"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            this.gvRptDelEff.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptDelEff.DataSource = (DataTable)Session["tbDeEffi"];
            this.gvRptDelEff.DataBind();


            Session["Report1"] = gvRptDelEff;
            ((HyperLink)this.gvRptDelEff.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
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

            return dt1;
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvRptDelEff_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRptDelEff.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
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
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtDate = "From " + fromdate + " To " + todate;


            DataTable dt = (DataTable)Session["tbDeEffi"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.DeliveryEffciency>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.rptDeliveryEfficiency", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Delivery Efficiency Report"));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = Getcomcod();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //DataTable dt = (DataTable)Session["tbDeEffi"];

            //ReportDocument rptstate = new RealERPRPT.R_14_Pro.rptDeliveryEfficiency();

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptftdate.Text = "Date: " + this.txtfromdate.Text + " To " + this.txttodate.Text;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource(dt);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Transaction Statement";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            ////lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ////                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lbtnAddMat_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.Getcomcod();
                Session.Remove("matleadtime");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETMATDELLEADTIME", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.grvmatlead.DataSource = null;
                    this.grvmatlead.DataBind();
                    return;
                }
                Session["matleadtime"] = ds1.Tables[0];
                this.Data_BindMat();

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openLeadModal();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void Data_BindMat()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["matleadtime"];
                this.grvmatlead.DataSource = tbl1;
                this.grvmatlead.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSaveLead_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session_Update();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataTable tblt05 = (DataTable)Session["matleadtime"];
                foreach (DataRow dr in tblt05.Rows)
                {
                    string sircode = dr["sircode"].ToString();
                    string mark = dr["mark"].ToString();
                    bool resulta = MktData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE01", "INSUPDATEMATLEADTIME", sircode, mark, "", "", "", "",
                                "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                        return;
                    }
                }
               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        protected void Session_Update()
        {
            DataTable tbl1 = (DataTable)Session["matleadtime"];
            int TblRowIndex2;
            for (int j = 0; j < this.grvmatlead.Rows.Count; j++)
            {
                double mark = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvmatlead.Rows[j].FindControl("txtgvmark")).Text.Trim()));
                ((TextBox)this.grvmatlead.Rows[j].FindControl("txtgvmark")).Text = mark.ToString("#,##0.00;(#,##0.00); ");
                TblRowIndex2 = (this.grvmatlead.PageIndex) * this.grvmatlead.PageSize + j;
                tbl1.Rows[TblRowIndex2]["mark"] = mark;
            }
            Session["matleadtime"] = tbl1;
            this.Data_BindMat();
        }
    }
}











