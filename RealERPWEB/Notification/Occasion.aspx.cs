using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.Notification
{
    public partial class Occasion : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01" + date.Substring(2);
                this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Occasions";
                this.GetOccasion();

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private void GetOccasion()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();
            string curDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "RPT_PROSPECT_OCCASION", curDate , "", "", "", "", "","","","","");
            if (ds1 == null)
                return;
            Session["tbloccasion"] = ds1.Tables[0];
            this.gvOccasion.DataSource = ds1.Tables[0];
            this.gvOccasion.DataBind();
        }
    }
}