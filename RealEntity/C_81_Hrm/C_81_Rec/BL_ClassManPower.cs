using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using RealERPLIB;

namespace RealEntity.C_81_Hrm.C_81_Rec
{
   public class BL_ClassManPower
    {
        ProcessAccess _ProAccess = new ProcessAccess();


        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> GetBgdTypelist( string comcod)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETBUDGETEDTYPE", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType(sdr["hrgcod"].ToString(), sdr["hrgdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }

        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> GetEmpTypelist(string comcod)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETTYPEOFEMPLOYES", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType(sdr["hrgcod"].ToString(), sdr["hrgdesc"].ToString());
                lst.Add(TypeInfo);
            }

            return lst;
        }
        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> GetWstation(string comcod, string usrid)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETACCESSORGANOGRAMLIST", "94%", "%%", usrid, "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }

        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> GETORGANOGRAMALLLIST(string comcod, string userid)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETORGANOGRAMALLLIST", "94%", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }
        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> GetDptlist(string comcod, string usrid)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", "94%", "%%", usrid,  "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf(sdr["actcode"].ToString(), sdr["actdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }

        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList> GetSectionlist(string comcod, string dptcode)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", dptcode, "%%", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSectionList(sdr["sectionname"].ToString(), sdr["section"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }
        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList> GetDisgnation(string comcod, string grade)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDESIGNATION", grade, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrDeisgList(sdr["designation"].ToString(), sdr["desigcod"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }

        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> GetEmpGradelist(string comcod)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETEMPGRADE", "", "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType(sdr["hrgcod"].ToString(), sdr["hrgdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }


        public List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> GetCommonHRgcod(string comcod, string hrgcod)
        {
            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType> lst = new List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType>();

            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETCOMMONHRGCOD", hrgcod, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType TypeInfo = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType(sdr["hrgcod"].ToString(), sdr["hrgdesc"].ToString());
                lst.Add(TypeInfo);

            }

            return lst;
        }
    }
}
