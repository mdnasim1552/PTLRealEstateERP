using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEntity;
using RealERPLIB;
namespace RealERPWEB.F_08_PPlan
{
    public partial class ScheduleVsPayment : System.Web.UI.Page
    {

        ProcessAccess ProjPlan = new ProcessAccess();
        UserProjPlan _ProjPlan = new UserProjPlan();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "SCHEDULE VS PAYMENT";
                //this.LoadPaymentDetails ();
                this.ibtnFindproj_OnClick(null, null);
            }
        }

        protected void ibtnFindresname_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

        }

        protected void ibtnFindproj_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchTxt = "%" + this.txtproj.Text.Trim() + "%";
            List<RealEntity.C_08_PPlan.E_CLassPaymetSch.ProjectName> lst = _ProjPlan.GetProject(comcod, srchTxt);
            this.ddlproj.Items.Clear();
            this.ddlproj.DataTextField = lst[0].actdesc;
            this.ddlproj.DataValueField = lst[0].actcode;
            this.ddlproj.DataSource = lst;
            this.ddlproj.DataBind();
        }
    }
}