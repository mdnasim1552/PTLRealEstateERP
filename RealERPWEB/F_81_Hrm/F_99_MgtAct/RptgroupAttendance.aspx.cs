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

namespace RealERPWEB.F_81_Hrm.F_99_MgtAct
{
    public partial class RptgroupAttendance : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (prevPage.Length == 0)
                //{
                //    prevPage = Request.UrlReferrer.ToString();
                //}

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Group Attendence";
                //((Label)this.Master.FindControl("lblTitle")).Text = "Group Attandance Report";

                //((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Diagnosis Entry";

                //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
                //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

                //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
                //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

                //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
                //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.ShowGroupAttendance();
                if (hst["comcod"].ToString().Substring(0, 1) == "8")
                {
                    this.comlist.Visible = true;
                    this.Company();
                }

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler();
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkDiagEnrty_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            //  ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).Checked += new EventHandler(chkBoxN_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");
            DataTable dt = (DataTable)ViewState["tblgroupAttendace"];
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.ERptGroupAtt>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptGroupAtt", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Group Attendence")); 
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
           
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowGroupAttendance();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;
        }

        private void Company()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.ddlComName.DataTextField = "comsnam";
            this.ddlComName.DataValueField = "comcod";
            this.ddlComName.DataSource = ds1.Tables[0];
            this.ddlComName.DataBind();

        }


        protected void gvRptAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvRptAttn.PageIndex = e.NewPageIndex;
            //this.Data_Bind();

        }
        private void ShowGroupAttendance()
        {
            string comcod = this.GetCompCode();
            string todydate = this.txtFdate.Text;

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETGROUPATTENDENCE", todydate, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            ViewState["tblgroupAttendace"] = ds.Tables[0];
            ViewState["tblgroupAttenPersen"] = ds.Tables[1];

            this.Data_Bind();
        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblgroupAttendace"];
            DataTable dt2 = (DataTable)ViewState["tblgroupAttenPersen"];

            this.gvRptAttn.DataSource = dt;
            this.gvRptAttn.DataBind();





            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "drawChart();", true);

            this.gvAttPersent.DataSource = dt2;
            this.gvAttPersent.DataBind();


            double tostaff = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ttlstap)", "")) ? 0.00 : dt.Compute("Sum(ttlstap)", "")));
            double present = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(present)", "")) ? 0.00 : dt.Compute("Sum(present)", "")));
            double late = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(late)", "")) ? 0.00 : dt.Compute("Sum(late)", "")));
            double eleave = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(earlyLev)", "")) ? 0.00 : dt.Compute("Sum(earlyLev)", "")));
            double onlaeve = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(onlev)", "")) ? 0.00 : dt.Compute("Sum(onlev)", "")));
            double absent = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(absnt)", "")) ? 0.00 : dt.Compute("Sum(absnt)", "")));
            double prepostaff = 0.00, latepostaff = 0.00, eleavepostaff = 0.00, onlaevepostaff = 0.00, absentpostaff = 0.00;
            prepostaff = (present * 100) / tostaff;
            latepostaff = (late * 100) / tostaff;
            eleavepostaff = (eleave * 100) / tostaff;
            onlaevepostaff = (onlaeve * 100) / tostaff;
            absentpostaff = (absent * 100) / tostaff;
            this.txtpresent.Text = prepostaff.ToString("#,##0;(#,##0); ");
            this.txtlate.Text = latepostaff.ToString("#,##0;(#,##0); ");
            this.txtearlylev.Text = eleavepostaff.ToString("#,##0;(#,##0); ");
            this.txtonleave.Text = onlaevepostaff.ToString("#,##0;(#,##0); ");
            this.txtabsent.Text = absentpostaff.ToString("#,##0;(#,##0); ");




            //((Label)this.gvAttPersent.FooterRow.FindControl("lblstaf")).Text = tostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblprs")).Text = prepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblfotlate")).Text = latepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lbleleave")).Text = eleavepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblol")).Text = onlaevepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblabs")).Text = absentpostaff.ToString("#,##0;(#,##0); ");
        }



        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void gvRptAttn_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvcomname = (HyperLink)e.Row.FindControl("hlnkgvcomname");



                //  string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }


                hlnkgvcomname.Font.Bold = true;
                hlnkgvcomname.Style.Add("color", "blue");

                hlnkgvcomname.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkLateElLeaveAAbs.aspx?Type=LELLAndAbsent&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy"); ;





            }
        }
    }
}