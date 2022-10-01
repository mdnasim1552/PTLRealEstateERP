using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_38_AI
{
    public partial class Projects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Projects OverView";
            }

        }

        protected void ProjectDetails_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string value = this.ProjectDetails.SelectedValue.ToString();
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
                case "5":
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "6":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case "7":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;
                case "8":
                    this.MultiView1.ActiveViewIndex = 7;
                    break;
            }
        }
    }
}