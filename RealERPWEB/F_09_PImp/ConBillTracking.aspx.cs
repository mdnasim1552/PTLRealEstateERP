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
namespace RealERPWEB.F_09_PImp
{
    public partial class ConBillTracking : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();
        AutoCompleted Data = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SUB-CONTRACTOR BILL TRACKING LIST";
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
            DataSet ds4 = MISData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "CBILLDYNAMICFIELD", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.cblCBillTracking.Items.Clear();
                return;
            }

            this.cblCBillTracking.DataTextField = "descrip";
            this.cblCBillTracking.DataValueField = "code";
            this.cblCBillTracking.DataSource = ds4.Tables[0];
            this.cblCBillTracking.DataBind();
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

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "CBILLTRACKING", fdate, tdate, SearchInfo, orderinfo, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCBillTracking.DataSource = null;
                this.gvCBillTracking.DataBind();
                return;
            }
            Session["tbldata"] = HiddenSameData(ds1.Tables[0]);
            //Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Contractor Bill Tracking";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbldata"];

            int i;


            for (i = 0; i < this.gvCBillTracking.Columns.Count; i++)
                this.gvCBillTracking.Columns[i].Visible = false;


            for (i = 0; i < this.cblCBillTracking.Items.Count; i++)
            {
                if (this.cblCBillTracking.Items[i].Selected)
                {
                    this.gvCBillTracking.Columns[i].Visible = true;
                    this.gvCBillTracking.Columns[i].HeaderText = this.cblCBillTracking.Items[i].Text.Trim();


                }

            }
            this.gvCBillTracking.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvCBillTracking.DataSource = dt;
            this.gvCBillTracking.DataBind();
            this.FooterCalculation();

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbldata"];
            DataTable dt1 = dt.Copy();
            if (dt1.Rows.Count == 0)
                return;
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("labdesc like 'Total%'");
            dt1 = dv.ToTable();

            ((Label)this.gvCBillTracking.FooterRow.FindControl("lgvFamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
                                    0 : dt1.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string billno = dt1.Rows[0]["billno"].ToString();

            //string billdate = dt1.Rows[0]["billdate"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["billno"].ToString() == billno)
                {

                    billno = dt1.Rows[j]["billno"].ToString();
                    dt1.Rows[j]["billno"] = "";
                    dt1.Rows[j]["cbillref"] = "";
                    dt1.Rows[j]["vounum"] = "";

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
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new ReportDocument();
            this.printBillTracking();
            DataTable dt1 = (DataTable)Session["tblrptdata"];
            rptstk = new RealERPRPT.R_09_PImp.RptConBillTracking();

            for (int i = 0; i < this.cblCBillTracking.Items.Count; i++)
            {




                if (cblCBillTracking.Items[i].Selected)
                {
                    string header = this.cblCBillTracking.Items[i].Value;
                    string title = this.cblCBillTracking.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects[header] as TextObject;
                    rpttxth.Text = title;
                }



                else
                {
                    string header = this.cblCBillTracking.Items[i].Value;

                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects[header] as TextObject;
                    rpttxth.Text = "";

                }

            }
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Contractor Bill Tracking";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            rptstk.SetDataSource(dt1);


            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);


            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
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
            for (int i = 0; i < this.cblCBillTracking.Items.Count; i++)
            {
                if (cblCBillTracking.Items[i].Selected)
                {
                    fieldinfo = fieldinfo + "" + cblCBillTracking.Items[i].Value.ToString() + " " + cblCBillTracking.Items[i].Value.ToString() + ", ";
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
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTCBILLTRACKING", fdate, tdate, fieldinfo, SearchInfo, orderinfo, "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            Session["tblrptdata"] = HiddenSameData(ds1.Tables[0]);
        }

        protected void chkallCBillTracking_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkallCBillTracking.Checked)
            {
                for (int i = 0; i < this.cblCBillTracking.Items.Count; i++)
                {
                    cblCBillTracking.Items[i].Selected = true;

                }


            }

            else
            {
                for (int i = 0; i < this.cblCBillTracking.Items.Count; i++)
                {
                    cblCBillTracking.Items[i].Selected = false;

                }

            }
            this.cblCBillTracking_SelectedIndexChanged(null, null);
        }
        protected void cblCBillTracking_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbldyfield"];


            for (int i = 0; i < this.cblCBillTracking.Items.Count; i++)
            {
                dt.Rows[i]["ffalse"] = (this.cblCBillTracking.Items[i].Selected) ? "True" : "False";
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
        protected void gvCBillTracking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCBillTracking.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvCBillTracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //Label lgvResDescd = (Label)e.Row.FindControl("lgvBillNo");
                Label lgvLabDesc = (Label)e.Row.FindControl("lgvLabDesc");
                Label lgvamt = (Label)e.Row.FindControl("lgvamt");



                //string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string labdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "labdesc")).ToString();

                if (labdesc == "")
                {
                    return;
                }
                //if (ASTUtility.Left(billno, 3) == "PBL")
                //{
                //    lgvResDescd.Font.Bold = true;
                //}

                if (labdesc == "Total Amt: ---------------------------")
                {
                    lgvLabDesc.Font.Bold = true;
                    lgvamt.Font.Bold = true;
                }

            }
        }
    }
}