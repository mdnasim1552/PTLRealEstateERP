﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;

using RealERPRDLC;

namespace RealERPWEB
{
    public partial class UserProfile : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();

        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        ProcessAccess UserData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                Get_UpComingHoliday();
                Get_Events();
                getLink();

                GetAllHolidays();
                ((Label)this.Master.FindControl("lblTitle")).Text = "User Profile";
            }

            this.GetProfile();

            if (fileuploaddropzone.HasFile)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string UserId = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                bool updatPhoto;
                string uploadFolder = Request.PhysicalApplicationPath + "Upload\\UserImages\\";
                string extension = Path.GetExtension(fileuploaddropzone.PostedFile.FileName);
                string savelocation = uploadFolder + UserId + extension;
                ((Panel)this.Master.FindControl("AlertArea")).Visible = false;

                image_file = fileuploaddropzone.PostedFile.InputStream;
                size = fileuploaddropzone.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;

                if (size < 125000)
                {
                    string oldImg = this.userimg.ImageUrl;
                    string dburl = "~/Upload/UserImages/" + UserId + extension;
                    var filePath = Server.MapPath(oldImg);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }


                    fileuploaddropzone.SaveAs(savelocation);

                    updatPhoto = UserData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSERIMAGES", UserId, dburl, "", "", "", "", "", "", "", "");
                    if (updatPhoto)
                    {
                        this.userimg.ImageUrl = dburl;
                        ((Image)this.Master.FindControl("userimg")).ImageUrl = dburl;

                        hst["userimg"] = dburl;
                        Session["tblLogin"] = hst;

                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Your Porofile Picture Updated Successfully');", true);

                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Your Porofile Picture Updated failed');", true);

                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Profile Picture Size Large');", true);


                }


            }
        }
        private void getLink()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = GetCompCode();
            switch (comcod)
            {
                case "3365":
                case "3101":

                    string userrole = hst["userrole"].ToString();

                    this.winsList.Visible = true;

                    this.lnkOrintation.Visible = true;
                    this.lnkOrintation.NavigateUrl = "http://172.16.4.113/bti_training/orientation.html";
                    this.HyperCodeofConduct.Visible = (userrole == "1" || userrole == "2" || userrole == "4" ? true : false);
                    this.HypOrganogram.Visible = (userrole == "1" || userrole == "2" || userrole == "4" ? true : false);
                    this.PaySlipPart.Visible = true;

                    this.GetWinList();
                    this.OrganoGram();
                    this.getConduct();
                    break;
                default:

                    this.lnkOrintation.Visible = false;
                    this.HyperCodeofConduct.Visible = false;
                    this.HypOrganogram.Visible = false;
                    this.pnlServHis.Visible = false;
                    this.winsList.Visible = false;
                    this.hrpolicy.Visible = false;
                    this.PaySlipPart.Visible = false;
                    break;


            }

        }

        private void GetWinList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            string comcod = this.GetCompCode();
            string curYear = System.DateTime.Today.ToString("yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETEMPMONTHLYWINLIST", curYear, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            DataTable dt = ds1.Tables[0];

            string winlist = "";
            for (int j = 1; j < dt.Rows.Count; j++)
            {
                winlist += "<li class='list-group-item pt-1 pb-1'><a class='list-group-item-body'  href='" + dt.Rows[j]["fileurl"].ToString() + "' target='_blank'>" + dt.Rows[j]["title"].ToString() + "</a></li>";

            }
            this.winUlList.InnerHtml = winlist;

        }
        private void getConduct()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETEMPMONTHLYWINLIST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[2];
            this.conductid.InnerHtml = "<iframe src='" + dt.Rows[0]["fileurl"].ToString() + "' width='50%' height='700px'></iframe>";
        }

        private void OrganoGram()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETEMPMONTHLYWINLIST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            DataTable dt = ds1.Tables[1];

            int count1 = dt.Rows.Count / 3;
            int count2 = count1 + count1;


            string ormlist1 = "";
            string ormlist2 = "";
            string ormlist3 = "";

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (j + 1 <= count1)
                {
                    ormlist1 += "<li class='list-group-item pt-1 pb-1'>" +
                                              "<div class='list-group-item-figure'>" +
                                                  "<div class='tile bg-success'>" + dt.Rows[j]["title"].ToString().Substring(0, 2) +
                                             "</div> </div>" +
                                             "<a class='list-group-item-body'  href='" + dt.Rows[j]["fileurl"].ToString() + "' target='_blank'>" + dt.Rows[j]["title"].ToString() + "</a>" +
                                         " </li>";
                }
                else if (j + 1 <= count2)
                {
                    ormlist2 += "<li class='list-group-item pt-1 pb-1'>" +
                                              "<div class='list-group-item-figure'>" +
                                                  "<div class='tile bg-success'>" + dt.Rows[j]["title"].ToString().Substring(0, 2) +
                                             "</div></div>" +
                                             "<a class='list-group-item-body'  href='" + dt.Rows[j]["fileurl"].ToString() + "' target='_blank'>" + dt.Rows[j]["title"].ToString() + "</a>" +
                                         " </li>";
                }
                else
                {
                    ormlist3 += "<li class='list-group-item pt-1 pb-1'>" +
                                                             "<div class='list-group-item-figure'>" +
                                                                 "<div class='tile bg-success'>" + dt.Rows[j]["title"].ToString().Substring(0, 2) +
                                                            "</div></div>" +
                                                            "<a class='list-group-item-body'  href='" + dt.Rows[j]["fileurl"].ToString() + "' target='_blank'>" + dt.Rows[j]["title"].ToString() + "</a>" +
                                                        " </li>";
                }


            }


            this.orgrm1.InnerHtml = ormlist1;
            this.orgrm2.InnerHtml = ormlist2;
            this.orgrm3.InnerHtml = ormlist3;
        }


        public void GetProfile()
        {
            this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
            {
                Response.Redirect("~/Error404.aspx");
            }
            this.UDptment.InnerHtml = hst["dptdesc"].ToString();
            this.UDesignation.InnerHtml = hst["usrdesig"].ToString();

            UserName.InnerHtml = hst["userfname"].ToString();
            UserName1.InnerHtml = "<b>" + hst["username"].ToString() + "!!</b>  do you want to enable Notifications Panel in your Main Dashboard? (Note: ON for Enable and OFF for Disable)";
            userimg.ImageUrl = hst["userimg"].ToString();
            if (hst["events"].ToString() == "True")
            {
                EventSTatus.InnerHtml = "<input type='checkbox'  class='switcher-input'> " +
                                "<span class='switcher-indicator'></span> <span class='switcher-label-on'>ON</span> <span class='switcher-label-off'>OFF</span>";

            }
            else
            {
                EventSTatus.InnerHtml = "<input type='checkbox' class='switcher-input' checked='checked'> " +
                                         "<span class='switcher-indicator'></span> <span class='switcher-label-on'>ON</span> <span class='switcher-label-off'>OFF</span>";

            }
            this.getData();

        }

        [WebMethod]
        public static void ChangeEventsStatus()
        {
            Common comn = new Common();
            string usrid = comn.GetUserCode();
            string comcod = comn.GetCompCode();
            ProcessAccess accData = new ProcessAccess();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "EVENTSTATUS_UPDATE", usrid, "", "", "", "", "", "");
            if (result == true)
            {

            }
        }
        private string GetCompCode()
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

        private void getData()
        {

            ViewState.Remove("tblgrph");
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            string comcod = this.GetCompCode();

            string qempid = this.Request.QueryString["empid"] ?? "";
            string calltype = "";
            string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //string Date = this.txtDate.Text.Trim();
            switch (comcod)
            {
                // case "3101":  // For BTI as Per Instructiion Emdad Vai and Uzzal Vai  create by Md Ibrahim Khalil
                case "3365":
                    calltype = "RPTMYSERVICESBTI";
                    break;

                default:
                    calltype = "RPTMYSERVICES";
                    break;
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", calltype, empid, Date, "", "", "", "", "", "", "");

            // DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTMYSERVICES", empid, Date, "", "", "", "", "", "", "");
            //this.lbldesg.Visible = true;

            if (ds1 == null)
            {
                this.gvempservices.DataSource = null;
                this.gvempservices.DataBind();
                return;
            }
            ViewState["tblservices"] = ds1.Tables[0];
            ViewState["tblAttHist"] = ds1.Tables[1];
            ViewState["tblLeaveStatus"] = ds1.Tables[2];
            Session["tblEmpimg"] = ds1.Tables[3];
            ViewState["tblJobRespon"] = ds1.Tables[4];
            ViewState["tblAttHistGraph"] = ds1.Tables[5];
            ViewState["tblPaySlip"] = ds1.Tables[6];


            if (ViewState["tblgrph"] == null)
            {
                DataTable tblt01 = new DataTable();
                tblt01.Columns.Add("yearmon", Type.GetType("System.String"));
                tblt01.Columns.Add("absnt", Type.GetType("System.Double"));
                tblt01.Columns.Add("acintime", Type.GetType("System.Double"));
                tblt01.Columns.Add("aclate", Type.GetType("System.Double"));
                tblt01.Columns.Add("leave", Type.GetType("System.Double"));
                ViewState["tblgrph"] = tblt01;

                this.ShowJobRespon();
            }

            DataTable dt3 = (DataTable)ViewState["tblAttHist"];
            DataTable dt1 = (DataTable)ViewState["tblgrph"];

            DataRow dr1 = dt1.NewRow();
            dr1["yearmon"] = "";
            dr1["absnt"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(absnt)", "")) ? 0.00 : dt3.Compute("Sum(absnt)", ""))).ToString("#,##0;(#,##0)"); ;
            dr1["acintime"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(acintime)", "")) ? 0.00 : dt3.Compute("Sum(acintime)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dr1["aclate"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(aclate)", "")) ? 0.00 : dt3.Compute("Sum(aclate)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dr1["leave"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(leave)", "")) ? 0.00 : dt3.Compute("Sum(leave)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dt1.Rows.Add(dr1);
            ViewState["tblgrph"] = dt1;

            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETMYALLINFORMATION", empid, Date, "", "", "", "", "", "", "");



            this.Data_Bind();


        }
        protected void RptAttHistroy_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HyperLink lnkyearmon = (HyperLink)e.Item.FindControl("hlnkbtnadd");
                string comcod = this.GetCompCode();
                string ymonid = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ymonid")).ToString();
                string frmdate = "";
                string date = "";
                switch (comcod)
                {
                    case "3365":
                    case "3101":
                        date = "26-" + ASTUtility.Month3digit(Convert.ToInt32(ymonid.Substring(4, 2))) + "-" + ymonid.Substring(0, 4);
                        frmdate = Convert.ToDateTime(date).AddMonths(-1).ToString("dd-MMM-yyyy");
                        //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                        break;

                    default:
                        date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(ymonid.Substring(4, 2))) + "-" + ymonid.Substring(0, 4);
                        frmdate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                        break;
                }

                //string frmdate = Convert.ToDateTime(ymonid.Substring(4, 2) + "/"+ Convert.ToDateTime(ymonid.Substring(4, 2) + "/" + ymonid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                //string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                //string empid = this.ddlEmpName.SelectedValue.ToString().Trim();
                //lnkyearmon.NavigateUrl = "~/F_81_Hrm/F_82_App/RptMyAttendenceSheet.aspx?Type=&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;

                //HyperLink lnkyearmon = (HyperLink)e.Item.FindControl("hlnkbtnadd");
                //string comcod = this.GetCompCode();
                //string ymonid = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ymonid")).ToString();
                //string frmdate = Convert.ToDateTime(ymonid.Substring(4, 2) + "/01/" + ymonid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                lnkyearmon.NavigateUrl = "~/F_81_Hrm/F_82_App/RptMyAttendenceSheet.aspx?Type=&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;
            }



            if (e.Item.ItemType == ListItemType.Footer)
            {

                DataTable dt3 = (DataTable)ViewState["tblAttHist"];
                if (dt3.Rows.Count == 0)
                {

                    ((Label)e.Item.FindControl("lblacintime")).Text = "";
                    ((Label)e.Item.FindControl("lbltotalabs")).Text = "";
                    ((Label)e.Item.FindControl("lbltotallate")).Text = "";
                    ((Label)e.Item.FindControl("lbltotalleave")).Text = "";
                    ((Label)e.Item.FindControl("lbltolvadj")).Text = "";
                    ((Label)e.Item.FindControl("lblfrtolateapp")).Text = "";


                    return;
                }

                DataTable dt5 = (DataTable)ViewState["tblAttHistGraph"];

                ((Label)e.Item.FindControl("lblperIntime")).Text = Convert.ToDouble(dt5.Rows[0]["perpontow"]).ToString("#,##0.00;(#,##0.00)");
                ((Label)e.Item.FindControl("lblPerabs")).Text = Convert.ToDouble(dt5.Rows[0]["perab"]).ToString("#,##0.00;(#,##0.00)");
                ((Label)e.Item.FindControl("lblperLate")).Text = Convert.ToDouble(dt5.Rows[0]["perlate"]).ToString("#,##0.00;(#,##0.00)");
                ((Label)e.Item.FindControl("lblperleave")).Text = Convert.ToDouble(dt5.Rows[0]["perleave"]).ToString("#,##0.00;(#,##0.00) ");



                ((Label)e.Item.FindControl("lblacintime")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(acintime)", "")) ? 0.00 : dt3.Compute("Sum(acintime)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)e.Item.FindControl("lbltotalabs")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(absnt)", "")) ? 0.00 : dt3.Compute("Sum(absnt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)e.Item.FindControl("lbltotallate")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(aclate)", "")) ? 0.00 : dt3.Compute("Sum(aclate)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)e.Item.FindControl("lbltotalleave")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(leave)", "")) ? 0.00 : dt3.Compute("Sum(leave)", ""))).ToString("#,##0.00;(#,##0.00); ");

                ((Label)e.Item.FindControl("lbltolvadj")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(lvadj)", "")) ? 0.00 : dt3.Compute("Sum(lvadj)", ""))).ToString("#,##0.00;(#,##0.00); ");



                ((Label)e.Item.FindControl("lblfrtolateapp")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(lateapp)", "")) ? 0.00 : dt3.Compute("Sum(lateapp)", ""))).ToString("#,##0.00;(#,##0.00); ");



            }

        }

        private void ShowJobRespon()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataTable dt = (DataTable)ViewState["tblJobRespon"]; ;
            if (dt == null)
            {

                //this.lblnotfound.Visible = true;
                this.grvJobRespo.DataSource = null;
                this.grvJobRespo.DataBind();

                return;

            }
            this.grvJobRespo.DataSource = dt;
            this.grvJobRespo.DataBind();

        }
        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblservices"];
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString();
                this.gvempservices.DataSource = dt;
                this.gvempservices.DataBind();




                DataTable dt2 = (DataTable)ViewState["tblAttHist"];
                this.ShowAttHistoryGraph();
                this.RptAttHistroy.DataSource = dt2;
                this.RptAttHistroy.DataBind();


                DataTable dt3 = (DataTable)ViewState["tblLeaveStatus"];
                this.gvLeaveStatus.DataSource = dt3;
                this.gvLeaveStatus.DataBind();



                DataTable dt6 = (DataTable)ViewState["tblPaySlip"];
                this.gvPaySlip.DataSource = dt6;
                this.gvPaySlip.DataBind();


                string comcod = this.GetCompCode();

                string frmdate = Convert.ToDateTime("01-Jan-" + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");


                //this.hlnkbtnNext.NavigateUrl = "../../F_81_Hrm/F_82_App/LinkMyHRLeave?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;

                DataTable dt4 = (DataTable)ViewState["tblEmpimg"];
                DataTable dt5 = (DataTable)ViewState["tblJobRespon"];



            }

            catch (Exception ex)
            { }

        }

        private void ShowAttHistoryGraph()
        {
            DataTable dt4 = (DataTable)ViewState["tblAttHistGraph"];


            double present = Convert.ToDouble(dt4.Rows[0]["perpontow"].ToString());
            double late = Convert.ToDouble(dt4.Rows[0]["perlate"].ToString());
            //  double eleave = Convert.ToDouble(dt4.Rows[0]["earlyLev"].ToString());
            double onlaeve = Convert.ToDouble(dt4.Rows[0]["perleave"].ToString());
            double absent = Convert.ToDouble(dt4.Rows[0]["perab"].ToString());

            this.lblpresent.Text = present.ToString("#,##0.00;(#,##0.00);");
            this.lbllate.Text = late.ToString("#,##0.00;(#,##0.00);");
            //  this.lbleleave.Text = eleave.ToString("#,##0.00;(#,##0.00);");
            this.lblonleave.Text = onlaeve.ToString("#,##0.00;(#,##0.00);");
            this.lblabs.Text = absent.ToString("#,##0.00;(#,##0.00);");




        }
        protected void hyplPreviewCv_Click1(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //this.hyplPreviewCv.Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptempservices = new ReportDocument();

            if (comcod == "3340" || comcod == "3101")
            {
                rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptEmpAllInformationUrban();



            }

            else
            {
                rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptMyAllInformation();
                TextObject Empsigna = rptempservices.ReportDefinition.ReportObjects["Empsigna"] as TextObject;
                Empsigna.Text = (ds1.Tables[2].Rows.Count == 0) ? "Employee Name" : ds1.Tables[2].Rows[0]["empname"].ToString();
                TextObject txtnetsalary = rptempservices.ReportDefinition.ReportObjects["txtnetsalary"] as TextObject;
                txtnetsalary.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
                TextObject txtNetpayable = rptempservices.ReportDefinition.ReportObjects["txtNetpayable"] as TextObject;
                txtNetpayable.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netpay"]).ToString("#,##0; (#,##0); ");

            }



            TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = (ds1.Tables[2].Rows.Count == 0) ? "Employee Name" : ds1.Tables[2].Rows[0]["empname"].ToString();



            TextObject txtCompName = rptempservices.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = (ds1.Tables[2].Rows.Count == 0) ? comnam : ds1.Tables[2].Rows[0]["empcomdesc"].ToString();

            TextObject rpttxtempdept = rptempservices.ReportDefinition.ReportObjects["txtempdept"] as TextObject;
            rpttxtempdept.Text = (ds1.Tables[2].Rows.Count == 0) ? "Department Name: " : "DEPARTMENT NAME: " + ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            TextObject txtcomaddress = rptempservices.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            txtcomaddress.Text = comadd;

            rptempservices.SetDataSource(ds1.Tables[0]);
            //string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            this.ShowAttHistoryGraph();
            //this.lbtnOk_Click(null, null);

        }

        private object HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string type = "Pabx";
            string company, secid;
            switch (type)
            {

                case "Pabx":
                    company = dt1.Rows[0]["company"].ToString();
                    secid = dt1.Rows[0]["secid"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["secid"].ToString() == secid)
                        {

                            dt1.Rows[j]["companyname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["company"].ToString() == company)
                                dt1.Rows[j]["companyname"] = "";

                            if (dt1.Rows[j]["secid"].ToString() == secid)
                                dt1.Rows[j]["secton"] = "";
                        }


                        company = dt1.Rows[j]["company"].ToString();
                        secid = dt1.Rows[j]["secid"].ToString();
                    }

                    break;


            }

            return dt1;

        }

        private void Get_Events()
        {

            string comcod = this.GetCompCode();
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            
            this.EventBirthday.Visible = (comcod=="3365" & usrid=="3")?false:true;
            

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GET_UPCOMMING_EVENTS", fdate, usrid, "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            string innHTML = "";
            string innHTMLTopnot = "";
            string BirthdayHTML = "";
            string status = "";
            int i = 0;







            foreach (DataRow dr in ds1.Tables[0].Rows)
            {

                //string url = "";


                //if (dr["imgurl2"] != null && dr["imgurl2"].ToString() != "")
                //{
                //    url = "../../" + dr["imgurl2"].ToString().Remove(0, 2);
                //}
                //else if (dr["imgurl"] != null && dr["imgurl"].ToString() != "")
                //{

                //    byte[] biempimg = (byte[])dr["imgurl"];
                //    url = "data:image;base64," + Convert.ToBase64String(biempimg);
                //}
                //else
                //{
                //    url = "Content/Theme/images/avatars/human_avatar.png";
                //}

                //string type = dr["evtype"].ToString();
                //if (type == "Birthday")
                //{
                //    BirthdayHTML += @"<div class='col-12 col-sm-6 col-lg-4'><div class='media align-items-center mb-3'><a href='#' class='user-avatar user-avatar-lg mr-3'><img src='" + url + "' alt=''></a><div class='media-body'><h6 class='card-subtitle text-muted'>" + dr["eventitle"] + "</h6></div><a href='#' class='btn btn-reset text-muted' data-toggle='tooltip' title='' data-original-title='Chat with teams'><i class='oi oi-chat'></i></a></div></div>";
                //}
                //i++;
            }

            foreach (DataRow dr in ds1.Tables[1].Rows)
            {
                status = (i == 0) ? "active" : "";
                innHTMLTopnot += @"<p>" + dr["eventitle"] + "</p>";
                i++;
            }


            this.gvAllNotice.DataSource = ds1.Tables[1];
            this.gvAllNotice.DataBind();

            this.EventBirthday.InnerHtml = BirthdayHTML;
            this.EventCaro.InnerHtml = innHTMLTopnot;


        }


        private void Get_UpComingHoliday()
        {

            string comcod = this.GetCompCode();
            string empid = GetEmpID();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETEMPOFFDAYINFORAMTIONUPCOMING", empid, "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;


            string innHTML = "";
            int i = 0;
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {

                innHTML += @"<div class='row mb-2 pb-1' style='border-bottom:1px solid #ecedf1'><div class='col-8'><a href='#' class='tile bg-pink text-white mr-1'>" + dr["shortday"] + "</a><a href='#'>" + dr["daynam"] + " <small> ( " + dr["reason"] + " ) </small></a></div><div class='col-4'><span class='badge bg-purple text-white'>" + dr["wkdate1"] + "</span></div></div>";

                i++;
            }

            this.upComingHolidays.InnerHtml = innHTML;


        }

        private void GetAllHolidays()
        {
            string comcod = this.GetCompCode();
            string empid = GetEmpID();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETEMPOFFDAYINFORAMTION", empid, "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;

            Session["tblHolidays"] = ds1.Tables[0];

            this.LoadGridHolidays();
        }

        private void LoadGridHolidays()
        {
            DataTable dtx = (DataTable)Session["tblHolidays"];
            DataView dv1 = dtx.DefaultView;
            dv1.RowFilter = ("dstatus ='H'");
            DataTable dt = dv1.ToTable();


            DataView dv2 = dtx.DefaultView;
            dv2.RowFilter = ("dstatus ='ST'");
            DataTable dt2 = dv2.ToTable();



            this.GvHoliday.DataSource = dt;
            this.GvHoliday.DataBind();
            this.gvSpHolidyas.DataSource = dt2;
            this.gvSpHolidyas.DataBind();
        }

        protected void gvSpHolidyas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSpHolidyas.PageIndex = e.NewPageIndex;
            this.LoadGridHolidays();

        }

        protected void hlnkbtnNext_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            string comcod = this.GetCompCode();

            string monName;
            if (comcod == "3365" || comcod == "3101")
            {
                monName = "-Dec-";
            }
            else
            {
                monName = "-Jan-";

            }

            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);

            string date1 = Convert.ToDateTime(startdate + monName + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");
            // string frmdate = Convert.ToDateTime(date).AddYears(-1).ToString("dd-MMM-yyyy");

            DateTime date = Convert.ToDateTime(date1);


            //string date1 = Convert.ToDateTime(Convert.ToInt32(startdate)+ "-Dec-" + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");

            string frmdate = Convert.ToInt32(date.ToString("dd")) > Convert.ToInt32(startdate) ? Convert.ToDateTime(date).ToString("dd-MMM-yyyy") : Convert.ToDateTime(date).AddYears(-1).ToString("dd-MMM-yyyy");


            //string frmdate = startdate + "-Dec-"  + this.txtDate.Text.Substring(7);
            //this.txtDate.Text = frmdate; 

            //string frmdate = "";
            //string date = "";
            //switch (comcod)
            //{
            //    case "3365":
            //    case "3101":
            //        date = Convert.ToDateTime("26-Dec-" + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");
            //        frmdate = Convert.ToDateTime(date).AddYears(-1).ToString("dd-MMM-yyyy");
            //        //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
            //        break;

            //    default:
            //        date = Convert.ToDateTime("01-Jan-" + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");
            //        frmdate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
            //        break;
            //}

            //string frmdate = Convert.ToDateTime("01-Jan-" + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");

            string todate = Convert.ToDateTime(frmdate).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
            Response.Redirect("~/F_81_Hrm/F_82_App/LinkMyHRLeave?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate);
        }




        protected void birthday_print_click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dddd");

            string curdate = System.DateTime.Now.ToString("yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var ds = HRData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "BIRTHDAYNOTICE");
            if (ds == null)
            {
                return;
            }

            DataTable dt = ds.Tables[0];


            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.birthdayDate>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.rptBirthday", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("rptTitle", "Upcomming Employee Birthday-"+ curdate));
            Rpt1.SetParameters(new ReportParameter("comName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printDate", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;

            string printype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRpt('" + printype + "');", true);


        }


        protected void gvholidayprint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dddd");
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") ?? "";
            //string todate = this.txttodate.Text.ToString() ?? "";
            //string type = this.ddlholidayType.SelectedValue.ToString();
            string curdate = System.DateTime.Now.ToString("yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dtx = (DataTable)Session["tblHolidays"];
            DataView dv1 = dtx.DefaultView;
            dv1.RowFilter = ("dstatus ='H'");
            DataTable dt = dv1.ToTable();

            if (dt == null)
            {
                return;
            }


            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.yearlyholiday>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.rptYearlyHolidayGov", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rptTitle", curdate));


            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "test"));

            Session["Report1"] = Rpt1;

            string printype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRpt('" + printype + "');", true);


        }

        protected void spholidayprint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dddd");
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") ?? "";
            //string todate = this.txttodate.Text.ToString() ?? "";
            //string type = this.ddlholidayType.SelectedValue.ToString();
            string curdate = System.DateTime.Now.ToString("yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dtx = (DataTable)Session["tblHolidays"];
            DataView dv2 = dtx.DefaultView;
            dv2.RowFilter = ("dstatus ='ST'");
            DataTable dt2 = dv2.ToTable();

            if (dt2 == null)
            {
                return;
            }

            var list = dt2.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.yearlyholiday>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.rptYearlyHoliday", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rptTitle", curdate));

            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "test"));

            Session["Report1"] = Rpt1;
            string printype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRpt('" + printype + "');", true);
        }

        protected void gvPaySlip_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkPrintPaySlip = (HyperLink)e.Row.FindControl("hlnkPrintPaySlip");
                string monthid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "monthid")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();

                hlnkPrintPaySlip.NavigateUrl = "~/F_81_Hrm/F_89_Pay/PrintPaySlip.aspx?Type=paySlip&monthid=" + monthid + "&empid=" + empid;

            }
        }
    }
}


