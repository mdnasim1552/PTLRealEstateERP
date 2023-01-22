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
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class AccSubCodeBook : System.Web.UI.Page
    {


        ProcessAccess da = new ProcessAccess();
        static string[] CarArray = new string[3] { "Sub Code-1", "Sub Code-2", "Details Code" };
        //static string tempddl1 = "", tempddl2 = "";
        //static string  ddlname = "";
        //static string pactcode="";




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "New Employee Code Entry";

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Load_CodeBooList()
        {

            try
            {

                string comcod = this.GetComeCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTCODE", "",
                                "", "", "", "", "", "", "", "");

                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
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
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.ConfirmMessage.Visible = true;
            try
            {

                string comcod = this.GetComeCode();
                string sircode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string sircode = "";
                bool updateallow = true;//01-001-01-001

                if (sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    updateallow = false;
                }


                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtsirtype = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridsirtype")).Text.Trim();
                string txtsirtdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
                string txtsirunit = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirunit")).Text.Trim();
                string txtsirval = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim()).ToString();
                string psircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();

                DataTable tbl1 = (DataTable)Session["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

                if (tempddl2 == "4" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) != psircode1.Substring(2, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "7" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "9" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {

                    if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3) || sircode1.Substring(3, 3) == "000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }

                }
                else if (tempddl2 == "12" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) == "000" && sircode1.Substring(7, 2) != "00" || sircode1.Substring(7, 2) == "00" && sircode1.Substring(10, 3) != "000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }


                if (updateallow)
                {
                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;


                    txtsirval = "0" + txtsirval;
                    tbl1.Rows[Index]["SIRCODE"] = sircode;
                    tbl1.Rows[Index]["SIRDESC"] = Desc;
                    tbl1.Rows[Index]["SIRTYPE"] = txtsirtype;
                    tbl1.Rows[Index]["SIRTDES"] = txtsirtdesc;
                    tbl1.Rows[Index]["SIRUNIT"] = txtsirunit;
                    tbl1.Rows[Index]["SIRVAL"] = Convert.ToDecimal(txtsirval);


                    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, "", "", "",
                        "", "", "", "", "");
                    this.ShowInformation();
                    if (result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();

                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.grvacc_DataBind();
            }
            catch
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
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable Dtable = (DataTable)Session["storedata"];
            //string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //ReportDocument rptAccCode = new  RealERPRPT.R_15_Acc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            ////TextObject txtadress = rptAccCode.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtadress.Text = comadd;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            ////this.lbljavascript.Text = @"<script>window.open('RptViewer.aspx','','status=no,height=250px,width=340px,left=250px,top=250px');</script>";
            //this.lbljavascript.Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";     
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            string tempddl1 = "";
            try
            {
                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    this.LblBookName1.Text = "Code Book:";
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ddlOthersBook.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lbalterofddl.Text = this.ddlOthersBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

                    if (tempddl1 == "01")
                    {
                        grvacc.Columns[3].HeaderText = "Code";
                        grvacc.Columns[4].HeaderText = "Description of Code";
                        grvacc.Columns[5].Visible = true;
                        grvacc.Columns[6].Visible = true;
                        grvacc.Columns[5].HeaderText = "Unit";
                        grvacc.Columns[6].HeaderText = "Std.Rate";
                        grvacc.Columns[7].HeaderText = "Type";
                        grvacc.Columns[8].HeaderText = "Type Desc";
                    }
                    else if (tempddl1 == "02")
                    {
                        grvacc.Columns[3].HeaderText = "Code";
                        grvacc.Columns[4].HeaderText = "Description of Code";
                        grvacc.Columns[5].Visible = true;
                        grvacc.Columns[6].Visible = true;
                        grvacc.Columns[5].HeaderText = "Unit";
                        grvacc.Columns[6].HeaderText = "Std.Rate";
                        grvacc.Columns[7].HeaderText = "Type";
                        grvacc.Columns[8].HeaderText = "Type Desc";
                    }
                    else if (tempddl1 == "03")
                    {
                        grvacc.Columns[3].HeaderText = "Code";
                        grvacc.Columns[4].HeaderText = "Description of Code";
                        grvacc.Columns[5].Visible = true;
                        grvacc.Columns[6].Visible = true;
                        grvacc.Columns[5].HeaderText = "Unit";
                        grvacc.Columns[6].HeaderText = "Std.Rate";
                        grvacc.Columns[7].HeaderText = "Type";
                        grvacc.Columns[8].HeaderText = "Type Desc";
                    }
                    else if (tempddl1 == "04")
                    {
                        grvacc.Columns[3].HeaderText = "Code";
                        grvacc.Columns[4].HeaderText = "Description of Code";
                        grvacc.Columns[5].Visible = true;
                        grvacc.Columns[6].Visible = true;
                        grvacc.Columns[5].HeaderText = "Unit";
                        grvacc.Columns[6].HeaderText = "Std.Rate";
                        grvacc.Columns[7].HeaderText = "Type";
                        grvacc.Columns[8].HeaderText = "Type Desc";
                    }
                    else if (tempddl1 == "41")
                    {
                        grvacc.Columns[3].HeaderText = "Item Code";
                        grvacc.Columns[4].HeaderText = "Description of Code";
                        grvacc.Columns[5].Visible = true;
                        grvacc.Columns[6].Visible = true;
                        grvacc.Columns[5].HeaderText = "Unit";
                        grvacc.Columns[6].HeaderText = "Std.Qty";
                        grvacc.Columns[7].HeaderText = "Type";
                        grvacc.Columns[8].HeaderText = "Type Desc";
                    }
                    else if (tempddl1 == "81")
                    {
                        grvacc.Columns[3].HeaderText = "Code";
                        grvacc.Columns[4].HeaderText = "Contractor Name";
                        grvacc.Columns[5].Visible = false;
                        grvacc.Columns[6].Visible = false;
                        grvacc.Columns[7].HeaderText = "Type";
                        grvacc.Columns[8].HeaderText = "Type Desc";
                    }
                    else if (tempddl1 == "83")
                    {
                        grvacc.Columns[3].HeaderText = "Code";
                        grvacc.Columns[4].HeaderText = "Supplier/Purchaser Name";
                        grvacc.Columns[5].Visible = false;
                        grvacc.Columns[6].Visible = false;
                        grvacc.Columns[7].HeaderText = "Type";
                        grvacc.Columns[8].HeaderText = "Type Desc";
                    }
                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";

                    this.LblBookName1.Text = "Select Code Book:";
                    this.ibtnSrch.Visible = false;
                    this.lbalterofddl.Visible = false;
                    this.lbalterofddl0.Visible = false;
                    this.ddlOthersBook.Visible = true;
                    this.ddlOthersBookSegment.Visible = true;
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
            string comcod = this.GetComeCode();
            Session.Remove("storedata");
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            tempddl1 = (tempddl1 == "00" ? "" : tempddl1);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            string srchoption = this.txtsrch.Text.Trim() + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1,
                                  tempddl2, srchoption, "", "", "", "", "", "");
            if (ds1 == null)
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
    }
}
