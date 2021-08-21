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
using RealEntity;

namespace RealEntity.C_34_Mgt
{
    public class SalPurAccComboManager
    {
        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

        public List<SalPurAccCombo> GetSalPurAccByMon(string date, string comcod) 
        {
            List<SalPurAccCombo> SalPurAccByMonlist = new List<SalPurAccCombo>();
            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO_ALL", "MONTHWISEDASHBOARD", date, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                SalPurAccCombo monthly = new SalPurAccCombo();
                monthly.yearmon = dr["yearmon"].ToString();
                monthly.yearmon1 = dr["yearmon1"].ToString();
                monthly.ttlsalamt = Convert.ToDouble(dr["ttlsalamt"].ToString());//sales
                monthly.dram = Convert.ToDouble(dr["dram"].ToString());
                monthly.tpayamt = Convert.ToDouble(dr["tpayamt"].ToString());
                monthly.tllpuramt = Convert.ToDouble(dr["ttlpuramt"].ToString());
                monthly.collamt = Convert.ToDouble(dr["collamt"].ToString());//collection
                monthly.cram = Convert.ToDouble(dr["cram"].ToString());

                monthly.taramt = Convert.ToDouble(dr["taramt"].ToString());//Target
                monthly.examt = Convert.ToDouble(dr["examt"].ToString());

                SalPurAccByMonlist.Add(monthly);
            }

            return SalPurAccByMonlist;
        }


        public List<RealEntity.C_34_Mgt.EClassSalPurAcc> GetSalPurAccByMonLP(string date, string comcod)
        {
            List<RealEntity.C_34_Mgt.EClassSalPurAcc> lst = new List<RealEntity.C_34_Mgt.EClassSalPurAcc>();
            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO_LP", "MONTHWISEDASHBOARD", date, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_34_Mgt.EClassSalPurAcc details = new RealEntity.C_34_Mgt.EClassSalPurAcc(dr["yearmon"].ToString(), dr["yearmon1"].ToString(), Convert.ToDouble(dr["ttlsalamt"].ToString()), Convert.ToDouble(dr["collamt"].ToString()), Convert.ToDouble(dr["ttlpuramt"].ToString()), Convert.ToDouble(dr["tpayamt"].ToString()), Convert.ToDouble(dr["dram"].ToString()), Convert.ToDouble(dr["cram"].ToString()));
                lst.Add(details);
              
            }

            return lst;
        }
        //purchase
       

        //sales
       
    }
}
