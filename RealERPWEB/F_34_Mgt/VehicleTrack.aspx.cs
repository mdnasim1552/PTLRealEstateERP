using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_34_Mgt
{
    public partial class VehicleTrack : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/F_34_Mgt/VehicleTrackEntry.aspx?Type=Add");
        }

    }
}