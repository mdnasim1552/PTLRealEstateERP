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
    public partial class LinkSubContractorSd02 : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //this.ink
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = " SUB CONTRACTOR BILL  (R/A Wise)";

                this.lblActDesc.Text = this.Request.QueryString["ssirdesc"].ToString();
                this.txtDate.Text = Convert.ToDateTime(this.Request.QueryString["date1"]).ToString("dd-MMM-yyyy");
                this.ShowBillDetails();


            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            this.ShowBillDetails();




        }

        private void ShowBillDetails()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string SubconName = this.Request.QueryString["ssircode"].ToString().Trim();
            string Pactcode = this.Request.QueryString["pactcode"].ToString().Trim();
            string date = this.Request.QueryString["Date1"].ToString().Trim();
            //string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "RPTSUBCONSDDETAILS03", Pactcode, SubconName, date, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //Session["tblconsddetails"] = HiddenSameData(ds1.Tables[0]);
            Session["tblconsddetails"] = ds1.Tables[0];
            this.Data_Bind();

        }



        private void Data_Bind()
        {


            DataTable dt = (DataTable)Session["tblconsddetails"];

            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();
            this.FooterCalculation(dt);


        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            else
            {
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                             dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFSecurityAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sdamt)", "")) ? 0.00 :
                    dt.Compute("sum(sdamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFdedAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dedamt)", "")) ? 0.00 :
                       dt.Compute("sum(dedamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPenaltyAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(penamt)", "")) ? 0.00 :
                       dt.Compute("sum(penamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFTotalAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayamt)", "")) ? 0.00 :
                                      dt.Compute("sum(netpayamt)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
                     dt.Compute("sum(payment)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 :
                      dt.Compute("sum(netpayable)", ""))).ToString("#,##0;(#,##0); ");


            }



        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            this.PrintBillDetails();

        }


        private void PrintBillDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptConSD = new RealERPRPT.R_09_PImp.RptSubConSD();
            TextObject rptCname = rptConSD.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;
            //TextObject rptpactdesc = rptConSD.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptpactdesc.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text.Substring(13);
            TextObject rptSubdesc = rptConSD.ReportDefinition.ReportObjects["SubConName"] as TextObject;
            rptSubdesc.Text = this.Request.QueryString["ssirdesc"].ToString();//this.ddlSubName.SelectedItem.Text.Substring(13); ;
            TextObject rptDate = rptConSD.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "Date: " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");//Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptConSD.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptConSD.SetDataSource((DataTable)Session["tblconsddetails"]);


            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptConSD.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptConSD;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }



    }
}


