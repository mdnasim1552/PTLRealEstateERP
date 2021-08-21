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

    public class UserManagerLCM 
    {
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();


        public UserManagerLCM() 
        {
        
        }

        public List<RealEntity.C_09_LCM.EClassLCCode> GetLCCode(string Procedure, string Calltype, string LCSch)
        {
            List<RealEntity.C_09_LCM.EClassLCCode> lst = new List<RealEntity.C_09_LCM.EClassLCCode>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, LCSch, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_09_LCM.EClassLCCode details = new RealEntity.C_09_LCM.EClassLCCode(dr["actcode"].ToString(), dr["actdesc"].ToString());
                lst.Add(details);
            }

            return lst;

        }



        public List<RealEntity.C_09_LCM.EClassLCCosting> ShowLCCosting(string Procedure, string Calltype, string LCnumber, string Label)
        {
            List<RealEntity.C_09_LCM.EClassLCCosting> lst = new List<RealEntity.C_09_LCM.EClassLCCosting>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, LCnumber, Label, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_09_LCM.EClassLCCosting details = new RealEntity.C_09_LCM.EClassLCCosting(dr["grp"].ToString(),
                        dr["rescode"].ToString(), dr["resdesc"].ToString(), dr["unit"].ToString(), Convert.ToDouble(dr["qty"]), Convert.ToDouble(dr["fcrate"]), Convert.ToDouble(dr["fcamt"]),
                         Convert.ToDouble(dr["bdamt"]), Convert.ToDouble(dr["tparcent"]), dr["lcdate"].ToString(), dr["bankname"].ToString(), dr["currency"].ToString(),
                    dr["expdate"].ToString(), Convert.ToDouble(dr["conrate"]), dr["supname"].ToString());
                lst.Add(details);
            }

            return lst;

        }


        public List<RealEntity.C_09_LCM.EClassLCStatus> ShowLCStatus(string Procedure, string Calltype, string LCnumber)
        {
            List<RealEntity.C_09_LCM.EClassLCStatus> lst = new List<RealEntity.C_09_LCM.EClassLCStatus>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, Procedure, Calltype, LCnumber, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_09_LCM.EClassLCStatus details = new RealEntity.C_09_LCM.EClassLCStatus(dr["actcode"].ToString(), dr["actdesc"].ToString(), dr["rescod"].ToString(), 
                    dr["resdesc"].ToString(), Convert.ToDouble(dr["ordrqty"]), Convert.ToDouble(dr["rate"]), Convert.ToDouble(dr["lcamt"]), dr["lcdate"].ToString(), 
                    dr["shipdate"].ToString(), dr["shipardate"].ToString(), dr["deldate"].ToString(), dr["expdate"].ToString(), dr["rcvdate"].ToString(), dr["lcstatus"].ToString());
                lst.Add(details);
            }

            return lst;

        }
        

    }
}
