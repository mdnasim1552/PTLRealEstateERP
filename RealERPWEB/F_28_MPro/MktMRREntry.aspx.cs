using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Microsoft.Reporting.WinForms;
using RealERPLIB;

namespace RealERPWEB.F_28_MPro
{
    public partial class MktMRREntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtCurMRRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtChaDate.Text = DateTime.Today.ToString("dd.MM.yyyy");

                string qgenno = this.Request.QueryString["genno"] ?? "";
                if (qgenno.Length > 0)
                {

                    if (qgenno.Substring(0, 3) == "MRR")
                    {
                        this.ImgbtnPreMRR_Click(null, null);

                    }

                }

                this.DupMRR();
                this.ImgbtnFindProject_Click(null, null);
                //this.ImgbtnFindSup_Click(null, null);
                this.txtCurMRRDate_CalendarExtender.EndDate = System.DateTime.Today;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void DupMRR()
        {
            this.chkdupMRR.Visible = false;
            this.chkdupMRR.Checked = false;
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Load_Project_Combo()
        {
            this.ddlSupList.Items.Clear();
            this.ddlOrderList.Items.Clear();
            string comcod = this.GetCompCode();
            string FindProject = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? this.txtProjectSearch.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_MRR_PROJECT_LIST", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            this.ddlProject_SelectedIndexChanged(null, null);

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string mMRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            string currentptah = "PurchasePrint.aspx?Type=MktMRRPrint&mrno=" + mMRRNO;
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

        }

        private string CompanyLength()
        {
            string comcod = this.GetCompCode();
            string length = "";
            switch (comcod)
            {
                case "3101":
                case "3340":
                    length = "length";
                    break;


                default:
                    length = "";
                    break;
            }

            return length;

        }
        protected void ImgbtnPreMRR_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string length = this.CompanyLength();
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string SearchMrr = (qgenno.Length == 0 ? "%" : this.Request.QueryString["genno"].ToString()) + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_PREV_MRR_LIST", CurDate1, SearchMrr, length, usrid, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevMRRList.Items.Clear();
            this.ddlPrevMRRList.DataTextField = "mrrno1";
            this.ddlPrevMRRList.DataValueField = "mrrno";
            this.ddlPrevMRRList.DataSource = ds1.Tables[0];
            this.ddlPrevMRRList.DataBind();

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.ImgbtnPreMRR.Visible = true;
                this.ddlPrevMRRList.Visible = true;
                this.ddlPrevMRRList.Items.Clear();
                this.ddlProject.Visible = true;
                this.ddlSupList.Enabled = true;
                this.ImgbtnFindSup.Enabled = true;
                this.lblCurMRRNo1.Text = "MRR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMRRDate.Enabled = true;
                this.txtMRRRef.Text = "";
                this.ddlOrderList.Enabled = true;
                this.txtMRRNarr.Text = "";
                this.gvMRRInfo.DataSource = null;
                this.gvMRRInfo.DataBind();
                this.lbtnOk.Text = "Ok";
                this.txtChalanNo.Text = "";
                this.pnlResDetails.Visible = false;
                this.PnlNarration.Visible = false;
                this.FindOrderList();
                return;
            }

            this.ImgbtnPreMRR.Visible = false;
            this.ddlPrevMRRList.Visible = false;
            this.ddlProject.Visible = true;
            this.ddlProject.Enabled = false;
            this.ddlSupList.Enabled = false;
            this.ImgbtnFindSup.Enabled = false;
            this.txtCurMRRNo2.ReadOnly = true;
            this.ddlOrderList.Enabled = false;
            this.pnlResDetails.Visible = true;
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";

            this.Get_Receive_Info();
            this.ImgbtnFindRes_Click(null, null);



        }



        protected void Session_tblMRR_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvMRRInfo.Rows.Count; j++)
            {
                double dgvOrderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvOrderQty")).Text.Trim()));
                double dgvMRRQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvMRRQty")).Text.Trim()));
                double dgvMRRRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvMRRRate")).Text.Trim()));
                double dgvChlnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvChlnqty")).Text.Trim()));
                string dgvMRRNote = ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvMRRNote")).Text.Trim();
                double dgvMRRAmt = dgvMRRQty * dgvMRRRate;
                double Balqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvOrderBal")).Text.Trim()));

                // double dgvOrderBal = dgvOrderQty - dgvMRRQty;
                if (Balqty >= dgvMRRQty)
                {
                    ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvMRRQty")).Text = dgvMRRQty.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvMRRRate")).Text = dgvMRRRate.ToString("#,##0.00;(#,##0.00); ");
                    ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvChlnqty")).Text = dgvChlnQty.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvMRRAmt")).Text = dgvMRRAmt.ToString("#,##0.00;(#,##0.00); ");
                    TblRowIndex2 = (this.gvMRRInfo.PageIndex) * this.gvMRRInfo.PageSize + j;
                    tbl1.Rows[TblRowIndex2]["mrrqty"] = dgvMRRQty;
                    tbl1.Rows[TblRowIndex2]["mrrrate"] = dgvMRRRate;
                    tbl1.Rows[TblRowIndex2]["mrramt"] = dgvMRRAmt;
                    tbl1.Rows[TblRowIndex2]["mrrnote"] = dgvMRRNote;
                    tbl1.Rows[TblRowIndex2]["chlnqty"] = dgvChlnQty;

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "MRR Qty  must be less then equal Balance Qty" + "');", true);
                    return;
                }

            }

            ViewState["tblMRR"] = tbl1;
        }

        protected void gvMRRInfo_DataBind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            this.gvMRRInfo.DataSource = tbl1;
            this.gvMRRInfo.DataBind();

            string comcod = GetCompCode();        
            if (this.Request.QueryString["Type"].ToString() == "Entry")
            {

                ((LinkButton)this.gvMRRInfo.FooterRow.FindControl("lbtnDelMRR")).Visible = false;
                if (this.ddlPrevMRRList.Items.Count > 0)
                {

                    ((LinkButton)this.gvMRRInfo.FooterRow.FindControl("lbtnUpdateMRR")).Visible = false;
                    ((LinkButton)this.gvMRRInfo.FooterRow.FindControl("lbtnResFooterTotal")).Visible = false;
                }

            }
            else
            {

                this.gvMRRInfo.AutoGenerateDeleteButton = false;

            }

          
            this.lbtnResFooterTotal_Click(null, null);
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string reqno = dt1.Rows[0]["reqno"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    reqno = dt1.Rows[j]["reqno"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["reqno1"] = "";
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
                        dt1.Rows[j]["reqno1"] = "";

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        dt1.Rows[j]["rsirdesc1"] = "";
                    reqno = dt1.Rows[j]["reqno"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                }

            }

            return dt1;
        }

        protected void GetReceiveNo()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mMRRNo = "NEWMRR";
            if (this.ddlPrevMRRList.Items.Count > 0)
                mMRRNo = this.ddlPrevMRRList.SelectedValue.ToString();

            if (mMRRNo == "NEWMRR")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_LAST_MRR_INFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMRRNo1.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(0, 6);
                    this.txtCurMRRNo2.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(6, 5);
                    this.ddlPrevMRRList.DataTextField = "maxmrrno1";
                    this.ddlPrevMRRList.DataValueField = "maxmrrno";
                    this.ddlPrevMRRList.DataSource = ds1.Tables[0];
                    this.ddlPrevMRRList.DataBind();
                }

            }


        }

        protected void Get_Receive_Info()
        {
            ViewState.Remove("tblMRR");
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mMRRNo = "NEWMRR";
            if (this.ddlPrevMRRList.Items.Count > 0)
            {
                this.txtCurMRRDate.Enabled = false;
                mMRRNo = this.ddlPrevMRRList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_PUR_MRR_INFO", mMRRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            ViewState["tblMRR"] = ds1.Tables[0];
            Session["UserLog"] = ds1.Tables[1];

            if (mMRRNo == "NEWMRR")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_LAST_MRR_INFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMRRNo1.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(0, 6);
                    this.txtCurMRRNo2.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(6, 5);
                }

                return;

            }

            //this.Load_Project_Combo();
            if (ds1.Tables[1].Rows.Count > 0)
            {
                //Project
                this.ddlProject.DataTextField = "pactdesc";
                this.ddlProject.DataValueField = "pactcode";
                this.ddlProject.DataSource = ds1.Tables[1];
                this.ddlProject.DataBind();

                // Supplier
                this.ddlSupList.DataTextField = "ssirdesc1";
                this.ddlSupList.DataValueField = "ssircode";
                this.ddlSupList.DataSource = ds1.Tables[1];
                this.ddlSupList.DataBind();

                //Order
                this.ddlOrderList.DataTextField = "orderno1";
                this.ddlOrderList.DataValueField = "orderno";
                this.ddlOrderList.DataSource = ds1.Tables[1];
                this.ddlOrderList.DataBind();
            }

            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlSupList.SelectedValue = ds1.Tables[1].Rows[0]["ssircode"].ToString();
            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["orderno"].ToString();
            this.txtMRRRef.Text = ds1.Tables[1].Rows[0]["mrrref"].ToString();
            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["orderno"].ToString();
            this.lblCurMRRNo1.Text = ds1.Tables[1].Rows[0]["mrrno1"].ToString().Substring(0, 6);
            this.txtCurMRRNo2.Text = ds1.Tables[1].Rows[0]["mrrno1"].ToString().Substring(6, 5);
            this.txtCurMRRDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdat"]).ToString("dd.MM.yyyy");
            this.txtMRRNarr.Text = ds1.Tables[1].Rows[0]["mrrnar"].ToString();
            this.txtChalanNo.Text = ds1.Tables[1].Rows[0]["chlnno"].ToString();
            this.txtQc.Text = ds1.Tables[1].Rows[0]["qcno"].ToString();
            this.txtChaDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["challandat"]).ToString("dd.MM.yyyy");
            this.gvMRRInfo_DataBind();
        }

        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            string prType = this.ddlPRType.SelectedValue.ToString();
            string actType = this.ddlActType.SelectedValue.ToString();

            DataTable tbl2 = (DataTable)ViewState["tblMat"];
            DataView dv = tbl2.DefaultView;
            dv.RowFilter = ("orderno='" + orderno + "' and acttype = '" + actType + "'");
            tbl2 = dv.ToTable();

            DataRow[] dr = tbl1.Select("orderno='" + orderno + "' and acttype = '" + actType + "'");
            if (dr.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {

                    DataRow dr1 = tbl1.NewRow();
                    dr1["orderno"] = tbl2.Rows[i]["orderno"].ToString();
                    dr1["reqno"] = tbl2.Rows[i]["reqno"].ToString();
                    dr1["reqno1"] = tbl2.Rows[i]["reqno1"].ToString();
                    dr1["prtype"] = tbl2.Rows[i]["prtype"].ToString();
                    dr1["acttype"] = tbl2.Rows[i]["acttype"].ToString();
                    dr1["mkttype"] = tbl2.Rows[i]["mkttype"].ToString();
                    dr1["rsirdetdesc"] = tbl2.Rows[i]["rsirdetdesc"].ToString();
                    dr1["prtypedesc"] = this.ddlPRType.SelectedItem.Text.Trim();
                    dr1["acttypedesc"] = this.ddlActType.SelectedItem.Text.Trim();
                    dr1["orderdat"] = tbl2.Rows[i]["orderdat"].ToString();
                    dr1["orderqty"] = tbl2.Rows[i]["orderqty"].ToString();
                    dr1["recup"] = Convert.ToDouble(tbl2.Rows[i]["recup"]).ToString();
                    dr1["orderbal"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    dr1["mrrqty"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    dr1["mrrrate"] = Convert.ToDouble(tbl2.Rows[i]["mrrrate"]).ToString();
                    dr1["mrramt"] = 0;
                    dr1["mrrnote"] = "";
                    dr1["chlnqty"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    tbl1.Rows.Add(dr1);
                }

                ViewState["tblMRR"] = tbl1;
            }

            int RowNo = 1;          
            double PageNo = Math.Ceiling(RowNo * 1.00 / this.gvMRRInfo.PageSize);
            this.gvMRRInfo.PageIndex = Convert.ToInt32(PageNo - 1);
            this.gvMRRInfo_DataBind();

        }

        protected void lbtnSelectResAll_Click(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            string orderno = this.ddlOrderList.SelectedValue.ToString();
           
            string prType = this.ddlPRType.SelectedValue.ToString();
            string actType = this.ddlActType.SelectedValue.ToString();

            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            DataTable tbl2 = (DataTable)ViewState["tblMat"];
            DataRow[] dr = tbl1.Select("orderno='" + orderno + "' and acttype = '" + actType + "'");
            if (dr.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {

                    DataRow dr1 = tbl1.NewRow();
                    dr1["orderno"] = tbl2.Rows[i]["orderno"].ToString();
                    dr1["reqno"] = tbl2.Rows[i]["reqno"].ToString();
                    dr1["reqno1"] = tbl2.Rows[i]["reqno1"].ToString();
                    dr1["prtype"] = tbl2.Rows[i]["prtype"].ToString();
                    dr1["acttype"] = tbl2.Rows[i]["acttype"].ToString();
                    dr1["rsirdetdesc"] = tbl2.Rows[i]["rsirdetdesc"].ToString();
                    dr1["prtypedesc"] = tbl2.Rows[i]["prtypedesc"].ToString();
                    dr1["acttypedesc"] = tbl2.Rows[i]["acttypedesc"].ToString();
                    dr1["orderdat"] = tbl2.Rows[i]["orderdat"].ToString();
                    dr1["orderqty"] = tbl2.Rows[i]["orderqty"].ToString();
                    dr1["recup"] = Convert.ToDouble(tbl2.Rows[i]["recup"]).ToString();
                    dr1["orderbal"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    dr1["mrrqty"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    dr1["mrrrate"] = Convert.ToDouble(tbl2.Rows[i]["mrrrate"]).ToString();
                    dr1["mrramt"] = 0;
                    dr1["mrrnote"] = "";
                    dr1["chlnqty"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    tbl1.Rows.Add(dr1);
                }
                ViewState["tblMRR"] = tbl1;
            }
            this.gvMRRInfo_DataBind();

        }

        protected void lbtnUpdateMRR_Click(object sender, EventArgs e)
        {

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            string PostedByid = (this.Request.QueryString["type"] == "Entry") ? ((tblPostedByid == "") ? userid : tblPostedByid) : ((tblPostedByid == "") ? userid : tblPostedByid);
            string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid) : ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid);
            string PostSession = (this.Request.QueryString["type"] == "Entry") ? ((tblPostedSession == "") ? Sessionid : tblPostedSession) : ((tblPostedSession == "") ? Sessionid : tblPostedSession);
            string Posteddat = (this.Request.QueryString["type"] == "Entry") ? ((tblPosteddat == "") ? Date : tblPosteddat) : ((tblPosteddat == "") ? Date : tblPosteddat);

            string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : userid;
            string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");


            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            DataRow[] dr = tbl1.Select("mrrqty>0");
            if (dr.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Input Receive Qty" + "');", true);
                return;
            }


            string mMRRDAT = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
            string mSSIRCODE = this.ddlSupList.SelectedValue.ToString().Trim();
            string mORDERNO = this.ddlOrderList.SelectedValue.ToString().Trim();
            string mMRRNAR = this.txtMRRNarr.Text.Trim();
            string mMRRChlnNo = this.txtChalanNo.Text.Trim();
            //string mrrno = this.txtMRRRef.Text.Trim();
            string mchlndate = this.GetStdDate(this.txtChaDate.Text.Trim()); ;
            string mQcno = this.txtQc.Text.Trim();

            //MRR No
            if (this.txtMRRRef.Text.Trim().Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "MRR Ref. No Required" + "');", true);
                return;
            }

            //Chalan No
            else if (mMRRChlnNo.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Chalan No Required" + "');", true);
                return;
            }
           
            ////For Balace Orderqty Qty

            if (this.Request.QueryString["type"].ToString().Trim() == "Entry")
            {
                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mreqno = tbl1.Rows[i]["reqno"].ToString();
                    string prtype = tbl1.Rows[i]["prtype"].ToString();
                    string acttype = tbl1.Rows[i]["acttype"].ToString();
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "BAL_ORDER_QTY", mORDERNO, mreqno, prtype, acttype, "", "", "", "", "");
                    if (ds.Tables[0].Rows.Count == 0) continue;
                    else if (Convert.ToDouble(ds.Tables[0].Rows[0]["balqty"]) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "There is no balance qty in Order" + "');", true);
                        return;
                    }
                }

                //MRRREF
                // this.GetMRRefno();

            }

            //Forward Order Date
            foreach (DataRow drf in tbl1.Rows)
            {
                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(drf["orderdat"].ToString()), Convert.ToDateTime(mMRRDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "MRR Date is equal or greater Order Date" + "');", true);
                    return;
                }
            }


            this.lbtnResFooterTotal_Click(null, null);
            string mMRRREF = this.txtMRRRef.Text.Trim();
            if (this.ddlPrevMRRList.Items.Count == 0)
                this.GetReceiveNo();

            string mMRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();

            if (this.chkdupMRR.Checked)
            {

                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "CHECKED_DUP_MRRNO", mMRRREF, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                    ;


                else
                {

                    DataView dv1 = ds2.Tables[0].DefaultView;
                    dv1.RowFilter = ("mrrno <>'" + mMRRNO + "'");
                    DataTable dt = dv1.ToTable();
                    if (dt.Rows.Count == 0)
                        ;
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Found Duplicate MRR No" + "');", true);
                        this.ddlPrevMRRList.Items.Clear();
                        return;
                    }
                }
            }

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "UPDATE_MKT_MRR_INFO", "MKTMRRB",
                             mMRRNO, mMRRDAT, mPACTCODE, mSSIRCODE, mORDERNO, mMRRREF, mMRRNAR, mMRRChlnNo, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, mQcno, mchlndate, "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["orderdat"].ToString()), Convert.ToDateTime(mMRRDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "MRR Date is equal or greater Order Date" + "');", true);
                    return;
                }



                string orderno = tbl1.Rows[i]["orderno"].ToString();
                string mreqno = tbl1.Rows[i]["reqno"].ToString();
              
                double orbal = Convert.ToDouble(tbl1.Rows[i]["orderbal"].ToString());
                double mMRRQTY = Convert.ToDouble(tbl1.Rows[i]["mrrqty"].ToString());
                string mMRRAMT = tbl1.Rows[i]["mrramt"].ToString();
                string prtype = tbl1.Rows[i]["prtype"].ToString();
                string acttype = tbl1.Rows[i]["acttype"].ToString();
                string mkttype = tbl1.Rows[i]["mkttype"].ToString();
                string mMRRNOTE = tbl1.Rows[i]["mrrnote"].ToString();
                //string mMRRchlnqty = tbl1.Rows[i]["chlnqty"].ToString();
                if (orbal >= mMRRQTY)
                {
                    if (mMRRQTY > 0)
                        result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "UPDATE_MKT_MRR_INFO", "MKTMRRA",
                                 mMRRNO, "", "", mMRRQTY.ToString(), mMRRAMT, mreqno, prtype, acttype, orderno, mkttype, mMRRNOTE, "", "", "", "", "", "","","","");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }

                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "MRR Qty  must be less then or equal Balance Qty" + "');", true);
                    return;
                }
            }
            this.txtCurMRRDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Marketing Material Received Successfully" + "');", true);

            if (hst["compsms"].ToString() == "True")
            {
                switch (comcod)
                {
                    case "3333":
                        break;

                    default:
                        if (this.Request.QueryString["type"].ToString().Trim() == "Entry")
                        {
                            SendSmsProcess sms = new SendSmsProcess();
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string frmname = "'PurBillEntry.aspx?Type=BillEntry";

                            string SMSHead = "Ready To Bill Conformation, ";
                            string SMSText = comnam + ":\n" + SMSHead + "\n" + "MRR No: " + mMRRNO;
                            bool resultsms = sms.SendSmms(SMSText, userid, frmname);

                        }
                        break;
                }



            }

        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            ((Label)this.gvMRRInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(mrramt)", "")) ?
                    0.00 : tbl1.Compute("Sum(mrramt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void ImgbtnFindSup_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {

                string comcod = this.GetCompCode();
                string FindSupplier = (this.Request.QueryString["sircode"].ToString()).Length == 0 ? this.txtSupSearch.Text.Trim() + "%" : this.Request.QueryString["sircode"].ToString() + "%";
                string mProjCode = this.ddlProject.SelectedValue.ToString();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_MRR_SUP_LIST", FindSupplier, mProjCode, "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                this.ddlSupList.DataTextField = "ssirdesc1";
                this.ddlSupList.DataValueField = "ssircode";
                this.ddlSupList.DataSource = ds1.Tables[0];
                this.ddlSupList.DataBind();
                this.FindOrderList();
            }
        }


        private void FindOrderList()
        {

            string comcod = this.GetCompCode();
            string mProjCode = this.ddlProject.SelectedValue.ToString();
            string mSupCode = this.ddlSupList.SelectedValue.ToString();
            string Date = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string orderno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + "%" : this.Request.QueryString["genno"].ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_MRR_ORDER_LIST", orderno, mProjCode, mSupCode, Date, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "orderno1";
            this.ddlOrderList.DataValueField = "orderno";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();

        }
        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblMat");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mProject = this.ddlProject.SelectedValue.ToString();
            string mSupCode = this.ddlSupList.SelectedValue.ToString();
            string mOrderNo = this.ddlOrderList.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_MRR_RES_LIST", mProject, mSupCode, mOrderNo, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPRType.DataTextField = "prtypedesc";
            this.ddlPRType.DataValueField = "prtype";
            this.ddlPRType.DataSource = ds1.Tables[0];
            this.ddlPRType.DataBind();

            ViewState["tblMat"] = ds1.Tables[0];
            this.ddlPRType_SelectedIndexChanged1(null, null);

        }

        protected void ddlPRType_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblMat"];
            string code = this.ddlPRType.SelectedValue;
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "ccode like ('" + code + "')";
            this.ddlActType.DataTextField = "acttypedesc";
            this.ddlActType.DataValueField = "acttype";
            this.ddlActType.DataSource = dv1.ToTable();
            this.ddlActType.DataBind();
        }       

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSupList.Items.Clear();
            this.ddlOrderList.Items.Clear();
            if (this.Request.QueryString["prjcode"].ToString().Length != 0)
            {
                this.ImgbtnFindSup_Click(null, null);
            }

        }
        protected void ddlSupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FindOrderList();
            //this.ddlOrderList.Items.Clear();
        }

        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.Load_Project_Combo();
        }

        protected void lbtnDelMRR_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)ViewState["tblMRR"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string date = System.DateTime.Today.ToString();
            string mMRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "DELETE_MRR", mMRRNO, userid, Terminal, Sessionid, date, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Material Deleted Failed!" + "');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Material Deleted successfully" + "');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Receive Information";
                string eventdesc = "Delete Materials Received Qty";
                string eventdesc2 = mMRRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvMRRInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbtngvDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblMRR"];
            int gvRowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowIndex = (this.gvMRRInfo.PageSize * this.gvMRRInfo.PageIndex) + gvRowIndex;
            string MRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            string reqno = ((Label)this.gvMRRInfo.Rows[rowIndex].FindControl("lblgvReqnomain")).Text.Trim();
            //string rescode = ((Label)this.gvMRRInfo.Rows[rowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "DELETE_MRR_MAT", MRRNO, reqno, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Received Material Deleted Failed!" + "');", true);
                return;
            }

            dt.Rows[rowIndex].Delete();
            DataView dv = dt.DefaultView;
            ViewState["tblMRR"] = dv.ToTable();
            this.gvMRRInfo_DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Received Material Deleted successfully" + "');", true);
        }
       
    }
}