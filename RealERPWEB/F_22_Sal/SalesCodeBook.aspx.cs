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
namespace RealERPWEB.F_22_Sal
{
    public partial class SalesCodeBook : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        string msg = "";



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = SetTextTitle();
                //        (this.Request.QueryString["Type"].ToString().Trim() == "Sales") ? "SALES CODE BOOK"
                //: (this.Request.QueryString["Type"].ToString().Trim() == "RABill") ? "Sub-Contractor R/A Code Book" : " Supplier/Sub-Contractor INFORMATION FIELD";


            }
            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private string SetTextTitle()
        {
            string txtTitle = "";
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Sales":
                    txtTitle = "SALES CODE BOOK";
                    break;
                case "RABill":
                    txtTitle = "Sub-Contractor R/A Code Book";
                    break;
                case "Budget":
                    txtTitle = "Code Book (Unit & R/A)";
                    break;

                default:
                    txtTitle = "Supplier/Sub-Contractor INFORMATION FIELD";
                    break;
            }
            return txtTitle;

        }
        protected void Load_CodeBooList()
        {

            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                string comcod = this.GetCompCode();
                string Type = this.Request.QueryString["Type"].ToString().Trim();

                string code = this.Request.QueryString["Code"] == "22" ? "22%" : "%%";

                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTSALECODE", Type,
                                code, "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "gdesc";
                this.ddlOthersBook.DataValueField = "gcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }



        }


        private void LoadGrid()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.lnknewentry.Visible = true;

            }
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

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
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod3")).Text.Trim();

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();


            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string Rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvrate")).Text.Trim()).ToString();
            string slno = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtslno")).Text.Trim()).ToString();

            string gdescbn = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDescbn")).Text.Trim();
            bool isResultValid = true;
            if (Desc.Length == 0)
            {
                msg = "Resource Head is not empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                isResultValid = false;
                return;
            }

            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPSALINF", tgcod,
                           gdesc, Gtype, Rate, "", slno, gdescbn, "", "", "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
            }
            this.grvacc.EditIndex = -1;
            this.ShowInformation();
            this.grvacc_DataBind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales Code Book";
                string eventdesc = "Update CodeBook";
                string eventdesc2 = tgcod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void grvacc_DataBind()
        {
            try
            {

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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            DataSet ds1 = Rprss.DataCodeBooks("SP_REPORT_CODEBOOK", comcod, "RPTOTHERACCOUNTCODEBook", "", tempddl2);
            ReportDocument rptAccCode = new RealERPRPT.R_17_Acc.rptOthersAccCode();
            TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtadress = rptAccCode.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            txtadress.Text = comadd;
            TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            rpttxtNameR.Text = "OTHER ACCOUNTS  CODE BOOK  REPORT";
            TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptAccCode.SetDataSource(ds1.Tables[0]);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales Code Book";
                string eventdesc = "Print CodeBook";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = rptAccCode;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





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
                    string Code = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
                    this.grvacc.Columns[7].Visible = ((Code == "88") || (Code == "71")) ? true : false;
                    this.grvacc.Columns[10].Visible = (Code == "01") ? true : false;
                    this.ShowInformation();
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();

                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
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
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTSALECODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.lnknewentry.Visible = true;

            }
            Session["storedata"] = ds1.Tables[0];
        }



        protected void lnknewentry_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string sircode = tempddl1 + "0100000000";
            bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", tempddl1, sircode, "", "", "", "", "0.000000", "", "", "",
                        "", "", "", "", "");
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1, "12",
                            "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];


            this.grvacc.DataSource = (DataTable)Session["storedata"];
            this.grvacc.DataBind();
            ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;


            this.lnknewentry.Visible = false;


        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["storedata"];
            string comcod = this.GetCompCode();

            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                string slno = ((TextBox)grvacc.Rows[i].FindControl("lblslno")).Text.ToString();
                string gcod = ((Label)grvacc.Rows[i].FindControl("lbgrcod1")).Text.ToString();
                CheckBox chk = ((CheckBox)grvacc.Rows[i].FindControl("chkStatus"));
                string checkstatus = (chk.Checked == true) ? "True" : "False";
                //dt1.Rows[i]["slno"] = slno;
                //dt1.Rows[i]["status"] = checkstatus;

                bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPSALINFSTATUS", gcod,
                           slno, checkstatus, "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = da.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }

        protected void lbtnAdd_Click(object sender, EventArgs e)    
        {
            string msg = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {

                msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                return;
            }

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int index = this.grvacc.PageSize * this.grvacc.PageIndex + RowIndex;
            string gcod = ((DataTable)Session["storedata"]).Rows[index]["gcod"].ToString();
            this.lblgrcode.Text = gcod;
            this.txtgrcode.Text = gcod.Substring(0, 2) + "-" + ASTUtility.Right(gcod, 3);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
        }

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string gcod = lblgrcode.Text;

           
            string tgrcode = this.txtgrcode.Text.Trim().Replace("-", "");
            string Desc = this.txtresourcehead.Text.Trim();
            string DescBN = this.txtresourceheadBN.Text.Trim();
            string gtype = this.txttype.Text.Trim();            
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;

            string rate =Convert.ToDouble(("0"+this.txtrate.Text.Trim())).ToString();

            string sl = Convert.ToInt32("0"+this.txtsl.Text.Trim()).ToString();
            string chkstatus = (this.chkstatus.Checked == true) ? "True" : "False";
            string mnumber = (gcod == tgrcode) ? "" : "manual";

            bool isResultValid = false;
            if (Desc.Length == 0)
            {
                msg = "Resource Head is not empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                isResultValid = false;
                return;
            }

            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDSALECODE", tgrcode,
                          Desc, Gtype, rate, "", sl, DescBN, mnumber,"" ,chkstatus, "", "", "", "");
            
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Created ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Create Failed";
            }
            ShowInformation();
            grvacc_DataBind();
        }

        //protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["storedata"];
        //    var maxgcod = dt.AsEnumerable().Max(x => x[1]);

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string gcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GCOD"));
        //        if (gcod == Convert.ToString(maxgcod))
        //        {
        //            LinkButton link = (LinkButton)e.Row.FindControl("lbtnAdd") ;
        //            link.Visible = true;
        //        }
        //    }
            
        //}
    }
}
