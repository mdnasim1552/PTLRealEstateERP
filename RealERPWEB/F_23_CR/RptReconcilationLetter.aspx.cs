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
            string Desc2 = "asdas";
            //string csircode = "98%";//this.ddlcontractorlist.SelectedValue.ToString() == "000000000000" ? "98%" : this.ddlcontractorlist.SelectedValue.ToString() + "%";
            //string billtcode = "%";//this.ddlcatagory.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlcatagory.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCUSTOMERDETAILS", pactcode, "", "", "", "", "", "", "", "");
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

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empName = dt.Rows[rowIndex]["Name"].ToString();
            TkLabel.Text = "100";
            TkLabelWord.Text = "One Hundred Tk";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenDedModal();", true);
        }

        private void RptReconsilationLetter()
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

            //string selectedProjectText = ddlprjlist.SelectedItem.Text;
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("selectedProjectText", selectedProjectText));
            //Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("custTK", "100"));
            Rpt1.SetParameters(new ReportParameter("custTKWord", "One Hundred Tk"));

            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
        }

        protected void lbtnPrintMail_Click(object sender, EventArgs e)
        {
            this.RptReconsilationLetter();
            string url = "../RDLCViewer.aspx?PrintOpt=PDF";
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OpenWindow", script, true);
        }

        protected void lbtnSendMail_Click(object sender, EventArgs e)
        {
            this.RptReconsilationLetter();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = (LocalReport)Session["Report1"];
            DataTable dt = (DataTable)Session["RptReconcilationLetter"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string custName = dt.Rows[rowIndex]["Name"].ToString();
            string custEmail= dt.Rows[rowIndex]["Email"].ToString();


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
    }
}