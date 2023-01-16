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
namespace RealERPWEB.F_14_Pro
{
    public partial class BillTracking : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();
        AutoCompleted Data = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    // Response.Redirect("../AcceessError.aspx");
                    //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                    ((Label)this.Master.FindControl("lblTitle")).Text = "BILL TRACKING LIST";



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.GetDynamcifield();
                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetDynamcifield()
        {
            Session.Remove("tbldyfield");
            string comcod = this.GetComeCode();
            DataSet ds4 = MISData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "BILLDYNAMICFIELD", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.cblBillTracking.Items.Clear();
                return;
            }

            this.cblBillTracking.DataTextField = "descrip";
            this.cblBillTracking.DataValueField = "code";
            this.cblBillTracking.DataSource = ds4.Tables[0];
            this.cblBillTracking.DataBind();
            Session["tbldyfield"] = ds4.Tables[0];


        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string txtBillDate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";
            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtBillDate = ((this.ddlFieldList1.SelectedValue.ToString() == "billdate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "vounum") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtBillDate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + this.txtSearch1.Text.Trim() + txtBillDate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtBillDate = ((this.ddlFieldList1.SelectedValue.ToString() == "billdate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "vounum") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtBillDate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + this.txtSearch2.Text.Trim() + txtBillDate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtBillDate = ((this.ddlFieldList1.SelectedValue.ToString() == "billdate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "vounum") ? "'" : "");
                txtlike = (this.ddlSrch3.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + " " + this.ddlOperator2.SelectedValue.ToString() + " " + this.ddlFieldList3.SelectedValue.ToString() + " " + this.ddlSrch3.SelectedValue.ToString() + " " + txtBillDate + ((this.ddlSrch3.SelectedValue == "like") ? "'" : "") + this.txtSearch3.Text.Trim() + txtBillDate + txtlike;

            }

            if (this.ddlOrder1.SelectedValue != "00000")
            {
                orderinfo = orderinfo + this.ddlOrder1.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder2.SelectedValue != "00000")
            {
                orderinfo = orderinfo + this.ddlOrder2.SelectedValue.ToString() + " " + this.ddlOrderad2.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder3.SelectedValue != "00000")
            {
                orderinfo = orderinfo + this.ddlOrder3.SelectedValue.ToString() + " " + this.ddlOrderad3.SelectedValue.ToString() + ",";

            }

            SearchInfo = SearchInfo.Trim();
            if (orderinfo.Length > 0)
                orderinfo = ASTUtility.Left(orderinfo.Trim(), orderinfo.Trim().Length - 1);
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "BILLTRACKING", fdate, tdate, SearchInfo, orderinfo, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBillTracking.DataSource = null;
                this.gvBillTracking.DataBind();
                return;
            }
            Session["tbldata"] = HiddenSameData(ds1.Tables[0]);
            //Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Supplier Bill Tracking";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbldata"];

            int i;


            for (i = 0; i < this.gvBillTracking.Columns.Count; i++)
                this.gvBillTracking.Columns[i].Visible = false;


            for (i = 0; i < this.cblBillTracking.Items.Count; i++)
            {
                if (this.cblBillTracking.Items[i].Selected)
                {
                    this.gvBillTracking.Columns[i].Visible = true;
                    this.gvBillTracking.Columns[i].HeaderText = this.cblBillTracking.Items[i].Text.Trim();
                }

            }
            this.gvBillTracking.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvBillTracking.DataSource = dt;
            this.gvBillTracking.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbldata"];
            DataTable dt1 = dt.Copy();
            if (dt1.Rows.Count == 0)
                return;
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("matdesc like 'Total%'");
            dt1 = dv.ToTable();

            ((Label)this.gvBillTracking.FooterRow.FindControl("lgvFamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
                                    0 : dt1.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string billno = dt1.Rows[0]["billno"].ToString();

            //string matdesc = dt1.Rows[0]["matdesc"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["billno"].ToString() == billno)
                {

                    billno = dt1.Rows[j]["billno"].ToString();
                    dt1.Rows[j]["billno"] = "";
                    dt1.Rows[j]["billref"] = "";
                    dt1.Rows[j]["vounum"] = "";
                    dt1.Rows[j]["supdesc"] = "";

                }

                else
                {
                    billno = dt1.Rows[j]["billno"].ToString();
                }

            }

            return dt1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintSearchBill();
        }

        private void PrintSearchBill()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            this.printBillTracking();

            DataTable dt = (DataTable)Session["tblrptdata"];
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.GeneralBillTracking>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptBillTracking", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Bill Tracking"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            for (int i = 0; i < this.cblBillTracking.Items.Count; i++)
            {
                if (cblBillTracking.Items[i].Selected)
                {
                    string header = this.cblBillTracking.Items[i].Value;
                    string title = this.cblBillTracking.Items[i].Text.Trim();
                    Rpt1.SetParameters(new ReportParameter(header, title));

                }
                else
                {
                    string header = this.cblBillTracking.Items[i].Value;
                    Rpt1.SetParameters(new ReportParameter(header, ""));

                }

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Supplier Bill Tracking";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }






        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        private void printBillTracking()
        {
            string comcod = this.GetComeCode();
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string fieldinfo = "";
            string txtBillDate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";
            for (int i = 0; i < this.cblBillTracking.Items.Count; i++)
            {
                if (cblBillTracking.Items[i].Selected)
                {
                    fieldinfo = fieldinfo + "" + cblBillTracking.Items[i].Value.ToString() + " " + cblBillTracking.Items[i].Value.ToString() + ", ";
                }


            }
            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtBillDate = ((this.ddlFieldList1.SelectedValue.ToString() == "billdate") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtBillDate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + this.txtSearch1.Text.Trim() + txtBillDate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtBillDate = ((this.ddlFieldList1.SelectedValue.ToString() == "billdate") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtBillDate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + this.txtSearch2.Text.Trim() + txtBillDate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtBillDate = ((this.ddlFieldList1.SelectedValue.ToString() == "billdate") ? "'" : "");
                txtlike = (this.ddlSrch3.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + " " + this.ddlOperator2.SelectedValue.ToString() + " " + this.ddlFieldList3.SelectedValue.ToString() + " " + this.ddlSrch3.SelectedValue.ToString() + " " + txtBillDate + ((this.ddlSrch3.SelectedValue == "like") ? "'" : "") + this.txtSearch3.Text.Trim() + txtBillDate + txtlike;

            }

            if (this.ddlOrder1.SelectedValue != "00000")
            {
                orderinfo = orderinfo + this.ddlOrder1.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder2.SelectedValue != "00000")
            {
                orderinfo = orderinfo + this.ddlOrder2.SelectedValue.ToString() + " " + this.ddlOrderad2.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder3.SelectedValue != "00000")
            {
                orderinfo = orderinfo + this.ddlOrder3.SelectedValue.ToString() + " " + this.ddlOrderad3.SelectedValue.ToString() + ",";

            }
            fieldinfo = ASTUtility.Left(fieldinfo.Trim(), fieldinfo.Trim().Length - 1);
            SearchInfo = SearchInfo.Trim();
            if (orderinfo.Length > 0)
                orderinfo = ASTUtility.Left(orderinfo.Trim(), orderinfo.Trim().Length - 1);
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTBILLTRACKING", fdate, tdate, fieldinfo, SearchInfo, orderinfo, "", "", "", "");
            if (ds1 == null)
            {
                return;
            }


            Session["tblrptdata"] = ds1.Tables[0]; //HiddenSameData(ds1.Tables[0]);
        }
        protected void gvBillTracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //Label lgvResDescd = (Label)e.Row.FindControl("lgvBillNo");
                Label lgvlgvMDesc = (Label)e.Row.FindControl("lgvMDesc");
                Label lgvllgvamt = (Label)e.Row.FindControl("lgvamt");



                //string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string matdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "matdesc")).ToString();

                if (matdesc == "")
                {
                    return;
                }
                //if (ASTUtility.Left(billno, 3) == "PBL")
                //{
                //    lgvResDescd.Font.Bold = true;
                //}

                if (matdesc == "Total Amt: ---------------------------")
                {
                    lgvlgvMDesc.Font.Bold = true;
                    lgvllgvamt.Font.Bold = true;
                }

            }
        }
        protected void gvBillTracking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBillTracking.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void chkallBillTracking_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkallBillTracking.Checked)
            {
                for (int i = 0; i < this.cblBillTracking.Items.Count; i++)
                {
                    cblBillTracking.Items[i].Selected = true;

                }


            }

            else
            {
                for (int i = 0; i < this.cblBillTracking.Items.Count; i++)
                {
                    cblBillTracking.Items[i].Selected = false;

                }

            }
            this.cblBillTracking_SelectedIndexChanged(null, null);
        }
        protected void cblBillTracking_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbldyfield"];


            for (int i = 0; i < this.cblBillTracking.Items.Count; i++)
            {
                dt.Rows[i]["ffalse"] = (this.cblBillTracking.Items[i].Selected) ? "True" : "False";
            }


            DataRow dr1 = dt.NewRow();
            dr1["code"] = "00000";
            dr1["descrip"] = "----selecttion---";
            dr1["ffalse"] = "True";
            dt.Rows.Add(dr1);
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("ffalse like 'True%'");

            //Search Field Option

            this.ddlFieldList1.DataTextField = "descrip";
            this.ddlFieldList1.DataValueField = "code";
            this.ddlFieldList1.DataSource = dv.ToTable();
            this.ddlFieldList1.DataBind();

            this.ddlFieldList2.DataTextField = "descrip";
            this.ddlFieldList2.DataValueField = "code";
            this.ddlFieldList2.DataSource = dv.ToTable();
            this.ddlFieldList2.DataBind();

            this.ddlFieldList3.DataTextField = "descrip";
            this.ddlFieldList3.DataValueField = "code";
            this.ddlFieldList3.DataSource = dv.ToTable();
            this.ddlFieldList3.DataBind();

            this.ddlFieldList1.SelectedValue = "00000";
            this.ddlFieldList2.SelectedValue = "00000";
            this.ddlFieldList3.SelectedValue = "00000";

            // dv.Sort="code";

            this.ddlOrder1.DataTextField = "descrip";
            this.ddlOrder1.DataValueField = "code";
            this.ddlOrder1.DataSource = dv.ToTable();
            this.ddlOrder1.DataBind();

            this.ddlOrder2.DataTextField = "descrip";
            this.ddlOrder2.DataValueField = "code";
            this.ddlOrder2.DataSource = dv.ToTable();
            this.ddlOrder2.DataBind();

            this.ddlOrder3.DataTextField = "descrip";
            this.ddlOrder3.DataValueField = "code";
            this.ddlOrder3.DataSource = dv.ToTable();
            this.ddlOrder3.DataBind();




            this.ddlOrder1.SelectedValue = "00000";
            this.ddlOrder2.SelectedValue = "00000";
            this.ddlOrder3.SelectedValue = "00000";


            dv.RowFilter = ("code not in ('00000')");
            Session["tbldyfield"] = dv.ToTable();
        }
    }
}