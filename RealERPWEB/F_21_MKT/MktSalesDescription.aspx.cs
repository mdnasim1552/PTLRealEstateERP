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
namespace RealERPWEB.F_21_MKT
{
    public partial class MktSalesDescription : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ShowData();

            }

        }

        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ShowData()
        {
            ViewState.Remove("tblunitdesc");
            string comcod = GetComeCode();
            string proscode = this.Request.QueryString["proscod"].ToString();
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETMKKUNITINFORMATION", proscode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvMktUnitDesc.DataSource = null;
                this.gvMktUnitDesc.DataBind();
                return;
            }
            ViewState["tblunitdesc"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblunitdesc"];
            this.gvMktUnitDesc.DataSource = dt;
            this.gvMktUnitDesc.DataBind();
            ((Label)this.gvMktUnitDesc.FooterRow.FindControl("lFgvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uamt)", "")) ? 0.00 : dt.Compute("sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
        }


        protected void lbntTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblunitdesc"];

            for (int i = 0; i < this.gvMktUnitDesc.Rows.Count; i++)
            {
                string Unitdesc = ((TextBox)this.gvMktUnitDesc.Rows[i].FindControl("txtgvUnitDesc")).Text.Trim();
                string Unit = ((TextBox)this.gvMktUnitDesc.Rows[i].FindControl("txtgvUnit")).Text.Trim();
                double Size = Convert.ToDouble("0" + ((TextBox)this.gvMktUnitDesc.Rows[i].FindControl("txtgvUnitsize")).Text.Trim());
                double Rate = Convert.ToDouble("0" + ((TextBox)this.gvMktUnitDesc.Rows[i].FindControl("txtgvRate")).Text.Trim());
                double amt = Size * Rate;
                dt.Rows[i]["unitdesc"] = Unitdesc;
                dt.Rows[i]["unit"] = Unit;
                dt.Rows[i]["usize"] = Size;
                dt.Rows[i]["urate"] = Rate;
                dt.Rows[i]["uamt"] = amt;
            }


            ViewState["tblunitdesc"] = dt;
            this.Data_Bind();
        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            try
            {

                string comcod = this.GetComeCode();
                string proscode = this.Request.QueryString["proscod"].ToString().Trim();
                string txtdate = this.txtdate.Text.Trim();
                DataTable dt = (DataTable)ViewState["tblunitdesc"];


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string gcod = dt.Rows[i]["gcod"].ToString();
                    string Unitdesc = dt.Rows[i]["unitdesc"].ToString();
                    string Unit = dt.Rows[i]["unit"].ToString();
                    string Usize = dt.Rows[i]["usize"].ToString();
                    double Uamt = Convert.ToDouble(dt.Rows[i]["uamt"]);
                    if (Uamt > 0)
                    {
                        bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "INSORUPDATEUNITDESC", proscode, gcod, Unitdesc, Unit, Usize, Uamt.ToString(),
                            txtdate, "", "", "", "", "", "", "", "");
                        if (!result)
                            return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Entry Unit Info";
                    string eventdesc = "Update Unit";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }



        }
    }
}