using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_22_Sal
{
    public partial class ClusterSetup : System.Web.UI.Page
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
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string ctype = this.Request.QueryString["Type"].ToString();
                string title = "";
                this.ShowEmployee();
                this.GetEmployeeName();
                title = "Cluster Setup ";
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = title;
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
            // string IdCard = "%" + this.txtSrcEmpCode.Text.Trim () + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataView dv = new DataView();
            dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("seccode not like '9402%'");


            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = dv.ToTable();
            this.ddlEmpName.DataBind();

            this.ddlEmpName1.DataTextField = "empname";
            this.ddlEmpName1.DataValueField = "empid";
            this.ddlEmpName1.DataSource = dv.ToTable();
            this.ddlEmpName1.DataBind();

            Session["tblempinfo"] = dv.ToTable();
        }

        private void ShowEmployee()
        {
            string comcod = this.GetCompCode();
            //string empid = this.ddlEmpName.SelectedValue;
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCLUSTEREMP", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblsalemp"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsalemp"];
            this.gvEmpCluster.DataSource = dt;
            this.gvEmpCluster.DataBind();
        }



        protected void lnkbtnOk_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsalemp"];
            string empid = this.ddlEmpName.SelectedValue;
            string empname = this.ddlEmpName.SelectedItem.ToString();
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            DataTable dt1 = (DataTable)Session["tblempinfo"];
            string clusterId = this.ddlEmpName1.SelectedValue.ToString();
            string clusterName = this.ddlEmpName1.SelectedItem.Text;

            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = this.GetCompCode();
                dr1["empid"] = this.ddlEmpName.SelectedValue;
                dr1["empname"] = this.ddlEmpName.SelectedItem;
                dr1["desig"] = (dt1.Select("empid='" + empid + "'"))[0]["desig"].ToString();
                dr1["section"] = (dt1.Select("empid='" + empid + "'"))[0]["secdesc"];
                dr1["idcardno"] = (dt1.Select("empid='" + empid + "'"))[0]["idcardno"];
                dr1["clusterid"] = clusterId;
                dr1["clustername"] = clusterName;
                dt.Rows.Add(dr1);
            }
            else
            {
                string Message = "Already Added Employee : " + empname;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

            }

            Session["tblsalemp"] = dt;
            this.Data_Bind();
        }

        protected void lbntUpdateOtherDed_OnClick(object sender, EventArgs e)
        {
            this.SaveValue();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblsalemp"];


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string clusterid = dt.Rows[i]["clusterid"].ToString();

                bool result = true;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTUPDATECLUSTEREMP", empid, clusterid, "", "", "", "", "", "", "");

                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                else
                {

                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }


        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblsalemp"];
            int TblRowIndex;
            for (int i = 0; i < this.gvEmpCluster.Rows.Count; i++)
            {
                string lblempid = ((Label)this.gvEmpCluster.Rows[i].FindControl("lblempid")).Text.Trim().ToString();
                string lblclusterid = ((Label)this.gvEmpCluster.Rows[i].FindControl("lblgvclusterid")).Text.Trim().ToString();

                TblRowIndex = (gvEmpCluster.PageIndex) * gvEmpCluster.PageSize + i;

                dt.Rows[TblRowIndex]["empid"] = lblempid;
                dt.Rows[TblRowIndex]["clusterid"] = lblclusterid;
            }
            Session["tblsalemp"] = dt;
        }



        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            string Message;
            DataTable dt1 = ((DataTable)Session["tblsalemp"]).Copy();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = dt1.Rows[index]["empid"].ToString();
            string clusterid = dt1.Rows[index]["clusterid"].ToString();

            string comcod = this.GetCompCode();
            bool result;

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETECLUSTEREMP", empid, clusterid, "", "", "", "", "", "", "");
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
            dv.RowFilter = ("empid not like '" + empid + "%'");
            Session["tblsalemp"] = dv.ToTable();
            this.Data_Bind();

        }

      
       
       

     

        protected void btndeletep_Click(object sender, EventArgs e)
        {

        }

        protected void lbntUpdateOtherDedp_Click(object sender, EventArgs e)
        {

        }
    }
}