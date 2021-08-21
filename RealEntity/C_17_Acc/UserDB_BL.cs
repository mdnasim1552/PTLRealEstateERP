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


namespace RealEntity.C_17_Acc
{
    public class UserDB_BL
    {
        ProcessAccess _userData = new ProcessAccess();
        Common ObjComcod = new Common();

        public List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccYearly> ShowYearAcc(string comcod, string Date)
        {
            List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccYearly> lst = new List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccYearly>();
            //string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ACCINFOYEAR", Date, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_17_Acc.EClassDB_BO.EClassAccYearly Yearly = new C_17_Acc.EClassDB_BO.EClassAccYearly(dr["yearid"].ToString(), Convert.ToDouble(dr["dram"]), Convert.ToDouble(dr["cram"]));
                lst.Add(Yearly);
            }

            return lst;
        }

        public List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccMonthly> ShowMonthlyAcc(string comcod, string Date)
        {
            List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccMonthly> lst = new List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccMonthly>();
            //string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ACCINFOYMONTH", Date, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_17_Acc.EClassDB_BO.EClassAccMonthly Monthly = new C_17_Acc.EClassDB_BO.EClassAccMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(), 
                    Convert.ToDouble(dr["dram"]), Convert.ToDouble(dr["cram"]), Convert.ToDouble(dr["dramcore"]), Convert.ToDouble(dr["cramcore"]));
                lst.Add(Monthly);
            }

            return lst;
        }

        public List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccWeekly> ShowWeeklyAcc(string comcod, string Date1)
        {
            List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccWeekly> lst = new List<RealEntity.C_17_Acc.EClassDB_BO.EClassAccWeekly>();

            //string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "ACCINFOYDAILY", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_17_Acc.EClassDB_BO.EClassAccWeekly Weekly = new RealEntity.C_17_Acc.EClassDB_BO.EClassAccWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wpamt1"]),
                    Convert.ToDouble(dr["wramt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wpamt2"]), Convert.ToDouble(dr["wramt2"]), dr["wcode3"].ToString(),
                    Convert.ToDouble(dr["wpamt3"]), Convert.ToDouble(dr["wramt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wpamt4"]),
                    Convert.ToDouble(dr["wramt4"]), Convert.ToDouble(dr["brec"]), Convert.ToDouble(dr["bpay"]));
                lst.Add(Weekly);
            }

            return lst;

        }


        #region Land Purchase

        public List<RealEntity.C_17_Acc.EClassDB_BO.EClassLPROYearly> ShowYearLandPur(string comcod, string Date)
        {
            List<RealEntity.C_17_Acc.EClassDB_BO.EClassLPROYearly> lst = new List<RealEntity.C_17_Acc.EClassDB_BO.EClassLPROYearly>();
            //string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "LANDPROINFOYEAR", Date, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_17_Acc.EClassDB_BO.EClassLPROYearly Yearly = new C_17_Acc.EClassDB_BO.EClassLPROYearly(dr["yearid"].ToString(), Convert.ToDouble(dr["puram"]), Convert.ToDouble(dr["paymntam"]));
                lst.Add(Yearly);
            }

            return lst;
        }
        public List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROMonthly> ShowMonthlyLP(string comcod, string Date)
        {
            List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROMonthly> lst = new List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROMonthly>();
            //string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "LANDPROINFOMONTH", Date, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_17_Acc.EClassDB_BO.EClassPROMonthly Monthly = new C_17_Acc.EClassDB_BO.EClassPROMonthly(dr["yearmon"].ToString(), dr["yearmon1"].ToString(),
                    Convert.ToDouble(dr["puram"]), Convert.ToDouble(dr["paymntam"]));
                lst.Add(Monthly);
            }

            return lst;
        }
        public List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROWeekly> ShowWeeklylp(string comcod, string Date1)
        {
            List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROWeekly> lst = new List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROWeekly>();

            //string comcod = ObjComcod.GetCompCode();
            SqlDataReader dr = _userData.GetSqlReader(comcod, "SP_REPORT_DASH_BOARD_INFO", "LANDPROINFOWEEK", Date1, "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_17_Acc.EClassDB_BO.EClassPROWeekly Weekly = new RealEntity.C_17_Acc.EClassDB_BO.EClassPROWeekly(dr["wcode1"].ToString(), Convert.ToDouble(dr["wpamt1"]),
                    Convert.ToDouble(dr["wpayamt1"]), dr["wcode2"].ToString(), Convert.ToDouble(dr["wpamt2"]), Convert.ToDouble(dr["wpayamt2"]), dr["wcode3"].ToString(),
                    Convert.ToDouble(dr["wpamt3"]), Convert.ToDouble(dr["wpayamt3"]), dr["wcode4"].ToString(), Convert.ToDouble(dr["wpamt4"]),
                    Convert.ToDouble(dr["wpayamt4"]));
                lst.Add(Weekly);
            }

            return lst;

        }
        #endregion
    }
}
