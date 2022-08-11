using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using RealEntity;
using System.Web.SessionState;
using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using RealERPLIB;
using System.Data;
using System.Configuration;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace RealERPWEB.Service
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {
        UserManager userManager = new UserManager();
        private static UserManagerKPI objUser = new UserManagerKPI();
        UserManGenAccount userManAcc = new UserManGenAccount();
        List<Student> Std = new List<Student>();
        UserManSales objUserService = new UserManSales();
        ProcessAccess accData = new ProcessAccess();

        public UserService()
        {
            Std.Add(new Student { StdId = "100001", StdName = "Emdad", StdFName = "Rais uddin", StdMName = "Zarna Begum" });
            Std.Add(new Student { StdId = "100002", StdName = "Jahangir", StdFName = "Nurul", StdMName = "Miss X" });
            Std.Add(new Student { StdId = "100003", StdName = "Nurul", StdFName = "Mr. Hazrat Ali", StdMName = " Miss Y" });
            Std.Add(new Student { StdId = "100004", StdName = "Shobuz", StdFName = "Azam Mullah", StdMName = "Miss Z" });

        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<ProjectName1> GetProjectName(string SeachProject)
        {


            List<ProjectName1> lst = userManager.GetProjectName(SeachProject);
            return lst;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<Student> GetStdName()
        {

            return Std;

        }

        [WebMethod(EnableSession = true)]
        public List<EClassSalesOpening> ShowSalesOpening()
        {
            List<EClassSalesOpening> lst = userManager.ShowSalesOpening();
            return lst;
        }

        [WebMethod(EnableSession = true)]

        public List<EntityClassProject> ShowProject()
        {
            List<EntityClassProject> lst = userManager.ShowProject();
            return lst;


        }

        //[WebMethod(EnableSession = true)]
        //public List<EClassModule> GetModule(string ModuleId, string InputName)
        //{
        //    List<EClassModule> lst = userManager.ShowModule(ModuleId, InputName);
        //    return lst;
        //}










        [WebMethod(EnableSession = true)]
        public List<RealEntity.C_47_Kpi.EClassEmpCode> GetEmpList02(string srchteam, string userid, string deptcode)
        {


            List<RealEntity.C_47_Kpi.EClassEmpCode> lst = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            //if (lnkok.Text == "New")
            //    return lst;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = Getcomcod();
            // string userid = (this.Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";

            lst = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchteam, userid, deptcode);
            return lst;

        }


        [WebMethod(EnableSession = true)]
        public List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> GetEmpMonEva(string empid, string frmdate, string todate)
        {

            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = new List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>();
            lst = userManager.GetEmpMonEva(empid, frmdate, todate);
            HttpContext.Current.Session["lst1"] = lst;
            // var json = new JavaScriptSerializer().Serialize(obj);
            return lst;


        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> GetEmpMonEvagen(string empid, string frmdate, string todate)
        {

            List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lst = new List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen>();
            lst = userManager.GetEmpMonEvagen(empid, frmdate, todate);
            HttpContext.Current.Session["lst1"] = lst;
            // var json = new JavaScriptSerializer().Serialize(obj);
            return lst;


        }


        [WebMethod(EnableSession = true)]
        public List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02> GetEmpMonEva02(string empid, string frmdate, string todate, string type)
        {
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02> lst = new List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02>();

            lst = userManager.GetEmpMonEva02(empid, frmdate, todate, type);
            HttpContext.Current.Session["lst1"] = lst;
            // var json = new JavaScriptSerializer().Serialize(obj);
            return lst;


        }

        [WebMethod(EnableSession = true)]
        public List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> GetEmpMonEva04(string empid, string frmdate, string todate)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = hst["deptcode"].ToString();


            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = new List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>();


            lst = userManager.GetEmpMonEva04(empid, frmdate, todate, deptcode);
            HttpContext.Current.Session["lst1"] = lst;
            // var json = new JavaScriptSerializer().Serialize(obj);
            return lst;


        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]

        public List<RealEntity.C_47_Kpi.GradeWise> GetGpaList()
        {

            List<RealEntity.C_47_Kpi.GradeWise> lst = objUser.GetGpaList();
            return lst;

        }




        [WebMethod(EnableSession = true)]
        public List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> GetResHead(string actcode, string filter, string srchoption)
        {
            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = userManAcc.GetResHead(actcode, filter, srchoption);

            return lst;
        }


        //public List<BDACCOBJ.C_17_Acc.EClassAccVoucher.EClassResHead> GetResHead(string comcod, string actcode, string filter, string srchoption)
        //{
        //    List<BDACCOBJ.C_17_Acc.EClassAccVoucher.EClassResHead> lst = usermain.GetResHead(comcod, actcode, filter, srchoption);

        //    return lst;
        //}

        public List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> GetResHead1(string actcode, string filter, string srchoption)
        {
            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = userManAcc.GetResHead1(actcode, filter, srchoption);

            return lst;
        }


        [WebMethod(EnableSession = true)]
        public List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> GetResHeadREQ(string actcode, string filter, string srchoption)
        {
            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = userManAcc.GetResHeadREQ(actcode, filter, srchoption);

            return lst;
        }




        [WebMethod(EnableSession = true)]
        public List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> GetActHead(string filter, string acthead, string vounum)
        {
            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst = userManAcc.GetActHead(filter, acthead, vounum);

            return lst;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<EClassCompInf> GetCompInf(string date)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string UserId = hst["userid"].ToString();
            List<EClassCompInf> lst = userManager.ShowGetCompinf(date);
            Session["tblCompinfo"] = lst;
            return lst;
        }


        [WebMethod(EnableSession = true)]
        public List<EClassModule> GetModule(string ModuleId, string InputName)
        {
            List<EClassModule> lst = userManager.ShowModule(ModuleId, InputName);
            return lst;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<EClassComModule> GetComModule()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string UserId = hst["userid"].ToString();
            List<EClassComModule> lst = userManager.ShowModule2(hst["usrid"].ToString());
            Session["tblmodule"] = lst;
            return lst;
        }

        //For Graph  Nahid 20180325

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> GetMonthlyGraph(string date)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = DateTime.Today.ToString("dd-MMM-yyyy");
            List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> lst = objUserService.ShowMonthly(comcod, curdate);

            return lst;
        }





        ///================================Attached documnets 


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public List<ListItem> GetdoctypeList(string doctype)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            List<ListItem> catelist = new List<ListItem>();
            SqlDataReader sdr = accData.GetSqlReader(comcod, "SP_ENTRY_BILLMGT", "GETREFNO", doctype, "", "", "", "", "", "", "", "");
            while (sdr.Read())
            {
                catelist.Add(new ListItem
                {
                    Value = sdr["refno"].ToString(),
                    Text = sdr["textdata"].ToString()
                });
            }


            return catelist;
        }





        [WebMethod(EnableSession = true)]
        public List<RealEntity.C_45_GrAcc.GetAttachedDocs> GetAttachedDocs(string refNodatad)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            List<RealEntity.C_45_GrAcc.GetAttachedDocs> lst = userManAcc.GetAttachedDocsm(refNodatad, comcod);

            return lst;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<EclassShortCut> GetShortCut()
        {
            DataTable dt = ((DataSet)Session["tblusrlog"]).Tables[3];
            List<EclassShortCut> lst = dt.DataTableToList<EclassShortCut>();
            return lst;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<EClassModule> GetSearchUrl(string strkeys)
        {
            //List<EClassModule> lst = new List<EClassModule>();
            DataTable dt = ConstantInfo.MenuTable("00");
            //DataTable dt = ((DataSet)Session["tblusrlog"]).Tables[1];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "itemslct='" + true + "' and fbold not like '%mb%' and itemdesc like '%" + strkeys + "%'";

            List<EClassModule> lst = dv.ToTable().DataTableToList<EClassModule>();
            return lst;

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<EclassUserPermissionPages> GetSearchUrl1(string strkeys)
        {
            DataTable dt = ((DataSet)Session["tblusrlog"]).Tables[1];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "dscrption like '%" + strkeys + "%'";
            List<EclassUserPermissionPages> lst = dv.ToTable().DataTableToList<EclassUserPermissionPages>();
            return lst;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<RealEntity.UserManager.userNotification> GetNotAndMessage(string userid)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            List<RealEntity.UserManager.userNotification> lst = new List<RealEntity.UserManager.userNotification>();
            string todate = DateTime.Today.ToString("dd-MMM-yyyy");

            SqlDataReader sdr = accData.GetSqlReader(comcod, "SP_REPORT_NOTICE", "GETEVENTLOG", todate, userid, "", "", "", "", "", "", "");
            if (sdr == null)
            {
                return lst;
            }
            while (sdr.Read())
            {
                RealEntity.UserManager.userNotification typuser = new RealEntity.UserManager.userNotification(Convert.ToInt32(sdr["notifyid"].ToString()),
                    sdr["meassage"].ToString(), sdr["eventitle"].ToString(), Convert.ToInt32(sdr["userid"].ToString()), sdr["sendname"].ToString(), sdr["sendphoto"].ToString(), sdr["refid"].ToString(), sdr["notiytype"].ToString(), sdr["ntype"].ToString());
                lst.Add(typuser);
            }
            return lst;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveHolidayData(string holidaydata)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            var serializeData = JsonConvert.DeserializeObject<List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.Holiday>>(holidaydata);


            bool result = false;
            foreach (var data in serializeData)
            {


                result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTHOLIDAY", data.HolidayDate.ToString(), data.Occasion, data.holidayType, "", "", "", "", "", "", "", "", "", "", "", "");

            }

            return null;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHday(string data)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETHOLIDAY", "", "", "", "", "", "", "", "", "");

            if (ds == null)
                return "";
            else
            {
                var catlist = ds.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.HolidayType>();
                var catlist2 = catlist.Select(e => new { e.unit, e.gcod, e.hrdesc }).ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(catlist2);
                return json;
            }

        }
    }


}
