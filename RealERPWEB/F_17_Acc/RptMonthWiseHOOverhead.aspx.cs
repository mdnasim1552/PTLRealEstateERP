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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptMonthWiseHOOverhead : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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


               // ((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Head Office Overhead";

                this.txtFDate.Text = System.DateTime.Today.AddMonths(-12).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                this.GetProjectName();
            }

        }



        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    // Create an event handler for the master page's contentCallEvent event
        //    ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        //    //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        //}


        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS04", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();



        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            Session.Remove("tbloverhead");
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtFDate.Text.Trim()));
            if (mon > 12)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                return;
            }

            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "55%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string Details = (this.rbtnList1.SelectedIndex == 0) ? "Details" : "";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS04", "RPTOVERHEADDETAILS", fromdate, todate, ProjectCode, Details, "", "", "", "", "");
            if (ds1 == null)
            {
                this.prjcost.DataSource = null;
                this.prjcost.DataBind();
                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);

            Session["tbloverhead"] = dt;
            this.Data_Bind();



        }

        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;

            string actcode = dt1.Rows[0]["actcode"].ToString();
            string mrescode = dt1.Rows[0]["mrescode"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode && dt1.Rows[j]["mrescode"].ToString() == mrescode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    mrescode = dt1.Rows[j]["mrescode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["mresdesc"] = "";
                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {
                        dt1.Rows[j]["actdesc"] = "";
                    }

                    if (dt1.Rows[j]["mrescode"].ToString() == mrescode)
                    {
                        dt1.Rows[j]["mresdesc"] = "";

                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    mrescode = dt1.Rows[j]["mrescode"].ToString();
                }
            }


            return dt1;
        }
        private void Data_Bind()
        {
            //this.prjcost.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.prjcost.DataSource = (DataTable)Session["tbloverhead"];
            this.prjcost.DataBind();

            this.FooterCalculation();


        }



        private void FooterCalculation()
        {


            DataTable dt, dt1; DataView dv;
            dt1 = ((DataTable)Session["tbloverhead"]).Copy();
            dv = dt1.Copy().DefaultView;

            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            int i, j;
            DateTime datefrm, dateto;
            dv.RowFilter = ("rptcode not like '%AAAA%'");
            dt = dv.ToTable();
            amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
            amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
            amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
            amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
            amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
            amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
            amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
            amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
            amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
            amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
            amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
            amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

            this.prjcost.Columns[6].Visible = (amt1 != 0);
            this.prjcost.Columns[7].Visible = (amt2 != 0);
            this.prjcost.Columns[8].Visible = (amt3 != 0);
            this.prjcost.Columns[9].Visible = (amt4 != 0);
            this.prjcost.Columns[10].Visible = (amt5 != 0);
            this.prjcost.Columns[11].Visible = (amt6 != 0);
            this.prjcost.Columns[12].Visible = (amt7 != 0);
            this.prjcost.Columns[13].Visible = (amt8 != 0);
            this.prjcost.Columns[14].Visible = (amt9 != 0);
            this.prjcost.Columns[15].Visible = (amt10 != 0);
            this.prjcost.Columns[16].Visible = (amt11 != 0);
            this.prjcost.Columns[17].Visible = (amt12 != 0);


            datefrm = Convert.ToDateTime(this.txtFDate.Text.Trim());
            dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (i = 6; i < 18; i++)
            {
                if (datefrm > dateto)

                    break;

                this.prjcost.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            this.prjcost.DataSource = dt1;
            this.prjcost.DataBind();
            Session["Report1"] = prjcost;
            ((HyperLink)this.prjcost.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            //((Label)this.prjcost.FooterRow.FindControl("lgvFApprQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reqqty)", "")) ? 0.00 :
            //     dt.Compute("sum(reqqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.prjcost.FooterRow.FindControl("lgvFprocess")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(procesqty)", "")) ? 0.00 :
            //    dt.Compute("sum(procesqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.prjcost.FooterRow.FindControl("lgvFporder")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(orderqty)", "")) ? 0.00 :
            //    dt.Compute("sum(orderqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.prjcost.FooterRow.FindControl("lgvFmrr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrqty)", "")) ? 0.00 :
            //       dt.Compute("sum(mrqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.prjcost.FooterRow.FindControl("lgvFbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billqty)", "")) ? 0.00 :
            //    dt.Compute("sum(billqty)", ""))).ToString("#,##0;(#,##0); ");




        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }




        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }







        protected void prjcost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            // Label code = (Label)e.Row.FindControl("lblgvrptcode");

            Label lgvResDescd = (Label)e.Row.FindControl("lgvResDescd");
            Label lgvamt1d = (Label)e.Row.FindControl("lgvamt1d");
            Label lgvamt2d = (Label)e.Row.FindControl("lgvamt2d");
            Label lgvamt3d = (Label)e.Row.FindControl("lgvamt3d");
            Label lgvamt4d = (Label)e.Row.FindControl("lgvamt4d");
            Label lgvamt5d = (Label)e.Row.FindControl("lgvamt5d");
            Label lgvamt6d = (Label)e.Row.FindControl("lgvamt6d");
            Label lgvamt7d = (Label)e.Row.FindControl("lgvamt7d");
            Label lgvamt8d = (Label)e.Row.FindControl("lgvamt8d");
            Label lgvamt9d = (Label)e.Row.FindControl("lgvamt9d");
            Label lgvamt10d = (Label)e.Row.FindControl("lgvamt10d");
            Label lgvamt11d = (Label)e.Row.FindControl("lgvamt11d");
            Label lgvamt12d = (Label)e.Row.FindControl("lgvamt12d");
            Label lgvamttotal = (Label)e.Row.FindControl("lgvamttotal");



            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcode")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (ASTUtility.Right(code, 3) == "AAA")
            {
                lgvResDescd.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt1d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt2d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt3d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt4d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt5d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt6d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt7d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt8d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt9d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt10d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt11d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamt12d.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvamttotal.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";


                //lgvNagad.Style.Add("text-align", "left");
                lgvResDescd.Style.Add("text-align", "right");

            }

            if (ASTUtility.Right(code, 3) == "BBB")
            {
                lgvResDescd.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt1d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt2d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt3d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt4d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt5d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt6d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt7d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt8d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt9d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt10d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt11d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamt12d.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvamttotal.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";


                //lgvNagad.Style.Add("text-align", "left");
                lgvResDescd.Style.Add("text-align", "right");



            }

            if (ASTUtility.Right(code, 3) == "CCC")
            {

                lgvResDescd.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt1d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt2d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt3d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt4d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt5d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt6d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt7d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt8d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt9d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt10d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt11d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamt12d.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";
                lgvamttotal.Attributes["style"] = "font-weight:bold; color:green;font-family: Century Gothic, sans-serif;";


                //lgvNagad.Style.Add("text-align", "left");
                lgvResDescd.Style.Add("text-align", "right");

            }





        }
    }
}