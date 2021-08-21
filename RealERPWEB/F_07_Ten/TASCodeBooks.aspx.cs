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
namespace RealERPWEB.F_07_Ten
{

    public partial class TASCodeBooks : System.Web.UI.Page
    {
        ProcessAccess tasData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT CODEBOOK INFORMATION";

                this.InitPage();


            }



        }

        protected void InitPage()
        {
            Session.Remove("CodeBook");
            string Type = this.Request.QueryString["BookName"].ToString();
            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();

            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : (sQryParm == "ITEM" ? "ITM" : "RES"));
            this.gvCodeBook.Columns[8].Visible = (mInfGrp.Equals("ITM") || mInfGrp.Equals("RES"));
            this.gvCodeBook.Columns[8].HeaderText = (sQryParm == "RESOURCE" ? "Unit" : this.gvCodeBook.Columns[8].HeaderText);
            this.gvCodeBook.Columns[9].HeaderText = (sQryParm == "PROJECT" ? "Active" :
                (sQryParm == "RESOURCE" ? "Unit Rate" : this.gvCodeBook.Columns[9].HeaderText));
            this.gvCodeBook.Columns[10].Visible = (Type == "Project");
        }

        protected void ShowCodeList(string mInfGrp1)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Step1 = this.ddlGroupList.SelectedIndex.ToString();
            string Filter1 = this.txtFilter.Text.Trim() + "%";
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_CODEBOOK", "PIRINFCODELIST", mInfGrp1, Step1, Filter1, "", "", "", "", "", "");
            Session["CodeBook"] = ds1.Tables[0];
        }

        protected void gvCodeBookDataBind()
        {
            this.gvCodeBook.DataSource = (DataTable)Session["CodeBook"];
            this.gvCodeBook.DataBind();
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
            string rescode1 = ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtgvInfCod1")).Text.Trim();
            string rescode = rescode1.Substring(0, 2) + rescode1.Substring(3, 2) + rescode1.Substring(6, 3) + rescode1.Substring(10, 2) + rescode1.Substring(13);
            int rowindex = (gvCodeBook.PageSize) * (this.gvCodeBook.PageIndex) + e.NewEditIndex;
            string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();


            DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("ddlProName");
            Panel pnl02 = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("Panel2");
            if ((this.Request.QueryString["BookName"] == "Project") && (ASTUtility.Right(rescode, 3) != "000"))
            {
                ViewState["gindex"] = e.NewEditIndex;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string SearchProject = "%" + ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
                DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "actdesc1";
                ddl2.DataValueField = "actcode";
                ddl2.DataSource = ds1;
                ddl2.DataBind();
                ddl2.SelectedValue = actcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnl02.Visible = true;
                //ddl2.Visible = true;
            }
            else
            {
                pnl02.Visible = false;
                ddl2.Items.Clear();
            }
        }
        protected void gvCodeBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mProCode = "";
            string comcod = hst["comcod"].ToString();

            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();

            string mInfGrp = (sQryParm.Contains("PROJECT") ? "PRJ" : (sQryParm.Contains("ITEM") ? "ITM" : "RES"));
            string mInfCode = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfCod1")).Text.Trim().Replace("-", "");
            string mInfDesc = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfDesc")).Text.Trim();//.ToUpper();
            string mInfDes2 = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfDes2")).Text.Trim();//.ToUpper();
            string mUnitFPS = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvUnitFPS")).Text.Trim();//.ToUpper();
            string mStdQtyF = "0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvStdQtyF")).Text.Trim();
            //txtgvInfDesc, txtgvInfDes2, txtgvUnitMKS, txtgvUnitFPS, txtgvConvM2F, txtgvStdQtyM, txtgvStdQtyF
            string Type = this.Request.QueryString["BookName"].ToString();
            if (Type == "Project")
            {
                mProCode = ((DropDownList)this.gvCodeBook.Rows[e.RowIndex].FindControl("ddlProName")).Text.Trim();

                //if (mProCode != "")
                //{
                //    DataSet ds2 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_CODEBOOK", "CHECKEDDUPACCCODE", mProCode, "", "", "", "", "", "", "", "");
                //    if (ds2.Tables[0].Rows.Count == 0)
                //        ;


                //    else
                //    {

                //        DataView dv1 = ds2.Tables[0].DefaultView;
                //        dv1.RowFilter = ("infcod <>'" + mInfCode + "'");
                //        DataTable dt = dv1.ToTable();
                //        if (dt.Rows.Count == 0)
                //            ;
                //        else
                //        {
                //            //this.lblms.Text = "Found Duplicate Account Code";
                //            //this.ddlPrevReqList.Items.Clear();
                //            return;
                //        }
                //    }

                //}
            }



            bool result = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_CODEBOOK", "PIRINFCODEUPDATE",
                          mInfGrp, mInfCode, mInfDesc, mInfDes2, mUnitFPS, mStdQtyF, mProCode, "", "",
                          "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            DataTable tbl01 = (DataTable)Session["CodeBook"];
            DataRow[] dr1 = tbl01.Select("infcod='" + mInfCode + "'");
            if (dr1.Length == 0)
            {
                this.ShowCodeList(mInfGrp);
            }
            else
            {
                dr1[0]["infcod"] = mInfCode;
                dr1[0]["infcod1"] = mInfCode.Substring(0, 2) + "-" + mInfCode.Substring(2, 2) + "-" + mInfCode.Substring(4, 3) + "-" + mInfCode.Substring(7, 2) + "-" + mInfCode.Substring(9, 3);
                dr1[0]["infdesc"] = mInfDesc;
                dr1[0]["infdes2"] = mInfDes2;
                dr1[0]["unitfps"] = mUnitFPS;
                dr1[0]["actcode"] = mProCode;
                dr1[0]["actdesc1"] = ((DropDownList)this.gvCodeBook.Rows[e.RowIndex].FindControl("ddlProName")).SelectedItem.Text;
                dr1[0]["stdqtyf"] = Convert.ToDouble(mStdQtyF);
                Session["CodeBook"] = tbl01;
            }


            ((Label)this.Master.FindControl("lblmsg")).Text = " Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            this.gvCodeBook.EditIndex = -1;
            this.gvCodeBookDataBind();
        }
        protected void lbtnPrintBook_Click(object sender, EventArgs e)
        {
            ReportDocument rptCodeBookP = new RealERPRPT.R_02_Fea.rptTASCodeBook();
            rptCodeBookP.SetDataSource((DataTable)Session["CodeBook"]);
            TextObject TxtRptTitle = rptCodeBookP.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;


            Session["Report1"] = rptCodeBookP;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void lbtnShowData_Click(object sender, EventArgs e)
        {
            Session.Remove("CodeBook");
            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();

            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : (sQryParm == "ITEM" ? "ITM" : "RES"));
            this.gvCodeBook.Columns[8].Visible = (mInfGrp.Equals("ITM") || mInfGrp.Equals("RES"));
            this.gvCodeBook.Columns[8].HeaderText = (sQryParm == "RESOURCE" ? "Unit" : this.gvCodeBook.Columns[8].HeaderText);
            this.gvCodeBook.Columns[9].HeaderText = (sQryParm == "PROJECT" ? "Active" :
                (sQryParm == "RESOURCE" ? "Unit Rate" : this.gvCodeBook.Columns[9].HeaderText));
            this.ShowCodeList(mInfGrp);
            this.gvCodeBookDataBind();
        }
        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[rowindex].FindControl("ddlProName");
            string SearchProject = "%" + ((TextBox)gvCodeBook.Rows[rowindex].FindControl("txtSerachProject")).Text.Trim() + "%";
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();
            ((Label)this.Master.FindControl("lblTitle")).Text = sQryParm + " CODEBOOK INFORMATION INPUT/VIEW";
            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : (sQryParm == "ITEM" ? "ITM" : "RES"));
            this.ShowCodeList(mInfGrp);
        }
    }
}
