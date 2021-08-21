using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_04_Bgd
{
    public partial class ASITClientDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

            //    this.GetInitialiaze();


        }

        private void GetInitialiaze()
        {
            string date = ((HiddenField)this.Master.FindControl("hdate")).Value;

        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {

            string date = ((HiddenField)this.Master.FindControl("hdate")).Value;
        }
    }
}