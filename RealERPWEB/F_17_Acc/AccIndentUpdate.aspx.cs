﻿using System;
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
using RealEntity;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
using RealERPWEB.Service;

namespace RealERPWEB.F_17_Acc
{
    public partial class AccIndentUpdate : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        UserService userSer = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);              
            
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                this.LoadAccCombo();
                this.LoadBillCombo();
                CreateTable();

            }




        }

        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("rsircode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rsirdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt01.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("memono", Type.GetType("System.String"));
            tblt01.Columns.Add("mrnar", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void LoadBillCombo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Serchlsdno = "%" + this.txtSrclsdno.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETINDENTISSUELIST", Serchlsdno, "", "", "", "", "", "", "", "");
            this.ddlBillList.Items.Clear();
            this.ddlBillList.DataTextField = "textfield";
            this.ddlBillList.DataValueField = "issuno";
            this.ddlBillList.DataSource = ds1.Tables[0];
            this.ddlBillList.DataBind();
            string genno = this.Request.QueryString["genno"].ToString();
            if (genno.Length > 0)
            {
                this.ddlBillList.SelectedValue = genno;
                this.txtdate.Text = this.Request.QueryString["Date1"].ToString();
            }
        }
        private void LoadAccCombo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Serchlsdno = "%" + this.txtAccSch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", Serchlsdno, "", "", "", "", "", "", "", "");
            this.ddlActCode.Items.Clear();
            this.ddlActCode.DataTextField = "actdesc";
            this.ddlActCode.DataValueField = "actcode";
            this.ddlActCode.DataSource = ds1.Tables[0];
            this.ddlActCode.DataBind(); // DataTableToList<RealEntity.C_99_AllInterface.RptReqSts>();

            var list = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>();//DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>();

            ViewState["HeadAcc1"] = list;
            this.ddlActCode_SelectedIndexChanged(null, null);
        }
        protected void ddlActCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlActCode.BackColor = System.Drawing.Color.Pink;

            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst = (List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>)ViewState["HeadAcc1"];
            string search1 = this.ddlActCode.SelectedValue.ToString().Trim();
            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst1 = lst.FindAll((p => p.actcode == search1));

            if (lst1[0].actelev.ToString() == "2")
            {
                this.lbldetails.Visible = true;
                this.txtserchReCode.Visible = true;
                this.lnkRescode.Visible = true;
                this.ddlresuorcecode.Visible = true;


                string actcode = this.ddlActCode.SelectedValue.Substring(0, 2);
                this.GetResCode();
            }
            else
            {
                this.lbldetails.Visible = false;
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.ddlresuorcecode.Items.Clear();
            }
        }
        private void GetResCode()
        {


            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string actcode = this.ddlActCode.SelectedValue.ToString();
                string filter1 = "%" + this.txtserchReCode.Text.Trim() + "%";

                string oldRescode = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedValue.ToString();


                string SearchInfo = "";
                //var lst = (List<MFGOBJ.C_17_Acc.EClassDB_BO.EClassAcountsHead>)ViewState["tblsalinc"];

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lstacc = (List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>)ViewState["HeadAcc1"];

                string search1 = this.ddlActCode.SelectedValue.ToString().Trim();

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lstacc1 = lstacc.FindAll((p => p.actcode == search1));


                string type = lstacc1[0].acttype.ToString().Trim();
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
                ViewState["HeadRsc1"] = lst;

                this.ddlresuorcecode.DataSource = lst;
                this.ddlresuorcecode.DataTextField = "resdesc1";
                this.ddlresuorcecode.DataValueField = "rescode";
                this.ddlresuorcecode.DataBind();
                List < RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst1 = lst.FindAll((p => p.rescode == oldRescode));
                if (lst1.Count > 0)
                {
                    this.ddlresuorcecode.SelectedValue = oldRescode;


                }




                this.txtserchReCode.Text = "";
                string seaRes = this.ddlresuorcecode.SelectedValue.ToString().Trim();
                List < RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst2 = lst.FindAll((p => p.rescode == seaRes));
                if (lst2.Count == 0)
                    return;


            }


            catch (Exception ex)
            {

                this.lblmsg.Text = ex.Message;
            }


        }
        private void calculation(DataTable d2)
        {
            //DataTable dt2 = (DataTable)Session["tblt01"];
            if (d2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Ceiling(Convert.ToDouble((Convert.IsDBNull(d2.Compute("Sum(trndram)", "")) ?
                          0.00 : d2.Compute("Sum(trndram)", ""))));
            accData.ToCramt = Math.Ceiling(Convert.ToDouble((Convert.IsDBNull(d2.Compute("Sum(trncram)", "")) ?
                          0.00 : d2.Compute("Sum(trncram)", ""))));
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            Session.Remove("tblt01");
            this.CreateTable();
            this.LoadBillCombo();
            this.lblmsg.Text = "";
            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.lnkFinalUpdate.Enabled = true;
            // this.Panel1.Visible = false;
        }


        protected void imgbtnlsdno_Click(object sender, EventArgs e)
        {
            this.LoadBillCombo();
        }
        protected void imgbtnAcc_Click(object sender, EventArgs e)
        {
            this.LoadAccCombo();
        }

        protected void lbtnSelectTrns_Click(object sender, EventArgs e)
        {
            //Session.Remove("tblt01");
            DataTable tblt01 = (DataTable)Session["tblt01"];
            string actcode = this.ddlActCode.SelectedValue.ToString();
            string mrslid = this.ddlBillList.SelectedValue.ToString();
            string rescode = (this.ddlresuorcecode.SelectedValue.Length == 0) ? "000000000000" : this.ddlresuorcecode.SelectedValue.ToString();

            if (this.chkCharging.Checked == false)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();               
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETINDENTISSUE", mrslid,
                              actcode, rescode, "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
               


                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                    string dgResdesc = dt1.Rows[i]["rsirdesc"].ToString();
                    string dgResCode = dt1.Rows[i]["rsircode"].ToString();
                    string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                    string dgSpclCode = dt1.Rows[i]["spclcode"].ToString();
                    string dgSpclDesc = dt1.Rows[i]["spcldesc"].ToString();
                    double dgTrnQty = Convert.ToDouble(dt1.Rows[i]["trnqty"]);
                   

                    double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["trndram"]);
                    double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["trncram"]);
                    string dgMemono = dt1.Rows[i]["memono"].ToString();
                    string dgmrnar = dt1.Rows[i]["mrnar"].ToString();

                    DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and rsircode='" + dgResCode + "'");
                    if (dr2.Length > 0)
                    {
                        return;
                    }

                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dgAccCode;
                    dr1["rsircode"] = dgResCode;
                    dr1["actdesc"] = dgAccDesc;
                    dr1["rsirdesc"] = dgResdesc;
                    dr1["spclcode"] = dgSpclCode;
                    dr1["spcldesc"] = dgSpclDesc;
                    dr1["trnqty"] = dgTrnQty;
                    dr1["trndram"] = dgTrnDrAmt;
                    dr1["trncram"] = dgTrnCrAmt;
                    dr1["memono"] = dgMemono;
                    dr1["mrnar"] = dgmrnar;
                    tblt01.Rows.Add(dr1);
                }



                this.txtRefNum.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
                this.txtNarration.Text = ds1.Tables[1].Rows[0]["remarks"].ToString();
            }
            else
            {


                DataRow[] dr2 = tblt01.Select("actcode='" + actcode + "'  and rsircode='" + rescode + "'");
                if (dr2.Length == 0)
                {

                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = actcode;
                    dr1["rsircode"] = rescode;
                    dr1["actdesc"] = this.ddlActCode.SelectedItem.Text.Trim(); 
                    dr1["rsirdesc"] = (this.ddlresuorcecode.SelectedValue.Length == 0)?"": this.ddlresuorcecode.SelectedItem.Text.Trim();
                    dr1["spclcode"] = "000000000000";
                    dr1["spcldesc"] = "";
                    dr1["trnqty"] = 0;
                    dr1["trndram"] = 0;
                    dr1["trncram"] = 0;
                    dr1["memono"] = mrslid;
                    dr1["mrnar"] = tblt01.Rows[0]["mrnar"].ToString();
                    tblt01.Rows.Add(dr1);
                }



            }

            DataTable dt2 = (DataTable)Session["tblt01"];
            DataTable d2 = HiddenSameData(dt2);
            dgv2.DataSource = d2;
            dgv2.DataBind();
            calculation(d2);
           
            this.txtCurrntlast6.ReadOnly = false;
          
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
            this.Session_tblacc_Update();

            
        }
        protected void Session_tblacc_Update()
        {

            DataTable dt1 = (DataTable)Session["tblt01"];
            //DataTable dt1 = (DataTable)Session["tblt01"];
            


            for (int i = 0; i < this.dgv2.Rows.Count; i++)
            {
             

                dt1.Rows[i]["trndram"] = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("lblgvDrAmt")).Text.Trim()); ;
                dt1.Rows[i]["trncram"]= Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvCrAmt")).Text.Trim());;
              
            }
            DataTable dt2 = (DataTable)Session["tblt01"];
            DataTable d2 = HiddenSameData(dt2);
            dgv2.DataSource = d2;
            dgv2.DataBind();
            calculation(d2);

        }


        private void GetVouCherNumber()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;

            }

            DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

            if (txtopndate >= Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
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
        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = true;

            //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
            //    Response.Redirect("~/AcceessError.aspx");
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

        
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg.Text = "You have no permission";
            //    return;
            //}
            string voudat = this.txtdate.Text.Substring(0, 11);
            DateTime Bdate = this.GetBackDate();
            bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
            if (!dcon)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                return;
            }
            if (Math.Round(accData.ToDramt) != Math.Round(accData.ToCramt))
            {
                this.lblmsg.Text = "Debit Amount must be Equal Credit Amount";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            this.GetVouCherNumber();

            //string voudat = this.txtdate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Indent Journal Voucher";
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
                    this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                //-----------Update Transaction A Table-----------------//
                string memono2 = "XXXXXXXXXXXXXX";

                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    //string centrid = ((Label)this.dgv2.Rows[i].FindControl("lblcentrid")).Text.Trim();
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("lblgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);

                    string memono = ((Label)this.dgv2.Rows[i].FindControl("lblMemono")).Text.Trim();
                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                            actcode, rescode, cactcode, voudat, trnqty, memono, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                        this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }

                    if (memono2 != memono)
                    {
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEINDENTISS", actcode, memono, vounum, "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            this.lblmsg.Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }
                        memono2 = memono;
                    }
                }
                this.lblmsg.Text = "Update Successfully.";
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales Adjustment Journal";
                    string eventdesc = "Update Journal";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }

        }






    }
}