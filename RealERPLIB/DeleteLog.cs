using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using RealERPLIB;
using System.Net;


namespace RealERPLIB
{


    public class DeleteLog : System.Web.UI.Page
    {
        public string GetCompCode()
        {
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        public bool XmlDataInsertReq(string geno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
           // ds1.Merge(ds.Tables[2]);

            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";
            //ds1.Tables[3].TableName = "tbl4";

            ProcessAccess accData = new ProcessAccess();
            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, geno);

            if (!resulta)
            {

                return false;
            }


            return true;


        }
    }
}
