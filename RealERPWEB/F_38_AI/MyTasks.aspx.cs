using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_38_AI
{
    public partial class MyTasks : System.Web.UI.Page
    {
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "My Tasks";
                btnMyTasks_SelectedIndexChanged(null, null);
            }
        }

        protected void btnMyTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.btnMyTasks.SelectedValue.ToString();
            switch (value)
            {
                case "1":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "2":
                    this.MultiView1.ActiveViewIndex = 1;
                    GetAIInterface();
                    break;
                case "3":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "4":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetAIInterface()
        {
            string comcod = this.GetCompCode();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETINTERFACE", "", "", "", "", "", "");
            if (ds == null)
                return;

            Session["tblprojectlist"] = ds.Tables[0];
            Session["tblassinglist"] = ds.Tables[1];
            this.data_Bind();
        }
        private void data_Bind()
        {
            
            DataTable tblasing = (DataTable)Session["tblassinglist"];
             
            this.gvAssingJob.DataSource = tblasing;
            this.gvAssingJob.DataBind();

        }
    }
}