using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_34_Mgt
{
    public partial class Trigger : System.Web.UI.Page
    {

        ProcessAccess objTrigger = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = "Trigger Information";
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                DateTime date = System.DateTime.Today;
                DateTime frmdate = Convert.ToDateTime("01" + date.ToString("dd-MMM-yyyy").Substring(2));
                this.txtDatefrom.Text = frmdate.ToString("dd-MMM-yyyy");
                this.txtDateto.Text = frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");



            }
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {

            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                DateTime date = System.DateTime.Today;
                string frmdate = this.txtDatefrom.Text;
                string todate = this.txtDateto.Text;
                bool result = objTrigger.UpdateTransInfoTrigger("SP_REPORT_SALSMGTXML", "RPTDATEWALLPROINSDUES", frmdate, "", todate, "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = objTrigger.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }

            catch (Exception ex)
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


            }







        }

    }
}