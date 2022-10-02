using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_38_AI
{
    public partial class MyTasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "My Tasks";
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
                    break;
                case "3":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "4":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

            }
        }
        

    }
}