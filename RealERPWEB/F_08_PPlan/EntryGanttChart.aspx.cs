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
    public partial class EntryGanttChart : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT COMPLETION INFORMATION VIEW/EDIT";

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetProjectName();



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }




        private void LoadResources()
        {
            DayPilotScheduler1.Resources.Clear();


            Session.Remove("tblptarget");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROTARBDATE", pactcode, "", "", "", "", "", "", "", "");

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
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROTARBDATE", pactcode, "", "", "", "", "", "", "", "");

            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
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
        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string srchTxt = this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "GETPROJETNAME", srchTxt, "", "", "", "", "", "", "", "");
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            ds1.Dispose();
        }






        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblcost"];
            string PrjName = this.ddlProject.SelectedItem.Text.Substring(13);

            ReportDocument rrs1 = new RealERPRPT.R_32_Mis.RptProjectGraph();
            TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            rpttxtprjname.Text = PrjName;
            //TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtprjname.Text = PrjName;
            //TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtprjname.Text = PrjName;
            //TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtprjname.Text = PrjName;

            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);


            Session["Report1"] = rrs1;




            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
                this.ddlProject.Visible = false;
                this.lblProjectDesc.Visible = true;
                this.PnlColoumn.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.GetProStartAEndDate();
                this.ShowPtarget();

                return;
            }

            this.lbtnOk.Text = "Ok";
            this.ddlProject.Visible = true;
            this.lblProjectDesc.Visible = false;
            this.PnlColoumn.Visible = false;
            this.lblStartDate.Text = "";
            this.lblEndDate.Text = "";
            this.lblDuration.Text = "";
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;


        }


        private void GetProStartAEndDate()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROSTDATEAENDDATE", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.lblStartDate.Text = "";
                this.lblEndDate.Text = "";
                this.lblDuration.Text = Convert.ToInt32(ds1.Tables[0].Rows[0]["duration"]).ToString("#,#0;(#,#0); ") + " Month";
                return;
            }
            if (ds1.Tables[0].Rows.Count > 0)
            {

                this.lblStartDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["prjstrtdat"]).ToString("dd-MMM-yyyy");
                this.lblEndDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["prjenddat"]).ToString("dd-MMM-yyyy");
                this.lblDuration.Text = Convert.ToInt32(ds1.Tables[0].Rows[0]["duration"]).ToString("#,#0;(#,#0); ") + " Month";
            }


        }
        private void ShowPtarget()
        {


            //string comcod = this.GetCompCode();
            //string pactcode = this.ddlProject.SelectedValue.ToString();
            //DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROTARBDATE", pactcode, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{

            //    this.lblStartDate.Text = "";
            //    this.lblEndDate.Text = "";
            //    this.lblDuration.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["duration"]).ToString("#,#0;(#,#0); ") + " Month";
            //    return;
            //}
            //if (ds1.Tables[0].Rows.Count > 0)
            //{

            //    this.lblStartDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjstrtdat"]).ToString("dd-MMM-yyyy");
            //    this.lblEndDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjenddat"]).ToString("dd-MMM-yyyy");
            //    this.lblDuration.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["duration"]).ToString("#,#0;(#,#0); ") + " Month";
            //}

            //this.DayPilotScheduler1.StartDate = new DateTime(DateTime.Today.Year, 1, 1);
            //this.DayPilotScheduler1.Days = 365; //Days(DateTime.Today.Year);
            ////this.DayPilotScheduler1.DataStartField = "startdate";
            ////this.DayPilotScheduler1.DataEndField = "enddate";
            ////this.DayPilotScheduler1.DataTextField = "isirdesc";
            ////this.DayPilotScheduler1.DataIdField = "id";

            //this.DayPilotScheduler1.DataSource = ds1.Tables[0];
            //this.DayPilotScheduler1.SetScrollX(DateTime.Today);

            DateTime startdate, enddate;
            startdate = (this.lblStartDate.Text.Length) > 0 ? Convert.ToDateTime(this.lblStartDate.Text) : new DateTime(DateTime.Today.Year, 1, 1);
            enddate = (this.lblEndDate.Text.Length) > 0 ? Convert.ToDateTime(this.lblEndDate.Text) : new DateTime(DateTime.Today.Year, 1, 1);
            int datediff = ASTUtility.Datediffday(enddate, startdate);

            DayPilotScheduler1.StartDate = (this.lblStartDate.Text.Length) > 0 ? Convert.ToDateTime(this.lblStartDate.Text) : new DateTime(DateTime.Today.Year, 1, 1);
            DayPilotScheduler1.Days = (datediff > 0) ? datediff : 365; //Days(DateTime.Today.Year);
            DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
            DayPilotScheduler1.DataBind();

            DayPilotScheduler1.SetScrollX(DateTime.Today);







        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string isircode = dt1.Rows[0]["isircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["isircode"].ToString() == isircode)
                {
                    isircode = dt1.Rows[j]["isircode"].ToString();
                    dt1.Rows[j]["isirdesc"] = "";

                }


                isircode = dt1.Rows[j]["isircode"].ToString();

            }

            return dt1;

        }




        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblptarget"];
            this.DayPilotScheduler1.DataStartField = "startdate";
            this.DayPilotScheduler1.DataEndField = "enddate";
            this.DayPilotScheduler1.DataTextField = "isirdesc";
            this.DayPilotScheduler1.DataIdField = "isircode";
            this.DayPilotScheduler1.DataResourceField = "isircode";

            this.DayPilotScheduler1.DataSource = dt;
            this.DayPilotScheduler1.DataBind();
            this.DayPilotScheduler1.Update();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}