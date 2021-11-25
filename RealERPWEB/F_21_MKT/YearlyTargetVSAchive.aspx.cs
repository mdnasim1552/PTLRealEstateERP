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

namespace RealERPWEB.F_21_MKT
{
    public partial class YearlyTargetVSAchive : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Yearly Activities Target VS Achievement ";

                this.GetYear();
                GetAllSubdata();
                this.GetTeamCode();
                CommonButton();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnUpdate_Click);
            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);


        }
        public void CommonButton()
        {

            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;






        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetYear()
        {
            string comcod = this.GetComeCode();

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

        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = SalesData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "LANDREFINFODDL", "", "", "", "", "", "", "", "", "");

            ViewState["tblsubddl"] = ds2.Tables[0];
            ds2.Dispose();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETGENEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
            ViewState["tblempsup"] = ds1.Tables[0];
            ds1.Dispose();

        }


        private void GetTeamCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string lempid = hst["empid"].ToString();

            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataTable dtemp = (DataTable)ViewState["tblempsup"];

            DataView dv;
            dv = dt1.Copy().DefaultView;
            DataTable dtE = new DataTable();
            dv.RowFilter = ("gcod like '93%'");
            if (userrole == "1")
            {

                dtE = dv.ToTable();
                dtE.Rows.Add("000000000000", "Choose Employee..", "");

            }

            else
            {
                DataTable dts = dv.ToTable();
                var query = (from dtl1 in dts.AsEnumerable()
                             join dtl2 in dtemp.AsEnumerable() on dtl1.Field<string>("gcod") equals dtl2.Field<string>("empid")
                             select new
                             {
                                 gcod = dtl1.Field<string>("gcod"),
                                 gdesc = dtl1.Field<string>("gdesc"),
                                 code = dtl1.Field<string>("code")
                             }).ToList();
                dtE = ASITUtility03.ListToDataTable(query);
                if (dtE.Rows.Count >= 2)
                    dtE.Rows.Add("000000000000", "Choose Employee..", "");
                // if(dtE.Rows.Count>1)
                //dtE.Rows.Add("000000000000", "Choose Employee..", "");
            }

            this.ddlteam.DataTextField = "gdesc";
            this.ddlteam.DataValueField = "gcod";
            this.ddlteam.DataSource = dtE;
            this.ddlteam.DataBind();
            this.ddlteam.SelectedValue = "000000000000";

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            DataTable dt = (DataTable)ViewState["tblsal"];
            DataTable dt1 = (DataTable)ViewState["tblymon"];

            string salesteam = ddlteam.SelectedItem.Text.ToString();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassYearlyTarget>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalesTarget", lst, null, null);

            DateTime datefrm = Convert.ToDateTime("01-jan-" + ddlyear.SelectedValue.ToString());
            DateTime dateto = Convert.ToDateTime("31-dec-" + ddlyear.SelectedValue.ToString());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                Rpt1.SetParameters(new ReportParameter("txtmon" + i, datefrm.ToString("MMM yy")));

                datefrm = datefrm.AddMonths(1);

            }

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("year", "Year : " + ddlyear.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("salesteam", "Sales Team : " + salesteam));
            Rpt1.SetParameters(new ReportParameter("header", "Yearly Sales Budget"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lbtnYearbgd_Click(object sender, EventArgs e)
        {
            string teamcode = this.ddlteam.SelectedValue.ToString();

            if (teamcode == "000000000000")
            {
                this.ddlteam.Focus();
                return;
            }

            if (this.lbtnYearbgd.Text == "Ok")
            {

                this.lbtnYearbgd.Text = "New";
                this.ddlyear.Enabled = false;
                this.ddlteam.Enabled = false;

                this.ShowYearlyTarget();
                return;

            }

            this.lbtnYearbgd.Text = "Ok";
            this.ddlyear.Enabled = true;
            this.ddlteam.Enabled = true;
            this.gvySalbgd.DataSource = null;
            this.gvySalbgd.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }
        protected void ImgbtnFindteam_Click(object sender, EventArgs e)
        {
            this.GetTeamCode();

        }
        private void ShowYearlyTarget()
        {

            string comcod = this.GetComeCode();
            string Year = this.ddlyear.SelectedValue.ToString();
            string teamcode = this.ddlteam.SelectedValue.ToString();



            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "YEARLY_ACTIVITIES_TARGET_VS_ACHIVE", Year, teamcode, "", "", "", "", "", "", "");

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

        private void Data_Bind()
        {

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

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty2)", "")) ? 0.00
               : dt.Compute("Sum(qty2)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty3)", "")) ? 0.00
      : dt.Compute("Sum(qty3)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty4)", "")) ? 0.00
      : dt.Compute("Sum(qty4)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty5)", "")) ? 0.00
      : dt.Compute("Sum(qty5)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty6)", "")) ? 0.00
      : dt.Compute("Sum(qty6)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty7)", "")) ? 0.00
      : dt.Compute("Sum(qty7)", ""))).ToString("#,##0;(#,##0);  ");


            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty8)", "")) ? 0.00
      : dt.Compute("Sum(qty8)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty9)", "")) ? 0.00
      : dt.Compute("Sum(qty9)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty10)", "")) ? 0.00
      : dt.Compute("Sum(qty10)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty11)", "")) ? 0.00
      : dt.Compute("Sum(qty11)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFqty12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty12)", "")) ? 0.00
      : dt.Compute("Sum(qty12)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tqty)", "")) ? 0.00
         : dt.Compute("Sum(tqty)", ""))).ToString("#,##0;(#,##0);  ");



            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty1)", "")) ? 0.00
            : dt.Compute("Sum(aqty1)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty2)", "")) ? 0.00
               : dt.Compute("Sum(aqty2)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty3)", "")) ? 0.00
      : dt.Compute("Sum(aqty3)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty4)", "")) ? 0.00
      : dt.Compute("Sum(aqty4)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty5)", "")) ? 0.00
      : dt.Compute("Sum(aqty5)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty6)", "")) ? 0.00
      : dt.Compute("Sum(aqty6)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty7)", "")) ? 0.00
      : dt.Compute("Sum(aqty7)", ""))).ToString("#,##0;(#,##0);  ");


            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty8)", "")) ? 0.00
      : dt.Compute("Sum(aqty8)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty9)", "")) ? 0.00
      : dt.Compute("Sum(aqty9)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty10)", "")) ? 0.00
      : dt.Compute("Sum(aqty10)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty11)", "")) ? 0.00
      : dt.Compute("Sum(aqty11)", ""))).ToString("#,##0;(#,##0);  ");

            ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFaqty12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aqty12)", "")) ? 0.00
      : dt.Compute("Sum(aqty12)", ""))).ToString("#,##0;(#,##0);  ");





        }

        protected void gvySalbgd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell0 = new TableCell();
                cell0.Text = "Sl";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.RowSpan = 1;
                gvrow.Cells.Add(cell0);

                TableCell cell1 = new TableCell();
                cell1.Text = "Description";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.RowSpan =1;
                gvrow.Cells.Add(cell1);


                TableCell cell2 = new TableCell();
                cell2.Text = "Jan";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 2;
                gvrow.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = "Feb";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 2;
                gvrow.Cells.Add(cell3);


                 
                TableCell cell4 = new TableCell();
                cell4.Text = "Mar";
                cell4.HorizontalAlign = HorizontalAlign.Center;
                cell4.ColumnSpan = 2;
                gvrow.Cells.Add(cell4);


                TableCell cell5 = new TableCell();
                cell5.Text = "Apr";
                cell5.HorizontalAlign = HorizontalAlign.Center;
                cell5.ColumnSpan = 2;
                gvrow.Cells.Add(cell5);

                TableCell cell6 = new TableCell();
                cell6.Text = "May";
                cell6.HorizontalAlign = HorizontalAlign.Center;
                cell6.ColumnSpan = 2;
                gvrow.Cells.Add(cell6);

                TableCell cell7 = new TableCell();
                cell7.Text = "Jun";
                cell7.HorizontalAlign = HorizontalAlign.Center;
                cell7.ColumnSpan = 2;
                gvrow.Cells.Add(cell7);

                TableCell cell8 = new TableCell();
                cell8.Text = "Jul";
                cell8.HorizontalAlign = HorizontalAlign.Center;
                cell8.ColumnSpan = 2;
                gvrow.Cells.Add(cell8);

                TableCell cell9 = new TableCell();
                cell9.Text = "Aug";
                cell9.HorizontalAlign = HorizontalAlign.Center;
                cell9.ColumnSpan = 2;
                gvrow.Cells.Add(cell9);

                TableCell cell10 = new TableCell();
                cell10.Text = "Sep";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.ColumnSpan = 2;
                gvrow.Cells.Add(cell10);

                TableCell cell11 = new TableCell();
                cell11.Text = "Oct";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.ColumnSpan = 2;
                gvrow.Cells.Add(cell11);

                TableCell cell12 = new TableCell();
                cell12.Text = "Nov";
                cell12.HorizontalAlign = HorizontalAlign.Center;
                cell12.ColumnSpan = 2;
                gvrow.Cells.Add(cell12);

                TableCell cell13 = new TableCell();
                cell13.Text = "Dec";
                cell13.HorizontalAlign = HorizontalAlign.Center;
                cell13.ColumnSpan = 2;
                gvrow.Cells.Add(cell13);

                TableCell cell14 = new TableCell();
                cell14.Text = "Total Qty";
                cell14.HorizontalAlign = HorizontalAlign.Center;
                cell14.RowSpan = 1;
                gvrow.Cells.Add(cell14);


                gvySalbgd.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

        protected void gvySalbgd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvySalbgd.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }


        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            try
            {

                string comcod = this.GetComeCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string Year = this.ddlyear.SelectedValue.ToString();
                string teamcode = this.ddlteam.SelectedValue.ToString();
                bool result = true;
                result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "DELATESALCOLLTARINF", Year, Year, teamcode, "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = SalesData.ErrorObject["Msg"].ToString();
                    return;
                }



                //Details

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    int j = 1;

                    string pactcode = dt1.Rows[i]["pactcode"].ToString();

                    while (j <= 12)
                    {
                        double qty = Convert.ToDouble(dt1.Rows[i]["qty" + j.ToString()].ToString());

                        string monthid = this.ddlyear.SelectedValue.ToString() + ASTUtility.Right("0" + j.ToString(), 2);
                        if (qty != 0)
                        {
                            result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "INSERTEMPACTIVITIESTARGET", "", monthid, teamcode, pactcode, qty.ToString(), "", "", "", "", "", "", "", "", "", "");

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

        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }


        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            int rowindex;
            for (int i = 0; i < this.gvySalbgd.Rows.Count; i++)
            {
                rowindex = this.gvySalbgd.PageSize * this.gvySalbgd.PageIndex + i;

                tbl1.Rows[rowindex]["qty1"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty2"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty2")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty3"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty3")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty4"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty4")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty5"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty5")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty6"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty6")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty7"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty7")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty8"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty8")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty9"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty9")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty10"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty10")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty11"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty11")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty12"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty12")).Text.Trim()).ToString();

                //    Convert.ToDouble(tbl1.Rows[rowindex]["amt1"]) + tbl1.Rows[rowindex]["amt2"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"];
            }


            ViewState["tblsal"] = tbl1;


        }

      
    }
}