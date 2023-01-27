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
namespace RealERPWEB.F_12_Inv
{
    public partial class PurTopSheetCashPur : System.Web.UI.Page
    {

        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //----------------udate-20150120---------
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Top Sheet (Cash Purchase)";

                this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtCurISSDate_CalendarExtender.EndDate = System.DateTime.Today;
                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.lbtnPrevISSList_Click(null, null);
                }



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
            if (this.ddlPrevISSList.Items.Count > 0)
                mNEWBUNDLE = this.ddlPrevISSList.SelectedValue.ToString();

            //string mREQDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            //if (mREQNO == "NEWMISS")
            //{
            //    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTMISSUEINFO", mREQDAT,
            //           "", "", "", "", "", "", "", "");
            //    if (ds2 == null)
            //        return;
            //    if (ds2.Tables[0].Rows.Count > 0)
            //    {

            //        this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);

            //        this.ddlPrevISSList.DataTextField = "maxmisuno1";
            //        this.ddlPrevISSList.DataValueField = "maxmisuno";
            //        this.ddlPrevISSList.DataSource = ds2.Tables[0];
            //        this.ddlPrevISSList.DataBind();
            //    }
            //}


            if (mNEWBUNDLE == "NEWBUNDLE")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTCPAPPROVALNO", "",
                       "", "", "", "", "", "", "", "");

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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "RPTCASHPURTOPSHEET", bundno, "", "", "", "", "", "", "", "");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string supplier = "Purchaser: " + ds1.Tables[0].Rows[0]["ssirdesc"].ToString();
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_12_Inv.EClassTopSheetCashPurchase>();
            //Datt=(ds1.Tables[0].Rows[0]).se
            DataRow[] dr1 = (ds1.Tables[0]).Select("rsircode='BBBBAAAAAAAA'");

            double toamt = Convert.ToDouble(dr1[0]["reqamt"]);
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RDLCAccountSetup.GetLocalReport("R_12_Inv.rptTopSheetCashPurchase", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("txtcomadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtcomname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtbundlenumber", "SL No:" + bundno));
            Rpt1.SetParameters(new ReportParameter("txtSupdesc", supplier));
            Rpt1.SetParameters(new ReportParameter("txtbudledat", "Buddle Date:" + date));
            Rpt1.SetParameters(new ReportParameter("txttakainword", ASTUtility.Trans(toamt, 2)));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }







        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");

            string budleno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREVBUND", CurDate1, budleno, "", "", "", "", "", "", "");
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
        }



        private void Get_Issue_Info()
        {

            string comcod = this.GetCompCode();

            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            string mNEWBUNDLE = "NEWBUNDLE";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                mNEWBUNDLE = this.ddlPrevISSList.SelectedValue.ToString();
            }
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETCASHPURCHASE", mNEWBUNDLE, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblbundleinfo"] = ds1.Tables[0];
            ViewState["UserLog"] = ds1.Tables[1];


            if (mNEWBUNDLE == "NEWBUNDLE")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTCPAPPROVALNO", CurDate1,
                       "", "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["bundno"].ToString();

                }
                this.pnlbill.Visible = true;
                this.GetReqInfo();
                return;

            }

            this.pnlCashpurchase.Visible = true;
            this.LoadReqno();

            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["bundno"].ToString();
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["bunddate"]).ToString("dd-MMM-yyyy");
            this.txtMIsuRef.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.MultiView1.ActiveViewIndex = 0;
            this.Data_Bind();
        }

        private void LoadReqno()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETREQINFO", "", "", "", "", "", "", "", "", "");
            Session["tblbillno"] = ds1.Tables[0];
            this.ddlReqno.DataTextField = "reqno1";
            this.ddlReqno.DataValueField = "reqno";
            this.ddlReqno.DataSource = ds1.Tables[0];
            this.ddlReqno.DataBind();

        }
        private void GetReqInfo()
        {
            Session.Remove("tblbillno");
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETREQINFO", "", "", "", "", "", "", "", "", "");
            Session["tblbillno"] = ds1.Tables[0];
            this.gvbill.DataSource = ds1.Tables[0];
            this.gvbill.DataBind();

        }

        protected void Data_Bind()
        {
            this.gvbundle.DataSource = (DataTable)ViewState["tblbundleinfo"];
            this.gvbundle.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)ViewState["tblbundleinfo"];

            if (dt.Rows.Count > 0)
            {

                ((Label)this.gvbundle.FooterRow.FindControl("lblgvfreqamtcp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reqamt)", "")) ? 0.00 : dt.Compute("Sum(reqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvbundle.FooterRow.FindControl("lblgFCaramtcp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(carring)", "")) ? 0.00 : dt.Compute("Sum(carring)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvbundle.FooterRow.FindControl("lblgFtoamtcp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(toamt)", "")) ? 0.00 : dt.Compute("Sum(toamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }

            else
            {
                return;
            }

            //((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTReqAmt")).Text =
            //    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(preqamt)", "")) ?
            //        0.00 : tbl1.Compute("Sum(preqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }



        protected void lnkupdate_OnClick(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);

            //    return;
            //}
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            DataTable dtuser = (DataTable)ViewState["UserLog"];
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
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQCSAB", "PURREQCSB",
                             mBUNDNO, mISUDAT, mRef, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }





            foreach (DataRow dr2 in tbl2.Rows)
            {

                string reqno = dr2["reqno"].ToString();
                string[] rsircode = dr2["rsircode"].ToString().Trim().Split(',');
                string[] spcfcod = dr2["spcfcod"].ToString().Trim().Split(',');
                int i = 0;
                foreach (string rsircode1 in rsircode)
                {

                    string spcfcod1 = spcfcod[i++].ToString();
                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQCSAB", "PURREQCSA", mBUNDNO,
                                       reqno, rsircode1, spcfcod1, "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }



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
            string reqno = ((Label)this.gvbundle.Rows[e.RowIndex].FindControl("lblorgvreqnocp")).Text.Trim();
            //string orderno = ((Label)this.gvbundle.Rows[e.RowIndex].FindControl("txtorderorg")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETECASHREQ", bundno, reqno, "", "", "", "", "", "", "", "", "", "", "", "", "");

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
                string reqno = ((Label)this.gvbill.Rows[i].FindControl("lblgvorBillno")).Text.Trim();

                if (chkitm == true)
                {
                    DataRow[] dr2 = dtResP.Select("reqno='" + reqno + "'");
                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = 1;
                    }


                }

                else
                {

                    DataRow[] dr2 = dtResP.Select("reqno='" + reqno + "'");

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
                    dr1["reqno"] = dtResP.Rows[i]["reqno"];
                    dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                    dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                    dr1["reqdat"] = dtResP.Rows[i]["reqdat"];
                    dr1["rsircode"] = dtResP.Rows[i]["rsircode"];
                    dr1["spcfcod"] = dtResP.Rows[i]["spcfcod"];
                    dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                    dr1["reqamt"] = dtResP.Rows[i]["reqamt"];
                    dr1["carring"] = dtResP.Rows[i]["carring"];
                    dr1["toamt"] = dtResP.Rows[i]["toamt"];
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
            string reqno = this.ddlReqno.SelectedValue;
            dv.RowFilter = ("reqno='" + reqno + "'");
            dt = dv.ToTable();

            DataTable dt2 = (DataTable)ViewState["tblbundleinfo"];
            foreach (DataRow dr in dt.Rows)
            {
                DataRow dr1 = dt2.NewRow();
                dr1["comcod"] = dr["comcod"];
                dr1["reqno"] = dr["reqno"];
                dr1["reqno1"] = dr["reqno1"];
                dr1["mrfno"] = dr["mrfno"];
                dr1["reqdat"] = dr["reqdat"];
                dr1["rsircode"] = dr["rsircode"];
                dr1["spcfcod"] = dr["spcfcod"];
                dr1["ssircode"] = dr["ssircode"];
                dr1["reqamt"] = dr["reqamt"];
                dr1["carring"] = dr["carring"];

                dr1["toamt"] = dr["toamt"];
                dr1["rsirdesc"] = dr["rsirdesc"];
                dr1["ssirdesc"] = dr["ssirdesc"];
                dr1["pactdesc"] = dr["pactdesc"];
                dr1["chk"] = dr["chk"];
                dt2.Rows.Add(dr1);

            }
            //Session["tblbillno"] = dt;
            this.Data_Bind();

        }
    }
}