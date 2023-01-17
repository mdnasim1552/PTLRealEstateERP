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
//using  RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_21_MKT
{
    public partial class TransferClient : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        private UserManagerKPI objUser = new UserManagerKPI();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lnkprint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Transfer Client Information";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lbluseid.Text = (Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSalesList();
                //this.GetClientList();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void GetSalesList()
        {

            if (this.lnkok.Text == "New")
                return;
            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string userid = (this.Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
            string deptcode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETENTRYEMPID", srchEmp, userid, deptcode);

            this.ddlSalesTeam.DataTextField = "empname1";
            this.ddlSalesTeam.DataValueField = "empid";
            this.ddlSalesTeam.DataSource = lst3;
            this.ddlSalesTeam.DataBind();

            //this.ddlSalesTeam.SelectedValue = this.Request.QueryString["empid"].ToString();
            this.GetClientList();
        }
        private void GetSalesListNew()
        {
            //-----------Get Sales Person List ---------------//
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchteam = "%" + this.txtSrchSalesTeamNew.Text;
            string userid = (this.Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
            string deptcode = hst["deptcode"].ToString();

            DataSet dss = this.MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETENTRYEMPID", srchteam, userid, deptcode, "", "", "", "", "", "");
            this.ddlSalesTeamNew.DataTextField = "empname1";
            this.ddlSalesTeamNew.DataValueField = "empid";
            this.ddlSalesTeamNew.DataSource = dss.Tables[0];
            this.ddlSalesTeamNew.DataBind();
        }
        private void GetClientList()
        {

            //-----------Get Person List ---------------//
            UserManagerKPI objUser = new UserManagerKPI();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Getcomcod();
            string Empid = this.ddlSalesTeam.SelectedValue.ToString();
            string srchEmp = "%%";
            List<RealEntity.C_47_Kpi.EClassClientCode> lst3 = new List<RealEntity.C_47_Kpi.EClassClientCode>();
            lst3 = objUser.GetClientCode("dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWCLIENTGRP", srchEmp, Empid);

            this.ddlClientList.DataTextField = "cdesc";
            this.ddlClientList.DataValueField = "ccode";
            this.ddlClientList.DataSource = lst3;
            this.ddlClientList.DataBind();
        }
        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.ddlClientList.Items.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Client!!!";
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
            }
            if (this.lnkok.Text == "Ok")
            {


                string comcod = this.Getcomcod();
                this.ddlSalesTeam.Enabled = false;
                this.ddlClientList.Enabled = false;
                string teamcode = this.ddlSalesTeam.SelectedValue.ToString();
                string clientcod = this.ddlClientList.SelectedValue.ToString();
                this.lnkok.Text = "New";
                this.GetSalesListNew();
                this.Panel2.Visible = true;
            }
            else
            {
                //this.GetClientList();
                this.lnkok.Text = "Ok";
                this.ddlSalesTeam.Enabled = true;
                this.ddlClientList.Enabled = true;
                this.Panel2.Visible = false;



            }
        }

        //protected void lnkappupdate_Click(object sender, EventArgs e)
        //{
        //    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
        //    if (!Convert.ToBoolean(dr1[0]["entry"]))
        //    {
        //     ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
        //        return;
        //    }
        //    try
        //    {

        //        string comcod = this.Getcomcod();
        //        string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0,14).ToString();
        //        string proscod = this.ddlClientList.SelectedValue.ToString();

        //        string cdate = ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtmtingdate")).Text.Trim();
        //        string Projectname = ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtprojectname")).Text.Trim();
        //        string callorvispur = ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtcallvispurpose")).Text.Trim();
        //        string destinatin = ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtdestination")).Text.Trim();
        //        string callorvistime = ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtcallvistime")).Text.Trim();
        //        string discussion = ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtgvVal")).Text.Trim();
        //        string nextapnt = ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtgvna")).Text.Trim();
        //        string remarks = ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtremarks")).Text.Trim();

        //        bool m = MktData.UpdateTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "INORUPCLIENTSCHEDULE", teamcode, proscod, cdate, Projectname, callorvispur, destinatin,
        //           callorvistime, discussion, nextapnt, remarks, "", "", "", "","");
        //        if (m == false)
        //        { 
        //     ((Label)this.Master.FindControl("lblmsg")).Text="Error"+MktData.ErrorObject["Msg"];
        //        return;
        //        }

        //        //}
        //         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
        //        if (ConstantInfo.LogStatus == true)
        //            {
        //                string eventtype = this.lblTitle.Text;
        //                string eventdesc = "Update Info";
        //                string eventdesc2 = "";
        //                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //            }

        //    }
        //    catch (Exception ex)
        //    {
        //     ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
        //    }
        //}



        protected void ddlSalesTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgSearchClient_Click(null, null);
            this.GetClientList();
        }

        protected void imgSearchClient_Click(object sender, EventArgs e)
        {
            this.GetClientList();
        }
        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetSalesList();
            this.GetClientList();
        }
        protected void imgSearchSalesTeamNew_Click(object sender, EventArgs e)
        {
            this.GetSalesListNew();
        }
        private string GetNewClientCode()
        {
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeamNew.SelectedValue.ToString();
            DataSet dset = this.MktData.GetTransInfo(comcod, "SP_REPORT_CLIENT_INFORMATION", "GETNEWCLIENT", teamcode, "", "", "", "", "", "", "", "");


            string newcode = dset.Tables[0].Rows[0]["proscod"].ToString();
            return newcode;

        }
        protected void Update_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                string comcod = this.Getcomcod();
                string teamcode = this.ddlSalesTeamNew.SelectedValue.ToString();
                string oldteamcode = this.ddlSalesTeam.SelectedValue.ToString();
                string precode = this.ddlClientList.SelectedValue.ToString();
                string newcode = this.GetNewClientCode();
                string trdate = Convert.ToDateTime(this.txtDate.Text).ToString();

                bool mkt = MktData.UpdateTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "INSETNEWCLIENT", precode, newcode, trdate, teamcode, oldteamcode, "", "", "", "", "", "", "", "", "", "");
                if (mkt == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + MktData.ErrorObject["Msg"];
                    return;
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Update Info";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e) { }
    }
}
