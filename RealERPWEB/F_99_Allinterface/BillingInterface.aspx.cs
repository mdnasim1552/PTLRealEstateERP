using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using System.IO;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_99_Allinterface
{

    public partial class BillingInterface : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES INTERFACE";//
                string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                DateTime date = Convert.ToDateTime("01" + curdate.Substring(2));
                DateTime enddate = date.AddMonths(1).AddDays(-1);


                this.RadioButtonList1.SelectedIndex = 0;
                txtdate_TextChanged(null, null);
                

            }
        }


      
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string value = this.RadioButtonList1.SelectedValue.ToString();

        }


       


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            // txtdate_TextChanged(null, null);


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.txtdate_TextChanged(null, null);


        }
        protected void lnkInteface_Click(object sender, EventArgs e)
        {
           
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
           
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? comcod;
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.Countqty();
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }


        private void Countqty()
        {
            string comcod = this.GetCompCode();
            //string frdate = "01-Jan-2015";

            string frdate = "01" + this.txtdate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            //string endmonth = Convert.ToDateTime(frdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALES_INTERFACE", "SALESDASHBORD_OTH", frdate, todate, "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[3];
            ViewState["alltable"] = ds1;
            ViewState["tblcount"] = dt;


        }

        private void SaleRequRpt()
        {
            DataTable dt = (DataTable)ViewState["tblcount"];
            if (dt.Rows.Count == 0)
            {
                return;
            }

            string comcod = this.GetCompCode().Substring(0, 3);
            string daywisesales = "Day Wise Invoice";
            string dues = "Total Dues";
            
            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["invstatus"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + daywisesales + "</span>";
            this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-ruler fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["collstatus"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Money Receipt" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-ruler fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["duestatus"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + dues + "</span>";
           

        }
       
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataSet ds1 = (DataSet)ViewState["alltable"];
            DataTable dt = new DataTable();
            DataView dv = new DataView();

            switch (value)
            {
                case "0":

                    dt = ((DataTable)ds1.Tables[0]).Copy();
                    this.Data_Bind("gvDayWSale", dt);

                    this.pnlgvDayWSale.Visible = true;
                    this.pnlinprocess.Visible = false;
                    this.PnlDues.Visible = false;

                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";
                
                    break;

                case "1":
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    this.Data_Bind("grvTrnDatWise", dt);
                    this.pnlgvDayWSale.Visible = false;
                    this.pnlinprocess.Visible = true;
                    this.PnlDues.Visible = false;

                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    break;
                case "2":
                    dt = ((DataTable)ds1.Tables[2]).Copy();
                    this.Data_Bind("grvNetDues", dt);
                    this.pnlgvDayWSale.Visible = false;
                    this.pnlinprocess.Visible = false;
                    this.PnlDues.Visible = true;
                    
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";


                    break;

               
            }
        }
        private void Data_Bind(string gv, DataTable dt)
        {

            try
            {
                switch (gv)
                {
                   
                    case "gvDayWSale":

                        this.gvDayWSale.DataSource = HiddenSameData(dt);
                        this.gvDayWSale.DataBind();
                        if (dt.Rows.Count > 0)
                        {

                            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFbillamt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(billamt)", ""))
                                    ? 0.00
                                    : dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                            //((HyperLink)this.gvDayWSale.HeaderRow.FindControl("hyconlbtn")).NavigateUrl =
                            //    "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=&Date1=";

                        }
                        break;
                    case "grvTrnDatWise":

                        this.grvTrnDatWise.DataSource = HiddenSameData(dt);
                        this.grvTrnDatWise.DataBind();
                        if (dt.Rows.Count > 0)
                        {
                            double cashamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0 : dt.Compute("sum(cashamt)", "")));
                            double chqamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chqamt)", "")) ? 0 : dt.Compute("sum(chqamt)", "")));


                            ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFCashamt")).Text = cashamt.ToString("#,##0;(#,##0); ");
                            ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFChqamt")).Text = chqamt.ToString("#,##0;(#,##0); ");
                            ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvCDNetTotal")).Text = (cashamt + chqamt).ToString("#,##0;(#,##0); ");

                        }
                        break;
                    case "grvNetDues":

                        this.grvNetDues.DataSource = HiddenSameData(dt);
                        this.grvNetDues.DataBind();
                        if (dt.Rows.Count > 0)
                        {
                            double billamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0 : dt.Compute("sum(billamt)", "")));
                            double paidamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ? 0 : dt.Compute("sum(paidamt)", "")));
                            double dueamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueamt)", "")) ? 0 : dt.Compute("sum(dueamt)", "")));


                            ((Label)this.grvNetDues.FooterRow.FindControl("lgvFbillamt")).Text = billamt.ToString("#,##0;(#,##0); ");
                            ((Label)this.grvNetDues.FooterRow.FindControl("lgvFpaidamt")).Text = paidamt.ToString("#,##0;(#,##0); ");
                            ((Label)this.grvNetDues.FooterRow.FindControl("lgvFdueamt")).Text = dueamt.ToString("#,##0;(#,##0); ");

                        }
                        break;


                }
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }




        }

        private DataTable HiddenSameData(DataTable dt1)
        {




            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }

            return dt1;
        }

        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////if (e.Row.RowType == DataControlRowType.DataRow)
            ////{
            ////    HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyOrderPrint");
            ////    Hashtable hst = (Hashtable)Session["tblLogin"];
            ////    string comcod = hst["comcod"].ToString();
            ////    string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
            ////    string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();

            ////    hlink1.NavigateUrl = "~/F_23_SaM/Print?Type=OrderPrint&comcod=" + comcod + "&centrid=" + centrid + "&orderno=" + orderno;
            ////}
        }
       


    }
}