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
    public class UserManMAccount
    {

        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();



        public List<RealEntity.EClassResource> ShowResource( string Serchoption)
        {
            List<RealEntity.EClassResource> lst = new List<RealEntity.EClassResource>();

            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETSUPPLIER", Serchoption, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.EClassResource details = new RealEntity.EClassResource(dr["sircode"].ToString(), dr["sirdesc"].ToString());
                lst.Add(details);
            }

            return lst;



        }


        public List<RealEntity.C_17_Acc.EClassSupChequeSt> ShowSupChequeSt( string frmdate, string todate,string Rescode)
        {
            List<RealEntity.C_17_Acc.EClassSupChequeSt> lst = new List<RealEntity.C_17_Acc.EClassSupChequeSt>();

            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "CHQISSUEVSCLEARDWISE", frmdate, todate, Rescode, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_17_Acc.EClassSupChequeSt details = new RealEntity.C_17_Acc.EClassSupChequeSt(dr["vounum1"].ToString(), dr["isunum"].ToString(), dr["chequeno"].ToString(), dr["voudat1"].ToString(), dr["chequedat"].ToString(), Convert.ToDouble(dr["chequeam"].ToString()), dr["clchequeno"].ToString(), dr["cldate"].ToString(), Convert.ToDouble(dr["clchequeam"].ToString()), dr["actdesc"].ToString(), dr["cactdesc"].ToString(), dr["resdesc"].ToString());
                lst.Add(details);
            }

            return lst;



        }
    }
}
