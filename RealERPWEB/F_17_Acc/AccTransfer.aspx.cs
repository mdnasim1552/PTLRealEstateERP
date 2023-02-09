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


    public partial class AccTransfer : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = " Transfer Journal";
                //this.Master.Page.Title = " Transfer Journal";
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                //this.LoadTrnsCombo();
                CreateTable();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CompanyPost();
            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void CompanyPost()
        {


            string comcod = this.GetCompCode();

            switch (comcod)
            {

                case "3332":
                    //   case "3101":
                    this.chkpost.Checked = true;
                    break;


                default:
                    this.chkpost.Checked = false;
                    break;
            }


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
            tblt01.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("trnno", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
        }


        private void LoadTrnsCombo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string SerchTrnNo = (this.Request.QueryString["genno"].ToString()).Length == 0 ? ("%" + this.txtSrcTrnNo.Text.Trim() + "%") : (this.Request.QueryString["genno"].ToString() + "%");

            //string SerchTrnNo = "%"+this.txtSrcTrnNo.Text.Trim()+"%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETTRANSFERNO", SerchTrnNo, "", "", "", "", "", "", "", "");
            this.ddlTrnsList.Items.Clear();
            this.ddlTrnsList.DataTextField = "textfield";
            this.ddlTrnsList.DataValueField = "trnno";
            this.ddlTrnsList.DataSource = ds1.Tables[0];
            this.ddlTrnsList.DataBind();
        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)Session["tblt01"];
            if (dt2.Rows.Count == 0)
                return;


            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                       0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");





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

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
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
            string Payto = "";
            string isunum = "";
            string recndt = "01-Jan-1900";
            string rpcode = "";



            for (int i = 0; i < dgv2.Rows.Count; i++)
            {
                string Trnno = ((Label)this.dgv2.Rows[i].FindControl("lblTrnno")).Text.Trim();
                DataSet ds5;
                if (i == 0)
                    ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "CHKVOUCHER", Trnno, "", "", "", "", "", "", "", "");

                else if (((Label)this.dgv2.Rows[i - 1].FindControl("lblTrnno")).Text.Trim() == Trnno)
                    continue;

                else
                    ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "CHKVOUCHER", Trnno, "", "", "", "", "", "", "", "");

                if (ds5.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this Bill No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }


            this.GetVouCherNumber();

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

                string CallType = (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                                  vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "", "");

                //bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                //        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//
                string trnno2 = "XXXXXXXXXXXXXX";
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    string trnno = ((Label)this.dgv2.Rows[i].FindControl("lblTrnno")).Text.Trim();


                    bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, cactcode,
                          voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, "", userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                    //bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                    //        actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");



                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                    if (trnno2 != trnno)
                    {
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEPRJTRNSFERNO",
                                trnno, vounum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        trnno2 = trnno;
                    }
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Transfer Journal";
                    string eventdesc = "Update Journal";
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
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";


                //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHERPUR",
                //                         vounum, "", "", "", "", "", "", "", "");

                //ReportDocument rptinfo = new  RealERPRPT.R_17_Acc.rptPrintVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Transfer Journal";
                //    string eventdesc = "Print Journal";
                //    string eventdesc2 = vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}

                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void lbtnSelectTrns_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string trnno = this.ddlTrnsList.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCTRANSFERINFO", trnno,
                          "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];

            DataTable tblt01 = (DataTable)Session["tblt01"];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {


                DataRow dr1 = tblt01.NewRow();
                dr1["actcode"] = dt1.Rows[i]["actcode"].ToString(); ;
                dr1["subcode"] = dt1.Rows[i]["rescode"].ToString(); ;
                dr1["spclcode"] = dt1.Rows[i]["spcode"].ToString(); ;
                dr1["actdesc"] = dt1.Rows[i]["actdesc"].ToString(); // dgAccCode + "-" + dgAccDesc;
                dr1["subdesc"] = dt1.Rows[i]["resdesc"].ToString(); ; // dgResCode + "-" + dgResDesc;
                dr1["spcldesc"] = dt1.Rows[i]["spcfdesc"].ToString(); ;
                dr1["trnqty"] = Convert.ToDouble(dt1.Rows[i]["tqty"]);
                dr1["trnrate"] = Convert.ToDouble(dt1.Rows[i]["trate"]);
                dr1["trndram"] = Convert.ToDouble(dt1.Rows[i]["dr"]);
                dr1["trncram"] = Convert.ToDouble(dt1.Rows[i]["cr"]);
                dr1["trnrmrk"] = dt1.Rows[i]["trnno"].ToString();
                dr1["trnno"] = dt1.Rows[i]["trnno"].ToString();
                tblt01.Rows.Add(dr1);
            }
            if (tblt01.Rows.Count == 0)
                return;
            Session["tblt01"] = this.HiddenSameData(tblt01);
            dgv2.DataSource = (DataTable)Session["tblt01"];
            dgv2.DataBind();
            calculation();

            this.txtCurrntlast6.ReadOnly = false;
            this.pnlTrans.Visible = true;
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


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.LoadTrnsCombo();
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlTrans.Visible = true;
                this.PnlNarration.Visible = true;
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.PnlNarration.Visible = false;
            Session.Remove("tblt01");
            this.CreateTable();
            this.LoadTrnsCombo();

            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.lnkFinalUpdate.Enabled = true;
            this.pnlTrans.Visible = false;
        }

        protected void imgbtnTrnNo_Click(object sender, EventArgs e)
        {
            this.LoadTrnsCombo();
        }

    }
}
