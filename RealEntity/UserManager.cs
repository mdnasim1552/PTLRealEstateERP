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

    public class UserManager : System.Web.UI.Page
    {
        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();



        public List<ProjectName1> GetProjectName(string SearchProject)
        {
            List<ProjectName1> lst = new List<ProjectName1>();

            string comcod = ObjCommon.GetCompCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPROJECTNAMETEST", SearchProject, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                ProjectName1 details = new ProjectName1(dr["pactcode"].ToString(), dr["pactdesc"].ToString());
                lst.Add(details);
            }

            return lst;



        }

        public List<EClassSalesOpening> ShowSalesOpening()
        {
            List<EClassSalesOpening> lst = new List<EClassSalesOpening>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_SALSMGT", "RPTOPNSAESUMMARY", "", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                EClassSalesOpening details = new EClassSalesOpening(dr["pactcode"].ToString(), dr["pactdesc"].ToString(), Convert.ToDouble(dr["opnamt"].ToString()));
                lst.Add(details);
            }

            return lst;


        }

        public List<EntityClassProject> ShowProject()
        {
            List<EntityClassProject> lst = new List<EntityClassProject>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", "%%", "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                EntityClassProject details = new EntityClassProject(dr["pactcode"].ToString(), dr["pactdesc"].ToString());
                lst.Add(details);


            }
            return lst;
        }
        private void CreateTable()
        {
            DataTable mnuTbl1 = new DataTable();
            mnuTbl1.Columns.Add("formid", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemcod", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemdesc", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemurl", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemtips", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemslct", Type.GetType("System.Boolean"));
            mnuTbl1.Columns.Add("fbold", Type.GetType("System.String"));
            Session["tblpageinfo"] = mnuTbl1;

        }
        public List<EClassModule> ShowModule(string Moduleid, string Inputtype)
        {
            this.CreateTable();
            List<EClassModule> lst = new List<EClassModule>();
            DataTable dt = ConstantInfo.MenuTable(Moduleid);

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcod like '" + Inputtype + "'");
            DataTable dt1 = dv.ToTable();

            DataTable dtdb = ((DataSet)Session["tblusrlog"]).Tables[1];

            DataTable dtpage = (DataTable)Session["tblpageinfo"];
            int i = 1;
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                string frmname = dt1.Rows[j]["itemurl"].ToString();
                Boolean itemslct = Convert.ToBoolean(dt1.Rows[j]["itemslct"].ToString());
                frmname = frmname.Substring(frmname.LastIndexOf('/') + 1) + "";
                frmname = frmname.Replace("&empid=", "");
                frmname = frmname.Replace("&prjcode=", "");
                frmname = frmname.Replace("&genno=", "");
                frmname = frmname.Replace("&sircode=", "");
                frmname = frmname.Replace("&usircode=", "");
                frmname = frmname.Replace("&chqno=", "");
                frmname = frmname.Replace("&Date1=", "");
                frmname = frmname.Replace("&Date2=", "");
                frmname = frmname.Replace("&vounum=", "");
                frmname = frmname.Replace("&actcode=", "");
                frmname = frmname.Replace("&flrcod=", "");
                frmname = frmname.Replace ("&year=", "");
                frmname = frmname.Replace ("&comcod=", "");
                frmname = frmname.Replace ("&usrid=", "");

                DataRow[] dr1 = dtdb.Select("(frmname+qrytype)='" + frmname + "'");

                if (dr1.Length > 0)
                {                  
                    DataRow dr2 = dtpage.NewRow();
                    dr2["formid"] = dr1[0]["frmid"].ToString();
                    dr2["itemcod"] = dt1.Rows[j]["itemcod"].ToString();
                    dr2["itemdesc"] = ASTUtility.Right("00" + i.ToString(), 2) + ". " + dr1[0]["dscrption"]; //dt1.Rows[j]["itemdesc"].ToString().Substring(3);
                    dr2["itemurl"] = dt1.Rows[j]["itemurl"].ToString();
                    dr2["itemtips"] = dt1.Rows[j]["itemtips"].ToString();
                    dr2["itemslct"] = Convert.ToBoolean(dt1.Rows[j]["itemslct"]).ToString();
                    dr2["fbold"] = dt1.Rows[j]["fbold"].ToString();
                    dtpage.Rows.Add(dr2);
                    i++;
                }
                else if (itemslct == false)
                {
                    DataRow dr2 = dtpage.NewRow();
                    dr2["formid"] = "";
                    dr2["itemcod"] = dt1.Rows[j]["itemcod"].ToString();
                    dr2["itemdesc"] = dt1.Rows[j]["itemdesc"].ToString();
                    dr2["itemurl"] = dt1.Rows[j]["itemurl"].ToString();
                    dr2["itemtips"] = dt1.Rows[j]["itemtips"].ToString();
                    dr2["itemslct"] = Convert.ToBoolean(dt1.Rows[j]["itemslct"]).ToString();
                    dr2["fbold"] = dt1.Rows[j]["fbold"].ToString();
                    dtpage.Rows.Add(dr2);

                }


            }
            //If Child  is not existed
            dv = dtpage.DefaultView;
            dv.RowFilter = ("itemslct=False");
            DataTable dtp = dv.ToTable();
            string mitemcode, itemcode;
            foreach (DataRow dr1 in dtp.Rows)
            {
                mitemcode = dr1["itemcod"].ToString();
                itemcode = dr1["itemcod"].ToString().Substring(0,4);

                DataRow[] dr = dtpage.Select("itemcod like '" + itemcode + "%'");
                if (dr.Length==1)
                {

                    dv = dtpage.DefaultView;
                    dv.RowFilter = ("itemcod not like '"+ mitemcode + "%'");
                    dtpage = dv.ToTable();
                }

            }
           


            foreach (DataRow dr in dtpage.Rows)
            {

                EClassModule details = new EClassModule(dr["itemcod"].ToString(), dr["itemdesc"].ToString(), dr["itemurl"].ToString(), Convert.ToBoolean(dr["itemslct"].ToString()), dr["fbold"].ToString(), dr["formid"].ToString());
                lst.Add(details);
            }
            return lst;

        }


      

        #region KPI
        public List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> GetEmpMonEvagen(string empid, string frmdate, string todate)
        {
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lst = new List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen>();
            string comcod = ObjCommon.GetCompCode();
          //  string deptcode = "9402%";//ObjCommon.GetDeptCode();

            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI03", "GRAPHMONWISE", empid, frmdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEmployeeMonEvagen details = new RealEntity.C_47_Kpi.EClassEmployeeMonEvagen(dr["ymonid"].ToString(), dr["yearmon"].ToString(),
                        Convert.ToDouble(dr["tmark"].ToString()), Convert.ToDouble(dr["acmark"].ToString()), Convert.ToDouble(dr["Target"].ToString()), Convert.ToDouble(dr["Actual"].ToString()), Convert.ToDouble(dr["avgmark"].ToString()), dr["gpa"].ToString());
                lst.Add(details);
            }

            return lst;
        }

        public List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> GetEmpMonEva(string empid, string frmdate, string todate)
        {
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = new List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>();
            string comcod = ObjCommon.GetCompCode();
            //string deptcode = "9402%";//ObjCommon.GetDeptCode();
            // string CallType = (deptcode == "010100101001") ? "SHOWMONWISEEVA" : "GRAPHMONWISE";
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "SHOWMONWISEEVA", empid, frmdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEmployeeMonEva details = new RealEntity.C_47_Kpi.EClassEmployeeMonEva(dr["ymonid"].ToString(), dr["yearmon"].ToString(),
                        Convert.ToDouble(dr["tamt1"].ToString()), Convert.ToDouble(dr["tamt2"].ToString()), Convert.ToDouble(dr["tamt3"].ToString()), Convert.ToDouble(dr["tamt4"].ToString()),
                        Convert.ToDouble(dr["tamt5"].ToString()), Convert.ToDouble(dr["tamt6"].ToString()), Convert.ToDouble(dr["tamt7"].ToString()), Convert.ToDouble(dr["tamt8"].ToString()), 
                        Convert.ToDouble(dr["amt1"].ToString()), Convert.ToDouble(dr["amt2"].ToString()),Convert.ToDouble(dr["amt3"].ToString()), Convert.ToDouble(dr["amt4"].ToString()),
                        Convert.ToDouble(dr["amt5"].ToString()), Convert.ToDouble(dr["amt6"].ToString()), Convert.ToDouble(dr["amt7"].ToString()), Convert.ToDouble(dr["amt8"].ToString()),
                        Convert.ToDouble(dr["tper"].ToString()), Convert.ToDouble(dr["tmark"].ToString()), dr["gpa"].ToString(), Convert.ToDouble(dr["Target"].ToString()), Convert.ToDouble(dr["Actual"].ToString()), Convert.ToDouble(dr["avgmark"].ToString()));
                lst.Add(details);
            }

            return lst;
        }



        public List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02> GetEmpMonEva02(string empid, string frmdate, string todate, string type)
        {
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02> lst = new List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "SHOWMONWISEEVA02", empid, frmdate, todate, type, "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEmployeeMonEva02 details = new RealEntity.C_47_Kpi.EClassEmployeeMonEva02(dr["ymonid"].ToString(), dr["yearmon"].ToString(),
                        Convert.ToDouble(dr["tar"].ToString()), Convert.ToDouble(dr["cumtar"].ToString()), Convert.ToDouble(dr["act"].ToString()), Convert.ToDouble(dr["cumact"].ToString()), Convert.ToDouble(dr["tper"].ToString()),
                        Convert.ToDouble(dr["tmark"].ToString()), dr["gpa"].ToString(), Convert.ToDouble(dr["Target"].ToString()), Convert.ToDouble(dr["Actual"].ToString()));
                lst.Add(details);
            }

            return lst;
        }

        public List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> GetEmpMonEva04(string empid, string frmdate, string todate, string deptcode)
        {
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = new List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "SHOWMONWISEEVACR", empid, frmdate, todate, deptcode, "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEmployeeMonEva details = new RealEntity.C_47_Kpi.EClassEmployeeMonEva(dr["ymonid"].ToString(), dr["yearmon"].ToString(),
                        Convert.ToDouble(dr["tamt1"].ToString()), Convert.ToDouble(dr["tamt2"].ToString()), Convert.ToDouble(dr["tamt3"].ToString()), Convert.ToDouble(dr["tamt4"].ToString()),
                        Convert.ToDouble(dr["tamt5"].ToString()), Convert.ToDouble(dr["tamt6"].ToString()), Convert.ToDouble(dr["tamt7"].ToString()), Convert.ToDouble(dr["tamt8"].ToString()), Convert.ToDouble(dr["amt1"].ToString()), Convert.ToDouble(dr["amt2"].ToString()),
                        Convert.ToDouble(dr["amt3"].ToString()), Convert.ToDouble(dr["amt4"].ToString()), Convert.ToDouble(dr["amt5"].ToString()), Convert.ToDouble(dr["amt6"].ToString()), Convert.ToDouble(dr["amt7"].ToString()), Convert.ToDouble(dr["amt8"].ToString()),
                        Convert.ToDouble(dr["tper"].ToString()), Convert.ToDouble(dr["tmark"].ToString()), dr["gpa"].ToString(), Convert.ToDouble(dr["Target"].ToString()), Convert.ToDouble(dr["Actual"].ToString()), Convert.ToDouble(dr["Actual"].ToString()));
                lst.Add(details);
            }

            return lst;
        }


        public List<RealEntity.C_47_Kpi.EClassEmpHistory> GetEmpHistory(string empid, string frmdate, string todate)
        {
            List<RealEntity.C_47_Kpi.EClassEmpHistory> lst = new List<RealEntity.C_47_Kpi.EClassEmpHistory>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_kpi.SP_REPORT_EMP_KPI02", "RPTEMPPROWISEHISTORY", empid, frmdate, todate, "", "", "", "", "", "");
            while (dr.Read())
            {
                RealEntity.C_47_Kpi.EClassEmpHistory details = new RealEntity.C_47_Kpi.EClassEmpHistory(dr["pactdesc"].ToString(), dr["actdesc"].ToString(),
                        Convert.ToDouble(dr["duration"].ToString()), Convert.ToDouble(dr["aduration"].ToString()), Convert.ToDouble(dr["deloadv"].ToString()), dr["deloadvsign"].ToString());
                lst.Add(details);
            }

            return lst;
        }


        #endregion



        public List<EClassCompInf> ShowGetCompinf(string date)
        {

            List<EClassCompInf> lst = new List<EClassCompInf>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETGROUPATTENDENCE", date, "", "", "", "", "", "", "", "");
            while (dr.Read())
            {
                EClassCompInf details = new EClassCompInf(dr["comcod"].ToString(), dr["comnam"].ToString(), Convert.ToDouble(dr["ttlstap"].ToString()),
                  Convert.ToDouble(dr["present"].ToString()), Convert.ToDouble(dr["late"].ToString()), Convert.ToDouble(dr["earlyLev"].ToString()), Convert.ToDouble(dr["onlev"].ToString()),
                  Convert.ToDouble(dr["absnt"].ToString()));
                lst.Add(details);
            }

            return lst;


        }

        public List<EClassComModule> ShowModule2(string UserId)
        {

            List<EClassComModule> lst = new List<EClassComModule>();
            string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_UTILITY_LOGIN_MGT", "GETMODULELISTAll", UserId, "", "", "", "", "", "", "", "");
            while (dr.Read())     
            {
                EClassComModule details = new EClassComModule(dr["moduleid"].ToString(), dr["modulename"].ToString(), dr["url"].ToString());
                lst.Add(details);
            }

            return lst;


        }

        [Serializable]
        public class userNotification
        {
            public int notifyid { get; set; }
            public string meassage { get; set; }
            public string eventitle { get; set; }
            public int userid { get; set; }          
            public string sendname { get; set; }
            public string sendphoto { get; set; }
            public string refid { get; set; }
            public string notiytype { get; set; }
            public string ntype { get; set; }
            public userNotification()
            {

            }
            public userNotification(int notifyid, string meassage, string eventitle, int userid,     string sendname, string sendphoto, string refid, string notiytype, string ntype)
            {
                this.notifyid = notifyid;
                this.meassage = meassage;
                this.eventitle = eventitle;
                this.userid = userid;
              
                 
                this.sendname = sendname;
                this.sendphoto = sendphoto;
                this.refid = refid;
                this.notiytype = notiytype;
                this.ntype = ntype;

            }
        }


    }




}
