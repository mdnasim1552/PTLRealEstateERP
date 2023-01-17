using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
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

        ProcessAccess ProData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");

                //((Label)this.Master.FindControl("lblTitle")).Text = "Land Search";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            }

        }





        public string GetCompCode()
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




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetLandInfo(string comcod, string zone, string dist, string thana, string mouza, string csdhagno)
        {

            csdhagno = (csdhagno.Length == 0 ? "" : csdhagno) + "%";
            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "SHOWLANDINFO", zone, dist, thana, mouza, csdhagno, "", "", "", "", "");


            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Success", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                var lst = ds2.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;


            }

        }


        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string zone = this.ddlZone.SelectedValue.ToString();
            string dist = this.ddldistrict.SelectedValue.ToString();
            string thana = this.ddlthana.SelectedValue.ToString();
            string mouza = this.ddlMouza.SelectedValue.ToString();
            string csdhagno = (this.txtcsdhagno.Text.Trim().Length == 0 ? "" : this.txtcsdhagno.Text.Trim()) + "%";
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "GETLANDINFO", zone, dist, thana, mouza, csdhagno, "", "", "", "", "");

        }

        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public static string   PrintLandInfo()
        //{
           

        //    var result = new { Message = "Success", result = true };
        //    var jsonSerialiser = new JavaScriptSerializer();
        //    var json = jsonSerialiser.Serialize(result);
        //    return json;

        //}
    }
}