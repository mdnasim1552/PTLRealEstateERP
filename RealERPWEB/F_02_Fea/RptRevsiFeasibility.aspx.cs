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
    public partial class RptRevsiFeasibility : System.Web.UI.Page
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

                this.ProjectName();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString().Trim() == "PrjInfo") ? "Project Feasibility Project Information" : ((Request.QueryString["Type"].ToString().Trim() == "Cost") ? "Project Feasibility Cost Information" : "Project Feasibility Revenue Information");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

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

        private void ProjectName()
        {
            string comcod = this.GetComCode();
            string Filter1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "GETPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ProjectName();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {

            this.lbtnOk.Text = "OK";
            this.Load_Grid();
            return;


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            ReportDocument rpcp = new RealERPRPT.R_02_Fea.RptReviseFea();

            DataTable dt = (DataTable)ViewState["tblfeaprjInfo"];

            TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;

            if (ASTUtility.Left(comcod, 1) != "2")
            {

                TextObject txtPrjInfo = rpcp.ReportDefinition.ReportObjects["txtPrjInfo"] as TextObject;
                txtPrjInfo.Text = dt.Rows.Count > 0 ? "Storied: " + Convert.ToDouble(dt.Rows[0]["prjinfo"]).ToString("#,##0;(#,##0); ") : "";
                TextObject txtPrjAdd = rpcp.ReportDefinition.ReportObjects["txtPrjAdd"] as TextObject;
                txtPrjAdd.Text = dt.Rows.Count > 0 ? "Address: " + dt.Rows[0]["prjadd"].ToString() : "";
                TextObject txtPtype = rpcp.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
                txtPtype.Text = dt.Rows.Count > 0 ? "Project Type: " + dt.Rows[0]["prjtyp"].ToString() : "";




                TextObject txtLand = rpcp.ReportDefinition.ReportObjects["txtLand"] as TextObject;
                txtLand.Text = " Land Owner:  " + this.lblLownerval.Text;
                TextObject txtComp = rpcp.ReportDefinition.ReportObjects["txtComp"] as TextObject;
                txtComp.Text = "Company:  " + this.lblCompanyval.Text;
                TextObject txtCFT = rpcp.ReportDefinition.ReportObjects["txtCFT"] as TextObject;
                txtCFT.Text = "Cost/SFT:  " + this.lblCFTVal.Text;

                TextObject txtDuration = rpcp.ReportDefinition.ReportObjects["txtDuration"] as TextObject;
                txtDuration.Text = "Project Duration:  " + this.lblDurationVal.Text;

                TextObject txtProfit = rpcp.ReportDefinition.ReportObjects["txtProfit"] as TextObject;
                txtProfit.Text = "Profit:  " + this.lblProfitVal.Text;

                TextObject txtProfitRev = rpcp.ReportDefinition.ReportObjects["txttRev"] as TextObject;
                txtProfitRev.Text = "Target Revenue:  " + this.lblTrevVal.Text;

                TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtDate.Text = "Date :" + System.DateTime.Now.ToString("dd-MMM-yyyy");
            }



            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        // protected void Load_Grid(object sender, EventArgs e)
        protected void Load_Grid()
        {
            string comcod = this.GetComCode();

            string qtype = this.Request.QueryString["Type"];
            //int rindex = this.rbtnList1.SelectedIndex;
            switch (qtype)
            {
                case "RevFea":
                case "RevFeaCL":
                    this.ShowRevFeaReport();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }

        }
        private void ShowRevFeaReport()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string qtype = this.Request.QueryString["Type"];
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '83%'";
            string CostOrSale = "82%";
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string stpro = (qtype == "RevFeaCL") ? "SP_REPORT_FEA_PROFEASIBILITY_02" : "SP_REPORT_FEA_PROFEASIBILITY";
            string CallType = (qtype == "RevFeaCL") ? "RPT_REVFEABIBILITY02" : "RPT_REVFEABIBILITY";
            DataSet ds2 = feaData.GetTransInfo(comcod, stpro, CallType, pactcode, Code, CostOrSale, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvAreaCal.DataSource = null;
                this.gvAreaCal.DataBind();
                this.gvDevShare.DataSource = null;
                this.gvDevShare.DataBind();
                this.gvSales.DataSource = null;
                this.gvSales.DataBind();
                this.gvSold.DataSource = null;
                this.gvSold.DataBind();
                this.gvUnsold.DataSource = null;
                this.gvUnsold.DataBind();
                this.gvVari.DataSource = null;
                this.gvVari.DataBind();
                this.gvPrjStatus.DataSource = null;
                this.gvPrjStatus.DataBind();
                this.gvInSta.DataSource = null;
                this.gvInSta.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblfeaprjInfo"] = ds2.Tables[2];

            this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblProfitVal.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["profit"]).ToString("#,##0;(#,##0); ") + "%";
            this.lblCFTVal.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["cpsft"]).ToString("#,##0;(#,##0); ");
            this.lblDurationVal.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["dration"]).ToString("#,##0;(#,##0); ") + " Month";
            this.lblTrevVal.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["trev"]).ToString("#,##0;(#,##0); ");

            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string comcod = this.GetComCode();
            string qtype = this.Request.QueryString["Type"];
            DataTable dt1;
            DataView dv;
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            switch (qtype)
            {
                case "RevFea":
                case "RevFeaCL":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("grp = 'AAAA'");
                    dt1 = dv.ToTable();
                    this.gvAreaCal.DataSource = dt1;
                    this.gvAreaCal.DataBind();

                    dv = dt.DefaultView;
                    dv.RowFilter = ("grp = 'BBBB'");
                    dt1 = dv.ToTable();
                    this.gvDevShare.DataSource = dt1;
                    this.gvDevShare.DataBind();


                    dv = dt.DefaultView;
                    dv.RowFilter = ("grp = 'CCCC'");
                    dt1 = dv.ToTable();
                    this.gvSales.DataSource = dt1;
                    this.gvSales.DataBind();
                    if (qtype == "RevFeaCL")
                    {
                        this.gvSales.Columns[4].Visible = true;
                        this.gvSales.Columns[5].Visible = true;
                    }

                    dv = dt.DefaultView;
                    dv.RowFilter = ("grp = 'DDDD'");
                    dt1 = dv.ToTable();
                    this.gvSold.DataSource = dt1;
                    this.gvSold.DataBind();

                    dv = dt.DefaultView;
                    dv.RowFilter = ("grp = 'EEEE'");
                    dt1 = dv.ToTable();
                    this.gvUnsold.DataSource = dt1;
                    this.gvUnsold.DataBind();

                    dv = dt.DefaultView;
                    dv.RowFilter = ("grp = 'FFFF'");
                    dt1 = dv.ToTable();
                    this.gvVari.DataSource = dt1;
                    this.gvVari.DataBind();

                    dv = dt.DefaultView;
                    dv.RowFilter = ("grp = 'GGGG'");
                    dt1 = dv.ToTable();
                    this.gvPrjStatus.DataSource = dt1;
                    this.gvPrjStatus.DataBind();


                    dv = dt.DefaultView;
                    dv.RowFilter = ("grp = 'HHHH'");
                    dt1 = dv.ToTable();
                    this.gvInSta.DataSource = dt1;
                    this.gvInSta.DataBind();
                    break;
            }
        }


        protected void gvAreaCal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCode = (Label)e.Row.FindControl("lblgvItmCodfcs");
                Label lgvItemdesc3 = (Label)e.Row.FindControl("lgvItemdesc3");
                Label lgvtssfts = (Label)e.Row.FindControl("lgvtssfts");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    lgvItemdesc3.Font.Bold = true;
                    lgvtssfts.Font.Bold = true;
                    lgvItemdesc3.Style.Add("text-align", "right");
                    lblCode.Visible = false;
                }

            }
        }
        protected void gvDevShare_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCode = (Label)e.Row.FindControl("lblgvItmCod");
                Label lgvItemdesc = (Label)e.Row.FindControl("lgvItemdesc");
                Label lblgvSize = (Label)e.Row.FindControl("lblgvSize");
                Label lgvlowner = (Label)e.Row.FindControl("lgvlowner");
                Label lgvcompany = (Label)e.Row.FindControl("lgvcompany");
                Label lgvtotalcompany = (Label)e.Row.FindControl("lgvtotalcompany");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    lgvItemdesc.Font.Bold = true;
                    lblgvSize.Font.Bold = true;
                    lgvlowner.Font.Bold = true;
                    lgvcompany.Font.Bold = true;
                    lgvtotalcompany.Font.Bold = true;
                    lgvItemdesc.Style.Add("text-align", "right");
                    lblCode.Visible = false;
                }

            }
        }
        protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCode = (Label)e.Row.FindControl("lblgvItmCodsalrev");
                Label lgvItemdesc4 = (Label)e.Row.FindControl("lgvItemdesc4");
                Label lbllsizes = (Label)e.Row.FindControl("lbllsizes");
                Label lgvamtsalrev = (Label)e.Row.FindControl("lgvamtsalrev");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    lgvItemdesc4.Font.Bold = true;
                    lbllsizes.Font.Bold = true;
                    lgvamtsalrev.Font.Bold = true;
                    lgvItemdesc4.Style.Add("text-align", "right");
                    lblCode.Visible = false;
                }

            }
        }
        protected void gvSold_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCode = (Label)e.Row.FindControl("lblgvItmCodsoldinfo");
                Label lblinfdesc = (Label)e.Row.FindControl("lblinfdesc");
                Label lbllsizes = (Label)e.Row.FindControl("lbllsizes");
                Label lblgvcontosft = (Label)e.Row.FindControl("lblgvcontosft");
                Label lblgvlsizes = (Label)e.Row.FindControl("lblgvlsizes");
                Label lblgvtsizes = (Label)e.Row.FindControl("lblgvtsizes");
                //Label lblgvstonum = (Label)e.Row.FindControl("lblgvstonum");
                //Label lblgvtcsizes = (Label)e.Row.FindControl("lblgvtcsizes");
                //Label lblgvbqty = (Label)e.Row.FindControl("lblgvbqty");
                Label lblgvtamt = (Label)e.Row.FindControl("lblgvtamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    lblinfdesc.Font.Bold = true;
                    lbllsizes.Font.Bold = true;
                    lblgvcontosft.Font.Bold = true;
                    lblgvlsizes.Font.Bold = true;
                    lblgvtsizes.Font.Bold = true;
                    //lblgvstonum.Font.Bold = true;
                    //lblgvtcsizes.Font.Bold = true;
                    //lblgvbqty.Font.Bold = true;
                    lblgvtamt.Font.Bold = true;
                    lblinfdesc.Style.Add("text-align", "right");
                    lblCode.Visible = false;
                }

            }
        }
        protected void gvUnsold_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblCode = (Label)e.Row.FindControl("lblgvItmCodsoldinfo");
                Label lblinfdesc = (Label)e.Row.FindControl("lblinfdesc");
                Label lbllsizes = (Label)e.Row.FindControl("lbllsizes");
                Label lblgvcontosft = (Label)e.Row.FindControl("lblgvcontosft");
                Label lblgvlsizes = (Label)e.Row.FindControl("lblgvlsizes");
                Label lblgvtsizes = (Label)e.Row.FindControl("lblgvtsizes");
                //Label lblgvstonum = (Label)e.Row.FindControl("lblgvstonum");
                //Label lblgvtcsizes = (Label)e.Row.FindControl("lblgvtcsizes");
                //Label lblgvbqty = (Label)e.Row.FindControl("lblgvbqty");
                Label lblgvtamt = (Label)e.Row.FindControl("lblgvtamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    lblinfdesc.Font.Bold = true;
                    lbllsizes.Font.Bold = true;
                    lblgvcontosft.Font.Bold = true;
                    lblgvlsizes.Font.Bold = true;
                    lblgvtsizes.Font.Bold = true;
                    //lblgvstonum.Font.Bold = true;
                    //lblgvtcsizes.Font.Bold = true;
                    //lblgvbqty.Font.Bold = true;
                    lblgvtamt.Font.Bold = true;
                    lblinfdesc.Style.Add("text-align", "right");
                    lblCode.Visible = false;
                }

            }
        }
        protected void gvInSta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblCode = (Label)e.Row.FindControl("lblgvItmCodsoldinfo");
                Label lblinfdesc = (Label)e.Row.FindControl("lblinfdesc");
                Label lblgvcontosft = (Label)e.Row.FindControl("lblgvcontosft");
                Label lblgvlsizes = (Label)e.Row.FindControl("lblgvlsizes");
                Label lblgratesoldinfo = (Label)e.Row.FindControl("lblgratesoldinfo");
                Label lblgvtsizes = (Label)e.Row.FindControl("lblgvtsizes");
                Label lblgvstonum = (Label)e.Row.FindControl("lblgvstonum");
                Label lblgvtcsizes = (Label)e.Row.FindControl("lblgvtcsizes");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    lblinfdesc.Font.Bold = true;
                    lblgratesoldinfo.Font.Bold = true;
                    lblgvcontosft.Font.Bold = true;
                    lblgvlsizes.Font.Bold = true;
                    lblgvtsizes.Font.Bold = true;
                    lblgvstonum.Font.Bold = true;
                    lblgvtcsizes.Font.Bold = true;
                    lblinfdesc.Style.Add("text-align", "right");
                    lblCode.Visible = false;
                }

            }
        }
    }
}