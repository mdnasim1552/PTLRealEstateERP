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
    public partial class MaterialsTransfer : System.Web.UI.Page
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS TRANSFER Approval";

                if (this.ddlprjlistfrom.Items.Count == 0)
                {

                    this.Load_Dates_And_Trans_No();
                    this.Load_Project_From_Combo();
                    this.tableintosession();

                }
                if ((this.Request.QueryString["genno"].ToString().Length > 0))
                {
                    if (this.Request.QueryString.AllKeys.Contains("pType"))
                    {
                        //
                        this.printMtrRecevFAudit();

                    }
                    else
                    {
                        string genno = this.Request.QueryString["genno"].ToString();
                        if (genno.Substring(0, 3) == "GPN")
                        {
                            this.chkGatePass.Checked = true;
                            this.chkGatePass_CheckedChanged(null, null);
                            this.lbtnGatePassNo_Click(null, null);
                        }
                        else
                        {
                            this.Load_Prev_Trans_List();
                        }

                        this.lbtnOk_Click(null, null);
                    }


                }
                if (this.Request.QueryString["Type"] == "Audit")
                {
                    this.disableRef();
                }


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


        private void disableRef()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "3101":
                case "1205":
                case "3351":
                case "3352":
                    this.txtrefno.ReadOnly = true;
                    break;
                default:
                    this.txtrefno.ReadOnly = false;
                    break;
            }
        }

        protected void GetMatTrns()
        {

            string comcod = GetCompCode();
            string mTRNNO = "NEWTRNS";

            if (this.ddlPrevISSList.Items.Count > 0)
                mTRNNO = this.ddlPrevISSList.SelectedValue.ToString();

            string mTRNDAT = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString();
            if (mTRNNO == "NEWTRNS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LastTransferNo", mTRNDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 6);
                    this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                    this.ddlPrevISSList.DataTextField = "maxtrnno1";
                    this.ddlPrevISSList.DataValueField = "maxtrnno";
                    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                    this.ddlPrevISSList.DataBind();
                }
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

        protected void Load_Project_From_Combo()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjectFromList", "%%", "", "", "", "", "", "", "", "");
            Session["projectlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlprjlistfrom.DataTextField = "actdesc1";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
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
            DataRow[] projectrow = dt.Select("actcode <> '" + this.ddlprjlistfrom.SelectedValue.ToString().Trim() + "'");
            string actcode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode not in ('" + actcode + "')";
            this.ddlprjlistto.DataTextField = "actdesc1";
            this.ddlprjlistto.DataValueField = "actcode";
            this.ddlprjlistto.DataSource = dv1.ToTable();
            this.ddlprjlistto.DataBind();
        }
        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_To_Combo();

        }
        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Load_Project_Res_Combo();
        }

        private string CompBalConMat()
        {

            string comcod = this.GetCompCode();
            string conbal = "";
            switch (comcod)
            {
                case "3301":
                case "1301":
                    conbal = "notcon";
                    break;
                default:
                    conbal = "";
                    break;
            }
            return conbal;

        }
        private string CompanyBGDRes()
        {
            string comcod = this.GetCompCode();
            string bdgRes = "";
            switch (comcod)
            {
                case "3368":
                case "3101":
                    bdgRes = "BgdResource";
                    break;
                default:
                    bdgRes = "";
                    break;
            }
            return bdgRes;

        }

        protected void Load_Project_Res_Combo()
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string FindResDesc = this.txtSearchRes.Text.Trim() + "%";
            string curdate = this.txtCurTransDate.Text.ToString().Trim();
            string balcon = this.CompBalConMat();
            string bgdres = this.CompanyBGDRes();
            Session.Remove("projectreslist");
            ViewState.Remove("tblspcf");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjResList", ProjectCode, curdate, FindResDesc, balcon, bgdres, "", "", "", "");
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

            //this.GetSpecification();


            //this.ddlreslist.DataTextField = "resdesc";
            //this.ddlreslist.DataValueField = "rsircode";
            //this.ddlreslist.DataSource = ds1.Tables[0];
            //this.ddlreslist.DataBind();
            //ds1.Dispose();

            this.GetSpecification();
        }

        private void GetSpecification()
        {
            string mResCode = this.ddlreslist.SelectedValue.ToString().Substring(0, 9);
            string mResCode1 = this.ddlreslist.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblspcf"];
            DataView dv1 = tbl1.DefaultView;
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3368":
                    dv1.RowFilter = "mspcfcod = '" + mResCode1 + "'";
                    break;
                default:
                    dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
                    break;
            }
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();
        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "3101":
                case "3370":
                    this.lblReqNarr.Visible = true;
                    this.txtNarr.Visible = true;
                    string fmprj = this.ddlprjlistfrom.SelectedValue.ToString();
                    string tomprj = this.ddlprjlistto.SelectedValue.ToString();
                    DataSet ds21 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "MATERIALENTRYNATATION", fmprj, tomprj, "", "", "", "", "", "", "");
                    ViewState["prjnaton"] = ds21.Tables[0];
                    string naration = ds21.Tables[0].Rows.Count>0 ? ds21.Tables[0].Rows[0]["mtrnar"].ToString(): "";
                    this.txtNarr.Text = naration;
                    break;
                default:
                    this.lblReqNarr.Visible = false;
                    this.txtNarr.Visible = false;
                    break;

            }
            string rescode = this.ddlreslist.SelectedValue.ToString().Trim();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            DataTable dt1 = (DataTable)Session["projectreslist"];
            DataRow[] projectrow1 = dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            DataRow[] projectrow2 = dt.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");

            if (projectrow2.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();

                drforgrid["mtreqno"] = "";
                drforgrid["mtrref"] = "";
                drforgrid["getpno"] = "";
                drforgrid["getpref"] = "";
                drforgrid["rsircode"] = projectrow1[0]["rsircode"];
                drforgrid["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                drforgrid["resdesc"] = projectrow1[0]["resdesc"];
                drforgrid["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text;
                drforgrid["sirunit"] = projectrow1[0]["sirunit"];
                drforgrid["mtrfqty"] = 0.00;
                drforgrid["balqty"] = projectrow1[0]["balqty"];
                drforgrid["qty"] = projectrow1[0]["qty"];

                drforgrid["rate"] = projectrow1[0]["rate"];
                drforgrid["amt"] = projectrow1[0]["amt"];
                drforgrid["reqno"] = "";

                dt.Rows.Add(drforgrid);
            }
            ViewState["tblmattrns"] = dt;
            this.Data_Bind();

        }



        private void SaveValue()
        {
            DataTable dt1 = (DataTable)ViewState["tblmattrns"];
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

            switch (GetCompCode())
            {
                case "3370":
                    for (int i = 0; i < this.grvacc.Rows.Count; i++)
                    {
                        double mtrfqty = Convert.ToDouble("0" + ((Label)this.grvacc.Rows[i].FindControl("lblgvmtrfqty")).Text.Trim());
                        double balqty = Convert.ToDouble("0" + ((Label)this.grvacc.Rows[i].FindControl("lblBalqty")).Text.Trim());
                        double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());

                        string rsircode = ((Label)this.grvacc.Rows[i].FindControl("lblgvMatCode")).Text.ToString();
                        string spcfcod = ((Label)this.grvacc.Rows[i].FindControl("lblgspcfcode")).Text.ToString();

                        DataRow[] dr3 = dt1.Select("rsircode = '" + rsircode + "' and spcfcod = '" + spcfcod + "'");
                        double rate1 = Convert.ToDouble(dr3[0]["rate"]);

                        double rat = Convert.ToDouble("0" + rate1);
                        int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + i;

                        dt1.Rows[rowindex]["qty"] = qty;
                        double damt = qty * rat;
                        dt1.Rows[i]["rate"] = rat;
                        dt1.Rows[i]["amt"] = damt;
                    }
                    break;

                default:
                    for (int i = 0; i < this.grvacc.Rows.Count; i++)
                    {
                        double mtrfqty = Convert.ToDouble("0" + ((Label)this.grvacc.Rows[i].FindControl("lblgvmtrfqty")).Text.Trim());
                        double balqty = Convert.ToDouble("0" + ((Label)this.grvacc.Rows[i].FindControl("lblBalqty")).Text.Trim());
                        double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                        double rat = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                        int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + i;

                        switch (comcod)
                        {
                            case "3330":
                                if (mtrfqty > 0)
                                {
                                    if (mtrfqty < qty)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Within the Budget');", true);
                                        break;
                                    }
                                }
                                else if (balqty < qty)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Within the Budget');", true);
                                    break;
                                }
                                break;

                            default:
                                break;

                        }

                        dt1.Rows[rowindex]["qty"] = qty;
                        double damt = qty * rat;
                        dt1.Rows[i]["rate"] = rat;
                        dt1.Rows[i]["amt"] = damt;
                    }
                    break;
            }
            ViewState["tblmattrns"] = dt1;
        }

        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("auditbyid", Type.GetType("System.String"));
            tblt01.Columns.Add("audittrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("auditsession", Type.GetType("System.String"));
            tblt01.Columns.Add("auditdat", Type.GetType("System.String"));

            ViewState["tblapproval"] = tblt01;
        }


        private string GetReqApproval(string audit)
        {


            string type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (type)
            {
                case "Entry":
                    switch (comcod)
                    {
                        // todo for audit part
                        case "3101":
                        case "3338": // p2p
                        case "1205": // p2p
                        case "3351": // p2p
                        case "3352": // p2p
                        case "3370": // cpdl

                            break;

                        default:
                            if (audit == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["auditbyid"] = usrid;
                                dr1["auditdat"] = Date;
                                dr1["audittrmid"] = trmnid;
                                dr1["auditsession"] = session;

                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                audit = ds1.GetXml();

                            }

                            break;
                    }

                    break;

                case "Audit":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  

                    if (audit == "")
                    {


                        this.CreateDataTable();
                        DataTable dt = (DataTable)ViewState["tblapproval"];
                        DataRow dr1 = dt.NewRow();

                        dr1["auditbyid"] = usrid;
                        dr1["auditdat"] = Date;
                        dr1["audittrmid"] = trmnid;
                        dr1["auditsession"] = session;

                        dt.Rows.Add(dr1);
                        ds1.Merge(dt);
                        ds1.Tables[0].TableName = "tbl1";
                        audit = ds1.GetXml();

                    }

                    else
                    {

                        xmlSR = new System.IO.StringReader(audit);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["auditbyid"] = usrid;
                        ds1.Tables[0].Rows[0]["auditdat"] = Date;
                        ds1.Tables[0].Rows[0]["audittrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["auditsession"] = session;

                        audit = ds1.GetXml();
                    }

                    break;


            }


            return audit;

        }
        //private string GetReqApproval(string audit="", string mordoc = "")
        //{



        //    string comcod = this.GetCompCode();
        //    string type = this.Request.QueryString["Type"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string usrid = hst["usrid"].ToString();
        //    string trmnid = hst["compname"].ToString();
        //    string session = hst["session"].ToString();
        //    string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

        //    DataSet ds1 = new DataSet("ds1");
        //    System.IO.StringReader xmlSR;

        //    switch (comcod)
        //    {
        //        default:
        //            break;
        //    }
        //    if (audit == "")
        //    {


        //        this.CreateDataTable();
        //        DataTable dt = (DataTable)ViewState["tblapproval"];
        //        DataRow dr1 = dt.NewRow();

        //        dr1["auditbyid"] = usrid;
        //        dr1["auditdat"] = Date;
        //        dr1["audittrmid"] = trmnid;
        //        dr1["auditsession"] = session;

        //        dt.Rows.Add(dr1);
        //        ds1.Merge(dt);
        //        ds1.Tables[0].TableName = "tbl1";
        //        audit = ds1.GetXml();

        //    }

        //    else
        //    {

        //        xmlSR = new System.IO.StringReader(audit);
        //        ds1.ReadXml(xmlSR);
        //        ds1.Tables[0].TableName = "tbl1";
        //        ds1.Tables[0].Rows[0]["auditbyid"] = usrid;
        //        ds1.Tables[0].Rows[0]["auditdat"] = Date;
        //        ds1.Tables[0].Rows[0]["audittrmid"] = trmnid;
        //        ds1.Tables[0].Rows[0]["auditsession"] = session;

        //        audit = ds1.GetXml();
        //    }

        //    return audit;

        //}

        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            string msg = "";
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
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
            string curdate = this.txtCurTransDate.Text.ToString().Trim();

            //if (ddlPrevISSList.Items.Count == 0)
            //    this.GetMatTrns();

            DataRow[] dr2 = dt.Select("qty=0.00");
            DataRow[] dr3 = dt.Select("rate=0.00");


            if (dr2.Length > 0)
            {
                msg = "Please Fillup Qtuantity  ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            if(dr3.Length > 0)
            {
                msg = "You Can not Save Without Rate";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            //if (ddlPrevISSList.Items.Count == 0)
            //    this.GetMatTrns();

            //string transno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

            string Refno = this.txtrefno.Text.ToString();




            switch (comcod)
            {
                case "3340":
                case "3338":
                    break;

              default:
                    if (this.Request.QueryString["Type"] == "Entry")
                    {
                        if (Refno.Length == 0)
                        {
                            msg = "Ref. No. Should Not Be Empty";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                            return;
                        }
                        DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "CHECKEDDUPREFNO", Refno, "", "", "", "", "", "", "", "");
                        if (ds3.Tables[0].Rows.Count == 0)
                            ;
                        else
                        {
                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                msg = "Found Duplicate Ref. No.";
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                                return;
                            }
                        }
                    }
                    break;

                //default:

                //    if (Refno.Length == 0)
                //    {
                //        msg = "Ref. No. Should Not Be Empty";
                //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                //        return;
                //    }
                //    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "CHECKEDDUPREFNO", Refno, "", "", "", "", "", "", "", "");
                //    if (ds2.Tables[0].Rows.Count == 0)
                //        ;

                //    else
                //    {
                //        if (ds2.Tables[0].Rows.Count > 0)
                //        {
                //            msg = "Found Duplicate Ref. No.";
                //            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                //            return;
                //        }
                        
                //    }
                //    break;
            }



            switch (comcod)
            {
                case "3330":
                    foreach (DataRow dr in dt.Rows)
                    {
                        double balqty = Convert.ToDouble(dr["balqty"]);
                        double mtrfqty = Convert.ToDouble(dr["mtrfqty"]);
                        double qty = Convert.ToDouble(dr["qty"]);
                        if (mtrfqty > 0)
                        {
                            if (mtrfqty < qty)
                            {
                                msg = "Not Within the Budget";
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                                break;
                            }
                        }
                        else if (balqty < qty)
                        {
                            msg = "Not Within the Budget";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                            break;
                        }
                    }
                    break;
            }


            if (ddlPrevISSList.Items.Count == 0)
                this.GetMatTrns();

            string transno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

            string fromprj = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim();
            string narration = this.txtNarr.Text.Trim();

            foreach (DataRow dr in dt.Rows)
            {

                string trsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string tqty = dr["qty"].ToString().Trim();
                string trate = dr["rate"].ToString().Trim();
                string tamt = dr["amt"].ToString().Trim();
                string reqno = dr["reqno"].ToString().Trim();
                string mtreqno = dr["mtreqno"].ToString().Trim();
                string gatepno = dr["getpno"].ToString().Trim();

                string appxml = dr["audit"].ToString();
                string audit = GetReqApproval(appxml); // todo check audit or not

                bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_03", "UpdateTransferInf", transno, fromprj, toprj, trsircode,
                    spcfcod, tqty, trate, tamt, curdate, Refno, PostedByid, Posttrmid, PostSession, Posteddat, reqno, mtreqno, gatepno, narration, audit, "", "", "", "");
                if (!result)
                {
                    msg = "Update Failed .. !";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
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

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            this.txtCurTransDate.Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Transfer";
                string eventdesc = "Update New QTY & RATE";
                string eventdesc2 = "From " + this.ddlprjlistfrom.SelectedItem.ToString() + " To " + this.ddlprjlistto.SelectedItem.ToString() + " - " + transno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.grvacc.Columns[6].FooterText.Length > 0)
                this.grvacc.Columns[6].FooterText = "";
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.pnlgrd.Visible = !(this.chkGatePass.Checked);
                this.pnlGatePass.Visible = (this.chkGatePass.Checked);
                this.ddlGatePass.Enabled = false;
                this.lblddlProjectFrom.Visible = true;
                this.lblddlProjectTo.Visible = true;
                this.ddlprjlistfrom.Visible = false;
                this.ddlprjlistto.Visible = false;
                // this.lbtnPrevTransList.Visible = true;

                this.ImgbtnPreList.Visible = false;
                this.lblPre.Visible = false;
                this.ddlPrevISSList.Visible = false;
                this.txtRefNo1.Visible = false;

                this.chkGatePass.Visible = false;
                this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
                this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;
                this.GetMatTransfer();
                this.ImgbtnFindRes_Click(null, null);
            }
            else
            {

                this.ddlprjlistfrom.Visible = true;
                this.ddlprjlistto.Visible = true;
                this.ImgbtnPreList.Visible = true; ;
                this.lblPre.Visible = true;
                this.txtRefNo1.Visible = true;
                //this.lbtnPrevTransList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.lblddlProjectFrom.Visible = false;
                this.lblddlProjectTo.Visible = false;
                this.txtCurTransDate.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.Last_trn_no();
                this.pnlgrd.Visible = false;
                this.chkGatePass.Visible = true;
                this.chkGatePass.Checked = false;
                this.chkGatePass_CheckedChanged(null, null);
                this.pnlGatePass.Visible = false;
                this.ddlGatePass.Enabled = true;
                this.txtrefno.Text = "";
                this.txtNarr.Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.lblVoucherNo.Text = "";
                lbtnOk.Text = "Ok";
                this.ddlPrevISSList.Items.Clear();
                this.ddlGatePass.Items.Clear();
                this.ddlreslist.Items.Clear();
                this.ddlreslistgp.Items.Clear();
                this.ddlResSpcfgp.Items.Clear();

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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "PrevTransferInfo", mTRNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];

            if (this.ddlGatePass.Items.Count > 0)
            {
                string gatepno = this.ddlGatePass.SelectedValue.ToString();
                DataTable dtres = (DataTable)ViewState["tblgatepno"];
                DataRow[] dr = dtres.Select("getpno='" + gatepno + "'");
                this.ddlprjlistfrom.SelectedValue = dr[0]["tfpactcode"].ToString();
                this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
                this.ddlprjlistfrom_SelectedIndexChanged(null, null);
                this.ddlprjlistto.SelectedValue = dr[0]["ttpactcode"].ToString();
                this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;

            }
            if (mTRNNo == "NEWTRNS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LastTransferNo", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurTransNo1.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(0, 6);
                this.txtCurTransNo2.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim().Substring(6);
                return;
            }

            this.pnlgrd.Visible = ds1.Tables[0].Rows[0]["getpno"].ToString().Trim().Length > 0 ? false : true;
            this.ddlprjlistfrom.SelectedValue = ds1.Tables[1].Rows[0]["tfpactcode"].ToString();
            this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);
            this.ddlprjlistto.SelectedValue = ds1.Tables[1].Rows[0]["ttpactcode"].ToString();
            this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;

            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["date"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.txtNarr.Text = ds1.Tables[1].Rows[0]["narration"].ToString();
            this.lblCurTransNo1.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds1.Tables[1].Rows[0]["trnno1"].ToString().Trim().Substring(6);
            this.lblVoucherNo.Text = ds1.Tables[1].Rows[0]["vounum"].ToString().Trim();
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = (DataTable)ViewState["tblmattrns"];
            this.grvacc.DataBind();
            switch (GetCompCode())
            {
                case "3370":
                    this.grvacc.Columns[11].Visible = false;
                    this.grvacc.Columns[12].Visible = false;
                    break;
                default:
                    this.grvacc.Columns[11].Visible = true;
                    this.grvacc.Columns[12].Visible = true;
                    break;
            }

            this.grvacc.Columns[1].Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
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
        //protected void lbtnPrevTransList_Click(object sender, EventArgs e)
        //{
        //    this.Load_Prev_Trans_List();
        //}
        protected void Load_Prev_Trans_List()
        {

            string comcod = this.GetCompCode();
            string curdate = this.txtCurTransDate.Text.ToString().Trim();
            string refno = (this.Request.QueryString["genno"].Length > 0 ? this.Request.QueryString["genno"].ToString() : ("%" + this.txtRefNo1.Text.Trim())) + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetPrevTrnsList", curdate, refno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.DataTextField = "trnno1";
            this.ddlPrevISSList.DataValueField = "trnno";
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


            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "5101":
                case "3330":
                    // case "3101":
                    this.PrintMatTransferBridge();
                    break;

                case "3101":
                case "1205":
                case "3351":
                case "3352":
                    this.PrintMatTransferP2P();
                    break;
                case "3370":
                    PrintMatTransferGenCPDL();
                    break;
                default:
                    PrintMatTransferGen();
                    break;

            }
        }


        private void PrintMatTransferBridge()
        {
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
            string transno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + this.txtCurTransDate.Text.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPADDAOTRNINFO", transno, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string desig = ds1.Tables[0].Rows[0]["usrnam"].ToString() + "," + ds1.Tables[0].Rows[0]["deg"].ToString() + "\n" + Convert.ToDateTime(ds1.Tables[0].Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptMatTransReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMatTransferRec", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", ds1.Tables[0].Rows[0]["ttpactdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtProToAddress", ds1.Tables[0].Rows[0]["ttpactadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txttrnsrefno", this.txtrefno.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txttrnsdate", Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("trnno", this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtmtrref", ds1.Tables[0].Rows[0]["mtrref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtdeg", desig));
            Rpt1.SetParameters(new ReportParameter("txtmtrdat", Convert.ToDateTime(ds1.Tables[0].Rows[0]["mtrdat"]).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("txttoProject", ds1.Tables[0].Rows[0]["tfpactdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txttoprojectadd", ds1.Tables[0].Rows[0]["tfpactadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtgatepref", ds1.Tables[0].Rows[0]["getpref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtgpdat", Convert.ToDateTime(ds1.Tables[0].Rows[0]["getpdat"]).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtNarr.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " TRANSFER MATERIAL RECEIVING REPORT (TMRR)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //ReportDocument rptFassttran = new RealERPRPT.R_12_Inv.RptMatTransferRec();
            //TextObject rptCname = rptFassttran.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptCAdd = rptFassttran.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //rptCAdd.Text = comadd;

            //TextObject rptProjectNameft = rptFassttran.ReportDefinition.ReportObjects["ProjectNamef"] as TextObject;
            //rptProjectNameft.Text = ds1.Tables[0].Rows[0]["ttpactdesc"].ToString();

            //TextObject txtProToAddress = rptFassttran.ReportDefinition.ReportObjects["txtProToAddress"] as TextObject;
            //txtProToAddress.Text = ds1.Tables[0].Rows[0]["ttpactadd"].ToString();


            //TextObject txttrnsrefno = rptFassttran.ReportDefinition.ReportObjects["txttrnsrefno"] as TextObject;
            //txttrnsrefno.Text = this.txtrefno.Text.Trim();
            //TextObject txttrnsdate = rptFassttran.ReportDefinition.ReportObjects["txttrnsdate"] as TextObject;
            //txttrnsdate.Text = Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd.MM.yyyy");


            //TextObject rpttxtTransNo = rptFassttran.ReportDefinition.ReportObjects["trnno"] as TextObject;
            //rpttxtTransNo.Text = this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim();

            //TextObject txtmtrref = rptFassttran.ReportDefinition.ReportObjects["txtmtrref"] as TextObject;
            //txtmtrref.Text = ds1.Tables[0].Rows[0]["mtrref"].ToString();
            //TextObject txtdeg = rptFassttran.ReportDefinition.ReportObjects["txtdeg"] as TextObject;
            //txtdeg.Text = ds1.Tables[0].Rows[0]["usrnam"].ToString() + "," + ds1.Tables[0].Rows[0]["deg"].ToString() + "\n" + Convert.ToDateTime(ds1.Tables[0].Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");

            //TextObject txtmtrdat = rptFassttran.ReportDefinition.ReportObjects["txtmtrdat"] as TextObject;
            //txtmtrdat.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["mtrdat"]).ToString("dd.MM.yyyy");
            //TextObject rptProjectNameto = rptFassttran.ReportDefinition.ReportObjects["txttoProject"] as TextObject;
            //rptProjectNameto.Text = ds1.Tables[0].Rows[0]["tfpactdesc"].ToString();

            //TextObject txttoprojectadd = rptFassttran.ReportDefinition.ReportObjects["txttoprojectadd"] as TextObject;
            //txttoprojectadd.Text = ds1.Tables[0].Rows[0]["tfpactadd"].ToString();


            //TextObject txtgatepref = rptFassttran.ReportDefinition.ReportObjects["txtgatepref"] as TextObject;
            //txtgatepref.Text = ds1.Tables[0].Rows[0]["getpref"].ToString();

            //TextObject txtgpdat = rptFassttran.ReportDefinition.ReportObjects["txtgpdat"] as TextObject;
            //txtgpdat.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["getpdat"]).ToString("dd.MM.yyyy");


            //TextObject txtnarration = rptFassttran.ReportDefinition.ReportObjects["txtnarration"] as TextObject;
            //txtnarration.Text = this.txtNarr.Text.Trim();





            //TextObject txtuserinfo = rptFassttran.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Transfer";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy"); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptFassttran.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptFassttran.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptFassttran;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintMatTransferGen()
        {

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


            string prjfrm = this.ddlprjlistfrom.SelectedItem.Text.Trim().Substring(13);
            string prjto = this.ddlprjlistto.SelectedItem.Text.Trim().Substring(13);

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptMatTransReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMaterialTrnsfer", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", "Transfer From: " + prjfrm));
            Rpt1.SetParameters(new ReportParameter("txtProjectNameto", "Transfer To: " + prjto));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("txttrnsno", "Transfer No: " + this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtNarr.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Transfer Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //TextObject rptCname = rptFassttran.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptProjectNameft = rptFassttran.ReportDefinition.ReportObjects["ProjectNamef"] as TextObject;
            //rptProjectNameft.Text = "Transfer From: " + prjfrm;
            //TextObject rptProjectNameto = rptFassttran.ReportDefinition.ReportObjects["ProjectNamet"] as TextObject;
            //rptProjectNameto.Text = "Transfer To: " + prjto;

            //TextObject rptdate = rptFassttran.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy");
            //TextObject rpttrnno = rptFassttran.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
            //rpttrnno.Text = "Transfer No: " + this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim();
            //TextObject txtuserinfo = rptFassttran.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Transfer";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy"); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptFassttran.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptFassttran.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptFassttran;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintMatTransferGenCPDL()
        {

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


            string prjfrm = this.ddlprjlistfrom.SelectedItem.Text.Trim().Substring(13);
            string prjto = this.ddlprjlistto.SelectedItem.Text.Trim().Substring(13);

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptMatTransReqcp>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMaterialTrnsferCPDL", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", "Transfer From: " + prjfrm));
            Rpt1.SetParameters(new ReportParameter("txtProjectNameto", "Transfer To: " + prjto));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("txttrnsno", "Transfer No: " + this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtNarr.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Transfer Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //TextObject rptCname = rptFassttran.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptProjectNameft = rptFassttran.ReportDefinition.ReportObjects["ProjectNamef"] as TextObject;
            //rptProjectNameft.Text = "Transfer From: " + prjfrm;
            //TextObject rptProjectNameto = rptFassttran.ReportDefinition.ReportObjects["ProjectNamet"] as TextObject;
            //rptProjectNameto.Text = "Transfer To: " + prjto;

            //TextObject rptdate = rptFassttran.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy");
            //TextObject rpttrnno = rptFassttran.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
            //rpttrnno.Text = "Transfer No: " + this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim();
            //TextObject txtuserinfo = rptFassttran.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Transfer";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy"); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptFassttran.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptFassttran.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptFassttran;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintMatTransferP2P()
        {

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

            string transno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + this.txtCurTransDate.Text.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "RPTMRTREQOAUDIT", transno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[0];
            DataTable dt2 = ds1.Tables[1];

            string reqsign = dt2.Rows[0]["trnusrnam"].ToString() + "\n" + dt2.Rows[0]["trndeg"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["transdat"]).Year == "1900" ? "" : Convert.ToDateTime(dt2.Rows[0]["transdat"]).ToString("dd-MMM-yyyy");
            string aprvsign = dt2.Rows[0]["gpausrnam"].ToString() + "\n" + dt2.Rows[0]["gpadeg"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["gpadat"]).Year == "1900" ? "" : Convert.ToDateTime(dt2.Rows[0]["gpadat"]).ToString("dd-MMM-yyyy");
            string gpasign = dt2.Rows[0]["mtraprvusrnam"].ToString() + "\n" + dt2.Rows[0]["mtraprvdeg"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["mtraprvdat"]).Year == "1900" ? "" : Convert.ToDateTime(dt2.Rows[0]["mtraprvdat"]).ToString("dd-MMM-yyyy");
            string recvsign = dt2.Rows[0]["mtrequsrnam"].ToString() + "\n" + dt2.Rows[0]["mtreqdeg"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["mtreqdat"]).Year == "1900" ? "" : Convert.ToDateTime(dt2.Rows[0]["mtreqdat"]).ToString("dd-MMM-yyyy");

            string prjfrm = this.ddlprjlistfrom.SelectedItem.Text.Trim().Substring(13);
            string prjto = this.ddlprjlistto.SelectedItem.Text.Trim().Substring(13);

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptMatTransReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMaterialTrnsferP2P", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", "Transfer From: " + prjfrm));
            Rpt1.SetParameters(new ReportParameter("txtProjectNameto", "Transfer To: " + prjto));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("txttrnsno", "Transfer No: " + this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtNarr.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Transfer Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("reqsign", reqsign));
            Rpt1.SetParameters(new ReportParameter("aprvsign", aprvsign));
            Rpt1.SetParameters(new ReportParameter("gpasign", gpasign));
            Rpt1.SetParameters(new ReportParameter("recvsign", recvsign));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void printMtrRecevFAudit()
        {
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

            string transno = this.Request.QueryString["genno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "RPTMRTREQOAUDIT", transno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[0];
            DataTable dt2 = ds1.Tables[1];

            string reqsign = dt2.Rows[0]["trnusrnam"].ToString() + "\n" + dt2.Rows[0]["trndeg"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["transdat"]).ToString("dd-MMM-yyyy");
            string aprvsign = dt2.Rows[0]["gpausrnam"].ToString() + "\n" + dt2.Rows[0]["gpadeg"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["gpadat"]).ToString("dd-MMM-yyyy");
            string gpasign = dt2.Rows[0]["mtraprvusrnam"].ToString() + "\n" + dt2.Rows[0]["mtraprvdeg"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["mtraprvdat"]).ToString("dd-MMM-yyyy");
            string recvsign = dt2.Rows[0]["mtrequsrnam"].ToString() + "\n" + dt2.Rows[0]["mtreqdeg"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["mtreqdat"]).ToString("dd-MMM-yyyy");

            string prjfrm = this.ddlprjlistfrom.SelectedItem.Text.Trim().Substring(13);
            string prjto = this.ddlprjlistto.SelectedItem.Text.Trim().Substring(13);

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptMatTransReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMaterialTrnsferP2P", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", "Transfer From: " + prjfrm));
            Rpt1.SetParameters(new ReportParameter("txtProjectNameto", "Transfer To: " + prjto));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("txttrnsno", "Transfer No: " + this.lblCurTransNo1.Text.Trim() + this.txtCurTransNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtNarr.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Transfer Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("reqsign", reqsign));
            Rpt1.SetParameters(new ReportParameter("aprvsign", aprvsign));
            Rpt1.SetParameters(new ReportParameter("gpasign", gpasign));
            Rpt1.SetParameters(new ReportParameter("recvsign", recvsign));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
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
            string FrmPrjCode = this.ddlprjlistfrom.SelectedValue.ToString();
            string ToPrjCode = this.ddlprjlistto.SelectedValue.ToString();
            string MatCode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvMatCode")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELMATTRANSFER", mISUNO, FrmPrjCode, ToPrjCode, MatCode, "", "", "", "", "", "", "", "", "", "", "");

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
        protected void chkGatePass_CheckedChanged(object sender, EventArgs e)
        {
            this.lblGatePassNo.Visible = this.chkGatePass.Checked;
            this.txtsrchGatePass.Visible = this.chkGatePass.Checked;
            this.lbtnGatePassNo.Visible = this.chkGatePass.Checked;
            this.ddlGatePass.Visible = this.chkGatePass.Checked;
        }

        protected void lbtnGatePassNo_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblgatepinfo");
            ViewState.Remove("tblRes");
            string comcod = this.GetCompCode();

            string SerchText = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" + this.txtsrchGatePass.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";

            string CurDate1 = this.txtCurTransDate.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETMTREQGPASSLIST", CurDate1, SerchText, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            ViewState["tblgatepinfo"] = ds1.Tables[0];
            ViewState["tblRes"] = ds1.Tables[1];
            ViewState["tblgatepno"] = ds1.Tables[2];
            this.ddlGatePass.DataTextField = "textfield";
            this.ddlGatePass.DataValueField = "valuefiled";
            this.ddlGatePass.DataSource = ds1.Tables[2];
            this.ddlGatePass.DataBind();

        }
        protected void lbtnFindResgp_Click(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblRes"];
            string gatepno = this.ddlGatePass.SelectedValue.ToString();
            DataView dv = dtres.DefaultView;
            dv.RowFilter = "getpno in ('" + gatepno + "')";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";
            DataTable dtd = dv.ToTable();
            this.ddlreslistgp.DataTextField = "rsirdesc";
            this.ddlreslistgp.DataValueField = "rsircode";
            this.ddlreslistgp.DataSource = dv.ToTable();
            this.ddlreslistgp.DataBind();
            this.ddlreslistgp_SelectedIndexChanged(null, null);
        }
        protected void lnkselectgp_Click(object sender, EventArgs e)
        {
            string resSpcf = this.ddlResSpcfgp.SelectedValue.ToString();
            if (resSpcf.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Resource List";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string gatepno = resSpcf.Substring(0, 14);
            string rescode = resSpcf.Substring(14, 12);
            string spcfcod = resSpcf.Substring(26, 12);

            DataTable dt = (DataTable)ViewState["tblmattrns"];
            DataTable dt1 = (DataTable)ViewState["tblgatepinfo"];
            DataRow[] drgp = dt1.Select("getpno = '" + gatepno + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            DataRow[] dr1 = dt.Select("getpno = '" + gatepno + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");

            if (dr1.Length == 0)
            {
                DataRow dr = dt.NewRow();
                dr["mtreqno"] = drgp[0]["mtreqno"];
                dr["mtrref"] = drgp[0]["mtrref"];
                dr["getpno"] = drgp[0]["getpno"];
                dr["getpref"] = drgp[0]["getpref"];

                dr["rsircode"] = drgp[0]["rsircode"];
                dr["spcfcod"] = drgp[0]["spcfcod"];
                dr["resdesc"] = drgp[0]["rsirdesc"];
                dr["spcfdesc"] = drgp[0]["spcfdesc"];
                dr["sirunit"] = drgp[0]["rsirunit"];
                dr["mtrfqty"] = drgp[0]["mtrfqty"];
                dr["balqty"] = drgp[0]["getpqty"];
                dr["qty"] = drgp[0]["getpqty"];
                dr["rate"] = drgp[0]["rate"];
                dr["amt"] = Convert.ToDouble(drgp[0]["getpqty"]) * Convert.ToDouble(drgp[0]["rate"]);   // drgp[0]["getpamt"];
                dr["reqno"] = "";
                dt.Rows.Add(dr);
            }
            ViewState["tblmattrns"] = dt;
            this.Data_Bind();

        }

        protected void lnkselectgpAll_Click(object sender, EventArgs e)
        {

            string resSpcf1 = this.ddlResSpcfgp.SelectedValue.ToString();
            if (resSpcf1.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select  Resource List";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string gatepno = resSpcf1.Substring(0, 14);

            DataTable dt = (DataTable)ViewState["tblmattrns"];
            DataTable dt1 = (DataTable)ViewState["tblgatepinfo"];
            // DataRow[] drgp = dt1.Select("getpno = '" + gatepno + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            DataRow[] dr1 = dt.Select("getpno = '" + gatepno + "'");
            DataView dvg = dt1.DefaultView;
            dvg.RowFilter = ("getpno = '" + gatepno + "'");
            DataTable dtg = dvg.ToTable();

            if (dr1.Length == 0)
            {


                foreach (DataRow dr2 in dtg.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["mtreqno"] = dr2["mtreqno"];
                    dr["mtrref"] = dr2["mtrref"];
                    dr["getpno"] = dr2["getpno"];
                    dr["getpref"] = dr2["getpref"];
                    dr["rsircode"] = dr2["rsircode"];
                    dr["spcfcod"] = dr2["spcfcod"];
                    dr["resdesc"] = dr2["rsirdesc"];
                    dr["spcfdesc"] = dr2["spcfdesc"];
                    dr["sirunit"] = dr2["rsirunit"];
                    dr["balqty"] = dr2["getpqty"];
                    dr["mtrfqty"] = dr2["mtrfqty"];
                    dr["qty"] = dr2["getpqty"];
                    dr["rate"] = dr2["rate"];
                    dr["amt"] = Convert.ToDouble(dr2["rate"]) * Convert.ToDouble(dr2["getpqty"]); //dr2["getpamt"];
                    dr["reqno"] = "";
                    dt.Rows.Add(dr);
                }


            }
            ViewState["tblmattrns"] = dt;
            this.Data_Bind();
        }


        protected void ddlreslistgp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblgatepinfo"];
            string gatepno = this.ddlGatePass.SelectedValue.ToString();
            string rsircode = this.ddlreslistgp.SelectedValue.ToString();
            DataView dv = dtres.DefaultView;

            dv.RowFilter = "getpno='" + gatepno + "' and  rsircode='" + rsircode + "'";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";

            this.ddlResSpcfgp.DataTextField = "textfield";
            this.ddlResSpcfgp.DataValueField = "valuefiled";
            this.ddlResSpcfgp.DataSource = dv.ToTable();
            this.ddlResSpcfgp.DataBind();

        }


        protected void ImgbtnPreList_Click(object sender, EventArgs e)
        {

            this.Load_Prev_Trans_List();

        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "3101": // ptl
                case "1205":
                case "3351":
                case "3352":
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtqty = (TextBox)e.Row.FindControl("txtqty");
                        TextBox txtrate = (TextBox)e.Row.FindControl("txtrate");
                        txtqty.ReadOnly = true;
                        txtrate.ReadOnly = true;
                    }
                    break;
                default:
                    break;
            }

        }
    }
}