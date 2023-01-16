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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_22_Sal
{
    public partial class MonthlySalesBudget03 : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Yearly  Sales Budget ";

                this.GetYear();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetYear()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEAR", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyear.DataTextField = "year1";
            this.ddlyear.DataValueField = "year1";
            this.ddlyear.DataSource = ds1.Tables[0];
            this.ddlyear.DataBind();
            this.ddlyear.SelectedValue = System.DateTime.Today.Year.ToString();
            ds1.Dispose();
        }








        private void Data_Bind()
        {



            DataTable dtymon = (DataTable)ViewState["tblymon"];
            //int j = 2;
            //for (int i = 0; i < dtymon.Rows.Count; i++)
            //{
            //    gvySalbgd.Columns[j++].HeaderText = dtymon.Rows[i]["yearmon"].ToString();
            //    if (j == 26)
            //        break;



            //}

            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            this.gvySalbgd.DataSource = tbl1;
            this.gvySalbgd.DataBind();
            this.FooterCalculation(tbl1);

        }




        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;




            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty1)", "")) ? 0.00
            : dt.Compute("Sum(qty1)", ""))).ToString("#,##0;(#,##0);  ");
            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt1)", "")) ? 0.00
              : dt.Compute("Sum(amt1)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty2)", "")) ? 0.00
               : dt.Compute("Sum(qty2)", ""))).ToString("#,##0;(#,##0);  ");
            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt2)", "")) ? 0.00
           : dt.Compute("Sum(amt2)", ""))).ToString("#,##0;(#,##0);  ");
            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty3)", "")) ? 0.00
            : dt.Compute("Sum(qty3)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt3)", "")) ? 0.00
             : dt.Compute("Sum(amt3)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty4)", "")) ? 0.00
            : dt.Compute("Sum(qty4)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt4)", "")) ? 0.00
   : dt.Compute("Sum(amt4)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty5)", "")) ? 0.00
                : dt.Compute("Sum(qty5)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt5)", "")) ? 0.00
     : dt.Compute("Sum(amt5)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty6)", "")) ? 0.00
           : dt.Compute("Sum(qty6)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt6)", "")) ? 0.00
     : dt.Compute("Sum(amt6)", ""))).ToString("#,##0;(#,##0);  ");


            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty7)", "")) ? 0.00
      : dt.Compute("Sum(qty7)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt7)", "")) ? 0.00
     : dt.Compute("Sum(amt7)", ""))).ToString("#,##0;(#,##0);  ");


            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty8)", "")) ? 0.00
      : dt.Compute("Sum(qty8)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt8)", "")) ? 0.00
     : dt.Compute("Sum(amt8)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty9)", "")) ? 0.00
      : dt.Compute("Sum(qty9)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt9)", "")) ? 0.00
     : dt.Compute("Sum(amt9)", ""))).ToString("#,##0;(#,##0);  ");


            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty10)", "")) ? 0.00
      : dt.Compute("Sum(qty10)", ""))).ToString("#,##0;(#,##0);  ");
            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt10)", "")) ? 0.00
     : dt.Compute("Sum(amt10)", ""))).ToString("#,##0;(#,##0);  ");


            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty11)", "")) ? 0.00
      : dt.Compute("Sum(qty11)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt11)", "")) ? 0.00
     : dt.Compute("Sum(amt11)", ""))).ToString("#,##0;(#,##0);  ");


            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty12)", "")) ? 0.00
      : dt.Compute("Sum(qty12)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt12)", "")) ? 0.00
     : dt.Compute("Sum(amt12)", ""))).ToString("#,##0;(#,##0);  ");


            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tqty)", "")) ? 0.00
         : dt.Compute("Sum(tqty)", ""))).ToString("#,##0;(#,##0);  ");







        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            int rowindex;
            for (int i = 0; i < this.gvySalbgd.Rows.Count; i++)
            {



                rowindex = this.gvySalbgd.PageSize * this.gvySalbgd.PageIndex + i;
                tbl1.Rows[rowindex]["qty1"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt1"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt1")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["qty2"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty2")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt2"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt2")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["qty3"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty3")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt3"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt3")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["qty4"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty4")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt4"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt4")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["qty5"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty5")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt5"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt5")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["qty6"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty6")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt6"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt6")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["qty7"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty7")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt7"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt7")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["qty8"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty8")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt8"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt8")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["qty9"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty9")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt9"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt9")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty10"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty10")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt10"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt10")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty11"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty11")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt11"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt11")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty12"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty12")).Text.Trim()).ToString();
                tbl1.Rows[rowindex]["amt12"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvamt12")).Text.Trim()).ToString();



                tbl1.Rows[rowindex]["tqty"] = Convert.ToDouble(tbl1.Rows[rowindex]["qty1"]) + Convert.ToDouble(tbl1.Rows[rowindex]["qty2"])
                      + Convert.ToDouble(tbl1.Rows[rowindex]["qty3"]) + Convert.ToDouble(tbl1.Rows[rowindex]["qty4"]) + Convert.ToDouble(tbl1.Rows[rowindex]["qty5"])
                      + Convert.ToDouble(tbl1.Rows[rowindex]["qty6"]) + Convert.ToDouble(tbl1.Rows[rowindex]["qty7"]) + Convert.ToDouble(tbl1.Rows[rowindex]["qty8"])
                      + Convert.ToDouble(tbl1.Rows[rowindex]["qty9"]) + Convert.ToDouble(tbl1.Rows[rowindex]["qty10"]) + Convert.ToDouble(tbl1.Rows[rowindex]["qty11"])
                      + Convert.ToDouble(tbl1.Rows[rowindex]["qty12"]);




        





            }


            ViewState["tblsal"] = tbl1;


        }

        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();      
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //LocalReport Rpt1 = new LocalReport();

            //DataTable dt = (DataTable)ViewState["tblsal"];
            //DataTable dt1 = (DataTable)ViewState["tblymon"];

            //string salesteam = ddlteam.SelectedItem.Text.ToString();
            //var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassYearlyTarget>();

            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalesTarget", lst, null, null);

            //DateTime datefrm = Convert.ToDateTime("01-jan-" + ddlyear.SelectedValue.ToString());
            //DateTime dateto = Convert.ToDateTime("31-dec-" + ddlyear.SelectedValue.ToString());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    Rpt1.SetParameters(new ReportParameter("txtmon" + i, datefrm.ToString("MMM yy")));

            //    datefrm = datefrm.AddMonths(1);

            //}

            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters(new ReportParameter("comname", comnam));   
            //Rpt1.SetParameters(new ReportParameter("year", "Year : " + ddlyear.SelectedItem.Text.ToString()));
            //Rpt1.SetParameters(new ReportParameter("salesteam", "Sales Team : " +salesteam ));
            //Rpt1.SetParameters(new ReportParameter("header", "Yearly Sales Budget"));
            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lbtnYearbgd_Click(object sender, EventArgs e)
        {

            if (this.lbtnYearbgd.Text == "Ok")
            {

                this.lbtnYearbgd.Text = "New";
                this.ddlyear.Enabled = false;


                this.ShowYearlyBudget();
                return;

            }

            this.lbtnYearbgd.Text = "Ok";
            this.ddlyear.Enabled = true;

            this.gvySalbgd.DataSource = null;
            this.gvySalbgd.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";






        }

        private void ShowYearlyBudget()
        {

            string comcod = this.GetCompCode();
            string Year = this.ddlyear.SelectedValue.ToString();

            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "YEARLYSALESTARGETALLEMPLOYEE", Year, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvySalbgd.DataSource = null;
                this.gvySalbgd.DataBind();
                return;


            }
            ViewState["tblsal"] = ds1.Tables[0];
            ViewState["tblymon"] = ds1.Tables[1];
            this.Data_Bind();
        }
        protected void lbYearbgdTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnYBgdUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string Year = this.ddlyear.SelectedValue.ToString();

                bool result = true;



                result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "DELETESALCOLLTARINFALLEMPLOYEE", Year, "", "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = SalesData.ErrorObject["Msg"].ToString();
                    return;
                }


                // Summary
                //for (int i = 1; i <= 12; i++)
                //{



                //    double amt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt" + i + ")", "")) ? 0.00 : dt1.Compute("Sum(amt" + i + ")", "")));

                //   // double amt = Convert.ToDouble(dt1.Rows[i]["amt" + i.ToString()].ToString());
                //    string collamt = "0";
                //    string monthid = this.ddlyear.SelectedValue.ToString() + ASTUtility.Right("0" + i.ToString(), 2);
                //    if (amt != 0)
                //    {
                //        result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INSORUPSALCOLTARINF",  monthid, teamcode, amt.ToString(), collamt, "0", "0", "", "", "", "", "", "", "");



                //        if (result == false)
                //        {
                //         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                //            return;
                //        }




                //    }




                //}


                //Details

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    int j = 1;

                    string teamcode = dt1.Rows[i]["empid"].ToString();
                    string pactcode = "000000000000";

                    //if (perc != 0)
                    //{
                    // result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSORUPYSCOLPURBGDINF", "YEARLYBGDB", Year, rescode, perc.ToString(), "", "", "", "", "", "", "", "", "", "", "");
                    // }

                    while (j <= 12)
                    {
                        double qty = Convert.ToDouble(dt1.Rows[i]["qty" + j.ToString()].ToString());
                        double amt = Convert.ToDouble(dt1.Rows[i]["amt" + j.ToString()].ToString());
                        string monthid = this.ddlyear.SelectedValue.ToString() + ASTUtility.Right("0" + j.ToString(), 2);
                        if (qty != 0)
                        {
                            result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INSERTORUPSALCOLLTARPINF", Year, monthid, teamcode, pactcode, qty.ToString(), amt.ToString(), "", "", "", "", "", "", "", "", "");

                            if (result == false)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                                return;
                            }

                        }


                        j++;


                    }


                 ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";




                }






            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }

        }

        protected void gvySalbgd_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 2;
                gvrow.Cells.Add(cell0);


                DataTable dtymon = (DataTable)ViewState["tblymon"];
                int j = 2;
                for (int i = 0; i < dtymon.Rows.Count; i++)
                {

                    TableCell cell = new TableCell();
                    cell.Text = dtymon.Rows[i]["yearmon"].ToString();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.ColumnSpan = 2;
                    cell.Font.Bold = true;
                    gvrow.Cells.Add(cell);

                    if (j == 30)
                        break;


                }


                TableCell celll = new TableCell();
                celll.Text = "";
                celll.HorizontalAlign = HorizontalAlign.Center;
                celll.ColumnSpan = 2;
                gvrow.Cells.Add(celll);



                //this.gvHourlyProd.Columns[5 + i].HeaderText = dt2.Rows[i]["gdesc"].ToString();
                //  i++;


                gvySalbgd.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvySalbgd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvySalbgd.PageIndex = e.NewPageIndex;
            this.Data_Bind();



        }
    }
}