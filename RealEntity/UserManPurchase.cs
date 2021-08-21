using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RealERPLIB;

namespace RealEntity
{
   public class UserManPurchase
   {


       ProcessAccess _ProAccess = new ProcessAccess();
       #region order
       public List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> GetOrderRange(string comcod)
       {

           List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange>();
           SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_PURCHASE_02", "GETORDERAPPRANGE", "", "", "", "", "", "", "", "", "");
           while (dr.Read())
           {
               RealEntity.C_14_Pro.EClassPur.EClassOrderRange details = new RealEntity.C_14_Pro.EClassPur.EClassOrderRange(dr["slnum"].ToString(),Convert.ToDouble(dr["minamt"].ToString()),Convert.ToDouble(dr["maxamt"].ToString()) );
               lst.Add(details);
           }

           return lst;



       }

       #endregion

   }
}
