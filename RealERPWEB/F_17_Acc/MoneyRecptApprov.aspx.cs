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
    public partial class MoneyRecptApprov : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (
                    !ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                        (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.SaleRequRpt();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void SaleRequRpt()
        {
            string comcod = this.GetCompCode();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTRECPTAPPROVAL", "%", Date, "%",
                "", "", "", "", "", "");

            Session["tblapprecpt"] = ds1.Tables[0];
            this.Data_Bind();
        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblapprecpt"];
            this.gvMoneyRcptApp.DataSource = dt;
            this.gvMoneyRcptApp.DataBind();
            if (dt.Rows.Count > 0)
                ((Label)this.gvMoneyRcptApp.FooterRow.FindControl("lgvMFdramt")).Text = Convert.ToDouble(
                    (Convert.IsDBNull(dt.Compute("sum(paidamt)", ""))
                        ? 0
                        : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
        }
        protected void lnkbtnMAppcdep_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblapprecpt"];
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string mrno = dt.Rows[Rowindex]["mrno"].ToString();
            string chqno = dt.Rows[Rowindex]["chqno"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string appid = hst["usrid"].ToString();
            string appsession = hst["session"].ToString();
            string Terminal = hst["compname"].ToString();


            //string appdate = System.DateTime.Now.ToString ("dd.MM.yyyy hh:mm:ss tt");
            string appdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "INSERTRECPTAPPROVAL", appid, appdate,
               appsession, Terminal, chqno, mrno, "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Failed !!!');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Update Successfully !!!.');", true);
            }

            this.SaleRequRpt();
        }
    }
}