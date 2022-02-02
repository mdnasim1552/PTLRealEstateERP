using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB
{
    public partial class LetterDefault1 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // this.CommonButton();
                var type = this.Request.QueryString["Entry"].ToString().Trim();
                if (type == "Apprv" || type == "HR")
                {
                    this.panl1.Visible = false;
                    this.btnsave.Visible = false;
                    this.btnapprv.Visible = false;
                    this.ShowLetter();
                }


                // this.txtml.Text = "<h1><span style=" + "text-decoration:" + "underline;" + "><strong>sabid hossain</strong></span></h1>";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = date;
                // this.ShowView();


                string type1 = this.Request.QueryString["Type"].ToString().Trim();
                if (type1 == "10003" || type1 == "10004" || type1 == "10005" || type1 == "10020" || type1 == "10002" || type1 == "10013" || type1 == "10021" || type1 == "10022" || type1 == "10023")
                {
                    this.GetSelected();
                } 
                else
                {
                    this.GetEmployee();
                }


              //  this.GetLettPattern();
                string titale = this.Request.QueryString["Entry"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = titale;
               // ddlEmployee_SelectedIndexChanged(null, null);

                string Apprv = this.Request.QueryString["Entry"].ToString();
                if (Apprv == "Apprv")
                {

                    ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = true;
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = true;

                }
                if (Apprv == "HR")
                {
                    //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                }
                else
                {
                    //((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                    //((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                }
                this.lbtnOk_Click(null,null);

            }


        }

        private void ShowLetter()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            string empid = this.Request.QueryString["ID"].ToString().Trim();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", empid, type, "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            string lett = (string)ds3.Tables[0].Rows[0]["LETTDESC"];
            this.txtml.Text = lett;

            ViewState["letter"] = ds3.Tables[0];
        }
        private void GetEmployee()
        {



            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();
            ds3.Dispose();
            ViewState["empinfo"] = ds3;
        }

        private void CommonButton()
        {
            ((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;

            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            string Type = this.Request.QueryString["Entry"].ToString();
            if (Type == "Apprv")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Approve";
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = true;

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Entry"].ToString();
            switch (Type)
            {
                case "Apprv":
                    this.mgtprint();
                    break;

                default:
                    this.hrPrint();
                    break;
            }
        }

        private void mgtprint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string comcod = GetCompCode();

            //string msg = this.txtml.Text;
            //DataTable dt1 = (DataTable)ViewState["letter"];
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //LocalReport Rpt1 = new LocalReport();
            //string img = string.Empty;
            //try
            //{
            //    if (!(dt1.Rows[0]["EMPSIGN"] is DBNull))
            //        img = Convert.ToBase64String((byte[])dt1.Rows[0]["EMPSIGN"]);
            //}
            //catch (Exception)
            //{
            //    img = string.Empty;

            //}
            //Rpt1 = RptHRSetup.GetLocalReport("RD_81_Hrm.LetterDefault01", null, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("rpetext", msg));
            //Rpt1.SetParameters(new ReportParameter("ComImg", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("comName", comnam));
            //Rpt1.SetParameters(new ReportParameter("Comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("ApprvDesig", (string)dt1.Rows[0]["apprvdesig"]));
            //Rpt1.SetParameters(new ReportParameter("ApprvName", (string)dt1.Rows[0]["apprvname"]));
            //Rpt1.SetParameters(new ReportParameter("Apprasign", img));
            //Session["Report1"] = Rpt1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RDLCViewerWin.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void hrPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string usrid = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = GetCompCode();

            string msg = this.txtml.Text;

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "SHOWUSERSIGN", usrid, "", "", "", "");

            DataTable dt1 = ds3.Tables[0];
            // DataTable dt1 = (DataTable)ViewState["letter"];



            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            LocalReport Rpt1 = new LocalReport();
            string img = string.Empty;
            try
            {
                if (!(dt1.Rows[0]["empsign"] is DBNull))
                    img = Convert.ToBase64String((byte[])dt1.Rows[0]["empsign"]);
                //img = string.Empty;
            }
            catch (Exception)
            {
                img = string.Empty;
            }
            Rpt1 = RDLCAccountSetup.GetLocalReport("RD_81_Hrm.LetterDefault01", null, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rpetext", msg));
            Rpt1.SetParameters(new ReportParameter("ComImg", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comName", comnam));
            Rpt1.SetParameters(new ReportParameter("Comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ApprvDesig", (string)dt1.Rows[0]["desig"]));
            Rpt1.SetParameters(new ReportParameter("ApprvName", (string)dt1.Rows[0]["empname"]));
            Rpt1.SetParameters(new ReportParameter("Apprasign", img));
            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            
            string type1 = this.Request.QueryString["Type"].ToString().Trim();
            if (type1 == "10021" || type1 == "10022" || type1 == "10023")
            {
                bool result = false;
                var empid = this.ddlEmployee.SelectedValue.ToString();
                var strval = this.txtml.Text;
                var type = this.Request.QueryString["Type"].ToString().Trim();
                var date = this.txttodate.Text;
                string comcod = this.GetCompCode();
                string refno = "";
               
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEJLTCMPLTTRMLT", empid, type, date, refno, strval, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    this.lblmsg1.Text = "Save Failed";
                    this.lblmsg1.Visible = true;
                }
               
                this.lblmsg1.Text = "Save Successfully";
                this.lblmsg1.Visible = true;

            }
             


        }

        private void GetSelected()
        {
            string comcod = this.GetCompCode();

            string qtype = this.Request.QueryString["Type"].ToString();
            string empid = this.Request.QueryString["empid"]??"%%";
            string callType = "";
            if (qtype == "10002")
            {
                callType = "GETJOINEMPLIST";
            }
            else if (qtype == "10013" || qtype == "10020" || qtype == "10021" || qtype == "10022" || qtype == "10023")
            {
                callType = "GETCONFIRMEMP";
            }


            else
            {
                this.dptDiv.Visible = false;
                this.sectDiv.Visible = false;
                callType = "GETCANDIDATELIST";

            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LETTER", callType, qtype, empid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlEmployee.Items.Clear();
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds1.Tables[0];
            this.ddlEmployee.DataBind();
             
            ViewState["empinfo"] = ds1;
        }

        protected void GetLettPattern()
        {

        }

        protected string data(string type01)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string compname = hst["compname"].ToString();
            string usrid = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = GetCompCode();

            //created by nahid for dynamic html value
            
            string comaddress = (((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(6) : ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();

            string imgpge = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
            
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LETTER", "SHOWUSERSIGN", usrid, "", "", "", "");
            DataTable dt1 = ds3.Tables[0];
            string empid = this.ddlEmployee.SelectedValue.ToString();

            string type1 = this.Request.QueryString["Type"].ToString().Trim();

            string calltype = (Request.QueryString["Type"].ToString() == "10002") ? "GETEMPSALINFOAPP" : "GETEMPSALINFO";


            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", calltype, empid, "", "", "", "", "", "", "", "");
            DataTable dtsalery = ds5.Tables[0];
            DataView dvr = new DataView();
            DataTable dtr1 = new DataTable();
            dtr1 = dtsalery;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '040%'");
            dtr1 = dvr.ToTable();
            DataTable dtsal = dtr1;
            double SalAdd = Convert.ToDouble((Convert.IsDBNull(dtsal.Compute("sum(gval)", "")) ? 0.00 : dtsal.Compute("sum(gval)", "")));
            string ttlsalary = SalAdd.ToString("#,##0.00;(#,##0.00); ");
            string inwords = "In Word: " + ASTUtility.Trans(SalAdd, 2);
            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table width='400' border = '1'>");

            //Building the Header row.
            html.Append("<tr><th style='width:100px;font-weight:bold'>Desctiption</th><th style='width:100px;font-weight:bold'>Value</th></tr>");

            //Building the Data rows.
            foreach (DataRow dr1 in dtsal.Rows)
            {
                html.Append("<tr>");
                html.Append("<td style='width:100px;'>" + dr1["gdesc"].ToString() + "</td>" + "<td style='width:80px; text-align:right'>" + Convert.ToDouble(dr1["gval"]).ToString("#,##0.00;(#,##0.00); ") + "</td>");
                html.Append("</tr>");
            }
            html.Append("<tr><th style='width:100px;font-weight:bold'>Total</th><th style='width:100px;font-weight:bold; text-align:right'>" + ttlsalary + "</th></tr>");
            //Table end.
            html.Append("</table>");
            string tablesale = html.ToString();

            DataSet dtempinf = (DataSet)ViewState["empinfo"];
            DataView dv = dtempinf.Tables[0].DefaultView;
            dv.RowFilter = ("empid='" + empid + "'");
            DataTable dtempinf_ = dv.ToTable();
            string jdate = "", idCard = "";


            if (type1 == "10015" || type1 == "10012")
            {
                jdate = Convert.ToDateTime(dtempinf_.Rows[0]["jdate"]).ToString("dd-MMM-yyy");
                idCard = dtempinf_.Rows[0]["idcard"].ToString();

            }

            if (type1 == "10021" || type1 == "10022" || type1 == "10023")
            {
                this.btnsave.Visible = true;
            }



            //string empid = this.ddlEmployee.SelectedValue.ToString();
            // var empname = this.ddlEmployee.SelectedItem.ToString();
            string lbody = string.Empty;
            // string empid=hst["empid"].ToString();
            string name = this.ddlEmployee.SelectedItem.ToString();
            string Desig = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["desig"].ToString();//(string)ViewState["desig"];
            string depart = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["dptdesc"].ToString();//(string)ViewState["section"];
            string dptdesc = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["section"].ToString();//(string)ViewState["section"];

            string usrdesig = (dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["desig"].ToString();
            string usersign = "";//(dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["empsign"].ToString(); //Convert.ToBase64String((byte[])dt1.Rows[0]["empsign"]);
            string uname = (dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["empname"].ToString();
             
            switch (type01)
            {
                //"<img class='Companylogo' src='Image/LOGO8701.PNG' />";
                case "10001":
                    lbody = "<p><strong>Ref: SPL/HR/Appt/16/524</strong></p><p><strong>Nov 20, 2016</strong></p><p><strong>Abdullah Al Noman</strong></p><p><strong>C/O: Abdul Bashar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Boro Hossainput, PO: Banglabazar-3822,</strong></p><p><strong>PS: Begumganj, Dist: Noakhali&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong>Subject: <u>Letter of Appointment.</u></strong></p><p>&nbsp;</p><p>Dear <strong>Mr. Abdullah</strong>,</p><p>&nbsp;With reference to your application and subsequent interview with us, we have the pleasure to inform you that the Management of Star Paradise Ltd is pleased to appoint you as <strong>&ldquo;Field Officer&rdquo;</strong> in our Company <strong>Star Paradise Ltd </strong>on the following terms and conditions:</p><p>&nbsp;</p><p><strong>Commencement and nature of Appointment:</strong></p><ol><li>Your appointment is effective from <strong>Nov 20, 2016</strong>.</li><li>Your appointment is initially on probation basis for 6 (Six) months from the date of your joining. On satisfactory completion of your probationary period, your service may be confirmed. Otherwise, your probationary period may be extended for such a period as may be decided upon by the Management.</li><li>Your place of posting shall be at <strong>'Comilla &ndash; Chandpur&rdquo;</strong> and you shall work under the supervision of <strong>Divisional Head</strong>.</li></ol><p>&nbsp;</p><ol start='2'><li><strong>Placement, Compensation and other benefits:</strong></li><li>You will be entitled to Festival Bonuses as per Company policy, which are two (02) Eid Bonus, 100% of your Gross Salary each.</li><li>Your monthly salary as in &ldquo;Annexure-A&rdquo; only, which includes all your perquisites and allowances. Compensation will be governed by the rules of the Company on the subject, as applicable and / or amended hereafter.</li><li>Personal Income Tax, if any will be on your account and will be deducted each month by the company at source at the time of monthly salary disbursement for onward submission to the relevant Income Tax authorities.</li><li>You are not to disclose / discuss your salary with anyone related to this organization and keep it strictly confidential.</li><li>This is a position of full time and continuous responsibilities and will not engage yourself any Part-time work, profession or employment without written permission from the management.</li><li>You will be entitled with other benefits of the organization time to time as per the Company policy.</li></ol><p>&nbsp;</p><ol start='3'><li><strong>Duties and responsibilities:</strong></li><li>You will carry on with the duties and responsibilities entrusted to you and also the duties and responsibilities that may be entrusted to you by the Management from time to time. You will require working late hours whenever necessary for the greater interest of the organization.</li><li>You have to abide by all instructions and orders issued by the management in good spirit.</li><li>You will retire attaining the age limit fixed by the Bangladesh Govt. through Bangladesh Labor Act.</li></ol><p>&nbsp;</p><ol start='4'><li><strong>Transfer:</strong></li><li>Your Service is transferable from one project to another project of the Company for the greater interest of the Organization.</li><li>The Management may change your designation, duties and responsibilities from time to time as they think fit and proper without disturbing salary and allowances.</li></ol><p>&nbsp;</p><ol start='5'><li><strong>Termination of Service:</strong></li><li>The Management reserves the right to terminate your service at any time without assigning any reason, if your work, attitude or behavior not found satisfactory.</li><li>Either party may however, terminate the contract of employment by giving a notice period of 60 (Sixty) days in writing or in lieu thereof an equivalent of two months&rsquo; basic salary, will have to be paid by the company / surrendered by you in case of failure in giving two months&rsquo; prior notice after confirmation of service.</li><li>During the probation period, either party may terminate the contract of employment with 30 (Thirty) days prior notice.</li><li>When you intend to resign you will have to handover official charges to the nominated person of the Company.</li></ol><p>&nbsp;</p><ol start='6'><li><strong>Confidentiality:</strong></li></ol><p>You shall not, at any time, during the continuance or even after the cessation of your employment hereunder, disclose or divulge or make public, except on legal obligations, either directly to any person, firm or company or use for yourself any trade secret or confidential, technical knowledge, formula, process, compositions, ideas or documents, concerning the business and affairs of the company or any of its dealings, transactions or affairs which you may have acquired from the company or have to your knowledge during the course of and incidental to your employment. If you disclose any such information to any other person(s) or organization, the Company shall prosecute against you for such breach of code of conducts, as it considers necessary to protect its interest and enforce its rights.</p><p>&nbsp;</p><h2>Annexure &ndash; A</h2><p>&nbsp;</p><p><strong>Dear Mr. Abdullah,</strong></p><p>&nbsp;</p><p>You shall be placed at <strong>Grade-2</strong>, the monthly Gross salary of <strong>Tk. 6,000.00 (Six Thousand)</strong> only which is broken down as follows:</p><p>&nbsp;</p><p>&nbsp;<strong><u>Particulars &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; In Taka</u></strong></p><p><strong><u>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3,600.00&nbsp;&nbsp;</u></strong></p><p><strong><u>House Rent &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;1,800.00</u></strong></p><p><strong><u>Conveyance Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;330.00</u></strong></p><p><strong><u>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;270.00</u></strong></p><p><strong><u>Total Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;6,000.00 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;(Six Thousand)</u></strong></p><p>&nbsp;</p><p>If you are agreeable to the aforesaid offer, please acknowledge this letter by way of formal acceptance and return immediately for further action.</p><p>&nbsp;We have the pleasure in welcoming you and sincerely hope that our company will get benefited from your service.</p><p>&nbsp;</p><p>For <strong>Star Paradise Ltd</strong>,</p><p>&nbsp;</p><p>&nbsp;<strong>(Moshiur Hossain Uday)</strong></p><p><strong>Managing Director</strong></p><p>&nbsp;</p><p>I<strong>, Abdullah Al Noman</strong> have fully understood the contents of the letter of appointment and willingly agree to abide by the terms and conditions as stipulated herein above.</p><p>&nbsp;</p><p>&nbsp;</p><p>______________________</p><p>Signature of the Employee</p><p>&nbsp;</p><p>Date: __________________</p><p><strong>&nbsp;</strong></p><p><strong>Copy to:</strong></p><ol><li>HRIS</li><li>Personal File</li></ol>";
                    break;
                //appoinment letter for factory    
                case "10002":
                    lbody = "<p><strong>Ref: SPL/HR/Apt/16/223</strong></p><p><strong>Sep 01, 2016</strong></p><p><strong>" + name + "</strong></p><p><strong>C/O: Md. Abdul Muttalib&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Gibanda ( Sharkar bar), PO: Gibanda</strong></p><p><strong>PS: Islampur, Dist: Jamalpur&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>&nbsp;</strong></p><p><strong>Subject: <u>Letter of Appointment.</u></strong></p><p>&nbsp;</p><p>Dear <strong>" + name + "</strong>,</p><p>&nbsp;</p><p>With reference to your application and subsequent interview with us, we have the pleasure to inform you that the Management of Star Paradise Ltd is pleased to appoint you as <strong>  " + Desig + "</strong> in our Company <strong> " + comnam + " </strong>on the following terms and conditions:</p><p>&nbsp;</p><ol><li><strong>Commencement and nature of Appointment:</strong></li><li>Your appointment is effective from <strong>Sep 01, 2016</strong>.</li><li>Your appointment is initially on probation basis for 6 (Six) months from the date of your joining. On satisfactory completion of your probationary period, your service may be confirmed. Otherwise, your probationary period may be extended for such a period as may be decided upon by the Management.</li><li>Your place of posting shall be at <strong>'Factory&rdquo;</strong> and you shall work under the supervision of <strong>Divisional Head</strong>.</li></ol><p>&nbsp;</p><ol start='2'><li><strong>Placement, Compensation and other benefits:</strong></li><li>You will be entitled to Festival Bonuses as per Company policy, which are two (02) Eid Bonus, 60% of your Gross Salary each.</li><li>Your monthly salary as in &ldquo;Annexure-A&rdquo; only, which includes all your perquisites and allowances. Compensation will be governed by the rules of the Company on the subject, as applicable and / or amended hereafter.</li><li>Personal Income Tax, if any will be on your account and will be deducted each month by the company at source at the time of monthly salary disbursement for onward submission to the relevant Income Tax authorities.</li><li>You are not to disclose / discuss your salary with anyone related to this organization and keep it strictly confidential.</li><li>This is a position of full time and continuous responsibilities and will not engage yourself any Part-time work, profession or employment without written permission from the management.</li><li>You will be entitled with other benefits of the organization time to time as per the Company policy.</li></ol><p>&nbsp;</p><ol start='3'><li><strong>Duties and responsibilities: </strong></li><li>You will carry on with the duties and responsibilities entrusted to you and also the duties and responsibilities that may be entrusted to you by the Management from time to time. You will require working late hours whenever necessary for the greater interest of the organization.</li><li>You have to abide by all instructions and orders issued by the management in good spirit.</li><li>You will retire attaining the age limit fixed by the Bangladesh Govt. through Bangladesh Labor Act.</li></ol><p>&nbsp;</p><ol start='4'><li><strong>Transfer:</strong></li><li>Your Service is transferable from one project to another project of the Company for the greater interest of the Organization.</li><li>The Management may change your designation, duties and responsibilities from time to time as they think fit and proper without disturbing salary and allowances.</li></ol><p>&nbsp;</p><ol start='5'><li><strong>Termination of Service:</strong></li><li>The Management reserves the right to terminate your service at any time without assigning any reason, if your work, attitude or behavior not found satisfactory.</li><li>Either party may however, terminate the contract of employment by giving a notice period of 60 (Sixty) days in writing or in lieu thereof an equivalent of two months&rsquo; basic salary, will have to be paid by the company / surrendered by you in case of failure in giving two months&rsquo; prior notice after confirmation of service.</li><li>During the probation period, either party may terminate the contract of employment with 30 (Thirty) days prior notice.</li><li>When you intend to resign you will have to handover official charges to the nominated person of the Company.</li></ol><p>&nbsp;</p><ol start='6'><li><strong>Confidentiality: </strong></li></ol><p>You shall not, at any time, during the continuance or even after the cessation of your employment hereunder, disclose or divulge or make public, except on legal obligations, either directly to any person, firm or company or use for yourself any trade secret or confidential, technical knowledge, formula, process, compositions, ideas or documents, concerning the business and affairs of the company or any of its dealings, transactions or affairs which you may have acquired from the company or have to your knowledge during the course of and incidental to your employment. If you disclose any such information to any other person(s) or organization, the Company shall prosecute against you for such breach of code of conducts, as it considers necessary to protect its interest and enforce its rights.</p><p>&nbsp;</p><h2>Annexure &ndash; A</h2><p>&nbsp;</p><p><strong>Dear " + name + " ,</strong></p><p>&nbsp;</p><p>You shall be placed at <strong>Grade-2</strong>, the monthly Gross salary of <strong>Tk. " + ttlsalary + " (" + inwords + ")</strong> only which is broken down as follows:</p><p>" + tablesale + "</p><p>&nbsp;</p><p>If you are agreeable to the aforesaid offer, please acknowledge this letter by way of formal acceptance and return immediately for further action.</p><p>&nbsp;We have the pleasure in welcoming you and sincerely hope that our company will get benefited from your service.</p><p>&nbsp;For <strong>Star Paradise Ltd</strong>,</p><p>&nbsp;</p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p><p>I, <strong> " + name + " </strong> have fully understood the contents of the letter of appointment and willingly agree to abide by the terms and conditions as stipulated herein above.</p><p>Yours Sincerely,</p><p>______________________</p><p>Signature of the Employee</p><p>&nbsp;</p><p>Date: __________________</p><p><strong>&nbsp;</strong></p><p><strong>Copy to:</strong></p><ol><li>HRIS</li><li>Personal File</li></ol>";
                    break;

                //offer later for sales department;
                case "10003":
                    lbody = "<p style=margin-bottom:0><br/><br/>Ref: bti/HR/2021/</p><p>To</p><p style=margin-bottom:0><strong>" + name + "</strong></p><p style=margin-bottom:0>Address: House: 271, Tejgaon I/A,</p><p style=margin-bottom:0>Tejgaon, Dhaka-1208</p><p style=margin-bottom:0><br>Mobile : 01913169818</p><p><strong>Subject: Offer for Employment</strong></p><p><br>Dear <strong>" + name + "</strong></p><p>With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong> " + Desig + ", " + dptdesc + ",</strong> in " + depart + ", with effect from <strong> " + cdate + " </strong>.<p>Before that you are requested to submit a copy of your resignation letter, which has been duly received by your present employer within 3 (Three) days from the date of receipt of this offer letter and a clearance letter at the time of joining.</p></p><p>&nbsp;The Letter of Appointment will be issued soon.</p><p>&nbsp;Please bring the following papers on the date of joining:</p><ol><li>Photocopy of all academic certificates</li><li>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer</li><li>Passport size photograph -3</li><li>Photocopy of passport /Photocopy of National ID Card</li><li>Salary Certificate/ Bank Statement</li><li>300 (100*3) Tk Stamp Paper</li></ol><p>&nbsp;</p><p>Yours Sincerely,</p><p class='pImage'><strong></p>  <p class='pUname'><strong>" + "Brig Gen Mohammad Ayub Ansary, psc (Retd)" + "</p> <p>" + "Additional Managing Director and Head of HR" + "</p><p>" + "Admin and Security Department" + "</p>";

                    break;
                //offer Later For general
                case "10004":
                    lbody = "<p><strong>SPL/HR/Ofr/16/559</strong></p><p><strong>" + name + "</strong></p><p><strong>Vill: West Vashanchar, PO: Ambikapur</strong></p><p><strong>PS: Faridpur, Dist: Faridpur</strong></p><p>&nbsp;</p><p>&nbsp;Dear <strong>" + name + "</strong></p><p>&nbsp;</p><p>&nbsp;<span style='text-decoration: underline;'><strong>Offer for Employment</strong></span></p><p>&nbsp;With reference to our discussions with you and your willingness to join our company, we are pleased to offer you appointment as  <strong>&ldquo;Plant Manager-Pole Project&rdquo; </strong>in <strong>" + comnam + ".</strong> .”, which shall commence on or before  <strong>11 December 2021.</strong></p><p>&nbsp;</p><p>Before that you are requested to submit a copy of your resignation letter, which has been duly received by your present employer within 3 (Three) days from the date of receipt of this offer letter and a clearance letter at the time of joining.</p><p><br>The Letter of Appointment will be issued soon.</p><p>Please bring the following papers on the date of joining: </p><ol><li>1.	Original & photocopies of release letter / letter of acceptance of resignation in the Company Letter Head from the previous employer.</li><li>Original & photocopies of all certificates (experience, academic, professional courses etc).</li><li>Photocopy of National ID card/ Passport (employee and nominee).</li><li>Passport size photograph (employee 7 copies and Nominee 3 copies).</li><li>Pay Slip/ Proof of Salary and ETIN.</li></ol><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp; &nbsp; Yours Sincerely,</p>"; 
                    break;
                //confirmation letter
                case "10005":
                    lbody = "<p>&nbsp;Ref.: SPL<strong>/</strong>HR/Conf/<strong>555/16 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</strong>Date: December 1, 2016</p><p>&nbsp;<strong>" + name + ", </strong>Staff ID #<strong>128</strong></p><p><strong>STSM, Bagerhat</strong></p><p><strong>Khulna.</strong></p><p>&nbsp;</p><p>&nbsp;Subject:&nbsp; <strong><u>Confirmation of Service</u></strong></p><p>&nbsp;</p><p><strong>Dear " + name + ",</strong></p><p><strong>&nbsp;</strong><strong>&nbsp;</strong>This has reference to your appointment letter.</p><p>&nbsp;We are pleased to inform you that, on successful completion of your period of probation, your services in " + comnam + " have been confirmed effective from <strong>3 November, 2016.</strong></p><p>&nbsp;We hope that you will extend your full support and cooperation to promote company activities and growth in the days to come.</p><p>&nbsp;</p><p>&nbsp;</p><p>Yours faithfully,</p><p>&nbsp;</p><p>&nbsp;<strong>Moshiur Hossain </strong></p><p><strong>Managing Director.</strong></p><p>&nbsp;CC: &nbsp;</p><p>HRIS</p><p>Personal File</p>";
                    break;
                // extension letter
                case "10006":
                    lbody = "<p><strong>Ref. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : SPL/HR/Exten/511/15</strong></p><p>Date &nbsp;&nbsp;&nbsp;&nbsp; : December 1, 2016</p><p>&nbsp;<strong>" + name + "</strong></p><p><strong>Staff ID # 124</strong></p><p>" + Desig + "</p><p>" + depart + "</p><p>&nbsp;Subject: <strong>Unsatisfactory Performance during Probationary Period.</strong></p><p>&nbsp;</p><p>Dear Mr. <strong>" + name + "</strong>,</p><p>&nbsp;Please be informed that after a careful review of your performance during probation period, it is found that you have failed to show the satisfactory performance, which is required for your position. Due to your unsatisfactory performance, we are unable to confirm you after six months&rsquo; probation period.</p><p>Your supervisor has suggested areas for improvement in your probationary assessment sheet, and it is expected that you will seek every opportunity to improve the highlighted areas.Therefore, through this letter you are placed under observation for <strong>3 months</strong> beginning from, <strong>01.11.2016.</strong> After completion of the observation period, your performance will re-evaluated by your supervisor. If you fail to show satisfactory performance during that period, your service may be no longer required for the organization.</p><p>&nbsp;</p><p>Thank you and hope you will make every efforts to improve yourself within <strong>next three months</strong>.</p><p>&nbsp;</p><p><strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p>&nbsp;CC:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; HRIS</p><p>Personal File&nbsp;</p>";
                    break;
                //promotion letter
                case "10007":
                    lbody = "<p style='text-align: center;'>&nbsp;</p><h3 style='text-align: center;'><span style='text-decoration: underline;'><strong>Private &amp; Confidential</strong></span></h3><p>&nbsp;<strong>Ref: SPL/HR/Prom/489/16&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong></p><p><strong>16 November, 2016</strong></p><p><strong>&nbsp;</strong><strong>" + name + ", ID: 278</strong></p><p>" + Desig + ",</p><p>" + depart + "</p><p>&nbsp;<strong>Subject: Promotion</strong></p><p>&nbsp;Dear Mr. <strong>" + name + "</strong>,</p><p>&nbsp;We are pleased to inform you that, the company have decided to promote you to the position of <strong><u>Junior Territory Sales Manager</u></strong> recognition of your performance, effective December 1, 2016.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 360px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 360px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 360px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 360px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 360px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>We acknowledge your excellent performance and congratulate you on your well-deserved promotion. We hope you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;<strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p><strong><u>Copy to:</u></strong></p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HRIS</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal File</p>";
                    break;
                //increment letter
                case "10008":
                    lbody = "<p style='text-align: center;'>&nbsp;</p><p style='padding-left: 360px;'>&nbsp;</p><h3 style='text-align: center;'><span style='text-decoration: underline;'><strong>&nbsp;</strong><strong>Private &amp; Confidential</strong></span></h3><p>&nbsp;</p><p><strong>REF: SPL/HR/INCREMENT/16</strong></p><p><strong>Date: July 12, 2016</strong></p><p>&nbsp;<strong>" + name + ", ID: 101</strong></p><p><strong>" + Desig + ",</strong></p><p>" + depart + "</p><p><strong>Factory.</strong></p><p>&nbsp;</p><p><strong>Subject: Increment of Salary</strong></p><p>&nbsp;</p><p><strong>Dear " + name + ",</strong></p><p>&nbsp;We are pleased to inform you that the management has decided to review your monthly gross salary in recognition of your performance during the year 2015-2016, effective from <strong>July 01, 2016</strong>.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 330px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 330px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 330px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 330px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 330px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>&nbsp;We acknowledge your good performance and hope that you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;We wish you all the best and look forward to better performance in future.</p><p>&nbsp;</p><p>With best regards</p>";//<p>&nbsp;<strong>Moshiur Hossain Uday</strong></p><p>Managing Director</p><p>&nbsp;CC: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>Personal File</p><p>HRIS</p><p>&nbsp;</p>";
                    break;
                //transfer lettre
                case "10009":
                    lbody = "<p>&nbsp;<strong>Ref: SPL/HR/TL/558/16</strong></p><p><strong>December 10, 2016</strong></p><p><strong>Mr.</strong> <strong>" + name + "</strong> <strong>(ID # 202)</strong></p><p><strong>TSM,</strong><strong>Dhaka South</strong></p><p>&nbsp;</p><p>&nbsp;Subject: <strong><u>Transfer of Service </u></strong></p><p><br /> <strong>Dear Mr. </strong><strong>" + name + "</strong>,</p><p>&nbsp;In consideration of the exigencies of the Company, it has been decided to transfer you from <strong>Dhaka South (Munshigonj) to Faridpur (Barishal) </strong>effective from <strong>20 Dec, 2016</strong>.</p><p>&nbsp;Please note that the terms and conditions of your service shall not be changed due to this transfer.</p><p>&nbsp;It is expected that you will continue to provide your best services in achieving business goals and objectives of the Company in the days to come.</p><p>&nbsp;Wish you a happy career in Star Paradise Limited.</p><p>&nbsp;</p><p>Yours sincerely<br /> &nbsp;</p>"; //<p>&nbsp;_______________________</p><p><strong>Ridwan Rouf Khan</strong></p><p>Assistant Manager-Human Resources</p><p>&nbsp;&nbsp; C.C</p><ul><li>HRIS</li><li>Personal File</li></ul><p>&nbsp;</p>";
                    break;
                //acceptance of resignation
                case "10010":
                    lbody = "<p>July 13, 2016</p><p>&nbsp;<strong>" + name + ", ID: 106</strong></p><p>CMO,</p><p>" + Desig + "</p><p>" + depart + "</p><p>&nbsp;Subject:<strong><u> Acceptance of Resignation</u></strong></p><p>&nbsp;</p><p>Dear Mr. <strong>" + name + "</strong>,</p><p>This with reference to your letter dated <strong>July 1, 2016</strong> in which you have expressed your inability to continue your service with the organization. We would like to inform you that the management has accepted your resignation with effect from <strong>July 11, 2016.</strong></p><p>Accordingly, you will be released from your work at the close of business of <strong>July 10, 2016</strong> subject to a clearance certificate being issued to you by the concerned departments to the effect that you do not owe to <strong>Fidelity Holdings Ltd</strong> any outstanding dues and or any liabilities thereof.</p><p>You are, also, requested to submit the Identity Card and others official things to HR Department to facilitate your quick clearance from the service.</p><p>We take this opportunity to wish you well and success in all your future endeavors.</p><p>&nbsp;</p><p>&nbsp;</p><p>Sincerely,</p>"; 
                    break;
                //release letter
                case "10011":
                    lbody = "<p><strong>June 04, 2015</strong></p><p><strong>" + name + "</strong></p><p>" + Desig + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>Accounts&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>" + comnam + "</p><p><strong>&nbsp;</strong></p><p>&nbsp;</p><h3 style=" + "text-align: center;" + "><span style=" + "text-decoration: underline;" + "><strong>RELEASE LETTER</strong></span></h3><p>&nbsp;</p><p>&nbsp;<strong>Dear Mr. </strong><strong>" + name + "</strong><strong>,</strong></p><p><strong>&nbsp;</strong>With reference to, your letter dated <strong>March 05, 2015;</strong> you are hereby released from the service of <strong>" + comnam + "</strong> as at close of business on <strong>April 30, 2015</strong>.</p><p>&nbsp;We wish you all success in life.</p><p>&nbsp;</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;</p><p><strong>Ridwan Rouf Khan</strong></p><p><strong>Assistant Manager-Human Resources.</strong></p>";
                    break;


                //experence certificate for current employee
                case "10012":
                    lbody = "<div class='printHeader'> <p style='text-align:right'> <br /><br /><br /><br /><br /><br /><br />  Date: " + cdate + "</p><p class='cfHead'>To Whom it May Concern</p><p></br></br></br><br></br></br></br></p><p> This is to certify that <strong>" + name + "</strong>, <strong> " + Desig + " </strong> -  <strong>" + dptdesc + ", " + " <strong> Employee ID No. " + idCard + " </strong> working in " + comnam + " from <strong>" + jdate + " to till date</strong>.</p><p>The organization has never found any serious misconduct from his end and has no objection recommending him.</p><p>&nbsp;</p><p>I wish him all round success.</p><p>&nbsp;</p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                    break;
                ///Salary certificate
                case "10015":
                    lbody = "<div class='printHeader'> <p style='text-align:right'><br /><br /><br /><br /><br /><br /><br />Date: " + cdate + "</p><p class='cfHead'>To Whom it May Concern</p><p></br></br></br><br></br></br></br></p><p>This is to certify that <strong>" + name + "</strong>, <strong> " + Desig + " </strong> -  <strong>" + dptdesc + ", " + "</strong> has been working as a regular employee in our company since  <strong>" + jdate + "</strong>.</p><p class='pMargin'><strong>Breakdown of his monthly salary stands as follow:</strong></p><div style='width:400px; margin:0 auto;'>" + tablesale + "</div><p>&nbsp;</p><p><strong>" + inwords + "</strong></p><p>&nbsp;</p> <p class='pFottext'>I hereby certify that the above mentioned information is correct and accurate to the best of my knowledge it also noticeable that company will not be responsible for any sort of loan or personal transection of the above mentioned employee </p>  <br><br><hr width='20%'><p class=''><strong><u><b>HR & Administrator</b></u></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                    break;
                //Joining Letter for factory    
                case "10020":
                    lbody = "<div class='printHeader'><p>Ref: SPL/HR/Appt/16/524</p><p>" + cdate + "</p><p><strong>" + name + "</strong></p><p><strong>C/O: Abdul Bashar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Boro Hossainput, PO: Banglabazar-3822,</strong></p><p><strong>PS: Begumganj, Dist: Noakhali&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong>Subject: <u>Confirmation of Service.</u></strong></p><p>&nbsp;</p> <p></br></br></br><br></br></br></br></p><p>Dear <strong>" + name + "</strong>,</p><p>This has reference to your appointment letter.</p><p>We are pleased to inform you that, on successful completion of your period of probation, your services in Star Paradise Ltd have been confirmed effective from 3 November, 2016. </p><p>We hope that you will extend your full support and cooperation to promote company activities and growth in the days to come.</p>Yours faithfully,<p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                    break;
                case "10021":
                    lbody = "<div><p>Date-</p></div><div><p>To<br />The HRD<br />Acme Technologies Ltd.<br />House- 630, Road-9, Mirpur DOHS,<br />Dhaka-1216.</p></div>" +
                    "<div><p>Dear concern,<br />In response to your offer letter provided dated on &hellip;&hellip;&hellip;........ in connection with the interviews held on" + "&hellip;&hellip;........, I&rsquo;d like to inform that I&rsquo;ve joined your organization today at &hellip;................ as an &hellip;................in the" + " &hellip;&hellip;....................... department.</p></div>" +
                    "<div><p>I thanks for providing me the opportunity to serve the institute. I&rsquo;ll perform my duties sincerely, honestly and to the best of my abilities.</p>" +
                    "</div><div><p>I therefore, request you to accept my joining letter. Please, accord and oblige.</p></div>Sincerely yours,";
                    break;
                case "10022":
                    lbody = "<div style='text-align: left;'><br /><br /><br /> Date...................................</div><div style='text-align: left;'>&nbsp;</div>" +
                            "<div style='text-align: center;'><strong>To Whom It May Concern</strong></div><div style='text-align: left;'>&nbsp;</div>" +
                            "<div style='text-align: left;'>This is to certify that Mr. /Ms. ................................., worked in our organization, ACME" + "Technologies Ltd as an Executive in our ............... dept. from&nbsp; ................................(D-M-Y) to .............................(D-M-Y)." + "<br /><br /></div><div style='text-align: left;'>During his/ her tenure with us, we found him/her to be a sincere, hardworking, loyal " + "and orks well as part of a team.<br /><br /></div><div style='text-align: left;'>We thank her/" + "him for her/his contribution and wish her/him success in her/his future endeavors.</div>" + "<div style='text-align: left;'>&nbsp;</div>" + "<div style='text-align: left;'>Best " + "regards<br /><br /></div><div style='text-align: left;'>Name&nbsp; &nbsp;" +
                            " &nbsp; &nbsp; ..........................................<br /><br /></div><div style=text-align: " +
                            "left;'>Position: ..........................................<br /><br /></div><div style='text-align: left;'>ACMETechnologies Ltd</div><div>&nbsp;" +
                            "</div>";
                    break;
                case "10023":
                    lbody = "<div style='text-align: left;'><p style='text-align: left;'>Date-d/m/y<br /><br />Mr. ................<br />House no- &hellip;.......... " +
                              "Road-...............<br />Dhaka<br /><br />Dear .....................,</p><p style='text-align: left;'>I regret to inform you that your" +
                            " employment with ACME Technologies Ltd. is terminated effective as of 10<sup>th</sup> Sep, 18.<br /><br />The reasons for your termination" +
                            " are as follows:<br /" +
    ">&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "<br />&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "<br />&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "</p><p style='text-align: left;'>Within &hellip;&hellip;&hellip;.. Days of the effective date, you must return all company documents and " +
                              "property to the company within this date (d/m/y). (Optional) [According to our records, the following company property is in your" +
                            " possession:<br /" +
                            ">&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "<br />&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
     "</p><p style='text-align: left;'>Sincerely yours,</p><p style='text-align: left;'>(Appointing Authority)</p></div><div style='text-align: left;'>&nbsp;</div>";
                    break;


                default:
                    break;
            }
            return lbody;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.txtml.Text = "";
            if (chkpre.Checked)
            {
                this.PreviousD();
            }
            else
            {
                this.ShowView();
            }
        }


        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            this.txtml.Text = this.data(type); 
        }

        private void PreviousD()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            string empid = this.ddlPrevious.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", empid, type, "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            this.ddlEmployee.Items.Clear();
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();

            string lett = (string)ds3.Tables[0].Rows[0]["LETTDESC"];
            this.txtml.Text = lett;
            ViewState["letter"] = ds3.Tables[0];
        }

    }
}