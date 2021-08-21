using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RealERPLIB;
namespace RealERPWEB
{
    public partial class ASITDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Panel)this.Master.FindControl("pnlTitle")).Visible = false;
                ((Label)this.Master.FindControl("LblGrpCompany")).Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();// ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
                ((Label)this.Master.FindControl("lbladd")).Text = ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();
                ((Image)this.Master.FindControl("Image1")).ImageUrl = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";

            }
        }
    }
}