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

namespace RealEntity.C_08_PPlan
{
    public class BL_UserManage_Con
    {
        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

        #region DashBoard_Constraction
        public List<RealEntity.C_08_PPlan.BO_Class_Con.EClassYear> ShowConYearly(string comcod, string date)
        {
            List<RealEntity.C_08_PPlan.BO_Class_Con.EClassYear> list = new List<RealEntity.C_08_PPlan.BO_Class_Con.EClassYear>();
            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "CONSYEARLYSTATUS", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_08_PPlan.BO_Class_Con.EClassYear yearly = new RealEntity.C_08_PPlan.BO_Class_Con.EClassYear(dr["ymonth"].ToString(), Convert.ToDouble(dr["taramt"].ToString()),
                        Convert.ToDouble(dr["examt"].ToString()));
                list.Add(yearly);
            }
            return list;


        }


        public List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly> ShowConMonth(string comcod, string date)
        {
            List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly> list2 = new List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly>();
            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "CONSMONTHLYSTATUS", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly monthly = new RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly(dr["yearmon"].ToString(), dr["ymon"].ToString(),
                        Convert.ToDouble(dr["taramt"]), Convert.ToDouble(dr["examt"]), Convert.ToDouble(dr["taramtcore"]), Convert.ToDouble(dr["examtcore"]));                list2.Add(monthly);
            }
            return list2;

        }


        public List<RealEntity.C_08_PPlan.BO_Class_Con.EClassWeekly> ShowPurWeekly(string comcod, string date1)
        {
            List<RealEntity.C_08_PPlan.BO_Class_Con.EClassWeekly> lst = new List<RealEntity.C_08_PPlan.BO_Class_Con.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "CONSWEEKLYSTATUS", date1, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_08_PPlan.BO_Class_Con.EClassWeekly Weekly = new RealEntity.C_08_PPlan.BO_Class_Con.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wtaramt1"]),
                  Convert.ToDouble(dr["wexamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wtaramt2"]), Convert.ToDouble(dr["wexamt2"]), dr["wcode3"].ToString(),
                   Convert.ToDouble(dr["wtaramt3"]), Convert.ToDouble(dr["wexamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wtaramt4"]), Convert.ToDouble(dr["wexamt4"]));
                lst.Add(Weekly);
            }
            return lst;
        }

        //public List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> ShowPurDayWise(string date1)
        //{
        //    List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur>();

        //    string comcod = ObjCommon.GetCompCode();

        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SHOWDAYWISEBILL", date1, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        RealEntity.C_14_Pro.EClassPur.EClassDayWisePur Pur = new RealEntity.C_14_Pro.EClassPur.EClassDayWisePur(dr["pactcode"].ToString(), dr["pactdesc"].ToString(),
        //          dr["rsircode"].ToString(), dr["rsirdesc"].ToString(), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["billno1"].ToString(),
        //           dr["billno"].ToString(), dr["billdate1"].ToString(), dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["billamt"]));
        //        lst.Add(Pur);
        //    }
        //    return lst;
        //}

        //public List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> ShowPayDayWise(string date1)
        //{
        //    List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> lst = new List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay>();

        //    string comcod = ObjCommon.GetCompCode();

        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SHOWDAYWISEPAY", date1, "", "", "", "", "", "", "", "");
        //    while (dr.Read())
        //    {
        //        RealEntity.C_14_Pro.EClassPur.EClassDayWisePay Pay = new RealEntity.C_14_Pro.EClassPur.EClassDayWisePay(dr["pactcode"].ToString(), dr["pactdesc"].ToString(),
        //          dr["cactcode"].ToString(), dr["cactdesc"].ToString(), dr["ssircode"].ToString(), dr["ssirdesc"].ToString(), dr["billno1"].ToString(),
        //           dr["billno"].ToString(), dr["voudat"].ToString(), dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["payamt"]));
        //        lst.Add(Pay);
        //    }
        //    return lst;
        //}

        #endregion

    }
}
