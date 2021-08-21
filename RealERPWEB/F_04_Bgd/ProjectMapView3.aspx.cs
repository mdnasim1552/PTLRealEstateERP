using BilSimser.GeoJson;
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
using System.Xml.Linq;
using System.IO;
namespace RealERPWEB.F_04_Bgd
{
    public partial class ProjectMapView3 : System.Web.UI.Page
    {
        ProcessAccess myobj = new ProcessAccess();
        public static string prjcode { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            // GetLandInfo();
            prjcode = Request.QueryString["prjcode"];
        }
        public static string GetMapString()
        {
            string elementToFind = "example";
            string url = HttpContext.Current.Request.MapPath("~/Upload/Map/CS-Map.kml");
            //System.IO.Stream kmlFile = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("CS-Map.kml");
            //XDocument xDoc = System.Xml.Linq.XDocument.Load(kmlFile);
            //string xNs = "{" + xDoc.Root.Name.Namespace.ToString() + "}";
            XDocument doc = XDocument.Load(url);
            string kml = "<?xml version=\"1.0\" encoding=\"UTF -8\"?> <kml xmlns=\"http://www.opengis.net/kml/2.2\"> <Placemark> <name>Simple placemark</name> <description>Attached to the ground. Intelligently places itself at the height of the underlying terrain.</description> <Point> <coordinates>-122.0822035425683,37.42228990140251,0</coordinates> </Point> </Placemark> </kml>";
            return GeoJsonConvert.Convert(doc.ToString());
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
            string mapstring = GetMapString();
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