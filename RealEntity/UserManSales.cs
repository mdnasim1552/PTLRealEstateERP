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
    public class UserManSales
    {

        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

       

        #region SalesDash_Board


        public List<RealEntity.C_22_Sal.EClassSales_02.EClassYear> ShowYearly(string comcod, string Date1, string recondate="")
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassYear> lst = new List<RealEntity.C_22_Sal.EClassSales_02.EClassYear>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALESINFOYEAR", Date1, recondate, "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_22_Sal.EClassSales_02.EClassYear Yearly = new C_22_Sal.EClassSales_02.EClassYear(dr["yearid"].ToString(), Convert.ToDouble(dr["samt"]), Convert.ToDouble(dr["collamt"]));
                lst.Add(Yearly);
            }

            return lst;

        }

        public List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly> ShowWeekly(string comcod, string Date1)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly> lst = new List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALESINFOWEEK", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_22_Sal.EClassSales_02.EClassWeekly Weekly = new C_22_Sal.EClassSales_02.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wsamt1"]),
                    Convert.ToDouble(dr["wcamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wsamt2"]), Convert.ToDouble(dr["wcamt2"]), dr["wcode3"].ToString(),
                    Convert.ToDouble(dr["wsamt3"]), Convert.ToDouble(dr["wcamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wsamt4"]),
                    Convert.ToDouble(dr["wcamt4"]));
                lst.Add(Weekly);
            }

            return lst;

        }

        public List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly> ShowWeeklySalesACollection(string comcod, string Date1, string pactcode)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly> lst = new List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALESINFOWEEKPRJWISE", Date1, pactcode, "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_22_Sal.EClassSales_02.EClassWeekly Weekly = new C_22_Sal.EClassSales_02.EClassWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wsamt1"]),
                    Convert.ToDouble(dr["wcamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wsamt2"]), Convert.ToDouble(dr["wcamt2"]), dr["wcode3"].ToString(),
                    Convert.ToDouble(dr["wsamt3"]), Convert.ToDouble(dr["wcamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wsamt4"]),
                    Convert.ToDouble(dr["wcamt4"]));
                lst.Add(Weekly);
            }

            return lst;

        }

        
        public List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> ShowMonthly(string comcod, string Date1, string recndate = "")
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> lst = new List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALESINFYEARMONTH", Date1, recndate, "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_22_Sal.EClassSales_02.EClassMonthly Monthly = new C_22_Sal.EClassSales_02.EClassMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(), Convert.ToDouble(dr["ttlsalamt"]), Convert.ToDouble(dr["collamt"]),
                    Convert.ToDouble(dr["targtsaleamt"]), Convert.ToDouble(dr["tarcollamt"]), Convert.ToDouble(dr["targtsaleamtcore"]), Convert.ToDouble(dr["tarcollamtcore"]), Convert.ToDouble(dr["ttlsalamtcore"]), Convert.ToDouble(dr["collamtcrore"]), dr["ymon"].ToString());
                lst.Add(Monthly);
            }

            return lst;

        }

        public List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWise> ShowDayWise(string comcod, string Date1)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWise> lst = new List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWise>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "DAYWISESALES", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_22_Sal.EClassSales_02.EClassDayWise DayWise = new C_22_Sal.EClassSales_02.EClassDayWise(dr["centrid"].ToString(), dr["centrdesc"].ToString(),
                    dr["custid"].ToString(), dr["custdesc"].ToString(), dr["memono1"].ToString(), dr["memono"].ToString(), dr["memodat"].ToString(),
                    dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["itmamt"]), Convert.ToDouble(dr["vat"]), Convert.ToDouble(dr["invdis"]));
                lst.Add(DayWise);
            }

            return lst;

        }

        public List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWiseColl> ShowDayWiseColl(string comcod, string Date1)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWiseColl> lst = new List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWiseColl>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "DAYWISECOLL", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_22_Sal.EClassSales_02.EClassDayWiseColl DayWise = new C_22_Sal.EClassSales_02.EClassDayWiseColl(dr["centrid"].ToString(), dr["centrdesc"].ToString(),
                    dr["custid"].ToString(), dr["custdesc"].ToString(), dr["mrslno1"].ToString(), dr["mrslno"].ToString(), dr["mrdat"].ToString(),
                    dr["vounum1"].ToString(), dr["vounum"].ToString(), Convert.ToDouble(dr["amount"]));
                lst.Add(DayWise);
            }

            return lst;

        }

        #endregion


        public List<RealEntity.C_22_Sal.EClassSales_02.CustomerID> GetCustomerID(string comcod, string pactcode)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.CustomerID> lst = new List<RealEntity.C_22_Sal.EClassSales_02.CustomerID>();


            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_SALSMGT", "GETCUSTOMERID", pactcode, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_22_Sal.EClassSales_02.CustomerID objcust = new C_22_Sal.EClassSales_02.CustomerID(dr["custcode"].ToString());
                lst.Add(objcust);
            }

            return lst;

        }
        
    }
}
