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
    public partial class ProjectMapView2 : System.Web.UI.Page
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

            DataSet ds1 = myobj.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETPROCESSCODE", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return "failed";
            DataSet landata = myobj.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "SHOW_LAND_DETAILS_INFO", prjcode, "", "", "", "", "", "", "", "");
            List<LandDetails> landdetails = landata.Tables[0].DataTableToList<LandDetails>();

            List<LandProcess> landprocess = ds1.Tables[0].DataTableToList<LandProcess>();

            ClassBinder maindata = new ClassBinder(landprocess, landdetails);
            var jsonSerialiser = new JavaScriptSerializer();
            var landjson = jsonSerialiser.Serialize(maindata);
            return landjson;
            //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "GetLandInfo('" + landjson + "')", true);

        }
    }

    //public class ClassBinder
    //{
    //    public List<LandProcess> prodata { get; set; }
    //    public List<LandDetails> landinfo { get; set; }
    //    public ClassBinder(List<LandProcess> prodata, List<LandDetails> landinfo)
    //    {
    //        this.prodata = prodata;
    //        this.landinfo = landinfo;
    //    }

    //}
    //public class LandProcess
    //{
    //    public string comcod { get; set; }
    //    public string gcode { get; set; }
    //    public string gdesc { get; set; }// land proc process
    //    public string gdesc2 { get; set; } // color
    //}

    //public class LandDetails
    //{
    //    public string pactcode { get; set; }
    //    public string pactdesc { get; set; }// project name 
    //    public string ssircode { get; set; }
    //    public string title { get; set; }// csdag
    //    public string csdag { get; set; }// csdag
    //    public string ownername { get; set; }// csdag
    //    public string fathername { get; set; }// 
    //    public string mothername { get; set; }// 
    //    public string owaddress { get; set; }// 
    //    public string contactno { get; set; }// 
    //    public string dalilno { get; set; }// 
    //    public string cskhatian { get; set; }// 
    //    public string landtype { get; set; }//
    //    public string landarea { get; set; }// csdag 
    //    public string procode { get; set; } // current process code
    //}
}