using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using CrystalDecisions.ReportSource;
//using ASITWEBRPT;
namespace RealERPWEB
{

    public partial class Clients_List1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblTitle")).Text = "Few Of our Clients";
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //ReportDocument rrs1 = new RptTecUse();


            //Session["Report1"] = rrs1;
            //lbljavascript.Text = @"<script>window.open('RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}