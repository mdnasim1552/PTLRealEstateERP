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
    public partial class RptProFeasibilityAll : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"])); this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString().Trim() == "FeInSumm") ? "Income Statement All Project(Summary)" :
                //    (Request.QueryString["Type"].ToString().Trim() == "FeTopSheet") ? "Feasibility Top Sheet" : "Feasibility Report-All Project(Summary)";
                this.ViewSection();



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void lbtnOk01_Click(object sender, EventArgs e)
        {
            this.ShowFeaAnaly();
        }
        private void ViewSection()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "FeaAllSum":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "FeInSumm":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "FeTopSheet":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.GetProject();
                    this.Panel1.Visible = false;
                    //this.ShowFeaTopSheet();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report: " + Type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        private void GetProject()
        {

            string comcod = this.GetCompCode();

            string filter = this.txtseachPrj.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_02", "GETPROJECTLISTALL", filter, "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.DropCheck1.DataSource = dt1;
            this.DropCheck1.DataTextField = "infdesc";
            this.DropCheck1.DataValueField = "infdesc";
            this.DropCheck1.DataBind();
            //this.DropCheck1.SelectedIndex = 1;
            ds1.Dispose();



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string qtype = this.Request.QueryString["Type"];
            ReportDocument rptProfea = new ReportDocument();

            if (qtype == "FeInSumm")

                rptProfea = new RealERPRPT.R_02_Fea.rptFeaInSt02();

            else if (qtype == "FeTopSheet")
                rptProfea = new RealERPRPT.R_02_Fea.rptFeaTSheet();

            else
                rptProfea = new RealERPRPT.R_02_Fea.rptBgdInSt();


            //rptProfea = (this.Request.QueryString["Type"] == "INSTATALLPRJSUM") ? (new RealERPRPT.R_02_Fea.rptFeaInSt02()) : (new RealERPRPT.R_02_Fea.rptBgdInSt());



            TextObject rpttxtCompanyName = rptProfea.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rpttxtCompanyName.Text = comnam;

            TextObject rpttxtDate = rptProfea.ReportDefinition.ReportObjects["TxtDate"] as TextObject;
            rpttxtDate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy"); ;

            TextObject txtuserinfo = rptProfea.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptProfea.SetDataSource((DataTable)Session["tbfeaall"]);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sold Info";
                string eventdesc = "Print Report Sold Inf";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptProfea.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptProfea;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void ShowFeaAnaly()
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "FeaAllSum":
                    this.ShowInStAllPro01();
                    break;
                case "FeInSumm":
                    this.ShowInStAllPro02();
                    break;

            }




        }

        private void ShowInStAllPro01()
        {

            string comcod = this.GetCompCode();
            //DataSet ds2 = CustData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITYALL", "", "", "", "", "", "", "", "", "");
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITYALL", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.grvFeallAnly.DataSource = null;
                this.grvFeallAnly.DataBind();

                return;
            }
            Session["tbfeaall"] = ds2.Tables[0];
            this.Data_Bind();


        }


        private void ShowInStAllPro02()
        {

            string comcod = this.GetCompCode();
            string date = this.txtDate.Text.Trim();
            string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "INSTATALLPRJSUM", date, consolidate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.grvInSt02.DataSource = null;
                this.grvInSt02.DataBind();

                return;
            }
            Session["tbfeaall"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void ShowFeaTopSheet()
        {

            string comcod = this.GetCompCode();
            string prjcode = "";
            string[] prj = this.DropCheck1.Text.Trim().Split(',');
            if (prj[0].Substring(0, 4) == "0000")
                prjcode = "";
            else
                foreach (string s1 in prj)
                    prjcode = prjcode + s1.Substring(0, 12);


            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_02", "RPT_FEATOPSHEET", prjcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.grvFeaTopSheet.DataSource = null;
                this.grvFeaTopSheet.DataBind();

                return;
            }
            Session["tbfeaall"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbfeaall"];
            if (dt.Rows.Count == 0)
                return;

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "FeaAllSum":
                    ((Label)this.grvFeallAnly.FooterRow.FindControl("lgvFRevenue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(revamt)", "")) ?
                                       0 : dt.Compute("sum(revamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvFeallAnly.FooterRow.FindControl("lgvFCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(costamt)", "")) ?
                                    0 : dt.Compute("sum(costamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvFeallAnly.FooterRow.FindControl("lgvFmargin")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(prolos)", "")) ?
                                        0 : dt.Compute("sum(prolos)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "FeInSumm":
                    //DataTable dt1 = dt.Copy();
                    //DataView dv = dt1.DefaultView;
                    //dv.RowFilter = ("infcod like'%AAAAAAAA%' or  infcod like'%BBBBBBBB%' or  infcod like'%CCCCCCCC%'");
                    //dt1 = dv.ToTable();
                    //((Label)this.grvInSt02.FooterRow.FindControl("lgvoFRevenue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(orevamt)", "")) ?
                    //                  0 : dt.Compute("sum(orevamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvInSt02.FooterRow.FindControl("logvoFCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ocostamt)", "")) ?
                    //                0 : dt.Compute("sum(ocostamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvInSt02.FooterRow.FindControl("logvoFmargin")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(omargin)", "")) ?
                    //                    0 : dt.Compute("sum(omargin)", ""))).ToString("#,##0;(#,##0); ");


                    //((Label)this.grvInSt02.FooterRow.FindControl("lgvFRevenue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(revamt)", "")) ?
                    //                   0 : dt.Compute("sum(revamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvInSt02.FooterRow.FindControl("lgvFCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(costamt)", "")) ?
                    //                0 : dt.Compute("sum(costamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvInSt02.FooterRow.FindControl("lgvFmargin")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(prolos)", "")) ?
                    //                    0 : dt.Compute("sum(prolos)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }




        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbfeaall"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "FeaAllSum":
                    this.grvFeallAnly.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvFeallAnly.DataSource = dt;
                    this.grvFeallAnly.DataBind();
                    this.FooterCalculation();
                    break;
                case "FeInSumm":
                    //this.grvInSt02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvInSt02.DataSource = dt;
                    this.grvInSt02.DataBind();

                    //for (int i = 0; i < grvInSt02.Rows.Count; i++)
                    //{
                    //    string usircode = ((Label)grvInSt02.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                    //    LinkButton lbtn1 = (LinkButton)grvInSt02.Rows[i].FindControl("lbtnusize");
                    //    if (lbtn1 != null)
                    //        if (lbtn1.Text.Trim().Length > 0)
                    //            lbtn1.CommandArgument = usircode;
                    //}

                    // this.FooterCalculation();
                    break;
                case "FeTopSheet":
                    this.grvFeaTopSheet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvFeaTopSheet.DataSource = dt;
                    this.grvFeaTopSheet.DataBind();
                    break;
            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        protected void grvFeallAnly_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvFeallAnly.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvInSt02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvInSt02.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvInSt02_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label lgvoRev = (Label)e.Row.FindControl("lgvoRev");
                Label logvCost = (Label)e.Row.FindControl("logvCost");
                Label logvmargin = (Label)e.Row.FindControl("logvmargin");
                Label lgvperorCost = (Label)e.Row.FindControl("lgvperorCost");
                Label lgvRev = (Label)e.Row.FindControl("lgvRev");
                Label lgvCost = (Label)e.Row.FindControl("lgvCost");
                Label lgvProfit = (Label)e.Row.FindControl("lgvProfit");
                Label lgvperCost = (Label)e.Row.FindControl("lgvperCost");
                Label lgvPerRev = (Label)e.Row.FindControl("lgvPerRev");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    lgvoRev.Font.Bold = true;
                    logvCost.Font.Bold = true;
                    logvmargin.Font.Bold = true;
                    lgvperorCost.Font.Bold = true;

                    lgvRev.Font.Bold = true;
                    lgvCost.Font.Bold = true;
                    lgvProfit.Font.Bold = true;
                    lgvperCost.Font.Bold = true;
                    lgvPerRev.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }

            }




            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string Actcode = ((Label)e.Row.FindControl("lgvInfoCode")).Text;
            string ActDesc = ((Label)e.Row.FindControl("lgvInfodesc")).Text;
            if (ASTUtility.Right(Actcode, 4) == "AAAA")
                return;
            hlink1.NavigateUrl = "RptLinkFeaIncomeSt.aspx?actcode=" + Actcode + "&actdesc=" + ActDesc;





        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataTable dt = (DataTable)Session["tbfeaall"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "FeInSumm":
                    {
                        string company = dt1.Rows[0]["company"].ToString();
                        for (int j = 1; j < dt1.Rows.Count; j++)
                        {



                            if (dt1.Rows[j]["company"].ToString() == company)
                            {
                                dt1.Rows[j]["companydesc"] = "";
                            }


                            company = dt1.Rows[j]["company"].ToString();


                        }
                        break;
                    }
                case "FeTopSheet":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string infcod1 = dt1.Rows[0]["infcod1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["duration"] = 0.00;
                            dt1.Rows[j]["storid"] = 0.00;
                            dt1.Rows[j]["cpsft"] = 0.00;
                            //dt1.Rows[j]["profit1"] = 0.00;


                        }
                        if (dt1.Rows[j]["infcod1"].ToString() == infcod1)
                        {
                            infcod1 = dt1.Rows[j]["infcod1"].ToString();
                            dt1.Rows[j]["iconarea"] = 0.00;


                        }


                        pactcode = dt1.Rows[j]["pactcode"].ToString();
                        infcod1 = dt1.Rows[j]["infcod1"].ToString();
                    }
                    break;
            }
            return dt1;


        }

        protected void grvFeaTopSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("grdesc");
                Label lgvMinRate = (Label)e.Row.FindControl("lgvMinRate");
                Label lgvMinamt = (Label)e.Row.FindControl("lgvMinamt");
                Label lgvsalarea = (Label)e.Row.FindControl("lgvsalarea");
                Label lgvperCost = (Label)e.Row.FindControl("lgvperCost");
                Label lgvprofit = (Label)e.Row.FindControl("lgvprofit");
                // Label lgvsalamt = (Label)e.Row.FindControl("lgvsalamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    lgvMinRate.Font.Bold = true;
                    lgvMinamt.Font.Bold = true;
                    lgvsalarea.Font.Bold = true;
                    lgvperCost.Font.Bold = true;
                    lgvprofit.Font.Bold = true;
                    //lgvsalamt.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }

            }

        }
        protected void ImgbtnFindPrj_Click(object sender, EventArgs e)
        {
            this.GetProject();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            this.ShowFeaTopSheet();
        }

    }
}