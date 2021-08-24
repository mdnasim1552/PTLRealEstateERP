using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_23_CR
{
    public partial class RptMonthlyCollectionMerge : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtfrmdate.Text = date;
            this.txttodate.Text = Convert.ToDateTime("01" + date.Substring(2)).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            this.ViewSelection();
           
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            ((Label)this.Master.FindControl("lblTitle")).Text = this.Request.QueryString["Type"] == "MonthlyCollMerge" ? "Monthly Collection(Receipt Type Merge)"
                : "Monthly Collection Schedule(Merge)" ;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
           
        }

        public string GetCompCode()
        {
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }
        private void ViewSelection()
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "MonthlyCollSchMerge":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "MonthlyCollMerge":
                    this.ddllang.Visible = true;
                    this.lblLang.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddllang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvmoncollsch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvmoncoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

        }
    }
}