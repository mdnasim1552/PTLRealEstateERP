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

    public partial class AccVoucherUnposted : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Voucher List (Unposted)";
                //this.Master.Page.Title = "Unposted Voucher";

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
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string refnum = "%" + this.txtrefno.Text.Trim() + "%";
            string voutype = this.ddlvoucher.SelectedValue.ToString()==""? "%": this.ddlvoucher.SelectedValue.ToString()+ "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETUNVOULIST", frmdate, todate, refnum, voutype, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvAccUnPosted.DataSource = null;
                this.gvAccUnPosted.DataBind();
                return;
            }

            Session["tblunposted"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblunposted"];
            this.gvAccUnPosted.DataSource = dt;
            this.gvAccUnPosted.DataBind();
            this.FooterCalCulation();


        }

        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["tblunposted"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvAccUnPosted.FooterRow.FindControl("lblgvFvouamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0);");

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }
        protected void gvAccUnPosted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkVoucherEdit");
                HyperLink hlnkPrintVoucher = (HyperLink)e.Row.FindControl("hlnkVoucherPrint");

                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
                hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;



                //if (vounum.Substring(0, 2) == "BD" || vounum.Substring(0, 2) == "CD")
                //    hlink1.NavigateUrl = "~/F_17_Acc/LinkGeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Management&vounum=" + vounum;
                //else  if (vounum.Substring(0, 2) == "BC" || vounum.Substring(0, 2) == "CC")
                //    hlink1.NavigateUrl = "~/F_17_Acc/LinkGeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Management&vounum=" + vounum;

                //else if (vounum.Substring(0, 2) == "JV" )
                //    hlink1.NavigateUrl = "~/F_17_Acc/LinkGeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Management&vounum=" + vounum;
                //else

                //hlink1.NavigateUrl = "~/F_17_Acc/LinkGeneralAccounts.aspx?tcode=92&tname=Contra Voucher&Mod=Management&vounum=" + vounum;

            }
        }

        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string vounum = dt.Rows[index]["vounum"].ToString();
            this.Printvoucher(vounum);
        }

        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                case "3310":
                case "3311":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;

                case "3315":
                case "3316":
                case "3317":
                    vouprint = "VocherPrint5";
                    break;

                case "3330":
                    vouprint = "VocherPrint6";
                    break;

                case "3101":
                case "3332":
                    vouprint = "VocherPrintIns";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }
        private string GetCompInstar()
        {

            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3332":
                case "3101":
                    printinstar = "Innstar";
                    break;

                default:

                    break;


            }
            return printinstar;
        }
        private void Printvoucher(string vounum)
        {

            string paytype = this.ChboxPayee.Checked ? "0" : "1";

            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
            //string currentptah = "AccPrint.aspx?Type=PostDatVou&vounum=" + vounum;
            string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;


            
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

        }
        protected void lbtnVoucherApp_Click(object sender, EventArgs e)
        {



            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            dt.Rows[index]["chkmv"] = "True";


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string vounum = dt.Rows[index]["vounum"].ToString();

            string ApprovedByid = hst["usrid"].ToString();
            string Approvedtrmid = hst["compname"].ToString();
            string ApprovedSession = hst["session"].ToString();
            string Approvedddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            // Existing Voucher

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETEXISTPOSTEDVOUCHER", vounum, "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Already Posted";
                return;

            }


            bool resultb = AccData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPUNPOSTEDVOUCHER", vounum, ApprovedByid, Approvedtrmid, ApprovedSession, Approvedddat,
                               "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resultb)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = AccData.ErrorObject["Msg"].ToString();
                return;
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            Session.Remove("tblunposted");
            DataView dv = dt.DefaultView;
            Session["tblunposted"] = dv.ToTable();
            this.Data_Bind();
            this.lbtnOk_Click(null, null);

        }

        protected void lbtnDeleteVoucher_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)Session["tblunposted"];
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

        protected void BtnVouDetials_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            // this.lblabsheading.Text = "Individual Monthly Absent Approval Details Information. Date :" + this.txtfrmDate.Text.ToString() + " To: " + this.txttoDate.Text.ToString();
            DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string vounum = dt.Rows[index]["vounum"].ToString();

            //string vounum = ((Label)this.gvabsapp02.Rows[index].FindControl("lgvEmpIdabs02")).Text.ToString(); // "%" + this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETUNVOUDETAILS", vounum, "", "", "", "", "", "", "", "");
 
            if (ds1 == null)
            {
                this.gvdetails.DataSource = null;
                this.gvdetails.DataBind();
                return;
            }

            this.lblvalvounum.Text = vounum;
            Session["tblunpostvoudetails"] = ds1.Tables[0];
            this.gvdetails.DataSource = ds1.Tables[0];
            this.gvdetails.DataBind();
            this.FooterCalCulationDetails();
            //Session["Report1"] = mgvbreakdown;
            //if (ds2.Tables[0].Rows.Count > 0)
            //    ((HyperLink)this.mgvmonabsent.HeaderRow.FindControl("mhlbtntbCdataExelabs02")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalAbs();", true);

        }


        private void FooterCalCulationDetails()
        {
            DataTable dt = (DataTable)Session["tblunpostvoudetails"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvdetails.FooterRow.FindControl("lblgvFvouamtd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0);");

        }

        protected void btnVouApproval_Click(object sender, EventArgs e)
        {


            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
             DataTable dt = (DataTable)Session["tblunposted"];
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;
            //dt.Rows[index]["chkmv"] = "True";


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string vounum = this.lblvalvounum.Text.Trim();

            string ApprovedByid = hst["usrid"].ToString();
            string Approvedtrmid = hst["compname"].ToString();
            string ApprovedSession = hst["session"].ToString();
            string Approvedddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            // Existing Voucher

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETEXISTPOSTEDVOUCHER", vounum, "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Already Posted";
                return;

            }


            bool resultb = AccData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPUNPOSTEDVOUCHER", vounum, ApprovedByid, Approvedtrmid, ApprovedSession, Approvedddat,
                               "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resultb)
            {
                string errMfsg=  AccData.ErrorObject["Msg"].ToString();
               
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMfsg + "');", true);

                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseMOdal();", true);
            string errMsg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + errMsg + "');", true);

            this.lbtnOk_Click(null, null);

            //Session.Remove("tblunposted");
            //DataView dv = dt.DefaultView;
            //Session["tblunposted"] = dv.ToTable();
            //this.Data_Bind();


        }

        protected void chkAllCheckid_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblunposted"];

            int i, index;
            if (((CheckBox)this.gvAccUnPosted.HeaderRow.FindControl("chkAllCheckid")).Checked)
            {

                for (i = 0; i < this.gvAccUnPosted.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAccUnPosted.Rows[i].FindControl("chkvmrno")).Checked = true;
                    index = (this.gvAccUnPosted.PageSize) * (this.gvAccUnPosted.PageIndex) + i;
                    dt.Rows[index]["chkmv"] = "True";
                }
            }
            else
            {
                for (i = 0; i < this.gvAccUnPosted.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAccUnPosted.Rows[i].FindControl("chkvmrno")).Checked = false;
                    index = (this.gvAccUnPosted.PageSize) * (this.gvAccUnPosted.PageIndex) + i;
                    dt.Rows[index]["chkmv"] = "False";
                }
            }
            Session["tblunposted"] = dt;
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
            DataTable dt = (DataTable)Session["tblunposted"];          

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
            DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;
            int index = row.RowIndex;
            dt.Rows[index]["chkmv"] = "True";
            Session["tblunposted"] = dt;
        }
    }
}