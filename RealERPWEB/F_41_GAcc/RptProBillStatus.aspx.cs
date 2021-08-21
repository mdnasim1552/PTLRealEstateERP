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
namespace RealERPWEB.F_41_GAcc
{
    public partial class RptProBillStatus : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.ProjectName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT PROGRESS BILL STATUS";
                if (this.Request.QueryString["prjcode"].ToString().Length > 0)
                    this.lnkbtnSerOk_Click(null, null);
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ProjectName()
        {
            string comcod = this.GetComCode();
            string Filter1 = this.Request.QueryString["prjcode"].ToString().Length > 0 ? (this.Request.QueryString["prjcode"].ToString() + "%") : ("%" + this.txtSrcPro.Text.Trim() + "%"); ;
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETPROJECT", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void ibtnFindProject_OnClick(object sender, EventArgs e)
        {
            this.ProjectName();
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            this.ShowReport();
        }

        //protected void lnkPrint_Click(object sender, EventArgs e)
        //{       
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comname = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
        //    ReportDocument rpcp = new RealERPRPT.R_41_GAcc.RptPRogBillStatus();
        //    TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    CompName.Text = comname;
        //    TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
        //    txtPrjName.Text = "Project Name: " + prjname;

        //    DataTable dt2 = (DataTable)Session["Header"];
        //    for (int i = 0; i < dt2.Rows.Count; i++)
        //    {

        //        string titletxt = dt2.Rows[i]["sirdesc"].ToString();
        //        string title = "r";
        //        TextObject rpttxth = rpcp.ReportDefinition.ReportObjects[title + (i + 1)] as TextObject;
        //        rpttxth.Text = titletxt;
        //    }

        //    TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = ((Label) this.Master.FindControl("lblTitle")).Text;
        //        string eventdesc = "Print Report";
        //        //string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, "");
        //    }

        //    rpcp.SetDataSource((DataTable)Session["tblProBill"]);
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rpcp.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rpcp;
        //    ((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //           ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        //}

        protected void lnkPrint_Click(object sender, EventArgs e)
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
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            //Session["tblProBill"] = this.HiddenSameData(ds2.Tables[0]);
            //Session["Header"] = ds2.Tables[1];


            DataTable dt = (DataTable)Session["tblProBill"];
            DataTable dt1 = (DataTable)Session["Header"];

            var lst = dt.DataTableToList<RealEntity.C_41_GAcc.ProProgBillStatus>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_41_GAcc.RptProProgBillStatus", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("prjname", prjname));
            Rpt1.SetParameters(new ReportParameter("rptname", "PROJECT PROGRESS BILL STATUS"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void ShowReport()
        {
            Session.Remove("tblProBill");
            string comcod = this.GetComCode();
            string pactcode = ((this.ddlProjectName.SelectedValue.Substring(2) == "0000000000") ? this.ddlProjectName.SelectedValue.ToString().Substring(0, 2) : this.ddlProjectName.SelectedValue.ToString()) + "%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "RPTPROBILLSTATUS", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvProBillSt.DataSource = null;
                this.gvProBillSt.DataBind();
                return;
            }
            Session["tblProBill"] = this.HiddenSameData(ds2.Tables[0]);
            Session["Header"] = ds2.Tables[1];
            //DataTable dt = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                }

            }
            return dt1;

        }


        private void Data_Bind()
        {

            DataTable dt1 = (DataTable)Session["Header"];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                this.gvProBillSt.Columns[i + 7].HeaderText = dt1.Rows[i]["sirdesc"].ToString();
            }
            this.gvProBillSt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            DataTable dt = (DataTable)Session["tblProBill"];
            this.gvProBillSt.DataSource = dt;
            this.gvProBillSt.DataBind();

        }

        protected void gvProBillSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            //if (voucher.Substring(0,2)=="BC"|| voucher.Substring(0,2)=="BD"|| voucher.Substring(0,2)=="CC"|| voucher.Substring(0,2)=="CD"|| voucher.Substring(0,2)=="JV"|| voucher.Substring(0,2)=="CT")  
            //    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgvvounum");
                HyperLink hlnkgvbillno = (HyperLink)e.Row.FindControl("hlnkgvbillno");
                Label actdesc = (Label)e.Row.FindControl("lgvActdesc");

                // Label vounum = (Label)e.Row.FindControl("lgvvounum");
                Label billamt = (Label)e.Row.FindControl("lgvbillamt");
                Label advanced = (Label)e.Row.FindControl("lgvadvanced");

                Label NbillAmt = (Label)e.Row.FindControl("lgvNbillAmt");
                Label taxamt = (Label)e.Row.FindControl("lgvtaxAmt");
                Label vatamt = (Label)e.Row.FindControl("lgvVatAmt");
                Label sdamt = (Label)e.Row.FindControl("lgvSdAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                string voucher = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "XXXXXXXXXXAAAA" || code == "XXXXXXXXXXBBBB")
                {

                    actdesc.Font.Bold = true;
                    hlink1.Font.Bold = true;
                    billamt.Font.Bold = true;
                    advanced.Font.Bold = true;
                    NbillAmt.Font.Bold = true;
                    taxamt.Font.Bold = true;
                    vatamt.Font.Bold = true;
                    sdamt.Font.Bold = true;
                    // actdesc.Style.Add("text-align", "right");
                }
                else if (code == "XXXXXXXXXXCCCC")
                {

                    actdesc.Attributes["style"] = "font-weight:bold;font-size:12px;color:maroon;";
                    hlink1.Attributes["style"] = "font-weight:bold;font-size:12px;color:maroon;";
                    billamt.Attributes["style"] = "font-weight:bold;font-size:12px;color:maroon;";
                    advanced.Attributes["style"] = "font-weight:bold;font-size:12px;color:maroon;";
                    NbillAmt.Attributes["style"] = "font-weight:bold;font-size:12px;color:maroon;";
                    taxamt.Attributes["style"] = "font-weight:bold;font-size:12px;color:maroon;";
                    vatamt.Attributes["style"] = "font-weight:bold;font-size:12px;color:maroon;";
                    sdamt.Attributes["style"] = "font-weight:bold;font-size:12px;color:maroon;";
                }

                if (billno.Length > 0)
                    hlnkgvbillno.NavigateUrl = "~/F_16_Bill/BillEntry.aspx?Type=Entry&genno=" + billno;


                if (voucher.Trim().Length == 14)
                {
                    if (ASTUtility.Left(voucher, 2) == "PV" || ASTUtility.Left(voucher, 2) == "DV")
                    {
                        hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher02.aspx?vounum=" + voucher;
                        hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                    }
                    else
                    {

                        if (ASTUtility.Left(voucher, 2) == "BD" || ASTUtility.Left(voucher, 2) == "CD" || ASTUtility.Left(voucher, 2) == "BC" || ASTUtility.Left(voucher, 2) == "CC" || ASTUtility.Left(voucher, 2) == "JV" || ASTUtility.Left(voucher, 2) == "CT")
                        {
                            hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + voucher;
                            hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                        }
                    }
                }





            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvProBillSt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProBillSt.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


    }
}