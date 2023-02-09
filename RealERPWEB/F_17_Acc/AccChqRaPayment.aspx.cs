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
    public partial class AccChqRaPayment : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = type == "ChequeReady" ? "Cheque Ready" : "Cheque Payment to Party";
                this.ViewSection();
                //this.Master.Page.Title = type == "ChequeReady" ? "Cheque Ready" : "Cheque Payment to Party";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                CommonButton();

            }

        }
        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChequeReady":
                    this.gvPayment.Columns[10].Visible = false;
                    break;
                case "ChequePayment":
                    this.gvPayment.Columns[9].Visible = false;
                    break;


            }

        }


        protected void ibtnFindRefNo_Click(object sender, EventArgs e)
        {

            this.ShowChequeReadyOrPay();


        }
        private void ShowChequeReadyOrPay()
        {
            ViewState.Remove("tblpayment");
            string comcod = this.GetCompCode();
            string refno = "%" + this.txtSearch.Text.Trim() + "%";
            string Type = this.Request.QueryString["Type"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT02", "GETCHEQUEREADY", refno, Type, "", "", "", "", "", "", "");
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
            string Type = this.Request.QueryString["Type"].ToString();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChequeReady":

                    break;
                case "ChequePayment":
                    this.PrintReadyCheque();
                    break;


            }
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

                tbl1.Rows[i]["refno"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvref")).Text.Trim();
                tbl1.Rows[i]["amt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvbillamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["apppaydate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpaymentdate")).Text.Trim();
                tbl1.Rows[i]["chqready"] = (((CheckBox)this.gvPayment.Rows[i].FindControl("chkready")).Checked) ? "True" : "False";
                tbl1.Rows[i]["chqptparty"] = (((CheckBox)this.gvPayment.Rows[i].FindControl("lgvchkrptparty")).Checked) ? "True" : "False";


            }
            ViewState["tblpayment"] = tbl1;

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChequeReady":
                    this.UpdateChequeReady();
                    break;
                case "ChequePayment":
                    this.UpdateChqPaytoParty();
                    break;


            }





        }

        private void UpdateChequeReady()
        {

            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();

                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;



                foreach (DataRow dr in dt1.Rows)
                {
                    string slnum = dr["slnum"].ToString().Trim();
                    string refno = dr["refno"].ToString().Trim();

                    string amt = Convert.ToDouble("0" + dr["amt"].ToString()).ToString();
                    string apppaydate = ASTUtility.DateFormat(dr["apppaydate"].ToString());
                    string chqreadydate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    string chqready = dr["chqready"].ToString();
                    if (chqready == "True")
                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT02", "UPDATECHEQUEREADY", slnum, refno, amt,
                                                                   apppaydate, chqreadydate, chqready, "", "", "", "", "", "", "", "", "");

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

        private void UpdateChqPaytoParty()
        {

            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;
                foreach (DataRow dr in dt1.Rows)
                {
                    string slnum = dr["slnum"].ToString().Trim();
                    string refno = dr["refno"].ToString().Trim();

                    string amt = Convert.ToDouble("0" + dr["amt"].ToString()).ToString();
                    string apppaydate = ASTUtility.DateFormat(dr["apppaydate"].ToString());
                    string chqptopartydate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    string chqptparty = dr["chqptparty"].ToString();
                    if (chqptparty == "True")
                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT02", "UPDATECHEQUEPTPARTY", slnum, refno, amt,
                                                                   apppaydate, chqptopartydate, chqptparty, "", "", "", "", "", "", "", "", "");

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