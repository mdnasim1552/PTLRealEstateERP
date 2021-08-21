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

    public partial class AccPurchaseFor : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = " Purchase Accounts";
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.Master.Page.Title = "Purchase Accounts";
                this.LoadBillCombo();
                CreateTable();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
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
            tblt01.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("billnar", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void LoadBillCombo()
        {

            string comcod = this.GetCompCode();
            string Billno = this.txtBillno.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBILLACCLC", Billno, "", "", "", "", "", "", "", "");
            this.ddlBillList.Items.Clear();
            this.ddlBillList.DataTextField = "textfield";
            this.ddlBillList.DataValueField = "billno";
            this.ddlBillList.DataSource = ds1.Tables[0];
            this.ddlBillList.DataBind();
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
            ((TextBox)this.gvPurFor.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.gvPurFor.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



        }

        protected void ibtnvounu_Click(object sender, ImageClickEventArgs e)
        {

            try
            {

                string comcod = this.GetCompCode();
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
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


            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")

            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                //this.ibtnvounu.Visible = true;
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            Session.Remove("tblt01");
            this.CreateTable();
            this.LoadBillCombo();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.gvPurFor.DataSource = null;
            this.gvPurFor.DataBind();
            //this.lnkFinalUpdate.Enabled = true;
            this.txtcurrentvou.Enabled = true;
            this.txtCurrntlast6.Enabled = true;
            this.ibtnvounu.Visible = false;
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
        }


        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            if (this.txtcurrentvou.Text.Trim() != "")
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string LCNumber = this.ddlBillList.SelectedValue.ToString();
                string cactcode = "000000000000";
                //string spclcode = "000000000000";
                string voudat = this.txtdate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                      this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                //string voudat = this.txtdate.Text.Substring(0, 11);
                string refnum = "";
                string srinfo = "";
                string trnremarks = "LCXXXXXXXXXXXX";
                string vounarration1 = "";
                string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
                string voutype = "Journal Voucher";

                string vtcode = "01";
                string edit = "";
                string TgvDrAmt = ((Label)this.gvPurFor.FooterRow.FindControl("lblgvTDramt")).Text;
                string TgvCrAmt = ((Label)this.gvPurFor.FooterRow.FindControl("lblgvTCramt")).Text;
                if (vouno == "JV" && TgvDrAmt != TgvCrAmt)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Dr. Amount not equals to Cr. Amount.";
                    return;
                }
                try
                {
                    //-----------Update Transaction B Table-----------------//
                    bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE_01", vounum, voudat, refnum, srinfo, vounarration1, vounarration2, voutype, vtcode, edit, "", "", "", "", "", "");
                    if (!resultb)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }
                    string actcode2 = "XXXXXXXXXXXX";
                    //-----------Update Transaction A Table-----------------//
                    for (int i = 0; i < gvPurFor.Rows.Count; i++)
                    {

                        string actcode = ((Label)this.gvPurFor.Rows[i].FindControl("lblAccCod")).Text.Trim();
                        string rescode = ((Label)this.gvPurFor.Rows[i].FindControl("lblResCod")).Text.Trim();

                        string trnqty = Convert.ToDouble("0" + ((Label)this.gvPurFor.Rows[i].FindControl("lblgvqty")).Text.Trim()).ToString();
                        double Dramt = Convert.ToDouble("0" + ((Label)this.gvPurFor.Rows[i].FindControl("lblgvDramt")).Text.Trim());
                        double Cramt = Convert.ToDouble("0" + ((Label)this.gvPurFor.Rows[i].FindControl("lblgvCramt")).Text.Trim());
                        string trnamt = Convert.ToString(Dramt - Cramt);

                        string spclcode = (ASTUtility.Left(actcode, 2) == "17") ? LCNumber : "000000000000";

                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE_01", vounum, actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }

                        if (actcode2 != actcode)
                        {
                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEPURLC",
                                    actcode, vounum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                return;
                            }
                            actcode2 = actcode;
                        }
                    }
                 ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                    this.lnkFinalUpdate.Enabled = false;
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

                //jscript_Click(object sender, EventArgs e)
                //{};
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Get Vocher No.";


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

                //ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void lbtnSelectBill_Click(object sender, EventArgs e)
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.lnkFinalUpdate.Enabled = true;
                if (this.ddlBillList.Items.Count == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select LC Number.";
                    return;
                }
                this.Panel1.Visible = true;
                this.ibtnvounu.Visible = true;
                string actcode = this.ddlBillList.SelectedValue.ToString();
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETLCACCVOU", actcode, "", "", "", "", "", "", "", "");
                DataTable dt1 = ds2.Tables[0];
                Session["DCInfo"] = HiddenSameData(dt1);
                this.gvPurFor.DataSource = dt1;
                this.gvPurFor.DataBind();
                //this.Panel2.Visible = true;
                ((Label)this.gvPurFor.FooterRow.FindControl("lblgvTDramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(Dramt)", "")) ?
                0.00 : dt1.Compute("Sum(Dramt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
                ((Label)this.gvPurFor.FooterRow.FindControl("lblgvTCramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(Cramt)", "")) ?
                0.00 : dt1.Compute("Sum(Cramt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
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




        protected void imgSearchBillno_Click(object sender, EventArgs e)
        {
            this.LoadBillCombo();
        }
    }
}
