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
using System.IO;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_33_Doc
{
    public partial class SimCardMange : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                this.getAllData();
                this.GetEmp();
                this.GetMobile();

            }
        }


        private void getAllData()
        {
            string comcod = this.GetCompCode();
        
         
            this.simtype.SelectedIndex = 0;
            gvdoc.Columns[2].HeaderText = "Issued Date";
            DataSet ds = HRData.GetTransInfo(comcod, "SP_ENTRY_SIM", "ALL_SIM_USER", "ALL", "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                this.gvdoc.DataSource = null;
                this.gvdoc.DataBind();
                return;
            }
            DataTable dt = ds.Tables[0];
           
            DataView dv = dt.DefaultView;
            dv.RowFilter = "isactive='true' and isassign='true' and isreturn='false'";
            this.gvdoc.DataSource = dv;
            this.gvdoc.DataBind();
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetEmp()
        {
            string comcod = this.GetCompCode();
            DataSet ds = HRData.GetTransInfo(comcod, "SP_ENTRY_SIM", "GET_ALL_ACTIVE_EMP", "", "", "", "", "", "", "", "", "");
            if (ds == null)
                return;

            this.ddlEmp.DataTextField = "empname";
            this.ddlEmp.DataValueField = "empid";
            this.ddlEmp.DataSource = ds.Tables[0];
            this.ddlEmp.DataBind();
        }

        private void GetMobile()
        {
            string comcod = this.GetCompCode();
            DataSet ds = HRData.GetTransInfo(comcod, "SP_ENTRY_SIM", "ALL_SIM", "ACTIVE", "", "", "", "", "", "", "", "");
            if (ds == null)
                return;

            this.ddlMobile.DataTextField = "mobileno";
            this.ddlMobile.DataValueField = "id";
            this.ddlMobile.DataSource = ds.Tables[0];
            this.ddlMobile.DataBind();


        }

        protected void simtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int index = this.simtype.SelectedIndex;
 
            DataView dv = new DataView();

            DataSet ds = HRData.GetTransInfo(comcod, "SP_ENTRY_SIM", "ALL_SIM_USER", "ALL", "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            DataTable dt = ds.Tables[0];
            switch (index)
            {
                case 0:
                    gvdoc.Columns[2].HeaderText = "Issued Date";
 

                    dv = dt.DefaultView;
                    dv.RowFilter = "isactive='true' and isassign='true' and isreturn='false'";
                    this.gvdoc.DataSource = dv;
                    this.gvdoc.DataBind();
                    break;

                case 1:
                    gvdoc.Columns[2].HeaderText = "Returned Date";
   

                    dv = dt.DefaultView;
                    dv.RowFilter = "isactive='false' and isassign='false' and isreturn='true'";
                    this.gvdoc.DataSource = dv;
                    this.gvdoc.DataBind();
                    break;
            }

        }

        protected void lnk_save_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string remarks = "";
            string msg = "";
            string empid = this.ddlEmp.SelectedValue.ToString();
            string simid = this.ddlMobile.SelectedValue.ToString();
            string empname = "";


            if (chckguest.Checked)
            {
                empid = "";
                empname = this.txtguest.Text;

            }
            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SIM", "ENTRY_SIMUSER", empid, "true", remarks, simid, empname, "", "", "", "");
            if (result)
            {
                msg = "Saved Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                this.GetEmp();
                this.GetMobile();
                this.getAllData();
            }
            else
            {
                msg = "Insert Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
       
            }

        }


        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMobile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void btn_remove_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string msg = "";
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = ((Label)this.gvdoc.Rows[index].FindControl("lblempid")).Text.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SIM", "REMOVE_SIM_USER", empid, "", "", "", "", "", "", "", "");
            if (result)
            {
                msg = "Deleted Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                this.GetEmp();
                this.GetMobile();
                this.getAllData();
            }
            else
            {
                msg = "Delete Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                this.getAllData();
            }

        }

        protected void lnkreturn_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string msg = "";
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = ((Label)this.gvdoc.Rows[index].FindControl("lblempid")).Text.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SIM", "RETURN_SIM", empid, "", "", "", "", "", "", "", "");
            if (result)
            {
                msg = "Returned Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                this.GetEmp();
                this.GetMobile();
                this.getAllData();
            }
        }

        protected void gvdoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int index = this.simtype.SelectedIndex;
            if (index ==0)
            {
                ((LinkButton)e.Row.FindControl("lnkreturn")).Enabled = true;

     
            }
            else if(index ==1)
            {
                ((LinkButton)e.Row.FindControl("lnkreturn")).Enabled = false;

            }
           
        }

        protected void chckguest_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chckguest.Checked)
            {
                this.pnlemp.Visible = false;
                this.pnlguest.Visible = true;
            }
            else
            {
                this.pnlemp.Visible = true;
                this.pnlguest.Visible = false;
            }
        }
    }
}