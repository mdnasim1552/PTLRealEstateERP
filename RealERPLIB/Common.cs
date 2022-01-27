using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace RealERPLIB
{
    public sealed  class Common: System.Web.UI.Page
    {


        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        public string GetHRCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }





        public string GetDeptCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["deptcode"].ToString());

        }

        public string GetUserCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["usrid"].ToString());

        }
        public string Terminal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["compname"].ToString());

        }
        public string Sessionid()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["session"].ToString());

        }


        public DataSet GetShortCutLink()
        {
            return ((DataSet)Session["tblusrlog"]);
        }
        public bool PushSesLog(DataSet ds)
        {
            Session["tblusrlog"] = ds;
            return true;
        }
        public bool LogStatus(string eventtype, string eventdesc, string eventdesc2, string Para1)
        {
            bool IsVoucherSaved = false;
            if (ConstantInfo.LogStatus == true)
            {
                eventdesc2 = eventdesc2 + Para1;
                IsVoucherSaved = CALogRecord.AddLogRecord(this.GetCompCode(), ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }
            return IsVoucherSaved;
        }


       
    }
}
