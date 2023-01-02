using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.IO;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_01_LPA
{
    public partial class LpSCodeBook : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT CODEBOOK INFORMATION";
            }

            if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("WELCOME"))
                this.InitPage();
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintBook_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void InitPage()
        {
            Session.Remove("CodeBook");
            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();
            ((Label)this.Master.FindControl("lblTitle")).Text = sQryParm + " CODEBOOK INFORMATION INPUT/VIEW";
            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : sQryParm == "PRIPROJ" ? "PRJ" : "RES");
            this.gvCodeBook.Columns[9].Visible = mInfGrp.Equals("RES");
            //this.gvCodeBook.Columns[10].Visible = mInfGrp.Equals("PRJ");
            //this.gvCodeBook.Columns[11].Visible = mInfGrp.Equals("PRJ");
            // (sQryParm == "PROJECT" ? "Active" : this.gvCodeBook.Columns[9].HeaderText);
        }

        protected void ShowCodeList(string mInfGrp1)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Step = this.ddlGroupList.SelectedIndex.ToString();

            string sQryParm = Request.QueryString["BookName"].ToString();
            //string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : (sQryParm == "ITEM" ? "ITM" : "RES"));

            //string Querytype = this.Request.QueryString["InputType"];
            string coderange = (sQryParm == "Resource") ? "infcod like '0[1-2]%'" : (sQryParm == "Resource02") ? "infcod like '51%'"
                : (sQryParm == "Cost") ? "infcod like '0[3-9]%'  or  infcod like '1[0-9]%' or infcod like '2[0-9]%' or infcod like '3[0-9]%' or infcod like '4[0-9]%'"
                : (sQryParm == "Cost02") ? "infcod like '5[2-5]%'"
                : (sQryParm == "Other02") ? "infcod like '5[6-7]%'" : "infcod like '%%'";





            string pactcode = (Request.QueryString["BookName"].ToString() == "Project") ? "99%" : (Request.QueryString["BookName"].ToString() == "PriProj") ? "98%" : "%";
            string Filter1 = "%" + this.txtFilter.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_CODEBOOK", "PRINFCODELIST", mInfGrp1, Step, Filter1, coderange, pactcode, "", "", "", "");
            Session["CodeBook"] = ds1.Tables[0];
            this.gvCodeBookDataBind();
        }

        protected void gvCodeBookDataBind()
        {
            this.gvCodeBook.DataSource = (DataTable)Session["CodeBook"];
            this.gvCodeBook.DataBind();
            if (Request.QueryString["BookName"].ToString() == "Project")
            {
                this.gvCodeBook.Columns[10].Visible = true;
            }


        }

        protected void gvCodeBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCodeBook.PageIndex = e.NewPageIndex;
            this.gvCodeBookDataBind();
        }
        protected void gvCodeBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvCodeBook.EditIndex = -1;
            this.gvCodeBookDataBind();
        }
        protected void gvCodeBook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvCodeBook.EditIndex = e.NewEditIndex;
            this.gvCodeBookDataBind();
            //

            string rescode1 = ((Label)gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvInfCod1")).Text.Trim();
            string rescode = rescode1.Substring(0, 2) + rescode1.Substring(3, 2) + rescode1.Substring(6, 3) + rescode1.Substring(10, 2) + rescode1.Substring(13);
            int rowindex = (gvCodeBook.PageSize) * (this.gvCodeBook.PageIndex) + e.NewEditIndex;

            string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            // string teamcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["catcode"].ToString();

            DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("ddlProName");
            DropDownList ddlteam = (DropDownList)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("ddlteam");
            Panel pnlteam = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("pnlTeam");

            Panel pnl02 = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("Panel2");
            if ((this.Request.QueryString["BookName"] == "Project") && (ASTUtility.Right(rescode, 3) != "000"))
            {
                ViewState["gindex"] = e.NewEditIndex;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string SearchProject = "%"; //+ ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
                DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "GETPROJECT_FS", SearchProject, "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "actdesc1";
                ddl2.DataValueField = "actcode";
                ddl2.DataSource = ds1;
                ddl2.DataBind();
                ddl2.SelectedValue = actcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnl02.Visible = true;
            }
            else
            {
                pnl02.Visible = false;
                ddl2.Items.Clear();

            }


        }
        protected void gvCodeBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string sQryParm = ((Label)this.Master.FindControl("lblTitle")).Text;
            string mInfGrp = (sQryParm.Contains("PROJECT") ? "PRJ" : sQryParm.Contains("PRIPROJ") ? "PRJ" : "RES");
            string mInfCode = ((Label)this.gvCodeBook.Rows[e.RowIndex].FindControl("lblgvInfCod1")).Text.Trim().Replace("-", "");
            string mInfDesc = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfDesc")).Text.Trim();//.ToUpper();
            string mInfDes2 = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfDes2")).Text.Trim();//.ToUpper();
            string mUnitFPS = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvUnitFPS")).Text.Trim();//.ToUpper();
            string mStdQtyF = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvStdQtyF")).Text.Trim()).ToString();
            string mConsArea = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvcarea")).Text.Trim()).ToString();
            string mSalArea = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvsarea")).Text.Trim()).ToString();
            string Type = this.Request.QueryString["BookName"].ToString();

            string mProCode = "";

            if (Type == "Project")
            {
                mProCode = ((DropDownList)this.gvCodeBook.Rows[e.RowIndex].FindControl("ddlProName")).Text.Trim();

                if (mProCode != "")
                {
                    DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "CHECKEDDUPACCCODE_FS", mProCode, "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                        ;


                    else
                    {

                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("infcod <>'" + mInfCode + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Account Code";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                            //this.ddlPrevReqList.Items.Clear();
                            return;
                        }
                    }

                }
            }









            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_CODEBOOK", "PRINFCODEUPDATE",
                          mInfGrp, mInfCode, mInfDesc, mInfDes2, mUnitFPS, mStdQtyF, mConsArea, mSalArea, mProCode,
                          "", "", "", "", "", "");
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

            this.gvCodeBook.EditIndex = -1;
            this.ShowCodeList(mInfGrp);

            //this.gvCodeBookDataBind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update CodeBook";
                string eventdesc2 = mInfCode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnPrintBook_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            ReportDocument rptCodeBookP = new RealERPRPT.R_01_LPA.rptLPCodeBook();
            rptCodeBookP.SetDataSource((DataTable)Session["CodeBook"]);
            TextObject TxtRptTitle = rptCodeBookP.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            TxtRptTitle.Text = ((Label)this.Master.FindControl("lblTitle")).Text.Trim().Replace("INFORMATION INPUT/VIEW", "");

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print CodeBook";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            Session["Report1"] = rptCodeBookP;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnShowData_Click(object sender, EventArgs e)
        {
            Session.Remove("CodeBook");
            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();
            ((Label)this.Master.FindControl("lblTitle")).Text = sQryParm + " CODEBOOK INFORMATION INPUT/VIEW";
            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : (sQryParm == "ITEM" ? "ITM" : (sQryParm == "PRIPROJ" ? "PRJ" : "RES")));
            this.ShowCodeList(mInfGrp);

        }



        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();
            ((Label)this.Master.FindControl("lblTitle")).Text = sQryParm + " CODEBOOK INFORMATION INPUT/VIEW";
            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : (sQryParm == "ITEM" ? "ITM" : (sQryParm == "PRIPROJ" ? "PRJ" : "RES")));
            this.ShowCodeList(mInfGrp);
        }
        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[rowindex].FindControl("ddlProName");
            string SearchProject = "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            //ddl2.SelectedValue = actcode;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
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
            int index = this.gvCodeBook.PageSize * this.gvCodeBook.PageIndex + RowIndex;
            string infgrp = ((DataTable)Session["CodeBook"]).Rows[index]["infgrp"].ToString();
            string infcod = ((DataTable)Session["CodeBook"]).Rows[index]["infcod"].ToString();
            string actcod = ((DataTable)Session["CodeBook"]).Rows[index]["actcode"].ToString();
            this.lbgrcod.Text = infcod;
            this.infgrpchk.Text = infgrp;
            this.infcodchk.Text = infcod;
            this.actcodechk.Text = actcod;

            this.Chboxchild.Checked = (ASTUtility.Right(infcod, 8) == "00000000" && ASTUtility.Right(infcod, 10) != "0000000000") || (ASTUtility.Right(infcod, 5) == "00000" && ASTUtility.Right(infcod, 8) != "00000000") || (ASTUtility.Right(infcod, 3) == "000");
            this.chkbod.Visible = (ASTUtility.Right(infcod, 8) == "00000000" && ASTUtility.Right(infcod, 10) != "0000000000") || (ASTUtility.Right(infcod, 5) == "00000" && ASTUtility.Right(infcod, 8) != "00000000") || (ASTUtility.Right(infcod, 3) == "000");
            this.lblchild.Visible = (ASTUtility.Right(infcod, 8) == "00000000" && ASTUtility.Right(infcod, 10) != "0000000000") || (ASTUtility.Right(infcod, 5) == "00000" && ASTUtility.Right(infcod, 8) != "00000000") || (ASTUtility.Right(infcod, 3) == "000");


            this.txtlandcode.Text = infcod.Substring(0, 2) + "-" + infcod.Substring(2, 2) + "-"+ infcod.Substring(4, 3) + "-" + infcod.Substring(7, 2) + "-" + ASTUtility.Right(infcod, 3);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
        }
        

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string isinfcod = infcodchk.Text;
            string infgrp = infgrpchk.Text;
            string actcod = actcodechk.Text;

            string tinfcode = this.txtlandcode.Text.Trim().Replace("-", "");
            string DescFull = this.txtfullDesc.Text.Trim();
            string DescShort = this.txtshortDesc.Text.Trim();
            string unit = this.txtunit.Text.Trim();
            string unitrate = this.txtunitrate.Text.Trim();
            string projectname = this.txtprojectname.Text.Trim();
            string infcod = (this.Chboxchild.Checked) ? 
                ((ASTUtility.Right(isinfcod, 8) == "00000000") ? (ASTUtility.Left(isinfcod, 4) + "001" + ASTUtility.Right(isinfcod, 5))
                    : ((ASTUtility.Right(isinfcod, 5) == "00000" && ASTUtility.Right(isinfcod, 8) != "00000000") ? (ASTUtility.Left(isinfcod, 7) + "01" + ASTUtility.Right(isinfcod, 3)) 
                    : ASTUtility.Left(isinfcod, 9) + "001"))
                    : ((isinfcod != tinfcode) ? tinfcode : isinfcod);

            string mnumber = (isinfcod == tinfcode) ? "" : "manual";
            unitrate = (unitrate == "") ? "0" : unitrate;

            bool isResultValid = true;
            if (DescFull.Length == 0)
            {
                msg = "Resource Head is not empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                isResultValid = false;
                return;
            }

            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_CODEBOOK", "INSERTLPCODE", infcod, infgrp,
                          DescFull, DescShort, unit, unitrate, actcod, mnumber, "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Created ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Create Failed";
            }
            string sQryParm = ((Label)this.Master.FindControl("lblTitle")).Text;
            string mInfGrp = (sQryParm.Contains("PROJECT") ? "PRJ" : sQryParm.Contains("PRIPROJ") ? "PRJ" : "RES");
            ShowCodeList(mInfGrp);
            gvCodeBookDataBind();
        }
    }
}

