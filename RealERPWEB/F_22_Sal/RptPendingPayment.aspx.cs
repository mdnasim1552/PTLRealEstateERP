using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptPendingPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData()
        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            ProcessAccess MktData = new ProcessAccess();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RECEIVIABLE", "PENDINGPAYMENT", "", "", "", "", "", "", "", "", "");



            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.PostDCheck>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.Sales_BO.ApprrovBill>();
            var lst2 = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.Sales_BO.BillInPro>();
            var lst3 = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.Sales_BO.ChqInPro>();
            var datalist = new MyAllGraphData(lst, lst1, lst2, lst3);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);
            return json;

        }
        public class MyAllGraphData
        {
            public List<RealEntity.C_22_Sal.Sales_BO.PostDCheck> pdc { get; set; }
            public List<RealEntity.C_22_Sal.Sales_BO.ApprrovBill> approvbill { get; set; }
            public List<RealEntity.C_22_Sal.Sales_BO.BillInPro> billinpro { get; set; }
            public List<RealEntity.C_22_Sal.Sales_BO.ChqInPro> chqinpro { get; set; }

            public MyAllGraphData()
            {

            }
            public MyAllGraphData(List<RealEntity.C_22_Sal.Sales_BO.PostDCheck> pdc, List<RealEntity.C_22_Sal.Sales_BO.ApprrovBill> approvbill, List<RealEntity.C_22_Sal.Sales_BO.BillInPro> billinpro, List<RealEntity.C_22_Sal.Sales_BO.ChqInPro> chqinpro)
            {
                this.pdc = pdc;
                this.approvbill = approvbill;
                this.billinpro = billinpro;
                this.chqinpro = chqinpro;


            }
        }
    }
}