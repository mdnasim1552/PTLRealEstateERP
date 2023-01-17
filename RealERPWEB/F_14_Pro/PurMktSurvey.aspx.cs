using System;
using System.Collections;
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
namespace RealERPWEB.F_14_Pro
{

    public partial class PurMktSurvey : System.Web.UI.Page
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

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "MktSurvey") ? "Comparative Statement - Purchase 01" : "Survey Link";
                //if (this.txtCurMSRDate.Text.Trim().Length == 0)
                //{
                //    this.lbtnMSROk.Text = "New";
                //    this.lbtnMSROk_Click(null, null)


                //}
                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurMSRDate_CalendarExtender.EndDate = System.DateTime.Today;

                //this.txtCurReqDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.SelectView();


            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "MktSurvey":
                    this.txtInfo.Visible = false;
                    this.LinkButton1.Visible = false;
                    this.lblInformation.Visible = false;
                    this.ddlSurveyType.Visible = false;

                    this.MultiView1.ActiveViewIndex = 0;
                    this.lbtnMSROk.Text = "New";
                    this.lbtnMSROk_Click(null, null);
                    this.ImgbtnFindMat_Click(null, null);
                    //this.ImgbtnFindSup_Click(null, null);
                    //this.lbtnMSRSupl_Click(null, null);

                    break;
                case "SurveyLink":
                    this.ddlSurveyType.Items[0].Enabled = false;
                    this.ddlSurveyType.Items[1].Enabled = true;
                    this.ddlSurveyType.Items[2].Selected = true;
                    this.ImgbtnFindSupl2_Click(null, null);
                    this.ddlSurveyType_SelectedIndexChanged(null, null);

                    break;




            }




        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            ReportClass rptstk = null;
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
            DataTable dt = (DataTable)Session["ResSupl"];
            LocalReport Rpt1 = new LocalReport();
            if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "1")
            {

                this.PrintMarketSurvey();
                return;

            }
            else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "2")
            {


                var lst = dt.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptMatWiseSupList>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptMktSurveyMatWiseSupList", lst, null, null);
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Matrial Wise Supplier List Report"));





                //RealERPRPT.R_14_Pro.RptMktSurveyMatWiseSupList rptstk2 = new RealERPRPT.R_14_Pro.RptMktSurveyMatWiseSupList();
                //rptstk2.SetDataSource((DataTable)Session["ResSupl"]);
                //Session["Report1"] = rptstk2;
                //rptstk = rptstk2;
            }
            else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "3")
            {
                DataTable dt2 = (DataTable)Session["SuplRes"];

                var lst = dt2.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptSupWiseMatList>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptMktSurveySupWiseMatList", lst, null, null);
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Supplier Wise Matrial List Report"));


                //RealERPRPT.R_14_Pro.RptMktSurveySupWiseMatList rptstk3 = new RealERPRPT.R_14_Pro.RptMktSurveySupWiseMatList();
                //rptstk3.SetDataSource((DataTable)Session["SuplRes"]);
                //Session["Report1"] = rptstk3;
                //rptstk = rptstk3;
            }


            string ss = this.lblCurMSRNo1.Text.Trim();
            string ss1 = this.txtCurMSRNo2.Text.Trim();




            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("surveynoname", "Survey No.: " + this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Survey Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtMSRNarr.Text.ToString()));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            ////TextObject txtCompanyAddress = rptstk.ReportDefinition.ReportObjects["companyaddress"] as TextObject;
            ////txtCompanyAddress.Text = ConstantInfo.ComAdd;
            //TextObject txtsurveynoname = rptstk.ReportDefinition.ReportObjects["surveynoname"] as TextObject;
            //txtsurveynoname.Text = this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim();
            ////TextObject txtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            ////txtadate.Text = this.txtApprovalDate.Text.ToString().Trim();
            //TextObject txtnarrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //txtnarrationname.Text = this.txtMSRNarr.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = this.Label1.Text;
            //    string eventdesc = "Print Report Survey";
            //    string eventdesc2 = this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintMarketSurvey()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            switch (comcod)
            {

                // case "3101":
                case "3335":
                    this.PrintEdisonMarSurvey();

                    break;

                default:
                    this.PringenMarSurvey();
                    break;


            }


        }

        private void PringenMarSurvey()
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

            DataTable dt1 = (DataTable)Session["tblMSR"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptPurMktSurvey>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurvey", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtmsrdate", this.txtCurMSRDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtExpiredDate", Convert.ToDateTime(this.GetStdDate(this.txtCurMSRDate.Text.Trim())).AddDays(30).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("surveynoname", this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtMSRNarr.Text.ToString()));

            Rpt1.SetParameters(new ReportParameter("rptTitle", "Comparative Statement  Of Materials"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //ReportClass rptstk = null;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)Session["tblMSR"];
            //RealERPRPT.R_14_Pro.RptPurMktSurvey rptstk1 = new RealERPRPT.R_14_Pro.RptPurMktSurvey();

            //TextObject txtmsrdate = rptstk1.ReportDefinition.ReportObjects["txtmsrdate"] as TextObject;
            //txtmsrdate.Text = this.txtCurMSRDate.Text.Trim();

            //TextObject txtExpiredDate = rptstk1.ReportDefinition.ReportObjects["txtExpiredDate"] as TextObject;
            //txtExpiredDate.Text = Convert.ToDateTime(this.GetStdDate(this.txtCurMSRDate.Text.Trim())).AddDays(30).ToString("dd.MM.yyyy");

            //rptstk1.SetDataSource((DataTable)Session["tblMSR"]);
            //Session["Report1"] = rptstk1;
            //rptstk = rptstk1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEdisonMarSurvey()
        {
            // ReportClass rptstk = null;
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

            DataTable dt = (DataTable)Session["tblMSR"];

            List<RealEntity.C_14_Pro.EClassPur.EClassSupAmt> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassSupAmt>();
            foreach (DataRow dr in dt.Rows)
            {

                string rsircode = dr["ssircode"].ToString();
                string rsirdesc = dr["ssirdesc1"].ToString();
                double samount = Convert.ToDouble(dr["amount"].ToString());

                lst.Add(new RealEntity.C_14_Pro.EClassPur.EClassSupAmt(rsircode, rsirdesc, samount));


            }





            var lsts = (from lst1 in lst
                        group lst1 by lst1.rsirdesc into g

                        select new SumClass
                        {
                            rsirdesc = g.Key,
                            samount = g.Sum(lst1 => lst1.samount)
                        }).ToList();




            LocalReport Rpt1 = new LocalReport();

            var lst2 = dt.DataTableToList<RealEntity.C_12_Inv.PurEqisition.RptPurMktSurvey>();
            //var lst3 = dt.ListToDataTable<RealEntity.C_12_Inv.PurEqisition.RptPurMktSurveySummary>();





            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyEdison", lst2, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtmsrdate", this.txtCurMSRDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtExpiredDate", Convert.ToDateTime(this.GetStdDate(this.txtCurMSRDate.Text.Trim())).AddDays(30).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("surveynoname", this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtMSRNarr.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Comparative Statement  Of Materials"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rptstk1 =new RealERPRPT.R_14_Pro.RptPurMktSurveyEdison();

            //TextObject txtmsrdate = rptstk1.ReportDefinition.ReportObjects["txtmsrdate"] as TextObject;
            //txtmsrdate.Text = this.txtCurMSRDate.Text.Trim();
            //TextObject txtExpiredDate = rptstk1.ReportDefinition.ReportObjects["txtExpiredDate"] as TextObject;
            //txtExpiredDate.Text = Convert.ToDateTime(this.GetStdDate(this.txtCurMSRDate.Text.Trim())).AddDays(30).ToString("dd.MM.yyyy");



            //TextObject narrationname = rptstk1.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //narrationname.Text = this.txtMSRNarr.Text.Trim();

            //rptstk1.SetDataSource((DataTable)Session["tblMSR"]);
            //rptstk1.Subreports["RptPurMktSurveySupDet.rpt"].SetDataSource(lsts);
            //Session["Report1"] = rptstk1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        class SumClass
        {

            public string rsirdesc { get; set; }
            public double samount { get; set; }

        }
        protected void ddlSurveyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            int indeex = this.ddlSurveyType.SelectedIndex;
            this.MultiView1.ActiveViewIndex = this.ddlSurveyType.SelectedIndex;
            switch (this.ddlSurveyType.SelectedIndex)
            {
                case 0:
                    this.lbtnMSROk.Text = "New";
                    this.lbtnMSROk_Click(null, null);
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }

        }

        protected void lbtnPrevMSRList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREVMSRLIST", CurDate1,
                          "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevMSRList.Items.Clear();
            this.ddlPrevMSRList.DataTextField = "msrno1";
            this.ddlPrevMSRList.DataValueField = "msrno";
            this.ddlPrevMSRList.DataSource = ds1.Tables[0];
            this.ddlPrevMSRList.DataBind();
        }
        protected void lbtnMSROk_Click(object sender, EventArgs e)
        {
            if (this.lbtnMSROk.Text == "New")
            {
                this.ImgbtnFindPreMR.Visible = true;
                this.ddlPrevMSRList.Visible = true;
                //this.lblPreMrList.Visible = true;
                //this.txtPreMSRSearch.Visible = true;
                this.ddlPrevMSRList.Items.Clear();

                // this.txtCurMSRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.lblCurMSRNo1.Text = "MSR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMSRDate.Enabled = true;

                this.txtMSRResSearch.Text = "";
                this.ddlMSRRes.Items.Clear();
                this.ddlMSRSupl.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtMSRNarr.Text = "";

                this.gvMSRInfo.DataSource = null;
                this.gvMSRInfo.DataBind();

                this.Panel1.Visible = false;
                this.lbtnMSROk.Text = "Ok";

                return;
            }
            this.ImgbtnFindPreMR.Visible = false;
            this.ddlPrevMSRList.Visible = false;
            //this.lblPreMrList.Visible = false;
            //this.txtPreMSRSearch.Visible = false;


            this.txtCurMSRDate.ReadOnly = true;
            this.txtCurMSRNo2.ReadOnly = true;


            this.Panel1.Visible = true;
            this.lbtnMSROk.Text = "New";
            this.Get_Survey_Info();


            //this.ImgbtnFindSup_Click(null, null);

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        //protected void Get_Last_Survey_Info()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO", "MAXMSRNO",
        //                  "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    this.lblLastMSRDate.Text = "";
        //    this.lblLastMSRNo.Text = "";
        //    if (ds1.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds1.Tables[0].Rows[0]["maxmsrno"].ToString() != "XXXXXXXXXXXXXX")
        //        {
        //            this.lblLastMSRDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["maxmsrdt"]).ToString("dd.MM.yyyy");
        //            this.lblLastMSRNo.Text = ds1.Tables[0].Rows[0]["maxmsrno1"].ToString();
        //        }
        //    }
        //}

        protected void Get_Survey_Info()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string mMSRNo = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
            {
                this.txtCurMSRDate.Enabled = false;
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO", mMSRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblMSR"] = this.HiddenSameData(ds1.Tables[0]);


            if (mMSRNo == "NEWMSR")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMSRNo1.Text = ds1.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds1.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.lblCurMSRNo1.Text = ds1.Tables[1].Rows[0]["msrno1"].ToString().Substring(0, 6);
            this.txtCurMSRNo2.Text = ds1.Tables[1].Rows[0]["msrno1"].ToString().Substring(6, 5);
            this.txtCurMSRDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["msrdat"]).ToString("dd.MM.yyyy");
            //this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["msrbydes"].ToString();
            //this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            //this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtMSRNarr.Text = ds1.Tables[1].Rows[0]["msrnar"].ToString();

            this.gvMSRInfo_DataBind();


        }

        protected void gvMSRInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblMSR"];
            tbl1 = this.HiddenSameData(tbl1);
            this.gvMSRInfo.DataSource = tbl1;
            this.gvMSRInfo.DataBind();
            ((DropDownList)this.gvMSRInfo.FooterRow.FindControl("ddlMSRPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvMSRInfo.PageSize);
            ((DropDownList)this.gvMSRInfo.FooterRow.FindControl("ddlMSRPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvMSRInfo.FooterRow.FindControl("ddlMSRPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.gvMSRInfo.FooterRow.FindControl("ddlMSRPageNo")).Visible = true;
            ((DropDownList)this.gvMSRInfo.FooterRow.FindControl("ddlMSRPageNo")).SelectedIndex = this.gvMSRInfo.PageIndex;

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string Type = this.Request.QueryString["Type"].ToString();
            string rescod;
            switch (Type)
            {
                case "MktSurvey":
                    rescod = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rescod)
                            dt1.Rows[j]["rsirdesc1"] = "";
                        rescod = dt1.Rows[j]["rsircode"].ToString();
                    }

                    break;

                case "SurveyLink":
                    string index = this.ddlSurveyType.SelectedValue.ToString();
                    switch (index)
                    {

                        case "2":
                            string ssircode = dt1.Rows[0]["ssircode"].ToString();
                            for (int j = 1; j < dt1.Rows.Count; j++)
                            {
                                if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                                    dt1.Rows[j]["ssirdesc1"] = "";
                                ssircode = dt1.Rows[j]["ssircode"].ToString();
                            }
                            break;


                        case "3":

                            rescod = dt1.Rows[0]["rsircode"].ToString();
                            for (int j = 1; j < dt1.Rows.Count; j++)
                            {
                                if (dt1.Rows[j]["rsircode"].ToString() == rescod)
                                    dt1.Rows[j]["rsirdesc1"] = "";
                                rescod = dt1.Rows[j]["rsircode"].ToString();
                            }
                            break;


                    }



                    break;



            }






            return dt1;

        }




        protected void ddlMSRPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
            this.gvMSRInfo.PageIndex = ((DropDownList)this.gvMSRInfo.FooterRow.FindControl("ddlMSRPageNo")).SelectedIndex;
            this.gvMSRInfo_DataBind();
        }

        protected void Session_tblMSR_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblMSR"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvMSRInfo.Rows.Count; j++)
            {
                double dgvLpRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMSRInfo.Rows[j].FindControl("lblgvLRate")).Text.Trim()));
                double dgvMSRRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRRate")).Text.Trim()));
                double dgvMSRqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRqty")).Text.Trim()));
                string dgvMSRRemarks = ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRRemarks")).Text.Trim();
                //string dgvMSRBrand = ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvbrand")).Text.Trim();
                double dgvMSRDelivery = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRDel")).Text.Trim()));
                double dgvMSRPay = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRPayment")).Text.Trim()));
                //double gvPayLimit = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMSRInfo.Rows[j].FindControl("lblgvPayLimit")).Text.Trim()));

                ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRRate")).Text = dgvMSRRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRqty")).Text = dgvMSRqty.ToString("#,##0.00;(#,##0.00); ");

                TblRowIndex2 = (this.gvMSRInfo.PageIndex) * this.gvMSRInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["maxrate"] = dgvLpRate;
                tbl1.Rows[TblRowIndex2]["resrate"] = dgvMSRRate;
                tbl1.Rows[TblRowIndex2]["reqqty"] = dgvMSRqty;
                tbl1.Rows[TblRowIndex2]["msrrmrk"] = dgvMSRRemarks;
                // tbl1.Rows[TblRowIndex2]["brand"] = dgvMSRBrand;
                tbl1.Rows[TblRowIndex2]["delivery"] = dgvMSRDelivery;
                tbl1.Rows[TblRowIndex2]["payment"] = dgvMSRPay;
                tbl1.Rows[TblRowIndex2]["amount"] = dgvMSRqty * dgvMSRRate;
                //tbl1.Rows[TblRowIndex2]["paylimit"] = gvPayLimit;
            }


            Session["tblMSR"] = tbl1;
        }



        protected void lbtnMSRResList_Click(object sender, EventArgs e)
        {
            string mSrchTxt = this.txtMSRResSearch.Text.Trim() + "%";
            this.Resource_List(mSrchTxt);
            this.ddlMSRRes.DataTextField = "rsirdesc1";
            this.ddlMSRRes.DataValueField = "rsircode";
            this.ddlMSRRes.DataSource = (DataTable)Session["tblMat"];
            this.ddlMSRRes.DataBind();
            this.lbtnMSRSupl_Click(null, null);

            // this.lbtnMSRResSpcf_Click(null, null);
        }


        protected void lbtnMSRSelect_Click(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
            DataTable tbl1 = (DataTable)Session["tblMSR"];
            string mResCode = this.ddlMSRRes.SelectedValue.ToString();
            string mSpcfCod = this.ddlSpecificationms.SelectedValue.ToString();
            string mSuplCode = this.ddlMSRSupl.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and ssircode = '" + mSuplCode + "' and spcfcod = '" + mSpcfCod + "'");
            if (dr2.Length == 0)
            {



                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlMSRRes.SelectedValue.ToString();
                dr1["spcfcod"] = mSpcfCod;
                dr1["ssircode"] = this.ddlMSRSupl.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlMSRRes.SelectedItem.Text.Trim().Substring(15);
                dr1["rsirdesc1"] = this.ddlMSRRes.SelectedItem.Text.Trim().Substring(15);
                dr1["spcfdesc"] = this.ddlSpecificationms.SelectedItem.Text;
                dr1["ssirdesc1"] = this.ddlMSRSupl.SelectedItem.Text.Trim().Substring(15);
                DataTable tbl2 = (DataTable)Session["tblMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["maxrate"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["maxrate"].ToString();
                dr1["resrate"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["rate"].ToString();
                dr1["msrrmrk"] = "";
                dr1["cperson"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["cperson"].ToString();
                dr1["phone"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["phone"].ToString();
                dr1["mobile"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["mobile"].ToString();
                dr1["reqqty"] = 0;
                dr1["amount"] = 0;

                dr1["brand"] = "";
                dr1["delivery"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["delsys"].ToString();
                dr1["payment"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["paysys"].ToString();
                dr1["paylimit"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["paylimit"].ToString();

                tbl1.Rows.Add(dr1);
            }
            Session["tblMSR"] = tbl1;
            //int RowNo = 1;
            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{
            //    if (tbl1.Rows[i]["rsircode"].ToString() == mResCode && tbl1.Rows[i]["ssircode"].ToString() == mSuplCode)
            //    {
            //        RowNo = i + 1;
            //        break;
            //    }
            //}
            //double PageNo = Math.Ceiling(RowNo * 1.00 / this.gvMSRInfo.PageSize);
            //this.gvMSRInfo.PageIndex = Convert.ToInt32(PageNo - 1);
            this.gvMSRInfo_DataBind();





            //string ssircode = this.ddlMSRSupl.SelectedValue.ToString().Trim();

            //if (this.ddlMSRSupl.Items.Count < 0)
            //{
            //    this.lCperson.Text = "";
            //    this.lPhone.Text = "";
            //    this.lMob.Text = "";
            //    return;
            //}

            //DataTable dtsup = (DataTable)Session["Supplier"];


            //DataRow[] dr1 = dtsup.Select("(ssircode = '" + ssircode + "')");
            //if (dr1.Length > 0)
            //{
            //    this.lCperson.Text = dr1[0]["cperson"].ToString();
            //    this.lPhone.Text = dr1[0]["phone"].ToString(); ;
            //    this.lMob.Text = dr1[0]["mobile"].ToString(); ;

            //}
            //else
            //{
            //    this.lCperson.Text = "";
            //    this.lPhone.Text = "";
            //    this.lMob.Text = "";

            //}

        }
        protected void lbtnMSRTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
            this.gvMSRInfo_DataBind();
        }
        protected void lbtnMSRUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            this.Session_tblMSR_Update();
            string mMSRNO = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
                mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();
            string mMSRDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            if (mMSRNO == "NEWMSR")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO", mMSRDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mMSRNO = ds2.Tables[0].Rows[0]["maxmsrno"].ToString();
                    this.lblCurMSRNo1.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);

                    this.ddlPrevMSRList.DataTextField = "maxmsrno1";
                    this.ddlPrevMSRList.DataValueField = "maxmsrno";
                    this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                    this.ddlPrevMSRList.DataBind();
                }
                else
                    return;
            }
            string PostedByid = hst["usrid"].ToString();
            string Postedtrmid = hst["compname"].ToString();
            string PostedSession = hst["session"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            string mMSRNAR = this.txtMSRNarr.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURMSRINFO", "PURMSRB",
                             mMSRNO, mMSRDAT, PostedByid, Postedtrmid, PostedSession, PostedDat, mMSRNAR, "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            DataTable tbl1 = (DataTable)Session["tblMSR"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRESRATE = tbl1.Rows[i]["resrate"].ToString();
                string mMSRRMRK = tbl1.Rows[i]["msrrmrk"].ToString();
                string mMSRRQty = tbl1.Rows[i]["reqqty"].ToString();
                string mMSRRBrand = tbl1.Rows[i]["brand"].ToString();
                string mMSRRDelivery = tbl1.Rows[i]["delivery"].ToString();
                string mMSRRPay = tbl1.Rows[i]["payment"].ToString();
                string mMaxrate = tbl1.Rows[i]["maxrate"].ToString();
                string mPaylimit = tbl1.Rows[i]["paylimit"].ToString();

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURMSRINFO", "PURMSRA",
                         mMSRNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mRESRATE, mMSRRMRK, mMSRRQty, mMSRRBrand, mMSRRDelivery, mMSRRPay, mMaxrate, mPaylimit, "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.Label1.Text;
                string eventdesc = "Update Survey";
                string eventdesc2 = mMSRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        //protected void lbtnMSRResSpcf_Click(object sender, EventArgs e)
        //{
        //    string mResCode = this.ddlMSRRes.SelectedValue.ToString().Substring(0, 9);
        //    this.ddlMSRResSpcf.Items.Clear();
        //    DataTable tbl1 = (DataTable)Session["tblSpcf"];
        //    DataView dv1 = tbl1.DefaultView;
        //    dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";

        //    this.ddlMSRResSpcf.DataTextField = "spcfdesc";
        //    this.ddlMSRResSpcf.DataValueField = "spcfcod";
        //    this.ddlMSRResSpcf.DataSource = dv1;
        //    this.ddlMSRResSpcf.DataBind();

        //    this.lbtnMSRSupl_Click(null, null);
        //}

        protected void ddlMSRResSpcf_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnMSRSupl_Click(null, null);
        }


        protected void lbtnMSRSupl_Click(object sender, EventArgs e)
        {
            Session.Remove("Supplier");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = this.txtMSRSupSearch.Text.Trim() + "%";
            string mResCode = this.ddlMSRRes.SelectedValue.ToString();
            string mSpcfCod = "000000000000";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, mResCode, mSpcfCod, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["Supplier"] = ds1.Tables[0];
            this.ddlMSRSupl.DataTextField = "ssirdesc1";
            this.ddlMSRSupl.DataValueField = "ssircode";
            this.ddlMSRSupl.DataSource = ds1.Tables[0];
            this.ddlMSRSupl.DataBind();


        }




        protected void Resource_List(string pmSrchTxt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRRESLIST", pmSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];
        }

        protected void ImgbtnFindRes1_Click(object sender, EventArgs e)
        {
            string mSrchTxt = "%" + this.txtResSearch1.Text.Trim() + "%";
            this.Resource_List(mSrchTxt);
            this.ddlResList1.DataTextField = "rsirdesc1";
            this.ddlResList1.DataValueField = "rsircode";
            this.ddlResList1.DataSource = (DataTable)Session["tblMat"];
            this.ddlResList1.DataBind();
            this.GetSpecification01();

        }


        private void GetSpecification01()
        {

            DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();
            string Resource01 = this.ddlResList1.SelectedValue.ToString().Substring(0, 9);
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("mspcfcod='" + Resource01 + "' or mspcfcod='000000000'");
            //dv.Sort = ("wrkcode, rsircode");
            dt = dv.ToTable();


            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = dt;
            this.ddlSpecification.DataBind();

        }



        private void GetSpecification02()
        {

            DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();
            string Resource02 = this.ddlResList2.SelectedValue.ToString().Substring(0, 9);
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("mspcfcod='" + Resource02 + "' or mspcfcod='000000000'");
            dt = dv.ToTable();


            //Search
            string srchspcf = this.txtsrchSpecification2.Text.Trim();

            if (srchspcf.Length > 0)
            {
                var results = (from srchrow in dt.AsEnumerable()
                               where srchrow.Field<string>("spcfdesc").ToUpper().Contains(srchspcf.ToUpper())
                               select srchrow);
                dv = results.AsDataView();

            }

            else
            {

                dv = dt.DefaultView;

            }
            dt = dv.ToTable();


            this.ddlSpecification02.DataTextField = "spcfdesc";
            this.ddlSpecification02.DataValueField = "spcfcod";
            this.ddlSpecification02.DataSource = dt;
            this.ddlSpecification02.DataBind();

        }

        protected void ImgbtnFindSpecification_Click(object sender, EventArgs e)
        {
            this.GetSpecification01();

        }
        protected void ddlResList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification01();
        }
        protected void ImgbtnFindSpecification2_Click(object sender, EventArgs e)
        {
            this.GetSpecification02();
        }
        protected void ddlResList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification02();
        }



        protected void ImgbtnFindRes2_Click(object sender, EventArgs e)
        {
            string mSrchTxt = "%" + this.txtResSearch2.Text.Trim() + "%";
            this.Resource_List(mSrchTxt);

            this.ddlResList2.DataTextField = "rsirdesc1";
            this.ddlResList2.DataValueField = "rsircode";
            this.ddlResList2.DataSource = (DataTable)Session["tblMat"];
            this.ddlResList2.DataBind();
            this.GetSpecification02();


        }



        protected void ImgbtnFindSupl1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%" + this.txtSuplSearch1.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlSupl1.DataTextField = "ssirdesc1";
            this.ddlSupl1.DataValueField = "ssircode";
            this.ddlSupl1.DataSource = ds1.Tables[0];
            this.ddlSupl1.DataBind();
        }

        protected void lbtnSelectRes1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mResCode = this.ddlResList1.SelectedValue.ToString();
            //string mSpcfCod = "000000000000";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETRESSUPLRLIST", mResCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["ResSupl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Panel2.Visible = true;
            this.gvSuplInfo_DataBind();
        }

        protected void gvSuplInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["ResSupl"];
            this.gvSuplInfo.DataSource = tbl1;
            this.gvSuplInfo.DataBind();
            if (tbl1.Rows.Count == 0)
                return;
            ((DropDownList)this.gvSuplInfo.FooterRow.FindControl("ddlSuplPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvSuplInfo.PageSize);
            ((DropDownList)this.gvSuplInfo.FooterRow.FindControl("ddlSuplPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvSuplInfo.FooterRow.FindControl("ddlSuplPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.gvSuplInfo.FooterRow.FindControl("ddlSuplPageNo")).Visible = true;
            ((DropDownList)this.gvSuplInfo.FooterRow.FindControl("ddlSuplPageNo")).SelectedIndex = this.gvSuplInfo.PageIndex;
        }

        protected void ddlSuplPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblResSupl_Update();
            this.gvSuplInfo.PageIndex = ((DropDownList)this.gvSuplInfo.FooterRow.FindControl("ddlSuplPageNo")).SelectedIndex;
            this.gvSuplInfo_DataBind();
        }

        private void Session_tblResSupl_Update()
        {
            DataTable tbl1 = (DataTable)Session["ResSupl"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvSuplInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvSuplInfo.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();
                double gvrate1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvSuplInfo.Rows[j].FindControl("txtgvRate1")).Text.Trim()));


                TblRowIndex2 = (this.gvSuplInfo.PageIndex) * this.gvSuplInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["rmrks"] = dgvRemarks;
                tbl1.Rows[TblRowIndex2]["rate"] = gvrate1;

            }
            Session["ResSupl"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            //this.Session_tblResSupl_Update();
            //DataTable tbl1 = (DataTable)Session["ResSupl"];
            //string mSuplCode = this.ddlSupl1.SelectedValue.ToString();
            //string mSpcfCod = this.ddlSpecification.SelectedValue.ToString();
            //DataRow[] dr2 = tbl1.Select("ssircode = '" + mSuplCode + "' and  spcfcod='" + mSpcfCod + "'");
            //if (dr2.Length == 0)
            //{
            //    DataRow dr1 = tbl1.NewRow();
            //    dr1["rsircode"] = this.ddlResList1.SelectedValue.ToString();
            //    dr1["ssircode"] = this.ddlSupl1.SelectedValue.ToString();
            //    dr1["ssirdesc1"] = this.ddlSupl1.SelectedItem.Text.Trim();
            //    dr1["spcfcod"] = this.ddlSpecification.SelectedValue.ToString();
            //    dr1["spcfdesc"] = this.ddlSpecification.SelectedItem.Text.Trim();
            //    dr1["rmrks"] = "";
            //    dr1["rmrks"] = "";
            //    dr1["delsys"] = 0.00;
            //    dr1["paysys"] = 0.00;
            //    dr1["rate"] = 0.00;
            //    dr1["paylimit"] = 0.00;
            //    tbl1.Rows.Add(dr1);
            //}


            //DataView dv = tbl1.DefaultView;
            //dv.Sort = ("ssircode, spcfcod");
            //tbl1 = this.HiddenSameData(dv.ToTable());
            //Session["SuplRes"] = tbl1;
            //this.gvSuplInfo_DataBind();


            this.Session_tblSuplRes_Update();
            DataTable tbl1 = (DataTable)Session["ResSupl"];

            string mResCode = this.ddlResList1.SelectedValue.ToString();
            string mSuplCode = this.ddlSupl1.SelectedValue.ToString();
            string mSpcfCod = this.ddlSpecification.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("ssircode = '" + mSuplCode + "' and  spcfcod='" + mSpcfCod + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["ssircode"] = this.ddlSupl1.SelectedValue.ToString();
                dr1["rsircode"] = this.ddlResList1.SelectedValue.ToString();
                dr1["ssirdesc1"] = this.ddlSupl1.SelectedItem.Text.Trim();
                dr1["spcfcod"] = this.ddlSpecification.SelectedValue.ToString();
                dr1["spcfdesc"] = this.ddlSpecification.SelectedItem.Text.Trim();
                dr1["rmrks"] = "";
                dr1["delsys"] = 0.00;
                dr1["paysys"] = 0.00;
                dr1["rate"] = (Convert.ToDouble((((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"].ToString()) == 0.00) ? (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"] : (((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"];
                dr1["paylimit"] = 0.00;
                tbl1.Rows.Add(dr1);
            }

            DataView dv = tbl1.DefaultView;
            dv.Sort = ("ssircode, spcfcod");
            tbl1 = this.HiddenSameData(dv.ToTable());
            Session["ResSupl"] = tbl1;
            this.gvSuplInfo_DataBind();



        }


        protected void lbtnSelectSupAll_Click(object sender, EventArgs e)
        {


            this.Session_tblSuplRes_Update();
            DataTable tbl1 = (DataTable)Session["ResSupl"];

            string mResCode = this.ddlResList1.SelectedValue.ToString();
            string mSpcfCod = this.ddlSpecification.SelectedValue.ToString();

            for (int i = 0; i < this.ddlSupl1.Items.Count; i++)
            {


                string mSuplCode = this.ddlSupl1.Items[i].Value.ToString();
                DataRow[] dr2 = tbl1.Select("ssircode = '" + mSuplCode + "' and  spcfcod='" + mSpcfCod + "'");
                if (dr2.Length == 0)
                {

                    DataRow dr1 = tbl1.NewRow();
                    dr1["ssircode"] = mSuplCode;
                    dr1["rsircode"] = mResCode;
                    dr1["ssirdesc1"] = this.ddlSupl1.Items[i].Text.Trim();
                    dr1["spcfcod"] = mSpcfCod;
                    dr1["spcfdesc"] = this.ddlSpecification.SelectedItem.Text.Trim();
                    dr1["rmrks"] = "";
                    dr1["delsys"] = 0.00;
                    dr1["paysys"] = 0.00;
                    dr1["rate"] = (Convert.ToDouble((((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"].ToString()) == 0.00) ? (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"] : (((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"];
                    dr1["paylimit"] = 0.00;
                    tbl1.Rows.Add(dr1);



                }


            }

            DataView dv = tbl1.DefaultView;
            dv.Sort = ("ssircode, spcfcod");
            tbl1 = this.HiddenSameData(dv.ToTable());
            Session["ResSupl"] = tbl1;
            this.gvSuplInfo_DataBind();

        }


        protected void gvSuplInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["ResSupl"];

            string Supcode = ((Label)this.gvSuplInfo.Rows[e.RowIndex].FindControl("lblgvSuplCodsup")).Text.Trim();
            string Rescode = ((Label)this.gvSuplInfo.Rows[e.RowIndex].FindControl("lblgvResCodsup")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETESUPLRES",
                             Supcode, Rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (result == true)
            {
                int rowindex = (this.gvSuplInfo.PageSize) * (this.gvSuplInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("ResSupl");
            Session["ResSupl"] = dv.ToTable();
            this.gvSuplInfo_DataBind();

        }

        protected void lbtnSuplUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.Session_tblResSupl_Update();
            DataTable tbl1 = (DataTable)Session["ResSupl"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mRMRKS = tbl1.Rows[i]["rmrks"].ToString();
                string mDelsys = tbl1.Rows[i]["delsys"].ToString();
                string mPaysys = tbl1.Rows[i]["paysys"].ToString();
                string mRate = tbl1.Rows[i]["rate"].ToString();
                string mPaylimit = tbl1.Rows[i]["paylimit"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLRES",
                              mSSIRCODE, mRSIRCODE, mSPCFCOD, mRMRKS, mDelsys, mPaysys, mRate, mPaylimit, "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.Label1.Text;
                string eventdesc = "Update Supplier";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ImgbtnFindSupl2_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%" + this.txtSuplSearch2.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlSupl2.DataTextField = "ssirdesc1";
            this.ddlSupl2.DataValueField = "ssircode";
            this.ddlSupl2.DataSource = ds1.Tables[0];
            this.ddlSupl2.DataBind();
        }

        protected void lbtnSelectSupl2_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSuplCode = this.ddlSupl2.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETSUPLRRESLIST", mSuplCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["SuplRes"] = this.HiddenSameData(ds1.Tables[0]);
            this.Panel3.Visible = true;
            this.gvResInfo_DataBind();
            this.ImgbtnFindRes2_Click(null, null);
        }

        protected void gvResInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["SuplRes"];
            this.gvResInfo.DataSource = tbl1;
            this.gvResInfo.DataBind();
            if (tbl1.Rows.Count == 0)
                return;
            ((DropDownList)this.gvResInfo.FooterRow.FindControl("ddlResPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvResInfo.PageSize);
            ((DropDownList)this.gvResInfo.FooterRow.FindControl("ddlResPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvResInfo.FooterRow.FindControl("ddlResPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.gvResInfo.FooterRow.FindControl("ddlResPageNo")).Visible = true;
            ((DropDownList)this.gvResInfo.FooterRow.FindControl("ddlResPageNo")).SelectedIndex = this.gvResInfo.PageIndex;
        }

        protected void ddlResPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblSuplRes_Update();
            this.gvResInfo.PageIndex = ((DropDownList)this.gvResInfo.FooterRow.FindControl("ddlResPageNo")).SelectedIndex;
            this.gvResInfo_DataBind();
        }

        private void Session_tblSuplRes_Update()
        {
            DataTable tbl1 = (DataTable)Session["SuplRes"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvResInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvResRemarks1")).Text.Trim();
                double gvrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvRate")).Text.Trim()));


                TblRowIndex2 = (this.gvResInfo.PageIndex) * this.gvResInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["rmrks"] = dgvRemarks;
                tbl1.Rows[TblRowIndex2]["rate"] = gvrate;
            }
            Session["SuplRes"] = tbl1;
        }

        protected void lbtnSelectRes2_Click(object sender, EventArgs e)
        {
            this.Session_tblSuplRes_Update();
            DataTable tbl1 = (DataTable)Session["SuplRes"];
            DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();

            string mResCode = this.ddlResList2.SelectedValue.ToString();
            string mSpcfCod = this.ddlSpecification02.SelectedValue.ToString();

            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and  spcfcod='" + mSpcfCod + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["ssircode"] = this.ddlSupl2.SelectedValue.ToString();
                dr1["rsircode"] = this.ddlResList2.SelectedValue.ToString();
                dr1["rsirdesc1"] = this.ddlResList2.SelectedItem.Text.Trim();
                dr1["spcfcod"] = this.ddlSpecification02.SelectedValue.ToString();
                dr1["spcfdesc"] = this.ddlSpecification02.SelectedItem.Text.Trim();
                dr1["rsirunit"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["rsirunit"];
                dr1["rmrks"] = "";
                dr1["delsys"] = 0.00;
                dr1["paysys"] = 0.00;
                dr1["rate"] = (Convert.ToDouble((((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"].ToString()) == 0.00) ? (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"] : (((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"];
                dr1["paylimit"] = 0.00;
                tbl1.Rows.Add(dr1);
            }

            DataView dv = tbl1.DefaultView;
            dv.Sort = ("rsircode, spcfcod");
            tbl1 = this.HiddenSameData(dv.ToTable());
            Session["SuplRes"] = tbl1;

            this.gvResInfo_DataBind();
        }

        protected void lbtnSelectResAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["SuplRes"];

            string mResCode = this.ddlResList2.SelectedValue.ToString();
            string rsirunit = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["rsirunit"].ToString();

            for (int i = 0; i < this.ddlSpecification02.Items.Count; i++)
            {

                string mSpcfCod = this.ddlSpecification02.Items[i].Value.ToString();
                DataRow[] dr2 = dt.Select("rsircode = '" + mResCode + "' and  spcfcod='" + mSpcfCod + "'");
                if (dr2.Length == 0)
                {

                    DataRow dr1 = dt.NewRow();
                    dr1["ssircode"] = this.ddlSupl2.SelectedValue.ToString();
                    dr1["rsircode"] = this.ddlResList2.SelectedValue.ToString(); ;
                    dr1["rsirdesc1"] = this.ddlResList2.SelectedItem.Text.Trim();
                    dr1["spcfcod"] = this.ddlSpecification02.Items[i].Value.ToString();
                    dr1["spcfdesc"] = this.ddlSpecification02.Items[i].Text.Trim();
                    dr1["rsirunit"] = rsirunit;
                    dr1["rmrks"] = "";
                    dr1["delsys"] = 0.00;
                    dr1["paysys"] = 0.00;
                    dr1["rate"] = dr1["rate"] = (Convert.ToDouble((((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"].ToString()) == 0.00) ? (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"] : (((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"];
                    dr1["paylimit"] = 0.00;
                    dt.Rows.Add(dr1);

                }



            }

            DataView dv = dt.DefaultView;
            dv.Sort = ("rsircode, spcfcod");
            dt = this.HiddenSameData(dv.ToTable());
            Session["SuplRes"] = dt;
            this.gvResInfo_DataBind();






        }
        protected void lbtnSelectReaSpesAll_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["SuplRes"];
            DataTable dt2 = ((DataTable)Session["tblSpcf"]).Copy();
            // DataTable dt3 = new DataTable(); 

            for (int i = 0; i < this.ddlResList2.Items.Count; i++)
            {

                string mResCode = this.ddlResList2.Items[i].Value.ToString();
                string msmcfcod = this.ddlResList2.Items[i].Value.ToString().Substring(0, 9);
                string rsirunit = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["rsirunit"].ToString();
                DataView dv = dt2.DefaultView;
                dv.RowFilter = ("mspcfcod='" + msmcfcod + "' or mspcfcod='000000000'");
                DataTable dt3 = dv.ToTable();
                for (int j = 0; j < dt3.Rows.Count; j++)
                {

                    string mSpcfCod = dt3.Rows[j]["spcfcod"].ToString();
                    DataRow[] dr2 = dt.Select("rsircode = '" + mResCode + "' and  spcfcod='" + mSpcfCod + "'");
                    if (dr2.Length == 0)
                    {

                        DataRow dr1 = dt.NewRow();
                        dr1["ssircode"] = this.ddlSupl2.SelectedValue.ToString();
                        dr1["rsircode"] = mResCode;
                        dr1["rsirdesc1"] = this.ddlResList2.Items[i].Text.ToString();
                        dr1["spcfcod"] = mSpcfCod;
                        dr1["spcfdesc"] = dt3.Rows[j]["spcfdesc"].ToString();
                        dr1["rsirunit"] = rsirunit;
                        dr1["rmrks"] = "";
                        dr1["delsys"] = 0.00;
                        dr1["paysys"] = 0.00;
                        dr1["rate"] = dr1["rate"] = (Convert.ToDouble((((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"].ToString()) == 0.00) ? (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"] : (((DataTable)Session["tblSpcf"]).Select("spcfcod='" + mSpcfCod + "'"))[0]["stdrat"]; ;
                        dr1["paylimit"] = 0.00;
                        dt.Rows.Add(dr1);

                    }



                }
            }

            DataView dv1 = dt.DefaultView;
            dv1.Sort = ("rsircode, spcfcod");
            dt = this.HiddenSameData(dv1.ToTable());
            Session["SuplRes"] = dt;
            this.gvResInfo_DataBind();
        }


        protected void lbtnResUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string Supcode = this.ddlSupl2.SelectedValue.ToString();
            //bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETESUPLRES",
            //                 Supcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            this.Session_tblSuplRes_Update();
            DataTable tbl1 = (DataTable)Session["SuplRes"];


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString(); ;
                string mRMRKS = tbl1.Rows[i]["rmrks"].ToString();
                string mDelsys = tbl1.Rows[i]["delsys"].ToString();
                string mPayss = tbl1.Rows[i]["paysys"].ToString();
                string mRate = tbl1.Rows[i]["rate"].ToString();
                string mPaylimit = tbl1.Rows[i]["paylimit"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLRES",
                              mSSIRCODE, mRSIRCODE, mSPCFCOD, mRMRKS, mDelsys, mPayss, mRate, mPaylimit, "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.Label1.Text;
                string eventdesc = "Update Resource Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void gvResInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //string Supcode = this.ddlSupl2.SelectedValue.ToString();
            //bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETESUPLRES",
            //                 Supcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["SuplRes"];
            string Supcode = this.ddlSupl2.SelectedValue.ToString();
            string Rescode = ((Label)this.gvResInfo.Rows[e.RowIndex].FindControl("lblgvResCod1")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETESUPLRES",
                             Supcode, Rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (result == true)
            {
                int rowindex = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("SuplRes");
            Session["SuplRes"] = dv.ToTable();
            this.gvResInfo_DataBind();


            //DataTable dt = (DataTable)Session["SuplRes"];
            //int rowindex = gvResInfo.PageSize * gvResInfo.PageIndex + e.RowIndex;
            //string Supcode = ((Label)this.gvResInfo.Rows[e.RowIndex].FindControl("lblgvSuplCod1")).Text.Trim();
            //string Rescode = ((Label)this.gvResInfo.Rows[e.RowIndex].FindControl("lblgvResCod1")).Text.Trim();        
            //dt.Rows[rowindex].Delete();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "ssircode <>''";
            //Session["SuplRes"] = dv.ToTable();
            //this.gvResInfo_DataBind();

        }
        protected void ImgbtnFindMat_Click(object sender, EventArgs e)
        {
            string mSrchTxt = this.txtMSRResSearch.Text.Trim() + "%";
            this.Resource_List(mSrchTxt);
            this.ddlMSRRes.DataTextField = "rsirdesc1";
            this.ddlMSRRes.DataValueField = "rsircode";
            this.ddlMSRRes.DataSource = (DataTable)Session["tblMat"];
            this.ddlMSRRes.DataBind();
            this.GetSpecificationms();

        }

        private void GetSpecificationms()
        {

            DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();
            string Resource01 = this.ddlMSRRes.SelectedValue.ToString().Substring(0, 9);
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("mspcfcod='" + Resource01 + "' or mspcfcod='000000000'");
            //dv.Sort = ("wrkcode, rsircode");
            dt = dv.ToTable();


            this.ddlSpecificationms.DataTextField = "spcfdesc";
            this.ddlSpecificationms.DataValueField = "spcfcod";
            this.ddlSpecificationms.DataSource = dt;
            this.ddlSpecificationms.DataBind();
            this.ImgbtnFindSup_Click(null, null);

        }




        protected void ddlMSRRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecificationms();

        }

        protected void ImgbtnFindSpecificationms_Click(object sender, EventArgs e)
        {
            this.GetSpecificationms();
        }
        protected void ddlSpecificationms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnFindSup_Click(null, null);
        }


        private string GetComSupplier()
        {
            string comcod = this.GetComCode();
            string comallsupp = "";
            switch (comcod)
            {

                case "3353"://Manama
                //case "3101"://Manama
                    comallsupp = "All Supplier";
                    break;

                default:
                    break;

            }


            return comallsupp;
        }
        protected void ImgbtnFindSup_Click(object sender, EventArgs e)
        {
            Session.Remove("Supplier");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = this.txtMSRSupSearch.Text.Trim() + "%";
            string mResCode = (this.GetComSupplier().Length > 0) ? "" : this.ddlMSRRes.SelectedValue.ToString();
            string mSpcfCod = this.ddlSpecificationms.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, mResCode, mSpcfCod, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["Supplier"] = ds1.Tables[0];
            this.ddlMSRSupl.DataTextField = "ssirdesc1";
            this.ddlMSRSupl.DataValueField = "ssircode";
            this.ddlMSRSupl.DataSource = ds1.Tables[0];
            this.ddlMSRSupl.DataBind();
        }
        protected void ImgbtnFindPreMR_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string premrlist = "%" + this.txtPreMSRSearch.Text + "%";
            string premrlist = "%%";
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREVMSRLIST", CurDate1,
                          premrlist, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevMSRList.Items.Clear();
            this.ddlPrevMSRList.DataTextField = "msrno1";
            this.ddlPrevMSRList.DataValueField = "msrno";
            this.ddlPrevMSRList.DataSource = ds1.Tables[0];
            this.ddlPrevMSRList.DataBind();
        }







    }
}
