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
namespace RealERPWEB.F_14_Pro
{
    public partial class PurBillBundle : System.Web.UI.Page
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

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "SuppEntry") ? "Supplier Bundle Entry"
                //    : (this.Request.QueryString["Type"].ToString() == "MgtSuppEntry") ? "Supplier Bundle Edit"
                //    : (this.Request.QueryString["Type"].ToString() == "ContEntry") ? "Contractor Bundle Entry"
                //    : (this.Request.QueryString["Type"].ToString() == "MgtConEntry") ? "Contractor Bundle Edit" : "";


                this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtCurISSDate_CalendarExtender.EndDate = System.DateTime.Today;



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
            string bundtype = (this.Request.QueryString["Type"].ToString() == "SuppEntry") ? "Supp" : "Cont";
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
            string date = this.txtCurISSDate.Text.Trim();
            string bundno = this.lblCurNo1.Text.Trim();
            string bundtype = (this.Request.QueryString["Type"].ToString() == "SuppEntry") ? "Supp" : "Cont";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTBUNDLEINFO", bundno, bundtype, "", "", "", "", "", "", "");
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
            DataTable dt = (DataTable)ViewState["tblrptbundle"];
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.BundleRpt>();
            LocalReport Rpt1 = new LocalReport();
            if (bundtype == "Supp")
                Rpt1 = RealERPRDLC.RDLCAccountSetup.GetLocalReport("R_14_Pro.RptBundle", lst, null, null);
            else
                Rpt1 = RealERPRDLC.RDLCAccountSetup.GetLocalReport("R_14_Pro.RptConBundle", lst, null, null);
            //if (bundtype == "Supp")
            //    Rpt1.SetParameters (new ReportParameter ("title", "Supplier Bill Approval Sheet"));
            //else
            //    Rpt1.SetParameters (new ReportParameter ("title", "Contractor Bill Approval Sheet"));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtcomname", comnam));

            Rpt1.SetParameters(new ReportParameter("bundno", bundno));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("footer", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }







        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string bundtype = (this.Request.QueryString["Type"].ToString() == "SuppEntry") ? "Supp" : "Cont";
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
                this.txtCurISSDate.Enabled = true;
                this.pnlbill.Visible = false;
                this.txtMIsuRef.Text = "";
                this.gvbundle.DataSource = null;
                this.gvbundle.DataBind();
                return;
            }
            this.lbtnPrevISSList.Visible = false;
            this.ddlPrevISSList.Visible = false;

            //this.txtsmcr.Visible = false;
            this.lbtnOk.Text = "New";
            this.Get_Issue_Info();
            //test
            //test


        }



        private void Get_Issue_Info()
        {

            string comcod = this.GetCompCode();
            string bundtype = (this.Request.QueryString["Type"].ToString() == "SuppEntry") ? "Supp" : "Cont";

            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            string mNEWBUNDLE = "NEWBUNDLE";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                mNEWBUNDLE = this.ddlPrevISSList.SelectedValue.ToString();
            }
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETBUNDLEINFO", mNEWBUNDLE, bundtype,
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblbundleinfo"] = ds1.Tables[0];
            ViewState["UserLog"] = ds1.Tables[1];


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
            this.pnlBundleEntry.Visible = true;
            this.LoadddlBill();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["bundno"].ToString();
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["bunddate"]).ToString("dd-MMM-yyyy");
            this.txtMIsuRef.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.MultiView1.ActiveViewIndex = 0;


            this.Data_Bind();
        }


        private void GetBillInfo()
        {
            Session.Remove("tblbillno");
            string comcod = this.GetCompCode();
            string buntype = (this.Request.QueryString["Type"].ToString() == "SuppEntry" ? "Supp" : "Cont");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETBILLNO", buntype, "", "", "", "", "", "", "", "");
            Session["tblbillno"] = ds1.Tables[0];
            this.gvbill.DataSource = ds1.Tables[0];
            this.gvbill.DataBind();

        }

        private void LoadddlBill()
        {
            string comcod = this.GetCompCode();
            string buntype = (this.Request.QueryString["Type"].ToString() == "SuppEntry" ? "Supp" : "Cont");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETBILLNO", buntype, "", "", "", "", "", "", "", "");
            Session["tblbillno"] = ds1.Tables[0];
            this.ddlbillno.DataTextField = "billno1";
            this.ddlbillno.DataValueField = "billno";
            this.ddlbillno.DataSource = ds1.Tables[0];
            this.ddlbillno.DataBind();
        }
        protected void Data_Bind()
        {
            this.gvbundle.DataSource = (DataTable)ViewState["tblbundleinfo"];
            this.gvbundle.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)ViewState["tblbundleinfo"]; ;
            //((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTReqAmt")).Text =
            //    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(preqamt)", "")) ?
            //        0.00 : tbl1.Compute("Sum(preqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvbundle.FooterRow.FindControl("lblgvfamnt")).Text =
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
            DataTable dtuser = (DataTable)ViewState["UserLog"];
            string bundtype = (this.Request.QueryString["Type"].ToString() == "SuppEntry") ? "Supp" : "Cont";
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
            DataTable tbl2 = (DataTable)ViewState["tblbundleinfo"];
            string comcod = this.GetCompCode();
            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetPerMatIssu();
            }

            string mRef = this.txtMIsuRef.Text;
            string mBUNDNO = this.lblCurNo1.Text.Trim();
            string mISUDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
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

                string billno = tbl2.Rows[i]["billno"].ToString();
                string orderno = tbl2.Rows[i]["orderno"].ToString();



                //if (Isuqty > 0)
                //{

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEBUNDLEAB", "BUNDA", mBUNDNO, bundtype,
                        billno, orderno, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //}
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            this.txtCurISSDate.Enabled = false;

        }

        protected void gvbundle_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblbundleinfo"];
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
            ViewState.Remove("tblbundleinfo");
            ViewState["tblbundleinfo"] = dv.ToTable();
            this.Data_Bind();

        }

        protected void gvbundle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvbundle.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void chkAllfrm_OnCheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvbill.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvbill.Rows.Count; i++)
                {
                    ((CheckBox)this.gvbill.Rows[i].FindControl("chkitem")).Checked = true;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvbill.Rows.Count; i++)
                {
                    ((CheckBox)this.gvbill.Rows[i].FindControl("chkitem")).Checked = false;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "False";

                }

            }
        }


        protected void lbtnSelectedOrdr_OnClick(object sender, EventArgs e)
        {

            DataTable dt1 = (DataTable)ViewState["tblbundleinfo"];
            DataTable dtResP = (DataTable)Session["tblbillno"];

            int i;
            for (i = 0; i < this.gvbill.Rows.Count; i++)
            {
                bool chkitm = ((CheckBox)this.gvbill.Rows[i].FindControl("chkitem")).Checked;
                string bilno = ((Label)this.gvbill.Rows[i].FindControl("lblgvorBillno")).Text.Trim();
                string orderno = ((Label)this.gvbill.Rows[i].FindControl("lblorgvorderno")).Text.Trim();
                if (chkitm == true)
                {
                    DataRow[] dr2 = dtResP.Select("billno='" + bilno + "'and orderno='" + orderno + " '");
                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = 1;
                    }


                }

                else
                {

                    DataRow[] dr2 = dtResP.Select("billno='" + bilno + "'and orderno='" + orderno + " '");

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

                    //string aprovno = dtResP.Rows[i]["aprovno"].ToString();
                    dr1["billno"] = dtResP.Rows[i]["billno"];
                    dr1["billno1"] = dtResP.Rows[i]["billno1"];
                    dr1["billref"] = dtResP.Rows[i]["billref"];
                    dr1["billdate"] = dtResP.Rows[i]["billdate"];
                    dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                    dr1["orderno"] = dtResP.Rows[i]["orderno"];
                    dr1["orderno1"] = dtResP.Rows[i]["orderno1"];
                    dr1["billamt"] = dtResP.Rows[i]["billamt"];
                    dr1["rsirdesc"] = dtResP.Rows[i]["rsirdesc"];
                    dr1["ssirdesc"] = dtResP.Rows[i]["ssirdesc"];
                    dr1["pactdesc"] = dtResP.Rows[i]["pactdesc"];
                    dr1["chk"] = dtResP.Rows[i]["chk"];
                    dt1.Rows.Add(dr1);

                }
            }


            ViewState["tblbundleinfo"] = dt1;
            this.MultiView1.ActiveViewIndex = 0;
            this.pnlbill.Visible = false;
            this.Data_Bind();

            //this.gvOrderInfo_DataBind();
        }


        protected void lnkAddNew_OnClick(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblbillno"];
            DataTable dt = dt1.Copy();
            DataView dv = dt.DefaultView;
            string billno = this.ddlbillno.SelectedValue;
            dv.RowFilter = ("billno='" + billno + "'");
            dt = dv.ToTable();

            DataTable dt2 = (DataTable)ViewState["tblbundleinfo"];
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
    }
}