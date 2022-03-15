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
                ((Label)this.Master.FindControl("lblTitle")).Text = "OCCASIONS";
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
            string empId = this.Request.QueryString["EmpId"].ToString();
            string curDate = this.Request.QueryString["curDate"].ToString();
            DataSet ds1 = accData.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE", "RPT_PROSPECT_OCCASION", null, null, null, curDate, empId, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tbloccasion"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbloccasion"];
            
            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod like '0306003%'"); //Birthday
            this.gvBirthday.DataSource = dv.ToTable();
            this.gvBirthday.DataBind();

            DataView dv1 = dt.Copy().DefaultView;
            dv1.RowFilter = ("gcod like '0306005%'"); //Marriage day
            this.gvMarriageday.DataSource = dv1.ToTable();
            this.gvMarriageday.DataBind();
        }
    }
}