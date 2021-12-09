using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_14_Pro
{
    public partial class RptPurchaseAgeingDetails : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);              
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Credit Status (Purchase)";
                //this.lblProjName.Text = this.Request.QueryString["SupName"].ToString();
                spanProjName.InnerText = this.Request.QueryString["SupName"].ToString() + " - " + this.setCatType();
                this.getSupplierInfo();

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string setCatType()
        {

            string Type = Request.QueryString["Type"].ToString();
            string comcod = this.GetCompCode();
            string title = "";
            switch (Type)
            {
                case "chqhamt":
                    title = "Cheque In Hand";
                    break;

                case "billacamt":
                    title = "Bill In ACC";
                    break;

                case "billiccdamt":
                    title = "Bill In ICCD";
                    break;

                case "billinpamt":
                    title = "Bill In Process";
                    break;

                case "mon4amt":
                    if (comcod == "3354" || comcod=="3101")
                    {
                        title = "Bill Days 60 Over";
                    }
                    else
                    {
                        title = "Bill Days 120 Over";
                    }                   
                    break;
                case "mon34amt":
                    if (comcod == "3354" || comcod == "3101")
                    {
                        title = "Bill Days 45 to 60 Days";
                    }
                    else
                    {
                        title = "Bill Days 90 to 120 Days";
                    }
                    break;
                case "mon13amt":
                    if (comcod == "3354" || comcod == "3101")
                    {
                        title = "Bill Days up to 30 Days";
                    }
                    else
                    {
                        title = "Bill Days up to 90 Days";
                    }
                    
                    break;
                default:
                    title = "";
                    break;

            }
            return title;

        }

        private void getSupplierInfo()
        {
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "chqhamt":
                case "billacamt":
                case "billiccdamt":
                case "billinpamt":
                    this.getSupplierData();
                    break;

                case "mon4amt":
                case "mon34amt":
                case "mon13amt":
                    this.getSupplierDataDatewise();
                    break;

            }

        }



        private void getSupplierData()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string type = this.Request.QueryString["Type"].ToString();
            string todate = this.Request.QueryString["todate"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "SUPPLIERDETAILSSTATUS", pactcode, type, todate, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvsupdetails.DataSource = null;
                this.gvsupdetails.DataBind();
                return;

            }
            Session["tblstatus"] = HiddenSameData(ds1.Tables[0]);

            this.Data_Bind();

        }



        private void getSupplierDataDatewise()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string type = this.Request.QueryString["Type"].ToString();
            string todate = this.Request.QueryString["todate"].ToString();
            string day1 = "";
            string day2 = "";
            string day3 = "";
            // string 
            switch (comcod)
            {
                case "3101":
                case "3354":
                    day1 = "30";
                    day2 = "45";
                    day3 = "60";
                    break;

                default: // For Rupayan
                    day1 = "30";
                    day2 = "90";
                    day3 = "120";
                    break;
            }


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "SUPPLIERDETAILSDAYWISE", pactcode, type, todate, day1, day2, day3, "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvsupdayswise.DataSource = null;
                this.gvsupdayswise.DataBind();
                return;

            }

            Session["tblstatusdays"] = HiddenSameData(ds1.Tables[0]);

            this.Data_Bind_Days();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            //DataView dv = dt1.DefaultView;
            //dv.Sort = "reqno";
            //dt1 = dv.ToTable();
            //string rsircode = dt1.Rows[0]["rsircode"].ToString();
            string pactcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                }


                else
                    pactcode = dt1.Rows[j]["actcode"].ToString();
            }

            return dt1;
        }

        private void Data_Bind()
        {
            this.MultiView1.ActiveViewIndex = 0;
            this.gvsupdetails.DataSource = (DataTable)Session["tblstatus"];
            this.gvsupdetails.DataBind();

            this.FooterCalculation();
            this.VisibleGrid();


        }
       
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblstatus"];

            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvsupdetails.FooterRow.FindControl("lblFtrnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ? 0.00 :
                 dt.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0); ");


            Session["Report1"] = gvsupdetails;
            ((HyperLink)this.gvsupdetails.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        private void Data_Bind_Days()
        {
            this.MultiView1.ActiveViewIndex = 1;
            this.gvsupdayswise.DataSource = (DataTable)Session["tblstatusdays"];
            this.gvsupdayswise.DataBind();

            this.FooterCalculationDays();

        }


        private void FooterCalculationDays()
        {
            DataTable dt = (DataTable)Session["tblstatusdays"];

            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvsupdayswise.FooterRow.FindControl("lblFdtrnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ? 0.00 :
                 dt.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0); ");


            Session["Report1"] = gvsupdayswise;
            ((HyperLink)this.gvsupdayswise.HeaderRow.FindControl("hlbtntbCdataExceld")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        private void VisibleGrid()
        {

            string Type = Request.QueryString["Type"].ToString();

            switch (Type)
            {
                case "chqhamt":
                    this.gvsupdetails.Columns[7].Visible = false;
                    this.gvsupdetails.Columns[8].Visible = false;
                    this.gvsupdetails.Columns[9].Visible = false;
                    this.gvsupdetails.Columns[10].Visible = false;
                    this.gvsupdetails.Columns[11].Visible = false;
                    break;

                case "billacamt":
                case "billiccdamt":
                    this.gvsupdetails.Columns[4].Visible = false;
                    this.gvsupdetails.Columns[5].Visible = false;
                    this.gvsupdetails.Columns[6].Visible = false;
                    this.gvsupdetails.Columns[9].Visible = false;
                    this.gvsupdetails.Columns[10].Visible = false;
                    this.gvsupdetails.Columns[11].Visible = false;
                    break;

                case "billinpamt":
                    this.gvsupdetails.Columns[3].Visible = false;
                    this.gvsupdetails.Columns[4].Visible = false;
                    this.gvsupdetails.Columns[5].Visible = false;
                    this.gvsupdetails.Columns[6].Visible = false;
                    this.gvsupdetails.Columns[7].Visible = false;
                    this.gvsupdetails.Columns[8].Visible = false;
                    this.gvsupdetails.Columns[12].Visible = false;
                    break;
                default:
                    break;

            }

        }

    }
}