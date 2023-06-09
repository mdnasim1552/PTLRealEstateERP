﻿using System;
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
    public partial class AccOverHeadJournal : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "OverHead Allocation";
                this.lbldate.Text = this.Request.QueryString["Date2"].ToString();
                CreateTable();
                this.GetOverheadAllocation();
                //this.Master.Page.Title = "OverHead Allocation";
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
            tblt01.Columns.Add("Dr", Type.GetType("System.Double"));
            tblt01.Columns.Add("Cr", Type.GetType("System.Double"));
            ViewState["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void calculation()
        {
            DataTable dt2 = (DataTable)ViewState["tblt01"];
            if (dt2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(Dr)", "")) ?
                         0.00 : dt2.Compute("Sum(Dr)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(Cr)", "")) ?
                        0.00 : dt2.Compute("Sum(Cr)", ""))), 2);
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



        }





        private void GetVouCherNumber()
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

                if (txtopndate > Convert.ToDateTime(this.lbldate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.lbldate.Text.Substring(0, 11).Trim();
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
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            if (Math.Round(accData.ToDramt) != Math.Round(accData.ToCramt))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must be Equal Credit Amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
            //Existing   Purchase No  




            this.GetVouCherNumber();
            string voudat = this.lbldate.Text.Substring(0, 11);
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

                    //   Convert.ToDouble(ASTUtility.StrPosOrNagative("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());

                    //double Dramt = Convert.ToDouble( ASTUtility.StrPosOrNagative("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                    //double Cramt = Convert.ToDouble( ASTUtility.StrPosOrNagative("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));

                    double trnamt = (Dramt - Cramt);

                    if (trnamt != 0)
                    {
                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                                actcode, rescode, cactcode, voudat, trnqty, "", vtcode, trnamt.ToString(), spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                    }
                }

                bool resultD = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSORUPDEPVOUOVERHEAD", vounum,
                            voudat, "", "", "", "", "", "", "", "", "", "", "", "", "");

                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Purchase Update";
                    string eventdesc = "Update Purchase Bill";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

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
                string curvoudat = this.lbldate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void GetOverheadAllocation()


        {
            string comcod = this.GetCompCode();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();

            //string method = this.Request.QueryString["Method"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_MIS04", "GETOVERHEADVOU", frmdate,
                          todate, "", "", "", "", "", "", "");
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

                //if (Convert.ToDouble(dt1.Rows[i]["billqty"]) > 0)
                //{
                //    dgTrnrate =  Convert.ToDouble(dt1.Rows[i]["Dr"])/Convert.ToDouble(dt1.Rows[i]["billqty"]) ;
                //}

                double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["dr"]);
                double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["cr"]);

                DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'");
                if (dr2.Length > 0)
                {

                    return;

                }

                DataRow dr1 = tblt01.NewRow();
                dr1["actcode"] = dgAccCode;
                dr1["subcode"] = dgResCode;
                dr1["spclcode"] = dgSpclCode;
                dr1["actdesc"] = dgAccDesc;
                dr1["subdesc"] = dgResDesc;
                dr1["spcldesc"] = dgSpclDesc;
                dr1["Dr"] = dgTrnDrAmt;
                dr1["Cr"] = dgTrnCrAmt;
                tblt01.Rows.Add(dr1);
            }
            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();

        }


        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            dgv2.DataSource = tbl1;
            dgv2.DataBind();
            // this.GridColoumnVisible();
            calculation();


        }

        private void GridColoumnVisible()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {

                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                string mRSIRCODE = tbl1.Rows[TblRowIndex2]["subcode"].ToString();
                if (ASTUtility.Left(mRSIRCODE, 2) != "97")
                    ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).ReadOnly = true;
            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {


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
                string actcode = ((Label)this.dgv2.Rows[j].FindControl("lblAccCod")).Text.Trim();
                // double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                // todramt = todramt + dramt;
                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                //if (actcode == "550188880001")
                //{
                //    dt1.Rows[TblRowIndex2]["Dr"] = tocramt;
                //    // todramt = 0; tocramt = 0;
                //    continue;
                //}

                double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                todramt = todramt + dramt;

                dt1.Rows[TblRowIndex2]["Dr"] = dramt;

            }
            ViewState["tblt01"] = dt1;
            this.Data_Bind();



        }

    }
}
