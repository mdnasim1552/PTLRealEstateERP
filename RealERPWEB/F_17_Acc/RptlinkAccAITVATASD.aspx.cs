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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptlinkAccAITVATASD : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "AIT, VAT & SD DEDECUTION INFORMATION";
                this.Master.Page.Title = "AIT, VAT & SD DEDECUTION INFORMATION";
                this.CompanyDayWise();
                this.ShowSupAITVatASD();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void CompanyDayWise()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3340":
                //case "3101":
                    this.Checkdaywise.Checked = true;
                    break;

                default:
                    break;

            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowSupAITVatASD();
        }
        protected void ShowSupAITVatASD()
        {
            Session.Remove("tblaitvatsd");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = this.Request.QueryString["frmdate"].ToString();
            string todate = this.Request.QueryString["todate"].ToString();
            string resource = this.Request.QueryString["rescode"].ToString();
            string SuporConName = this.Request.QueryString["subcode"].ToString() + "%";
            string ProjectName = "%";
            string daywise = this.Checkdaywise.Checked ? "daywise" : "";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTSUPPAITAVAT", frmdate, todate, resource, SuporConName, ProjectName, daywise, "", "", "");
            if (ds1 == null)
            {

                this.gvaitvsd.DataSource = null;
                this.gvaitvsd.DataBind();
                return;
            }
            DataTable dt = (this.Checkdaywise.Checked) ? ds1.Tables[0] : HiddenSameData(ds1.Tables[0]);
            dt = BalCalculationSp(dt);
            Session["tblaitvatsd"] = dt;
            this.lblvalfrmdate.Text = frmdate;
            this.lblvaltodate.Text = todate;
            this.lblValresDescription.Text = ds1.Tables[1].Rows[0]["resdesc"].ToString();
            this.lblValSubDescription.Text = ds1.Tables[1].Rows[0]["subdesc"].ToString();
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            this.gvaitvsd.DataSource = (DataTable)Session["tblaitvatsd"];
            this.gvaitvsd.DataBind();
            this.FooterCal();


        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum1"].ToString() == vounum))
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["vounum1"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {

                        dt1.Rows[j]["actdesc"] = "";
                    }

                    if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                    {

                        dt1.Rows[j]["vounum1"] = "";

                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                }





            }
            return dt1;

        }

        private DataTable BalCalculationSp(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double opnam, dramt, cramt, bbalamt = 0.00;

            bool result = this.Checkdaywise.Checked;
            switch (result)
            {
                case true:

                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if ((dr1["vounum"]).ToString().Trim() == "SUB TOTAL")
                            continue;
                        opnam = Convert.ToDouble(dr1["opam"]);
                        dramt = Convert.ToDouble(dr1["dram"]);
                        cramt = Convert.ToDouble(dr1["cram"]);
                        bbalamt = bbalamt + (opnam + dramt - cramt);
                        dr1["clsam"] = bbalamt;
                    }


                    break;


                default:
                    string actcode = dt.Rows[0]["actcode"].ToString();
                    //string grp=
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        if ((dt.Rows[i]["actcode"]).ToString().Trim() != actcode)
                        {
                            bbalamt = 0.00;
                        }
                        actcode = dt.Rows[i]["actcode"].ToString();

                        if ((dt.Rows[i]["vounum"]).ToString().Trim() == "SUB TOTAL")
                            continue;



                        //if (((dt.Rows[i]["actcode"]).ToString().Trim()).Length == 12)
                        //{
                        opnam = Convert.ToDouble(dt.Rows[i]["opam"]);
                        dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                        cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                        bbalamt = bbalamt + (opnam + dramt - cramt);
                        dt.Rows[i]["clsam"] = bbalamt;
                        //}


                    }

                    break;



            }


            return dt;








        }




        private void FooterCal()
        {
            DataTable dt = ((DataTable)Session["tblaitvatsd"]).Copy();
            if (dt.Rows.Count == 0)
                return;
            //double clsam =Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["clsam"]);
            double clsam = 0.00;
            bool result = this.Checkdaywise.Checked;
            DataView dv = dt.DefaultView;
            switch (result)
            {

                case true:
                    clsam = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["clsam"]);
                    break;

                default:
                    dv.RowFilter = "head1='03CT'";


                    clsam = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(clsam)", "")) ?
                      0 : dv.ToTable().Compute("sum(clsam)", "")));
                    break;
            }


            // DataView dv = dt.DefaultView;

            dt = dv.ToTable();
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFOpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
                    0 : dt.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                    0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                    0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFClsAmt")).Text = clsam.ToString("#,##0;(#,##0); ");
        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt1 = (DataTable)Session["tblaitvatsd"];
            DataTable dt = (DataTable)Session["tblaitvatsd"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "head1='03CT'";
            dt = dv.ToTable();

            string opam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
                    0 : dt.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
            string dram = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                    0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            string cram = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                    0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            string clsam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                    0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");

            string date = "( From: " + this.Request.QueryString["frmdate"].ToString() + "  To: " + this.Request.QueryString["todate"].ToString() + " )";

            string rpttitle = "AIT VAT & SD Report";
            string resource = this.lblValresDescription.Text.Trim();
            string suborconname = this.lblValSubDescription.Text;

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataView dv1 = dt1.Copy().DefaultView;
            dv1.RowFilter = ("grp='ZZ'");
            DataTable dt2 = dv1.ToTable();

            string sumop = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(opam)", "")) ? 0 
                    : dt2.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
            string sumdr = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(dram)", "")) ? 0 
                    : dt2.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            string sumcr = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(cram)", "")) ? 0 
                    : dt2.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            string sumclsm = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(clsam)", "")) ? 0
                    : dt2.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); "); 



            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptAitVatSdDeduction>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccAitVatSd", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));
            Rpt1.SetParameters(new ReportParameter("resource", resource));
            Rpt1.SetParameters(new ReportParameter("suborconname", suborconname));
            Rpt1.SetParameters(new ReportParameter("opam", opam));
            Rpt1.SetParameters(new ReportParameter("dram", dram));
            Rpt1.SetParameters(new ReportParameter("cram", cram));
            Rpt1.SetParameters(new ReportParameter("clsam", clsam));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("sumop", sumop));
            Rpt1.SetParameters(new ReportParameter("sumdr", sumdr));
            Rpt1.SetParameters(new ReportParameter("sumcr", sumcr));
            Rpt1.SetParameters(new ReportParameter("sumclsm", sumclsm));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void gvaitvsd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounum");
                //Label OpAmt = (Label)e.Row.FindControl("lblgvOpAmount");

                // lblgvvounum
                Label lblgvvounum = (Label)e.Row.FindControl("lblgvvounum");
                Label OpAmount = (Label)e.Row.FindControl("lblgvOpAmount");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmount");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmount");
                Label ClAmt = (Label)e.Row.FindControl("lblgvClAmount");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "head1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "03CT")
                {
                    lblgvvounum.Attributes["style"] = "font-weight:bold;";
                    OpAmount.Attributes["style"] = "font-weight:bold;";
                    DrAmt.Attributes["style"] = "font-weight:bold;";
                    CrAmt.Attributes["style"] = "font-weight:bold;";
                    ClAmt.Attributes["style"] = "font-weight:bold;";

                }
            }
        }
    }
}