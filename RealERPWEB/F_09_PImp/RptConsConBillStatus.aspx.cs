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
using RealEntity;
namespace RealERPWEB.F_09_PImp
{
    public partial class RptConsConBillStatus : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sub-Contractor Bill(All in One)";
                this.Master.Page.Title = "Sub-Contractor Bill(All in One)";
                //  this.txtDateto.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                string date1 = this.Request.QueryString["Date1"];
                string date2 = this.Request.QueryString["Date2"];
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = date1.Length > 0 ? date1 : Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = date2.Length > 0 ? date2 : System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.SectionView();
                this.GetProjectName();


            }

        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SectionView()
        {

            string type = this.Request.QueryString["Type"];

            switch (type)
            {
                case "ConBill":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


                default:
                    break;


            }


        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            // New Row Add
            DataTable dt = ds1.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["comcod"] = this.GetCompCode();
            dr1["pactcode"] = "000000000000";
            dr1["pactdesc"] = "All Project";
            dt.Rows.Add(dr1);
            DataView dv = dt.DefaultView;
            dv.Sort = ("pactcode");
            dt = dv.ToTable();
            //
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = dt;
            this.ddlProjectName.DataBind();
            if (Request.QueryString["prjcode"].Length > 0)
            {
                ddlProjectName.SelectedValue = Request.QueryString["prjcode"].ToString();
                ddlProjectName.Enabled = false;
            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string type = this.ddlReport.SelectedValue.ToString();

            switch (type)
            {
                case "ConSummary":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowConBillSummary();
                    break;
                case "ConBill":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowConBill();
                    break;

                case "ConPayment":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowConPayment();
                    break;


                default:
                    break;


            }

        }


        private void ShowConBillSummary()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTSUBCONSUMMARY", pactcode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvconbillsum.DataSource = null;
                this.gvconbillsum.DataBind();
                return;

            }

            // DataTable dt=this.HiddenSamaData(ds1.Tables[0])

            Session["billstatus"] = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.SubConBill.EClassConBillSummary>();
            this.Data_Bind();

        }

        private void ShowConBill()
        {


            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTSUBCONSBILL", pactcode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvbillstatus.DataSource = null;
                this.gvbillstatus.DataBind();
                return;

            }

            // DataTable dt=this.HiddenSamaData(ds1.Tables[0])

            Session["billstatus"] = HiddenSameData(ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.SubConBill.EClassConBill>());
            this.Data_Bind();



        }

        private void ShowConPayment()
        {


            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "26" : "26" + this.ddlProjectName.SelectedValue.ToString().Substring(2)) + "%";
            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTSUBCONPAYMENT", pactcode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvbillstatus.DataSource = null;
                this.gvbillstatus.DataBind();
                return;

            }


            Session["billstatus"] = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.SubConBill.EClassConPayment>();
            this.Data_Bind();





        }


        private List<RealEntity.C_09_PIMP.SubConBill.EClassConBill> HiddenSameData(List<RealEntity.C_09_PIMP.SubConBill.EClassConBill> lst)
        {
            if (lst.Count == 0)
                return lst;
            int j = 0;
            string csircode = lst[0].csircode;
            foreach (var lst1 in lst)
            {
                if (j == 0)
                {
                    j++;

                }

                else if (lst1.csircode.ToString() == csircode)
                {
                    lst1.csirdesc = "";

                }



                csircode = lst1.csircode;




            }

            return lst;

        }


        private void Data_Bind()
        {

            string type = this.ddlReport.SelectedValue.ToString();

            switch (type)
            {
                case "ConSummary":
                    List<RealEntity.C_09_PIMP.SubConBill.EClassConBillSummary> lst = (List<RealEntity.C_09_PIMP.SubConBill.EClassConBillSummary>)Session["billstatus"];
                    this.gvconbillsum.DataSource = lst;
                    this.gvconbillsum.DataBind();
                    break;

                case "ConBill":
                    List<RealEntity.C_09_PIMP.SubConBill.EClassConBill> lstb = (List<RealEntity.C_09_PIMP.SubConBill.EClassConBill>)Session["billstatus"];
                    this.gvbillstatus.DataSource = lstb;
                    this.gvbillstatus.DataBind();
                    break;

                case "ConPayment":
                    List<RealEntity.C_09_PIMP.SubConBill.EClassConPayment> lstp = (List<RealEntity.C_09_PIMP.SubConBill.EClassConPayment>)Session["billstatus"];
                    this.gvpayment.DataSource = lstp;
                    this.gvpayment.DataBind();
                    break;




                default:
                    break;


            }


            this.FooterCalculation();
        }



        private void FooterCalculation()
        {


            string type = this.ddlReport.SelectedValue.ToString();
            switch (type)
            {

                case "ConSummary":

                    List<RealEntity.C_09_PIMP.SubConBill.EClassConBillSummary> lst = (List<RealEntity.C_09_PIMP.SubConBill.EClassConBillSummary>)Session["billstatus"];
                    if (lst.Count == 0)
                        return;
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvbillamtcsum")).Text = lst.Sum(l => l.billamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvbillaamtcsum")).Text = lst.Sum(l => l.billaamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvdedamtcsum")).Text = lst.Sum(l => l.dedamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvpenamtcsum")).Text = lst.Sum(l => l.penamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvadvamtcsum")).Text = lst.Sum(l => l.advamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvsecamtcsum")).Text = lst.Sum(l => l.sdamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvtaxamtcsum")).Text = lst.Sum(l => l.taxamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvvatamtcsum")).Text = lst.Sum(l => l.vatamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvnetamtcsum")).Text = lst.Sum(l => l.netamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvconbillsum.FooterRow.FindControl("lgvFgvpayamtcsum")).Text = lst.Sum(l => l.payamt).ToString("#,##0;(#,##0); ");


                    break;


                case "ConBill":
                    List<RealEntity.C_09_PIMP.SubConBill.EClassConBill> lstb = (List<RealEntity.C_09_PIMP.SubConBill.EClassConBill>)Session["billstatus"];
                    if (lstb.Count == 0)
                        return;
                    ((HyperLink)this.gvbillstatus.FooterRow.FindControl("hlnkgvFgvbillamt")).Text = lstb.Sum(l => l.billamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvbillaamt")).Text = lstb.Sum(l => l.billaamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvdedamt")).Text = lstb.Sum(l => l.dedamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvpenamt")).Text = lstb.Sum(l => l.penamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvadvamt")).Text = lstb.Sum(l => l.advamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvsecamt")).Text = lstb.Sum(l => l.sdamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvtaxamt")).Text = lstb.Sum(l => l.taxamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvvatamt")).Text = lstb.Sum(l => l.vatamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvnetamt")).Text = lstb.Sum(l => l.netamt).ToString("#,##0;(#,##0); ");

                    string pactcode = this.ddlProjectName.SelectedValue.ToString();

                    if (this.ddlProjectName.SelectedValue != "000000000000")
                        ((HyperLink)this.gvbillstatus.FooterRow.FindControl("hlnkgvFgvbillamt")).NavigateUrl =
                            "~/F_16_Bill/RptUpconVsSobCon.aspx?Type=Report&prjcode=" + pactcode + "&Date1=" + this.txtFDate.Text + "&Date2=" + this.txttoDate.Text;


                    break;



                case "ConPayment":
                    List<RealEntity.C_09_PIMP.SubConBill.EClassConPayment> lstp = (List<RealEntity.C_09_PIMP.SubConBill.EClassConPayment>)Session["billstatus"];
                    if (lstp.Count == 0)
                        return;
                    ((Label)this.gvpayment.FooterRow.FindControl("lgvFgvpayamt")).Text = lstp.Sum(l => l.payamt).ToString("#,##0;(#,##0); ");

                    break;

                default:
                    break;


            }


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string date = this.txtDateto.Text;
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string session = hst["session"].ToString();
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //DataTable dt = (DataTable)Session["billstatus"];

            //var rptlist = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.SubConBillStatus>();

            //LocalReport Rpt1a = new LocalReport();

            //Rpt1a = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubConBillStu", rptlist, null, null);
            //Rpt1a.EnableExternalImages = true;
            //Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1a.SetParameters(new ReportParameter("date", "Date :" + date));
            //Rpt1a.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //Session["Report1"] = Rpt1a;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvbillstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvbillstatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void gvbillstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlnkgvbillno = (HyperLink)e.Row.FindControl("hlnkgvbillno");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sirocde = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                string lisuno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lisuno")).ToString();

                if (code == "")
                {
                    return;
                }


                else
                {

                    hlnkgvbillno.NavigateUrl = "~/F_09_PImp/PurLabIssue.aspx?Type=Current&prjcode=" + code + "&sircode=" + sirocde +
                                   "&genno=" + lisuno;




                }




            }
        }
        protected void gvpayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                HyperLink hlnkgvvounum = (HyperLink)e.Row.FindControl("hlnkgvvounum");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                if (vounum == "")
                {
                    return;
                }


                else
                {

                    hlnkgvvounum.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;






                }

            }
        }
    }
}




