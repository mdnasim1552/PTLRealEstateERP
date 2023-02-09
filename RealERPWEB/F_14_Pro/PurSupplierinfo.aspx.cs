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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_14_Pro
{

    public partial class PurSupplierinfo : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        ProcessAccess _processAccessMsgdb = new ProcessAccess("ASTREALERPMSG");
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = " Supplier/Sub-Contractor INFORMATION";

                if (this.Request.QueryString["Type"].ToString() == "Entry")
                {
                    if (this.Request.QueryString.AllKeys.Contains("ssircode"))
                    {
                        getSupplierInfo();
                    }
                }
                if (this.ddlSName.Items.Count == 0)
                {
                    this.GetProjectName();
                }

            }          
           
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void getSupplierInfo()
        {
            string ssircode = this.Request.QueryString["ssircode"].ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + ssircode + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPSNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlSName.DataTextField = "sirdesc";
            this.ddlSName.DataValueField = "sircode";
            this.ddlSName.DataSource = ds1.Tables[0];
            this.ddlSName.DataBind();

            this.lbtnOk_Click(null, null);

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPSNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlSName.DataTextField = "sirdesc";
            this.ddlSName.DataValueField = "sircode";
            this.ddlSName.DataSource = ds1.Tables[0];
            this.ddlSName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.lblProjectdesc.Text = this.ddlSName.SelectedItem.Text;
                //this.lblProjectmDesc.Text = this.ddlSName.SelectedItem.Text.Substring(13);
                this.ddlSName.Enabled = false;
                //this.lblProjectmDesc.Visible = true;
                //this.lblProjectdesc.Visible = true;
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlSName.Enabled = true;
            //this.lblProjectmDesc.Text = "";
            //this.lblProjectmDesc.Visible = false;
            //this.lblProjectdesc.Text = "";

            //this.lblProjectdesc.Visible = false;
            this.gvPersonalInfo.DataSource = null;
            this.gvPersonalInfo.DataBind();
        }

        private void LoadGrid()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SupplierCode = this.ddlSName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SUPPERSONALINFO", SupplierCode, "", "", "", "", "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            ViewState["tblsup"] = ds1.Tables[0];
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string txtsign1, txtsign2, txtsign3;
            switch (comcod)
            {
                case "3101":
                case "3366":
                    txtsign1 = "Site Engineer";
                    txtsign2 = "Engineer";
                    txtsign3 = "Head Engineer";
                    break;

                default:
                    txtsign1= "Md. Golam Rashul,\nAssistant Manager,\nContract Management";
                    txtsign2= "Md. Shamsur Rahamam,\nD.G.M. Customar Management ";
                    txtsign3= "Engr. Md. Abdur Razzaque,\nG.M. Construction";
                    break;
            }

            //  string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
            DataTable dt = (DataTable)ViewState["tblsup"];
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EClassSuppaContractior02>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSubContEnlistmentForm", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Supplier", "Supplier Name: " + this.ddlSName.SelectedItem.Text.Substring(13).Trim().ToString()));
            // Rpt1.SetParameters(new ReportParameter("ReceiveHead", "Resource Head: " + this.ddlSName.SelectedItem.Text.Trim().ToString()));
            // Rpt1.SetParameters(new ReportParameter("billno", this.ddlBillList.SelectedItem.Text.Trim().ToString()));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Sub-Contractor Enlistment Form"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("txtsign1", txtsign1));
            Rpt1.SetParameters(new ReportParameter("txtsign2", txtsign2));
            Rpt1.SetParameters(new ReportParameter("txtsign3", txtsign3));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
               string Messagesdx = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesdx + "');", true);
                 
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ssirCode = this.ddlSName.SelectedValue.ToString();

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESUPLINF", ssirCode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "", "");

            }
            string Messagesd = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
             

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "SUPPLIERS INFORMATION";
                string eventdesc = "Update Sup Info";
                string eventdesc2 = this.ddlSName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void lnkMesSend_Click(object sender, EventArgs e)
        {
            string Messagesd;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ssirCode = this.ddlSName.SelectedValue.ToString();

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                if (Gcode == "71003" && Gvalue.Length > 11)
                {
                    Messagesd = "Mobile noumber length must be 11 digit";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                     
                    return;
                }
                else if (Gcode == "71003" && Gvalue.Length < 11)
                {

                    Messagesd = "Mobile noumber length must be 11 digit";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                   
                    return;
                }
                else if (Gcode == "71003" && Gvalue != "")
                {
                    string cellphone = Gvalue.ToString();
                    // send sms code

                    switch (comcod)
                    {
                        //case "3101": // Pintech                   
                        case "3356"://Intech
                            this.SMSSendforSupplier(comcod, cellphone);
                            break;


                        default:
                            break;
                    }
                }
            }
        }

        private void SMSSendforSupplier(string comcod, string cellphone)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            DataSet ds1 = this._processAccessMsgdb.GetTransInfo(comcod, "ASTREALERPMSGDB.dbo.SP_ENTRY_SMS_MAIL_INFO", "GETSMSMAILTEMPLATE", "140202%", "", "", "", "", "", "", "", "");

            string supcode = this.ddlSName.SelectedValue.ToString();
            string supname1 = this.ddlSName.SelectedItem.ToString();
            int supname2 = this.ddlSName.SelectedItem.ToString().Length;
            string supname = supname1.Substring(13, supname2 - 13);

            string tempeng = ds1.Tables[0].Rows[0]["smscont"].ToString();
            tempeng = tempeng.Replace("[compname]", supname);
            tempeng = tempeng.Replace("[username]", comnam);
            //tempeng = tempeng.Replace("[date]", paymentdate);
            //tempeng = tempeng.Replace("[payamt]", payableamt);
            //tempeng = tempeng.Replace("[duesamt]", dues);
            //tempeng = tempeng.Replace("[paymode]", paymod);
            //tempeng = tempeng.Replace("[chequeno]", cheq);

            string smtext = tempeng;

            SendSmsProcess sms = new SendSmsProcess();
            string ntype = ds1.Tables[0].Rows[0]["gcod"].ToString();
            string smsstatus = (ds1.Tables[0].Rows[0]["sactive"].ToString() == "True") ? "Y" : "N";
            bool resultsms = sms.SendSMSClient(comcod, smtext, cellphone);
            if (resultsms == true)
            {
                bool IsSMSaved = CALogRecord.AddSMRecord(comcod, ((Hashtable)Session["tblLogin"]), supcode, "", "", "", ntype, smsstatus, smtext, "",
                           "", "", cellphone, "");

              
                string Messagesd = "SMS Send Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
                
            }
            else
            {
                string Messagesd = "Error occured while sending your message.";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }

        }

        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string gcode = ((Label)e.Row.FindControl("lblgvItmCode")).Text.ToString();
                string gText = ((TextBox)e.Row.FindControl("txtgvVal")).Text.ToString();
                if (gcode == "71003")
                {
                    ((TextBox)e.Row.FindControl("txtgvVal")).Attributes.Add("class", "chkmobile");
                }
            }
        }
    }
}

