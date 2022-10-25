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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class HREmpMonthlyAtten : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.GetMonth();
                this.GetEmployeeName();


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
            return (hst["comcod"].ToString());
        }
        private void GetMonth()
        {
            //string comcod = this.GetCompCode();
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETMONTHFORABS", "", "", "", "", "", "", "", "", "");
            //this.ddlMonth.DataTextField = "mnam";
            //this.ddlMonth.DataValueField = "mno";
            //this.ddlMonth.DataSource = ds1.Tables[0];
            //this.ddlMonth.DataBind();
            //this.ddlMonth.SelectedValue =System.DateTime.Today.Month.ToString().Trim();
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMonth.DataTextField = "yearmon";
            this.ddlMonth.DataValueField = "ymon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();
        }


        private void GetEmployeeName()
        {
            Session.Remove("tblEmpDesc");
            string comcod = this.GetCompCode();
            string atttype = this.radioAttType.SelectedValue.ToString();
             
            string IdCard = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAME_MANUAL", IdCard, atttype, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblEmpDesc"] = ds1.Tables[0];
            this.ddlEmpName_SelectedIndexChanged(null, null);

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {         
        }
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");
            if (dr1.Length > 0)
            {
                this.lblCompany.Text = dr1[0]["companydesc"].ToString();
                this.lblSection.Text = dr1[0]["secdesc"].ToString();
                this.lblDesignation.Text = dr1[0]["desig"].ToString();
            }
            this.ddlMonth_SelectedIndexChanged(null, null);
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string Month = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string date = "01-" + Month + "-" + year;
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "ABSENT_DATE", date, empid, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                return;
            }
            DataTable dt = ds4.Tables[0];
        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string empid = this.ddlEmpName.SelectedValue.ToString();
            //string month = this.ddlMonth.SelectedValue.ToString().Trim();
            //string month1=month.PadLeft(2, '0');
            //string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            //string monyr = month1+ year;
            //bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "DELETEABSCT", empid, monyr, "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (result == false) 
            //{
            //    this.lmsg11.Text = "Data was Noted Updated";
            //    return;
            //}
            //for (int i = 0; i < this.chkDate.Items.Count; i++) 
            //{
            //    if (this.chkDate.Items[i].Selected) 
            //    {
            //        string absdat = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("dd-MMM-yyyy");
            //        string absfl ="1";
            //        bool result1 = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, absdat, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");
            //    }
            //}
            //this.lmsg11.Text = "Updated Successfully";

        }
        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void gvMonthlyAttn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void lFinalTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        private void ShowData()
        {
            Session.Remove("tblEmpDesc");
            string comcod = this.GetCompCode();
            string Empid = this.ddlEmpName.SelectedValue.ToString();    
            string MonthId = this.ddlMonth.SelectedValue.ToString().Trim();
            //string yearmon = this.ddlMonth.SelectedValue.ToString(); ;
            // string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);        
            // DateTime date2 = DateTime.ParseExact(date1, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            //DateTime date1 = DateTime.Parse(this.ddlMonth.SelectedValue.ToString());
           // string ymonid = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ymonid")).ToString();
            string todate = "";
            string frmdate = "";
            
            switch (comcod)
            {
                case "3369":
                case "3365":
                case "3101":
                //case "3348":
                    string date1 = "26-" + ASTUtility.Month3digit(Convert.ToInt32(MonthId.Substring(4, 2))) + "-" + MonthId.Substring(0, 4);
                    frmdate = Convert.ToDateTime(date1).AddMonths(-1).ToString("dd-MMM-yyyy");
                    todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;
                default:
                    frmdate = "01-" + ASTUtility.Month3digit(Convert.ToInt32(MonthId.Substring(4, 2))) + "-" + MonthId.Substring(0, 4);
                    todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy"); 
                    break;
            }
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "MONTHLYTENDENCE", Empid, MonthId, frmdate, todate, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvMonthlyAttn.DataSource = null;
                this.gvMonthlyAttn.DataBind();
                return;
            }
            Session["tblEmpDesc"] = ds4.Tables[0];
            this.LoadGrid();
        }
        private void LoadGrid()
        {
            //this.gvMonthlyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMonthlyAttn.DataSource = (DataTable)Session["tblEmpDesc"]; ;
            this.gvMonthlyAttn.DataBind();
        }
        protected void gvMonthlyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            int TblRowIndex;
            for (int i = 0; i < this.gvMonthlyAttn.Rows.Count; i++)
            {
                string offintime = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvoffIntime")).Text.Trim();
                string offouttime = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvoffouttime")).Text.Trim();
                string intime = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvIntime")).Text.Trim();
                string outime = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvOuttime")).Text.Trim();
                string lnintime = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvlnintime")).Text.Trim();
                string lnouttime = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvlnouttime")).Text.Trim();
                string leave = ((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvLeave")).Text.Trim();
                string absent = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvAbsent")).Text.Trim();
                string hday = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvholiday")).Text.Trim();
                //string dedout = Convert.ToDouble("0" + ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvDed")).Text.Trim()).ToString();
                //string Addhour = Convert.ToDouble("0" + ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvAddHour")).Text.Trim()).ToString();
                TblRowIndex = (gvMonthlyAttn.PageIndex) * gvMonthlyAttn.PageSize + i;

                dt.Rows[TblRowIndex]["offintime"] = Convert.ToDateTime(((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + offintime;
                dt.Rows[TblRowIndex]["offouttime"] = Convert.ToDateTime(((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + offouttime;
                dt.Rows[TblRowIndex]["intime"] = Convert.ToDateTime(((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + intime;
                dt.Rows[TblRowIndex]["outtime"] = Convert.ToDateTime(((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + outime;
                dt.Rows[TblRowIndex]["lnchintime"] = Convert.ToDateTime(((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + lnintime;
                dt.Rows[TblRowIndex]["lnchouttime"] = Convert.ToDateTime(((Label)this.gvMonthlyAttn.Rows[i].FindControl("lblgvDate")).Text.Trim()).ToString("dd-MMM-yyyy") + " " + lnouttime;
                dt.Rows[TblRowIndex]["leav"] = leave;
                dt.Rows[TblRowIndex]["absnt"] = absent;
                dt.Rows[TblRowIndex]["hday"] = hday;
                //dt.Rows[TblRowIndex]["dedout"] = dedout;
                //dt.Rows[TblRowIndex]["addhour"] = Addhour;
            }
            Session["tblEmpDesc"] = dt;
        }
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string msg = "";
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            string comcod = this.GetCompCode();
            string MonthId = this.ddlMonth.SelectedValue.ToString().Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string absent = dt.Rows[i]["absnt"].ToString().Trim();
                string leave = dt.Rows[i]["leav"].ToString().Trim();
                string hday = dt.Rows[i]["hday"].ToString().Trim();
                if ((absent != "A") && (leave != "L") && (hday != "H"))
                {
                    string dayid = Convert.ToDateTime(dt.Rows[i]["cdate"].ToString()).ToString("yyyyMMdd");
                    string empid = dt.Rows[i]["empid"].ToString();
                    string machid = "01";
                    string idcardno = dt.Rows[i]["idcardno"].ToString();
                    string intime = dt.Rows[i]["intime"].ToString();
                    string outtime = dt.Rows[i]["outtime"].ToString();
                    string dedout = "0";
                    string addhour = "0";
                    string addoffhour = "0";
                    string offintime = dt.Rows[i]["offintime"].ToString();
                    string offoutime = dt.Rows[i]["offouttime"].ToString();
                    string lnintime = dt.Rows[i]["lnchintime"].ToString();
                    string lnoutime = dt.Rows[i]["lnchouttime"].ToString();
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIME", dayid, empid, machid, idcardno, intime, outtime, leave, absent, dedout, addhour, addoffhour, offintime, offoutime, lnintime, lnoutime);
                }
                if (absent == "A")
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string frmdate = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy");
                    string absfl = "1";
                    string month = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("ddMMyyyy").Substring(2, 2);
                    //tring month1 = month.PadLeft(2, '0');
                    string year = ASTUtility.Right(Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy"), 4);
                    string monyr = month + year;
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, frmdate, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");
                }
                if (hday == "H")
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string hdate = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTOUPHOLIDAY", hdate, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
                }
            }
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            this.LoadGrid();
        }
        protected void radioAttType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmployeeName();
        }
    }
}
