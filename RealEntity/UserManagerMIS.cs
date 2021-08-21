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
   public class UserManagerMIS:System.Web.UI.Page
    {
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();


        #region MIS Land
     
   
        public List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProject> GetEvaonProject(string date)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProject> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProject>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_MIS02", "RPTPROJECTSTATUS", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EvaluationonProject details = new RealEntity.C_39_MyPage.EClassMIS.EvaluationonProject(dr["pactcode"].ToString(),dr["pactdesc"].ToString(),Convert.ToDateTime(dr["tstdat"].ToString()),
                        Convert.ToDateTime(dr["tenddat"].ToString()), Convert.ToDouble(dr["tper"].ToString()), Convert.ToDouble(dr["aper"].ToString()));
                lst.Add(details);
            }

            return lst;
        }

        public List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva> GetEmployeeEva(string frmdate, string todate, string deptcode)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWKPISTATUSALL02", frmdate, todate, deptcode, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EmployeeEva details = new RealEntity.C_39_MyPage.EClassMIS.EmployeeEva(dr["empid"].ToString(), dr["empname"].ToString(), Convert.ToDouble(dr["tar"].ToString()), Convert.ToDouble(dr["cumtar"].ToString()), Convert.ToDouble(dr["act"].ToString()), Convert.ToDouble(dr["cumact"].ToString()), Convert.ToDouble(dr["tper"].ToString()), dr["gpa"].ToString(), dr["graph"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        public List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen> GetEmployeeEvaGen(string yearmon, string deptcode)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWKPISTATUSALLGEN", yearmon, deptcode, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen details = new RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen(dr["empid"].ToString(), dr["empname"].ToString(), Convert.ToDouble(dr["tar"].ToString()), Convert.ToDouble(dr["act"].ToString()), dr["gpa"].ToString(), dr["graph"].ToString(), dr["desig"].ToString(), Convert.ToDouble(dr["gtar"].ToString()), Convert.ToDouble(dr["gact"].ToString()));
                lst.Add(details);
            }

            return lst;
        }



        public List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaLeg> GetEmployeeEvaLeg(string yearmon, string deptcode)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaLeg> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaLeg>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWKPISTATUSALLLEG", yearmon, deptcode, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaLeg details = new RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaLeg(dr["empid"].ToString(), Convert.ToDouble(dr["tar"].ToString()), Convert.ToDouble(dr["act"].ToString()), dr["gpa"].ToString(), dr["empname"].ToString(), dr["graph"].ToString(), dr["desig"].ToString(), Convert.ToDouble(dr["gtar"].ToString()), Convert.ToDouble(dr["gact"].ToString()));
                lst.Add(details);
            }

            return lst;
        }


        public List<RealEntity.C_39_MyPage.EClassMIS.EClassDepartment> GetDepartment()
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EClassDepartment> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EClassDepartment>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "GETDEPTNAME", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EClassDepartment details = new RealEntity.C_39_MyPage.EClassMIS.EClassDepartment(dr["deptcode"].ToString(), dr["deptname"].ToString());
                lst.Add(details);
            }

            return lst;
        }


       
       

        public List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory> GetEmployyHistory(string frmdate, string todate, string userid)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_MIS02", "RPTINDIVIDUALHISTORY", frmdate, todate, userid, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory details = new RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory(dr["empid"].ToString(), dr["empname"].ToString(), 
                        dr["pactcode"].ToString(), dr["actcode"].ToString(), dr["pactdesc"].ToString(), dr["actdesc"].ToString(), Convert.ToDateTime(dr["disdate"].ToString()),
                        dr["discuss"].ToString(), Convert.ToDateTime(dr["tstdat"].ToString()), Convert.ToDateTime(dr["tenddat"].ToString()));
                lst.Add(details);
            }

            return lst;
        }



        public List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryLand> GetEmployyHistoryLand(string frmdate, string todate, string userid)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryLand> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryLand>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_MIS02", "RPTINDEMPLOYEEHISTORY", frmdate, todate, userid, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryLand details = new RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryLand(dr["empid"].ToString(), dr["empname"].ToString(),
                        dr["pactcode"].ToString(), dr["actcode"].ToString(), dr["pactdesc"].ToString(), dr["actdesc"].ToString(), Convert.ToDateTime(dr["disdate"].ToString()),
                        dr["discuss"].ToString(), Convert.ToDateTime(dr["tstdat"].ToString()), Convert.ToDateTime(dr["tenddat"].ToString()) , Convert.ToDateTime(dr["acstdat"].ToString()) , Convert.ToDateTime(dr["acenddat"].ToString()), Convert.ToDouble(dr["deloadv"].ToString()), dr["deloadvsign"].ToString());
                lst.Add(details);
            }

            return lst;
        }



        public List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar> GetEmployyHistoryMar(string frmdate, string todate, string userid)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_MIS02", "RPTINDEMPLOYEEHISTORYMAR", frmdate, todate, userid, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar details = new RealEntity.C_39_MyPage.EClassMIS.EclassEmployeeHistoryMar(dr["empid"].ToString(), dr["empname"].ToString(),
                        dr["prono"].ToString(), dr["actcode"].ToString(), dr["refno"].ToString(), dr["actdesc"].ToString(), Convert.ToDateTime(dr["disdate"].ToString()),
                        dr["discuss"].ToString(), Convert.ToDateTime(dr["tstdat"].ToString()), Convert.ToDateTime(dr["tenddat"].ToString()), Convert.ToDateTime(dr["acstdat"].ToString()), Convert.ToDateTime(dr["acenddat"].ToString()), Convert.ToDouble(dr["deloadv"].ToString()), dr["deloadvsign"].ToString());
                lst.Add(details);
            }

            return lst;
        }


        public List<RealEntity.C_39_MyPage.EClassMIS.EclassDeptPendWork> GetDeptPendWork(string deptcode, string frmdate, string todate)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EclassDeptPendWork> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EclassDeptPendWork>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "RPTDEPTWISEPENDINGWORK", deptcode, frmdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EclassDeptPendWork details = new RealEntity.C_39_MyPage.EClassMIS.EclassDeptPendWork(dr["tarmon"].ToString(), dr["empid"].ToString(), dr["empname"].ToString(),
                         dr["actcode"].ToString(), dr["actdesc"].ToString(),  Convert.ToDouble(dr["bal"].ToString()));
                lst.Add(details);
            }

            return lst;
        }

        #endregion
        #region Mis Marketing
        public List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram> GetEvaonProgram(string date)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_MIS02", "RPTPROGRAMSTATUS", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram details = new RealEntity.C_39_MyPage.EClassMIS.EvaluationonProgram(dr["prono"].ToString(), dr["refno"].ToString(), Convert.ToDateTime(dr["tstdat"].ToString()),
                        Convert.ToDateTime(dr["tenddat"].ToString()), Convert.ToDouble(dr["tper"].ToString()), Convert.ToDouble(dr["aper"].ToString()));
                lst.Add(details);
            }

            return lst;
        }

        public List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02> GetEmployyHistory02(string frmdate, string todate, string userid)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_MIS02", "RPTINDIVIDUALHISTORY02", frmdate, todate, userid, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02 details = new RealEntity.C_39_MyPage.EClassMIS.EmployeeHistory02(dr["empid"].ToString(), dr["empname"].ToString(), 
                        dr["prono"].ToString(), dr["actcode"].ToString(), dr["refno"].ToString(), dr["actdesc"].ToString(), Convert.ToDateTime(dr["disdate"].ToString()),
                        dr["discuss"].ToString(), Convert.ToDateTime(dr["tstdat"].ToString()), Convert.ToDateTime(dr["tenddat"].ToString()));
                lst.Add(details);
            }

            return lst;
        }

        public List<RealEntity.C_39_MyPage.EClassMIS.EClassEmpHistory02> GetEmpHistory02(string empid, string frmdate, string todate)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EClassEmpHistory02> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EClassEmpHistory02>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "RPTEMPPROWISEHISTORY02", empid, frmdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EClassEmpHistory02 details = new RealEntity.C_39_MyPage.EClassMIS.EClassEmpHistory02(dr["prono"].ToString(),dr["refno"].ToString(), dr["actdesc"].ToString(),
                        Convert.ToDouble(dr["duration"].ToString()), Convert.ToDouble(dr["aduration"].ToString()), Convert.ToDouble(dr["deloadv"].ToString()), dr["deloadvsign"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        

        #endregion


        public List<RealEntity.C_39_MyPage.EClassMIS.EclassDeptEvaSheet> GetDeptEvaList(string yearmon)
        {
            List<RealEntity.C_39_MyPage.EClassMIS.EclassDeptEvaSheet> lst = new List<RealEntity.C_39_MyPage.EClassMIS.EclassDeptEvaSheet>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWALLDEPTAVEREGE", yearmon, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_39_MyPage.EClassMIS.EclassDeptEvaSheet details = new RealEntity.C_39_MyPage.EClassMIS.EclassDeptEvaSheet(dr["deptcode"].ToString(), Convert.ToDouble(dr["nofemp"].ToString()), Convert.ToDouble(dr["tar"].ToString()), Convert.ToDouble(dr["avgact"].ToString()), dr["prgdesc"].ToString(), dr["gpa"].ToString());
                lst.Add(details);
            }

            return lst;
        }

    }
}
