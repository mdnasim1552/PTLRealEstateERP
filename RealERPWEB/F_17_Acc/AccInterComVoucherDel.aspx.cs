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
namespace RealERPWEB.F_17_Acc
{

    public partial class AccInterComVoucherDel : System.Web.UI.Page 
    {
        public static double TAmount;
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
               
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Inertcompany Voucher List";
                this.Master.Page.Title = "Intercom Voucher";

                string date = DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


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

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string refnum = "%" + this.txtrefno.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETINTERCOMVOUCHER", frmdate, todate, refnum, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvAccIntercom.DataSource = null;
                this.gvAccIntercom.DataBind();
                return;
            }
            Session["tblintercom"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblintercom"];
            this.gvAccIntercom.DataSource = dt;
            this.gvAccIntercom.DataBind();
            this.FooterCalCulation();


        }

        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["tblintercom"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvAccIntercom.FooterRow.FindControl("lblgvFvouamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ?
            0 : dt.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0);");

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }
        protected void gvAccIntercom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkVoucherEdit");
                HyperLink hlnkPrintVoucher = (HyperLink)e.Row.FindControl("hlnkVoucherPrint");

                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
                hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;

            }
        }

        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)Session["tblintercom"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string vounum = dt.Rows[index]["vounum"].ToString();
            this.Printvoucher(vounum);
        }

        private void Printvoucher(string vounum)
        {

            string paytype = "";// this.ChboxPayee.Checked ? "0" : "1";

            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
            //string currentptah = "AccPrint.aspx?Type=PostDatVou&vounum=" + vounum;
            string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
            
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

        }    

        protected void lbtnDeleteVoucher_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)Session["tblintercom"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string vounum = dt.Rows[index]["vounum"].ToString();
            
            bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELETEVOUCHERUNPOSTED", vounum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Deleted";
                return;

            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.Data_Bind();
        }     

        protected void chkAllCheckid_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblintercom"];

            int i, index;
            if (((CheckBox)this.gvAccIntercom.HeaderRow.FindControl("chkAllCheckid")).Checked)
            {

                for (i = 0; i < this.gvAccIntercom.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAccIntercom.Rows[i].FindControl("chkvmrno")).Checked = true;
                    index = (this.gvAccIntercom.PageSize) * (this.gvAccIntercom.PageIndex) + i;
                    dt.Rows[index]["chkmv"] = "True";
                }
            }
            else
            {
                for (i = 0; i < this.gvAccIntercom.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAccIntercom.Rows[i].FindControl("chkvmrno")).Checked = false;
                    index = (this.gvAccIntercom.PageSize) * (this.gvAccIntercom.PageIndex) + i;
                    dt.Rows[index]["chkmv"] = "False";
                }
            }
            Session["tblintercom"] = dt;
        }

        protected void lnkbtnChekedId_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string ApprovedByid = hst["usrid"].ToString();
            string Approvedtrmid = hst["compname"].ToString();
            string ApprovedSession = hst["session"].ToString();
            string Approvedddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string msg = "";
            DataTable dt = (DataTable)Session["tblintercom"];          

            foreach (DataRow dr in dt.Rows)
            {
                string chkemerge = dr["chkmv"].ToString();
                string vounum = dr["vounum"].ToString();
                if (chkemerge == "True")
                {
                    DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETEXISTPOSTEDVOUCHER", vounum, "", "", "", "", "", "", "", "");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        bool resultb = AccData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPUNPOSTEDVOUCHER", vounum, ApprovedByid, Approvedtrmid, ApprovedSession, Approvedddat,
                                       "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resultb)
                        {
                            msg = AccData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Voucher Posted Failed..!.."+ msg + "');", true);
                            return;
                        }
                    }                    
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Voucher Posted Successfully" + "');", true);
            this.lbtnOk_Click(null, null);
        }       

        protected void chkvmrno_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblintercom"];
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;
            int index = row.RowIndex;
            dt.Rows[index]["chkmv"] = "True";
            Session["tblintercom"] = dt;
        }
        
    }
}