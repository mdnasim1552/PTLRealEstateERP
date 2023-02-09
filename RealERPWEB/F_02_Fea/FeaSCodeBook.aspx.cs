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

namespace RealERPWEB.F_02_Fea
{
    public partial class FeaSCodeBook : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT CODEBOOK INFORMATION";

                this.GetTeamCode();
            }

            if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("WELCOME"))
                this.InitPage();
            this.ConfirmMessage.Visible = false;
        }


        private void GetTeamCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchoption = "%%";
            DataSet dsone = this.feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "GETCATNAME", srchoption, "", "", "", "", "", "", "", "");
            ViewState["tblteam"] = dsone.Tables[0];
            dsone.Dispose();

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
            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : "RES");
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
            string coderange = (sQryParm == "Resource") ? "infcod like '0[1-2]%'" : (sQryParm == "Cost") ? "infcod like '0[3-9]%'  or  infcod like '1[0-9]%' or infcod like '2[0-9]%' or infcod like '3[0-9]%' or infcod like '4[0-9]%'"
                : (sQryParm == "Other") ? "infcod like '5[1-9]%' or infcod like '[6-9][0-9]%' " : "infcod like '%%'";



            string Filter1 = "%" + this.txtFilter.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "PRINFCODELIST", mInfGrp1, Step, Filter1, coderange, "", "", "", "", "");
            Session["CodeBook"] = ds1.Tables[0];
            this.gvCodeBookDataBind();
        }

        protected void gvCodeBookDataBind()
        {
            this.gvCodeBook.DataSource = (DataTable)Session["CodeBook"];
            this.gvCodeBook.DataBind();

            string Type = this.Request.QueryString["BookName"].ToString();
            if (Type == "Project")
            {
                this.gvCodeBook.Columns[12].Visible = true;
                this.gvCodeBook.Columns[13].Visible = true;
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
            string rescode1 = ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtgvInfCod1")).Text.Trim();
            string rescode = rescode1.Substring(0, 2) + rescode1.Substring(3, 2) + rescode1.Substring(6, 3) + rescode1.Substring(10, 2) + rescode1.Substring(13);
            int rowindex = (gvCodeBook.PageSize) * (this.gvCodeBook.PageIndex) + e.NewEditIndex;

            string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            string teamcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["catcode"].ToString();

            DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("ddlProName");
            DropDownList ddlteam = (DropDownList)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("ddlteam");
            Panel pnlteam = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("pnlTeam");

            Panel pnl02 = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("Panel2");
            if ((this.Request.QueryString["BookName"] == "Project") && (ASTUtility.Right(rescode, 3) != "000"))
            {
                ViewState["gindex"] = e.NewEditIndex;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string SearchProject = "%" + ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
                DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "actdesc1";
                ddl2.DataValueField = "actcode";
                ddl2.DataSource = ds1;
                ddl2.DataBind();
                ddl2.SelectedValue = actcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnl02.Visible = true;



                ddlteam.DataTextField = "catdesc";
                ddlteam.DataValueField = "catcode";
                ddlteam.DataSource = (DataTable)ViewState["tblteam"];
                ddlteam.DataBind();
                ddlteam.SelectedValue = teamcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
                pnlteam.Visible = true;


                foreach (ListItem lteam in ddlteam.Items)
                {
                    string item = lteam.Value;

                    string Link = (((DataTable)ViewState["tblteam"]).Select("catcode='" + item + "'"))[0]["link"].ToString();
                    if (Link == "No Link")
                    {
                        lteam.Attributes.Add("style", "background-color:#a3ffa3");
                    }
                }






                //ddl2.Visible = true;
            }
            else
            {
                pnl02.Visible = false;
                ddl2.Items.Clear();
                pnlteam.Visible = false;
                ddlteam.Items.Clear();
            }



        }
        protected void gvCodeBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.ConfirmMessage.Visible = true;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mProCode = "";
            string catcode = "";
            string sQryParm = ((Label)this.Master.FindControl("lblTitle")).Text;

            string mInfGrp = (sQryParm.Contains("PROJECT") ? "PRJ" : "RES");
            string mInfCode = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfCod1")).Text.Trim().Replace("-", "");
            string mInfDesc = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfDesc")).Text.Trim();//.ToUpper();
            string mInfDes2 = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfDes2")).Text.Trim();//.ToUpper();
            string mUnitFPS = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvUnitFPS")).Text.Trim();//.ToUpper();
            string mStdQtyF = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvStdQtyF")).Text.Trim()).ToString();
            string mConsArea = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvcarea")).Text.Trim()).ToString();
            string mSalArea = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvsarea")).Text.Trim()).ToString();

            string Type = this.Request.QueryString["BookName"].ToString();

            if (Type == "Project")
            {
                mProCode = ((DropDownList)this.gvCodeBook.Rows[e.RowIndex].FindControl("ddlProName")).Text.Trim();
                catcode = ((DropDownList)this.gvCodeBook.Rows[e.RowIndex].FindControl("ddlteam")).Text.Trim();

                if (mProCode != "")
                {
                    DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "CHECKEDDUPACCCODE", mProCode, "", "", "", "", "", "", "", "");
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
                            //this.ddlPrevReqList.Items.Clear();
                            return;
                        }
                    }

                }
            }


            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "PRINFCODEUPDATE",
                          mInfGrp, mInfCode, mInfDesc, mInfDes2, mUnitFPS, mStdQtyF, mConsArea, mSalArea, mProCode,
                          catcode, "", "", "", "", "");
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
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
            ReportDocument rptCodeBookP = new RealERPRPT.R_02_Fea.rptTASCodeBook();
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
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            Session.Remove("CodeBook");
            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();
            ((Label)this.Master.FindControl("lblTitle")).Text = sQryParm + " CODEBOOK INFORMATION INPUT/VIEW";
            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : (sQryParm == "ITEM" ? "ITM" : "RES"));
            this.ShowCodeList(mInfGrp);

        }



        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            string sQryParm = Request.QueryString["BookName"].ToString().ToUpper();
            ((Label)this.Master.FindControl("lblTitle")).Text = sQryParm + " CODEBOOK INFORMATION INPUT/VIEW";
            string mInfGrp = (sQryParm == "PROJECT" ? "PRJ" : (sQryParm == "ITEM" ? "ITM" : "RES"));
            this.ShowCodeList(mInfGrp);
        }
        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[rowindex].FindControl("ddlProName");
            string SearchProject = "%" + ((TextBox)gvCodeBook.Rows[rowindex].FindControl("txtSerachProject")).Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            //ddl2.SelectedValue = actcode;

        }
        protected void ibtnSrchteam_Click(object sender, EventArgs e)
        {

        }
    }
}

