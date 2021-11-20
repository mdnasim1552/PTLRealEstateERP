using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RealERPLIB;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace RealERPWEB
{
    public partial class index2 : System.Web.UI.Page
    {
        ProcessAccess ulogin = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "ERP DASHBOARD";
                this.ParentDir.Text = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;

                this.GetCompCode();
                (this.Master.FindControl("DDPrintOpt")).Visible = false;
                (this.Master.FindControl("lnkPrint")).Visible = false;
                 
                string comcod = this.GetCompCode();
                if (comcod.Substring(0, 1) == "8")
                {
                   // this.div_groupUSers.Visible = true;
                   // this.getComName();
                    //this.ddlCompcode_SelectedIndexChanged(null, null);
                }
                else
                {
                 //   this.div_groupUSers.Visible = false;
              //      this.getComponnet();
                    this.getGraphComponent();
                   // this.getUserLogData();
                  //  this.getHomeWidget();
                    //ddlyearSale.SelectedIndex = 1;
                    
                    this.ddlyearSale_SelectedIndexChanged(null, null);
                }
            }
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void getGraphComponent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;

            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = ulogin.GetTransInfo(comcod, "SP_UTILITY_USER_DASHBOARD", "GETUSERGRAPHCOMPNENT", usrid, "3", fdate, "", "", "", "", "", "");
            if (ds2 == null)
                return;

            DataTable dt = ds2.Tables[0];
            string component = "";
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 0)
                {
                    component += "<li class='nav-item'><a class='nav-link show active' data-toggle='tab' href='#ContentPlaceHolder1_tab_" + row["MENUID"] + "'>" + row["title"] + "</a></li>";

                    if (row["MENUID"].ToString() == "1343")
                    {
                        tab_1343.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1232")
                    {

                        tab_1232.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1231")
                    {

                        tab_1231.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1233")
                    {

                        tab_1233.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1234")
                    {

                        tab_1234.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1235")
                    {

                        tab_1235.Attributes["class"] = "tab-pane fade show active";

                    }
                    else if (row["MENUID"].ToString() == "1236")
                    {

                        tab_1236.Attributes["class"] = "tab-pane fade show active";

                    }
                    else
                    {
                        tab_1231.Attributes["class"] = "tab-pane fade show active";

                    }
                }
                else
                {
                    component += "<li class='nav-item'><a class='nav-link' data-toggle='tab' href='#ContentPlaceHolder1_tab_" + row["MENUID"] + "'>" + row["title"] + "</a></li>";

                }
                i++;
            }
            this.userGraph.InnerHtml = component;
            this.Hypersales.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=sales";
            this.HyperProcurement.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=Procurement";
            this.HypAccounts.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=Accounts";
            this.hypConstruction.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=Construction";
            this.lblSubContractor.NavigateUrl = "CompanyOverAllReport?comcod=" + comcod + "&Type=SubContractor";
            this.hypCrmDetails.NavigateUrl = "F_21_MKT/RptSalesFunnel";

            if (dt.Rows.Count > 0)
            {
                string fxdate = System.DateTime.Today.ToString("MMM");
                // this.ddlMonths.SelectedValue = "00";//fxdate.ToString();
                //ddlyearSale_SelectedIndexChanged(null, null);
            }
            else
            {
                this.divgsraph.Visible = false;
            }

        }

        protected void ddlyearSale_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetGraphFilterData();

        }
        private void GetGraphFilterData()
        {
            string ddlmonths = this.ddlMonths.SelectedValue.ToString();

            if (ddlmonths == "00")
            {
                this.ShowData();
            }
            else
            {
               // this.showDataMonthly(ddlmonths);
            }
        }
        protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetGraphFilterData();
        }
        protected void ddlGraphtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGraphFilterData();

        }

        private void ShowData()
        {
            this.pnlMonthlySales.Visible = false;
            this.pnlsalchart.Visible = true;
            this.Panel2.Visible = true;
            this.Panel1.Visible = false;

            this.Panel4.Visible = true;
            this.Panel3.Visible = false;

            this.Panel5.Visible = false;
            this.Panel6.Visible = true;

            this.Panel7.Visible = false;
            this.Panel8.Visible = true;
            this.Panel9.Visible = true;

            string gtype = this.ddlGraphtype.SelectedValue.ToString();
            // For Cache Data

            DataSet ds2 = new DataSet();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string userrole = hst["usrrmrk"].ToString();
            string usercode = "0000000";
            string fdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string ddlyear = this.ddlyearSale.SelectedValue.ToString();
            string pdate = "01-Jan-" + ddlyear;
            // string tdate = "01-Jan-" + ddlyear;

            if (Cache["dsinterface"] == null)
            {
                ds2 = ulogin.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEALLTESTPURPOSE", pdate, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                int minute = this.GetCacheTimeinMinute();
                Cache.Remove("dsinterface");
                Cache.Insert("dsinterface", ds2, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);
            }

            else
            {


                ds2 = (DataSet)Cache["dsinterface"];
                string pcomod = ds2.Tables[0].Rows.Count == 0 ? comcod : ds2.Tables[0].Rows[0]["comcod"].ToString();
                if (pcomod != comcod)
                {

                    ds2 = ulogin.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEALLTESTPURPOSE", pdate, "", "", "", "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    int minute = this.GetCacheTimeinMinute();
                    Cache.Remove("dsinterface");
                    Cache.Insert("dsinterface", ds2, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);

                }

                else
                {
                    ds2 = (DataSet)Cache["dsinterface"];

                }

            }


            string empid = "%";
            string prjcode = "%";
            string professioncode = "%";
            string sourceref = "%";

            DataSet ds2CRM2 = ulogin.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETSALESFUNNEL", empid, pdate, prjcode, professioncode, tdate, sourceref, "", "95%");
            if (ds2CRM2 == null)
            {
                return;
            }

            var jsonSerialiser = new JavaScriptSerializer();
            //if (userrole == "admin")
            //{

            var lst = ds2.Tables[0].DataTableToList<Salgraph>();
            var lst1 = ds2.Tables[1].DataTableToList<Purgraph>();
            var lst2 = ds2.Tables[2].DataTableToList<Accgraph>();
            var lst3 = ds2.Tables[3].DataTableToList<Consgraph>();
            var lst4 = ds2.Tables[4].DataTableToList<Scongraph>();
            var lst5 = ds2CRM2.Tables[1].DataTableToList<SalFunnelgraph>();

            var data1 = jsonSerialiser.Serialize(lst1);
            var data2 = jsonSerialiser.Serialize(lst2);
            var data3 = jsonSerialiser.Serialize(lst3);
            var data = jsonSerialiser.Serialize(lst);
            var data4 = jsonSerialiser.Serialize(lst4);
            var crm = jsonSerialiser.Serialize(lst5);


          //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + data + "','" + data1 + "','" + data2 + "','" + data3 + "','" + data4 + "','" + gtype + "','" + crm + "')", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + data + "','" + data1 + "','" + data2 + "','" + data3 + "','" + data4 + "','" + gtype + "')", true);




            ds2.Dispose();
            ds2CRM2.Dispose();

        }

        private int GetCacheTimeinMinute()
        {

            int minute = 0;
            return minute;

        }


        [Serializable]
        public class SalFunnelgraph
        {
            public decimal query { get; set; }
            public decimal lead { get; set; }
            public decimal qualiflead { get; set; }
            public decimal finalnego { get; set; }
            public decimal nego { get; set; }
            public decimal win { get; set; }
            public decimal total { get; set; }
        }
    }
}