using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB
{
    public partial class login_new : System.Web.UI.Page
    {
        string ind = "x";
        public static int day = 0;
        #region Property
        public DataTable DataSource
        {
            get;
            set;
        }
        public TreeNode SelectedNode { get; set; }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.Initilize();
                this.getComName();
                this.GetHitCounter();
                this.getListModulename();
                listComName_SelectedIndexChanged(null, null);
                Session.Remove("tbllog1");
                //this.notice();
                if ((Hashtable)Session["tblLogin"] == null)
                    return;
                this.txtuserid.Text = ((Hashtable)Session["tblLogin"])["username"].ToString();
                this.txtuserpass.Text = ((Hashtable)Session["tblLogin"])["password"].ToString();
            }
            Session.Remove("tblLogin");

        }
        private void Initilize()
        {

            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GoupCompany();
            if (ds1 == null)
                return; 

        }
        private void getComName()
        {
            //Access Database (List View)
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            this.listComName.DataTextField = "comnam";
            this.listComName.DataValueField = "comcod";
            this.listComName.DataSource = ds1.Tables[0];
            this.listComName.DataBind();

            if (this.listComName.Items.Count > 0)
            {
                // DataRow[] dr = ds1.Tables[0].Select("comcod like '3%'");
                DataView dv = ds1.Tables[0].DefaultView;
                dv.RowFilter = ("comcod like '3%'");
                dv.Sort = "slnum";
                DataTable dt1 = dv.ToTable();

                if (dt1.Rows.Count == 0)
                {

                    this.listComName.SelectedIndex = 0;
                }
                else
                {
                    //nahid vs uzzal
                    this.listComName.SelectedValue = (Session["ixComcod"] == null ? dt1.Rows[0]["comcod"].ToString() : Session["ixComcod"].ToString());

                }
            }
            Session["tbllog"] = ds1.Tables[0];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;
            string comcod = hst["comcod"].ToString();
            string module = hst["modulenam"].ToString().Trim();
            if (comcod != "")
            {
                this.listComName.SelectedValue = comcod;
            }

        }

        private void GetHitCounter()
        {
            Session.Remove("tblhcntlmt");
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetHitCounter();
            DataSet ds3 = ulog.ExpDate();
            DataSet ds2 = ulog.GetHitCounterLimit();
            string comcod = this.listComName.SelectedValue.ToString();
            ProcessAccess ulogin = (ASTUtility.Left(this.listComName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGIN", "", "", "", "", "", "", "", "", "");
            Session["tblhcntlmt"] = ds2.Tables[0];


            double cnumber = Convert.ToDouble(ds1.Tables[0].Rows[0]["cnumber"]);
            double cntlim1, cntlim2, cntlim3, dcntlim1, dcntlim2, dcntlim3;
            cntlim1 = Convert.ToDouble(ds2.Tables[0].Rows[0]["cntval"]);
            cntlim2 = Convert.ToDouble(ds2.Tables[0].Rows[1]["cntval"]);
            cntlim3 = Convert.ToDouble(ds2.Tables[0].Rows[2]["cntval"]);
            dcntlim1 = Convert.ToDouble(ds5.Tables[0].Rows[0]["cntval"]);
            dcntlim2 = Convert.ToDouble(ds5.Tables[0].Rows[1]["cntval"]);
            dcntlim3 = Convert.ToDouble(ds5.Tables[0].Rows[2]["cntval"]);
            double dcnumber = Convert.ToDouble(ds5.Tables[1].Rows[0]["cnumber"]);

            DateTime dt1 = Convert.ToDateTime(ds3.Tables[0].Rows[0]["expdate"]);
            DateTime dt2 = System.DateTime.Today;
            day = ASTUtility.Datediffday(dt1, dt2);
            if (ds5.Tables[2].Rows.Count != 0)
            {
                string commsg = ds5.Tables[2].Rows[0]["commsg"].ToString();
                string msgCol = ds5.Tables[2].Rows[0]["commsgcol"].ToString(); //"text-danger";
                if (commsg.Length != 1)
                {
                    //this.pnlmsgbox.Visible = true;
                    //this.lblalrtmsg.InnerText = commsg;
                    //this.lblalrtmsg.Attributes.Add("class", msgCol + " text-justify");
                }
            }
            else
            {
                //this.pnlmsgbox.Visible = false;

            }


            if ((cnumber >= cntlim1 && cnumber < cntlim2) || (day < 10) || (dcnumber >= dcntlim1 && dcnumber < dcntlim2))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);

            }
            else if ((cnumber >= cntlim2 && cnumber < cntlim3) || (day <= 5) || (dcnumber >= dcntlim2 && cnumber < dcntlim3))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);
            }

            else if ((cnumber >= cntlim3) || (day <= 0) || (dcnumber >= dcntlim3))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);

            }
        }
        private void getListModulename()
        {

            string comcod = this.listComName.SelectedValue.ToString();
            ProcessAccess ulogin = new ProcessAccess();
            
            DataSet ds51 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", "", "", "", "", "", "", "", "", "");           
            this.ListModulename.DataTextField = "modulename";
            this.ListModulename.DataValueField = "moduleid";
            this.ListModulename.DataSource = ds51.Tables[0];
            this.ListModulename.DataBind();
            ViewState["tblmoduleName"] = ds51.Tables[0];
        }
        protected void listComName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MasComNameaAdd();
        }
        private void MasComNameaAdd()
        { 
            this.Image1.ImageUrl = "~/Image/" + "LOGO" + this.listComName.SelectedValue.ToString() + ".PNG";
            compName.InnerText = this.listComName.SelectedItem.ToString();
        }


        protected void loginBtn_Click(object sender, EventArgs e)
        {


            try
            {

                UserLogin ulog = new UserLogin();
                DataSet ds1 = ulog.GetHitCounter();
                string comcod = this.listComName.SelectedValue.ToString();
                this.setCookieFieldName(comcod);
                ProcessAccess ulogin = (ASTUtility.Left(this.listComName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();
                DataSet ds51 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGIN", "", "", "", "", "", "", "", "", "");

                DataTable ass = (DataTable)Session["tblhcntlmt"];

                double cnumber = Convert.ToDouble(ds1.Tables[0].Rows[0]["cnumber"]);
                double dcnumber = Convert.ToDouble(ds51.Tables[1].Rows[0]["cnumber"]);
                double cntlim3, dcntlim3;
                cntlim3 = Convert.ToDouble(((DataTable)Session["tblhcntlmt"]).Rows[2]["cntval"]);
                dcntlim3 = Convert.ToDouble(ds51.Tables[0].Rows[2]["cntval"].ToString());
                if ((cnumber >= cntlim3) || (day <= 0) || (dcnumber >= dcntlim3))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);
                    return;

                }

                Session.Remove("tblusrlog");
                Session.Remove("tbllog1");
                string username = this.txtuserid.Text.Trim();
                // string pass =this.txtuserpass.Text.Trim();
                string pass = ASTUtility.EncodePassword(this.txtuserpass.Text.Trim());
                string HostAddress = Request.UserHostAddress.ToString();
                //if (this.ChkChangePass.Checked)
                //{
                //    this.CheangePassword();
                //    return;

                //}
                string modulid = this.ListModulename.SelectedValue.ToString();
                string modulename = this.ListModulename.SelectedItem.Text.Trim();


                //string modulid = "AA";
                //string modulename = "All Module";  3D-18-68-04-53-43-70-C3-C8-17-DB-05-63-F0-E4-61
                DataSet ds5 = new DataSet();

                //if (comcod == "1102")
                //{
                //    ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER_NAHID", username, pass, modulid, modulename, "", "", "", "", "");

                //}
                // else
                //  {
                ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");

                // }






                if (ds5.Tables[0].Rows.Count == 1)
                {
                    FormsAuthentication.RedirectFromLoginPage(username, false);
                    //FormsAuthentication.SignOut();



                }
                else
                {
                    //this.lblmsg.Visible = true;
                    //this.lblmsg.Text = "Please Enter Correct Name & Password";
                    return;
                }



                //Hit counter Update
                //  bool result=ulog.UpdateHitCounter((cnumber + 1).ToString());
                //ulog.UpdateHitCounter((cnumber + 1).ToString());
                //if (!reuslt)
                //{
                // ((Label)this.Master.FindControl("lblmsg")).Text = ulog.ErrorObject["Msg"].ToString();
                //    return;

                //}


                //

                bool result = ulogin.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "UPDATEHCNTLMT", (dcnumber + 1).ToString(),
                                 "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {

                    return;

                }


                if (Request.QueryString["index"] != null)
                    ind = Request.QueryString["index"].ToString();

                if (Request.QueryString["index"] == null)
                    ind = this.ListModulename.SelectedIndex.ToString();


                string Comcode = this.listComName.SelectedValue.ToString();
                string ComName = this.listComName.SelectedItem.Text;

                string Mname = this.ListModulename.SelectedValue.ToString();
                DataTable dt1 = (DataTable)Session["tbllog"];
                DataTable dt2 = new DataTable();
                if ((DataTable)Session["tbllog1"] == null)
                {
                    dt2.Columns.Add("comcod", Type.GetType("System.String"));
                    dt2.Columns.Add("comnam", Type.GetType("System.String"));
                    dt2.Columns.Add("comsnam", Type.GetType("System.String"));
                    dt2.Columns.Add("comadd1", Type.GetType("System.String"));
                    dt2.Columns.Add("comadd", Type.GetType("System.String"));
                    dt2.Columns.Add("usrsname", Type.GetType("System.String"));
                    dt2.Columns.Add("session", Type.GetType("System.String"));
                    dt2.Columns.Add("compsms", Type.GetType("System.String"));
                    dt2.Columns.Add("compmail", Type.GetType("System.String"));


                    Session["tbllog1"] = dt2;
                }
                //COMPSMS
                DataTable dtr = (DataTable)Session["tbllog1"];
                DataRow[] dr = dt1.Select("comcod='" + Comcode + "'");
                Hashtable hst = new Hashtable();
                if (dr.Length > 0)
                {

                    hst["comnam"] = dr[0]["comnam"];
                    hst["comnam"] = dr[0]["comnam"];
                    hst["comsnam"] = dr[0]["comsnam"];
                    hst["comadd1"] = dr[0]["comadd1"];
                    hst["comweb"] = dr[0]["comadd3"];
                    hst["combranch"] = dr[0]["combranch"];
                    hst["comadd"] = dr[0]["comadd"];


                    DataRow dr2 = dt2.NewRow();
                    dr2["comcod"] = Comcode;
                    dr2["comnam"] = dr[0]["comnam"];
                    dr2["comsnam"] = dr[0]["comsnam"];
                    dr2["comadd1"] = dr[0]["comadd1"];
                    dr2["comadd"] = dr[0]["comadd"];

                    dt2.Rows.Add(dr2);

                }
                string sessionid = (ASTUtility.RandNumber(111111, 999999)).ToString();
                hst["comcod"] = Comcode;
                hst["deptcode"] = ds5.Tables[0].Rows[0]["deptcode"];
                hst["dptdesc"] = ds5.Tables[0].Rows[0]["dptdesc"];

                // hst["comnam"] = ComName;
                hst["modulenam"] = "";
                hst["username"] = ds5.Tables[0].Rows[0]["usrsname"];
                hst["userfname"] = ds5.Tables[0].Rows[0]["usrname"];
                hst["compname"] = HostAddress;
                hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
                hst["password"] = this.txtuserpass.Text;
                hst["session"] = sessionid;
                hst["trmid"] = "";
                hst["commod"] = "1";
                hst["compsms"] = ds5.Tables[0].Rows[0]["compsms"];
                hst["ssl"] = ds5.Tables[0].Rows[0]["ssl"];
                hst["opndate"] = ds5.Tables[0].Rows[0]["opndate"];
                hst["empid"] = ds5.Tables[0].Rows[0]["empid"];
                hst["teamid"] = ds5.Tables[0].Rows[0]["teamid"];
                hst["mcomcod"] = ds5.Tables[5].Rows[0]["mcomcod"];
                hst["usrdesig"] = ds5.Tables[0].Rows[0]["usrdesig"];
                hst["events"] = ds5.Tables[0].Rows[0]["eventspanel"];
                hst["usrrmrk"] = ds5.Tables[0].Rows[0]["usrrmrk"];
                hst["userrole"] = ds5.Tables[0].Rows[0]["userrole"];
                hst["compmail"] = ds5.Tables[0].Rows[0]["compmail"];
                hst["userimg"] = ds5.Tables[0].Rows[0]["imgurl"];
                hst["ddldesc"] = ds5.Tables[0].Rows[0]["ddldesc"];
                //hst["logowidth"] = ds5.Tables[0].Rows[0]["logowidth"];
                //hst["logoheight"] = ds5.Tables[0].Rows[0]["logoheight"];



                // hst["permission"] = ds5.Tables[0].Rows[0]["permission"];
                Session["tblLogin"] = hst;
                dt2.Rows[0]["usrsname"] = ds5.Tables[0].Rows[0]["usrsname"];
                dt2.Rows[0]["session"] = sessionid;

                Session["tbllog1"] = dt2;


                // nahid new menu
                string userid = ds5.Tables[0].Rows[0]["usrid"].ToString();
                string usertype = "0";

                DataSet dsmenu = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER_NAHID", username, pass, modulid, modulename, "", "", "", "", "");

                DataSet dsmodule = ulogin.GetTransInfo(comcod, "SP_UTILITY_USER_DASHBOARD", "GETMODULELIST", userid, "", "", "", "", "", "", "", "");
                //  DataSet dsmenu = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETMENULISTSIDEBAR", userid, usertype);

                Session["dsmenu"] = dsmenu.Tables[1];
                Session["dsmodule"] = dsmodule.Tables[0];

                Session["tblusrlog"] = ds5;

                //Log Report

                //Company Logstatus
                ConstantInfo.LogStatus = Convert.ToBoolean(ds5.Tables[0].Rows[0]["logstatus"]);
                //string eventtype = ""
                //string eventdesc = "Login into the system";
                //string eventdesc2 = "";
                //bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);





                string eventtype = "1";
                string eventdesc = "Login into the system";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                string Url1 = "";

                string userrole = ds5.Tables[0].Rows[0]["userrole"].ToString();

                string masterurl = (ds5.Tables[4].Rows.Count == 0) ? "" : ds5.Tables[4].Rows[0]["url"].ToString();
                String hrmodule = dsmodule.Tables[1].Rows.Count == 0 ? "" : dsmodule.Tables[1].Rows[0]["moduleid"].ToString();

                string dptcod = ds5.Tables[0].Rows[0]["deptcode"].ToString().Substring(0, 4);

                if (userrole == "2")
                {
                    Url1 = "AllGraph";
                }

                else if (userrole == "3" && hrmodule == "81")
                {
                    //use nahid for crm users 
                    string crmlink = "F_99_Allinterface/CRMDashboard";
                    Url1 = "UserProfile";
                    Url1 = dptcod == "9402" ? crmlink : Url1;
                }

                else if (userrole == "4" && hrmodule == "81")
                {
                    Url1 = "DashboardHRM_NEW";

                }
                else if (comcod.Substring(0, 1) == "8")
                {
                    Url1 = "F_46_GrMgtInter/RptGrpDailyReportJq?Type=Report&comcod=";
                }


                else
                {
                    if (masterurl != "")
                    {
                        Url1 = ds5.Tables[4].Rows[0]["url"].ToString();
                    }
                    else
                    {
                        if (comcod == "3333")
                        {
                            Url1 = "DeafultMenu?Type=3333";
                        }

                        else if (comcod == "3335")
                        {
                            Url1 = "MyDashboard?Type=5020";
                        }
                        else if (comcod == "3349")
                        {
                            Url1 = "MyDashboard?Type=5500";
                        }
                        else if (comcod == "3109")
                        {
                            Url1 = "MyDashboard?Type=5019";
                        }
                        else if (comcod == "3347")
                        {
                            Url1 = "HrWinMenu";
                        }
                        else
                        {
                            Url1 = "MyDashboard?Type=";
                            string UComcode = ASTUtility.Left(Comcode, 1);
                            if (UComcode == "3")
                            {
                                Url1 += "5000";
                            }
                            else if (UComcode == "4")
                            {
                                Url1 += "7000";
                            }
                            else
                            {
                                Url1 += "5000";

                            }
                        }

                    }
                }

                Response.Redirect(Url1, false);


            }
            catch (Exception ex)
            {
                //this.lblmsg.Visible = true;
                //this.lblmsg.Text = ex.Message;



            }

            //Response.Redirect("ASITDefault");

        }
        private void setCookieFieldName(string comcod)
        {
            //Create a Cookie with a suitable Key.
            HttpCookie nameCookie = new HttpCookie("MRF");
            string refno = "";
            switch (comcod)
            {
                case "2305":
                case "3305":
                case "3306":
                case "3310":
                case "3311":
                case "3315":
                case "3325":
                case "3353":
                case "3101":
                case "3364":
                    refno = "MPR No";
                    break;
                default:
                    refno = "MRF No";
                    break;

            }
            //Set the Cookie value.
            nameCookie.Values["MRF"] = refno;

            //Set the Expiry date.
            nameCookie.Expires = DateTime.Now.AddDays(30);

            //Add the Cookie to Browser.
            Response.Cookies.Add(nameCookie);
        }
    }
}