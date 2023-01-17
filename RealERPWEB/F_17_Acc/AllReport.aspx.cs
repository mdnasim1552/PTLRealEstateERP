using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEntity;
namespace RealERPWEB.F_17_Acc
{

    public partial class AllReport : System.Web.UI.Page
    {
        ProcessAccess User = new ProcessAccess();
        UserManager userManager = new UserManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "All Account Reports";
                this.GetDataForRadioBtn();
            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetDataForRadioBtn()
        {

            string ModuleId = "17";
            string InputName = "04%";
            List<EClassModule> lst = userManager.ShowModule(ModuleId, InputName);

            this.rdallreport.DataSource = lst;
            this.rdallreport.DataTextField = "itemdesc";
            this.rdallreport.DataValueField = "itemurl";
            this.rdallreport.DataBind();
        }

        protected void rdallreport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = this.rdallreport.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openpage('../" + url + "', target='_blank');", true);
        }
    }
}