using RealERPLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class CRMDashboard03 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //DateTime curdate = System.DateTime.Today;
                //this.txtfrmdate.Text = Convert.ToDateTime("01-Jan-" + curdate.ToString("yyyy")).ToString("dd-MMM-yyyy");
                //this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");

                //this.GetAllSubdata();
                //this.GETEMPLOYEEUNDERSUPERVISED();
                //this.ModalDataBind();
                //this.GetComponentData();

            }
        }
    }
}