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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_12_Inv
{
    public partial class PurMTReqEntry : System.Web.UI.Page
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
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string title = "";

                if (this.Request.QueryString["Type"].ToString() == "Entry")
                {
                    title = "MATERIALS TRANSFER Requisition";
                    if (this.ddlprjlistfrom.Items.Count == 0)
                    {
                        this.Load_Dates_And_Trans_No();
                        this.GetProject();
                        this.Load_Project_From_Combo();
                        this.tableintosession();
                    }

                    this.lblddlProjectFrom.Visible = false;
                    this.lblddlProjectTo.Visible = false;
                    this.ddlprjlistfrom.Visible = true;
                    this.ddlprjlistto.Visible = true;
                    this.lbtnOk.Visible = true;
                    this.pnlprevious.Visible = true;
                    this.pnlreq.Visible = false;
                    this.pnlreqaprv.Visible = false;
                    this.pnlreqchk.Visible = false;

                }
                else if (this.Request.QueryString["Type"].ToString() == "ReqApproval")
                {
                    title = "MATERIALS TRANSFER Approval";

                    this.lblddlProjectFrom.Visible = true;
                    this.lblddlProjectTo.Visible = true;
                    this.ddlprjlistfrom.Visible = false;
                    this.ddlprjlistto.Visible = false;
                    this.lbtnOk.Visible = false;
                    this.pnlprevious.Visible = false;
                    this.pnlreq.Visible = false;
                    this.pnlreqaprv.Visible = true;
                    this.pnlreqchk.Visible = false;
                    this.txtCurTransDate.ReadOnly = true;
                    this.getMatReqInfo();
                }
                else if (this.Request.QueryString["Type"].ToString() == "ReqChecked")
                {
                    title = "MATERIALS TRANSFER Checked";
                    this.lblddlProjectFrom.Visible = true;
                    this.lblddlProjectTo.Visible = true;
                    this.ddlprjlistfrom.Visible = false;
                    this.ddlprjlistto.Visible = false;
                    this.lbtnOk.Visible = false;
                    this.pnlprevious.Visible = false;
                    this.pnlreqaprv.Visible = false;
                    this.pnlreqchk.Visible = true;
                    this.txtCurTransDate.ReadOnly = true;
                    this.getMatReqInfoChk();
                }
                else if (this.Request.QueryString["Type"].ToString() == "ReqEdit")
                {
                    title = "MATERIALS TRANSFER Requisition Edit";
                    this.getReqMaterialInfo();

                }

                ((Label)this.Master.FindControl("lblTitle")).Text = title;
            }

            this.txtCurTransDate_CalendarExtender.EndDate = System.DateTime.Today;
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
        protected void GetMatTrns()
        {

            string comcod = GetCompCode();
            //string mREQNO = "NEWISS";
            string mREQNO;
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();

            string mREQDAT = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString();
            //if (mREQNO == "NEWISS")
            //{
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LASTMTRNO", mREQDAT,
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxmtrno"].ToString();
                this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 6);
                this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                this.ddlPrevISSList.DataTextField = "maxtrnno1";
                this.ddlPrevISSList.DataValueField = "maxmtrno";
                this.ddlPrevISSList.DataSource = ds2.Tables[0];
                this.ddlPrevISSList.DataBind();
            }

        }

        protected void Load_Dates_And_Trans_No()
        {


            string comcod = this.GetCompCode();
            this.txtCurTransDate.Text = GetStdDate(DateTime.Today.ToString("dd.MM.yyyy"));//XXXXXXXXXXXXXX
            this.Last_trn_no();


        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;

        }
        protected void tableintosession()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("comcod", Type.GetType("System.String"));
            dttemp.Columns.Add("rsircode", Type.GetType("System.String"));
            dttemp.Columns.Add("spcfcod", Type.GetType("System.String"));
            dttemp.Columns.Add("resdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("spcfdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("sirunit", Type.GetType("System.String"));
            dttemp.Columns.Add("qty", Type.GetType("System.String"));
            dttemp.Columns.Add("rate", Type.GetType("System.String"));
            dttemp.Columns.Add("amt", Type.GetType("System.Double"));
            dttemp.Columns.Add("reqno", Type.GetType("System.String"));

            Session["sessionforgrid"] = dttemp;

        }

        private void GetProject()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetProjectFromList", "%%", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            Session["projectlist"] = ds1.Tables[0];

        }
        protected void Load_Project_From_Combo()
        {

            DataTable dt = (DataTable)Session["projectlist"];

            string srchfrmproject = this.txtSearchRes.Text.Trim();
            //string srchfrmproject = this.txtSrcfrmprojet.Text.Trim();

            if (srchfrmproject.Length > 0)
            {

                EnumerableRowCollection<DataRow> item = (from r in dt.AsEnumerable()
                                                         where r.Field<string>("actdesc1").ToUpper().Contains(srchfrmproject.ToUpper())
                                                         select r);
                dt = item.AsDataView().ToTable();


            }




            this.ddlprjlistfrom.DataTextField = "actdesc1";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = dt;
            this.ddlprjlistfrom.DataBind();
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }
        protected void Load_Project_To_Combo()
        {
            string comcod = this.GetCompCode();
            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjectFromList", "%%", "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            DataTable dt = (DataTable)Session["projectlist"];

            string actcode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode not in ('" + actcode + "')";
            DataTable dt1 = dv1.ToTable();


            string srchfrmproject = this.txtSearchRes.Text.Trim();

            //string srchfrmproject = this.txtsrchtoproject.Text.Trim();

            if (srchfrmproject.Length > 0)
            {

                EnumerableRowCollection<DataRow> item = (from r in dt1.AsEnumerable()
                                                         where r.Field<string>("actdesc1").ToUpper().Contains(srchfrmproject.ToUpper())
                                                         select r);
                dt1 = item.AsDataView().ToTable();


            }

            this.ddlprjlistto.DataTextField = "actdesc1";
            this.ddlprjlistto.DataValueField = "actcode";
            this.ddlprjlistto.DataSource = dt1;
            this.ddlprjlistto.DataBind();
            string prjcode = this.Request.QueryString["prjcode"].ToString();
            if (prjcode.Length > 0)
                this.ddlprjlistto.SelectedValue = prjcode;
        }
        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_To_Combo();

        }
        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Load_Project_Res_Combo();
        }

        protected void Load_Project_Res_Combo()
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string FindResDesc = this.txtSearchRes.Text.Trim() + "%";
            string curdate = this.txtCurTransDate.Text.ToString().Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetProjResList", ProjectCode, curdate, FindResDesc, "", "", "", "", "", "");
            Session["projectreslist"] = ds1.Tables[0];
            ViewState["tblspcf"] = ds1.Tables[1];

            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for Store');", true);
                return;
            }

            DataView dv = ds1.Tables[0].DefaultView;
            dv.Sort = "rsircode";
            DataTable dt = dv.ToTable(true, "rsircode", "resdesc");
            this.ddlreslist.DataTextField = "resdesc";
            this.ddlreslist.DataValueField = "rsircode";
            this.ddlreslist.DataSource = dt;
            this.ddlreslist.DataBind();
            ds1.Dispose();

            this.GetSpecification();
        }

        private void GetSpecification()
        {
            string mResCode = this.ddlreslist.SelectedValue.ToString();
            //string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblspcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = ("mspcfcod = '" + mResCode + "'");
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string rescode = this.ddlreslist.SelectedValue.ToString().Trim();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            DataTable dt1 = (DataTable)Session["projectreslist"];
            DataRow[] projectrow1 = dt1.Select("rsircode = '" + rescode + "' and spcfcod ='" + spcfcod + "'");
            DataRow[] projectrow2 = dt.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");

            if (projectrow2.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();
                drforgrid["comcod"] = projectrow1[0]["comcod"];
                drforgrid["rsircode"] = projectrow1[0]["rsircode"];
                drforgrid["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                drforgrid["resdesc"] = projectrow1[0]["resdesc"];
                drforgrid["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text;
                drforgrid["sirunit"] = projectrow1[0]["sirunit"];
                drforgrid["qty"] = projectrow1[0]["qty"];
                drforgrid["rate"] = projectrow1[0]["rate"];
                drforgrid["amt"] = projectrow1[0]["amt"];
                drforgrid["balqty"] = projectrow1[0]["balqty"];

                dt.Rows.Add(drforgrid);
            }
            ViewState["tblmattrns"] = dt;
            this.Data_Bind();

        }



        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tblmattrns"];
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                double rat = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + i;
                dt1.Rows[rowindex]["qty"] = qty;
                double damt = qty * rat;
                dt1.Rows[i]["rate"] = rat;
                dt1.Rows[i]["amt"] = damt;
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
            if (Request.QueryString["Type"].ToString() == "ReqEdit")
            {
                this.mtrReqEditSave();
            }
            else
            {
                this.mtrReqEntrySave();
            }
        }


        private void mtrReqEntrySave()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string msg1 = "";

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                msg1 = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
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
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            string mtreqdat = this.txtCurTransDate.Text.ToString().Trim();
            if (ddlPrevISSList.Items.Count == 0)
                this.GetMatTrns();
            string mtreqno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + mtreqdat.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
            string mtrref = this.txtrefno.Text.ToString();
            string mtrnar = this.txtReqNarr.Text.ToString();
            string fromprj = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim();

            dr1 = dt.Select("balqty<qty");

            if ((comcod=="3367") && ASTUtility.Left(fromprj, 2) == "11")
            {

            }

            else
            {
                if (dr1.Length > 0)
                {
                    msg1 = "Not Within the Balance";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                    return;
                }

            }
           

            if (mtrref.Length == 0)
            {
                msg1 = "MTRF No Should Not Be Empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                return;
            }
            DataRow[] dr2 = dt.Select("qty=0.00");
            if (dr2.Length > 0)
            {
                msg1 = "Please Fillup Qtuantity  ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                return;
            }
            switch (comcod)
            {
                case "3101":
                case "3340":
                case "3315":
                case "3316":
                case "1108":
                case "1109":
                case "1205":
                case "3351":
                case "3352":
                case "3348":

                    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "CHECKEDDUPMATREQREF", mtrref, "", "", "", "", "", "", "", "");

                    if (ds2.Tables[0].Rows.Count == 0)
                        ;


                    else
                    {

                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("mtrref ='" + mtrref + "'");
                        DataTable dt1 = dv1.ToTable();
                        if (dt1.Rows.Count == 0)
                            ;
                        else
                        {
                            msg1 = "Found Duplicate MTRF. No !!!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                            return;
                        }
                    }
                    break;
            }

           
            string reqno = this.lblreqno.Text.Trim();
            string reqApproval = this.getCompReApproval();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTREQ", "PURMTREQB", mtreqno, mtreqdat, fromprj, toprj, mtrref, mtrnar, PostedByid, Posttrmid,

                           PostSession, Posteddat, reqno, reqApproval, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                msg1 = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                return;
            }



            foreach (DataRow dr in dt.Rows)
            {

                string trsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string tqty = dr["qty"].ToString().Trim();
                string trate = dr["rate"].ToString().Trim();
                string tamt = dr["amt"].ToString().Trim();
                // string reqno = dr["reqno"].ToString().Trim();


                //if (dr["qty"] <= 0)
                //{

                //}

                bool result1 = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTREQ", "PURMTREQA", mtreqno, trsircode, spcfcod, tqty,
                   tamt, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result1)
                {
                    msg1 = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);

                }
            }

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //  string trsircode=dt.Rows[i]["rsircode"].ToString().Trim();
            //  string tunit=dt.Rows[i]["sirunit"].ToString().Trim();
            //  string tqty=dt.Rows[i]["qty"].ToString().Trim();
            //  string trate=dt.Rows[i]["rate"].ToString().Trim();
            //  string tamt = dt.Rows[i]["amt"].ToString().Trim();

            //  bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UpdateTransferInf", transno, fromprj, toprj, trsircode,
            //      tunit, tqty, trate, tamt, curdate, Refno, PostedByid, Posttrmid, PostSession, Posteddat, "");
            //}

            msg1 = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg1 + "');", true);

            this.txtCurTransDate.Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Transfer";
                string eventdesc = "Update New QTY & RATE";
                string eventdesc2 = "From " + this.ddlprjlistfrom.SelectedItem.ToString() + " To " + this.ddlprjlistto.SelectedItem.ToString() + " - " + mtreqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void mtrReqEditSave()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                string Messaged = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            string mtreqdat = this.txtCurTransDate.Text.ToString().Trim();
            string mtreqno = this.Request.QueryString["genno"].ToString();
            string mtrref = this.txtrefno.Text.ToString();
            string mtrnar = this.txtReqNarr.Text.ToString();

       

            dr1 = dt.Select("balqty<qty");          
           if (dr1.Length > 0)
            {
                string Messaged = "Not Within the Balance";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }


            

            if (mtrref.Length == 0)
            {
                string Messaged = "MTRF No Should Not Be Empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }
            DataRow[] dr2 = dt.Select("qty=0.00");
            if (dr2.Length > 0)
            {
                string Messaged = "Please Fillup Qtuantity  ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }

            string fromprj = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim();
            string reqno = this.lblreqno.Text.Trim();
            string reqApproval = this.getCompReApproval();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTREQ", "PURMTREQB", mtreqno, mtreqdat, fromprj, toprj, mtrref, mtrnar, PostedByid, Posttrmid,
                           PostSession, Posteddat, reqno, reqApproval, "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                string msg = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            foreach (DataRow dr in dt.Rows)
            {

                string trsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string tqty = dr["qty"].ToString().Trim();
                string trate = dr["rate"].ToString().Trim();
                string tamt = dr["amt"].ToString().Trim();
                bool result1 = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTREQ", "PURMTREQA", mtreqno, trsircode, spcfcod, tqty,
                   tamt, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result1)
                {
                    string msg = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                }
            }


            string msg1 = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg1 + "');", true);
            this.txtCurTransDate.Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Transfer";
                string eventdesc = "Update New QTY & RATE";
                string eventdesc2 = "From " + this.ddlprjlistfrom.SelectedItem.ToString() + " To " + this.ddlprjlistto.SelectedItem.ToString() + " - " + mtreqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }




        private string getCompReApproval()
        {
            string comcod = this.GetCompCode();
            string ptype = "";
            switch (comcod)
            {
                //case "3101":
                case "1205":
                case "3351":
                case "3352":
                case "8306":
                    ptype = "Approved";
                    break;

                case "3101":
                case "3367":
                    ptype = "Checked";
                    break;

                default:
                    ptype = "";
                    break;

            }
            return ptype;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.grvacc.Columns[6].FooterText.Length > 0)
                this.grvacc.Columns[6].FooterText = "";
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.pnlreq.Visible = true;
                this.lblddlProjectFrom.Visible = true;
                this.lblddlProjectTo.Visible = true;
                this.ddlprjlistfrom.Visible = false;
                this.ddlprjlistto.Visible = false;
                this.lblprious.Visible = false;
                this.ImgbtnFindMTno.Visible = false;
                this.txtSrchMrfNo.Visible = false;
                //this.lbtnPrevTransList.Visible = false;
                this.ddlPrevISSList.Visible = false;
                this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
                this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;
                if (this.Request.QueryString["Type"].ToString() == "ReqEdit")
                {
                    this.GetMatTransferReq();
                }
                else
                {
                    this.GetMatTransfer();
                }

                this.ImgbtnFindRes_Click(null, null);
                this.lnkselect.Visible = true;
            }
            else
            {

                this.ddlprjlistfrom.Visible = true;
                this.ddlprjlistto.Visible = true;
                this.lblprious.Visible = true;
                this.txtSrchMrfNo.Visible = true;
                this.ImgbtnFindMTno.Visible = true; ;

                // this.lbtnPrevTransList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.lblddlProjectFrom.Visible = false;
                this.lblddlProjectTo.Visible = false;
                this.txtCurTransDate.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.Last_trn_no();
                this.pnlreq.Visible = false;
                this.txtrefno.Text = "";

                this.lblVoucherNo.Text = "";
                lbtnOk.Text = "Ok";
                this.ddlPrevISSList.Items.Clear();
                this.lnkselect.Visible = false;

            }
        }

        private void GetMatTransfer()
        {


            ViewState.Remove("tblmattrns");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            string mTRNNo = "NEWTRNS";
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurTransDate.Enabled = false;
                mTRNNo = this.ddlPrevISSList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mTRNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];


            if (mTRNNo == "NEWTRNS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LASTMTRNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurTransNo1.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(0, 6);
                this.txtCurTransNo2.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(6);


                string reqno = this.Request.QueryString["genno"].ToString();
                if (reqno.Length > 0)
                {
                    this.ShowReqInfo();

                }

                return;
            }


            this.ddlprjlistfrom.SelectedValue = ds1.Tables[1].Rows[0]["tfpactcode"].ToString();
            this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);
            this.ddlprjlistto.SelectedValue = ds1.Tables[1].Rows[0]["ttpactcode"].ToString();
            this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;

            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mtrdat"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["mtrref"].ToString();
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["mtrnar"].ToString();
            this.lblCurTransNo1.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(6);
            this.lblreqno.Text = ds1.Tables[1].Rows[0]["reqno"].ToString(); ;
            // this.lblVoucherNo.Text = ds1.Tables[1].Rows[0]["vounum"].ToString().Trim();
            this.Data_Bind();
        }


        private void GetMatTransferReq()
        {


            ViewState.Remove("tblmattrns");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            string mTRNNo = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mTRNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];

            this.ddlprjlistfrom.SelectedValue = ds1.Tables[1].Rows[0]["tfpactcode"].ToString();
            this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);
            this.ddlprjlistto.SelectedValue = ds1.Tables[1].Rows[0]["ttpactcode"].ToString();
            this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;

            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mtrdat"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["mtrref"].ToString();
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["mtrnar"].ToString();
            this.lblCurTransNo1.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(6);
            this.lblreqno.Text = ds1.Tables[1].Rows[0]["reqno"].ToString();
            this.Data_Bind();
        }

        private void ShowReqInfo()
        {

            string comcod = this.GetCompCode();
            string reqno = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETREQINFO", reqno, "",
                        "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];
            this.ddlprjlistto.SelectedValue = this.Request.QueryString["prjcode"].ToString();
            this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;
            this.lblreqno.Text = reqno;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt1 = (DataTable)ViewState["tblmattrns"];
            this.grvacc.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = dt1;
            this.grvacc.DataBind();

            this.grvacc.Columns[1].Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            if (dt1.Rows.Count == 0)
                return;
            ((LinkButton)this.grvacc.FooterRow.FindControl("lnkupdate")).Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
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

        protected void Last_trn_no()
        {

            string comcod = this.GetCompCode();
            string date = this.txtCurTransDate.Text;
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LastTransferNo", date, "", "", "", "", "", "", "", "");
            if (ds == null)
                return;
            this.lblCurTransNo1.Text = ds.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(6);

        }
        protected void lbtnPrevTransList_Click(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();
        }
        protected void Load_Prev_Trans_List()
        {

            string comcod = this.GetCompCode();
            string fxtast = "%";
            string mtrfno = "%" + this.txtSrchMrfNo.Text.Trim() + "%";
            string prjcode = "%"; // this.ddlprjlistfrom.SelectedValue.ToString();
            string CurDate1 = (this.txtCurTransDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPRIVIOUSLIST", CurDate1,
                          prjcode, fxtast, mtrfno, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.DataTextField = "mtreqno1";
            this.ddlPrevISSList.DataValueField = "mtreqno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvacc.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk_Click(null, null);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt1 = (DataTable)ViewState["tblmattrns"];

            // General Part
            string mtreqno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + this.txtCurTransDate.Text.ToString().Trim().Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPADDANDOTHERPTREQ", mtreqno, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.PurMatReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMaterialTrnsferReq", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));

            Rpt1.SetParameters(new ReportParameter("txtprjname", ds1.Tables[0].Rows[0]["ttpactdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtfpactdesc", ds1.Tables[0].Rows[0]["tfpactdesc"].ToString() + "\n" + ds1.Tables[0].Rows[0]["tfpactadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtPrjAddress", ds1.Tables[0].Rows[0]["ttpactadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtMrrRef", this.txtrefno.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtDate", Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtmtrno", this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtReqNarr.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Transfer Requisition  Form (MTRF)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            // ReportDocument rptFassttran = new RealERPRPT.R_12_Inv.RptMaterialTrnsferReq();
            // TextObject rptCname = rptFassttran.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            // rptCname.Text = comnam;

            // TextObject rptProjectNameft = rptFassttran.ReportDefinition.ReportObjects["ProjectNamef"] as TextObject;
            // rptProjectNameft.Text = ds1.Tables[0].Rows[0]["tfpactdesc"].ToString() + "\n" + ds1.Tables[0].Rows[0]["tfpactadd"].ToString();

            // TextObject rptdate = rptFassttran.ReportDefinition.ReportObjects["date"] as TextObject;
            // rptdate.Text =  Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd.MM.yyyy");
            // TextObject txttoproj = rptFassttran.ReportDefinition.ReportObjects["txttoproj"] as TextObject;
            // txttoproj.Text =ds1.Tables[0].Rows[0]["ttpactdesc"].ToString() ;
            // TextObject txttoAddress = rptFassttran.ReportDefinition.ReportObjects["txttoAddress"] as TextObject;
            // txttoAddress.Text =  ds1.Tables[0].Rows[0]["ttpactadd"].ToString();


            // TextObject txtadd = rptFassttran.ReportDefinition.ReportObjects["txtadd"] as TextObject;
            // txtadd.Text = comadd;

            // TextObject txtnarration = rptFassttran.ReportDefinition.ReportObjects["txtnarration"] as TextObject;
            // txtnarration.Text = txtReqNarr.Text.Trim();

            //TextObject mtrref = rptFassttran.ReportDefinition.ReportObjects["mtrref"] as TextObject;
            // mtrref.Text = this.txtrefno.Text.Trim();

            // TextObject mtrno = rptFassttran.ReportDefinition.ReportObjects["mtrno"] as TextObject;
            // mtrno.Text = this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim();
            // TextObject txtuserinfo = rptFassttran.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            // if (ConstantInfo.LogStatus == true)
            // {
            //     string eventtype = "Materials Transfer";
            //     string eventdesc = "Print Report";
            //     string eventdesc2 = "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy"); ;
            //     bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            // }
            // rptFassttran.SetDataSource(dt1);
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            // rptFassttran.SetParameterValue("ComLogo", ComLogo);
            // Session["Report1"] = rptFassttran;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            string mISUNO = this.lblCurTransNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurTransDate.Text.Trim()), 4) + this.lblCurTransNo1.Text.Trim().Substring(3, 2) + this.txtCurTransNo2.Text.Trim();
            //string FrmPrjCode = this.ddlprjlistfrom.SelectedValue.ToString();
            //string ToPrjCode = this.ddlprjlistto.SelectedValue.ToString();
            string rsircode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvMatCode")).Text.Trim();
            string spcfcod = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgspcfcode")).Text.Trim();


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELMTRREQNO", mISUNO, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmattrns");
            ViewState["tblmattrns"] = dv.ToTable();
            this.Data_Bind();

        }
        protected void ddlreslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();
        }

        protected void lbtntoProject_Click(object sender, EventArgs e)
        {
            this.Load_Project_To_Combo();

        }
        protected void lbtnfrmproject_Click(object sender, EventArgs e)
        {
            this.Load_Project_From_Combo();
        }



        protected void ImgbtnFindMTno_Click1(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();

        }


        private void getMatReqInfo()
        {
            string comcod = this.GetCompCode();
            string reqno = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPREVIOUSMTRREQ", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            this.lblddlProjectFrom.Text = ds1.Tables[1].Rows[0]["fproject"].ToString();
            this.lblddlProjectTo.Text = ds1.Tables[1].Rows[0]["tproject"].ToString();

            ViewState["tblreqaprv"] = ds1.Tables[0];
            ViewState["tblreqprj"] = ds1.Tables[1];
            this.Data_Bind_Aprv();

            //ddlprjlistfrom
            //ddlprjlistto
            //lblddlProjectTo
            //lbtnOk


        }

        private void getMatReqInfoChk()
        {
            string comcod = this.GetCompCode();
            string reqno = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPREVIOUSMTRREQ", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            this.lblddlProjectFrom.Text = ds1.Tables[1].Rows[0]["fproject"].ToString();
            this.lblddlProjectTo.Text = ds1.Tables[1].Rows[0]["tproject"].ToString();

            ViewState["tblreqchk"] = ds1.Tables[0];
            ViewState["tblreqprjchk"] = ds1.Tables[1];
            this.Data_Bind_Checked();
            this.txtNarchk.Visible = true;
            this.txtNarchk.Text= ds1.Tables[1].Rows[0]["mtrnar"].ToString(); 

            //ddlprjlistfrom
            //ddlprjlistto
            //lblddlProjectTo
            //lbtnOk


        }

        private void Data_Bind_Aprv()
        {
            DataTable dt1 = (DataTable)ViewState["tblreqaprv"];
            this.gvreqaprv.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
            this.gvreqaprv.DataSource = dt1;
            this.gvreqaprv.DataBind();

            if (dt1.Rows.Count == 0)
                return;
            this.FooterCalCulationaprv();
        }

        private void FooterCalCulationaprv()
        {
            DataTable dt1 = (DataTable)ViewState["tblreqaprv"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.gvreqaprv.FooterRow.FindControl("lgvAprvAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); ;
        }


        protected void gvreqaprv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblreqaprv"];
            string rsircode = ((Label)this.gvreqaprv.Rows[e.RowIndex].FindControl("lblgvrsircode")).Text.Trim();
            string spcfcod = ((Label)this.gvreqaprv.Rows[e.RowIndex].FindControl("lblgvspcfcod")).Text.Trim();
            string mISUNO = this.Request.QueryString["genno"].ToString();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "DELMTRREQAPPROVAL", mISUNO, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvreqaprv.PageSize) * (this.gvreqaprv.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblreqaprv");
            ViewState["tblreqaprv"] = dv.ToTable();
            this.Data_Bind_Aprv();
        }

        protected void lnkbtnApproved_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.SaveApproval();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string APRVBYID = hst["usrid"].ToString();
            string APRVDAT = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string APRVTRMID = hst["compname"].ToString();
            string APRVSESON = hst["session"].ToString();


            string comcod = this.GetCompCode();
            DataTable dt1 = (DataTable)ViewState["tblreqaprv"];
            DataTable dt2 = (DataTable)ViewState["tblreqprj"];

            string mtreqno = this.Request.QueryString["genno"].ToString();
            string fromprj = dt2.Rows[0]["TFPACTCODE"].ToString();
            string toprj = dt2.Rows[0]["TTPACTCODE"].ToString();


            bool result;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                string mRSIRCODE = dt1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = dt1.Rows[i]["spcfcod"].ToString();
                double reqty = Convert.ToDouble(dt1.Rows[i]["tqty"]);
                double reqamt = Convert.ToDouble(dt1.Rows[i]["amt"]);
                result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "UPDATEMTRREQAPROVAL", mtreqno, mRSIRCODE, mSPCFCOD, reqty.ToString(), reqamt.ToString(), "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }


            result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "MTREQAPPROVAL", mtreqno, fromprj, toprj, APRVBYID, APRVDAT, APRVSESON, APRVTRMID, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }

        protected void lnkaptotal_Click(object sender, EventArgs e)
        {
            this.SaveApproval();
            this.Data_Bind_Aprv();
        }

        private void SaveApproval()
        {
            DataTable tbl1 = (DataTable)ViewState["tblreqaprv"];
            int index;
            for (int j = 0; j < this.gvreqaprv.Rows.Count; j++)
            {

                index = (this.gvreqaprv.PageSize) * (this.gvreqaprv.PageIndex) + j;
                double reqqty = Convert.ToDouble(tbl1.Rows[index]["tqty"]);
                double aprvqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvreqaprv.Rows[j].FindControl("txtgvtqty")).Text.Trim()));
                double rat = Convert.ToDouble("0" + ((Label)this.gvreqaprv.Rows[j].FindControl("lblgvrate")).Text.Trim());

                if (aprvqty > reqqty)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Approved Qty Can't Large Requisition Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);",
                        true);
                    return;
                }

                tbl1.Rows[index]["tqty"] = aprvqty;
                double damt = aprvqty * rat;
                tbl1.Rows[j]["rate"] = rat;
                tbl1.Rows[j]["amt"] = damt;
            }
            ViewState["tblreqaprv"] = tbl1;


        }

        private void getReqMaterialInfo()
        {

            //this.lblddlProjectFrom.Visible = true;
            //this.lblddlProjectTo.Visible = true;
            //this.ddlprjlistfrom.Visible = false;
            //this.ddlprjlistto.Visible = false;
            //this.lbtnOk.Visible = false;
            //this.pnlprevious.Visible = false;
            //this.pnlreq.Visible = true;
            //this.pnlreqaprv.Visible = true;
            //this.txtCurTransDate.ReadOnly = true;
            //this.getMatReqInfo();

            if (this.ddlprjlistfrom.Items.Count == 0)
            {
                this.Load_Dates_And_Trans_No();
                this.GetProject();
                this.Load_Project_From_Combo();
                this.tableintosession();
            }
            this.getMatReqInfo();
            this.lbtnOk_Click(null, null);
        }

        private void Data_Bind_Checked()
        {
            DataTable dt1 = (DataTable)ViewState["tblreqchk"];
            this.gvreqchk.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
            this.gvreqchk.DataSource = dt1;
            this.gvreqchk.DataBind();

            if (dt1.Rows.Count == 0)
                return;
            this.FooterCalCulationChecked();
        }

        private void FooterCalCulationChecked()
        {
            DataTable dt1 = (DataTable)ViewState["tblreqchk"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.gvreqchk.FooterRow.FindControl("lgvAmountChk")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); ;
        }

        protected void gvreqchk_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblreqchk"];
            string rsircode = ((Label)this.gvreqchk.Rows[e.RowIndex].FindControl("lblgvrsircodechk")).Text.Trim();
            string spcfcod = ((Label)this.gvreqchk.Rows[e.RowIndex].FindControl("lblgvspcfcodchk")).Text.Trim();
            string mISUNO = this.Request.QueryString["genno"].ToString();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "DELMTRREQAPPROVAL", mISUNO, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvreqchk.PageSize) * (this.gvreqchk.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblreqchk");
            ViewState["tblreqchk"] = dv.ToTable();
            this.Data_Bind_Checked();
        }

        protected void lnkaptotalchk_Click(object sender, EventArgs e)
        {
            this.SaveChecked();
            this.Data_Bind_Checked();
        }

        private void SaveChecked()
        {
            DataTable tbl1 = (DataTable)ViewState["tblreqchk"];
            int index;
            for (int j = 0; j < this.gvreqaprv.Rows.Count; j++)
            {

                index = (this.gvreqchk.PageSize) * (this.gvreqchk.PageIndex) + j;
                double reqqty = Convert.ToDouble(tbl1.Rows[index]["tqty"]);
                double chkqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvreqchk.Rows[j].FindControl("txtgvtqtychk")).Text.Trim()));
                double rat = Convert.ToDouble("0" + ((Label)this.gvreqchk.Rows[j].FindControl("lblgvratechk")).Text.Trim());

                if (chkqty > reqqty)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Checked Qty Can't Large Requisition Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);",
                        true);
                    return;
                }

                tbl1.Rows[index]["tqty"] = chkqty;
                double damt = chkqty * rat;
                tbl1.Rows[j]["rate"] = rat;
                tbl1.Rows[j]["amt"] = damt;
            }
            ViewState["tblreqchk"] = tbl1;
        }

        protected void lnkbtnChecked_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.SaveChecked();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string APRVBYID = hst["usrid"].ToString();
            string APRVDAT = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string APRVTRMID = hst["compname"].ToString();
            string APRVSESON = hst["session"].ToString();


            string comcod = this.GetCompCode();
            DataTable dt1 = (DataTable)ViewState["tblreqchk"];
            DataTable dt2 = (DataTable)ViewState["tblreqprjchk"];

            string mtreqno = this.Request.QueryString["genno"].ToString();
            string fromprj = dt2.Rows[0]["TFPACTCODE"].ToString();
            string toprj = dt2.Rows[0]["TTPACTCODE"].ToString();
            string txtNarchk = this.txtNarchk.Text.Trim();

            bool result;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                string mRSIRCODE = dt1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = dt1.Rows[i]["spcfcod"].ToString();
                double reqty = Convert.ToDouble(dt1.Rows[i]["tqty"]);
                double reqamt = Convert.ToDouble(dt1.Rows[i]["amt"]);
                result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "UPDATEMTRREQAPROVAL", mtreqno, mRSIRCODE, mSPCFCOD, reqty.ToString(), reqamt.ToString(), "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }


            result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "MTREQCHECKED", mtreqno, fromprj, toprj, APRVBYID, APRVDAT, APRVSESON, APRVTRMID, txtNarchk, "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

    }
}
