using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
//using RealERPRPT;
namespace RealERPWEB.F_64_Mgt
{
    public partial class ProjectCode : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Code";
                this.Load_CodeBooList();

                // this.GetCode();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void Load_CodeBooList()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string type = this.Request.QueryString["Type"].ToString();
                DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK_NEW", "ACCOUNTCODE", type,
                                "", "", "", "", "", "", "", "");

                this.ddlCodeBook.DataSource = dsone.Tables[0];
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

        private void GetCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("tblgencode");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK_NEW", "GETGENERALCODE", "", "", "", "", "", "", "", "", "");
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
            //int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            //string pactcode = ((DataTable)Session["storedata"]).Rows[rowindex]["pactcode"].ToString();
            //string actcode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim().Replace("-", "");
            //string actcode2 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            //string actcode = actcode1 + actcode2;
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

            this.ConfirmMessage.Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            {
                string actcode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
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
                    updateallow = false;
                }
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtgvlevel = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridlevel")).Text.Trim();
                string typeCode = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvType")).Text.Trim();
                string TypeDesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvTypeDesc")).Text.Trim();
                string pactcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                DataTable tbl1 = (DataTable)Session["storedata"];
                string dd2value = this.ddlCodeBookSegment.SelectedValue.ToString().Trim();
                if (dd2value == "4" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) != pactcode1.Substring(2, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                    else if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (dd2value == "8" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(8, 4) != pactcode1.Substring(6, 4))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (dd2value == "12" && pactcode1 != actcode.Substring(2, 10) && actcode1.Length == 12 && actcode1.Substring(2, 1) == "-" && actcode1.Substring(7, 1) == "-" && !actcode1.Contains(" "))
                {
                    if (actcode1.Substring(3, 4) == "0000" && actcode1.Substring(8, 4) != "0000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
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
                    tbl1.Rows[Index]["ACTELEV"] = txtgvlevel;
                    tbl1.Rows[Index]["ACTTYPE"] = typeCode;
                    tbl1.Rows[Index]["ACTTDESC"] = TypeDesc;
                    this.grvacc.EditIndex = -1;
                    bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK_NEW", "ACCOUNTUPDATE", actcode2.Substring(0, 2), actcode, Desc, txtgvlevel, typeCode, TypeDesc, userid, "", "", "",
                        "", "", "", "", "");
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
        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {
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
                    this.ConfirmMessage.Visible = false;
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

                    this.ConfirmMessage.Visible = false;
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
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK_NEW", "ACCOUNTINFO", dd1value, dd2value, srchoption, "", "", "", "", "", "");
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

    }
}
