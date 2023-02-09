using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class LandEmployeeEntry : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetEmployeeName();
                this.ShowEmployee();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Land Employee Entry";
            }            
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetEmployeeName()
        {
            string comcod = GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAME", "%", "", "", "", "", "", "", "", "","");
            if (ds1 == null)
                return;

            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds1.Tables[0];
            this.ddlEmployee.DataBind();
            Session["tblempinfo"] = ds1.Tables[0];
        }

        private void ShowEmployee()
        {
            string comcod = GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETSALEMPLAND", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblsalemp"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsalemp"];
            this.gvEmpSalLand.DataSource = dt;
            this.gvEmpSalLand.DataBind();
            
        }

        protected void lnkBtnOk_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsalemp"];
            string empid = this.ddlEmployee.SelectedValue;
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            DataTable dt1 = (DataTable)Session["tblempinfo"];
            if(dr.Length==0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = GetCompCode();
                dr1["empid"] = this.ddlEmployee.SelectedValue;
                dr1["empname"] = this.ddlEmployee.SelectedItem;
                dr1["desig"] = (dt1.Select("empid='" + empid + "'"))[0]["desig"].ToString();
                dr1["section"] = (dt1.Select("empid='" + empid + "'"))[0]["secdesc"].ToString();
                dr1["idcardno"] = (dt1.Select("empid='" + empid + "'"))[0]["idcardno"].ToString();
                dt.Rows.Add(dr1);
            }
            Session["tblsalemp"] = dt;
            this.Data_Bind();
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)Session["tblsalemp"]).Copy();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = dt.Rows[index]["empid"].ToString();
            string comcod = GetCompCode();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETESALEMPLAND", empid, "", "", "", "", "", "", "", "", "","","","","","");
            if (!result)
                return;

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("empid not like '" + empid + "%'");
            Session["tblsalemp"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void lbntUpdateOtherDed_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            DataTable dt1 = (DataTable)Session["tblsalemp"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt1);
            ds1.Tables[0].TableName = "tbl1";
            string xmlData = ds1.GetXml();
            bool result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTUPDATESALEMPLAND", ds1, null, null, "", "", "", "", "", "", "", "", "",
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
    }
}