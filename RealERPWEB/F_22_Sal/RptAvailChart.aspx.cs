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
using RealERPRDLC;

namespace RealERPWEB.F_22_Sal
{
    public partial class RptAvailChart : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = type == "Details" ? "Availability Chart 1" : "Availability Chart 2";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void Visibility()
        {
            string type = this.Request.QueryString["Type"].ToString();
            string comcod = this.GetComCode();
            switch (type)
            {
                case "Details":
                    this.gvAailChart.Columns[10].Visible = false;
                    this.gvAailChart.Columns[11].Visible = false;
                    this.gvAailChart.Columns[12].Visible = false;
                    this.gvAailChart.Columns[13].Visible = false;
                    if (comcod == "3366" || comcod == "3101")
                    {
                        this.gvAailChart.Columns[9].Visible = false;
                    }
                    break;
                default:
                    break;
            }

        }

        private void CompVisibility()
        {
            string type = ASTUtility.Left(this.GetComCode(), 1);
            switch (type)
            {
                case "2":
                    this.gvAailChart.Columns[3].Visible = false;
                    break;
                default:
                    this.gvAailChart.Columns[2].Visible = false;
                    break;
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void ProjectName()
        {
            string comcod = this.GetComCode();
            string Filter1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ProjectName();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            this.ShowReport();
            rbtnList_SelectedIndexChanged(null, null);

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string comcod = this.GetComCode();
            switch (comcod)
            {
                case "3101":
                case "3370":
                    this.PrintAvailityChart();
                    break;
                default:
                    break;
            }
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string prjname = this.ddlProjectName.SelectedItem.Text.Trim();
            //ReportDocument rpcp = new ReportDocument();
            //if (this.Request.QueryString["Type"].ToString() == "Details")
            //{
            //    if (ASTUtility.Left(GetComCode(), 1) == "2")
            //    {
            //        rpcp = new RealERPRPT.R_22_Sal.rptAvailChartLand();

            //        TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //        CompName.Text = comname;
            //    }


            //    else if (ASTUtility.Left(GetComCode(), 4) == "3335" || ASTUtility.Left(GetComCode(), 4)=="3101")
            //    {
            //        rpcp = new RealERPRPT.R_22_Sal.rptAvailChartEdision();
            //    }
            //     else 
            //    {
            //        rpcp = new RealERPRPT.R_22_Sal.rptAvailChart();
            //    }


            //}
            //else
            //{
            //    rpcp = new RealERPRPT.R_22_Sal.rptAvailChart2();

            //}
            //DataTable dt = (DataTable)Session["tblFtCal"];
            //if (dt.Rows.Count == 0)
            //    return;


            ////TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            ////txtPrjName.Text = prjname;

            ////TextObject txtuqty = rpcp.ReportDefinition.ReportObjects["txtuqty"] as TextObject;
            ////txtuqty.Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uqty)", "")) ?
            ////                    0 : dt.Compute("sum(uqty)", ""))).ToString("#,##0;(#,##0); "); ;

            ////TextObject txtUsize = rpcp.ReportDefinition.ReportObjects["txtUsize"] as TextObject;
            ////txtUsize.Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usize)", "")) ?
            ////                    0 : dt.Compute("sum(usize)", ""))).ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //   // string eventtype = this.lblHeader.Text;
            //   // string eventdesc = "Print Report";
            //    //string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
            //   // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, "");
            //}

            //rpcp.SetDataSource((DataTable)Session["tblAvChart"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintAvailityChart()
        {
            try
            {
                string comcod = this.GetComCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comsnam = hst["comsnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string session = hst["session"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
                DataTable dt = (DataTable)Session["tblAvChart"];

                var list = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptAvailability>();

                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptAvailbilityPrint", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compname", comnam));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));

                Rpt1.SetParameters(new ReportParameter("title", "Availability Chart"));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
            catch (Exception Exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Exp.Message.ToString() + "');", true);

            }

        }
        private void ShowReport()
        {
            Session.Remove("tblAvChart");
            Session.Remove("tblflorlist");
            Session.Remove("tblflorUnit");
            string comcod = this.GetComCode();
            string CallType = (this.Request.QueryString["Type"].ToString() == "Details") ? "AVAILCHART" : "AVAILCHART02";
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "18" : this.ddlProjectName.SelectedValue.ToString()) + "%"; 
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null || ds2.Tables[0].Rows.Count == 0 || ds2.Tables[1].Rows.Count == 0)
            {
                this.gvAailChart.DataSource = null;
                this.gvAailChart.DataBind();
                return;
            }

            DataTable dt = this.HiddenSameData(ds2.Tables[0]);
            Session["tblAvChart"] = dt;
            this.Data_Bind();
            Session["tblFtCal"] = (DataTable)ds2.Tables[1];
            Session["tblflorlist"] = (DataTable)ds2.Tables[2];
            Session["tblflorUnit"] = (DataTable)ds2.Tables[3];

            // this.FooterCalculation();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            string type = ASTUtility.Left(this.GetComCode(), 1);
            //string type = this.Request.QueryString["Type"].ToString();

            string usircode1 = dt1.Rows[0]["usircode1"].ToString();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            switch (type)
            {
                case "2":

                    string usircode3 = dt1.Rows[0]["usircode3"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["usircode3"].ToString() == usircode3)
                        {
                            usircode3 = dt1.Rows[j]["usircode3"].ToString();
                            dt1.Rows[j]["flrdesc3"] = "";
                        }
                        if (dt1.Rows[j]["usircode1"].ToString() == usircode1)
                        {
                            usircode1 = dt1.Rows[j]["usircode1"].ToString();
                            dt1.Rows[j]["flrdesc"] = "";
                        }

                        usircode3 = dt1.Rows[j]["usircode3"].ToString();
                        usircode1 = dt1.Rows[j]["usircode1"].ToString();


                    }
                    break;

                default:

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode1"].ToString() == usircode1)
                        {

                            dt1.Rows[j]["flrdesc"] = "";
                            dt1.Rows[j]["pactdesc"] = "";
                        }


                        else
                        {

                            if (dt1.Rows[j]["usircode1"].ToString() == usircode1)
                            {
                                dt1.Rows[j]["flrdesc"] = "";
                            }

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";

                            }



                        }


                        pactcode = dt1.Rows[j]["pactcode"].ToString();
                        usircode1 = dt1.Rows[j]["usircode1"].ToString();
                    }
                    break;
            }
            return dt1;

        }

        protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string datatype = this.rbtnList.SelectedValue.ToString();
            if (datatype=="0")
            {
                this.divGVData.Visible = true;
                this.divgvChart.Visible = false;
            }
            else
            {
                this.divGVData.Visible = false;
                this.divgvChart.Visible = true;
                GetAvailabilityChart();
            }
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblAvChart"];
            this.gvAailChart.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvAailChart.DataSource = dt;
            this.gvAailChart.DataBind();
            this.Visibility();
            this.CompVisibility();
        }

        private void GetAvailabilityChart()
        {
            DataTable dt = (DataTable)Session["tblflorlist"];
            DataTable dtunit = (DataTable)Session["tblflorUnit"];
            string str = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string usircode = dt.Rows[i]["usircode1"].ToString();
                string pactcode = dt.Rows[i]["pactcode"].ToString();

                DataTable dtunitfilter = new DataTable();
                DataView dv = dtunit.DefaultView;
                dv.RowFilter = ("usircode1='" + usircode + "' and pactcode='"+ pactcode + "'");   
                dtunitfilter = dv.ToTable();


                string strunit = string.Empty;
                for (int j = 0; j < dtunitfilter.Rows.Count; j++)
                {                    
                    strunit += "<a hrf='#' class='" + dtunitfilter.Rows[i]["cssStype"].ToString() + " btn text-white m-1'>" + dtunitfilter.Rows[i]["udesc"].ToString() + "</a>";
                }
                 
                    str += "<div class='row mt-1'>" +
                    "" +
                    "<div class='col-md-2'><h5 class='text-right'>" + dt.Rows[i]["flrdesc"].ToString() + "</h5></div>" +
                    "" +
                    "<div class='col-md-10'>" + strunit +
                    "</div>" +
                    "" +
                    "</div>";
            }
            this.divUnitGraph.InnerHtml = str;
        }

        
        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblFtCal"];
            if (dt.Rows.Count == 0)
                return;
            //lgvFQty, lgvFSize1, lgvFLoqty, lgvFSize2

            ((Label)this.gvAailChart.FooterRow.FindControl("lgvFQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uqty)", "")) ?
                                0 : dt.Compute("sum(uqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAailChart.FooterRow.FindControl("lgvFSize1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usize)", "")) ?
                                0 : dt.Compute("sum(usize)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAailChart.FooterRow.FindControl("lgvFLoqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(luqty)", "")) ?
                                0 : dt.Compute("sum(luqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAailChart.FooterRow.FindControl("lgvFSize2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(losize)", "")) ?
                                0 : dt.Compute("sum(losize)", ""))).ToString("#,##0;(#,##0); ");

            



            //DataTable dt = (DataTable)Session["tblAvChart"];
            //if (dt.Rows.Count == 0)
            //    return;
            //((Label)this.gvAailChart.FooterRow.FindControl("lgvFSize1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usize)", "")) ?
            //                    0 : dt.Compute("sum(usize)", ""))).ToString("#,##0;(#,##0); ");

            //((Label)this.gvAailChart.FooterRow.FindControl("lgvFSize2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ownsize)", "")) ?
            //                    0 : dt.Compute("sum(ownsize)", ""))).ToString("#,##0;(#,##0); ");

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvAailChart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label unitdesc = (Label)e.Row.FindControl("lgvUnitdesc");
                Label custname = (Label)e.Row.FindControl("lgvCustdesc");
                Label lgQty = (Label)e.Row.FindControl("lgQty");
                Label sizeSft = (Label)e.Row.FindControl("lgSize1");
                Label percent = (Label)e.Row.FindControl("lgper");
                Label pamt = (Label)e.Row.FindControl("lgpamt");
                Label utility = (Label)e.Row.FindControl("lgputility");
                Label Cooperative = (Label)e.Row.FindControl("lgpCooperative");
                Label famt = (Label)e.Row.FindControl("lgpfamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode2")).ToString().Trim();


                if (code == "")
                {
                    return;
                }
                else if (ASTUtility.Right(code, 1) == "A")
                {
                    unitdesc.ForeColor = System.Drawing.Color.Red;
                    custname.ForeColor = System.Drawing.Color.Red;
                    sizeSft.ForeColor = System.Drawing.Color.Red;
                    percent.ForeColor = System.Drawing.Color.Red;

                }

                else if (ASTUtility.Right(code, 1) == "C")
                {
                    unitdesc.ForeColor = System.Drawing.Color.Blue;
                    custname.ForeColor = System.Drawing.Color.Blue;
                    sizeSft.ForeColor = System.Drawing.Color.Blue;
                    percent.ForeColor = System.Drawing.Color.Blue;

                }
                else if (ASTUtility.Right(code, 1) == "X" || ASTUtility.Right(code, 1) == "Y" || ASTUtility.Right(code, 1) == "Z")
                {
                    unitdesc.Font.Bold = true;
                    lgQty.Font.Bold = true;
                    sizeSft.Font.Bold = true;
                    unitdesc.Style.Add("text-align", "right");
                    percent.Font.Bold = true;
                    pamt.Font.Bold = true;
                    utility.Font.Bold = true;
                    Cooperative.Font.Bold = true;
                    famt.Font.Bold = true;
                }


            }

        }

    }
}