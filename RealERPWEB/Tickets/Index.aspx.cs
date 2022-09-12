using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.Tickets
{
    public partial class Index : System.Web.UI.Page
    {

        ProcessAccess _linkVendorDb = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkUser();
            }
        }
        protected void lnkbtnCreateTicket_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "RedirTicketCreate();", true);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void checkUser()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userId = hst["usrid"].ToString();
            string comcod = GetCompCode();
            DataSet ds1 = _linkVendorDb.GetcheckUser(comcod, userId);
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                string Url1 = "RequestTicketId";
                Response.Redirect(Url1);
            }
            else
            {
                Session["TicketUseId"] = ds1.Tables[0].Rows[0]["USERID"].ToString();
                this.Data_Bind();
            }


        }
        private void Data_Bind()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();

            DataSet ds1 = _linkVendorDb.GetTicketData(comcod);
            if (ds1 == null)
                return;
            this.grvacc.DataSource = ds1.Tables[0];
            this.grvacc.DataBind();

            //RowFilter taskprogress== 99204
            DataTable dt = ds1.Tables[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("taskprogress='"+ "99204" +"'");
            DataTable tblQC = dv.ToTable();

            this.grvQC.DataSource = tblQC;
            this.grvQC.DataBind();

            //RowFilter taskprogress==99209
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("taskprogress='"+ "99209" +"'");
            DataTable tblComplete = dv.ToTable();

            this.grvComplete.DataSource = tblComplete;
            this.grvComplete.DataBind();
        }

        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void editTicket_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDeleteTicket_Click(object sender, EventArgs e)
        {

        }

        protected void TickQcDoneEng_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
            {
                Response.Redirect("~/Tickets/Index.aspx");
                return;
            }
            string cdate = System.DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");

            string createuser = Session["TicketUseId"].ToString();

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            int index = this.grvQC.PageSize * this.grvQC.PageIndex + RowIndex;
            string comcod = "";
            string ticketId = ((Label)this.grvQC.Rows[RowIndex].FindControl("lbltaskid")).Text.Trim();
            string ticketType = ((Label)this.grvQC.Rows[RowIndex].FindControl("lbltasktypecode")).Text.Trim();
            bool resultb = _linkVendorDb.UpdateTicket(comcod, createuser, ticketType, cdate, ticketId, "99209", "");
            if (!resultb)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail();", true);
                return;
            }
            else
            {
                string msg = "Task Done send QC Dpt";


                string eventtype = "2";
                string eventdesc = msg;
                string eventdesc2 = ticketId + ".- Description: " + msg;
                bool IsVoucherSaved = CALogRecord.AddLogRecord("", ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                this.Data_Bind();
            }
        }

        protected void lnkForwardAll_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
            {
                Response.Redirect("~/Tickets/Index.aspx");
                return;
            }
            string cdate = System.DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");

            string createuser = Session["TicketUseId"].ToString();


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            int index = this.grvQC.PageSize * this.grvQC.PageIndex + RowIndex;
            string comcod = "";
            string ticketId = ((Label)this.grvQC.Rows[RowIndex].FindControl("lbltaskid")).Text.Trim();
            string ticketType = ((Label)this.grvQC.Rows[RowIndex].FindControl("lbltasktypecode")).Text.Trim();
            bool resultb = _linkVendorDb.UpdateTicket(comcod, createuser, ticketType, cdate, ticketId, "99204", "Back");
            if (!resultb)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail();", true);
                return;
            }
            else
            {
                string msg = "Task Done send QC Dpt";


                string eventtype = "2";
                string eventdesc = msg;
                string eventdesc2 = ticketId + ".- Description: " + msg;
                bool IsVoucherSaved = CALogRecord.AddLogRecord("", ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                this.Data_Bind();
            }
        }
    }
}