using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB
{
    public partial class DefaultPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;


                this.landpanal.Visible = (this.Request.QueryString["Type"] == "landpro");
                this.pnlintrland.Visible = (this.Request.QueryString["Type"] == "landpro");

                this.bgpanal.Visible = (this.Request.QueryString["Type"] == "bgpanal");
                this.pnlIntrbudget.Visible = (this.Request.QueryString["Type"] == "bgpanal");

                this.pnlConst.Visible = (this.Request.QueryString["Type"] == "pnlConst");
                this.pnlintrConst.Visible = (this.Request.QueryString["Type"] == "pnlConst");


                this.pnlsales.Visible = (this.Request.QueryString["Type"] == "pnlsales");
                this.pnlintrsales.Visible = (this.Request.QueryString["Type"] == "pnlsales");

                this.plncr.Visible = (this.Request.QueryString["Type"] == "plncr");
                this.plnintrcr.Visible = (this.Request.QueryString["Type"] == "plncr");

                this.pnlModification.Visible = (this.Request.QueryString["Type"] == "pnlModification");
                this.pnlintrModification.Visible = (this.Request.QueryString["Type"] == "pnlModification");

                this.pnlpur.Visible = (this.Request.QueryString["Type"] == "pnlpur");
                this.pnlintrpur.Visible = (this.Request.QueryString["Type"] == "pnlpur");

                this.pnlInv.Visible = (this.Request.QueryString["Type"] == "pnlInv");
                this.pnlintrInv.Visible = (this.Request.QueryString["Type"] == "pnlInv");

                this.pnlAcc.Visible = (this.Request.QueryString["Type"] == "pnlAcc");
                this.pnlintrAcc.Visible = (this.Request.QueryString["Type"] == "pnlAcc");

            }
        }

        private void ComProfile()
        {
            //string comcod = this.GetCompCode();
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "COMPANYPROFILE", "", "", "", "", "", "", "", "", "");

            //this.lblCompName.Text = ds1.Tables[0].Rows[0]["comnam"].ToString();
            //this.lblAddress.Text = ds1.Tables[0].Rows[0]["comadd1"].ToString();
            //this.lblphone1.Text = ds1.Tables[0].Rows[0]["comadd2"].ToString();
            //this.lblemail.Text = ds1.Tables[0].Rows[0]["comadd3"].ToString().Replace("E-mail:", "");
            //this.lblweb.Text = ds1.Tables[0].Rows[0]["comadd4"].ToString().Replace("Web:", "");
            //this.lblAccPeriod.Text = "From " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["sdate"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["edate"]).ToString("dd-MMM-yyyy");


            //string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void lblGrp_Click(object sender, EventArgs e)
        {
            this.ComProfile();
        }
    }
}