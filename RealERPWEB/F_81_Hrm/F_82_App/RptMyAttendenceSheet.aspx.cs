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
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class RptMyAttendenceSheet : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if (!IsPostBack)
                {


                    //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    //    Response.Redirect("../../AcceessError.aspx");
                    string nextday = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtgvenjoydt1.Text = nextday;
                    this.txtgvenjoydt2.Text = nextday;
                    SelectDate();
                    ((Label)this.Master.FindControl("lblTitle")).Text = "  Employee Status";
                    string qtype = this.Request.QueryString["Type"].ToString();
                    GetLeaveType();
                    if (qtype == "MGT")
                    {
                        this.mgtCard.Visible = true;
                        this.empMgt.Visible = true;
                        GetEmpLoyee();
                        // GetSupvisorCheck();
                        this.ddlEmpName_SelectedIndexChanged(null, null);
                        //GetLeavType();
                    }
                    else
                    {
                        this.mgtCard.Visible = false;
                        this.empMgt.Visible = false;
                        this.getMyAttData();
                    }

                }
            } catch (Exception ex)
            {

            }
           
        }
        private void GetLeaveType()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3354":                
                   // ddlReqType.Items.Add(new ListItem("Late Approval Request", "LA"));
                    // ddlReqType.Items.Add(new ListItem("Late Present Approval Request(if Finger 10:00 to 5:30)", "LP"));
                    // ddlReqType.Items.Add(new ListItem("Time Correction Approval Request(Project Visit, Customer visit, etc)", "TC"));
                    ddlReqType.Items.Add(new ListItem("Absent Approval Request (IF Finger/Attandance missed but present)", "AB"));
                    break;
                case "3366":
                    ddlReqType.Items.Add(new ListItem("Late Approval Request", "LA"));
                   // ddlReqType.Items.Add(new ListItem("Late Present Approval Request(if Finger 10:00 to 5:30)", "LP"));
                   // ddlReqType.Items.Add(new ListItem("Time Correction Approval Request(Project Visit, Customer visit, etc)", "TC"));
                    ddlReqType.Items.Add(new ListItem("Absent Approval Request (IF Finger missed but present)", "AB"));
                    break;
                case "3365":
                case "3102":
                    ddlReqType.Items.Add(new ListItem("Time Correction Approval Request(Project Visit, Customer visit, etc)", "TC"));
                    ddlReqType.Items.Add(new ListItem("Absent Approval Request (IF Finger missed but present)", "AB"));
                    break;
                case "3101":
                    ddlReqType.Items.Add(new ListItem("Late Approval Request(if Finger 9:04:59 to 9:59:59)", "LA"));
                    ddlReqType.Items.Add(new ListItem("Late Present Approval Request(if Finger 10:00 to 5:30)", "LP"));
                    ddlReqType.Items.Add(new ListItem("Time Correction Approval Request(Project Visit, Customer visit, etc)", "TC"));
                    ddlReqType.Items.Add(new ListItem("Absent Approval Request (IF Finger missed but present)", "AB"));
                    break;

                default:
                    break;
            }
           
          

        }
        private void SelectDate()
        {
            try
            {
                string comcod = this.GetComeCode();
                this.txtgvenjoydt1.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                DateTime date = Convert.ToDateTime(txtgvenjoydt1.Text);
                DataSet datSetup = compUtility.GetCompUtility();
                if (datSetup == null)
                    return;
                string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
                string frmdate = Convert.ToInt32(date.ToString("dd")) > Convert.ToInt32(startdate) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                frmdate = startdate + frmdate.Substring(2);
                this.txtgvenjoydt1.Text = frmdate;
                this.txtgvenjoydt2.Text = Convert.ToDateTime(this.txtgvenjoydt1.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                //string tdate = date.ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {

            }
        }
        private void GetEmpLoyee()
        {

            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", "94%", "%%", "%%", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private string GetComLateAccTime()
        {
            string comcod = this.GetComeCode();
            string acclate = "";
            switch (comcod)
            {

                case "3336":
                    acclate = "acclate";
                    break;

                default:
                    break;
            }

            return acclate;
        }
        private void getMyAttData()
        {
            try
            {
                string comcod = this.GetComeCode();

                string type = this.Request.QueryString["Type"].ToString();
                string frmdate = "";
                string todate = "";
                string empid = "";

                if (type == "MGT")
                {
                    frmdate = this.txtgvenjoydt1.Text.ToString();
                    todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    empid = this.ddlEmpName.SelectedValue.ToString(); ;
                }
                else
                {
                    frmdate = this.Request.QueryString["frmdate"].ToString();
                    todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    empid = this.Request.QueryString["empid"].ToString();
                }

                string Actime = this.GetComLateAccTime();

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, Actime, "", "", "", "", "");

                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                //this.lblDateOn.Text = " From " + this.Request.QueryString["frmdate"].ToString() + " To " + this.Request.QueryString["todate"].ToString();
                // this.lblcompname.Text = ds1.Tables[2].Rows[0]["companyname"].ToString();
                this.lblname.Text = ds1.Tables[0].Rows[0]["empnam"].ToString();
                this.lblattype.Text = ds1.Tables[0].Rows[0]["attype"].ToString();
                this.tbldept.Text = ds1.Tables[0].Rows[0]["empdept"].ToString();
                this.lbljoindate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["joindate"]).ToString("dd/MMM/yyyy");

                this.lblconfirmdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["confirmdate"]).ToString("dd/MMM/yyyy");
                this.sysid.Visible = (type == "MGT" ? true : false);
                this.lblsysid.Text = ds1.Tables[0].Rows[0]["empid"].ToString();
                this.lbldpt.Text = ds1.Tables[0].Rows[0]["empdept"].ToString();
                this.lbldesg.Text = ds1.Tables[0].Rows[0]["empdsg"].ToString();
                this.lblcard.Text = ds1.Tables[0].Rows[0]["idcardno"].ToString();
                this.empdeptid.Value = ds1.Tables[0].Rows[0]["empdeptid"].ToString();

                this.lblwork.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["twrkday"]).ToString("#, ##0;(#, ##0);");
                this.lblLate.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tlday"]).ToString("#, ##0;(#, ##0);");
                this.lblLeave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tlvday"]).ToString("#, ##0;(#, ##0);");
                this.lblAbsent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tabsday"]).ToString("#, ##0;(#, ##0);");
                this.lblHoliday.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["thday"]).ToString("#, ##0;(#, ##0);");
                if (comcod == "3366")
                {
                    if (ds1.Tables[3].Rows.Count != 0)
                    {
                        this.lblIntime.Text = ds1.Tables[3].Rows[0]["intime"].ToString();
                        this.lblout.Text = ds1.Tables[3].Rows[0]["outtime"].ToString();
                    }
                }
                else
                {
                    this.lblIntime.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["offintime1"]).ToString("hh:mm tt");
                    this.lblout.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["stdtimeout"]).ToString("hh:mm tt");
                }



                Session["tblempdatewise"] = ds1.Tables[0];

                this.RptMyAttenView.DataSource = ds1;
                this.RptMyAttenView.DataBind();
            }
            catch (Exception ex)
            {

            }

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintEmpAttnIdWise();
        }

        private void PrintEmpAttnIdWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComeCode();
            //string Company = "";
            // string PCompany = "";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.Request.QueryString["empid"].ToString();
            string frmdate = this.Request.QueryString["frmdate"].ToString();
            string todate = this.Request.QueryString["todate"].ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, "", "", "", "", "", "");


            DataTable dtdailyiemp = ds4.Tables[0];
            int sum = 0;
            string hour, minute;
            for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            }
            hour = Convert.ToInt32(sum / 60).ToString();
            minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            string TotalHour = hour + ":" + minute;
            ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.rptDailyAttnEmp();
            rptTest.SetDataSource(ds4.Tables[0]);
            TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            txtRptComName.Text = ds4.Tables[2].Rows[0]["companyname"].ToString();

            TextObject txttowrkday = rptTest.ReportDefinition.ReportObjects["txttowrkday"] as TextObject;
            txttowrkday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            txttolateday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            TextObject txttoleaveday = rptTest.ReportDefinition.ReportObjects["txttoleaveday"] as TextObject;
            txttoleaveday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            TextObject txtoabsday = rptTest.ReportDefinition.ReportObjects["txtoabsday"] as TextObject;
            txtoabsday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            TextObject txtohday = rptTest.ReportDefinition.ReportObjects["txtohday"] as TextObject;
            txtohday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");


            TextObject txtrptTotalHour = rptTest.ReportDefinition.ReportObjects["txtTHour"] as TextObject;
            txtrptTotalHour.Text = TotalHour;
            TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            Session["Report1"] = rptTest;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void RptMyAttenView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
            {

                string comcod = this.GetComeCode();
                string qtype = this.Request.QueryString["Type"] ?? "";
                
                string empid = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "empid")).ToString().Trim();
                string applyReq = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "applyReq")).ToString().Trim();
                string ahleave = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "leav")).ToString().Trim();
                string lateapp = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "lateapp")).ToString().Trim();
                string iscancel = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "iscancel")).ToString().Trim();

                DateTime offimein = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimein"));
                DateTime offouttim = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimeout"));


                DateTime actualin = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualin"));
                DateTime actualout = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualout"));

                HyperLink hyplinkapp =((HyperLink)e.Item.FindControl("hyplnkApplyLv"));


//rakib
                string sysdate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "wintime")).ToString("dd-MM-yyyy");
                string sysdated = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "wintime")).ToString("dd");
                string sysdatem = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "wintime")).ToString("MM");
                string sysdatemprev = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "wintime")).AddMonths(-1).ToString("MM");
                string sysdatedprev = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "wintime")).AddMonths(-1).ToString("dd");
                string sysdatednext = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "wintime")).AddMonths(1).ToString("dd");
                string curdatd = DateTime.Today.ToString("dd");
                string curdatm = DateTime.Today.ToString("MM");
                string curdaty = DateTime.Today.ToString("yyyy");
                string prevmon = DateTime.Today.AddMonths(-1).ToString("MM");
                string nextd = DateTime.Today.AddMonths(-1).ToString("dd");
                string nextm = DateTime.Today.AddMonths(-1).ToString("MM");
                string nexty = DateTime.Today.AddMonths(-1).ToString("yyyy");
//
                switch (comcod)
                {
                    case "3365":
                    case "3101":
                    case "3366":
                    
                        if (ahleave == "A" && iscancel == "False")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                            ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                            ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;color:red";
                            ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = applyReq == "" ? true : false;
                            ((HyperLink)e.Item.FindControl("hyplnkApplyLv")).Visible = applyReq == "" ? true : false;


                        }
                        else if (ahleave == "H" || ahleave == "Lv" && iscancel == "False")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                            ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                            ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;";

                        }
                        else if ((offimein < actualin) && lateapp == "False" && iscancel == "False")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((Label)e.Item.FindControl("lblactualin")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((Label)e.Item.FindControl("lbldtimehour")).Attributes["style"] = "font-weight:bold; color:red;";
                            //((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = applyReq == "" ? true : false;
                            //((HyperLink)e.Item.FindControl("hyplnkApplyLv")).Visible = applyReq == "" ? true : false;    

                            if (Convert.ToInt32(sysdated) < 26 && (sysdatem == curdatm || prevmon == sysdatemprev) || Convert.ToInt32(sysdatedprev) >= 26 && comcod=="3365")
                            {
                                ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = applyReq == "" ? true : false;
                                ((HyperLink)e.Item.FindControl("hyplnkApplyLv")).Visible = applyReq == "" ? true : false;
                            }
                            else if (Convert.ToInt32(sysdated) > 25 && (sysdatem == curdatm || sysdatem == nextm) || Convert.ToInt32(sysdatednext) < 26)
                            {

                                ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = applyReq == "" ? true : false;
                                ((HyperLink)e.Item.FindControl("hyplnkApplyLv")).Visible = applyReq == "" ? true : false;
                            }
                            else
                            {

                                ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = false;
                                ((HyperLink)e.Item.FindControl("hyplnkApplyLv")).Visible = false;
                            }
                        }
                        else if (iscancel == "True")
                        {
                            ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = true;
                            ((LinkButton)e.Item.FindControl("lnkRequstApply")).Text = "Re Apply Request";
                        }
                        else
                        {
                            ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = false;                          
                        }
                        if (qtype == "MGT")
                        {
                            hyplinkapp.NavigateUrl="~/F_81_Hrm/F_84_Lea/MyLeave?Type=MGT&Empid="+ empid+"&LevDay="+ offimein + "&LvType=";
                            //Response.Redirect("~/F_81_Hrm/F_84_Lea/MyLeave?Type=MGT");
                        }
                        else
                        {
                            hyplinkapp.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave?Type=User";
                            //Response.Redirect("~/F_81_Hrm/F_84_Lea/MyLeave?Type=User");
                        }
                        break;
                    case "3354":
              
                        if (ahleave == "A" && iscancel == "False")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                            ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                            ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;color:red";
                            ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = applyReq == "" ? true : false;
                            ((HyperLink)e.Item.FindControl("hyplnkApplyLv")).Visible = applyReq == "" ? true : false;



                        }
                        if (qtype == "MGT")
                        {
                            hyplinkapp.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave?Type=MGT&Empid=" + empid + "&LevDay=" + offimein + "&LvType=";
                            //Response.Redirect("~/F_81_Hrm/F_84_Lea/MyLeave?Type=MGT");
                        }
                        else
                        {
                            hyplinkapp.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave?Type=User";
                            //Response.Redirect("~/F_81_Hrm/F_84_Lea/MyLeave?Type=User");
                        }

                        break;
                    default:
                        if (ahleave == "A" || ahleave == "H" || ahleave == "Lv")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                            ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                            ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;";

                        }
                        else if ((offimein < actualin || offouttim > actualout) && lateapp == "False")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((Label)e.Item.FindControl("lblactualin")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((Label)e.Item.FindControl("lbldtimehour")).Attributes["style"] = "font-weight:bold; color:red;";
                        }
                        break;
                }






             



       
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                double AcTime = 0.00;
                DataTable dt3 = (DataTable)Session["tblempdatewise"];
                foreach (DataRow dr in dt3.Rows)
                {
                    double time = Convert.ToDouble("0" + dr["actTimehour"]);
                    AcTime = AcTime + time;
                }
                ((Label)e.Item.FindControl("lblTotalHour")).Text = AcTime.ToString("#,##0.00;(#,##0.00);"); //? 0.00 : dt3.Compute("Sum(actTimehour)", ""))).ToString("#,##0.00;(#,##0.00);");

                //Double actTimehour =Convert.ToDouble(dt3.Rows[0]["actTimehour"]);
                //((Label)e.Item.FindControl("lblTotalHour")).Text = Convert.ToDouble((Convert.IsDBNull(Convert.ToDouble(dt3.Compute("Sum(actTimehour)", ""))))).ToString("#,##0.00;(#,##0.00);"); //? 0.00 : dt3.Compute("Sum(actTimehour)", ""))).ToString("#,##0.00;(#,##0.00);");
            }
        }
        protected void lnkRequstApply_Click(object sender, EventArgs e)
        {
            this.ddlReqType.Items.Clear();
            GetLeaveType();


            string comcod = this.GetComeCode();
            //  this.lblabsheading.Text = "Apply for RequestDate :" + this.txtfrmDate.Text.ToString() + " To: " + this.txttoDate.Text.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();

            LinkButton lnkBtn1 = sender as LinkButton;
            RepeaterItem Rptitem = (RepeaterItem)lnkBtn1.NamingContainer;
            Label lblIntime = (Label)Rptitem.FindControl("lblIntime");
            Label issuedate = (Label)Rptitem.FindControl("lblacintime");
            Label actualin = (Label)Rptitem.FindControl("lblactualin");
            Label lblstatus = (Label)Rptitem.FindControl("lblstatus");
            Label lblisremarks = (Label)Rptitem.FindControl("lblisremarks");
            Label lblRequid = (Label)Rptitem.FindControl("lblRequid");
            Label lblapremarks = (Label)Rptitem.FindControl("lblapremarks");

            string attstatus = lblstatus.Text.Trim();
            ddlReqType.SelectedValue = (attstatus == "" && comcod == "3365" ? "TC" : attstatus == "A" ? "AB" : "LA");
            ddlReqType.Enabled = (attstatus == "A" ? false : true);

            if (attstatus == "A")
            {

                this.InfoApply.Visible = true;
            }

            else
            {
                //DateTime acint = DateTime.Parse(actualin.Text);
                //TimeSpan acintime = TimeSpan.Parse(acint.ToString("HH:mm"));
                //TimeSpan maxTime = TimeSpan.Parse("10:00");
                //if (userrole == "3")
                //{

                //    //ddlReqType.Items.Remove("Late Present Approval Request (if Finger 10:00 to 5:30)");
                //    this.ddlReqType.Items.RemoveAt(1);

                //}
                //else
                //{
                //    if (acintime >= maxTime)
                //    {
                //        ddlReqType.SelectedValue = "LP";
                //    }
                //    else
                //    {
                //        ddlReqType.SelectedValue = "LA";
                //    }


                //}

                this.InfoApply.Visible = false;
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalAbs();", true);

            //lblIntime

            this.lbldadteIntime.Text = lblIntime.Text;
            this.lbldadte.Text = issuedate.Text;
            this.lbldadteTime.Text = actualin.Text;
            this.lbldadteOuttime.Text = lblIntime.Text;
            this.txtAreaReson.Text = lblisremarks.Text;
            this.ReqID.Value = lblRequid.Text;

        }
        protected void lbntnAbsentApproval_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();
            //Sender Informaiton
            string comcod = this.GetComeCode();
            string sendUsername = hst["userfname"].ToString();
            string empid = this.GetEmpID();
            string sendDptdesc = hst["dptdesc"].ToString();
            string sendUsrdesig = hst["usrdesig"].ToString();
            string compName = hst["comnam"].ToString();
            string htmtableboyd = "";
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();
            
            string reqtype = this.ddlReqType.SelectedValue.ToString();
            string reqfor = reqtype == "AB" ? "Absent Approval" : reqtype == "LP" ? "Late Present Approval" : reqtype == "LA"? "Late Approval" : "Time of Correction";
            string reqdate = this.lbldadte.Text.Trim();
            string dayID =  Convert.ToDateTime(this.lbldadte.Text.Trim()).ToString("yyyyMMdd");
            string reqtimeIN = this.lbldadteIntime.Text.Trim();
            string reqtimeOUT = this.lbldadteOuttime.Text.Trim();
            string txtReson = txtAreaReson.Text.Trim();
            string reqid = this.ReqID.Value;
            if(txtReson=="")
            {
                this.txtAreaReson.Focus();
                this.txtAreaReson.CssClass = "form-control is-invalid";
                string errMsg = "Please Fill the Remarks";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalAbs();", true);
                return;
            }
           // if (reqtype == "AB")
          ///  {
                string[] tbcodeValue = txtReson.ToUpper().Split(' ');
                string[] validCodes = new string[] { "SICKNESS","LEAVE", "SICK", "STAR", "CASUAL", "SICK", "ALTERNATE","ADJUSTMENT", "SICKLEAVE", "SL","CL" };

                foreach (string aitem in validCodes)
                {
                    if (tbcodeValue.Contains(aitem))
                    {
                        string errMsg = "For Leave Related Request please apply from Leave";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                        this.txtAreaReson.Text = "";
                     
                        return;
                    }

                }
            //}     
            string usetime = "0:00";
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string qtype = this.Request.QueryString["Type"] ?? "";
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "INSERT_REQ_ATTN_CAHNGE", dayID, empid, reqdate, reqtype, reqtimeIN, reqtimeOUT, txtReson, usetime, usrid, postDat, reqid);

            if (!result)
            {
                string errMsg = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                return;
            }

            else
            {
                string trnid = this.GetattAppId(empid, reqdate);


                string Messaged = "";
                if (qtype != "MGT")
                {
                     Messaged = "Successfully applied for " + reqfor + ", please wait for approval";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);
                    this.SendNotificaion(reqdate, reqdate, trnid, deptcode, compsms, compmail, ssl, compName, htmtableboyd);
                    this.getMyAttData();
                }
                else
                {
                     Messaged = "Your request approved Successfully " + reqfor ;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);

                    string roletype = "DPT";
                    string Centrid = this.empdeptid.Value;
                    //DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "GETCEHCKAPPROVALBYID", trnid, roletype, Centrid, "", "", "", "", "", "");
                    //if (ds4.Tables[0].Rows.Count != 0)
                    //{
                    //    string Messagesd = "Request Already Approved";
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    //    return;
                    //}
                    //else
                    //{
                        string ApprovByid = hst["usrid"].ToString();
                        string Approvtrmid = hst["compname"].ToString();
                        string ApprovSession = hst["session"].ToString();
                        string approvdat = System.DateTime.Now.ToString("dd-MMM-yyyy");
                        string remarks = "Approved by Department Head";
                        // this.LeaveUpdate();
                        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "UPDATEATTAPPREQ", trnid, ApprovByid, Approvtrmid, ApprovSession, approvdat, Centrid, roletype, remarks, reqtype, "", "", "", "", "", "");
                        if (result == false)
                        {
                            string Messagesd = "Request Approved Fail";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                            return;
                        }
                        else
                        {
                            string date = this.lbldadte.Text.ToString();
                            string idcard = this.lblcard.Text.ToString();
                            string Messagesd = "Request Approved";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
                            string absapp = "0";
                            if (reqtype == "AB")
                            {
                                absapp = "1";
                                string frmdate = this.lbldadte.Text.ToString();
                                string todate = this.lbldadte.Text.ToString();
                                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPDATEOFFTIMEANDDELABSENTALL", frmdate, todate, empid, absapp, idcard, "REQ", "", "", "", "", "", "", "", "", "");
                            }

                            else if (reqtype == "LP")
                            {
                                string lateapp = "1";
                                string remarkss = "Late Present Approval";
                               
                                string frmdate =  Convert.ToDateTime(date).ToString("yyyyMMdd");
                                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATEATTLATEAPPROVAL", frmdate, empid, idcard, lateapp, remarkss, usrid, "", "", "", "", "", "", "", "");
                            }
                            else if (reqtype == "LA")
                            {
                                string lateapp = "1";
                                string remarkss = "Late Approval";
                                string frmdate = Convert.ToDateTime(date).ToString("yyyyMMdd");
                                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATEATTLATEAPPROVAL", frmdate, empid, idcard, lateapp, remarkss, usrid, "", "", "", "", "", "", "");
                            }

                            else if (reqtype == "TC")
                            {
                                absapp = "1";
                                string remarkss = "Time Correction";
                                string frmdate = Convert.ToDateTime(date).ToString("yyyyMMdd");
                                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATEATTLATEAPPROVAL", frmdate, empid, idcard, absapp, remarkss, usrid, reqtype, "", "", "", "", "", "");
                            }
                            this.getMyAttData();
                        }
                    //}
                }
                string eventdesc2 = "Details: " + htmtableboyd;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "New Request for "+ reqfor, htmtableboyd, Messaged);
            }

            
        }

        private string GetattAppId(string empid, string reqdate)
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.[SP_REPORT_HR_MGT_INTERFACE]", "GETATTAPPID", empid, reqdate, "", "", "", "", "", "", "");
            string lstid = ds5.Tables[0].Rows[0]["ltrnid"].ToString().Trim();
            return lstid;
        }
        private void SendNotificaion(string frmdate, string todate, string ltrnid, string deptcode, string compsms, string compmail, string ssl, string compName, string htmtableboyd)
        {
            try
            {
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)ViewState["tblempinfo"];
                string reqtype = this.ddlReqType.SelectedValue.ToString();
                string reqfor = reqtype == "AB" ? "Absent Approval" : reqtype == "LP" ? "Late Present Approval" : reqtype == "LA" ? "Late Approval" : "Time of Correction";
                string empid = this.GetEmpID();
                var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISERMAIL", empid, "", "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                string supphone = "";
                string suserid = ds1.Tables[0].Rows[0]["suserid"].ToString();
                string tomail = ds1.Tables[0].Rows[0]["mail"].ToString();
                string idcard = (string)ds1.Tables[1].Rows[0]["idcard"];
                string empname = (string)ds1.Tables[1].Rows[0]["name"];
                string empdesig = (string)ds1.Tables[1].Rows[0]["desig"];
                string deptname = (string)ds1.Tables[1].Rows[0]["deptname"];
                string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_81_Hrm/F_84_Lea/";
                string currentptah = "EmpLvApproval?Type=Ind&comcod=" + comcod + "&refno=" + deptcode + "&ltrnid=" + ltrnid + "&Date=" + frmdate + "&usrid=" + suserid + "&RoleType=SUP";
                string totalpath = uhostname + currentptah;
                string maildescription = "Dear Sir, Please Approve My Request." + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                     "Department Name : " + deptname + "," + "<br>" + "Request Type : " + reqfor + ",<br>" + " Request id: " + ltrnid + ". <br>";
                maildescription += htmtableboyd;
                maildescription += "<div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Request Interface</div>" + "<br/>";

                
                ///GET SMTP AND SMS API INFORMATION
                #region
                string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
                DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
                if (dssmtpandmail == null)
                    return;
                //SMTP
                string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());

                #endregion

                #region

                string subj = "New Request "+ reqfor; ;
                string msgbody = maildescription;
                
                bool result2 = UserNotify.SendNotification(subj, msgbody, suserid);

                if (compsms == "True")
                {
                    SendSmsProcess sms = new SendSmsProcess();
                    string SMSText = "New Leave Request from : " + frmdate + " To " + todate;// 
                    bool resultsms = sms.SendSmmsPwd(comcod, SMSText, supphone);
                }
                if (compmail == "True")
                {

                    bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, empname, empdesig, deptname, compName, tomail, msgbody, isSSL);
                    if (Result_email == false)
                    {
                        string Messagesd = "Request Applied but Notification has not been sent";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                string Messagesd = "Request Applied but Notification has not been sent " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }

        }
        public string GetEmpID()
        {
            string Empid = "";
            string qtype = this.Request.QueryString["Type"] ?? "";
            if (qtype == "MGT")
            {
                Empid = this.ddlEmpName.SelectedValue.ToString();
            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                Empid = (hst["empid"].ToString() == "") ? "" : hst["empid"].ToString();

            }
            return (Empid);
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            getMyAttData();
        }

        protected void hyplnkApplyLv_Click(object sender, EventArgs e)
        {      
        }
        protected void lnkbtnRefresh_Click(object sender, EventArgs e)
        {
            getMyAttData();
        }
    }
}