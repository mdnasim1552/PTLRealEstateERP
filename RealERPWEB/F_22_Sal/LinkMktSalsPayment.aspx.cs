﻿using System;
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
namespace RealERPWEB.F_22_Sal
{
    public partial class LinkMktSalsPayment : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        Common objcom = new Common();

        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                string TypeDesc = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (TypeDesc == "Sales" ? "SALES WITH PAYMENT " : (TypeDesc == "Cust" ? "SALES WITH PAYMENT " : (TypeDesc == "Loan" ? "CUSTOMER LOAN " : (TypeDesc == "Registration" ? "CUSTOMER REGISTRATION  " : "")))) + " INFORMATIOIN VIEW/EDIT";

                Session.Remove("Unit");
                this.chkVisible.Checked = false;
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CompInitialize();
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALES WITH PAYMENT  INFORMATION ";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.gvSpayment.Columns[0].Visible = false;


                this.lbtnOk_Click(null, null);

            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CompInitialize()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3336":
                    this.lblCollection.Visible = false;
                    this.ddlCollectionTeam.Visible = false;

                    break;

                default:


                    this.txtBookDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtAggrementdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txthandoverdate.Text = System.DateTime.Today.AddYears(2).ToString("dd-MMM-yyyy");

                    break;

            }

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
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                //this.lblProjectdesc.Visible = true;
                this.lmsg111.Text = "";
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                MultiView1.ActiveViewIndex = -1;
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.ldiscountt.Text = "";
                this.ldiscountp.Text = "";
                this.lbtnBack.Visible = false;
                this.ClearScreen();
            }
        }



        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            //this.lblProjectdesc.Text = "";
            //this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lmsg111.Text = "";
        }




        private void LoadGrid()
        {
            ViewState.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.Request.QueryString["prjcode"].ToString();
            string srchunit = "%" + this.txtsrchunit.Text.Trim() + "%";
            string musircode = "51";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "DETAILSIRINFINFORMATION", PactCode, srchunit, musircode, "", "", "", "", "", "");
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFORMATION", PactCode, srchunit, musircode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvSpayment.DataSource = ds1.Tables[0];
            this.gvSpayment.DataBind();
            ViewState["tblData"] = ds1.Tables[0];

            for (int i = 0; i < gvSpayment.Rows.Count; i++)
            {
                string usircode = ((Label)gvSpayment.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSpayment.Rows[i].FindControl("lbtnusize");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = usircode;
            }

            this.lbtnusize_Click(null, null);
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string prjname = this.ddlProjectName.SelectedItem.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //item info
            DataTable basicinfo = (DataTable)Session["UsirBasicInformation"];
            string UsirCode = this.lblCode.Text;
            string ItemName = basicinfo.Rows[0]["udesc"].ToString();
            string size = Convert.ToDouble(basicinfo.Rows[0]["usize"]).ToString("#,##0.00;(#,##0.00); ");
            //ToString("#,##0;(#,##0); ")
            string unit = basicinfo.Rows[0]["munit"].ToString();

            string concat1 = ItemName + " , " + "Unit Size: " + size + " " + unit;

            //direct cost
            string ldiscounttT = this.ldiscountt.Text;
            string ldiscountpP = this.ldiscountp.Text;

            string salesteams = ddlSalesTeam.SelectedItem.Text;
            DataSet dss = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "COMBINEDTABLEFORSALES", PactCode, UsirCode, "", "", "", "", "", "", "");
            ReportDocument rpcp = new RealERPRPT.R_22_Sal.RptPCPayment();

            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;

            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = "Project Name: " + prjname.ToString().Substring(13);


            TextObject txtItemName = rpcp.ReportDefinition.ReportObjects["txtItemName"] as TextObject;
            txtItemName.Text = "Unit Description: " + concat1;


            TextObject txtdist = rpcp.ReportDefinition.ReportObjects["txtdist"] as TextObject;
            txtdist.Text = "Discount in Tk. " + ldiscounttT;

            TextObject txtdisp = rpcp.ReportDefinition.ReportObjects["txtdisp"] as TextObject;
            txtdisp.Text = "Discount in (%) " + ldiscountpP;

            TextObject txtsalest = rpcp.ReportDefinition.ReportObjects["txtsalest"] as TextObject;
            txtsalest.Text = "Sales Team: " + salesteams;


            TextObject txtNetTotalPayment = rpcp.ReportDefinition.ReportObjects["txtNetTotalPayment"] as TextObject;
            txtNetTotalPayment.Text = this.lblValNetTotalPayment.Text.Trim();


            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            TextObject txtcominfo = rpcp.ReportDefinition.ReportObjects["txtcominfo"] as TextObject;
            txtcominfo.Text = ASTUtility.Cominformation();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Print Payment Schedule";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " Unit Description: " + concat1;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            rpcp.SetDataSource(dss.Tables[0]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void lbtnusize_Click(object sender, EventArgs e)
        {
            this.gvSpayment.Columns[0].Visible = true;

            this.lbtnBack.Visible = true;

            string usircode = this.Request.QueryString["usircode"].ToString();

            //string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            Session.Remove("UsirBasicInformation");

            DataTable dtOrder = (DataTable)ViewState["tblData"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dtOrder = dv1.ToTable();
            //if ((Convert.ToBoolean(dtOrder.Rows[0]["mgtbook"])) == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Booked from Management');", true);
            //    return;
            //}
            this.MultiView1.ActiveViewIndex = 0;
            Session["UsirBasicInformation"] = dtOrder;
            this.gvSpayment.DataSource = dtOrder;
            this.gvSpayment.DataBind();
            this.lblCode.Text = usircode;
            this.lblAcAmt.Text = Convert.ToDouble(dtOrder.Rows[0]["tamt"]).ToString("#,##0;(#,##0); ");
            this.gvSpayment.Columns[5].Visible = true;
            this.gvSpayment.Columns[6].Visible = true;

            if (this.Request.QueryString["Type"].ToString().Trim() == "Loan")
            {
                this.MultiView1.ActiveViewIndex = 1;
                this.ShowCustLoan();
                return;
            }

            else if (this.Request.QueryString["Type"].ToString().Trim() == "Registration")
            {
                this.MultiView1.ActiveViewIndex = 2;
                this.ShowRegistration();
                return;
            }


            this.CustInf();
            this.BtnEnabled();
        }

        private void BtnEnabled()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Sales":
                    //((LinkButton)this.gvPersonalInfo.FooterRow.FindControl("lUpdatPerInfo")).Enabled = true;
                    ((LinkButton)this.gvCost.FooterRow.FindControl("lFinalUpdateCost")).Enabled = true;
                    this.lbtnUpdateCAST.Enabled = true;
                    this.chkVisible.Visible = true;
                    this.chkAddIns.Visible = true;
                    ((LinkButton)this.gvPayment.FooterRow.FindControl("lUpdatpayment")).Enabled = true;


                    break;

                case "Cust":
                    ((LinkButton)this.gvPersonalInfo.FooterRow.FindControl("lUpdatPerInfo")).Enabled = false;
                    ((LinkButton)this.gvCost.FooterRow.FindControl("lFinalUpdateCost")).Enabled = false;
                    this.lbtnUpdateCAST.Enabled = false;
                    this.chkVisible.Visible = false;
                    this.chkAddIns.Visible = false;
                    ((LinkButton)this.gvPayment.FooterRow.FindControl("lUpdatpayment")).Enabled = false;

                    break;
            }
        }


        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.ldiscountt.Text = "";
            this.ldiscountp.Text = "";
            this.LoadGrid();



        }

        private void CustInf()
        {
            Session.Remove("tblcost");
            Session.Remove("tblPay");
            Session.Remove("tpripay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string empid = hst["empid"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            Session["UserLog"] = ds1.Tables[6];
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            Session["tpripay"] = ds1.Tables[4];
            this.gvPersonalInfo.DataBind();
            this.gvCost.DataSource = ds1.Tables[1];
            this.gvCost.DataBind();
            Session["tblcost"] = ds1.Tables[1];



            //Sales Team, CR Team
            DataTable dtscr = ds1.Tables[2].Copy();
            DataView dv;
            dv = dtscr.DefaultView;
            dv.RowFilter = ("secid like '9402%'");
            this.ddlSalesTeam.DataTextField = "gdesc";
            this.ddlSalesTeam.DataValueField = "gcod";
            this.ddlSalesTeam.DataSource = dv.ToTable();
            this.ddlSalesTeam.DataBind();


            if ((dv.ToTable().Select("gcod='" + empid + "'")).Length > 0)
                this.ddlSalesTeam.SelectedValue = empid;





            dv = dtscr.DefaultView;
            dv.RowFilter = ("secid like '9403%'");
            this.ddlCollectionTeam.DataTextField = "gdesc";
            this.ddlCollectionTeam.DataValueField = "gcod";
            this.ddlCollectionTeam.DataSource = dv.ToTable(); ;
            this.ddlCollectionTeam.DataBind();


            // Sales Team/ Customer Care Team
            DataTable dtst = ds1.Tables[8];

            //this.ddlSalesTeam.SelectedValue = (ds1.Tables[7].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[7].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            //this.txthandoverdate.Text = (ds1.Tables[7].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[7].Rows[0]["hdate"]).ToString("dd-MMM-yyyy");


            DataRow[] drst = dtst.Select("gcod='51005'");
            if (drst.Length > 0)
            {
                this.ddlSalesTeam.SelectedValue = drst[0]["scteam"].ToString();
            }

            DataRow[] drct = dtst.Select("gcod='51007'");
            if (drct.Length > 0)
            {
                this.ddlCollectionTeam.SelectedValue = drct[0]["scteam"].ToString();
            }





            //DataTable dtst = ds1.Tables[2];
            //DataRow[] dr = dtst.Select("secid like '9402%' and gdesc1 <>''");
            //if (dr.Length > 0)
            //{
            //    this.ddlSalesTeam.SelectedValue= dr[0]["gcod"].ToString().Trim();

            //}


            //DataRow[] drc = dtst.Select("secid like '9403%' and gdesc1 <>''");
            //if (drc.Length > 0)
            //{
            //    this.ddlCollectionTeam.SelectedValue = drc[0]["gcod"].ToString().Trim();

            //}


            if (ds1.Tables[3].Rows.Count > 0)
            {
                this.Panel3.Visible = false;
                Session["tblPay"] = ds1.Tables[3];
                this.gvPayment.DataSource = ds1.Tables[3];
                this.gvPayment.DataBind();

            }
            else
            {
                this.Panel3.Visible = true;
                Session["tblPay"] = ds1.Tables[4];
                DataView dv1 = ds1.Tables[4].DefaultView;
                dv1.RowFilter = ("gcod like '81001%' or gcod like '81002%' or gcod like '81985%'");
                dv1.Sort = "gcod";
                this.gvPayment.DataSource = dv1.ToTable();
                this.gvPayment.DataBind();



            }


            addamt = (ds1.Tables[5].Rows.Count > 0) ? Convert.ToDouble(ds1.Tables[5].Rows[0]["adamt"]) : 0.00;
            dedamt = (ds1.Tables[5].Rows.Count > 0) ? Convert.ToDouble(ds1.Tables[5].Rows[0]["dedamt"]) : 0.00;
            this.txtBookDate.Text = (ds1.Tables[7].Rows.Count == 0) ? ""
                : Convert.ToDateTime(ds1.Tables[7].Rows[0]["bookdate"]).ToString("dd-MMM-yyyy");
            this.txtAggrementdate.Text = (ds1.Tables[7].Rows.Count == 0) ? "" : Convert.ToDateTime(ds1.Tables[7].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            this.txthandoverdate.Text = (ds1.Tables[7].Rows.Count == 0) ? "" : Convert.ToDateTime(ds1.Tables[7].Rows[0]["hdate"]).ToString("dd-MMM-yyyy");
            this.Calculation();
            this.lbtnTotalCost_Click(null, null);
            //System.DateTime.Today.AddYears(-2).ToString("dd-MMM-yyyy")
        }
        private void Calculation()
        {

            DataTable dtcost = (DataTable)Session["tblcost"];
            DataTable dtpay = (DataTable)Session["tblPay"];

            double tocost;
            tocost = Convert.ToDouble((Convert.IsDBNull(dtcost.Compute("sum(uamt)", "")) ? 0.00 :
                 dtcost.Compute("sum(uamt)", "")));
            ((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text = tocost.ToString("#,##0;(#,##0); ");
            ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dtpay.Compute("sum(schamt)", "")) ? 0.00 :
                 dtpay.Compute("sum(schamt)", ""))).ToString("#,##0;(#,##0); ");
            //tocost = Convert.ToDouble(((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text);
            //addwork = Convert.ToDouble(((Label)this.gvAddWork.FooterRow.FindControl("lgvFAmtw")).Text);
            //dedwork = Convert.ToDouble(((Label)this.gvDedWork.FooterRow.FindControl("lgvFAmtdw")).Text);
            this.lblValAddWork.Text = addamt.ToString("#,##0;(#,##0); ");
            this.lblValDedWork.Text = dedamt.ToString("#,##0;(#,##0); ");
            this.lblValNetTotalPayment.Text = tocost.ToString("#,##0;(#,##0); ");
            this.lblInword.Text = tocost == 0 ? "" : ASTUtility.Trans(tocost, 2);


        }


        private void ShowCustLoan()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "CUSTLOANINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            this.gvLoanInformation.DataSource = ds1.Tables[0];
            this.gvLoanInformation.DataBind();


        }
        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Personal Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {
            double Amount = 0;
            double Usize = 0;
            // double PaidAmt = 0;

            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                //Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));

                double dUsize = Convert.ToDouble('0' + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvUSize")).Text.Trim());
                double dAmt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));
                Amount += dAmt;
                Usize += dUsize;
                double dRate = (dUsize > 0) ? (dAmt / dUsize) : 0.00;
                ((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text = dAmt.ToString("#,##0; -#,##0; ");
                ((Label)this.gvCost.Rows[i].FindControl("lgvRate")).Text = dRate.ToString("#,##0.00;(#,##0.00); ");
                //if (i == 0) 
                //{

                //    PaidAmt = dUsize * dRate;
                //}



            }

            ((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text = Amount.ToString("#,##0;(#,##0); ");

            double AcAmt = Convert.ToDouble("0" + this.lblAcAmt.Text);
            if (Amount > 0)
            {
                double discount = (AcAmt - Amount);
                double discountp = (discount * 100) / Amount;
                this.ldiscountt.Text = Math.Round((AcAmt - Amount), 0).ToString("#,##0;(#,##0);");
                this.ldiscountp.Text = discountp.ToString("#,##0;(#,##0);") + '%';
                if (discountp >= 0)
                {
                    this.ldT.Text = "Discount";
                }
                else
                {
                    this.ldT.Text = "Gain";
                }
            }
            Session["amt"] = Amount;

            this.lblInword.Text = Amount == 0 ? "" : ASTUtility.Trans(Amount, 2);
        }

        protected void lFinalUpdateCost_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvCost.Rows[i].FindControl("lblgvGcod")).Text.Trim();
                string UNumber = ((TextBox)this.gvCost.Rows[i].FindControl("txtgUnitnum")).Text.Trim();
                string Usize = Convert.ToDouble('0' + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvUSize")).Text.Trim()).ToString();
                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));
                string Remarks = ((TextBox)this.gvCost.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                //if (Amt!=0)
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESALGINF1", PactCode, Usircode, Gcode, UNumber, Usize, Amt.ToString(), Remarks, "", "", "", "", "", "", "", "");

            }
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Revenue Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void lbtnUpdateCAST_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            // string Gcode = "", Gval = "";



            string salesteam = this.ddlSalesTeam.SelectedValue.ToString();
            //Gval = this.ddlSalesTeam.SelectedItem.Text.Trim();
            string ccareteam = this.ddlCollectionTeam.SelectedValue.ToString();
            //Gval = this.ddlCollectionTeam.SelectedItem.Text.Trim();


            bool res;

            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETESCTEAM", PactCode, Usircode, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (!res)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;

            }


            //Sales Team
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51005", "T", salesteam, "", "", "", "", "", "", "", "", "", "");


            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }



            //Customer Care Team
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51007", "T", ccareteam, "", "", "", "", "", "", "", "", "", "");


            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }




            //Gcode = this.ddlSalesTeam.SelectedValue.ToString();
            //Gval = this.ddlSalesTeam.SelectedItem.Text.Trim();
            ////Gcode = this.ddlCollectionTeam.SelectedValue.ToString();
            ////Gval = this.ddlCollectionTeam.SelectedItem.Text.Trim();


            //bool res;

            //res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETESCTEAM", PactCode, Usircode, "", "", "", "", "", "", "", "", "", "", "", "", "");


            //if (!res) 
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
            //    return;

            //}


            //res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, Gcode, "T", Gval, "", "", "", "", "", "", "", "", "", "");


            //if (res == false)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
            //    return;
            //}
            //}
            string aggrementdate = (this.txtAggrementdate.Text.Trim() == "") ? "01-jan-1900" : this.txtAggrementdate.Text;
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51001", "D", aggrementdate, "", "", "", "", "", "", "", "", "", "");
            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }
            string handoverdate = (this.txthandoverdate.Text.Trim() == "") ? "01-jan-1900" : this.txthandoverdate.Text;
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51002", "D", handoverdate, "", "", "", "", "", "", "", "", "", "");

            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }

            string BookDate = (this.txtBookDate.Text.Trim() == "") ? "01-jan-1900" : this.txtBookDate.Text;
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51003", "D", BookDate, "", "", "", "", "", "", "", "", "", "");

            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated";

        }
        protected void lUpdatpayment_Click(object sender, EventArgs e)
        {
            //Min Booking
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            DataTable basicinfo = (DataTable)Session["UsirBasicInformation"];
            double minbam = Convert.ToDouble(basicinfo.Rows[0]["minbam"]);
            DataTable dt = (DataTable)Session["tblPay"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("gcod like'81001%'");
            double bookam = Convert.ToDouble((Convert.IsDBNull((dv1.ToTable()).Compute("sum(schamt)", "")) ? 0.00 :
                (dv1.ToTable()).Compute("sum(schamt)", "")));
            bool minbook = (minbam == 0) ? true : (minbam <= bookam) ? true : false;

            if (!(minbook))
            {
                this.lmsg111.Text = "Booking Amount greater or equal Min Booking";
                return;

            }


            /////////

            double a = Convert.ToDouble(Session["amt"]);
            double b = Convert.ToDouble(Session["Amt11"]);
            if (a == b)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string PactCode = this.ddlProjectName.SelectedValue.ToString();
                string Usircode = this.lblCode.Text.Trim();
                for (int i = 0; i < this.gvPayment.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPayment.Rows[i].FindControl("lblgvItmCode3")).Text.Trim();
                    string schDate = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                    double Amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                    // string Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim())).ToString();
                    if (Amount != 0)
                    {
                        MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEPAYMENTINF", PactCode, Usircode, Gcode, schDate, Amount.ToString(), "", "", "", "", "", "", "", "", "", "");
                    }

                }

                //Log Entry
                DataTable dtuser = (DataTable)Session["UserLog"];

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();

                string PostedByid = (dtuser.Rows.Count == 0) ? userid : dtuser.Rows[0]["postedbyid"].ToString();
                string Posttrmid = (dtuser.Rows.Count == 0) ? Terminal : dtuser.Rows[0]["postrmid"].ToString();
                string PostSession = (dtuser.Rows.Count == 0) ? Sessionid : dtuser.Rows[0]["postseson"].ToString();
                string Posteddat = (dtuser.Rows.Count == 0) ? System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt") : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
                string tblEditSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editseson"].ToString();
                string tblEdittrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["edittrmid"].ToString();
                string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");

                string EditByid = (dtuser.Rows.Count == 0) ? "" : (tblEditByid == "") ? userid : (tblEditByid != "") ? userid : tblEditByid;
                string EditSession = (dtuser.Rows.Count == 0) ? "" : (tblEditSession == "") ? Sessionid : (tblEditSession != "") ? Sessionid : tblEditSession;
                string EditTrmid = (dtuser.Rows.Count == 0) ? "" : (tblEdittrmid == "") ? Terminal : (tblEdittrmid == "") ? Terminal : tblEdittrmid;
                string Editdat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : (tblEditDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : (tblEditDat != "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblEditDat;


                double tAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ?
                                       0 : dt.Compute("sum(schamt)", "")));

                if (tAmt > 0)
                {
                    MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEPAYMENTUSERINF", PactCode, Usircode, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, EditSession, EditTrmid, Editdat, tAmt.ToString(), "", "", "", "");
                }


                //this.lmsg111.Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales With Payment Schedule";
                    string eventdesc = "Update Payment Schedule";
                    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            else if (a < b)
            {
                //this.lmsg111.Text = "Plz Check... Amount is Overflow !!!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plz Check... Amount is Overflow !!!!!!');", true);
                return;

            }
            else
            {
                //this.lmsg111.Text = "Plz Check... Amount is Shortage !!!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plz Check... Amount is Overflow !!!!!!');", true);
                return;
            }



        }
        protected void lTotalPayment_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            double Amount = 0;
            DataTable dt = (DataTable)Session["tblPay"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                string date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text).ToString("dd-MMM-yyyy");
                dt.Rows[i]["schamt"] = Amt;
                dt.Rows[i]["schdate"] = date;
                ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text = Amt.ToString("#,##0;-#,##0; ");

            }
            Session["tblPay"] = dt;
            Amount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ? 0.00 : dt.Compute("sum(schamt)", "")));

            if (Amount > 0)
            {
                ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text = Amount.ToString("#,##0;(#,##0); ");
            }
            Session["Amt11"] = Amount;
        }

        protected void ddlSalesTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            int i, k = 0;
            this.Panel3.Visible = false;
            DataTable dt = (DataTable)Session["tblPay"];
            double bandpamt = 0;
            for (i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                //Convert.ToDouble(ASTUtility.ExprToValue('0' + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()))

                string gcode = ((Label)this.gvPayment.Rows[i].FindControl("lblgvItmCode3")).Text.Trim();
                string schDate = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double Amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                //  Amount = (Amount>0)?Amount:0;
                bandpamt += Amount;

                if (ASTUtility.Left(gcode, 5) == "81985")
                {
                    DataRow[] dr = dt.Select("gcod='" + gcode + "'");
                    if (dr.Length > 0)
                    {


                        dr[0]["schdate"] = schDate;
                        dr[0]["schamt"] = Amount;
                    }
                }
                else
                {

                    dt.Rows[k]["schdate"] = schDate;
                    dt.Rows[k]["schamt"] = Amount;
                    k++;

                }


            }
            DataTable dt2 = dt;
            double Tamt = Convert.ToDouble(((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text.Trim());
            double ramt = Tamt - bandpamt;
            int tin = Convert.ToInt32("0" + this.txtTInstall.Text);
            int dur = Convert.ToInt32(this.ddlMonth.SelectedValue.ToString());
            double insamt = ramt / tin;

            // string schDate1 = Convert.ToDateTime(dt.Rows[i-1]["schdate"]).ToString("dd-MMM-yyyy");
            string schDate1 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy"); // Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i-1].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
            for (int j = k; j < tin + k; j++)
            {
                string schdate2 = (j == k) ? schDate1 : Convert.ToDateTime(schDate1).AddMonths(dur).ToString("dd-MMM-yyyy");
                double schamt = insamt;
                dt.Rows[j]["schdate"] = schdate2;
                dt.Rows[j]["schamt"] = schamt;
                schDate1 = schdate2;
            }


            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("schamt>0");
            dv1.Sort = "gcod";
            Session["tblPay"] = dv1.ToTable();
            this.gvPayment.DataSource = dv1.ToTable();
            this.gvPayment.DataBind();
            this.lTotalPayment_Click(null, null);
            this.chkVisible.Checked = false;

        }
        protected void lbtnSlab_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblPay"];
            int strins, endins; double insamt;
            strins = Convert.ToInt32("0" + this.txtfrmslab.Text.Trim());
            endins = Convert.ToInt32("0" + this.txttoslab.Text.Trim());
            insamt = Convert.ToDouble("0" + this.txtperslabamt.Text.Trim());
            int drowcount = dt.Rows.Count;
            endins = endins > drowcount ? drowcount : endins;
            for (int i = strins - 1; i < endins; i++)
            {
                dt.Rows[i]["schamt"] = insamt;

            }
            Session["tblPay"] = dt;
            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();
            this.lTotalPayment_Click(null, null);

        }
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkVisible.Checked == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string PactCode = this.ddlProjectName.SelectedValue.ToString();
                string Usircode = this.lblCode.Text.Trim();
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEPAYMENTINF", PactCode, Usircode, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result == false)
                    return;
                DataTable dt = (DataTable)Session["tblPay"];
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = ("gcod like'81001%' or gcod like '81002%' or gcod like '81985%'  ");
                dv1.Sort = "gcod";
                this.gvPayment.DataSource = dv1.ToTable();
                this.gvPayment.DataBind();
                Session.Remove("tblPay");
                DataTable dt1 = (DataTable)Session["tpripay"];
                Session["tblPay"] = dt1;
                this.Panel3.Visible = true;

            }
            else
            {
                this.Panel3.Visible = false;
            }
        }


        protected void chkSegment_CheckedChanged(object sender, EventArgs e)
        {


            this.pnlSlab.Visible = this.chkSegment.Checked;

        }


        protected void lUpdateLoanInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvLoanInformation.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvLoanInformation.Rows[i].FindControl("lblgvItmCodeloan")).Text.Trim();
                string gtype = ((Label)this.gvLoanInformation.Rows[i].FindControl("lgvgvalloan")).Text.Trim();
                string Gvalue = ((TextBox)this.gvLoanInformation.Rows[i].FindControl("txtgvValloan")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTLOAN", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated ";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Loan Info";
                string eventdesc = "Update Loan Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        private void ShowRegistration()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "CUSTREGISTRATION", PactCode, UsirCode, "", "", "", "", "", "", "");
            this.gvRegStatus.DataSource = ds1.Tables[0];
            this.gvRegStatus.DataBind();


        }

        protected void lUpdateRegis_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvRegStatus.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvRegStatus.Rows[i].FindControl("lblgvItmCodeReg")).Text.Trim();
                string reclegdept = ((TextBox)this.gvRegStatus.Rows[i].FindControl("txtgvValRecleg")).Text.Trim();
                string protoclient = ((TextBox)this.gvRegStatus.Rows[i].FindControl("txtgvValprotoclient")).Text.Trim();

                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPCUSTREG", PactCode, Usircode, Gcode, reclegdept, protoclient, "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated ";
        }
        protected void ibtnFindInstallment_Click(object sender, EventArgs e)
        {
            this.ShowInstallment();
        }
        protected void lbtnAddInstallment_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblPay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string gcod = this.ddlInstallment.SelectedValue.ToString();

            DataRow[] dr = dt.Select("gcod='" + gcod + "'");
            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = hst["comcod"].ToString();
                dr1["gcod"] = this.ddlInstallment.SelectedValue.ToString();
                dr1["gval"] = "T";
                dr1["gdesc"] = this.ddlInstallment.SelectedItem.Text.Trim();
                dr1["pactcode"] = this.ddlProjectName.SelectedValue.ToString();
                dr1["usircode"] = this.lblCode.Text.Trim();
                dr1["schdate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                dr1["schamt"] = 0;
                dt.Rows.Add(dr1);
            }

            Session["tblPay"] = dt;
            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();
            this.lTotalPayment_Click(null, null);

        }
        protected void chkAddIns_CheckedChanged(object sender, EventArgs e)
        {
            this.PanelAddIns.Visible = this.chkAddIns.Checked;
        }


        private void ShowInstallment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtsrchisn = this.txtsrchInstallment.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETINSTALLMENT", txtsrchisn, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlInstallment.Items.Clear();
                return;

            }
            this.ddlInstallment.DataTextField = "gdesc";
            this.ddlInstallment.DataValueField = "gcod";
            this.ddlInstallment.DataSource = ds1.Tables[0];
            this.ddlInstallment.DataBind();

        }
        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblPay"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            string Gcode = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lblgvItmCode3")).Text.Trim();
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEINSTALLMENT", PactCode, Usircode, Gcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("gcod<>''");
                Session["tblPay"] = dv.ToTable();
                this.gvPayment.DataSource = dt;
                this.gvPayment.DataBind();
                this.lTotalPayment_Click(null, null);
            }
        }
        protected void gvSpayment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvSpayment.EditIndex = -1;
            this.gvSpayment.DataSource = (DataTable)ViewState["tblData"];
            this.gvSpayment.DataBind();
        }
        protected void gvSpayment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var indx = e.NewEditIndex;
            string usircode = ((Label)this.gvSpayment.Rows[e.NewEditIndex].FindControl("lblgvItmCod")).Text.Trim();

            DataTable dtOrder = (DataTable)ViewState["tblData"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode='" + usircode + "'";
            dtOrder = dv1.ToTable();




            this.gvSpayment.EditIndex = e.NewEditIndex;
            this.gvSpayment.DataSource = dtOrder;
            this.gvSpayment.DataBind();

            int rowindex = (gvSpayment.PageSize) * (this.gvSpayment.PageIndex) + e.NewEditIndex;
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            ////string usircode = ((DataTable)ViewState["tblData"]).Rows[rowindex]["usircode"].ToString();
            //string proscod = (dtOrder.Rows[rowindex]["proscod"].ToString());
            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[e.NewEditIndex].FindControl("ddlClientName");
            //ViewState["gindex"] = e.NewEditIndex;
            string comcod = objcom.GetCompCode();


            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "ALLCLILIST", "", "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "cdesc";
            ddl2.DataValueField = "ccode";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
            //ddl2.SelectedValue = proscod;

            string others = ddl2.SelectedValue.ToString();

            //(DropDownList)this.gvSpayment.Rows[e.NewEditIndex].FindControl("ddlClientName").Visible == false;

            if (others == "000000000000")
            {
                ddl2.Visible = false;
                ((TextBox)this.gvSpayment.Rows[e.NewEditIndex].FindControl("txtCustname")).Visible = true;
            }







        }
        protected void ibtnSrchClient_Click(object sender, EventArgs e)
        {

            string comcod = objcom.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[rowindex].FindControl("ddlClientName");
            string SearchClient = "%" + ((TextBox)gvSpayment.Rows[rowindex].FindControl("txtSerachClient")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETCLEINTNAME", pactcode, SearchClient, "", "", "", "", "", "", "");
            ddl2.DataTextField = "prosdesc";
            ddl2.DataValueField = "proscod";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
        }

        protected void gvSpayment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string Proscode = "";

            string comcod = objcom.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = ((Label)this.gvSpayment.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();//.ToUpper();
            string clname = ((DropDownList)this.gvSpayment.Rows[e.RowIndex].FindControl("ddlClientName")).SelectedItem.ToString() == "Others Client" ? ((TextBox)this.gvSpayment.Rows[e.RowIndex].FindControl("txtCustname")).Text.Trim() : ((DropDownList)this.gvSpayment.Rows[e.RowIndex].FindControl("ddlClientName")).SelectedItem.ToString();




            string schDate1 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy"); // Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i-1].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");



            if (clname != "")
            {
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTCLIENTINF", pactcode, usircode, "01001", clname, 0.ToString(), schDate1, "T", "", "", "", "", "", "", "", "");
                if (result == true)
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Successfully Updated');", true);
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Fail');", true);
                    return;
                }


                DataTable dt = (DataTable)ViewState["tblData"];
                // dt.Rows[rowindex]["proscod"] = Proscode;

                //dt.Rows[rowindex]["custname"] =clname;

                dt.Select("usircode='" + usircode + "'")[0]["custname"] = clname;
                // DataRow []dr=dt.Select("usircode='" + usircode + "'")[""].ToString()

                ViewState["tblData"] = dt;


                DataView dv = dt.Copy().DefaultView;
                dv.RowFilter = ("usircode='" + usircode + "'");
                this.gvSpayment.EditIndex = -1;
                this.gvSpayment.DataSource = dv.ToTable();
                this.gvSpayment.DataBind();


                for (int i = 0; i < gvSpayment.Rows.Count; i++)
                {
                    string usircode1 = ((Label)gvSpayment.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                    LinkButton lbtn1 = (LinkButton)gvSpayment.Rows[i].FindControl("lbtnusize");
                    if (lbtn1 != null)
                        if (lbtn1.Text.Trim().Length > 0)
                            lbtn1.CommandArgument = usircode1;
                }





            }

        }

        protected void ibtnFindSalesteam_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnsrchunit_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void ddlCollectionTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ibtnFindCollectionteam_Click(object sender, EventArgs e)
        {

        }
        protected void gvSpayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txt01 = (Label)e.Row.FindControl("lblgvRemarks");
                LinkButton lbtn1 = (LinkButton)e.Row.FindControl("lbtnusize");
                HyperLink hypcustlink = (HyperLink)e.Row.FindControl("hypcustomer");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string pactdesc = this.ddlProjectName.SelectedItem.Text.ToString().Substring(16);

                string custname = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "custname")).ToString();



                if (custname.Length > 0)
                {
                    lbtn1.Style.Add("color", "Red");


                }
                else
                {
                    lbtn1.Style.Add("color", "Green");
                    //LinkButton lnkedit = (LinkButton)e.Row.FindControl("lnkedit");
                    //lnkedit.Visible = true;
                }
                if (hypcustlink != null)
                {
                    hypcustlink.NavigateUrl = "~/F_39_MyPage/CustomerDetail.aspx?prjcode=" + pactcode + "&genno=" + code + "&pactdesc=" + pactdesc + "";
                    hypcustlink.Target = "_blank";

                }



            }
        }
        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[0].FindControl("ddlClientName");

            string clientcode = ddl2.SelectedValue.ToString();
            if (ddl2 == null)
                return;


            if (clientcode == "000000000000")
            {
                ddl2.Visible = false;
                ((TextBox)this.gvSpayment.Rows[0].FindControl("txtCustname")).Visible = true;
            }

        }
    }
}

