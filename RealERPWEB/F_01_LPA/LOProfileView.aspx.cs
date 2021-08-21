using RealERPLIB;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_01_LPA
{
    public partial class LOProfileView : System.Web.UI.Page
    {
        ProcessAccess ClientData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetClientinfo();
                this.GetLODiscussuion();
            }

        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetClientinfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string ClientId = this.Request.QueryString["clientid"].ToString();

            DataSet ds1 = ClientData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", "CLIENTINFO", ClientId, "", "", "", "", "", "", "", "");


            DataTable dt = ds1.Tables[0];

            DataView dv;

            //if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
            //    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
            //    return;
            //}
            this.LandInfo.InnerText = "PROPERTIES:";//(dt.Select("sgcod='001'").Length == 0) ? "" : dt.Rows[0]["name1"].ToString();
            this.LandName.InnerText = (ds1.Tables[1].Rows.Count == 0) ? "" : ds1.Tables[1].Rows[0]["sirdesc"].ToString();

            this.ClientName.InnerText = "NAME:";//(dt.Select("sgcod='001'").Length == 0) ? "" : dt.Rows[0]["name1"].ToString();
            this.ClientName1.InnerText = (ds1.Tables[1].Rows.Count == 0) ? "" : ds1.Tables[1].Rows[0]["name1"].ToString();

            this.ClintMail.InnerText = (ds1.Tables[1].Rows.Count == 0) ? "" : ds1.Tables[1].Rows[0]["email"].ToString();// (dt.Select("sgcod='015'").Length == 0) ? "" : dt.Rows[14]["gdesc1"].ToString();
            this.lblEmail.InnerText = "EMAIL:"; //(dt.Select("sgcod='015'").Length == 0) ? "" : dt.Rows[14]["gdesc"].ToString();

            this.lblphn.InnerText = "MOBILE NUMBER:";// (dt.Select("sgcod='007'").Length == 0) ? "" : dt.Rows[6]["gdesc"].ToString();
            this.ClintPhn.InnerText = (ds1.Tables[1].Rows.Count == 0) ? "" : ds1.Tables[1].Rows[0]["phn"].ToString();//(dt.Select("sgcod='007'").Length == 0) ? "" : dt.Rows[6]["gdesc1"].ToString();


            if (ds1.Tables[0].Rows.Count == 0)
                return;

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0404013'");

            this.lblspous.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClintSpousNam.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0404019'");
            this.lblFather.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClientFather.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0404021'");
            this.lblMother.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClientMother.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0404002'");
            this.lblpermanent.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClientperAdd.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= ''");
            this.lblPresent.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClientpAdd.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= ''");
            this.lblTphon.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClientTphn.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0404003'");
            this.lblbirth.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.clintbirth.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0404005'");
            this.lblmarrage.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClientMarrage.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0404007'");
            this.lblNatid.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClientNaid.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0404008'");
            this.lblpassport.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.Clientpassport.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= '0403005'");
            this.lblproffession.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.ClientProffession.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

            dv = dt.Copy().DefaultView;
            dv.RowFilter = ("gcod= ''");
            this.lblloc.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc"].ToString();
            this.Clientloc.InnerText = (dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows[0]["gdesc1"].ToString();

        }

        private void GetLODiscussuion()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            //string userid = hst["usrid"].ToString();
            DateTime time = System.DateTime.Now;
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy") + " " + time.ToString("HH:mm"); ;
            string userid = this.Request.QueryString["teamcode"].ToString();
            string ClientId = this.Request.QueryString["clientid"].ToString();


            DataSet dscd = ClientData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPRELOWNERDISCUSSION", userid, ClientId, date, "", "", "", "", "", "", "");
            if (dscd == null)
                return;


            this.gvclient.DataSource = dscd;
            this.gvclient.DataBind();

            for (int i = 0; i < this.gvclient.Rows.Count; i++)
            {
                string disgnote = ((Label)gvclient.Rows[i].FindControl("lblgvdisgnote")).Text.Trim();
                string subgnote = ((Label)gvclient.Rows[i].FindControl("lblgvsubgnote")).Text.Trim();
                if (disgnote.Length != 0)
                {
                    this.gvclient.Rows[i].Cells[3].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                }
                if (subgnote.Length != 0)
                {
                    this.gvclient.Rows[i].Cells[7].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                }
            }

        }


        protected void gvclient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int index = e.Row.RowIndex;

                Panel Lbtn = (Panel)e.Row.FindControl("pnldis");
                Lbtn.Attributes.Add("onmouseover", "AddButton(" + index + ")");
                Lbtn.Attributes.Add("onmouseout", "HiddenButton(" + index + ")");

                LinkButton Lbtn1 = (LinkButton)e.Row.FindControl("lnkAdddis");
                Lbtn1.Attributes.Add("class", "hiddenb" + index);
                Lbtn1.Attributes.Add("style", "display:none;");


                Panel pnlsub = (Panel)e.Row.FindControl("pnlsub");
                pnlsub.Attributes.Add("onmouseover", "AddButtonsub(" + index + ")");
                pnlsub.Attributes.Add("onmouseout", "HiddenButtonsub(" + index + ")");

                LinkButton Lbtnsub = (LinkButton)e.Row.FindControl("lnkAdddissub");
                Lbtnsub.Attributes.Add("class", "hiddensub" + index);
                Lbtnsub.Attributes.Add("style", "display:none;");



            }
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                //string comcod = this.Getcomcod();
                //DataTable dt = (DataTable)ViewState["tblprediscussion"];
                //int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                //string empid = this.ddlEmpid.SelectedValue.ToString();
                //string client = this.ddlClient.SelectedValue.ToString();
                //string cdate = Convert.ToDateTime(dt.Rows[RowIndex]["cdate"]).ToString();
                //string kpigrp = dt.Rows[RowIndex]["kpigrp"].ToString();
                //bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DELETEPREDISCUSSION", empid, client, kpigrp, cdate, "", "", "", "", "", "");
                //if (!result)
                //{
                //    ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Fail";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;
                //}

                //dt.Rows[RowIndex].Delete();
                //DataView dv = dt.DefaultView;
                //ViewState.Remove("tblprediscussion");
                //ViewState["tblprediscussion"] = dv.ToTable();
                //this.gvclient.DataSource = dv.ToTable();
                //this.gvclient.DataBind();


                //((Label)this.Master.FindControl("lblmsg")).Text = "Successfully  Delete";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


        }
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

        }


        protected void lnkAdddis_Click(object sender, EventArgs e)
        {
            this.txtComm.Text = "";
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string cDate = ((Label)this.gvclient.Rows[index].FindControl("lblgvDate")).Text.ToString().Trim();

            string discussion = ((Label)this.gvclient.Rows[index].FindControl("lgvDiscussion0")).Text.ToString().Trim();
            string disgnote = ((Label)this.gvclient.Rows[index].FindControl("lblgvdisgnote")).Text.ToString().Trim();

            this.GetClientData(cDate, discussion, disgnote);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "opencommModal();", true);

        }
        private void GetClientData(string cDate, string discussion, string disgnote)
        {
            string comcod = this.GetCompCode();
            this.lbldsi.InnerText = "Discussion:";
            this.lbldiscussion.Text = discussion;
            this.lblheader.InnerText = "Add New Comments on discussion for " + Convert.ToDateTime(cDate).ToString("dd-MMM-yyyy hh:mm tt");
            this.lblEmpid.Text = this.Request.QueryString["teamcode"].ToString();
            this.lblclient.Text = this.Request.QueryString["clientid"].ToString();
            this.lbldate.Text = cDate;
            if (disgnote.Length != 0)
                this.txtComm.Text = disgnote;

        }
        protected void lUpdatInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();
            string empid = this.lblEmpid.Text;
            string Client = this.lblclient.Text;
            string cdate = Convert.ToDateTime(this.lbldate.Text).ToString("dd-MMM-yyyy HH:mm:ss");
            string Gvalue = this.lbldiscussion.Text;
            string Comments = this.txtComm.Text;
            string gcod = "";
            string Type = this.lbldsi.InnerText;

            if (Type == "Discussion:")
            {

                gcod = "810100102015";
            }
            else
            {
                gcod = "810100102025";

            }
            bool result = ClientData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATE_COMM", empid, Client, "000000000000", "", "000000000000", cdate, gcod, "T", Gvalue, Comments, userid, Posteddat);

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }
            this.txtComm.Text = "";
            this.GetLODiscussuion();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void lnkAdddissub_Click(object sender, EventArgs e)
        {
            this.txtComm.Text = "";
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string cDate = ((Label)this.gvclient.Rows[index].FindControl("lblgvDate")).Text.ToString().Trim();

            string lgvndissub = ((Label)this.gvclient.Rows[index].FindControl("lgvndissub")).Text.ToString().Trim();
            string subgnote = ((Label)this.gvclient.Rows[index].FindControl("lblgvsubgnote")).Text.ToString().Trim();

            this.GetClientData2(cDate, lgvndissub, subgnote);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "opencommModal();", true);

        }
        private void GetClientData2(string cDate, string lgvndissub, string subgnote)
        {


            this.lbldsi.InnerText = "Subject:";
            this.lbldiscussion.Text = lgvndissub;
            this.lblheader.InnerText = "Add New Comments on Subject for " + Convert.ToDateTime(cDate).ToString("dd-MMM-yyyy hh:mm tt");
            this.lblEmpid.Text = this.Request.QueryString["teamcode"].ToString();
            this.lblclient.Text = this.Request.QueryString["clientid"].ToString();
            this.lbldate.Text = cDate;
            if (subgnote.Length != 0)
                this.txtComm.Text = subgnote;
        }


        protected void lnkSndMail_Click(object sender, EventArgs e)
        {

            this.ClienEmail.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }

        protected void lnkNdisscuss_Click(object sender, EventArgs e)
        {
            string ClientId = this.Request.QueryString["clientid"].ToString().Trim();
            string url = "~/F_21_Mkt/MktLandOwnerDiscus.aspx?Type=Entry&clientid=" + ClientId;

            Response.Redirect(url);
        }

        protected void SubmitMsg_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.ClientData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");




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

            MailMessage msg = new MailMessage();


            string subject = string.Empty;
            string body = string.Empty;
            string tomail = this.ClienEmail.Text.ToString();



            subject = this.subject.Text.ToString();

            body = this.msgbody.Text.ToString();



            // System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(frmemail);



            msg.To.Add(new System.Net.Mail.MailAddress(tomail));
            msg.Subject = subject;
            msg.IsBodyHtml = true;

            ////System.Net.Mail.Attachment attachment;

            ////string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;

            //attachment = new System.Net.Mail.Attachment(apppath);
            //msg.Attachments.Add(attachment);


            msg.Body = body;



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