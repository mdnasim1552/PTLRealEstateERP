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
namespace RealERPWEB.F_17_Acc
{
    public partial class AccPayLandOwner : System.Web.UI.Page
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


                //Session.Remove("Unit");

                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "LAND WITH PAYMENT SCHEDULE INFORMATION ";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.RadioButtonList1.SelectedIndex = 0;
                //this.RadioButtonList1.SelectedIndex = 1;
                this.Master.Page.Title = "LAND WITH PAYMENT SCHEDULE INFORMATION ";

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_LANDOWNER", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
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
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.RadioButtonList1.Enabled = false;
                this.lblandown.Text = this.ddlLand.SelectedItem.Text.Substring(13);
                this.lblandown.Visible = true;
                this.ddlLand.Visible = false;
                if (this.RadioButtonList1.SelectedIndex == 0)
                {

                    this.MultiView.ActiveViewIndex = 0;
                    this.GetLownerAndPaymntschdule();
                }
                else
                {
                    this.MultiView.ActiveViewIndex = 1;

                }




                //this.RadioButtonList1.Visible = false;



                //this.LoadGrid();

                // this.BtnEnabled();



            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
                this.RadioButtonList1.Enabled = true;
            }
        }



        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;

            this.ddlLand.Visible = true;
            this.lblandown.Text = "";
            this.lblandown.Visible = false;
            this.MultiView.ActiveViewIndex = -1;
            this.RadioButtonList1.SelectedIndex = 0;
            //this.RadioButtonList1.Visible = false;


            //this.gvSpayment.DataSource = null;
            //this.gvSpayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)

        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string custid = this.ddlLand.SelectedValue.ToString();
            ReportDocument rptStatus = new RealERPRPT.R_17_Acc.RptLandPaymentSchedule();


            DataTable dtstatus = (DataTable)Session["tpripay1"];



            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string UsirCode = this.ddlLand.SelectedValue.ToString();
            DataSet ds5 = MktData.GetTransInfo(comcod, "SP_ENTRY_LANDOWNER", "PERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            if (ds5 == null)
                return;
            // DataTable dtcust = ds5.Tables[0];
            string Owneradd = ds5.Tables[0].Rows.Count == 0 ? "" : ds5.Tables[0].Rows[5]["gdesc1"].ToString();
            string Ownermob = ds5.Tables[0].Rows.Count == 0 ? "" : ds5.Tables[0].Rows[6]["gdesc1"].ToString();
            ///s1.Tables[2].Rows.Count == 0?""
            //dtcust.Columns[5]rows["gdesc1"].ToString();


            //string custname = dtcust.Rows[0]["custadd"].ToString();
            //string custadd = dtcust.Rows[0]["custadd"].ToString();
            // string custmob = dtcust.Rows[0]["custmob"].ToString();
            //string pactdesc = dtcust.Rows[0]["pactdesc"].ToString();
            //string munit = dtcust.Rows[0]["munit"].ToString();
            //string udesc = dtcust.Rows[0]["udesc"].ToString();
            //string usize = Convert.ToDouble(dtcust.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");


            //double SAmount = Convert.ToDouble("0" + ((Label)this.gvCustPayment.FooterRow.FindControl("lfAmt")).Text);
            //double PAmount = Convert.ToDouble("0" + ((Label)this.gvCustPayment.FooterRow.FindControl("lgvfpayamt")).Text);

            TextObject rptCompanyName = rptStatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCompanyName.Text = comnam;

            TextObject rptProjectName = rptStatus.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            rptProjectName.Text = this.ddlProjectName.SelectedItem.Text.Substring(18);

            TextObject rptLandOwner = rptStatus.ReportDefinition.ReportObjects["txtLandowner"] as TextObject;
            rptLandOwner.Text = this.ddlLand.SelectedItem.Text.Substring(13);
            TextObject rptCustAdd = rptStatus.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            rptCustAdd.Text = Owneradd + ", " + "Mobile: " + Ownermob;
            //TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["pactdesc"] as TextObject;
            //rptpactdesc.Text = pactdesc;
            //TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
            //rptUsize.Text = udesc + ", " + usize + " " + munit;

            //TextObject txtbalamt = rptStatus.ReportDefinition.ReportObjects["txtbalamt"] as TextObject;
            //txtbalamt.Text = (SAmount - PAmount).ToString("#,##0;(#,##0); "); ;
            TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptStatus.SetDataSource(dtstatus);
            // rptStatus.SetDataSource(dtstatusTable[0]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptStatus.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptStatus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }






        private void BtnEnabled()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Sales":
                    ((LinkButton)this.gvPersonalInfo.FooterRow.FindControl("lUpdatPerInfo")).Enabled = true;

                    break;

            }
        }


        protected void lbtnBack_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Text = "";




        }

        private void GetLownerAndPaymntschdule()
        {

            Session.Remove("tblPay");
            Session.Remove("tpripay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string UsirCode = this.ddlLand.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_LANDOWNER", "PERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            Session["tpripay"] = ds1.Tables[2];
            Session["tpripay1"] = ds1.Tables[1];




            if (ds1.Tables[1].Rows.Count > 0)
            {

                this.Panel3.Visible = false;
                Session["tblPay"] = ds1.Tables[1];
                this.gvPayment.DataSource = ds1.Tables[1];
                this.gvPayment.DataBind();
                ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("sum(schamt)", "")) ? 0.00 : ds1.Tables[1].Compute("sum(schamt)", ""))).ToString("#,##0;(#,##0); ");



            }
            else
            {

                this.Panel3.Visible = false;
                Session["tblPay"] = ds1.Tables[2];
                DataView dv1 = ds1.Tables[2].DefaultView;
                dv1.RowFilter = ("gcod like '84001%'");
                dv1.Sort = "gcod";
                this.gvPayment.DataSource = dv1.ToTable();
                this.gvPayment.DataBind();



            }


            //addamt = (ds1.Tables[5].Rows.Count > 0) ? Convert.ToDouble(ds1.Tables[5].Rows[0]["adamt"]) : 0.00;
            //dedamt = (ds1.Tables[5].Rows.Count > 0) ? Convert.ToDouble(ds1.Tables[5].Rows[0]["dedamt"]) : 0.00;
            //this.txtAggrementdate.Text = (ds1.Tables[7].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[7].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            //this.txthandoverdate.Text = (ds1.Tables[7].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[7].Rows[0]["hdate"]).ToString("dd-MMM-yyyy");
            //this.Calculation();
            //this.lbtnTotalCost_Click(null, null);

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
            string Usircode = this.ddlLand.Text.Trim();
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_LANDOWNER", "INSERTORUPDATEPERINF", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

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



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlLand.Text.Trim();
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPayment.Rows[i].FindControl("lblgvItmCode3")).Text.Trim();
                string schDate = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double Amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                // string Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim())).ToString();
                if (Amount != 0)
                {
                    MktData.UpdateTransInfo(comcod, "SP_ENTRY_LANDOWNER", "INSERTORUPDATEPAYMENTINF", PactCode, Usircode, Gcode, schDate, Amount.ToString(), "", "", "", "", "", "", "", "", "", "");
                }

            }


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);



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

        }

        protected void ddlSalesTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            int i, k = 0;

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

                if (ASTUtility.Left(gcode, 5) == "84985")
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
            //DataTable dt2 = dt;

            double Tamt = Convert.ToInt32("0" + txtToatal.Text);
            //double Tamt = Convert.ToDouble(((TextBox)this.FindControl("txtToatal")).Text.Trim());
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


        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblPay"];
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlLand.Text.Trim();
            string Gcode = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lblgvItmCode3")).Text.Trim();
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_LANDOWNER", "DELETEINSTALLMENT", PactCode, Usircode, Gcode, "", "", "", "", "", "", "", "", "", "", "", "");

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







        protected void lbtnsrchland_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtlandowner = "%" + this.txtsrchland.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_LANDOWNER", "GETRESOURCELIST", txtlandowner, "", "", "", "", "", "", "", "");
            this.ddlLand.DataTextField = "sirdesc";
            this.ddlLand.DataValueField = "sircode";
            this.ddlLand.DataSource = ds1.Tables[0];
            this.ddlLand.DataBind();
        }
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {

            if (this.chkVisible.Checked == true)
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string PactCode = this.ddlProjectName.SelectedValue.ToString();
                string Usircode = this.ddlLand.Text.Trim();
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_LANDOWNER", "DELETEPAYMENTINF", PactCode, Usircode, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result == false)
                    return;
                DataTable dt = (DataTable)Session["tblPay"];
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = ("gcod like'84001%'");
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
        protected void chkAddIns_CheckedChanged(object sender, EventArgs e)
        {
            this.PanelAddIns.Visible = this.chkAddIns.Checked;
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
        protected void ibtnFindInstallment_Click(object sender, EventArgs e)
        {
            this.ShowInstallment();
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

        private void ShowInstallment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtsrchisn = this.txtsrchInstallment.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_LANDOWNER", "GETINSTALLMENT", txtsrchisn, "", "", "", "", "", "", "", "");
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

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

