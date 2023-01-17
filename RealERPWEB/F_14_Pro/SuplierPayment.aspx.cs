using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRDLC;
using RealERPRPT;
namespace RealERPWEB.F_14_Pro
{
    public partial class SuplierPayment : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Payment Shedule";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSupplierName();
                this.GetProjectName();


            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);



        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            //string serch1 = "%" + this.txtSrcPro.Text.Trim () + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME_01", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }

        private void GetSupplierName()
        {

            string comcod = this.GetCompCode();
            //string serch1 = "%" + this.txtSrcSub.Text.Trim () + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "GETSUPPLIERNAME01", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "sirdesc";
            this.ddlSubName.DataValueField = "sircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();
            this.GetProjectName();



        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            double aprovamt = Convert.ToDouble(((Label)this.gvSupPayment.FooterRow.FindControl("lgvaprvamtF")).Text);
            string date = this.txtDate.Text;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblSuppay"];
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptSupPayment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_14_Pro.RptSupPayment", list, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(aprovamt), 2)));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printfooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            this.ShowPaymentDetails();

        }

        private void ShowPaymentDetails()
        {
            //Session.Remove ("tblconsddetails");
            string comcod = this.GetCompCode();
            string PactCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string SupplierName = (this.ddlSubName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSubName.SelectedValue.ToString() + "%";
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETSUPPLIERSCHEDULE", PactCode, SupplierName, date, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblSuppay"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string ssircode = dt1.Rows[0]["ssircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();
                    dt1.Rows[j]["ssirdesc"] = "";
                }

                else
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();
                }
            }
            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblSuppay"];
            this.gvSupPayment.DataSource = dt;
            this.gvSupPayment.DataBind();
            this.FooterCalculation(dt);

        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvSupPayment.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 :
                 dt.Compute("sum(netpayable)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvSupPayment.FooterRow.FindControl("lgvFBillpendAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pendingamt)", "")) ? 0.00 :
                 dt.Compute("sum(pendingamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvSupPayment.FooterRow.FindControl("lgvFtotalbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalpay)", "")) ? 0.00 :
                 dt.Compute("sum(totalpay)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvSupPayment.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aitamt)", "")) ? 0.00 :
                dt.Compute("sum(aitamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSupPayment.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aitamt)", "")) ? 0.00 :
                dt.Compute("sum(aitamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSupPayment.FooterRow.FindControl("lgvaprvamtF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aprovamt)", "")) ? 0.00 :
               dt.Compute("sum(aprovamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            Session["Report1"] = gvSupPayment;




        }

        private void Save_Value()
        {
            DataTable dt = (DataTable)Session["tblSuppay"];
            int rowindex;
            for (int i = 0; i < this.gvSupPayment.Rows.Count; i++)
            {
                string pactcode = ((Label)this.gvSupPayment.Rows[i].FindControl("lgvpactcode")).Text.Trim();
                string ssircode = ((Label)this.gvSupPayment.Rows[i].FindControl("lgvssircode")).Text.Trim();
                double aitamt = Convert.ToDouble("0" + ((TextBox)this.gvSupPayment.Rows[i].FindControl("lblpayment")).Text.Trim());
                double aitamtper = Convert.ToDouble("0" + ((TextBox)this.gvSupPayment.Rows[i].FindControl("lgvbillamt2")).Text.Trim());
                double aprovamt = Convert.ToDouble("0" + ((TextBox)this.gvSupPayment.Rows[i].FindControl("lgvaprvamt")).Text.Trim());



                rowindex = (this.gvSupPayment.PageSize) * (this.gvSupPayment.PageIndex) + i;
                dt.Rows[rowindex]["aitamt"] = aitamt;
                dt.Rows[rowindex]["aitper"] = aitamtper;
                dt.Rows[rowindex]["aprovamt"] = aprovamt;
                dt.Rows[rowindex]["ssircode"] = ssircode;
                dt.Rows[rowindex]["pactcode"] = pactcode;

            }

            Session["tblSuppay"] = dt;
        }
        protected void lbtnupdate_OnClick(object sender, EventArgs e)
        {
            this.Save_Value();
            DataTable dt = (DataTable)Session["tblSuppay"];
            string comcod = this.GetCompCode();

            bool result = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ssircode = dt.Rows[i]["ssircode"].ToString();
                double aitamt = Convert.ToDouble(dt.Rows[i]["aitamt"]);
                double aitper = Convert.ToDouble(dt.Rows[i]["aitper"]);
                double aprvamt = Convert.ToDouble(dt.Rows[i]["aprovamt"]);
                string pactcode = dt.Rows[i]["pactcode"].ToString();

                result = BgdData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE01", "UPDATESUPPAYMENT", pactcode, ssircode,
                    aitamt.ToString(), aitper.ToString(), aprvamt.ToString());



                if (!result)
                {

                    return;
                }

                 ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


            }
        }
    }
}