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
                this.GetEmployee();
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

        private void GetEmployee()
        {
            string comcod = GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds1.Tables[0];
            this.ddlEmployee.DataBind();
            Session["tblempinfo"] = ds1.Tables[0];
        }

        private void ShowTeamMember()
        {
            string comcod = GetCompCode();
            string teamCode = this.ddlTeam.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "SHOWTEAMMEMBER", teamCode, "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblTeamMember"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblTeamMember"];
            this.gvTeamMember.DataSource = dt;
            this.gvTeamMember.DataBind();
        }

        protected void lnkBtnOk_Click(object sender, EventArgs e)
        {
            if (this.lnkBtnOk.Text == "Ok")
            {
                this.lnkBtnOk.Text = "NEW";
                this.ddlEmployee.Visible = true;
                this.lnkBtnAdd.Visible = true;
                this.ShowTeamMember();
            }
            else
            {
                this.lnkBtnOk.Text = "Ok";
                this.ddlEmployee.Visible = false;
                this.lnkBtnAdd.Visible = false;
                this.gvTeamMember.DataSource = null;
                this.gvTeamMember.DataBind();
            }

        }

        protected void lnkBtnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblTeamMember"];
            string empid = this.ddlEmployee.SelectedValue;
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            DataTable dt1 = (DataTable)Session["tblempinfo"];
            if(dr.Length==0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = GetCompCode();
                dr1["teamcode"] = this.ddlTeam.SelectedValue;
                dr1["empid"] = this.ddlEmployee.SelectedValue;
                dr1["empname"] = this.ddlEmployee.SelectedItem;
                dr1["desig"] = (dt1.Select("empid='" + empid + "'"))[0]["desig"].ToString();
                dr1["section"] = (dt1.Select("empid='" + empid + "'"))[0]["secdesc"].ToString();
                dr1["idcardno"] = (dt1.Select("empid='" + empid + "'"))[0]["idcardno"].ToString();
                dt.Rows.Add(dr1);
            }
            Session["tblTeamMember"] = dt;
            this.Data_Bind();
        }

        protected void lbntUpdateOtherDed_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            DataTable dt = (DataTable)Session["tblTeamMember"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            string xmdData = ds1.GetXml();
            bool result = HRData.UpdateXmlTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "INSERTUPDATETEAMMEMBER", ds1, null, null, "", "", "", "", "", "", "", "", "",
           "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            DataTable dt = ((DataTable)Session["tblTeamMember"]).Copy();
            GridViewRow row = (GridViewRow)((LinkButton)(sender)).NamingContainer;
            int rowIndex = row.RowIndex;
            string empId = dt.Rows[rowIndex]["empid"].ToString();
            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "DELETETEAMCODE", empId, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("empid not like '" + empId + "%'");
            Session["tblTeamMember"] = dv.ToTable();
            this.Data_Bind();
        }
    }
}