using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using RealEntity.C_22_Sal;
namespace RealERPWEB.F_14_Pro
{
    public partial class PurSumMatWise : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                //        (DataSet) Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                //    (DataSet) Session["tblusrlog"]);
                //((LinkButton) this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //  Hashtable hst=new Hashtable();


                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                    (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));


                //if (hst["comcod"]=="3101" || hst["comcod"]=="")

                //((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Summary (Material Wise)";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ddlprojname.Visible = false;
                this.txtFDate.Text = (this.Request.QueryString["Date1"].Length > 0) ? this.Request.QueryString["Date1"].ToString() : Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = (this.Request.QueryString["Date2"].Length > 0) ? this.Request.QueryString["Date2"].ToString() : Convert.ToDateTime(txtFDate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                string qprjcode = Request.QueryString["prjcode"] ?? "";
                if (qprjcode.Length > 0)
                {
                    this.ddlprojname.Visible = true;
                    getprojmodule();
                    ddlprojname.Enabled = false;


                }
                this.ShowAll(); //  by defalut summary

            }
        }


        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }
        private void getprojmodule()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETPROJECTNAME", "%%", "", "", "", "", "", "", "", "");
            this.ddlprojname.DataSource = ds1.Tables[0];
            this.ddlprojname.DataTextField = "actdesc";
            this.ddlprojname.DataValueField = "actcode";
            this.ddlprojname.DataBind();
            ddlprojname.SelectedValue = Request.QueryString["prjcode"].ToString();


        }
        private void ShowAll()
        {
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string prjcode = "";
            if (Request.QueryString["prjcode"] != null)
            {
                prjcode = (Request.QueryString["prjcode"].Length > 0 ? Request.QueryString["prjcode"].ToString() + "%" : "%%");
            }
            else
            {
                prjcode = "%%";
            }

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURSUMMARYALLPRJ", fromdate, todate, "", prjcode, "", "", "", "", "");
            // DataSet ds = MktData.GetTransInfo (comcod, "SP_REPORT_ACCOUNTS_BGD", "GETYSALBGDBREAK", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurmatwise.DataSource = null;
                this.gvPurmatwise.DataBind();
                return;
            }

            Session["tblmainhead"] = ds1.Tables[0];
            Session["tblbreakd"] = ds1.Tables[1];
            Session["tblbmainhead"] = ds1.Tables[0];
            Session["tblabpamt"] = ds1.Tables[4];
            this.LoadGrid();
        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            this.ShowAll();
        }

        private void LoadGrid()
        {
            string comcod = this.GetComeCode();
            string date1 = this.txtFDate.Text;
            string date2 = this.txttodate.Text;
            DataTable dt = (DataTable)Session["tblmainhead"];
            DataTable dt1 = (DataTable)Session["tblabpamt"];
            this.gvPurmatwise.DataSource = dt;
            this.gvPurmatwise.DataBind();
            this.FooterCal();
            this.ShowGraph();
            this.abppanel.Visible = true;
            this.abpamt.Text = (dt1.Rows.Count == 0) ? "0.00" : Convert.ToDouble(dt1.Rows[0]["topamt"]).ToString("#,##0.00;(#,##0.00); ");

            if (dt.Rows.Count > 0)
            {
                HyperLink FotLnk = (HyperLink)this.gvPurmatwise.FooterRow.FindControl("lgvFtotal");
                string prjcode = Request.QueryString["prjcode"] ?? "";

                 prjcode = (prjcode.Length > 0) ? prjcode : "";

                //string prjcode = (Request.QueryString["prjcode"].Length > 0) ? Request.QueryString["prjcode"].ToString() : "";
                FotLnk.NavigateUrl = "~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur&comcod=" + comcod + "&prjcode=" + prjcode + "&Date1=" + date1 + "&Date2=" + date2;
            }

        }

        private void ShowGraph()
        {

            DataTable dt = ((DataTable)Session["tblbmainhead"]);
            DataTable dt2 = ((DataTable)Session["tblbreakd"]);
            DataTable dt3 = ((DataTable)Session["tblbmainhead"]);

            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.PrjSummary>();
            var lst2 = dt2.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.PrjSummary>();
            var lst3 = dt3.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.PrjSummary>();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);
            var json2 = jsonSerialiser.Serialize(lst2);
            var json3 = jsonSerialiser.Serialize(lst3);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + json + "','" + json2 + "', '" + json3 + "')", true);
            //,'" + json2 + "'


        }



        protected void lnkgvWDescgp_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string mrptcode = ((DataTable)Session["tblmainhead"]).Rows[index]["mrptcode"].ToString();
            string colst = ((DataTable)Session["tblmainhead"]).Rows[index]["colst"].ToString();
            DataTable dt = ((DataTable)Session["tblmainhead"]);
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = ("rptcode like '%000'");
            dt = dv.ToTable();

            DataRow[] dr1 = dt.Select("mrptcode='" + mrptcode + "'");
            dr1[0]["colst"] = (colst == "0") ? "1" : "0";


            // For Status 0
            foreach (DataRow dr2 in dt.Rows)
            {
                if (dr2["mrptcode"] != mrptcode)
                {
                    dr2["colst"] = "0";

                }
            }

            colst = (dt.Select("mrptcode='" + mrptcode + "'"))[0]["colst"].ToString();
            if (colst == "1")
            {
                DataTable dtb = ((DataTable)Session["tblbreakd"]).Copy();
                dv = dtb.DefaultView;
                dv.RowFilter = ("mrptcode='" + mrptcode + "' and rptcode not like '%000'");
                dtb = dv.ToTable();
                dt.Merge(dtb);

            }

            dv = dt.DefaultView;
            dv.Sort = ("mrptcode, rptcode");
            Session["tblmainhead"] = dv.ToTable();
            this.LoadGrid();


        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblmainhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rptcode like '%000'");
            dt = dv.ToTable();
            if (dt.Rows.Count == 0)
                return;

            ((HyperLink)this.gvPurmatwise.FooterRow.FindControl("lgvFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                             0 : dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void gvPurmatwise_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetComeCode();

                LinkButton lnkgvWDescgp = (LinkButton)e.Row.FindControl("lnkgvWDescgp");

                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lgvqty = (Label)e.Row.FindControl("lgvqty");
                Label lgvrate = (Label)e.Row.FindControl("lgvrate");
                Label lgvAmt = (Label)e.Row.FindControl("lgvBudgetAmtgp");
                Label lgvunit = (Label)e.Row.FindControl("lgvunit");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcode")).ToString();


                if (code == "")
                {
                    return;
                }

                if (ASTUtility.Right(code, 3) == "000")
                {

                    lnkgvWDescgp.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvqty.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvrate.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvAmt.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvunit.Attributes["style"] = "color:maroon;font-weight:bold;";

                }


            }
        }

        //protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string comcod = this.GetComeCode();
        //    string date1 = this.txtFDate.Text;
        //    string date2 = this.txttodate.Text;

        //    int index = ddlReport.SelectedIndex;
        //    switch (index)
        //    {
        //        case 0:


        //            //Response.Write("<script>window.open('RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum&comcod=" + comcod + "&Date1=" + date1 + "&Date2=" + date2 + "','_blank');</script>");
        //            //Response.Redirect("~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum&comcod=" + comcod + "&Date1=" + date1 + "&Date2=" + date2);
        //            break;
        //        case 1:

        //            break;
        //        case 2:

        //            break;
        //        //default:
        //        //    break;
        //    }
        //}
    }
}