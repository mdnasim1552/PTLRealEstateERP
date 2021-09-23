using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class MktTeamMember : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.GetTeamCode();
                this.ShowTeamMember();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Team Member";
            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetTeamCode()
        {
            string comcod = GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETTEAM", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlTeam.DataTextField = "teamdesc";
            this.ddlTeam.DataValueField = "teamcode";
            this.ddlTeam.DataSource = ds1.Tables[0];
            this.ddlTeam.DataBind();
            Session["tblTeam"] = ds1.Tables[0];
        }

        private void ShowTeamMember()
        {
            
        }

        protected void lnkBtnOk_Click(object sender, EventArgs e)
        {
            if (this.lnkBtnOk.Text == "Ok")
            {
                this.lnkBtnOk.Text = "NEW";
                this.ddlTeamMember.Visible = true;
                this.lnkBtnAdd.Visible = true;
                this.ShowTeamMember();
            }
            else
            {
                this.lnkBtnOk.Text = "Ok";
                this.ddlTeamMember.Visible = false;
                this.lnkBtnAdd.Visible = false;
                this.gvTeamMember.DataSource = null;
                this.gvTeamMember.DataBind();
            }

        }
    }
}