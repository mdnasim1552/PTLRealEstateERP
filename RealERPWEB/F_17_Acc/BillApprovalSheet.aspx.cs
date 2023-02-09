using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
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

    public partial class BillApprovalSheet : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Bill Approval Sheet";
                //this.Master.Page.Title = "Bill Approval Sheet";
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtBundleDat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");







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

        protected void GetPerMatIssu()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mNEWBUNDLE = "NEWBUNDLE";
            string bundtype = this.Request.QueryString["Type"].ToString();
            if (this.ddlPrevISSList.Items.Count > 0)
                mNEWBUNDLE = this.ddlPrevISSList.SelectedValue.ToString();


            if (mNEWBUNDLE == "NEWBUNDLE")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTBUNDLENO", "",
                       bundtype, "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["bundno"].ToString();
                    this.ddlPrevISSList.DataTextField = "bundno";
                    this.ddlPrevISSList.DataValueField = "bundno";
                    this.ddlPrevISSList.DataSource = ds1.Tables[0];
                    this.ddlPrevISSList.DataBind();

                }


            }


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string bundno = this.lblCurNo1.Text.Trim();
            string dundate = this.txtBundleDat.Text;
            string bundtype = this.Request.QueryString["Type"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETBILLAPPROVEBUNDLE", bundno, bundtype, "", "", "", "", "", "", "");
            ViewState["tblrptbundle"] = ds1.Tables[0];


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string usrname = ds1.Tables[1].Rows[0]["usrname"].ToString();

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            DataTable dt = (DataTable)ViewState["tblrptbundle"];
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.BillApproval>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptBillApprovalSheet", lst, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            ////Rpt1.SetParameters(new ReportParameter("title", "Contractor Bill Approval Sheet"));
            ////Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "Date : " + dundate));
            Rpt1.SetParameters(new ReportParameter("usrname", "Name : " + usrname));


            Rpt1.SetParameters(new ReportParameter("bundleno", "Bundle No : " + bundno));
            ////Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("footer", printFooter));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }







        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtBundleDat.Text.Trim()).ToString("dd-MMM-yyyy");
            string bundtype = this.Request.QueryString["Type"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVBUND", CurDate1, bundtype, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "bundno1";
            this.ddlPrevISSList.DataValueField = "bundno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();


        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lbtnOk.Text = "Ok";
                this.lbtnPrevISSList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.pnlBundleEntry.Visible = false;
                this.ddlPrevISSList.Items.Clear();
                this.txtBundleDat.Enabled = true;
                this.pnlbill.Visible = false;
                this.lblCurNo1.Text = "";
                this.gvbundle.DataSource = null;
                this.gvbundle.DataBind();

                return;
            }
            this.lbtnPrevISSList.Visible = false;
            this.ddlPrevISSList.Visible = false;
            this.lbtnOk.Text = "New";
            this.Get_Issue_Info();

        }



        private void Get_Issue_Info()
        {

            string comcod = this.GetCompCode();
            string bundtype = this.Request.QueryString["Type"].ToString();

            string CurDate1 = Convert.ToDateTime(this.txtBundleDat.Text.Trim()).ToString();
            string mNEWBUNDLE = "NEWBUNDLE";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtBundleDat.Enabled = false;
                mNEWBUNDLE = this.ddlPrevISSList.SelectedValue.ToString();
            }
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETBILLAPPROVEBUNDLE", mNEWBUNDLE, bundtype,
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblbundleinfo"] = ds1.Tables[0];
            Session["UserLog"] = ds1.Tables[1];


            if (mNEWBUNDLE == "NEWBUNDLE")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTBUNDLENO", CurDate1,
                       bundtype, "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["bundno"].ToString();

                }
                this.pnlbill.Visible = true;
                this.GetBillInfo();
                return;

            }

            // this.pnlBundleEntry.Visible = true;
            this.LoadddlBill();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["bundno"].ToString();
            this.txtBundleDat.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["bundate"]).ToString("dd-MMM-yyyy");




            //this.ddlUserList.SelectedItem.Text = ds1.Tables[1].Rows[0]["usrname"].ToString();


            this.MultiView1.ActiveViewIndex = 0;


            this.Data_Bind();
        }


        private void GetBillInfo()
        {
            Session.Remove("tblbillno");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string buntype = this.Request.QueryString["Type"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETBILLAPPROVAL", buntype, frmdate, todate, "", "", "", "", "", "");
            Session["tblbillno"] = ds1.Tables[0];
            this.gvbillApp.DataSource = ds1.Tables[0];
            this.gvbillApp.DataBind();

        }

        private void LoadddlBill()
        {
            string comcod = this.GetCompCode();
            string buntype = this.Request.QueryString["Type"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETBILLAPPROVAL", buntype, frmdate, todate, "", "", "", "", "", "");
            Session["tblbillno"] = ds1.Tables[0];
            this.ddlbillno.DataTextField = "vounum";
            this.ddlbillno.DataValueField = "vounum";
            this.ddlbillno.DataSource = ds1.Tables[0];
            this.ddlbillno.DataBind();
        }


        protected void Data_Bind()
        {

            this.gvbundle.DataSource = (DataTable)Session["tblbundleinfo"];
            this.gvbundle.DataBind();
            this.FooterCalculation();



        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblbundleinfo"];

            ((Label)this.gvbundle.FooterRow.FindControl("lgvFgvappbill")).Text =
            Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(billamt)", "")) ?
                    0.00 : dt.Compute("Sum(billamt)", ""))).ToString("#,##0;(#,##0); ");





        }



        protected void lnkupdate_OnClick(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);

                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            DataTable dtuser = (DataTable)Session["UserLog"];
            string bundtype = this.Request.QueryString["Type"].ToString();
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : userid;
            string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");

            DataTable tbl2 = (DataTable)Session["tblbundleinfo"];
            string comcod = this.GetCompCode();
            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetPerMatIssu();
            }

            string mRef = "";
            string mBUNDNO = this.lblCurNo1.Text.Trim();
            string mISUDAT = Convert.ToDateTime(this.txtBundleDat.Text.Trim()).ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEBUNDLEAB", "BUNDB",
                             mBUNDNO, bundtype, mISUDAT, mRef, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }





            for (int i = 0; i < tbl2.Rows.Count; i++)
            {

                string vounum = tbl2.Rows[i]["vounum"].ToString();
                string orderno = "000000000000";





                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEBUNDLEAB", "BUNDA", mBUNDNO, bundtype,
                        vounum, orderno, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            //  this.txtCurISSDate.Enabled = false;

        }

        protected void gvbundle_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblbundleinfo"];
            string bundno = this.lblCurNo1.Text.Trim();
            string billno = ((Label)this.gvbundle.Rows[e.RowIndex].FindControl("lblorgvBillno")).Text.Trim();
            string orderno = ((Label)this.gvbundle.Rows[e.RowIndex].FindControl("txtorderorg")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEBUND", bundno, billno, orderno, "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            if (result == true)
            {
                int rowindex = (this.gvbundle.PageSize) * (this.gvbundle.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblbundleinfo");
            Session["tblbundleinfo"] = dv.ToTable();
            this.Data_Bind();

        }

        protected void gvbundle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvbundle.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }







        protected void lnkAddNew_OnClick(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblbillno"];
            DataTable dt = dt1.Copy();
            DataView dv = dt.DefaultView;
            string billno = this.ddlbillno.SelectedValue;
            dv.RowFilter = ("billno='" + billno + "'");
            dt = dv.ToTable();

            DataTable dt2 = (DataTable)Session["tblbundleinfo"];
            foreach (DataRow dr in dt.Rows)
            {
                DataRow dr1 = dt2.NewRow();

                dr1["billno"] = dr["billno"].ToString();
                dr1["billno1"] = dr["billno1"].ToString();
                dr1["billref"] = dr["billref"].ToString();
                dr1["billdate"] = dr["billdate"].ToString();
                dr1["ssircode"] = dr["ssircode"].ToString();
                dr1["orderno"] = dr["orderno"].ToString();
                dr1["orderno1"] = dr["orderno1"].ToString();
                dr1["billamt"] = dr["billamt"].ToString();
                dr1["rsirdesc"] = dr["rsirdesc"].ToString();
                dr1["ssirdesc"] = dr["ssirdesc"].ToString();
                dr1["pactdesc"] = dr["pactdesc"].ToString();
                dr1["chk"] = dr["chk"].ToString();
                dt2.Rows.Add(dr1);

            }
            Session["tblbillno"] = dt;
            this.Data_Bind();

        }

        protected void lbtnSelectedBill_Click(object sender, EventArgs e)
        {


            DataTable dt1 = (DataTable)Session["tblbundleinfo"];
            DataTable dtResP = (DataTable)Session["tblbillno"];

            int i;
            for (i = 0; i < this.gvbillApp.Rows.Count; i++)
            {
                bool chkitm = ((CheckBox)this.gvbillApp.Rows[i].FindControl("chkitem")).Checked;
                string vounum = ((Label)this.gvbillApp.Rows[i].FindControl("lblgvorBillno")).Text.Trim();
                string pactcode = ((Label)this.gvbillApp.Rows[i].FindControl("lblgvpactcodeno")).Text.Trim();


                if (chkitm == true)
                {
                    DataRow[] dr2 = dtResP.Select("vounum='" + vounum + " '");

                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = 1;
                    }


                }

                else
                {
                    DataRow[] dr2 = dtResP.Select("vounum='" + vounum + " '");
                    //DataRow[] dr2 = dtResP.Select("vounum='" + vounum + " '");

                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = 0;
                    }
                }

            }

            for (i = 0; i < dtResP.Rows.Count; i++)
            {
                string chkitem = dtResP.Rows[i]["chk"].ToString();
                if (chkitem == "True")
                {
                    DataRow dr1 = dt1.NewRow();
                    dr1["vounum"] = dtResP.Rows[i]["vounum"];
                    dr1["vounum1"] = dtResP.Rows[i]["vounum1"];
                    dr1["voudate"] = dtResP.Rows[i]["voudate"];
                    dr1["billamt"] = dtResP.Rows[i]["billamt"];
                    dr1["narrations"] = dtResP.Rows[i]["narrations"];
                    dr1["pactdesc"] = dtResP.Rows[i]["pactdesc"];
                    dr1["chk"] = dtResP.Rows[i]["chk"];
                    dt1.Rows.Add(dr1);

                }
            }


            Session["tblbundleinfo"] = dt1;
            this.MultiView1.ActiveViewIndex = 0;
            this.pnlbill.Visible = false;
            this.Data_Bind();

        }
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvbillApp.HeaderRow.FindControl("chkAll")).Checked)
            {

                for (i = 0; i < this.gvbillApp.Rows.Count; i++)
                {
                    ((CheckBox)this.gvbillApp.Rows[i].FindControl("chkitem")).Checked = true;


                }


            }

            else
            {
                for (i = 0; i < this.gvbillApp.Rows.Count; i++)
                {
                    ((CheckBox)this.gvbillApp.Rows[i].FindControl("chkitem")).Checked = false;


                }

            }


        }
    }
}