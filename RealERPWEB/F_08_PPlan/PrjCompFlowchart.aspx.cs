using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using AjaxControlToolkit;
namespace RealERPWEB.F_08_PPlan
{
    public partial class PrjCompFlowchart : System.Web.UI.Page
    {
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        ProcessAccess MktData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                string qType = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (qType == "Report") ? "Project Pre-Planning" : (qType == "Legal") ? "Project Pre-Planning-Legal"
                //    : (qType == "Logistic") ? "Project Pre-Planning-Logistic" : (qType == "Design") ? "Project Pre-Planning-Design" : "Project Pre-Planning-Brand";
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetProjectName();
                    this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.Load_CodeBooList();
                }
                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);
                }

            }
        }

        private void Getuser()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETUSERNAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetComCode();
            string txtSProject = "%" + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETEXPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            this.ddlPrjName.SelectedValue = this.Request.QueryString["prjcode"];

        }
        protected void Load_CodeBooList()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet dsone = this.MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "OACCOUNTPFCODE", "",
                                "", "", "", "", "", "", "", "");

                string qType = this.Request.QueryString["Type"].ToString();
                string typeCode = (qType == "Legal") ? "0100000" : (qType == "Logistic") ? "0300000" : (qType == "Design") ? "0500000" : "0700000";
                DataView dv = dsone.Tables[0].Copy().DefaultView;
                DataTable dt = new DataTable();
                if (qType != "Report")
                {
                    dv.RowFilter = ("prgcod='" + typeCode + "'");
                    dt = dv.ToTable();
                }
                else
                {
                    dt = dsone.Tables[0];
                }



                this.ddlOthersBook.DataTextField = "prgdesc";
                this.ddlOthersBook.DataValueField = "prgcod";
                this.ddlOthersBook.DataSource = dt;
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.lblProjectdesc.Text = this.ddlPrjName.SelectedItem.Text;
                //this.lblProjectmDesc.Text = this.ddlPrjName.SelectedItem.Text.Substring(13);
                this.ddlPrjName.Enabled = false;
                //this.lblProjectmDesc.Visible = true;
                //this.lblProjectdesc.Visible = true;
                this.LoadGrid();
                //this.pnlAppDate.Visible = true;
            }
            else
            {

                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
                //this.pnlAppDate.Visible = false;
            }
        }

        private void ClearScreen()
        {
            this.ddlPrjName.Enabled = true;
            //this.lblProjectmDesc.Text = "";
            //this.lblProjectmDesc.Visible = false;
            //this.lblProjectdesc.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            //this.lblProjectdesc.Visible = false;
            this.gvPrjInfo.DataSource = null;
            this.gvPrjInfo.DataBind();
        }
        protected void Chboxchild_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        private void LoadGrid()
        {

            string comcod = this.GetComCode();
            string ProjectCode = this.ddlPrjName.SelectedValue.ToString();
            string cType = (ASTUtility.Left(this.ddlOthersBook.SelectedValue.ToString(), 2) == "00") ? "%" : ASTUtility.Left(this.ddlOthersBook.SelectedValue.ToString(), 2);
            string chkStatus = (this.Chboxchild.Checked == true) ? "All" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "PROJECTCOMFLOWINF", ProjectCode, cType, chkStatus, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
                return;
            Session["tblprocom"] = this.HiddenSameData(ds1.Tables[0]);
            this.txtdate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["tarsdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["tarsdat"]).ToString("dd-MMM-yyyy");


            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string deptcod = dt1.Rows[0]["deptcod"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcod"].ToString() == deptcod)
                    dt1.Rows[j]["deptdesc"] = "";
                deptcod = dt1.Rows[j]["deptcod"].ToString();

            }
            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprocom"];
            this.gvPrjInfo.DataSource = dt;
            this.gvPrjInfo.DataBind();

            ((Label)this.gvPrjInfo.FooterRow.FindControl("lblgvFbgdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?
                   0 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvPrjInfo.FooterRow.FindControl("lblgvFactamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(actamt)", "")) ?
                   0 : dt.Compute("sum(actamt)", ""))).ToString("#,##0;(#,##0);");

        }
        protected void lbtnCalculaton_Click(object sender, EventArgs e)
        {
            string inidate, inidate1, preenddate;
            DataTable dt = (DataTable)Session["tblprocom"];

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {

                int duration = Convert.ToInt32("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvduration")).Text.Trim());
                string inidate2 = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txttarEndDate")).Text.Trim();

                if (duration > 0 && inidate2 == "")
                {
                    //Convert.ToDateTime(this.txtdate.Text);
                    inidate1 = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtTarsDate")).Text.Trim();
                    if (i != 0)
                    {
                        inidate = (((TextBox)this.gvPrjInfo.Rows[i - 1].FindControl("txttarEndDate")).Text.Trim() == "") ? ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtTarsDate")).Text.Trim()
                            : ((TextBox)this.gvPrjInfo.Rows[i - 1].FindControl("txttarEndDate")).Text.Trim();


                    }
                    else
                    {
                        inidate = inidate1;
                    }
                    if (inidate == "")
                        continue;

                    dt.Rows[i]["duration"] = duration;
                    dt.Rows[i]["tarsdat"] = inidate;//(inidate1 == "") ? inidate.ToString() : inidate1;
                    dt.Rows[i]["taredat"] = Convert.ToDateTime(inidate).AddDays(duration - 1); //(inidate1 == "") ? inidate.AddDays(duration - 1) : Convert.ToDateTime(inidate1).AddDays(duration - 1); //inidate.AddDays(duration - 1);

                    ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtTarsDate")).Text = inidate;
                    ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txttarEndDate")).Text = Convert.ToDateTime(inidate).AddDays(duration - 1).ToString();
                }







            }

            Session["tblprocom"] = dt;
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblprocom"];

            ReportDocument rptResource = new RealERPRPT.R_08_PPlan.rptProFlowChart01();
            TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rpttxtComName.Text = comnam;

            TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtprojectname"] as TextObject;
            rpttxtProName.Text = this.ddlPrjName.SelectedItem.ToString().Substring(13);

            TextObject rpttxtDate = rptResource.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtDate.Text = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptResource.SetDataSource(dt);
            Session["Report1"] = rptResource;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {
                string Actcode = ((Label)this.gvPrjInfo.Rows[i].FindControl("lgvActcode")).Text.Trim();
                //string Deptcode = ((Label)this.gvPrjInfo.Rows[i].FindControl("lgvdeptcode")).Text.Trim();
                //string mindur = Convert.ToDouble("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvMinDur")).Text.Trim()).ToString() ;
                //string maxdur = Convert.ToDouble("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvMaxDur")).Text.Trim()).ToString();
                string tarsdat = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtTarsDate")).Text.Trim() == "" ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtTarsDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string taredat = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txttarEndDate")).Text.Trim() == "" ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txttarEndDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string strtdat = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtAcStDate")).Text.Trim() == "" ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtAcStDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string enddate = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtAcEndDate")).Text.Trim() == "" ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtAcEndDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string remarks = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvRemark")).Text.Trim();
                string repons = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvRespon")).Text.Trim();
                double BgdAmt = Convert.ToDouble("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvbgdamt")).Text.Trim());
                double ActAmt = Convert.ToDouble("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvactamt")).Text.Trim());

                if (tarsdat != "01-Jan-1900" && taredat != "01-Jan-1900")
                {
                    bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "PROJECTCOMFLOWUPDATE", pactcode, Actcode, tarsdat, taredat, strtdat, enddate, repons, remarks, BgdAmt.ToString(), ActAmt.ToString(), userid, PostedByid, "", "", "");

                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                        return;
                    }
                }
                //Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.LoadGrid();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Project Information";
                string eventdesc = "Update Project Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        protected void gvPrjInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvPrjInfo_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 3;

                TableCell cell1 = new TableCell();
                cell1.Text = "TARGET";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 3;
                cell1.Font.Bold = true;

                TableCell cell2 = new TableCell();
                cell2.Text = "";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 2;
                cell2.Font.Bold = true;

                TableCell cell3 = new TableCell();
                cell3.Text = "ACTUAL";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 7;
                cell3.Font.Bold = true;

                TableCell cell4 = new TableCell();
                cell4.Text = "";
                cell4.HorizontalAlign = HorizontalAlign.Center;
                cell4.ColumnSpan = 3;
                cell4.Font.Bold = true;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                gvrow.Cells.Add(cell4);
                gvPrjInfo.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd");
                LinkButton lbtnAddUsr = (LinkButton)e.Row.FindControl("lbtnAddUsr");
                LinkButton lbtnDoc = (LinkButton)e.Row.FindControl("lbtnDoc");

                DateTime sdate = System.DateTime.Today;
                TextBox txtTarsDate = (TextBox)e.Row.FindControl("txtTarsDate");
                TextBox txttarEndDate = (TextBox)e.Row.FindControl("txttarEndDate");
                Label lblgvdday = (Label)e.Row.FindControl("lblgvdday");
                Label txtgvpercnt = (Label)e.Row.FindControl("txtgvpercnt");


                DateTime tarenddat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "taredat"));

                double duration = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "duration"));
                double maxdur = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "maxdur"));
                string additem = DataBinder.Eval(e.Row.DataItem, "addcomm").ToString();

                string comments = DataBinder.Eval(e.Row.DataItem, "comments").ToString();
                string docurl = DataBinder.Eval(e.Row.DataItem, "docurl").ToString();


                TextBox txtAcStDate = (TextBox)e.Row.FindControl("txtAcStDate");
                TextBox txtAcEndDate = (TextBox)e.Row.FindControl("txtAcEndDate");

                if (duration < maxdur)
                {
                    lblgvdday.Attributes["style"] = "color:red;";
                    txtgvpercnt.Attributes["style"] = "color:red;";
                    lblgvdday.ToolTip = "Delay";
                    txtgvpercnt.ToolTip = "Delay";

                }
                else if (duration > maxdur)
                {
                    txtAcStDate.Attributes["style"] = "color:green;";
                    txtAcEndDate.Attributes["style"] = "color:green;";
                    //txtgvpercnt.Attributes["style"] = "color:green;";

                    txtAcStDate.ToolTip = "In-time";
                    txtAcEndDate.ToolTip = "In-time";

                }

                if (tarenddat != Convert.ToDateTime("01-jan-1900") && tarenddat <= sdate)
                {
                    DateTime acenddat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "acenddat"));
                    if (acenddat == Convert.ToDateTime("01-jan-1900"))
                    {
                        txtTarsDate.Attributes["style"] = "color:red;";
                        txttarEndDate.Attributes["style"] = "color:red;";
                    }

                }
                if (additem == "Y")
                {
                    lbtnAdd.Visible = true;
                    lbtnAddUsr.Visible = true;
                    if (comments.Length != 0)
                    {
                        lbtnAdd.Attributes["style"] = "color:yellow;";
                        lbtnAdd.ToolTip = comments;

                    }
                }

                if (docurl.Length != 0)
                {
                    lbtnDoc.Attributes["style"] = "color:green;";
                    lbtnDoc.ToolTip = "Document Already Uploaded";

                }


            }

        }


        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string actcode = ((Label)this.gvPrjInfo.Rows[RowIndex].FindControl("lgvActcode")).Text.ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "SHOWCOMMENTS", pactcode, actcode, "", "", "", "", "", "");

            ViewState["tblComm"] = ds1.Tables[0];

            this.gvComm.DataSource = ds1.Tables[0];
            this.gvComm.DataBind();


            this.lblactcode.Text = actcode;


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
                string pactcode = this.ddlPrjName.SelectedValue.ToString();
                string actcode = this.lblactcode.Text;

                string gdesc = this.txtComments.Text.Trim();


                DataTable dt = (DataTable)ViewState["tblComm"];

                string cdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                DataRow[] dr2 = dt.Select("cdate='" + cdate + "'");
                if (dr2.Length == 0)
                {
                    dt.Rows.Add(comcod, cdate, gdesc, userid, "");
                    //dr2[0]["cdate"] = cdate;
                    //dr2[0]["comments"] = gdesc;
                    //dr2[0]["usrid"] = userid;
                }




                DataSet ds = new DataSet("ds2");
                //ds.Merge(dt);

                dt.Columns.Remove("comcod");
                dt.Columns.Remove("username");

                ds.Merge(dt);
                ds.Tables[0].TableName = "tbl1";



                if (gdesc.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Comments is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {
                    bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATE_PREPLAN_COMM", ds, null, null, pactcode, actcode, "", "", "", "", "", "", "", "", "", "", "", "");



                    if (!result)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }


                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    //this.ShowInformation();


                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }


        protected void lbtnAddUsr_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;


            this.Getuser();

            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string actcode = ((Label)this.gvPrjInfo.Rows[RowIndex].FindControl("lgvActcode")).Text.ToString();

            DataTable dt = ((DataTable)Session["tblprocom"]).Copy();

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='" + actcode + "' and assusrid <> ''");
            DataTable dt1 = dv.ToTable();
            if (dt1.Rows.Count != 0)
            {
                this.ddlUserList.SelectedValue = dv.ToTable().Rows[0]["assusrid"].ToString();

            }

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();


            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "SHOWCOMMENTS", pactcode, actcode, "", "", "", "", "", "");

            //ViewState["tblComm"] = ds1.Tables[0];

            //this.gvComm.DataSource = ds1.Tables[0];
            //this.gvComm.DataBind();


            this.lblactcode.Text = actcode;


            // Project Link

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "UsrloadModal();", true);
        }

        protected void lbtnUpdateUsr_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string pactcode = this.ddlPrjName.SelectedValue.ToString();
                string actcode = this.lblactcode.Text;




                DataTable dt = (DataTable)ViewState["tblComm"];

                string Assuserid = this.ddlUserList.SelectedValue.ToString();



                if (Assuserid.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Comments is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {
                    bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "UPDATEUSER", pactcode, actcode, Assuserid);



                    if (!result)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }


                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    //this.ShowInformation();


                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        protected void lbtnDoc_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string actcode = ((Label)this.gvPrjInfo.Rows[RowIndex].FindControl("lgvActcode")).Text.ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "SHOWUPDOC", pactcode, actcode, "", "", "", "", "", "");

            ViewState["tblupdoc"] = ds1.Tables[0];


            ListViewEmpAll.DataSource = ds1.Tables[0];
            ListViewEmpAll.DataBind();




            this.lblImcode.Text = actcode;




            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "DocloadModal();", true);
        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;


                }

            }

        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetComCode();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string actcode = this.lblImcode.Text;
            string docurl = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userid = hst["usrid"].ToString();

            if (AsyncFileUpload1.HasFile)
            {

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/Legal/") + random + extension);

                //docurl = "~/Upload/Legal/" + random + extension;

                docurl = "Upload/Legal/" + random + extension;


                DataTable dt = (DataTable)ViewState["tblupdoc"];

                string cdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                DataRow[] dr2 = dt.Select("cdate='" + cdate + "'");
                if (dr2.Length == 0)
                {
                    dt.Rows.Add(comcod, cdate, docurl, userid, "");
                }

                DataSet ds = new DataSet("ds2");
                //ds.Merge(dt);

                dt.Columns.Remove("comcod");
                dt.Columns.Remove("username");

                ds.Merge(dt);
                ds.Tables[0].TableName = "tbl1";

                if (docurl.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Doc is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {

                    bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATE_DOC", ds, null, null, pactcode, actcode, "", "", "", "", "", "", "", "", "", "", "", "");

                    if (result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                        return;

                    }

                    ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }


            //this.lbtnDoc_Click(null, null);
        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string actcode = this.lblImcode.Text;
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string cdate = ((Label)this.ListViewEmpAll.Items[j].FindControl("lblcdate")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "DELETEXMLDATA", pactcode, actcode, cdate, "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        //string filePath = Server.MapPath("/Upload/Legal/");
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + filesname);


                        ((Label)this.Master.FindControl("lblmsg")).Text = " Files Removed ";
                    }


                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "DocloadModal();", true);
            //if (this.Request.QueryString["genno"].Length != 0)
            //{
            //    this.lnkReqList_Click(null, null);
            //    this.lbtnMSROk.Text = "Ok";
            //    this.lbtnMSROk_Click(null, null);

            //}
            //this.viewseciton();

        }
    }
}



