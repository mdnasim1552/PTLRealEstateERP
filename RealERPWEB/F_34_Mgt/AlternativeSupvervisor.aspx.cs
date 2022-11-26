using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_34_Mgt
{
    public partial class AlternativeSupvervisor : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString(); 
                this.GetEmployeeName();
                this.ShowEmployee();



            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetEmployeeName()
        {

            string comcod = this.GetCompCode();           
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlEmpName.DataSource = null;
                this.ddlEmpName.DataBind();
                this.ddlaltEmpName.DataSource = null;
                this.ddlaltEmpName.DataBind();
                return;
            }
                

          


            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();

            this.ddlaltEmpName.DataTextField = "empname";
            this.ddlaltEmpName.DataValueField = "empid";
            this.ddlaltEmpName.DataSource = ds1.Tables[0];
            this.ddlaltEmpName.DataBind();
            Session["tblempinfo"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void ShowEmployee()
        {
            string comcod = this.GetCompCode();          
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETALTERNATIVESUPINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblsalemp"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblsalemp"];
                this.gvEmpCluster.DataSource = dt;
                this.gvEmpCluster.DataBind();
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);

            }
        }



        protected void lnkbtnOk_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsalemp"];
            string sempid = this.ddlEmpName.SelectedValue.ToString();
            string sempname = this.ddlaltEmpName.SelectedItem.Text;
            DataRow[] dr = dt.Select("sempid='" + sempid + "'");
            DataTable dt1 = (DataTable)Session["tblempinfo"];
            string asempid = this.ddlaltEmpName.SelectedValue.ToString();
            

            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = this.GetCompCode();
                dr1["sempid"] =sempid;
                dr1["sempname"] = (dt1.Select("empid='" + sempid + "'"))[0]["empname1"].ToString();
                dr1["sdesig"] = (dt1.Select("empid='" + sempid + "'"))[0]["desig"].ToString();
                dr1["ssection"] = (dt1.Select("empid='" + sempid + "'"))[0]["secdesc"];
                dr1["sidcardno"] = (dt1.Select("empid='" + sempid + "'"))[0]["idcardno"];


                dr1["asempid"] = asempid;
                dr1["asempname"] = (dt1.Select("empid='" + asempid + "'"))[0]["empname1"].ToString(); ;
                dr1["asdesig"] = (dt1.Select("empid='" + asempid + "'"))[0]["desig"].ToString();
                dr1["assection"] = (dt1.Select("empid='" + asempid + "'"))[0]["secdesc"];
                dr1["asidcardno"] = (dt1.Select("empid='" + asempid + "'"))[0]["idcardno"];


              
                dt.Rows.Add(dr1);
            }
            else
            {
                string Message = "Already Added Employee : " + sempname;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

            }

            Session["tblsalemp"] = dt;
            this.Data_Bind();
        }

        protected void lbntUpdateOtherDed_OnClick(object sender, EventArgs e)
        {
            
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblsalemp"];


            foreach (DataRow dr1 in dt.Rows)
            {
                string sempid = dr1["sempid"].ToString();
                string asempid = dr1["asempid"].ToString();

                bool result = true;
                result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTORUPDATEALTERSUPREVISOR", sempid, asempid, "", "", "", "", "", "", "");

                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
              
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }


       


        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            string Message;
            DataTable dt1 = ((DataTable)Session["tblsalemp"]).Copy();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sempid = dt1.Rows[index]["sempid"].ToString();
            string asempid = dt1.Rows[index]["asempid"].ToString();

            string comcod = this.GetCompCode();
            bool result;

            result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEALTERSUP", sempid, asempid, "", "", "", "", "", "", "");
            if (!result)
            {
                Message = "Deleted Fail ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

                return;
            }
            Message = "Deleted Succed ";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


            DataView dv = new DataView();
            dv = dt1.DefaultView;
            dv.RowFilter = ("sempid not like '" + sempid + "%'");
            Session["tblsalemp"] = dv.ToTable();
            this.Data_Bind();

        }

      
       
       

     

       

       
    }
}