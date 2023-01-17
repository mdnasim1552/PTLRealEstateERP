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
namespace RealERPWEB.F_21_MKT
{
    public partial class MktSalsPaymentPro : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");


                Session.Remove("Unit");
                this.GetProjectName();


                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES WITH PAYMENT  PROPOSAL ";
                this.txtAggrementdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txthandoverdate.Text = System.DateTime.Today.AddYears(2).ToString("dd-MMM-yyyy");


            }

        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }


        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
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
                //this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                //this.lblProjectmDesc.Visible = true;
                this.lblProjectdesc.Visible = true;
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
            //this.lblProjectmDesc.Text = "";
            //this.lblProjectmDesc.Visible = false;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }




        private void LoadGrid()
        {
            ViewState.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "DETAILSIRINFINFORMATION", PactCode, "", "", "", "", "", "", "", "");
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
            this.CustInf();

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

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");

            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            this.gvCost.DataSource = ds1.Tables[1];
            this.gvCost.DataBind();
            Session["tblcost"] = ds1.Tables[1];



            //this.ddlSalesTeam.DataTextField = "gdesc";
            //this.ddlSalesTeam.DataValueField = "gcod";
            //this.ddlSalesTeam.DataSource = ds1.Tables[2];
            //this.ddlSalesTeam.DataBind();

            //DataTable dtst = ds1.Tables[2];
            //DataRow[] dr = dtst.Select("gdesc1 <>''");
            //if (dr.Length > 0)
            //{
            //    this.ddlSalesTeam.SelectedValue = dr[0]["gcod"].ToString().Trim();

            //}
            //this.ddlCollectionTeam.DataTextField = "gdesc";
            //this.ddlCollectionTeam.DataValueField = "gcod";
            //this.ddlCollectionTeam.DataSource = ds1.Tables[3];
            //this.ddlCollectionTeam.DataBind();
            //DataTable dtcc = ds1.Tables[3];

            //DataRow[] dr1 = dtcc.Select("gdesc1 <>''");
            //if (dr1.Length > 0)
            //{
            //    this.ddlCollectionTeam.SelectedValue = dr1[0]["gcod"].ToString().Trim();

            //}




            //Sales Team, CR Team
            DataTable dtscr = ds1.Tables[2].Copy();
            DataView dv;
            dv = dtscr.DefaultView;
            dv.RowFilter = ("secid like '9402%'");
            this.ddlSalesTeam.DataTextField = "gdesc";
            this.ddlSalesTeam.DataValueField = "gcod";
            this.ddlSalesTeam.DataSource = dv.ToTable();
            this.ddlSalesTeam.DataBind();

            dv = dtscr.DefaultView;
            dv.RowFilter = ("secid like '9403%'");
            this.ddlCollectionTeam.DataTextField = "gdesc";
            this.ddlCollectionTeam.DataValueField = "gcod";
            this.ddlCollectionTeam.DataSource = dv.ToTable(); ;
            this.ddlCollectionTeam.DataBind();

            DataTable dtst = ds1.Tables[4];
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


            Session["tblPay"] = ds1.Tables[3];
            this.gvPayment.DataSource = ds1.Tables[3];
            this.gvPayment.DataBind();

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

            ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dtpay.Compute("sum(schamt)", "")) ? 0.00 :
                   dtpay.Compute("sum(schamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPayment.FooterRow.FindControl("lftoAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dtpay.Compute("sum(toamt)", "")) ? 0.00 :
                   dtpay.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");


        }


        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
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
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "INSERTORUPDATECUSTINF", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

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
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "INSERTORUPDATESALGINF1", PactCode, Usircode, Gcode, UNumber, Usize, Amt.ToString(), Remarks, "", "", "", "", "", "", "", "");

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

        protected void lbtnUpdateCAST_OnClick(object sender, EventArgs e)
        {

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
            string Gcode = "", Gval = "";
            Gcode = this.ddlSalesTeam.SelectedValue.ToString();
            // Gval = this.ddlSalesTeam.SelectedItem.Text.Trim();
            bool res;

            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "DELETESCTEAM", PactCode, Usircode, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (!res)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;

            }

            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51005", "T", Gcode, "", "", "", "", "", "", "", "", "", "");

            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }

            Gcode = this.ddlCollectionTeam.SelectedValue.ToString();
            // Gval = this.ddlCollectionTeam.SelectedItem.Text.Trim();

            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51007", "T", Gcode, "", "", "", "", "", "", "", "", "", "");
            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }


            string aggrementdate = this.txtAggrementdate.Text;
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51001", "D", aggrementdate, "", "", "", "", "", "", "", "", "", "");
            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }
            string handoverdate = this.txthandoverdate.Text;
            res = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "INSERTORUPDATECUSTINF", PactCode, Usircode, "51002", "D", handoverdate, "", "", "", "", "", "", "", "", "", "");
            if (res == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            }

        }

        protected void lUpdatpayment_Click(object sender, EventArgs e)
        {
            //Min Booking

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            DataTable basicinfo = (DataTable)Session["UsirBasicInformation"];
            double minbam = Convert.ToDouble(basicinfo.Rows[0]["minbam"]);



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

                    string insnum = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvinsnum")).Text).ToString();
                    string duration = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvduration")).Text;
                    double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));

                    // string Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim())).ToString();
                    if (Amt != 0)
                    {
                        MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "INSERTORUPDATEPAYMENTINF", PactCode, Usircode, Gcode, insnum, duration, Amt.ToString(), "", "", "", "", "", "", "", "", "");
                    }

                }

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

                double insnum = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvinsnum")).Text);
                string duration = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvduration")).Text;
                insnum = (insnum > 0) ? insnum : 1.00;
                dt.Rows[i]["insnum"] = insnum;
                dt.Rows[i]["schamt"] = Amt;
                dt.Rows[i]["toamt"] = insnum * Amt;
                ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvinsnum")).Text = insnum.ToString("#,##0;-#,##0; ");
                ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text = Amt.ToString("#,##0.00;-#,##0.00; ");
                ((Label)this.gvPayment.Rows[i].FindControl("lblgvtoAmt")).Text = (insnum * Amt).ToString("#,##0;-#,##0; ");

            }
            Session["tblPay"] = dt;
            Amount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ? 0.00 : dt.Compute("sum(schamt)", "")));
            double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00 : dt.Compute("sum(toamt)", "")));
            if (Amount > 0)
            {
                ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text = Amount.ToString("#,##0;(#,##0); ");
                ((Label)this.gvPayment.FooterRow.FindControl("lftoAmt")).Text = toamt.ToString("#,##0;(#,##0); ");
            }
            Session["Amt11"] = toamt.ToString("#,##0;(#,##0);");
        }

        protected void ddlSalesTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblPay"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            string Gcode = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lblgvItmCode3")).Text.Trim();
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MTKSALPAYPROPOSAL", "DELETEINSTALLMENT", PactCode, Usircode, Gcode, "", "", "", "", "", "", "", "", "", "", "", "");

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


    }
}

