using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_32_Mis
{
    public partial class SalesTarget : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetYearMonth();
                this.loademp.Text = this.Request.QueryString["empname"];
                this.LoadAllProject();
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
            //this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString ("yyyyMM");
            this.ddlyearmon.SelectedValue = this.Request.QueryString["yearmon"];
            ds1.Dispose();
        }

        private void LoadAllProject()
        {

            string comcod = this.GetCompCode();
            string empid = this.Request.QueryString["empid"].ToString();
            string yearmon = this.Request.QueryString["yearmon"].ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "LOADALLPROJECT", yearmon, empid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblallPrj"] = ds1.Tables[0];
            this.Data_Bind();


        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblallPrj"];
            this.gvSalbgd.DataSource = dt;
            this.gvSalbgd.DataBind();
            this.FooterCalculation((DataTable)ViewState["tblallPrj"]);
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblallPrj"];

            for (int i = 0; i < this.gvSalbgd.Rows.Count; i++)
            {

                dt.Rows[i]["saltg"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtgvsalamt")).Text.Trim()).ToString();
                dt.Rows[i]["coltg"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtgvcolamt")).Text.Trim()).ToString();


            }

            ViewState["tblallPrj"] = dt;
        }

        private void FooterCalculation(DataTable dt)
        {
            ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFsaltg")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(saltg)", "")) ?
                    0.00 : dt.Compute("Sum(saltg)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFcoltg")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(coltg)", "")) ?
              0.00 : dt.Compute("Sum(coltg)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }
        protected void lbTotal_OnClick(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void lbtnFinalUpdate_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string empid = this.Request.QueryString["empid"].ToString();
            string monthid = this.ddlyearmon.SelectedValue.ToString();
            string tsaltar = Convert.ToDouble(((Label)this.gvSalbgd.FooterRow.FindControl("lgvFsaltg")).Text).ToString();
            string tcoltar = Convert.ToDouble(((Label)this.gvSalbgd.FooterRow.FindControl("lgvFcoltg")).Text).ToString();

            DataTable dt = (DataTable)ViewState["tblallPrj"];

            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";

            bool result = SalesData.UpdateXmlTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATESALCOLTAR", ds1, null, null, empid, monthid, tsaltar, tcoltar);

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
            }



        }
    }
}