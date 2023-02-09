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

    public partial class RptTransactionSearch02 : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)

            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Transaction Search - 02";
                //this.Master.Page.Title = "Transaction Search";

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.lbljavascript.Text = "";

            if (this.chkPrint.Checked)
            {
                this.RptChequePrint();
            }
            else
            {
                this.VouPrint();
            }
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {

            this.Pnlmain.Visible = true;
            // this.PnlNarration.Visible = true;
            string comcod = this.GetCompCode();
            string SearchOption = "";

            if (this.txtvoudate.Text.Length > 0)
            {
                SearchOption = SearchOption + "voudat='" + ASTUtility.DateFormat(txtvoudate.Text.Trim()) + "' and ";
            }
            if (this.txtBankDesc.Text.Length > 0)
            {
                SearchOption = SearchOption + "cactdesc like '%" + this.txtBankDesc.Text.Trim() + "%' and ";
            }

            if (this.txtAccountHead.Text.Length > 0)
            {
                SearchOption = SearchOption + " actdesc like '%" + this.txtAccountHead.Text.Trim() + "%'  and ";
            }

            if (this.txtDetailsHead.Text.Length > 0)
            {
                SearchOption = SearchOption + "resdesc like '%" + this.txtDetailsHead.Text.Trim() + "%' and ";
            }
            if (this.txtamount.Text.Length > 0)
            {
                SearchOption = SearchOption + "amount='" + this.txtamount.Text.Trim() + "' and ";
            }
            if (this.txtchequeno.Text.Length > 0)
            {
                SearchOption = SearchOption + "chequeno like '%" + this.txtchequeno.Text.Trim() + "%' and ";
            }
            if (this.txtissueno.Text.Length > 0)
            {
                SearchOption = SearchOption + "isunum like '%" + this.txtissueno.Text.Trim() + "%' and ";
            }
            if (this.txtchequedate.Text.Length > 0)
            {
                SearchOption = SearchOption + "chequedat='" + ASTUtility.DateFormat(this.txtchequedate.Text.Trim()) + "' and ";
            }
            if (this.txtpayto.Text.Length > 0)
            {
                SearchOption = SearchOption + "payto like '%" + this.txtpayto.Text.Trim() + "%' and ";
            }
            if (this.txtnarration.Text.Length > 0)
            {
                SearchOption = SearchOption + "venar like '%" + this.txtnarration.Text.Trim() + "%' and ";
            }


            SearchOption = (SearchOption.Length) > 0 ? ASTUtility.Left(SearchOption, (SearchOption.Length - 4)) : SearchOption;

            //string SearchOption=(this.txtvoudate.Text.Length>0)?"voudat='"+ASTUtility.DateFormat(txtvoudate.Text.Trim())+"'"
            //    :  (this.txtBankDesc.Text.Length>0)?" cactdesc like '%"+this.txtBankDesc.Text.Trim()+"%'"
            //    : (this.txtAccountHead.Text.Length>0)?" actdesc like '%"+this.txtAccountHead.Text.Trim()+"%'"
            //    : (this.txtDetailsHead.Text.Length > 0) ? " resdesc like '%" + this.txtDetailsHead.Text.Trim() + "%'"
            //    : (this.txtamount.Text.Length > 0) ? "amount='" + this.txtamount.Text.Trim() + "'"
            //    :  (this.txtchequeno.Text.Length>0)?"chequeno like '%"+this.txtchequeno.Text.Trim()+"%'"
            //    : (this.txtissueno.Text.Length > 0) ? "isunum like '%" + this.txtissueno.Text.Trim() + "%'"
            //    : (this.txtchequedate.Text.Length > 0) ? "chequedat='" + ASTUtility.DateFormat(this.txtchequedate.Text.Trim()) + "'"

            //    : (this.txtpayto.Text.Length>0)?" payto like '%"+this.txtpayto.Text.Trim()+"%'"
            //    :  (this.txtnarration.Text.Length>0)?"venar like '%"+this.txtnarration.Text.Trim()+"%'":"";



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETONDATEDVOUCHER", SearchOption, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.lstVouname.Items.Clear();
                return;
            }

            this.lstVouname.DataTextField = "vounum1";
            this.lstVouname.DataValueField = "vounum";
            this.lstVouname.DataSource = ds1.Tables[0];
            this.lstVouname.DataBind();
            ds1.Dispose();
            this.lstVouname.Focus();
            if (this.lstVouname.Items.Count > 0)
                this.lstVouname.SelectedIndex = 0;
            this.ShowVoucher();



        }


        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            this.txtvoudate.Text = "";
            this.txtBankDesc.Text = "";
            this.txtAccountHead.Text = "";
            this.txtDetailsHead.Text = "";
            this.txtamount.Text = "";
            this.txtchequeno.Text = "";
            this.txtissueno.Text = "";
            this.txtchequedate.Text = "";
            this.txtpayto.Text = "";
            this.txtnarration.Text = "";
        }
        private void ShowVoucher()
        {
            string comcod = this.GetCompCode();
            string vounum = this.lstVouname.SelectedValue.ToString();

            DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
            DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);

            if (dt.Rows.Count == 0)
                return;


            DataTable dtedit = _EditDataSet.Tables[1];
            this.lblvalVoucherDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            this.lblvalVoucherNo.Text = dtedit.Rows[0]["vounum"].ToString().Substring(0, 2) + "-" + dtedit.Rows[0]["vounum"].ToString().Substring(6);
            this.lblValBankDescription.Text = dtedit.Rows[0]["cactdesc"].ToString();

            this.lblisunum.Text = dtedit.Rows[0]["isunum"].ToString();
            this.lblvalRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
            this.lblvalSirinfo.Text = dtedit.Rows[0]["srinfo"].ToString();


            this.lblvalpayto.Text = dtedit.Rows[0]["payto"].ToString();
            this.lblvalNarration.Text = dtedit.Rows[0]["venar"].ToString();
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();

            ((Label)this.dgv1.FooterRow.FindControl("lblFgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndram)", "")) ?
                           0 : dt.Compute("sum(trndram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("txtFgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trncram)", "")) ?
                  0 : dt.Compute("sum(trncram)", ""))).ToString("#,##0;(#,##0); ");




        }
        protected void txtvoudate_TextChanged(object sender, EventArgs e)
        {
            this.txtvoudate.Text = ASTUtility.DateInVal(this.txtvoudate.Text);
            this.txtBankDesc.Focus();
        }
        protected void txtchequedate_TextChanged(object sender, EventArgs e)
        {
            this.txtchequedate.Text = ASTUtility.DateInVal(this.txtchequedate.Text);
            this.txtpayto.Focus();

        }
        protected void lstVouname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowVoucher();

            this.lstVouname.Focus();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();

                }

            }
            return dt1;

        }



        private void VouPrint()
        {
            string vounum = this.lstVouname.SelectedValue.ToString();
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccPrint.aspx?Type=accVou&vounum=" +
                          vounum + "', target='_blank');</script>";
        }

        private void RptChequePrint()
        {
            try
            {

                string vounum = this.lstVouname.SelectedValue.ToString();
                string paytype = this.ChboxPayee.Checked ? "0" : "1";
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccPrint.aspx?Type=AccCheque&vounum=" +
                              vounum + "&paytype=" + paytype + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }



        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }



    }
}