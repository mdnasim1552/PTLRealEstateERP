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
namespace RealERPWEB.F_29_Fxt
{
    public partial class EntryFxtAssetITDept : System.Web.UI.Page
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
                //----------------udate-20210508---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Fixed Asset Information";
                this.GetDepartmentList();
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
                this.GetDepartmentList();
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
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "GETLASTFXTINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxfxtno"].ToString();
                    this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxfxtno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxfxtno1"].ToString().Substring(6, 5);
                    this.ddlPrevISSList.DataTextField = "maxfxtno1";
                    this.ddlPrevISSList.DataValueField = "maxfxtno";
                    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                    this.ddlPrevISSList.DataBind();




                }
            }
        }
        private void GetDepartmentList()
        {

            string comcod = this.GetCompCode();
            this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string srchproject = "%" + this.txtsrchproject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "GETDEPTNAME", srchproject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddldeptlist.DataTextField = "deptname";
            this.ddldeptlist.DataValueField = "deptcode";
            this.ddldeptlist.DataSource = ds1.Tables[0];
            this.ddldeptlist.DataBind();
            ds1.Dispose();


        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3330":
                case "3101":
                    this.PrintBridgeHolding();
                    break;
                default:
                    this.PrintGeneral();
                    break;

            }


        }



        private void PrintBridgeHolding()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string ProjectName = this.ddlprjlist.SelectedItem.Text.Substring(14);
            //string Issueno = this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim();
            //string date = this.txtCurISSDate.Text.Trim();
            //string narrationname = this.txtISSNarr.Text.Trim();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)ViewState["tblmatissue"];
            DataTable dt1 = (DataTable)ViewState["UserLog"];

            var list = dt.DataTableToList<RealEntity.C_12_Inv.RptMatIssue>();
            LocalReport Rpt1 = new LocalReport();



            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMatIssueBridge", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddldeptlist.SelectedItem.Text.Substring(14)));

            Rpt1.SetParameters(new ReportParameter("rpttxtref", "SMCR NO: " + this.txtRef.Text));
            Rpt1.SetParameters(new ReportParameter("rpttxtdate", "Date : " + this.txtCurISSDate.Text.Trim()));
            //Rpt1.SetParameters(new ReportParameter("txtsmcrno", "REF.DMIRF NO: " + this.txtsmcr.Text));
            Rpt1.SetParameters(new ReportParameter("rpttxtissueno", "Issue No : " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtdeg", dt1.Rows[0]["usrnam"].ToString() + "," + dt1.Rows[0]["deg"].ToString() + "\n" + Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy")));
            // Rpt1.SetParameters(new ReportParameter("narrationname", this.txtISSNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "STORE MATERIAL CONSUMPTION(SMCR)"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






            //DataTable dt = (DataTable)ViewState["tblmatissue"];
            //DataTable dt1 = (DataTable) ViewState["UserLog"];



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
            //string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string ProjectName = this.ddlprjlist.SelectedItem.Text.Substring(14);
            //string Issueno = this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim();
            //string date = this.txtCurISSDate.Text.Trim();
            //string narrationname = this.txtISSNarr.Text.Trim();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)ViewState["tblmatissue"];



            var list = dt.DataTableToList<RealEntity.C_12_Inv.RptMatIssue>();

            LocalReport Rpt1 = new LocalReport();



            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMatIssue", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddldeptlist.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("rpttxtissueno", "Issue No : " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rpttxtdate", "Date : " + this.txtCurISSDate.Text.Trim()));
            //Rpt1.SetParameters(new ReportParameter("narrationname", this.txtISSNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Matrial Issue"));
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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVMISSUELIST", CurDate1, genno, "", "", "", "", "", "", "");
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

                this.ddldeptlist.Visible = true;
                this.lblddlProject.Visible = false;
                this.txtCurISSDate.Enabled = true;
                this.lblCurISSNo1.Text = "FXT" + DateTime.Today.ToString("MM") + "-";
                this.txtCurISSNo2.Text = "";
                this.ddlMaterials.Items.Clear();
                this.txtISSNarr.Text = "";

                this.PnlRes.Visible = false;
                //this.PnlNarration.Visible = false;
                this.txtRef.Text = "";

                this.lbtnPrevISSList.Visible = false;
                this.ddlPrevISSList.Visible = false;
                this.grvissue.DataSource = null;
                this.grvissue.DataBind();
                return;
            }
            this.lbtnPrevISSList.Visible = false;
            this.ddlPrevISSList.Visible = false;
            //this.txtsmcr.Visible = false;
            this.lblddlProject.Text = this.ddldeptlist.SelectedItem.Text.Trim();
            this.ddldeptlist.Visible = false;//it will be used
            this.lblddlProject.Visible = true;
            this.PnlRes.Visible = true;
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Fxt_Info();
            this.ibtnSearchMaterisl_Click(null, null);

        }


        private void Get_Fxt_Info()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddldeptlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            string mISSNo = "NEWMISS";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                mISSNo = this.ddlPrevISSList.SelectedValue.ToString();
            }
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "GETFXTASST", mISSNo, CurDate1,
                         pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblmatissue"] = ds1.Tables[0];
            ViewState["UserLog"] = ds1.Tables[1];


            if (mISSNo == "NEWMISS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "GETLASTFXTINFO", CurDate1,
                       "", "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {


                    this.lblCurISSNo1.Text = ds1.Tables[0].Rows[0]["maxfxtno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds1.Tables[0].Rows[0]["maxfxtno1"].ToString().Substring(6, 5);

                }
                return;
            }



            this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["FXTASSTNO1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["FXTASSTNO1"].ToString().Substring(6, 5);
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["PURDATE"]).ToString("dd-MMM-yyyy");
            this.ddldeptlist.SelectedValue = ds1.Tables[1].Rows[0]["DEPTNO"].ToString();
            this.lblddlProject.Text = this.ddldeptlist.SelectedItem.Text.Trim();
            //this.txtISSNarr.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
            this.txtRef.Text = ds1.Tables[1].Rows[0]["refno"].ToString();



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


        private string CompBalConMat()
        {

            string comcod = this.GetCompCode();
            string conbal = "";
            switch (comcod)
            {
                case "3301":
                case "1301":
                    //case "3101":
                    conbal = "notcon";
                    break;

                default:
                    conbal = "GETMETERIALS";
                    break;


            }

            return conbal;

        }



        private void GetMaterials()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddldeptlist.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = this.txtSearchMaterials.Text.Trim() + "%";

            //string CallType = this.CompReceived();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "GETMETERIALS", SearchMat, "", "", "", "", "", "", "", "");
            Session["itemlist"] = ds1.Tables[0];

            if (ds1 == null)
                return;

            this.ddlMaterials.DataTextField = "rsirdesc";
            this.ddlMaterials.DataValueField = "rsircode";
            this.ddlMaterials.DataSource = ds1.Tables[0];
            this.ddlMaterials.DataBind();
            ds1.Dispose();
            //this.ddlMaterials_SelectedIndexChanged(null, null);

        }


        protected void grvissue_DataBind()
        {
            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.grvissue.DataSource = (DataTable)ViewState["tblmatissue"]; ;
            this.grvissue.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.grvissue.FooterRow.FindControl("lgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ? 0.00 :
                 dt.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void ddlMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.GetSpecification();
        }


        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string comcod = this.GetCompCode();
            string rsircode = this.ddlMaterials.SelectedValue.ToString().Trim();
            string fxtasstno = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

            DataTable dt = (DataTable)ViewState["tblmatissue"];
            DataRow[] dr = dt.Select("rsircode='" + rsircode + "'");
            //DataRow[] dr = dt.Select ("rsircode='" + rsircode + "'");



            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = comcod;
                dr1["fxtasstno"] = fxtasstno;
                dr1["deptno"] = this.ddldeptlist.SelectedValue.ToString();
                dr1["rsircode"] = this.ddlMaterials.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlMaterials.SelectedItem.Text.Trim();
                dr1["purdate"] = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
                dr1["qty"] = 0.00;
                dr1["rate"] = 0.00;
                dr1["amount"] = 0.0;
                // dr1["rmks"] = "";



                //dr1["rsirunit"] = (((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
                //dr1["balqty"] = ((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'"))[0]["bbgdqty"]).ToString();
                //dr1["isuqty"] = 0.00;
                //dr1["spcfcod"] = this.ddlSpecification.SelectedValue.ToString();
                //dr1["spcfdesc"] = this.ddlSpecification.SelectedItem.Text.Trim();
                //dr1["useoflocation"] = "";
                //dr1["remarks"] = "";
                dt.Rows.Add(dr1);

            }
            ViewState["tblmatissue"] = dt;
            this.grvissue_DataBind();


        }

        protected void lnkupdate_Click(object sender, EventArgs e)
        {


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

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblmatissue"];

            //DataRow[] dr = tbl2.Select("isuqty=0.00");

            //if (dr.Length > 0)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fillup Qtuantity Field ";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;
            //}

            string comcod = this.GetCompCode();
            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetPerMatIssu();
            }



            string mRef = this.txtRef.Text;
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string mISUDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();



            string deptno = this.ddldeptlist.SelectedValue.ToString().Trim();
            string refno = this.txtRef.Text.Trim();

            string narration = this.txtISSNarr.Text.Trim();

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string Rsircode = tbl2.Rows[i]["rsircode"].ToString();
                double qty = Convert.ToDouble(tbl2.Rows[i]["qty"].ToString());
                double amount = Convert.ToDouble(tbl2.Rows[i]["amount"].ToString());
                string date = Convert.ToDateTime(tbl2.Rows[i]["purdate"]).ToString("dd-MMM-yyyy");


                if (qty > 0)
                {

                    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "INSERTORUPDATEFXTASST", mISUNO, deptno,
                        Rsircode, date, refno, qty.ToString(), amount.ToString(), narration, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat);


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
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);

            this.txtCurISSDate.Enabled = false;
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Fixed asset information";
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
                double txtkqty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtqty")).Text.Trim());
                double txtRate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtRate")).Text.Trim());
                //string txtrmks = ((TextBox)this.grvissue.Rows[i].FindControl("txtrmks")).Text.Trim();


                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;
                dt.Rows[TblRowIndex]["qty"] = txtkqty;
                dt.Rows[TblRowIndex]["rate"] = txtRate;

                dt.Rows[TblRowIndex]["amount"] = txtkqty * txtRate;

                //dt.Rows[TblRowIndex]["RMKS"] = txtrmks;



            }
            ViewState["tblmatissue"] = dt;
        }




        protected void lbtFxtasstdelete_Click(object sender, EventArgs e)
        {
            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            string fxtasstno = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string deptno = this.ddldeptlist.SelectedValue.ToString();
            string rsircode = ((Label)this.grvissue.Rows[rownum].FindControl("lblrsircode")).Text.Trim();


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "DELETEFXTASSTNO",
                       fxtasstno, deptno, rsircode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[rownum].Delete();
            }

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Fail !!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmatissue");
            ViewState["tblmatissue"] = dv.ToTable();
            this.grvissue_DataBind();

        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblmatissue"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEMATISUEALL", mISUNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Delete  successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


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
        protected void lbkTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();

        }

    }
}
