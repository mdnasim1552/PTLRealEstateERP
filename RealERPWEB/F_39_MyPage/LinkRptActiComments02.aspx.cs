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
namespace RealERPWEB.F_39_MyPage
{
    public partial class LinkRptActiComments02 : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Activities Comments";

                this.lblvalprogram.Text = this.Request.QueryString["prono"].ToString();
                this.lblvalrefno.Text = this.Request.QueryString["refno"].ToString();
                this.lblvalactivities.Text = this.Request.QueryString["actdesc"].ToString();

                this.ShowData();
                //this.LoadGrid();

            }
        }






        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }








        private void ShowData()
        {


            string comcod = this.GetComdCode();

            string teamcode = this.Request.QueryString["empid"].ToString();
            string pactcode = this.Request.QueryString["prono"].ToString();
            string actcode = this.Request.QueryString["actcode"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "RPTEMPTRANS", "", teamcode, pactcode, actcode, "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            ds1.Dispose();


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }




    }
}



