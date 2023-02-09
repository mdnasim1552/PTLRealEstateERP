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
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class EntryAllEmp : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string ctype = this.Request.QueryString["Type"].ToString();
                string title = "";
                if (ctype == "EmpMarket")
                {

                    this.ShowEmployee();
                    this.GetEmployeeName();
                    title = "Entry All Employee Marketing ";
                    this.pnlplanemp.Visible = false;
                    this.pnlmarketemp.Visible = true;
                }

                else if (ctype == "EmpPlan")
                {
                    this.GetEmployeeNamePlan();
                    this.ShowEmployeePlan();
                    title = "Entry All Employee Planning ";
                    this.pnlplanemp.Visible = true;
                    this.pnlmarketemp.Visible = false;

                }


                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = title;




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

            Session["tblempinfo"] = dv.ToTable();
        }

        private void ShowEmployee()
        {
            string comcod = this.GetCompCode();
            //string empid = this.ddlEmpName.SelectedValue;
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETSALEMP", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblsalemp"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsalemp"];
            this.gvEmpSal.DataSource = dt;
            this.gvEmpSal.DataBind();
        }



        protected void lnkbtnOk_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsalemp"];
            string empid = this.ddlEmpName.SelectedValue;
            string empname = this.ddlEmpName.SelectedItem.ToString();
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            DataTable dt1 = (DataTable)Session["tblempinfo"];
            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = this.GetCompCode();
                dr1["empid"] = this.ddlEmpName.SelectedValue;
                dr1["empname"] = this.ddlEmpName.SelectedItem;
                dr1["desig"] = (dt1.Select("empid='" + empid + "'"))[0]["desig"].ToString();
                dr1["section"] = (dt1.Select("empid='" + empid + "'"))[0]["secdesc"];
                dr1["idcardno"] = (dt1.Select("empid='" + empid + "'"))[0]["idcardno"];
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
            string comcod = this.GetCompCode();
            DataTable dt1 = (DataTable)Session["tblsalemp"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt1);
            ds1.Tables[0].TableName = "tbl1";
            bool result = false;
            string xml = ds1.GetXml();
            result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTUPDATESALEMP", ds1, null, null, "", "", "", "", "", "", "", "", "",
           "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                string Message = HRData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

                return;
            }
            string Messages = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messages + "');", true);

        }

        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            string Message;
            DataTable dt1 = ((DataTable)Session["tblsalemp"]).Copy();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = dt1.Rows[index]["empid"].ToString();
            string comcod = this.GetCompCode();
            bool result;

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETESALEMP", empid, "", "", "", "", "", "", "", "");
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

        private void ShowEmployeePlan()
        {
            string comcod = this.GetCompCode();
            //string empid = this.ddlEmpName.SelectedValue;
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETPLANEMP", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblplanemp"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void GetEmployeeNamePlan()
        {

            string comcod = this.GetCompCode();
            // string IdCard = "%" + this.txtSrcEmpCode.Text.Trim () + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblpempinfo"] = ds1.Tables[0];
        }

        private void Data_BindPlan()
        {
            DataTable dt = (DataTable)Session["tblplanemp"];
            this.gvEmpPlan.DataSource = dt;
            this.gvEmpPlan.DataBind();
        }

        protected void lnkbtnOkPlan_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblplanemp"];
            string empid = this.ddlEmpName.SelectedValue;
            string empname = this.ddlEmpName.SelectedItem.ToString();

            DataRow[] dr = dt.Select("empid='" + empid + "'");
            DataTable dt1 = (DataTable)Session["tblpempinfo"];
            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = this.GetCompCode();
                dr1["empid"] = this.ddlEmpName.SelectedValue;
                dr1["empname"] = this.ddlEmpName.SelectedItem;
                dr1["desig"] = (dt1.Select("empid='" + empid + "'"))[0]["desig"].ToString();
                dr1["section"] = (dt1.Select("empid='" + empid + "'"))[0]["secdesc"];
                dr1["idcardno"] = (dt1.Select("empid='" + empid + "'"))[0]["idcardno"];
                dt.Rows.Add(dr1);

            }
            else
            {
                string Message = "Already Added Employee : " + empname;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

            }

            Session["tblplanemp"] = dt;
            this.Data_BindPlan();
        }

        protected void btndeletep_Click(object sender, EventArgs e)
        {

        }

        protected void lbntUpdateOtherDedp_Click(object sender, EventArgs e)
        {

        }
    
    }
}