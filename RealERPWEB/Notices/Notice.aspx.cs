using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.Notices
{
    public partial class Notice : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.NoticeDetails();
                ((Label)this.Master.FindControl("lblTitle")).Text = "All Notices";
            }
        }
        private string GetcompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void NoticeDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetcompCode();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GETNOTICEDATA", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            Session["tblNotice"] = ds1.Tables[2];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblNotice"];
            this.grvNotice.DataSource = dt;
            this.grvNotice.DataBind();
        }
        protected void grvNotice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }
        protected void lbtnCreateNotice_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "RedirNoticeCreate();", true);
        }

        protected void grvNotice_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string ndetails = DataBinder.Eval(e.Row.DataItem, "ndetails").ToString();
                Label lbldetails = e.Row.FindControl("lbltaskdesc") as Label;

                if (e.Row.RowIndex > 1)
                {

                    if (ndetails.Length > 60)
                    {
                        lbldetails.Text = ndetails.Substring(1, 60);

                    }
                    else
                    {
                        lbldetails.Text = ndetails;

                    }


                }




            }
        }
    }
}