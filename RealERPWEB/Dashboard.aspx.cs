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
using RealERPLIB;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
namespace RealERPWEB
{
    public partial class Dashboard : System.Web.UI.Page
    {
        ProcessAccess ulogin = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "ERP DASHBOARD";
                this.ParentDir.Text = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;

                this.GetCompCode();
                (this.Master.FindControl("DDPrintOpt")).Visible = false;
                (this.Master.FindControl("lnkPrint")).Visible = false;
                string comcod = this.GetCompCode();

                if (comcod.Substring(0, 1) == "8")
                {
                    this.divuser.Visible = false;
                    this.div_admin.Visible = false;
                    this.div_groupUSers.Visible = true;
                    this.getComName();
                    this.ddlCompcode_SelectedIndexChanged(null, null);

                }
                else
                {
                    this.divuser.Visible = true;
                    this.div_admin.Visible = true;
                    this.div_groupUSers.Visible = false;
                    GetModulename();
                    //   this.ShowData();
                    this.ddlyearSale_SelectedIndexChanged(null, null);
                    this.Get_Events();
                    this.getPnale();
                }
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
        protected void getPnale()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["usrrmrk"].ToString();

            //below code user wise some data
            DataSet ds = (DataSet)Session["tblusrlog"];
            DataView dv = ds.Tables[1].DefaultView;
            dv.RowFilter = ("frmid = '1702066' or frmid = '1702064'");
            DataTable dt = dv.ToTable();
            if (hst == null)
            {
                return;
            }

            if (userrole == "admin")
            {
                this.div_admin.Visible = true;
                this.divuser.Visible = false;
            }
            else
            {
                this.div_admin.Visible = false;
                this.divuser.Visible = true;
            }

            if (dt.Rows.Count > 0)
            {
                this.divPostDateChecSchdule.Visible = true;
                ShowDataPOSTDATEDCHEQUE();
            }

        }

        private void ShowDataPOSTDATEDCHEQUE()
        {


            try
            {
               
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string frmdate = System.DateTime.Today.ToString("dd-MMM-yyyy");  
                DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "REPORTCHEQUEUPDATE", frmdate, todate, "%", "0", "200", "%", "%", "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                this.divPostDateChecSchdule.Visible = ds1.Tables[0].Rows.Count == 0 ? false : true;

                this.dgv1.DataSource = ds1.Tables[0];
                this.dgv1.DataBind(); 
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private int GetCacheTimeinMinute()
        {

            int minute = 0;
            return minute;
            //DateTime.Now.Addminutes(5), TimeSpan.Zero

            // GetChase

            // if (Cache["user"] == null)
            //{
            //    Cache.Remove("user");
            //    Cache.Insert("user", "Emdadul", null, DateTime.Now.AddMinutes(2), TimeSpan.Zero);

            //}

            //else 
            //{

            //    string data = (string)Cache["user"];


            // }


        }


        private void ShowData()
        {
            string gtype = this.ddlGraphtype.SelectedValue.ToString();
            // For Cache Data
            DataSet ds1 = new DataSet();
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
            var jsonSerialiser = new JavaScriptSerializer();

            if (userrole == "admin")
            {

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
                var lst = ds2.Tables[0].DataTableToList<Salgraph>();
                var lst1 = ds2.Tables[1].DataTableToList<Purgraph>();
                var lst2 = ds2.Tables[2].DataTableToList<Accgraph>();
                var lst3 = ds2.Tables[3].DataTableToList<Consgraph>();
                var lst4 = ds2.Tables[4].DataTableToList<Scongraph>();

                var data1 = jsonSerialiser.Serialize(lst1);
                var data2 = jsonSerialiser.Serialize(lst2);
                var data3 = jsonSerialiser.Serialize(lst3);
                var data = jsonSerialiser.Serialize(lst);
                var data4 = jsonSerialiser.Serialize(lst4);
                this.divgraph.Visible = true;
                divuser.Visible = false;

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + data + "','" + data1 + "','" + data2 + "','" + data3 + "','" + data4 + "','" + gtype + "')", true);

                ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
                if (ds1 == null)
                    return;

                this.todaywrkcount.InnerHtml = ds1.Tables[2].Rows[0]["tcount"].ToString();
                //this.TaskRemaining.InnerHtml = ds1.Tables [2].Rows [0] ["tasks"].ToString();
                this.todaywrk.Attributes.Add("href", "F_34_Mgt/RptUserLogDetails");    //?Type=Entry&genno=" + usrid);

                if (ds1.Tables[3] == null)
                    return;
                //if (ds1.Tables[3] == null || ds1.Tables[3].Rows.Count == 0)
                //    return;
                string innHTML = "";
                int i = 0;
                foreach (DataRow dr in ds1.Tables[3].Rows)
                {
                    string url1 = dr["usrimg"].ToString();
                    url1 = url1.Substring(2, url1.Length - 2);
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
                string prjcount = ds1.Tables[2].Rows[0]["pcount"].ToString();
                this.offlineUserCount.InnerHtml = Convert.ToString(i);
                this.OfflineUsers.InnerHtml = innHTML;
                int l = 0;
                string toactivity = "";
                string modaldata = "";
                foreach (DataRow dr in ds1.Tables[4].Rows)
                {

                    string url = dr["usrimg"].ToString();


                    toactivity += @"<a id='myModal" + l + "' class='list-group-item list-group-item-action'>" +
                                  "<div class='list-group-item-figure'>" +
                                    "<div class='user-avatar'>" +
                                      "<img src ='" + url + "' alt=''></div></div>" +

                                  "<div class='list-group-item-body'>" +
                                    "<h4 class='list-group-item-title font-size-sm'> " + dr["usersname"] + "</h4>" +
                                    "<p class='list-group-item-text text-truncate'>" +
                                    "<span class='text-dark font-size-sm'>" + dr["usrdesig"] + "</span> – <span class='badge badge-success'>" + dr["tcount"] + "</span> </p>" +
                                  "</div></a>";


                }
                TopActivity.InnerHtml = toactivity;
                this.noProjCount.InnerHtml = prjcount;
                this.noProj.Attributes.Add("href", "F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan&comcod=" + comcod);    //?Type=Entry&genno=" + usrid);



            }
            else
            {

                ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
                if (ds1 == null)
                    return;
                divuser.Visible = true;
                this.divgraph.Visible = false;
                DataView dv = ds1.Tables[1].DefaultView;
                dv.RowFilter = "entryid like '" + usrid + "'";
                DataTable dt1 = dv.ToTable();
                var lst = dt1.DataTableToList<Userdata>();
                var data1 = jsonSerialiser.Serialize(lst);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteUserdata('" + data1 + "')", true);



            }



           
            ds1.Dispose();
            ds2.Dispose();

        }

        private void ShowData_Column()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            // For Cache Data
            DataSet ds1 = new DataSet();
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



            if (userrole == "admin")
            {
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

           
           

            
           
                var lst = ds2.Tables[0].DataTableToList<Salgraph>();
                var lst1 = ds2.Tables[1].DataTableToList<Purgraph>();
                var lst2 = ds2.Tables[2].DataTableToList<Accgraph>();
                var lst3 = ds2.Tables[3].DataTableToList<Consgraph>();
                var lst4 = ds2.Tables[4].DataTableToList<Scongraph>();

                var data1 = jsonSerialiser.Serialize(lst1);
                var data2 = jsonSerialiser.Serialize(lst2);
                var data3 = jsonSerialiser.Serialize(lst3);
                var data = jsonSerialiser.Serialize(lst);
                var data4 = jsonSerialiser.Serialize(lst4);
                this.divgraph.Visible = true;
                divuser.Visible = false;

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph_column('" + data + "','" + data1 + "','" + data2 + "','" + data3 + "','" + data4 + "')", true);

                ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
                if (ds1 == null)
                    return;
                 var listhr = ds1.Tables[5].DataTableToList<ManPowerStatus>();
                this.todaywrkcount.InnerHtml = ds1.Tables[2].Rows[0]["tcount"].ToString();
                //this.TaskRemaining.InnerHtml = ds1.Tables [2].Rows [0] ["tasks"].ToString();
                this.todaywrk.Attributes.Add("href", "F_34_Mgt/RptUserLogDetails");    //?Type=Entry&genno=" + usrid);

                if (ds1.Tables[3] == null)
                    return;
                //if (ds1.Tables[3] == null || ds1.Tables[3].Rows.Count == 0)
                //    return;
                string innHTML = "";
                int i = 0;
                foreach (DataRow dr in ds1.Tables[3].Rows)
                {

                    string url1 = dr["usrimg"].ToString();
                    url1 = url1.Length == 0 ? "" : url1.Substring(2, url1.Length - 2);


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
                string prjcount = ds1.Tables[2].Rows[0]["pcount"].ToString();

                this.offlineUserCount.InnerHtml = Convert.ToString(i);
                this.OfflineUsers.InnerHtml = innHTML;
                int l = 0;
                string toactivity = "";
                string modaldata = "";
                foreach (DataRow dr in ds1.Tables[4].Rows)
                {

                    string url = dr["usrimg"].ToString();
                    url = url.Substring(2, url.Length - 2);
                    //  Response.BinaryWrite(ifff);
                    toactivity += @"<a id='myModal" + l + "' class='list-group-item list-group-item-action'>" +
                                  "<div class='list-group-item-figure'>" +
                                    "<div class='user-avatar'>" +
                                      "<img src ='" + url + "' alt=''></div></div>" +

                                  "<div class='list-group-item-body'>" +
                                    "<h4 class='list-group-item-title font-size-sm'> " + dr["usersname"] + "</h4>" +
                                    "<p class='list-group-item-text text-truncate'>" +
                                    "<span class='text-dark font-size-sm'>" + dr["usrdesig"] + "</span> – <span class='badge badge-success'>" + dr["tcount"] + "</span> </p>" +
                                  "</div></a>";


                }
                TopActivity.InnerHtml = toactivity;
                this.noProjCount.InnerHtml = prjcount;
                this.noProj.Attributes.Add("href", "F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan&comcod=" + comcod);    //?Type=Entry&genno=" + usrid);


            }
            else
            {

                ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
                if (ds1 == null)
                    return;
                divuser.Visible = true;
                this.divgraph.Visible = false;
                DataView dv = ds1.Tables[1].DefaultView;
                dv.RowFilter = "entryid like '" + usrid + "'";
                DataTable dt1 = dv.ToTable();
                var lst = dt1.DataTableToList<Userdata>();
                var data1 = jsonSerialiser.Serialize(lst);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteUserdata('" + data1 + "')", true);


            }



            ds1.Dispose();
            ds2.Dispose();

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





        private void Get_Events()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GET_UPCOMMING_EVENTS", fdate, usrid, "", "", "", "", "");
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
            string gtype = this.ddlGraphtype.SelectedValue.ToString();
            if (gtype == "column")
            {
                ShowData_Column();
            }
            else
            {
                this.ShowData();
            }

        }
      

        protected void ddlGraphtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gtype = this.ddlGraphtype.SelectedValue.ToString();
            if (gtype == "column")
            {
                ShowData_Column();
            }
            else if (gtype == "line")
            {
                this.ShowData();
            }
            else if (gtype == "area")
            {
                this.ShowData();
            }
            else if (gtype == "pie")
            {
                this.ShowData();
            }
        }
        protected void ddlGropuYear_SelectedIndexChanged(object sender, EventArgs e)
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
            this.divgraph.Visible = true;
            divuser.Visible = false;

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGroupGraph('" + data + "','" + data1 + "','" + data2 + "','" + data3 + "','" + data4 + "','" + gtype + "')", true);

            this.salesView.NavigateUrl = "F_34_Mgt/RptAllDashboard?Type=Sales&comcod=" + comcod;
            this.Purchaselink.NavigateUrl = "F_34_Mgt/RptAllDashboard?Type=Purchase&comcod=" + comcod;
            this.Accountsglink.NavigateUrl = "F_34_Mgt/RptAllDashboard?Type=Accounts&comcod=" + comcod;
            this.Constructionglink.NavigateUrl = "F_34_Mgt/RptAllDashboard?Type=Construction&comcod=" + comcod;




        }
        protected void ddlCompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowDataGroupWise();
        }
        protected void ddlGrpGraphtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowDataGroupWise();
        }
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

}


