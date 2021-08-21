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
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using RealERPLIB;
using RealEntity;
namespace RealERPWEB.F_46_GrMgtInter
{

    [ScriptService]
    public partial class GrpDashBoardAll : System.Web.UI.Page
    {
        //static UserManSales objUserSalService = new UserManSales();
        static RealEntity.C_34_Mgt.SalPurAccComboManager objUserPurService = new RealEntity.C_34_Mgt.SalPurAccComboManager();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "DASHBOARD " + this.Request.QueryString["Desc"].ToString();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDatefrom.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        //private string GetCompCode()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    return (hst["comcod"].ToString());

        //}

        //protected void lbtnOk_Click(object sender, EventArgs e)
        //{
        //    this.lblMon.Visible = true;
        //    this.lblMon.Visible = true;
        //    //this.GetMonthly();
        //}

        // [WebMethod]
        // public static List<RealEntity.C_34_Mgt.SalPurAccCombo> GetMonthlySalCal(string selectedDate)
        //{
        //    List<RealEntity.C_34_Mgt.SalPurAccCombo> lst1 = new List<RealEntity.C_34_Mgt.SalPurAccCombo>();
        //    try
        //    {
        //        lst1 = objUserPurService.GetSalPurAccByMon(selectedDate);
        //        return lst1;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        lst1 = null;
        //    }

        //}

        public string GetCompCode()
        {

            return (Request.QueryString["comcod"].ToString());

        }



    }
}