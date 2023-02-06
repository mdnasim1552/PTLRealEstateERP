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
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_22_Sal
{
    public partial class MktEntryUnit : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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
                this.gvUnit.Columns[1].Visible = (Convert.ToBoolean(dr1[0]["entry"]));
                if (Request.QueryString["Type"] != null)
                {
                    //((Label)this.Master.FindControl("lblTitle")).Text = "Budget-Sales(Land Owner) ";
                }
                else
                {
                    //((Label)this.Master.FindControl("lblTitle")).Text = "Budget-Sales ";
                }
                
                this.GetProjectName();
                this.GetUnitType();
                this.ChangeName();
                this.ComColumnVisible();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ViewState["PreviousPageUrl"] = this.Request.UrlReferrer.ToString();
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"]; 
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string ddldesc = hst["ddldesc"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAME", txtSProject, userid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlProjectName.DataTextField = TextField;
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        protected void btnType_Click(object sender, EventArgs e)
        {
            this.GetUnitType();
        }
        private void GetUnitType()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETUNITTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlUnitType.DataTextField = "grpdesc";
            this.ddlUnitType.DataValueField = "grpcode";
            this.ddlUnitType.DataSource = ds1.Tables[0];
            this.ddlUnitType.DataBind();
            this.ddlUnitType_SelectedIndexChanged(null, null);
        }
        protected void ddlUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetGroup();
        }
        private void GetGroup()
        {
            string comcod = this.GetCompCode();
            string unitType = this.ddlUnitType.SelectedValue.ToString() + "%";
            string stchgrp = "%" + this.txtSrcMat.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETGROUP", unitType, stchgrp, "", "", "", "", "", "", "");
            this.ddlGroup.DataTextField = "grpdesc";
            this.ddlGroup.DataValueField = "grpcode";
            this.ddlGroup.DataSource = ds1.Tables[0];
            this.ddlGroup.DataBind();
            this.ddlGroup_SelectedIndexChanged(null, null);
        }

        private void ChangeName()
        {
            string comcod = this.GetCompCode();
            // this.gvUnit.Columns[10].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Parking";
            //this.gvUnit.Columns[14].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Land Qty" : " Qty";
            this.gvUnit.Columns[15].Visible = (ASTUtility.Left(comcod, 1) == "2") ? false : true;
            this.gvUnit.Columns[16].Visible = (ASTUtility.Left(comcod, 1) == "2") ? false : true;
        }
        private void ComColumnVisible()
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3336":
                case "3337":
                    // this.gvUnit.Columns[11].Visible = false;
                    //this.gvUnit.Columns[12].Visible = false;
                    this.gvUnit.Columns[16].Visible = false;
                    this.gvUnit.Columns[19].Visible = false;
                    this.gvUnit.Columns[21].Visible = false;
                    this.gvUnit.Columns[22].Visible = false;
                    break;


                case "3340":
                    this.gvUnit.Columns[11].HeaderText = "Association Fee";
                    break;
                default:
                    break;


            }

        }

        private void GetFloor()
        {

            string comcod = this.GetCompCode();
            string grpcode = this.ddlGroup.SelectedValue.ToString() + "%";
            string srchfloor = "%" + this.txtsrchfloor.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETFLOORCODE", grpcode, srchfloor, "", "", "", "", "", "", "");
            this.ddlFloor.DataTextField = "flrdesc";
            this.ddlFloor.DataValueField = "flrcode";
            this.ddlFloor.DataSource = ds1.Tables[0];
            this.ddlFloor.DataBind();
            this.ddlFloor_SelectedIndexChanged(null, null);

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
                return;

            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "Ok")
            {

                //this.PanelGroup.Visible = true;
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                //this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                //this.lblProjectmDesc.Visible = true;
                this.lblProjectdesc.Visible = true;
                //this.ibtnFindProject.Enabled = false;
                this.LoadGrid();


            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.PanelGroup.Visible = false;
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            //this.ibtnFindProject.Enabled = true;
            //this.lblProjectmDesc.Text = "";
            //this.lblProjectmDesc.Visible = false;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvUnit.DataSource = null;
            this.gvUnit.DataBind();



        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        private void SaveValue()
        {

            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tblUnit"];
            for (int i = 0; i < this.gvUnit.Rows.Count; i++)
            {
                string UsirCode = ((Label)this.gvUnit.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                if (ASTUtility.Right(UsirCode, 3) == "000")
                    continue;
                string udescbn = ((TextBox)this.gvUnit.Rows[i].FindControl("txtItemdescbn")).Text.Trim();
                string udesc = ((TextBox)this.gvUnit.Rows[i].FindControl("txtItemdesc")).Text.Trim();
                string munit = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgUnitnum")).Text.Trim();
                string facing = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUFacing")).Text.Trim();
                string view = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUView")).Text.Trim();
                double dUsize = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUSize")).Text.Trim());
                double dRate = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvRate")).Text.Trim());
                double Uamount = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvuamt")).Text.Trim());
                string handvodate= ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvhoverdate")).Text.Trim()==""?"01-Jan-1900" :Convert.ToDateTime(((TextBox)this.gvUnit.Rows[i].FindControl("txtgvhoverdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double handovper = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvhoverper")).Text.Trim());

                double Qty = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUqty")).Text.Trim());
                string status = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvbstat")).Text.Trim();

                double pqty = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvPqty")).Text.Trim());
                double pamt = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvPamt")).Text.Trim());
                double utility = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUtility")).Text.Trim());
                double cooperative = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvPCooprative")).Text.Trim());

                double MinBMoney = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvBookingMoney")).Text.Trim());
                double bookingper = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvBookingper")).Text.Trim());
                int noofinstall = Convert.ToInt32('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvemi")).Text.Trim());


                
                string chkper = (((CheckBox)gvUnit.Rows[i].FindControl("chk")).Checked) ? "True" : "False";



                string Remarsk = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                string fcode = ((TextBox)this.gvUnit.Rows[i].FindControl("txtfcode")).Text.Trim();



                dRate = Uamount > 0 ? Uamount / dUsize : dRate;
                Uamount = Uamount > 0 ? Uamount : dRate * dUsize;

                double tamt = Convert.ToDouble((dUsize * dRate) + pamt + utility + cooperative);
                rowindex = (this.gvUnit.PageSize * this.gvUnit.PageIndex) + i;
                tblt02.Rows[rowindex]["udescbn"] = udescbn;
                tblt02.Rows[rowindex]["udesc"] = udesc;
                tblt02.Rows[rowindex]["munit"] = munit;
                tblt02.Rows[rowindex]["usize"] = dUsize;
                tblt02.Rows[rowindex]["urate"] = dRate;
                tblt02.Rows[rowindex]["uamt"] = Uamount;
                tblt02.Rows[rowindex]["uqty"] = Qty;
                tblt02.Rows[rowindex]["bstat"] = status;
                tblt02.Rows[rowindex]["pqty"] = pqty;
                tblt02.Rows[rowindex]["pamt"] = pamt;
                tblt02.Rows[rowindex]["tamt"] = tamt;
                tblt02.Rows[rowindex]["minbam"] = MinBMoney;
                tblt02.Rows[rowindex]["bookingper"] = bookingper;
                tblt02.Rows[rowindex]["noofinstall"] = noofinstall;
                tblt02.Rows[rowindex]["handovdate"] = handvodate;
                tblt02.Rows[rowindex]["handovper"] = handovper;
                
                tblt02.Rows[rowindex]["urmrks"] = Remarsk;

                tblt02.Rows[rowindex]["facing"] = facing;
                tblt02.Rows[rowindex]["uview"] = view;
                tblt02.Rows[rowindex]["utility"] = utility;
                tblt02.Rows[rowindex]["cooperative"] = cooperative;
                tblt02.Rows[rowindex]["mgtbook"] = chkper;
                tblt02.Rows[rowindex]["fcode"] = fcode;


            }
            ViewState["tblUnit"] = tblt02;


        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('You have no permission');", true);
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblUnit"];

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();




            switch (comcod)
            {

                case "3339":

                    DataView chdv = new DataView();
                    dt = dt.Copy();
                    chdv = dt.DefaultView;
                    chdv.RowFilter = ("usize >0 ");
                    DataTable finaltable = chdv.ToTable();

                    DataView dv = new DataView();
                    finaltable = finaltable.Copy();
                    dv = finaltable.DefaultView;
                    dv.RowFilter = ("fcode <>'' ");
                    DataTable testtble = dv.ToTable();

                    DataView dv1 = new DataView();
                    finaltable = finaltable.Copy();
                    dv1 = finaltable.DefaultView;
                    dv1.RowFilter = ("udesc <>'' ");
                    DataTable desctable = dv1.ToTable();

                    var dtrows = testtble.AsEnumerable().GroupBy(x => x["fcode"]).Where(x => x.Count() > 1).Count();
                    if (dtrows > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Found Duplicate Code');", true);                     
                        return;
                    }

                    var udescdesc = desctable.AsEnumerable().GroupBy(x => x["udesc"]).Where(x => x.Count() > 1).Count();
                    if (udescdesc > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Found Duplicate Description');", true);                      
                        return;
                    }
                    break;


                default:

                    break;
            }







            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string UsirCode = dt.Rows[i]["usircode"].ToString();
                if (ASTUtility.Right(UsirCode, 3) == "000")
                    continue;
                string Udescbn = dt.Rows[i]["udescbn"].ToString();
                string Udesc = dt.Rows[i]["udesc"].ToString();
                string UNumber = dt.Rows[i]["munit"].ToString();
                double dUsize = Convert.ToDouble(dt.Rows[i]["usize"].ToString());
                double Uqty = Convert.ToDouble(dt.Rows[i]["uqty"].ToString());
                double duqty = (Uqty <= 0) ? 1 : Uqty;
                string qty = duqty.ToString();
                string Amt = Convert.ToDouble(dt.Rows[i]["uamt"].ToString()).ToString();
                string bstat = dt.Rows[i]["bstat"].ToString();
                string Uramrks = dt.Rows[i]["urmrks"].ToString();
                string Pqty = dt.Rows[i]["pqty"].ToString();
                string Pamt = dt.Rows[i]["pamt"].ToString();
                string Minbam = dt.Rows[i]["minbam"].ToString();
                string facing = dt.Rows[i]["facing"].ToString();
                string view = dt.Rows[i]["uview"].ToString();
                string utility = dt.Rows[i]["utility"].ToString();
                string cooprative = dt.Rows[i]["cooperative"].ToString();
                string chkper = dt.Rows[i]["mgtbook"].ToString().Trim();
                string fcode = dt.Rows[i]["fcode"].ToString().Trim();
                string isLO = (Request.QueryString["Type"] == null) ? "0" : "1";
                string bookingper = dt.Rows[i]["bookingper"].ToString();
                string noofinstall = dt.Rows[i]["noofinstall"].ToString();
                string handovdate = Convert.ToDateTime(dt.Rows[i]["handovdate"].ToString()).ToString("dd-MMM-yyyy");
                string handovper = dt.Rows[i]["handovper"].ToString();
                

                if (dUsize > 0)
                {
                    bool result = MktData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESALINF", PactCode, UsirCode, UNumber, dUsize.ToString(), qty, Udesc,
                        bstat, Uramrks, Amt, Pqty, Pamt, Minbam, facing, view, utility, cooprative, chkper, fcode, Udescbn, isLO, bookingper, noofinstall, handovdate, handovper, "","","","","","","","","");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Failed');", true);

                    }


                }







            }
       
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string ddldesc = hst["ddldesc"].ToString();
                string eventtype = "Unit Fixation";
                string eventdesc = "Update Fixation";
                string eventdesc2 = "Project Name: " + ddldesc == "True" ? this.ddlProjectName.SelectedItem.ToString() : this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void LoadGrid()
        {

            ViewState.Remove("tblUnit");

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string group = (this.ddlFloor.SelectedValue.ToString() == "000000000") ? this.ddlGroup.SelectedValue.ToString() + "%" : this.ddlFloor.SelectedValue.ToString() + "%";
            string isLO = (Request.QueryString["Type"] == null) ? "0" : "1";            
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFORMATION", PactCode, group, isLO, "", "", "", "", "", "");
            if (ds4 == null)
                return;
            ViewState["tblUnit"] = ds4.Tables[0];
            this.Data_bind();

        }

        private void Data_bind()
        {
            try

            {

                string comcod = this.GetCompCode();
                DataTable tblt05 = (DataTable)ViewState["tblUnit"];
                this.gvUnit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvUnit.DataSource = tblt05;
                this.gvUnit.DataBind();

                if (tblt05.Rows.Count == 0)
                    return;
                ((Label)this.gvUnit.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(uamt)", "")) ?
                  0.00 : tblt05.Compute("Sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvUnit.FooterRow.FindControl("lFUsize")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(usize)", "")) ?
                  0.00 : tblt05.Compute("Sum(usize)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvUnit.FooterRow.FindControl("lgvPAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(pamt)", "")) ?
                  0.00 : tblt05.Compute("Sum(pamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvUnit.FooterRow.FindControl("lgvTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(tamt)", "")) ?
                  0.00 : tblt05.Compute("Sum(tamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvUnit.FooterRow.FindControl("lgvfAptqty")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(uqty)", "")) ?
               0.00 : tblt05.Compute("Sum(uqty)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvUnit.FooterRow.FindControl("lgvfParkingqty")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(pqty)", "")) ?
               0.00 : tblt05.Compute("Sum(pqty)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvUnit.FooterRow.FindControl("lgvPUtility")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(utility)", "")) ?
              0.00 : tblt05.Compute("Sum(utility)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvUnit.FooterRow.FindControl("lgvPCooprative")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(cooperative)", "")) ?
              0.00 : tblt05.Compute("Sum(cooperative)", ""))).ToString("#,##0;(#,##0); ");
            }
            catch (Exception ex)
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = ex.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


        }
        private string GetComPrint()
        {
            string comcod = this.GetCompCode();
            string ComPrint = "";
            switch (comcod)
            {
                case "3336":
                    ComPrint = "Printsuvastu";
                    break;

                default:
                    ComPrint = "Printgenaral";
                    break;


            }

            return ComPrint;


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ddldesc = hst["ddldesc"].ToString();
            string TextField = (ddldesc == "True" ? this.ddlProjectName.SelectedItem.Text.Trim().ToString() : this.ddlProjectName.SelectedItem.Text.Substring(13));
            string ComPrint = this.GetComPrint();
            DataTable dt1 = (DataTable)ViewState["tblUnit"];
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "uamt>0";
            dt1 = dv1.ToTable();

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_22_Sal.Sales_BO.BudgetnSales>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.rptUnitFxInf", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Unit Fixation Report"));
            Rpt1.SetParameters(new ReportParameter("projectName", TextField));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds3 = new DataSet();
            DataTable dt1 = new DataTable();
            if (this.chkAllSInf.Checked)
            {
                this.ddlFloor_SelectedIndexChanged(null, null);
                //ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFANDUNITINFO", PactCode, "", "", "", "", "", "", "", "");
                //if (ds3 == null)
                //    return;
                
            }

            else
            {
                this.LoadGrid();

                //change this code for multi building//

                //ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFORMATION", PactCode, "", "", "", "", "", "", "", "");
                //if (ds3 == null)
                //    return;
                
                //ViewState["tblUnit"] = ds3.Tables[0];
                //this.Data_bind();

            }


        }


        protected void gvUnit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string UsirCode = ((Label)this.gvUnit.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();

            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEUNITENTRY", pactcode, UsirCode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            if (result == true)
            {
                this.LoadGrid();

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Unit Fixation";
                string eventdesc = "Delete Fixation";
                string eventdesc2 = "Project Name: " + ddldesc == "True" ? this.ddlProjectName.SelectedItem.ToString() : this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt01 = (TextBox)e.Row.FindControl("txtItemdesc");
                TextBox txt02 = (TextBox)e.Row.FindControl("txtgUnitnum");
                TextBox txt03 = (TextBox)e.Row.FindControl("txtgvUSize");
                TextBox txt04 = (TextBox)e.Row.FindControl("txtgvUqty");
                TextBox txt05 = (TextBox)e.Row.FindControl("txtgvRate");
                TextBox txt06 = (TextBox)e.Row.FindControl("txtgvbstat");
                TextBox txt07 = (TextBox)e.Row.FindControl("txtgvRemarks");
                TextBox txt08 = (TextBox)e.Row.FindControl("txtgvPqty");
                TextBox txt09 = (TextBox)e.Row.FindControl("txtgvPamt");
                TextBox txt20 = (TextBox)e.Row.FindControl("txtItemdescbn");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string sales = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sales")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Substring(9, 3) == "000")
                {

                    txt01.ReadOnly = true;
                    txt02.ReadOnly = true;
                    txt03.ReadOnly = true;
                    txt04.ReadOnly = true;
                    txt05.ReadOnly = true;
                    txt06.ReadOnly = true;
                    txt07.ReadOnly = true;
                    txt20.ReadOnly = true;

                }


                if (sales == "True")
                {


                    txt01.Style.Add("color", "red");


                }

            }
        }


        protected void gvUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvUnit.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        protected void ibtnGroup_Click(object sender, EventArgs e)
        {
            this.GetGroup();
        }
        protected void ibtnFloor_Click(object sender, EventArgs e)
        {
            this.GetFloor();

        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetFloor();
        }
        protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkAllSInf.Checked)
            {
                ViewState.Remove("tblUnit");
                string comcod = this.GetCompCode();
                string PactCode = this.ddlProjectName.SelectedValue.ToString();
                string group = (this.ddlFloor.SelectedValue.ToString() == "000000000") ? this.ddlGroup.SelectedValue.ToString() + "%" : this.ddlFloor.SelectedValue.ToString() + "%";

                string totno = (this.ddlFloor.SelectedValue.ToString() == "000000000") ? "" : (this.txttotalno.Text.Trim() == "") ? "" : this.txttotalno.Text.Trim();
                string isLO = (Request.QueryString["Type"] == null) ? "0" : "1";


                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFANDUNITINFO", PactCode, group, totno, isLO, "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.gvUnit.Columns[1].Visible = false;
                ViewState["tblUnit"] = ds3.Tables[0];
                this.Data_bind();
            }
            else
            {
                if (this.lbtnOk.Text != "Ok")
                {
                    this.LoadGrid();
                }

            }
        }
        protected void ibtnTotal_Click(object sender, EventArgs e)
        {
            this.ddlFloor_SelectedIndexChanged(null, null);
        }

        protected void lbtnOk_Click1(object sender, EventArgs e)
        {

        }
        private void btnClose_Click(object sender, EventArgs e)
        {

            Response.Redirect((string)ViewState["PreviousPageUrl"]);

        }
    }
}
