using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Drawing;
namespace RealERPWEB.F_15_DPayReg
{
    public partial class AccOnlinePaymntPlan : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtReceiveDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Add Payment Plan Schedule Confirm";

                this.GetDataShow();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Okbtn_Click(object sender, EventArgs e)
        {

            this.GetDataShow();

        }

        private void GetDataShow()
        {
            string comcod = this.GetCompCode();
            string date = this.txtReceiveDate.Text.ToString();

            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT_02", "GETPAYMENTPLANISSUE", date, "", "", "", "", "", "", "", "");
            if (ds == null)
                return;

            ViewState["tblplanlist"] = ds.Tables[0];

            this.Data_Bind();

        }


        private void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblplanlist"];

            this.gvPayment.DataSource = tbl1;
            this.gvPayment.DataBind();


            for (int j = 1; j < tbl1.Rows.Count; j++)
            {
                string slnum = tbl1.Rows[j]["slnum"].ToString();
                string billno = tbl1.Rows[j]["billno"].ToString();

                if (slnum == tbl1.Rows[j]["slnum"].ToString())
                {

                    ((Label)this.gvPayment.Rows[j].FindControl("lbgvslnum")).ForeColor = Color.Red;
                }
                // slnum = tbl1.Rows[j]["slnum"].ToString();
            }




        }

        protected void chkallView_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblplanlist"];
            int i, index;
            if (((CheckBox)this.gvPayment.HeaderRow.FindControl("chkallView")).Checked)
            {

                for (i = 0; i < this.gvPayment.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPayment.Rows[i].FindControl("chkActive")).Checked = true;
                    index = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + i;
                    dt.Rows[index]["rpayplan"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPayment.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPayment.Rows[i].FindControl("chkActive")).Checked = false;
                    index = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + i;
                    dt.Rows[index]["rpayplan"] = "False";


                }

            }

            ViewState["tblplanlist"] = dt;
        }


        private void Session_update()
        {
            DataTable dt = (DataTable)ViewState["tblplanlist"];
            int index;
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string chkper = (((CheckBox)gvPayment.Rows[i].FindControl("chkActive")).Checked) ? "True" : "False";

                index = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + i;
                dt.Rows[index]["rpayplan"] = chkper;

            }
            Session["tblplanlist"] = dt;
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;


            string comcod = this.GetCompCode();
            this.Session_update();

            DataTable dt1 = (DataTable)ViewState["tblplanlist"];


            bool result = false;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string slnum = dt1.Rows[i]["slnum"].ToString().Trim();
                string mslnum = dt1.Rows[i]["mslnum"].ToString().Trim();
                string chkper = dt1.Rows[i]["rpayplan"].ToString().Trim();

                if (chkper == "True")
                {
                    result = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT_02", "UPDATEPAYMPLAN", mslnum, slnum, chkper, "", "", "", "", "", "", "", "", "", "", "", "");

                }

            }
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update";
            }

        }
        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}