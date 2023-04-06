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
namespace RealERPWEB.F_12_Inv
{
    public partial class LinkMatSpeciStock : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS STOCK REPORT ";
               
                this.lblvalprojectname.Text = this.Request.QueryString["pactdesc"].ToString();
                //this.lblvalmaterial.Text = this.Request.QueryString["rsirdesc"].ToString();
                this.lblvaldaterange.Text = "(From " + this.Request.QueryString["frmdate"].ToString() + " To " + this.Request.QueryString["todate"].ToString() + ")";
                this.MatStock();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }




        private void MatStock()
        {
            Session.Remove("UserLog");
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string usircode = this.Request.QueryString["rsircode"].ToString();
            string fdate = this.Request.QueryString["frmdate"].ToString();
            string tdate = this.Request.QueryString["todate"].ToString();
            string chalan = this.Request.QueryString["chalan"].ToString()==""?"": this.Request.QueryString["chalan"].ToString();

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTINDMATSTOCKWSPC", pactcode, usircode, fdate, tdate, chalan, "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();
                return;
            }
            Session["tbMatStc"] = ds1.Tables[0];
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report:";
                string eventdesc2 = "Project Name: " + this.Request.QueryString["pactdesc"].ToString(); ;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatStock.DataSource = dt;
            this.gvMatStock.DataBind();
            this.FooterCalculation();


        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvMatStock.FooterRow.FindControl("lgvFopening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opqty)", "")) ? 0.00 : dt.Compute("Sum(opqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lgvFRecqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rcvqty)", "")) ? 0.00 : dt.Compute("Sum(rcvqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lgvFtraninqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninqty)", "")) ? 0.00 : dt.Compute("Sum(trninqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lgvFtranoutqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnoutqty)", "")) ? 0.00 : dt.Compute("Sum(trnoutqty)", ""))).ToString("#,##0.00;(#,##0.00); ");



            ((Label)this.gvMatStock.FooterRow.FindControl("lgvFDamage")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lqty)", "")) ? 0.00 : dt.Compute("Sum(lqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lgvFtreceived")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tqty)", "")) ? 0.00 : dt.Compute("Sum(tqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lgvFisuqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueqty)", "")) ? 0.00 : dt.Compute("Sum(issueqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lgvFacstock")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(acstock)", "")) ? 0.00 : dt.Compute("Sum(acstock)", ""))).ToString("#,##0.00;(#,##0.00); ");




        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tbMatStc"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            ReportDocument rptstk = new RealERPRPT.R_12_Inv.rptProIndMatStock();

            TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            rptProjectName.Text = this.lblvalprojectname.Text;
            TextObject txtMaterial = rptstk.ReportDefinition.ReportObjects["txtMaterial"] as TextObject;
            txtMaterial.Text = this.lblvalmaterial.Text;

            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rpttxtdate.Text = this.lblvaldaterange.Text;

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}

