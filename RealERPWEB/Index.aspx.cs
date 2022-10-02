using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB
{
    public partial class Index : System.Web.UI.Page
    {
        ProcessAccess ulogin = new ProcessAccess();
        DataTable tbl_component = new DataTable();
        DataTable tbl_tdwk = new DataTable();
        DataTable tbl_topactivity = new DataTable();
        DataTable tbl_offlineUser = new DataTable();

        Common compUtility = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "ERP DASHBOARD";
                this.ParentDir.Text = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                (this.Master.FindControl("DDPrintOpt")).Visible = false;
                (this.Master.FindControl("lnkPrint")).Visible = false;
                this.GetCompCode();



                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtDateFrom.Text = "01" + date.Substring(2);
                this.txtDateto.Text = date;


                string comcod = this.GetCompCode();
                if (comcod == "3365" || comcod == "3347")
                {
                     filterData.Attributes["class"] = "d-none";
                }

                if (comcod.Substring(0, 1) == "8")
                {
                    this.div_groupUSers.Visible = true;
                    this.getComName();
                    this.ddlCompcode_SelectedIndexChanged(null, null);
                }
                
                else
                {
                    this.div_groupUSers.Visible = false;
                    this.getComponnet();
                    this.getGraphComponent();
                    this.getUserLogData();
                    this.getHomeWidget();
                    //ddlyearSale.SelectedIndex = 1;
                    this.ddlyearSale_SelectedIndexChanged(null, null);
                }

                this.Get_Events();



            }
        }
        private void getComName()
        {
            //Access Database (List View)
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("comcod not like '8%'");
            dv.Sort = "slnum";
            DataTable dt1 = dv.ToTable();



            this.ddlCompcode.DataTextField = "comnam";
            this.ddlCompcode.DataValueField = "comcod";
            this.ddlCompcode.DataSource = dt1;
            this.ddlCompcode.DataBind();


        }

        protected void getComponnet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();

            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string fdate = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            fdate = startdate + fdate.Substring(2);
            string tdate = Convert.ToDateTime(fdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            // string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");



            string userrole = hst["usrrmrk"].ToString();

            DataSet ds2 = ulogin.GetTransInfo(comcod, "SP_UTILITY_USER_DASHBOARD", "GETUSERCOMPNENT", usrid, "2", fdate, tdate, "", "", "", "", "");
            if (ds2 == null)
                return;
            tbl_component = ds2.Tables[0];
            DataTable dt = ds2.Tables[0];
            string component = "";

            foreach (DataRow row in dt.Rows)
            {
                string menuid = row["MENUID"].ToString();
                if (menuid == "1252")
                {
                    DataSet ds1 = new DataSet();
                    string usercode = "0000000";
                    string logdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    DateTime ctdate = DateTime.Now;
                    ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, logdate, logdate, "%" + usrid + "%", ctdate.ToString("yyyyMMddHHmm"), "", "", "", "");
                    if (ds1 == null)
                        return;
                    string todaywrkcount = ds1.Tables[2].Rows[0]["tcount"].ToString();
                    tbl_offlineUser = ds1.Tables[3];
                    getOfflineUser();

                    string add = this.offlineUserCount.InnerHtml;
                    component += "<div class='col-12 col-sm-2 col-lg-2'><div class='card-metric'><a href='F_34_Mgt/RptUserLogDetails.aspx' target='_blank'><div class='metric metric_cus badge text-purpule " + row["bgcolor"] + "'><div class='has-badge'><h5 id='todaywrkcount' class='text-gray textfont16' runat='server'>" + todaywrkcount + "</h5><h2 class='metric-label text-primary'>Today Activities</h2></div></div></a></div></div><div class='col-12 col-sm-2 col-lg-2'><div class='card-metric'><a href='#' data-toggle='modal' data-target='#exampleModalDrawerRight'><div class='metric metric_cus badge text-purpule " + row["bgcolor"] + "'><div class='has-badge'><h5 id='todaywrkcount' class='text-gray textfont16' runat='server'>" + add + "</h5><h2 class='metric-label text-primary '>User Offline</h2></div></div></a></div></div><div class='col-12 col-sm-2 col-lg-2 d-none'><div class='card-metric'><a href='F_33_Doc/GroupChat.aspx'><div class='metric metric_cus badge text-purpule " + row["bgcolor"] + "'><div class='has-badge'><h5 id='todaywrkcount' class='text-gray textfont16' runat='server'>&nbsp</h5><h2 class='metric-label text-primary '>Task</h2></div></div></a></div></div>";

                }
                else
                {
                    string compamount = Convert.ToDouble(row["amount"]) == 0 ? "&nbsp;" : Convert.ToDouble(row["amount"]).ToString("#,##;(#,##);");
                    string compQty = Convert.ToDouble(row["qty"]) == 0 ? "<span class='  tile-lg'></span>" : "<span class='  tile tile-lg bg-gray'>" + row["qty"] + "</span>";
                    component += "<div class='col-12 col-sm-2 col-lg-2'><div class='card-metric'><a id='menuid_" + row["menuid"] + "' runat=server href='" + row["url"] + "' target='_blank'><div class='metric metric_cus badge " + row["bgcolor"] + " text-purpule'><div class='has-badge'><h5 id='id_" + row["menuid"] + "' class='text-gray textfont16' runat='server'>" + compamount + "</h5><h2 class='metric-label text-primary '>" + row["title"] + "</h2>" + compQty + "</div></div></a></div></div>";


                }


            }
            this.Usercompnent.InnerHtml = component;


        }

        protected void getGraphComponent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;

            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = ulogin.GetTransInfo(comcod, "SP_UTILITY_USER_DASHBOARD", "GETUSERGRAPHCOMPNENT", usrid, "3", fdate, "", "", "", "", "", "");
            if (ds2 == null)
                return;

            DataTable dt = ds2.Tables[0];
            string component = "";
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 0)
                {
                    component += "<li class='nav-item'><a class='nav-link show active' data-toggle='tab' href='#ContentPlaceHolder1_tab_" + row["MENUID"] + "'>" + row["title"] + "</a></li>";

                    if (row["MENUID"].ToString() == "1343")
                    {
                        tab_1343.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1232")
                    {

                        tab_1232.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1231")
                    {

                        tab_1231.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1233")
                    {

                        tab_1233.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1234")
                    {

                        tab_1234.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1235")
                    {

                        tab_1235.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1236")
                    {

                        tab_1236.Attributes["class"] = "tab-pane fade show active";

                    }
                    else
                    {
                        tab_1231.Attributes["class"] = "tab-pane fade show active";

                    }



                }
                else
                {
                    component += "<li class='nav-item'><a class='nav-link' data-toggle='tab' href='#ContentPlaceHolder1_tab_" + row["MENUID"] + "'>" + row["title"] + "</a></li>";


                }
                i++;
            }
            this.userGraph.InnerHtml = component;
            this.Hypersales.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=sales";
            this.HyperProcurement.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=Procurement";
            this.HypAccounts.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=Accounts";
            this.hypConstruction.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=Construction";
            this.lblSubContractor.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=SubContractor";
            this.hypCrmDetails.NavigateUrl = "F_21_MKT/RptSalesFunnel";


            ViewState["tblgrpMenu"] = ds2.Tables[0];

            if (dt.Rows.Count > 0)
            {
                string fxdate = System.DateTime.Today.ToString("MMM");

                // this.ddlMonths.SelectedValue = "00";//fxdate.ToString();
                //ddlyearSale_SelectedIndexChanged(null, null);

            }
            else
            {
                this.divgsraph.Visible = false;
            }

        }

        protected void getHomeWidget()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = ulogin.GetTransInfo(comcod, "SP_UTILITY_USER_DASHBOARD", "GETUSERHOMEWIDGET", usrid, "0", fdate, "", "", "", "", "", "");
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[0];

            int topuserAct = (from DataRow dr in dt.Rows
                              where (int)dr["menuid"] == 1250
                              select (int)dr["menuid"]).FirstOrDefault();

            int wlist = (from DataRow dr in dt.Rows
                         where (int)dr["menuid"] == 1251
                         select (int)dr["menuid"]).FirstOrDefault();

            int wkpresence = (from DataRow dr in dt.Rows
                              where (int)dr["menuid"] == 1256
                              select (int)dr["menuid"]).FirstOrDefault();



            //int tmntatt = (from DataRow dr in dt.Rows
            //               where (int)dr["menuid"] == 108
            //               select (int)dr["menuid"]).FirstOrDefault();
            //int empattst = (from DataRow dr in dt.Rows
            //                where (int)dr["menuid"] == 109
            //                select (int)dr["menuid"]).FirstOrDefault();


            if (topuserAct > 0)
            {

                if (tbl_topactivity.Rows.Count > 0)
                {
                    this.pnlActiveUser.Visible = true;
                    this.getActivityuser();
                }
                else
                {
                    this.pnlActiveUser.Visible = false;
                }

            }

            if (wlist > 0)
            {

                DataView dv = tbl_tdwk.DefaultView;
                dv.RowFilter = "entryid like '" + usrid + "'";
                DataTable dt1 = dv.ToTable();


                if (dt1.Rows.Count > 0)
                {
                    this.pnlTdayWork.Visible = true;
                    this.getTdayWorkList();
                }
                else
                {
                    this.pnlTdayWork.Visible = false;
                }

            }

            if (wkpresence > 0)
            {


                this.getWeeklypresence();

            }
            else
            {
                this.pnlwkpresence.Visible = false;

            }


        }

        private void getWeeklypresence()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string date = System.DateTime.Today.ToString();
            string edate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
            string xsdate = DateTime.Today.AddDays(-7).ToString();
            string sdate = Convert.ToDateTime(xsdate).ToString("dd-MMM-yyyy");

            DataSet ds1 = ulogin.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETGROUPATTENDENCE", edate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.pnlwkpresence.Visible = true;
                ViewState["tblgroupAttendace"] = ds1.Tables[0];
                this.Data_Bind();
            }

            //ViewState["tblgroupAttenPersen"] = ds1.Tables[1];

            //DataSet ds = ulogin.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETWEEKLYATTENDENCE", sdate, edate, "1", "", "", "", "", "", "");
            //if (ds == null)
            //{
            //    return;
            //}
            //ViewState["tblglWeekPre"] = ds.Tables[0];


        }

        protected void gvRptAttn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string date = System.DateTime.Today.ToString();


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvcomname = (HyperLink)e.Row.FindControl("hlnkgvcomname");



                //  string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }


                hlnkgvcomname.Font.Bold = true;
                hlnkgvcomname.Style.Add("color", "blue");

                hlnkgvcomname.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkLateElLeaveAAbs.aspx?Type=LELLAndAbsent&comcod=" + comcod + "&Date=" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy"); ;





            }
        }


        private void Data_Bind()
        {
            //  var jsonSerialiser = new JavaScriptSerializer();

            //  DataTable dt5 = (DataTable)ViewState["tblgroupAttendace"];
            //  // DataTable dt6 = (DataTable)ViewState["tblgroupAttenPersen"];
            //  this.gvRptAttn.DataSource = dt5;
            //  this.gvRptAttn.DataBind();

            //  double present = Convert.ToDouble(dt5.Rows[0]["present"].ToString());
            //  double late = Convert.ToDouble(dt5.Rows[0]["late"].ToString());
            //  double eleave = Convert.ToDouble(dt5.Rows[0]["earlyLev"].ToString());
            //  double onlaeve = Convert.ToDouble(dt5.Rows[0]["onlev"].ToString());
            //  double absent = Convert.ToDouble(dt5.Rows[0]["absnt"].ToString());

            //  this.lblpresent.Text = present.ToString("#,##0.00;(#,##0.00);");
            //  this.lbllate.Text = late.ToString("#,##0.00;(#,##0.00);");
            ////  this.lbleleave.Text = eleave.ToString("#,##0.00;(#,##0.00);");
            //  this.lblonleave.Text = onlaeve.ToString("#,##0.00;(#,##0.00);");
            //  this.lblabs.Text = absent.ToString("#,##0.00;(#,##0.00);");





            //  //string l = dt5.Rows[0]["late"].ToString();

            //  var lst9 = dt5.DataTableToList<EmpHRStatus>();
            //  var empStatusData = jsonSerialiser.Serialize(lst9);
            //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExcuteEmpStatus()", true);


        }


        private void getUserLogData()
        {
            DataSet ds1 = new DataSet();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string userrole = hst["usrrmrk"].ToString();
            string usercode = "0000000";
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string ddlyear = this.ddlyearSale.SelectedValue.ToString();
            string pdate = "01-Jan-" + ddlyear;
            ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
            if (ds1 == null)
                return;
            tbl_tdwk = ds1.Tables[0];
            //    tbl_offlineUser = ds1.Tables[3];
            tbl_topactivity = ds1.Tables[4];

        }



        private void getTdayWorkList()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            DataView dv = tbl_tdwk.DefaultView;
            dv.RowFilter = "entryid like '" + usrid + "'";
            DataTable dt1 = dv.ToTable();
            var lst9 = dt1.DataTableToList<Userdata>();
            var data9 = jsonSerialiser.Serialize(lst9);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteUserdata('" + data9 + "')", true);


        }

        private void getActivityuser()
        {

            int l = 0;
            string toactivity = "";
            string modaldata = "";
            foreach (DataRow dr in tbl_topactivity.Rows)
            {
                string url = dr["usrimg"].ToString();
                url = url.Substring(2, url.Length - 2);

                //string url = "";

                //if (dr["usrimg"] != null && dr["usrimg"].ToString() != "")
                //{

                //    byte[] ifff = (byte[])dr["usrimg"];
                //    url = "data:image;base64," + Convert.ToBase64String(ifff);
                //}
                //else
                //{
                //    url = "Content/Theme/images/avatars/human_avatar.png";
                //}



                //  Response.BinaryWrite(ifff);

                //  toactivity += "<div class='col-12 col-sm-2 col-lg-2'><div class='card-metric'><a href='F_34_Mgt/RptUserLogDetails.aspx'><div class='metric metric_cus badge text-purpule " + row["bgcolor"] + "'><div class='has-badge'><h5 id='todaywrkcount' class='text-gray textfont16' runat='server'>" + add + "</h5><h2 class='metric-label text-primary '>Today Activities</h2></div></div></a></div></div><div class='col-12 col-sm-2 col-lg-2'><div class='card-metric'><a href='#' data-toggle='modal' data-target='#exampleModalDrawerRight'><div class='metric metric_cus badge text-purpule " + row["bgcolor"] + "'><div class='has-badge'><h5 id='todaywrkcount' class='text-gray textfont16' runat='server'>" + add + "</h5><h2 class='metric-label text-primary '>User Offline</h2></div></div></a></div></div><div class='col-12 col-sm-2 col-lg-2'><div class='card-metric'><a href='F_33_Doc/GroupChat.aspx'><div class='metric metric_cus badge text-purpule " + row["bgcolor"] + "'><div class='has-badge'><h5 id='todaywrkcount' class='text-gray textfont16' runat='server'>&nbsp</h5><h2 class='metric-label text-primary '>Task</h2></div></div></a></div></div>";
                string top1 = l == 0 ? "<i class='fas fa-crown'></i>" : "";

                toactivity += @"<div id='myModal" + l + "' class='col-12 col-sm-2 col-lg-2 text-center'>" +
                              "<div class='bestemp'>" + top1 + "</div><div class='list-group-item-figure' style='display:block'>" +
                                "<div class='user-avatar'>" +
                                  "<img src ='" + url + "' alt=''></div></div>" +

                              "<div class='list-group-item-body'>" +
                                "<h4 class='list-group-item-title font-size-sm'> " + dr["usersname"] + ", <span class='text-dark font-size-sm'>" + dr["usrdesig"] + "</span> – <span class='badge badge-success'>" + dr["tcount"] + "</span></h4>" +

                              "</div></div>";

                l++;
            }
            TopActivity.InnerHtml = toactivity;

        }

        private void getOfflineUser()
        {

            string innHTML = "";
            int i = 0;
            foreach (DataRow dr in tbl_offlineUser.Rows)
            {
                string url1 = dr["usrimg"].ToString();
                url1 = url1.Substring(2, url1.Length - 2);


                //string url1 = "";
                //if (dr["usrimg"] != null && dr["usrimg"].ToString() != "")
                //{
                //    byte[] ifff = (byte[])dr["usrimg"];
                //    url1 = "data:image;base64," + Convert.ToBase64String(ifff);
                //}
                //else
                //{
                //    url1 = "Content/Theme/images/avatars/human_avatar.png";
                //}
                i++;
                innHTML += @"<div class='card mb-2'>" +
                        "<div class='card-body'>" +
                          "<div class='row align-items-center'>" +
                            "<div class='col-auto'>" +
                              "<a href = 'user-profile.html' class='user-avatar user-avatar-lg'><img src = '" + url1 + "' alt=''><span class='avatar-badge offline' title='offline'></span></a> </div>" +
                            "<div class='col'>" +
                              "<h3 class='card-title'>" +
                                "<a href = 'user-profile.html' >" + dr["usrname"] + "</a> <small class='text-muted'>@</small></h3>" +
                              "<h6 class='card-subtitle text-muted'>" + dr["usrdesig"] + "</h6> </div>" +
                            "<div class='col-auto'>" +
                              "<button type = 'button' class='btn btn-icon btn-secondary mr-1' data-toggle='tooltip' title='' data-original-title='Private message'><i class='far fa-comment-alt'></i></button>" +
                              "<div class='dropdown d-inline-block'>" +
                                "<button class='btn btn-icon btn-secondary' data-toggle='dropdown'><i class='fa fa-fw fa-ellipsis-h'></i></button>" +
                                "<div class='dropdown-menu dropdown-menu-right'>" +
                                  "<div class='dropdown-arrow'></div><button type = 'button' class='dropdown-item'>Invite to a team</button> <button type = 'button' class='dropdown-item'>Copy member ID</button>" +
                                  "<div class='dropdown-divider'></div><button type = 'button' class='dropdown-item'>Remove</button>" +
                                "</div> </div>  </div>  </div>  </div>  </div>";


            }
            this.offlineUserCount.InnerHtml = Convert.ToString(i);
            this.OfflineUsers.InnerHtml = innHTML;


        }

        private int GetCacheTimeinMinute()
        {

            int minute = 0;
            return minute;

        }
        private void ShowData()
        {

            this.pnlMonthlySales.Visible = false;
            this.pnlsalchart.Visible = true;
            this.Panel2.Visible = true;
            this.Panel1.Visible = false;

            this.Panel4.Visible = true;
            this.Panel3.Visible = false;

            this.Panel5.Visible = false;
            this.Panel6.Visible = true;

            this.Panel7.Visible = false;
            this.Panel8.Visible = true;
            this.Panel9.Visible = true;

            string gtype = this.ddlGraphtype.SelectedValue.ToString();
            // For Cache Data

            DataSet ds2 = new DataSet();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string userrole = hst["usrrmrk"].ToString();
            string usercode = "0000000";
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string ddlyear = this.ddlyearSale.SelectedValue.ToString();
            string pdate = "01-Jan-" + ddlyear;
            // string tdate = "01-Jan-" + ddlyear;

            if (Cache["dsinterface"] == null)
            {
                ds2 = ulogin.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "ALLGRAPHDASHBOARD", pdate, "", usrid, tdate, "", "", "", "", "");
                if (ds2 == null)
                    return;
                int minute = this.GetCacheTimeinMinute();
                Cache.Remove("dsinterface");
                Cache.Insert("dsinterface", ds2, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);
            }

            else
            {


                ds2 = (DataSet)Cache["dsinterface"];
                string pcomod = ds2.Tables[0].Rows.Count == 0 ? comcod : ds2.Tables[0].Rows[0]["comcod"].ToString();
                if (pcomod != comcod)
                {

                    ds2 = ulogin.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "ALLGRAPHDASHBOARD", pdate, "", usrid, tdate, "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    int minute = this.GetCacheTimeinMinute();
                    Cache.Remove("dsinterface");
                    Cache.Insert("dsinterface", ds2, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);

                }

                else
                {
                    ds2 = (DataSet)Cache["dsinterface"];

                }

            }


            //string empid ="%";
            //string prjcode ="%";
            //string professioncode = "%";
            //string sourceref = "%";

            //DataSet ds2CRM2 = ulogin.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETSALESFUNNEL", empid, pdate, prjcode, professioncode, tdate, sourceref,"","95%");
            //if(ds2CRM2==null)
            //{
            //    return;
            //}

            var jsonSerialiser = new JavaScriptSerializer();
            //if (userrole == "admin")
            //{

            var lst = ds2.Tables[0].DataTableToList<Salgraph>();
            var lst1 = ds2.Tables[1].DataTableToList<Purgraph>();
            var lst2 = ds2.Tables[2].DataTableToList<Accgraph>();
            var lst3 = ds2.Tables[3].DataTableToList<Consgraph>();
            var lst4 = ds2.Tables[4].DataTableToList<Scongraph>();

            var lst5 = ds2.Tables[5].DataTableToList<SalFunnelgraph>();// crm data old 17
            var lst6 = ds2.Tables[6].DataTableToList<LeadName>();// new class add lead name 
            var lst7 = ds2.Tables[7].DataTableToList<EmpLeadInfo>();// new class add employee lead

            var lst8 = ds2.Tables[8].DataTableToList<EmpHRStatus>();// HR data
            var lst9 = ds2.Tables[9].DataTableToList<Eclassdeptwisedata>();// HR data
            var lst10 = ds2.Tables[10].DataTableToList<EClass7daysinfo>();// HR data


            // var lst18 = ds2.Tables[18].DataTableToList<EmpHRDptStatus>();// HR data



            var data1 = jsonSerialiser.Serialize(lst1);
            var data2 = jsonSerialiser.Serialize(lst2);
            var data3 = jsonSerialiser.Serialize(lst3);
            var data = jsonSerialiser.Serialize(lst);
            var data4 = jsonSerialiser.Serialize(lst4);
            var crm = jsonSerialiser.Serialize(lst5);
            var leadname = jsonSerialiser.Serialize(lst6);
            var emplead = jsonSerialiser.Serialize(lst7);

            var hrAttn = jsonSerialiser.Serialize(lst8);
            var hrempdepwise = jsonSerialiser.Serialize(lst9);
            var last7days = jsonSerialiser.Serialize(lst10);


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + data + "','" + data1 + "','" + data2 + "','" + data3 + "','" + data4 + "','" + gtype + "','" + crm + "','" + leadname + "','" + emplead + "','" + hrAttn + "','" + hrempdepwise + "','" + last7days + "')", true);


            ds2.Dispose();


        }

        private void showDataMonthly(string months)
        {

            DataTable dt = (DataTable)ViewState["tblgrpMenu"];
            this.pnlMonthlySales.Visible = true;
            this.pnlsalchart.Visible = false;

            this.Panel2.Visible = false;
            this.Panel1.Visible = true;
            this.Panel3.Visible = true;
            this.Panel4.Visible = false;
            this.Panel5.Visible = true;
            this.Panel6.Visible = false;
            this.Panel7.Visible = true;
            this.Panel8.Visible = false;
            string gtype = this.ddlGraphtype.SelectedValue.ToString();
            // For Cache Data

            DataSet ds2 = new DataSet();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string userrole = hst["usrrmrk"].ToString();
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string ddlyear = this.ddlyearSale.SelectedValue.ToString();
            string ddlMonths = this.ddlMonths.SelectedValue.ToString();
            string pdate = "01-Jan-" + ddlyear;



            ds2 = ulogin.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "ALLGRAPHDASHBOARDMONTHLY", ddlyear, ddlMonths, usrid, pdate, tdate, "", "", "", "");
            if (ds2 == null)
                return;


            var jsonSerialiser = new JavaScriptSerializer();

            var lst = ds2.Tables[0].DataTableToList<SalesGrphDay>();
            var lst1 = ds2.Tables[1].DataTableToList<PurchaseGrphDay>();
            var lst2 = ds2.Tables[2].DataTableToList<AccoutnsGrphDay>();
            var lst3 = ds2.Tables[3].DataTableToList<ConsDayGrphDay>();
            var lst4 = ds2.Tables[4].DataTableToList<SubConGrphDay>();



            var lst5 = ds2.Tables[5].DataTableToList<SalFunnelgraph>();// crm data old 17
            var lst6 = ds2.Tables[6].DataTableToList<LeadName>();// new class add lead name 
            var lst7 = ds2.Tables[7].DataTableToList<EmpLeadInfo>();// new class add employee lead

            var lst8 = ds2.Tables[8].DataTableToList<EmpHRStatus>();// HR data

            var lst9 = ds2.Tables[9].DataTableToList<Eclassdeptwisedata>();// HR data
            var lst10 = ds2.Tables[10].DataTableToList<EClass7daysinfo>();// HR data


            //var lst6 = ds2.Tables[5].DataTableToList<EmpHRStatus>();// HR data old 17


            //var lst7 = ds2.Tables[6].DataTableToList<SalFunnelgraph>();// crm data
            //var lst17 = ds2.Tables[17].DataTableToList<EmpHRStatus>();// HR data
            //  var lst18 = ds2.Tables[18].DataTableToList<EmpHRDptStatus>();// HR data



            var dataSale = jsonSerialiser.Serialize(lst);
            var dataPur = jsonSerialiser.Serialize(lst1);
            var dataacc = jsonSerialiser.Serialize(lst2);
            var datacons = jsonSerialiser.Serialize(lst3);
            var datasubcons = jsonSerialiser.Serialize(lst4);

            var crm = jsonSerialiser.Serialize(lst5);
            var leadname = jsonSerialiser.Serialize(lst6);
            var emplead = jsonSerialiser.Serialize(lst7);

            var hrAttn = jsonSerialiser.Serialize(lst8);
            var hrempdepwise = jsonSerialiser.Serialize(lst9);
            var last7days = jsonSerialiser.Serialize(lst10);


            var ttsalemonths = lst.Select(p => p.ttlsalamtcore).Sum().ToString("#,##0;(#,##0); ");

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteMotnhsGraph('" + dataSale + "','" + dataPur + "','" + dataacc + "','" + datacons + "','" + datasubcons + "','" + gtype + "','" + crm + "','" + leadname + "','" + emplead + "','" + hrAttn + "','" + hrempdepwise + "','" + last7days + "')", true);

        }


        private void Get_Events()
        {
            string comcod = this.GetCompCode();
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GET_UPCOMMING_EVENTS", fdate, "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            string innHTML = "";
            int i = 0;
            string status = "";
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                status = (i == 0) ? "active" : "";
                innHTML += @"<div class='carousel-item " + status + "'><div class='row'><div class='col-md-1'><a href ='#' class='font-size-sm'><span class='position-relative mx-2 badge badge-primary rounded-0 '>" + dr["evtype"] + "</span></a></div><div class='col-md-10'> <a class='label font-size-sm' href='#'>" + dr["eventitle"] + "</a></div></div></div>";
                i++;
            }
            this.EventCaro.InnerHtml = innHTML;
        }

        private void GetModulename()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            //UserName.InnerHtml = "Hi, " + hst ["username"].ToString();

            if (hst["events"].ToString() == "True")
            {
                EventNotice.Visible = true;
            }
            else
            {
                EventNotice.Visible = false;
            }
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void ddlyearSale_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetGraphFilterData();

        }

        protected void ddlGraphtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGraphFilterData();

        }

        protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetGraphFilterData();
        }

        private void GetGraphFilterData()
        {
            string ddlmonths = this.ddlMonths.SelectedValue.ToString();

            if (ddlmonths == "00")
            {
                this.ShowData();
            }
            else
            {
                this.showDataMonthly(ddlmonths);
            }

        }

        //////////////////////////////// For Group Users

        protected void ddlGropuYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowDataGroupWise();
        }
        protected void ddlCompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowDataGroupWise();
        }
        protected void ddlGrpGraphtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowDataGroupWise();
        }
        private void ShowDataGroupWise()
        {
            string comcod = this.ddlCompcode.SelectedValue.ToString();
            string gtype = this.ddlGraphtype.SelectedValue.ToString();
            // For Cache Data
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            // string comcod = hst["comcod"].ToString();
            string userrole = hst["usrrmrk"].ToString();
            string usercode = "0000000";
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string ddlyear = this.ddlGropuYear.SelectedValue.ToString();
            string pdate = "01-Jan-" + ddlyear;
            // string tdate = "01-Jan-" + ddlyear;





            if (Cache["dsinterface"] == null)
            {
                ds2 = ulogin.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEALLTESTPURPOSE", pdate, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                int minute = this.GetCacheTimeinMinute();
                Cache.Remove("dsinterface");
                //Cache.Remove("dsinterface");
                // Cache.Insert("dsalllogin", ds1, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);
                Cache.Insert("dsinterface", ds2, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);
            }

            else
            {


                ds2 = (DataSet)Cache["dsinterface"];
                // ds1 = (DataSet)Cache["dsalllogin"];

                string pcomod = ds2.Tables[0].Rows.Count == 0 ? comcod : ds2.Tables[0].Rows[0]["comcod"].ToString();
                if (pcomod != comcod)
                {

                    ds2 = ulogin.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEALLTESTPURPOSE", pdate, "", "", "", "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    int minute = this.GetCacheTimeinMinute();
                    Cache.Remove("dsinterface");
                    //Cache.Remove("dsinterface");
                    // Cache.Insert("dsalllogin", ds1, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);
                    Cache.Insert("dsinterface", ds2, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);

                }

                else
                {
                    ds2 = (DataSet)Cache["dsinterface"];

                }



            }

            ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
            if (ds1 == null)
                return;
            var jsonSerialiser = new JavaScriptSerializer();

            var lst = ds2.Tables[0].DataTableToList<Salgraph>();
            var lst1 = ds2.Tables[1].DataTableToList<Purgraph>();
            var lst2 = ds2.Tables[2].DataTableToList<Accgraph>();
            var lst3 = ds2.Tables[3].DataTableToList<Consgraph>();
            var lst4 = ds2.Tables[4].DataTableToList<Scongraph>();

            var data = jsonSerialiser.Serialize(lst);
            var data1 = jsonSerialiser.Serialize(lst1);
            var data2 = jsonSerialiser.Serialize(lst2);
            var data3 = jsonSerialiser.Serialize(lst3);

            var data4 = jsonSerialiser.Serialize(lst4);


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGroupGraph('" + data + "','" + data1 + "','" + data2 + "','" + data3 + "','" + data4 + "','" + gtype + "')", true);

            this.salesView.NavigateUrl = "F_34_Mgt/RptAllDashboard.aspx?Type=Sales&comcod=" + comcod;
            this.Purchaselink.NavigateUrl = "F_34_Mgt/RptAllDashboard.aspx?Type=Purchase&comcod=" + comcod;
            this.Accountsglink.NavigateUrl = "F_34_Mgt/RptAllDashboard.aspx?Type=Accounts&comcod=" + comcod;
            this.Constructionglink.NavigateUrl = "F_34_Mgt/RptAllDashboard.aspx?Type=Construction&comcod=" + comcod;




        }

        [WebMethod]
        public static string GetTopData()
        {
            ProcessAccess ulogin = new ProcessAccess();
            string usrid = "3101001";
            string comcod = "3101";
            string usercode = "0000000";
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
            DataSet ds2 = ulogin.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEALLTESTPURPOSE", fdate, "", "", "", "", "", "", "", "");
            //DataSet ds3 = ulogin.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GET_UPCOMMING_EVENTS", fdate, "", "", "", "", "", "");
            //if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            //    return "";


            TopData list1 = new TopData();
            list1.TopActivity = ds1.Tables[2].Rows[0]["tcount"].ToString();
            list1.UserOffline = ds1.Tables[3].Rows.Count.ToString();
            list1.salGraph = ds2.Tables[0].DataTableToList<Salgraph>();
            list1.purGraph = ds2.Tables[1].DataTableToList<Purgraph>();
            list1.accGraph = ds2.Tables[2].DataTableToList<Accgraph>();
            list1.consGraph = ds2.Tables[3].DataTableToList<Consgraph>();
            list1.sconGraph = ds2.Tables[4].DataTableToList<Scongraph>();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list1);

            return json;
        }


        [Serializable]
        public class Salgraph
        {
            public string yearmon1 { get; set; }
            public decimal targtsaleamtcore { get; set; }
            public decimal ttlsalamtcore { get; set; }
        }
        [Serializable]
        public class Purgraph
        {
            public string yearmon1 { get; set; }
            public decimal ttlsalamtcore { get; set; }
            public decimal tpayamtcore { get; set; }
        }
        [Serializable]
        public class Accgraph
        {
            public string yearmon1 { get; set; }
            public decimal dramcore { get; set; }
            public decimal cramcore { get; set; }
        }
        [Serializable]
        public class Consgraph
        {
            public string yearmon1 { get; set; }
            public decimal taramtcore { get; set; }
            public decimal examtcore { get; set; }
        }
        [Serializable]
        public class Scongraph
        {
            public string yearmon1 { get; set; }
            public decimal tcbamtcore { get; set; }
            public decimal tcbpayamtcore { get; set; }
        }
        [Serializable]
        public class Userdata
        {
            public string grpdesc { get; set; }
            public decimal tcount { get; set; }
        }
        [Serializable]

        public class ManPowerStatus
        {
            public string descr { get; set; }
            public int value { get; set; }
        }


        [Serializable]
        public class TopData
        {
            public string TopActivity { get; set; }
            public string UserOffline { get; set; }

            public List<Salgraph> salGraph { get; set; }
            public List<Purgraph> purGraph { get; set; }
            public List<Accgraph> accGraph { get; set; }
            public List<Consgraph> consGraph { get; set; }
            public List<Scongraph> sconGraph { get; set; }

        }

        public class SalesGrphDay
        {
            public string yearmon1 { get; set; }
            public string mondays { get; set; }
            public double targtsaleamtcore { get; set; }
            public double ttlsalamtcore { get; set; }
            public double tarcollamtcore { get; set; }
            public double collamtcrore { get; set; }

        }
        public class PurchaseGrphDay
        {
            public string yearmon1 { get; set; }
            public string mondays { get; set; }
            public double ttlsalamt { get; set; }
            public double tpayamt { get; set; }
            public double ttlsalamtcore { get; set; }
            public double tpayamtcore { get; set; }

        }

        public class AccoutnsGrphDay
        {
            public string yearmon1 { get; set; }
            public string mondays { get; set; }
            public double ttlsalamt { get; set; }
            public double tpayamt { get; set; }
            public double ttlsalamtcore { get; set; }
            public double tpayamtcore { get; set; }

        }
        public class ConsDayGrphDay
        {
            public string yearmon1 { get; set; }
            public string mondays { get; set; }
            public double taramt { get; set; }
            public double examt { get; set; }
            public double taramtcore { get; set; }
            public double examtcore { get; set; }

        }
        public class SubConGrphDay
        {
            public string yearmon1 { get; set; }
            public string mondays { get; set; }
            public double tcbamt { get; set; }
            public double tcbpayamt { get; set; }
            public double tcbamtcore { get; set; }
            public double tcbpayamtcore { get; set; }

        }

        public class HrmWeeklyGraph
        {
            public string ymonddesc { get; set; }
            public double present { get; set; }
            public double staff { get; set; }
        }

        public class EmpHRDptStatus
        {
            public string dptdesc { get; set; }
            public double qty { get; set; }

        }



        public class EmpHRStatus
        {

            public double ttlprsnt { get; set; }

            public double ttlabs { get; set; }
            public double ttllate { get; set; }
            public double ttlleave { get; set; }
            public double earlyLevnl { get; set; }
            public double ttlearlv { get; set; }


        }
        [Serializable]
        public class SalFunnelgraph
        {
            public decimal query { get; set; }
            public decimal lead { get; set; }
            public decimal qualiflead { get; set; }
            public decimal finalnego { get; set; }
            public decimal nego { get; set; }
            public decimal win { get; set; }
            public decimal total { get; set; }
        }

        [Serializable]

        public class LeadName
        {
            public string comcod { get; set; }
            public string la { get; set; }
            public string lb { get; set; }
            public string lc { get; set; }
            public string ld { get; set; }
            public string le { get; set; }
            public string lf { get; set; }

        }

        [Serializable]

        public class EmpLeadInfo
        {
            //teamcode='0', total=0, usrname='',lead=0,qualiflead=0,finalnego=0,nego=0,win=0 
            public string teamcode { get; set; }
            public int total { get; set; }
            public string usrname { get; set; }
            public string lead { get; set; }
            public string qualiflead { get; set; }
            public string finalnego { get; set; }
            public string nego { get; set; }
            public string win { get; set; }

        }
        [Serializable]

        public class Eclassdeptwisedata
        {
            public string comcod { get; set; }
            public string deptcode { get; set; }
            public string deptname { get; set; }
            public int total { get; set; }
        }

        [Serializable]

        public class EClass7daysinfo
        {
            public string comcod { get; set; }
            public string ymonday { get; set; }
            public string ymonddesc { get; set; }
            public decimal staff { get; set; }
            public decimal present { get; set; }
            public decimal absnt { get; set; }
            public decimal oabsnt { get; set; }
            public decimal onleave { get; set; }
        }

        // hr data
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllDataHR(string date1, string date2)
        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            ProcessAccess purData = new ProcessAccess();
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HRM_DASHBOARD", "HRMDETAILSDASH", date1, date2, "", "", "", "", "", "", "");
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst2 = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst3 = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst4 = ds1.Tables[4].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst5 = ds1.Tables[5].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst6 = ds1.Tables[6].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst7 = ds1.Tables[7].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst8 = ds1.Tables[8].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            //var lst9 = ds1.Tables[9].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales> ();
            //var lst10 = ds1.Tables[10].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales> ();
            //var lst11 = ds1.Tables[11].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales> ();
            //var datalist = new MyAllData (lst, lst1, lst2, lst3, lst4, lst5, lst6, lst7, lst8, lst9, lst10, lst11);
            var datalist = new MyAllData(lst, lst1, lst2, lst3, lst4, lst5, lst6, lst7, lst8);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);
            return json;
        }


        public class MyAllData
        {
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> member { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> attendance { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> salary { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> leave { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> confirm { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> separation { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> loan { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> pffund { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> empjoining { get; set; }



            public MyAllData()
            {

            }
            public MyAllData(List<RealEntity.C_22_Sal.EClassSales_02.sales> member, List<RealEntity.C_22_Sal.EClassSales_02.sales> attendance, List<RealEntity.C_22_Sal.EClassSales_02.sales> salary, List<RealEntity.C_22_Sal.EClassSales_02.sales> leave, List<RealEntity.C_22_Sal.EClassSales_02.sales> confirm, List<RealEntity.C_22_Sal.EClassSales_02.sales> separation, List<RealEntity.C_22_Sal.EClassSales_02.sales> loan, List<RealEntity.C_22_Sal.EClassSales_02.sales> pffund, List<RealEntity.C_22_Sal.EClassSales_02.sales> empjoining)
            {
                this.member = member;
                this.attendance = attendance;
                this.salary = salary;
                this.leave = leave;
                this.confirm = confirm;
                this.separation = separation;
                this.loan = loan;
                this.pffund = pffund;
                this.empjoining = empjoining;

            }
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            GetGraphFilterData();
        }
    }
}