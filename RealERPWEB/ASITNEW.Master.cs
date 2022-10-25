using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using RealERPWEB.Service;
using System.Web.UI.WebControls;
namespace RealERPWEB
{
    public partial class ASITNEW : System.Web.UI.MasterPage
    {
        DataTable Menus = new DataTable();

        ProcessAccess ulogin = new ProcessAccess();
        public string Label
        {
            get
            {
                return this.lblmsg.Text;
            }
            set
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            this.lblmsg.Text = "";
            //string qusrid = this.Request.QueryString["usrid"] ?? "";
            //if (qusrid.Length > 0)
            //{
            //    this.GetComNameAAdd();
            //    this.GetUserPermission();
            //    this.MasComNameAndAdd();

            //}

            if (!IsPostBack)
            {
                GetModulename();
                // new process add - nahid 20210525

                //if (comcod == "1102")
                //{
                this.MenuDynamic.Visible = true;
                //    this.oldMenu.Visible = false;


                //}
                //else
                //{
                // this.MenuDynamic.Visible = false;
                this.oldMenu.Visible = true;


                // }

                this.GetShortCut();
                this.GetAdminUserMenu();
                string comcod = this.GetCompCode();

                if ((comcod == "3365") || (comcod == "3347"))
                {
                    BindMenu();
                   // GetProFileMEnu();
                }
                else if (comcod.Substring(0, 1) == "8")
                {
                    this.WraperMain.Attributes.Add("class", "app has-fullwidth");
                    this.mySidenav.Attributes.Add("class", "app-aside ");
                    this.main.Attributes.Add("class", "app-main ml-0 ");
                    this.GroupMenu.Visible = true;



                }
                getLinkPRofile();

            }

            string Cont = "Developed by :"; //+ System.DateTime.Today.ToString("yyyy") + ".";

            var CopyRight = "<div class='copyright'>" + Cont +
                                     " <a href='http://pintechltd.com/' target='_blank'>" + "Pinovation Tech Ltd." + " </a>" +

                               " </div>";

            this.lblCopy.InnerHtml = CopyRight;

            if (Session["tbllog1"] != null)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (hst.Count == 0)
                {
                    Response.Redirect("~/ErrorHandling?Type=SDestroy");

                }
                //((Image)this.FindControl("ComLogo")).ImageUrl = "";
                this.LblGrpCompany.Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
                ((Image)this.FindControl("ComLogo")).ImageUrl = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
                this.ParentDir.Text = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;

            }
            else
            {
                Response.Redirect("~/ErrorHandling?Type=SDestroy");
            }


        }
        private void getLinkPRofile()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = (hst["usrid"].ToString());

            DataSet ds = (DataSet)Session["tblusrlog"]; 
            DataView dv = ds.Tables[1].DefaultView;
            dv.RowFilter = ("frmid = '8102113'");
            DataTable dt = dv.ToTable(); 
            if (hst == null)
            {
                return;
            }
            string userrole = hst["userrole"].ToString();
            switch (comcod)
            {
                case "3101":
                    //sidebar nav off for bti general user                    
                    //this.mySidenav.Visible = (userrole == "3" ? false : true);
                    this.HypLinkApplyLvCommon.Visible = (userrole == "3" ? false : true);
                    this.HypLinkApplyReqCommon.Visible = (userrole == "3" ? false : true);
                    this.hypUtility.Visible = (userrole == "3" ? false : true);

                    this.hypGroupChat.Visible = false;
                    this.HypOldModules.Visible = false;
                    this.lnkFormLink.Visible = true;
                    this.hypTimeOfleave.Visible = true;

                    this.HypLinkReqInterFace.Visible = true;
                    break;
                case "3338":
                case "1108":
                case "1109":
                case "3315":
                    this.HypLinkApplyLvCommon.Visible = (userrole == "3" ? false : true);
                    this.HypLinkReqInterFace.Visible = true;
                    this.HypLinkApplyReqCommon.Visible = (userrole == "3" ? false : true);
                    break;
                case "3316":
                case "3348":
                case "3364":
                case "3366": //lanco
                    this.HypLinkReqInterFace.Visible = true;
                    this.HypLinkApplyReqCommon.Visible = (userrole == "3" ? false : true);
                    break;
                case "3368": //finlay
                    this.HypLinkApplyLvCommon.Visible = (userrole == "3" ? false : true);
                    this.HypLinkApplyReqCommon.Visible = (userrole == "3" ? false : true);
                    break;

                case "3353":
                    this.HypLinkApplyReqCommon.Visible = (userrole == "3" ? false : true);
                    break;


                case "3367": //epic
                    this.HypLinkApplyLvCommon.Visible = (userrole == "3" ? false : true);
                    this.HypLinkApplyReqCommon.Visible = (userrole == "3" ? false : true);
                    this.hypGroupChat.Visible = false;
                    this.HypOldModules.Visible = false;
                    this.lnkFormLink.Visible = false;
                    this.hypTimeOfleave.Visible = false;

                    this.hypOnlineAttendance.Visible = false;
                    this.HypLinkReqInterFace.Visible = false;
                    break;
                case "3347": // Pebsteel 
                    this.HypLinkApplyLvCommon.Visible = (userrole == "3" ? true : true); /// for Common User thatswhy all are ture
                    break;

                case "3343":
                    this.HypLinkApplyLvCommon.Visible = false;
                    this.hypGroupChat.Visible = false;
                    this.HypOldModules.Visible = false;
                    this.lnkFormLink.Visible = false;
                    this.hypTimeOfleave.Visible = false;

                    this.hypOnlineAttendance.Visible = false;
                    this.HypLinkReqInterFace.Visible = false;

                    break;
                case "3354":

                    this.HypLinkApplyLvCommon.Visible = (userrole == "3" ? false : true);
                    this.HypLinkApplyReqCommon.Visible = (userrole == "3" ? false : true);
                    this.hypGroupChat.Visible = false;
                    this.HypOldModules.Visible = false;
                    this.HypLinkReqInterFace.Visible = true;

                    break;

                case "3365":

                    this.hypUtility.Visible = (((ASTUtility.Right(userid, 3) == "011") || (ASTUtility.Right(userid, 3) == "001")) ? true : false);

                    //sidebar nav off for bti general user                    
                    this.mySidenav.Visible = (userrole == "3" ? false : true);
                    this.HypLinkApplyLvCommon.Visible = (userrole == "3" ? false : true);
                    this.HypLinkApplyReqCommon.Visible = (userrole == "3" ? false : true);
                    this.hypTimeOfleaveMgt.Visible = (userrole == "3" ? false : true);
                    this.hypGroupChat.Visible = false;
                    this.HypOldModules.Visible = false;
                    this.lnkFormLink.Visible = true;
                    this.hypTimeOfleave.Visible = true;
                
                    this.HypLinkReqInterFace.Visible = true;
                    this.lnkFormLink.NavigateUrl = "https://www.facebook.com/groups/btiforum";
                     
                    break; 
                default:
                    this.lnkFormLink.Text = "Forum";
                    this.lnkFormLink.Visible = true;
                    this.HypLinkApplyLvCommon.Visible = false;
                    this.hypTimeOfleave.Visible = false;
                   
                    this.lnkFormLink.NavigateUrl = "https://www.facebook.com/pintechltd"; 
                    this.HypLinkApplyLvCommon.Visible = (userrole == "3" ? false : true);
                    this.HypLinkReqInterFace.Visible = false;
                    

                    break;
            }
            hypOnlineAttendance.Visible = (dt.Rows.Count == 0) ? false : true;

            this.HypLinkApplyLvCommon.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave?Type=MGT";       
            this.hypOnlineAttendance.NavigateUrl = "~/F_81_Hrm/F_83_Att/AttnOutOfOffice";
            this.hypTimeOfleave.NavigateUrl = "~/F_81_Hrm/F_84_Lea/TimeOfLeave?Type=Ind";
            this.hypTimeOfleaveMgt.NavigateUrl = "~/F_81_Hrm/F_84_Lea/TimeOfLeave?Type=MGT";
            this.HypLinkReqInterFace.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/InterfaceAttApp?Type=Ind";            
            // Attendance Request MGT /F_81_Hrm/F_82_App/RptMyAttendenceSheet.aspx?Type=&empid=&frmdate=26-Feb-2022&todate=25-Mar-2022
            this.HypLinkApplyReqCommon.NavigateUrl = "~/F_81_Hrm/F_82_App/RptMyAttendenceSheet?Type=MGT&empid=&frmdate=&todate=";
            this.hypUtility.NavigateUrl = "~/F_34_Mgt/CompUtilitySetUp";
        }

        private void GetComNameAAdd()
        {
            string comcod = this.GetCompCode();
            //Access Database (List View)
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("comcod = '" + comcod + "'");
            DataTable dt = dv.ToTable();
            Session["tbllog"] = dt;
            ds1.Dispose();


        }
        private void GetUserPermission()
        {
            string comcod = this.GetCompCode();

            string usrid = this.Request.QueryString["usrid"];
            string HostAddress = Request.UserHostAddress.ToString();
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSERNAMEAPASS", usrid, "", "", "", "", "", "", "", "");

            //  if()

            //  ProcessAccess ulogin = (ASTUtility.Left(this.ddlCompany.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();

            string username = ds1.Tables[0].Rows[0]["username"].ToString();
            string pass = ds1.Tables[0].Rows[0]["password"].ToString();

            //string decodepass = ASTUtility.EncodePassword(pass);

            //        string pass = ASTUtility.EncodePassword(hst["password"].ToString());
            string modulid = "AA";
            string modulename = "All Module";
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");
            Session["tblusrlog"] = ds5;

            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = new DataTable();

            //if ((DataTable)Session["tbllog1"] == null)
            // {
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
            // }

            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            // Hashtable hst = (Hashtable)Session["tblLogin"];
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
                dr2["comcod"] = comcod;
                dr2["comnam"] = dr[0]["comnam"];
                dr2["comsnam"] = dr[0]["comsnam"];
                dr2["comadd1"] = dr[0]["comadd1"];
                dr2["comadd"] = dr[0]["comadd"];

                dt2.Rows.Add(dr2);

            }
            string sessionid = (ASTUtility.RandNumber(111111, 999999)).ToString();
            hst["comcod"] = comcod;
            hst["deptcode"] = ds5.Tables[0].Rows[0]["deptcode"];

            // hst["comnam"] = ComName;
            hst["modulenam"] = "";
            hst["username"] = ds5.Tables[0].Rows[0]["usrsname"];
            hst["userfname"] = ds5.Tables[0].Rows[0]["usrname"];
            hst["compname"] = HostAddress;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["password"] = pass;
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
            if (ds5.Tables[0].Columns.Contains("comunpost"))
            {
                hst["comunpost"] = ds5.Tables[0].Rows[0]["comunpost"];
            }
            else
            {
                hst["comunpost"] = "0";
            }

            if (ds5.Tables[0].Columns.Contains("homeurl"))
            {
                hst["homeurl"] = ds5.Tables[0].Rows[0]["homeurl"];
            }
            else
            {
                hst["homeurl"] = "UserProfile";
            }


            Session["tblLogin"] = hst;
            dt2.Rows[0]["usrsname"] = ds5.Tables[0].Rows[0]["usrsname"];
            dt2.Rows[0]["session"] = sessionid;
            Session["tbllog1"] = dt2;





        }

        private void MasComNameAndAdd()
        {
            //((Image)this.Master.FindControl("ComLogo")).ImageUrl = "";
            string comcod = this.GetCompCode();
            DataTable dt1 = ((DataTable)Session["tbllog"]);
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            DataTable dt = ((DataTable)Session["tbllog1"]);
            dt.Rows[0]["comcod"] = comcod;
            Session["tbllog1"] = dt;
            ((Label)this.Master.FindControl("LblGrpCompany")).Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            //((Label)this.Master.FindControl("lbladd")).Text = (dr[0]
        }



        private void CompanyHome()
        {



            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3333"://Allaiance
                    this.sop.Attributes.Add("href", this.ResolveUrl("~/StepofOperation?moduleid="));
                    break;

                default:
                    this.sop.Attributes.Add("href", this.ResolveUrl("~/StepofOperationNew?moduleid="));
                    break;




            }
        }

        private void CompanyMaster()
        {




            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3333"://Allaiance
                    this.amaster.Attributes.Add("href", this.ResolveUrl("~/DeafultMenu?Type=" + comcod));
                    break;

                default:
                    //  this.amaster.Attributes.Add("href",   this.ResolveUrl ("~/DeafultMenu?Type=" + comcod));
                    this.limaster.Visible = false;
                    //  this.sop.Attributes.Add("href", path + "/StepofOperationNew?moduleid=");
                    break;




            }
        }
        private void GetModulename()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            // string path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;

            string usrid = hst["usrid"].ToString();
            string userrole = hst["userrole"].ToString();
            string homelink = hst["homeurl"].ToString();
            // string usrperm = "1";
            this.LogoBar.Attributes.Add("href", this.ResolveUrl("~/"+ homelink));

            //if (comcod == "3365" || comcod == "3347")

            //{
            //    string urlnorm = (userrole == "3" ? "~/UserProfile" : "~/Index?pid=");
            //    this.LogoBar.Attributes.Add("href", this.ResolveUrl(urlnorm));

            //}
            //else if (comcod.Substring(0, 1) == "8")
            //{

            //    this.LogoBar.Attributes.Add("href", this.ResolveUrl("~/F_46_GrMgtInter/RptGrpDailyReportJq?Type=Report&comcod="));

            //}
            //else
            //{
            //    this.LogoBar.Attributes.Add("href", this.ResolveUrl("~/Dashboard"));
            //}


            this.CompanyHome();
            this.CompanyMaster();
            // string path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            //this.sop.Attributes.Add("href", path + "/StepofOperationNew?moduleid=");

            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", usrid, "", "", "", "", "", "", "", "");

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("moduleid<>'AA' AND usrper='True'");
            DataTable dt = dv.ToTable();
            string imageHTML = "";
            foreach (DataRow dr in dv.ToTable().Rows)
            {
                imageHTML += @"<li class='menu-item'>
                        <a href ='" + this.ResolveUrl("~/StepofOperationNew?moduleid=" + dr["moduleid"]) + "' class='menu-link'>" + dr["modulename"] + "</a></li>";
            }

            this.Module.InnerHtml = imageHTML;
            // for interfce//
            DataSet dtint = (DataSet)Session["tblusrlog"];
            DataView dvint = dtint.Tables[1].DefaultView;
            dvint.RowFilter = ("interface='I'");
            string interfacehtml = "";

            foreach (DataRow dr in dvint.ToTable().Rows)
            {
                interfacehtml += @"<li class='menu-item'><a href ='" + this.ResolveUrl("~/" + dr["floc"] + "/" + dr["urlinf"]) + "' class='menu-link' target='_blank'>" + dr["dscrption"] + "</a></li>";
            }

            this.Interface.InnerHtml = interfacehtml;


            // for Dashboard//

            DataView dvDas = dtint.Tables[1].Copy().DefaultView;
            dvDas.RowFilter = ("interface='D'");
            string dashboardhtml = "";

            foreach (DataRow dr in dvDas.ToTable().Rows)
            {
                dashboardhtml += @"<li class='menu-item'><a href ='" + this.ResolveUrl("~/" + dr["floc"] + "/" + dr["urlinf"]) + "' class='menu-link' target='_blank'>" + dr["dscrption"] + "</a></li>";
            }
            //string
            //this.dbGraph.InnerHtml = dashboardhtml;

            //this for only HR 
            if ((comcod == "3365") || (comcod == "3347"))
            {
                lstFeturedMenu.Visible = false;
            }


            dbAllinOne.HRef = "CompanyOverAllReport?comcod=" + comcod;
            prjdash.HRef = "F_99_Allinterface/ProjectDashBoardAllNew?comcod=" + comcod;
            PrjSummary.HRef = "F_99_Allinterface/ProjectDashBoard?comcod=" + comcod;





            Designation.InnerHtml = hst["usrdesig"].ToString();
            //UserName.InnerHtml = "Hi, " + hst ["usrname"].ToString();
            UserName1.InnerHtml = hst["username"].ToString();
            UserName2.InnerHtml = hst["username"].ToString();
            UserName3.InnerHtml = hst["username"].ToString();
            userimg.ImageUrl = hst["userimg"].ToString();

        }

        private void GetShortCut()
        {
            //string path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            DataSet ds = ((DataSet)Session["tblusrlog"]);
            DataTable dt1 = ds.Tables[1];
            DataTable shrtcuttble = ds.Tables[1].Clone();
            DataTable dt3 = ds.Tables[3];
            if (dt3.Rows.Count == 0 || dt3 == null)
                return;
            foreach (DataRow dr in dt3.Rows)
            {
                DataRow[] rows = dt1.Select("frmid = " + dr["formid"] + "");
                if (rows.Length > 0)
                    shrtcuttble.ImportRow(rows[0]);
            }
            DataTable newdt = shrtcuttble;

            string MyShortCut = "";

            foreach (DataRow dr in shrtcuttble.Rows)
            {
                MyShortCut += @"<li class='menu-item'><a href ='" + this.ResolveUrl("~/" + dr["floc"] + "/" + dr["urlinf"]) + "' class='menu-link' target='_blank'>" + dr["dscrption"] + "</a></li>";
            }

            this.ShorCut.InnerHtml = MyShortCut;

        }

        private string GetCompCode()
        {

            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            
            qcomcod = qcomcod.Length > 0 ? qcomcod : hst["comcod"].ToString();
            return (qcomcod);


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string qcomcod = this.Request.QueryString["comcod"] ?? "";
            //strign comcod= qcomcod.Length>0? qcomcod:hst["comcod"].ToString()
            //return (hst["comcod"].ToString());
        }


        public string GetUserId()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            //Int32 usrid =(Int32) (hst["usrid"].ToString());

            //return (usrid);
           return (hst["usrid"].ToString());


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string qcomcod = this.Request.QueryString["comcod"] ?? "";
            //strign comcod= qcomcod.Length>0? qcomcod:hst["comcod"].ToString()
            //return (hst["comcod"].ToString());
        }
        protected void lnkbtnLedger_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnHisprice_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnTranList_Click(object sender, EventArgs e)
        {

        }

        protected void chkBoxN_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {





        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnAdd_Click1(object sender, EventArgs e)
        {

        }
        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {


        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GetAdminUserMenu()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
            {
                return;
            }
            string qusrid = this.Request.QueryString["usrid"] ?? "";
            string usrid = qusrid.Length > 0 ? qusrid : hst["usrid"].ToString();
            // string usrid = hst["usrid"].ToString();

            string adminUid = usrid.Substring(4, 3).ToString();
            if (adminUid == "001")
            {
                this.hypCompagPerm.Visible = true;
                this.hypCompagPerm.NavigateUrl = "~/F_34_Mgt/UserCompPagePrivilegesPtl";
                this.hypPagPerm.Visible = true;
                this.hypPagPerm.NavigateUrl = "~/F_34_Mgt/UserLoginfrmNew";               

            }

        }

        private void BindMenu()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
            {
                return;
            }
            

            //string comcod = this.GetCompCode();
            //string usrid = hst["usrid"].ToString();


            // string usertype = Request.QueryString["pid"] == null ? (string)Session["sesspid"] : Request.QueryString["pid"].ToString();
            //if (comcod == "1102")
            //{
            //    this.LogoBar.Attributes.Add("href", this.ResolveUrl("~/Index?pid=" + usertype));

            //}
            //else
            //{
            //   this.LogoBar.Attributes.Add("href", this.ResolveUrl("~/Index?pid="));

            //}



            //string usertype = "";//Request.QueryString["pid"]==null?"0": Request.QueryString["pid"].ToString();

            DataTable dt = (DataTable)Session["dsmenu"];

            //DataView dv = dt1.DefaultView;
            //dv.RowFilter = ("moduleid='0' or moduleid=" + usertype);
            //DataTable dt = dv.ToTable();


            //DataSet ds = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETMENULISTSIDEBAR", usrid, usertype);
            //if (ds == null)
            //    return;

            //Session["tblpagerole"] = ds;




            Menus = dt;//ds.Tables[0];

            DataView view = new DataView(Menus);
            //if (usertype != null)
            //{

            //  view.RowFilter = ("sidebar='True' and moduleid = '0'");
            // view.RowFilter = ("sidebar='True'");


            view.RowFilter = ("moduleid=0");



            DataTable dtt = view.ToTable();
            string mainmenu = "";
            int i = 0;
            foreach (DataRow dr in view.ToTable().Rows)
            {
                string cssclass = (i == 0) ? "has-active" : "";

                DataRow[] childRow = Menus.Select("ParentMenuId =" + dr["menuid"]);

                if (childRow.Count() > 0)
                {
                    //string geturl=dr["url"] + dr["qrytype"];
                    mainmenu += "<li class=\"menu-item has-child\">" +
                      "<a href=\"" + ResolveUrl(dr["url"].ToString() + dr["qrytype"].ToString()) + "\" class=\"menu-link\">" +
                        "<span class=\"" + dr["CssFont"] + "\"></span>" +
                        "<span class=\"menu-text\">" + dr["Title"] + "</span>" +
                     " </a><ul class=\"menu\">";
                    foreach (DataRow dr1 in childRow)
                    {
                        DataRow[] childRow2 = Menus.Select("ParentMenuId=" + dr1["menuid"]);

                        if (childRow2.Count() > 0)
                        {

                            mainmenu += "<li class=\"menu-item has-child\">" +
                          "<a href=\"" + ResolveUrl(dr1["url"].ToString() + dr1["qrytype"].ToString()) + "\" class=\"menu-link\"><strong>" + dr1["title"] + "</strong></a><ul class=\"menu\">";
                            foreach (DataRow dr2 in childRow2)
                            {
                                mainmenu += "<li class=\"menu-item \">" +
                           "   <a href=\"" + ResolveUrl(dr2["url"].ToString() + dr2["qrytype"].ToString()) + "\" class=\"menu-link\">" + dr2["title"] + "</a>" +
                           " </li>";
                            }
                            mainmenu += "</ul></li>";
                        }
                        else
                        {
                            mainmenu += "<li class=\"menu-item\">" +
                        "<a href=\"" + ResolveUrl(dr1["url"].ToString() + dr1["qrytype"].ToString()) + "\" class=\"menu-link\">" + dr1["title"] + "</a>" +
                      "</li>";
                        }

                    }
                    mainmenu += " </ul></li>";
                }

                else
                {
                    string menuid = dr["menuid"].ToString();
                    if (menuid == "1")
                    {
                        string homeurl = "Index?pid=";
                        mainmenu += "<li class=\"menu-item " + cssclass + "\">" +
                  "<a href=\"" + ResolveUrl(homeurl) + "\" class=\"menu-link\">" +
                    "<span class=\"" + dr["CssFont"] + "\"></span>" +
                    "<span class=\"menu-text\">" + dr["Title"] + "</span>" +
                  "</a></li>";

                    }
                    else
                    {
                        mainmenu += "<li class=\"menu-item " + cssclass + "\">" +
                  "<a href=\"" + ResolveUrl(dr["url"].ToString() + dr["qrytype"].ToString()) + "\" class=\"menu-link\">" +
                    "<span class=\"" + dr["CssFont"] + "\"></span>" +
                    "<span class=\"menu-text\">" + dr["Title"] + "</span>" +
                  "</a></li>";
                    }

                }
                i++;
            }

            this.MenuDynamic.InnerHtml = mainmenu;










            //this.rptCategories.DataSource = view;
            //this.rptCategories.DataBind();
        }

        private StringBuilder CreateChild(StringBuilder sb, string parentId, string parentTitle, DataRow[] parentRows)
        {
            if (parentRows.Length > 0)
            {
                sb.Append("<ul id='" + parentTitle + "' class='menu'>");
                foreach (var item in parentRows)
                {
                    string childId = item["MenuId"].ToString();
                    string childTitle = item["Title"].ToString();
                    string url = HostingEnvironment.ApplicationVirtualPath + item["Url"];


                    DataRow[] childRow = Menus.Select("ParentMenuId=" + childId);

                    if (childRow.Count() > 0)
                    {



                        sb.Append("<li class='menu-item has-child'><a class='menu-link' href='" + url + "'>" + item["Title"] + "</a>");
                        CreateChild(sb, childId, childTitle, childRow);

                        sb.Append("</li>");
                    }

                    else
                    {
                        sb.Append("<li class='menu-item'><a class='menu-link' href='" + url + "'><span>" + item["Title"] + "</span></a>");

                        sb.Append("</li>");
                    }
                }
                sb.Append("</ul>");

            }
            return sb;
        }

       
    }

}

