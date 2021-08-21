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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            ((Label)this.Master.FindControl("lblTitle")).Text = " Supplier/Sub-Contractor INFORMATION";

            if (this.ddlSName.Items.Count == 0)
            {
                this.GetProjectName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
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
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
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

                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESUPLINF", ssirCode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "SUPPLIERS INFORMATION";
                string eventdesc = "Update Sup Info";
                string eventdesc2 = this.ddlSName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


    }
}

