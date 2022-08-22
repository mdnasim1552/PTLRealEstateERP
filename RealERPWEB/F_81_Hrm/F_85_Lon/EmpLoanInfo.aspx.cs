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


namespace RealERPWEB.F_81_Hrm.F_85_Lon
{
    public partial class EmpLoanInfo1 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");


                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE LOAN INFORMATION";
                // this.GetLoanNo();
                string genno = this.Request.QueryString["genno"] ?? "";
                if (genno.Length > 0)
                {
                    this.GetAprLoanist();
                }
                VisibleSection();
                this.GetEmplist();
                this.GetLoanType();
                this.txtstrdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtUptoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string lnno = this.Request.QueryString["lnno"] ?? "";


                if (lnno.Length > 0)
                {
                    this.ShowLoanInfo();
                }
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void VisibleSection()
        {
            string qstring = this.Request.QueryString["Type"].ToString();
            if (qstring == "Refund")
            {
                this.lnkSearcEMP.Enabled = false;
                this.ddlEmpList.Enabled = false;
                this.lbldate.Text = "Refund Date";
                this.ddlLoantype.Enabled = false;
                this.txtstrdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtToamt.Enabled = false;
                this.txtinsamt.Enabled = false;
                this.txtstrdate.Enabled = false;
                this.ddlMonth.Enabled = false;
                this.txtPaidAmt.Enabled = false;
                this.txtUptoDate.Enabled = false;
                this.lbtnGenerate.Visible = false;                 
                this.chkVisible.Visible = false;
                this.rfuBox.Visible = true;
                this.refunNotes.Visible = true;
                
            }
        }


        private void GetAprLoanist()
        {
            string comcod = this.GetComeCode();
            string genno = this.Request.QueryString["genno"] ?? "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAPRVLOAN", genno, "", "", "", "", "", "", "", "");
            ViewState["tblAprLoan"] = ds1.Tables[0];





        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void GetLNNo()
        {
            string comcod = this.GetComeCode();
            string mLNNO = "NEWLN";
            if (this.ddlPrevLoanList.Items.Count > 0)
                mLNNO = this.ddlPrevLoanList.SelectedValue.ToString();

            string date = this.txtCurDate.Text; ;
            if (mLNNO == "NEWLN")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTLOANNO", date, "", "", "", "", "", "", "", "");

                if (ds3 == null)
                    return;
                if (ds3.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);

                    this.ddlPrevLoanList.DataTextField = "maxlnno1";
                    this.ddlPrevLoanList.DataValueField = "maxlnno";
                    this.ddlPrevLoanList.DataSource = ds3.Tables[0];
                    this.ddlPrevLoanList.DataBind();
                }
            }
        }
        private void GetEmplist()
        {
            string comcod = this.GetComeCode();
            string txtEmpname = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, "", "", "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            string genno = this.Request.QueryString["genno"] ?? "";
            if (genno.Length > 0)
            {
                DataTable dt = (DataTable)ViewState["tblAprLoan"];
                this.ddlEmpList.SelectedValue = dt.Rows[0]["empid"].ToString();
            }

        }
        private void GetLoanType()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLOANTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlLoantype.DataTextField = "loantype";
            this.ddlLoantype.DataValueField = "gcod";
            this.ddlLoantype.DataSource = ds1.Tables[0];
            this.ddlLoantype.DataBind();
            string genno = this.Request.QueryString["genno"] ?? "";
            if (genno.Length > 0)
            {
                DataTable dt = (DataTable)ViewState["tblAprLoan"];
                this.ddlLoantype.SelectedValue = dt.Rows[0]["loantype"].ToString().Trim();
            }
        }
        private void GetPreLnlist()
        {
            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            string lonatype = this.ddlLoantype.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GetPrevLN", curdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevLoanList.DataTextField = "lnno1";
            this.ddlPrevLoanList.DataValueField = "lnno";
            this.ddlPrevLoanList.DataSource = ds1.Tables[0];
            this.ddlPrevLoanList.DataBind();
        }

        protected void lbtnPrevLoanList_Click(object sender, EventArgs e)
        {
            this.GetPreLnlist();
        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmplist();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlEmpList.Enabled = false;
                // this.lblEmpName.Text = this.ddlEmpList.SelectedItem.Text.Trim();
                this.lnkSearcEMP.Enabled = false;
                this.lbtnPrevLoanList.Enabled = false;
                this.ddlPrevLoanList.Enabled = false;
                //   this.ddlEmpList.Visible = false;
                // this.lblEmpName.Visible = true;
                this.ddlLoantype.Enabled = false;

                this.chkAddIns.Checked = false;
                this.chkVisible.Checked = false;
                this.chkVisible.Visible = true;
                this.chkVisible.Text = "Gen. Installment";

                this.ShowLoanInfo();
                if (this.ddlPrevLoanList.Items.Count > 0)
                {
                    this.pnlloan.Visible = true;
                    lbtnTotal_Click(null, null);
                }
                VisibleSection();

                return;
            }
            this.lbtnOk.Text = "Ok";
            //this.lblEmpName.Text = "";
            this.txtPaidAmt.Text = "0";

            this.ddlEmpList.Enabled = true;

            this.ddlPrevLoanList.Items.Clear();
            this.lbtnPrevLoanList.Enabled = true;
            this.ddlPrevLoanList.Enabled = true;
            // this.ddlEmpList.Visible = true;
            this.txtCurDate.Enabled = true;
            // this.lblEmpName.Visible = false;
            this.chkAddIns.Visible = false;
            this.lbtnAddInstallment.Visible = false;
            this.chkVisible.Visible = false;
            this.pnlloan.Visible = false;
            this.ddlLoantype.Enabled = true;
            VisibleSection();
            this.gvloan.DataSource = null;
            this.gvloan.DataBind();

        }



        private void ComponeEnable()
        {
            this.txtCurDate.Enabled = true;
            this.chkAddIns.Visible = false;
            this.chkVisible.Visible = true;
            this.ddlLoantype.Enabled = true;
            this.txtToamt.Enabled = true;
            this.txtEmployePayment.Enabled = true;
            this.txtCompPaid.Enabled = true;
            this.txtstrdate.Enabled = true;
            this.ddlMonth.Enabled = true;
            this.lbtnGenerate.Enabled = true;
            this.txtinsamt.Enabled = true;
        }
        private void ComponeDisable()
        {
            this.txtCurDate.Enabled = false;
            this.chkAddIns.Visible = true;
            this.chkVisible.Visible = false;
            this.ddlLoantype.Enabled = false;
            this.txtToamt.Enabled = false;
            this.txtEmployePayment.Enabled = false;
            this.txtCompPaid.Enabled = false;
            this.txtstrdate.Enabled = false;
            this.ddlMonth.Enabled = false;
            this.lbtnGenerate.Enabled = false;
            this.txtinsamt.Enabled = false;
        }
        private void ShowLoanInfo()
        {
            ViewState.Remove("tblln");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            this.txtToamt.Text = "0";
            this.txtUptoDate.Text = "";


            string lnno = this.Request.QueryString["lnno"] ?? "";
            string mLNNo = "NEWLN";
            if (this.ddlPrevLoanList.Items.Count > 0)
            {
                ComponeDisable();
                mLNNo = this.ddlPrevLoanList.SelectedValue.ToString();
            }
            else
            {
                ComponeEnable();
            }
            if (lnno.Length > 0)
            {
                ComponeDisable();
                mLNNo = lnno.ToString();
            }
            else
            {
                ComponeEnable();
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNINFO", mLNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblln"] = ds1.Tables[0];
            if (mLNNo == "NEWLN")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTLOANNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);
                return;
            }
            ViewState["tblln1"] = ds1.Tables[1];
            this.ddlEmpList.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
            this.ddlLoantype.SelectedValue = ds1.Tables[1].Rows[0]["loantype"].ToString();
            //  this.lblEmpName.Text = this.ddlEmpList.SelectedItem.Text.Trim();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["lnno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["lnno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["lndate"]).ToString("dd-MMM-yyyy");
            this.txtPaidAmt.Text = Convert.ToDecimal(ds1.Tables[1].Rows[0]["uptopaid"]).ToString();
            this.txtUptoDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["uptopaiddate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(ds1.Tables[1].Rows[0]["uptopaiddate"]).ToString("dd-MMM-yyyy");
            this.txtToamt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["lnamt"]).ToString();

            this.Data_DataBind();
        }

        private void Data_DataBind()
        {
            DataTable dt = (DataTable)ViewState["tblln"];
            this.gvloan.DataSource = dt;
            this.gvloan.DataBind();
            this.FooterCalculation((DataTable)ViewState["tblln"]);


            string qstring = this.Request.QueryString["Type"].ToString();
            if (qstring == "Refund")
            {
                this.gvloan.Columns[7].Visible = false;
                this.gvloan.Columns[8].Visible = true;
                ((LinkButton)this.gvloan.FooterRow.FindControl("lnkbtnRefund")).Visible = true;
                ((LinkButton)this.gvloan.FooterRow.FindControl("lbtnTotal")).Visible = false;
                ((LinkButton)this.gvloan.FooterRow.FindControl("lbtnFinalUpdate")).Visible = false;
                ((LinkButton)this.gvloan.FooterRow.FindControl("lnkCalculation")).Visible = true;
                
            }
            else
            {
                this.gvloan.Columns[8].Visible = false;
                ((LinkButton)this.gvloan.FooterRow.FindControl("lnkbtnRefund")).Visible = false;
            }

            // for bti formula column hide
            this.gvloan.Columns[2].Visible = (dt.Rows.Count > 0 && dt.Rows[0]["isformula"].ToString().Trim() != "") ? true : false;
            this.gvloan.Columns[4].Visible = (dt.Rows.Count > 0 && dt.Rows[0]["isformula"].ToString().Trim() != "") ? true : false;
            this.gvloan.Columns[6].Visible = (dt.Rows.Count > 0 && dt.Rows[0]["isformula"].ToString().Trim() != "") ? true : false;
            



        }
        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvloan.FooterRow.FindControl("gvlFToamtTTL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttlinsamt)", "")) ? 0.00 : dt.Compute("sum(ttlinsamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvloan.FooterRow.FindControl("gvlFToamtComp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(comppay)", "")) ? 0.00 : dt.Compute("sum(comppay)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvloan.FooterRow.FindControl("gvlFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))).ToString("#,##0;(#,##0); ");
            Session["Report1"] = gvloan;
            ((HyperLink)this.gvloan.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
        }
        protected void lnkupdate_Click(object sender, EventArgs e)
        {
        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            //this.pnlloan.Visible = false;
            this.chkVisible.Checked = false;
            DataTable dt = (DataTable)ViewState["tblln"];
            DataView dv = dt.DefaultView;
            DataTable dt1 = new DataTable();
            dt1 = dt.Clone();

            double toamt = Convert.ToDouble("0" + this.txtToamt.Text.Trim());
            double lnamt = Convert.ToDouble("0" + this.txtinsamt.Text.Trim());
            double compamt = Convert.ToDouble("0" + this.txtCompPaid.Text.Trim());
            double empamt = Convert.ToDouble("0" + this.txtEmployePayment.Text.Trim());
            double setcomppay = 0.00;
            double setemppay = 0.00;
            double tlnamt = 0.00;

            if (toamt == 0 || lnamt == 0)
            {
                string Msg = "Please enter amount for generate loan";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                return;
            }
            string calculationtype = (isFormulaChekcbox.Checked == true) ? ddlFormulatype.SelectedValue.ToString() : "";

            if (calculationtype == "F")
            {
                if (lnamt != (compamt + empamt))
                {
                    string Msg = "Please Equal Formula amount = Instalment amount";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                    return;
                }
            }
            //// only for bti
            //if (isFormulaChekcbox.Checked)
            //{
            //    string calculationtype = ddlFormulatype.SelectedValue.ToString();
            //    if (calculationtype == "0")
            //    {
            //        setcomppay = (compamt / 100) * lnamt;
            //        setemppay = (empamt / 100) * lnamt; 
            //    }
            //    else
            //    {
            //        setcomppay = compamt;
            //        setemppay = empamt;
            //    }
            //    if(lnamt!=(setcomppay+ setemppay))
            //    {
            //        string Msg = "Please Equal Formula amount = Instalment amount";
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
            //        return;
            //    }
            //}
            //else
            //{
            //    setcomppay = 0.00;
            //    setemppay = lnamt;
            //}
            // end bti formula

            int dur = Convert.ToInt32(this.ddlMonth.SelectedValue.ToString());
            string date = this.txtstrdate.Text.Trim();
            string lndate;
            DataRow dr1;
            for (int i = 0; i < 500; i++)
            {
                if (toamt > 0)
                {
                    lnamt = (toamt > lnamt) ? lnamt : toamt;

                    // only for bti
                    if (isFormulaChekcbox.Checked)
                    {
                        setcomppay = GetCalculateParcentage(lnamt, compamt);
                        setemppay = GetCalculateParcentage(lnamt, empamt);
                    }
                    else
                    {
                        setcomppay = 0.00;
                        setemppay = lnamt;
                    }
                    if (i == 0)
                    {
                        dr1 = dt1.NewRow();
                        lndate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                        dr1["lndate"] = lndate;
                        dr1["lnamt"] = setemppay;
                        dr1["comppay"] = setcomppay;
                        dr1["paidamt"] = "True";
                        dr1["isformula"] = calculationtype;
                        dr1["ttlinsamt"] = setemppay + setcomppay;
                        dt1.Rows.Add(dr1);
                        toamt = toamt - (setemppay + setcomppay);
                        continue;
                    }
                    dr1 = dt1.NewRow();
                    lndate = Convert.ToDateTime(dt1.Rows[i - 1]["lndate"].ToString()).AddMonths(dur).ToString("dd-MMM-yyyy");
                    dr1["lndate"] = lndate;
                    dr1["lnamt"] = setemppay;
                    dr1["comppay"] = setcomppay;
                    dr1["ttlinsamt"] = setemppay + setcomppay;
                    dr1["isformula"] = calculationtype;

                    dr1["id"] = 0;
                    dr1["paidamt"] = "True";
                    dt1.Rows.Add(dr1);
                    toamt = toamt - (setemppay + setcomppay);
                }
                else
                {
                    break;
                }
            }
            ViewState["tblln"] = dt1;
            this.Data_DataBind();
            lbtnTotal_Click(null, null);
        }
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlloan.Visible = this.chkVisible.Checked;

            string comcod = this.GetComeCode();

            string genno = this.Request.QueryString["genno"] ?? "";
            if (genno.Length > 0)
            {
                DataTable dt = (DataTable)ViewState["tblAprLoan"];
                this.txtstrdate.Text = Convert.ToDateTime(dt.Rows[0]["effdate"]).ToString("dd-MMM-yyyy");
                this.txtToamt.Text = Convert.ToDouble(dt.Rows[0]["loanamt"]).ToString();
                this.txtinsamt.Text = Convert.ToDouble(dt.Rows[0]["perinstlamt"]).ToString();

            }



            switch (comcod)
            {
                case "3365":
                    this.isFormulaChekcbox.Visible = true;
                    this.isFormulaDiv.Visible = true;

                    isFormulaChekcbox_CheckedChanged(null, null);

                    break;
                default:
                    this.isFormulaChekcbox.Visible = false;
                    this.isFormulaDiv.Visible = false;

                    break;

            }
        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = (DataTable)ViewState["tblln"];
                string lnno = "";
                string lnnoqu = this.Request.QueryString["lnno"] ?? "";
                string textamt = this.txtToamt.Text == "" ? "0" : this.txtToamt.Text;
                double loanamt = Convert.ToDouble(textamt);

                string isformula = isFormulaChekcbox.Checked == true ? this.ddlFormulatype.SelectedValue.ToString() : "";

                if (this.ddlPrevLoanList.Items.Count > 0)
                {
                    this.txtCurDate.Enabled = false;
                    lnno = this.ddlPrevLoanList.SelectedValue.ToString();

                    DataTable dt1 = (DataTable)ViewState["tblln1"];
                    if (dt1 == null)
                    {
                        return;
                    }
                    DataRow[] dr = dt1.Select("lnno='" + lnno + "'");
                    loanamt = (dr.Length == 0) ? 0.00 : Convert.ToDouble(dr[0]["lnamt"]); // Get Loan Amount
                    isformula = dr[0]["isformula"].ToString();
                }
                if (lnnoqu.Length > 0)
                {
                    this.txtCurDate.Enabled = false;


                    DataTable dt1 = (DataTable)ViewState["tblln1"];
                    if (dt1 == null)
                    {
                        return;
                    }
                    DataRow[] dr = dt1.Select("lnno='" + lnnoqu + "'");
                    loanamt = (dr.Length == 0) ? 0.00 : Convert.ToDouble(dr[0]["lnamt"]); // Get Loan Amount
                    lnno = lnnoqu;
                }

                lbtnTotal_Click(null, null);

                string comcod = this.GetComeCode();
                if (this.ddlPrevLoanList.Items.Count == 0)
                    this.GetLNNo();

                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");

                string txtUptopaid = this.txtUptoDate.Text.Trim().ToString() == "" ? "01-Jan-1900" : Convert.ToDateTime(this.txtUptoDate.Text.Trim()).ToString("dd-MMM-yyyy");

                if (lnnoqu.Length > 0)
                {
                    lnno = lnnoqu;
                }
                else
                {
                    lnno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();

                }
                string empid = this.ddlEmpList.SelectedValue.ToString();
                double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", "")));
                double tcomppay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(comppay)", "")) ? 0.00 : dt.Compute("sum(comppay)", "")));
                string loantype = ddlLoantype.SelectedValue.ToString();
                string uptopaid = this.txtPaidAmt.Text.ToString() == "" ? "0" : this.txtPaidAmt.Text.ToString();

                toamt = toamt + tcomppay;
                //  double ttlinsamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttlinsamt)", "")) ? 0.00 : dt.Compute("sum(ttlinsamt)", "")));
                double tloan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", "")));
                //  tloan = comcod == "3365" ? ttlinsamt : tloan;
                double balance1 = loanamt - tloan;
                string balance = balance1 == 0 ? "Level" : balance1.ToString();
                //if (loanamt < tloan)
                //{
                //    string msg = "Sorry, Please adjust or delete before add Installment. Balance amount " + balance;
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                //    return;
                //}
                //else if (loanamt > tloan)
                //{
                //    string msg = "Sorry, Please adjust the amount. Balance amount " + balance;
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                //    return;
                //}
                bool result;
                //Delete Loaninfo
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETELNINFO", lnno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATELN", "LNINFB", lnno, curdate, toamt.ToString(), "", loantype, uptopaid, isformula, txtUptopaid, "", "", "", "", "", "");

                if (!result)
                {
                    return;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string lndate = Convert.ToDateTime(dt.Rows[i]["lndate"]).ToString("dd-MMM-yyyy");
                    string lnamt = Convert.ToDouble(dt.Rows[i]["lnamt"]).ToString();
                    string comppay = Convert.ToDouble(dt.Rows[i]["comppay"]).ToString();

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATELN", "LNINFA", lnno, empid, lndate, lnamt, comppay,
                        "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }
                string genno = this.Request.QueryString["genno"] ?? "";
                if (genno.Length > 0)
                {
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "LOANGENEREATEDUPODATE", genno, lnno, "", "", "", "", "", "", "", "", "", "", "", "", "");

                }
                string Msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);
                lbtnOk_Click(null, null);

                lnno = this.Request.QueryString["lnno"] ?? "";
                if (lnno.Length > 0)
                {
                    Response.Redirect("~/F_81_Hrm/F_85_Lon/EmpLoanStatus?Type=Report&comcod=&lnno=update");
                }
            }
            catch (Exception ex)
            {
                string Msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
            }
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string ulndat = this.txtUptoDate.Text.Trim().ToString() == "" ? "01-Jan-1900" : this.txtUptoDate.Text.Trim().ToString();
            DataTable dt = (DataTable)ViewState["tblln"];
            string lnno = "";
            string lnnoqu = this.Request.QueryString["lnno"] ?? "";
            string textamt = this.txtToamt.Text == "" ? "0" : this.txtToamt.Text;
            double loanamt = Convert.ToDouble(textamt);
            string isformultype = "";

            if (this.ddlPrevLoanList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                lnno = this.ddlPrevLoanList.SelectedValue.ToString();

                DataTable dt1 = (DataTable)ViewState["tblln1"];
                if (dt1 == null)
                {
                    return;
                }
                DataRow[] dr = dt1.Select("lnno='" + lnno + "'");
                loanamt = (dr.Length == 0) ? 0.00 : Convert.ToDouble(dr[0]["lnamt"]); // Get Loan Amount
                //double comppay = (dr.Length == 0) ? 0.00 : Convert.ToDouble(dr[0]["comppay"]); // Get Loan Amount            
            }
            if (lnnoqu.Length > 0)
            {
                this.txtCurDate.Enabled = false;
                DataTable dt1 = (DataTable)ViewState["tblln1"];
                if (dt1 == null)
                {
                    return;
                }
                DataRow[] dr = dt1.Select("lnno='" + lnnoqu + "'");
                loanamt = (dr.Length == 0) ? 0.00 : Convert.ToDouble(dr[0]["lnamt"]); // Get Loan Amount
                lnno = lnnoqu;
            }
            for (int i = 0; i < this.gvloan.Rows.Count; i++)
            {
                string Insdate = Convert.ToDateTime(((TextBox)this.gvloan.Rows[i].FindControl("txtgvinstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string InsAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvloan.Rows[i].FindControl("gvtxtamt")).Text.Trim())).ToString();
                string comppay = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvloan.Rows[i].FindControl("gvtxtamtComppay")).Text.Trim())).ToString();
                string ttlinsamta = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvloan.Rows[i].FindControl("gvtxtamtttlinsamt")).Text.Trim())).ToString();
                string paidamt = ((Label)this.gvloan.Rows[i].FindControl("lblStatus")).Text.ToString();
                isformultype = ((Label)this.gvloan.Rows[i].FindControl("lblisformula")).Text.ToString();
                dt.Rows[i]["lndate"] = Insdate;
                dt.Rows[i]["lnamt"] = InsAmt;
                dt.Rows[i]["comppay"] = comppay;
                dt.Rows[i]["ttlinsamt"] = Convert.ToDouble(InsAmt) + Convert.ToDouble(comppay);

                if (Convert.ToDateTime(Insdate) < Convert.ToDateTime(ulndat))
                {
                    dt.Rows[i]["paidamt"] = "False";
                }
                else
                {
                    dt.Rows[i]["paidamt"] = paidamt;
                }
            }
            double tloan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttlinsamt)", "")) ? 0.00 : dt.Compute("sum(ttlinsamt)", ""))); //Installment Loan
                                                                                                                                             //  double tloan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", ""))); //Installment Loan

            // tloan = (isformultype != "") ? ttlinsamt : tloan;
            double balance1 = loanamt - tloan;
            string balance = balance1 == 0 ? "Level" : balance1.ToString();

            //if (loanamt < tloan)
            //{
            //    string msg = "Sorry, Please adjust or delete before add Installment. Balance amount " + balance;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            //    return;
            //}

            //else if (loanamt > tloan)
            //{
            //    string msg = "Sorry, Please adjust the amount. Balance amount " + balance;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            //    return;
            //}
            DataTable dt2 = dt.Copy();
            DataView dv1 = dt2.DefaultView;
            dv1.RowFilter = ("lndate<'" + ulndat + "'");
            dt2 = dv1.ToTable();
            // string totalamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(lnamt)", "")) ? 0.00 : dt2.Compute("sum(lnamt)", ""))).ToString();
            string totalamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(ttlinsamt)", "")) ? 0.00 : dt2.Compute("sum(ttlinsamt)", ""))).ToString();

            //  totalamt = (isformultype != "") ? ttlinsamtx : totalamt;

            this.txtPaidAmt.Text = totalamt;
            ViewState["tblln"] = dt;
            //DataTable dt1 = (DataTable)ViewState["tblln1"];
            this.Data_DataBind();
        }
        protected void lbtnAddInstallment_Click(object sender, EventArgs e)
        {
            lbtnTotal_Click(null, null);
            DataTable dt1 = (DataTable)ViewState["tblln1"];
            DataTable dt = (DataTable)ViewState["tblln"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string lnno = "";
            string lnnoqu = this.Request.QueryString["lnno"] ?? "";
            if (this.ddlPrevLoanList.Items.Count > 0 || lnnoqu.Length > 0)
            {
                this.txtCurDate.Enabled = false;

                if (lnnoqu.Length > 0)
                {
                    lnno = lnnoqu;
                }
                else
                {
                    lnno = this.ddlPrevLoanList.SelectedValue.ToString();

                }

                DataRow[] dr = dt1.Select("lnno='" + lnno + "'");
                double loanamt = (dr.Length == 0) ? 0.00 : Convert.ToDouble(dr[0]["lnamt"]);
                // double comppay = (dr.Length == 0) ? 0.00 : Convert.ToDouble(dr[0]["comppay"]);

                //  double tloan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00 : dt.Compute("sum(lnamt)", "")));
                double tloan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttlinsamt)", "")) ? 0.00 : dt.Compute("sum(ttlinsamt)", "")));

                double balance1 = loanamt - tloan;
                string balance = balance1 == 0 ? "Level" : balance1.ToString();
                //if (loanamt <= tloan)
                //{
                //    string msg = "Sorry, Please adjust or delete before add Installment. Balance amount " + balance;
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                //    return;
                //}
                //else if (loanamt > tloan)
                //{
                //    string msg = "Please adjust the amount. Balance amount " + balance;
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                //}
                if (dr.Length != 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["lndate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    dr1["lnamt"] = 0;
                    dr1["comppay"] = 0;
                    dr1["ttlinsamt"] = 0;
                    dr1["id"] = 0;
                    dr1["paidamt"] = "True";
                    dt.Rows.Add(dr1);
                }
            }

            //Session["tblln"] = dt;
            this.gvloan.DataSource = dt;
            this.gvloan.DataBind();
            /// this.lbtnTotal_Click(null, null);
        }
        protected void chkAddIns_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddIns.Checked)
            {
                this.lbtnAddInstallment.Visible = true;


            }
            else
            {


                this.lbtnAddInstallment.Visible = false;
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            DataTable dt = (DataTable)ViewState["tblln"];
            string msg = "Data deleted successfully ";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            int ins = this.gvloan.PageSize * this.gvloan.PageIndex + index;
            dt.Rows[ins].Delete();
            ViewState.Remove("tblln");
            DataView dv = dt.DefaultView;
            ViewState["tblln"] = dv.ToTable();
            this.Data_DataBind();
        }

        protected void gvloan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkDel");
                CheckBox rfnTxt = (CheckBox)e.Row.FindControl("chkRefund");

                string paidamt = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paidamt")).ToString();
                //if (paidamt == "False")
                //{
                //    e.Row.ToolTip = "Already Paid";
                //    lnkbtn.Visible = false;
                //    e.Row.Enabled = false;
                //}
                if (paidamt == "False")
                {
                    e.Row.ToolTip = "Already Paid";
                    rfnTxt.Visible = false;
                    e.Row.Enabled = false;
                }

            }
        }

        protected void isFormulaChekcbox_CheckedChanged(object sender, EventArgs e)
        {
            if (isFormulaChekcbox.Checked)
            {
                this.isFormulaDiv.Visible = true;
            }
            else
            {

                this.txtEmployePayment.Text = "0.00";
                this.txtCompPaid.Text = "0.00";
                this.isFormulaDiv.Visible = false;
            }
        }

        protected void ddlFormulatype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private double GetCalculateParcentage(double amt, double persantage)
        {
            string calculationtype = ddlFormulatype.SelectedValue.ToString();
            if (calculationtype == "P")
            {
                amt = (persantage / 100) * amt;

            }


            return amt;
        }

        protected void gvtxtamtttlinsamt_TextChanged(object sender, EventArgs e)
        {
            double comppay = 40.00;
            double emppay = 60.00;
            string comcod = this.GetComeCode();


            TextBox textBox = sender as TextBox;
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            int rowindex = row.RowIndex;

            Label ttday = (Label)row.FindControl("lblgvabsday02");
            TextBox ttlinstamt = (TextBox)row.FindControl("gvtxtamtttlinsamt");
            TextBox gvtxtamtComppay = (TextBox)row.FindControl("gvtxtamtComppay");
            TextBox gvtxtamt = (TextBox)row.FindControl("gvtxtamt");
            double instamt = Convert.ToDouble("0" + ttlinstamt.Text.Trim());


            double compvalue = GetCalculateParcentage(instamt, comppay);
            double empvalue = GetCalculateParcentage(instamt, emppay);

            gvtxtamtComppay.Text = compvalue.ToString();
            gvtxtamt.Text = empvalue.ToString();

        }

        protected void lnkSearcEMP_Click(object sender, EventArgs e)
        {
            GetEmplist();
        }

        protected void lnkbtnRefund_Click(object sender, EventArgs e)
        {
            try
            {
                bool result;
                string comcod = this.GetComeCode();
                this.lnkCalculation_Click(null,null);
                DataTable dt = (DataTable)ViewState["tblln"];
                string textamt = this.txtRefunAmt.Text == "" ? "0" : this.txtRefunAmt.Text;
                double loanamt = Convert.ToDouble(textamt);
                string lnno = this.ddlPrevLoanList.SelectedValue.ToString();
                string curdate = this.txtCurDate.Text.ToString() ;
                string refnotes = this.txtRefunds.Text.ToString().Trim();
                string empid = this.ddlEmpList.SelectedValue.ToString();

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "LOANREFUND", "LNINFB", lnno, curdate, loanamt.ToString(), refnotes, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    return;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string isrefund = dt.Rows[i]["isrefund"].ToString();
                   
                         
                        string lnamt = Convert.ToDouble(dt.Rows[i]["lnamt"]).ToString();
                        string comppay = Convert.ToDouble(dt.Rows[i]["comppay"]).ToString();
                        string id = dt.Rows[i]["id"].ToString();

                        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "LOANREFUND", "LNINFA", lnno, empid, isrefund, id, "",
                            "", "", "", "", "", "", "", "", "");
                        if (!result)
                            return;
                   
                   
                }
               
    
                string Msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);
                lbtnOk_Click(null, null);
            }
            catch (Exception ex)
            {
               
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        protected void lnkCalculation_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblln"];
            double refundamt = 0.00;
            for (int i = 0; i < this.gvloan.Rows.Count; i++)
            {

                CheckBox chkRefund = ((CheckBox)this.gvloan.Rows[i].FindControl("chkRefund"));

                if (chkRefund.Checked)
                {

                    string InsAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvloan.Rows[i].FindControl("gvtxtamt")).Text.Trim())).ToString();

                    dt.Rows[i]["isrefund"] = "True";
                    refundamt += Convert.ToDouble(InsAmt);

                }
                else
                {
                    dt.Rows[i]["isrefund"] = "False";

                }

            }
            ViewState["tblln"] = dt;
            this.txtRefunAmt.Text = refundamt.ToString();
            this.Data_DataBind();

        }
    }
}