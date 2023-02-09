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
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RetiredEmpFinalSett : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtCurdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetEmployeeName();

            }

        }

        protected void imgbtnFindEmp_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetEmployeeName()
        {
            string comcod = this.GetComeCode();
            //string txtSProject = this.txtSrcProject.Text.Trim();

            string txtSEmp = "%" + this.ddlEmpName.Text + "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "EMPSETMENT", txtSEmp, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();

            Session["tblEmpInfo"] = ds1.Tables[0];
            ddlEmpName_OnSelectedIndexChanged(null, null);
        }
        protected void ddlEmpName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.PanelEmpinfo.Visible = true;
            DataTable dt = (DataTable)Session["tblEmpInfo"];
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");
            this.lblname.Text = dr1.Length == 0 ? "" : dr1[0]["empname"].ToString();
            this.lbldesig.Text = dr1.Length == 0 ? "" : dr1[0]["desig"].ToString();
            this.lblwrkst.Text = dr1.Length == 0 ? "" : dr1[0]["wrkstation"].ToString();
            this.lbljdate.Text = dr1.Length == 0 ? "" : Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");
            this.lblrsd.Text = dr1.Length == 0 ? "" : Convert.ToDateTime(dr1[0]["retired"]).ToString("dd-MMM-yyyy");
            this.lblslen.Text = dr1.Length == 0 ? "" : dr1[0]["serlength"].ToString();
        }

        protected void lnkTotal_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblrtrEmpSal"];

            this.SaveValue();
            this.Data_Bind();

        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblrtrEmpSal"];
            for (int i = 0; i < this.gvRtrSalSett.Rows.Count; i++)
            {
                double gratuity = Convert.ToDouble("0" + ((TextBox)this.gvRtrSalSett.Rows[i].FindControl("lgvnetamtcash")).Text.Trim());

                dt.Rows[i]["sal"] = gratuity;
            }
        }
        private void ShowEmpSalary()
        {
            string comcod = this.GetComeCode();
            string emid = this.ddlEmpName.SelectedValue;
            DataSet ds1 = purData.GetTransInfo(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "RETIREEMPSAL", emid, "", "", "", "", "", "", "", "");
            Session["tblrtrEmpSal"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblrtrEmpSal"];
            this.gvRtrSalSett.DataSource = (DataTable)Session["tblrtrEmpSal"];
            this.gvRtrSalSett.DataBind();

            ((Label)this.gvRtrSalSett.FooterRow.FindControl("lgvFTNetmtcash")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sal)", "")) ? 0.00 : dt.Compute("sum(sal)", ""))).ToString("#,##0;(#,##0); ");
        }
        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lbtnPrevsetNo.Visible = false;
                this.ddlPrevsetNo.Visible = false;
                this.ShowEmpSalary();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.ddlPrevsetNo.Items.Clear();
            this.lbtnPrevsetNo.Visible = true;
            this.ddlPrevsetNo.Visible = true;
            this.gvRtrSalSett.DataSource = null;
            this.gvRtrSalSett.DataBind();

        }

        protected void lnkUpdate_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string emid = this.ddlEmpName.SelectedValue;
            string date = this.txtCurdate.Text;
            string curdate = Convert.ToDateTime(this.txtCurdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string setno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            DataTable dt = (DataTable)Session["tblrtrEmpSal"];
            bool result = false;
            foreach (DataRow dr1 in dt.Rows)
            {

                string code = dr1["code"].ToString();
                string desc = dr1["gdesc"].ToString();
                string amt = dr1["sal"].ToString();

                result = purData.UpdateTransInfo(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "UPDATEEMPSETT", emid, code, desc, amt, date, setno, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString().ToUpper();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = this.txtCurdate.Text;
            DataTable dt1 = (DataTable)Session["tblEmpInfo"];
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataRow[] dr1 = dt1.Select("empid='" + empid + "'");

            string name = dr1.Length == 0 ? "" : dr1[0]["empname"].ToString();
            string desig = dr1.Length == 0 ? "" : dr1[0]["desig"].ToString();
            string wrkst = dr1.Length == 0 ? "" : dr1[0]["wrkstation"].ToString();
            string jdate = dr1.Length == 0 ? "" : Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");
            string rlsdate = dr1.Length == 0 ? "" : Convert.ToDateTime(dr1[0]["retired"]).ToString("dd-MMM-yyyy");
            string slen = dr1.Length == 0 ? "" : dr1[0]["serlength"].ToString();


            LocalReport Rpt1 = new LocalReport();
            DataTable dt3 = (DataTable)Session["tblrtrEmpSal"];

            var lst = dt3.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.EmpSettlmnt.EmpFinalSettlmnt>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.EmpFinalSettmnt", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));

            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            // Rpt1.SetParameters (new ReportParameter ("Inword", "In Word: " + ASTUtility.Trans (tAmt, 2)));


            Rpt1.SetParameters(new ReportParameter("name", name));
            Rpt1.SetParameters(new ReportParameter("desig", desig));
            Rpt1.SetParameters(new ReportParameter("wrkst", wrkst));
            Rpt1.SetParameters(new ReportParameter("jdate", jdate));
            Rpt1.SetParameters(new ReportParameter("rlsdate", rlsdate));
            Rpt1.SetParameters(new ReportParameter("slen", slen));
            Rpt1.SetParameters(new ReportParameter("date", date));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void GetOffNO()
        {
            string comcod = this.GetComeCode();
            string mSETNO = "NEWSET";
            if (this.ddlPrevsetNo.Items.Count > 0)
                mSETNO = this.ddlPrevsetNo.SelectedValue.ToString();

            string date = this.txtCurdate.Text; ;
            if (mSETNO == "NEWSET")
            {
                DataSet ds3 = purData.GetTransInfo(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "LASTSETTINFO", date, "", "", "", "", "", "", "", "");

                if (ds3 == null)
                    return;
                if (ds3.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxsetno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxsetno1"].ToString().Substring(6);

                    this.ddlPrevsetNo.DataTextField = "maxsetno1";
                    this.ddlPrevsetNo.DataValueField = "maxsetno";
                    this.ddlPrevsetNo.DataSource = ds3.Tables[0];
                    this.ddlPrevsetNo.DataBind();
                }
            }

        }
        private void GetPreSetNo()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurdate.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "[dbo_hrm].[SP_ENTRY_EMPLOYEE]", "GETPREVSETNO", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevsetNo.DataTextField = "setno1";
            this.ddlPrevsetNo.DataValueField = "setno";
            this.ddlPrevsetNo.DataSource = ds1.Tables[0];
            this.ddlPrevsetNo.DataBind();
        }

        protected void lbtnPrevsetNo_OnClick(object sender, EventArgs e)
        {
            this.GetPreSetNo();
        }
    }
}