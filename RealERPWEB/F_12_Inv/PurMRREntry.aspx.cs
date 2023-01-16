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



    public partial class PurMRREntry : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Material Receive";
                string comcod = this.GetCompCode();

                this.txtCurMRRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtChaDate.Text = (comcod == "3354" ? "" : DateTime.Today.ToString("dd.MM.yyyy"));
                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "Entry") ? "Materials Receive"
                //    : "Delete Materials Receive Information Input/Edit Screen";

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

                if (comcod == "3354")
                {

                    this.CalendarExtender1.EndDate = System.DateTime.Today;
                }
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
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101": // pintech
                case "3340":
                case "3335":
                case "1301":
                case "3301":
                case "1205": //p2p Engineering
                case "3351": //WECON Properties
                case "3352": //P2P 360
                case "3354": //Edison Real Estate
                case "3353": //Manama

                case "3368": //finlay
                    this.chkdupMRR.Enabled = false;
                    this.chkdupMRR.Checked = true;
                    break;

                default:
                    this.chkdupMRR.Visible = false;
                    this.chkdupMRR.Checked = false;
                    break;
            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Load_Project_Combo()
        {

            //PRJCODELIST1
            this.ddlSupList.Items.Clear();
            this.ddlOrderList.Items.Clear();
            string comcod = this.GetCompCode();
            //string FindProject =  this.txtProjectSearch.Text.Trim() + "%" ;
            string FindProject = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? this.txtProjectSearch.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            //string CallType = (this.Request.QueryString["InputType"].ToString() == "Entry") ? "PRJCODELIST1" : "PRJCODELIST";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRRPRJLIST", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            this.ddlProject_SelectedIndexChanged(null, null);
            //if((this.Request.QueryString["prjcode"].ToString()).Length == 0)


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
            string comcod = this.GetCompCode();   //GetComeCode();
            switch (comcod)
            {
                case "3330":
                case "3101":



                    this.PrintMrrBridge();
                    break;

                // case"3101":
                case "3335":
                    this.PrintMrrEdision();
                    break;
                default:
                    this.PrintMrrGen();                   //PrintMonLeave();
                    break;

            }


        }



        private void PrintMrrEdision()
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
            DataTable dt = (DataTable)ViewState["tblMRR"];
            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mMRRNo = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNo, CurDate1,
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptPurMrrEdison", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + ddlProject.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtSubName", "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtchalanno", "Chalan No: " + this.txtChalanNo.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtMrrno", "MRR No: " + this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtMrrRef", "MRR Ref: " + this.txtMRRRef.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + this.txtCurMRRDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtQc", "Quality Certificate: " + this.txtQc.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtOrder", ds1.Tables[1].Rows[0]["pordref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtpostedby", ds1.Tables[1].Rows[0]["usrnam"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", "Mrf No : " + ds1.Tables[0].Rows[0]["mrfno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Receiving Report"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            // DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.rptPurMrrEdision();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Substring(14);
            //TextObject rpttxtsupplier = rptstk.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            //rpttxtsupplier.Text = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
            ////////TextObject rpttxtorder = rptstk.ReportDefinition.ReportObjects["txtorder"] as TextObject;
            ////////rpttxtorder.Text = "Order No: " + this.ddlOrderList.SelectedValue.ToString();
            //TextObject rpttxtchlnno = rptstk.ReportDefinition.ReportObjects["txtchalanno"] as TextObject;
            //rpttxtchlnno.Text = "Chalan No : " + this.txtChalanNo.Text;
            //TextObject rpttxtmrfno = rptstk.ReportDefinition.ReportObjects["rpttxtmrfno"] as TextObject;
            //rpttxtmrfno.Text = "Mrf No : " + ds1.Tables[0].Rows[0]["mrfno"].ToString();
            //TextObject rpttxtMrrno = rptstk.ReportDefinition.ReportObjects["Mrrno"] as TextObject;
            //rpttxtMrrno.Text = "MRR No: " + this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //TextObject rpttxtMrrRef = rptstk.ReportDefinition.ReportObjects["MrrRef"] as TextObject;
            //rpttxtMrrRef.Text = "MRR Ref: " + this.txtMRRRef.Text.Trim();
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + this.txtCurMRRDate.Text.Trim();
            //TextObject rpttxtQc = rptstk.ReportDefinition.ReportObjects["txtQc"] as TextObject;
            //rpttxtQc.Text = "Quality Certificate: " + this.txtQc.Text.Trim();

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    //string comcod = this.GetCompCode();
            //    string eventtype = "Materials Receive Information";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMrrGen()
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

            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mMRRNo = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNo, CurDate1,
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptPurMrrEntry", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + ddlProject.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtSubName", "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtchalanno", "Chalan No: " + this.txtChalanNo.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtMrrno", "MRR No: " + this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtMrrRef", "MRR Ref: " + this.txtMRRRef.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + this.txtCurMRRDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtQc", "Quality Certificate: " + this.txtQc.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtOrder", ds1.Tables[1].Rows[0]["pordref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtpostedby", ds1.Tables[1].Rows[0]["usrnam"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Receiving Report"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comnam = hst["comnam"].ToString();
            // string comadd = hst["comadd1"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // DataTable dt = (DataTable)ViewState["tblMRR"];

            //string comcod = this.GetCompCode();
            //string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            //string mMRRNo = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNo, CurDate1,
            //             "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            //// DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.rptPurMrrEntry();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Substring(14);
            //TextObject rpttxtsupplier = rptstk.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            //rpttxtsupplier.Text = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
            ////////TextObject rpttxtorder = rptstk.ReportDefinition.ReportObjects["txtorder"] as TextObject;
            ////////rpttxtorder.Text = "Order No: " + this.ddlOrderList.SelectedValue.ToString();
            //TextObject rpttxtchlnno = rptstk.ReportDefinition.ReportObjects["txtchalanno"] as TextObject;
            //rpttxtchlnno.Text = "Chalan No: " + this.txtChalanNo.Text.Trim();
            ////TextObject rpttxtchlnnoDate = rptstk.ReportDefinition.ReportObjects["rpttxtchlnnoDate"] as TextObject;
            ////rpttxtchlnnoDate.Text = "Chalan Date: " + this.txtChalanDate.Text.Trim();
            //TextObject rpttxtMrrno = rptstk.ReportDefinition.ReportObjects["Mrrno"] as TextObject;
            //rpttxtMrrno.Text = "MRR No: " + this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //TextObject rpttxtMrrRef = rptstk.ReportDefinition.ReportObjects["MrrRef"] as TextObject;
            //rpttxtMrrRef.Text = "MRR Ref: " + this.txtMRRRef.Text.Trim();
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + this.txtCurMRRDate.Text.Trim();
            //TextObject rpttxtQc = rptstk.ReportDefinition.ReportObjects["txtQc"] as TextObject;
            //rpttxtQc.Text = "Quality Certificate: " + this.txtQc.Text.Trim();

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //TextObject txtpostedby = rptstk.ReportDefinition.ReportObjects["txtpostedby"] as TextObject;
            //txtpostedby.Text = ds1.Tables[1].Rows[0]["usrnam"].ToString();



            //if (ConstantInfo.LogStatus == true)
            //{
            //    //string comcod = this.GetCompCode();
            //    string eventtype = "Materials Receive Information";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }






        private void PrintMrrBridge()
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

            string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mMRRNo = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNo, CurDate1,
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            string desig = ds1.Tables[1].Rows[0]["usrnam"].ToString() + "," + ds1.Tables[1].Rows[0]["deg"].ToString() + "\n" + Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd-MMM-yyyy");
            string txtreqno = dt.Rows[0]["reqno"].ToString().Substring(0, 3) + dt.Rows[0]["reqno"].ToString().Substring(7, 2) + '-' + dt.Rows[0]["reqno"].ToString().Substring(9);

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptPurMrrEntryBridge", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", this.ddlProject.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtSubName", this.ddlSupList.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtchalanno", this.txtChalanNo.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtMrrno", this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtMrrRef", this.txtMRRRef.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtDate", this.txtCurMRRDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtOrder", ds1.Tables[1].Rows[0]["pordref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtdesig", desig));
            Rpt1.SetParameters(new ReportParameter("txtaddress", ds1.Tables[1].Rows[0]["address"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtreqno", txtreqno));
            Rpt1.SetParameters(new ReportParameter("txtreqdate", Convert.ToDateTime(dt.Rows[0]["reqdat"]).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtMRRNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtorderdat", Convert.ToDateTime(dt.Rows[0]["orderdat"]).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtchlnnoDate", txtChaDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Receiving Report"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.rptPurMrrEntryBridge();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text =  this.ddlProject.SelectedItem.Text.Substring(14);
            //TextObject rpttxtsupplier = rptstk.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            //rpttxtsupplier.Text = this.ddlSupList.SelectedItem.Text.Trim();
            //TextObject rpttxtorder = rptstk.ReportDefinition.ReportObjects["txtorder"] as TextObject;
            //rpttxtorder.Text = ds1.Tables[1].Rows[0]["pordref"].ToString(); ;
            //TextObject rpttxtchlnno = rptstk.ReportDefinition.ReportObjects["txtchalanno"] as TextObject;
            //rpttxtchlnno.Text =this.txtChalanNo.Text.Trim();
            //TextObject rpttxtchlnnoDate = rptstk.ReportDefinition.ReportObjects["rpttxtchlnnoDate"] as TextObject;
            //rpttxtchlnnoDate.Text = this.txtChaDate.Text.Trim();

            //TextObject txtorderdate = rptstk.ReportDefinition.ReportObjects["txtorderdat"] as TextObject;
            //txtorderdate.Text = Convert.ToDateTime(dt.Rows[0]["orderdat"]).ToString("dd.MM.yyyy");

            //TextObject txtaddress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtaddress.Text = ds1.Tables[1].Rows[0]["address"].ToString();

            //TextObject txtdeg = rptstk.ReportDefinition.ReportObjects["txtdeg"] as TextObject;
            //txtdeg.Text = ds1.Tables[1].Rows[0]["usrnam"].ToString() + "," + ds1.Tables[1].Rows[0]["deg"].ToString() + "\n" + Convert.ToDateTime( ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd-MMM-yyyy"); 

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["txtmrfno"] as TextObject;
            //txtmrfno.Text = dt.Rows[0]["mrfno"].ToString();


            //TextObject txtreqno = rptstk.ReportDefinition.ReportObjects["txtreqno"] as TextObject;
            //txtreqno.Text =  dt.Rows[0]["reqno"].ToString().Substring(0, 3) + dt.Rows[0]["reqno"].ToString().Substring(7, 2) + '-' + dt.Rows[0]["reqno"].ToString().Substring(9);
            //TextObject txtreadate = rptstk.ReportDefinition.ReportObjects["txtreadate"] as TextObject;
            //txtreadate.Text = Convert.ToDateTime(dt.Rows[0]["reqdat"]).ToString("dd.MM.yyyy");


            //TextObject rpttxtMrrno = rptstk.ReportDefinition.ReportObjects["Mrrno"] as TextObject;
            //rpttxtMrrno.Text =this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //TextObject rpttxtMrrRef = rptstk.ReportDefinition.ReportObjects["MrrRef"] as TextObject;
            //rpttxtMrrRef.Text =this.txtMRRRef.Text.Trim();
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = this.txtCurMRRDate.Text.Trim();



            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtMRRNarr.Text.Trim();

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{

            //    string eventtype = "Materials Receive Information";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


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
            // string SearchMrr = "%%";
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string SearchMrr = (qgenno.Length == 0 ? "%" : this.Request.QueryString["genno"].ToString()) + "%";
            //string SearchMrr = this.txtSrchPreMRR.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPREVMRRLIST", CurDate1, SearchMrr, length, usrid, "", "", "", "", "");
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
                //this.lblPreMRR.Visible = true;          // gvMRRInfo
                //this.txtSrchPreMRR.Visible = true;
                this.ImgbtnPreMRR.Visible = true;
                this.ddlPrevMRRList.Visible = true;
                this.ddlPrevMRRList.Items.Clear();
                this.ddlProject.Visible = true;
                this.lblddlProject.Visible = false;

                this.ddlSupList.Enabled = true;
                this.ImgbtnFindSup.Enabled = true;
                this.lblCurMRRNo1.Text = "MRR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMRRDate.Enabled = true;
                this.txtMRRRef.Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                this.ddlOrderList.Enabled = true;
                this.txtResSearch.Text = "";
                //this.DropCheck1.Items.Clear();
                this.listGroup.Items.Clear();

                //  this.ddlResList.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtMRRNarr.Text = "";

                this.gvMRRInfo.DataSource = null;
                this.gvMRRInfo.DataBind();


                this.lbtnOk.Text = "Ok";
                this.txtChalanNo.Text = "";
                this.Panel1.Visible = false;
                this.PnlNarration.Visible = false;
                this.FindOrderList();
                return;
            }

            //this.lblPreMRR.Visible = false;
            //this.txtSrchPreMRR.Visible = false;
            this.ImgbtnPreMRR.Visible = false;
            this.ddlPrevMRRList.Visible = false;

            this.lblddlProject.Text = (this.ddlProject.Items.Count == 0 ? "XXX" : this.ddlProject.SelectedItem.Text.Trim());
            this.ddlProject.Visible = false;
            this.lblddlProject.Visible = true;

            this.ddlSupList.Enabled = false;
            this.ImgbtnFindSup.Enabled = false;
            this.txtCurMRRNo2.ReadOnly = true;
            this.ddlOrderList.Enabled = false;
            this.Panel1.Visible = true;
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";
            this.lblChaDate.Text = getChalanDateMsg();
            this.Get_Receive_Info();
            this.ImgbtnFindRes_Click(null, null);



        }
        private string getChalanDateMsg()
        {
            string msg = "";
            string comcode = this.GetCompCode();
            switch (comcode)
            {
                case "3354":
                    msg = "Actual Receive Date";
                    break;
                default:
                    msg = "Challan Date";
                    break;
            }
            return msg;
        }


        protected void Session_tblMRR_Update()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
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
                    ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvMRRQty")).Text = dgvMRRQty.ToString("#,##0.000;(#,##0.000); ");
                    ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvMRRRate")).Text = dgvMRRRate.ToString("#,##0.0000;(#,##0.0000); ");
                    ((TextBox)this.gvMRRInfo.Rows[j].FindControl("txtgvChlnqty")).Text = dgvChlnQty.ToString("#,##0.000;(#,##0.000); ");
                    ((Label)this.gvMRRInfo.Rows[j].FindControl("lblgvMRRAmt")).Text = dgvMRRAmt.ToString("#,##0.000;(#,##0.000); ");
                    TblRowIndex2 = (this.gvMRRInfo.PageIndex) * this.gvMRRInfo.PageSize + j;
                    tbl1.Rows[TblRowIndex2]["mrrqty"] = dgvMRRQty;
                    tbl1.Rows[TblRowIndex2]["mrrrate"] = dgvMRRRate;
                    tbl1.Rows[TblRowIndex2]["mrramt"] = dgvMRRAmt;
                    tbl1.Rows[TblRowIndex2]["mrrnote"] = dgvMRRNote;
                    tbl1.Rows[TblRowIndex2]["chlnqty"] = dgvChlnQty;

                }

                else
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "MRR Qty  must be less then equal Balance Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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

            //For Visible Item Serial Manama
            string comcod = GetCompCode();
            switch (comcod)
            {
                case "3353":
                    this.gvMRRInfo.Columns[1].Visible = true;
                    break; 
                
                case "3370":
                    this.gvMRRInfo.Columns[1].Visible = false;
                    this.gvMRRInfo.Columns[13].Visible = false;
                    break;
                default:
                    this.gvMRRInfo.Columns[1].Visible = false;
                    this.gvMRRInfo.Columns[13].Visible = true;
                    break;

            }
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

            if (tbl1.Rows.Count == 0)
                return;
            ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvMRRInfo.PageSize);
            ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
            ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvMRRInfo.PageIndex;
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
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTMRRINFO", CurDate1, "", "", "", "", "", "", "", "");
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
        private void GetMRRefno()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                case "3101":
                case "3330":
                case "5101":
                    string pactcode = this.ddlProject.SelectedValue.ToString();
                    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRRREFNO", pactcode, "", "", "", "", "", "", "", "");
                    this.txtMRRRef.Text = ds2.Tables[0].Rows[0]["mrrref"].ToString();
                    break;


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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;



            ViewState["tblMRR"] = this.HiddenSameData(ds1.Tables[0]);
            Session["UserLog"] = ds1.Tables[1];

            if (mMRRNo == "NEWMRR")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTMRRINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMRRNo1.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(0, 6);
                    this.txtCurMRRNo2.Text = ds1.Tables[0].Rows[0]["maxmrrno1"].ToString().Substring(6, 5);
                }

                // this.GetMRRefno();
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
            // this.ImgbtnFindSup_Click(null, null);

            this.ddlSupList.SelectedValue = ds1.Tables[1].Rows[0]["ssircode"].ToString();
            // this.FindOrderList();        



            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["orderno"].ToString();

            this.txtMRRRef.Text = ds1.Tables[1].Rows[0]["mrrref"].ToString();
            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["orderno"].ToString();
            this.lblCurMRRNo1.Text = ds1.Tables[1].Rows[0]["mrrno1"].ToString().Substring(0, 6);
            this.txtCurMRRNo2.Text = ds1.Tables[1].Rows[0]["mrrno1"].ToString().Substring(6, 5);
            this.txtCurMRRDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdat"]).ToString("dd.MM.yyyy");
            this.lblddlProject.Text = (this.ddlProject.Items.Count == 0 ? "XXX" : this.ddlProject.SelectedItem.Text.Trim());
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["mrrbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
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
            //string mReqno = this.DropCheck1.Text.ToString().Substring(0, 14);
            //string mResCode = this.DropCheck1.Text.ToString().Substring(14, 12);
            //string mSpcfCode = this.DropCheck1.Text.ToString().Substring(26, 12);
            string mReqno1 = "";
            string Narration = "";
            string mReqno2 = "000000000000";


            //string[] reqno = this.DropCheck1.Text.Trim().Split(',');

            //string grpcode = "";
            //foreach (ListItem item in listGroup.Items)
            //{
            //    if (item.Selected)
            //    {
            //        grpcode += item.Value;
            //    }
            //}

            //string reqno1 in reqno 
            //string[] reqno = this.listGroup.Text.Trim().Split(',');
            foreach (ListItem reqno1 in listGroup.Items)
            {
                if (reqno1.Selected)
                {

                    mReqno1 = reqno1.Value.Substring(0, 38);
                    DataTable tbl2 = (DataTable)ViewState["tblMat"];
                    string mReqno = mReqno1.Substring(0, 14);
                    string mResCode = mReqno1.Substring(14, 12);
                    string mSpcfCode = mReqno1.Substring(26, 12);

                    DataView dv = tbl2.DefaultView;
                    dv.RowFilter = ("orderno='" + orderno + "' and  reqno='" + mReqno + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
                    tbl2 = dv.ToTable();

                    for (int i = 0; i < tbl2.Rows.Count; i++)
                    {
                        //string mReqno = tbl2.Rows[i]["reqno"].ToString();

                        string reqno2 = tbl2.Rows[i]["reqno1"].ToString();
                        string sirdesc = tbl2.Rows[i]["sirdesc"].ToString();
                        string spcfdesc = tbl2.Rows[i]["spcfdesc"].ToString();
                        string rsirunit = tbl2.Rows[i]["rsirunit"].ToString();
                        string orderdat = tbl2.Rows[i]["orderdat"].ToString();
                        string orderqty = tbl2.Rows[i]["orderqty"].ToString();
                        string recup = tbl2.Rows[i]["recup"].ToString();
                        double balqty = Convert.ToDouble(tbl2.Rows[i]["balqty"]);
                        double mrrqty = 0.00;
                        double mrrrate = Convert.ToDouble(tbl2.Rows[i]["mrrrate"]);
                        double rowid = Convert.ToDouble(tbl2.Rows[i]["rowid"]);
                        //dr1["reqno1"] = dr3[0]["reqno1"];
                        //dr1["rsirdesc1"] = dr3[0]["sirdesc"];
                        //dr1["spcfdesc"] = dr3[0]["spcfdesc"];
                        //dr1["rsirunit"] = dr3[0]["rsirunit"];
                        //dr1["orderdat"] = dr3[0]["orderdat"];
                        //dr1["orderqty"] = dr3[0]["orderqty"];
                        //dr1["recup"] = dr3[0]["recup"];
                        //dr1["orderbal"] = dr3[0]["balqty"];
                        //dr1["mrrqty"] = 0;
                        //dr1["mrrrate"] = dr3[0]["mrrrate"];




                        //string mReqno = this.DropCheck1.Text.ToString().Substring(0, 14);
                        //string mResCode = this.DropCheck1.SelectedValue.ToString().Substring(14, 12);
                        //string mSpcfCode = this.DropCheck1.SelectedValue.ToString().Substring(26, 12);
                        DataRow[] dr2 = tbl1.Select("orderno='" + orderno + "' and  reqno='" + mReqno + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
                        if (dr2.Length == 0)
                        {
                            DataRow dr1 = tbl1.NewRow();
                            dr1["orderno"] = orderno;
                            dr1["reqno"] = mReqno;
                            dr1["rsircode"] = mResCode;
                            dr1["spcfcod"] = mSpcfCode;




                            // DataRow[] dr3 = tbl2.Select("orderno='" + orderno + "' and  reqno='" + mReqno + "'and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
                            dr1["reqno1"] = reqno2;  //dr3[0]["reqno1"];
                            dr1["rsirdesc1"] = sirdesc;// dr3[0]["sirdesc"];
                            dr1["spcfdesc"] = spcfdesc; //dr3[0]["spcfdesc"];
                            dr1["rsirunit"] = rsirunit; //dr3[0]["rsirunit"];
                            dr1["orderdat"] = orderdat; //dr3[0]["orderdat"];
                            dr1["orderqty"] = orderqty;//dr3[0]["orderqty"];
                            dr1["recup"] = recup; // dr3[0]["recup"];
                            dr1["orderbal"] = balqty; //dr3[0]["balqty"];
                            dr1["mrrqty"] = balqty;
                            dr1["mrrrate"] = mrrrate; // dr3[0]["mrrrate"];
                            dr1["mrramt"] = 0;
                            dr1["mrrnote"] = "";
                            dr1["chlnqty"] = balqty;
                            dr1["rowid"] = rowid;
                            tbl1.Rows.Add(dr1);

                            if (mReqno2 != mReqno)
                            {
                                Narration = Narration + tbl2.Rows[i]["reqnar"] + ", ";

                            }

                            mReqno2 = mReqno;


                            this.lblreqnaration.Text = "Req Naration : " + Narration.Substring(0, (Narration.Length) - 2);

                        }
                    }
                }
            }


            ViewState["tblMRR"] = this.HiddenSameData(tbl1);
            int RowNo = 1;
            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{
            //    if (tbl1.Rows[i]["rsircode"].ToString() == mResCode && tbl1.Rows[i]["spcfcod"].ToString() == mSpcfCode)
            //    {
            //        RowNo = i + 1;
            //        break;
            //    }
            //}
            double PageNo = Math.Ceiling(RowNo * 1.00 / this.gvMRRInfo.PageSize);
            this.gvMRRInfo.PageIndex = Convert.ToInt32(PageNo - 1);
            this.gvMRRInfo_DataBind();

        }

        protected void lbtnSelectResAll_Click(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            //string mReqno = this.DropCheck1.Text.ToString().Substring(0, 14);
            //string mResCode = this.DropCheck1.Text.ToString().Substring(14, 12);
            //string mSpcfCode = this.DropCheck1.Text.ToString().Substring(26, 12);

            string mReqno = this.listGroup.SelectedValue.ToString().Substring(0, 14);
            string mResCode = this.listGroup.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCode = this.listGroup.SelectedValue.ToString().Substring(26, 12);

            //string mReqno = this.DropCheck1.SelectedValue.ToString().Substring(0, 14);
            //string mResCode = this.DropCheck1.SelectedValue.ToString().Substring(14, 12);
            //string mSpcfCode = this.DropCheck1.SelectedValue.ToString().Substring(26, 12);
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            DataTable tbl2 = (DataTable)ViewState["tblMat"];
            DataRow[] dr = tbl1.Select("orderno='" + orderno + "' and  reqno='" + mReqno + "' and  rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
            if (dr.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {

                    DataRow dr1 = tbl1.NewRow();
                    dr1["orderno"] = tbl2.Rows[i]["orderno"].ToString();
                    dr1["reqno"] = tbl2.Rows[i]["reqno"].ToString();
                    dr1["reqno1"] = tbl2.Rows[i]["reqno1"].ToString();
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    dr1["rsirdesc1"] = tbl2.Rows[i]["sirdesc"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
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
                ViewState["tblMRR"] = this.HiddenSameData(tbl1);
            }
            this.gvMRRInfo_DataBind();

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            this.gvMRRInfo.PageIndex = ((DropDownList)this.gvMRRInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvMRRInfo_DataBind();
        }


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("finappid", Type.GetType("System.String"));
            tblt01.Columns.Add("finappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("finapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("finappseson", Type.GetType("System.String"));

            ViewState["tblapproval"] = tblt01;
        }

        private string GetReqApproval(string approval)
        {

            try
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

                            case "3348": // Credence
                            case "3368": // Finlay
                                break;

                            default:
                                if (approval == "")
                                {
                                    this.CreateDataTable();
                                    DataTable dt = (DataTable)ViewState["tblapproval"];
                                    DataRow dr1 = dt.NewRow();
                                    dr1["finappid"] = usrid;
                                    dr1["finappdat"] = Date;
                                    dr1["finapptrmid"] = trmnid;
                                    dr1["finappseson"] = session;
                                    dt.Rows.Add(dr1);
                                    ds1.Merge(dt);
                                    ds1.Tables[0].TableName = "tbl1";
                                    approval = ds1.GetXml();

                                }
                                else
                                {

                                    xmlSR = new System.IO.StringReader(approval);
                                    ds1.ReadXml(xmlSR);
                                    ds1.Tables[0].TableName = "tbl1";
                                    ds1.Tables[0].Rows[0]["finappid"] = usrid;
                                    ds1.Tables[0].Rows[0]["finappdat"] = Date;
                                    ds1.Tables[0].Rows[0]["finapptrmid"] = trmnid;
                                    ds1.Tables[0].Rows[0]["finappseson"] = session;
                                    approval = ds1.GetXml();
                                }
                                break;

                        }
                        break;
                    case "FinalApp":
                        if (approval == "")
                        {
                            this.CreateDataTable();
                            DataTable dt = (DataTable)ViewState["tblapproval"];
                            DataRow dr1 = dt.NewRow();
                            dr1["finappid"] = usrid;
                            dr1["finappdat"] = Date;
                            dr1["finapptrmid"] = trmnid;
                            dr1["finappseson"] = session;
                            dt.Rows.Add(dr1);
                            ds1.Merge(dt);
                            ds1.Tables[0].TableName = "tbl1";
                            approval = ds1.GetXml();

                        }
                        else
                        {
                            xmlSR = new System.IO.StringReader(approval);
                            ds1.ReadXml(xmlSR);
                            ds1.Tables[0].TableName = "tbl1";
                            ds1.Tables[0].Rows[0]["finappid"] = usrid;
                            ds1.Tables[0].Rows[0]["finappdat"] = Date;
                            ds1.Tables[0].Rows[0]["finapptrmid"] = trmnid;
                            ds1.Tables[0].Rows[0]["finappseson"] = session;
                            approval = ds1.GetXml();
                        }

                        break;
                }
            }
            catch (Exception ex)
            {

            }

            return approval;

        }

        protected void lbtnUpdateMRR_Click(object sender, EventArgs e)
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

            //string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            //string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            //string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            //string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;


            string PostedByid = (this.Request.QueryString["type"] == "Entry") ? ((tblPostedByid == "") ? userid : tblPostedByid) : ((tblPostedByid == "") ? userid : tblPostedByid);
            string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid) : ((tblPostedtrmid == "") ? Terminal : tblPostedtrmid);
            string PostSession = (this.Request.QueryString["type"] == "Entry") ? ((tblPostedSession == "") ? Sessionid : tblPostedSession) : ((tblPostedSession == "") ? Sessionid : tblPostedSession);
            string Posteddat = (this.Request.QueryString["type"] == "Entry") ? ((tblPosteddat == "") ? Date : tblPosteddat) : ((tblPosteddat == "") ? Date : tblPosteddat);



            //string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            //string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            //string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            //string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;




            string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : userid;
            string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");


            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            DataRow[] dr = tbl1.Select("mrrqty>0");
            if (dr.Length == 0)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Input Receive Qty";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            string mMRRDAT = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
            string mSSIRCODE = this.ddlSupList.SelectedValue.ToString().Trim();
            string mORDERNO = this.ddlOrderList.SelectedValue.ToString().Trim();
            string mMRRUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mMRRBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mMRRNAR = this.txtMRRNarr.Text.Trim();
            string mMRRChlnNo = this.txtChalanNo.Text.Trim();
            string mrrno = this.txtMRRRef.Text.Trim();
            string chldate = this.txtChaDate.Text.Trim();
            string mchlndate = this.GetStdDate(this.txtChaDate.Text.Trim()); ;
            string mQcno = this.txtQc.Text.Trim();





            //Chalan No && MRR No check
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                case "3339": // THL
                    if (mMRRChlnNo.Length <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Required Chalan No";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    break;


                case "3101":
                case "3368": // finlay
                case "5101":
                case "3330": // bridge
                case "1205": //p2p Engineering
                case "3351": //WECON Properties
                case "3352": //P2P 360

                    if (mrrno.Length <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Required MRR No";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    break;

                case "3340":
                case "3336":
                    if (this.txtMRRRef.Text.Trim().Length <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Required MRR Ref. No";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                    if (mMRRChlnNo.Length <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Required Chalan No";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    break;

                // case "3101":
                case "3354":
                    //  case "3101":

                    if (chldate.Length == 0)
                    {
                        this.txtChaDate.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Challan Daten Required');", true);
                        return;
                    }
                    break;

                default:
                    break;

            }


            ////For Balace Orderqty Qty

            if (this.Request.QueryString["type"].ToString().Trim() == "Entry")
            {
                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mreqno = tbl1.Rows[i]["reqno"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "BALORDERQTY", mORDERNO, mreqno, mRSIRCODE, mSPCFCOD, "", "", "", "", "");
                    if (ds.Tables[0].Rows.Count == 0) continue;
                    else if (Convert.ToDouble(ds.Tables[0].Rows[0]["balqty"]) <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "There is no balance qty in Order";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('MRR Date is equal or greater Order Date');", true);
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
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "CHECKEDDUPMRRNO", mMRRREF, "", "", "", "", "", "", "", "");
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
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate MRR No";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //this.ddlPrevMRRList.Items.Clear();
                        this.ddlPrevMRRList.Items.Clear();
                        return;
                    }
                }
            }





            string appxml = tbl1.Rows[0]["approval"].ToString();
            string Approval = this.GetReqApproval(appxml);

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURMRRINFO", "PURMRRB",
                             mMRRNO, mMRRDAT, mPACTCODE, mSSIRCODE, mORDERNO, mMRRUSRID, mAPPRUSRID, mAPPRDAT, mMRRBYDES, mAPPBYDES, mMRRREF, mMRRNAR, mMRRChlnNo, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, mQcno, mchlndate, Approval);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {




                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["orderdat"].ToString()), Convert.ToDateTime(mMRRDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('MRR Date is equal or greater Order Date');", true);
                    return;
                }



                string orderno = tbl1.Rows[i]["orderno"].ToString();
                string mreqno = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                double orbal = Convert.ToDouble(tbl1.Rows[i]["orderbal"].ToString());
                double mMRRQTY = Convert.ToDouble(tbl1.Rows[i]["mrrqty"].ToString());
                string mMRRAMT = tbl1.Rows[i]["mrramt"].ToString();
                string mMRRNOTE = tbl1.Rows[i]["mrrnote"].ToString();
                string mMRRchlnqty = tbl1.Rows[i]["chlnqty"].ToString();
                if (orbal >= mMRRQTY)
                {
                    if (mMRRQTY > 0)
                        result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURMRRINFO", "PURMRRA",
                                 mMRRNO, mRSIRCODE, mSPCFCOD, mMRRQTY.ToString(), mMRRAMT, mMRRNOTE, mMRRchlnqty, mreqno, orderno, "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }

                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "MRR Qty  must be less then equal Balance Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
            this.txtCurMRRDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

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






            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Receive Information";
            //    string eventdesc = "Update MRR Qty";
            //    string eventdesc2 = mMRRNO;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblMRR_Update();
            DataTable tbl1 = (DataTable)ViewState["tblMRR"];
            ((Label)this.gvMRRInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(mrramt)", "")) ?
                    0.00 : tbl1.Compute("Sum(mrramt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        private string GetResSupplier()
        {
            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3336":
                case "3340":

                    Calltype = "GETALLMRRSUPLIST";
                    break;

                default:
                    Calltype = "GETMRRSUPLIST";
                    break;

            }
            return Calltype;


        }

        protected void ImgbtnFindSup_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {

                string comcod = this.GetCompCode();
                string FindSupplier = (this.Request.QueryString["sircode"].ToString()).Length == 0 ? this.txtSupSearch.Text.Trim() + "%" : this.Request.QueryString["sircode"].ToString() + "%";
                string mProjCode = this.ddlProject.SelectedValue.ToString();
                string CallType = this.GetResSupplier();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", CallType, FindSupplier, mProjCode, "", "", "", "", "", "", "");
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
            //(this.Request.QueryString["prjcode"].ToString()).Length == 0 ? this.txtProjectSearch.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRRORDERLIST", orderno, mProjCode, mSupCode, Date, "", "", "", "", "");
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
            string mSrchTxt = "%" + this.txtResSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMRRRESLIST", mSrchTxt, mProject, mSupCode, mOrderNo, "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblMat"] = ds1.Tables[0];
            this.listGroup.DataTextField = "rsirdesc1";
            this.listGroup.DataValueField = "rsircode1";
            this.listGroup.DataSource = ds1.Tables[0];
            this.listGroup.DataBind();

            //this.DropCheck1.DataTextField = "rsirdesc1";
            //this.DropCheck1.DataValueField = "rsircode1";
            //this.DropCheck1.DataSource = ds1.Tables[0];
            //this.DropCheck1.DataBind();

            //this.ddlResList.DataTextField = "rsirdesc1";
            //this.ddlResList.DataValueField = "rsircode1";
            //this.ddlResList.DataSource = ds1.Tables[0];
            //this.ddlResList.DataBind();
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


        protected void gvMRRInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblMRR"];
            string MRRNO = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            string reqno = ((Label)this.gvMRRInfo.Rows[e.RowIndex].FindControl("lblgvReqnomain")).Text.Trim();
            string rescode = ((Label)this.gvMRRInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMRRMAT", MRRNO, reqno, rescode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvMRRInfo.PageSize) * (this.gvMRRInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState["tblMRR"] = dv.ToTable();
            this.gvMRRInfo_DataBind();
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
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMRR", mMRRNO, userid, Terminal, Sessionid, date, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
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

    }
}
