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
    public partial class AccSales01 : System.Web.UI.Page
    {
        public static string Narration = "";
        public static string RefNo = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");


            if (this.ddlConAccHead.Items.Count > 0)
                return;
            this.LoadAcccombo();

            this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy (ddd)");

        }
        //private void GetPriviousVoucher()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
        //    string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
        //    string VNo2 = (VNo1 == "J" ? "V" : (this.lblGeneralAcc.Text.Contains("Payment") ? "D" : "C"));
        //    string VNo3 = Convert.ToString(VNo1 + VNo2);
        //    DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPRIVOUSVOUCHER", VNo3, "", "", "", "", "", "", "", "");

        //    this.ddlPrivousVou.DataSource = ds5.Tables[0];
        //    this.ddlPrivousVou.DataTextField = "vounum1";
        //    this.ddlPrivousVou.DataValueField = "vounum";
        //    this.ddlPrivousVou.DataBind();
        //}

        private void LoadAcccombo()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string txtsrchconcode = this.txtScrchConCode.Text.Trim() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD", txtsrchconcode, "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
                //this.GetPriviousVoucher();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }




        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (this.lnkOk.Text == "Ok")
            {
                if (this.ddlPrivousVou.Items.Count > 0)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    string vounum = this.ddlPrivousVou.SelectedValue.ToString();// this.ddlPrivousVou.SelectedItem.ToString();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
                    this.dgv1.DataSource = _EditDataSet.Tables[0];
                    if (_EditDataSet.Tables[0].Rows.Count == 0)
                        return;

                    this.dgv1.DataBind();
                    this.CalculatrGridTotal();
                    //-------------** Edit **---------------------------//
                    DataTable dtedit = _EditDataSet.Tables[1];

                    if (vounum.Substring(0, 2).ToString() != "JV")
                    {
                        this.ddlConAccHead.SelectedValue = dtedit.Rows[0]["cactcode"].ToString();
                    }
                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy (ddd)");
                    this.txtRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
                    this.txtSrinfo.Text = dtedit.Rows[0]["srinfo"].ToString();
                    this.txtNarration.Text = dtedit.Rows[0]["venar1"].ToString();
                    this.ddlConAccHead.Enabled = false;
                    this.txtEntryDate.Enabled = false;

                    //-------------------------------------------------//
                    this.ibtnvounu.Visible = false;
                    //this.lbllstVouno.Visible = false;
                    //this.txtLastVou.Visible = false;
                    //this.lblcurVounum.Text = "Edit Voucher No.";
                    this.txtcurrentvou.Text = this.ddlPrivousVou.SelectedValue.ToString().Substring(0, 8);
                    this.txtCurrntlast6.Text = this.ddlPrivousVou.SelectedValue.ToString().Substring(8);
                    this.txtcurrentvou.BackColor = System.Drawing.Color.Aqua;
                    this.txtCurrntlast6.BackColor = System.Drawing.Color.Aqua;
                    this.txtCurrntlast6.Enabled = false;
                }
                else
                {
                    this.ddlConAccHead.Enabled = true;
                    this.txtEntryDate.Enabled = true;
                    //this.lblfrmdate.Visible = true;
                    //this.txtfrmdate.Visible = true;
                    //this.lbltodate.Visible = true;
                    //this.txttodate.Visible = true;
                    //this.lnkOk0.Visible = true;
                    this.PnlMonDuePriod.Visible = true;

                    this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                }
                this.lnkPrivVou.Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.lnkOk.Text = "New Entry";
                this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
                this.txtEntryDate.BackColor = System.Drawing.Color.Aqua;

            }
            else
            {
                this.lnkOk.Text = "Ok";
                dgv1.DataSource = null;
                dgv1.DataBind();
                this.dgvVou.DataSource = null;
                this.dgvVou.DataBind();
                this.ddlPrivousVou.Items.Clear();
                this.ibtnvounu.Visible = true; ;
                //this.lbllstVouno.Visible = true;
                //this.txtLastVou.Visible = true;
                //this.lblcurVounum.Text = "Current Voucher No.";
                this.txtcurrentvou.Text = "";
                this.txtCurrntlast6.Text = "";
                this.txtCurrntlast6.Enabled = true;
                this.lnkPrivVou.Visible = true;
                this.ddlPrivousVou.Visible = true;
                this.ddlConAccHead.Enabled = true;
                this.txtEntryDate.Enabled = true;
                this.PnlMonDuePriod.Visible = false;
                this.PnlRmrks.Visible = false;
                //this.lblfrmdate.Visible = false;
                //this.txtfrmdate.Visible = false;
                //this.lbltodate.Visible = false;
                //this.txttodate.Visible = false;
                //this.lnkOk0.Visible = false;
                this.dgv1.Visible = true;

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
        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
            //this.GetPriviousVoucher();
        }
        protected void txtEntryDate_TextChanged(object sender, EventArgs e)
        {
            this.txtEntryDate.BackColor = System.Drawing.Color.Aqua;
        }
        protected void lnkOk0_Click(object sender, EventArgs e)
        {
            try
            {
                this.PnlRmrks.Visible = true;
                Session.Remove("tblMrr");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "REPORTACCSALES", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                Session["tblMrr"] = ds1.Tables[0];
                DataTable dt1 = ds1.Tables[0];
                this.HiddenSameDate(dt1);
                this.CalculatrGridTotal();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
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

            this.dgv1.DataSource = dt1;
            this.dgv1.DataBind();
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

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {

            if (this.txtcurrentvou.Text.Trim() != "")
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string userid = hst["usrid"].ToString();
                string Terminal = hst["trmid"].ToString();
                string Sessionid = hst["session"].ToString();

                if (this.ddlPrivousVou.Items.Count == 0)
                    this.ibtnvounu_Click(null, null);

                string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
                string voudat = this.txtEntryDate.Text.Substring(0, 11);
                string refnum = this.txtRefNum.Text.Trim();
                string srinfo = this.txtSrinfo.Text;
                string vounarration1 = this.txtNarration.Text.Trim();
                string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
                string voutype = (vouno == "JV" ? "Journal Voucher" :
                                 (vouno == "CD" ? "Cash Payment Voucher" :
                                 (vouno == "BD" ? "Bank Payment Voucher" :
                                 (vouno == "CC" ? "Cash Deposit Voucher" :
                                 (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
                string cactcode = (vouno == "JV" ? "000000000000" : this.ddlConAccHead.SelectedValue.ToString());
                string vtcode = "99";
                string edit = (this.txtCurrntlast6.Enabled ? "" : "EDIT");

                try
                {
                    //-----------Update Transaction B Table-----------------//
                    bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
                                    vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, "", "", "");
                    if (!resultb)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }
                    //-----------Update Transaction A Table-----------------//
                    for (int i = 0; i < this.dgvVou.Rows.Count; i++)
                    {

                        string actcode = ((Label)this.dgvVou.Rows[i].FindControl("lblgvAccCod1")).Text.Trim();
                        string rescode = ((Label)this.dgvVou.Rows[i].FindControl("lgcUcode1")).Text.Trim();
                        string spclcode = "000000000000";
                        string trnqty = Convert.ToDouble("0" + ((Label)this.dgvVou.Rows[i].FindControl("lgvqty1")).Text.Trim()).ToString();
                        double Cramt = Convert.ToDouble("0" + ((Label)this.dgvVou.Rows[i].FindControl("lgvcramt1")).Text.Trim());
                        double Dramt = Convert.ToDouble("0" + ((Label)this.dgvVou.Rows[i].FindControl("lgvdramt1")).Text.Trim());
                        string trnremarks = ((Label)this.dgvVou.Rows[i].FindControl("lgvrmrks1")).Text.Trim();
                        string trnamt = Convert.ToString(Dramt - Cramt);
                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, actcode, rescode, cactcode,
                                       voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }
                    }
                 ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                    this.UPdateMrinf(vounum);

                    ((LinkButton)this.dgvVou.FooterRow.FindControl("lnkFinalUpdate")).Enabled = false;

                    //this.GetPriviousVoucher();
                    //this.txtcurrentvou.Text = "";
                    //this.txtCurrntlast6.Text = "";

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
                        return;
                    }
                }
            }


        }
        protected void ibtnvounu_Click(object sender, ImageClickEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;

            }

            DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

            if (txtopndate >= Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                return;

            }
            double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
            string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
            string VNo2 = (VNo1 == "J" ? "V" : (this.lblGeneralAcc.Text.Contains("Payment") ? "D" : "C"));
            string VNo3 = Convert.ToString(VNo1 + VNo2);
            string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            this.txtcurrentvou.Text = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
            this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);
            //this.txtLastVou.Text = ds4.Tables[1].Rows[0]["lastvounum"].ToString();
            this.ddlPrivousVou.BackColor = System.Drawing.Color.Aqua;
        }


        protected void lnkPrivVou_Click(object sender, EventArgs e)
        {
            //this.GetPriviousVoucher();
        }


        protected void lbtnVouOk_Click(object sender, EventArgs e)
        {
            Session.Remove("dtvou");
            Narration = "";
            RefNo = "";
            if (Session["dtvou"] == null)
            {
                DataTable tblt01 = new DataTable();
                tblt01.Columns.Add("pactcode", Type.GetType("System.String"));
                tblt01.Columns.Add("usircode", Type.GetType("System.String"));
                //tblt01.Columns.Add("mrno", Type.GetType("System.String"));
                tblt01.Columns.Add("pactdesc", Type.GetType("System.String"));
                tblt01.Columns.Add("udesc", Type.GetType("System.String"));
                tblt01.Columns.Add("qty", Type.GetType("System.Double"));
                tblt01.Columns.Add("dramt", Type.GetType("System.Double"));
                tblt01.Columns.Add("cramt", Type.GetType("System.Double"));
                tblt01.Columns.Add("urmrks", Type.GetType("System.String"));
                Session["dtvou"] = tblt01;

            }

            DataTable dtvou = (DataTable)Session["dtvou"];

            DataTable dt = (DataTable)Session["tblMrr"];

            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {

                bool chkmr = ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked;
                if (chkmr == true)
                {
                    string pactcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvAccCod")).Text.Trim();
                    string usircode = ((Label)this.dgv1.Rows[i].FindControl("lgcUcode")).Text.Trim();
                    string pactdesc = ((Label)this.dgv1.Rows[i].FindControl("lgcPactdesc1r")).Text.Trim();
                    string usirdesc = ((Label)this.dgv1.Rows[i].FindControl("lgcUdesc")).Text.Trim();
                    double qty = Convert.ToDouble("0" + ((Label)this.dgv1.Rows[i].FindControl("lgvqty")).Text.Trim());
                    double cramt = Convert.ToDouble("0" + ((Label)this.dgv1.Rows[i].FindControl("lgvcramt")).Text.Trim());
                    double dramt = Convert.ToDouble("0" + ((Label)this.dgv1.Rows[i].FindControl("lgvdramt")).Text.Trim());
                    string rmrks = ((Label)this.dgv1.Rows[i].FindControl("lgvrmrks")).Text.Trim();

                    DataRow[] dr = dtvou.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["cramt"] = Convert.ToDouble(dr[0]["cramt"]) + cramt;

                    }
                    else
                    {
                        DataRow dr1 = dtvou.NewRow();
                        dr1["pactcode"] = pactcode;
                        dr1["usircode"] = usircode;
                        dr1["pactdesc"] = pactdesc;
                        dr1["udesc"] = usirdesc;
                        dr1["qty"] = qty;
                        dr1["cramt"] = cramt;
                        dr1["dramt"] = dramt;
                        dr1["urmrks"] = rmrks;
                        dtvou.Rows.Add(dr1);
                    }

                    if (dt.Rows[i]["paydesc"].ToString().Trim() == "CASH")
                    {
                        Narration += "Mrr" + dt.Rows[i]["mrno"].ToString() + " Date:" + Convert.ToDateTime(dt.Rows[i]["paydate"]).ToString("dd.MM.yyyy") + "; ";


                    }
                    else
                    {
                        RefNo += dt.Rows[i]["chqno"].ToString() + ", ";
                        Narration += "Mrr" + dt.Rows[i]["mrno"].ToString() + ", " + "Date:" + Convert.ToDateTime(dt.Rows[i]["paydate"]).ToString("dd.MM.yyyy") + ", " + dt.Rows[i]["bankname"].ToString() + ", " + dt.Rows[i]["bbranch"].ToString() + "; ";

                    }



                }

            }
            int Reflenght = RefNo.Length;
            if (Reflenght > 0)
            {
                RefNo = RefNo.Substring(0, Reflenght - 2);
                this.txtRefNum.Text = RefNo;
            }
            int lenght = Narration.Length;
            Narration = Narration.Substring(0, lenght - 2);
            this.txtNarration.Text = Narration;
            Session["dtvou"] = dtvou;
            this.HiddenSameDate1(dtvou);
            this.dgv1.Visible = false;

        }

        private void HiddenSameDate1(DataTable dt1)
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

            this.dgvVou.DataSource = dt1;
            this.dgvVou.DataBind();
            this.CalculatrGridTotal1();
        }

        private void CalculatrGridTotal1()
        {
            DataTable dttotal = (DataTable)Session["dtvou"];
            ((Label)this.dgvVou.FooterRow.FindControl("lgvFAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dttotal.Compute("Sum(cramt)", "")) ?
          0.00 : dttotal.Compute("Sum(cramt)", ""))).ToString("#,##0;-#,##0; ");
            TAmount = Convert.ToDouble(((Label)this.dgvVou.FooterRow.FindControl("lgvFAmt1")).Text);

        }


        protected void ibtnFindConCode_Click(object sender, ImageClickEventArgs e)
        {
            this.LoadAcccombo();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.ddlPrivousVou.Items.Count > 0 && this.lnkOk.Text == "Ok")
                    this.lnkOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(6, 2) + this.txtCurrntlast6.Text.Trim();


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";




                ////string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt = _ReportDataSet.Tables[0];
                //if (dt.Rows.Count == 0)
                //    return;
                //double dramt, cramt;
                //dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                //cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                //if (dramt > 0 && cramt > 0)
                //{
                //    TAmount = cramt;

                //}
                //else if (dramt > 0 && cramt <= 0)
                //{
                //    TAmount = dramt;
                //}
                //else
                //{
                //    TAmount = cramt;
                //}

                //DataTable dt1 = _ReportDataSet.Tables[1];
                //string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string refnum = dt1.Rows[0]["refnum"].ToString();
                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string venar = dt1.Rows[0]["venar"].ToString();
                //ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No.: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = " Date:" + voudat;
                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Naration: " + venar;

                ////TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                ////txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                ////string comcod = this.GetComeCode();
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //this.lblprint.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //            this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
    }
}


