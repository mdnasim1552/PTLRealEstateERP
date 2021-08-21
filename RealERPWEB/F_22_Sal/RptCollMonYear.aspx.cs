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
using RealEntity.C_22_Sal;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptCollMonYear : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = (this.Request.QueryString["Date1"].Length > 0) ? this.Request.QueryString["Date1"] : Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = (this.Request.QueryString["Date2"].Length > 0) ? this.Request.QueryString["Date2"] : Convert.ToDateTime(txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.Visibility();
                // After problem
            }

        }


        public string GetCompCode()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //return (hst["comcod"].ToString ());
            return (this.Request.QueryString["comcod"].ToString());

        }

        private void Visibility()
        {

            string monsalname = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM yyyy");

            this.lblmonsal.Visible = true;
            this.lblmonsal.Text = "Collection " + monsalname;

            string yearsalname = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM");
            this.lblyearlysal.Visible = true;
            this.lblyearlysal.Text = "Collection " + yearsalname + " YTD";
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string date1, string date2)
        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            ProcessAccess MktData = new ProcessAccess();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTMONTHLYSALE", date1, date2, "", "", "", "", "", "", "");
            var lst = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.monsale>();
            var lst1 = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.yearsale>();
            var datalist = new MyAllGraphData(lst, lst1);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);
            return json;

        }
        public class MyAllGraphData
        {
            public List<RealEntity.C_22_Sal.EClassSales_02.monsale> monsale { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.yearsale> yearsale { get; set; }

            public MyAllGraphData()
            {

            }
            public MyAllGraphData(List<RealEntity.C_22_Sal.EClassSales_02.monsale> monsale, List<RealEntity.C_22_Sal.EClassSales_02.yearsale> yearsale)
            {
                this.monsale = monsale;
                this.yearsale = yearsale;


            }
        }

    }
}