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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_09_PImp
{
    public partial class ImplementPlan : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.ImgbtnFindProject_Click(null, null);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Implementation Plan";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void GetImpPlanNo()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETVOUNO", this.txtDate.Text.ToString().Trim().Substring(0, 11), "", "", "", "", "", "", "", "");
            this.lblCurVOUNo1.Text = ds1.Tables[0].Rows[0]["vouno"].ToString().Trim().Substring(0, 3);
            this.txtCurVOUNo2.Text = ds1.Tables[0].Rows[0]["vouno"].ToString().Trim().Substring(3, ds1.Tables[0].Rows[0]["vouno"].ToString().Trim().Length - 3);

        }



        protected void lbtnOk1_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk1.Text == "New")
            {
                Session.Remove("tblImplemt");
                this.lbtnOk1.Text = "OK";
                this.txtProjectSearch.Enabled = true;
                this.ImgbtnFindProject.Enabled = true;
                this.ddlProject.Visible = true;
                this.lblProjectDesc.Visible = false;

                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                this.ddlPrevVOUList.Items.Clear();
                this.lbtnPrevVOUList.Visible = true;
                this.lblPreList.Visible = true;
                this.txtPreVouSearch.Visible = true;
                this.ddlPrevVOUList.Visible = true;
                this.txtDate.Enabled = true;

                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();

                this.Panel3.Visible = false;

                return;
            }
            this.lbtnOk1.Text = "New";
            this.lbtnPrevVOUList.Visible = false;
            this.lblPreList.Visible = false;
            this.ddlPrevVOUList.Visible = false;
            this.txtPreVouSearch.Visible = false;
            this.txtProjectSearch.Enabled = false;
            this.ImgbtnFindProject.Enabled = false;
            this.ddlProject.Visible = false;
            this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.lblProjectDesc.Width = this.ddlProject.Width;
            this.lblProjectDesc.Visible = true;
            this.txtDate.Enabled = true;
            this.Panel3.Visible = true;
            this.GetImpPlanNo();
            this.GetFloorCode();
            this.ShowImplementationPlan();


        }


        private void GetFloorCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString().Trim();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEFLRLIST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlfloorno.DataTextField = "flrdes";
            this.ddlfloorno.DataValueField = "flrcod";
            this.ddlfloorno.DataSource = ds1.Tables[0];
            this.ddlfloorno.DataBind();
            this.ddlfloorno_SelectedIndexChanged(null, null);

        }
        //protected string GetStdDate(string Date1)
        //{
        //    Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
        //    string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
        //    return Date1;
        //}

        private void GetItemList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string flrcode = this.ddlfloorno.SelectedValue.ToString();
            string date = (this.txtDate.Text.Trim());
            string txtsrchItem = this.txtsrchItemName.Text.Trim() + "%";

            string ItemSearch = "%" + this.txtSearchItem.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "RPTWRKBASIS", pactcode, flrcode, "12", date, ItemSearch, "", "", "", "");

            //DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETITEMRESFLRCODE", pactcode, date, flrcode, txtsrchItem, "", "", "", "", "");
            Session["itemlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlitemlist.DataTextField = "rptdesc1";
            this.ddlitemlist.DataValueField = "rptcod";
            this.ddlitemlist.DataSource = ds1.Tables[0];
            this.ddlitemlist.DataBind();
            //ddlitemlist_SelectedIndexChanged(null, null);
        }

        protected void ddlitemlist_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string Worklists = this.ddlitemlist.SelectedValue.ToString();

            //DataTable dt = ((DataTable)Session["itemlist"]).Copy();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("rptcod= " + Worklists);
            //dt = dv.ToTable(true, "flrcod", "flrdes");


            //this.ddlfloorno.DataTextField = "flrdes";
            //this.ddlfloorno.DataValueField = "flrcod";
            //this.ddlfloorno.DataSource = dt;
            //this.ddlfloorno.DataBind();
            //this.DropCheck1.DataTextField = "flrdes1";
            //this.DropCheck1.DataValueField = "flrdes1";
            //this.DropCheck1.DataSource = dt;
            //this.DropCheck1.DataBind();

        }
        private void ShowImplementationPlan()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = null;
            string date = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string ItemSearch = "%" + this.txtSearchItem.Text.Trim() + "%";
            string flrcode = this.ddlfloorno.SelectedValue.ToString();

            string prevvouno = this.ddlPrevVOUList.SelectedValue.ToString().Trim();
            ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETINFOBYVOUNO", prevvouno, "AAA", "12", date, ItemSearch, "", "", "", "");

            if (this.ddlPrevVOUList.Items.Count > 0)
            {
                //prevvouno = this.ddlPrevVOUList.SelectedValue.ToString().Trim();
                // ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETINFOBYVOUNO", prevvouno, "AAA", "12", date, ItemSearch, "", "", "", "");

                string vouno = this.ddlPrevVOUList.SelectedValue.ToString().Trim();
                this.txtCurVOUNo2.Text = vouno.Substring(3, vouno.Length - 3);
                this.txtDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["impdate"]).ToString("dd-MMM-yyyy");
                this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["bldcod"].ToString();
                this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
                this.txtDate.Enabled = false;

            }
            //else
            //{

            //    string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();
            //    ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "RPTWRKBASIS", PrjCod, "AAA", "12", date, ItemSearch, "", "", "", "");

            // }
            Session["tblImplemt"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;

            string flrcod = dt1.Rows[0]["flrcod"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["flrcod"].ToString() == flrcod)
                    dt1.Rows[j]["flrdes"] = "";

                flrcod = dt1.Rows[j]["flrcod"].ToString();
            }

            return dt1;

        }
        protected void ImgbtnItemSearch_Click(object sender, EventArgs e)
        {
            this.ShowImplementationPlan();
        }


        protected void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblImplemt"];
            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptResBasis.DataSource = dt;
            this.gvRptResBasis.DataBind();
            if (dt.Rows.Count == 0)
                return;
            double mSUMAM = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ?
            0.00 : dt.Compute("sum(rptamt)", "")));
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lgvFAmt")).Text = mSUMAM.ToString("#,##0.00;(#,##0.00); ");

        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string srchTxt = "%" + this.txtProjectSearch.Text.Trim() + "%";

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRJCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblPrjCod"] = ds1.Tables[0];
            //Session["tblFlrCod"] = ds1.Tables[1];
            this.ddlProject.DataTextField = "prjdesc1";
            this.ddlProject.DataValueField = "prjcod";
            this.ddlProject.DataSource = (DataTable)Session["tblPrjCod"];
            this.ddlProject.DataBind();

            this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        }

        protected void lnktotal_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblImplemt"];
            double dtotalamt = 0;
            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            int TblRowIndex;
            for (int i = 0; i < gvRptResBasis.Rows.Count; i++)
            {
                TblRowIndex = (gvRptResBasis.PageIndex) * gvRptResBasis.PageSize + i;
                double totalqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtcurqty")).Text.Trim());

                dt1.Rows[TblRowIndex]["qty"] = totalqty;
                double totalrat = Convert.ToDouble("0" + ((Label)this.gvRptResBasis.Rows[i].FindControl("lblgvRptRat1")).Text.Trim());

                dtotalamt = totalqty * totalrat;
                dt1.Rows[TblRowIndex]["rptamt"] = dtotalamt;
            }
            Session["tblImplemt"] = dt1;
            this.Data_Bind();
        }

        protected void GetPlanNo()
        {
            string comcod = this.GetCompCode();
            string mWEPNO = "NEWWEP";
            if (this.ddlPrevVOUList.Items.Count > 0)
                mWEPNO = this.ddlPrevVOUList.SelectedValue.ToString();

            string mWEPDAT = this.txtDate.Text.Trim();
            if (mWEPNO == "NEWWEP")
            {
                DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETVOUNO", mWEPDAT,
                       "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurVOUNo1.Text = ds1.Tables[0].Rows[0]["vouno"].ToString().Trim().Substring(0, 3);
                    this.txtCurVOUNo2.Text = ds1.Tables[0].Rows[0]["vouno"].ToString().Trim().Substring(3, ds1.Tables[0].Rows[0]["vouno"].ToString().Trim().Length - 3);
                    this.ddlPrevVOUList.DataTextField = "vouno1";
                    this.ddlPrevVOUList.DataValueField = "vouno";
                    this.ddlPrevVOUList.DataSource = ds1.Tables[0];
                    this.ddlPrevVOUList.DataBind();
                }
            }

        }


        protected void lnkfinalup_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.lnktotal_Click(null, null);
            DataTable dt1 = (DataTable)Session["tblImplemt"];
            if (this.ddlPrevVOUList.Items.Count == 0)
                this.GetPlanNo();
            string comcod = this.GetCompCode();
            string vouno = this.lblCurVOUNo1.Text.ToString().Trim() + this.txtCurVOUNo2.Text.ToString().Trim();
            string pactcode1 = this.ddlProject.SelectedValue.ToString();
            //bool result1 = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEIMPPLAN", vouno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            string impdate = this.txtDate.Text.ToString().Trim().Substring(0, 11);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                string flrcod = dt1.Rows[i]["flrcod"].ToString().Trim();
                string pactcode = dt1.Rows[i]["bldcod"].ToString().Trim();
                string isircode = dt1.Rows[i]["rptcod"].ToString().Trim();
                string unit = dt1.Rows[i]["rptunit"].ToString().Trim();
                string tolqty = dt1.Rows[i]["rptqty"].ToString().Trim();
                string rate = dt1.Rows[i]["rptrat"].ToString().Trim();
                string balqty = dt1.Rows[i]["balqty"].ToString();
                double rptWrkQty = Convert.ToDouble((dt1.Rows[i]["qty"].ToString().Trim()).Trim());
                string wrkqty = (dt1.Rows[i]["qty"].ToString().Trim()).Trim();
                string wrkamt = (dt1.Rows[i]["rptamt"].ToString().Trim()).Trim();

                if (rptWrkQty > 0)
                {
                    bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEIMPLEMT", vouno, flrcod, pactcode1, isircode,
                       unit, tolqty, rate, balqty, wrkqty, wrkamt, impdate, "", "", "", "");
                }


            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Date Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.txtDate.Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Implement Plan";
                string eventdesc = "Update Resource";
                string eventdesc2 = vouno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void lbtnPrevVOUList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.txtDate.Text.Trim().Substring(0, 11);
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVVOULIST", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevVOUList.Items.Clear();
            this.ddlPrevVOUList.DataTextField = "vouno1";
            this.ddlPrevVOUList.DataValueField = "vouno";
            this.ddlPrevVOUList.DataSource = ds1.Tables[0];
            this.ddlPrevVOUList.DataBind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblImplemt"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "qty>0";
            DataTable dt1 = dv1.ToTable();

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_09_PIMP.EClassExecution.MonthlyPlan>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptImplemenPlan", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProject.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtdate", "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("ImplementNo", "Implement No : " + this.lblCurVOUNo1.Text.Trim() + "-" + this.txtCurVOUNo2.Text.Trim().Substring(4, 2) + "-" + this.txtCurVOUNo2.Text.Trim().Substring(6, 5)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Implementation Plan"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new RealERPRPT.R_09_PImp.RptImplementPlan();
            //DataTable dt1 = new DataTable();
            //dt1 = (DataTable)Session["tblImplemt"];
            ////DataTable dt2 = new DataTable();
            ////dt2 = (DataTable)Session["tblImplemtn"];
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = "qty>0";

            //rptstk.SetDataSource(dv1);
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            ////TextObject txtCompanyAddress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtCompanyAddress.Text = comadd;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //txtprojectname.Text ="Project Name : "+ this.ddlProject.SelectedItem.Text.Substring(14);
            //TextObject rpttxtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text = "Date : " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //TextObject rpttxtVou = rptstk.ReportDefinition.ReportObjects["txtVou"] as TextObject;
            //rpttxtVou.Text = "Voucher : " + this.lblCurVOUNo1.Text.Trim() + "-" + this.txtCurVOUNo2.Text.Trim().Substring(4, 2) + "-" + this.txtCurVOUNo2.Text.Trim().Substring(6, 5);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Implement Plan";
            //    string eventdesc = "Print Plan Report";
            //    string eventdesc2 = "Voucher: " + this.lblCurVOUNo1.Text.Trim() + this.txtCurVOUNo2.Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //dv1.RowFilter = "";




        }

        protected void gvRptResBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lnktotal_Click(null, null);
            this.gvRptResBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnktotal_Click(null, null);
            this.Data_Bind();

        }
        protected void lbtnPrevVOUList_Click1(object sender, ImageClickEventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.txtDate.Text.Trim().Substring(0, 11);
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVVOULIST", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevVOUList.Items.Clear();
            this.ddlPrevVOUList.DataTextField = "vouno1";
            this.ddlPrevVOUList.DataValueField = "vouno";
            this.ddlPrevVOUList.DataSource = ds1.Tables[0];
            this.ddlPrevVOUList.DataBind();


        }

        protected void gvRptResBasis_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblImplemt"];
            string vouno = this.lblCurVOUNo1.Text.ToString().Trim() + this.txtCurVOUNo2.Text.ToString().Trim();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string ItemCode = ((Label)this.gvRptResBasis.Rows[e.RowIndex].FindControl("lblgvItemCode")).Text.Trim();
            string Flrcode = ((Label)this.gvRptResBasis.Rows[e.RowIndex].FindControl("lblgvFloorCode")).Text.Trim();

            bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEPLANITEME", vouno, pactcode, ItemCode, Flrcode, "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvRptResBasis.PageSize) * (this.gvRptResBasis.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblImplemt");
            Session["tblImplemt"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblImplemt"];
            string vouno = this.lblCurVOUNo1.Text.ToString().Trim() + this.txtCurVOUNo2.Text.ToString().Trim();

            bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEPLANITEMEALL", vouno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
                return;
            DataView dv = dt.DefaultView;
            Session.Remove("tblImplemt");
            Session["tblImplemt"] = dv.ToTable();
            this.Data_Bind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "Date Delete Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        protected void ddlfloorno_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetItemList();
        }

        protected void imgbtnSearchItemList_Click(object sender, EventArgs e)
        {

            this.GetItemList();
        }

        protected void lbtnAllLab_Click(object sender, EventArgs e)
        {

            //this.LoopForSession();
            // DataTable dt=Session["itemlist"]
            string floorcode = this.ddlfloorno.SelectedValue.ToString();
            string selecteditem = this.ddlitemlist.SelectedValue.ToString().Trim();
            DataTable itemtable = (DataTable)Session["itemlist"];
            DataTable dt = (DataTable)Session["tblImplemt"];
            // DataTable tempforgrid = (DataTable)Session["sessionforgrid"];
            DataRow[] dr1 = dt.Select("flrcod='" + floorcode + "'  and rptcod='" + selecteditem + "'");
            if (dr1.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();
                drforgrid["flrcod"] = this.ddlfloorno.SelectedValue.ToString();
                drforgrid["flrdes"] = this.ddlfloorno.SelectedItem.Text.Trim();
                drforgrid["rptdesc"] = itemtable.Select("rptcod='" + selecteditem + "'")[0]["rptdesc"];
                drforgrid["rptunit"] = itemtable.Select("rptcod='" + selecteditem + "'")[0]["rptunit"];
                drforgrid["rptqty"] = itemtable.Select("rptcod='" + selecteditem + "'")[0]["rptqty"];
                drforgrid["comqty"] = itemtable.Select("rptcod='" + selecteditem + "'")[0]["comqty"];
                drforgrid["balqty"] = itemtable.Select("rptcod='" + selecteditem + "'")[0]["balqty"];

                drforgrid["rptrat"] = itemtable.Select("rptcod='" + selecteditem + "'")[0]["rptrat"];
                drforgrid["trqty"] = itemtable.Select("rptcod='" + selecteditem + "'")[0]["trqty"];
                drforgrid["qty"] = 0;
                drforgrid["rptamt"] = itemtable.Select("rptcod='" + selecteditem + "'")[0]["rptamt"];
                //drforgrid["balqty"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + selecteditem + "'"))[0]["balqty"];
                //drforgrid["wrkunit"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + selecteditem + "'"))[0]["wrkunit"];
                drforgrid["rptcod"] = this.ddlitemlist.SelectedValue.ToString();
                drforgrid["rptdesc1"] = this.ddlitemlist.SelectedItem.ToString().Trim();
                dt.Rows.Add(drforgrid);


            }
            Session["tblImplemt"] = dt;
            this.Data_Bind();

        }

        //private void LoopForSession()
        //{
        //    DataTable dt = (DataTable)Session["tblImplemt"];
        //    int TblRowIndex;
        //    for (int i = 0; i < this.gvRptResBasis.Rows.Count; i++)
        //    {
        //        double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtcurqty")).Text.Trim());
        //        TblRowIndex = (gvRptResBasis.PageIndex) * gvRptResBasis.PageSize + i;
        //        dt.Rows[TblRowIndex]["qty"] = txtwrkqty;
        //    }
        //    Session["tblImplemt"] = dt;
        //}


    }
}
