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
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_28_MPro
{
    public partial class MktMatIssue : System.Web.UI.Page
    {
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Marketing Materials Issue Information";
                this.GetProjectList();
                string qgenno = this.Request.QueryString["genno"] ?? "";
                if (qgenno.Length > 0)
                {
                    this.lbtnPrevISSList_Click(null, null);

                }
                this.txtCurISSDate_CalendarExtender.EndDate = System.DateTime.Today;

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lbtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.GetProjectList();
            }
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
            string mREQNO = "NEWMISS";
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();

            string mREQDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            if (mREQNO == "NEWMISS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_LAST_MISSUE_INFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);

                    this.ddlPrevISSList.DataTextField = "maxmisuno1";
                    this.ddlPrevISSList.DataValueField = "maxmisuno";
                    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                    this.ddlPrevISSList.DataBind();

                }

            }
        }
        private void GetProjectList()
        {

            string comcod = this.GetCompCode();
            this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string srchproject = "%" + this.txtsrchproject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_ISSUE_PRJ_LIST", srchproject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "actdesc1";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ds1.Dispose();


        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintGeneral();
        }

        private void PrintGeneral()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)ViewState["tblmatissue"];
            var list = dt.DataTableToList<RealEntity.C_28_Mpro.EClassMktProcurement.RptMktMatIssue>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_28_MPro.RptMktMatIssue", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlprjlist.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("rpttxtissueno", "Issue No : " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rpttxtdate", "Date : " + this.txtCurISSDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtISSNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Marketing Matrial Issue"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string genno = (qgenno.Length == 0 ? "%" : this.Request.QueryString["genno"].ToString()) + "%";
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_PREV_MISSUE_LIST", CurDate1, genno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "isuno1";
            this.ddlPrevISSList.DataValueField = "isuno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lbtnOk.Text = "Ok";
                this.lbtnPrevISSList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.ddlPrevISSList.Items.Clear();
                this.ddlprjlist.Enabled = true;
                this.txtCurISSDate.Enabled = true;
                this.lblCurISSNo1.Text = "ISU" + DateTime.Today.ToString("MM") + "-";
                this.txtCurISSNo2.Text = "";
                this.ddlPrType.Items.Clear();
                this.txtISSNarr.Text = "";
                this.PnlRes.Visible = false;
                this.PnlNarration.Visible = false;
                this.txtMIsuRef.Text = "";
                this.txtsmcr.Text = "";
                this.grvissue.DataSource = null;
                this.grvissue.DataBind();
                return;
            }
            this.lbtnPrevISSList.Visible = false;
            this.ddlPrevISSList.Visible = false;
            this.ddlprjlist.Enabled=false;
            this.PnlRes.Visible = true;
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Issue_Info();
            this.ibtnSearchMaterisl_Click(null, null);

        }


        private void Get_Issue_Info()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mISSNo = "NEWMISS";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                mISSNo = this.ddlPrevISSList.SelectedValue.ToString();
            }
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_MKT_MISSUE_INFO", mISSNo, CurDate1,
                         pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblmatissue"] = ds1.Tables[0];
            ViewState["UserLog"] = ds1.Tables[1];


            if (mISSNo == "NEWMISS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_LAST_MISSUE_INFO", CurDate1,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);

                }
                return;
            }



            this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["isuno1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["isuno1"].ToString().Substring(6, 5);
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.txtISSNarr.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
            this.txtMIsuRef.Text = ds1.Tables[1].Rows[0]["isurefno"].ToString();
            this.txtsmcr.Text = ds1.Tables[1].Rows[0]["smcrno"].ToString();
            this.grvissue_DataBind();
        }

        private string CompReceived()
        {

            string comcod = this.GetCompCode();
            string CallType = "";
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                    CallType = "GETMETERIALSMRR";
                    break;

                default:
                    CallType = "GETMETERIALS";
                    break;


            }

            return CallType;

        }

        private void GetMaterials()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = this.txtSearchMaterials.Text.Trim() + "%";
            string balcon = "GETMETERIALS";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_METERIALS", pactcode, date, SearchMat, balcon, "", "", "", "", "");
            Session["itemlist"] = ds1.Tables[0];
            Session["acttypelist"] = ds1.Tables[2];
            if (ds1 == null)
                return;

            this.ddlPrType.DataTextField = "prtypedesc";
            this.ddlPrType.DataValueField = "prtype";
            this.ddlPrType.DataSource = ds1.Tables[1];
            this.ddlPrType.DataBind();
            ds1.Dispose();
            this.ddlPrType_SelectedIndexChanged(null, null);

        }


        protected void grvissue_DataBind()
        {
            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.grvissue.DataSource = (DataTable)ViewState["tblmatissue"]; ;
            this.grvissue.DataBind();
        }

        protected void ddlPrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetActivity();
        }

        private void GetActivity()
        {
            string prtype = this.ddlPrType.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["acttypelist"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "macttype = '" + prtype + "'";
            this.ddlActType.DataTextField = "acttypedesc";
            this.ddlActType.DataValueField = "acttype";
            this.ddlActType.DataSource = dv1.ToTable();
            this.ddlActType.DataBind();

        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string prtype = this.ddlPrType.SelectedValue.ToString().Trim();
            string acttype = this.ddlActType.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            DataRow[] dr = dt.Select("prtype='" + prtype + "' and acttype='" + acttype + "'");

            DataTable dt1 = (DataTable)Session["itemlist"];

            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["prtype"] = this.ddlPrType.SelectedValue.ToString();
                dr1["prtypedesc"] = this.ddlPrType.SelectedItem.Text.Trim();
                dr1["acttype"] = this.ddlActType.SelectedValue.ToString();
                dr1["acttypedesc"] = this.ddlActType.SelectedItem.Text.Trim();
                dr1["balqty"] = ((dt1.Select("prtype='" + prtype + "' and acttype='" + acttype + "'")).Length == 0) ? "0.00" : 
                                Convert.ToDouble((dt1.Select("prtype='" + prtype + "' and acttype='" + acttype + "'"))[0]["bbgdqty"]).ToString();
                dr1["isuqty"] = 0.00;
                dr1["remarks"] = "";
                dt.Rows.Add(dr1);

            }
            ViewState["tblmatissue"] = dt;
            this.grvissue_DataBind();


        }
        protected void lbtnSelectReaSpesAll_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblmatissue"];
            DataTable dt1 = (DataTable)Session["itemlist"];
            DataTable dt2 = ((DataTable)Session["acttypelist"]).Copy();

            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                string acttype = dt2.Rows[j]["acttype"].ToString();
                DataRow[] dr2 = dt.Select("acttype = '" + acttype + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["prtype"] = dt1.Select("acttype='"+acttype+"'")[0]["prtype"].ToString();
                    dr1["prtypedesc"] = dt1.Select("acttype='"+acttype+"'")[0]["prtypedesc"].ToString();
                    dr1["acttype"] = acttype;
                    dr1["acttypedesc"] = dt2.Rows[j]["acttypedesc"].ToString();
                    dr1["balqty"] = ((dt1.Select("acttype='" + acttype + "'")).Length == 0) ? "0.00" :
                                    Convert.ToDouble((dt1.Select("acttype='" + acttype + "'"))[0]["bbgdqty"]).ToString();
                    dr1["isuqty"] = 0.00;                  
                    dr1["remarks"] = "";

                    dt.Rows.Add(dr1);

                }
            }

            ViewState["tblmatissue"] = dt;
            this.grvissue_DataBind();
        }
        protected void lnkupdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
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

            this.SaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblmatissue"];


            string comcod = this.GetCompCode();


            string mRef = this.txtMIsuRef.Text;
            string mSmcr = this.txtsmcr.Text;

            string dmirfno = this.txtsmcr.Text;


            if (this.Request.QueryString["type"] == "Entry")
            {

                dr1 = tbl2.Select("balqty<isuqty");

                if (dr1.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Balance" + "');", true);
                    return;
                }
            }


            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetPerMatIssu();
            }

            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string mISUDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");

            // Duplicate 
            if (mRef.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "SMCR No Should Not Be Empty" + "');", true);
                this.ddlPrevISSList.Items.Clear();
                return;
            }

            else if (dmirfno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "DMIRF No Should Not Be Empty" + "');", true);
                this.ddlPrevISSList.Items.Clear();
                return;
            }


            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKEDDUPISUMRFNO", mRef, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
            }

            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("isuno <>'" + mISUNO + "'");
                DataTable dt = dv1.ToTable();
                if (dt.Rows.Count == 0)
                { }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Found Duplicate SMCR.No" + "');", true);
                    this.ddlPrevISSList.Items.Clear();
                    return;
                }
            }


            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string mISURNAR = this.txtISSNarr.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "UPDATE_MKT_MISSUE_INFO", "MKTMISSUEB",
                             mISUNO, mISUDAT, mPACTCODE, mISURNAR, mRef, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, mSmcr, "", "");
         
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }


            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string prtype = tbl2.Rows[i]["prtype"].ToString();
                string acttype = tbl2.Rows[i]["acttype"].ToString();
                double Isuqty = Convert.ToDouble(tbl2.Rows[i]["isuqty"].ToString());
                string txtremarks = tbl2.Rows[i]["remarks"].ToString();

                if (Isuqty > 0)
                {

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "UPDATE_MKT_MISSUE_INFO", "MKTMISSUEA", mISUNO,
                        prtype, acttype, Isuqty.ToString(), txtremarks, "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
            }

            string CurDate1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet dsx = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURMISSUEINFO", mISUNO, "", "", "", "", "", "", "", "");
            if (dsx == null)
                return;
            this.XmlDataInsert(mISUNO, dsx);

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Material Issue Update Successfully" + "');", true);

            this.txtCurISSDate.Enabled = false;
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Issue Information";
                string eventdesc = "Update Issue QTY";
                string eventdesc2 = "Issue No: " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblmatissue"];
            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {
                double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtisuqty")).Text.Trim());
                string txtisurmk = ((TextBox)this.grvissue.Rows[i].FindControl("txtisurmk")).Text.Trim();

                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;
                dt.Rows[TblRowIndex]["isuqty"] = txtwrkqty;
                dt.Rows[TblRowIndex]["remarks"] = txtisurmk;

            }
            ViewState["tblmatissue"] = dt;
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblmatissue"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

            if (tbl1.Rows.Count > 0)
            {
                DataSet ds1 = new DataSet();
                ds1.Tables.Add(tbl1);
                //this.XmlDataDeleted(mISUNO, ds1);
            }


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEMATISUEALL", mISUNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Delete Successfully" + "');", true);
            }


        }

        protected void grvissue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvissue.PageIndex = e.NewPageIndex;
            this.grvissue_DataBind();
        }
        protected void ibtnSearchMaterisl_Click(object sender, EventArgs e)
        {
            this.GetMaterials();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();
        }


        protected void lbtnPrevOk_Click(object sender, EventArgs e)
        {

        }

        private bool XmlDataDeleted(string Reqno, DataSet ds)
        {
            //Log Data
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno);

            if (!resulta)
            {

                return false;
            }


            return true;

        }

        private bool XmlDataInsert(string Reqno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("postedbyid", typeof(string));
            dt1.Columns.Add("postedseson", typeof(string));
            dt1.Columns.Add("postedtrmnid", typeof(string));
            dt1.Columns.Add("posteddate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["postedbyid"] = usrid;
            dr1["postedseson"] = session;
            dr1["postedtrmnid"] = trmnid;
            dr1["posteddate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno);

            if (!resulta)
            {

                return false;
            }


            return true;
        }

        protected void lbtngvMatDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            int gvRowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowIndex = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + gvRowIndex; 
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string prtype = ((Label)this.grvissue.Rows[rowIndex].FindControl("lblPRType")).Text.Trim();
            string acttype = ((Label)this.grvissue.Rows[rowIndex].FindControl("lblgvActType")).Text.Trim();

            if (dt.Rows.Count > 0)
            {
                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt);
            }

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "DELETE_ISSUED_MAT", mISUNO, prtype, acttype, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {                
                dt.Rows[rowIndex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmatissue");
            ViewState["tblmatissue"] = dv.ToTable();
            this.grvissue_DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Material Deleted Successfully" + "');", true);

        }
    }
}