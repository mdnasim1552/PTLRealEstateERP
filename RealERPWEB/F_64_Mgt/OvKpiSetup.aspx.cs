using System;
using System.Collections;
using System.Collections.Generic;
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
using RealEntity;
namespace RealERPWEB.F_64_Mgt
{
    public partial class OvKpiSetup : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = true;
                ((Label)this.Master.FindControl("lblTitle")).Text = "Kpi Setup Information";

                this.LoadGrid();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }
        protected void lnkprint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblSepupDet"];
            DataTable dt2 = (DataTable)ViewState["tblDesc"];

            //string emplName = dt.Tables[1].Rows[0]["empname"].ToString();
            //string desg = dt.Tables[1].Rows[0]["desg"].ToString();

            ReportDocument rptOvKpiSetup = new RealERPRPT.R_64_Mgt.RptOvKpiSetup();
            TextObject txtCompName = rptOvKpiSetup.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = comnam.ToUpper();

            TextObject rpttxtEmpName = rptOvKpiSetup.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            rpttxtEmpName.Text = "Name: " + dt2.Rows[0]["empname"].ToString();

            TextObject txtdesgnation = rptOvKpiSetup.ReportDefinition.ReportObjects["txtdeptname"] as TextObject;
            txtdesgnation.Text = "Designation: " + dt2.Rows[0]["desg"].ToString();


            TextObject txtuserinfo = rptOvKpiSetup.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptOvKpiSetup.SetDataSource(dt);


            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptOvKpiSetup.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptOvKpiSetup;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (this.Request.QueryString["comcod"].ToString());


        }




        private void SaveValue()
        {

            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tblSepupDet"];
            for (int i = 0; i < this.gvSetupDet.Rows.Count; i++)
            {
                double Wqty = Convert.ToDouble("0" + ((TextBox)this.gvSetupDet.Rows[i].FindControl("txtgvwqty")).Text.Trim());
                double Marks = Convert.ToDouble("0" + ((TextBox)this.gvSetupDet.Rows[i].FindControl("txtgvmarks")).Text.Trim());
                bool chkmr = ((CheckBox)this.gvSetupDet.Rows[i].FindControl("chkvmrno")).Checked;

                rowindex = (this.gvSetupDet.PageSize * this.gvSetupDet.PageIndex) + i;
                tblt02.Rows[rowindex]["wqty"] = Wqty;
                tblt02.Rows[rowindex]["marks"] = Marks;
                tblt02.Rows[rowindex]["active"] = chkmr;
            }
            ViewState["tblSepupDet"] = tblt02;


        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblSepupDet"];

            string comcod = this.GetCompCode();

            string monid = this.Request.QueryString["monid"].ToString();
            string Deptcode = this.Request.QueryString["deptcode"].ToString();
            string empid = this.Request.QueryString["empid"].ToString();
            //string PactCode = this.lblCenter.Text;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string actcode = dt.Rows[i]["actcode"].ToString();
                string active = dt.Rows[i]["active"].ToString();
                double Wqty = Convert.ToDouble(dt.Rows[i]["wqty"].ToString());
                double Marks = Convert.ToDouble(dt.Rows[i]["marks"].ToString());
                if (active == "True" || Wqty != 0 || Marks != 0)
                {
                    MktData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_MGT", "INSERTUPSTDKPISETUP", monid, empid, actcode, Wqty.ToString(), Marks.ToString(), Deptcode);
                }

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Unit Fixation";
                string eventdesc = "Update Fixation";
                string eventdesc2 = "Project Name: "; //this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void LoadGrid()
        {

            ViewState.Remove("tblSepupDet");

            string comcod = this.GetCompCode();
            string monid = this.Request.QueryString["monid"].ToString();
            string Deptcode = this.Request.QueryString["deptcode"].ToString();
            string empid = this.Request.QueryString["empid"].ToString();

            DataSet ds4 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_MGT", "STDKPISETUP", Deptcode, monid, empid, "", "", "", "", "", "");
            if (ds4 == null)
                return;

            ViewState["tblSepupDet"] = ds4.Tables[0].Copy();
            ViewState["tblSepupDet1"] = ds4.Tables[0].Copy();

            this.txtDesc.Text = "Name - " + ds4.Tables[1].Rows[0]["empname"].ToString() //+ " , Department - " + ds4.Tables[1].Rows[0]["depname"].ToString()
                + " , Designation - " + ds4.Tables[1].Rows[0]["desg"].ToString(); //+ " , Date of Joining - " + ds4.Tables[1].Rows[0]["joindat"].ToString() 
                                                                                  // + " , Target of Month - " + this.Request.QueryString["month"].ToString(); 

            ViewState["tblDesc"] = ds4.Tables[1];

            this.ShowValue();

        }
        private void ShowValue()
        {
            DataTable tblt05 = (DataTable)ViewState["tblSepupDet"];
            DataView dv = tblt05.DefaultView;
            dv.RowFilter = "active='True'";
            ViewState["tblSepupDet"] = dv.ToTable();
            if (dv.ToTable().Rows.Count == 0)
            {
                DataTable dt = (DataTable)ViewState["tblSepupDet1"];
                ViewState["tblSepupDet"] = HiddenSameData(dt);

            }

            this.Data_bind();
        }
        private void Data_bind()
        {
            DataTable tblt05 = (DataTable)ViewState["tblSepupDet"];
            this.gvSetupDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSetupDet.DataSource = tblt05;
            this.gvSetupDet.DataBind();
            this.FooterCal();
        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["tblSepupDet"];
            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvSetupDet.FooterRow.FindControl("lblgvFmarks")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(marks)", "")) ?
                                   0 : dt.Compute("sum(marks)", ""))).ToString("#,##0.00;(#,##0.00); ");

            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new  RealERPRPT.R_06_Sal.rptUnitFxInf();
            //DataTable dt1 = (DataTable)ViewState["tblSepupDet"];
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = "uamt>0";
            //rptstk.SetDataSource(dv1);

            //TextObject txtCompname = rptstk.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //txtCompname.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Unit Fixation";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //dv1.RowFilter = "";

        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {
            DataTable tblt05 = (DataTable)ViewState["tblSepupDet1"];
            string comcod = this.GetCompCode();
            if (this.chkAllSInf.Checked)
            {
                ViewState["tblSepupDet"] = HiddenSameData(tblt05);
            }

            else
            {
                DataView dv = tblt05.DefaultView;
                dv.RowFilter = "active='True'";
                ViewState["tblSepupDet"] = HiddenSameData(dv.ToTable());
            }
            this.Data_bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string actcode1 = dt1.Rows[0]["actcode1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode1"].ToString() == actcode1)
                    dt1.Rows[j]["actdesc1"] = "";
                actcode1 = dt1.Rows[j]["actcode1"].ToString();
            }
            return dt1;

        }

        //protected void gvUnit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{

        //    string comcod =this.GetCompCode(); 
        //    //string pactcode = this.ddlProjectName.SelectedValue.ToString();
        //    string UsirCode = ((Label)this.gvPlanDet.Rows[e.RowIndex].FindControl("lblCode")).Text.Trim();
        //    string Code = ((TextBox)this.gvPlanDet.Rows[e.RowIndex].FindControl("txtgvSCode")).Text.Trim();

        //    bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEUNITENTRY", "pactcode", UsirCode, Code, "", "", "", "", "", "", "", "", "", "", "", "");

        //    if (result == true) 
        //    {
        //        this.LoadGrid();

        //    }

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Unit Fixation";
        //        string eventdesc = "Delete Fixation";
        //        string eventdesc2 = "Project Name: " ;
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }
        //}

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        protected void gvSetupDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvSetupDet.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }

        protected void lblgvFTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }
        protected void gvSetupDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblSepupDet"];
            string comcod = this.GetCompCode();
            string monid = this.Request.QueryString["monid"].ToString();
            string Deptcode = this.Request.QueryString["deptcode"].ToString();
            string empid = this.Request.QueryString["empid"].ToString();
            string Actcode = ((Label)this.gvSetupDet.Rows[e.RowIndex].FindControl("lblgvActcode")).Text.Trim();

            bool result = MktData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_MGT", "DELSTDKPISETUP", monid, Deptcode, empid, Actcode, "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                //int rowindex = (this.gvSetupDet.PageSize) * (this.gvSetupDet.PageIndex) + e.RowIndex;
                //dt.Rows[rowindex].Delete();
                this.LoadGrid();

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Kpi Setup";
                string eventdesc = "Delete Work List";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void Chkcopy_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Chkcopy.Checked)
            {
                this.GetCopyYearMonth();
            }
            this.pnlCopy.Visible = (this.Chkcopy.Checked);
        }
        private void GetCopyYearMonth()
        {


            string comcod = this.GetCompCode();

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlperyearmon.DataTextField = "yearmon";
            this.ddlperyearmon.DataValueField = "ymon";
            this.ddlperyearmon.DataSource = ds1.Tables[0];
            this.ddlperyearmon.DataBind();

            ds1.Dispose();
        }
        protected void lmsg_DataBinding(object sender, EventArgs e)
        {

        }
        protected void lbtnCopy_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string empid = this.Request.QueryString["empid"].ToString();

            string copymonth = this.ddlperyearmon.SelectedValue.ToString();
            string currentmonth = this.Request.QueryString["monid"].ToString();
            // string upload = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = hst["deptcode"].ToString();

            bool result = MktData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "INSORUPEMPSTDKPIEMP", empid, copymonth, currentmonth);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "updated fail !";
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "suscessfully ";
            this.Chkcopy.Checked = false;
            this.Chkcopy_CheckedChanged(null, null);

            this.LoadGrid();

        }
    }
}
