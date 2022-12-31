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
namespace RealERPWEB.F_29_Fxt
{
    public partial class RptFixAssetStatus02 : System.Web.UI.Page
    {
        ProcessAccess FxtData = new ProcessAccess();
        Common ObjCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "RecAndRefund") ? "Equipment Use Status  (Employee Wise)" : (type == "RecIssueARefund") ? "Equipment Use Status (Resource Wise)" : "Equipment Use Status Summary";

                this.GetEmployeeorResList();
                this.SelectView();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "RecAndRefund":

                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "RecIssueARefund":
                    this.lblEmployee.Text = "Material: ";
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "RecIssueARefSum":
                    this.lblEmployee.Text = "Material: ";
                    this.MultiView1.ActiveViewIndex = 2;
                    break;








            }
        }

        private void GetEmployeeorResList()
        {


            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = ObjCommon.GetCompCode();
            string mSrchEmployee = this.txtSrchEmployeeorRes.Text.Trim() + "%";
            string CallType = (type == "RecAndRefund") ? "GETEMPLOYEE" : "GETFASSETLIST";
            DataSet ds1 = FxtData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", CallType, mSrchEmployee, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlEmporResList.Items.Clear();
                return;
            }


            this.ddlEmporResList.DataTextField = "sirdesc";
            this.ddlEmporResList.DataValueField = "sircode";
            this.ddlEmporResList.DataSource = ds1.Tables[0];
            this.ddlEmporResList.DataBind();
            ds1.Dispose();


        }

        protected void ImgbtnFindEmp_Click(object sender, EventArgs e)
        {
            this.GetEmployeeorResList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "RecAndRefund":
                    this.ShowRecAndRefund();
                    break;

                case "RecIssueARefund":
                    this.ShowRecIssueAndRefund();
                    break;

                case "RecIssueARefSum":
                    this.ShowRecIssuedAndRefSum();
                    break;

            }

        }
        private void ShowRecAndRefund()
        {

            Session.Remove("tblfxtasset");

            string comcod = ObjCommon.GetCompCode();
            string empcode = this.ddlEmporResList.SelectedValue.ToString();
            DataSet ds1 = FxtData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTEQUIPUSESSTATUS", empcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvrecandref.DataSource = null;
                this.gvrecandref.DataBind();
                return;
            }



            Session["tblfxtasset"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();


        }
        private void ShowRecIssueAndRefund()
        {
            Session.Remove("tblfxtasset");

            string comcod = ObjCommon.GetCompCode();
            string Resource = (this.ddlEmporResList.SelectedValue.ToString() == "000000000000" ? "22" : this.ddlEmporResList.SelectedValue.ToString()) + "%";

            DataSet ds1 = FxtData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTEQUIPSTRESWISE", Resource, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvrecissaref.DataSource = null;
                this.gvrecissaref.DataBind();
                return;
            }



            Session["tblfxtasset"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();
        }
        private void ShowRecIssuedAndRefSum()
        {
            Session.Remove("tblfxtasset");

            string comcod = ObjCommon.GetCompCode();
            string Resource = (this.ddlEmporResList.SelectedValue.ToString() == "000000000000" ? "22" : this.ddlEmporResList.SelectedValue.ToString()) + "%";

            DataSet ds1 = FxtData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTEQUIPUSESTSUMMARY", Resource, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvrecissarefsum.DataSource = null;
                this.gvrecissarefsum.DataBind();
                return;
            }



            Session["tblfxtasset"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();



        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string rescode, recpcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "RecIssueARefund":
                    rescode = dt1.Rows[0]["rescode"].ToString();
                    recpcode = dt1.Rows[0]["recpcode"].ToString();

                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rescode"].ToString() == rescode && dt1.Rows[j]["recpcode"].ToString() == recpcode)
                        {
                            dt1.Rows[j]["recpdesc"] = "";
                            dt1.Rows[j]["resdesc"] = "";

                        }

                        else
                        {

                            if (dt1.Rows[j]["rescode"].ToString() == rescode)
                                dt1.Rows[j]["resdesc"] = "";



                            if (dt1.Rows[j]["recpcode"].ToString() == recpcode)
                                dt1.Rows[j]["recpdesc"] = "";




                        }
                        recpcode = dt1.Rows[j]["recpcode"].ToString();
                        rescode = dt1.Rows[j]["rescode"].ToString();




                    }

                    break;



            }


            return dt1;

        }

        private void Data_Bind()
        {


            DataTable dt = (DataTable)Session["tblfxtasset"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "RecAndRefund":
                    this.gvrecandref.DataSource = dt;
                    this.gvrecandref.DataBind();
                    break;

                case "RecIssueARefund":
                    this.gvrecissaref.DataSource = dt;
                    this.gvrecissaref.DataBind();
                    break;

                case "RecIssueARefSum":
                    this.gvrecissarefsum.DataSource = dt;
                    this.gvrecissarefsum.DataBind();
                    break;



            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfxtasset"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "RecIssueARefund":
                    this.PrintRecIssueARefund();
                    break;

                case "RecAndRefund":
                    this.PrintRecAndRefund();
                    break;

                case "RecIssueARefSum":
                    this.PrintRecIssueARefSum();
                    break;

            }


        }
        private void PrintRecIssueARefund()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblfxtasset"];

            var list = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EquipmentUseStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptEquipStResWise", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Equipment Status (Resource Wise)"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintRecAndRefund()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblfxtasset"];

            var list = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EquipmentUseStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptEquipStEmpWise", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Equipment Status (Employee Wise)"));
            Rpt1.SetParameters(new ReportParameter("txtEmployee", "Employee: " + this.ddlEmporResList.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintRecIssueARefSum()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblfxtasset"];

            var list = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EquipmentUseStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptEquipStSummary", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Equipment  Use Status - Summary"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvrecandref_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "Issued";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 3;

                TableCell cell03 = new TableCell();
                cell03.Text = "Refund";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 3;



                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);

                gvrecandref.Controls[0].Controls.AddAt(0, gvrow);
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label RecDesc = (Label)e.Row.FindControl("lblRecDesc");
                Label recqty = (Label)e.Row.FindControl("lblgvrecqty");
                Label refDesc = (Label)e.Row.FindControl("lblgvrefDesc");
                Label refqty = (Label)e.Row.FindControl("lblgvrefqty");

                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refundcode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 4) == "AAAA")
                {

                    RecDesc.Font.Bold = true;
                    recqty.Font.Bold = true;
                    RecDesc.Style.Add("text-align", "right");
                }
                if (ASTUtility.Right(code2, 4) == "AAAA")
                {
                    refDesc.Font.Bold = true;
                    refqty.Font.Bold = true;
                    refDesc.Style.Add("text-align", "right");
                }
            }
        }


        protected void gvrecissaref_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;




                TableCell cell03 = new TableCell();
                cell03.Text = "Total Received";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 3;

                TableCell cell04 = new TableCell();
                cell04.Text = "Issued";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;



                TableCell cell05 = new TableCell();
                cell05.Text = "Refund";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 3;

                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;



                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);

                this.gvrecissaref.Controls[0].Controls.AddAt(0, gvrow);
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label RecDesc = (Label)e.Row.FindControl("lblRecDescres");
                Label recqty = (Label)e.Row.FindControl("lblgvrecqtyres");

                Label issueDesc = (Label)e.Row.FindControl("lblgvissueDescres");
                Label issueqty = (Label)e.Row.FindControl("lblgvissueqtyres");

                Label refDesc = (Label)e.Row.FindControl("lblgvrefDescres");
                Label refqty = (Label)e.Row.FindControl("lblgvrefqtyres");
                Label balqty = (Label)e.Row.FindControl("lblgvbalqtyres");


                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issuecode")).ToString();
                string code3 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refundcode")).ToString();

                if (code1 == "" && code2 == "" && code3 == "")
                {
                    return;
                }

                balqty.Font.Bold = true;


                if (ASTUtility.Right(code1, 4) == "AAAA")
                {

                    RecDesc.Font.Bold = true;
                    recqty.Font.Bold = true;
                    RecDesc.Style.Add("text-align", "right");
                }

                if (ASTUtility.Right(code2, 4) == "AAAA")
                {
                    issueDesc.Font.Bold = true;
                    issueqty.Font.Bold = true;
                    issueDesc.Style.Add("text-align", "right");
                }


                if (ASTUtility.Right(code3, 4) == "AAAA")
                {
                    refDesc.Font.Bold = true;
                    refqty.Font.Bold = true;
                    refDesc.Style.Add("text-align", "right");
                }

            }

        }
    }
}