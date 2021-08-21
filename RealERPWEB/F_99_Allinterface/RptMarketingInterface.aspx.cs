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
using RealERPLIB;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptMarketingInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                // this.GetBirthDay();
                // this.GetMarriageDay();

                //this.PurchaseInfoRpt();
                this.RadioButtonList1.SelectedIndex = 3;
                ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.MkIntView.ActiveViewIndex = 2;
                    break;
                case "1":
                    this.MkIntView.ActiveViewIndex = 3;
                    break;
                case "2":
                    this.MkIntView.ActiveViewIndex = 4;
                    break;
                case "3":
                    this.MkIntView.ActiveViewIndex = 0;
                    //  this.GetBirthDay();
                    break;
                case "4":
                    this.MkIntView.ActiveViewIndex = 1;
                    // this.GetMarriageDay();
                    break;
            }
        }
        private void PurchaseInfoRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "MKTDASHBORD", frmdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;



            //DataTable dt1 = (DataTable)ViewState["tblClientMrgDay"];
            //DataTable dt = (DataTable)ViewState["tblclbdday"];
            //if (dt == null)
            //    return;
            //string bday = dt.Rows.Count.ToString();
            //if (dt1 == null)
            //    return;
            //string mday1 = dt1.Rows.Count.ToString();


            //this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[6].Rows[0]["reqqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Appointment " + "</span>";
            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[3].Rows[0]["appointment"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Appointment " + "</span>";

            this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[3].Rows[0]["napoint"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "New Appointment " + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[3].Rows[0]["disw"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Discussion" + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[3].Rows[0]["bdday"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Birth Day" + "</span>";

            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[3].Rows[0]["marday"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Marriage Day" + "</span>";

            DataTable dt = new DataTable();
            DataView dv = new DataView();
            //BitrhDay
            dt = (DataTable)ds1.Tables[0];
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus = 'Requisition Checked' ");
            this.Data_Bind("gvClientBrthDay", dt);


            //MrgDay
            dt = (DataTable)ds1.Tables[1];
            this.Data_Bind("gvClientMrgDay", dt);


            //Appoinment 
            dt = (DataTable)ds1.Tables[2];
            this.Data_Bind("gvtodayapp", dt);
        }

        //private void GetBirthDay()
        //{

        //    string comcod = this.GetCompCode();
        //    string date = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM");

        //    DataSet ds2 = accData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_CLIENT_INFORMATION", "GETCLIENTMARGDAY", date, "", "", "", "", "", "", "", "");

        //    if (ds2 == null)
        //    {
        //        this.gvClientBrthDay.DataSource = null;
        //        this.gvClientBrthDay.DataBind();
        //        return;
        //    }

        //    //if (ds2.Tables[0].Rows.Count == 0)
        //    //    this.LblmMsg.Text = "No Match Found";

        //    ViewState["tblclbdday"] = ds2.Tables[0];
        //    gvClientBrthDay.DataSource = ds2.Tables[0];
        //    gvClientBrthDay.DataBind();

        //}
        //private void GetMarriageDay()
        //{
        //    ((Label)this.Master.FindControl("lblTitle")).Text = "";
        //    string comcod = this.GetCompCode();
        //    string date = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM");

        //    DataSet ds2 = accData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_CLIENT_INFORMATION", "GETCLIENTMARGDAY", date, "", "", "", "", "", "", "", "");

        //    if (ds2 == null)
        //    {
        //        this.gvClientMrgDay.DataSource = null;
        //        this.gvClientMrgDay.DataBind();
        //        return;
        //    }

        //    ViewState["tblClientMrgDay"] = ds2.Tables[0];
        //    gvClientMrgDay.DataSource = ds2.Tables[0];
        //    gvClientMrgDay.DataBind();
        //}
        //private void ShowNextApp()
        //{

        //    Session.Remove("tbltoapp");
        //    string comcod = this.GetCompCode();
        //    //string teamcode = this.ddlSalesTeam.SelectedValue.ToString();
        //    string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
        //    DataSet ds1 = this.accData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", "RPTNEXTAPP", teamcode, frmdate, todate, "", "", "", "", "", "");
        //    if (ds1 == null)
        //    {
        //        this.gvtodayapp.DataSource = null;
        //        this.gvtodayapp.DataBind();
        //        return;

        //    }
        //    Session["tbltoapp"] = ds1.Tables[0];
        //    Session["tbltoapp1"] = ds1.Tables[1];
        //    gvtodayapp.DataSource = ds1.Tables[0];
        //    gvtodayapp.DataBind();

        //    //this.Data_Bind();

        //}
        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvClientBrthDay":
                    this.gvClientBrthDay.DataSource = dt;
                    this.gvClientBrthDay.DataBind();
                    break;

                case "gvClientMrgDay":
                    this.gvClientMrgDay.DataSource = dt;
                    this.gvClientMrgDay.DataBind();
                    break;

                case "gvtodayapp":
                    this.gvtodayapp.DataSource = dt;
                    this.gvtodayapp.DataBind();
                    break;

            }

            // this.FooterCalculation(gv, dt);
        }
    }
}
