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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class RptWeekPresence : System.Web.UI.Page
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

                ((Label)this.Master.FindControl("lblTitle")).Text = "Last 7 Days Presence";

                //this.txtFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtTdate.Text =  System.DateTime.Today.AddDays(-6).ToString("dd-MMM-yyyy");

                string date = System.DateTime.Today.ToString();
                this.txtFdate.Text = Convert.ToDateTime("01-" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txtTdate.Text = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                //this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);

                this.GetCompany();
                this.onDayGraph.Visible = false;
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            this.pnlgraph.Visible = true;
            this.onDayGraph.Visible = true;
            this.ShowGPWeekPresence();
            this.GetOndayGraph();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtCompName = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETCOMPANYNAME", txtCompName, "", "", "", "", "", "", "", "");
            if (ds5 == null)
                return;

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "comcod";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();

        }

        private void ShowGPWeekPresence()
        {
            string comcod = this.ddlCompany.SelectedValue.ToString();
            string sdate = this.txtFdate.Text;
            string edate = this.txtTdate.Text;

            string rdtype = this.rpttype.SelectedValue.ToString();

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETWEEKLYATTENDENCE", edate, sdate, rdtype, "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            ViewState["tblglWeekPre"] = ds.Tables[0];
            //ViewState["tblgOndayPre"] = ds.Tables[1];

            this.GetCalculateGraph();
            this.Data_Bind();
        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblglWeekPre"];

            this.gvWeekPresence.DataSource = dt;
            this.gvWeekPresence.DataBind();

            chartWeekly.Series["Series1"].XValueMember = "ymonddesc";
            chartWeekly.Series["Series1"].YValueMembers = "present";
            chartWeekly.Series["Series2"].XValueMember = "ymonddesc";
            chartWeekly.Series["Series2"].YValueMembers = "staff";
            chartWeekly.DataSource = dt;
            chartWeekly.DataBind();



        }





        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowGPWeekPresence();
            this.GetOndayGraph();
            this.pnlgraph.Visible = true;
            this.onDayGraph.Visible = true;
        }
        private void GetCalculateGraph()
        {


            DataTable dt = (DataTable)ViewState["tblglWeekPre"];
           
            double staff = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1]["staff"]);
            double lrowpre = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1]["present"]);
            double pre6row = Convert.ToDouble(dt.Rows[dt.Rows.Count - 2]["present"]);
            double staff6row = Convert.ToDouble(dt.Rows[dt.Rows.Count - 2]["staff"]);


            double result = pre6row <= 0 ? 0.00 : ((lrowpre - pre6row) * 100) / pre6row;


            this.lbltodayprs.Text = lrowpre.ToString("#,##0;(#,##0);") + "  Present";
            this.lbltodaystaff.Text = staff6row.ToString("#,##0;(#,##0);") + "  Staff";
            this.lblresult.Text = Math.Abs(result).ToString("#,##0;(#,##0);") + "%";


            if (lrowpre > pre6row)
            {
                this.lblrangedwn.Visible = false;
                this.lblrangeup.Visible = true;
                lblresult.BackColor = System.Drawing.ColorTranslator.FromHtml("#00B050");  //System.Drawing.Color.Green;
                lblresult.ForeColor = System.Drawing.Color.White;
                lblrangeup.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00B050"); ;

            }
            else
            {
                this.lblrangeup.Visible = false;

                this.lblrangedwn.Visible = true;
                lblresult.ForeColor = System.Drawing.Color.White;
                lblresult.BackColor = System.Drawing.ColorTranslator.FromHtml("#EA0F0F");
                lblrangedwn.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EA0F0F");
            }
        }
        protected void txtFdate_TextChanged(object sender, EventArgs e)
        {
            this.txtTdate.Text = Convert.ToDateTime(this.txtFdate.Text).AddDays(-6).ToString("dd-MMM-yyyy");
        }
        private void GetOndayGraph()
        {

            string comcod = this.ddlCompany.SelectedValue.ToString();
            string txtFdate = this.txtFdate.Text;
            string rdtype = this.rpttype.SelectedValue.ToString();

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETINDVATTENDENCE", txtFdate, "", rdtype, "", "", "", "", "", "");
            if (ds.Tables[1].Rows.Count == 0)
            {
                this.onDayGraph.Visible = false;
                return;
            }



            DataTable dt2 = ds.Tables[1];
            double tostaff = Convert.ToDouble(dt2.Rows[0]["ttlstaff"].ToString());
            double present = Convert.ToDouble(dt2.Rows[0]["present"].ToString());
            double late = Convert.ToDouble(dt2.Rows[0]["late"].ToString());
            double eleave = Convert.ToDouble(dt2.Rows[0]["earlyLev"].ToString());
            double onlaeve = Convert.ToDouble(dt2.Rows[0]["onlev"].ToString());
            double absent = Convert.ToDouble(dt2.Rows[0]["absnt"].ToString());

            double ttlpresent = present;// +late;

            this.txtpresent.Text = present.ToString("#,##0.00;(#,##0.00);");
            this.txtlate.Text = late.ToString("#,##0.00;(#,##0.00);");
            this.txtearlylev.Text = eleave.ToString("#,##0.00;(#,##0.00);");
            this.txtonleave.Text = onlaeve.ToString("#,##0.00;(#,##0.00);");
            this.txtabsent.Text = absent.ToString("#,##0.00;(#,##0.00);");
            this.txttostaff.Text = tostaff.ToString("#,##0.00;(#,##0.00);");


        }
    }
}