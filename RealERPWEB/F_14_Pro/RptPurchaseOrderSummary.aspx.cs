using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_14_Pro
{
    public partial class RptPurchaseOrderSummary : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                   (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                DateTime startDate = DateTime.Now;
                DateTime enddate = DateTime.Now.AddDays(20);
                this.txtfrmdate.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(enddate).ToString("dd-MMM-yyyy");
                this.GetProject();
                this.GETSupplierlist();
            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProject()
        {
            string comcod = this.GetCompCode();
            DataSet ds = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPRJLIST", "", "", "", "", "", "");
            if (ds == null)
                return;
            this.ddlprjname.DataTextField = "prjdesc";
            this.ddlprjname.DataValueField = "actcode";
            this.ddlprjname.DataSource = ds.Tables[0];
            this.ddlprjname.DataBind();



        }
        private void GETSupplierlist()
        {
            string comcod = this.GetCompCode();
            DataSet ds = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETSUPPLIERLIST", "", "", "", "", "", "");
            if (ds == null)
                return;
            this.ddlsupplierlist.DataTextField = "supplierdesc";
            this.ddlsupplierlist.DataValueField = "sircode";
            this.ddlsupplierlist.DataSource = ds.Tables[0];
            this.ddlsupplierlist.DataBind();



        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                string fromdat = this.txtfrmdate.Text;
                string todat = this.txttodate.Text;
                string prjname = this.ddlprjname.SelectedValue.Trim() == "000000000000" ? "16%" : this.ddlprjname.SelectedValue.Trim() + "%";

                string supplier = this.ddlsupplierlist.SelectedValue.Trim() == "000000000000" ? "99%" : this.ddlsupplierlist.SelectedValue.Trim() + "%";

                string calltype = this.radiolist.SelectedValue == "1" ? "GETPRJWISESUPLIERSUMMARY" : "GETPRJWISESUPPLIERDETAILS";

                DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", calltype, prjname, supplier, fromdat, todat, "", "");

                if (ds1 == null)
                    return;
                Session["purorder"] = HiddenSameData(ds1.Tables[0]);
                this.Data_Bound();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }


        private void Data_Bound()
        {


            string check = this.radiolist.SelectedValue.Trim();
            if (check == "1")
            {
                this.Ordersummary.Visible = true;
                this.orderdetails.Visible = false;
                this.gvOrderSummary.DataSource = (DataTable)Session["purorder"];
                this.gvOrderSummary.DataBind();

            }
            else
            {
                this.orderdetails.Visible = true;
                this.Ordersummary.Visible = false;
                this.gvOrderDetails.DataSource = (DataTable)Session["purorder"];
                this.gvOrderDetails.DataBind();
            }

        }

        private DataTable HiddenSameData(DataTable ds1)
        {



            string pactcode = ds1.Rows[0]["pactcode"].ToString();
            string ssircode = ds1.Rows[0]["ssircode"].ToString();

            for (int j = 1; j < ds1.Rows.Count; j++)
            {
                if (ds1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = ds1.Rows[j]["pactcode"].ToString();
                    ds1.Rows[j]["pactdesc"] = "";

                    if (ds1.Rows[j]["ssircode"].ToString() == ssircode)
                    {
                        ssircode = ds1.Rows[j]["ssircode"].ToString();
                        ds1.Rows[j]["sirdesc"] = "";
                    }

                    else
                    {
                        ssircode = ds1.Rows[j]["ssircode"].ToString();
                    }
                }

                else
                {
                    pactcode = ds1.Rows[j]["pactcode"].ToString();

                    ssircode = ds1.Rows[j]["ssircode"].ToString();

                }
            }



            return ds1;

        }

        protected void gvOrderSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrderSummary.PageIndex = e.NewPageIndex;
            this.Data_Bound();
        }

        protected void gvOrderDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrderDetails.PageIndex = e.NewPageIndex;
            this.Data_Bound();
        }

        protected void ddlBatchPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string check = this.radiolist.SelectedValue.Trim();
            if (check == "1")
            {
                this.gvOrderSummary.PageSize = Convert.ToInt32(this.ddlBatchPage.SelectedValue.ToString());
            }
            else
            {
                this.gvOrderDetails.PageSize = Convert.ToInt32(this.ddlBatchPage.SelectedValue.ToString());
            }
            this.Data_Bound();
        }
    }
}