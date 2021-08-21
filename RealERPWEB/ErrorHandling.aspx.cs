using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB
{
    public partial class ErrorHandling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "db")
            {
                this.lblDbError.Visible = true;
            }
            else
            {
                this.lblSssion.Visible = true;
            }
        }
    }
}