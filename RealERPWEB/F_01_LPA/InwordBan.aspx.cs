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
namespace RealERPWEB.F_01_LPA
{
    public partial class InwordBan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(this.txtamt.Text);

            this.lblinword.Text = "কথাঁয় : " + Inword.Trans((amount), 2);

        }

    }
}