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
namespace RealERPWEB.F_81_Hrm.F_99_MgtAct
{
    public partial class MonthlyTarget : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            string comcod = this.GetCompCode();
            string YearMon = this.ddlyearmon.SelectedValue.ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETSBUDGETINFO", YearMon, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalbgd.DataSource = null;
                this.gvSalbgd.DataBind();
                return;


            }
            ViewState["tblsal"] = ds1.Tables[0];
            this.Data_Bind();

        }



        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            this.gvSalbgd.DataSource = tbl1;
            this.gvSalbgd.DataBind();
            this.FooterCalculation(tbl1);
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string deptcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            deptcode = dt1.Rows[0]["deptcode"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    dt1.Rows[j]["deptname"] = "";
                }

                else
                    deptcode = dt1.Rows[j]["deptcode"].ToString();

            }


            return dt1;

        }


        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFSaleTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(saleamt)", "")) ? 0.00
            : dt.Compute("Sum(saleamt)", ""))).ToString("#,##0;(#,##0);  ");
            ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFCollTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(collamt)", "")) ? 0.00
            : dt.Compute("Sum(collamt)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFtoSaleTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsaleamt)", "")) ? 0.00
            : dt.Compute("Sum(tsaleamt)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFtoCollTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tcollamt)", "")) ? 0.00
            : dt.Compute("Sum(tcollamt)", ""))).ToString("#,##0;(#,##0);  ");
        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            for (int i = 0; i < this.gvSalbgd.Rows.Count; i++)

            {
                tbl1.Rows[i]["saleamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtgvsalamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["collamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtgvcollamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["tsaleamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtogvtosalamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["tcollamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtgvtocollamt")).Text.Trim()).ToString();
            }
            ViewState["tblsal"] = tbl1;


        }

        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string Yearmon = this.ddlyearmon.SelectedValue.ToString();
                bool result = true;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string deptcode = dt1.Rows[i]["deptcode"].ToString();
                    string saleamt = Convert.ToDouble(dt1.Rows[i]["saleamt"].ToString()).ToString();
                    string collamt = Convert.ToDouble(dt1.Rows[i]["collamt"].ToString()).ToString();
                    string tsaleamt = Convert.ToDouble(dt1.Rows[i]["tsaleamt"].ToString()).ToString();
                    string tcollamt = Convert.ToDouble(dt1.Rows[i]["tcollamt"].ToString()).ToString();
                    result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INSORUPSALCOLTARINF", Yearmon, deptcode, saleamt, collamt, tsaleamt, tcollamt, "", "", "", "", "", "", "", "", "");


                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    }



                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }






    }
}