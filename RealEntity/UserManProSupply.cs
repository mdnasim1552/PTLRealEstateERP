using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealERPLIB;

namespace RealEntity
{
   public class UserManProSupply
    {

        DataAccess _dataAccess = new DataAccess ();
        ProcessAccess _ProAccess = new ProcessAccess ();
        private Common ObjCommon = new Common ();


        #region Supplier


        public List<RealEntity.C_14_Pro.EClassPur.LastSupplier> GetLastSupplierCode ( string comcod, string sircode )
        {

            List<RealEntity.C_14_Pro.EClassPur.LastSupplier> lst = new List<RealEntity.C_14_Pro.EClassPur.LastSupplier> ();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_PURCHASE_02", "GETLASTSUPPLIER", sircode, "", "", "", "", "", "", "", "");
            while (dr.Read ())
            {
                RealEntity.C_14_Pro.EClassPur.LastSupplier details = new RealEntity.C_14_Pro.EClassPur.LastSupplier (dr["sircode"].ToString ());
                lst.Add (details);
            }

            return lst;



        }

        #endregion
    }
}
