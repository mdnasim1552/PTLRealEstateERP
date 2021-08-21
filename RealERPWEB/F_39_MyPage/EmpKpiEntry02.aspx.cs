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
//using  RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_39_MyPage
{
    public partial class EmpKpiEntry02 : System.Web.UI.Page
    {
        UserManagerKPI objUser = new UserManagerKPI();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Job Execution";
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetEmpList();
            }
        }


        private void GetEmpList()
        {
            if (this.lnkok.Text == "New")
                return;
            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string userid = (this.Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";

            string deptcode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid, deptcode);

            this.ddlEmpid.DataTextField = "empname";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();

        }





        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }




        protected void lnkok_Click(object sender, EventArgs e)
        {

            if (this.lnkok.Text == "Ok")
            {


                string comcod = this.Getcomcod();
                this.ddlEmpid.Enabled = false;
                this.txtdate.Enabled = false;

                this.lnkok.Text = "New";
                this.ShowEmpData();
            }
            else
            {

                this.lnkok.Text = "Ok";
                this.gvempkpi.DataSource = null;
                this.gvempkpi.DataBind();
                this.txtdate.Enabled = true;
                this.ddlEmpid.Enabled = true;
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            }
        }

        private void ShowEmpData()
        {
            string comcod = this.Getcomcod();
            string empid = this.ddlEmpid.SelectedValue.ToString();

            DataSet ds = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWEMPACTIVITIES", empid);
            ViewState["tbEmpKpiEnrty"] = this.HiddenSameData(ds.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    dt1.Rows[j]["pactdesc"] = "";
                pactcode = dt1.Rows[j]["pactcode"].ToString();
            }
            return dt1;

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tbEmpKpiEnrty"];
            this.gvempkpi.DataSource = dt;
            this.gvempkpi.DataBind();
            this.FooterCal();

        }
        private void FooterCal()
        {


            DataTable dt = (DataTable)ViewState["tbEmpKpiEnrty"];

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvempkpi.FooterRow.FindControl("lblgvFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(duration)", "")) ?
                                0 : dt.Compute("sum(duration)", ""))).ToString("#,##0;(#,##0); ");

        }

        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tbEmpKpiEnrty"];
            for (int i = 0; i < this.gvempkpi.Rows.Count; i++)
            {

                string acstdate = (((TextBox)this.gvempkpi.Rows[i].FindControl("txtacstDate")).Text.Trim() == "") ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvempkpi.Rows[i].FindControl("txtacstDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string acdate = (((TextBox)this.gvempkpi.Rows[i].FindControl("txtacDate")).Text.Trim() == "") ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvempkpi.Rows[i].FindControl("txtacDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                dt.Rows[i]["acstdat"] = acstdate;
                dt.Rows[i]["acenddat"] = acdate;


            }
            ViewState["tbEmpKpiEnrty"] = dt;

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            string comcod = this.Getcomcod();
            this.Save_Value();
            DataTable dt = (DataTable)ViewState["tbEmpKpiEnrty"];
            bool result;

            foreach (DataRow dr2 in dt.Rows)
            {

                string pactcode = dr2["pactcode"].ToString();
                string Actcode = dr2["actcode"].ToString();
                string acstdat = Convert.ToDateTime(dr2["acstdat"].ToString()).ToString("dd-MMM-yyyy");
                string acdat = Convert.ToDateTime(dr2["acenddat"].ToString()).ToString("dd-MMM-yyyy");


                if (acstdat != "01-Jan-1900" || acdat != "01-Jan-1900")
                {

                    result = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATEEMPSTDKPI02FMAR", pactcode, Actcode, acstdat, acdat, "", "", "", "", "", "", "", "", "", "", "");

                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Project Information";
                string eventdesc = "Update Project Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void lnkprint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string comcod = this.Getcomcod();
            //string clientcod = this.ddlClientList.SelectedValue.ToString();
            //DataSet dset1 = this.KpiData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTCLIENTCOMUCATION", clientcod, "", "", "", "", "", "", "", "");
            //DataTable dtab1 = dset1.Tables[0];
            //ReportDocument rptAppMonitor = new  RealERPRPT.R_21_Mkt.RptTodaysDisAndNextApp();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = this.ddlSalesTeam.SelectedItem.Text;
            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //TextObject txtclientname = rptAppMonitor.ReportDefinition.ReportObjects["txtclientname"] as TextObject;
            //txtclientname.Text = this.ddlClientList.SelectedItem.Text;
            //rptAppMonitor.SetDataSource(dtab1);
            //Session["Report1"] = rptAppMonitor;
            //this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetEmpList();
        }




        protected void gvempkpi_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvcomments = (HyperLink)e.Row.FindControl("hlnkgvcomments");
                string empid = this.ddlEmpid.SelectedValue.ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string actdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc")).ToString();
                string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");



                hlnkgvcomments.NavigateUrl = "~/F_05_MyPage/LinkActiComments.aspx?empid=" + empid + "&pactcode=" + pactcode + "&pactdesc=" + pactdesc + "&actcode=" + actcode + "&actdesc=" + actdesc + "&date=" + date;


            }

        }

        protected void gvempkpi_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;


                TableCell cell04 = new TableCell();
                cell04.Text = "TARGET";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;


                TableCell cell05 = new TableCell();
                cell05.Text = "ACTUAL";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;
                cell05.Font.Bold = true;



                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;
                cell06.Font.Bold = true;






                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);



                gvempkpi.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}
