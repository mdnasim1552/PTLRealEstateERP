using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealERPLIB;
using System.Data.SqlClient;

namespace RealEntity.C_45_GrAcc
{
    public class CompanyManager
    {
        ProcessAccess MktData = new ProcessAccess();
        public List<Companies> GetCompanyList(string comcod, string consolidate) 
        {
            List<Companies> lst = new List<Companies>();

            SqlDataReader dr = MktData.GetSqlReader(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                Companies curComp = new Companies();
                curComp.comcod = dr["comcod"].ToString();
                curComp.comsnam = dr["comsnam"].ToString();

                lst.Add(curComp);

            }

            return lst;
        }
    }
}
