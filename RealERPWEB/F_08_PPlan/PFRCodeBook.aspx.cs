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
using RealERPRPT;
namespace RealERPWEB.F_08_PPlan
{



    public partial class PFRCodeBook : System.Web.UI.Page
    {

        ProcessAccess da = new ProcessAccess();
        // static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "Project Pre-Construction Code";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void Load_CodeBooList()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "OACCOUNTPFCODE", "",
                                "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "prgdesc";
                this.ddlOthersBook.DataValueField = "prgcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            string actcode = ((DataTable)Session["storedata"]).Rows[RowIndex]["prgcod"].ToString();
            string pactcode = ((DataTable)Session["storedata"]).Rows[RowIndex]["prgcod"].ToString();
            this.lblactcode.Text = actcode;


            this.chkbod.Visible = (ASTUtility.Right(actcode, 3) == "000" && ASTUtility.Right(actcode, 5) != "00000") || (ASTUtility.Right(actcode, 3) == "000");

            this.lblchild.Visible = (ASTUtility.Right(actcode, 3) == "000" && ASTUtility.Right(actcode, 5) != "00000") || (ASTUtility.Right(actcode, 3) == "000");
            // Project Link

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
                string tgcod = (this.Chboxchild.Checked) ? ((ASTUtility.Right(iactcode, 3) == "000") ?
                        (ASTUtility.Left(this.lblactcode.Text, 4) + "001")
                    : ASTUtility.Left(this.lblactcode.Text, 4) + "001") : iactcode;
                //(ASTUtility.Left(this.lblactcode.Text, 4) + "01" + ASTUtility.Right(iactcode, 3))
                string gdesc = this.txtaccounthead.Text.Trim();
                string Gtype = this.txttype.Text.Trim();
                string duration = "0" + this.txtDur.Text;
                string gddesc = this.txtDDesc.Text.Trim();

                //return;
                if (gdesc.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Account Head is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {
                    bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSERTUPPFINF", tgcod,
                            gdesc, Gtype, duration, gddesc, "", "INSERT", "", "", "", "", "", "", "", "");
                    //bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDACCOUNTCODE", actcode, Desc, txtgvlevel, typeCode, TypeDesc, userid, wodesc, mProCode, catcode,
                    //  pactcode, "", "", "", "", "");

                    if (!result)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = da.ErrorObject["Msg"].ToString();
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
        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {


            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "General Information Code";
                string eventdesc = "Edit Code Book";
                string eventdesc2 = this.ddlOthersBook.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gddesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string duration = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtduration")).Text.Trim()).ToString();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSERTUPPFINF", tgcod,
                           gdesc, Gtype, duration, gddesc, "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.ShowInformation();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "General Information Code";
                string eventdesc = "Update Code Book";
                string eventdesc2 = this.ddlOthersBook.SelectedItem.ToString() + ", " + tgcod + "-" + gdesc;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void grvacc_DataBind()
        {
            try
            {
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                DataTable tbl1 = (DataTable)Session["storedata"];

                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();




            }
            catch (Exception ex)
            {
            }

        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //if (this.lnkok.Visible)
            //    this.lnkok_Click(null, null);

            //string CodeDesc = this.ddlOthersBook.SelectedItem.ToString().Trim().Substring(3)
            //            + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable Dtable = (DataTable)Session["storedata"];
            //tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //ReportDocument rptAccCode = new RptHRCodeBookInfo();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            // ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    this.lnkok.Text = "New";
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ddlOthersBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    //this.LblBookName1.Visible = false;
                    //this.lbalterofddl.Visible = true;
                    //this.lbalterofddl.Text = "Project Code Book: " + this.ddlOthersBook.SelectedItem.ToString().Trim()
                    //             + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    this.ShowInformation();
                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
            }
            else
            {
                this.lnkok.Text = "Ok";
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.ddlOthersBook.Enabled = true;
                this.ddlOthersBookSegment.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
            }
        }

        private void ShowInformation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            tempddl1 = ((tempddl1 == "00" ? "" : tempddl1));
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "OACCOUNTPFCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];

            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();


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

                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgcod")).ToString();
                int additem = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "additem"));

                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 3) == "000" && ASTUtility.Right(Code, 5) != "00000")
                {


                    e.Row.Attributes["style"] = "color:#3399FF; font-weight:bold;";

                }


                else if (ASTUtility.Right(Code, 5) == "00000")
                {

                    e.Row.Attributes["style"] = "color:#3399FF; font-weight:bold;";
                }


                if (additem == 1)
                {

                    lbtnAdd.Visible = true;


                }




            }


        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();

        }
    }
}
