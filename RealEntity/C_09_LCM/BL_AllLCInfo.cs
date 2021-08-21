using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using RealERPLIB;

namespace RealEntity.C_09_LCM
{
   public class BL_AllLCInfo
    {
        ProcessAccess _userData = new ProcessAccess();
        Common ObjCommon = new Common();

        public List<RealEntity.C_09_LCM.BO_AllLCInfo.AllLCInfolist> GetAllLCInfo(string frmDate, string todate,string actcode)
        {
            List<RealEntity.C_09_LCM.BO_AllLCInfo.AllLCInfolist> lst = new List<RealEntity.C_09_LCM.BO_AllLCInfo.AllLCInfolist>();
            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_LC_INFO", "GETLCALLINFO", frmDate, todate, actcode, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_09_LCM.BO_AllLCInfo.AllLCInfolist details = new RealEntity.C_09_LCM.BO_AllLCInfo.AllLCInfolist(dr["actdesc1"].ToString(),dr["actcode"].ToString(),
                    dr["lcdate"].ToString(), dr["shipdate"].ToString(), dr["deldate"].ToString(), dr["expdate"].ToString(), dr["expdate"].ToString(),
                    Convert.ToDouble(dr["cnvrsion"].ToString()),Convert.ToDouble(dr["fcamt"].ToString()), Convert.ToDouble(dr["bdamt"].ToString()),
                    dr["bankname"].ToString(), dr["actdesc"].ToString(), dr["currency"].ToString(),
                    dr["curdesc"].ToString(), dr["csplname"].ToString(), dr["cspldesc"].ToString());
                lst.Add(details);
            }

            return lst;
        }
    }
}