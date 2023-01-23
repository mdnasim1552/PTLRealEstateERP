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
    public partial class ProjectDashBoardAllNew : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  this.Master.FindControl("printpart").Visible = false;
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Status At a Glance";
                //this.Master.Page.Title = "Project Status At a Glance";

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("=")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('=') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp+1), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                getprojmodule();

            }
        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //return (hst["comcod"].ToString());
            string qcomcod = this.Request.QueryString["comcod"]??"";
            string comcod = qcomcod.Length>0? qcomcod: hst["comcod"].ToString();
            return (comcod);

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
            this.lblopndate.Text = (ds1.Tables[1].Rows[0]["opndat"]).ToString();
        }



        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string comcodi, string projcode)
        {

            try
            {


                ProcessAccess purData = new ProcessAccess();
                string comcod = comcodi;
                string projectcode = "18" + ASTUtility.Right(projcode, 10);
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string fdate = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(fdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string lblvalstartdate, lblvalconsarea, lblvalstoried, lblvallandarea, lblvalhandoverdate, lblvalsalablearea, lblvallocation, lblprojname;

                DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_MIS05", "RPTPRODETAILSINFO", projcode, date, todate, "", "", "", "", "", "");

                if (ds == null) { return ""; };
                var lst = ds.Tables[0].DataTableToList<TBLTAB101>();

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

                var datalist = new MyAllData(lst, txt);
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(datalist);
                return json;
            }

            catch (Exception ex)
            {

                return "";
            }

        }


        public class MyAllData
        {
            public List<TBLTAB101> TBLTAB101 { get; set; }
            public List<string> TBLTAB102 { get; set; }


            public MyAllData()
            {

            }
            public MyAllData(List<TBLTAB101> TBLTAB101, List<string> TBLTAB102)
            {
                this.TBLTAB101 = TBLTAB101;
                this.TBLTAB102 = TBLTAB102;


            }
        }



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



    }
}



