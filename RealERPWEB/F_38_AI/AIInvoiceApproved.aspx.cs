using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_38_AI
{
    public partial class AIInvoiceApproved : System.Web.UI.Page
    {
        ProcessAccess AIData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "AI Invoice Aproved";

                this.txtfrmdate.Text = Request.QueryString["Date"];
                this.tblinvo.Text = Request.QueryString["Invono"];


            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                this.pnlupdate.Visible = true;
                string tblinvo = (this.tblinvo.Text) + "%";
                this.ibtnvounu_Click(null, null);

                DataSet ds2 = AIData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETAIVOUNUM", tblinvo, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }
                ViewState["tblt01"] = ds2.Tables[0];

                this.GetData_Bound();





            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        private void GetData_Bound()
        {
            DataTable dt = (DataTable)ViewState["tblt01"];
            this.gvInvoApp.DataSource = dt;
            this.gvInvoApp.DataBind();
            this.FooterCalculation(dt);

        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            AIData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dramount)", "")) ? 0.00 :
                dt.Compute("sum(dramount)", ""))), 2);
            AIData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cramount)", "")) ? 0.00 :
                dt.Compute("sum(cramount)", ""))), 2);
            ((Label)this.gvInvoApp.FooterRow.FindControl("tbldrsum")).Text = (AIData.ToDramt).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvInvoApp.FooterRow.FindControl("tblcrsum")).Text = (AIData.ToCramt).ToString("#,##0.00;(#,##0.00); ");
        }



        protected void ibtnvounu_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                string comcod = this.GetCompCode();

                DataSet ds2 = AIData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate >= Convert.ToDateTime(this.txtfrmdate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.txtfrmdate.Text.Substring(0, 11).Trim();
                DataSet ds4 = AIData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtvounum1.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtvounum2.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception exp)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (AIData.ToDramt != AIData.ToCramt)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must be Equal Credit Amount";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string Terminal = hst["trmid"].ToString();
                string Sessionid = hst["session"].ToString();
                string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
               

                string invoiceno = this.tblinvo.Text;
                DataSet ds4 = AIData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGSAIVOUCHER", invoiceno, "", "", "", "", "", "", "", "");
                if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")

                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in Sale Journal";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                string voudat = this.txtfrmdate.Text.Substring(0, 11);
                string vounum = this.txtvounum1.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtvounum1.Text.Trim().Substring(2, 2) + this.txtvounum2.Text.Trim();
                string refnum = this.txtrefno.Text.Trim();
                string srinfo = this.txtotherif.Text;
                string vounarration1 = this.txtvounarr.Text.Trim();
                string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);

                string voutype = "Journal Voucher";
                string cactcode = "000000000000";
                string vtcode = "98";
                string edit = "";
                string schcode = "0000000";
                string EditByid = "";
                string Editdat = "01-Jan-1900";
                //string pounaction = "";
                string pounaction =  "";
                string aprovbyid = "";
                string aprvtrmid = "";
                string aprvseson = "";
                string aprvdat = "01-jan-1900";
                //this.txtPayto.Text.Trim() == ""
                string Payto = "";
                string isunum = "";
                string recndt = "01-Jan-1900";
                string rpcode = "";
                string rate = ds4.Tables[0].Rows[0]["rate"].ToString();
                string currency = ds4.Tables[0].Rows[0]["currency"].ToString();

                  //-----------Update Transaction B Table-----------------//
                    bool resultb = AIData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEACCOUNTVAUTCHER", vounum, voudat, refnum, srinfo,
                            vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, EditByid, Editdat, Payto, isunum, currency, rate,"","","","","","","");




                    if (!resultb)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = AIData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                //-----------Update Transaction A Table-----------------//


                for (int i = 0; i < gvInvoApp.Rows.Count; i++)
                {
                    string actcode = ((Label)this.gvInvoApp.Rows[i].FindControl("lblactcode")).Text.Trim();
                   
                    string trnqty = "0";
                    double Dramt = Convert.ToDouble("0" + ((Label)this.gvInvoApp.Rows[i].FindControl("lbldramount")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((Label)this.gvInvoApp.Rows[i].FindControl("lblcramount")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = "";
                    bool resulta = AIData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEACCOUNTVAUTCHER", vounum,
                            actcode, "", cactcode, voudat, trnqty, trnremarks, vtcode, trnamt,  recndt, rpcode, "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = AIData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                      resulta = AIData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSERTAIJOURNAL", vounum, invoiceno, "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = AIData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
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




            }
            catch (Exception exp)
            {

            }
        }
    }
}