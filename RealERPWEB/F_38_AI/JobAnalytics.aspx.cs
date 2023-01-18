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
    public partial class JobAnalytics : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = "Job Analytics";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ProjectCount();
            }
        }

        private void ProjectCount()
        {
            try
            {
                string comcod = this.GetComdCode();
                string pid = Request.QueryString["PID"].ToString();

                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI ", "GETPROJECTDASHBOARD", pid, "", "", "", "", "");
                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                    return;


                //dashboard counting (rakib)
                string doninstnace = ds1.Tables[0].Rows[0]["doninstnace"].ToString() ?? "";
                string attinstance = ds1.Tables[0].Rows[0]["attinstance"].ToString() ?? "";
                string qaspent = ds1.Tables[0].Rows[0]["qaspent"].ToString() ?? "";
                string annotspent = ds1.Tables[0].Rows[0]["annotspent"].ToString() ?? "";
                string adminspnt = ds1.Tables[0].Rows[0]["annotspent"].ToString() ?? "";
                string ttlskip = ds1.Tables[0].Rows[0]["annotspent"].ToString() ?? "";
                this.doninstnace.InnerText = doninstnace;
                this.attinstance.InnerText = attinstance;
                this.qaspent.InnerText = qaspent;
                this.annotspent.InnerText = annotspent;
                this.adminspnt.InnerText = adminspnt;
                this.ttlskip.InnerText = ttlskip;

                //project details(rakib)
                 this.lblprjname.Text =ds1.Tables[1].Rows[0]["projectName"].ToString() ?? "";
                 this.lblprjtype.Text =ds1.Tables[1].Rows[0]["typedesc"].ToString() ?? "";
                 this.lblwktype.Text= ds1.Tables[1].Rows[0]["worktype"].ToString() ?? "";
                 this.lblcreatedat.Text= ds1.Tables[1].Rows[0]["createdate"].ToString() ?? "";
                 this.lblqty.Text= ds1.Tables[1].Rows[0]["quantity"].ToString() ?? "";
                 this.lblcusname.Text= ds1.Tables[1].Rows[0]["empname"].ToString() ?? "";

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
          private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
    }
}