using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Net.Mail;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_08_PPlan
{
    public partial class Default : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                // this.GetProjectName();

                LoadResources();

                DayPilotScheduler1.StartDate = new DateTime(DateTime.Today.Year, 1, 1);
                DayPilotScheduler1.Days = 365;// Year.Days(DateTime.Today.Year);
                DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
                DayPilotScheduler1.DataBind();
                DayPilotScheduler1.SetScrollX(DateTime.Today);


            }
        }




        private void LoadResources()
        {
            DayPilotScheduler1.Resources.Clear();


            Session.Remove("tblptarget");
            string comcod = this.GetCompCode();
            //string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROTARBDATE", "", "", "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                string name = (string)r["name"];
                string id = Convert.ToString(r["id"]);

                DayPilotScheduler1.Resources.Add(name, id);
            }
        }


        protected void DayPilotScheduler1_EventMove(object sender, DayPilot.Web.Ui.Events.EventMoveEventArgs e)
        {
            string id = e.Value;
            DateTime start = e.NewStart;
            DateTime end = e.NewEnd;
            string resource = e.NewResource;
            DbUpdateEvent(id, start, end, resource);
            DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
            DayPilotScheduler1.DataBind();
            DayPilotScheduler1.Update();
        }
        private DataTable DbGetEvents(DateTime start, int days)
        {
            //SqlDataAdapter da = new SqlDataAdapter("SELECT [id], [name], [eventstart], [eventend], [resource_id] FROM [event] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))", ConfigurationManager.ConnectionStrings["DayPilot"].ConnectionString);
            //da.SelectCommand.Parameters.AddWithValue("start", start);
            //da.SelectCommand.Parameters.AddWithValue("end", start.AddDays(days));
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //return dt;


            Session.Remove("tblptarget");
            string comcod = this.GetCompCode();
            // string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROTARBDATE", "", "", "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];
            return dt;

        }
        private void DbUpdateEvent(string id, DateTime start, DateTime end, string resource)
        {
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DayPilot"].ConnectionString))
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand("UPDATE [event] SET eventstart = @start, eventend = @end, resource_id = @resource WHERE id = @id", con);
            //    cmd.Parameters.AddWithValue("id", id);
            //    cmd.Parameters.AddWithValue("start", start);
            //    cmd.Parameters.AddWithValue("end", end);
            //    cmd.Parameters.AddWithValue("resource", resource);
            //    cmd.ExecuteNonQuery();
            //}
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
    }
}