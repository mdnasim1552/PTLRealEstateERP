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
//using  RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_21_MKT
{
    public partial class ProspectTransferLog : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        private UserManagerKPI objUser = new UserManagerKPI();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                 

                ((Label)this.Master.FindControl("lblTitle")).Text = "Transfer Client Information";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string txtDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01-" + Convert.ToDateTime(txtDate).ToString("MMM-yyyy");
                this.txtTdate.Text = Convert.ToDateTime(this.txtFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                 
                this.GetClientInfo();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
           // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }



        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetClientInfo()
        {
            string frmdate = this.txtFdate.Text;
            string todate = this.txtTdate.Text;
            string comcod = this.Getcomcod();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "PROSPECT_TRANSFER_LOG", frmdate, todate, "", "", "", "","","","","");
           if(ds1==null)
            {
                return;
            }
            DataTable dt1 = ds1.Tables[0];
            ViewState["clientinfo"] = dt1;
            this.Data_Bind();
        }
        protected void Data_Bind()
        {
            DataTable dt1 = (DataTable)ViewState["clientinfo"];
            this.gvtransLog.DataSource = dt1;
            this.gvtransLog.DataBind();
        }

        protected void lnkbtnOK_Click(object sender, EventArgs e)
        {
            string frmdate = this.txtFdate.Text;
            string todate = this.txtTdate.Text;

            ViewState.Remove("clientinfo");
                this.GetClientInfo();
            
        }




    }
}
