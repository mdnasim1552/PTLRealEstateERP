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
namespace RealERPWEB.F_39_MyPage
{
    public partial class LinkActiComments : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Activities Comments";
                this.lblvalprojectName.Text = this.Request.QueryString["pactdesc"].ToString();
                this.lblvalactivities.Text = this.Request.QueryString["actdesc"].ToString();
                this.lblvaldate.Text = this.Request.QueryString["date"].ToString();
                this.ShowData();
                //this.LoadGrid();

            }
        }






        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }








        private void ShowData()
        {
            try
            {

                string comcod = this.GetComdCode();
                string date = this.Request.QueryString["date"].ToString();
                string teamcode = this.Request.QueryString["empid"].ToString();
                string pactcode = this.Request.QueryString["pactcode"].ToString();
                string actcode = this.Request.QueryString["actcode"].ToString();
                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWEMPTRANS", date, teamcode, pactcode, actcode, "", "", "", "");
                this.gvPersonalInfo.DataSource = ds1.Tables[0];
                this.gvPersonalInfo.DataBind();
                ds1.Dispose();
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;


            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetComdCode();
            string teamcode = this.Request.QueryString["empid"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string actcode = this.Request.QueryString["actcode"].ToString();


            //string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string dayid = Convert.ToDateTime(((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvdate")).Text.Trim()).ToString("yyyyMMdd");
                string date = (((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvdate")).Text.Trim());

                string comments = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvcomments")).Text.Trim();
                if (comments.Length > 0)
                {
                    bool result = MktData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "INSORUPKPITRANS02", dayid, teamcode, pactcode, actcode, date, comments, "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                        return;
                    }
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }



    }
}



