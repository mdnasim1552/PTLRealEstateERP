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
   public  class UserManBudget
    {

        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();



        public List<RealEntity.C_04_Bgd.EClassProProgress> GetProProgress()
        {
            List<RealEntity.C_04_Bgd.EClassProProgress> lst = new List<RealEntity.C_04_Bgd.EClassProProgress>();

            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_PRJ_INFO", "GETPROPROGRESS", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_04_Bgd.EClassProProgress details = new RealEntity.C_04_Bgd.EClassProProgress(dr["pactcode"].ToString(), dr["pactdesc"].ToString(), Convert.ToDouble(dr["conprogress"].ToString()), dr["comdate"].ToString(), dr["catagory"].ToString());
                lst.Add(details);
            }

            return lst;



        }
    }
}
