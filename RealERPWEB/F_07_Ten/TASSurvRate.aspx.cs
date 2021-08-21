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
using RealERPRPT;
namespace RealERPWEB.F_07_Ten
{

    public partial class TASSurvRate : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //this.lblTitle.Text = (this.Request.QueryString["Type"].ToString().Trim() == "Rate") ? "SUB-CONTRACTOR'S RATE INFORMATION INPUT/EDIT" : "CONSTRUCTION LEVEL INFORMATION INPUT/EDIT";
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "Rate" ? "SUB-CONTRACTOR'S RATE INFORMATION INPUT/EDIT" : "CONSTRUCTION LEVEL INFORMATION INPUT / EDIT");
            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();
            }
        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            // string comcod = hst["comcod"].ToString();
            //string serch1 = "%" + this.txtProjectSearch.Text.Trim() + "%";
            //DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            //this.ddlProjectName.DataTextField = "pactdesc";
            //this.ddlProjectName.DataValueField = "pactcode";
            //this.ddlProjectName.DataSource = ds1.Tables[0];
            //this.ddlProjectName.DataBind(); 
            //------------

            string comcod = hst["comcod"].ToString();
            string srchTxt = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "PRJCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblPrjCod"] = ds1.Tables[0];
            //Session["tblFlrCod"] = ds1.Tables[1];
            this.ddlProjectName.DataTextField = "prjdesc1";
            this.ddlProjectName.DataValueField = "prjcod";
            this.ddlProjectName.DataSource = (DataTable)Session["tblPrjCod"];
            this.ddlProjectName.DataBind();

        }

        protected void ImgbtnFindImpNo_OnClick(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        //protected void ImgbtnFindImpNo_OnClick(object sender, ImageClickEventArgs e)
        //{

        //}
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectName.Text = this.ddlProjectName.SelectedItem.Text.Trim();
                this.ddlProjectName.Visible = false;
                this.lblProjectName.Visible = true;
                //this.lgvPage.Visible = true;
                if (this.Request.QueryString["Type"].ToString().Trim() == "Level")
                {
                    this.chkUllock.Visible = true;
                }
                this.ddlpagesize.Visible = true;

                this.ShowView();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.ddlProjectName.Visible = true;
                this.lblProjectName.Visible = false;
                //this.lgvPage.Visible = false;
                this.ddlpagesize.Visible = false;
                if (this.Request.QueryString["Type"].ToString().Trim() == "Level")
                {
                    this.chkUllock.Visible = false;
                }
                this.gvSubRate.DataSource = null;
                this.gvSubRate.DataBind();
                this.gvConLevel.DataSource = null;
                this.gvConLevel.DataBind();
            }
        }
        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Rate":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowRate();
                    break;
                case "Level":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.Level();
                    break;
            }
        }

        private void ShowRate()
        {
            Session.Remove("tblSupRate");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string projectname = this.ddlProjectName.SelectedValue.ToString();

            //DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "BGDRATE", projectname, "", "", "", "", "", "", "", "");
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "GETGURVEYRATE", projectname, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvSubRate.DataSource = null;
                this.gvSubRate.DataBind();
                return;
            }
            Session["tblSupRate"] = ds2.Tables[0];
            this.LoadGrid("gvSubRate");

        }
        private void Level()
        {
            Session.Remove("tblSupRate");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string projectname = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "BGDRATE", projectname, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvConLevel.DataSource = null;
                this.gvConLevel.DataBind();
                return;
            }
            Session["tblSupRate"] = ds2.Tables[0];
            this.LoadGrid("gvConLevel");
        }
        private void LoadGrid(string gvname)
        {
            DataTable dt = (DataTable)Session["tblSupRate"];

            switch (gvname)
            {
                case "gvSubRate":

                    this.gvSubRate.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSubRate.DataSource = dt;
                    this.gvSubRate.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    double schamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ? 0.0 : dt.Compute("sum(schamt)", "")));
                    double srvamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saramt)", "")) ? 0.0 : dt.Compute("sum(saramt)", "")));
                    double billamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.0 : dt.Compute("sum(billamt)", "")));
                    ((Label)this.gvSubRate.FooterRow.FindControl("ftlblSurveyAmt")).Text = srvamt.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubRate.FooterRow.FindControl("ftlblBoqAmt")).Text = schamt.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubRate.FooterRow.FindControl("ftlbbillamtAmt")).Text = billamt.ToString("#,##0.00;(#,##0.00); ");


                    break;
                case "gvConLevel":
                    this.gvConLevel.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvConLevel.DataSource = dt;
                    this.gvConLevel.DataBind();
                    this.CheeckLevel();
                    break;
            }
        }

        private void CheeckLevel()
        {
            for (int i = 0; i < this.gvConLevel.Rows.Count; i++)
            {
                ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Enabled = ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Checked ? false : true;
            }
        }




        protected void lbtnPrintReport_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblSupRate"];

            //ReportDocument rptsale = new  RealERPRPT.R_04_Bgd.rptSubConRat();
            ReportDocument rptadj = new RealERPRPT.R_07_Ten.rptTASSurvRate();
            TextObject rptCname = rptadj.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rptCname.Text = comnam;

            //TextObject rptpactdesc = rptsale.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptpactdesc.Text = "Project Name: " + this.lblProjectName.Text;

            TextObject txtuserinfo = rptadj.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptadj.SetDataSource(dt);
            Session["Report1"] = rptadj;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>"; 
        }

        protected void gvSubRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvSubRate.PageIndex = e.NewPageIndex;
            this.LoadGrid("gvSubRate");

        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt1 = (DataTable)Session["tblSupRate"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode1 = this.ddlProjectName.SelectedValue.ToString();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                //string isircode = dt1.Rows[i]["isircode"].ToString().Trim();
                string mcod = dt1.Rows[i]["itmdesc"].ToString().Trim();
                string itemcod = mcod.Substring(0, 12);
                string flrcod = dt1.Rows[i]["flrcod"].ToString().Trim();
                string qty = dt1.Rows[i]["sarqty"].ToString().Trim();
                string amt = dt1.Rows[i]["sarrate"].ToString().Trim();
                //double subrat1 = Convert.ToDouble(dt1.Rows[i]["subconrat"].ToString().Trim());

                //bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATESUPRAT", pactcode1, isircode,
                //              flrcod, subrat, conlevel, "", "", "", "", "", "", "", "", "", "");

                bool result1 = ImpleData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "UPDATETASRATADJ", pactcode1, itemcod, qty, amt, flrcod, "", "", "", "", "", "", "", "", "", "");
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

        }

        protected void SaveValue()
        {
            DataTable dt1 = (DataTable)Session["tblSupRate"];
            int TblRowIndex;
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Rate":
                    for (int i = 0; i < this.gvSubRate.Rows.Count; i++)
                    {
                        TblRowIndex = (gvSubRate.PageIndex) * gvSubRate.PageSize + i;
                        double qty = Convert.ToDouble("0" + ((TextBox)this.gvSubRate.Rows[i].FindControl("txtgvSurvQty")).Text.Trim().Replace(",", ""));
                        double budgetrate = Convert.ToDouble("0" + ((Label)this.gvSubRate.Rows[i].FindControl("lblgvSRate")).Text.Trim().Replace(",", ""));
                        double rate = Convert.ToDouble("0" + ((TextBox)this.gvSubRate.Rows[i].FindControl("txtgvSurvRete")).Text.Trim().Replace(",", ""));
                        ((Label)this.gvSubRate.Rows[i].FindControl("lblgvSurvAmt")).Text = (qty * rate).ToString("#,##0.00;(#,##0.00); ");

                        dt1.Rows[TblRowIndex]["sarqty"] = qty;
                        dt1.Rows[TblRowIndex]["sarrate"] = rate;
                        dt1.Rows[TblRowIndex]["saramt"] = qty * rate;
                        dt1.Rows[TblRowIndex]["saramt"] = qty * rate;
                        dt1.Rows[TblRowIndex]["diffrat"] = rate - budgetrate;
                        dt1.Rows[TblRowIndex]["billamt"] = qty * (rate - budgetrate);

                    }

                    double schamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(schamt)", "")) ? 0.0 : dt1.Compute("sum(schamt)", "")));
                    double srvamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(saramt)", "")) ? 0.0 : dt1.Compute("sum(saramt)", "")));
                    double billamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(billamt)", "")) ? 0.0 : dt1.Compute("sum(billamt)", "")));
                    ((Label)this.gvSubRate.FooterRow.FindControl("ftlblSurveyAmt")).Text = srvamt.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubRate.FooterRow.FindControl("ftlblBoqAmt")).Text = schamt.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSubRate.FooterRow.FindControl("ftlbbillamtAmt")).Text = billamt.ToString("#,##0.00;(#,##0.00); ");
                    Session["tblSupRate"] = dt1;
                    LoadGrid("gvSubRate");
                    break;


            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gvname = (this.Request.QueryString["Type"] == "Rate") ? "gvSubRate" : (this.Request.QueryString["Type"] == "Level") ? "gvConLevel" : "";
            this.LoadGrid(gvname);
        }

        protected void lnkbtnUpdatelevel_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt1 = (DataTable)Session["tblSupRate"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode1 = this.ddlProjectName.SelectedValue.ToString();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string isircode = dt1.Rows[i]["isircode"].ToString().Trim();
                string flrcod = dt1.Rows[i]["flrcod"].ToString().Trim();
                string subrat = dt1.Rows[i]["subconrat"].ToString().Trim();
                string conlevel = dt1.Rows[i]["markitem"].ToString().Trim();
                bool conlevel1 = Convert.ToBoolean(dt1.Rows[i]["markitem"].ToString().Trim());
                bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATESUPRAT", pactcode1, isircode,
                      flrcod, subrat, conlevel, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    return;
                }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.chkUllock.Checked = false;
            this.chkUllock_CheckedChanged(null, null);
        }

        protected void gvConLevel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvConLevel.PageIndex = e.NewPageIndex;
            this.LoadGrid("gvConLevel");
        }
        protected void chkUllock_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUllock.Checked)
            {

                for (int i = 0; i < this.gvConLevel.Rows.Count; i++)
                {
                    ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < this.gvConLevel.Rows.Count; i++)
                {
                    ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Enabled = ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Checked ? false : true;
                }
            }
        }
        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblSupRate"];
            int i, index;

            if (((CheckBox)this.gvConLevel.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvConLevel.Rows.Count; i++)
                {
                    ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Enabled = (((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Checked) ? false : true;
                    ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Checked = true;
                    index = (this.gvConLevel.PageSize) * (this.gvConLevel.PageIndex) + i;
                    dt.Rows[i]["markitem"] = "True";
                }
            }

            else
            {
                for (i = 0; i < this.gvConLevel.Rows.Count; i++)
                {
                    ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Enabled = true;
                    ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Checked = false;
                    index = (this.gvConLevel.PageSize) * (this.gvConLevel.PageIndex) + i;
                    dt.Rows[i]["markitem"] = "False";

                }

            }

            Session["tblSupRate"] = dt;
        }
        protected void lnkbtnTotal_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = (DataTable)Session["tblSupRate"];
            //int TblRowIndex;
            //string type = this.Request.QueryString["Type"].ToString();

            //        for (int i = 0; i < this.gvSubRate.Rows.Count; i++)
            //        {
            //            TblRowIndex = (gvSubRate.PageIndex) * gvSubRate.PageSize + i;
            //            double qty = Convert.ToDouble("0" + ((TextBox)this.gvSubRate.Rows[i].FindControl("txtgvSurvQty")).Text.Trim().Replace(",",""));
            //            double rate = Convert.ToDouble("0" + ((TextBox)this.gvSubRate.Rows[i].FindControl("txtgvSurvRete")).Text.Trim().Replace(",",""));
            //            dt1.Rows[TblRowIndex]["sarqty"] = qty;
            //            dt1.Rows[TblRowIndex]["sarrate"] = rate;
            //            dt1.Rows[TblRowIndex]["saramt"] =qty * rate;
            //        }
            //        Session["tblSupRate"] = dt1;
            //        LoadGrid("gvSubRate");
            ////gvSubRate
            SaveValue();
        }


    }

}