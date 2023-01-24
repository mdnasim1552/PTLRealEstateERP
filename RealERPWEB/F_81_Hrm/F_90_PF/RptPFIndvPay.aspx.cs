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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class RptPFIndvPay : System.Web.UI.Page
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

                //   this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompanyName();
                this.SelectView();
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "IndPfund":             
                    this.MultiView1.ActiveViewIndex = 0;
                   
                    break;


                case "Indswfsum":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "IndPfSattlement":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

            }


        }


        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();


            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompanyAgg.DataTextField = "actdesc";
            this.ddlCompanyAgg.DataValueField = "actcode";
            this.ddlCompanyAgg.DataSource = ds5.Tables[0];
            this.ddlCompanyAgg.DataBind();
            this.GetDepartment();
            this.ddlCompanyAgg_SelectedIndexChanged(null, null);
        }
        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            //   string type = this.Request.QueryString["Type"].ToString().Trim();
            string Company = ((this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";

            string txtSProject = "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAME", Company, txtSProject, "", "", "", "", "", "", "");

            this.ddldepartmentagg.DataTextField = "deptdesc";
            this.ddldepartmentagg.DataValueField = "deptcode";
            this.ddldepartmentagg.DataSource = ds4.Tables[0];
            this.ddldepartmentagg.DataBind();
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string deptcode = this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 4) + "%";
            string txtSProject = "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", deptcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetEmpName();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetEmpName()
        {
            string comcod = this.GetComeCode();
            string ProjectCode =  this.ddlProjectName.SelectedValue.ToString() + "%";
            string txtSProject = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds5.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
            this.GetComASecSelected();
        }

        private void GetComASecSelected()
        {
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
            }

        }

        protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }

        protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
            //this.ddlProjectName_SelectedIndexChanged(null,null);
        }

        protected void lbtndeptagg_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
            // this.GetDepartment();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ddlProjectName_SelectedIndexChanged(null, null);
            //this.ddlProjectName_SelectedIndexChanged(null, null);
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();

        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "IndPfund":
               
                    this.GetIndpfund();
                    break;


                case "Indswfsum":
                    this.ShowindSwfSum();
                    break;

                case "IndPfSattlement":
                    this.ShowindPFSattlement();
                    break;

                    

            }




           

        }

        private void ShowindPFSattlement()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            string date = this.txttodate.Text.Trim();

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETINDSWFANDPFSATTLEMENTSTATUS", empid, date, "", "", "", "", "", "", "");
            if (ds == null)
                return;
            ViewState["tblempinfo"] = ds.Tables[1];
            ViewState["tblemppfinfo"] = ds.Tables[0];
            ViewState["tblemppfinfototalsum"] = ds.Tables[2];
            this.Data_Bind();

        }



        private void GetIndpfund()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPINDPAY", empid, "", "", "", "", "", "", "", "");
            if (ds == null)
                return;
            ViewState["tblempinfo"] = ds.Tables[1];
            ViewState["tblemppfinfo"] = ds.Tables[0];
            this.Data_Bind();

            //this.gvpayinfo.DataSource = ds.Tables[0];
            //this.gvpayinfo.DataBind();
            

        }

        private void ShowindSwfSum()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETINDSWFANDPFSTATUS", empid, "", "", "", "", "", "", "", "");
            if (ds == null)
                return;
            ViewState["tblempinfo"] = ds.Tables[1];
            ViewState["tblemppfinfo"] = ds.Tables[0];
            this.Data_Bind();
            //this.gvswfsum.DataSource = ds.Tables[0];
            //this.gvswfsum.DataBind();
            //this.FooterCalCulation(ds.Tables[0]);

        }

        private void Data_Bind()
        {
        
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)ViewState["tblemppfinfo"];
            switch (Type)
            {
           
                case "IndPfund":
                    this.gvpayinfo.DataSource = dt;
                    this.gvpayinfo.DataBind();
                    this.FooterCalCulation();
                    break;


                case "Indswfsum":
                    this.gvswfsum.DataSource = dt;
                    this.gvswfsum.DataBind();
                    this.FooterCalCulation();
                    break;

                case "IndPfSattlement":
                    this.gvpfSattlement.DataSource = dt;
                    this.gvpfSattlement.DataBind();
                    this.FooterCalCulation();
                    break;
            }

        }



        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)ViewState["tblemppfinfo"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "IndPfund":


                    ((Label)this.gvpayinfo.FooterRow.FindControl("lbltotalpf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfamt)", "")) ? 0.00
                        : dt.Compute("sum(pfamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvpayinfo.FooterRow.FindControl("gvFcontribu")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(contribu)", "")) ? 0.00
                        : dt.Compute("sum(contribu)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvpayinfo.FooterRow.FindControl("gvFBalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balance)", "")) ? 0.00
                        : dt.Compute("sum(balance)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;

                case "Indswfsum":


                    ((Label)this.gvswfsum.FooterRow.FindControl("gvFgvswfamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(swf)", "")) ? 0.00
                        : dt.Compute("sum(swf)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvswfsum.FooterRow.FindControl("lblFgvpfswf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfamt)", "")) ? 0.00
                        : dt.Compute("sum(pfamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvswfsum.FooterRow.FindControl("gvFtotamat")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalamt)", "")) ? 0.00
                        : dt.Compute("sum(totalamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;

                case "IndPfSattlement":

                    ((Label)this.gvpfSattlement.FooterRow.FindControl("lblFgvpfsattle")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfamt)", "")) ? 0.00
                        : dt.Compute("sum(pfamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvpfSattlement.FooterRow.FindControl("gvFgvswfamtsattle")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(swf)", "")) ? 0.00
                        : dt.Compute("sum(swf)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvpfSattlement.FooterRow.FindControl("gvFtotamatsattle")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalamt)", "")) ? 0.00
                        : dt.Compute("sum(totalamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;

            }
                    


        }
        protected void txtsrchdeptagg_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "IndPfund":

                    this.PrintIndpfund();
                    break;


                case "Indswfsum":
                    this.PrintindSwfSum();
                    break;
                case "IndPfSattlement":
                    this.PrintIndPfSattlement();
                    break;

            }





        }

        private void PrintindSwfSum()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable Pfinfo = (DataTable)ViewState["tblemppfinfo"];
            DataTable empinfo = (DataTable)ViewState["tblempinfo"];
            DataTable emppfinfototalsum = (DataTable)ViewState["tblemppfinfototalsum"];

            var pflist = Pfinfo.DataTableToList<RealEntity.C_81_Hrm.C_90_PF.EmpWiseSWF>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_90_PF.RptEmpWiseSwf", pflist, null, null);
            //Rpt1.SetParameters(new ReportParameter("txtconpriod", Convert.ToDouble(empinfo.Rows[0]["years"]).ToString("#,##0;(#,##0") + "  Year  " + Convert.ToDouble(empinfo.Rows[0]["months"]).ToString("#,##0;(#,##0") + "  Month"));



            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("printdate", "Date : " + printdate));
            Rpt1.SetParameters(new ReportParameter("empname", empinfo.Rows[0]["name"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empid", empinfo.Rows[0]["idcard"].ToString()));
            
            Rpt1.SetParameters(new ReportParameter("rptname", "Employee Wise SWF Status"));
         
            Rpt1.SetParameters(new ReportParameter("dept", empinfo.Rows[0]["dept"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintIndPfSattlement()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable Pfinfo = (DataTable)ViewState["tblemppfinfo"];
            DataTable empinfo = (DataTable)ViewState["tblempinfo"];
            DataTable emppfinfototalsum = (DataTable)ViewState["tblemppfinfototalsum"];




            string tswf = Convert.ToDouble(emppfinfototalsum.Rows[0]["tswf"]).ToString("#,##0.00;(#,##0.00); ");

            string grandtotalamt = Convert.ToDouble(emppfinfototalsum.Rows[0]["grandtotalamt"]).ToString("#,##0.00;(#,##0.00); ");
            string contributioncom = Convert.ToDouble(emppfinfototalsum.Rows[0]["contributioncom"]).ToString("#,##0.00;(#,##0.00); ");
            string grandtotalpf = Convert.ToDouble(emppfinfototalsum.Rows[0]["grandtotalpf"]).ToString("#,##0.00;(#,##0.00); ");


            string inword = ASTUtility.Trans(Convert.ToDouble(grandtotalamt), 2);


            var pflist = Pfinfo.DataTableToList<RealEntity.C_81_Hrm.C_90_PF.IndPfSattlement>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_90_PF.RptIndPfSattlement", pflist, null, null);
            //Rpt1.SetParameters(new ReportParameter("txtconpriod", Convert.ToDouble(empinfo.Rows[0]["years"]).ToString("#,##0;(#,##0") + "  Year  " + Convert.ToDouble(empinfo.Rows[0]["months"]).ToString("#,##0;(#,##0") + "  Month"));



            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("printdate", "Date : " + printdate));
            Rpt1.SetParameters(new ReportParameter("empname", empinfo.Rows[0]["name"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empid", empinfo.Rows[0]["idcard"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empdesig", empinfo.Rows[0]["desig"].ToString()));
            Rpt1.SetParameters(new ReportParameter("joindate", Convert.ToDateTime(empinfo.Rows[0]["joindate"]).ToString("dd-MMM-yy")));
            Rpt1.SetParameters(new ReportParameter("empconfdate", Convert.ToDateTime(empinfo.Rows[0]["confirmdate"]).ToString("dd-MMM-yy")));
            Rpt1.SetParameters(new ReportParameter("slength", empinfo.Rows[0]["slength"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptname", "Final Sattlement of PF and SWF"));
            Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));
            Rpt1.SetParameters(new ReportParameter("contributioncom", contributioncom));
            Rpt1.SetParameters(new ReportParameter("tswf", tswf));
            Rpt1.SetParameters(new ReportParameter("grandtotalpf", grandtotalpf));
            Rpt1.SetParameters(new ReportParameter("grandtotalamt", grandtotalamt));
            Rpt1.SetParameters(new ReportParameter("InWrd", inword));
            Rpt1.SetParameters(new ReportParameter("pfend", empinfo.Rows[0]["pfend"].ToString()));
            Rpt1.SetParameters(new ReportParameter("dept", empinfo.Rows[0]["dept"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintIndpfund()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable Pfinfo = (DataTable)ViewState["tblemppfinfo"];
            DataTable empinfo = (DataTable)ViewState["tblempinfo"];
            var pflist = Pfinfo.DataTableToList<RealEntity.C_81_Hrm.IndvPf.PaymentScheduleList>();
            var emplist = empinfo.DataTableToList<RealEntity.C_81_Hrm.IndvPf.Empinfo>();

            if (comcod == "3333")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_90_PF.RptIndvPfAlli", pflist, null, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIndvPf", pflist, null, null);
                Rpt1.SetParameters(new ReportParameter("txtconpriod", Convert.ToDouble(empinfo.Rows[0]["years"]).ToString("#,##0;(#,##0") + "  Year  " + Convert.ToDouble(empinfo.Rows[0]["months"]).ToString("#,##0;(#,##0") + "  Month"));
            }


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("empname", empinfo.Rows[0]["name"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empid", empinfo.Rows[0]["idcard"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empdesig", empinfo.Rows[0]["desig"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptname", "Individual Payment Schedule of Provident Fund"));
            Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));
            Rpt1.SetParameters(new ReportParameter("pfend", empinfo.Rows[0]["pfend"].ToString()));
            Rpt1.SetParameters(new ReportParameter("dept", empinfo.Rows[0]["dept"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
       
    }
}