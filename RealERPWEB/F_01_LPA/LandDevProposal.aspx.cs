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
namespace RealERPWEB.F_01_LPA
{
    public partial class LandDevProposal : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Land Development Proposal";
                //this.Master.Page.Title = "Land Development Proposal";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            }
        }
        //private void TableCreate()
        //{
        //    DataTable tblt01 = new DataTable();
        //    tblt01.Columns.Add("infcod", Type.GetType("System.String"));
        //    tblt01.Columns.Add("amt01", Type.GetType("System.Double"));
        //    tblt01.Columns.Add("amt02", Type.GetType("System.Double"));
        //    tblt01.Columns.Add("amt03", Type.GetType("System.Double"));
        //    ViewState["tblt01"] = tblt01;

        //    //actcode,subcode,spclcode,actdesc,subdesc,spcldesc,trnqty,trnrate,trndram,trncram,trnrmrk
        //}
        //private void Calculation()
        //{
        //    DataTable tblt01 = (DataTable)ViewState["tblt01"];


        //    //DataRow dr1 = tblt01.NewRow();
        //    //dr1["infcod"] = dgAccCode;
        //    //dr1["amt01"] = dgTrnQty;
        //    //dr1["amt02"] = dgTrnrate;
        //    //dr1["amt03"] = dgTrnrate;
        //    //tblt01.Rows.Add(dr1);
        //}

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
            Session.Remove("tblpro");
            string comcod = this.GetComCode();
            string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            Session["tblpro"] = ds1.Tables[0];
            ds1.Dispose();
        }


        private void GetProNameCopy()
        {
            string comcod = this.GetComCode();
            string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            string Project = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDCOPYPPROJECT", Project, Filter1, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlCopyProjectName.DataSource = null;
                this.ddlCopyProjectName.DataBind();
                return;
            }
            this.ddlCopyProjectName.DataTextField = "infdesc";
            this.ddlCopyProjectName.DataValueField = "infcod";
            this.ddlCopyProjectName.DataSource = ds1.Tables[0];
            this.ddlCopyProjectName.DataBind();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.ProjectName();
        }
        protected void ibtnCopyFindProject_Click(object sender, EventArgs e)
        {

            this.GetProNameCopy();
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
                this.ProjectCDate();
                return;
            }
            this.lbtnOk.Text = "Ok";

            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lblMsg2.Text = "";
            this.rbtnList1.SelectedIndex = -1;
            this.MultiView1.ActiveViewIndex = -1;
            this.ChkCopy.Checked = false;
            this.gvProjectInfo.DataSource = null;
            this.gvProjectInfo.DataBind();
            this.gvFeaPrj.DataSource = null;
            this.gvFeaPrj.DataBind();
            this.gvFeaPrjC.DataSource = null;
            this.gvFeaPrjC.DataBind();
            this.gvFeaLOwner.DataSource = null;
            this.gvFeaLOwner.DataBind();
            this.gvFeaPrjRep.DataSource = null;
            this.gvFeaPrjRep.DataBind();
            this.gvFeaPrjFC.DataSource = null;
            this.gvFeaPrjFC.DataBind();
            this.gvFeaPrjFCS.DataSource = null;
            this.gvFeaPrjFCS.DataBind();
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.PanelSelName.Visible = false;
            this.PnlCopyProject.Visible = false;
            this.ddlCopyProjectName.Items.Clear();
            this.ChkCopy.Visible = false;

        }

        protected void lbtnCopyData_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = hst["comcod"].ToString();
            string Copypactcode = this.ddlCopyProjectName.SelectedValue.ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSORUPLPPROCOPY", Copypactcode, pactcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
          ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }




        private void ProjectLock()
        {

            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LNDPROJECTLOCK", pactcode, "", "", "", "", "", "", "", "");
            this.lblFeaProLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();

        }
        private void ProjectCDate()
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "SHOWPCDATE", pactcode, "", "", "", "", "", "", "", "");
            this.txtDate.Text = (ds1.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["cdate"]).ToString("dd-MMM-yyyy");


        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.chkAllRes.Checked)
            //{
            //    ViewState.Remove("tblfeaprj");
            //    string comcod = this.GetComCode();
            //    string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //    string Code = (this.rbtnList1.SelectedIndex == 1) ? "infcod like '01%'" : (this.rbtnList1.SelectedIndex == 2) ? "(infcod like '0[2-9]%'  or infcod like '1[0-9]%'  or infcod like '2[0-9]%' or infcod like '3[0-9]%'  or infcod like '4[0-9]%')" : "infcod like '5[1-2]%'";
            //    DataSet ds3 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "FEAPRANDPRJCTLDP", pactcode, Code, "", "", "", "", "", "", "");
            //    ViewState["tblfeaprj"] = ds3.Tables[0];
            //    this.Data_Bind();
            //}

            //else
            //{
            //    this.ShowRevenue();
            //}



            string comcod = this.GetComCode();
            string selectindex = this.rbtnList1.SelectedIndex.ToString();
            if (this.chkAllRes.Checked)
            {


                switch (selectindex)
                {


                    case "1":
                        this.ShowRevneueAll();
                        break;


                    case "2":
                        this.ShowCostAll();

                        break;

                    case "3":
                        this.ShowLandBenifitAll();

                        break;

                }

            }

            else
            {

                switch (selectindex)
                {


                    case "1":
                        this.ShowRevenue();
                        break;


                    case "2":
                        this.ShowCost();

                        break;

                    case "3":
                        this.ShowLandBenifit();

                        break;

                }
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
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            ReportDocument rpcp = new RealERPRPT.R_01_LPA.RptFeaLandDevProposal();//RptFeaProject();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int rindex = this.rbtnList1.SelectedIndex;
            switch (rindex)
            {
                case 0:
                    this.ChkCopy.Visible = true;
                    this.GetProjectInfo();
                    this.chkAllRes.Visible = false;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;
                case 1:
                    this.ChkCopy.Visible = false;
                    this.ShowRevenue();
                    this.chkAllRes.Visible = true;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;
                case 2:
                    this.ChkCopy.Visible = false;
                    this.ShowCost();
                    this.chkAllRes.Visible = true;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;
                case 3:
                    this.ChkCopy.Visible = false;
                    this.ShowLandBenifit();
                    this.chkAllRes.Visible = true;
                    this.MultiView1.ActiveViewIndex = rindex;
                    break;
                case 4:
                    this.ChkCopy.Visible = false;
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


            string fpactcode = this.ddlProjectName.SelectedValue.ToString();

            // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();

            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "LDPROJECTINFO", pactcode, fpactcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;
            }

            //DataSet dsTmp = (DataSet)(dataGridView1.DataSource);   //<--- it is OK 

            //DataView dv = ds1.Tables[0].DefaultView;
            //dv.RowFilter = string.Format("prgcod LIKE '03%'");



            this.gvProjectInfo.DataSource = ds1.Tables[0];
            this.gvProjectInfo.DataBind();
            ((CheckBox)this.gvProjectInfo.FooterRow.FindControl("chkProjectLock")).Checked = (this.lblFeaProLock.Text == "True") ? true : false;

            if (Request.QueryString["Type"].ToString() == "LandEntry")
            {
                string tf = this.lblFeaProLock.Text;
                ((LinkButton)this.gvProjectInfo.FooterRow.FindControl("lUpdatProInfo")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
                ((CheckBox)this.gvProjectInfo.FooterRow.FindControl("chkProjectLock")).Enabled = false;
            }

        }
        protected void lUpdatProInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string fpactcode = this.ddlProjectName.SelectedValue.ToString();
            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();


            bool result = false;
            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? (0 + Gvalue).ToString() : Gvalue;

                result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSORUPLDPRJINF", pactcode, Gcode, gtype, Gvalue, fpactcode, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }





         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            string Projectlock = (((CheckBox)this.gvProjectInfo.FooterRow.FindControl("chkProjectLock")).Checked) ? "1" : "0";
            result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INORUPLNDPROJECTLOCK", pactcode, Projectlock, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSETLDPLOG", pactcode, date, userid, Terminal, Sessionid, Posteddat, "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            string pcdate = this.txtDate.Text.Trim();
            result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSOUPPCDATE", pactcode, pcdate, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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


        private void ShowRevenue()
        {


            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '01%'";
            string CostOrSale = "82%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPROSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrj.DataSource = null;
                this.gvFeaPrj.DataBind();
                this.gvFeaPrjFCS.DataSource = null;
                this.gvFeaPrjFCS.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblfeaprjRev"] = ds2.Tables[1];
            ViewState["tblsaldis"] = ds2.Tables[2];
            ViewState["tblfeaprjcal"] = ds2.Tables[4];
            this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            this.gvFeaPrjFCS.DataBind();

            this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";

            if (ds2.Tables[1].Rows.Count != 0)
                ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");


            this.Data_Bind();
        }

        private void ShowCost()
        {

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "(infcod like '0[2-9]%'  or infcod like '1[0-9]%'  or infcod like '2[0-9]%' or infcod like '3[0-9]%'  or infcod like '4[0-9]%')";
            string CostOrSale = "81%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPROSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrjC.DataSource = null;
                this.gvFeaPrjC.DataBind();
                this.gvFeaPrjFC.DataSource = null;
                this.gvFeaPrjFC.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblfeaprjcal"] = ds2.Tables[4];
            this.gvFeaPrjFC.DataSource = ds2.Tables[1];
            this.gvFeaPrjFC.DataBind();
            if (ASTUtility.Left(comcod, 1) == "2")
            {
                this.gvFeaPrjFC.Columns[8].Visible = false;
            }
            if (ASTUtility.Left(comcod, 1) != "2")
            {
                this.gvFeaPrjFC.Columns[12].Visible = false;
            }
            ds2.Dispose();
            if (ds2.Tables[1].Rows.Count != 0)
            {
                ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(percntge)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(percntge)", ""))).ToString("#,##0.00;(#,##0.00); ") + " %";
            }
            this.Data_Bind();


        }

        private void ShowLandBenifit()
        {
            //sircode like '0[89]%'  or  sircode like '1[0-9]%'

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '5[1-2]%'";
            string CostOrSale = "";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPROSALES", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaLOwner.DataSource = null;
                this.gvFeaLOwner.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ds2.Dispose();
            this.Data_Bind();
        }

        private void ShowRevneueAll()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '01%'";
            string CostOrSale = "82%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "FEAPRANDPRJCTLDP", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrj.DataSource = null;
                this.gvFeaPrj.DataBind();
                this.gvFeaPrjFCS.DataSource = null;
                this.gvFeaPrjFCS.DataBind();
                this.gvlpsaldis.DataSource = null;
                this.gvlpsaldis.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblfeaprjRev"] = ds2.Tables[1];
            ViewState["tblsaldis"] = ds2.Tables[2];
            ViewState["tblfeaprjcal"] = ds2.Tables[4];
            this.gvFeaPrjFCS.DataSource = ds2.Tables[1];
            this.gvFeaPrjFCS.DataBind();
            //this.gvlpsaldis.DataSource = ds2.Tables[2];
            //this.gvlpsaldis.DataBind();

            this.lblLownerval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["lownershare"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lblCompanyval.Text = Convert.ToDouble(ds2.Tables[3].Rows[0]["comshare"]).ToString("#,##0.00;(#,##0.00); ") + "%";

            if (ds2.Tables[1].Rows.Count != 0)
                ((Label)this.gvFeaPrjFCS.FooterRow.FindControl("lgvFtssfts")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
            //if (ds2.Tables[2].Rows.Count != 0)
            //{

            //    ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFtotalshsd")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[2].Compute("Sum(lsizes)", "")) ?
            //                      0.00 : ds2.Tables[2].Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFlownershsd")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[2].Compute("Sum(lowner)", "")) ?
            //        0.00 : ds2.Tables[2].Compute("Sum(lowner)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFcompanyshsd")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[2].Compute("Sum(company)", "")) ?
            //      0.00 : ds2.Tables[2].Compute("Sum(company)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFpfrmlownershsd")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[2].Compute("Sum(pflowner)", "")) ?
            //      0.00 : ds2.Tables[2].Compute("Sum(pflowner)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFadjmntsd")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[2].Compute("Sum(adjmnt)", "")) ?
            //      0.00 : ds2.Tables[2].Compute("Sum(adjmnt)", ""))).ToString("#,##0;(#,##0); ");
            //    ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFtcompanyshsd")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[2].Compute("Sum(totalcom)", "")) ?
            //      0.00 : ds2.Tables[2].Compute("Sum(totalcom)", ""))).ToString("#,##0;(#,##0); ");
            //}


            this.Data_Bind();
        }

        private void ShowCostAll()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "(infcod like '0[2-9]%'  or infcod like '1[0-9]%'  or infcod like '2[0-9]%' or infcod like '3[0-9]%'  or infcod like '4[0-9]%')";
            string CostOrSale = "81%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "FEAPRANDPRJCTLDP", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaPrjC.DataSource = null;
                this.gvFeaPrjC.DataBind();
                this.gvFeaPrjFC.DataSource = null;
                this.gvFeaPrjFC.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ViewState["tblfeaprjcal"] = ds2.Tables[4];
            this.gvFeaPrjFC.DataSource = ds2.Tables[1];
            this.gvFeaPrjFC.DataBind();

            ds2.Dispose();
            if (ds2.Tables[1].Rows.Count != 0)
            {
                ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(tcsizes)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(tcsizes)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(percntge)", "")) ? 0.00 : ds2.Tables[1].Compute("Sum(percntge)", ""))).ToString("#,##0.00;(#,##0.00); ") + " %";
            }
            this.Data_Bind();
        }
        private void ShowLandBenifitAll()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Code = "infcod like '5[1-2]%'";
            string CostOrSale = "";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "FEAPRANDPRJCTLDP", pactcode, Code, CostOrSale, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaLOwner.DataSource = null;
                this.gvFeaLOwner.DataBind();

            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void ShowReport()
        {
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //RPTPRJFEALDP
            // DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITY", pactcode, "", "", "", "", "", "", "", "");
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "RPTPRJFEALDP", pactcode, "", "", "", "", "", "", "", "");
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
            int rindex = this.rbtnList1.SelectedIndex;
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            DataTable dt1 = (DataTable)ViewState["tblsaldis"];
            switch (rindex)
            {

                case 1:
                    this.gvlpsaldis.DataSource = dt1;
                    this.gvlpsaldis.DataBind();

                    if (dt1.Rows.Count != 0)
                    {
                        DataView dv = dt1.Copy().DefaultView;
                        dv.RowFilter = "infcod like('8301%')";
                        DataTable dts = dv.ToTable();
                        ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFcompanyshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(company)", "")) ?
                          0.00 : dts.Compute("Sum(company)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFtcompanyshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(totalcom)", "")) ?
                          0.00 : dts.Compute("Sum(totalcom)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFtotalshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(lsizes)", "")) ?
                                              0.00 : dts.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFlownershsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(lowner)", "")) ?
                            0.00 : dts.Compute("Sum(lowner)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFpfrmlownershsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(pflowner)", "")) ?
                          0.00 : dts.Compute("Sum(pflowner)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFadjmntsd")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(adjmnt)", "")) ?
                          0.00 : dts.Compute("Sum(adjmnt)", ""))).ToString("#,##0;(#,##0); ");

                    }



                    this.gvFeaPrj.DataSource = dt;
                    this.gvFeaPrj.DataBind();
                    if (Request.QueryString["Type"].ToString() == "LandEntry")
                    {
                        ((LinkButton)this.gvFeaPrj.FooterRow.FindControl("lbtnFUpdateSales")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;

                    }
                    this.FooterCal();
                    break;

                case 2:


                    this.gvFeaPrjC.DataSource = dt;
                    this.gvFeaPrjC.DataBind();
                    if (Request.QueryString["Type"].ToString() == "LandEntry")
                    {
                        ((LinkButton)this.gvFeaPrjC.FooterRow.FindControl("lbtnfUpdateCost")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
                    }
                    this.FooterCal();
                    break;

                case 3:
                    this.gvFeaLOwner.DataSource = dt;
                    this.gvFeaLOwner.DataBind();
                    if (Request.QueryString["Type"].ToString() == "LandEntry")
                    {
                        if (dt.Rows.Count > 0)
                            ((LinkButton)this.gvFeaLOwner.FooterRow.FindControl("lbtnfUpdateLOwner")).Enabled = (this.lblFeaProLock.Text == "True") ? false : true;
                    }
                    this.FooterCal();
                    break;

                case 4:
                    this.gvFeaPrjRep.DataSource = dt;
                    this.gvFeaPrjRep.DataBind();
                    break;



            }


        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            if (dt.Rows.Count == 0)
                return;
            double Stotalsize, stotalAmt;
            int rindex = this.rbtnList1.SelectedIndex;
            switch (rindex)
            {

                case 1:

                    DataView dv = dt.Copy().DefaultView;
                    dv.RowFilter = "infcod like('0101%') or infcod like('0102%')";
                    DataTable dts = dv.ToTable();
                    Stotalsize = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(tsize)", "")) ?
                    0.00 : dts.Compute("Sum(tsize)", "")));
                    stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt01)", "")) ? 0.00 : dt.Compute("Sum(amt01)", "")));
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFTsize")).Text = Stotalsize.ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFSratepsft01")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt01")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt02)", "")) ?
                        0.00 : dt.Compute("Sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt03)", "")) ?
                        0.00 : dt.Compute("Sum(amt03)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case 2:
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFSFT")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(persft)", "")) ?
                       0.00 : dt.Compute("Sum(persft)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc01")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt01)", "")) ?
                        0.00 : dt.Compute("Sum(amt01)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt02)", "")) ?
                        0.00 : dt.Compute("Sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt03)", "")) ?
                        0.00 : dt.Compute("Sum(amt03)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case 3:
                    Stotalsize = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsize)", "")) ?
                    0.00 : dt.Compute("Sum(tsize)", "")));
                    stotalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt01)", "")) ?
                    0.00 : dt.Compute("Sum(amt01)", "")));
                    ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFTsizel")).Text = Stotalsize.ToString("#,##0;(#,##0); ");
                    // ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFsalratel01")).Text = ((Stotalsize == 0) ? "" : (stotalAmt / Stotalsize).ToString("#,##0;(#,##0); "));
                    ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFAmtl01")).Text = stotalAmt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFAmtl02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt02)", "")) ?
                        0.00 : dt.Compute("Sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFeaLOwner.FooterRow.FindControl("lgvFAmtl03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt03)", "")) ?
                        0.00 : dt.Compute("Sum(amt03)", ""))).ToString("#,##0;(#,##0); ");


                    break;
            }

        }





        protected void lbtnFUpdateSales_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.UpdateProjectSaleAndCost();
        }

        private void UpdateProjectSaleAndCost()
        {

            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string ResCode = dt.Rows[i]["infcod"].ToString();
                string Size = Convert.ToDouble(dt.Rows[i]["rsize"]).ToString();
                string Number = Convert.ToDouble(dt.Rows[i]["number"]).ToString();
                string flrno = dt.Rows[i]["flrno"].ToString();
                string Quantity = Convert.ToDouble(dt.Rows[i]["tsize"]).ToString();
                double Amt01 = Convert.ToDouble(dt.Rows[i]["amt01"]);
                string salrate01 = Convert.ToDouble(dt.Rows[i]["salrate01"]).ToString();
                double Amt02 = Convert.ToDouble(dt.Rows[i]["amt02"]);
                string salrate02 = Convert.ToDouble(dt.Rows[i]["salrate02"]).ToString();
                double Amt03 = Convert.ToDouble(dt.Rows[i]["amt03"]);
                string salrate03 = Convert.ToDouble(dt.Rows[i]["salrate03"]).ToString();

                if (Amt01 > 0 || Amt02 > 0 || Amt03 > 0)
                {


                    bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INORUPDFEAPRJCT", PactCode, ResCode, Size, Number, Amt01.ToString(),
                            salrate01, Quantity, Amt02.ToString(), salrate02, Amt03.ToString(), salrate03, flrno, "", "", "");
                }
            }

            if (this.rbtnList1.SelectedIndex == 1)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                this.ShowRevenue();
            }
            else
            {
                this.lblMsg2.Text = "Updated Successfully";
                this.ShowCost();
            }
            //(this.rbtnList1.SelectedIndex = 1) ? this.ShowRevenue() : this.ShowCost();
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
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "DELETELDPITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod not in('" + Itemcode + "')";
            ViewState.Remove("tblfeaprj");
            ViewState["tblfeaprj"] = dv.ToTable();
            this.Data_Bind();
        }



        protected void lbtnTotalsd_Click(object sender, EventArgs e)
        {
            double plownershare = Convert.ToDouble("0" + this.lblLownerval.Text.Trim().Replace("%", ""));
            double pcomshare = Convert.ToDouble("0" + this.lblCompanyval.Text.Trim().Replace("%", ""));
            DataTable dt = (DataTable)ViewState["tblsaldis"];
            for (int i = 0; i < this.gvlpsaldis.Rows.Count; i++)
            {
                string Code = ((Label)this.gvlpsaldis.Rows[i].FindControl("lblgvItmCodsd")).Text.Trim();
                string Desc = ((TextBox)this.gvlpsaldis.Rows[i].FindControl("txtgvItemdescsd")).Text.Trim();
                double tosize = Convert.ToDouble("0" + ((TextBox)this.gvlpsaldis.Rows[i].FindControl("txtgvSizesd")).Text.Trim());
                double Purflowner = Convert.ToDouble("0" + ((TextBox)this.gvlpsaldis.Rows[i].FindControl("txtgvpurfrmlanownersd")).Text.Trim());
                double adjmnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvlpsaldis.Rows[i].FindControl("txtgvadjsd")).Text.Trim()));

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

            }
            ViewState["tblsaldis"] = dt;
            this.Data_Bind();
            //this.gvlpsaldis.DataSource = dt;
            //this.gvlpsaldis.DataBind();


            //((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFtotalshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lsizes)", "")) ?
            //                  0.00 : dt.Compute("Sum(lsizes)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFlownershsd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lowner)", "")) ?
            //    0.00 : dt.Compute("Sum(lowner)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFcompanyshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(company)", "")) ?
            //  0.00 : dt.Compute("Sum(company)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFpfrmlownershsd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pflowner)", "")) ?
            //  0.00 : dt.Compute("Sum(pflowner)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFadjmntsd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(adjmnt)", "")) ?
            //  0.00 : dt.Compute("Sum(adjmnt)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvlpsaldis.FooterRow.FindControl("lgvFtcompanyshsd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalcom)", "")) ?
            //  0.00 : dt.Compute("Sum(totalcom)", ""))).ToString("#,##0;(#,##0); ");
            this.lblMsg0.Text = "";

        }
        private void RevPart()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            double plownershare = Convert.ToDouble("0" + this.lblLownerval.Text.Trim().Replace("%", ""));
            double pcomshare = Convert.ToDouble("0" + this.lblCompanyval.Text.Trim().Replace("%", ""));


            for (int i = 0; i < this.gvlpsaldis.Rows.Count; i++)
            {
                string Code = ((Label)this.gvlpsaldis.Rows[i].FindControl("lblgvItmCodsd")).Text.Trim();
                string Desc = ((TextBox)this.gvlpsaldis.Rows[i].FindControl("txtgvItemdescsd")).Text.Trim();
                double tosize = Convert.ToDouble("0" + ((TextBox)this.gvlpsaldis.Rows[i].FindControl("txtgvSizesd")).Text.Trim());
                double Purflowner = Convert.ToDouble("0" + ((TextBox)this.gvlpsaldis.Rows[i].FindControl("txtgvpurfrmlanownersd")).Text.Trim());
                double adjmnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvlpsaldis.Rows[i].FindControl("txtgvadjsd")).Text.Trim()));

                double lownershare = tosize * plownershare * 0.01;
                double comshare = tosize * pcomshare * 0.01;


                if (Code == "830200101001")
                {

                    DataRow[] dr2 = dt.Select("infcod='" + "010200101001" + "'");
                    if (dr2.Length > 0)
                        dr2[0]["tsize"] = comshare + Purflowner + adjmnt;
                }
                else if (Code == "830300101001")
                {

                    DataRow[] dr3 = dt.Select("infcod='" + "010300101001" + "'");
                    if (dr3.Length > 0)
                        dr3[0]["tsize"] = comshare + Purflowner + adjmnt;
                }
                else if (Code == "830300101003")
                {

                    DataRow[] dr4 = dt.Select("infcod='" + "010300301001" + "'");
                    if (dr4.Length > 0)
                        dr4[0]["tsize"] = comshare + Purflowner + adjmnt;
                }
                else if (Code == "830300101005")
                {

                    DataRow[] dr5 = dt.Select("infcod='" + "010300501001" + "'");
                    if (dr5.Length > 0)
                        dr5[0]["tsize"] = comshare + Purflowner + adjmnt;
                }
                else if (Code == "830300101008")
                {

                    DataRow[] dr6 = dt.Select("infcod='" + "010300801001" + "'");
                    if (dr6.Length > 0)
                        dr6[0]["tsize"] = comshare + Purflowner + adjmnt;
                }
                else if (Code == "830400101001")
                {

                    DataRow[] dr6 = dt.Select("infcod='" + "010400101001" + "'");
                    if (dr6.Length > 0)
                        dr6[0]["tsize"] = comshare + Purflowner + adjmnt;
                }
            }
            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void lbtnFUpdateSalessd_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.lblMsg0.Text = "You have no permission";
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

                //if (lsizes > 0)
                //{
                //feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPDATEFEAPRJCT", PactCode, ResCode, Size, Number, Amt, salrate, "", "", "", "", "", "", "","","");
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INORUPLPSALDIS", PactCode, ResCode, Desc, lsizes.ToString(), lowner, company, Pflowner, adjmnt, "", "", "", "", "", "", "");
                // }


            }

            string selectindex = this.rbtnList1.SelectedIndex.ToString();
            if (this.chkAllRes.Checked)
            {
                switch (selectindex)
                {
                    case "1":
                        this.ShowRevneueAll();
                        break;
                }
            }

            this.RevPart();
            this.lblMsg0.Text = "Updated Successfully";
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

        }
        protected void UpdateSession()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprjRev"];

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



                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["infdesc"] = Desc;
                dr1[0]["lsizek"] = lsizek;
                dr1[0]["lsizes"] = sftperf;
                dr1[0]["tsizes"] = tsizes;
                dr1[0]["tcsizes"] = tcsizesft;
                dr1[0]["bqty"] = bqty;
                dr1[0]["bdesc"] = bdesc;
            }
            ViewState["tblfeaprjRev"] = dt;
            this.Data_Bind();

        }
        protected void lbtnfUpdateCostFCS_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            this.UpdateSession();
            DataTable dt = (DataTable)ViewState["tblfeaprjRev"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Code = dt.Rows[i]["infcod"].ToString();
                string Desc = dt.Rows[i]["infdesc"].ToString();
                string lsizek = dt.Rows[i]["lsizek"].ToString();
                string sftperf = dt.Rows[i]["lsizes"].ToString();
                double tsizes = Convert.ToDouble(dt.Rows[i]["tsizes"]);
                string tcsizesft = dt.Rows[i]["tcsizes"].ToString();
                string bqty = dt.Rows[i]["bqty"].ToString();
                string bdesc = dt.Rows[i]["bdesc"].ToString();
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INORUPDATELPPRJCTFC", PactCode, Code, Desc, lsizek, sftperf, tsizes.ToString(), tcsizesft.ToString(), bqty, bdesc, "", "", "", "", "", "");

            }
            this.SalesDisCom();
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

        }
        private void SalesDisCom()
        {
            DataTable dt = (DataTable)ViewState["tblsaldis"];
            double tpark = 0.00;
            double landSc = 0.00;

            for (int i = 0; i < this.gvFeaPrjFCS.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrjFCS.Rows[i].FindControl("lblgvItmCodfcs")).Text.Trim();

                string tcsizesft = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvtssfts")).Text.Trim()).ToString();
                double bqty = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFCS.Rows[i].FindControl("txtgvBQTY")).Text.Trim());
                if (ASTUtility.Left(Code, 4) == "8201")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "8301" + ASTUtility.Right(Code, 8) + "'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = tcsizesft;
                }
                if (ASTUtility.Left(Code, 4) == "8201")
                {
                    DataRow[] dr1 = dt.Select("infcod='" + "8303" + ASTUtility.Right(Code, 8) + "'");
                    if (dr1.Length > 0)
                        dr1[0]["lsizes"] = bqty;
                }
                if (ASTUtility.Left(Code, 4) == "8202")
                {
                    tpark = tpark + bqty;
                }
                if (Code == "820100101003" || Code == "820100101005" || Code == "820100101008")
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


            ViewState["tblsaldis"] = dt;
            this.Data_Bind();
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            DataTable dta = (DataTable)ViewState["tblfeaprjcal"];
            double Amt01, Amt02, Amt03, size;

            for (int i = 0; i < this.gvFeaPrj.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrj.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                string flrno = ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvnum")).Text.Trim();
                double percent = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtSize")).Text.Trim());
                double tsize = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgtsize")).Text.Trim());
                double salrate01 = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvsalrate01")).Text.Trim());
                double salrate02 = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvsalrate02")).Text.Trim());
                double salrate03 = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrj.Rows[i].FindControl("txtgvsalrate03")).Text.Trim());


                string unit = ((Label)this.gvFeaPrj.Rows[i].FindControl("lgUnitnum")).Text.Trim();


                if (unit == "%" & percent != 0)
                {
                    DataRow[] dr11 = dta.Select("infcod='" + Code + "'");
                    if (dr11.Length > 0)
                    {

                        tsize = ((Convert.ToDouble(dr11[0]["amt01"]) * percent) / 100);
                    }
                }


                Amt01 = tsize * salrate01;
                Amt02 = tsize * salrate02;
                Amt03 = tsize * salrate03;

                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["rsize"] = percent;
                dr1[0]["flrno"] = flrno;
                dr1[0]["tsize"] = tsize;
                dr1[0]["amt01"] = Amt01;
                dr1[0]["salrate01"] = salrate01;
                dr1[0]["amt02"] = Amt02;
                dr1[0]["salrate02"] = salrate02;
                dr1[0]["amt03"] = Amt03;
                dr1[0]["salrate03"] = salrate03;
            }
            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }
        protected void lbtnTotalCostFC_Click(object sender, EventArgs e)
        {
            double toamt = 0.00, tparcent = 0.00;
            for (int i = 0; i < this.gvFeaPrjFC.Rows.Count; i++)
            {
                double landsize = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvlandsize")).Text.Trim());
                double sftperf = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvsftperf")).Text.Trim());
                double stonum = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvstonum")).Text.Trim());
                double tcsizesft = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvtcsft")).Text.Trim());
                double tsizes = (stonum == 0) ? 0 : (tcsizesft / stonum);
                ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvpercntge")).Text = (sftperf == 0) ? "0" : Convert.ToDouble(((tsizes * 100) / sftperf)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvlsizes")).Text = tsizes.ToString("#,##0;(#,##0); ");
                toamt = toamt + tcsizesft;
                ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lblPercent")).Text = (landsize == 0) ? "0" : Convert.ToDouble(tcsizesft * 100 / landsize).ToString("#,##0.00;(#,##0.00); ");

                double per = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lblPercent")).Text.Trim());
                tparcent = tparcent + per;
            }

            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFtcsft")).Text = toamt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjFC.FooterRow.FindControl("lgvFPercent")).Text = tparcent.ToString("#,##0;(#,##0); ");
        }
        protected void lbtnfUpdateCostFC_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                double nofflr = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvstonum")).Text.Trim());
                double tsizes = Convert.ToDouble("0" + ((Label)this.gvFeaPrjFC.Rows[i].FindControl("lgvlsizes")).Text.Trim());
                string tcsizesft = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjFC.Rows[i].FindControl("txtgvtcsft")).Text.Trim()).ToString();

                //if (nofflr > 0)
                //{

                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INORUPDATELPPRJCTFC", PactCode, Code, Desccription, lsizek, sftperf, tsizes.ToString(), tcsizesft, "0", "", "", "", "", "", "", "");
                //}
            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            DataTable dta = (DataTable)ViewState["tblfeaprjcal"];
            //DataTable dt4;
            //DataView dv1;
            for (int i = 0; i < this.gvFeaPrjC.Rows.Count; i++)
            {

                string Code = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lblgvItmCodc")).Text.Trim();
                double Size = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgtsizec")).Text.Trim());
                double salrate01 = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgsalratec01")).Text.Trim());
                double Amt01 = Convert.ToDouble("0" + ((Label)this.gvFeaPrjC.Rows[i].FindControl("txtgvAmtc01")).Text.Trim());
                double salrate02 = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgsalratec02")).Text.Trim());
                double Amt02 = Convert.ToDouble("0" + ((Label)this.gvFeaPrjC.Rows[i].FindControl("txtgvAmtc02")).Text.Trim());
                double salrate03 = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgsalratec03")).Text.Trim());
                double Amt03 = Convert.ToDouble("0" + ((Label)this.gvFeaPrjC.Rows[i].FindControl("txtgvAmtc03")).Text.Trim());

                string unit = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lgUnitnum0")).Text.Trim();
                //dt4 = dt.Copy();
                //dv1 = dt4.DefaultView;
                //dv1.RowFilter = ("unit = '%'");
                //dt4 = dv1.ToTable();
                if (unit == "%")
                {
                    DataRow[] dr11 = dta.Select("infcod='" + Code + "'");
                    if (dr11.Length > 0)
                    {
                        Amt01 = Convert.ToDouble(dr11[0]["amt01"]) * (salrate01 / 100);
                        Amt02 = Convert.ToDouble(dr11[0]["amt02"]) * (salrate02 / 100);
                        Amt03 = Convert.ToDouble(dr11[0]["amt03"]) * (salrate03 / 100);
                    }
                    else
                    {
                        Amt01 = Size * (salrate01 / 100);
                        Amt02 = Size * (salrate02 / 100);
                        Amt03 = Size * (salrate03 / 100);
                    }
                }


                else
                {
                    //if (Amt01 > 0)
                    //{
                    //    salrate01 = Amt01 / Size;

                    //}
                    //else if (salrate01 > 0)
                    Amt01 = Size * salrate01;

                    //if (Amt02 > 0)
                    //{
                    //    salrate02 = Amt02 / Size;

                    //}
                    //else if (salrate02 > 0)
                    Amt02 = Size * salrate02;

                    //if (Amt03 > 0)
                    //{
                    //    salrate03 = Amt03 / Size;

                    //}
                    //else if (salrate03 > 0)
                    Amt03 = Size * salrate03;
                }
                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["tsize"] = Size;
                dr1[0]["amt01"] = Amt01;
                dr1[0]["salrate01"] = salrate01;
                dr1[0]["amt02"] = Amt02;
                dr1[0]["salrate02"] = salrate02;
                dr1[0]["amt03"] = Amt03;
                dr1[0]["salrate03"] = salrate03;
            }

            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
            this.lblMsg2.Text = "";
        }
        protected void lbtnfUpdateCost_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.UpdateProjectSaleAndCost();
        }
        protected void gvFeaPrjC_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvFeaPrjC.Rows[e.RowIndex].FindControl("lblgvItmCodc")).Text.Trim();

            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "DELETELDPITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

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
        protected void lbtnTotalLOwner_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            for (int i = 0; i < this.gvFeaLOwner.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaLOwner.Rows[i].FindControl("lblgvItmCodl")).Text.Trim();
                double number = Convert.ToDouble("0" + ((TextBox)this.gvFeaLOwner.Rows[i].FindControl("txtgvnuml")).Text.Trim());
                double tsize = Convert.ToDouble("0" + ((TextBox)this.gvFeaLOwner.Rows[i].FindControl("txtgtsizel")).Text.Trim());
                double salrate01 = Convert.ToDouble("0" + ((TextBox)this.gvFeaLOwner.Rows[i].FindControl("txtgsalratel01")).Text.Trim());
                double salrate02 = Convert.ToDouble("0" + ((TextBox)this.gvFeaLOwner.Rows[i].FindControl("txtgsalratel02")).Text.Trim());
                double salrate03 = Convert.ToDouble("0" + ((TextBox)this.gvFeaLOwner.Rows[i].FindControl("txtgsalratel03")).Text.Trim());

                double size = (number > 0) ? (tsize / number) : 0.00;
                double Amt01 = tsize * salrate01;
                double Amt02 = tsize * salrate02;
                double Amt03 = tsize * salrate03;
                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["rsize"] = size;
                dr1[0]["number"] = number;
                dr1[0]["tsize"] = tsize;
                dr1[0]["amt01"] = Amt01;
                dr1[0]["salrate01"] = salrate01;
                dr1[0]["amt02"] = Amt02;
                dr1[0]["salrate02"] = salrate02;
                dr1[0]["amt03"] = Amt03;
                dr1[0]["salrate03"] = salrate03;
            }

            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void lbtnfUpdateLOwner_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.UpdateProjectSaleAndCost();
        }
        protected void gvFeaLOwner_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvFeaLOwner.Rows[e.RowIndex].FindControl("lblgvItmCodl")).Text.Trim();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "DELETELDPITEME", PactCode, Itemcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

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
                Label lgvAmt01 = (Label)e.Row.FindControl("lgvAmtrep01");
                Label lgvAmt02 = (Label)e.Row.FindControl("lgvAmtrep02");
                Label lgvAmt03 = (Label)e.Row.FindControl("lgvAmtrep03");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    ToSize.Font.Bold = true;
                    // RatepSft.Font.Bold = true;
                    //amt01.Font.Bold = true;
                    //amt02.Font.Bold = true;
                    lgvAmt01.Font.Bold = true;
                    lgvAmt02.Font.Bold = true;
                    lgvAmt03.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");


                }

            }

        }



        protected void ChkCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCopyProject.Visible = this.ChkCopy.Checked;
            if (ChkCopy.Checked)
            {
                this.lblPrjName.Text = "New Project Name";
            }
            else
            {
                this.lblPrjName.Text = "Project Name";
            }

        }


        protected void btnPSameVal_Click(object sender, EventArgs e)
        {
            this.SameValUpdate();
        }
        private void SameValUpdate()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            for (int i = 0; i < this.gvFeaPrjC.Rows.Count; i++)
            {
                string Code = ((Label)this.gvFeaPrjC.Rows[i].FindControl("lblgvItmCodc")).Text.Trim();
                double salrate01 = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgsalratec01")).Text.Trim());
                DataRow[] dr1 = dt.Select("infcod='" + Code + "'");
                dr1[0]["salrate01"] = salrate01;
                dr1[0]["salrate02"] = salrate01;
                dr1[0]["salrate03"] = salrate01;
                dr1[0]["amt01"] = 0.00;
                dr1[0]["amt02"] = 0.00;
                dr1[0]["amt03"] = 0.00;
            }

            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
        }

        protected void gvFeaPrjC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtgtsizec = (TextBox)e.Row.FindControl("txtgtsizec");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }

                if (ASTUtility.Left(code, 2) == "27" || ASTUtility.Left(code, 2) == "29" || ASTUtility.Left(code, 2) == "30")
                {


                    txtgtsizec.ReadOnly = true;
                }

            }
        }

    }
}