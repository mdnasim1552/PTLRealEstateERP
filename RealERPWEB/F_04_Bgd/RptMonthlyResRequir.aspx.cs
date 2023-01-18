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
namespace RealERPWEB.F_04_Bgd
{
    public partial class RptMonthlyResRequir : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Resource Requirement";


                this.GetProjectName();
                this.txtDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetProjectName()
        {

            string comcod = this.GetCompCode();

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PROJECTTARGET", "GETPROJETNAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }

        private void LoadData()
        {
            string comcod = this.GetCompCode();
            string prjcode = this.ddlProjectName.SelectedValue;
            string date = this.txtDate.Text;
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS_01", "RPTRESREQUIRMENT", prjcode, date, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvRptResBasis.DataSource = ds1.Tables[0];
            this.gvRptResBasis.DataBind();
            this.gvmatreq.DataSource = ds1.Tables[1];
            this.gvmatreq.DataBind();


        }
        protected void lbtOk_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        protected void gvmatreq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rsircod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();

                //Label des = (Label)e.Row.FindControl("lgvResDesbgdvsac");
                //Label bgdamt = (Label)e.Row.FindControl("lgvresamtbgdvsac");
                //Label acamt = (Label)e.Row.FindControl("lgvatualamt");


                if (rsircod.Substring(2) == "0000000000")
                {
                    //des.Attributes["style"] = "font-weight:bold;color:blue";
                    //bgdamt.Attributes["style"] = "font-weight:bold;color:blue";
                    //acamt.Attributes["style"] = "font-weight:bold;color:blue";
                    e.Row.BackColor = System.Drawing.Color.PowderBlue;

                }
                if (rsircod == "AAAAAAAAAAAA")
                {
                    e.Row.ForeColor = System.Drawing.Color.Maroon;
                    e.Row.Attributes["style"] = "font-weight:bold";

                }


            }
        }
    }
}