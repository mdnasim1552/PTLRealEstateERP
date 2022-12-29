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

namespace RealERPWEB.F_01_LPA
{
    public partial class LandOwCodeBook : System.Web.UI.Page
    {

        ProcessAccess da = new ProcessAccess();
        // static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            ((Label)this.Master.FindControl("lblTitle")).Text = "Project Information Code";

            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            this.ConfirmMessage.Visible = false;
        }

        protected void Load_CodeBooList()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string qtype = (this.Request.QueryString["Type"] == "lp") ? this.Request.QueryString["Type"].ToString() : "";

                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "LANDOWCODE", qtype,
                                    "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "prgdesc";
                this.ddlOthersBook.DataValueField = "prgcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();

                ViewState["tblres"] = dsone.Tables[1];
                this.ddlOthersBook_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void ddlOthersBook_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = ((DataTable)ViewState["tblres"]).Copy();

            string mrescode = this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2);
            EnumerableRowCollection<DataRow> item = (from r in dt.AsEnumerable()
                                                     where (r.Field<string>("prgcod").Substring(0, 2) == mrescode || r.Field<string>("prgcod").Substring(0, 2) == "00")
                                                     select r);

            dt = item.AsDataView().ToTable();
            dt.Rows.Add("0000", "All Catagory");

            this.ddlCatcode.DataTextField = "prgdesc";
            this.ddlCatcode.DataValueField = "prgcod";
            this.ddlCatcode.DataSource = dt;
            this.ddlCatcode.DataBind();
            this.ddlCatcode.SelectedValue = "0000";


        }
        protected void ddlCatcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "New")
            {
                this.ShowInformation();
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

            int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            Panel pnl02 = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("Paneldata");
            string code = ((DataTable)Session["storedata"]).Rows[rowindex]["code"].ToString();
            string gcode = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");

            string gcode2 = ASTUtility.Right(((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", ""), 3);


            DropDownList ddl3 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlData");
            ViewState["gindex"] = e.NewEditIndex;
            string Type = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "54" & gcode2 != "000") ? "53%"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "55" & gcode2 != "000") ? "54%"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "56" & gcode2 != "000") ? "55%"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "57" & gcode2 != "000") ? "56%"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "58" & gcode2 != "000") ? "57%"

                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "54" & gcode2 == "000") ? "52%"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "55" & gcode2 == "000") ? "53%"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "56" & gcode2 == "000") ? "54%"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "57" & gcode2 == "000") ? "55%"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "58" & gcode2 == "000") ? "56%" : "";

            if (gcode != "00000")
            {

                DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETINFOLIST", Type, "", "", "", "", "", "", "", "");
                ddl3.DataTextField = "prgdesc";
                ddl3.DataValueField = "prgcod";
                ddl3.DataSource = ds1;
                ddl3.DataBind();
                ddl3.SelectedValue = code;
                pnl02.Visible = true;
            }
            else
            {
                ddl3.Items.Clear();
                pnl02.Visible = false;


            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Information Code";
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
                this.ConfirmMessage.Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string codedsc = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            if (gcode2 != "00000")
            {
                codedsc = (gcode1.Replace("-", "") == "53" || gcode1.Replace("-", "") == "54" || gcode1.Replace("-", "") == "55" || gcode1.Replace("-", "") == "56"
                    || gcode1.Replace("-", "") == "57" || gcode1.Replace("-", "") == "58") ?
                        ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlData")).SelectedItem.Text.Trim() + " - " : "";
            }
            else
            {

            }



            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = (ASTUtility.Right(gcode2, 3) == "000") ? codedsc + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim()
                    : ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();// ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string slno = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvtslno")).Text.Trim();
            string code = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlData")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;



            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPPRINF", tgcod,
                           gdesc, Gtype, slno, code, "", "", "", "", "", "", "", "", "", "");

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
                string eventtype = "Information Code";
                string eventdesc = "Update Code Book";
                string eventdesc2 = this.ddlOthersBook.SelectedItem.ToString() + ", " + tgcod + "-" + gdesc;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];

                this.grvacc.Columns[2].HeaderText = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "54") ? "Country/District"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "55") ? "District/Zone"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "56") ? "Zone/Police Station"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "57") ? "Police Station/Area"
                : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "58") ? "Area/Block" : "";

                this.grvacc.Columns[2].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "54") ? true
                    : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "55") ? true
                    : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "56") ? true
                    : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "57") ? true
                    : ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "58") ? true : false;

                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
                //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                //for (int i = 1; i <= TotalPage; i++)
                //    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                //if (TotalPage > 1)
                //    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = true;
                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;



            }
            catch (Exception ex)
            {
            }

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
                this.grvacc.EditIndex = -1;

                this.grvacc_DataBind();
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
            //this.lbljavascript.Text = "<script>window.open('RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





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
            string catgrory = ((this.ddlCatcode.SelectedValue.ToString() == "0000") ? "%" : this.ddlCatcode.SelectedValue.ToString()) + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "LANDOWCODEDETAIL", tempddl1,
                            tempddl2, catgrory, "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];

            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();


        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgcod")).ToString();

                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 3) == "000" && ASTUtility.Right(Code, 5) != "00000")
                {

                    e.Row.Attributes["style"] = "background-color:#3399FF; font-weight:bold; color:white";
                }

                //else if (ASTUtility.Right(Code, 10) == "0000000000")
                //    e.Row.Attributes["style"] = "background-color:#3399FF; font-weight:bold;";




            }


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
