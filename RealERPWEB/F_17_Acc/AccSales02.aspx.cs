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
    public partial class AccSales02 : System.Web.UI.Page
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
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            if (this.ddlConAccHead.Items.Count > 0)
                return;
            this.LoadAcccombo();
            this.GetProjectName();
            this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
            this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
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
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        private void Refrsh()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }
        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
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
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "REPORTACCSALES", frmdate, todate, pactcode, "", "", "", "", "", "");
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
                DataTable dt1 = ds1.Tables[0];
                this.HiddenSameDate(dt1);
                this.CalculatrGridTotal();

            }
            catch (Exception ex)
            {
                //this.lblmsg.Text = "Error :" + ex.Message;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Available Data Not in Position";
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
                        return;
                    }
                }
            }


        }
        private void vounum()
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
            Session["NEWVOUNUM"] = dt4;
        }

        protected void ibtnFindConCode_Click(object sender, ImageClickEventArgs e)
        {
            this.LoadAcccombo();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text;


            ReportDocument rptProSummary = new RealERPRPT.R_17_Acc.rptCollUpdate();
            TextObject rpttxtPrjName = rptProSummary.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            rpttxtPrjName.Text = "Project Name:- " + projectName;
            TextObject rpttxtDate = rptProSummary.ReportDefinition.ReportObjects["date"] as TextObject;
            rpttxtDate.Text = "From: " + this.txtfrmdate.Text + " To: " + this.txttodate.Text;
            TextObject txtuserinfo = rptProSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Collection Update";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptProSummary.SetDataSource((DataTable)Session["tblMrr"]);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptProSummary.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptProSummary;
            this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void Data_Bind()
        {
            this.dgv1.DataSource = (DataTable)Session["tblMrr"];
            this.dgv1.DataBind();
            DataTable dt1 = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string pactcode = ((Label)dgv1.Rows[i].FindControl("lblgvAccCod")).Text.Trim();
                string usircode = ((Label)dgv1.Rows[i].FindControl("lgcUcode")).Text.Trim();
                string mrno = ((Label)dgv1.Rows[i].FindControl("lgvmrno")).Text.Trim();
                string cheqno = ((Label)dgv1.Rows[i].FindControl("lgvCheNo")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)dgv1.Rows[i].FindControl("lbok");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = pactcode + usircode + mrno + cheqno;
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
                dt.Rows[i]["sirialno"] = Sirialno;
                dt.Rows[i]["recondat"] = Recdate;
                dt.Rows[i]["chkmv"] = chkmr;
                ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblMrr"] = dt;
        }
        protected void lbok_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
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
            string pactcode = code.Substring(0, 12).ToString();
            string usircode = code.Substring(12, 12).ToString();
            string mrno = code.Substring(24, 9).ToString();
            string cheqno = code.Substring(33).ToString();

            DataTable dt = (DataTable)Session["tblMrr"];
            DataRow[] dr = dt.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "' and mrno='" + mrno + "' and chqno='" + cheqno + "'");


            double tqty = Convert.ToDouble(dr[0]["qty"].ToString());
            double dramt = Convert.ToDouble(dr[0]["dramt"].ToString());
            double cramt = Convert.ToDouble(dr[0]["cramt"].ToString());
            string trnRemarks = dr[0]["urmrks"].ToString().Trim();
            string Chk = dr[0]["chkmv"].ToString();
            string trnamt = Convert.ToString(dramt - cramt);
            if (Chk == "False")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Check CheckBox";
                return;
            }
            /////////////////--------------------------------------------------
            //Existing MR

            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGCOLUP", mrno, pactcode, usircode, cheqno, "", "", "", "", "");
            if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this MR No";
                return;
            }

            //return;
            //////////////-----------------------------
            this.vounum();
            DataTable dt12 = (DataTable)Session["NEWVOUNUM"];

            ///-----------------------------------------------------///////////////////
            string type = dr[0]["paydesc"].ToString();
            if (type == "CASH")
            {
                Narration = "Mr" + dr[0]["mrno"].ToString() + " Date:" + Convert.ToDateTime(dr[0]["paydate"]).ToString("dd.MM.yyyy") + "; ";
            }

            else
            {
                RefNo = dr[0]["chqno"].ToString() + ", ";
                Narration = "Mr" + dr[0]["mrno"].ToString() + ", " + "Date:" + Convert.ToDateTime(dr[0]["paydate"]).ToString("dd.MM.yyyy") + ", " + dr[0]["bankname"].ToString() + ", " + dr[0]["bbranch"].ToString() + "; ";

            }
            int Reflenght = RefNo.Length;
            if (Reflenght > 0)
            {
                RefNo = RefNo.Substring(0, Reflenght - 2);
            }
            int lenght = Narration.Length;
            Narration = Narration.Substring(0, lenght - 2);
            /////////---------------------------------------
            string vounum = dt12.Rows[0]["couvounum"].ToString();
            string voudat = this.txtEntryDate.Text.Substring(0, 11);
            string refnum = RefNo;
            string srinfo = dr[0]["sirialno"].ToString().Trim();
            string recondat = (dr[0]["recondat"].ToString().Trim() == "01-Jan-1900") ? voudat : dr[0]["recondat"].ToString().Trim();
            string vounarration1 = Narration;
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string vouno = vounum.Substring(0, 2).ToString();
            string voutype = (vouno == "JV" ? "Journal Voucher" :
                                (vouno == "CD" ? "Cash Payment Voucher" :
                                (vouno == "BD" ? "Bank Payment Voucher" :
                                (vouno == "CC" ? "Cash Deposit Voucher" :
                                (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
            string cactcode = (vouno == "JV" ? "000000000000" : this.ddlConAccHead.SelectedValue.ToString());
            string vtcode = "99";
            string edit = "EDIT";

            dr[0]["newvocnum"] = vounum.Substring(0, 2).ToString() + vounum.Substring(6, 2).ToString() + "-" + vounum.Substring(8).ToString();
            dr[0]["recondat"] = recondat;

            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                //-----------Update Transaction A Table-----------------//

                string spclcode = "000000000000";

                bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, pactcode, usircode, cactcode,
                                voudat, tqty.ToString(), trnRemarks, vtcode, trnamt, spclcode, recondat, "", "", "", "");
                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";

                this.Data_Bind();
                this.CheckValue();

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Collection Update";
                    string eventdesc = "Update Collection";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }


                // Update  MRR------------------------------------
                resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEEMRINF", pactcode, usircode, mrno, cheqno, vounum,
                            "", "", "", "", "", "", "", "", "", "");
                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }


        }
        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }
    }
}