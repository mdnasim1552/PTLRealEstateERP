using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccSales : System.Web.UI.Page
    {

        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        ProcessAccess _processAccessMsgdb = new ProcessAccess("ASTREALERPMSG");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
           
                ((Label)this.Master.FindControl("lblTitle")).Text = "Collection Update";
                this.Master.Page.Title = "Collection Update";
                this.GetProjectName();
                this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtEntryDate_CalendarExtender.EndDate = System.DateTime.Today;

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string temProject = this.Request.QueryString["prjcode"] ?? "";
            string txtSProject = (temProject.ToString()).Length == 0 ? "%" + this.txtSrcPro.Text.Trim() + "%" : temProject + "%";


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = "000000000000";

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void Refrsh()
        {

        }

        protected void txtEntryDate_TextChanged(object sender, EventArgs e)
        {
            this.txtEntryDate.BackColor = System.Drawing.Color.Aqua;
        }
        protected void lnkOk0_Click(object sender, EventArgs e)
        {
            try
            {
                this.Refrsh();
                Session.Remove("tblMrr");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
                //string Chequeno = "%" + this.txtSrcChequeNo.Text.Trim() + "%";

                string Chequeno = (this.Request.QueryString["chqno"].ToString()).Length == 0 ? "%" + this.txtSrcChequeNo.Text.Trim() + "%" : this.Request.QueryString["chqno"].ToString() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "REPORTACCSALES", frmdate, todate, pactcode, Chequeno, "", "", "", "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                Session["tblMrr"] = ds1.Tables[0];
                Session["tblMrr1"] = ds1.Tables[1];
                DataTable dt1 = ds1.Tables[0];
                this.HiddenSameDate(dt1);
                this.CalculatrGridTotal();


                double cramt = Convert.ToDouble(dt1.Rows[0]["cramt"]);

                if (cramt < 0)
                {
                    dgv1.Columns[11].Visible = true;
                }

                else
                {
                    // dgv1.Columns[10].Visible = true;
                }

            }
            catch (Exception ex)
            {
                //this.lblmsg.Text = "Error :" + ex.Message;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Available Data Not in Position";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }


        private void HiddenSameDate(DataTable dt1)
        {

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }

            this.Data_Bind();
        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)Session["tblMrr"];
            ((Label)this.dgv1.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dttotal.Compute("Sum(cramt)", "")) ?
          0.00 : dttotal.Compute("Sum(cramt)", ""))).ToString("#,##0;-#,##0; ");
        }
        protected void lnkVouOk_Click(object sender, EventArgs e)
        {

        }

        private void UPdateMrinf(string vounum)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Vounum = vounum;

            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {

                bool chkmr = ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked;
                if (chkmr == true)
                {
                    string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv1.Rows[i].FindControl("lgcUcode")).Text.Trim();
                    string Mrno = ((Label)this.dgv1.Rows[i].FindControl("lgvmrno")).Text.Trim();
                    string Chequeno = ((Label)this.dgv1.Rows[i].FindControl("lgvCheNo")).Text.Trim();

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEEMRINF", actcode, rescode, Mrno, Chequeno, vounum,
                                    "", "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
            }


        }
        private void vounum(string Cactcode, string VouMode)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;

            }

            DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

            if (txtopndate >= Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }
            double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
            string ConAccHead = Cactcode;
            string VNo1 = (ConAccHead.Substring(0, 4) == "1901" ? "C"
                : ((ConAccHead.Substring(0, 2) == "22" || ConAccHead.Substring(0, 2) == "23" || ConAccHead.Substring(0, 2) == "24" || ConAccHead.Substring(0, 2) == "16" || ConAccHead.Substring(0, 2) == "13" || ConAccHead.Substring(0, 2) =="26") ? "J" : "B"));




            string VNo2 = (VNo1 == "J" ? "V" : ((VouMode == "Payment") ? "D" : "C"));
            string VNo3 = Convert.ToString(VNo1 + VNo2);
            string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            Session["NEWVOUNUM"] = dt4;
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintVoucher();

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string projectcode = this.ddlProjectName.SelectedValue.ToString();
            //string projectName = this.ddlProjectName.SelectedItem.Text;


            //ReportDocument rptProSummary = new RealERPRPT.R_17_Acc.rptCollUpdate();
            //TextObject rpttxtPrjName = rptProSummary.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text = "Project Name:- " + projectName;
            //TextObject rpttxtDate = rptProSummary.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtDate.Text = "From: " + this.txtfrmdate.Text + " To: " + this.txttodate.Text;
            //TextObject txtuserinfo = rptProSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Collection Update";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptProSummary.SetDataSource((DataTable)Session["tblMrr"]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptProSummary.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptProSummary;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                case "3310":
                case "3311":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;


                case "3330":
                    vouprint = "VocherPrint6";
                    break;


                case "3332":

                    vouprint = "VocherPrintIns";
                    break;


                case "3101":
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;


                default:
                    vouprint = "VocherPrintMod";
                    break;
            }
            return vouprint;
        }

        private string GetCompInstar()
        {

            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3330":
                    break;

                default:
                    printinstar = "Innstar";
                    break;


            }
            return printinstar;
        }

        private void PrintVoucher()
        {
            try
            {
                DataTable dt12 = (DataTable)Session["NEWVOUNUM"];
                string vounum = dt12.Rows[0]["couvounum"].ToString();
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccPrint.aspx?Type=accVou&vounum=" +
                              vounum + "', target='_blank');</script>";


                //    Hashtable hst = (Hashtable)Session["tblLogin"];
                //    //string usrname = hst["usrname"].ToString();
                //    string comcod = hst["comcod"].ToString();
                //    string comnam = hst["comnam"].ToString();
                //    string comadd = hst["comadd1"].ToString();
                //    string combranch = hst["combranch"].ToString();
                //    string compname = hst["compname"].ToString();
                //    string username = hst["username"].ToString();
                //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


                //    DataTable dt12 = (DataTable)Session["NEWVOUNUM"];
                //    string vounum = dt12.Rows[0]["couvounum"].ToString();
                //    string curvoudat = this.txtEntryDate.Text.Substring(0, 11);



                //    string PrintInstar = this.GetCompInstar();




                //    string CallType =  "PRINTVOUCHER01";



                //    //string vounum = this.ddlPrivousVou.SelectedValue.ToString();

                //    DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", CallType, vounum, PrintInstar, "", "", "", "", "", "", "");
                //    if (_ReportDataSet == null)
                //        return;
                //    DataTable dt = _ReportDataSet.Tables[0];
                //    if (dt.Rows.Count == 0)
                //        return;
                //    double dramt, cramt;
                //    dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                //    cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                //    if (dramt > 0 && cramt > 0)
                //    {
                //        TAmount = cramt;

                //    }
                //    else if (dramt > 0 && cramt <= 0)
                //    {
                //        TAmount = dramt;
                //    }
                //    else
                //    {
                //        TAmount = cramt;
                //    }

                //    DataTable dt1 = _ReportDataSet.Tables[1];
                //    string Vounum = dt1.Rows[0]["vounum"].ToString();
                //    string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //    string refnum = dt1.Rows[0]["refnum"].ToString();
                //    string voutype = dt1.Rows[0]["voutyp"].ToString();
                //    string venar = dt1.Rows[0]["venar"].ToString();
                //    string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                //    string Type = this.CompanyPrintVou();
                //    ReportDocument rptinfo = new ReportDocument();
                //    if (Type == "VocherPrint")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        rpttxtPartyName.Text = "";
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //    }
                //    else if (Type == "VocherPrint1")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //        TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //        txtisunum1.Text = "";
                //        TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //        txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        rpttxtPartyName.Text = "";
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;

                //    }
                //    else if (Type == "VocherPrint2")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //        TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //        txtisunum2.Text = "";
                //        TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //        txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        rpttxtPartyName.Text = "";
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //    }
                //    else if (Type == "VocherPrint3")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //        TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //        txtisunum1.Text = "";
                //        TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //        txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        rpttxtPartyName.Text = "";
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;

                //    }
                //    else if (Type == "VocherPrint5")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
                //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //        TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //        txtisunum1.Text = "";
                //        TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //        txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        rpttxtPartyName.Text = "";
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;

                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //    }
                //    else if (Type == "VocherPrint6")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
                //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        rpttxtPartyName.Text = "";
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;


                //    }

                //    else if (Type == "VocherPrintIns")
                //    {

                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();

                //        //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //        //txtCompanyName.Text = comnam;
                //        //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //        //txtcAdd.Text = comadd;
                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //        //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //        //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        //rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //        //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //        //voutype1.Text = voutype;
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;

                //    }



                //    else if (Type == "VocherPrintMod")
                //    {

                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //    }

                //    else
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";

                //        TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //        txtPosteddate3.Text = "Entry Date: " + Posteddat;

                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;

                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //    }


                //    rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //    TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //    txtCompanyName.Text = comnam;
                //    TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //    txtcAdd.Text = comadd;


                //    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //    voutype1.Text = voutype;


                //    //TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                //    //txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                //    TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //    rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                //    TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


                //    //string comcod = this.GetComeCode();
                //    //string comcod = hst["comcod"].ToString();
                //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //    rptinfo.SetParameterValue("ComLogo", ComLogo);
                //    Session["Report1"] = rptinfo;
                //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void Data_Bind()
        {
            this.dgv1.DataSource = (DataTable)Session["tblMrr"];
            this.dgv1.DataBind();
            DataTable dt1 = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                //    string pactcode = ((Label)dgv1.Rows[i].FindControl("lblgvAccCod")).Text.Trim();
                //    string usircode = ((Label)dgv1.Rows[i].FindControl("lgcUcode")).Text.Trim();
                string mrno = ((Label)dgv1.Rows[i].FindControl("lgvmrno")).Text.Trim();
                string cheqno = ((Label)dgv1.Rows[i].FindControl("lgvCheNo")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)dgv1.Rows[i].FindControl("lbok");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = mrno + cheqno;
                // lbtn1.CommandArgument = pactcode + usircode + mrno + cheqno;
            }
        }


        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                string Sirialno = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvsirialno")).Text.Trim();
                string Recdate = (((TextBox)this.dgv1.Rows[i].FindControl("txtgvReconDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.dgv1.Rows[i].FindControl("txtgvReconDat")).Text.Trim();
                string chqno = ((DropDownList)this.dgv1.Rows[i].FindControl("ddlChq")).SelectedValue.ToString();
                double cramt = ASTUtility.StrPosOrNagative(((Label)this.dgv1.Rows[i].FindControl("lgvcramt")).Text.Trim());


                //lgvcramt



                //double crmat

                dt.Rows[i]["sirialno"] = Sirialno;
                dt.Rows[i]["recondat"] = Recdate;
                dt.Rows[i]["chkmv"] = chkmr;

                if (cramt < 0)
                {
                    dt.Rows[i]["chqno"] = chqno;

                }

                ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblMrr"] = dt;
        }

        protected void chkvmrno_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblMrr"];
            int i = 0;
            string Narration = "", refno = "";
            string payto = "";
            foreach (GridViewRow gv1 in dgv1.Rows)
            {
                bool chkmr = (((CheckBox)gv1.FindControl("chkvmrno")).Checked) ? true : false;

                if (chkmr)
                {

                    if (((CheckBox)gv1.FindControl("chkvmrno")).Enabled)
                    {


                        string type = dt.Rows[i]["paydesc"].ToString();
                        if (type == "CASH")
                        {
                            //RefNo = dt1.Rows[0]["chqno"].ToString();
                            Narration = Narration + "Mr" + dt.Rows[i]["mrno"].ToString() + ", Date:" +
                                        Convert.ToDateTime(dt.Rows[i]["paydate"]).ToString("dd.MM.yyyy") + ", Ref:" +
                                        dt.Rows[i]["refno"].ToString() + "; ";
                        }

                        else
                        {
                            refno = refno + dt.Rows[i]["chqno"].ToString();
                            Narration = Narration + "Mr" + dt.Rows[i]["mrno"].ToString() + ", " + "Date:" +
                                        Convert.ToDateTime(dt.Rows[i]["paydate"]).ToString("dd.MM.yyyy") + ", Ref:" +
                                        dt.Rows[i]["refno"].ToString()
                                        + (dt.Rows[i]["bankname"].ToString() == ""
                                            ? ""
                                            : ", " + "Bank: " + dt.Rows[i]["bankname"].ToString()) +
                                        (dt.Rows[i]["bbranch"].ToString() == ""
                                            ? ""
                                            : ", " + "Branch: " + dt.Rows[i]["bbranch"].ToString()) + "; ";

                        }

                        payto = dt.Rows[i]["custdesc"].ToString();


                    }
                }



                i++;



            }


            this.txtRefNum.Text = refno;
            this.txtNarration.Text = Narration;
            this.txtPayto.Text = payto;



        }

        protected void lbok_Click(object sender, EventArgs e)
        {
            try
            {
                // DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);


                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&"))
                    ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&')
                    : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(
                    HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                    (DataSet)Session["tblusrlog"]);

                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                this.CheckValue();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string Terminal = hst["trmid"].ToString();
                string Sessionid = hst["session"].ToString();
                string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
                //string pactcode = code.Substring(0, 12).ToString();
                //string usircode = code.Substring(12, 12).ToString();
                //string mrno = code.Substring(24, 9).ToString();
                //string cheqno = code.Substring(33).ToString();

                DataTable dt = (DataTable)Session["tblMrr"];
                DataTable dts = (DataTable)Session["tblMrr1"];
                double cramt1 = Convert.ToDouble(dt.Rows[0]["cramt"].ToString());

                string mrno = code.Substring(0, 9).ToString();
                string cheqno = cramt1 < 0 ? dt.Rows[0]["chqno"].ToString() : code.Substring(9).ToString();
                string cheqno2 = code.Substring(9).ToString();

                //DataTable dt = (DataTable)Session["tb"""];
                //DataTable dts = (DataTable)Session["tblMrr1"];

                DataTable dt1 = dt.Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("mrno='" + mrno + "' and chqno='" + cheqno + "'");
                dt1 = dv.ToTable();

                // Details
                DataTable dts1 = dts.Copy();
                DataView dvs = dts.DefaultView;
                dvs.RowFilter = ("mrno='" + mrno + "' and chqno='" + cheqno2 + "'");
                dts1 = dvs.ToTable();




                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string Chk = dt1.Rows[i]["chkmv"].ToString();
                    if (Chk == "False")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check CheckBox');",
                            true);
                        return;
                    }
                }




                //string Cactcode = (dt1.Rows[0]["Cactcode"].ToString());

                //double cramt = Convert.ToDouble(dt1.Rows[0]["cramt"].ToString());
                //string VouMode = (cramt < 0) ? "Payment" : "";
                //this.vounum(Cactcode, VouMode);



                //  DataRow[] dr = dt.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "' and mrno='" + mrno + "' and chqno='" + cheqno + "'");



                //if (Chk == "False")
                //{
                // ((Label)this.Master.FindControl("lblmsg")).Text = "Please Check CheckBox";
                //    return;
                //}




                //DateTime Paydate = Convert.ToDateTime(dr[0]["paydate"]);
                string voudat = this.txtEntryDate.Text.Substring(0, 11);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    string Paydate = Convert.ToDateTime(dt1.Rows[i]["paydate"]).ToString("dd-MMM-yyyy");
                    bool dcon = ASITUtility02.TransPostDateCheque(Convert.ToDateTime(Paydate), Convert.ToDateTime(voudat));
                    if (!dcon)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('Voucher Date is equal or greater Cheque Date');", true);
                        return;
                    }

                }



                //bool dcon = ASITUtility02.TransPostDateCheque(Paydate, Convert.ToDateTime(voudat));
                //if (!dcon)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is equal or greater Cheque Date');", true);
                //    return;
                //}


                /////////////////--------------------------------------------------
                //Existing MR

                //if(Convert.ToDouble( dt.Rows[0]["cramt"]) >0)
                //{
                string cheqno1 = code.Substring(9).ToString();


                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGCOLUP", mrno, "", "",
                    cheqno1, "", "", "", "", "");
                if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this MR No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                //}



                //For Ribate
                string vounum = "";
                double chkcramt, chkdramt;
                chkcramt = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(cramt)", ""))
                    ? 0
                    : dts1.Compute("sum(cramt)", "")));

                chkdramt = Convert.ToDouble((Convert.IsDBNull(dts1.Compute("sum(dramt)", ""))
                    ? 0
                    : dts1.Compute("sum(dramt)", "")));
                string Cactcode = (dt1.Rows[0]["Cactcode"].ToString());
                string RefNo = dt1.Rows[0]["chqno"].ToString();
                string recondat =
                    (Convert.ToDateTime(dt1.Rows[0]["recondat"].ToString().Trim()).ToString("dd-MMM-yyyy") ==
                     "01-Jan-1900")
                        ? voudat
                        : dt1.Rows[0]["recondat"].ToString().Trim();

                if (chkcramt == 0.00)
                {
                    vounum = "ADJUSTMENT0000";
                    string pactcoded = dts1.Rows[0]["pactcode"].ToString();
                    string usirocded = dts1.Rows[0]["usircode"].ToString();


                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEEMRINF", pactcoded,
                        usirocded, mrno, cheqno1, vounum,
                        "", "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }


                else
                {



                    double cramt = Convert.ToDouble(dt1.Rows[0]["cramt"].ToString());
                    string VouMode = (cramt < 0) ? "Payment" : "";
                    this.vounum(Cactcode, VouMode);
                    DataTable dt12 = (DataTable)Session["NEWVOUNUM"];
                    string Narration = this.txtNarration.Text;



                    ///-----------------------------------------------------///////////////////
                    ///



                    //string type = dt.Rows[0]["paydesc"].ToString();
                    //if (type == "CASH")
                    //{
                    //    //RefNo = dt1.Rows[0]["chqno"].ToString();
                    //    Narration = "Mr" + dt1.Rows[0]["mrno"].ToString() + ", Date:" +
                    //                Convert.ToDateTime(dt1.Rows[0]["paydate"]).ToString("dd.MM.yyyy") + ", Ref:" +
                    //                dt1.Rows[0]["refno"].ToString() + "; ";
                    //}

                    //else
                    //{
                    //    RefNo = dt1.Rows[0]["chqno"].ToString();
                    //    Narration = "Mr" + dt1.Rows[0]["mrno"].ToString() + ", " + "Date:" +
                    //                Convert.ToDateTime(dt1.Rows[0]["paydate"]).ToString("dd.MM.yyyy") + ", Ref:" +
                    //                dt1.Rows[0]["refno"].ToString()
                    //                + (dt1.Rows[0]["bankname"].ToString() == ""
                    //                    ? ""
                    //                    : ", " + "Bank: " + dt1.Rows[0]["bankname"].ToString()) +
                    //                (dt1.Rows[0]["bbranch"].ToString() == ""
                    //                    ? ""
                    //                    : ", " + "Branch: " + dt1.Rows[0]["bbranch"].ToString()) + "; ";

                    //}



                    //int Reflenght = RefNo.Length;
                    //if (Reflenght > 0)
                    //{
                    //    RefNo = RefNo.Substring(0, Reflenght - 2);
                    //}
                    int lenght = Narration.Length;
                    Narration = Narration.Substring(0, lenght - 2);
                    /////////---------------------------------------
                    vounum = dt12.Rows[0]["couvounum"].ToString();

                    string refnum = this.txtRefNum.Text;
                    string srinfo = dt1.Rows[0]["sirialno"].ToString().Trim();

                    string vounarration1 = Narration;
                    string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                    vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                    string vouno = vounum.Substring(0, 2).ToString();
                    string voutype = (vouno == "JV"
                        ? "Journal Voucher"
                        : (vouno == "CD"
                            ? "Cash Payment Voucher"
                            : (vouno == "BD"
                                ? "Bank Payment Voucher"
                                : (vouno == "CC"
                                    ? "Cash Deposit Voucher"
                                    : (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
                    string cactcode = (vouno == "JV" ? "000000000000" : Cactcode);
                    string vtcode = "99";
                    string edit = "EDIT";


                    //  Hashtable hst = (Hashtable)Session["tblLogin"];
                    //string comcod = hst["comcod"].ToString();
                    //string userid = hst["usrid"].ToString();
                    //string Terminal = hst["compname"].ToString();
                    //string Sessionid = hst["session"].ToString();
                    string PostedByid = userid;
                    string Posttrmid = Terminal;
                    string PostSession = Sessionid;

                    string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                    string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                    string EditByid = "";
                    string Editdat = "01-Jan-1900";
                    string pounaction = "";
                    string aprovbyid = "";
                    string aprvtrmid = "";
                    string aprvseson = "";
                    string aprvdat = "01-jan-1900";
                    string Payto = this.txtPayto.Text.Trim();
                    string isunum = "";
                    string recndt = "01-Jan-1900";
                    string rpcode = "";
                    string chequedat = voudat;

                    string rbankname = dt1.Rows[0]["bankname"].ToString();

                    //-----------Update Transaction B Table-----------------//
                    //bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
                    //                vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");


                    bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE02",
                        vounum, voudat, refnum, srinfo, vounarration1,
                        vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid,
                        Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, rbankname, chequedat,
                        "", "");


                    if (!resultb)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                    //-----------Update Transaction A Table-----------------//
                    bool resulta = false;
                    string spclcode = "000000000000";
                    string pactcode = "";
                    string usircode = "000000000000";
                    double tqty = 0.00;



                    if (vounum.Substring(0, 2) == "JV")
                    {
                        for (int i = 0; i < dts1.Rows.Count; i++)
                        {
                            pactcode = dts1.Rows[i]["pactcode"].ToString();

                            usircode = dts1.Rows[i]["usircode"].ToString();
                            spclcode = dts1.Rows[i]["spclcode"].ToString();
                            tqty = Convert.ToDouble(dts1.Rows[i]["qty"].ToString());
                            double dramt = Convert.ToDouble(dts1.Rows[i]["dramt"].ToString());
                            cramt = Convert.ToDouble(dts1.Rows[i]["cramt"].ToString());
                            string trnRemarks = dts1.Rows[i]["urmrks"].ToString().Trim();
                            string Chk = dts1.Rows[i]["chkmv"].ToString();
                            string trnamt = Convert.ToString(dramt - cramt);


                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                                pactcode, usircode, cactcode,
                                voudat, tqty.ToString(), trnRemarks, vtcode, trnamt, spclcode, recondat, "", "", "", "");




                            // Update  MRR------------------------------------
                            bool resultma = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEEMRINF", pactcode,
                                usircode, mrno, cheqno1, vounum,
                                "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }

                            if (!resultma)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                        }


                        double tdramt = (dts1.Rows.Count == 0)
                            ? 0.00
                            : Convert.ToDouble((Convert.IsDBNull(dts1.Compute("Sum(dramt)", ""))
                                ? 0.00
                                : dts1.Compute("Sum(dramt)", "")));
                        double tcramt = (dts1.Rows.Count == 0)
                            ? 0.00
                            : Convert.ToDouble((Convert.IsDBNull(dts1.Compute("Sum(cramt)", ""))
                                ? 0.00
                                : dts1.Compute("Sum(cramt)", "")));
                        string ttrnamt = Convert.ToString((tdramt - tcramt) * -1);
                        pactcode = dts1.Rows[0]["cactcode"].ToString();

                        usircode = dts1.Rows[0]["rsircode"].ToString();
                        // usircode = "000000000000";
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                            pactcode, usircode, cactcode,
                            voudat, tqty.ToString(), "", vtcode, ttrnamt, spclcode, recondat, "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }



                    }


                    else
                    {

                        for (int i = 0; i < dts1.Rows.Count; i++)
                        {
                            pactcode = dts1.Rows[i]["pactcode"].ToString();
                            usircode = dts1.Rows[i]["usircode"].ToString();
                            spclcode = dts1.Rows[i]["spclcode"].ToString();
                            tqty = Convert.ToDouble(dts1.Rows[i]["qty"].ToString());
                            double dramt = Convert.ToDouble(dts1.Rows[i]["dramt"].ToString());
                            cramt = Convert.ToDouble(dts1.Rows[i]["cramt"].ToString());
                            string trnRemarks = dts1.Rows[i]["urmrks"].ToString().Trim();
                            string Chk = dts1.Rows[i]["chkmv"].ToString();
                            string trnamt = Convert.ToString(dramt - cramt);


                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                                pactcode, usircode, cactcode,
                                voudat, tqty.ToString(), trnRemarks, vtcode, trnamt, spclcode, recondat, "", "", "", "");

                            // Update  MRR------------------------------------
                            bool resultma = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEEMRINF", pactcode,
                                usircode, mrno, cheqno1, vounum,
                                "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }

                            if (!resultma)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                        }


                    }

                    //string comcod = this.GetCompCode();
                   
                    switch (comcod)
                    {
                        //case "3101":
                        case "3356": // intech
                           this.CollectionUpdateSMS(pactcode, usircode);
                            break;

                        default:
                       
                            break;
                    }



                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



                    if (ConstantInfo.LogStatus == true)
                    {
                        string eventtype = "Collection Update";
                        string eventdesc = "Update Collection";
                        string eventdesc2 = vounum;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]),
                            eventtype, eventdesc, eventdesc2);
                    }
                }


                //////////////////////////////// JV Part /////////////////////////////////////////////

                string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
                DataSet ds6 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, "JV",
                    "", "", "", "", "", "", "");
                string jvvounum = ds6.Tables[0].Rows[0]["couvounum"].ToString();

                string jvvouno1 = jvvounum.Substring(0, 2).ToString();
                string voutype1 = (jvvouno1 == "JV"
                    ? "Journal Voucher"
                    : (jvvouno1 == "CD"
                        ? "Cash Payment Voucher"
                        : (jvvouno1 == "BD"
                            ? "Bank Payment Voucher"
                            : (jvvouno1 == "CC"
                                ? "Cash Deposit Voucher"
                                : (jvvouno1 == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
                string cactcode1 = (jvvouno1 == "JV" ? "000000000000" : "000000000000");
                string vtcoded = "99";
                string editd = "EDIT";
                string srinfod = "";

                string refnumd = "";
                string vounarration1d = "";
                string vounarration2d = "";


                DataSet ds7 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETDISJV", mrno, "", "", "", "",
                    "", "", "", "");

                if (ds7.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        //-----------Update Transaction B Table-----------------//
                        bool resultd = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", jvvounum,
                            voudat, refnumd, srinfod, vounarration1d,
                            vounarration2d, voutype1, vtcoded, editd, userid, Terminal, Sessionid, Postdat, "", "");
                        if (!resultd)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        //-----------Update Transaction A Table-----------------//

                        //string spclcode = "000000000000";
                        //bool resulta;

                        foreach (DataRow drr in ds7.Tables[0].Rows)
                        {
                            string invno1 = drr["billid"].ToString().Trim();
                            string pactcode = drr["pactcode"].ToString().Trim();
                            string usircode = drr["rescode"].ToString().Trim();
                            double tqty = Convert.ToDouble(drr["qty"].ToString());
                            double dramt = Convert.ToDouble(drr["Dr"].ToString());
                            double cramt = Convert.ToDouble(drr["Cr"].ToString());
                            string trnRemarks = "";
                            string trnamt = Convert.ToString(dramt - cramt);

                            bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE",
                                jvvounum, pactcode, usircode, cactcode1,
                                voudat, tqty.ToString(), trnRemarks, vtcoded, trnamt, "000000000000", recondat, "", invno1,
                                "", "");


                            // Update  MRR------------------------------------
                            bool resultma = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEADJMRINF", pactcode,
                                usircode, mrno, cheqno, jvvounum,
                                "", "", "", "", "", "", "", "", "", "");

                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                        }

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('" + jvvounum.Substring(0, 2).ToString() + jvvounum.Substring(6, 2).ToString() + "-" +
                            jvvounum.Substring(8).ToString() + "');", true);


                        if (ConstantInfo.LogStatus == true)
                        {
                            string eventtype = "Collection Update";
                            string eventdesc = "Update Collection";
                            string eventdesc2 = jvvounum;
                            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]),
                                eventtype, eventdesc, eventdesc2);
                        }



                    }

                    catch (Exception ex)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }


                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   = ("mrno='" + mrno + "' and chqno='" + cheqno + "'");

                    string mrno1 = dt.Rows[i]["mrno"].ToString();
                    string chqno1 = dt.Rows[i]["chqno"].ToString();

                    if (mrno == mrno1 && cheqno == chqno1)
                    {


                        dt.Rows[i]["newvocnum"] = vounum; // vounum.Substring(0, 2).ToString() + vounum.Substring(6, 2).ToString() + "-" + vounum.Substring(8).ToString();
                        dt.Rows[i]["recondat"] = recondat;



                    }

                }
                Session["tblMrr"] = dt;

                double cramtd = Convert.ToDouble(dt.Rows[0]["cramt"]);

                if (cramtd < 0)
                {

                    bool resultd = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATECHQLIST", Cactcode,
                        RefNo, vounum, "", "",
                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }

                this.Data_Bind();
                this.CheckValue();

                //--------------------------------------------//---
                string compsms = hst["compsms"].ToString();
                if (compsms == "True")
                {
                    DataSet dsSm = CALogRecord.CheckStatus(comcod, "1703");
                    if (dsSm.Tables[0].Rows.Count == 0)
                        return;

                    if (dsSm.Tables[0].Rows[0]["sactive"].ToString() == "True")
                    {

                        string PactCode = (dt1.Rows[0]["pactcode"].ToString());
                        string Usircode = dt1.Rows[0]["usircode"].ToString();
                        string chqno = dt1.Rows[0]["chqno"].ToString();
                        string phone = dt1.Rows[0]["custphn"].ToString();
                        double amt = Convert.ToDouble(dt1.Rows[0]["cramt"].ToString());
                        //double amt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cramt)", ""))));
                        string ntype = dsSm.Tables[0].Rows[0]["gcod"].ToString();
                        string smsstatus = (dsSm.Tables[0].Rows[0]["sactive"].ToString() == "True") ? "Y" : "N";
                        string smscontent = dsSm.Tables[0].Rows[0]["smscont"].ToString().Replace("XXXXX", amt.ToString());
                        string mailstatus = (dsSm.Tables[0].Rows[0]["mactive"].ToString() == "True") ? "Y" : "N";
                        string mailcontent = dsSm.Tables[0].Rows[0]["mailcont"].ToString();
                        string mailattch = "";
                        bool IsSMSaved = CALogRecord.AddSMRecord(comcod, ((Hashtable)Session["tblLogin"]), PactCode, Usircode, vounum, voudat, ntype, smsstatus, smscontent.Replace("YYYYY", chqno), mailstatus,
                                mailcontent, mailattch, phone, "");
                    }

                }
                //------------------------------------//////
            }






            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }

        private void CollectionUpdateSMS(string pactcode, string usercode)       
        {
            string comcod = this.GetCompCode();

            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETOUTSTANDAMT", pactcode, usercode,
                    "", "", "", "", "", "", "");
            if(ds==null)
            {
                return;
            }
            string Data1 = "1703%"; // Update collection 
            string Data2 = "";
            DataSet ds1 = this._processAccessMsgdb.GetTransInfo(comcod, "ASTREALERPMSGDB.dbo.SP_ENTRY_SMS_MAIL_INFO", "GETSMSMAILTEMPLATE", Data1, Data2, "", "", "", "", "", "", "");
          
            string cutname = ds.Tables[0].Rows[0]["custname"].ToString()==""?"": ds.Tables[0].Rows[0]["custname"].ToString();    
            string custphone = ds.Tables[0].Rows[0]["custphone"].ToString()==""?"": ds.Tables[0].Rows[0]["custphone"].ToString();    
            string trcvamt = ds.Tables[0].Rows[0]["trecvamt"].ToString()==""?"": Convert.ToDouble(ds.Tables[0].Rows[0]["trecvamt"]).ToString("#,##0.00;(#,##0.00) "); ;    
            string payment = ds.Tables[0].Rows[0]["payamt"].ToString()==""?"": Convert.ToDouble(ds.Tables[0].Rows[0]["payamt"]).ToString("#,##0.00;(#,##0.00) ");
            string paymentdate = ds.Tables[0].Rows[0]["paydate"].ToString()==""?"": Convert.ToDateTime(ds.Tables[0].Rows[0]["paydate"]).ToString("dd-MMM-yyyy");    
            string dues = ds.Tables[0].Rows[0]["duesamt"].ToString()==""?"": Convert.ToDouble(ds.Tables[0].Rows[0]["duesamt"]).ToString("#,##0.00;(#,##0.00) ");

            string tempeng = ds1.Tables[0].Rows[0]["smscont"].ToString();
            tempeng = tempeng.Replace("[name]", cutname);
            tempeng = tempeng.Replace("[date]", paymentdate);
            tempeng = tempeng.Replace("[payamt]", payment);
            tempeng = tempeng.Replace("[duesamt]", dues);

            string  smtext = tempeng;

            SendSmsProcess sms = new SendSmsProcess();

            bool resultsms = sms.SendSMSClient(comcod, smtext, custphone);
            if (resultsms == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Message sent Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Message sent Failed.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ibtnSearchChequeno_Click(object sender, EventArgs e)
        {
            this.lnkOk0_Click(null, null);
        }

        protected void dgv1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (DataTable)Session["tblMrr"];

                string bankcode = dt.Rows[0]["cactcode"].ToString();

                double cramt = Convert.ToDouble(dt.Rows[0]["cramt"]);


                if (cramt < 0)
                {


                    DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlChq");

                    string comcod = this.GetCompCode();
                    string mSrchTxt = bankcode + "%";

                    //string Calltype = this.GetResSupplier();
                    DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETCHQNO", mSrchTxt, "", "", "", "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    if (ds2.Tables[0].Rows.Count == 0)
                        return;
                    ddl2.DataTextField = "chequeno";
                    ddl2.DataValueField = "chequeno";
                    ddl2.DataSource = ds2.Tables[0];
                    ddl2.DataBind();
                }
                HyperLink hlnkPrintVoucher = (HyperLink)e.Row.FindControl("hlnkVoucherPrint");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "newvocnum")).ToString();

                if (vounum.Length > 0)
                {
                    hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;
                }
                else
                {
                    hlnkPrintVoucher.Visible = false;
                }
            }
        }

    }
}


