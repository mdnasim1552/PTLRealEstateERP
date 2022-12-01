using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_16_Bill
{
    public partial class BillEntry : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                // this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                Session.Remove("tblBill");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetTrade();
                this.ImgbtnFindProject_OnClick(null, null);
                this.Loadsaleteam();

                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.lbtnPreList_Click(null, null);
                }
                // billno
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private void GetTrade()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETTRADENAME", "", "", "", "", "", "", "", "", "");


            // R/A No



            DataTable dt = ds1.Tables[1];
            DataView dv;

            switch (comcod)
            {

                default:
                    dv = dt.DefaultView;
                    break;




            }
            this.ddlRA.DataTextField = "radesc";
            this.ddlRA.DataValueField = "racode";
            this.ddlRA.DataSource = dv.ToTable();
            this.ddlRA.DataBind();
            ds1.Dispose();
            this.ddlRA_SelectedIndexChanged(null, null);

        }

        protected void GetIssuno(string init)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            if (init != "others")
            {
                this.txtDate.Text = Convert.ToDateTime(this.txtDate.Text.ToString().Trim()).ToString("dd-MMM-yyyy");
            }
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "GETBILLNO", this.txtDate.Text.ToString().Trim().Substring(0, 11), "",
                          "", "", "", "", "", "", "");
            this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["billno"].ToString().Trim().Substring(0, 3);
            this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["billno"].ToString().Trim().Substring(3, ds1.Tables[0].Rows[0]["billno"].ToString().Trim().Length - 3);

        }

        protected void lbtnOk1_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk1.Text == "New")
            {
                Session.Remove("tblBill");
                this.lbtnOk1.Text = "Ok";
                this.txtDate.Enabled = true;
                this.txtProjectSearch.Enabled = true;
                this.ImgbtnFindProject.Enabled = true;
                this.ddlProject.Visible = true;
                this.lblProjectDesc.Visible = false;
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                this.ddlPrevList.Items.Clear();
                this.lbtnPreList.Visible = true;
                this.ddlPrevList.Visible = true;

                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                this.lblpage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.PnlProRemarks.Visible = false;
                this.lblvalvounum.Text = "";
                this.clearTextField();
                return;
            }

            this.lbtnOk1.Text = "New";
            this.lbtnPreList.Visible = false;
            this.ddlPrevList.Visible = false;
            this.txtProjectSearch.Enabled = false;
            this.ImgbtnFindProject.Enabled = false;
            this.ddlProject.Visible = false;
            this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.lblProjectDesc.Width = this.ddlProject.Width;
            this.lblProjectDesc.Visible = true;
            this.lblpage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.PnlProRemarks.Visible = true;
            this.GetBillInfo();

        }

        private void clearTextField()
        {
            this.txtpercentage.Text = "";
            this.txtSDAmount.Text = "";
            this.txttaxpercentage.Text = "";
            this.txtTaxAmount.Text = "";
            this.txtvatpercentage.Text = "";
            this.txtvatAmount.Text = "";
            this.TxtAdvPer.Text = "";
            this.txtAdvanced.Text = "";
            this.txtDeduc.Text = "";

        }

        private void Loadsaleteam()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "CONSSALESTEAM", "", "", "", "", "", "", "", "", "");
            this.ddlSalesTeam.DataTextField = "empname";
            this.ddlSalesTeam.DataValueField = "empid";
            this.ddlSalesTeam.DataSource = ds1.Tables[0];
            this.ddlSalesTeam.DataBind();
        }
        private void GetBillInfo()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString();
            string mBILLNo = "NEWBILL";
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtDate.Enabled = false;
                // this.ddlRA.Enabled = false;
                mBILLNo = this.ddlPrevList.SelectedValue.ToString();
            }
            DataSet ds1 = new DataSet();
            ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "GETBILLINFO", mBILLNo, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblBill"] = this.HiddenSameData(ds1.Tables[0]);

            if (mBILLNo == "NEWBILL")
            {
                ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "GETBILLNO", CurDate1,
                       "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);
                }


                ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "GETPROBALINFO", pactcode, CurDate1, "",
                        "", "", "", "", "", "");
                Session["tblBill"] = this.HiddenSameData(ds1.Tables[0]);
                this.Data_Bind();
                return;
            }


            this.txtRefno.Text = ds1.Tables[1].Rows[0]["billrefno"].ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["billno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["billno1"].ToString().Substring(6, 5);
            this.txtDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd-MMM-yyyy");
            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.ddlRA.SelectedValue = ds1.Tables[1].Rows[0]["rano"].ToString();
            this.ddlSalesTeam.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
            // this.txtmbbook.Text = ds1.Tables[1].Rows[0]["mbbook"].ToString();

            this.txtpercentage.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.txttaxpercentage.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tpercntge"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.txtvatpercentage.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["vpercntge"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.TxtAdvPer.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["advpercntge"]).ToString("#,##0.00;(#,##0.00); ") + "%";

            this.txtTaxAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["taxamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtvatAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["vatamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtSDAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["sdamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtAdvanced.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtDeduc.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dedamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.lblvalvounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();

            this.Data_Bind();
            this.SaveDeposit();






        }


        protected void GetLastBillNo()
        {

            string comcod = this.GetCompCode();
            string mBillNO = "NEWBILL";
            if (this.ddlPrevList.Items.Count > 0)
                mBillNO = this.ddlPrevList.SelectedValue.ToString();
            string mREQDAT = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString();

            if (mBillNO == "NEWBILL")
            {
                DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "GETBILLNO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);
                    this.ddlPrevList.DataTextField = "maxbillno1";
                    this.ddlPrevList.DataValueField = "maxbillno";
                    this.ddlPrevList.DataSource = ds2.Tables[0];
                    this.ddlPrevList.DataBind();
                }
            }
        }


        private DataTable ExtractHiddenData(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return dt;

            int i = 0;
            string misircode = dt.Rows[0]["misircode"].ToString();

            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["misircode"].ToString() == misircode)
                {
                    dt.Rows[j]["misirdesc"] = dt.Rows[i]["misirdesc"];
                }
                else
                {
                    i = j;
                    misircode = dt.Rows[j]["misircode"].ToString();
                }
            }

            string flrcod = dt.Rows[0]["flrcod"].ToString();
            int n = 0;
            for (int k = 1; k < dt.Rows.Count; k++)
            {
                if (dt.Rows[k]["flrcod"].ToString() == flrcod)
                {
                    dt.Rows[k]["flrdes"] = dt.Rows[n]["flrdes"];
                }
                else
                {
                    n = k;
                    flrcod = dt.Rows[k]["flrcod"].ToString();
                }
            }
            return dt;
        }


        private DataTable HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;

            string flrcod = dt.Rows[0]["flrcod"].ToString();


            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["flrcod"].ToString() == flrcod)
                {
                    dt.Rows[j]["flrdes"] = "";
                }

                flrcod = dt.Rows[j]["flrcod"].ToString();

            }

            int i = 0;
            string misircode = dt.Rows[0]["misircode"].ToString();

            foreach (DataRow dr1 in dt.Rows)
            {
                if (i == 0)
                {
                    misircode = dr1["misircode"].ToString();
                    i++;
                    continue;
                }

                if (dr1["misircode"].ToString() == misircode)
                {

                    dr1["misirdesc"] = "";

                }

                misircode = dr1["misircode"].ToString();
            }
            return dt;
        }


        protected void ChangeDDLToText()
        {


        }
        protected void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblBill"];
            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptResBasis.DataSource = dt;
            this.gvRptResBasis.DataBind();

            if (dt.Rows.Count > 0)
                ((LinkButton)this.gvRptResBasis.FooterRow.FindControl("lnkfinalup")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");



            this.FooterCalCulation();
        }


        private void FooterCalCulation()
        {

            DataTable dt = (DataTable)Session["tblBill"];
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFordam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordam)", "")) ? 0.00 : dt.Compute("Sum(ordam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFprebam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(prebam)", "")) ? 0.00 : dt.Compute("Sum(prebam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFbillam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(billam)", "")) ? 0.00 : dt.Compute("Sum(billam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFbalam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(balam)", "")) ? 0.00 : dt.Compute("Sum(balam)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }

        protected void ImgbtnFindProject_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string srchTxt = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRJCODELIST", srchTxt, "", usrid, "", "", "", "", "", "");
            this.ddlProject.DataTextField = "prjdesc1";
            this.ddlProject.DataValueField = "prjcod";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();

        }

        private void SaveValue()
        {

            DataTable dt1 = (DataTable)Session["tblBill"];
            int TblRowIndex;
            double bgdqty, exeqty, prebqty, balqty, exosqty, sysqty, ordrate, billam, balam;
            string mbnumber, pagenumber;
            for (int i = 0; i < gvRptResBasis.Rows.Count; i++)
            {
                TblRowIndex = (gvRptResBasis.PageIndex) * gvRptResBasis.PageSize + i;
                double wrkqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvwrkqty")).Text.Trim());
                double percnt = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvpercnt")).Text.Trim());
                double dgvproqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvproqty")).Text.Trim());
                double dgvbillqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvbillqty")).Text.Trim());
                exeqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvexeqty")).Text.Trim());
                prebqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvprebqty")).Text.Trim());
                sysqty = exeqty - prebqty;
                //  isuqty = (percent > 0) ? (wrkqty > 0 ? wrkqty * percent * 0.01 : balqty * percent * 0.01) : dgvQty;

                double proqty = (percnt > 0) ? (wrkqty > 0 ? wrkqty : sysqty > 0 ? sysqty : 0.00) : dgvproqty;
                double billqty = (percnt > 0) ? (wrkqty > 0 ? wrkqty : sysqty > 0 ? sysqty : 0.00) : dgvbillqty;

                //double proqty = (percnt > 0) ? (wrkqty > 0 ? wrkqty * percnt * 0.01 : sysqty * percnt * 0.01) : dgvproqty;
                //double billqty = (percnt > 0) ? (wrkqty > 0 ? wrkqty * percnt * 0.01 : sysqty * percnt * 0.01) : dgvbillqty; 


                ordrate = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvbillrate")).Text.Trim());
                bgdqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvbgdqty")).Text.Trim());

                mbnumber = ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvmajbook")).Text.Trim();
                pagenumber = ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvpagenumber")).Text.Trim();

                billam = (percnt > 0) ? (billqty * ordrate * percnt * 0.01) : (billqty * ordrate);

                balqty = bgdqty - prebqty - billqty;
                exosqty = exeqty - prebqty - billqty;
                balam = balqty * ordrate;
                dt1.Rows[TblRowIndex]["balqty"] = balqty;
                dt1.Rows[TblRowIndex]["wrkqty"] = wrkqty;
                dt1.Rows[TblRowIndex]["percnt"] = percnt;
                dt1.Rows[TblRowIndex]["sysqty"] = sysqty;
                dt1.Rows[TblRowIndex]["proqty"] = proqty;
                dt1.Rows[TblRowIndex]["billqty"] = billqty;
                dt1.Rows[TblRowIndex]["exosqty"] = exosqty;
                dt1.Rows[TblRowIndex]["billam"] = billam;
                dt1.Rows[TblRowIndex]["balam"] = balam;
                dt1.Rows[TblRowIndex]["mbnumber"] = mbnumber;
                dt1.Rows[TblRowIndex]["pagenumber"] = pagenumber;
            }
            Session["tblBill"] = dt1;

        }

        protected void lnkfinalup_Click(object sender, EventArgs e)
        {

            try
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                this.SaveValue();
                DataTable dt1 = ((DataTable)Session["tblBill"]).Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("billam>0");
                dt1 = dv.ToTable();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                if (this.ddlPrevList.Items.Count == 0)
                {
                    this.GetLastBillNo();
                }
                string mbillno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
                string Refno = this.txtRefno.Text.Trim();


                string pactcode = this.ddlProject.SelectedValue.ToString();
                string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                string rano = this.ddlRA.SelectedValue.ToString();
                string narration = this.txtRemarks.Text.Trim();


                DataSet ds1 = new DataSet("ds1");
                ds1.Merge(dt1);
                ds1.Tables[0].TableName = "tbl1";

                string percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim()).ToString();
                string sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()).ToString();
                string tax = Convert.ToDouble("0" + this.txtTaxAmount.Text.Trim()).ToString();
                string vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim()).ToString();
                string advamt = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim()).ToString();
                string txtDeduc = Convert.ToDouble("0" + this.txtDeduc.Text.Trim()).ToString();

                string salesempid = this.ddlSalesTeam.SelectedValue.ToString();
                string tpercentage = Convert.ToDouble("0" + this.txttaxpercentage.Text.Replace("%", "").Trim()).ToString();
                string tvpercentage = Convert.ToDouble("0" + this.txtvatpercentage.Text.Replace("%", "").Trim()).ToString();
                string TxtAdvPer = Convert.ToDouble("0" + this.TxtAdvPer.Text.Replace("%", "").Trim()).ToString();

                bool result = ImpleData.UpdateXmlTransInfo(comcod, "SP_ENTRY_BILLMGT", "INSORUPEBILLINFAB", ds1, null, null, mbillno, pactcode, date, rano,
                       narration, Refno, percentage, sdamt, tax, vat, advamt, salesempid, tpercentage, tvpercentage, TxtAdvPer, txtDeduc);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                if (result == true)
                {
                    this.clearTextField();
                }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
        }

        protected void lbtnPreList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtDate.Text.Trim(); string projectname = this.ddlProject.SelectedValue.ToString();
            string srchoption = ((this.Request.QueryString["genno"].ToString().Length > 0) ? this.Request.QueryString["genno"].ToString() : "%") + "%";

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "GETPREBILLNO", date, srchoption, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevList.Items.Clear();
            this.ddlPrevList.DataTextField = "billno1";
            this.ddlPrevList.DataValueField = "billno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();
        }


        //protected void lnkPrint_Click(object sender, EventArgs e)
        //{
        //Hashtable hst = (Hashtable)Session["tblLogin"];
        //string comnam = hst["comnam"].ToString();
        //string comadd = hst["comadd1"].ToString();
        //string compname = hst["compname"].ToString();
        //string username = hst["username"].ToString();
        //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //RptImplementPlan rptstk = new RptImplementPlan();
        //DataTable dt1 = new DataTable();
        //dt1 = (DataTable)Session["tblImplemt"];
        ////DataTable dt2 = new DataTable();
        ////dt2 = (DataTable)Session["tblImplemtn"];
        //DataView dv1 = dt1.DefaultView;
        //dv1.RowFilter = "qty>0";

        //rptstk.SetDataSource(dv1);
        // TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
        //txtCompanyName.Text = comnam;
        //TextObject txtCompanyAddress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
        //txtCompanyAddress.Text = comadd;
        //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
        //txtprojectname.Text =this.ddlProject.SelectedItem.Text.Substring(14);
        //TextObject rpttxtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //rpttxtDate.Text ="Date: " +Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
        //TextObject rpttxtVou = rptstk.ReportDefinition.ReportObjects["txtVou"] as TextObject;
        //rpttxtVou.Text ="Voucher: "+ this.lblCurVOUNo1.Text.Trim()+this.txtCurVOUNo2.Text.Trim();



        //Session["Report1"] = rptstk;

        //this.lbljavascript.Text = @"<script>window.open('RptViewer.aspx');</script>";
        //dv1.RowFilter = "";




        //}

        protected void gvRptResBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvRptResBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();



        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.SaveValue();
            this.Data_Bind();
        }
        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();


        }


        protected void ddlRA_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtRefno.Text = this.ddlRA.SelectedItem.Text.Trim();
        }
        protected void gvRptResBasis_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {


                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //  gvrow.Cells.Remove(TableCell [0]);

                TableCell cell01 = new TableCell();
                cell01.Text = "Sl.No.";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Floor";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Work Description";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);


                TableCell cell04 = new TableCell();
                cell04.Text = "Unit";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.RowSpan = 2;
                gvrow.Cells.Add(cell04);


                TableCell cell05 = new TableCell();
                cell05.Text = "Quotation";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.Attributes["style"] = "font-weight:bold;";
                cell05.ColumnSpan = 3;
                gvrow.Cells.Add(cell05);





                TableCell cell08 = new TableCell();
                cell08.Text = "Executed Qty";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.RowSpan = 2;
                gvrow.Cells.Add(cell08);



                TableCell cell09a = new TableCell();
                cell09a.Text = "Previous Quantity";
                cell09a.HorizontalAlign = HorizontalAlign.Center;
                cell09a.RowSpan = 2;
                gvrow.Cells.Add(cell09a);


                TableCell cell09 = new TableCell();
                cell09.Text = "System Quantity";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.RowSpan = 2;
                gvrow.Cells.Add(cell09);

                //TableCell cell09 = new TableCell();
                //cell09.Text = "Process Qty";
                //cell09.HorizontalAlign = HorizontalAlign.Center;
                //cell09.RowSpan = 2;
                //gvrow.Cells.Add(cell09);






                TableCell cell11 = new TableCell();
                cell11.Text = "Current Bill";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.Attributes["style"] = "font-weight:bold;";
                cell11.ColumnSpan = 7;
                gvrow.Cells.Add(cell11);
                gvRptResBasis.Controls[0].Controls.AddAt(0, gvrow);



            }



            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{


            //    //GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //    ////  gvrow.Cells.Remove(TableCell [0]);

            //    //TableCell cell01 = new TableCell();
            //    //cell01.Text = "Sl.No.";
            //    //cell01.HorizontalAlign = HorizontalAlign.Center;
            //    //cell01.RowSpan = 2;
            //    //gvrow.Cells.Add(cell01);



            //    //TableCell cell02 = new TableCell();
            //    //cell02.Text = "Floor";
            //    //cell02.HorizontalAlign = HorizontalAlign.Center;
            //    //cell02.RowSpan = 2;
            //    //gvrow.Cells.Add(cell02);

            //    //TableCell cell03 = new TableCell();
            //    //cell03.Text = "Work Description";
            //    //cell03.HorizontalAlign = HorizontalAlign.Center;
            //    //cell03.RowSpan = 2;
            //    //gvrow.Cells.Add(cell03);


            //    //TableCell cell04 = new TableCell();
            //    //cell04.Text = "Unit";
            //    //cell04.HorizontalAlign = HorizontalAlign.Center;
            //    //cell04.RowSpan = 2;
            //    //gvrow.Cells.Add(cell04);


            //    //TableCell cell05 = new TableCell();
            //    //cell05.Text = "Order";
            //    //cell05.HorizontalAlign = HorizontalAlign.Center;
            //    //cell05.Attributes["style"] = "font-weight:bold;";
            //    //cell05.ColumnSpan = 3;
            //    //gvrow.Cells.Add(cell05);





            //    //TableCell cell08 = new TableCell();
            //    //cell08.Text = "Executed Qty";
            //    //cell08.HorizontalAlign = HorizontalAlign.Center;
            //    //cell08.RowSpan = 2;
            //    //gvrow.Cells.Add(cell08);



            //    //TableCell cell09a = new TableCell();
            //    //cell09a.Text = "Previous Quantity";
            //    //cell09a.HorizontalAlign = HorizontalAlign.Center;
            //    //cell09a.RowSpan = 2;
            //    //gvrow.Cells.Add(cell09a);


            //    //TableCell cell09 = new TableCell();
            //    //cell09.Text = "System Quantity";
            //    //cell09.HorizontalAlign = HorizontalAlign.Center;
            //    //cell09.RowSpan = 2;
            //    //gvrow.Cells.Add(cell09);

            //    ////TableCell cell09 = new TableCell();
            //    ////cell09.Text = "Process Qty";
            //    ////cell09.HorizontalAlign = HorizontalAlign.Center;            
            //    ////cell09.RowSpan = 2;
            //    ////gvrow.Cells.Add(cell09);






            //    //TableCell cell11 = new TableCell();
            //    //cell11.Text = "Current Bill";
            //    //cell11.HorizontalAlign = HorizontalAlign.Center;
            //    //cell11.Attributes["style"] = "font-weight:bold;";
            //    //cell11.ColumnSpan = 5;
            //    //gvrow.Cells.Add(cell11);
            //    //gvRptResBasis.Controls[0].Controls.AddAt(0, gvrow);
            //}



        }


        protected void gvRptResBasis_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{


            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //    //  gvrow.Cells.Remove(TableCell [0]);

            //    TableCell cell01 = new TableCell();
            //    cell01.Text = "Sl.No.";
            //    cell01.HorizontalAlign = HorizontalAlign.Center;
            //    cell01.RowSpan = 2;
            //    gvrow.Cells.Add(cell01);



            //    TableCell cell02 = new TableCell();
            //    cell02.Text = "Floor";
            //    cell02.HorizontalAlign = HorizontalAlign.Center;
            //    cell02.RowSpan = 2;
            //    gvrow.Cells.Add(cell02);

            //    TableCell cell03 = new TableCell();
            //    cell03.Text = "Work Description";
            //    cell03.HorizontalAlign = HorizontalAlign.Center;
            //    cell03.RowSpan = 2;
            //    gvrow.Cells.Add(cell03);


            //    TableCell cell04 = new TableCell();
            //    cell04.Text = "Unit";
            //    cell04.HorizontalAlign = HorizontalAlign.Center;
            //    cell04.RowSpan = 2;
            //    gvrow.Cells.Add(cell04);


            //    TableCell cell05 = new TableCell();
            //    cell05.Text = "Order";
            //    cell05.HorizontalAlign = HorizontalAlign.Center;
            //    cell05.Attributes["style"] = "font-weight:bold;";
            //    cell05.ColumnSpan = 3;
            //    gvrow.Cells.Add(cell05);





            //    TableCell cell08 = new TableCell();
            //    cell08.Text = "Executed Qty";
            //    cell08.HorizontalAlign = HorizontalAlign.Center;
            //    cell08.RowSpan = 2;
            //    gvrow.Cells.Add(cell08);



            //    TableCell cell09a = new TableCell();
            //    cell09a.Text = "Previous Quantity";
            //    cell09a.HorizontalAlign = HorizontalAlign.Center;
            //    cell09a.RowSpan = 2;
            //    gvrow.Cells.Add(cell09a);


            //    TableCell cell09 = new TableCell();
            //    cell09.Text = "System Quantity";
            //    cell09.HorizontalAlign = HorizontalAlign.Center;
            //    cell09.RowSpan = 2;
            //    gvrow.Cells.Add(cell09);

            //    //TableCell cell09 = new TableCell();
            //    //cell09.Text = "Process Qty";
            //    //cell09.HorizontalAlign = HorizontalAlign.Center;
            //    //cell09.RowSpan = 2;
            //    //gvrow.Cells.Add(cell09);






            //    TableCell cell11 = new TableCell();
            //    cell11.Text = "Current Bill";
            //    cell11.HorizontalAlign = HorizontalAlign.Center;
            //    cell11.Attributes["style"] = "font-weight:bold;";
            //    cell11.ColumnSpan = 5;
            //    gvrow.Cells.Add(cell11);
            //    gvRptResBasis.Controls[0].Controls.AddAt(0, gvrow);



            //}

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                // e.Row.Cells[15].Visible = false;
            }
        }



        protected void lbtnDepost_Click(object sender, EventArgs e)
        {
            this.SaveDeposit();
        }

        private void SaveDeposit()
        {

            if (((DataTable)Session["tblbill"]).Rows.Count == 0)
                return;
            // double taxableamt = (Method == "I") ? (amt / (1 + (.01 * Tax))) : amt;

            double amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)Session["tblbill"]).Compute("sum(billam)", "")) ? 0.00
                    : ((DataTable)Session["tblbill"]).Compute("sum(billam)", "")));
            double tax = Convert.ToDouble("0" + this.txtTaxAmount.Text.Trim());
            double vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim());
            double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            double Deduction = Convert.ToDouble("0" + this.txtDeduc.Text.Trim());

            // double netamt = (amount - tax - vat);

            double percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim());
            double sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            double taxpercentage = Convert.ToDouble("0" + this.txttaxpercentage.Text.Replace("%", "").Trim());
            double vatpercentage = Convert.ToDouble("0" + this.txtvatpercentage.Text.Replace("%", "").Trim());

            double advpercentage = Convert.ToDouble("0" + this.TxtAdvPer.Text.Replace("%", "").Trim());


            //  // Security Deposit
            //   tax = Convert.ToDouble("0" + this.txtTaxAmount.Text.Trim());
            //   vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim());
            ////  double netamt = (amount - tax - vat);
            this.txtSDAmount.Text = sdamt > 0 ? sdamt.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(amount * percentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");
            sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            double namtasec = amount - sdamt;
            double vatableamt = (namtasec / (1 + (.01 * vatpercentage)));
            this.txtvatAmount.Text = vat > 0 ? vat.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(vatableamt * vatpercentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");

            vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim());
            double namtasecavat = amount - sdamt - vat;

            this.txtTaxAmount.Text = tax > 0 ? tax.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(namtasecavat * taxpercentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");

            //  adv percentage
            this.txtAdvanced.Text = Advanced > 0 ? Advanced.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(amount * advpercentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");


            double fpercntage = (sdamt > 0) ? (amount > 0 ? ((Convert.ToDouble(this.txtSDAmount.Text) * 100) / amount) : 0.00) : percentage;
            this.txtpercentage.Text = fpercntage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";


            double fvatpercentage = (vat > 0) ? (vatableamt > 0 ? ((Convert.ToDouble(this.txtvatAmount.Text) * 100) / vatableamt) : 0.00) : vatpercentage;
            this.txtvatpercentage.Text = fvatpercentage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";

            double ftaxpercentage = (tax > 0) ? (namtasecavat > 0 ? ((Convert.ToDouble(this.txtTaxAmount.Text) * 100) / namtasecavat) : 0.00) : taxpercentage;
            this.txttaxpercentage.Text = ftaxpercentage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";


            double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            tax = Convert.ToDouble("0" + this.txtTaxAmount.Text.Trim());
            vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim());

            this.lblvalnettotal.Text = (amount - (security + tax + vat + Advanced + Deduction)).ToString("#,##0.00;(#,##0.00); ");



            //double amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)Session["tblbill"]).Compute("sum(billam)", "")) ? 0.00
            //       : ((DataTable)Session["tblbill"]).Compute("sum(billam)", "")));
            //double tax = Convert.ToDouble("0" + this.txtTaxAmount.Text.Trim());
            //double vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim());
            //// double netamt = (amount - tax - vat);

            //double percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim());
            //double sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            //double taxpercentage = Convert.ToDouble("0" + this.txttaxpercentage.Text.Replace("%", "").Trim());
            //double vatpercentage = Convert.ToDouble("0" + this.txtvatpercentage.Text.Replace("%", "").Trim());

            //this.txtTaxAmount.Text = tax > 0 ? tax.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(amount * taxpercentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");
            //this.txtvatAmount.Text = vat > 0 ? vat.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(amount * vatpercentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");
            //// Security Deposit
            //tax = Convert.ToDouble("0" + this.txtTaxAmount.Text.Trim());
            //vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim());
            //double netamt = (amount - tax - vat);
            //this.txtSDAmount.Text = sdamt > 0 ? sdamt.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(netamt * percentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");
            //double fpercntage = (sdamt > 0) ? (netamt > 0 ? ((Convert.ToDouble(this.txtSDAmount.Text) * 100) / netamt) : 0.00) : percentage;
            //this.txtpercentage.Text = fpercntage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";
            //double ftaxpercentage = (tax > 0) ? (amount > 0 ? ((Convert.ToDouble(this.txtTaxAmount.Text) * 100) / amount) : 0.00) : taxpercentage;
            //this.txttaxpercentage.Text = ftaxpercentage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";
            //double fvatpercentage = (vat > 0) ? (amount > 0 ? ((Convert.ToDouble(this.txtvatAmount.Text) * 100) / amount) : 0.00) : vatpercentage;
            //this.txtvatpercentage.Text = fvatpercentage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";
            //double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            //tax = Convert.ToDouble("0" + this.txtTaxAmount.Text.Trim());
            //vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim());
            //double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            //this.lblvalnettotal.Text = (amount - (security + tax + vat + Advanced)).ToString("#,##0.00;(#,##0.00); ");
        }



        protected void lnkputsubmit_Click(object sender, EventArgs e)
        {


            DataTable dt1 = (DataTable)Session["tblBill"];
            int TblRowIndex;
            double bgdqty, exeqty, prebqty, balqty, exosqty, sysqty, ordrate, billam, balam;
            string mbnumber, pagenumber;
            for (int i = 0; i < gvRptResBasis.Rows.Count; i++)
            {
                TblRowIndex = (gvRptResBasis.PageIndex) * gvRptResBasis.PageSize + i;
                double wrkqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvwrkqty")).Text.Trim());
                double percnt = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvpercnt")).Text.Trim());
                double dgvproqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvproqty")).Text.Trim());
                double dgvbillqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvbillqty")).Text.Trim());
                exeqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvexeqty")).Text.Trim());
                prebqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvprebqty")).Text.Trim());
                sysqty = exeqty - prebqty;
                //  isuqty = (percent > 0) ? (wrkqty > 0 ? wrkqty * percent * 0.01 : balqty * percent * 0.01) : dgvQty;

                double proqty = (percnt > 0) ? (wrkqty > 0 ? wrkqty : sysqty > 0 ? sysqty : 0.0) : dgvproqty;
                double billqty = proqty;



                //double proqty = (percnt > 0) ? (wrkqty > 0 ? wrkqty * percnt * 0.01 : sysqty * percnt * 0.01) : dgvproqty;
                //double billqty = (percnt > 0) ? (wrkqty > 0 ? wrkqty * percnt * 0.01 : sysqty * percnt * 0.01) : dgvbillqty; 


                ordrate = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvbillrate")).Text.Trim());
                bgdqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvbgdqty")).Text.Trim());

                mbnumber = ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvmajbook")).Text.Trim();
                pagenumber = ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvpagenumber")).Text.Trim();

                billam = (percnt > 0) ? (billqty * ordrate * percnt * 0.01) : (billqty * ordrate);

                balqty = bgdqty - prebqty - billqty;
                exosqty = exeqty - prebqty - billqty;
                balam = balqty * ordrate;
                dt1.Rows[TblRowIndex]["balqty"] = balqty;
                dt1.Rows[TblRowIndex]["wrkqty"] = wrkqty;
                dt1.Rows[TblRowIndex]["percnt"] = percnt;
                dt1.Rows[TblRowIndex]["sysqty"] = sysqty;
                dt1.Rows[TblRowIndex]["proqty"] = proqty;
                dt1.Rows[TblRowIndex]["billqty"] = billqty;
                dt1.Rows[TblRowIndex]["exosqty"] = exosqty;
                dt1.Rows[TblRowIndex]["billam"] = billam;
                dt1.Rows[TblRowIndex]["balam"] = balam;
                dt1.Rows[TblRowIndex]["mbnumber"] = mbnumber;
                dt1.Rows[TblRowIndex]["pagenumber"] = pagenumber;
            }
            Session["tblBill"] = dt1;
            this.Data_Bind();








            //DataTable dt1 = (DataTable)Session["tblBill"];
            //int TblRowIndex;
            //double bgdqty, billqty, exeqty, prebqty, balqty, exosqty, sysqty, ordrate, billam, balam;
            //int i = 0;
            //foreach (GridViewRow gv1 in gvRptResBasis.Rows)
            //{
            //    TblRowIndex = (gvRptResBasis.PageIndex) * gvRptResBasis.PageSize + i;

            //    double wrkqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvwrkqty")).Text.Trim());
            //    double percnt = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvpercnt")).Text.Trim());
            //    double dgvproqty = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvproqty")).Text.Trim());
            //    exeqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvexeqty")).Text.Trim());
            //    prebqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvprebqty")).Text.Trim());
            //    sysqty = exeqty - prebqty;
            //    double proqty = (percnt > 0) ? (wrkqty > 0 ? wrkqty * percnt * 0.01 : sysqty * percnt * 0.01) : dgvproqty;


            //    billqty = proqty;
            //    // double billqty = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvbillqty")).Text.Trim());
            //    ordrate = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvbillrate")).Text.Trim());
            //    bgdqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvbgdqty")).Text.Trim());
            //    exeqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvexeqty")).Text.Trim());
            //    prebqty = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvprebqty")).Text.Trim());
            //    billam = billqty * ordrate;
            //    sysqty = exeqty - prebqty;
            //    balqty = bgdqty - prebqty - billqty;
            //    exosqty = exeqty - prebqty - billqty;
            //    balam = balqty * ordrate;
            //    dt1.Rows[TblRowIndex]["wrkqty"] = wrkqty;
            //    dt1.Rows[TblRowIndex]["percnt"] = percnt;
            //    dt1.Rows[TblRowIndex]["balqty"] = balqty;
            //    dt1.Rows[TblRowIndex]["proqty"] = proqty;
            //    dt1.Rows[TblRowIndex]["sysqty"] = sysqty;
            //    dt1.Rows[TblRowIndex]["billqty"] = billqty;
            //    dt1.Rows[TblRowIndex]["exosqty"] = exosqty;
            //    dt1.Rows[TblRowIndex]["billam"] = billam;
            //    dt1.Rows[TblRowIndex]["balam"] = balam;
            //    i++;
            //}
            //Session["tblBill"] = dt1;
            //this.Data_Bind();


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string Project = this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string mBILLNo = this.ddlPrevList.SelectedValue.ToString();
            string RANO = this.ddlRA.SelectedItem.Text.ToString();

            double amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)Session["tblbill"]).Compute("sum(billam)", "")) ? 0.00
                  : ((DataTable)Session["tblbill"]).Compute("sum(billam)", "")));
            double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            double tax = Convert.ToDouble("0" + this.txtTaxAmount.Text.Trim());
            double vat = Convert.ToDouble("0" + this.txtvatAmount.Text.Trim());
            double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            double netamt = (amount - (security + tax + vat + Advanced));

            //.ToString("#,##0.00;(#,##0.00); ")
            string mbillno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            //string mbillno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            //string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            // DataTable dt = (DataTable)Session["tblBill"];


            DataTable dt = ((DataTable)Session["tblBill"]).Copy();

            //AddedBy Nime 
            DataTable dt2 = this.ExtractHiddenData(dt);

            //Commented By Nime 
            //DataView dv = dt.DefaultView;
            //Added By Nime 
            DataView dv = dt2.DefaultView;
            dv.RowFilter = ("billqty<>0");

            var lst = dv.ToTable().DataTableToList<RealEntity.C_16_Bill.BO_BillEntry.BillEmtry>();

            if (comcod == "1205" || comcod == "1101")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_16_Bill.RptBillInvoiceP2P", lst, null, null);
            }   
            else if (comcod == "1206" || comcod == "1207")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_16_Bill.RptBillInvoiceAcme", lst, null, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_16_Bill.RptBillInvoice", lst, null, null);
            }

            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_16_Bill.RptBillInvoice", lst, null, null);
            Rpt1.EnableExternalImages = true;
            //   Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Project", "Project Name: " + Project));
            Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
            Rpt1.SetParameters(new ReportParameter("mBILLNo", "Invoice No: " + mbillno));
            Rpt1.SetParameters(new ReportParameter("txtsecpercntge", "Security(" + this.txtpercentage.Text + ")"));


            Rpt1.SetParameters(new ReportParameter("txttaxpercntge", "Tax(" + this.txttaxpercentage.Text + ")"));


            Rpt1.SetParameters(new ReportParameter("txtvatpercntge", "Vat(" + this.txtvatpercentage.Text + ")"));
            Rpt1.SetParameters(new ReportParameter("txttaxamt", this.txtTaxAmount.Text));
            Rpt1.SetParameters(new ReportParameter("txtsecamt", this.txtSDAmount.Text));
            Rpt1.SetParameters(new ReportParameter("txtvatamt", this.txtvatAmount.Text));
            Rpt1.SetParameters(new ReportParameter("txtadvamt", this.txtAdvanced.Text));
            Rpt1.SetParameters(new ReportParameter("txtnetamt", this.lblvalnettotal.Text));


            //Rpt1.SetParameters(new ReportParameter("txttaxamt", tax.ToString()));
            //Rpt1.SetParameters(new ReportParameter("txtsecamt",security.ToString("#,##0.00;(#,##0.00); ")));
            //Rpt1.SetParameters(new ReportParameter("txtvatamt", vat.ToString()));
            //Rpt1.SetParameters(new ReportParameter("txtadvamt", Advanced.ToString()));
            //Rpt1.SetParameters(new ReportParameter("txtnetamt", netamt.ToString()));


            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Rpt1.SetParameters(new ReportParameter("rptname", "Upcon Bill Invoice"));
            Rpt1.SetParameters(new ReportParameter("RANO", "R/A No: " + RANO));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("InWrd", "In Word : " + ASTUtility.Trans(Math.Round(netamt), 2)));




            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}
