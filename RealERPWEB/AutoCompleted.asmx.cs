using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Data;
using RealERPLIB;
 

namespace RealERPWEB

{

    /// <summary>
    /// Summary description for AutoCompleted
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]



    public class AutoCompleted : System.Web.Services.WebService
    {

        ProcessAccess MISData = new ProcessAccess();


        public AutoCompleted()
        {



            //Uncomment the following line if using designed components 

        }

        [WebMethod]
        public void GetResCode(string Comcode, string ProcedureName, string CallType, string Desc1, string Desc2, string Desc3, string Desc4, string Desc5, string Desc6, string Desc7, string Desc8, string Desc9)
        {
            DataSet ds4 = MISData.GetTransInfo(Comcode, ProcedureName, CallType, Desc1, Desc2, Desc3, Desc4, Desc5, Desc6, Desc7, Desc8, Desc9);
            if (ds4 == null)
                return;
            Session["tblrescode"] = ds4.Tables[0];
        }



        [WebMethod]
        public void GetRecAndPayto(string Comcode, string ProcedureName, string CallType, string Desc1, string Desc2, string Desc3, string Desc4, string Desc5, string Desc6, string Desc7, string Desc8, string Desc9)
        {
            DataSet ds4 = MISData.GetTransInfo(Comcode, ProcedureName, CallType, Desc1, Desc2, Desc3, Desc4, Desc5, Desc6, Desc7, Desc8, Desc9);
            if (ds4 == null)
                return;
            Session["tblrecandPayto"] = ds4.Tables[0];
        }

        [WebMethod]
        public void GetProspective(string Comcode, string ProcedureName, string CallType, string Desc1, string Desc2, string Desc3, string Desc4, string Desc5, string Desc6, string Desc7, string Desc8, string Desc9)
        {
            DataSet ds4 = MISData.GetTransInfo(Comcode, ProcedureName, CallType, Desc1, Desc2, Desc3, Desc4, Desc5, Desc6, Desc7, Desc8, Desc9);
            if (ds4 == null)
                return;
            Session["tblprospective"] = ds4.Tables[0];
        }




        [WebMethod(true)]
        public string[] GetDetailsHead(string prefixText, int count)
        {
            HttpSessionState session = HttpContext.Current.Session;
            DataTable dt = ((DataTable)Session["tblrescode"]);
            string txtname = prefixText.ToString().Trim() + "%";
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("resdesc like '" + txtname + "'");
            dt = dv.ToTable();
            int rowcount = dt.Rows.Count;
            List<string> responses = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i > (rowcount - 1))
                    break;
                responses.Add(dt.Rows[i]["resdesc"].ToString());

            }
            return responses.ToArray();

        }




        [WebMethod(true)]
        public string[] GetRecandPayDetails(string prefixText, int count)
        {
            HttpSessionState session = HttpContext.Current.Session;
            DataTable dt = ((DataTable)Session["tblrecandPayto"]);
            string txtname = "%" + prefixText.ToString().Trim() + "%";
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("prdesc like '" + txtname + "'");
            dt = dv.ToTable();
            int rowcount = dt.Rows.Count;
            List<string> responses = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i > (rowcount - 1))
                    break;
                responses.Add(dt.Rows[i]["prdesc"].ToString());

            }
            return responses.ToArray();

        }


        [WebMethod(true)]
        public string[] GetRecandPayDetails02(string prefixText, int count)
        {
            HttpSessionState session = HttpContext.Current.Session;
            DataTable dt = ((DataTable)Session["tblrecandPayto"]);
            string txtname = prefixText.ToString().Trim() + "%";
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("prdesc like '" + txtname + "'");
            dt = dv.ToTable();
            int rowcount = dt.Rows.Count;
            List<string> responses = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i > (rowcount - 1))
                    break;
                responses.Add(dt.Rows[i]["prdesc"].ToString());

            }
            return responses.ToArray();

        }



        [WebMethod(true)]
        public string[] GetprospectiveDetails(string prefixText, int count)
        {
           
            DataTable dt = ((DataTable)Session["tblprospective"]);
            string txtname = prefixText.ToString().Trim() + "%";
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("prosdesc like '" + txtname + "'");
            dt = dv.ToTable();
            int rowcount = dt.Rows.Count;
            List<string> responses = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i > (rowcount - 1))
                    break;
                responses.Add(dt.Rows[i]["prdesc"].ToString());

            }
            return responses.ToArray();

        }




        [WebMethod]
        public string[] GetSuggestions(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Rows.Add(new Object[] { "amina" });
            dt.Rows.Add(new Object[] { "amika" });
            dt.Rows.Add(new Object[] { "amima" });
            dt.Rows.Add(new Object[] { "amica" });
            dt.Rows.Add(new Object[] { "amina" });
            dt.Rows.Add(new Object[] { "amika" });
            dt.Rows.Add(new Object[] { "amima" });
            dt.Rows.Add(new Object[] { "amica" });
            dt.Rows.Add(new Object[] { "amina" });
            dt.Rows.Add(new Object[] { "amika" });
            dt.Rows.Add(new Object[] { "amima" });
            dt.Rows.Add(new Object[] { "amica" });
            dt.Rows.Add(new Object[] { "binaca" });
            dt.Rows.Add(new Object[] { "binara" });
            dt.Rows.Add(new Object[] { "binama" });
            dt.Rows.Add(new Object[] { "binana" });
            string txtname = prefixText.ToString().Trim() + "%";
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("name like '" + txtname + "'");
            dt = dv.ToTable();
            List<string> responses = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                responses.Add(dt.Rows[i]["name"].ToString());
            return responses.ToArray();
        }

        //[WebMethod]
        //public static string CheckMobile(string mobile="")
        //{
        //    return "True";
        //}

        
      
    }
}
