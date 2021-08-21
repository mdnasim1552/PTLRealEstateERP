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
namespace RealERPWEB.F_62_Mis
{
    public partial class LinkMyHRLeave : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                //string date= System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromdate.Text = "01" + date.Substring(2);


                this.MultiView1.ActiveViewIndex = 0;
                this.ShowLeaveStatus();

                ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE STATUS";

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
            return (hst["hcomcod"].ToString());

        }



        private void ShowLeaveStatus()
        {
            this.lblleavesummary.Visible = true;
            this.lblleavesDetails.Visible = true;
            ViewState.Remove("tblleave");
            string comcod = this.GetCompCode();
            string Empid = this.Request.QueryString["empid"].ToString();
            string frmdate = this.Request.QueryString["frmdate"].ToString();
            string todate = this.Request.QueryString["todate"].ToString();

            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_REPORT_EMP_DASEBOARD", "LEAVESTATUS02", Empid, frmdate, todate, "", "", "", "", "", "");


            if (ds3 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                this.gvLeavedetails.DataSource = null;
                this.gvLeavedetails.DataBind();
                return;

            }

            this.lblname.Text = ds3.Tables[1].Rows[0]["empname1"].ToString();
            this.lbldpt.Text = ds3.Tables[1].Rows[0]["section"].ToString();
            this.lbldesg.Text = ds3.Tables[1].Rows[0]["desig"].ToString();
            this.lblcard.Text = ds3.Tables[1].Rows[0]["idcard"].ToString();


            ViewState["tblleave"] = ds3.Tables[0];
            this.Data_Bind();

        }


        private void Data_Bind()
        {
            DataTable dt = ((DataTable)ViewState["tblleave"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();





            //A. Sales
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'A'");
            dt1 = dvr.ToTable();
            this.gvLeaveStatus.DataSource = dt1;
            this.gvLeaveStatus.DataBind();
            //this.FooterCalculation(dt1, "gvLeaveStatus");   

            //B. Collection Summary
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'B'");
            dt1 = dvr.ToTable();
            this.gvLeavedetails.DataSource = dt1;
            this.gvLeavedetails.DataBind();
            //  this.FooterCalculation(dt1, "gvLeavedetails"); 
            //C. Cheque In Hand




        }


        private void FooterCalculation(DataTable dt, string grview)
        {

            if (dt.Rows.Count == 0)
                return;

            switch (grview)
            {
                case "gvLeaveStatus":
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opleave)", "")) ? 0.00
                    : dt.Compute("sum(opleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleaveentitled")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(enleave)", "")) ? 0.00
                          : dt.Compute("sum(enleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleaveenjoy")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(enjleave)", "")) ? 0.00
                          : dt.Compute("sum(enjleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleavebal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balleave)", "")) ? 0.00
                          : dt.Compute("sum(balleave)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "gvLeavedetails":
                    ((Label)this.gvLeavedetails.FooterRow.FindControl("lblgvFleavedays")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lvday)", "")) ? 0.00
                         : dt.Compute("sum(lvday)", ""))).ToString("#,##0;(#,##0); ");
                    break;



            }


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpLeaveSt":
                    this.PrintEmpLvStatus();
                    break;


            }


        }

        private void PrintEmpLvStatus()
        {
            DataTable dt = (DataTable)ViewState["tblleave"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["hcomcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            string empid = this.Request.QueryString["empid"].ToString();
            string frmdate = this.Request.QueryString["frmdate"].ToString();
            string todate = this.Request.QueryString["todate"].ToString();



            // string empid = this.ddlEmployee.SelectedValue.ToString();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds = HRData.GetTransInfo(comcod, "SP_REPORT_EMP_DASEBOARD", "GETEMPDETAILSINFO", empid, todate, "", "", "", "", "", "", "");
            DataTable dt1 = ds.Tables[0];
            ReportDocument rpcp = new RealERPRPT.R_62_Mis.RptEmpLeavStatus();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            CompName.Text = dt1.Rows[0]["companyname"].ToString();
            TextObject txtaddress = rpcp.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            txtaddress.Text = comadd;
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtccaret.Text = "Period: " + frmdate + " To " + todate;
            TextObject txtcardno = rpcp.ReportDefinition.ReportObjects["txtcardno"] as TextObject;
            txtcardno.Text = dt1.Rows[0]["idcardno"].ToString();
            TextObject txtEmpName = rpcp.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            txtEmpName.Text = dt1.Rows[0]["empname"].ToString();
            TextObject txtDesig = rpcp.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            txtDesig.Text = dt1.Rows[0]["desig"].ToString();

            TextObject txtDepartment = rpcp.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            txtDepartment.Text = dt1.Rows[0]["section"].ToString();

            TextObject txtjoindate = rpcp.ReportDefinition.ReportObjects["txtjoindate"] as TextObject;
            txtjoindate.Text = Convert.ToDateTime(dt1.Rows[0]["joindate"]).ToString("dd-MMM-yyyy");

            TextObject txtprobitionperiod = rpcp.ReportDefinition.ReportObjects["txtservicelength"] as TextObject;
            txtprobitionperiod.Text = dt1.Rows[0]["serlength"].ToString();
            //TextObject txtcondate = rpcp.ReportDefinition.ReportObjects["txtcondate"] as TextObject;
            //txtcondate.Text = (Convert.ToDateTime(dt1.Rows[0]["condate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(dt1.Rows[0]["condate"]).ToString("dd-MMM-yyyy");
            //TextObject txtsalary = rpcp.ReportDefinition.ReportObjects["txtsalary"] as TextObject;
            //txtsalary.Text = Convert.ToDouble(dt1.Rows[0]["gssal"]).ToString("#,##0;(#,##0); ");
            TextObject txtllenjoydate = rpcp.ReportDefinition.ReportObjects["txtllenjoydate"] as TextObject;
            txtllenjoydate.Text = (Convert.ToDateTime(dt1.Rows[0]["lstrtdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(dt1.Rows[0]["lstrtdat"]).ToString("dd-MMM-yyyy"); ;

            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void gvLeaveStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label Description = (Label)e.Row.FindControl("lblgvDescription");
                Label opnleave = (Label)e.Row.FindControl("lblgvopnleave");
                Label leaveentitled = (Label)e.Row.FindControl("lblgvleaveentitled");
                Label leaveenjoy = (Label)e.Row.FindControl("lblgvleaveenjoy");
                Label leavebal = (Label)e.Row.FindControl("lblgvleavebal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "51AAA")
                {


                    Description.Font.Bold = true;
                    opnleave.Font.Bold = true;
                    leaveentitled.Font.Bold = true;
                    leaveenjoy.Font.Bold = true;
                    leavebal.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }

            }

        }
        protected void gvLeavedetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label Description = (Label)e.Row.FindControl("lblgvDescriptionld");

                Label leavedays = (Label)e.Row.FindControl("lblgvleavedays");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "51AAA")
                {


                    Description.Font.Bold = true;
                    leavedays.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }

            }

        }

    }
}
