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
    public partial class YearlyActivitiesTarget : System.Web.UI.Page
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
                string qtype = this.Request.QueryString["Type"].ToString();


                ((Label)this.Master.FindControl("lblTitle")).Text = qtype=="CRM"? "CRM Yearly Activities Target ": "Land CRM Yearly Activities Target ";

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


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


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
            string qtype = this.Request.QueryString["Type"].ToString();
            

            if (qtype=="CRM")
            {
                DataSet ds2 = SalesData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");

                ViewState["tblsubddl"] = ds2.Tables[0];
                ds2.Dispose();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString();
                DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
                ViewState["tblempsup"] = ds1.Tables[0];
                ds1.Dispose();

            }
            else
            {

                DataSet ds2 = SalesData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "LANDREFINFODDL", "", "", "", "", "", "", "", "", "");

                ViewState["tblsubddl"] = ds2.Tables[0];
                ds2.Dispose();


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString();
                DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETGENEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
                ViewState["tblempsup"] = ds1.Tables[0];
                ds1.Dispose();
            }


            

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
            string qtype = this.Request.QueryString["Type"].ToString();

            string comcod = this.GetComeCode();
            string Year = this.ddlyear.SelectedValue.ToString();
            string teamcode = this.ddlteam.SelectedValue.ToString();
            
            string ctype = "";
            if(qtype=="CRM")
            {
                ctype = "YEARLY_ACTIVITIES_TARGETCRM";
            }
            else
            {
                ctype = "YEARLY_ACTIVITIES_TARGET";

            }

            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", ctype, Year, teamcode, qtype, "", "", "", "", "", "");

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

        }

        protected void gvySalbgd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //    TableCell cell0 = new TableCell();
            //    cell0.Text = "";
            //    cell0.HorizontalAlign = HorizontalAlign.Center;
            //    cell0.ColumnSpan = 2;
            //    gvrow.Cells.Add(cell0);


            //    DataTable dtymon = (DataTable)ViewState["tblymon"];
            //    int j = 2;
            //    //for (int i = 0; i < dtymon.Rows.Count; i++)
            //    //{

            //    //    TableCell cell = new TableCell();
            //    //    cell.Text = dtymon.Rows[i]["yearmon"].ToString();
            //    //    cell.HorizontalAlign = HorizontalAlign.Center;
            //    //    cell.ColumnSpan = 2;
            //    //    cell.Font.Bold = true;
            //    //    gvrow.Cells.Add(cell);

            //    ////    if (j == 26)
            //    //        break;


            //    //}


            //    TableCell celll = new TableCell();
            //    celll.Text = "";
            //    celll.HorizontalAlign = HorizontalAlign.Center;
            //    celll.ColumnSpan = 2;
            //    gvrow.Cells.Add(celll);



            //    //this.gvHourlyProd.Columns[5 + i].HeaderText = dt2.Rows[i]["gdesc"].ToString();
            //    //  i++;


            //    gvySalbgd.Controls[0].Controls.AddAt(0, gvrow);
            //}
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
                string qtype = this.Request.QueryString["type"].ToString();

                string comcod = this.GetComeCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string Year = this.ddlyear.SelectedValue.ToString();
                string teamcode = this.ddlteam.SelectedValue.ToString();
                bool result = true;
                result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "DELATEMPACTIVITIESTARBYEMP", Year, Year, teamcode, qtype, "", "", "", "", "", "", "", "", "", "", "");
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
                            result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "INSERTEMPACTIVITIESTARGET", "", monthid, teamcode, pactcode, qty.ToString(), qtype, "", "", "", "", "", "", "", "", "");

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

        protected void lnkbtnCopyBtn_Click(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
           
            
            int rowindex;
            for (int i = 0; i < this.gvySalbgd.Rows.Count; i++)
            {
                rowindex = this.gvySalbgd.PageSize * this.gvySalbgd.PageIndex + i;

                tbl1.Rows[rowindex]["qty1"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty2"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty3"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty4"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty5"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty6"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty7"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty8"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty9"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty10"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty11"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();

                tbl1.Rows[rowindex]["qty12"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();
                //int tqty  Convert.ToInt32(tbl1.Rows[rowindex]["qty1"]) + Convert.ToDouble(tbl1.Rows[rowindex]["qty"]) + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"] + tbl1.Rows[rowindex]["amt1"];
                double tqty1 = Convert.ToDouble(tbl1.Rows[rowindex]["qty1"]);
                double tqty2 = Convert.ToDouble(tbl1.Rows[rowindex]["qty2"]);
                double tqty3 = Convert.ToDouble(tbl1.Rows[rowindex]["qty3"]);
                double tqty4 = Convert.ToDouble(tbl1.Rows[rowindex]["qty4"]);
                double tqty5 = Convert.ToDouble(tbl1.Rows[rowindex]["qty5"]);
                double tqty6 = Convert.ToDouble(tbl1.Rows[rowindex]["qty6"]);
                double tqty7 = Convert.ToDouble(tbl1.Rows[rowindex]["qty7"]);
                double tqty8 = Convert.ToDouble(tbl1.Rows[rowindex]["qty8"]);
                double tqty9 = Convert.ToDouble(tbl1.Rows[rowindex]["qty9"]);
                double tqty10 = Convert.ToDouble(tbl1.Rows[rowindex]["qty10"]);
                double tqty11 = Convert.ToDouble(tbl1.Rows[rowindex]["qty11"]);
                double tqty12 = Convert.ToDouble(tbl1.Rows[rowindex]["qty12"]);
                tbl1.Rows[rowindex]["tqty"] = tqty1 + tqty2 + tqty3 + tqty4 + tqty5 + tqty6 + tqty7 + tqty8 + tqty9 + tqty10 + tqty11 + tqty12;
            }
 
            ViewState["tblsal"] = tbl1;
            this.Data_Bind();

             
        }
    }
}