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
using System.Collections.Generic;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_17_Acc

{
    public partial class RptAccSales : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        UserManGenAccount objUserService = new UserManGenAccount();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Advance Sales, Sales, Received, Receivable , Cost";
                //this.Master.Page.Title = "Advance Sales, Sales, Received, Receivable , Cost";
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblsales");
            string Date = this.txtCurDate.Text;
            List<RealEntity.C_17_Acc.EClassSalesDetails> lst = objUserService.ShowSalesDetails(Date);
            // List<SalesOpening> lst = objwcfservice.ShowSalesOpening();
            Session["tblsales"] = lst;
            this.Data_Bind();
        }

        protected void Data_Bind()
        {
            try
            {
                List<RealEntity.C_17_Acc.EClassSalesDetails> lst = (List<RealEntity.C_17_Acc.EClassSalesDetails>)Session["tblsales"];
                this.gvSalesDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvSalesDet.DataSource = lst;
                this.gvSalesDet.DataBind();
                this.FooterCalCulation(lst);
            }
            catch (Exception ex)
            {


            }

        }

        private void FooterCalCulation(List<RealEntity.C_17_Acc.EClassSalesDetails> lst)
        {
            //double sum = 0.00;
            //foreach (SalesOpening  r in lst)
            //sum=sum+r.opnamt;


            // double sum = lst.Select(p => p.opnamt).Sum();
            ((Label)this.gvSalesDet.FooterRow.FindControl("lgvFadsale")).Text = Convert.ToDouble((lst.Select(p => p.advsale).Sum())).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalesDet.FooterRow.FindControl("lgvFsale")).Text = Convert.ToDouble((lst.Select(p => p.sale).Sum())).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalesDet.FooterRow.FindControl("lgvFreceived")).Text = Convert.ToDouble((lst.Select(p => p.received).Sum())).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalesDet.FooterRow.FindControl("lgvFrecagsal")).Text = Convert.ToDouble((lst.Select(p => p.recagsal).Sum())).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalesDet.FooterRow.FindControl("lgvFreceivable")).Text = Convert.ToDouble((lst.Select(p => p.receivable).Sum())).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalesDet.FooterRow.FindControl("lgvFcost")).Text = Convert.ToDouble((lst.Select(p => p.costam).Sum())).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalesDet.FooterRow.FindControl("lgvFmargin")).Text = Convert.ToDouble((lst.Select(p => p.margin).Sum())).ToString("#,##0;(#,##0); ");

        }


        protected void gvCostOfFund_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.rptAccSales();
            List<RealEntity.C_17_Acc.EClassSalesDetails> lst = (List<RealEntity.C_17_Acc.EClassSalesDetails>)Session["tblsales"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "Date: As On " + Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(lst);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvSalesDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalesDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}