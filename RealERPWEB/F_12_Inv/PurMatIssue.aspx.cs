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
namespace RealERPWEB.F_12_Inv
{

    public partial class PurMatIssue : System.Web.UI.Page
    {


        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Materials Issue Information";
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

        public string GetCompCode()
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
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTMISSUEINFO", mREQDAT,
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

                //else {
                //    this.ddlPrevISSList.DataTextField = "maxmisuno1";
                //    this.ddlPrevISSList.DataValueField = "maxmisuno";
                //    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                //    this.ddlPrevISSList.DataBind();
                //}
            }
        }
        private void GetProjectList()
        {

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string srchproject = "%" + this.txtsrchproject.Text.Trim() + "%";
            string isComplain = Request.QueryString["Type"].ToString()=="ComplainMgt" ? "Complain" : "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST01", srchproject, isComplain, userid, "", "", "", "", "", "");
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
            Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlprjlist.SelectedItem.Text.Substring(14)));

            Rpt1.SetParameters(new ReportParameter("rpttxtref", "SMCR NO: " + this.txtMIsuRef.Text));
            Rpt1.SetParameters(new ReportParameter("rpttxtdate", "Date : " + this.txtCurISSDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtsmcrno", "REF.DMIRF NO: " + this.txtsmcr.Text));
            Rpt1.SetParameters(new ReportParameter("rpttxtissueno", "Issue No : " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtdeg", dt1.Rows[0]["usrnam"].ToString() + "," + dt1.Rows[0]["deg"].ToString() + "\n" + Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtISSNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "STORE MATERIAL CONSUMPTION(SMCR)"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






            //DataTable dt = (DataTable)ViewState["tblmatissue"];
            //DataTable dt1 = (DataTable) ViewState["UserLog"];


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd1 = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptMatIssueBridge();



            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtrptcomadd = rptstk.ReportDefinition.ReportObjects["txtrptcomadd"] as TextObject;
            //txtrptcomadd.Text = comadd1;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text = "PROJECT NAME: " + this.ddlprjlist.SelectedItem.Text.Substring(14);

            //TextObject rpttxtMatissueNo = rptstk.ReportDefinition.ReportObjects["Issueno"] as TextObject;
            //rpttxtMatissueNo.Text = this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim();

            //TextObject rpttxtissueno = rptstk.ReportDefinition.ReportObjects["txtsmcrno"] as TextObject;
            //rpttxtissueno.Text = this.txtsmcr.Text;

            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["rpttxtdate"] as TextObject;
            //rpttxtdate.Text = this.txtCurISSDate.Text.Trim();


            //TextObject rpttxtref = rptstk.ReportDefinition.ReportObjects["rpttxtref"] as TextObject;
            //rpttxtref.Text = this.txtMIsuRef.Text.Trim();
            //TextObject txtdeg = rptstk.ReportDefinition.ReportObjects["txtdeg"] as TextObject;
            //txtdeg.Text = dt1.Rows[0]["usrnam"].ToString() +  "," + dt1.Rows[0]["deg"].ToString() + "\n" +Convert.ToDateTime( dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");

            //TextObject narrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //narrationname.Text = this.txtISSNarr.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Issue Information";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "Materails Name " + this.ddlMaterials.SelectedItem.ToString() + "- " + "Issue No: " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim(); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlprjlist.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("rpttxtissueno", "Issue No : " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rpttxtdate", "Date : " + this.txtCurISSDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtISSNarr.Text.Trim()));
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
            string pactcodetype = Request.QueryString["Type"].ToString()=="ComplainMgt"?"1561":"16";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVMISSUELIST", CurDate1, genno, pactcodetype, "", "", "", "", "", "");
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

                this.ddlprjlist.Visible = true;
                this.lblddlProject.Visible = false;
                this.txtCurISSDate.Enabled = true;
                this.lblCurISSNo1.Text = "ISU" + DateTime.Today.ToString("MM") + "-";
                this.txtCurISSNo2.Text = "";
                this.ddlMaterials.Items.Clear();
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
            //this.txtsmcr.Visible = false;
            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
            this.ddlprjlist.Visible = false;//it will be used
            this.lblddlProject.Visible = true;
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
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURMISSUEINFO", mISSNo, CurDate1,
                         pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblmatissue"] = ds1.Tables[0];
            ViewState["UserLog"] = ds1.Tables[1];


            if (mISSNo == "NEWMISS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTMISSUEINFO", CurDate1,
                       "", "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {


                    this.lblCurISSNo1.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);

                }
                return;
            }



            this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["isuno1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["isuno1"].ToString().Substring(6, 5);
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
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
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = this.txtSearchMaterials.Text.Trim() + "%";
            string balcon = this.CompBalConMat();
            //string CallType = this.CompReceived();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETMETERIALS", pactcode, date, SearchMat, balcon, "", "", "", "", "");
            Session["itemlist"] = ds1.Tables[0];
            Session["specification"] = ds1.Tables[2];
            if (ds1 == null)
                return;

            this.ddlMaterials.DataTextField = "rsirdesc";
            this.ddlMaterials.DataValueField = "rsircode";
            this.ddlMaterials.DataSource = ds1.Tables[1];
            this.ddlMaterials.DataBind();
            ds1.Dispose();
            this.ddlMaterials_SelectedIndexChanged(null, null);

        }


        protected void grvissue_DataBind()
        {
            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.grvissue.DataSource = (DataTable)ViewState["tblmatissue"]; ;
            this.grvissue.DataBind();
        }

        protected void ddlMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();
        }

        private void GetSpecification()
        {

            ////string mResCode = this.ddlMaterials.SelectedValue.ToString();

            ////DataTable dt = ((DataTable)Session["itemlist"]).Copy();
            ////DataView dv = dt.DefaultView;

            ////dv.RowFilter = ("rsircode='" + mResCode + "'");

            ////this.ddlSpecification.DataTextField = "spcfdesc";
            ////this.ddlSpecification.DataValueField = "spcfcod";
            ////this.ddlSpecification.DataSource = dv.ToTable();
            ////this.ddlSpecification.DataBind();


            /////
            //string mResCode = this.ddlMaterials.SelectedValue.ToString().Substring(0,9);
            string mResCode = this.ddlMaterials.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["specification"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "'";
            // dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = dv1.ToTable();
            this.ddlSpecification.DataBind();



            //DataTable tbl1 = (DataTable)Session["specification"];
            //DataView dv1 = tbl1.DefaultView;
            //dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
            //this.ddlSpecification.DataTextField = "spcfdesc";
            //this.ddlSpecification.DataValueField = "spcfcod";
            //this.ddlSpecification.DataSource = dv1.ToTable();
            //this.ddlSpecification.DataBind();
        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string rsircode = this.ddlMaterials.SelectedValue.ToString().Trim();
            string specification = this.ddlSpecification.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            DataRow[] dr = dt.Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'");
            //DataRow[] dr = dt.Select ("rsircode='" + rsircode + "'");

            DataTable dt1 = (DataTable)Session["itemlist"];

            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["rsircode"] = this.ddlMaterials.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlMaterials.SelectedItem.Text.Trim();
                dr1["rsirunit"] = (((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
                dr1["balqty"] = ((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'"))[0]["bbgdqty"]).ToString();
                dr1["isuqty"] = 0.00;
                dr1["spcfcod"] = this.ddlSpecification.SelectedValue.ToString();
                dr1["spcfdesc"] = this.ddlSpecification.SelectedItem.Text.Trim();
                dr1["useoflocation"] = "";
                dr1["remarks"] = "";
                dt.Rows.Add(dr1);

            }
            ViewState["tblmatissue"] = dt;
            this.grvissue_DataBind();


        }
        protected void lbtnSelectReaSpesAll_Click(object sender, EventArgs e)
        {






            //////////
            DataTable dt = (DataTable)ViewState["tblmatissue"];

            DataTable dt2 = ((DataTable)Session["specification"]).Copy();


            // string rsircode = this.ddlMaterials.SelectedValue.ToString();
            // string mSpcfCod = this.ddlSpecification.SelectedValue.ToString();


            // DataTable dt3 = new DataTable(); 
            //for (int i = 0; i < dt2.Rows.Count; i++)
            for (int i = 0; i < this.ddlMaterials.Items.Count; i++)
            {

                string rsircode = this.ddlMaterials.Items[i].Value.ToString();
                string msmcfcod = this.ddlMaterials.Items[i].Value.ToString().Substring(0, 9);
                // string msmcfcod = this.ddlMaterials.Items[i].Value.ToString().Substring(0, 12);


                DataView dv = dt2.DefaultView;
                // dv.RowFilter = ("mspcfcod='" + msmcfcod + "'");


                dv.RowFilter = ("mspcfcod='" + msmcfcod + "' or mspcfcod='000000000'");
                DataTable dt3 = dv.ToTable();


                for (int j = 0; j < dt3.Rows.Count; j++)
                {
                    //string mSpcfCod=dt3.Select("flrcod='" + floorcode + "'  and itemcode='" + selecteditem + "'");


                    string mSpcfCod = dt3.Rows[j]["spcfcod"].ToString();


                    DataRow[] dr2 = dt.Select("rsircode = '" + rsircode + "' and  spcfcod='" + mSpcfCod + "'");
                    if (dr2.Length == 0)
                    {

                        DataRow dr1 = dt.NewRow();

                        dr1["rsircode"] = rsircode;
                        dr1["rsirdesc"] = this.ddlMaterials.Items[i].Text.ToString();
                        dr1["rsirunit"] = (((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
                        dr1["balqty"] = ((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "' and spcfcod='" + mSpcfCod + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "' and spcfcod='" + mSpcfCod + "'"))[0]["bbgdqty"]).ToString();
                        dr1["isuqty"] = 0.00;
                        dr1["spcfcod"] = mSpcfCod;
                        dr1["spcfdesc"] = dt3.Rows[j]["spcfdesc"].ToString();
                        dr1["useoflocation"] = "";
                        dr1["remarks"] = "";

                        dt.Rows.Add(dr1);

                    }



                }
            }

            //DataView dv1 = dt.DefaultView;
            //dv1.Sort = ("rsircode, spcfcod");
            //dt = this.HiddenSameData(dv1.ToTable());
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

           // ((LinkButton)this.grvissue.FooterRow.FindControl("lnkupdate")).Enabled = false;
          
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


            string mRef = this.txtMIsuRef.Text;
            string mSmcr = this.txtsmcr.Text;
           
            string dmirfno = this.txtsmcr.Text;


            if (this.Request.QueryString["type"] == "Entry")
            {

                switch (comcod)
                {
                    case "3301":
                    case "1301":
                        //case "3101":
                        break;

                    default:

                        dr1 = tbl2.Select("balqty<isuqty");

                        if (dr1.Length > 0)
                        {

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Balance";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }


                        break;



                }
            }


            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetPerMatIssu();
            }

            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string mISUDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");

            // Duplicate 
            switch (comcod)
            {
                case "3315": // assure 
                case "3316": // assure
                case "3317": // assure
                case "3367": // epic 
                //case "3101": // epic 

                    break;

                default:

                    if (mRef.Length == 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "SMCR No Should Not Be Empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        this.ddlPrevISSList.Items.Clear();
                        return;
                    }
                    else if (dmirfno.Length == 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "DMIRF No Should Not Be Empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Found Duplicate SMCR.No');", true);                           
                            this.ddlPrevISSList.Items.Clear();
                            return;
                        }
                    }
                    break;
            }         


            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string mISURNAR = this.txtISSNarr.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "PURMISSUEB",
                             mISUNO, mISUDAT, mPACTCODE, mISURNAR, mRef, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, mSmcr, "", "");
            //if (!result)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;
            //}
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string Rsircode = tbl2.Rows[i]["rsircode"].ToString();
                string Spcfcod = tbl2.Rows[i]["spcfcod"].ToString();
                double Isuqty = Convert.ToDouble(tbl2.Rows[i]["isuqty"].ToString());
                string txtlocation = tbl2.Rows[i]["useoflocation"].ToString();
                string txtremarks = tbl2.Rows[i]["remarks"].ToString();

                if (Isuqty > 0)
                {

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "PURMISSUEA", mISUNO,
                        Rsircode, Spcfcod, Isuqty.ToString(), txtlocation, txtremarks, "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
            }

            string CurDate1= System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet dsx = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURMISSUEINFO", mISUNO, "", "", "", "", "", "", "", "");
            if (dsx == null)
                return;
            this.XmlDataInsert(mISUNO, dsx);

            //((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Update Successfully" + "');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Update Successfully" + "');", true);

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
                string txtlocation = ((TextBox)this.grvissue.Rows[i].FindControl("txtlocation")).Text.Trim();
                string txtisurmk = ((TextBox)this.grvissue.Rows[i].FindControl("txtisurmk")).Text.Trim();


                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;
                dt.Rows[TblRowIndex]["isuqty"] = txtwrkqty;
                dt.Rows[TblRowIndex]["useoflocation"] = txtlocation;
                dt.Rows[TblRowIndex]["remarks"] = txtisurmk;



            }
            ViewState["tblmatissue"] = dt;
        }


        protected void grvissue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string MatCode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            string spcfcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblgvspcfcode")).Text.Trim();

            if (dt.Rows.Count > 0)
            {
                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt);
                //this.XmlDataDeleted(mISUNO, ds1);
            }

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEMATISUE", mISUNO, MatCode, spcfcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
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

            if (tbl1.Rows.Count > 0)
            {
                DataSet ds1 = new DataSet();
                ds1.Tables.Add(tbl1);
                //this.XmlDataDeleted(mISUNO, ds1);
            }


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEMATISUEALL", mISUNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;
            //}

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Delete Successfully" + "');", true);
            }

            //((Label)this.Master.FindControl("lblmsg")).Text = "Data Delete  successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


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
    }
}