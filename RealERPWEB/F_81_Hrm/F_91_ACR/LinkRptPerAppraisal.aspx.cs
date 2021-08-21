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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_91_ACR
{
    public partial class LinkRptPerAppraisal : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE  PERFORMANCE REMARKS";
                this.ShowEmpNarration();


            }

        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }




        private void ShowEmpNarration()
        {
            Session.Remove("tblper");
            string comcod = this.GetComeCode();
            string empid = this.Request.QueryString["empid"].ToString();
            string Date1 = this.Request.QueryString["Date1"].ToString();
            string Date2 = this.Request.QueryString["Date2"].ToString();



            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ACR_EMPLOYEE", "RPTEMPNARRATIONDETAILS", empid, Date1, Date2, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpper.DataSource = null;
                this.gvEmpper.DataBind();
                return;
            }
            Session["tblper"] = ds2.Tables[0];
            this.gvEmpper.DataSource = ds2.Tables[0];
            this.gvEmpper.DataBind();

        }


        //txtDate







    }
}