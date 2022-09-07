using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Net.Mail;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{
    public partial class RptLoanStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string TypeDesc = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (TypeDesc == "Loan" ? "CUSTOMER LOAN  " : (TypeDesc == "Letter" ? "CUSTOMER LETTER "
                        : (TypeDesc == "Registration" ? "CUSTOMER REGISTRATION" : (TypeDesc == "ADWork" ? "CLIENT'S MODIFICATION" : "")))) + " INFORMATIOIN VIEW/EDIT";
                this.GetProjectName();
                this.ViewSection();
                this.chekAss.Visible = false;
                this.panAss.Visible = false;

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }


        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Loan":
                    this.lblDate.Visible = false;
                    this.txtdate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Letter":
                    this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


                case "Registration":
                    this.lblProject.Visible = false;
                    this.txtSrcPro.Visible = false;
                   // this.ibtnFindProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.lblDate.Visible = false;
                    this.txtdate.Visible = false;
                    this.lbtnOk.Visible = false;
                    this.lbtnOk_Click(null, null);
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "SendOnlineLetter":
                    this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "ADWork":

                    this.lblDate.Visible = false;
                    this.txtdate.Visible = false;
                    this.GetUnitName();
                    this.MultiView1.ActiveViewIndex = 3;
                    return;

                case "AddTopSheet":
                    this.lblDate.Visible = false;
                    this.txtdate.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.GetUnitName();
                    this.MultiView1.ActiveViewIndex = 4;
                    return;
            }


        }
        private void GetUnitName()
        {
            Session.Remove("tblunit");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Unitname = this.txtSrcUnit.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETUNITNAME", pactcode, Unitname, "", "", "", "", "", "", "");

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "ADWork":
                    this.ddlUnitName.DataTextField = "udesc";
                    this.ddlUnitName.DataValueField = "usircode";
                    this.ddlUnitName.DataSource = ds1.Tables[0];
                    this.ddlUnitName.DataBind();
                    break;

                case "AddTopSheet":
                    this.ddlCustName.DataTextField = "uacustname";
                    this.ddlCustName.DataValueField = "usircode";
                    this.ddlCustName.DataSource = ds1.Tables[0];
                    this.ddlCustName.DataBind();
                    break;

            }

            Session["tblunit"] = ds1.Tables[0];


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowSectionVal();
        }
        private void ShowSectionVal()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Loan":
                    this.ShowLoanStatus();
                    break;

                case "Letter":
                case "SendOnlineLetter":
                    this.PanelSendMail.Visible = (Type == "SendOnlineLetter") ? true : false;
                    this.ShowLetterInfo();
                    this.chekAss.Visible = true;
                    break;

                case "Registration":
                    //this.lblProject.Visible = false;
                    //this.ibtnFindProject.Visible = false;
                    //this.ddlProjectName.Visible = false;
                    //this.lblDate.Visible = false;
                    //this.txtdate.Visible = false;
                    this.ShowCustRegistration();
                    break;

                case "ADWork":
                    this.ShowMaintenanceWork();
                    break;


                case "AddTopSheet":
                    this.ShowTopSheetMainWork();
                    break;

            }


        }



        private void ShowLoanStatus()
        {
            Session.Remove("tblloan");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTLOANSTATUS", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLoan.DataSource = null;
                this.gvLoan.DataBind();
                return;
            }
            Session["tblloan"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void ShowLetterInfo()
        {
            Session.Remove("tblloan");
            this.lblletterinfo.Visible = true;
            this.lblClientInfo.Visible = true;
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTLETTERANDCUST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLetter.DataSource = null;
                this.gvLetter.DataBind();
                this.gvClientLetter.DataSource = null;
                this.gvClientLetter.DataBind();
                return;

            }

            this.gvLetter.DataSource = ds1.Tables[0];
            this.gvLetter.DataBind();
            Session["tblloan"] = ds1.Tables[1];
            this.Data_Bind();

        }

        private void ShowCustRegistration()
        {

            Session.Remove("tblloan");
            string comcod = this.GetComeCode();
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTREGISINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvRegis.DataSource = null;
                this.gvRegis.DataBind();

                return;

            }

            Session["tblloan"] = ds1.Tables[0];
            this.Data_Bind();
        }





        private void SendLetterOnline()
        {

            string comcod = this.GetComeCode();
            this.ConfirmMessage.Visible = true;
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string Letcode = "";
            for (int i = 0; i < this.gvLetter.Rows.Count; i++)
            {
                if (((CheckBox)this.gvLetter.Rows[i].FindControl("chkletter")).Checked)
                {

                    Letcode = ((Label)this.gvLetter.Rows[i].FindControl("lgvletcode")).Text.Trim();
                    break;
                }

            }

            DataView dv = ((DataTable)Session["tblloan"]).DefaultView;
            dv.RowFilter = ("chk='True'");
            DataTable dt = dv.ToTable();

            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTLETTERINFO", Letcode, "", "", "", "", "", "", "", "");
            //  string Title = ds1.Tables[0].Rows[0]["letdesc"].ToString().Trim();
            string subject = ds1.Tables[0].Rows[1]["letdesc"].ToString().Trim();
            this.txtDescription.Text = ds1.Tables[0].Rows[2]["letdesc"].ToString().Trim();
            //Via Google
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, password);
            //client.UseDefaultCredentials = false;
            //client.Credentials = credentials;
            // string frmemail = "emdadul.cse03@gmail.com";
            // string password = "01719862989";

            //Attach file using FileUpload Control and put the file in memory stream
            //if (FileUpload1.HasFile)
            //{
            //    mail.Attachments.Add(new Attachment(FileUpload1.PostedFile.InputStream, FileUpload1.FileName));
            //}

            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            SmtpClient client = new SmtpClient(hostname, portnumber);
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            client.EnableSsl = false;
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            ///////////////////////

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(frmemail);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["email"].ToString().Trim() != "")
                    msg.To.Add(new MailAddress(dt.Rows[i]["email"].ToString().Trim()));

            }


            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + this.txtDescription.Text + "</pre></body></html>");
            try
            {
                client.Send(msg);
                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            }




        }

        private void ShowMaintenanceWork()
        {
            Session.Remove("tblloan");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usricode = this.ddlUnitName.SelectedValue.ToString();
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMAINTENANCEWORK", pactcode, usricode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvadwrk.DataSource = null;
                this.gvadwrk.DataBind();

                return;

            }

            Session["tblloan"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private void ShowTopSheetMainWork()
        {
            Session.Remove("tblloan");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usricode = this.ddlCustName.SelectedValue.ToString();
            string group = this.ddlgroup.SelectedValue.ToString();
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTTOPSHEETMAINWORK", pactcode, usricode, group, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtopsheetmwrk.DataSource = null;
                this.gvtopsheetmwrk.DataBind();

                return;

            }

            Session["tblloan"] = ds1.Tables[0];
            this.Data_Bind();

        }





        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grpcod = dt1.Rows[0]["grpcod"].ToString();
            string adno = dt1.Rows[0]["adno"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grpcod"].ToString() == grpcod && dt1.Rows[j]["adno"].ToString() == adno)
                {
                    grpcod = dt1.Rows[j]["grpcod"].ToString();
                    adno = dt1.Rows[j]["adno"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["adno"] = "";
                    dt1.Rows[j]["addate"] = "";
                    //dt1.Rows[j]["addate1"] = "";


                }

                else
                {
                    if (dt1.Rows[j]["grpcod"].ToString() == grpcod)
                    {

                        dt1.Rows[j]["grpdesc"] = "";
                    }

                    if (dt1.Rows[j]["adno"].ToString() == adno)
                    {

                        dt1.Rows[j]["adno"] = "";
                        dt1.Rows[j]["addate"] = "";
                        //dt1.Rows[j]["addate1"] = "";

                    }
                    if (ASTUtility.Right(dt1.Rows[j]["adno"].ToString(), 4) == "AAAA")
                    {
                        dt1.Rows[j]["adno"] = "";
                    }
                    grpcod = dt1.Rows[j]["grpcod"].ToString();
                    adno = dt1.Rows[j]["adno"].ToString();

                }



            }

            return dt1;

        }

        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();


            switch (Type)
            {
                case "Loan":
                    this.gvLoan.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvLoan.DataSource = (DataTable)Session["tblloan"]; ;
                    this.gvLoan.DataBind();
                    this.FooterCal();
                    break;

                case "Letter":
                case "SendOnlineLetter":
                    this.gvClientLetter.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvClientLetter.DataSource = (DataTable)Session["tblloan"];
                    this.gvClientLetter.DataBind();
                    break;


                case "Registration":
                    this.gvRegis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvRegis.DataSource = (DataTable)Session["tblloan"];
                    this.gvRegis.DataBind();
                    //this.Repeater1.DataSource = (DataTable)Session["tblloan"];;
                    //this.Repeater1.DataBind();
                    break;

                case "ADWork":
                    this.gvadwrk.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvadwrk.DataSource = (DataTable)Session["tblloan"];
                    this.gvadwrk.DataBind();
                    //this.Repeater1.DataSource = (DataTable)Session["tblloan"];;
                    //this.Repeater1.DataBind();
                    break;


                case "AddTopSheet":

                    this.gvtopsheetmwrk.DataSource = (DataTable)Session["tblloan"];
                    this.gvtopsheetmwrk.DataBind();
                    //this.Repeater1.DataSource = (DataTable)Session["tblloan"];;
                    //this.Repeater1.DataBind();
                    break;




            }

        }

        private void FooterCal()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblloan"];
            if (dt.Rows.Count == 0)
                return;
            switch (Type)
            {
                case "Loan":
                    ((Label)this.gvLoan.FooterRow.FindControl("lblgvFLnamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "Letter":
                    break;
            }





        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Loan":
                    this.LoanStatus();
                    break;

                case "Letter":
                    this.ClientLetterInfo();
                    break;

                case "Registration":
                    this.PrintCustRegistation();
                    break;

                case "ADWork":
                    this.PrintMaintenanceWork();
                    break;

                case "AddTopSheet":
                    this.PrintTopSheet();
                    break;

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = Type;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }


        private void LoanStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblloan"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.LoanStatus>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptCustLnStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Customer Loan Status"));
            Rpt1.SetParameters(new ReportParameter("projectName", this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void ClientLetterInfo()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Letcode = "";
            for (int i = 0; i < this.gvLetter.Rows.Count; i++)
            {
                if (((CheckBox)this.gvLetter.Rows[i].FindControl("chkletter")).Checked)
                {

                    Letcode = ((Label)this.gvLetter.Rows[i].FindControl("lgvletcode")).Text.Trim();
                    break;
                }

            }
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTLETTERINFO", Letcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            string Title = ds1.Tables[0].Rows[0]["letdesc"].ToString().Trim();
            string subject = "Subject: " + ds1.Tables[0].Rows[1]["letdesc"].ToString().Trim();
            string Description = ds1.Tables[0].Rows[2]["letdesc"].ToString().Trim();
            if (!(chekAss.Checked))
            {
                this.SaveValue();
                DataView dv = ((DataTable)Session["tblloan"]).DefaultView;
                dv.RowFilter = ("chk='True'");

                DataTable dt1 = dv.ToTable();

                var lst = dt1.DataTableToList<RealEntity.C_24_CC.EClassClientLetterInfo>();
                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptCustLetter", lst, null, null);


                string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd MMMM, yyyy");

                Rpt1.SetParameters(new ReportParameter("title", Title));
                Rpt1.SetParameters(new ReportParameter("subject", subject));
                Rpt1.SetParameters(new ReportParameter("Description", Description));
                Rpt1.SetParameters(new ReportParameter("date", date));

                Session["Report1"] = Rpt1;

            }
            else
            {
                LocalReport Rpt2 = new LocalReport();

                DataView dv = ((DataTable)Session["tblloan"]).DefaultView;
                //dv.RowFilter = ("chk='True'");

                DataTable dt2 = dv.ToTable();
                var lst = dt2.DataTableToList<RealEntity.C_24_CC.EClassClientLetterInfo>();
                Rpt2 = RptSetupClass1.GetLocalReport("R_24_CC.RptCustLetter01", lst, null, null);
                string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd MMMM, yyyy");
                string custName = this.txtName.Text;
                string custAdd = this.txtAdd.Text;
                string custPhone = "Phone : " + this.txtMob.Text;
                Rpt2.SetParameters(new ReportParameter("title", Title));
                Rpt2.SetParameters(new ReportParameter("subject", subject));
                Rpt2.SetParameters(new ReportParameter("Description", Description));
                Rpt2.SetParameters(new ReportParameter("date", date));
                Rpt2.SetParameters(new ReportParameter("custName", custName));
                Rpt2.SetParameters(new ReportParameter("custAdd", custAdd));
                Rpt2.SetParameters(new ReportParameter("custPhone", custPhone));
                Session["Report1"] = Rpt2;

            }

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintCustRegistation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblloan"];

            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassCustRegistrationStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptLoanStatus", lst, null, null);

            string title = "Customer Registration Status";

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", title));

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMaintenanceWork()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string unitName = this.ddlUnitName.SelectedItem.Text.Trim();
            DataTable dt = (DataTable)Session["tblloan"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.ClientModification>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptNetADWork", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", projectName));
            Rpt1.SetParameters(new ReportParameter("txtUnitName", unitName));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "CLIENT'S MODIFICATION"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintTopSheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string usrdesig = hst["usrdesig"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;




            string project = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13).ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = System.DateTime.Now.ToString("dd.MM.yyyy");


            DataTable dt = (DataTable)Session["tblloan"];
            DataTable dtc = (DataTable)Session["tblunit"];
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string flatowner = dtc.Select("usircode='" + usircode + "'")[0]["custname"].ToString();
            string appartment = dtc.Select("usircode='" + usircode + "'")[0]["udesc"].ToString();



            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddTopSheet>();
            var lst1 = lst.FindAll(p => p.gcod == "29BBAAAAA");
            double NetAmt = lst1.Select(p => p.netamt).Sum();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_24_CC.RptAddTopSheet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("flatowner", flatowner));
            Rpt1.SetParameters(new ReportParameter("appartment", appartment));
            Rpt1.SetParameters(new ReportParameter("project", project));
            Rpt1.SetParameters(new ReportParameter("footer", printFooter));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "OPTIONAL BILL"));
            Rpt1.SetParameters(new ReportParameter("txtpreparedby", username));
            Rpt1.SetParameters(new ReportParameter("txtdesig", usrdesig));
            Rpt1.SetParameters(new ReportParameter("InWrd", "Total Payable Amount to SDL : " + ASTUtility.Trans(Math.Round(NetAmt), 2) + "."));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        protected void gvLoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLoan.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvClientLetter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvClientLetter.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblloan"];
            int i, index;
            for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
            {

                index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                dt.Rows[index]["chk"] = ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkletterc")).Checked ? "True" : "False";

            }

            Session["tblloan"] = dt;
        }

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblloan"];
            int i, index;
            if (((CheckBox)this.gvClientLetter.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
                {
                    ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkletterc")).Checked = true;
                    index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                    dt.Rows[index]["chk"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
                {
                    ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkletterc")).Checked = false;
                    index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                    dt.Rows[index]["chk"] = "False";

                }

            }

            Session["tblloan"] = dt;
        }
        protected void gvRegis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRegis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnSend_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.SendLetterOnline();
        }
        protected void ibtnFindUnit_Click(object sender, ImageClickEventArgs e)
        {
            this.GetUnitName();
        }
        protected void gvadwrk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbldesc = (Label)e.Row.FindControl("lblgvDesc");
                Label lblamt = (Label)e.Row.FindControl("lblgvAmount");
                Label lblgvdisamt = (Label)e.Row.FindControl("lblgvdisamt");
                Label lblgvnetAmount = (Label)e.Row.FindControl("lblgvnetAmount");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lbldesc.Font.Bold = true;
                    lblamt.Font.Bold = true;
                    lblgvdisamt.Font.Bold = true;
                    lblgvnetAmount.Font.Bold = true;
                }

            }
        }
        protected void gvadwrk_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvadwrk.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void chekAss_CheckedChanged(object sender, EventArgs e)
        {
            if (chekAss.Checked == true)
            {
                this.panAss.Visible = true;
            }
            else
            {
                this.panAss.Visible = false;
            }
            //this.panAss.Visible = false;
        }
        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void gvtopsheetmwrk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string group = this.ddlgroup.SelectedValue.ToString();
                Label lgvgdesc = (Label)e.Row.FindControl("lgvworkdesc");
                Label lblgvdemamt = (Label)e.Row.FindControl("lblgvdemamt");
                Label lblgvrefamt = (Label)e.Row.FindControl("lblgvrefamt");
                Label lblgvdisamt = (Label)e.Row.FindControl("lblgvdisamt");

                TextBox txtamt = (TextBox)e.Row.FindControl("txtgvAmt");
                string gcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();


                if (gcod == "")
                {
                    return;
                }

                if (group == "9" && gcod.Substring(6) == "000")
                {

                    lgvgdesc.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lblgvdemamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lblgvrefamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lblgvdisamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                    txtamt.Attributes["style"] = "color:maroon; font-weight:bold; text-align:right;";
                    //lgvgdesc.Font.Bold = true;
                    //lblgvdemamt.Font.Bold = true;
                    //lblgvrefamt.Font.Bold = true;
                    //lblgvdisamt.Font.Bold = true;
                    //txtamt.Font.Bold = true;

                }
                if (gcod.Substring(5) == "AAAA")
                {
                    lgvgdesc.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lblgvdemamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lblgvrefamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                    lblgvdisamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                    txtamt.Attributes["style"] = "color:maroon; font-weight:bold; text-align:right;";

                    //lgvgdesc.Font.Bold = true;
                    //lblgvdemamt.Font.Bold = true;
                    //lblgvrefamt.Font.Bold = true;
                    //lblgvdisamt.Font.Bold = true;
                    //txtamt.Font.Bold = true;
                }


            }
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            string comcod = this.GetComeCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlCustName.Text.Trim();


            DataTable dt1 = ((DataTable)Session["tblloan"]).Copy();
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("gcod in ('290101001', '290102001')");

            dt1 = dv.ToTable();
            foreach (DataRow dr2 in dt1.Rows)
            {

                string gcod = dr2["gcod"].ToString();
                string amt = dr2["netamt"].ToString();

                bool result = MktData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT", "INORUPDATECUSTUASER", PactCode, Usircode, gcod, amt, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                    return;

                }

            }


            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

        }
        protected void lbtnCalculation_Click(object sender, EventArgs e)
        {

            DataTable dt1 = (DataTable)Session["tblloan"];
            int count = dt1.Rows.Count;
            int i;

            for (i = 0; i < this.gvtopsheetmwrk.Rows.Count; i++)
            {
                string gcod = dt1.Rows[i]["gcod"].ToString();
                if (gcod == "290101001" || gcod == "290102001")
                    dt1.Rows[i]["netamt"] = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvtopsheetmwrk.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
            }

            double toamt = Convert.ToDouble(dt1.Rows[count - 4]["netamt"].ToString());
            double service = Convert.ToDouble(dt1.Rows[count - 3]["netamt"].ToString());
            double utility = Convert.ToDouble(dt1.Rows[count - 2]["netamt"].ToString());
            //double Adjustment = Convert.ToDouble(dt1.Rows[count - 3]["amt"].ToString());
            dt1.Rows[count - 1]["netamt"] = toamt + service + utility;
            Session["tblloan"] = dt1;
            this.Data_Bind();

        }
    }
}