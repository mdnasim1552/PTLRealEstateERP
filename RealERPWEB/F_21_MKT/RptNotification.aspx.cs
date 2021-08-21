using RealERPLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;
namespace RealERPWEB.F_21_MKT
{

    public partial class RptNotification : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetNotificationinfo();
                this.GetTitleName();
            }
        }



        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetNotificationinfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usertype = hst["usrrmrk"].ToString();
            string comcod = this.GetComeCode();
            string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            string flouby = this.Request.QueryString["Type"].ToString();
            DataSet ds3 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETCOUNTNOTIDEATILS", "8301%", Empid, flouby, "", "", "", "");

            if (ds3 == null)
            {
                return;
            }


            this.gvSummary.DataSource = ds3.Tables[0];//ds3.Tables[0];//
            this.gvSummary.DataBind();



        }

        private void GetTitleName()
        {

            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {


                case "Call":// Credence 
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Call Information";
                    break;
                case "visit":// Credence 
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Visit Information";
                    break;
                case "Others":// Credence 
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Others Information";
                    break;
                case "Dpass":// Credence 
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Day Passed Information";
                    break;
                case "Commnts":// Credence 
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Comments Information";
                    break;
                case "Frezz":// Credence 
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Frezzing Prospect Information";
                    break;


                default:

                    break;

            }
        }
    }
}