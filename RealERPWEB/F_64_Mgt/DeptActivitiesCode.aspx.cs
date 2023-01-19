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
using RealERPLIB;
namespace RealERPWEB.F_64_Mgt
{

    public partial class DeptActivitiesCode : System.Web.UI.Page
    {

        ProcessAccess da = new ProcessAccess();
        // static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)


                ((Label)this.Master.FindControl("lblTitle")).Text = "Information Code";
            if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));



            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }
        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

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
                DataSet dsone = this.da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_PROJECTCOMFCHART", "OACCOUNTPFACTAOTHCODE", "",
                                "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "prgdesc";
                this.ddlOthersBook.DataValueField = "prgcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
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
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            string actcode = gcode1.Substring(0, 2) + gcode2;
            int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;

            string deptcod = ((DataTable)Session["storedata"]).Rows[rowindex]["deptcod"].ToString();


            DropDownList ddldept = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlDeptName");
            Panel pnl02 = (Panel)this.grvacc.Rows[e.NewEditIndex].FindControl("Panel2");

            if ((actcode.Substring(0, 2) == "02" || actcode.Substring(0, 2) == "03"))
            {
                if ((ASTUtility.Right(actcode, 8) == "00000000") && (ASTUtility.Right(actcode, 10) != "0000000000"))
                {
                    ViewState["gindex"] = e.NewEditIndex;
                    Hashtable hst = (Hashtable)Session["tblLogin"];

                    string SearchProject = "%" + ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtSearchDeptName")).Text.Trim() + "%";
                    DataSet ds1 = da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_PROJECTCOMFCHART", "GETDEPTNAME", SearchProject, "", "", "", "", "", "", "", "");
                    ddldept.DataTextField = "deptdesc";
                    ddldept.DataValueField = "deptcod";
                    ddldept.DataSource = ds1;
                    ddldept.DataBind();
                    ddldept.SelectedValue = deptcod; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                    pnl02.Visible = true;









                }
                else
                {
                    pnl02.Visible = false;
                    ddldept.Items.Clear();


                }
            }

            else
            {
                pnl02.Visible = false;
                ddldept.Visible = false;
                ddldept.Items.Clear();

            }





            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Information Code";
            //    string eventdesc = "Edit Code Book";
            //    string eventdesc2 = this.ddlOthersBook.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1 + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string duration = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvduration")).Text.Trim()).ToString("#,##0.00;(#,##0.00);");
            string deptcod = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlDeptName")).Text.Trim();
            string marks = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvmarks")).Text.Trim()).ToString("#,##0.00;(#,##0.00);");
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            bool result = da.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_PROJECTCOMFCHART", "INSERTUPPFINF", tgcod,
                           gdesc, Gtype, duration, deptcod, marks, "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
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

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];

                this.grvacc.Columns[8].Visible = (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "01") ? false : true;
                this.grvacc.Columns[9].Visible = (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "03") ? true : false;
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
                double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                for (int i = 1; i <= TotalPage; i++)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                if (TotalPage > 1)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;



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
                }
            }
            else
            {
                this.lnkok.Text = "Ok";

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
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
            string deptcode = hst["deptcode"].ToString();

            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_PROJECTCOMFCHART", "OACCOUNTPFCODEDETAILDEPT", tempddl1,
                            tempddl2, deptcode, "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];

            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();


        }



        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void ibtnSrchdept_Click(object sender, EventArgs e)
        {

        }
    }
}
