
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEntity;
using RealEntity.C_08_PPlan;
using RealERPLIB;
namespace RealERPWEB.F_34_Mgt
{
    public partial class RptAllDashboard : System.Web.UI.Page
    {
        BL_UserManage_Con objUserService = new BL_UserManage_Con();
        ProcessAccess _DataEntry = new ProcessAccess();
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (prevPage.Length == 0)
                //{
                //    prevPage = Request.UrlReferrer.ToString();
                //}
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.txtCurTransDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ExRelz") ? "Export & Realization Dashboard" :
                    (type == "Purchase") ? "Purchase Dashboard" : (type == "Accounts") ? "Accounts Dashboard" : (type == "Production" || type == "ProductionRMG") ? "Production Dashboard" : "";

                // this.hdntype.Value = this.Request.QueryString["Type"];
                //this.SelectView();

                // this.OkBtn_Click(null, null);
                this.VisibilityReOActual();
            }
        }

        private void VisibilityReOActual()
        {
            string type = this.Request.QueryString["Type"];

            switch (type)
            {

                case "Sales":
                    this.rbtList.Visible = true;

                    break;
                default:
                    this.rbtList.Visible = false;

                    break;



            }
        }
        private string GetCompCode()
        {
            if (Request.QueryString["comcod"] != null)
            {
                string comcod = this.Request.QueryString["comcod"].ToString();
                return (comcod);
            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }


        }







        public void SelectView()
        {
            string type = this.Request.QueryString["Type"].Trim().ToString();
            switch (type)
            {
                case "Sales":
                    GetSalesData();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "Purchase":
                    GetPurchaseData();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "Accounts":
                    GetAccountsData();
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "Construction":
                    GetProductionData();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                // Emdad 
                case "SubConBillaPay":
                    GetConBillAndPay();
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

            }

        }
        public void GetSalesData()
        {

            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"

            string typedata = this.rbtList.SelectedValue.ToString();

            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO_ALL", "SALESANALYSIS_DASHBOARD", "%", dates, typedata, "", "", "", "", "");
            if (ds2 == null)
                return;
            List<double> singldata = new List<double>();
            double ttlsalamtyear = ds2.Tables[4].Rows.Count == 0 ? 0 : Convert.ToDouble(ds2.Tables[4].Rows[0]["ttlsalamtyear"].ToString());
            double collamtyear = ds2.Tables[4].Rows.Count == 0 ? 0 : Convert.ToDouble(ds2.Tables[4].Rows[0]["collamtyear"].ToString());
            double recam = ds2.Tables[4].Rows.Count == 0 ? 0 : Convert.ToDouble(ds2.Tables[4].Rows[0]["recam"].ToString());
            double percn = ds2.Tables[2].Rows[0]["percn"].ToString() == "" ? 0 : Convert.ToDouble(ds2.Tables[2].Rows[0]["percn"].ToString());

            singldata.Add(Convert.ToDouble(ds2.Tables[0].Rows[0]["newcust"].ToString()));
            singldata.Add(Convert.ToDouble(ds2.Tables[1].Rows[0]["ttlsalamtmon"].ToString()));
            singldata.Add(Convert.ToDouble(ds2.Tables[1].Rows[0]["collamtmon"].ToString()));
            singldata.Add(Convert.ToDouble(ds2.Tables[2].Rows[0]["ttlsalamtday"].ToString()));
            singldata.Add(Convert.ToDouble(ds2.Tables[2].Rows[0]["collamtday"].ToString()));
            singldata.Add(percn);
            singldata.Add(ttlsalamtyear);
            singldata.Add(collamtyear);
            singldata.Add(recam);
            singldata.Add(Convert.ToDouble(ds2.Tables[1].Rows[0]["targtsaleamt"].ToString()));
            singldata.Add(Convert.ToDouble(ds2.Tables[1].Rows[0]["tarcollamt"].ToString()));
            List<EClassMonthly> weeksal = ds2.Tables[3].DataTableToList<EClassMonthly>();
            List<EClassMonthly> monthsal = ds2.Tables[5].DataTableToList<EClassMonthly>();
            List<EclassSalCommon> custname = ds2.Tables[6].DataTableToList<EclassSalCommon>();
            List<EclassSalCommon> unitname = ds2.Tables[7].DataTableToList<EclassSalCommon>();
            List<EclassSalCommon> saleteam = ds2.Tables[8].DataTableToList<EclassSalCommon>();
            var list = custname.Concat(unitname).Concat(saleteam).ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list);
            var json2 = jsonSerialiser.Serialize(weeksal);
            var json3 = jsonSerialiser.Serialize(monthsal);
            var json4 = jsonSerialiser.Serialize(singldata);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteSalesGraph('" + json + "','" + json2 + "','" + json3 + "','" + json4 + "')", true);


        }
        public void GetPurchaseData()
        {
            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"
                                                                                                        // string month = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("MMM");
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO_ALL", "PURPAYANALYSISGRAPH", dates, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            List<data2> weeklypur = ds2.Tables[2].DataTableToList<data2>();
            List<data1> topsuppur = ds2.Tables[4].DataTableToList<data1>();
            List<data1> topmat = ds2.Tables[5].DataTableToList<data1>();
            List<data1> topsupout = ds2.Tables[7].DataTableToList<data1>();
            List<data1> topsuppay = ds2.Tables[6].DataTableToList<data1>();
            List<data2> purmonth = ds2.Tables[3].DataTableToList<data2>();
            List<data2> curmonth = ds2.Tables[0].DataTableToList<data2>();
            var monthly = purmonth.Concat(curmonth).Concat(weeklypur).ToList();
            var top5data = topsuppur.Concat(topmat).Concat(topsupout).Concat(topsuppay).ToList();
            var jsonSerialiser = new JavaScriptSerializer();

            var pur_json = jsonSerialiser.Serialize(monthly);
            var pur_json1 = jsonSerialiser.Serialize(top5data);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecutePurchaseGraph('" + pur_json + "','" + pur_json1 + "')", true);

        }
        public void GetAccountsData()
        {
            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"        
            DataSet ds1 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO", "ACCINFOYMONTH", dates, "", "", "", "", "", "", "");
            var monthacc = ds1.Tables[0].DataTableToList<EClassAccMonthly>();
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_MIS_GRAPH", "GET_MIS_GRAPH_DATA", dates, "", "", "", "", "", "", "");

            List<EclassBalSheetSum> balsheet = ds2.Tables[0].DataTableToList<EclassBalSheetSum>();
            DataSet accds = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO_ALL", "ACCOUNTS_DASHBOARD_INFORMATION", dates, "", "", "", "", "", "", "");
            var curbalnce = accds.Tables[0].DataTableToList<EclassOverallBalance>();
            var todayreceive = accds.Tables[1].DataTableToList<EclassOverallBalance>();
            var todaypay = accds.Tables[2].DataTableToList<EclassOverallBalance>();
            var monthlyrec = accds.Tables[3].DataTableToList<EclassOverallBalance>();
            var monthpay = accds.Tables[4].DataTableToList<EclassOverallBalance>();
            var curmonth = monthacc.FindAll(s => s.yearmon1 == Convert.ToDateTime(dates).ToString("MMM"));
            var balsheetlist = balsheet.FindAll(p => p.grp == "2");
            var jsonSerialiser = new JavaScriptSerializer();
            var bal_json = jsonSerialiser.Serialize(balsheetlist);
            var acc_json = jsonSerialiser.Serialize(monthacc.Concat(curmonth));
            var curbl_json = jsonSerialiser.Serialize(curbalnce);
            var todayreceive_json = jsonSerialiser.Serialize(todayreceive);
            var todaypay_json = jsonSerialiser.Serialize(todaypay);
            var monthrecpay_json = jsonSerialiser.Serialize(monthlyrec);
            var monthpay_json = jsonSerialiser.Serialize(monthpay);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteAccGraph('" + bal_json + "','" + acc_json + "','" + curbl_json + "','" + todayreceive_json + "','" + todaypay_json + "','" + monthrecpay_json + "','" + monthpay_json + "')", true);

        }

        public void GetProductionData()
        {
            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"
            List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly> list2 = objUserService.ShowConMonth(comcod, dates);
            DataSet ds1 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO_LP", "CONSANALYSIS_DASHBOARD", dates, "", "", "", "", "", "", "");
            List<RealEntity.C_08_PPlan.BO_Class_Con.ConsAnaGraphClass2> list1 = ds1.Tables[2].DataTableToList<RealEntity.C_08_PPlan.BO_Class_Con.ConsAnaGraphClass2>();
            List<RealEntity.C_08_PPlan.BO_Class_Con.ConsAnaGraphClass1> list3 = ds1.Tables[1].DataTableToList<RealEntity.C_08_PPlan.BO_Class_Con.ConsAnaGraphClass1>();


            List<string> list4 = new List<string>();
            list4.Add(ds1.Tables[0].Rows[0]["montaramt"].ToString());
            list4.Add(ds1.Tables[0].Rows[0]["monexeamt"].ToString());
            var jsonSerialiser = new JavaScriptSerializer();
            var projB = jsonSerialiser.Serialize(list1);
            var projA = jsonSerialiser.Serialize(list3);
            var moncons = jsonSerialiser.Serialize(list2);
            var singldata = jsonSerialiser.Serialize(list4);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteProductionGrpah('" + projA + "','" + projB + "','" + moncons + "','" + singldata + "')", true);

        }

        public void GetConBillAndPay()
        {
            string comcod = this.GetCompCode();
            string dates = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");//"10-Apr-2018"
                                                                                                        // string month = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("MMM");
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO_ALL", "SUBCONBILLANDPAYANALYSISGRAPH", dates, "", "", "", "", "", "", "");
            List<data2> weeklypur = ds2.Tables[2].DataTableToList<data2>();
            List<data2> purmonth = ds2.Tables[3].DataTableToList<data2>();
            List<data2> curmonth = ds2.Tables[0].DataTableToList<data2>();
            var monthly = purmonth.Concat(curmonth).Concat(weeklypur).ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var pur_json = jsonSerialiser.Serialize(monthly);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteBillGraph('" + pur_json + "')", true);

        }


        protected void OkBtn_Click(object sender, EventArgs e)
        {
            this.SelectView();
        }
    }
    [Serializable]
    public class EclassCustomerProductSumary
    {

        public string sirdesc { get; set; }
        public double itmamt { get; set; }

    }

    [Serializable]
    public class EclassSalCommon
    {

        public string sirdesc { get; set; }
        public double suamt { get; set; }

    }
    [Serializable]
    public class EClassMonthly
    {
        public string ymon { set; get; }

        public string yearmon { set; get; }

        public double ttlsalamt { set; get; }
        public double collamt { set; get; }

        public double targtsaleamt { set; get; }
        public double tarcollamt { set; get; }

    }



    [Serializable]
    public class data1
    {
        public string comcod { get; set; }
        public string sircode { get; set; }
        public string sirdesc { get; set; }
        public double itmamt { get; set; }

    }

    [Serializable]
    public class data2
    {
        public string yearmon { get; set; }
        public double ttlsalamt { get; set; }
        public double tpayamt { get; set; }
        public double ttlsalamtcore { get; set; }
        public double tpayamtcore { get; set; }


    }
    [Serializable]
    public class EclassOverallBalance
    {
        public string cactcode { get; set; }
        public string cactdesc { get; set; }
        public double trnam { get; set; }

    }
    [Serializable]
    public class EClassAccMonthly
    {
        public string yearmon { set; get; }
        public string yearmon1 { set; get; }
        public double dram { set; get; }
        public double cram { set; get; }
        public double dramcore { set; get; }
        public double cramcore { set; get; }
    }
    [Serializable]
    public class EclassBalSheetSum
    {
        public string grp { get; set; }
        public string grpdesc { get; set; }
        public double noncuram { get; set; }
        public double curam { get; set; }
        public double equityam { get; set; }
        public double noncurlia { get; set; }
        public double curlia { get; set; }
        public double toasset { get; set; }
        public double tolib { get; set; }
    }
}
