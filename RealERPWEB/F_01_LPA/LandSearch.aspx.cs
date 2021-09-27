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

namespace RealERPWEB.F_01_LPA
{
    public partial class LandSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


       


        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetZone(string comcod)
        {


            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "GETZONE", "", "", "", "", "", "", "", "", "", "");


            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Success", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                var lst = ds2.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EClassZDTM>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;


            }







        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetDistrict(string comcod, string zone)
        {


            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "GETDISTRICT", zone, "", "", "", "", "", "", "", "", "");


            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Success", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                var lst = ds2.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EClassZDTM>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;


            }







        }



        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetThana(string comcod, string dist)
        {


            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "GETTHANA", dist, "", "", "", "", "", "", "", "", "");


            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Success", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                var lst = ds2.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EClassZDTM>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;


            }







        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetMouza(string comcod, string thana)
        {


            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "GETMOUZA", thana, "", "", "", "", "", "", "", "", "");


            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Success", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                var lst = ds2.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EClassZDTM>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;


            }







        }



        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlthana_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {

        }
    }
}