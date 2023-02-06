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
using RealERPRPT;
namespace RealERPWEB.F_04_Bgd
{

    public partial class RptPrjFloorWise : System.Web.UI.Page
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

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Catagory Wise Material Details";
                //this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.GetSupplierName();
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
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_MIS01", "GETPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowData();


        }
        private void ShowData()
        {
            Session.Remove("tblflr");
            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "REPORTRESOURCEFLOORWISE", PactCode, "", "12", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblflr"] = ds1.Tables[0];
            Session["tblflbdesc"] = ds1.Tables[1];
            this.Data_Bind();

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblflr"];
            DataTable dt1 = (DataTable)Session["tblflbdesc"];


            double qty1, qty2, qty3, qty4, qty5, qty6, qty7, qty8, qty9, qty10, qty11, qty12, qty13, qty14, qty15, qty16, qty17, qty18, qty19, qty20;

            qty1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty1)", "")) ? 0.00 : dt.Compute("sum(qty1)", "")));
            qty2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty2)", "")) ? 0.00 : dt.Compute("sum(qty2)", "")));
            qty3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty3)", "")) ? 0.00 : dt.Compute("sum(qty3)", "")));
            qty4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty4)", "")) ? 0.00 : dt.Compute("sum(qty4)", "")));
            qty5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty5)", "")) ? 0.00 : dt.Compute("sum(qty5)", "")));
            qty6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty6)", "")) ? 0.00 : dt.Compute("sum(qty6)", "")));
            qty7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty7)", "")) ? 0.00 : dt.Compute("sum(qty7)", "")));
            qty8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty8)", "")) ? 0.00 : dt.Compute("sum(qty8)", "")));
            qty9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty9)", "")) ? 0.00 : dt.Compute("sum(qty9)", "")));
            qty10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty10)", "")) ? 0.00 : dt.Compute("sum(qty10)", "")));
            qty11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty11)", "")) ? 0.00 : dt.Compute("sum(qty11)", "")));
            qty12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty12)", "")) ? 0.00 : dt.Compute("sum(qty12)", "")));
            qty13 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty13)", "")) ? 0.00 : dt.Compute("sum(qty13)", "")));
            qty14 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty14)", "")) ? 0.00 : dt.Compute("sum(qty14)", "")));
            qty15 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty15)", "")) ? 0.00 : dt.Compute("sum(qty15)", "")));
            qty16 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty16)", "")) ? 0.00 : dt.Compute("sum(qty16)", "")));
            qty17 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty17)", "")) ? 0.00 : dt.Compute("sum(qty17)", "")));
            qty18 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty18)", "")) ? 0.00 : dt.Compute("sum(qty18)", "")));
            qty19 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty19)", "")) ? 0.00 : dt.Compute("sum(qty19)", "")));
            qty20 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty20)", "")) ? 0.00 : dt.Compute("sum(qty20)", "")));

            this.gvPrjflr.Columns[3].Visible = (qty1 != 0);
            this.gvPrjflr.Columns[4].Visible = (qty2 != 0);
            this.gvPrjflr.Columns[5].Visible = (qty3 != 0);
            this.gvPrjflr.Columns[6].Visible = (qty4 != 0);
            this.gvPrjflr.Columns[7].Visible = (qty5 != 0);
            this.gvPrjflr.Columns[8].Visible = (qty6 != 0);
            this.gvPrjflr.Columns[9].Visible = (qty7 != 0);
            this.gvPrjflr.Columns[10].Visible = (qty8 != 0);
            this.gvPrjflr.Columns[11].Visible = (qty9 != 0);
            this.gvPrjflr.Columns[12].Visible = (qty10 != 0);
            this.gvPrjflr.Columns[13].Visible = (qty11 != 0);
            this.gvPrjflr.Columns[14].Visible = (qty12 != 0);
            this.gvPrjflr.Columns[15].Visible = (qty13 != 0);
            this.gvPrjflr.Columns[16].Visible = (qty14 != 0);
            this.gvPrjflr.Columns[17].Visible = (qty15 != 0);
            this.gvPrjflr.Columns[18].Visible = (qty16 != 0);
            this.gvPrjflr.Columns[19].Visible = (qty17 != 0);
            this.gvPrjflr.Columns[20].Visible = (qty18 != 0);
            this.gvPrjflr.Columns[21].Visible = (qty19 != 0);
            this.gvPrjflr.Columns[22].Visible = (qty20 != 0);

            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                this.gvPrjflr.Columns[i + 3].HeaderText = dt1.Rows[i]["flrdes"].ToString();

            }

            this.gvPrjflr.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPrjflr.DataSource = dt;
            this.gvPrjflr.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblflr"];

            if (dt.Rows.Count == 0)
                return;
            //DataTable dt1 = (DataTable)Session["tblflbdesc"];

            //((Label)this.gvPrjflr.FooterRow.FindControl("lgvFtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toqty)", "")) ? 0.00 :
            //           dt.Compute("sum(toqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty1)", "")) ? 0.00 :
                dt.Compute("sum(qty1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty2)", "")) ? 0.00 :
                    dt.Compute("sum(qty2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty3)", "")) ? 0.00 :
                    dt.Compute("sum(qty3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty4)", "")) ? 0.00 :
                    dt.Compute("sum(qty4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty5)", "")) ? 0.00 :
                    dt.Compute("sum(qty5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty6)", "")) ? 0.00 :
                    dt.Compute("sum(qty6)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty7)", "")) ? 0.00 :
                    dt.Compute("sum(qty7)", ""))).ToString("#,##0.00;(#,##0.00); ");



            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty8)", "")) ? 0.00 :
                    dt.Compute("sum(qty8)", ""))).ToString("#,##0.00;(#,##0.00); ");


            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty9)", "")) ? 0.00 :
                    dt.Compute("sum(qty9)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty10)", "")) ? 0.00 :
                    dt.Compute("sum(qty10)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty11)", "")) ? 0.00 :
                    dt.Compute("sum(qty11)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty12)", "")) ? 0.00 :
                    dt.Compute("sum(qty12)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty13)", "")) ? 0.00 :
                    dt.Compute("sum(qty13)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty14)", "")) ? 0.00 :
                    dt.Compute("sum(qty14)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty15)", "")) ? 0.00 :
                    dt.Compute("sum(qty15)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty16)", "")) ? 0.00 :
                    dt.Compute("sum(qty16)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty17)", "")) ? 0.00 :
                    dt.Compute("sum(qty17)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty18)", "")) ? 0.00 :
                    dt.Compute("sum(qty18)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty19)", "")) ? 0.00 :
                    dt.Compute("sum(qty19)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPrjflr.FooterRow.FindControl("gvFbasement20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty20)", "")) ? 0.00 :
                    dt.Compute("sum(qty20)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvPrjflr.FooterRow.FindControl("lgvFtqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toqty)", "")) ? 0.00 :
                    dt.Compute("sum(toqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvPrjflr.FooterRow.FindControl("lgvFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0.00 :
                dt.Compute("sum(rptamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }

        protected void ddlpagesize_SelectedIndexChanged1(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            // ** ***Iqbal Nayan   
            // This Report Temporal Off Recommend by Emdad vai 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy"); // This Report Temporal Off Recommend by Emdad vai 
            LocalReport Rpt1 = new LocalReport();
            //Session["tblflr"] = ds1.Tables[0];
            //Session["tblflbdesc"] = ds1.Tables[1];
            DataTable dt = (DataTable)Session["tblflr"];
            DataTable dt1 = (DataTable)Session["tblflbdesc"];
            string qty1 = dt1.Rows[0]["flrdes"].ToString();
          
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.PrjFoorWise>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptPrjFloorWise", lst, null, null);
            Rpt1.EnableExternalImages = true;

            if (dt1.Rows.Count > 1)
            {
                string qty2 = dt1.Rows[1]["flrdes"].ToString();
                string qty3 = dt1.Rows[2]["flrdes"].ToString();
                string qty4 =  dt1.Rows[3]["flrdes"].ToString();
                string qty5 = dt1.Rows[4]["flrdes"].ToString();
                string qty6 = dt1.Rows[5]["flrdes"].ToString();
                string qty7 = dt1.Rows[6]["flrdes"].ToString();
                string qty8 = dt1.Rows[7]["flrdes"].ToString();
                string qty9 = dt1.Rows[8]["flrdes"].ToString();
                string qty10 = dt1.Rows[9]["flrdes"].ToString();
                string qty11 = dt1.Rows[10]["flrdes"].ToString();
                string qty12 = dt1.Rows[11]["flrdes"].ToString();
                string qty13 = dt1.Rows[12]["flrdes"].ToString();
                string qty14 = dt1.Rows[13]["flrdes"].ToString();
                Rpt1.SetParameters(new ReportParameter("qty2", qty2));
                Rpt1.SetParameters(new ReportParameter("qty3", qty3));
                Rpt1.SetParameters(new ReportParameter("qty4", qty4));
                Rpt1.SetParameters(new ReportParameter("qty5", qty5));
                Rpt1.SetParameters(new ReportParameter("qty6", qty6));
                Rpt1.SetParameters(new ReportParameter("qty7", qty7));
                Rpt1.SetParameters(new ReportParameter("qty8", qty8));
                Rpt1.SetParameters(new ReportParameter("qty9", qty9));
                Rpt1.SetParameters(new ReportParameter("qty10", qty10));
                Rpt1.SetParameters(new ReportParameter("qty11", qty11));
                Rpt1.SetParameters(new ReportParameter("qty12", qty12));
                Rpt1.SetParameters(new ReportParameter("qty13", qty13));
                Rpt1.SetParameters(new ReportParameter("qty14", qty14));

            }
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("qty1", qty1));
           
           
            //  Rpt1.SetParameters(new ReportParameter("date", "As On: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.Text.Substring(17)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "PROJECT FLOOR WISE"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //string comcod = this.GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //DataTable dt = (DataTable)Session["tblflr"];
            //var lst = dt.DataTableToList<RealEntity.C_04_Bgd.PrjFoorWise>();
            //LocalReport Rpt1 = new LocalReport();



            //Rpt1 = RealERPRDLC.RDLCAccountSetup.GetLocalReport("R_04_Bgd.RptPrjFloorWise", lst, null, null);


            ////Rpt1.SetParameters(new ReportParameter("title", "Contractor Bill Approval Sheet"));
            ////Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            ////Rpt1.SetParameters(new ReportParameter("txtcomname", comnam));

            ////Rpt1.SetParameters(new ReportParameter("bundno", bundno));
            ////Rpt1.SetParameters(new ReportParameter("date", date));
            ////Rpt1.SetParameters(new ReportParameter("footer", printFooter));

            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvPrjflr_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            this.gvPrjflr.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}
