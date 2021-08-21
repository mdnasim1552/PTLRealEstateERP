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
namespace RealERPWEB
{
    public partial class CompanyOverAllReport : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  this.Master.FindControl("printpart").Visible = false;
                //this.Master.FindControl("pnlTitle").Visible = false;

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = "01" + date.Substring(2);
                this.txtDateto.Text = Convert.ToDateTime(this.txtDateFrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");



                //DateTime date = Convert.ToDateTime(System.DateTime.Today.ToString("dd-MMM-yyyy"));
                //DateTime prevdate = date.AddMonths(-1);
                //this.txtDateFrom.Text = prevdate.ToString ("dd-MMM-yyyy");
                //this.txtDateto.Text = System.DateTime.Today.ToString ("dd-MMM-yyyy");
                // this.btnok_Click(null, null);


                Visibility();

            }
        }

        public string GetCompCode()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //return (hst["comcod"].ToString());
            // string qcomcod = this.Request.QueryString["comcod"].ToString();
            //string comcod = qcomcod ?? hst["comcod"].ToString();
            return (this.Request.QueryString["comcod"].ToString());

        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string comcod, string date1, string date2)
        {

            // Common ObjCommon = new Common ();
            //string comcod = ObjCommon.GetCompCode ();
            // string comcod = "1010";
            ProcessAccess purData = new ProcessAccess();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "RECEIPTPAYMETCASHFLOW", date1, date2, "", "", "", "", "", "", "");
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.account>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst2 = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst3 = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst4 = ds1.Tables[4].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst5 = ds1.Tables[5].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst6 = ds1.Tables[6].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst7 = ds1.Tables[7].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst8 = ds1.Tables[8].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst9 = ds1.Tables[9].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst10 = ds1.Tables[10].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst11 = ds1.Tables[11].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst12 = ds1.Tables[12].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst13 = ds1.Tables[13].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst14 = ds1.Tables[14].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst15 = ds1.Tables[15].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst16 = ds1.Tables[16].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var datalist = new MyAllData(lst, lst1, lst2, lst3, lst4, lst5, lst6, lst7, lst8, lst9, lst10, lst11, lst12, lst13, lst14, lst15, lst16);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);
            return json;
        }


        private void Visibility()
        {
            string comcod = this.GetCompCode();
            this.lblaccount.Visible = true;
            this.lblsales.Visible = true;
            this.lblpurchase.Visible = true;
            this.lblscon.Visible = false;

            this.lblcons.Visible = true;
            this.lblbbalance.Visible = true;
            this.lblstock.Visible = true;
            this.lbldues.Visible = true;
            this.lblbill.Visible = true;
            this.lblmanpower.Visible = true;
            this.lblsalary.Visible = true;
            this.lblbudget.Visible = true;
            this.lblfcost.Visible = true;
            this.lblfunvscost.Visible = true;
            this.hlnkinventory.Visible = true;
            //this.lnkratio.Visible = true;
            //this.hlnkDetails.Visible = comcod.Substring(0, 1) == "1";
            //this.hlnksubconbill.Visible = comcod.Substring(0, 1) == "1" || comcod.Substring(0, 1) == "3";
        }
        public class MyAllData
        {
            public List<RealEntity.C_22_Sal.EClassSales_02.account> account { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> sales { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> purchase { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> construction { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> bankbalance { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> stock { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> penbil { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> ffund { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> conprogress { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> dues { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> fcost { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> fundcost { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> landpro { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> inventory { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> prjrpt { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> ratio { get; set; }

            public List<RealEntity.C_22_Sal.EClassSales_02.sales> subcontractor { get; set; }

            public MyAllData()
            {

            }
            public MyAllData(List<RealEntity.C_22_Sal.EClassSales_02.account> account, List<RealEntity.C_22_Sal.EClassSales_02.sales> sales,
                List<RealEntity.C_22_Sal.EClassSales_02.sales> purchase, List<RealEntity.C_22_Sal.EClassSales_02.sales> construction,
                List<RealEntity.C_22_Sal.EClassSales_02.sales> bankbalance, List<RealEntity.C_22_Sal.EClassSales_02.sales> stock,
                List<RealEntity.C_22_Sal.EClassSales_02.sales> penbil, List<RealEntity.C_22_Sal.EClassSales_02.sales> dues,
                List<RealEntity.C_22_Sal.EClassSales_02.sales> ffund, List<RealEntity.C_22_Sal.EClassSales_02.sales> conprogress,
                List<RealEntity.C_22_Sal.EClassSales_02.sales> fcost, List<RealEntity.C_22_Sal.EClassSales_02.sales> fundcost,
                List<RealEntity.C_22_Sal.EClassSales_02.sales> landpro, List<RealEntity.C_22_Sal.EClassSales_02.sales> inventory,
                List<RealEntity.C_22_Sal.EClassSales_02.sales> prjrpt, List<RealEntity.C_22_Sal.EClassSales_02.sales> ratio, List<RealEntity.C_22_Sal.EClassSales_02.sales> subcontractor)
            {
                this.account = account;
                this.sales = sales;
                this.purchase = purchase;
                this.construction = construction;
                this.bankbalance = bankbalance;
                this.stock = stock;
                this.penbil = penbil;
                this.ffund = ffund;
                this.conprogress = conprogress;
                this.dues = dues;
                this.fcost = fcost;
                this.fundcost = fundcost;
                this.landpro = landpro;
                this.inventory = inventory;
                this.prjrpt = prjrpt;
                this.ratio = ratio;
                this.subcontractor = subcontractor;
            }
        }

    }
}