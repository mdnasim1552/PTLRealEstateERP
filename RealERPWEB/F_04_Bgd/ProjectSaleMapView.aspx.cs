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
namespace RealERPWEB.F_04_Bgd
{
    public partial class ProjectSaleMapView : System.Web.UI.Page
    {
        ProcessAccess myobj = new ProcessAccess();
        public static string prjcode { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            // GetLandInfo();
            prjcode = Request.QueryString["prjcode"];
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetLandInfo()
        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            ProcessAccess myobj = new ProcessAccess();


            DataSet landata = myobj.GetTransInfo(comcod, "SP_REPORT_PRJ_INFO", "SHOW_PLOT_DETAILS_INFO", prjcode, "", "", "", "", "", "", "", "");
            List<PlotDetails> landdetails = landata.Tables[0].DataTableToList<PlotDetails>();

            var jsonSerialiser = new JavaScriptSerializer();
            var landjson = jsonSerialiser.Serialize(landdetails);
            return landjson;
            //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "GetLandInfo('" + landjson + "')", true);

        }
    }


    public class PlotDetails
    {
        public string pactcode { get; set; }
        public string pactdesc { get; set; }// project name 
        public string usircode { get; set; }
        public string title { get; set; }// csdag
        public string usize { get; set; }// csdag
        public string munit { get; set; }// csdag   
        public string uamt { get; set; } // 
        public string saleamt { get; set; }
        public string sstatus { get; set; }
    }
}