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

namespace RealEntity
{
    public class UserManMgtAct
    {

        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();



        public List<RealEntity.F_35_MgtAct.EClassMisOverall> ShowMgtOverall(string Date)
        {
            List<RealEntity.F_35_MgtAct.EClassMisOverall> lst = new List<RealEntity.F_35_MgtAct.EClassMisOverall>();

            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_MIS_DACT", "RPTDAILYACTIVITIES", Date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.F_35_MgtAct.EClassMisOverall details = new RealEntity.F_35_MgtAct.EClassMisOverall(dr["actcode"].ToString(), dr["actdesc"].ToString(), Convert.ToDouble(dr["amt01"].ToString()), Convert.ToDouble(dr["amt02"].ToString()), Convert.ToDouble(dr["amt03"].ToString()));
                lst.Add(details);
            }

            return lst;



        }


      
    }
}
