using Microsoft.Reporting.WinForms;
using RealEntity.C_81_Hrm.C_81_Rec;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RptSettlementStatus : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess feaData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.CommonButton();
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = "01" + this.txtDatefrom.Text.Trim().Substring(2);
                this.txtdateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompanyName();
                this.lnkbtnSerOk_Click(null, null);

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("href", "../../F_81_Hrm/F_92_Mgt/EmpSettlement?Type=Entry&actcode=");
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("target", "_blank");
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).ToolTip = "Add Settlement Info";

        }

        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        private void ShowData()
        {
            string comcod = this.GetComCode();
            string fdate = this.txtDatefrom.Text.ToString();
            string tdate = this.txtdateto.Text.ToString();
            string empType = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string div = "%";
            string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "SHOW_SEPERATED_EMP", fdate, tdate, empType, div, Dept, section, "", "", "");

            if (ds2 == null)
            {
                this.gvSettInfo.DataSource = null;
                this.gvSettInfo.DataBind();
                return;
            }

            ViewState["tblSetTopInfo"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblSetTopInfo"];

            this.gvSettInfo.DataSource = dt;
            this.gvSettInfo.DataBind();

            this.FooterCalculation();
            Session["Report1"] = gvSettInfo;
            if (dt.Rows.Count > 0)
            {
                ((HyperLink)this.gvSettInfo.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblSetTopInfo"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvSettInfo.FooterRow.FindControl("gvlblFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ttlamt)", "")) ?
               0.00 : dt.Compute("Sum(ttlamt)", ""))).ToString("#,##0;(#,##0); ");
        }

        protected void gvSettInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
    

                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).Trim().ToString();
                string apstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "aprvstatus"));

                if (apstatus == "False")
                {
                    lnkEdit.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/EmpSettlement?Type=Entry&actcode=" + empid;
                }
                else
                {
                    lnkEdit.Text = "<i class='fa fa-lock'></i>";
                    lnkEdit.CssClass = "btn btn-xs btn-danger";
                    lnkEdit.ToolTip = "Approved";
                }

            }

        }
   
        protected void HypRDDoPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empId = ((Label)this.gvSettInfo.Rows[index].FindControl("lblgvempid")).Text.ToString();
            switch (comcod)
            {
                case "3101":
                    this.PrintEmpFinalSett(empId);
                    break;

                case "3370":
                    this.PrintEmpFinalSettCPDL(empId);
                    break;

                default:
                    this.PrintEmpFinalSett(empId);
                    break;
            }
        }


        private void PrintEmpFinalSettCPDL(string empId) {
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string calltype = getcallType();


            //select comcod, empid, empname, septypedesc, billdate, refno, ttlamt = isnull(ttlamt, 0.00), retdat, joindat, idno, designation, deptcode,
            // deptname, servleng, aprvstatus from #tblsttopsheet1 where deptcode like @Desc3 and deptcode like @Desc4 and deptcode like @Desc5 and deptcode like @Desc6
            DataTable dt = (DataTable)ViewState["tblSetTopInfo"];
            string  name       = dt.Rows[0]["empname"].ToString()??"";
            string  idno       = dt.Rows[0]["idno"].ToString() ?? "";
            string  joindat    = Convert.ToDateTime(dt.Rows[0]["joindat"]).ToString("dd-MMM-yyyy") ?? "";
            string  resigndat  = Convert.ToDateTime(dt.Rows[0]["retdat"]).ToString("dd-MMM-yyyy") ?? "";
            string  servlen    = dt.Rows[0]["servleng"].ToString() ?? "";
            string  desig      = dt.Rows[0]["designation"].ToString() ?? "";
            string  dept       = dt.Rows[0]["deptname"].ToString() ?? "";
            string  septype     = dt.Rows[0]["septypedesc"].ToString() ?? "";
            string  aplydat    =Convert.ToDateTime( dt.Rows[0]["billdate"]).ToString("dd-MMM-yyyy") ?? "";
            string  confirmdat = Convert.ToDateTime(dt.Rows[0]["billdate"]).ToString("dd-MMM-yyyy") ?? "";


            DataSet ds2 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", calltype, empId, "", "", "", "", "", "", "");
            if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                return;
           var list= ds2.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>().FindAll(p => p.hrgcod.Substring(0, 3) == "351").OrderBy(x => x.hrgcod).ToList();
           var list2 = ds2.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>().FindAll(p => p.hrgcod.Substring(0, 3) == "352").OrderBy(x => x.hrgcod).ToList();

            double ttlearn= Math.Round(Convert.ToDouble(list.Sum(x => x.ttlamt)));
            double ttlded =Math.Round( Convert.ToDouble(list2.Sum(x => x.ttlamt)));
            double netpay = Math.Round(ttlearn - ttlded);

            string inwords =  ASTUtility.Trans(netpay, 2);

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptFinalSettlmntCP", list, list2, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comlogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Final Settlement Salary"));
            Rpt1.SetParameters(new ReportParameter("name", name));
            Rpt1.SetParameters(new ReportParameter("idno", idno));
            Rpt1.SetParameters(new ReportParameter("joindat", joindat));
            Rpt1.SetParameters(new ReportParameter("resigndat", resigndat));
            Rpt1.SetParameters(new ReportParameter("servlen", servlen));
            Rpt1.SetParameters(new ReportParameter("desig", desig));
            Rpt1.SetParameters(new ReportParameter("dept", dept));
            Rpt1.SetParameters(new ReportParameter("aplydat", aplydat));
            Rpt1.SetParameters(new ReportParameter("confirmdat", confirmdat));
            Rpt1.SetParameters(new ReportParameter("septype", septype));
            Rpt1.SetParameters(new ReportParameter("inwords", inwords));
            Rpt1.SetParameters(new ReportParameter("ttlearning", ttlearn.ToString()));
            Rpt1.SetParameters(new ReportParameter("ttldeduct", ttlded.ToString()));
            Rpt1.SetParameters(new ReportParameter("netpay", netpay.ToString()));

            Rpt1.SetParameters(new ReportParameter("txtfooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            string printype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + printype + "');", true);

        }

        private string getcallType()
        {
            string calltype = "";
            
            string comcod = GetComCode();
            switch (comcod)
            {
                case "3365":
                    calltype = "GET_EMP_SETTLEMENT_INFO";
                    break;

                case "3370":
                    calltype = "GET_EMP_SETTLEMENT_INFO_CPDL";
                    break;
            }
            return calltype;
        }

        private void PrintEmpFinalSett(string empId)
        {
            //string comcod = this.GetComCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string approvStatus = "True";
            //DataSet ds2 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_SEPERATED_EMP", approvStatus, "", "", "", "", "", "", "");
            //if (ds2 == null || ds2.Tables[0].Rows.Count ==0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Approve First!" + "');", true);
            //    return;
            //}

            //var emplist1 = ds2.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>();
            //var emplist = emplist1.FindAll(p => p.empid==sEmpId);
            //var deptcode = emplist[0].deptcode.Substring(0, 4);
            //DataSet ds3 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_EMP_SETTLEMENT_INFO_FB", sEmpId, "0", "", "", "", "", "", "");
            //var list = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();
            //var list1 = list.FindAll(p => p.hrgcod.Substring(0, 3) == "351");//Wages
            //var list2 = list.FindAll(p => p.hrgcod.Substring(0, 3) == "352");//Deduction
            //var list3 = list.FindAll(p => p.hrgcod.Substring(0, 3) == "353");//Earnings
            //string txtDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //string empName = emplist[0].empname.ToString();
            //string empId = emplist[0].idno.ToString();
            //string empDesig = emplist[0].designation.ToString();
            //string empDept = emplist[0].deptname.ToString();
            //string empSection = emplist[0].section.ToString();
            //string joinDate = emplist[0].joindat.ToString("dd-MMM-yyyy");
            //string sepDate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            //string effDate = emplist[0].effectdate.ToString("dd-MMM-yyyy");
            //string serLength = emplist[0].servleng.ToString();
            //string daysConMonth = emplist[0].daysconmonth.ToString();
            //var totalWages = (list1.Sum(s => s.amount)).ToString("#,##0.00;(#,##0.00); ");
            //var totalEarn = (list3.Sum(s => s.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
            //var totalDed = (list2.Sum(s => s.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
            //var netAmount = (list3.Sum(p => p.ttlamt) - list2.Sum(p => p.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
            //double netpay = Convert.ToDouble(netAmount);

            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RptSetupClass1.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSattelmentFB", list1, list2, list3);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("compName", comnam));
            //Rpt1.SetParameters(new ReportParameter("rptTitle", "Final Sattelment Bill"));
            //Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            //Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            //Rpt1.SetParameters(new ReportParameter("totalWages", totalWages));
            //Rpt1.SetParameters(new ReportParameter("totalEarn", totalEarn));
            //Rpt1.SetParameters(new ReportParameter("totalDed", totalDed));
            //Rpt1.SetParameters(new ReportParameter("netAmount", netAmount));
            //Rpt1.SetParameters(new ReportParameter("tkInWords", ASTUtility.Trans(netpay, 2)));
            //Rpt1.SetParameters(new ReportParameter("empName", empName));
            //Rpt1.SetParameters(new ReportParameter("empId", empId));
            //Rpt1.SetParameters(new ReportParameter("empDesig", empDesig));
            //Rpt1.SetParameters(new ReportParameter("empDept", empDept));
            //Rpt1.SetParameters(new ReportParameter("empSection", empSection));
            //Rpt1.SetParameters(new ReportParameter("joinDate", joinDate));
            //Rpt1.SetParameters(new ReportParameter("sepDate", sepDate));
            //Rpt1.SetParameters(new ReportParameter("effDate", effDate));
            //Rpt1.SetParameters(new ReportParameter("serLength", serLength));
            //Rpt1.SetParameters(new ReportParameter("daysConMonth", daysConMonth));

            //Session["Report1"] = Rpt1;
            //string type = "PDF";
            //ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }

        public string GetMonthName(string name)
        {
            return name.Replace("Jan", "জানুয়ারী").Replace("Feb", "ফেব্রুয়ারী").Replace("Mar", "মার্চ").
                Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").
                Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর").Replace("Nov", "নভেম্বর").
                Replace("Dec", "ডিসেম্বর");

        }
        public string GetBanglaNumber(int number)
        {
            return string.Concat(number.ToString().Select(c => (char)('\u09E6' + c - '0')));
        }
       
        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            if (ds1==null)
                return;

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();

            this.GetDeptList();
            ds1.Dispose();
        }
      
        private void GetDeptList()
        {
            string comcod = this.GetComCode();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string txtSProject = "%";
            DataSet ds2 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            if (ds2==null)
                return;

            this.ddlDept.DataTextField = "deptdesc";
            this.ddlDept.DataValueField = "deptcode";
            this.ddlDept.DataSource = ds2.Tables[0];
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";

            this.GetSectionList();
            ds2.Dispose();
        }

        private void GetSectionList()
        {
            string comcod = this.GetComCode();
            string deptcode = this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string txtSProject = "%";
            DataSet ds3 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", deptcode, txtSProject, "", "", "", "", "", "", "");
            if (ds3==null)
                return;

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = ds3.Tables[0];
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
            ds3.Dispose();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
    }
}