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
using RealEntity;
namespace RealERPWEB.F_35_MgtAct
{

    public partial class RptMisOverall : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();

        UserManMgtAct objMgtact = new UserManMgtAct();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALES OPENING INFORMATION VIEW/EDIT";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ShowMisOverall();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowMisOverall();
        }


        private void ShowMisOverall()
        {

            string Date = this.txtDate.Text;
            List<RealEntity.F_35_MgtAct.EClassMisOverall> lst = objMgtact.ShowMgtOverall(Date);
            this.Data_Bind(lst);
        }

        private void Data_Bind(List<RealEntity.F_35_MgtAct.EClassMisOverall> lst)
        {

            this.gvMisOverall.DataSource = lst;
            this.gvMisOverall.DataBind();
        }







        protected void lbtnPrint_Click(object sender, EventArgs e)
        {






        }



    }
}