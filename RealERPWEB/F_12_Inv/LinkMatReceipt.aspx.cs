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
    public partial class LinkMatReceipt : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS RECEIPT REPORT";
                this.ViewSection();



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void ViewSection()
        {

            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "MatReceipt":
                    this.MatReceipt();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "BudgetedMat":
                    this.BudgetedMat();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;




            }


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void MatReceipt()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.Request["pactcode"].ToString();
            string rsircode = this.Request["rsircode"].ToString();
            string date = this.Request["date"].ToString(); ;


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTINDMATRECEIPT", pactcode, rsircode, date, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();
                return;
            }
            Session["tbMatStc"] = ds1.Tables[0];
            this.lblvalprojectname.Text = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            this.lblvalmatname.Text = ds1.Tables[1].Rows[0]["resdesc"].ToString();
            this.lblvaldat.Text = this.Request.QueryString["date"].ToString();


            this.Data_Bind();





        }

        private void BudgetedMat()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.Request["pactcode"].ToString();
            string rsircode = this.Request["rsircode"].ToString();


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTINDRESBASIS", pactcode, "AAA", "12", "", "", "", rsircode, "", "");

            if (ds1 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }
            Session["tbMatStc"] = ds1.Tables[0];
            this.lblvalprojectname.Text = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            this.lblvalmatname.Text = ds1.Tables[1].Rows[0]["resdesc"].ToString();


            this.Data_Bind();



        }
        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            DataTable dt = (DataTable)Session["tbMatStc"];

            switch (Type)
            {

                case "MatReceipt":

                    this.gvMatStock.DataSource = dt;
                    this.gvMatStock.DataBind();
                    this.FooterCalculation();
                    break;

                case "BudgetedMat":
                    this.lbldate.Visible = false;
                    this.lblvaldat.Visible = false;
                    this.gvRptResBasis.DataSource = dt;
                    this.gvRptResBasis.DataBind();
                    this.FooterCalculation();
                    break;
            }



        }


        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tbMatStc"];
            if (dt.Rows.Count == 0)
                return;


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "MatReceipt":

                    ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFinqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(inqty)", "")) ? 0.00 : dt.Compute("Sum(inqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFoutqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(outqty)", "")) ? 0.00 : dt.Compute("Sum(outqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFclsqty")).Text = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["clqty"]).ToString("#,##0.00;(#,##0.00); ");


                    break;

                case "BudgetedMat":
                    ((Label)this.gvRptResBasis.FooterRow.FindControl("lbftTqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptqty)", "")) ? 0.00 : dt.Compute("sum(rptqty)", ""))).ToString("#,##0.00;(#,##0.00);");
                    ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFTotalCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rptamt)", "")) ? 0.00 : dt.Compute("Sum(rptamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    break;
            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            //DataTable dt = (DataTable)Session["tbMatStc"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fdate=this.txtfromdate.Text.ToString();
            //string tdate=this.txttodate.Text.ToString();
            //string rsircode = this.ddlMatName.SelectedValue.ToString();

            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptMaterialsStock();


            //TextObject txtCompName = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //txtCompName.Text = comnam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rptProjectName.Text ="Project: "+ this.ddlProName.SelectedItem.Text;

            //TextObject txtMaterials =rptstk.ReportDefinition.ReportObjects["txtMaterials"] as TextObject;
            //txtMaterials.Text = "Materials: " + this.ddlMatName.SelectedItem.Text + "   Unit:" + (((DataTable)ViewState["tblmat"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"]; ;


            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtdate.Text = "From: " + fdate + " To: " + tdate;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Print Report:";
            //    string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


    }
}

