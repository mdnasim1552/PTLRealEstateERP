using RealERPLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_14_Pro
{
    public partial class RptPurchesStatusPrjWise : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (dr1.Length == 0)
                //    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Status Report";
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(Date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            }

        }
    }
}