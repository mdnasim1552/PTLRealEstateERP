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
    public partial class ProjFeasibilityLandDev : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT FEASIBILITY";
                //this.VColoumnAHeaderChange();
                rbtnList1.Items.Remove("Reports");
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

        private void VColoumnAHeaderChange()
        {

            string comcod = ASTUtility.Left((this.GetComCode()), 1);
            switch (comcod)
            {
                case "2":
                    //Cost
                    this.gvFeaPrjFC.Columns[5].Visible = false;
                    this.gvFeaPrjFC.Columns[6].Visible = false;
                    this.gvFeaPrjFC.Columns[8].HeaderText = "Khata/Plot";
                    this.gvFeaPrjFC.Columns[9].HeaderText = "No Of Plot";
                    this.gvFeaPrjFC.Columns[10].HeaderText = "Total Khata";
                    //Revenue
                    this.gvFeaPrjFCS.Columns[5].Visible = false;
                    this.gvFeaPrjFCS.Columns[6].Visible = false;
                    this.gvFeaPrjFCS.Columns[8].HeaderText = "Khata/Plot";
                    this.gvFeaPrjFCS.Columns[9].HeaderText = "No Of Plot";
                    this.gvFeaPrjFCS.Columns[10].HeaderText = "Total Khata";
                    break;




            }


        }

        private void ProjectName()
        {
            string comcod = this.GetComCode();
            string Filter1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.ProjectName();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.PanelSelName.Visible = true;
                this.ProjectLock();
                return;
            }

            this.lbtnOk.Text = "Ok";
            this.lblFeaProLock.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.rbtnList1.SelectedIndex = -1;
            this.MultiView1.ActiveViewIndex = -1;
            this.gvProjectInfo.DataSource = null;
            this.gvProjectInfo.DataBind();

            this.gvFeaPrjFC.DataSource = null;
            this.gvFeaPrjFC.DataBind();
            this.gvFeaPrjC.DataSource = null;
            this.gvFeaPrjC.DataBind();
            this.gvFeaPrjFCS.DataSource = null;
            this.gvFeaPrjFCS.DataBind();
            this.gvFeaPrj.DataSource = null;
            this.gvFeaPrj.DataBind();
            this.gvFeaPrjRep.DataSource = null;
            this.gvFeaPrjRep.DataBind();
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.PanelSelName.Visible = false;

        }

        private void ProjectLock()
        {

            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "FEAPROJECTLOCK", pactcode, "", "", "", "", "", "", "", "");
            this.lblFeaProLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();

        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            if (this.chkAllRes.Checked)
            {
                string selectindex = this.rbtnList1.SelectedIndex.ToString();

                switch (selectindex)
                {
                    case "0":

                        break;

                    case "1":
                        this.ShowCostAll();
                        break;


                    case "2":
                        this.ShowSaleAll();
                        break;

                }




            }

            else
            {

                string selectindex = this.rbtnList1.SelectedIndex.ToString();

                switch (selectindex)
                {
                    case "1":
                        this.ShowCost();
                        break;
                    case "2":
                        this.ShowRevenue();
                        break;
                }
            }
        }


        private void ShowCostAll()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string Code = (this.rbtnList1.SelectedIndex == 1) ? ((this.ddlProjectName.SelectedValue.Substring(2, 2).ToString() == "98") ? "(infcod like '04%')"
            //                : (this.ddlProjectName.SelectedValue.Substring(2, 2).ToString() == "99") ? "(infcod like '05%')" : "(infcod like '0[3-5]%')")
            //                    : (this.rbtnList1.SelectedIndex == 2) ? "infcod like '01%'" : "infcod like '5[1-2]%'";

            string CostOrSale = (this.rbtnList1.SelectedIndex == 1) ? "81%" : (this.rbtnList1.SelectedIndex == 1) ? "82%" : "";

            DataSet ds3 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "FEAPRANDPRJCT02LAND", pactcode, "infcod like '03%'", CostOrSale, "12", "", "", "", "", "");
            ViewState["tblfeaprj"] = ds3.Tables[0];
            ViewState["tblfeaprjCal"] = ds3.Tables[2];
            this.gvFeaPrjFC.DataSource = ds3.Tables[1];
            this.gvFeaPrjFC.DataBind();

            this.gvFeaPrjC.Columns[7].Visible = false;
            this.gvFeaPrjC.Columns[8].Visible = false;
            this.gvFeaPrjC.Columns[9].Visible = false;
            //if (ASTUtility.Left(comcod, 1) == "2")
            //{
            //    this.gvFeaPrjFC.Columns[9].Visible = false;
            //}
            //if (ASTUtility.Left(comcod, 1) != "2")
            //{
            //    this.gvFeaPrjFC.Columns[13].Visible = false;
            //}
            ds3.Dispose();
            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFFar")).Text = Convert.ToDouble((Convert.IsDBNull(ds3.Tables[1].Compute("Sum(far)", "")) ? 0.00 : ds3.Tables[1].Compute("Sum(far)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = Convert.ToDouble((Convert.IsDBNull(ds3.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds3.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
            this.Data_Bind();
        }

        private void ShowSaleAll()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '83%'";
            string CostOrSale = "82%";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "FEAPRANDPRJCT0S2", pactcode, Code, CostOrSale, "12", "", "", "", "", "");
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblfeaprjRev"] = ds2.Tables[1];
            ViewState["tblsaldis"] = ds2.Tables[2];
            ViewState["tblfeaprjcal"] = ds2.Tables[4];
            this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            this.gvFeaPrjFCS.DataBind();
            if (ds2.Tables[1].Rows.Count != 0)
                ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");

            this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblProfitVal.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["profit"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblPerFlr.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["sftpflr"]).ToString("#,##0.00;(#,##0.00); ");
            this.lblBrate.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["brorate"]).ToString("#,##0.00;(#,##0.00); ");
            this.lblTarVal.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["trev"]).ToString("#,##0.00;(#,##0.00); ");

            this.Data_Bind();
            ds2.Dispose();




        }
        //private void ShowSaleAllLand()
        //{
        //    ViewState.Remove("tblfeaprj");
        //    string comcod = this.GetComCode();
        //    string pactcode = this.ddlProjectName.SelectedValue.ToString();
        //    string Code = "infcod like '81%'";
        //    DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "FEAPRANDPRJCT0S2LAND", pactcode, Code, "12", "", "", "", "", "", "");
        //    ViewState["tblfeaprj"] = ds2.Tables[0];

        //    DataTable dt = ((DataTable)ds2.Tables[0]).Copy();
        //    DataTable dt1 = new DataTable();
        //    DataTable dt2 = new DataTable();
        //    DataView dv = dt.DefaultView;
        //    dv.RowFilter = ("infcod  like '0101%' or infcod  like '0102%' or infcod  like '0103%'");
        //    dt1 = dv.ToTable();
        //    this.grvSaleRevLand.DataSource = dt1;
        //    this.grvSaleRevLand.DataBind();


        //    dv = dt.DefaultView;
        //    dv.RowFilter = ("infcod   like '0101%' or  infcod   like '0102%'");
        //    dt2 = dv.ToTable();

        //    if (dt1.Rows.Count != 0)
        //    {
        //        ((Label)this.grvSaleRevLand.FooterRow.FindControl("lgvFlsisessalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
        //        ((Label)this.grvSaleRevLand.FooterRow.FindControl("lgvFamtsalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
        //    }

        //    dv = dt.DefaultView;
        //    dv.RowFilter = ("infcod   like '015100101%' or infcod   like '015100102%'");
        //    dt1 = dv.ToTable();
        //    this.grvSoldLand.DataSource = dt1;
        //    this.grvSoldLand.DataBind();

        //    dv = dt.DefaultView;
        //    dv.RowFilter = ("infcod   like '015100101%'");
        //    dt2 = dv.ToTable();
        //    if (dt1.Rows.Count != 0)
        //    {
        //        ((Label)this.grvSoldLand.FooterRow.FindControl("lgvFlsisessoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
        //        ((Label)this.grvSoldLand.FooterRow.FindControl("lgvFamtsoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
        //    }

        //    dv = dt.DefaultView;
        //    dv.RowFilter = ("infcod   like '015200101%' or infcod   like '015200102%' ");
        //    dt1 = dv.ToTable();
        //    this.grvUnSoldLAnd.DataSource = dt1;
        //    this.grvUnSoldLAnd.DataBind();
        //    dv = dt.DefaultView;
        //    dv.RowFilter = ("infcod   like '015200101%'");
        //    dt2 = dv.ToTable();
        //    if (dt1.Rows.Count != 0)
        //    {
        //        ((Label)this.grvUnSoldLAnd.FooterRow.FindControl("lgvFlsisesusoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(lsizes)", "")) ? 0.00 : dt2.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
        //        ((Label)this.grvUnSoldLAnd.FooterRow.FindControl("lgvFamtusoldinfo")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(salamt)", "")) ? 0.00 : dt1.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
        //    }
        //    this.Data_Bind();
        //    ds2.Dispose();


        //}

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            //ReportDocument rpcp = new RealERPRPT.R_02_Fea.RptFeaProject();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = prjname;
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = this.lblHeader.Text;
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            //Session["Report1"] = rpcp;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>"; 
        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int rindex = this.rbtnList1.SelectedIndex;
            switch (rindex)
            {
                case 0:

                    this.GetProjectInfo();
                    this.chkAllRes.Visible = false;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;

                case 1:
                    this.ShowCost();
                    this.chkAllRes.Visible = true;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;
                case 2:
                    this.chkAllRes.Visible = true;

                    this.ShowRevenue();
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;

                case 3:
                    this.ShowReport();
                    this.chkAllRes.Visible = false;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;


            }

        }

        private void GetProjectInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "PROJECTINFO", ProjectCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;

            }



            this.gvProjectInfo.DataSource = ds1.Tables[0];
            this.gvProjectInfo.DataBind();
            ((CheckBox)this.gvProjectInfo.FooterRow.FindControl("chkProjectLock")).Checked = (this.lblFeaProLock.Text == "True") ? true : false;
            if (Request.QueryString["Type"].ToString() == "FeaEntry")
            {
                string tf = this.lblFeaProLock.Text;
                ((LinkButton)this.gvProjectInfo.FooterRow.FindControl("lUpdatProInfo")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
                ((CheckBox)this.gvProjectInfo.FooterRow.FindControl("chkProjectLock")).Enabled = false;
            }

        }
        protected void lUpdatProInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            bool result = false;
            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                //if (Gvalue.Length > 0)
                //{
                result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INSERTORUPFEAPRJINF", pactcode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                    return;
                }
                //}
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";


            string Projectlock = (((CheckBox)this.gvProjectInfo.FooterRow.FindControl("chkProjectLock")).Checked) ? "1" : "0";
            result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPPROJECTLOCK", pactcode, Projectlock, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void ShowCost()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string Code = (this.ddlProjectName.SelectedValue.Substring(2, 2).ToString() == "98") ? "(infcod like '04%')"
            //                : (this.ddlProjectName.SelectedValue.Substring(2, 2).ToString() == "99") ? "(infcod like '05%')" : "(infcod like '0[3-5]%')";
            string CostOrSale = "81%";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "GETPROSALES02LAND", pactcode, "infcod like '03%'", CostOrSale, "12", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaPrjC.DataSource = null;
                this.gvFeaPrjC.DataBind();
                this.gvFeaPrjFC.DataSource = null;
                this.gvFeaPrjFC.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblfeaprjCal"] = ds2.Tables[2];
            this.gvFeaPrjFC.DataSource = ds2.Tables[1];
            this.gvFeaPrjFC.DataBind();

            this.gvFeaPrjC.Columns[7].Visible = false;
            this.gvFeaPrjC.Columns[8].Visible = false;
            this.gvFeaPrjC.Columns[9].Visible = false;
            //if (ASTUtility.Left(comcod, 1) == "2")
            //{
            //    this.gvFeaPrjFC.Columns[9].Visible = false;
            //}
            //if (ASTUtility.Left(comcod, 1) != "2")
            //{
            //    this.gvFeaPrjFC.Columns[13].Visible = false;
            //}
            ds2.Dispose();
            if (ds2.Tables[1].Rows.Count != 0)
            {
                ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFFar")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(far)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(far)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(perent)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(perent)", ""))).ToString("#,##0.00;(#,##0.00); ") + " %";
            }

            this.Data_Bind();

        }



        private void ShowRevenue()
        {


            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '83%'";
            string CostOrSale = "82%";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "GETPROSALESREVNUE02", pactcode, Code, CostOrSale, "12", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrj.DataSource = null;
                this.gvFeaPrj.DataBind();
                this.gvFeaPrjFCS.DataSource = null;
                this.gvFeaPrjFCS.DataBind();
                this.gvFeaPrjsalrev.DataSource = null;
                this.gvFeaPrjsalrev.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblfeaprjRev"] = ds2.Tables[1];
            ViewState["tblsaldis"] = ds2.Tables[2];
            ViewState["tblfeaprjcal"] = ds2.Tables[4];
            this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            this.gvFeaPrjFCS.DataBind();
            if (ds2.Tables[1].Rows.Count != 0)
                ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");

            this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblProfitVal.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["profit"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblPerFlr.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["sftpflr"]).ToString("#,##0.00;(#,##0.00); ");
            this.lblBrate.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["brorate"]).ToString("#,##0.00;(#,##0.00); ");
            this.lblTarVal.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["trev"]).ToString("#,##0.00;(#,##0.00); ");

            this.Data_Bind();
            ds2.Dispose();

        }


        private void ShowReport()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITY", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaPrjRep.DataSource = null;
                this.gvFeaPrjRep.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = this.HiddenSameData(ds2.Tables[0]);
            DataTable dt = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();



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
            int rindex = this.rbtnList1.SelectedIndex;
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            switch (rindex)
            {

                case 1:
                    this.gvFeaPrjC.DataSource = dt;
                    this.gvFeaPrjC.DataBind();
                    if (Request.QueryString["Type"].ToString() == "FeaEntry")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ((LinkButton)this.gvFeaPrjFC.FooterRow.FindControl("lbtnfUpdateCostFC")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
                            ((LinkButton)this.gvFeaPrjC.FooterRow.FindControl("lbtnfUpdateCost")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
                        }

                    }

                    this.FooterCal();

                    break;


                case 2:
                    DataTable dt1 = (DataTable)ViewState["tblsaldis"];
                    this.gvFeaPrj.DataSource = dt1;
                    this.gvFeaPrj.DataBind();
                    if (dt1.Rows.Count != 0)
                    {
                        DataView dv = dt1.Copy().DefaultView;
                        dv.RowFilter = "infcod like('8301%')";
                        DataTable dts = dv.ToTable();
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFcompanysh")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(company)", "")) ?
                          0.00 : dts.Compute("Sum(company)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFtcompanysh")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(totalcom)", "")) ?
                          0.00 : dts.Compute("Sum(totalcom)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFtotalsh")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(lsizes)", "")) ?
                                              0.00 : dts.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFlownersh")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(lowner)", "")) ?
                            0.00 : dts.Compute("Sum(lowner)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFpfrmlownersh")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(pflowner)", "")) ?
                          0.00 : dts.Compute("Sum(pflowner)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFadjmnt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(adjmnt)", "")) ?
                          0.00 : dts.Compute("Sum(adjmnt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFConArea")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(iconarea)", "")) ?
                         0.00 : dts.Compute("Sum(iconarea)", ""))).ToString("#,##0;(#,##0); ");

                    }

                    this.gvFeaPrjsalrev.DataSource = dt;
                    this.gvFeaPrjsalrev.DataBind();

                    if (Request.QueryString["Type"].ToString() == "FeaEntry")
                    {
                        //((LinkButton)this.gvFeaPrjFCS.FooterRow.FindControl("lbtnfUpdateCostFCS")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
                        ((LinkButton)this.gvFeaPrj.FooterRow.FindControl("lbtnFUpdateSales")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
                        //   ((LinkButton)this.gvFeaPrjsalrev.FooterRow.FindControl("lbtnFUpdatesalrev")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;


                    }
                    this.FooterCal();
                    //}
                    break;

                case 3:
                    this.gvFeaPrjRep.DataSource = dt;
                    this.gvFeaPrjRep.DataBind();
                    break;



            }


        }
        private void FooterCal()
        {
            string comcod = this.GetComCode();
            try
            {
                DataTable dt = (DataTable)ViewState["tblfeaprj"];
                if (dt.Rows.Count == 0)
                    return;

                int rindex = this.rbtnList1.SelectedIndex;
                switch (rindex)
                {

                    case 1:

                        double Conarea = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text.Trim());
                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFeAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(estam)", "")) ?
                        0.00 : dt.Compute("Sum(estam)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFphwam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(phwam)", "")) ?
                        0.00 : dt.Compute("Sum(phwam)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFprjwam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(prjwam)", "")) ?
                        0.00 : dt.Compute("Sum(prjwam)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFtcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(prjwam)", "")) ?
                        0.00 : dt.Compute("Sum(tcost)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFaaddAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aadam)", "")) ?
                            0.00 : dt.Compute("Sum(aadam)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFexaddAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(eadam)", "")) ?
                         0.00 : dt.Compute("Sum(eadam)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFsaveAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(savam)", "")) ?
                         0.00 : dt.Compute("Sum(savam)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFtotalAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalam)", "")) ?
                         0.00 : dt.Compute("Sum(totalam)", ""))).ToString("#,##0;(#,##0); ");
                        double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalam)", "")) ? 0.00 : dt.Compute("Sum(totalam)", "")));
                        ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFCostpersftc")).Text = (toamt / Conarea).ToString("#,##0;(#,##0); ");




                        break;


                    case 2:
                        ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lgvFamtsalrev")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salamt)", "")) ?
                            0.00 : dt.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lblgrFBAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                            0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvFeaPrjsalrev.FooterRow.FindControl("lblgrFMAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(minamt)", "")) ?
                            0.00 : dt.Compute("Sum(minamt)", ""))).ToString("#,##0;(#,##0); ");
                        break;

                }

            }

            catch (Exception ex)
            {
            }

        }


        private void UpdateProjectSaleAndCost()
        {

            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string ResCode = dt.Rows[i]["infcod"].ToString();
                string Description = dt.Rows[i]["infdesc"].ToString();
                string Qty = Convert.ToDouble(dt.Rows[i]["qty"]).ToString();
                string Esamt = Convert.ToDouble(dt.Rows[i]["estam"]).ToString();
                string phwam = Convert.ToDouble(dt.Rows[i]["phwam"]).ToString();
                string prjwam = Convert.ToDouble(dt.Rows[i]["prjwam"]).ToString();
                string Aadam = Convert.ToDouble(dt.Rows[i]["aadam"]).ToString();
                string Exadam = Convert.ToDouble(dt.Rows[i]["eadam"]).ToString();
                string savam = Convert.ToDouble(dt.Rows[i]["savam"]).ToString();
                double Toam = Convert.ToDouble(dt.Rows[i]["totalam"]);
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "INORUPFEAPRJCTCOST02", PactCode, ResCode, Description, Qty, Esamt, phwam,
                       prjwam, Aadam, Exadam, savam, Toam.ToString(), "", "", "", "");
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.ShowCost();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void gvFeaPrj_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvFeaPrj.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "DELETEITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod not in('" + Itemcode + "')";
            ViewState.Remove("tblfeaprj");
            ViewState["tblfeaprj"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void lbtnTotalCostFC_Click(object sender, EventArgs e)
        {
            double toamt = 0.00, tparcent = 0.00, tsft = 0.00, bensft = 0.00;
            for (int i = 0; i < this.gvFeaPrjFC.Rows.Count; i++)
            {
                double landsize = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvlandsize")).Text.Trim());
                double tkhata = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvconktosft")).Text.Trim());//sft per flr
                double sftperf = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvsftperf")).Text.Trim());//sft per flr

                double far = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvFar")).Text.Trim());
                double othben = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvBenifit")).Text.Trim());

                tsft = tkhata * (far / 100);
                bensft = 0.00;//tsft * othben;



                double tcsizesft = tsft + bensft;//total sft
                ((Label)this.gvFeaPrjFC.Rows[i].FindControl("txtgvtcsft")).Text = tcsizesft.ToString("#,##0;(#,##0); ");
                //double tcsizesft = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvtcsft")).Text.Trim());//total sft

                double lsizes = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtlsizes")).Text.Trim());//no of flr
                double tsizes = (lsizes == 0) ? 0 : (tcsizesft / lsizes);
                ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvpercntge")).Text = (sftperf == 0) ? "0" : Convert.ToDouble(((tsizes * 100) / sftperf)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lblgvstonum")).Text = tsizes.ToString("#,##0;(#,##0); ");
                toamt = toamt + tcsizesft;
                //((Label)this.gvFeaPrjFC.Rows[i].FindControl("lblPercent")).Text = (landsize == 0) ? "0" : Convert.ToDouble(tcsizesft * 100 / landsize).ToString("#,##0.00;(#,##0.00); ");

                //double per = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lblPercent")).Text.Trim());
                //tparcent = tparcent + per;
            }

            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = toamt.ToString("#,##0;(#,##0); ");
            //((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFPercent")).Text = tparcent.ToString("#,##0;(#,##0); ");


        }

        protected void lbtnfUpdateCostFC_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();

            for (int i = 0; i < this.gvFeaPrjFC.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lblgvItmCodfc")).Text.Trim();
                string Desccription = ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvItemdescc")).Text.Trim();
                string lsizek = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvlandsize")).Text.Trim()).ToString();
                string sftperf = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvsftperf")).Text.Trim()).ToString();
                string far = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvFar")).Text.Trim()).ToString();
                string othben = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvBenifit")).Text.Trim()).ToString();
                string tsizes = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtlsizes")).Text.Trim()).ToString();
                string tcsizesft = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("txtgvtcsft")).Text.Trim()).ToString();
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "INORUPDATEFEAPRJCTFC", PactCode, Code, Desccription, lsizek, sftperf, tsizes, tcsizesft, "0", "", far, othben, "", "", "", "");
            }
            this.ShowCostAll();
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
        }




        protected void lbtnTotalCostFCS_Click(object sender, EventArgs e)
        {
            double toamt = 0.00;
            for (int i = 0; i < this.gvFeaPrjFCS.Rows.Count; i++)
            {

                double sftperf = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lgvsftperfs")).Text.Trim());
                double stonum = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvstonums")).Text.Trim());
                double tssizesft = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvtssfts")).Text.Trim());
                double tsizes = (stonum == 0) ? 0 : (tssizesft / stonum);
                ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lgvpercntges")).Text = (sftperf == 0) ? "0" : Convert.ToDouble(((tsizes * 100) / sftperf)).ToString("#, ##0;(#,##0); ");
                ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lgvlsizess")).Text = tsizes.ToString("#,##0;(#,##0); ");
                toamt = toamt + tssizesft;
            }

            ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = toamt.ToString("#,##0;(#,##0); ");
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }
        protected void lbtnfUpdateCostFCS_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();

            for (int i = 0; i < this.gvFeaPrjFCS.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lblgvItmCodfcs")).Text.Trim();
                string Desc = ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvItemdescsalar")).Text.Trim();
                string lsizek = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lgvlandsizes")).Text.Trim()).ToString();
                string sftperf = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lgvsftperfs")).Text.Trim()).ToString();
                double tsizes = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lgvlsizess")).Text.Trim());
                string tcsizesft = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvtssfts")).Text.Trim()).ToString();
                string bqty = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvBQTY")).Text.Trim()).ToString();
                string bdesc = ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvBDesc")).Text.Trim();
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPDATEFEAPRJCTFC", PactCode, Code, Desc, lsizek, sftperf, tsizes.ToString(), tcsizesft.ToString(), bqty, bdesc, "", "", "", "", "", "");

            }
            this.SalesDisCom();
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";


        }
        private void SalesDisCom()
        {
            DataTable dt = (DataTable)ViewState["tblsaldis"];
            double tpark = 0.00, tutys = 0.00, tutysr = 0.00, tutyo = 0.00, tutya = 0.00;
            double landSc = 0.00;
            double PerFlr = Convert.ToDouble("0" + this.lblPerFlr.Text.Trim());
            for (int i = 0; i < this.gvFeaPrjFCS.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lblgvItmCodfcs")).Text.Trim();
                double stnum = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvstonums")).Text.Trim());
                string tcsizesft = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvtssfts")).Text.Trim()).ToString();
                double bqty = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvBQTY")).Text.Trim());
                if (ASTUtility.Left(Code, 4) == "8201")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "8301" + ASTUtility.Right(Code, 8) + "'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = tcsizesft;
                    dr1[0]["iconarea"] = stnum * PerFlr;

                }

                /* Utilities*/
                if (ASTUtility.Left(Code, 7) == "8201001")
                {
                    //DataRow[] dr1 = dt.Select("infcod1='" + "8303" + Code.Substring(4,3)  + "'");/*ASTUtility.Right(Code, 8)*/
                    //if (dr1.Length > 0)
                    tutys = tutys + bqty;
                    //dr1[0]["lsizes"] = bqty;
                }
                if (ASTUtility.Left(Code, 7) == "8201002")
                {
                    tutysr = tutysr + bqty;
                }
                if (ASTUtility.Left(Code, 7) == "8201003")
                {
                    tutyo = tutyo + bqty;
                }
                if (ASTUtility.Left(Code, 7) == "8201004")
                {
                    tutya = tutya + bqty;
                }
                /* Utilities End*/
                if (ASTUtility.Left(Code, 4) == "8202")
                {
                    tpark = tpark + bqty;
                }
                if (ASTUtility.Left(Code, 4) == "8201")/*(Code == "820100101003" || Code == "820100101005" || Code == "820100101008")*/
                {

                    landSc = landSc + bqty;
                }

            }

            DataRow[] dr2 = dt.Select("infcod='" + "830200101001" + "'");
            if (dr2.Length > 0)
                dr2[0]["lsizes"] = tpark;

            DataRow[] dr3 = dt.Select("infcod='" + "830400101001" + "'");
            if (dr3.Length > 0)
                dr3[0]["lsizes"] = landSc;
            /* Utilities*/
            DataRow[] drs = dt.Select("infcod='" + "830300101001" + "'");
            if (drs.Length > 0)
                drs[0]["lsizes"] = tutys;
            DataRow[] drsr = dt.Select("infcod='" + "830300201001" + "'");
            if (drsr.Length > 0)
                drsr[0]["lsizes"] = tutysr;
            DataRow[] dro = dt.Select("infcod='" + "830300301001" + "'");
            if (dro.Length > 0)
                dro[0]["lsizes"] = tutyo;
            DataRow[] dra = dt.Select("infcod='" + "830300401001" + "'");
            if (dra.Length > 0)
                dra[0]["lsizes"] = tutya;


            ViewState["tblsaldis"] = dt;
            this.Data_Bind();
        }

        protected void lbtnFUpdateSales_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblsaldis"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ResCode = dt.Rows[i]["infcod"].ToString();
                string Desc = dt.Rows[i]["infdesc"].ToString();
                double lsizes = Convert.ToDouble(dt.Rows[i]["lsizes"]);
                string lowner = Convert.ToDouble(dt.Rows[i]["lowner"]).ToString();
                string company = Convert.ToDouble(dt.Rows[i]["company"]).ToString();
                string Pflowner = Convert.ToDouble(dt.Rows[i]["pflowner"]).ToString();
                string adjmnt = Convert.ToDouble(dt.Rows[i]["adjmnt"]).ToString();
                string iconarea = Convert.ToDouble(dt.Rows[i]["iconarea"]).ToString();
                //if (lsizes > 0)
                //{
                //feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPDATEFEAPRJCT", PactCode, ResCode, Size, Number, Amt, salrate, "", "", "", "", "", "", "","","");
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPFEAPRJCTSALE02", PactCode, ResCode, Desc, lsizes.ToString(), lowner, company, Pflowner, adjmnt, iconarea, "", "", "", "", "", "");
                // }


            }
            this.ShowSaleAll();
            this.SalesRevDis();
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

        }
        private void SalesRevDis()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];


            for (int i = 0; i < this.gvFeaPrj.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrj.Rows[i].FindControl("lblgvItmCod")).Text.Trim();


                double qty = Convert.ToDouble("0" + ((Label)this.gvFeaPrj.Rows[i].FindControl("lgvtotalcompany")).Text.Trim());
                if (ASTUtility.Left(Code, 4) == "8302")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "0102" + ASTUtility.Right(Code, 8) + "'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = qty;
                }
                if (ASTUtility.Left(Code, 4) == "8304")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "0104" + ASTUtility.Right(Code, 8) + "'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = qty;
                }
                if (Code == "830300101001")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "010300101001'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = qty;
                }
                if (Code == "830300201001")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "010300201001'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = qty;
                }
                if (Code == "830300301001")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "010300301001'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = qty;
                }
                if (Code == "830300401001")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "010300401001'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = qty;
                }


            }

            //DataRow[] dr2 = dt.Select("infcod='" + "830200101001" + "'");
            //if (dr2.Length > 0)
            //    dr2[0]["lsizes"] = tpark;

            //DataRow[] dr3 = dt.Select("infcod='" + "830400101001" + "'");
            //if (dr3.Length > 0)
            //    dr3[0]["lsizes"] = landSc;


            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {

            double plownershare = Convert.ToDouble("0" + this.lblLownerval.Text.Trim().Replace("%", ""));
            double pcomshare = Convert.ToDouble("0" + this.lblCompanyval.Text.Trim().Replace("%", ""));
            //double tssize =Convert.ToDouble("0"+((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text);
            DataTable dt = (DataTable)ViewState["tblsaldis"];
            for (int i = 0; i < this.gvFeaPrj.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrj.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                string Desc = ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvItemdescsaldet")).Text.Trim();
                double tosize = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvSize")).Text.Trim());
                double Purflowner = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvpurfrmlanowner")).Text.Trim());
                double adjmnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvadj")).Text.Trim()));
                double iconarea = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvConArea")).Text.Trim());

                double lownershare = tosize * plownershare * 0.01;
                double comshare = tosize * pcomshare * 0.01;

                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["infdesc"] = Desc;
                dr1[0]["lsizes"] = tosize;
                dr1[0]["lowner"] = lownershare;
                dr1[0]["company"] = comshare;
                dr1[0]["pflowner"] = Purflowner;
                dr1[0]["adjmnt"] = adjmnt;
                dr1[0]["totalcom"] = comshare + Purflowner + adjmnt;
                dr1[0]["iconarea"] = iconarea;

            }
            ViewState["tblsaldis"] = dt;
            this.Data_Bind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }
        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            DataTable dt1 = (DataTable)ViewState["tblfeaprjCal"];
            double Conarea = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text);
            double Esamt = 0.00, phwam = 0.00, prjwam = 0.00;
            for (int i = 0; i < this.gvFeaPrjC.Rows.Count; i++)
            {

                string Code = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lblgvItmCodc")).Text.Trim();
                string Desc = ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvItemdescc01")).Text.Trim();
                double Qty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvqtyc")).Text.Trim()));
                double rate = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvratec")).Text.Trim());
                //double Aaddam = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvaaddamtc")).Text.Trim());
                //double Exaddam = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvexaddamtc")).Text.Trim());
                //double Saveam = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvsaveamtc")).Text.Trim());


                double Aaddam = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvaaddamtc")).Text.Trim()));
                double Exaddam = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvexaddamtc")).Text.Trim()));
                double Saveam = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvsaveamtc")).Text.Trim()));

                string unit = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lgUnitnumc")).Text.Trim();

                if (unit == "%")
                {
                    DataRow[] dr11 = dt1.Select("infcod='" + Code + "'");
                    if (dr11.Length > 0)
                    {
                        Esamt = Convert.ToDouble(dr11[0]["tcost"]) * (Qty / 100);
                        phwam = 0.00;
                        prjwam = 0.00;
                    }
                    else
                    {
                        //tcost = Qty * (tcost / 100);
                        Esamt = 0.00;
                        phwam = 0.00;
                        prjwam = 0.00;
                    }
                }
                else
                {


                    if (ASTUtility.Left(Code, 2) == "03")
                    {
                        DataRow[] dr11 = dt.Select("infcod='" + Code + "'");
                        if (dr11.Length > 0)
                        {
                            Esamt = Qty * rate;
                            phwam = 0.00 * .00;
                            prjwam = 0.00 * .000;
                        }

                    }
                    else if (ASTUtility.Left(Code, 2) == "04")
                    {
                        DataRow[] dr11 = dt.Select("infcod='" + Code + "'");
                        if (dr11.Length > 0)
                        {
                            Esamt = 0.00 * 0.00;
                            phwam = Qty * rate;
                            prjwam = 0.00 * .000;
                        }
                    }
                    else if (ASTUtility.Left(Code, 2) == "05")
                    {
                        DataRow[] dr11 = dt.Select("infcod='" + Code + "'");
                        if (dr11.Length > 0)
                        {
                            Esamt = 0.00 * 0.00;
                            phwam = 0.00 * 0.00;
                            prjwam = Qty * rate;
                        }
                    }
                    else
                    {
                        //DataRow[] dr11 = dt.Select("infcod='" + Code + "'");
                        //if (dr11.Length > 0)
                        //{
                        //    //Esamt = Qty * rate;
                        //}
                        Esamt = 0.00 * 0.00;
                        phwam = 0.00 * 0.00;
                        prjwam = 0.00 * 0.00;

                    }

                }


                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["infdesc"] = Desc;
                dr1[0]["qty"] = Qty;
                dr1[0]["rate"] = rate;
                dr1[0]["estam"] = Esamt;
                dr1[0]["phwam"] = phwam;
                dr1[0]["prjwam"] = prjwam;
                dr1[0]["tcost"] = Esamt + phwam + prjwam;
                dr1[0]["aadam"] = Aaddam;
                dr1[0]["eadam"] = Exaddam;
                dr1[0]["savam"] = Saveam;
                dr1[0]["totalam"] = Esamt + phwam + prjwam + Aaddam + Exaddam - Saveam;
                dr1[0]["costpsft"] = (Conarea == 0) ? 0.00 : ((Esamt + phwam + prjwam + Aaddam + Exaddam - Saveam) / Conarea);
            }
            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void lbtnfUpdateCost_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.UpdateProjectSaleAndCost();
        }

        protected void lbtnTotalsalrev_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            DataTable dta = (DataTable)ViewState["tblfeaprjcal"];
            double bratevar = Convert.ToDouble(this.lblBrate.Text.Trim());
            double salamt;
            double bamt = 0.00, brate = 0.00, sprofit = 0.00;
            for (int i = 0; i < this.gvFeaPrjsalrev.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("lblgvItmCodsalrev")).Text.Trim();
                string infdesc = ((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("txtgvItemdescsalrev")).Text.Trim();
                string flrno = ((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("txtgvnum")).Text.Trim();
                double percent = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("txtSize")).Text.Trim());
                double tsize = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("txtglsizessalrev")).Text.Trim());
                double salrate = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("Txtgratesalrev")).Text.Trim());

                string unit = ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("lgUnitnum4")).Text.Trim();


                if (unit == "%" & percent != 0)
                {
                    DataRow[] dr11 = dta.Select("infcod='" + Code + "'");
                    if (dr11.Length > 0)
                    {

                        tsize = ((Convert.ToDouble(dr11[0]["salamt"]) * percent) / 100);
                    }
                }


                salamt = tsize * salrate;
                //-------------------------------Brochure Rate-------------------------------//
                if (unit == "%")
                {
                    brate = salrate + bratevar;
                }
                else
                {
                    brate = salrate;
                }
                ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("lblgbroratsalrev")).Text = brate.ToString("#,##0;(#,##0); ");
                bamt = (tsize != 0) ? Math.Abs(brate) * tsize : 0;
                ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("lblgrBAmt")).Text = bamt.ToString("#,##0;(#,##0); ");
                //--------------------------------Profit Calculation-----------------------------------//
                double bep = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("txtgvBep")).Text.Trim()));
                sprofit = (bep != 0) ? ((salrate - bep) / bep) * 100 : 0.00;
                ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("txtgvProfit")).Text = sprofit.ToString("#,##0.00;-#,##0.00; ");
                //------------------------------------------------------------------------//
                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["infdesc"] = infdesc;
                dr1[0]["rsize"] = percent;
                dr1[0]["flrno"] = flrno;
                dr1[0]["lsizes"] = tsize;
                dr1[0]["salamt"] = salamt;
                dr1[0]["rate"] = salrate;
                dr1[0]["brorat"] = brate;
                dr1[0]["bamt"] = bamt;
                dr1[0]["bep"] = bep;
                dr1[0]["profit"] = sprofit;
            }
            ViewState["tblfeaprj"] = dt;
            this.MinCalculation();
            this.Data_Bind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }
        private void MinCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            double taramt = Convert.ToDouble(this.lblTarVal.Text);

            double pamt = 0.00, tamt = 0.00, minamt = 0.00, mrate = 0.00, newprofit = 0.00;
            if (dt.Rows.Count != 0)
            {
                DataView dv = dt.Copy().DefaultView;
                dv.RowFilter = "infcod not like('0101%')";
                DataTable dts = dv.ToTable();

                pamt = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(salamt)", "")) ?
                  0.00 : dts.Compute("Sum(salamt)", "")));


                DataView dv1 = dt.Copy().DefaultView;
                dv.RowFilter = "infcod  like('0101%')";
                DataTable dts1 = dv.ToTable();

                tamt = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("Sum(salamt)", "")) ?
                  0.00 : dts1.Compute("Sum(salamt)", "")));
            }
            newprofit = (tamt != 0.00) ? (((taramt - pamt) - tamt) / tamt) * 100 : 0.00;

            for (int i = 0; i < this.gvFeaPrjsalrev.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("lblgvItmCodsalrev")).Text.Trim();
                double tsize = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("txtglsizessalrev")).Text.Trim());
                double salrate = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("Txtgratesalrev")).Text.Trim());
                double bep = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("txtgvBep")).Text.Trim()));
                string unit = ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("lgUnitnum4")).Text.Trim();

                if (bep != 0 && newprofit != 0)
                {
                    mrate = salrate + (salrate * (newprofit / 100));
                    ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("txtgMinPrice")).Text = mrate.ToString("#,##0;(#,##0); ");
                }
                else if (unit != "%")
                {
                    mrate = salrate;
                    ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("txtgMinPrice")).Text = mrate.ToString("#,##0;(#,##0); ");
                }
                else
                {
                    mrate = 0.00;
                }
                double minprice = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("txtgMinPrice")).Text.Trim()));
                minamt = (tsize != 0) ? Math.Abs(minprice) * tsize : 0;
                ((Label)this.gvFeaPrjsalrev.Rows[i].FindControl("lblgrMAmt")).Text = minamt.ToString("#,##0;(#,##0); ");



                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");

                dr1[0]["minprice"] = (bep != 0 && newprofit != 0) ? mrate : minprice;
                dr1[0]["minamt"] = minamt;

            }
            ViewState["tblfeaprj"] = dt;


        }
        protected void lbtnFUpdatesalrev_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();

            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Code = dt.Rows[i]["infcod"].ToString();
                string Size = Convert.ToDouble(dt.Rows[i]["rsize"]).ToString();
                string Desc = dt.Rows[i]["infdesc"].ToString();
                string flrno = dt.Rows[i]["flrno"].ToString();
                string lsizes = Convert.ToDouble(dt.Rows[i]["lsizes"]).ToString();
                double salamt = Convert.ToDouble(dt.Rows[i]["salamt"]);
                string rsize = Convert.ToDouble(dt.Rows[i]["rsize"]).ToString();
                double rate = Convert.ToDouble(dt.Rows[i]["rate"]);
                string brorat = Convert.ToDouble(dt.Rows[i]["brorat"]).ToString();
                string bep = Convert.ToDouble(dt.Rows[i]["bep"]).ToString();
                string profit = Convert.ToDouble(dt.Rows[i]["profit"]).ToString();
                string minprice = Convert.ToDouble(dt.Rows[i]["minprice"]).ToString();

                if (salamt > 0)
                {

                    bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPPROFEASALREGFV", PactCode, Code, Desc, lsizes,
                        salamt.ToString(), brorat, rsize, flrno, minprice, bep, profit, "", "", "", "");
                }
            }


         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.ShowRevenue();

        }

        protected void gvFeaPrjC_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvFeaPrjC.Rows[e.RowIndex].FindControl("lblgvItmCodc")).Text.Trim();

            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "DELETEITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod not in('" + Itemcode + "')";
            ViewState.Remove("tblfeaprj");
            ViewState["tblfeaprj"] = dv.ToTable();
            this.Data_Bind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }




        protected void gvFeaPrjRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lgvgroupdesc");
                Label ToSize = (Label)e.Row.FindControl("lgtsizecRep");
                Label RatepSft = (Label)e.Row.FindControl("lgsalraterep");
                Label amt = (Label)e.Row.FindControl("lgvAmtrep");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    ToSize.Font.Bold = true;
                    RatepSft.Font.Bold = true;
                    amt.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");


                }

            }

        }

        protected void gvFeaPrjC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtgvratec = (TextBox)e.Row.FindControl("txtgvratec");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();
                string unit = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "unit")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Left(this.ddlProjectName.SelectedValue.ToString(), 4) == "0101")
                {
                    if (ASTUtility.Left(code, 2) == "04" || ASTUtility.Left(code, 2) == "05" || unit.Trim() == "%")
                    {


                        txtgvratec.ReadOnly = true;
                    }
                }

            }
        }
        protected void gvFeaPrj_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtgvConArea = (TextBox)e.Row.FindControl("txtgvConArea");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }

                if (ASTUtility.Left(code, 4) != "8301")
                {


                    txtgvConArea.ReadOnly = true;
                }

            }
        }
        protected void gvFeaPrjsalrev_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvFeaPrjsalrev.Rows[e.RowIndex].FindControl("lblgvItmCodsalrev")).Text.Trim();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_02", "DELETEREVITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod not in('" + Itemcode + "')";
            ViewState.Remove("tblfeaprj");
            ViewState["tblfeaprj"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void gvFeaPrjsalrev_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvItmCodsalrev = (Label)e.Row.FindControl("lblgvItmCodsalrev");
                TextBox txtgvItemdescsalrev = (TextBox)e.Row.FindControl("txtgvItemdescsalrev");
                Label lgUnitnum4 = (Label)e.Row.FindControl("lgUnitnum4");
                TextBox txtglsizessalrev = (TextBox)e.Row.FindControl("txtglsizessalrev");
                TextBox Txtgratesalrev = (TextBox)e.Row.FindControl("Txtgratesalrev");
                Label lblgvamtsalrev = (Label)e.Row.FindControl("lblgvamtsalrev");
                Label lblgrBAmt = (Label)e.Row.FindControl("lblgrBAmt");
                Label txtgvBep = (Label)e.Row.FindControl("txtgvBep");
                Label lblgrMAmt = (Label)e.Row.FindControl("lblgrMAmt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }

                if (ASTUtility.Left(code, 4) == "0101" && ASTUtility.Right(code, 3) == "001")
                {

                    lblgvItmCodsalrev.BackColor = System.Drawing.Color.Green;
                    txtgvItemdescsalrev.BackColor = System.Drawing.Color.Green;
                    lgUnitnum4.BackColor = System.Drawing.Color.Green;
                    txtglsizessalrev.BackColor = System.Drawing.Color.Green;
                    Txtgratesalrev.BackColor = System.Drawing.Color.Green;
                    lblgvamtsalrev.BackColor = System.Drawing.Color.Green;
                    lblgrBAmt.BackColor = System.Drawing.Color.Green;
                    txtgvBep.ForeColor = System.Drawing.Color.Red;
                    lblgrMAmt.BackColor = System.Drawing.Color.Green;


                    lblgvItmCodsalrev.ForeColor = System.Drawing.Color.White;
                    txtgvItemdescsalrev.ForeColor = System.Drawing.Color.White;
                    lgUnitnum4.ForeColor = System.Drawing.Color.White;
                    txtglsizessalrev.ForeColor = System.Drawing.Color.White;
                    Txtgratesalrev.ForeColor = System.Drawing.Color.White;
                    lblgvamtsalrev.ForeColor = System.Drawing.Color.White;
                    lblgrBAmt.ForeColor = System.Drawing.Color.White;

                    lblgrMAmt.ForeColor = System.Drawing.Color.White;


                }

            }
        }
    }
}