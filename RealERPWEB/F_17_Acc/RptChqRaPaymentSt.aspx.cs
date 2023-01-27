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
    public partial class RptChqRaPaymentSt : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Cheque Payment Status";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

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








        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpayment"];
            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();
            this.FooterCalculation();
        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblpayment"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvPayment.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ? 0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");


        }









        protected void lnkOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string SearchOption = "";


            if (this.txtrcvDate.Text.Length > 0)
            {
                SearchOption = SearchOption + "rcvdate='" + ASTUtility.DateFormat(this.txtrcvDate.Text.Trim()) + "' and ";
            }


            if (this.txtapppaydate.Text.Length > 0)
            {



                if (this.ddlapppaydate.SelectedValue == "between")
                {
                    SearchOption = "( apppaydate between '" + ASTUtility.DateFormat(this.txtapppaydate.Text.Trim()) + "' and '"
                        + ASTUtility.DateFormat(this.txtapppaydateto.Text.Trim()) + "' )  and ";

                }

                else
                {
                    SearchOption = SearchOption + "apppaydate = '" + ASTUtility.DateFormat(this.txtapppaydate.Text.Trim()) + "' and ";

                }


            }
            if (this.txtRefnum.Text.Length > 0)
            {
                SearchOption = SearchOption + "refno like '%" + this.txtRefnum.Text.Trim() + "%' and ";
            }

            if (this.txtnofbill.Text.Length > 0)
            {
                SearchOption = SearchOption + " billndesc like '%" + this.txtnofbill.Text.Trim() + "%'  and ";
            }

            if (this.txtProjectName.Text.Length > 0)
            {
                SearchOption = SearchOption + "actdesc like '%" + this.txtProjectName.Text.Trim() + "%' and ";
            }

            if (this.txtpartyName.Text.Length > 0)
            {
                SearchOption = SearchOption + "paydesc like '%" + this.txtpartyName.Text.Trim() + "%' and ";
            }

            if (this.txtchqrdate.Text.Length > 0)
            {
                SearchOption = SearchOption + "chqrdate = '" + ASTUtility.DateFormat(this.txtchqrdate.Text.Trim()) + "' and ";
            }


            if (this.txtcptpdate.Text.Length > 0)
            {
                SearchOption = SearchOption + "chqptpdate='" + ASTUtility.DateFormat(this.txtcptpdate.Text.Trim()) + "' and ";
            }


            if (this.txtBillamount.Text.Length > 0)
            {
                SearchOption = SearchOption + "amt = '" + this.txtBillamount.Text.Trim() + "' and ";
            }

            if (this.txtissueno.Text.Length > 0)
            {
                SearchOption = SearchOption + "slnum like '%" + this.txtissueno.Text.Trim() + "%' and ";
            }

            SearchOption = (SearchOption.Length) > 0 ? ASTUtility.Left(SearchOption, (SearchOption.Length - 4)) : SearchOption;



            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");


            string wchequeready = (this.rbtnChqType.SelectedIndex == 0) ? "WithOutChq" : (this.rbtnChqType.SelectedIndex == 1) ? "BillChqReady" : "";
            //this.chkwchqready.Checked?"wcready":"";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT02", "RPTBILLSTATUS", SearchOption, frmdate, toDate, wchequeready, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();

                return;
            }


            Session["tblpayment"] = ds1.Tables[0];
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccBillStatus();
            DataTable dt = (DataTable)Session["tblpayment"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void txtrcvDate_TextChanged(object sender, EventArgs e)
        {
            this.txtrcvDate.Text = ASTUtility.DateInVal(this.txtrcvDate.Text);
            this.txtapppaydate.Focus();
        }
        protected void txtapppaydate_TextChanged(object sender, EventArgs e)
        {
            this.txtapppaydate.Text = ASTUtility.DateInVal(this.txtapppaydate.Text);
            this.txtapppaydateto.Focus();

        }


        protected void txtapppaydateto_TextChanged(object sender, EventArgs e)
        {
            this.txtapppaydateto.Text = ASTUtility.DateInVal(this.txtapppaydateto.Text);
            this.txtRefnum.Focus();
        }
        protected void txtchqrdate_TextChanged(object sender, EventArgs e)
        {
            this.txtchqrdate.Text = ASTUtility.DateInVal(this.txtchqrdate.Text);
            this.txtcptpdate.Focus();
        }
        protected void txtcptpdate_TextChanged(object sender, EventArgs e)
        {
            this.txtcptpdate.Text = ASTUtility.DateInVal(this.txtcptpdate.Text);
            this.txtBillamount.Focus();
        }
        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            this.txtrcvDate.Text = "";
            this.txtapppaydate.Text = "";
            this.txtapppaydateto.Text = "";
            this.txtRefnum.Text = "";
            this.txtnofbill.Text = "";
            this.txtProjectName.Text = "";
            this.txtpartyName.Text = "";
            this.txtchqrdate.Text = "";
            this.txtcptpdate.Text = "";

            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblpayment"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                tbl1.Rows[i]["apppaydate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpaymentdate")).Text.Trim();

            }
            Session["tblpayment"] = tbl1;

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();
                DataTable dt1 = (DataTable)Session["tblpayment"];
                bool result = true;
                foreach (DataRow dr in dt1.Rows)
                {
                    string slnum = ASTUtility.Right("000000000" + dr["slnum"].ToString().Trim(), 9);
                    string rcvdate = ASTUtility.DateFormat(dr["rcvdate"].ToString());
                    string refno = dr["refno"].ToString().Trim();
                    string actcode = dr["actcode"].ToString();
                    string paycode = dr["paycode"].ToString();
                    string billnature = dr["bncode"].ToString();
                    string amt = Convert.ToDouble("0" + dr["amt"].ToString()).ToString();
                    string apppaydate = ASTUtility.DateFormat(dr["apppaydate"].ToString());

                    result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT02", "INSERTORUPPAYPROPS", slnum, rcvdate, refno, actcode, paycode, billnature, amt,
                                                               apppaydate, "", "", "", "", "", "", "");

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

        protected void ddlapppaydate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblapppdateto.Visible = (this.ddlapppaydate.SelectedValue == "between");
            this.txtapppaydateto.Visible = (this.ddlapppaydate.SelectedValue == "between");
            this.txtapppaydate.Focus();
        }
    }
}