using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class EntryPFBenefit : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

           
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfrmdate.Text = "01-Jan-" + date.Substring(7);
                //this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(12).ToString("dd-MMM-yyyy");
                this.GetCompany();
                this.GetYear();




            }

        }

        private void GetYear()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETYEAR", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyear.DataTextField = "year1";
            this.ddlyear.DataValueField = "year1";
            this.ddlyear.DataSource = ds1.Tables[0];
            this.ddlyear.DataBind();
            this.ddlyear.SelectedValue = System.DateTime.Today.Year.ToString();
            ds1.Dispose();
        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }

        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetDeptName();
        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }

        private void GetDeptName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = "%%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }
        private void SectionName()
        {

            string comcod = this.GetCompCode();
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblprofund");
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            string frmdate = "01-jan-" + ddlyear.SelectedValue.ToString();
            string todate = "31-Dec-" + ddlyear.SelectedValue.ToString();

            string CompanyName = ((this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string projectcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 8)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "SHOWEMPBENEFIT", CompanyName, projectcode, section, frmdate, todate, "", "", "", "");
            if (ds1 == null)
            {
                this.gvProFund.DataSource = null;
                this.gvProFund.DataBind();
                return;
            }

            Session["tblprofund"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }


        private void Data_Bind()
        {
            this.gvProFund.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvProFund.DataSource = (DataTable)Session["tblprofund"];
            this.gvProFund.DataBind();
            this.FooterCalCulation((DataTable)Session["tblprofund"]);


        }
        private void FooterCalCulation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvProFund.FooterRow.FindControl("lgvFBen")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(benamt)", "")) ? 0.00 : dt.Compute("sum(benamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProFund.FooterRow.FindControl("lgvFPrinciple")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");
            Session["Report1"] = gvProFund;
            ((HyperLink)this.gvProFund.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string deptid = dt1.Rows[0]["section"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == deptid)
                {

                    dt1.Rows[j]["sectionname"] = "";

                }

                deptid = dt1.Rows[j]["section"].ToString();

            }

            return dt1;

        }

        private void SaveVulue()
        {
            DataTable dt = (DataTable)Session["tblprofund"];
            double interamt = Convert.ToDouble("0" + this.txtInterest.Text.Trim());
            double topriciple = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", "")));
            double benamt, pclamt, percent;
            for (int i = 0; i < this.gvProFund.Rows.Count; i++)
            {

                pclamt = Convert.ToDouble("0" + ((Label)this.gvProFund.Rows[i].FindControl("lblgvPrinciple")).Text.Trim());
                benamt = Convert.ToDouble("0" + ((Label)this.gvProFund.Rows[i].FindControl("lblgvBenefit")).Text.Trim());

                int rowindex = (this.gvProFund.PageSize) * (this.gvProFund.PageIndex) + i;
                // dt.Rows[rowindex]["toam"] = pclamt;        
                percent = Math.Round((pclamt * interamt) / topriciple, 2);
                dt.Rows[i]["benamt"] = percent;

            }
            Session["tblprofund"] = dt;

        }

        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            this.SaveVulue();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tblprofund"];
            string comcod = this.GetComeCode();
            string year = this.ddlyear.SelectedValue.ToString();

            string insamt = this.txtInterest.Text;
            string msg = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string empid = dt.Rows[i]["empid"].ToString();

                double benamt = Convert.ToDouble(dt.Rows[i]["benamt"].ToString());
                double pclamt = Convert.ToDouble(dt.Rows[i]["toam"].ToString());

                bool resultb = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEBENEFIT", year, empid, benamt.ToString(), pclamt.ToString(), insamt,
                            "", "", "", "", "", "", "", "", "", "");

                if (!resultb)
                {

                    msg = "Update Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                }



            }

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);





        }


        protected void lbtnCal_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblprofund"];
            double interamt = Convert.ToDouble("0" + this.txtInterest.Text.Trim());
            double topriciple = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", "")));
            double benamt, pclamt, percent;
            foreach (DataRow dr1 in dt.Rows)
            {
                pclamt = Convert.ToDouble(dr1["toam"]);
                percent = Convert.ToDouble((pclamt * interamt) / topriciple);
                dr1["benamt"] = percent.ToString("#,##0.00; -#,##0.00");

            }

            Session["tblprofund"] = dt;
            this.Data_Bind();
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvProFund_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProFund.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}