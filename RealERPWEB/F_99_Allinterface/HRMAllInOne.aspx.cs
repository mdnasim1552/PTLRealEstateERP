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
    public partial class HRMAllInOne : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = "01" + date.Substring(2);
                this.txtDateto.Text = date;
                // this.txtDateto.Text = Convert.ToDateTime (this.txtDateFrom.Text.Trim ()).AddMonths (1).AddDays (-1).ToString ("dd-MMM-yyyy");


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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string date1, string date2)
        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            ProcessAccess purData = new ProcessAccess();
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HRM_DASHBOARD", "HRMDETAILSDASH", date1, date2, "", "", "", "", "", "", "");
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst2 = ds1.Tables[2].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst3 = ds1.Tables[3].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst4 = ds1.Tables[4].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst5 = ds1.Tables[5].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst6 = ds1.Tables[6].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst7 = ds1.Tables[7].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            var lst8 = ds1.Tables[8].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales>();
            //var lst9 = ds1.Tables[9].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales> ();
            //var lst10 = ds1.Tables[10].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales> ();
            //var lst11 = ds1.Tables[11].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.sales> ();
            //var datalist = new MyAllData (lst, lst1, lst2, lst3, lst4, lst5, lst6, lst7, lst8, lst9, lst10, lst11);
            var datalist = new MyAllData(lst, lst1, lst2, lst3, lst4, lst5, lst6, lst7, lst8);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(datalist);
            return json;
        }


        private void Visibility()
        {
            string comcod = this.GetCompCode();
            this.lblaccount.Visible = true;
            this.lblsales.Visible = true;
            this.hlnkatt.Visible = true;
            this.lblcons.Visible = true;
            this.lblbbalance.Visible = true;
            this.lblstock.Visible = true;
            this.lbldues.Visible = true;
            this.lblbill.Visible = true;

            this.hlnkDetails.Visible = comcod.Substring(0, 1) == "1";
        }


        public class MyAllData
        {
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> member { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> attendance { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> salary { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> leave { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> confirm { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> separation { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> loan { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> pffund { get; set; }
            public List<RealEntity.C_22_Sal.EClassSales_02.sales> empjoining { get; set; }



            public MyAllData()
            {

            }
            public MyAllData(List<RealEntity.C_22_Sal.EClassSales_02.sales> member, List<RealEntity.C_22_Sal.EClassSales_02.sales> attendance, List<RealEntity.C_22_Sal.EClassSales_02.sales> salary, List<RealEntity.C_22_Sal.EClassSales_02.sales> leave, List<RealEntity.C_22_Sal.EClassSales_02.sales> confirm, List<RealEntity.C_22_Sal.EClassSales_02.sales> separation, List<RealEntity.C_22_Sal.EClassSales_02.sales> loan, List<RealEntity.C_22_Sal.EClassSales_02.sales> pffund, List<RealEntity.C_22_Sal.EClassSales_02.sales> empjoining)
            {
                this.member = member;
                this.attendance = attendance;
                this.salary = salary;
                this.leave = leave;
                this.confirm = confirm;
                this.separation = separation;
                this.loan = loan;
                this.pffund = pffund;
                this.empjoining = empjoining;





            }
        }
    }
}