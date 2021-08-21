using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using RealERPLIB;
namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class EmpLeaveStatus : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = "01" + date.Substring(2);
                this.txttoDate.Text = date;
                //this.txttoDate.Text = Convert.ToDateTime (this.txtfrmDate.Text).AddYears (1).AddDays (-1).ToString ("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Leave Status";
                this.lnkbtnShow_OnClick(null, null);

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lnkbtnShow_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "EMPLEAVESTATUS", frmdate, todate, "", "", "");
            if (ds2 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }
            //Session["leavest"]=ds2.Tables[0];
            Session["leavest"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["leavest"];
            this.gvLeaveStatus.DataSource = dt;
            this.gvLeaveStatus.DataBind();

            ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblprel")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(preday)", "")) ?
                0.00 : dt.Compute("Sum(preday)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblcrntl")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(curday)", "")) ?
                0.00 : dt.Compute("Sum(curday)", ""))).ToString("#,##0;(#,##0); ");

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string empid = dt1.Rows[0]["empid"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["empid"].ToString() == empid)
                {

                    dt1.Rows[j]["empname"] = "";
                }

                empid = dt1.Rows[j]["empid"].ToString();
            }
            return dt1;


        }
        protected void gvLeaveStatus_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell01 = new TableCell();
                cell01.Text = "Leave Details";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 6;


                gvrow.Cells.Add(cell01);

                gvLeaveStatus.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}










