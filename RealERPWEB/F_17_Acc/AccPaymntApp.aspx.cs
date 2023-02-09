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
    public partial class AccPaymntApp : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            lnkFinalUpdate.Attributes.Add("onClick",
           " javascript:return confirm('You sure you want to Save the record?');");
            this.TableCreate();
            this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy (ddd)");
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetPriviousVoucher()
        {

            string comcod = this.GetComeCode();
            string date = Convert.ToDateTime(this.txtEntryDate.Text.Substring(0, 11)).ToString("dd-MMM-yyyy");
            DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "GETPRIVOUSPRAPP", date, "", "", "", "", "", "", "", "");

            this.ddlPrivousVou.DataSource = ds5.Tables[0];
            this.ddlPrivousVou.DataTextField = "pronum1";
            this.ddlPrivousVou.DataValueField = "papnum";
            this.ddlPrivousVou.DataBind();
        }
        private void TableCreate()
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
            tblt01.Columns.Add("paidto", Type.GetType("System.String"));
            tblt01.Columns.Add("purpose", Type.GetType("System.String"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
            DataTable tblt02 = new DataTable();
            tblt02.Columns.Add("actcode", Type.GetType("System.String"));
            tblt02.Columns.Add("subcode", Type.GetType("System.String"));
            tblt02.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt02.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt02.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt02.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt02.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt02.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt02.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt02.Columns.Add("paidto", Type.GetType("System.String"));
            tblt02.Columns.Add("purpose", Type.GetType("System.String"));
            tblt02.Columns.Add("trnrmrk", Type.GetType("System.String"));
            Session["tblt02"] = tblt02;
            //actcode,subcode,spclcode,actdesc,subdesc,spcldesc,trnqty,trnrate,trndram,trncram,trnrmrk
        }

        //protected void lnkAcccode_Click(object sender, EventArgs e)
        //{

        //    string comcod = this.GetComeCode();
        //    string filter = "%" + this.txtserceacc.Text + "%";
        //    DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "GETACCCODE", filter, "", "", "", "", "", "", "", "");
        //    DataTable dt2 = ds2.Tables[0];
        //    Session["HeadAcc1"] = ds2.Tables[0];
        //    this.ddlacccode.DataSource = dt2;
        //    this.ddlacccode.DataTextField = "actdesc1";
        //    this.ddlacccode.DataValueField = "actcode";
        //    this.ddlacccode.DataBind();
        //    //----Show Resource code and Specification Code------------// 

        //    DataTable dt01 = (DataTable)Session["HeadAcc1"];
        //    string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
        //    DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
        //    if (dr1.Length == 0)
        //        return;

        //    if (dr1[0]["actelev"].ToString() == "2")
        //    {
        //        this.txtserchReCode.Visible = true;
        //        this.lnkRescode.Visible = true;
        //        this.ddlresuorcecode.Visible = true;
        //        this.txtSearchSpeci.Visible = true;
        //        this.lnkSpecification.Visible = true;
        //        this.ddlSpclinf.Visible = true;
        //        this.lblqty.Visible = true;
        //        this.txtqty.Visible = true;
        //        this.lblrate.Visible = true;
        //        this.txtrate.Visible = true;
        //        this.txtqty.Text = "";
        //        this.txtrate.Text = "";
        //    }
        //    else
        //    {
        //        this.txtserchReCode.Visible = false;
        //        this.lnkRescode.Visible = false;
        //        this.ddlresuorcecode.Visible = false;
        //        this.txtSearchSpeci.Visible = false;
        //        this.lnkSpecification.Visible = false;
        //        this.ddlSpclinf.Visible = false;
        //        this.lblqty.Visible = false;
        //        this.txtqty.Visible = false;
        //        this.lblrate.Visible = false;
        //        this.txtrate.Visible = false;
        //        this.ddlSpclinf.Items.Clear();
        //        this.ddlresuorcecode.Items.Clear();
        //        this.txtqty.Text = "";
        //        this.txtrate.Text = "";
        //    }
        //    //---------------------------------------------//
        //    this.txtserceacc.Text = "";
        //}
        //protected void lnkRescode_Click(object sender, EventArgs e)
        //{

        //    string comcod = this.GetComeCode();
        //    string filter1 = this.txtserchReCode.Text + "%";
        //    DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "GETRESCODE", filter1, "", "", "", "", "", "", "", "");
        //    DataTable dt3 = ds3.Tables[0];
        //    this.ddlresuorcecode.DataSource = dt3;
        //    this.ddlresuorcecode.DataTextField = "resdesc1";
        //    this.ddlresuorcecode.DataValueField = "rescode";
        //    this.ddlresuorcecode.DataBind();
        //    this.txtserchReCode.Text = "";
        //}
        //protected void lnkSpecification_Click(object sender, EventArgs e)
        //{

        //    string comcod =this.GetComeCode();
        //    string rescode = this.ddlresuorcecode.SelectedValue.ToString().Trim();
        //    string filter2 = "%" + this.txtSearchSpeci.Text + "%";
        //    DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "GETSPCILINFCODE", filter2, rescode, "", "", "", "", "", "", "");
        //    DataTable dt4 = ds4.Tables[0];
        //    this.ddlSpclinf.DataSource = dt4;
        //    this.ddlSpclinf.DataTextField = "spdesc1";
        //    this.ddlSpclinf.DataValueField = "spcod";
        //    this.ddlSpclinf.DataBind();

        //}



        protected void Calculate_Rate()
        {
            //double Qty1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtqty.Text.Trim()));
            //if (Qty1 == 0)
            //    return;

            //double DrAmt2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
            //this.txtrate.Text = (DrAmt2 / Qty1).ToString("#,##0.00;(#,##0.00); ");
        }


        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (this.lnkOk.Text == "Ok")
            {
                if (this.ddlPrivousVou.Items.Count > 0)
                {

                    string comcod = this.GetComeCode();
                    string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "EDITPAYMENTPRP", vounum, "", "", "", "", "", "", "", "");

                    DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);

                    if (dt.Rows.Count == 0)
                        return;
                    this.dgv1.DataSource = dt;
                    this.dgv1.DataBind();
                    this.CalculatrGridTotal();
                    Session["tblPayApp"] = dt;
                    //-------------** Edit **---------------------------//
                    DataTable dtedit = _EditDataSet.Tables[1];
                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["papdat"]).ToString("dd-MMM-yyyy (ddd)");
                    this.txtRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
                    // this.txtSrinfo.Text = dtedit.Rows[0]["srinfo"].ToString();
                    this.txtNarration.Text = dtedit.Rows[0]["venar"].ToString();
                    this.txtEntryDate.Enabled = false;
                    //-------------------------------------------------//
                    this.Panel4.Visible = true;
                    this.lbllstVouno.Visible = false;
                    this.txtLastVou.Visible = false;
                    this.lblcurVounum.Text = "Edit Voucher No.";
                    string cvno1 = this.ddlPrivousVou.SelectedValue.ToString().Substring(0, 8);
                    this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                    this.txtCurrntlast6.Text = this.ddlPrivousVou.SelectedValue.ToString().Substring(8);
                    this.txtCurrntlast6.Enabled = false;
                }
                else
                {
                    //------------
                    Session.Remove("tblPayApp");
                    string comcod = this.GetComeCode();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "PAYMENTPRP", "", "", "", "", "", "", "", "", "");

                    DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);

                    if (dt.Rows.Count == 0)
                        return;
                    this.dgv1.DataSource = dt;
                    this.dgv1.DataBind();
                    this.CalculatrGridTotal();
                    Session["tblPayApp"] = dt;

                    //------------

                    this.txtEntryDate.Enabled = true;
                    this.txtCurrntlast6.Enabled = true;
                    this.ibtnvounu.Visible = true;
                }
                this.lnkPrivVou.Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.Panel4.Visible = true;
                this.lnkFinalUpdate.Enabled = true;
                this.lnkOk.Text = "New Entry";
            }
            else
            {
                this.lnkOk.Text = "Ok";
                this.txtCurrntlast6.Enabled = false;
                this.ibtnvounu.Visible = false;

                this.Panel4.Visible = false;

                dgv1.DataSource = null;
                dgv1.DataBind();
                this.ddlPrivousVou.Items.Clear();
                this.lbllstVouno.Visible = true;
                this.txtLastVou.Visible = true;
                this.lblcurVounum.Text = "Current Voucher No.";
                this.txtcurrentvou.Text = "";
                this.txtCurrntlast6.Text = "";
                this.lnkPrivVou.Visible = true;
                this.ddlPrivousVou.Visible = true;
                this.txtEntryDate.Enabled = true;
                this.Refrsh();

            }
        }
        private void Refrsh()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.txtSrinfo.Text = "";
            this.txtRefNum.Text = "";
            this.txtNarration.Text = "";
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

        protected void lnkOk0_Click(object sender, EventArgs e)
        {
            this.Calculate_Rate();
            try
            {
                //----------------Add Data Into Grid--------------------------//
                this.Panel4.Visible = true;
                DataTable tblt01 = (DataTable)Session["tblt01"];
                DataTable tblt02 = (DataTable)Session["tblt02"];
                DataTable tblt03 = new DataTable();
                tblt01.Rows.Clear();
                tblt02.Rows.Clear();
                tblt03.Rows.Clear();

                for (int i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    string dgAccCode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string dgResCode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();

                    string dgSpclCode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string dgAccDesc = ((Label)this.dgv1.Rows[i].FindControl("lblAccdesc")).Text.Trim();
                    string dgResDesc = ((Label)this.dgv1.Rows[i].FindControl("lblResdesc")).Text.Trim();
                    string dgSpclDesc = ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text.Trim();
                    double dgTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                    double dgTrnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                    double dgTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));

                    string dgPaidto = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvpaidto")).Text.Trim();
                    string dgPurpose = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvpurpose")).Text.Trim();
                    string dgTrnRemarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    //actcode subcode actdesc subdesc trnqty trnrate trndram trncram trnrmrk 
                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dgAccCode;
                    dr1["subcode"] = dgResCode;
                    dr1["spclcode"] = dgSpclCode;
                    dr1["actdesc"] = dgAccDesc;
                    dr1["subdesc"] = dgResDesc;
                    dr1["spcldesc"] = dgSpclDesc;
                    dr1["trnqty"] = dgTrnQty;
                    dr1["trnrate"] = dgTrnrate;
                    dr1["trndram"] = dgTrnDrAmt;
                    dr1["paidto"] = dgPaidto;
                    dr1["purpose"] = dgPurpose;
                    dr1["trnrmrk"] = dgTrnRemarks;
                    tblt01.Rows.Add(dr1);
                    // }
                }
                //DataRow dr2 = tblt01.NewRow();
                //dr2["actcode"] = AccCode;
                //dr2["subcode"] = ResCode;
                //dr2["spclcode"] = SpclCode;
                //dr2["actdesc"] = AccDesc;
                //dr2["subdesc"] = ResDesc;
                //dr2["spcldesc"] = SpclDesc;
                //dr2["trnqty"] = TrnQty;
                //dr2["trnrate"] = Trnrate;
                //dr2["trndram"] = TrnDrAmt;
                //dr2["paidto"] = "";
                //dr2["purpose"] = "";
                //dr2["trnrmrk"] = TrnRemarks;
                //tblt01.Rows.Add(dr2);
                //--------------** Remove Duplicate Value **----------------------------//
                //--** Only Actdesc remove actcod not remove from grid **---------------// 
                DataView dv1 = tblt01.DefaultView;
                dv1.Sort = "actcode";
                tblt03 = dv1.ToTable();
                string AccDesc1 = null;
                for (int j = 0; j < tblt03.Rows.Count; j++)
                {
                    DataRow dr3 = tblt02.NewRow();
                    dr3["actcode"] = tblt03.Rows[j]["actcode"].ToString();
                    dr3["subcode"] = tblt03.Rows[j]["subcode"].ToString();
                    dr3["spclcode"] = tblt03.Rows[j]["spclcode"].ToString();
                    string tserch = tblt03.Rows[j]["actcode"].ToString();
                    if (tserch == AccDesc1 || tserch == "")
                    {
                        dr3["actdesc"] = "";
                    }
                    else
                    {
                        dr3["actdesc"] = tblt03.Rows[j]["actdesc"].ToString();
                        AccDesc1 = tblt03.Rows[j]["actcode"].ToString();
                    }

                    dr3["subdesc"] = tblt03.Rows[j]["subdesc"].ToString();
                    dr3["spcldesc"] = tblt03.Rows[j]["spcldesc"].ToString();
                    dr3["trnqty"] = Convert.ToDouble(tblt03.Rows[j]["trnqty"].ToString());
                    dr3["trnrate"] = Convert.ToDouble(tblt03.Rows[j]["trnrate"].ToString());
                    dr3["trndram"] = Convert.ToDouble(tblt03.Rows[j]["trndram"].ToString());
                    dr3["paidto"] = tblt03.Rows[j]["paidto"].ToString();
                    dr3["purpose"] = tblt03.Rows[j]["purpose"].ToString();
                    dr3["trnrmrk"] = tblt03.Rows[j]["trnrmrk"].ToString();
                    tblt02.Rows.Add(dr3);

                }
                //---------------------------------------------//
                dgv1.DataSource = tblt02;
                dgv1.DataBind();
                this.CalculatrGridTotal();

                //this.txtDrAmt.Text = "";
                //this.txtqty.Text = "";
                //this.txtrate.Text = "";
                //this.txtremarks.Text = "";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
            }

        }
        protected void CalculatrGridTotal()
        {
            double TQty = 0.00;
            double TRate = 0.00;
            double TDrAmt = 0.00;
            double TCrAmt = 0.00;
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                double dg1TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                double dg1TrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double dg1TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                TQty += dg1TrnQty;
                TRate += dg1TrnRate;
                TDrAmt += dg1TrnDrAmt;
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = dg1TrnQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = dg1TrnRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = dg1TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");


            }
            if (this.dgv1.Rows.Count > 0)
            {
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvQty")).Text = TQty.ToString("#,##0.00;(#,##0.00); - ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvRate")).Text = TRate.ToString("#,##0.00;(#,##0.00); - ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text = TDrAmt.ToString("#,##0.00;(#,##0.00); - ");
            }
            if (this.ddlPrivousVou.Items.Count > 0)
            {
                for (int j = 0; j < this.dgv1.Rows.Count; j++)
                {
                    ((CheckBox)this.dgv1.Rows[j].FindControl("chkItem")).Checked = true;
                    //(CheckBox)this.dgv1.Rows[i].FindControl("chkAllfrm")).ckecked=true;
                }
            }
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.CalculatrGridTotal();
        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            this.CalculatrGridTotal();
            if (this.txtLastVou.Text.Trim() != "")
            {
                if (this.txtcurrentvou.Text.Trim() != "")
                {

                    string comcod = this.GetComeCode();
                    string prodat = this.txtEntryDate.Text.Substring(0, 11);
                    string pronum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + prodat.Substring(7, 4) +
                                    this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                    string refnum = this.txtRefNum.Text.Trim();
                    string srinfo = this.txtSrinfo.Text;
                    string pronarration1 = this.txtNarration.Text.Trim();
                    string pronarration2 = (pronarration1.Length > 200 ? pronarration1.Substring(200) : "");
                    pronarration1 = (pronarration1.Length > 200 ? pronarration1.Substring(0, 200) : pronarration1);

                    string voutype = "Payment Proposal";
                    string edit = (this.txtCurrntlast6.Enabled ? "" : "EDIT");

                    try
                    {
                        //-----------Update Transaction B Table-----------------//
                        bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "INACPAPB", pronum, prodat, refnum, pronarration1,
                                        "", "", "", "", "", "", "", "", "", "", "");
                        if (!resultb)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }
                        //-----------Update Transaction A Table-----------------//
                        for (int i = 0; i < dgv1.Rows.Count; i++)
                        {

                            string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                            string rescode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                            string spclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                            string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                            string trnamt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()).ToString();

                            string paidto = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvpaidto")).Text.Trim();
                            string purpose = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvpurpose")).Text.Trim();
                            //string txtgvpaidto = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                            string trnremarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                            if (((CheckBox)this.dgv1.Rows[i].FindControl("chkItem")).Checked)//chkItem
                            {
                                bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "UPDATEACPPROA", actcode, rescode, pronum,
                                          "", "", "", "", "", "", "", "", "", "", "", "");
                                if (!resulta)
                                {
                                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                    return;
                                }

                                bool resulta1 = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "INACPAPA", pronum, actcode, rescode, spclcode,
                                         prodat, trnamt, trnqty, "", "", "", "", "", "", "", "");
                                if (!resulta1)
                                {
                                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                    return;
                                }
                            }


                        }
                     ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                        //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                        this.lnkFinalUpdate.Enabled = false;
                        this.GetPriviousVoucher();
                        this.txtcurrentvou.Text = "";
                        this.txtCurrntlast6.Text = "";

                    }
                    catch (Exception ex)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                    }
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Get Vocher No.";
                }
            }
            else
            {
                string comcod = this.GetComeCode();
                string prodat = this.txtEntryDate.Text.Substring(0, 11);
                string pronum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + prodat.Substring(7, 4) +
                                this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

                for (int i = 0; i < dgv1.Rows.Count; i++)
                {

                    string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    string trnamt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()).ToString();

                    string paidto = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvpaidto")).Text.Trim();
                    string purpose = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvpurpose")).Text.Trim();
                    //string txtgvpaidto = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                    string trnremarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                    if (((CheckBox)this.dgv1.Rows[i].FindControl("chkItem")).Checked == false)//chkItem
                    {
                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "UPACPPROA", actcode, rescode, pronum,
                                  "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }

                        bool resulta1 = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "DELACPAPA", pronum, actcode, rescode, spclcode,
                                 prodat, trnamt, trnqty, "", "", "", "", "", "", "", "");
                        if (!resulta1)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }

                    }
                 ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";

                    if (ConstantInfo.LogStatus == true)
                    {
                        string eventtype = "Payment Approval";
                        string eventdesc = "Update Approval";
                        string eventdesc2 = pronum;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                    }

                }

            }
        }
        //protected void ddlacccode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.ddlacccode.BackColor = System.Drawing.Color.Pink;
        //    DataTable dt01 = (DataTable)Session["HeadAcc1"];
        //    string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
        //    DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
        //    if (dr1[0]["actelev"].ToString() == "2")
        //    {
        //        this.txtserchReCode.Visible = true;
        //        this.lnkRescode.Visible = true;
        //        this.ddlresuorcecode.Visible = true;
        //        this.txtSearchSpeci.Visible = true;
        //        this.lnkSpecification.Visible = true;
        //        this.ddlSpclinf.Visible = true;
        //        this.lblqty.Visible = true;
        //        this.txtqty.Visible = true;
        //        this.lblrate.Visible = true;
        //        this.txtrate.Visible = true;
        //        this.txtqty.Text = "";
        //        this.txtrate.Text = "";
        //    }
        //    else
        //    {
        //        this.txtserchReCode.Visible = false;
        //        this.lnkRescode.Visible = false;
        //        this.ddlresuorcecode.Visible = false;
        //        this.txtSearchSpeci.Visible = false;
        //        this.lnkSpecification.Visible = false;
        //        this.ddlSpclinf.Visible = false;
        //        this.lblqty.Visible = false;
        //        this.txtqty.Visible = false;
        //        this.lblrate.Visible = false;
        //        this.txtrate.Visible = false;
        //        this.ddlSpclinf.Items.Clear();
        //        this.ddlresuorcecode.Items.Clear();
        //        this.txtqty.Text = "";
        //        this.txtrate.Text = "";
        //    }
        //}

        protected void ibtnvounu_Click(object sender, ImageClickEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "GETNEWPAYAPP", entrydate, "", "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            string cvno1 = dt4.Rows[0]["coupronum"].ToString().Substring(0, 8);
            this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
            this.txtCurrntlast6.Text = dt4.Rows[0]["coupronum"].ToString().Substring(8);
            string pvno1 = ds4.Tables[1].Rows[0]["lastpronum"].ToString().Trim();
            this.txtLastVou.Text = pvno1.Substring(0, 2) + pvno1.Substring(6, 2) + "-" + pvno1.Substring(8, 6);
            //this.ddlPrivousVou.BackColor = System.Drawing.Color.Aqua;
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Session.Remove("tblPayApp");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                string vounum = this.ddlPrivousVou.SelectedValue.ToString();

                DataTable dt1 = (DataTable)Session["tblPayApp"];
                ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptAccPaymntApp(); //.rptPrintVoucher();
                TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["CompName"] as TextObject;
                txtCompanyName.Text = comnam;
                if (this.ddlPrivousVou.Items.Count > 0)
                {
                    TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["ProNum"] as TextObject;
                    txtcAdd.Text = "Approve No. : " + this.ddlPrivousVou.SelectedItem.Text.Substring(0, 11).ToString();
                    TextObject txtcDate = rptinfo.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                    txtcDate.Text = "Date : " + this.ddlPrivousVou.SelectedItem.Text.Substring(12, 11).ToString();
                }
                TextObject rpttxtHead = rptinfo.ReportDefinition.ReportObjects["txtHead"] as TextObject;
                rpttxtHead.Text = "Payment Approval";// ASTUtility.Trans(TAmount, 2);
                TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Payment Approval";
                    string eventdesc = "Print Approval";
                    string eventdesc2 = "Approve No. : " + this.ddlPrivousVou.SelectedItem.Text.Substring(0, 11).ToString();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                rptinfo.SetDataSource((DataTable)Session["tblPayApp"]);
                Session["Report1"] = rptinfo;
                this.lblprint.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }


        protected void lnkPrivVou_Click(object sender, EventArgs e)
        {
            this.GetPriviousVoucher();
        }
        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblSupRate"];
            int i, index;

            if (((CheckBox)this.dgv1.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    ((CheckBox)this.dgv1.Rows[i].FindControl("chkItem")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkItem")).Checked) ? false : true;
                    ((CheckBox)this.dgv1.Rows[i].FindControl("chkItem")).Checked = true;
                    index = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + i;
                    //dt.Rows[i]["markitem"] = "True";
                }
            }

            else
            {
                for (i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    ((CheckBox)this.dgv1.Rows[i].FindControl("chkItem")).Enabled = true;
                    ((CheckBox)this.dgv1.Rows[i].FindControl("chkItem")).Checked = false;
                    index = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + i;
                    // dt.Rows[i]["markitem"] = "False";

                }

            }

            Session["tblSupRate"] = dt;
        }
    }
}




