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
namespace RealERPWEB.F_14_Pro
{
    public partial class MatLedCodeBook : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        static string[] CarArray = new string[3] { "Sub Code-1", "Sub Code-2", "Details Code" };
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "Material Code Book (Lead Time Input)";

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
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

                string Querytype = this.Request.QueryString["InputType"];
                string coderange = (Querytype == "Res") ? "sircode like '0[1-6]%'" : "sircode like '99%'";
                string comcod = this.GetComeCode();
                string AllRes = (Querytype == "ResCodePrint") ? "ALL" : "";
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTCODE", coderange, AllRes, "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }


        //protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {

        //        string comcod =this.GetComeCode();
        //        string sircode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
        //        string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
        //        string sircode = "";
        //        bool updateallow = true;//01-001-01-001

        //        if (sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
        //        {
        //            sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
        //        }
        //        else
        //        {
        //           ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
        //            updateallow = false;
        //        }


        //        string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
        //        string txtsirtype = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridsirtype")).Text.Trim();
        //        string txtsirtdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
        //        string txtsirunit = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirunit")).Text.Trim();
        //        string txtsirval =Convert.ToDouble("0"+((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim()).ToString();
        //        string psircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();

        //        DataTable tbl1 = (DataTable)Session["storedata"];//check whether it is needed or not

        //        string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

        //        if (tempddl2 == "4" && psircode1!=sircode.Substring(2,10)&& sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
        //        {
        //            if (sircode1.Substring(3, 3) != psircode1.Substring(2, 3))
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
        //                updateallow = false;
        //            }
        //            else if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
        //                updateallow = false;
        //            }
        //            else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
        //                updateallow = false;
        //            }
        //        }
        //        else if (tempddl2 == "7" && psircode1!=sircode.Substring(2,10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
        //        {
        //            if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
        //                updateallow = false;
        //            }
        //            else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
        //                updateallow = false;
        //            }
        //        }
        //        else if (tempddl2 == "9" && psircode1!=sircode.Substring(2,10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
        //        {

        //           if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3) || sircode1.Substring(3,3)=="000")
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
        //                updateallow = false;
        //            }

        //        }
        //        else if (tempddl2 == "12" && psircode1!=sircode.Substring(2,10) &&  sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
        //        {
        //            if (sircode1.Substring(3, 3) == "000" && sircode1.Substring(7, 2) != "00" || sircode1.Substring(7, 2) == "00" && sircode1.Substring(10, 3) != "000")
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
        //                updateallow = false;
        //            }
        //        }


        //        if (updateallow)
        //        {
        //            Hashtable hst = (Hashtable)Session["tblLogin"];
        //            string userid = hst["usrid"].ToString();

        //            int Index=grvacc.PageSize * grvacc.PageIndex + e.RowIndex;


        //            txtsirval = "0" + txtsirval;
        //            tbl1.Rows[Index]["SIRCODE"] = sircode;
        //            tbl1.Rows[Index]["SIRDESC"] = Desc;
        //            tbl1.Rows[Index]["SIRTYPE"] = txtsirtype;
        //            tbl1.Rows[Index]["SIRTDES"] = txtsirtdesc;
        //            tbl1.Rows[Index]["SIRUNIT"] = txtsirunit;
        //            tbl1.Rows[Index]["SIRVAL"] = Convert.ToDecimal(txtsirval);


        //            bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, "", "",
        //                "", "", "", "", "");               
        //            this.ShowInformation();
        //            if (result)
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
        //            }
        //            else
        //            {
        //               ((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
        //            }
        //            this.grvacc.EditIndex = -1;
        //            this.grvacc_DataBind();

        //        }
        //        if (ConstantInfo.LogStatus == true)
        //        {
        //            string eventtype = "Account Sub-CodeBook";
        //            string eventdesc = "Update Sub-CodeBook";
        //            string eventdesc2 = sircode;
        //            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //       ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
        //    }
        //}

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
            //ReportDocument rptAccCode = new RealERPRPT.R_17_Acc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Account Sub-CodeBook";
            //    string eventdesc = "Print Sub-CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";     
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    ((Label)this.Master.FindControl("lblmsg")).Text = "";
                    this.LblBookName1.Text = "Code Book:";
                    this.ddlOthersBook.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.lbalterofddl.Text = this.ddlOthersBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
                    string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";
                    ((Label)this.Master.FindControl("lblmsg")).Text = "";
                    this.LblBookName1.Text = "Code Book:";
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
            }
        }

        private void ShowInformation()
        {
            string comcod = this.GetComeCode();
            Session.Remove("storedata");
            string srchoptionmain = this.txtsrch.Text.Trim();
            //string srchoption1 = "";
            string srchoption = (srchoptionmain.Contains("-")) ? srchoptionmain : srchoptionmain + "%";
            if (srchoption.Contains("-"))
            {

                int index, index01;
                if (srchoption.Contains(","))
                {
                    index = srchoption.IndexOf(",");
                    index01 = srchoption.IndexOf("-");
                    srchoption = "sircode like '" + srchoptionmain.Substring(0, 1) + "[" + srchoptionmain.Substring(1, 1) + "-" + srchoptionmain.Substring(index01 + 2, 1) + "]%'";
                    srchoption = srchoption + " or " + "sircode like '" + srchoptionmain.Substring(index + 1) + "%'";
                }
                else
                {
                    index01 = srchoption.IndexOf("-");
                    srchoption = "sircode like '" + srchoptionmain.Substring(0, 1) + "[" + srchoptionmain.Substring(1, 1) + "-" + srchoptionmain.Substring(index01 + 2, 1) + "]%'";

                }

            }

            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            tempddl1 = (tempddl1 == "00") ? "" : tempddl1;
            // string Calltype = (srchoptionmain.Contains("-")) ? "OACCOUNTBTWNCINFO" : "OACCOUNTINFO";

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GERMATINFO", tempddl1, tempddl2, srchoption, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }
        protected void Session_tblMat_Update()
        {
            DataTable tbl1 = (DataTable)Session["storedata"];
            int TblRowIndex2;
            for (int j = 0; j < this.grvacc.Rows.Count; j++)
            {
                double dgvOrderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[j].FindControl("txtLeadTime")).Text.Trim()));
                ((TextBox)this.grvacc.Rows[j].FindControl("txtLeadTime")).Text = dgvOrderQty.ToString("#,##0.00;(#,##0.00); ");
                TblRowIndex2 = (this.grvacc.PageIndex) * this.grvacc.PageSize + j;

                tbl1.Rows[TblRowIndex2]["leadtime"] = dgvOrderQty;
            }
            Session["storedata"] = tbl1;
            this.grvacc_DataBind();
        }


        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblMat_Update();
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc_DataBind();
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_tblMat_Update();
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc_DataBind();
        }
        protected void lbtnUpdateMat_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                Session_tblMat_Update();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataTable tblt05 = (DataTable)Session["storedata"];
                for (int i = 0; i < tblt05.Rows.Count; i++)
                {
                    string sircode = tblt05.Rows[i]["sircode"].ToString();
                    string leadtime = Convert.ToDouble("0" + tblt05.Rows[i]["leadtime"]).ToString();

                    bool resulta = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "UPDATE_LEADTIME", sircode, leadtime, "", "", "", "",
                                "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = this.da.ErrorObject["Msg"].ToString();
                        return;
                    }

                }
               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }



        }
    }
}
