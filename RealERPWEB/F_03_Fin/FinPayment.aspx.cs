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
namespace RealERPWEB.F_03_Fin
{
    public partial class FinPayment : System.Web.UI.Page
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
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES WITH PAYMENT INFORMATION VIEW/EDIT";

                Session.Remove("Unit");
                this.chkVisible.Checked = false;
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetBankName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void GetBankName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcBank.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_FINANCIALMGT", "GETBANKNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1.Tables[0];
            this.ddlBankName.DataBind();
        }



        protected void ibtnFindBank_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetBankName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblBankdesc.Text = this.ddlBankName.SelectedItem.Text;
                this.lblBankmDesc.Text = this.ddlBankName.SelectedItem.Text.Substring(13);
                this.ddlBankName.Visible = false;
                this.lblBankmDesc.Visible = true;
                this.lblBankdesc.Visible = true;
                this.MultiView1.ActiveViewIndex = 0;
                this.GetPayandPaymentSch();

            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.MultiView1.ActiveViewIndex = -1;
                this.ClearScreen();
            }
        }



        private void ClearScreen()
        {
            this.ddlBankName.Visible = true;
            this.lblBankmDesc.Text = "";
            this.lblBankmDesc.Visible = false;
            this.lblBankdesc.Text = "";
            this.lblBankdesc.Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }





        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //string prjname = this.ddlProjectName.SelectedItem.Text;
            //string PactCode = this.ddlProjectName.SelectedValue.ToString();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ////item info
            //DataTable basicinfo = (DataTable)Session["UsirBasicInformation"];
            //string UsirCode = this.lblCode.Text;
            //string ItemName = basicinfo.Rows[0]["udesc"].ToString();
            //string size = Convert.ToDouble(basicinfo.Rows[0]["usize"]).ToString("#,##0.00;(#,##0.00); ");
            ////ToString("#,##0;(#,##0); ")
            //string unit = basicinfo.Rows[0]["munit"].ToString();

            //string concat1 = ItemName + " , " + "Unit Size: " + size + " " + unit;

            ////direct cost
            //string ldiscounttT = this.ldiscountt.Text;
            //string ldiscountpP = this.ldiscountp.Text;

            //string salesteams = ddlSalesTeam.SelectedItem.Text;
            //DataSet dss = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "COMBINEDTABLEFORSALES", PactCode, UsirCode, "", "", "", "", "", "", "");
            //ReportDocument rpcp = new RealERPRPT.R_22_Sal.RptPCPayment() ;

            ////TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            ////CompName.Text = comname;

            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = "Project Name: " + prjname.ToString().Substring(13);


            //TextObject txtItemName = rpcp.ReportDefinition.ReportObjects["txtItemName"] as TextObject;
            //txtItemName.Text = "Unit Description: " + concat1;


            //TextObject txtdist = rpcp.ReportDefinition.ReportObjects["txtdist"] as TextObject;
            //txtdist.Text = "Discount in Tk. " + ldiscounttT;

            //TextObject txtdisp = rpcp.ReportDefinition.ReportObjects["txtdisp"] as TextObject;
            //txtdisp.Text = "Discount in (%) " + ldiscountpP;

            //TextObject txtsalest = rpcp.ReportDefinition.ReportObjects["txtsalest"] as TextObject;
            //txtsalest.Text = "Sales Team: " + salesteams;


            //TextObject txtNetTotalPayment = rpcp.ReportDefinition.ReportObjects["txtNetTotalPayment"] as TextObject;
            //txtNetTotalPayment.Text = this.lblValNetTotalPayment.Text.Trim() ;


            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //TextObject txtcominfo = rpcp.ReportDefinition.ReportObjects["txtcominfo"] as TextObject;
            //txtcominfo.Text = ASTUtility.Cominformation();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Sales With Payment Schedule";
            //    string eventdesc = "Print Payment Schedule";
            //    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " Unit Description: " + concat1;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rpcp.SetDataSource(dss.Tables[0]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rpcp;


            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }





        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }

        private void GetPayandPaymentSch()
        {
            Session.Remove("tblcost");
            Session.Remove("tblPay");
            Session.Remove("tpripay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string ActCode = this.ddlBankName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_FINANCIALMGT", "GETPAYAPMNTSCHDULE", ActCode, "", "", "", "", "", "", "", "");

            Session["tpripay"] = ds1.Tables[2];
            this.gvCost.DataSource = ds1.Tables[0];
            this.gvCost.DataBind();
            Session["tblcost"] = ds1.Tables[0];




            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.Panel3.Visible = false;
                Session["tblPay"] = ds1.Tables[1];
                this.gvPayment.DataSource = ds1.Tables[1];
                this.gvPayment.DataBind();

            }
            else
            {
                this.Panel3.Visible = true;
                Session["tblPay"] = ds1.Tables[2];
                DataView dv1 = ds1.Tables[2].DefaultView;
                dv1.RowFilter = ("gcod like '81001%' or gcod like '81002%' or gcod like '81985%'");
                dv1.Sort = "gcod";
                this.gvPayment.DataSource = dv1.ToTable();
                this.gvPayment.DataBind();



            }

            this.Calculation();
            this.lbtnTotalCost_Click(null, null);


        }
        private void Calculation()
        {

            DataTable dtcost = (DataTable)Session["tblcost"];
            DataTable dtpay = (DataTable)Session["tblPay"];
            ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dtpay.Compute("sum(schamt)", "")) ? 0.00 :
                 dtpay.Compute("sum(schamt)", ""))).ToString("#,##0;(#,##0); ");


        }






        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {
            double Amount = 0;
            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                //Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));

                string Gcode = ((Label)this.gvCost.Rows[i].FindControl("lblgvGcod")).Text.Trim();
                double dAmt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));
                Amount = ASTUtility.Left(Gcode, 5) == "02002" ? (Amount - dAmt) : (Amount + dAmt);
                ((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text = dAmt.ToString("#,##0; -#,##0; ");
            }

            ((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text = Amount.ToString("#,##0;(#,##0); ");


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
            string PactCode = this.ddlBankName.SelectedValue.ToString();

            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvCost.Rows[i].FindControl("lblgvGcod")).Text.Trim();
                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));
                string Remarks = ((TextBox)this.gvCost.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                //if (Amt!=0)
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_FINANCIALMGT", "INSORUPDATEFINGINF1", PactCode, Gcode, Amt.ToString(), Remarks, "", "", "", "", "", "", "", "", "", "", "");

            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);


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

            DataTable dt = (DataTable)Session["tblPay"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("gcod like'81001%'");
            double bookam = Convert.ToDouble((Convert.IsDBNull((dv1.ToTable()).Compute("sum(schamt)", "")) ? 0.00 :
                (dv1.ToTable()).Compute("sum(schamt)", "")));



            /////////

            double a = Convert.ToDouble(Session["amt"]);
            double b = Convert.ToDouble(Session["Amt11"]);
            if (a == b)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string PactCode = this.ddlBankName.SelectedValue.ToString();
                for (int i = 0; i < this.gvPayment.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPayment.Rows[i].FindControl("lblgvItmCode3")).Text.Trim();
                    string schDate = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                    double Amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                    // string Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim())).ToString();
                    if (Amount != 0)
                    {
                        MktData.UpdateTransInfo(comcod, "SP_ENTRY_FINANCIALMGT", "INSORUPPAYMENTINF", PactCode, Gcode, schDate, Amount.ToString(), "", "", "", "", "", "", "", "", "", "", "");
                    }

                }

                ////Log Entry
                //DataTable dtuser = (DataTable)Session["UserLog"];

                //string userid = hst["usrid"].ToString();
                //string Terminal = hst["compname"].ToString();
                //string Sessionid = hst["session"].ToString();

                //string PostedByid = (dtuser.Rows.Count == 0) ? userid : dtuser.Rows[0]["postedbyid"].ToString();
                //string Posttrmid = (dtuser.Rows.Count == 0) ? Terminal : dtuser.Rows[0]["postrmid"].ToString();
                //string PostSession = (dtuser.Rows.Count == 0) ? Sessionid : dtuser.Rows[0]["postseson"].ToString();
                //string Posteddat = (dtuser.Rows.Count == 0) ? System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt") : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                //string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
                //string tblEditSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editseson"].ToString();
                //string tblEdittrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["edittrmid"].ToString();
                //string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");

                //string EditByid = (dtuser.Rows.Count == 0) ? "" : (tblEditByid == "") ? userid : (tblEditByid != "")? userid: tblEditByid;
                //string EditSession = (dtuser.Rows.Count == 0) ? "" : (tblEditSession == "") ? Sessionid : (tblEditSession != "") ? Sessionid : tblEditSession;
                //string EditTrmid = (dtuser.Rows.Count == 0) ? "" : (tblEdittrmid == "") ? Terminal : (tblEdittrmid == "") ? Terminal : tblEdittrmid;
                //string Editdat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : (tblEditDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : (tblEditDat != "01-Jan-1900")? System.DateTime.Today.ToString("dd-MMM-yyyy"): tblEditDat;


                //double tAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ?
                //                       0 : dt.Compute("sum(schamt)", "")));

                //if (tAmt > 0)
                //{
                //    MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEPAYMENTUSERINF", PactCode,  PostedByid, PostSession, Posttrmid, Posteddat, EditByid, EditSession, EditTrmid, Editdat, tAmt.ToString(), "", "", "", "","");
                //}


                //this.lmsg111.Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);


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
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkVisible.Checked == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string PactCode = this.ddlBankName.SelectedValue.ToString();
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_FINANCIALMGT", "DELETEPAYMENTINF", PactCode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
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






        protected void ibtnFindInstallment_Click(object sender, ImageClickEventArgs e)
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
                dr1["pactcode"] = this.ddlBankName.SelectedValue.ToString();
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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_FINANCIALMGT", "GETINSTALLMENT", txtsrchisn, "", "", "", "", "", "", "", "");
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
            string PactCode = this.ddlBankName.SelectedValue.ToString();

            string Gcode = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lblgvItmCode3")).Text.Trim();
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_FINANCIALMGT", "DELETEINSTALLMENT", PactCode, Gcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

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

