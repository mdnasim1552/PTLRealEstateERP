using RealERPLIB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity
{
     public  class UserManLand
    {


        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
       



        public List<EClassCommon.EClassProject> GetProjectName(string comcod, string SearchProject)
        {
            List<EClassCommon.EClassProject> lst = new List<EClassCommon.EClassProject>();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_LPROCUREMENT", "GETLANDPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                EClassCommon.EClassProject details = new EClassCommon.EClassProject(dr["actcode"].ToString(), dr["actdesc"].ToString());
                lst.Add(details);
            }

            return lst;



        }

        public List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo> GetLandInventory(string comcod, string pactcode)
        {
            List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo> lst = new List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo>();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_LPROCUREMENT", "SHOWPROJECTLANDINFO", pactcode, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {




                RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo details = new RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo(dr["gcod"].ToString(), dr["lsircode"].ToString(), dr["ocsdhagno"].ToString(), dr["csdhagno"].ToString(), dr["osadhagno"].ToString(), dr["sadhagno"].ToString(), dr["rsdhagno"].ToString(), dr["bsdhagno"].ToString(), dr["bskhotianno"].ToString(),Convert.ToDouble(dr["cslarea"].ToString()), Convert.ToDouble(dr["bslarea"].ToString()), Convert.ToDouble(dr["bsklarea"].ToString()), dr["jblrefno"].ToString(), Convert.ToDouble(dr["budarea"].ToString()), Convert.ToDouble(dr["purarea"].ToString()), Convert.ToDouble(dr["restlarea"].ToString()));
                lst.Add(details);
            }

            return lst;



        }


        


    }
}
