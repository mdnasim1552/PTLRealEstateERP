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
namespace RealERPWEB.F_04_Bgd
{
    public partial class RptBgdAll : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.txtDateFrom.Text = "01" + date.Substring(2);
                this.txtDateto.Text = date;
            }
        }


        private string GetCompCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCod();


            string todate = this.txtDateto.Text.Trim();

            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO", "RPTBUDGETALL", todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();

                return;

            }

            Session["bgdall"] = ds1.Tables[0];

            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["bgdall"];

            this.gvMatStock.DataSource = dt;
            this.gvMatStock.DataBind();
            this.FooterCal();

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["bgdall"];

            ((Label)this.gvMatStock.FooterRow.FindControl("lrvbgdf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tosalval)", "")) ?
                0.00 : dt.Compute("Sum(tosalval)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lconbgdf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbgdamt)", "")) ?
                0.00 : dt.Compute("Sum(cbgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lgvgbdgf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(gbgd)", "")) ?
               0.00 : dt.Compute("Sum(gbgd)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lgvtbdgf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tbgdamt)", "")) ?
                0.00 : dt.Compute("Sum(tbgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvMatStock.FooterRow.FindControl("lgvtpamtf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tpamt)", "")) ?
            //   0.00 : dt.Compute("Sum(tpamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvMatStock.FooterRow.FindControl("lgvcfamtf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cfamt)", "")) ?
            //    0.00 : dt.Compute("Sum(cfamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }

        protected void gvMatStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lconbgd");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lrvbgd");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lgvcfamt");
                HyperLink hlink4 = (HyperLink)e.Row.FindControl("lgvtpamt");
                HyperLink hlink5 = (HyperLink)e.Row.FindControl("lgvtbdg");
                HyperLink hlink6 = (HyperLink)e.Row.FindControl("lgvmargin");

                string date1 = this.txtDateto.Text;
                string comcod = this.GetCompCod();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                hlink1.NavigateUrl = "~/F_04_Bgd/RptBgdConsAll.aspx?prjcode=" + pactcode + "&Date1=" + date1 + "&pactdesc=" + pactdesc;
                hlink2.NavigateUrl = "~/F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod=&prjcode=" + pactcode + "&Date1=" + date1;
                hlink3.NavigateUrl = "~/F_08_PPlan/RptProTarget.aspx?Type=RealFlow&comcod=" + comcod + "&prjcode=" + pactcode;
                hlink4.NavigateUrl = "~/F_08_PPlan/ProTargetTimeBasis.aspx?Type=GrpWise&prjcode=" + pactcode + "&sircode=&flrcod=";
                hlink5.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdGrWiseDet&comcod=&prjcode=" + pactcode;
                hlink6.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk&comcod=&prjcode=" + pactcode;






            }
        }
    }
}