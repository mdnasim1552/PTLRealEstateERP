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

using RealERPRPT;
using RealERPLIB;

namespace RealERPWEB.F_81_Hrm.F_83_Att

{

    public partial class HRDailyAttenManually : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                Session.Remove("DailyAttendence");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompany();
                //((Label)this.Master.FindControl("lblTitle")).Text = "DAILY ATTENDANCE INFORMATION";
                //((Label)this.Master.FindControl("lblmsg")).Visible = false;

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
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

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            ds1.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);


        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetSection();

        }

        private void GetSection()
        {


            string comcod = this.GetCompCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string DeptCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            // string txtsection = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETSECTIONNAME", Company, DeptCode, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = ds1.Tables[0];
            this.ddlSection.DataBind();
            ds1.Dispose();



        }


        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowData();
        }



        private void ShowData()
        {


            Session.Remove("DailyAttendence");

            string comcod = this.GetCompCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string txtempcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string atttype = this.radioAttType.SelectedValue.ToString();

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DAILYATTENDENCE", Deptid, dayid, date, Company, txtempcode, section, atttype, "", "");
            if (ds4 == null)
            {
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;
            }

            Session["DailyAttendence"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }

                else
                {

                    if (dt1.Rows[j]["secid"].ToString() == secid)
                    {
                        dt1.Rows[j]["section"] = "";

                    }

                    secid = dt1.Rows[j]["secid"].ToString();
                }

            }
            return dt1;

        }



        private void LoadGrid()
        {

            this.gvDailyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDailyAttn.DataSource = (DataTable)Session["DailyAttendence"]; ;
            this.gvDailyAttn.DataBind();
        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveValue();
            DataTable dt = (DataTable)Session["DailyAttendence"];
            string comcod = this.GetCompCode();
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string absent = dt.Rows[i]["absnt"].ToString().Trim();
                string leave = dt.Rows[i]["leave"].ToString().Trim();
                if ((absent != "A") && (leave != "L"))
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string machid = "01";
                    string idcardno = dt.Rows[i]["idcardno"].ToString();
                    string intime = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dt.Rows[i]["intime"].ToString()).ToString("hh:mm:ss tt");
                    string outtime = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dt.Rows[i]["outtime"].ToString()).ToString("hh:mm:ss tt");
                    string dedout = dt.Rows[i]["dedout"].ToString();
                    string addhour = dt.Rows[i]["addhour"].ToString();
                    string addoffhour = "0";
                    string offintime = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dt.Rows[i]["offintime"].ToString()).ToString("hh:mm:ss tt");
                    string offoutime = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dt.Rows[i]["offouttime"].ToString()).ToString("hh:mm:ss tt");
                    string lnintime = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dt.Rows[i]["lnchintime"].ToString()).ToString("hh:mm:ss tt");
                    string lnoutime = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(dt.Rows[i]["lnchouttime"].ToString()).ToString("hh:mm:ss tt");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIME", dayid, empid, machid, idcardno, intime, outtime, leave, absent, dedout, addhour, addoffhour, offintime, offoutime, lnintime, lnoutime);
                }

                if (absent == "A")
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string frmdate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                    string absfl = "1";
                    string month = Convert.ToDateTime(this.txtdate.Text).ToString("ddMMyyyy").Substring(2, 2);
                    //tring month1 = month.PadLeft(2, '0');
                    string year = ASTUtility.Right(Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy"), 4);
                    string monyr = month + year;

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, frmdate, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");

                }

            }

            string msg = "Upload Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

             
            this.LoadGrid();





        }


        protected void gvDailyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvDailyAttn.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["DailyAttendence"];
            int TblRowIndex;
            for (int i = 0; i < this.gvDailyAttn.Rows.Count; i++)
            {
                string offintime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvoffIntime")).Text.Trim();
                string offouttime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvoffouttime")).Text.Trim();
                string intime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvIntime")).Text.Trim();
                string outime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvOuttime")).Text.Trim();
                string lnintime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvlnintime")).Text.Trim();
                string lnouttime = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvlnouttime")).Text.Trim();
                string leave = ((Label)this.gvDailyAttn.Rows[i].FindControl("lblgvLeave")).Text.Trim();
                string absent = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvAbsent")).Text.Trim();
                string dedout = Convert.ToDouble("0" + ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvDed")).Text.Trim()).ToString();
                string Addhour = Convert.ToDouble("0" + ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvAddHour")).Text.Trim()).ToString();
                TblRowIndex = (gvDailyAttn.PageIndex) * gvDailyAttn.PageSize + i;

                dt.Rows[TblRowIndex]["offintime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + offintime;
                dt.Rows[TblRowIndex]["offouttime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + offouttime;
                dt.Rows[TblRowIndex]["intime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + intime;
                dt.Rows[TblRowIndex]["outtime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + outime;
                dt.Rows[TblRowIndex]["lnchintime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + lnintime;
                dt.Rows[TblRowIndex]["lnchouttime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + lnouttime;
                dt.Rows[TblRowIndex]["leave"] = leave;
                dt.Rows[TblRowIndex]["absnt"] = absent;
                dt.Rows[TblRowIndex]["dedout"] = dedout;
                dt.Rows[TblRowIndex]["addhour"] = Addhour;

            }
            Session["DailyAttendence"] = dt;


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        protected void gvDailyAttn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt = (DataTable)Session["DailyAttendence"];

            string comcod = this.GetCompCode();
            //string date = this.ddlProjectName.SelectedValue.ToString();
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
            string EmpID = ((Label)this.gvDailyAttn.Rows[e.RowIndex].FindControl("lblgvEmpId")).Text.Trim();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "DELETE_MANUALENTRY", EmpID, dayid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            //bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEUNITENTRY", EmpID, dayid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvDailyAttn.PageSize) * (this.gvDailyAttn.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();


            }

            DataView dv = dt.DefaultView;
            //dv.RowFilter = ("empid<>''");
            //this.gvAnalysis.DataSource = dv.ToTable();
            //this.gvAnalysis.DataBind();
            //Session.Remove("tblActAna1");
            Session["DailyAttendence"] = dv.ToTable();
            this.LoadGrid();

        }

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void radioAttType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
