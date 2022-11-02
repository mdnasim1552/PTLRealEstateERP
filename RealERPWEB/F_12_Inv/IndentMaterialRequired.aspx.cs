using RealERPLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_12_Inv
{
    public partial class IndentMaterialRequired : System.Web.UI.Page
    {
        ProcessAccess dbaccess = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


            }

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlResSpcf_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}