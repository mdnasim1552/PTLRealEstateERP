using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccClientLedger : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] frmname = (HttpContext.Current.Request.Url.AbsoluteUri.ToString()).Split('&');


            string urlname = (frmname[0].Trim().Contains("Type=ClientLedger")) ? frmname[0] : frmname[0] + "&" + frmname[1];
            //  int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            if (!ASTUtility.PagePermission(urlname, (DataSet)Session["tblusrlog"]))
                Response.Redirect("~/AcceessError.aspx");

            DataRow[] dr1 = ASTUtility.PagePermission1(urlname, (DataSet)Session["tblusrlog"]);



            //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
            //    Response.Redirect("../AcceessError.aspx");
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

            //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "ClientLedger") ? "Client Ledger" : "";
            //this.Master.Page.Title = (Request.QueryString["Type"].ToString() == "ClientLedger") ? "Client  Ledger" : "";
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            this.lbtnSearchAcc_Click(null, null);
            if (Request.QueryString["Type"].ToString() == "ClientLedger")
            {


                //this.Panel1.Visible = true;
            }


            if (ConstantInfo.LogStatus)
            {
                string comcod = this.GetCompcode();
                string eventdesc = "View " + ((Label)this.Master.FindControl("lblTitle")).Text;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, "");


            }

            //this.rbtnList1.SelectedIndex = 0;
        }

        private string GetCompcode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void lbtnSearchAcc_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = GetCompcode();
                string gprjcode = this.Request.QueryString["prjcode"] ?? "";
                string filter = (gprjcode.Length == 0) ? this.txtAccSearch.Text.Trim() == "" ? "18%" : this.txtAccSearch.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
                DataSet ds1 = new DataSet();

                ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCHEADWITHCUSTOMER", filter, "", "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                DataTable dt2 = ds1.Tables[1];

                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();

                this.ddlCusHead.DataSource = dt2;
                this.ddlCusHead.DataTextField = "sirdesc1";
                this.ddlCusHead.DataValueField = "sircode";
                this.ddlCusHead.DataBind();

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
    }
}
