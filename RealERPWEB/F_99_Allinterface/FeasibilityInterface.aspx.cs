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
using System.Drawing;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class FeasibilityInterface : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtdate.Text = "01" + date.Substring(2);
                this.txtdate.Text = firstDay.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Feasibility Interface";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;

                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;
                //this.GetProfession();
                //this.GetBudgetData();
                //this.Createtable();
                //this.GetNewClient();

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.gvPrjInfo_RowDataBound(null, null);
            this.GetBudgetData();
            //this.Data_Bind();
            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                case "0":
                    this.pnlinitial.Visible = true;
                    this.pnlcheck.Visible = false;
                    this.pnlfeasibility.Visible = false;
                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;
                    this.pnllegal.Visible = false;
                    this.pnlforword.Visible = false;
                    this.pnlapp.Visible = false;

                    this.pnlplanning.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;

                case "1":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = true;
                    this.pnlfeasibility.Visible = false;
                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;
                    this.pnllegal.Visible = false;
                    this.pnlforword.Visible = false;
                    this.pnlapp.Visible = false;

                    this.pnlplanning.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlfeasibility.Visible = true;
                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;
                    this.pnllegal.Visible = false;
                    this.pnlforword.Visible = false;
                    this.pnlapp.Visible = false;

                    this.pnlplanning.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "3":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlfeasibility.Visible = false;
                    this.pnldocument.Visible = true;
                    this.pnlmoredoc.Visible = false;
                    this.pnllegal.Visible = false;
                    this.pnlforword.Visible = false;
                    this.pnlapp.Visible = false;

                    this.pnlplanning.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "4":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlfeasibility.Visible = false;
                    this.pnlmoredoc.Visible = true;
                    this.pnldocument.Visible = false;
                    this.pnllegal.Visible = false;
                    this.pnlforword.Visible = false;
                    this.pnlapp.Visible = false;

                    this.pnlplanning.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "5":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlfeasibility.Visible = false;
                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;
                    this.pnllegal.Visible = true;
                    this.pnlforword.Visible = false;
                    this.pnlapp.Visible = false;

                    this.pnlplanning.Visible = false;
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[4].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "6":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlfeasibility.Visible = false;
                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;
                    this.pnllegal.Visible = false;
                    this.pnlforword.Visible = true;
                    this.pnlapp.Visible = false;
                    this.pnlplanning.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[5].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "7":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlfeasibility.Visible = false;
                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;
                    this.pnllegal.Visible = false;
                    this.pnlforword.Visible = false;
                    this.pnlapp.Visible = true;
                    this.pnlplanning.Visible = false;
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[6].Attributes["style"] = "background: #430000; display:block; ";
                    break;

                    //case "8":
                    //    this.pnlinitial.Visible = false;
                    //    this.pnlcheck.Visible = false;
                    //    this.pnlfeasibility.Visible = false;
                    //    this.pnldocument.Visible = false;
                    //    this.pnlmoredoc.Visible = false;
                    //    this.pnllegal.Visible = false;
                    //    this.pnlforword.Visible = false;
                    //    this.pnlapp.Visible = false;
                    //    this.pnlplanning.Visible = true;
                    //    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    //    //this.RadioButtonList1.Items[7].Attributes["style"] = "background: #430000; display:block; ";
                    //    break;
            }


        }
        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);

            GetBudgetData();
        }

        private void GetBudgetData()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            //string catcode = this.ddlcatag.SelectedValue.ToString() + "%";
            string catcode = "99%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_LP_PROFEASIBILITY", "RPTALLPROTOPSHEET", frmdate, todate, catcode, "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }




            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["intial"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Initial</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["checked"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Checked</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["feasibility"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Feasibility</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["document"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Documentation</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["mordoc"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>More-Document</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["legal"]) + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Legal</div></div></div>";
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue  counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["forword"]) + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Forword</div></div></div>";
            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["approval"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Approval</div></div></div>";
            //this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'></i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Planning</div></div></div>";

            Session["tblfeaprjLand"] = ds2.Tables[0];

            DataTable dt = new DataTable();
            DataView dv;
            //Intial
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("status='true'");
            this.Data_Bind("gvFeaPrjLand", dv.ToTable());

            ////Checked
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkid='' and status='true'");
            this.Data_Bind("gvCheck", dv.ToTable());
            //Forward

            ////Feasibility
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkid<>'' and feasid=''");
            this.Data_Bind("gvFeasibility", dv.ToTable());

            ////Documentation
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("feasid <>'' and docid=''");
            this.Data_Bind("gvDocument", dv.ToTable());

            ////More Documentation
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("bakid <>'' and legalid=''");
            this.Data_Bind("gvMoreDoc", dv.ToTable());

            ////Legal
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("(docid <>'' and legalid='' and bakid ='') or (mordocid <>'' and legalid='')");
            this.Data_Bind("gvLegal", dv.ToTable());

            //pnlForward
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = "legalid<>'' and forwid=''";
            this.Data_Bind("gvForward", dv.ToTable());


            //pnlApproval
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = "forwid<>'' and apprid=''";
            this.Data_Bind("gvApprov", dv.ToTable());

            //// dv.RowFilter = ("issued = 'D' ");
            //this.Data_Bind("gvChequeSign", dv.ToTable());


            //////Complited
            //dt = ((DataTable)ds1.Tables[2]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("CQPAYTPPARTY = 'False' ");
            //ViewState["tblcqkpary"] = dv.ToTable();
            //this.Data_Bind("grvComp", dv.ToTable());









            //this.Data_Bind();
            //this.ttlcustomer.Text = Convert.ToDouble(dta.Rows[0]["ttlprj"]).ToString("#,##0;(#,##0); ");



            //DataTable dt = new DataTable();
            //DataView dv = new DataView();
            //DataTable dt0 = new DataTable();

            //dt0 = ((DataTable)dskpi.Tables[1]).Copy();

            //dv = dt0.DefaultView;
            //dv.Sort = "actcode asc";
            //dt = dv.ToTable();

            //ViewState["ALLBudugetData"] = dt;
            //this.Data_Bind();




        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvFeaPrjLand":
                    this.gvFeaPrjLand.DataSource = dt;
                    this.gvFeaPrjLand.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvCheck":
                    this.gvCheck.DataSource = dt;
                    this.gvCheck.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvFeasibility":
                    this.gvFeasibility.DataSource = dt;
                    this.gvFeasibility.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvDocument":
                    this.gvDocument.DataSource = dt;
                    this.gvDocument.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvMoreDoc":
                    this.gvMoreDoc.DataSource = dt;
                    this.gvMoreDoc.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvLegal":
                    this.gvLegal.DataSource = dt;
                    this.gvLegal.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvForward":
                    this.gvForward.DataSource = dt;
                    this.gvForward.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvApprov":
                    this.gvApprov.DataSource = dt;
                    this.gvApprov.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

            }

            //DataTable dt = (DataTable)Session["tblfeaprjLand"];



        }


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("chkid", Type.GetType("System.String"));
            tblt01.Columns.Add("chkdat", Type.GetType("System.String"));
            tblt01.Columns.Add("chktrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("chkseson", Type.GetType("System.String"));
            tblt01.Columns.Add("feasid", Type.GetType("System.String"));
            tblt01.Columns.Add("feasdat", Type.GetType("System.String"));
            tblt01.Columns.Add("feastrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("feasseson", Type.GetType("System.String"));
            tblt01.Columns.Add("docid", Type.GetType("System.String"));
            tblt01.Columns.Add("docdat", Type.GetType("System.String"));
            tblt01.Columns.Add("doctrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("docseson", Type.GetType("System.String"));
            tblt01.Columns.Add("bakid", Type.GetType("System.String"));
            tblt01.Columns.Add("bakdat", Type.GetType("System.String"));
            tblt01.Columns.Add("baktrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("bakseson", Type.GetType("System.String"));
            tblt01.Columns.Add("mordocid", Type.GetType("System.String"));
            tblt01.Columns.Add("mordocdat", Type.GetType("System.String"));
            tblt01.Columns.Add("mordoctrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("mordocseson", Type.GetType("System.String"));
            tblt01.Columns.Add("legalid", Type.GetType("System.String"));
            tblt01.Columns.Add("legaldat", Type.GetType("System.String"));
            tblt01.Columns.Add("legaltrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("legalseson", Type.GetType("System.String"));
            tblt01.Columns.Add("forwid", Type.GetType("System.String"));
            tblt01.Columns.Add("forwdat", Type.GetType("System.String"));
            tblt01.Columns.Add("forwtrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("forwseson", Type.GetType("System.String"));
            tblt01.Columns.Add("apprid", Type.GetType("System.String"));
            tblt01.Columns.Add("apprdat", Type.GetType("System.String"));
            tblt01.Columns.Add("apprtrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("apprseson", Type.GetType("System.String"));
            ViewState["tblapproval"] = tblt01;
        }

        private string GetReqApproval(string approval, string mordoc = "")
        {


            string view = this.RadioButtonList1.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (view)
            {


                case "1":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  

                    if (approval == "")
                    {


                        this.CreateDataTable();
                        DataTable dt = (DataTable)ViewState["tblapproval"];
                        DataRow dr1 = dt.NewRow();

                        dr1["chkid"] = usrid;
                        dr1["chkdat"] = Date;
                        dr1["chktrmid"] = trmnid;
                        dr1["chkseson"] = session;
                        dr1["feasid"] = "";
                        dr1["feasdat"] = "";
                        dr1["feastrmid"] = "";
                        dr1["feasseson"] = "";
                        dr1["docid"] = "";
                        dr1["docdat"] = "";
                        dr1["doctrmid"] = "";
                        dr1["docseson"] = "";
                        dr1["bakid"] = "";
                        dr1["bakdat"] = "";
                        dr1["baktrmid"] = "";
                        dr1["bakseson"] = "";
                        dr1["mordocid"] = "";
                        dr1["mordocdat"] = "";
                        dr1["mordoctrmid"] = "";
                        dr1["mordocseson"] = "";
                        dr1["legalid"] = "";
                        dr1["legaldat"] = "";
                        dr1["legaltrmid"] = "";
                        dr1["legalseson"] = "";
                        dr1["forwid"] = "";
                        dr1["forwdat"] = "";
                        dr1["forwtrmid"] = "";
                        dr1["forwseson"] = "";
                        dr1["apprid"] = "";
                        dr1["apprdat"] = "";
                        dr1["apprtrmid"] = "";
                        dr1["apprseson"] = "";
                        dt.Rows.Add(dr1);
                        ds1.Merge(dt);
                        ds1.Tables[0].TableName = "tbl1";
                        approval = ds1.GetXml();

                    }

                    else
                    {

                        xmlSR = new System.IO.StringReader(approval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["chkid"] = usrid;
                        ds1.Tables[0].Rows[0]["chkdat"] = Date;
                        ds1.Tables[0].Rows[0]["chktrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["chkseson"] = session;
                        ds1.Tables[0].Rows[0]["feasid"] = "";
                        ds1.Tables[0].Rows[0]["feasdat"] = "";
                        ds1.Tables[0].Rows[0]["feastrmid"] = "";
                        ds1.Tables[0].Rows[0]["feasseson"] = "";
                        ds1.Tables[0].Rows[0]["docid"] = "";
                        ds1.Tables[0].Rows[0]["docdat"] = "";
                        ds1.Tables[0].Rows[0]["doctrmid"] = "";
                        ds1.Tables[0].Rows[0]["docseson"] = "";
                        ds1.Tables[0].Rows[0]["bakid"] = "";
                        ds1.Tables[0].Rows[0]["bakdat"] = "";
                        ds1.Tables[0].Rows[0]["baktrmid"] = "";
                        ds1.Tables[0].Rows[0]["bakseson"] = "";
                        ds1.Tables[0].Rows[0]["mordocid"] = "";
                        ds1.Tables[0].Rows[0]["mordocdat"] = "";
                        ds1.Tables[0].Rows[0]["mordoctrmid"] = "";
                        ds1.Tables[0].Rows[0]["mordocseson"] = "";
                        ds1.Tables[0].Rows[0]["legalid"] = "";
                        ds1.Tables[0].Rows[0]["legaldat"] = "";
                        ds1.Tables[0].Rows[0]["legaltrmid"] = "";
                        ds1.Tables[0].Rows[0]["legalseson"] = "";
                        ds1.Tables[0].Rows[0]["forwid"] = "";
                        ds1.Tables[0].Rows[0]["forwdat"] = "";
                        ds1.Tables[0].Rows[0]["forwtrmid"] = "";
                        ds1.Tables[0].Rows[0]["forwseson"] = "";
                        ds1.Tables[0].Rows[0]["apprid"] = "";
                        ds1.Tables[0].Rows[0]["apprdat"] = "";
                        ds1.Tables[0].Rows[0]["apprtrmid"] = "";
                        ds1.Tables[0].Rows[0]["apprseson"] = "";
                        approval = ds1.GetXml();
                    }

                    break;




                case "2":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["feasid"] = usrid;
                    ds1.Tables[0].Rows[0]["feasdat"] = Date;
                    ds1.Tables[0].Rows[0]["feastrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["feasseson"] = session;
                    approval = ds1.GetXml();

                    break;

                case "3":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["docid"] = usrid;
                    ds1.Tables[0].Rows[0]["docdat"] = Date;
                    ds1.Tables[0].Rows[0]["doctrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["docseson"] = session;
                    approval = ds1.GetXml();
                    break;
                case "4":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["mordocid"] = usrid;
                    ds1.Tables[0].Rows[0]["mordocdat"] = Date;
                    ds1.Tables[0].Rows[0]["mordoctrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["mordocseson"] = session;
                    ds1.Tables[0].Rows[0]["bakid"] = "";
                    ds1.Tables[0].Rows[0]["bakdat"] = "";
                    ds1.Tables[0].Rows[0]["baktrmid"] = "";
                    ds1.Tables[0].Rows[0]["bakseson"] = "";
                    approval = ds1.GetXml();
                    break;
                case "5":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    if (mordoc == "bak")
                    {
                        xmlSR = new System.IO.StringReader(approval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["bakid"] = usrid;
                        ds1.Tables[0].Rows[0]["bakdat"] = Date;
                        ds1.Tables[0].Rows[0]["baktrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["bakseson"] = session;
                        ds1.Tables[0].Rows[0]["mordocid"] = "";
                        ds1.Tables[0].Rows[0]["mordocdat"] = "";
                        ds1.Tables[0].Rows[0]["mordoctrmid"] = "";
                        ds1.Tables[0].Rows[0]["mordocseson"] = "";
                        approval = ds1.GetXml();
                    }
                    else
                    {
                        xmlSR = new System.IO.StringReader(approval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["legalid"] = usrid;
                        ds1.Tables[0].Rows[0]["legaldat"] = Date;
                        ds1.Tables[0].Rows[0]["legaltrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["legalseson"] = session;
                        approval = ds1.GetXml();
                    }

                    break;
                case "6":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["forwid"] = usrid;
                    ds1.Tables[0].Rows[0]["forwdat"] = Date;
                    ds1.Tables[0].Rows[0]["forwtrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["forwseson"] = session;
                    approval = ds1.GetXml();
                    break;
                case "7":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["apprid"] = usrid;
                    ds1.Tables[0].Rows[0]["apprdat"] = Date;
                    ds1.Tables[0].Rows[0]["apprtrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["apprseson"] = session;
                    approval = ds1.GetXml();
                    break;

            }


            return approval;

        }


        protected void gvFeaPrjLand_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvproname");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "status")).ToString();
                string chkid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "chkid")).ToString();
                string feasid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "feasid")).ToString();
                string docid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "docid")).ToString();
                string legalid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "legalid")).ToString();
                string forwid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "forwid")).ToString();
                string apprid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "apprid")).ToString();
                string bakid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bakid")).ToString();
                string mordocid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mordocid")).ToString();



                hlnkprj.NavigateUrl = "~/F_01_LPA/PriLandProposal?Type=Report&prjcode=" + pactcode;

                Label track = (Label)e.Row.FindControl("lgvtrack");

                //for (int i = 0; i < gvFeaPrjLand.Rows.Count; i++)
                //{
                if (status == "True" && chkid.Trim() == "")
                {

                    // track.Attributes["class"] = "color:green;";
                    track.Attributes.CssStyle.Add("color", "Green");
                }
                else if (chkid != "" && feasid.Trim() == "")
                {
                    track.Attributes.CssStyle.Add("color", "Maroon");
                }
                else if (feasid != "" && docid.Trim() == "")
                {
                    track.Attributes.CssStyle.Add("color", "blue");
                }

                else if (bakid != "" && legalid.Trim() == "")
                {
                    track.Attributes.CssStyle.Add("color", "violet");
                }
                else if (legalid != "" && forwid.Trim() == "")
                {
                    track.Attributes.CssStyle.Add("color", "magenta");
                }
                else if (forwid != "" && apprid.Trim() == "")
                {
                    track.Attributes.CssStyle.Add("color", "gray");
                }
                else if (apprid != "")
                {
                    track.Attributes.CssStyle.Add("color", "brown");
                }
                else if ((docid != "" && legalid.Trim() == "" && bakid.Trim() == "") || (mordocid != "" && legalid.Trim() == ""))
                {
                    track.Attributes.CssStyle.Add("color", "YellowGreen");
                }
                //}


            }


        }
        protected void gvCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvpronamec");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnPreEntry");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                hlnkprj.NavigateUrl = "~/F_01_LPA/PriLandProposal?Type=Report&prjcode=" + pactcode;
                //hlink3.NavigateUrl = "~/F_08_PPlan/PrjCompFlowchart";





            }
        }
        protected void gvFeasibility_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvpronamef");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnPreEntry");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                //hlink2.NavigateUrl = "~/F_02_Fea/ProjectFeasibility";
                hlnkprj.NavigateUrl = "~/F_02_Fea/ProjectFeasibility?Type=FeaEntry&prjcode=" + pactcode;


            }

        }
        protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvpronamed");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnPreEntry");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                //hlink2.NavigateUrl = "~/F_02_Fea/ProjectFeasibility";
                hlnkprj.NavigateUrl = "~/F_02_Fea/ProjectFeasibility?Type=doc&prjcode=" + pactcode;


            }

        }
        protected void lnkbtnEntry_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lblpactcode")).Text.Trim();
            string appxml = dt.Rows[0]["approval"].ToString();
            string approval = GetReqApproval(appxml);
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "True", approval, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkbtnPreEntry_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lblpactcode")).Text.Trim();
            //string appxml = dt.Rows[0]["approval"].ToString();
            //string approval = GetReqApproval(appxml);
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "False", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Cancellation failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Canceled Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkbtnEntryf_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvFeasibility.Rows[Rowindex].FindControl("lblpactcodefe")).Text.Trim();
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "pactcode=" + pactcode + "";
            dt = dv.ToTable();
            string appxml = dt.Rows[0]["approval"].ToString();
            string approval = GetReqApproval(appxml);
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "True", approval, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Updated";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkbtnEntryd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lblpactcodedoc")).Text.Trim();
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "pactcode=" + pactcode + "";
            dt = dv.ToTable();
            string appxml = dt.Rows[0]["approval"].ToString();

            string approval = GetReqApproval(appxml);
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "True", approval, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Updated";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void gvLegal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvpronamel");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnPreEntry");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                //hlink2.NavigateUrl = "~/F_02_Fea/ProjectFeasibility";
                hlnkprj.NavigateUrl = "~/F_02_Fea/ProjectFeasibility?Type=docmore&prjcode=" + pactcode;


            }

        }
        protected void lnkbakword_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvLegal.Rows[Rowindex].FindControl("lblpactcodele")).Text.Trim();
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "pactcode=" + pactcode + "";
            dt = dv.ToTable();
            string appxml = dt.Rows[0]["approval"].ToString();

            string approval = GetReqApproval(appxml, "bak");
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "True", approval, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Updated";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkforword_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvLegal.Rows[Rowindex].FindControl("lblpactcodele")).Text.Trim();
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "pactcode=" + pactcode + "";
            dt = dv.ToTable();
            string appxml = dt.Rows[0]["approval"].ToString();

            string approval = GetReqApproval(appxml);
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "True", approval, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Updated";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void gvMoreDoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvpronamemd");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                hlnkprj.NavigateUrl = "~/F_02_Fea/ProjectFeasibility?Type=doc&prjcode=" + pactcode;


            }
        }
        protected void lnkbtnEntrymd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvMoreDoc.Rows[Rowindex].FindControl("lblpactcodemdoc")).Text.Trim();
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "pactcode=" + pactcode + "";
            dt = dv.ToTable();
            string appxml = dt.Rows[0]["approval"].ToString();

            string approval = GetReqApproval(appxml);
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "True", approval, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Updated";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkconfor_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvForward.Rows[Rowindex].FindControl("lblpactcofor")).Text.Trim();
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "pactcode=" + pactcode + "";
            dt = dv.ToTable();
            string appxml = dt.Rows[0]["approval"].ToString();

            string approval = GetReqApproval(appxml);
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "True", approval, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Forward failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Forwarded";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkaprrov_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblfeaprjLand"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblpactcoap")).Text.Trim();
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "pactcode=" + pactcode + "";
            dt = dv.ToTable();
            string appxml = dt.Rows[0]["approval"].ToString();

            string approval = GetReqApproval(appxml);
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LANDFEASPROGRESS", pactcode, "True", approval, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Approval failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Approved";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void gvApprov_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkintial = (HyperLink)e.Row.FindControl("lgvintialdtap");
                HyperLink hlnfeas = (HyperLink)e.Row.FindControl("lgvfesibilityap");
                HyperLink hlndocfinal = (HyperLink)e.Row.FindControl("lgvfinaldocap");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                hlnkintial.NavigateUrl = "~/F_01_LPA/PriLandProposal?Type=Report&prjcode=" + pactcode;
                hlnfeas.NavigateUrl = "~/F_02_Fea/ProjectFeasibility?Type=fea&prjcode=" + pactcode;
                hlndocfinal.NavigateUrl = "~/F_02_Fea/ProjectFeasibility?Type=doc&prjcode=" + pactcode;


            }
        }
    }
}