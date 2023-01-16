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
using System.Collections.Generic;
using RealERPWEB.Service;

namespace RealERPWEB.F_17_Acc
{
    public partial class AccTopPage : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        UserService userSer = new UserService();
        public static string lblTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "pttycash") ? "Petty cash Bill List" : "";
                //this.Master.Page.Title = (type == "pttycash") ? "Petty cash Bill Approval Sheet" : "";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            }



        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);


            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void lnkPrint_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "pttycash":
                    Get_pettyCashBillInfo();
                    break;
            }


        }
        protected void Get_pettyCashBillInfo()
        {
            //string SrchProject = "%" + this.txtSrchProjectName.Text.Trim() + "%";
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GET_PETTY_CASH_LIST", fromdate, todate);
            if (ds == null)
                return;
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = ds.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>();
            ViewState["Pettycash"] = lst;
            this.Data_Bind();




        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "pttycash":

                    List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];
                    if (lst.Count == 0)
                    {
                        this.gvpetty.DataSource = null;
                        this.gvpetty.DataBind();
                        return;
                    }

                    this.gvpetty.DataSource = lst;
                    this.gvpetty.DataBind();

                    break;
            }
        }

        protected void gvpetty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink applink = (HyperLink)e.Row.FindControl("HypApprv");
                LinkButton dltbutton = (LinkButton)e.Row.FindControl("hlnkdlt");
                string pcblno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pcblno")).ToString();
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                string billdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "billdate")).ToString("dd-MMM-yyyy");

                applink.NavigateUrl = "~/F_17_Acc/AccPettyCashApp.aspx?Type=Entry&genno=" + pcblno + "&date=" + billdate;

                if (vounum == "00000000000000")
                {
                    dltbutton.Visible = true;
                }

            }
        }


        protected void hlnkdlt_OnClick(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            string comcod = this.GetCompCode();
            int index = row.RowIndex;
            string pcblno = ((Label)this.gvpetty.Rows[index].FindControl("lblpcbl1")).Text.ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PETTY_CASH_DELETE", pcblno);
            if (!result)
            {
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Successful.";
            this.Get_pettyCashBillInfo();
        }
    }
}


