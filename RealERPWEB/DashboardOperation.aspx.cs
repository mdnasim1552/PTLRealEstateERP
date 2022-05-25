using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASITSTDWEB
{
    public partial class DashboardOperation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "ERP DASHBOARD";
            }

        }
    }
}