using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_04_Bgd
{
    public partial class AsitDevlop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = this.txtname.Text;

        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            string name = this.txtname.Text;
        }
    }
}