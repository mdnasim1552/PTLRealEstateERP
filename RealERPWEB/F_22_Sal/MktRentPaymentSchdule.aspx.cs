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
namespace RealERPWEB.F_22_Sal
{
    public partial class MktRentPaymentSchdule : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        Common objcom = new Common();
        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string TypeDesc = this.Request.QueryString["Type"].ToString().Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = "RENT PAYMENT SCHEDULE ";

                Session.Remove("Unit");
                this.chkVisible.Checked = false;
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtAggrementdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txthandoverdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;



            }

        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
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
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                this.lblProjectdesc.Visible = true;

                this.lmsg111.Text = "";
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }



        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            this.lmsg111.Text = "";
        }




        private void LoadGrid()
        {
            ViewState.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "DETAILSIRINFINFORMATION", PactCode, "", "", "", "", "", "", "", "");
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

            this.gvSpayment.Columns[5].Visible = false;
            this.gvSpayment.Columns[6].Visible = false;
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
            DataSet dss = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "COMBINEDTABLEFORSALES", PactCode, UsirCode, "", "", "", "", "", "", "");
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

            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            Session.Remove("UsirBasicInformation");

            DataTable dtOrder = (DataTable)ViewState["tblData"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dtOrder = dv1.ToTable();
            if ((Convert.ToBoolean(dtOrder.Rows[0]["mgtbook"])) == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Booked from Management');", true);
                return;
            }
            this.MultiView1.ActiveViewIndex = 0;
            Session["UsirBasicInformation"] = dtOrder;
            this.gvSpayment.DataSource = dtOrder;
            this.gvSpayment.DataBind();
            this.lblCode.Text = usircode;
            this.lblAcAmt.Text = Convert.ToDouble(dtOrder.Rows[0]["uamt"]).ToString("#,##0;(#,##0); ");
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
                    ((LinkButton)this.gvPersonalInfo.FooterRow.FindControl("lUpdatPerInfo")).Enabled = true;
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
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            Session["UserLog"] = ds1.Tables[6];
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            Session["tpripay"] = ds1.Tables[4];
            this.gvPersonalInfo.DataBind();
            this.gvCost.DataSource = ds1.Tables[1];
            this.gvCost.DataBind();
            Session["tblcost"] = ds1.Tables[1];

            this.ddlSalesTeam.DataTextField = "gdesc";
            this.ddlSalesTeam.DataValueField = "gcod";
            this.ddlSalesTeam.DataSource = ds1.Tables[2];
            this.ddlSalesTeam.DataBind();
            DataTable dtst = ds1.Tables[2];
            DataRow[] dr = dtst.Select("gdesc1 <>''");
            if (dr.Length > 0)
            {
                this.ddlSalesTeam.SelectedValue = dr[0]["gcod"].ToString().Trim();

            }



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
            this.txtAggrementdate.Text = (ds1.Tables[7].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[7].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            this.txthandoverdate.Text = (ds1.Tables[7].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[7].Rows[0]["hdate"]).ToString("dd-MMM-yyyy");
            this.Calculation();
            this.lbtnTotalCost_Click(null, null);

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


        }


        private void ShowCustLoan()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "CUSTLOANINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

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

            // double PaidAmt = 0;

            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                //Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));

                DateTime strtdate = (((TextBox)this.gvCost.Rows[i].FindControl("txtfrmdate")).Text.Trim() == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvCost.Rows[i].FindControl("txtfrmdate")).Text.Trim());
                DateTime enddate = (((TextBox)this.gvCost.Rows[i].FindControl("txttodate")).Text.Trim() == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvCost.Rows[i].FindControl("txttodate")).Text.Trim());
                double dUsize = Convert.ToDouble('0' + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvUSize")).Text.Trim());
                double rentpmon = Convert.ToDouble('0' + ((TextBox)this.gvCost.Rows[i].FindControl("txtRentPMon")).Text.Trim());
                int duration = (strtdate == Convert.ToDateTime("01-Jan-1900")) ? 0 : ASTUtility.Datediff(enddate, strtdate) + 1;


                double dAmt = rentpmon > 0 ? (rentpmon * duration) : Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));
                Amount += dAmt;


                double dRate = (dUsize > 0) ? (rentpmon / dUsize) : 0.00;
                ((Label)this.gvCost.Rows[i].FindControl("lgvduration")).Text = duration.ToString("#,##0;(#,##0); ");
                ((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text = dAmt.ToString("#,##0; -#,##0; ");
                ((Label)this.gvCost.Rows[i].FindControl("lgvRate")).Text = dRate.ToString("#,##0.00;(#,##0.00); ");




            }

            ((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text = Amount.ToString("#,##0;(#,##0); ");

            double AcAmt = Convert.ToDouble(this.lblAcAmt.Text);
            if (Amount > 0)
            {
                double discount = (AcAmt - Amount);
                double discountp = (discount * 100) / Amount;
                this.ldiscountt.Text = Math.Round((AcAmt - Amount), 0).ToString("#,##0;(#,##0);");
                this.ldiscountp.Text = discountp.ToString("#,##0;(#,##0);") + '%';
            }
            Session["amt"] = Amount;
        }

        protected void lFinalUpdateCost_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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

                string strtdate = (((TextBox)this.gvCost.Rows[i].FindControl("txtfrmdate")).Text.Trim() == "") ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvCost.Rows[i].FindControl("txtfrmdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string enddate = (((TextBox)this.gvCost.Rows[i].FindControl("txttodate")).Text.Trim() == "") ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvCost.Rows[i].FindControl("txttodate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string rentpmon = Convert.ToDouble('0' + ((TextBox)this.gvCost.Rows[i].FindControl("txtRentPMon")).Text.Trim()).ToString();
                //if (Amt!=0)
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPDATESALGINF1", PactCode, Usircode, Gcode, UNumber, Usize, Amt.ToString(), Remarks, strtdate, enddate, rentpmon, "", "", "", "", "");

            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            string Gcode = "", Gval = "";
            Gcode = this.ddlSalesTeam.SelectedValue.ToString();
            Gval = this.ddlSalesTeam.SelectedItem.Text.Trim();
            bool res;

            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "DELETESCTEAM", PactCode, Usircode, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (!res)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }


            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, Gcode, "T", Gval, "", "", "", "", "", "", "", "", "", "");


            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string aggrementdate = this.txtAggrementdate.Text;
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51001", "D", aggrementdate, "", "", "", "", "", "", "", "", "", "");
            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string handoverdate = this.txthandoverdate.Text;
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51002", "D", handoverdate, "", "", "", "", "", "", "", "", "", "");

            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void lUpdatpayment_Click(object sender, EventArgs e)
        {
            //Min Booking
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                        MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPDATEPAYMENTINF", PactCode, Usircode, Gcode, schDate, Amount.ToString(), "", "", "", "", "", "", "", "", "", "");
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
                    MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPDATEPAYMENTUSERINF", PactCode, Usircode, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, EditSession, EditTrmid, Editdat, tAmt.ToString(), "", "", "", "");
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
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "DELETEPAYMENTINF", PactCode, Usircode, "", "", "", "", "", "", "", "", "", "", "", "", "");
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



        protected void lUpdateLoanInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPDATECUSTLOAN", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "CUSTREGISTRATION", PactCode, UsirCode, "", "", "", "", "", "", "");
            this.gvRegStatus.DataSource = ds1.Tables[0];
            this.gvRegStatus.DataBind();


        }

        protected void lUpdateRegis_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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

                MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPCUSTREG", PactCode, Usircode, Gcode, reclegdept, protoclient, "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETINSTALLMENT", txtsrchisn, "", "", "", "", "", "", "", "");
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
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "DELETEINSTALLMENT", PactCode, Usircode, Gcode, "", "", "", "", "", "", "", "", "", "", "", "");

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
            this.gvSpayment.EditIndex = e.NewEditIndex;
            this.gvSpayment.DataSource = (DataTable)ViewState["tblData"];
            this.gvSpayment.DataBind();
            int rowindex = (gvSpayment.PageSize) * (this.gvSpayment.PageIndex) + e.NewEditIndex;
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string usircode = ((DataTable)ViewState["tblData"]).Rows[rowindex]["usircode"].ToString();
            string proscod = ((DataTable)ViewState["tblData"]).Rows[rowindex]["proscod"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[e.NewEditIndex].FindControl("ddlClientName");
            ViewState["gindex"] = e.NewEditIndex;
            string comcod = objcom.GetCompCode();
            string SearchClient = "%" + ((TextBox)this.gvSpayment.Rows[e.NewEditIndex].FindControl("txtSerachClient")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETCLEINTNAME", pactcode, SearchClient, "", "", "", "", "", "", "");
            ddl2.DataTextField = "prosdesc";
            ddl2.DataValueField = "proscod";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
            ddl2.SelectedValue = proscod; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();

            //ddl2.Visible = true;
        }
        protected void ibtnSrchClient_Click(object sender, EventArgs e)
        {

            string comcod = objcom.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[rowindex].FindControl("ddlClientName");
            string SearchClient = "%" + ((TextBox)gvSpayment.Rows[rowindex].FindControl("txtSerachClient")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETCLEINTNAME", pactcode, SearchClient, "", "", "", "", "", "", "");
            ddl2.DataTextField = "prosdesc";
            ddl2.DataValueField = "proscod";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
        }

        protected void gvSpayment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string comcod = objcom.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = ((Label)this.gvSpayment.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();//.ToUpper();
            string Proscode = ((DropDownList)this.gvSpayment.Rows[e.RowIndex].FindControl("ddlClientName")).SelectedValue.ToString();

            if (Proscode != "")
            {
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPMKTASALLINK", pactcode, usircode, Proscode, "", "", "", "", "", "", "", "", "", "", "", "");
                if (result == true)
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Successfully Updated');", true);
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Fail');", true);
                    return;
                }

                int rowindex = (gvSpayment.PageSize) * (this.gvSpayment.PageIndex) + e.RowIndex;
                DataTable dt = (DataTable)ViewState["tblData"];

                dt.Rows[rowindex]["proscod"] = Proscode;
                dt.Rows[rowindex]["prosdesc"] = ((DropDownList)this.gvSpayment.Rows[e.RowIndex].FindControl("ddlClientName")).SelectedItem.Text;
                ViewState["tblData"] = dt;
                this.gvSpayment.EditIndex = -1;
                this.gvSpayment.DataSource = dt;
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
        protected void chkSegment_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlSlab.Visible = this.chkSegment.Checked;
        }
    }
}

