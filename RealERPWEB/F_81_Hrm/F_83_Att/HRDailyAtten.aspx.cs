using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Services;

using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_83_Att
{


    public partial class HRDailyAtten : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                Session.Remove("DayAtten");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ComVisibility();
            }
        }
        private void ComVisibility()
        {          
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                    this.chktype.Visible = true;
                    break;
                default:
                    this.chktype.Visible = false;
                    break;
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        public void GetDataSet()
        {
            bool result;
            OleDbConnection conn;
            string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
            string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
            string con = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =\\asitlaptopp\rims\Database\RAS.mdb; Jet OLEDB:Database Password=ras258";
            conn = new OleDbConnection(con);
            conn.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter("Select din, clock from checkinout where Clock between " + date1 + " and " + date2, conn);
            adapter.Fill(ds);
            Session["DayAtten"] = ds.Tables[0];
            conn.Close();
            DataTable dt = (DataTable)Session["DayAtten"]; 
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string idcardno1 = dt.Rows[i]["din"].ToString();
                string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);
            this.ShowData();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comid = dt1.Rows[0]["comid"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comid"].ToString() == comid)
                {
                    comid = dt1.Rows[j]["comid"].ToString();
                    dt1.Rows[j]["comname"] = "";
                }
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }
                else
                {
                    comid = dt1.Rows[j]["comid"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;

        }
        private void LoadGrid()
        {

            this.gvDailyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDailyAttn.DataSource = (DataTable)Session["ShowAtten"]; ;
            this.gvDailyAttn.DataBind();
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["ShowAtten"];
            int TblRowIndex;
            for (int i = 0; i < this.gvDailyAttn.Rows.Count; i++)
            {

                string intime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvIntime")).Text.Trim();
                string outime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvOuttime")).Text.Trim();
                TblRowIndex = (gvDailyAttn.PageIndex) * gvDailyAttn.PageSize + i;
                outime = (outime == "") ? intime : outime;
                dt.Rows[TblRowIndex]["intime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + intime;
                dt.Rows[TblRowIndex]["outtime"] = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + " " + outime;
            }
            Session["ShowAtten"] = dt;
        }
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            // ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            bool result;
            this.SaveValue();
            DataTable dt = (DataTable)Session["ShowAtten"];
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
            //result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEOFFTIME", dayid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string absent = dt.Rows[i]["absnt"].ToString().Trim();
                //string leave = dt.Rows[i]["leave"].ToString().Trim();
                //if ((absent != "A") && (leave != "L"))
                //{
                string empid = dt.Rows[i]["empid"].ToString();
                string machid = "01";
                string idcardno = dt.Rows[i]["idcardno"].ToString();
                string intime = dt.Rows[i]["intime"].ToString();
                string outtime = dt.Rows[i]["outtime"].ToString();
                string offintime = dt.Rows[i]["offintime"].ToString();
                string offoutime = dt.Rows[i]["offouttime"].ToString();
                string lnintime = dt.Rows[i]["lnchintime"].ToString();
                string lnoutime = dt.Rows[i]["lnchouttime"].ToString();
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIMEAUTO", dayid, empid, machid, idcardno, intime, outtime, offintime, offoutime, lnintime, lnoutime, "", "", "", "", "");
                // }
                //if (absent == "A")
                //{
                //    string empid = dt.Rows[i]["empid"].ToString();
                //    string frmdate = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy");
                //    string absfl = "1";
                //    string month = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("ddMMyyyy").Substring(2, 2);
                //    //tring month1 = month.PadLeft(2, '0');
                //    string year = ASTUtility.Right(Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy"), 4);
                //    string monyr = month + year;
                //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INORUPDATEABSENTCT", empid, frmdate, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");
                //}
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);
            //this.LoadGrid();
        }
        private void GetTBLAttnLogData()
        {
            Session.Remove("ShowAtten");
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "SHOWEMPATTEN", "", "", date, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;
            }
            Session["ShowAtten"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }
        private void ShowData()
        {
            Session.Remove("ShowAtten");
            string comcod = this.GetCompCode();
            string date = this.txtdate.Text;
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "SHOWEMPATTEN", "", "", date, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;
            }
            Session["ShowAtten"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }
        protected void gvDailyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvDailyAttn.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3353":
                    this.InsertDailyAttnManama();
                    break;
                case "4305":
                    this.InsertDailyAttnRup();
                    break;
                case "3348":
                case "3101":
                    this.InsertDailyAttnCredence();
                    break;
                case "4301":
                    this.InsertDailyAttnSan();
                    break;
                case "3333":
                case "3336":

                case "3338": //Acme Technologies Ltd.
                case "1206": //Acme Construction
                case "1207": //Acme Service

                case "3330": // Bridge
                case "3355": // Greenwood
                case "3347": // Peb Steel              
                             // case "3353": // Greenwood
                    this.InsertDailyAttnAlliance();
                    break;

                case "3354": // edidison real
                    this.GetTBLAttnLogData();
                    break;
                case "3315"://Assure
                    this.InsertDailyAttnAssure();
                    break;
                case "3365"://
                    this.GetDailyAttenDanceZKT();
                    break;
                default:
                    this.InsertDailyAttnAlliance();
                    break;
            }
            //Web Referecne
            //SystemProcessAccess prodata = new SystemProcessAccess();
            //try
            //{
            //    Session.Remove("DayAtten");
            //    bool result;
            //    string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
            //    string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
            //    string cmd = "Select din, clock from ras_AttRecord where Clock between " + date1 + " and " + date2;
            //    //string cmd= "select comcod, comnam, comsnam,  comadd1+'<br />'+comadd2+' '+comadd3 as comadd, comadd1,comadd2, comadd3, comadd4  from compinf order by comcod asc";
            //    DataSet ds = prodata.GetDailyAttenDance(cmd);
            //    if (ds == null)
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = prodata.ErrorObject["Msg"].ToString();
            //        return;
            //    }
            //      Session["DayAtten"] = ds.Tables[0];
            //    DataTable dt = (DataTable)Session["DayAtten"];
            //    string comcod = this.GetCompCode();
            //    string date = this.txtdate.Text;
            //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string idcardno1 = dt.Rows[i]["din"].ToString();
            //        string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
            //        string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            //        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
            //    }
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //    this.ShowData();
            //}
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        private void InsertDailyAttnRup()
        {
            try
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                bool result;
                string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
              
                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                DataSet ds = DailyAttendance.GetDailyAttenDance(date1, date2);
                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idcardno1 = dt.Rows[i]["din"].ToString();
                    string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upload Successfully');", true);
                // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                this.ShowData();
            }
            catch (Exception ex)
            {
            }
        }
        private void InsertDailyAttnManama()
        {
            //if (chktype.Checked == true)
            //{
            //    this.GetAccessAtteDataAssure();
            //}
            //else
            //{
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                //HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                //string hello = DailyAttendance.HelloWorld();
                //return;
                Session.Remove("DayAtten");
                bool result;
                string pdate = Convert.ToDateTime(this.txtdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");
                string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                //  DataSet ds = DailyAttendance.GetDailyAttenDanceCredence(date1, date2);
                DataSet ds = DailyAttendance.GetDailyAttenDanceManama(date1, date2);
                //  string count = DailyAttendance.Country();
                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idcardno1 = dt.Rows[i]["din"].ToString();
                    string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                }
                string Msg = "Upload Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true); 
                // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                this.ShowData();
            }
            catch (Exception ex)
            {       
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);           
                return;
            }
            //}
        }
        private void InsertDailyAttnCredence()
        {
            //if (chktype.Checked == true)
            //{
            //    this.GetAccessAtteDataAssure();
            //}
            //else
            //{
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                //HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                //string hello = DailyAttendance.HelloWorld();
                //return;
                Session.Remove("DayAtten");
                bool result;
                string pdate = Convert.ToDateTime(this.txtdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");
                string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                //  DataSet ds = DailyAttendance.GetDailyAttenDanceCredence(date1, date2);
                DataSet ds = DailyAttendance.GetDailyAttenDanceCredence(date1, date2);
                //  string count = DailyAttendance.Country();
                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idcardno1 = dt.Rows[i]["din"].ToString();
                    string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                }
                string msg = "Upload Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                this.ShowData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;
            }
            //}
        }
        private void PreviousDayAutoUpdate()
        {
            try
            {
                //((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                bool result;
                string pdate = Convert.ToDateTime(this.txtdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");
                string date1 = pdate + " 12:00:00 AM";
                string date2 = pdate + " 11:59:00 PM";
                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                DataSet ds = DailyAttendance.GetDailyAttenDance02(date1, date2);
                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                // Get EmpIdCard
                DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETEMPIDCARD", "", "", "", "", "", "", "", "", "");
                if (ds4 == null) return;
                DataTable dtcrdno = ds4.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idcardno1 = dt.Rows[i]["din"].ToString();

                    DataRow[] dr = dtcrdno.Select("scardno='" + idcardno1 + "'");

                    if (dr.Length > 0)
                        idcardno1 = dr[0]["idcardno"].ToString();
                    else
                        continue;
                    string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
                // All Data Update
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSORUPPREEMPOFFTIME", "", "", pdate, "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);
                //this.ShowData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }
        private void InsertDailyAttnSan()
        {
            try
            {
               // ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                //Previous Day Update
                this.PreviousDayAutoUpdate();
                bool result;
                string date1 = this.txtdate.Text + " 12:00:00 AM";
                string date2 = this.txtdate.Text + " 11:59:00 PM";
                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                DataSet ds = DailyAttendance.GetDailyAttenDance02(date1, date2);
                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                // Get EmpIdCard
                DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETEMPIDCARD", "", "", "", "", "", "", "", "", "");
                if (ds4 == null) return;
                DataTable dtcrdno = ds4.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idcardno1 = dt.Rows[i]["din"].ToString();
                    DataRow[] dr = dtcrdno.Select("scardno='" + idcardno1 + "'");
                    if (dr.Length > 0)
                        idcardno1 = dr[0]["idcardno"].ToString();
                    else
                        continue;
                    string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upload Successfully');", true);
                this.ShowData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        private void InsertDailyAttnAlliance()
        {
            try
            {
                //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                bool result;
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;
                //string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                //string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
                //HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                //DataSet ds = DailyAttendance.GetDailyAttenDanceAlli(date1, date2);
                DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETDATAFORUPLOAD", date, "", "", "", "", "", "", "", "");
                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idcardno = dt.Rows[i]["din"].ToString();
                    //string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upload Successfully');", true);
                this.ShowData();
            }
            catch (Exception ex)
            {
            }
        }
        private void InsertDailyAttnAssure()
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                if (chktype.Checked == true)
                {
                    this.GetAccessAtteDataAssure();
                }
                else
                {
                    //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    Session.Remove("DayAtten");
                    bool result;
                    string comcod = this.GetCompCode();
                    string date = this.txtdate.Text;
                    //string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                    //string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";
                    //HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                    //DataSet ds = DailyAttendance.GetDailyAttenDanceAlli(date1, date2);
                    DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETDATAFORUPLOADASSURE", date, "", "", "", "", "", "", "", "");
                    Session["DayAtten"] = ds.Tables[0];
                    DataTable dt = (DataTable)Session["DayAtten"];
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string idcardno = dt.Rows[i]["din"].ToString();
                        //string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                        string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upload Successfully');", true);
                    this.ShowData();
                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error in exception";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void GetAccessAtteDataAssure()
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                bool result;
                string pdate = Convert.ToDateTime(this.txtdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");

                string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";

                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                DataSet ds = DailyAttendance.GetDailyAttenDanceAssure(date1, date2);
                //DataSet ds = DailyAttendance.GetDailyAttenDanceAssure(date1, date2);
                //string count = DailyAttendance.Country();

                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];
                string comcod = this.GetCompCode();
                string date = this.txtdate.Text;

                // result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEATTEN", date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idcardno1 = dt.Rows[i]["din"].ToString();
                    string idcardno = ASTUtility.Right(("000000" + idcardno1.Trim()), 6);
                    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upload Successfully');", true);
                // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                this.ShowData();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error in exception";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void GetDailyAttenDanceZKT()
        {
            try
            {
                string comcod = this.GetCompCode();
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Session.Remove("DayAtten");
                bool result;
                string pdate = Convert.ToDateTime(this.txtdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");

                string date1 = "#" + this.txtdate.Text + " 12:00:00 AM" + "#";
                string date2 = "#" + this.txtdate.Text + " 11:59:00 PM" + "#";

                //PtlComClass obj = new PtlComClass(comcod);
                //string mrno = obj.mrfno;
               // List<PtlComClass> lst = new List<PtlComClass>(comcod);
                HrWebService.HrDailyAtten DailyAttendance = new HrWebService.HrDailyAtten();
                DataSet ds = DailyAttendance.GetDailyAttenDanceZKT(date1, date2);
                //DataSet ds = DailyAttendance.GetDailyAttenDanceAssure(date1, date2);
                //string count = DailyAttendance.Country();
                Session["DayAtten"] = ds.Tables[0];
                DataTable dt = (DataTable)Session["DayAtten"];              
                string date = this.txtdate.Text;      
                DataSet ds1 = new DataSet("ds1");
                ds1.Merge(dt);
                ds1.Tables[0].TableName = "dt1";
                //string xml = ds1.GetXml();
                result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTENZKT", ds1, null, null, date, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    string msg = HRData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + msg + "');", true);
                    return;
                }
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string idcardno1 = dt.Rows[i]["din"].ToString();
                //    string idcardno = ASTUtility.Right(("00000" + idcardno1.Trim()), 5);
                //    string intime = Convert.ToDateTime(dt.Rows[i]["clock"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTUPDATEATTEN", idcardno, date, intime, "", "", "", "", "", "", "", "", "", "", "", "");
                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upload Successfully');", true);
                // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";         
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error in exception";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }  
    }
}
