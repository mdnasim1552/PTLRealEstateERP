using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_97_MIS
{
    public partial class EnvelopeTemplate : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();


                this.GetCompany();
                this.visibilityBracnh();
                this.GetEnvType();
                this.GetProjectName();
                this.SectionName();

                //this.GetEmpName();
                // this.SelectType();

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

        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "94%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            // DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");


            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetBranch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtSProject = "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETBRANCH", Company, txtSProject, "", "", "", "", "", "", "");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETBRANCH_NEW", Company, userid, "", "", "", "", "", "", "");

            this.ddlBranch.DataTextField = "actdesc";
            this.ddlBranch.DataValueField = "actcode";
            this.ddlBranch.DataSource = ds1.Tables[0];
            this.ddlBranch.DataBind();
            // this.ddlBranch_SelectedIndexChanged(null, null);
        }
        private void visibilityBracnh()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3315":
                case "3347":
                case "3353":
                case "3358":
                    this.divBracnhLsit.Visible = false;
                    this.ddlBranch.Items.Clear();
                    break;

                default:
                    this.divBracnhLsit.Visible = true;
                    break;

            }
        }
        private void GetEnvType()
        {

            string comcod = this.GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETENVTYPE", "7201%", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            this.ddlTypeHeader.DataTextField = "title";
            this.ddlTypeHeader.DataValueField = "gcod";
            this.ddlTypeHeader.DataSource = ds1.Tables[0];
            this.ddlTypeHeader.DataBind();
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";
            string txtSProject = "%%";
            //  DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", branch, txtSProject, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETDPTLIST_NEW", branch, userid, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
            // this.SectionName();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3315":
                case "3347":
                case "3353":
                case "3358":
                    this.divBracnhLsit.Visible = false;
                    this.ddlBranch.Items.Clear();
                    this.GetProjectName();
                    break;

                default:
                    this.GetBranch();
                    break;

            }

        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        private void SectionName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            //  string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlProjectName.SelectedValue.ToString();
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string txtSSec = "%%";
            // DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETSECTION_LIST", projectcode, userid, "", "", "", "", "", "", "");

            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            // this.GetEmpName();
            ddlSection_SelectedIndexChanged(null, null);
        }
        private void SectionNameAll()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            // string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlProjectName.SelectedValue.ToString();
            string txtSSec = "%%";
            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", Company, txtSSec, txtSSec, "", "", "", "", "", "", "");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETSECTION_LIST", Company, userid, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            // this.GetEmpName();
            ddlSection_SelectedIndexChanged(null, null);
        }
        private void GetEmpName(string empcode)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";
            string ProjectCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? branch : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%");
            string section = this.ddlSection.SelectedValue.ToString() == "000000000000" ? ProjectCode : this.ddlSection.SelectedValue.ToString();
            string emptype = "";

            section = empcode.Length > 0 ? "%%" : section;

            string getmethod = (this.chkresign.Checked ? "True" : "False");
            if (getmethod == "True")
            {
                emptype = "R";
            }
            else
            {
                emptype = "A";
            }
   

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_EMPLIST", section, "%%", userid, emptype, "", "", "", "", "");
            if (ds5 == null)
                return;

            //  DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPAYSLIPEMPNAMEALL", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds5.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
            empcode = "";


        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empcode = "";
            this.GetEmpName(empcode);

        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            SectionNameAll();
            string empcode = "all";
            this.GetEmpName(empcode);
        }
        private void GetComASecSelected()
        {
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString().Trim();
            if (empid == "000000000000" || empid == "")
                return;
            DataTable dt = (DataTable)ViewState["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");

        }
        protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            GetEmpList();

        }
        private void GetEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string secid = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETALLACTIVEEMP", Company, Deptid, secid, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;
            }
            Session["tblEmpstatus"] = ds4.Tables[0] ;
            this.LoadGrid();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string company, secid;

            company = dt1.Rows[0]["company"].ToString();
            secid = dt1.Rows[0]["secid"].ToString();
            string depcod = dt1.Rows[0]["depcod"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company)
                {
                    dt1.Rows[j]["companyname"] = "";
                }
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    dt1.Rows[j]["section"] = "";
                }
                if (dt1.Rows[j]["depcod"].ToString() == depcod)
                {
                    dt1.Rows[j]["deptname"] = "";
                }

                company = dt1.Rows[j]["company"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
                depcod = dt1.Rows[j]["depcod"].ToString();
            }


            return dt1;

        }

        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblEmpstatus"];
            //this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpList.DataSource = dt;
            this.gvEmpList.DataBind();


            if (dt.Rows.Count > 0)
            {

                Session["Report1"] = gvEmpList;
                ((HyperLink)this.gvEmpList.HeaderRow.FindControl("hlbtntbCdataExcelemplist")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }




        }
        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected   void lbtnPrint_Click(object sender, EventArgs e)
        {             
            GetChckedDataPrint();
        }

        private void GetChckedDataPrint()
        {
            string typeheader = ddlTypeHeader.SelectedItem.Text.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam =  hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string envtype = this.ddlTypeHeader.SelectedValue.ToString();
            
            // GET DATA FROM EMP STATUS
            DataTable dt = (DataTable)Session["tblEmpstatus"]; 
            int index;
            for (int i = 0; i < this.gvEmpList.Rows.Count; i++)
            {
                string isPrint = (((CheckBox)gvEmpList.Rows[i].FindControl("isPrint")).Checked) ? "True" : "False";                 
                index = (this.gvEmpList.PageSize) * (this.gvEmpList.PageIndex) + i;
                dt.Rows[index]["isPrint"] = isPrint;
            }
            Session["tblEmpstatus"] = dt;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "isPrint = 'True'";
            DataTable dt1 = dv.ToTable();


            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_97_MIS.Mgt_ManPower.HrmEnvelopPrint>();
            
            LocalReport Rpt1 = new LocalReport();

            if (comcod == "3368")
            {
                switch (envtype)
                {
                    case "7201001":
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_97_MIS.RptHrmPaySlipEnvelop", list, null, null);
                        break;
                    case "7201002":
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_97_MIS.RptPromotionEnvelop", list, null, null);
                        Rpt1.SetParameters(new ReportParameter("HeadTitle", "Stricty Private & Confidential")); 
                        Rpt1.SetParameters(new ReportParameter("Dpttitle", "Human Resources Department"));
                        break;
                }
            }
            else
            {
                switch (envtype)
                {
                    case "7201001":
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_97_MIS.RptHrmPaySlipEnvelop", list, null, null);

                        break;
                    case "7201002":
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_97_MIS.RptPromotionEnvelop", list, null, null);
                        break;
                }
            }


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("toheader", typeheader));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));


            Session["Report1"] = Rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "printEnvelop('" + type + "');", true);




        }

        //protected void lnkbtnEnvelop_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

        //        int index = row.RowIndex;
        //       // bool isprint = ((CheckBox)row.FindControl("isPrint")).Checked;

        //        string name = ((Label)row.FindControl("lblgvdeptandemployeeemp")).Text.ToString();
        //        string card = ((Label)row.FindControl("lblgvcardnoemp")).Text.ToString();
        //        string designation = ((Label)row.FindControl("lblgvdesignationemp")).Text.ToString();
        //        string department = ((Label)row.FindControl("lblgvdepname")).Text.ToString();


        //        string typeheader = ddlTypeHeader.SelectedItem.Text.ToString();
        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comcod = hst["comcod"].ToString();
        //        string comnam = comcod == "3101" ? hst["comnam"].ToString().Substring(4) : hst["comnam"].ToString();
        //        string session = hst["session"].ToString();
        //        string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
        //        string envtype = this.ddlTypeHeader.SelectedValue.ToString();

        //        var list = new List<RealEntity.C_81_Hrm.C_97_MIS.Mgt_ManPower.HrmEnvelopPrint>();
        //        var obj = new RealEntity.C_81_Hrm.C_97_MIS.Mgt_ManPower.HrmEnvelopPrint();
        //        if (comcod == "3368")
        //        {
        //            obj = new RealEntity.C_81_Hrm.C_97_MIS.Mgt_ManPower.HrmEnvelopPrint()
        //            {
        //                Name = name,
        //                Card = card,
        //                Designation = designation,
        //                Department = department,

        //            };
        //        }
        //        else if (comcod == "3101")
        //        {

        //            obj = new RealEntity.C_81_Hrm.C_97_MIS.Mgt_ManPower.HrmEnvelopPrint()
        //            {
        //                Name = name,
        //                Card = card,
        //                Designation = designation,
        //                Department = department,
        //            };
        //        }
        //        list.Add(obj);
        //        LocalReport Rpt1 = new LocalReport();

        //        if (comcod == "3368")
        //        {
        //            switch (envtype)
        //            {
        //                case "7201001":
        //                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_97_MIS.RptHrmPaySlipEnvelop", list, null, null);
        //                    break;
        //                case "7201002":
        //                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_97_MIS.RptPromotionEnvelop", list, null, null);
        //                    break;
        //            }
        //        }
        //        else 
        //        {
        //            switch (envtype)
        //            {
        //                case "7201001":
        //                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_97_MIS.RptHrmPaySlipEnvelop", list, null, null);

        //                    break;
        //                case "7201002":
        //                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_97_MIS.RptPromotionEnvelop", list, null, null);
        //                    break;
        //            }
        //        }
             

        //        Rpt1.EnableExternalImages = true;
        //        Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
        //        Rpt1.SetParameters(new ReportParameter("toheader", typeheader));
        //        Rpt1.SetParameters(new ReportParameter("comnam", comnam));


        //        Session["Report1"] = Rpt1;
        //        string type = "PDF";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "target", "printEnvelop('" + type + "');", true);
        //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
        //        //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //    }
        //    catch (Exception ex)
        //    {
        //        ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        //    }

        //}

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            int i, index;
            if (((CheckBox)this.gvEmpList.HeaderRow.FindControl("chkAllfrm")).Checked)
            {
                for (i = 0; i < this.gvEmpList.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmpList.Rows[i].FindControl("isPrint")).Checked = true;

                    index = (this.gvEmpList.PageSize) * (this.gvEmpList.PageIndex) + i;
                    dt.Rows[index]["isPrint"] = "True";
                }
            }

            else
            {
                for (i = 0; i < this.gvEmpList.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmpList.Rows[i].FindControl("isPrint")).Checked = false;

                    index = (this.gvEmpList.PageSize) * (this.gvEmpList.PageIndex) + i;
                    dt.Rows[index]["isPrint"] = "False";
                }
            }

            Session["tblEmpstatus"] = dt;
            // this.ShowPer();

        }

        protected void chkresign_CheckedChanged(object sender, EventArgs e)
        {
            string empcode = "";
            GetEmpName(empcode);
        }
    }
}