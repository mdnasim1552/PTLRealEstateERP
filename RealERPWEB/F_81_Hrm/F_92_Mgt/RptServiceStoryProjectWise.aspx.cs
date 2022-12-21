using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RptServiceStoryProjectWise : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
                Response.Redirect("../AcceessError.aspx");
            ((Label)this.Master.FindControl("lblTitle")).Text = "Service Story (Project Wise)";
        }
       
    }
}