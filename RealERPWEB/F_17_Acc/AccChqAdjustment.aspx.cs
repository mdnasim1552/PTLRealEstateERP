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
namespace RealERPWEB.F_17_Acc
{
    public partial class AccChqAdjustment : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Adjustment (Date Issue)";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.Master.Page.Title = "Adjustment (Date Issue)";


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




        protected void ibtnFindRefNo_Click(object sender, EventArgs e)
        {

            this.ShowChequeAdjustment();


        }
        private void ShowChequeAdjustment()
        {
            ViewState.Remove("tblpayment");
            string comcod = this.GetCompCode();
            string refno = "%" + this.txtSearch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT02", "GETCHEQUEREGISTER", refno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                return;

            }
            ViewState["tblpayment"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblpayment"];
            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();
            this.FooterCalculation();
        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblpayment"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvPayment.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ? 0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintBillAdjustmentDate();





            //string Type = this.Request.QueryString["Type"].ToString();
            //switch (Type)
            //{
            //    case "ChequeReady":

            //        break;
            //    case "ChequePayment":
            //        this.PrintReadyCheque();
            //        break;


            //}
        }

        private void PrintBillAdjustmentDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccBillAdjustmentDate();
            DataTable dt = (DataTable)ViewState["tblpayment"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintReadyCheque()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccBillStatus();
            DataTable dt = (DataTable)ViewState["tblpayment"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {


                tbl1.Rows[i]["adjdate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvadjdate")).Text.Trim() == "" ? "01-Jan-1900" : ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvadjdate")).Text.Trim();
                tbl1.Rows[i]["remarks"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvremarks")).Text.Trim();



            }
            ViewState["tblpayment"] = tbl1;

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();

                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;



                foreach (DataRow dr in dt1.Rows)
                {
                    string slnum = dr["slnum"].ToString().Trim();
                    string adjdate = ASTUtility.DateFormat(dr["adjdate"].ToString());
                    string remarks = dr["remarks"].ToString().Trim();

                    if (adjdate.Length > 0)
                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT02", "UPDATEPAYPROADJ", slnum, adjdate, remarks,
                                                                   "", "", "", "", "", "", "", "", "", "", "", "");

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }


                }







            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }







        }

    }
}