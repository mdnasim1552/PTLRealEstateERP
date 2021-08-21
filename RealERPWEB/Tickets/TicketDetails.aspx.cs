using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.Tickets
{
    public partial class TicketDetails : System.Web.UI.Page
    {
        ProcessAccess _linkVendorDb = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.GetTicketDetails();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetTicketDetails()
        {
            string comcod = GetCompCode();
            string ticketID = this.Request.QueryString["TicketId"].ToString();
            DataSet ds1 = _linkVendorDb.GetTicketDataByID(comcod,ticketID);
            if (ds1 == null)
                return;
            assignEngName.InnerText = ds1.Tables[0].Rows[0]["assignuname"].ToString();
            companyName.InnerText = ds1.Tables[0].Rows[0]["compname"].ToString();
            creatDate.InnerText = Convert.ToDateTime(ds1.Tables[0].Rows[0]["createtask"].ToString()).ToString("dd-MMM-yyyy");
            ticketDesc.InnerText = ds1.Tables[0].Rows[0]["taskdesc"].ToString();
        }
    }
}