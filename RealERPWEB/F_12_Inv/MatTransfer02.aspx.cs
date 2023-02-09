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
namespace RealERPWEB.F_12_Inv
{
    public partial class MatTransfer02 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Materials Transfer Information Input/Edit Screen";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtCurTransDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");



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
        protected void imgbtnReq_Click(object sender, EventArgs e)
        {
            this.GetRequisition();
        }

        private void GetRequisition()
        {
            ViewState.Remove("tblreqlist");
            string comcod = this.GetCompCode();
            string ReqSearch = "%" + this.txtReqSearch.Text + "%";
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETREQNO", ReqSearch, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlReqList.Items.Clear();
                return;

            }
            ViewState["tblreqlist"] = ds1.Tables[0];
            this.ddlReqList.DataTextField = "reqno1";
            this.ddlReqList.DataValueField = "reqno";
            this.ddlReqList.DataSource = ds1.Tables[1];
            this.ddlReqList.DataBind();
            ds1.Dispose();



        }


        protected void GetMatTrns()
        {

            string comcod = GetCompCode();
            string mTRNNO = "NEWTRNS";
            if (this.ddlPreList.Items.Count > 0)
                mTRNNO = this.ddlPreList.SelectedValue.ToString();

            string mREQDAT = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString();


            if (mTRNNO == "NEWTRNS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LastTransferNo", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 6);
                    this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                    this.ddlPreList.DataTextField = "maxtrnno1";
                    this.ddlPreList.DataValueField = "maxtrnno";
                    this.ddlPreList.DataSource = ds2.Tables[0];
                    this.ddlPreList.DataBind();
                }
            }

        }

        private void PreTrnsList()
        {


            string comcod = this.GetCompCode();
            string curdate = this.txtCurTransDate.Text.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetPrevTrnsList", curdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPreList.DataTextField = "trnno1";
            this.ddlPreList.DataValueField = "trnno";
            this.ddlPreList.DataSource = ds1.Tables[0];
            this.ddlPreList.DataBind();


        }
        protected void ImgbtnFindPrevious_Click(object sender, EventArgs e)
        {
            this.PreTrnsList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.PanelSub.Visible = true;
                this.lblPreViousList.Visible = false;
                this.txtSrchPrevious.Visible = false;
                this.ImgbtnFindPrevious.Visible = false;
                this.ddlPreList.Visible = false;
                this.GetMatTransfer();

                return;
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lbtnOk.Text = "Ok";
            this.txtCurTransDate.Enabled = true;
            // this.PanelSub.Visible = false;
            this.lblPreViousList.Visible = true;
            this.txtSrchPrevious.Visible = true;
            this.ImgbtnFindPrevious.Visible = true;
            this.ddlPreList.Visible = true;
            this.ddlPreList.Items.Clear();
            this.ddlReqList.Items.Clear();
            this.grvacc.DataSource = null;
            this.grvacc.DataBind();
        }


        private void GetMatTransfer()
        {


            ViewState.Remove("tblmattrns");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            string mTRNNo = "NEWTRNS";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtCurTransDate.Enabled = false;
                mTRNNo = this.ddlPreList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "PrevTransferInfo", mTRNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];


            if (mTRNNo == "NEWTRNS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LastTransferNo", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurTransNo1.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(0, 6);
                this.txtCurTransNo2.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(6);
                return;
            }




            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["date"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurTransNo1.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(6);
            this.Data_Bind();
        }


        private void Data_Bind()
        {

            this.grvacc.DataSource = (DataTable)ViewState["tblmattrns"];
            this.grvacc.DataBind();
            this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblmattrns"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); ;




        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tblmattrns"];

            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((Label)this.grvacc.Rows[i].FindControl("lblgvqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                int rowindex = ((this.grvacc.PageIndex) * (this.grvacc.PageSize)) + i;
                dt1.Rows[rowindex]["qty"] = qty;
                dt1.Rows[rowindex]["rate"] = rate;
                dt1.Rows[rowindex]["amt"] = qty * rate;
            }
            ViewState["tblmattrns"] = dt1;
        }
        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }



        protected void lnkupdate_Click(object sender, EventArgs e)
        {


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //DataTable dtuser = (DataTable)Session["UserLog"];
            //string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            //string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            //string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            //string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            //string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            //string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            //string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            //string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : userid;
            //string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");





            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmattrns"]; ;
            string curdate = this.txtCurTransDate.Text.ToString().Trim();
            if (this.ddlPreList.Items.Count == 0)
                this.GetMatTrns();
            string transno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
            string Refno = this.txtrefno.Text.ToString();




            if (Refno.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Ref. No. Should Not Be Empty";
                return;
            }

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "CHECKEDDUPREFNO", Refno, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
                ;


            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("trnno <>'" + transno + "'");
                DataTable dt1 = dv1.ToTable();
                if (dt1.Rows.Count == 0)
                    ;
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Ref. No.";
                    //this.ddlPrevReqList.Items.Clear();
                    return;
                }
            }



            foreach (DataRow dr in dt.Rows)
            {
                string fromprj = dr["tfpactcode"].ToString().Trim();
                string toprj = dr["ttpactcode"].ToString().Trim();
                string trsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string tqty = dr["qty"].ToString().Trim();
                string trate = dr["rate"].ToString().Trim();
                string tamt = dr["amt"].ToString().Trim();
                string reqno = dr["reqno"].ToString().Trim();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UpdateTransferInf", transno, fromprj, toprj, trsircode,
                    spcfcod, tqty, trate, tamt, curdate, Refno, PostedByid, Posttrmid, PostSession, Posteddat, reqno);
            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.txtCurTransDate.Enabled = false;


        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {


            DataTable dt = ((DataTable)ViewState["tblmattrns"]).Copy();
            string reqno = this.ddlReqList.SelectedValue.ToString();
            DataTable dt1 = (DataTable)ViewState["tblreqlist"];


            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = ("reqno like '" + reqno + "'");
            dt1 = dv1.ToTable();
            DataRow[] dr1 = dt.Select("reqno = '" + reqno + "'");
            if (dr1.Length == 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {

                    DataRow dr2 = dt.NewRow();
                    dr2["reqno"] = dr["reqno"].ToString();
                    dr2["tfpactcode"] = dr["storecode"].ToString();
                    dr2["ttpactcode"] = dr["pactcode"].ToString();
                    dr2["rsircode"] = dr["rsircode"].ToString();
                    dr2["spcfcod"] = dr["spcfcod"].ToString();
                    dr2["sirunit"] = dr["rsirunit"].ToString();

                    dr2["tfdesc"] = dr["storedesc"].ToString();
                    dr2["ttdesc"] = dr["pactdesc"].ToString();
                    dr2["resdesc"] = dr["rsirdesc"].ToString();
                    dr2["spcfdesc"] = dr["spcfdesc"].ToString();
                    dr2["qty"] = dr["areqty"].ToString();
                    dr2["rate"] = dr["reqrat"].ToString();
                    dr2["amt"] = dr["reqamt"].ToString();
                    dt.Rows.Add(dr2);


                }
            }



            ViewState["tblmattrns"] = dt;
            this.Data_Bind();

        }
    }
}