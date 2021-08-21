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


namespace RealEntity.C_47_Kpi
{
    public class BL_EntryKpi
    {
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

       
        #region RealKPI Entry

        public List<RealEntity.C_47_Kpi.EntryKpi> EntryKPIWorkList(string Empid, string MonthID, string DptCode)
        {
            List<RealEntity.C_47_Kpi.EntryKpi> lst = new List<RealEntity.C_47_Kpi.EntryKpi>();
            string comcod = ObjCommon.GetCompCode();
            
            SqlDataReader sdr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY_02", "ENTRYWORKLIST", Empid, MonthID, DptCode, "","","","","","");
            while (sdr.Read())
            {
                RealEntity.C_47_Kpi.EntryKpi details = new RealEntity.C_47_Kpi.EntryKpi(sdr["actcode1"].ToString(), sdr["actdesc1"].ToString(), sdr["actcode"].ToString(),
                    sdr["actdesc"].ToString(), Convert.ToDouble(sdr["acqty"]), Convert.ToDouble(sdr["ppercent"]), sdr["note"].ToString(), sdr["remarks"].ToString(), Convert.ToDouble(sdr["wQty"]));
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

        #endregion

    }
}
