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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class RptGrpMisDailyActiviteisJq : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        ProcessAccess GrpData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                SetCompCode();


                //this.chkConsolidate.Checked = true;

                //this.chkConsolidate_CheckedChanged(null, null);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Management Interface";
                //this.chkall.Checked = true;
                //this.chkall_CheckedChanged(null, null);
                CallCompanyList();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);



        }
        private void SetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            Session["comcod"] = (hst["comcod"].ToString());

        }


        protected void CallCompanyList()
        {
            string comcod = this.GetCompCode();
            string consolidate = this.chkConsolidate.Checked ? "Consolidate" : "";
            DataSet ds1 = this.GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = "comcod not like '0000%'";
            DataTable dt = dv.ToTable();
            this.ddlComCode.DataTextField = "comsnam";
            this.ddlComCode.DataValueField = "comcod";
            this.ddlComCode.DataSource = dt;
            this.ddlComCode.DataBind();

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }








    }
}
