using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class AIinterface : System.Web.UI.Page
    {
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                //this.txtcreateDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                

                this.txtfrmdate.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                this.GetEmplist();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                

                //this.getAllData();

                this.TaskSteps.SelectedIndex = 0;
                this.TaskSteps_SelectedIndexChanged(null, null);



            }
        }

        private void GetEmplist()
        {
            string comcod = this.GetCompCode();
            string txtEmpname = "%%";
            string type = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    type = "lnemp";
                    break;
                default:
                    type = "";
                    break;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString() ?? "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, type, "", "", "", "", "", "", "");
            



        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

       

        protected void TasktState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TaskSteps_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void AddCustomerModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddCustomer();", true);
        }

        protected void tblAddProjectModal_Click(object sender, EventArgs e)
        {


            //string srttime = System.DateTime.Now.ToLocalTime().ToString();
            this.tblstarttime.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm"); 

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddProject();", true);
        }

        protected void tblTaskCreateModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddTask();", true);
        }
    }
}