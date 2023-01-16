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
    public partial class AccMatConversion : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = " Transfer Journal";
                //this.Master.Page.Title = " Transfer Journal";
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                ////if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                ////    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.LoadTrnsCombo();
                CreateTable();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }




        }

        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("comcod", Type.GetType("System.String"));
            tblt01.Columns.Add("convrno", Type.GetType("System.String"));
            tblt01.Columns.Add("pactcode", Type.GetType("System.String"));
            tblt01.Columns.Add("rsircode", Type.GetType("System.String"));
            tblt01.Columns.Add("spcfcode", Type.GetType("System.String"));

            tblt01.Columns.Add("pactdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rsirdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcfdesc", Type.GetType("System.String"));

            tblt01.Columns.Add("qty", Type.GetType("System.Double"));
            tblt01.Columns.Add("rate", Type.GetType("System.Double"));
            tblt01.Columns.Add("Dr", Type.GetType("System.Double"));
            tblt01.Columns.Add("Cr", Type.GetType("System.Double"));



            Session["tblt01"] = tblt01;
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void LoadConversionCombo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string SerchTrnNo = (this.Request.QueryString["genno"].ToString()).Length == 0 ? ("%" + this.txtSrcConvrNo.Text.Trim() + "%") : (this.Request.QueryString["genno"].ToString() + "%");

            //string SerchTrnNo = "%"+this.txtSrcTrnNo.Text.Trim()+"%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONVERSIONNO", SerchTrnNo, "", "", "", "", "", "", "", "");
            this.ddlConversionList.Items.Clear();
            this.ddlConversionList.DataTextField = "textfield";
            this.ddlConversionList.DataValueField = "convrno";
            this.ddlConversionList.DataSource = ds1.Tables[0];
            this.ddlConversionList.DataBind();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.LoadConversionCombo();
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
            this.LoadConversionCombo();

            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.grvAccMatConversion.DataSource = null;
            this.grvAccMatConversion.DataBind();
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.lnkFinalUpdate.Enabled = true;
            this.pnlTrans.Visible = false;

        }


        protected void imgbtnConvrNo_Click(object sender, EventArgs e)
        {
            this.LoadConversionCombo();
        }


        protected void lbtnSelectConversion_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string conversionno = this.ddlConversionList.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETMATCONVERSIONINFO", conversionno,
                          "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            DataTable dt2 = ds1.Tables[1];

            DataTable tblt01 = (DataTable)Session["tblt01"];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {


                DataRow dr1 = tblt01.NewRow();
                dr1["comcod"] = dt1.Rows[i]["comcod"].ToString();
                dr1["convrno"] = dt1.Rows[i]["convrno"].ToString();
                dr1["pactcode"] = dt1.Rows[i]["pactcode"].ToString();
                dr1["rsircode"] = dt1.Rows[i]["rsircode"].ToString();
                dr1["spcfcode"] = dt1.Rows[i]["spcfcode"].ToString();
                dr1["pactdesc"] = dt1.Rows[i]["pactdesc"].ToString();
                dr1["rsirdesc"] = dt1.Rows[i]["rsirdesc"].ToString();
                dr1["spcfdesc"] = dt1.Rows[i]["spcfdesc"].ToString();

                dr1["qty"] = Convert.ToDouble(dt1.Rows[i]["qty"]);
                dr1["rate"] = Convert.ToDouble(dt1.Rows[i]["rate"]);
                dr1["Dr"] = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                dr1["Cr"] = Convert.ToDouble(dt1.Rows[i]["Cr"]);

                tblt01.Rows.Add(dr1);
            }
            if (tblt01.Rows.Count == 0)
                return;
            Session["tblt01"] = this.HiddenSameData(tblt01);
            grvAccMatConversion.DataSource = (DataTable)Session["tblt01"];
            grvAccMatConversion.DataBind();
            calculation();

            this.txtCurrntlast6.ReadOnly = false;
            this.pnlTrans.Visible = true;
            this.txtSrinfo.Text = dt2.Rows[0]["convrref"].ToString();
            this.txtNarration.Text = dt2.Rows[0]["convrnar"].ToString();







        }


        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;
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

            return dt1;
        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)Session["tblt01"];
            if (dt2.Rows.Count == 0)
                return;


            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(Dr)", "")) ?
                       0.00 : dt2.Compute("Sum(Dr)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(Cr)", "")) ?
                        0.00 : dt2.Compute("Sum(Dr)", ""))), 2);
            ((TextBox)this.grvAccMatConversion.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.grvAccMatConversion.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");





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
            //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;
            //}
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

            string conversionno = ((Label)this.grvAccMatConversion.Rows[0].FindControl("lblConvno")).Text.Trim();


            try
            {

                string CallType = (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                                  vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "", "");


                bool updatevoucher = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEVOUNUMCONVR", conversionno, vounum, "", "", "",
                                 "", "", "", "", "", "", "", "", "", "");

                //bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                //        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                if (!updatevoucher)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                //-----------Update Transaction A Table-----------------//
                //string trnno2 = "XXXXXXXXXXXXXX";
                for (int i = 0; i < grvAccMatConversion.Rows.Count; i++)
                {
                    string actcode = ((Label)this.grvAccMatConversion.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.grvAccMatConversion.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.grvAccMatConversion.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.grvAccMatConversion.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.grvAccMatConversion.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.grvAccMatConversion.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((TextBox)this.grvAccMatConversion.Rows[i].FindControl("txtgvRemarks")).Text.Trim();


                    bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, cactcode,
                          voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, "", userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");




                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }


                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Material Conversion Journal";
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


    }
}