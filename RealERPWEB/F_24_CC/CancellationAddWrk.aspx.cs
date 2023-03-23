using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_24_CC
{
    public partial class CancellationAddWrk : System.Web.UI.Page
    {
        ProcessAccess CcData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtFDate.Text = System.DateTime.Today.AddMonths(0).ToString("dd-MMM-yyyy");
                this.txtFDate.Text = "01" + this.txtFDate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtFDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void Data_Bind()
        {
            DataTable dt = (DataTable)Session["CancellationWrk"];
            this.grvCancellationWrk.PageSize = 10;
            this.grvCancellationWrk.DataSource = dt;
            this.grvCancellationWrk.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = GetComeCode();

            string fromDate = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = CcData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETCANCELLATIONWORK", fromDate, toDate);
            Session["CancellationWrk"] = ds1.Tables[0];
            ds1.Dispose();
            this.Data_Bind();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string fromDate = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["CancellationWrk"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.CancellationAddWork>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_24_CC.CancellationAddWork", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Cancellation Additional Work"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("Fromdate", fromDate));
            Rpt1.SetParameters(new ReportParameter("ToDate", toDate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "&embedded=true', target='_blank');</script>";

        }
    }
}