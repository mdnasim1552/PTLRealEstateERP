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
namespace RealERPWEB.F_23_CR
{
    public partial class ClientInfo : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();
        AutoCompleted Data = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetDynamcifield();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = " CLIENT INFORMATION";
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
            ViewState.Remove("tbldyfield");
            string comcod = this.GetComeCode();
            DataSet ds4 = MISData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "CLIENTDYNAMICFIELD", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.cblInfoList.Items.Clear();
                return;
            }

            this.cblInfoList.DataTextField = "descrip";
            this.cblInfoList.DataValueField = "code";
            this.cblInfoList.DataSource = ds4.Tables[0];
            this.cblInfoList.DataBind();
            ViewState["tbldyfield"] = ds4.Tables[0];


        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string txtjorbirdate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";
            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "birth") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "marr") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + this.txtSearch1.Text.Trim() + txtjorbirdate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "birth") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "marr") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + this.txtSearch2.Text.Trim() + txtjorbirdate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "birth") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "marr") ? "'" : "");
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

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "CLIENTSEARCHLIST", SearchInfo, orderinfo, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvClientInfo.DataSource = null;
                this.gvClientInfo.DataBind();
                return;
            }
            ViewState["tbldata"] = HiddenSameData(ds1.Tables[0]);
            //ViewState["tbldata"] = ds1.Tables[0];
            this.Data_Bind();

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tbldata"];

            int i;


            for (i = 0; i < this.gvClientInfo.Columns.Count; i++)
                this.gvClientInfo.Columns[i].Visible = false;


            for (i = 0; i < this.cblInfoList.Items.Count; i++)
            {
                if (this.cblInfoList.Items[i].Selected)
                {
                    this.gvClientInfo.Columns[i].Visible = true;
                    this.gvClientInfo.Columns[i].HeaderText = this.cblInfoList.Items[i].Text.Trim();


                }

            }
            this.gvClientInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvClientInfo.DataSource = dt;
            this.gvClientInfo.DataBind();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string proname = dt1.Rows[0]["proname"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["proname"].ToString() == proname)
                {
                    proname = dt1.Rows[j]["proname"].ToString();
                    dt1.Rows[j]["proname"] = "";
                }

                else
                {
                    proname = dt1.Rows[j]["proname"].ToString();
                }

            }

            return dt1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintComProCost();
        }

        private void PrintComProCost()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //ReportDocument rptstk = new ReportDocument();
            this.printTransSearch();
            DataTable dt1 = (DataTable)Session["tblrptdata"];

            // rdlc start
            LocalReport Rpt1 = new LocalReport();
            var list = dt1.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientInfos>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptClientInfo", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtTitle", "Client Information Details"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            for (int i = 0; i < this.cblInfoList.Items.Count; i++)
            {

                if (cblInfoList.Items[i].Selected)
                {
                    string header = this.cblInfoList.Items[i].Value;
                    string title = this.cblInfoList.Items[i].Text.Trim();
                    Rpt1.SetParameters(new ReportParameter(header, title));
                }
                else
                {
                    string header = this.cblInfoList.Items[i].Value;
                    Rpt1.SetParameters(new ReportParameter(header, ""));
                }

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Information";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            // rdle end

            /*
            rptstk = new RealERPRPT.R_23_CR.ClientInfo();

            for (int i = 0; i < this.cblInfoList.Items.Count; i++)
            {




                if (cblInfoList.Items[i].Selected)
                {
                    string header = this.cblInfoList.Items[i].Value;
                    string title = this.cblInfoList.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects[header] as TextObject;
                    rpttxth.Text = title;
                }



                else
                {
                    string header = this.cblInfoList.Items[i].Value;

                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects[header] as TextObject;
                    rpttxth.Text = "";

                }

            }
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
             */

            //string comcod = hst["comcod"].ToString();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Client Info";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);


            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }





        protected void cblTransList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbldyfield"];


            for (int i = 0; i < this.cblInfoList.Items.Count; i++)
            {
                dt.Rows[i]["ffalse"] = (this.cblInfoList.Items[i].Selected) ? "True" : "False";
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
            ViewState["tbldyfield"] = dv.ToTable();

        }

        protected void chkallTransList_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkallInfoList.Checked)
            {
                for (int i = 0; i < this.cblInfoList.Items.Count; i++)
                {
                    cblInfoList.Items[i].Selected = true;

                }


            }

            else
            {
                for (int i = 0; i < this.cblInfoList.Items.Count; i++)
                {
                    cblInfoList.Items[i].Selected = false;

                }

            }
            this.cblTransList_SelectedIndexChanged(null, null);
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        private void printTransSearch()
        {
            string comcod = this.GetComeCode();
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string fieldinfo = "";
            string txtjorbirdate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";
            for (int i = 0; i < this.cblInfoList.Items.Count; i++)
            {
                if (cblInfoList.Items[i].Selected)
                {
                    fieldinfo = fieldinfo + "" + cblInfoList.Items[i].Value.ToString() + " " + cblInfoList.Items[i].Value.ToString() + ", ";
                }


            }
            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "birth") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "marr") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + this.txtSearch1.Text.Trim() + txtjorbirdate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "birth") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "marr") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";
                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + this.txtSearch2.Text.Trim() + txtjorbirdate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "birth") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "marr") ? "'" : "");
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
            fieldinfo = ASTUtility.Left(fieldinfo.Trim(), fieldinfo.Trim().Length - 1);
            SearchInfo = SearchInfo.Trim();
            if (orderinfo.Length > 0)
                orderinfo = ASTUtility.Left(orderinfo.Trim(), orderinfo.Trim().Length - 1);

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCLIENTSEARCHLIST", fieldinfo, SearchInfo, orderinfo, "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            Session["tblrptdata"] = HiddenSameData(ds1.Tables[0]);

            //    //ViewState["tbldata"] = ds1.Tables[0];
            //    //this.Data_Bind();
        }
        protected void gvClientInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvCustname = (Label)e.Row.FindControl("lgvCustname");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "name")).ToString();

                if (code == "")
                {
                    return;
                }

                lgvCustname.Font.Bold = true;

            }
        }
        protected void gvClientInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvClientInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}