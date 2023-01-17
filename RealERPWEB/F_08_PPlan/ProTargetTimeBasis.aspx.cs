using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Net.Mail;
using RealERPLIB;
using RealERPRPT;
using AjaxControlToolkit;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_08_PPlan
{
    public partial class ProTargetTimeBasis : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
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

                string type = this.Request.QueryString["Type"].ToString();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = type == "Entry" ? "Construction Planning -Time Base " : "Construction Planning - Time Basis(Management)";

                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.GetProjectName();
                    this.GetFloor();
                    this.Sectype();
                    this.lbtnOk_Click(null, null);
                    this.ddlgroupwise_SelectedIndexChanged(null, null);

                }

                this.GetProjectName();
                this.GetFloor();
                this.Sectype();
            }



        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void Sectype()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {

                case "GrpWise":
                    this.lblgrpwise.Visible = true;
                    this.ddlgroupwise.Visible = true;
                    //this.lblfloorno.Visible = false;
                    //this.ddlfloorno.Visible = false;
                    this.GetGroupCode();
                    break;

                default:
                    this.lblgrpwise.Visible = false;
                    this.ddlgroupwise.Visible = false;
                    break;


            }






        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            //  string srchTxt =this.txtProjectSearch.Text.Trim() + "%";
            string srchTxt = (this.Request.QueryString["prjcode"].ToString().Length > 0 ? this.Request.QueryString["prjcode"].ToString() : ("%" + this.txtProjectSearch.Text.Trim())) + "%";

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "GETPROJETNAME", srchTxt, "", "", "", "", "", "", "", "");
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            this.ddlProject.SelectedValue = this.Request.QueryString["prjcode"];
            ds1.Dispose();
        }

        private void GetFloor()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString().Trim();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEFLRLIST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            string type = this.Request.QueryString["Type"].ToString();
            DataTable dt = ds1.Tables[0];
            if (type == "GrpWise")
            {
                DataRow dr1 = dt.NewRow();
                dr1["flrdes"] = "All Floor";
                dr1["flrcod"] = "000";
                dt.Rows.Add(dr1);

            }

            this.ddlfloorno.DataTextField = "flrdes";
            this.ddlfloorno.DataValueField = "flrcod";
            this.ddlfloorno.DataSource = ds1.Tables[0];
            this.ddlfloorno.DataBind();
            this.ddlfloorno.SelectedValue = "000";
            if (this.Request.QueryString["flrcod"].ToString().Length > 0)
                this.ddlfloorno.SelectedValue = (this.Request.QueryString["flrcod"]);


        }
        private void GetGroupCode()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString().Trim();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETGROUPCODE", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlgroupwise.DataTextField = "isirdesc";
            this.ddlgroupwise.DataValueField = "isircode";
            this.ddlgroupwise.DataSource = ds1.Tables[0];
            this.ddlgroupwise.DataBind();
            if (this.Request.QueryString["sircode"].Trim().Length > 0)
                this.ddlgroupwise.SelectedValue = (this.Request.QueryString["sircode"]);


        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
                this.ddlProject.Visible = false;
                this.lblProjectDesc.Visible = true;
                this.PnlColoumn.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.ShowPtarget();

                return;
            }

            this.lbtnOk.Text = "Ok";
            this.ddlProject.Visible = true;
            this.lblProjectDesc.Visible = false;
            this.PnlColoumn.Visible = false;
            this.lblStartDate.Text = "";
            this.lblEndDate.Text = "";
            this.lblDuration.Text = "";
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvProTarget.DataSource = null;
            this.gvProTarget.DataBind();


        }

        private void ShowPtarget()
        {


            Session.Remove("tblptarget");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string wwise = (this.chkWorkwise.Checked) ? "Workwise" : "";
            string srchmaterial = "%" + this.txtsrchmaterial.Text.Trim() + "%";
            string flrcode = this.ddlfloorno.SelectedValue.ToString();
            string groupcode = (this.ddlgroupwise.SelectedValue.ToString()=="0000"? "41%" : this.ddlgroupwise.SelectedValue.ToString()+"%");
            string type = (this.Request.QueryString["Type"] == "GrpWise") ? "GroupWise" : "";
            string CallType = (this.checkBalance.Checked) ? "PROBALTARGETTIMEBASIS" : "PROTARGETTIMEBASIS";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", CallType, pactcode, wwise, srchmaterial, flrcode, groupcode, type, "", "", "");
            if (ds1 == null)
            {
                this.gvProTarget.DataSource = null;
                this.gvProTarget.DataBind();
                return;
            }

            Session["tblptarget"] = (this.chkWorkwise.Checked) ? ds1.Tables[0] : this.HiddenSameDate(ds1.Tables[0]);

            this.lblStartDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjstrtdat"]).ToString("dd-MMM-yyyy");
            this.lblEndDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjenddat"]).ToString("dd-MMM-yyyy");
            this.lblDuration.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["duration"]).ToString("#,#0;(#,#0); ") + " Month";
            ds1.Dispose();
            this.Data_Bind();

        }


        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string flrcod = dt1.Rows[0]["flrcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["flrcod"].ToString() == flrcod)
                    dt1.Rows[j]["flrdes"] = "";
                flrcod = dt1.Rows[j]["flrcod"].ToString();

            }

            return dt1;

        }

        protected void lbtngvP_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }



        private void Data_Bind()
        {
            this.gvProTarget.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            DataTable dt = (DataTable)Session["tblptarget"];
            this.gvProTarget.DataSource = dt;
            this.gvProTarget.DataBind();

            this.FooterCalculation();


            //Session["Report1"] = gvProTarget;
            //((HyperLink)this.gvProTarget.HeaderRow.FindControl("hlbtntbCdataExcel01")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            //if (((DataTable)Session["tblptarget"]).Rows.Count >0) 
            //{
            //    //((LinkButton)this.gvProTarget.FooterRow.FindControl("lbtnAdddays")).Visible = (this.Request.QueryString["Type"] == "Mgt") ? true : false;
            //    ((HyperLink)this.gvProTarget.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            //}



        }
        private void SaveAutoGenerate()
        {
            DataTable dt = (DataTable)Session["tblptarget"];
            string flrcod = dt.Rows[0]["flrcod"].ToString();
            DateTime sDate = Convert.ToDateTime(this.lblStartDate.Text);
            DateTime eDate = Convert.ToDateTime(this.lblEndDate.Text);

            foreach (DataRow dr1 in dt.Rows)
            {

                dr1["startdate"] = sDate;
                dr1["enddate"] = eDate;


            }

            Session["tblptarget"] = dt;
            // Initialize
            //dt.Rows[0]["startdate"] = sDate;
            //dt.Rows[0]["enddate"] = sDate.AddMonths(1).AddDays(-1);
            //for (int i=1; i< dt.Rows.Count;i++) 
            //{
            //    if (sDate > eDate)
            //        break;

            //    if (dt.Rows[i]["flrcod"].ToString() == flrcod)
            //    {
            //        dt.Rows[i]["startdate"] = sDate;
            //        dt.Rows[i]["enddate"] = sDate.AddMonths(1).AddDays(-1);
            //    }
            //    else
            //    {
            //        sDate = sDate.AddMonths(1);
            //        dt.Rows[i]["startdate"] = sDate;
            //        dt.Rows[i]["enddate"] = sDate.AddMonths(1).AddDays(-1);

            //    }


            //    flrcod = dt.Rows[i]["flrcod"].ToString();


            //}


        }

        protected void SaveValue()
        {


            DataTable tbl1 = (DataTable)Session["tblptarget"];
            int rowindex;
            for (int i = 0; i < this.gvProTarget.Rows.Count; i++)
            {


                DateTime Startdate = ((TextBox)this.gvProTarget.Rows[i].FindControl("txtgvstartdate")).Text.Trim() == "" ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvProTarget.Rows[i].FindControl("txtgvstartdate")).Text.Trim());
                DateTime Enddate = ((TextBox)this.gvProTarget.Rows[i].FindControl("txtgvenddate")).Text.Trim() == "" ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvProTarget.Rows[i].FindControl("txtgvenddate")).Text.Trim());

                if (Startdate > Enddate)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('End Date must be greater than Start Date');", true);
                    return;
                }


                TimeSpan timespan = (Enddate - Startdate);
                int today = (Convert.ToInt16((timespan.TotalDays)) == 0) ? (Startdate == Convert.ToDateTime("01-Jan-1900")) ? 0 : 1 : Convert.ToInt16((timespan.TotalDays) + 1);
                rowindex = (this.gvProTarget.PageSize * this.gvProTarget.PageIndex) + i;


                tbl1.Rows[rowindex]["startdate"] = Startdate;
                tbl1.Rows[rowindex]["enddate"] = Enddate;
                tbl1.Rows[rowindex]["today"] = today;



            }
            Session["tblptarget"] = tbl1;
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblptarget"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?
                              0 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFProAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proamt)", "")) ?
                            0 : dt.Compute("sum(proamt)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblptarget"];


            var list = dt.DataTableToList<RealEntity.C_08_PPlan.BO_Class_Con.ProjectTargetAnalysis>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_08_PPlan.RptProTargetTimeBasis", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compname", comnam));
            rpt.SetParameters(new ReportParameter("projName", "Project Name : " + this.ddlProject.SelectedItem.Text.ToString()));
            rpt.SetParameters(new ReportParameter("txtTitle", "Construction Planning -Time Base"));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rptpur = new RealERPRPT.R_08_PPlan.RptProTargetTimeBasis();
            //TextObject rptCname = rptpur.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtproject = rptpur.ReportDefinition.ReportObjects["txtproject"] as TextObject;
            //txtproject.Text = "Project Name : " + this.ddlProject.SelectedItem.Text;

            //TextObject txtuserinfo = rptpur.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptpur.SetDataSource(dt);
            //Session["Report1"] = rptpur;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["tblptarget"];
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string usrid = hst["usrid"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string trmid = hst["compname"].ToString();
            string sessionid = hst["session"].ToString();

            bool result = false;

            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{

            foreach (DataRow dr1 in tbl1.Rows)
            {

                string Itemcode = dr1["isircode"].ToString();
                string flrcod = dr1["flrcod"].ToString();
                DateTime Startdate = Convert.ToDateTime(dr1["startdate"]);
                DateTime Enddate = Convert.ToDateTime(dr1["enddate"]);
                TimeSpan timespan = (Enddate - Startdate);
                double bgdqty = Convert.ToDouble(dr1["bgdqty"]);
                int today = (Convert.ToInt16((timespan.TotalDays)) == 0) ? (Startdate == Convert.ToDateTime("01-Jan-1900")) ? 0 : 1 : Convert.ToInt16((timespan.TotalDays) + 1);
                int itoday = 0;
                if (today >= 1)
                {
                    result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "INSERTORUPPTARGETB", pactcode,
                                 Itemcode, flrcod, Startdate.ToString("dd-MMM-yyyy"), Enddate.ToString("dd-MMM-yyyy"), today.ToString(), usrid, PostedDat, trmid, sessionid, "", "", "", "", "");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated Fail.');", true);
                        return;

                    }





                    int inidate = 0;
                    while (Startdate <= Enddate)
                    {


                        if (inidate == 0)
                        {
                            string startdate1 = Startdate.ToString("dd-MMM-yyyy");
                            DateTime ibegdate = Convert.ToDateTime("01" + startdate1.Substring(2));
                            DateTime ienddate = Convert.ToDateTime(ibegdate.AddMonths(1).AddDays(-1));


                            if (ienddate > Enddate)
                                ienddate = Enddate;
                            timespan = (ienddate - Startdate);
                            itoday = Convert.ToInt16(timespan.TotalDays) + 1;
                            inidate = 1;

                        }
                        else
                        {

                            DateTime enddate1 = Convert.ToDateTime(Startdate).AddMonths(1).AddDays(-1);
                            if (enddate1 > Enddate)
                                enddate1 = Enddate;
                            timespan = (enddate1 - Startdate);
                            itoday = Convert.ToInt16(timespan.TotalDays) + 1;


                        }


                        string yearmon = Startdate.ToString("yyyyMM");
                        double trqty = (bgdqty * itoday) / today;
                        if (trqty > 0)
                            result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "INSERTORUPPTARGET", pactcode,
                                   Itemcode, flrcod, yearmon, trqty.ToString(), "", "", "", "", "", "", "", "", "", "");
                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated Fail.');", true);

                        }

                        Startdate = Startdate.AddDays(itoday);

                    }




                }


            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "PROJECT COMPLETION INFORMATION";
                string eventdesc = "Update";
                string eventdesc2 = this.ddlProject.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void gvProTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvProTarget.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private void SavePutSameValue()
        {
            DataTable dt = (DataTable)Session["tblptarget"];
            string flrcod = dt.Rows[0]["flrcod"].ToString();


            DateTime sDate = ((TextBox)this.gvProTarget.Rows[0].FindControl("txtgvstartdate")).Text.Trim() == "" ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvProTarget.Rows[0].FindControl("txtgvstartdate")).Text.Trim());
            DateTime eDate = ((TextBox)this.gvProTarget.Rows[0].FindControl("txtgvenddate")).Text.Trim() == "" ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvProTarget.Rows[0].FindControl("txtgvenddate")).Text.Trim());



            int today = 0;
            TimeSpan timespan;

            foreach (DataRow dr1 in dt.Rows)
            {

                timespan = (eDate - sDate);
                today = (Convert.ToInt16((timespan.TotalDays)) == 0) ? (sDate == Convert.ToDateTime("01-Jan-1900")) ? 0 : 1 : Convert.ToInt16((timespan.TotalDays) + 1);
                dr1["startdate"] = sDate;
                dr1["enddate"] = eDate;
                dr1["today"] = today;



            }

            Session["tblptarget"] = dt;
            // Initialize
            //dt.Rows[0]["startdate"] = sDate;
            //dt.Rows[0]["enddate"] = sDate.AddMonths(1).AddDays(-1);
            //for (int i=1; i< dt.Rows.Count;i++) 
            //{
            //    if (sDate > eDate)
            //        break;

            //    if (dt.Rows[i]["flrcod"].ToString() == flrcod)
            //    {
            //        dt.Rows[i]["startdate"] = sDate;
            //        dt.Rows[i]["enddate"] = sDate.AddMonths(1).AddDays(-1);
            //    }
            //    else
            //    {
            //        sDate = sDate.AddMonths(1);
            //        dt.Rows[i]["startdate"] = sDate;
            //        dt.Rows[i]["enddate"] = sDate.AddMonths(1).AddDays(-1);

            //    }


            //    flrcod = dt.Rows[i]["flrcod"].ToString();


            //}


        }

        private void AddDelaysDays()
        {

            DataTable dt = (DataTable)Session["tblptarget"];
            int adddays = Convert.ToInt32("0" + ((TextBox)this.gvProTarget.FooterRow.FindControl("txtadddays")).Text.Trim());

            if (this.rbtndelayaext.SelectedIndex == 0)
            {
                foreach (DataRow dr1 in dt.Rows)
                {

                    dr1["startdate"] = Convert.ToDateTime(dr1["startdate"]).AddDays(adddays);
                    dr1["enddate"] = Convert.ToDateTime(dr1["enddate"]).AddDays(adddays);
                }

            }

            else
            {
                foreach (DataRow dr1 in dt.Rows)
                {

                    //  dr1["startdate"] = Convert.ToDateTime(dr1["startdate"]).AddDays(adddays);
                    dr1["enddate"] = Convert.ToDateTime(dr1["enddate"]).AddDays(adddays);
                }


            }

            Session["tblptarget"] = dt;

        }
        protected void lbtnAdddays_Click(object sender, EventArgs e)
        {

            this.AddDelaysDays();
            this.Data_Bind();

        }

        protected void lbtnPutSameValue_Click(object sender, EventArgs e)
        {
            this.SavePutSameValue();
            this.Data_Bind();

        }

        protected void chkWorkwise_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowPtarget();
        }
        protected void lnksrchmaterial_Click(object sender, EventArgs e)
        {
            this.ShowPtarget();
        }
        protected void ddlfloorno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPtarget();
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetFloor();
            this.GetGroupCode();
        }

        protected void ddlgroupwise_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPtarget();
        }
        //protected void gvProTarget_RowDataBound(object sender, GridViewRowEventArgs e)
        //{


        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {



        //        TextBox txtgvstartdate = (TextBox)e.Row.FindControl("txtgvstartdate");
        //        TextBox txtgvenddate = (TextBox)e.Row.FindControl("txtgvenddate");
        //        CalendarExtender CalendarExtender_txtgvstartdate = new AjaxControlToolkit.CalendarExtender();
        //        CalendarExtender_txtgvstartdate.Format = "dd-MMM-yyyy";
        //        CalendarExtender_txtgvstartdate.TargetControlID = txtgvstartdate.ClientID;


        //        CalendarExtender CalendarExtender_txtgvenddate = new AjaxControlToolkit.CalendarExtender();
        //        CalendarExtender_txtgvenddate.Format = "dd-MMM-yyyy";
        //        CalendarExtender_txtgvenddate.TargetControlID = txtgvenddate.ClientID;

        //    //    this.Controls.Add(CalendarExtender_txtgvstartdate);
        //     //   this.Controls.Add(CalendarExtender_txtgvenddate);






        //    }
        //}
        protected void checkBalance_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowPtarget();
        }


    }
}