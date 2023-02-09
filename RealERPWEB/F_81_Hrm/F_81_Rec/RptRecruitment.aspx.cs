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
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class RptRecruitment : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string Type = this.Request.QueryString["Type"].ToString().Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Type == "JobAdvertise") ? "JOB ADVERTISEMENT INFORMATION"
                //    : (Type == "SortListing") ? "Short Listing Process" : (Type == "InterviewResult") ? "Interview Result" : (Type == "FinalSelect") ? "Final Selection" : "";


                this.GetDate();
                this.ViewSection();
            }

        }

        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
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

        private void ViewSection()
        {


            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "JobAdvertise":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "SortListing":
                case "InterviewResult":
                case "FinalSelect":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }



        }


        protected void ibtnEmpList_Click(object sender, ImageClickEventArgs e)
        {
            //this.GetEmployeeName();
        }
        protected void ibtnInformation_Click(object sender, ImageClickEventArgs e)
        {
            //this.GetInformation();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "JobAdvertise":
                    this.PrintJobAdvertise();
                    break;

                case "SortListing":
                    this.PrintSortList();
                    break;


                case "InterviewResult":
                    this.PrintInterviewResult();
                    break;



                case "FinalSelect":
                    this.PrintFinalSelect();
                    break;


            }









        }
        private void PrintJobAdvertise()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt1 = (DataTable)Session["tblrec"];
            ReportDocument rrs1 = new RealERPRPT.R_81_Hrm.R_81_Rec.RptJobAdvetise();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtCompAddress = rrs1.ReportDefinition.ReportObjects["txtCompAddress"] as TextObject;
            txtCompAddress.Text = comadd;
            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "From " + fromdate + " To " + todate;
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);

            Session["Report1"] = rrs1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintSortList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            string advno = this.ddlAdvList.SelectedValue.ToString();
            DataRow[] dr1 = (((DataTable)Session["tbladvno"]).Select("advno='" + advno + "'"));
            DataTable dt1 = (DataTable)Session["tblrec"];
            ReportDocument rrs1 = new RealERPRPT.R_81_Hrm.R_81_Rec.RptSortList();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = dr1[0]["company"].ToString();

            TextObject txtCompAddress = rrs1.ReportDefinition.ReportObjects["txtCompAddress"] as TextObject;
            txtCompAddress.Text = comadd;
            TextObject txtadvno = rrs1.ReportDefinition.ReportObjects["txtadvno"] as TextObject;
            txtadvno.Text = "Adv. No: " + dr1[0]["advno02"].ToString();
            TextObject txtrefno = rrs1.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
            txtrefno.Text = "Ref. No: " + dr1[0]["refno"].ToString();
            TextObject txtDate = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + dr1[0]["advdat"].ToString();

            //DataTable dt2 = (DataTable)ViewState["tblreqdesc"];
            //int j = 1;
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{



            //    TextObject rpttxth = rrs1.ReportDefinition.ReportObjects["txtcol" + j.ToString()] as TextObject;
            //        rpttxth.Text = dt2.Rows[i]["reqdesc"].ToString();
            //        j++;
            //        if (j == 8)
            //            break;
            //    }
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);
            rrs1.Subreports["RptInterViewer.rpt"].SetDataSource((DataTable)ViewState["tblInterv"]);
            Session["Report1"] = rrs1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintInterviewResult()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string advno = this.ddlAdvList.SelectedValue.ToString();
            DataRow[] dr1 = (((DataTable)Session["tbladvno"]).Select("advno='" + advno + "'"));
            DataTable dt1 = (DataTable)Session["tblrec"];
            ReportDocument rrs1 = new RealERPRPT.R_81_Hrm.R_81_Rec.RptInterviewResult();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = dr1[0]["company"].ToString();

            TextObject txtCompAddress = rrs1.ReportDefinition.ReportObjects["txtCompAddress"] as TextObject;
            txtCompAddress.Text = comadd;
            TextObject txtadvno = rrs1.ReportDefinition.ReportObjects["txtadvno"] as TextObject;
            txtadvno.Text = "Adv. No: " + dr1[0]["advno02"].ToString();
            TextObject txtrefno = rrs1.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
            txtrefno.Text = "Ref. No: " + dr1[0]["refno"].ToString();
            TextObject txtDate = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + dr1[0]["advdat"].ToString();

            //DataTable dt2 = (DataTable)ViewState["tblreqdesc"];
            //int j = 1;
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{



            //    TextObject rpttxth = rrs1.ReportDefinition.ReportObjects["txtcol" + j.ToString()] as TextObject;
            //    rpttxth.Text = dt2.Rows[i]["reqdesc"].ToString();
            //    j++;
            //    if (j == 8)
            //        break;
            //}
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);
            rrs1.Subreports["RptInterViewer.rpt"].SetDataSource((DataTable)ViewState["tblInterv"]);
            Session["Report1"] = rrs1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintFinalSelect()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string advno = this.ddlAdvList.SelectedValue.ToString();
            DataRow[] dr1 = (((DataTable)Session["tbladvno"]).Select("advno='" + advno + "'"));
            DataTable dt1 = (DataTable)Session["tblrec"];
            ReportDocument rrs1 = new RealERPRPT.R_81_Hrm.R_81_Rec.RptFinalSelection();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = dr1[0]["company"].ToString();

            TextObject txtCompAddress = rrs1.ReportDefinition.ReportObjects["txtCompAddress"] as TextObject;
            txtCompAddress.Text = comadd;
            TextObject txtadvno = rrs1.ReportDefinition.ReportObjects["txtadvno"] as TextObject;
            txtadvno.Text = "Adv. No: " + dr1[0]["advno02"].ToString();
            TextObject txtrefno = rrs1.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
            txtrefno.Text = "Ref. No: " + dr1[0]["refno"].ToString();
            TextObject txtDate = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + dr1[0]["advdat"].ToString();

            //DataTable dt2 = (DataTable)ViewState["tblreqdesc"];
            //int j = 1;
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{



            //    TextObject rpttxth = rrs1.ReportDefinition.ReportObjects["txtcol" + j.ToString()] as TextObject;
            //    rpttxth.Text = dt2.Rows[i]["reqdesc"].ToString();
            //    j++;
            //    if (j == 8)
            //        break;
            //}
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);
            rrs1.Subreports["RptInterViewer.rpt"].SetDataSource((DataTable)ViewState["tblInterv"]);
            Session["Report1"] = rrs1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "JobAdvertise":
                    this.ShowJObAdvertise();
                    break;

                case "SortListing":
                case "InterviewResult":
                case "FinalSelect":
                    this.ShowSortList();
                    break;
            }
        }


        private void ShowJObAdvertise()
        {
            Session.Remove("tblrec");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_REPORT_RECRUITMENT", "RPTJOBADVERTISEMENT", frmdate, todate, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblrec"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        protected void ShowSortList()
        {

            this.lblInterViewer.Visible = true;
            string comcod = this.GetCompCode();
            string mADVNO = this.ddlAdvList.SelectedValue.ToString();
            string mPOST = this.ddlPOSTList.SelectedValue.ToString();
            string Type = (this.Request.QueryString["Type"].ToString().Trim() == "FinalSelect") ? "FinalSelect" : "";

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_RECRUITMENT", "SHOWSORTLIST", mADVNO, mPOST,
                      Type, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblrec"] = ds1.Tables[0];
            ViewState["tblInterv"] = ds1.Tables[1];
            ViewState["tblreqdesc"] = ds1.Tables[2];
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblrec"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "JobAdvertise":
                    this.gvappinfo.DataSource = dt;
                    this.gvappinfo.DataBind();
                    break;

                case "SortListing":
                case "InterviewResult":
                case "FinalSelect":
                    DataTable dtname = (DataTable)ViewState["tblreqdesc"];
                    int j = 3;
                    for (int i = 0; i < dtname.Rows.Count; i++)
                    {

                        this.gvSListInfo.Columns[j].HeaderText = dtname.Rows[i]["reqdesc"].ToString();
                        j++;
                        if (j == 21)
                            break;
                    }


                    this.gvSListInfo.DataSource = dt;
                    this.gvSListInfo.DataBind();

                    this.gvIntInfo.DataSource = (DataTable)ViewState["tblInterv"]; ;
                    this.gvIntInfo.DataBind();


                    break;
            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "JobAdvertise":
                    string advno = dt1.Rows[0]["advno"].ToString();
                    string deptcode = dt1.Rows[0]["deptcode"].ToString();
                    string postcode = dt1.Rows[0]["postcode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["advno"].ToString() == advno && dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[j]["postcode"].ToString() == postcode)
                        {


                            dt1.Rows[j]["advno1"] = "";
                            dt1.Rows[j]["advdat1"] = "";
                            dt1.Rows[j]["refno"] = "";

                            dt1.Rows[j]["company"] = "";
                            dt1.Rows[j]["deptdesc"] = "";
                            dt1.Rows[j]["postdesc"] = "";
                            dt1.Rows[j]["jobsource"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["advno"].ToString() == advno)
                            {
                                dt1.Rows[j]["advno1"] = "";
                                dt1.Rows[j]["advdat1"] = "";
                                dt1.Rows[j]["refno"] = "";
                                dt1.Rows[j]["company"] = "";
                                dt1.Rows[j]["jobsource"] = "";
                            }

                            if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                            {
                                dt1.Rows[j]["deptdesc"] = "";

                            }
                            if (dt1.Rows[j]["postcode"].ToString() == postcode)
                            {
                                dt1.Rows[j]["postdesc"] = "";

                            }
                            advno = dt1.Rows[j]["advno"].ToString();
                            deptcode = dt1.Rows[j]["deptcode"].ToString();
                            postcode = dt1.Rows[j]["postcode"].ToString();
                        }
                    }

                    break;

                case "SortListing":
                case "InterviewResult":
                case "FinalSelect":

                    break;
            }





            return dt1;
        }

        private void GetAdvertise()
        {
            Session.Remove("tbladvno");
            string comcod = this.GetCompCode();
            string refno = "%" + this.txtSrchPre.Text.Trim() + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_RECRUITMENT", "GETADVERTISENO", frmdate, todate,
                          refno, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlAdvList.Items.Clear();
            this.ddlAdvList.DataTextField = "advno1";
            this.ddlAdvList.DataValueField = "advno";
            this.ddlAdvList.DataSource = ds1.Tables[0];
            this.ddlAdvList.DataBind();
            Session["tbladvno"] = ds1.Tables[0];
            ds1.Dispose();
            this.GetPostName();

        }
        private void GetPostName()

        {
            string comcod = this.GetCompCode();
            string Advno = this.ddlAdvList.SelectedValue.ToString();
            string mSrchTxt = this.txtPostSearch.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_RECRUITMENT", "GETADVPOST", Advno, mSrchTxt, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPOSTList.DataTextField = "postdesc";
            this.ddlPOSTList.DataValueField = "postcode";
            this.ddlPOSTList.DataSource = ds1.Tables[0];
            this.ddlPOSTList.DataBind();

        }
        protected void ddlAdvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetPostName();

        }
        protected void ImgbtnFindAdv_Click(object sender, EventArgs e)
        {
            this.GetAdvertise();
        }
        protected void ImgbtnFindPost_Click(object sender, EventArgs e)
        {
            this.GetPostName();

        }
        protected void gvSListInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void gvIntInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

    }
}