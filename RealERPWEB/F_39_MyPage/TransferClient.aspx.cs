using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_39_MyPage
{
    public partial class TransferClient : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Transfer Information";


                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSalesList();


            }
        }
        private void GetSalesList()
        {
            if (this.lnkok.Text == "New")
                return;
            //-----------Get Sales Person List ---------------//
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchteam = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string Userid = (this.Request.QueryString["Type"].ToString() == "Mkt") ? hst["usrid"].ToString() : "";
            DataSet dss = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETMKTTEAM", srchteam, Userid, "", "", "", "", "", "", "");
            this.ddlSalesTeam.DataTextField = "teamdesc";
            this.ddlSalesTeam.DataValueField = "teamcode";
            this.ddlSalesTeam.DataSource = dss.Tables[0];
            this.ddlSalesTeam.DataBind();
            this.GetClientList();
        }
        private void GetSalesListNew()
        {

            //-----------Get Sales Person List ---------------//
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchteam = "%" + this.txtSrchSalesTeamNew.Text.Trim() + "%";
            DataSet dss = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETMKTTEAM", srchteam, "", "", "", "", "", "", "", "");
            this.ddlSalesTeamNew.DataTextField = "teamdesc";
            this.ddlSalesTeamNew.DataValueField = "teamcode";
            this.ddlSalesTeamNew.DataSource = dss.Tables[0];
            this.ddlSalesTeamNew.DataBind();
        }
        private void GetClientList()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            if (this.lnkok.Text == "New")
                return;
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeam.SelectedValue.ToString();
            string txtSerch = "%" + this.txtSrchClient.Text.Trim() + "%";
            DataSet dset = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETCLIENT02", teamcode, txtSerch, "", "", "", "", "", "", "");
            this.ddlClientList.DataTextField = "prosdesc";
            this.ddlClientList.DataValueField = "proscod";
            this.ddlClientList.DataSource = dset.Tables[0];
            this.ddlClientList.DataBind();

        }
        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void lnkok_Click(object sender, EventArgs e)
        {

            if (this.lnkok.Text == "Ok")
            {


                string comcod = this.Getcomcod();
                this.ddlSalesTeam.Enabled = false;
                this.ddlClientList.Enabled = false;
                this.lnkok.Text = "New";
                this.Panel2.Visible = true;
                this.Update.Enabled = true;
            }
            else
            {
                //this.GetClientList();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
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
            this.GetClientList();
        }

        protected void lbtnSearchClient_Click(object sender, EventArgs e)
        {
            this.GetClientList();
        }
        protected void lbtnSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetSalesList();
        }
        protected void lbtnSearchSalesTeamNew_Click(object sender, EventArgs e)
        {
            this.GetSalesListNew();
        }
        private string GetNewClientCode()
        {
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeamNew.SelectedValue.ToString();
            DataSet dset = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETNEWCLIENT", teamcode, "", "", "", "", "", "", "", "");
            string newcode = dset.Tables[0].Rows[0]["newclientcode"].ToString();
            return newcode;

        }
        protected void Update_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
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

                bool mkt = MktData.UpdateTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "INSERTNEWCLIENT", precode, newcode, trdate, teamcode, oldteamcode, "", "", "", "", "", "", "", "", "", "");
                if (mkt == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + MktData.ErrorObject["Msg"];
                    return;
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                this.Update.Enabled = false;
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
        protected void lnkprint_Click(object sender, EventArgs e)
        {

        }
    }
}
