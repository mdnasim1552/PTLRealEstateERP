using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using Microsoft.Reporting.WinForms;
using RealERPRPT;
using System.IO;
using System.Net.Mail;
using System.Globalization;

namespace RealERPWEB.F_23_CR
{
    public partial class RptReconcilationLetter : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));



                this.GetProjectName();
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtTodate.Text = date;

            }
        }
        protected void gvflrwisbill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvflrwisbill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            this.gvflrwisbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvflrwisbill.DataSource = dt;
            this.gvflrwisbill.DataBind();


        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvflrwisbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();



            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETALLSALESPROJECTS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "pactdesc";
            this.ddlprjlist.DataValueField = "pactcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            //this.GetSubContractor();
        }
        protected void lbtnok_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string @Desc3 = Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");

            //string csircode = "98%";//this.ddlcontractorlist.SelectedValue.ToString() == "000000000000" ? "98%" : this.ddlcontractorlist.SelectedValue.ToString() + "%";
            //string billtcode = "%";//this.ddlcatagory.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlcatagory.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCUSTOMERDETAILS", pactcode, "", @Desc3, "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }
            Session["RptReconcilationLetter"] = ds1.Tables[0];
            this.Data_Bind();

        }

        protected void ddlprjlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string SkipBeforeDelimeter(string originalString)
        {
            char delimiter = '-';
            int index = originalString.IndexOf(delimiter);
            if (index >= 0)
            {
                string newString = originalString.Substring(index + 1);
                return newString;
                // newString will now contain "Finlay dev"
            }
            else
            {
                return originalString;
            }
            
        }
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empName = dt.Rows[rowIndex]["name"].ToString();
            string unit= dt.Rows[rowIndex]["udesc"].ToString();
            unitLabel.Text = unit;
            projectLabel.Text= SkipBeforeDelimeter(ddlprjlist.SelectedItem.Text);

            //DateTime today = DateTime.Today;
            //string formattedDate = today.ToString("MMMM, yyyy", CultureInfo.InvariantCulture);
            //string suffix = GetDateSuffix(today.Day);

            DateTime toDate = DateTime.ParseExact(txtTodate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            int day = toDate.Day;
            string formattedDate = Convert.ToDateTime(this.txtTodate.Text).ToString("MMMM, yyyy", CultureInfo.InvariantCulture);
            string suffix = GetDateSuffix(day);


            //string todayDateFormatted = $"{today.Day}{suffix} {formattedDate}";
            string todayDateFormatted = $"{day}{suffix} {formattedDate}";
            todayDateFormattedLabel.Text = todayDateFormatted;

            //Decimal paidAmount = (Decimal)(dt.Rows[rowIndex]["paidamt"]);//.ToString("#,##0;(#,##0); ");
            //string formattedPaidamt = paidAmount.ToString("#,##0;(#,##0);0");
            //TkLabel.Text = formattedPaidamt;//"100";
            //TkLabelWord.Text = NumberToWords(int.Parse(formattedPaidamt));// "One Hundred Tk";
            int paidAmount = Convert.ToInt32(dt.Rows[rowIndex]["paidamt"]);
            TkLabel.Text = paidAmount.ToString("#,##0;(#,##0);0");//ToString("#,##0;(#,##0); ")// Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");
            //TkLabelWord.Text = NumberToWords(paidAmount)+" Tk"; //
            string str = ASTUtility.Trans(paidAmount, 2);
            TkLabelWord.Text = str.Substring(1, str.Length - 2);


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenDedModal();", true);
        }
        public string GetDateSuffix(int day)
        {
            switch (day % 10)
            {
                case 1 when day != 11:
                    return "st";
                case 2 when day != 12:
                    return "nd";
                case 3 when day != 13:
                    return "rd";
                default:
                    return "th";
            }
        }
        private void RptReconsilationLetter(string paidAmount="",string paidAmountInWord="",string unit="")
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["RptReconcilationLetter"];//Session["tblflrwisbill"];
            if (dt1 == null || dt1.Rows.Count == 0)
                return;
            //var lst = dt1.DataTableToList<RealEntity.C_09_PIMP.SubConBill.RptPrjFloorWiseBill>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptReconcilationLetter", null, null, null);
            Rpt1.EnableExternalImages = true;

            //DateTime today = DateTime.Today;
            //string formattedDate = today.ToString("MMMM, yyyy", CultureInfo.InvariantCulture);
            //string suffix = GetDateSuffix(today.Day);

            //string todayDateFormatted = $"{today.Day}{suffix} {formattedDate}";
            DateTime toDate = DateTime.ParseExact(txtTodate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            int day = toDate.Day;
            string formattedDate = Convert.ToDateTime(this.txtTodate.Text).ToString("MMMM, yyyy", CultureInfo.InvariantCulture);
            string suffix = GetDateSuffix(day);


            //string todayDateFormatted = $"{today.Day}{suffix} {formattedDate}";
            string todayDateFormatted = $"{day}{suffix} {formattedDate}";

            //string selectedProjectText = ddlprjlist.SelectedItem.Text;
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("selectedProjectText", selectedProjectText));
            //Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("custTK", paidAmount));
            Rpt1.SetParameters(new ReportParameter("custTKWord", paidAmountInWord));
            Rpt1.SetParameters(new ReportParameter("todayDateFormatted", todayDateFormatted));
            Rpt1.SetParameters(new ReportParameter("unit", unit));
            Rpt1.SetParameters(new ReportParameter("projectName", SkipBeforeDelimeter(ddlprjlist.SelectedItem.Text)));


            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
        }

        protected void lbtnPrintMail_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int paidAmount = Convert.ToInt32(dt.Rows[rowIndex]["paidamt"]);

            string str = ASTUtility.Trans(paidAmount, 2);
            //TkLabelWord.Text = str.Substring(1, str.Length - 2);

            string unit = dt.Rows[rowIndex]["udesc"].ToString();
            this.RptReconsilationLetter(paidAmount.ToString("#,##0;(#,##0);0"), str.Substring(1, str.Length - 2), unit);

            string url = "../RDLCViewer.aspx?PrintOpt=PDF";
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OpenWindow", script, true);
        }

        protected void lbtnSendMail_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int paidAmount = Convert.ToInt32(dt.Rows[rowIndex]["paidamt"]);
            string str = ASTUtility.Trans(paidAmount, 2);

            string unit = dt.Rows[rowIndex]["udesc"].ToString();
            this.RptReconsilationLetter(paidAmount.ToString("#,##0;(#,##0);0"), str.Substring(1, str.Length - 2), unit);

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = (LocalReport)Session["Report1"];
            //DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            //int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string custName = dt.Rows[rowIndex]["name"].ToString();
            string custEmail= dt.Rows[rowIndex]["email"].ToString();


            string deviceInfo = "<DeviceInfo>" +
                   "  <OutputFormat>PDF</OutputFormat>" +

                   "  <EmbedFonts>None</EmbedFonts>" +
                   "</DeviceInfo>";
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes = Rpt1.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //return File(renderedBytes, mimeType);


            //Warning[] warnings;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string extension;
            //byte[] bytes = Rpt1.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            // string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;
            //  CODE TO SAVE THE REPORT FILE ON SERVER
            if (File.Exists(Server.MapPath("~/SupWorkOreder/" + custName + ".pdf")))
            {
                File.Delete(Server.MapPath("~/SupWorkOreder/" + custName + ".pdf"));
            }

            FileStream fileStream = new FileStream(Server.MapPath("~/SupWorkOreder/" + custName + ".pdf"), FileMode.Create);

            for (int i = 0; i < renderedBytes.Length; i++)
            {
                fileStream.WriteByte(renderedBytes[i]);
            }
            fileStream.Close();

            //this.SendNormalMail(custName, custEmail);
        }

        private void SendNormalMail(string custName, string custEmail)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            //string mORDERNO = orderno;

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Reconsilation Letter";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(hostname, portnumber);
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
            ///
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(frmemail);

            //msg.To.Add(new System.Net.Mail.MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));
            msg.To.Add(custEmail);
            msg.Subject = subject;
            msg.IsBodyHtml = true;

            System.Net.Mail.Attachment attachment;

            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + custName + ".pdf"; ;

            attachment = new System.Net.Mail.Attachment(apppath);
            msg.Attachments.Add(attachment);



            msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>");
            try
            {
                client.Send(msg);

                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                //string savelocation = Server.MapPath("~") + "\\SupWorkOreder";
                //string[] filePaths = Directory.GetFiles(savelocation);
                //foreach (string filePath in filePaths)
                //    File.Delete(filePath);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        //between -999,999,999 and 999,999,999.
        public string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 10000000) > 0)
            {
                words += NumberToWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lac ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}