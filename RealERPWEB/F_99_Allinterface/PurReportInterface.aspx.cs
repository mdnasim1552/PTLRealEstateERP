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
using RealERPLIB;
namespace RealERPWEB.F_99_Allinterface
{

    public partial class PurReportInterface : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = "01" + date.Substring(2);
                this.txtDateto.Text = Convert.ToDateTime(this.txtDateFrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                Visibility();


            }
        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;

        }






        //[WebMethod(EnableSession = false)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public static string GetAllData(string comcod, string date1, string date2)
        //{

        //    // Common ObjCommon = new Common ();
        //    //string comcod = ObjCommon.GetCompCode ();
        //    // string comcod = "1010";
        //    ProcessAccess purData = new ProcessAccess();
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEALL", date1, date2, "", "", "", "", "", "", "");

        //    var lst1 = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();

        //    var datalist = new MyAllData(lst1);
        //    var jsonSerialiser = new JavaScriptSerializer();
        //    var json = jsonSerialiser.Serialize(datalist);
        //    return json;
        //}

        public void GetAllData()
        {

            // Common ObjCommon = new Common ();
            //string comcod = ObjCommon.GetCompCode ();
            // string comcod = "1010";
            string comcod = this.GetCompCode();
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            //ProcessAccess purData = new ProcessAccess();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEALL", date1, date2, "", "", "", "", "", "", "");

            var lst1 = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();

            var datalist = new MyAllData(lst1);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "GetData('" + json + "')", true);
        }


        private void Visibility()
        {
            string comcod = this.GetCompCode();
            //this.lblaccount.Visible = true;
            //this.lblsales.Visible = true;
            this.lblpurchase.Visible = true;
            //this.lblcons.Visible = true;
            //this.lblbbalance.Visible = true;
            //this.lblstock.Visible = true;
            //this.lbldues.Visible = true;
            //this.lblbill.Visible = true;
            //this.lblmanpower.Visible = true;
            //this.lblsalary.Visible = true;
            //this.lblfcost.Visible = true;
            //this.lblfunvscost.Visible = true;
            //this.hlnkDetails.Visible = comcod.Substring(0, 1) == "1";
            //this.hlnksubconbill.Visible = comcod.Substring(0, 1) == "1";
        }
        public class MyAllData
        {
            //public List<RealEntity.C_22_Sal.EClassSales_02.account> account { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> sales { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> purchase { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> construction { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> bankbalance { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> stock { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> penbil { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> ffund { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> conprogress { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> dues { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> fcost { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> fundcost { get; set; }
            //public List<RealEntity.C_22_Sal.EClassSales_02.sales> landpro { get; set; }
            public MyAllData()
            {

            }
            public MyAllData(
                List<RealEntity.C_22_Sal.EClassSales_02.sales> purchase)
            {

                this.purchase = purchase;


            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.graphpart.Visible = true;

            this.GetAllData();



        }



    }
}