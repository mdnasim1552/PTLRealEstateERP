using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using RealERPLIB;

using RealOBJ;
using RealEntity;
using RealEntity.C_14_Pro;
using RealEntity.C_17_Acc;
//using RealEntity.C_13_ProdMon;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using RealEntity.C_08_PPlan;
namespace RealERPWEB
{

    public partial class AllGraph : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                // ((Label)this.Master.FindControl("lblTitle")).Visible = false;
                //  ((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
                ((Label)this.Master.FindControl("lblprintstk")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((Label)this.Master.FindControl("lblTitle")).Text = "DASHBOARD";

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string dates)

        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            UserManSales objUserService = new UserManSales();
            UserManPur objUserServicePur = new UserManPur();
            BL_UserManage_Con objUserServiceCon = new BL_UserManage_Con();
            UserDB_BL objUserServiceACC = new UserDB_BL();
            ProcessAccess _DataEntry = new ProcessAccess();
            List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> lst1 = objUserService.ShowMonthly(comcod, dates);
            List<RealEntity.C_14_Pro.EClassPur.EClassMonthly> list2 = objUserServicePur.ShowPurMonth(comcod, dates);
            List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly> lst3 = objUserServiceCon.ShowConMonth(comcod, dates);
            List<EClassDB_BO.EClassAccMonthly> list4 = objUserServiceACC.ShowMonthlyAcc(comcod, dates);
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_MIS_GRAPH", "GET_MIS_GRAPH_DATA", dates, "", "", "", "", "", "", "");
            List<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum> lst5 = ds2.Tables[0].DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum>();
            //   return "hello Safi"+Convert.ToDateTime(dates).ToString("dd-MM-yyyy");
            var balsheetlist = lst5.FindAll(p => p.grp == "2");
            //  return js;
            //  var result = lst1.Concat(list2);


            var datalist = new MyallData(lst1, list2, lst3, list4, balsheetlist);

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);
            //var json = jsonSerialiser.Serialize(list2);
            return json;

        }


    }

    public class MyallData
    {
        public List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> sales { get; set; }
        public List<RealEntity.C_14_Pro.EClassPur.EClassMonthly> pur { get; set; }
        public List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly> prod { get; set; }
        public List<EClassDB_BO.EClassAccMonthly> acc { get; set; }
        public List<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum> balshet { get; set; }

        public MyallData() { }
        public MyallData(List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> sales, List<RealEntity.C_14_Pro.EClassPur.EClassMonthly> pur, List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly> prod, List<EClassDB_BO.EClassAccMonthly> acc, List<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum> balshet)
        {
            this.sales = sales;
            this.pur = pur;
            this.prod = prod;
            this.acc = acc;
            this.balshet = balshet;
        }
    }
    
}