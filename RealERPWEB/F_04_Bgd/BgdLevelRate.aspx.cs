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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_04_Bgd
{

    public partial class BgdLevelRate : System.Web.UI.Page
    {

        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Rate") ? "SUB-CONTRACTOR'S RATE INFORMATION INPUT/EDIT" : "CONSTRUCTION LEVEL INFORMATION INPUT/EDIT";

                this.SectionView();

            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();
            }

            string qprjcode = this.Request.QueryString["prjcode"] ?? "";
            if (qprjcode.Length > 0)
            {

                this.lbtnOk_Click(null, null);
            }



        }



        private void SectionView()
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Rate":
                    this.MultiView1.ActiveViewIndex = 0;

                    break;
                case "Level":
                    this.MultiView1.ActiveViewIndex = 1;

                    break;

                case "ItemLock":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "ItemLock02":
                    this.lbltxtprojectname.Visible = false;
                    this.txtProjectSearch.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.ImgbtnFindImpNo.Visible = false;
                    this.MultiView1.ActiveViewIndex = 3;

                    break;
            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintReport_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            string qprjcode = this.Request.QueryString["prjcode"] ?? "";
            this.ddlProjectName.SelectedValue = qprjcode;
        }

        protected void ImgbtnFindImpNo_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectName.Text = this.ddlProjectName.SelectedItem.Text.Trim();
                this.ddlProjectName.Visible = this.Request.QueryString["Type"].ToString() == "ItemLock02" ? false : false;
                this.lblProjectName.Visible = this.Request.QueryString["Type"].ToString() == "ItemLock02" ? false : true;

                this.lbltxtprojectname.Visible = this.Request.QueryString["Type"].ToString() == "ItemLock02" ? false : true;
                this.ImgbtnFindImpNo.Visible = this.Request.QueryString["Type"].ToString() == "ItemLock02" ? false : true;



                this.lgvPage.Visible = true;
                if (this.Request.QueryString["Type"].ToString().Trim() == "Level" || this.Request.QueryString["Type"].ToString().Trim() == "ItemLock")
                {
                    this.chkUllock.Visible = true;
                }
                this.ddlpagesize.Visible = true;
                this.ShowView();


            }
            else
            {
                this.lbtnOk.Text = "Ok";

                this.ddlProjectName.Visible = true;
                this.lblProjectName.Visible = false;



                this.ddlProjectName.Visible = this.Request.QueryString["Type"].ToString() == "ItemLock02" ? false : true;
                this.lblProjectName.Visible = this.Request.QueryString["Type"].ToString() == "ItemLock02" ? false : false;

                this.lbltxtprojectname.Visible = this.Request.QueryString["Type"].ToString() == "ItemLock02" ? false : true;
                this.ImgbtnFindImpNo.Visible = this.Request.QueryString["Type"].ToString() == "ItemLock02" ? false : true;


                this.lgvPage.Visible = false;
                this.ddlpagesize.Visible = false;
                if (this.Request.QueryString["Type"].ToString().Trim() == "Level" || this.Request.QueryString["Type"].ToString().Trim() == "ItemLock")
                {
                    this.chkUllock.Visible = false;
                }
                this.gvSubRate.DataSource = null;
                this.gvSubRate.DataBind();
                this.gvConLevel.DataSource = null;
                this.gvConLevel.DataBind();
                this.gvItemlk.DataSource = null;
                this.gvItemlk.DataBind();
                this.MultiView1.ActiveViewIndex = -1;
            }
        }
        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Rate":
                    this.ShowRate();
                    break;
                case "Level":
                    this.Level();
                    break;

                case "ItemLock":

                    this.ItemLock();
                    break;

                case "ItemLock02":
                    this.ItemLock02();
                    break;
            }
        }

        private void ShowRate()
        {
            Session.Remove("tblSupRate");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string projectname = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "BGDRATE", projectname, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvSubRate.DataSource = null;
                this.gvSubRate.DataBind();
                return;
            }
            Session["tblSupRate"] = this.HiddenSameData(ds2.Tables[0]);
            this.LoadGrid("gvSubRate");

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Rate":
                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            rsircode = dt1.Rows[j]["rsircode"].ToString();
                            dt1.Rows[j]["rsirdesc"] = "";
                        }

                        rsircode = dt1.Rows[j]["rsircode"].ToString();
                    }

                    break;
                case "Level":
                case "ItemLock":
                    string flrcod = dt1.Rows[0]["flrcod"].ToString();
                    //string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["flrcod"].ToString() == flrcod)
                        {
                            dt1.Rows[j]["flrdes"] = "";

                        }

                        flrcod = dt1.Rows[j]["flrcod"].ToString();




                    }

                    break;
            }




            return dt1;
        }


        private void Level()
        {
            Session.Remove("tblSupRate");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string projectname = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "BGDCONLEVEL", projectname, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvConLevel.DataSource = null;
                this.gvConLevel.DataBind();
                return;
            }
            Session["tblSupRate"] = HiddenSameData(ds2.Tables[0]);
            this.LoadGrid("gvConLevel");
        }

        private void ItemLock()
        {
            Session.Remove("tblSupRate");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string projectname = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "BGDCONLEVEL", projectname, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvItemlk.DataSource = null;
                this.gvItemlk.DataBind();
                return;
            }
            Session["tblSupRate"] = HiddenSameData(ds2.Tables[0]);
            this.LoadGrid("gvItemlk");
        }


        private void ItemLock02()
        {
            Session.Remove("tblSupRate");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "ITEMLOCK", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvitemlk02.DataSource = null;
                this.gvitemlk02.DataBind();
                return;
            }
            Session["tblSupRate"] = ds2.Tables[0];
            this.LoadGrid("gvitemlk02");


        }


        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string flrcod = dt1.Rows[0]["flrcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["flrcod"].ToString() == flrcod)
                    dt1.Rows[j]["flrdes"] = "";
                flrcod = dt1.Rows[j]["flrcod"].ToString();

            }

            return dt1;

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
                    break;
                case "gvConLevel":
                    this.gvConLevel.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvConLevel.DataSource = dt;
                    this.gvConLevel.DataBind();
                    this.CheeckLevel();
                    break;
                case "gvItemlk":
                    this.gvItemlk.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvItemlk.DataSource = dt;
                    this.gvItemlk.DataBind();
                    this.CheeckItemLevel();
                    break;

                case "gvitemlk02":
                    this.gvitemlk02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvitemlk02.DataSource = dt;
                    this.gvitemlk02.DataBind();
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


        private void CheeckItemLevel()
        {
            for (int i = 0; i < this.gvItemlk.Rows.Count; i++)
            {
                ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Enabled = ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Checked ? false : true;
            }
        }



        protected void lbtnPrintReport_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Rate":
                    this.SubConRatRpt();
                    break;
                case "Level":
                    this.ColLevelRpt();
                    break;
            }

        }
        private void SubConRatRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectname = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "RPTBGDRATE", projectname, "", "", "", "", "", "", "", "");
            DataTable dt = ds2.Tables[0];

            var list = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.WorkvsRes>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.rptSubConRat", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Sub Contractor Rate"));
            Rpt1.SetParameters(new ReportParameter("projectName", this.lblProjectName.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sub-Contrator Rate";
                string eventdesc = "Sub-Contrator Rate Print";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void ColLevelRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblSupRate"];

            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["tblSupRate"];

            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "markitem=True";
            DataTable dt2 = dv1.ToTable();

            var list = dt2.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.WorkvsRes>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptConLavel", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Construction Level Selection"));
            Rpt1.SetParameters(new ReportParameter("projectName", this.lblProjectName.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Level";
                string eventdesc = "Constraction Level Print";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void gvSubRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvSubRate.PageIndex = e.NewPageIndex;
            this.LoadGrid("gvSubRate");

        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.SaveValue();
            DataTable dt1 = (DataTable)Session["tblSupRate"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                string flrcod = dt1.Rows[i]["flrcod"].ToString().Trim();
                string rsircode = dt1.Rows[i]["rsircode"].ToString().Trim();
                string subrat = dt1.Rows[i]["subconrat"].ToString().Trim();
                string workrat = dt1.Rows[i]["workrat"].ToString().Trim();
                //double subrat1 = Convert.ToDouble(dt1.Rows[i]["subconrat"].ToString().Trim());

                bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "INSERTORUPBGDSRAT", pactcode,
                              flrcod, rsircode, subrat, workrat, "", "", "", "", "", "", "", "", "", "");
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sub-Contrator Rate";
                string eventdesc = "Sub-Contrator Rate Update";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void SaveValue()
        {
            DataTable dt1 = (DataTable)Session["tblSupRate"];
            int TblRowIndex;
            int i = 0;
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Rate":
                    for (i = 0; i < this.gvSubRate.Rows.Count; i++)
                    {
                        TblRowIndex = (gvSubRate.PageIndex) * gvSubRate.PageSize + i;
                        double rate = Convert.ToDouble("0" + ((TextBox)this.gvSubRate.Rows[i].FindControl("txtgvSRate")).Text.Trim());
                        double workrate = Convert.ToDouble("0" + ((TextBox)this.gvSubRate.Rows[i].FindControl("txtgvwrkrate")).Text.Trim());
                        dt1.Rows[TblRowIndex]["subconrat"] = rate;
                        dt1.Rows[TblRowIndex]["workrat"] = workrate;
                    }
                    Session["tblSupRate"] = dt1;
                    break;

                case "Level":
                    for (i = 0; i < this.gvConLevel.Rows.Count; i++)
                    {
                        TblRowIndex = (gvConLevel.PageIndex) * gvConLevel.PageSize + i;
                        bool chkitem = ((CheckBox)this.gvConLevel.Rows[i].FindControl("chkItem")).Checked ? true : false;
                        dt1.Rows[TblRowIndex]["markitem"] = chkitem;
                    }
                    Session["tblSupRate"] = dt1;
                    break;


                case "ItemLock":
                    for (i = 0; i < this.gvItemlk.Rows.Count; i++)
                    {
                        TblRowIndex = (gvItemlk.PageIndex) * gvItemlk.PageSize + i;
                        bool chkitem = ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Checked ? true : false;
                        dt1.Rows[TblRowIndex]["itemlock"] = chkitem;
                    }
                    Session["tblSupRate"] = dt1;
                    break;



                case "ItemLock02":

                    foreach (GridViewRow gv1 in gvitemlk02.Rows)
                    {

                        TblRowIndex = (gvItemlk.PageIndex) * gvItemlk.PageSize + i;
                        bool chkitem = ((CheckBox)gv1.FindControl("chkItemlk")).Checked ? true : false;
                        dt1.Rows[TblRowIndex]["itemlock"] = chkitem;
                        i++;

                    }
                    Session["tblSupRate"] = dt1;
                    break;

            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            string gvname = (this.Request.QueryString["Type"] == "Rate") ? "gvSubRate" : (this.Request.QueryString["Type"] == "Level") ? "gvConLevel"
                : (this.Request.QueryString["Type"] == "Level") ? "gvItemlk" : "gvitemlk02";
            this.SaveValue();
            this.LoadGrid(gvname);
        }

        protected void lnkbtnUpdatelevel_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                string conlevel = dt1.Rows[i]["markitem"].ToString().Trim();
                // bool conlevel1 = Convert.ToBoolean(dt1.Rows[i]["markitem"].ToString().Trim()); 

                bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATESUPRAT", pactcode1, isircode,
                      flrcod, conlevel, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {

                    return;
                }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.chkUllock.Checked = false;
            this.chkUllock_CheckedChanged(null, null);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Construction Level";
                string eventdesc = "Lock/Unlock Construction Level";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void gvConLevel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvConLevel.PageIndex = e.NewPageIndex;
            this.LoadGrid("gvConLevel");
        }
        protected void chkUllock_CheckedChanged(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {

                case "Level":
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
                    break;

                case "ItemLock":
                    if (this.chkUllock.Checked)
                    {

                        for (int i = 0; i < this.gvItemlk.Rows.Count; i++)
                        {
                            ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < this.gvItemlk.Rows.Count; i++)
                        {
                            ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Enabled = ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Checked ? false : true;
                        }
                    }
                    break;


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
        protected void lnkbtnUpdateItemlk_Click(object sender, EventArgs e)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.SaveValue();
            DataTable dt1 = (DataTable)Session["tblSupRate"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();




            DataSet ds1 = new DataSet();
            ds1.DataSetName = "ds1";
            ds1.Merge(dt1);

            ds1.Tables[0].TableName = "tbl1";
            bool result = ImpleData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEITEMLOCK", ds1, null, null, pactcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ImpleData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Construction Level";
                string eventdesc = "Lock/Unlock Construction Level";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void chkAllfrmItemlk_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblSupRate"];
            int i, index;

            if (((CheckBox)this.gvItemlk.HeaderRow.FindControl("chkAllfrmItemlk")).Checked)
            {

                for (i = 0; i < this.gvItemlk.Rows.Count; i++)
                {
                    ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Enabled = (((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Checked) ? false : true;
                    ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Checked = true;
                    index = (this.gvItemlk.PageSize) * (this.gvItemlk.PageIndex) + i;
                    dt.Rows[index]["itemlock"] = "True";
                }
            }

            else
            {
                for (i = 0; i < this.gvItemlk.Rows.Count; i++)
                {
                    ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Enabled = true;
                    ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk")).Checked = false;
                    index = (this.gvItemlk.PageSize) * (this.gvItemlk.PageIndex) + i;
                    dt.Rows[index]["itemlock"] = "False";

                }

            }

            Session["tblSupRate"] = dt;
        }
        protected void gvItemlk_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvItemlk.PageIndex = e.NewPageIndex;
            this.LoadGrid("gvItemlk");
        }

        protected void gvitemlk02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvitemlk02.PageIndex = e.NewPageIndex;
            this.LoadGrid("gvitemlk02");


        }
        protected void chkAllfrmItemlk02_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblSupRate"];
            int i = 0, index;

            if (((CheckBox)this.gvitemlk02.HeaderRow.FindControl("chkAllfrmItemlk02")).Checked)
            {


                foreach (GridViewRow gv1 in gvitemlk02.Rows)
                {
                    ((CheckBox)gv1.FindControl("chkItemlk02")).Enabled = (((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk02")).Checked) ? false : true;
                    ((CheckBox)this.gvItemlk.Rows[i].FindControl("chkItemlk02")).Checked = true;
                    index = (this.gvItemlk.PageSize) * (this.gvItemlk.PageIndex) + i;
                    dt.Rows[index]["itemlock"] = "True";
                    i++;
                }


            }

            else
            {

                foreach (GridViewRow gv1 in gvitemlk02.Rows)
                {


                    ((CheckBox)gv1.FindControl("chkItemlk02")).Enabled = true;
                    ((CheckBox)gv1.FindControl("chkItemlk02")).Checked = false;
                    index = (this.gvItemlk.PageSize) * (this.gvItemlk.PageIndex) + i;
                    dt.Rows[index]["itemlock"] = "True";
                    i++;
                }




            }

            Session["tblSupRate"] = dt;
        }
        protected void lnkbtnUpdateitemlk02_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.SaveValue();
            DataTable dt1 = (DataTable)Session["tblSupRate"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = new DataSet();
            ds1.DataSetName = "ds1";
            ds1.Merge(dt1);

            ds1.Tables[0].TableName = "tbl1";


            bool result = ImpleData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEITEMLOCK02", ds1, null, null, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ImpleData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Construction Level";
                string eventdesc = "Lock/Unlock Construction Level";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
    }
}
