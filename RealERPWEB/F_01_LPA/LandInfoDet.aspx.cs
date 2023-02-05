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
using System.IO;
//using Microsoft.Office.Interop.Excel;
using RealERPLIB;
using RealERPRPT;
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;
using AjaxControlToolkit;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Newtonsoft.Json;
using ListBox = System.Web.UI.WebControls.ListBox;

namespace RealERPWEB.F_01_LPA
{
    public partial class LandInfoDet : System.Web.UI.Page
    {
        int count = 0;
        ProcessAccess HRData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        public static string landid;
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");


                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtkpitodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy"); ;


                ((Label)this.Master.FindControl("lblTitle")).Text = "Land/Owner Information";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;



            }
        }

        private void autoClickBtn_tempBTN()
        {
            this.GetInformation();
            GetAllSubdata();
            this.DataBindStatus();
            GetYEARLAND();
            this.GETEMPLOYEEUNDERSUPERVISED();
            this.GetGridSummary();
            this.ModalDataBind();
            // this.GetNotificationinfo();
            divexland.Visible = false;
            divddlinfo.Visible = false;
            divLaOw.Visible = false;
            this.CreateTable();
            this.ShowDiscussion();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Click Land Interface (Land CRM)";
                string eventdesc = "Click Land Interface (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (this.hdnlblattribute.Value.Trim() == "")
            {
                this.GetGridSummary();
                this.ModalDataBind();
                //  this.GetNotificationinfo();
            }
            else
            {
                this.EmpMonthlyKPI();


            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Information (Land CRM) ";
                string eventdesc = "Show Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }




        }

        private void CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("sircode", Type.GetType("System.String"));
            ViewState["newlandcode"] = dt;
            //test

        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetSchedulenumber(string comcod, string followupdate, string lastfollowup, string empid)
        {


            string sircode = "8305%";
            ProcessAccess _processAccess = new ProcessAccess();

            string gcodland = "gcodlan";
            DataSet ds2 = _processAccess.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SCHEDULENUMBER", sircode, empid, lastfollowup, followupdate, gcodland, "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Schedule:", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {
                var schedule = Convert.ToDouble(ds2.Tables[0].Rows[0]["schnumber"]).ToString("#,##0;(#,##0);");
                var schdesc = (ds2.Tables[0].Rows[0]["schdesc"]);
                var result = new { Message = "Schedule:" + schdesc + "(" + schedule + ")", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;


            }




        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ShowDetNotification(string comcod, string empid, string rtype, string date)
        {
            empid = empid + "%";
            ProcessAccess JData = new ProcessAccess();
            DataSet ds1 = JData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETNOTIFICATIONDETAILS", "8305%", empid, rtype, date, "");

            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.LandNotification>();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);
            return json;



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string usertype = hst["usrrmrk"].ToString();
            //string comcod = this.GetComeCode();
            //string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            //string rtype = this.Request.QueryString["genno"].ToString();
            //string date = this.txtdate.Text.Trim();

            //DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETNOTIFICATIONDETAILS", "8305%", Empid, rtype, date, "");

            //if (ds1 == null)
            //{
            //    return;
            //}

            //this.gvinformation.DataSource = ds1.Tables[0];
            //this.gvinformation.DataBind();

            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }



        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UpdatePost(string comcod, string userid, string proscod, string date, string post, string comdate)
        {

            string gcod = "810100102015";
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            ProcessAccess JData = new ProcessAccess();

            bool result = JData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATECOMMENTS", null, null, null, proscod, gcod, date, post, userid, comdate, Posteddat, "", "", "", "", "", "", "", "", "", "", "");


            //bool result = JData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATECOMMENTS", null, null, null, proscod, gcod, date, post, userid, Posteddat, "", "", "", "", "", "", "", "", "", "", "", "");
            // bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATE_COMM", empid, Client, "000000000000", "", "000000000000", cdate, gcod, "T", Gvalue, Comments, userid, Posteddat);


            if (!result)
            {

                var lst = new { Message = JData.ErrorObject["Msg"].ToString(), result = false };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }

            else
            {

                var lst = new { Message = "Update successfully.", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }



            //var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.LandNotification>();






            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string usertype = hst["usrrmrk"].ToString();
            //string comcod = this.GetComeCode();
            //string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            //string rtype = this.Request.QueryString["genno"].ToString();
            //string date = this.txtdate.Text.Trim();

            //DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETNOTIFICATIONDETAILS", "8305%", Empid, rtype, date, "");

            //if (ds1 == null)
            //{
            //    return;
            //}

            //this.gvinformation.DataSource = ds1.Tables[0];
            //this.gvinformation.DataBind();

            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }




        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CheckPlotNo(string comcod, string landinfo)
        {


            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "CHECKDUPLANDINFO", landinfo, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Success", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;
            }


            else
            {

                string empname = ds2.Tables[0].Rows[0]["empname"].ToString();
                string lid = ds2.Tables[0].Rows[0]["sircode1"].ToString();
                string holdername = " His/Her Name " + empname + ",  Land Id:" + lid;
                string Message = "Found Duplicate Land Info. ";
                string totmsg = Message + holdername;
                var result = new { Message = totmsg, result = false };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UpdateStatus(string comcod, string proscod, string statusid, string empid)
        {

            string gcod = "810100102016";

            ProcessAccess JData = new ProcessAccess();

            bool result = JData.UpdateXmlTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "UPDATELASTEMPSTATUS", null, null, null, empid, proscod, gcod, statusid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            //bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATE_COMM", empid, Client, "000000000000", "", "000000000000", cdate, gcod, "T", Gvalue, Comments, userid, Posteddat);


            if (!result)
            {

                var lst = new { Message = JData.ErrorObject["Msg"].ToString(), result = false };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }

            else
            {

                var lst = new { Message = "Update successfully.", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }




        }





        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetReschedule(string comcod, string empid, string proscod, string cdate)
        {
            string kpigrp = "000000000000";
            string wrkdpt = "000000000000";
            ProcessAccess JData = new ProcessAccess();
            string reschedule = "reschedule";
            DataSet ds1 = JData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", empid, proscod, kpigrp, "", wrkdpt, cdate, reschedule, "", "", "");


            //   DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", Empid, Client, kpigrp, "", wrkdpt, cdate);

            if (ds1 == null)
            {
                //List<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum> lst5 = ds2.Tables[0].DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum>();
                var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EclassPreLandownerDiscuss>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }

            else
            {
                var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EclassPreLandownerDiscuss>().ToList();
                // var lst = new { Message = "Update successfully.", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }




        }


        private void GetNotificationinfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usertype = hst["usrrmrk"].ToString();
            string comcod = this.GetComeCode();
            string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            string ddlempid = (this.ddlEmpid.SelectedValue.ToString() == "000000000000" ? "93" : this.ddlEmpid.SelectedValue.ToString()) + "%";
            string date = this.txtdate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETNOTIFICATIONNUMBER", "8305%", Empid, ddlempid, date, "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            DataTable dt = ds1.Tables[0];
            Session["tblNotification"] = dt;
            ds1.Dispose();
            bindDataIntoLabel();

        }

        private void GetNotificationByEmployee(string ddlempid)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();

            string comcod = this.GetComeCode();
            //string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();

            string Empid = "";
            if (userrole != "1")
            {
                Empid = hst["empid"].ToString();
            }

            string date = this.txtdate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETNOTIFICATIONNUMBER", "8305%", Empid, ddlempid, date, "");

            if (ds1 == null)
            {
                return;
            }
            DataTable dt = ds1.Tables[0];
            Session["tblNotification"] = dt;
            ds1.Dispose();
            bindDataIntoLabel();


            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Add Land (Land CRM)";
                string eventdesc = "Add Land (Land CRM)";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }



        }


        private void bindDataIntoLabel()
        {
            DataTable dt = (DataTable)Session["tblNotification"];
            if (dt == null)
            {
                return;
            }
            this.lbldws.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["dws"].ToString();
            this.lbldwr.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["dwr"].ToString();
            this.lbloth.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["othact"].ToString();
            //this.cpro.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["pro"].ToString();
            this.lblDayPass.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["daypassed"].ToString();
            this.lblCall.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["call"].ToString();
            // this.lblvisit.InnerText = ds3.Tables[0].Rows[0]["visit"].ToString();
            this.lblLome.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["lome"].ToString();
            this.lblLomi.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["lomi"].ToString();
            //this.csurvey.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["survey"].ToString();
            this.lblComments.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["comments"].ToString();
            this.lblFreez.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["freezing"].ToString();
            this.lblDeadProspect.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["deadl"].ToString();
            this.lblcsigned.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["signed"].ToString();
            this.lblDatablank.InnerText = dt.Rows.Count == 0 ? "" : dt.Rows[0]["databank"].ToString();
            
            //lblDatablank

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        public string GetEmpID()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];

            string Empid = (hst["empid"].ToString() == "") ? "93" : hst["empid"].ToString();
            return (Empid);

        }

        public string GetUserID()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["usrid"].ToString());

        }


        private void GetYEARLAND()
        {
            ViewState.Remove("tblempname");
            string comcod = this.GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETLANDDDLYEARINFO", "", "", "", "", "", "", "", "", "");
            this.ddlyearland.DataTextField = "sirdesc";
            this.ddlyearland.DataValueField = "sircode";
            this.ddlyearland.DataSource = ds3.Tables[0];
            this.ddlyearland.DataBind();
            SelectViewNew();
            //GetGridSummary();

            //GetEmployeeName();
        }


        private void GetGridSummary()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            //string Empid = ((hst["empid"].ToString() == "") ? "" : hst["empid"].ToString()) + "%";

            string Empid = ((hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString());
            if (userrole == "1")
            {
                Empid = "%";
            }


            string comcod = this.GetComeCode();
            string srchempid = ((this.ddlEmpid.SelectedValue.ToString() == "000000000000") ? "93" : this.ddlEmpid.SelectedValue.ToString()) + "%";
            string Country = (this.ddlCountry.SelectedValue.ToString() == "0000000") ? "%" : this.ddlCountry.SelectedValue.ToString() + "%";
            string Dist = (this.ddlDist.SelectedValue.ToString() == "0000000") ? "%" : this.ddlDist.SelectedValue.ToString() + "%";
            string Zone = (this.ddlZone.SelectedValue.ToString() == "0000000") ? "%" : this.ddlZone.SelectedValue.ToString() + "%";
            string PStat = (this.ddlPStat.SelectedValue.ToString() == "0000000") ? "%" : this.ddlPStat.SelectedValue.ToString() + "%";
            string Area = (this.ddlArea.SelectedValue.ToString() == "0000000") ? "%" : this.ddlArea.SelectedValue.ToString() + "%";
            string Block = (this.ddlBlock.SelectedValue.ToString() == "0000000") ? "%" : this.ddlBlock.SelectedValue.ToString() + "%";
            string Road = (this.ddlRoad.SelectedValue.ToString() == "0000000") ? "%" : this.ddlRoad.SelectedValue.ToString() + "%";
            string Pri = (this.ddlPri.SelectedValue.ToString() == "0000000") ? "%" : this.ddlPri.SelectedValue.ToString() + "%";
            string Status = (this.ddlStatus.SelectedValue.ToString() == "0000000") ? "%" : this.ddlStatus.SelectedValue.ToString() + "%";
            string Other = this.ddlOther.SelectedValue.ToString();
            string TxtVal = "%" + this.txtVal.Text + "%";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "SP_ENTRY_LANDPROCUREMENT", "LINFOSUM", null, null, null, "8305%", Empid, Country, Dist, Zone, PStat, Area,
                Block, Road, Pri, Status, Other, TxtVal, srchempid);
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();

            ViewState["tblsummData"] = ds3.Tables[0];
            if (ds3.Tables[0].Rows.Count == 0)
                return;



            lblIntputtype.Value = "Active";


            DataView dv = ds3.Tables[0].Copy().DefaultView;
            string pempid = hst["empid"].ToString();
            if (pempid.Length == 0)
            {
                dv.RowFilter = ("active='False'");
            }
            else
            {
                //dv.RowFilter=("active='False'");
                dv.RowFilter=("(dealcode='" + pempid + "' or empid='"+ pempid + "') and active='False'");

            }
            DataTable dt = dv.ToTable();
            this.lbtPending.Text = "Pending:" + ((dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows.Count.ToString());

          
            this.Data_Bind();

        }


        private void Data_Bind()
        {
            DataTable dt = ((DataTable)ViewState["tblsummData"]).Copy();
            DataView dv = dt.DefaultView;


            string value = this.lblIntputtype.Value;

            switch (value)
            {
                case "Active":
                    this.gvSummary.Columns[14].Visible = false;
                    dv.RowFilter = ("active='True'");


                    break;
                case "Pending":

                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string pempid = hst["empid"].ToString();
                    if (pempid.Length == 0)
                    {
                        dv.RowFilter = ("active='False'");
                    }
                    else
                    {

                        dv.RowFilter = ("(dealcode='" + pempid + "' or empid='" + pempid + "') and active='False'");
                        //dv.RowFilter = ("dealcode='" + pempid + "' and active='False'");

                    }
                    
                    break;

                default:
                   // this.gvSummary.Columns[14].Visible = false;
                   // dv.RowFilter = ("active='True'");
                    break;



            }


            this.gvSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSummary.DataSource = dv.ToTable();
            this.gvSummary.DataBind();
            if (gvSummary.Rows.Count > 0)
            {
                Session["Report1"] = gvSummary;
                ((HyperLink)this.gvSummary.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }









        protected void lbtPending_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblsummData"];
            if (ASTUtility.Left(this.lbtPending.Text, 4) == "Pend")
            {
                this.lbtPending.Text = "Show";
                this.lblIntputtype.Value = "Pending";

                this.gvSummary.Columns[0].Visible = false;
                this.gvSummary.Columns[6].Visible = false;
                this.gvSummary.Columns[13].Visible = true;
            }
            else
            {
                this.lblIntputtype.Value = "Active";
                this.gvSummary.Columns[0].Visible = true;
                this.gvSummary.Columns[6].Visible = true;
                this.gvSummary.Columns[13].Visible = false;
            }

            this.Data_Bind();





        }
        private void GetInformation()
        {

            string comcod = this.GetComeCode();
            string txtinformation = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETLANDDDLINFOTYPE", txtinformation, "", "", "", "", "", "", "", "");
            this.ddlInformation.DataTextField = "prgdesc";
            this.ddlInformation.DataValueField = "prgcod";
            this.ddlInformation.DataSource = ds3.Tables[0];
            this.ddlInformation.DataBind();
        }



        protected void ddlInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectView();
            ///ddlNLandOwner_SelectedIndexChanged(null, null);
        }

        protected void ddlNLandOwner_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Bind_MultiData();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnOk_Click(object sender, EventArgs e) { }

        private void SelectViewNew()
        {
            this.MultiView1.ActiveViewIndex = 2;
        }




        private void SelectView()
        {
            string infoid = this.ddlInformation.SelectedValue.ToString();

            switch (infoid)
            {
                case "0300000":

                    this.MultiView1.ActiveViewIndex = 0;
                    this.divLaOw.Visible = false;
                    this.GetNoofLOData();
                    this.GetData();
                    this.GetPersonalinfo();
                    this.ShowPersonalInformation();
                    showplotinformation();
                    ShowPropDet();
                    ShowOther();
                    break;
                case "0400000":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.divLaOw.Visible = true;
                    this.GetNoofLOData();
                    this.GETLOINFOALLDATA();
                    this.Bind_MultiData();
                    break;
            }

        }

        private void GetPersonalinfo()
        {
            //string comcod = this.GetComeCode();
            //string landid = (string)ViewState["sircodegrid"];
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];//(divexland.Visible == false) ? LandNewCode() : (string)ViewState["sircodegrid"];


            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "LANDOWNERINFODETGRID", landid, "", "", "", "", "", "", "");
            ViewState["tblDatagridLOwnall"] = ds2;
            //DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable tblowner = ds2.Tables[0];

            if (ds2 == null)
                return;
            DataTable dt = tblowner;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1 = dt.DefaultView;
            string ownid = "01"; //ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            dv1.RowFilter = "own='" + ownid + "'";
            dt = dv1.ToTable();
            if (dv1.ToTable().Rows.Count == 0)
            {
                DataView dvtemp = ds2.Tables[0].DefaultView;
                dvtemp.RowFilter = "own='00'";
                if (dvtemp.ToTable().Rows.Count > 0)
                {

                    dt = dvtemp.ToTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["own"] = ownid;
                    }
                    ViewState["tblowner"] = dt;
                }
                else
                {
                    string fiowner = ds2.Tables[0].Rows[0]["own"].ToString();
                    dvtemp.RowFilter = "own='" + fiowner + "'";
                    int dtrow = dvtemp.ToTable().Rows.Count;
                    for (int i = 0; i < dtrow; i++)
                    {

                        DataRow dr = dt.NewRow();
                        dr["own"] = ownid;
                        dr["gph"] = dvtemp.ToTable().Rows[i]["gph"];
                        dr["gcod"] = dvtemp.ToTable().Rows[i]["gcod"];
                        dr["gval"] = dvtemp.ToTable().Rows[i]["gval"];
                        dr["slno"] = dvtemp.ToTable().Rows[i]["slno"];
                        dr["gdesc"] = dvtemp.ToTable().Rows[i]["gdesc"];
                        dr["gvalue"] = "";
                        dt.Rows.Add(dr);

                    }
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = "own='" + ownid + "'";
                    dt = dv1.ToTable();

                    DataTable newdt = new DataTable();
                    newdt = tblowner.Copy();
                    newdt.Merge(dt);
                    ViewState["tblowner"] = newdt;

                }

            }

            this.GvOwnerLand.DataSource = dt;// dv1.ToTable();// ds2.Tables[0];
            this.GvOwnerLand.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0401011": //Priority
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '51%'");
                        ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Visible = false;
                        //((TextBox)this.gvOwnerInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        //((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.GvOwnerLand.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }
            }
        }
        public void Bind_MultiData()
        {
            this.showLOinfo();
            this.showLOHomeAddress();
            this.showLOBusnsDet();
            this.ShowPersLO();
        }
        private void GetNoofLOData()
        {
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETLANDOWNERCOUNT", landid, "", "", "", "", "", "", "", "");

            this.ddlNLandOwner.DataTextField = "idesc";
            this.ddlNLandOwner.DataValueField = "id";
            this.ddlNLandOwner.DataSource = ds2.Tables[0];
            this.ddlNLandOwner.DataBind();

        }
        private void showLOinfo()
        {
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable tblowner = (DataTable)ViewState["tblowner"];

            if (ds2 == null)
                return;
            DataTable dt = tblowner;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "own='" + ownid + "'";
            dt = dv1.ToTable();
            if (dv1.ToTable().Rows.Count == 0)
            {
                DataView dvtemp = ds2.Tables[0].DefaultView;
                dvtemp.RowFilter = "own='00'";
                if (dvtemp.ToTable().Rows.Count > 0)
                {

                    dt = dvtemp.ToTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["own"] = ownid;
                    }
                    ViewState["tblowner"] = dt;
                }
                else
                {
                    string fiowner = ds2.Tables[0].Rows[0]["own"].ToString();
                    dvtemp.RowFilter = "own='" + fiowner + "'";
                    int dtrow = dvtemp.ToTable().Rows.Count;
                    for (int i = 0; i < dtrow; i++)
                    {

                        DataRow dr = dt.NewRow();
                        dr["own"] = ownid;
                        dr["gph"] = dvtemp.ToTable().Rows[i]["gph"];
                        dr["gcod"] = dvtemp.ToTable().Rows[i]["gcod"];
                        dr["gval"] = dvtemp.ToTable().Rows[i]["gval"];
                        dr["slno"] = dvtemp.ToTable().Rows[i]["slno"];
                        dr["gdesc"] = dvtemp.ToTable().Rows[i]["gdesc"];
                        dr["gvalue"] = "";
                        dt.Rows.Add(dr);

                    }
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = "own='" + ownid + "'";
                    dt = dv1.ToTable();

                    DataTable newdt = new DataTable();
                    newdt = tblowner.Copy();
                    newdt.Merge(dt);
                    ViewState["tblowner"] = newdt;

                }

            }

            this.gvOwnerInfo.DataSource = dt;// dv1.ToTable();// ds2.Tables[0];
            this.gvOwnerInfo.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {



                    case "0401011": //Priority
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '51%'");
                        ((TextBox)this.gvOwnerInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        //((TextBox)this.gvOwnerInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvOwnerInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvOwnerInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        //((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvOwnerInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvOwnerInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvOwnerInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }
            }
        }
        private void GetData()
        {
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "LANDINFODETGRID", landid, "", "", "", "", "", "", "", "");
            ViewState["tblDatagridall"] = ds2;
            return;
        }

        private DataSet GetAllData()
        {
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            DataSet ds2 = (DataSet)ViewState["tblDatagridall"];
            return ds2;
        }


        private void ShowPersonalInformation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            string teamid = hst["teamid"].ToString();
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[0];
            DataTable dt1 = ((DataTable)ViewState["tblsubddl"]).Copy(); 
          
            DataView dv1;
            //DataRow[] dr = ds2.Tables[0].Select("gcod='0301007'");
            //dr[0]["value"] = empid;


            this.gvPersonalInfo.DataSource = ds2.Tables[0];
            this.gvPersonalInfo.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0301001": //Source
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '31%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "0301003": //Dealing Person workdone

                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;             
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '93%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "0301005": //Priority
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '51%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "0301007": //Associate
                        dv1 = dt1.DefaultView;                        
                        if (comcod== "3348")
                        {
                            dv1.RowFilter = ("gcod="+empid+" ");
                        }
                        else
                        {
                            dv1.RowFilter = ("gcod like '93%'");
                        }
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }

            }

        }
        private void showplotinformation()
        {
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1;
            //DataView dv1;
            this.gvplot.DataSource = ds2.Tables[1];
            this.gvplot.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0302001": //Country
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '52%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panelr")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Visible = false;
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        if (ddlgval.Items.Count > 0)
                            ddlgval.SelectedIndex = 1;
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;



                    case "0302003": //District 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '53%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panelr")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Visible = false;
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        if (ddlgval.Items.Count > 0)
                            ddlgval.SelectedIndex = 1;
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;


                    case "0302005": //Zone

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '54%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panelr")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Visible = false;
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;
                    case "0302007": //Police Station                    
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '55%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panelr")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Visible = false;
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;
                    case "0302009": //Area

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '56%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panelr")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Visible = false;
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;
                    case "0302011": //Block

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '57%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panelr")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Visible = false;
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;
                    case "0302012": //Road

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '58%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Visible = false;
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panelr")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).Visible = false;
                        break;

                }

            }
        }

        private void ShowPropDet()
        {

            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[2];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];

            DataView dv1;
            this.gvpropdet.DataSource = ds2.Tables[2];
            this.gvpropdet.DataBind();

            DropDownList ddlgval;
            ListBox ddlPartic;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0303003": //Source
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '33%'");
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((Panel)this.gvpropdet.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Visible = false;
                        ddlgval = ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "0303005": //Dealing Person
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '34%'");

                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((Panel)this.gvpropdet.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Visible = false;
                        ddlgval = ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "0303009": //Priority facing
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '35%'");
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((Panel)this.gvpropdet.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Visible = false;
                        ddlgval = ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0303011": //Priority view
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '36%'");
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((Panel)this.gvpropdet.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Visible = false;
                        ddlgval = ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0303017": // plot Priority
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '37%'");
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((Panel)this.gvpropdet.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Visible = false;
                        ddlgval = ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0303019": //Priority
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '38%'");
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpropdet.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet")).Items.Clear();
                        ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet")).Visible = false;
                        ((Panel)this.gvpropdet.Rows[i].FindControl("pnlParic")).Visible = true;
                        ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Visible = true;

                        ddlPartic = ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic"));
                        ddlPartic.DataTextField = "gdesc";
                        ddlPartic.DataValueField = "gcod";
                        ddlPartic.DataSource = dv1.ToTable();
                        ddlPartic.DataBind();
                        if (((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim() != "")
                        {

                            ddlPartic.SelectedValue = ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        }
                        int count = Convert.ToInt32(ds2.Tables[2].Rows[i]["value"].ToString().Count());
                        int j;
                        int k = 0;
                        string data = "";
                        for (j = 0; j < count / 5; j++)
                        {
                            data = ds2.Tables[2].Rows[i]["value"].ToString().Substring(k, 5);
                            //DataRow[] dr3 = dv1.ToTable().Select("gcod = '" + data + "'");
                            foreach (ListItem item in ddlPartic.Items)
                            {
                                if (item.Value == data)
                                {
                                    item.Selected = true;
                                }

                            }
                            k = k + 5;
                        }

                        //ddlgvalA.Items = ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpropdet.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet")).Items.Clear();
                        ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet")).Visible = false;
                        ((Panel)this.gvpropdet.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Items.Clear();
                        ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Visible = false;
                        break;

                }

            }

        }

        private void ShowOther()
        {
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            this.gvother.DataSource = ds2.Tables[3];
            this.gvother.DataBind();

        }

        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "0301007")
                {

                    txtgvname.ReadOnly = true;

                }
                //if (code == "0401003")
                //{
                //    //text box change event 
                //    //txtgvname.TextChanged += new EventHandler(txtgvname_TextChanged);

                //}

            }


        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }




        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            try
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                if (ddlInformation.SelectedValue.ToString() == "0300000")//Land Information
                {
                    this.LandInfo();
                }
                else
                {
                    this.LandOwnerInfo();
                }




            }
            catch (Exception ex)
            {
                msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }



            //DataTable dt = (DataTable)ViewState["tblowner"];
            //DataTable dt1 = (DataTable)ViewState["tblhomeadd"];
            //DataTable dt2 = (DataTable)ViewState["tblbusiness"];
            //DataTable dt3 = (DataTable)ViewState["tblpersonal"];


            //if (ddlInformation.SelectedValue.ToString() == "0300000")//Land Information
            //{
            //    string landplotinfo = "";

            //    for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            //    {

            //        DataRow dr = dt.NewRow();
            //        string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
            //        dr["gcod"] = Gcode;
            //        dr["gvalue"] = Gvalue;
            //        dt.Rows.Add(dr);

            //    }

            //    for (int i = 0; i < this.gvplot.Rows.Count; i++)
            //    {
            //        DataRow dr = dt1.NewRow();
            //        string Gcode = ((Label)this.gvplot.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string Gvalue = "";
            //        if (Gcode == "0302001")
            //        {
            //            Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).SelectedValue.ToString();

            //        }
            //        else if (Gcode == "0302003")
            //        {
            //            Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).SelectedValue.ToString();
            //            //landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).SelectedItem.Text + ",";

            //        }
            //        else if (Gcode == "0302005")
            //        {
            //            Gvalue = (((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Count == 0) ?
            //                ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim() :
            //                ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).SelectedValue.ToString();
            //            landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).SelectedItem.Text + ", ";

            //        }
            //        else if (Gcode == "0302007")
            //        {
            //            Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).SelectedValue.ToString();
            //            landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).SelectedItem.Text + ", ";

            //        }
            //        else if (Gcode == "0302009")
            //        {
            //            Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).SelectedValue.ToString();
            //            landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).SelectedItem.Text + ", ";

            //        }
            //        else if (Gcode == "0302011")
            //        {
            //            Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).SelectedValue.ToString();
            //            landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).SelectedItem.Text + ", ";

            //        }
            //        else if (Gcode == "0302012")
            //        {
            //            Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).SelectedValue.ToString();
            //            landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).SelectedItem.Text + ", Plot: ";

            //        }
            //        else if (Gcode == "0302013")
            //        {
            //            Gvalue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //            landplotinfo += ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();

            //        }
            //        else
            //        {
            //            Gvalue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //        }

            //        dr["gcod"] = Gcode;
            //        dr["gvalue"] = Gvalue;
            //        dt1.Rows.Add(dr);

            //    }


            //    for (int i = 0; i < this.gvpropdet.Rows.Count; i++)
            //    {
            //        DataRow dr = dt2.NewRow();
            //        string Gcode = ((Label)this.gvpropdet.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string Gvalue = "";
            //        if (Gcode == "0303019")
            //        {
            //            foreach (ListItem item in ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Items)
            //            {
            //                if (item.Selected)
            //                {

            //                    if (item.Selected)
            //                    {
            //                        Gvalue += item.Value;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            Gvalue = (((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet")).Items.Count == 0) ? ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet")).SelectedValue.ToString();
            //        }
            //        dr["gcod"] = Gcode;
            //        dr["gvalue"] = Gvalue;
            //        dt2.Rows.Add(dr);
            //    }




            //    for (int i = 0; i < this.gvother.Rows.Count; i++)
            //    {
            //        DataRow dr = dt3.NewRow();
            //        string Gcode = ((Label)this.gvother.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string Gvalue = ((TextBox)this.gvother.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //        dr["gcod"] = Gcode;
            //        dr["gvalue"] = Gvalue;
            //        dt3.Rows.Add(dr);

            //    }
            //    DataSet ds = new DataSet("ds1");
            //    //ds.Merge(dt);
            //    ds.Tables.Add(dt);
            //    ds.Tables.Add(dt1);
            //    ds.Tables.Add(dt2);
            //    ds.Tables.Add(dt3);
            //    ds.Tables[0].TableName = "tblinfo1";
            //    ds.Tables[1].TableName = "tblinfo2";
            //    ds.Tables[2].TableName = "tblinfo3";
            //    ds.Tables[3].TableName = "tblinfo4";
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = this.GetCompCode();
            //    ProcessAccess ulogin = new ProcessAccess();
            //    string usrid = hst["usrid"].ToString();
            //    string empid = hst["empid"].ToString();

            //    string landid = (divexland.Visible == false) ? LandNewCode() : (string)ViewState["sircodegrid"];
            //    string landinfo = landplotinfo;
            //    bool result = HRData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATE_LINFO", ds, null, null, landid, landinfo, usrid, empid, "", "", "", "", "", "", "", "", "", "", "");

            //    //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            //}
            //else //if (ddlInformation.SelectedValue.ToString() == "0400000")//Land Owner Information
            //{
            //    string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            //    for (int i = 0; i < this.gvOwnerInfo.Rows.Count; i++)
            //    {
            //        string Gcode = ((Label)this.gvOwnerInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string Gvalue = (((DropDownList)this.gvOwnerInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ?
            //            ((TextBox)this.gvOwnerInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvOwnerInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
            //        DataRow[] dr2 = dt.Select("own='" + ownid + "' and gcod='" + Gcode + "'");
            //        if (dr2.Length > 0)
            //        {

            //            dr2[0]["gvalue"] = Gvalue;
            //        }
            //    }



            //    for (int i = 0; i < this.gvLOhomeadd.Rows.Count; i++)
            //    {

            //        string Gcode = ((Label)this.gvLOhomeadd.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string Gvalue = "";
            //        if (Gcode == "0402001")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).SelectedValue.ToString();

            //        }
            //        else if (Gcode == "0402003")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).SelectedValue.ToString();


            //        }
            //        else if (Gcode == "0402005")
            //        {
            //            Gvalue = (((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Count == 0) ?
            //                ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim() :
            //                ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).SelectedValue.ToString();


            //        }
            //        else if (Gcode == "0402007")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).SelectedValue.ToString();


            //        }
            //        else if (Gcode == "0402009")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).SelectedValue.ToString();


            //        }
            //        else if (Gcode == "0402011")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).SelectedValue.ToString();
            //        }
            //        else if (Gcode == "0402012")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).SelectedValue.ToString();
            //        }
            //        else
            //        {
            //            Gvalue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //        }
            //        DataRow[] dr2 = dt1.Select("own='" + ownid + "' and gcod='" + Gcode + "'");

            //        if (dr2.Length > 0)
            //        {

            //            dr2[0]["gvalue"] = Gvalue;
            //        }

            //    }
            //    for (int i = 0; i < this.gvLOBusns.Rows.Count; i++)
            //    {
            //        DataRow dr = dt2.NewRow();
            //        string Gcode = ((Label)this.gvLOBusns.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string Gvalue = "";
            //        if (Gcode == "0403009")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).SelectedValue.ToString();

            //        }
            //        else if (Gcode == "0403011")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).SelectedValue.ToString();


            //        }
            //        else if (Gcode == "0403013")
            //        {
            //            Gvalue = (((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Count == 0) ?
            //                ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim() :
            //                ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).SelectedValue.ToString();


            //        }
            //        else if (Gcode == "0403015")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).SelectedValue.ToString();


            //        }
            //        else if (Gcode == "0403017")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).SelectedValue.ToString();


            //        }
            //        else if (Gcode == "0403019")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).SelectedValue.ToString();
            //        }
            //        else if (Gcode == "0403020")
            //        {
            //            Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).SelectedValue.ToString();
            //        }
            //        else
            //        {
            //            Gvalue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //        }
            //        DataRow[] dr2 = dt2.Select("own='" + ownid + "' and gcod='" + Gcode + "'");

            //        if (dr2.Length > 0)
            //        {

            //            dr2[0]["gvalue"] = Gvalue;
            //        }



            //    }
            //    for (int i = 0; i < this.gvperLO.Rows.Count; i++)
            //    {
            //        DataRow dr = dt3.NewRow();
            //        string Gcode = ((Label)this.gvperLO.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string Gvalue = "";
            //        if (Gcode == "0404001")
            //        {
            //            Gvalue = ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

            //        }
            //        else if (Gcode == "0404003")
            //        {
            //            Gvalue = ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Text.Trim();


            //        }
            //        else if (Gcode == "0404005")
            //        {
            //            Gvalue = ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Text.Trim();


            //        }
            //        else if (Gcode == "0404011")
            //        {
            //            Gvalue = ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).SelectedValue.ToString();


            //        }
            //        else
            //        {
            //            Gvalue = ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //        }
            //        DataRow[] dr2 = dt3.Select("own='" + ownid + "' and gcod='" + Gcode + "'");

            //        if (dr2.Length > 0)
            //        {

            //            dr2[0]["gvalue"] = Gvalue;
            //        }


            //    }


            //    string LOsl = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);



            //    DataSet ds = new DataSet("ds2");
            //    //ds.Merge(dt);

            //    dt.Columns.Remove("comcod");
            //    dt.Columns.Remove("gph");
            //    dt.Columns.Remove("gdesc");
            //    dt.Columns.Remove("gval");
            //    dt.Columns.Remove("slno");

            //    dt1.Columns.Remove("comcod");
            //    dt1.Columns.Remove("gph");
            //    dt1.Columns.Remove("gdesc");
            //    dt1.Columns.Remove("gval");
            //    dt1.Columns.Remove("slno");

            //    dt2.Columns.Remove("comcod");
            //    dt2.Columns.Remove("gph");
            //    dt2.Columns.Remove("gdesc");
            //    dt2.Columns.Remove("gval");
            //    dt2.Columns.Remove("slno");

            //    dt3.Columns.Remove("comcod");
            //    dt3.Columns.Remove("gph");
            //    dt3.Columns.Remove("gdesc");
            //    dt3.Columns.Remove("gval");
            //    dt3.Columns.Remove("slno");



            //    ds.Merge(dt);
            //    ds.Merge(dt1);
            //    ds.Merge(dt2);
            //    ds.Merge(dt3);

            //    ds.Tables[0].TableName = "tbl1";
            //    ds.Tables[1].TableName = "tbl2";
            //    ds.Tables[2].TableName = "tbl3";
            //    ds.Tables[3].TableName = "tbl4";
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = this.GetCompCode();
            //    ProcessAccess ulogin = new ProcessAccess();
            //    string usrid = hst["usrid"].ToString();
            //    string landid = (divexland.Visible == false) ? LandNewCode() : (string)ViewState["sircodegrid"];
            //    bool result = HRData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATE_LOINFO", ds, null, null, landid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            //    if (result == true)
            //    {
            //        this.GETLOINFOALLDATA();
            //    }
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            //}



        }



        //private string GetNewId()
        //{


        //    string comcod = this.GetComeCode();
        //    DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTNEWCODE", "", "", "", "", "", "", "", "", "");
        //    string clientid = ds1.Tables[0].Rows[0]["sircode"].ToString();
        //    ds1.Dispose();
        //    this.lblnewprospect.Value = clientid;
        //    return clientid;




        //}


        private void LandInfo()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            ProcessAccess ulogin = new ProcessAccess();
            string usrid = hst["usrid"].ToString();
            //string empid = hst["empid"].ToString();
            string empid = "";


            DataTable dt = new DataTable();
            DataTable dtowner = new DataTable();
            DataTable dthomadd = new DataTable();
            DataTable dtbusinesadd = new DataTable();
            DataTable dtPeradd = new DataTable();


            dt.Clear();
            dt.Columns.Add("gcod");
            dt.Columns.Add("gval");
            dt.Columns.Add("gvalue");
            dt.Columns.Add("remarks");
            //dt1.Clear();
            //dt1.Columns.Add("gcod");
            //dt1.Columns.Add("gvalue");
            //dt2.Clear();
            //dt2.Columns.Add("gcod");
            //dt2.Columns.Add("gvalue");
            //dt3.Clear();
            //dt3.Columns.Add("gcod");
            //dt3.Columns.Add("gvalue");
            dtowner.Clear();
            dtowner.TableName = "tbl1";
            dtowner.Columns.Add("own");
            dtowner.Columns.Add("gcod");
            dtowner.Columns.Add("gval");
            dtowner.Columns.Add("gvalue");




            string landplotinfo = "";
            string landid = (divexland.Visible == false) ? LandNewCode() : (string)ViewState["sircodegrid"]; //newlandcode
                                                                                                             // Duplicate Land Information


            DataSet dsloinfo = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "LANDOWNERINFO", landid, "", "", "", "", "", "", "", "");
            if (dsloinfo == null)
            {
                return;
            }

            DataTable dtlowoinfo = dsloinfo.Tables[1];
            //dtlohom.TableName = "tbl2";

            //DataTable dtbusiness = dsloinfo.Tables[2];
            //dtbusiness.TableName = "tbl3";

            //DataTable dtpersonal = dsloinfo.Tables[3];
            //dtpersonal.TableName = "tbl4";

            string Name = "";
            string Phone = "";

            for (int i = 0; i < this.GvOwnerLand.Rows.Count; i++)
            {
                string Gcode = ((Label)this.GvOwnerLand.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                if (Gcode == "0401001")
                {
                    Name = ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Text.Trim();

                    if (Name.Trim().Length == 0)
                    {
                        msg = "Please provide Land Owner Name .. ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        return;

                    }
                }
                if (Gcode == "0401003") // phone number check 
                {
                    Phone = ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Text.Trim();

                    if (Phone.Trim().Length == 0)
                    {

                        msg = "Please provide Land Owner Phone .. ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        return;
                    }
                    if (Phone.Trim().Length > 11)
                    {
                        msg = "Phone Number Must 11 Digit... ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        return;
                    }
                    else
                    {
                        this.getDuplicatePhone(comcod, Phone);

                        //DataSet dsp = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "CHECKDUPLICATEPHONE", Phone, "", "", "", "", "", "", "", "");

                        //if (dsp.Tables[0].Rows.Count !=0)
                        //{
                        //    string pid = dsp.Tables[0].Rows[0]["pid"].ToString();
                        //    string sirdesc = dsp.Tables[0].Rows[0]["username"].ToString();
                        //    string supervisor = dsp.Tables[0].Rows[0]["leadname"].ToString();
                        //    string phone = dsp.Tables[0].Rows[0]["phone"].ToString();                                         
                        //    string Message = "Duplicate : ";
                        //    string totmsg = Message + phone + ", " + pid + ", Associate : " + sirdesc + ", Team :  " + supervisor;
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + totmsg + "')", true);
                        //    return;
                        //}
                    }
                }
            }

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gval = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgvalper")).Text.Trim();

                if (Gcode == "0301007")
                {
                    empid = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                }

                string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ?
                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["gvalue"] = Gvalue;
                dt.Rows.Add(dr);

            }

            for (int i = 0; i < this.gvplot.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                string Gcode = ((Label)this.gvplot.Rows[i].FindControl("lblgvItmCodeplot")).Text.Trim();
                string gval = ((Label)this.gvplot.Rows[i].FindControl("lgvgvalplot")).Text.Trim();
                string Gvalue = "";
                if (Gcode == "0302001")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).SelectedValue.ToString();

                }
                else if (Gcode == "0302003")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).SelectedValue.ToString();
                    //landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).SelectedItem.Text + ",";

                }
                else if (Gcode == "0302005")
                {
                    Gvalue = (((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Count == 0) ?
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim() :
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).SelectedValue.ToString();
                    landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).SelectedItem.Text + ", ";

                }
                else if (Gcode == "0302007")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).SelectedValue.ToString();
                    landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).SelectedItem.Text + ", ";

                }
                else if (Gcode == "0302009")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).SelectedValue.ToString();
                    landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).SelectedItem.Text + ", ";

                }
                else if (Gcode == "0302011")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).SelectedValue.ToString();
                    landplotinfo += ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot")).SelectedItem.Text + ", ";

                }
                else if (Gcode == "0302012")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).SelectedValue.ToString();
                    string val = Gvalue.Length == 0 ? "" : ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr")).SelectedItem.Text;
                    landplotinfo += val + ", Plot: ";

                }
                else if (Gcode == "0302013")
                {
                    Gvalue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                    landplotinfo += ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();

                }
                else
                {
                    Gvalue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                }

                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["gvalue"] = Gvalue;
                dt.Rows.Add(dr);

            }


            for (int i = 0; i < this.gvpropdet.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                string Gcode = ((Label)this.gvpropdet.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gval = ((Label)this.gvpropdet.Rows[i].FindControl("lgvgvalplotdet")).Text.Trim();
                string Gvalue = "";
                string remarks = "";
                if (Gcode == "0303019")
                {
                    foreach (ListItem item in ((ListBox)this.gvpropdet.Rows[i].FindControl("ddlPartic")).Items)
                    {
                        if (item.Selected)
                        {

                            if (item.Selected)
                            {
                                Gvalue += item.Value;
                                remarks = remarks + item.Text + ", ";
                            }
                        }
                    }
                }
                else
                {
                    Gvalue = (((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet")).Items.Count == 0) ? ((TextBox)this.gvpropdet.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvpropdet.Rows[i].FindControl("ddlvalprojdet")).SelectedValue.ToString();
                }
                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["gvalue"] = Gvalue;
                dr["remarks"] = remarks.Length == 0 ? "" : remarks.Substring(0, remarks.Length - 2);
                dt.Rows.Add(dr);
            }




            for (int i = 0; i < this.gvother.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                string Gcode = ((Label)this.gvother.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gval = ((Label)this.gvother.Rows[i].FindControl("lgvgvalother")).Text.Trim();
                string Gvalue = ((TextBox)this.gvother.Rows[i].FindControl("txtgvVal")).Text.Trim();
                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["gvalue"] = Gvalue;
                dt.Rows.Add(dr);

            }




            for (int i = 0; i < this.GvOwnerLand.Rows.Count; i++)
            {

                DataRow dr = dtowner.NewRow();
                string ownid = "01"; //ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
                string Gcode = ((Label)this.GvOwnerLand.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gval = ((Label)this.GvOwnerLand.Rows[i].FindControl("lgvgvallowner")).Text.Trim();
                string Gvalue = (((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                dr["own"] = ownid;
                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["gvalue"] = Gvalue;
                dtowner.Rows.Add(dr);

            }

            DataSet ds1 = new DataSet("ds1");
            DataSet ds2 = new DataSet("ds2");
            //ds.Merge(dt);
            ds1.Tables.Add(dt);
            //ds.Tables.Add(dt1);
            //ds.Tables.Add(dt2);
            //ds.Tables.Add(dt3);



            foreach (DataRow dr1 in dtowner.Rows)
            {
                dtlowoinfo.ImportRow(dr1);

                //dtlowoinfo.Rows.Merge(dr1);



            }


            ds2.Merge(dtlowoinfo);
            //dtlohom = dtlohom.Copy();
            //ds2.Merge(dtlohom);
            //ds2.Merge(dtbusiness);
            //ds2.Merge(dtpersonal);




            ds1.Tables[0].TableName = "tbl1";
            ds2.Tables[0].TableName = "tbl1";
            //string xml = ds1.GetXml();
            //string xml1 = ds2.GetXml();




            string landinfo = landplotinfo;
            //Check Duplicate


            if (divexland.Visible == false)
            {
                DataSet dsd = HRData.GetTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "CHECKDUPLANDINFO", landinfo, "", "", "", "", "", "", "", "", "");


                if (dsd == null)
                {
                    return;
                }



                if (dsd.Tables[0].Rows.Count == 0)
                {



                }

                else
                {
                    string empname = dsd.Tables[0].Rows[0]["empname"].ToString();
                    string holdername = " His/Her Name " + empname;
                    string Message = "Found Duplicate Land Info. ";
                    string totmsg = Message + holdername;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success",
                               "alert('" + totmsg + "')", true);
                    return;
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alertmsg('" + Message + holdername + "','" + faclass + "');", true);
                }


            }

            string date = this.txtdate.Text.Trim();
            bool result = HRData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATE_LINFO", ds1, ds2, null, landid, landinfo, usrid, empid, date, "", "", "", "", "", "", "", "", "", "");

            msg = " Land Information Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);


            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Update Land Information (Land CRM)";
                string eventdesc = "Update Land Information (Land CRM)";
                string eventdesc2 = lbllandname.Text;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }




        }
        private void LandOwnerInfo()
        {
            DataTable dt = (DataTable)ViewState["tblowner"];
            DataTable dt1 = (DataTable)ViewState["tblhomeadd"];
            DataTable dt2 = (DataTable)ViewState["tblbusiness"];
            DataTable dt3 = (DataTable)ViewState["tblpersonal"];



            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            for (int i = 0; i < this.gvOwnerInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvOwnerInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string Gvalue = (((DropDownList)this.gvOwnerInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ?
                    ((TextBox)this.gvOwnerInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvOwnerInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                DataRow[] dr2 = dt.Select("own='" + ownid + "' and gcod='" + Gcode + "'");
                if (dr2.Length > 0)
                {

                    dr2[0]["gvalue"] = Gvalue;
                }
            }



            for (int i = 0; i < this.gvLOhomeadd.Rows.Count; i++)
            {

                string Gcode = ((Label)this.gvLOhomeadd.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string Gvalue = "";
                if (Gcode == "0402001")
                {
                    Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).SelectedValue.ToString();

                }
                else if (Gcode == "0402003")
                {
                    Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).SelectedValue.ToString();


                }
                else if (Gcode == "0402005")
                {
                    Gvalue = (((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Count == 0) ?
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim() :
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).SelectedValue.ToString();


                }
                else if (Gcode == "0402007")
                {
                    Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).SelectedValue.ToString();


                }
                else if (Gcode == "0402009")
                {
                    Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).SelectedValue.ToString();


                }
                else if (Gcode == "0402011")
                {
                    Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).SelectedValue.ToString();
                }
                else if (Gcode == "0402012")
                {
                    Gvalue = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).SelectedValue.ToString();
                }
                else
                {
                    Gvalue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
                }
                DataRow[] dr2 = dt1.Select("own='" + ownid + "' and gcod='" + Gcode + "'");

                if (dr2.Length > 0)
                {

                    dr2[0]["gvalue"] = Gvalue;
                }

            }
            for (int i = 0; i < this.gvLOBusns.Rows.Count; i++)
            {
                DataRow dr = dt2.NewRow();
                string Gcode = ((Label)this.gvLOBusns.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string Gvalue = "";
                if (Gcode == "0403009")
                {
                    Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).SelectedValue.ToString();

                }
                else if (Gcode == "0403011")
                {
                    Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).SelectedValue.ToString();


                }
                else if (Gcode == "0403013")
                {
                    Gvalue = (((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Count == 0) ?
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim() :
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).SelectedValue.ToString();


                }
                else if (Gcode == "0403015")
                {
                    Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).SelectedValue.ToString();


                }
                else if (Gcode == "0403017")
                {
                    Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).SelectedValue.ToString();


                }
                else if (Gcode == "0403019")
                {
                    Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).SelectedValue.ToString();
                }
                else if (Gcode == "0403020")
                {
                    Gvalue = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).SelectedValue.ToString();
                }
                else
                {
                    Gvalue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
                }
                DataRow[] dr2 = dt2.Select("own='" + ownid + "' and gcod='" + Gcode + "'");

                if (dr2.Length > 0)
                {

                    dr2[0]["gvalue"] = Gvalue;
                }



            }
            for (int i = 0; i < this.gvperLO.Rows.Count; i++)
            {
                DataRow dr = dt3.NewRow();
                string Gcode = ((Label)this.gvperLO.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string Gvalue = "";
                if (Gcode == "0404001")
                {
                    Gvalue = ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                }
                else if (Gcode == "0404003")
                {
                    Gvalue = ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Text.Trim();


                }
                else if (Gcode == "0404005")
                {
                    Gvalue = ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Text.Trim();


                }
                else if (Gcode == "0404011")
                {
                    Gvalue = ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).SelectedValue.ToString();


                }
                else
                {
                    Gvalue = ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvVal")).Text.Trim();
                }
                DataRow[] dr2 = dt3.Select("own='" + ownid + "' and gcod='" + Gcode + "'");

                if (dr2.Length > 0)
                {

                    dr2[0]["gvalue"] = Gvalue;
                }


            }

            string LOsl = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);



            DataSet ds = new DataSet("ds2");
            //ds.Merge(dt);

            dt.Columns.Remove("comcod");
            dt.Columns.Remove("gph");
            dt.Columns.Remove("gdesc");
            dt.Columns.Remove("gval");
            dt.Columns.Remove("slno");

            dt1.Columns.Remove("comcod");
            dt1.Columns.Remove("gph");
            dt1.Columns.Remove("gdesc");
            dt1.Columns.Remove("gval");
            dt1.Columns.Remove("slno");

            dt2.Columns.Remove("comcod");
            dt2.Columns.Remove("gph");
            dt2.Columns.Remove("gdesc");
            dt2.Columns.Remove("gval");
            dt2.Columns.Remove("slno");

            dt3.Columns.Remove("comcod");
            dt3.Columns.Remove("gph");
            dt3.Columns.Remove("gdesc");
            dt3.Columns.Remove("gval");
            dt3.Columns.Remove("slno");



            ds.Merge(dt);
            ds.Merge(dt1);
            ds.Merge(dt2);
            ds.Merge(dt3);

            ds.Tables[0].TableName = "tbl1";
            ds.Tables[1].TableName = "tbl2";
            ds.Tables[2].TableName = "tbl3";
            ds.Tables[3].TableName = "tbl4";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            ProcessAccess ulogin = new ProcessAccess();
            string usrid = hst["usrid"].ToString();
            string landid = (string)ViewState["sircodegrid"]; //(divexland.Visible == false) ? LandNewCode() : (string)ViewState["sircodegrid"];
                                                              // string date = this.txtdate.Text.Trim() ;
            bool result = HRData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATE_LOINFO", ds, null, null, landid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.GETLOINFOALLDATA();
            }
            msg = " Land Owner Information Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);


            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Update Land Owner Information (Land CRM)";
                string eventdesc = "Update Land Owner Information (Land CRM)";
                string eventdesc2 = "";

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }

        }

        protected void ddlvalplot_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0302003": //District   
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string country = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalplot")).Text.Trim();
                        DataView dv2;
                        dv2 = dt1.DefaultView;
                        dv2.RowFilter = ("code='" + country + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302005": //Zone

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string district = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvald")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302007": //Police Station                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string zone = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302009": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string ps = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302011": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvala")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302012": //Road

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string block = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblockplot")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;

                }
            }
        }

        protected void ddlvald_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0302005": //Zone

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string district = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvald")).Text.Trim();
                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302007": //Police Station                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string zone = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302009": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string ps = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302011": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvala")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302012": //Road

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string block = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblockplot")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }

        protected void ddlvalz_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0302007": //Police Station                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string zone = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302009": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string ps = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302011": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvala")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302012": //Road

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string block = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblockplot")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }

        protected void ddlvalp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0302009": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string ps = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302011": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvala")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302012": //Road

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string block = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblockplot")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }

        protected void ddlvala_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0302011": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvala")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblockplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0302012": //Road

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string block = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblockplot")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }

        protected void ddlblockplot_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds2 = GetAllData();
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    //case "0302011": //Block

                    //    ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvala")).Text.Trim();
                    //    DataView dv6;
                    //    dv6 = dt1.DefaultView;
                    //    dv6.RowFilter = ("code='" + area + "'");

                    //    ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv6.ToTable();
                    //    ddlgval.DataBind();

                    //    break;
                    case "0302012": //Road

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string block = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblockplot")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");
                        //DataTable dtE = dv7.ToTable();
                        //if (dtE.Rows.Count == 0)
                        //    dtE.Rows.Add("0000000", "Choose One..", "");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlpnlr"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }

        protected void ddlpnlr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string LandNewCode()
        {
            DataTable dt = (DataTable)ViewState["newlandcode"];
            if (dt.Rows.Count != 0)
            {
                ViewState["newlandcode"] = dt;
                return (dt.Rows[0]["sircode"].ToString());
            }
            else
            {
                string comcod = this.GetComeCode();
                string yearcod = ASTUtility.Left(ddlyearland.SelectedValue.ToString(), 7);
                DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "LANDADDNEW", yearcod, "", "", "", "", "", "", "", "");
                ViewState["newlandcode"] = ds2.Tables[0];
                landid = ds2.Tables[0].Rows[0]["sircode"].ToString();
                return (ds2.Tables[0].Rows[0]["sircode"].ToString());

            }


        }

        protected void btnaddland_Click(object sender, EventArgs e)
        {

            if (btnaddland.Text == "Add Land")
            {
                this.lUpdatPerInfo.Visible = true;
                //this.CreateTable();
                ViewState["sircodegrid"] = "New";//LandNewCode();
                divexland.Visible = false;
                this.lbtPending.Visible = true;
                GetData();
                this.MultiView1.ActiveViewIndex = 0;
                this.GetPersonalinfo();
                this.ShowPersonalInformation();
                showplotinformation();
                ShowPropDet();
                ShowOther();
                btnaddland.Text = "Back";
                // this.divNot.Visible = false;
                //this.divSear.Visible = false;


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Add Land (Land CRM)";
                    string eventdesc = "Add Land (Land CRM)";
                    string eventdesc2 = "";
                    string comcod = this.GetCompCode();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }


            }
            else
            {
                this.ddlNLandOwner.Items.Clear();
                this.CreateTable();
                this.lUpdatPerInfo.Visible = false;
                this.MultiView1.ActiveViewIndex = 2;
                GetYEARLAND();
                GetInformation();
                divexland.Visible = false;
                this.lbtPending.Visible = true;
                divddlinfo.Visible = false;
                divLaOw.Visible = false;
                btnaddland.Text = "Add Land";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Back Option (Land CRM)";
                    string eventdesc = "Back Option (Land CRM)";
                    string eventdesc2 = "";
                    string comcod = this.GetCompCode();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }

            }


        }

        protected void ddlyearland_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGridSummary();
            //GetEmployeeName();
        }



        protected void lnkLOEnterInfo_Click(object sender, EventArgs e)
        {


        }
        private void GETLOINFOALLDATA()
        {
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];//(divexland.Visible == false) ? LandNewCode() : (string)ViewState["sircodegrid"];


            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "LANDOWNERINFODETGRID", landid, "", "", "", "", "", "", "");
            ViewState["tblDatagridLOwnall"] = ds2;
            ViewState["tblowner"] = ds2.Tables[0];
            ViewState["tblhomeadd"] = ds2.Tables[1];
            ViewState["tblbusiness"] = ds2.Tables[2];
            ViewState["tblpersonal"] = ds2.Tables[3];
            //if (ds2.Tables[3].Rows.Count > 0)
            //{
            //    this.Text1.Value = ds2.Tables[4].Rows[0]["name1"].ToString();
            //    this.Number1.Value = ds2.Tables[4].Rows[0]["phn"].ToString();
            //    this.Email1.Value = ds2.Tables[4].Rows[0]["email"].ToString();
            //    this.Text2.Value = ds2.Tables[4].Rows[0]["img"].ToString();
            //    this.Text3.Value = ds2.Tables[4].Rows[0]["name2"].ToString();
            //    this.Text4.Value = ds2.Tables[4].Rows[0]["name3"].ToString();
            //    this.Text5.Value = ds2.Tables[4].Rows[0]["name4"].ToString();
            //    this.Text6.Value = ds2.Tables[4].Rows[0]["name5"].ToString();
            //    if (this.Text3.Value != "")
            //    {
            //        div1.Visible = true;
            //    }

            //    if (this.Text4.Value != "")
            //    {
            //        div2.Visible = true;
            //    }
            //    if (this.Text5.Value != "")
            //    {
            //        div3.Visible = true;
            //    }
            //    if (this.Text6.Value != "")
            //    {
            //        div4.Visible = true;
            //    }
            //}
            //else
            //{
            //    this.Text1.Value = "";
            //    this.Number1.Value = "";
            //    this.Email1.Value = "";
            //    this.Text2.Value = "";
            //    this.Text3.Value = "";
            //    this.Text4.Value = "";
            //    this.Text5.Value = "";
            //    this.Text6.Value = "";
            //    div1.Visible = false;
            //    div2.Visible = false;
            //    div3.Visible = false;
            //    div4.Visible = false;


            //}




            return;
        }
        private void showLOHomeAddress()
        {
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable tblhomeadd = (DataTable)ViewState["tblhomeadd"];
            if (ds2 == null)
                return;
            DataTable dt = tblhomeadd;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "own='" + ownid + "'";
            dt = dv1.ToTable();
            if (dv1.ToTable().Rows.Count == 0)
            {
                DataView dvtemp = ds2.Tables[1].DefaultView;
                dvtemp.RowFilter = "own='00'";
                if (dvtemp.ToTable().Rows.Count > 0)
                {
                    dt = dvtemp.ToTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["own"] = ownid;
                    }
                    ViewState["tblhomeadd"] = dt;
                }
                else
                {
                    string fiowner = ds2.Tables[1].Rows[0]["own"].ToString();
                    dvtemp.RowFilter = "own='" + fiowner + "'";
                    int dtrow = dvtemp.ToTable().Rows.Count;
                    for (int i = 0; i < dtrow; i++)
                    {

                        DataRow dr = dt.NewRow();
                        dr["own"] = ownid;
                        dr["gph"] = dvtemp.ToTable().Rows[i]["gph"];
                        dr["gcod"] = dvtemp.ToTable().Rows[i]["gcod"];
                        dr["gval"] = dvtemp.ToTable().Rows[i]["gval"];
                        dr["slno"] = dvtemp.ToTable().Rows[i]["slno"];
                        dr["gdesc"] = dvtemp.ToTable().Rows[i]["gdesc"];
                        dr["gvalue"] = "";
                        dt.Rows.Add(dr);

                    }
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = "own='" + ownid + "'";
                    dt = dv1.ToTable();

                    DataTable newdt = new DataTable();
                    newdt = tblhomeadd.Copy();
                    newdt.Merge(dt);
                    ViewState["tblhomeadd"] = newdt;
                }


            }

            this.gvLOhomeadd.DataSource = dt;// dv1.ToTable();
            this.gvLOhomeadd.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0402001": //Country
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '52%' or gcod like '00%'");
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelBlA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelrA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "0402003": //District 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '53%' or gcod like '00%'");
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelBlA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelrA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "0402005": //Zone

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '54%' or gcod like '00%'");
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelBlA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelrA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0402007": //Police Station                    
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '55%' or gcod like '00%'");
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelBlA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelrA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0402009": //Area

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '56%' or gcod like '00%'");
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelrA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelBlA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0402011": //Block

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '57%' or gcod like '00%'");
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelrA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0402012": //Road

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '58%' or gcod like '00%'");
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelBlA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalplotA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelBlA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA")).Visible = false;
                        ((Panel)this.gvLOhomeadd.Rows[i].FindControl("PanelrA")).Visible = false;
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Items.Clear();
                        ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA")).Visible = false;
                        break;

                }

            }
        }

        private void showLOBusnsDet()
        {
            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable tblbusiness = (DataTable)ViewState["tblbusiness"];
            if (ds2 == null)
                return;
            DataTable dt = tblbusiness;// ds2.Tables[2];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "own='" + ownid + "'";
            dt = dv1.ToTable();
            if (dv1.ToTable().Rows.Count == 0)
            {
                DataView dvtemp = ds2.Tables[2].DefaultView;
                dvtemp.RowFilter = "own='00'";
                if (dvtemp.ToTable().Rows.Count > 0)
                {
                    dt = dvtemp.ToTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["own"] = ownid;
                    }
                    ViewState["tblbusiness"] = dt;
                }
                else
                {
                    string fiowner = ds2.Tables[2].Rows[0]["own"].ToString();
                    dvtemp.RowFilter = "own='" + fiowner + "'";
                    int dtrow = dvtemp.ToTable().Rows.Count;
                    for (int i = 0; i < dtrow; i++)
                    {

                        DataRow dr = dt.NewRow();
                        dr["own"] = ownid;
                        dr["gph"] = dvtemp.ToTable().Rows[i]["gph"];
                        dr["gcod"] = dvtemp.ToTable().Rows[i]["gcod"];
                        dr["gval"] = dvtemp.ToTable().Rows[i]["gval"];
                        dr["slno"] = dvtemp.ToTable().Rows[i]["slno"];
                        dr["gdesc"] = dvtemp.ToTable().Rows[i]["gdesc"];
                        dr["gvalue"] = "";
                        dt.Rows.Add(dr);

                    }
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = "own='" + ownid + "'";
                    dt = dv1.ToTable();

                    DataTable newdt = new DataTable();
                    newdt = tblbusiness.Copy();
                    newdt.Merge(dt);
                    ViewState["tblbusiness"] = newdt;
                }


            }

            this.gvLOBusns.DataSource = dt;// dv1.ToTable();// ds2.Tables[2];
            this.gvLOBusns.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0403009": //Country
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '52%' or gcod like '00%'");
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelBlAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelrAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "0403011": //District 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '53%' or gcod like '00%'");
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelBlAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelrAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "0403013": //Zone

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '54%' or gcod like '00%'");
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelBlAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelrAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0403015": //Police Station                    
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '55%' or gcod like '00%'");
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelBlAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelrAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0403017": //Area

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '56%' or gcod like '00%'");
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelBlAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelrAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0403019": //Block

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '57%' or gcod like '00%'");
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelrAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelrAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0403020": //Road

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '58%' or gcod like '00%'");
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelBlAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Visible = false;
                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalplotAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelBlAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB")).Visible = false;
                        ((Panel)this.gvLOBusns.Rows[i].FindControl("PanelrAB")).Visible = false;
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Items.Clear();
                        ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB")).Visible = false;
                        break;

                }

            }
        }


        private void ShowPersLO()
        {

            string comcod = this.GetComeCode();
            string landid = (string)ViewState["sircodegrid"];
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable tblpersonal = (DataTable)ViewState["tblpersonal"];
            if (ds2 == null)
                return;
            DataTable dt = tblpersonal;// ds2.Tables[3];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];




            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "own='" + ownid + "'";
            dt = dv1.ToTable();
            if (dv1.ToTable().Rows.Count == 0)
            {
                DataView dvtemp = ds2.Tables[3].DefaultView;
                dvtemp.RowFilter = "own='00'";
                if (dvtemp.ToTable().Rows.Count > 0)
                {
                    dt = dvtemp.ToTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["own"] = ownid;
                    }
                    ViewState["tblpersonal"] = dt;
                }
                else
                {
                    string fiowner = ds2.Tables[3].Rows[0]["own"].ToString();
                    dvtemp.RowFilter = "own='" + fiowner + "'";
                    int dtrow = dvtemp.ToTable().Rows.Count;
                    for (int i = 0; i < dtrow; i++)
                    {

                        DataRow dr = dt.NewRow();
                        dr["own"] = ownid;
                        dr["gph"] = dvtemp.ToTable().Rows[i]["gph"];
                        dr["gcod"] = dvtemp.ToTable().Rows[i]["gcod"];
                        dr["gval"] = dvtemp.ToTable().Rows[i]["gval"];
                        dr["slno"] = dvtemp.ToTable().Rows[i]["slno"];
                        dr["gdesc"] = dvtemp.ToTable().Rows[i]["gdesc"];
                        dr["gvalue"] = "";
                        dt.Rows.Add(dr);

                    }
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = "own='" + ownid + "'";
                    dt = dv1.ToTable();

                    DataTable newdt = new DataTable();
                    newdt = tblpersonal.Copy();
                    newdt.Merge(dt);
                    ViewState["tblpersonal"] = newdt;
                }

            }

            this.gvperLO.DataSource = dt;// dv1.ToTable();// ds2.Tables[3];
            this.gvperLO.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0404001": //District
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '53%'");
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "0404003": //Dealing Person
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Visible = true;
                        ((Panel)this.gvperLO.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).Visible = false;
                        break;


                    case "0404005": //Priority
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Visible = true;
                        ((Panel)this.gvperLO.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).Visible = false;
                        break;
                    case "0404011": //Religion
                                    //dv1 = dt1.DefaultView;
                                    //dv1.RowFilter = ("gcod like '95%'");
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = (DataTable)ViewState["tblstatus"];  //DataTable dt1 = (DataTable)ViewState["tblstatus"];
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    default:
                        ((TextBox)this.gvperLO.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvperLO.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvperLO.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }

            }

        }



        protected void ddlvalplotA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[1];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();

            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[1];

            }

            if (ds2 == null)
                return;

            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0402003": //District   
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string country = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalplotA")).Text.Trim();
                        DataView dv2;
                        dv2 = dt1.DefaultView;
                        dv2.RowFilter = ("code='" + country + "'");
                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvaldA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402005": //Zone

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string district = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvaldA")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402007": //Police Station                    
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalzA")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402009": //Area

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalpA")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402011": //Block

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalaA")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402012": //Road

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlblockA")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlvaldA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[1];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[1];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0402005": //Zone

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string district = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvaldA")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalzA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402007": //Police Station                    
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalzA")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402009": //Area

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalpA")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402011": //Block

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalaA")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402012": //Road

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlblockA")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlvalzA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[1];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[1];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0402007": //Police Station                    
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalzA")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalpA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402009": //Area

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalpA")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402011": //Block

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalaA")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402012": //Road

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlblockA")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlvalpA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[1];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[1];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0402009": //Area

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalpA")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlvalaA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402011": //Block

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalaA")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402012": //Road

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlblockA")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlvalaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[1];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[1];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0402011": //Block

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalaA")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0402012": //Road

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlblockA")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }

        protected void ddlblockA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[1];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[1];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[1];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    //case "0402011": //Block

                    //    ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    string area = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlvalaA")).Text.Trim();
                    //    DataView dv6;
                    //    dv6 = dt1.DefaultView;
                    //    dv6.RowFilter = ("code='" + area + "'");

                    //    ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlblockA"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv6.ToTable();
                    //    ddlgval.DataBind();

                    //    break;
                    case "0402012": //Road

                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOhomeadd.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOhomeadd.Rows[i - 1].FindControl("ddlblockA")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOhomeadd.Rows[i].FindControl("ddlpnlrA"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlpnlrA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlpnlrAB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlvalaAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[2];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[2];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[2];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0403019": //Block

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalaAB")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403020": //Road

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlblockAB")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlblockAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[2];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[2];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[2];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    //case "0403019": //Block

                    //    ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    string area = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalaAB")).Text.Trim();
                    //    DataView dv6;
                    //    dv6 = dt1.DefaultView;
                    //    dv6.RowFilter = ("code='" + area + "'");

                    //    ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv6.ToTable();
                    //    ddlgval.DataBind();

                    //    break;
                    case "0403020": //Road

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlblockAB")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlvalpAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[2];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[2];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[2];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0403017": //Area

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalpAB")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403019": //Road

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalaAB")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403020": //Block

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlblockAB")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlvalzAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[2];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[2];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[2];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0403015": //Police Station                    
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalzAB")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403017": //Area

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalpAB")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403019": //Block

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalaAB")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403020": //Road

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlblockAB")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlvaldAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[2];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[2];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[2];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0403013": //Zone

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string district = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvaldAB")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403015": //Police Station                    
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalzAB")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403017": //Area

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalpAB")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403019": //Block

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalaAB")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403020": //Road

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlblockAB")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void ddlvalplotAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ownid = ASTUtility.Right("0" + this.ddlNLandOwner.SelectedValue.ToString(), 2);
            DataSet ds2 = (DataSet)ViewState["tblDatagridLOwnall"];
            DataTable dt = ds2.Tables[2];
            DataView dvo = dt.Copy().DefaultView;
            dvo.RowFilter = "own='" + ownid + "'";
            dt = dvo.ToTable();
            if (dt.Rows.Count == 0)
            {
                dt = ds2.Tables[2];

            }
            if (ds2 == null)
                return;
            //DataTable dt = ds2.Tables[2];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0403011": //District   
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string country = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalplotAB")).Text.Trim();
                        DataView dv2;
                        dv2 = dt1.DefaultView;
                        dv2.RowFilter = ("code='" + country + "'");
                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvaldAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403013": //Zone

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string district = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvaldAB")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalzAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403015": //Police Station                    
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalzAB")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalpAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403017": //Area

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalpAB")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlvalaAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv5.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403019": //Block

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlvalaAB")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlblockAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv6.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0403020": //Road

                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvLOBusns.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string block = ((DropDownList)this.gvLOBusns.Rows[i - 1].FindControl("ddlblockAB")).Text.Trim();
                        DataView dv7;
                        dv7 = dt1.DefaultView;
                        dv7.RowFilter = ("code='" + block + "'");

                        ddlgval = ((DropDownList)this.gvLOBusns.Rows[i].FindControl("ddlpnlrAB"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv7.ToTable();
                        ddlgval.DataBind();

                        break;
                }
            }
        }
        protected void lnkaddok_Click(object sender, EventArgs e)
        {
            this.GetInformation();
            GetYEARLAND();
        }




        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "LANDREFINFODDL", "", "", "", "", "", "", "", "", "");
            ViewState["tblsubddl"] = ds2.Tables[0];
            ViewState["tblstatus"] = ds2.Tables[1];
            ds2.Dispose();
        }

        private void DataBindStatus()
        {
            try
            {

                DataTable dt = ((DataTable)ViewState["tblsubddl"]).Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("gcod like '95%'");
                this.ddlmStatus.DataTextField = "gdesc";
                this.ddlmStatus.DataValueField = "gcod";
                this.ddlmStatus.DataSource = dv.ToTable();
                this.ddlmStatus.DataBind();
            }

            catch (Exception ex)
            {



            }



        }

        private void GETEMPLOYEEUNDERSUPERVISED()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETGENEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblempsup"] = ds1.Tables[0];
            ds1.Dispose();

        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            this.lUpdatPerInfo.Visible = true;
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string comcod = GetComeCode();
            string styleid = ((Label)this.gvSummary.Rows[index].FindControl("lsircode")).Text.ToString();
            string lidno = ((Label)this.gvSummary.Rows[index].FindControl("lsircode1")).Text.ToString();

            lbllandname.Text = ((Label)this.gvSummary.Rows[index].FindControl("lsircode1")).Text.ToString() + ':' + ((Label)this.gvSummary.Rows[index].FindControl("ldesc")).Text.ToString();
            ViewState["sircodegrid"] = styleid;

            this.MultiView1.ActiveViewIndex = 0;
            GetData();
            this.ShowPersonalInformation();
            showplotinformation();
            this.GetPersonalinfo();
            ShowPropDet();
            ShowOther();
            divexland.Visible = true;
            this.lbtPending.Visible = false;
            btnaddland.Text = "Back";
            DataSet ds1 = (DataSet)ViewState["tblDatagridall"];
            this.txtdate.Text = Convert.ToDateTime(ds1.Tables[4].Rows[0]["createdate"]).ToString("dd-MMM-yyyy");


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Edit Land & Owner Info Information (Land CRM)";
                string eventdesc = "Edit Land & Owner Info Information (Land CRM)";
                string eventdesc2 = "Edit Land Id " + lidno;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }



            // this.divNot.Visible = false;
            //this.divSear.Visible = false;

        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblsummData"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComeCode();
            int gridindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //int RowIndex = (this.gvSummary.PageSize * this.gvSummary.PageIndex) + gridindex;
            //string proscod = this.lblproscod.Value.Trim();

            string proscod = ((Label)this.gvSummary.Rows[gridindex].FindControl("lsircode")).Text.Trim();//dt.Rows[RowIndex]["sircode"].ToString();
            bool result = HRData.UpdateXmlTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "DELETEPROSPECT", null, null, null, proscod, userid, Posteddat, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;

            }

            //dt.Rows[RowIndex].Delete();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("sircode<>'" + proscod + "'");
            ViewState.Remove("tblsummData");
            ViewState["tblsummData"] = dv.ToTable();
            this.Data_Bind();

            msg = "Data Deleted Successfully!";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);



            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "delete land Information (Land CRM)";
                string eventdesc = "delete land Information (Land CRM)";
                string eventdesc2 = "";

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }

        }

        protected void lnkbtnRetreive_Click(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }


            DataTable dt = (DataTable)ViewState["tblsummData"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComeCode();
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowno = (this.gvSummary.PageSize) * (this.gvSummary.PageIndex) + RowIndex;
            string proscod = dt.Rows[RowIndex]["sircode"].ToString();
            bool result = HRData.UpdateXmlTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY_RND", "RETREIVE_PROSPECT", null, null, null, proscod, userid, Posteddat, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Prospect Retreive Fail');", true);
                return;

            }

            dt.Rows[rowno].Delete();
            DataView dv = dt.DefaultView;
            Session.Remove("tblsummData");
            ViewState["tblsummData"] = dv.ToTable();
            this.Data_Bind();

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Prospect Retreived Successfully');", true);
        }
        protected void gvSummary_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int index = e.Row.RowIndex;
                Panel Lbtn = (Panel)e.Row.FindControl("pnlfollowup");
                Lbtn.Attributes.Add("onmouseover", "AddButton(" + index + ")");
                Lbtn.Attributes.Add("onmouseout", "HiddenButton(" + index + ")");
                Lbtn.Attributes.Add("style", "cursor:pointer");
                LinkButton Lbtn1 = (LinkButton)e.Row.FindControl("lnkEditfollowup");
                Lbtn1.Attributes.Add("class", "hiddenb" + index);
                Lbtn1.Attributes.Add("style", "display:none");

                LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
                lbtnView.Attributes.Add("class", "hiddenb" + index);
                lbtnView.Attributes.Add("style", "display:none");


                Label ldesc = (Label)e.Row.FindControl("ldesc");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                //  LinkButton lnkAct = (LinkButton)e.Row.FindControl("lnkAct");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string proscod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                string dealcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dealcode")).ToString();
                string Empid = hst["empid"].ToString();
                string landst = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "active"));
                LinkButton lnkAct = (LinkButton)e.Row.FindControl("lnkAct");
                lnkAct.Visible = false;


                if (landst == "False")
                {

                    Lbtn1.Enabled = false;
                    lbtnView.Enabled = false;


                    //Active
                 //   if (landst == "False")
                        if (dealcode == Empid)
                        {
                            lnkAct.Visible = true;
                        }
                        else
                        {
                            lnkAct.Visible = false;
                        }


                }


                if (proscod.Length == 7)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    lnkAct.Visible = false;
                    ldesc.Attributes["style"] = "font-size:13px; font-weight:bold; color:maroon;";
                }














            }
        }


        //protected void btnSearch_Click(object sender, EventArgs e)
        //{

        //    this.ModalDataBind();
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        //}

        private void ModalDataBind()
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            // start
            DataTable dtemp = (DataTable)ViewState["tblempsup"];
            DataView dv;
            dv = dt1.Copy().DefaultView;
            string ddlempid = this.ddlEmpid.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string lempid = hst["empid"].ToString();
            string comcod = this.GetComeCode();
            DataTable dtE = new DataTable();
            dv.RowFilter = ("gcod like '93%'");
            if (userrole == "1")
            {

                dtE = dv.ToTable();
                dtE.Rows.Add("000000000000", "Employee", "");
            }
            else
            {
                DataTable dts = dv.ToTable();
                var query = (from dtl1 in dts.AsEnumerable()
                             join dtl2 in dtemp.AsEnumerable() on dtl1.Field<string>("gcod") equals dtl2.Field<string>("empid")
                             select new
                             {
                                 gcod = dtl1.Field<string>("gcod"),
                                 gdesc = dtl1.Field<string>("gdesc"),
                                 code = dtl1.Field<string>("code")
                             }).ToList();
                dtE = ASITUtility03.ListToDataTable(query);
                if (dtE.Rows.Count >= 2)
                    dtE.Rows.Add("000000000000", "Employee", "");
            }
            // end


            //DataView dv;
            //dv = dt1.Copy().DefaultView;
            //dv.RowFilter = ("gcod like '93%'");
            //DataTable dtE = dv.ToTable();
            //dtE.Rows.Add("000000000000", "Choose Employee..", "");

            this.ddlEmpid.DataTextField = "gdesc";
            this.ddlEmpid.DataValueField = "gcod";
            this.ddlEmpid.DataSource = dtE;
            this.ddlEmpid.DataBind();
            if (dtE.Rows.Count >= 2)
                this.ddlEmpid.SelectedValue = "000000000000";

            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '52%'");
            DataTable dtCo = dv.ToTable();
            dtCo.Rows.Add("0000000", "Country", "");
            this.ddlCountry.DataTextField = "gdesc";
            this.ddlCountry.DataValueField = "gcod";
            this.ddlCountry.DataSource = dtCo;
            this.ddlCountry.DataBind();
            this.ddlCountry.SelectedValue = "5201001";


            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '51%'");
            DataTable dtPr = dv.ToTable();
            dtPr.Rows.Add("0000000", "Priotiry", "");
            this.ddlPri.DataTextField = "gdesc";
            this.ddlPri.DataValueField = "gcod";
            this.ddlPri.DataSource = dtPr;
            this.ddlPri.DataBind();
            this.ddlPri.SelectedValue = "0000000";


            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '95%'");
            DataTable dtSta = dv.ToTable();
            dtSta.Rows.Add("0000000", "Status");
            this.ddlStatus.DataTextField = "gdesc";
            this.ddlStatus.DataValueField = "gcod";
            this.ddlStatus.DataSource = dtSta;
            this.ddlStatus.DataBind();
            this.ddlStatus.SelectedValue = "0000000";

            this.ddlCountry_SelectedIndexChanged(null, null);

        }
        private void GetSearchGridSummary()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string Empid = ((hst["empid"].ToString() == "") ? "" : hst["empid"].ToString()) + "%";
            if (userrole == "1")
            {
                Empid = "%";
            }

            string comcod = this.GetComeCode();
            string srchempid = ((this.ddlEmpid.SelectedValue.ToString() == "000000000000") ? "93" : this.ddlEmpid.SelectedValue.ToString()) + "%";
            string Country = (this.ddlCountry.SelectedValue.ToString() == "0000000") ? "%" : this.ddlCountry.SelectedValue.ToString() + "%";
            string Dist = (this.ddlDist.SelectedValue.ToString() == "0000000") ? "%" : this.ddlDist.SelectedValue.ToString() + "%";
            string Zone = (this.ddlZone.SelectedValue.ToString() == "0000000") ? "%" : this.ddlZone.SelectedValue.ToString() + "%";
            string PStat = (this.ddlPStat.SelectedValue.ToString() == "0000000") ? "%" : this.ddlPStat.SelectedValue.ToString() + "%";
            string Area = (this.ddlArea.SelectedValue.ToString() == "0000000") ? "%" : this.ddlArea.SelectedValue.ToString() + "%";
            string Block = (this.ddlBlock.SelectedValue.ToString() == "0000000") ? "%" : this.ddlBlock.SelectedValue.ToString() + "%";
            string Road = (this.ddlRoad.SelectedValue.ToString() == "0000000") ? "%" : this.ddlRoad.SelectedValue.ToString() + "%";
            string Pri = (this.ddlPri.SelectedValue.ToString() == "0000000") ? "%" : this.ddlPri.SelectedValue.ToString() + "%";
            string Status = (this.ddlStatus.SelectedValue.ToString() == "0000000") ? "%" : this.ddlStatus.SelectedValue.ToString() + "%";
            string Other = this.ddlOther.SelectedValue.ToString();
            string TxtVal = "%" + this.txtVal.Text + "%";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "SP_ENTRY_LANDPROCUREMENT", "LINFOSUM", null, null, null, "8305%", Empid, Country, Dist, Zone, PStat, Area,
                Block, Road, Pri, Status, Other, TxtVal, srchempid);
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            ViewState["tblsummData"] = ds3.Tables[0];
            if (ds3.Tables[0].Rows.Count == 0)
                return;


            lblIntputtype.Value = "Active";

            DataView dv1 = ds3.Tables[0].Copy().DefaultView;
            dv1.RowFilter = ("active='True'");
            this.gvSummary.DataSource = dv1.ToTable();
            this.gvSummary.DataBind();
            this.gvSummary.Columns[14].Visible = false;
            DataView dv = ds3.Tables[0].Copy().DefaultView;
            dv.RowFilter = ("active='False'");
            this.lbtPending.Text = "Pending:" + ((dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows.Count.ToString());

            if (gvSummary.Rows.Count > 0)
            {
                Session["Report1"] = gvSummary;
                ((HyperLink)this.gvSummary.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            //if (ds3.Tables[0].Rows.Count == 0)
            //    return;
            //DataTable dt2 = ds3.Tables[0];
            //ViewState["tblsummData"] = dt2;
            this.Data_Bind();

        }

        protected void lUpdatInfo_Click(object sender, EventArgs e)
        {
            this.GetSearchGridSummary();
            //this.GetNotificationinfo();

        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv;

            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '53%'");
            DataTable dtDis = dv.ToTable();
            // dtDis.Rows.Add("0000000", "Choose District..", "");
            this.ddlDist.DataTextField = "gdesc";
            this.ddlDist.DataValueField = "gcod";
            this.ddlDist.DataSource = dtDis;
            this.ddlDist.DataBind();
            //this.ddlDist.SelectedValue = "0000000";
            this.ddlDist.SelectedValue = "5301001";
            this.ddlDist_SelectedIndexChanged(null, null);
        }
        protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string dis = this.ddlDist.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '54%' and code ='" + dis + "'");
            DataTable dtZone = dv.ToTable();
            dtZone.Rows.Add("0000000", "Zone", "");
            this.ddlZone.DataTextField = "gdesc";
            this.ddlZone.DataValueField = "gcod";
            this.ddlZone.DataSource = dtZone;
            this.ddlZone.DataBind();

            this.ddlZone.SelectedValue = "0000000";
            //if (dtZone.Rows.Count > 1)
            //    this.ddlZone.SelectedIndex = 0;
            this.ddlZone_SelectedIndexChanged(null, null);
        }
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string zone = this.ddlZone.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '55%' and code ='" + zone + "'");
            DataTable dtPO = dv.ToTable();
            dtPO.Rows.Add("0000000", "P.S.", "");
            this.ddlPStat.DataTextField = "gdesc";
            this.ddlPStat.DataValueField = "gcod";
            this.ddlPStat.DataSource = dtPO;
            this.ddlPStat.DataBind();
            this.ddlPStat.SelectedValue = "0000000";

            //if (dtPO.Rows.Count > 1)
            //    this.ddlPStat.SelectedIndex = 0;
            this.ddlPStat_SelectedIndexChanged(null, null);
        }
        protected void ddlPStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string PoSt = this.ddlPStat.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '56%' and code ='" + PoSt + "'");
            DataTable dtArea = dv.ToTable();
            dtArea.Rows.Add("0000000", "Area", "");
            this.ddlArea.DataTextField = "gdesc";
            this.ddlArea.DataValueField = "gcod";
            this.ddlArea.DataSource = dtArea;
            this.ddlArea.DataBind();
            this.ddlArea.SelectedValue = "0000000";

            //if (dtArea.Rows.Count > 1)
            //    this.ddlArea.SelectedIndex = 0;
            this.ddlArea_SelectedIndexChanged(null, null);
        }
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string Area = this.ddlArea.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '57%' and code ='" + Area + "'");
            DataTable dtBlk = dv.ToTable();
            dtBlk.Rows.Add("0000000", "Block", "");
            this.ddlBlock.DataTextField = "gdesc";
            this.ddlBlock.DataValueField = "gcod";
            this.ddlBlock.DataSource = dtBlk;
            this.ddlBlock.DataBind();
            this.ddlBlock.SelectedValue = "0000000";

            //if (dtBlk.Rows.Count > 1)
            //    this.ddlBlock.SelectedIndex = 0;
            this.ddlBlock_SelectedIndexChanged(null, null);
        }
        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string Block = this.ddlBlock.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '58%' and code ='" + Block + "'");
            DataTable dtRoad = dv.ToTable();
            dtRoad.Rows.Add("0000000", "Road", "");
            this.ddlRoad.DataTextField = "gdesc";
            this.ddlRoad.DataValueField = "gcod";
            this.ddlRoad.DataSource = dtRoad;
            this.ddlRoad.DataBind();
            this.ddlRoad.SelectedValue = "0000000";

            //if (dtRoad.Rows.Count > 1)
            //    this.ddlRoad.SelectedIndex = 0;
        }
        protected void lnkAct_Click(object sender, EventArgs e)
        {
            //this.txtComm.Text = "";
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //string cDate = ((Label)this.gvSummary.Rows[index].FindControl("lblgvDate")).Text.ToString().Trim();

            this.lblsircode.Text = ((Label)this.gvSummary.Rows[index].FindControl("lsircode")).Text.ToString().Trim();
            //string disgnote = ((Label)this.gvSummary.Rows[index].FindControl("lblgvdisgnote")).Text.ToString().Trim();

            //this.GetClientData(cDate, discussion, disgnote);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }
        protected void btmodal_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string Procode = this.lblsircode.Text;
            string active = "";

            bool result = HRData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATEACTLAND", Procode, active, userid, Posteddat);

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }

            DataTable dt = ((DataTable)ViewState["tblsummData"]).Copy();
            DataRow[] dr1 = dt.Select("sircode='" + Procode + "'");
            dr1[0]["active"] = true;
            ViewState["tblsummData"] = dt;
            this.Data_Bind();
            //this.GetGridSummary();


            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Update land Information (Land CRM)";
                string eventdesc = "Update land Information (Land CRM)";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }



        }



        protected void lnkEditfollowup_Click(object sender, EventArgs e)
        {




            try
            {



                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                string proscod = ((Label)this.gvSummary.Rows[rowindex].FindControl("lsircode")).Text;
                string gempid = ((Label)this.gvSummary.Rows[rowindex].FindControl("lblgvempid")).Text;
                string flidno = ((Label)this.gvSummary.Rows[rowindex].FindControl("lsircode1")).Text;


                string cdate = this.txtdate.Text.Trim();
                DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "SHOWPRELOWNERDISCUSSION", proscod, cdate, "", "", "", "");

                this.rpclientinfo.DataSource = ds1.Tables[0];
                this.rpclientinfo.DataBind();
                //
                this.lbllowner.InnerText = ds1.Tables[1].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["name"].ToString() : ds1.Tables[1].Rows[0]["name"].ToString();
                //this.lbllowner.InnerText = ds1.Tables[1].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["phone"].ToString() : ds1.Tables[1].Rows[0]["phone"].ToString();
                this.lbllowphone.InnerText = ds1.Tables[1].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["phone"].ToString() : ds1.Tables[1].Rows[0]["phone"].ToString();
                this.lbllandaddress.InnerText = ds1.Tables[1].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["prosdesc"].ToString() : ds1.Tables[1].Rows[0]["prosdesc"].ToString();
                this.lblproscod.Value = ds1.Tables[0].Rows.Count == 0 ? proscod : ds1.Tables[0].Rows[0]["proscod"].ToString();

                this.lbleditempid.Value = gempid;
                this.lbllaststatus.InnerHtml = "Status:" + "<span style='color:#ffef2f; font-size:14px; font-weight:bold'>" + (ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["lastlsdesc"].ToString()) + "</span>";






                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Follow Up (Land CRM) ";
                    string eventdesc = "Follow Up (Land CRM) ";
                    string eventdesc2 = "Follow Up " + flidno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }



                //this.gvclient.DataSource = ds1.Tables[0];
                //this.gvclient.DataBind();
                //ViewState["tblprediscussion"] = ds1.Tables[0];
                //ds1.Dispose();




                //for (int i = 0; i < this.gvclient.Rows.Count; i++)
                //{
                //    string disgnote = ((Label)gvclient.Rows[i].FindControl("lblgvdisgnote")).Text.Trim();
                //    string subgnote = ((Label)gvclient.Rows[i].FindControl("lblgvsubgnote")).Text.Trim();
                //    if (disgnote.Length != 0)
                //    {
                //        this.gvclient.Rows[i].Cells[5].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                //        //gvclient.Columns[9].ItemStyle.BackColor = System.Drawing.Color.FromName("#6EB6C2");
                //    }
                //    if (subgnote.Length != 0)
                //    {
                //        this.gvclient.Rows[i].Cells[9].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                //    }
                //}



            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }
        protected void lbtnView_Click(object sender, EventArgs e)
        {

        }
        protected void Reschdule_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnFollowup_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnComments_Click(object sender, EventArgs e)
        {

        }
        protected void lbtntfollowup_Click(object sender, EventArgs e)
        {
            this.ShowDiscussion();


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);
        }
        protected void lbtnStatus_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnUpdateDiscussiont_Click(object sender, EventArgs e)
        {


            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string comcod = this.GetComeCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString(); 
                // start
                string hempid = this.lbleditempid.Value;
                DataTable dt = ((DataTable)ViewState["tblempsup"]).Copy();

                var query = (from dtl1 in dt.AsEnumerable()
                             where (dtl1.Field<string>("empid") == hempid) || (dtl1.Field<string>("empid") == empid)
                             select dtl1);

                DataTable dtE = query.AsDataView().ToTable();
                if (dtE.Rows.Count == 0)
                {
                    msg = "This prospect is not your under";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

                if (empid.Length == 0)
                {

                    msg = "Employee is not exixted";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;

                }
                // end



                string Client = this.lblproscod.Value.ToString();

                string kpigrp = "000000000000";

                string wrkdpt = "000000000000";

                DateTime time = System.DateTime.Now;


                //string cdate = this.txtFrom.Text.ToString() +" "+ time.ToString("HH:mm");

                string cdate = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[0].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");



                string Gvalue = "";
                bool result;
                for (int i = 0; i < this.gvInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvInfo.Rows[i].FindControl("lblgvItmCodedis")).Text.Trim();
                    string gtype = ((Label)this.gvInfo.Rows[i].FindControl("lgvgvaldis")).Text.Trim();
                    string remarks = "";




                    if (Gcode == "810100102002")
                    {


                        //Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                        //    : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();



                        foreach (ListItem chkfollow in ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items)
                        {


                            if (chkfollow.Selected)
                            {
                                Gvalue += chkfollow.Value;

                            }

                        }
                    }


                    else if (Gcode == "810100102019")
                    {
                        //Commented on 05-Feb-2023
                        //Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                        //    : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();

                        //For Multiple Next Followup
                        foreach (ListItem chkfollow in ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items)
                        {
                            if (chkfollow.Selected)
                            {
                                Gvalue += chkfollow.Value;
                            }
                        }
                    }



                    else if (Gcode == "810100102016")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).SelectedValue.ToString();
                    }


                    else if (Gcode == "810100102018")
                    {

                        //Gvalue == "";
                        foreach (ListItem item in ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis")).Items)
                        {
                            if (item.Selected)
                            {

                                if (item.Selected)
                                {
                                    Gvalue += item.Value;
                                    remarks = remarks + item.Text + ", ";
                                }
                            }
                        }



                        // }
                        // }

                        remarks = (remarks.Length == 0) ? "" : remarks.Substring(0, remarks.Length - 2);


                    }




                    else if (Gcode == "810100102001")
                    {

                        //string fdatetime = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim()+ " " + ddlhour+":" + ddlMmin +" "+ ddlslb)).ToString("dd-MMM-yyyy HH:mm:ss");

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                            : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    else if (Gcode == "810100102020")
                    {
                        string sdsd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim();

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() == "") ? "1900-01-01 00:00:00"
                           : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    else
                    {

                        Gvalue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;




                    if (Gvalue != "")
                    {
                        result = HRData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "INSERTUPDATESCDINF", empid, Client, kpigrp, "", wrkdpt, cdate, Gcode, gtype, Gvalue, remarks);
                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }
                        Gvalue = "";
                    }


                }
                // For Update
                this.GetNotificationinfo();
                string rptype = this.hdnfrpttype.Value.ToString();
                if (rptype.Length > 0)
                {
                    this.ShowDetNotification(this.hdnfrpttype.Value.ToString());
                }
                else
                {
                    this.GetGridSummary();

                }

                msg = "Discussion Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);
                this.clearModalField(); // clear modal 


                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Update Discuss Information (Land CRM)";
                    string eventdesc = "Show Discuss Information (Land CRM)";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }


                //this.ShowData();


            }
            catch (Exception ex)
            {
                msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
            }

        }

        protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvdVal = (TextBox)e.Row.FindControl("txtgvdVal");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();


                if (this.Request.QueryString["Type"].ToString() == "Edit")
                {

                    if (code == "810100102001")
                    {

                        txtgvdVal.Enabled = false;

                    }
                }


            }
        }


        private void ShowDiscussion()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("yyyyMM");
            string Empid = hst["empid"].ToString();
            //string grpcode = this.lblgrp.Text;
            string Client = this.lblproscod.Value.ToString();
            string kpigrp = "000000000000";
            string wrkdpt = "000000000000";
            DateTime time = System.DateTime.Now;

            string cdate = this.txtdate.Text + " " + time.ToString("HH:mm");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", Empid, Client, kpigrp, "", wrkdpt, cdate);
            if (ds1 == null)
                return;
            ViewState["tbModalData"] = ds1.Tables[0];
            this.Modal_Data_Bind();


        }



        private void Modal_Data_Bind()
        {
            string comcod = this.GetComeCode();
            try
            {
                DataTable dt = (DataTable)ViewState["tbModalData"];
                //if(comcod=="3348")
                //{
                //    DataView dvm = dt.DefaultView;
                //    dvm.RowFilter = ("gcod<>'810100102010'");
                //    this.gvInfo.DataSource = dvm.ToTable();
                //    this.gvInfo.DataBind();
                //}
                //else
                //{
                //    this.gvInfo.DataSource = dt;
                //    this.gvInfo.DataBind();
                //}

                this.gvInfo.DataSource = dt;
                this.gvInfo.DataBind();


                this.GetFollow();
                DataTable dt5 = ((DataTable)ViewState["tblFollow"]).Copy(); ;
                DataView dv1;
                dv1 = dt5.DefaultView;
                dv1.RowFilter = ("gcod like '96%'");

                this.GetParcipants();
                DataTable dt6 = (DataTable)ViewState["tblparti"];

                //GetVisitoraStatinfo();
                DataView dv;
                DataTable dtvs = ((DataTable)ViewState["tblFollow"]).Copy();


                //  Status
                dv = dtvs.DefaultView;
                dv.RowFilter = ("gcod like '95%'");

                //DataTable dts = dv.ToTable();
                //dts.Rows.Add("0000", "0000000", "----Select Status----");
                //dts.DefaultView.Sort = "gcod asc";
                //dts = dts.DefaultView.ToTable();
                //DataView dv1;


                //DataView dv1;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString()) + "%";

                DropDownList ddlgval1, ddlgval2, ddlgval3;
                ListBox ddlPartic;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string Gcode = dt.Rows[i]["gcod"].ToString();

                    switch (Gcode)
                    {



                        case "810100102001": //Followup Date

                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = true;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                            DateTime datetime = System.DateTime.Now;

                            string gTime = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                            ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                            ddlgval1.SelectedValue = (gTime.Length == 0) ? datetime.ToString("hh") : ASTUtility.Left(gTime, 2);
                            ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                            ddlgval2.SelectedValue = (gTime.Length == 0) ? datetime.ToString("mm") : gTime.Substring(3, 2);
                            ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                            ddlgval3.SelectedValue = (gTime.Length == 0) ? datetime.ToString("tt") : ASTUtility.Right(gTime, 2);


                            //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                            break;

                        case "810100102002":
                        case "810100102019"://Follow

                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;
                            //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;                        
                            //ChkBoxLstFollow = ((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow"));
                            //ChkBoxLstFollow.DataTextField = "gdesc";
                            //ChkBoxLstFollow.DataValueField = "gcod";
                            //ChkBoxLstFollow.DataSource = dt5;
                            //ChkBoxLstFollow.DataBind();
                            //ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();


                            ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;
                            CheckBoxList ChkBoxLstFollow = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow"));
                            ChkBoxLstFollow.DataTextField = "gdesc";
                            ChkBoxLstFollow.DataValueField = "gcod";
                            ChkBoxLstFollow.DataSource = dv1.ToTable();
                            ChkBoxLstFollow.DataBind();
                            ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();

                            break;





                        case "810100102016": //Status

                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = true;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                            CheckBoxList ChkBoxLstStatus = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus"));
                            ChkBoxLstStatus.DataTextField = "gdesc";
                            ChkBoxLstStatus.DataValueField = "gcod";
                            ChkBoxLstStatus.DataSource = dv.ToTable();
                            ChkBoxLstStatus.DataBind();
                            ChkBoxLstStatus.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                            break;

                        case "810100102018": //PARTICIPANTS  

                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = true;
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                            ddlPartic = ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis"));
                            ddlPartic.DataTextField = "gdesc";
                            ddlPartic.DataValueField = "gcod";
                            ddlPartic.DataSource = dt6;
                            ddlPartic.DataBind();
                            if (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim() != "")
                            {
                                ddlPartic.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                            }
                            int count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());
                            if (count == 0)
                            {


                                if (empid.Length == 13)
                                {

                                    empid = empid.Replace("%", "");
                                    ddlPartic.SelectedValue = empid;


                                }
                                //string empid1 = empid.Substring(0, 12);
                                //int index = 0;
                                //DataRow[] rows = dt6.Select("gcod='" + empid1 + "'");

                                //if (rows.Length > 0)
                                //{
                                //    index = Convert.ToInt32(dt6.Rows.IndexOf(rows[0]));
                                //}
                                //ddlPartic.SelectedIndex = index;
                            }


                            int j;
                            int k = 0;
                            string data = "";
                            for (j = 0; j < count / 12; j++)
                            {
                                data = dt.Rows[i]["gdesc1"].ToString().Substring(k, 12);
                                foreach (ListItem item in ddlPartic.Items)
                                {
                                    if (item.Value == data)
                                    {
                                        item.Selected = true;
                                    }

                                }
                                k = k + 12;
                            }


                            break;



                        case "810100102015":
                        case "810100102025"://Muliline
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).TextMode = TextBoxMode.MultiLine;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Rows = 3;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                            //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Clear();
                            //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = false;

                            TextBox sd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis"));
                            sd.Style.Add("background", "#DFF0D8");
                            sd.Style.Add("width", "100%");


                            //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                            break;

                        case "810100102020": //next Followup date

                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = true;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = true;


                            string gTime20 = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                            ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                            ddlgval1.SelectedValue = (gTime20.Length == 0) ? "" : ASTUtility.Left(gTime20, 2);
                            ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                            ddlgval2.SelectedValue = (gTime20.Length == 0) ? "" : gTime20.Substring(3, 2);
                            ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                            ddlgval3.SelectedValue = (gTime20.Length == 0) ? "" : ASTUtility.Right(gTime20, 2);

                            break;

                        //case "810100102001": //
                        //    ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        //    ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;


                        //    //string gTime = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                        //    //ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                        //    //ddlgval1.SelectedValue = ASTUtility.Left(gTime,2);
                        //    //ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                        //    //ddlgval2.SelectedValue = gTime.Substring(3,2);
                        //    //ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                        //    //ddlgval3.SelectedValue = ASTUtility.Right(gTime,2);


                        //    break;

                        default:
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis")).Items.Clear();
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

                            break;

                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }


        private void GetFollow()
        {
            ViewState.Remove("tblFollow");
            string comcod = this.GetComeCode();
            DataSet dt11 = this.HRData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "FOLLOWUPCODE", "", "", "", "", "", "", "", "", "");
            DataTable dt = dt11.Tables[0];
            ViewState["tblFollow"] = dt;

        }

        private void GetParcipants()
        {
            ViewState.Remove("tblparti");
            string comcod = GetComeCode();
            DataSet ds1 = this.HRData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "PARTICIPANTS", "", "", "", "", "", "", "", "", "");
            DataTable dt11 = ds1.Tables[0];
            ViewState["tblparti"] = dt11;

        }



        private void ShowDetNotification(string rtype)
        {
            this.lblIntputtype.Value = "";
            this.hdnlblattribute.Value = "";
            this.lbltodatekpi.Visible = false;
            this.txtkpitodate.Visible = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            string empid = (hst["empid"].ToString() == "" ? "93%" : hst["empid"].ToString());
            if (userrole == "1")
            {
                empid = "%";
            }

            //string empid = (ddlempid == "000000000000" ? ((hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString())) : ddlempid) + "%";
            string date = this.txtdate.Text.Trim();
            string fempid = (this.ddlEmpid.SelectedValue.ToString() == "000000000000" ? "93" : this.ddlEmpid.SelectedValue.ToString()) + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETNOTIFICATIONDETAILS", "8305%", empid, rtype, date, fempid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ViewState["tblsummData"] = ds1.Tables[0];
                this.Data_Bind();
                ds1.Dispose();

            }
            else
            {
                DataTable dt = new DataTable();
                this.gvSummary.DataSource = dt;
                this.gvSummary.Width = 900;
                this.gvSummary.DataBind();
            }

            if (rtype == "databank")
            {
                //Prospect Retreive Button
                this.gvSummary.Columns[17].Visible = true;
            }
            else
            {
                this.gvSummary.Columns[17].Visible = false;
            }

            this.gvkpi.DataSource = null;
            this.gvkpi.DataBind();
            this.gvkpidet.DataSource = null;
            this.gvkpidet.DataBind();
            this.lblkpiDetails.Visible = false;
            //ViewState["tblsummData"] = ds1.Tables[0];
            //this.gvSummary.DataSource = ds1.Tables[0];
            //this.gvSummary.DataBind();
            //ds1.Dispose();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Show DWR Information";
                string eventdesc = "Show  DWR Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        public bool ShowComments(string rtype)
        {
            ProcessAccess JData = new ProcessAccess();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string type = rtype;
            string comcod = this.GetComeCode();
            string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString()) + "%";
            string date = this.txtdate.Text.Trim();
            string fempid = (this.ddlEmpid.SelectedValue.ToString() == "000000000000" ? "93" : this.ddlEmpid.SelectedValue.ToString()) + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETNOTIFICATIONDETAILS", "8305%", empid, rtype, date, fempid);
            DataTable dt = new DataTable();
            if (ds1.Tables[0].Rows.Count != 0)
            {
                ViewState["tblComData"] = ds1.Tables[0];
                dt = ds1.Tables[0];
                Session["tblCommentData"] = dt;
                this.gvComments.DataSource = ds1.Tables[0];
                this.gvComments.DataBind();
                //this.gvSummary.Visible = false;
            }

            this.gvkpi.DataSource = null;
            this.gvkpi.DataBind();

            this.gvkpidet.DataSource = null;
            this.gvkpidet.DataBind();
            this.lblkpiDetails.Visible = false;
            return true;

        }



        protected void lnkCall_Click(object sender, EventArgs e)
        {
            hdnfrpttype.Value = "call";
            //string rtype = "dws";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Call Information (Land CRM)";
                string eventdesc = "Show Call Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }






        }
        protected void lnkbtnDws_Click(object sender, EventArgs e)
        {
            hdnfrpttype.Value = "dws";
            //string rtype = "dws";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());
        }
        protected void lnkbtnDwr_Click(object sender, EventArgs e)
        {

            hdnfrpttype.Value = "dwr";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show daily Work Schedule Information (Land CRM)";
                string eventdesc = "Show daily Work Schedule Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lnkbtnDayPassed_Click(object sender, EventArgs e)
        {
            hdnfrpttype.Value = "daypassed";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show daypass Information (Land CRM)";
                string eventdesc = "Show daypass Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lnkbtnComment_Click(object sender, EventArgs e)
        {
            //string rtype = "comments";
            //this.ShowDetNotification(rtype);
            string rtype = "comments";
            if (ShowComments(rtype))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openComModal();", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show comment Information (Land CRM)";
                string eventdesc = "Show comment Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void lnkbtnFreezland_Click(object sender, EventArgs e)
        {


            hdnfrpttype.Value = "freezing";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Freezing Information (Land CRM)";
                string eventdesc = "Show Freezing Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void lnkbtnDead_Click(object sender, EventArgs e)
        {
            hdnfrpttype.Value = "deadl";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Dead Pros Information (Land CRM)";
                string eventdesc = "Show Dead Pros Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lbtnSigned_Click(object sender, EventArgs e)
        {

            hdnfrpttype.Value = "signed";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Signed Information (Land CRM)";
                string eventdesc = "Show Signed Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        protected void lnkBtnDatablank_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            hdnfrpttype.Value = "databank";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());           
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Data Bank Information (Land CRM)";
                string eventdesc = "Show Data Bank Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lnkbtnProposal_Click(object sender, EventArgs e)
        {

            hdnfrpttype.Value = "pro";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show proposal Information (Land CRM)";
                string eventdesc = "Show proposal Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lnkbtnLome_Click(object sender, EventArgs e)
        {

            hdnfrpttype.Value = "lome";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Land meeting External Information (Land CRM)";
                string eventdesc = "Show Land meeting External Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }



        }
        protected void lnkbtnLomi_Click(object sender, EventArgs e)
        {

            hdnfrpttype.Value = "lomi";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Meeting Internal Information (Land CRM)";
                string eventdesc = "Show Meeting Internal Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void lnkbtnServey_Click(object sender, EventArgs e)
        {
            hdnfrpttype.Value = "survey";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());


        }
        protected void lnkbtnOther_Click(object sender, EventArgs e)
        {
            hdnfrpttype.Value = "Others";
            this.ShowDetNotification(this.hdnfrpttype.Value.ToString());


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Other Information (Land CRM)";
                string eventdesc = "Show Other Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }
        protected void lnkbtnReturn_Click(object sender, EventArgs e)
        {
            //this.ddlEmpid.SelectedValue = "000000000000";
            // this.ddlZone.SelectedValue = "0000000";
            // this.ddlZone_SelectedIndexChanged(null, null);
            this.ddlOther.SelectedIndex = 0;
            this.gvkpi.DataSource = null;
            this.gvkpi.DataBind();
            this.gvkpidet.DataSource = null;
            this.gvkpidet.DataBind();
            this.lblkpiDetails.Visible = false;
            this.lbltodatekpi.Visible = false;
            this.txtkpitodate.Visible = false;
            this.hdnlblattribute.Value = "";
            this.hdnfrpttype.Value = "";
            this.txtVal.Text = "";
            this.GetGridSummary();
            this.GetNotificationinfo();


        }
        protected void lnkbtnkpi_Click(object sender, EventArgs e)
        {


            this.lbltodatekpi.Visible = true;
            this.txtkpitodate.Visible = true;
            this.hdnlblattribute.Value = "Kpi";
            this.EmpMonthlyKPI();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Show kpi Information (Land CRM)";
                string eventdesc = "Show kpi Information (Land CRM)";
                string eventdesc2 = "";
                string comcod = this.GetComeCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }





        }
        private void EmpMonthlyKPI()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();
            string userrole = hst["userrole"].ToString();

            string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString());
            if (userrole == "1")
            {
                empid = "%";
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTMONTHLYKPI", "8305%", frmdate, todate, empid);
            if(ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            ViewState["tbltempdt"] = ds1.Tables[0];
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpi.DataSource = ds1.Tables[0];
            this.gvkpi.DataBind();
            this.footerCalculations();

        }


        private void footerCalculations()
        {
            DataTable dt1 = (DataTable)ViewState["tbltempdt"];
            if (dt1.Rows.Count == 0)
                return;

            if (dt1.Rows.Count > 0)
            {
                Session["Report1"] = gvkpi;
                ((HyperLink)this.gvkpi.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }



            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFcall")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(call)", "")) ?
                    0.00 : dt1.Compute("sum(call)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFexmeeting")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(extmeeting)", "")) ?
            0.00 : dt1.Compute("sum(extmeeting)", ""))).ToString("#,##0;(#,##0);-");


            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFintmeeting")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(intmeeting)", "")) ?
                    0.00 : dt1.Compute("sum(intmeeting)", ""))).ToString("#,##0;(#,##0);-");


            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFsurvey")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(survey)", "")) ?
               0.00 : dt1.Compute("sum(survey)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFfeasibility")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(feasibility)", "")) ?
                    0.00 : dt1.Compute("sum(feasibility)", ""))).ToString("#,##0;(#,##0);-");



            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFproposal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(proposal)", "")) ?
               0.00 : dt1.Compute("sum(proposal)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFleads")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(leads)", "")) ?
                0.00 : dt1.Compute("sum(leads)", ""))).ToString("#,##0;(#,##0);-");
            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFothers")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(others)", "")) ?
              0.00 : dt1.Compute("sum(others)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFclosing")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(close)", "")) ?
              0.00 : dt1.Compute("sum(close)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(total)", "")) ?
                0.00 : dt1.Compute("sum(total)", ""))).ToString("#,##0;(#,##0);-");


        }
        protected void btnSaveComments_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblCommentData"];

            for (int i = 0; i < this.gvComments.Rows.Count; i++)
            {
                string sircode = ((Label)gvComments.Rows[i].FindControl("lblcomsircode1")).Text.ToString();
                string generated = ((Label)gvComments.Rows[i].FindControl("lblcomgenerated")).Text.ToString(); ;
                string prospect = ((Label)gvComments.Rows[i].FindControl("lblcomdesc")).Text.ToString();
                string prefdesc = ((Label)gvComments.Rows[i].FindControl("lblcomprefdesc")).Text.ToString();
                string associate = ((Label)gvComments.Rows[i].FindControl("lblcomassoc")).Text.ToString();
                string teamlead = ((Label)gvComments.Rows[i].FindControl("lblcomTeam")).Text.ToString();
                string comment = ((Label)gvComments.Rows[i].FindControl("lblComments")).Text.ToString();
                CheckBox chk = ((CheckBox)gvComments.Rows[i].FindControl("chkCommentView"));

                string checkstatus = (chk.Checked == true) ? "True" : "False";
                //string checkstatu1s = (((CheckBox)gvEmployeeInfo.Rows[i].FindControl("CheckPermission")).Checked) ? "True" : "False";

                string sirc = "";
                if (sircode.Length > 6)
                {
                    sirc = sircode.Substring(sircode.Length - 6);
                }
                dt1.Rows[i]["sircode1"] = sirc;
                dt1.Rows[i]["generated"] = generated;
                dt1.Rows[i]["ownname"] = prospect;
                dt1.Rows[i]["sirdesc"] = prefdesc;
                dt1.Rows[i]["assoc"] = associate;
                dt1.Rows[i]["dealname"] = teamlead;
                dt1.Rows[i]["gnote"] = comment;
                dt1.Rows[i]["comview"] = checkstatus;

            }
            Session["tblCommentData"] = dt1;


            if (updateCommentView())
            {
                msg = "Comment View Successfully!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeComModal();", true);
                this.GetNotificationinfo();
            }
        }


        private bool updateCommentView()
        {
            DataTable dt1 = (DataTable)Session["tblCommentData"];
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("comview=true");
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dv.ToTable());
            ds1.Tables[0].TableName = "tbl1";
            string comcod = this.GetComeCode();


            bool result = HRData.UpdateXmlTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "UPDATECOMMENTVIEW", ds1, null, null, "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                
            }

            return true;
        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((System.Web.UI.WebControls.CheckBox)this.gvComments.HeaderRow.FindControl("chkall")).Checked)
            {
                for (i = 0; i < this.gvComments.Rows.Count; i++)
                {
                    ((System.Web.UI.WebControls.CheckBox)this.gvComments.Rows[i].FindControl("chkCommentView")).Checked = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openComModal();", true);

                }
            }
            else
            {
                for (i = 0; i < this.gvComments.Rows.Count; i++)
                {
                    //((CheckBox)this.gvEmployeeInfo.Rows[i].FindControl("chkPermission")).Enabled == true
                    if (((Label)gvComments.Rows[i].FindControl("lblgvPerm")).Text.ToString() == "True")
                    {
                        ((System.Web.UI.WebControls.CheckBox)this.gvComments.Rows[i].FindControl("chkCommentView")).Checked = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeComModal();", true);

                        //this.lblgvdeptandemployeeemp_Click(null, null);
                    }
                }
            }
        }

        private void clearModalField()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString();

                foreach (GridViewRow gvr1 in gvInfo.Rows)
                {

                    string gcode = ((Label)gvr1.FindControl("lblgvItmCodedis")).Text.Trim();
                    switch (gcode)
                    {

                        case "810100102018": //PARTICIPANTS  
                            ListBox ddlPartic = ((ListBox)gvr1.FindControl("ddlParticdis"));
                            if (empid.Trim().Length > 0)
                                ddlPartic.SelectedValue = empid;
                            break;

                        default:
                            ((CheckBoxList)gvr1.FindControl("ChkBoxLstFollow")).SelectedValue = null;
                            ((CheckBoxList)gvr1.FindControl("ChkBoxLstStatus")).SelectedValue = null;
                            ((TextBox)gvr1.FindControl("txtgvValdis")).Text = "";
                            break;
                    }


                }
            }

            catch (Exception ex)
            {
                msg = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);

            }



        }


        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid = this.ddlEmpid.SelectedValue.ToString();
            if (empid != "000000000000")
                this.GetSearchGridSummary();
            this.GetNotificationByEmployee(empid);
        }
        protected void gvSummary_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            this.gvSummary.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private DataSet getDuplicatePhone(string comcod, string Phone)
        {
            DataSet dsp = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "CHECKDUPLICATEPHONE", Phone, "", "", "", "", "", "", "", "");

            if (dsp.Tables[0].Rows.Count != 0)
            {
                string pid = dsp.Tables[0].Rows[0]["pid"].ToString();
                string sirdesc = dsp.Tables[0].Rows[0]["username"].ToString();
                string supervisor = dsp.Tables[0].Rows[0]["leadname"].ToString();
                string phone = dsp.Tables[0].Rows[0]["phone"].ToString();
                string Message = "Duplicate : ";
                string totmsg = Message + phone + ", " + pid + ", Associate : " + sirdesc + ", Team :  " + supervisor;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + totmsg + "');", true);
               
            }
            return dsp;
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();

        }
        protected void lblgvkpitotal_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);

            ViewState["tbltempdt"] = ds1.Tables[0];
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = ds1.Tables[0];
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvkpidet_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int index = e.Row.RowIndex;
                Panel Lbtn = (Panel)e.Row.FindControl("pnlfollowupkpisum");
                Lbtn.Attributes.Add("onmouseover", "AddButton(" + index + ")");
                Lbtn.Attributes.Add("onmouseout", "HiddenButton(" + index + ")");
                Lbtn.Attributes.Add("style", "cursor:pointer");
                LinkButton Lbtn1 = (LinkButton)e.Row.FindControl("lnkEditfollowupkpisum");
                Lbtn1.Attributes.Add("class", "hiddenb" + index);
                Lbtn1.Attributes.Add("style", "display:none");

                LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnViewkpisum");
                lbtnView.Attributes.Add("class", "hiddenb" + index);
                lbtnView.Attributes.Add("style", "display:none");


                Label ldesc = (Label)e.Row.FindControl("ldesckpisum");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDeletekpisum");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEditkpisum");
                //  LinkButton lnkAct = (LinkButton)e.Row.FindControl("lnkAct");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string proscod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                string dealcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dealcode")).ToString();
                string Empid = hst["empid"].ToString();




                if (proscod.Length == 7)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    ldesc.Attributes["style"] = "font-size:13px; font-weight:bold; color:maroon;";
                }



            }

        }
        protected void lnkDeletekpisum_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnViewkpisum_Click(object sender, EventArgs e)
        {

        }
        protected void lnkEditfollowupkpisum_Click(object sender, EventArgs e)
        {

        }
        protected void lnkEditkpisum_Click(object sender, EventArgs e)
        {

        }


        protected void rpclientinfo_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("yyyyMM");
            string Empid = hst["empid"].ToString();
            //string grpcode = this.lblgrp.Text;
            string Client = this.lblproscod.Value.ToString();
            string kpigrp = "000000000000";
            string wrkdpt = "000000000000";
            DateTime time = System.DateTime.Now;



            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //    DataRowView dr = (DataRowView)e.Item.DataItem;

                //    string rownum = Convert.ToString(dr["rownum"]);
                //    string cdate = Convert.ToString(dr["cdate"]);
                //    string proscod = Convert.ToString(dr["proscod"]);
                //    string summery = Convert.ToString(dr["discus"]);
                //    string subj = Convert.ToString(dr["ndissub"]);
                //    string cStaus = Convert.ToString(dr["nfollowup"]);

                //    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", Empid, Client, kpigrp, "", wrkdpt, cdate);
                //    // ViewState["tbModalData"] = ds1.Tables[0];

                //    Label asd = (Label)e.Item.FindControl("lblTx_") as Label;


                //    DataTable dt = ds1.Tables[0];
                //    GridView gv = e.Item.FindControl("Schedule_") as GridView;

                //    gv.DataSource = ds1;
                //    gv.DataBind();
                //    DataTable dt6 = (DataTable)ViewState["tblparti"];
                //    //GetVisitoraStatinfo();
                //    DataView dv;
                //    DataTable dtvs = ((DataTable)ViewState["tblFollow"]).Copy();
                //    //  Status
                //    dv = dtvs.DefaultView;
                //    dv.RowFilter = ("gcod like '95%'");

                //    DataTable dt5 = ((DataTable)ViewState["tblFollow"]).Copy(); ;
                //    DataView dv1;
                //    dv1 = dt5.DefaultView;
                //    dv1.RowFilter = ("gcod like '96%'");


                //    string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString()) + "%";

                //    DropDownList ddlgval1, ddlgval2, ddlgval3;
                //    ListBox ddlPartic;

                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {

                //        string Gcode = dt.Rows[i]["gcod"].ToString();

                //        switch (Gcode)
                //        {
                //            case "810100102001": //Followup Date

                //                ((TextBox)gv.Rows[i].FindControl("txtgvdValdis")).Visible = true;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlParicdis")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlFollow")).Visible = true;
                //                ((Label)gv.Rows[i].FindControl("lblgvTime")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlTime")).Visible = true;
                //                ((Label)gv.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                //                DateTime datetime = System.DateTime.Now;

                //                string gTime = ((Label)gv.Rows[i].FindControl("lblgvTime")).Text.Trim();

                //                ddlgval1 = ((DropDownList)gv.Rows[i].FindControl("ddlhourSH"));
                //                ddlgval1.SelectedValue = (gTime.Length == 0) ? datetime.ToString("hh") : ASTUtility.Left(gTime, 2);
                //                ddlgval2 = ((DropDownList)gv.Rows[i].FindControl("ddlMminSH"));
                //                ddlgval2.SelectedValue = (gTime.Length == 0) ? datetime.ToString("mm") : gTime.Substring(3, 2);
                //                ddlgval3 = ((DropDownList)gv.Rows[i].FindControl("ddlslbSH"));
                //                ddlgval3.SelectedValue = (gTime.Length == 0) ? datetime.ToString("tt") : ASTUtility.Right(gTime, 2);


                //                //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                //                break;

                //            case "810100102002":
                //            case "810100102019"://Follow

                //                ((TextBox)gv.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlParicdis")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlFollow")).Visible = true;
                //                ((Label)gv.Rows[i].FindControl("lblgvTime")).Visible = false;
                //                ((Label)gv.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                //                ((CheckBoxList)gv.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;
                //                CheckBoxList ChkBoxLstFollow = ((CheckBoxList)gv.Rows[i].FindControl("ChkBoxLstFollow"));
                //                ChkBoxLstFollow.DataTextField = "gdesc";
                //                ChkBoxLstFollow.DataValueField = "gcod";
                //                ChkBoxLstFollow.DataSource = dv1.ToTable();
                //                ChkBoxLstFollow.DataBind();
                //                ChkBoxLstFollow.SelectedValue = ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Text.Trim();

                //                break;





                //            case "810100102016": //Status

                //                ((Panel)gv.Rows[i].FindControl("pnlStatus")).Visible = true;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlParicdis")).Visible = false;
                //                ((Label)gv.Rows[i].FindControl("lblgvTime")).Visible = false;
                //                ((CheckBoxList)gv.Rows[i].FindControl("ChkBoxLstStatus")).Visible = true;
                //                ((Label)gv.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                //                CheckBoxList ChkBoxLstStatus = ((CheckBoxList)gv.Rows[i].FindControl("ChkBoxLstStatus"));
                //                ChkBoxLstStatus.DataTextField = "gdesc";
                //                ChkBoxLstStatus.DataValueField = "gcod";
                //                ChkBoxLstStatus.DataSource = dv.ToTable();
                //                ChkBoxLstStatus.DataBind();
                //                ChkBoxLstStatus.SelectedValue = cStaus.ToString();
                //                break;

                //            case "810100102018": //PARTICIPANTS  

                //                ((Panel)gv.Rows[i].FindControl("pnlFollow")).Visible = false;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlParicdis")).Visible = true;
                //                ((ListBox)gv.Rows[i].FindControl("ddlParticdis")).Visible = true;
                //                ((Label)gv.Rows[i].FindControl("lblgvTime")).Visible = false;
                //                ((Label)gv.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                //                ddlPartic = ((ListBox)gv.Rows[i].FindControl("ddlParticdis"));
                //                ddlPartic.DataTextField = "gdesc";
                //                ddlPartic.DataValueField = "gcod";
                //                ddlPartic.DataSource = dt6;
                //                ddlPartic.DataBind();
                //                if (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim() != "")
                //                {
                //                    ddlPartic.SelectedValue = ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                //                }
                //                int count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());
                //                if (count == 0)
                //                {


                //                    if (empid.Length == 13)
                //                    {

                //                        empid = empid.Replace("%", "");
                //                        ddlPartic.SelectedValue = empid;


                //                    }
                //                    //string empid1 = empid.Substring(0, 12);
                //                    //int index = 0;
                //                    //DataRow[] rows = dt6.Select("gcod='" + empid1 + "'");

                //                    //if (rows.Length > 0)
                //                    //{
                //                    //    index = Convert.ToInt32(dt6.Rows.IndexOf(rows[0]));
                //                    //}
                //                    //ddlPartic.SelectedIndex = index;
                //                }


                //                int j;
                //                int k = 0;
                //                string data = "";
                //                for (j = 0; j < count / 12; j++)
                //                {
                //                    data = dt.Rows[i]["gdesc1"].ToString().Substring(k, 12);
                //                    foreach (ListItem item in ddlPartic.Items)
                //                    {
                //                        if (item.Value == data)
                //                        {
                //                            item.Selected = true;
                //                        }

                //                    }
                //                    k = k + 12;
                //                }


                //                break;



                //            case "810100102015":

                //                ((TextBox)gv.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).TextMode = TextBoxMode.MultiLine;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Rows = 3;
                //                ((Panel)gv.Rows[i].FindControl("pnlFollow")).Visible = false;
                //                ((Label)gv.Rows[i].FindControl("lblgvTime")).Visible = false;
                //                ((Label)gv.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                //                TextBox sd = ((TextBox)gv.Rows[i].FindControl("txtgvValdis"));
                //                sd.Style.Add("background", "#DFF0D8");
                //                sd.Style.Add("width", "100%");
                //                sd.Text = summery;
                //                break;

                //            case "810100102025"://Muliline
                //                ((TextBox)gv.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).TextMode = TextBoxMode.MultiLine;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Rows = 3;
                //                ((Panel)gv.Rows[i].FindControl("pnlFollow")).Visible = false;
                //                ((Label)gv.Rows[i].FindControl("lblgvTime")).Visible = false;
                //                ((Label)gv.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                //                TextBox ssd = ((TextBox)gv.Rows[i].FindControl("txtgvValdis"));
                //                ssd.Style.Add("background", "#DFF0D8");
                //                ssd.Style.Add("width", "100%");
                //                ssd.Text = subj;
                //                break;

                //            case "810100102020": //next Followup date

                //                ((TextBox)gv.Rows[i].FindControl("txtgvdValdis")).Visible = true;
                //                ((TextBox)gv.Rows[i].FindControl("txtgvValdis")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlParicdis")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlFollow")).Visible = true;
                //                ((Label)gv.Rows[i].FindControl("lblgvTime")).Visible = false;
                //                ((Panel)gv.Rows[i].FindControl("pnlTime")).Visible = true;
                //                ((Label)gv.Rows[i].FindControl("lblschedulenumber")).Visible = true;


                //                string gTime20 = ((Label)gv.Rows[i].FindControl("lblgvTime")).Text.Trim();

                //                ddlgval1 = ((DropDownList)gv.Rows[i].FindControl("ddlhourSH"));
                //                ddlgval1.SelectedValue = (gTime20.Length == 0) ? "" : ASTUtility.Left(gTime20, 2);
                //                ddlgval2 = ((DropDownList)gv.Rows[i].FindControl("ddlMminSH"));
                //                ddlgval2.SelectedValue = (gTime20.Length == 0) ? "" : gTime20.Substring(3, 2);
                //                ddlgval3 = ((DropDownList)gv.Rows[i].FindControl("ddlslbSH"));
                //                ddlgval3.SelectedValue = (gTime20.Length == 0) ? "" : ASTUtility.Right(gTime20, 2);

                //                break;



                //            default:
                //                ((TextBox)gv.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                //                ((ListBox)gv.Rows[i].FindControl("ddlParticdis")).Items.Clear();
                //                ((ListBox)gv.Rows[i].FindControl("ddlParticdis")).Visible = false;
                //                ((Label)gv.Rows[i].FindControl("lblgvTime")).Visible = false;

                //                break;

                //        }

                //    }
            }
        }
        protected void lbtnUpdateReSheduleDiscus_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string comcod = this.GetComeCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString(); ;
                // start
                string hempid = this.lbleditempid.Value;
                DataTable dt = ((DataTable)ViewState["tblempsup"]).Copy();

                var query = (from dtl1 in dt.AsEnumerable()
                             where (dtl1.Field<string>("empid") == hempid) || (dtl1.Field<string>("empid") == empid)
                             select dtl1);

                DataTable dtE = query.AsDataView().ToTable();
                if (dtE.Rows.Count == 0)
                {
                    msg = "This prospect is not your under";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

                if (empid.Length == 0)
                {
                    msg = "Employee is not exixted";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;

                }
                // end



                string Client = this.lblproscod.Value.ToString();

                string kpigrp = "000000000000";

                string wrkdpt = "000000000000";

                DateTime time = System.DateTime.Now;

                GridView gv = (GridView)this.FindControl("Schedule_");


                //string cdate = this.txtFrom.Text.ToString() +" "+ time.ToString("HH:mm");

                string cdate = Convert.ToDateTime((((TextBox)gv.Rows[0].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)gv.Rows[0].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)gv.Rows[0].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)gv.Rows[0].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");



                string Gvalue = "";
                bool result;
                for (int i = 0; i < this.gvInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvInfo.Rows[i].FindControl("lblgvItmCodedis")).Text.Trim();
                    string gtype = ((Label)this.gvInfo.Rows[i].FindControl("lgvgvaldis")).Text.Trim();





                    if (Gcode == "810100102002")
                    {


                        //Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                        //    : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();



                        foreach (ListItem chkfollow in ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items)
                        {


                            if (chkfollow.Selected)
                            {
                                Gvalue += chkfollow.Value;

                            }

                        }
                    }


                    else if (Gcode == "810100102019")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
                    }



                    else if (Gcode == "810100102016")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).SelectedValue.ToString();
                    }


                    else if (Gcode == "810100102018")
                    {

                        //Gvalue == "";
                        foreach (ListItem item in ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis")).Items)
                        {
                            if (item.Selected)
                            {

                                if (item.Selected)
                                {
                                    Gvalue += item.Value;
                                }
                            }
                        }


                        //Gvalue = (((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                        //    : ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).SelectedValue.ToString();
                    }




                    else if (Gcode == "810100102001")
                    {

                        //string fdatetime = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim()+ " " + ddlhour+":" + ddlMmin +" "+ ddlslb)).ToString("dd-MMM-yyyy HH:mm:ss");

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                            : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    else if (Gcode == "810100102020")
                    {
                        string sdsd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim();

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() == "") ? "1900-01-01 00:00:00"
                           : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    else
                    {

                        Gvalue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;




                    if (Gvalue != "")
                    {
                        result = HRData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "INSERTUPDATESCDINF", empid, Client, kpigrp, "", wrkdpt, cdate, Gcode, gtype, Gvalue);
                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }
                        Gvalue = "";
                    }


                }
                // For Update
                this.GetNotificationinfo();
                string rptype = this.hdnfrpttype.Value.ToString();
                if (rptype.Length > 0)
                {
                    this.ShowDetNotification(this.hdnfrpttype.Value.ToString());
                }
                else
                {
                    this.GetGridSummary();

                }


                msg = "Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);
                this.clearModalField(); // clear modal 

                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Update Info (Land CRM)";
                    string eventdesc = "Update Info (Land CRM)";
                    string eventdesc2 = "";

                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }




                //this.ShowData();


            }
            catch (Exception ex)
            {
                msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }
        }











        protected void GetValue(object sender, EventArgs e)
        {
            //Reference the Repeater Item using Button.
            RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

            //Reference the Label and TextBox.
            string proscode = (item.FindControl("lbldes_proscod") as Label).Text;
            string cdate = (item.FindControl("lblCdate") as Label).Text;
            ShowDiscussionRes(proscode, cdate);




            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);

        }
        private void ShowDiscussionRes(string proscode, string cdate)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("yyyyMM");
            string Empid = hst["empid"].ToString();
            //string grpcode = this.lblgrp.Text;
            string Client = proscode;
            string kpigrp = "000000000000";
            string wrkdpt = "000000000000";


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", Empid, Client, kpigrp, "", wrkdpt, cdate);

            ViewState["tbModalData"] = ds1.Tables[0];
            this.Modal_Data_Bind();


        }

        protected void lnkShowNotifcation_Click(object sender, EventArgs e)
        {
            this.GetNotificationinfo();
        }

        protected void lnkbtnOther_Click1(object sender, EventArgs e)
        {

        }

        protected void lnkBtnVisit_Click(object sender, EventArgs e)
        {

        }

        protected void Btn_tempBTN_Click(object sender, EventArgs e)
        {
            this.autoClickBtn_tempBTN();
        }

        protected void ChkBoxLstFollow_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);
               
                DataTable dt = (DataTable)ViewState["tbModalData"];               
                this.gvInfo.DataSource = dt;
                this.gvInfo.DataBind();


                this.GetFollow();
                DataTable dt5 = ((DataTable)ViewState["tblFollow"]).Copy();
                DataView dv1;
                dv1 = dt5.DefaultView;
                dv1.RowFilter = ("gcod like '96%'");

                this.GetParcipants();
                DataTable dt6 = (DataTable)ViewState["tblparti"];

                //GetVisitoraStatinfo();
                DataView dv;
                DataTable dtvs = ((DataTable)ViewState["tblFollow"]).Copy();


                //  Status
                dv = dtvs.DefaultView;
                dv.RowFilter = ("gcod like '95%'");

                //DataTable dts = dv.ToTable();
                //dts.Rows.Add("0000", "0000000", "----Select Status----");
                //dts.DefaultView.Sort = "gcod asc";
                //dts = dts.DefaultView.ToTable();
                //DataView dv1;


                //DataView dv1;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString()) + "%";

                DropDownList ddlgval1, ddlgval2, ddlgval3;
                ListBox ddlPartic;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string Gcode = dt.Rows[i]["gcod"].ToString();

                    switch (Gcode)
                    {



                        case "810100102001": //Followup Date

                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = true;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                            DateTime datetime = System.DateTime.Now;

                            string gTime = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                            ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                            ddlgval1.SelectedValue = (gTime.Length == 0) ? datetime.ToString("hh") : ASTUtility.Left(gTime, 2);
                            ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                            ddlgval2.SelectedValue = (gTime.Length == 0) ? datetime.ToString("mm") : gTime.Substring(3, 2);
                            ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                            ddlgval3.SelectedValue = (gTime.Length == 0) ? datetime.ToString("tt") : ASTUtility.Right(gTime, 2);


                            //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                            break;

                        case "810100102002":
                        case "810100102019"://Follow

                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;
                            //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;                        
                            //ChkBoxLstFollow = ((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow"));
                            //ChkBoxLstFollow.DataTextField = "gdesc";
                            //ChkBoxLstFollow.DataValueField = "gcod";
                            //ChkBoxLstFollow.DataSource = dt5;
                            //ChkBoxLstFollow.DataBind();
                            //ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();


                            ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;
                            CheckBoxList ChkBoxLstFollow = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow"));
                            ChkBoxLstFollow.DataTextField = "gdesc";
                            ChkBoxLstFollow.DataValueField = "gcod";
                            ChkBoxLstFollow.DataSource = dv1.ToTable();
                            ChkBoxLstFollow.DataBind();
                            ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();

                            break;





                        case "810100102016": //Status

                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = true;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                            CheckBoxList ChkBoxLstStatus = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus"));
                            ChkBoxLstStatus.DataTextField = "gdesc";
                            ChkBoxLstStatus.DataValueField = "gcod";
                            ChkBoxLstStatus.DataSource = dv.ToTable();
                            ChkBoxLstStatus.DataBind();
                            ChkBoxLstStatus.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                            break;

                        case "810100102018": //PARTICIPANTS  

                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = true;
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                            ddlPartic = ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis"));
                            ddlPartic.DataTextField = "gdesc";
                            ddlPartic.DataValueField = "gcod";
                            ddlPartic.DataSource = dt6;
                            ddlPartic.DataBind();
                            if (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim() != "")
                            {
                                ddlPartic.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                            }
                            int count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());
                            if (count == 0)
                            {


                                if (empid.Length == 13)
                                {

                                    empid = empid.Replace("%", "");
                                    ddlPartic.SelectedValue = empid;


                                }
                                //string empid1 = empid.Substring(0, 12);
                                //int index = 0;
                                //DataRow[] rows = dt6.Select("gcod='" + empid1 + "'");

                                //if (rows.Length > 0)
                                //{
                                //    index = Convert.ToInt32(dt6.Rows.IndexOf(rows[0]));
                                //}
                                //ddlPartic.SelectedIndex = index;
                            }


                            int j;
                            int k = 0;
                            string data = "";
                            for (j = 0; j < count / 12; j++)
                            {
                                data = dt.Rows[i]["gdesc1"].ToString().Substring(k, 12);
                                foreach (ListItem item in ddlPartic.Items)
                                {
                                    if (item.Value == data)
                                    {
                                        item.Selected = true;
                                    }

                                }
                                k = k + 12;
                            }


                            break;



                        case "810100102015":
                        case "810100102025"://Muliline
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).TextMode = TextBoxMode.MultiLine;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Rows = 3;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                            //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Clear();
                            //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = false;

                            TextBox sd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis"));
                            sd.Style.Add("background", "#DFF0D8");
                            sd.Style.Add("width", "100%");


                            //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                            break;

                        case "810100102020": //next Followup date

                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = true;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParicdis")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = true;


                            string gTime20 = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                            ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                            ddlgval1.SelectedValue = (gTime20.Length == 0) ? "" : ASTUtility.Left(gTime20, 2);
                            ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                            ddlgval2.SelectedValue = (gTime20.Length == 0) ? "" : gTime20.Substring(3, 2);
                            ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                            ddlgval3.SelectedValue = (gTime20.Length == 0) ? "" : ASTUtility.Right(gTime20, 2);

                            break;

                        default:
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis")).Items.Clear();
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlParticdis")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

                            break;

                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        protected void lblgvkpicall_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();
            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);
            if(ds1 == null )
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            ViewState["tbltempdt"] = ds1.Tables[0];
            DataView dv = new DataView(ds1.Tables[0]);
            dv.RowFilter = ("sircode='9601001' or gpcode ='9601001' ");
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = dv;
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void lblgvkpiextmeeting_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            ViewState["tbltempdt"] = ds1.Tables[0];
            DataView dv = new DataView(ds1.Tables[0]);
            dv.RowFilter = ("sircode='9601004' or gpcode ='9601004' ");
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = dv;
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void lblgvkpiintmeeting_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            ViewState["tbltempdt"] = ds1.Tables[0];
            DataView dv = new DataView(ds1.Tables[0]);
            dv.RowFilter = ("sircode='9601008' or gpcode ='9601008' ");
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = dv;
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void lblgvkpisurvey_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            ViewState["tbltempdt"] = ds1.Tables[0];
            DataView dv = new DataView(ds1.Tables[0]);
            dv.RowFilter = ("sircode='9601012' or gpcode ='9601012' ");
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = dv;
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void lblgvkpifeasibility_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            ViewState["tbltempdt"] = ds1.Tables[0];
            DataView dv = new DataView(ds1.Tables[0]);
            dv.RowFilter = ("sircode='9601016' or gpcode ='9601016' ");
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = dv;
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void lblgvkpiproposal_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            ViewState["tbltempdt"] = ds1.Tables[0];
            DataView dv = new DataView(ds1.Tables[0]);
            dv.RowFilter = ("sircode='9601020' or gpcode ='9601020' ");
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = dv;
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void lblgvkpileads_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblkpiDetails.Visible = true;
                int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
                string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

                lblkpiDetails.Text = "Kpi Details: " + empname;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComeCode();
                string frmdate = this.txtdate.Text.Trim();
                string todate = this.txtkpitodate.Text.Trim();
                DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPT_EMPKPI_LEAD_DETAILS", "8305%", frmdate, todate, empid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }
                this.gvSummary.DataSource = null;
                this.gvSummary.DataBind();
                this.gvkpidet.DataSource = ds1.Tables[0];
                this.gvkpidet.DataBind();

                if (gvkpidet.Rows.Count > 0)
                {
                    Session["Report1"] = gvkpidet;
                    ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                }
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        protected void lblgvkpiclosing_Click(object sender, EventArgs e)
        {
            //this.lblkpiDetails.Visible = true;

            //int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            //string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            //lblkpiDetails.Text = "Kpi Details: " + empname;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetComeCode();
            //string frmdate = this.txtdate.Text.Trim();
            //string todate = this.txtkpitodate.Text.Trim();

            //DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);

            //ViewState["tbltempdt"] = ds1.Tables[0];
            //DataView dv = new DataView(ds1.Tables[0]);
            //dv.RowFilter = ("sircode='9601001' or gpcode ='9601001' ");
            //this.gvSummary.DataSource = null;
            //this.gvSummary.DataBind();
            //this.gvkpidet.DataSource = dv;
            //this.gvkpidet.DataBind();

            //if (gvkpidet.Rows.Count > 0)
            //{
            //    Session["Report1"] = gvkpidet;
            //    ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            //}
        }

        protected void lblgvkpiothers_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "RPTEMPKPIDETAILS", "8305%", frmdate, todate, empid);

            ViewState["tbltempdt"] = ds1.Tables[0];
            DataView dv = new DataView(ds1.Tables[0]);
            dv.RowFilter = ("sircode='9601024' or gpcode ='9601024' ");
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = dv;
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }
    }
}


