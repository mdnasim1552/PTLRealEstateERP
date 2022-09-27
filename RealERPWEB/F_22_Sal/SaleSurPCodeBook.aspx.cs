
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

namespace RealERPWEB.F_22_Sal
{


    public partial class SaleSurPCodeBook : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Code";
                this.Master.Page.Title = "Project Code";
                this.GetCompany();
                this.ShowInformation();
             
              
             

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


           
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //Commented
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


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

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }


        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return(hst["comcod"].ToString());
        }

        private void GetCompany()
        {
           
            string comcod = this.GetCompCode();
            
            DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETCOMPANYNAME", "", "", "", "", "", "", "", "", "");
            ViewState["tblcompany"] = dsone.Tables[0];
            dsone.Dispose();

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
            
            string actcode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
            string actcode2 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            string actcode = actcode1 + actcode2;

           
            string companycode = ((DataTable)Session["storedata"]).Rows[rowindex]["companycode"].ToString();
            //
      

            DropDownList ddlcompany = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlcompany");
            Panel pnlCompany = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("pnlCompany");

            if (actcode.Trim().Substring(8) != "0000")
            {

               


               ddlcompany.DataTextField = "companydesc";
               ddlcompany.DataValueField = "companycode";
               ddlcompany.DataSource = (DataTable)ViewState["tblcompany"];
               ddlcompany.DataBind();
                ddlcompany.SelectedValue = companycode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnlCompany.Visible = true;

            }

            else
            {
                ddlcompany.Items.Clear();
                pnlCompany.Visible = false;


            }


          


            
            


        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try

            {



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);                  
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

                    string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();


                    string TypeDesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvTypeDesc")).Text.Trim();
                    string pactcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                    string companycode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlcompany")).Text.Trim();

                    DataTable tbl1 = (DataTable)Session["storedata"];
                    string dd2value = "12";
                    if (dd2value == "4" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                    {
                        if (actcode1.Substring(3, 4) != pactcode1.Substring(2, 4))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);                         
                            updateallow = false;
                        }
                        else if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);
                            updateallow = false;
                        }
                    }
                    else if (dd2value == "8" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                    {
                        if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);
                            updateallow = false;
                        }
                    }
                    else if (dd2value == "12" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                    {
                        if (actcode1.Substring(3, 4) == "0000" && actcode1.Substring(8, 4) != "0000")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);
                           
                            updateallow = false;
                        }
                    }

                    if (updateallow)
                    {
                        Hashtable hst = (Hashtable)Session["tblLogin"];
                        string comcod = this.GetCompCode();
                        string userid = hst["usrid"].ToString();
                        int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                        tbl1.Rows[Index]["ACTCODE"] = actcode;
                        tbl1.Rows[Index]["ACTDESC"] = Desc;
                        tbl1.Rows[Index]["ACTTDESC"] = TypeDesc;



                        this.grvacc.EditIndex = -1;
                        bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "PROJECTCODEUPDATE",  actcode, Desc, TypeDesc, companycode, userid, "", "", "",
                            "", "", "", "", "");

                        this.ShowInformation();
                        if (result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                           
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
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        protected void grvacc_DataBind()
        {
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = (DataTable)Session["storedata"]; ;
            this.grvacc.DataBind();
           


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

           

        }




        private void ShowInformation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string search = "%" + this.txtSearch.Text.Trim() + "%";
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETPROJECTINFO", search, "", "", "", "", "", "", "", "");
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


               


                // For Add
                if (additem == 1)
                {

                    lbtnAdd.Visible = true;


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
            string companycode = ((DataTable)Session["storedata"]).Rows[index]["companycode"].ToString();
            this.lblactcode.Text = actcode;
            this.txtacountcode.Text = actcode.Substring(0, 2) + "-" + actcode.Substring(2, 2) + "-" + actcode.Substring(4, 4) + "-" + ASTUtility.Right(actcode, 4);

            this.Chboxchild.Checked = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");
            this.chkbod.Visible = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");

            this.lblchild.Visible = (ASTUtility.Right(actcode, 8) == "00000000" && ASTUtility.Right(actcode, 10) != "0000000000") || (ASTUtility.Right(actcode, 4) == "0000");

            if (actcode.Substring(8)== "0000")
            {



                this.ddladdCompany.Items.Clear();
                this.lblddlcompany.Visible = false;
                this.ddladdCompany.Visible = false;
            }
            else 
            {


                DataTable dt = (DataTable)ViewState["tblcompany"];
                this.ddladdCompany.DataTextField = "companydesc";
                this.ddladdCompany.DataValueField = "companycode";
                this.ddladdCompany.DataSource = dt;
                this.ddladdCompany.DataBind();
                this.lblddlcompany.Visible = true;
                this.ddladdCompany.Visible = true;

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
               
                string Desc = this.txtaccounthead.Text.Trim();
                string TypeDesc = this.txtshort.Text;               
                string companycode = this.ddladdCompany.Items.Count == 0 ? "" : this.ddladdCompany.SelectedValue.ToString();

                string mnumber = (iactcode == tactcode) ? "" : "manual";
                if (Desc.Length == 0)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Account Head is not empty');", true);
                    return;
                }
                else
                {
                    bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "ADDPROJECTCODE", actcode, Desc,  TypeDesc, companycode, userid, mnumber, "", "",
                      "", "", "", "", "", "");

                    if (!result)
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ accData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }


              
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                    this.ShowInformation();
                    this.Chboxchild.Checked = false;

                }




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+ex.Message+"');", true);
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

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
    }
}