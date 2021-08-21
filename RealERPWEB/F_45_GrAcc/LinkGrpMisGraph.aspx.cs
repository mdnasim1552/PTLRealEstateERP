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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkGrpMisGraph : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ////this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Graph";
                this.ShowGraph();






            }
        }
        private void ShowGraph()
        {



            string category = "";



            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{
            //    category = category + "," + dt1.Rows[i]["Name"].ToString();
            //}
            decimal[] values = new decimal[1];
            decimal[] values2 = new decimal[1];
            decimal[] values3 = new decimal[1];
            decimal[] values4 = new decimal[1];
            decimal[] values5 = new decimal[1];


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    values[i] = Convert.ToDecimal(dt.Rows[i]["orderamt"]);
            //    values2[i] = Convert.ToDecimal(dt.Rows[i]["proamt"]);
            //    values3[i] = Convert.ToDecimal(dt.Rows[i]["shipamt"]);

            //}


            values[0] = Convert.ToDecimal(this.Request.QueryString["capacity"].ToString().Trim());
            values2[0] = Convert.ToDecimal(this.Request.QueryString["masbgd"].ToString().Trim());
            values3[0] = Convert.ToDecimal(this.Request.QueryString["bep"].ToString().Trim());
            values4[0] = Convert.ToDecimal(this.Request.QueryString["ttargetamt"].ToString().Trim());
            values5[0] = Convert.ToDecimal(this.Request.QueryString["acsalamt"].ToString().Trim());



            BarChart1.CategoriesAxis = category.Remove(0, 0);

            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values, BarColor = "#2fd1f9", Name = "Capacity " });
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values2, BarColor = "#000000", Name = "Master Budget " });
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values3, BarColor = "#2fd5g9", Name = "Break-even " });
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values4, BarColor = "#2fd6g9", Name = "Monthly Budget " });
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values5, BarColor = "#2fd7g9", Name = "Actual " });
            //BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values3, BarColor = "#2fd5g9", Name = "Shipment" });


        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }





    }
}
