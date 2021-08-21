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
    public partial class RptLinkRevenueACost : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewSection();
            }
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ViewSection()

        {
            string comcod = this.GetComCode();
            this.lblActDesc.Text = Request.QueryString["actdesc"].ToString().Trim();
            string qtype = this.Request.QueryString["Type"];

            switch (qtype)
            {

                case "Cost":

                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowCost();
                    break;
                case "Revenue":
                    if (ASTUtility.Left(comcod, 1) == "2")
                    {
                        this.ShowRevenueLand();
                        this.MultiView1.ActiveViewIndex = 2;
                    }
                    else
                    {
                        this.ShowRevenue();
                        this.MultiView1.ActiveViewIndex = 1;
                    }
                    break;
            }

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
            string prjname = this.lblActDesc.Text.Trim(); ;
            string pactcode = Request.QueryString["actcode"].ToString().Trim();
            string qtype = this.Request.QueryString["Type"];
            ReportDocument rpcp = new ReportDocument();
            DataSet dsrpt = new DataSet();


            if (qtype == "PrjInfo")
            {
                rpcp = new RealERPRPT.R_02_Fea.RptPrjFeasInfo();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                CompName.Text = comname;
                TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
                txtPrjName.Text = prjname;
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = this.lblHeader.Text;
                    string eventdesc = "Print Report";
                    string eventdesc2 = "";// this.rbtnList1.SelectedItem.ToString();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                rpcp.SetDataSource((DataTable)ViewState["tblprjinfo"]);
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;
            }
            if (qtype == "Cost")
            {
                string Code = "(infcod like '0[2-9]%'  or infcod like '1[0-9]%'  or infcod like '2[0-9]%' or infcod like '3[0-9]%'  or infcod like '4[0-9]%')";
                string CostOrSale = "81%";
                dsrpt = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "RPT_GETPROSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");
                if (dsrpt == null)
                {
                    return;
                }
                rpcp = new RealERPRPT.R_02_Fea.RptProjectFeasibility02();
                TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
                txtPrjName.Text = prjname;
                TextObject txtPrjInfo = rpcp.ReportDefinition.ReportObjects["txtPrjInfo"] as TextObject;
                txtPrjInfo.Text = dsrpt.Tables[1].Rows[0]["prjinfo"].ToString();
                TextObject txtPrjAdd = rpcp.ReportDefinition.ReportObjects["txtPrjAdd"] as TextObject;
                txtPrjAdd.Text = dsrpt.Tables[1].Rows[0]["prjadd"].ToString();
                TextObject txtPtype = rpcp.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
                txtPtype.Text = dsrpt.Tables[1].Rows[0]["prjtyp"].ToString();


                TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtDate.Text = "Date :" + System.DateTime.Now.ToString("dd-MMM-yyyy");


                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = this.lblHeader.Text;
                    string eventdesc = "Print Report";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                rpcp.SetDataSource((DataTable)dsrpt.Tables[0]);
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;
            }
            if (qtype == "Revenue")
            {
                DataSet dsrevpt;
                if (ASTUtility.Left(comcod, 1) == "2")
                {
                    string Code = "infcod like '81%'";
                    dsrevpt = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "RPT_GETPROREVENUELAND", pactcode, Code, "", "", "", "", "", "", "");
                    rpcp = new RealERPRPT.R_02_Fea.RptPrjFeasRevnue1();
                }
                else
                {
                    string Code = "infcod like '83%'";
                    string CostOrSale = "82%";
                    dsrevpt = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "RPT_GETPROREVENUE", pactcode, Code, CostOrSale, "", "", "", "", "", "");
                    rpcp = new RealERPRPT.R_02_Fea.RptPrjFeasRevnue();
                }
                if (dsrevpt == null)
                {
                    return;
                }
                TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
                txtPrjName.Text = prjname;
                TextObject txtPrjInfo = rpcp.ReportDefinition.ReportObjects["txtPrjInfo"] as TextObject;
                txtPrjInfo.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjinfo"].ToString() : "";
                TextObject txtPrjAdd = rpcp.ReportDefinition.ReportObjects["txtPrjAdd"] as TextObject;
                txtPrjAdd.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjadd"].ToString() : "";
                TextObject txtPtype = rpcp.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
                txtPtype.Text = dsrevpt.Tables[1].Rows.Count < 0 ? dsrevpt.Tables[1].Rows[0]["prjtyp"].ToString() : "";
                if (ASTUtility.Left(comcod, 1) != "2")
                {
                    TextObject txtLand = rpcp.ReportDefinition.ReportObjects["txtLand"] as TextObject;
                    txtLand.Text = "Land Owner Share:  " + Convert.ToDouble(dsrevpt.Tables[2].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + " %";
                    TextObject txtComp = rpcp.ReportDefinition.ReportObjects["txtComp"] as TextObject;
                    txtComp.Text = "Company Share:  " + Convert.ToDouble(dsrevpt.Tables[2].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + " %";

                    TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                    txtDate.Text = "Date :" + System.DateTime.Now.ToString("dd-MMM-yyyy");
                }

                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = this.lblHeader.Text;
                    string eventdesc = "Print Report";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                rpcp.SetDataSource((DataTable)dsrevpt.Tables[0]);
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;
            }

            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        private void ShowCost()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["actcode"].ToString().Trim();
            string Code = "(infcod like '0[2-9]%'  or infcod like '1[0-9]%'  or infcod like '2[0-9]%' or infcod like '3[0-9]%'  or infcod like '4[0-9]%')";
            string CostOrSale = "81%";
            string Label = "12";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROSALES02", pactcode, Code, CostOrSale, Label, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrjC.DataSource = null;
                this.gvFeaPrjC.DataBind();
                this.gvFeaPrjFC.DataSource = null;
                this.gvFeaPrjFC.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            this.gvFeaPrjFC.DataSource = ds2.Tables[1];
            this.gvFeaPrjFC.DataBind();
            if (ASTUtility.Left(comcod, 1) == "2")
            {
                this.gvFeaPrjFC.Columns[7].Visible = false;
            }
            if (ASTUtility.Left(comcod, 1) != "2")
            {
                this.gvFeaPrjFC.Columns[11].Visible = false;
            }
            ds2.Dispose();
            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(perent)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(perent)", ""))).ToString("#,##0.00;(#,##0.00); ") + " %";
            this.Data_Bind();
        }
        private void ShowRevenueLand()
        {


            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["actcode"].ToString().Trim();
            string Code = "infcod like '81%'";
            string Label = "12";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROSALESREVNUELAND", pactcode, Code, Label, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.grvSaleRevLand.DataSource = null;
                this.grvSaleRevLand.DataBind();
                this.grvSoldLand.DataSource = null;
                this.grvSoldLand.DataBind();
                this.grvUnSoldLAnd.DataSource = null;
                this.grvUnSoldLAnd.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            //this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            //this.gvFeaPrjFCS.DataBind();
            //if (ds2.Tables[1].Rows.Count != 0)
            //    ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");

            DataTable dt = ((DataTable)ds2.Tables[0]).Copy();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("infcod  like '0101%' or infcod  like '0102%' or infcod  like '0103%'");
            dt1 = dv.ToTable();
            this.grvSaleRevLand.DataSource = dt1;
            this.grvSaleRevLand.DataBind();


            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '0101%' or  infcod   like '0102%'");
            dt2 = dv.ToTable();

            if (dt1.Rows.Count != 0)
            {
                ((Label)this.grvSaleRevLand.FooterRow.FindControl("lgvFlsisessalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.grvSaleRevLand.FooterRow.FindControl("lgvFamtsalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
            }


            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '015100101%' or infcod   like '015100102%'");
            dt1 = dv.ToTable();
            this.grvSoldLand.DataSource = dt1;
            this.grvSoldLand.DataBind();

            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '015100101%'");
            dt2 = dv.ToTable();
            if (dt1.Rows.Count != 0)
            {
                ((Label)this.grvSoldLand.FooterRow.FindControl("lgvFlsisessoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.grvSoldLand.FooterRow.FindControl("lgvFamtsoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
            }

            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '015200101%' or infcod   like '015200102%' ");
            dt1 = dv.ToTable();
            this.grvUnSoldLAnd.DataSource = dt1;
            this.grvUnSoldLAnd.DataBind();
            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '015200101%'");
            dt2 = dv.ToTable();
            if (dt1.Rows.Count != 0)
            {
                ((Label)this.grvUnSoldLAnd.FooterRow.FindControl("lgvFlsisesusoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.grvUnSoldLAnd.FooterRow.FindControl("lgvFamtusoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
            }
            this.Data_Bind();
            ds2.Dispose();

        }
        private void ShowRevenue()
        {

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["actcode"].ToString().Trim();
            string Code = "infcod like '83%'";
            string CostOrSale = "82%";
            string mRptGroup = "12";
            // mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROSALESREVNUE02", pactcode, Code, CostOrSale, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrj.DataSource = null;
                this.gvFeaPrj.DataBind();
                this.gvFeaPrjFCS.DataSource = null;
                this.gvFeaPrjFCS.DataBind();
                return;
            }

            ViewState["tblfeaprj"] = ds2.Tables[0];
            this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            this.gvFeaPrjFCS.DataBind();
            if (ds2.Tables[1].Rows.Count != 0)
                ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");

            DataTable dt = ((DataTable)ds2.Tables[2]).Copy();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("infcod  like '0101%' or infcod  like '0102%' or infcod  like '0103%'");
            dt1 = dv.ToTable();
            this.gvFeaPrjsalrev.DataSource = dt1;
            this.gvFeaPrjsalrev.DataBind();


            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '0101%' or  infcod   like '0102%'");
            dt2 = dv.ToTable();

            if (dt1.Rows.Count != 0)
            {
                ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lgvFlsisessalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lgvFamtsalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
            }
            this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";


            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '015100101%' or infcod   like '015100102%'");
            dt1 = dv.ToTable();
            this.gvFeaPrjSoldInfo.DataSource = dt1;
            this.gvFeaPrjSoldInfo.DataBind();

            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '015100101%'");
            dt2 = dv.ToTable();
            if (dt1.Rows.Count != 0)
            {
                ((Label)this.gvFeaPrjSoldInfo.FooterRow.FindControl("lgvFlsisessoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjSoldInfo.FooterRow.FindControl("lgvFamtsoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
            }

            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '015200101%' or infcod   like '015200102%' ");
            dt1 = dv.ToTable();
            this.gvFeaPrjusoldinfo.DataSource = dt1;
            this.gvFeaPrjusoldinfo.DataBind();
            dv = dt.DefaultView;
            dv.RowFilter = ("infcod   like '015200101%'");
            dt2 = dv.ToTable();
            if (dt1.Rows.Count != 0)
            {
                ((Label)this.gvFeaPrjusoldinfo.FooterRow.FindControl("lgvFlsisesusoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjusoldinfo.FooterRow.FindControl("lgvFamtusoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
            }
            this.Data_Bind();
            ds2.Dispose();



            //ViewState.Remove("tblfeaprj");
            //string comcod = this.GetComCode();
            //string pactcode = Request.QueryString["actcode"].ToString().Trim();
            //string Code = "infcod like '83%'";
            //string CostOrSale = "82%";
            //string Label = "12";
            //DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROSALESREVNUE02", pactcode, Code, CostOrSale, Label, "", "", "", "", "");
            //if (ds2 == null)
            //{
            //    this.gvFeaPrj.DataSource = null;
            //    this.gvFeaPrj.DataBind();
            //    this.gvFeaPrjFCS.DataSource = null;
            //    this.gvFeaPrjFCS.DataBind();
            //    return;
            //}
            //ViewState["tblfeaprj"] = ds2.Tables[0];
            //this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            //this.gvFeaPrjFCS.DataBind();
            //((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");

            //this.gvFeaPrjsalrev.DataSource = ds2.Tables[2];
            //this.gvFeaPrjsalrev.DataBind();

            //DataTable dt = ((DataTable)ds2.Tables[2]).Copy();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("infcod  not like '0103%'");
            //dt = dv.ToTable();

            //((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lgvFlsisessalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lsizes)", "")) ? 0.00 : dt.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lgvFamtsalrev")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[2].Compute("Sum(salamt)", "")) ? 0.00 : ds2.Tables[2].Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");

            //this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            //this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            //this.Data_Bind();
            //ds2.Dispose();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            string grp = dt1.Rows[0]["grp"].ToString();
            string subgrp = dt1.Rows[0]["subgrp"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["subgrp"].ToString() == subgrp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    subgrp = dt1.Rows[j]["subgrp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["subgrpdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["subgrp"].ToString() == subgrp)
                    {
                        dt1.Rows[j]["subgrpdesc"] = "";
                    }

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";
                    }

                    grp = dt1.Rows[j]["grp"].ToString();
                    subgrp = dt1.Rows[j]["subgrp"].ToString();
                }
            }
            return dt1;
        }

        private void Data_Bind()
        {
            string comcod = this.GetComCode();
            string qtype = this.Request.QueryString["Type"];
            //int rindex = this.rbtnList1.SelectedIndex;
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            switch (qtype)
            {

                case "Cost":
                    this.gvFeaPrjC.DataSource = dt;
                    this.gvFeaPrjC.DataBind();
                    this.FooterCal();
                    break;
                case "Revenue":
                    if (ASTUtility.Left(comcod, 1) == "2")
                    {

                    }
                    else
                    {
                        this.gvFeaPrj.DataSource = dt;
                        this.gvFeaPrj.DataBind();
                        this.FooterCal();
                    }
                    break;

            }
        }
        private void FooterCal()
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            if (dt.Rows.Count == 0)
                return;
            string qtype = this.Request.QueryString["Type"];
            // int rindex = this.rbtnList1.SelectedIndex;
            switch (qtype)
            {

                case "Cost":
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFeAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(estam)", "")) ?
                    0.00 : dt.Compute("Sum(estam)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFaaddAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aadam)", "")) ?
                        0.00 : dt.Compute("Sum(aadam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFexaddAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(eadam)", "")) ?
                     0.00 : dt.Compute("Sum(eadam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFsaveAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(savam)", "")) ?
                     0.00 : dt.Compute("Sum(savam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFtotalAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalam)", "")) ?
                     0.00 : dt.Compute("Sum(totalam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFtotalCpSft")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(costpsft)", "")) ?
                     0.00 : dt.Compute("Sum(costpsft)", ""))).ToString("#,##0;(#,##0); ");


                    break;
                case "Revenue":
                    if (ASTUtility.Left(comcod, 1) == "2")
                    {

                    }
                    else
                    {
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = ("infcod like '8301%'");
                        dt = dv.ToTable();
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFtotalsh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lsizes)", "")) ?
                           0.00 : dt.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFlownersh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lowner)", "")) ?
                            0.00 : dt.Compute("Sum(lowner)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFcompanysh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(company)", "")) ?
                          0.00 : dt.Compute("Sum(company)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFpfrmlownersh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pflowner)", "")) ?
                          0.00 : dt.Compute("Sum(pflowner)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFadjmnt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(adjmnt)", "")) ?
                              0.00 : dt.Compute("Sum(adjmnt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFtcompanysh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalcom)", "")) ?
                          0.00 : dt.Compute("Sum(totalcom)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    break;
            }
        }






    }
}