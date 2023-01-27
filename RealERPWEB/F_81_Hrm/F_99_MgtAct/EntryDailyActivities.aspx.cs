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
namespace RealERPWEB.F_81_Hrm.F_99_MgtAct
{
    public partial class EntryDailyActivities : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetDeptName();

            }
        }


        private void GetDeptName()
        {
            try
            {
                string comcod = this.GetCompCode();
                string deptdesc = "%" + this.txtSrcDepartment.Text.Trim() + "%";
                DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_DAILYACTIVITIES", "GETDEPTNAME", deptdesc, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.ddlDeapartmentName.Items.Clear();
                    return;
                }

                this.ddlDeapartmentName.DataTextField = "deptdesc";
                this.ddlDeapartmentName.DataValueField = "deptcode";
                this.ddlDeapartmentName.DataSource = ds1.Tables[0];
                this.ddlDeapartmentName.DataBind();
                ds1.Dispose();
                this.GetActivities();

            }
            catch (Exception e)
            {
            }
        }
        private void GetActivities()
        {
            try
            {
                string comcod = this.GetCompCode();
                string deptcode = this.ddlDeapartmentName.SelectedValue.ToString().Substring(0, 2);
                string deptActCode = (this.Request.QueryString["Type"] == "EntryDeptAct") ? "01" : "02";
                string deptactdesc = "%" + this.txtSrcDepAc.Text.Trim() + "%";
                DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_DAILYACTIVITIES", "GETDEPTACTIVITIEES", deptcode, deptActCode, deptactdesc, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.ddlActivities.Items.Clear();
                    return;
                }

                this.ddlActivities.DataTextField = "actdesc";
                this.ddlActivities.DataValueField = "actcode";
                this.ddlActivities.DataSource = ds1.Tables[0];
                this.ddlActivities.DataBind();
                ds1.Dispose();


            }
            catch (Exception e)
            {
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        protected void ibtnFindDepartment_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetDeptName();
        }
        protected void ibtnFindDepActvities_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetActivities();
        }

        protected void ddlDeapartmentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetActivities();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblDeptdesc.Text = this.ddlDeapartmentName.SelectedItem.Text;
                this.lblActivitiesdesc.Text = this.ddlActivities.SelectedItem.Text;
                this.ddlDeapartmentName.Visible = false;
                this.ddlActivities.Visible = false;
                this.lblDeptdesc.Visible = true;
                this.lblActivitiesdesc.Visible = true;
                this.ShowSection();
                return;


            }

            this.lbtnOk.Text = "Ok";
            this.MultiView1.ActiveViewIndex = -1;
            this.gvDailyEntry.DataSource = null;
            this.gvDailyEntry.DataBind();
            this.ddlDeapartmentName.Visible = true;
            this.ddlActivities.Visible = true;
            this.lblDeptdesc.Visible = false;
            this.lblActivitiesdesc.Visible = false;


        }



        private void ShowSection()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "EntryDeptAct":
                case "EntryIndAct":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowDailyActivities();
                    break; ;
            }

        }
        private void ShowDailyActivities()
        {
            try
            {
                ViewState.Remove("tbldact");
                string comcod = this.GetCompCode();
                string edate = Convert.ToDateTime(this.txtDate.Text).ToString("yyyyMMdd");
                string dactcode = this.ddlActivities.SelectedValue.ToString().Substring(0, 6) + "%";
                DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_DAILYACTIVITIES", "SHOWDAILYACTIVITIES", dactcode, edate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvDailyEntry.DataSource = null;
                    this.gvDailyEntry.DataBind();
                    return;
                }

                ViewState["tbldact"] = ds1.Tables[0];
                ds1.Dispose();
                this.Data_Bind();


            }
            catch (Exception e)
            {
            }



        }

        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)ViewState["tbldact"];
            switch (Type)
            {

                case "EntryDeptAct":
                case "EntryIndAct":
                    this.gvDailyEntry.DataSource = tbl1;
                    this.gvDailyEntry.DataBind();
                    break;

            }


        }





        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tbldact"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {

                case "EntryDeptAct":
                case "EntryIndAct":
                    for (int i = 0; i < this.gvDailyEntry.Rows.Count; i++)
                    {

                        tbl1.Rows[i]["prticlars"] = Convert.ToDouble("0" + ((TextBox)this.gvDailyEntry.Rows[i].FindControl("txtgvpertuculars")).Text.Trim()).ToString();
                        tbl1.Rows[i]["remarks"] = ((TextBox)this.gvDailyEntry.Rows[i].FindControl("txtgvremarks")).Text.Trim();


                    }

                    break;

            }
            ViewState["tbldact"] = tbl1;


        }

        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {


                case "EntryDeptAct":
                case "EntryIndAct":
                    this.RptPrintDailyreport();
                    break;
            }

        }


        protected void lbtnUpDailyEntry_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tbldact"];
                string dayid = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyyMMdd");
                string EntryDate = this.txtDate.Text.Trim();
                bool result = true;
                foreach (DataRow datarow in dt1.Rows)
                {


                    string gencode = datarow["gencode"].ToString();
                    string Perticulars = Convert.ToDouble(datarow["prticlars"].ToString()).ToString();
                    string Remarks = datarow["remarks"].ToString();
                    result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_DAILYACTIVITIES", "INORUPDAILYACVITIESINF", dayid, gencode, EntryDate, Perticulars, Remarks, "", "", "", "", "", "", "", "", "", "");


                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Failed');", true);
                        return;
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }

        }

        private void RptPrintDailyreport()

        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comnam = hst["comnam"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            // string comcod = this.GetCompCode();
            // string dayid = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyyMMdd");
            // DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "RPTDAILYACTIVITIES", dayid, "", "", "", "", "", "", "", "");

            // if (ds1 == null)        
            //     return;
            //DataTable dt= this.HiddenSameData(ds1.Tables[0]);
            //ReportDocument rptsl = new RealERPRPT.R_32_Mis.RptDailyActivities();
            //TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource(dt);
            //Session["Report1"] = rptsl;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }




    }
}