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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccTranSearch : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();
        AutoCompleted Data = new AutoCompleted();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "TRANSACTION SEARCH";
                //this.Master.Page.Title = "TRANSACTION SEARCH";

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
            DataSet ds4 = MISData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "TRANSDYNAMICFIELDAC", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.cblTransList.Items.Clear();
                return;
            }

            this.cblTransList.DataTextField = "descrip";
            this.cblTransList.DataValueField = "code";
            this.cblTransList.DataSource = ds4.Tables[0];
            this.cblTransList.DataBind();
            Session["tbldyfield"] = ds4.Tables[0];


        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string txtjorbirdate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";
            string narration = ((this.chkneNar.Checked) ? "" : "All");
            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "voudate") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + this.txtSearch1.Text.Trim() + txtjorbirdate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "voudate") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + this.txtSearch2.Text.Trim() + txtjorbirdate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "voudate") ? "'" : "");
                txtlike = (this.ddlSrch3.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + " " + this.ddlOperator2.SelectedValue.ToString() + " " + this.ddlFieldList3.SelectedValue.ToString() + " " + this.ddlSrch3.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch3.SelectedValue == "like") ? "'" : "") + this.txtSearch3.Text.Trim() + txtjorbirdate + txtlike;

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

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "PRINTTRANSACTIONSSEARCH", fromdate, todate, SearchInfo, orderinfo, narration, "", "", "", "");
            if (ds1 == null)
            {
                this.gvAcTransSearch.DataSource = null;
                this.gvAcTransSearch.DataBind();
                return;
            }
            Session["tbldata"] = HiddenSameData(ds1.Tables[0]);
            //Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbldata"];
            if (dt.Rows.Count == 0)
            {
                this.gvAcTransSearch.DataSource = null;
                this.gvAcTransSearch.DataBind();
                return;
            }

            int i;
            for (i = 0; i < this.gvAcTransSearch.Columns.Count; i++)
                this.gvAcTransSearch.Columns[i].Visible = false;


            for (i = 0; i < this.cblTransList.Items.Count; i++)
            {
                if (this.cblTransList.Items[i].Selected)
                {
                    this.gvAcTransSearch.Columns[i].Visible = true;
                    this.gvAcTransSearch.Columns[i].HeaderText = this.cblTransList.Items[i].Text.Trim();


                }

            }
            this.gvAcTransSearch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvAcTransSearch.DataSource = dt;
            this.gvAcTransSearch.DataBind();

            string date = Convert.ToDateTime(((Label)gvAcTransSearch.Rows[0].FindControl("lgvVouDate")).Text).ToString("dd-MMM-yyyy");
            string vounum = ((Label)gvAcTransSearch.Rows[0].FindControl("lgvVouNo02")).Text;
            for (i = 1; i < gvAcTransSearch.Rows.Count; i++)

            {

                if (((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouNo02")).Text == vounum && Convert.ToDateTime(((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouDate")).Text).ToString("dd-MMM-yyyy") == date)
                {
                    ((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouDate")).Visible = false;
                    vounum = ((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouNo02")).Text;
                    date = Convert.ToDateTime(((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouDate")).Text).ToString("dd-MMM-yyyy");
                }
                else
                {
                    //if (Convert.ToDateTime(((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouDate")).Text).ToString("dd-MMM-yyyy") == date)
                    //{ 
                    //((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouDate")).Visible = false;
                    //}


                    vounum = ((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouNo02")).Text;
                    date = Convert.ToDateTime(((Label)gvAcTransSearch.Rows[i].FindControl("lgvVouDate")).Text).ToString("dd-MMM-yyyy");
                }
            }


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string vounum = dt1.Rows[0]["vounum"].ToString();
            //string voudate = dt1.Rows[0]["voudate"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["vounum"].ToString() == vounum)
                {
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    //voudate = dt1.Rows[j]["voudate"].ToString();
                    dt1.Rows[j]["vounum"] = "";
                    //dt1.Rows[j]["voudate"] = "";
                }

                else
                    vounum = dt1.Rows[j]["vounum"].ToString();
                //voudate = dt1.Rows[j]["voudate"].ToString();
            }

            return dt1;




            //if (dt1.Rows.Count == 0)
            //    return dt1;
            //string vounum = dt1.Rows[0]["vounum"].ToString();
            //string cheqno = dt1.Rows[0]["cheqrefno"].ToString();
            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["vounum"].ToString() == vounum || dt1.Rows[j]["cheqrefno"].ToString() == cheqno)
            //    {
            //        vounum = dt1.Rows[j]["vounum"].ToString();
            //        cheqno = dt1.Rows[j]["cheqrefno"].ToString();
            //        dt1.Rows[j]["vounum"] = "";
            //        dt1.Rows[j]["cheqrefno"] = "";
            //    }

            //    else
            //    {
            //        if (dt1.Rows[j]["vounum"].ToString() == vounum)
            //            dt1.Rows[j]["vounum"] = "";

            //        else if (dt1.Rows[j]["cheqrefno"].ToString() == cheqno)
            //            dt1.Rows[j]["cheqrefno"] = "";


            //        vounum = dt1.Rows[j]["vounum"].ToString();
            //        cheqno = dt1.Rows[j]["cheqrefno"].ToString();
            //    }

            //}

            //return dt1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            //    this.PrintComProCost();

        }

        //private void PrintComProCost()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //    ReportDocument rptstk = new ReportDocument();
        //    this.printTransSearch();
        //    DataTable dt1 = (DataTable)Session["tblrptdata"];
        //    rptstk = new RealERPRPT.R_23_CR.RptTransSearch();

        //    for (int i = 0; i < this.cblTransList.Items.Count; i++)
        //    {




        //        if (cblTransList.Items[i].Selected)
        //        {
        //            string header = this.cblTransList.Items[i].Value;
        //            string title = this.cblTransList.Items[i].Text.Trim();
        //            TextObject rpttxth = rptstk.ReportDefinition.ReportObjects[header] as TextObject;
        //            rpttxth.Text = title;
        //        }



        //        else
        //        {
        //            string header = this.cblTransList.Items[i].Value;

        //            TextObject rpttxth = rptstk.ReportDefinition.ReportObjects[header] as TextObject;
        //            rpttxth.Text = "";

        //        }

        //    }
        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptstk.SetDataSource(dt1);

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = lblHtitle.Text;
        //        string eventdesc = "Print Report";
        //        string eventdesc2 = "";
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }

        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptstk.SetParameterValue("ComLogo", ComLogo);


        //    Session["Report1"] = rptstk;
        //    this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}





        protected void cblTransList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbldyfield"];


            for (int i = 0; i < this.cblTransList.Items.Count; i++)
            {
                dt.Rows[i]["ffalse"] = (this.cblTransList.Items[i].Selected) ? "True" : "False";
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

        protected void chkallTransList_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkallTransList.Checked)
            {
                for (int i = 0; i < this.cblTransList.Items.Count; i++)
                {
                    cblTransList.Items[i].Selected = true;

                }


            }

            else
            {
                for (int i = 0; i < this.cblTransList.Items.Count; i++)
                {
                    cblTransList.Items[i].Selected = false;

                }

            }
            this.cblTransList_SelectedIndexChanged(null, null);
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        //private void printTransSearch()
        //{
        //    string comcod = this.GetComeCode();
        //    this.lblPage.Visible = true;
        //    this.ddlpagesize.Visible = true;
        //    string fieldinfo = "";
        //    string txtjorbirdate = "";
        //    string txtlike = "";
        //    string SearchInfo = "";
        //    string orderinfo = "";
        //    for (int i = 0; i < this.cblTransList.Items.Count; i++)
        //    {
        //        if (cblTransList.Items[i].Selected)
        //        {
        //            fieldinfo = fieldinfo + "" + cblTransList.Items[i].Value.ToString() + " " + cblTransList.Items[i].Value.ToString() + ", ";
        //        }


        //    }
        //    if (this.ddlFieldList1.SelectedValue != "00000")
        //    {

        //        txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "mrdate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "paydate") ? "'" : "");
        //        txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";
        //        SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + this.txtSearch1.Text.Trim() + txtjorbirdate + txtlike;

        //    }


        //    if (this.ddlFieldList2.SelectedValue != "00000")
        //    {

        //        txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "mrdate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "paydate") ? "'" : "");
        //        txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";
        //        SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + this.txtSearch2.Text.Trim() + txtjorbirdate + txtlike;

        //    }

        //    if (this.ddlFieldList3.SelectedValue != "00000")
        //    {
        //        txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "mrdate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "paydate") ? "'" : "");
        //        txtlike = (this.ddlSrch3.SelectedValue == "like") ? "%'" : "";
        //        SearchInfo = SearchInfo + " " + this.ddlOperator2.SelectedValue.ToString() + " " + this.ddlFieldList3.SelectedValue.ToString() + " " + this.ddlSrch3.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch3.SelectedValue == "like") ? "'" : "") + this.txtSearch3.Text.Trim() + txtjorbirdate + txtlike;

        //    }

        //    if (this.ddlOrder1.SelectedValue != "00000")
        //    {
        //        orderinfo = orderinfo + this.ddlOrder1.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString() + ", ";

        //    }
        //    if (this.ddlOrder2.SelectedValue != "00000")
        //    {
        //        orderinfo = orderinfo + this.ddlOrder2.SelectedValue.ToString() + " " + this.ddlOrderad2.SelectedValue.ToString() + ", ";

        //    }
        //    if (this.ddlOrder3.SelectedValue != "00000")
        //    {
        //        orderinfo = orderinfo + this.ddlOrder3.SelectedValue.ToString() + " " + this.ddlOrderad3.SelectedValue.ToString() + ",";

        //    }
        //    fieldinfo = ASTUtility.Left(fieldinfo.Trim(), fieldinfo.Trim().Length - 1);
        //    SearchInfo = SearchInfo.Trim();
        //    if (orderinfo.Length > 0)
        //        orderinfo = ASTUtility.Left(orderinfo.Trim(), orderinfo.Trim().Length - 1);

        //    DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTTRANSSEARCHLIST", fieldinfo, SearchInfo, orderinfo, "", "", "", "", "", "");
        //    if (ds1 == null)
        //    {
        //        return;
        //    }
        //    Session["tblrptdata"] = HiddenSameData(ds1.Tables[0]);

        //    //Session["tbldata"] = ds1.Tables[0];
        //    //this.Data_Bind();
        //}
        protected void gvAcTransSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {









            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVouNo");
                Label lgvlgvAcode = (Label)e.Row.FindControl("lgvAcode");
                Label lgvlgvDesc = (Label)e.Row.FindControl("lgvDesc");
                Label lgvlgvDamt = (Label)e.Row.FindControl("lgvDamt");
                Label lgvlgvCamt = (Label)e.Row.FindControl("lgvCamt");
                string vounum = ((Label)e.Row.FindControl("lgvVouNo02")).Text;
                // string vounum = ((HyperLink)e.Row.FindControl("HLgvVouNo")).Text;
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "Total Amt:")
                {

                    lgvlgvAcode.Font.Bold = true;
                    lgvlgvDesc.Font.Bold = true;
                    lgvlgvDamt.Font.Bold = true;
                    lgvlgvCamt.Font.Bold = true;
                }

                if (vounum.Substring(0, 2) == "PV")
                    hlink1.NavigateUrl = "RptAccVouher02.aspx?vounum=" + vounum;
                else
                    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + vounum;
            }
        }
        protected void gvAcTransSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAcTransSearch.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

    }
}