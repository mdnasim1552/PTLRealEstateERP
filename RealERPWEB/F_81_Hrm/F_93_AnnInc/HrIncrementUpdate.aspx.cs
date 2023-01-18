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
namespace RealERPWEB.F_81_Hrm.F_93_AnnInc
{
    public partial class HrIncrementUpdate : System.Web.UI.Page
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
                this.txtdate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private void GetIncrementList()
        {

            string comcod = GetCompCode();
            string mREQDAT = this.GetStdDate(this.txtdate.Text);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENTNO", mREQDAT, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.ddlIncList.Items.Clear();
                return;

            }

            this.ddlIncList.DataTextField = "incrno1";
            this.ddlIncList.DataValueField = "incrno";
            this.ddlIncList.DataSource = ds2.Tables[0];
            this.ddlIncList.DataBind();

        }

        protected void imgbtnIncrementList_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
                this.GetIncrementList();
        }

        private string CompanyCalltype()
        {
            string callType = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3333":
                    // case "3101":
                    callType = "UPDATEINCSALARYALLI";
                    break;
                case "3339":
                    callType = "UPDATEINCSALARYTRO";
                    break;

                case "3347":
                    callType = "UPDATEINCSALARYPEB";
                    break;
                default:
                    // Increment update  except above
                    callType = "UPDATEINCSALARY";
                    break;

            }
            return callType;

        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblAnnInc"];

            for (int i = 0; i < this.gvAnnIncre.Rows.Count; i++)
            {

                string chksal = ((CheckBox)this.gvAnnIncre.Rows[i].FindControl("chkInc")).Checked ? "True" : "False";
                // TblRowIndex = (gvAnnIncre.PageIndex) * gvAnnIncre.PageSize + i;
                dt.Rows[i]["chksal"] = chksal;

            }

            Session["tblAnnInc"] = dt;



        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string cutdate = this.GetStdDate(this.txtdate.Text);
            string msg = "";

            string ddllistno = this.ddlIncList.SelectedValue.ToString();



            try
            {
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tblAnnInc"];


                //// Only for Peb Steel---- Amount check--30000

                if (comcod == "3347")
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        double grosssalchk = Convert.ToDouble(dt.Rows[j]["grossal"].ToString()) + Convert.ToDouble(dt.Rows[j]["finincamt"].ToString());

                        if (grosssalchk <= 30000 && dt.Rows[j]["chksal"].ToString() == "True")
                        {
                          
                            msg = "Please Check CheckBox Gross Salary less then 30000 taka !!!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                            return;

                        }


                    }



                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    double grossal = Convert.ToDouble(dt.Rows[i]["grossal"].ToString()) + Convert.ToDouble(dt.Rows[i]["finincamt"].ToString());
                    string chksal = (comcod == "3347" && dt.Rows[i]["chksal"].ToString() == "True") ? "Length" : "";
                    string deptcode = (comcod == "3347") ? dt.Rows[i]["deptcode"].ToString().Substring(0, 4) : "";
                    string calltype = this.CompanyCalltype();

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", calltype, empid,
                                               grossal.ToString(), chksal, deptcode, "", "", "", "", "", "", "", "", "", "", "");

                    bool resulb = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "UPDATEINCREMENTSAL", ddllistno,
                                                      empid, userid, cutdate, Terminal, Sessionid, "", "", "", "", "", "", "", "", "");

                }





                msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }
            catch (Exception ex)
            {
                msg = "Update Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.lnkbtnShow.Text = "New";
                this.lblddlIncrementNo.Text = this.ddlIncList.SelectedItem.Text.Trim();
                this.ddlIncList.Visible = false;
                this.lblddlIncrementNo.Visible = true;
                this.ShowIncrementInfo();
                return;
            }


            this.lnkbtnShow.Text = "Ok";
            this.ddlIncList.Visible = true;
            this.lblddlIncrementNo.Visible = false;

            this.gvAnnIncre.DataSource = null;
            this.gvAnnIncre.DataBind();
        }

        private void ShowIncrementInfo()
        {

            string comcod = this.GetCompCode();
            string preincreno = this.ddlIncList.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENT", preincreno, "", "", "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblAnnInc"] = dt;
            this.Data_Bind();



        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            string seccode = dt1.Rows[0]["seccode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[j]["seccode"].ToString() == seccode)
                {

                    dt1.Rows[j]["deptname"] = "";
                    dt1.Rows[j]["section"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                        dt1.Rows[j]["deptname"] = "";
                    if (dt1.Rows[j]["seccode"].ToString() == seccode)
                        dt1.Rows[j]["section"] = "";
                }

                deptcode = dt1.Rows[j]["deptcode"].ToString();
                seccode = dt1.Rows[j]["seccode"].ToString();

            }
            return dt1;
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            this.gvAnnIncre.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvAnnIncre.DataSource = dt;
            this.gvAnnIncre.DataBind();
            string comcod = this.GetCompCode();

            if (comcod == "3347")
            {
                this.gvAnnIncre.Columns[11].Visible = true;
            }

            this.FooterCal();

        }

        protected void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFpresal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossal)", "")) ? 0.00 : dt.Compute("sum(grossal)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incamt)", "")) ? 0.00 : dt.Compute("sum(incamt)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFfinincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(finincamt)", "")) ? 0.00 : dt.Compute("sum(finincamt)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFRevise")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(revisesal)", "")) ? 0.00 : dt.Compute("sum(revisesal)", ""))).ToString("#,##0;(#,##0);");



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void gvAnnIncre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAnnIncre.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}