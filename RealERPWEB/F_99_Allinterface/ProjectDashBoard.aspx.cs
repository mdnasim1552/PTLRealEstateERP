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
using RealERPLIB;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class ProjectDashBoard : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  this.Master.FindControl("printpart").Visible = false;
                //this.Master.FindControl("pnlTitle").Visible = false;

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                getprojmodule();

            }
        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //return (hst["comcod"].ToString());
            string qcomcod = this.Request.QueryString["comcod"]??"";
            string comcod = qcomcod.Length>0?qcomcod: hst["comcod"].ToString();
            return comcod;

        }
        public string GetModulevalue()
        {
            return (ddlprojname.SelectedValue.ToString());

        }
        private void getprojmodule()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETPROJECTNAME", "%%", "", "", "", "", "", "", "", "");
            this.ddlprojname.DataSource = ds1.Tables[0];
            this.ddlprojname.DataTextField = "actdesc";
            this.ddlprojname.DataValueField = "actcode";
            this.ddlprojname.DataBind();
        }



        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string comcodi, string projcode)
        {
            ProcessAccess purData = new ProcessAccess();
            string comcod = comcodi;
            string projectcode = "18" + ASTUtility.Right(projcode, 10);
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string fdate = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(fdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string lblvalstartdate, lblvalconsarea, lblvalstoried, lblvallandarea, lblvalhandoverdate, lblvalsalablearea, lblvallocation, lblprojname;

            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_MIS05", "RPTPRODETAILSINFO", projcode, date, todate, "", "", "", "", "", "");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTSALSUMMERY", projectcode, date, "9", "", "", "", "", "", "");

            string ProType = (ASTUtility.Left(comcod, 1) == "1") ? "SP_REPORT_BGDANALYSIS_01" : "SP_REPORT_BGDANALYSIS";
            string CallType = (ASTUtility.Left(comcod, 1) == "1") ? "RPTMASBGDACWORK_01" : "RPTMASBGDACWORK";
            DataSet ds2 = purData.GetTransInfo(comcod, ProType, CallType, "", "", projcode, "000", "", "", "", "", "");
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGTDUESAOVERDUES", "RPTDUESAOVERDUES", projectcode + "%", fdate, todate, "", "", "", "", "", "", "");
            DataSet ds4 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWISEPROINSDUES", projectcode, fdate, todate, "", "", "", "", "", "");

            if (ds == null) { return ""; };
            var lst = ds.Tables[0].DataTableToList<TBLTAB101>();
            var lst1 = ds1.Tables[0].DataTableToList<TBLTAB2>();
            var lst2 = ds2.Tables[0].DataTableToList<TBLTAB21>();
            var lst3 = ds3.Tables[0].DataTableToList<TBLTAB31>();
            var lst4 = ds4.Tables[1].DataTableToList<TBLTAB32>();
            //lblvalprojectname.Text = ds.Tables[2].Rows[0]["actdesc"].ToString().Substring(4);
            DataTable dtp = ds.Tables[1];
            lblvalstartdate = ((dtp.Select("gcod=01003")).Length == 0) ? "" : dtp.Select("gcod=01003")[0]["gval"].ToString();
            lblvalconsarea = ((dtp.Select("gcod=01001")).Length == 0) ? "" : dtp.Select("gcod=01001")[0]["gval"].ToString();
            lblvalstoried = ((dtp.Select("gcod=02008")).Length == 0) ? "" : dtp.Select("gcod=02008")[0]["gval"].ToString();
            lblvallandarea = ((dtp.Select("gcod=02005")).Length == 0) ? "" : dtp.Select("gcod=02005")[0]["gval"].ToString();
            lblvalhandoverdate = ((dtp.Select("gcod=01004")).Length == 0) ? "" : dtp.Select("gcod=01004")[0]["gval"].ToString();
            lblvalsalablearea = ((dtp.Select("gcod=01002")).Length == 0) ? "" : dtp.Select("gcod=01002")[0]["gval"].ToString();
            lblvallocation = ((dtp.Select("gcod=02002")).Length == 0) ? "" : dtp.Select("gcod=02002")[0]["gval"].ToString();
            lblprojname = ds.Tables[2].Rows[0]["actdesc"].ToString();


            List<string> txt = new List<string>();
            txt.Add(lblvalstartdate);
            txt.Add(lblvalconsarea);
            txt.Add(lblvalstoried);
            txt.Add(lblvallandarea);
            txt.Add(lblvalsalablearea);
            txt.Add(lblvalhandoverdate);
            txt.Add(lblvallocation);
            txt.Add(lblprojname);

            //----------------start by Safi----------------------
            DataSet dsdashboard = purData.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO_LP", "CONSTRUCTION_DASHBOARD", projcode, date, "", "", "", "", "", "");

            List<workProgessgraph> wrkproggress = dsdashboard.Tables[0].DataTableToList<workProgessgraph>();
            List<ProjectInformation> projectinfo = dsdashboard.Tables[1].DataTableToList<ProjectInformation>();

            var datalist = new MyAllData(lst, txt, lst1, lst2, lst3, lst4, wrkproggress, projectinfo);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);
            return json;

        }


        public class MyAllData
        {
            public List<TBLTAB101> TBLTAB101 { get; set; }
            public List<string> TBLTAB102 { get; set; }
            public List<TBLTAB2> TBLTAB2 { get; set; }
            public List<TBLTAB21> TBLTAB21 { get; set; }
            public List<TBLTAB31> TBLTAB31 { get; set; }
            public List<TBLTAB32> TBLTAB32 { get; set; }
            public List<workProgessgraph> wrkproggress { get; set; }
            public List<ProjectInformation> projectinfo { get; set; }
            public MyAllData()
            {

            }
            public MyAllData(List<TBLTAB101> TBLTAB101, List<string> TBLTAB102, List<TBLTAB2> TBLTAB2, List<TBLTAB21> TBLTAB21, List<TBLTAB31> TBLTAB31,
                List<TBLTAB32> TBLTAB32, List<workProgessgraph> wrkproggress, List<ProjectInformation> projectinfo)
            {
                this.TBLTAB101 = TBLTAB101;
                this.TBLTAB102 = TBLTAB102;
                this.TBLTAB2 = TBLTAB2;
                this.TBLTAB21 = TBLTAB21;
                this.TBLTAB31 = TBLTAB31;
                this.TBLTAB32 = TBLTAB32;
                this.wrkproggress = wrkproggress;
                this.projectinfo = projectinfo;
            }
        }



        protected void btnlink_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string prjcode = this.ddlprojname.SelectedValue.ToString();
            Response.Redirect("~/F_32_Mis/LinkRptConstruProgress?Type=&comcod=" + comcod + "&Pactcode=" + prjcode);
        }
        protected void lnkMob_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string prjcode = this.ddlprojname.SelectedValue.ToString();
            string gencode = "41040101";


            Response.Redirect("~/F_32_Mis/LinkConstruDashDetails?Type=&comcod=" + comcod + "&Pactcode=" + prjcode + "&Gencode=" + gencode);

        }
        protected void lnkSub_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string prjcode = this.ddlprojname.SelectedValue.ToString();
            string gencode = "41050101";
            Response.Redirect("~/F_32_Mis/LinkConstruDashDetails?Type=&comcod=" + comcod + "&Pactcode=" + prjcode + "&Gencode=" + gencode);

        }
        protected void lbkSup_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string prjcode = this.ddlprojname.SelectedValue.ToString();
            string gencode = "41060101";
            Response.Redirect("~/F_32_Mis/LinkConstruDashDetails?Type=&comcod=" + comcod + "&Pactcode=" + prjcode + "&Gencode=" + gencode);

        }
        protected void lnkOverall_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string prjcode = this.ddlprojname.SelectedValue.ToString();
            string gencode = "00000000";
            Response.Redirect("~/F_32_Mis/LinkConstruDashDetails?Type=&comcod=" + comcod + "&Pactcode=" + prjcode + "&Gencode=" + gencode);

        }
    }

    //--------------start cody by Safi-----------------

    [Serializable]

    public class workProgessgraph
    {
        public string wrktype { get; set; }
        public string wrkdesc { get; set; }
        public double budget { get; set; }
        public double execution { get; set; }
        public double parcnt { get; set; }


    }

    [Serializable]

    public class ProjectInformation
    {
        public string pactcode { get; set; }
        public string prjname { get; set; }
        public string location { get; set; }
        public string prjtype { get; set; }
        public double landareasft { get; set; }
        public string prjduration { get; set; }

        public double aprtmntunit { get; set; }
        public double aprtmntsqty { get; set; }
        public double aprtmntusqty { get; set; }
        public double aprtmntlo { get; set; }
        public double shopunit { get; set; }

        public double shopsqty { get; set; }

        public double shopusqty { get; set; }
        public double shoplo { get; set; }
        public string numofbuilding { get; set; }
        public string prjcode { get; set; }
        public string other { get; set; }
    }
    // ---------------end code by Safi------------------------------






    [Serializable]
    public class TBLTAB101
    {
        public string comcod { get; set; }
        public string grp { get; set; }
        public string grpdesc { get; set; }
        public double bgdsales { get; set; }
        public double bgdcost { get; set; }
        public double bgdmar { get; set; }
        public double salam { get; set; }
        public double collam { get; set; }
        public double cbgdcost { get; set; }
        public double cprogress { get; set; }
        public double cdelay { get; set; }
        public double pdueam { get; set; }
        public double cdueam { get; set; }
        public double invamt { get; set; }
        public double liaam { get; set; }
        public double rinflow { get; set; }
        public double routflow { get; set; }
        public double fblock { get; set; }

    }
    [Serializable]
    public class TBLTAB2
    {
        public string comcod { get; set; }
        public string cusname { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public double tusize { get; set; }
        public double tqty { get; set; }
        public double tuamt { get; set; }
        public double susize { get; set; }
        public double sqty { get; set; }
        public double srate { get; set; }
        public double suamt { get; set; }
        public double usqty { get; set; }
        public double urate { get; set; }
        public double usuamt { get; set; }
        public double disamt { get; set; }
        public double disper { get; set; }
        public double usize { get; set; }
        public string munit { get; set; }
        public string udesc { get; set; }
        public string pactdesc { get; set; }
        public string schdate { get; set; }
        public double colamt { get; set; }
        public double recvamt { get; set; }


    }
    [Serializable]
    public class TBLTAB21
    {
        public string comcod { get; set; }
        public string grp { get; set; }
        public string actcode { get; set; }
        public string rescode1 { get; set; }
        public string rescode { get; set; }
        public double bgdqty { get; set; }
        public double bgdrate { get; set; }
        public double bgdam { get; set; }
        public double devcost { get; set; }
        public double salcost { get; set; }
        public string actdesc { get; set; }
        public string resdesc { get; set; }
        public string resunit { get; set; }

    }

    [Serializable]
    public class TBLTAB31
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string custname { get; set; }
        public string udesc { get; set; }
        public double predues { get; set; }
        public double curdues { get; set; }
        public double receivable { get; set; }
        public double netdues { get; set; }
        public double recamt { get; set; }
        public string pactdesc { get; set; }
        public string mrno { get; set; }
        public string recdate { get; set; }
        public double dueins { get; set; }


    }
    [Serializable]
    public class TBLTAB32
    {
        public string code { get; set; }
        public string codedesc { get; set; }
        public string unumber { get; set; }
        public double usize { get; set; }
        public double samt { get; set; }
        public double usamt { get; set; }
        public double amount { get; set; }
        public double rate { get; set; }
        public double percnt { get; set; }


    }
}

