
using System;
using System.Collections;
using System.Collections.Generic;
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
using RealEntity;
using Microsoft.Reporting.WinForms;
using dpant;
using RealERPRDLC;

namespace RealERPWEB.F_17_Acc
{


    public partial class AccCodeBook : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        protected FullGridPager fullGridPager;
        protected int MaxVisible = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Account Code";
                this.Master.Page.Title = "Account Code";
                this.Load_CodeBooList();
                this.GetTeamCode();
               // CommonButton();

            }

            if (IsPostBack)
            {


                fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
                fullGridPager.CreateCustomPager(grvacc.BottomPagerRow);
            }

        }

        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
          

        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void Load_CodeBooList()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTCODE", userid,
                                "", "", "", "", "", "", "", "");
                Session["LoadDataForDropDownList"] = dsone.Tables[0];
                DataTable dt1 = (DataTable)Session["LoadDataForDropDownList"];
                if (dt1.Rows.Count == 0)
                {
                    dt1.Rows.Add("----Have No Code Permission Please Contact Sys Admin----", "XXXXXXXXXXXX");

                }

                this.ddlCodeBook.DataSource = dt1;
                this.ddlCodeBook.DataTextField = "actcode";
                this.ddlCodeBook.DataValueField = "actcode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }


        private void GetTeamCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchoption = "%%";
            DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "GETCATNAME", srchoption, "", "", "", "", "", "", "", "");
            ViewState["tblteam"] = dsone.Tables[0];
            dsone.Dispose();

        }

        private void GetCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("tblgencode");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETGENERALCODE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblgencode"] = ds1.Tables[0];


        }




        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }

        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {


            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
            ViewState["gindex"] = e.NewEditIndex;
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            // string pactcode = ((DataTable)Session["storedata"]).Rows[rowindex]["pactcode"].ToString();
            string actcode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
            string actcode2 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            string actcode = actcode1 + actcode2;

            string teamcode = ((DataTable)Session["storedata"]).Rows[rowindex]["catcode"].ToString();
            string pactcode = ((DataTable)Session["storedata"]).Rows[rowindex]["pactcode"].ToString();
            //
            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlProName");
            // ////DropDownList ddlteam = (DropDownList)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("ddlteam");
            //  Panel pnlteam = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("pnlTeam");

            Panel pnl02 = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("Panel2");

            DropDownList ddlteam = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlteam");
            Panel pnlteam = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("pnlTeam");


            DropDownList ddlpro = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlpro");
            Panel pnlProject = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("pnlProject");

            if (actcode.Trim().Substring(0, 4) == "2357" && actcode.Trim().Substring(8) != "0000")
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string SearchProject = "%" + ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "DEPTNAME", SearchProject, "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "sirdesc";
                ddl2.DataValueField = "sircode";
                ddl2.DataSource = ds1;
                ddl2.DataBind();
                ddl2.SelectedValue = actcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnl02.Visible = true;

            }

            else
            {
                ddl2.Items.Clear();
                pnl02.Visible = false;


            }


            if (actcode.Trim().Substring(0, 2) == "16" && actcode.Trim().Substring(8) != "0000")
            {


                ddlteam.DataTextField = "catdesc";
                ddlteam.DataValueField = "catcode";
                ddlteam.DataSource = (DataTable)ViewState["tblteam"];
                ddlteam.DataBind();
                ddlteam.SelectedValue = teamcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnlteam.Visible = true;


                foreach (ListItem lteam in ddlteam.Items)
                {
                    string item = lteam.Value;

                    string Link = (((DataTable)ViewState["tblteam"]).Select("catcode='" + item + "'"))[0]["link"].ToString();
                    if (Link == "No Link")
                    {
                        lteam.Attributes.Add("style", "background-color:#a3ffa3");
                    }
                }


            }

            else
            {
                pnl02.Visible = false;
                ddl2.Items.Clear();
                pnlteam.Visible = false;
                ddlteam.Items.Clear();
            }



            // Project Link

            if ((actcode.Trim().Substring(0, 2) == "19" || actcode.Trim().Substring(0, 2) == "29" || actcode.Trim().Substring(0, 2) == "21" || actcode.Trim().Substring(0, 2) == "14") && actcode.Trim().Substring(8) != "0000")
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string SearchProject = "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETCACTPROJECT", SearchProject, "", "", "", "", "", "", "", "");


                ddlpro.DataTextField = "pactdesc";
                ddlpro.DataValueField = "pactcode";
                ddlpro.DataSource = ds1.Tables[0];
                ddlpro.DataBind();
                ddlpro.SelectedValue = pactcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnlProject.Visible = true;


                //foreach (ListItem lteam in ddlpro.Items)
                //{
                //    string item = lteam.Value;

                //    string Link = (((DataTable)ViewState["tblteam"]).Select("catcode='" + item + "'"))[0]["link"].ToString();
                //    if (Link == "No Link")
                //    {
                //        lteam.Attributes.Add("style", "background-color:#a3ffa3");
                //    }
                //}


            }

            else
            {

                pnlProject.Visible = false;
                ddlpro.Items.Clear();
            }




            //}
            //DropDownList ddl2 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlProCode");
            //Panel pnl02 = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("Panel2");

            //if (actcode.Trim().Substring(0, 4) == "1315" && actcode.Trim().Substring(8) != "0000") 
            //{
            //   ViewState["gindex"] = e.NewEditIndex; ;
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string SearchProject = "%" + ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
            //    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            //    ddl2.DataTextField = "actdesc1";
            //    ddl2.DataValueField = "actcode";
            //    ddl2.DataSource = ds1;
            //    ddl2.DataBind();
            //    ddl2.SelectedValue = pactcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
            //    pnl02.Visible = true;
            //    return;

            //}
            //pnl02.Visible = false;
            //ddl2.Items.Clear();

            //  this.grvacc.EditIndex = e.NewEditIndex;
            //this.grvacc_DataBind();

            //string lblgvAccType = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lblgvAccTypeEdit")).Text.Trim().Replace("-", "");
            //string actcode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
            //string actcode2 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            //string actcode = actcode1 + actcode2;


            //if (actcode.Trim().Substring(0, 4) != "1395" || actcode.Trim().Substring(8) == "0000")
            //{
            //    ddl1.Items.Clear();
            //    ddl1.Visible = false;
            //    return;
            //}
            //ddl1.SelectedValue = lblgvAccType;


        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            {
                string actcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string actcode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string actcode = "";
                bool updateallow = true;
                bool c = actcode1.Contains(" ");
                if (actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    actcode = actcode2.Substring(0, 2) + actcode1.Substring(0, 2) + actcode1.Substring(3, 4) + actcode1.Substring(8, 4);
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    updateallow = false;
                }
                string Descbn = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDescbn")).Text.Trim();
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtgvlevel = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridlevel")).Text.Trim();
                string typeCode = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvType")).Text.Trim();
                string TypeDesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvTypeDesc")).Text.Trim();
                string pactcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                string wodesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvShortDesc")).Text.Trim();
                string mProCode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlProName")).Text.Trim();
                string catcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlteam")).Text.Trim();
                string pactcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlpro")).Text.Trim();

                DataTable tbl1 = (DataTable)Session["storedata"];
                string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                if (dd2value == "4" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) != pactcode1.Substring(2, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (dd2value == "8" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (dd2value == "12" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) == "0000" && actcode1.Substring(8, 4) != "0000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }

                if (updateallow)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    string comnam = hst["comnam"].ToString();
                    string comadd = hst["comadd1"].ToString();
                    string compname = hst["compname"].ToString();
                    string username = hst["username"].ToString();
                    string userid = hst["usrid"].ToString();
                    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                    tbl1.Rows[Index]["ACTCODE"] = actcode;
                    tbl1.Rows[Index]["ACTDESC"] = Desc;
                    tbl1.Rows[Index]["ACTDESCBN"] = Descbn;
                    tbl1.Rows[Index]["ACTELEV"] = txtgvlevel;
                    tbl1.Rows[Index]["ACTTYPE"] = typeCode;
                    tbl1.Rows[Index]["ACTTDESC"] = TypeDesc;
                    tbl1.Rows[Index]["WODESC"] = wodesc;


                    this.grvacc.EditIndex = -1;
                    bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTUPDATE", actcode2.Substring(0, 2), actcode, Desc, txtgvlevel, typeCode, TypeDesc, userid, wodesc, mProCode, catcode,
                        pactcode, Descbn, "", "", "");
                    //string tempddl3 = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
                    //tempddl3 = (tempddl3 == "00" ? "" : tempddl3);
                    //string tempddl4 = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                    //DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", tempddl3,
                    //        tempddl4, "", "", "", "", "", "", "");
                    //Session["storedata"] = ds1.Tables[0];
                    //this.grvacc_DataBind();
                    this.ShowInformation();
                    if (result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                        if (ConstantInfo.LogStatus == true)
                        {
                            string eventtype = "Accounts CodeBook";
                            string eventdesc = "Update CodeBook";
                            string eventdesc2 = actcode;
                            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                        }
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }
                }
            }
            //catch (Exception ex)
            //{
            //   this.lblprintstk.Text = "Error:" + ex.Message;
            //}
        }

        protected void grvacc_DataBind()
        {
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = (DataTable)Session["storedata"]; ;
            this.grvacc.DataBind();
            //int rowindex = grvacc.CurrentCell.RowIndex;

            //int rowindex = (int)ViewState["gindex"];

            //    //int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + i;
            //string actcode1 = ((Label)grvacc.Rows[rowindex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
            //string actcode2 = ((TextBox)grvacc.Rows[rowindex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            //string actcode = actcode1 + actcode2;


            //if (ASTUtility.Left(actcode, 4) == "5723")
            //{
            //    this.grvacc.Columns[10].Visible = true;


            //}


        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            try
            {



                Hashtable hst = (Hashtable)Session["tblLogin"];

                string comnam = hst["comnam"].ToString();





                // var AccTrialBl1 = ds1.Tables[0].DataTableToList<BDACCRDLC.R_17_Acc.AccRptList1.AccTrialBl1>();
                var lst = ASITUtility03.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassAccCode>((DataTable)Session["storedata"]);


                //LocalReport rpt1 = new LocalReport();

                //rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", null, null, null);

                //Session["Report1"] = rpt1;

                LocalReport rpt1 = new LocalReport();
                Hashtable hshtbl = new Hashtable();
                hshtbl["companyname"] = comnam;

                rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptAccountCode2", hshtbl, lst, null);


                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            }
            catch (Exception ex)
            {

            }

            //if (this.lnkok.Visible == true)
            //{
            //    this.lnkok_Click(null, null);
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string CodeDesc = this.ddlCodeBook.SelectedItem.ToString().Trim().Substring(3)
            //            + " " + "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";

            //DataTable ddup = (DataTable)Session["storedata"];
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccountcode2();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtCodeBookDesc = rptstk.ReportDefinition.ReportObjects["CodeBookDesc"] as TextObject;
            //txtCodeBookDesc.Text = CodeDesc;

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Accounts CodeBook";
            //    string eventdesc = "Print CodeBook";
            //    string eventdesc2 = CodeDesc;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(ddup);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    this.ddlCodeBook.Visible = false;
                    this.ddlCodeBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.LblBookName1.Text = "Code Book:";
                    this.lbalterofddl.Text = this.ddlCodeBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = "(" + this.ddlCodeBookSegment.SelectedItem.ToString().Trim() + ")";
                    this.ShowInformation();

                }
                else
                {

                    this.lnkok.Text = "Ok";


                    this.ddlCodeBook.Visible = true;
                    this.ddlCodeBookSegment.Visible = true;
                    this.lbalterofddl.Visible = false;
                    this.lbalterofddl0.Visible = false;
                    this.ibtnSrch.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }


        private void ShowInformation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string dd1value = (this.ddlCodeBook.SelectedValue.ToString()).Substring(0, 2);
            dd1value = (dd1value == "00" ? "" : dd1value);
            string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
            string srchoption = this.txtsrch.Text.Trim() + "%";
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ACCOUNTINFO", dd1value, dd2value, srchoption, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();
        }

        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }
        protected void ibtnSrchProject_Click(object sender, ImageClickEventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];

            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[rowindex].FindControl("ddlProCode");
            string SearchProject = "%" + ((TextBox)grvacc.Rows[rowindex].FindControl("txtSerachProject")).Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();

        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //  ((LinkButton)e.Row.FindControl("lbtnAdd")).Visible = false;
                e.Row.Cells[2].ToolTip = "Edit Information";
                LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd");
                int index = e.Row.RowIndex;
                int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + index;
                DataTable dt = ((DataTable)Session["storedata"]);

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                int additem = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "additem"));

                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 4) == "0000" && ASTUtility.Right(Code, 8) != "00000000")
                {

                    //  lbtnAdd.Visible = true;
                    e.Row.Attributes["style"] = "background-color:#3399FF; font-weight:bold;";

                }


                else if (ASTUtility.Right(Code, 10) == "0000000000")
                {

                    e.Row.Attributes["style"] = "background-color:#3399FF; font-weight:bold;";
                }


                //else if (ASTUtility.Right(Code, 4) != "0000")
                //{
                //    lbtnAdd.Visible = false;
                //  //  e.Row.Attributes["style"] = "background-color:#3399FF; font-weight:bold;";
                //}




                // For Add
                if (additem == 1)
                {

                    lbtnAdd.Visible = true;


                }




                //if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState.ToString() == "Alternate, Edit")
                //{
                //    int i = 0;
                //    foreach (TableCell cell in e.Row.Cells)
                //    {
                //        if (e.Row.Cells.GetCellIndex(cell) == 4)
                //        {
                //            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[4].Controls[0])).ToolTip = "Update Resource Details";
                //            ((System.Web.UI.LiteralControl)(e.Row.Cells[4].Controls[1])).Text = "&nbsp;";
                //            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[4].Controls[2])).ToolTip = "Close Resource Details";
                //        }
                //        i++;
                //    }
                //}

                switch (comcod)
                {
                    //case "3101":
                    case "3356":
                    case "3357":
                        HyperLink hlnkgvdesc = (HyperLink)e.Row.FindControl("hlnkgvactdesc");
                        if ((ASTUtility.Left(Code, 2) == "16") && ASTUtility.Right(Code, 3) != "000")
                        {
                            hlnkgvdesc.NavigateUrl = "~/F_04_Bgd/PrjInformation?Type=Report&prjcode="+Code;
                        }
                        break;
                    default:
                        break;
                }



            }


        }

        protected void ibtnSrchProject_Click1(object sender, EventArgs e)
        {

        }

        protected void ibtnSrchteam_Click(object sender, EventArgs e)
        {

        }

        protected void ibtnSrchpro_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            int index = this.grvacc.PageSize * this.grvacc.PageIndex + RowIndex;
            string actcode = ((DataTable)Session["storedata"]).Rows[index]["actcode"].ToString();
            string pactcode = ((DataTable)Session["storedata"]).Rows[index]["actcode"].ToString();
            this.lblactcode.Text = actcode;
            this.txtacountcode.Text = actcode.Substring(0, 2) + "-" + actcode.Substring(2, 2) + "-" + actcode.Substring(4, 4) + "-" + ASTUtility.Right(actcode, 4);

            this.Chboxchild.Checked = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");
            this.chkbod.Visible = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");

            this.lblchild.Visible = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");
            // Project Link

            if ((actcode.Trim().Substring(0, 2) == "19" || actcode.Trim().Substring(0, 2) == "29" || actcode.Trim().Substring(0, 2) == "21") && actcode.Trim().Substring(8) != "0000")
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string SearchProject = "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETCACTPROJECT", SearchProject, "", "", "", "", "", "", "", "");
                this.ddlProject.DataTextField = "pactdesc";
                this.ddlProject.DataValueField = "pactcode";
                this.ddlProject.DataSource = ds1.Tables[0];
                this.ddlProject.DataBind();
                this.ddlProject.SelectedValue = pactcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                ds1.Dispose();

            }
            else
            {
                this.ddlProject.Items.Clear();
                this.lblddlproject.Visible = false;
                this.ddlProject.Visible = false;



            }



            // this.GetDetailsInfo(rsircode);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }
        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string iactcode = this.lblactcode.Text.Trim();
                string tactcode = this.txtacountcode.Text.Trim().Replace("-", "");

                string actcode = (this.Chboxchild.Checked) ? ((ASTUtility.Right(iactcode, 8) == "00000000") ? (ASTUtility.Left(this.lblactcode.Text, 4) + "0001" + ASTUtility.Right(iactcode, 4))
                    : ASTUtility.Left(this.lblactcode.Text, 8) + "0001") : ((iactcode != tactcode) ? tactcode : iactcode);
                //string actcode2 = actcode.Substring(0, 2);
                string Desc = this.txtaccounthead.Text.Trim();
                string txtgvlevel = this.txtlevel.Text.Trim();
                string typeCode = this.txttype.Text.Trim();
                string TypeDesc = this.txtshort.Text;
                string wodesc = this.txtrefid.Text.Trim();
                string mProCode = "";
                string catcode = "";
                string pactcode = this.ddlProject.Items.Count == 0 ? "" : this.ddlProject.SelectedValue.ToString();

                string mnumber = (iactcode == tactcode) ? "" : "manual";
                // return;

                if (Desc.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Account Head is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {
                    bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDACCOUNTCODE", actcode, Desc, txtgvlevel, typeCode, TypeDesc, userid, wodesc, mProCode, catcode,
                      pactcode, mnumber, "", "", "", "");

                    if (!result)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }


                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    this.ShowInformation();
                    this.Chboxchild.Checked = false;

                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }



        protected void ddlPageGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fullGridPager == null)
            {
                fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
            }
            fullGridPager.PageGroupChanged(grvacc.BottomPagerRow);
        }



        protected void grvacc_DataBound(object sender, EventArgs e)
        {
            if (fullGridPager == null)
            {
                fullGridPager = new FullGridPager(grvacc, MaxVisible, "Page", "of");
            }
            fullGridPager.CreateCustomPager(grvacc.BottomPagerRow);
            fullGridPager.PageGroups(grvacc.BottomPagerRow);

        }

    }
}