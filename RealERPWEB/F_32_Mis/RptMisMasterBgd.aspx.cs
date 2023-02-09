using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using RealERPRPT;
namespace RealERPWEB.F_32_Mis
{
    public partial class RptMisMasterBgd : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();
        AutoCompleted Data = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("tblmasterbgd");
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");



                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                //        (DataSet) Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"])) ;
                //    Response.Redirect("../AcceessError.aspx");

               // DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgd") ? "BUDGETED INCOME - ALL PROJECT(DETAILS) "
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "SrAUtilities") ? "SOURCES & UTILIZATION OF FUND - CASH BASIS"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "SrAUtilitiesFF") ? "SOURCES & UTILIZATION OF FUND - ACURAL BASIS"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "ProDetails") ? "Individual Project Cost Last 12 Month"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "ProExpenses") ? "Head Office overhead-last 12 Month"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "BgdVsExpenses") ? "BUDGET VS EXPENSES ALL- PROJECT"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "SalesVsColection") ? "COLLECTION VS EXPENSES- ALL PROJECTS"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "ColVsExpenses") ? "INVESTMENT PLAN - ALL PROJECTS"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "ComProCost") ? "Any Cost-All Project Report"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "InvPlan") ? "INVESTMENT PLAN SUMMARY"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "BgdVsActual") ? "Budget VS Actual-All Project"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "CostOfFund") ? "Interest Allocation" : "COST OF FUND OF PROJECTS";

                this.SectionView();
                // this.pnlMenu.Visible = true;


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);



        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
            //Hashtable hst = (Hashtable)System.Web.HttpContext.Current.Session["tblLogin"];
            ////string str =ASTUtility.Left((hst["comcod"].ToString()),1);
            //return ASTUtility.Left((hst["comcod"].ToString()), 1);
        }
        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MasterBgd":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetProjectName();
                    break;

                case "SrAUtilities":
                case "SrAUtilitiesFF":
                    this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-12).ToString("dd-MMM-yyyy");
                    this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


                case "ProDetails":
                    this.GetAccCode();
                    this.ddlReportLevelA.Visible = false;
                    this.txtDatefromd.Text = System.DateTime.Today.AddMonths(-12).ToString("dd-MMM-yyyy");
                    this.txtDatetod.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    break;



                case "ProExpenses":
                    this.GetAccCode();
                    this.ddlReportLeveld.Visible = false;
                    this.txtDatefromd.Text = System.DateTime.Today.AddMonths(-12).ToString("dd-MMM-yyyy");
                    this.txtDatetod.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "BgdVsExpenses":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;
                    break; ;

                case "SalesVsColection":
                    this.txtDateCollect.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "ComProCost":
                    this.txtDatefromCom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtDatetoCom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.GetProjectName();
                    this.GetResCode();

                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "ColVsExpenses":
                    this.txtDateExpens.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 6;
                    this.chkCrore.Checked = true;

                    break;

                case "CostOfFund":
                    this.txtDateCostofFund.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 7;
                    break;
                case "InvPlan":
                    this.txtDateInPlan.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 8;
                    this.GetProjectType();
                    this.lbtnShowInPlan_Click(null, null);

                    break;

                case "BgdVsActual":
                    this.txtDateb.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 9;
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetComeCode();
                // string eventtype = this.lblHtitle.Text;
                string eventdesc = "Show Report: " + Type;
                string eventdesc2 = "";
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }




        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;

        }

        private void GetProjectName()
        {


            string comcod = this.GetComeCode();
            DataSet ds4 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "GETPROJECTNAME", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {

                return;
            }

            if (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgd")
            {
                this.cblProject.DataTextField = "actdesc";
                this.cblProject.DataValueField = "actcode";
                this.cblProject.DataSource = ds4.Tables[0];
                this.cblProject.DataBind();
            }





        }

        private void GetAccCode()
        {
            string comcod = this.GetComeCode();
            string txtprosearch = this.txtSrcProject.Text.Trim() + "%";
            string Level2 = (this.Request.QueryString["Type"].ToString().Trim() == "ProDetails") ? "LEVEL2" : "";
            DataSet ds4 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "GETACCPROCODE", txtprosearch, Level2, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.ddlProjectName.Items.Clear();
                return;
            }
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();

        }

        public void GetResCode()
        {
            Session.Remove("tblrescode");
            string comcod = this.GetComeCode();
            DataSet ds = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "GETRESCODE", "", "", "", "", "", "", "", "", "");
            if (ds == null)
                return;

            this.ddlDetailsCode.DataTextField = "resdesc";
            this.ddlDetailsCode.DataValueField = "rescode";
            this.ddlDetailsCode.DataSource = ds.Tables[0];
            this.ddlDetailsCode.DataBind();
        }

        protected void chkDeselectAll_CheckedChanged(object sender, EventArgs e)
        {

            if (chkDeselectAll.Checked)
            {
                for (int i = 0; i < this.cblProject.Items.Count; i++)
                {
                    cblProject.Items[i].Selected = false;

                }
            }


        }
        //protected void chkall_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkall.Checked)
        //    {
        //        for (int i = 0; i < this.cblProject.Items.Count; i++)
        //        {
        //            cblProject.Items[i].Selected = true;

        //        }


        //    }

        //    else
        //    {
        //        for (int i = 0; i < this.cblProject.Items.Count; i++)
        //        {
        //            cblProject.Items[i].Selected = false;

        //        }

        //    }
        //}

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();
            string fieldinfo = "";
            int top = 0;
            for (int i = 0; i < this.cblProject.Items.Count; i++)
            {
                if (cblProject.Items[i].Selected)
                {

                    fieldinfo = fieldinfo + cblProject.Items[i].Value.ToString();
                    if (top == 11)
                        break;
                    top++;
                }


            }

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTMASTERBGDSLECTEDPRO", fieldinfo.Trim(), "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgd.DataSource = null;
                this.gvBgd.DataBind();
                return;
            }
            Session["tblmasterbgd"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }



        protected void lbtnShowSource_Click(object sender, EventArgs e)
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txtDateto.Text.Trim()), Convert.ToDateTime(this.txtDatefrom.Text.Trim()));
            if (mon > 12)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                return;
            }

            string txtdatefrm = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string lavel = this.ddlReportLevel.SelectedValue.ToString();
            string FundFlow = (this.Request.QueryString["Type"].ToString() == "SrAUtilitiesFF") ? "" : "RP";
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTSOURCEANDLIABILITIES", txtdatefrm, txtdateto, lavel, FundFlow, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSource.DataSource = null;
                this.gvSource.DataBind();
                return;
            }
            //DataTable dt = ds1.Tables[0];
            //if (this.Request.QueryString["Type"].ToString().Trim() == "SrAUtilitiesWOP")
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.RowFilter = ("toamtwp<>0");
            //    dt = dv.ToTable();

            //}


            Session["tblmasterbgd"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            string actcode = dt1.Rows[0]["grp"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["grp"].ToString();

                }
            }
            return dt1;

        }


        private DataTable HiddenSameData02(DataTable dt1)


        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MasterBgd":

                    break;

                case "SrAUtilities":
                case "SrAUtilitiesFF":

                    break;


                case "ProDetails":

                    break;



                case "ProExpenses":

                    break;

                case "BgdVsExpenses":

                    break; ;
                case "BgdVsActual":

                    break; ;

                case "SalesVsColection":





                    break;
                case "ComProCost":
                    break;

                case "ColVsExpenses":
                case "InvPlan":
                    string catmcode = dt1.Rows[0]["catmcode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["catmcode"].ToString() == catmcode)
                        {

                            dt1.Rows[j]["catmdesc"] = "";


                        }



                        catmcode = dt1.Rows[j]["catmcode"].ToString();

                    }

                    break;


            }



            return dt1;


        }


        protected void lbtnShowPDetails_Click(object sender, EventArgs e)
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txtDatetod.Text.Trim()), Convert.ToDateTime(this.txtDatefromd.Text.Trim()));
            if (mon > 12)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                return;
            }



            string txtdatefrm = Convert.ToDateTime(this.txtDatefromd.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txtDatetod.Text.Trim()).ToString("dd-MMM-yyyy");
            string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "ProDetails") ? "RPTPROJECTDETAILS" : "RPTPROJECTEXPENSES";
            string lavel = (this.Request.QueryString["Type"].ToString().Trim() == "ProDetails") ? this.ddlReportLeveld.SelectedValue.ToString() : this.ddlReportLevelA.SelectedValue.ToString();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", CallType, txtdatefrm, txtdateto, lavel, ProjectCode, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProDetials.DataSource = null;
                this.gvProDetials.DataBind();
                return;
            }


            Session["tblmasterbgd"] = ds1.Tables[0];
            this.Data_Bind();
        }

        protected void lbtnProVsExp_Click(object sender, EventArgs e)
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();
            string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTBGDVSEXPENSES", txtdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgdVsExp.DataSource = null;
                this.gvBgdVsExp.DataBind();
                return;
            }


            Session["tblmasterbgd"] = ds1.Tables[0];
            this.Data_Bind();
        }

        protected void lbtnCollectVsExpenses_Click(object sender, EventArgs e)
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();
            string txtdate = Convert.ToDateTime(this.txtDateCollect.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTSALESVSCOLLECTION", txtdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalVsCollect.DataSource = null;
                this.gvSalVsCollect.DataBind();
                return;
            }


            Session["tblmasterbgd"] = ds1.Tables[0];
            this.Data_Bind();

        }

        protected void LinkButtonShowCostOfFund_Click(object sender, EventArgs e)
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();
            string txtdate = Convert.ToDateTime(this.txtDateCostofFund.Text.Trim()).ToString("dd-MMM-yyyy");
            string IntrstRate = this.TxtIntrstRate.Text.Trim().Replace("%", "");
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTCOSTOFFUND", txtdate, IntrstRate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCostOfFund.DataSource = null;
                this.gvCostOfFund.DataBind();
                return;
            }

            Session["tblmasterbgd"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        protected void lbtnShowColvsExp_Click(object sender, EventArgs e)
        {
            //this.lblmsg01.Visible = true;
            try
            {
                this.pnlNoteDe.Visible = true;
                this.lblBankstatus.Visible = true;
                Session.Remove("tblmasterbgd");
                string comcod = this.GetComeCode();
                string txtdate = Convert.ToDateTime(this.txtDateExpens.Text.Trim()).ToString("dd-MMM-yyyy");
                string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";
                string crore = (this.chkCrore.Checked) ? "10000000" : "1";
                DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTCOLLECTVSEXPENSES", txtdate, consolidate, crore, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvColvsExp.DataSource = null;
                    this.gvColvsExp.DataBind();
                    return;
                }
                //this.PanelNote.Visible = true;
                //this.txtColl.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";
                //this.txtNp.Text = Convert.ToDouble(ds1.Tables[1].Rows[1]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";
                //this.txtBgd.Text = Convert.ToDouble(ds1.Tables[1].Rows[2]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";

                // DataTable dt = this.HiddenSameData02(ds1.Tables[0]);
                Session["tblmasterbgd"] = this.HiddenSameData02(ds1.Tables[0]);
                this.Data_Bind();
                this.gvCatdesc.DataSource = ds1.Tables[2];
                this.gvCatdesc.DataBind();
            }

            catch (Exception ex)
            {

                //((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;

            }
        }

        protected void lbtnShowInPlan_Click(object sender, EventArgs e)
        {

            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();
            string txtdate = Convert.ToDateTime(this.txtDateInPlan.Text.Trim()).ToString("dd-MMM-yyyy");
            string consolidate = (this.CheckBoxPlan.Checked) ? "consolidate" : "";
            string crore = (this.chkCore.Checked) ? "10000000" : "1";
            string catcode = this.ddlProjectType.SelectedItem.Value.ToString();
            string catcode1 = catcode.Substring(0, 2);



            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTINVESTPLAN", txtdate, consolidate, crore, "", "", "", "", "", "");

            DataView dv = ds1.Tables[0].DefaultView;

            DataTable dt;
            if (catcode1 != "00")
            {
                dv.RowFilter = "catcode like '" + catcode1 + "%' or catmcode like '" + catcode1 + "%'";
            }

            dt = dv.ToTable();
            if (ds1 == null)
            {
                this.gvInPlan.DataSource = null;
                this.gvInPlan.DataBind();
                return;
            }
            Session["tblmasterbgd"] = this.HiddenSameData02(dt);

            //Session["tblmasterbgd"] = this.HiddenSameData02(ds1.Tables[0]);
            Session["tblcomsummary"] = ds1.Tables[1];
            Session["forActcode"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt, dt1; DataView dv;
            dt1 = ((DataTable)Session["tblmasterbgd"]).Copy();
            dv = dt1.Copy().DefaultView;

            int i, j;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            double p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34,
                p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67,
                p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98, p99, p100;
            DateTime datefrm, dateto;

            switch (Type)
            {
                case "MasterBgd":
                    for (i = 2; i < this.gvBgd.Columns.Count; i++)
                        this.gvBgd.Columns[i].Visible = false;
                    j = 2;

                    for (i = 0; i < this.cblProject.Items.Count; i++)
                    {
                        if (this.cblProject.Items[i].Selected)
                        {
                            this.gvBgd.Columns[j].Visible = true;
                            this.gvBgd.Columns[j].HeaderText = this.cblProject.Items[i].Text.Trim();

                            if (j == 13)
                                break;
                            j++;
                        }

                    }

                    this.gvBgd.DataSource = dt1;
                    this.gvBgd.DataBind();
                    break;




                case "SrAUtilities":
                case "SrAUtilitiesFF":
                    //dv.RowFilter = ("rptcode not like '%AAAA%'");
                    //dt = dv.ToTable();
                    //amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    //amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    //amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    //amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    //amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    //amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    //amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    //amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    //amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    //amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    //amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    //amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));


                    //this.gvSource.Columns[1].Visible = (Type == "SrAUtilities");
                    //this.gvSource.Columns[2].Visible = (Type == "SrAUtilities");
                    //this.gvSource.Columns[3].Visible = (Type == "SrAUtilitiesWOP");
                    //this.gvSource.Columns[4].Visible = (amt1 != 0);
                    //this.gvSource.Columns[5].Visible = (amt2 != 0);
                    //this.gvSource.Columns[6].Visible = (amt3 != 0);
                    //this.gvSource.Columns[7].Visible = (amt4 != 0);
                    //this.gvSource.Columns[8].Visible = (amt5 != 0);
                    //this.gvSource.Columns[9].Visible = (amt6 != 0);
                    //this.gvSource.Columns[10].Visible = (amt7 != 0);
                    //this.gvSource.Columns[11].Visible = (amt8 != 0);
                    //this.gvSource.Columns[12].Visible = (amt9 != 0);
                    //this.gvSource.Columns[13].Visible = (amt10 != 0);
                    //this.gvSource.Columns[14].Visible = (amt11 != 0);
                    //this.gvSource.Columns[15].Visible = (amt12 != 0);
                    for (i = 4; i < this.gvSource.Columns.Count; i++)
                        this.gvSource.Columns[i].Visible = false;

                    datefrm = Convert.ToDateTime(this.txtDatefrom.Text.Trim());
                    dateto = Convert.ToDateTime(this.txtDateto.Text.Trim());
                    for (i = 4; i < 16; i++)
                    {
                        if (datefrm > dateto)
                            break;
                        this.gvSource.Columns[i].Visible = true;
                        this.gvSource.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvSource.DataSource = dt1;
                    this.gvSource.DataBind();
                    break;


                case "ProDetails":
                case "ProExpenses":
                    dv.RowFilter = ("rptcode not like '%AAAA%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    this.gvProDetials.Columns[3].Visible = (amt1 != 0);
                    this.gvProDetials.Columns[4].Visible = (amt2 != 0);
                    this.gvProDetials.Columns[5].Visible = (amt3 != 0);
                    this.gvProDetials.Columns[6].Visible = (amt4 != 0);
                    this.gvProDetials.Columns[7].Visible = (amt5 != 0);
                    this.gvProDetials.Columns[8].Visible = (amt6 != 0);
                    this.gvProDetials.Columns[9].Visible = (amt7 != 0);
                    this.gvProDetials.Columns[10].Visible = (amt8 != 0);
                    this.gvProDetials.Columns[11].Visible = (amt9 != 0);
                    this.gvProDetials.Columns[12].Visible = (amt10 != 0);
                    this.gvProDetials.Columns[13].Visible = (amt11 != 0);
                    this.gvProDetials.Columns[14].Visible = (amt12 != 0);
                    datefrm = Convert.ToDateTime(this.txtDatefromd.Text.Trim());
                    dateto = Convert.ToDateTime(this.txtDatetod.Text.Trim());
                    for (i = 3; i < 15; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvProDetials.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvProDetials.DataSource = dt1;
                    this.gvProDetials.DataBind();
                    break;

                case "BgdVsExpenses":

                    this.gvBgdVsExp.DataSource = dt1;
                    this.gvBgdVsExp.DataBind();
                    ((Label)this.gvBgdVsExp.FooterRow.FindControl("lgvFbgdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(bgdamt)", "")) ? 0.00 : dt1.Compute("sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvBgdVsExp.FooterRow.FindControl("lgvFacamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(acamt)", "")) ? 0.00 : dt1.Compute("sum(acamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvBgdVsExp.FooterRow.FindControl("lgvFreamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(reamt)", "")) ? 0.00 : dt1.Compute("sum(reamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;


                case "BgdVsActual":

                    this.gvBgdVsActual.DataSource = dt1;
                    this.gvBgdVsActual.DataBind();

                    ((Label)this.gvBgdVsActual.FooterRow.FindControl("lgvFbgdamn")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(bgdamt)", "")) ? 0.00 : dt1.Compute("sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvBgdVsActual.FooterRow.FindControl("lgvFmat")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(mat)", "")) ? 0.00 : dt1.Compute("sum(mat)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvBgdVsActual.FooterRow.FindControl("lgvFlab")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(labor)", "")) ? 0.00 : dt1.Compute("sum(labor)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvBgdVsActual.FooterRow.FindControl("lgvFothers")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(others)", "")) ? 0.00 : dt1.Compute("sum(others)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvBgdVsActual.FooterRow.FindControl("lgvFreamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(reamt)", "")) ? 0.00 : dt1.Compute("sum(reamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvBgdVsActual.FooterRow.FindControl("lgvFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(acamt)", "")) ? 0.00 : dt1.Compute("sum(acamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;

                case "SalesVsColection":
                    this.gvSalVsCollect.DataSource = dt1;
                    this.gvSalVsCollect.DataBind();




                    ((Label)this.gvSalVsCollect.FooterRow.FindControl("lgvFSalVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(bgdamt)", "")) ? 0.00 : dt1.Compute("sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalVsCollect.FooterRow.FindControl("lgvFSalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(salamt)", "")) ? 0.00 : dt1.Compute("sum(salamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalVsCollect.FooterRow.FindControl("lgvFUsolAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(usolamt)", "")) ? 0.00 : dt1.Compute("sum(usolamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalVsCollect.FooterRow.FindControl("lgvFColAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(colamt)", "")) ? 0.00 : dt1.Compute("sum(colamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalVsCollect.FooterRow.FindControl("lgvFrColAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(rcolamt)", "")) ? 0.00 : dt1.Compute("sum(rcolamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalVsCollect.FooterRow.FindControl("lgvFrFundAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(rfundamt)", "")) ? 0.00 : dt1.Compute("sum(rfundamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;

                case "ComProCost":
                    string level = this.ddlReportLevelCom.SelectedValue.ToString();
                    // stri
                    if (level == "12")
                        dv.RowFilter = ("rescode like '%000'");
                    dt = dv.ToTable();
                    double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(topamt)", "")) ? 0.00 : dt.Compute("sum(topamt)", "")));
                    p1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p1)", "")) ? 0.00 : dt.Compute("sum(p1)", "")));
                    p2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p2)", "")) ? 0.00 : dt.Compute("sum(p2)", "")));
                    p3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p3)", "")) ? 0.00 : dt.Compute("sum(p3)", "")));
                    p4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p4)", "")) ? 0.00 : dt.Compute("sum(p4)", "")));
                    p5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p5)", "")) ? 0.00 : dt.Compute("sum(p5)", "")));
                    p6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p6)", "")) ? 0.00 : dt.Compute("sum(p6)", "")));
                    p7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p7)", "")) ? 0.00 : dt.Compute("sum(p7)", "")));
                    p8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p8)", "")) ? 0.00 : dt.Compute("sum(p8)", "")));
                    p9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p9)", "")) ? 0.00 : dt.Compute("sum(p9)", "")));
                    p10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p10)", "")) ? 0.00 : dt.Compute("sum(p10)", "")));
                    p11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p11)", "")) ? 0.00 : dt.Compute("sum(p11)", "")));
                    p12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p12)", "")) ? 0.00 : dt.Compute("sum(p12)", "")));
                    p13 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p13)", "")) ? 0.00 : dt.Compute("sum(p13)", "")));
                    p14 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p14)", "")) ? 0.00 : dt.Compute("sum(p14)", "")));
                    p15 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p15)", "")) ? 0.00 : dt.Compute("sum(p15)", "")));
                    p16 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p16)", "")) ? 0.00 : dt.Compute("sum(p16)", "")));
                    p17 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p17)", "")) ? 0.00 : dt.Compute("sum(p17)", "")));
                    p18 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p18)", "")) ? 0.00 : dt.Compute("sum(p18)", "")));
                    p19 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p19)", "")) ? 0.00 : dt.Compute("sum(p19)", "")));
                    p20 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p20)", "")) ? 0.00 : dt.Compute("sum(p20)", "")));
                    p21 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p21)", "")) ? 0.00 : dt.Compute("sum(p21)", "")));
                    p22 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p22)", "")) ? 0.00 : dt.Compute("sum(p22)", "")));
                    p23 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p23)", "")) ? 0.00 : dt.Compute("sum(p23)", "")));
                    p24 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p24)", "")) ? 0.00 : dt.Compute("sum(p24)", "")));
                    p25 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p25)", "")) ? 0.00 : dt.Compute("sum(p25)", "")));
                    p26 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p26)", "")) ? 0.00 : dt.Compute("sum(p26)", "")));
                    p27 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p27)", "")) ? 0.00 : dt.Compute("sum(p27)", "")));
                    p28 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p28)", "")) ? 0.00 : dt.Compute("sum(p28)", "")));
                    p29 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p29)", "")) ? 0.00 : dt.Compute("sum(p29)", "")));
                    p30 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p30)", "")) ? 0.00 : dt.Compute("sum(p30)", "")));
                    p31 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p31)", "")) ? 0.00 : dt.Compute("sum(p31)", "")));
                    p32 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p32)", "")) ? 0.00 : dt.Compute("sum(p32)", "")));
                    p33 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p33)", "")) ? 0.00 : dt.Compute("sum(p33)", "")));
                    p34 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p34)", "")) ? 0.00 : dt.Compute("sum(p34)", "")));
                    p35 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p35)", "")) ? 0.00 : dt.Compute("sum(p35)", "")));
                    p36 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p36)", "")) ? 0.00 : dt.Compute("sum(p36)", "")));
                    p37 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p37)", "")) ? 0.00 : dt.Compute("sum(p37)", "")));
                    p38 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p38)", "")) ? 0.00 : dt.Compute("sum(p38)", "")));
                    p39 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p39)", "")) ? 0.00 : dt.Compute("sum(p39)", "")));
                    p40 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p40)", "")) ? 0.00 : dt.Compute("sum(p40)", "")));
                    p41 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p41)", "")) ? 0.00 : dt.Compute("sum(p41)", "")));
                    p42 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p42)", "")) ? 0.00 : dt.Compute("sum(p42)", "")));
                    p43 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p43)", "")) ? 0.00 : dt.Compute("sum(p43)", "")));
                    p44 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p44)", "")) ? 0.00 : dt.Compute("sum(p44)", "")));
                    p45 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p45)", "")) ? 0.00 : dt.Compute("sum(p45)", "")));
                    p46 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p46)", "")) ? 0.00 : dt.Compute("sum(p46)", "")));
                    p47 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p47)", "")) ? 0.00 : dt.Compute("sum(p47)", "")));
                    p48 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p48)", "")) ? 0.00 : dt.Compute("sum(p48)", "")));
                    p49 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p49)", "")) ? 0.00 : dt.Compute("sum(p49)", "")));
                    p50 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p50)", "")) ? 0.00 : dt.Compute("sum(p50)", "")));


                    p51 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p51)", "")) ? 0.00 : dt.Compute("sum(p51)", "")));
                    p52 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p52)", "")) ? 0.00 : dt.Compute("sum(p52)", "")));
                    p53 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p53)", "")) ? 0.00 : dt.Compute("sum(p53)", "")));
                    p54 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p54)", "")) ? 0.00 : dt.Compute("sum(p54)", "")));
                    p55 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p55)", "")) ? 0.00 : dt.Compute("sum(p55)", "")));
                    p56 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p56)", "")) ? 0.00 : dt.Compute("sum(p56)", "")));
                    p57 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p57)", "")) ? 0.00 : dt.Compute("sum(p57)", "")));
                    p58 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p58)", "")) ? 0.00 : dt.Compute("sum(p58)", "")));
                    p59 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p59)", "")) ? 0.00 : dt.Compute("sum(p59)", "")));
                    p60 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p60)", "")) ? 0.00 : dt.Compute("sum(p60)", "")));
                    p61 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p61)", "")) ? 0.00 : dt.Compute("sum(p61)", "")));
                    p62 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p62)", "")) ? 0.00 : dt.Compute("sum(p62)", "")));
                    p63 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p63)", "")) ? 0.00 : dt.Compute("sum(p63)", "")));
                    p64 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p64)", "")) ? 0.00 : dt.Compute("sum(p64)", "")));
                    p65 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p65)", "")) ? 0.00 : dt.Compute("sum(p65)", "")));
                    p66 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p66)", "")) ? 0.00 : dt.Compute("sum(p66)", "")));
                    p67 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p67)", "")) ? 0.00 : dt.Compute("sum(p67)", "")));
                    p68 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p68)", "")) ? 0.00 : dt.Compute("sum(p68)", "")));
                    p69 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p69)", "")) ? 0.00 : dt.Compute("sum(p69)", "")));
                    p70 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p70)", "")) ? 0.00 : dt.Compute("sum(p70)", "")));
                    p71 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p71)", "")) ? 0.00 : dt.Compute("sum(p71)", "")));
                    p72 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p72)", "")) ? 0.00 : dt.Compute("sum(p72)", "")));
                    p73 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p73)", "")) ? 0.00 : dt.Compute("sum(p73)", "")));
                    p74 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p74)", "")) ? 0.00 : dt.Compute("sum(p74)", "")));
                    p75 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p75)", "")) ? 0.00 : dt.Compute("sum(p75)", "")));
                    p76 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p76)", "")) ? 0.00 : dt.Compute("sum(p76)", "")));
                    p77 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p77)", "")) ? 0.00 : dt.Compute("sum(p77)", "")));
                    p78 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p78)", "")) ? 0.00 : dt.Compute("sum(p78)", "")));
                    p79 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p79)", "")) ? 0.00 : dt.Compute("sum(p79)", "")));
                    p80 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p80)", "")) ? 0.00 : dt.Compute("sum(p80)", "")));
                    p81 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p81)", "")) ? 0.00 : dt.Compute("sum(p81)", "")));
                    p82 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p82)", "")) ? 0.00 : dt.Compute("sum(p82)", "")));
                    p83 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p83)", "")) ? 0.00 : dt.Compute("sum(p83)", "")));
                    p84 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p84)", "")) ? 0.00 : dt.Compute("sum(p84)", "")));
                    p85 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p85)", "")) ? 0.00 : dt.Compute("sum(p85)", "")));
                    p86 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p86)", "")) ? 0.00 : dt.Compute("sum(p86)", "")));
                    p87 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p87)", "")) ? 0.00 : dt.Compute("sum(p87)", "")));
                    p88 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p88)", "")) ? 0.00 : dt.Compute("sum(p88)", "")));
                    p89 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p89)", "")) ? 0.00 : dt.Compute("sum(p89)", "")));
                    p90 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p90)", "")) ? 0.00 : dt.Compute("sum(p90)", "")));
                    p91 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p91)", "")) ? 0.00 : dt.Compute("sum(p91)", "")));
                    p92 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p92)", "")) ? 0.00 : dt.Compute("sum(p92)", "")));
                    p93 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p93)", "")) ? 0.00 : dt.Compute("sum(p93)", "")));
                    p94 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p94)", "")) ? 0.00 : dt.Compute("sum(p94)", "")));
                    p95 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p95)", "")) ? 0.00 : dt.Compute("sum(p95)", "")));
                    p96 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p96)", "")) ? 0.00 : dt.Compute("sum(p96)", "")));
                    p97 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p97)", "")) ? 0.00 : dt.Compute("sum(p97)", "")));
                    p98 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p98)", "")) ? 0.00 : dt.Compute("sum(p98)", "")));
                    p99 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p99)", "")) ? 0.00 : dt.Compute("sum(p99)", "")));
                    p100 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p100)", "")) ? 0.00 : dt.Compute("sum(p100)", "")));



                    this.gvComCost.Columns[2].Visible = (p1 != 0);
                    this.gvComCost.Columns[3].Visible = (p2 != 0);
                    this.gvComCost.Columns[4].Visible = (p3 != 0);
                    this.gvComCost.Columns[5].Visible = (p4 != 0);
                    this.gvComCost.Columns[6].Visible = (p5 != 0);
                    this.gvComCost.Columns[7].Visible = (p6 != 0);
                    this.gvComCost.Columns[8].Visible = (p7 != 0);
                    this.gvComCost.Columns[9].Visible = (p8 != 0);
                    this.gvComCost.Columns[10].Visible = (p9 != 0);
                    this.gvComCost.Columns[11].Visible = (p10 != 0);
                    this.gvComCost.Columns[12].Visible = (p11 != 0);
                    this.gvComCost.Columns[13].Visible = (p12 != 0);
                    this.gvComCost.Columns[14].Visible = (p13 != 0);
                    this.gvComCost.Columns[15].Visible = (p14 != 0);
                    this.gvComCost.Columns[16].Visible = (p15 != 0);
                    this.gvComCost.Columns[17].Visible = (p16 != 0);
                    this.gvComCost.Columns[18].Visible = (p17 != 0);
                    this.gvComCost.Columns[19].Visible = (p18 != 0);
                    this.gvComCost.Columns[20].Visible = (p19 != 0);
                    this.gvComCost.Columns[21].Visible = (p20 != 0);
                    this.gvComCost.Columns[22].Visible = (p21 != 0);
                    this.gvComCost.Columns[23].Visible = (p22 != 0);
                    this.gvComCost.Columns[24].Visible = (p23 != 0);
                    this.gvComCost.Columns[25].Visible = (p24 != 0);
                    this.gvComCost.Columns[26].Visible = (p25 != 0);
                    this.gvComCost.Columns[27].Visible = (p26 != 0);
                    this.gvComCost.Columns[28].Visible = (p27 != 0);
                    this.gvComCost.Columns[29].Visible = (p28 != 0);
                    this.gvComCost.Columns[30].Visible = (p29 != 0);
                    this.gvComCost.Columns[31].Visible = (p30 != 0);
                    this.gvComCost.Columns[32].Visible = (p31 != 0);
                    this.gvComCost.Columns[33].Visible = (p32 != 0);
                    this.gvComCost.Columns[34].Visible = (p33 != 0);
                    this.gvComCost.Columns[35].Visible = (p34 != 0);
                    this.gvComCost.Columns[36].Visible = (p35 != 0);
                    this.gvComCost.Columns[37].Visible = (p36 != 0);
                    this.gvComCost.Columns[38].Visible = (p37 != 0);
                    this.gvComCost.Columns[39].Visible = (p38 != 0);
                    this.gvComCost.Columns[40].Visible = (p39 != 0);
                    this.gvComCost.Columns[41].Visible = (p40 != 0);
                    this.gvComCost.Columns[42].Visible = (p41 != 0);
                    this.gvComCost.Columns[43].Visible = (p42 != 0);
                    this.gvComCost.Columns[44].Visible = (p43 != 0);
                    this.gvComCost.Columns[45].Visible = (p44 != 0);
                    this.gvComCost.Columns[46].Visible = (p45 != 0);
                    this.gvComCost.Columns[47].Visible = (p46 != 0);
                    this.gvComCost.Columns[48].Visible = (p47 != 0);
                    this.gvComCost.Columns[49].Visible = (p48 != 0);
                    this.gvComCost.Columns[50].Visible = (p49 != 0);
                    this.gvComCost.Columns[51].Visible = (p50 != 0);

                    this.gvComCost.Columns[52].Visible = (p51 != 0);
                    this.gvComCost.Columns[53].Visible = (p52 != 0);
                    this.gvComCost.Columns[54].Visible = (p53 != 0);
                    this.gvComCost.Columns[55].Visible = (p54 != 0);
                    this.gvComCost.Columns[56].Visible = (p55 != 0);
                    this.gvComCost.Columns[57].Visible = (p56 != 0);
                    this.gvComCost.Columns[58].Visible = (p57 != 0);
                    this.gvComCost.Columns[59].Visible = (p58 != 0);
                    this.gvComCost.Columns[60].Visible = (p59 != 0);
                    this.gvComCost.Columns[61].Visible = (p60 != 0);
                    this.gvComCost.Columns[62].Visible = (p61 != 0);
                    this.gvComCost.Columns[63].Visible = (p62 != 0);
                    this.gvComCost.Columns[64].Visible = (p63 != 0);
                    this.gvComCost.Columns[65].Visible = (p64 != 0);
                    this.gvComCost.Columns[66].Visible = (p65 != 0);
                    this.gvComCost.Columns[67].Visible = (p66 != 0);
                    this.gvComCost.Columns[68].Visible = (p67 != 0);
                    this.gvComCost.Columns[69].Visible = (p68 != 0);
                    this.gvComCost.Columns[70].Visible = (p69 != 0);
                    this.gvComCost.Columns[71].Visible = (p70 != 0);
                    this.gvComCost.Columns[72].Visible = (p71 != 0);
                    this.gvComCost.Columns[73].Visible = (p72 != 0);
                    this.gvComCost.Columns[74].Visible = (p73 != 0);
                    this.gvComCost.Columns[75].Visible = (p74 != 0);
                    this.gvComCost.Columns[76].Visible = (p75 != 0);
                    this.gvComCost.Columns[77].Visible = (p76 != 0);
                    this.gvComCost.Columns[78].Visible = (p77 != 0);
                    this.gvComCost.Columns[79].Visible = (p78 != 0);
                    this.gvComCost.Columns[80].Visible = (p79 != 0);
                    this.gvComCost.Columns[81].Visible = (p80 != 0);
                    this.gvComCost.Columns[82].Visible = (p81 != 0);
                    this.gvComCost.Columns[83].Visible = (p82 != 0);
                    this.gvComCost.Columns[84].Visible = (p83 != 0);
                    this.gvComCost.Columns[85].Visible = (p84 != 0);
                    this.gvComCost.Columns[86].Visible = (p85 != 0);
                    this.gvComCost.Columns[87].Visible = (p86 != 0);
                    this.gvComCost.Columns[88].Visible = (p87 != 0);
                    this.gvComCost.Columns[89].Visible = (p88 != 0);
                    this.gvComCost.Columns[90].Visible = (p89 != 0);
                    this.gvComCost.Columns[91].Visible = (p90 != 0);
                    this.gvComCost.Columns[92].Visible = (p91 != 0);
                    this.gvComCost.Columns[93].Visible = (p92 != 0);
                    this.gvComCost.Columns[94].Visible = (p93 != 0);
                    this.gvComCost.Columns[95].Visible = (p94 != 0);
                    this.gvComCost.Columns[96].Visible = (p95 != 0);
                    this.gvComCost.Columns[97].Visible = (p96 != 0);
                    this.gvComCost.Columns[98].Visible = (p97 != 0);
                    this.gvComCost.Columns[99].Visible = (p98 != 0);
                    this.gvComCost.Columns[100].Visible = (p99 != 0);
                    this.gvComCost.Columns[101].Visible = (p100 != 0);
                    j = 2;


                    DataTable dtpname = (DataTable)ViewState["tblproname"];
                    for (i = 0; i < dtpname.Rows.Count; i++)
                    {

                        this.gvComCost.Columns[j].HeaderText = dtpname.Rows[i]["pactdesc"].ToString();
                        j++;
                        if (j == 102)
                            break;


                    }

                    this.gvComCost.DataSource = dt1;
                    this.gvComCost.DataBind();


                    if (dt1.Rows.Count == 0)
                        return;

                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFtoCost")).Text = toamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC1")).Text = p1.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC2")).Text = p2.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC3")).Text = p3.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC4")).Text = p4.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC5")).Text = p5.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC6")).Text = p6.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC7")).Text = p7.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC8")).Text = p8.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC9")).Text = p9.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC10")).Text = p10.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC11")).Text = p11.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC12")).Text = p12.ToString("#,##0;(#,##0); ");

                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC13")).Text = p13.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC14")).Text = p14.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC15")).Text = p15.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC16")).Text = p16.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC17")).Text = p17.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC18")).Text = p18.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC19")).Text = p19.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC20")).Text = p20.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC21")).Text = p21.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC22")).Text = p22.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC23")).Text = p23.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC24")).Text = p24.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC25")).Text = p25.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC26")).Text = p26.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC27")).Text = p27.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC28")).Text = p28.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC29")).Text = p29.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC30")).Text = p30.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC31")).Text = p31.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC32")).Text = p32.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC33")).Text = p33.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC34")).Text = p34.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC35")).Text = p35.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC36")).Text = p36.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC37")).Text = p37.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC38")).Text = p38.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC39")).Text = p39.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC40")).Text = p40.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC41")).Text = p41.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC42")).Text = p42.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC43")).Text = p43.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC44")).Text = p44.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC45")).Text = p45.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC46")).Text = p46.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC47")).Text = p47.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC48")).Text = p48.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC49")).Text = p49.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC50")).Text = p50.ToString("#,##0;(#,##0); ");





                    Session["Report1"] = gvComCost;
                    ((HyperLink)this.gvComCost.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case "ColVsExpenses":
                    this.gvColvsExp.DataSource = dt1;// start
                    this.gvColvsExp.DataBind();

                    Session["Report1"] = gvColvsExp;
                    ((HyperLink)this.gvColvsExp.HeaderRow.FindControl("hlbtnCdataExelExp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



                    for (i = 0; i < this.gvColvsExp.Rows.Count; i++)
                    {
                        if (this.chkCrore.Checked)
                        {


                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvtosales")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvtosales")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvsales")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvsales")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvcoll")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvcoll")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvrecoll")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvrecoll")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvBgdAmt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvBgdAmt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvExp")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvExp")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvrebgdamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvrebgdamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvresdramt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvresdramt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvrescramt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvrescramt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvusoamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvusoamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvfsamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvfsamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvfsuamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvfsuamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvlibamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvlibamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvnoiamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvnoiamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvltoamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvltoamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvlfrmamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvlfrmamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvbgdsaamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvbgdsaamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvnp")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvnp")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");

                        }

                        else
                        {



                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvtosales")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvtosales")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvsales")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvsales")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvcoll")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvcoll")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvrecoll")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvrecoll")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvBgdAmt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvBgdAmt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvExp")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvExp")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvrebgdamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvrebgdamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvresdramt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvresdramt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvrescramt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvrescramt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvusoamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvusoamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvfsamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvfsamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvfsuamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvfsuamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvlibamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvlibamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvnoiamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvnoiamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvltoamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvltoamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvlfrmamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvlfrmamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvbgdsaamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvbgdsaamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvColvsExp.Rows[i].FindControl("lgvnp")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvColvsExp.Rows[i].FindControl("lgvnp")).Text.Trim())).ToString("#,##0;-#,##0; ");



                        }




                    }
                    //DataView dv1 = dt1.Copy().DefaultView;
                    //dv1.RowFilter = ("actcode = 'BBBBAAAAAAAA'");
                    DataTable dts = dt1;

                    if (this.chkCrore.Checked)
                    {

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFToSalVal")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(tosalval)", "")) ? 0.00 : dts.Compute("sum(tosalval)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFSaleAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(salamt)", "")) ? 0.00 : dts.Compute("sum(salamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(collamt)", "")) ? 0.00 : dts.Compute("sum(collamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFperonsale")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(perontosal)", "")) ? 0.00 : dts.Compute("sum(perontosal)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFperontocol")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(perontocol)", "")) ? 0.00 : dts.Compute("sum(perontocol)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(bgdamt)", "")) ? 0.00 : dts.Compute("sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFExpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(examt)", "")) ? 0.00 : dts.Compute("sum(examt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFperonPro")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(peronpgres)", "")) ? 0.00 : dts.Compute("sum(peronpgres)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFipamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(ipamt)", "")) ? 0.00 : dts.Compute("sum(ipamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFreCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(rsalamt)", "")) ? 0.00 : dts.Compute("sum(rsalamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFusoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(usoamt)", "")) ? 0.00 : dts.Compute("sum(usoamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFRbgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(rbgdamt)", "")) ? 0.00 : dts.Compute("sum(rbgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFfsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(fsamt)", "")) ? 0.00 : dts.Compute("sum(fsamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFfsuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(fsuamt)", "")) ? 0.00 : dts.Compute("sum(fsuamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFresdramt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(invreqamt)", "")) ? 0.00 : dts.Compute("sum(invreqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFrescramt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(invblkamt)", "")) ? 0.00 : dts.Compute("sum(invblkamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFlibamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(libamt)", "")) ? 0.00 : dts.Compute("sum(libamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFnoiamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(noiamt)", "")) ? 0.00 : dts.Compute("sum(noiamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFltoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(ltoamt)", "")) ? 0.00 : dts.Compute("sum(ltoamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFlfrmamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(lfrmamt)", "")) ? 0.00 : dts.Compute("sum(lfrmamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFbgdsaamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(bgdsaamt)", "")) ? 0.00 : dts.Compute("sum(bgdsaamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFnp")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(np)", "")) ? 0.00 : dts.Compute("sum(np)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    }

                    else
                    {

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFToSalVal")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(tosalval)", "")) ? 0.00 : dts.Compute("sum(tosalval)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFSaleAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(salamt)", "")) ? 0.00 : dts.Compute("sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(collamt)", "")) ? 0.00 : dts.Compute("sum(collamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFperonsale")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(perontosal)", "")) ? 0.00 : dts.Compute("sum(perontosal)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFperontocol")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(perontocol)", "")) ? 0.00 : dts.Compute("sum(perontocol)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(bgdamt)", "")) ? 0.00 : dts.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFExpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(examt)", "")) ? 0.00 : dts.Compute("sum(examt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFperonPro")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(peronpgres)", "")) ? 0.00 : dts.Compute("sum(peronpgres)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFipamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(ipamt)", "")) ? 0.00 : dts.Compute("sum(ipamt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFreCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(rsalamt)", "")) ? 0.00 : dts.Compute("sum(rsalamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFusoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(usoamt)", "")) ? 0.00 : dts.Compute("sum(usoamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFRbgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(rbgdamt)", "")) ? 0.00 : dts.Compute("sum(rbgdamt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFfsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(fsamt)", "")) ? 0.00 : dts.Compute("sum(fsamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFfsuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(fsuamt)", "")) ? 0.00 : dts.Compute("sum(fsuamt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFresdramt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(invreqamt)", "")) ? 0.00 : dts.Compute("sum(invreqamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFrescramt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(invblkamt)", "")) ? 0.00 : dts.Compute("sum(invblkamt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFlibamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(libamt)", "")) ? 0.00 : dts.Compute("sum(libamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFnoiamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(noiamt)", "")) ? 0.00 : dts.Compute("sum(noiamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFltoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(ltoamt)", "")) ? 0.00 : dts.Compute("sum(ltoamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFlfrmamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(lfrmamt)", "")) ? 0.00 : dts.Compute("sum(lfrmamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFbgdsaamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(bgdsaamt)", "")) ? 0.00 : dts.Compute("sum(bgdsaamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvColvsExp.FooterRow.FindControl("lgvFnp")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(np)", "")) ? 0.00 : dts.Compute("sum(np)", ""))).ToString("#,##0;(#,##0); ");

                    }




                    break;

                case "CostOfFund":
                    this.gvCostOfFund.DataSource = dt1;
                    this.gvCostOfFund.DataBind();
                    //((Label)this.gvCostOfFund.FooterRow.FindControl("lgvFExpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(examt)", "")) ? 0.00 : dt1.Compute("sum(examt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvCostOfFund.FooterRow.FindControl("lgvFCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(collamt)", "")) ? 0.00 : dt1.Compute("sum(collamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvCostOfFund.FooterRow.FindControl("lgvFDiffAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(diff)", "")) ? 0.00 : dt1.Compute("sum(diff)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvCostOfFund.FooterRow.FindControl("lgvFcostfund")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(costfund)", "")) ? 0.00 : dt1.Compute("sum(costfund)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    break;
                case "InvPlan":
                    this.gvInPlan.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvInPlan.DataSource = dt1;
                    this.gvInPlan.DataBind();
                    this.ShowBarChart();
                    //DataView dv2 = dt1.Copy().DefaultView;
                    //dv2.RowFilter = ("actcode = 'BBBBAAAAAAAA'");//  final 
                    //Session["Report1"] = gvInPlan;
                    //  ((HyperLink)this.gvInPlan.HeaderRow.FindControl("HLgvDesc")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



                    Session["Report1"] = gvInPlan;
                    ((HyperLink)this.gvInPlan.HeaderRow.FindControl("hlbtnCdataExelinv")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    DataTable dts1 = dt1;

                    for (i = 0; i < this.gvInPlan.Rows.Count; i++)
                    {
                        if (this.chkCore.Checked)
                        {
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvtosalesip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvtosalesip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvsalesip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvsalesip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvcollip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvcollip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvperonsaleip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvperonsaleip")).Text.Trim().Replace("%", ""))).ToString("#,##0.00;-#,##0.00; ");


                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvperontocolip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvperontocolip")).Text.Trim().Replace("%", ""))).ToString("#,##0.00;-#,##0.00; ");
                            //((Label)this.gvInPlan.Rows[i].FindControl("lblgvconBgdAmt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lblgvconBgdAmt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");

                            ((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacTRAmtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacTRAmtip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacCCAmtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacCCAmtip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");



                            ((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacBgdAmtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacBgdAmtip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvExpip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvExpip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvperonProip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvperonProip")).Text.Trim().Replace("%", ""))).ToString("#,##0.00;-#,##0.00; ");

                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvusoamtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvusoamtip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            //((Label)this.gvInPlan.Rows[i].FindControl("lgvrebgdamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvrebgdamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvrebgdamtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvrebgdamtip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            // ((Label)this.gvInPlan.Rows[i].FindControl("lgvstinvamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvstinvamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");

                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvrescramtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvrescramtip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvresdramtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvresdramtip")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");

                        }

                        else
                        {
                            //((Label)this.gvInPlan.Rows[i].FindControl("lgvtosales")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvtosales")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvsalesip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvsalesip")).Text.Trim())).ToString("#,##0;-#,##0; ");

                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvcollip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvcollip")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvperonsaleip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvperonsaleip")).Text.Trim().Replace("%", ""))).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvperontocolip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvperontocolip")).Text.Trim().Replace("%", ""))).ToString("#,##0.00;-#,##0.00; ");
                            //((Label)this.gvInPlan.Rows[i].FindControl("lblgvconBgdAmt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lblgvconBgdAmt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacTRAmtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacTRAmtip")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacCCAmtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacCCAmtip")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacBgdAmtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvacBgdAmtip")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvExpip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((HyperLink)this.gvInPlan.Rows[i].FindControl("HLgvExpip")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvperonProip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvperonProip")).Text.Trim().Replace("%", ""))).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvusoamtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvusoamtip")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            //((Label)this.gvInPlan.Rows[i].FindControl("lgvrebgdamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvrebgdamt")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvrebgdamtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvrebgdamtip")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            //   ((Label)this.gvInPlan.Rows[i].FindControl("lgvstinvamt")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvstinvamt")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvrescramtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvrescramtip")).Text.Trim())).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvInPlan.Rows[i].FindControl("lgvresdramtip")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvInPlan.Rows[i].FindControl("lgvresdramtip")).Text.Trim())).ToString("#,##0;-#,##0; ");



                        }
                    }// lgvtosales.Style.Add("width", "50px");




                    if (this.chkCore.Checked)
                    {


                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFToSalValip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(tosalval)", "")) ? 0.00 : dts1.Compute("sum(tosalval)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFSaleAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(salamt)", "")) ? 0.00 : dts1.Compute("sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFCollAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(collamt)", "")) ? 0.00 : dts1.Compute("sum(collamt)", ""))).ToString("#,##0;(#,##0); ");
                        //((Label)this.gvInPlan.FooterRow.FindControl("lgvFintamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(intamt)", "")) ? 0.00 : dts1.Compute("sum(intamt)", ""))).ToString("#,##0;(#,##0); ");

                        //  ((Label)this.gvInPlan.FooterRow.FindControl("lgvFperonsale")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(perontosal)", "")) ? 0.00 : dts1.Compute("sum(perontosal)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";
                        // ((Label)this.gvInPlan.FooterRow.FindControl("lgvFperontocol")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(perontocol)", "")) ? 0.00 : dts1.Compute("sum(perontocol)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";


                        // ((Label)this.gvInPlan.FooterRow.FindControl("lgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(bgdamt)", "")) ? 0.00 : dts1.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFconAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(cbgdamt)", "")) ? 0.00 : dts1.Compute("sum(cbgdamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFacBgdAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(acbgdamt)", "")) ? 0.00 : dts1.Compute("sum(acbgdamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFExpAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(examt)", "")) ? 0.00 : dts1.Compute("sum(examt)", ""))).ToString("#,##0;(#,##0); ");

                        //((Label)this.gvInPlan.FooterRow.FindControl("lgvFperonPro")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(peronpgres)", "")) ? 0.00 : dts1.Compute("sum(peronpgres)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";

                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFusoamtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(usoamt)", "")) ? 0.00 : dts1.Compute("sum(usoamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFRbgdAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(rbgdamt)", "")) ? 0.00 : dts1.Compute("sum(rbgdamt)", ""))).ToString("#,##0;(#,##0); ");


                        //  ((Label)this.gvInPlan.FooterRow.FindControl("lgvFstinvamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(stinvamt)", "")) ? 0.00 : dts1.Compute("sum(stinvamt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFresdramtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(invreqamt)", "")) ? 0.00 : dts1.Compute("sum(invreqamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFrescramtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(invblkamt)", "")) ? 0.00 : dts1.Compute("sum(invblkamt)", ""))).ToString("#,##0;(#,##0); ");



                        //Session["Report1"] = gvColvsExp;
                        //((HyperLink)this.gvColvsExp.HeaderRow.FindControl("hlbtnProject")).NavigateUrl = "~/F_32_Mis/RptProjectStatus.aspx?Type=PrjStatus";

                    }
                    else
                    {
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFToSalValip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(tosalval)", "")) ? 0.00 : dts1.Compute("sum(tosalval)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFSaleAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(salamt)", "")) ? 0.00 : dts1.Compute("sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFCollAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(collamt)", "")) ? 0.00 : dts1.Compute("sum(collamt)", ""))).ToString("#,##0;(#,##0); ");
                        //((Label)this.gvInPlan.FooterRow.FindControl("lgvFintamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(intamt)", "")) ? 0.00 : dts1.Compute("sum(intamt)", ""))).ToString("#,##0;(#,##0); ");

                        // ((Label)this.gvInPlan.FooterRow.FindControl("lgvFperonsale")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(perontosal)", "")) ? 0.00 : dts1.Compute("sum(perontosal)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";
                        // ((Label)this.gvInPlan.FooterRow.FindControl("lgvFperontocol")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(perontocol)", "")) ? 0.00 : dts1.Compute("sum(perontocol)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";

                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFconAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(cbgdamt)", "")) ? 0.00 : dts1.Compute("sum(cbgdamt)", ""))).ToString("#,##0;(#,##0); ");
                        // ((Label)this.gvInPlan.FooterRow.FindControl("lgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(bgdamt)", "")) ? 0.00 : dts1.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFacBgdAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(acbgdamt)", "")) ? 0.00 : dts1.Compute("sum(acbgdamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFExpAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(examt)", "")) ? 0.00 : dts1.Compute("sum(examt)", ""))).ToString("#,##0;(#,##0); ");

                        //  ((Label)this.gvInPlan.FooterRow.FindControl("lgvFperonPro")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(peronpgres)", "")) ? 0.00 : dts1.Compute("sum(peronpgres)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";

                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFusoamtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(usoamt)", "")) ? 0.00 : dts1.Compute("sum(usoamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFRbgdAmtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(rbgdamt)", "")) ? 0.00 : dts1.Compute("sum(rbgdamt)", ""))).ToString("#,##0;(#,##0); ");


                        //    ((Label)this.gvInPlan.FooterRow.FindControl("lgvFstinvamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(stinvamt)", "")) ? 0.00 : dts1.Compute("sum(stinvamt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFresdramtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(invreqamt)", "")) ? 0.00 : dts1.Compute("sum(invreqamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvInPlan.FooterRow.FindControl("lgvFrescramtip")).Text = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(invblkamt)", "")) ? 0.00 : dts1.Compute("sum(invblkamt)", ""))).ToString("#,##0;(#,##0); ");


                    }
                    break;


            }
        }



        private void ShowBarChart()
        {
            //a.comcod, a.pactcode, a.bgdamt, a.mplan , a.mplanat, a.eamt, a.leamt, a.perontw, a.peronac,  peronlp= a.perontw-a.peronac, pactdesc=isnull(b.acttdesc,'')  
            DataTable drt = ((DataTable)Session["tblcomsummary"]).Copy();


            this.gv01.DataSource = drt;
            this.gv01.DataBind();

            DataView dv = drt.DefaultView;
            dv.RowFilter = ("grp='1'");
            drt = dv.ToTable();



            // Chart Image

            this.txtbgdsales.Text = Math.Round(Convert.ToDouble(drt.Rows[0]["bgdsales"]), 2).ToString();
            this.txtsalam.Text = Math.Round(Convert.ToDouble(drt.Rows[0]["salam"]), 2).ToString();

            this.txtunsoldam.Text = Math.Round(Convert.ToDouble(drt.Rows[0]["unsoldam"]), 2).ToString();
            this.txtbgdcost.Text = Math.Round(Convert.ToDouble(drt.Rows[0]["bgdcost"]), 2).ToString();

            this.txtinvamt.Text = Math.Round(Convert.ToDouble(drt.Rows[0]["invamt"]), 2).ToString();
            this.txtremcost.Text = Math.Round(Convert.ToDouble(drt.Rows[0]["remcost"]), 2).ToString();

            this.txtfblock.Text = Math.Round(Convert.ToDouble(drt.Rows[0]["fblock"]), 2).ToString();
            this.txtfgenrate.Text = Math.Round(Convert.ToDouble(drt.Rows[0]["fgenrate"]), 2).ToString();






            //Chart1.Series["Series1"].YValueMembers = "bgdsales";
            //Chart1.Series["Series2"].YValueMembers = "salam";
            //Chart1.Series["Series3"].YValueMembers = "unsoldam";
            //Chart1.Series["Series4"].YValueMembers = "bgdcost";
            //Chart1.Series["Series5"].YValueMembers = "invamt";
            //Chart1.Series["Series6"].YValueMembers = "remcost";
            //Chart1.Series["Series14"].YValueMembers = "fblock";
            //Chart1.Series["Series15"].YValueMembers = "fgenrate";






            //Chart1.DataSource = drt;
            //Chart1.DataBind();
            ////   Chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = this.Chart1.dataCharting.ChartDashStyle.Dash;
            //Chart1.Series["Series1"].IsValueShownAsLabel = true;
            //Chart1.Series["Series2"].IsValueShownAsLabel = true;
            //Chart1.Series["Series3"].IsValueShownAsLabel = true;
            //Chart1.Series["Series4"].IsValueShownAsLabel = true;
            //Chart1.Series["Series5"].IsValueShownAsLabel = true;
            //Chart1.Series["Series6"].IsValueShownAsLabel = true;
            //Chart1.Series["Series14"].IsValueShownAsLabel = true;
            //Chart1.Series["Series15"].IsValueShownAsLabel = true;




            //Chart1.Series["Series1"].LabelAngle = 90;
            //Chart1.Series["Series1"]["BarLabelStyle"] = "Center";
            //Chart1.Series["Series1"]["DrawingStyle"] = "Cylinder";
            //Chart1.Series["Series1"]["ShowMarkerLines"] = "true";
            //Chart1.Series["Series1"]["PointWidth"] = "2";
            //// Chart1.Series["Series1"]["groupPadding"] = "2";

            //Chart1.Series["Series2"].LabelAngle = 90;
            //Chart1.Series["Series2"]["BarLabelStyle"] = "Center";
            //Chart1.Series["Series2"]["DrawingStyle"] = "Cylinder";
            //Chart1.Series["Series2"]["ShowMarkerLines"] = "true";
            //Chart1.Series["Series2"]["PointWidth"] = "2";
            ////  Chart1.Series["Series1"]["groupPadding"] = "2";

            //Chart1.Series["Series3"].LabelAngle = 90;
            //Chart1.Series["Series3"]["BarLabelStyle"] = "Center";
            //Chart1.Series["Series3"]["DrawingStyle"] = "Cylinder";
            //Chart1.Series["Series3"]["ShowMarkerLines"] = "true";
            //Chart1.Series["Series3"]["PointWidth"] = "2";


            //Chart1.Series["Series4"].LabelAngle = 90;
            //Chart1.Series["Series4"]["BarLabelStyle"] = "Center";
            //Chart1.Series["Series4"]["DrawingStyle"] = "Cylinder";
            //Chart1.Series["Series4"]["ShowMarkerLines"] = "true";
            //Chart1.Series["Series4"]["PointWidth"] = "2";

            //Chart1.Series["Series5"].LabelAngle = 90;
            //Chart1.Series["Series5"]["BarLabelStyle"] = "Center";
            //Chart1.Series["Series5"]["DrawingStyle"] = "Cylinder";
            //Chart1.Series["Series5"]["ShowMarkerLines"] = "true";
            //Chart1.Series["Series5"]["PointWidth"] = "2";

            //Chart1.Series["Series6"].LabelAngle = 90;
            //Chart1.Series["Series6"]["BarLabelStyle"] = "Center";
            //Chart1.Series["Series6"]["DrawingStyle"] = "Cylinder";
            //Chart1.Series["Series6"]["ShowMarkerLines"] = "true";
            //Chart1.Series["Series6"]["PointWidth"] = "2";



            //Chart1.Series["Series14"].LabelAngle = 90;
            //Chart1.Series["Series14"]["BarLabelStyle"] = "Center";
            //Chart1.Series["Series14"]["DrawingStyle"] = "Cylinder";
            //Chart1.Series["Series14"]["ShowMarkerLines"] = "true";
            //Chart1.Series["Series14"]["PointWidth"] = "2";

            //Chart1.Series["Series15"].LabelAngle = 90;
            //Chart1.Series["Series15"]["BarLabelStyle"] = "Center";
            //Chart1.Series["Series15"]["DrawingStyle"] = "Cylinder";
            //Chart1.Series["Series15"]["ShowMarkerLines"] = "true";
            //Chart1.Series["Series15"]["PointWidth"] = "2";




            // Chart1.Series["Series15"].Color = System.Drawing.Color.FromArgb(0, 128, 0);


            // System.Drawing.Color.FromArgb(255, 0, 0);

        }

        protected void gvInPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvInPlan.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgvActDesc");
                Label lgvtopamt = (Label)e.Row.FindControl("lgvtopamt");
                Label lgvp1 = (Label)e.Row.FindControl("lgvp1");
                Label lgvp2 = (Label)e.Row.FindControl("lgvp2");
                Label lgvp3 = (Label)e.Row.FindControl("lgvp3");
                Label lgvp4 = (Label)e.Row.FindControl("lgvp4");
                Label lgvp5 = (Label)e.Row.FindControl("lgvp5");
                Label lgvp6 = (Label)e.Row.FindControl("lgvp6");
                Label lgvp7 = (Label)e.Row.FindControl("lgvp7");
                Label lgvp8 = (Label)e.Row.FindControl("lgvp8");
                Label lgvp9 = (Label)e.Row.FindControl("lgvp9");
                Label lgvp10 = (Label)e.Row.FindControl("lgvp10");
                Label lgvp11 = (Label)e.Row.FindControl("lgvp11");
                Label lgvp12 = (Label)e.Row.FindControl("lgvp12");
                //Label lgvp13 = (Label)e.Row.FindControl("lgvp13");
                //Label lgvp14 = (Label)e.Row.FindControl("lgvp14");
                //Label lgvp15 = (Label)e.Row.FindControl("lgvp15");
                //Label lgvp16 = (Label)e.Row.FindControl("lgvp16");
                //Label lgvp17 = (Label)e.Row.FindControl("lgvp17");
                //Label lgvp18 = (Label)e.Row.FindControl("lgvp18");
                //Label lgvp19 = (Label)e.Row.FindControl("lgvp19");
                //Label lgvp20 = (Label)e.Row.FindControl("lgvp20");
                //Label lgvp21 = (Label)e.Row.FindControl("lgvp21");
                //Label lgvp22 = (Label)e.Row.FindControl("lgvp22");
                //Label lgvp23 = (Label)e.Row.FindControl("lgvp23");
                //Label lgvp24 = (Label)e.Row.FindControl("lgvp24");
                //Label lgvp25 = (Label)e.Row.FindControl("lgvp25");
                //Label lgvp26 = (Label)e.Row.FindControl("lgvp26");
                //Label lgvp27 = (Label)e.Row.FindControl("lgvp27");
                //Label lgvp28 = (Label)e.Row.FindControl("lgvp28");
                //Label lgvp29 = (Label)e.Row.FindControl("lgvp29");
                //Label lgvp30 = (Label)e.Row.FindControl("lgvp30");
                //Label lgvp31 = (Label)e.Row.FindControl("lgvp31");
                //Label lgvp32 = (Label)e.Row.FindControl("lgvp32");
                //Label lgvp33 = (Label)e.Row.FindControl("lgvp33");
                //Label lgvp34 = (Label)e.Row.FindControl("lgvp34");
                //Label lgvp35 = (Label)e.Row.FindControl("lgvp35");
                //Label lgvp36 = (Label)e.Row.FindControl("lgvp36");
                //Label lgvp37 = (Label)e.Row.FindControl("lgvp37");
                //Label lgvp38 = (Label)e.Row.FindControl("lgvp38");
                //Label lgvp39 = (Label)e.Row.FindControl("lgvp39");
                //Label lgvp40 = (Label)e.Row.FindControl("lgvp40");
                //Label lgvp41 = (Label)e.Row.FindControl("lgvp41");
                //Label lgvp42 = (Label)e.Row.FindControl("lgvp42");
                //Label lgvp43 = (Label)e.Row.FindControl("lgvp43");
                //Label lgvp44 = (Label)e.Row.FindControl("lgvp44");
                //Label lgvp45 = (Label)e.Row.FindControl("lgvp45");
                //Label lgvp46 = (Label)e.Row.FindControl("lgvp46");
                //Label lgvp47 = (Label)e.Row.FindControl("lgvp47");
                //Label lgvp48 = (Label)e.Row.FindControl("lgvp48");
                //Label lgvp49 = (Label)e.Row.FindControl("lgvp49");
                //Label lgvp50 = (Label)e.Row.FindControl("lgvp50");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA" || ASTUtility.Left(code, 2) == "51")
                {

                    acresdesc.Font.Bold = true;
                    lgvtopamt.Font.Bold = true;
                    lgvp1.Font.Bold = true;
                    lgvp2.Font.Bold = true;
                    lgvp3.Font.Bold = true;
                    lgvp4.Font.Bold = true;
                    lgvp5.Font.Bold = true;
                    lgvp6.Font.Bold = true;
                    lgvp7.Font.Bold = true;
                    lgvp8.Font.Bold = true;
                    lgvp9.Font.Bold = true;
                    lgvp10.Font.Bold = true;
                    lgvp11.Font.Bold = true;
                    lgvp12.Font.Bold = true;
                    //lgvp13.Font.Bold = true;
                    //lgvp14.Font.Bold = true;
                    //lgvp15.Font.Bold = true;
                    //lgvp16.Font.Bold = true;
                    //lgvp17.Font.Bold = true;
                    //lgvp18.Font.Bold = true;
                    //lgvp19.Font.Bold = true;
                    //lgvp20.Font.Bold = true;
                    //lgvp21.Font.Bold = true;
                    //lgvp22.Font.Bold = true;
                    //lgvp23.Font.Bold = true;
                    //lgvp24.Font.Bold = true;
                    //lgvp25.Font.Bold = true;
                    //lgvp26.Font.Bold = true;
                    //lgvp27.Font.Bold = true;
                    //lgvp28.Font.Bold = true;
                    //lgvp29.Font.Bold = true;
                    //lgvp30.Font.Bold = true;
                    //lgvp31.Font.Bold = true;
                    //lgvp32.Font.Bold = true;
                    //lgvp33.Font.Bold = true;
                    //lgvp34.Font.Bold = true;
                    //lgvp35.Font.Bold = true;
                    //lgvp36.Font.Bold = true;
                    //lgvp37.Font.Bold = true;
                    //lgvp38.Font.Bold = true;
                    //lgvp39.Font.Bold = true;
                    //lgvp40.Font.Bold = true;
                    //lgvp41.Font.Bold = true;
                    //lgvp42.Font.Bold = true;
                    //lgvp43.Font.Bold = true;
                    //lgvp44.Font.Bold = true;
                    //lgvp45.Font.Bold = true;
                    //lgvp46.Font.Bold = true;
                    //lgvp47.Font.Bold = true;
                    //lgvp48.Font.Bold = true;
                    //lgvp49.Font.Bold = true;
                    //lgvp50.Font.Bold = true;
                    if (ASTUtility.Right(code, 4) == "AAAA")
                        acresdesc.Style.Add("text-align", "right");
                }

            }


        }

        protected void gvSource_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgvActDesc");
                Label lgvtoamt = (Label)e.Row.FindControl("lgvtoamt");
                Label lgvamtup = (Label)e.Row.FindControl("lgvamtup");
                Label lgvtoamtwp = (Label)e.Row.FindControl("lgvamttoamtwp");
                Label lgvamt1 = (Label)e.Row.FindControl("lgvamt1");
                Label lgvamt2 = (Label)e.Row.FindControl("lgvamt2");
                Label lgvamt3 = (Label)e.Row.FindControl("lgvamt3");
                Label lgvamt4 = (Label)e.Row.FindControl("lgvamt4");
                Label lgvamt5 = (Label)e.Row.FindControl("lgvamt5");
                Label lgvamt6 = (Label)e.Row.FindControl("lgvamt6");
                Label lgvamt7 = (Label)e.Row.FindControl("lgvamt7");
                Label lgvamt8 = (Label)e.Row.FindControl("lgvamt8");
                Label lgvamt9 = (Label)e.Row.FindControl("lgvamt9");
                Label lgvamt10 = (Label)e.Row.FindControl("lgvamt10");
                Label lgvamt11 = (Label)e.Row.FindControl("lgvamt11");
                Label lgvamt12 = (Label)e.Row.FindControl("lgvamt12");
                Label lgvamt13 = (Label)e.Row.FindControl("lgvamt13");
                Label lgvamt14 = (Label)e.Row.FindControl("lgvamt14");
                Label lgvamt15 = (Label)e.Row.FindControl("lgvamt15");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    acresdesc.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvamtup.Font.Bold = true;
                    lgvtoamtwp.Font.Bold = true;
                    lgvamt1.Font.Bold = true;
                    lgvamt2.Font.Bold = true;
                    lgvamt3.Font.Bold = true;
                    lgvamt4.Font.Bold = true;
                    lgvamt5.Font.Bold = true;
                    lgvamt6.Font.Bold = true;
                    lgvamt7.Font.Bold = true;
                    lgvamt8.Font.Bold = true;
                    lgvamt9.Font.Bold = true;
                    lgvamt10.Font.Bold = true;
                    lgvamt11.Font.Bold = true;
                    lgvamt12.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");
                }

            }

        }


        protected void gvProDetials_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvResDescd = (Label)e.Row.FindControl("lgvResDescd");
                Label lgvtoamt = (Label)e.Row.FindControl("lgvtoamtd");
                Label lgvamtup = (Label)e.Row.FindControl("lgvamtupd");
                Label lgvamt1 = (Label)e.Row.FindControl("lgvamt1d");
                Label lgvamt2 = (Label)e.Row.FindControl("lgvamt2d");
                Label lgvamt3 = (Label)e.Row.FindControl("lgvamt3d");
                Label lgvamt4 = (Label)e.Row.FindControl("lgvamt4d");
                Label lgvamt5 = (Label)e.Row.FindControl("lgvamt5d");
                Label lgvamt6 = (Label)e.Row.FindControl("lgvamt6d");
                Label lgvamt7 = (Label)e.Row.FindControl("lgvamt7d");
                Label lgvamt8 = (Label)e.Row.FindControl("lgvamt8d");
                Label lgvamt9 = (Label)e.Row.FindControl("lgvamt9d");
                Label lgvamt10 = (Label)e.Row.FindControl("lgvamt10d");
                Label lgvamt11 = (Label)e.Row.FindControl("lgvamt11d");
                Label lgvamt12 = (Label)e.Row.FindControl("lgvamt12d");
                Label lgvamt13 = (Label)e.Row.FindControl("lgvamt13d");
                Label lgvamt14 = (Label)e.Row.FindControl("lgvamt14d");
                Label lgvamt15 = (Label)e.Row.FindControl("lgvamt15d");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvResDescd.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvamtup.Font.Bold = true;
                    lgvamt1.Font.Bold = true;
                    lgvamt2.Font.Bold = true;
                    lgvamt3.Font.Bold = true;
                    lgvamt4.Font.Bold = true;
                    lgvamt5.Font.Bold = true;
                    lgvamt6.Font.Bold = true;
                    lgvamt7.Font.Bold = true;
                    lgvamt8.Font.Bold = true;
                    lgvamt9.Font.Bold = true;
                    lgvamt10.Font.Bold = true;
                    lgvamt11.Font.Bold = true;
                    lgvamt12.Font.Bold = true;
                    lgvResDescd.Style.Add("text-align", "right");
                }

            }
        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetAccCode();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }

        protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnShowInPlan_Click(null, null);


        }

        protected void GetProjectType()
        {
            string comcod = this.GetComeCode();
            //  string txtprosearch = this.txtSrcProject.Text.Trim() + "%";
            // string Level2 = (this.Request.QueryString["Type"].ToString().Trim() == "ProDetails") ? "LEVEL2" : "";
            DataSet ds4 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "GETPROJECTTYPE", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.ddlProjectType.Items.Clear();
                return;
            }

            DataRow dr = ds4.Tables[0].NewRow();
            dr["catdesc"] = "All Project Type";
            dr["catcode"] = "00000";
            ds4.Tables[0].Rows.InsertAt(dr, 0);

            //this.ddlProjectType.Items.Insert(0, "--Select--");
            this.ddlProjectType.DataTextField = "catdesc";
            this.ddlProjectType.DataValueField = "catcode";
            this.ddlProjectType.DataSource = ds4.Tables[0];
            this.ddlProjectType.DataBind();





        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MasterBgd":
                    this.PrinMasterBgd();
                    break;

                case "SrAUtilities":
                case "SrAUtilitiesFF":
                    this.PrintSourceAndUtilities();
                    break;

                case "ProDetails":
                    this.PrintProjectDetails();
                    break;


                case "ProExpenses":
                    this.PrintProjectExpenses();
                    break;

                case "BgdVsExpenses":
                    this.PrintBgdVsExp();
                    break; ;
                case "BgdVsActual":
                    this.PrintBgdVsAct();
                    break;

                case "SalesVsColection":
                    this.PrintSalesVsCol();
                    break;

                case "ComProCost":
                    this.PrintComProCost();
                    break;

                case "ColVsExpenses":
                    this.PrintColVsExpenses();
                    break;

                case "CostOfFund":
                    this.PrintCostOfFund();
                    break;

                case "InvPlan":
                    this.PrintInvestmentPlan();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetComeCode();
                //string eventtype = this.lblHtitle.Text;
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "";
                //  bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        private void PrintInvestmentPlan()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            if (dt1.Rows.Count == 0)
                return;
            if (chkCore.Checked)
            {
                var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.EClassMasterBgd>();
                LocalReport Rpt2 = new LocalReport();
                Rpt2 = RptSetupClass1.GetLocalReport("R_32_Mis.RptInvestmentPlan02", lst, null, null);
                string date = "As On Date: " + this.txtDateInPlan.Text.Trim();
                string comcod = hst["comcod"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
                Rpt2.EnableExternalImages = true;
                Rpt2.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt2.SetParameters(new ReportParameter("companyname", comnam));
                Rpt2.SetParameters(new ReportParameter("title", "Invesment Plan Summary - All Project"));
                Rpt2.SetParameters(new ReportParameter("date", date));
                Rpt2.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
                Session["Report1"] = Rpt2;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                //       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

                //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptInvestmentPlan02();

                //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
                //txtCompany.Text = comnam;
                ////TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                ////txtTitle.Text = "Budget vs Expenses- All Projects";

                ////TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                ////txtDate.Text = this.txtDate.Text.Trim();



                //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //rptstk.SetDataSource(dt1);
                ////string comcod = this.GetComeCode();
                //string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptstk.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptstk;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }


            else
            {
                var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.EClassMasterBgd>();
                LocalReport Rpt2 = new LocalReport();
                Rpt2 = RptSetupClass1.GetLocalReport("R_32_Mis.RptInvestmentPlan", lst, null, null);
                string date = "As On Date: " + this.txtDateInPlan.Text.Trim();
                string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
                string comcod = hst["comcod"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                Rpt2.EnableExternalImages = true;
                Rpt2.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt2.SetParameters(new ReportParameter("title", "Invesment Plan - All Project"));
                Rpt2.SetParameters(new ReportParameter("companyname", comnam));

                Rpt2.SetParameters(new ReportParameter("date", date));
                Rpt2.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
                Session["Report1"] = Rpt2;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                //       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



                //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptInvestmentPlan();
                //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
                //txtCompany.Text = comnam;
                ////TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                ////txtTitle.Text = "Budget vs Expenses- All Projects";

                //TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                //txtDate.Text ="As On Date: "+ this.txtDateInPlan.Text.Trim();



                //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //rptstk.SetDataSource(dt1);
                ////string comcod = this.GetComeCode();
                //string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptstk.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptstk;


            }
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrinMasterBgd()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            ReportDocument rptstk = new ReportDocument();
            rptstk = new RealERPRPT.R_32_Mis.RptMasterBgd();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            int j = 1;
            for (int i = 0; i < this.cblProject.Items.Count; i++)
            {

                if (cblProject.Items[i].Selected)
                {
                    string header = this.cblProject.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 13)
                        break;
                }

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            //  }


            //else 
            //{
            //    rptstk = new RealERPRPT.R_32_Mis.RptBgdProjectTotal();
            //    TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //    txtCompany.Text = comnam;
            //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptstk.SetDataSource(dt1);

            //    string comcod = hst["comcod"].ToString();
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptstk.SetParameterValue("ComLogo", ComLogo);
            //}
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSourceAndUtilities()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblmasterbgd"];
            if (dt.Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.IndiProjCost12Month>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptSourceAndUtilities", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", (this.Request.QueryString["Type"].ToString() == "SrAUtilities") ? "Sources & Utilization - Cash Basis" : "Sources & Utilization - Acural Basis"));
            Rpt1.SetParameters(new ReportParameter("projectName", this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            DateTime datefrm = Convert.ToDateTime(this.txtDatefromd.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txtDatetod.Text.Trim());
            string rpttxth = "";
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                rpttxth = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);
                Rpt1.SetParameters(new ReportParameter("txtamt" + i.ToString(), rpttxth));

            }
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptSourceAndUtilities();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = (this.Request.QueryString["Type"].ToString() == "SrAUtilities") ? "Sources & Utilization - Cash Basis" : "Sources & Utilization - Acural Basis";
            //DateTime datefrm = Convert.ToDateTime(this.txtDatefrom.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txtDateto.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            ////string comcod = this.GetComeCode();
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        //private void PrintSourceAndUtilitiesWOP()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    DataTable dt1 = (DataTable)Session["tblmasterbgd"];
        //    if (dt1.Rows.Count == 0)
        //        return;
        //    ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptSourceAndUtilitiesBDate();
        //    TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
        //    txtCompany.Text = comnam;
        //    TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
        //    txtTitle.Text = "Sources & Utilitilization of Fund";
        //    DateTime datefrm = Convert.ToDateTime(this.txtDatefrom.Text.Trim());
        //    DateTime dateto = Convert.ToDateTime(this.txtDateto.Text.Trim());
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        if (datefrm > dateto)
        //            break;
        //        TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
        //        rpttxth.Text = datefrm.ToString("MMM yy");
        //        datefrm = datefrm.AddMonths(1);

        //    }

        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptstk.SetDataSource(dt1);
        //   // string comcod = this.GetComeCode();
        //    string comcod = hst["comcod"].ToString();
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptstk.SetParameterValue("ComLogo", ComLogo);

        //    Session["Report1"] = rptstk;
        //    this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        //}


        private void PrintProjectDetails()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblmasterbgd"];
            if (dt.Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.IndiProjCost12Month>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptSourceAndUtilities", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Sources & Utilities"));
            Rpt1.SetParameters(new ReportParameter("projectName", this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            DateTime datefrm = Convert.ToDateTime(this.txtDatefromd.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txtDatetod.Text.Trim());
            string rpttxth = "";
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                rpttxth = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);
                Rpt1.SetParameters(new ReportParameter("txtamt" + i.ToString(), rpttxth));

            }
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintProjectExpenses()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblmasterbgd"];
            if (dt.Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.IndiProjCost12Month>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptSourceAndUtilities", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Head Office overhead-last 12 Month Report"));
            Rpt1.SetParameters(new ReportParameter("projectName", this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            DateTime datefrm = Convert.ToDateTime(this.txtDatefromd.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txtDatetod.Text.Trim());
            string rpttxth = "";
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                rpttxth = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);
                Rpt1.SetParameters(new ReportParameter("txtamt" + i.ToString(), rpttxth));

            }
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptSourceAndUtilities();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Head Office overhead-last 12 Month Report";
            //TextObject txtprojecname = rptstk.ReportDefinition.ReportObjects["txtprojecname"] as TextObject;
            //txtprojecname.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);

            //DateTime datefrm = Convert.ToDateTime(this.txtDatefromd.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txtDatetod.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            ////string comcod = this.GetComeCode();
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintBgdVsExp()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            if (dt1.Rows.Count == 0)
                return;

            var list = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.BudgetVsExpensesAllProj>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptmBgdVsExp", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("date", "As On:" + this.txtDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Budget vs Expenses- All Projects"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptmBgdVsExp();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Budget vs Expenses- All Projects";

            //TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "As On:" + this.txtDate.Text.Trim();



            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            ////string comcod = this.GetComeCode();
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintBgdVsAct()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.txtDateb.Text.Trim();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)Session["tblmasterbgd"];

            var list = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.RptBgdvsActual>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_32_Mis.RptBgdVsAct", list, null, null);
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintSalesVsCol()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptColVsExp();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = "Sales VS Collection- All Projects";

            TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "As On:" + this.txtDateCollect.Text.Trim();
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            // string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintComProCost()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            if (dt1.Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.AnyCostAllProject>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptComProCost", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("dateFromTo", "From " + Convert.ToDateTime(this.txtDatefromCom.Text.Trim()).ToString("dd.MM.yyyy") + " To " + Convert.ToDateTime(this.txtDatetoCom.Text.Trim()).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Comparative Project Expenses"));
            Rpt1.SetParameters(new ReportParameter("detLev", this.ddlDetailsCode.SelectedItem.ToString().Substring(3) + " - " + this.ddlReportLevelCom.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            int j = 1;
            //string rpttxth = "";

            DataTable dtpname = (DataTable)ViewState["tblproname"];
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {
                Rpt1.SetParameters(new ReportParameter(("txtp" + j.ToString()), dtpname.Rows[i]["pactdesc"].ToString()));
                //rpttxth = dtpname.Rows[i]["pactdesc"].ToString();
                j++;
                if (j == 13)
                    break;
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptComProCost();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //int j = 1;


            //DataTable dtpname = (DataTable)ViewState["tblproname"];
            //for (int i = 0; i < dtpname.Rows.Count; i++)
            //{

            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
            //    rpttxth.Text = dtpname.Rows[i]["pactdesc"].ToString();
            //    j++;
            //    if (j == 13)
            //        break;
            //}

            //TextObject DateFrmTo = rptstk.ReportDefinition.ReportObjects["DateFrmTo"] as TextObject;
            //DateFrmTo.Text = "From " + Convert.ToDateTime(this.txtDatefromCom.Text.Trim()).ToString("dd.MM.yyyy") + " To " + Convert.ToDateTime(this.txtDatetoCom.Text.Trim()).ToString("dd.MM.yyyy");
            //TextObject DetLev = rptstk.ReportDefinition.ReportObjects["DetLev"] as TextObject;
            //DetLev.Text = this.ddlDetailsCode.SelectedItem.ToString().Substring(3) + " - " + this.ddlReportLevelCom.SelectedItem.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO1.jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintColVsExpenses()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptColVsExp1();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;


            TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "As On:" + this.txtDateExpens.Text.Trim();

            TextObject txtColl = rptstk.ReportDefinition.ReportObjects["txtColl"] as TextObject;
            txtColl.Text = this.txtColl.Text;
            TextObject txtNp = rptstk.ReportDefinition.ReportObjects["txtNp"] as TextObject;
            txtNp.Text = this.txtNp.Text;
            TextObject txtBgd = rptstk.ReportDefinition.ReportObjects["txtBgd"] as TextObject;
            txtBgd.Text = this.txtBgd.Text;

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintCostOfFund()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string intrsrate = "Interest Rate(Per Month): " + this.TxtIntrstRate.Text.Trim();
            string txtscale = "Tk In Lac";
            string date = "As On:" + this.txtDateCostofFund.Text.Trim();

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblmasterbgd"];

            if (dt1.Rows.Count == 0)
                return;

            var list = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.RptBankInterestAllocation>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptCostOfFund", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("title", "Interest Calculation"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("intrsrate", intrsrate));
            Rpt1.SetParameters(new ReportParameter("txtscale", txtscale));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptCostOfFund();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "As On:" + this.txtDateCostofFund.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //TextObject IntrsrRate = rptstk.ReportDefinition.ReportObjects["IntrstRate"] as TextObject;
            //IntrsrRate.Text = "Interest Rate(Per Month): " + this.TxtIntrstRate.Text.Trim();
            //rptstk.SetDataSource(dt1);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        protected void lbtnShowComCost_Click(object sender, EventArgs e)
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();

            string txtdatefrm = Convert.ToDateTime(this.txtDatefromCom.Text).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txtDatetoCom.Text).ToString("dd-MMM-yyyy");
            string txtdetailCode = this.ddlDetailsCode.SelectedValue.Substring(0, 2);

            string matList = "";
            foreach (ListItem item in ddlDetailsCode.Items)
            {
                if (item.Selected)
                {
                    matList += item.Value.Substring(0, 2);
                }
            }





            string level = this.ddlReportLevelCom.SelectedValue.ToString();
            string OnlyProject = this.ddlReportLevelCom.SelectedValue.ToString();

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTCOMPROJECTCOST", "", txtdatefrm, txtdateto, matList, level, "", "", "", "");
            if (ds1 == null)
            {
                this.gvComCost.DataSource = null;
                this.gvComCost.DataBind();
                return;
            }


            Session["tblmasterbgd"] = ds1.Tables[0];
            ViewState["tblproname"] = ds1.Tables[1];
            this.Data_Bind();
        }
        protected void imgbtnDetailsCode_Click(object sender, ImageClickEventArgs e)
        {
            this.GetResCode();
        }

        protected void gvCostOfFund_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lgvActDesc");
                Label Exp = (Label)e.Row.FindControl("lgvExp");
                Label Col = (Label)e.Row.FindControl("lgvcoll");
                Label Difamt = (Label)e.Row.FindControl("lgvDiffAmt");
                Label fund = (Label)e.Row.FindControl("lgvcostfund");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 1) == "A" || ASTUtility.Right(code, 1) == "B" || ASTUtility.Right(code, 1) == "C")
                {

                    groupdesc.Font.Bold = true;
                    Exp.Font.Bold = true;
                    Col.Font.Bold = true;
                    Difamt.Font.Bold = true;
                    fund.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");


                }


            }
        }

        protected void gvColvsExp_RowCreated(object sender, GridViewRowEventArgs e)
        {





            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvtosales = (Label)e.Row.FindControl("lgvtosales");
                Label lgvsales = (Label)e.Row.FindControl("lgvsales");
                Label lgvcoll = (Label)e.Row.FindControl("lgvcoll");
                Label lgvrecoll = (Label)e.Row.FindControl("lgvrecoll");
                Label lgvBgdAmt = (Label)e.Row.FindControl("lgvBgdAmt");
                Label lgvExpAmt = (Label)e.Row.FindControl("lgvExp");
                Label lgvRbgdAmt = (Label)e.Row.FindControl("lgvrebgdamt");
                Label lgvresdramt = (Label)e.Row.FindControl("lgvresdramt");
                Label lgvrescramt = (Label)e.Row.FindControl("lgvrescramt");
                Label lgvusoamt = (Label)e.Row.FindControl("lgvusoamt");
                Label lgvfsamt = (Label)e.Row.FindControl("lgvfsamt");
                Label lgvfsuamt = (Label)e.Row.FindControl("lgvfsuamt");
                Label lgvlibamt = (Label)e.Row.FindControl("lgvlibamt");
                Label lgvnoiamt = (Label)e.Row.FindControl("lgvnoiamt");
                Label lgvltoamt = (Label)e.Row.FindControl("lgvltoamt");
                Label lgvlfrmamt = (Label)e.Row.FindControl("lgvlfrmamt");
                Label lgvbgdsaamt = (Label)e.Row.FindControl("lgvbgdsaamt");
                Label lgvnp = (Label)e.Row.FindControl("lgvnp");



                // Label lgvtosales = (Label)e.FindControl("lgvtosales");



                if (this.chkCrore.Checked)
                {
                    lgvtosales.Style.Add("width", "50px");
                    lgvsales.Style.Add("width", "50px");
                    lgvcoll.Style.Add("width", "50px");
                    lgvrecoll.Style.Add("width", "50px");
                    lgvBgdAmt.Style.Add("width", "50px");
                    lgvExpAmt.Style.Add("width", "50px");
                    lgvRbgdAmt.Style.Add("width", "50px");
                    lgvresdramt.Style.Add("width", "50px");
                    lgvrescramt.Style.Add("width", "50px");
                    lgvusoamt.Style.Add("width", "50px");
                    lgvfsamt.Style.Add("width", "50px");
                    lgvfsuamt.Style.Add("width", "50px");
                    lgvlibamt.Style.Add("width", "50px");
                    lgvnoiamt.Style.Add("width", "50px");
                    lgvltoamt.Style.Add("width", "50px");
                    lgvlfrmamt.Style.Add("width", "50px");
                    lgvbgdsaamt.Style.Add("width", "50px");
                    lgvnp.Style.Add("width", "50px");


                }


                else
                {
                    lgvtosales.Style.Add("width", "90px");
                    lgvsales.Style.Add("width", "90px");
                    lgvcoll.Style.Add("width", "90px");
                    lgvrecoll.Style.Add("width", "90px");
                    lgvBgdAmt.Style.Add("width", "90px");
                    lgvExpAmt.Style.Add("width", "90px");
                    lgvRbgdAmt.Style.Add("width", "90px");
                    lgvresdramt.Style.Add("width", "90px");
                    lgvrescramt.Style.Add("width", "90px");
                    lgvusoamt.Style.Add("width", "90px");
                    lgvfsamt.Style.Add("width", "90px");
                    lgvfsuamt.Style.Add("width", "90px");
                    lgvlibamt.Style.Add("width", "90px");
                    lgvnoiamt.Style.Add("width", "90px");
                    lgvltoamt.Style.Add("width", "90px");
                    lgvlfrmamt.Style.Add("width", "90px");
                    lgvbgdsaamt.Style.Add("width", "90px");
                    lgvnp.Style.Add("width", "90px");
                    //lgvRemCol.Style.Add("width", "80px");

                }




            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lgvFToSalVal = ((Label)e.Row.FindControl("lgvFToSalVal"));
                Label lgvFSaleAmt = ((Label)e.Row.FindControl("lgvFSaleAmt"));
                Label lgvFCollAmt = ((Label)e.Row.FindControl("lgvFCollAmt"));
                Label lgvFreCollAmt = ((Label)e.Row.FindControl("lgvFreCollAmt"));
                Label lgvFBgdAmt = ((Label)e.Row.FindControl("lgvFBgdAmt"));
                Label lgvFExpAmt = ((Label)e.Row.FindControl("lgvFExpAmt"));
                Label lgvFRbgdAmt = ((Label)e.Row.FindControl("lgvFRbgdAmt"));
                Label lgvFresdramt = ((Label)e.Row.FindControl("lgvFresdramt"));
                Label lgvFrescramt = ((Label)e.Row.FindControl("lgvFrescramt"));
                Label lgvFusoamt = ((Label)e.Row.FindControl("lgvFusoamt"));
                Label lgvFfsamt = ((Label)e.Row.FindControl("lgvFfsamt"));
                Label lgvFfsuamt = ((Label)e.Row.FindControl("lgvFfsuamt"));
                Label lgvFlibamt = ((Label)e.Row.FindControl("lgvFlibamt"));
                Label lgvFnoiamt = ((Label)e.Row.FindControl("lgvFnoiamt"));
                Label lgvFltoamt = ((Label)e.Row.FindControl("lgvFltoamt"));
                Label lgvFlfrmamt = ((Label)e.Row.FindControl("lgvFlfrmamt"));
                Label lgvFbgdsaamt = ((Label)e.Row.FindControl("lgvFbgdsaamt"));
                Label lgvFnp = ((Label)e.Row.FindControl("lgvFnp"));



                //Label lgvFrColAmt = ((Label)e.Row.FindControl("lgvFrColAmt"));

                //((Label)this.gvColvsExp.FooterRow.FindControl("lgvFSaleAmt")).Text =

                if (this.chkCrore.Checked)
                {
                    lgvFToSalVal.Style.Add("width", "45px");
                    lgvFSaleAmt.Style.Add("width", "45px");
                    lgvFCollAmt.Style.Add("width", "45px");
                    lgvFreCollAmt.Style.Add("width", "45px");
                    lgvFBgdAmt.Style.Add("width", "45px");
                    lgvFExpAmt.Style.Add("width", "45px");
                    lgvFRbgdAmt.Style.Add("width", "45px");
                    lgvFresdramt.Style.Add("width", "45px");
                    lgvFrescramt.Style.Add("width", "45px");
                    lgvFusoamt.Style.Add("width", "45px");
                    lgvFfsamt.Style.Add("width", "45px");
                    lgvFfsuamt.Style.Add("width", "45px");
                    lgvFlibamt.Style.Add("width", "45px");
                    lgvFnoiamt.Style.Add("width", "45px");
                    lgvFltoamt.Style.Add("width", "45px");
                    lgvFlfrmamt.Style.Add("width", "45px");
                    lgvFbgdsaamt.Style.Add("width", "45px");
                    lgvFnp.Style.Add("width", "45px");
                    //lgvFrColAmt.Style.Add("width", "45px");

                }


                else
                {
                    lgvFToSalVal.Style.Add("width", "80px");
                    lgvFSaleAmt.Style.Add("width", "80px");
                    lgvFCollAmt.Style.Add("width", "90px");
                    lgvFreCollAmt.Style.Add("width", "80px");
                    lgvFBgdAmt.Style.Add("width", "80px");
                    lgvFExpAmt.Style.Add("width", "90px");
                    lgvFRbgdAmt.Style.Add("width", "90px");
                    lgvFresdramt.Style.Add("width", "80px");
                    lgvFrescramt.Style.Add("width", "80px");
                    lgvFusoamt.Style.Add("width", "80px");
                    lgvFfsamt.Style.Add("width", "90px");
                    lgvFfsuamt.Style.Add("width", "80px");
                    lgvFlibamt.Style.Add("width", "80px");
                    lgvFnoiamt.Style.Add("width", "80px");
                    lgvFltoamt.Style.Add("width", "80px");
                    lgvFlfrmamt.Style.Add("width", "80px");
                    lgvFbgdsaamt.Style.Add("width", "80px");
                    lgvFnp.Style.Add("width", "80px");
                    // lgvFrColAmt.Style.Add("width", "80px");

                }
            }









        }
        protected void gvColvsExp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvActDesc = (Label)e.Row.FindControl("lgvIActDesc");
                Label lgvtosales = (Label)e.Row.FindControl("lgvtosales");
                Label lgvsales = (Label)e.Row.FindControl("lgvsales");
                Label lgvcoll = (Label)e.Row.FindControl("lgvcoll");
                Label lgvrecoll = (Label)e.Row.FindControl("lgvrecoll");
                Label lgvBgdAmt = (Label)e.Row.FindControl("lgvBgdAmt");
                Label lgvExpAmt = (Label)e.Row.FindControl("lgvExp");
                Label lgvRbgdAmt = (Label)e.Row.FindControl("lgvrebgdamt");
                Label lgvresdramt = (Label)e.Row.FindControl("lgvresdramt");
                Label lgvrescramt = (Label)e.Row.FindControl("lgvrescramt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                double torevnue = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "tosalval"));

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvActDesc.Font.Bold = true;
                    lgvtosales.Font.Bold = true;
                    lgvsales.Font.Bold = true;
                    lgvcoll.Font.Bold = true;
                    lgvrecoll.Font.Bold = true;
                    lgvBgdAmt.Font.Bold = true;
                    lgvExpAmt.Font.Bold = true;
                    lgvRbgdAmt.Font.Bold = true;
                    lgvresdramt.Font.Bold = true;
                    lgvrescramt.Font.Bold = true;
                    lgvActDesc.Style.Add("text-align", "right");

                }
                if (torevnue == 0.00 && ASTUtility.Right(code, 4) != "AAAA")
                { lgvActDesc.Style.Add("color", "red"); }
            }
        }

        protected void gvInPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescip");
                // HyperLink HLgvacTRAmt = (HyperLink)e.Row.FindControl("HLgvacTRAmt");
                HyperLink HLgvacBgdAmt = (HyperLink)e.Row.FindControl("HLgvacBgdAmtip");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HLgvExpip");//
                HyperLink HLgvacCCAmt = (HyperLink)e.Row.FindControl("HLgvacCCAmtip");




                //Label lgvActDesc = (Label)e.Row.FindControl("lgvIActDesc");
                //Label lgvtosales = (Label)e.Row.FindControl("lgvtosales");
                //Label lgvsales = (Label)e.Row.FindControl("lgvsales");
                //Label lgvcoll = (Label)e.Row.FindControl("lgvcoll");
                //Label lgvrecoll = (Label)e.Row.FindControl("lgvrecoll");
                //Label lgvBgdAmt = (Label)e.Row.FindControl("lgvBgdAmt");
                //Label lgvExpAmt = (Label)e.Row.FindControl("lgvExp");
                //Label lgvRbgdAmt = (Label)e.Row.FindControl("lgvrebgdamt");
                //Label lgvresdramt = (Label)e.Row.FindControl("lgvresdramt");//
                Label lgvperonPro = (Label)e.Row.FindControl("lgvperonProip");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                double invreqamt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "invreqamt"));
                double invblkamt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "invblkamt"));
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    hlink1.Font.Bold = true;//
                                            //lgvtosales.Font.Bold = true;
                                            //lgvsales.Font.Bold = true;
                                            //lgvcoll.Font.Bold = true;
                                            //lgvrecoll.Font.Bold = true;
                                            //lgvBgdAmt.Font.Bold = true;
                                            //lgvExpAmt.Font.Bold = true;
                                            //lgvRbgdAmt.Font.Bold = true;
                                            //lgvresdramt.Font.Bold = true;
                                            //lgvperonPro.Font.Bold = true;//
                    hlink1.Style.Add("text-align", "right");

                }
                else
                {
                    // hlink1.HLgvacTRAmt = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal";
                    //hlink1.Style.Add("color", "blue");

                    HLgvacCCAmt.NavigateUrl = "~/F_32_Mis/RptConstruProgressSum.aspx";//?type=ConstRpt&prjcode="+code;
                    HLgvacCCAmt.Style.Add("color", "blue");

                    //HLgvacTRAmt.NavigateUrl = "~/F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold";
                    //HLgvacTRAmt.Style.Add("color", "blue");

                    HLgvacBgdAmt.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk&prjcode=" + code;
                    HLgvacBgdAmt.Style.Add("color", "blue");
                    hlink3.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal&prjcode=" + code;
                    hlink3.Style.Add("color", "blue");
                    hlink1.NavigateUrl = "~/F_32_Mis/LinkIndProjectStatus.aspx?prjcode=" + code + "&date=" + this.txtDateInPlan.Text.Trim();


                }
                //if (torevnue == 0.00 && ASTUtility.Right(code, 4) != "AAAA")
                //{ hlink1.Style.Add("color", "red"); }
                if (invblkamt != 0.00 && ASTUtility.Right(code, 4) != "AAAA")
                {
                    lgvperonPro.Style.Add("color", "red");
                    hlink1.Style.Add("color", "red");
                }
                if (invreqamt != 0.00 && ASTUtility.Right(code, 4) != "AAAA")
                {
                    hlink1.Style.Add("color", "green");
                }
            }
        }


        protected void gvInPlan_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lgvtosales = (Label)e.Row.FindControl("lgvtosalesip");
                Label lgvsales = (Label)e.Row.FindControl("lgvsalesip");
                Label lgvcoll = (Label)e.Row.FindControl("lgvcollip");
                Label lgvperonsale = (Label)e.Row.FindControl("lgvperonsaleip");

                Label lgvperontocol = (Label)e.Row.FindControl("lgvperontocolip");
                //Label lblgvconBgdAmt = (Label)e.Row.FindControl("lblgvconBgdAmt");

                HyperLink HLgvacTRAmt = (HyperLink)e.Row.FindControl("HLgvacTRAmtip");
                HyperLink HLgvacCCAmt = (HyperLink)e.Row.FindControl("HLgvacCCAmtip");


                HyperLink HLgvacBgdAmt = (HyperLink)e.Row.FindControl("HLgvacBgdAmtip");
                HyperLink HLgvExp = (HyperLink)e.Row.FindControl("HLgvExpip");//
                Label lgvperonPro = (Label)e.Row.FindControl("lgvperonProip");

                Label lgvusoamt = (Label)e.Row.FindControl("lgvusoamtip");
                Label lgvrebgdamt = (Label)e.Row.FindControl("lgvrebgdamtip");

                Label lgvrescramt = (Label)e.Row.FindControl("lgvrescramtip");
                Label lgvresdramt = (Label)e.Row.FindControl("lgvresdramtip");


                // Label lgvtosales = (Label)e.FindControl("lgvtosales");

                if (this.chkCore.Checked)
                {
                    lgvtosales.Style.Add("width", "50px");
                    lgvsales.Style.Add("width", "50px");
                    lgvcoll.Style.Add("width", "50px");
                    lgvperonsale.Style.Add("width", "50px");

                    lgvperontocol.Style.Add("width", "50px");
                    //lblgvconBgdAmt.Style.Add("width", "50px");

                    HLgvacTRAmt.Style.Add("width", "50px");
                    HLgvacCCAmt.Style.Add("width", "50px");


                    HLgvacBgdAmt.Style.Add("width", "50px");
                    HLgvExp.Style.Add("width", "50px");
                    lgvperonPro.Style.Add("width", "50px");
                    lgvusoamt.Style.Add("width", "50px");

                    lgvrebgdamt.Style.Add("width", "50px");

                    lgvrescramt.Style.Add("width", "50px");
                    lgvresdramt.Style.Add("width", "50px");



                }


                else
                {
                    lgvtosales.Style.Add("width", "90px");
                    lgvsales.Style.Add("width", "90px");
                    lgvcoll.Style.Add("width", "90px");
                    lgvperonsale.Style.Add("width", "90px");
                    //lblgvconBgdAmt.Style.Add("width", "90px");

                    HLgvacTRAmt.Style.Add("width", "90px");
                    HLgvacCCAmt.Style.Add("width", "90px");


                    HLgvacBgdAmt.Style.Add("width", "90px");
                    HLgvExp.Style.Add("width", "90px");
                    lgvperonPro.Style.Add("width", "90px");
                    lgvusoamt.Style.Add("width", "90px");

                    lgvrebgdamt.Style.Add("width", "90px");

                    lgvrescramt.Style.Add("width", "90px");
                    lgvresdramt.Style.Add("width", "90px");
                    //lgvRemCol.Style.Add("width", "80px");

                }

                //GridViewRow gvRow = e.Row;
                //if (gvRow.RowType == DataControlRowType.Header)
                //{
                //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //    TableCell cell01 = new TableCell();
                //    cell01.Text = "Project Information";
                //    cell01.HorizontalAlign = HorizontalAlign.Center;
                //    cell01.ColumnSpan = 4;


                //    TableCell cell02 = new TableCell();
                //    cell02.Text = "Revenue";
                //    cell02.HorizontalAlign = HorizontalAlign.Center;
                //    cell02.ColumnSpan = 5;


                //    TableCell cell03 = new TableCell();
                //    cell03.Text = "Cost";
                //    cell03.HorizontalAlign = HorizontalAlign.Center;
                //    cell03.ColumnSpan = 4;


                //    TableCell cell04 = new TableCell();
                //    cell04.Text = "Remaining";
                //    cell04.HorizontalAlign = HorizontalAlign.Center;
                //    cell04.ColumnSpan = 2;

                //    TableCell cell05 = new TableCell();
                //    cell05.Text = "Result";
                //    cell05.HorizontalAlign = HorizontalAlign.Center;
                //    cell05.ColumnSpan = 3;


                //    gvrow.Cells.Add(cell01);
                //    gvrow.Cells.Add(cell02);
                //    gvrow.Cells.Add(cell03);
                //    gvrow.Cells.Add(cell04);
                //    gvrow.Cells.Add(cell05);
                //    gvInPlan.Controls[0].Controls.AddAt(0, gvrow);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lgvFToSalVal = (Label)e.Row.FindControl("lgvFToSalValip");
                Label lgvFSaleAmt = (Label)e.Row.FindControl("lgvFSaleAmtip");
                Label lgvFCollAmt = (Label)e.Row.FindControl("lgvFCollAmtip");
                Label lgvFintamt = (Label)e.Row.FindControl("lgvFintamtip");
                Label lgvFBgdAmt = (Label)e.Row.FindControl("lgvFBgdAmtip");
                Label lgvFacBgdAmt = (Label)e.Row.FindControl("lgvFacBgdAmtip");
                Label lgvFExpAmt = (Label)e.Row.FindControl("lgvFExpAmtip");
                Label lgvFusoamt = (Label)e.Row.FindControl("lgvFusoamtip");
                Label lgvFRbgdAmt = (Label)e.Row.FindControl("lgvFRbgdAmtip");
                Label lgvFperonPro = (Label)e.Row.FindControl("lgvFperonProip");
                Label lgvFstinvamt = (Label)e.Row.FindControl("lgvFstinvamtip");
                Label lgvFresdramt = (Label)e.Row.FindControl("lgvFresdramtip");
                Label lgvFrescramt = (Label)e.Row.FindControl("lgvFrescramtip");
                //Label lgvFrColAmt = ((Label)e.Row.FindControl("lgvFrColAmt"));
                //((Label)this.gvColvsExp.FooterRow.FindControl("lgvFSaleAmt")).Text =

                if (this.chkCrore.Checked)
                {
                    lgvFToSalVal.Style.Add("width", "50px");
                    lgvFSaleAmt.Style.Add("width", "50px");
                    lgvFCollAmt.Style.Add("width", "50px");
                    lgvFintamt.Style.Add("width", "50px");
                    lgvFBgdAmt.Style.Add("width", "50px");
                    lgvFacBgdAmt.Style.Add("width", "50px");
                    lgvFExpAmt.Style.Add("width", "50px");
                    lgvFRbgdAmt.Style.Add("width", "50px");
                    lgvFusoamt.Style.Add("width", "50px");
                    lgvFperonPro.Style.Add("width", "50px");
                    lgvFstinvamt.Style.Add("width", "50px");
                    lgvFresdramt.Style.Add("width", "50px");
                    lgvFrescramt.Style.Add("width", "50px"); ;
                    //lgvFrColAmt.Style.Add("width", "45px");

                }


                else
                {

                    lgvFToSalVal.Style.Add("width", "90px");
                    lgvFSaleAmt.Style.Add("width", "90px");
                    lgvFCollAmt.Style.Add("width", "90px");
                    lgvFintamt.Style.Add("width", "90px");
                    lgvFBgdAmt.Style.Add("width", "90px");
                    lgvFacBgdAmt.Style.Add("width", "90px");
                    lgvFExpAmt.Style.Add("width", "90px");
                    lgvFRbgdAmt.Style.Add("width", "90px");
                    lgvFusoamt.Style.Add("width", "90px");

                    lgvFperonPro.Style.Add("width", "90px");

                    // lgvFstinvamt.Style.Add("width","90px");
                    lgvFrescramt.Style.Add("width", "90px");
                    // lgvFrColAmt.Style.Add("width", "80px");

                }
            }


        }



        protected void chkCrore_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void chkCore_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void gv01_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                cell01.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:BlueViolet; color:white; ";




                TableCell cell04 = new TableCell();
                cell04.Text = "Sales";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;
                cell04.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:BlueViolet; color:white;";



                TableCell cell05 = new TableCell();
                cell05.Text = "Cost";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 3;
                cell05.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:Gray; color:white;";
                TableCell cell06 = new TableCell();
                cell06.Text = "Fund";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 2;
                cell06.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:#993366; color:white; ";

                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);


                gv01.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void gv01_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void lbtnProjImg_OnClick(object sender, EventArgs e)
        {


            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                DataTable dt = (DataTable)Session["forActcode"];

                int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string actcode = dt.Rows[Rowindex]["actcode"].ToString();
                string comcod = this.GetComeCode();


                // ViewState.Remove ("tblimages");
                DataSet ds1 = MISData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJIMG", actcode, "", "", "", "", "", "",
                    "", "");

                // ViewState["tblimages"] = ds1.Tables[9];
                ListViewEmpAll.DataSource = ds1.Tables[0];
                ListViewEmpAll.DataBind();
                ds1.Dispose();

            }

            catch (Exception ex)
            {

            }


        }



        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;


                }

            }

        }

        protected void lbtnexecution_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadProgModal();", true);
                DataTable dt = (DataTable)Session["forActcode"];

                int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string actcode = dt.Rows[Rowindex]["actcode"].ToString();
                string comcod = this.GetComeCode();


                // ViewState.Remove ("tblimages");
                DataSet ds1 = MISData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROGRESSIMG", actcode, "", "", "", "", "", "",
                    "", "");

                // ViewState["tblimages"] = ds1.Tables[9];
                ListProgImage.DataSource = ds1.Tables[0];
                ListProgImage.DataBind();
                ds1.Dispose();

            }

            catch (Exception ex)
            {

            }
        }

        protected void ListProgImage_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;


                }

            }
        }

        protected void lbtnok_OnClick(object sender, EventArgs e)
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetComeCode();
            string txtdate = Convert.ToDateTime(this.txtDateb.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTBGDVSEXPENSES", txtdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgdVsActual.DataSource = null;
                this.gvBgdVsActual.DataBind();
                return;
            }


            Session["tblmasterbgd"] = ds1.Tables[0];
            this.Data_Bind();
        }
        protected void gvComCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvRptRes1 = (Label)e.Row.FindControl("lgvActDescCost");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lblgvRptAmt1 = (Label)e.Row.FindControl("lgvtopamtcost");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }

                string level = this.ddlReportLevelCom.SelectedValue.ToString();

                if (ASTUtility.Right(code, 3) == "000" && level == "12")
                {

                    lblgvRptRes1.Font.Bold = true;
                    lblgvRptAmt1.Font.Bold = true;
                    e.Row.Attributes["style"] = " background-color:green; color:white;font-size:12px; font-weight:bold;";
                    //lblgvRptAmt1.Attributes["style"] = "background-color:green;font-size:16px; font-weight:bold;";
                    //lblgvPer.Attributes["style"] = "background-color:green;font-size:16px; font-weight:bold;";
                }


            }
        }
        protected void lbtnPrjDetails_Click(object sender, EventArgs e)
        {

            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            DataTable dt = (DataTable)Session["tblmasterbgd"];
            string actcode = dt.Rows[Rowindex]["actcode"].ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "actcode='" + actcode + "'";
            dt = dv.ToTable();
            Session["tblmasterbgdpop"] = dt;
            this.PopUpData();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadPrjModal();", true);

            DataTable dt2 = (DataTable)Session["tblmasterbgdpop"];
            string comcod = this.GetComeCode();
            string date = this.txtDateInPlan.Text.Trim();
            string prjcode = dt.Rows[0]["actcode"].ToString();
            this.prjname.NavigateUrl = "~/F_32_Mis/LinkIndProjectStatus.aspx?prjcode=" + prjcode + "&date=" + date;
            this.bdgconcost.NavigateUrl = "~/F_32_Mis/RptConstruProgressSum.aspx?Type=Report&comcod=" + comcod;
            this.bgdtcost.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk&comcod=" + comcod + "&prjcode=" + prjcode;
            this.actcost.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal&prjcode=" + prjcode;

            double remrev = Convert.ToDouble(dt2.Rows[0]["usoamt"]);
            double remcost = Convert.ToDouble(dt2.Rows[0]["rbgdamt"]);
            double finance = remrev - remcost;
            if (finance >= 0)
            {
                this.finance.Attributes["style"] = "color:green";
            }
            else
            {
                this.finance.Attributes["style"] = "color:red";
            }
            //data-toggle="modal" data-target="#prjModalDt" data-dismiss="modal"

        }

        private void PopUpData()
        {
            DataTable dt = (DataTable)Session["tblmasterbgdpop"];
            double remrev = Convert.ToDouble(dt.Rows[0]["usoamt"]);
            double remcost = Convert.ToDouble(dt.Rows[0]["rbgdamt"]);
            double finance = remrev - (remcost > 0 ? remcost : (remcost * -1));

            this.prjname.Text = dt.Rows[0]["actdesc"].ToString();
            this.location.Text = dt.Rows[0]["location"].ToString();
            this.nature.Text = dt.Rows[0]["natland"].ToString();
            this.larea.Text = Convert.ToDouble(dt.Rows[0]["lanarea"]).ToString("#,##0.00;(#,##0.00); ");
            this.sdate.Text = Convert.ToDateTime(dt.Rows[0]["cdate"]).Year == 1900 ? "" : Convert.ToDateTime(dt.Rows[0]["cdate"]).ToString("dd-MMM-yyyy");
            this.trevenue.Text = Convert.ToDouble(dt.Rows[0]["tosalval"]).ToString("#,##0.00;(#,##0.00); ");
            this.sales.Text = Convert.ToDouble(dt.Rows[0]["salamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.collection.Text = Convert.ToDouble(dt.Rows[0]["collamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.salper.Text = Convert.ToDouble(dt.Rows[0]["perontosal"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.collper.Text = Convert.ToDouble(dt.Rows[0]["perontocol"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.bdgconcost.Text = Convert.ToDouble(dt.Rows[0]["cbgdamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.bgdtcost.Text = Convert.ToDouble(dt.Rows[0]["acbgdamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.actcost.Text = Convert.ToDouble(dt.Rows[0]["examt"]).ToString("#,##0.00;(#,##0.00); ");
            this.costper.Text = Convert.ToDouble(dt.Rows[0]["peronpgres"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.conspper.Text = Convert.ToDouble(dt.Rows[0]["peroncpro"]).ToString("#,##0.00;(#,##0.00); " + "%");
            this.remrev.Text = Convert.ToDouble(dt.Rows[0]["usoamt"]).ToString("#,##0.00;(#,##0.00); ");

            this.remcost.Text = Convert.ToDouble(dt.Rows[0]["rbgdamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.finance.Text = finance.ToString("#,##0.00;(#,##0.00); ");
            this.bgdmargin.Text = Convert.ToDouble(dt.Rows[0]["bgdmar"]).ToString("#,##0.00;(#,##0.00); ");
            this.actmargin.Text = Convert.ToDouble(dt.Rows[0]["actmar"]).ToString("#,##0.00;(#,##0.00); ");
            this.fblock.Text = Convert.ToDouble(dt.Rows[0]["invblkamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.fgen.Text = Convert.ToDouble(dt.Rows[0]["invreqamt"]).ToString("#,##0.00;(#,##0.00); ");

        }

    }
}