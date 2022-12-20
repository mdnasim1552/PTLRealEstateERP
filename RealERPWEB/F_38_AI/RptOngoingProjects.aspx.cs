using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_38_AI
{
    public partial class RptOngoingProjects : System.Web.UI.Page
    {
        ProcessAccess AIData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (dr1.Length == 0)
                //    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "On Going Projects";
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(Date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            }
        }

        private string GetCompCode()
        {
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
           
        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                string fromdat = this.txtfrmdate.Text;
                string todat = this.txttodate.Text;
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETAIONGOINPROJECTDETAILS", fromdat, todat, "", "", "", "");
                if (ds == null)
                    return;

                Session["tblongoingproject"] = ds.Tables[0];
                this.gv_OngoingProject.DataSource = ds;
                this.gv_OngoingProject.DataBind();



            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        //for ongoing project print
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();               
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comsnam = hst["comsnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string session = hst["session"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                DataTable ds =(DataTable)Session["tblongoingproject"];
                LocalReport Rpt1 = new LocalReport();
                var lst = ds.DataTableToList<RealEntity.C_38_AI.AIallPrint.RptOngoingProject>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_38_AI.RptOngoingProjectPrint", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "ONGOING PROJECTS"));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("date", "From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() ));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
    }
}