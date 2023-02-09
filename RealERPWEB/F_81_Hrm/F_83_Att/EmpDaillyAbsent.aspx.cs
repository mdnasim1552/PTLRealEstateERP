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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class EmpDaillyAbsent : System.Web.UI.Page
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

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompany();


                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                lnkbtnShow_Click(null,null);
            }
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETCOMPANYNAMEIALL", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            ds1.Dispose();

        }





        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void ibtnFindCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }


        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmpAbsent();

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.txtDate.Enabled = false;
                this.ddlCompanyName.Enabled = false;
                this.lblCompanyName.Enabled = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.lnkbtnShow.Text = "New";
                this.lblCompanyName.Text = this.ddlCompanyName.SelectedItem.Text;
                this.GetEmpAbsent();
                return;
            }

            this.txtDate.Enabled = true;
            this.ddlCompanyName.Visible = true;
            this.lblCompanyName.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvempabsent.DataSource = null;
            this.gvempabsent.DataBind();
            this.lnkbtnShow.Text = "Ok";
            this.lblCompanyName.Text = "";

        }

        private void GetEmpAbsent()
        {

            Session.Remove("tblempabsent");
            string comcod = this.GetCompCode();
            string date = this.txtDate.Text.Trim();
            string company = ((this.ddlCompanyName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2)) + "%";
            string monthid = this.txtDate.Text.Trim();
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETEMPABSENT", date, company, Empcode, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvempabsent.DataSource = null;
                this.gvempabsent.DataBind();
                return;
            }
            Session["tblempabsent"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
            ds2.Dispose();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string deptid = dt1.Rows[0]["deptid"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptid"].ToString() == deptid && dt1.Rows[j]["secid"].ToString() == secid)
                {
                    deptid = dt1.Rows[j]["deptid"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["deptid"] = "";
                    dt1.Rows[j]["secid"] = "";
                    dt1.Rows[j]["deptdesc"] = "";
                    dt1.Rows[j]["section"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["deptid"].ToString() == deptid)
                        dt1.Rows[j]["deptdesc"] = "";
                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["section"] = "";


                    deptid = dt1.Rows[j]["deptid"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;
        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblempabsent"];
            this.gvempabsent.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvempabsent.DataSource = dt;
            this.gvempabsent.DataBind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }



        protected void lbtnFiUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tblempabsent"];
            string comcod = this.GetCompCode();
            string Monthid = Convert.ToDateTime(this.txtDate.Text).ToString("MMyyyy"); //year
            string date = this.txtDate.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString(); //empid
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, date, "1", Monthid, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }


      ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }
        protected void gvempabsent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvempabsent.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

    }
}