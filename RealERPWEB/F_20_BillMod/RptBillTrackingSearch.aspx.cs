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
namespace RealERPWEB.F_20_BillMod
{
    public partial class RptBillTrackingSearch : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Bill Status Information";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpayment"];
            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();
            // this.FooterCalculation();
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string SearchOption = "";

            if (this.txtissueno.Text.Length > 0)
            {
                SearchOption = SearchOption + "slnum like '%" + this.txtissueno.Text.Trim() + "%' and ";
            }


            if (this.TxtDept.Text.Length > 0)
            {
                SearchOption = SearchOption + "deptdesc like '%" + this.TxtDept.Text.Trim() + "%' and ";
            }

            if (this.txtRefnum.Text.Length > 0)
            {
                SearchOption = SearchOption + " refno like '%" + this.txtRefnum.Text.Trim() + "%'  and ";
            }

            if (this.txtnofbill.Text.Length > 0)
            {
                SearchOption = SearchOption + " billndesc like '%" + this.txtnofbill.Text.Trim() + "%'  and ";
            }

            if (this.txtTranDate.Text.Length > 0)
            {
                SearchOption = SearchOption + "trndate='" + ASTUtility.DateFormat(this.txtTranDate.Text.Trim()) + "%' and ";
            }

            if (this.txtTranDept.Text.Length > 0)
            {
                SearchOption = SearchOption + "tdeptdesc like '%" + this.txtTranDept.Text.Trim() + "%' and ";
            }

            if (this.txtProjectName.Text.Length > 0)
            {
                SearchOption = SearchOption + "actdesc like '%" + this.txtProjectName.Text.Trim() + "%' and ";
            }

            if (this.txtpartyName.Text.Length > 0)
            {
                SearchOption = SearchOption + "paydesc like '%" + this.txtpartyName.Text.Trim() + "%' and ";
            }

            if (this.txtBillamount.Text.Length > 0)
            {
                SearchOption = SearchOption + "amt = '" + this.txtBillamount.Text.Trim() + "' and ";
            }

            SearchOption = (SearchOption.Length) > 0 ? ASTUtility.Left(SearchOption, (SearchOption.Length - 4)) : SearchOption;



            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            //// string wchequeready = this.chkwchqready.Checked ? "wcready" : "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT02", "RPTBILLSEARCHTRACKING", frmdate, toDate, SearchOption, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();

                return;
            }


            Session["tblpayment"] = ds1.Tables[0];
            this.Data_Bind();
        }

        protected void lblfrmdate_DataBinding(object sender, EventArgs e)
        {

        }
        protected void txtrcvDate_TextChanged(object sender, EventArgs e)
        {
            //this.txtrcvDate.Text = ASTUtility.DateInVal(this.txtrcvDate.Text);
            //this.txtapppaydate.Focus();
        }
        protected void txtchqrdate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            //this.txtrcvDate.Text = "";
            //this.txtapppaydate.Text = "";
            //this.txtapppaydateto.Text = "";
            this.txtRefnum.Text = "";
            this.txtnofbill.Text = "";
            this.txtProjectName.Text = "";
            this.txtpartyName.Text = "";
            //this.txtchqrdate.Text = "";
            //this.txtcptpdate.Text = "";


            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }
        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                tbl1.Rows[i]["apppaydate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpaymentdate")).Text.Trim();

            }
            ViewState["tblpayment"] = tbl1;

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

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
                DataTable dt1 = (DataTable)ViewState["tblpayment"];
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
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    }


                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }





        }

        protected void txtcptpdate_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtapppaydateto_TextChanged(object sender, EventArgs e)
        {
            //this.txtapppaydateto.Text = ASTUtility.DateInVal(this.txtapppaydateto.Text);
            // this.txtRefnum.Focus();

        }
        protected void txtapppaydate_TextChanged(object sender, EventArgs e)
        {
            //    this.txtapppaydate.Text = ASTUtility.DateInVal(this.txtapppaydate.Text);
            //    this.txtapppaydateto.Focus();
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");

            DataTable dt1 = (DataTable)Session["tblpayment"];
            ReportDocument rptstate = new RealERPRPT.R_20_BillMod.RptBillTrackingSearch();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date:As On  " + Convert.ToDateTime(this.txtfrmDate.Text.Trim()).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt1);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {

        }



    }
}