using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using  RealERPLIB;


namespace RealEntity
{

    public class UserManagerKPI 
    {
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();
      
   

        public UserManagerKPI() 
        {
        
        }
        #region  Employee KPI 

        public List<RealEntity.C_47_Kpi.EClassEmpCode> GetEmpCode(string Procedure, string Calltype, string srchEmp, string usrid, string deptcode)
        {
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, srchEmp, usrid, deptcode, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEmpCode details = new RealEntity.C_47_Kpi.EClassEmpCode(dr["empid"].ToString(), dr["empname"].ToString(), dr["empname1"].ToString(), dr["desig"].ToString(),Convert.ToDateTime(dr["joindate"]));
                lst.Add(details);
            }

            return lst;

        }


        public List<RealEntity.C_47_Kpi.EClassEmpCode2> GetEmpCode2(string srchEmp, string usrid, string deptcode, string Date)
        {
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst = new List<RealEntity.C_47_Kpi.EClassEmpCode2>();
            string comcod = ObjCommon.GetCompCode();

            //string CallType = deptcode.Contains("%") ? "SHOWEMPCRSALLIST" : "SHOWEMPLIST";
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY_02", "SHOWEMPLIST", srchEmp, usrid, deptcode, Date, "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEmpCode2 details = new RealEntity.C_47_Kpi.EClassEmpCode2(dr["empid"].ToString(), dr["empname"].ToString(), dr["empname1"].ToString(), dr["desgcod"].ToString(), dr["desg"].ToString(), Convert.ToDateTime(dr["joindat"]), dr["deptcode"].ToString(), dr["deptname"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        public List<RealEntity.C_47_Kpi.EClassShowEmpData> GetEmpWorklist(string Calltype, string MonthID, string Empid, string Date)
        {
            List<RealEntity.C_47_Kpi.EClassShowEmpData> lst = new List<RealEntity.C_47_Kpi.EClassShowEmpData>();
            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY_02", Calltype, "", MonthID, Empid, Date, "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassShowEmpData details = new RealEntity.C_47_Kpi.EClassShowEmpData(dr["actcode1"].ToString(), dr["actdesc1"].ToString(),
                    dr["actcode"].ToString(), dr["actdesc"].ToString(), Convert.ToDouble(dr["wqty"]), Convert.ToDouble(dr["marks"]), Convert.ToDouble(dr["acqty"]),
                    Convert.ToDouble(dr["acmarks"]), Convert.ToDouble(dr["ppercent"])); 
                lst.Add(details);
            }

            return lst;
        }


        public List<RealEntity.C_47_Kpi.EClassShowEval> GetEmpEval( string CallType, string MonthID, string Empid, string Date)
        {
            List<RealEntity.C_47_Kpi.EClassShowEval> lst = new List<RealEntity.C_47_Kpi.EClassShowEval>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY_02", CallType, "", MonthID, Empid, Date, "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassShowEval details = new RealEntity.C_47_Kpi.EClassShowEval(Convert.ToDouble(dr["actmark"]), Convert.ToDouble(dr["acpar"]), dr["grade"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        

        public List<RealEntity.C_47_Kpi.EClassEMPKPI.EClassProject> GetProject( string srchproject, string empid)
        {
            List<RealEntity.C_47_Kpi.EClassEMPKPI.EClassProject> lst = new List<RealEntity.C_47_Kpi.EClassEMPKPI.EClassProject>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETPROJECTNAME", srchproject, empid, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEMPKPI.EClassProject details = new RealEntity.C_47_Kpi.EClassEMPKPI.EClassProject(dr["pactcode"].ToString(), dr["pactdesc"].ToString());
                lst.Add(details);
            }

            return lst;

        }
        public List<RealEntity.C_47_Kpi.EClassEMPKPI.EClassActivities> GetActivities(string srchproject, string empid, string pactcode)
        {
            List<RealEntity.C_47_Kpi.EClassEMPKPI.EClassActivities> lst = new List<RealEntity.C_47_Kpi.EClassEMPKPI.EClassActivities>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETACTIVITIES", srchproject, empid, pactcode, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEMPKPI.EClassActivities details = new RealEntity.C_47_Kpi.EClassEMPKPI.EClassActivities(dr["actcode"].ToString(), dr["actdesc"].ToString());
                lst.Add(details);
            }

            return lst;

        }
        #endregion 

        public List<RealEntity.C_47_Kpi.EClassCompCode> GetCodeCode(string Procedure, string Calltype, string srchEmp)
        {
            List<RealEntity.C_47_Kpi.EClassCompCode> lst = new List<RealEntity.C_47_Kpi.EClassCompCode>();
            string comcod = ObjCommon.GetHRCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, srchEmp, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassCompCode details = new RealEntity.C_47_Kpi.EClassCompCode(dr["comcod"].ToString(), dr["comnam"].ToString());
                lst.Add(details);
            }

            return lst;

        }

        public List<RealEntity.C_47_Kpi.EClassDptCode> GetDptCode(string Procedure, string Calltype, string srchEmp)
        {
            List<RealEntity.C_47_Kpi.EClassDptCode> lst = new List<RealEntity.C_47_Kpi.EClassDptCode>();
            string comcod = ObjCommon.GetHRCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, srchEmp, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassDptCode details = new RealEntity.C_47_Kpi.EClassDptCode(dr["wrkdpt"].ToString(), dr["wrkdptdesc"].ToString());
                lst.Add(details);
            }

            return lst;

        }

        public List<RealEntity.C_47_Kpi.EClassSirCode> GetSirCode(string Procedure, string Calltype, string srchEmp)
        {
            List<RealEntity.C_47_Kpi.EClassSirCode> lst = new List<RealEntity.C_47_Kpi.EClassSirCode>();
            string comcod = ObjCommon.GetHRCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, srchEmp, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassSirCode details = new RealEntity.C_47_Kpi.EClassSirCode(dr["sircode"].ToString(), dr["sirdesc"].ToString());
                lst.Add(details);
            }

            return lst;

        }


        public List<RealEntity.C_47_Kpi.EClassClientCode> GetClientCode(string Procedure, string Calltype, string srchEmp, string Empid)
        {
            List<RealEntity.C_47_Kpi.EClassClientCode> lst = new List<RealEntity.C_47_Kpi.EClassClientCode>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, srchEmp, Empid, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassClientCode details = new RealEntity.C_47_Kpi.EClassClientCode(dr["ccode"].ToString(), dr["cdesc"].ToString());
                lst.Add(details);
            }

            return lst;

        }


        public List<RealEntity.C_47_Kpi.EClassLandowner> GetLandowner(string Procedure, string Calltype, string srchEmp, string Empid)
        {
            List<RealEntity.C_47_Kpi.EClassLandowner> lst = new List<RealEntity.C_47_Kpi.EClassLandowner>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, srchEmp, Empid, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassLandowner details = new RealEntity.C_47_Kpi.EClassLandowner(dr["ccode"].ToString(), dr["cdesc"].ToString(), 
                        Convert.ToDouble(dr["lsize"].ToString()), Convert.ToDouble(dr["lamt"].ToString()), Convert.ToDouble(dr["broamt"].ToString()));
                lst.Add(details);
            }

            return lst;

        }

        public List<RealEntity.C_47_Kpi.EClassClientDis> GetClientDis_(string YmonID, string Empid, string Grp)
        {
            List<RealEntity.C_47_Kpi.EClassClientDis> lst = new List<RealEntity.C_47_Kpi.EClassClientDis>();
            string comcod = ObjCommon.GetCompCode();
            
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_CLIENT_DISCUSSION", "SHOWMKTTEAMSTSTUSIND", YmonID, Empid, Grp, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassClientDis details = new RealEntity.C_47_Kpi.EClassClientDis(dr["grp"].ToString(), dr["grpdesc"].ToString(),dr["empid"].ToString(), dr["empname"].ToString(), dr["prosdesc"].ToString(),
                    dr["gdesc"].ToString(), dr["gdesc1"].ToString(), dr["gdesc2"].ToString(), dr["gdesc3"].ToString(), dr["gdesc4"].ToString(), dr["gdesc5"].ToString(),
                    dr["gdesc6"].ToString(), dr["gdesc7"].ToString(), dr["gdesc8"].ToString(), dr["gdesc9"].ToString(), dr["gdesc10"].ToString(), dr["gdesc11"].ToString(),
                    dr["gdesc12"].ToString(), dr["gdesc13"].ToString(), dr["gdesc14"].ToString(), dr["gdesc15"].ToString(), dr["gdesc16"].ToString(), dr["gdesc17"].ToString(),
                    dr["gdesc18"].ToString(), dr["gdesc19"].ToString(), dr["phone"].ToString(), Convert.ToDouble(dr["difpercnt"].ToString()), dr["uausize"].ToString()); 
                lst.Add(details);
            }

            return lst;

        }

        public List<RealEntity.C_47_Kpi.EClassHeader> GetGvHeader()
        {
            List<RealEntity.C_47_Kpi.EClassHeader> lst = new List<RealEntity.C_47_Kpi.EClassHeader>();
            string comcod = ObjCommon.GetHRCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_CLIENT_DISCUSSION", "GETGRP", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassHeader details = new RealEntity.C_47_Kpi.EClassHeader(dr["sircode"].ToString(), dr["sirdesc"].ToString());
                lst.Add(details);
            }

            return lst;

        }


        public List<RealEntity.C_47_Kpi.EClassDaySales> GetDayWiseSales_(string YmonID, string Empid)
        {
            List<RealEntity.C_47_Kpi.EClassDaySales> lst = new List<RealEntity.C_47_Kpi.EClassDaySales>();
            string comcod = ObjCommon.GetHRCompCode();
            string Mcomcod = ObjCommon.GetCompCode();
          
            SqlDataReader dr = _ProAccess.GetSqlReader2(comcod, Mcomcod, "dbo_kpi.SP_REPORT_SALE_COLLECTION", "DAYWISESALES", YmonID, Empid);
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassDaySales details = new RealEntity.C_47_Kpi.EClassDaySales(dr["custname"].ToString(), dr["custadd"].ToString(), dr["gcod"].ToString(),
                    dr["pactcode"].ToString(), dr["usircode"].ToString(), dr["pactdesc"].ToString(), dr["udesc"].ToString(), dr["munit"].ToString(), dr["schdate"].ToString(),Convert.ToDouble(dr["tuamt"]),
                    Convert.ToDouble(dr["suamt"]), Convert.ToDouble(dr["disamt"]), Convert.ToDouble(dr["disper"]), Convert.ToDouble(dr["usize"]), Convert.ToDouble(dr["sftpr"]));
                lst.Add(details);
            }
            
            return lst;

        }


        public List<RealEntity.C_47_Kpi.EClassEmpInf> GetEmpInf(string Empid)
        {
            List<RealEntity.C_47_Kpi.EClassEmpInf> lst = new List<RealEntity.C_47_Kpi.EClassEmpInf>();
            string comcod = ObjCommon.GetHRCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_SALE_COLLECTION", "GETEMPINF", Empid, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEmpInf details = new RealEntity.C_47_Kpi.EClassEmpInf(dr["gdatat"].ToString(), dr["gdatad"].ToString());
                lst.Add(details);
            }

            return lst;

        }


        public List<RealEntity.C_47_Kpi.EClassDayCollection> GetDayWiseColl_(string YmonID, string Empid)
        {
            List<RealEntity.C_47_Kpi.EClassDayCollection> lst = new List<RealEntity.C_47_Kpi.EClassDayCollection>();
            string comcod = ObjCommon.GetHRCompCode();
            string Mcomcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader2(comcod, Mcomcod, "dbo_kpi.SP_REPORT_SALE_COLLECTION", "DAYWISECOLLECTION", YmonID, Empid);
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassDayCollection details = new RealEntity.C_47_Kpi.EClassDayCollection(dr["grp"].ToString(), dr["grpdesc"].ToString(), dr["mrno"].ToString(),
                    dr["mrdate1"].ToString(), dr["pactcode"].ToString(), dr["usircode"].ToString(), dr["pactdesc"].ToString(), dr["udesc"].ToString(), dr["custname"].ToString(), 
                    dr["chqdate"].ToString(), dr["chqno"].ToString(), dr["bankname"].ToString(), dr["bbranch"].ToString(), dr["repchqno"].ToString(), dr["recndt"].ToString(),
                    dr["entrydat"].ToString(),Convert.ToDouble(dr["chqamt"]), Convert.ToDouble(dr["cashamt"]));
                lst.Add(details);
            }


            return lst;

        }


        public List<RealEntity.C_47_Kpi.GradeWise> GetGpaList()
        {
            List<RealEntity.C_47_Kpi.GradeWise> lst = new List<RealEntity.C_47_Kpi.GradeWise>();
            string comcod = ObjCommon.GetCompCode();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY_02", "EMPWPRESULTSHOW", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_47_Kpi.GradeWise details = new RealEntity.C_47_Kpi.GradeWise(sdr["mrange"].ToString(), sdr["mdescrip"].ToString());
                lst.Add(details);
            }

            return lst;
        }


       

    }
}
