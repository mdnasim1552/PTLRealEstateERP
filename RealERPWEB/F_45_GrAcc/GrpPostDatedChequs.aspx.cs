using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_45_GrAcc
{
    public partial class GrpPostDatedChequs : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        public static int PageNumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblprintstk")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Post Dated Cheque Status";
                this.Master.Page.Title = "Post Dated Cheque Status";
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.AddYears(1).ToString("dd-MMM-yyyy");


                // string Type=this.Request.QueryString["Type"].ToString();

                this.lnkOk_Click(null, null);


            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void Refrsh()
        {

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void lnkOk_Click(object sender, EventArgs e)
        {
            PageNumber = 0;

            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

        }


        private void ShowData()
        {


            try
            {
                Session.Remove("tblMrr");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string voutype = "PV%";
                int startRow = PageNumber * 100;
                int endRow = (PageNumber + 1) * 100;
                string SrchChequeno = "%%";
                string BankName = "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_GROUP_MIS02", "GRPREPORTCHEQUEUPDATE", frmdate, todate, voutype, startRow.ToString(), endRow.ToString(), SrchChequeno, BankName, "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                Session["tblMrr"] = this.HiddenSameDate(ds1.Tables[0]);
                Session["tbltopage"] = ds1.Tables[1];
                this.Data_Bind();



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();


            this.CalculatrGridTotal();
            Session["Report1"] = dgv1;
            if (dt.Rows.Count > 0)
                ((HyperLink)this.dgv1.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            string pactcode = dt1.Rows[0]["actcode"].ToString();
            string cactcode = dt1.Rows[0]["cactcode"].ToString();
            string comcod = dt1.Rows[0]["comcod"].ToString();
            int j;
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();

                }


                if (dt1.Rows[j]["comcod"].ToString() == comcod)
                {
                    grp = dt1.Rows[j]["comcod"].ToString();
                    dt1.Rows[j]["comnam"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["comcod"].ToString();

                }

            }



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["actcode"].ToString() == pactcode) && (dt1.Rows[j]["cactcode"].ToString() == cactcode))
                {
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["cactdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["actcode"].ToString() == pactcode)
                        dt1.Rows[j]["actdesc"] = "";
                    if (dt1.Rows[j]["cactcode"].ToString() == cactcode)
                        dt1.Rows[j]["cactdesc"] = "";
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                }

            }
            return dt1;




        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)Session["tbltopage"];
            double cramt = Convert.ToDouble(((DataTable)Session["tbltopage"]).Rows[0]["cramt"]);
            ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = cramt.ToString("#,##0;-#,##0; ");
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblMrr"];
            string grandTotal = dt.AsEnumerable().Where(y => y.Field<string>("typesum") == "ZZZZ").Sum(x => x.Field<decimal>("cramt")).ToString("#,##0.00;(#,##0.00); ");

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.DayWiseissueCheek>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPostDatCheque", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("grandTotal", grandTotal));
            Rpt1.SetParameters(new ReportParameter("Date", "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "List of Post Dated Cheque"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");
                //Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "Z")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void imgBtnFirst_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = 0;
            this.ShowData();


        }



        protected void imgbtnSearchCheqNO_Click(object sender, EventArgs e)
        {

        }
    }
}


