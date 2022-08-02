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
    public partial class RptPurOrderTopSheet : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Order Top Sheet";
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.GetPrjName();



            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetPrjName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string SrchSupplier = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPROJECTNAMETOPSHEET", SrchSupplier, userid, "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc1";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            ViewState["tblprjName"] = ds1.Tables[0];
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblpurordertopsheet");
            string comcod = this.GetCompCode();

            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string prjname = this.ddlPrjName.SelectedValue.ToString()=="000000000000"? "16%": ddlPrjName.SelectedValue.ToString()+"%";
            DataSet ds3;

            ds3 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SUPPLIERWRKORDERTOPSHEET", fromdate, todate, prjname, "", "", "", "", "", "");
           
            if (ds3 == null)
            {
                this.gvPurOrderTopSheet.DataSource = null;
                this.gvPurOrderTopSheet.DataBind();
                return;
            }
            DataTable dt = ds3.Tables[0];

            Session["tblpurordertopsheet"] = dt;

            this.Data_Bind();
        
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpurordertopsheet"];

            if (dt.Rows.Count > 0)
            {
                this.gvPurOrderTopSheet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvPurOrderTopSheet.DataSource = dt;
                this.gvPurOrderTopSheet.DataBind();

                this.FooterCalculation();
             

            }
            else
            {
                this.gvPurOrderTopSheet.DataSource = null;
                this.gvPurOrderTopSheet.DataBind();
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }







        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblpurordertopsheet"];

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvPurOrderTopSheet.FooterRow.FindControl("lgAmountFb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ? 0.00 : dt.Compute("sum(amount)", ""))).ToString("#,##0;(#,##0); ");
            Session["Report1"] = gvPurOrderTopSheet;
          
            Session["ReportName"] = "Pur Order Top Sheet";
            ((HyperLink)this.gvPurOrderTopSheet.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RDLCViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        protected void gvPurOrderTopSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvPurOrderTopSheet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurOrderTopSheet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}