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

namespace RealEntity.C_14_Pro
{

    public class UserManPur
    {
        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

        ///----------BL----

        #region DashBoard_Purchase
        public List<RealEntity.C_14_Pro.EClassPur.EClassYear> ShowPurYearly(string comcod, string date)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassYear> list = new List<RealEntity.C_14_Pro.EClassPur.EClassYear>();
            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PURCHASWINFOYEAR", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassYear yearly = new RealEntity.C_14_Pro.EClassPur.EClassYear(dr["yearid"].ToString(), Convert.ToDouble(dr["ttlamt"].ToString()),
                        Convert.ToDouble(dr["purpay"].ToString()));
                list.Add(yearly);
            }
            return list;


        }


        public List<RealEntity.C_14_Pro.EClassPur.EClassMonthly> ShowPurMonth(string comcod, string date)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassMonthly> list2 = new List<RealEntity.C_14_Pro.EClassPur.EClassMonthly>();
            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PURCHASEINFYEARMONTH", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassMonthly monthly = new RealEntity.C_14_Pro.EClassPur.EClassMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(),
                        Convert.ToDouble(dr["ttlsalamt"]), Convert.ToDouble(dr["tpayamt"]), Convert.ToDouble(dr["ttlsalamtcore"]), Convert.ToDouble(dr["tpayamtcore"]) );
                list2.Add(monthly);
            }
            return list2;

        }


        public List<RealEntity.C_14_Pro.EClassPur.EClassWeekly> ShowPurWeekly(string comcod, string date1)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassWeekly> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "PURCHASEINFOWEEK", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassWeekly Weekly = new RealEntity.C_14_Pro.EClassPur.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wpamt1"]),
                  Convert.ToDouble(dr["wpayamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wpamt2"]), Convert.ToDouble(dr["wpayamt2"]), dr["wcode3"].ToString(),
                   Convert.ToDouble(dr["wpamt3"]), Convert.ToDouble(dr["wpayamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wpamt4"]), Convert.ToDouble(dr["wpayamt4"]));
                lst.Add(Weekly);
            }
            return lst;
        }

        public List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> ShowPurDayWise(string comcod, string date1)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SHOWDAYWISEBILL", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassDayWisePur Pur = new RealEntity.C_14_Pro.EClassPur.EClassDayWisePur(dr["pactcode"].ToString(), dr["pactdesc"].ToString(),
                  dr["rsircode"].ToString(), dr["rsirdesc"].ToString(),dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["billno1"].ToString(),
                   dr["billno"].ToString(), dr["billdate1"].ToString(), dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["billamt"]));
                lst.Add(Pur);
            }
            return lst;
        }

        public List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> ShowPayDayWise(string comcod, string date1)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SHOWDAYWISEPAY", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassDayWisePay Pay = new RealEntity.C_14_Pro.EClassPur.EClassDayWisePay(dr["pactcode"].ToString(), dr["pactdesc"].ToString(),
                  dr["cactcode"].ToString(), dr["cactdesc"].ToString(), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["billno1"].ToString(),
                   dr["billno"].ToString(), dr["voudat"].ToString(), dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["payamt"]));
                lst.Add(Pay);
            }
            return lst;
        }

        #endregion



        #region DashBoard_Sub_Con
        public List<RealEntity.C_14_Pro.EClassPur.EClassYearSCon> ShowSConYearly(string comcod, string date)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassYearSCon> list = new List<RealEntity.C_14_Pro.EClassPur.EClassYearSCon>();
            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SCONINFOYEAR", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassYearSCon yearly = new RealEntity.C_14_Pro.EClassPur.EClassYearSCon(dr["yearid"].ToString(), Convert.ToDouble(dr["tbamt"].ToString()),
                        Convert.ToDouble(dr["tpayamt"].ToString()));
                list.Add(yearly);
            }
            return list;


        }


        public List<RealEntity.C_14_Pro.EClassPur.EClassMonthlySCon> ShowSConMonth(string comcod, string date)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassMonthlySCon> list2 = new List<RealEntity.C_14_Pro.EClassPur.EClassMonthlySCon>();
            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SCONINFYEARMONTH", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassMonthlySCon monthly = new RealEntity.C_14_Pro.EClassPur.EClassMonthlySCon(dr["yearmon"].ToString(), dr["yearmon1"].ToString(),
                        Convert.ToDouble(dr["tcbamt"]), Convert.ToDouble(dr["tcbpayamt"]), Convert.ToDouble(dr["tcbamtcore"]), Convert.ToDouble(dr["tcbpayamtcore"]));
                list2.Add(monthly);
            }
            return list2;

        }


        public List<RealEntity.C_14_Pro.EClassPur.EClassWeekly> ShowSConWeekly(string comcod, string date1)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassWeekly> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SCONINFOWEEK", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassWeekly Weekly = new RealEntity.C_14_Pro.EClassPur.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wpamt1"]),
                  Convert.ToDouble(dr["wpayamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wpamt2"]), Convert.ToDouble(dr["wpayamt2"]), dr["wcode3"].ToString(),
                   Convert.ToDouble(dr["wpamt3"]), Convert.ToDouble(dr["wpayamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wpamt4"]), Convert.ToDouble(dr["wpayamt4"]));
                lst.Add(Weekly);
            }
            return lst;
        }

        public List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> ShowSConDayWise(string comcod, string date1)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SHOWDAYWISESCONBILL", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassDayWisePur Pur = new RealEntity.C_14_Pro.EClassPur.EClassDayWisePur(dr["pactcode"].ToString(), dr["pactdesc"].ToString(),
                  dr["rsircode"].ToString(), dr["rsirdesc"].ToString(), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["billno1"].ToString(),
                   dr["billno"].ToString(), dr["billdate1"].ToString(), dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["billamt"]));
                lst.Add(Pur);
            }
            return lst;
        }

        public List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> ShowSConPayDayWise(string comcod, string date1)
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SHOWDAYWISESCONPAY", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_14_Pro.EClassPur.EClassDayWisePay Pay = new RealEntity.C_14_Pro.EClassPur.EClassDayWisePay(dr["pactcode"].ToString(), dr["pactdesc"].ToString(),
                  dr["cactcode"].ToString(), dr["cactdesc"].ToString(), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["billno1"].ToString(),
                   dr["billno"].ToString(), dr["voudat"].ToString(), dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["payamt"]));
                lst.Add(Pay);
            }
            return lst;
        }

        #endregion
    }
}
