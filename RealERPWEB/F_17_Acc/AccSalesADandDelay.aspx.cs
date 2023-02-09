using System;
using System.Collections;
using System.Collections.Generic;
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
using RealEntity;
using RealERPWEB.Service;

namespace RealERPWEB.F_17_Acc
{

    public partial class AccSalesADandDelay : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        UserService userSer = new UserService();
        public static double TAmount;
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Modification Update";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                CreateTable();
                //this.Master.Page.Title = " additional, deduction & delay charge information";
            }

        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("adno", Type.GetType("System.String"));
            ViewState["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetMRNo()
        {

            ViewState.Remove("tbladdno");
            string comcod = this.GetCompCode();



            string Mrno = "%" + ((this.Request.QueryString["genno"]) ?? this.txtMRno.Text.Trim()) + "%";
            string date = this.txtdate.Text.Substring(0, 11);

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETMRNO", Mrno, date, "", "", "", "", "", "", "");
            ViewState["tbladdno"] = ds1.Tables[0];
            this.ddlMRList.Items.Clear();
            this.ddlMRList.DataTextField = "textfield";
            this.ddlMRList.DataValueField = "valuefield";
            this.ddlMRList.DataSource = ds1.Tables[0];
            this.ddlMRList.DataBind();
        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)ViewState["tblt01"];
            if (dt2.Rows.Count == 0)
                return;

            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                          0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFDrAmt")).Text = (accData.ToDramt).ToString("#,##0;(#,##0); - ");
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFCrAmt")).Text = (accData.ToCramt).ToString("#,##0;(#,##0); - ");



        }

        private void GetVouNum()
        {
            try
            {

                string comcod = this.GetCompCode();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }
        }
        //protected void ibtnvounu_Click(object sender, ImageClickEventArgs e)
        //{

        //    try
        //    {

        //        string comcod =this.GetCompCode();

        //        DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
        //        if (ds2.Tables[0].Rows.Count == 0)
        //        {
        //            return;

        //        }

        //        DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

        //        if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
        //        {
        //         ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
        //         ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
        //            return;

        //        }

        //        string VNo3 = "JV";
        //        string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
        //        DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
        //        DataTable dt4 = ds4.Tables[0];
        //        string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
        //        this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
        //        this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

        //    }
        //    catch (Exception ex) 
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

        //    }
        //}


        protected void imgSearchMrno_Click(object sender, EventArgs e)
        {
            this.GetMRNo();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                // this.ibtnvounu.Visible = true;
                this.GetVouNum();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            this.pnlAcHead.Visible = false;
            ViewState.Remove("tblt01");
            this.GetMRNo();
            this.CreateTable();


            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            //this.ibtnvounu.Visible = false;
            this.lnkFinalUpdate.Enabled = true;
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
        }




        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            if (this.txtcurrentvou.Text.Trim() == "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please insert Voucher no !!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            if (Math.Round(accData.ToDramt) != Math.Round(accData.ToCramt))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must be Equal Credit Amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            //if (this.ibtnvounu.Visible)
            //    this.ibtnvounu_Click(null, null);

            this.GetVouNum();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
            //Existing   Purchase No  

            for (int i = 0; i < dgv2.Rows.Count; i++)
            {

                string adno = ((Label)this.dgv2.Rows[i].FindControl("lbladno")).Text.Trim();
                //string mrno = ((Label)this.dgv2.Rows[i].FindControl("lblmrandchqno")).Text.Trim().Substring(0,9);
                //string chqno = ((Label)this.dgv2.Rows[i].FindControl("lblmrandchqno")).Text.Trim().Substring(9);
                DataSet ds4;
                if (i == 0)
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT ", "EXISTINGMRNO", adno, "", "", "", "", "", "", "", "");

                else if (((Label)this.dgv2.Rows[i - 1].FindControl("lbladno")).Text.Trim() == adno)
                    continue;

                else
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT ", "EXISTINGMRNO", adno, "", "", "", "", "", "", "", "");


                if (ds4.Tables[0].Rows[0]["schvounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this ADW No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }


            string voudat = this.txtdate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = "";
            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//


                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = "0";
                    double Dramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = "";
                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                            actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }


                    if (ASTUtility.Left(actcode, 2) == "18")
                    {

                        string adno = ((Label)this.dgv2.Rows[i].FindControl("lbladno")).Text.Trim();
                        //string chqno = ((Label)this.dgv2.Rows[i].FindControl("lblmrandchqno")).Text.Trim().Substring(9);
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "UPDATESCHVOUNUM",
                                adno, "", vounum, "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                    }
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales Journal";
                    string eventdesc = "Update Journal";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                //this.txtcurrentvou.Enabled = false;
                //this.txtCurrntlast6.Enabled = false;

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }





        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.ddlPrivousVou.Items.Count > 0 && this.lnkOk.Text == "Ok")
                //    this.lnkOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtdate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";




                //string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                // DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                // if (_ReportDataSet == null)
                //     return;
                // DataTable dt = _ReportDataSet.Tables[0];
                // if (dt.Rows.Count == 0)
                //     return;
                // double dramt, cramt;
                // dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                // cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                // if (dramt > 0 && cramt > 0)
                // {
                //     TAmount = cramt;

                // }
                // else if (dramt > 0 && cramt <= 0)
                // {
                //     TAmount = dramt;
                // }
                // else
                // {
                //     TAmount = cramt;
                // }

                // DataTable dt1 = _ReportDataSet.Tables[1];
                // string Vounum = dt1.Rows[0]["vounum"].ToString();
                // string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                // string refnum = dt1.Rows[0]["refnum"].ToString();
                // string voutype = dt1.Rows[0]["voutyp"].ToString();
                // string venar = dt1.Rows[0]["venar"].ToString();
                // string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                //// string Type = this.CompanyPrintVou();
                // ReportDocument rptinfo = new ReportDocument();
                // rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                // //if (Type == "VocherPrint")
                // //{
                // //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                // //}
                // //else if (Type == "VocherPrint1")
                // //{
                // //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                // //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                // //    txtPosteddate1.Text = "Entry Date: " + Posteddat;

                // //}
                // //else if (Type == "VocherPrint2")
                // //{
                // //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                // //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate2"] as TextObject;
                // //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                // //}
                // //else
                // //{
                // //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                // //    TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate3"] as TextObject;
                // //    txtPosteddate3.Text = "Entry Date: " + Posteddat;
                // //}


                // rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                // TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                // txtCompanyName.Text = comnam;
                // TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                // txtcAdd.Text = comadd;
                // TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                // vounum1.Text = "Voucher No.: " + vounum;
                // TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                // date.Text = "Voucher Date: " + voudat;
                // TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                // Refnum.Text = "Cheque/Ref. No.: " + refnum;
                // TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                // rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                // TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                // voutype1.Text = voutype;
                // TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                // naration.Text = "Narration: " + venar;

                // //TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                // //txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                // TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                // rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                // TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                // if (ConstantInfo.LogStatus == true)
                // {
                //     string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                //     string eventdesc = "Print Voucher";
                //     string eventdesc2 = vounum;
                //     bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                // }
                // //string comcod = this.GetComeCode();
                // //string comcod = hst["comcod"].ToString();
                // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                // rptinfo.SetParameterValue("ComLogo", ComLogo);
                // Session["Report1"] = rptinfo;
                // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private string CompCallType()
        {

            string comcod = this.GetCompCode();
            string Calltype = "";

            switch (comcod)
            {

                //case "3101":
                case "3330":
                    Calltype = "GETBRSALESJOURNAL";
                    break;

                default:
                    Calltype = "GETSALESJOURNAL";
                    break;




            }

            return Calltype;

        }

        protected void lbtnSelectMR_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string adno = this.ddlMRList.SelectedValue.ToString();
            string CompCallType = this.CompCallType();
            string actcode = this.ddlAccHead.SelectedValue.ToString();
            string rescode = this.ddlresource.SelectedValue.ToString();

            //string chqno = this.ddlMRList.SelectedValue.ToString().Substring(9);
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", CompCallType, adno,
                          actcode, rescode, "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            DataTable tblt01 = (DataTable)ViewState["tblt01"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                string dgResCode = dt1.Rows[i]["rescode"].ToString();
                string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                string dgResDesc = dt1.Rows[i]["resdesc"].ToString();
                string dgSpclDesc = dt1.Rows[i]["spcfdesc"].ToString();
                double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["Cr"]);
                string dgadno = dt1.Rows[i]["adno"].ToString();

                DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'");
                if (dr2.Length > 0)
                {
                    if (ASTUtility.Left(dgAccCode, 2) == "18")
                        return;
                    else
                        dr2[0]["trncram"] = Convert.ToDouble(dr2[0]["trncram"]) + dgTrnCrAmt;
                }


                else
                {
                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dgAccCode;
                    dr1["subcode"] = dgResCode;
                    dr1["spclcode"] = dgSpclCode;
                    dr1["actdesc"] = dgAccDesc;
                    dr1["subdesc"] = dgResDesc;
                    dr1["spcldesc"] = dgSpclDesc;
                    dr1["trndram"] = dgTrnDrAmt;
                    dr1["trncram"] = dgTrnCrAmt;
                    dr1["adno"] = dgadno;
                    tblt01.Rows.Add(dr1);
                }
            }
            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();
        }


        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            dgv2.DataSource = tbl1;
            dgv2.DataBind();
            calculation();


        }



        private DataTable HiddenSameData(DataTable dt1)
        {

            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                }

            }

            return dt1;
        }





        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblt01"];
            double todramt = 0, tocramt = 0;
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {
                string Supplier = ((Label)this.dgv2.Rows[j].FindControl("lblResCod")).Text.Trim();
                double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                todramt = todramt + dramt;
                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                if (ASTUtility.Left(Supplier, 2) == "99")
                {
                    dt1.Rows[TblRowIndex2]["trncram"] = todramt - tocramt;
                    todramt = 0; tocramt = 0;
                    continue;
                }

                double cramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
                tocramt = tocramt + cramt;

                dt1.Rows[TblRowIndex2]["trncram"] = cramt;

            }
            ViewState["tblt01"] = dt1;
            this.Data_Bind();



        }


        protected void ddlAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {

            string search1 = this.ddlAccHead.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["tblachead"];
            DataView dv = new DataView();
            var results = (from srchrow in dt.AsEnumerable()
                           where srchrow.Field<string>("actcode").Equals(search1)
                           select srchrow);
            dv = results.AsDataView();


            if (dv.ToTable().Rows[0]["actelev"].ToString() == "2")
            {
                this.lblreshead.Visible = true;
                this.txtsrchres.Visible = true;
                this.lbtnreshead.Visible = true;
                this.ddlresource.Visible = true;
                this.GetResCode();
            }
            else
            {
                this.lblreshead.Visible = false;
                this.txtsrchres.Visible = false;
                this.lbtnreshead.Visible = false;
                this.ddlresource.Visible = false;
                this.ddlresource.Items.Clear();

            }
        }


        private void GetResCode()
        {


            try
            {

                DataTable dt = (DataTable)Session["tblachead"];
                string actcode = this.ddlAccHead.SelectedValue.ToString();
                string filter1 = "%" + this.txtsrchres.Text.Trim() + "%";
                string SearchInfo = "";


                DataView dv = new DataView();
                var results = (from srchrow in dt.AsEnumerable()
                               where srchrow.Field<string>("actcode").Equals(actcode)
                               select srchrow);
                dv = results.AsDataView();


                string type = dv.ToTable().Rows[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {

                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {


                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHead(actcode, filter1, SearchInfo);


                this.ddlresource.DataSource = lst;
                this.ddlresource.DataTextField = "resdesc1";
                this.ddlresource.DataValueField = "rescode";
                this.ddlresource.DataBind();
                string rescode = "019300101001";
                this.ddlresource.SelectedValue = rescode;
            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
            }


        }
        private void GetAccHead()
        {



            Session.Remove("tblachead");
            string comcod = this.GetCompCode();
            string SearchBank = "%" + this.txtSerchAccHead.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETACTCODE", SearchBank, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataTextField = "actdesc";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataSource = ds1;
            this.ddlAccHead.DataBind();
            Session["tblachead"] = ds1.Tables[0];

            DataTable dt = ((DataTable)ViewState["tbladdno"]).Copy();
            string addno = this.ddlMRList.SelectedValue.ToString();
            DataRow[] dr1 = dt.Select("adno='" + addno + "'");
            if (dr1.Length > 0)
            {
                string actcode = "16" + dr1[0]["pactcode"].ToString().Substring(2);
                this.ddlAccHead.SelectedValue = actcode;

            }




            this.GetResCode();

        }
        protected void ibtnSrchAcchead_Click(object sender, EventArgs e)
        {
            this.GetAccHead();
        }
        protected void lbtnreshead_Click(object sender, EventArgs e)
        {
            this.GetResCode();
        }
        protected void Adjusted_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlAcHead.Visible = this.chkAdustCOde.Checked;

            if (!this.chkAdustCOde.Checked)
            {
                this.ddlAccHead.Items.Clear();
                this.ddlresource.Items.Clear();


            }
            else
            {

                this.GetAccHead();
            }

        }
    }
}
