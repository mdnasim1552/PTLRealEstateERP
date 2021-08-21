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
namespace RealERPWEB.F_81_Hrm.F_97_MIS
{
    public partial class RptNewJoingInfo : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01" + date.Substring(2);
                this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetDynamcifield();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE JOINING INFORMATION";
                this.ddlReportLevel_SelectedIndexChanged(null, null);
                this.lnkbtnShow_Click(null, null);
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (hst["comcod"].ToString().Substring(0, 1) == "8")
                {
                    this.comlist.Visible = true;
                    this.Company();
                }
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowMgtInterface();
        }

        private void GetDynamcifield()
        {
            //ViewState.Remove("tbldyfield");
            string comcod = this.GetCompCode();

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "RPTMGTDYNAMICFIELD", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.ddlOrder.Items.Clear();
                return;
            }

            this.ddlOrder.DataTextField = "descrip";
            this.ddlOrder.DataValueField = "code";
            this.ddlOrder.DataSource = ds4.Tables[0];
            this.ddlOrder.DataBind();
            //ViewState["tbldyfield"] = ds4.Tables[0];


        }


        private void ShowMgtInterface()
        {
            ViewState.Remove("tblempstatus");
            string comcod = this.GetCompCode();
            string fdate = Convert.ToDateTime(this.txtFdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string tdate = Convert.ToDateTime(this.txtTdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string order = "";
            if (this.ddlOrder.SelectedValue != "00000")
            {
                order = this.ddlOrder.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString();
            }

            string label = this.ddlReportLevel.SelectedValue.ToString();

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTNEWEMPSTATUS", fdate, tdate, label, order, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            ViewState["tblempstatus"] = this.HiddenSameData(dt);
            this.Data_Bind();

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string company = dt1.Rows[0]["company"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            string deptid = dt1.Rows[0]["deptid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    dt1.Rows[j]["section"] = "";
                }
                if (dt1.Rows[j]["deptid"].ToString() == deptid)
                {
                    dt1.Rows[j]["department"] = "";
                }

                if (dt1.Rows[j]["company"].ToString() == company)
                {
                    dt1.Rows[j]["companyname"] = "";
                }

                company = dt1.Rows[j]["company"].ToString();
                deptid = dt1.Rows[j]["deptid"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();



            }

            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblempstatus"];


            this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpList.DataSource = dt;
            this.gvEmpList.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblempstatus"];

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvEmpList.FooterRow.FindControl("lgvFNoEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(noemp)", "")) ? 0.00 : dt.Compute("sum(noemp)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpList.FooterRow.FindControl("lgvFsalary")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");





        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptGradeWiseEmp();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;


            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)ViewState["tblempstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        protected void ddlReportLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.ddlReportLevel.SelectedValue.ToString() == "1")
            //{
            //    this.gvEmpList.Columns[3].Visible = false;
            //    this.gvEmpList.Columns[4].Visible = false;
            //    this.gvEmpList.Columns[5].Visible = false;
            //    this.gvEmpList.Columns[6].Visible = false;
            //    this.gvEmpList.Columns[9].Visible = false;
            //    this.gvEmpList.Columns[13].Visible = false;
            //}
            //else if (this.ddlReportLevel.SelectedValue.ToString() == "2")
            //{
            //    this.gvEmpList.Columns[3].Visible = true;
            //    this.gvEmpList.Columns[4].Visible = false;
            //    this.gvEmpList.Columns[5].Visible = false;
            //    this.gvEmpList.Columns[6].Visible = false;
            //    this.gvEmpList.Columns[9].Visible = false;
            //    this.gvEmpList.Columns[13].Visible = false;

            //}
            //else if (this.ddlReportLevel.SelectedValue.ToString() == "3")
            //{
            //    this.gvEmpList.Columns[3].Visible = true;
            //    this.gvEmpList.Columns[4].Visible = true;
            //    this.gvEmpList.Columns[5].Visible = false;
            //    this.gvEmpList.Columns[6].Visible = false;
            //    this.gvEmpList.Columns[9].Visible = false;
            //    this.gvEmpList.Columns[13].Visible = false;

            //}
            //else
            //{
            this.gvEmpList.Columns[3].Visible = true;
            this.gvEmpList.Columns[4].Visible = true;
            this.gvEmpList.Columns[5].Visible = true;
            this.gvEmpList.Columns[6].Visible = true;
            this.gvEmpList.Columns[9].Visible = true;
            this.gvEmpList.Columns[10].Visible = false;
            this.gvEmpList.Columns[11].Visible = false;
            this.gvEmpList.Columns[12].Visible = false;

            this.gvEmpList.Columns[13].Visible = true;



            //}

        }
        protected void gvEmpList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink serperiod = (HyperLink)e.Row.FindControl("hlnkgvserperiod");
                HyperLink hlnksalary = (HyperLink)e.Row.FindControl("hlnksalary");
                HyperLink Late = (HyperLink)e.Row.FindControl("hlnkgvLate");
                Label joinning = (Label)e.Row.FindControl("lblgvjoindateemp");

                string comcod = this.GetCompCode();
                string Empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();

                string jdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "jdat")).ToString();

                if (jdat == "1")
                {
                    joinning.Style.Add("color", "red");
                }

                //---------------------Salary-----------------------//
                if (Empid != "000000000000")
                {
                    hlnksalary.Font.Bold = true;
                    hlnksalary.NavigateUrl = "LinkMgtInterface.aspx?Type=EmpSal&comcod=" + comcod + "&empid=" + Empid;
                }
                //---------------------Late-----------------------//

                Late.Font.Bold = true;
                Late.NavigateUrl = "LinkMgtInterface.aspx?Type=LateStatus&comcod=" + comcod + "&empid=" + Empid + "&Date1=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") +
                        "&Date2=" + Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");

                //---------------------Service period-----------------------//

                serperiod.Style.Add("color", "blue");
                serperiod.NavigateUrl = "LinkMgtInterface.aspx?Type=Services&comcod=" + comcod + "&empid=" + Empid + "&Date=" + Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");

            }

        }
    }
}