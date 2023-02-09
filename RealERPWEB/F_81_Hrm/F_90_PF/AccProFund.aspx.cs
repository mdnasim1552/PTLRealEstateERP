
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
namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class AccProFund : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetCompany();
                this.GetYearMonth();


            }

            //   lbtnGenerate.Attributes.Add("onclick", "return IsDecimal('"+txtCompShare.ClientID+"');");
            // lbtnGenerate.Attributes.Add("onClick", "javascript:return IsDecimal('" + this.txtCompShare.Text.Trim().Replace("%", "") + "');");
            //  lbtnFinalUpdate.Attributes.Add("onClick",
            //" javascript:return confirm('You sure you want to Save the record?');");
        }


        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];

            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlyearmon.DataBind();
            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
            ds1.Dispose();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnOk(object sender, EventArgs e)
        {
            if (this.lbtnoK.Text == "Ok")
            {
                this.lbtnoK.Text = "New";
                //this.lblPage.Visible = true;
                //this.ddlpagesize.Visible = true;
                this.ddlCompany.Enabled = false;
                this.pnlNaration.Visible = true;
                this.chkcomshare.Visible = true;
                this.ddlyearmon.Enabled = false;

                this.ShowPFInfo();
            }
            else
            {
                this.lbtnoK.Text = "Ok";
                //this.lblPage.Visible = false;
                //this.ddlpagesize.Visible = false;
                this.ddlCompany.Enabled = true;
                this.pnlNaration.Visible = false;
                this.chkcomshare.Visible = false;
                this.ddlyearmon.Enabled = true;
                this.gvPfAcc.DataSource = null;
                this.gvPfAcc.DataBind();
                this.ClearScreen();

            }
        }
        private void ClearScreen()
        {
            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtRefNum.Text = "";
        }

        private void ShowPFInfo()
        {
            ViewState.Remove("tblpfacc");
            string comcod = this.GetComeCode();
            string Month = this.ddlyearmon.SelectedValue.ToString();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            // string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);


            DataSet ds5 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "PFACCOUNTINFO", Month, CompanyName, "", "", "", "", "", "", "");
            if (ds5 == null)
            {
                this.gvPfAcc.DataSource = null;
                this.gvPfAcc.DataBind();
                return;

            }
            ViewState["tblpfacc"] = this.HiddenSameData(ds5.Tables[0]);
            this.LoadGrid();

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            // DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string company = dt1.Rows[0]["company"].ToString();
            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            string sectionid = dt1.Rows[0]["sectionid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[0]["sectionid"].ToString() == sectionid)
                {
                    company = dt1.Rows[j]["company"].ToString();
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    sectionid = dt1.Rows[j]["sectionid"].ToString();
                    dt1.Rows[j]["companydesc"] = "";
                    dt1.Rows[j]["deptname"] = "";
                    dt1.Rows[j]["section"] = "";

                }

                else
                {


                    if (dt1.Rows[j]["company"].ToString() == company)
                    {
                        dt1.Rows[j]["companydesc"] = "";
                    }
                    if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                    {
                        dt1.Rows[j]["deptname"] = "";
                    }

                    if (dt1.Rows[j]["sectionid"].ToString() == sectionid)
                    {
                        dt1.Rows[j]["section"] = "";
                    }

                    company = dt1.Rows[j]["company"].ToString();
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    sectionid = dt1.Rows[j]["sectionid"].ToString();

                }

            }
            return dt1;


        }

        private void LoadGrid()
        {
            //this.gvPfAcc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPfAcc.DataSource = (DataTable)ViewState["tblpfacc"];
            this.gvPfAcc.DataBind();
            this.FooterCal((DataTable)ViewState["tblpfacc"]);


        }
        private void FooterCal(DataTable dt)
        {
            ((Label)this.gvPfAcc.FooterRow.FindControl("lgvFDr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dr)", "")) ? 0.00 :
                    dt.Compute("sum(dr)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPfAcc.FooterRow.FindControl("lgvFCr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cr)", "")) ? 0.00 :
                    dt.Compute("sum(cr)", ""))).ToString("#,##0;(#,##0); ");
            Session["Report1"] = gvPfAcc;
            ((HyperLink)this.gvPfAcc.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }


        protected void getVoucherNumber()
        {
            string comcod = this.GetComeCode();
            string VNo3 = "JV";
            string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
            this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
            this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);
            string pvno1 = ds4.Tables[1].Rows[0]["lastvounum"].ToString().Trim();
            //this.txtLastVou.Text = pvno1.Substring(0, 2) + pvno1.Substring(6, 2) + "-" + pvno1.Substring(8, 6);
        }
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblpfacc"];
            double shareamt = Convert.ToDouble("0" + this.txtCompShare.Text.Trim().Replace("%", ""));
            double cramt, itcramt;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cramt = Convert.ToDouble(dt.Rows[i]["cr"]);
                itcramt = cramt + (shareamt * cramt * 0.01);
                dt.Rows[i]["cr"] = itcramt;
            }
            ViewState["tblpfacc"] = dt;
            this.LoadGrid();
            this.chkcomshare.Checked = false;
            this.chkcomshare_CheckedChanged(null, null);



        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblpfacc"];
            double dramt, cramt;
            for (int i = 0; i < this.gvPfAcc.Rows.Count; i++)
            {
                dramt = Convert.ToDouble("0" + ((TextBox)this.gvPfAcc.Rows[i].FindControl("txtgvDr")).Text.Trim());
                cramt = Convert.ToDouble("0" + ((TextBox)this.gvPfAcc.Rows[i].FindControl("txtgvCr")).Text.Trim());
                string rmarks = ((TextBox)this.gvPfAcc.Rows[i].FindControl("txtgvrmarks")).Text.Trim();
                int rowindex = (this.gvPfAcc.PageSize) * (this.gvPfAcc.PageIndex) + i;
                dt.Rows[rowindex]["dr"] = dramt;
                dt.Rows[rowindex]["cr"] = cramt;
                dt.Rows[rowindex]["rmarks"] = rmarks;
            }
            ViewState["tblpfacc"] = dt;


        }
        //protected void gvPfAcc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.SaveValue();
        //    this.gvPfAcc.PageIndex = e.NewPageIndex;
        //    this.LoadGrid();
        //}
        //protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.SaveValue();
        //    this.LoadGrid();
        //}
        protected void chkcomshare_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlgen.Visible = this.chkcomshare.Checked;
        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)ViewState["tblpfacc"];
            this.getVoucherNumber();
            string comcod = this.GetComeCode();

            string voudat = this.txtdate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                            this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = (this.txtCurrntlast6.Enabled ? "" : "EDIT");
            double TgvDrAmt = Convert.ToDouble("0" + ((Label)this.gvPfAcc.FooterRow.FindControl("lgvFDr")).Text.Trim());
            double TgvCrAmt = Convert.ToDouble("0" + ((Label)this.gvPfAcc.FooterRow.FindControl("lgvFCr")).Text.Trim());
            string msg = "";
            if (vouno == "JV" && TgvDrAmt != TgvCrAmt)
            {
                msg = "Dr.Amount not equals to Cr.Amount.";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                return;
            }

            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, "", "", "", "", "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sectionid = dt.Rows[i]["company"].ToString();
                    string rescode = dt.Rows[i]["rescode"].ToString();
                    string spclcode = "000000000000";
                    string trnqty = "0";
                    double Dramt = Convert.ToDouble(dt.Rows[i]["dr"].ToString());
                    double Cramt = Convert.ToDouble(dt.Rows[i]["cr"].ToString());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = dt.Rows[i]["rmarks"].ToString();
                    bool resulta = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, sectionid, rescode, cactcode,
                                   voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
                msg = "Updated Successfully!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                ((LinkButton)this.gvPfAcc.FooterRow.FindControl("lbtnFinalUpdate")).Enabled = false;


            }
            catch (Exception ex)
            {

                msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }


        }
        protected void gvPfAcc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblpfacc"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (dt != null)
            {
                int rowindex = (this.gvPfAcc.PageSize) * (this.gvPfAcc.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }
            DataView dv = dt.DefaultView;

            ViewState["tblpfacc"] = dv.ToTable();
            this.LoadGrid();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.FooterCal((DataTable)ViewState["tblpfacc"]);
        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
