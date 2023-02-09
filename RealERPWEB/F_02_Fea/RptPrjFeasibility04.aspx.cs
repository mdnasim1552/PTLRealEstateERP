
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
namespace RealERPWEB.F_02_Fea
{
    public partial class RptPrjFeasibility04 : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString().Trim() == "SoldUSold") ? "Sales Statement"
                //    : (Request.QueryString["Type"].ToString().Trim() == "PriceList02") ? "Price List 02" : "Feasibility Top Sheet";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.ViewSection();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ViewSection()
        {

            string qtype = this.Request.QueryString["Type"];

            switch (qtype)
            {
                case "SoldUSold":
                    this.chkconsolidate.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "GPNPALLPRO":
                    this.chkconsolidate.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "PriceList02":
                    this.lblGroup.Visible = true;
                    this.ddlRptGroup.Visible = true;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
            }

        }



        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {




            this.Load_Grid();


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string qtype = this.Request.QueryString["Type"];

            switch (qtype)
            {
                case "SoldUSold":
                    this.PrintSoldUnSold();
                    break;

                case "GPNPALLPRO":
                    this.PrintGPNPAllPro();
                    break;
                case "PriceList02":
                    this.PrintPriceList();
                    break;
            }



        }


        private void PrintSoldUnSold()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rpcp = new RealERPRPT.R_02_Fea.RptSolduSoldSum();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            CompName.Text = comname;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintGPNPAllPro()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rpcp = new RealERPRPT.R_02_Fea.RptGPNPAllProject();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            CompName.Text = comname;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintPriceList()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rpcp = new RealERPRPT.R_02_Fea.RptAllProPriceList();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtPrjName.Text = "Effected Date: " + this.txtDate.Text;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void Load_Grid()
        {

            string qtype = this.Request.QueryString["Type"];
            //int rindex = this.rbtnList1.SelectedIndex;
            switch (qtype)
            {
                case "SoldUSold":
                    this.ShowSoldUnsold();
                    break;

                case "GPNPALLPRO":
                    this.ShowAllProGPNP();
                    break;
                case "PriceList02":
                    this.ShowPriceList();
                    break;
            }
        }




        private void ShowSoldUnsold()
        {

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string date = this.txtDate.Text.Trim();
            string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_04", "RPTSOLDUSOLDSUMMARY", date, consolidate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvSoldUnSold.DataSource = null;
                this.gvSoldUnSold.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private void ShowAllProGPNP()
        {


            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string date = this.txtDate.Text.Trim();
            string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_04", "RPTALLPROCOSTASALE", date, consolidate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvgpnp.DataSource = null;
                this.gvgpnp.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }
        private void ShowPriceList()
        {

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_04", "RPTPRICELIST", mRptGroup, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPriceList02.DataSource = null;
                this.gvFeaPriceList02.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            if (dt1.Rows.Count == 0)
                return dt1;
            string qtype = this.Request.QueryString["Type"];
            string grp = "";

            switch (qtype)
            {
                case "SoldUSold":

                    break;

                case "GPNPALLPRO":
                    string pactcode1 = dt1.Rows[0]["pactcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {


                        if (dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                        {
                            dt1.Rows[j]["pactdesc1"] = "";
                        }


                        pactcode1 = dt1.Rows[j]["pactcode1"].ToString();

                    }
                    break;



                case "PriceList02":
                    grp = dt1.Rows[0]["grp"].ToString();
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["loc"] = "";
                            dt1.Rows[j]["ptype"] = "";
                            dt1.Rows[j]["hdate"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["grp"].ToString() == grp)
                                dt1.Rows[j]["grpdesc"] = "";

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";
                                dt1.Rows[j]["loc"] = "";
                                dt1.Rows[j]["ptype"] = "";
                                dt1.Rows[j]["hdate"] = "";
                            }


                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            grp = dt1.Rows[j]["grp"].ToString();
                        }

                    }

                    break;
            }
            return dt1;

        }

        private void Data_Bind()
        {

            string qtype = this.Request.QueryString["Type"];
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            switch (qtype)
            {
                case "SoldUSold":
                    this.gvSoldUnSold.DataSource = dt;
                    this.gvSoldUnSold.DataBind();
                    this.FooterCalCulation();
                    break;

                case "GPNPALLPRO":
                    this.gvgpnp.DataSource = dt;
                    this.gvgpnp.DataBind();
                    this.FooterCalCulation();
                    break;

                case "PriceList02":
                    this.gvFeaPriceList02.DataSource = dt;
                    this.gvFeaPriceList02.DataBind();
                    break;




            }
        }

        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            if (dt.Rows.Count == 0)
                return;
            string qtype = this.Request.QueryString["Type"];

            switch (qtype)
            {
                case "SoldUSold":
                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFtusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tusize)", "")) ?
                    0.00 : dt.Compute("Sum(tusize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFtparking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tpqty)", "")) ?
                               0.00 : dt.Compute("Sum(tpqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFtuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tuamt)", "")) ?
                               0.00 : dt.Compute("Sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFsusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(susize)", "")) ?
                           0.00 : dt.Compute("Sum(susize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFsparking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(spqty)", "")) ?
                               0.00 : dt.Compute("Sum(spqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFSuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(suamt)", "")) ?
                               0.00 : dt.Compute("Sum(suamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFunusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(unusize)", "")) ?
                           0.00 : dt.Compute("Sum(unusize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFunparking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(unpqty)", "")) ?
                               0.00 : dt.Compute("Sum(unpqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSoldUnSold.FooterRow.FindControl("lblgvFunuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(unuamt)", "")) ?
                               0.00 : dt.Compute("Sum(unuamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "GPNPALLPRO":

                    break;

                case "PriceList02":
                    break;

            }

        }



        protected void gvgpnp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvProjectgn");
                Label conarea = (Label)e.Row.FindControl("lblgvconarea");
                Label salamt = (Label)e.Row.FindControl("lblgvsaleamt");
                Label conscost = (Label)e.Row.FindControl("lblgvconsct");
                Label binterest = (Label)e.Row.FindControl("lblgvbinterest");
                Label lblgvtconabin = (Label)e.Row.FindControl("lblgvtconabin");
                Label lcost = (Label)e.Row.FindControl("lblgvlcost");
                Label lblgvcoffund = (Label)e.Row.FindControl("lblgvcoffund");
                Label lblgvtocfaland = (Label)e.Row.FindControl("lblgvtocfaland");
                Label adcost = (Label)e.Row.FindControl("lblgvadcost");

                Label tprcost = (Label)e.Row.FindControl("lblgvtprcost");
                Label gprofit = (Label)e.Row.FindControl("lblgvgp");

                Label ovrhead = (Label)e.Row.FindControl("lblgvovrhead");
                Label rfund = (Label)e.Row.FindControl("lblgvrfund");
                Label topcost = (Label)e.Row.FindControl("lblgvtopcost");
                Label tocost = (Label)e.Row.FindControl("lblgvtocost");
                Label nprofit = (Label)e.Row.FindControl("lblgvnp");
                Label peroncost = (Label)e.Row.FindControl("lblgvperoncost");
                Label peronsl = (Label)e.Row.FindControl("lblgvperonsl");
                Label npperoncost = (Label)e.Row.FindControl("lblgvnpperoncost");
                Label vnpperonsl = (Label)e.Row.FindControl("lblgvnpperonsl");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    conarea.Font.Bold = true;
                    salamt.Font.Bold = true;
                    conscost.Font.Bold = true;
                    binterest.Font.Bold = true;
                    lblgvtconabin.Font.Bold = true;
                    lcost.Font.Bold = true;
                    lblgvcoffund.Font.Bold = true;
                    lblgvtocfaland.Font.Bold = true;
                    adcost.Font.Bold = true;
                    tprcost.Font.Bold = true;
                    gprofit.Font.Bold = true;

                    ovrhead.Font.Bold = true;
                    rfund.Font.Bold = true;
                    topcost.Font.Bold = true;
                    nprofit.Font.Bold = true;
                    peroncost.Font.Bold = true;
                    peronsl.Font.Bold = true;
                    npperoncost.Font.Bold = true;
                    vnpperonsl.Font.Bold = true;

                    actdesc.Style.Add("text-align", "right");


                }

            }

        }

        protected void gvFeaPriceList02_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void ddlRptGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPriceList();
        }
    }
}